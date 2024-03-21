Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmInvestmentDeclaration
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
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.cmbStatus = New common.Controls.MyComboBox()
        Me.lblStatus = New common.Controls.MyLabel()
        Me.TxtActualAmt = New common.MyNumBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.TxtProvAmt = New common.MyNumBox()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtFinYear = New common.UserControls.txtFinder()
        Me.TxtEmpCode = New common.UserControls.txtFinder()
        Me.TxtInvType = New common.UserControls.txtFinder()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.txtcode = New common.UserControls.txtNavigator()
        Me.lblEmpName = New common.Controls.MyLabel()
        Me.lblInvesTypeName = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel84 = New common.Controls.MyLabel()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.TxtDesp = New common.Controls.MyTextBox()
        Me.lblItemCategoryCode = New common.Controls.MyLabel()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.cmbStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtActualAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtProvAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInvesTypeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel84, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDesp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(678, 20)
        Me.RadMenu2.TabIndex = 320
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExport, Me.rmImport})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'rmExport
        '
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export"
        '
        'rmImport
        '
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(678, 272)
        Me.SplitContainer1.SplitterDistance = 225
        Me.SplitContainer1.TabIndex = 321
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(678, 225)
        Me.RadPageView1.TabIndex = 320
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.cmbStatus)
        Me.RadPageViewPage1.Controls.Add(Me.lblStatus)
        Me.RadPageViewPage1.Controls.Add(Me.TxtActualAmt)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.TxtProvAmt)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtFinYear)
        Me.RadPageViewPage1.Controls.Add(Me.TxtEmpCode)
        Me.RadPageViewPage1.Controls.Add(Me.TxtInvType)
        Me.RadPageViewPage1.Controls.Add(Me.btnreset)
        Me.RadPageViewPage1.Controls.Add(Me.txtcode)
        Me.RadPageViewPage1.Controls.Add(Me.lblEmpName)
        Me.RadPageViewPage1.Controls.Add(Me.lblInvesTypeName)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel84)
        Me.RadPageViewPage1.Controls.Add(Me.lblDescription)
        Me.RadPageViewPage1.Controls.Add(Me.TxtDesp)
        Me.RadPageViewPage1.Controls.Add(Me.lblItemCategoryCode)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(51.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(657, 177)
        Me.RadPageViewPage1.Text = "Master"
        '
        'cmbStatus
        '
        Me.cmbStatus.AutoCompleteDisplayMember = Nothing
        Me.cmbStatus.AutoCompleteValueMember = Nothing
        Me.cmbStatus.CalculationExpression = Nothing
        Me.cmbStatus.DropDownAnimationEnabled = True
        Me.cmbStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbStatus.FieldCode = Nothing
        Me.cmbStatus.FieldDesc = Nothing
        Me.cmbStatus.FieldMaxLength = 0
        Me.cmbStatus.FieldName = Nothing
        Me.cmbStatus.isCalculatedField = False
        Me.cmbStatus.IsSourceFromTable = False
        Me.cmbStatus.IsSourceFromValueList = False
        Me.cmbStatus.IsUnique = False
        Me.cmbStatus.Location = New System.Drawing.Point(140, 144)
        Me.cmbStatus.MendatroryField = True
        Me.cmbStatus.MyLinkLable1 = Me.lblStatus
        Me.cmbStatus.MyLinkLable2 = Nothing
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.ReferenceFieldDesc = Nothing
        Me.cmbStatus.ReferenceFieldName = Nothing
        Me.cmbStatus.ReferenceTableName = Nothing
        Me.cmbStatus.Size = New System.Drawing.Size(186, 20)
        Me.cmbStatus.TabIndex = 310
        '
        'lblStatus
        '
        Me.lblStatus.FieldName = Nothing
        Me.lblStatus.Location = New System.Drawing.Point(9, 146)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(40, 18)
        Me.lblStatus.TabIndex = 311
        Me.lblStatus.Text = "Status "
        '
        'TxtActualAmt
        '
        Me.TxtActualAmt.BackColor = System.Drawing.Color.White
        Me.TxtActualAmt.CalculationExpression = Nothing
        Me.TxtActualAmt.DecimalPlaces = 2
        Me.TxtActualAmt.FieldCode = Nothing
        Me.TxtActualAmt.FieldDesc = Nothing
        Me.TxtActualAmt.FieldMaxLength = 0
        Me.TxtActualAmt.FieldName = Nothing
        Me.TxtActualAmt.isCalculatedField = False
        Me.TxtActualAmt.IsSourceFromTable = False
        Me.TxtActualAmt.IsSourceFromValueList = False
        Me.TxtActualAmt.IsUnique = False
        Me.TxtActualAmt.Location = New System.Drawing.Point(417, 120)
        Me.TxtActualAmt.MaxLength = 10
        Me.TxtActualAmt.MendatroryField = False
        Me.TxtActualAmt.MyLinkLable1 = Me.MyLabel3
        Me.TxtActualAmt.MyLinkLable2 = Nothing
        Me.TxtActualAmt.Name = "TxtActualAmt"
        Me.TxtActualAmt.ReferenceFieldDesc = Nothing
        Me.TxtActualAmt.ReferenceFieldName = Nothing
        Me.TxtActualAmt.ReferenceTableName = Nothing
        Me.TxtActualAmt.Size = New System.Drawing.Size(102, 20)
        Me.TxtActualAmt.TabIndex = 308
        Me.TxtActualAmt.Text = "0"
        Me.TxtActualAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtActualAmt.Value = 0R
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(332, 121)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(84, 18)
        Me.MyLabel3.TabIndex = 309
        Me.MyLabel3.Text = "Actual Amount "
        '
        'TxtProvAmt
        '
        Me.TxtProvAmt.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtProvAmt.CalculationExpression = Nothing
        Me.TxtProvAmt.DecimalPlaces = 2
        Me.TxtProvAmt.FieldCode = Nothing
        Me.TxtProvAmt.FieldDesc = Nothing
        Me.TxtProvAmt.FieldMaxLength = 0
        Me.TxtProvAmt.FieldName = Nothing
        Me.TxtProvAmt.isCalculatedField = False
        Me.TxtProvAmt.IsSourceFromTable = False
        Me.TxtProvAmt.IsSourceFromValueList = False
        Me.TxtProvAmt.IsUnique = False
        Me.TxtProvAmt.Location = New System.Drawing.Point(141, 121)
        Me.TxtProvAmt.MaxLength = 10
        Me.TxtProvAmt.MendatroryField = True
        Me.TxtProvAmt.MyLinkLable1 = Me.RadLabel8
        Me.TxtProvAmt.MyLinkLable2 = Nothing
        Me.TxtProvAmt.Name = "TxtProvAmt"
        Me.TxtProvAmt.ReferenceFieldDesc = Nothing
        Me.TxtProvAmt.ReferenceFieldName = Nothing
        Me.TxtProvAmt.ReferenceTableName = Nothing
        Me.TxtProvAmt.Size = New System.Drawing.Size(185, 20)
        Me.TxtProvAmt.TabIndex = 306
        Me.TxtProvAmt.Text = "0"
        Me.TxtProvAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtProvAmt.Value = 0R
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Location = New System.Drawing.Point(9, 122)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(107, 18)
        Me.RadLabel8.TabIndex = 307
        Me.RadLabel8.Text = "Provisional Amount "
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(9, 76)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(123, 16)
        Me.MyLabel1.TabIndex = 305
        Me.MyLabel1.Text = "Investment Type Code "
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(454, 6)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 304
        '
        'txtFinYear
        '
        Me.txtFinYear.CalculationExpression = Nothing
        Me.txtFinYear.FieldCode = Nothing
        Me.txtFinYear.FieldDesc = Nothing
        Me.txtFinYear.FieldMaxLength = 0
        Me.txtFinYear.FieldName = Nothing
        Me.txtFinYear.isCalculatedField = False
        Me.txtFinYear.IsSourceFromTable = False
        Me.txtFinYear.IsSourceFromValueList = False
        Me.txtFinYear.IsUnique = False
        Me.txtFinYear.Location = New System.Drawing.Point(140, 51)
        Me.txtFinYear.MendatroryField = True
        Me.txtFinYear.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFinYear.MyLinkLable1 = Nothing
        Me.txtFinYear.MyLinkLable2 = Nothing
        Me.txtFinYear.MyReadOnly = False
        Me.txtFinYear.MyShowMasterFormButton = False
        Me.txtFinYear.Name = "txtFinYear"
        Me.txtFinYear.ReferenceFieldDesc = Nothing
        Me.txtFinYear.ReferenceFieldName = Nothing
        Me.txtFinYear.ReferenceTableName = Nothing
        Me.txtFinYear.Size = New System.Drawing.Size(186, 20)
        Me.txtFinYear.TabIndex = 303
        Me.txtFinYear.Value = ""
        '
        'TxtEmpCode
        '
        Me.TxtEmpCode.CalculationExpression = Nothing
        Me.TxtEmpCode.FieldCode = Nothing
        Me.TxtEmpCode.FieldDesc = Nothing
        Me.TxtEmpCode.FieldMaxLength = 0
        Me.TxtEmpCode.FieldName = Nothing
        Me.TxtEmpCode.isCalculatedField = False
        Me.TxtEmpCode.IsSourceFromTable = False
        Me.TxtEmpCode.IsSourceFromValueList = False
        Me.TxtEmpCode.IsUnique = False
        Me.TxtEmpCode.Location = New System.Drawing.Point(140, 98)
        Me.TxtEmpCode.MendatroryField = True
        Me.TxtEmpCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEmpCode.MyLinkLable1 = Nothing
        Me.TxtEmpCode.MyLinkLable2 = Nothing
        Me.TxtEmpCode.MyReadOnly = False
        Me.TxtEmpCode.MyShowMasterFormButton = False
        Me.TxtEmpCode.Name = "TxtEmpCode"
        Me.TxtEmpCode.ReferenceFieldDesc = Nothing
        Me.TxtEmpCode.ReferenceFieldName = Nothing
        Me.TxtEmpCode.ReferenceTableName = Nothing
        Me.TxtEmpCode.Size = New System.Drawing.Size(186, 20)
        Me.TxtEmpCode.TabIndex = 302
        Me.TxtEmpCode.Value = ""
        '
        'TxtInvType
        '
        Me.TxtInvType.CalculationExpression = Nothing
        Me.TxtInvType.FieldCode = Nothing
        Me.TxtInvType.FieldDesc = Nothing
        Me.TxtInvType.FieldMaxLength = 0
        Me.TxtInvType.FieldName = Nothing
        Me.TxtInvType.isCalculatedField = False
        Me.TxtInvType.IsSourceFromTable = False
        Me.TxtInvType.IsSourceFromValueList = False
        Me.TxtInvType.IsUnique = False
        Me.TxtInvType.Location = New System.Drawing.Point(140, 74)
        Me.TxtInvType.MendatroryField = True
        Me.TxtInvType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtInvType.MyLinkLable1 = Nothing
        Me.TxtInvType.MyLinkLable2 = Nothing
        Me.TxtInvType.MyReadOnly = False
        Me.TxtInvType.MyShowMasterFormButton = False
        Me.TxtInvType.Name = "TxtInvType"
        Me.TxtInvType.ReferenceFieldDesc = Nothing
        Me.TxtInvType.ReferenceFieldName = Nothing
        Me.TxtInvType.ReferenceTableName = Nothing
        Me.TxtInvType.Size = New System.Drawing.Size(186, 20)
        Me.TxtInvType.TabIndex = 301
        Me.TxtInvType.Value = ""
        '
        'btnreset
        '
        Me.btnreset.Image = Global.XpertERPTDS.My.Resources.Resources._new
        Me.btnreset.Location = New System.Drawing.Point(414, 6)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(15, 21)
        Me.btnreset.TabIndex = 234
        '
        'txtcode
        '
        Me.txtcode.FieldName = Nothing
        Me.txtcode.Location = New System.Drawing.Point(140, 6)
        Me.txtcode.MendatroryField = True
        Me.txtcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcode.MyLinkLable1 = Nothing
        Me.txtcode.MyLinkLable2 = Nothing
        Me.txtcode.MyMaxLength = 30
        Me.txtcode.MyReadOnly = False
        Me.txtcode.Name = "txtcode"
        Me.txtcode.Size = New System.Drawing.Size(274, 21)
        Me.txtcode.TabIndex = 233
        Me.txtcode.Value = ""
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.FieldName = Nothing
        Me.lblEmpName.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblEmpName.Location = New System.Drawing.Point(331, 98)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(188, 20)
        Me.lblEmpName.TabIndex = 232
        '
        'lblInvesTypeName
        '
        Me.lblInvesTypeName.AutoSize = False
        Me.lblInvesTypeName.BorderVisible = True
        Me.lblInvesTypeName.FieldName = Nothing
        Me.lblInvesTypeName.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblInvesTypeName.Location = New System.Drawing.Point(331, 76)
        Me.lblInvesTypeName.Name = "lblInvesTypeName"
        Me.lblInvesTypeName.Size = New System.Drawing.Size(188, 20)
        Me.lblInvesTypeName.TabIndex = 231
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(9, 100)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(90, 16)
        Me.MyLabel2.TabIndex = 227
        Me.MyLabel2.Text = "Employee Code "
        '
        'MyLabel84
        '
        Me.MyLabel84.FieldName = Nothing
        Me.MyLabel84.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel84.Location = New System.Drawing.Point(9, 55)
        Me.MyLabel84.Name = "MyLabel84"
        Me.MyLabel84.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel84.TabIndex = 223
        Me.MyLabel84.Text = "Financial Year "
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(9, 34)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(66, 16)
        Me.lblDescription.TabIndex = 222
        Me.lblDescription.Text = "Description "
        '
        'TxtDesp
        '
        Me.TxtDesp.AutoSize = False
        Me.TxtDesp.CalculationExpression = Nothing
        Me.TxtDesp.FieldCode = Nothing
        Me.TxtDesp.FieldDesc = Nothing
        Me.TxtDesp.FieldMaxLength = 0
        Me.TxtDesp.FieldName = Nothing
        Me.TxtDesp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDesp.isCalculatedField = False
        Me.TxtDesp.IsSourceFromTable = False
        Me.TxtDesp.IsSourceFromValueList = False
        Me.TxtDesp.IsUnique = False
        Me.TxtDesp.Location = New System.Drawing.Point(140, 28)
        Me.TxtDesp.MaxLength = 100
        Me.TxtDesp.MendatroryField = False
        Me.TxtDesp.Multiline = True
        Me.TxtDesp.MyLinkLable1 = Me.lblDescription
        Me.TxtDesp.MyLinkLable2 = Nothing
        Me.TxtDesp.Name = "TxtDesp"
        Me.TxtDesp.ReferenceFieldDesc = Nothing
        Me.TxtDesp.ReferenceFieldName = Nothing
        Me.TxtDesp.ReferenceTableName = Nothing
        Me.TxtDesp.Size = New System.Drawing.Size(274, 21)
        Me.TxtDesp.TabIndex = 2
        Me.TxtDesp.Text = " "
        '
        'lblItemCategoryCode
        '
        Me.lblItemCategoryCode.FieldName = Nothing
        Me.lblItemCategoryCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblItemCategoryCode.Location = New System.Drawing.Point(9, 11)
        Me.lblItemCategoryCode.Name = "lblItemCategoryCode"
        Me.lblItemCategoryCode.Size = New System.Drawing.Size(36, 16)
        Me.lblItemCategoryCode.TabIndex = 221
        Me.lblItemCategoryCode.Text = "Code "
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 28.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 37)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(657, 177)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(657, 177)
        Me.UcAttachment1.TabIndex = 1
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(170, 11)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 21)
        Me.btnPost.TabIndex = 16
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(28, 11)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 21)
        Me.btnsave.TabIndex = 13
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(585, 11)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 15
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(99, 11)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 14
        Me.btndelete.Text = "Delete"
        '
        'FrmInvestmentDeclaration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(678, 292)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu2)
        Me.Name = "FrmInvestmentDeclaration"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmInvestmentDeclaration"
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.cmbStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtActualAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtProvAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInvesTypeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel84, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDesp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtFinYear As common.UserControls.txtFinder
    Friend WithEvents TxtEmpCode As common.UserControls.txtFinder
    Friend WithEvents TxtInvType As common.UserControls.txtFinder
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtcode As common.UserControls.txtNavigator
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents lblInvesTypeName As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel84 As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents TxtDesp As common.Controls.MyTextBox
    Friend WithEvents lblItemCategoryCode As common.Controls.MyLabel
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents TxtProvAmt As common.MyNumBox
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents TxtActualAmt As common.MyNumBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents cmbStatus As common.Controls.MyComboBox
    Friend WithEvents lblStatus As common.Controls.MyLabel
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
End Class

