<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGSTunitMeasure
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGSTunitMeasure))
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtName = New common.Controls.MyTextBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.TxtNavigator1 = New common.UserControls.txtNavigator()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.MyTextBox1 = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.TxtNavigator2 = New common.UserControls.txtNavigator()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.MyTextBox2 = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TxtNavigator3 = New common.UserControls.txtNavigator()
        Me.TxtNavigator5 = New common.UserControls.txtNavigator()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtcode1 = New common.UserControls.txtNavigator()
        Me.lblcode = New common.Controls.MyLabel()
        Me.btnNew1 = New Telerik.WinControls.UI.RadButton()
        Me.txtName1 = New common.Controls.MyTextBox()
        Me.lblName = New common.Controls.MyLabel()
        Me.btnSave1 = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete1 = New Telerik.WinControls.UI.RadButton()
        Me.btnClose1 = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadMenu2.SuspendLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyTextBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyTextBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.lblcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuItemImport, Me.MenuItemExport})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        Me.RadMenuItem3.UseCompatibleTextRendering = False
        '
        'MenuItemImport
        '
        Me.MenuItemImport.Name = "MenuItemImport"
        Me.MenuItemImport.Text = "Import"
        Me.MenuItemImport.UseCompatibleTextRendering = False
        '
        'MenuItemExport
        '
        Me.MenuItemExport.Name = "MenuItemExport"
        Me.MenuItemExport.Text = "Export"
        Me.MenuItemExport.UseCompatibleTextRendering = False
        '
        'RadMenu2
        '
        Me.RadMenu2.Controls.Add(Me.Splitter1)
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(512, 20)
        Me.RadMenu2.TabIndex = 16
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 20)
        Me.Splitter1.TabIndex = 0
        Me.Splitter1.TabStop = False
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(73, 0)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.RadLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(252, 20)
        Me.txtCode.TabIndex = 20
        Me.txtCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(1, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "Code"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(327, 0)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(19, 21)
        Me.btnNew.TabIndex = 19
        Me.btnNew.Text = " "
        '
        'txtName
        '
        Me.txtName.CalculationExpression = Nothing
        Me.txtName.FieldCode = Nothing
        Me.txtName.FieldDesc = Nothing
        Me.txtName.FieldMaxLength = 0
        Me.txtName.FieldName = Nothing
        Me.txtName.isCalculatedField = False
        Me.txtName.IsSourceFromTable = False
        Me.txtName.IsSourceFromValueList = False
        Me.txtName.IsUnique = False
        Me.txtName.Location = New System.Drawing.Point(73, 26)
        Me.txtName.MaxLength = 100
        Me.txtName.MendatroryField = True
        Me.txtName.MyLinkLable1 = Me.RadLabel2
        Me.txtName.MyLinkLable2 = Nothing
        Me.txtName.Name = "txtName"
        Me.txtName.ReferenceFieldDesc = Nothing
        Me.txtName.ReferenceFieldName = Nothing
        Me.txtName.ReferenceTableName = Nothing
        Me.txtName.Size = New System.Drawing.Size(359, 20)
        Me.txtName.TabIndex = 2
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(0, 27)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel2.TabIndex = 1
        Me.RadLabel2.Text = "Description"
        '
        'TxtNavigator1
        '
        Me.TxtNavigator1.FieldName = Nothing
        Me.TxtNavigator1.Location = New System.Drawing.Point(73, 0)
        Me.TxtNavigator1.MendatroryField = False
        Me.TxtNavigator1.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.TxtNavigator1.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.TxtNavigator1.MyLinkLable1 = Me.MyLabel1
        Me.TxtNavigator1.MyLinkLable2 = Nothing
        Me.TxtNavigator1.MyMaxLength = 12
        Me.TxtNavigator1.MyReadOnly = False
        Me.TxtNavigator1.Name = "TxtNavigator1"
        Me.TxtNavigator1.Size = New System.Drawing.Size(252, 20)
        Me.TxtNavigator1.TabIndex = 20
        Me.TxtNavigator1.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(1, 3)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel1.TabIndex = 0
        Me.MyLabel1.Text = "Code"
        '
        'RadButton1
        '
        Me.RadButton1.Image = CType(resources.GetObject("RadButton1.Image"), System.Drawing.Image)
        Me.RadButton1.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadButton1.Location = New System.Drawing.Point(327, 0)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(19, 21)
        Me.RadButton1.TabIndex = 19
        Me.RadButton1.Text = " "
        '
        'MyTextBox1
        '
        Me.MyTextBox1.CalculationExpression = Nothing
        Me.MyTextBox1.FieldCode = Nothing
        Me.MyTextBox1.FieldDesc = Nothing
        Me.MyTextBox1.FieldMaxLength = 0
        Me.MyTextBox1.FieldName = Nothing
        Me.MyTextBox1.isCalculatedField = False
        Me.MyTextBox1.IsSourceFromTable = False
        Me.MyTextBox1.IsSourceFromValueList = False
        Me.MyTextBox1.IsUnique = False
        Me.MyTextBox1.Location = New System.Drawing.Point(73, 26)
        Me.MyTextBox1.MaxLength = 100
        Me.MyTextBox1.MendatroryField = True
        Me.MyTextBox1.MyLinkLable1 = Me.MyLabel2
        Me.MyTextBox1.MyLinkLable2 = Nothing
        Me.MyTextBox1.Name = "MyTextBox1"
        Me.MyTextBox1.ReferenceFieldDesc = Nothing
        Me.MyTextBox1.ReferenceFieldName = Nothing
        Me.MyTextBox1.ReferenceTableName = Nothing
        Me.MyTextBox1.Size = New System.Drawing.Size(359, 20)
        Me.MyTextBox1.TabIndex = 2
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(0, 27)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel2.TabIndex = 1
        Me.MyLabel2.Text = "Description"
        '
        'TxtNavigator2
        '
        Me.TxtNavigator2.FieldName = Nothing
        Me.TxtNavigator2.Location = New System.Drawing.Point(73, 0)
        Me.TxtNavigator2.MendatroryField = False
        Me.TxtNavigator2.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.TxtNavigator2.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.TxtNavigator2.MyLinkLable1 = Me.MyLabel3
        Me.TxtNavigator2.MyLinkLable2 = Nothing
        Me.TxtNavigator2.MyMaxLength = 12
        Me.TxtNavigator2.MyReadOnly = False
        Me.TxtNavigator2.Name = "TxtNavigator2"
        Me.TxtNavigator2.Size = New System.Drawing.Size(252, 20)
        Me.TxtNavigator2.TabIndex = 20
        Me.TxtNavigator2.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(1, 3)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel3.TabIndex = 0
        Me.MyLabel3.Text = "Code"
        '
        'RadButton2
        '
        Me.RadButton2.Image = CType(resources.GetObject("RadButton2.Image"), System.Drawing.Image)
        Me.RadButton2.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadButton2.Location = New System.Drawing.Point(327, 0)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(19, 21)
        Me.RadButton2.TabIndex = 19
        Me.RadButton2.Text = " "
        '
        'MyTextBox2
        '
        Me.MyTextBox2.CalculationExpression = Nothing
        Me.MyTextBox2.FieldCode = Nothing
        Me.MyTextBox2.FieldDesc = Nothing
        Me.MyTextBox2.FieldMaxLength = 0
        Me.MyTextBox2.FieldName = Nothing
        Me.MyTextBox2.isCalculatedField = False
        Me.MyTextBox2.IsSourceFromTable = False
        Me.MyTextBox2.IsSourceFromValueList = False
        Me.MyTextBox2.IsUnique = False
        Me.MyTextBox2.Location = New System.Drawing.Point(73, 26)
        Me.MyTextBox2.MaxLength = 100
        Me.MyTextBox2.MendatroryField = True
        Me.MyTextBox2.MyLinkLable1 = Me.MyLabel4
        Me.MyTextBox2.MyLinkLable2 = Nothing
        Me.MyTextBox2.Name = "MyTextBox2"
        Me.MyTextBox2.ReferenceFieldDesc = Nothing
        Me.MyTextBox2.ReferenceFieldName = Nothing
        Me.MyTextBox2.ReferenceTableName = Nothing
        Me.MyTextBox2.Size = New System.Drawing.Size(359, 20)
        Me.MyTextBox2.TabIndex = 2
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(0, 27)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel4.TabIndex = 1
        Me.MyLabel4.Text = "Description"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(48, 25)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(77, 20)
        Me.TextBox1.TabIndex = 0
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(12, 52)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(92, 20)
        Me.TextBox2.TabIndex = 0
        '
        'TxtNavigator3
        '
        Me.TxtNavigator3.FieldName = Nothing
        Me.TxtNavigator3.Location = New System.Drawing.Point(0, 0)
        Me.TxtNavigator3.MendatroryField = False
        Me.TxtNavigator3.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.TxtNavigator3.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.TxtNavigator3.MyLinkLable1 = Me.RadLabel1
        Me.TxtNavigator3.MyLinkLable2 = Nothing
        Me.TxtNavigator3.MyMaxLength = 12
        Me.TxtNavigator3.MyReadOnly = False
        Me.TxtNavigator3.Name = "TxtNavigator3"
        Me.TxtNavigator3.Size = New System.Drawing.Size(252, 20)
        Me.TxtNavigator3.TabIndex = 20
        Me.TxtNavigator3.Value = ""
        '
        'TxtNavigator5
        '
        Me.TxtNavigator5.FieldName = Nothing
        Me.TxtNavigator5.Location = New System.Drawing.Point(0, 0)
        Me.TxtNavigator5.MendatroryField = False
        Me.TxtNavigator5.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.TxtNavigator5.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.TxtNavigator5.MyLinkLable1 = Me.RadLabel1
        Me.TxtNavigator5.MyLinkLable2 = Nothing
        Me.TxtNavigator5.MyMaxLength = 12
        Me.TxtNavigator5.MyReadOnly = False
        Me.TxtNavigator5.Name = "TxtNavigator5"
        Me.TxtNavigator5.Size = New System.Drawing.Size(252, 20)
        Me.TxtNavigator5.TabIndex = 20
        Me.TxtNavigator5.Value = ""
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnSave.Location = New System.Drawing.Point(0, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnDelete.Location = New System.Drawing.Point(71, 0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(457, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'Splitter2
        '
        Me.Splitter2.Location = New System.Drawing.Point(0, 20)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 113)
        Me.Splitter2.TabIndex = 26
        Me.Splitter2.TabStop = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose1)
        Me.SplitContainer1.Size = New System.Drawing.Size(509, 113)
        Me.SplitContainer1.SplitterDistance = 84
        Me.SplitContainer1.TabIndex = 27
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtcode1)
        Me.GroupBox1.Controls.Add(Me.btnNew1)
        Me.GroupBox1.Controls.Add(Me.txtName1)
        Me.GroupBox1.Controls.Add(Me.lblName)
        Me.GroupBox1.Controls.Add(Me.lblcode)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(500, 77)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtcode1
        '
        Me.txtcode1.FieldName = Nothing
        Me.txtcode1.Location = New System.Drawing.Point(78, 7)
        Me.txtcode1.MendatroryField = False
        Me.txtcode1.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtcode1.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcode1.MyLinkLable1 = Me.lblcode
        Me.txtcode1.MyLinkLable2 = Nothing
        Me.txtcode1.MyMaxLength = 12
        Me.txtcode1.MyReadOnly = False
        Me.txtcode1.Name = "txtcode1"
        Me.txtcode1.Size = New System.Drawing.Size(252, 20)
        Me.txtcode1.TabIndex = 25
        Me.txtcode1.Value = ""
        '
        'lblcode
        '
        Me.lblcode.FieldName = Nothing
        Me.lblcode.Location = New System.Drawing.Point(5, 10)
        Me.lblcode.Name = "lblcode"
        Me.lblcode.Size = New System.Drawing.Size(32, 18)
        Me.lblcode.TabIndex = 21
        Me.lblcode.Text = "Code"
        '
        'btnNew1
        '
        Me.btnNew1.Image = CType(resources.GetObject("btnNew1.Image"), System.Drawing.Image)
        Me.btnNew1.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew1.Location = New System.Drawing.Point(332, 7)
        Me.btnNew1.Name = "btnNew1"
        Me.btnNew1.Size = New System.Drawing.Size(19, 21)
        Me.btnNew1.TabIndex = 24
        Me.btnNew1.Text = " "
        '
        'txtName1
        '
        Me.txtName1.CalculationExpression = Nothing
        Me.txtName1.FieldCode = Nothing
        Me.txtName1.FieldDesc = Nothing
        Me.txtName1.FieldMaxLength = 0
        Me.txtName1.FieldName = Nothing
        Me.txtName1.isCalculatedField = False
        Me.txtName1.IsSourceFromTable = False
        Me.txtName1.IsSourceFromValueList = False
        Me.txtName1.IsUnique = False
        Me.txtName1.Location = New System.Drawing.Point(78, 33)
        Me.txtName1.MaxLength = 100
        Me.txtName1.MendatroryField = True
        Me.txtName1.MyLinkLable1 = Me.lblName
        Me.txtName1.MyLinkLable2 = Nothing
        Me.txtName1.Name = "txtName1"
        Me.txtName1.ReferenceFieldDesc = Nothing
        Me.txtName1.ReferenceFieldName = Nothing
        Me.txtName1.ReferenceTableName = Nothing
        Me.txtName1.Size = New System.Drawing.Size(359, 20)
        Me.txtName1.TabIndex = 23
        '
        'lblName
        '
        Me.lblName.FieldName = Nothing
        Me.lblName.Location = New System.Drawing.Point(5, 34)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(36, 18)
        Me.lblName.TabIndex = 22
        Me.lblName.Text = "Name"
        '
        'btnSave1
        '
        Me.btnSave1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave1.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnSave1.Location = New System.Drawing.Point(3, 3)
        Me.btnSave1.Name = "btnSave1"
        Me.btnSave1.Size = New System.Drawing.Size(68, 18)
        Me.btnSave1.TabIndex = 6
        Me.btnSave1.Text = "Save"
        '
        'btnDelete1
        '
        Me.btnDelete1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete1.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnDelete1.Location = New System.Drawing.Point(74, 3)
        Me.btnDelete1.Name = "btnDelete1"
        Me.btnDelete1.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete1.TabIndex = 7
        Me.btnDelete1.Text = "Delete"
        '
        'btnClose1
        '
        Me.btnClose1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose1.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose1.Location = New System.Drawing.Point(436, 3)
        Me.btnClose1.Name = "btnClose1"
        Me.btnClose1.Size = New System.Drawing.Size(68, 18)
        Me.btnClose1.TabIndex = 8
        Me.btnClose1.Text = "Close"
        '
        'frmGSTunitMeasure
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(512, 133)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.RadMenu2)
        Me.Name = "frmGSTunitMeasure"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmGSTunitMeasure"
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadMenu2.ResumeLayout(False)
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyTextBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyTextBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.lblcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenuItem3 As RadMenuItem
    Friend WithEvents MenuItemImport As RadMenuItem
    Friend WithEvents MenuItemExport As RadMenuItem
    Friend WithEvents RadMenu2 As RadMenu
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnNew As RadButton
    Friend WithEvents txtName As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents TxtNavigator1 As common.UserControls.txtNavigator
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents MyTextBox1 As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents Splitter1 As Splitter
    Friend WithEvents TxtNavigator2 As common.UserControls.txtNavigator
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadButton2 As RadButton
    Friend WithEvents MyTextBox2 As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TxtNavigator3 As common.UserControls.txtNavigator
    Friend WithEvents TxtNavigator5 As common.UserControls.txtNavigator
    Friend WithEvents btnSave As RadButton
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents Splitter2 As Splitter
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtcode1 As common.UserControls.txtNavigator
    Friend WithEvents lblcode As common.Controls.MyLabel
    Friend WithEvents btnNew1 As RadButton
    Friend WithEvents txtName1 As common.Controls.MyTextBox
    Friend WithEvents lblName As common.Controls.MyLabel
    Friend WithEvents btnSave1 As RadButton
    Friend WithEvents btnDelete1 As RadButton
    Friend WithEvents btnClose1 As RadButton
End Class
