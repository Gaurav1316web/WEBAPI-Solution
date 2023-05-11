Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAMAcquisitionCode
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
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.lblDefaultAcct = New common.Controls.MyLabel()
        Me.txtAccount = New common.UserControls.txtFinder()
        Me.txtVendor = New common.UserControls.txtFinder()
        Me.cmbType = New common.Controls.MyComboBox()
        Me.cmbAcqWith = New common.Controls.MyComboBox()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtAcqCode = New common.UserControls.txtNavigator()
        Me.RadLabel6 = New Telerik.WinControls.UI.RadLabel()
        Me.lblVenor = New Telerik.WinControls.UI.RadLabel()
        Me.lblType = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel3 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDefaultAcct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbAcqWith, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVenor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.lblVendorName)
        Me.RadGroupBox1.Controls.Add(Me.lblDefaultAcct)
        Me.RadGroupBox1.Controls.Add(Me.txtAccount)
        Me.RadGroupBox1.Controls.Add(Me.txtVendor)
        Me.RadGroupBox1.Controls.Add(Me.cmbType)
        Me.RadGroupBox1.Controls.Add(Me.cmbAcqWith)
        Me.RadGroupBox1.Controls.Add(Me.txtDesc)
        Me.RadGroupBox1.Controls.Add(Me.btnAddNew)
        Me.RadGroupBox1.Controls.Add(Me.txtAcqCode)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel6)
        Me.RadGroupBox1.Controls.Add(Me.lblVenor)
        Me.RadGroupBox1.Controls.Add(Me.lblType)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(1, 25)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(717, 138)
        Me.RadGroupBox1.TabIndex = 0
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorName.Location = New System.Drawing.Point(275, 107)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(429, 18)
        Me.lblVendorName.TabIndex = 56
        Me.lblVendorName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVendorName.TextWrap = False
        Me.lblVendorName.Visible = False
        '
        'lblDefaultAcct
        '
        Me.lblDefaultAcct.AutoSize = False
        Me.lblDefaultAcct.BorderVisible = True
        Me.lblDefaultAcct.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDefaultAcct.Location = New System.Drawing.Point(275, 83)
        Me.lblDefaultAcct.Name = "lblDefaultAcct"
        Me.lblDefaultAcct.Size = New System.Drawing.Size(429, 18)
        Me.lblDefaultAcct.TabIndex = 55
        Me.lblDefaultAcct.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDefaultAcct.TextWrap = False
        '
        'txtAccount
        '
        Me.txtAccount.Location = New System.Drawing.Point(110, 83)
        Me.txtAccount.MendatroryField = True
        Me.txtAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccount.MyLinkLable1 = Nothing
        Me.txtAccount.MyLinkLable2 = Nothing
        Me.txtAccount.MyReadOnly = False
        Me.txtAccount.MyShowMasterFormButton = False
        Me.txtAccount.Name = "txtAccount"
        Me.txtAccount.Size = New System.Drawing.Size(159, 18)
        Me.txtAccount.TabIndex = 5
        Me.txtAccount.Value = ""
        '
        'txtVendor
        '
        Me.txtVendor.Location = New System.Drawing.Point(111, 106)
        Me.txtVendor.MendatroryField = True
        Me.txtVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendor.MyLinkLable1 = Nothing
        Me.txtVendor.MyLinkLable2 = Nothing
        Me.txtVendor.MyReadOnly = False
        Me.txtVendor.MyShowMasterFormButton = False
        Me.txtVendor.Name = "txtVendor"
        Me.txtVendor.Size = New System.Drawing.Size(158, 18)
        Me.txtVendor.TabIndex = 6
        Me.txtVendor.Value = ""
        Me.txtVendor.Visible = False
        '
        'cmbType
        '
        Me.cmbType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem5.Text = "Create A/P Invoice"
        RadListDataItem6.Text = "Create Misc Payment"
        Me.cmbType.Items.Add(RadListDataItem5)
        Me.cmbType.Items.Add(RadListDataItem6)
        Me.cmbType.Location = New System.Drawing.Point(351, 59)
        Me.cmbType.MendatroryField = False
        Me.cmbType.MyLinkLable1 = Nothing
        Me.cmbType.MyLinkLable2 = Nothing
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(159, 18)
        Me.cmbType.TabIndex = 4
        '
        'cmbAcqWith
        '
        Me.cmbAcqWith.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbAcqWith.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "AP"
        RadListDataItem2.Text = "GL"
        Me.cmbAcqWith.Items.Add(RadListDataItem1)
        Me.cmbAcqWith.Items.Add(RadListDataItem2)
        Me.cmbAcqWith.Location = New System.Drawing.Point(110, 59)
        Me.cmbAcqWith.MendatroryField = False
        Me.cmbAcqWith.MyLinkLable1 = Nothing
        Me.cmbAcqWith.MyLinkLable2 = Nothing
        Me.cmbAcqWith.Name = "cmbAcqWith"
        Me.cmbAcqWith.Size = New System.Drawing.Size(159, 18)
        Me.cmbAcqWith.TabIndex = 3
        '
        'txtDesc
        '
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(110, 38)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Nothing
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(594, 18)
        Me.txtDesc.TabIndex = 2
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPFixedAssets.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(377, 11)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 0
        '
        'txtAcqCode
        '
        Me.txtAcqCode.Location = New System.Drawing.Point(110, 11)
        Me.txtAcqCode.MendatroryField = False
        Me.txtAcqCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtAcqCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtAcqCode.MyLinkLable1 = Nothing
        Me.txtAcqCode.MyLinkLable2 = Nothing
        Me.txtAcqCode.MyMaxLength = 32767
        Me.txtAcqCode.MyReadOnly = False
        Me.txtAcqCode.Name = "txtAcqCode"
        Me.txtAcqCode.Size = New System.Drawing.Size(264, 20)
        Me.txtAcqCode.TabIndex = 1
        Me.txtAcqCode.Value = ""
        '
        'RadLabel6
        '
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(13, 83)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(86, 16)
        Me.RadLabel6.TabIndex = 1
        Me.RadLabel6.Text = "Default Account"
        '
        'lblVenor
        '
        Me.lblVenor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVenor.Location = New System.Drawing.Point(13, 107)
        Me.lblVenor.Name = "lblVenor"
        Me.lblVenor.Size = New System.Drawing.Size(82, 16)
        Me.lblVenor.TabIndex = 3
        Me.lblVenor.Text = "Default Vendor"
        Me.lblVenor.Visible = False
        '
        'lblType
        '
        Me.lblType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.Location = New System.Drawing.Point(304, 59)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(31, 16)
        Me.lblType.TabIndex = 2
        Me.lblType.Text = "Type"
        '
        'RadLabel3
        '
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(13, 59)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(87, 16)
        Me.RadLabel3.TabIndex = 1
        Me.RadLabel3.Text = "Acquisition With"
        '
        'RadLabel2
        '
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(13, 35)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel2.TabIndex = 1
        Me.RadLabel2.Text = "Description"
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(13, 11)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(91, 16)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "Acquisition Code"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(74, 214)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(649, 214)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(1, 214)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(720, 20)
        Me.RadMenu1.TabIndex = 323
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmImport, Me.rmExport})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'rmImport
        '
        Me.rmImport.AccessibleDescription = "Import"
        Me.rmImport.AccessibleName = "Import"
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
        '
        'rmExport
        '
        Me.rmExport.AccessibleDescription = "Export"
        Me.rmExport.AccessibleName = "Export"
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export"
        '
        'FrmAMAcquisitionCode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(720, 242)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "FrmAMAcquisitionCode"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Acquisition Codes"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDefaultAcct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbAcqWith, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVenor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel3 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblType As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel6 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblVenor As Telerik.WinControls.UI.RadLabel
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtAcqCode As common.UserControls.txtNavigator
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents cmbType As common.Controls.MyComboBox
    Friend WithEvents cmbAcqWith As common.Controls.MyComboBox
    Friend WithEvents txtAccount As common.UserControls.txtFinder
    Friend WithEvents txtVendor As common.UserControls.txtFinder
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents lblDefaultAcct As common.Controls.MyLabel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
End Class

