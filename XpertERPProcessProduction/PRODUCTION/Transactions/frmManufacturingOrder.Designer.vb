<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManufacturingOrder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmManufacturingOrder))
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition5 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadSplitContainer1 = New Telerik.WinControls.UI.RadSplitContainer()
        Me.SplitPanel1 = New Telerik.WinControls.UI.SplitPanel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtParentMOCode = New common.Controls.MyLabel()
        Me.txtSODesc = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.fndSONo = New common.UserControls.txtFinder()
        Me.txtProductionPlanDesc = New common.Controls.MyLabel()
        Me.lblSourceDocumentCode = New common.Controls.MyLabel()
        Me.fndProductionPlan = New common.UserControls.txtFinder()
        Me.cboSourceDocType = New common.Controls.MyComboBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtMOReference = New common.Controls.MyTextBox()
        Me.lblMOReference = New common.Controls.MyLabel()
        Me.fndOrderQtyUOM = New common.UserControls.txtFinder()
        Me.lblMasterItem = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblMODesc = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtProducedItem = New common.UserControls.txtFinder()
        Me.txtOrderedQtyStockUnit = New common.MyNumBox()
        Me.lblMinBatchSize = New common.Controls.MyLabel()
        Me.txtMasterItemName = New common.Controls.MyLabel()
        Me.cboMOStatus = New common.Controls.MyComboBox()
        Me.lblMOStatus = New common.Controls.MyLabel()
        Me.txtOrderedQty = New common.MyNumBox()
        Me.lblQtyOrdered = New common.Controls.MyLabel()
        Me.lblUnitName = New common.Controls.MyLabel()
        Me.SplitPanel2 = New Telerik.WinControls.UI.SplitPanel()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.pageGeneral = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.lblProductionLine = New common.Controls.MyLabel()
        Me.fndProductionLine = New common.UserControls.txtFinder()
        Me.lblClosedate = New common.Controls.MyLabel()
        Me.dtpCloseDate = New common.Controls.MyDateTimePicker()
        Me.lblClosedByCode = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtComments = New common.Controls.MyTextBox()
        Me.lblComments = New common.Controls.MyLabel()
        Me.lblReleasedDate = New common.Controls.MyLabel()
        Me.dtpReleasedDate = New common.Controls.MyDateTimePicker()
        Me.lblApprovedDate = New common.Controls.MyLabel()
        Me.dtpApprovedDate = New common.Controls.MyDateTimePicker()
        Me.lblReleasedByCode = New common.Controls.MyLabel()
        Me.lblReleasedBy = New common.Controls.MyLabel()
        Me.lblCreatedDate = New common.Controls.MyLabel()
        Me.dtpCreatedDate = New common.Controls.MyDateTimePicker()
        Me.lblActualStartSate = New common.Controls.MyLabel()
        Me.dtpActualStartDate = New common.Controls.MyDateTimePicker()
        Me.lblActualEndDate = New common.Controls.MyLabel()
        Me.dtpActualEndDate = New common.Controls.MyDateTimePicker()
        Me.lblIncharge = New common.Controls.MyLabel()
        Me.fndInCharge = New common.UserControls.txtFinder()
        Me.lblPlanner = New common.Controls.MyLabel()
        Me.fndPlanner = New common.UserControls.txtFinder()
        Me.lblDueDate = New common.Controls.MyLabel()
        Me.dtpDueDate = New common.Controls.MyDateTimePicker()
        Me.lblBOMCode = New common.Controls.MyLabel()
        Me.fndBomCode = New common.UserControls.txtFinder()
        Me.lblOrderDate = New common.Controls.MyLabel()
        Me.dtpMODate = New common.Controls.MyDateTimePicker()
        Me.lblApprovedByName = New common.Controls.MyLabel()
        Me.lblApprovedBy = New common.Controls.MyLabel()
        Me.lblCreatedByName = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblCreatedBy = New common.Controls.MyLabel()
        Me.lblPlanStartDate = New common.Controls.MyLabel()
        Me.dtpPlanStartDate = New common.Controls.MyDateTimePicker()
        Me.lblRevisionNo = New common.Controls.MyLabel()
        Me.lblEndDate = New common.Controls.MyLabel()
        Me.dtpPlanEndDate = New common.Controls.MyDateTimePicker()
        Me.btnBrowse = New Telerik.WinControls.UI.RadButton()
        Me.txtDocPath = New common.Controls.MyTextBox()
        Me.lblDocument = New common.Controls.MyLabel()
        Me.btnNewFile = New Telerik.WinControls.UI.RadButton()
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
        Me.pageTotal = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPanel4 = New Telerik.WinControls.UI.RadPanel()
        Me.gvCost = New common.UserControls.MyRadGridView()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadSplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadSplitContainer1.SuspendLayout()
        CType(Me.SplitPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel1.SuspendLayout()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtParentMOCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSODesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProductionPlanDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSourceDocumentCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboSourceDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMOReference, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMOReference, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMasterItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMODesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOrderedQtyStockUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMinBatchSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMasterItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboMOStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMOStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOrderedQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQtyOrdered, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnitName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel2.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.pageGeneral.SuspendLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProductionLine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblClosedate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpCloseDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblClosedByCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblComments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReleasedDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpReleasedDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApprovedDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpApprovedDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReleasedByCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReleasedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreatedDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpCreatedDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblActualStartSate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpActualStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblActualEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpActualEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIncharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPlanner, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDueDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDueDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBOMCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOrderDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpMODate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApprovedByName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApprovedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreatedByName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreatedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPlanStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpPlanStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRevisionNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpPlanEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocument, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pageTotal.SuspendLayout()
        CType(Me.RadPanel4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel4.SuspendLayout()
        CType(Me.gvCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCost.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(142, 10)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(322, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCode.Location = New System.Drawing.Point(10, 14)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(56, 16)
        Me.lblCode.TabIndex = 17
        Me.lblCode.Text = "MO Code"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadSplitContainer1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(1160, 600)
        Me.SplitContainer1.SplitterDistance = 562
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
        Me.RadSplitContainer1.Size = New System.Drawing.Size(1160, 562)
        Me.RadSplitContainer1.TabIndex = 230
        Me.RadSplitContainer1.TabStop = False
        '
        'SplitPanel1
        '
        Me.SplitPanel1.Controls.Add(Me.MyLabel8)
        Me.SplitPanel1.Controls.Add(Me.txtParentMOCode)
        Me.SplitPanel1.Controls.Add(Me.txtSODesc)
        Me.SplitPanel1.Controls.Add(Me.MyLabel7)
        Me.SplitPanel1.Controls.Add(Me.fndSONo)
        Me.SplitPanel1.Controls.Add(Me.txtProductionPlanDesc)
        Me.SplitPanel1.Controls.Add(Me.lblSourceDocumentCode)
        Me.SplitPanel1.Controls.Add(Me.fndProductionPlan)
        Me.SplitPanel1.Controls.Add(Me.cboSourceDocType)
        Me.SplitPanel1.Controls.Add(Me.MyLabel3)
        Me.SplitPanel1.Controls.Add(Me.txtMOReference)
        Me.SplitPanel1.Controls.Add(Me.lblMOReference)
        Me.SplitPanel1.Controls.Add(Me.fndOrderQtyUOM)
        Me.SplitPanel1.Controls.Add(Me.txtDescription)
        Me.SplitPanel1.Controls.Add(Me.lblCode)
        Me.SplitPanel1.Controls.Add(Me.lblMODesc)
        Me.SplitPanel1.Controls.Add(Me.UsLock1)
        Me.SplitPanel1.Controls.Add(Me.btnNew)
        Me.SplitPanel1.Controls.Add(Me.txtCode)
        Me.SplitPanel1.Controls.Add(Me.lblMasterItem)
        Me.SplitPanel1.Controls.Add(Me.txtProducedItem)
        Me.SplitPanel1.Controls.Add(Me.txtOrderedQtyStockUnit)
        Me.SplitPanel1.Controls.Add(Me.txtMasterItemName)
        Me.SplitPanel1.Controls.Add(Me.cboMOStatus)
        Me.SplitPanel1.Controls.Add(Me.lblMOStatus)
        Me.SplitPanel1.Controls.Add(Me.lblMinBatchSize)
        Me.SplitPanel1.Controls.Add(Me.txtOrderedQty)
        Me.SplitPanel1.Controls.Add(Me.lblQtyOrdered)
        Me.SplitPanel1.Controls.Add(Me.lblUnitName)
        Me.SplitPanel1.Location = New System.Drawing.Point(0, 0)
        Me.SplitPanel1.Name = "SplitPanel1"
        '
        '
        '
        Me.SplitPanel1.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.SplitPanel1.Size = New System.Drawing.Size(1160, 217)
        Me.SplitPanel1.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(0!, -0.1111111!)
        Me.SplitPanel1.SizeInfo.SplitterCorrection = New System.Drawing.Size(0, -43)
        Me.SplitPanel1.TabIndex = 0
        Me.SplitPanel1.TabStop = False
        Me.SplitPanel1.Text = "SplitPanel1"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(383, 36)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel8.TabIndex = 282
        Me.MyLabel8.Text = "Parent MO Code"
        '
        'txtParentMOCode
        '
        Me.txtParentMOCode.AutoSize = False
        Me.txtParentMOCode.BorderVisible = True
        Me.txtParentMOCode.FieldName = Nothing
        Me.txtParentMOCode.Location = New System.Drawing.Point(482, 34)
        Me.txtParentMOCode.Name = "txtParentMOCode"
        Me.txtParentMOCode.Size = New System.Drawing.Size(251, 19)
        Me.txtParentMOCode.TabIndex = 281
        '
        'txtSODesc
        '
        Me.txtSODesc.AutoSize = False
        Me.txtSODesc.BorderVisible = True
        Me.txtSODesc.FieldName = Nothing
        Me.txtSODesc.Location = New System.Drawing.Point(365, 75)
        Me.txtSODesc.Name = "txtSODesc"
        Me.txtSODesc.Size = New System.Drawing.Size(368, 19)
        Me.txtSODesc.TabIndex = 280
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(11, 78)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(40, 16)
        Me.MyLabel7.TabIndex = 279
        Me.MyLabel7.Text = "SO No"
        '
        'fndSONo
        '
        Me.fndSONo.CalculationExpression = Nothing
        Me.fndSONo.FieldCode = Nothing
        Me.fndSONo.FieldDesc = Nothing
        Me.fndSONo.FieldMaxLength = 0
        Me.fndSONo.FieldName = Nothing
        Me.fndSONo.isCalculatedField = False
        Me.fndSONo.IsSourceFromTable = False
        Me.fndSONo.IsSourceFromValueList = False
        Me.fndSONo.IsUnique = False
        Me.fndSONo.Location = New System.Drawing.Point(141, 75)
        Me.fndSONo.MendatroryField = False
        Me.fndSONo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSONo.MyLinkLable1 = Me.MyLabel7
        Me.fndSONo.MyLinkLable2 = Nothing
        Me.fndSONo.MyReadOnly = False
        Me.fndSONo.MyShowMasterFormButton = False
        Me.fndSONo.Name = "fndSONo"
        Me.fndSONo.ReferenceFieldDesc = Nothing
        Me.fndSONo.ReferenceFieldName = Nothing
        Me.fndSONo.ReferenceTableName = Nothing
        Me.fndSONo.Size = New System.Drawing.Size(219, 19)
        Me.fndSONo.TabIndex = 6
        Me.fndSONo.Value = ""
        '
        'txtProductionPlanDesc
        '
        Me.txtProductionPlanDesc.AutoSize = False
        Me.txtProductionPlanDesc.BorderVisible = True
        Me.txtProductionPlanDesc.FieldName = Nothing
        Me.txtProductionPlanDesc.Location = New System.Drawing.Point(366, 54)
        Me.txtProductionPlanDesc.Name = "txtProductionPlanDesc"
        Me.txtProductionPlanDesc.Size = New System.Drawing.Size(367, 19)
        Me.txtProductionPlanDesc.TabIndex = 277
        '
        'lblSourceDocumentCode
        '
        Me.lblSourceDocumentCode.FieldName = Nothing
        Me.lblSourceDocumentCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSourceDocumentCode.Location = New System.Drawing.Point(11, 57)
        Me.lblSourceDocumentCode.Name = "lblSourceDocumentCode"
        Me.lblSourceDocumentCode.Size = New System.Drawing.Size(86, 16)
        Me.lblSourceDocumentCode.TabIndex = 276
        Me.lblSourceDocumentCode.Text = "Production Plan"
        '
        'fndProductionPlan
        '
        Me.fndProductionPlan.CalculationExpression = Nothing
        Me.fndProductionPlan.FieldCode = Nothing
        Me.fndProductionPlan.FieldDesc = Nothing
        Me.fndProductionPlan.FieldMaxLength = 0
        Me.fndProductionPlan.FieldName = Nothing
        Me.fndProductionPlan.isCalculatedField = False
        Me.fndProductionPlan.IsSourceFromTable = False
        Me.fndProductionPlan.IsSourceFromValueList = False
        Me.fndProductionPlan.IsUnique = False
        Me.fndProductionPlan.Location = New System.Drawing.Point(142, 54)
        Me.fndProductionPlan.MendatroryField = False
        Me.fndProductionPlan.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndProductionPlan.MyLinkLable1 = Me.lblSourceDocumentCode
        Me.fndProductionPlan.MyLinkLable2 = Nothing
        Me.fndProductionPlan.MyReadOnly = False
        Me.fndProductionPlan.MyShowMasterFormButton = False
        Me.fndProductionPlan.Name = "fndProductionPlan"
        Me.fndProductionPlan.ReferenceFieldDesc = Nothing
        Me.fndProductionPlan.ReferenceFieldName = Nothing
        Me.fndProductionPlan.ReferenceTableName = Nothing
        Me.fndProductionPlan.Size = New System.Drawing.Size(219, 19)
        Me.fndProductionPlan.TabIndex = 5
        Me.fndProductionPlan.Value = ""
        '
        'cboSourceDocType
        '
        Me.cboSourceDocType.AutoCompleteDisplayMember = Nothing
        Me.cboSourceDocType.AutoCompleteValueMember = Nothing
        Me.cboSourceDocType.CalculationExpression = Nothing
        Me.cboSourceDocType.DropDownAnimationEnabled = True
        Me.cboSourceDocType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboSourceDocType.FieldCode = Nothing
        Me.cboSourceDocType.FieldDesc = Nothing
        Me.cboSourceDocType.FieldMaxLength = 0
        Me.cboSourceDocType.FieldName = Nothing
        Me.cboSourceDocType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSourceDocType.isCalculatedField = False
        Me.cboSourceDocType.IsSourceFromTable = False
        Me.cboSourceDocType.IsSourceFromValueList = False
        Me.cboSourceDocType.IsUnique = False
        Me.cboSourceDocType.Location = New System.Drawing.Point(142, 34)
        Me.cboSourceDocType.MendatroryField = True
        Me.cboSourceDocType.MyLinkLable1 = Me.MyLabel3
        Me.cboSourceDocType.MyLinkLable2 = Nothing
        Me.cboSourceDocType.Name = "cboSourceDocType"
        Me.cboSourceDocType.ReferenceFieldDesc = Nothing
        Me.cboSourceDocType.ReferenceFieldName = Nothing
        Me.cboSourceDocType.ReferenceTableName = Nothing
        Me.cboSourceDocType.Size = New System.Drawing.Size(219, 18)
        Me.cboSourceDocType.TabIndex = 4
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(10, 34)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel3.TabIndex = 20
        Me.MyLabel3.Text = "Source Doc Type"
        '
        'txtMOReference
        '
        Me.txtMOReference.CalculationExpression = Nothing
        Me.txtMOReference.FieldCode = Nothing
        Me.txtMOReference.FieldDesc = Nothing
        Me.txtMOReference.FieldMaxLength = 0
        Me.txtMOReference.FieldName = Nothing
        Me.txtMOReference.isCalculatedField = False
        Me.txtMOReference.IsSourceFromTable = False
        Me.txtMOReference.IsSourceFromValueList = False
        Me.txtMOReference.IsUnique = False
        Me.txtMOReference.Location = New System.Drawing.Point(142, 166)
        Me.txtMOReference.MaxLength = 50
        Me.txtMOReference.MendatroryField = True
        Me.txtMOReference.MyLinkLable1 = Me.lblMOReference
        Me.txtMOReference.MyLinkLable2 = Nothing
        Me.txtMOReference.Name = "txtMOReference"
        Me.txtMOReference.ReferenceFieldDesc = Nothing
        Me.txtMOReference.ReferenceFieldName = Nothing
        Me.txtMOReference.ReferenceTableName = Nothing
        Me.txtMOReference.Size = New System.Drawing.Size(591, 20)
        Me.txtMOReference.TabIndex = 11
        '
        'lblMOReference
        '
        Me.lblMOReference.FieldName = Nothing
        Me.lblMOReference.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMOReference.Location = New System.Drawing.Point(14, 168)
        Me.lblMOReference.Name = "lblMOReference"
        Me.lblMOReference.Size = New System.Drawing.Size(80, 16)
        Me.lblMOReference.TabIndex = 11
        Me.lblMOReference.Text = "MO Reference"
        '
        'fndOrderQtyUOM
        '
        Me.fndOrderQtyUOM.CalculationExpression = Nothing
        Me.fndOrderQtyUOM.FieldCode = Nothing
        Me.fndOrderQtyUOM.FieldDesc = Nothing
        Me.fndOrderQtyUOM.FieldMaxLength = 0
        Me.fndOrderQtyUOM.FieldName = Nothing
        Me.fndOrderQtyUOM.isCalculatedField = False
        Me.fndOrderQtyUOM.IsSourceFromTable = False
        Me.fndOrderQtyUOM.IsSourceFromValueList = False
        Me.fndOrderQtyUOM.IsUnique = False
        Me.fndOrderQtyUOM.Location = New System.Drawing.Point(277, 119)
        Me.fndOrderQtyUOM.MendatroryField = True
        Me.fndOrderQtyUOM.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndOrderQtyUOM.MyLinkLable1 = Me.lblMasterItem
        Me.fndOrderQtyUOM.MyLinkLable2 = Nothing
        Me.fndOrderQtyUOM.MyReadOnly = False
        Me.fndOrderQtyUOM.MyShowMasterFormButton = False
        Me.fndOrderQtyUOM.Name = "fndOrderQtyUOM"
        Me.fndOrderQtyUOM.ReferenceFieldDesc = Nothing
        Me.fndOrderQtyUOM.ReferenceFieldName = Nothing
        Me.fndOrderQtyUOM.ReferenceTableName = Nothing
        Me.fndOrderQtyUOM.Size = New System.Drawing.Size(187, 19)
        Me.fndOrderQtyUOM.TabIndex = 9
        Me.fndOrderQtyUOM.Value = ""
        '
        'lblMasterItem
        '
        Me.lblMasterItem.FieldName = Nothing
        Me.lblMasterItem.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMasterItem.Location = New System.Drawing.Point(10, 98)
        Me.lblMasterItem.Name = "lblMasterItem"
        Me.lblMasterItem.Size = New System.Drawing.Size(57, 18)
        Me.lblMasterItem.TabIndex = 15
        Me.lblMasterItem.Text = "Main Item"
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
        Me.txtDescription.Location = New System.Drawing.Point(142, 190)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.MendatroryField = True
        Me.txtDescription.MyLinkLable1 = Me.lblMODesc
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(591, 20)
        Me.txtDescription.TabIndex = 12
        '
        'lblMODesc
        '
        Me.lblMODesc.FieldName = Nothing
        Me.lblMODesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMODesc.Location = New System.Drawing.Point(14, 192)
        Me.lblMODesc.Name = "lblMODesc"
        Me.lblMODesc.Size = New System.Drawing.Size(63, 16)
        Me.lblMODesc.TabIndex = 10
        Me.lblMODesc.Text = "Description"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(857, 8)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 2
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(465, 11)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
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
        Me.txtProducedItem.Location = New System.Drawing.Point(142, 97)
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
        Me.txtProducedItem.Size = New System.Drawing.Size(219, 19)
        Me.txtProducedItem.TabIndex = 7
        Me.txtProducedItem.Value = ""
        '
        'txtOrderedQtyStockUnit
        '
        Me.txtOrderedQtyStockUnit.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtOrderedQtyStockUnit.CalculationExpression = Nothing
        Me.txtOrderedQtyStockUnit.DecimalPlaces = 0
        Me.txtOrderedQtyStockUnit.FieldCode = Nothing
        Me.txtOrderedQtyStockUnit.FieldDesc = Nothing
        Me.txtOrderedQtyStockUnit.FieldMaxLength = 0
        Me.txtOrderedQtyStockUnit.FieldName = Nothing
        Me.txtOrderedQtyStockUnit.isCalculatedField = False
        Me.txtOrderedQtyStockUnit.IsSourceFromTable = False
        Me.txtOrderedQtyStockUnit.IsSourceFromValueList = False
        Me.txtOrderedQtyStockUnit.IsUnique = False
        Me.txtOrderedQtyStockUnit.Location = New System.Drawing.Point(142, 142)
        Me.txtOrderedQtyStockUnit.MendatroryField = True
        Me.txtOrderedQtyStockUnit.MyLinkLable1 = Me.lblMinBatchSize
        Me.txtOrderedQtyStockUnit.MyLinkLable2 = Nothing
        Me.txtOrderedQtyStockUnit.Name = "txtOrderedQtyStockUnit"
        Me.txtOrderedQtyStockUnit.ReadOnly = True
        Me.txtOrderedQtyStockUnit.ReferenceFieldDesc = Nothing
        Me.txtOrderedQtyStockUnit.ReferenceFieldName = Nothing
        Me.txtOrderedQtyStockUnit.ReferenceTableName = Nothing
        Me.txtOrderedQtyStockUnit.Size = New System.Drawing.Size(129, 20)
        Me.txtOrderedQtyStockUnit.TabIndex = 10
        Me.txtOrderedQtyStockUnit.Text = "0"
        Me.txtOrderedQtyStockUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtOrderedQtyStockUnit.Value = 0R
        '
        'lblMinBatchSize
        '
        Me.lblMinBatchSize.FieldName = Nothing
        Me.lblMinBatchSize.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblMinBatchSize.Location = New System.Drawing.Point(11, 144)
        Me.lblMinBatchSize.Name = "lblMinBatchSize"
        Me.lblMinBatchSize.Size = New System.Drawing.Size(128, 16)
        Me.lblMinBatchSize.TabIndex = 12
        Me.lblMinBatchSize.Text = "Qty Ordered(Stock Unit)"
        '
        'txtMasterItemName
        '
        Me.txtMasterItemName.AutoSize = False
        Me.txtMasterItemName.BorderVisible = True
        Me.txtMasterItemName.FieldName = Nothing
        Me.txtMasterItemName.Location = New System.Drawing.Point(366, 97)
        Me.txtMasterItemName.Name = "txtMasterItemName"
        Me.txtMasterItemName.Size = New System.Drawing.Size(367, 19)
        Me.txtMasterItemName.TabIndex = 16
        '
        'cboMOStatus
        '
        Me.cboMOStatus.AutoCompleteDisplayMember = Nothing
        Me.cboMOStatus.AutoCompleteValueMember = Nothing
        Me.cboMOStatus.CalculationExpression = Nothing
        Me.cboMOStatus.DropDownAnimationEnabled = True
        Me.cboMOStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboMOStatus.FieldCode = Nothing
        Me.cboMOStatus.FieldDesc = Nothing
        Me.cboMOStatus.FieldMaxLength = 0
        Me.cboMOStatus.FieldName = Nothing
        Me.cboMOStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMOStatus.isCalculatedField = False
        Me.cboMOStatus.IsSourceFromTable = False
        Me.cboMOStatus.IsSourceFromValueList = False
        Me.cboMOStatus.IsUnique = False
        Me.cboMOStatus.Location = New System.Drawing.Point(548, 10)
        Me.cboMOStatus.MendatroryField = True
        Me.cboMOStatus.MyLinkLable1 = Me.lblMOStatus
        Me.cboMOStatus.MyLinkLable2 = Nothing
        Me.cboMOStatus.Name = "cboMOStatus"
        Me.cboMOStatus.ReferenceFieldDesc = Nothing
        Me.cboMOStatus.ReferenceFieldName = Nothing
        Me.cboMOStatus.ReferenceTableName = Nothing
        Me.cboMOStatus.Size = New System.Drawing.Size(185, 18)
        Me.cboMOStatus.TabIndex = 3
        '
        'lblMOStatus
        '
        Me.lblMOStatus.FieldName = Nothing
        Me.lblMOStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMOStatus.Location = New System.Drawing.Point(489, 12)
        Me.lblMOStatus.Name = "lblMOStatus"
        Me.lblMOStatus.Size = New System.Drawing.Size(60, 16)
        Me.lblMOStatus.TabIndex = 18
        Me.lblMOStatus.Text = "MO Status"
        '
        'txtOrderedQty
        '
        Me.txtOrderedQty.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtOrderedQty.CalculationExpression = Nothing
        Me.txtOrderedQty.DecimalPlaces = 0
        Me.txtOrderedQty.FieldCode = Nothing
        Me.txtOrderedQty.FieldDesc = Nothing
        Me.txtOrderedQty.FieldMaxLength = 0
        Me.txtOrderedQty.FieldName = Nothing
        Me.txtOrderedQty.isCalculatedField = False
        Me.txtOrderedQty.IsSourceFromTable = False
        Me.txtOrderedQty.IsSourceFromValueList = False
        Me.txtOrderedQty.IsUnique = False
        Me.txtOrderedQty.Location = New System.Drawing.Point(142, 119)
        Me.txtOrderedQty.MendatroryField = True
        Me.txtOrderedQty.MyLinkLable1 = Me.lblQtyOrdered
        Me.txtOrderedQty.MyLinkLable2 = Nothing
        Me.txtOrderedQty.Name = "txtOrderedQty"
        Me.txtOrderedQty.ReferenceFieldDesc = Nothing
        Me.txtOrderedQty.ReferenceFieldName = Nothing
        Me.txtOrderedQty.ReferenceTableName = Nothing
        Me.txtOrderedQty.Size = New System.Drawing.Size(129, 20)
        Me.txtOrderedQty.TabIndex = 8
        Me.txtOrderedQty.Text = "0"
        Me.txtOrderedQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtOrderedQty.Value = 0R
        '
        'lblQtyOrdered
        '
        Me.lblQtyOrdered.FieldName = Nothing
        Me.lblQtyOrdered.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblQtyOrdered.Location = New System.Drawing.Point(10, 122)
        Me.lblQtyOrdered.Name = "lblQtyOrdered"
        Me.lblQtyOrdered.Size = New System.Drawing.Size(93, 16)
        Me.lblQtyOrdered.TabIndex = 14
        Me.lblQtyOrdered.Text = "Quantity Ordered"
        '
        'lblUnitName
        '
        Me.lblUnitName.AutoSize = False
        Me.lblUnitName.BorderVisible = True
        Me.lblUnitName.FieldName = Nothing
        Me.lblUnitName.Location = New System.Drawing.Point(277, 141)
        Me.lblUnitName.Name = "lblUnitName"
        Me.lblUnitName.Size = New System.Drawing.Size(187, 19)
        Me.lblUnitName.TabIndex = 13
        '
        'SplitPanel2
        '
        Me.SplitPanel2.Controls.Add(Me.RadPageView1)
        Me.SplitPanel2.Location = New System.Drawing.Point(0, 221)
        Me.SplitPanel2.Name = "SplitPanel2"
        '
        '
        '
        Me.SplitPanel2.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.SplitPanel2.Size = New System.Drawing.Size(1160, 341)
        Me.SplitPanel2.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(0!, 0.1111111!)
        Me.SplitPanel2.SizeInfo.SplitterCorrection = New System.Drawing.Size(0, 43)
        Me.SplitPanel2.TabIndex = 1
        Me.SplitPanel2.TabStop = False
        Me.SplitPanel2.Text = "SplitPanel2"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.pageGeneral)
        Me.RadPageView1.Controls.Add(Me.pageOperations)
        Me.RadPageView1.Controls.Add(Me.pageComponent)
        Me.RadPageView1.Controls.Add(Me.pageTotal)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.pageGeneral
        Me.RadPageView1.Size = New System.Drawing.Size(1160, 341)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'pageGeneral
        '
        Me.pageGeneral.Controls.Add(Me.MyLabel5)
        Me.pageGeneral.Controls.Add(Me.lblLocationDesc)
        Me.pageGeneral.Controls.Add(Me.MyLabel2)
        Me.pageGeneral.Controls.Add(Me.fndLocation)
        Me.pageGeneral.Controls.Add(Me.lblProductionLine)
        Me.pageGeneral.Controls.Add(Me.fndProductionLine)
        Me.pageGeneral.Controls.Add(Me.lblClosedate)
        Me.pageGeneral.Controls.Add(Me.dtpCloseDate)
        Me.pageGeneral.Controls.Add(Me.lblClosedByCode)
        Me.pageGeneral.Controls.Add(Me.MyLabel4)
        Me.pageGeneral.Controls.Add(Me.txtComments)
        Me.pageGeneral.Controls.Add(Me.lblComments)
        Me.pageGeneral.Controls.Add(Me.lblReleasedDate)
        Me.pageGeneral.Controls.Add(Me.dtpReleasedDate)
        Me.pageGeneral.Controls.Add(Me.lblApprovedDate)
        Me.pageGeneral.Controls.Add(Me.dtpApprovedDate)
        Me.pageGeneral.Controls.Add(Me.lblReleasedByCode)
        Me.pageGeneral.Controls.Add(Me.lblReleasedBy)
        Me.pageGeneral.Controls.Add(Me.lblCreatedDate)
        Me.pageGeneral.Controls.Add(Me.dtpCreatedDate)
        Me.pageGeneral.Controls.Add(Me.lblActualStartSate)
        Me.pageGeneral.Controls.Add(Me.dtpActualStartDate)
        Me.pageGeneral.Controls.Add(Me.lblActualEndDate)
        Me.pageGeneral.Controls.Add(Me.dtpActualEndDate)
        Me.pageGeneral.Controls.Add(Me.lblIncharge)
        Me.pageGeneral.Controls.Add(Me.fndInCharge)
        Me.pageGeneral.Controls.Add(Me.lblPlanner)
        Me.pageGeneral.Controls.Add(Me.fndPlanner)
        Me.pageGeneral.Controls.Add(Me.lblDueDate)
        Me.pageGeneral.Controls.Add(Me.dtpDueDate)
        Me.pageGeneral.Controls.Add(Me.lblBOMCode)
        Me.pageGeneral.Controls.Add(Me.fndBomCode)
        Me.pageGeneral.Controls.Add(Me.lblOrderDate)
        Me.pageGeneral.Controls.Add(Me.dtpMODate)
        Me.pageGeneral.Controls.Add(Me.lblApprovedByName)
        Me.pageGeneral.Controls.Add(Me.lblApprovedBy)
        Me.pageGeneral.Controls.Add(Me.lblCreatedByName)
        Me.pageGeneral.Controls.Add(Me.MyLabel1)
        Me.pageGeneral.Controls.Add(Me.lblCreatedBy)
        Me.pageGeneral.Controls.Add(Me.lblPlanStartDate)
        Me.pageGeneral.Controls.Add(Me.dtpPlanStartDate)
        Me.pageGeneral.Controls.Add(Me.lblRevisionNo)
        Me.pageGeneral.Controls.Add(Me.lblEndDate)
        Me.pageGeneral.Controls.Add(Me.dtpPlanEndDate)
        Me.pageGeneral.Controls.Add(Me.btnBrowse)
        Me.pageGeneral.Controls.Add(Me.txtDocPath)
        Me.pageGeneral.Controls.Add(Me.lblDocument)
        Me.pageGeneral.Controls.Add(Me.btnNewFile)
        Me.pageGeneral.ItemSize = New System.Drawing.SizeF(55.0!, 28.0!)
        Me.pageGeneral.Location = New System.Drawing.Point(10, 37)
        Me.pageGeneral.Name = "pageGeneral"
        Me.pageGeneral.Size = New System.Drawing.Size(1139, 293)
        Me.pageGeneral.Text = "General"
        '
        'MyLabel5
        '
        Me.MyLabel5.AutoSize = False
        Me.MyLabel5.BorderVisible = True
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(355, 208)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(260, 19)
        Me.MyLabel5.TabIndex = 47
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.FieldName = Nothing
        Me.lblLocationDesc.Location = New System.Drawing.Point(355, 185)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(260, 19)
        Me.lblLocationDesc.TabIndex = 46
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(7, 185)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(78, 18)
        Me.MyLabel2.TabIndex = 45
        Me.MyLabel2.Text = "Location Code"
        '
        'fndLocation
        '
        Me.fndLocation.CalculationExpression = Nothing
        Me.fndLocation.FieldCode = Nothing
        Me.fndLocation.FieldDesc = Nothing
        Me.fndLocation.FieldMaxLength = 0
        Me.fndLocation.FieldName = Nothing
        Me.fndLocation.isCalculatedField = False
        Me.fndLocation.IsSourceFromTable = False
        Me.fndLocation.IsSourceFromValueList = False
        Me.fndLocation.IsUnique = False
        Me.fndLocation.Location = New System.Drawing.Point(130, 185)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.MyLabel2
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(219, 19)
        Me.fndLocation.TabIndex = 15
        Me.fndLocation.Value = ""
        '
        'lblProductionLine
        '
        Me.lblProductionLine.FieldName = Nothing
        Me.lblProductionLine.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblProductionLine.Location = New System.Drawing.Point(6, 208)
        Me.lblProductionLine.Name = "lblProductionLine"
        Me.lblProductionLine.Size = New System.Drawing.Size(85, 18)
        Me.lblProductionLine.TabIndex = 18
        Me.lblProductionLine.Text = "Production Line"
        '
        'fndProductionLine
        '
        Me.fndProductionLine.CalculationExpression = Nothing
        Me.fndProductionLine.FieldCode = Nothing
        Me.fndProductionLine.FieldDesc = Nothing
        Me.fndProductionLine.FieldMaxLength = 0
        Me.fndProductionLine.FieldName = Nothing
        Me.fndProductionLine.isCalculatedField = False
        Me.fndProductionLine.IsSourceFromTable = False
        Me.fndProductionLine.IsSourceFromValueList = False
        Me.fndProductionLine.IsUnique = False
        Me.fndProductionLine.Location = New System.Drawing.Point(129, 208)
        Me.fndProductionLine.MendatroryField = False
        Me.fndProductionLine.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndProductionLine.MyLinkLable1 = Me.lblProductionLine
        Me.fndProductionLine.MyLinkLable2 = Nothing
        Me.fndProductionLine.MyReadOnly = False
        Me.fndProductionLine.MyShowMasterFormButton = False
        Me.fndProductionLine.Name = "fndProductionLine"
        Me.fndProductionLine.ReferenceFieldDesc = Nothing
        Me.fndProductionLine.ReferenceFieldName = Nothing
        Me.fndProductionLine.ReferenceTableName = Nothing
        Me.fndProductionLine.Size = New System.Drawing.Size(219, 19)
        Me.fndProductionLine.TabIndex = 17
        Me.fndProductionLine.Value = ""
        '
        'lblClosedate
        '
        Me.lblClosedate.FieldName = Nothing
        Me.lblClosedate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClosedate.Location = New System.Drawing.Point(632, 119)
        Me.lblClosedate.Name = "lblClosedate"
        Me.lblClosedate.Size = New System.Drawing.Size(69, 16)
        Me.lblClosedate.TabIndex = 24
        Me.lblClosedate.Text = "Closed Date"
        '
        'dtpCloseDate
        '
        Me.dtpCloseDate.CalculationExpression = Nothing
        Me.dtpCloseDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpCloseDate.Enabled = False
        Me.dtpCloseDate.FieldCode = Nothing
        Me.dtpCloseDate.FieldDesc = Nothing
        Me.dtpCloseDate.FieldMaxLength = 0
        Me.dtpCloseDate.FieldName = Nothing
        Me.dtpCloseDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpCloseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpCloseDate.isCalculatedField = False
        Me.dtpCloseDate.IsSourceFromTable = False
        Me.dtpCloseDate.IsSourceFromValueList = False
        Me.dtpCloseDate.IsUnique = False
        Me.dtpCloseDate.Location = New System.Drawing.Point(731, 117)
        Me.dtpCloseDate.MendatroryField = False
        Me.dtpCloseDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpCloseDate.MyLinkLable1 = Me.lblClosedate
        Me.dtpCloseDate.MyLinkLable2 = Nothing
        Me.dtpCloseDate.Name = "dtpCloseDate"
        Me.dtpCloseDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpCloseDate.ReferenceFieldDesc = Nothing
        Me.dtpCloseDate.ReferenceFieldName = Nothing
        Me.dtpCloseDate.ReferenceTableName = Nothing
        Me.dtpCloseDate.ShowCheckBox = True
        Me.dtpCloseDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpCloseDate.TabIndex = 12
        Me.dtpCloseDate.TabStop = False
        Me.dtpCloseDate.Text = "03/05/2011"
        Me.dtpCloseDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblClosedByCode
        '
        Me.lblClosedByCode.AutoSize = False
        Me.lblClosedByCode.BorderVisible = True
        Me.lblClosedByCode.FieldName = Nothing
        Me.lblClosedByCode.Location = New System.Drawing.Point(473, 116)
        Me.lblClosedByCode.Name = "lblClosedByCode"
        Me.lblClosedByCode.Size = New System.Drawing.Size(142, 19)
        Me.lblClosedByCode.TabIndex = 23
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(373, 119)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel4.TabIndex = 22
        Me.MyLabel4.Text = "Closed By"
        '
        'txtComments
        '
        Me.txtComments.CalculationExpression = Nothing
        Me.txtComments.FieldCode = Nothing
        Me.txtComments.FieldDesc = Nothing
        Me.txtComments.FieldMaxLength = 0
        Me.txtComments.FieldName = Nothing
        Me.txtComments.isCalculatedField = False
        Me.txtComments.IsSourceFromTable = False
        Me.txtComments.IsSourceFromValueList = False
        Me.txtComments.IsUnique = False
        Me.txtComments.Location = New System.Drawing.Point(131, 138)
        Me.txtComments.MaxLength = 50
        Me.txtComments.MendatroryField = False
        Me.txtComments.MyLinkLable1 = Me.lblComments
        Me.txtComments.MyLinkLable2 = Nothing
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ReferenceFieldDesc = Nothing
        Me.txtComments.ReferenceFieldName = Nothing
        Me.txtComments.ReferenceTableName = Nothing
        Me.txtComments.Size = New System.Drawing.Size(744, 20)
        Me.txtComments.TabIndex = 13
        '
        'lblComments
        '
        Me.lblComments.FieldName = Nothing
        Me.lblComments.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComments.Location = New System.Drawing.Point(6, 140)
        Me.lblComments.Name = "lblComments"
        Me.lblComments.Size = New System.Drawing.Size(61, 16)
        Me.lblComments.TabIndex = 20
        Me.lblComments.Text = "Comments"
        '
        'lblReleasedDate
        '
        Me.lblReleasedDate.FieldName = Nothing
        Me.lblReleasedDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReleasedDate.Location = New System.Drawing.Point(631, 97)
        Me.lblReleasedDate.Name = "lblReleasedDate"
        Me.lblReleasedDate.Size = New System.Drawing.Size(81, 16)
        Me.lblReleasedDate.TabIndex = 28
        Me.lblReleasedDate.Text = "Released Date"
        '
        'dtpReleasedDate
        '
        Me.dtpReleasedDate.CalculationExpression = Nothing
        Me.dtpReleasedDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpReleasedDate.Enabled = False
        Me.dtpReleasedDate.FieldCode = Nothing
        Me.dtpReleasedDate.FieldDesc = Nothing
        Me.dtpReleasedDate.FieldMaxLength = 0
        Me.dtpReleasedDate.FieldName = Nothing
        Me.dtpReleasedDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpReleasedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpReleasedDate.isCalculatedField = False
        Me.dtpReleasedDate.IsSourceFromTable = False
        Me.dtpReleasedDate.IsSourceFromValueList = False
        Me.dtpReleasedDate.IsUnique = False
        Me.dtpReleasedDate.Location = New System.Drawing.Point(730, 95)
        Me.dtpReleasedDate.MendatroryField = False
        Me.dtpReleasedDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpReleasedDate.MyLinkLable1 = Me.lblReleasedDate
        Me.dtpReleasedDate.MyLinkLable2 = Nothing
        Me.dtpReleasedDate.Name = "dtpReleasedDate"
        Me.dtpReleasedDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpReleasedDate.ReferenceFieldDesc = Nothing
        Me.dtpReleasedDate.ReferenceFieldName = Nothing
        Me.dtpReleasedDate.ReferenceTableName = Nothing
        Me.dtpReleasedDate.ShowCheckBox = True
        Me.dtpReleasedDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpReleasedDate.TabIndex = 10
        Me.dtpReleasedDate.TabStop = False
        Me.dtpReleasedDate.Text = "03/05/2011"
        Me.dtpReleasedDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblApprovedDate
        '
        Me.lblApprovedDate.FieldName = Nothing
        Me.lblApprovedDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApprovedDate.Location = New System.Drawing.Point(631, 76)
        Me.lblApprovedDate.Name = "lblApprovedDate"
        Me.lblApprovedDate.Size = New System.Drawing.Size(82, 16)
        Me.lblApprovedDate.TabIndex = 32
        Me.lblApprovedDate.Text = "Approved Date"
        '
        'dtpApprovedDate
        '
        Me.dtpApprovedDate.CalculationExpression = Nothing
        Me.dtpApprovedDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpApprovedDate.Enabled = False
        Me.dtpApprovedDate.FieldCode = Nothing
        Me.dtpApprovedDate.FieldDesc = Nothing
        Me.dtpApprovedDate.FieldMaxLength = 0
        Me.dtpApprovedDate.FieldName = Nothing
        Me.dtpApprovedDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpApprovedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpApprovedDate.isCalculatedField = False
        Me.dtpApprovedDate.IsSourceFromTable = False
        Me.dtpApprovedDate.IsSourceFromValueList = False
        Me.dtpApprovedDate.IsUnique = False
        Me.dtpApprovedDate.Location = New System.Drawing.Point(730, 74)
        Me.dtpApprovedDate.MendatroryField = False
        Me.dtpApprovedDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpApprovedDate.MyLinkLable1 = Me.lblApprovedDate
        Me.dtpApprovedDate.MyLinkLable2 = Nothing
        Me.dtpApprovedDate.Name = "dtpApprovedDate"
        Me.dtpApprovedDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpApprovedDate.ReferenceFieldDesc = Nothing
        Me.dtpApprovedDate.ReferenceFieldName = Nothing
        Me.dtpApprovedDate.ReferenceTableName = Nothing
        Me.dtpApprovedDate.ShowCheckBox = True
        Me.dtpApprovedDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpApprovedDate.TabIndex = 8
        Me.dtpApprovedDate.TabStop = False
        Me.dtpApprovedDate.Text = "03/05/2011"
        Me.dtpApprovedDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblReleasedByCode
        '
        Me.lblReleasedByCode.AutoSize = False
        Me.lblReleasedByCode.BorderVisible = True
        Me.lblReleasedByCode.FieldName = Nothing
        Me.lblReleasedByCode.Location = New System.Drawing.Point(473, 93)
        Me.lblReleasedByCode.Name = "lblReleasedByCode"
        Me.lblReleasedByCode.Size = New System.Drawing.Size(142, 19)
        Me.lblReleasedByCode.TabIndex = 27
        '
        'lblReleasedBy
        '
        Me.lblReleasedBy.FieldName = Nothing
        Me.lblReleasedBy.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblReleasedBy.Location = New System.Drawing.Point(374, 96)
        Me.lblReleasedBy.Name = "lblReleasedBy"
        Me.lblReleasedBy.Size = New System.Drawing.Size(70, 16)
        Me.lblReleasedBy.TabIndex = 26
        Me.lblReleasedBy.Text = "Released By"
        '
        'lblCreatedDate
        '
        Me.lblCreatedDate.FieldName = Nothing
        Me.lblCreatedDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreatedDate.Location = New System.Drawing.Point(631, 54)
        Me.lblCreatedDate.Name = "lblCreatedDate"
        Me.lblCreatedDate.Size = New System.Drawing.Size(74, 16)
        Me.lblCreatedDate.TabIndex = 36
        Me.lblCreatedDate.Text = "Created Date"
        '
        'dtpCreatedDate
        '
        Me.dtpCreatedDate.CalculationExpression = Nothing
        Me.dtpCreatedDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpCreatedDate.Enabled = False
        Me.dtpCreatedDate.FieldCode = Nothing
        Me.dtpCreatedDate.FieldDesc = Nothing
        Me.dtpCreatedDate.FieldMaxLength = 0
        Me.dtpCreatedDate.FieldName = Nothing
        Me.dtpCreatedDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpCreatedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpCreatedDate.isCalculatedField = False
        Me.dtpCreatedDate.IsSourceFromTable = False
        Me.dtpCreatedDate.IsSourceFromValueList = False
        Me.dtpCreatedDate.IsUnique = False
        Me.dtpCreatedDate.Location = New System.Drawing.Point(730, 52)
        Me.dtpCreatedDate.MendatroryField = False
        Me.dtpCreatedDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpCreatedDate.MyLinkLable1 = Me.lblCreatedDate
        Me.dtpCreatedDate.MyLinkLable2 = Nothing
        Me.dtpCreatedDate.Name = "dtpCreatedDate"
        Me.dtpCreatedDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpCreatedDate.ReferenceFieldDesc = Nothing
        Me.dtpCreatedDate.ReferenceFieldName = Nothing
        Me.dtpCreatedDate.ReferenceTableName = Nothing
        Me.dtpCreatedDate.ShowCheckBox = True
        Me.dtpCreatedDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpCreatedDate.TabIndex = 6
        Me.dtpCreatedDate.TabStop = False
        Me.dtpCreatedDate.Text = "03/05/2011"
        Me.dtpCreatedDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblActualStartSate
        '
        Me.lblActualStartSate.FieldName = Nothing
        Me.lblActualStartSate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActualStartSate.Location = New System.Drawing.Point(631, 9)
        Me.lblActualStartSate.Name = "lblActualStartSate"
        Me.lblActualStartSate.Size = New System.Drawing.Size(92, 16)
        Me.lblActualStartSate.TabIndex = 43
        Me.lblActualStartSate.Text = "Actual Start Date"
        '
        'dtpActualStartDate
        '
        Me.dtpActualStartDate.CalculationExpression = Nothing
        Me.dtpActualStartDate.Checked = True
        Me.dtpActualStartDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpActualStartDate.FieldCode = Nothing
        Me.dtpActualStartDate.FieldDesc = Nothing
        Me.dtpActualStartDate.FieldMaxLength = 0
        Me.dtpActualStartDate.FieldName = Nothing
        Me.dtpActualStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpActualStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpActualStartDate.isCalculatedField = False
        Me.dtpActualStartDate.IsSourceFromTable = False
        Me.dtpActualStartDate.IsSourceFromValueList = False
        Me.dtpActualStartDate.IsUnique = False
        Me.dtpActualStartDate.Location = New System.Drawing.Point(730, 7)
        Me.dtpActualStartDate.MendatroryField = False
        Me.dtpActualStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpActualStartDate.MyLinkLable1 = Me.lblActualStartSate
        Me.dtpActualStartDate.MyLinkLable2 = Nothing
        Me.dtpActualStartDate.Name = "dtpActualStartDate"
        Me.dtpActualStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpActualStartDate.ReferenceFieldDesc = Nothing
        Me.dtpActualStartDate.ReferenceFieldName = Nothing
        Me.dtpActualStartDate.ReferenceTableName = Nothing
        Me.dtpActualStartDate.ShowCheckBox = True
        Me.dtpActualStartDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpActualStartDate.TabIndex = 2
        Me.dtpActualStartDate.TabStop = False
        Me.dtpActualStartDate.Text = "03/05/2011"
        Me.dtpActualStartDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblActualEndDate
        '
        Me.lblActualEndDate.FieldName = Nothing
        Me.lblActualEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActualEndDate.Location = New System.Drawing.Point(631, 31)
        Me.lblActualEndDate.Name = "lblActualEndDate"
        Me.lblActualEndDate.Size = New System.Drawing.Size(88, 16)
        Me.lblActualEndDate.TabIndex = 40
        Me.lblActualEndDate.Text = "Actual End Date"
        '
        'dtpActualEndDate
        '
        Me.dtpActualEndDate.CalculationExpression = Nothing
        Me.dtpActualEndDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpActualEndDate.FieldCode = Nothing
        Me.dtpActualEndDate.FieldDesc = Nothing
        Me.dtpActualEndDate.FieldMaxLength = 0
        Me.dtpActualEndDate.FieldName = Nothing
        Me.dtpActualEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpActualEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpActualEndDate.isCalculatedField = False
        Me.dtpActualEndDate.IsSourceFromTable = False
        Me.dtpActualEndDate.IsSourceFromValueList = False
        Me.dtpActualEndDate.IsUnique = False
        Me.dtpActualEndDate.Location = New System.Drawing.Point(730, 29)
        Me.dtpActualEndDate.MendatroryField = False
        Me.dtpActualEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpActualEndDate.MyLinkLable1 = Me.lblActualEndDate
        Me.dtpActualEndDate.MyLinkLable2 = Nothing
        Me.dtpActualEndDate.Name = "dtpActualEndDate"
        Me.dtpActualEndDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpActualEndDate.ReferenceFieldDesc = Nothing
        Me.dtpActualEndDate.ReferenceFieldName = Nothing
        Me.dtpActualEndDate.ReferenceTableName = Nothing
        Me.dtpActualEndDate.ShowCheckBox = True
        Me.dtpActualEndDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpActualEndDate.TabIndex = 4
        Me.dtpActualEndDate.TabStop = False
        Me.dtpActualEndDate.Text = "03/05/2011"
        Me.dtpActualEndDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblIncharge
        '
        Me.lblIncharge.FieldName = Nothing
        Me.lblIncharge.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblIncharge.Location = New System.Drawing.Point(6, 116)
        Me.lblIncharge.Name = "lblIncharge"
        Me.lblIncharge.Size = New System.Drawing.Size(54, 18)
        Me.lblIncharge.TabIndex = 21
        Me.lblIncharge.Text = "In Charge"
        '
        'fndInCharge
        '
        Me.fndInCharge.CalculationExpression = Nothing
        Me.fndInCharge.FieldCode = Nothing
        Me.fndInCharge.FieldDesc = Nothing
        Me.fndInCharge.FieldMaxLength = 0
        Me.fndInCharge.FieldName = Nothing
        Me.fndInCharge.isCalculatedField = False
        Me.fndInCharge.IsSourceFromTable = False
        Me.fndInCharge.IsSourceFromValueList = False
        Me.fndInCharge.IsUnique = False
        Me.fndInCharge.Location = New System.Drawing.Point(130, 115)
        Me.fndInCharge.MendatroryField = False
        Me.fndInCharge.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndInCharge.MyLinkLable1 = Me.lblIncharge
        Me.fndInCharge.MyLinkLable2 = Nothing
        Me.fndInCharge.MyReadOnly = False
        Me.fndInCharge.MyShowMasterFormButton = False
        Me.fndInCharge.Name = "fndInCharge"
        Me.fndInCharge.ReferenceFieldDesc = Nothing
        Me.fndInCharge.ReferenceFieldName = Nothing
        Me.fndInCharge.ReferenceTableName = Nothing
        Me.fndInCharge.Size = New System.Drawing.Size(219, 19)
        Me.fndInCharge.TabIndex = 11
        Me.fndInCharge.Value = ""
        '
        'lblPlanner
        '
        Me.lblPlanner.FieldName = Nothing
        Me.lblPlanner.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblPlanner.Location = New System.Drawing.Point(6, 93)
        Me.lblPlanner.Name = "lblPlanner"
        Me.lblPlanner.Size = New System.Drawing.Size(44, 18)
        Me.lblPlanner.TabIndex = 25
        Me.lblPlanner.Text = "Planner"
        '
        'fndPlanner
        '
        Me.fndPlanner.CalculationExpression = Nothing
        Me.fndPlanner.FieldCode = Nothing
        Me.fndPlanner.FieldDesc = Nothing
        Me.fndPlanner.FieldMaxLength = 0
        Me.fndPlanner.FieldName = Nothing
        Me.fndPlanner.isCalculatedField = False
        Me.fndPlanner.IsSourceFromTable = False
        Me.fndPlanner.IsSourceFromValueList = False
        Me.fndPlanner.IsUnique = False
        Me.fndPlanner.Location = New System.Drawing.Point(129, 92)
        Me.fndPlanner.MendatroryField = False
        Me.fndPlanner.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPlanner.MyLinkLable1 = Me.lblPlanner
        Me.fndPlanner.MyLinkLable2 = Nothing
        Me.fndPlanner.MyReadOnly = False
        Me.fndPlanner.MyShowMasterFormButton = False
        Me.fndPlanner.Name = "fndPlanner"
        Me.fndPlanner.ReferenceFieldDesc = Nothing
        Me.fndPlanner.ReferenceFieldName = Nothing
        Me.fndPlanner.ReferenceTableName = Nothing
        Me.fndPlanner.Size = New System.Drawing.Size(219, 19)
        Me.fndPlanner.TabIndex = 9
        Me.fndPlanner.Value = ""
        '
        'lblDueDate
        '
        Me.lblDueDate.FieldName = Nothing
        Me.lblDueDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDueDate.Location = New System.Drawing.Point(5, 70)
        Me.lblDueDate.Name = "lblDueDate"
        Me.lblDueDate.Size = New System.Drawing.Size(54, 16)
        Me.lblDueDate.TabIndex = 29
        Me.lblDueDate.Text = "Due Date"
        '
        'dtpDueDate
        '
        Me.dtpDueDate.CalculationExpression = Nothing
        Me.dtpDueDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDueDate.FieldCode = Nothing
        Me.dtpDueDate.FieldDesc = Nothing
        Me.dtpDueDate.FieldMaxLength = 0
        Me.dtpDueDate.FieldName = Nothing
        Me.dtpDueDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDueDate.isCalculatedField = False
        Me.dtpDueDate.IsSourceFromTable = False
        Me.dtpDueDate.IsSourceFromValueList = False
        Me.dtpDueDate.IsUnique = False
        Me.dtpDueDate.Location = New System.Drawing.Point(131, 71)
        Me.dtpDueDate.MendatroryField = True
        Me.dtpDueDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDueDate.MyLinkLable1 = Me.lblDueDate
        Me.dtpDueDate.MyLinkLable2 = Nothing
        Me.dtpDueDate.Name = "dtpDueDate"
        Me.dtpDueDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDueDate.ReferenceFieldDesc = Nothing
        Me.dtpDueDate.ReferenceFieldName = Nothing
        Me.dtpDueDate.ReferenceTableName = Nothing
        Me.dtpDueDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpDueDate.TabIndex = 7
        Me.dtpDueDate.TabStop = False
        Me.dtpDueDate.Text = "03/05/2011"
        Me.dtpDueDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblBOMCode
        '
        Me.lblBOMCode.FieldName = Nothing
        Me.lblBOMCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblBOMCode.Location = New System.Drawing.Point(4, 6)
        Me.lblBOMCode.Name = "lblBOMCode"
        Me.lblBOMCode.Size = New System.Drawing.Size(61, 18)
        Me.lblBOMCode.TabIndex = 41
        Me.lblBOMCode.Text = "BOM Code"
        '
        'fndBomCode
        '
        Me.fndBomCode.CalculationExpression = Nothing
        Me.fndBomCode.FieldCode = Nothing
        Me.fndBomCode.FieldDesc = Nothing
        Me.fndBomCode.FieldMaxLength = 0
        Me.fndBomCode.FieldName = Nothing
        Me.fndBomCode.isCalculatedField = False
        Me.fndBomCode.IsSourceFromTable = False
        Me.fndBomCode.IsSourceFromValueList = False
        Me.fndBomCode.IsUnique = False
        Me.fndBomCode.Location = New System.Drawing.Point(131, 5)
        Me.fndBomCode.MendatroryField = True
        Me.fndBomCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBomCode.MyLinkLable1 = Me.lblBOMCode
        Me.fndBomCode.MyLinkLable2 = Nothing
        Me.fndBomCode.MyReadOnly = False
        Me.fndBomCode.MyShowMasterFormButton = False
        Me.fndBomCode.Name = "fndBomCode"
        Me.fndBomCode.ReferenceFieldDesc = Nothing
        Me.fndBomCode.ReferenceFieldName = Nothing
        Me.fndBomCode.ReferenceTableName = Nothing
        Me.fndBomCode.Size = New System.Drawing.Size(219, 19)
        Me.fndBomCode.TabIndex = 0
        Me.fndBomCode.Value = ""
        '
        'lblOrderDate
        '
        Me.lblOrderDate.FieldName = Nothing
        Me.lblOrderDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrderDate.Location = New System.Drawing.Point(5, 48)
        Me.lblOrderDate.Name = "lblOrderDate"
        Me.lblOrderDate.Size = New System.Drawing.Size(62, 16)
        Me.lblOrderDate.TabIndex = 33
        Me.lblOrderDate.Text = "Order Date"
        '
        'dtpMODate
        '
        Me.dtpMODate.CalculationExpression = Nothing
        Me.dtpMODate.CustomFormat = "dd/MM/yyyy"
        Me.dtpMODate.FieldCode = Nothing
        Me.dtpMODate.FieldDesc = Nothing
        Me.dtpMODate.FieldMaxLength = 0
        Me.dtpMODate.FieldName = Nothing
        Me.dtpMODate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpMODate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMODate.isCalculatedField = False
        Me.dtpMODate.IsSourceFromTable = False
        Me.dtpMODate.IsSourceFromValueList = False
        Me.dtpMODate.IsUnique = False
        Me.dtpMODate.Location = New System.Drawing.Point(131, 49)
        Me.dtpMODate.MendatroryField = True
        Me.dtpMODate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpMODate.MyLinkLable1 = Me.lblOrderDate
        Me.dtpMODate.MyLinkLable2 = Nothing
        Me.dtpMODate.Name = "dtpMODate"
        Me.dtpMODate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpMODate.ReferenceFieldDesc = Nothing
        Me.dtpMODate.ReferenceFieldName = Nothing
        Me.dtpMODate.ReferenceTableName = Nothing
        Me.dtpMODate.Size = New System.Drawing.Size(143, 18)
        Me.dtpMODate.TabIndex = 5
        Me.dtpMODate.TabStop = False
        Me.dtpMODate.Text = "03/05/2011"
        Me.dtpMODate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblApprovedByName
        '
        Me.lblApprovedByName.AutoSize = False
        Me.lblApprovedByName.BorderVisible = True
        Me.lblApprovedByName.FieldName = Nothing
        Me.lblApprovedByName.Location = New System.Drawing.Point(473, 71)
        Me.lblApprovedByName.Name = "lblApprovedByName"
        Me.lblApprovedByName.Size = New System.Drawing.Size(142, 19)
        Me.lblApprovedByName.TabIndex = 31
        '
        'lblApprovedBy
        '
        Me.lblApprovedBy.FieldName = Nothing
        Me.lblApprovedBy.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblApprovedBy.Location = New System.Drawing.Point(374, 73)
        Me.lblApprovedBy.Name = "lblApprovedBy"
        Me.lblApprovedBy.Size = New System.Drawing.Size(71, 16)
        Me.lblApprovedBy.TabIndex = 30
        Me.lblApprovedBy.Text = "Approved By"
        '
        'lblCreatedByName
        '
        Me.lblCreatedByName.AutoSize = False
        Me.lblCreatedByName.BorderVisible = True
        Me.lblCreatedByName.FieldName = Nothing
        Me.lblCreatedByName.Location = New System.Drawing.Point(473, 51)
        Me.lblCreatedByName.Name = "lblCreatedByName"
        Me.lblCreatedByName.Size = New System.Drawing.Size(142, 19)
        Me.lblCreatedByName.TabIndex = 35
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(4, 27)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel1.TabIndex = 37
        Me.MyLabel1.Text = "Revision No"
        '
        'lblCreatedBy
        '
        Me.lblCreatedBy.FieldName = Nothing
        Me.lblCreatedBy.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCreatedBy.Location = New System.Drawing.Point(374, 51)
        Me.lblCreatedBy.Name = "lblCreatedBy"
        Me.lblCreatedBy.Size = New System.Drawing.Size(63, 16)
        Me.lblCreatedBy.TabIndex = 34
        Me.lblCreatedBy.Text = "Created By"
        '
        'lblPlanStartDate
        '
        Me.lblPlanStartDate.FieldName = Nothing
        Me.lblPlanStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlanStartDate.Location = New System.Drawing.Point(373, 9)
        Me.lblPlanStartDate.Name = "lblPlanStartDate"
        Me.lblPlanStartDate.Size = New System.Drawing.Size(83, 16)
        Me.lblPlanStartDate.TabIndex = 42
        Me.lblPlanStartDate.Text = "Plan Start Date"
        '
        'dtpPlanStartDate
        '
        Me.dtpPlanStartDate.CalculationExpression = Nothing
        Me.dtpPlanStartDate.Checked = True
        Me.dtpPlanStartDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpPlanStartDate.FieldCode = Nothing
        Me.dtpPlanStartDate.FieldDesc = Nothing
        Me.dtpPlanStartDate.FieldMaxLength = 0
        Me.dtpPlanStartDate.FieldName = Nothing
        Me.dtpPlanStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpPlanStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPlanStartDate.isCalculatedField = False
        Me.dtpPlanStartDate.IsSourceFromTable = False
        Me.dtpPlanStartDate.IsSourceFromValueList = False
        Me.dtpPlanStartDate.IsUnique = False
        Me.dtpPlanStartDate.Location = New System.Drawing.Point(472, 7)
        Me.dtpPlanStartDate.MendatroryField = False
        Me.dtpPlanStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPlanStartDate.MyLinkLable1 = Me.lblPlanStartDate
        Me.dtpPlanStartDate.MyLinkLable2 = Nothing
        Me.dtpPlanStartDate.Name = "dtpPlanStartDate"
        Me.dtpPlanStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPlanStartDate.ReferenceFieldDesc = Nothing
        Me.dtpPlanStartDate.ReferenceFieldName = Nothing
        Me.dtpPlanStartDate.ReferenceTableName = Nothing
        Me.dtpPlanStartDate.ShowCheckBox = True
        Me.dtpPlanStartDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpPlanStartDate.TabIndex = 1
        Me.dtpPlanStartDate.TabStop = False
        Me.dtpPlanStartDate.Text = "03/05/2011"
        Me.dtpPlanStartDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblRevisionNo
        '
        Me.lblRevisionNo.AutoSize = False
        Me.lblRevisionNo.BorderVisible = True
        Me.lblRevisionNo.FieldName = Nothing
        Me.lblRevisionNo.Location = New System.Drawing.Point(131, 27)
        Me.lblRevisionNo.Name = "lblRevisionNo"
        Me.lblRevisionNo.Size = New System.Drawing.Size(218, 19)
        Me.lblRevisionNo.TabIndex = 38
        '
        'lblEndDate
        '
        Me.lblEndDate.FieldName = Nothing
        Me.lblEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEndDate.Location = New System.Drawing.Point(374, 31)
        Me.lblEndDate.Name = "lblEndDate"
        Me.lblEndDate.Size = New System.Drawing.Size(79, 16)
        Me.lblEndDate.TabIndex = 39
        Me.lblEndDate.Text = "Plan End Date"
        '
        'dtpPlanEndDate
        '
        Me.dtpPlanEndDate.CalculationExpression = Nothing
        Me.dtpPlanEndDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpPlanEndDate.FieldCode = Nothing
        Me.dtpPlanEndDate.FieldDesc = Nothing
        Me.dtpPlanEndDate.FieldMaxLength = 0
        Me.dtpPlanEndDate.FieldName = Nothing
        Me.dtpPlanEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpPlanEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPlanEndDate.isCalculatedField = False
        Me.dtpPlanEndDate.IsSourceFromTable = False
        Me.dtpPlanEndDate.IsSourceFromValueList = False
        Me.dtpPlanEndDate.IsUnique = False
        Me.dtpPlanEndDate.Location = New System.Drawing.Point(472, 29)
        Me.dtpPlanEndDate.MendatroryField = False
        Me.dtpPlanEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPlanEndDate.MyLinkLable1 = Me.lblEndDate
        Me.dtpPlanEndDate.MyLinkLable2 = Nothing
        Me.dtpPlanEndDate.Name = "dtpPlanEndDate"
        Me.dtpPlanEndDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPlanEndDate.ReferenceFieldDesc = Nothing
        Me.dtpPlanEndDate.ReferenceFieldName = Nothing
        Me.dtpPlanEndDate.ReferenceTableName = Nothing
        Me.dtpPlanEndDate.ShowCheckBox = True
        Me.dtpPlanEndDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpPlanEndDate.TabIndex = 3
        Me.dtpPlanEndDate.TabStop = False
        Me.dtpPlanEndDate.Text = "03/05/2011"
        Me.dtpPlanEndDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'btnBrowse
        '
        Me.btnBrowse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(355, 161)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(66, 18)
        Me.btnBrowse.TabIndex = 16
        Me.btnBrowse.Text = "Browse"
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
        Me.txtDocPath.Location = New System.Drawing.Point(131, 162)
        Me.txtDocPath.MaxLength = 49
        Me.txtDocPath.MendatroryField = False
        Me.txtDocPath.MyLinkLable1 = Me.lblDocument
        Me.txtDocPath.MyLinkLable2 = Nothing
        Me.txtDocPath.Name = "txtDocPath"
        Me.txtDocPath.ReferenceFieldDesc = Nothing
        Me.txtDocPath.ReferenceFieldName = Nothing
        Me.txtDocPath.ReferenceTableName = Nothing
        Me.txtDocPath.Size = New System.Drawing.Size(198, 18)
        Me.txtDocPath.TabIndex = 14
        '
        'lblDocument
        '
        Me.lblDocument.FieldName = Nothing
        Me.lblDocument.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDocument.Location = New System.Drawing.Point(7, 163)
        Me.lblDocument.Name = "lblDocument"
        Me.lblDocument.Size = New System.Drawing.Size(58, 16)
        Me.lblDocument.TabIndex = 19
        Me.lblDocument.Text = "Document"
        '
        'btnNewFile
        '
        Me.btnNewFile.Image = CType(resources.GetObject("btnNewFile.Image"), System.Drawing.Image)
        Me.btnNewFile.Location = New System.Drawing.Point(334, 161)
        Me.btnNewFile.Name = "btnNewFile"
        Me.btnNewFile.Size = New System.Drawing.Size(14, 20)
        Me.btnNewFile.TabIndex = 15
        Me.btnNewFile.Text = " "
        '
        'pageOperations
        '
        Me.pageOperations.Controls.Add(Me.RadSplitContainer2)
        Me.pageOperations.Controls.Add(Me.RadPanel1)
        Me.pageOperations.ItemSize = New System.Drawing.SizeF(71.0!, 28.0!)
        Me.pageOperations.Location = New System.Drawing.Point(10, 37)
        Me.pageOperations.Name = "pageOperations"
        Me.pageOperations.Size = New System.Drawing.Size(1139, 293)
        Me.pageOperations.Text = "Operations"
        '
        'RadSplitContainer2
        '
        Me.RadSplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadSplitContainer2.Controls.Add(Me.SplitPanel3)
        Me.RadSplitContainer2.Controls.Add(Me.SplitPanel4)
        Me.RadSplitContainer2.Location = New System.Drawing.Point(4, 177)
        Me.RadSplitContainer2.Name = "RadSplitContainer2"
        '
        '
        '
        Me.RadSplitContainer2.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.RadSplitContainer2.Size = New System.Drawing.Size(1132, 116)
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
        Me.SplitPanel3.Size = New System.Drawing.Size(564, 116)
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
        Me.gvResources.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvResources.MyStopExport = False
        Me.gvResources.Name = "gvResources"
        Me.gvResources.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvResources.ShowHeaderCellButtons = True
        Me.gvResources.Size = New System.Drawing.Size(564, 116)
        Me.gvResources.TabIndex = 6
        Me.gvResources.TabStop = False
        '
        'SplitPanel4
        '
        Me.SplitPanel4.Controls.Add(Me.gvTools)
        Me.SplitPanel4.Location = New System.Drawing.Point(568, 0)
        Me.SplitPanel4.Name = "SplitPanel4"
        '
        '
        '
        Me.SplitPanel4.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.SplitPanel4.Size = New System.Drawing.Size(564, 116)
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
        Me.gvTools.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gvTools.MyStopExport = False
        Me.gvTools.Name = "gvTools"
        Me.gvTools.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvTools.ShowHeaderCellButtons = True
        Me.gvTools.Size = New System.Drawing.Size(564, 116)
        Me.gvTools.TabIndex = 6
        Me.gvTools.TabStop = False
        '
        'RadPanel1
        '
        Me.RadPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPanel1.Controls.Add(Me.gvOperations)
        Me.RadPanel1.Location = New System.Drawing.Point(3, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1133, 174)
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
        Me.gvOperations.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gvOperations.MyStopExport = False
        Me.gvOperations.Name = "gvOperations"
        Me.gvOperations.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvOperations.ShowHeaderCellButtons = True
        Me.gvOperations.Size = New System.Drawing.Size(1133, 174)
        Me.gvOperations.TabIndex = 5
        Me.gvOperations.TabStop = False
        '
        'pageComponent
        '
        Me.pageComponent.Controls.Add(Me.gvBOM)
        Me.pageComponent.ItemSize = New System.Drawing.SizeF(80.0!, 28.0!)
        Me.pageComponent.Location = New System.Drawing.Point(10, 37)
        Me.pageComponent.Name = "pageComponent"
        Me.pageComponent.Size = New System.Drawing.Size(1139, 284)
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
        Me.gvBOM.MasterTemplate.AutoGenerateColumns = False
        Me.gvBOM.MasterTemplate.EnableGrouping = False
        Me.gvBOM.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvBOM.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvBOM.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gvBOM.MyStopExport = False
        Me.gvBOM.Name = "gvBOM"
        Me.gvBOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvBOM.ShowHeaderCellButtons = True
        Me.gvBOM.Size = New System.Drawing.Size(1139, 284)
        Me.gvBOM.TabIndex = 4
        Me.gvBOM.TabStop = False
        '
        'pageTotal
        '
        Me.pageTotal.Controls.Add(Me.RadPanel4)
        Me.pageTotal.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.pageTotal.Location = New System.Drawing.Point(10, 37)
        Me.pageTotal.Name = "pageTotal"
        Me.pageTotal.Size = New System.Drawing.Size(1139, 284)
        Me.pageTotal.Text = "Total"
        '
        'RadPanel4
        '
        Me.RadPanel4.Controls.Add(Me.gvCost)
        Me.RadPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel4.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel4.Name = "RadPanel4"
        Me.RadPanel4.Size = New System.Drawing.Size(1139, 284)
        Me.RadPanel4.TabIndex = 231
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
        Me.gvCost.MasterTemplate.AutoGenerateColumns = False
        Me.gvCost.MasterTemplate.EnableGrouping = False
        Me.gvCost.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvCost.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCost.MasterTemplate.ViewDefinition = TableViewDefinition5
        Me.gvCost.MyStopExport = False
        Me.gvCost.Name = "gvCost"
        Me.gvCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvCost.ShowHeaderCellButtons = True
        Me.gvCost.Size = New System.Drawing.Size(1139, 284)
        Me.gvCost.TabIndex = 5
        Me.gvCost.TabStop = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(228, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(156, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(8, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1085, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 22)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(82, 8)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(68, 22)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'frmManufacturingOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1160, 600)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmManufacturingOrder"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Manufacturing Order"
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadSplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadSplitContainer1.ResumeLayout(False)
        CType(Me.SplitPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel1.ResumeLayout(False)
        Me.SplitPanel1.PerformLayout()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtParentMOCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSODesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProductionPlanDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSourceDocumentCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboSourceDocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMOReference, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMOReference, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMasterItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMODesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOrderedQtyStockUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMinBatchSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMasterItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboMOStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMOStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOrderedQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQtyOrdered, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnitName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel2.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.pageGeneral.ResumeLayout(False)
        Me.pageGeneral.PerformLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProductionLine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblClosedate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpCloseDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblClosedByCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblComments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReleasedDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpReleasedDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApprovedDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpApprovedDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReleasedByCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReleasedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreatedDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpCreatedDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblActualStartSate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpActualStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblActualEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpActualEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIncharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPlanner, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDueDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDueDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBOMCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOrderDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpMODate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApprovedByName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApprovedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreatedByName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreatedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPlanStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpPlanStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRevisionNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpPlanEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocPath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocument, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.pageTotal.ResumeLayout(False)
        CType(Me.RadPanel4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel4.ResumeLayout(False)
        CType(Me.gvCost.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtProducedItem As common.UserControls.txtFinder
    Friend WithEvents lblMasterItem As common.Controls.MyLabel
    Friend WithEvents lblOrderDate As common.Controls.MyLabel
    Friend WithEvents dtpMODate As common.Controls.MyDateTimePicker
    Friend WithEvents gvBOM As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents txtMasterItemName As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpPlanEndDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblEndDate As common.Controls.MyLabel
    Friend WithEvents dtpPlanStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblPlanStartDate As common.Controls.MyLabel
    Friend WithEvents lblMODesc As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents cboMOStatus As common.Controls.MyComboBox
    Friend WithEvents lblMOStatus As common.Controls.MyLabel
    Friend WithEvents lblDocument As common.Controls.MyLabel
    Friend WithEvents btnBrowse As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDocPath As common.Controls.MyTextBox
    Friend WithEvents lblRevisionNo As common.Controls.MyLabel
    Friend WithEvents lblQtyOrdered As common.Controls.MyLabel
    Friend WithEvents txtOrderedQty As common.MyNumBox
    Friend WithEvents lblUnitName As common.Controls.MyLabel
    Friend WithEvents txtOrderedQtyStockUnit As common.MyNumBox
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
    Friend WithEvents fndOrderQtyUOM As common.UserControls.txtFinder
    Friend WithEvents txtMOReference As common.Controls.MyTextBox
    Friend WithEvents lblMOReference As common.Controls.MyLabel
    Friend WithEvents pageTotal As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPanel4 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents lblBOMCode As common.Controls.MyLabel
    Friend WithEvents fndBomCode As common.UserControls.txtFinder
    Friend WithEvents lblDueDate As common.Controls.MyLabel
    Friend WithEvents dtpDueDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblPlanner As common.Controls.MyLabel
    Friend WithEvents fndPlanner As common.UserControls.txtFinder
    Friend WithEvents lblIncharge As common.Controls.MyLabel
    Friend WithEvents fndInCharge As common.UserControls.txtFinder
    Friend WithEvents lblActualStartSate As common.Controls.MyLabel
    Friend WithEvents dtpActualStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblActualEndDate As common.Controls.MyLabel
    Friend WithEvents dtpActualEndDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblCreatedDate As common.Controls.MyLabel
    Friend WithEvents dtpCreatedDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblApprovedDate As common.Controls.MyLabel
    Friend WithEvents dtpApprovedDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblReleasedByCode As common.Controls.MyLabel
    Friend WithEvents lblReleasedBy As common.Controls.MyLabel
    Friend WithEvents lblReleasedDate As common.Controls.MyLabel
    Friend WithEvents dtpReleasedDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtComments As common.Controls.MyTextBox
    Friend WithEvents lblComments As common.Controls.MyLabel
    Friend WithEvents lblClosedate As common.Controls.MyLabel
    Friend WithEvents dtpCloseDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblClosedByCode As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents gvCost As common.UserControls.MyRadGridView
    Friend WithEvents RadSplitContainer2 As Telerik.WinControls.UI.RadSplitContainer
    Friend WithEvents SplitPanel3 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents SplitPanel4 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents lblProductionLine As common.Controls.MyLabel
    Friend WithEvents fndProductionLine As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents cboSourceDocType As common.Controls.MyComboBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtProductionPlanDesc As common.Controls.MyLabel
    Friend WithEvents lblSourceDocumentCode As common.Controls.MyLabel
    Friend WithEvents fndProductionPlan As common.UserControls.txtFinder
    Friend WithEvents txtSODesc As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents fndSONo As common.UserControls.txtFinder
    Friend WithEvents txtParentMOCode As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
End Class
