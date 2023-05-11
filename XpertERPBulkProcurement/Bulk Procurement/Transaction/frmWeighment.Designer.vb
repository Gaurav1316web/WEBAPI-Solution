<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmWeighment
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.UcWeighing1 = New XpertERPEngine.ucWeighing()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtLineNo = New common.MyNumBox()
        Me.btnUpdateWeight = New Telerik.WinControls.UI.RadButton()
        Me.lblLineNo = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtNewNetWt = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtNewTare = New common.MyNumBox()
        Me.txtGrossNew = New common.Controls.MyLabel()
        Me.txtNewGross = New common.MyNumBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblSubLocation = New common.Controls.MyLabel()
        Me.chkJobWork = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtSubLocation = New common.UserControls.txtFinder()
        Me.lblVendorBulk = New common.Controls.MyLabel()
        Me.chkTankerReturn = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtGateEntryDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtChallanDate = New common.Controls.MyDateTimePicker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblGrossWt = New common.Controls.MyLabel()
        Me.txtGrossWt = New common.MyNumBox()
        Me.dtpTareWeight = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.chkBothDoc = New Telerik.WinControls.UI.RadCheckBox()
        Me.fndTankerNo = New common.UserControls.txtFinder()
        Me.chkPendingTare = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblVendorCodeBulk = New common.Controls.MyTextBox()
        Me.lblLocationCodeBulk = New common.Controls.MyTextBox()
        Me.lblChallanNoBulk = New common.Controls.MyTextBox()
        Me.lblTankerNoBulk = New common.Controls.MyTextBox()
        Me.txtWeighmentSlipNo = New common.Controls.MyTextBox()
        Me.lblWeighmentSlipNo = New common.Controls.MyLabel()
        Me.txtDipValue = New common.Controls.MyTextBox()
        Me.lblDipValue = New common.Controls.MyLabel()
        Me.lblPending = New common.usLock()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.grpGateEntryType = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkBulkMilkProc = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkMccProc = New Telerik.WinControls.UI.RadRadioButton()
        Me.lblStatus = New common.Controls.MyLabel()
        Me.fndDocNO = New common.UserControls.txtNavigator()
        Me.lblDocNo = New common.Controls.MyLabel()
        Me.dtpWeighmentDateBulk = New common.Controls.MyDateTimePicker()
        Me.lblTankerNo = New common.Controls.MyLabel()
        Me.fndGateEntryNoBulk = New common.UserControls.txtFinder()
        Me.lblLocationNameBulk = New common.Controls.MyLabel()
        Me.lblLocationBulk = New common.Controls.MyLabel()
        Me.lblStatusBulk = New common.Controls.MyLabel()
        Me.lblDateAndTimeBulk = New common.Controls.MyLabel()
        Me.lblChallanDate = New common.Controls.MyLabel()
        Me.lblChallanNo = New common.Controls.MyLabel()
        Me.lblVendorNameBulk = New common.Controls.MyLabel()
        Me.lblGateEntryNO = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvItemBulk = New common.UserControls.MyRadGridView()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.mnuSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuExit = New Telerik.WinControls.UI.RadMenuItem()
        Me.gvItemMcc = New Telerik.WinControls.UI.MasterGridViewTemplate()
        Me.fndRefrencesNo = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.txtLineNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpdateWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLineNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNewNetWt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNewTare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGrossNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNewGross, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.lblSubLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkJobWork, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTankerReturn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGateEntryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChallanDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.lblGrossWt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGrossWt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpTareWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBothDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPendingTare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorCodeBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationCodeBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChallanNoBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNoBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWeighmentSlipNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWeighmentSlipNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDipValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDipValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpGateEntryType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpGateEntryType.SuspendLayout()
        CType(Me.chkBulkMilkProc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMccProc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpWeighmentDateBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationNameBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStatusBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDateAndTimeBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChallanDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChallanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorNameBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGateEntryNO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gvItemBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItemBulk.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItemMcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.UcWeighing1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1137, 509)
        Me.SplitContainer1.SplitterDistance = 62
        Me.SplitContainer1.TabIndex = 1
        '
        'UcWeighing1
        '
        Me.UcWeighing1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcWeighing1.form_ID = Nothing
        Me.UcWeighing1.LiveReading = 0R
        Me.UcWeighing1.Location = New System.Drawing.Point(0, 0)
        Me.UcWeighing1.Machine = ""
        Me.UcWeighing1.Name = "UcWeighing1"
        Me.UcWeighing1.Port = ""
        Me.UcWeighing1.Size = New System.Drawing.Size(1137, 62)
        Me.UcWeighing1.TabIndex = 348
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer2.Size = New System.Drawing.Size(1137, 443)
        Me.SplitContainer2.SplitterDistance = 407
        Me.SplitContainer2.TabIndex = 1
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndRefrencesNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Panel2)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Panel3)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkTankerReturn)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtGateEntryDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtChallanDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpTareWeight)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkBothDoc)
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndTankerNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkPendingTare)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblVendorCodeBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblLocationCodeBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblChallanNoBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblTankerNoBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtWeighmentSlipNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblWeighmentSlipNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtDipValue)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblDipValue)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblPending)
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer3.Panel1.Controls.Add(Me.grpGateEntryType)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblStatus)
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndDocNO)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpWeighmentDateBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblDocNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblTankerNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndGateEntryNoBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblLocationNameBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblLocationBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblStatusBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblDateAndTimeBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblChallanDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblChallanNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblVendorNameBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblVendorBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblGateEntryNO)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer3.Size = New System.Drawing.Size(1137, 407)
        Me.SplitContainer3.SplitterDistance = 185
        Me.SplitContainer3.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtLineNo)
        Me.Panel2.Controls.Add(Me.btnUpdateWeight)
        Me.Panel2.Controls.Add(Me.lblLineNo)
        Me.Panel2.Controls.Add(Me.MyLabel5)
        Me.Panel2.Controls.Add(Me.txtNewNetWt)
        Me.Panel2.Controls.Add(Me.MyLabel4)
        Me.Panel2.Controls.Add(Me.txtNewTare)
        Me.Panel2.Controls.Add(Me.txtGrossNew)
        Me.Panel2.Controls.Add(Me.txtNewGross)
        Me.Panel2.Location = New System.Drawing.Point(939, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(188, 115)
        Me.Panel2.TabIndex = 356
        Me.Panel2.Visible = False
        '
        'txtLineNo
        '
        Me.txtLineNo.BackColor = System.Drawing.Color.White
        Me.txtLineNo.CalculationExpression = Nothing
        Me.txtLineNo.DecimalPlaces = 0
        Me.txtLineNo.FieldCode = Nothing
        Me.txtLineNo.FieldDesc = Nothing
        Me.txtLineNo.FieldMaxLength = 0
        Me.txtLineNo.FieldName = Nothing
        Me.txtLineNo.isCalculatedField = False
        Me.txtLineNo.IsSourceFromTable = False
        Me.txtLineNo.IsSourceFromValueList = False
        Me.txtLineNo.IsUnique = False
        Me.txtLineNo.Location = New System.Drawing.Point(84, 1)
        Me.txtLineNo.MendatroryField = False
        Me.txtLineNo.MyLinkLable1 = Nothing
        Me.txtLineNo.MyLinkLable2 = Nothing
        Me.txtLineNo.Name = "txtLineNo"
        Me.txtLineNo.ReferenceFieldDesc = Nothing
        Me.txtLineNo.ReferenceFieldName = Nothing
        Me.txtLineNo.ReferenceTableName = Nothing
        Me.txtLineNo.Size = New System.Drawing.Size(100, 20)
        Me.txtLineNo.TabIndex = 355
        Me.txtLineNo.Text = "0"
        Me.txtLineNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLineNo.Value = 0R
        '
        'btnUpdateWeight
        '
        Me.btnUpdateWeight.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateWeight.Location = New System.Drawing.Point(85, 93)
        Me.btnUpdateWeight.Name = "btnUpdateWeight"
        Me.btnUpdateWeight.Size = New System.Drawing.Size(99, 18)
        Me.btnUpdateWeight.TabIndex = 354
        Me.btnUpdateWeight.Text = "Update Weight"
        '
        'lblLineNo
        '
        Me.lblLineNo.FieldName = Nothing
        Me.lblLineNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLineNo.Location = New System.Drawing.Point(3, 4)
        Me.lblLineNo.Name = "lblLineNo"
        Me.lblLineNo.Size = New System.Drawing.Size(45, 16)
        Me.lblLineNo.TabIndex = 353
        Me.lblLineNo.Text = "Line No"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(4, 67)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel5.TabIndex = 351
        Me.MyLabel5.Text = "Net Weight"
        '
        'txtNewNetWt
        '
        Me.txtNewNetWt.BackColor = System.Drawing.Color.White
        Me.txtNewNetWt.CalculationExpression = Nothing
        Me.txtNewNetWt.DecimalPlaces = 2
        Me.txtNewNetWt.FieldCode = Nothing
        Me.txtNewNetWt.FieldDesc = Nothing
        Me.txtNewNetWt.FieldMaxLength = 0
        Me.txtNewNetWt.FieldName = Nothing
        Me.txtNewNetWt.isCalculatedField = False
        Me.txtNewNetWt.IsSourceFromTable = False
        Me.txtNewNetWt.IsSourceFromValueList = False
        Me.txtNewNetWt.IsUnique = False
        Me.txtNewNetWt.Location = New System.Drawing.Point(85, 67)
        Me.txtNewNetWt.MendatroryField = False
        Me.txtNewNetWt.MyLinkLable1 = Nothing
        Me.txtNewNetWt.MyLinkLable2 = Nothing
        Me.txtNewNetWt.Name = "txtNewNetWt"
        Me.txtNewNetWt.ReferenceFieldDesc = Nothing
        Me.txtNewNetWt.ReferenceFieldName = Nothing
        Me.txtNewNetWt.ReferenceTableName = Nothing
        Me.txtNewNetWt.Size = New System.Drawing.Size(100, 20)
        Me.txtNewNetWt.TabIndex = 350
        Me.txtNewNetWt.Text = "0"
        Me.txtNewNetWt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNewNetWt.Value = 0R
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(4, 46)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel4.TabIndex = 351
        Me.MyLabel4.Text = "Tare Weight"
        '
        'txtNewTare
        '
        Me.txtNewTare.BackColor = System.Drawing.Color.White
        Me.txtNewTare.CalculationExpression = Nothing
        Me.txtNewTare.DecimalPlaces = 2
        Me.txtNewTare.FieldCode = Nothing
        Me.txtNewTare.FieldDesc = Nothing
        Me.txtNewTare.FieldMaxLength = 0
        Me.txtNewTare.FieldName = Nothing
        Me.txtNewTare.isCalculatedField = False
        Me.txtNewTare.IsSourceFromTable = False
        Me.txtNewTare.IsSourceFromValueList = False
        Me.txtNewTare.IsUnique = False
        Me.txtNewTare.Location = New System.Drawing.Point(85, 46)
        Me.txtNewTare.MendatroryField = False
        Me.txtNewTare.MyLinkLable1 = Nothing
        Me.txtNewTare.MyLinkLable2 = Nothing
        Me.txtNewTare.Name = "txtNewTare"
        Me.txtNewTare.ReferenceFieldDesc = Nothing
        Me.txtNewTare.ReferenceFieldName = Nothing
        Me.txtNewTare.ReferenceTableName = Nothing
        Me.txtNewTare.Size = New System.Drawing.Size(100, 20)
        Me.txtNewTare.TabIndex = 350
        Me.txtNewTare.Text = "0"
        Me.txtNewTare.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNewTare.Value = 0R
        '
        'txtGrossNew
        '
        Me.txtGrossNew.FieldName = Nothing
        Me.txtGrossNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGrossNew.Location = New System.Drawing.Point(3, 25)
        Me.txtGrossNew.Name = "txtGrossNew"
        Me.txtGrossNew.Size = New System.Drawing.Size(75, 16)
        Me.txtGrossNew.TabIndex = 349
        Me.txtGrossNew.Text = "Gross Weight"
        '
        'txtNewGross
        '
        Me.txtNewGross.BackColor = System.Drawing.Color.White
        Me.txtNewGross.CalculationExpression = Nothing
        Me.txtNewGross.DecimalPlaces = 2
        Me.txtNewGross.FieldCode = Nothing
        Me.txtNewGross.FieldDesc = Nothing
        Me.txtNewGross.FieldMaxLength = 0
        Me.txtNewGross.FieldName = Nothing
        Me.txtNewGross.isCalculatedField = False
        Me.txtNewGross.IsSourceFromTable = False
        Me.txtNewGross.IsSourceFromValueList = False
        Me.txtNewGross.IsUnique = False
        Me.txtNewGross.Location = New System.Drawing.Point(84, 25)
        Me.txtNewGross.MendatroryField = False
        Me.txtNewGross.MyLinkLable1 = Nothing
        Me.txtNewGross.MyLinkLable2 = Nothing
        Me.txtNewGross.Name = "txtNewGross"
        Me.txtNewGross.ReferenceFieldDesc = Nothing
        Me.txtNewGross.ReferenceFieldName = Nothing
        Me.txtNewGross.ReferenceTableName = Nothing
        Me.txtNewGross.Size = New System.Drawing.Size(100, 20)
        Me.txtNewGross.TabIndex = 348
        Me.txtNewGross.Text = "0"
        Me.txtNewGross.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNewGross.Value = 0R
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.lblSubLocation)
        Me.Panel3.Controls.Add(Me.chkJobWork)
        Me.Panel3.Controls.Add(Me.MyLabel16)
        Me.Panel3.Controls.Add(Me.txtSubLocation)
        Me.Panel3.Location = New System.Drawing.Point(734, 120)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(396, 44)
        Me.Panel3.TabIndex = 355
        Me.Panel3.Visible = False
        '
        'lblSubLocation
        '
        Me.lblSubLocation.AutoSize = False
        Me.lblSubLocation.BorderVisible = True
        Me.lblSubLocation.FieldName = Nothing
        Me.lblSubLocation.Location = New System.Drawing.Point(213, 21)
        Me.lblSubLocation.Name = "lblSubLocation"
        Me.lblSubLocation.Size = New System.Drawing.Size(180, 19)
        Me.lblSubLocation.TabIndex = 276
        Me.lblSubLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkJobWork
        '
        Me.chkJobWork.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkJobWork.Location = New System.Drawing.Point(3, 3)
        Me.chkJobWork.Name = "chkJobWork"
        Me.chkJobWork.ReadOnly = True
        Me.chkJobWork.Size = New System.Drawing.Size(80, 16)
        Me.chkJobWork.TabIndex = 354
        Me.chkJobWork.Text = "Is Job Work"
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(3, 21)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel16.TabIndex = 274
        Me.MyLabel16.Text = "Sub Location"
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
        Me.txtSubLocation.Location = New System.Drawing.Point(81, 21)
        Me.txtSubLocation.MendatroryField = False
        Me.txtSubLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubLocation.MyLinkLable1 = Me.lblVendorBulk
        Me.txtSubLocation.MyLinkLable2 = Nothing
        Me.txtSubLocation.MyReadOnly = True
        Me.txtSubLocation.MyShowMasterFormButton = False
        Me.txtSubLocation.Name = "txtSubLocation"
        Me.txtSubLocation.ReferenceFieldDesc = Nothing
        Me.txtSubLocation.ReferenceFieldName = Nothing
        Me.txtSubLocation.ReferenceTableName = Nothing
        Me.txtSubLocation.Size = New System.Drawing.Size(124, 20)
        Me.txtSubLocation.TabIndex = 275
        Me.txtSubLocation.Value = ""
        '
        'lblVendorBulk
        '
        Me.lblVendorBulk.FieldName = Nothing
        Me.lblVendorBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorBulk.Location = New System.Drawing.Point(7, 146)
        Me.lblVendorBulk.Name = "lblVendorBulk"
        Me.lblVendorBulk.Size = New System.Drawing.Size(43, 16)
        Me.lblVendorBulk.TabIndex = 276
        Me.lblVendorBulk.Text = "Vendor"
        '
        'chkTankerReturn
        '
        Me.chkTankerReturn.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTankerReturn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTankerReturn.Location = New System.Drawing.Point(731, 90)
        Me.chkTankerReturn.Name = "chkTankerReturn"
        Me.chkTankerReturn.Size = New System.Drawing.Size(105, 16)
        Me.chkTankerReturn.TabIndex = 353
        Me.chkTankerReturn.Text = "Is Tanker Return"
        Me.chkTankerReturn.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'txtGateEntryDate
        '
        Me.txtGateEntryDate.CalculationExpression = Nothing
        Me.txtGateEntryDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtGateEntryDate.Enabled = False
        Me.txtGateEntryDate.FieldCode = Nothing
        Me.txtGateEntryDate.FieldDesc = Nothing
        Me.txtGateEntryDate.FieldMaxLength = 0
        Me.txtGateEntryDate.FieldName = Nothing
        Me.txtGateEntryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtGateEntryDate.isCalculatedField = False
        Me.txtGateEntryDate.IsSourceFromTable = False
        Me.txtGateEntryDate.IsSourceFromValueList = False
        Me.txtGateEntryDate.IsUnique = False
        Me.txtGateEntryDate.Location = New System.Drawing.Point(348, 82)
        Me.txtGateEntryDate.MendatroryField = False
        Me.txtGateEntryDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtGateEntryDate.MyLinkLable1 = Me.MyLabel2
        Me.txtGateEntryDate.MyLinkLable2 = Nothing
        Me.txtGateEntryDate.Name = "txtGateEntryDate"
        Me.txtGateEntryDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtGateEntryDate.ReferenceFieldDesc = Nothing
        Me.txtGateEntryDate.ReferenceFieldName = Nothing
        Me.txtGateEntryDate.ReferenceTableName = Nothing
        Me.txtGateEntryDate.Size = New System.Drawing.Size(132, 20)
        Me.txtGateEntryDate.TabIndex = 352
        Me.txtGateEntryDate.TabStop = False
        Me.txtGateEntryDate.Text = "10/06/2011 11:51 AM"
        Me.txtGateEntryDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(485, 62)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(102, 16)
        Me.MyLabel2.TabIndex = 287
        Me.MyLabel2.Text = "Gross Weight Date"
        '
        'txtChallanDate
        '
        Me.txtChallanDate.CalculationExpression = Nothing
        Me.txtChallanDate.CustomFormat = "dd/MM/yyyy"
        Me.txtChallanDate.Enabled = False
        Me.txtChallanDate.FieldCode = Nothing
        Me.txtChallanDate.FieldDesc = Nothing
        Me.txtChallanDate.FieldMaxLength = 0
        Me.txtChallanDate.FieldName = Nothing
        Me.txtChallanDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtChallanDate.isCalculatedField = False
        Me.txtChallanDate.IsSourceFromTable = False
        Me.txtChallanDate.IsSourceFromValueList = False
        Me.txtChallanDate.IsUnique = False
        Me.txtChallanDate.Location = New System.Drawing.Point(348, 103)
        Me.txtChallanDate.MendatroryField = False
        Me.txtChallanDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtChallanDate.MyLinkLable1 = Me.MyLabel2
        Me.txtChallanDate.MyLinkLable2 = Nothing
        Me.txtChallanDate.Name = "txtChallanDate"
        Me.txtChallanDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtChallanDate.ReferenceFieldDesc = Nothing
        Me.txtChallanDate.ReferenceFieldName = Nothing
        Me.txtChallanDate.ReferenceTableName = Nothing
        Me.txtChallanDate.Size = New System.Drawing.Size(132, 20)
        Me.txtChallanDate.TabIndex = 351
        Me.txtChallanDate.TabStop = False
        Me.txtChallanDate.Text = "10/06/2011"
        Me.txtChallanDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblGrossWt)
        Me.Panel1.Controls.Add(Me.txtGrossWt)
        Me.Panel1.Location = New System.Drawing.Point(731, 61)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(188, 26)
        Me.Panel1.TabIndex = 350
        Me.Panel1.Visible = False
        '
        'lblGrossWt
        '
        Me.lblGrossWt.FieldName = Nothing
        Me.lblGrossWt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGrossWt.Location = New System.Drawing.Point(3, 3)
        Me.lblGrossWt.Name = "lblGrossWt"
        Me.lblGrossWt.Size = New System.Drawing.Size(75, 16)
        Me.lblGrossWt.TabIndex = 349
        Me.lblGrossWt.Text = "Gross Weight"
        '
        'txtGrossWt
        '
        Me.txtGrossWt.BackColor = System.Drawing.Color.White
        Me.txtGrossWt.CalculationExpression = Nothing
        Me.txtGrossWt.DecimalPlaces = 2
        Me.txtGrossWt.FieldCode = Nothing
        Me.txtGrossWt.FieldDesc = Nothing
        Me.txtGrossWt.FieldMaxLength = 0
        Me.txtGrossWt.FieldName = Nothing
        Me.txtGrossWt.isCalculatedField = False
        Me.txtGrossWt.IsSourceFromTable = False
        Me.txtGrossWt.IsSourceFromValueList = False
        Me.txtGrossWt.IsUnique = False
        Me.txtGrossWt.Location = New System.Drawing.Point(84, 3)
        Me.txtGrossWt.MendatroryField = False
        Me.txtGrossWt.MyLinkLable1 = Nothing
        Me.txtGrossWt.MyLinkLable2 = Nothing
        Me.txtGrossWt.Name = "txtGrossWt"
        Me.txtGrossWt.ReferenceFieldDesc = Nothing
        Me.txtGrossWt.ReferenceFieldName = Nothing
        Me.txtGrossWt.ReferenceTableName = Nothing
        Me.txtGrossWt.Size = New System.Drawing.Size(100, 20)
        Me.txtGrossWt.TabIndex = 348
        Me.txtGrossWt.Text = "0"
        Me.txtGrossWt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtGrossWt.Value = 0R
        '
        'dtpTareWeight
        '
        Me.dtpTareWeight.CalculationExpression = Nothing
        Me.dtpTareWeight.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpTareWeight.Enabled = False
        Me.dtpTareWeight.FieldCode = Nothing
        Me.dtpTareWeight.FieldDesc = Nothing
        Me.dtpTareWeight.FieldMaxLength = 0
        Me.dtpTareWeight.FieldName = Nothing
        Me.dtpTareWeight.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTareWeight.isCalculatedField = False
        Me.dtpTareWeight.IsSourceFromTable = False
        Me.dtpTareWeight.IsSourceFromValueList = False
        Me.dtpTareWeight.IsUnique = False
        Me.dtpTareWeight.Location = New System.Drawing.Point(595, 82)
        Me.dtpTareWeight.MendatroryField = False
        Me.dtpTareWeight.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTareWeight.MyLinkLable1 = Me.MyLabel1
        Me.dtpTareWeight.MyLinkLable2 = Nothing
        Me.dtpTareWeight.Name = "dtpTareWeight"
        Me.dtpTareWeight.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTareWeight.ReferenceFieldDesc = Nothing
        Me.dtpTareWeight.ReferenceFieldName = Nothing
        Me.dtpTareWeight.ReferenceTableName = Nothing
        Me.dtpTareWeight.Size = New System.Drawing.Size(132, 20)
        Me.dtpTareWeight.TabIndex = 346
        Me.dtpTareWeight.TabStop = False
        Me.dtpTareWeight.Text = "10/06/2011 11:51 AM"
        Me.dtpTareWeight.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(485, 84)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(95, 16)
        Me.MyLabel1.TabIndex = 347
        Me.MyLabel1.Text = "Tare Weight Date"
        '
        'chkBothDoc
        '
        Me.chkBothDoc.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBothDoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBothDoc.Location = New System.Drawing.Point(627, 41)
        Me.chkBothDoc.Name = "chkBothDoc"
        Me.chkBothDoc.Size = New System.Drawing.Size(219, 16)
        Me.chkBothDoc.TabIndex = 345
        Me.chkBothDoc.Text = "Show Bulk and MCC In Both Document"
        Me.chkBothDoc.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'fndTankerNo
        '
        Me.fndTankerNo.CalculationExpression = Nothing
        Me.fndTankerNo.FieldCode = Nothing
        Me.fndTankerNo.FieldDesc = Nothing
        Me.fndTankerNo.FieldMaxLength = 0
        Me.fndTankerNo.FieldName = Nothing
        Me.fndTankerNo.isCalculatedField = False
        Me.fndTankerNo.IsSourceFromTable = False
        Me.fndTankerNo.IsSourceFromValueList = False
        Me.fndTankerNo.IsUnique = False
        Me.fndTankerNo.Location = New System.Drawing.Point(95, 61)
        Me.fndTankerNo.MendatroryField = True
        Me.fndTankerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTankerNo.MyLinkLable1 = Nothing
        Me.fndTankerNo.MyLinkLable2 = Nothing
        Me.fndTankerNo.MyReadOnly = False
        Me.fndTankerNo.MyShowMasterFormButton = False
        Me.fndTankerNo.Name = "fndTankerNo"
        Me.fndTankerNo.ReferenceFieldDesc = Nothing
        Me.fndTankerNo.ReferenceFieldName = Nothing
        Me.fndTankerNo.ReferenceTableName = Nothing
        Me.fndTankerNo.Size = New System.Drawing.Size(384, 19)
        Me.fndTankerNo.TabIndex = 344
        Me.fndTankerNo.Value = ""
        '
        'chkPendingTare
        '
        Me.chkPendingTare.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPendingTare.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPendingTare.Location = New System.Drawing.Point(485, 41)
        Me.chkPendingTare.Name = "chkPendingTare"
        Me.chkPendingTare.Size = New System.Drawing.Size(127, 16)
        Me.chkPendingTare.TabIndex = 343
        Me.chkPendingTare.Text = "Pending Tare Weight"
        Me.chkPendingTare.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'lblVendorCodeBulk
        '
        Me.lblVendorCodeBulk.CalculationExpression = Nothing
        Me.lblVendorCodeBulk.Enabled = False
        Me.lblVendorCodeBulk.FieldCode = Nothing
        Me.lblVendorCodeBulk.FieldDesc = Nothing
        Me.lblVendorCodeBulk.FieldMaxLength = 0
        Me.lblVendorCodeBulk.FieldName = Nothing
        Me.lblVendorCodeBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorCodeBulk.isCalculatedField = False
        Me.lblVendorCodeBulk.IsSourceFromTable = False
        Me.lblVendorCodeBulk.IsSourceFromValueList = False
        Me.lblVendorCodeBulk.IsUnique = False
        Me.lblVendorCodeBulk.Location = New System.Drawing.Point(95, 145)
        Me.lblVendorCodeBulk.MaxLength = 50
        Me.lblVendorCodeBulk.MendatroryField = True
        Me.lblVendorCodeBulk.MyLinkLable1 = Nothing
        Me.lblVendorCodeBulk.MyLinkLable2 = Nothing
        Me.lblVendorCodeBulk.Name = "lblVendorCodeBulk"
        Me.lblVendorCodeBulk.ReferenceFieldDesc = Nothing
        Me.lblVendorCodeBulk.ReferenceFieldName = Nothing
        Me.lblVendorCodeBulk.ReferenceTableName = Nothing
        Me.lblVendorCodeBulk.Size = New System.Drawing.Size(198, 18)
        Me.lblVendorCodeBulk.TabIndex = 298
        '
        'lblLocationCodeBulk
        '
        Me.lblLocationCodeBulk.CalculationExpression = Nothing
        Me.lblLocationCodeBulk.Enabled = False
        Me.lblLocationCodeBulk.FieldCode = Nothing
        Me.lblLocationCodeBulk.FieldDesc = Nothing
        Me.lblLocationCodeBulk.FieldMaxLength = 0
        Me.lblLocationCodeBulk.FieldName = Nothing
        Me.lblLocationCodeBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationCodeBulk.isCalculatedField = False
        Me.lblLocationCodeBulk.IsSourceFromTable = False
        Me.lblLocationCodeBulk.IsSourceFromValueList = False
        Me.lblLocationCodeBulk.IsUnique = False
        Me.lblLocationCodeBulk.Location = New System.Drawing.Point(95, 124)
        Me.lblLocationCodeBulk.MaxLength = 50
        Me.lblLocationCodeBulk.MendatroryField = True
        Me.lblLocationCodeBulk.MyLinkLable1 = Nothing
        Me.lblLocationCodeBulk.MyLinkLable2 = Nothing
        Me.lblLocationCodeBulk.Name = "lblLocationCodeBulk"
        Me.lblLocationCodeBulk.ReferenceFieldDesc = Nothing
        Me.lblLocationCodeBulk.ReferenceFieldName = Nothing
        Me.lblLocationCodeBulk.ReferenceTableName = Nothing
        Me.lblLocationCodeBulk.Size = New System.Drawing.Size(198, 18)
        Me.lblLocationCodeBulk.TabIndex = 297
        '
        'lblChallanNoBulk
        '
        Me.lblChallanNoBulk.CalculationExpression = Nothing
        Me.lblChallanNoBulk.Enabled = False
        Me.lblChallanNoBulk.FieldCode = Nothing
        Me.lblChallanNoBulk.FieldDesc = Nothing
        Me.lblChallanNoBulk.FieldMaxLength = 0
        Me.lblChallanNoBulk.FieldName = Nothing
        Me.lblChallanNoBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChallanNoBulk.isCalculatedField = False
        Me.lblChallanNoBulk.IsSourceFromTable = False
        Me.lblChallanNoBulk.IsSourceFromValueList = False
        Me.lblChallanNoBulk.IsUnique = False
        Me.lblChallanNoBulk.Location = New System.Drawing.Point(95, 104)
        Me.lblChallanNoBulk.MaxLength = 50
        Me.lblChallanNoBulk.MendatroryField = True
        Me.lblChallanNoBulk.MyLinkLable1 = Nothing
        Me.lblChallanNoBulk.MyLinkLable2 = Nothing
        Me.lblChallanNoBulk.Name = "lblChallanNoBulk"
        Me.lblChallanNoBulk.ReferenceFieldDesc = Nothing
        Me.lblChallanNoBulk.ReferenceFieldName = Nothing
        Me.lblChallanNoBulk.ReferenceTableName = Nothing
        Me.lblChallanNoBulk.Size = New System.Drawing.Size(198, 18)
        Me.lblChallanNoBulk.TabIndex = 295
        '
        'lblTankerNoBulk
        '
        Me.lblTankerNoBulk.CalculationExpression = Nothing
        Me.lblTankerNoBulk.Enabled = False
        Me.lblTankerNoBulk.FieldCode = Nothing
        Me.lblTankerNoBulk.FieldDesc = Nothing
        Me.lblTankerNoBulk.FieldMaxLength = 0
        Me.lblTankerNoBulk.FieldName = Nothing
        Me.lblTankerNoBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTankerNoBulk.isCalculatedField = False
        Me.lblTankerNoBulk.IsSourceFromTable = False
        Me.lblTankerNoBulk.IsSourceFromValueList = False
        Me.lblTankerNoBulk.IsUnique = False
        Me.lblTankerNoBulk.Location = New System.Drawing.Point(95, 61)
        Me.lblTankerNoBulk.MaxLength = 50
        Me.lblTankerNoBulk.MendatroryField = True
        Me.lblTankerNoBulk.MyLinkLable1 = Nothing
        Me.lblTankerNoBulk.MyLinkLable2 = Nothing
        Me.lblTankerNoBulk.Name = "lblTankerNoBulk"
        Me.lblTankerNoBulk.ReferenceFieldDesc = Nothing
        Me.lblTankerNoBulk.ReferenceFieldName = Nothing
        Me.lblTankerNoBulk.ReferenceTableName = Nothing
        Me.lblTankerNoBulk.Size = New System.Drawing.Size(290, 18)
        Me.lblTankerNoBulk.TabIndex = 294
        Me.lblTankerNoBulk.Visible = False
        '
        'txtWeighmentSlipNo
        '
        Me.txtWeighmentSlipNo.CalculationExpression = Nothing
        Me.txtWeighmentSlipNo.FieldCode = Nothing
        Me.txtWeighmentSlipNo.FieldDesc = Nothing
        Me.txtWeighmentSlipNo.FieldMaxLength = 0
        Me.txtWeighmentSlipNo.FieldName = Nothing
        Me.txtWeighmentSlipNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeighmentSlipNo.isCalculatedField = False
        Me.txtWeighmentSlipNo.IsSourceFromTable = False
        Me.txtWeighmentSlipNo.IsSourceFromValueList = False
        Me.txtWeighmentSlipNo.IsUnique = False
        Me.txtWeighmentSlipNo.Location = New System.Drawing.Point(595, 145)
        Me.txtWeighmentSlipNo.MaxLength = 50
        Me.txtWeighmentSlipNo.MendatroryField = False
        Me.txtWeighmentSlipNo.MyLinkLable1 = Nothing
        Me.txtWeighmentSlipNo.MyLinkLable2 = Nothing
        Me.txtWeighmentSlipNo.Name = "txtWeighmentSlipNo"
        Me.txtWeighmentSlipNo.ReferenceFieldDesc = Nothing
        Me.txtWeighmentSlipNo.ReferenceFieldName = Nothing
        Me.txtWeighmentSlipNo.ReferenceTableName = Nothing
        Me.txtWeighmentSlipNo.Size = New System.Drawing.Size(132, 18)
        Me.txtWeighmentSlipNo.TabIndex = 291
        '
        'lblWeighmentSlipNo
        '
        Me.lblWeighmentSlipNo.FieldName = Nothing
        Me.lblWeighmentSlipNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeighmentSlipNo.Location = New System.Drawing.Point(485, 146)
        Me.lblWeighmentSlipNo.Name = "lblWeighmentSlipNo"
        Me.lblWeighmentSlipNo.Size = New System.Drawing.Size(103, 16)
        Me.lblWeighmentSlipNo.TabIndex = 292
        Me.lblWeighmentSlipNo.Text = "Weighment Slip No"
        '
        'txtDipValue
        '
        Me.txtDipValue.CalculationExpression = Nothing
        Me.txtDipValue.FieldCode = Nothing
        Me.txtDipValue.FieldDesc = Nothing
        Me.txtDipValue.FieldMaxLength = 0
        Me.txtDipValue.FieldName = Nothing
        Me.txtDipValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDipValue.isCalculatedField = False
        Me.txtDipValue.IsSourceFromTable = False
        Me.txtDipValue.IsSourceFromValueList = False
        Me.txtDipValue.IsUnique = False
        Me.txtDipValue.Location = New System.Drawing.Point(595, 124)
        Me.txtDipValue.MaxLength = 50
        Me.txtDipValue.MendatroryField = False
        Me.txtDipValue.MyLinkLable1 = Nothing
        Me.txtDipValue.MyLinkLable2 = Nothing
        Me.txtDipValue.Name = "txtDipValue"
        Me.txtDipValue.ReferenceFieldDesc = Nothing
        Me.txtDipValue.ReferenceFieldName = Nothing
        Me.txtDipValue.ReferenceTableName = Nothing
        Me.txtDipValue.Size = New System.Drawing.Size(132, 18)
        Me.txtDipValue.TabIndex = 3
        '
        'lblDipValue
        '
        Me.lblDipValue.FieldName = Nothing
        Me.lblDipValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDipValue.Location = New System.Drawing.Point(485, 125)
        Me.lblDipValue.Name = "lblDipValue"
        Me.lblDipValue.Size = New System.Drawing.Size(57, 16)
        Me.lblDipValue.TabIndex = 290
        Me.lblDipValue.Text = "DIP Value"
        '
        'lblPending
        '
        Me.lblPending.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(548, 12)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(97, 25)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 248
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPBulkProcurement.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(458, 40)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(20, 18)
        Me.btnReset.TabIndex = 250
        '
        'grpGateEntryType
        '
        Me.grpGateEntryType.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpGateEntryType.Controls.Add(Me.chkBulkMilkProc)
        Me.grpGateEntryType.Controls.Add(Me.chkMccProc)
        Me.grpGateEntryType.HeaderText = "Select Type Of Weighment"
        Me.grpGateEntryType.Location = New System.Drawing.Point(3, 3)
        Me.grpGateEntryType.Name = "grpGateEntryType"
        Me.grpGateEntryType.Size = New System.Drawing.Size(376, 34)
        Me.grpGateEntryType.TabIndex = 10
        Me.grpGateEntryType.Text = "Select Type Of Weighment"
        '
        'chkBulkMilkProc
        '
        Me.chkBulkMilkProc.Location = New System.Drawing.Point(5, 13)
        Me.chkBulkMilkProc.Name = "chkBulkMilkProc"
        Me.chkBulkMilkProc.Size = New System.Drawing.Size(110, 18)
        Me.chkBulkMilkProc.TabIndex = 0
        Me.chkBulkMilkProc.Text = "Contractor Milk In"
        '
        'chkMccProc
        '
        Me.chkMccProc.Location = New System.Drawing.Point(230, 13)
        Me.chkMccProc.Name = "chkMccProc"
        Me.chkMccProc.Size = New System.Drawing.Size(136, 18)
        Me.chkMccProc.TabIndex = 1
        Me.chkMccProc.Text = "Plant / MCC Transfer In"
        '
        'lblStatus
        '
        Me.lblStatus.FieldName = Nothing
        Me.lblStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(485, 105)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(58, 16)
        Me.lblStatus.TabIndex = 285
        Me.lblStatus.Text = "QC Status"
        '
        'fndDocNO
        '
        Me.fndDocNO.FieldName = Nothing
        Me.fndDocNO.Location = New System.Drawing.Point(95, 40)
        Me.fndDocNO.MendatroryField = False
        Me.fndDocNO.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndDocNO.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocNO.MyLinkLable1 = Me.lblDocNo
        Me.fndDocNO.MyLinkLable2 = Nothing
        Me.fndDocNO.MyMaxLength = 32767
        Me.fndDocNO.MyReadOnly = False
        Me.fndDocNO.Name = "fndDocNO"
        Me.fndDocNO.Size = New System.Drawing.Size(363, 18)
        Me.fndDocNO.TabIndex = 0
        Me.fndDocNO.Value = ""
        '
        'lblDocNo
        '
        Me.lblDocNo.FieldName = Nothing
        Me.lblDocNo.Location = New System.Drawing.Point(7, 40)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(79, 18)
        Me.lblDocNo.TabIndex = 251
        Me.lblDocNo.Text = "Document No."
        '
        'dtpWeighmentDateBulk
        '
        Me.dtpWeighmentDateBulk.CalculationExpression = Nothing
        Me.dtpWeighmentDateBulk.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpWeighmentDateBulk.FieldCode = Nothing
        Me.dtpWeighmentDateBulk.FieldDesc = Nothing
        Me.dtpWeighmentDateBulk.FieldMaxLength = 0
        Me.dtpWeighmentDateBulk.FieldName = Nothing
        Me.dtpWeighmentDateBulk.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpWeighmentDateBulk.isCalculatedField = False
        Me.dtpWeighmentDateBulk.IsSourceFromTable = False
        Me.dtpWeighmentDateBulk.IsSourceFromValueList = False
        Me.dtpWeighmentDateBulk.IsUnique = False
        Me.dtpWeighmentDateBulk.Location = New System.Drawing.Point(595, 60)
        Me.dtpWeighmentDateBulk.MendatroryField = False
        Me.dtpWeighmentDateBulk.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpWeighmentDateBulk.MyLinkLable1 = Me.MyLabel2
        Me.dtpWeighmentDateBulk.MyLinkLable2 = Nothing
        Me.dtpWeighmentDateBulk.Name = "dtpWeighmentDateBulk"
        Me.dtpWeighmentDateBulk.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpWeighmentDateBulk.ReferenceFieldDesc = Nothing
        Me.dtpWeighmentDateBulk.ReferenceFieldName = Nothing
        Me.dtpWeighmentDateBulk.ReferenceTableName = Nothing
        Me.dtpWeighmentDateBulk.Size = New System.Drawing.Size(132, 20)
        Me.dtpWeighmentDateBulk.TabIndex = 2
        Me.dtpWeighmentDateBulk.TabStop = False
        Me.dtpWeighmentDateBulk.Text = "10/06/2011 11:51 AM"
        Me.dtpWeighmentDateBulk.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblTankerNo
        '
        Me.lblTankerNo.FieldName = Nothing
        Me.lblTankerNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTankerNo.Location = New System.Drawing.Point(7, 62)
        Me.lblTankerNo.Name = "lblTankerNo"
        Me.lblTankerNo.Size = New System.Drawing.Size(59, 16)
        Me.lblTankerNo.TabIndex = 279
        Me.lblTankerNo.Text = "Tanker No"
        '
        'fndGateEntryNoBulk
        '
        Me.fndGateEntryNoBulk.CalculationExpression = Nothing
        Me.fndGateEntryNoBulk.Enabled = False
        Me.fndGateEntryNoBulk.FieldCode = Nothing
        Me.fndGateEntryNoBulk.FieldDesc = Nothing
        Me.fndGateEntryNoBulk.FieldMaxLength = 0
        Me.fndGateEntryNoBulk.FieldName = Nothing
        Me.fndGateEntryNoBulk.isCalculatedField = False
        Me.fndGateEntryNoBulk.IsSourceFromTable = False
        Me.fndGateEntryNoBulk.IsSourceFromValueList = False
        Me.fndGateEntryNoBulk.IsUnique = False
        Me.fndGateEntryNoBulk.Location = New System.Drawing.Point(95, 83)
        Me.fndGateEntryNoBulk.MendatroryField = True
        Me.fndGateEntryNoBulk.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndGateEntryNoBulk.MyLinkLable1 = Nothing
        Me.fndGateEntryNoBulk.MyLinkLable2 = Nothing
        Me.fndGateEntryNoBulk.MyReadOnly = False
        Me.fndGateEntryNoBulk.MyShowMasterFormButton = False
        Me.fndGateEntryNoBulk.Name = "fndGateEntryNoBulk"
        Me.fndGateEntryNoBulk.ReferenceFieldDesc = Nothing
        Me.fndGateEntryNoBulk.ReferenceFieldName = Nothing
        Me.fndGateEntryNoBulk.ReferenceTableName = Nothing
        Me.fndGateEntryNoBulk.Size = New System.Drawing.Size(196, 19)
        Me.fndGateEntryNoBulk.TabIndex = 1
        Me.fndGateEntryNoBulk.Value = ""
        '
        'lblLocationNameBulk
        '
        Me.lblLocationNameBulk.AutoSize = False
        Me.lblLocationNameBulk.BorderVisible = True
        Me.lblLocationNameBulk.FieldName = Nothing
        Me.lblLocationNameBulk.Location = New System.Drawing.Point(298, 124)
        Me.lblLocationNameBulk.Name = "lblLocationNameBulk"
        Me.lblLocationNameBulk.Size = New System.Drawing.Size(181, 18)
        Me.lblLocationNameBulk.TabIndex = 275
        Me.lblLocationNameBulk.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocationNameBulk.TextWrap = False
        '
        'lblLocationBulk
        '
        Me.lblLocationBulk.FieldName = Nothing
        Me.lblLocationBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationBulk.Location = New System.Drawing.Point(7, 125)
        Me.lblLocationBulk.Name = "lblLocationBulk"
        Me.lblLocationBulk.Size = New System.Drawing.Size(49, 16)
        Me.lblLocationBulk.TabIndex = 273
        Me.lblLocationBulk.Text = "Location"
        '
        'lblStatusBulk
        '
        Me.lblStatusBulk.AutoSize = False
        Me.lblStatusBulk.BorderVisible = True
        Me.lblStatusBulk.FieldName = Nothing
        Me.lblStatusBulk.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblStatusBulk.Location = New System.Drawing.Point(595, 104)
        Me.lblStatusBulk.Name = "lblStatusBulk"
        Me.lblStatusBulk.Size = New System.Drawing.Size(132, 18)
        Me.lblStatusBulk.TabIndex = 286
        Me.lblStatusBulk.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDateAndTimeBulk
        '
        Me.lblDateAndTimeBulk.FieldName = Nothing
        Me.lblDateAndTimeBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateAndTimeBulk.Location = New System.Drawing.Point(294, 84)
        Me.lblDateAndTimeBulk.Name = "lblDateAndTimeBulk"
        Me.lblDateAndTimeBulk.Size = New System.Drawing.Size(53, 16)
        Me.lblDateAndTimeBulk.TabIndex = 252
        Me.lblDateAndTimeBulk.Text = "GE. Date"
        '
        'lblChallanDate
        '
        Me.lblChallanDate.FieldName = Nothing
        Me.lblChallanDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChallanDate.Location = New System.Drawing.Point(294, 105)
        Me.lblChallanDate.Name = "lblChallanDate"
        Me.lblChallanDate.Size = New System.Drawing.Size(51, 16)
        Me.lblChallanDate.TabIndex = 283
        Me.lblChallanDate.Text = "Ch. Date"
        '
        'lblChallanNo
        '
        Me.lblChallanNo.FieldName = Nothing
        Me.lblChallanNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChallanNo.Location = New System.Drawing.Point(7, 105)
        Me.lblChallanNo.Name = "lblChallanNo"
        Me.lblChallanNo.Size = New System.Drawing.Size(62, 16)
        Me.lblChallanNo.TabIndex = 281
        Me.lblChallanNo.Text = "Challan No"
        '
        'lblVendorNameBulk
        '
        Me.lblVendorNameBulk.AutoSize = False
        Me.lblVendorNameBulk.BorderVisible = True
        Me.lblVendorNameBulk.FieldName = Nothing
        Me.lblVendorNameBulk.Location = New System.Drawing.Point(298, 145)
        Me.lblVendorNameBulk.Name = "lblVendorNameBulk"
        Me.lblVendorNameBulk.Size = New System.Drawing.Size(181, 18)
        Me.lblVendorNameBulk.TabIndex = 278
        Me.lblVendorNameBulk.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVendorNameBulk.TextWrap = False
        '
        'lblGateEntryNO
        '
        Me.lblGateEntryNO.FieldName = Nothing
        Me.lblGateEntryNO.Location = New System.Drawing.Point(7, 83)
        Me.lblGateEntryNO.Name = "lblGateEntryNO"
        Me.lblGateEntryNO.Size = New System.Drawing.Size(82, 18)
        Me.lblGateEntryNO.TabIndex = 32
        Me.lblGateEntryNO.Text = "Gate Entry No. "
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gvItemBulk)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = "Item Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(1137, 218)
        Me.RadGroupBox1.TabIndex = 285
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
        Me.gvItemBulk.Size = New System.Drawing.Size(1133, 198)
        Me.gvItemBulk.TabIndex = 264
        Me.gvItemBulk.Text = "RadGridView1"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(215, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(286, 7)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(69, 18)
        Me.btnReverse.TabIndex = 4
        Me.btnReverse.Text = "Reverse"
        Me.btnReverse.Visible = False
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(144, 7)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 18)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(73, 7)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1066, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 7)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuSetting})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1137, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'mnuSetting
        '
        Me.mnuSetting.AccessibleDescription = "Setting"
        Me.mnuSetting.AccessibleName = "Setting"
        Me.mnuSetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuSaveLayout, Me.mnuDeleteLayout, Me.mnuExit})
        Me.mnuSetting.Name = "mnuSetting"
        Me.mnuSetting.Text = "Setting"
        '
        'mnuSaveLayout
        '
        Me.mnuSaveLayout.AccessibleDescription = "Save Layout"
        Me.mnuSaveLayout.AccessibleName = "Save Layout"
        Me.mnuSaveLayout.Name = "mnuSaveLayout"
        Me.mnuSaveLayout.Text = "Save Layout"
        '
        'mnuDeleteLayout
        '
        Me.mnuDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.mnuDeleteLayout.AccessibleName = "Delete Layout"
        Me.mnuDeleteLayout.Name = "mnuDeleteLayout"
        Me.mnuDeleteLayout.Text = "Delete Layout"
        '
        'mnuExit
        '
        Me.mnuExit.AccessibleDescription = "Exit"
        Me.mnuExit.AccessibleName = "Exit"
        Me.mnuExit.Name = "mnuExit"
        Me.mnuExit.Text = "Exit"
        '
        'fndRefrencesNo
        '
        Me.fndRefrencesNo.CalculationExpression = Nothing
        Me.fndRefrencesNo.Enabled = False
        Me.fndRefrencesNo.FieldCode = Nothing
        Me.fndRefrencesNo.FieldDesc = Nothing
        Me.fndRefrencesNo.FieldMaxLength = 0
        Me.fndRefrencesNo.FieldName = Nothing
        Me.fndRefrencesNo.isCalculatedField = False
        Me.fndRefrencesNo.IsSourceFromTable = False
        Me.fndRefrencesNo.IsSourceFromValueList = False
        Me.fndRefrencesNo.IsUnique = False
        Me.fndRefrencesNo.Location = New System.Drawing.Point(95, 165)
        Me.fndRefrencesNo.MendatroryField = True
        Me.fndRefrencesNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRefrencesNo.MyLinkLable1 = Nothing
        Me.fndRefrencesNo.MyLinkLable2 = Nothing
        Me.fndRefrencesNo.MyReadOnly = False
        Me.fndRefrencesNo.MyShowMasterFormButton = False
        Me.fndRefrencesNo.Name = "fndRefrencesNo"
        Me.fndRefrencesNo.ReferenceFieldDesc = Nothing
        Me.fndRefrencesNo.ReferenceFieldName = Nothing
        Me.fndRefrencesNo.ReferenceTableName = Nothing
        Me.fndRefrencesNo.Size = New System.Drawing.Size(385, 19)
        Me.fndRefrencesNo.TabIndex = 358
        Me.fndRefrencesNo.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(3, 166)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel3.TabIndex = 357
        Me.MyLabel3.Text = "Reference No"
        '
        'FrmWeighment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1137, 529)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmWeighment"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmWeighment"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.txtLineNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpdateWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLineNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNewNetWt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNewTare, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGrossNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNewGross, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.lblSubLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkJobWork, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTankerReturn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGateEntryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChallanDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.lblGrossWt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGrossWt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpTareWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBothDoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPendingTare, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorCodeBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationCodeBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChallanNoBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNoBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWeighmentSlipNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWeighmentSlipNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDipValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDipValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpGateEntryType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpGateEntryType.ResumeLayout(False)
        Me.grpGateEntryType.PerformLayout()
        CType(Me.chkBulkMilkProc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMccProc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpWeighmentDateBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationNameBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStatusBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDateAndTimeBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChallanDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChallanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorNameBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGateEntryNO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gvItemBulk.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItemBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItemMcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents grpGateEntryType As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkBulkMilkProc As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkMccProc As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents lblGateEntryNO As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndGateEntryNoBulk As common.UserControls.txtFinder
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDateAndTimeBulk As common.Controls.MyLabel
    Friend WithEvents lblVendorNameBulk As common.Controls.MyLabel
    Friend WithEvents lblVendorBulk As common.Controls.MyLabel
    Friend WithEvents lblLocationNameBulk As common.Controls.MyLabel
    Friend WithEvents lblLocationBulk As common.Controls.MyLabel
    Friend WithEvents lblTankerNo As common.Controls.MyLabel
    Friend WithEvents lblChallanDate As common.Controls.MyLabel
    Friend WithEvents lblChallanNo As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvItemBulk As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblStatusBulk As common.Controls.MyLabel
    Friend WithEvents lblStatus As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents dtpWeighmentDateBulk As common.Controls.MyDateTimePicker
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndDocNO As common.UserControls.txtNavigator
    Friend WithEvents lblDocNo As common.Controls.MyLabel
    Friend WithEvents gvItemMcc As Telerik.WinControls.UI.MasterGridViewTemplate
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents mnuSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuExit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtDipValue As common.Controls.MyTextBox
    Friend WithEvents lblDipValue As common.Controls.MyLabel
    Friend WithEvents txtWeighmentSlipNo As common.Controls.MyTextBox
    Friend WithEvents lblWeighmentSlipNo As common.Controls.MyLabel
    Friend WithEvents lblChallanNoBulk As common.Controls.MyTextBox
    Friend WithEvents lblTankerNoBulk As common.Controls.MyTextBox
    Friend WithEvents lblVendorCodeBulk As common.Controls.MyTextBox
    Friend WithEvents lblLocationCodeBulk As common.Controls.MyTextBox
    Friend WithEvents chkPendingTare As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents fndTankerNo As common.UserControls.txtFinder
    Friend WithEvents chkBothDoc As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents dtpTareWeight As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents UcWeighing1 As XpertERPEngine.ucWeighing
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblGrossWt As common.Controls.MyLabel
    Friend WithEvents txtGrossWt As common.MyNumBox
    Friend WithEvents txtChallanDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtGateEntryDate As common.Controls.MyDateTimePicker
    Friend WithEvents chkTankerReturn As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblSubLocation As common.Controls.MyLabel
    Friend WithEvents chkJobWork As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtSubLocation As common.UserControls.txtFinder
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnUpdateWeight As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblLineNo As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtNewNetWt As common.MyNumBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtNewTare As common.MyNumBox
    Friend WithEvents txtGrossNew As common.Controls.MyLabel
    Friend WithEvents txtNewGross As common.MyNumBox
    Friend WithEvents txtLineNo As common.MyNumBox
    Friend WithEvents fndRefrencesNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
End Class

