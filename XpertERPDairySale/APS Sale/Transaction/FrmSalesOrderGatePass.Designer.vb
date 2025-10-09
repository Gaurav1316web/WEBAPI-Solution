<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSalesOrderGatePass
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.txtDriverMobNo = New common.Controls.MyTextBox()
        Me.lblDriverMobno = New common.Controls.MyLabel()
        Me.txtDriverName = New common.Controls.MyTextBox()
        Me.lblDriveName = New common.Controls.MyLabel()
        Me.txtRemark = New common.Controls.MyTextBox()
        Me.lblRemark = New common.Controls.MyLabel()
        Me.lblTransporterName = New common.Controls.MyTextBox()
        Me.lblTranspoter = New common.Controls.MyLabel()
        Me.txtTransporterCode = New common.UserControls.txtFinder()
        Me.lblVehicleNo = New common.Controls.MyTextBox()
        Me.txtVehicleCode = New common.UserControls.txtFinder()
        Me.lblVehicleCode = New common.Controls.MyLabel()
        Me.lblDispatchCode = New common.Controls.MyLabel()
        Me.lblSubLocationDesc = New common.Controls.MyLabel()
        Me.lblSubLocaiton = New common.Controls.MyLabel()
        Me.txtDispatchCode = New common.UserControls.txtFinder()
        Me.lblDispatch = New common.Controls.MyLabel()
        Me.txtSubLocation = New common.UserControls.txtFinder()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtLoadingSlip = New common.Controls.MyTextBox()
        Me.lblLoadingSlip = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblpaymentpostdate = New common.Controls.MyLabel()
        Me.lblGPCode = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtDocCode = New common.UserControls.txtNavigator()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnGPCancel = New Telerik.WinControls.UI.RadButton()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDriverMobNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDriverMobno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDriverName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDriveName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTranspoter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDispatchCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubLocaiton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDispatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLoadingSlip, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLoadingSlip, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentpostdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGPCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGPCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(800, 20)
        Me.RadMenu1.TabIndex = 1
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGPCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 430)
        Me.SplitContainer1.SplitterDistance = 394
        Me.SplitContainer1.TabIndex = 2
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnGo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDriverMobNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDriverMobno)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDriverName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDriveName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRemark)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRemark)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblTransporterName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblTranspoter)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtTransporterCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblVehicleNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtVehicleCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblVehicleCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDispatchCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblSubLocationDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblSubLocaiton)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDispatchCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtSubLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDispatch)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocationDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtLoadingSlip)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLoadingSlip)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblGPCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDocCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblpaymentpostdate)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(800, 394)
        Me.SplitContainer2.SplitterDistance = 174
        Me.SplitContainer2.TabIndex = 0
        '
        'btnGo
        '
        Me.btnGo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnGo.Location = New System.Drawing.Point(464, 147)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(91, 18)
        Me.btnGo.TabIndex = 1640
        Me.btnGo.Text = ">>"
        '
        'txtDriverMobNo
        '
        Me.txtDriverMobNo.CalculationExpression = Nothing
        Me.txtDriverMobNo.FieldCode = Nothing
        Me.txtDriverMobNo.FieldDesc = Nothing
        Me.txtDriverMobNo.FieldMaxLength = 0
        Me.txtDriverMobNo.FieldName = Nothing
        Me.txtDriverMobNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDriverMobNo.isCalculatedField = False
        Me.txtDriverMobNo.IsSourceFromTable = False
        Me.txtDriverMobNo.IsSourceFromValueList = False
        Me.txtDriverMobNo.IsUnique = False
        Me.txtDriverMobNo.Location = New System.Drawing.Point(520, 70)
        Me.txtDriverMobNo.MaxLength = 15
        Me.txtDriverMobNo.MendatroryField = False
        Me.txtDriverMobNo.MyLinkLable1 = Me.lblDriverMobno
        Me.txtDriverMobNo.MyLinkLable2 = Nothing
        Me.txtDriverMobNo.Name = "txtDriverMobNo"
        Me.txtDriverMobNo.ReferenceFieldDesc = Nothing
        Me.txtDriverMobNo.ReferenceFieldName = Nothing
        Me.txtDriverMobNo.ReferenceTableName = Nothing
        Me.txtDriverMobNo.Size = New System.Drawing.Size(182, 18)
        Me.txtDriverMobNo.TabIndex = 1638
        '
        'lblDriverMobno
        '
        Me.lblDriverMobno.FieldName = Nothing
        Me.lblDriverMobno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDriverMobno.Location = New System.Drawing.Point(439, 71)
        Me.lblDriverMobno.Name = "lblDriverMobno"
        Me.lblDriverMobno.Size = New System.Drawing.Size(82, 16)
        Me.lblDriverMobno.TabIndex = 1639
        Me.lblDriverMobno.Text = "Driver Mob No."
        '
        'txtDriverName
        '
        Me.txtDriverName.CalculationExpression = Nothing
        Me.txtDriverName.FieldCode = Nothing
        Me.txtDriverName.FieldDesc = Nothing
        Me.txtDriverName.FieldMaxLength = 0
        Me.txtDriverName.FieldName = Nothing
        Me.txtDriverName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDriverName.isCalculatedField = False
        Me.txtDriverName.IsSourceFromTable = False
        Me.txtDriverName.IsSourceFromValueList = False
        Me.txtDriverName.IsUnique = False
        Me.txtDriverName.Location = New System.Drawing.Point(520, 51)
        Me.txtDriverName.MaxLength = 200
        Me.txtDriverName.MendatroryField = False
        Me.txtDriverName.MyLinkLable1 = Me.lblDriveName
        Me.txtDriverName.MyLinkLable2 = Nothing
        Me.txtDriverName.Name = "txtDriverName"
        Me.txtDriverName.ReferenceFieldDesc = Nothing
        Me.txtDriverName.ReferenceFieldName = Nothing
        Me.txtDriverName.ReferenceTableName = Nothing
        Me.txtDriverName.Size = New System.Drawing.Size(182, 18)
        Me.txtDriverName.TabIndex = 1636
        '
        'lblDriveName
        '
        Me.lblDriveName.FieldName = Nothing
        Me.lblDriveName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDriveName.Location = New System.Drawing.Point(439, 52)
        Me.lblDriveName.Name = "lblDriveName"
        Me.lblDriveName.Size = New System.Drawing.Size(70, 16)
        Me.lblDriveName.TabIndex = 1637
        Me.lblDriveName.Text = "Driver Name"
        '
        'txtRemark
        '
        Me.txtRemark.CalculationExpression = Nothing
        Me.txtRemark.FieldCode = Nothing
        Me.txtRemark.FieldDesc = Nothing
        Me.txtRemark.FieldMaxLength = 0
        Me.txtRemark.FieldName = Nothing
        Me.txtRemark.isCalculatedField = False
        Me.txtRemark.IsSourceFromTable = False
        Me.txtRemark.IsSourceFromValueList = False
        Me.txtRemark.IsUnique = False
        Me.txtRemark.Location = New System.Drawing.Point(84, 144)
        Me.txtRemark.MendatroryField = False
        Me.txtRemark.MyLinkLable1 = Me.lblRemark
        Me.txtRemark.MyLinkLable2 = Nothing
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ReferenceFieldDesc = Nothing
        Me.txtRemark.ReferenceFieldName = Nothing
        Me.txtRemark.ReferenceTableName = Nothing
        Me.txtRemark.Size = New System.Drawing.Size(349, 20)
        Me.txtRemark.TabIndex = 1635
        '
        'lblRemark
        '
        Me.lblRemark.FieldName = Nothing
        Me.lblRemark.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemark.Location = New System.Drawing.Point(4, 147)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(46, 16)
        Me.lblRemark.TabIndex = 1634
        Me.lblRemark.Text = "Remark"
        '
        'lblTransporterName
        '
        Me.lblTransporterName.CalculationExpression = Nothing
        Me.lblTransporterName.Enabled = False
        Me.lblTransporterName.FieldCode = Nothing
        Me.lblTransporterName.FieldDesc = Nothing
        Me.lblTransporterName.FieldMaxLength = 0
        Me.lblTransporterName.FieldName = Nothing
        Me.lblTransporterName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransporterName.isCalculatedField = False
        Me.lblTransporterName.IsSourceFromTable = False
        Me.lblTransporterName.IsSourceFromValueList = False
        Me.lblTransporterName.IsUnique = False
        Me.lblTransporterName.Location = New System.Drawing.Point(270, 100)
        Me.lblTransporterName.MaxLength = 200
        Me.lblTransporterName.MendatroryField = False
        Me.lblTransporterName.MyLinkLable1 = Nothing
        Me.lblTransporterName.MyLinkLable2 = Nothing
        Me.lblTransporterName.Name = "lblTransporterName"
        Me.lblTransporterName.ReferenceFieldDesc = Nothing
        Me.lblTransporterName.ReferenceFieldName = Nothing
        Me.lblTransporterName.ReferenceTableName = Nothing
        Me.lblTransporterName.Size = New System.Drawing.Size(162, 18)
        Me.lblTransporterName.TabIndex = 1633
        '
        'lblTranspoter
        '
        Me.lblTranspoter.FieldName = Nothing
        Me.lblTranspoter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTranspoter.Location = New System.Drawing.Point(4, 101)
        Me.lblTranspoter.Name = "lblTranspoter"
        Me.lblTranspoter.Size = New System.Drawing.Size(65, 16)
        Me.lblTranspoter.TabIndex = 1632
        Me.lblTranspoter.Text = "Transporter"
        '
        'txtTransporterCode
        '
        Me.txtTransporterCode.CalculationExpression = Nothing
        Me.txtTransporterCode.FieldCode = Nothing
        Me.txtTransporterCode.FieldDesc = Nothing
        Me.txtTransporterCode.FieldMaxLength = 0
        Me.txtTransporterCode.FieldName = Nothing
        Me.txtTransporterCode.isCalculatedField = False
        Me.txtTransporterCode.IsSourceFromTable = False
        Me.txtTransporterCode.IsSourceFromValueList = False
        Me.txtTransporterCode.IsUnique = False
        Me.txtTransporterCode.Location = New System.Drawing.Point(84, 99)
        Me.txtTransporterCode.MendatroryField = False
        Me.txtTransporterCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransporterCode.MyLinkLable1 = Me.lblTranspoter
        Me.txtTransporterCode.MyLinkLable2 = Nothing
        Me.txtTransporterCode.MyReadOnly = False
        Me.txtTransporterCode.MyShowMasterFormButton = False
        Me.txtTransporterCode.Name = "txtTransporterCode"
        Me.txtTransporterCode.ReferenceFieldDesc = Nothing
        Me.txtTransporterCode.ReferenceFieldName = Nothing
        Me.txtTransporterCode.ReferenceTableName = Nothing
        Me.txtTransporterCode.Size = New System.Drawing.Size(180, 21)
        Me.txtTransporterCode.TabIndex = 1631
        Me.txtTransporterCode.Value = ""
        '
        'lblVehicleNo
        '
        Me.lblVehicleNo.CalculationExpression = Nothing
        Me.lblVehicleNo.FieldCode = Nothing
        Me.lblVehicleNo.FieldDesc = Nothing
        Me.lblVehicleNo.FieldMaxLength = 0
        Me.lblVehicleNo.FieldName = Nothing
        Me.lblVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicleNo.isCalculatedField = False
        Me.lblVehicleNo.IsSourceFromTable = False
        Me.lblVehicleNo.IsSourceFromValueList = False
        Me.lblVehicleNo.IsUnique = False
        Me.lblVehicleNo.Location = New System.Drawing.Point(270, 124)
        Me.lblVehicleNo.MaxLength = 200
        Me.lblVehicleNo.MendatroryField = False
        Me.lblVehicleNo.MyLinkLable1 = Nothing
        Me.lblVehicleNo.MyLinkLable2 = Nothing
        Me.lblVehicleNo.Name = "lblVehicleNo"
        Me.lblVehicleNo.ReferenceFieldDesc = Nothing
        Me.lblVehicleNo.ReferenceFieldName = Nothing
        Me.lblVehicleNo.ReferenceTableName = Nothing
        Me.lblVehicleNo.Size = New System.Drawing.Size(162, 18)
        Me.lblVehicleNo.TabIndex = 1630
        '
        'txtVehicleCode
        '
        Me.txtVehicleCode.CalculationExpression = Nothing
        Me.txtVehicleCode.FieldCode = Nothing
        Me.txtVehicleCode.FieldDesc = Nothing
        Me.txtVehicleCode.FieldMaxLength = 0
        Me.txtVehicleCode.FieldName = Nothing
        Me.txtVehicleCode.isCalculatedField = False
        Me.txtVehicleCode.IsSourceFromTable = False
        Me.txtVehicleCode.IsSourceFromValueList = False
        Me.txtVehicleCode.IsUnique = False
        Me.txtVehicleCode.Location = New System.Drawing.Point(84, 123)
        Me.txtVehicleCode.MendatroryField = False
        Me.txtVehicleCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleCode.MyLinkLable1 = Me.lblVehicleCode
        Me.txtVehicleCode.MyLinkLable2 = Nothing
        Me.txtVehicleCode.MyReadOnly = False
        Me.txtVehicleCode.MyShowMasterFormButton = False
        Me.txtVehicleCode.Name = "txtVehicleCode"
        Me.txtVehicleCode.ReferenceFieldDesc = Nothing
        Me.txtVehicleCode.ReferenceFieldName = Nothing
        Me.txtVehicleCode.ReferenceTableName = Nothing
        Me.txtVehicleCode.Size = New System.Drawing.Size(180, 21)
        Me.txtVehicleCode.TabIndex = 1628
        Me.txtVehicleCode.Value = ""
        '
        'lblVehicleCode
        '
        Me.lblVehicleCode.FieldName = Nothing
        Me.lblVehicleCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicleCode.Location = New System.Drawing.Point(4, 125)
        Me.lblVehicleCode.Name = "lblVehicleCode"
        Me.lblVehicleCode.Size = New System.Drawing.Size(74, 16)
        Me.lblVehicleCode.TabIndex = 1629
        Me.lblVehicleCode.Text = "Vehicle Code"
        '
        'lblDispatchCode
        '
        Me.lblDispatchCode.AutoSize = False
        Me.lblDispatchCode.BorderVisible = True
        Me.lblDispatchCode.FieldName = Nothing
        Me.lblDispatchCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDispatchCode.Location = New System.Drawing.Point(271, 28)
        Me.lblDispatchCode.Name = "lblDispatchCode"
        Me.lblDispatchCode.Size = New System.Drawing.Size(162, 20)
        Me.lblDispatchCode.TabIndex = 1616
        Me.lblDispatchCode.TextWrap = False
        '
        'lblSubLocationDesc
        '
        Me.lblSubLocationDesc.AutoSize = False
        Me.lblSubLocationDesc.BorderVisible = True
        Me.lblSubLocationDesc.FieldName = Nothing
        Me.lblSubLocationDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubLocationDesc.Location = New System.Drawing.Point(271, 77)
        Me.lblSubLocationDesc.Name = "lblSubLocationDesc"
        Me.lblSubLocationDesc.Size = New System.Drawing.Size(162, 20)
        Me.lblSubLocationDesc.TabIndex = 1621
        Me.lblSubLocationDesc.TextWrap = False
        '
        'lblSubLocaiton
        '
        Me.lblSubLocaiton.FieldName = Nothing
        Me.lblSubLocaiton.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubLocaiton.Location = New System.Drawing.Point(4, 79)
        Me.lblSubLocaiton.Name = "lblSubLocaiton"
        Me.lblSubLocaiton.Size = New System.Drawing.Size(72, 16)
        Me.lblSubLocaiton.TabIndex = 1622
        Me.lblSubLocaiton.Text = "Sub Location"
        '
        'txtDispatchCode
        '
        Me.txtDispatchCode.CalculationExpression = Nothing
        Me.txtDispatchCode.FieldCode = Nothing
        Me.txtDispatchCode.FieldDesc = Nothing
        Me.txtDispatchCode.FieldMaxLength = 0
        Me.txtDispatchCode.FieldName = Nothing
        Me.txtDispatchCode.isCalculatedField = False
        Me.txtDispatchCode.IsSourceFromTable = False
        Me.txtDispatchCode.IsSourceFromValueList = False
        Me.txtDispatchCode.IsUnique = False
        Me.txtDispatchCode.Location = New System.Drawing.Point(85, 28)
        Me.txtDispatchCode.MendatroryField = False
        Me.txtDispatchCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDispatchCode.MyLinkLable1 = Me.lblDispatch
        Me.txtDispatchCode.MyLinkLable2 = Me.lblDispatchCode
        Me.txtDispatchCode.MyReadOnly = False
        Me.txtDispatchCode.MyShowMasterFormButton = False
        Me.txtDispatchCode.Name = "txtDispatchCode"
        Me.txtDispatchCode.ReferenceFieldDesc = Nothing
        Me.txtDispatchCode.ReferenceFieldName = Nothing
        Me.txtDispatchCode.ReferenceTableName = Nothing
        Me.txtDispatchCode.Size = New System.Drawing.Size(180, 21)
        Me.txtDispatchCode.TabIndex = 1614
        Me.txtDispatchCode.Value = ""
        '
        'lblDispatch
        '
        Me.lblDispatch.FieldName = Nothing
        Me.lblDispatch.Location = New System.Drawing.Point(4, 29)
        Me.lblDispatch.Name = "lblDispatch"
        Me.lblDispatch.Size = New System.Drawing.Size(79, 18)
        Me.lblDispatch.TabIndex = 1615
        Me.lblDispatch.Text = "Dispatch Code"
        '
        'txtSubLocation
        '
        Me.txtSubLocation.CalculationExpression = Nothing
        Me.txtSubLocation.FieldCode = Nothing
        Me.txtSubLocation.FieldDesc = Nothing
        Me.txtSubLocation.FieldMaxLength = 0
        Me.txtSubLocation.FieldName = Nothing
        Me.txtSubLocation.isCalculatedField = False
        Me.txtSubLocation.IsSourceFromTable = False
        Me.txtSubLocation.IsSourceFromValueList = False
        Me.txtSubLocation.IsUnique = False
        Me.txtSubLocation.Location = New System.Drawing.Point(85, 77)
        Me.txtSubLocation.MendatroryField = False
        Me.txtSubLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubLocation.MyLinkLable1 = Me.lblSubLocaiton
        Me.txtSubLocation.MyLinkLable2 = Me.lblSubLocationDesc
        Me.txtSubLocation.MyReadOnly = False
        Me.txtSubLocation.MyShowMasterFormButton = False
        Me.txtSubLocation.Name = "txtSubLocation"
        Me.txtSubLocation.ReferenceFieldDesc = Nothing
        Me.txtSubLocation.ReferenceFieldName = Nothing
        Me.txtSubLocation.ReferenceTableName = Nothing
        Me.txtSubLocation.Size = New System.Drawing.Size(180, 21)
        Me.txtSubLocation.TabIndex = 1620
        Me.txtSubLocation.Value = ""
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.FieldName = Nothing
        Me.lblLocationDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationDesc.Location = New System.Drawing.Point(271, 53)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(162, 20)
        Me.lblLocationDesc.TabIndex = 1619
        Me.lblLocationDesc.TextWrap = False
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(85, 53)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblLocation
        Me.txtLocation.MyLinkLable2 = Me.lblLocationDesc
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(180, 21)
        Me.txtLocation.TabIndex = 1618
        Me.txtLocation.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(4, 55)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 1617
        Me.lblLocation.Text = "Location"
        '
        'txtLoadingSlip
        '
        Me.txtLoadingSlip.CalculationExpression = Nothing
        Me.txtLoadingSlip.FieldCode = Nothing
        Me.txtLoadingSlip.FieldDesc = Nothing
        Me.txtLoadingSlip.FieldMaxLength = 0
        Me.txtLoadingSlip.FieldName = Nothing
        Me.txtLoadingSlip.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoadingSlip.isCalculatedField = False
        Me.txtLoadingSlip.IsSourceFromTable = False
        Me.txtLoadingSlip.IsSourceFromValueList = False
        Me.txtLoadingSlip.IsUnique = False
        Me.txtLoadingSlip.Location = New System.Drawing.Point(520, 32)
        Me.txtLoadingSlip.MaxLength = 200
        Me.txtLoadingSlip.MendatroryField = False
        Me.txtLoadingSlip.MyLinkLable1 = Nothing
        Me.txtLoadingSlip.MyLinkLable2 = Nothing
        Me.txtLoadingSlip.Name = "txtLoadingSlip"
        Me.txtLoadingSlip.ReferenceFieldDesc = Nothing
        Me.txtLoadingSlip.ReferenceFieldName = Nothing
        Me.txtLoadingSlip.ReferenceTableName = Nothing
        Me.txtLoadingSlip.Size = New System.Drawing.Size(182, 18)
        Me.txtLoadingSlip.TabIndex = 84
        '
        'lblLoadingSlip
        '
        Me.lblLoadingSlip.FieldName = Nothing
        Me.lblLoadingSlip.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoadingSlip.Location = New System.Drawing.Point(439, 33)
        Me.lblLoadingSlip.Name = "lblLoadingSlip"
        Me.lblLoadingSlip.Size = New System.Drawing.Size(69, 16)
        Me.lblLoadingSlip.TabIndex = 85
        Me.lblLoadingSlip.Text = "Loading Slip"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(522, 6)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(91, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 83
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy  hh:mm tt"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(366, 4)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblpaymentpostdate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(131, 20)
        Me.txtDate.TabIndex = 29
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "10/06/2011  11:51 AM"
        Me.txtDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblpaymentpostdate
        '
        Me.lblpaymentpostdate.FieldName = Nothing
        Me.lblpaymentpostdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentpostdate.Location = New System.Drawing.Point(335, 6)
        Me.lblpaymentpostdate.Name = "lblpaymentpostdate"
        Me.lblpaymentpostdate.Size = New System.Drawing.Size(30, 16)
        Me.lblpaymentpostdate.TabIndex = 32
        Me.lblpaymentpostdate.Text = "Date"
        '
        'lblGPCode
        '
        Me.lblGPCode.FieldName = Nothing
        Me.lblGPCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGPCode.Location = New System.Drawing.Point(4, 6)
        Me.lblGPCode.Name = "lblGPCode"
        Me.lblGPCode.Size = New System.Drawing.Size(77, 16)
        Me.lblGPCode.TabIndex = 33
        Me.lblGPCode.Text = "Gate Pass No"
        '
        'btnNew
        '
        Me.btnNew.BackgroundImage = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnNew.Image = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(316, 4)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(19, 21)
        Me.btnNew.TabIndex = 31
        '
        'txtDocCode
        '
        Me.txtDocCode.FieldName = Nothing
        Me.txtDocCode.Location = New System.Drawing.Point(82, 4)
        Me.txtDocCode.MendatroryField = True
        Me.txtDocCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocCode.MyLinkLable1 = Me.lblGPCode
        Me.txtDocCode.MyLinkLable2 = Nothing
        Me.txtDocCode.MyMaxLength = 30
        Me.txtDocCode.MyReadOnly = False
        Me.txtDocCode.Name = "txtDocCode"
        Me.txtDocCode.Size = New System.Drawing.Size(234, 21)
        Me.txtDocCode.TabIndex = 30
        Me.txtDocCode.Value = ""
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.Gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.Gv1.MyExportFilePath = ""
        Me.Gv1.MyStopExport = False
        Me.Gv1.Name = "Gv1"
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(800, 216)
        Me.Gv1.TabIndex = 2
        Me.Gv1.VarID = ""
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(729, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 24)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(143, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 24)
        Me.btnDelete.TabIndex = 10
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(213, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 24)
        Me.btnPrint.TabIndex = 8
        Me.btnPrint.Text = "Print"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(74, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 24)
        Me.btnPost.TabIndex = 9
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(4, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 24)
        Me.btnSave.TabIndex = 7
        Me.btnSave.Text = "Save"
        '
        'btnGPCancel
        '
        Me.btnGPCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGPCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGPCancel.Location = New System.Drawing.Point(577, 5)
        Me.btnGPCancel.Name = "btnGPCancel"
        Me.btnGPCancel.Size = New System.Drawing.Size(69, 24)
        Me.btnGPCancel.TabIndex = 1036
        Me.btnGPCancel.Text = "Cancel"
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(651, 5)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(69, 24)
        Me.btnReverse.TabIndex = 1035
        Me.btnReverse.Text = "Reverse"
        Me.btnReverse.Visible = False
        '
        'FrmSalesOrderGatePass
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmSalesOrderGatePass"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmSalesOrderGatePass"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDriverMobNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDriverMobno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDriverName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDriveName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTranspoter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDispatchCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubLocaiton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDispatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLoadingSlip, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLoadingSlip, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentpostdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGPCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGPCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RadMenu1 As RadMenu
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents btnPost As RadButton
    Friend WithEvents btnSave As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblpaymentpostdate As common.Controls.MyLabel
    Friend WithEvents lblGPCode As common.Controls.MyLabel
    Friend WithEvents btnNew As RadButton
    Friend WithEvents txtDocCode As common.UserControls.txtNavigator
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtLoadingSlip As common.Controls.MyTextBox
    Friend WithEvents lblLoadingSlip As common.Controls.MyLabel
    Friend WithEvents lblDispatchCode As common.Controls.MyLabel
    Friend WithEvents lblSubLocationDesc As common.Controls.MyLabel
    Friend WithEvents lblSubLocaiton As common.Controls.MyLabel
    Friend WithEvents txtDispatchCode As common.UserControls.txtFinder
    Friend WithEvents lblDispatch As common.Controls.MyLabel
    Friend WithEvents txtSubLocation As common.UserControls.txtFinder
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtRemark As common.Controls.MyTextBox
    Friend WithEvents lblRemark As common.Controls.MyLabel
    Friend WithEvents lblTransporterName As common.Controls.MyTextBox
    Friend WithEvents lblTranspoter As common.Controls.MyLabel
    Friend WithEvents txtTransporterCode As common.UserControls.txtFinder
    Friend WithEvents lblVehicleNo As common.Controls.MyTextBox
    Friend WithEvents txtVehicleCode As common.UserControls.txtFinder
    Friend WithEvents lblVehicleCode As common.Controls.MyLabel
    Friend WithEvents txtDriverMobNo As common.Controls.MyTextBox
    Friend WithEvents lblDriverMobno As common.Controls.MyLabel
    Friend WithEvents txtDriverName As common.Controls.MyTextBox
    Friend WithEvents lblDriveName As common.Controls.MyLabel
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnGPCancel As RadButton
    Friend WithEvents btnReverse As RadButton
End Class
