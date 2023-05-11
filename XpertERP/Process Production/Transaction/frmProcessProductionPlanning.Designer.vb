<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProcessProductionPlanning
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmProcessProductionPlanning))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.lblCostCenterName = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.TxtCostCenterCode = New common.UserControls.txtFinder()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.FndLineNo = New common.UserControls.txtFinder()
        Me.TxtCategory = New common.Controls.MyLabel()
        Me.TxtProfitCenterCode = New common.UserControls.txtFinder()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.lblProfitCenterName = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblConsmSectionLocCode = New common.Controls.MyLabel()
        Me.TxtSection = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.fndSection = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.fndItemCategory = New common.UserControls.txtFinder()
        Me.txtDispatch_Days = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtdesc = New common.Controls.MyTextBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtlocationcode = New common.UserControls.txtFinder()
        Me.lblMasterItem = New common.Controls.MyLabel()
        Me.txtlocationname = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.lblBOMStatus = New common.Controls.MyLabel()
        Me.cboBOMStatus = New common.Controls.MyComboBox()
        Me.lblCode = New common.Controls.MyLabel()
        Me.lblBomDate = New common.Controls.MyLabel()
        Me.dtpDate = New common.Controls.MyDateTimePicker()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvSectionStock = New common.UserControls.MyRadGridView()
        Me.pageSectionStockHistory = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvSectionStockHistory = New common.UserControls.MyRadGridView()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.btnCopy = New Telerik.WinControls.UI.RadButton()
        Me.btnunpost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblCostCenterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProfitCenterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConsmSectionLocCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDispatch_Days, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMasterItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtlocationname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboBOMStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gvSectionStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSectionStock.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageSectionStockHistory.SuspendLayout()
        CType(Me.gvSectionStockHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSectionStockHistory.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnunpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCopy)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnunpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(974, 431)
        Me.SplitContainer1.SplitterDistance = 395
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.pageSectionStockHistory)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(3, 3)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(968, 389)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(71.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(947, 341)
        Me.RadPageViewPage1.Text = "Item Detail"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCostCenterName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.TxtCostCenterCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.FndLineNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.TxtProfitCenterCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblProfitCenterName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel10)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblConsmSectionLocCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.TxtSection)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndSection)
        Me.SplitContainer2.Panel1.Controls.Add(Me.TxtCategory)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndItemCategory)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDispatch_Days)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtdesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtlocationcode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtlocationname)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblMasterItem)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBOMStatus)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboBOMStatus)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBomDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(1)
        Me.SplitContainer2.Size = New System.Drawing.Size(947, 341)
        Me.SplitContainer2.SplitterDistance = 225
        Me.SplitContainer2.TabIndex = 0
        '
        'lblCostCenterName
        '
        Me.lblCostCenterName.AutoSize = False
        Me.lblCostCenterName.BorderVisible = True
        Me.lblCostCenterName.FieldName = Nothing
        Me.lblCostCenterName.Location = New System.Drawing.Point(282, 177)
        Me.lblCostCenterName.Name = "lblCostCenterName"
        Me.lblCostCenterName.Size = New System.Drawing.Size(389, 19)
        Me.lblCostCenterName.TabIndex = 64
        Me.lblCostCenterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(7, 177)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(94, 18)
        Me.MyLabel7.TabIndex = 63
        Me.MyLabel7.Text = "Cost Center Code"
        '
        'TxtCostCenterCode
        '
        Me.TxtCostCenterCode.CalculationExpression = Nothing
        Me.TxtCostCenterCode.FieldCode = Nothing
        Me.TxtCostCenterCode.FieldDesc = Nothing
        Me.TxtCostCenterCode.FieldMaxLength = 0
        Me.TxtCostCenterCode.FieldName = Nothing
        Me.TxtCostCenterCode.isCalculatedField = False
        Me.TxtCostCenterCode.IsSourceFromTable = False
        Me.TxtCostCenterCode.IsSourceFromValueList = False
        Me.TxtCostCenterCode.IsUnique = False
        Me.TxtCostCenterCode.Location = New System.Drawing.Point(127, 177)
        Me.TxtCostCenterCode.MendatroryField = False
        Me.TxtCostCenterCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCostCenterCode.MyLinkLable1 = Me.MyLabel7
        Me.TxtCostCenterCode.MyLinkLable2 = Me.lblCostCenterName
        Me.TxtCostCenterCode.MyReadOnly = False
        Me.TxtCostCenterCode.MyShowMasterFormButton = False
        Me.TxtCostCenterCode.Name = "TxtCostCenterCode"
        Me.TxtCostCenterCode.ReferenceFieldDesc = Nothing
        Me.TxtCostCenterCode.ReferenceFieldName = Nothing
        Me.TxtCostCenterCode.ReferenceTableName = Nothing
        Me.TxtCostCenterCode.Size = New System.Drawing.Size(153, 19)
        Me.TxtCostCenterCode.TabIndex = 58
        Me.TxtCostCenterCode.Value = ""
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(7, 156)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel8.TabIndex = 62
        Me.MyLabel8.Text = "Line No"
        '
        'FndLineNo
        '
        Me.FndLineNo.CalculationExpression = Nothing
        Me.FndLineNo.FieldCode = Nothing
        Me.FndLineNo.FieldDesc = Nothing
        Me.FndLineNo.FieldMaxLength = 0
        Me.FndLineNo.FieldName = Nothing
        Me.FndLineNo.isCalculatedField = False
        Me.FndLineNo.IsSourceFromTable = False
        Me.FndLineNo.IsSourceFromValueList = False
        Me.FndLineNo.IsUnique = False
        Me.FndLineNo.Location = New System.Drawing.Point(127, 155)
        Me.FndLineNo.MendatroryField = False
        Me.FndLineNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndLineNo.MyLinkLable1 = Me.MyLabel8
        Me.FndLineNo.MyLinkLable2 = Me.TxtCategory
        Me.FndLineNo.MyReadOnly = False
        Me.FndLineNo.MyShowMasterFormButton = False
        Me.FndLineNo.Name = "FndLineNo"
        Me.FndLineNo.ReferenceFieldDesc = Nothing
        Me.FndLineNo.ReferenceFieldName = Nothing
        Me.FndLineNo.ReferenceTableName = Nothing
        Me.FndLineNo.Size = New System.Drawing.Size(153, 19)
        Me.FndLineNo.TabIndex = 57
        Me.FndLineNo.Value = ""
        '
        'TxtCategory
        '
        Me.TxtCategory.AutoSize = False
        Me.TxtCategory.BorderVisible = True
        Me.TxtCategory.FieldName = Nothing
        Me.TxtCategory.Location = New System.Drawing.Point(282, 64)
        Me.TxtCategory.Name = "TxtCategory"
        Me.TxtCategory.Size = New System.Drawing.Size(389, 19)
        Me.TxtCategory.TabIndex = 50
        Me.TxtCategory.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtProfitCenterCode
        '
        Me.TxtProfitCenterCode.CalculationExpression = Nothing
        Me.TxtProfitCenterCode.Enabled = False
        Me.TxtProfitCenterCode.FieldCode = Nothing
        Me.TxtProfitCenterCode.FieldDesc = Nothing
        Me.TxtProfitCenterCode.FieldMaxLength = 0
        Me.TxtProfitCenterCode.FieldName = Nothing
        Me.TxtProfitCenterCode.isCalculatedField = False
        Me.TxtProfitCenterCode.IsSourceFromTable = False
        Me.TxtProfitCenterCode.IsSourceFromValueList = False
        Me.TxtProfitCenterCode.IsUnique = False
        Me.TxtProfitCenterCode.Location = New System.Drawing.Point(127, 199)
        Me.TxtProfitCenterCode.MendatroryField = False
        Me.TxtProfitCenterCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtProfitCenterCode.MyLinkLable1 = Me.MyLabel10
        Me.TxtProfitCenterCode.MyLinkLable2 = Me.lblProfitCenterName
        Me.TxtProfitCenterCode.MyReadOnly = False
        Me.TxtProfitCenterCode.MyShowMasterFormButton = False
        Me.TxtProfitCenterCode.Name = "TxtProfitCenterCode"
        Me.TxtProfitCenterCode.ReferenceFieldDesc = Nothing
        Me.TxtProfitCenterCode.ReferenceFieldName = Nothing
        Me.TxtProfitCenterCode.ReferenceTableName = Nothing
        Me.TxtProfitCenterCode.Size = New System.Drawing.Size(153, 19)
        Me.TxtProfitCenterCode.TabIndex = 59
        Me.TxtProfitCenterCode.Value = ""
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel10.Location = New System.Drawing.Point(7, 200)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(99, 18)
        Me.MyLabel10.TabIndex = 61
        Me.MyLabel10.Text = "Profit Center Code"
        '
        'lblProfitCenterName
        '
        Me.lblProfitCenterName.AutoSize = False
        Me.lblProfitCenterName.BorderVisible = True
        Me.lblProfitCenterName.FieldName = Nothing
        Me.lblProfitCenterName.Location = New System.Drawing.Point(282, 199)
        Me.lblProfitCenterName.Name = "lblProfitCenterName"
        Me.lblProfitCenterName.Size = New System.Drawing.Size(389, 19)
        Me.lblProfitCenterName.TabIndex = 60
        Me.lblProfitCenterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(286, 131)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(116, 18)
        Me.MyLabel5.TabIndex = 56
        Me.MyLabel5.Text = "Consumpton Location"
        '
        'lblConsmSectionLocCode
        '
        Me.lblConsmSectionLocCode.AutoSize = False
        Me.lblConsmSectionLocCode.BorderVisible = True
        Me.lblConsmSectionLocCode.FieldName = Nothing
        Me.lblConsmSectionLocCode.Location = New System.Drawing.Point(419, 131)
        Me.lblConsmSectionLocCode.Name = "lblConsmSectionLocCode"
        Me.lblConsmSectionLocCode.Size = New System.Drawing.Size(252, 19)
        Me.lblConsmSectionLocCode.TabIndex = 55
        Me.lblConsmSectionLocCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtSection
        '
        Me.TxtSection.AutoSize = False
        Me.TxtSection.BorderVisible = True
        Me.TxtSection.FieldName = Nothing
        Me.TxtSection.Location = New System.Drawing.Point(282, 86)
        Me.TxtSection.Name = "TxtSection"
        Me.TxtSection.Size = New System.Drawing.Size(389, 19)
        Me.TxtSection.TabIndex = 54
        Me.TxtSection.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(7, 86)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(43, 18)
        Me.MyLabel6.TabIndex = 53
        Me.MyLabel6.Text = "Section"
        '
        'fndSection
        '
        Me.fndSection.CalculationExpression = Nothing
        Me.fndSection.FieldCode = Nothing
        Me.fndSection.FieldDesc = Nothing
        Me.fndSection.FieldMaxLength = 0
        Me.fndSection.FieldName = Nothing
        Me.fndSection.isCalculatedField = False
        Me.fndSection.IsSourceFromTable = False
        Me.fndSection.IsSourceFromValueList = False
        Me.fndSection.IsUnique = False
        Me.fndSection.Location = New System.Drawing.Point(127, 86)
        Me.fndSection.MendatroryField = True
        Me.fndSection.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSection.MyLinkLable1 = Me.MyLabel6
        Me.fndSection.MyLinkLable2 = Me.TxtSection
        Me.fndSection.MyReadOnly = False
        Me.fndSection.MyShowMasterFormButton = False
        Me.fndSection.Name = "fndSection"
        Me.fndSection.ReferenceFieldDesc = Nothing
        Me.fndSection.ReferenceFieldName = Nothing
        Me.fndSection.ReferenceTableName = Nothing
        Me.fndSection.Size = New System.Drawing.Size(153, 19)
        Me.fndSection.TabIndex = 5
        Me.fndSection.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(7, 65)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(109, 16)
        Me.MyLabel3.TabIndex = 51
        Me.MyLabel3.Text = "Production Category"
        '
        'fndItemCategory
        '
        Me.fndItemCategory.CalculationExpression = Nothing
        Me.fndItemCategory.FieldCode = Nothing
        Me.fndItemCategory.FieldDesc = Nothing
        Me.fndItemCategory.FieldMaxLength = 0
        Me.fndItemCategory.FieldName = Nothing
        Me.fndItemCategory.isCalculatedField = False
        Me.fndItemCategory.IsSourceFromTable = False
        Me.fndItemCategory.IsSourceFromValueList = False
        Me.fndItemCategory.IsUnique = False
        Me.fndItemCategory.Location = New System.Drawing.Point(127, 64)
        Me.fndItemCategory.MendatroryField = True
        Me.fndItemCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndItemCategory.MyLinkLable1 = Me.MyLabel3
        Me.fndItemCategory.MyLinkLable2 = Me.TxtCategory
        Me.fndItemCategory.MyReadOnly = False
        Me.fndItemCategory.MyShowMasterFormButton = False
        Me.fndItemCategory.Name = "fndItemCategory"
        Me.fndItemCategory.ReferenceFieldDesc = Nothing
        Me.fndItemCategory.ReferenceFieldName = Nothing
        Me.fndItemCategory.ReferenceTableName = Nothing
        Me.fndItemCategory.Size = New System.Drawing.Size(153, 19)
        Me.fndItemCategory.TabIndex = 4
        Me.fndItemCategory.Value = ""
        '
        'txtDispatch_Days
        '
        Me.txtDispatch_Days.CalculationExpression = Nothing
        Me.txtDispatch_Days.DecimalPlaces = 0
        Me.txtDispatch_Days.FieldCode = Nothing
        Me.txtDispatch_Days.FieldDesc = Nothing
        Me.txtDispatch_Days.FieldMaxLength = 0
        Me.txtDispatch_Days.FieldName = Nothing
        Me.txtDispatch_Days.isCalculatedField = False
        Me.txtDispatch_Days.IsSourceFromTable = False
        Me.txtDispatch_Days.IsSourceFromValueList = False
        Me.txtDispatch_Days.IsUnique = False
        Me.txtDispatch_Days.Location = New System.Drawing.Point(127, 131)
        Me.txtDispatch_Days.MendatroryField = True
        Me.txtDispatch_Days.MyLinkLable1 = Me.MyLabel1
        Me.txtDispatch_Days.MyLinkLable2 = Nothing
        Me.txtDispatch_Days.Name = "txtDispatch_Days"
        Me.txtDispatch_Days.ReferenceFieldDesc = Nothing
        Me.txtDispatch_Days.ReferenceFieldName = Nothing
        Me.txtDispatch_Days.ReferenceTableName = Nothing
        Me.txtDispatch_Days.Size = New System.Drawing.Size(153, 20)
        Me.txtDispatch_Days.TabIndex = 7
        Me.txtDispatch_Days.Text = "0"
        Me.txtDispatch_Days.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDispatch_Days.Value = 0.0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(7, 133)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel1.TabIndex = 48
        Me.MyLabel1.Text = "Dispatch Days"
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
        Me.txtdesc.Location = New System.Drawing.Point(127, 41)
        Me.txtdesc.MaxLength = 200
        Me.txtdesc.MendatroryField = False
        Me.txtdesc.MyLinkLable1 = Me.MyLabel9
        Me.txtdesc.MyLinkLable2 = Nothing
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.ReferenceFieldDesc = Nothing
        Me.txtdesc.ReferenceFieldName = Nothing
        Me.txtdesc.ReferenceTableName = Nothing
        Me.txtdesc.Size = New System.Drawing.Size(544, 20)
        Me.txtdesc.TabIndex = 3
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(7, 43)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel9.TabIndex = 47
        Me.MyLabel9.Text = "Description"
        '
        'txtlocationcode
        '
        Me.txtlocationcode.CalculationExpression = Nothing
        Me.txtlocationcode.FieldCode = Nothing
        Me.txtlocationcode.FieldDesc = Nothing
        Me.txtlocationcode.FieldMaxLength = 0
        Me.txtlocationcode.FieldName = Nothing
        Me.txtlocationcode.isCalculatedField = False
        Me.txtlocationcode.IsSourceFromTable = False
        Me.txtlocationcode.IsSourceFromValueList = False
        Me.txtlocationcode.IsUnique = False
        Me.txtlocationcode.Location = New System.Drawing.Point(127, 108)
        Me.txtlocationcode.MendatroryField = True
        Me.txtlocationcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlocationcode.MyLinkLable1 = Me.lblMasterItem
        Me.txtlocationcode.MyLinkLable2 = Me.txtlocationname
        Me.txtlocationcode.MyReadOnly = False
        Me.txtlocationcode.MyShowMasterFormButton = False
        Me.txtlocationcode.Name = "txtlocationcode"
        Me.txtlocationcode.ReferenceFieldDesc = Nothing
        Me.txtlocationcode.ReferenceFieldName = Nothing
        Me.txtlocationcode.ReferenceTableName = Nothing
        Me.txtlocationcode.Size = New System.Drawing.Size(153, 19)
        Me.txtlocationcode.TabIndex = 6
        Me.txtlocationcode.Value = ""
        '
        'lblMasterItem
        '
        Me.lblMasterItem.FieldName = Nothing
        Me.lblMasterItem.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMasterItem.Location = New System.Drawing.Point(7, 109)
        Me.lblMasterItem.Name = "lblMasterItem"
        Me.lblMasterItem.Size = New System.Drawing.Size(49, 18)
        Me.lblMasterItem.TabIndex = 35
        Me.lblMasterItem.Text = "Location"
        '
        'txtlocationname
        '
        Me.txtlocationname.AutoSize = False
        Me.txtlocationname.BorderVisible = True
        Me.txtlocationname.FieldName = Nothing
        Me.txtlocationname.Location = New System.Drawing.Point(282, 108)
        Me.txtlocationname.Name = "txtlocationname"
        Me.txtlocationname.Size = New System.Drawing.Size(389, 19)
        Me.txtlocationname.TabIndex = 34
        Me.txtlocationname.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(841, 17)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 32
        '
        'lblBOMStatus
        '
        Me.lblBOMStatus.FieldName = Nothing
        Me.lblBOMStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBOMStatus.Location = New System.Drawing.Point(505, 19)
        Me.lblBOMStatus.Name = "lblBOMStatus"
        Me.lblBOMStatus.Size = New System.Drawing.Size(38, 16)
        Me.lblBOMStatus.TabIndex = 31
        Me.lblBOMStatus.Text = "Status"
        '
        'cboBOMStatus
        '
        Me.cboBOMStatus.AutoCompleteDisplayMember = Nothing
        Me.cboBOMStatus.AutoCompleteValueMember = Nothing
        Me.cboBOMStatus.CalculationExpression = Nothing
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
        RadListDataItem4.Text = "In-Active"
        Me.cboBOMStatus.Items.Add(RadListDataItem1)
        Me.cboBOMStatus.Items.Add(RadListDataItem2)
        Me.cboBOMStatus.Items.Add(RadListDataItem3)
        Me.cboBOMStatus.Items.Add(RadListDataItem4)
        Me.cboBOMStatus.Location = New System.Drawing.Point(545, 18)
        Me.cboBOMStatus.MendatroryField = True
        Me.cboBOMStatus.MyLinkLable1 = Me.lblBOMStatus
        Me.cboBOMStatus.MyLinkLable2 = Nothing
        Me.cboBOMStatus.Name = "cboBOMStatus"
        Me.cboBOMStatus.ReferenceFieldDesc = Nothing
        Me.cboBOMStatus.ReferenceFieldName = Nothing
        Me.cboBOMStatus.ReferenceTableName = Nothing
        Me.cboBOMStatus.Size = New System.Drawing.Size(126, 18)
        Me.cboBOMStatus.TabIndex = 2
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(7, 18)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(80, 16)
        Me.lblCode.TabIndex = 4
        Me.lblCode.Text = "Planning Code"
        '
        'lblBomDate
        '
        Me.lblBomDate.FieldName = Nothing
        Me.lblBomDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBomDate.Location = New System.Drawing.Point(386, 19)
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
        Me.dtpDate.Location = New System.Drawing.Point(419, 18)
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
        Me.btnNew.Location = New System.Drawing.Point(372, 16)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 21)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = " "
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(127, 16)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(242, 21)
        Me.txtCode.TabIndex = 3
        Me.txtCode.Value = ""
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(1, 1)
        '
        'gv
        '
        Me.gv.MasterTemplate.AllowDragToGroup = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.EnableGrouping = False
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(945, 89)
        Me.gv.TabIndex = 0
        Me.gv.Text = "RadGridView1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(1, 90)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(945, 21)
        Me.Panel1.TabIndex = 1
        '
        'MyLabel2
        '
        Me.MyLabel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel2.ForeColor = System.Drawing.Color.Blue
        Me.MyLabel2.Location = New System.Drawing.Point(639, 1)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(303, 18)
        Me.MyLabel2.TabIndex = 50
        Me.MyLabel2.Text = "Click F4 key on Stock In Hand column to see detail of stock."
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(947, 341)
        Me.RadPageViewPage2.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(947, 341)
        Me.UcAttachment1.TabIndex = 4
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gvSectionStock)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(53.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(947, 341)
        Me.RadPageViewPage3.Text = "Section"
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
        Me.gvSectionStock.MasterTemplate.EnableFiltering = True
        Me.gvSectionStock.MasterTemplate.EnableGrouping = False
        Me.gvSectionStock.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvSectionStock.Name = "gvSectionStock"
        Me.gvSectionStock.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvSectionStock.ShowHeaderCellButtons = True
        Me.gvSectionStock.Size = New System.Drawing.Size(947, 341)
        Me.gvSectionStock.TabIndex = 3
        Me.gvSectionStock.TabStop = False
        Me.gvSectionStock.Text = "RadGridView1"
        '
        'pageSectionStockHistory
        '
        Me.pageSectionStockHistory.Controls.Add(Me.gvSectionStockHistory)
        Me.pageSectionStockHistory.ItemSize = New System.Drawing.SizeF(92.0!, 28.0!)
        Me.pageSectionStockHistory.Location = New System.Drawing.Point(10, 37)
        Me.pageSectionStockHistory.Name = "pageSectionStockHistory"
        Me.pageSectionStockHistory.Size = New System.Drawing.Size(947, 341)
        Me.pageSectionStockHistory.Text = "Section History"
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
        Me.gvSectionStockHistory.MasterTemplate.EnableFiltering = True
        Me.gvSectionStockHistory.MasterTemplate.EnableGrouping = False
        Me.gvSectionStockHistory.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvSectionStockHistory.Name = "gvSectionStockHistory"
        Me.gvSectionStockHistory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvSectionStockHistory.ShowHeaderCellButtons = True
        Me.gvSectionStockHistory.Size = New System.Drawing.Size(947, 341)
        Me.gvSectionStockHistory.TabIndex = 4
        Me.gvSectionStockHistory.TabStop = False
        Me.gvSectionStockHistory.Text = "RadGridView1"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(396, 6)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(73, 20)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        '
        'btnCopy
        '
        Me.btnCopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCopy.Location = New System.Drawing.Point(240, 6)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(73, 20)
        Me.btnCopy.TabIndex = 3
        Me.btnCopy.Text = "Copy"
        '
        'btnunpost
        '
        Me.btnunpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnunpost.Location = New System.Drawing.Point(317, 6)
        Me.btnunpost.Name = "btnunpost"
        Me.btnunpost.Size = New System.Drawing.Size(73, 20)
        Me.btnunpost.TabIndex = 4
        Me.btnunpost.Text = "Unpost"
        Me.btnunpost.Visible = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(887, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Location = New System.Drawing.Point(163, 6)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(86, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(9, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(475, 6)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(71, 20)
        Me.btnHistory.TabIndex = 37
        Me.btnHistory.Text = "&History"
        '
        'FrmProcessProductionPlanning
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(974, 431)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmProcessProductionPlanning"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmProcessProductionPlanning"
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
        CType(Me.lblCostCenterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProfitCenterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConsmSectionLocCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDispatch_Days, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMasterItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtlocationname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboBOMStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gvSectionStock.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSectionStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageSectionStockHistory.ResumeLayout(False)
        CType(Me.gvSectionStockHistory.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSectionStockHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCopy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnunpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents lblBomDate As common.Controls.MyLabel
    Friend WithEvents dtpDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents lblBOMStatus As common.Controls.MyLabel
    Friend WithEvents cboBOMStatus As common.Controls.MyComboBox
    Friend WithEvents txtlocationcode As common.UserControls.txtFinder
    Friend WithEvents lblMasterItem As common.Controls.MyLabel
    Friend WithEvents txtlocationname As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents txtdesc As common.Controls.MyTextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtDispatch_Days As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents btnunpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents TxtCategory As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents fndItemCategory As common.UserControls.txtFinder
    Friend WithEvents TxtSection As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents fndSection As common.UserControls.txtFinder
    Friend WithEvents btnCopy As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pageSectionStockHistory As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvSectionStock As common.UserControls.MyRadGridView
    Friend WithEvents gvSectionStockHistory As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblConsmSectionLocCode As common.Controls.MyLabel
    Friend WithEvents btnCancel As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblCostCenterName As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents TxtCostCenterCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents FndLineNo As common.UserControls.txtFinder
    Friend WithEvents TxtProfitCenterCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents lblProfitCenterName As common.Controls.MyLabel
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
End Class

