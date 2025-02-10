<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBoothRouteMapping
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnGExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadBExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnGImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnBImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.btnCC = New Telerik.WinControls.UI.RadButton()
        Me.lblRouteDesc = New common.Controls.MyLabel()
        Me.cmbItemType = New common.Controls.MyComboBox()
        Me.lblItemType = New common.Controls.MyLabel()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.lblRemark = New common.Controls.MyLabel()
        Me.cmbShiftType = New common.Controls.MyComboBox()
        Me.lblShiftType = New common.Controls.MyLabel()
        Me.txtRouteNo = New common.UserControls.txtFinder()
        Me.lblRoute = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtSupplyDate = New common.Controls.MyDateTimePicker()
        Me.lblSupplyDate = New System.Windows.Forms.Label()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.lblDocCode = New common.Controls.MyLabel()
        Me.gv1 = New Telerik.WinControls.UI.RadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.btnShowHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.btnLatestExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.btnCC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbShiftType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShiftType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSupplyDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.btnShowHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExport, Me.btnImport})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        Me.RadMenuItem3.UseCompatibleTextRendering = False
        '
        'btnExport
        '
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnGExport, Me.RadBExport, Me.btnLatestExport})
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "Export"
        Me.btnExport.UseCompatibleTextRendering = False
        '
        'btnGExport
        '
        Me.btnGExport.Name = "btnGExport"
        Me.btnGExport.Text = "Grid Export"
        '
        'RadBExport
        '
        Me.RadBExport.Name = "RadBExport"
        Me.RadBExport.Text = "Bulk Export"
        '
        'btnImport
        '
        Me.btnImport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnGImport, Me.btnBImport})
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Text = "Import"
        Me.btnImport.UseCompatibleTextRendering = False
        '
        'btnGImport
        '
        Me.btnGImport.Name = "btnGImport"
        Me.btnGImport.Text = "Grid Import"
        '
        'btnBImport
        '
        Me.btnBImport.Name = "btnBImport"
        Me.btnBImport.Text = "Bulk Import"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnShowHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Size = New System.Drawing.Size(851, 430)
        Me.SplitContainer1.SplitterDistance = 389
        Me.SplitContainer1.TabIndex = 11
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(851, 389)
        Me.RadPageView1.TabIndex = 0
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(127.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(830, 341)
        Me.RadPageViewPage1.Text = "Booth Route Mapping"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnCC)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRouteDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmbItemType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblItemType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRemark)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRemark)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmbShiftType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblShiftType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRouteNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRoute)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtSupplyDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblSupplyDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDocNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDocCode)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(830, 341)
        Me.SplitContainer2.SplitterDistance = 84
        Me.SplitContainer2.TabIndex = 1
        '
        'btnCC
        '
        Me.btnCC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCC.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCC.Location = New System.Drawing.Point(326, 5)
        Me.btnCC.Name = "btnCC"
        Me.btnCC.Size = New System.Drawing.Size(25, 21)
        Me.btnCC.TabIndex = 1538
        Me.btnCC.Text = "CC"
        '
        'lblRouteDesc
        '
        Me.lblRouteDesc.AutoSize = False
        Me.lblRouteDesc.BorderVisible = True
        Me.lblRouteDesc.FieldName = Nothing
        Me.lblRouteDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRouteDesc.Location = New System.Drawing.Point(329, 29)
        Me.lblRouteDesc.Name = "lblRouteDesc"
        Me.lblRouteDesc.Size = New System.Drawing.Size(181, 16)
        Me.lblRouteDesc.TabIndex = 1537
        Me.lblRouteDesc.TextWrap = False
        '
        'cmbItemType
        '
        Me.cmbItemType.AutoCompleteDisplayMember = Nothing
        Me.cmbItemType.AutoCompleteValueMember = Nothing
        Me.cmbItemType.CalculationExpression = Nothing
        Me.cmbItemType.DropDownAnimationEnabled = True
        Me.cmbItemType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbItemType.FieldCode = Nothing
        Me.cmbItemType.FieldDesc = Nothing
        Me.cmbItemType.FieldMaxLength = 0
        Me.cmbItemType.FieldName = Nothing
        Me.cmbItemType.isCalculatedField = False
        Me.cmbItemType.IsSourceFromTable = False
        Me.cmbItemType.IsSourceFromValueList = False
        Me.cmbItemType.IsUnique = False
        RadListDataItem1.Selected = True
        RadListDataItem1.Text = "MILK"
        RadListDataItem2.Text = "Product"
        RadListDataItem3.Text = "Ice-cream"
        Me.cmbItemType.Items.Add(RadListDataItem1)
        Me.cmbItemType.Items.Add(RadListDataItem2)
        Me.cmbItemType.Items.Add(RadListDataItem3)
        Me.cmbItemType.Location = New System.Drawing.Point(579, 27)
        Me.cmbItemType.MendatroryField = False
        Me.cmbItemType.MyLinkLable1 = Nothing
        Me.cmbItemType.MyLinkLable2 = Nothing
        Me.cmbItemType.Name = "cmbItemType"
        Me.cmbItemType.ReferenceFieldDesc = Nothing
        Me.cmbItemType.ReferenceFieldName = Nothing
        Me.cmbItemType.ReferenceTableName = Nothing
        Me.cmbItemType.Size = New System.Drawing.Size(85, 20)
        Me.cmbItemType.TabIndex = 1535
        Me.cmbItemType.Text = "MILK"
        '
        'lblItemType
        '
        Me.lblItemType.FieldName = Nothing
        Me.lblItemType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemType.Location = New System.Drawing.Point(516, 29)
        Me.lblItemType.Name = "lblItemType"
        Me.lblItemType.Size = New System.Drawing.Size(57, 16)
        Me.lblItemType.TabIndex = 1536
        Me.lblItemType.Text = "Item Type"
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(91, 49)
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(628, 20)
        Me.txtRemark.TabIndex = 1534
        '
        'lblRemark
        '
        Me.lblRemark.FieldName = Nothing
        Me.lblRemark.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemark.Location = New System.Drawing.Point(7, 51)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(46, 16)
        Me.lblRemark.TabIndex = 1533
        Me.lblRemark.Text = "Remark"
        '
        'cmbShiftType
        '
        Me.cmbShiftType.AutoCompleteDisplayMember = Nothing
        Me.cmbShiftType.AutoCompleteValueMember = Nothing
        Me.cmbShiftType.CalculationExpression = Nothing
        Me.cmbShiftType.DropDownAnimationEnabled = True
        Me.cmbShiftType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbShiftType.FieldCode = Nothing
        Me.cmbShiftType.FieldDesc = Nothing
        Me.cmbShiftType.FieldMaxLength = 0
        Me.cmbShiftType.FieldName = Nothing
        Me.cmbShiftType.isCalculatedField = False
        Me.cmbShiftType.IsSourceFromTable = False
        Me.cmbShiftType.IsSourceFromValueList = False
        Me.cmbShiftType.IsUnique = False
        RadListDataItem4.Selected = True
        RadListDataItem4.Text = "Morning"
        RadListDataItem5.Text = "Evening"
        Me.cmbShiftType.Items.Add(RadListDataItem4)
        Me.cmbShiftType.Items.Add(RadListDataItem5)
        Me.cmbShiftType.Location = New System.Drawing.Point(579, 6)
        Me.cmbShiftType.MendatroryField = False
        Me.cmbShiftType.MyLinkLable1 = Nothing
        Me.cmbShiftType.MyLinkLable2 = Nothing
        Me.cmbShiftType.Name = "cmbShiftType"
        Me.cmbShiftType.ReferenceFieldDesc = Nothing
        Me.cmbShiftType.ReferenceFieldName = Nothing
        Me.cmbShiftType.ReferenceTableName = Nothing
        Me.cmbShiftType.Size = New System.Drawing.Size(85, 20)
        Me.cmbShiftType.TabIndex = 1531
        Me.cmbShiftType.Text = "Morning"
        '
        'lblShiftType
        '
        Me.lblShiftType.FieldName = Nothing
        Me.lblShiftType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShiftType.Location = New System.Drawing.Point(516, 8)
        Me.lblShiftType.Name = "lblShiftType"
        Me.lblShiftType.Size = New System.Drawing.Size(57, 16)
        Me.lblShiftType.TabIndex = 1532
        Me.lblShiftType.Text = "Shift Type"
        '
        'txtRouteNo
        '
        Me.txtRouteNo.CalculationExpression = Nothing
        Me.txtRouteNo.FieldCode = Nothing
        Me.txtRouteNo.FieldDesc = Nothing
        Me.txtRouteNo.FieldMaxLength = 0
        Me.txtRouteNo.FieldName = Nothing
        Me.txtRouteNo.isCalculatedField = False
        Me.txtRouteNo.IsSourceFromTable = False
        Me.txtRouteNo.IsSourceFromValueList = False
        Me.txtRouteNo.IsUnique = False
        Me.txtRouteNo.Location = New System.Drawing.Point(91, 29)
        Me.txtRouteNo.MendatroryField = False
        Me.txtRouteNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteNo.MyLinkLable1 = Nothing
        Me.txtRouteNo.MyLinkLable2 = Nothing
        Me.txtRouteNo.MyReadOnly = False
        Me.txtRouteNo.MyShowMasterFormButton = False
        Me.txtRouteNo.Name = "txtRouteNo"
        Me.txtRouteNo.ReferenceFieldDesc = Nothing
        Me.txtRouteNo.ReferenceFieldName = Nothing
        Me.txtRouteNo.ReferenceTableName = Nothing
        Me.txtRouteNo.Size = New System.Drawing.Size(234, 17)
        Me.txtRouteNo.TabIndex = 1523
        Me.txtRouteNo.Value = ""
        '
        'lblRoute
        '
        Me.lblRoute.FieldName = Nothing
        Me.lblRoute.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoute.Location = New System.Drawing.Point(7, 29)
        Me.lblRoute.Name = "lblRoute"
        Me.lblRoute.Size = New System.Drawing.Size(67, 16)
        Me.lblRoute.TabIndex = 1522
        Me.lblRoute.Text = "Route Code"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(669, 5)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(121, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 185
        '
        'txtSupplyDate
        '
        Me.txtSupplyDate.CalculationExpression = Nothing
        Me.txtSupplyDate.CustomFormat = "dd/MM/yyyy"
        Me.txtSupplyDate.FieldCode = Nothing
        Me.txtSupplyDate.FieldDesc = Nothing
        Me.txtSupplyDate.FieldMaxLength = 0
        Me.txtSupplyDate.FieldName = Nothing
        Me.txtSupplyDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplyDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtSupplyDate.isCalculatedField = False
        Me.txtSupplyDate.IsSourceFromTable = False
        Me.txtSupplyDate.IsSourceFromValueList = False
        Me.txtSupplyDate.IsUnique = False
        Me.txtSupplyDate.Location = New System.Drawing.Point(428, 7)
        Me.txtSupplyDate.MendatroryField = False
        Me.txtSupplyDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtSupplyDate.MyLinkLable1 = Nothing
        Me.txtSupplyDate.MyLinkLable2 = Nothing
        Me.txtSupplyDate.Name = "txtSupplyDate"
        Me.txtSupplyDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtSupplyDate.ReferenceFieldDesc = Nothing
        Me.txtSupplyDate.ReferenceFieldName = Nothing
        Me.txtSupplyDate.ReferenceTableName = Nothing
        Me.txtSupplyDate.Size = New System.Drawing.Size(87, 18)
        Me.txtSupplyDate.TabIndex = 181
        Me.txtSupplyDate.TabStop = False
        Me.txtSupplyDate.Text = "13/06/2011"
        Me.txtSupplyDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblSupplyDate
        '
        Me.lblSupplyDate.AutoSize = True
        Me.lblSupplyDate.Location = New System.Drawing.Point(354, 10)
        Me.lblSupplyDate.Name = "lblSupplyDate"
        Me.lblSupplyDate.Size = New System.Drawing.Size(69, 13)
        Me.lblSupplyDate.TabIndex = 182
        Me.lblSupplyDate.Text = "Supply Date"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(305, 5)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 180
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(95, 6)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Nothing
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 30
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(209, 19)
        Me.txtDocNo.TabIndex = 179
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
        '
        'lblDocCode
        '
        Me.lblDocCode.FieldName = Nothing
        Me.lblDocCode.Location = New System.Drawing.Point(5, 7)
        Me.lblDocCode.Name = "lblDocCode"
        Me.lblDocCode.Size = New System.Drawing.Size(88, 18)
        Me.lblDocCode.TabIndex = 178
        Me.lblDocCode.Text = "Document Code"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(830, 253)
        Me.gv1.TabIndex = 0
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(830, 341)
        Me.RadPageViewPage2.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(830, 341)
        Me.UcAttachment1.TabIndex = 2
        Me.UcAttachment1.TabStop = False
        '
        'btnShowHistory
        '
        Me.btnShowHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnShowHistory.Location = New System.Drawing.Point(235, 9)
        Me.btnShowHistory.Name = "btnShowHistory"
        Me.btnShowHistory.Size = New System.Drawing.Size(92, 24)
        Me.btnShowHistory.TabIndex = 170
        Me.btnShowHistory.Text = "Show History"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(79, 10)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(71, 24)
        Me.btnDelete.TabIndex = 169
        Me.btnDelete.Text = "Delete"
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Location = New System.Drawing.Point(658, 9)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(110, 24)
        Me.btnReverse.TabIndex = 168
        Me.btnReverse.Text = "Reverse and Unpost"
        Me.btnReverse.Visible = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(3, 9)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(71, 24)
        Me.btnSave.TabIndex = 165
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(774, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(71, 24)
        Me.btnClose.TabIndex = 166
        Me.btnClose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Location = New System.Drawing.Point(154, 10)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(77, 24)
        Me.btnPost.TabIndex = 167
        Me.btnPost.Text = "Post"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(851, 20)
        Me.RadMenu1.TabIndex = 10
        '
        'btnLatestExport
        '
        Me.btnLatestExport.Name = "btnLatestExport"
        Me.btnLatestExport.Text = "Latest Export by Route"
        '
        'FrmBoothRouteMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(851, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmBoothRouteMapping"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Booth Route Mapping"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.btnCC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbShiftType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShiftType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSupplyDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.btnShowHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenuItem3 As RadMenuItem
    Friend WithEvents btnExport As RadMenuItem
    Friend WithEvents btnImport As RadMenuItem
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnShowHistory As RadButton
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnReverse As RadButton
    Friend WithEvents btnSave As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnPost As RadButton
    Friend WithEvents RadBExport As RadMenuItem
    Friend WithEvents btnGExport As RadMenuItem
    Friend WithEvents btnBImport As RadMenuItem
    Friend WithEvents btnGImport As RadMenuItem
    Friend WithEvents RadMenu1 As RadMenu
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents btnCC As RadButton
    Friend WithEvents lblRouteDesc As common.Controls.MyLabel
    Friend WithEvents cmbItemType As common.Controls.MyComboBox
    Friend WithEvents lblItemType As common.Controls.MyLabel
    Friend WithEvents txtRemark As TextBox
    Friend WithEvents lblRemark As common.Controls.MyLabel
    Friend WithEvents cmbShiftType As common.Controls.MyComboBox
    Friend WithEvents lblShiftType As common.Controls.MyLabel
    Friend WithEvents txtRouteNo As common.UserControls.txtFinder
    Friend WithEvents lblRoute As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtSupplyDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblSupplyDate As Label
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents lblDocCode As common.Controls.MyLabel
    Friend WithEvents gv1 As RadGridView
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents UcAttachment1 As ucAttachment
    Friend WithEvents btnLatestExport As RadMenuItem
End Class
