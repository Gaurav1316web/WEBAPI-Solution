<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProcessProductionReturn
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UsLock1 = New common.usLock()
        Me.cmbDocumentType = New common.Controls.MyComboBox()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.LblTransferDocNo = New common.Controls.MyLabel()
        Me.GrpTransfer = New System.Windows.Forms.GroupBox()
        Me.lblProfitCenterCode = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.TxtManualBatchNo = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.dtpProdDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblCostCenterName = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.LblCostCenterCode = New common.Controls.MyLabel()
        Me.lblConsmSectionLocCode = New common.Controls.MyLabel()
        Me.lblProfitCenterName = New common.Controls.MyLabel()
        Me.lblConsmSectionCode = New common.Controls.MyLabel()
        Me.lblLineNo = New common.Controls.MyLabel()
        Me.txtBatchNo = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.lblBatchDate = New common.Controls.MyLabel()
        Me.dtpBatchDate = New common.Controls.MyDateTimePicker()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.fndProdNo = New common.UserControls.txtFinder()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.txtRmks = New common.Controls.MyTextBox()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        Me.btnShowInventory = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.cmbDocumentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblTransferDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTransfer.SuspendLayout()
        CType(Me.lblProfitCenterCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtManualBatchNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpProdDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCostCenterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblCostCenterCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConsmSectionLocCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProfitCenterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConsmSectionCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLineNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBatchDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpBatchDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRmks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        Me.pvpCustomFields.SuspendLayout()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnShowInventory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(884, 452)
        Me.SplitContainer1.SplitterDistance = 420
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(884, 420)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.cmbDocumentType)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel18)
        Me.RadPageViewPage1.Controls.Add(Me.LblTransferDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.GrpTransfer)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.fndProdNo)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtRmks)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(107.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(863, 374)
        Me.RadPageViewPage1.Text = "Production Return"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(508, -1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(114, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 54
        '
        'cmbDocumentType
        '
        Me.cmbDocumentType.CalculationExpression = Nothing
        Me.cmbDocumentType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbDocumentType.FieldCode = Nothing
        Me.cmbDocumentType.FieldDesc = Nothing
        Me.cmbDocumentType.FieldMaxLength = 0
        Me.cmbDocumentType.FieldName = Nothing
        Me.cmbDocumentType.isCalculatedField = False
        Me.cmbDocumentType.IsSourceFromTable = False
        Me.cmbDocumentType.IsSourceFromValueList = False
        Me.cmbDocumentType.IsUnique = False
        Me.cmbDocumentType.Location = New System.Drawing.Point(130, 47)
        Me.cmbDocumentType.MendatroryField = True
        Me.cmbDocumentType.MyLinkLable1 = Nothing
        Me.cmbDocumentType.MyLinkLable2 = Nothing
        Me.cmbDocumentType.Name = "cmbDocumentType"
        Me.cmbDocumentType.ReferenceFieldDesc = Nothing
        Me.cmbDocumentType.ReferenceFieldName = Nothing
        Me.cmbDocumentType.ReferenceTableName = Nothing
        Me.cmbDocumentType.Size = New System.Drawing.Size(153, 20)
        Me.cmbDocumentType.TabIndex = 52
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(6, 47)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel18.TabIndex = 53
        Me.MyLabel18.Text = "Document Type"
        '
        'LblTransferDocNo
        '
        Me.LblTransferDocNo.FieldName = Nothing
        Me.LblTransferDocNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTransferDocNo.Location = New System.Drawing.Point(373, 47)
        Me.LblTransferDocNo.Name = "LblTransferDocNo"
        Me.LblTransferDocNo.Size = New System.Drawing.Size(44, 16)
        Me.LblTransferDocNo.TabIndex = 19
        Me.LblTransferDocNo.Text = "Doc No"
        '
        'GrpTransfer
        '
        Me.GrpTransfer.Controls.Add(Me.lblProfitCenterCode)
        Me.GrpTransfer.Controls.Add(Me.MyLabel5)
        Me.GrpTransfer.Controls.Add(Me.MyLabel13)
        Me.GrpTransfer.Controls.Add(Me.TxtManualBatchNo)
        Me.GrpTransfer.Controls.Add(Me.MyLabel4)
        Me.GrpTransfer.Controls.Add(Me.MyLabel16)
        Me.GrpTransfer.Controls.Add(Me.dtpProdDate)
        Me.GrpTransfer.Controls.Add(Me.MyLabel19)
        Me.GrpTransfer.Controls.Add(Me.MyLabel3)
        Me.GrpTransfer.Controls.Add(Me.lblCostCenterName)
        Me.GrpTransfer.Controls.Add(Me.MyLabel1)
        Me.GrpTransfer.Controls.Add(Me.LblCostCenterCode)
        Me.GrpTransfer.Controls.Add(Me.lblConsmSectionLocCode)
        Me.GrpTransfer.Controls.Add(Me.lblProfitCenterName)
        Me.GrpTransfer.Controls.Add(Me.lblConsmSectionCode)
        Me.GrpTransfer.Controls.Add(Me.lblLineNo)
        Me.GrpTransfer.Controls.Add(Me.txtBatchNo)
        Me.GrpTransfer.Controls.Add(Me.MyLabel20)
        Me.GrpTransfer.Controls.Add(Me.txtDesc)
        Me.GrpTransfer.Controls.Add(Me.lblBatchDate)
        Me.GrpTransfer.Controls.Add(Me.dtpBatchDate)
        Me.GrpTransfer.Controls.Add(Me.lblLocation)
        Me.GrpTransfer.Controls.Add(Me.RadLabel5)
        Me.GrpTransfer.Controls.Add(Me.txtLocation)
        Me.GrpTransfer.Controls.Add(Me.RadLabel6)
        Me.GrpTransfer.Location = New System.Drawing.Point(0, 73)
        Me.GrpTransfer.Name = "GrpTransfer"
        Me.GrpTransfer.Size = New System.Drawing.Size(863, 301)
        Me.GrpTransfer.TabIndex = 3
        Me.GrpTransfer.TabStop = False
        Me.GrpTransfer.Text = "Production Details"
        '
        'lblProfitCenterCode
        '
        Me.lblProfitCenterCode.AutoSize = False
        Me.lblProfitCenterCode.BorderVisible = True
        Me.lblProfitCenterCode.FieldName = Nothing
        Me.lblProfitCenterCode.Location = New System.Drawing.Point(130, 226)
        Me.lblProfitCenterCode.Name = "lblProfitCenterCode"
        Me.lblProfitCenterCode.Size = New System.Drawing.Size(153, 19)
        Me.lblProfitCenterCode.TabIndex = 91
        Me.lblProfitCenterCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(6, 50)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel5.TabIndex = 41
        Me.MyLabel5.Text = "Batch Code"
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(6, 163)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(93, 16)
        Me.MyLabel13.TabIndex = 89
        Me.MyLabel13.Text = "Manual Batch No"
        '
        'TxtManualBatchNo
        '
        Me.TxtManualBatchNo.AutoSize = False
        Me.TxtManualBatchNo.BorderVisible = True
        Me.TxtManualBatchNo.FieldName = Nothing
        Me.TxtManualBatchNo.Location = New System.Drawing.Point(130, 163)
        Me.TxtManualBatchNo.Name = "TxtManualBatchNo"
        Me.TxtManualBatchNo.Size = New System.Drawing.Size(361, 19)
        Me.TxtManualBatchNo.TabIndex = 90
        Me.TxtManualBatchNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(6, 25)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel4.TabIndex = 40
        Me.MyLabel4.Text = "Production Date"
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel16.Location = New System.Drawing.Point(6, 204)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(94, 18)
        Me.MyLabel16.TabIndex = 97
        Me.MyLabel16.Text = "Cost Center Code"
        '
        'dtpProdDate
        '
        Me.dtpProdDate.CalculationExpression = Nothing
        Me.dtpProdDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpProdDate.FieldCode = Nothing
        Me.dtpProdDate.FieldDesc = Nothing
        Me.dtpProdDate.FieldMaxLength = 0
        Me.dtpProdDate.FieldName = Nothing
        Me.dtpProdDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpProdDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpProdDate.isCalculatedField = False
        Me.dtpProdDate.IsSourceFromTable = False
        Me.dtpProdDate.IsSourceFromValueList = False
        Me.dtpProdDate.IsUnique = False
        Me.dtpProdDate.Location = New System.Drawing.Point(130, 23)
        Me.dtpProdDate.MendatroryField = False
        Me.dtpProdDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpProdDate.MyLinkLable1 = Me.MyLabel4
        Me.dtpProdDate.MyLinkLable2 = Nothing
        Me.dtpProdDate.Name = "dtpProdDate"
        Me.dtpProdDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpProdDate.ReadOnly = True
        Me.dtpProdDate.ReferenceFieldDesc = Nothing
        Me.dtpProdDate.ReferenceFieldName = Nothing
        Me.dtpProdDate.ReferenceTableName = Nothing
        Me.dtpProdDate.Size = New System.Drawing.Size(79, 18)
        Me.dtpProdDate.TabIndex = 39
        Me.dtpProdDate.TabStop = False
        Me.dtpProdDate.Text = "13/06/2011"
        Me.dtpProdDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(6, 183)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel19.TabIndex = 96
        Me.MyLabel19.Text = "Line No"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(6, 93)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel3.TabIndex = 38
        Me.MyLabel3.Text = "Consm. Location"
        '
        'lblCostCenterName
        '
        Me.lblCostCenterName.AutoSize = False
        Me.lblCostCenterName.BorderVisible = True
        Me.lblCostCenterName.FieldName = Nothing
        Me.lblCostCenterName.Location = New System.Drawing.Point(292, 204)
        Me.lblCostCenterName.Name = "lblCostCenterName"
        Me.lblCostCenterName.Size = New System.Drawing.Size(389, 19)
        Me.lblCostCenterName.TabIndex = 98
        Me.lblCostCenterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 117)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel1.TabIndex = 37
        Me.MyLabel1.Text = "Consm. Section"
        '
        'LblCostCenterCode
        '
        Me.LblCostCenterCode.AutoSize = False
        Me.LblCostCenterCode.BorderVisible = True
        Me.LblCostCenterCode.FieldName = Nothing
        Me.LblCostCenterCode.Location = New System.Drawing.Point(130, 204)
        Me.LblCostCenterCode.Name = "LblCostCenterCode"
        Me.LblCostCenterCode.Size = New System.Drawing.Size(153, 19)
        Me.LblCostCenterCode.TabIndex = 92
        Me.LblCostCenterCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblConsmSectionLocCode
        '
        Me.lblConsmSectionLocCode.AutoSize = False
        Me.lblConsmSectionLocCode.BorderVisible = True
        Me.lblConsmSectionLocCode.FieldName = Nothing
        Me.lblConsmSectionLocCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConsmSectionLocCode.Location = New System.Drawing.Point(130, 92)
        Me.lblConsmSectionLocCode.Name = "lblConsmSectionLocCode"
        Me.lblConsmSectionLocCode.Size = New System.Drawing.Size(361, 20)
        Me.lblConsmSectionLocCode.TabIndex = 36
        Me.lblConsmSectionLocCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblProfitCenterName
        '
        Me.lblProfitCenterName.AutoSize = False
        Me.lblProfitCenterName.BorderVisible = True
        Me.lblProfitCenterName.FieldName = Nothing
        Me.lblProfitCenterName.Location = New System.Drawing.Point(292, 226)
        Me.lblProfitCenterName.Name = "lblProfitCenterName"
        Me.lblProfitCenterName.Size = New System.Drawing.Size(389, 19)
        Me.lblProfitCenterName.TabIndex = 94
        Me.lblProfitCenterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblConsmSectionCode
        '
        Me.lblConsmSectionCode.AutoSize = False
        Me.lblConsmSectionCode.BorderVisible = True
        Me.lblConsmSectionCode.FieldName = Nothing
        Me.lblConsmSectionCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConsmSectionCode.Location = New System.Drawing.Point(130, 116)
        Me.lblConsmSectionCode.Name = "lblConsmSectionCode"
        Me.lblConsmSectionCode.Size = New System.Drawing.Size(361, 20)
        Me.lblConsmSectionCode.TabIndex = 35
        Me.lblConsmSectionCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLineNo
        '
        Me.lblLineNo.AutoSize = False
        Me.lblLineNo.BorderVisible = True
        Me.lblLineNo.FieldName = Nothing
        Me.lblLineNo.Location = New System.Drawing.Point(130, 183)
        Me.lblLineNo.Name = "lblLineNo"
        Me.lblLineNo.Size = New System.Drawing.Size(153, 19)
        Me.lblLineNo.TabIndex = 93
        Me.lblLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBatchNo
        '
        Me.txtBatchNo.CalculationExpression = Nothing
        Me.txtBatchNo.FieldCode = Nothing
        Me.txtBatchNo.FieldDesc = Nothing
        Me.txtBatchNo.FieldMaxLength = 0
        Me.txtBatchNo.FieldName = Nothing
        Me.txtBatchNo.isCalculatedField = False
        Me.txtBatchNo.IsSourceFromTable = False
        Me.txtBatchNo.IsSourceFromValueList = False
        Me.txtBatchNo.IsUnique = False
        Me.txtBatchNo.Location = New System.Drawing.Point(130, 47)
        Me.txtBatchNo.MendatroryField = True
        Me.txtBatchNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBatchNo.MyLinkLable1 = Nothing
        Me.txtBatchNo.MyLinkLable2 = Me.lblLocation
        Me.txtBatchNo.MyReadOnly = True
        Me.txtBatchNo.MyShowMasterFormButton = False
        Me.txtBatchNo.Name = "txtBatchNo"
        Me.txtBatchNo.ReferenceFieldDesc = Nothing
        Me.txtBatchNo.ReferenceFieldName = Nothing
        Me.txtBatchNo.ReferenceTableName = Nothing
        Me.txtBatchNo.Size = New System.Drawing.Size(190, 19)
        Me.txtBatchNo.TabIndex = 25
        Me.txtBatchNo.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(292, 69)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(199, 20)
        Me.lblLocation.TabIndex = 31
        Me.lblLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel20.Location = New System.Drawing.Point(6, 227)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(99, 18)
        Me.MyLabel20.TabIndex = 95
        Me.MyLabel20.Text = "Profit Center Code"
        '
        'txtDesc
        '
        Me.txtDesc.CalculationExpression = Nothing
        Me.txtDesc.FieldCode = Nothing
        Me.txtDesc.FieldDesc = Nothing
        Me.txtDesc.FieldMaxLength = 0
        Me.txtDesc.FieldName = Nothing
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.isCalculatedField = False
        Me.txtDesc.IsSourceFromTable = False
        Me.txtDesc.IsSourceFromValueList = False
        Me.txtDesc.IsUnique = False
        Me.txtDesc.Location = New System.Drawing.Point(130, 140)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel5
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReadOnly = True
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(361, 18)
        Me.txtDesc.TabIndex = 28
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(6, 141)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel5.TabIndex = 30
        Me.RadLabel5.Text = "Description"
        '
        'lblBatchDate
        '
        Me.lblBatchDate.FieldName = Nothing
        Me.lblBatchDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBatchDate.Location = New System.Drawing.Point(345, 50)
        Me.lblBatchDate.Name = "lblBatchDate"
        Me.lblBatchDate.Size = New System.Drawing.Size(62, 16)
        Me.lblBatchDate.TabIndex = 33
        Me.lblBatchDate.Text = "Batch Date"
        '
        'dtpBatchDate
        '
        Me.dtpBatchDate.CalculationExpression = Nothing
        Me.dtpBatchDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpBatchDate.FieldCode = Nothing
        Me.dtpBatchDate.FieldDesc = Nothing
        Me.dtpBatchDate.FieldMaxLength = 0
        Me.dtpBatchDate.FieldName = Nothing
        Me.dtpBatchDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpBatchDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBatchDate.isCalculatedField = False
        Me.dtpBatchDate.IsSourceFromTable = False
        Me.dtpBatchDate.IsSourceFromValueList = False
        Me.dtpBatchDate.IsUnique = False
        Me.dtpBatchDate.Location = New System.Drawing.Point(412, 48)
        Me.dtpBatchDate.MendatroryField = False
        Me.dtpBatchDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBatchDate.MyLinkLable1 = Me.lblBatchDate
        Me.dtpBatchDate.MyLinkLable2 = Nothing
        Me.dtpBatchDate.Name = "dtpBatchDate"
        Me.dtpBatchDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBatchDate.ReadOnly = True
        Me.dtpBatchDate.ReferenceFieldDesc = Nothing
        Me.dtpBatchDate.ReferenceFieldName = Nothing
        Me.dtpBatchDate.ReferenceTableName = Nothing
        Me.dtpBatchDate.Size = New System.Drawing.Size(79, 18)
        Me.dtpBatchDate.TabIndex = 26
        Me.dtpBatchDate.TabStop = False
        Me.dtpBatchDate.Text = "13/06/2011"
        Me.dtpBatchDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
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
        Me.txtLocation.Location = New System.Drawing.Point(130, 71)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel6
        Me.txtLocation.MyLinkLable2 = Me.lblLocation
        Me.txtLocation.MyReadOnly = True
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(154, 18)
        Me.txtLocation.TabIndex = 27
        Me.txtLocation.Value = ""
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(6, 73)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel6.TabIndex = 32
        Me.RadLabel6.Text = "Location"
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(6, 25)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel8.TabIndex = 17
        Me.RadLabel8.Text = "Remarks"
        '
        'fndProdNo
        '
        Me.fndProdNo.CalculationExpression = Nothing
        Me.fndProdNo.FieldCode = Nothing
        Me.fndProdNo.FieldDesc = Nothing
        Me.fndProdNo.FieldMaxLength = 0
        Me.fndProdNo.FieldName = Nothing
        Me.fndProdNo.isCalculatedField = False
        Me.fndProdNo.IsSourceFromTable = False
        Me.fndProdNo.IsSourceFromValueList = False
        Me.fndProdNo.IsUnique = False
        Me.fndProdNo.Location = New System.Drawing.Point(423, 47)
        Me.fndProdNo.MendatroryField = True
        Me.fndProdNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndProdNo.MyLinkLable1 = Me.LblTransferDocNo
        Me.fndProdNo.MyLinkLable2 = Nothing
        Me.fndProdNo.MyReadOnly = False
        Me.fndProdNo.MyShowMasterFormButton = False
        Me.fndProdNo.Name = "fndProdNo"
        Me.fndProdNo.ReferenceFieldDesc = Nothing
        Me.fndProdNo.ReferenceFieldName = Nothing
        Me.fndProdNo.ReferenceTableName = Nothing
        Me.fndProdNo.Size = New System.Drawing.Size(220, 18)
        Me.fndProdNo.TabIndex = 0
        Me.fndProdNo.Value = ""
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(389, 1)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 21
        Me.RadLabel4.Text = "Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(6, 1)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(103, 16)
        Me.RadLabel1.TabIndex = 20
        Me.RadLabel1.Text = "Transfer Return No"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(130, 1)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.RadLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(230, 18)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'txtRmks
        '
        Me.txtRmks.CalculationExpression = Nothing
        Me.txtRmks.FieldCode = Nothing
        Me.txtRmks.FieldDesc = Nothing
        Me.txtRmks.FieldMaxLength = 0
        Me.txtRmks.FieldName = Nothing
        Me.txtRmks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRmks.isCalculatedField = False
        Me.txtRmks.IsSourceFromTable = False
        Me.txtRmks.IsSourceFromValueList = False
        Me.txtRmks.IsUnique = False
        Me.txtRmks.Location = New System.Drawing.Point(130, 25)
        Me.txtRmks.MaxLength = 200
        Me.txtRmks.MendatroryField = True
        Me.txtRmks.MyLinkLable1 = Me.RadLabel8
        Me.txtRmks.MyLinkLable2 = Nothing
        Me.txtRmks.Name = "txtRmks"
        Me.txtRmks.ReferenceFieldDesc = Nothing
        Me.txtRmks.ReferenceFieldName = Nothing
        Me.txtRmks.ReferenceTableName = Nothing
        Me.txtRmks.Size = New System.Drawing.Size(508, 18)
        Me.txtRmks.TabIndex = 2
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(423, 1)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(79, 18)
        Me.txtDate.TabIndex = 1
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(365, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 18)
        Me.btnAddNew.TabIndex = 1
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 31)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(863, 378)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(863, 378)
        Me.UcAttachment1.TabIndex = 0
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(89.0!, 26.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 35)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(863, 374)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(863, 374)
        Me.UcCustomFields1.TabIndex = 1
        '
        'btnShowInventory
        '
        Me.btnShowInventory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnShowInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowInventory.Location = New System.Drawing.Point(209, 4)
        Me.btnShowInventory.Name = "btnShowInventory"
        Me.btnShowInventory.Size = New System.Drawing.Size(121, 22)
        Me.btnShowInventory.TabIndex = 4
        Me.btnShowInventory.Text = "Show Inventory"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(73, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(63, 22)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(140, 4)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(63, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(810, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(4, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(63, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(337, 4)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(71, 22)
        Me.btnHistory.TabIndex = 39
        Me.btnHistory.Text = "&History"
        '
        'frmProcessProductionReturn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 452)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmProcessProductionReturn"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Production Return"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.cmbDocumentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblTransferDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpTransfer.ResumeLayout(False)
        Me.GrpTransfer.PerformLayout()
        CType(Me.lblProfitCenterCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtManualBatchNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpProdDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCostCenterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblCostCenterCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConsmSectionLocCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProfitCenterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConsmSectionCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLineNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBatchDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpBatchDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRmks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        Me.pvpCustomFields.ResumeLayout(False)
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtRmks As common.Controls.MyTextBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents LblTransferDocNo As common.Controls.MyLabel
    Friend WithEvents fndProdNo As common.UserControls.txtFinder
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents GrpTransfer As System.Windows.Forms.GroupBox
    Friend WithEvents cmbDocumentType As common.Controls.MyComboBox
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblConsmSectionLocCode As common.Controls.MyLabel
    Friend WithEvents lblConsmSectionCode As common.Controls.MyLabel
    Friend WithEvents txtBatchNo As common.UserControls.txtFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents lblBatchDate As common.Controls.MyLabel
    Friend WithEvents dtpBatchDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents dtpProdDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblProfitCenterCode As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents TxtManualBatchNo As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents lblCostCenterName As common.Controls.MyLabel
    Friend WithEvents LblCostCenterCode As common.Controls.MyLabel
    Friend WithEvents lblProfitCenterName As common.Controls.MyLabel
    Friend WithEvents lblLineNo As common.Controls.MyLabel
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents btnShowInventory As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton

End Class

