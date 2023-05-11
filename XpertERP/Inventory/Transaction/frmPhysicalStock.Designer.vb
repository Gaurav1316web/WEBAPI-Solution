<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPhysicalStock
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPhysicalStock))
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtSelfLifeDays = New common.MyNumBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtManualBatchNo = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.chkMilk = New common.Controls.MyCheckBox()
        Me.txtdesc = New common.Controls.MyTextBox()
        Me.txtsubLocName = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtsubLoc = New common.UserControls.txtFinder()
        Me.txtLocName = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.dtpdate = New common.Controls.MyDateTimePicker()
        Me.lbldate = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnexport = New Telerik.WinControls.UI.RadButton()
        Me.btnimport = New Telerik.WinControls.UI.RadButton()
        Me.btnpost = New Telerik.WinControls.UI.RadButton()
        Me.txtInventoryAccount = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtItemType = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel8 = New common.Controls.MyLabel()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.txtSelfLifeDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtManualBatchNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMilk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtsubLocName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnexport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnimport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(594, 2)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(72, 19)
        Me.btnclose.TabIndex = 1
        Me.btnclose.Text = "Close"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(2, 18)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(668, 246)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(4, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(72, 19)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnexport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnimport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(674, 494)
        Me.SplitContainer1.SplitterDistance = 463
        Me.SplitContainer1.TabIndex = 1
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtItemType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtInventoryAccount)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Panel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnGo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkMilk)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtdesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtsubLocName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtsubLoc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtLocName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel15)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpdate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lbldate)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(1)
        Me.SplitContainer2.Size = New System.Drawing.Size(674, 463)
        Me.SplitContainer2.SplitterDistance = 170
        Me.SplitContainer2.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtSelfLifeDays)
        Me.Panel2.Controls.Add(Me.MyLabel6)
        Me.Panel2.Controls.Add(Me.txtManualBatchNo)
        Me.Panel2.Controls.Add(Me.MyLabel5)
        Me.Panel2.Location = New System.Drawing.Point(3, 142)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(560, 25)
        Me.Panel2.TabIndex = 1379
        '
        'txtSelfLifeDays
        '
        Me.txtSelfLifeDays.BackColor = System.Drawing.Color.White
        Me.txtSelfLifeDays.CalculationExpression = Nothing
        Me.txtSelfLifeDays.DecimalPlaces = 0
        Me.txtSelfLifeDays.FieldCode = Nothing
        Me.txtSelfLifeDays.FieldDesc = Nothing
        Me.txtSelfLifeDays.FieldMaxLength = 5
        Me.txtSelfLifeDays.FieldName = Nothing
        Me.txtSelfLifeDays.isCalculatedField = False
        Me.txtSelfLifeDays.IsSourceFromTable = False
        Me.txtSelfLifeDays.IsSourceFromValueList = False
        Me.txtSelfLifeDays.IsUnique = False
        Me.txtSelfLifeDays.Location = New System.Drawing.Point(339, 2)
        Me.txtSelfLifeDays.MendatroryField = False
        Me.txtSelfLifeDays.MyLinkLable1 = Nothing
        Me.txtSelfLifeDays.MyLinkLable2 = Nothing
        Me.txtSelfLifeDays.Name = "txtSelfLifeDays"
        Me.txtSelfLifeDays.ReferenceFieldDesc = Nothing
        Me.txtSelfLifeDays.ReferenceFieldName = Nothing
        Me.txtSelfLifeDays.ReferenceTableName = Nothing
        Me.txtSelfLifeDays.Size = New System.Drawing.Size(46, 20)
        Me.txtSelfLifeDays.TabIndex = 1379
        Me.txtSelfLifeDays.Text = "0"
        Me.txtSelfLifeDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSelfLifeDays.Value = 0.0R
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(252, 4)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(80, 16)
        Me.MyLabel6.TabIndex = 1377
        Me.MyLabel6.Text = "Shelf life days "
        '
        'txtManualBatchNo
        '
        Me.txtManualBatchNo.CalculationExpression = Nothing
        Me.txtManualBatchNo.FieldCode = Nothing
        Me.txtManualBatchNo.FieldDesc = Nothing
        Me.txtManualBatchNo.FieldMaxLength = 0
        Me.txtManualBatchNo.FieldName = Nothing
        Me.txtManualBatchNo.isCalculatedField = False
        Me.txtManualBatchNo.IsSourceFromTable = False
        Me.txtManualBatchNo.IsSourceFromValueList = False
        Me.txtManualBatchNo.IsUnique = False
        Me.txtManualBatchNo.Location = New System.Drawing.Point(103, 2)
        Me.txtManualBatchNo.MaxLength = 200
        Me.txtManualBatchNo.MendatroryField = False
        Me.txtManualBatchNo.MyLinkLable1 = Me.MyLabel2
        Me.txtManualBatchNo.MyLinkLable2 = Nothing
        Me.txtManualBatchNo.Name = "txtManualBatchNo"
        Me.txtManualBatchNo.ReferenceFieldDesc = Nothing
        Me.txtManualBatchNo.ReferenceFieldName = Nothing
        Me.txtManualBatchNo.ReferenceTableName = Nothing
        Me.txtManualBatchNo.Size = New System.Drawing.Size(143, 20)
        Me.txtManualBatchNo.TabIndex = 1376
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(12, 36)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel2.TabIndex = 1374
        Me.MyLabel2.Text = "Description"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(10, 4)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel5.TabIndex = 1375
        Me.MyLabel5.Text = "Batch No "
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(569, 10)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(99, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1378
        '
        'btnGo
        '
        Me.btnGo.Location = New System.Drawing.Point(569, 145)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(99, 19)
        Me.btnGo.TabIndex = 1376
        Me.btnGo.Text = ">>"
        '
        'chkMilk
        '
        Me.chkMilk.Location = New System.Drawing.Point(511, 11)
        Me.chkMilk.MyLinkLable1 = Nothing
        Me.chkMilk.MyLinkLable2 = Nothing
        Me.chkMilk.Name = "chkMilk"
        Me.chkMilk.Size = New System.Drawing.Size(52, 18)
        Me.chkMilk.TabIndex = 2
        Me.chkMilk.Tag1 = Nothing
        Me.chkMilk.Text = "Is Milk"
        '
        'txtdesc
        '
        Me.txtdesc.CalculationExpression = Nothing
        Me.txtdesc.FieldCode = Nothing
        Me.txtdesc.FieldDesc = Nothing
        Me.txtdesc.FieldMaxLength = 0
        Me.txtdesc.FieldName = Nothing
        Me.txtdesc.isCalculatedField = False
        Me.txtdesc.IsSourceFromTable = False
        Me.txtdesc.IsSourceFromValueList = False
        Me.txtdesc.IsUnique = False
        Me.txtdesc.Location = New System.Drawing.Point(106, 34)
        Me.txtdesc.MaxLength = 200
        Me.txtdesc.MendatroryField = False
        Me.txtdesc.MyLinkLable1 = Me.MyLabel2
        Me.txtdesc.MyLinkLable2 = Nothing
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.ReferenceFieldDesc = Nothing
        Me.txtdesc.ReferenceFieldName = Nothing
        Me.txtdesc.ReferenceTableName = Nothing
        Me.txtdesc.Size = New System.Drawing.Size(562, 20)
        Me.txtdesc.TabIndex = 3
        '
        'txtsubLocName
        '
        Me.txtsubLocName.AutoSize = False
        Me.txtsubLocName.BorderVisible = True
        Me.txtsubLocName.FieldName = Nothing
        Me.txtsubLocName.Location = New System.Drawing.Point(252, 78)
        Me.txtsubLocName.Name = "txtsubLocName"
        Me.txtsubLocName.Size = New System.Drawing.Size(416, 19)
        Me.txtsubLocName.TabIndex = 1373
        Me.txtsubLocName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(12, 79)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel3.TabIndex = 1372
        Me.MyLabel3.Text = "Sub Location"
        '
        'txtsubLoc
        '
        Me.txtsubLoc.CalculationExpression = Nothing
        Me.txtsubLoc.Enabled = False
        Me.txtsubLoc.FieldCode = Nothing
        Me.txtsubLoc.FieldDesc = Nothing
        Me.txtsubLoc.FieldMaxLength = 0
        Me.txtsubLoc.FieldName = Nothing
        Me.txtsubLoc.isCalculatedField = False
        Me.txtsubLoc.IsSourceFromTable = False
        Me.txtsubLoc.IsSourceFromValueList = False
        Me.txtsubLoc.IsUnique = False
        Me.txtsubLoc.Location = New System.Drawing.Point(106, 78)
        Me.txtsubLoc.MendatroryField = True
        Me.txtsubLoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsubLoc.MyLinkLable1 = Me.MyLabel3
        Me.txtsubLoc.MyLinkLable2 = Me.txtsubLocName
        Me.txtsubLoc.MyReadOnly = False
        Me.txtsubLoc.MyShowMasterFormButton = False
        Me.txtsubLoc.Name = "txtsubLoc"
        Me.txtsubLoc.ReferenceFieldDesc = Nothing
        Me.txtsubLoc.ReferenceFieldName = Nothing
        Me.txtsubLoc.ReferenceTableName = Nothing
        Me.txtsubLoc.Size = New System.Drawing.Size(143, 18)
        Me.txtsubLoc.TabIndex = 5
        Me.txtsubLoc.Value = ""
        '
        'txtLocName
        '
        Me.txtLocName.AutoSize = False
        Me.txtLocName.BorderVisible = True
        Me.txtLocName.FieldName = Nothing
        Me.txtLocName.Location = New System.Drawing.Point(252, 57)
        Me.txtLocName.Name = "txtLocName"
        Me.txtLocName.Size = New System.Drawing.Size(416, 19)
        Me.txtLocName.TabIndex = 1370
        Me.txtLocName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnReset
        '
        Me.btnReset.Image = CType(resources.GetObject("btnReset.Image"), System.Drawing.Image)
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(368, 10)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(20, 21)
        Me.btnReset.TabIndex = 0
        Me.btnReset.Text = " "
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(106, 10)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.MyLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(262, 21)
        Me.txtCode.TabIndex = 45
        Me.txtCode.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 12)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(88, 16)
        Me.MyLabel1.TabIndex = 4
        Me.MyLabel1.Text = "Document Code"
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(12, 58)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 3
        Me.RadLabel15.Text = "Location"
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
        Me.txtLocation.Location = New System.Drawing.Point(106, 57)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtLocation.MyLinkLable2 = Me.txtLocName
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(143, 18)
        Me.txtLocation.TabIndex = 4
        Me.txtLocation.Value = ""
        '
        'dtpdate
        '
        Me.dtpdate.CalculationExpression = Nothing
        Me.dtpdate.CustomFormat = "dd/MM/yyyy"
        Me.dtpdate.FieldCode = Nothing
        Me.dtpdate.FieldDesc = Nothing
        Me.dtpdate.FieldMaxLength = 0
        Me.dtpdate.FieldName = Nothing
        Me.dtpdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpdate.isCalculatedField = False
        Me.dtpdate.IsSourceFromTable = False
        Me.dtpdate.IsSourceFromValueList = False
        Me.dtpdate.IsUnique = False
        Me.dtpdate.Location = New System.Drawing.Point(424, 11)
        Me.dtpdate.MendatroryField = False
        Me.dtpdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpdate.MyLinkLable1 = Me.lbldate
        Me.dtpdate.MyLinkLable2 = Nothing
        Me.dtpdate.Name = "dtpdate"
        Me.dtpdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpdate.ReferenceFieldDesc = Nothing
        Me.dtpdate.ReferenceFieldName = Nothing
        Me.dtpdate.ReferenceTableName = Nothing
        Me.dtpdate.Size = New System.Drawing.Size(82, 18)
        Me.dtpdate.TabIndex = 1
        Me.dtpdate.TabStop = False
        Me.dtpdate.Text = "13/06/2011"
        Me.dtpdate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lbldate
        '
        Me.lbldate.FieldName = Nothing
        Me.lbldate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldate.Location = New System.Drawing.Point(389, 12)
        Me.lbldate.Name = "lbldate"
        Me.lbldate.Size = New System.Drawing.Size(30, 16)
        Me.lbldate.TabIndex = 2
        Me.lbldate.Text = "Date"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gv1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = "Item Detail"
        Me.RadGroupBox1.Location = New System.Drawing.Point(1, 1)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(672, 266)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "Item Detail"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.MyLabel4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(1, 267)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(672, 21)
        Me.Panel1.TabIndex = 1
        '
        'MyLabel4
        '
        Me.MyLabel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.ForeColor = System.Drawing.Color.Navy
        Me.MyLabel4.Location = New System.Drawing.Point(452, 3)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(220, 16)
        Me.MyLabel4.TabIndex = 1373
        Me.MyLabel4.Text = "Click F4 for serializing || F5 For Batch Item"
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(464, 2)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(127, 19)
        Me.btnReverse.TabIndex = 138
        Me.btnReverse.Text = "Revese and Unpost"
        Me.btnReverse.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(79, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(72, 19)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnexport
        '
        Me.btnexport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnexport.Location = New System.Drawing.Point(229, 5)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(72, 19)
        Me.btnexport.TabIndex = 3
        Me.btnexport.Text = "Export"
        '
        'btnimport
        '
        Me.btnimport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnimport.Location = New System.Drawing.Point(304, 5)
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Size = New System.Drawing.Size(72, 19)
        Me.btnimport.TabIndex = 2
        Me.btnimport.Text = "Import"
        '
        'btnpost
        '
        Me.btnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnpost.Location = New System.Drawing.Point(154, 5)
        Me.btnpost.Name = "btnpost"
        Me.btnpost.Size = New System.Drawing.Size(72, 19)
        Me.btnpost.TabIndex = 1
        Me.btnpost.Text = "Post"
        '
        'txtInventoryAccount
        '
        Me.txtInventoryAccount.arrDispalyMember = Nothing
        Me.txtInventoryAccount.arrValueMember = Nothing
        Me.txtInventoryAccount.Location = New System.Drawing.Point(106, 99)
        Me.txtInventoryAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryAccount.MyLinkLable1 = Me.MyLabel7
        Me.txtInventoryAccount.MyLinkLable2 = Nothing
        Me.txtInventoryAccount.MyNullText = "All"
        Me.txtInventoryAccount.Name = "txtInventoryAccount"
        Me.txtInventoryAccount.Size = New System.Drawing.Size(562, 19)
        Me.txtInventoryAccount.TabIndex = 1381
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(12, 99)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(97, 18)
        Me.MyLabel7.TabIndex = 1380
        Me.MyLabel7.Text = "Inventory Account"
        '
        'txtItemType
        '
        Me.txtItemType.arrDispalyMember = Nothing
        Me.txtItemType.arrValueMember = Nothing
        Me.txtItemType.Location = New System.Drawing.Point(106, 120)
        Me.txtItemType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemType.MyLinkLable1 = Me.MyLabel8
        Me.txtItemType.MyLinkLable2 = Nothing
        Me.txtItemType.MyNullText = "All"
        Me.txtItemType.Name = "txtItemType"
        Me.txtItemType.Size = New System.Drawing.Size(562, 19)
        Me.txtItemType.TabIndex = 1383
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(12, 120)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(56, 18)
        Me.MyLabel8.TabIndex = 1382
        Me.MyLabel8.Text = "Item Type"
        '
        'FrmPhysicalStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(674, 494)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmPhysicalStock"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Physical Stock"
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.txtSelfLifeDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtManualBatchNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMilk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtsubLocName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnexport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnimport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents dtpdate As common.Controls.MyDateTimePicker
    Friend WithEvents lbldate As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents txtLocName As common.Controls.MyLabel
    Friend WithEvents txtsubLocName As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtsubLoc As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtdesc As common.Controls.MyTextBox
    Friend WithEvents chkMilk As common.Controls.MyCheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnimport As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtManualBatchNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtSelfLifeDays As common.MyNumBox
    Friend WithEvents txtItemType As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtInventoryAccount As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
End Class

