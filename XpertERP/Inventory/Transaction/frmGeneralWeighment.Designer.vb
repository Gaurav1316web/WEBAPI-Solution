<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGeneralWeighment
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbtnJobWork = New common.Controls.MyRadioButton()
        Me.rbtnScrap = New common.Controls.MyRadioButton()
        Me.rbtnNone = New common.Controls.MyRadioButton()
        Me.txtGateInNoJW = New common.UserControls.txtFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.chkEmptyVehicle = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblItemName = New common.Controls.MyLabel()
        Me.txtItemCode = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtTransporter = New common.Controls.MyTextBox()
        Me.lblTransporter = New common.Controls.MyLabel()
        Me.lblComments = New common.Controls.MyLabel()
        Me.txtComments = New common.Controls.MyTextBox()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.lblWeighment = New common.Controls.MyLabel()
        Me.lblRemarks = New common.Controls.MyLabel()
        Me.fndWeighmentcode = New common.UserControls.txtNavigator()
        Me.txtNetWeight = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtGrossWeight = New common.MyNumBox()
        Me.txtVehicle_No = New common.Controls.MyTextBox()
        Me.LblLocationName = New common.Controls.MyLabel()
        Me.txtWeighmentdate = New common.Controls.MyDateTimePicker()
        Me.lblVehicle_No = New common.Controls.MyLabel()
        Me.TxtTareWeight = New common.MyNumBox()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.UcWeighing1 = New XpertERPEngine.ucWeighing()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.dtpGrossWeighment = New common.Controls.MyDateTimePicker()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.dtpTareWeighment = New common.Controls.MyDateTimePicker()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.rbtnJobWork, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnScrap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnNone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEmptyVehicle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTransporter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblComments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWeighment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNetWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGrossWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicle_No, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWeighmentdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicle_No, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTareWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpGrossWeighment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpTareWeighment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 64)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpTareWeighment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpGrossWeighment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtGateInNoJW)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkEmptyVehicle)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblItemName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtItemCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTransporter)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTransporter)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblComments)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtComments)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblWeighment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndWeighmentcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtNetWeight)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtGrossWeight)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtVehicle_No)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblLocationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtWeighmentdate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblVehicle_No)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtTareWeight)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(978, 262)
        Me.SplitContainer1.SplitterDistance = 218
        Me.SplitContainer1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbtnJobWork)
        Me.Panel1.Controls.Add(Me.rbtnScrap)
        Me.Panel1.Controls.Add(Me.rbtnNone)
        Me.Panel1.Location = New System.Drawing.Point(330, 48)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(144, 61)
        Me.Panel1.TabIndex = 357
        '
        'rbtnJobWork
        '
        Me.rbtnJobWork.Location = New System.Drawing.Point(3, 21)
        Me.rbtnJobWork.MyLinkLable1 = Nothing
        Me.rbtnJobWork.MyLinkLable2 = Nothing
        Me.rbtnJobWork.Name = "rbtnJobWork"
        Me.rbtnJobWork.Size = New System.Drawing.Size(67, 18)
        Me.rbtnJobWork.TabIndex = 2
        Me.rbtnJobWork.Text = "Job Work"
        '
        'rbtnScrap
        '
        Me.rbtnScrap.Location = New System.Drawing.Point(3, 39)
        Me.rbtnScrap.MyLinkLable1 = Nothing
        Me.rbtnScrap.MyLinkLable2 = Nothing
        Me.rbtnScrap.Name = "rbtnScrap"
        Me.rbtnScrap.Size = New System.Drawing.Size(48, 18)
        Me.rbtnScrap.TabIndex = 1
        Me.rbtnScrap.Text = "Scrap"
        '
        'rbtnNone
        '
        Me.rbtnNone.Location = New System.Drawing.Point(3, 3)
        Me.rbtnNone.MyLinkLable1 = Nothing
        Me.rbtnNone.MyLinkLable2 = Nothing
        Me.rbtnNone.Name = "rbtnNone"
        Me.rbtnNone.Size = New System.Drawing.Size(48, 18)
        Me.rbtnNone.TabIndex = 1
        Me.rbtnNone.Text = "None"
        '
        'txtGateInNoJW
        '
        Me.txtGateInNoJW.CalculationExpression = Nothing
        Me.txtGateInNoJW.FieldCode = Nothing
        Me.txtGateInNoJW.FieldDesc = Nothing
        Me.txtGateInNoJW.FieldMaxLength = 0
        Me.txtGateInNoJW.FieldName = Nothing
        Me.txtGateInNoJW.isCalculatedField = False
        Me.txtGateInNoJW.IsSourceFromTable = False
        Me.txtGateInNoJW.IsSourceFromValueList = False
        Me.txtGateInNoJW.IsUnique = False
        Me.txtGateInNoJW.Location = New System.Drawing.Point(138, 48)
        Me.txtGateInNoJW.MendatroryField = True
        Me.txtGateInNoJW.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGateInNoJW.MyLinkLable1 = Nothing
        Me.txtGateInNoJW.MyLinkLable2 = Nothing
        Me.txtGateInNoJW.MyReadOnly = False
        Me.txtGateInNoJW.MyShowMasterFormButton = False
        Me.txtGateInNoJW.Name = "txtGateInNoJW"
        Me.txtGateInNoJW.ReferenceFieldDesc = Nothing
        Me.txtGateInNoJW.ReferenceFieldName = Nothing
        Me.txtGateInNoJW.ReferenceTableName = Nothing
        Me.txtGateInNoJW.Size = New System.Drawing.Size(189, 20)
        Me.txtGateInNoJW.TabIndex = 355
        Me.txtGateInNoJW.Value = ""
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(7, 48)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(78, 16)
        Me.MyLabel5.TabIndex = 353
        Me.MyLabel5.Text = "Gate Entry No"
        '
        'chkEmptyVehicle
        '
        Me.chkEmptyVehicle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEmptyVehicle.Location = New System.Drawing.Point(329, 29)
        Me.chkEmptyVehicle.Name = "chkEmptyVehicle"
        Me.chkEmptyVehicle.Size = New System.Drawing.Size(93, 16)
        Me.chkEmptyVehicle.TabIndex = 352
        Me.chkEmptyVehicle.Text = "Empty Vehicle"
        '
        'lblItemName
        '
        Me.lblItemName.AutoSize = False
        Me.lblItemName.BorderVisible = True
        Me.lblItemName.FieldName = Nothing
        Me.lblItemName.Location = New System.Drawing.Point(330, 155)
        Me.lblItemName.Name = "lblItemName"
        Me.lblItemName.Size = New System.Drawing.Size(365, 19)
        Me.lblItemName.TabIndex = 351
        Me.lblItemName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtItemCode
        '
        Me.txtItemCode.CalculationExpression = Nothing
        Me.txtItemCode.FieldCode = Nothing
        Me.txtItemCode.FieldDesc = Nothing
        Me.txtItemCode.FieldMaxLength = 0
        Me.txtItemCode.FieldName = Nothing
        Me.txtItemCode.isCalculatedField = False
        Me.txtItemCode.IsSourceFromTable = False
        Me.txtItemCode.IsSourceFromValueList = False
        Me.txtItemCode.IsUnique = False
        Me.txtItemCode.Location = New System.Drawing.Point(138, 156)
        Me.txtItemCode.MendatroryField = True
        Me.txtItemCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.MyLinkLable1 = Nothing
        Me.txtItemCode.MyLinkLable2 = Nothing
        Me.txtItemCode.MyReadOnly = False
        Me.txtItemCode.MyShowMasterFormButton = False
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.ReferenceFieldDesc = Nothing
        Me.txtItemCode.ReferenceFieldName = Nothing
        Me.txtItemCode.ReferenceTableName = Nothing
        Me.txtItemCode.Size = New System.Drawing.Size(189, 20)
        Me.txtItemCode.TabIndex = 350
        Me.txtItemCode.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(7, 158)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel4.TabIndex = 349
        Me.MyLabel4.Text = "Item Code"
        '
        'txtTransporter
        '
        Me.txtTransporter.CalculationExpression = Nothing
        Me.txtTransporter.FieldCode = Nothing
        Me.txtTransporter.FieldDesc = Nothing
        Me.txtTransporter.FieldMaxLength = 0
        Me.txtTransporter.FieldName = Nothing
        Me.txtTransporter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransporter.isCalculatedField = False
        Me.txtTransporter.IsSourceFromTable = False
        Me.txtTransporter.IsSourceFromValueList = False
        Me.txtTransporter.IsUnique = False
        Me.txtTransporter.Location = New System.Drawing.Point(138, 91)
        Me.txtTransporter.MaxLength = 50
        Me.txtTransporter.MendatroryField = False
        Me.txtTransporter.MyLinkLable1 = Nothing
        Me.txtTransporter.MyLinkLable2 = Nothing
        Me.txtTransporter.Name = "txtTransporter"
        Me.txtTransporter.ReferenceFieldDesc = Nothing
        Me.txtTransporter.ReferenceFieldName = Nothing
        Me.txtTransporter.ReferenceTableName = Nothing
        Me.txtTransporter.Size = New System.Drawing.Size(189, 18)
        Me.txtTransporter.TabIndex = 53
        '
        'lblTransporter
        '
        Me.lblTransporter.FieldName = Nothing
        Me.lblTransporter.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblTransporter.Location = New System.Drawing.Point(7, 92)
        Me.lblTransporter.Name = "lblTransporter"
        Me.lblTransporter.Size = New System.Drawing.Size(65, 16)
        Me.lblTransporter.TabIndex = 52
        Me.lblTransporter.Text = "Transporter"
        '
        'lblComments
        '
        Me.lblComments.FieldName = Nothing
        Me.lblComments.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblComments.Location = New System.Drawing.Point(7, 180)
        Me.lblComments.Name = "lblComments"
        Me.lblComments.Size = New System.Drawing.Size(61, 16)
        Me.lblComments.TabIndex = 49
        Me.lblComments.Text = "Comments"
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
        Me.txtComments.Location = New System.Drawing.Point(138, 179)
        Me.txtComments.MaxLength = 200
        Me.txtComments.MendatroryField = False
        Me.txtComments.MyLinkLable1 = Nothing
        Me.txtComments.MyLinkLable2 = Nothing
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ReferenceFieldDesc = Nothing
        Me.txtComments.ReferenceFieldName = Nothing
        Me.txtComments.ReferenceTableName = Nothing
        Me.txtComments.Size = New System.Drawing.Size(557, 18)
        Me.txtComments.TabIndex = 51
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
        Me.txtLocation.Location = New System.Drawing.Point(138, 112)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Nothing
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(189, 20)
        Me.txtLocation.TabIndex = 50
        Me.txtLocation.Value = ""
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
        Me.txtRemarks.Location = New System.Drawing.Point(138, 135)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Nothing
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(557, 18)
        Me.txtRemarks.TabIndex = 50
        '
        'lblWeighment
        '
        Me.lblWeighment.FieldName = Nothing
        Me.lblWeighment.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblWeighment.Location = New System.Drawing.Point(7, 6)
        Me.lblWeighment.Name = "lblWeighment"
        Me.lblWeighment.Size = New System.Drawing.Size(111, 16)
        Me.lblWeighment.TabIndex = 17
        Me.lblWeighment.Text = "Weighment Entry No"
        '
        'lblRemarks
        '
        Me.lblRemarks.FieldName = Nothing
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblRemarks.Location = New System.Drawing.Point(7, 136)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(51, 16)
        Me.lblRemarks.TabIndex = 48
        Me.lblRemarks.Text = "Remarks"
        '
        'fndWeighmentcode
        '
        Me.fndWeighmentcode.FieldName = Nothing
        Me.fndWeighmentcode.Location = New System.Drawing.Point(138, 4)
        Me.fndWeighmentcode.MendatroryField = True
        Me.fndWeighmentcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndWeighmentcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndWeighmentcode.MyLinkLable1 = Me.lblWeighment
        Me.fndWeighmentcode.MyLinkLable2 = Nothing
        Me.fndWeighmentcode.MyMaxLength = 32767
        Me.fndWeighmentcode.MyReadOnly = False
        Me.fndWeighmentcode.Name = "fndWeighmentcode"
        Me.fndWeighmentcode.Size = New System.Drawing.Size(227, 21)
        Me.fndWeighmentcode.TabIndex = 0
        Me.fndWeighmentcode.Value = ""
        '
        'txtNetWeight
        '
        Me.txtNetWeight.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtNetWeight.CalculationExpression = Nothing
        Me.txtNetWeight.DecimalPlaces = 2
        Me.txtNetWeight.FieldCode = Nothing
        Me.txtNetWeight.FieldDesc = Nothing
        Me.txtNetWeight.FieldMaxLength = 0
        Me.txtNetWeight.FieldName = Nothing
        Me.txtNetWeight.isCalculatedField = False
        Me.txtNetWeight.IsSourceFromTable = False
        Me.txtNetWeight.IsSourceFromValueList = False
        Me.txtNetWeight.IsUnique = False
        Me.txtNetWeight.Location = New System.Drawing.Point(570, 48)
        Me.txtNetWeight.MendatroryField = True
        Me.txtNetWeight.MyLinkLable1 = Nothing
        Me.txtNetWeight.MyLinkLable2 = Nothing
        Me.txtNetWeight.Name = "txtNetWeight"
        Me.txtNetWeight.ReferenceFieldDesc = Nothing
        Me.txtNetWeight.ReferenceFieldName = Nothing
        Me.txtNetWeight.ReferenceTableName = Nothing
        Me.txtNetWeight.Size = New System.Drawing.Size(125, 20)
        Me.txtNetWeight.TabIndex = 11
        Me.txtNetWeight.Text = "0"
        Me.txtNetWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNetWeight.Value = 0.0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(7, 29)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(120, 16)
        Me.MyLabel2.TabIndex = 22
        Me.MyLabel2.Text = "Weighment Entry Date"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(483, 50)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel6.TabIndex = 45
        Me.MyLabel6.Text = "Net Weight"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(483, 6)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel7.TabIndex = 47
        Me.MyLabel7.Text = "Gross Weight"
        '
        'txtGrossWeight
        '
        Me.txtGrossWeight.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtGrossWeight.CalculationExpression = Nothing
        Me.txtGrossWeight.DecimalPlaces = 2
        Me.txtGrossWeight.FieldCode = Nothing
        Me.txtGrossWeight.FieldDesc = Nothing
        Me.txtGrossWeight.FieldMaxLength = 0
        Me.txtGrossWeight.FieldName = Nothing
        Me.txtGrossWeight.isCalculatedField = False
        Me.txtGrossWeight.IsSourceFromTable = False
        Me.txtGrossWeight.IsSourceFromValueList = False
        Me.txtGrossWeight.IsUnique = False
        Me.txtGrossWeight.Location = New System.Drawing.Point(570, 4)
        Me.txtGrossWeight.MendatroryField = True
        Me.txtGrossWeight.MyLinkLable1 = Nothing
        Me.txtGrossWeight.MyLinkLable2 = Nothing
        Me.txtGrossWeight.Name = "txtGrossWeight"
        Me.txtGrossWeight.ReferenceFieldDesc = Nothing
        Me.txtGrossWeight.ReferenceFieldName = Nothing
        Me.txtGrossWeight.ReferenceTableName = Nothing
        Me.txtGrossWeight.Size = New System.Drawing.Size(125, 20)
        Me.txtGrossWeight.TabIndex = 10
        Me.txtGrossWeight.Text = "0"
        Me.txtGrossWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtGrossWeight.Value = 0.0R
        '
        'txtVehicle_No
        '
        Me.txtVehicle_No.CalculationExpression = Nothing
        Me.txtVehicle_No.FieldCode = Nothing
        Me.txtVehicle_No.FieldDesc = Nothing
        Me.txtVehicle_No.FieldMaxLength = 0
        Me.txtVehicle_No.FieldName = Nothing
        Me.txtVehicle_No.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicle_No.isCalculatedField = False
        Me.txtVehicle_No.IsSourceFromTable = False
        Me.txtVehicle_No.IsSourceFromValueList = False
        Me.txtVehicle_No.IsUnique = False
        Me.txtVehicle_No.Location = New System.Drawing.Point(138, 70)
        Me.txtVehicle_No.MaxLength = 50
        Me.txtVehicle_No.MendatroryField = False
        Me.txtVehicle_No.MyLinkLable1 = Nothing
        Me.txtVehicle_No.MyLinkLable2 = Nothing
        Me.txtVehicle_No.Name = "txtVehicle_No"
        Me.txtVehicle_No.ReferenceFieldDesc = Nothing
        Me.txtVehicle_No.ReferenceFieldName = Nothing
        Me.txtVehicle_No.ReferenceTableName = Nothing
        Me.txtVehicle_No.Size = New System.Drawing.Size(189, 18)
        Me.txtVehicle_No.TabIndex = 49
        '
        'LblLocationName
        '
        Me.LblLocationName.AutoSize = False
        Me.LblLocationName.BorderVisible = True
        Me.LblLocationName.FieldName = Nothing
        Me.LblLocationName.Location = New System.Drawing.Point(330, 113)
        Me.LblLocationName.Name = "LblLocationName"
        Me.LblLocationName.Size = New System.Drawing.Size(365, 19)
        Me.LblLocationName.TabIndex = 8
        Me.LblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtWeighmentdate
        '
        Me.txtWeighmentdate.CalculationExpression = Nothing
        Me.txtWeighmentdate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtWeighmentdate.FieldCode = Nothing
        Me.txtWeighmentdate.FieldDesc = Nothing
        Me.txtWeighmentdate.FieldMaxLength = 0
        Me.txtWeighmentdate.FieldName = Nothing
        Me.txtWeighmentdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeighmentdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtWeighmentdate.isCalculatedField = False
        Me.txtWeighmentdate.IsSourceFromTable = False
        Me.txtWeighmentdate.IsSourceFromValueList = False
        Me.txtWeighmentdate.IsUnique = False
        Me.txtWeighmentdate.Location = New System.Drawing.Point(138, 28)
        Me.txtWeighmentdate.MendatroryField = True
        Me.txtWeighmentdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtWeighmentdate.MyLinkLable1 = Me.MyLabel2
        Me.txtWeighmentdate.MyLinkLable2 = Nothing
        Me.txtWeighmentdate.Name = "txtWeighmentdate"
        Me.txtWeighmentdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtWeighmentdate.ReferenceFieldDesc = Nothing
        Me.txtWeighmentdate.ReferenceFieldName = Nothing
        Me.txtWeighmentdate.ReferenceTableName = Nothing
        Me.txtWeighmentdate.Size = New System.Drawing.Size(134, 18)
        Me.txtWeighmentdate.TabIndex = 2
        Me.txtWeighmentdate.TabStop = False
        Me.txtWeighmentdate.Text = "13/06/2011 11:29 AM"
        Me.txtWeighmentdate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblVehicle_No
        '
        Me.lblVehicle_No.FieldName = Nothing
        Me.lblVehicle_No.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblVehicle_No.Location = New System.Drawing.Point(7, 71)
        Me.lblVehicle_No.Name = "lblVehicle_No"
        Me.lblVehicle_No.Size = New System.Drawing.Size(99, 16)
        Me.lblVehicle_No.TabIndex = 46
        Me.lblVehicle_No.Text = "Tanker/Vehicle No"
        '
        'TxtTareWeight
        '
        Me.TxtTareWeight.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtTareWeight.CalculationExpression = Nothing
        Me.TxtTareWeight.DecimalPlaces = 2
        Me.TxtTareWeight.FieldCode = Nothing
        Me.TxtTareWeight.FieldDesc = Nothing
        Me.TxtTareWeight.FieldMaxLength = 0
        Me.TxtTareWeight.FieldName = Nothing
        Me.TxtTareWeight.isCalculatedField = False
        Me.TxtTareWeight.IsSourceFromTable = False
        Me.TxtTareWeight.IsSourceFromValueList = False
        Me.TxtTareWeight.IsUnique = False
        Me.TxtTareWeight.Location = New System.Drawing.Point(570, 27)
        Me.TxtTareWeight.MendatroryField = True
        Me.TxtTareWeight.MyLinkLable1 = Nothing
        Me.TxtTareWeight.MyLinkLable2 = Nothing
        Me.TxtTareWeight.Name = "TxtTareWeight"
        Me.TxtTareWeight.ReferenceFieldDesc = Nothing
        Me.TxtTareWeight.ReferenceFieldName = Nothing
        Me.TxtTareWeight.ReferenceTableName = Nothing
        Me.TxtTareWeight.Size = New System.Drawing.Size(125, 20)
        Me.TxtTareWeight.TabIndex = 9
        Me.TxtTareWeight.Text = "0"
        Me.TxtTareWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtTareWeight.Value = 0.0R
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(365, 4)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(19, 21)
        Me.btnnew.TabIndex = 1
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(483, 29)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel3.TabIndex = 44
        Me.MyLabel3.Text = "Tare Weight"
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblLocation.Location = New System.Drawing.Point(7, 114)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 21
        Me.lblLocation.Text = "Location"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(385, 4)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(92, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 48
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(315, 8)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(68, 20)
        Me.btnReverse.TabIndex = 5
        Me.btnReverse.Text = "Reverse"
        Me.btnReverse.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(241, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 20)
        Me.btnPrint.TabIndex = 4
        Me.btnPrint.Text = "Print"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(84, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(893, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(162, 8)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(7, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'UcWeighing1
        '
        Me.UcWeighing1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UcWeighing1.form_ID = Nothing
        Me.UcWeighing1.LiveReading = 0.0R
        Me.UcWeighing1.Location = New System.Drawing.Point(0, 0)
        Me.UcWeighing1.Machine = ""
        Me.UcWeighing1.Name = "UcWeighing1"
        Me.UcWeighing1.Port = ""
        Me.UcWeighing1.Size = New System.Drawing.Size(978, 64)
        Me.UcWeighing1.TabIndex = 1
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(701, 6)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(102, 16)
        Me.MyLabel8.TabIndex = 358
        Me.MyLabel8.Text = "Gross Weight Date"
        '
        'dtpGrossWeighment
        '
        Me.dtpGrossWeighment.CalculationExpression = Nothing
        Me.dtpGrossWeighment.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpGrossWeighment.FieldCode = Nothing
        Me.dtpGrossWeighment.FieldDesc = Nothing
        Me.dtpGrossWeighment.FieldMaxLength = 0
        Me.dtpGrossWeighment.FieldName = Nothing
        Me.dtpGrossWeighment.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpGrossWeighment.isCalculatedField = False
        Me.dtpGrossWeighment.IsSourceFromTable = False
        Me.dtpGrossWeighment.IsSourceFromValueList = False
        Me.dtpGrossWeighment.IsUnique = False
        Me.dtpGrossWeighment.Location = New System.Drawing.Point(809, 4)
        Me.dtpGrossWeighment.MendatroryField = False
        Me.dtpGrossWeighment.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpGrossWeighment.MyLinkLable1 = Me.MyLabel2
        Me.dtpGrossWeighment.MyLinkLable2 = Nothing
        Me.dtpGrossWeighment.Name = "dtpGrossWeighment"
        Me.dtpGrossWeighment.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpGrossWeighment.ReferenceFieldDesc = Nothing
        Me.dtpGrossWeighment.ReferenceFieldName = Nothing
        Me.dtpGrossWeighment.ReferenceTableName = Nothing
        Me.dtpGrossWeighment.Size = New System.Drawing.Size(132, 20)
        Me.dtpGrossWeighment.TabIndex = 359
        Me.dtpGrossWeighment.TabStop = False
        Me.dtpGrossWeighment.Text = "10/06/2011 11:51 AM"
        Me.dtpGrossWeighment.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(701, 29)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(95, 16)
        Me.MyLabel9.TabIndex = 360
        Me.MyLabel9.Text = "Tare Weight Date"
        '
        'dtpTareWeighment
        '
        Me.dtpTareWeighment.CalculationExpression = Nothing
        Me.dtpTareWeighment.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpTareWeighment.FieldCode = Nothing
        Me.dtpTareWeighment.FieldDesc = Nothing
        Me.dtpTareWeighment.FieldMaxLength = 0
        Me.dtpTareWeighment.FieldName = Nothing
        Me.dtpTareWeighment.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTareWeighment.isCalculatedField = False
        Me.dtpTareWeighment.IsSourceFromTable = False
        Me.dtpTareWeighment.IsSourceFromValueList = False
        Me.dtpTareWeighment.IsUnique = False
        Me.dtpTareWeighment.Location = New System.Drawing.Point(809, 28)
        Me.dtpTareWeighment.MendatroryField = False
        Me.dtpTareWeighment.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTareWeighment.MyLinkLable1 = Me.MyLabel2
        Me.dtpTareWeighment.MyLinkLable2 = Nothing
        Me.dtpTareWeighment.Name = "dtpTareWeighment"
        Me.dtpTareWeighment.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTareWeighment.ReferenceFieldDesc = Nothing
        Me.dtpTareWeighment.ReferenceFieldName = Nothing
        Me.dtpTareWeighment.ReferenceTableName = Nothing
        Me.dtpTareWeighment.Size = New System.Drawing.Size(132, 20)
        Me.dtpTareWeighment.TabIndex = 361
        Me.dtpTareWeighment.TabStop = False
        Me.dtpTareWeighment.Text = "10/06/2011 11:51 AM"
        Me.dtpTareWeighment.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'frmGeneralWeighment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(978, 326)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.UcWeighing1)
        Me.Name = "frmGeneralWeighment"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmGeneralWeighment"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.rbtnJobWork, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnScrap, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnNone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEmptyVehicle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTransporter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblComments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWeighment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNetWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGrossWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicle_No, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWeighmentdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicle_No, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTareWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpGrossWeighment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpTareWeighment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblWeighment As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndWeighmentcode As common.UserControls.txtNavigator
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtWeighmentdate As common.Controls.MyDateTimePicker
    Friend WithEvents TxtTareWeight As common.MyNumBox
    Friend WithEvents txtNetWeight As common.MyNumBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtGrossWeight As common.MyNumBox
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblVehicle_No As common.Controls.MyLabel
    Friend WithEvents LblLocationName As common.Controls.MyLabel
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents UcWeighing1 As XpertERPEngine.ucWeighing
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtVehicle_No As common.Controls.MyTextBox
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents lblComments As common.Controls.MyLabel
    Friend WithEvents lblRemarks As common.Controls.MyLabel
    Friend WithEvents txtComments As common.Controls.MyTextBox
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents lblTransporter As common.Controls.MyLabel
    Friend WithEvents txtTransporter As common.Controls.MyTextBox
    Friend WithEvents txtItemCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblItemName As common.Controls.MyLabel
    Friend WithEvents chkEmptyVehicle As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtGateInNoJW As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbtnJobWork As common.Controls.MyRadioButton
    Friend WithEvents rbtnScrap As common.Controls.MyRadioButton
    Friend WithEvents rbtnNone As common.Controls.MyRadioButton
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents dtpGrossWeighment As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents dtpTareWeighment As common.Controls.MyDateTimePicker
End Class

