<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLoadingTanker
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
        Me.LblWeighmentCode = New common.Controls.MyLabel()
        Me.FndTankerNo = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.LblAvailableQty = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.LblItemName = New common.Controls.MyLabel()
        Me.LblSiloName = New common.Controls.MyLabel()
        Me.FndItemCode = New common.UserControls.txtFinder()
        Me.FndSiloNo = New common.UserControls.txtFinder()
        Me.LblLocationName = New common.Controls.MyLabel()
        Me.LblTankerName = New common.Controls.MyLabel()
        Me.lblLocationCode = New common.Controls.MyLabel()
        Me.lblTankerNoValue = New common.Controls.MyLabel()
        Me.fndWeighmentEntryNo = New common.UserControls.txtFinder()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtQuantity = New common.MyNumBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtLoadingdate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblLoadingTanker = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.fndLoadingcode = New common.UserControls.txtNavigator()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvItem = New common.UserControls.MyRadGridView()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.LblWeighmentCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblAvailableQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblSiloName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblTankerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNoValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLoadingdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLoadingTanker, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1084, 511)
        Me.SplitContainer1.SplitterDistance = 470
        Me.SplitContainer1.TabIndex = 0
        '
        'LblWeighmentCode
        '
        Me.LblWeighmentCode.AutoSize = False
        Me.LblWeighmentCode.BorderVisible = True
        Me.LblWeighmentCode.FieldName = Nothing
        Me.LblWeighmentCode.Location = New System.Drawing.Point(90, 74)
        Me.LblWeighmentCode.Name = "LblWeighmentCode"
        Me.LblWeighmentCode.Size = New System.Drawing.Size(166, 19)
        Me.LblWeighmentCode.TabIndex = 298
        Me.LblWeighmentCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.FndTankerNo.Location = New System.Drawing.Point(90, 51)
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
        Me.FndTankerNo.Size = New System.Drawing.Size(166, 19)
        Me.FndTankerNo.TabIndex = 4
        Me.FndTankerNo.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(374, 9)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(92, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 2
        '
        'LblAvailableQty
        '
        Me.LblAvailableQty.AutoSize = False
        Me.LblAvailableQty.BorderVisible = True
        Me.LblAvailableQty.FieldName = Nothing
        Me.LblAvailableQty.Location = New System.Drawing.Point(339, 159)
        Me.LblAvailableQty.Name = "LblAvailableQty"
        Me.LblAvailableQty.Size = New System.Drawing.Size(96, 19)
        Me.LblAvailableQty.TabIndex = 15
        Me.LblAvailableQty.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblAvailableQty.Visible = False
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(262, 160)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(73, 16)
        Me.MyLabel8.TabIndex = 14
        Me.MyLabel8.Text = "Available Qty"
        Me.MyLabel8.Visible = False
        '
        'LblItemName
        '
        Me.LblItemName.AutoSize = False
        Me.LblItemName.BorderVisible = True
        Me.LblItemName.FieldName = Nothing
        Me.LblItemName.Location = New System.Drawing.Point(262, 137)
        Me.LblItemName.Name = "LblItemName"
        Me.LblItemName.Size = New System.Drawing.Size(173, 19)
        Me.LblItemName.TabIndex = 12
        Me.LblItemName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblSiloName
        '
        Me.LblSiloName.AutoSize = False
        Me.LblSiloName.BorderVisible = True
        Me.LblSiloName.FieldName = Nothing
        Me.LblSiloName.Location = New System.Drawing.Point(262, 115)
        Me.LblSiloName.Name = "LblSiloName"
        Me.LblSiloName.Size = New System.Drawing.Size(173, 19)
        Me.LblSiloName.TabIndex = 10
        Me.LblSiloName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FndItemCode
        '
        Me.FndItemCode.CalculationExpression = Nothing
        Me.FndItemCode.FieldCode = Nothing
        Me.FndItemCode.FieldDesc = Nothing
        Me.FndItemCode.FieldMaxLength = 0
        Me.FndItemCode.FieldName = Nothing
        Me.FndItemCode.isCalculatedField = False
        Me.FndItemCode.IsSourceFromTable = False
        Me.FndItemCode.IsSourceFromValueList = False
        Me.FndItemCode.IsUnique = False
        Me.FndItemCode.Location = New System.Drawing.Point(90, 137)
        Me.FndItemCode.MendatroryField = True
        Me.FndItemCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndItemCode.MyLinkLable1 = Nothing
        Me.FndItemCode.MyLinkLable2 = Nothing
        Me.FndItemCode.MyReadOnly = False
        Me.FndItemCode.MyShowMasterFormButton = False
        Me.FndItemCode.Name = "FndItemCode"
        Me.FndItemCode.ReferenceFieldDesc = Nothing
        Me.FndItemCode.ReferenceFieldName = Nothing
        Me.FndItemCode.ReferenceTableName = Nothing
        Me.FndItemCode.Size = New System.Drawing.Size(166, 19)
        Me.FndItemCode.TabIndex = 11
        Me.FndItemCode.Value = ""
        '
        'FndSiloNo
        '
        Me.FndSiloNo.CalculationExpression = Nothing
        Me.FndSiloNo.FieldCode = Nothing
        Me.FndSiloNo.FieldDesc = Nothing
        Me.FndSiloNo.FieldMaxLength = 0
        Me.FndSiloNo.FieldName = Nothing
        Me.FndSiloNo.isCalculatedField = False
        Me.FndSiloNo.IsSourceFromTable = False
        Me.FndSiloNo.IsSourceFromValueList = False
        Me.FndSiloNo.IsUnique = False
        Me.FndSiloNo.Location = New System.Drawing.Point(90, 115)
        Me.FndSiloNo.MendatroryField = True
        Me.FndSiloNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndSiloNo.MyLinkLable1 = Nothing
        Me.FndSiloNo.MyLinkLable2 = Nothing
        Me.FndSiloNo.MyReadOnly = False
        Me.FndSiloNo.MyShowMasterFormButton = False
        Me.FndSiloNo.Name = "FndSiloNo"
        Me.FndSiloNo.ReferenceFieldDesc = Nothing
        Me.FndSiloNo.ReferenceFieldName = Nothing
        Me.FndSiloNo.ReferenceTableName = Nothing
        Me.FndSiloNo.Size = New System.Drawing.Size(166, 19)
        Me.FndSiloNo.TabIndex = 9
        Me.FndSiloNo.Value = ""
        '
        'LblLocationName
        '
        Me.LblLocationName.AutoSize = False
        Me.LblLocationName.BorderVisible = True
        Me.LblLocationName.FieldName = Nothing
        Me.LblLocationName.Location = New System.Drawing.Point(262, 94)
        Me.LblLocationName.Name = "LblLocationName"
        Me.LblLocationName.Size = New System.Drawing.Size(173, 19)
        Me.LblLocationName.TabIndex = 8
        Me.LblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTankerName
        '
        Me.LblTankerName.AutoSize = False
        Me.LblTankerName.BorderVisible = True
        Me.LblTankerName.FieldName = Nothing
        Me.LblTankerName.Location = New System.Drawing.Point(262, 73)
        Me.LblTankerName.Name = "LblTankerName"
        Me.LblTankerName.Size = New System.Drawing.Size(173, 19)
        Me.LblTankerName.TabIndex = 6
        Me.LblTankerName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTankerName.Visible = False
        '
        'lblLocationCode
        '
        Me.lblLocationCode.AutoSize = False
        Me.lblLocationCode.BorderVisible = True
        Me.lblLocationCode.FieldName = Nothing
        Me.lblLocationCode.Location = New System.Drawing.Point(90, 94)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(166, 19)
        Me.lblLocationCode.TabIndex = 7
        Me.lblLocationCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTankerNoValue
        '
        Me.lblTankerNoValue.AutoSize = False
        Me.lblTankerNoValue.BorderVisible = True
        Me.lblTankerNoValue.FieldName = Nothing
        Me.lblTankerNoValue.Location = New System.Drawing.Point(418, 50)
        Me.lblTankerNoValue.Name = "lblTankerNoValue"
        Me.lblTankerNoValue.Size = New System.Drawing.Size(47, 19)
        Me.lblTankerNoValue.TabIndex = 4
        Me.lblTankerNoValue.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTankerNoValue.Visible = False
        '
        'fndWeighmentEntryNo
        '
        Me.fndWeighmentEntryNo.CalculationExpression = Nothing
        Me.fndWeighmentEntryNo.Enabled = False
        Me.fndWeighmentEntryNo.FieldCode = Nothing
        Me.fndWeighmentEntryNo.FieldDesc = Nothing
        Me.fndWeighmentEntryNo.FieldMaxLength = 0
        Me.fndWeighmentEntryNo.FieldName = Nothing
        Me.fndWeighmentEntryNo.isCalculatedField = False
        Me.fndWeighmentEntryNo.IsSourceFromTable = False
        Me.fndWeighmentEntryNo.IsSourceFromValueList = False
        Me.fndWeighmentEntryNo.IsUnique = False
        Me.fndWeighmentEntryNo.Location = New System.Drawing.Point(348, 48)
        Me.fndWeighmentEntryNo.MendatroryField = True
        Me.fndWeighmentEntryNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndWeighmentEntryNo.MyLinkLable1 = Nothing
        Me.fndWeighmentEntryNo.MyLinkLable2 = Nothing
        Me.fndWeighmentEntryNo.MyReadOnly = False
        Me.fndWeighmentEntryNo.MyShowMasterFormButton = False
        Me.fndWeighmentEntryNo.Name = "fndWeighmentEntryNo"
        Me.fndWeighmentEntryNo.ReferenceFieldDesc = Nothing
        Me.fndWeighmentEntryNo.ReferenceFieldName = Nothing
        Me.fndWeighmentEntryNo.ReferenceTableName = Nothing
        Me.fndWeighmentEntryNo.Size = New System.Drawing.Size(64, 19)
        Me.fndWeighmentEntryNo.TabIndex = 5
        Me.fndWeighmentEntryNo.Value = ""
        Me.fndWeighmentEntryNo.Visible = False
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(8, 138)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel7.TabIndex = 297
        Me.MyLabel7.Text = "Item Code"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(8, 95)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel5.TabIndex = 288
        Me.MyLabel5.Text = "Location"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(8, 55)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(59, 16)
        Me.MyLabel4.TabIndex = 296
        Me.MyLabel4.Text = "Tanker No"
        '
        'txtQuantity
        '
        Me.txtQuantity.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtQuantity.CalculationExpression = Nothing
        Me.txtQuantity.DecimalPlaces = 2
        Me.txtQuantity.FieldCode = Nothing
        Me.txtQuantity.FieldDesc = Nothing
        Me.txtQuantity.FieldMaxLength = 0
        Me.txtQuantity.FieldName = Nothing
        Me.txtQuantity.isCalculatedField = False
        Me.txtQuantity.IsSourceFromTable = False
        Me.txtQuantity.IsSourceFromValueList = False
        Me.txtQuantity.IsUnique = False
        Me.txtQuantity.Location = New System.Drawing.Point(90, 159)
        Me.txtQuantity.MendatroryField = True
        Me.txtQuantity.MyLinkLable1 = Nothing
        Me.txtQuantity.MyLinkLable2 = Nothing
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.ReferenceFieldDesc = Nothing
        Me.txtQuantity.ReferenceFieldName = Nothing
        Me.txtQuantity.ReferenceTableName = Nothing
        Me.txtQuantity.Size = New System.Drawing.Size(166, 20)
        Me.txtQuantity.TabIndex = 13
        Me.txtQuantity.Text = "0"
        Me.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtQuantity.Value = 0.0R
        Me.txtQuantity.Visible = False
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(8, 162)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel6.TabIndex = 295
        Me.MyLabel6.Text = "Quantity"
        Me.MyLabel6.Visible = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(8, 116)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel3.TabIndex = 294
        Me.MyLabel3.Text = "Silo No"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(8, 31)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel2.TabIndex = 290
        Me.MyLabel2.Text = "Loading Date"
        '
        'txtLoadingdate
        '
        Me.txtLoadingdate.CalculationExpression = Nothing
        Me.txtLoadingdate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtLoadingdate.FieldCode = Nothing
        Me.txtLoadingdate.FieldDesc = Nothing
        Me.txtLoadingdate.FieldMaxLength = 0
        Me.txtLoadingdate.FieldName = Nothing
        Me.txtLoadingdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoadingdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtLoadingdate.isCalculatedField = False
        Me.txtLoadingdate.IsSourceFromTable = False
        Me.txtLoadingdate.IsSourceFromValueList = False
        Me.txtLoadingdate.IsUnique = False
        Me.txtLoadingdate.Location = New System.Drawing.Point(90, 30)
        Me.txtLoadingdate.MendatroryField = True
        Me.txtLoadingdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtLoadingdate.MyLinkLable1 = Me.MyLabel2
        Me.txtLoadingdate.MyLinkLable2 = Nothing
        Me.txtLoadingdate.Name = "txtLoadingdate"
        Me.txtLoadingdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtLoadingdate.ReferenceFieldDesc = Nothing
        Me.txtLoadingdate.ReferenceFieldName = Nothing
        Me.txtLoadingdate.ReferenceTableName = Nothing
        Me.txtLoadingdate.Size = New System.Drawing.Size(166, 18)
        Me.txtLoadingdate.TabIndex = 3
        Me.txtLoadingdate.TabStop = False
        Me.txtLoadingdate.Text = "13/06/2011 11:29 AM"
        Me.txtLoadingdate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(8, 74)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(81, 16)
        Me.MyLabel1.TabIndex = 287
        Me.MyLabel1.Text = "Weighment No"
        '
        'lblLoadingTanker
        '
        Me.lblLoadingTanker.FieldName = Nothing
        Me.lblLoadingTanker.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblLoadingTanker.Location = New System.Drawing.Point(8, 8)
        Me.lblLoadingTanker.Name = "lblLoadingTanker"
        Me.lblLoadingTanker.Size = New System.Drawing.Size(64, 16)
        Me.lblLoadingTanker.TabIndex = 286
        Me.lblLoadingTanker.Text = "Loading No"
        '
        'btnnew
        '
        Me.btnnew.Image = XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(318, 7)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 1
        '
        'fndLoadingcode
        '
        Me.fndLoadingcode.FieldName = Nothing
        Me.fndLoadingcode.Location = New System.Drawing.Point(90, 6)
        Me.fndLoadingcode.MendatroryField = True
        Me.fndLoadingcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndLoadingcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndLoadingcode.MyLinkLable1 = Me.lblLoadingTanker
        Me.fndLoadingcode.MyLinkLable2 = Nothing
        Me.fndLoadingcode.MyMaxLength = 32767
        Me.fndLoadingcode.MyReadOnly = False
        Me.fndLoadingcode.Name = "fndLoadingcode"
        Me.fndLoadingcode.Size = New System.Drawing.Size(227, 21)
        Me.fndLoadingcode.TabIndex = 0
        Me.fndLoadingcode.Value = ""
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(86, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(1008, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(163, 8)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(9, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLoadingTanker)
        Me.SplitContainer2.Panel1.Controls.Add(Me.LblWeighmentCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndLoadingcode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.FndTankerNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.LblAvailableQty)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtLoadingdate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.LblItemName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.LblSiloName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.FndItemCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtQuantity)
        Me.SplitContainer2.Panel1.Controls.Add(Me.FndSiloNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.LblLocationName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.LblTankerName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocationCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndWeighmentEntryNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblTankerNoValue)
        Me.SplitContainer2.Panel1.Padding = New System.Windows.Forms.Padding(2)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(2)
        Me.SplitContainer2.Size = New System.Drawing.Size(1084, 470)
        Me.SplitContainer2.SplitterDistance = 184
        Me.SplitContainer2.TabIndex = 299
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gvItem)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = "Item Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(2, 2)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(1080, 278)
        Me.RadGroupBox1.TabIndex = 1
        Me.RadGroupBox1.Text = "Item Details"
        '
        'gvItem
        '
        Me.gvItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvItem.Location = New System.Drawing.Point(2, 18)
        '
        'gvItem
        '
        Me.gvItem.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvItem.Name = "gvItem"
        Me.gvItem.ShowHeaderCellButtons = True
        Me.gvItem.Size = New System.Drawing.Size(1076, 258)
        Me.gvItem.TabIndex = 0
        Me.gvItem.Text = "RadGridView1"
        '
        'FrmLoadingTanker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1084, 511)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmLoadingTanker"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmLoadingTanker"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.LblWeighmentCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblAvailableQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblSiloName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblTankerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNoValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLoadingdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLoadingTanker, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents LblLocationName As common.Controls.MyLabel
    Friend WithEvents LblTankerName As common.Controls.MyLabel
    Friend WithEvents lblLocationCode As common.Controls.MyLabel
    Friend WithEvents lblTankerNoValue As common.Controls.MyLabel
    Friend WithEvents fndWeighmentEntryNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtQuantity As common.MyNumBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtLoadingdate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblLoadingTanker As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndLoadingcode As common.UserControls.txtNavigator
    Friend WithEvents LblItemName As common.Controls.MyLabel
    Friend WithEvents LblSiloName As common.Controls.MyLabel
    Friend WithEvents FndItemCode As common.UserControls.txtFinder
    Friend WithEvents FndSiloNo As common.UserControls.txtFinder
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents LblAvailableQty As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents FndTankerNo As common.UserControls.txtFinder
    Friend WithEvents LblWeighmentCode As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvItem As common.UserControls.MyRadGridView
End Class

