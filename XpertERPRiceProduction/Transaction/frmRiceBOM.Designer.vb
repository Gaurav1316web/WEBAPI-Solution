Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRiceBOM
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.txtCode = New common.UserControls.txtNavigator
        Me.lblCode = New common.Controls.MyLabel
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadSplitContainer1 = New Telerik.WinControls.UI.RadSplitContainer
        Me.SplitPanel1 = New Telerik.WinControls.UI.SplitPanel
        Me.txtadmin_amt = New common.MyNumBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtprocess_amt = New common.MyNumBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.txtDescription = New common.Controls.MyTextBox
        Me.lblBomDesc = New common.Controls.MyLabel
        Me.UsLock1 = New common.usLock
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.lblRevision = New common.Controls.MyLabel
        Me.lblMasterItem = New common.Controls.MyLabel
        Me.txtProducedItem = New common.UserControls.txtFinder
        Me.lblMasterItemName = New common.Controls.MyLabel
        Me.lblRevisionNo = New common.Controls.MyLabel
        Me.SplitPanel2 = New Telerik.WinControls.UI.SplitPanel
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.pageGeneral = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadPanel4 = New Telerik.WinControls.UI.RadPanel
        Me.gvCost = New common.UserControls.MyRadGridView
        Me.lblBomDate = New common.Controls.MyLabel
        Me.dtpBOMDate = New common.Controls.MyDateTimePicker
        Me.lblApprovedByName = New common.Controls.MyLabel
        Me.lblApprovedBy = New common.Controls.MyLabel
        Me.lblCreatedByName = New common.Controls.MyLabel
        Me.lblCreatedBy = New common.Controls.MyLabel
        Me.txtMinBatchQty = New common.MyNumBox
        Me.lblMinBatchSize = New common.Controls.MyLabel
        Me.lblStartDate = New common.Controls.MyLabel
        Me.dtpStartDate = New common.Controls.MyDateTimePicker
        Me.lblUnitName = New common.Controls.MyLabel
        Me.txtBuildQty = New common.MyNumBox
        Me.lblBuildQty = New common.Controls.MyLabel
        Me.lblEndDate = New common.Controls.MyLabel
        Me.dtpEndDate = New common.Controls.MyDateTimePicker
        Me.btnBrowse = New Telerik.WinControls.UI.RadButton
        Me.lblBOMStatus = New common.Controls.MyLabel
        Me.txtDocPath = New common.Controls.MyTextBox
        Me.lblDocument = New common.Controls.MyLabel
        Me.cboBOMStatus = New common.Controls.MyComboBox
        Me.btnNewFile = New Telerik.WinControls.UI.RadButton
        Me.pageOperations = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadSplitContainer2 = New Telerik.WinControls.UI.RadSplitContainer
        Me.SplitPanel3 = New Telerik.WinControls.UI.SplitPanel
        Me.gvResources = New common.UserControls.MyRadGridView
        Me.SplitPanel4 = New Telerik.WinControls.UI.SplitPanel
        Me.gvTools = New common.UserControls.MyRadGridView
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel
        Me.gvOperations = New common.UserControls.MyRadGridView
        Me.pageComponent = New Telerik.WinControls.UI.RadPageViewPage
        Me.gvBOM = New common.UserControls.MyRadGridView
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.btnPost = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadSplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadSplitContainer1.SuspendLayout()
        CType(Me.SplitPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel1.SuspendLayout()
        CType(Me.txtadmin_amt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtprocess_amt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBomDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRevision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMasterItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMasterItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRevisionNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel2.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.pageGeneral.SuspendLayout()
        CType(Me.RadPanel4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel4.SuspendLayout()
        CType(Me.gvCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCost.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpBOMDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApprovedByName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApprovedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreatedByName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreatedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMinBatchQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMinBatchSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnitName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBuildQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBuildQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboBOMStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNewFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageOperations.SuspendLayout()
        CType(Me.RadSplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadSplitContainer2.SuspendLayout()
        CType(Me.SplitPanel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel3.SuspendLayout()
        CType(Me.gvResources, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvResources.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitPanel4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel4.SuspendLayout()
        CType(Me.gvTools, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTools.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.gvOperations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvOperations.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageComponent.SuspendLayout()
        CType(Me.gvBOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvBOM.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(108, 44)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(322, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCode.Location = New System.Drawing.Point(10, 48)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(63, 16)
        Me.lblCode.TabIndex = 7
        Me.lblCode.Text = "Bom Code"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.SplitContainer1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 1, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(986, 548)
        Me.RadGroupBox1.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(10, 1)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadSplitContainer1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(966, 537)
        Me.SplitContainer1.SplitterDistance = 496
        Me.SplitContainer1.TabIndex = 203
        '
        'RadSplitContainer1
        '
        Me.RadSplitContainer1.Controls.Add(Me.SplitPanel1)
        Me.RadSplitContainer1.Controls.Add(Me.SplitPanel2)
        Me.RadSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadSplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.RadSplitContainer1.Name = "RadSplitContainer1"
        Me.RadSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        '
        '
        Me.RadSplitContainer1.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.RadSplitContainer1.Size = New System.Drawing.Size(966, 496)
        Me.RadSplitContainer1.TabIndex = 230
        Me.RadSplitContainer1.TabStop = False
        Me.RadSplitContainer1.Text = "RadSplitContainer1"
        '
        'SplitPanel1
        '
        Me.SplitPanel1.Controls.Add(Me.txtadmin_amt)
        Me.SplitPanel1.Controls.Add(Me.MyLabel2)
        Me.SplitPanel1.Controls.Add(Me.txtprocess_amt)
        Me.SplitPanel1.Controls.Add(Me.MyLabel1)
        Me.SplitPanel1.Controls.Add(Me.RadMenu1)
        Me.SplitPanel1.Controls.Add(Me.txtDescription)
        Me.SplitPanel1.Controls.Add(Me.lblCode)
        Me.SplitPanel1.Controls.Add(Me.lblBomDesc)
        Me.SplitPanel1.Controls.Add(Me.UsLock1)
        Me.SplitPanel1.Controls.Add(Me.btnNew)
        Me.SplitPanel1.Controls.Add(Me.txtCode)
        Me.SplitPanel1.Controls.Add(Me.lblRevision)
        Me.SplitPanel1.Controls.Add(Me.lblMasterItem)
        Me.SplitPanel1.Controls.Add(Me.txtProducedItem)
        Me.SplitPanel1.Controls.Add(Me.lblMasterItemName)
        Me.SplitPanel1.Controls.Add(Me.lblRevisionNo)
        Me.SplitPanel1.Location = New System.Drawing.Point(0, 0)
        Me.SplitPanel1.Name = "SplitPanel1"
        '
        '
        '
        Me.SplitPanel1.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.SplitPanel1.Size = New System.Drawing.Size(966, 151)
        Me.SplitPanel1.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(0.0!, -0.1924686!)
        Me.SplitPanel1.SizeInfo.SplitterCorrection = New System.Drawing.Size(0, -78)
        Me.SplitPanel1.TabIndex = 0
        Me.SplitPanel1.TabStop = False
        Me.SplitPanel1.Text = "SplitPanel1"
        '
        'txtadmin_amt
        '
        Me.txtadmin_amt.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtadmin_amt.DecimalPlaces = 0
        Me.txtadmin_amt.Location = New System.Drawing.Point(670, 113)
        Me.txtadmin_amt.MendatroryField = False
        Me.txtadmin_amt.MyLinkLable1 = Me.MyLabel2
        Me.txtadmin_amt.MyLinkLable2 = Nothing
        Me.txtadmin_amt.Name = "txtadmin_amt"
        Me.txtadmin_amt.Size = New System.Drawing.Size(129, 20)
        Me.txtadmin_amt.TabIndex = 5
        Me.txtadmin_amt.Text = "0"
        Me.txtadmin_amt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtadmin_amt.Value = 0
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(585, 115)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel2.TabIndex = 23
        Me.MyLabel2.Text = "Admin Charge"
        '
        'txtprocess_amt
        '
        Me.txtprocess_amt.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtprocess_amt.DecimalPlaces = 0
        Me.txtprocess_amt.Location = New System.Drawing.Point(444, 113)
        Me.txtprocess_amt.MendatroryField = False
        Me.txtprocess_amt.MyLinkLable1 = Me.MyLabel1
        Me.txtprocess_amt.MyLinkLable2 = Nothing
        Me.txtprocess_amt.Name = "txtprocess_amt"
        Me.txtprocess_amt.Size = New System.Drawing.Size(129, 20)
        Me.txtprocess_amt.TabIndex = 4
        Me.txtprocess_amt.Text = "0"
        Me.txtprocess_amt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtprocess_amt.Value = 0
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(336, 115)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(103, 16)
        Me.MyLabel1.TabIndex = 21
        Me.MyLabel1.Text = "Processing Charge"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(966, 20)
        Me.RadMenu1.TabIndex = 11
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Setting"
        Me.RadMenuItem2.AccessibleName = "Setting"
        Me.RadMenuItem2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem3})
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Setting"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Save Layout"
        Me.RadMenuItem1.AccessibleName = "Save Layout"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem3.AccessibleName = "Delete Layout"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Delete Layout"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(108, 68)
        Me.txtDescription.MaxLength = 200
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.lblBomDesc
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(807, 20)
        Me.txtDescription.TabIndex = 2
        '
        'lblBomDesc
        '
        Me.lblBomDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBomDesc.Location = New System.Drawing.Point(11, 70)
        Me.lblBomDesc.Name = "lblBomDesc"
        Me.lblBomDesc.Size = New System.Drawing.Size(63, 16)
        Me.lblBomDesc.TabIndex = 6
        Me.lblBomDesc.Text = "Description"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(449, 45)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 0
        '
        'btnNew
        '
        Me.btnNew.Image = My.Resources._new
        Me.btnNew.Location = New System.Drawing.Point(431, 45)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'lblRevision
        '
        Me.lblRevision.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRevision.Location = New System.Drawing.Point(11, 114)
        Me.lblRevision.Name = "lblRevision"
        Me.lblRevision.Size = New System.Drawing.Size(67, 16)
        Me.lblRevision.TabIndex = 4
        Me.lblRevision.Text = "Revision No"
        '
        'lblMasterItem
        '
        Me.lblMasterItem.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMasterItem.Location = New System.Drawing.Point(10, 93)
        Me.lblMasterItem.Name = "lblMasterItem"
        Me.lblMasterItem.Size = New System.Drawing.Size(53, 18)
        Me.lblMasterItem.TabIndex = 5
        Me.lblMasterItem.Text = "Raw Item"
        '
        'txtProducedItem
        '
        Me.txtProducedItem.Location = New System.Drawing.Point(108, 92)
        Me.txtProducedItem.MendatroryField = True
        Me.txtProducedItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProducedItem.MyLinkLable1 = Me.lblMasterItem
        Me.txtProducedItem.MyLinkLable2 = Nothing
        Me.txtProducedItem.MyReadOnly = False
        Me.txtProducedItem.MyShowMasterFormButton = False
        Me.txtProducedItem.Name = "txtProducedItem"
        Me.txtProducedItem.Size = New System.Drawing.Size(219, 19)
        Me.txtProducedItem.TabIndex = 3
        Me.txtProducedItem.Value = ""
        '
        'lblMasterItemName
        '
        Me.lblMasterItemName.AutoSize = False
        Me.lblMasterItemName.BorderVisible = True
        Me.lblMasterItemName.Location = New System.Drawing.Point(336, 92)
        Me.lblMasterItemName.Name = "lblMasterItemName"
        Me.lblMasterItemName.Size = New System.Drawing.Size(579, 19)
        Me.lblMasterItemName.TabIndex = 10
        Me.lblMasterItemName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRevisionNo
        '
        Me.lblRevisionNo.AutoSize = False
        Me.lblRevisionNo.BorderVisible = True
        Me.lblRevisionNo.Location = New System.Drawing.Point(108, 114)
        Me.lblRevisionNo.Name = "lblRevisionNo"
        Me.lblRevisionNo.Size = New System.Drawing.Size(218, 19)
        Me.lblRevisionNo.TabIndex = 9
        Me.lblRevisionNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SplitPanel2
        '
        Me.SplitPanel2.Controls.Add(Me.RadPageView1)
        Me.SplitPanel2.Location = New System.Drawing.Point(0, 155)
        Me.SplitPanel2.Name = "SplitPanel2"
        '
        '
        '
        Me.SplitPanel2.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.SplitPanel2.Size = New System.Drawing.Size(966, 341)
        Me.SplitPanel2.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(0.0!, 0.1924686!)
        Me.SplitPanel2.SizeInfo.SplitterCorrection = New System.Drawing.Size(0, 78)
        Me.SplitPanel2.TabIndex = 1
        Me.SplitPanel2.TabStop = False
        Me.SplitPanel2.Text = "SplitPanel2"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.pageGeneral)
        Me.RadPageView1.Controls.Add(Me.pageOperations)
        Me.RadPageView1.Controls.Add(Me.pageComponent)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.pageGeneral
        Me.RadPageView1.Size = New System.Drawing.Size(966, 341)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'pageGeneral
        '
        Me.pageGeneral.Controls.Add(Me.RadPanel4)
        Me.pageGeneral.Controls.Add(Me.lblBomDate)
        Me.pageGeneral.Controls.Add(Me.dtpBOMDate)
        Me.pageGeneral.Controls.Add(Me.lblApprovedByName)
        Me.pageGeneral.Controls.Add(Me.lblApprovedBy)
        Me.pageGeneral.Controls.Add(Me.lblCreatedByName)
        Me.pageGeneral.Controls.Add(Me.lblCreatedBy)
        Me.pageGeneral.Controls.Add(Me.txtMinBatchQty)
        Me.pageGeneral.Controls.Add(Me.lblStartDate)
        Me.pageGeneral.Controls.Add(Me.dtpStartDate)
        Me.pageGeneral.Controls.Add(Me.lblMinBatchSize)
        Me.pageGeneral.Controls.Add(Me.lblUnitName)
        Me.pageGeneral.Controls.Add(Me.txtBuildQty)
        Me.pageGeneral.Controls.Add(Me.lblEndDate)
        Me.pageGeneral.Controls.Add(Me.lblBuildQty)
        Me.pageGeneral.Controls.Add(Me.dtpEndDate)
        Me.pageGeneral.Controls.Add(Me.btnBrowse)
        Me.pageGeneral.Controls.Add(Me.lblBOMStatus)
        Me.pageGeneral.Controls.Add(Me.txtDocPath)
        Me.pageGeneral.Controls.Add(Me.cboBOMStatus)
        Me.pageGeneral.Controls.Add(Me.lblDocument)
        Me.pageGeneral.Controls.Add(Me.btnNewFile)
        Me.pageGeneral.ItemSize = New System.Drawing.SizeF(55.0!, 28.0!)
        Me.pageGeneral.Location = New System.Drawing.Point(10, 37)
        Me.pageGeneral.Name = "pageGeneral"
        Me.pageGeneral.Size = New System.Drawing.Size(945, 293)
        Me.pageGeneral.Text = "General"
        '
        'RadPanel4
        '
        Me.RadPanel4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadPanel4.Controls.Add(Me.gvCost)
        Me.RadPanel4.Location = New System.Drawing.Point(3, 117)
        Me.RadPanel4.Name = "RadPanel4"
        Me.RadPanel4.Size = New System.Drawing.Size(907, 173)
        Me.RadPanel4.TabIndex = 229
        Me.RadPanel4.Text = "RadPanel4"
        '
        'gvCost
        '
        Me.gvCost.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvCost.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvCost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gvCost.ForeColor = System.Drawing.Color.Black
        Me.gvCost.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvCost.Location = New System.Drawing.Point(0, 0)
        '
        'gvCost
        '
        Me.gvCost.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvCost.MasterTemplate.AllowAddNewRow = False
        Me.gvCost.MasterTemplate.AllowColumnReorder = False
        Me.gvCost.MasterTemplate.AutoGenerateColumns = False
        Me.gvCost.MasterTemplate.EnableGrouping = False
        Me.gvCost.Name = "gvCost"
        Me.gvCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvCost.Size = New System.Drawing.Size(907, 173)
        Me.gvCost.TabIndex = 0
        Me.gvCost.TabStop = False
        Me.gvCost.Text = "RadGridView4"
        '
        'lblBomDate
        '
        Me.lblBomDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBomDate.Location = New System.Drawing.Point(3, 10)
        Me.lblBomDate.Name = "lblBomDate"
        Me.lblBomDate.Size = New System.Drawing.Size(59, 16)
        Me.lblBomDate.TabIndex = 17
        Me.lblBomDate.Text = "BOM Date"
        '
        'dtpBOMDate
        '
        Me.dtpBOMDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpBOMDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpBOMDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBOMDate.Location = New System.Drawing.Point(101, 8)
        Me.dtpBOMDate.MendatroryField = True
        Me.dtpBOMDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBOMDate.MyLinkLable1 = Me.lblBomDate
        Me.dtpBOMDate.MyLinkLable2 = Nothing
        Me.dtpBOMDate.Name = "dtpBOMDate"
        Me.dtpBOMDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBOMDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpBOMDate.TabIndex = 0
        Me.dtpBOMDate.TabStop = False
        Me.dtpBOMDate.Text = "03/05/2011"
        Me.dtpBOMDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblApprovedByName
        '
        Me.lblApprovedByName.AutoSize = False
        Me.lblApprovedByName.BorderVisible = True
        Me.lblApprovedByName.Location = New System.Drawing.Point(648, 72)
        Me.lblApprovedByName.Name = "lblApprovedByName"
        Me.lblApprovedByName.Size = New System.Drawing.Size(260, 19)
        Me.lblApprovedByName.TabIndex = 20
        Me.lblApprovedByName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblApprovedBy
        '
        Me.lblApprovedBy.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblApprovedBy.Location = New System.Drawing.Point(557, 74)
        Me.lblApprovedBy.Name = "lblApprovedBy"
        Me.lblApprovedBy.Size = New System.Drawing.Size(71, 16)
        Me.lblApprovedBy.TabIndex = 13
        Me.lblApprovedBy.Text = "Approved By"
        '
        'lblCreatedByName
        '
        Me.lblCreatedByName.AutoSize = False
        Me.lblCreatedByName.BorderVisible = True
        Me.lblCreatedByName.Location = New System.Drawing.Point(648, 52)
        Me.lblCreatedByName.Name = "lblCreatedByName"
        Me.lblCreatedByName.Size = New System.Drawing.Size(260, 19)
        Me.lblCreatedByName.TabIndex = 21
        Me.lblCreatedByName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCreatedBy
        '
        Me.lblCreatedBy.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCreatedBy.Location = New System.Drawing.Point(557, 52)
        Me.lblCreatedBy.Name = "lblCreatedBy"
        Me.lblCreatedBy.Size = New System.Drawing.Size(63, 16)
        Me.lblCreatedBy.TabIndex = 16
        Me.lblCreatedBy.Text = "Created By"
        '
        'txtMinBatchQty
        '
        Me.txtMinBatchQty.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMinBatchQty.DecimalPlaces = 0
        Me.txtMinBatchQty.Location = New System.Drawing.Point(648, 29)
        Me.txtMinBatchQty.MendatroryField = True
        Me.txtMinBatchQty.MyLinkLable1 = Me.lblMinBatchSize
        Me.txtMinBatchQty.MyLinkLable2 = Nothing
        Me.txtMinBatchQty.Name = "txtMinBatchQty"
        Me.txtMinBatchQty.Size = New System.Drawing.Size(129, 20)
        Me.txtMinBatchQty.TabIndex = 5
        Me.txtMinBatchQty.Text = "0"
        Me.txtMinBatchQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMinBatchQty.Value = 0
        '
        'lblMinBatchSize
        '
        Me.lblMinBatchSize.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblMinBatchSize.Location = New System.Drawing.Point(557, 31)
        Me.lblMinBatchSize.Name = "lblMinBatchSize"
        Me.lblMinBatchSize.Size = New System.Drawing.Size(85, 16)
        Me.lblMinBatchSize.TabIndex = 18
        Me.lblMinBatchSize.Text = "Min. Batch Size"
        '
        'lblStartDate
        '
        Me.lblStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartDate.Location = New System.Drawing.Point(2, 31)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(57, 16)
        Me.lblStartDate.TabIndex = 15
        Me.lblStartDate.Text = "Start Date"
        '
        'dtpStartDate
        '
        Me.dtpStartDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDate.Location = New System.Drawing.Point(101, 29)
        Me.dtpStartDate.MendatroryField = False
        Me.dtpStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartDate.MyLinkLable1 = Me.lblStartDate
        Me.dtpStartDate.MyLinkLable2 = Nothing
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpStartDate.TabIndex = 1
        Me.dtpStartDate.TabStop = False
        Me.dtpStartDate.Text = "03/05/2011"
        Me.dtpStartDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblUnitName
        '
        Me.lblUnitName.AutoSize = False
        Me.lblUnitName.BorderVisible = True
        Me.lblUnitName.Location = New System.Drawing.Point(782, 7)
        Me.lblUnitName.Name = "lblUnitName"
        Me.lblUnitName.Size = New System.Drawing.Size(128, 19)
        Me.lblUnitName.TabIndex = 2
        Me.lblUnitName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBuildQty
        '
        Me.txtBuildQty.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtBuildQty.DecimalPlaces = 0
        Me.txtBuildQty.Location = New System.Drawing.Point(648, 6)
        Me.txtBuildQty.MendatroryField = True
        Me.txtBuildQty.MyLinkLable1 = Me.lblBuildQty
        Me.txtBuildQty.MyLinkLable2 = Nothing
        Me.txtBuildQty.Name = "txtBuildQty"
        Me.txtBuildQty.Size = New System.Drawing.Size(129, 20)
        Me.txtBuildQty.TabIndex = 4
        Me.txtBuildQty.Text = "0"
        Me.txtBuildQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBuildQty.Value = 0
        '
        'lblBuildQty
        '
        Me.lblBuildQty.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblBuildQty.Location = New System.Drawing.Point(557, 8)
        Me.lblBuildQty.Name = "lblBuildQty"
        Me.lblBuildQty.Size = New System.Drawing.Size(78, 16)
        Me.lblBuildQty.TabIndex = 19
        Me.lblBuildQty.Text = "Used Quantity"
        '
        'lblEndDate
        '
        Me.lblEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEndDate.Location = New System.Drawing.Point(3, 53)
        Me.lblEndDate.Name = "lblEndDate"
        Me.lblEndDate.Size = New System.Drawing.Size(53, 16)
        Me.lblEndDate.TabIndex = 14
        Me.lblEndDate.Text = "End Date"
        '
        'dtpEndDate
        '
        Me.dtpEndDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDate.Location = New System.Drawing.Point(101, 51)
        Me.dtpEndDate.MendatroryField = False
        Me.dtpEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndDate.MyLinkLable1 = Me.lblEndDate
        Me.dtpEndDate.MyLinkLable2 = Nothing
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndDate.ShowCheckBox = True
        Me.dtpEndDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpEndDate.TabIndex = 2
        Me.dtpEndDate.TabStop = False
        Me.dtpEndDate.Text = "03/05/2011"
        Me.dtpEndDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'btnBrowse
        '
        Me.btnBrowse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(842, 94)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(66, 18)
        Me.btnBrowse.TabIndex = 9
        Me.btnBrowse.Text = "Browse"
        '
        'lblBOMStatus
        '
        Me.lblBOMStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBOMStatus.Location = New System.Drawing.Point(2, 72)
        Me.lblBOMStatus.Name = "lblBOMStatus"
        Me.lblBOMStatus.Size = New System.Drawing.Size(65, 16)
        Me.lblBOMStatus.TabIndex = 12
        Me.lblBOMStatus.Text = "Bom Status"
        '
        'txtDocPath
        '
        Me.txtDocPath.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocPath.Location = New System.Drawing.Point(648, 93)
        Me.txtDocPath.MaxLength = 49
        Me.txtDocPath.MendatroryField = False
        Me.txtDocPath.MyLinkLable1 = Me.lblDocument
        Me.txtDocPath.MyLinkLable2 = Nothing
        Me.txtDocPath.Name = "txtDocPath"
        Me.txtDocPath.Size = New System.Drawing.Size(173, 18)
        Me.txtDocPath.TabIndex = 6
        '
        'lblDocument
        '
        Me.lblDocument.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDocument.Location = New System.Drawing.Point(557, 94)
        Me.lblDocument.Name = "lblDocument"
        Me.lblDocument.Size = New System.Drawing.Size(58, 16)
        Me.lblDocument.TabIndex = 11
        Me.lblDocument.Text = "Document"
        '
        'cboBOMStatus
        '
        Me.cboBOMStatus.AllowShowFocusCues = False
        Me.cboBOMStatus.AutoCompleteDisplayMember = Nothing
        Me.cboBOMStatus.AutoCompleteValueMember = Nothing
        Me.cboBOMStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboBOMStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "Open"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Approved"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "On Hold"
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Text = "Discountinued"
        RadListDataItem4.TextWrap = True
        Me.cboBOMStatus.Items.Add(RadListDataItem1)
        Me.cboBOMStatus.Items.Add(RadListDataItem2)
        Me.cboBOMStatus.Items.Add(RadListDataItem3)
        Me.cboBOMStatus.Items.Add(RadListDataItem4)
        Me.cboBOMStatus.Location = New System.Drawing.Point(101, 72)
        Me.cboBOMStatus.MendatroryField = True
        Me.cboBOMStatus.MyLinkLable1 = Me.lblBOMStatus
        Me.cboBOMStatus.MyLinkLable2 = Nothing
        Me.cboBOMStatus.Name = "cboBOMStatus"
        Me.cboBOMStatus.Size = New System.Drawing.Size(143, 18)
        Me.cboBOMStatus.TabIndex = 3
        '
        'btnNewFile
        '
        Me.btnNewFile.Image = My.Resources._new
        Me.btnNewFile.Location = New System.Drawing.Point(824, 92)
        Me.btnNewFile.Name = "btnNewFile"
        Me.btnNewFile.Size = New System.Drawing.Size(14, 20)
        Me.btnNewFile.TabIndex = 10
        Me.btnNewFile.Text = " "
        '
        'pageOperations
        '
        Me.pageOperations.Controls.Add(Me.RadSplitContainer2)
        Me.pageOperations.Controls.Add(Me.RadPanel1)
        Me.pageOperations.ItemSize = New System.Drawing.SizeF(71.0!, 28.0!)
        Me.pageOperations.Location = New System.Drawing.Point(10, 37)
        Me.pageOperations.Name = "pageOperations"
        Me.pageOperations.Size = New System.Drawing.Size(945, 295)
        Me.pageOperations.Text = "Operations"
        '
        'RadSplitContainer2
        '
        Me.RadSplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadSplitContainer2.Controls.Add(Me.SplitPanel3)
        Me.RadSplitContainer2.Controls.Add(Me.SplitPanel4)
        Me.RadSplitContainer2.Location = New System.Drawing.Point(3, 177)
        Me.RadSplitContainer2.Name = "RadSplitContainer2"
        '
        '
        '
        Me.RadSplitContainer2.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.RadSplitContainer2.Size = New System.Drawing.Size(939, 116)
        Me.RadSplitContainer2.TabIndex = 7
        Me.RadSplitContainer2.TabStop = False
        Me.RadSplitContainer2.Text = "RadSplitContainer2"
        '
        'SplitPanel3
        '
        Me.SplitPanel3.Controls.Add(Me.gvResources)
        Me.SplitPanel3.Location = New System.Drawing.Point(0, 0)
        Me.SplitPanel3.Name = "SplitPanel3"
        '
        '
        '
        Me.SplitPanel3.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.SplitPanel3.Size = New System.Drawing.Size(468, 116)
        Me.SplitPanel3.TabIndex = 0
        Me.SplitPanel3.TabStop = False
        Me.SplitPanel3.Text = "SplitPanel3"
        '
        'gvResources
        '
        Me.gvResources.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvResources.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvResources.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvResources.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvResources.ForeColor = System.Drawing.Color.Black
        Me.gvResources.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvResources.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvResources.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvResources.MasterTemplate.AllowAddNewRow = False
        Me.gvResources.MasterTemplate.AutoGenerateColumns = False
        Me.gvResources.MasterTemplate.EnableGrouping = False
        Me.gvResources.MasterTemplate.ShowRowHeaderColumn = False
        Me.gvResources.Name = "gvResources"
        Me.gvResources.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvResources.Size = New System.Drawing.Size(468, 116)
        Me.gvResources.TabIndex = 6
        Me.gvResources.Text = "RadGridView2"
        '
        'SplitPanel4
        '
        Me.SplitPanel4.Controls.Add(Me.gvTools)
        Me.SplitPanel4.Location = New System.Drawing.Point(472, 0)
        Me.SplitPanel4.Name = "SplitPanel4"
        '
        '
        '
        Me.SplitPanel4.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.SplitPanel4.Size = New System.Drawing.Size(467, 116)
        Me.SplitPanel4.TabIndex = 1
        Me.SplitPanel4.TabStop = False
        Me.SplitPanel4.Text = "SplitPanel4"
        '
        'gvTools
        '
        Me.gvTools.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvTools.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvTools.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvTools.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvTools.ForeColor = System.Drawing.Color.Black
        Me.gvTools.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvTools.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvTools.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvTools.MasterTemplate.AllowAddNewRow = False
        Me.gvTools.MasterTemplate.AutoGenerateColumns = False
        Me.gvTools.MasterTemplate.EnableGrouping = False
        Me.gvTools.MasterTemplate.ShowRowHeaderColumn = False
        Me.gvTools.Name = "gvTools"
        Me.gvTools.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvTools.Size = New System.Drawing.Size(467, 116)
        Me.gvTools.TabIndex = 6
        Me.gvTools.Text = "RadGridView3"
        '
        'RadPanel1
        '
        Me.RadPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPanel1.Controls.Add(Me.gvOperations)
        Me.RadPanel1.Location = New System.Drawing.Point(3, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(939, 174)
        Me.RadPanel1.TabIndex = 6
        Me.RadPanel1.Text = "RadPanel1"
        '
        'gvOperations
        '
        Me.gvOperations.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvOperations.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvOperations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvOperations.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvOperations.ForeColor = System.Drawing.Color.Black
        Me.gvOperations.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvOperations.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvOperations.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvOperations.MasterTemplate.AllowAddNewRow = False
        Me.gvOperations.MasterTemplate.AutoGenerateColumns = False
        Me.gvOperations.MasterTemplate.EnableGrouping = False
        Me.gvOperations.MasterTemplate.EnableSorting = False
        Me.gvOperations.Name = "gvOperations"
        Me.gvOperations.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvOperations.Size = New System.Drawing.Size(939, 174)
        Me.gvOperations.TabIndex = 5
        Me.gvOperations.Text = "RadGridView1"
        '
        'pageComponent
        '
        Me.pageComponent.Controls.Add(Me.gvBOM)
        Me.pageComponent.ItemSize = New System.Drawing.SizeF(80.0!, 28.0!)
        Me.pageComponent.Location = New System.Drawing.Point(10, 37)
        Me.pageComponent.Name = "pageComponent"
        Me.pageComponent.Size = New System.Drawing.Size(945, 295)
        Me.pageComponent.Text = "Components"
        '
        'gvBOM
        '
        Me.gvBOM.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvBOM.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvBOM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvBOM.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvBOM.ForeColor = System.Drawing.Color.Black
        Me.gvBOM.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvBOM.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvBOM.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvBOM.MasterTemplate.AllowAddNewRow = False
        Me.gvBOM.MasterTemplate.AllowDragToGroup = False
        Me.gvBOM.MasterTemplate.AutoGenerateColumns = False
        Me.gvBOM.MasterTemplate.EnableFiltering = True
        Me.gvBOM.MasterTemplate.EnableGrouping = False
        Me.gvBOM.Name = "gvBOM"
        Me.gvBOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvBOM.Size = New System.Drawing.Size(945, 295)
        Me.gvBOM.TabIndex = 4
        Me.gvBOM.Text = "RadGridView1"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(228, 11)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(156, 11)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(8, 11)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(891, 11)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 22)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(82, 11)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(68, 22)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'frmRiceBOM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(986, 548)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "frmRiceBOM"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bill of Material With Costing"
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadSplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadSplitContainer1.ResumeLayout(False)
        CType(Me.SplitPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel1.ResumeLayout(False)
        Me.SplitPanel1.PerformLayout()
        CType(Me.txtadmin_amt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtprocess_amt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBomDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRevision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMasterItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMasterItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRevisionNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel2.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.pageGeneral.ResumeLayout(False)
        Me.pageGeneral.PerformLayout()
        CType(Me.RadPanel4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel4.ResumeLayout(False)
        CType(Me.gvCost.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpBOMDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApprovedByName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApprovedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreatedByName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreatedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMinBatchQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMinBatchSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnitName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBuildQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBuildQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocPath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboBOMStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNewFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageOperations.ResumeLayout(False)
        CType(Me.RadSplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadSplitContainer2.ResumeLayout(False)
        CType(Me.SplitPanel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel3.ResumeLayout(False)
        CType(Me.gvResources.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvResources, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitPanel4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel4.ResumeLayout(False)
        CType(Me.gvTools.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTools, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.gvOperations.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvOperations, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageComponent.ResumeLayout(False)
        CType(Me.gvBOM.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvBOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblRevision As common.Controls.MyLabel
    Friend WithEvents txtProducedItem As common.UserControls.txtFinder
    Friend WithEvents lblMasterItem As common.Controls.MyLabel
    Friend WithEvents lblBomDate As common.Controls.MyLabel
    Friend WithEvents dtpBOMDate As common.Controls.MyDateTimePicker
    Friend WithEvents gvBOM As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents lblMasterItemName As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpEndDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblEndDate As common.Controls.MyLabel
    Friend WithEvents dtpStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblStartDate As common.Controls.MyLabel
    Friend WithEvents lblBomDesc As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents cboBOMStatus As common.Controls.MyComboBox
    Friend WithEvents lblBOMStatus As common.Controls.MyLabel
    Friend WithEvents lblDocument As common.Controls.MyLabel
    Friend WithEvents btnBrowse As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDocPath As common.Controls.MyTextBox
    Friend WithEvents lblRevisionNo As common.Controls.MyLabel
    Friend WithEvents lblBuildQty As common.Controls.MyLabel
    Friend WithEvents txtBuildQty As common.MyNumBox
    Friend WithEvents lblUnitName As common.Controls.MyLabel
    Friend WithEvents txtMinBatchQty As common.MyNumBox
    Friend WithEvents lblMinBatchSize As common.Controls.MyLabel
    Friend WithEvents lblApprovedByName As common.Controls.MyLabel
    Friend WithEvents lblApprovedBy As common.Controls.MyLabel
    Friend WithEvents lblCreatedByName As common.Controls.MyLabel
    Friend WithEvents lblCreatedBy As common.Controls.MyLabel
    Friend WithEvents btnNewFile As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents pageGeneral As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pageOperations As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pageComponent As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadSplitContainer1 As Telerik.WinControls.UI.RadSplitContainer
    Friend WithEvents SplitPanel1 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents SplitPanel2 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents gvOperations As common.UserControls.MyRadGridView
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents gvTools As common.UserControls.MyRadGridView
    Friend WithEvents gvResources As common.UserControls.MyRadGridView
    Friend WithEvents RadPanel4 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents gvCost As common.UserControls.MyRadGridView
    Friend WithEvents RadSplitContainer2 As Telerik.WinControls.UI.RadSplitContainer
    Friend WithEvents SplitPanel3 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents SplitPanel4 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtadmin_amt As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtprocess_amt As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
End Class
