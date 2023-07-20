<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDCSforSale
    'Inherits System.Windows.Forms.Form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDCSforSale))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblCustomerDesc = New common.Controls.MyLabel()
        Me.lblCustomer = New common.Controls.MyLabel()
        Me.lblZoneDesc = New common.Controls.MyLabel()
        Me.txtCustomer = New common.UserControls.txtFinder()
        Me.txtZone = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.txtUpload = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtName = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.lblZone = New common.Controls.MyLabel()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblCustomerDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblZoneDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUpload, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblZone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCustomerDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCustomer)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblZoneDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustomer)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtZone)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtUpload)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblZone)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(636, 224)
        Me.SplitContainer1.SplitterDistance = 175
        Me.SplitContainer1.TabIndex = 0
        '
        'lblCustomerDesc
        '
        Me.lblCustomerDesc.AutoSize = False
        Me.lblCustomerDesc.BorderVisible = True
        Me.lblCustomerDesc.FieldName = Nothing
        Me.lblCustomerDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerDesc.Location = New System.Drawing.Point(373, 125)
        Me.lblCustomerDesc.Name = "lblCustomerDesc"
        Me.lblCustomerDesc.Size = New System.Drawing.Size(242, 18)
        Me.lblCustomerDesc.TabIndex = 59
        Me.lblCustomerDesc.TextWrap = False
        '
        'lblCustomer
        '
        Me.lblCustomer.FieldName = Nothing
        Me.lblCustomer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(18, 128)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(55, 16)
        Me.lblCustomer.TabIndex = 57
        Me.lblCustomer.Text = "Customer"
        '
        'lblZoneDesc
        '
        Me.lblZoneDesc.AutoSize = False
        Me.lblZoneDesc.BorderVisible = True
        Me.lblZoneDesc.FieldName = Nothing
        Me.lblZoneDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZoneDesc.Location = New System.Drawing.Point(375, 79)
        Me.lblZoneDesc.Name = "lblZoneDesc"
        Me.lblZoneDesc.Size = New System.Drawing.Size(242, 18)
        Me.lblZoneDesc.TabIndex = 58
        Me.lblZoneDesc.TextWrap = False
        '
        'txtCustomer
        '
        Me.txtCustomer.CalculationExpression = Nothing
        Me.txtCustomer.Enabled = False
        Me.txtCustomer.FieldCode = Nothing
        Me.txtCustomer.FieldDesc = Nothing
        Me.txtCustomer.FieldMaxLength = 0
        Me.txtCustomer.FieldName = Nothing
        Me.txtCustomer.isCalculatedField = False
        Me.txtCustomer.IsSourceFromTable = False
        Me.txtCustomer.IsSourceFromValueList = False
        Me.txtCustomer.IsUnique = False
        Me.txtCustomer.Location = New System.Drawing.Point(114, 125)
        Me.txtCustomer.MendatroryField = True
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Me.lblCustomer
        Me.txtCustomer.MyLinkLable2 = Nothing
        Me.txtCustomer.MyReadOnly = False
        Me.txtCustomer.MyShowMasterFormButton = False
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.ReferenceFieldDesc = Nothing
        Me.txtCustomer.ReferenceFieldName = Nothing
        Me.txtCustomer.ReferenceTableName = Nothing
        Me.txtCustomer.Size = New System.Drawing.Size(253, 19)
        Me.txtCustomer.TabIndex = 55
        Me.txtCustomer.Value = ""
        '
        'txtZone
        '
        Me.txtZone.CalculationExpression = Nothing
        Me.txtZone.FieldCode = Nothing
        Me.txtZone.FieldDesc = Nothing
        Me.txtZone.FieldMaxLength = 0
        Me.txtZone.FieldName = Nothing
        Me.txtZone.isCalculatedField = False
        Me.txtZone.IsSourceFromTable = False
        Me.txtZone.IsSourceFromValueList = False
        Me.txtZone.IsUnique = False
        Me.txtZone.Location = New System.Drawing.Point(114, 79)
        Me.txtZone.MendatroryField = True
        Me.txtZone.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtZone.MyLinkLable1 = Me.lblLocation
        Me.txtZone.MyLinkLable2 = Nothing
        Me.txtZone.MyReadOnly = False
        Me.txtZone.MyShowMasterFormButton = False
        Me.txtZone.Name = "txtZone"
        Me.txtZone.ReferenceFieldDesc = Nothing
        Me.txtZone.ReferenceFieldName = Nothing
        Me.txtZone.ReferenceTableName = Nothing
        Me.txtZone.Size = New System.Drawing.Size(253, 19)
        Me.txtZone.TabIndex = 56
        Me.txtZone.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(18, 104)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 53
        Me.lblLocation.Text = "Location"
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.FieldName = Nothing
        Me.lblLocationDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationDesc.Location = New System.Drawing.Point(373, 101)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(242, 18)
        Me.lblLocationDesc.TabIndex = 54
        Me.lblLocationDesc.TextWrap = False
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
        Me.txtLocation.Location = New System.Drawing.Point(114, 101)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblLocation
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(253, 19)
        Me.txtLocation.TabIndex = 52
        Me.txtLocation.Value = ""
        '
        'txtUpload
        '
        Me.txtUpload.CalculationExpression = Nothing
        Me.txtUpload.FieldCode = Nothing
        Me.txtUpload.FieldDesc = Nothing
        Me.txtUpload.FieldMaxLength = 0
        Me.txtUpload.FieldName = Nothing
        Me.txtUpload.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUpload.isCalculatedField = False
        Me.txtUpload.IsSourceFromTable = False
        Me.txtUpload.IsSourceFromValueList = False
        Me.txtUpload.IsUnique = False
        Me.txtUpload.Location = New System.Drawing.Point(114, 55)
        Me.txtUpload.MaxLength = 50
        Me.txtUpload.MendatroryField = False
        Me.txtUpload.Multiline = True
        Me.txtUpload.MyLinkLable1 = Me.MyLabel2
        Me.txtUpload.MyLinkLable2 = Nothing
        Me.txtUpload.Name = "txtUpload"
        Me.txtUpload.ReferenceFieldDesc = Nothing
        Me.txtUpload.ReferenceFieldName = Nothing
        Me.txtUpload.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtUpload.RootElement.StretchVertically = True
        Me.txtUpload.Size = New System.Drawing.Size(253, 21)
        Me.txtUpload.TabIndex = 51
        Me.txtUpload.Text = " "
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(18, 60)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel2.TabIndex = 46
        Me.MyLabel2.Text = "Uploader No"
        '
        'txtName
        '
        Me.txtName.CalculationExpression = Nothing
        Me.txtName.FieldCode = Nothing
        Me.txtName.FieldDesc = Nothing
        Me.txtName.FieldMaxLength = 0
        Me.txtName.FieldName = Nothing
        Me.txtName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.isCalculatedField = False
        Me.txtName.IsSourceFromTable = False
        Me.txtName.IsSourceFromValueList = False
        Me.txtName.IsUnique = False
        Me.txtName.Location = New System.Drawing.Point(114, 33)
        Me.txtName.MaxLength = 50
        Me.txtName.MendatroryField = False
        Me.txtName.Multiline = True
        Me.txtName.MyLinkLable1 = Me.MyLabel1
        Me.txtName.MyLinkLable2 = Nothing
        Me.txtName.Name = "txtName"
        Me.txtName.ReferenceFieldDesc = Nothing
        Me.txtName.ReferenceFieldName = Nothing
        Me.txtName.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtName.RootElement.StretchVertically = True
        Me.txtName.Size = New System.Drawing.Size(253, 21)
        Me.txtName.TabIndex = 50
        Me.txtName.Text = " "
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(18, 38)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel1.TabIndex = 45
        Me.MyLabel1.Text = "DCS Name"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(366, 11)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(20, 21)
        Me.btnNew.TabIndex = 49
        Me.btnNew.Text = " "
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(114, 11)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblDescription
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(252, 21)
        Me.txtCode.TabIndex = 48
        Me.txtCode.Value = ""
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(18, 16)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(33, 16)
        Me.lblDescription.TabIndex = 44
        Me.lblDescription.Text = "Code"
        '
        'lblZone
        '
        Me.lblZone.FieldName = Nothing
        Me.lblZone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZone.Location = New System.Drawing.Point(18, 82)
        Me.lblZone.Name = "lblZone"
        Me.lblZone.Size = New System.Drawing.Size(32, 16)
        Me.lblZone.TabIndex = 47
        Me.lblZone.Text = "Zone"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(556, 15)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 24)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(86, 15)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 24)
        Me.btnDelete.TabIndex = 6
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(12, 15)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 24)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save"
        '
        'rdmenufile1
        '
        Me.rdmenufile1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenuimport, Me.rdmenuexport})
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "File"
        Me.rdmenufile1.UseCompatibleTextRendering = False
        '
        'rdmenuimport
        '
        Me.rdmenuimport.Name = "rdmenuimport"
        Me.rdmenuimport.Text = "Import"
        Me.rdmenuimport.UseCompatibleTextRendering = False
        '
        'rdmenuexport
        '
        Me.rdmenuexport.AccessibleDescription = "RadMenuItem1"
        Me.rdmenuexport.AccessibleName = "RadMenuItem1"
        Me.rdmenuexport.Name = "rdmenuexport"
        Me.rdmenuexport.Text = "Export"
        Me.rdmenuexport.UseCompatibleTextRendering = False
        '
        'rdmenufile
        '
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(636, 20)
        Me.rdmenufile.TabIndex = 8
        '
        'frmDCSforSale
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(636, 244)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Name = "frmDCSforSale"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmDCSforSale"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblCustomerDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblZoneDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUpload, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblZone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnSave As RadButton
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents rdmenufile1 As RadMenuItem
    Friend WithEvents rdmenuimport As RadMenuItem
    Friend WithEvents rdmenuexport As RadMenuItem
    Friend WithEvents rdmenufile As RadMenu
    Friend WithEvents lblCustomerDesc As common.Controls.MyLabel
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents lblZoneDesc As common.Controls.MyLabel
    Friend WithEvents txtCustomer As common.UserControls.txtFinder
    Friend WithEvents txtZone As common.UserControls.txtFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents txtUpload As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtName As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnNew As RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents lblZone As common.Controls.MyLabel
End Class
