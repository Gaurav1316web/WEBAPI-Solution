<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmStanderdProductionEntry
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.CboShift = New common.Controls.MyComboBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtImportTemplate = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.fndItemCategory = New common.UserControls.txtFinder()
        Me.TxtCategory = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtConsmLocMilk = New common.UserControls.txtFinder()
        Me.lblConsmLocMilkDesc = New common.Controls.MyLabel()
        Me.lblConsmLocOtherDesc = New common.Controls.MyLabel()
        Me.txtConsmLocOther = New common.UserControls.txtFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtBatchNo = New common.Controls.MyTextBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.lblBatchNo = New common.Controls.MyLabel()
        Me.lblReceiptCode = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.dtpDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.txtComment = New common.Controls.MyTextBox()
        Me.lblBatchDate = New common.Controls.MyLabel()
        Me.dtpBatchDate = New common.Controls.MyDateTimePicker()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.UsLock1 = New common.usLock()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.pageBatchProduction = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvBatch = New common.UserControls.MyRadGridView()
        Me.pageConsumption = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvConsumption = New common.UserControls.MyRadGridView()
        Me.pageProductionCost = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvProductionCost = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.btnShowInventory = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnunpost = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.CboShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConsmLocMilkDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConsmLocOtherDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBatchNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBatchNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReceiptCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBatchDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpBatchDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.pageBatchProduction.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gvBatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvBatch.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageConsumption.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gvConsumption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvConsumption.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageProductionCost.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.gvProductionCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvProductionCost.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnunpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnShowInventory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnunpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1036, 505)
        Me.SplitContainer1.SplitterDistance = 462
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.CboShift)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblConsmLocOtherDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtConsmLocOther)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtBatchNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBatchNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblReceiptCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpDate)
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
        Me.SplitContainer2.Size = New System.Drawing.Size(1036, 462)
        Me.SplitContainer2.SplitterDistance = 146
        Me.SplitContainer2.TabIndex = 2
        '
        'CboShift
        '
        Me.CboShift.AutoCompleteDisplayMember = Nothing
        Me.CboShift.AutoCompleteValueMember = Nothing
        Me.CboShift.CalculationExpression = Nothing
        Me.CboShift.DropDownAnimationEnabled = True
        Me.CboShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboShift.FieldCode = Nothing
        Me.CboShift.FieldDesc = Nothing
        Me.CboShift.FieldMaxLength = 0
        Me.CboShift.FieldName = Nothing
        Me.CboShift.isCalculatedField = False
        Me.CboShift.IsSourceFromTable = False
        Me.CboShift.IsSourceFromValueList = False
        Me.CboShift.IsUnique = False
        Me.CboShift.Location = New System.Drawing.Point(504, 30)
        Me.CboShift.MendatroryField = True
        Me.CboShift.MyLinkLable1 = Me.MyLabel4
        Me.CboShift.MyLinkLable2 = Nothing
        Me.CboShift.Name = "CboShift"
        Me.CboShift.ReferenceFieldDesc = Nothing
        Me.CboShift.ReferenceFieldName = Nothing
        Me.CboShift.ReferenceTableName = Nothing
        Me.CboShift.Size = New System.Drawing.Size(79, 20)
        Me.CboShift.TabIndex = 1519
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(469, 32)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(29, 16)
        Me.MyLabel4.TabIndex = 333
        Me.MyLabel4.Text = "Shift"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Controls.Add(Me.txtImportTemplate)
        Me.Panel1.Controls.Add(Me.MyLabel1)
        Me.Panel1.Controls.Add(Me.fndItemCategory)
        Me.Panel1.Controls.Add(Me.TxtCategory)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Controls.Add(Me.txtConsmLocMilk)
        Me.Panel1.Controls.Add(Me.lblConsmLocMilkDesc)
        Me.Panel1.Location = New System.Drawing.Point(860, 76)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(155, 67)
        Me.Panel1.TabIndex = 331
        Me.Panel1.Visible = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(10, 18)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel3.TabIndex = 58
        Me.MyLabel3.Text = "Prod. Category"
        '
        'txtImportTemplate
        '
        Me.txtImportTemplate.arrDispalyMember = Nothing
        Me.txtImportTemplate.arrValueMember = Nothing
        Me.txtImportTemplate.Enabled = False
        Me.txtImportTemplate.Location = New System.Drawing.Point(236, 37)
        Me.txtImportTemplate.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImportTemplate.MyLinkLable1 = Me.MyLabel1
        Me.txtImportTemplate.MyLinkLable2 = Nothing
        Me.txtImportTemplate.MyNullText = "All"
        Me.txtImportTemplate.Name = "txtImportTemplate"
        Me.txtImportTemplate.Size = New System.Drawing.Size(104, 20)
        Me.txtImportTemplate.TabIndex = 329
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(236, 16)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(90, 18)
        Me.MyLabel1.TabIndex = 330
        Me.MyLabel1.Text = "Import Template"
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
        Me.fndItemCategory.Location = New System.Drawing.Point(10, 37)
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
        Me.fndItemCategory.Size = New System.Drawing.Size(19, 20)
        Me.fndItemCategory.TabIndex = 55
        Me.fndItemCategory.Value = ""
        '
        'TxtCategory
        '
        Me.TxtCategory.AutoSize = False
        Me.TxtCategory.BorderVisible = True
        Me.TxtCategory.FieldName = Nothing
        Me.TxtCategory.Location = New System.Drawing.Point(3, 63)
        Me.TxtCategory.Name = "TxtCategory"
        Me.TxtCategory.Size = New System.Drawing.Size(32, 19)
        Me.TxtCategory.TabIndex = 57
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(98, 18)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(107, 16)
        Me.MyLabel2.TabIndex = 61
        Me.MyLabel2.Text = "Con. Location( Milk)"
        '
        'txtConsmLocMilk
        '
        Me.txtConsmLocMilk.CalculationExpression = Nothing
        Me.txtConsmLocMilk.FieldCode = Nothing
        Me.txtConsmLocMilk.FieldDesc = Nothing
        Me.txtConsmLocMilk.FieldMaxLength = 0
        Me.txtConsmLocMilk.FieldName = Nothing
        Me.txtConsmLocMilk.isCalculatedField = False
        Me.txtConsmLocMilk.IsSourceFromTable = False
        Me.txtConsmLocMilk.IsSourceFromValueList = False
        Me.txtConsmLocMilk.IsUnique = False
        Me.txtConsmLocMilk.Location = New System.Drawing.Point(49, 40)
        Me.txtConsmLocMilk.MendatroryField = True
        Me.txtConsmLocMilk.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConsmLocMilk.MyLinkLable1 = Me.MyLabel2
        Me.txtConsmLocMilk.MyLinkLable2 = Me.lblConsmLocMilkDesc
        Me.txtConsmLocMilk.MyReadOnly = False
        Me.txtConsmLocMilk.MyShowMasterFormButton = False
        Me.txtConsmLocMilk.Name = "txtConsmLocMilk"
        Me.txtConsmLocMilk.ReferenceFieldDesc = Nothing
        Me.txtConsmLocMilk.ReferenceFieldName = Nothing
        Me.txtConsmLocMilk.ReferenceTableName = Nothing
        Me.txtConsmLocMilk.Size = New System.Drawing.Size(43, 18)
        Me.txtConsmLocMilk.TabIndex = 59
        Me.txtConsmLocMilk.Value = ""
        '
        'lblConsmLocMilkDesc
        '
        Me.lblConsmLocMilkDesc.AutoSize = False
        Me.lblConsmLocMilkDesc.BorderVisible = True
        Me.lblConsmLocMilkDesc.FieldName = Nothing
        Me.lblConsmLocMilkDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConsmLocMilkDesc.Location = New System.Drawing.Point(98, 43)
        Me.lblConsmLocMilkDesc.Name = "lblConsmLocMilkDesc"
        Me.lblConsmLocMilkDesc.Size = New System.Drawing.Size(51, 20)
        Me.lblConsmLocMilkDesc.TabIndex = 60
        '
        'lblConsmLocOtherDesc
        '
        Me.lblConsmLocOtherDesc.AutoSize = False
        Me.lblConsmLocOtherDesc.BorderVisible = True
        Me.lblConsmLocOtherDesc.FieldName = Nothing
        Me.lblConsmLocOtherDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConsmLocOtherDesc.Location = New System.Drawing.Point(269, 75)
        Me.lblConsmLocOtherDesc.Name = "lblConsmLocOtherDesc"
        Me.lblConsmLocOtherDesc.Size = New System.Drawing.Size(314, 20)
        Me.lblConsmLocOtherDesc.TabIndex = 63
        '
        'txtConsmLocOther
        '
        Me.txtConsmLocOther.CalculationExpression = Nothing
        Me.txtConsmLocOther.FieldCode = Nothing
        Me.txtConsmLocOther.FieldDesc = Nothing
        Me.txtConsmLocOther.FieldMaxLength = 0
        Me.txtConsmLocOther.FieldName = Nothing
        Me.txtConsmLocOther.isCalculatedField = False
        Me.txtConsmLocOther.IsSourceFromTable = False
        Me.txtConsmLocOther.IsSourceFromValueList = False
        Me.txtConsmLocOther.IsUnique = False
        Me.txtConsmLocOther.Location = New System.Drawing.Point(101, 76)
        Me.txtConsmLocOther.MendatroryField = True
        Me.txtConsmLocOther.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConsmLocOther.MyLinkLable1 = Me.MyLabel5
        Me.txtConsmLocOther.MyLinkLable2 = Me.lblConsmLocOtherDesc
        Me.txtConsmLocOther.MyReadOnly = False
        Me.txtConsmLocOther.MyShowMasterFormButton = False
        Me.txtConsmLocOther.Name = "txtConsmLocOther"
        Me.txtConsmLocOther.ReferenceFieldDesc = Nothing
        Me.txtConsmLocOther.ReferenceFieldName = Nothing
        Me.txtConsmLocOther.ReferenceTableName = Nothing
        Me.txtConsmLocOther.Size = New System.Drawing.Size(167, 19)
        Me.txtConsmLocOther.TabIndex = 62
        Me.txtConsmLocOther.Value = ""
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(13, 77)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel5.TabIndex = 64
        Me.MyLabel5.Text = "Con. Location"
        '
        'txtBatchNo
        '
        Me.txtBatchNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBatchNo.CalculationExpression = Nothing
        Me.txtBatchNo.FieldCode = Nothing
        Me.txtBatchNo.FieldDesc = Nothing
        Me.txtBatchNo.FieldMaxLength = 0
        Me.txtBatchNo.FieldName = Nothing
        Me.txtBatchNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBatchNo.isCalculatedField = False
        Me.txtBatchNo.IsSourceFromTable = False
        Me.txtBatchNo.IsSourceFromValueList = False
        Me.txtBatchNo.IsUnique = False
        Me.txtBatchNo.Location = New System.Drawing.Point(101, 99)
        Me.txtBatchNo.MaxLength = 200
        Me.txtBatchNo.MendatroryField = False
        Me.txtBatchNo.MyLinkLable1 = Me.RadLabel2
        Me.txtBatchNo.MyLinkLable2 = Nothing
        Me.txtBatchNo.Name = "txtBatchNo"
        Me.txtBatchNo.ReferenceFieldDesc = Nothing
        Me.txtBatchNo.ReferenceFieldName = Nothing
        Me.txtBatchNo.ReferenceTableName = Nothing
        Me.txtBatchNo.Size = New System.Drawing.Size(167, 18)
        Me.txtBatchNo.TabIndex = 28
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(13, 122)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel2.TabIndex = 10
        Me.RadLabel2.Text = "Comment"
        '
        'lblBatchNo
        '
        Me.lblBatchNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblBatchNo.FieldName = Nothing
        Me.lblBatchNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBatchNo.Location = New System.Drawing.Point(13, 100)
        Me.lblBatchNo.Name = "lblBatchNo"
        Me.lblBatchNo.Size = New System.Drawing.Size(53, 16)
        Me.lblBatchNo.TabIndex = 17
        Me.lblBatchNo.Text = "Batch No"
        '
        'lblReceiptCode
        '
        Me.lblReceiptCode.FieldName = Nothing
        Me.lblReceiptCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReceiptCode.Location = New System.Drawing.Point(13, 9)
        Me.lblReceiptCode.Name = "lblReceiptCode"
        Me.lblReceiptCode.Size = New System.Drawing.Size(75, 16)
        Me.lblReceiptCode.TabIndex = 18
        Me.lblReceiptCode.Text = "Receipt Code"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPProcessProduction.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(329, 8)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 19)
        Me.btnAddNew.TabIndex = 1
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
        Me.txtDesc.Location = New System.Drawing.Point(101, 31)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel5
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(361, 18)
        Me.txtDesc.TabIndex = 6
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(13, 32)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel5.TabIndex = 11
        Me.RadLabel5.Text = "Description"
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
        Me.dtpDate.Location = New System.Drawing.Point(383, 8)
        Me.dtpDate.MendatroryField = False
        Me.dtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.MyLinkLable1 = Me.RadLabel4
        Me.dtpDate.MyLinkLable2 = Nothing
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.ReferenceFieldDesc = Nothing
        Me.dtpDate.ReferenceFieldName = Nothing
        Me.dtpDate.ReferenceTableName = Nothing
        Me.dtpDate.Size = New System.Drawing.Size(79, 18)
        Me.dtpDate.TabIndex = 2
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "13/06/2011"
        Me.dtpDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(351, 9)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 19
        Me.RadLabel4.Text = "Date"
        '
        'txtComment
        '
        Me.txtComment.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.txtComment.Location = New System.Drawing.Point(101, 121)
        Me.txtComment.MaxLength = 200
        Me.txtComment.MendatroryField = False
        Me.txtComment.MyLinkLable1 = Me.RadLabel2
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        Me.txtComment.Size = New System.Drawing.Size(482, 18)
        Me.txtComment.TabIndex = 8
        '
        'lblBatchDate
        '
        Me.lblBatchDate.FieldName = Nothing
        Me.lblBatchDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBatchDate.Location = New System.Drawing.Point(269, 100)
        Me.lblBatchDate.Name = "lblBatchDate"
        Me.lblBatchDate.Size = New System.Drawing.Size(62, 16)
        Me.lblBatchDate.TabIndex = 16
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
        Me.dtpBatchDate.Location = New System.Drawing.Point(337, 99)
        Me.dtpBatchDate.MendatroryField = False
        Me.dtpBatchDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBatchDate.MyLinkLable1 = Me.lblBatchDate
        Me.dtpBatchDate.MyLinkLable2 = Nothing
        Me.dtpBatchDate.Name = "dtpBatchDate"
        Me.dtpBatchDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBatchDate.ReferenceFieldDesc = Nothing
        Me.dtpBatchDate.ReferenceFieldName = Nothing
        Me.dtpBatchDate.ReferenceTableName = Nothing
        Me.dtpBatchDate.Size = New System.Drawing.Size(79, 18)
        Me.dtpBatchDate.TabIndex = 4
        Me.dtpBatchDate.TabStop = False
        Me.dtpBatchDate.Text = "13/06/2011"
        Me.dtpBatchDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(269, 52)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(314, 20)
        Me.lblLocation.TabIndex = 14
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(101, 8)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblReceiptCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(228, 19)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(469, 8)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(114, 19)
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
        Me.txtLocation.Location = New System.Drawing.Point(101, 53)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel6
        Me.txtLocation.MyLinkLable2 = Me.lblLocation
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(167, 19)
        Me.txtLocation.TabIndex = 5
        Me.txtLocation.Value = ""
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(13, 54)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel6.TabIndex = 15
        Me.RadLabel6.Text = "Location"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.pageBatchProduction)
        Me.RadPageView1.Controls.Add(Me.pageConsumption)
        Me.RadPageView1.Controls.Add(Me.pageProductionCost)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.pageBatchProduction
        Me.RadPageView1.Size = New System.Drawing.Size(1036, 312)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(2), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Production"
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(3), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Production"
        '
        'pageBatchProduction
        '
        Me.pageBatchProduction.Controls.Add(Me.RadGroupBox2)
        Me.pageBatchProduction.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pageBatchProduction.ItemSize = New System.Drawing.SizeF(70.0!, 26.0!)
        Me.pageBatchProduction.Location = New System.Drawing.Point(10, 35)
        Me.pageBatchProduction.Name = "pageBatchProduction"
        Me.pageBatchProduction.Size = New System.Drawing.Size(1015, 266)
        Me.pageBatchProduction.Text = "Production"
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
        Me.RadGroupBox2.Size = New System.Drawing.Size(1015, 266)
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
        Me.gvBatch.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvBatch.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvBatch.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvBatch.MyStopExport = False
        Me.gvBatch.Name = "gvBatch"
        Me.gvBatch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvBatch.ShowGroupPanel = False
        Me.gvBatch.ShowHeaderCellButtons = True
        Me.gvBatch.Size = New System.Drawing.Size(995, 236)
        Me.gvBatch.TabIndex = 0
        Me.gvBatch.TabStop = False
        '
        'pageConsumption
        '
        Me.pageConsumption.Controls.Add(Me.RadGroupBox1)
        Me.pageConsumption.ItemSize = New System.Drawing.SizeF(83.0!, 26.0!)
        Me.pageConsumption.Location = New System.Drawing.Point(10, 35)
        Me.pageConsumption.Name = "pageConsumption"
        Me.pageConsumption.Size = New System.Drawing.Size(1015, 266)
        Me.pageConsumption.Text = "Consumption"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gvConsumption)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Consumption Item Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1015, 266)
        Me.RadGroupBox1.TabIndex = 10
        Me.RadGroupBox1.Text = "Consumption Item Details"
        '
        'gvConsumption
        '
        Me.gvConsumption.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvConsumption.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvConsumption.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvConsumption.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvConsumption.ForeColor = System.Drawing.Color.Black
        Me.gvConsumption.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvConsumption.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gvConsumption.MasterTemplate.AllowAddNewRow = False
        Me.gvConsumption.MasterTemplate.AllowDeleteRow = False
        Me.gvConsumption.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvConsumption.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvConsumption.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gvConsumption.MyStopExport = False
        Me.gvConsumption.Name = "gvConsumption"
        Me.gvConsumption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvConsumption.ShowGroupPanel = False
        Me.gvConsumption.ShowHeaderCellButtons = True
        Me.gvConsumption.Size = New System.Drawing.Size(995, 236)
        Me.gvConsumption.TabIndex = 0
        Me.gvConsumption.TabStop = False
        '
        'pageProductionCost
        '
        Me.pageProductionCost.Controls.Add(Me.RadGroupBox3)
        Me.pageProductionCost.ItemSize = New System.Drawing.SizeF(97.0!, 26.0!)
        Me.pageProductionCost.Location = New System.Drawing.Point(10, 35)
        Me.pageProductionCost.Name = "pageProductionCost"
        Me.pageProductionCost.Size = New System.Drawing.Size(1015, 266)
        Me.pageProductionCost.Text = "Production Cost"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.gvProductionCost)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox3.HeaderText = "Production Cost details"
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(1015, 266)
        Me.RadGroupBox3.TabIndex = 10
        Me.RadGroupBox3.Text = "Production Cost details"
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
        Me.gvProductionCost.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvProductionCost.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvProductionCost.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gvProductionCost.MyStopExport = False
        Me.gvProductionCost.Name = "gvProductionCost"
        Me.gvProductionCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvProductionCost.ShowGroupPanel = False
        Me.gvProductionCost.ShowHeaderCellButtons = True
        Me.gvProductionCost.Size = New System.Drawing.Size(995, 236)
        Me.gvProductionCost.TabIndex = 0
        Me.gvProductionCost.TabStop = False
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1015, 266)
        Me.RadPageViewPage1.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(1015, 266)
        Me.UcAttachment1.TabIndex = 2
        '
        'btnShowInventory
        '
        Me.btnShowInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowInventory.Location = New System.Drawing.Point(357, 8)
        Me.btnShowInventory.Name = "btnShowInventory"
        Me.btnShowInventory.Size = New System.Drawing.Size(105, 24)
        Me.btnShowInventory.TabIndex = 48
        Me.btnShowInventory.Text = "Show Inventory"
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(544, 8)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(76, 24)
        Me.RadButton1.TabIndex = 36
        Me.RadButton1.Text = "Re Create JE"
        Me.RadButton1.Visible = False
        '
        'btnGo
        '
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(11, 8)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(55, 24)
        Me.btnGo.TabIndex = 35
        Me.btnGo.Text = ">>"
        '
        'btnunpost
        '
        Me.btnunpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnunpost.Location = New System.Drawing.Point(465, 8)
        Me.btnunpost.Name = "btnunpost"
        Me.btnunpost.Size = New System.Drawing.Size(76, 24)
        Me.btnunpost.TabIndex = 34
        Me.btnunpost.Text = "Unpost"
        Me.btnunpost.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(285, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 24)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(141, 8)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 24)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(213, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 24)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(962, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 24)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(69, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 24)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1036, 20)
        Me.RadMenu1.TabIndex = 2
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Import"
        '
        'frmStanderdProductionEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1036, 525)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmStanderdProductionEntry"
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
        CType(Me.CboShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConsmLocMilkDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConsmLocOtherDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBatchNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBatchNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReceiptCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBatchDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpBatchDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.pageBatchProduction.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gvBatch.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvBatch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageConsumption.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gvConsumption.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvConsumption, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageProductionCost.ResumeLayout(False)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        CType(Me.gvProductionCost.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvProductionCost, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnunpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Friend WithEvents lblBatchNo As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnunpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents pageConsumption As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvConsumption As common.UserControls.MyRadGridView
    Friend WithEvents pageProductionCost As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvProductionCost As common.UserControls.MyRadGridView
    Friend WithEvents txtBatchNo As common.Controls.MyTextBox
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents TxtCategory As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents fndItemCategory As common.UserControls.txtFinder
    Friend WithEvents lblConsmLocMilkDesc As common.Controls.MyLabel
    Friend WithEvents txtConsmLocMilk As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblConsmLocOtherDesc As common.Controls.MyLabel
    Friend WithEvents txtConsmLocOther As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtImportTemplate As common.UserControls.txtMultiSelectFinder
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents CboShift As common.Controls.MyComboBox
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents btnShowInventory As RadButton
End Class

