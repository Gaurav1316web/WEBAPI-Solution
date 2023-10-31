<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmStandardization
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStandardization))
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition5 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition6 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.UsLock1 = New common.usLock()
        Me.lblCode = New common.Controls.MyLabel()
        Me.lblBomDate = New common.Controls.MyLabel()
        Me.dtpDate = New common.Controls.MyDateTimePicker()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.pageBatchDetail = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvProduce = New common.UserControls.MyRadGridView()
        Me.pageIssueDetail = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvIssue = New common.UserControls.MyRadGridView()
        Me.pageAddRemoveDetail = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvAddRemove = New common.UserControls.MyRadGridView()
        Me.pageAttachment = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel32 = New common.Controls.MyLabel()
        Me.MyLabel31 = New common.Controls.MyLabel()
        Me.lblTotRemovedSNFKG = New common.Controls.MyLabel()
        Me.lblTotRemovedFATKG = New common.Controls.MyLabel()
        Me.lblTotRemovedQty = New common.Controls.MyLabel()
        Me.MyLabel30 = New common.Controls.MyLabel()
        Me.lblTotAddedSNFKG = New common.Controls.MyLabel()
        Me.lblTotAddedFATKG = New common.Controls.MyLabel()
        Me.lblTotAddedQty = New common.Controls.MyLabel()
        Me.MyLabel23 = New common.Controls.MyLabel()
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
        Me.lblTotProduceQty = New common.Controls.MyLabel()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnShowInventory = New Telerik.WinControls.UI.RadButton()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.btnunpost = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.pageBatchDetail.SuspendLayout()
        CType(Me.gvProduce, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvProduce.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageIssueDetail.SuspendLayout()
        CType(Me.gvIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvIssue.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageAddRemoveDetail.SuspendLayout()
        CType(Me.gvAddRemove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAddRemove.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageAttachment.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRemovedSNFKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRemovedFATKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRemovedQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotAddedSNFKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotAddedFATKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotAddedQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.lblTotProduceQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnunpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnunpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(830, 503)
        Me.SplitContainer1.SplitterDistance = 465
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
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
        Me.SplitContainer2.Size = New System.Drawing.Size(824, 459)
        Me.SplitContainer2.SplitterDistance = 45
        Me.SplitContainer2.TabIndex = 0
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(596, 13)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(188, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 46
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(9, 15)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(75, 16)
        Me.lblCode.TabIndex = 4
        Me.lblCode.Text = "Document No"
        '
        'lblBomDate
        '
        Me.lblBomDate.FieldName = Nothing
        Me.lblBomDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBomDate.Location = New System.Drawing.Point(471, 15)
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
        Me.dtpDate.Location = New System.Drawing.Point(504, 14)
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
        Me.btnNew.Location = New System.Drawing.Point(446, 13)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(20, 21)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = " "
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(89, 13)
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
        Me.RadPageView1.Controls.Add(Me.pageBatchDetail)
        Me.RadPageView1.Controls.Add(Me.pageIssueDetail)
        Me.RadPageView1.Controls.Add(Me.pageAddRemoveDetail)
        Me.RadPageView1.Controls.Add(Me.pageAttachment)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(3, 3)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(818, 404)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'pageBatchDetail
        '
        Me.pageBatchDetail.Controls.Add(Me.gvProduce)
        Me.pageBatchDetail.ItemSize = New System.Drawing.SizeF(129.0!, 28.0!)
        Me.pageBatchDetail.Location = New System.Drawing.Point(10, 37)
        Me.pageBatchDetail.Name = "pageBatchDetail"
        Me.pageBatchDetail.Size = New System.Drawing.Size(797, 356)
        Me.pageBatchDetail.Text = "Production Item Detail"
        '
        'gvProduce
        '
        Me.gvProduce.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvProduce.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvProduce.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvProduce.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvProduce.ForeColor = System.Drawing.Color.Black
        Me.gvProduce.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvProduce.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvProduce.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvProduce.MasterTemplate.AllowAddNewRow = False
        Me.gvProduce.MasterTemplate.AutoGenerateColumns = False
        Me.gvProduce.MasterTemplate.EnableGrouping = False
        Me.gvProduce.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvProduce.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvProduce.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gvProduce.Name = "gvProduce"
        Me.gvProduce.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvProduce.ShowHeaderCellButtons = True
        Me.gvProduce.Size = New System.Drawing.Size(797, 356)
        Me.gvProduce.TabIndex = 0
        Me.gvProduce.TabStop = False
        '
        'pageIssueDetail
        '
        Me.pageIssueDetail.Controls.Add(Me.gvIssue)
        Me.pageIssueDetail.ItemSize = New System.Drawing.SizeF(99.0!, 28.0!)
        Me.pageIssueDetail.Location = New System.Drawing.Point(10, 37)
        Me.pageIssueDetail.Name = "pageIssueDetail"
        Me.pageIssueDetail.Size = New System.Drawing.Size(797, 356)
        Me.pageIssueDetail.Text = "Issue Item Detail"
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
        Me.gvIssue.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvIssue.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvIssue.MasterTemplate.ViewDefinition = TableViewDefinition5
        Me.gvIssue.Name = "gvIssue"
        Me.gvIssue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvIssue.ShowHeaderCellButtons = True
        Me.gvIssue.Size = New System.Drawing.Size(797, 356)
        Me.gvIssue.TabIndex = 1
        Me.gvIssue.TabStop = False
        '
        'pageAddRemoveDetail
        '
        Me.pageAddRemoveDetail.Controls.Add(Me.gvAddRemove)
        Me.pageAddRemoveDetail.ItemSize = New System.Drawing.SizeF(159.0!, 28.0!)
        Me.pageAddRemoveDetail.Location = New System.Drawing.Point(10, 37)
        Me.pageAddRemoveDetail.Name = "pageAddRemoveDetail"
        Me.pageAddRemoveDetail.Size = New System.Drawing.Size(797, 356)
        Me.pageAddRemoveDetail.Text = "Added/Removed Item Detail"
        '
        'gvAddRemove
        '
        Me.gvAddRemove.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvAddRemove.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvAddRemove.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvAddRemove.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvAddRemove.ForeColor = System.Drawing.Color.Black
        Me.gvAddRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvAddRemove.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvAddRemove.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvAddRemove.MasterTemplate.AllowAddNewRow = False
        Me.gvAddRemove.MasterTemplate.AutoGenerateColumns = False
        Me.gvAddRemove.MasterTemplate.EnableGrouping = False
        Me.gvAddRemove.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvAddRemove.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvAddRemove.MasterTemplate.ViewDefinition = TableViewDefinition6
        Me.gvAddRemove.Name = "gvAddRemove"
        Me.gvAddRemove.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvAddRemove.ShowHeaderCellButtons = True
        Me.gvAddRemove.Size = New System.Drawing.Size(797, 356)
        Me.gvAddRemove.TabIndex = 1
        Me.gvAddRemove.TabStop = False
        '
        'pageAttachment
        '
        Me.pageAttachment.Controls.Add(Me.UcAttachment1)
        Me.pageAttachment.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.pageAttachment.Location = New System.Drawing.Point(10, 37)
        Me.pageAttachment.Name = "pageAttachment"
        Me.pageAttachment.Size = New System.Drawing.Size(797, 356)
        Me.pageAttachment.Text = "Attahcment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(797, 356)
        Me.UcAttachment1.TabIndex = 5
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel32)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel31)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotRemovedSNFKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotRemovedFATKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotRemovedQty)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel30)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotAddedSNFKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotAddedFATKG)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotAddedQty)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel23)
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
        Me.RadPageViewPage1.Controls.Add(Me.lblTotProduceQty)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel21)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel14)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(797, 356)
        Me.RadPageViewPage1.Text = "Total"
        '
        'MyLabel32
        '
        Me.MyLabel32.FieldName = Nothing
        Me.MyLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel32.Location = New System.Drawing.Point(586, 130)
        Me.MyLabel32.Name = "MyLabel32"
        Me.MyLabel32.Size = New System.Drawing.Size(42, 16)
        Me.MyLabel32.TabIndex = 88
        Me.MyLabel32.Text = "F [D-E]"
        '
        'MyLabel31
        '
        Me.MyLabel31.FieldName = Nothing
        Me.MyLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel31.Location = New System.Drawing.Point(586, 109)
        Me.MyLabel31.Name = "MyLabel31"
        Me.MyLabel31.Size = New System.Drawing.Size(14, 16)
        Me.MyLabel31.TabIndex = 87
        Me.MyLabel31.Text = "E"
        '
        'lblTotRemovedSNFKG
        '
        Me.lblTotRemovedSNFKG.AutoSize = False
        Me.lblTotRemovedSNFKG.BorderVisible = True
        Me.lblTotRemovedSNFKG.FieldName = Nothing
        Me.lblTotRemovedSNFKG.Location = New System.Drawing.Point(431, 108)
        Me.lblTotRemovedSNFKG.Name = "lblTotRemovedSNFKG"
        Me.lblTotRemovedSNFKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotRemovedSNFKG.TabIndex = 85
        Me.lblTotRemovedSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotRemovedFATKG
        '
        Me.lblTotRemovedFATKG.AutoSize = False
        Me.lblTotRemovedFATKG.BorderVisible = True
        Me.lblTotRemovedFATKG.FieldName = Nothing
        Me.lblTotRemovedFATKG.Location = New System.Drawing.Point(275, 108)
        Me.lblTotRemovedFATKG.Name = "lblTotRemovedFATKG"
        Me.lblTotRemovedFATKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotRemovedFATKG.TabIndex = 86
        Me.lblTotRemovedFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotRemovedQty
        '
        Me.lblTotRemovedQty.AutoSize = False
        Me.lblTotRemovedQty.BorderVisible = True
        Me.lblTotRemovedQty.FieldName = Nothing
        Me.lblTotRemovedQty.Location = New System.Drawing.Point(119, 108)
        Me.lblTotRemovedQty.Name = "lblTotRemovedQty"
        Me.lblTotRemovedQty.Size = New System.Drawing.Size(153, 19)
        Me.lblTotRemovedQty.TabIndex = 84
        Me.lblTotRemovedQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel30
        '
        Me.MyLabel30.FieldName = Nothing
        Me.MyLabel30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel30.Location = New System.Drawing.Point(41, 109)
        Me.MyLabel30.Name = "MyLabel30"
        Me.MyLabel30.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel30.TabIndex = 83
        Me.MyLabel30.Text = "Removed"
        '
        'lblTotAddedSNFKG
        '
        Me.lblTotAddedSNFKG.AutoSize = False
        Me.lblTotAddedSNFKG.BorderVisible = True
        Me.lblTotAddedSNFKG.FieldName = Nothing
        Me.lblTotAddedSNFKG.Location = New System.Drawing.Point(431, 87)
        Me.lblTotAddedSNFKG.Name = "lblTotAddedSNFKG"
        Me.lblTotAddedSNFKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotAddedSNFKG.TabIndex = 81
        Me.lblTotAddedSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotAddedFATKG
        '
        Me.lblTotAddedFATKG.AutoSize = False
        Me.lblTotAddedFATKG.BorderVisible = True
        Me.lblTotAddedFATKG.FieldName = Nothing
        Me.lblTotAddedFATKG.Location = New System.Drawing.Point(275, 87)
        Me.lblTotAddedFATKG.Name = "lblTotAddedFATKG"
        Me.lblTotAddedFATKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotAddedFATKG.TabIndex = 82
        Me.lblTotAddedFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotAddedQty
        '
        Me.lblTotAddedQty.AutoSize = False
        Me.lblTotAddedQty.BorderVisible = True
        Me.lblTotAddedQty.FieldName = Nothing
        Me.lblTotAddedQty.Location = New System.Drawing.Point(119, 87)
        Me.lblTotAddedQty.Name = "lblTotAddedQty"
        Me.lblTotAddedQty.Size = New System.Drawing.Size(153, 19)
        Me.lblTotAddedQty.TabIndex = 80
        Me.lblTotAddedQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel23
        '
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel23.Location = New System.Drawing.Point(41, 88)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(39, 16)
        Me.MyLabel23.TabIndex = 79
        Me.MyLabel23.Text = "Added"
        '
        'MyLabel42
        '
        Me.MyLabel42.FieldName = Nothing
        Me.MyLabel42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel42.Location = New System.Drawing.Point(586, 151)
        Me.MyLabel42.Name = "MyLabel42"
        Me.MyLabel42.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel42.TabIndex = 52
        Me.MyLabel42.Text = "G [C+F]"
        '
        'MyLabel41
        '
        Me.MyLabel41.FieldName = Nothing
        Me.MyLabel41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel41.Location = New System.Drawing.Point(586, 88)
        Me.MyLabel41.Name = "MyLabel41"
        Me.MyLabel41.Size = New System.Drawing.Size(14, 16)
        Me.MyLabel41.TabIndex = 52
        Me.MyLabel41.Text = "D"
        '
        'MyLabel40
        '
        Me.MyLabel40.FieldName = Nothing
        Me.MyLabel40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel40.Location = New System.Drawing.Point(586, 67)
        Me.MyLabel40.Name = "MyLabel40"
        Me.MyLabel40.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel40.TabIndex = 52
        Me.MyLabel40.Text = "C [ A-B]"
        '
        'MyLabel39
        '
        Me.MyLabel39.FieldName = Nothing
        Me.MyLabel39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel39.Location = New System.Drawing.Point(586, 46)
        Me.MyLabel39.Name = "MyLabel39"
        Me.MyLabel39.Size = New System.Drawing.Size(14, 16)
        Me.MyLabel39.TabIndex = 52
        Me.MyLabel39.Text = "B"
        '
        'MyLabel38
        '
        Me.MyLabel38.FieldName = Nothing
        Me.MyLabel38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel38.Location = New System.Drawing.Point(586, 25)
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
        Me.lblTotNetSNFKG.Location = New System.Drawing.Point(431, 150)
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
        Me.lblTotNetFATKG.Location = New System.Drawing.Point(275, 150)
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
        Me.lblTotNetQty.Location = New System.Drawing.Point(119, 150)
        Me.lblTotNetQty.Name = "lblTotNetQty"
        Me.lblTotNetQty.Size = New System.Drawing.Size(153, 19)
        Me.lblTotNetQty.TabIndex = 71
        Me.lblTotNetQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel37
        '
        Me.MyLabel37.FieldName = Nothing
        Me.MyLabel37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel37.Location = New System.Drawing.Point(41, 151)
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
        Me.lblTotAddRemoveSNFKG.Location = New System.Drawing.Point(431, 129)
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
        Me.lblTotAddRemoveFATKG.Location = New System.Drawing.Point(275, 129)
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
        Me.lblTotDifferenceSNFKG.Location = New System.Drawing.Point(431, 66)
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
        Me.lblTotAddRemoveQty.Location = New System.Drawing.Point(119, 129)
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
        Me.lblTotDifferenceFATKG.Location = New System.Drawing.Point(275, 66)
        Me.lblTotDifferenceFATKG.Name = "lblTotDifferenceFATKG"
        Me.lblTotDifferenceFATKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotDifferenceFATKG.TabIndex = 65
        Me.lblTotDifferenceFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel33
        '
        Me.MyLabel33.FieldName = Nothing
        Me.MyLabel33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel33.Location = New System.Drawing.Point(41, 130)
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
        Me.lblTotDifferenceQty.Location = New System.Drawing.Point(119, 66)
        Me.lblTotDifferenceQty.Name = "lblTotDifferenceQty"
        Me.lblTotDifferenceQty.Size = New System.Drawing.Size(153, 19)
        Me.lblTotDifferenceQty.TabIndex = 63
        Me.lblTotDifferenceQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel29
        '
        Me.MyLabel29.FieldName = Nothing
        Me.MyLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel29.Location = New System.Drawing.Point(41, 67)
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
        Me.lblTotIssueSNFKG.Location = New System.Drawing.Point(431, 45)
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
        Me.lblTotProduceSNFKG.Location = New System.Drawing.Point(431, 24)
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
        Me.lblTotIssueFATKG.Location = New System.Drawing.Point(275, 45)
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
        Me.lblTotIssueQty.Location = New System.Drawing.Point(119, 45)
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
        Me.lblTotProduceFATKG.Location = New System.Drawing.Point(275, 24)
        Me.lblTotProduceFATKG.Name = "lblTotProduceFATKG"
        Me.lblTotProduceFATKG.Size = New System.Drawing.Size(153, 19)
        Me.lblTotProduceFATKG.TabIndex = 57
        Me.lblTotProduceFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel25
        '
        Me.MyLabel25.FieldName = Nothing
        Me.MyLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel25.Location = New System.Drawing.Point(41, 46)
        Me.MyLabel25.Name = "MyLabel25"
        Me.MyLabel25.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel25.TabIndex = 58
        Me.MyLabel25.Text = "Issue"
        '
        'lblTotProduceQty
        '
        Me.lblTotProduceQty.AutoSize = False
        Me.lblTotProduceQty.BorderVisible = True
        Me.lblTotProduceQty.FieldName = Nothing
        Me.lblTotProduceQty.Location = New System.Drawing.Point(119, 24)
        Me.lblTotProduceQty.Name = "lblTotProduceQty"
        Me.lblTotProduceQty.Size = New System.Drawing.Size(153, 19)
        Me.lblTotProduceQty.TabIndex = 55
        Me.lblTotProduceQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(41, 25)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel21.TabIndex = 54
        Me.MyLabel21.Text = "Produce"
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
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(492, 5)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(71, 22)
        Me.btnHistory.TabIndex = 37
        Me.btnHistory.Text = "&History"
        '
        'btnShowInventory
        '
        Me.btnShowInventory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnShowInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowInventory.Location = New System.Drawing.Point(398, 5)
        Me.btnShowInventory.Name = "btnShowInventory"
        Me.btnShowInventory.Size = New System.Drawing.Size(91, 22)
        Me.btnShowInventory.TabIndex = 37
        Me.btnShowInventory.Text = "Show Inventory"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(240, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(76, 22)
        Me.btnCancel.TabIndex = 34
        Me.btnCancel.Text = "Cancel"
        '
        'btnunpost
        '
        Me.btnunpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnunpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnunpost.Location = New System.Drawing.Point(319, 5)
        Me.btnunpost.Name = "btnunpost"
        Me.btnunpost.Size = New System.Drawing.Size(76, 22)
        Me.btnunpost.TabIndex = 33
        Me.btnunpost.Text = "Unpost"
        Me.btnunpost.Visible = False
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(161, 5)
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
        Me.btnclose.Location = New System.Drawing.Point(743, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(78, 22)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(82, 5)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(76, 22)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'frmStandardization
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(830, 503)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmStandardization"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Standardization"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.pageBatchDetail.ResumeLayout(False)
        CType(Me.gvProduce.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvProduce, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageIssueDetail.ResumeLayout(False)
        CType(Me.gvIssue.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvIssue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageAddRemoveDetail.ResumeLayout(False)
        CType(Me.gvAddRemove.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAddRemove, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageAttachment.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRemovedSNFKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRemovedFATKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRemovedQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotAddedSNFKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotAddedFATKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotAddedQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.lblTotProduceQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnunpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents pageBatchDetail As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvProduce As common.UserControls.MyRadGridView
    Friend WithEvents pageAttachment As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents pageIssueDetail As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pageAddRemoveDetail As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvIssue As common.UserControls.MyRadGridView
    Friend WithEvents gvAddRemove As common.UserControls.MyRadGridView
    Friend WithEvents btnunpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnCancel As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
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
    Friend WithEvents lblTotProduceQty As common.Controls.MyLabel
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents btnShowInventory As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel32 As common.Controls.MyLabel
    Friend WithEvents MyLabel31 As common.Controls.MyLabel
    Friend WithEvents lblTotRemovedSNFKG As common.Controls.MyLabel
    Friend WithEvents lblTotRemovedFATKG As common.Controls.MyLabel
    Friend WithEvents lblTotRemovedQty As common.Controls.MyLabel
    Friend WithEvents MyLabel30 As common.Controls.MyLabel
    Friend WithEvents lblTotAddedSNFKG As common.Controls.MyLabel
    Friend WithEvents lblTotAddedFATKG As common.Controls.MyLabel
    Friend WithEvents lblTotAddedQty As common.Controls.MyLabel
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
End Class

