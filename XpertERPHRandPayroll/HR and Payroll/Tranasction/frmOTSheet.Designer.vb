Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOTSheet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOTSheet))
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblPayPeriod = New common.Controls.MyLabel()
        Me.txtPayPeriod = New common.UserControls.txtFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtOtRate = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.lblEmpName = New common.Controls.MyLabel()
        Me.txtEmpCode = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblOtName = New common.Controls.MyLabel()
        Me.txtOTCode = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtOTAmount = New common.Controls.MyTextBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtOTHours = New common.Controls.MyTextBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.UsLock1 = New common.usLock()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOtRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOTAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOTHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.UsLock1)
        Me.RadGroupBox1.Controls.Add(Me.lblPayPeriod)
        Me.RadGroupBox1.Controls.Add(Me.txtPayPeriod)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.txtOtRate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.lblEmpName)
        Me.RadGroupBox1.Controls.Add(Me.txtEmpCode)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.lblOtName)
        Me.RadGroupBox1.Controls.Add(Me.txtOTCode)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtOTAmount)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox1.Controls.Add(Me.RadButton1)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.txtOTHours)
        Me.RadGroupBox1.Controls.Add(Me.txtCode)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.HeaderText = " "
        Me.RadGroupBox1.Location = New System.Drawing.Point(4, 12)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(519, 189)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = " "
        '
        'lblPayPeriod
        '
        Me.lblPayPeriod.AutoSize = False
        Me.lblPayPeriod.BorderVisible = True
        Me.lblPayPeriod.FieldName = Nothing
        Me.lblPayPeriod.Location = New System.Drawing.Point(317, 92)
        Me.lblPayPeriod.Name = "lblPayPeriod"
        Me.lblPayPeriod.Size = New System.Drawing.Size(189, 18)
        Me.lblPayPeriod.TabIndex = 7
        Me.lblPayPeriod.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPayPeriod
        '
        Me.txtPayPeriod.CalculationExpression = Nothing
        Me.txtPayPeriod.FieldCode = Nothing
        Me.txtPayPeriod.FieldDesc = Nothing
        Me.txtPayPeriod.FieldMaxLength = 0
        Me.txtPayPeriod.FieldName = Nothing
        Me.txtPayPeriod.isCalculatedField = False
        Me.txtPayPeriod.IsSourceFromTable = False
        Me.txtPayPeriod.IsSourceFromValueList = False
        Me.txtPayPeriod.IsUnique = False
        Me.txtPayPeriod.Location = New System.Drawing.Point(132, 92)
        Me.txtPayPeriod.MendatroryField = True
        Me.txtPayPeriod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayPeriod.MyLinkLable1 = Me.MyLabel5
        Me.txtPayPeriod.MyLinkLable2 = Me.lblPayPeriod
        Me.txtPayPeriod.MyReadOnly = False
        Me.txtPayPeriod.MyShowMasterFormButton = False
        Me.txtPayPeriod.Name = "txtPayPeriod"
        Me.txtPayPeriod.ReferenceFieldDesc = Nothing
        Me.txtPayPeriod.ReferenceFieldName = Nothing
        Me.txtPayPeriod.ReferenceTableName = Nothing
        Me.txtPayPeriod.Size = New System.Drawing.Size(181, 18)
        Me.txtPayPeriod.TabIndex = 6
        Me.txtPayPeriod.Value = ""
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(13, 92)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(88, 18)
        Me.MyLabel5.TabIndex = 34
        Me.MyLabel5.Text = "Pay Period Code"
        '
        'txtOtRate
        '
        Me.txtOtRate.CalculationExpression = Nothing
        Me.txtOtRate.FieldCode = Nothing
        Me.txtOtRate.FieldDesc = Nothing
        Me.txtOtRate.FieldMaxLength = 0
        Me.txtOtRate.FieldName = Nothing
        Me.txtOtRate.isCalculatedField = False
        Me.txtOtRate.IsSourceFromTable = False
        Me.txtOtRate.IsSourceFromValueList = False
        Me.txtOtRate.IsUnique = False
        Me.txtOtRate.Location = New System.Drawing.Point(132, 115)
        Me.txtOtRate.MaxLength = 50
        Me.txtOtRate.MendatroryField = False
        Me.txtOtRate.MyLinkLable1 = Me.MyLabel4
        Me.txtOtRate.MyLinkLable2 = Nothing
        Me.txtOtRate.Name = "txtOtRate"
        Me.txtOtRate.ReadOnly = True
        Me.txtOtRate.ReferenceFieldDesc = Nothing
        Me.txtOtRate.ReferenceFieldName = Nothing
        Me.txtOtRate.ReferenceTableName = Nothing
        Me.txtOtRate.Size = New System.Drawing.Size(181, 20)
        Me.txtOtRate.TabIndex = 8
        Me.txtOtRate.TabStop = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(13, 115)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(46, 18)
        Me.MyLabel4.TabIndex = 31
        Me.MyLabel4.Text = "OT Rate"
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.FieldName = Nothing
        Me.lblEmpName.Location = New System.Drawing.Point(317, 46)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(189, 18)
        Me.lblEmpName.TabIndex = 3
        Me.lblEmpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtEmpCode
        '
        Me.txtEmpCode.CalculationExpression = Nothing
        Me.txtEmpCode.FieldCode = Nothing
        Me.txtEmpCode.FieldDesc = Nothing
        Me.txtEmpCode.FieldMaxLength = 0
        Me.txtEmpCode.FieldName = Nothing
        Me.txtEmpCode.isCalculatedField = False
        Me.txtEmpCode.IsSourceFromTable = False
        Me.txtEmpCode.IsSourceFromValueList = False
        Me.txtEmpCode.IsUnique = False
        Me.txtEmpCode.Location = New System.Drawing.Point(132, 46)
        Me.txtEmpCode.MendatroryField = True
        Me.txtEmpCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.MyLinkLable1 = Me.MyLabel3
        Me.txtEmpCode.MyLinkLable2 = Me.lblEmpName
        Me.txtEmpCode.MyReadOnly = False
        Me.txtEmpCode.MyShowMasterFormButton = False
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.ReferenceFieldDesc = Nothing
        Me.txtEmpCode.ReferenceFieldName = Nothing
        Me.txtEmpCode.ReferenceTableName = Nothing
        Me.txtEmpCode.Size = New System.Drawing.Size(181, 18)
        Me.txtEmpCode.TabIndex = 2
        Me.txtEmpCode.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(13, 46)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(84, 18)
        Me.MyLabel3.TabIndex = 27
        Me.MyLabel3.Text = "Employee Code"
        '
        'lblOtName
        '
        Me.lblOtName.AutoSize = False
        Me.lblOtName.BorderVisible = True
        Me.lblOtName.FieldName = Nothing
        Me.lblOtName.Location = New System.Drawing.Point(317, 69)
        Me.lblOtName.Name = "lblOtName"
        Me.lblOtName.Size = New System.Drawing.Size(189, 18)
        Me.lblOtName.TabIndex = 5
        Me.lblOtName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtOTCode
        '
        Me.txtOTCode.CalculationExpression = Nothing
        Me.txtOTCode.FieldCode = Nothing
        Me.txtOTCode.FieldDesc = Nothing
        Me.txtOTCode.FieldMaxLength = 0
        Me.txtOTCode.FieldName = Nothing
        Me.txtOTCode.isCalculatedField = False
        Me.txtOTCode.IsSourceFromTable = False
        Me.txtOTCode.IsSourceFromValueList = False
        Me.txtOTCode.IsUnique = False
        Me.txtOTCode.Location = New System.Drawing.Point(132, 69)
        Me.txtOTCode.MendatroryField = True
        Me.txtOTCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOTCode.MyLinkLable1 = Me.MyLabel1
        Me.txtOTCode.MyLinkLable2 = Me.lblOtName
        Me.txtOTCode.MyReadOnly = False
        Me.txtOTCode.MyShowMasterFormButton = False
        Me.txtOTCode.Name = "txtOTCode"
        Me.txtOTCode.ReferenceFieldDesc = Nothing
        Me.txtOTCode.ReferenceFieldName = Nothing
        Me.txtOTCode.ReferenceTableName = Nothing
        Me.txtOTCode.Size = New System.Drawing.Size(181, 18)
        Me.txtOTCode.TabIndex = 4
        Me.txtOTCode.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(13, 69)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(50, 18)
        Me.MyLabel1.TabIndex = 24
        Me.MyLabel1.Text = "OT Code"
        '
        'txtOTAmount
        '
        Me.txtOTAmount.CalculationExpression = Nothing
        Me.txtOTAmount.FieldCode = Nothing
        Me.txtOTAmount.FieldDesc = Nothing
        Me.txtOTAmount.FieldMaxLength = 0
        Me.txtOTAmount.FieldName = Nothing
        Me.txtOTAmount.isCalculatedField = False
        Me.txtOTAmount.IsSourceFromTable = False
        Me.txtOTAmount.IsSourceFromValueList = False
        Me.txtOTAmount.IsUnique = False
        Me.txtOTAmount.Location = New System.Drawing.Point(132, 161)
        Me.txtOTAmount.MaxLength = 50
        Me.txtOTAmount.MendatroryField = False
        Me.txtOTAmount.MyLinkLable1 = Me.RadLabel3
        Me.txtOTAmount.MyLinkLable2 = Nothing
        Me.txtOTAmount.Name = "txtOTAmount"
        Me.txtOTAmount.ReferenceFieldDesc = Nothing
        Me.txtOTAmount.ReferenceFieldName = Nothing
        Me.txtOTAmount.ReferenceTableName = Nothing
        Me.txtOTAmount.Size = New System.Drawing.Size(181, 20)
        Me.txtOTAmount.TabIndex = 10
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(13, 161)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(92, 18)
        Me.RadLabel3.TabIndex = 20
        Me.RadLabel3.Text = "Total OT Amount"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Location = New System.Drawing.Point(13, 89)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(2, 2)
        Me.RadLabel4.TabIndex = 19
        '
        'RadButton1
        '
        Me.RadButton1.Image = CType(resources.GetObject("RadButton1.Image"), System.Drawing.Image)
        Me.RadButton1.Location = New System.Drawing.Point(352, -422)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(14, 20)
        Me.RadButton1.TabIndex = 17
        Me.RadButton1.Text = " "
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(356, 22)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'txtOTHours
        '
        Me.txtOTHours.CalculationExpression = Nothing
        Me.txtOTHours.FieldCode = Nothing
        Me.txtOTHours.FieldDesc = Nothing
        Me.txtOTHours.FieldMaxLength = 0
        Me.txtOTHours.FieldName = Nothing
        Me.txtOTHours.isCalculatedField = False
        Me.txtOTHours.IsSourceFromTable = False
        Me.txtOTHours.IsSourceFromValueList = False
        Me.txtOTHours.IsUnique = False
        Me.txtOTHours.Location = New System.Drawing.Point(132, 138)
        Me.txtOTHours.MaxLength = 50
        Me.txtOTHours.MendatroryField = False
        Me.txtOTHours.MyLinkLable1 = Me.RadLabel2
        Me.txtOTHours.MyLinkLable2 = Nothing
        Me.txtOTHours.Name = "txtOTHours"
        Me.txtOTHours.ReferenceFieldDesc = Nothing
        Me.txtOTHours.ReferenceFieldName = Nothing
        Me.txtOTHours.ReferenceTableName = Nothing
        Me.txtOTHours.Size = New System.Drawing.Size(181, 20)
        Me.txtOTHours.TabIndex = 9
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(13, 138)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(54, 18)
        Me.RadLabel2.TabIndex = 19
        Me.RadLabel2.Text = "OT Hours"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(132, 22)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.RadLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(222, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(13, 23)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "Code"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(460, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnDelete.Location = New System.Drawing.Point(74, 6)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnSave.Location = New System.Drawing.Point(3, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenuItemExport
        '
        Me.RadMenuItemExport.AccessibleDescription = "File"
        Me.RadMenuItemExport.AccessibleName = "File"
        Me.RadMenuItemExport.Name = "RadMenuItemExport"
        Me.RadMenuItemExport.Text = "File"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "File"
        Me.RadMenuItem2.AccessibleName = "File"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "File"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(529, 478)
        Me.SplitContainer1.SplitterDistance = 447
        Me.SplitContainer1.TabIndex = 0
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(529, 20)
        Me.RadMenu2.TabIndex = 0
        Me.RadMenu2.Text = "RadMenu2"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "File"
        Me.RadMenuItem3.AccessibleName = "File"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuItemImport, Me.MenuItemExport, Me.MenuItemClose})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'MenuItemImport
        '
        Me.MenuItemImport.AccessibleDescription = "Import"
        Me.MenuItemImport.AccessibleName = "Import"
        Me.MenuItemImport.Name = "MenuItemImport"
        Me.MenuItemImport.Text = "Import"
        '
        'MenuItemExport
        '
        Me.MenuItemExport.AccessibleDescription = "Export"
        Me.MenuItemExport.AccessibleName = "Export"
        Me.MenuItemExport.Name = "MenuItemExport"
        Me.MenuItemExport.Text = "Export"
        '
        'MenuItemClose
        '
        Me.MenuItemClose.AccessibleDescription = "Close"
        Me.MenuItemClose.AccessibleName = "Close"
        Me.MenuItemClose.Name = "MenuItemClose"
        Me.MenuItemClose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnPost.Location = New System.Drawing.Point(146, 6)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 18)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(376, 25)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 18)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 213
        '
        'frmOTSheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(529, 478)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmOTSheet"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "OT Sheet"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOtRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOTAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOTHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtOTHours As common.Controls.MyTextBox
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtOTAmount As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtOtRate As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents txtEmpCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblOtName As common.Controls.MyLabel
    Friend WithEvents txtOTCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblPayPeriod As common.Controls.MyLabel
    Friend WithEvents txtPayPeriod As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
End Class

