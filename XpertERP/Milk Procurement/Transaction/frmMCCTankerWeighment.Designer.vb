<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMCCTankerWeighment
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
        Me.lblTransporterCode = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.lblTransporterName = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.ucStatusGros = New common.usLock()
        Me.lblGateEntryNo = New common.Controls.MyLabel()
        Me.txtTankerNo = New common.UserControls.txtFinder()
        Me.ucStatusTare = New common.usLock()
        Me.LblLocationName = New common.Controls.MyLabel()
        Me.lblLocationCode = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.TxtTareWeight = New common.MyNumBox()
        Me.txtNetWeight = New common.MyNumBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtGrossWeight = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtDocumentDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblWeighment = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.txtDocumentNo = New common.UserControls.txtNavigator()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.UcWeighing1 = New XpertERPEngine.ucWeighing()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblTransporterCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGateEntryNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTareWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNetWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGrossWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWeighment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTransporterCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTransporterName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel12)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ucStatusGros)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblGateEntryNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTankerNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ucStatusTare)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblLocationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtTareWeight)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtNetWeight)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtGrossWeight)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDocumentDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblWeighment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDocumentNo)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(576, 268)
        Me.SplitContainer1.SplitterDistance = 224
        Me.SplitContainer1.TabIndex = 0
        '
        'lblTransporterCode
        '
        Me.lblTransporterCode.AutoSize = False
        Me.lblTransporterCode.BorderVisible = True
        Me.lblTransporterCode.FieldName = Nothing
        Me.lblTransporterCode.Location = New System.Drawing.Point(95, 72)
        Me.lblTransporterCode.Name = "lblTransporterCode"
        Me.lblTransporterCode.Size = New System.Drawing.Size(190, 19)
        Me.lblTransporterCode.TabIndex = 53
        Me.lblTransporterCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(358, 32)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(110, 16)
        Me.MyLabel9.TabIndex = 53
        Me.MyLabel9.Text = "Gross Weight Status"
        '
        'lblTransporterName
        '
        Me.lblTransporterName.AutoSize = False
        Me.lblTransporterName.BorderVisible = True
        Me.lblTransporterName.FieldName = Nothing
        Me.lblTransporterName.Location = New System.Drawing.Point(285, 72)
        Me.lblTransporterName.Name = "lblTransporterName"
        Me.lblTransporterName.Size = New System.Drawing.Size(279, 19)
        Me.lblTransporterName.TabIndex = 51
        Me.lblTransporterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel12.Location = New System.Drawing.Point(7, 73)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel12.TabIndex = 52
        Me.MyLabel12.Text = "Transporter"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(358, 10)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(103, 16)
        Me.MyLabel8.TabIndex = 52
        Me.MyLabel8.Text = "Tare Weight Status"
        '
        'ucStatusGros
        '
        Me.ucStatusGros.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ucStatusGros.Location = New System.Drawing.Point(472, 30)
        Me.ucStatusGros.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ucStatusGros.Name = "ucStatusGros"
        Me.ucStatusGros.Size = New System.Drawing.Size(92, 20)
        Me.ucStatusGros.Status = common.ERPTransactionStatus.Pending
        Me.ucStatusGros.TabIndex = 51
        '
        'lblGateEntryNo
        '
        Me.lblGateEntryNo.AutoSize = False
        Me.lblGateEntryNo.BorderVisible = True
        Me.lblGateEntryNo.FieldName = Nothing
        Me.lblGateEntryNo.Location = New System.Drawing.Point(95, 93)
        Me.lblGateEntryNo.Name = "lblGateEntryNo"
        Me.lblGateEntryNo.Size = New System.Drawing.Size(190, 19)
        Me.lblGateEntryNo.TabIndex = 50
        Me.lblGateEntryNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.txtTankerNo.Location = New System.Drawing.Point(95, 51)
        Me.txtTankerNo.MendatroryField = True
        Me.txtTankerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTankerNo.MyLinkLable1 = Nothing
        Me.txtTankerNo.MyLinkLable2 = Nothing
        Me.txtTankerNo.MyReadOnly = False
        Me.txtTankerNo.MyShowMasterFormButton = False
        Me.txtTankerNo.Name = "txtTankerNo"
        Me.txtTankerNo.ReferenceFieldDesc = Nothing
        Me.txtTankerNo.ReferenceFieldName = Nothing
        Me.txtTankerNo.ReferenceTableName = Nothing
        Me.txtTankerNo.Size = New System.Drawing.Size(190, 19)
        Me.txtTankerNo.TabIndex = 4
        Me.txtTankerNo.Value = ""
        '
        'ucStatusTare
        '
        Me.ucStatusTare.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ucStatusTare.Location = New System.Drawing.Point(472, 8)
        Me.ucStatusTare.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ucStatusTare.Name = "ucStatusTare"
        Me.ucStatusTare.Size = New System.Drawing.Size(92, 20)
        Me.ucStatusTare.Status = common.ERPTransactionStatus.Pending
        Me.ucStatusTare.TabIndex = 48
        '
        'LblLocationName
        '
        Me.LblLocationName.AutoSize = False
        Me.LblLocationName.BorderVisible = True
        Me.LblLocationName.FieldName = Nothing
        Me.LblLocationName.Location = New System.Drawing.Point(285, 114)
        Me.LblLocationName.Name = "LblLocationName"
        Me.LblLocationName.Size = New System.Drawing.Size(279, 19)
        Me.LblLocationName.TabIndex = 8
        Me.LblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLocationCode
        '
        Me.lblLocationCode.AutoSize = False
        Me.lblLocationCode.BorderVisible = True
        Me.lblLocationCode.FieldName = Nothing
        Me.lblLocationCode.Location = New System.Drawing.Point(95, 114)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(190, 19)
        Me.lblLocationCode.TabIndex = 7
        Me.lblLocationCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(7, 159)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel7.TabIndex = 47
        Me.MyLabel7.Text = "Gross Weight"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(7, 115)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel5.TabIndex = 21
        Me.MyLabel5.Text = "Location"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(7, 52)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(59, 16)
        Me.MyLabel4.TabIndex = 46
        Me.MyLabel4.Text = "Tanker No"
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
        Me.TxtTareWeight.Location = New System.Drawing.Point(95, 135)
        Me.TxtTareWeight.MendatroryField = True
        Me.TxtTareWeight.MyLinkLable1 = Nothing
        Me.TxtTareWeight.MyLinkLable2 = Nothing
        Me.TxtTareWeight.Name = "TxtTareWeight"
        Me.TxtTareWeight.ReadOnly = True
        Me.TxtTareWeight.ReferenceFieldDesc = Nothing
        Me.TxtTareWeight.ReferenceFieldName = Nothing
        Me.TxtTareWeight.ReferenceTableName = Nothing
        Me.TxtTareWeight.Size = New System.Drawing.Size(190, 20)
        Me.TxtTareWeight.TabIndex = 9
        Me.TxtTareWeight.Text = "0"
        Me.TxtTareWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtTareWeight.Value = 0.0R
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
        Me.txtNetWeight.Location = New System.Drawing.Point(95, 179)
        Me.txtNetWeight.MendatroryField = True
        Me.txtNetWeight.MyLinkLable1 = Nothing
        Me.txtNetWeight.MyLinkLable2 = Nothing
        Me.txtNetWeight.Name = "txtNetWeight"
        Me.txtNetWeight.ReadOnly = True
        Me.txtNetWeight.ReferenceFieldDesc = Nothing
        Me.txtNetWeight.ReferenceFieldName = Nothing
        Me.txtNetWeight.ReferenceTableName = Nothing
        Me.txtNetWeight.Size = New System.Drawing.Size(190, 20)
        Me.txtNetWeight.TabIndex = 11
        Me.txtNetWeight.Text = "0"
        Me.txtNetWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNetWeight.Value = 0.0R
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(7, 181)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel6.TabIndex = 45
        Me.MyLabel6.Text = "Net Weight"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(7, 137)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel3.TabIndex = 44
        Me.MyLabel3.Text = "Tare Weight"
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
        Me.txtGrossWeight.Location = New System.Drawing.Point(95, 157)
        Me.txtGrossWeight.MendatroryField = True
        Me.txtGrossWeight.MyLinkLable1 = Nothing
        Me.txtGrossWeight.MyLinkLable2 = Nothing
        Me.txtGrossWeight.Name = "txtGrossWeight"
        Me.txtGrossWeight.ReadOnly = True
        Me.txtGrossWeight.ReferenceFieldDesc = Nothing
        Me.txtGrossWeight.ReferenceFieldName = Nothing
        Me.txtGrossWeight.ReferenceTableName = Nothing
        Me.txtGrossWeight.Size = New System.Drawing.Size(190, 20)
        Me.txtGrossWeight.TabIndex = 10
        Me.txtGrossWeight.Text = "0"
        Me.txtGrossWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtGrossWeight.Value = 0.0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(7, 32)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel2.TabIndex = 22
        Me.MyLabel2.Text = "Document Date"
        '
        'txtDocumentDate
        '
        Me.txtDocumentDate.CalculationExpression = Nothing
        Me.txtDocumentDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtDocumentDate.FieldCode = Nothing
        Me.txtDocumentDate.FieldDesc = Nothing
        Me.txtDocumentDate.FieldMaxLength = 0
        Me.txtDocumentDate.FieldName = Nothing
        Me.txtDocumentDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocumentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDocumentDate.isCalculatedField = False
        Me.txtDocumentDate.IsSourceFromTable = False
        Me.txtDocumentDate.IsSourceFromValueList = False
        Me.txtDocumentDate.IsUnique = False
        Me.txtDocumentDate.Location = New System.Drawing.Point(95, 31)
        Me.txtDocumentDate.MendatroryField = True
        Me.txtDocumentDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDocumentDate.MyLinkLable1 = Me.MyLabel2
        Me.txtDocumentDate.MyLinkLable2 = Nothing
        Me.txtDocumentDate.Name = "txtDocumentDate"
        Me.txtDocumentDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDocumentDate.ReferenceFieldDesc = Nothing
        Me.txtDocumentDate.ReferenceFieldName = Nothing
        Me.txtDocumentDate.ReferenceTableName = Nothing
        Me.txtDocumentDate.Size = New System.Drawing.Size(190, 18)
        Me.txtDocumentDate.TabIndex = 2
        Me.txtDocumentDate.TabStop = False
        Me.txtDocumentDate.Text = "13/06/2011 11:29 AM"
        Me.txtDocumentDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(7, 94)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(78, 16)
        Me.MyLabel1.TabIndex = 20
        Me.MyLabel1.Text = "Gate Entry No"
        '
        'lblWeighment
        '
        Me.lblWeighment.FieldName = Nothing
        Me.lblWeighment.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblWeighment.Location = New System.Drawing.Point(7, 10)
        Me.lblWeighment.Name = "lblWeighment"
        Me.lblWeighment.Size = New System.Drawing.Size(75, 16)
        Me.lblWeighment.TabIndex = 17
        Me.lblWeighment.Text = "Document No"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(328, 8)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(19, 21)
        Me.btnnew.TabIndex = 1
        '
        'txtDocumentNo
        '
        Me.txtDocumentNo.FieldName = Nothing
        Me.txtDocumentNo.Location = New System.Drawing.Point(95, 8)
        Me.txtDocumentNo.MendatroryField = True
        Me.txtDocumentNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDocumentNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocumentNo.MyLinkLable1 = Me.lblWeighment
        Me.txtDocumentNo.MyLinkLable2 = Nothing
        Me.txtDocumentNo.MyMaxLength = 32767
        Me.txtDocumentNo.MyReadOnly = False
        Me.txtDocumentNo.Name = "txtDocumentNo"
        Me.txtDocumentNo.Size = New System.Drawing.Size(233, 21)
        Me.txtDocumentNo.TabIndex = 0
        Me.txtDocumentNo.Value = ""
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(84, 10)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(491, 10)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(162, 10)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(7, 10)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'UcWeighing1
        '
        Me.UcWeighing1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UcWeighing1.Location = New System.Drawing.Point(0, 0)
        Me.UcWeighing1.Name = "UcWeighing1"
        Me.UcWeighing1.Size = New System.Drawing.Size(576, 64)
        Me.UcWeighing1.TabIndex = 1
        '
        'frmMCCTankerWeighment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(576, 332)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.UcWeighing1)
        Me.Name = "frmMCCTankerWeighment"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "MCC Tanker Weighment"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblTransporterCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGateEntryNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTareWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNetWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGrossWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWeighment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblWeighment As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDocumentNo As common.UserControls.txtNavigator
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtDocumentDate As common.Controls.MyDateTimePicker
    Friend WithEvents TxtTareWeight As common.MyNumBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtGrossWeight As common.MyNumBox
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblLocationCode As common.Controls.MyLabel
    Friend WithEvents LblLocationName As common.Controls.MyLabel
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents ucStatusTare As common.usLock
    Friend WithEvents txtTankerNo As common.UserControls.txtFinder
    Friend WithEvents lblGateEntryNo As common.Controls.MyLabel
    Friend WithEvents UcWeighing1 As XpertERPEngine.ucWeighing
    Friend WithEvents ucStatusGros As common.usLock
    Friend WithEvents txtNetWeight As common.MyNumBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents lblTransporterCode As common.Controls.MyLabel
    Friend WithEvents lblTransporterName As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
End Class

