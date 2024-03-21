<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProductionPlanning
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProductionPlanning))
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvPP = New common.UserControls.MyRadGridView()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtplan_to_date = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtloc_name = New common.Controls.MyLabel()
        Me.txtloc_code = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblPlannedByName = New common.Controls.MyLabel()
        Me.txtPlannedBy = New common.UserControls.txtFinder()
        Me.lblPlannedBy = New common.Controls.MyLabel()
        Me.txtComments = New common.Controls.MyTextBox()
        Me.lblComments = New common.Controls.MyLabel()
        Me.lblApprovedByName = New common.Controls.MyLabel()
        Me.lblApprovedBy = New common.Controls.MyLabel()
        Me.lblCreatedByName = New common.Controls.MyLabel()
        Me.lblCreatedBy = New common.Controls.MyLabel()
        Me.lblDesc = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.dtpPlanForDate = New common.Controls.MyDateTimePicker()
        Me.lblPlanForDate = New common.Controls.MyLabel()
        Me.dtpBOMDate = New common.Controls.MyDateTimePicker()
        Me.lblPPDate = New common.Controls.MyLabel()
        Me.mnuImportBOM = New Telerik.WinControls.UI.RadSplitButton()
        Me.mnuImportPlanHead = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuImportPlanDetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuExportBOM = New Telerik.WinControls.UI.RadSplitButton()
        Me.mnuExportPlanHead = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuExportPlanDetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gvPP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPP.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtplan_to_date, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtloc_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPlannedByName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPlannedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblComments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApprovedByName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApprovedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreatedByName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreatedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpPlanForDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPlanForDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpBOMDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPPDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mnuImportBOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mnuExportBOM, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.txtCode.Location = New System.Drawing.Point(110, 14)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(326, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(20, 40)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(52, 16)
        Me.lblCode.TabIndex = 2
        Me.lblCode.Text = "PP Code"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.lblCode)
        Me.RadGroupBox1.Controls.Add(Me.gvPP)
        Me.RadGroupBox1.Controls.Add(Me.SplitContainer1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(943, 472)
        Me.RadGroupBox1.TabIndex = 0
        '
        'gvPP
        '
        Me.gvPP.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvPP.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvPP.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvPP.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvPP.ForeColor = System.Drawing.Color.Black
        Me.gvPP.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvPP.Location = New System.Drawing.Point(13, 140)
        '
        '
        '
        Me.gvPP.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvPP.MasterTemplate.AllowAddNewRow = False
        Me.gvPP.MasterTemplate.AutoGenerateColumns = False
        Me.gvPP.MasterTemplate.EnableGrouping = False
        Me.gvPP.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvPP.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvPP.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvPP.MyStopExport = False
        Me.gvPP.Name = "gvPP"
        Me.gvPP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvPP.ShowHeaderCellButtons = True
        Me.gvPP.Size = New System.Drawing.Size(917, 277)
        Me.gvPP.TabIndex = 0
        Me.gvPP.TabStop = False
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtplan_to_date)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtloc_name)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtloc_code)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPlannedByName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPlannedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPlannedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtComments)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblComments)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblApprovedByName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblApprovedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCreatedByName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCreatedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpPlanForDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPlanForDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpBOMDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPPDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.mnuImportBOM)
        Me.SplitContainer1.Panel2.Controls.Add(Me.mnuExportBOM)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(923, 442)
        Me.SplitContainer1.SplitterDistance = 399
        Me.SplitContainer1.TabIndex = 0
        '
        'txtplan_to_date
        '
        Me.txtplan_to_date.CalculationExpression = Nothing
        Me.txtplan_to_date.CustomFormat = "dd/MM/yyyy"
        Me.txtplan_to_date.FieldCode = Nothing
        Me.txtplan_to_date.FieldDesc = Nothing
        Me.txtplan_to_date.FieldMaxLength = 0
        Me.txtplan_to_date.FieldName = Nothing
        Me.txtplan_to_date.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtplan_to_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtplan_to_date.isCalculatedField = False
        Me.txtplan_to_date.IsSourceFromTable = False
        Me.txtplan_to_date.IsSourceFromValueList = False
        Me.txtplan_to_date.IsUnique = False
        Me.txtplan_to_date.Location = New System.Drawing.Point(553, 100)
        Me.txtplan_to_date.MendatroryField = True
        Me.txtplan_to_date.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtplan_to_date.MyLinkLable1 = Me.MyLabel1
        Me.txtplan_to_date.MyLinkLable2 = Nothing
        Me.txtplan_to_date.Name = "txtplan_to_date"
        Me.txtplan_to_date.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtplan_to_date.ReferenceFieldDesc = Nothing
        Me.txtplan_to_date.ReferenceFieldName = Nothing
        Me.txtplan_to_date.ReferenceTableName = Nothing
        Me.txtplan_to_date.Size = New System.Drawing.Size(138, 18)
        Me.txtplan_to_date.TabIndex = 7
        Me.txtplan_to_date.TabStop = False
        Me.txtplan_to_date.Text = "03/05/2011"
        Me.txtplan_to_date.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        Me.txtplan_to_date.Visible = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(462, 100)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel1.TabIndex = 22
        Me.MyLabel1.Text = "Plan To Date"
        Me.MyLabel1.Visible = False
        '
        'txtloc_name
        '
        Me.txtloc_name.AutoSize = False
        Me.txtloc_name.BorderVisible = True
        Me.txtloc_name.FieldName = Nothing
        Me.txtloc_name.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtloc_name.Location = New System.Drawing.Point(258, 98)
        Me.txtloc_name.Name = "txtloc_name"
        Me.txtloc_name.Size = New System.Drawing.Size(194, 20)
        Me.txtloc_name.TabIndex = 20
        Me.txtloc_name.Visible = False
        '
        'txtloc_code
        '
        Me.txtloc_code.CalculationExpression = Nothing
        Me.txtloc_code.FieldCode = Nothing
        Me.txtloc_code.FieldDesc = Nothing
        Me.txtloc_code.FieldMaxLength = 0
        Me.txtloc_code.FieldName = Nothing
        Me.txtloc_code.isCalculatedField = False
        Me.txtloc_code.IsSourceFromTable = False
        Me.txtloc_code.IsSourceFromValueList = False
        Me.txtloc_code.IsUnique = False
        Me.txtloc_code.Location = New System.Drawing.Point(109, 99)
        Me.txtloc_code.MendatroryField = True
        Me.txtloc_code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtloc_code.MyLinkLable1 = Me.MyLabel2
        Me.txtloc_code.MyLinkLable2 = Me.txtloc_name
        Me.txtloc_code.MyReadOnly = False
        Me.txtloc_code.MyShowMasterFormButton = False
        Me.txtloc_code.Name = "txtloc_code"
        Me.txtloc_code.ReferenceFieldDesc = Nothing
        Me.txtloc_code.ReferenceFieldName = Nothing
        Me.txtloc_code.ReferenceTableName = Nothing
        Me.txtloc_code.Size = New System.Drawing.Size(148, 18)
        Me.txtloc_code.TabIndex = 5
        Me.txtloc_code.Value = ""
        Me.txtloc_code.Visible = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(12, 99)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel2.TabIndex = 19
        Me.MyLabel2.Text = "Location"
        Me.MyLabel2.Visible = False
        '
        'lblPlannedByName
        '
        Me.lblPlannedByName.AutoSize = False
        Me.lblPlannedByName.BorderVisible = True
        Me.lblPlannedByName.FieldName = Nothing
        Me.lblPlannedByName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlannedByName.Location = New System.Drawing.Point(258, 77)
        Me.lblPlannedByName.Name = "lblPlannedByName"
        Me.lblPlannedByName.Size = New System.Drawing.Size(194, 20)
        Me.lblPlannedByName.TabIndex = 8
        '
        'txtPlannedBy
        '
        Me.txtPlannedBy.CalculationExpression = Nothing
        Me.txtPlannedBy.FieldCode = Nothing
        Me.txtPlannedBy.FieldDesc = Nothing
        Me.txtPlannedBy.FieldMaxLength = 0
        Me.txtPlannedBy.FieldName = Nothing
        Me.txtPlannedBy.isCalculatedField = False
        Me.txtPlannedBy.IsSourceFromTable = False
        Me.txtPlannedBy.IsSourceFromValueList = False
        Me.txtPlannedBy.IsUnique = False
        Me.txtPlannedBy.Location = New System.Drawing.Point(109, 78)
        Me.txtPlannedBy.MendatroryField = True
        Me.txtPlannedBy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlannedBy.MyLinkLable1 = Nothing
        Me.txtPlannedBy.MyLinkLable2 = Nothing
        Me.txtPlannedBy.MyReadOnly = False
        Me.txtPlannedBy.MyShowMasterFormButton = False
        Me.txtPlannedBy.Name = "txtPlannedBy"
        Me.txtPlannedBy.ReferenceFieldDesc = Nothing
        Me.txtPlannedBy.ReferenceFieldName = Nothing
        Me.txtPlannedBy.ReferenceTableName = Nothing
        Me.txtPlannedBy.Size = New System.Drawing.Size(148, 18)
        Me.txtPlannedBy.TabIndex = 4
        Me.txtPlannedBy.Value = ""
        '
        'lblPlannedBy
        '
        Me.lblPlannedBy.FieldName = Nothing
        Me.lblPlannedBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlannedBy.Location = New System.Drawing.Point(12, 78)
        Me.lblPlannedBy.Name = "lblPlannedBy"
        Me.lblPlannedBy.Size = New System.Drawing.Size(64, 16)
        Me.lblPlannedBy.TabIndex = 7
        Me.lblPlannedBy.Text = "Planned By"
        '
        'txtComments
        '
        Me.txtComments.AutoSize = False
        Me.txtComments.CalculationExpression = Nothing
        Me.txtComments.FieldCode = Nothing
        Me.txtComments.FieldDesc = Nothing
        Me.txtComments.FieldMaxLength = 0
        Me.txtComments.FieldName = Nothing
        Me.txtComments.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComments.isCalculatedField = False
        Me.txtComments.IsSourceFromTable = False
        Me.txtComments.IsSourceFromValueList = False
        Me.txtComments.IsUnique = False
        Me.txtComments.Location = New System.Drawing.Point(553, 54)
        Me.txtComments.MaxLength = 49
        Me.txtComments.MendatroryField = False
        Me.txtComments.Multiline = True
        Me.txtComments.MyLinkLable1 = Me.lblComments
        Me.txtComments.MyLinkLable2 = Nothing
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ReferenceFieldDesc = Nothing
        Me.txtComments.ReferenceFieldName = Nothing
        Me.txtComments.ReferenceTableName = Nothing
        Me.txtComments.Size = New System.Drawing.Size(366, 43)
        Me.txtComments.TabIndex = 6
        '
        'lblComments
        '
        Me.lblComments.FieldName = Nothing
        Me.lblComments.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComments.Location = New System.Drawing.Point(462, 58)
        Me.lblComments.Name = "lblComments"
        Me.lblComments.Size = New System.Drawing.Size(61, 16)
        Me.lblComments.TabIndex = 10
        Me.lblComments.Text = "Comments"
        '
        'lblApprovedByName
        '
        Me.lblApprovedByName.AutoSize = False
        Me.lblApprovedByName.BorderVisible = True
        Me.lblApprovedByName.FieldName = Nothing
        Me.lblApprovedByName.Location = New System.Drawing.Point(553, 34)
        Me.lblApprovedByName.Name = "lblApprovedByName"
        Me.lblApprovedByName.Size = New System.Drawing.Size(262, 19)
        Me.lblApprovedByName.TabIndex = 14
        '
        'lblApprovedBy
        '
        Me.lblApprovedBy.FieldName = Nothing
        Me.lblApprovedBy.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblApprovedBy.Location = New System.Drawing.Point(462, 37)
        Me.lblApprovedBy.Name = "lblApprovedBy"
        Me.lblApprovedBy.Size = New System.Drawing.Size(71, 16)
        Me.lblApprovedBy.TabIndex = 13
        Me.lblApprovedBy.Text = "Approved By"
        '
        'lblCreatedByName
        '
        Me.lblCreatedByName.AutoSize = False
        Me.lblCreatedByName.BorderVisible = True
        Me.lblCreatedByName.FieldName = Nothing
        Me.lblCreatedByName.Location = New System.Drawing.Point(553, 14)
        Me.lblCreatedByName.Name = "lblCreatedByName"
        Me.lblCreatedByName.Size = New System.Drawing.Size(262, 19)
        Me.lblCreatedByName.TabIndex = 15
        '
        'lblCreatedBy
        '
        Me.lblCreatedBy.FieldName = Nothing
        Me.lblCreatedBy.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCreatedBy.Location = New System.Drawing.Point(462, 14)
        Me.lblCreatedBy.Name = "lblCreatedBy"
        Me.lblCreatedBy.Size = New System.Drawing.Size(63, 16)
        Me.lblCreatedBy.TabIndex = 16
        Me.lblCreatedBy.Text = "Created By"
        '
        'lblDesc
        '
        Me.lblDesc.FieldName = Nothing
        Me.lblDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesc.Location = New System.Drawing.Point(10, 38)
        Me.lblDesc.Name = "lblDesc"
        Me.lblDesc.Size = New System.Drawing.Size(63, 16)
        Me.lblDesc.TabIndex = 12
        Me.lblDesc.Text = "Description"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(821, 15)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 17
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
        Me.txtDescription.Location = New System.Drawing.Point(110, 36)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.MendatroryField = True
        Me.txtDescription.MyLinkLable1 = Me.lblDesc
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(342, 20)
        Me.txtDescription.TabIndex = 1
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(438, 15)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = " "
        '
        'dtpPlanForDate
        '
        Me.dtpPlanForDate.CalculationExpression = Nothing
        Me.dtpPlanForDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpPlanForDate.FieldCode = Nothing
        Me.dtpPlanForDate.FieldDesc = Nothing
        Me.dtpPlanForDate.FieldMaxLength = 0
        Me.dtpPlanForDate.FieldName = Nothing
        Me.dtpPlanForDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpPlanForDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPlanForDate.isCalculatedField = False
        Me.dtpPlanForDate.IsSourceFromTable = False
        Me.dtpPlanForDate.IsSourceFromValueList = False
        Me.dtpPlanForDate.IsUnique = False
        Me.dtpPlanForDate.Location = New System.Drawing.Point(314, 58)
        Me.dtpPlanForDate.MendatroryField = True
        Me.dtpPlanForDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPlanForDate.MyLinkLable1 = Me.lblPlanForDate
        Me.dtpPlanForDate.MyLinkLable2 = Nothing
        Me.dtpPlanForDate.Name = "dtpPlanForDate"
        Me.dtpPlanForDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPlanForDate.ReferenceFieldDesc = Nothing
        Me.dtpPlanForDate.ReferenceFieldName = Nothing
        Me.dtpPlanForDate.ReferenceTableName = Nothing
        Me.dtpPlanForDate.Size = New System.Drawing.Size(138, 18)
        Me.dtpPlanForDate.TabIndex = 3
        Me.dtpPlanForDate.TabStop = False
        Me.dtpPlanForDate.Text = "03/05/2011"
        Me.dtpPlanForDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblPlanForDate
        '
        Me.lblPlanForDate.FieldName = Nothing
        Me.lblPlanForDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlanForDate.Location = New System.Drawing.Point(259, 60)
        Me.lblPlanForDate.Name = "lblPlanForDate"
        Me.lblPlanForDate.Size = New System.Drawing.Size(49, 16)
        Me.lblPlanForDate.TabIndex = 9
        Me.lblPlanForDate.Text = "Plan For"
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
        Me.dtpBOMDate.Location = New System.Drawing.Point(110, 58)
        Me.dtpBOMDate.MendatroryField = True
        Me.dtpBOMDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBOMDate.MyLinkLable1 = Me.lblPPDate
        Me.dtpBOMDate.MyLinkLable2 = Nothing
        Me.dtpBOMDate.Name = "dtpBOMDate"
        Me.dtpBOMDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBOMDate.ReferenceFieldDesc = Nothing
        Me.dtpBOMDate.ReferenceFieldName = Nothing
        Me.dtpBOMDate.ReferenceTableName = Nothing
        Me.dtpBOMDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpBOMDate.TabIndex = 2
        Me.dtpBOMDate.TabStop = False
        Me.dtpBOMDate.Text = "03/05/2011"
        Me.dtpBOMDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblPPDate
        '
        Me.lblPPDate.FieldName = Nothing
        Me.lblPPDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPPDate.Location = New System.Drawing.Point(12, 60)
        Me.lblPPDate.Name = "lblPPDate"
        Me.lblPPDate.Size = New System.Drawing.Size(77, 16)
        Me.lblPPDate.TabIndex = 11
        Me.lblPPDate.Text = "Planning Date"
        '
        'mnuImportBOM
        '
        Me.mnuImportBOM.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.mnuImportBOM.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuImportPlanHead, Me.mnuImportPlanDetail})
        Me.mnuImportBOM.Location = New System.Drawing.Point(438, 6)
        Me.mnuImportBOM.Name = "mnuImportBOM"
        Me.mnuImportBOM.Size = New System.Drawing.Size(110, 22)
        Me.mnuImportBOM.TabIndex = 11
        Me.mnuImportBOM.Text = "Import Plan"
        '
        'mnuImportPlanHead
        '
        Me.mnuImportPlanHead.Name = "mnuImportPlanHead"
        Me.mnuImportPlanHead.Text = "Plan Head"
        '
        'mnuImportPlanDetail
        '
        Me.mnuImportPlanDetail.Name = "mnuImportPlanDetail"
        Me.mnuImportPlanDetail.Text = "Plan Detail"
        '
        'mnuExportBOM
        '
        Me.mnuExportBOM.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.mnuExportBOM.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuExportPlanHead, Me.mnuExportPlanDetail})
        Me.mnuExportBOM.Location = New System.Drawing.Point(314, 6)
        Me.mnuExportBOM.Name = "mnuExportBOM"
        Me.mnuExportBOM.Size = New System.Drawing.Size(110, 22)
        Me.mnuExportBOM.TabIndex = 10
        Me.mnuExportBOM.Text = "Export Plan"
        '
        'mnuExportPlanHead
        '
        Me.mnuExportPlanHead.Name = "mnuExportPlanHead"
        Me.mnuExportPlanHead.Text = "Plan Head"
        '
        'mnuExportPlanDetail
        '
        Me.mnuExportPlanDetail.Name = "mnuExportPlanDetail"
        Me.mnuExportPlanDetail.Text = "Plan Detail"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(228, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(80, 6)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(8, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(851, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(154, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(68, 22)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'frmProductionPlanning
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(943, 472)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "frmProductionPlanning"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Production Planning"
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.gvPP.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtplan_to_date, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtloc_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPlannedByName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPlannedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblComments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApprovedByName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApprovedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreatedByName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreatedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpPlanForDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPlanForDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpBOMDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPPDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mnuImportBOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mnuExportBOM, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents lblPPDate As common.Controls.MyLabel
    Friend WithEvents dtpBOMDate As common.Controls.MyDateTimePicker
    Friend WithEvents gvPP As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpPlanForDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblPlanForDate As common.Controls.MyLabel
    Friend WithEvents lblDesc As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblApprovedByName As common.Controls.MyLabel
    Friend WithEvents lblApprovedBy As common.Controls.MyLabel
    Friend WithEvents lblCreatedByName As common.Controls.MyLabel
    Friend WithEvents lblCreatedBy As common.Controls.MyLabel
    Friend WithEvents txtComments As common.Controls.MyTextBox
    Friend WithEvents lblComments As common.Controls.MyLabel
    Friend WithEvents lblPlannedByName As common.Controls.MyLabel
    Friend WithEvents txtPlannedBy As common.UserControls.txtFinder
    Friend WithEvents lblPlannedBy As common.Controls.MyLabel
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtloc_name As common.Controls.MyLabel
    Friend WithEvents txtloc_code As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtplan_to_date As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents mnuImportBOM As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents mnuExportBOM As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents mnuExportPlanHead As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuExportPlanDetail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuImportPlanHead As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuImportPlanDetail As Telerik.WinControls.UI.RadMenuItem
End Class
