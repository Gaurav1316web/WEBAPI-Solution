<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGatePassPS
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
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.lblSalesman = New common.Controls.MyLabel()
        Me.lblpaymentno = New common.Controls.MyLabel()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkOwnVehicle = New System.Windows.Forms.CheckBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtManualVehicleNo = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtdate = New common.Controls.MyDateTimePicker()
        Me.lblProvAmt = New common.Controls.MyLabel()
        Me.lblProvNo = New common.Controls.MyLabel()
        Me.NumProvisionAmount = New common.MyNumBox()
        Me.txtProvisionDocNo = New common.Controls.MyTextBox()
        Me.txtVehicleCapacity = New common.MyNumBox()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtGrossWeight = New common.MyNumBox()
        Me.dtpToDate = New common.Controls.MyDateTimePicker()
        Me.dtpFromDate = New common.Controls.MyDateTimePicker()
        Me.lblGrossWeight = New common.Controls.MyLabel()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.lblFromDate = New common.Controls.MyLabel()
        Me.txtCityName = New common.Controls.MyLabel()
        Me.lblCity = New common.Controls.MyLabel()
        Me.fndCityCode = New common.UserControls.txtFinder()
        Me.txtTransporterName = New common.Controls.MyLabel()
        Me.fndTransporterCode = New common.UserControls.txtFinder()
        Me.txtVehicleName = New common.Controls.MyLabel()
        Me.txtLocationName = New common.Controls.MyLabel()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.fndLocationCode = New common.UserControls.txtFinder()
        Me.txtComments = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.fndVehicleCode = New common.UserControls.txtFinder()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblpaymentpostdate = New common.Controls.MyLabel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnDeleteInvoiceafterPost = New Telerik.WinControls.UI.RadButton()
        Me.btnprint = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSelect = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtManualVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProvAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProvNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumProvisionAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProvisionDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicleCapacity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGrossWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGrossWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCityName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTransporterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicleName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentpostdate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnDeleteInvoiceafterPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 239)
        '
        'Gv1
        '
        Me.Gv1.MasterTemplate.EnableFiltering = True
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.Name = "Gv1"
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(1033, 236)
        Me.Gv1.TabIndex = 0
        Me.Gv1.Text = "RadGridView1"
        '
        'lblSalesman
        '
        Me.lblSalesman.FieldName = Nothing
        Me.lblSalesman.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesman.Location = New System.Drawing.Point(4, 96)
        Me.lblSalesman.Name = "lblSalesman"
        Me.lblSalesman.Size = New System.Drawing.Size(61, 16)
        Me.lblSalesman.TabIndex = 35
        Me.lblSalesman.Text = "Vehicle No"
        '
        'lblpaymentno
        '
        Me.lblpaymentno.FieldName = Nothing
        Me.lblpaymentno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentno.Location = New System.Drawing.Point(4, 6)
        Me.lblpaymentno.Name = "lblpaymentno"
        Me.lblpaymentno.Size = New System.Drawing.Size(77, 16)
        Me.lblpaymentno.TabIndex = 30
        Me.lblpaymentno.Text = "Gate Pass No"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(74, 9)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(64, 24)
        Me.btnSave.TabIndex = 11
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(962, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 24)
        Me.btnClose.TabIndex = 21
        Me.btnClose.Text = "Close"
        '
        'btnNew
        '
        Me.btnNew.BackgroundImage = Global.ERP.My.Resources.Resources._new
        Me.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(339, 4)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(15, 20)
        Me.btnNew.TabIndex = 32
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkOwnVehicle)
        Me.Panel1.Controls.Add(Me.MyLabel5)
        Me.Panel1.Controls.Add(Me.txtManualVehicleNo)
        Me.Panel1.Controls.Add(Me.txtdate)
        Me.Panel1.Controls.Add(Me.lblProvAmt)
        Me.Panel1.Controls.Add(Me.lblProvNo)
        Me.Panel1.Controls.Add(Me.NumProvisionAmount)
        Me.Panel1.Controls.Add(Me.txtProvisionDocNo)
        Me.Panel1.Controls.Add(Me.txtVehicleCapacity)
        Me.Panel1.Controls.Add(Me.MyLabel19)
        Me.Panel1.Controls.Add(Me.UsLock1)
        Me.Panel1.Controls.Add(Me.txtGrossWeight)
        Me.Panel1.Controls.Add(Me.dtpToDate)
        Me.Panel1.Controls.Add(Me.dtpFromDate)
        Me.Panel1.Controls.Add(Me.lblGrossWeight)
        Me.Panel1.Controls.Add(Me.lblToDate)
        Me.Panel1.Controls.Add(Me.lblFromDate)
        Me.Panel1.Controls.Add(Me.txtCityName)
        Me.Panel1.Controls.Add(Me.lblCity)
        Me.Panel1.Controls.Add(Me.fndCityCode)
        Me.Panel1.Controls.Add(Me.txtTransporterName)
        Me.Panel1.Controls.Add(Me.fndTransporterCode)
        Me.Panel1.Controls.Add(Me.txtVehicleName)
        Me.Panel1.Controls.Add(Me.txtLocationName)
        Me.Panel1.Controls.Add(Me.btnGo)
        Me.Panel1.Controls.Add(Me.MyLabel4)
        Me.Panel1.Controls.Add(Me.fndLocationCode)
        Me.Panel1.Controls.Add(Me.txtComments)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Controls.Add(Me.MyLabel1)
        Me.Panel1.Controls.Add(Me.lblSalesman)
        Me.Panel1.Controls.Add(Me.lblpaymentno)
        Me.Panel1.Controls.Add(Me.btnNew)
        Me.Panel1.Controls.Add(Me.fndVehicleCode)
        Me.Panel1.Controls.Add(Me.txtCode)
        Me.Panel1.Controls.Add(Me.lblpaymentpostdate)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1033, 239)
        Me.Panel1.TabIndex = 0
        '
        'chkOwnVehicle
        '
        Me.chkOwnVehicle.AutoSize = True
        Me.chkOwnVehicle.Location = New System.Drawing.Point(740, 139)
        Me.chkOwnVehicle.Name = "chkOwnVehicle"
        Me.chkOwnVehicle.Size = New System.Drawing.Size(90, 17)
        Me.chkOwnVehicle.TabIndex = 1465
        Me.chkOwnVehicle.Text = "Own Vehicle"
        Me.chkOwnVehicle.UseVisualStyleBackColor = True
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(4, 118)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel5.TabIndex = 1464
        Me.MyLabel5.Text = "Man Vehicle No"
        '
        'txtManualVehicleNo
        '
        Me.txtManualVehicleNo.CalculationExpression = Nothing
        Me.txtManualVehicleNo.FieldCode = Nothing
        Me.txtManualVehicleNo.FieldDesc = Nothing
        Me.txtManualVehicleNo.FieldMaxLength = 0
        Me.txtManualVehicleNo.FieldName = Nothing
        Me.txtManualVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtManualVehicleNo.isCalculatedField = False
        Me.txtManualVehicleNo.IsSourceFromTable = False
        Me.txtManualVehicleNo.IsSourceFromValueList = False
        Me.txtManualVehicleNo.IsUnique = False
        Me.txtManualVehicleNo.Location = New System.Drawing.Point(101, 115)
        Me.txtManualVehicleNo.MaxLength = 200
        Me.txtManualVehicleNo.MendatroryField = False
        Me.txtManualVehicleNo.MyLinkLable1 = Me.MyLabel3
        Me.txtManualVehicleNo.MyLinkLable2 = Nothing
        Me.txtManualVehicleNo.Name = "txtManualVehicleNo"
        Me.txtManualVehicleNo.ReferenceFieldDesc = Nothing
        Me.txtManualVehicleNo.ReferenceFieldName = Nothing
        Me.txtManualVehicleNo.ReferenceTableName = Nothing
        Me.txtManualVehicleNo.Size = New System.Drawing.Size(631, 18)
        Me.txtManualVehicleNo.TabIndex = 1463
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(4, 165)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel3.TabIndex = 36
        Me.MyLabel3.Text = "Remarks"
        '
        'txtdate
        '
        Me.txtdate.CalculationExpression = Nothing
        Me.txtdate.CustomFormat = "dd-MM-yyyy"
        Me.txtdate.FieldCode = Nothing
        Me.txtdate.FieldDesc = Nothing
        Me.txtdate.FieldMaxLength = 0
        Me.txtdate.FieldName = Nothing
        Me.txtdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtdate.isCalculatedField = False
        Me.txtdate.IsSourceFromTable = False
        Me.txtdate.IsSourceFromValueList = False
        Me.txtdate.IsUnique = False
        Me.txtdate.Location = New System.Drawing.Point(455, 6)
        Me.txtdate.MendatroryField = False
        Me.txtdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdate.MyLinkLable1 = Nothing
        Me.txtdate.MyLinkLable2 = Nothing
        Me.txtdate.Name = "txtdate"
        Me.txtdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdate.ReferenceFieldDesc = Nothing
        Me.txtdate.ReferenceFieldName = Nothing
        Me.txtdate.ReferenceTableName = Nothing
        Me.txtdate.Size = New System.Drawing.Size(92, 20)
        Me.txtdate.TabIndex = 1461
        Me.txtdate.TabStop = False
        Me.txtdate.Text = "17-12-2011"
        Me.txtdate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblProvAmt
        '
        Me.lblProvAmt.FieldName = Nothing
        Me.lblProvAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProvAmt.Location = New System.Drawing.Point(591, 28)
        Me.lblProvAmt.Name = "lblProvAmt"
        Me.lblProvAmt.Size = New System.Drawing.Size(59, 16)
        Me.lblProvAmt.TabIndex = 1460
        Me.lblProvAmt.Text = "Prov. Amt."
        '
        'lblProvNo
        '
        Me.lblProvNo.FieldName = Nothing
        Me.lblProvNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProvNo.Location = New System.Drawing.Point(596, 6)
        Me.lblProvNo.Name = "lblProvNo"
        Me.lblProvNo.Size = New System.Drawing.Size(50, 16)
        Me.lblProvNo.TabIndex = 1459
        Me.lblProvNo.Text = "Prov. No"
        '
        'NumProvisionAmount
        '
        Me.NumProvisionAmount.CalculationExpression = Nothing
        Me.NumProvisionAmount.DecimalPlaces = 0
        Me.NumProvisionAmount.FieldCode = Nothing
        Me.NumProvisionAmount.FieldDesc = Nothing
        Me.NumProvisionAmount.FieldMaxLength = 0
        Me.NumProvisionAmount.FieldName = Nothing
        Me.NumProvisionAmount.isCalculatedField = False
        Me.NumProvisionAmount.IsSourceFromTable = False
        Me.NumProvisionAmount.IsSourceFromValueList = False
        Me.NumProvisionAmount.IsUnique = False
        Me.NumProvisionAmount.Location = New System.Drawing.Point(652, 25)
        Me.NumProvisionAmount.MendatroryField = False
        Me.NumProvisionAmount.MyLinkLable1 = Nothing
        Me.NumProvisionAmount.MyLinkLable2 = Nothing
        Me.NumProvisionAmount.Name = "NumProvisionAmount"
        Me.NumProvisionAmount.ReferenceFieldDesc = Nothing
        Me.NumProvisionAmount.ReferenceFieldName = Nothing
        Me.NumProvisionAmount.ReferenceTableName = Nothing
        Me.NumProvisionAmount.Size = New System.Drawing.Size(151, 20)
        Me.NumProvisionAmount.TabIndex = 1458
        Me.NumProvisionAmount.Text = "0"
        Me.NumProvisionAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumProvisionAmount.Value = 0.0R
        '
        'txtProvisionDocNo
        '
        Me.txtProvisionDocNo.CalculationExpression = Nothing
        Me.txtProvisionDocNo.FieldCode = Nothing
        Me.txtProvisionDocNo.FieldDesc = Nothing
        Me.txtProvisionDocNo.FieldMaxLength = 0
        Me.txtProvisionDocNo.FieldName = Nothing
        Me.txtProvisionDocNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProvisionDocNo.isCalculatedField = False
        Me.txtProvisionDocNo.IsSourceFromTable = False
        Me.txtProvisionDocNo.IsSourceFromValueList = False
        Me.txtProvisionDocNo.IsUnique = False
        Me.txtProvisionDocNo.Location = New System.Drawing.Point(652, 6)
        Me.txtProvisionDocNo.MaxLength = 200
        Me.txtProvisionDocNo.MendatroryField = False
        Me.txtProvisionDocNo.MyLinkLable1 = Me.MyLabel3
        Me.txtProvisionDocNo.MyLinkLable2 = Nothing
        Me.txtProvisionDocNo.Name = "txtProvisionDocNo"
        Me.txtProvisionDocNo.ReferenceFieldDesc = Nothing
        Me.txtProvisionDocNo.ReferenceFieldName = Nothing
        Me.txtProvisionDocNo.ReferenceTableName = Nothing
        Me.txtProvisionDocNo.Size = New System.Drawing.Size(151, 18)
        Me.txtProvisionDocNo.TabIndex = 1457
        '
        'txtVehicleCapacity
        '
        Me.txtVehicleCapacity.CalculationExpression = Nothing
        Me.txtVehicleCapacity.DecimalPlaces = 0
        Me.txtVehicleCapacity.FieldCode = Nothing
        Me.txtVehicleCapacity.FieldDesc = Nothing
        Me.txtVehicleCapacity.FieldMaxLength = 0
        Me.txtVehicleCapacity.FieldName = Nothing
        Me.txtVehicleCapacity.isCalculatedField = False
        Me.txtVehicleCapacity.IsSourceFromTable = False
        Me.txtVehicleCapacity.IsSourceFromValueList = False
        Me.txtVehicleCapacity.IsUnique = False
        Me.txtVehicleCapacity.Location = New System.Drawing.Point(366, 205)
        Me.txtVehicleCapacity.MendatroryField = False
        Me.txtVehicleCapacity.MyLinkLable1 = Nothing
        Me.txtVehicleCapacity.MyLinkLable2 = Nothing
        Me.txtVehicleCapacity.Name = "txtVehicleCapacity"
        Me.txtVehicleCapacity.ReferenceFieldDesc = Nothing
        Me.txtVehicleCapacity.ReferenceFieldName = Nothing
        Me.txtVehicleCapacity.ReferenceTableName = Nothing
        Me.txtVehicleCapacity.Size = New System.Drawing.Size(120, 20)
        Me.txtVehicleCapacity.TabIndex = 1456
        Me.txtVehicleCapacity.Text = "0"
        Me.txtVehicleCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtVehicleCapacity.Value = 0.0R
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(262, 208)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel19.TabIndex = 1455
        Me.MyLabel19.Text = "Vehicle Capacity"
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(915, 6)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(97, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1454
        '
        'txtGrossWeight
        '
        Me.txtGrossWeight.CalculationExpression = Nothing
        Me.txtGrossWeight.DecimalPlaces = 0
        Me.txtGrossWeight.FieldCode = Nothing
        Me.txtGrossWeight.FieldDesc = Nothing
        Me.txtGrossWeight.FieldMaxLength = 0
        Me.txtGrossWeight.FieldName = Nothing
        Me.txtGrossWeight.isCalculatedField = False
        Me.txtGrossWeight.IsSourceFromTable = False
        Me.txtGrossWeight.IsSourceFromValueList = False
        Me.txtGrossWeight.IsUnique = False
        Me.txtGrossWeight.Location = New System.Drawing.Point(112, 205)
        Me.txtGrossWeight.MendatroryField = False
        Me.txtGrossWeight.MyLinkLable1 = Nothing
        Me.txtGrossWeight.MyLinkLable2 = Nothing
        Me.txtGrossWeight.Name = "txtGrossWeight"
        Me.txtGrossWeight.ReadOnly = True
        Me.txtGrossWeight.ReferenceFieldDesc = Nothing
        Me.txtGrossWeight.ReferenceFieldName = Nothing
        Me.txtGrossWeight.ReferenceTableName = Nothing
        Me.txtGrossWeight.Size = New System.Drawing.Size(144, 20)
        Me.txtGrossWeight.TabIndex = 1418
        Me.txtGrossWeight.Text = "0"
        Me.txtGrossWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtGrossWeight.Value = 0.0R
        '
        'dtpToDate
        '
        Me.dtpToDate.CalculationExpression = Nothing
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate.FieldCode = Nothing
        Me.dtpToDate.FieldDesc = Nothing
        Me.dtpToDate.FieldMaxLength = 0
        Me.dtpToDate.FieldName = Nothing
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.isCalculatedField = False
        Me.dtpToDate.IsSourceFromTable = False
        Me.dtpToDate.IsSourceFromValueList = False
        Me.dtpToDate.IsUnique = False
        Me.dtpToDate.Location = New System.Drawing.Point(315, 30)
        Me.dtpToDate.MendatroryField = False
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Nothing
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.ReferenceFieldDesc = Nothing
        Me.dtpToDate.ReferenceFieldName = Nothing
        Me.dtpToDate.ReferenceTableName = Nothing
        Me.dtpToDate.Size = New System.Drawing.Size(156, 20)
        Me.dtpToDate.TabIndex = 1417
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "17-12-2011"
        Me.dtpToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CalculationExpression = Nothing
        Me.dtpFromDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromDate.FieldCode = Nothing
        Me.dtpFromDate.FieldDesc = Nothing
        Me.dtpFromDate.FieldMaxLength = 0
        Me.dtpFromDate.FieldName = Nothing
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.isCalculatedField = False
        Me.dtpFromDate.IsSourceFromTable = False
        Me.dtpFromDate.IsSourceFromValueList = False
        Me.dtpFromDate.IsUnique = False
        Me.dtpFromDate.Location = New System.Drawing.Point(101, 28)
        Me.dtpFromDate.MendatroryField = False
        Me.dtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.MyLinkLable1 = Nothing
        Me.dtpFromDate.MyLinkLable2 = Nothing
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.ReferenceFieldDesc = Nothing
        Me.dtpFromDate.ReferenceFieldName = Nothing
        Me.dtpFromDate.ReferenceTableName = Nothing
        Me.dtpFromDate.Size = New System.Drawing.Size(156, 20)
        Me.dtpFromDate.TabIndex = 1416
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "17-12-2011"
        Me.dtpFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblGrossWeight
        '
        Me.lblGrossWeight.FieldName = Nothing
        Me.lblGrossWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGrossWeight.Location = New System.Drawing.Point(3, 208)
        Me.lblGrossWeight.Name = "lblGrossWeight"
        Me.lblGrossWeight.Size = New System.Drawing.Size(103, 16)
        Me.lblGrossWeight.TabIndex = 1414
        Me.lblGrossWeight.Text = "Total Gross Weight"
        Me.lblGrossWeight.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(263, 33)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(46, 16)
        Me.lblToDate.TabIndex = 1413
        Me.lblToDate.Text = "To Date"
        '
        'lblFromDate
        '
        Me.lblFromDate.FieldName = Nothing
        Me.lblFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.Location = New System.Drawing.Point(4, 28)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(60, 16)
        Me.lblFromDate.TabIndex = 1411
        Me.lblFromDate.Text = "From Date"
        '
        'txtCityName
        '
        Me.txtCityName.AutoSize = False
        Me.txtCityName.BorderVisible = True
        Me.txtCityName.FieldName = Nothing
        Me.txtCityName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCityName.Location = New System.Drawing.Point(264, 76)
        Me.txtCityName.Name = "txtCityName"
        Me.txtCityName.Size = New System.Drawing.Size(471, 18)
        Me.txtCityName.TabIndex = 1409
        Me.txtCityName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCityName.TextWrap = False
        '
        'lblCity
        '
        Me.lblCity.FieldName = Nothing
        Me.lblCity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCity.Location = New System.Drawing.Point(4, 73)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(26, 16)
        Me.lblCity.TabIndex = 1408
        Me.lblCity.Text = "City"
        '
        'fndCityCode
        '
        Me.fndCityCode.CalculationExpression = Nothing
        Me.fndCityCode.FieldCode = Nothing
        Me.fndCityCode.FieldDesc = Nothing
        Me.fndCityCode.FieldMaxLength = 0
        Me.fndCityCode.FieldName = Nothing
        Me.fndCityCode.isCalculatedField = False
        Me.fndCityCode.IsSourceFromTable = False
        Me.fndCityCode.IsSourceFromValueList = False
        Me.fndCityCode.IsUnique = False
        Me.fndCityCode.Location = New System.Drawing.Point(101, 72)
        Me.fndCityCode.MendatroryField = True
        Me.fndCityCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCityCode.MyLinkLable1 = Me.lblCity
        Me.fndCityCode.MyLinkLable2 = Nothing
        Me.fndCityCode.MyReadOnly = False
        Me.fndCityCode.MyShowMasterFormButton = False
        Me.fndCityCode.Name = "fndCityCode"
        Me.fndCityCode.ReferenceFieldDesc = Nothing
        Me.fndCityCode.ReferenceFieldName = Nothing
        Me.fndCityCode.ReferenceTableName = Nothing
        Me.fndCityCode.Size = New System.Drawing.Size(153, 18)
        Me.fndCityCode.TabIndex = 1407
        Me.fndCityCode.Value = ""
        '
        'txtTransporterName
        '
        Me.txtTransporterName.AutoSize = False
        Me.txtTransporterName.BorderVisible = True
        Me.txtTransporterName.FieldName = Nothing
        Me.txtTransporterName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransporterName.Location = New System.Drawing.Point(263, 138)
        Me.txtTransporterName.Name = "txtTransporterName"
        Me.txtTransporterName.Size = New System.Drawing.Size(471, 18)
        Me.txtTransporterName.TabIndex = 1406
        Me.txtTransporterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtTransporterName.TextWrap = False
        '
        'fndTransporterCode
        '
        Me.fndTransporterCode.CalculationExpression = Nothing
        Me.fndTransporterCode.FieldCode = Nothing
        Me.fndTransporterCode.FieldDesc = Nothing
        Me.fndTransporterCode.FieldMaxLength = 0
        Me.fndTransporterCode.FieldName = Nothing
        Me.fndTransporterCode.isCalculatedField = False
        Me.fndTransporterCode.IsSourceFromTable = False
        Me.fndTransporterCode.IsSourceFromValueList = False
        Me.fndTransporterCode.IsUnique = False
        Me.fndTransporterCode.Location = New System.Drawing.Point(101, 137)
        Me.fndTransporterCode.MendatroryField = True
        Me.fndTransporterCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTransporterCode.MyLinkLable1 = Nothing
        Me.fndTransporterCode.MyLinkLable2 = Nothing
        Me.fndTransporterCode.MyReadOnly = False
        Me.fndTransporterCode.MyShowMasterFormButton = False
        Me.fndTransporterCode.Name = "fndTransporterCode"
        Me.fndTransporterCode.ReferenceFieldDesc = Nothing
        Me.fndTransporterCode.ReferenceFieldName = Nothing
        Me.fndTransporterCode.ReferenceTableName = Nothing
        Me.fndTransporterCode.Size = New System.Drawing.Size(153, 20)
        Me.fndTransporterCode.TabIndex = 1405
        Me.fndTransporterCode.Value = ""
        '
        'txtVehicleName
        '
        Me.txtVehicleName.AutoSize = False
        Me.txtVehicleName.BorderVisible = True
        Me.txtVehicleName.FieldName = Nothing
        Me.txtVehicleName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleName.Location = New System.Drawing.Point(263, 96)
        Me.txtVehicleName.Name = "txtVehicleName"
        Me.txtVehicleName.Size = New System.Drawing.Size(471, 18)
        Me.txtVehicleName.TabIndex = 68
        Me.txtVehicleName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtVehicleName.TextWrap = False
        '
        'txtLocationName
        '
        Me.txtLocationName.AutoSize = False
        Me.txtLocationName.BorderVisible = True
        Me.txtLocationName.FieldName = Nothing
        Me.txtLocationName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationName.Location = New System.Drawing.Point(263, 55)
        Me.txtLocationName.Name = "txtLocationName"
        Me.txtLocationName.Size = New System.Drawing.Size(471, 18)
        Me.txtLocationName.TabIndex = 67
        Me.txtLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtLocationName.TextWrap = False
        '
        'btnGo
        '
        Me.btnGo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnGo.Location = New System.Drawing.Point(755, 161)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(38, 22)
        Me.btnGo.TabIndex = 59
        Me.btnGo.Text = ">>"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(4, 52)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel4.TabIndex = 66
        Me.MyLabel4.Text = "Location"
        '
        'fndLocationCode
        '
        Me.fndLocationCode.CalculationExpression = Nothing
        Me.fndLocationCode.FieldCode = Nothing
        Me.fndLocationCode.FieldDesc = Nothing
        Me.fndLocationCode.FieldMaxLength = 0
        Me.fndLocationCode.FieldName = Nothing
        Me.fndLocationCode.isCalculatedField = False
        Me.fndLocationCode.IsSourceFromTable = False
        Me.fndLocationCode.IsSourceFromValueList = False
        Me.fndLocationCode.IsUnique = False
        Me.fndLocationCode.Location = New System.Drawing.Point(101, 51)
        Me.fndLocationCode.MendatroryField = True
        Me.fndLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocationCode.MyLinkLable1 = Me.MyLabel4
        Me.fndLocationCode.MyLinkLable2 = Nothing
        Me.fndLocationCode.MyReadOnly = False
        Me.fndLocationCode.MyShowMasterFormButton = False
        Me.fndLocationCode.Name = "fndLocationCode"
        Me.fndLocationCode.ReferenceFieldDesc = Nothing
        Me.fndLocationCode.ReferenceFieldName = Nothing
        Me.fndLocationCode.ReferenceTableName = Nothing
        Me.fndLocationCode.Size = New System.Drawing.Size(153, 18)
        Me.fndLocationCode.TabIndex = 64
        Me.fndLocationCode.Value = ""
        '
        'txtComments
        '
        Me.txtComments.CalculationExpression = Nothing
        Me.txtComments.FieldCode = Nothing
        Me.txtComments.FieldDesc = Nothing
        Me.txtComments.FieldMaxLength = 0
        Me.txtComments.FieldName = Nothing
        Me.txtComments.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComments.isCalculatedField = False
        Me.txtComments.IsSourceFromTable = False
        Me.txtComments.IsSourceFromValueList = False
        Me.txtComments.IsUnique = False
        Me.txtComments.Location = New System.Drawing.Point(101, 181)
        Me.txtComments.MaxLength = 200
        Me.txtComments.MendatroryField = False
        Me.txtComments.MyLinkLable1 = Me.MyLabel2
        Me.txtComments.MyLinkLable2 = Nothing
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ReferenceFieldDesc = Nothing
        Me.txtComments.ReferenceFieldName = Nothing
        Me.txtComments.ReferenceTableName = Nothing
        Me.txtComments.Size = New System.Drawing.Size(633, 18)
        Me.txtComments.TabIndex = 6
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(4, 186)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel2.TabIndex = 36
        Me.MyLabel2.Text = "Comments"
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
        Me.txtRemarks.Location = New System.Drawing.Point(101, 161)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.MyLabel3
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(633, 18)
        Me.txtRemarks.TabIndex = 5
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(4, 139)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel1.TabIndex = 63
        Me.MyLabel1.Text = "Transporter"
        '
        'fndVehicleCode
        '
        Me.fndVehicleCode.CalculationExpression = Nothing
        Me.fndVehicleCode.FieldCode = Nothing
        Me.fndVehicleCode.FieldDesc = Nothing
        Me.fndVehicleCode.FieldMaxLength = 0
        Me.fndVehicleCode.FieldName = Nothing
        Me.fndVehicleCode.isCalculatedField = False
        Me.fndVehicleCode.IsSourceFromTable = False
        Me.fndVehicleCode.IsSourceFromValueList = False
        Me.fndVehicleCode.IsUnique = False
        Me.fndVehicleCode.Location = New System.Drawing.Point(101, 94)
        Me.fndVehicleCode.MendatroryField = False
        Me.fndVehicleCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVehicleCode.MyLinkLable1 = Me.lblSalesman
        Me.fndVehicleCode.MyLinkLable2 = Nothing
        Me.fndVehicleCode.MyReadOnly = False
        Me.fndVehicleCode.MyShowMasterFormButton = False
        Me.fndVehicleCode.Name = "fndVehicleCode"
        Me.fndVehicleCode.ReferenceFieldDesc = Nothing
        Me.fndVehicleCode.ReferenceFieldName = Nothing
        Me.fndVehicleCode.ReferenceTableName = Nothing
        Me.fndVehicleCode.Size = New System.Drawing.Size(153, 18)
        Me.fndVehicleCode.TabIndex = 2
        Me.fndVehicleCode.Value = ""
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(101, 4)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblpaymentno
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(238, 20)
        Me.txtCode.TabIndex = 31
        Me.txtCode.Value = ""
        '
        'lblpaymentpostdate
        '
        Me.lblpaymentpostdate.FieldName = Nothing
        Me.lblpaymentpostdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentpostdate.Location = New System.Drawing.Point(363, 6)
        Me.lblpaymentpostdate.Name = "lblpaymentpostdate"
        Me.lblpaymentpostdate.Size = New System.Drawing.Size(86, 16)
        Me.lblpaymentpostdate.TabIndex = 60
        Me.lblpaymentpostdate.Text = "Gate Pass Date"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Gv1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDeleteInvoiceafterPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSelect)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1033, 519)
        Me.SplitContainer1.SplitterDistance = 475
        Me.SplitContainer1.TabIndex = 3
        '
        'btnDeleteInvoiceafterPost
        '
        Me.btnDeleteInvoiceafterPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteInvoiceafterPost.Location = New System.Drawing.Point(364, 9)
        Me.btnDeleteInvoiceafterPost.Name = "btnDeleteInvoiceafterPost"
        Me.btnDeleteInvoiceafterPost.Size = New System.Drawing.Size(122, 24)
        Me.btnDeleteInvoiceafterPost.TabIndex = 57
        Me.btnDeleteInvoiceafterPost.Text = "Reverse And Unpost"
        Me.btnDeleteInvoiceafterPost.Visible = False
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprint.Location = New System.Drawing.Point(294, 9)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(64, 24)
        Me.btnprint.TabIndex = 56
        Me.btnprint.Text = "Print"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(148, 9)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(64, 24)
        Me.btndelete.TabIndex = 55
        Me.btndelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(224, 9)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(64, 24)
        Me.btnPost.TabIndex = 12
        Me.btnPost.Text = "Post"
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(4, 9)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(64, 24)
        Me.btnSelect.TabIndex = 11
        Me.btnSelect.Text = "Select All"
        '
        'RadMenu1
        '
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1033, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'FrmGatePassPS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1033, 539)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmGatePassPS"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "GatePass Entry"
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtManualVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProvAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProvNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumProvisionAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProvisionDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicleCapacity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGrossWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGrossWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCityName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTransporterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicleName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentpostdate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnDeleteInvoiceafterPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents lblSalesman As common.Controls.MyLabel
    Friend WithEvents lblpaymentno As common.Controls.MyLabel
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents fndVehicleCode As common.UserControls.txtFinder
    Friend WithEvents lblpaymentpostdate As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtComments As common.Controls.MyTextBox
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents btnSelect As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents fndLocationCode As common.UserControls.txtFinder
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtVehicleName As common.Controls.MyLabel
    Friend WithEvents txtLocationName As common.Controls.MyLabel
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblFromDate As common.Controls.MyLabel
    Friend WithEvents txtCityName As common.Controls.MyLabel
    Friend WithEvents lblCity As common.Controls.MyLabel
    Friend WithEvents fndCityCode As common.UserControls.txtFinder
    Friend WithEvents txtTransporterName As common.Controls.MyLabel
    Friend WithEvents fndTransporterCode As common.UserControls.txtFinder
    Friend WithEvents lblGrossWeight As common.Controls.MyLabel
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtGrossWeight As common.MyNumBox
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtVehicleCapacity As common.MyNumBox
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents lblProvAmt As common.Controls.MyLabel
    Friend WithEvents lblProvNo As common.Controls.MyLabel
    Friend WithEvents NumProvisionAmount As common.MyNumBox
    Friend WithEvents txtProvisionDocNo As common.Controls.MyTextBox
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDeleteInvoiceafterPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtdate As common.Controls.MyDateTimePicker
    Friend WithEvents txtManualVehicleNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents chkOwnVehicle As System.Windows.Forms.CheckBox
End Class

