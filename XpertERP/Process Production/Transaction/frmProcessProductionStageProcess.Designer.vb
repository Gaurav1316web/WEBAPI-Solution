<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProcessProductionStageProcess
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProcessProductionStageProcess))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.chkJobWorkInward = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblProfitCenterCode = New common.Controls.MyLabel()
        Me.TxtManualBatchNo = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.LblCostCenterCode = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblLineNo = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblProfitCenterName = New common.Controls.MyLabel()
        Me.lblToLocation = New common.Controls.MyLabel()
        Me.lblCostCenterName = New common.Controls.MyLabel()
        Me.txtlocation = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.fndMainBatchNo = New common.UserControls.txtFinder()
        Me.lblFromLocation = New common.Controls.MyLabel()
        Me.txtlocationname = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.fndIssueNo = New common.UserControls.txtFinder()
        Me.lblCode = New common.Controls.MyLabel()
        Me.lblBomDate = New common.Controls.MyLabel()
        Me.dtpDate = New common.Controls.MyDateTimePicker()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.pageIssueDetail = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvIssue = New common.UserControls.MyRadGridView()
        Me.pageStageDetail = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvStage = New common.UserControls.MyRadGridView()
        Me.pageAddRemove = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvARDetail = New common.UserControls.MyRadGridView()
        Me.pageBatchDetail = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.pageAttachment = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnShowInventory = New Telerik.WinControls.UI.RadButton()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.btnunpost = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.chkJobWorkInward, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProfitCenterCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtManualBatchNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblCostCenterCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLineNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProfitCenterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCostCenterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtlocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtlocationname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.pageIssueDetail.SuspendLayout()
        CType(Me.gvIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvIssue.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageStageDetail.SuspendLayout()
        CType(Me.gvStage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvStage.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageAddRemove.SuspendLayout()
        CType(Me.gvARDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvARDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageBatchDetail.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageAttachment.SuspendLayout()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnunpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnShowInventory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnunpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(745, 493)
        Me.SplitContainer1.SplitterDistance = 455
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkJobWorkInward)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblProfitCenterCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.TxtManualBatchNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel10)
        Me.SplitContainer2.Panel1.Controls.Add(Me.LblCostCenterCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel13)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel11)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLineNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblProfitCenterName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblToLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCostCenterName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtlocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndMainBatchNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblFromLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtlocationname)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndIssueNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBomDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer2.Size = New System.Drawing.Size(739, 449)
        Me.SplitContainer2.SplitterDistance = 173
        Me.SplitContainer2.TabIndex = 0
        '
        'chkJobWorkInward
        '
        Me.chkJobWorkInward.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkJobWorkInward.Location = New System.Drawing.Point(278, 106)
        Me.chkJobWorkInward.Name = "chkJobWorkInward"
        Me.chkJobWorkInward.ReadOnly = True
        Me.chkJobWorkInward.Size = New System.Drawing.Size(105, 16)
        Me.chkJobWorkInward.TabIndex = 92
        Me.chkJobWorkInward.Text = "Job Work Inward"
        '
        'lblProfitCenterCode
        '
        Me.lblProfitCenterCode.AutoSize = False
        Me.lblProfitCenterCode.BorderVisible = True
        Me.lblProfitCenterCode.FieldName = Nothing
        Me.lblProfitCenterCode.Location = New System.Drawing.Point(119, 148)
        Me.lblProfitCenterCode.Name = "lblProfitCenterCode"
        Me.lblProfitCenterCode.Size = New System.Drawing.Size(153, 19)
        Me.lblProfitCenterCode.TabIndex = 81
        Me.lblProfitCenterCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtManualBatchNo
        '
        Me.TxtManualBatchNo.AutoSize = False
        Me.TxtManualBatchNo.BorderVisible = True
        Me.TxtManualBatchNo.FieldName = Nothing
        Me.TxtManualBatchNo.Location = New System.Drawing.Point(119, 84)
        Me.TxtManualBatchNo.Name = "TxtManualBatchNo"
        Me.TxtManualBatchNo.Size = New System.Drawing.Size(154, 19)
        Me.TxtManualBatchNo.TabIndex = 65
        Me.TxtManualBatchNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(8, 105)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel10.TabIndex = 86
        Me.MyLabel10.Text = "Line No"
        '
        'LblCostCenterCode
        '
        Me.LblCostCenterCode.AutoSize = False
        Me.LblCostCenterCode.BorderVisible = True
        Me.LblCostCenterCode.FieldName = Nothing
        Me.LblCostCenterCode.Location = New System.Drawing.Point(119, 126)
        Me.LblCostCenterCode.Name = "LblCostCenterCode"
        Me.LblCostCenterCode.Size = New System.Drawing.Size(153, 19)
        Me.LblCostCenterCode.TabIndex = 82
        Me.LblCostCenterCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(8, 84)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(93, 16)
        Me.MyLabel13.TabIndex = 64
        Me.MyLabel13.Text = "Manual Batch No"
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel11.Location = New System.Drawing.Point(8, 149)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(99, 18)
        Me.MyLabel11.TabIndex = 85
        Me.MyLabel11.Text = "Profit Center Code"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(278, 85)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel5.TabIndex = 50
        Me.MyLabel5.Text = "To Location"
        Me.MyLabel5.Visible = False
        '
        'lblLineNo
        '
        Me.lblLineNo.AutoSize = False
        Me.lblLineNo.BorderVisible = True
        Me.lblLineNo.FieldName = Nothing
        Me.lblLineNo.Location = New System.Drawing.Point(119, 105)
        Me.lblLineNo.Name = "lblLineNo"
        Me.lblLineNo.Size = New System.Drawing.Size(153, 19)
        Me.lblLineNo.TabIndex = 83
        Me.lblLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(278, 62)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel3.TabIndex = 49
        Me.MyLabel3.Text = "From Location"
        Me.MyLabel3.Visible = False
        '
        'lblProfitCenterName
        '
        Me.lblProfitCenterName.AutoSize = False
        Me.lblProfitCenterName.BorderVisible = True
        Me.lblProfitCenterName.FieldName = Nothing
        Me.lblProfitCenterName.Location = New System.Drawing.Point(275, 148)
        Me.lblProfitCenterName.Name = "lblProfitCenterName"
        Me.lblProfitCenterName.Size = New System.Drawing.Size(451, 19)
        Me.lblProfitCenterName.TabIndex = 84
        Me.lblProfitCenterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblToLocation
        '
        Me.lblToLocation.AutoSize = False
        Me.lblToLocation.BorderVisible = True
        Me.lblToLocation.FieldName = Nothing
        Me.lblToLocation.Location = New System.Drawing.Point(363, 86)
        Me.lblToLocation.Name = "lblToLocation"
        Me.lblToLocation.Size = New System.Drawing.Size(363, 19)
        Me.lblToLocation.TabIndex = 48
        Me.lblToLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblToLocation.Visible = False
        '
        'lblCostCenterName
        '
        Me.lblCostCenterName.AutoSize = False
        Me.lblCostCenterName.BorderVisible = True
        Me.lblCostCenterName.FieldName = Nothing
        Me.lblCostCenterName.Location = New System.Drawing.Point(275, 126)
        Me.lblCostCenterName.Name = "lblCostCenterName"
        Me.lblCostCenterName.Size = New System.Drawing.Size(451, 19)
        Me.lblCostCenterName.TabIndex = 88
        Me.lblCostCenterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtlocation
        '
        Me.txtlocation.AutoSize = False
        Me.txtlocation.BorderVisible = True
        Me.txtlocation.FieldName = Nothing
        Me.txtlocation.Location = New System.Drawing.Point(363, 40)
        Me.txtlocation.Name = "txtlocation"
        Me.txtlocation.Size = New System.Drawing.Size(170, 19)
        Me.txtlocation.TabIndex = 47
        Me.txtlocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(8, 126)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(94, 18)
        Me.MyLabel8.TabIndex = 87
        Me.MyLabel8.Text = "Cost Center Code"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(628, 13)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 46
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(9, 40)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(53, 16)
        Me.MyLabel6.TabIndex = 42
        Me.MyLabel6.Text = "Batch No"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(278, 43)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(81, 16)
        Me.MyLabel1.TabIndex = 36
        Me.MyLabel1.Text = "Location Detail"
        '
        'fndMainBatchNo
        '
        Me.fndMainBatchNo.CalculationExpression = Nothing
        Me.fndMainBatchNo.FieldCode = Nothing
        Me.fndMainBatchNo.FieldDesc = Nothing
        Me.fndMainBatchNo.FieldMaxLength = 0
        Me.fndMainBatchNo.FieldName = Nothing
        Me.fndMainBatchNo.isCalculatedField = False
        Me.fndMainBatchNo.IsSourceFromTable = False
        Me.fndMainBatchNo.IsSourceFromValueList = False
        Me.fndMainBatchNo.IsUnique = False
        Me.fndMainBatchNo.Location = New System.Drawing.Point(120, 40)
        Me.fndMainBatchNo.MendatroryField = True
        Me.fndMainBatchNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndMainBatchNo.MyLinkLable1 = Me.MyLabel6
        Me.fndMainBatchNo.MyLinkLable2 = Nothing
        Me.fndMainBatchNo.MyReadOnly = False
        Me.fndMainBatchNo.MyShowMasterFormButton = False
        Me.fndMainBatchNo.Name = "fndMainBatchNo"
        Me.fndMainBatchNo.ReferenceFieldDesc = Nothing
        Me.fndMainBatchNo.ReferenceFieldName = Nothing
        Me.fndMainBatchNo.ReferenceTableName = Nothing
        Me.fndMainBatchNo.Size = New System.Drawing.Size(153, 19)
        Me.fndMainBatchNo.TabIndex = 3
        Me.fndMainBatchNo.Value = ""
        '
        'lblFromLocation
        '
        Me.lblFromLocation.AutoSize = False
        Me.lblFromLocation.BorderVisible = True
        Me.lblFromLocation.FieldName = Nothing
        Me.lblFromLocation.Location = New System.Drawing.Point(363, 61)
        Me.lblFromLocation.Name = "lblFromLocation"
        Me.lblFromLocation.Size = New System.Drawing.Size(363, 19)
        Me.lblFromLocation.TabIndex = 40
        Me.lblFromLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFromLocation.Visible = False
        '
        'txtlocationname
        '
        Me.txtlocationname.AutoSize = False
        Me.txtlocationname.BorderVisible = True
        Me.txtlocationname.FieldName = Nothing
        Me.txtlocationname.Location = New System.Drawing.Point(536, 40)
        Me.txtlocationname.Name = "txtlocationname"
        Me.txtlocationname.Size = New System.Drawing.Size(190, 19)
        Me.txtlocationname.TabIndex = 37
        Me.txtlocationname.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(8, 62)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel4.TabIndex = 39
        Me.MyLabel4.Text = "Issue No"
        Me.MyLabel4.Visible = False
        '
        'fndIssueNo
        '
        Me.fndIssueNo.CalculationExpression = Nothing
        Me.fndIssueNo.FieldCode = Nothing
        Me.fndIssueNo.FieldDesc = Nothing
        Me.fndIssueNo.FieldMaxLength = 0
        Me.fndIssueNo.FieldName = Nothing
        Me.fndIssueNo.isCalculatedField = False
        Me.fndIssueNo.IsSourceFromTable = False
        Me.fndIssueNo.IsSourceFromValueList = False
        Me.fndIssueNo.IsUnique = False
        Me.fndIssueNo.Location = New System.Drawing.Point(119, 61)
        Me.fndIssueNo.MendatroryField = True
        Me.fndIssueNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndIssueNo.MyLinkLable1 = Me.MyLabel4
        Me.fndIssueNo.MyLinkLable2 = Me.lblFromLocation
        Me.fndIssueNo.MyReadOnly = False
        Me.fndIssueNo.MyShowMasterFormButton = False
        Me.fndIssueNo.Name = "fndIssueNo"
        Me.fndIssueNo.ReferenceFieldDesc = Nothing
        Me.fndIssueNo.ReferenceFieldName = Nothing
        Me.fndIssueNo.ReferenceTableName = Nothing
        Me.fndIssueNo.Size = New System.Drawing.Size(153, 19)
        Me.fndIssueNo.TabIndex = 2
        Me.fndIssueNo.Value = ""
        Me.fndIssueNo.Visible = False
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(9, 16)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(33, 16)
        Me.lblCode.TabIndex = 4
        Me.lblCode.Text = "Code"
        '
        'lblBomDate
        '
        Me.lblBomDate.FieldName = Nothing
        Me.lblBomDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBomDate.Location = New System.Drawing.Point(503, 17)
        Me.lblBomDate.Name = "lblBomDate"
        Me.lblBomDate.Size = New System.Drawing.Size(30, 16)
        Me.lblBomDate.TabIndex = 5
        Me.lblBomDate.Text = "Date"
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
        Me.dtpDate.Location = New System.Drawing.Point(536, 16)
        Me.dtpDate.MendatroryField = True
        Me.dtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.MyLinkLable1 = Me.lblBomDate
        Me.dtpDate.MyLinkLable2 = Nothing
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.ReferenceFieldDesc = Nothing
        Me.dtpDate.ReferenceFieldName = Nothing
        Me.dtpDate.ReferenceTableName = Nothing
        Me.dtpDate.Size = New System.Drawing.Size(86, 18)
        Me.dtpDate.TabIndex = 1
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "03/05/2011"
        Me.dtpDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(477, 13)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(20, 21)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = " "
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(120, 13)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(357, 21)
        Me.txtCode.TabIndex = 3
        Me.txtCode.Value = ""
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.pageIssueDetail)
        Me.RadPageView1.Controls.Add(Me.pageStageDetail)
        Me.RadPageView1.Controls.Add(Me.pageAddRemove)
        Me.RadPageView1.Controls.Add(Me.pageBatchDetail)
        Me.RadPageView1.Controls.Add(Me.pageAttachment)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(3, 3)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.pageBatchDetail
        Me.RadPageView1.Size = New System.Drawing.Size(733, 266)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'pageIssueDetail
        '
        Me.pageIssueDetail.Controls.Add(Me.gvIssue)
        Me.pageIssueDetail.ItemSize = New System.Drawing.SizeF(73.0!, 28.0!)
        Me.pageIssueDetail.Location = New System.Drawing.Point(10, 37)
        Me.pageIssueDetail.Name = "pageIssueDetail"
        Me.pageIssueDetail.Size = New System.Drawing.Size(712, 218)
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
        Me.gvIssue.Size = New System.Drawing.Size(712, 218)
        Me.gvIssue.TabIndex = 1
        Me.gvIssue.TabStop = False
        Me.gvIssue.Text = "RadGridView1"
        '
        'pageStageDetail
        '
        Me.pageStageDetail.Controls.Add(Me.gvStage)
        Me.pageStageDetail.ItemSize = New System.Drawing.SizeF(76.0!, 28.0!)
        Me.pageStageDetail.Location = New System.Drawing.Point(10, 37)
        Me.pageStageDetail.Name = "pageStageDetail"
        Me.pageStageDetail.Size = New System.Drawing.Size(712, 218)
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
        Me.gvStage.Size = New System.Drawing.Size(712, 218)
        Me.gvStage.TabIndex = 1
        Me.gvStage.TabStop = False
        Me.gvStage.Text = "RadGridView1"
        '
        'pageAddRemove
        '
        Me.pageAddRemove.Controls.Add(Me.gvARDetail)
        Me.pageAddRemove.ItemSize = New System.Drawing.SizeF(82.0!, 28.0!)
        Me.pageAddRemove.Location = New System.Drawing.Point(10, 37)
        Me.pageAddRemove.Name = "pageAddRemove"
        Me.pageAddRemove.Size = New System.Drawing.Size(712, 218)
        Me.pageAddRemove.Text = "Add/Remove"
        '
        'gvARDetail
        '
        Me.gvARDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvARDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvARDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvARDetail.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvARDetail.ForeColor = System.Drawing.Color.Black
        Me.gvARDetail.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvARDetail.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvARDetail.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvARDetail.MasterTemplate.AllowAddNewRow = False
        Me.gvARDetail.MasterTemplate.AutoGenerateColumns = False
        Me.gvARDetail.MasterTemplate.EnableGrouping = False
        Me.gvARDetail.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvARDetail.Name = "gvARDetail"
        Me.gvARDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvARDetail.ShowHeaderCellButtons = True
        Me.gvARDetail.Size = New System.Drawing.Size(712, 218)
        Me.gvARDetail.TabIndex = 2
        Me.gvARDetail.TabStop = False
        Me.gvARDetail.Text = "RadGridView1"
        '
        'pageBatchDetail
        '
        Me.pageBatchDetail.Controls.Add(Me.gv)
        Me.pageBatchDetail.ItemSize = New System.Drawing.SizeF(134.0!, 28.0!)
        Me.pageBatchDetail.Location = New System.Drawing.Point(10, 37)
        Me.pageBatchDetail.Name = "pageBatchDetail"
        Me.pageBatchDetail.Size = New System.Drawing.Size(712, 218)
        Me.pageBatchDetail.Text = "Batch Production Detail"
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        'gv
        '
        Me.gv.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AutoGenerateColumns = False
        Me.gv.MasterTemplate.EnableGrouping = False
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(712, 218)
        Me.gv.TabIndex = 0
        Me.gv.TabStop = False
        Me.gv.Text = "RadGridView1"
        '
        'pageAttachment
        '
        Me.pageAttachment.Controls.Add(Me.UcAttachment1)
        Me.pageAttachment.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.pageAttachment.Location = New System.Drawing.Point(10, 37)
        Me.pageAttachment.Name = "pageAttachment"
        Me.pageAttachment.Size = New System.Drawing.Size(807, 218)
        Me.pageAttachment.Text = "Attahcment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(807, 218)
        Me.UcAttachment1.TabIndex = 5
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(661, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(78, 22)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btnShowInventory
        '
        Me.btnShowInventory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnShowInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowInventory.Location = New System.Drawing.Point(260, 5)
        Me.btnShowInventory.Name = "btnShowInventory"
        Me.btnShowInventory.Size = New System.Drawing.Size(86, 22)
        Me.btnShowInventory.TabIndex = 36
        Me.btnShowInventory.Text = "Show Inventory"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(348, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(57, 22)
        Me.btnCancel.TabIndex = 35
        Me.btnCancel.Text = "Cancel"
        '
        'btnunpost
        '
        Me.btnunpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnunpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnunpost.Location = New System.Drawing.Point(195, 5)
        Me.btnunpost.Name = "btnunpost"
        Me.btnunpost.Size = New System.Drawing.Size(63, 22)
        Me.btnunpost.TabIndex = 34
        Me.btnunpost.Text = "Unpost"
        Me.btnunpost.Visible = False
        '
        'MyLabel2
        '
        Me.MyLabel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.ForeColor = System.Drawing.SystemColors.Highlight
        Me.MyLabel2.Location = New System.Drawing.Point(415, 10)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(329, 13)
        Me.MyLabel2.TabIndex = 31
        Me.MyLabel2.Text = "Press F4 to fill QC Log Sheet after selecting any stage in Stage Detail Tab"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(125, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(3, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(59, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(63, 5)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(61, 22)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(409, 5)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(71, 22)
        Me.btnHistory.TabIndex = 7
        Me.btnHistory.Text = "&History"
        '
        'frmProcessProductionStageProcess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(745, 493)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmProcessProductionStageProcess"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Stage Process"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.chkJobWorkInward, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProfitCenterCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtManualBatchNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblCostCenterCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLineNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProfitCenterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCostCenterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtlocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtlocationname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.pageIssueDetail.ResumeLayout(False)
        CType(Me.gvIssue.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvIssue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageStageDetail.ResumeLayout(False)
        CType(Me.gvStage.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvStage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageAddRemove.ResumeLayout(False)
        CType(Me.gvARDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvARDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageBatchDetail.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageAttachment.ResumeLayout(False)
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnunpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents lblBomDate As common.Controls.MyLabel
    Friend WithEvents dtpDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents fndMainBatchNo As common.UserControls.txtFinder
    Friend WithEvents lblFromLocation As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents fndIssueNo As common.UserControls.txtFinder
    Friend WithEvents txtlocationname As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents pageBatchDetail As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents pageAttachment As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents txtlocation As common.Controls.MyLabel
    Friend WithEvents pageIssueDetail As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pageStageDetail As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvIssue As common.UserControls.MyRadGridView
    Friend WithEvents gvStage As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblToLocation As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents btnunpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents pageAddRemove As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvARDetail As common.UserControls.MyRadGridView
    Friend WithEvents btnCancel As Telerik.WinControls.UI.RadButton
    Friend WithEvents TxtManualBatchNo As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents lblProfitCenterCode As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents LblCostCenterCode As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents lblLineNo As common.Controls.MyLabel
    Friend WithEvents lblProfitCenterName As common.Controls.MyLabel
    Friend WithEvents lblCostCenterName As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents chkJobWorkInward As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnShowInventory As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
End Class

