Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmJWParameterMaster
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkMOISTURE = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadCheckBox1 = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.cboType = New common.Controls.MyComboBox()
        Me.fndParamter = New common.UserControls.txtFinder()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenu = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.GroupBox1.SuspendLayout()
        CType(Me.chkMOISTURE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadCheckBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkMOISTURE)
        Me.GroupBox1.Controls.Add(Me.RadCheckBox1)
        Me.GroupBox1.Controls.Add(Me.MyLabel1)
        Me.GroupBox1.Controls.Add(Me.cboType)
        Me.GroupBox1.Controls.Add(Me.fndParamter)
        Me.GroupBox1.Controls.Add(Me.RadLabel3)
        Me.GroupBox1.Controls.Add(Me.txtDesc)
        Me.GroupBox1.Controls.Add(Me.RadLabel1)
        Me.GroupBox1.Controls.Add(Me.btnAddNew)
        Me.GroupBox1.Controls.Add(Me.txtCode)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 20)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(791, 158)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'chkMOISTURE
        '
        Me.chkMOISTURE.Location = New System.Drawing.Point(93, 87)
        Me.chkMOISTURE.Name = "chkMOISTURE"
        Me.chkMOISTURE.Size = New System.Drawing.Size(74, 18)
        Me.chkMOISTURE.TabIndex = 326
        Me.chkMOISTURE.Text = "MOISTURE"
        '
        'RadCheckBox1
        '
        Me.RadCheckBox1.Location = New System.Drawing.Point(380, 17)
        Me.RadCheckBox1.Name = "RadCheckBox1"
        Me.RadCheckBox1.Size = New System.Drawing.Size(109, 18)
        Me.RadCheckBox1.TabIndex = 323
        Me.RadCheckBox1.Text = "Parameter Master"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 63)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel1.TabIndex = 56
        Me.MyLabel1.Text = "Type"
        '
        'cboType
        '
        Me.cboType.AutoCompleteDisplayMember = Nothing
        Me.cboType.AutoCompleteValueMember = Nothing
        Me.cboType.CalculationExpression = Nothing
        Me.cboType.DropDownAnimationEnabled = True
        Me.cboType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboType.FieldCode = Nothing
        Me.cboType.FieldDesc = Nothing
        Me.cboType.FieldMaxLength = 0
        Me.cboType.FieldName = Nothing
        Me.cboType.isCalculatedField = False
        Me.cboType.IsSourceFromTable = False
        Me.cboType.IsSourceFromValueList = False
        Me.cboType.IsUnique = False
        Me.cboType.Location = New System.Drawing.Point(93, 61)
        Me.cboType.MendatroryField = False
        Me.cboType.MyLinkLable1 = Nothing
        Me.cboType.MyLinkLable2 = Nothing
        Me.cboType.Name = "cboType"
        Me.cboType.ReferenceFieldDesc = Nothing
        Me.cboType.ReferenceFieldName = Nothing
        Me.cboType.ReferenceTableName = Nothing
        Me.cboType.Size = New System.Drawing.Size(115, 20)
        Me.cboType.TabIndex = 55
        '
        'fndParamter
        '
        Me.fndParamter.CalculationExpression = Nothing
        Me.fndParamter.FieldCode = Nothing
        Me.fndParamter.FieldDesc = Nothing
        Me.fndParamter.FieldMaxLength = 0
        Me.fndParamter.FieldName = Nothing
        Me.fndParamter.isCalculatedField = False
        Me.fndParamter.IsSourceFromTable = False
        Me.fndParamter.IsSourceFromValueList = False
        Me.fndParamter.IsUnique = False
        Me.fndParamter.Location = New System.Drawing.Point(499, 16)
        Me.fndParamter.MendatroryField = True
        Me.fndParamter.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndParamter.MyLinkLable1 = Nothing
        Me.fndParamter.MyLinkLable2 = Nothing
        Me.fndParamter.MyReadOnly = False
        Me.fndParamter.MyShowMasterFormButton = False
        Me.fndParamter.Name = "fndParamter"
        Me.fndParamter.ReferenceFieldDesc = Nothing
        Me.fndParamter.ReferenceFieldName = Nothing
        Me.fndParamter.ReferenceTableName = Nothing
        Me.fndParamter.Size = New System.Drawing.Size(162, 20)
        Me.fndParamter.TabIndex = 325
        Me.fndParamter.Value = ""
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(6, 41)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel3.TabIndex = 27
        Me.RadLabel3.Text = "Description"
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
        Me.txtDesc.Location = New System.Drawing.Point(93, 40)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = True
        Me.txtDesc.MyLinkLable1 = Me.RadLabel3
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(281, 18)
        Me.txtDesc.TabIndex = 2
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(6, 18)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel1.TabIndex = 25
        Me.RadLabel1.Text = "Code"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPJobWorkOutward.My.Resources.Resources.new1
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(354, 16)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 20)
        Me.btnAddNew.TabIndex = 0
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(93, 16)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.RadLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(261, 20)
        Me.txtCode.TabIndex = 1
        Me.txtCode.Value = ""
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(4, 456)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(876, 456)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(77, 456)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenu})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(948, 20)
        Me.RadMenu1.TabIndex = 322
        '
        'RadMenu
        '
        Me.RadMenu.AccessibleDescription = "Setting"
        Me.RadMenu.AccessibleName = "Setting"
        Me.RadMenu.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmImport, Me.rmExport})
        Me.RadMenu.Name = "RadMenu"
        Me.RadMenu.Text = "File"
        '
        'rmImport
        '
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
        '
        'rmExport
        '
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export"
        '
        'frmJWParameterMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(948, 479)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmJWParameterMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Parameter Master"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.chkMOISTURE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadCheckBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents cboType As common.Controls.MyComboBox
    Friend WithEvents fndParamter As common.UserControls.txtFinder
    Friend WithEvents RadCheckBox1 As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkMOISTURE As Telerik.WinControls.UI.RadCheckBox
End Class

