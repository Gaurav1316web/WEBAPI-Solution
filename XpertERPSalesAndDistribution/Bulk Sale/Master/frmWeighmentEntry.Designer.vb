<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmWeighmentEntry
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
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.lblWeighment = New common.Controls.MyLabel()
        Me.lblGateEntryNo = New common.Controls.MyLabel()
        Me.fndWeighmentcode = New common.UserControls.txtNavigator()
        Me.chkPendingGrossWeight = New common.Controls.MyCheckBox()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.FndTankerNo = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblQCStatus = New common.Controls.MyLabel()
        Me.txtWeighmentdate = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.LblLocationName = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.LblTankerName = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblLocationCode = New common.Controls.MyLabel()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.lblTankerNoValue = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.fndGateEntryNo = New common.UserControls.txtFinder()
        Me.TxtTareWeight = New common.MyNumBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtNetWeight = New common.MyNumBox()
        Me.txtGrossWeight = New common.MyNumBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvItemBulk = New common.UserControls.MyRadGridView()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.UcWeighing1 = New XpertERPEngine.ucWeighing()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblWeighment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGateEntryNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPendingGrossWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQCStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWeighmentdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblTankerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.lblTankerNoValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTareWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNetWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGrossWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gvItemBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItemBulk.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(879, 422)
        Me.SplitContainer1.SplitterDistance = 378
        Me.SplitContainer1.TabIndex = 0
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblWeighment)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblGateEntryNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndWeighmentcode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkPendingGrossWeight)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.FndTankerNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblQCStatus)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtWeighmentdate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.LblLocationName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.LblTankerName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocationCode)
        Me.SplitContainer2.Panel1.Padding = New System.Windows.Forms.Padding(2)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(2)
        Me.SplitContainer2.Size = New System.Drawing.Size(879, 378)
        Me.SplitContainer2.SplitterDistance = 110
        Me.SplitContainer2.TabIndex = 51
        '
        'lblWeighment
        '
        Me.lblWeighment.FieldName = Nothing
        Me.lblWeighment.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblWeighment.Location = New System.Drawing.Point(6, 6)
        Me.lblWeighment.Name = "lblWeighment"
        Me.lblWeighment.Size = New System.Drawing.Size(111, 16)
        Me.lblWeighment.TabIndex = 17
        Me.lblWeighment.Text = "Weighment Entry No"
        '
        'lblGateEntryNo
        '
        Me.lblGateEntryNo.AutoSize = False
        Me.lblGateEntryNo.BorderVisible = True
        Me.lblGateEntryNo.FieldName = Nothing
        Me.lblGateEntryNo.Location = New System.Drawing.Point(131, 69)
        Me.lblGateEntryNo.Name = "lblGateEntryNo"
        Me.lblGateEntryNo.Size = New System.Drawing.Size(125, 19)
        Me.lblGateEntryNo.TabIndex = 50
        Me.lblGateEntryNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fndWeighmentcode
        '
        Me.fndWeighmentcode.FieldName = Nothing
        Me.fndWeighmentcode.Location = New System.Drawing.Point(131, 2)
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
        'chkPendingGrossWeight
        '
        Me.chkPendingGrossWeight.Location = New System.Drawing.Point(264, 49)
        Me.chkPendingGrossWeight.MyLinkLable1 = Nothing
        Me.chkPendingGrossWeight.MyLinkLable2 = Nothing
        Me.chkPendingGrossWeight.Name = "chkPendingGrossWeight"
        Me.chkPendingGrossWeight.Size = New System.Drawing.Size(143, 18)
        Me.chkPendingGrossWeight.TabIndex = 49
        Me.chkPendingGrossWeight.Tag1 = Nothing
        Me.chkPendingGrossWeight.Text = "Is Pending Gross Weight"
        '
        'btnnew
        '
        Me.btnnew.Image = XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(366, 2)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 1
        '
        'FndTankerNo
        '
        Me.FndTankerNo.CalculationExpression = Nothing
        Me.FndTankerNo.FieldCode = Nothing
        Me.FndTankerNo.FieldDesc = Nothing
        Me.FndTankerNo.FieldMaxLength = 0
        Me.FndTankerNo.FieldName = Nothing
        Me.FndTankerNo.isCalculatedField = False
        Me.FndTankerNo.IsSourceFromTable = False
        Me.FndTankerNo.IsSourceFromValueList = False
        Me.FndTankerNo.IsUnique = False
        Me.FndTankerNo.Location = New System.Drawing.Point(131, 46)
        Me.FndTankerNo.MendatroryField = True
        Me.FndTankerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndTankerNo.MyLinkLable1 = Nothing
        Me.FndTankerNo.MyLinkLable2 = Nothing
        Me.FndTankerNo.MyReadOnly = False
        Me.FndTankerNo.MyShowMasterFormButton = False
        Me.FndTankerNo.Name = "FndTankerNo"
        Me.FndTankerNo.ReferenceFieldDesc = Nothing
        Me.FndTankerNo.ReferenceFieldName = Nothing
        Me.FndTankerNo.ReferenceTableName = Nothing
        Me.FndTankerNo.Size = New System.Drawing.Size(125, 19)
        Me.FndTankerNo.TabIndex = 4
        Me.FndTankerNo.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(6, 72)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(78, 16)
        Me.MyLabel1.TabIndex = 20
        Me.MyLabel1.Text = "Gate Entry No"
        '
        'lblQCStatus
        '
        Me.lblQCStatus.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblQCStatus.FieldName = Nothing
        Me.lblQCStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblQCStatus.Location = New System.Drawing.Point(264, 31)
        Me.lblQCStatus.Name = "lblQCStatus"
        Me.lblQCStatus.Size = New System.Drawing.Size(71, 16)
        Me.lblQCStatus.TabIndex = 3
        Me.lblQCStatus.Text = "QC Pending"
        Me.lblQCStatus.Visible = False
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
        Me.txtWeighmentdate.Location = New System.Drawing.Point(133, 28)
        Me.txtWeighmentdate.MendatroryField = True
        Me.txtWeighmentdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtWeighmentdate.MyLinkLable1 = Me.MyLabel2
        Me.txtWeighmentdate.MyLinkLable2 = Nothing
        Me.txtWeighmentdate.Name = "txtWeighmentdate"
        Me.txtWeighmentdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtWeighmentdate.ReferenceFieldDesc = Nothing
        Me.txtWeighmentdate.ReferenceFieldName = Nothing
        Me.txtWeighmentdate.ReferenceTableName = Nothing
        Me.txtWeighmentdate.Size = New System.Drawing.Size(125, 18)
        Me.txtWeighmentdate.TabIndex = 2
        Me.txtWeighmentdate.TabStop = False
        Me.txtWeighmentdate.Text = "13/06/2011 11:29 AM"
        Me.txtWeighmentdate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(6, 29)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(120, 16)
        Me.MyLabel2.TabIndex = 22
        Me.MyLabel2.Text = "Weighment Entry Date"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(387, 2)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(92, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 48
        '
        'LblLocationName
        '
        Me.LblLocationName.AutoSize = False
        Me.LblLocationName.BorderVisible = True
        Me.LblLocationName.FieldName = Nothing
        Me.LblLocationName.Location = New System.Drawing.Point(262, 90)
        Me.LblLocationName.Name = "LblLocationName"
        Me.LblLocationName.Size = New System.Drawing.Size(210, 19)
        Me.LblLocationName.TabIndex = 8
        Me.LblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(6, 48)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(59, 16)
        Me.MyLabel4.TabIndex = 46
        Me.MyLabel4.Text = "Tanker No"
        '
        'LblTankerName
        '
        Me.LblTankerName.AutoSize = False
        Me.LblTankerName.BorderVisible = True
        Me.LblTankerName.FieldName = Nothing
        Me.LblTankerName.Location = New System.Drawing.Point(262, 69)
        Me.LblTankerName.Name = "LblTankerName"
        Me.LblTankerName.Size = New System.Drawing.Size(210, 19)
        Me.LblTankerName.TabIndex = 6
        Me.LblTankerName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTankerName.Visible = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(6, 93)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel5.TabIndex = 21
        Me.MyLabel5.Text = "Location"
        '
        'lblLocationCode
        '
        Me.lblLocationCode.AutoSize = False
        Me.lblLocationCode.BorderVisible = True
        Me.lblLocationCode.FieldName = Nothing
        Me.lblLocationCode.Location = New System.Drawing.Point(131, 90)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(125, 19)
        Me.lblLocationCode.TabIndex = 7
        Me.lblLocationCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(2, 2)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblTankerNoValue)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndGateEntryNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.TxtTareWeight)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtNetWeight)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtGrossWeight)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer3.Panel1.Padding = New System.Windows.Forms.Padding(2)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer3.Panel2.Padding = New System.Windows.Forms.Padding(2)
        Me.SplitContainer3.Size = New System.Drawing.Size(875, 260)
        Me.SplitContainer3.SplitterDistance = 71
        Me.SplitContainer3.TabIndex = 0
        '
        'lblTankerNoValue
        '
        Me.lblTankerNoValue.AutoSize = False
        Me.lblTankerNoValue.BorderVisible = True
        Me.lblTankerNoValue.FieldName = Nothing
        Me.lblTankerNoValue.Location = New System.Drawing.Point(387, 23)
        Me.lblTankerNoValue.Name = "lblTankerNoValue"
        Me.lblTankerNoValue.Size = New System.Drawing.Size(56, 19)
        Me.lblTankerNoValue.TabIndex = 4
        Me.lblTankerNoValue.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTankerNoValue.Visible = False
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(3, 26)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel7.TabIndex = 47
        Me.MyLabel7.Text = "Gross Weight"
        '
        'fndGateEntryNo
        '
        Me.fndGateEntryNo.CalculationExpression = Nothing
        Me.fndGateEntryNo.Enabled = False
        Me.fndGateEntryNo.FieldCode = Nothing
        Me.fndGateEntryNo.FieldDesc = Nothing
        Me.fndGateEntryNo.FieldMaxLength = 0
        Me.fndGateEntryNo.FieldName = Nothing
        Me.fndGateEntryNo.isCalculatedField = False
        Me.fndGateEntryNo.IsSourceFromTable = False
        Me.fndGateEntryNo.IsSourceFromValueList = False
        Me.fndGateEntryNo.IsUnique = False
        Me.fndGateEntryNo.Location = New System.Drawing.Point(449, 23)
        Me.fndGateEntryNo.MendatroryField = True
        Me.fndGateEntryNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndGateEntryNo.MyLinkLable1 = Nothing
        Me.fndGateEntryNo.MyLinkLable2 = Nothing
        Me.fndGateEntryNo.MyReadOnly = False
        Me.fndGateEntryNo.MyShowMasterFormButton = False
        Me.fndGateEntryNo.Name = "fndGateEntryNo"
        Me.fndGateEntryNo.ReferenceFieldDesc = Nothing
        Me.fndGateEntryNo.ReferenceFieldName = Nothing
        Me.fndGateEntryNo.ReferenceTableName = Nothing
        Me.fndGateEntryNo.Size = New System.Drawing.Size(57, 19)
        Me.fndGateEntryNo.TabIndex = 5
        Me.fndGateEntryNo.Value = ""
        Me.fndGateEntryNo.Visible = False
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
        Me.TxtTareWeight.Location = New System.Drawing.Point(130, 1)
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
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(3, 3)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel3.TabIndex = 44
        Me.MyLabel3.Text = "Tare Weight"
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
        Me.txtNetWeight.Location = New System.Drawing.Point(130, 48)
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
        Me.txtGrossWeight.Location = New System.Drawing.Point(130, 24)
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
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(3, 50)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel6.TabIndex = 45
        Me.MyLabel6.Text = "Net Weight"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gvItemBulk)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = "Item Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(2, 2)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(871, 181)
        Me.RadGroupBox1.TabIndex = 286
        Me.RadGroupBox1.Text = "Item Details"
        '
        'gvItemBulk
        '
        Me.gvItemBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvItemBulk.Location = New System.Drawing.Point(2, 18)
        '
        'gvItemBulk
        '
        Me.gvItemBulk.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvItemBulk.Name = "gvItemBulk"
        Me.gvItemBulk.ShowHeaderCellButtons = True
        Me.gvItemBulk.Size = New System.Drawing.Size(867, 161)
        Me.gvItemBulk.TabIndex = 264
        Me.gvItemBulk.Text = "RadGridView1"
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
        Me.btnclose.Location = New System.Drawing.Point(794, 8)
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
        Me.UcWeighing1.Size = New System.Drawing.Size(879, 64)
        Me.UcWeighing1.TabIndex = 1
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(241, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 20)
        Me.btnPrint.TabIndex = 4
        Me.btnPrint.Text = "Print"
        '
        'FrmWeighmentEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(879, 486)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.UcWeighing1)
        Me.Name = "FrmWeighmentEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmWeighmentEntry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblWeighment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGateEntryNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPendingGrossWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQCStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWeighmentdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblTankerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.lblTankerNoValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTareWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNetWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGrossWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gvItemBulk.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItemBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
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
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents fndGateEntryNo As common.UserControls.txtFinder
    Friend WithEvents lblTankerNoValue As common.Controls.MyLabel
    Friend WithEvents lblLocationCode As common.Controls.MyLabel
    Friend WithEvents LblLocationName As common.Controls.MyLabel
    Friend WithEvents LblTankerName As common.Controls.MyLabel
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents lblQCStatus As common.Controls.MyLabel
    Friend WithEvents FndTankerNo As common.UserControls.txtFinder
    Friend WithEvents chkPendingGrossWeight As common.Controls.MyCheckBox
    Friend WithEvents lblGateEntryNo As common.Controls.MyLabel
    Friend WithEvents UcWeighing1 As XpertERPEngine.ucWeighing
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvItemBulk As common.UserControls.MyRadGridView
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
End Class

