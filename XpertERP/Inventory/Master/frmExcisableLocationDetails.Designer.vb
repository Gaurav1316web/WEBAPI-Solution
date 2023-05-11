<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmExcisableLocationDetails
    Inherits Telerik.WinControls.UI.RadForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmExcisableLocationDetails))
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.fndLocation = New common.UserControls.txtNavigator
        Me.fndGLAccount = New common.UserControls.txtFinder
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.lblGlAccount = New common.Controls.MyLabel
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.ddlLocationType = New common.Controls.MyComboBox
        Me.lblLocationType = New common.Controls.MyLabel
        Me.chkExcisable = New Telerik.WinControls.UI.RadCheckBox
        Me.lblExcisable = New common.Controls.MyLabel
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.txtDivisionAddress = New common.Controls.MyTextBox
        Me.lblDivisionAddress = New common.Controls.MyLabel
        Me.txtDivisionName = New common.Controls.MyTextBox
        Me.lblDivisionName = New common.Controls.MyLabel
        Me.txtDivisionCode = New common.Controls.MyTextBox
        Me.lblDivisionCode = New common.Controls.MyLabel
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.txtRangeAddress = New common.Controls.MyTextBox
        Me.lblRangeAddress = New common.Controls.MyLabel
        Me.txtRangeName = New common.Controls.MyTextBox
        Me.lblName = New common.Controls.MyLabel
        Me.txtRangeCode = New common.Controls.MyTextBox
        Me.lblRangeCode = New common.Controls.MyLabel
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.fndSaleTaxGrp = New common.UserControls.txtFinder
        Me.fndPurchaseTaxGrp = New common.UserControls.txtFinder
        Me.txtCommissionerate = New common.Controls.MyTextBox
        Me.lblCommissionerate = New common.Controls.MyLabel
        Me.txtRegistration = New common.Controls.MyTextBox
        Me.lblRegistration = New common.Controls.MyLabel
        Me.txtEccNumber = New common.Controls.MyTextBox
        Me.lblEccNumber = New common.Controls.MyLabel
        Me.lblSalesTaxGroup = New common.Controls.MyLabel
        Me.lblPurchaseTaxGroup = New common.Controls.MyLabel
        Me.txtLocationDesc = New common.Controls.MyTextBox
        Me.lblLocation = New common.Controls.MyLabel
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGlAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlLocationType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkExcisable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExcisable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.txtDivisionAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivisionAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDivisionName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivisionName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDivisionCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivisionCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.txtRangeAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRangeAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRangeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRangeCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRangeCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.txtCommissionerate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCommissionerate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRegistration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRegistration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEccNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEccNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesTaxGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPurchaseTaxGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.fndLocation)
        Me.RadGroupBox1.Controls.Add(Me.fndGLAccount)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.lblGlAccount)
        Me.RadGroupBox1.Controls.Add(Me.btnDelete)
        Me.RadGroupBox1.Controls.Add(Me.btnSave)
        Me.RadGroupBox1.Controls.Add(Me.btnClose)
        Me.RadGroupBox1.Controls.Add(Me.ddlLocationType)
        Me.RadGroupBox1.Controls.Add(Me.lblLocationType)
        Me.RadGroupBox1.Controls.Add(Me.chkExcisable)
        Me.RadGroupBox1.Controls.Add(Me.lblExcisable)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.txtLocationDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblLocation)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Excisable Location Profile"
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(677, 483)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "Excisable Location Profile"
        '
        'fndLocation
        '
        Me.fndLocation.Location = New System.Drawing.Point(96, 25)
        Me.fndLocation.MendatroryField = False
        Me.fndLocation.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndLocation.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndLocation.MyLinkLable1 = Nothing
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyMaxLength = 32767
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.Size = New System.Drawing.Size(230, 21)
        Me.fndLocation.TabIndex = 0
        Me.fndLocation.Value = ""
        '
        'fndGLAccount
        '
        Me.fndGLAccount.Location = New System.Drawing.Point(274, 62)
        Me.fndGLAccount.MendatroryField = False
        Me.fndGLAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndGLAccount.MyLinkLable1 = Nothing
        Me.fndGLAccount.MyLinkLable2 = Nothing
        Me.fndGLAccount.MyReadOnly = False
        Me.fndGLAccount.Name = "fndGLAccount"
        Me.fndGLAccount.Size = New System.Drawing.Size(144, 18)
        Me.fndGLAccount.TabIndex = 3
        Me.fndGLAccount.Value = ""
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(332, 25)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(15, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'lblGlAccount
        '
        Me.lblGlAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGlAccount.Location = New System.Drawing.Point(186, 64)
        Me.lblGlAccount.Name = "lblGlAccount"
        Me.lblGlAccount.Size = New System.Drawing.Size(69, 16)
        Me.lblGlAccount.TabIndex = 20
        Me.lblGlAccount.Text = "G/L Account"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(96, 452)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 9
        Me.btnDelete.Text = " Delete"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(13, 452)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 8
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(593, 452)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "Close"
        '
        'ddlLocationType
        '
        Me.ddlLocationType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Selected = True
        RadListDataItem1.Text = "Depot"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Manufacturing Unit"
        RadListDataItem2.TextWrap = True
        Me.ddlLocationType.Items.Add(RadListDataItem1)
        Me.ddlLocationType.Items.Add(RadListDataItem2)
        Me.ddlLocationType.Location = New System.Drawing.Point(555, 64)
        Me.ddlLocationType.MendatroryField = False
        Me.ddlLocationType.MyLinkLable1 = Nothing
        Me.ddlLocationType.MyLinkLable2 = Nothing
        Me.ddlLocationType.Name = "ddlLocationType"
        Me.ddlLocationType.Size = New System.Drawing.Size(106, 18)
        Me.ddlLocationType.TabIndex = 4
        Me.ddlLocationType.Text = "Depot"
        '
        'lblLocationType
        '
        Me.lblLocationType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationType.Location = New System.Drawing.Point(449, 64)
        Me.lblLocationType.Name = "lblLocationType"
        Me.lblLocationType.Size = New System.Drawing.Size(77, 16)
        Me.lblLocationType.TabIndex = 8
        Me.lblLocationType.Text = "Location Type"
        '
        'chkExcisable
        '
        Me.chkExcisable.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExcisable.Location = New System.Drawing.Point(96, 64)
        Me.chkExcisable.Name = "chkExcisable"
        Me.chkExcisable.Size = New System.Drawing.Size(69, 16)
        Me.chkExcisable.TabIndex = 2
        Me.chkExcisable.Text = "Excisable"
        '
        'lblExcisable
        '
        Me.lblExcisable.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExcisable.Location = New System.Drawing.Point(14, 62)
        Me.lblExcisable.Name = "lblExcisable"
        Me.lblExcisable.Size = New System.Drawing.Size(55, 16)
        Me.lblExcisable.TabIndex = 6
        Me.lblExcisable.Text = "Excisable"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.txtDivisionAddress)
        Me.RadGroupBox4.Controls.Add(Me.lblDivisionAddress)
        Me.RadGroupBox4.Controls.Add(Me.txtDivisionName)
        Me.RadGroupBox4.Controls.Add(Me.lblDivisionName)
        Me.RadGroupBox4.Controls.Add(Me.txtDivisionCode)
        Me.RadGroupBox4.Controls.Add(Me.lblDivisionCode)
        Me.RadGroupBox4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox4.HeaderText = " C.E.Division"
        Me.RadGroupBox4.Location = New System.Drawing.Point(14, 330)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(647, 112)
        Me.RadGroupBox4.TabIndex = 7
        Me.RadGroupBox4.Text = " C.E.Division"
        '
        'txtDivisionAddress
        '
        Me.txtDivisionAddress.Location = New System.Drawing.Point(143, 71)
        Me.txtDivisionAddress.MaxLength = 100
        Me.txtDivisionAddress.MendatroryField = False
        Me.txtDivisionAddress.MyLinkLable1 = Nothing
        Me.txtDivisionAddress.MyLinkLable2 = Nothing
        Me.txtDivisionAddress.Name = "txtDivisionAddress"
        Me.txtDivisionAddress.Size = New System.Drawing.Size(494, 20)
        Me.txtDivisionAddress.TabIndex = 2
        Me.txtDivisionAddress.Text = " "
        '
        'lblDivisionAddress
        '
        Me.lblDivisionAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDivisionAddress.Location = New System.Drawing.Point(13, 73)
        Me.lblDivisionAddress.Name = "lblDivisionAddress"
        Me.lblDivisionAddress.Size = New System.Drawing.Size(48, 16)
        Me.lblDivisionAddress.TabIndex = 23
        Me.lblDivisionAddress.Text = "Address"
        '
        'txtDivisionName
        '
        Me.txtDivisionName.Location = New System.Drawing.Point(143, 47)
        Me.txtDivisionName.MaxLength = 100
        Me.txtDivisionName.MendatroryField = False
        Me.txtDivisionName.MyLinkLable1 = Nothing
        Me.txtDivisionName.MyLinkLable2 = Nothing
        Me.txtDivisionName.Name = "txtDivisionName"
        Me.txtDivisionName.Size = New System.Drawing.Size(494, 20)
        Me.txtDivisionName.TabIndex = 1
        Me.txtDivisionName.Text = " "
        '
        'lblDivisionName
        '
        Me.lblDivisionName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDivisionName.Location = New System.Drawing.Point(13, 49)
        Me.lblDivisionName.Name = "lblDivisionName"
        Me.lblDivisionName.Size = New System.Drawing.Size(36, 16)
        Me.lblDivisionName.TabIndex = 21
        Me.lblDivisionName.Text = "Name"
        '
        'txtDivisionCode
        '
        Me.txtDivisionCode.Location = New System.Drawing.Point(143, 23)
        Me.txtDivisionCode.MaxLength = 100
        Me.txtDivisionCode.MendatroryField = False
        Me.txtDivisionCode.MyLinkLable1 = Nothing
        Me.txtDivisionCode.MyLinkLable2 = Nothing
        Me.txtDivisionCode.Name = "txtDivisionCode"
        Me.txtDivisionCode.Size = New System.Drawing.Size(494, 20)
        Me.txtDivisionCode.TabIndex = 0
        Me.txtDivisionCode.Text = " "
        '
        'lblDivisionCode
        '
        Me.lblDivisionCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDivisionCode.Location = New System.Drawing.Point(13, 25)
        Me.lblDivisionCode.Name = "lblDivisionCode"
        Me.lblDivisionCode.Size = New System.Drawing.Size(33, 16)
        Me.lblDivisionCode.TabIndex = 19
        Me.lblDivisionCode.Text = "Code"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.txtRangeAddress)
        Me.RadGroupBox3.Controls.Add(Me.lblRangeAddress)
        Me.RadGroupBox3.Controls.Add(Me.txtRangeName)
        Me.RadGroupBox3.Controls.Add(Me.lblName)
        Me.RadGroupBox3.Controls.Add(Me.txtRangeCode)
        Me.RadGroupBox3.Controls.Add(Me.lblRangeCode)
        Me.RadGroupBox3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox3.HeaderText = " C.E.Range"
        Me.RadGroupBox3.Location = New System.Drawing.Point(14, 228)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(647, 96)
        Me.RadGroupBox3.TabIndex = 6
        Me.RadGroupBox3.Text = " C.E.Range"
        '
        'txtRangeAddress
        '
        Me.txtRangeAddress.Location = New System.Drawing.Point(143, 67)
        Me.txtRangeAddress.MaxLength = 100
        Me.txtRangeAddress.MendatroryField = False
        Me.txtRangeAddress.MyLinkLable1 = Nothing
        Me.txtRangeAddress.MyLinkLable2 = Nothing
        Me.txtRangeAddress.Name = "txtRangeAddress"
        Me.txtRangeAddress.Size = New System.Drawing.Size(494, 20)
        Me.txtRangeAddress.TabIndex = 2
        Me.txtRangeAddress.Text = " "
        '
        'lblRangeAddress
        '
        Me.lblRangeAddress.Location = New System.Drawing.Point(13, 71)
        Me.lblRangeAddress.Name = "lblRangeAddress"
        Me.lblRangeAddress.Size = New System.Drawing.Size(46, 18)
        Me.lblRangeAddress.TabIndex = 17
        Me.lblRangeAddress.Text = "Address"
        '
        'txtRangeName
        '
        Me.txtRangeName.Location = New System.Drawing.Point(143, 43)
        Me.txtRangeName.MaxLength = 100
        Me.txtRangeName.MendatroryField = False
        Me.txtRangeName.MyLinkLable1 = Nothing
        Me.txtRangeName.MyLinkLable2 = Nothing
        Me.txtRangeName.Name = "txtRangeName"
        Me.txtRangeName.Size = New System.Drawing.Size(494, 20)
        Me.txtRangeName.TabIndex = 1
        Me.txtRangeName.Text = " "
        '
        'lblName
        '
        Me.lblName.Location = New System.Drawing.Point(13, 47)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(36, 18)
        Me.lblName.TabIndex = 15
        Me.lblName.Text = "Name"
        '
        'txtRangeCode
        '
        Me.txtRangeCode.Location = New System.Drawing.Point(143, 19)
        Me.txtRangeCode.MaxLength = 100
        Me.txtRangeCode.MendatroryField = False
        Me.txtRangeCode.MyLinkLable1 = Nothing
        Me.txtRangeCode.MyLinkLable2 = Nothing
        Me.txtRangeCode.Name = "txtRangeCode"
        Me.txtRangeCode.Size = New System.Drawing.Size(494, 20)
        Me.txtRangeCode.TabIndex = 0
        Me.txtRangeCode.Text = " "
        '
        'lblRangeCode
        '
        Me.lblRangeCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRangeCode.Location = New System.Drawing.Point(13, 23)
        Me.lblRangeCode.Name = "lblRangeCode"
        Me.lblRangeCode.Size = New System.Drawing.Size(33, 16)
        Me.lblRangeCode.TabIndex = 13
        Me.lblRangeCode.Text = "Code"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.fndSaleTaxGrp)
        Me.RadGroupBox2.Controls.Add(Me.fndPurchaseTaxGrp)
        Me.RadGroupBox2.Controls.Add(Me.txtCommissionerate)
        Me.RadGroupBox2.Controls.Add(Me.lblCommissionerate)
        Me.RadGroupBox2.Controls.Add(Me.txtRegistration)
        Me.RadGroupBox2.Controls.Add(Me.lblRegistration)
        Me.RadGroupBox2.Controls.Add(Me.txtEccNumber)
        Me.RadGroupBox2.Controls.Add(Me.lblEccNumber)
        Me.RadGroupBox2.Controls.Add(Me.lblSalesTaxGroup)
        Me.RadGroupBox2.Controls.Add(Me.lblPurchaseTaxGroup)
        Me.RadGroupBox2.HeaderText = " "
        Me.RadGroupBox2.Location = New System.Drawing.Point(14, 86)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(647, 136)
        Me.RadGroupBox2.TabIndex = 5
        Me.RadGroupBox2.Text = " "
        '
        'fndSaleTaxGrp
        '
        Me.fndSaleTaxGrp.Location = New System.Drawing.Point(454, 23)
        Me.fndSaleTaxGrp.MendatroryField = False
        Me.fndSaleTaxGrp.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSaleTaxGrp.MyLinkLable1 = Nothing
        Me.fndSaleTaxGrp.MyLinkLable2 = Nothing
        Me.fndSaleTaxGrp.MyReadOnly = False
        Me.fndSaleTaxGrp.Name = "fndSaleTaxGrp"
        Me.fndSaleTaxGrp.Size = New System.Drawing.Size(183, 18)
        Me.fndSaleTaxGrp.TabIndex = 1
        Me.fndSaleTaxGrp.Value = ""
        '
        'fndPurchaseTaxGrp
        '
        Me.fndPurchaseTaxGrp.Location = New System.Drawing.Point(143, 23)
        Me.fndPurchaseTaxGrp.MendatroryField = False
        Me.fndPurchaseTaxGrp.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPurchaseTaxGrp.MyLinkLable1 = Nothing
        Me.fndPurchaseTaxGrp.MyLinkLable2 = Nothing
        Me.fndPurchaseTaxGrp.MyReadOnly = False
        Me.fndPurchaseTaxGrp.Name = "fndPurchaseTaxGrp"
        Me.fndPurchaseTaxGrp.Size = New System.Drawing.Size(172, 18)
        Me.fndPurchaseTaxGrp.TabIndex = 0
        Me.fndPurchaseTaxGrp.Value = ""
        '
        'txtCommissionerate
        '
        Me.txtCommissionerate.Location = New System.Drawing.Point(143, 101)
        Me.txtCommissionerate.MaxLength = 100
        Me.txtCommissionerate.MendatroryField = False
        Me.txtCommissionerate.MyLinkLable1 = Nothing
        Me.txtCommissionerate.MyLinkLable2 = Nothing
        Me.txtCommissionerate.Name = "txtCommissionerate"
        Me.txtCommissionerate.Size = New System.Drawing.Size(494, 20)
        Me.txtCommissionerate.TabIndex = 4
        Me.txtCommissionerate.Text = " "
        '
        'lblCommissionerate
        '
        Me.lblCommissionerate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCommissionerate.Location = New System.Drawing.Point(13, 105)
        Me.lblCommissionerate.Name = "lblCommissionerate"
        Me.lblCommissionerate.Size = New System.Drawing.Size(94, 16)
        Me.lblCommissionerate.TabIndex = 15
        Me.lblCommissionerate.Text = "Commissionerate"
        '
        'txtRegistration
        '
        Me.txtRegistration.Location = New System.Drawing.Point(143, 75)
        Me.txtRegistration.MaxLength = 100
        Me.txtRegistration.MendatroryField = False
        Me.txtRegistration.MyLinkLable1 = Nothing
        Me.txtRegistration.MyLinkLable2 = Nothing
        Me.txtRegistration.Name = "txtRegistration"
        Me.txtRegistration.Size = New System.Drawing.Size(494, 20)
        Me.txtRegistration.TabIndex = 3
        Me.txtRegistration.Text = " "
        '
        'lblRegistration
        '
        Me.lblRegistration.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegistration.Location = New System.Drawing.Point(13, 79)
        Me.lblRegistration.Name = "lblRegistration"
        Me.lblRegistration.Size = New System.Drawing.Size(110, 16)
        Me.lblRegistration.TabIndex = 13
        Me.lblRegistration.Text = "Registration Number"
        '
        'txtEccNumber
        '
        Me.txtEccNumber.Location = New System.Drawing.Point(143, 51)
        Me.txtEccNumber.MaxLength = 15
        Me.txtEccNumber.MendatroryField = False
        Me.txtEccNumber.MyLinkLable1 = Nothing
        Me.txtEccNumber.MyLinkLable2 = Nothing
        Me.txtEccNumber.Name = "txtEccNumber"
        Me.txtEccNumber.Size = New System.Drawing.Size(494, 20)
        Me.txtEccNumber.TabIndex = 2
        Me.txtEccNumber.Text = " "
        '
        'lblEccNumber
        '
        Me.lblEccNumber.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEccNumber.Location = New System.Drawing.Point(13, 53)
        Me.lblEccNumber.Name = "lblEccNumber"
        Me.lblEccNumber.Size = New System.Drawing.Size(77, 16)
        Me.lblEccNumber.TabIndex = 11
        Me.lblEccNumber.Text = "ECC. Number"
        '
        'lblSalesTaxGroup
        '
        Me.lblSalesTaxGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesTaxGroup.Location = New System.Drawing.Point(341, 25)
        Me.lblSalesTaxGroup.Name = "lblSalesTaxGroup"
        Me.lblSalesTaxGroup.Size = New System.Drawing.Size(91, 16)
        Me.lblSalesTaxGroup.TabIndex = 8
        Me.lblSalesTaxGroup.Text = "Sales Tax Group"
        '
        'lblPurchaseTaxGroup
        '
        Me.lblPurchaseTaxGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPurchaseTaxGroup.Location = New System.Drawing.Point(13, 23)
        Me.lblPurchaseTaxGroup.Name = "lblPurchaseTaxGroup"
        Me.lblPurchaseTaxGroup.Size = New System.Drawing.Size(111, 16)
        Me.lblPurchaseTaxGroup.TabIndex = 7
        Me.lblPurchaseTaxGroup.Text = "Purchase Tax Group"
        '
        'txtLocationDesc
        '
        Me.txtLocationDesc.Location = New System.Drawing.Point(355, 26)
        Me.txtLocationDesc.MaxLength = 100
        Me.txtLocationDesc.MendatroryField = False
        Me.txtLocationDesc.MyLinkLable1 = Nothing
        Me.txtLocationDesc.MyLinkLable2 = Nothing
        Me.txtLocationDesc.Name = "txtLocationDesc"
        Me.txtLocationDesc.Size = New System.Drawing.Size(306, 20)
        Me.txtLocationDesc.TabIndex = 1
        Me.txtLocationDesc.Text = " "
        '
        'lblLocation
        '
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(14, 29)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 0
        Me.lblLocation.Text = "Location"
        '
        'FrmExcisableLocationDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(701, 507)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "FrmExcisableLocationDetails"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = " Excisable Location Profile"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGlAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlLocationType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkExcisable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExcisable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.txtDivisionAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivisionAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDivisionName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivisionName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDivisionCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivisionCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.txtRangeAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRangeAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRangeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRangeCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRangeCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.txtCommissionerate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCommissionerate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRegistration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRegistration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEccNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEccNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesTaxGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPurchaseTaxGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtLocationDesc As common.Controls.MyTextBox
    Friend WithEvents chkExcisable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents ddlLocationType As common.Controls.MyComboBox
    Friend WithEvents txtRangeCode As common.Controls.MyTextBox
    Friend WithEvents txtCommissionerate As common.Controls.MyTextBox
    Friend WithEvents txtRegistration As common.Controls.MyTextBox
    Friend WithEvents txtEccNumber As common.Controls.MyTextBox
    Friend WithEvents txtDivisionAddress As common.Controls.MyTextBox
    Friend WithEvents txtDivisionName As common.Controls.MyTextBox
    Friend WithEvents txtDivisionCode As common.Controls.MyTextBox
    Friend WithEvents txtRangeAddress As common.Controls.MyTextBox
    Friend WithEvents txtRangeName As common.Controls.MyTextBox
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblExcisable As common.Controls.MyLabel
    Friend WithEvents lblLocationType As common.Controls.MyLabel
    Friend WithEvents lblRangeCode As common.Controls.MyLabel
    Friend WithEvents lblCommissionerate As common.Controls.MyLabel
    Friend WithEvents lblRegistration As common.Controls.MyLabel
    Friend WithEvents lblEccNumber As common.Controls.MyLabel
    Friend WithEvents lblSalesTaxGroup As common.Controls.MyLabel
    Friend WithEvents lblPurchaseTaxGroup As common.Controls.MyLabel
    Friend WithEvents lblDivisionAddress As common.Controls.MyLabel
    Friend WithEvents lblDivisionName As common.Controls.MyLabel
    Friend WithEvents lblDivisionCode As common.Controls.MyLabel
    Friend WithEvents lblRangeAddress As common.Controls.MyLabel
    Friend WithEvents lblName As common.Controls.MyLabel
    Friend WithEvents lblGlAccount As common.Controls.MyLabel
    Friend WithEvents fndSaleTaxGrp As common.UserControls.txtFinder
    Friend WithEvents fndPurchaseTaxGrp As common.UserControls.txtFinder
    Friend WithEvents fndGLAccount As common.UserControls.txtFinder
    Friend WithEvents fndLocation As common.UserControls.txtNavigator
End Class

