Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAssetDetails
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtSerialNo = New common.Controls.MyTextBox()
        Me.txtCompany = New common.Controls.MyTextBox()
        Me.lblAssetCode = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtAssetCode = New common.UserControls.txtNavigator()
        Me.txtAssetSubCategory = New common.UserControls.txtFinder()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblAssetDesc = New common.Controls.MyLabel()
        Me.dtpDOP = New common.Controls.MyDateTimePicker()
        Me.txtAssetDesc = New common.Controls.MyTextBox()
        Me.lblDateOfPurchase = New common.Controls.MyLabel()
        Me.lblAssetSpecification = New common.Controls.MyLabel()
        Me.txtAssetSpecs = New common.Controls.MyTextBox()
        Me.lblSerialNo = New common.Controls.MyLabel()
        Me.txtAssetType = New common.UserControls.txtFinder()
        Me.lblAssetType = New common.Controls.MyLabel()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ucCustomFields()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.txtSerialNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAssetCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAssetDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDOP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAssetDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDateOfPurchase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAssetSpecification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAssetSpecs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSerialNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAssetType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        Me.Attachments.SuspendLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(613, 212)
        Me.SplitContainer1.SplitterDistance = 168
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(613, 168)
        Me.RadPageView1.TabIndex = 92
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtSerialNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtCompany)
        Me.RadPageViewPage1.Controls.Add(Me.lblAssetCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtAssetCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtAssetSubCategory)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.lblAssetDesc)
        Me.RadPageViewPage1.Controls.Add(Me.dtpDOP)
        Me.RadPageViewPage1.Controls.Add(Me.txtAssetDesc)
        Me.RadPageViewPage1.Controls.Add(Me.lblDateOfPurchase)
        Me.RadPageViewPage1.Controls.Add(Me.lblAssetSpecification)
        Me.RadPageViewPage1.Controls.Add(Me.txtAssetSpecs)
        Me.RadPageViewPage1.Controls.Add(Me.lblSerialNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtAssetType)
        Me.RadPageViewPage1.Controls.Add(Me.lblAssetType)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(80.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(592, 120)
        Me.RadPageViewPage1.Text = "Asset Details"
        '
        'txtSerialNo
        '
        Me.txtSerialNo.AutoSize = False
        Me.txtSerialNo.Location = New System.Drawing.Point(123, 91)
        Me.txtSerialNo.MaxLength = 50
        Me.txtSerialNo.MendatroryField = False
        Me.txtSerialNo.Multiline = True
        Me.txtSerialNo.MyLinkLable1 = Nothing
        Me.txtSerialNo.MyLinkLable2 = Nothing
        Me.txtSerialNo.Name = "txtSerialNo"
        Me.txtSerialNo.Size = New System.Drawing.Size(195, 20)
        Me.txtSerialNo.TabIndex = 88
        '
        'txtCompany
        '
        Me.txtCompany.AutoSize = False
        Me.txtCompany.Location = New System.Drawing.Point(401, 66)
        Me.txtCompany.MaxLength = 50
        Me.txtCompany.MendatroryField = False
        Me.txtCompany.Multiline = True
        Me.txtCompany.MyLinkLable1 = Nothing
        Me.txtCompany.MyLinkLable2 = Nothing
        Me.txtCompany.Name = "txtCompany"
        Me.txtCompany.Size = New System.Drawing.Size(172, 20)
        Me.txtCompany.TabIndex = 89
        '
        'lblAssetCode
        '
        Me.lblAssetCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblAssetCode.Location = New System.Drawing.Point(10, 17)
        Me.lblAssetCode.Name = "lblAssetCode"
        Me.lblAssetCode.Size = New System.Drawing.Size(62, 18)
        Me.lblAssetCode.TabIndex = 78
        Me.lblAssetCode.Text = "Asset Code"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(324, 67)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel2.TabIndex = 88
        Me.MyLabel2.Text = "Company"
        '
        'txtAssetCode
        '
        Me.txtAssetCode.Location = New System.Drawing.Point(123, 15)
        Me.txtAssetCode.MendatroryField = True
        Me.txtAssetCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtAssetCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtAssetCode.MyLinkLable1 = Nothing
        Me.txtAssetCode.MyLinkLable2 = Nothing
        Me.txtAssetCode.MyMaxLength = 12
        Me.txtAssetCode.MyReadOnly = True
        Me.txtAssetCode.Name = "txtAssetCode"
        Me.txtAssetCode.Size = New System.Drawing.Size(171, 20)
        Me.txtAssetCode.TabIndex = 79
        Me.txtAssetCode.Value = ""
        '
        'txtAssetSubCategory
        '
        Me.txtAssetSubCategory.Location = New System.Drawing.Point(123, 66)
        Me.txtAssetSubCategory.MendatroryField = True
        Me.txtAssetSubCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAssetSubCategory.MyLinkLable1 = Nothing
        Me.txtAssetSubCategory.MyLinkLable2 = Nothing
        Me.txtAssetSubCategory.MyReadOnly = True
        Me.txtAssetSubCategory.MyShowMasterFormButton = False
        Me.txtAssetSubCategory.Name = "txtAssetSubCategory"
        Me.txtAssetSubCategory.Size = New System.Drawing.Size(195, 19)
        Me.txtAssetSubCategory.TabIndex = 91
        Me.txtAssetSubCategory.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(297, 15)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 20)
        Me.btnAddNew.TabIndex = 80
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(11, 69)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(107, 16)
        Me.MyLabel1.TabIndex = 87
        Me.MyLabel1.Text = "Asset Sub Category"
        '
        'lblAssetDesc
        '
        Me.lblAssetDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssetDesc.Location = New System.Drawing.Point(323, 19)
        Me.lblAssetDesc.Name = "lblAssetDesc"
        Me.lblAssetDesc.Size = New System.Drawing.Size(94, 16)
        Me.lblAssetDesc.TabIndex = 81
        Me.lblAssetDesc.Text = "Asset Description"
        '
        'dtpDOP
        '
        Me.dtpDOP.CustomFormat = "dd/MM/yyyy"
        Me.dtpDOP.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDOP.Location = New System.Drawing.Point(401, 91)
        Me.dtpDOP.MendatroryField = False
        Me.dtpDOP.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDOP.MyLinkLable1 = Nothing
        Me.dtpDOP.MyLinkLable2 = Nothing
        Me.dtpDOP.Name = "dtpDOP"
        Me.dtpDOP.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDOP.Size = New System.Drawing.Size(171, 20)
        Me.dtpDOP.TabIndex = 90
        Me.dtpDOP.TabStop = False
        Me.dtpDOP.Text = "11/10/2013"
        Me.dtpDOP.Value = New Date(2013, 10, 11, 0, 0, 0, 0)
        '
        'txtAssetDesc
        '
        Me.txtAssetDesc.AutoSize = False
        Me.txtAssetDesc.Location = New System.Drawing.Point(423, 15)
        Me.txtAssetDesc.MaxLength = 50
        Me.txtAssetDesc.MendatroryField = True
        Me.txtAssetDesc.Multiline = True
        Me.txtAssetDesc.MyLinkLable1 = Nothing
        Me.txtAssetDesc.MyLinkLable2 = Nothing
        Me.txtAssetDesc.Name = "txtAssetDesc"
        Me.txtAssetDesc.Size = New System.Drawing.Size(149, 20)
        Me.txtAssetDesc.TabIndex = 82
        '
        'lblDateOfPurchase
        '
        Me.lblDateOfPurchase.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateOfPurchase.Location = New System.Drawing.Point(323, 94)
        Me.lblDateOfPurchase.Name = "lblDateOfPurchase"
        Me.lblDateOfPurchase.Size = New System.Drawing.Size(77, 16)
        Me.lblDateOfPurchase.TabIndex = 89
        Me.lblDateOfPurchase.Text = "Date Of Asset"
        '
        'lblAssetSpecification
        '
        Me.lblAssetSpecification.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssetSpecification.Location = New System.Drawing.Point(10, 43)
        Me.lblAssetSpecification.Name = "lblAssetSpecification"
        Me.lblAssetSpecification.Size = New System.Drawing.Size(102, 16)
        Me.lblAssetSpecification.TabIndex = 83
        Me.lblAssetSpecification.Text = "Asset Specification"
        '
        'txtAssetSpecs
        '
        Me.txtAssetSpecs.AutoSize = False
        Me.txtAssetSpecs.Location = New System.Drawing.Point(123, 40)
        Me.txtAssetSpecs.MaxLength = 50
        Me.txtAssetSpecs.MendatroryField = True
        Me.txtAssetSpecs.Multiline = True
        Me.txtAssetSpecs.MyLinkLable1 = Nothing
        Me.txtAssetSpecs.MyLinkLable2 = Nothing
        Me.txtAssetSpecs.Name = "txtAssetSpecs"
        Me.txtAssetSpecs.Size = New System.Drawing.Size(195, 20)
        Me.txtAssetSpecs.TabIndex = 84
        '
        'lblSerialNo
        '
        Me.lblSerialNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSerialNo.Location = New System.Drawing.Point(11, 95)
        Me.lblSerialNo.Name = "lblSerialNo"
        Me.lblSerialNo.Size = New System.Drawing.Size(53, 16)
        Me.lblSerialNo.TabIndex = 87
        Me.lblSerialNo.Text = "Serial No"
        '
        'txtAssetType
        '
        Me.txtAssetType.Location = New System.Drawing.Point(414, 41)
        Me.txtAssetType.MendatroryField = True
        Me.txtAssetType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAssetType.MyLinkLable1 = Nothing
        Me.txtAssetType.MyLinkLable2 = Nothing
        Me.txtAssetType.MyReadOnly = True
        Me.txtAssetType.MyShowMasterFormButton = False
        Me.txtAssetType.Name = "txtAssetType"
        Me.txtAssetType.Size = New System.Drawing.Size(158, 19)
        Me.txtAssetType.TabIndex = 85
        Me.txtAssetType.Value = ""
        '
        'lblAssetType
        '
        Me.lblAssetType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssetType.Location = New System.Drawing.Point(324, 44)
        Me.lblAssetType.Name = "lblAssetType"
        Me.lblAssetType.Size = New System.Drawing.Size(84, 16)
        Me.lblAssetType.TabIndex = 86
        Me.lblAssetType.Text = "Asset Category"
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(86.0!, 28.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(967, 484)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(967, 484)
        Me.UcCustomFields1.TabIndex = 1
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 28.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 37)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(967, 464)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(967, 464)
        Me.UcAttachment1.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(537, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 19)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(88, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 19)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(13, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 19)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'FrmAssetDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(613, 212)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmAssetDetails"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Asset Details"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.txtSerialNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAssetCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAssetDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDOP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAssetDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDateOfPurchase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAssetSpecification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAssetSpecs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSerialNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAssetType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        Me.Attachments.ResumeLayout(False)
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents dtpDOP As common.Controls.MyDateTimePicker
    Friend WithEvents lblDateOfPurchase As common.Controls.MyLabel
    Friend WithEvents txtSerialNo As common.Controls.MyTextBox
    Friend WithEvents lblSerialNo As common.Controls.MyLabel
    Friend WithEvents lblAssetType As common.Controls.MyLabel
    Friend WithEvents txtAssetType As common.UserControls.txtFinder
    Friend WithEvents txtAssetSpecs As common.Controls.MyTextBox
    Friend WithEvents lblAssetSpecification As common.Controls.MyLabel
    Friend WithEvents txtAssetDesc As common.Controls.MyTextBox
    Friend WithEvents lblAssetDesc As common.Controls.MyLabel
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtAssetCode As common.UserControls.txtNavigator
    Friend WithEvents lblAssetCode As common.Controls.MyLabel
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtAssetSubCategory As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtCompany As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ucCustomFields
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
End Class

