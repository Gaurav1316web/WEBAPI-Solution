<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymentCycleMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPaymentCycleMaster))
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkIsDefault = New Telerik.WinControls.UI.RadCheckBox()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtValue = New common.Controls.MyTextBox()
        Me.CboFirstDay = New common.Controls.MyComboBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.cboPCType = New common.Controls.MyComboBox()
        Me.lblCategory = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
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
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkIsDefault, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboFirstDay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPCType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkIsDefault)
        Me.RadGroupBox1.Controls.Add(Me.SplitContainer2)
        Me.RadGroupBox1.Controls.Add(Me.cboPCType)
        Me.RadGroupBox1.Controls.Add(Me.lblCategory)
        Me.RadGroupBox1.Controls.Add(Me.txtDescription)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox1.Controls.Add(Me.RadButton1)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.txtCode)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 20)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(569, 111)
        Me.RadGroupBox1.TabIndex = 0
        '
        'chkIsDefault
        '
        Me.chkIsDefault.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsDefault.Location = New System.Drawing.Point(434, 10)
        Me.chkIsDefault.Name = "chkIsDefault"
        Me.chkIsDefault.Size = New System.Drawing.Size(68, 16)
        Me.chkIsDefault.TabIndex = 169
        Me.chkIsDefault.Text = "Is Default"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Location = New System.Drawing.Point(10, 71)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtValue)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.CboFirstDay)
        Me.SplitContainer2.Panel2.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel2Collapsed = True
        Me.SplitContainer2.Size = New System.Drawing.Size(382, 24)
        Me.SplitContainer2.SplitterDistance = 127
        Me.SplitContainer2.TabIndex = 27
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(3, 2)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(34, 18)
        Me.MyLabel1.TabIndex = 24
        Me.MyLabel1.Text = "Value"
        '
        'txtValue
        '
        Me.txtValue.CalculationExpression = Nothing
        Me.txtValue.FieldCode = Nothing
        Me.txtValue.FieldDesc = Nothing
        Me.txtValue.FieldMaxLength = 0
        Me.txtValue.FieldName = Nothing
        Me.txtValue.isCalculatedField = False
        Me.txtValue.IsSourceFromTable = False
        Me.txtValue.IsSourceFromValueList = False
        Me.txtValue.IsUnique = False
        Me.txtValue.Location = New System.Drawing.Point(69, 2)
        Me.txtValue.MaxLength = 50
        Me.txtValue.MendatroryField = False
        Me.txtValue.MyLinkLable1 = Me.MyLabel1
        Me.txtValue.MyLinkLable2 = Nothing
        Me.txtValue.Name = "txtValue"
        Me.txtValue.ReferenceFieldDesc = Nothing
        Me.txtValue.ReferenceFieldName = Nothing
        Me.txtValue.ReferenceTableName = Nothing
        Me.txtValue.Size = New System.Drawing.Size(313, 20)
        Me.txtValue.TabIndex = 3
        '
        'CboFirstDay
        '
        Me.CboFirstDay.AutoCompleteDisplayMember = Nothing
        Me.CboFirstDay.AutoCompleteValueMember = Nothing
        Me.CboFirstDay.CalculationExpression = Nothing
        Me.CboFirstDay.DropDownAnimationEnabled = True
        Me.CboFirstDay.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboFirstDay.FieldCode = Nothing
        Me.CboFirstDay.FieldDesc = Nothing
        Me.CboFirstDay.FieldMaxLength = 0
        Me.CboFirstDay.FieldName = Nothing
        Me.CboFirstDay.isCalculatedField = False
        Me.CboFirstDay.IsSourceFromTable = False
        Me.CboFirstDay.IsSourceFromValueList = False
        Me.CboFirstDay.IsUnique = False
        Me.CboFirstDay.Location = New System.Drawing.Point(69, 2)
        Me.CboFirstDay.MendatroryField = True
        Me.CboFirstDay.MyLinkLable1 = Nothing
        Me.CboFirstDay.MyLinkLable2 = Nothing
        Me.CboFirstDay.Name = "CboFirstDay"
        Me.CboFirstDay.ReferenceFieldDesc = Nothing
        Me.CboFirstDay.ReferenceFieldName = Nothing
        Me.CboFirstDay.ReferenceTableName = Nothing
        Me.CboFirstDay.Size = New System.Drawing.Size(313, 20)
        Me.CboFirstDay.TabIndex = 25
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(0, 3)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel2.TabIndex = 26
        Me.MyLabel2.Text = "First Day"
        '
        'cboPCType
        '
        Me.cboPCType.AutoCompleteDisplayMember = Nothing
        Me.cboPCType.AutoCompleteValueMember = Nothing
        Me.cboPCType.CalculationExpression = Nothing
        Me.cboPCType.DropDownAnimationEnabled = True
        Me.cboPCType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboPCType.FieldCode = Nothing
        Me.cboPCType.FieldDesc = Nothing
        Me.cboPCType.FieldMaxLength = 0
        Me.cboPCType.FieldName = Nothing
        Me.cboPCType.isCalculatedField = False
        Me.cboPCType.IsSourceFromTable = False
        Me.cboPCType.IsSourceFromValueList = False
        Me.cboPCType.IsUnique = False
        RadListDataItem1.Tag = "Fat"
        RadListDataItem1.Text = "Fat"
        RadListDataItem2.Tag = "SNF"
        RadListDataItem2.Text = "SNF"
        Me.cboPCType.Items.Add(RadListDataItem1)
        Me.cboPCType.Items.Add(RadListDataItem2)
        Me.cboPCType.Location = New System.Drawing.Point(79, 50)
        Me.cboPCType.MendatroryField = True
        Me.cboPCType.MyLinkLable1 = Nothing
        Me.cboPCType.MyLinkLable2 = Nothing
        Me.cboPCType.Name = "cboPCType"
        Me.cboPCType.ReferenceFieldDesc = Nothing
        Me.cboPCType.ReferenceFieldName = Nothing
        Me.cboPCType.ReferenceTableName = Nothing
        Me.cboPCType.Size = New System.Drawing.Size(313, 20)
        Me.cboPCType.TabIndex = 2
        '
        'lblCategory
        '
        Me.lblCategory.FieldName = Nothing
        Me.lblCategory.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblCategory.Location = New System.Drawing.Point(10, 50)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(30, 18)
        Me.lblCategory.TabIndex = 22
        Me.lblCategory.Text = "Type"
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
        Me.txtDescription.Location = New System.Drawing.Point(79, 29)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.RadLabel3
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(313, 20)
        Me.txtDescription.TabIndex = 1
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(10, 30)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel3.TabIndex = 20
        Me.RadLabel3.Text = "Description"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Location = New System.Drawing.Point(12, 89)
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
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(374, 7)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(18, 21)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(79, 7)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.RadLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(295, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(10, 8)
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
        Me.btnClose.Location = New System.Drawing.Point(498, 4)
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
        Me.btnDelete.Location = New System.Drawing.Point(74, 4)
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
        Me.btnSave.Location = New System.Drawing.Point(3, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenuItemExport
        '
        Me.RadMenuItemExport.Name = "RadMenuItemExport"
        Me.RadMenuItemExport.Text = "File"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(569, 162)
        Me.SplitContainer1.SplitterDistance = 131
        Me.SplitContainer1.TabIndex = 0
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(569, 20)
        Me.RadMenu2.TabIndex = 10
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuItemImport, Me.MenuItemExport, Me.MenuItemClose})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'MenuItemImport
        '
        Me.MenuItemImport.Name = "MenuItemImport"
        Me.MenuItemImport.Text = "Import"
        '
        'MenuItemExport
        '
        Me.MenuItemExport.Name = "MenuItemExport"
        Me.MenuItemExport.Text = "Export"
        '
        'MenuItemClose
        '
        Me.MenuItemClose.Name = "MenuItemClose"
        Me.MenuItemClose.Text = "Close"
        '
        'frmPaymentCycleMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(569, 162)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmPaymentCycleMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Payment Cycle Master"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkIsDefault, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboFirstDay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPCType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents cboPCType As common.Controls.MyComboBox
    Friend WithEvents lblCategory As common.Controls.MyLabel
    Friend WithEvents txtValue As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents CboFirstDay As common.Controls.MyComboBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents chkIsDefault As RadCheckBox
End Class

