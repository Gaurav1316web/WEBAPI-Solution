<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProductDispatchGateOut
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblTransportName = New common.Controls.MyLabel()
        Me.txtTransportId = New common.Controls.MyTextBox()
        Me.lblWeighmentNo = New common.Controls.MyLabel()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtCustomerName = New common.Controls.MyTextBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtCustomerCode = New common.Controls.MyTextBox()
        Me.fndDispatchNo = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblToLoc = New common.Controls.MyLabel()
        Me.lblFromLoc = New common.Controls.MyLabel()
        Me.txtToLoc = New common.Controls.MyTextBox()
        Me.txtFromLoc = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtVehicleNo = New common.Controls.MyTextBox()
        Me.txtVehicleId = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblStartDate = New common.Controls.MyLabel()
        Me.txtDispatchDate = New common.Controls.MyTextBox()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.txtGateOutDate = New common.Controls.MyDateTimePicker()
        Me.fndDocNo = New common.UserControls.txtNavigator()
        Me.lblDocNo = New common.Controls.MyLabel()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblTransportName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTransportId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWeighmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomerCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToLoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromLoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToLoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromLoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicleId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDispatchDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGateOutDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(836, 296)
        Me.SplitContainer1.SplitterDistance = 267
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(836, 267)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.lblTransportName)
        Me.RadPageViewPage1.Controls.Add(Me.txtTransportId)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel11)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustomerName)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustomerCode)
        Me.RadPageViewPage1.Controls.Add(Me.fndDispatchNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.lblToLoc)
        Me.RadPageViewPage1.Controls.Add(Me.lblFromLoc)
        Me.RadPageViewPage1.Controls.Add(Me.txtToLoc)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromLoc)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.txtVehicleNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtVehicleId)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.lblStartDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtDispatchDate)
        Me.RadPageViewPage1.Controls.Add(Me.btnReset)
        Me.RadPageViewPage1.Controls.Add(Me.txtGateOutDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblWeighmentNo)
        Me.RadPageViewPage1.Controls.Add(Me.fndDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblDocNo)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(815, 219)
        Me.RadPageViewPage1.Text = "Details"
        '
        'lblTransportName
        '
        Me.lblTransportName.AutoSize = False
        Me.lblTransportName.BorderVisible = True
        Me.lblTransportName.FieldName = Nothing
        Me.lblTransportName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransportName.Location = New System.Drawing.Point(200, 117)
        Me.lblTransportName.Name = "lblTransportName"
        Me.lblTransportName.Size = New System.Drawing.Size(193, 18)
        Me.lblTransportName.TabIndex = 457
        Me.lblTransportName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTransportName.TextWrap = False
        '
        'txtTransportId
        '
        Me.txtTransportId.CalculationExpression = Nothing
        Me.txtTransportId.FieldCode = Nothing
        Me.txtTransportId.FieldDesc = Nothing
        Me.txtTransportId.FieldMaxLength = 0
        Me.txtTransportId.FieldName = Nothing
        Me.txtTransportId.isCalculatedField = False
        Me.txtTransportId.IsSourceFromTable = False
        Me.txtTransportId.IsSourceFromValueList = False
        Me.txtTransportId.IsUnique = False
        Me.txtTransportId.Location = New System.Drawing.Point(88, 115)
        Me.txtTransportId.MaxLength = 50
        Me.txtTransportId.MendatroryField = False
        Me.txtTransportId.MyLinkLable1 = Me.lblWeighmentNo
        Me.txtTransportId.MyLinkLable2 = Nothing
        Me.txtTransportId.Name = "txtTransportId"
        Me.txtTransportId.ReadOnly = True
        Me.txtTransportId.ReferenceFieldDesc = Nothing
        Me.txtTransportId.ReferenceFieldName = Nothing
        Me.txtTransportId.ReferenceTableName = Nothing
        Me.txtTransportId.Size = New System.Drawing.Size(106, 20)
        Me.txtTransportId.TabIndex = 456
        '
        'lblWeighmentNo
        '
        Me.lblWeighmentNo.FieldName = Nothing
        Me.lblWeighmentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeighmentNo.Location = New System.Drawing.Point(408, 5)
        Me.lblWeighmentNo.Name = "lblWeighmentNo"
        Me.lblWeighmentNo.Size = New System.Drawing.Size(82, 16)
        Me.lblWeighmentNo.TabIndex = 410
        Me.lblWeighmentNo.Text = "Gate Out Date "
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(1, 117)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel11.TabIndex = 455
        Me.MyLabel11.Text = "Transport"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.CalculationExpression = Nothing
        Me.txtCustomerName.FieldCode = Nothing
        Me.txtCustomerName.FieldDesc = Nothing
        Me.txtCustomerName.FieldMaxLength = 0
        Me.txtCustomerName.FieldName = Nothing
        Me.txtCustomerName.isCalculatedField = False
        Me.txtCustomerName.IsSourceFromTable = False
        Me.txtCustomerName.IsSourceFromValueList = False
        Me.txtCustomerName.IsUnique = False
        Me.txtCustomerName.Location = New System.Drawing.Point(503, 48)
        Me.txtCustomerName.MaxLength = 50
        Me.txtCustomerName.MendatroryField = False
        Me.txtCustomerName.MyLinkLable1 = Me.lblWeighmentNo
        Me.txtCustomerName.MyLinkLable2 = Nothing
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.ReadOnly = True
        Me.txtCustomerName.ReferenceFieldDesc = Nothing
        Me.txtCustomerName.ReferenceFieldName = Nothing
        Me.txtCustomerName.ReferenceTableName = Nothing
        Me.txtCustomerName.Size = New System.Drawing.Size(305, 20)
        Me.txtCustomerName.TabIndex = 422
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(1, 31)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel5.TabIndex = 426
        Me.MyLabel5.Text = "Dispatch No."
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.CalculationExpression = Nothing
        Me.txtCustomerCode.FieldCode = Nothing
        Me.txtCustomerCode.FieldDesc = Nothing
        Me.txtCustomerCode.FieldMaxLength = 0
        Me.txtCustomerCode.FieldName = Nothing
        Me.txtCustomerCode.isCalculatedField = False
        Me.txtCustomerCode.IsSourceFromTable = False
        Me.txtCustomerCode.IsSourceFromValueList = False
        Me.txtCustomerCode.IsUnique = False
        Me.txtCustomerCode.Location = New System.Drawing.Point(88, 49)
        Me.txtCustomerCode.MaxLength = 50
        Me.txtCustomerCode.MendatroryField = False
        Me.txtCustomerCode.MyLinkLable1 = Me.lblWeighmentNo
        Me.txtCustomerCode.MyLinkLable2 = Nothing
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.ReadOnly = True
        Me.txtCustomerCode.ReferenceFieldDesc = Nothing
        Me.txtCustomerCode.ReferenceFieldName = Nothing
        Me.txtCustomerCode.ReferenceTableName = Nothing
        Me.txtCustomerCode.Size = New System.Drawing.Size(305, 20)
        Me.txtCustomerCode.TabIndex = 421
        '
        'fndDispatchNo
        '
        Me.fndDispatchNo.CalculationExpression = Nothing
        Me.fndDispatchNo.FieldCode = Nothing
        Me.fndDispatchNo.FieldDesc = Nothing
        Me.fndDispatchNo.FieldMaxLength = 0
        Me.fndDispatchNo.FieldName = Nothing
        Me.fndDispatchNo.isCalculatedField = False
        Me.fndDispatchNo.IsSourceFromTable = False
        Me.fndDispatchNo.IsSourceFromValueList = False
        Me.fndDispatchNo.IsUnique = False
        Me.fndDispatchNo.Location = New System.Drawing.Point(88, 27)
        Me.fndDispatchNo.MendatroryField = True
        Me.fndDispatchNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDispatchNo.MyLinkLable1 = Nothing
        Me.fndDispatchNo.MyLinkLable2 = Nothing
        Me.fndDispatchNo.MyReadOnly = False
        Me.fndDispatchNo.MyShowMasterFormButton = False
        Me.fndDispatchNo.Name = "fndDispatchNo"
        Me.fndDispatchNo.ReferenceFieldDesc = Nothing
        Me.fndDispatchNo.ReferenceFieldName = Nothing
        Me.fndDispatchNo.ReferenceTableName = Nothing
        Me.fndDispatchNo.Size = New System.Drawing.Size(305, 19)
        Me.fndDispatchNo.TabIndex = 425
        Me.fndDispatchNo.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(408, 52)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(89, 16)
        Me.MyLabel6.TabIndex = 420
        Me.MyLabel6.Text = "Customer Name"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(1, 54)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel7.TabIndex = 419
        Me.MyLabel7.Text = "Customer Code"
        '
        'lblToLoc
        '
        Me.lblToLoc.AutoSize = False
        Me.lblToLoc.BorderVisible = True
        Me.lblToLoc.FieldName = Nothing
        Me.lblToLoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToLoc.Location = New System.Drawing.Point(615, 93)
        Me.lblToLoc.Name = "lblToLoc"
        Me.lblToLoc.Size = New System.Drawing.Size(193, 18)
        Me.lblToLoc.TabIndex = 424
        Me.lblToLoc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblToLoc.TextWrap = False
        '
        'lblFromLoc
        '
        Me.lblFromLoc.AutoSize = False
        Me.lblFromLoc.BorderVisible = True
        Me.lblFromLoc.FieldName = Nothing
        Me.lblFromLoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromLoc.Location = New System.Drawing.Point(200, 95)
        Me.lblFromLoc.Name = "lblFromLoc"
        Me.lblFromLoc.Size = New System.Drawing.Size(193, 18)
        Me.lblFromLoc.TabIndex = 423
        Me.lblFromLoc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFromLoc.TextWrap = False
        '
        'txtToLoc
        '
        Me.txtToLoc.CalculationExpression = Nothing
        Me.txtToLoc.FieldCode = Nothing
        Me.txtToLoc.FieldDesc = Nothing
        Me.txtToLoc.FieldMaxLength = 0
        Me.txtToLoc.FieldName = Nothing
        Me.txtToLoc.isCalculatedField = False
        Me.txtToLoc.IsSourceFromTable = False
        Me.txtToLoc.IsSourceFromValueList = False
        Me.txtToLoc.IsUnique = False
        Me.txtToLoc.Location = New System.Drawing.Point(503, 91)
        Me.txtToLoc.MaxLength = 50
        Me.txtToLoc.MendatroryField = False
        Me.txtToLoc.MyLinkLable1 = Me.lblWeighmentNo
        Me.txtToLoc.MyLinkLable2 = Nothing
        Me.txtToLoc.Name = "txtToLoc"
        Me.txtToLoc.ReadOnly = True
        Me.txtToLoc.ReferenceFieldDesc = Nothing
        Me.txtToLoc.ReferenceFieldName = Nothing
        Me.txtToLoc.ReferenceTableName = Nothing
        Me.txtToLoc.Size = New System.Drawing.Size(108, 20)
        Me.txtToLoc.TabIndex = 422
        '
        'txtFromLoc
        '
        Me.txtFromLoc.CalculationExpression = Nothing
        Me.txtFromLoc.FieldCode = Nothing
        Me.txtFromLoc.FieldDesc = Nothing
        Me.txtFromLoc.FieldMaxLength = 0
        Me.txtFromLoc.FieldName = Nothing
        Me.txtFromLoc.isCalculatedField = False
        Me.txtFromLoc.IsSourceFromTable = False
        Me.txtFromLoc.IsSourceFromValueList = False
        Me.txtFromLoc.IsUnique = False
        Me.txtFromLoc.Location = New System.Drawing.Point(88, 93)
        Me.txtFromLoc.MaxLength = 50
        Me.txtFromLoc.MendatroryField = False
        Me.txtFromLoc.MyLinkLable1 = Me.lblWeighmentNo
        Me.txtFromLoc.MyLinkLable2 = Nothing
        Me.txtFromLoc.Name = "txtFromLoc"
        Me.txtFromLoc.ReadOnly = True
        Me.txtFromLoc.ReferenceFieldDesc = Nothing
        Me.txtFromLoc.ReferenceFieldName = Nothing
        Me.txtFromLoc.ReferenceTableName = Nothing
        Me.txtFromLoc.Size = New System.Drawing.Size(106, 20)
        Me.txtFromLoc.TabIndex = 421
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(408, 95)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel4.TabIndex = 420
        Me.MyLabel4.Text = "Ship To Location"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(1, 95)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel3.TabIndex = 419
        Me.MyLabel3.Text = "Bill To Location"
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.CalculationExpression = Nothing
        Me.txtVehicleNo.FieldCode = Nothing
        Me.txtVehicleNo.FieldDesc = Nothing
        Me.txtVehicleNo.FieldMaxLength = 0
        Me.txtVehicleNo.FieldName = Nothing
        Me.txtVehicleNo.isCalculatedField = False
        Me.txtVehicleNo.IsSourceFromTable = False
        Me.txtVehicleNo.IsSourceFromValueList = False
        Me.txtVehicleNo.IsUnique = False
        Me.txtVehicleNo.Location = New System.Drawing.Point(503, 70)
        Me.txtVehicleNo.MaxLength = 50
        Me.txtVehicleNo.MendatroryField = False
        Me.txtVehicleNo.MyLinkLable1 = Me.lblWeighmentNo
        Me.txtVehicleNo.MyLinkLable2 = Nothing
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.ReadOnly = True
        Me.txtVehicleNo.ReferenceFieldDesc = Nothing
        Me.txtVehicleNo.ReferenceFieldName = Nothing
        Me.txtVehicleNo.ReferenceTableName = Nothing
        Me.txtVehicleNo.Size = New System.Drawing.Size(305, 20)
        Me.txtVehicleNo.TabIndex = 418
        '
        'txtVehicleId
        '
        Me.txtVehicleId.CalculationExpression = Nothing
        Me.txtVehicleId.FieldCode = Nothing
        Me.txtVehicleId.FieldDesc = Nothing
        Me.txtVehicleId.FieldMaxLength = 0
        Me.txtVehicleId.FieldName = Nothing
        Me.txtVehicleId.isCalculatedField = False
        Me.txtVehicleId.IsSourceFromTable = False
        Me.txtVehicleId.IsSourceFromValueList = False
        Me.txtVehicleId.IsUnique = False
        Me.txtVehicleId.Location = New System.Drawing.Point(88, 71)
        Me.txtVehicleId.MaxLength = 50
        Me.txtVehicleId.MendatroryField = False
        Me.txtVehicleId.MyLinkLable1 = Me.lblWeighmentNo
        Me.txtVehicleId.MyLinkLable2 = Nothing
        Me.txtVehicleId.Name = "txtVehicleId"
        Me.txtVehicleId.ReadOnly = True
        Me.txtVehicleId.ReferenceFieldDesc = Nothing
        Me.txtVehicleId.ReferenceFieldName = Nothing
        Me.txtVehicleId.ReferenceTableName = Nothing
        Me.txtVehicleId.Size = New System.Drawing.Size(305, 20)
        Me.txtVehicleId.TabIndex = 417
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(408, 74)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel2.TabIndex = 416
        Me.MyLabel2.Text = "Vehicle No"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(1, 76)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel1.TabIndex = 415
        Me.MyLabel1.Text = "Vehicle Id"
        '
        'lblStartDate
        '
        Me.lblStartDate.FieldName = Nothing
        Me.lblStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartDate.Location = New System.Drawing.Point(408, 29)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(80, 16)
        Me.lblStartDate.TabIndex = 414
        Me.lblStartDate.Text = "Dispatch Date "
        '
        'txtDispatchDate
        '
        Me.txtDispatchDate.CalculationExpression = Nothing
        Me.txtDispatchDate.FieldCode = Nothing
        Me.txtDispatchDate.FieldDesc = Nothing
        Me.txtDispatchDate.FieldMaxLength = 0
        Me.txtDispatchDate.FieldName = Nothing
        Me.txtDispatchDate.isCalculatedField = False
        Me.txtDispatchDate.IsSourceFromTable = False
        Me.txtDispatchDate.IsSourceFromValueList = False
        Me.txtDispatchDate.IsUnique = False
        Me.txtDispatchDate.Location = New System.Drawing.Point(503, 26)
        Me.txtDispatchDate.MaxLength = 50
        Me.txtDispatchDate.MendatroryField = False
        Me.txtDispatchDate.MyLinkLable1 = Me.lblWeighmentNo
        Me.txtDispatchDate.MyLinkLable2 = Nothing
        Me.txtDispatchDate.Name = "txtDispatchDate"
        Me.txtDispatchDate.ReadOnly = True
        Me.txtDispatchDate.ReferenceFieldDesc = Nothing
        Me.txtDispatchDate.ReferenceFieldName = Nothing
        Me.txtDispatchDate.ReferenceTableName = Nothing
        Me.txtDispatchDate.Size = New System.Drawing.Size(305, 20)
        Me.txtDispatchDate.TabIndex = 413
        '
        'btnReset
        '
        Me.btnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(375, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(18, 18)
        Me.btnReset.TabIndex = 412
        '
        'txtGateOutDate
        '
        Me.txtGateOutDate.CalculationExpression = Nothing
        Me.txtGateOutDate.CustomFormat = "dd/MM/yyyy"
        Me.txtGateOutDate.FieldCode = Nothing
        Me.txtGateOutDate.FieldDesc = Nothing
        Me.txtGateOutDate.FieldMaxLength = 0
        Me.txtGateOutDate.FieldName = Nothing
        Me.txtGateOutDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGateOutDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtGateOutDate.isCalculatedField = False
        Me.txtGateOutDate.IsSourceFromTable = False
        Me.txtGateOutDate.IsSourceFromValueList = False
        Me.txtGateOutDate.IsUnique = False
        Me.txtGateOutDate.Location = New System.Drawing.Point(503, 4)
        Me.txtGateOutDate.MendatroryField = False
        Me.txtGateOutDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtGateOutDate.MyLinkLable1 = Nothing
        Me.txtGateOutDate.MyLinkLable2 = Nothing
        Me.txtGateOutDate.Name = "txtGateOutDate"
        Me.txtGateOutDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtGateOutDate.ReferenceFieldDesc = Nothing
        Me.txtGateOutDate.ReferenceFieldName = Nothing
        Me.txtGateOutDate.ReferenceTableName = Nothing
        Me.txtGateOutDate.Size = New System.Drawing.Size(79, 18)
        Me.txtGateOutDate.TabIndex = 411
        Me.txtGateOutDate.TabStop = False
        Me.txtGateOutDate.Text = "13/06/2011"
        Me.txtGateOutDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'fndDocNo
        '
        Me.fndDocNo.FieldName = Nothing
        Me.fndDocNo.Location = New System.Drawing.Point(88, 4)
        Me.fndDocNo.MendatroryField = False
        Me.fndDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocNo.MyLinkLable1 = Me.lblDocNo
        Me.fndDocNo.MyLinkLable2 = Nothing
        Me.fndDocNo.MyMaxLength = 32767
        Me.fndDocNo.MyReadOnly = False
        Me.fndDocNo.Name = "fndDocNo"
        Me.fndDocNo.Size = New System.Drawing.Size(287, 18)
        Me.fndDocNo.TabIndex = 408
        Me.fndDocNo.Value = ""
        '
        'lblDocNo
        '
        Me.lblDocNo.FieldName = Nothing
        Me.lblDocNo.Location = New System.Drawing.Point(1, 5)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(79, 18)
        Me.lblDocNo.TabIndex = 409
        Me.lblDocNo.Text = "Document No."
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(765, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 11
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(73, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 13
        Me.btnDelete.Text = "Delete"
        '
        'FrmProductDispatchGateOut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(836, 296)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmProductDispatchGateOut"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Product Dispatch Gate Out"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblTransportName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTransportId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWeighmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomerCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToLoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromLoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToLoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromLoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicleId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDispatchDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGateOutDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtGateOutDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblWeighmentNo As common.Controls.MyLabel
    Friend WithEvents fndDocNo As common.UserControls.txtNavigator
    Friend WithEvents lblDocNo As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents fndDispatchNo As common.UserControls.txtFinder
    Friend WithEvents lblToLoc As common.Controls.MyLabel
    Friend WithEvents lblFromLoc As common.Controls.MyLabel
    Friend WithEvents txtToLoc As common.Controls.MyTextBox
    Friend WithEvents txtFromLoc As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtVehicleNo As common.Controls.MyTextBox
    Friend WithEvents txtVehicleId As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblStartDate As common.Controls.MyLabel
    Friend WithEvents txtDispatchDate As common.Controls.MyTextBox
    Friend WithEvents txtCustomerName As common.Controls.MyTextBox
    Friend WithEvents txtCustomerCode As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents lblTransportName As common.Controls.MyLabel
    Friend WithEvents txtTransportId As common.Controls.MyTextBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
End Class

