<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBillOfMaterialCosting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBillOfMaterialCosting))
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition5 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition6 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadSplitContainer1 = New Telerik.WinControls.UI.RadSplitContainer()
        Me.SplitPanel1 = New Telerik.WinControls.UI.SplitPanel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblApprovedByName = New common.Controls.MyLabel()
        Me.lblBomDate = New common.Controls.MyLabel()
        Me.lblApprovedBy = New common.Controls.MyLabel()
        Me.dtpBOMDate = New common.Controls.MyDateTimePicker()
        Me.lblCreatedByName = New common.Controls.MyLabel()
        Me.txtdrawingNo = New common.Controls.MyLabel()
        Me.lblCreatedBy = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblBomDesc = New common.Controls.MyLabel()
        Me.txtMinBatchQty = New common.MyNumBox()
        Me.lblMinBatchSize = New common.Controls.MyLabel()
        Me.lblUnitName = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtBuildQty = New common.MyNumBox()
        Me.lblBuildQty = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.btnBrowse = New Telerik.WinControls.UI.RadButton()
        Me.lblStartDate = New common.Controls.MyLabel()
        Me.txtDocPath = New common.Controls.MyTextBox()
        Me.lblDocument = New common.Controls.MyLabel()
        Me.dtpStartDate = New common.Controls.MyDateTimePicker()
        Me.btnNewFile = New Telerik.WinControls.UI.RadButton()
        Me.lblRevision = New common.Controls.MyLabel()
        Me.lblMasterItem = New common.Controls.MyLabel()
        Me.txtProducedItem = New common.UserControls.txtFinder()
        Me.lblMasterItemName = New common.Controls.MyLabel()
        Me.lblEndDate = New common.Controls.MyLabel()
        Me.lblRevisionNo = New common.Controls.MyLabel()
        Me.chkDefaultBOM = New Telerik.WinControls.UI.RadCheckBox()
        Me.dtpEndDate = New common.Controls.MyDateTimePicker()
        Me.cboBOMStatus = New common.Controls.MyComboBox()
        Me.lblBOMStatus = New common.Controls.MyLabel()
        Me.SplitPanel2 = New Telerik.WinControls.UI.SplitPanel()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.pageGeneral = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPanel4 = New Telerik.WinControls.UI.RadPanel()
        Me.gvCost = New common.UserControls.MyRadGridView()
        Me.pageOperations = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadSplitContainer2 = New Telerik.WinControls.UI.RadSplitContainer()
        Me.SplitPanel3 = New Telerik.WinControls.UI.SplitPanel()
        Me.gvResources = New common.UserControls.MyRadGridView()
        Me.SplitPanel4 = New Telerik.WinControls.UI.SplitPanel()
        Me.gvTools = New common.UserControls.MyRadGridView()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.gvOperations = New common.UserControls.MyRadGridView()
        Me.pageComponent = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvBOM = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGridView1 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv_tree = New Telerik.WinControls.UI.RadTreeView()
        Me.btnCopy = New Telerik.WinControls.UI.RadButton()
        Me.mnuImportBOM = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnImportBOMHead = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnImportBOMDetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuExportBOM = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExportBOMHead = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExportBOMDetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btntreeview = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnCC = New Telerik.WinControls.UI.RadButton()
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
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApprovedByName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApprovedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpBOMDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreatedByName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdrawingNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreatedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBomDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMinBatchQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMinBatchSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnitName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBuildQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBuildQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNewFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRevision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMasterItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMasterItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRevisionNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDefaultBOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboBOMStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel2.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.pageGeneral.SuspendLayout()
        CType(Me.RadPanel4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel4.SuspendLayout()
        CType(Me.gvCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCost.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv_tree, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mnuImportBOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mnuExportBOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btntreeview, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(108, 10)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 100
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(307, 22)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCode.Location = New System.Drawing.Point(10, 14)
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
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1054, 548)
        Me.RadGroupBox1.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(10, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadSplitContainer1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCopy)
        Me.SplitContainer1.Panel2.Controls.Add(Me.mnuImportBOM)
        Me.SplitContainer1.Panel2.Controls.Add(Me.mnuExportBOM)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btntreeview)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1034, 518)
        Me.SplitContainer1.SplitterDistance = 482
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
        Me.RadSplitContainer1.Size = New System.Drawing.Size(1034, 482)
        Me.RadSplitContainer1.TabIndex = 230
        Me.RadSplitContainer1.TabStop = False
        '
        'SplitPanel1
        '
        Me.SplitPanel1.Controls.Add(Me.btnCC)
        Me.SplitPanel1.Controls.Add(Me.lblLocation)
        Me.SplitPanel1.Controls.Add(Me.txtLocation)
        Me.SplitPanel1.Controls.Add(Me.RadLabel6)
        Me.SplitPanel1.Controls.Add(Me.MyLabel1)
        Me.SplitPanel1.Controls.Add(Me.lblApprovedByName)
        Me.SplitPanel1.Controls.Add(Me.lblBomDate)
        Me.SplitPanel1.Controls.Add(Me.lblApprovedBy)
        Me.SplitPanel1.Controls.Add(Me.dtpBOMDate)
        Me.SplitPanel1.Controls.Add(Me.lblCreatedByName)
        Me.SplitPanel1.Controls.Add(Me.txtdrawingNo)
        Me.SplitPanel1.Controls.Add(Me.lblCreatedBy)
        Me.SplitPanel1.Controls.Add(Me.txtDescription)
        Me.SplitPanel1.Controls.Add(Me.txtMinBatchQty)
        Me.SplitPanel1.Controls.Add(Me.lblCode)
        Me.SplitPanel1.Controls.Add(Me.lblMinBatchSize)
        Me.SplitPanel1.Controls.Add(Me.lblBomDesc)
        Me.SplitPanel1.Controls.Add(Me.lblUnitName)
        Me.SplitPanel1.Controls.Add(Me.UsLock1)
        Me.SplitPanel1.Controls.Add(Me.txtBuildQty)
        Me.SplitPanel1.Controls.Add(Me.btnNew)
        Me.SplitPanel1.Controls.Add(Me.lblBuildQty)
        Me.SplitPanel1.Controls.Add(Me.btnBrowse)
        Me.SplitPanel1.Controls.Add(Me.lblStartDate)
        Me.SplitPanel1.Controls.Add(Me.txtDocPath)
        Me.SplitPanel1.Controls.Add(Me.txtCode)
        Me.SplitPanel1.Controls.Add(Me.lblDocument)
        Me.SplitPanel1.Controls.Add(Me.dtpStartDate)
        Me.SplitPanel1.Controls.Add(Me.btnNewFile)
        Me.SplitPanel1.Controls.Add(Me.lblRevision)
        Me.SplitPanel1.Controls.Add(Me.lblMasterItem)
        Me.SplitPanel1.Controls.Add(Me.txtProducedItem)
        Me.SplitPanel1.Controls.Add(Me.lblMasterItemName)
        Me.SplitPanel1.Controls.Add(Me.lblEndDate)
        Me.SplitPanel1.Controls.Add(Me.lblRevisionNo)
        Me.SplitPanel1.Controls.Add(Me.chkDefaultBOM)
        Me.SplitPanel1.Controls.Add(Me.dtpEndDate)
        Me.SplitPanel1.Controls.Add(Me.cboBOMStatus)
        Me.SplitPanel1.Controls.Add(Me.lblBOMStatus)
        Me.SplitPanel1.Location = New System.Drawing.Point(0, 0)
        Me.SplitPanel1.Name = "SplitPanel1"
        '
        '
        '
        Me.SplitPanel1.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.SplitPanel1.Size = New System.Drawing.Size(1034, 130)
        Me.SplitPanel1.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(0!, -0.2280335!)
        Me.SplitPanel1.SizeInfo.SplitterCorrection = New System.Drawing.Size(0, -95)
        Me.SplitPanel1.TabIndex = 0
        Me.SplitPanel1.TabStop = False
        Me.SplitPanel1.Text = "SplitPanel1"
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(336, 56)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(211, 20)
        Me.lblLocation.TabIndex = 24
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
        Me.txtLocation.Location = New System.Drawing.Point(108, 57)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel6
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(218, 19)
        Me.txtLocation.TabIndex = 23
        Me.txtLocation.Value = ""
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(11, 57)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel6.TabIndex = 22
        Me.RadLabel6.Text = "Location"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(336, 102)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel1.TabIndex = 11
        Me.MyLabel1.Text = "Drawing No"
        Me.MyLabel1.Visible = False
        '
        'lblApprovedByName
        '
        Me.lblApprovedByName.AutoSize = False
        Me.lblApprovedByName.BorderVisible = True
        Me.lblApprovedByName.FieldName = Nothing
        Me.lblApprovedByName.Location = New System.Drawing.Point(815, 79)
        Me.lblApprovedByName.Name = "lblApprovedByName"
        Me.lblApprovedByName.Size = New System.Drawing.Size(208, 19)
        Me.lblApprovedByName.TabIndex = 20
        '
        'lblBomDate
        '
        Me.lblBomDate.FieldName = Nothing
        Me.lblBomDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBomDate.Location = New System.Drawing.Point(553, 36)
        Me.lblBomDate.Name = "lblBomDate"
        Me.lblBomDate.Size = New System.Drawing.Size(59, 16)
        Me.lblBomDate.TabIndex = 17
        Me.lblBomDate.Text = "BOM Date"
        '
        'lblApprovedBy
        '
        Me.lblApprovedBy.FieldName = Nothing
        Me.lblApprovedBy.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblApprovedBy.Location = New System.Drawing.Point(724, 79)
        Me.lblApprovedBy.Name = "lblApprovedBy"
        Me.lblApprovedBy.Size = New System.Drawing.Size(71, 16)
        Me.lblApprovedBy.TabIndex = 13
        Me.lblApprovedBy.Text = "Approved By"
        '
        'dtpBOMDate
        '
        Me.dtpBOMDate.CalculationExpression = Nothing
        Me.dtpBOMDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpBOMDate.FieldCode = Nothing
        Me.dtpBOMDate.FieldDesc = Nothing
        Me.dtpBOMDate.FieldMaxLength = 0
        Me.dtpBOMDate.FieldName = Nothing
        Me.dtpBOMDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpBOMDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBOMDate.isCalculatedField = False
        Me.dtpBOMDate.IsSourceFromTable = False
        Me.dtpBOMDate.IsSourceFromValueList = False
        Me.dtpBOMDate.IsUnique = False
        Me.dtpBOMDate.Location = New System.Drawing.Point(617, 34)
        Me.dtpBOMDate.MendatroryField = True
        Me.dtpBOMDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBOMDate.MyLinkLable1 = Me.lblBomDate
        Me.dtpBOMDate.MyLinkLable2 = Nothing
        Me.dtpBOMDate.Name = "dtpBOMDate"
        Me.dtpBOMDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBOMDate.ReferenceFieldDesc = Nothing
        Me.dtpBOMDate.ReferenceFieldName = Nothing
        Me.dtpBOMDate.ReferenceTableName = Nothing
        Me.dtpBOMDate.Size = New System.Drawing.Size(96, 18)
        Me.dtpBOMDate.TabIndex = 3
        Me.dtpBOMDate.TabStop = False
        Me.dtpBOMDate.Text = "03/05/2011"
        Me.dtpBOMDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblCreatedByName
        '
        Me.lblCreatedByName.AutoSize = False
        Me.lblCreatedByName.BorderVisible = True
        Me.lblCreatedByName.FieldName = Nothing
        Me.lblCreatedByName.Location = New System.Drawing.Point(815, 57)
        Me.lblCreatedByName.Name = "lblCreatedByName"
        Me.lblCreatedByName.Size = New System.Drawing.Size(208, 19)
        Me.lblCreatedByName.TabIndex = 21
        '
        'txtdrawingNo
        '
        Me.txtdrawingNo.AutoSize = False
        Me.txtdrawingNo.BorderVisible = True
        Me.txtdrawingNo.FieldName = Nothing
        Me.txtdrawingNo.Location = New System.Drawing.Point(410, 101)
        Me.txtdrawingNo.Name = "txtdrawingNo"
        Me.txtdrawingNo.Size = New System.Drawing.Size(137, 19)
        Me.txtdrawingNo.TabIndex = 12
        Me.txtdrawingNo.Visible = False
        '
        'lblCreatedBy
        '
        Me.lblCreatedBy.FieldName = Nothing
        Me.lblCreatedBy.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCreatedBy.Location = New System.Drawing.Point(724, 58)
        Me.lblCreatedBy.Name = "lblCreatedBy"
        Me.lblCreatedBy.Size = New System.Drawing.Size(63, 16)
        Me.lblCreatedBy.TabIndex = 16
        Me.lblCreatedBy.Text = "Created By"
        '
        'txtDescription
        '
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(108, 34)
        Me.txtDescription.MendatroryField = True
        Me.txtDescription.MyLinkLable1 = Me.lblBomDesc
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(439, 20)
        Me.txtDescription.TabIndex = 2
        '
        'lblBomDesc
        '
        Me.lblBomDesc.FieldName = Nothing
        Me.lblBomDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBomDesc.Location = New System.Drawing.Point(11, 36)
        Me.lblBomDesc.Name = "lblBomDesc"
        Me.lblBomDesc.Size = New System.Drawing.Size(63, 16)
        Me.lblBomDesc.TabIndex = 6
        Me.lblBomDesc.Text = "Description"
        '
        'txtMinBatchQty
        '
        Me.txtMinBatchQty.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMinBatchQty.CalculationExpression = Nothing
        Me.txtMinBatchQty.DecimalPlaces = 0
        Me.txtMinBatchQty.FieldCode = Nothing
        Me.txtMinBatchQty.FieldDesc = Nothing
        Me.txtMinBatchQty.FieldMaxLength = 0
        Me.txtMinBatchQty.FieldName = Nothing
        Me.txtMinBatchQty.isCalculatedField = False
        Me.txtMinBatchQty.IsSourceFromTable = False
        Me.txtMinBatchQty.IsSourceFromValueList = False
        Me.txtMinBatchQty.IsUnique = False
        Me.txtMinBatchQty.Location = New System.Drawing.Point(815, 32)
        Me.txtMinBatchQty.MendatroryField = True
        Me.txtMinBatchQty.MyLinkLable1 = Me.lblMinBatchSize
        Me.txtMinBatchQty.MyLinkLable2 = Nothing
        Me.txtMinBatchQty.Name = "txtMinBatchQty"
        Me.txtMinBatchQty.ReferenceFieldDesc = Nothing
        Me.txtMinBatchQty.ReferenceFieldName = Nothing
        Me.txtMinBatchQty.ReferenceTableName = Nothing
        Me.txtMinBatchQty.Size = New System.Drawing.Size(116, 20)
        Me.txtMinBatchQty.TabIndex = 4
        Me.txtMinBatchQty.Text = "0"
        Me.txtMinBatchQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMinBatchQty.Value = 0R
        '
        'lblMinBatchSize
        '
        Me.lblMinBatchSize.FieldName = Nothing
        Me.lblMinBatchSize.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblMinBatchSize.Location = New System.Drawing.Point(724, 34)
        Me.lblMinBatchSize.Name = "lblMinBatchSize"
        Me.lblMinBatchSize.Size = New System.Drawing.Size(85, 16)
        Me.lblMinBatchSize.TabIndex = 18
        Me.lblMinBatchSize.Text = "Min. Batch Size"
        '
        'lblUnitName
        '
        Me.lblUnitName.AutoSize = False
        Me.lblUnitName.BorderVisible = True
        Me.lblUnitName.FieldName = Nothing
        Me.lblUnitName.Location = New System.Drawing.Point(935, 11)
        Me.lblUnitName.Name = "lblUnitName"
        Me.lblUnitName.Size = New System.Drawing.Size(88, 19)
        Me.lblUnitName.TabIndex = 2
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(459, 11)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(88, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 8
        '
        'txtBuildQty
        '
        Me.txtBuildQty.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtBuildQty.CalculationExpression = Nothing
        Me.txtBuildQty.DecimalPlaces = 0
        Me.txtBuildQty.FieldCode = Nothing
        Me.txtBuildQty.FieldDesc = Nothing
        Me.txtBuildQty.FieldMaxLength = 0
        Me.txtBuildQty.FieldName = Nothing
        Me.txtBuildQty.isCalculatedField = False
        Me.txtBuildQty.IsSourceFromTable = False
        Me.txtBuildQty.IsSourceFromValueList = False
        Me.txtBuildQty.IsUnique = False
        Me.txtBuildQty.Location = New System.Drawing.Point(815, 10)
        Me.txtBuildQty.MendatroryField = True
        Me.txtBuildQty.MyLinkLable1 = Me.lblBuildQty
        Me.txtBuildQty.MyLinkLable2 = Nothing
        Me.txtBuildQty.Name = "txtBuildQty"
        Me.txtBuildQty.ReferenceFieldDesc = Nothing
        Me.txtBuildQty.ReferenceFieldName = Nothing
        Me.txtBuildQty.ReferenceTableName = Nothing
        Me.txtBuildQty.Size = New System.Drawing.Size(116, 20)
        Me.txtBuildQty.TabIndex = 1
        Me.txtBuildQty.Text = "0"
        Me.txtBuildQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBuildQty.Value = 0R
        '
        'lblBuildQty
        '
        Me.lblBuildQty.FieldName = Nothing
        Me.lblBuildQty.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblBuildQty.Location = New System.Drawing.Point(724, 12)
        Me.lblBuildQty.Name = "lblBuildQty"
        Me.lblBuildQty.Size = New System.Drawing.Size(77, 16)
        Me.lblBuildQty.TabIndex = 19
        Me.lblBuildQty.Text = "Build Quantity"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(415, 10)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(20, 22)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        Me.btnNew.TextAlignment = System.Drawing.ContentAlignment.TopRight
        '
        'btnBrowse
        '
        Me.btnBrowse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(951, 102)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(72, 18)
        Me.btnBrowse.TabIndex = 9
        Me.btnBrowse.Text = "Browse"
        '
        'lblStartDate
        '
        Me.lblStartDate.FieldName = Nothing
        Me.lblStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartDate.Location = New System.Drawing.Point(553, 59)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(57, 16)
        Me.lblStartDate.TabIndex = 15
        Me.lblStartDate.Text = "Start Date"
        '
        'txtDocPath
        '
        Me.txtDocPath.CalculationExpression = Nothing
        Me.txtDocPath.FieldCode = Nothing
        Me.txtDocPath.FieldDesc = Nothing
        Me.txtDocPath.FieldMaxLength = 0
        Me.txtDocPath.FieldName = Nothing
        Me.txtDocPath.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocPath.isCalculatedField = False
        Me.txtDocPath.IsSourceFromTable = False
        Me.txtDocPath.IsSourceFromValueList = False
        Me.txtDocPath.IsUnique = False
        Me.txtDocPath.Location = New System.Drawing.Point(815, 101)
        Me.txtDocPath.MaxLength = 49
        Me.txtDocPath.MendatroryField = False
        Me.txtDocPath.MyLinkLable1 = Me.lblDocument
        Me.txtDocPath.MyLinkLable2 = Nothing
        Me.txtDocPath.Name = "txtDocPath"
        Me.txtDocPath.ReferenceFieldDesc = Nothing
        Me.txtDocPath.ReferenceFieldName = Nothing
        Me.txtDocPath.ReferenceTableName = Nothing
        Me.txtDocPath.Size = New System.Drawing.Size(116, 18)
        Me.txtDocPath.TabIndex = 8
        '
        'lblDocument
        '
        Me.lblDocument.FieldName = Nothing
        Me.lblDocument.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDocument.Location = New System.Drawing.Point(724, 103)
        Me.lblDocument.Name = "lblDocument"
        Me.lblDocument.Size = New System.Drawing.Size(58, 16)
        Me.lblDocument.TabIndex = 11
        Me.lblDocument.Text = "Document"
        '
        'dtpStartDate
        '
        Me.dtpStartDate.CalculationExpression = Nothing
        Me.dtpStartDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpStartDate.FieldCode = Nothing
        Me.dtpStartDate.FieldDesc = Nothing
        Me.dtpStartDate.FieldMaxLength = 0
        Me.dtpStartDate.FieldName = Nothing
        Me.dtpStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDate.isCalculatedField = False
        Me.dtpStartDate.IsSourceFromTable = False
        Me.dtpStartDate.IsSourceFromValueList = False
        Me.dtpStartDate.IsUnique = False
        Me.dtpStartDate.Location = New System.Drawing.Point(618, 57)
        Me.dtpStartDate.MendatroryField = False
        Me.dtpStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartDate.MyLinkLable1 = Me.lblStartDate
        Me.dtpStartDate.MyLinkLable2 = Nothing
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartDate.ReferenceFieldDesc = Nothing
        Me.dtpStartDate.ReferenceFieldName = Nothing
        Me.dtpStartDate.ReferenceTableName = Nothing
        Me.dtpStartDate.Size = New System.Drawing.Size(96, 18)
        Me.dtpStartDate.TabIndex = 5
        Me.dtpStartDate.TabStop = False
        Me.dtpStartDate.Text = "03/05/2011"
        Me.dtpStartDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'btnNewFile
        '
        Me.btnNewFile.Location = New System.Drawing.Point(935, 101)
        Me.btnNewFile.Name = "btnNewFile"
        Me.btnNewFile.Size = New System.Drawing.Size(14, 20)
        Me.btnNewFile.TabIndex = 10
        Me.btnNewFile.Text = " "
        '
        'lblRevision
        '
        Me.lblRevision.FieldName = Nothing
        Me.lblRevision.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRevision.Location = New System.Drawing.Point(11, 101)
        Me.lblRevision.Name = "lblRevision"
        Me.lblRevision.Size = New System.Drawing.Size(67, 16)
        Me.lblRevision.TabIndex = 4
        Me.lblRevision.Text = "Revision No"
        '
        'lblMasterItem
        '
        Me.lblMasterItem.FieldName = Nothing
        Me.lblMasterItem.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMasterItem.Location = New System.Drawing.Point(10, 80)
        Me.lblMasterItem.Name = "lblMasterItem"
        Me.lblMasterItem.Size = New System.Drawing.Size(57, 18)
        Me.lblMasterItem.TabIndex = 5
        Me.lblMasterItem.Text = "Main Item"
        '
        'txtProducedItem
        '
        Me.txtProducedItem.CalculationExpression = Nothing
        Me.txtProducedItem.FieldCode = Nothing
        Me.txtProducedItem.FieldDesc = Nothing
        Me.txtProducedItem.FieldMaxLength = 0
        Me.txtProducedItem.FieldName = Nothing
        Me.txtProducedItem.isCalculatedField = False
        Me.txtProducedItem.IsSourceFromTable = False
        Me.txtProducedItem.IsSourceFromValueList = False
        Me.txtProducedItem.IsUnique = False
        Me.txtProducedItem.Location = New System.Drawing.Point(108, 79)
        Me.txtProducedItem.MendatroryField = True
        Me.txtProducedItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProducedItem.MyLinkLable1 = Me.lblMasterItem
        Me.txtProducedItem.MyLinkLable2 = Nothing
        Me.txtProducedItem.MyReadOnly = False
        Me.txtProducedItem.MyShowMasterFormButton = False
        Me.txtProducedItem.Name = "txtProducedItem"
        Me.txtProducedItem.ReferenceFieldDesc = Nothing
        Me.txtProducedItem.ReferenceFieldName = Nothing
        Me.txtProducedItem.ReferenceTableName = Nothing
        Me.txtProducedItem.Size = New System.Drawing.Size(218, 19)
        Me.txtProducedItem.TabIndex = 3
        Me.txtProducedItem.Value = ""
        '
        'lblMasterItemName
        '
        Me.lblMasterItemName.AutoSize = False
        Me.lblMasterItemName.BorderVisible = True
        Me.lblMasterItemName.FieldName = Nothing
        Me.lblMasterItemName.Location = New System.Drawing.Point(336, 79)
        Me.lblMasterItemName.Name = "lblMasterItemName"
        Me.lblMasterItemName.Size = New System.Drawing.Size(211, 19)
        Me.lblMasterItemName.TabIndex = 10
        '
        'lblEndDate
        '
        Me.lblEndDate.FieldName = Nothing
        Me.lblEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEndDate.Location = New System.Drawing.Point(553, 81)
        Me.lblEndDate.Name = "lblEndDate"
        Me.lblEndDate.Size = New System.Drawing.Size(53, 16)
        Me.lblEndDate.TabIndex = 14
        Me.lblEndDate.Text = "End Date"
        '
        'lblRevisionNo
        '
        Me.lblRevisionNo.AutoSize = False
        Me.lblRevisionNo.BorderVisible = True
        Me.lblRevisionNo.FieldName = Nothing
        Me.lblRevisionNo.Location = New System.Drawing.Point(108, 101)
        Me.lblRevisionNo.Name = "lblRevisionNo"
        Me.lblRevisionNo.Size = New System.Drawing.Size(218, 19)
        Me.lblRevisionNo.TabIndex = 9
        '
        'chkDefaultBOM
        '
        Me.chkDefaultBOM.Location = New System.Drawing.Point(617, 10)
        Me.chkDefaultBOM.Name = "chkDefaultBOM"
        Me.chkDefaultBOM.Size = New System.Drawing.Size(96, 18)
        Me.chkDefaultBOM.TabIndex = 0
        Me.chkDefaultBOM.Text = "Is Default BOM"
        '
        'dtpEndDate
        '
        Me.dtpEndDate.CalculationExpression = Nothing
        Me.dtpEndDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpEndDate.FieldCode = Nothing
        Me.dtpEndDate.FieldDesc = Nothing
        Me.dtpEndDate.FieldMaxLength = 0
        Me.dtpEndDate.FieldName = Nothing
        Me.dtpEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDate.isCalculatedField = False
        Me.dtpEndDate.IsSourceFromTable = False
        Me.dtpEndDate.IsSourceFromValueList = False
        Me.dtpEndDate.IsUnique = False
        Me.dtpEndDate.Location = New System.Drawing.Point(617, 79)
        Me.dtpEndDate.MendatroryField = False
        Me.dtpEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndDate.MyLinkLable1 = Me.lblEndDate
        Me.dtpEndDate.MyLinkLable2 = Nothing
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndDate.ReferenceFieldDesc = Nothing
        Me.dtpEndDate.ReferenceFieldName = Nothing
        Me.dtpEndDate.ReferenceTableName = Nothing
        Me.dtpEndDate.ShowCheckBox = True
        Me.dtpEndDate.Size = New System.Drawing.Size(96, 18)
        Me.dtpEndDate.TabIndex = 6
        Me.dtpEndDate.TabStop = False
        Me.dtpEndDate.Text = "03/05/2011"
        Me.dtpEndDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'cboBOMStatus
        '
        Me.cboBOMStatus.AutoCompleteDisplayMember = Nothing
        Me.cboBOMStatus.AutoCompleteValueMember = Nothing
        Me.cboBOMStatus.CalculationExpression = Nothing
        Me.cboBOMStatus.DropDownAnimationEnabled = True
        Me.cboBOMStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboBOMStatus.FieldCode = Nothing
        Me.cboBOMStatus.FieldDesc = Nothing
        Me.cboBOMStatus.FieldMaxLength = 0
        Me.cboBOMStatus.FieldName = Nothing
        Me.cboBOMStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBOMStatus.isCalculatedField = False
        Me.cboBOMStatus.IsSourceFromTable = False
        Me.cboBOMStatus.IsSourceFromValueList = False
        Me.cboBOMStatus.IsUnique = False
        RadListDataItem1.Text = "Open"
        RadListDataItem2.Text = "Approved"
        RadListDataItem3.Text = "On Hold"
        RadListDataItem4.Text = "Discountinued"
        Me.cboBOMStatus.Items.Add(RadListDataItem1)
        Me.cboBOMStatus.Items.Add(RadListDataItem2)
        Me.cboBOMStatus.Items.Add(RadListDataItem3)
        Me.cboBOMStatus.Items.Add(RadListDataItem4)
        Me.cboBOMStatus.Location = New System.Drawing.Point(618, 103)
        Me.cboBOMStatus.MendatroryField = True
        Me.cboBOMStatus.MyLinkLable1 = Me.lblBOMStatus
        Me.cboBOMStatus.MyLinkLable2 = Nothing
        Me.cboBOMStatus.Name = "cboBOMStatus"
        Me.cboBOMStatus.ReferenceFieldDesc = Nothing
        Me.cboBOMStatus.ReferenceFieldName = Nothing
        Me.cboBOMStatus.ReferenceTableName = Nothing
        Me.cboBOMStatus.Size = New System.Drawing.Size(96, 18)
        Me.cboBOMStatus.TabIndex = 7
        '
        'lblBOMStatus
        '
        Me.lblBOMStatus.FieldName = Nothing
        Me.lblBOMStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBOMStatus.Location = New System.Drawing.Point(553, 103)
        Me.lblBOMStatus.Name = "lblBOMStatus"
        Me.lblBOMStatus.Size = New System.Drawing.Size(65, 16)
        Me.lblBOMStatus.TabIndex = 12
        Me.lblBOMStatus.Text = "Bom Status"
        '
        'SplitPanel2
        '
        Me.SplitPanel2.Controls.Add(Me.RadPageView1)
        Me.SplitPanel2.Location = New System.Drawing.Point(0, 134)
        Me.SplitPanel2.Name = "SplitPanel2"
        '
        '
        '
        Me.SplitPanel2.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.SplitPanel2.Size = New System.Drawing.Size(1034, 348)
        Me.SplitPanel2.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(0!, 0.2280335!)
        Me.SplitPanel2.SizeInfo.SplitterCorrection = New System.Drawing.Size(0, 95)
        Me.SplitPanel2.TabIndex = 1
        Me.SplitPanel2.TabStop = False
        Me.SplitPanel2.Text = "SplitPanel2"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.pageGeneral)
        Me.RadPageView1.Controls.Add(Me.pageOperations)
        Me.RadPageView1.Controls.Add(Me.pageComponent)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.pageGeneral
        Me.RadPageView1.Size = New System.Drawing.Size(1034, 348)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'pageGeneral
        '
        Me.pageGeneral.Controls.Add(Me.RadPanel4)
        Me.pageGeneral.ItemSize = New System.Drawing.SizeF(55.0!, 28.0!)
        Me.pageGeneral.Location = New System.Drawing.Point(10, 37)
        Me.pageGeneral.Name = "pageGeneral"
        Me.pageGeneral.Size = New System.Drawing.Size(1013, 300)
        Me.pageGeneral.Text = "General"
        '
        'RadPanel4
        '
        Me.RadPanel4.Controls.Add(Me.gvCost)
        Me.RadPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel4.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel4.Name = "RadPanel4"
        Me.RadPanel4.Size = New System.Drawing.Size(1013, 300)
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
        '
        '
        Me.gvCost.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvCost.MasterTemplate.AllowAddNewRow = False
        Me.gvCost.MasterTemplate.AllowColumnReorder = False
        Me.gvCost.MasterTemplate.AutoGenerateColumns = False
        Me.gvCost.MasterTemplate.EnableGrouping = False
        Me.gvCost.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvCost.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCost.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvCost.Name = "gvCost"
        Me.gvCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvCost.ShowHeaderCellButtons = True
        Me.gvCost.Size = New System.Drawing.Size(1013, 300)
        Me.gvCost.TabIndex = 0
        Me.gvCost.TabStop = False
        '
        'pageOperations
        '
        Me.pageOperations.Controls.Add(Me.RadSplitContainer2)
        Me.pageOperations.Controls.Add(Me.RadPanel1)
        Me.pageOperations.ItemSize = New System.Drawing.SizeF(71.0!, 28.0!)
        Me.pageOperations.Location = New System.Drawing.Point(10, 37)
        Me.pageOperations.Name = "pageOperations"
        Me.pageOperations.Size = New System.Drawing.Size(1013, 300)
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
        Me.RadSplitContainer2.Size = New System.Drawing.Size(1007, 121)
        Me.RadSplitContainer2.TabIndex = 7
        Me.RadSplitContainer2.TabStop = False
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
        Me.SplitPanel3.Size = New System.Drawing.Size(502, 121)
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
        Me.gvResources.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvResources.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvResources.MasterTemplate.ShowRowHeaderColumn = False
        Me.gvResources.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gvResources.Name = "gvResources"
        Me.gvResources.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvResources.ShowHeaderCellButtons = True
        Me.gvResources.Size = New System.Drawing.Size(502, 121)
        Me.gvResources.TabIndex = 6
        '
        'SplitPanel4
        '
        Me.SplitPanel4.Controls.Add(Me.gvTools)
        Me.SplitPanel4.Location = New System.Drawing.Point(506, 0)
        Me.SplitPanel4.Name = "SplitPanel4"
        '
        '
        '
        Me.SplitPanel4.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.SplitPanel4.Size = New System.Drawing.Size(501, 121)
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
        Me.gvTools.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvTools.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvTools.MasterTemplate.ShowRowHeaderColumn = False
        Me.gvTools.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gvTools.Name = "gvTools"
        Me.gvTools.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvTools.ShowHeaderCellButtons = True
        Me.gvTools.Size = New System.Drawing.Size(501, 121)
        Me.gvTools.TabIndex = 6
        '
        'RadPanel1
        '
        Me.RadPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPanel1.Controls.Add(Me.gvOperations)
        Me.RadPanel1.Location = New System.Drawing.Point(3, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1007, 174)
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
        Me.gvOperations.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvOperations.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvOperations.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gvOperations.Name = "gvOperations"
        Me.gvOperations.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvOperations.ShowHeaderCellButtons = True
        Me.gvOperations.Size = New System.Drawing.Size(1007, 174)
        Me.gvOperations.TabIndex = 5
        '
        'pageComponent
        '
        Me.pageComponent.Controls.Add(Me.gvBOM)
        Me.pageComponent.ItemSize = New System.Drawing.SizeF(80.0!, 28.0!)
        Me.pageComponent.Location = New System.Drawing.Point(10, 37)
        Me.pageComponent.Name = "pageComponent"
        Me.pageComponent.Size = New System.Drawing.Size(1013, 300)
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
        Me.gvBOM.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvBOM.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvBOM.MasterTemplate.ViewDefinition = TableViewDefinition5
        Me.gvBOM.Name = "gvBOM"
        Me.gvBOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvBOM.ShowHeaderCellButtons = True
        Me.gvBOM.Size = New System.Drawing.Size(1013, 300)
        Me.gvBOM.TabIndex = 4
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGridView1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(65.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1013, 300)
        Me.RadPageViewPage1.Text = "Tree View"
        '
        'RadGridView1
        '
        Me.RadGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGridView1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.RadGridView1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.RadGridView1.MasterTemplate.ShowHeaderCellButtons = True
        Me.RadGridView1.MasterTemplate.ViewDefinition = TableViewDefinition6
        Me.RadGridView1.Name = "RadGridView1"
        Me.RadGridView1.ShowHeaderCellButtons = True
        Me.RadGridView1.Size = New System.Drawing.Size(1013, 300)
        Me.RadGridView1.TabIndex = 0
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv_tree)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(65.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1013, 300)
        Me.RadPageViewPage2.Text = "Tree View"
        '
        'gv_tree
        '
        Me.gv_tree.AllowPlusMinusAnimation = True
        Me.gv_tree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv_tree.Location = New System.Drawing.Point(0, 0)
        Me.gv_tree.Name = "gv_tree"
        Me.gv_tree.Size = New System.Drawing.Size(1013, 300)
        Me.gv_tree.SpacingBetweenNodes = 1
        Me.gv_tree.TabIndex = 0
        '
        'btnCopy
        '
        Me.btnCopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCopy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCopy.Location = New System.Drawing.Point(724, 5)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(69, 22)
        Me.btnCopy.TabIndex = 10
        Me.btnCopy.Text = "Copy BOM"
        '
        'mnuImportBOM
        '
        Me.mnuImportBOM.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.mnuImportBOM.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnImportBOMHead, Me.btnImportBOMDetail})
        Me.mnuImportBOM.Location = New System.Drawing.Point(611, 5)
        Me.mnuImportBOM.Name = "mnuImportBOM"
        Me.mnuImportBOM.Size = New System.Drawing.Size(110, 22)
        Me.mnuImportBOM.TabIndex = 9
        Me.mnuImportBOM.Text = "Import BOM"
        '
        'btnImportBOMHead
        '
        Me.btnImportBOMHead.Name = "btnImportBOMHead"
        Me.btnImportBOMHead.Text = "BOM Head"
        '
        'btnImportBOMDetail
        '
        Me.btnImportBOMDetail.Name = "btnImportBOMDetail"
        Me.btnImportBOMDetail.Text = "BOM Detail"
        '
        'mnuExportBOM
        '
        Me.mnuExportBOM.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.mnuExportBOM.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExportBOMHead, Me.btnExportBOMDetail})
        Me.mnuExportBOM.Location = New System.Drawing.Point(495, 5)
        Me.mnuExportBOM.Name = "mnuExportBOM"
        Me.mnuExportBOM.Size = New System.Drawing.Size(110, 22)
        Me.mnuExportBOM.TabIndex = 8
        Me.mnuExportBOM.Text = "Export BOM"
        '
        'btnExportBOMHead
        '
        Me.btnExportBOMHead.Name = "btnExportBOMHead"
        Me.btnExportBOMHead.Text = "BOM Head"
        '
        'btnExportBOMDetail
        '
        Me.btnExportBOMDetail.Name = "btnExportBOMDetail"
        Me.btnExportBOMDetail.Text = "BOM Detail"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadSplitButton1.Location = New System.Drawing.Point(379, 5)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(110, 22)
        Me.RadSplitButton1.TabIndex = 7
        Me.RadSplitButton1.Text = "Export Tree View"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Export Treeview"
        '
        'btntreeview
        '
        Me.btntreeview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btntreeview.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntreeview.Location = New System.Drawing.Point(304, 5)
        Me.btntreeview.Name = "btntreeview"
        Me.btntreeview.Size = New System.Drawing.Size(69, 22)
        Me.btntreeview.TabIndex = 5
        Me.btntreeview.Text = "Tree View"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(229, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(157, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(958, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 22)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(83, 5)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(68, 22)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnCC
        '
        Me.btnCC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCC.Location = New System.Drawing.Point(435, 10)
        Me.btnCC.Name = "btnCC"
        Me.btnCC.Size = New System.Drawing.Size(21, 22)
        Me.btnCC.TabIndex = 1516
        Me.btnCC.Text = "cc"
        '
        'frmBillOfMaterialCosting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1054, 548)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "frmBillOfMaterialCosting"
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
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApprovedByName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApprovedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpBOMDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreatedByName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdrawingNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreatedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBomDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMinBatchQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMinBatchSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnitName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBuildQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBuildQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocPath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNewFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRevision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMasterItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMasterItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRevisionNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDefaultBOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboBOMStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel2.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.pageGeneral.ResumeLayout(False)
        CType(Me.RadPanel4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel4.ResumeLayout(False)
        CType(Me.gvCost.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCost, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv_tree, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCopy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mnuImportBOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mnuExportBOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btntreeview, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCC, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents chkDefaultBOM As Telerik.WinControls.UI.RadCheckBox
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
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btntreeview As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGridView1 As common.UserControls.MyRadGridView
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv_tree As Telerik.WinControls.UI.RadTreeView
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtdrawingNo As common.Controls.MyLabel
    Friend WithEvents mnuExportBOM As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents mnuImportBOM As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnExportBOMHead As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExportBOMDetail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnImportBOMHead As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnImportBOMDetail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnCopy As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents btnCC As RadButton
End Class
