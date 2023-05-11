<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProductionEntry
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
        Me.chkJobWorkInward = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblProfitCenterCode = New common.Controls.MyLabel()
        Me.LblCostCenterCode = New common.Controls.MyLabel()
        Me.lblLineNo = New common.Controls.MyLabel()
        Me.lblCostCenterName = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.lblProfitCenterName = New common.Controls.MyLabel()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.TxtManualBatchNo = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblConsmSectionLocCode = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblConsmSectionCode = New common.Controls.MyLabel()
        Me.lblBatchNo = New common.Controls.MyLabel()
        Me.lblReceiptCode = New common.Controls.MyLabel()
        Me.txtBatchNo = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.lblEmpName = New common.Controls.MyLabel()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.lblReceivedBy = New common.Controls.MyLabel()
        Me.dtpDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.txtReceivedBy = New common.UserControls.txtFinder()
        Me.txtComment = New common.Controls.MyTextBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.lblBatchDate = New common.Controls.MyLabel()
        Me.dtpBatchDate = New common.Controls.MyDateTimePicker()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.UsLock1 = New common.usLock()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.pageIssueDetail = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvIssue = New common.UserControls.MyRadGridView()
        Me.pageStageDetail = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvStage = New common.UserControls.MyRadGridView()
        Me.pageBatchProduction = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvBatch = New common.UserControls.MyRadGridView()
        Me.pageParameterDetail = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvParameter = New common.UserControls.MyRadGridView()
        Me.pageWreckageAndFlashing = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvWreckage = New common.UserControls.MyRadGridView()
        Me.pageScrapDetail = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GvScrap = New common.UserControls.MyRadGridView()
        Me.pageoverheadcost = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvProductionCost = New common.UserControls.MyRadGridView()
        Me.pageSectionStock = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvSectionStock = New common.UserControls.MyRadGridView()
        Me.pageSectionStockHistory = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvSectionStockHistory = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnShowInventory = New Telerik.WinControls.UI.RadButton()
        Me.btnunpost = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.chkJobWorkInward, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProfitCenterCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblCostCenterCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLineNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCostCenterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProfitCenterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtManualBatchNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConsmSectionLocCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConsmSectionCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBatchNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReceiptCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReceivedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBatchDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpBatchDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.pageIssueDetail.SuspendLayout()
        CType(Me.gvIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvIssue.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageStageDetail.SuspendLayout()
        CType(Me.gvStage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvStage.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageBatchProduction.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gvBatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvBatch.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageParameterDetail.SuspendLayout()
        CType(Me.gvParameter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvParameter.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageWreckageAndFlashing.SuspendLayout()
        CType(Me.gvWreckage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvWreckage.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageScrapDetail.SuspendLayout()
        CType(Me.GvScrap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GvScrap.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageoverheadcost.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.gvProductionCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvProductionCost.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageSectionStock.SuspendLayout()
        CType(Me.gvSectionStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSectionStock.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageSectionStockHistory.SuspendLayout()
        CType(Me.gvSectionStockHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSectionStockHistory.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnunpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnShowInventory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnunpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(982, 525)
        Me.SplitContainer1.SplitterDistance = 483
        Me.SplitContainer1.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkJobWorkInward)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblProfitCenterCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.LblCostCenterCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLineNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCostCenterName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel10)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblProfitCenterName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel11)
        Me.SplitContainer2.Panel1.Controls.Add(Me.TxtManualBatchNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel13)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblConsmSectionLocCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblConsmSectionCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBatchNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblReceiptCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtBatchNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblEmpName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblReceivedBy)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtReceivedBy)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtComment)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBatchDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpBatchDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel6)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Size = New System.Drawing.Size(982, 483)
        Me.SplitContainer2.SplitterDistance = 199
        Me.SplitContainer2.TabIndex = 2
        '
        'chkJobWorkInward
        '
        Me.chkJobWorkInward.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkJobWorkInward.Location = New System.Drawing.Point(572, 31)
        Me.chkJobWorkInward.Name = "chkJobWorkInward"
        Me.chkJobWorkInward.ReadOnly = True
        Me.chkJobWorkInward.Size = New System.Drawing.Size(105, 16)
        Me.chkJobWorkInward.TabIndex = 93
        Me.chkJobWorkInward.Text = "Job Work Inward"
        '
        'lblProfitCenterCode
        '
        Me.lblProfitCenterCode.AutoSize = False
        Me.lblProfitCenterCode.BorderVisible = True
        Me.lblProfitCenterCode.FieldName = Nothing
        Me.lblProfitCenterCode.Location = New System.Drawing.Point(112, 162)
        Me.lblProfitCenterCode.Name = "lblProfitCenterCode"
        Me.lblProfitCenterCode.Size = New System.Drawing.Size(240, 19)
        Me.lblProfitCenterCode.TabIndex = 81
        Me.lblProfitCenterCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCostCenterCode
        '
        Me.LblCostCenterCode.AutoSize = False
        Me.LblCostCenterCode.BorderVisible = True
        Me.LblCostCenterCode.FieldName = Nothing
        Me.LblCostCenterCode.Location = New System.Drawing.Point(112, 140)
        Me.LblCostCenterCode.Name = "LblCostCenterCode"
        Me.LblCostCenterCode.Size = New System.Drawing.Size(240, 19)
        Me.LblCostCenterCode.TabIndex = 82
        Me.LblCostCenterCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLineNo
        '
        Me.lblLineNo.AutoSize = False
        Me.lblLineNo.BorderVisible = True
        Me.lblLineNo.FieldName = Nothing
        Me.lblLineNo.Location = New System.Drawing.Point(112, 118)
        Me.lblLineNo.Name = "lblLineNo"
        Me.lblLineNo.Size = New System.Drawing.Size(240, 19)
        Me.lblLineNo.TabIndex = 83
        Me.lblLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCostCenterName
        '
        Me.lblCostCenterName.AutoSize = False
        Me.lblCostCenterName.BorderVisible = True
        Me.lblCostCenterName.FieldName = Nothing
        Me.lblCostCenterName.Location = New System.Drawing.Point(356, 140)
        Me.lblCostCenterName.Name = "lblCostCenterName"
        Me.lblCostCenterName.Size = New System.Drawing.Size(389, 19)
        Me.lblCostCenterName.TabIndex = 88
        Me.lblCostCenterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(7, 140)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(94, 18)
        Me.MyLabel8.TabIndex = 87
        Me.MyLabel8.Text = "Cost Center Code"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(7, 119)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel10.TabIndex = 86
        Me.MyLabel10.Text = "Line No"
        '
        'lblProfitCenterName
        '
        Me.lblProfitCenterName.AutoSize = False
        Me.lblProfitCenterName.BorderVisible = True
        Me.lblProfitCenterName.FieldName = Nothing
        Me.lblProfitCenterName.Location = New System.Drawing.Point(356, 162)
        Me.lblProfitCenterName.Name = "lblProfitCenterName"
        Me.lblProfitCenterName.Size = New System.Drawing.Size(389, 19)
        Me.lblProfitCenterName.TabIndex = 84
        Me.lblProfitCenterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel11.Location = New System.Drawing.Point(7, 162)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(99, 18)
        Me.MyLabel11.TabIndex = 85
        Me.MyLabel11.Text = "Profit Center Code"
        '
        'TxtManualBatchNo
        '
        Me.TxtManualBatchNo.AutoSize = False
        Me.TxtManualBatchNo.BorderVisible = True
        Me.TxtManualBatchNo.FieldName = Nothing
        Me.TxtManualBatchNo.Location = New System.Drawing.Point(427, 97)
        Me.TxtManualBatchNo.Name = "TxtManualBatchNo"
        Me.TxtManualBatchNo.Size = New System.Drawing.Size(318, 19)
        Me.TxtManualBatchNo.TabIndex = 67
        Me.TxtManualBatchNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(356, 98)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel13.TabIndex = 66
        Me.MyLabel13.Text = "M. Batch No"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(7, 76)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel1.TabIndex = 24
        Me.MyLabel1.Text = "Consm. Section"
        '
        'lblConsmSectionLocCode
        '
        Me.lblConsmSectionLocCode.AutoSize = False
        Me.lblConsmSectionLocCode.BorderVisible = True
        Me.lblConsmSectionLocCode.FieldName = Nothing
        Me.lblConsmSectionLocCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConsmSectionLocCode.Location = New System.Drawing.Point(112, 51)
        Me.lblConsmSectionLocCode.Name = "lblConsmSectionLocCode"
        Me.lblConsmSectionLocCode.Size = New System.Drawing.Size(240, 20)
        Me.lblConsmSectionLocCode.TabIndex = 23
        Me.lblConsmSectionLocCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(7, 53)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel2.TabIndex = 22
        Me.MyLabel2.Text = "Consm. Location"
        '
        'lblConsmSectionCode
        '
        Me.lblConsmSectionCode.AutoSize = False
        Me.lblConsmSectionCode.BorderVisible = True
        Me.lblConsmSectionCode.FieldName = Nothing
        Me.lblConsmSectionCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConsmSectionCode.Location = New System.Drawing.Point(112, 74)
        Me.lblConsmSectionCode.Name = "lblConsmSectionCode"
        Me.lblConsmSectionCode.Size = New System.Drawing.Size(240, 20)
        Me.lblConsmSectionCode.TabIndex = 21
        Me.lblConsmSectionCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBatchNo
        '
        Me.lblBatchNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblBatchNo.FieldName = Nothing
        Me.lblBatchNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBatchNo.Location = New System.Drawing.Point(7, 31)
        Me.lblBatchNo.Name = "lblBatchNo"
        Me.lblBatchNo.Size = New System.Drawing.Size(53, 16)
        Me.lblBatchNo.TabIndex = 17
        Me.lblBatchNo.Text = "Batch No"
        '
        'lblReceiptCode
        '
        Me.lblReceiptCode.FieldName = Nothing
        Me.lblReceiptCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReceiptCode.Location = New System.Drawing.Point(7, 9)
        Me.lblReceiptCode.Name = "lblReceiptCode"
        Me.lblReceiptCode.Size = New System.Drawing.Size(75, 16)
        Me.lblReceiptCode.TabIndex = 18
        Me.lblReceiptCode.Text = "Receipt Code"
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
        Me.txtBatchNo.Location = New System.Drawing.Point(112, 30)
        Me.txtBatchNo.MendatroryField = True
        Me.txtBatchNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBatchNo.MyLinkLable1 = Me.lblBatchNo
        Me.txtBatchNo.MyLinkLable2 = Me.lblLocation
        Me.txtBatchNo.MyReadOnly = False
        Me.txtBatchNo.MyShowMasterFormButton = False
        Me.txtBatchNo.Name = "txtBatchNo"
        Me.txtBatchNo.ReferenceFieldDesc = Nothing
        Me.txtBatchNo.ReferenceFieldName = Nothing
        Me.txtBatchNo.ReferenceTableName = Nothing
        Me.txtBatchNo.Size = New System.Drawing.Size(240, 18)
        Me.txtBatchNo.TabIndex = 3
        Me.txtBatchNo.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(572, 51)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(173, 20)
        Me.lblLocation.TabIndex = 14
        Me.lblLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(330, 7)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(22, 20)
        Me.btnAddNew.TabIndex = 1
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.FieldName = Nothing
        Me.lblEmpName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpName.Location = New System.Drawing.Point(572, 74)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(173, 20)
        Me.lblEmpName.TabIndex = 13
        Me.lblEmpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.txtDesc.Location = New System.Drawing.Point(112, 97)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel5
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(240, 18)
        Me.txtDesc.TabIndex = 6
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(7, 98)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel5.TabIndex = 11
        Me.RadLabel5.Text = "Description"
        '
        'lblReceivedBy
        '
        Me.lblReceivedBy.FieldName = Nothing
        Me.lblReceivedBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReceivedBy.Location = New System.Drawing.Point(356, 76)
        Me.lblReceivedBy.Name = "lblReceivedBy"
        Me.lblReceivedBy.Size = New System.Drawing.Size(70, 16)
        Me.lblReceivedBy.TabIndex = 12
        Me.lblReceivedBy.Text = "Received By"
        '
        'dtpDate
        '
        Me.dtpDate.CalculationExpression = Nothing
        Me.dtpDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDate.FieldCode = Nothing
        Me.dtpDate.FieldDesc = Nothing
        Me.dtpDate.FieldMaxLength = 0
        Me.dtpDate.FieldName = Nothing
        Me.dtpDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.isCalculatedField = False
        Me.dtpDate.IsSourceFromTable = False
        Me.dtpDate.IsSourceFromValueList = False
        Me.dtpDate.IsUnique = False
        Me.dtpDate.Location = New System.Drawing.Point(427, 8)
        Me.dtpDate.MendatroryField = False
        Me.dtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.MyLinkLable1 = Me.RadLabel4
        Me.dtpDate.MyLinkLable2 = Nothing
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.ReferenceFieldDesc = Nothing
        Me.dtpDate.ReferenceFieldName = Nothing
        Me.dtpDate.ReferenceTableName = Nothing
        Me.dtpDate.Size = New System.Drawing.Size(139, 18)
        Me.dtpDate.TabIndex = 2
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "13/06/2011"
        Me.dtpDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(356, 9)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 19
        Me.RadLabel4.Text = "Date"
        '
        'txtReceivedBy
        '
        Me.txtReceivedBy.CalculationExpression = Nothing
        Me.txtReceivedBy.FieldCode = Nothing
        Me.txtReceivedBy.FieldDesc = Nothing
        Me.txtReceivedBy.FieldMaxLength = 0
        Me.txtReceivedBy.FieldName = Nothing
        Me.txtReceivedBy.isCalculatedField = False
        Me.txtReceivedBy.IsSourceFromTable = False
        Me.txtReceivedBy.IsSourceFromValueList = False
        Me.txtReceivedBy.IsUnique = False
        Me.txtReceivedBy.Location = New System.Drawing.Point(427, 76)
        Me.txtReceivedBy.MendatroryField = True
        Me.txtReceivedBy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReceivedBy.MyLinkLable1 = Me.lblReceivedBy
        Me.txtReceivedBy.MyLinkLable2 = Me.lblEmpName
        Me.txtReceivedBy.MyReadOnly = False
        Me.txtReceivedBy.MyShowMasterFormButton = False
        Me.txtReceivedBy.Name = "txtReceivedBy"
        Me.txtReceivedBy.ReferenceFieldDesc = Nothing
        Me.txtReceivedBy.ReferenceFieldName = Nothing
        Me.txtReceivedBy.ReferenceTableName = Nothing
        Me.txtReceivedBy.Size = New System.Drawing.Size(139, 17)
        Me.txtReceivedBy.TabIndex = 7
        Me.txtReceivedBy.Value = ""
        '
        'txtComment
        '
        Me.txtComment.CalculationExpression = Nothing
        Me.txtComment.FieldCode = Nothing
        Me.txtComment.FieldDesc = Nothing
        Me.txtComment.FieldMaxLength = 0
        Me.txtComment.FieldName = Nothing
        Me.txtComment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComment.isCalculatedField = False
        Me.txtComment.IsSourceFromTable = False
        Me.txtComment.IsSourceFromValueList = False
        Me.txtComment.IsUnique = False
        Me.txtComment.Location = New System.Drawing.Point(427, 118)
        Me.txtComment.MaxLength = 200
        Me.txtComment.MendatroryField = False
        Me.txtComment.MyLinkLable1 = Me.RadLabel2
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        Me.txtComment.Size = New System.Drawing.Size(318, 18)
        Me.txtComment.TabIndex = 8
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(356, 119)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel2.TabIndex = 10
        Me.RadLabel2.Text = "Comment"
        '
        'lblBatchDate
        '
        Me.lblBatchDate.FieldName = Nothing
        Me.lblBatchDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBatchDate.Location = New System.Drawing.Point(356, 31)
        Me.lblBatchDate.Name = "lblBatchDate"
        Me.lblBatchDate.Size = New System.Drawing.Size(30, 16)
        Me.lblBatchDate.TabIndex = 16
        Me.lblBatchDate.Text = "Date"
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
        Me.dtpBatchDate.Location = New System.Drawing.Point(427, 30)
        Me.dtpBatchDate.MendatroryField = False
        Me.dtpBatchDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBatchDate.MyLinkLable1 = Me.lblBatchDate
        Me.dtpBatchDate.MyLinkLable2 = Nothing
        Me.dtpBatchDate.Name = "dtpBatchDate"
        Me.dtpBatchDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBatchDate.ReferenceFieldDesc = Nothing
        Me.dtpBatchDate.ReferenceFieldName = Nothing
        Me.dtpBatchDate.ReferenceTableName = Nothing
        Me.dtpBatchDate.Size = New System.Drawing.Size(139, 18)
        Me.dtpBatchDate.TabIndex = 4
        Me.dtpBatchDate.TabStop = False
        Me.dtpBatchDate.Text = "13/06/2011"
        Me.dtpBatchDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(112, 7)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblReceiptCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(218, 20)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(572, 8)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(173, 19)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 20
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
        Me.txtLocation.Location = New System.Drawing.Point(427, 53)
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
        Me.txtLocation.Size = New System.Drawing.Size(139, 17)
        Me.txtLocation.TabIndex = 5
        Me.txtLocation.Value = ""
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(356, 53)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel6.TabIndex = 15
        Me.RadLabel6.Text = "Location"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.pageIssueDetail)
        Me.RadPageView1.Controls.Add(Me.pageStageDetail)
        Me.RadPageView1.Controls.Add(Me.pageBatchProduction)
        Me.RadPageView1.Controls.Add(Me.pageParameterDetail)
        Me.RadPageView1.Controls.Add(Me.pageWreckageAndFlashing)
        Me.RadPageView1.Controls.Add(Me.pageScrapDetail)
        Me.RadPageView1.Controls.Add(Me.pageoverheadcost)
        Me.RadPageView1.Controls.Add(Me.pageSectionStock)
        Me.RadPageView1.Controls.Add(Me.pageSectionStockHistory)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.pageBatchProduction
        Me.RadPageView1.Size = New System.Drawing.Size(982, 280)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(2), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Batch Production"
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(3), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Batch Production"
        '
        'pageIssueDetail
        '
        Me.pageIssueDetail.Controls.Add(Me.gvIssue)
        Me.pageIssueDetail.ItemSize = New System.Drawing.SizeF(75.0!, 26.0!)
        Me.pageIssueDetail.Location = New System.Drawing.Point(10, 35)
        Me.pageIssueDetail.Name = "pageIssueDetail"
        Me.pageIssueDetail.Size = New System.Drawing.Size(961, 234)
        Me.pageIssueDetail.Text = "Issue Detail"
        '
        'gvIssue
        '
        Me.gvIssue.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvIssue.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvIssue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvIssue.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvIssue.ForeColor = System.Drawing.Color.Black
        Me.gvIssue.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvIssue.Location = New System.Drawing.Point(0, 0)
        '
        'gvIssue
        '
        Me.gvIssue.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvIssue.MasterTemplate.AllowAddNewRow = False
        Me.gvIssue.MasterTemplate.AutoGenerateColumns = False
        Me.gvIssue.MasterTemplate.EnableGrouping = False
        Me.gvIssue.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvIssue.Name = "gvIssue"
        Me.gvIssue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvIssue.ShowHeaderCellButtons = True
        Me.gvIssue.Size = New System.Drawing.Size(961, 234)
        Me.gvIssue.TabIndex = 1
        Me.gvIssue.TabStop = False
        Me.gvIssue.Text = "RadGridView1"
        '
        'pageStageDetail
        '
        Me.pageStageDetail.Controls.Add(Me.gvStage)
        Me.pageStageDetail.ItemSize = New System.Drawing.SizeF(78.0!, 26.0!)
        Me.pageStageDetail.Location = New System.Drawing.Point(10, 35)
        Me.pageStageDetail.Name = "pageStageDetail"
        Me.pageStageDetail.Size = New System.Drawing.Size(961, 234)
        Me.pageStageDetail.Text = "Stage Detail"
        '
        'gvStage
        '
        Me.gvStage.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvStage.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvStage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvStage.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvStage.ForeColor = System.Drawing.Color.Black
        Me.gvStage.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvStage.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvStage.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvStage.MasterTemplate.AllowAddNewRow = False
        Me.gvStage.MasterTemplate.AutoGenerateColumns = False
        Me.gvStage.MasterTemplate.EnableGrouping = False
        Me.gvStage.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvStage.Name = "gvStage"
        Me.gvStage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvStage.ShowHeaderCellButtons = True
        Me.gvStage.Size = New System.Drawing.Size(961, 234)
        Me.gvStage.TabIndex = 1
        Me.gvStage.TabStop = False
        Me.gvStage.Text = "RadGridView1"
        '
        'pageBatchProduction
        '
        Me.pageBatchProduction.Controls.Add(Me.RadGroupBox2)
        Me.pageBatchProduction.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pageBatchProduction.ItemSize = New System.Drawing.SizeF(102.0!, 26.0!)
        Me.pageBatchProduction.Location = New System.Drawing.Point(10, 35)
        Me.pageBatchProduction.Name = "pageBatchProduction"
        Me.pageBatchProduction.Size = New System.Drawing.Size(961, 234)
        Me.pageBatchProduction.Text = "Batch Production"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gvBatch)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Received Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(961, 234)
        Me.RadGroupBox2.TabIndex = 9
        Me.RadGroupBox2.Text = "Received Item Details"
        '
        'gvBatch
        '
        Me.gvBatch.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvBatch.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvBatch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvBatch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvBatch.ForeColor = System.Drawing.Color.Black
        Me.gvBatch.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvBatch.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gvBatch.MasterTemplate.AllowAddNewRow = False
        Me.gvBatch.MasterTemplate.AllowDeleteRow = False
        Me.gvBatch.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvBatch.Name = "gvBatch"
        Me.gvBatch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvBatch.ShowGroupPanel = False
        Me.gvBatch.ShowHeaderCellButtons = True
        Me.gvBatch.Size = New System.Drawing.Size(941, 204)
        Me.gvBatch.TabIndex = 0
        Me.gvBatch.TabStop = False
        Me.gvBatch.Text = "RadGridView1"
        '
        'pageParameterDetail
        '
        Me.pageParameterDetail.Controls.Add(Me.gvParameter)
        Me.pageParameterDetail.ItemSize = New System.Drawing.SizeF(87.0!, 26.0!)
        Me.pageParameterDetail.Location = New System.Drawing.Point(10, 35)
        Me.pageParameterDetail.Name = "pageParameterDetail"
        Me.pageParameterDetail.Size = New System.Drawing.Size(961, 234)
        Me.pageParameterDetail.Text = "Quality Check"
        '
        'gvParameter
        '
        Me.gvParameter.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvParameter.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvParameter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvParameter.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvParameter.ForeColor = System.Drawing.Color.Black
        Me.gvParameter.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvParameter.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvParameter.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvParameter.MasterTemplate.AllowAddNewRow = False
        Me.gvParameter.MasterTemplate.AutoGenerateColumns = False
        Me.gvParameter.MasterTemplate.EnableGrouping = False
        Me.gvParameter.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvParameter.Name = "gvParameter"
        Me.gvParameter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvParameter.ShowHeaderCellButtons = True
        Me.gvParameter.Size = New System.Drawing.Size(961, 234)
        Me.gvParameter.TabIndex = 1
        Me.gvParameter.TabStop = False
        Me.gvParameter.Text = "RadGridView1"
        '
        'pageWreckageAndFlashing
        '
        Me.pageWreckageAndFlashing.Controls.Add(Me.gvWreckage)
        Me.pageWreckageAndFlashing.ItemSize = New System.Drawing.SizeF(124.0!, 26.0!)
        Me.pageWreckageAndFlashing.Location = New System.Drawing.Point(10, 35)
        Me.pageWreckageAndFlashing.Name = "pageWreckageAndFlashing"
        Me.pageWreckageAndFlashing.Size = New System.Drawing.Size(961, 234)
        Me.pageWreckageAndFlashing.Text = "Wreckage & Flushing"
        '
        'gvWreckage
        '
        Me.gvWreckage.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvWreckage.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvWreckage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvWreckage.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvWreckage.ForeColor = System.Drawing.Color.Black
        Me.gvWreckage.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvWreckage.Location = New System.Drawing.Point(0, 0)
        '
        'gvWreckage
        '
        Me.gvWreckage.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvWreckage.MasterTemplate.AllowAddNewRow = False
        Me.gvWreckage.MasterTemplate.AutoGenerateColumns = False
        Me.gvWreckage.MasterTemplate.EnableGrouping = False
        Me.gvWreckage.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvWreckage.Name = "gvWreckage"
        Me.gvWreckage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvWreckage.ShowHeaderCellButtons = True
        Me.gvWreckage.Size = New System.Drawing.Size(961, 234)
        Me.gvWreckage.TabIndex = 1
        Me.gvWreckage.TabStop = False
        Me.gvWreckage.Text = "RadGridView1"
        '
        'pageScrapDetail
        '
        Me.pageScrapDetail.Controls.Add(Me.GvScrap)
        Me.pageScrapDetail.ItemSize = New System.Drawing.SizeF(78.0!, 26.0!)
        Me.pageScrapDetail.Location = New System.Drawing.Point(10, 35)
        Me.pageScrapDetail.Name = "pageScrapDetail"
        Me.pageScrapDetail.Size = New System.Drawing.Size(1015, 305)
        Me.pageScrapDetail.Text = "Scrap Detail"
        '
        'GvScrap
        '
        Me.GvScrap.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GvScrap.Cursor = System.Windows.Forms.Cursors.Default
        Me.GvScrap.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GvScrap.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.GvScrap.ForeColor = System.Drawing.Color.Black
        Me.GvScrap.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.GvScrap.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.GvScrap.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.GvScrap.MasterTemplate.AllowAddNewRow = False
        Me.GvScrap.MasterTemplate.AutoGenerateColumns = False
        Me.GvScrap.MasterTemplate.EnableGrouping = False
        Me.GvScrap.MasterTemplate.ShowHeaderCellButtons = True
        Me.GvScrap.Name = "GvScrap"
        Me.GvScrap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GvScrap.ShowHeaderCellButtons = True
        Me.GvScrap.Size = New System.Drawing.Size(1015, 305)
        Me.GvScrap.TabIndex = 2
        Me.GvScrap.TabStop = False
        Me.GvScrap.Text = "RadGridView1"
        '
        'pageoverheadcost
        '
        Me.pageoverheadcost.Controls.Add(Me.RadGroupBox3)
        Me.pageoverheadcost.ItemSize = New System.Drawing.SizeF(92.0!, 26.0!)
        Me.pageoverheadcost.Location = New System.Drawing.Point(10, 35)
        Me.pageoverheadcost.Name = "pageoverheadcost"
        Me.pageoverheadcost.Size = New System.Drawing.Size(1015, 305)
        Me.pageoverheadcost.Text = "Overhead Cost"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.gvProductionCost)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox3.HeaderText = "Overhead Cost details"
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(1015, 305)
        Me.RadGroupBox3.TabIndex = 11
        Me.RadGroupBox3.Text = "Overhead Cost details"
        '
        'gvProductionCost
        '
        Me.gvProductionCost.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvProductionCost.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvProductionCost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvProductionCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvProductionCost.ForeColor = System.Drawing.Color.Black
        Me.gvProductionCost.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvProductionCost.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gvProductionCost.MasterTemplate.AllowAddNewRow = False
        Me.gvProductionCost.MasterTemplate.AllowDeleteRow = False
        Me.gvProductionCost.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvProductionCost.Name = "gvProductionCost"
        Me.gvProductionCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvProductionCost.ShowGroupPanel = False
        Me.gvProductionCost.ShowHeaderCellButtons = True
        Me.gvProductionCost.Size = New System.Drawing.Size(995, 275)
        Me.gvProductionCost.TabIndex = 0
        Me.gvProductionCost.TabStop = False
        Me.gvProductionCost.Text = "RadGridView1"
        '
        'pageSectionStock
        '
        Me.pageSectionStock.Controls.Add(Me.gvSectionStock)
        Me.pageSectionStock.ItemSize = New System.Drawing.SizeF(85.0!, 26.0!)
        Me.pageSectionStock.Location = New System.Drawing.Point(10, 35)
        Me.pageSectionStock.Name = "pageSectionStock"
        Me.pageSectionStock.Size = New System.Drawing.Size(1015, 305)
        Me.pageSectionStock.Text = "Section Stock"
        '
        'gvSectionStock
        '
        Me.gvSectionStock.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvSectionStock.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvSectionStock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvSectionStock.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvSectionStock.ForeColor = System.Drawing.Color.Black
        Me.gvSectionStock.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvSectionStock.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvSectionStock.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvSectionStock.MasterTemplate.AllowAddNewRow = False
        Me.gvSectionStock.MasterTemplate.AutoGenerateColumns = False
        Me.gvSectionStock.MasterTemplate.EnableGrouping = False
        Me.gvSectionStock.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvSectionStock.Name = "gvSectionStock"
        Me.gvSectionStock.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvSectionStock.ShowHeaderCellButtons = True
        Me.gvSectionStock.Size = New System.Drawing.Size(1015, 305)
        Me.gvSectionStock.TabIndex = 2
        Me.gvSectionStock.TabStop = False
        Me.gvSectionStock.Text = "RadGridView1"
        '
        'pageSectionStockHistory
        '
        Me.pageSectionStockHistory.Controls.Add(Me.gvSectionStockHistory)
        Me.pageSectionStockHistory.ItemSize = New System.Drawing.SizeF(124.0!, 26.0!)
        Me.pageSectionStockHistory.Location = New System.Drawing.Point(10, 35)
        Me.pageSectionStockHistory.Name = "pageSectionStockHistory"
        Me.pageSectionStockHistory.Size = New System.Drawing.Size(1015, 305)
        Me.pageSectionStockHistory.Text = "Section Stock History"
        '
        'gvSectionStockHistory
        '
        Me.gvSectionStockHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvSectionStockHistory.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvSectionStockHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvSectionStockHistory.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvSectionStockHistory.ForeColor = System.Drawing.Color.Black
        Me.gvSectionStockHistory.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvSectionStockHistory.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvSectionStockHistory.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvSectionStockHistory.MasterTemplate.AllowAddNewRow = False
        Me.gvSectionStockHistory.MasterTemplate.AutoGenerateColumns = False
        Me.gvSectionStockHistory.MasterTemplate.EnableGrouping = False
        Me.gvSectionStockHistory.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvSectionStockHistory.Name = "gvSectionStockHistory"
        Me.gvSectionStockHistory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvSectionStockHistory.ShowHeaderCellButtons = True
        Me.gvSectionStockHistory.Size = New System.Drawing.Size(1015, 305)
        Me.gvSectionStockHistory.TabIndex = 3
        Me.gvSectionStockHistory.TabStop = False
        Me.gvSectionStockHistory.Text = "RadGridView1"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1015, 305)
        Me.RadPageViewPage1.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(1015, 305)
        Me.UcAttachment1.TabIndex = 2
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(565, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(61, 22)
        Me.btnCancel.TabIndex = 37
        Me.btnCancel.Text = "Cancel"
        '
        'btnHistory
        '
        Me.btnHistory.Location = New System.Drawing.Point(488, 5)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(71, 22)
        Me.btnHistory.TabIndex = 36
        Me.btnHistory.Text = "&History"
        '
        'btnShowInventory
        '
        Me.btnShowInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowInventory.Location = New System.Drawing.Point(387, 5)
        Me.btnShowInventory.Name = "btnShowInventory"
        Me.btnShowInventory.Size = New System.Drawing.Size(95, 22)
        Me.btnShowInventory.TabIndex = 35
        Me.btnShowInventory.Text = "Show Inventory"
        '
        'btnunpost
        '
        Me.btnunpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnunpost.Location = New System.Drawing.Point(305, 5)
        Me.btnunpost.Name = "btnunpost"
        Me.btnunpost.Size = New System.Drawing.Size(76, 22)
        Me.btnunpost.TabIndex = 34
        Me.btnunpost.Text = "Unpost"
        Me.btnunpost.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(230, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(80, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(155, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(856, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'frmProductionEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(982, 525)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmProductionEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Production Entry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.chkJobWorkInward, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProfitCenterCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblCostCenterCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLineNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCostCenterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProfitCenterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtManualBatchNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConsmSectionLocCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConsmSectionCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBatchNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReceiptCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReceivedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBatchDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpBatchDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.pageIssueDetail.ResumeLayout(False)
        CType(Me.gvIssue.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvIssue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageStageDetail.ResumeLayout(False)
        CType(Me.gvStage.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvStage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageBatchProduction.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gvBatch.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvBatch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageParameterDetail.ResumeLayout(False)
        CType(Me.gvParameter.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvParameter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageWreckageAndFlashing.ResumeLayout(False)
        CType(Me.gvWreckage.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvWreckage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageScrapDetail.ResumeLayout(False)
        CType(Me.GvScrap.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GvScrap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageoverheadcost.ResumeLayout(False)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        CType(Me.gvProductionCost.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvProductionCost, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageSectionStock.ResumeLayout(False)
        CType(Me.gvSectionStock.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSectionStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageSectionStockHistory.ResumeLayout(False)
        CType(Me.gvSectionStockHistory.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSectionStockHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnunpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents pageBatchProduction As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents dtpDate As common.Controls.MyDateTimePicker
    Friend WithEvents gvBatch As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpBatchDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblReceiptCode As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblBatchDate As common.Controls.MyLabel
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents lblReceivedBy As common.Controls.MyLabel
    Friend WithEvents txtReceivedBy As common.UserControls.txtFinder
    Friend WithEvents lblBatchNo As common.Controls.MyLabel
    Friend WithEvents txtBatchNo As common.UserControls.txtFinder
    Friend WithEvents pageIssueDetail As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pageStageDetail As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pageParameterDetail As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pageWreckageAndFlashing As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents gvIssue As common.UserControls.MyRadGridView
    Friend WithEvents gvStage As common.UserControls.MyRadGridView
    Friend WithEvents gvParameter As common.UserControls.MyRadGridView
    Friend WithEvents gvWreckage As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblConsmSectionCode As common.Controls.MyLabel
    Friend WithEvents lblConsmSectionLocCode As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents pageSectionStock As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvSectionStock As common.UserControls.MyRadGridView
    Friend WithEvents btnunpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents pageSectionStockHistory As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvSectionStockHistory As common.UserControls.MyRadGridView
    Friend WithEvents pageScrapDetail As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GvScrap As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents pageoverheadcost As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvProductionCost As common.UserControls.MyRadGridView
    Friend WithEvents TxtManualBatchNo As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents lblProfitCenterCode As common.Controls.MyLabel
    Friend WithEvents LblCostCenterCode As common.Controls.MyLabel
    Friend WithEvents lblLineNo As common.Controls.MyLabel
    Friend WithEvents lblCostCenterName As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents lblProfitCenterName As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents chkJobWorkInward As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnShowInventory As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnCancel As RadButton
End Class

