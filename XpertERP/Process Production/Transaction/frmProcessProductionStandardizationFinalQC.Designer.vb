<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProcessProductionStandardizationFinalQC
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProcessProductionStandardizationFinalQC))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.chkJobWorkInward = New Telerik.WinControls.UI.RadCheckBox()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.pageIssueDetail = New Telerik.WinControls.UI.RadPageViewPage()
        Me.pageAddRemoveDetail = New Telerik.WinControls.UI.RadPageViewPage()
        Me.pageStageDetail = New Telerik.WinControls.UI.RadPageViewPage()
        Me.pageBatchDetail = New Telerik.WinControls.UI.RadPageViewPage()
        Me.pageParameterDetail = New Telerik.WinControls.UI.RadPageViewPage()
        Me.pageSectionStock = New Telerik.WinControls.UI.RadPageViewPage()
        Me.pageSectionStockHistory = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.pageAttachment = New Telerik.WinControls.UI.RadPageViewPage()
        Me.btnShowInventory = New Telerik.WinControls.UI.RadButton()
        Me.btnunpost = New Telerik.WinControls.UI.RadButton()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.btnUpdateParameters = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btngo = New Telerik.WinControls.UI.RadButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel21 = New common.Controls.MyLabel()
        Me.cboStaus = New common.Controls.MyComboBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtAgainstStdCode = New common.UserControls.txtFinder()
        Me.lblChildBatchDesc = New common.Controls.MyLabel()
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
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblConsmSectionLocCode = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblConsmSectionCode = New common.Controls.MyLabel()
        Me.txtlocation = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.lblMainBatchDesc = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.fndMainBatchNo = New common.UserControls.txtFinder()
        Me.txtlocationname = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.fndChildBatchNo = New common.UserControls.txtFinder()
        Me.lblCode = New common.Controls.MyLabel()
        Me.lblBomDate = New common.Controls.MyLabel()
        Me.dtpDate = New common.Controls.MyDateTimePicker()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.gvParam = New common.UserControls.MyRadGridView()
        Me.gvIssue = New common.UserControls.MyRadGridView()
        Me.gvARDetail = New common.UserControls.MyRadGridView()
        Me.gvStage = New common.UserControls.MyRadGridView()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.gv_qc = New common.UserControls.MyRadGridView()
        Me.gvSectionStock = New common.UserControls.MyRadGridView()
        Me.gvSectionStockHistory = New common.UserControls.MyRadGridView()
        Me.lblJWESNFAmt = New common.Controls.MyLabel()
        Me.lblJWEFATAmt = New common.Controls.MyLabel()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.lblJWESNFKg = New common.Controls.MyLabel()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.lblJWEFATKg = New common.Controls.MyLabel()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.lblAvgRateSNF = New common.Controls.MyLabel()
        Me.lblAvgRateFAT = New common.Controls.MyLabel()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.MyLabel43 = New common.Controls.MyLabel()
        Me.MyLabel42 = New common.Controls.MyLabel()
        Me.MyLabel41 = New common.Controls.MyLabel()
        Me.MyLabel40 = New common.Controls.MyLabel()
        Me.MyLabel39 = New common.Controls.MyLabel()
        Me.MyLabel38 = New common.Controls.MyLabel()
        Me.lblTotNetSNFKG = New common.Controls.MyLabel()
        Me.lblTotNetFATKG = New common.Controls.MyLabel()
        Me.lblTotNetQty = New common.Controls.MyLabel()
        Me.MyLabel37 = New common.Controls.MyLabel()
        Me.lblTotAddRemoveSNFKG = New common.Controls.MyLabel()
        Me.lblTotAddRemoveFATKG = New common.Controls.MyLabel()
        Me.lblTotDifferenceSNFKG = New common.Controls.MyLabel()
        Me.lblTotAddRemoveQty = New common.Controls.MyLabel()
        Me.lblTotDifferenceFATKG = New common.Controls.MyLabel()
        Me.MyLabel33 = New common.Controls.MyLabel()
        Me.lblTotDifferenceQty = New common.Controls.MyLabel()
        Me.MyLabel29 = New common.Controls.MyLabel()
        Me.lblTotIssueSNFKG = New common.Controls.MyLabel()
        Me.lblTotProduceSNFKG = New common.Controls.MyLabel()
        Me.lblTotIssueFATKG = New common.Controls.MyLabel()
        Me.lblTotIssueQty = New common.Controls.MyLabel()
        Me.lblTotProduceFATKG = New common.Controls.MyLabel()
        Me.MyLabel25 = New common.Controls.MyLabel()
        Me.lblTotBatchSNFKG = New common.Controls.MyLabel()
        Me.lblTotProduceQty = New common.Controls.MyLabel()
        Me.lblTotBatchFATKG = New common.Controls.MyLabel()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.lblTotBatchQty = New common.Controls.MyLabel()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.chkJobWorkInward, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        Me.pageIssueDetail.SuspendLayout()
        Me.pageAddRemoveDetail.SuspendLayout()
        Me.pageStageDetail.SuspendLayout()
        Me.pageBatchDetail.SuspendLayout()
        Me.pageParameterDetail.SuspendLayout()
        Me.pageSectionStock.SuspendLayout()
        Me.pageSectionStockHistory.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.pageAttachment.SuspendLayout()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnunpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpdateParameters, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboStaus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChildBatchDesc, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConsmSectionLocCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConsmSectionCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtlocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMainBatchDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtlocationname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvParam, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvParam.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvIssue.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvARDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvARDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvStage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvStage.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_qc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_qc.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSectionStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSectionStock.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSectionStockHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSectionStockHistory.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblJWESNFAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblJWEFATAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblJWESNFKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblJWEFATKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAvgRateSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAvgRateFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotNetSNFKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotNetFATKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotNetQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotAddRemoveSNFKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotAddRemoveFATKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotDifferenceSNFKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotAddRemoveQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotDifferenceFATKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotDifferenceQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotIssueSNFKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotProduceSNFKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotIssueFATKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotIssueQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotProduceFATKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotBatchSNFKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotProduceQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotBatchFATKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotBatchQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnShowInventory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnunpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUpdateParameters)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btngo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Size = New System.Drawing.Size(830, 598)
        Me.SplitContainer1.SplitterDistance = 560
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel21)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboStaus)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtAgainstStdCode)
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblConsmSectionLocCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblConsmSectionCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtlocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblMainBatchDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndMainBatchNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblChildBatchDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtlocationname)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndChildBatchNo)
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
        Me.SplitContainer2.Size = New System.Drawing.Size(824, 554)
        Me.SplitContainer2.SplitterDistance = 197
        Me.SplitContainer2.TabIndex = 0
        '
        'chkJobWorkInward
        '
        Me.chkJobWorkInward.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkJobWorkInward.Location = New System.Drawing.Point(713, 82)
        Me.chkJobWorkInward.Name = "chkJobWorkInward"
        Me.chkJobWorkInward.ReadOnly = True
        Me.chkJobWorkInward.Size = New System.Drawing.Size(105, 16)
        Me.chkJobWorkInward.TabIndex = 91
        Me.chkJobWorkInward.Text = "Job Work Inward"
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
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.pageIssueDetail)
        Me.RadPageView1.Controls.Add(Me.pageAddRemoveDetail)
        Me.RadPageView1.Controls.Add(Me.pageStageDetail)
        Me.RadPageView1.Controls.Add(Me.pageBatchDetail)
        Me.RadPageView1.Controls.Add(Me.pageParameterDetail)
        Me.RadPageView1.Controls.Add(Me.pageSectionStock)
        Me.RadPageView1.Controls.Add(Me.pageSectionStockHistory)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.pageAttachment)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(3, 3)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(818, 347)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gvParam)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(54.0!, 24.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 33)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(797, 303)
        Me.RadPageViewPage2.Text = "Final QC"
        '
        'pageIssueDetail
        '
        Me.pageIssueDetail.Controls.Add(Me.gvIssue)
        Me.pageIssueDetail.ItemSize = New System.Drawing.SizeF(69.0!, 24.0!)
        Me.pageIssueDetail.Location = New System.Drawing.Point(10, 37)
        Me.pageIssueDetail.Name = "pageIssueDetail"
        Me.pageIssueDetail.Size = New System.Drawing.Size(797, 299)
        Me.pageIssueDetail.Text = "Issue Detail"
        '
        'pageAddRemoveDetail
        '
        Me.pageAddRemoveDetail.Controls.Add(Me.gvARDetail)
        Me.pageAddRemoveDetail.ItemSize = New System.Drawing.SizeF(155.0!, 24.0!)
        Me.pageAddRemoveDetail.Location = New System.Drawing.Point(10, 37)
        Me.pageAddRemoveDetail.Name = "pageAddRemoveDetail"
        Me.pageAddRemoveDetail.Size = New System.Drawing.Size(797, 299)
        Me.pageAddRemoveDetail.Text = "Added/Removed Item Detail"
        '
        'pageStageDetail
        '
        Me.pageStageDetail.Controls.Add(Me.gvStage)
        Me.pageStageDetail.ItemSize = New System.Drawing.SizeF(72.0!, 24.0!)
        Me.pageStageDetail.Location = New System.Drawing.Point(10, 37)
        Me.pageStageDetail.Name = "pageStageDetail"
        Me.pageStageDetail.Size = New System.Drawing.Size(797, 299)
        Me.pageStageDetail.Text = "Stage Detail"
        '
        'pageBatchDetail
        '
        Me.pageBatchDetail.Controls.Add(Me.gv)
        Me.pageBatchDetail.ItemSize = New System.Drawing.SizeF(130.0!, 24.0!)
        Me.pageBatchDetail.Location = New System.Drawing.Point(10, 37)
        Me.pageBatchDetail.Name = "pageBatchDetail"
        Me.pageBatchDetail.Size = New System.Drawing.Size(797, 299)
        Me.pageBatchDetail.Text = "Batch Production Detail"
        '
        'pageParameterDetail
        '
        Me.pageParameterDetail.Controls.Add(Me.gv_qc)
        Me.pageParameterDetail.ItemSize = New System.Drawing.SizeF(81.0!, 24.0!)
        Me.pageParameterDetail.Location = New System.Drawing.Point(10, 37)
        Me.pageParameterDetail.Name = "pageParameterDetail"
        Me.pageParameterDetail.Size = New System.Drawing.Size(797, 170)
        Me.pageParameterDetail.Text = "Quality Check"
        '
        'pageSectionStock
        '
        Me.pageSectionStock.Controls.Add(Me.gvSectionStock)
        Me.pageSectionStock.ItemSize = New System.Drawing.SizeF(79.0!, 24.0!)
        Me.pageSectionStock.Location = New System.Drawing.Point(10, 37)
        Me.pageSectionStock.Name = "pageSectionStock"
        Me.pageSectionStock.Size = New System.Drawing.Size(797, 170)
        Me.pageSectionStock.Text = "Section Stock"
        '
        'pageSectionStockHistory
        '
        Me.pageSectionStockHistory.Controls.Add(Me.gvSectionStockHistory)
        Me.pageSectionStockHistory.ItemSize = New System.Drawing.SizeF(118.0!, 24.0!)
        Me.pageSectionStockHistory.Location = New System.Drawing.Point(10, 37)
        Me.pageSectionStockHistory.Name = "pageSectionStockHistory"
        Me.pageSectionStockHistory.Size = New System.Drawing.Size(797, 170)
        Me.pageSectionStockHistory.Text = "Section Stock History"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.lblAvgRateSNF)
        Me.RadPageViewPage1.Controls.Add(Me.lblAvgRateFAT)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel16)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel43)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel42)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel41)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel40)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel39)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel38)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotNetSNFKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotNetFATKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotNetQty)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel37)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotAddRemoveSNFKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotAddRemoveFATKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotDifferenceSNFKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotAddRemoveQty)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotDifferenceFATKG)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel33)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotDifferenceQty)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel29)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotIssueSNFKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotProduceSNFKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotIssueFATKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotIssueQty)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotProduceFATKG)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel25)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotBatchSNFKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotProduceQty)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotBatchFATKG)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel21)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotBatchQty)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel14)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(37.0!, 24.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(797, 299)
        Me.RadPageViewPage1.Text = "Total"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblJWESNFAmt)
        Me.GroupBox1.Controls.Add(Me.lblJWEFATAmt)
        Me.GroupBox1.Controls.Add(Me.MyLabel24)
        Me.GroupBox1.Controls.Add(Me.MyLabel22)
        Me.GroupBox1.Controls.Add(Me.lblJWESNFKg)
        Me.GroupBox1.Controls.Add(Me.MyLabel19)
        Me.GroupBox1.Controls.Add(Me.lblJWEFATKg)
        Me.GroupBox1.Controls.Add(Me.MyLabel17)
        Me.GroupBox1.Location = New System.Drawing.Point(19, 180)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(568, 64)
        Me.GroupBox1.TabIndex = 79
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Job Work Estimation"
        '
        'pageAttachment
        '
        Me.pageAttachment.Controls.Add(Me.UcAttachment1)
        Me.pageAttachment.ItemSize = New System.Drawing.SizeF(71.0!, 24.0!)
        Me.pageAttachment.Location = New System.Drawing.Point(10, 37)
        Me.pageAttachment.Name = "pageAttachment"
        Me.pageAttachment.Size = New System.Drawing.Size(797, 170)
        Me.pageAttachment.Text = "Attahcment"
        '
        'btnShowInventory
        '
        Me.btnShowInventory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnShowInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowInventory.Location = New System.Drawing.Point(512, 5)
        Me.btnShowInventory.Name = "btnShowInventory"
        Me.btnShowInventory.Size = New System.Drawing.Size(91, 22)
        Me.btnShowInventory.TabIndex = 36
        Me.btnShowInventory.Text = "Show Inventory"
        '
        'btnunpost
        '
        Me.btnunpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnunpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnunpost.Location = New System.Drawing.Point(301, 5)
        Me.btnunpost.Name = "btnunpost"
        Me.btnunpost.Size = New System.Drawing.Size(60, 22)
        Me.btnunpost.TabIndex = 35
        Me.btnunpost.Text = "Unpost"
        Me.btnunpost.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(238, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(61, 22)
        Me.btnCancel.TabIndex = 34
        Me.btnCancel.Text = "Cancel"
        '
        'btnUpdateParameters
        '
        Me.btnUpdateParameters.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateParameters.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateParameters.Location = New System.Drawing.Point(363, 5)
        Me.btnUpdateParameters.Name = "btnUpdateParameters"
        Me.btnUpdateParameters.Size = New System.Drawing.Size(146, 22)
        Me.btnUpdateParameters.TabIndex = 33
        Me.btnUpdateParameters.Text = "Update Parameters Value"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(159, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(76, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(3, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(76, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(747, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(78, 22)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(81, 5)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(76, 22)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btngo
        '
        Me.btngo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btngo.Location = New System.Drawing.Point(689, 5)
        Me.btngo.Name = "btngo"
        Me.btngo.Size = New System.Drawing.Size(47, 22)
        Me.btngo.TabIndex = 6
        Me.btngo.Text = ">>"
        Me.btngo.Visible = False
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(605, 6)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(71, 22)
        Me.btnHistory.TabIndex = 6
        Me.btnHistory.Text = "&History"
        '
        'RadLabel21
        '
        Me.RadLabel21.FieldName = Nothing
        Me.RadLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel21.Location = New System.Drawing.Point(498, 61)
        Me.RadLabel21.Name = "RadLabel21"
        Me.RadLabel21.Size = New System.Drawing.Size(38, 16)
        Me.RadLabel21.TabIndex = 84
        Me.RadLabel21.Text = "Status"
        '
        'cboStaus
        '
        Me.cboStaus.AutoCompleteDisplayMember = Nothing
        Me.cboStaus.AutoCompleteValueMember = Nothing
        Me.cboStaus.CalculationExpression = Nothing
        Me.cboStaus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboStaus.FieldCode = Nothing
        Me.cboStaus.FieldDesc = Nothing
        Me.cboStaus.FieldMaxLength = 0
        Me.cboStaus.FieldName = Nothing
        Me.cboStaus.isCalculatedField = False
        Me.cboStaus.IsSourceFromTable = False
        Me.cboStaus.IsSourceFromValueList = False
        Me.cboStaus.IsUnique = False
        Me.cboStaus.Location = New System.Drawing.Point(569, 59)
        Me.cboStaus.MendatroryField = True
        Me.cboStaus.MyLinkLable1 = Me.RadLabel21
        Me.cboStaus.MyLinkLable2 = Nothing
        Me.cboStaus.Name = "cboStaus"
        Me.cboStaus.ReferenceFieldDesc = Nothing
        Me.cboStaus.ReferenceFieldName = Nothing
        Me.cboStaus.ReferenceTableName = Nothing
        Me.cboStaus.Size = New System.Drawing.Size(249, 20)
        Me.cboStaus.TabIndex = 5
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(499, 38)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel9.TabIndex = 82
        Me.MyLabel9.Text = "Std. Code"
        '
        'txtAgainstStdCode
        '
        Me.txtAgainstStdCode.CalculationExpression = Nothing
        Me.txtAgainstStdCode.FieldCode = Nothing
        Me.txtAgainstStdCode.FieldDesc = Nothing
        Me.txtAgainstStdCode.FieldMaxLength = 0
        Me.txtAgainstStdCode.FieldName = Nothing
        Me.txtAgainstStdCode.isCalculatedField = False
        Me.txtAgainstStdCode.IsSourceFromTable = False
        Me.txtAgainstStdCode.IsSourceFromValueList = False
        Me.txtAgainstStdCode.IsUnique = False
        Me.txtAgainstStdCode.Location = New System.Drawing.Point(569, 37)
        Me.txtAgainstStdCode.MendatroryField = True
        Me.txtAgainstStdCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAgainstStdCode.MyLinkLable1 = Me.MyLabel9
        Me.txtAgainstStdCode.MyLinkLable2 = Me.lblChildBatchDesc
        Me.txtAgainstStdCode.MyReadOnly = False
        Me.txtAgainstStdCode.MyShowMasterFormButton = False
        Me.txtAgainstStdCode.Name = "txtAgainstStdCode"
        Me.txtAgainstStdCode.ReferenceFieldDesc = Nothing
        Me.txtAgainstStdCode.ReferenceFieldName = Nothing
        Me.txtAgainstStdCode.ReferenceTableName = Nothing
        Me.txtAgainstStdCode.Size = New System.Drawing.Size(249, 19)
        Me.txtAgainstStdCode.TabIndex = 3
        Me.txtAgainstStdCode.Value = ""
        '
        'lblChildBatchDesc
        '
        Me.lblChildBatchDesc.AutoSize = False
        Me.lblChildBatchDesc.BorderVisible = True
        Me.lblChildBatchDesc.FieldName = Nothing
        Me.lblChildBatchDesc.Location = New System.Drawing.Point(276, 37)
        Me.lblChildBatchDesc.Name = "lblChildBatchDesc"
        Me.lblChildBatchDesc.Size = New System.Drawing.Size(221, 19)
        Me.lblChildBatchDesc.TabIndex = 40
        Me.lblChildBatchDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblProfitCenterCode
        '
        Me.lblProfitCenterCode.AutoSize = False
        Me.lblProfitCenterCode.BorderVisible = True
        Me.lblProfitCenterCode.FieldName = Nothing
        Me.lblProfitCenterCode.Location = New System.Drawing.Point(120, 170)
        Me.lblProfitCenterCode.Name = "lblProfitCenterCode"
        Me.lblProfitCenterCode.Size = New System.Drawing.Size(153, 19)
        Me.lblProfitCenterCode.TabIndex = 48
        Me.lblProfitCenterCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCostCenterCode
        '
        Me.LblCostCenterCode.AutoSize = False
        Me.LblCostCenterCode.BorderVisible = True
        Me.LblCostCenterCode.FieldName = Nothing
        Me.LblCostCenterCode.Location = New System.Drawing.Point(120, 148)
        Me.LblCostCenterCode.Name = "LblCostCenterCode"
        Me.LblCostCenterCode.Size = New System.Drawing.Size(153, 19)
        Me.LblCostCenterCode.TabIndex = 48
        Me.LblCostCenterCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLineNo
        '
        Me.lblLineNo.AutoSize = False
        Me.lblLineNo.BorderVisible = True
        Me.lblLineNo.FieldName = Nothing
        Me.lblLineNo.Location = New System.Drawing.Point(569, 126)
        Me.lblLineNo.Name = "lblLineNo"
        Me.lblLineNo.Size = New System.Drawing.Size(251, 19)
        Me.lblLineNo.TabIndex = 48
        Me.lblLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCostCenterName
        '
        Me.lblCostCenterName.AutoSize = False
        Me.lblCostCenterName.BorderVisible = True
        Me.lblCostCenterName.FieldName = Nothing
        Me.lblCostCenterName.Location = New System.Drawing.Point(276, 148)
        Me.lblCostCenterName.Name = "lblCostCenterName"
        Me.lblCostCenterName.Size = New System.Drawing.Size(544, 19)
        Me.lblCostCenterName.TabIndex = 80
        Me.lblCostCenterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(9, 148)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(94, 18)
        Me.MyLabel8.TabIndex = 79
        Me.MyLabel8.Text = "Cost Center Code"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(499, 127)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel10.TabIndex = 78
        Me.MyLabel10.Text = "Line No"
        '
        'lblProfitCenterName
        '
        Me.lblProfitCenterName.AutoSize = False
        Me.lblProfitCenterName.BorderVisible = True
        Me.lblProfitCenterName.FieldName = Nothing
        Me.lblProfitCenterName.Location = New System.Drawing.Point(276, 170)
        Me.lblProfitCenterName.Name = "lblProfitCenterName"
        Me.lblProfitCenterName.Size = New System.Drawing.Size(544, 19)
        Me.lblProfitCenterName.TabIndex = 76
        Me.lblProfitCenterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel11.Location = New System.Drawing.Point(9, 170)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(99, 18)
        Me.MyLabel11.TabIndex = 77
        Me.MyLabel11.Text = "Profit Center Code"
        '
        'TxtManualBatchNo
        '
        Me.TxtManualBatchNo.AutoSize = False
        Me.TxtManualBatchNo.BorderVisible = True
        Me.TxtManualBatchNo.FieldName = Nothing
        Me.TxtManualBatchNo.Location = New System.Drawing.Point(120, 126)
        Me.TxtManualBatchNo.Name = "TxtManualBatchNo"
        Me.TxtManualBatchNo.Size = New System.Drawing.Size(377, 19)
        Me.TxtManualBatchNo.TabIndex = 63
        Me.TxtManualBatchNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(9, 127)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(93, 16)
        Me.MyLabel13.TabIndex = 62
        Me.MyLabel13.Text = "Manual Batch No"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(499, 105)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel3.TabIndex = 51
        Me.MyLabel3.Text = "Consm. Sec."
        '
        'lblConsmSectionLocCode
        '
        Me.lblConsmSectionLocCode.AutoSize = False
        Me.lblConsmSectionLocCode.BorderVisible = True
        Me.lblConsmSectionLocCode.FieldName = Nothing
        Me.lblConsmSectionLocCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConsmSectionLocCode.Location = New System.Drawing.Point(120, 103)
        Me.lblConsmSectionLocCode.Name = "lblConsmSectionLocCode"
        Me.lblConsmSectionLocCode.Size = New System.Drawing.Size(377, 20)
        Me.lblConsmSectionLocCode.TabIndex = 50
        Me.lblConsmSectionLocCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(9, 105)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel5.TabIndex = 49
        Me.MyLabel5.Text = "Consm. Location"
        '
        'lblConsmSectionCode
        '
        Me.lblConsmSectionCode.AutoSize = False
        Me.lblConsmSectionCode.BorderVisible = True
        Me.lblConsmSectionCode.FieldName = Nothing
        Me.lblConsmSectionCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConsmSectionCode.Location = New System.Drawing.Point(569, 103)
        Me.lblConsmSectionCode.Name = "lblConsmSectionCode"
        Me.lblConsmSectionCode.Size = New System.Drawing.Size(251, 20)
        Me.lblConsmSectionCode.TabIndex = 48
        Me.lblConsmSectionCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtlocation
        '
        Me.txtlocation.AutoSize = False
        Me.txtlocation.BorderVisible = True
        Me.txtlocation.FieldName = Nothing
        Me.txtlocation.Location = New System.Drawing.Point(120, 81)
        Me.txtlocation.Name = "txtlocation"
        Me.txtlocation.Size = New System.Drawing.Size(153, 19)
        Me.txtlocation.TabIndex = 47
        Me.txtlocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(662, 13)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(156, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 46
        '
        'lblMainBatchDesc
        '
        Me.lblMainBatchDesc.AutoSize = False
        Me.lblMainBatchDesc.BorderVisible = True
        Me.lblMainBatchDesc.FieldName = Nothing
        Me.lblMainBatchDesc.Location = New System.Drawing.Point(276, 59)
        Me.lblMainBatchDesc.Name = "lblMainBatchDesc"
        Me.lblMainBatchDesc.Size = New System.Drawing.Size(221, 19)
        Me.lblMainBatchDesc.TabIndex = 43
        Me.lblMainBatchDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(9, 60)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(80, 16)
        Me.MyLabel6.TabIndex = 42
        Me.MyLabel6.Text = "Main Batch No"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(9, 82)
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
        Me.fndMainBatchNo.Location = New System.Drawing.Point(120, 59)
        Me.fndMainBatchNo.MendatroryField = True
        Me.fndMainBatchNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndMainBatchNo.MyLinkLable1 = Me.MyLabel6
        Me.fndMainBatchNo.MyLinkLable2 = Me.lblMainBatchDesc
        Me.fndMainBatchNo.MyReadOnly = False
        Me.fndMainBatchNo.MyShowMasterFormButton = False
        Me.fndMainBatchNo.Name = "fndMainBatchNo"
        Me.fndMainBatchNo.ReferenceFieldDesc = Nothing
        Me.fndMainBatchNo.ReferenceFieldName = Nothing
        Me.fndMainBatchNo.ReferenceTableName = Nothing
        Me.fndMainBatchNo.Size = New System.Drawing.Size(153, 19)
        Me.fndMainBatchNo.TabIndex = 4
        Me.fndMainBatchNo.Value = ""
        '
        'txtlocationname
        '
        Me.txtlocationname.AutoSize = False
        Me.txtlocationname.BorderVisible = True
        Me.txtlocationname.FieldName = Nothing
        Me.txtlocationname.Location = New System.Drawing.Point(276, 81)
        Me.txtlocationname.Name = "txtlocationname"
        Me.txtlocationname.Size = New System.Drawing.Size(431, 19)
        Me.txtlocationname.TabIndex = 37
        Me.txtlocationname.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(9, 38)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel4.TabIndex = 39
        Me.MyLabel4.Text = "Child Batch No"
        '
        'fndChildBatchNo
        '
        Me.fndChildBatchNo.CalculationExpression = Nothing
        Me.fndChildBatchNo.FieldCode = Nothing
        Me.fndChildBatchNo.FieldDesc = Nothing
        Me.fndChildBatchNo.FieldMaxLength = 0
        Me.fndChildBatchNo.FieldName = Nothing
        Me.fndChildBatchNo.isCalculatedField = False
        Me.fndChildBatchNo.IsSourceFromTable = False
        Me.fndChildBatchNo.IsSourceFromValueList = False
        Me.fndChildBatchNo.IsUnique = False
        Me.fndChildBatchNo.Location = New System.Drawing.Point(120, 37)
        Me.fndChildBatchNo.MendatroryField = True
        Me.fndChildBatchNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndChildBatchNo.MyLinkLable1 = Me.MyLabel4
        Me.fndChildBatchNo.MyLinkLable2 = Me.lblChildBatchDesc
        Me.fndChildBatchNo.MyReadOnly = False
        Me.fndChildBatchNo.MyShowMasterFormButton = False
        Me.fndChildBatchNo.Name = "fndChildBatchNo"
        Me.fndChildBatchNo.ReferenceFieldDesc = Nothing
        Me.fndChildBatchNo.ReferenceFieldName = Nothing
        Me.fndChildBatchNo.ReferenceTableName = Nothing
        Me.fndChildBatchNo.Size = New System.Drawing.Size(153, 19)
        Me.fndChildBatchNo.TabIndex = 2
        Me.fndChildBatchNo.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(9, 15)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(53, 16)
        Me.lblCode.TabIndex = 4
        Me.lblCode.Text = "QC Code"
        '
        'lblBomDate
        '
        Me.lblBomDate.FieldName = Nothing
        Me.lblBomDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBomDate.Location = New System.Drawing.Point(499, 15)
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
        Me.dtpDate.Location = New System.Drawing.Point(569, 14)
        Me.dtpDate.MendatroryField = True
        Me.dtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.MyLinkLable1 = Me.lblBomDate
        Me.dtpDate.MyLinkLable2 = Nothing
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.ReferenceFieldDesc = Nothing
        Me.dtpDate.ReferenceFieldName = Nothing
        Me.dtpDate.ReferenceTableName = Nothing
        Me.dtpDate.Size = New System.Drawing.Size(87, 18)
        Me.dtpDate.TabIndex = 1
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "03/05/2011"
        Me.dtpDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
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
        'gvParam
        '
        Me.gvParam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvParam.Location = New System.Drawing.Point(0, 0)
        '
        'gvParam
        '
        Me.gvParam.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvParam.Name = "gvParam"
        Me.gvParam.ShowHeaderCellButtons = True
        Me.gvParam.Size = New System.Drawing.Size(797, 303)
        Me.gvParam.TabIndex = 265
        Me.gvParam.Text = "RadGridView1"
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
        '
        '
        Me.gvIssue.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvIssue.MasterTemplate.AllowAddNewRow = False
        Me.gvIssue.MasterTemplate.AutoGenerateColumns = False
        Me.gvIssue.MasterTemplate.EnableGrouping = False
        Me.gvIssue.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvIssue.Name = "gvIssue"
        Me.gvIssue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvIssue.ShowHeaderCellButtons = True
        Me.gvIssue.Size = New System.Drawing.Size(797, 299)
        Me.gvIssue.TabIndex = 1
        Me.gvIssue.TabStop = False
        Me.gvIssue.Text = "RadGridView1"
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
        Me.gvARDetail.Size = New System.Drawing.Size(797, 299)
        Me.gvARDetail.TabIndex = 1
        Me.gvARDetail.TabStop = False
        Me.gvARDetail.Text = "RadGridView1"
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
        Me.gvStage.Size = New System.Drawing.Size(797, 299)
        Me.gvStage.TabIndex = 2
        Me.gvStage.TabStop = False
        Me.gvStage.Text = "RadGridView1"
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
        '
        '
        Me.gv.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AutoGenerateColumns = False
        Me.gv.MasterTemplate.EnableGrouping = False
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(797, 299)
        Me.gv.TabIndex = 0
        Me.gv.TabStop = False
        Me.gv.Text = "RadGridView1"
        '
        'gv_qc
        '
        Me.gv_qc.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv_qc.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv_qc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv_qc.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv_qc.ForeColor = System.Drawing.Color.Black
        Me.gv_qc.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv_qc.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv_qc.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv_qc.MasterTemplate.AllowAddNewRow = False
        Me.gv_qc.MasterTemplate.AutoGenerateColumns = False
        Me.gv_qc.MasterTemplate.EnableGrouping = False
        Me.gv_qc.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv_qc.Name = "gv_qc"
        Me.gv_qc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv_qc.ShowHeaderCellButtons = True
        Me.gv_qc.Size = New System.Drawing.Size(797, 170)
        Me.gv_qc.TabIndex = 2
        Me.gv_qc.TabStop = False
        Me.gv_qc.Text = "RadGridView1"
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
        Me.gvSectionStock.Size = New System.Drawing.Size(797, 170)
        Me.gvSectionStock.TabIndex = 3
        Me.gvSectionStock.TabStop = False
        Me.gvSectionStock.Text = "RadGridView1"
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
        Me.gvSectionStockHistory.Size = New System.Drawing.Size(797, 170)
        Me.gvSectionStockHistory.TabIndex = 4
        Me.gvSectionStockHistory.TabStop = False
        Me.gvSectionStockHistory.Text = "RadGridView1"
        '
        'lblJWESNFAmt
        '
        Me.lblJWESNFAmt.AutoSize = False
        Me.lblJWESNFAmt.BorderVisible = True
        Me.lblJWESNFAmt.FieldName = Nothing
        Me.lblJWESNFAmt.Location = New System.Drawing.Point(412, 42)
        Me.lblJWESNFAmt.Name = "lblJWESNFAmt"
        Me.lblJWESNFAmt.Size = New System.Drawing.Size(153, 19)
        Me.lblJWESNFAmt.TabIndex = 71
        Me.lblJWESNFAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblJWEFATAmt
        '
        Me.lblJWEFATAmt.AutoSize = False
        Me.lblJWEFATAmt.BorderVisible = True
        Me.lblJWEFATAmt.FieldName = Nothing
        Me.lblJWEFATAmt.Location = New System.Drawing.Point(412, 20)
        Me.lblJWEFATAmt.Name = "lblJWEFATAmt"
        Me.lblJWEFATAmt.Size = New System.Drawing.Size(153, 19)
        Me.lblJWEFATAmt.TabIndex = 73
        Me.lblJWEFATAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel24.Location = New System.Drawing.Point(338, 43)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel24.TabIndex = 70
        Me.MyLabel24.Text = "SNF Amount"
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel22.Location = New System.Drawing.Point(339, 21)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel22.TabIndex = 72
        Me.MyLabel22.Text = "FAT Amount"
        '
        'lblJWESNFKg
        '
        Me.lblJWESNFKg.AutoSize = False
        Me.lblJWESNFKg.BorderVisible = True
        Me.lblJWESNFKg.FieldName = Nothing
        Me.lblJWESNFKg.Location = New System.Drawing.Point(100, 42)
        Me.lblJWESNFKg.Name = "lblJWESNFKg"
        Me.lblJWESNFKg.Size = New System.Drawing.Size(153, 19)
        Me.lblJWESNFKg.TabIndex = 71
        Me.lblJWESNFKg.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(33, 43)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel19.TabIndex = 70
        Me.MyLabel19.Text = "SNF KG"
        '
        'lblJWEFATKg
        '
        Me.lblJWEFATKg.AutoSize = False
        Me.lblJWEFATKg.BorderVisible = True
        Me.lblJWEFATKg.FieldName = Nothing
        Me.lblJWEFATKg.Location = New System.Drawing.Point(100, 20)
        Me.lblJWEFATKg.Name = "lblJWEFATKg"
        Me.lblJWEFATKg.Size = New System.Drawing.Size(153, 19)
        Me.lblJWEFATKg.TabIndex = 69
        Me.lblJWEFATKg.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(33, 21)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(47, 16)
        Me.MyLabel17.TabIndex = 68
        Me.MyLabel17.Text = "FAT KG"
        '
        'lblAvgRateSNF
        '
        Me.lblAvgRateSNF.AutoSize = False
        Me.lblAvgRateSNF.BorderVisible = True
        Me.lblAvgRateSNF.FieldName = Nothing
        Me.lblAvgRateSNF.Location = New System.Drawing.Point(431, 157)
        Me.lblAvgRateSNF.Name = "lblAvgRateSNF"
        Me.lblAvgRateSNF.Size = New System.Drawing.Size(153, 19)
        Me.lblAvgRateSNF.TabIndex = 77
        Me.lblAvgRateSNF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAvgRateFAT
        '
        Me.lblAvgRateFAT.AutoSize = False
        Me.lblAvgRateFAT.BorderVisible = True
        Me.lblAvgRateFAT.FieldName = Nothing
        Me.lblAvgRateFAT.Location = New System.Drawing.Point(275, 157)
        Me.lblAvgRateFAT.Name = "lblAvgRateFAT"
        Me.lblAvgRateFAT.Size = New System.Drawing.Size(153, 19)
        Me.lblAvgRateFAT.TabIndex = 76
        Me.lblAvgRateFAT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(41, 158)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(213, 16)
        Me.MyLabel16.TabIndex = 75
        Me.MyLabel16.Text = "Average FAT/SNF Rate of Issued Item(s)"
        '
        'MyLabel43
        '
        Me.MyLabel43.FieldName = Nothing
        Me.MyLabel43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel43.Location = New System.Drawing.Point(585, 136)
        Me.MyLabel43.Name = "MyLabel43"
        Me.MyLabel43.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel43.TabIndex = 52
        Me.MyLabel43.Text = "F [D+E]"
        '
        'MyLabel42
        '
        Me.MyLabel42.FieldName = Nothing
        Me.MyLabel42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel42.Location = New System.Drawing.Point(585, 116)
        Me.MyLabel42.Name = "MyLabel42"
        Me.MyLabel42.Size = New System.Drawing.Size(14, 16)
        Me.MyLabel42.TabIndex = 52
        Me.MyLabel42.Text = "E"
        '
        'MyLabel41
        '
        Me.MyLabel41.FieldName = Nothing
        Me.MyLabel41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel41.Location = New System.Drawing.Point(585, 92)
        Me.MyLabel41.Name = "MyLabel41"
        Me.MyLabel41.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel41.TabIndex = 52
        Me.MyLabel41.Text = "D [C-B]"
        '
        'MyLabel40
        '
        Me.MyLabel40.FieldName = Nothing
        Me.MyLabel40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel40.Location = New System.Drawing.Point(585, 72)
        Me.MyLabel40.Name = "MyLabel40"
        Me.MyLabel40.Size = New System.Drawing.Size(14, 16)
        Me.MyLabel40.TabIndex = 52
        Me.MyLabel40.Text = "C"
        '
        'MyLabel39
        '
        Me.MyLabel39.FieldName = Nothing
        Me.MyLabel39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel39.Location = New System.Drawing.Point(585, 48)
        Me.MyLabel39.Name = "MyLabel39"
        Me.MyLabel39.Size = New System.Drawing.Size(14, 16)
        Me.MyLabel39.TabIndex = 52
        Me.MyLabel39.Text = "B"
        '
        'MyLabel38
        '
        Me.MyLabel38.FieldName = Nothing
        Me.MyLabel38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel38.Location = New System.Drawing.Point(585, 26)
        Me.MyLabel38.Name = "MyLabel38"
        Me.MyLabel38.Size = New System.Drawing.Size(14, 16)
        Me.MyLabel38.TabIndex = 74
        Me.MyLabel38.Text = "A"
        '
        'lblTotNetSNFKG
        '
        Me.lblTotNetSNFKG.AutoSize = False
        Me.lblTotNetSNFKG.BorderVisible = True
        Me.lblTotNetSNFKG.FieldName = Nothing
        Me.lblTotNetSNFKG.Location = New System.Drawing.Point(431, 135)
        Me.lblTotNetSNFKG.Name = "lblTotNetSNFKG"
        Me.lblTotNetSNFKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotNetSNFKG.TabIndex = 72
        Me.lblTotNetSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotNetFATKG
        '
        Me.lblTotNetFATKG.AutoSize = False
        Me.lblTotNetFATKG.BorderVisible = True
        Me.lblTotNetFATKG.FieldName = Nothing
        Me.lblTotNetFATKG.Location = New System.Drawing.Point(275, 135)
        Me.lblTotNetFATKG.Name = "lblTotNetFATKG"
        Me.lblTotNetFATKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotNetFATKG.TabIndex = 73
        Me.lblTotNetFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotNetQty
        '
        Me.lblTotNetQty.AutoSize = False
        Me.lblTotNetQty.BorderVisible = True
        Me.lblTotNetQty.FieldName = Nothing
        Me.lblTotNetQty.Location = New System.Drawing.Point(119, 135)
        Me.lblTotNetQty.Name = "lblTotNetQty"
        Me.lblTotNetQty.Size = New System.Drawing.Size(153, 19)
        Me.lblTotNetQty.TabIndex = 71
        Me.lblTotNetQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel37
        '
        Me.MyLabel37.FieldName = Nothing
        Me.MyLabel37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel37.Location = New System.Drawing.Point(41, 136)
        Me.MyLabel37.Name = "MyLabel37"
        Me.MyLabel37.Size = New System.Drawing.Size(24, 16)
        Me.MyLabel37.TabIndex = 70
        Me.MyLabel37.Text = "Net"
        '
        'lblTotAddRemoveSNFKG
        '
        Me.lblTotAddRemoveSNFKG.AutoSize = False
        Me.lblTotAddRemoveSNFKG.BorderVisible = True
        Me.lblTotAddRemoveSNFKG.FieldName = Nothing
        Me.lblTotAddRemoveSNFKG.Location = New System.Drawing.Point(431, 113)
        Me.lblTotAddRemoveSNFKG.Name = "lblTotAddRemoveSNFKG"
        Me.lblTotAddRemoveSNFKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotAddRemoveSNFKG.TabIndex = 68
        Me.lblTotAddRemoveSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotAddRemoveFATKG
        '
        Me.lblTotAddRemoveFATKG.AutoSize = False
        Me.lblTotAddRemoveFATKG.BorderVisible = True
        Me.lblTotAddRemoveFATKG.FieldName = Nothing
        Me.lblTotAddRemoveFATKG.Location = New System.Drawing.Point(275, 113)
        Me.lblTotAddRemoveFATKG.Name = "lblTotAddRemoveFATKG"
        Me.lblTotAddRemoveFATKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotAddRemoveFATKG.TabIndex = 69
        Me.lblTotAddRemoveFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotDifferenceSNFKG
        '
        Me.lblTotDifferenceSNFKG.AutoSize = False
        Me.lblTotDifferenceSNFKG.BorderVisible = True
        Me.lblTotDifferenceSNFKG.FieldName = Nothing
        Me.lblTotDifferenceSNFKG.Location = New System.Drawing.Point(431, 91)
        Me.lblTotDifferenceSNFKG.Name = "lblTotDifferenceSNFKG"
        Me.lblTotDifferenceSNFKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotDifferenceSNFKG.TabIndex = 64
        Me.lblTotDifferenceSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotAddRemoveQty
        '
        Me.lblTotAddRemoveQty.AutoSize = False
        Me.lblTotAddRemoveQty.BorderVisible = True
        Me.lblTotAddRemoveQty.FieldName = Nothing
        Me.lblTotAddRemoveQty.Location = New System.Drawing.Point(119, 113)
        Me.lblTotAddRemoveQty.Name = "lblTotAddRemoveQty"
        Me.lblTotAddRemoveQty.Size = New System.Drawing.Size(153, 19)
        Me.lblTotAddRemoveQty.TabIndex = 67
        Me.lblTotAddRemoveQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotDifferenceFATKG
        '
        Me.lblTotDifferenceFATKG.AutoSize = False
        Me.lblTotDifferenceFATKG.BorderVisible = True
        Me.lblTotDifferenceFATKG.FieldName = Nothing
        Me.lblTotDifferenceFATKG.Location = New System.Drawing.Point(275, 91)
        Me.lblTotDifferenceFATKG.Name = "lblTotDifferenceFATKG"
        Me.lblTotDifferenceFATKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotDifferenceFATKG.TabIndex = 65
        Me.lblTotDifferenceFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel33
        '
        Me.MyLabel33.FieldName = Nothing
        Me.MyLabel33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel33.Location = New System.Drawing.Point(41, 114)
        Me.MyLabel33.Name = "MyLabel33"
        Me.MyLabel33.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel33.TabIndex = 66
        Me.MyLabel33.Text = "Add/Remove"
        '
        'lblTotDifferenceQty
        '
        Me.lblTotDifferenceQty.AutoSize = False
        Me.lblTotDifferenceQty.BorderVisible = True
        Me.lblTotDifferenceQty.FieldName = Nothing
        Me.lblTotDifferenceQty.Location = New System.Drawing.Point(119, 91)
        Me.lblTotDifferenceQty.Name = "lblTotDifferenceQty"
        Me.lblTotDifferenceQty.Size = New System.Drawing.Size(153, 19)
        Me.lblTotDifferenceQty.TabIndex = 63
        Me.lblTotDifferenceQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel29
        '
        Me.MyLabel29.FieldName = Nothing
        Me.MyLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel29.Location = New System.Drawing.Point(41, 92)
        Me.MyLabel29.Name = "MyLabel29"
        Me.MyLabel29.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel29.TabIndex = 62
        Me.MyLabel29.Text = "Difference"
        '
        'lblTotIssueSNFKG
        '
        Me.lblTotIssueSNFKG.AutoSize = False
        Me.lblTotIssueSNFKG.BorderVisible = True
        Me.lblTotIssueSNFKG.FieldName = Nothing
        Me.lblTotIssueSNFKG.Location = New System.Drawing.Point(431, 69)
        Me.lblTotIssueSNFKG.Name = "lblTotIssueSNFKG"
        Me.lblTotIssueSNFKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotIssueSNFKG.TabIndex = 60
        Me.lblTotIssueSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotProduceSNFKG
        '
        Me.lblTotProduceSNFKG.AutoSize = False
        Me.lblTotProduceSNFKG.BorderVisible = True
        Me.lblTotProduceSNFKG.FieldName = Nothing
        Me.lblTotProduceSNFKG.Location = New System.Drawing.Point(431, 47)
        Me.lblTotProduceSNFKG.Name = "lblTotProduceSNFKG"
        Me.lblTotProduceSNFKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotProduceSNFKG.TabIndex = 56
        Me.lblTotProduceSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotIssueFATKG
        '
        Me.lblTotIssueFATKG.AutoSize = False
        Me.lblTotIssueFATKG.BorderVisible = True
        Me.lblTotIssueFATKG.FieldName = Nothing
        Me.lblTotIssueFATKG.Location = New System.Drawing.Point(275, 69)
        Me.lblTotIssueFATKG.Name = "lblTotIssueFATKG"
        Me.lblTotIssueFATKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotIssueFATKG.TabIndex = 61
        Me.lblTotIssueFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotIssueQty
        '
        Me.lblTotIssueQty.AutoSize = False
        Me.lblTotIssueQty.BorderVisible = True
        Me.lblTotIssueQty.FieldName = Nothing
        Me.lblTotIssueQty.Location = New System.Drawing.Point(119, 69)
        Me.lblTotIssueQty.Name = "lblTotIssueQty"
        Me.lblTotIssueQty.Size = New System.Drawing.Size(153, 19)
        Me.lblTotIssueQty.TabIndex = 59
        Me.lblTotIssueQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotProduceFATKG
        '
        Me.lblTotProduceFATKG.AutoSize = False
        Me.lblTotProduceFATKG.BorderVisible = True
        Me.lblTotProduceFATKG.FieldName = Nothing
        Me.lblTotProduceFATKG.Location = New System.Drawing.Point(275, 47)
        Me.lblTotProduceFATKG.Name = "lblTotProduceFATKG"
        Me.lblTotProduceFATKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotProduceFATKG.TabIndex = 57
        Me.lblTotProduceFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel25
        '
        Me.MyLabel25.FieldName = Nothing
        Me.MyLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel25.Location = New System.Drawing.Point(41, 70)
        Me.MyLabel25.Name = "MyLabel25"
        Me.MyLabel25.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel25.TabIndex = 58
        Me.MyLabel25.Text = "Issue"
        '
        'lblTotBatchSNFKG
        '
        Me.lblTotBatchSNFKG.AutoSize = False
        Me.lblTotBatchSNFKG.BorderVisible = True
        Me.lblTotBatchSNFKG.FieldName = Nothing
        Me.lblTotBatchSNFKG.Location = New System.Drawing.Point(431, 25)
        Me.lblTotBatchSNFKG.Name = "lblTotBatchSNFKG"
        Me.lblTotBatchSNFKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotBatchSNFKG.TabIndex = 53
        Me.lblTotBatchSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotProduceQty
        '
        Me.lblTotProduceQty.AutoSize = False
        Me.lblTotProduceQty.BorderVisible = True
        Me.lblTotProduceQty.FieldName = Nothing
        Me.lblTotProduceQty.Location = New System.Drawing.Point(119, 47)
        Me.lblTotProduceQty.Name = "lblTotProduceQty"
        Me.lblTotProduceQty.Size = New System.Drawing.Size(153, 19)
        Me.lblTotProduceQty.TabIndex = 55
        Me.lblTotProduceQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotBatchFATKG
        '
        Me.lblTotBatchFATKG.AutoSize = False
        Me.lblTotBatchFATKG.BorderVisible = True
        Me.lblTotBatchFATKG.FieldName = Nothing
        Me.lblTotBatchFATKG.Location = New System.Drawing.Point(275, 25)
        Me.lblTotBatchFATKG.Name = "lblTotBatchFATKG"
        Me.lblTotBatchFATKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotBatchFATKG.TabIndex = 53
        Me.lblTotBatchFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(41, 48)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel21.TabIndex = 54
        Me.MyLabel21.Text = "Produce"
        '
        'lblTotBatchQty
        '
        Me.lblTotBatchQty.AutoSize = False
        Me.lblTotBatchQty.BorderVisible = True
        Me.lblTotBatchQty.FieldName = Nothing
        Me.lblTotBatchQty.Location = New System.Drawing.Point(119, 25)
        Me.lblTotBatchQty.Name = "lblTotBatchQty"
        Me.lblTotBatchQty.Size = New System.Drawing.Size(153, 19)
        Me.lblTotBatchQty.TabIndex = 52
        Me.lblTotBatchQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(41, 26)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(35, 16)
        Me.MyLabel15.TabIndex = 51
        Me.MyLabel15.Text = "Batch"
        '
        'MyLabel14
        '
        Me.MyLabel14.AutoSize = False
        Me.MyLabel14.BorderVisible = True
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Location = New System.Drawing.Point(119, 3)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(153, 19)
        Me.MyLabel14.TabIndex = 50
        Me.MyLabel14.Text = "Quantity"
        Me.MyLabel14.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MyLabel12
        '
        Me.MyLabel12.AutoSize = False
        Me.MyLabel12.BorderVisible = True
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Location = New System.Drawing.Point(431, 3)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(153, 19)
        Me.MyLabel12.TabIndex = 50
        Me.MyLabel12.Text = "SNF KG"
        Me.MyLabel12.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MyLabel7
        '
        Me.MyLabel7.AutoSize = False
        Me.MyLabel7.BorderVisible = True
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(275, 3)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(153, 19)
        Me.MyLabel7.TabIndex = 49
        Me.MyLabel7.Text = "FAT KG"
        Me.MyLabel7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(797, 170)
        Me.UcAttachment1.TabIndex = 5
        '
        'MyLabel2
        '
        Me.MyLabel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.ForeColor = System.Drawing.SystemColors.Highlight
        Me.MyLabel2.Location = New System.Drawing.Point(642, 9)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(329, 13)
        Me.MyLabel2.TabIndex = 32
        Me.MyLabel2.Text = "Press F4 to fill QC Log Sheet after selecting any stage in Stage Detail Tab"
        Me.MyLabel2.Visible = False
        '
        'frmProcessProductionStandardizationFinalQC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(830, 598)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmProcessProductionStandardizationFinalQC"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Standardization Final QC"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.chkJobWorkInward, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.pageIssueDetail.ResumeLayout(False)
        Me.pageAddRemoveDetail.ResumeLayout(False)
        Me.pageStageDetail.ResumeLayout(False)
        Me.pageBatchDetail.ResumeLayout(False)
        Me.pageParameterDetail.ResumeLayout(False)
        Me.pageSectionStock.ResumeLayout(False)
        Me.pageSectionStockHistory.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.pageAttachment.ResumeLayout(False)
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnunpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpdateParameters, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboStaus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChildBatchDesc, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConsmSectionLocCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConsmSectionCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtlocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMainBatchDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtlocationname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvParam.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvParam, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvIssue.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvIssue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvARDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvARDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvStage.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvStage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_qc.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_qc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSectionStock.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSectionStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSectionStockHistory.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSectionStockHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblJWESNFAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblJWEFATAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblJWESNFKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblJWEFATKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAvgRateSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAvgRateFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotNetSNFKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotNetFATKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotNetQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotAddRemoveSNFKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotAddRemoveFATKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotDifferenceSNFKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotAddRemoveQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotDifferenceFATKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotDifferenceQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotIssueSNFKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotProduceSNFKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotIssueFATKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotIssueQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotProduceFATKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotBatchSNFKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotProduceQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotBatchFATKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotBatchQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents lblMainBatchDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents fndMainBatchNo As common.UserControls.txtFinder
    Friend WithEvents lblChildBatchDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents fndChildBatchNo As common.UserControls.txtFinder
    Friend WithEvents txtlocationname As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents pageBatchDetail As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pageParameterDetail As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents gv_qc As common.UserControls.MyRadGridView
    Friend WithEvents pageAttachment As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents txtlocation As common.Controls.MyLabel
    Friend WithEvents btngo As Telerik.WinControls.UI.RadButton
    Friend WithEvents pageIssueDetail As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pageAddRemoveDetail As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvIssue As common.UserControls.MyRadGridView
    Friend WithEvents gvARDetail As common.UserControls.MyRadGridView
    Friend WithEvents pageStageDetail As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvStage As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents btnUpdateParameters As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblConsmSectionLocCode As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblConsmSectionCode As common.Controls.MyLabel
    Friend WithEvents pageSectionStock As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pageSectionStockHistory As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvSectionStock As common.UserControls.MyRadGridView
    Friend WithEvents gvSectionStockHistory As common.UserControls.MyRadGridView
    Friend WithEvents btnCancel As Telerik.WinControls.UI.RadButton
    Friend WithEvents TxtManualBatchNo As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents lblCostCenterName As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents lblProfitCenterName As common.Controls.MyLabel
    Friend WithEvents lblProfitCenterCode As common.Controls.MyLabel
    Friend WithEvents LblCostCenterCode As common.Controls.MyLabel
    Friend WithEvents lblLineNo As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel43 As common.Controls.MyLabel
    Friend WithEvents MyLabel42 As common.Controls.MyLabel
    Friend WithEvents MyLabel41 As common.Controls.MyLabel
    Friend WithEvents MyLabel40 As common.Controls.MyLabel
    Friend WithEvents MyLabel39 As common.Controls.MyLabel
    Friend WithEvents MyLabel38 As common.Controls.MyLabel
    Friend WithEvents lblTotNetSNFKG As common.Controls.MyLabel
    Friend WithEvents lblTotNetFATKG As common.Controls.MyLabel
    Friend WithEvents lblTotNetQty As common.Controls.MyLabel
    Friend WithEvents MyLabel37 As common.Controls.MyLabel
    Friend WithEvents lblTotAddRemoveSNFKG As common.Controls.MyLabel
    Friend WithEvents lblTotAddRemoveFATKG As common.Controls.MyLabel
    Friend WithEvents lblTotDifferenceSNFKG As common.Controls.MyLabel
    Friend WithEvents lblTotAddRemoveQty As common.Controls.MyLabel
    Friend WithEvents lblTotDifferenceFATKG As common.Controls.MyLabel
    Friend WithEvents MyLabel33 As common.Controls.MyLabel
    Friend WithEvents lblTotDifferenceQty As common.Controls.MyLabel
    Friend WithEvents MyLabel29 As common.Controls.MyLabel
    Friend WithEvents lblTotIssueSNFKG As common.Controls.MyLabel
    Friend WithEvents lblTotProduceSNFKG As common.Controls.MyLabel
    Friend WithEvents lblTotIssueFATKG As common.Controls.MyLabel
    Friend WithEvents lblTotIssueQty As common.Controls.MyLabel
    Friend WithEvents lblTotProduceFATKG As common.Controls.MyLabel
    Friend WithEvents MyLabel25 As common.Controls.MyLabel
    Friend WithEvents lblTotBatchSNFKG As common.Controls.MyLabel
    Friend WithEvents lblTotProduceQty As common.Controls.MyLabel
    Friend WithEvents lblTotBatchFATKG As common.Controls.MyLabel
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents lblTotBatchQty As common.Controls.MyLabel
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents lblAvgRateSNF As common.Controls.MyLabel
    Friend WithEvents lblAvgRateFAT As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtAgainstStdCode As common.UserControls.txtFinder
    Friend WithEvents gvParam As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel21 As common.Controls.MyLabel
    Friend WithEvents cboStaus As common.Controls.MyComboBox
    Friend WithEvents btnunpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblJWESNFAmt As common.Controls.MyLabel
    Friend WithEvents lblJWEFATAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents lblJWESNFKg As common.Controls.MyLabel
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents lblJWEFATKg As common.Controls.MyLabel
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents chkJobWorkInward As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnShowInventory As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
End Class

