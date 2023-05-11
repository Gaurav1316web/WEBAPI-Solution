Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAllowanceDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAllowanceDetails))
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnUnSelect = New Telerik.WinControls.UI.RadButton()
        Me.txtGo = New Telerik.WinControls.UI.RadButton()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtBranch = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.lblAllowanceByName = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.lblPayPeriodName = New common.Controls.MyLabel()
        Me.findPayperiod = New common.UserControls.txtFinder()
        Me.lblPayPeriodCode = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblRemarks = New common.Controls.MyLabel()
        Me.lblAllowanceBy = New common.Controls.MyLabel()
        Me.findAllowancegiveby = New common.UserControls.txtFinder()
        Me.lblAllowanceDate = New common.Controls.MyLabel()
        Me.dtpAllowanceDate = New common.Controls.MyDateTimePicker()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.gvAllowance = New common.UserControls.MyRadGridView()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblTotRAmt1 = New common.Controls.MyLabel()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnUnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAllowanceByName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriodName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriodCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAllowanceBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAllowanceDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpAllowanceDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAllowance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAllowance.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(718, 20)
        Me.RadMenu2.TabIndex = 15
        Me.RadMenu2.Text = "RadMenu2"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "File"
        Me.RadMenuItem3.AccessibleName = "File"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem4, Me.RadMenuItem1})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "Export Blank Sheet"
        Me.RadMenuItem4.AccessibleName = "Export Blank Sheet"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Export Blank Sheet"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Import"
        Me.RadMenuItem1.AccessibleName = "Import"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Import"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnUnSelect)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtGo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBranch)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAllowanceByName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriodName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findPayperiod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriodCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAllowanceBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findAllowancegiveby)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAllowanceDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpAllowanceDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gvAllowance)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblTotRAmt1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(718, 507)
        Me.SplitContainer1.SplitterDistance = 466
        Me.SplitContainer1.TabIndex = 0
        '
        'btnUnSelect
        '
        Me.btnUnSelect.Location = New System.Drawing.Point(448, 145)
        Me.btnUnSelect.Name = "btnUnSelect"
        Me.btnUnSelect.Size = New System.Drawing.Size(80, 23)
        Me.btnUnSelect.TabIndex = 261
        Me.btnUnSelect.Text = "Select All"
        '
        'txtGo
        '
        Me.txtGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGo.Location = New System.Drawing.Point(626, 190)
        Me.txtGo.Name = "txtGo"
        Me.txtGo.Size = New System.Drawing.Size(50, 18)
        Me.txtGo.TabIndex = 260
        Me.txtGo.Text = ">>"
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.FieldName = Nothing
        Me.lblLocationDesc.Location = New System.Drawing.Point(378, 50)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(298, 19)
        Me.lblLocationDesc.TabIndex = 259
        Me.lblLocationDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(12, 50)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel9.TabIndex = 258
        Me.MyLabel9.Text = "Location"
        '
        'txtBranch
        '
        Me.txtBranch.CalculationExpression = Nothing
        Me.txtBranch.FieldCode = Nothing
        Me.txtBranch.FieldDesc = Nothing
        Me.txtBranch.FieldMaxLength = 0
        Me.txtBranch.FieldName = Nothing
        Me.txtBranch.isCalculatedField = False
        Me.txtBranch.IsSourceFromTable = False
        Me.txtBranch.IsSourceFromValueList = False
        Me.txtBranch.IsUnique = False
        Me.txtBranch.Location = New System.Drawing.Point(129, 50)
        Me.txtBranch.MendatroryField = True
        Me.txtBranch.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBranch.MyLinkLable1 = Me.MyLabel9
        Me.txtBranch.MyLinkLable2 = Nothing
        Me.txtBranch.MyReadOnly = False
        Me.txtBranch.MyShowMasterFormButton = False
        Me.txtBranch.Name = "txtBranch"
        Me.txtBranch.ReferenceFieldDesc = Nothing
        Me.txtBranch.ReferenceFieldName = Nothing
        Me.txtBranch.ReferenceTableName = Nothing
        Me.txtBranch.Size = New System.Drawing.Size(246, 19)
        Me.txtBranch.TabIndex = 257
        Me.txtBranch.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(477, 27)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 18)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 212
        '
        'lblAllowanceByName
        '
        Me.lblAllowanceByName.AutoSize = False
        Me.lblAllowanceByName.BorderVisible = True
        Me.lblAllowanceByName.FieldName = Nothing
        Me.lblAllowanceByName.Location = New System.Drawing.Point(378, 120)
        Me.lblAllowanceByName.Name = "lblAllowanceByName"
        Me.lblAllowanceByName.Size = New System.Drawing.Size(298, 19)
        Me.lblAllowanceByName.TabIndex = 203
        Me.lblAllowanceByName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(353, 25)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 202
        Me.btnNew.Text = " "
        '
        'lblPayPeriodName
        '
        Me.lblPayPeriodName.AutoSize = False
        Me.lblPayPeriodName.BorderVisible = True
        Me.lblPayPeriodName.FieldName = Nothing
        Me.lblPayPeriodName.Location = New System.Drawing.Point(378, 73)
        Me.lblPayPeriodName.Name = "lblPayPeriodName"
        Me.lblPayPeriodName.Size = New System.Drawing.Size(298, 19)
        Me.lblPayPeriodName.TabIndex = 200
        Me.lblPayPeriodName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'findPayperiod
        '
        Me.findPayperiod.CalculationExpression = Nothing
        Me.findPayperiod.FieldCode = Nothing
        Me.findPayperiod.FieldDesc = Nothing
        Me.findPayperiod.FieldMaxLength = 0
        Me.findPayperiod.FieldName = Nothing
        Me.findPayperiod.isCalculatedField = False
        Me.findPayperiod.IsSourceFromTable = False
        Me.findPayperiod.IsSourceFromValueList = False
        Me.findPayperiod.IsUnique = False
        Me.findPayperiod.Location = New System.Drawing.Point(129, 73)
        Me.findPayperiod.MendatroryField = True
        Me.findPayperiod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findPayperiod.MyLinkLable1 = Me.lblPayPeriodCode
        Me.findPayperiod.MyLinkLable2 = Nothing
        Me.findPayperiod.MyReadOnly = False
        Me.findPayperiod.MyShowMasterFormButton = False
        Me.findPayperiod.Name = "findPayperiod"
        Me.findPayperiod.ReferenceFieldDesc = Nothing
        Me.findPayperiod.ReferenceFieldName = Nothing
        Me.findPayperiod.ReferenceTableName = Nothing
        Me.findPayperiod.Size = New System.Drawing.Size(246, 19)
        Me.findPayperiod.TabIndex = 2
        Me.findPayperiod.Value = ""
        '
        'lblPayPeriodCode
        '
        Me.lblPayPeriodCode.FieldName = Nothing
        Me.lblPayPeriodCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayPeriodCode.Location = New System.Drawing.Point(12, 73)
        Me.lblPayPeriodCode.Name = "lblPayPeriodCode"
        Me.lblPayPeriodCode.Size = New System.Drawing.Size(92, 16)
        Me.lblPayPeriodCode.TabIndex = 198
        Me.lblPayPeriodCode.Text = "Pay Period Code"
        '
        'txtDescription
        '
        Me.txtDescription.AutoSize = False
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(129, 145)
        Me.txtDescription.MaxLength = 49
        Me.txtDescription.MendatroryField = True
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.lblRemarks
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(313, 63)
        Me.txtDescription.TabIndex = 5
        '
        'lblRemarks
        '
        Me.lblRemarks.FieldName = Nothing
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(12, 143)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(51, 16)
        Me.lblRemarks.TabIndex = 177
        Me.lblRemarks.Text = "Remarks"
        '
        'lblAllowanceBy
        '
        Me.lblAllowanceBy.FieldName = Nothing
        Me.lblAllowanceBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAllowanceBy.Location = New System.Drawing.Point(12, 121)
        Me.lblAllowanceBy.Name = "lblAllowanceBy"
        Me.lblAllowanceBy.Size = New System.Drawing.Size(104, 16)
        Me.lblAllowanceBy.TabIndex = 165
        Me.lblAllowanceBy.Text = "Allowance given By"
        '
        'findAllowancegiveby
        '
        Me.findAllowancegiveby.CalculationExpression = Nothing
        Me.findAllowancegiveby.FieldCode = Nothing
        Me.findAllowancegiveby.FieldDesc = Nothing
        Me.findAllowancegiveby.FieldMaxLength = 0
        Me.findAllowancegiveby.FieldName = Nothing
        Me.findAllowancegiveby.isCalculatedField = False
        Me.findAllowancegiveby.IsSourceFromTable = False
        Me.findAllowancegiveby.IsSourceFromValueList = False
        Me.findAllowancegiveby.IsUnique = False
        Me.findAllowancegiveby.Location = New System.Drawing.Point(129, 121)
        Me.findAllowancegiveby.MendatroryField = True
        Me.findAllowancegiveby.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findAllowancegiveby.MyLinkLable1 = Me.lblAllowanceBy
        Me.findAllowancegiveby.MyLinkLable2 = Nothing
        Me.findAllowancegiveby.MyReadOnly = False
        Me.findAllowancegiveby.MyShowMasterFormButton = False
        Me.findAllowancegiveby.Name = "findAllowancegiveby"
        Me.findAllowancegiveby.ReferenceFieldDesc = Nothing
        Me.findAllowancegiveby.ReferenceFieldName = Nothing
        Me.findAllowancegiveby.ReferenceTableName = Nothing
        Me.findAllowancegiveby.Size = New System.Drawing.Size(246, 19)
        Me.findAllowancegiveby.TabIndex = 4
        Me.findAllowancegiveby.Value = ""
        '
        'lblAllowanceDate
        '
        Me.lblAllowanceDate.FieldName = Nothing
        Me.lblAllowanceDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAllowanceDate.Location = New System.Drawing.Point(12, 97)
        Me.lblAllowanceDate.Name = "lblAllowanceDate"
        Me.lblAllowanceDate.Size = New System.Drawing.Size(85, 16)
        Me.lblAllowanceDate.TabIndex = 164
        Me.lblAllowanceDate.Text = "Allowance Date"
        '
        'dtpAllowanceDate
        '
        Me.dtpAllowanceDate.CalculationExpression = Nothing
        Me.dtpAllowanceDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpAllowanceDate.FieldCode = Nothing
        Me.dtpAllowanceDate.FieldDesc = Nothing
        Me.dtpAllowanceDate.FieldMaxLength = 0
        Me.dtpAllowanceDate.FieldName = Nothing
        Me.dtpAllowanceDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpAllowanceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAllowanceDate.isCalculatedField = False
        Me.dtpAllowanceDate.IsSourceFromTable = False
        Me.dtpAllowanceDate.IsSourceFromValueList = False
        Me.dtpAllowanceDate.IsUnique = False
        Me.dtpAllowanceDate.Location = New System.Drawing.Point(129, 98)
        Me.dtpAllowanceDate.MendatroryField = True
        Me.dtpAllowanceDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAllowanceDate.MyLinkLable1 = Me.lblAllowanceDate
        Me.dtpAllowanceDate.MyLinkLable2 = Nothing
        Me.dtpAllowanceDate.Name = "dtpAllowanceDate"
        Me.dtpAllowanceDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAllowanceDate.ReferenceFieldDesc = Nothing
        Me.dtpAllowanceDate.ReferenceFieldName = Nothing
        Me.dtpAllowanceDate.ReferenceTableName = Nothing
        Me.dtpAllowanceDate.Size = New System.Drawing.Size(130, 18)
        Me.dtpAllowanceDate.TabIndex = 3
        Me.dtpAllowanceDate.TabStop = False
        Me.dtpAllowanceDate.Text = "03/05/2011"
        Me.dtpAllowanceDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(129, 24)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(221, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(12, 29)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(88, 16)
        Me.lblCode.TabIndex = 161
        Me.lblCode.Text = "Allowance Code"
        '
        'gvAllowance
        '
        Me.gvAllowance.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvAllowance.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvAllowance.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvAllowance.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvAllowance.ForeColor = System.Drawing.Color.Black
        Me.gvAllowance.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvAllowance.Location = New System.Drawing.Point(9, 214)
        '
        'gvAllowance
        '
        Me.gvAllowance.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvAllowance.MasterTemplate.AllowAddNewRow = False
        Me.gvAllowance.MasterTemplate.AutoGenerateColumns = False
        Me.gvAllowance.MasterTemplate.EnableGrouping = False
        Me.gvAllowance.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvAllowance.Name = "gvAllowance"
        Me.gvAllowance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvAllowance.ShowHeaderCellButtons = True
        Me.gvAllowance.Size = New System.Drawing.Size(699, 249)
        Me.gvAllowance.TabIndex = 6
        Me.gvAllowance.Text = "RadGridView4"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(78, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 18)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(642, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(147, 8)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'MyLabel5
        '
        Me.MyLabel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(399, 10)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(118, 16)
        Me.MyLabel5.TabIndex = 127
        Me.MyLabel5.Text = "Total Allowance Amt"
        '
        'lblTotRAmt1
        '
        Me.lblTotRAmt1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotRAmt1.AutoSize = False
        Me.lblTotRAmt1.BorderVisible = True
        Me.lblTotRAmt1.FieldName = Nothing
        Me.lblTotRAmt1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt1.Location = New System.Drawing.Point(521, 8)
        Me.lblTotRAmt1.Name = "lblTotRAmt1"
        Me.lblTotRAmt1.Size = New System.Drawing.Size(115, 20)
        Me.lblTotRAmt1.TabIndex = 137
        Me.lblTotRAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmAllowanceDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(718, 507)
        Me.Controls.Add(Me.RadMenu2)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmAllowanceDetails"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Allowance Details"
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnUnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAllowanceByName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriodName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriodCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAllowanceBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAllowanceDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpAllowanceDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAllowance.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAllowance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblRemarks As common.Controls.MyLabel
    Friend WithEvents lblAllowanceBy As common.Controls.MyLabel
    Friend WithEvents findAllowancegiveby As common.UserControls.txtFinder
    Friend WithEvents lblAllowanceDate As common.Controls.MyLabel
    Friend WithEvents dtpAllowanceDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents gvAllowance As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblPayPeriodName As common.Controls.MyLabel
    Friend WithEvents findPayperiod As common.UserControls.txtFinder
    Friend WithEvents lblPayPeriodCode As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblAllowanceByName As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtBranch As common.UserControls.txtFinder
    Friend WithEvents txtGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnUnSelect As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt1 As common.Controls.MyLabel
End Class
