<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSaleIncentiveMaster
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer6 = New System.Windows.Forms.SplitContainer()
        Me.chkInactive = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtGLAccount = New common.UserControls.txtFinder()
        Me.lblGLAccount = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblPending = New common.usLock()
        Me.txtIncentiveUnit = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtRangeUnit = New common.UserControls.txtFinder()
        Me.lblUnit = New common.Controls.MyLabel()
        Me.txtIncentiveDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.txtIncentive = New common.UserControls.txtNavigator()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.txtItemSturcture = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.lblFromDate = New common.Controls.MyLabel()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.btnUpdates = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer6.Panel1.SuspendLayout()
        Me.SplitContainer6.Panel2.SuspendLayout()
        Me.SplitContainer6.SuspendLayout()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGLAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIncentiveDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpdates, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(795, 20)
        Me.RadMenu1.TabIndex = 3
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiImport, Me.rmiExport, Me.rmiClose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'rmiImport
        '
        Me.rmiImport.Name = "rmiImport"
        Me.rmiImport.Text = "Import"
        '
        'rmiExport
        '
        Me.rmiExport.AccessibleDescription = "Export"
        Me.rmiExport.AccessibleName = "Export"
        Me.rmiExport.Name = "rmiExport"
        Me.rmiExport.Text = "Export Blank Sheet"
        '
        'rmiClose
        '
        Me.rmiClose.Name = "rmiClose"
        Me.rmiClose.Text = "Close"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer6)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUpdates)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(795, 364)
        Me.SplitContainer1.SplitterDistance = 334
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer6
        '
        Me.SplitContainer6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer6.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer6.IsSplitterFixed = True
        Me.SplitContainer6.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer6.Name = "SplitContainer6"
        Me.SplitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer6.Panel1
        '
        Me.SplitContainer6.Panel1.Controls.Add(Me.chkInactive)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtGLAccount)
        Me.SplitContainer6.Panel1.Controls.Add(Me.lblGLAccount)
        Me.SplitContainer6.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer6.Panel1.Controls.Add(Me.lblPending)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtIncentiveUnit)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtRangeUnit)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtIncentiveDate)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtIncentive)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtCustomer)
        Me.SplitContainer6.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer6.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer6.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtItemSturcture)
        Me.SplitContainer6.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer6.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer6.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer6.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer6.Panel1.Controls.Add(Me.lblToDate)
        Me.SplitContainer6.Panel1.Controls.Add(Me.lblUnit)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtToDate)
        Me.SplitContainer6.Panel1.Controls.Add(Me.lblFromDate)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtDesc)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtFromDate)
        '
        'SplitContainer6.Panel2
        '
        Me.SplitContainer6.Panel2.Controls.Add(Me.gv)
        Me.SplitContainer6.Size = New System.Drawing.Size(795, 334)
        Me.SplitContainer6.SplitterDistance = 134
        Me.SplitContainer6.TabIndex = 0
        '
        'chkInactive
        '
        Me.chkInactive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInactive.Location = New System.Drawing.Point(629, 7)
        Me.chkInactive.Name = "chkInactive"
        Me.chkInactive.Size = New System.Drawing.Size(59, 16)
        Me.chkInactive.TabIndex = 51
        Me.chkInactive.Text = "Inactive"
        '
        'txtGLAccount
        '
        Me.txtGLAccount.CalculationExpression = Nothing
        Me.txtGLAccount.FieldCode = Nothing
        Me.txtGLAccount.FieldDesc = Nothing
        Me.txtGLAccount.FieldMaxLength = 0
        Me.txtGLAccount.FieldName = Nothing
        Me.txtGLAccount.isCalculatedField = False
        Me.txtGLAccount.IsSourceFromTable = False
        Me.txtGLAccount.IsSourceFromValueList = False
        Me.txtGLAccount.IsUnique = False
        Me.txtGLAccount.Location = New System.Drawing.Point(103, 114)
        Me.txtGLAccount.MendatroryField = True
        Me.txtGLAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGLAccount.MyLinkLable1 = Nothing
        Me.txtGLAccount.MyLinkLable2 = Nothing
        Me.txtGLAccount.MyReadOnly = False
        Me.txtGLAccount.MyShowMasterFormButton = False
        Me.txtGLAccount.Name = "txtGLAccount"
        Me.txtGLAccount.ReferenceFieldDesc = Nothing
        Me.txtGLAccount.ReferenceFieldName = Nothing
        Me.txtGLAccount.ReferenceTableName = Nothing
        Me.txtGLAccount.Size = New System.Drawing.Size(141, 19)
        Me.txtGLAccount.TabIndex = 48
        Me.txtGLAccount.Value = ""
        '
        'lblGLAccount
        '
        Me.lblGLAccount.CalculationExpression = Nothing
        Me.lblGLAccount.FieldCode = Nothing
        Me.lblGLAccount.FieldDesc = Nothing
        Me.lblGLAccount.FieldMaxLength = 0
        Me.lblGLAccount.FieldName = Nothing
        Me.lblGLAccount.isCalculatedField = False
        Me.lblGLAccount.IsSourceFromTable = False
        Me.lblGLAccount.IsSourceFromValueList = False
        Me.lblGLAccount.IsUnique = False
        Me.lblGLAccount.Location = New System.Drawing.Point(246, 113)
        Me.lblGLAccount.MendatroryField = False
        Me.lblGLAccount.MyLinkLable1 = Nothing
        Me.lblGLAccount.MyLinkLable2 = Nothing
        Me.lblGLAccount.Name = "lblGLAccount"
        Me.lblGLAccount.ReadOnly = True
        Me.lblGLAccount.ReferenceFieldDesc = Nothing
        Me.lblGLAccount.ReferenceFieldName = Nothing
        Me.lblGLAccount.ReferenceTableName = Nothing
        Me.lblGLAccount.Size = New System.Drawing.Size(544, 20)
        Me.lblGLAccount.TabIndex = 50
        Me.lblGLAccount.TabStop = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(12, 114)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel3.TabIndex = 49
        Me.MyLabel3.Text = "GL Account"
        '
        'lblPending
        '
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(525, 5)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(98, 20)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 44
        '
        'txtIncentiveUnit
        '
        Me.txtIncentiveUnit.CalculationExpression = Nothing
        Me.txtIncentiveUnit.FieldCode = Nothing
        Me.txtIncentiveUnit.FieldDesc = Nothing
        Me.txtIncentiveUnit.FieldMaxLength = 0
        Me.txtIncentiveUnit.FieldName = Nothing
        Me.txtIncentiveUnit.isCalculatedField = False
        Me.txtIncentiveUnit.IsSourceFromTable = False
        Me.txtIncentiveUnit.IsSourceFromValueList = False
        Me.txtIncentiveUnit.IsUnique = False
        Me.txtIncentiveUnit.Location = New System.Drawing.Point(661, 48)
        Me.txtIncentiveUnit.MendatroryField = True
        Me.txtIncentiveUnit.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIncentiveUnit.MyLinkLable1 = Me.MyLabel1
        Me.txtIncentiveUnit.MyLinkLable2 = Nothing
        Me.txtIncentiveUnit.MyReadOnly = False
        Me.txtIncentiveUnit.MyShowMasterFormButton = False
        Me.txtIncentiveUnit.Name = "txtIncentiveUnit"
        Me.txtIncentiveUnit.ReferenceFieldDesc = Nothing
        Me.txtIncentiveUnit.ReferenceFieldName = Nothing
        Me.txtIncentiveUnit.ReferenceTableName = Nothing
        Me.txtIncentiveUnit.Size = New System.Drawing.Size(129, 18)
        Me.txtIncentiveUnit.TabIndex = 35
        Me.txtIncentiveUnit.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(580, 49)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel1.TabIndex = 35
        Me.MyLabel1.Text = "Incentive Unit"
        '
        'txtRangeUnit
        '
        Me.txtRangeUnit.CalculationExpression = Nothing
        Me.txtRangeUnit.FieldCode = Nothing
        Me.txtRangeUnit.FieldDesc = Nothing
        Me.txtRangeUnit.FieldMaxLength = 0
        Me.txtRangeUnit.FieldName = Nothing
        Me.txtRangeUnit.isCalculatedField = False
        Me.txtRangeUnit.IsSourceFromTable = False
        Me.txtRangeUnit.IsSourceFromValueList = False
        Me.txtRangeUnit.IsUnique = False
        Me.txtRangeUnit.Location = New System.Drawing.Point(446, 48)
        Me.txtRangeUnit.MendatroryField = True
        Me.txtRangeUnit.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRangeUnit.MyLinkLable1 = Me.lblUnit
        Me.txtRangeUnit.MyLinkLable2 = Nothing
        Me.txtRangeUnit.MyReadOnly = False
        Me.txtRangeUnit.MyShowMasterFormButton = False
        Me.txtRangeUnit.Name = "txtRangeUnit"
        Me.txtRangeUnit.ReferenceFieldDesc = Nothing
        Me.txtRangeUnit.ReferenceFieldName = Nothing
        Me.txtRangeUnit.ReferenceTableName = Nothing
        Me.txtRangeUnit.Size = New System.Drawing.Size(128, 18)
        Me.txtRangeUnit.TabIndex = 43
        Me.txtRangeUnit.Value = ""
        '
        'lblUnit
        '
        Me.lblUnit.FieldName = Nothing
        Me.lblUnit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnit.Location = New System.Drawing.Point(377, 49)
        Me.lblUnit.Name = "lblUnit"
        Me.lblUnit.Size = New System.Drawing.Size(63, 16)
        Me.lblUnit.TabIndex = 17
        Me.lblUnit.Text = "Range Unit"
        '
        'txtIncentiveDate
        '
        Me.txtIncentiveDate.CalculationExpression = Nothing
        Me.txtIncentiveDate.CustomFormat = "dd/MM/yyyy"
        Me.txtIncentiveDate.FieldCode = Nothing
        Me.txtIncentiveDate.FieldDesc = Nothing
        Me.txtIncentiveDate.FieldMaxLength = 0
        Me.txtIncentiveDate.FieldName = Nothing
        Me.txtIncentiveDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIncentiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtIncentiveDate.isCalculatedField = False
        Me.txtIncentiveDate.IsSourceFromTable = False
        Me.txtIncentiveDate.IsSourceFromValueList = False
        Me.txtIncentiveDate.IsUnique = False
        Me.txtIncentiveDate.Location = New System.Drawing.Point(413, 6)
        Me.txtIncentiveDate.MendatroryField = False
        Me.txtIncentiveDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtIncentiveDate.MyLinkLable1 = Me.RadLabel3
        Me.txtIncentiveDate.MyLinkLable2 = Nothing
        Me.txtIncentiveDate.Name = "txtIncentiveDate"
        Me.txtIncentiveDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtIncentiveDate.ReferenceFieldDesc = Nothing
        Me.txtIncentiveDate.ReferenceFieldName = Nothing
        Me.txtIncentiveDate.ReferenceTableName = Nothing
        Me.txtIncentiveDate.Size = New System.Drawing.Size(106, 18)
        Me.txtIncentiveDate.TabIndex = 42
        Me.txtIncentiveDate.TabStop = False
        Me.txtIncentiveDate.Text = "17/05/2011"
        Me.txtIncentiveDate.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(377, 7)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel3.TabIndex = 21
        Me.RadLabel3.Text = "Date"
        '
        'txtIncentive
        '
        Me.txtIncentive.FieldName = Nothing
        Me.txtIncentive.Location = New System.Drawing.Point(103, 6)
        Me.txtIncentive.MendatroryField = True
        Me.txtIncentive.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtIncentive.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtIncentive.MyLinkLable1 = Me.RadLabel1
        Me.txtIncentive.MyLinkLable2 = Nothing
        Me.txtIncentive.MyMaxLength = 30
        Me.txtIncentive.MyReadOnly = False
        Me.txtIncentive.Name = "txtIncentive"
        Me.txtIncentive.Size = New System.Drawing.Size(241, 18)
        Me.txtIncentive.TabIndex = 1
        Me.txtIncentive.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.RadLabel1.Location = New System.Drawing.Point(12, 7)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(82, 16)
        Me.RadLabel1.TabIndex = 20
        Me.RadLabel1.Text = "Incentive Code"
        '
        'txtCustomer
        '
        Me.txtCustomer.arrDispalyMember = Nothing
        Me.txtCustomer.arrValueMember = Nothing
        Me.txtCustomer.Location = New System.Drawing.Point(103, 92)
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Nothing
        Me.txtCustomer.MyLinkLable2 = Nothing
        Me.txtCustomer.MyNullText = "Please Select"
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(687, 19)
        Me.txtCustomer.TabIndex = 41
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(12, 92)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel4.TabIndex = 40
        Me.MyLabel4.Text = "Customer"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(344, 6)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(19, 18)
        Me.btnReset.TabIndex = 1
        '
        'txtItemSturcture
        '
        Me.txtItemSturcture.arrDispalyMember = Nothing
        Me.txtItemSturcture.arrValueMember = Nothing
        Me.txtItemSturcture.Location = New System.Drawing.Point(103, 70)
        Me.txtItemSturcture.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemSturcture.MyLinkLable1 = Nothing
        Me.txtItemSturcture.MyLinkLable2 = Nothing
        Me.txtItemSturcture.MyNullText = "Please Select"
        Me.txtItemSturcture.Name = "txtItemSturcture"
        Me.txtItemSturcture.Size = New System.Drawing.Size(687, 19)
        Me.txtItemSturcture.TabIndex = 39
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(12, 70)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(80, 18)
        Me.MyLabel2.TabIndex = 38
        Me.MyLabel2.Text = "Item Structure "
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(12, 27)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel2.TabIndex = 19
        Me.RadLabel2.Text = "Description"
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(213, 49)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(46, 16)
        Me.lblToDate.TabIndex = 33
        Me.lblToDate.Text = "To Date"
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "MMM/yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(265, 48)
        Me.txtToDate.MendatroryField = True
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.lblToDate
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.ShowUpDown = True
        Me.txtToDate.Size = New System.Drawing.Size(102, 19)
        Me.txtToDate.TabIndex = 32
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "May/2011"
        Me.txtToDate.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'lblFromDate
        '
        Me.lblFromDate.FieldName = Nothing
        Me.lblFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.Location = New System.Drawing.Point(12, 49)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(60, 16)
        Me.lblFromDate.TabIndex = 31
        Me.lblFromDate.Text = "From Date"
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
        Me.txtDesc.Location = New System.Drawing.Point(103, 27)
        Me.txtDesc.MendatroryField = True
        Me.txtDesc.MyLinkLable1 = Me.RadLabel2
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(687, 18)
        Me.txtDesc.TabIndex = 5
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "MMM/yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(103, 48)
        Me.txtFromDate.MendatroryField = True
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.lblFromDate
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.ShowUpDown = True
        Me.txtFromDate.Size = New System.Drawing.Size(103, 19)
        Me.txtFromDate.TabIndex = 30
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "May/2011"
        Me.txtFromDate.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv.MyStopExport = False
        Me.gv.Name = "gv"
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(795, 196)
        Me.gv.TabIndex = 2
        Me.gv.TabStop = False
        '
        'btnUpdates
        '
        Me.btnUpdates.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdates.Location = New System.Drawing.Point(244, 4)
        Me.btnUpdates.Name = "btnUpdates"
        Me.btnUpdates.Size = New System.Drawing.Size(128, 18)
        Me.btnUpdates.TabIndex = 6
        Me.btnUpdates.Text = "Add More Customer"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(163, 4)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(81, 18)
        Me.btnPost.TabIndex = 3
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(712, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(83, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(80, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(3, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'frmSaleIncentiveMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(795, 384)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmSaleIncentiveMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Customer Incentive Master"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer6.Panel1.ResumeLayout(False)
        Me.SplitContainer6.Panel1.PerformLayout()
        Me.SplitContainer6.Panel2.ResumeLayout(False)
        Me.SplitContainer6.ResumeLayout(False)
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGLAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIncentiveDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpdates, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    ' Friend WithEvents UcCustomFields1 As EnumCustomFieldType
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblUnit As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents grdScheme As common.UserControls.MyRadGridView
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblFromDate As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents SplitContainer6 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtIncentiveDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtIncentive As common.UserControls.txtNavigator
    Friend WithEvents txtCustomer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtItemSturcture As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtIncentiveUnit As common.UserControls.txtFinder
    Friend WithEvents txtRangeUnit As common.UserControls.txtFinder
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtGLAccount As common.UserControls.txtFinder
    Friend WithEvents lblGLAccount As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents chkInactive As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnUpdates As Telerik.WinControls.UI.RadButton
End Class

