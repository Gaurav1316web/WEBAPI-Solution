<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmQualityCheckBulkSale
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
        Me.LblLoadingNo = New common.Controls.MyLabel()
        Me.lblCustomerName = New common.Controls.MyLabel()
        Me.lblCustomerCode = New common.Controls.MyLabel()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.FndTankerNo = New common.UserControls.txtFinder()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvItem = New common.UserControls.MyRadGridView()
        Me.UsLock1 = New common.usLock()
        Me.LblWeighmentNo = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtCorrectionFactor = New common.MyNumBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.lblSiloNo = New common.Controls.MyLabel()
        Me.lblGateEntryNo = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.LblSiloName = New common.Controls.MyLabel()
        Me.LblLocationName = New common.Controls.MyLabel()
        Me.LblTankerName = New common.Controls.MyLabel()
        Me.lblLocationCode = New common.Controls.MyLabel()
        Me.lblTankerNoValue = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.fndLoadingNo = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtQCdate = New common.Controls.MyDateTimePicker()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.lblQcNo = New common.Controls.MyLabel()
        Me.fndQcNo = New common.UserControls.txtNavigator()
        Me.grpParameterDetailBulk = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvParam = New common.UserControls.MyRadGridView()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RDSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RDDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.LblLoadingNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblWeighmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCorrectionFactor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSiloNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGateEntryNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblSiloName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblTankerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNoValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQCdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQcNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpParameterDetailBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpParameterDetailBulk.SuspendLayout()
        CType(Me.gvParam, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvParam.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(845, 462)
        Me.SplitContainer1.SplitterDistance = 425
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.LblLoadingNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCustomerName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCustomerCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel11)
        Me.SplitContainer2.Panel1.Controls.Add(Me.FndTankerNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.LblWeighmentNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCorrectionFactor)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblSiloNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblGateEntryNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.LblSiloName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.LblLocationName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.LblTankerName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocationCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblTankerNoValue)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndLoadingNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtQCdate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblQcNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndQcNo)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.grpParameterDetailBulk)
        Me.SplitContainer2.Size = New System.Drawing.Size(845, 425)
        Me.SplitContainer2.SplitterDistance = 241
        Me.SplitContainer2.TabIndex = 0
        '
        'LblLoadingNo
        '
        Me.LblLoadingNo.AutoSize = False
        Me.LblLoadingNo.BorderVisible = True
        Me.LblLoadingNo.Location = New System.Drawing.Point(109, 73)
        Me.LblLoadingNo.Name = "LblLoadingNo"
        Me.LblLoadingNo.Size = New System.Drawing.Size(170, 19)
        Me.LblLoadingNo.TabIndex = 327
        Me.LblLoadingNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = False
        Me.lblCustomerName.BorderVisible = True
        Me.lblCustomerName.Location = New System.Drawing.Point(281, 138)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(210, 19)
        Me.lblCustomerName.TabIndex = 325
        Me.lblCustomerName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCustomerCode
        '
        Me.lblCustomerCode.AutoSize = False
        Me.lblCustomerCode.BorderVisible = True
        Me.lblCustomerCode.Location = New System.Drawing.Point(109, 138)
        Me.lblCustomerCode.Name = "lblCustomerCode"
        Me.lblCustomerCode.Size = New System.Drawing.Size(170, 19)
        Me.lblCustomerCode.TabIndex = 324
        Me.lblCustomerCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel11
        '
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel11.Location = New System.Drawing.Point(9, 139)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel11.TabIndex = 326
        Me.MyLabel11.Text = "Customer"
        '
        'FndTankerNo
        '
        Me.FndTankerNo.Location = New System.Drawing.Point(109, 50)
        Me.FndTankerNo.MendatroryField = True
        Me.FndTankerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndTankerNo.MyLinkLable1 = Nothing
        Me.FndTankerNo.MyLinkLable2 = Nothing
        Me.FndTankerNo.MyReadOnly = False
        Me.FndTankerNo.MyShowMasterFormButton = False
        Me.FndTankerNo.Name = "FndTankerNo"
        Me.FndTankerNo.Size = New System.Drawing.Size(170, 19)
        Me.FndTankerNo.TabIndex = 323
        Me.FndTankerNo.Value = ""
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gvItem)
        Me.RadGroupBox1.HeaderText = "Item Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(792, 9)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(41, 62)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "Item Details"
        Me.RadGroupBox1.Visible = False
        '
        'gvItem
        '
        Me.gvItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvItem.Location = New System.Drawing.Point(2, 18)
        Me.gvItem.Name = "gvItem"
        Me.gvItem.Size = New System.Drawing.Size(37, 42)
        Me.gvItem.TabIndex = 0
        Me.gvItem.Text = "RadGridView1"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(423, 7)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(92, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 322
        '
        'LblWeighmentNo
        '
        Me.LblWeighmentNo.AutoSize = False
        Me.LblWeighmentNo.BorderVisible = True
        Me.LblWeighmentNo.Location = New System.Drawing.Point(109, 94)
        Me.LblWeighmentNo.Name = "LblWeighmentNo"
        Me.LblWeighmentNo.Size = New System.Drawing.Size(170, 19)
        Me.LblWeighmentNo.TabIndex = 320
        Me.LblWeighmentNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel9
        '
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(9, 95)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(81, 16)
        Me.MyLabel9.TabIndex = 321
        Me.MyLabel9.Text = "Weighment No"
        '
        'txtCorrectionFactor
        '
        Me.txtCorrectionFactor.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCorrectionFactor.DecimalPlaces = 2
        Me.txtCorrectionFactor.Location = New System.Drawing.Point(109, 204)
        Me.txtCorrectionFactor.MendatroryField = True
        Me.txtCorrectionFactor.MyLinkLable1 = Nothing
        Me.txtCorrectionFactor.MyLinkLable2 = Nothing
        Me.txtCorrectionFactor.Name = "txtCorrectionFactor"
        Me.txtCorrectionFactor.Size = New System.Drawing.Size(170, 20)
        Me.txtCorrectionFactor.TabIndex = 4
        Me.txtCorrectionFactor.Text = "0"
        Me.txtCorrectionFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCorrectionFactor.Value = 0.0R
        '
        'MyLabel6
        '
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(9, 206)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel6.TabIndex = 319
        Me.MyLabel6.Text = "Correction Factor"
        '
        'lblSiloNo
        '
        Me.lblSiloNo.AutoSize = False
        Me.lblSiloNo.BorderVisible = True
        Me.lblSiloNo.Location = New System.Drawing.Point(109, 182)
        Me.lblSiloNo.Name = "lblSiloNo"
        Me.lblSiloNo.Size = New System.Drawing.Size(170, 19)
        Me.lblSiloNo.TabIndex = 9
        Me.lblSiloNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblGateEntryNo
        '
        Me.lblGateEntryNo.AutoSize = False
        Me.lblGateEntryNo.BorderVisible = True
        Me.lblGateEntryNo.Location = New System.Drawing.Point(109, 116)
        Me.lblGateEntryNo.Name = "lblGateEntryNo"
        Me.lblGateEntryNo.Size = New System.Drawing.Size(170, 19)
        Me.lblGateEntryNo.TabIndex = 4
        Me.lblGateEntryNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(9, 117)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(78, 16)
        Me.MyLabel7.TabIndex = 315
        Me.MyLabel7.Text = "Gate Entry No"
        '
        'LblSiloName
        '
        Me.LblSiloName.AutoSize = False
        Me.LblSiloName.BorderVisible = True
        Me.LblSiloName.Location = New System.Drawing.Point(281, 182)
        Me.LblSiloName.Name = "LblSiloName"
        Me.LblSiloName.Size = New System.Drawing.Size(210, 19)
        Me.LblSiloName.TabIndex = 10
        Me.LblSiloName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblLocationName
        '
        Me.LblLocationName.AutoSize = False
        Me.LblLocationName.BorderVisible = True
        Me.LblLocationName.Location = New System.Drawing.Point(281, 160)
        Me.LblLocationName.Name = "LblLocationName"
        Me.LblLocationName.Size = New System.Drawing.Size(210, 19)
        Me.LblLocationName.TabIndex = 8
        Me.LblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTankerName
        '
        Me.LblTankerName.AutoSize = False
        Me.LblTankerName.BorderVisible = True
        Me.LblTankerName.Location = New System.Drawing.Point(281, 116)
        Me.LblTankerName.Name = "LblTankerName"
        Me.LblTankerName.Size = New System.Drawing.Size(210, 19)
        Me.LblTankerName.TabIndex = 6
        Me.LblTankerName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTankerName.Visible = False
        '
        'lblLocationCode
        '
        Me.lblLocationCode.AutoSize = False
        Me.lblLocationCode.BorderVisible = True
        Me.lblLocationCode.Location = New System.Drawing.Point(109, 160)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(170, 19)
        Me.lblLocationCode.TabIndex = 7
        Me.lblLocationCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTankerNoValue
        '
        Me.lblTankerNoValue.AutoSize = False
        Me.lblTankerNoValue.BorderVisible = True
        Me.lblTankerNoValue.Location = New System.Drawing.Point(404, 76)
        Me.lblTankerNoValue.Name = "lblTankerNoValue"
        Me.lblTankerNoValue.Size = New System.Drawing.Size(125, 19)
        Me.lblTankerNoValue.TabIndex = 5
        Me.lblTankerNoValue.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTankerNoValue.Visible = False
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(9, 161)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel5.TabIndex = 306
        Me.MyLabel5.Text = "Location"
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(9, 51)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(59, 16)
        Me.MyLabel4.TabIndex = 308
        Me.MyLabel4.Text = "Tanker No"
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(9, 183)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel3.TabIndex = 307
        Me.MyLabel3.Text = "Silo No"
        '
        'fndLoadingNo
        '
        Me.fndLoadingNo.Enabled = False
        Me.fndLoadingNo.Location = New System.Drawing.Point(404, 48)
        Me.fndLoadingNo.MendatroryField = True
        Me.fndLoadingNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLoadingNo.MyLinkLable1 = Nothing
        Me.fndLoadingNo.MyLinkLable2 = Nothing
        Me.fndLoadingNo.MyReadOnly = False
        Me.fndLoadingNo.MyShowMasterFormButton = False
        Me.fndLoadingNo.Name = "fndLoadingNo"
        Me.fndLoadingNo.Size = New System.Drawing.Size(125, 19)
        Me.fndLoadingNo.TabIndex = 3
        Me.fndLoadingNo.Value = ""
        Me.fndLoadingNo.Visible = False
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(9, 73)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(64, 16)
        Me.MyLabel1.TabIndex = 299
        Me.MyLabel1.Text = "Loading No"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(9, 30)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(50, 16)
        Me.MyLabel2.TabIndex = 292
        Me.MyLabel2.Text = "QC Date"
        '
        'txtQCdate
        '
        Me.txtQCdate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtQCdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQCdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtQCdate.Location = New System.Drawing.Point(109, 29)
        Me.txtQCdate.MendatroryField = True
        Me.txtQCdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtQCdate.MyLinkLable1 = Me.MyLabel2
        Me.txtQCdate.MyLinkLable2 = Nothing
        Me.txtQCdate.Name = "txtQCdate"
        Me.txtQCdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtQCdate.Size = New System.Drawing.Size(170, 18)
        Me.txtQCdate.TabIndex = 2
        Me.txtQCdate.TabStop = False
        Me.txtQCdate.Text = "13/06/2011 11:29 AM"
        Me.txtQCdate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(383, 8)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(19, 18)
        Me.btnReset.TabIndex = 1
        '
        'lblQcNo
        '
        Me.lblQcNo.Location = New System.Drawing.Point(9, 8)
        Me.lblQcNo.Name = "lblQcNo"
        Me.lblQcNo.Size = New System.Drawing.Size(42, 18)
        Me.lblQcNo.TabIndex = 254
        Me.lblQcNo.Text = "QC No."
        '
        'fndQcNo
        '
        Me.fndQcNo.Location = New System.Drawing.Point(109, 8)
        Me.fndQcNo.MendatroryField = False
        Me.fndQcNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndQcNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndQcNo.MyLinkLable1 = Me.lblQcNo
        Me.fndQcNo.MyLinkLable2 = Nothing
        Me.fndQcNo.MyMaxLength = 32767
        Me.fndQcNo.MyReadOnly = False
        Me.fndQcNo.Name = "fndQcNo"
        Me.fndQcNo.Size = New System.Drawing.Size(271, 18)
        Me.fndQcNo.TabIndex = 0
        Me.fndQcNo.Value = ""
        '
        'grpParameterDetailBulk
        '
        Me.grpParameterDetailBulk.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpParameterDetailBulk.Controls.Add(Me.gvParam)
        Me.grpParameterDetailBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpParameterDetailBulk.HeaderText = "QC Parameter Details"
        Me.grpParameterDetailBulk.Location = New System.Drawing.Point(0, 0)
        Me.grpParameterDetailBulk.Name = "grpParameterDetailBulk"
        Me.grpParameterDetailBulk.Size = New System.Drawing.Size(845, 180)
        Me.grpParameterDetailBulk.TabIndex = 0
        Me.grpParameterDetailBulk.Text = "QC Parameter Details"
        '
        'gvParam
        '
        Me.gvParam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvParam.Location = New System.Drawing.Point(2, 18)
        Me.gvParam.Name = "gvParam"
        Me.gvParam.Size = New System.Drawing.Size(841, 160)
        Me.gvParam.TabIndex = 0
        Me.gvParam.Text = "RadGridView1"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(82, 7)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(766, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(161, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(3, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(845, 20)
        Me.RadMenu1.TabIndex = 10
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RDSaveLayout, Me.RDDeleteLayout})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RDSaveLayout
        '
        Me.RDSaveLayout.AccessibleDescription = "Save Layout"
        Me.RDSaveLayout.AccessibleName = "Save Layout"
        Me.RDSaveLayout.Name = "RDSaveLayout"
        Me.RDSaveLayout.Text = "Save Layout"
        '
        'RDDeleteLayout
        '
        Me.RDDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.RDDeleteLayout.AccessibleName = "Delete Layout"
        Me.RDDeleteLayout.Name = "RDDeleteLayout"
        Me.RDDeleteLayout.Text = "Delete Layout"
        '
        'FrmQualityCheckBulkSale
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(845, 482)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmQualityCheckBulkSale"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Quality Check Bulk Sale"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.LblLoadingNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblWeighmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCorrectionFactor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSiloNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGateEntryNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblSiloName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblTankerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNoValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQCdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQcNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpParameterDetailBulk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpParameterDetailBulk.ResumeLayout(False)
        CType(Me.gvParam.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvParam, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblQcNo As common.Controls.MyLabel
    Friend WithEvents fndQcNo As common.UserControls.txtNavigator
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtQCdate As common.Controls.MyDateTimePicker
    Friend WithEvents fndLoadingNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents LblSiloName As common.Controls.MyLabel
    Friend WithEvents LblLocationName As common.Controls.MyLabel
    Friend WithEvents LblTankerName As common.Controls.MyLabel
    Friend WithEvents lblLocationCode As common.Controls.MyLabel
    Friend WithEvents lblTankerNoValue As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblGateEntryNo As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvItem As common.UserControls.MyRadGridView
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblSiloNo As common.Controls.MyLabel
    Friend WithEvents txtCorrectionFactor As common.MyNumBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents grpParameterDetailBulk As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvParam As common.UserControls.MyRadGridView
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RDSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RDDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents LblWeighmentNo As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents FndTankerNo As common.UserControls.txtFinder
    Friend WithEvents lblCustomerName As common.Controls.MyLabel
    Friend WithEvents lblCustomerCode As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents LblLoadingNo As common.Controls.MyLabel
End Class

