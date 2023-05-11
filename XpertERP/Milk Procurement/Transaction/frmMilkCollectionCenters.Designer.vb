<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMilkCollectionCenters
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
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.lblCollectionLevel = New common.Controls.MyLabel
        Me.lblcollectionCenterCode = New common.Controls.MyLabel
        Me.lblDescription = New common.Controls.MyLabel
        Me.txtDescription = New common.Controls.MyTextBox
        Me.txtCollectionCenters = New common.UserControls.txtNavigator
        Me.txtCollectionLevel = New common.UserControls.txtFinder
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.txtPhone2 = New common.Controls.MyTextBox
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.txtPhone1 = New common.Controls.MyTextBox
        Me.lblPhone1 = New common.Controls.MyLabel
        Me.txtstateprovince = New Telerik.WinControls.UI.RadTextBox
        Me.fndstate = New common.UserControls.txtFinder
        Me.lblStateProvince = New common.Controls.MyLabel
        Me.lblEmail = New common.Controls.MyLabel
        Me.txtEmail = New common.Controls.MyTextBox
        Me.txtTelephone = New common.Controls.MyTextBox
        Me.lblTelephone = New common.Controls.MyLabel
        Me.lblCountry = New common.Controls.MyLabel
        Me.txtCountry = New common.Controls.MyTextBox
        Me.txtZipPostalCode = New common.Controls.MyTextBox
        Me.lblZipPostalCode = New common.Controls.MyLabel
        Me.txtCity = New common.Controls.MyTextBox
        Me.lblCity = New common.Controls.MyLabel
        Me.txtAdd4 = New common.Controls.MyTextBox
        Me.lblAddress = New common.Controls.MyLabel
        Me.txtAdd3 = New common.Controls.MyTextBox
        Me.txtAdd2 = New common.Controls.MyTextBox
        Me.txtAdd1 = New common.Controls.MyTextBox
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCollectionLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcollectionCenterCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtPhone2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPhone1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPhone1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtstateprovince, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStateProvince, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTelephone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTelephone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtZipPostalCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblZipPostalCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(371, 24)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(16, 20)
        Me.btnnew.TabIndex = 1
        '
        'lblCollectionLevel
        '
        Me.lblCollectionLevel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCollectionLevel.Location = New System.Drawing.Point(13, 77)
        Me.lblCollectionLevel.Name = "lblCollectionLevel"
        Me.lblCollectionLevel.Size = New System.Drawing.Size(86, 16)
        Me.lblCollectionLevel.TabIndex = 190
        Me.lblCollectionLevel.Text = "Collection Level"
        '
        'lblcollectionCenterCode
        '
        Me.lblcollectionCenterCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblcollectionCenterCode.Location = New System.Drawing.Point(13, 29)
        Me.lblcollectionCenterCode.Name = "lblcollectionCenterCode"
        Me.lblcollectionCenterCode.Size = New System.Drawing.Size(131, 16)
        Me.lblcollectionCenterCode.TabIndex = 186
        Me.lblcollectionCenterCode.Text = "Collection Center Code"
        '
        'lblDescription
        '
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(13, 53)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 185
        Me.lblDescription.Text = "Description"
        '
        'txtDescription
        '
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(150, 50)
        Me.txtDescription.MaxLength = 49
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.lblDescription
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(215, 18)
        Me.txtDescription.TabIndex = 2
        '
        'txtCollectionCenters
        '
        Me.txtCollectionCenters.Location = New System.Drawing.Point(150, 24)
        Me.txtCollectionCenters.MendatroryField = True
        Me.txtCollectionCenters.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCollectionCenters.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCollectionCenters.MyLinkLable1 = Me.lblcollectionCenterCode
        Me.txtCollectionCenters.MyLinkLable2 = Nothing
        Me.txtCollectionCenters.MyMaxLength = 12
        Me.txtCollectionCenters.MyReadOnly = True
        Me.txtCollectionCenters.Name = "txtCollectionCenters"
        Me.txtCollectionCenters.Size = New System.Drawing.Size(215, 21)
        Me.txtCollectionCenters.TabIndex = 0
        Me.txtCollectionCenters.Value = ""
        '
        'txtCollectionLevel
        '
        Me.txtCollectionLevel.Location = New System.Drawing.Point(150, 73)
        Me.txtCollectionLevel.MendatroryField = True
        Me.txtCollectionLevel.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCollectionLevel.MyLinkLable1 = Nothing
        Me.txtCollectionLevel.MyLinkLable2 = Nothing
        Me.txtCollectionLevel.MyReadOnly = False
        Me.txtCollectionLevel.Name = "txtCollectionLevel"
        Me.txtCollectionLevel.Size = New System.Drawing.Size(215, 19)
        Me.txtCollectionLevel.TabIndex = 3
        Me.txtCollectionLevel.Value = ""
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPhone2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPhone1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPhone1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtstateprovince)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndstate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEmail)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEmail)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTelephone)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTelephone)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCountry)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCountry)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtZipPostalCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblZipPostalCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblStateProvince)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCity)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCity)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAdd4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAdd3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAdd2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAdd1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAddress)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblcollectionCenterCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCollectionLevel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCollectionCenters)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCollectionLevel)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(749, 485)
        Me.SplitContainer1.SplitterDistance = 442
        Me.SplitContainer1.TabIndex = 193
        '
        'txtPhone2
        '
        Me.txtPhone2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhone2.Location = New System.Drawing.Point(573, 275)
        Me.txtPhone2.MaxLength = 50
        Me.txtPhone2.MendatroryField = False
        Me.txtPhone2.MyLinkLable1 = Me.MyLabel3
        Me.txtPhone2.MyLinkLable2 = Nothing
        Me.txtPhone2.Name = "txtPhone2"
        Me.txtPhone2.Size = New System.Drawing.Size(130, 18)
        Me.txtPhone2.TabIndex = 15
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(495, 277)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel3.TabIndex = 214
        Me.MyLabel3.Text = "Phone2"
        '
        'txtPhone1
        '
        Me.txtPhone1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhone1.Location = New System.Drawing.Point(355, 276)
        Me.txtPhone1.MaxLength = 50
        Me.txtPhone1.MendatroryField = False
        Me.txtPhone1.MyLinkLable1 = Me.lblPhone1
        Me.txtPhone1.MyLinkLable2 = Nothing
        Me.txtPhone1.Name = "txtPhone1"
        Me.txtPhone1.Size = New System.Drawing.Size(130, 18)
        Me.txtPhone1.TabIndex = 14
        '
        'lblPhone1
        '
        Me.lblPhone1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhone1.Location = New System.Drawing.Point(296, 277)
        Me.lblPhone1.Name = "lblPhone1"
        Me.lblPhone1.Size = New System.Drawing.Size(45, 16)
        Me.lblPhone1.TabIndex = 212
        Me.lblPhone1.Text = "Phone1"
        '
        'txtstateprovince
        '
        Me.txtstateprovince.Location = New System.Drawing.Point(352, 206)
        Me.txtstateprovince.Name = "txtstateprovince"
        Me.txtstateprovince.Size = New System.Drawing.Size(107, 20)
        Me.txtstateprovince.TabIndex = 10
        '
        'fndstate
        '
        Me.fndstate.Location = New System.Drawing.Point(150, 207)
        Me.fndstate.MendatroryField = False
        Me.fndstate.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndstate.MyLinkLable1 = Me.lblStateProvince
        Me.fndstate.MyLinkLable2 = Nothing
        Me.fndstate.MyReadOnly = False
        Me.fndstate.Name = "fndstate"
        Me.fndstate.Size = New System.Drawing.Size(193, 19)
        Me.fndstate.TabIndex = 9
        Me.fndstate.Value = ""
        '
        'lblStateProvince
        '
        Me.lblStateProvince.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStateProvince.Location = New System.Drawing.Point(13, 210)
        Me.lblStateProvince.Name = "lblStateProvince"
        Me.lblStateProvince.Size = New System.Drawing.Size(89, 16)
        Me.lblStateProvince.TabIndex = 202
        Me.lblStateProvince.Text = " State/Provience"
        '
        'lblEmail
        '
        Me.lblEmail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmail.Location = New System.Drawing.Point(12, 299)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(38, 16)
        Me.lblEmail.TabIndex = 210
        Me.lblEmail.Text = " Email"
        '
        'txtEmail
        '
        Me.txtEmail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.Location = New System.Drawing.Point(150, 297)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.MendatroryField = False
        Me.txtEmail.MyLinkLable1 = Me.lblEmail
        Me.txtEmail.MyLinkLable2 = Nothing
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(272, 18)
        Me.txtEmail.TabIndex = 16
        '
        'txtTelephone
        '
        Me.txtTelephone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTelephone.Location = New System.Drawing.Point(150, 275)
        Me.txtTelephone.MaxLength = 50
        Me.txtTelephone.MendatroryField = False
        Me.txtTelephone.MyLinkLable1 = Me.lblTelephone
        Me.txtTelephone.MyLinkLable2 = Nothing
        Me.txtTelephone.Name = "txtTelephone"
        Me.txtTelephone.Size = New System.Drawing.Size(130, 18)
        Me.txtTelephone.TabIndex = 13
        '
        'lblTelephone
        '
        Me.lblTelephone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTelephone.Location = New System.Drawing.Point(12, 278)
        Me.lblTelephone.Name = "lblTelephone"
        Me.lblTelephone.Size = New System.Drawing.Size(60, 16)
        Me.lblTelephone.TabIndex = 209
        Me.lblTelephone.Text = "Telephone"
        '
        'lblCountry
        '
        Me.lblCountry.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCountry.Location = New System.Drawing.Point(13, 255)
        Me.lblCountry.Name = "lblCountry"
        Me.lblCountry.Size = New System.Drawing.Size(46, 16)
        Me.lblCountry.TabIndex = 208
        Me.lblCountry.Text = "Country"
        '
        'txtCountry
        '
        Me.txtCountry.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCountry.Location = New System.Drawing.Point(150, 253)
        Me.txtCountry.MaxLength = 50
        Me.txtCountry.MendatroryField = False
        Me.txtCountry.MyLinkLable1 = Me.lblCountry
        Me.txtCountry.MyLinkLable2 = Nothing
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.Size = New System.Drawing.Size(193, 18)
        Me.txtCountry.TabIndex = 12
        '
        'txtZipPostalCode
        '
        Me.txtZipPostalCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtZipPostalCode.Location = New System.Drawing.Point(150, 231)
        Me.txtZipPostalCode.MaxLength = 9
        Me.txtZipPostalCode.MendatroryField = False
        Me.txtZipPostalCode.MyLinkLable1 = Me.lblZipPostalCode
        Me.txtZipPostalCode.MyLinkLable2 = Nothing
        Me.txtZipPostalCode.Name = "txtZipPostalCode"
        Me.txtZipPostalCode.Size = New System.Drawing.Size(192, 18)
        Me.txtZipPostalCode.TabIndex = 11
        '
        'lblZipPostalCode
        '
        Me.lblZipPostalCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZipPostalCode.Location = New System.Drawing.Point(13, 233)
        Me.lblZipPostalCode.Name = "lblZipPostalCode"
        Me.lblZipPostalCode.Size = New System.Drawing.Size(87, 16)
        Me.lblZipPostalCode.TabIndex = 203
        Me.lblZipPostalCode.Text = " Zip/PostalCode"
        '
        'txtCity
        '
        Me.txtCity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCity.Location = New System.Drawing.Point(150, 184)
        Me.txtCity.MaxLength = 12
        Me.txtCity.MendatroryField = False
        Me.txtCity.MyLinkLable1 = Me.lblCity
        Me.txtCity.MyLinkLable2 = Nothing
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(193, 18)
        Me.txtCity.TabIndex = 8
        '
        'lblCity
        '
        Me.lblCity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCity.Location = New System.Drawing.Point(13, 189)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(29, 16)
        Me.lblCity.TabIndex = 199
        Me.lblCity.Text = " City"
        '
        'txtAdd4
        '
        Me.txtAdd4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd4.Location = New System.Drawing.Point(150, 162)
        Me.txtAdd4.MaxLength = 50
        Me.txtAdd4.MendatroryField = False
        Me.txtAdd4.MyLinkLable1 = Me.lblAddress
        Me.txtAdd4.MyLinkLable2 = Nothing
        Me.txtAdd4.Name = "txtAdd4"
        Me.txtAdd4.Size = New System.Drawing.Size(553, 18)
        Me.txtAdd4.TabIndex = 7
        '
        'lblAddress
        '
        Me.lblAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress.Location = New System.Drawing.Point(13, 97)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(48, 16)
        Me.lblAddress.TabIndex = 196
        Me.lblAddress.Text = "Address"
        '
        'txtAdd3
        '
        Me.txtAdd3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd3.Location = New System.Drawing.Point(150, 139)
        Me.txtAdd3.MaxLength = 50
        Me.txtAdd3.MendatroryField = False
        Me.txtAdd3.MyLinkLable1 = Me.lblAddress
        Me.txtAdd3.MyLinkLable2 = Nothing
        Me.txtAdd3.Name = "txtAdd3"
        Me.txtAdd3.Size = New System.Drawing.Size(553, 18)
        Me.txtAdd3.TabIndex = 6
        '
        'txtAdd2
        '
        Me.txtAdd2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd2.Location = New System.Drawing.Point(150, 117)
        Me.txtAdd2.MaxLength = 50
        Me.txtAdd2.MendatroryField = False
        Me.txtAdd2.MyLinkLable1 = Me.lblAddress
        Me.txtAdd2.MyLinkLable2 = Nothing
        Me.txtAdd2.Name = "txtAdd2"
        Me.txtAdd2.Size = New System.Drawing.Size(553, 18)
        Me.txtAdd2.TabIndex = 5
        '
        'txtAdd1
        '
        Me.txtAdd1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd1.Location = New System.Drawing.Point(150, 95)
        Me.txtAdd1.MaxLength = 50
        Me.txtAdd1.MendatroryField = False
        Me.txtAdd1.MyLinkLable1 = Me.lblAddress
        Me.txtAdd1.MyLinkLable2 = Nothing
        Me.txtAdd1.Name = "txtAdd1"
        Me.txtAdd1.Size = New System.Drawing.Size(553, 18)
        Me.txtAdd1.TabIndex = 4
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(13, 9)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(84, 9)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(671, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'FrmMilkCollectionCenters
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(749, 485)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmMilkCollectionCenters"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmMilkCollectionCenters"
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCollectionLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcollectionCenterCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtPhone2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPhone1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPhone1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtstateprovince, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStateProvince, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTelephone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTelephone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtZipPostalCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblZipPostalCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblCollectionLevel As common.Controls.MyLabel
    Friend WithEvents lblcollectionCenterCode As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents txtCollectionCenters As common.UserControls.txtNavigator
    Friend WithEvents txtCollectionLevel As common.UserControls.txtFinder
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtPhone2 As common.Controls.MyTextBox
    Friend WithEvents txtPhone1 As common.Controls.MyTextBox
    Friend WithEvents lblPhone1 As common.Controls.MyLabel
    Friend WithEvents txtstateprovince As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents fndstate As common.UserControls.txtFinder
    Friend WithEvents lblStateProvince As common.Controls.MyLabel
    Friend WithEvents lblEmail As common.Controls.MyLabel
    Friend WithEvents txtEmail As common.Controls.MyTextBox
    Friend WithEvents txtTelephone As common.Controls.MyTextBox
    Friend WithEvents lblTelephone As common.Controls.MyLabel
    Friend WithEvents lblCountry As common.Controls.MyLabel
    Friend WithEvents txtCountry As common.Controls.MyTextBox
    Friend WithEvents txtZipPostalCode As common.Controls.MyTextBox
    Friend WithEvents lblZipPostalCode As common.Controls.MyLabel
    Friend WithEvents txtCity As common.Controls.MyTextBox
    Friend WithEvents lblCity As common.Controls.MyLabel
    Friend WithEvents txtAdd4 As common.Controls.MyTextBox
    Friend WithEvents lblAddress As common.Controls.MyLabel
    Friend WithEvents txtAdd3 As common.Controls.MyTextBox
    Friend WithEvents txtAdd2 As common.Controls.MyTextBox
    Friend WithEvents txtAdd1 As common.Controls.MyTextBox
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
End Class

