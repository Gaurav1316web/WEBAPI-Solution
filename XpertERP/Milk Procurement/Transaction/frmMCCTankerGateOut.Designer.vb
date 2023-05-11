<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMCCTankerGateOut
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
        Me.txtTollAmount = New common.MyNumBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.chkForContractor = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtOpKM = New common.MyNumBox()
        Me.lblOpKM = New common.Controls.MyLabel()
        Me.txtRoute = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.txtDistanceOfRoute = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.dgv = New common.UserControls.MyRadGridView()
        Me.mulMccCode = New common.UserControls.txtMultiSelectFinder()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.lblRemarks = New common.Controls.MyLabel()
        Me.txt_Phone_No = New common.MyNumBox()
        Me.lblPhoneNo = New common.Controls.MyLabel()
        Me.txtDriver = New common.Controls.MyTextBox()
        Me.lblDriver = New common.Controls.MyLabel()
        Me.TxtTankerCapacity = New common.MyNumBox()
        Me.lblToLocationName = New common.Controls.MyLabel()
        Me.txtToLocationCode = New common.UserControls.txtFinder()
        Me.lblToLocation = New common.Controls.MyLabel()
        Me.grpMcc_Plant = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkMCC = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkPlant = New Telerik.WinControls.UI.RadRadioButton()
        Me.lblTankerNo = New common.Controls.MyLabel()
        Me.lblPending = New common.usLock()
        Me.txtTankerNo = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.lblDocNo = New common.Controls.MyLabel()
        Me.fndGateOutNo = New common.UserControls.txtNavigator()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.lblWeighmentNo = New common.Controls.MyLabel()
        Me.txtLocationCode = New common.UserControls.txtFinder()
        Me.txtGateOutDate = New common.Controls.MyDateTimePicker()
        Me.lblMccCode = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtMccCode = New common.UserControls.txtFinder()
        Me.btnUnpost = New Telerik.WinControls.UI.RadButton()
        Me.btn_cancel = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtTollAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkForContractor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOpKM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOpKM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDistanceOfRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Phone_No, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPhoneNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDriver, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDriver, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTankerCapacity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpMcc_Plant, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMcc_Plant.SuspendLayout()
        CType(Me.chkMCC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPlant, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWeighmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGateOutDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMccCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_cancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTollAmount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkForContractor)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtOpKM)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblOpKM)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRoute)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDistanceOfRoute)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgv)
        Me.SplitContainer1.Panel1.Controls.Add(Me.mulMccCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txt_Phone_No)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPhoneNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDriver)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDriver)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtTankerCapacity)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblToLocationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtToLocationCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblToLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.grpMcc_Plant)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTankerNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPending)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTankerNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDocNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndGateOutNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblWeighmentNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocationCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtGateOutDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblMccCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMccCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_cancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2MinSize = 15
        Me.SplitContainer1.Size = New System.Drawing.Size(943, 505)
        Me.SplitContainer1.SplitterDistance = 453
        Me.SplitContainer1.TabIndex = 0
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
        Me.txtTollAmount.Location = New System.Drawing.Point(506, 131)
        Me.txtTollAmount.MendatroryField = False
        Me.txtTollAmount.MyLinkLable1 = Nothing
        Me.txtTollAmount.MyLinkLable2 = Nothing
        Me.txtTollAmount.Name = "txtTollAmount"
        Me.txtTollAmount.ReferenceFieldDesc = Nothing
        Me.txtTollAmount.ReferenceFieldName = Nothing
        Me.txtTollAmount.ReferenceTableName = Nothing
        Me.txtTollAmount.Size = New System.Drawing.Size(101, 20)
        Me.txtTollAmount.TabIndex = 12140
        Me.txtTollAmount.Text = "0"
        Me.txtTollAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTollAmount.Value = 0R
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(423, 133)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel6.TabIndex = 12139
        Me.MyLabel6.Text = "Toll Amount"
        '
        'chkForContractor
        '
        Me.chkForContractor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkForContractor.Location = New System.Drawing.Point(650, 62)
        Me.chkForContractor.Name = "chkForContractor"
        Me.chkForContractor.Size = New System.Drawing.Size(83, 16)
        Me.chkForContractor.TabIndex = 12132
        Me.chkForContractor.Text = "For Contract"
        '
        'txtOpKM
        '
        Me.txtOpKM.BackColor = System.Drawing.Color.White
        Me.txtOpKM.CalculationExpression = Nothing
        Me.txtOpKM.DecimalPlaces = 0
        Me.txtOpKM.FieldCode = Nothing
        Me.txtOpKM.FieldDesc = Nothing
        Me.txtOpKM.FieldMaxLength = 5
        Me.txtOpKM.FieldName = Nothing
        Me.txtOpKM.isCalculatedField = False
        Me.txtOpKM.IsSourceFromTable = False
        Me.txtOpKM.IsSourceFromValueList = False
        Me.txtOpKM.IsUnique = False
        Me.txtOpKM.Location = New System.Drawing.Point(329, 130)
        Me.txtOpKM.MendatroryField = False
        Me.txtOpKM.MyLinkLable1 = Me.lblOpKM
        Me.txtOpKM.MyLinkLable2 = Nothing
        Me.txtOpKM.Name = "txtOpKM"
        Me.txtOpKM.ReferenceFieldDesc = Nothing
        Me.txtOpKM.ReferenceFieldName = Nothing
        Me.txtOpKM.ReferenceTableName = Nothing
        Me.txtOpKM.Size = New System.Drawing.Size(88, 20)
        Me.txtOpKM.TabIndex = 1455
        Me.txtOpKM.Text = "0"
        Me.txtOpKM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtOpKM.Value = 0R
        '
        'lblOpKM
        '
        Me.lblOpKM.FieldName = Nothing
        Me.lblOpKM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOpKM.Location = New System.Drawing.Point(253, 133)
        Me.lblOpKM.Name = "lblOpKM"
        Me.lblOpKM.Size = New System.Drawing.Size(69, 16)
        Me.lblOpKM.TabIndex = 1454
        Me.lblOpKM.Text = "Opening KM"
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
        Me.txtRoute.Location = New System.Drawing.Point(107, 59)
        Me.txtRoute.MendatroryField = True
        Me.txtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoute.MyLinkLable1 = Me.MyLabel3
        Me.txtRoute.MyLinkLable2 = Me.lblLocationName
        Me.txtRoute.MyReadOnly = False
        Me.txtRoute.MyShowMasterFormButton = False
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.ReferenceFieldDesc = Nothing
        Me.txtRoute.ReferenceFieldName = Nothing
        Me.txtRoute.ReferenceTableName = Nothing
        Me.txtRoute.Size = New System.Drawing.Size(143, 21)
        Me.txtRoute.TabIndex = 1453
        Me.txtRoute.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(11, 85)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(78, 18)
        Me.MyLabel3.TabIndex = 9
        Me.MyLabel3.Text = "From Location"
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationName.Location = New System.Drawing.Point(252, 84)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(395, 21)
        Me.lblLocationName.TabIndex = 10
        Me.lblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocationName.TextWrap = False
        '
        'txtDistanceOfRoute
        '
        Me.txtDistanceOfRoute.BackColor = System.Drawing.Color.White
        Me.txtDistanceOfRoute.CalculationExpression = Nothing
        Me.txtDistanceOfRoute.DecimalPlaces = 2
        Me.txtDistanceOfRoute.FieldCode = Nothing
        Me.txtDistanceOfRoute.FieldDesc = Nothing
        Me.txtDistanceOfRoute.FieldMaxLength = 0
        Me.txtDistanceOfRoute.FieldName = Nothing
        Me.txtDistanceOfRoute.isCalculatedField = False
        Me.txtDistanceOfRoute.IsSourceFromTable = False
        Me.txtDistanceOfRoute.IsSourceFromValueList = False
        Me.txtDistanceOfRoute.IsUnique = False
        Me.txtDistanceOfRoute.Location = New System.Drawing.Point(108, 130)
        Me.txtDistanceOfRoute.MendatroryField = False
        Me.txtDistanceOfRoute.MyLinkLable1 = Nothing
        Me.txtDistanceOfRoute.MyLinkLable2 = Nothing
        Me.txtDistanceOfRoute.Name = "txtDistanceOfRoute"
        Me.txtDistanceOfRoute.ReferenceFieldDesc = Nothing
        Me.txtDistanceOfRoute.ReferenceFieldName = Nothing
        Me.txtDistanceOfRoute.ReferenceTableName = Nothing
        Me.txtDistanceOfRoute.Size = New System.Drawing.Size(143, 20)
        Me.txtDistanceOfRoute.TabIndex = 1452
        Me.txtDistanceOfRoute.Text = "0"
        Me.txtDistanceOfRoute.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDistanceOfRoute.Value = 0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(12, 133)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(96, 16)
        Me.MyLabel2.TabIndex = 1451
        Me.MyLabel2.Text = "Distance of Route"
        '
        'dgv
        '
        Me.dgv.BackColor = System.Drawing.Color.Transparent
        Me.dgv.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgv.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgv.ForeColor = System.Drawing.Color.Black
        Me.dgv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgv.Location = New System.Drawing.Point(12, 282)
        '
        'dgv
        '
        Me.dgv.MasterTemplate.AllowAddNewRow = False
        Me.dgv.MasterTemplate.EnableFiltering = True
        Me.dgv.MasterTemplate.ShowHeaderCellButtons = True
        Me.dgv.Name = "dgv"
        Me.dgv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgv.ShowGroupPanel = False
        Me.dgv.ShowHeaderCellButtons = True
        Me.dgv.Size = New System.Drawing.Size(636, 164)
        Me.dgv.TabIndex = 1450
        Me.dgv.TabStop = False
        Me.dgv.Text = "RadGridView1"
        '
        'mulMccCode
        '
        Me.mulMccCode.arrDispalyMember = Nothing
        Me.mulMccCode.arrValueMember = Nothing
        Me.mulMccCode.Location = New System.Drawing.Point(107, 59)
        Me.mulMccCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mulMccCode.MyLinkLable1 = Nothing
        Me.mulMccCode.MyLinkLable2 = Nothing
        Me.mulMccCode.MyNullText = "Please Select"
        Me.mulMccCode.Name = "mulMccCode"
        Me.mulMccCode.Size = New System.Drawing.Size(143, 19)
        Me.mulMccCode.TabIndex = 1449
        '
        'txtRemarks
        '
        Me.txtRemarks.AutoSize = False
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
        Me.txtRemarks.Location = New System.Drawing.Point(108, 202)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.MyLinkLable1 = Nothing
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(540, 66)
        Me.txtRemarks.TabIndex = 1448
        '
        'lblRemarks
        '
        Me.lblRemarks.FieldName = Nothing
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(12, 202)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(51, 16)
        Me.lblRemarks.TabIndex = 1447
        Me.lblRemarks.Text = "Remarks"
        '
        'txt_Phone_No
        '
        Me.txt_Phone_No.BackColor = System.Drawing.Color.White
        Me.txt_Phone_No.CalculationExpression = Nothing
        Me.txt_Phone_No.DecimalPlaces = 2
        Me.txt_Phone_No.FieldCode = Nothing
        Me.txt_Phone_No.FieldDesc = Nothing
        Me.txt_Phone_No.FieldMaxLength = 0
        Me.txt_Phone_No.FieldName = Nothing
        Me.txt_Phone_No.isCalculatedField = False
        Me.txt_Phone_No.IsSourceFromTable = False
        Me.txt_Phone_No.IsSourceFromValueList = False
        Me.txt_Phone_No.IsUnique = False
        Me.txt_Phone_No.Location = New System.Drawing.Point(108, 154)
        Me.txt_Phone_No.MendatroryField = False
        Me.txt_Phone_No.MyLinkLable1 = Nothing
        Me.txt_Phone_No.MyLinkLable2 = Nothing
        Me.txt_Phone_No.Name = "txt_Phone_No"
        Me.txt_Phone_No.ReferenceFieldDesc = Nothing
        Me.txt_Phone_No.ReferenceFieldName = Nothing
        Me.txt_Phone_No.ReferenceTableName = Nothing
        Me.txt_Phone_No.Size = New System.Drawing.Size(143, 20)
        Me.txt_Phone_No.TabIndex = 1446
        Me.txt_Phone_No.Text = "0"
        Me.txt_Phone_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txt_Phone_No.Value = 0R
        '
        'lblPhoneNo
        '
        Me.lblPhoneNo.FieldName = Nothing
        Me.lblPhoneNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhoneNo.Location = New System.Drawing.Point(12, 157)
        Me.lblPhoneNo.Name = "lblPhoneNo"
        Me.lblPhoneNo.Size = New System.Drawing.Size(57, 16)
        Me.lblPhoneNo.TabIndex = 1445
        Me.lblPhoneNo.Text = "Phone No"
        '
        'txtDriver
        '
        Me.txtDriver.CalculationExpression = Nothing
        Me.txtDriver.FieldCode = Nothing
        Me.txtDriver.FieldDesc = Nothing
        Me.txtDriver.FieldMaxLength = 0
        Me.txtDriver.FieldName = Nothing
        Me.txtDriver.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDriver.isCalculatedField = False
        Me.txtDriver.IsSourceFromTable = False
        Me.txtDriver.IsSourceFromValueList = False
        Me.txtDriver.IsUnique = False
        Me.txtDriver.Location = New System.Drawing.Point(108, 179)
        Me.txtDriver.MaxLength = 30
        Me.txtDriver.MendatroryField = False
        Me.txtDriver.MyLinkLable1 = Nothing
        Me.txtDriver.MyLinkLable2 = Nothing
        Me.txtDriver.Name = "txtDriver"
        Me.txtDriver.ReferenceFieldDesc = Nothing
        Me.txtDriver.ReferenceFieldName = Nothing
        Me.txtDriver.ReferenceTableName = Nothing
        Me.txtDriver.Size = New System.Drawing.Size(540, 18)
        Me.txtDriver.TabIndex = 1444
        '
        'lblDriver
        '
        Me.lblDriver.FieldName = Nothing
        Me.lblDriver.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDriver.Location = New System.Drawing.Point(12, 179)
        Me.lblDriver.Name = "lblDriver"
        Me.lblDriver.Size = New System.Drawing.Size(36, 16)
        Me.lblDriver.TabIndex = 363
        Me.lblDriver.Text = "Driver"
        '
        'TxtTankerCapacity
        '
        Me.TxtTankerCapacity.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtTankerCapacity.CalculationExpression = Nothing
        Me.TxtTankerCapacity.DecimalPlaces = 2
        Me.TxtTankerCapacity.FieldCode = Nothing
        Me.TxtTankerCapacity.FieldDesc = Nothing
        Me.TxtTankerCapacity.FieldMaxLength = 0
        Me.TxtTankerCapacity.FieldName = Nothing
        Me.TxtTankerCapacity.isCalculatedField = False
        Me.TxtTankerCapacity.IsSourceFromTable = False
        Me.TxtTankerCapacity.IsSourceFromValueList = False
        Me.TxtTankerCapacity.IsUnique = False
        Me.TxtTankerCapacity.Location = New System.Drawing.Point(645, 35)
        Me.TxtTankerCapacity.MendatroryField = True
        Me.TxtTankerCapacity.MyLinkLable1 = Nothing
        Me.TxtTankerCapacity.MyLinkLable2 = Nothing
        Me.TxtTankerCapacity.Name = "TxtTankerCapacity"
        Me.TxtTankerCapacity.ReferenceFieldDesc = Nothing
        Me.TxtTankerCapacity.ReferenceFieldName = Nothing
        Me.TxtTankerCapacity.ReferenceTableName = Nothing
        Me.TxtTankerCapacity.Size = New System.Drawing.Size(137, 20)
        Me.TxtTankerCapacity.TabIndex = 310
        Me.TxtTankerCapacity.Text = "0"
        Me.TxtTankerCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtTankerCapacity.Value = 0R
        '
        'lblToLocationName
        '
        Me.lblToLocationName.AutoSize = False
        Me.lblToLocationName.BorderVisible = True
        Me.lblToLocationName.FieldName = Nothing
        Me.lblToLocationName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToLocationName.Location = New System.Drawing.Point(252, 106)
        Me.lblToLocationName.Name = "lblToLocationName"
        Me.lblToLocationName.Size = New System.Drawing.Size(395, 21)
        Me.lblToLocationName.TabIndex = 309
        Me.lblToLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.txtToLocationCode.Location = New System.Drawing.Point(107, 106)
        Me.txtToLocationCode.MendatroryField = True
        Me.txtToLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToLocationCode.MyLinkLable1 = Me.MyLabel3
        Me.txtToLocationCode.MyLinkLable2 = Me.lblLocationName
        Me.txtToLocationCode.MyReadOnly = False
        Me.txtToLocationCode.MyShowMasterFormButton = False
        Me.txtToLocationCode.Name = "txtToLocationCode"
        Me.txtToLocationCode.ReferenceFieldDesc = Nothing
        Me.txtToLocationCode.ReferenceFieldName = Nothing
        Me.txtToLocationCode.ReferenceTableName = Nothing
        Me.txtToLocationCode.Size = New System.Drawing.Size(144, 21)
        Me.txtToLocationCode.TabIndex = 214
        Me.txtToLocationCode.Value = ""
        '
        'lblToLocation
        '
        Me.lblToLocation.FieldName = Nothing
        Me.lblToLocation.Location = New System.Drawing.Point(12, 109)
        Me.lblToLocation.Name = "lblToLocation"
        Me.lblToLocation.Size = New System.Drawing.Size(64, 18)
        Me.lblToLocation.TabIndex = 213
        Me.lblToLocation.Text = "To Location"
        '
        'grpMcc_Plant
        '
        Me.grpMcc_Plant.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpMcc_Plant.Controls.Add(Me.chkMCC)
        Me.grpMcc_Plant.Controls.Add(Me.chkPlant)
        Me.grpMcc_Plant.HeaderText = ""
        Me.grpMcc_Plant.Location = New System.Drawing.Point(645, 5)
        Me.grpMcc_Plant.Name = "grpMcc_Plant"
        Me.grpMcc_Plant.Size = New System.Drawing.Size(123, 28)
        Me.grpMcc_Plant.TabIndex = 212
        '
        'chkMCC
        '
        Me.chkMCC.Location = New System.Drawing.Point(5, 5)
        Me.chkMCC.Name = "chkMCC"
        Me.chkMCC.Size = New System.Drawing.Size(44, 18)
        Me.chkMCC.TabIndex = 0
        Me.chkMCC.Text = "MCC"
        '
        'chkPlant
        '
        Me.chkPlant.Location = New System.Drawing.Point(61, 5)
        Me.chkPlant.Name = "chkPlant"
        Me.chkPlant.Size = New System.Drawing.Size(45, 18)
        Me.chkPlant.TabIndex = 1
        Me.chkPlant.Text = "Plant"
        '
        'lblTankerNo
        '
        Me.lblTankerNo.AutoSize = False
        Me.lblTankerNo.BorderVisible = True
        Me.lblTankerNo.FieldName = Nothing
        Me.lblTankerNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTankerNo.Location = New System.Drawing.Point(252, 34)
        Me.lblTankerNo.Name = "lblTankerNo"
        Me.lblTankerNo.Size = New System.Drawing.Size(395, 21)
        Me.lblTankerNo.TabIndex = 211
        Me.lblTankerNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTankerNo.TextWrap = False
        '
        'lblPending
        '
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(825, 9)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(105, 21)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 210
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
        Me.txtTankerNo.Location = New System.Drawing.Point(107, 34)
        Me.txtTankerNo.MendatroryField = True
        Me.txtTankerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTankerNo.MyLinkLable1 = Me.MyLabel4
        Me.txtTankerNo.MyLinkLable2 = Me.lblTankerNo
        Me.txtTankerNo.MyReadOnly = False
        Me.txtTankerNo.MyShowMasterFormButton = False
        Me.txtTankerNo.Name = "txtTankerNo"
        Me.txtTankerNo.ReferenceFieldDesc = Nothing
        Me.txtTankerNo.ReferenceFieldName = Nothing
        Me.txtTankerNo.ReferenceTableName = Nothing
        Me.txtTankerNo.Size = New System.Drawing.Size(143, 21)
        Me.txtTankerNo.TabIndex = 3
        Me.txtTankerNo.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(11, 35)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel4.TabIndex = 7
        Me.MyLabel4.Text = "Tanker No"
        '
        'lblDocNo
        '
        Me.lblDocNo.FieldName = Nothing
        Me.lblDocNo.Location = New System.Drawing.Point(11, 10)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(70, 18)
        Me.lblDocNo.TabIndex = 6
        Me.lblDocNo.Text = "Gate Out No"
        '
        'fndGateOutNo
        '
        Me.fndGateOutNo.FieldName = Nothing
        Me.fndGateOutNo.Location = New System.Drawing.Point(107, 9)
        Me.fndGateOutNo.MendatroryField = False
        Me.fndGateOutNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndGateOutNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndGateOutNo.MyLinkLable1 = Me.lblDocNo
        Me.fndGateOutNo.MyLinkLable2 = Nothing
        Me.fndGateOutNo.MyMaxLength = 32767
        Me.fndGateOutNo.MyReadOnly = False
        Me.fndGateOutNo.Name = "fndGateOutNo"
        Me.fndGateOutNo.Size = New System.Drawing.Size(244, 21)
        Me.fndGateOutNo.TabIndex = 0
        Me.fndGateOutNo.Value = ""
        '
        'btnReset
        '
        Me.btnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(350, 9)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(18, 21)
        Me.btnReset.TabIndex = 1
        '
        'lblWeighmentNo
        '
        Me.lblWeighmentNo.FieldName = Nothing
        Me.lblWeighmentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeighmentNo.Location = New System.Drawing.Point(375, 11)
        Me.lblWeighmentNo.Name = "lblWeighmentNo"
        Me.lblWeighmentNo.Size = New System.Drawing.Size(82, 16)
        Me.lblWeighmentNo.TabIndex = 13
        Me.lblWeighmentNo.Text = "Gate Out Date "
        '
        'txtLocationCode
        '
        Me.txtLocationCode.CalculationExpression = Nothing
        Me.txtLocationCode.FieldCode = Nothing
        Me.txtLocationCode.FieldDesc = Nothing
        Me.txtLocationCode.FieldMaxLength = 0
        Me.txtLocationCode.FieldName = Nothing
        Me.txtLocationCode.isCalculatedField = False
        Me.txtLocationCode.IsSourceFromTable = False
        Me.txtLocationCode.IsSourceFromValueList = False
        Me.txtLocationCode.IsUnique = False
        Me.txtLocationCode.Location = New System.Drawing.Point(107, 84)
        Me.txtLocationCode.MendatroryField = True
        Me.txtLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationCode.MyLinkLable1 = Me.MyLabel3
        Me.txtLocationCode.MyLinkLable2 = Me.lblLocationName
        Me.txtLocationCode.MyReadOnly = False
        Me.txtLocationCode.MyShowMasterFormButton = False
        Me.txtLocationCode.Name = "txtLocationCode"
        Me.txtLocationCode.ReferenceFieldDesc = Nothing
        Me.txtLocationCode.ReferenceFieldName = Nothing
        Me.txtLocationCode.ReferenceTableName = Nothing
        Me.txtLocationCode.Size = New System.Drawing.Size(143, 21)
        Me.txtLocationCode.TabIndex = 5
        Me.txtLocationCode.Value = ""
        '
        'txtGateOutDate
        '
        Me.txtGateOutDate.CalculationExpression = Nothing
        Me.txtGateOutDate.CustomFormat = "dd/MM/yyyy  hh:mm:ss tt"
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
        Me.txtGateOutDate.Location = New System.Drawing.Point(457, 10)
        Me.txtGateOutDate.MendatroryField = False
        Me.txtGateOutDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtGateOutDate.MyLinkLable1 = Nothing
        Me.txtGateOutDate.MyLinkLable2 = Nothing
        Me.txtGateOutDate.Name = "txtGateOutDate"
        Me.txtGateOutDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtGateOutDate.ReferenceFieldDesc = Nothing
        Me.txtGateOutDate.ReferenceFieldName = Nothing
        Me.txtGateOutDate.ReferenceTableName = Nothing
        Me.txtGateOutDate.Size = New System.Drawing.Size(190, 18)
        Me.txtGateOutDate.TabIndex = 2
        Me.txtGateOutDate.TabStop = False
        Me.txtGateOutDate.Text = "13/06/2011  11:29:49 AM"
        Me.txtGateOutDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblMccCode
        '
        Me.lblMccCode.AutoSize = False
        Me.lblMccCode.BorderVisible = True
        Me.lblMccCode.FieldName = Nothing
        Me.lblMccCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMccCode.Location = New System.Drawing.Point(252, 59)
        Me.lblMccCode.Name = "lblMccCode"
        Me.lblMccCode.Size = New System.Drawing.Size(395, 21)
        Me.lblMccCode.TabIndex = 11
        Me.lblMccCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMccCode.TextWrap = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(11, 60)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(60, 18)
        Me.MyLabel1.TabIndex = 8
        Me.MyLabel1.Text = "MCC Code"
        '
        'txtMccCode
        '
        Me.txtMccCode.CalculationExpression = Nothing
        Me.txtMccCode.FieldCode = Nothing
        Me.txtMccCode.FieldDesc = Nothing
        Me.txtMccCode.FieldMaxLength = 0
        Me.txtMccCode.FieldName = Nothing
        Me.txtMccCode.isCalculatedField = False
        Me.txtMccCode.IsSourceFromTable = False
        Me.txtMccCode.IsSourceFromValueList = False
        Me.txtMccCode.IsUnique = False
        Me.txtMccCode.Location = New System.Drawing.Point(704, 133)
        Me.txtMccCode.MendatroryField = True
        Me.txtMccCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMccCode.MyLinkLable1 = Me.MyLabel1
        Me.txtMccCode.MyLinkLable2 = Me.lblMccCode
        Me.txtMccCode.MyReadOnly = False
        Me.txtMccCode.MyShowMasterFormButton = False
        Me.txtMccCode.Name = "txtMccCode"
        Me.txtMccCode.ReferenceFieldDesc = Nothing
        Me.txtMccCode.ReferenceFieldName = Nothing
        Me.txtMccCode.ReferenceTableName = Nothing
        Me.txtMccCode.Size = New System.Drawing.Size(143, 21)
        Me.txtMccCode.TabIndex = 4
        Me.txtMccCode.Value = ""
        Me.txtMccCode.Visible = False
        '
        'btnUnpost
        '
        Me.btnUnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUnpost.Location = New System.Drawing.Point(347, 9)
        Me.btnUnpost.Name = "btnUnpost"
        Me.btnUnpost.Size = New System.Drawing.Size(69, 18)
        Me.btnUnpost.TabIndex = 14
        Me.btnUnpost.Text = "Reverse"
        '
        'btn_cancel
        '
        Me.btn_cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_cancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_cancel.Location = New System.Drawing.Point(277, 9)
        Me.btn_cancel.Name = "btn_cancel"
        Me.btn_cancel.Size = New System.Drawing.Size(69, 18)
        Me.btn_cancel.TabIndex = 13
        Me.btn_cancel.Text = "Cancel"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(208, 9)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 7
        Me.btnPrint.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(872, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 18)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(70, 9)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 18)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(2, 9)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(139, 9)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'FrmMCCTankerGateOut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(943, 505)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmMCCTankerGateOut"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "MCC Tanker Gate Out"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtTollAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkForContractor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOpKM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOpKM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDistanceOfRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Phone_No, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPhoneNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDriver, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDriver, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTankerCapacity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpMcc_Plant, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMcc_Plant.ResumeLayout(False)
        Me.grpMcc_Plant.PerformLayout()
        CType(Me.chkMCC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPlant, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWeighmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGateOutDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMccCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_cancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndGateOutNo As common.UserControls.txtNavigator
    Friend WithEvents lblDocNo As common.Controls.MyLabel
    Friend WithEvents txtGateOutDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblWeighmentNo As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents txtLocationCode As common.UserControls.txtFinder
    Friend WithEvents lblMccCode As common.Controls.MyLabel
    Friend WithEvents txtMccCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtTankerNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents lblTankerNo As common.Controls.MyLabel
    Friend WithEvents grpMcc_Plant As RadGroupBox
    Friend WithEvents chkMCC As RadRadioButton
    Friend WithEvents chkPlant As RadRadioButton
    Friend WithEvents txtToLocationCode As common.UserControls.txtFinder
    Friend WithEvents lblToLocation As common.Controls.MyLabel
    Friend WithEvents lblToLocationName As common.Controls.MyLabel
    Friend WithEvents TxtTankerCapacity As common.MyNumBox
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents lblDriver As common.Controls.MyLabel
    Friend WithEvents txtDriver As common.Controls.MyTextBox
    Friend WithEvents lblPhoneNo As common.Controls.MyLabel
    Friend WithEvents txt_Phone_No As common.MyNumBox
    Friend WithEvents lblRemarks As common.Controls.MyLabel
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents btn_cancel As RadButton
    Friend WithEvents mulMccCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents dgv As common.UserControls.MyRadGridView
    Friend WithEvents btnUnpost As RadButton
    Friend WithEvents txtDistanceOfRoute As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtRoute As common.UserControls.txtFinder
    Friend WithEvents lblOpKM As common.Controls.MyLabel
    Friend WithEvents txtOpKM As common.MyNumBox
    Friend WithEvents chkForContractor As RadCheckBox
    Friend WithEvents txtTollAmount As common.MyNumBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
End Class

