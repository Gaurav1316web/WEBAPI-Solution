Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResponsiblePerson
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.fndStateCodeNew = New common.UserControls.txtFinder()
        Me.lblStateCode = New common.Controls.MyLabel()
        Me.fndbranchCode = New common.UserControls.txtFinder()
        Me.lblBranch = New common.Controls.MyLabel()
        Me.fndCodeNew = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.chkActive = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblActive = New common.Controls.MyLabel()
        Me.txtSignaturePath = New common.Controls.MyTextBox()
        Me.lblSignaturePath = New common.Controls.MyLabel()
        Me.txtEMail = New common.Controls.MyTextBox()
        Me.lblEMail = New common.Controls.MyLabel()
        Me.txtFaxNo = New common.Controls.MyTextBox()
        Me.lblFaxNo = New common.Controls.MyLabel()
        Me.txtPinCode = New common.Controls.MyTextBox()
        Me.lblPinCode = New common.Controls.MyLabel()
        Me.txtTelephoneNo = New common.Controls.MyTextBox()
        Me.lblTelephoneNo = New common.Controls.MyLabel()
        Me.txtCountry = New common.Controls.MyTextBox()
        Me.lblCountry = New common.Controls.MyLabel()
        Me.txtStateCode = New common.Controls.MyTextBox()
        Me.txtBranch = New common.Controls.MyTextBox()
        Me.txtCity = New common.Controls.MyTextBox()
        Me.lblCity = New common.Controls.MyLabel()
        Me.txtAddress2 = New common.Controls.MyTextBox()
        Me.lblAddress = New common.Controls.MyLabel()
        Me.txtAddress1 = New common.Controls.MyTextBox()
        Me.txtFathersName = New common.Controls.MyTextBox()
        Me.lblFathersName = New common.Controls.MyLabel()
        Me.txtDesignation = New common.Controls.MyTextBox()
        Me.lblDesignation = New common.Controls.MyLabel()
        Me.txtName = New common.Controls.MyTextBox()
        Me.lblName = New common.Controls.MyLabel()
        Me.rdbtnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.rbtnClose = New Telerik.WinControls.UI.RadButton()
        Me.rbtnDelete = New Telerik.WinControls.UI.RadButton()
        Me.rbtnSave = New Telerik.WinControls.UI.RadButton()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.radmenu = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItemImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItemClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblStateCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSignaturePath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSignaturePath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEMail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEMail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFaxNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFaxNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPinCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPinCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTelephoneNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTelephoneNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStateCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAddress2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAddress1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFathersName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFathersName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesignation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDesignation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.fndStateCodeNew)
        Me.RadGroupBox1.Controls.Add(Me.fndbranchCode)
        Me.RadGroupBox1.Controls.Add(Me.fndCodeNew)
        Me.RadGroupBox1.Controls.Add(Me.chkActive)
        Me.RadGroupBox1.Controls.Add(Me.lblActive)
        Me.RadGroupBox1.Controls.Add(Me.txtSignaturePath)
        Me.RadGroupBox1.Controls.Add(Me.lblSignaturePath)
        Me.RadGroupBox1.Controls.Add(Me.txtEMail)
        Me.RadGroupBox1.Controls.Add(Me.lblEMail)
        Me.RadGroupBox1.Controls.Add(Me.txtFaxNo)
        Me.RadGroupBox1.Controls.Add(Me.lblFaxNo)
        Me.RadGroupBox1.Controls.Add(Me.txtPinCode)
        Me.RadGroupBox1.Controls.Add(Me.lblPinCode)
        Me.RadGroupBox1.Controls.Add(Me.txtTelephoneNo)
        Me.RadGroupBox1.Controls.Add(Me.lblTelephoneNo)
        Me.RadGroupBox1.Controls.Add(Me.txtCountry)
        Me.RadGroupBox1.Controls.Add(Me.lblCountry)
        Me.RadGroupBox1.Controls.Add(Me.txtStateCode)
        Me.RadGroupBox1.Controls.Add(Me.txtBranch)
        Me.RadGroupBox1.Controls.Add(Me.lblStateCode)
        Me.RadGroupBox1.Controls.Add(Me.lblBranch)
        Me.RadGroupBox1.Controls.Add(Me.txtCity)
        Me.RadGroupBox1.Controls.Add(Me.lblCity)
        Me.RadGroupBox1.Controls.Add(Me.txtAddress2)
        Me.RadGroupBox1.Controls.Add(Me.txtAddress1)
        Me.RadGroupBox1.Controls.Add(Me.lblAddress)
        Me.RadGroupBox1.Controls.Add(Me.txtFathersName)
        Me.RadGroupBox1.Controls.Add(Me.lblFathersName)
        Me.RadGroupBox1.Controls.Add(Me.txtDesignation)
        Me.RadGroupBox1.Controls.Add(Me.lblDesignation)
        Me.RadGroupBox1.Controls.Add(Me.txtName)
        Me.RadGroupBox1.Controls.Add(Me.lblName)
        Me.RadGroupBox1.Controls.Add(Me.rdbtnRefresh)
        Me.RadGroupBox1.Controls.Add(Me.lblCode)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(14, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(593, 433)
        Me.RadGroupBox1.TabIndex = 0
        '
        'fndStateCodeNew
        '
        Me.fndStateCodeNew.AccessibleName = "fndStateCodeNew"
        Me.fndStateCodeNew.CalculationExpression = Nothing
        Me.fndStateCodeNew.FieldCode = Nothing
        Me.fndStateCodeNew.FieldDesc = Nothing
        Me.fndStateCodeNew.FieldMaxLength = 0
        Me.fndStateCodeNew.FieldName = Nothing
        Me.fndStateCodeNew.isCalculatedField = False
        Me.fndStateCodeNew.IsSourceFromTable = False
        Me.fndStateCodeNew.IsSourceFromValueList = False
        Me.fndStateCodeNew.IsUnique = False
        Me.fndStateCodeNew.Location = New System.Drawing.Point(139, 206)
        Me.fndStateCodeNew.MendatroryField = False
        Me.fndStateCodeNew.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndStateCodeNew.MyLinkLable1 = Me.lblStateCode
        Me.fndStateCodeNew.MyLinkLable2 = Nothing
        Me.fndStateCodeNew.MyReadOnly = False
        Me.fndStateCodeNew.MyShowMasterFormButton = False
        Me.fndStateCodeNew.Name = "fndStateCodeNew"
        Me.fndStateCodeNew.ReferenceFieldDesc = Nothing
        Me.fndStateCodeNew.ReferenceFieldName = Nothing
        Me.fndStateCodeNew.ReferenceTableName = Nothing
        Me.fndStateCodeNew.Size = New System.Drawing.Size(213, 19)
        Me.fndStateCodeNew.TabIndex = 10
        Me.fndStateCodeNew.Value = ""
        '
        'lblStateCode
        '
        Me.lblStateCode.FieldName = Nothing
        Me.lblStateCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStateCode.Location = New System.Drawing.Point(13, 206)
        Me.lblStateCode.Name = "lblStateCode"
        Me.lblStateCode.Size = New System.Drawing.Size(63, 16)
        Me.lblStateCode.TabIndex = 17
        Me.lblStateCode.Text = "State Code"
        '
        'fndbranchCode
        '
        Me.fndbranchCode.AccessibleName = "fndbranchCode"
        Me.fndbranchCode.CalculationExpression = Nothing
        Me.fndbranchCode.FieldCode = Nothing
        Me.fndbranchCode.FieldDesc = Nothing
        Me.fndbranchCode.FieldMaxLength = 0
        Me.fndbranchCode.FieldName = Nothing
        Me.fndbranchCode.isCalculatedField = False
        Me.fndbranchCode.IsSourceFromTable = False
        Me.fndbranchCode.IsSourceFromValueList = False
        Me.fndbranchCode.IsUnique = False
        Me.fndbranchCode.Location = New System.Drawing.Point(138, 179)
        Me.fndbranchCode.MendatroryField = False
        Me.fndbranchCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndbranchCode.MyLinkLable1 = Me.lblBranch
        Me.fndbranchCode.MyLinkLable2 = Nothing
        Me.fndbranchCode.MyReadOnly = False
        Me.fndbranchCode.MyShowMasterFormButton = False
        Me.fndbranchCode.Name = "fndbranchCode"
        Me.fndbranchCode.ReferenceFieldDesc = Nothing
        Me.fndbranchCode.ReferenceFieldName = Nothing
        Me.fndbranchCode.ReferenceTableName = Nothing
        Me.fndbranchCode.Size = New System.Drawing.Size(214, 19)
        Me.fndbranchCode.TabIndex = 8
        Me.fndbranchCode.Value = ""
        '
        'lblBranch
        '
        Me.lblBranch.FieldName = Nothing
        Me.lblBranch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBranch.Location = New System.Drawing.Point(13, 182)
        Me.lblBranch.Name = "lblBranch"
        Me.lblBranch.Size = New System.Drawing.Size(42, 16)
        Me.lblBranch.TabIndex = 14
        Me.lblBranch.Text = "Branch"
        '
        'fndCodeNew
        '
        Me.fndCodeNew.AccessibleName = "fndCodeNew"
        Me.fndCodeNew.FieldName = Nothing
        Me.fndCodeNew.Location = New System.Drawing.Point(139, 18)
        Me.fndCodeNew.MendatroryField = False
        Me.fndCodeNew.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndCodeNew.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCodeNew.MyLinkLable1 = Me.lblCode
        Me.fndCodeNew.MyLinkLable2 = Nothing
        Me.fndCodeNew.MyMaxLength = 32767
        Me.fndCodeNew.MyReadOnly = False
        Me.fndCodeNew.Name = "fndCodeNew"
        Me.fndCodeNew.Size = New System.Drawing.Size(225, 21)
        Me.fndCodeNew.TabIndex = 0
        Me.fndCodeNew.TabStop = False
        Me.fndCodeNew.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(13, 23)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(33, 16)
        Me.lblCode.TabIndex = 0
        Me.lblCode.Text = "Code"
        '
        'chkActive
        '
        Me.chkActive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkActive.Location = New System.Drawing.Point(139, 365)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(15, 15)
        Me.chkActive.TabIndex = 18
        '
        'lblActive
        '
        Me.lblActive.FieldName = Nothing
        Me.lblActive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActive.Location = New System.Drawing.Point(13, 363)
        Me.lblActive.Name = "lblActive"
        Me.lblActive.Size = New System.Drawing.Size(37, 16)
        Me.lblActive.TabIndex = 32
        Me.lblActive.Text = "Active"
        '
        'txtSignaturePath
        '
        Me.txtSignaturePath.CalculationExpression = Nothing
        Me.txtSignaturePath.FieldCode = Nothing
        Me.txtSignaturePath.FieldDesc = Nothing
        Me.txtSignaturePath.FieldMaxLength = 0
        Me.txtSignaturePath.FieldName = Nothing
        Me.txtSignaturePath.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSignaturePath.isCalculatedField = False
        Me.txtSignaturePath.IsSourceFromTable = False
        Me.txtSignaturePath.IsSourceFromValueList = False
        Me.txtSignaturePath.IsUnique = False
        Me.txtSignaturePath.Location = New System.Drawing.Point(139, 340)
        Me.txtSignaturePath.MaxLength = 50
        Me.txtSignaturePath.MendatroryField = False
        Me.txtSignaturePath.MyLinkLable1 = Me.lblSignaturePath
        Me.txtSignaturePath.MyLinkLable2 = Nothing
        Me.txtSignaturePath.Name = "txtSignaturePath"
        Me.txtSignaturePath.ReferenceFieldDesc = Nothing
        Me.txtSignaturePath.ReferenceFieldName = Nothing
        Me.txtSignaturePath.ReferenceTableName = Nothing
        Me.txtSignaturePath.Size = New System.Drawing.Size(428, 18)
        Me.txtSignaturePath.TabIndex = 17
        '
        'lblSignaturePath
        '
        Me.lblSignaturePath.FieldName = Nothing
        Me.lblSignaturePath.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSignaturePath.Location = New System.Drawing.Point(13, 338)
        Me.lblSignaturePath.Name = "lblSignaturePath"
        Me.lblSignaturePath.Size = New System.Drawing.Size(81, 16)
        Me.lblSignaturePath.TabIndex = 30
        Me.lblSignaturePath.Text = "Signature Path"
        '
        'txtEMail
        '
        Me.txtEMail.CalculationExpression = Nothing
        Me.txtEMail.FieldCode = Nothing
        Me.txtEMail.FieldDesc = Nothing
        Me.txtEMail.FieldMaxLength = 0
        Me.txtEMail.FieldName = Nothing
        Me.txtEMail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEMail.isCalculatedField = False
        Me.txtEMail.IsSourceFromTable = False
        Me.txtEMail.IsSourceFromValueList = False
        Me.txtEMail.IsUnique = False
        Me.txtEMail.Location = New System.Drawing.Point(139, 318)
        Me.txtEMail.MaxLength = 50
        Me.txtEMail.MendatroryField = False
        Me.txtEMail.MyLinkLable1 = Me.lblEMail
        Me.txtEMail.MyLinkLable2 = Nothing
        Me.txtEMail.Name = "txtEMail"
        Me.txtEMail.ReferenceFieldDesc = Nothing
        Me.txtEMail.ReferenceFieldName = Nothing
        Me.txtEMail.ReferenceTableName = Nothing
        Me.txtEMail.Size = New System.Drawing.Size(428, 18)
        Me.txtEMail.TabIndex = 16
        '
        'lblEMail
        '
        Me.lblEMail.FieldName = Nothing
        Me.lblEMail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEMail.Location = New System.Drawing.Point(13, 316)
        Me.lblEMail.Name = "lblEMail"
        Me.lblEMail.Size = New System.Drawing.Size(38, 16)
        Me.lblEMail.TabIndex = 28
        Me.lblEMail.Text = "E-Mail"
        '
        'txtFaxNo
        '
        Me.txtFaxNo.CalculationExpression = Nothing
        Me.txtFaxNo.FieldCode = Nothing
        Me.txtFaxNo.FieldDesc = Nothing
        Me.txtFaxNo.FieldMaxLength = 0
        Me.txtFaxNo.FieldName = Nothing
        Me.txtFaxNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFaxNo.isCalculatedField = False
        Me.txtFaxNo.IsSourceFromTable = False
        Me.txtFaxNo.IsSourceFromValueList = False
        Me.txtFaxNo.IsUnique = False
        Me.txtFaxNo.Location = New System.Drawing.Point(139, 296)
        Me.txtFaxNo.MaxLength = 50
        Me.txtFaxNo.MendatroryField = False
        Me.txtFaxNo.MyLinkLable1 = Me.lblFaxNo
        Me.txtFaxNo.MyLinkLable2 = Nothing
        Me.txtFaxNo.Name = "txtFaxNo"
        Me.txtFaxNo.ReferenceFieldDesc = Nothing
        Me.txtFaxNo.ReferenceFieldName = Nothing
        Me.txtFaxNo.ReferenceTableName = Nothing
        Me.txtFaxNo.Size = New System.Drawing.Size(428, 18)
        Me.txtFaxNo.TabIndex = 15
        '
        'lblFaxNo
        '
        Me.lblFaxNo.FieldName = Nothing
        Me.lblFaxNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFaxNo.Location = New System.Drawing.Point(13, 294)
        Me.lblFaxNo.Name = "lblFaxNo"
        Me.lblFaxNo.Size = New System.Drawing.Size(43, 16)
        Me.lblFaxNo.TabIndex = 26
        Me.lblFaxNo.Text = "Fax No"
        '
        'txtPinCode
        '
        Me.txtPinCode.CalculationExpression = Nothing
        Me.txtPinCode.FieldCode = Nothing
        Me.txtPinCode.FieldDesc = Nothing
        Me.txtPinCode.FieldMaxLength = 0
        Me.txtPinCode.FieldName = Nothing
        Me.txtPinCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPinCode.isCalculatedField = False
        Me.txtPinCode.IsSourceFromTable = False
        Me.txtPinCode.IsSourceFromValueList = False
        Me.txtPinCode.IsUnique = False
        Me.txtPinCode.Location = New System.Drawing.Point(139, 252)
        Me.txtPinCode.MaxLength = 50
        Me.txtPinCode.MendatroryField = False
        Me.txtPinCode.MyLinkLable1 = Me.lblPinCode
        Me.txtPinCode.MyLinkLable2 = Nothing
        Me.txtPinCode.Name = "txtPinCode"
        Me.txtPinCode.ReferenceFieldDesc = Nothing
        Me.txtPinCode.ReferenceFieldName = Nothing
        Me.txtPinCode.ReferenceTableName = Nothing
        Me.txtPinCode.Size = New System.Drawing.Size(428, 18)
        Me.txtPinCode.TabIndex = 13
        '
        'lblPinCode
        '
        Me.lblPinCode.FieldName = Nothing
        Me.lblPinCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPinCode.Location = New System.Drawing.Point(13, 250)
        Me.lblPinCode.Name = "lblPinCode"
        Me.lblPinCode.Size = New System.Drawing.Size(53, 16)
        Me.lblPinCode.TabIndex = 22
        Me.lblPinCode.Text = "Pin Code"
        '
        'txtTelephoneNo
        '
        Me.txtTelephoneNo.CalculationExpression = Nothing
        Me.txtTelephoneNo.FieldCode = Nothing
        Me.txtTelephoneNo.FieldDesc = Nothing
        Me.txtTelephoneNo.FieldMaxLength = 0
        Me.txtTelephoneNo.FieldName = Nothing
        Me.txtTelephoneNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTelephoneNo.isCalculatedField = False
        Me.txtTelephoneNo.IsSourceFromTable = False
        Me.txtTelephoneNo.IsSourceFromValueList = False
        Me.txtTelephoneNo.IsUnique = False
        Me.txtTelephoneNo.Location = New System.Drawing.Point(139, 274)
        Me.txtTelephoneNo.MaxLength = 50
        Me.txtTelephoneNo.MendatroryField = False
        Me.txtTelephoneNo.MyLinkLable1 = Me.lblTelephoneNo
        Me.txtTelephoneNo.MyLinkLable2 = Nothing
        Me.txtTelephoneNo.Name = "txtTelephoneNo"
        Me.txtTelephoneNo.ReferenceFieldDesc = Nothing
        Me.txtTelephoneNo.ReferenceFieldName = Nothing
        Me.txtTelephoneNo.ReferenceTableName = Nothing
        Me.txtTelephoneNo.Size = New System.Drawing.Size(428, 18)
        Me.txtTelephoneNo.TabIndex = 14
        '
        'lblTelephoneNo
        '
        Me.lblTelephoneNo.FieldName = Nothing
        Me.lblTelephoneNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTelephoneNo.Location = New System.Drawing.Point(13, 272)
        Me.lblTelephoneNo.Name = "lblTelephoneNo"
        Me.lblTelephoneNo.Size = New System.Drawing.Size(77, 16)
        Me.lblTelephoneNo.TabIndex = 24
        Me.lblTelephoneNo.Text = "Telephone No"
        '
        'txtCountry
        '
        Me.txtCountry.CalculationExpression = Nothing
        Me.txtCountry.FieldCode = Nothing
        Me.txtCountry.FieldDesc = Nothing
        Me.txtCountry.FieldMaxLength = 0
        Me.txtCountry.FieldName = Nothing
        Me.txtCountry.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCountry.isCalculatedField = False
        Me.txtCountry.IsSourceFromTable = False
        Me.txtCountry.IsSourceFromValueList = False
        Me.txtCountry.IsUnique = False
        Me.txtCountry.Location = New System.Drawing.Point(139, 230)
        Me.txtCountry.MaxLength = 50
        Me.txtCountry.MendatroryField = False
        Me.txtCountry.MyLinkLable1 = Me.lblCountry
        Me.txtCountry.MyLinkLable2 = Nothing
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.ReferenceFieldDesc = Nothing
        Me.txtCountry.ReferenceFieldName = Nothing
        Me.txtCountry.ReferenceTableName = Nothing
        Me.txtCountry.Size = New System.Drawing.Size(428, 18)
        Me.txtCountry.TabIndex = 12
        '
        'lblCountry
        '
        Me.lblCountry.FieldName = Nothing
        Me.lblCountry.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCountry.Location = New System.Drawing.Point(13, 228)
        Me.lblCountry.Name = "lblCountry"
        Me.lblCountry.Size = New System.Drawing.Size(46, 16)
        Me.lblCountry.TabIndex = 20
        Me.lblCountry.Text = "Country"
        '
        'txtStateCode
        '
        Me.txtStateCode.CalculationExpression = Nothing
        Me.txtStateCode.FieldCode = Nothing
        Me.txtStateCode.FieldDesc = Nothing
        Me.txtStateCode.FieldMaxLength = 0
        Me.txtStateCode.FieldName = Nothing
        Me.txtStateCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStateCode.isCalculatedField = False
        Me.txtStateCode.IsSourceFromTable = False
        Me.txtStateCode.IsSourceFromValueList = False
        Me.txtStateCode.IsUnique = False
        Me.txtStateCode.Location = New System.Drawing.Point(358, 207)
        Me.txtStateCode.MendatroryField = False
        Me.txtStateCode.MyLinkLable1 = Nothing
        Me.txtStateCode.MyLinkLable2 = Nothing
        Me.txtStateCode.Name = "txtStateCode"
        Me.txtStateCode.ReadOnly = True
        Me.txtStateCode.ReferenceFieldDesc = Nothing
        Me.txtStateCode.ReferenceFieldName = Nothing
        Me.txtStateCode.ReferenceTableName = Nothing
        Me.txtStateCode.Size = New System.Drawing.Size(209, 18)
        Me.txtStateCode.TabIndex = 11
        Me.txtStateCode.TabStop = False
        '
        'txtBranch
        '
        Me.txtBranch.CalculationExpression = Nothing
        Me.txtBranch.FieldCode = Nothing
        Me.txtBranch.FieldDesc = Nothing
        Me.txtBranch.FieldMaxLength = 0
        Me.txtBranch.FieldName = Nothing
        Me.txtBranch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBranch.isCalculatedField = False
        Me.txtBranch.IsSourceFromTable = False
        Me.txtBranch.IsSourceFromValueList = False
        Me.txtBranch.IsUnique = False
        Me.txtBranch.Location = New System.Drawing.Point(359, 180)
        Me.txtBranch.MendatroryField = False
        Me.txtBranch.MyLinkLable1 = Nothing
        Me.txtBranch.MyLinkLable2 = Nothing
        Me.txtBranch.Name = "txtBranch"
        Me.txtBranch.ReadOnly = True
        Me.txtBranch.ReferenceFieldDesc = Nothing
        Me.txtBranch.ReferenceFieldName = Nothing
        Me.txtBranch.ReferenceTableName = Nothing
        Me.txtBranch.Size = New System.Drawing.Size(208, 18)
        Me.txtBranch.TabIndex = 9
        Me.txtBranch.TabStop = False
        '
        'txtCity
        '
        Me.txtCity.CalculationExpression = Nothing
        Me.txtCity.FieldCode = Nothing
        Me.txtCity.FieldDesc = Nothing
        Me.txtCity.FieldMaxLength = 0
        Me.txtCity.FieldName = Nothing
        Me.txtCity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCity.isCalculatedField = False
        Me.txtCity.IsSourceFromTable = False
        Me.txtCity.IsSourceFromValueList = False
        Me.txtCity.IsUnique = False
        Me.txtCity.Location = New System.Drawing.Point(139, 157)
        Me.txtCity.MaxLength = 50
        Me.txtCity.MendatroryField = False
        Me.txtCity.MyLinkLable1 = Me.lblCity
        Me.txtCity.MyLinkLable2 = Nothing
        Me.txtCity.Name = "txtCity"
        Me.txtCity.ReferenceFieldDesc = Nothing
        Me.txtCity.ReferenceFieldName = Nothing
        Me.txtCity.ReferenceTableName = Nothing
        Me.txtCity.Size = New System.Drawing.Size(428, 18)
        Me.txtCity.TabIndex = 7
        '
        'lblCity
        '
        Me.lblCity.FieldName = Nothing
        Me.lblCity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCity.Location = New System.Drawing.Point(13, 155)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(26, 16)
        Me.lblCity.TabIndex = 12
        Me.lblCity.Text = "City"
        '
        'txtAddress2
        '
        Me.txtAddress2.CalculationExpression = Nothing
        Me.txtAddress2.FieldCode = Nothing
        Me.txtAddress2.FieldDesc = Nothing
        Me.txtAddress2.FieldMaxLength = 0
        Me.txtAddress2.FieldName = Nothing
        Me.txtAddress2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress2.isCalculatedField = False
        Me.txtAddress2.IsSourceFromTable = False
        Me.txtAddress2.IsSourceFromValueList = False
        Me.txtAddress2.IsUnique = False
        Me.txtAddress2.Location = New System.Drawing.Point(139, 135)
        Me.txtAddress2.MaxLength = 50
        Me.txtAddress2.MendatroryField = False
        Me.txtAddress2.MyLinkLable1 = Me.lblAddress
        Me.txtAddress2.MyLinkLable2 = Nothing
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.ReferenceFieldDesc = Nothing
        Me.txtAddress2.ReferenceFieldName = Nothing
        Me.txtAddress2.ReferenceTableName = Nothing
        Me.txtAddress2.Size = New System.Drawing.Size(428, 18)
        Me.txtAddress2.TabIndex = 6
        '
        'lblAddress
        '
        Me.lblAddress.FieldName = Nothing
        Me.lblAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress.Location = New System.Drawing.Point(13, 111)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(48, 16)
        Me.lblAddress.TabIndex = 9
        Me.lblAddress.Text = "Address"
        '
        'txtAddress1
        '
        Me.txtAddress1.CalculationExpression = Nothing
        Me.txtAddress1.FieldCode = Nothing
        Me.txtAddress1.FieldDesc = Nothing
        Me.txtAddress1.FieldMaxLength = 0
        Me.txtAddress1.FieldName = Nothing
        Me.txtAddress1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress1.isCalculatedField = False
        Me.txtAddress1.IsSourceFromTable = False
        Me.txtAddress1.IsSourceFromValueList = False
        Me.txtAddress1.IsUnique = False
        Me.txtAddress1.Location = New System.Drawing.Point(139, 113)
        Me.txtAddress1.MaxLength = 50
        Me.txtAddress1.MendatroryField = False
        Me.txtAddress1.MyLinkLable1 = Me.lblAddress
        Me.txtAddress1.MyLinkLable2 = Nothing
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.ReferenceFieldDesc = Nothing
        Me.txtAddress1.ReferenceFieldName = Nothing
        Me.txtAddress1.ReferenceTableName = Nothing
        Me.txtAddress1.Size = New System.Drawing.Size(428, 18)
        Me.txtAddress1.TabIndex = 5
        '
        'txtFathersName
        '
        Me.txtFathersName.CalculationExpression = Nothing
        Me.txtFathersName.FieldCode = Nothing
        Me.txtFathersName.FieldDesc = Nothing
        Me.txtFathersName.FieldMaxLength = 0
        Me.txtFathersName.FieldName = Nothing
        Me.txtFathersName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFathersName.isCalculatedField = False
        Me.txtFathersName.IsSourceFromTable = False
        Me.txtFathersName.IsSourceFromValueList = False
        Me.txtFathersName.IsUnique = False
        Me.txtFathersName.Location = New System.Drawing.Point(139, 69)
        Me.txtFathersName.MaxLength = 50
        Me.txtFathersName.MendatroryField = False
        Me.txtFathersName.MyLinkLable1 = Me.lblFathersName
        Me.txtFathersName.MyLinkLable2 = Nothing
        Me.txtFathersName.Name = "txtFathersName"
        Me.txtFathersName.ReferenceFieldDesc = Nothing
        Me.txtFathersName.ReferenceFieldName = Nothing
        Me.txtFathersName.ReferenceTableName = Nothing
        Me.txtFathersName.Size = New System.Drawing.Size(428, 18)
        Me.txtFathersName.TabIndex = 3
        '
        'lblFathersName
        '
        Me.lblFathersName.FieldName = Nothing
        Me.lblFathersName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFathersName.Location = New System.Drawing.Point(13, 67)
        Me.lblFathersName.Name = "lblFathersName"
        Me.lblFathersName.Size = New System.Drawing.Size(80, 16)
        Me.lblFathersName.TabIndex = 5
        Me.lblFathersName.Text = "Father's Name"
        '
        'txtDesignation
        '
        Me.txtDesignation.CalculationExpression = Nothing
        Me.txtDesignation.FieldCode = Nothing
        Me.txtDesignation.FieldDesc = Nothing
        Me.txtDesignation.FieldMaxLength = 0
        Me.txtDesignation.FieldName = Nothing
        Me.txtDesignation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesignation.isCalculatedField = False
        Me.txtDesignation.IsSourceFromTable = False
        Me.txtDesignation.IsSourceFromValueList = False
        Me.txtDesignation.IsUnique = False
        Me.txtDesignation.Location = New System.Drawing.Point(139, 91)
        Me.txtDesignation.MaxLength = 50
        Me.txtDesignation.MendatroryField = False
        Me.txtDesignation.MyLinkLable1 = Me.lblDesignation
        Me.txtDesignation.MyLinkLable2 = Nothing
        Me.txtDesignation.Name = "txtDesignation"
        Me.txtDesignation.ReferenceFieldDesc = Nothing
        Me.txtDesignation.ReferenceFieldName = Nothing
        Me.txtDesignation.ReferenceTableName = Nothing
        Me.txtDesignation.Size = New System.Drawing.Size(428, 18)
        Me.txtDesignation.TabIndex = 4
        '
        'lblDesignation
        '
        Me.lblDesignation.FieldName = Nothing
        Me.lblDesignation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesignation.Location = New System.Drawing.Point(13, 89)
        Me.lblDesignation.Name = "lblDesignation"
        Me.lblDesignation.Size = New System.Drawing.Size(66, 16)
        Me.lblDesignation.TabIndex = 7
        Me.lblDesignation.Text = "Designation"
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
        Me.txtName.Location = New System.Drawing.Point(139, 47)
        Me.txtName.MaxLength = 50
        Me.txtName.MendatroryField = False
        Me.txtName.MyLinkLable1 = Me.lblName
        Me.txtName.MyLinkLable2 = Nothing
        Me.txtName.Name = "txtName"
        Me.txtName.ReferenceFieldDesc = Nothing
        Me.txtName.ReferenceFieldName = Nothing
        Me.txtName.ReferenceTableName = Nothing
        Me.txtName.Size = New System.Drawing.Size(428, 18)
        Me.txtName.TabIndex = 2
        '
        'lblName
        '
        Me.lblName.FieldName = Nothing
        Me.lblName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(13, 45)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(36, 16)
        Me.lblName.TabIndex = 3
        Me.lblName.Text = "Name"
        '
        'rdbtnRefresh
        '
        Me.rdbtnRefresh.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnRefresh.Image = Global.XpertERPTDS.My.Resources.Resources._new
        Me.rdbtnRefresh.Location = New System.Drawing.Point(370, 18)
        Me.rdbtnRefresh.Name = "rdbtnRefresh"
        Me.rdbtnRefresh.Size = New System.Drawing.Size(17, 21)
        Me.rdbtnRefresh.TabIndex = 1
        '
        'rbtnClose
        '
        Me.rbtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbtnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnClose.Location = New System.Drawing.Point(535, 6)
        Me.rbtnClose.Name = "rbtnClose"
        Me.rbtnClose.Size = New System.Drawing.Size(68, 18)
        Me.rbtnClose.TabIndex = 23
        Me.rbtnClose.Text = "Close"
        '
        'rbtnDelete
        '
        Me.rbtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnDelete.Location = New System.Drawing.Point(90, 6)
        Me.rbtnDelete.Name = "rbtnDelete"
        Me.rbtnDelete.Size = New System.Drawing.Size(68, 18)
        Me.rbtnDelete.TabIndex = 22
        Me.rbtnDelete.Text = "Delete"
        '
        'rbtnSave
        '
        Me.rbtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnSave.Location = New System.Drawing.Point(20, 6)
        Me.rbtnSave.Name = "rbtnSave"
        Me.rbtnSave.Size = New System.Drawing.Size(68, 18)
        Me.rbtnSave.TabIndex = 21
        Me.rbtnSave.Text = "Save"
        '
        'rdmenufile
        '
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.radmenu})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(623, 20)
        Me.rdmenufile.TabIndex = 0
        Me.rdmenufile.Text = "FILE"
        '
        'radmenu
        '
        Me.radmenu.AccessibleDescription = "File"
        Me.radmenu.AccessibleName = "File"
        Me.radmenu.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItemImport, Me.RadMenuItemExport, Me.RadMenuItemClose})
        Me.radmenu.Name = "radmenu"
        Me.radmenu.Text = "File"
        '
        'RadMenuItemImport
        '
        Me.RadMenuItemImport.AccessibleDescription = "Import"
        Me.RadMenuItemImport.AccessibleName = "Import"
        Me.RadMenuItemImport.Name = "RadMenuItemImport"
        Me.RadMenuItemImport.Text = "Import"
        '
        'RadMenuItemExport
        '
        Me.RadMenuItemExport.AccessibleDescription = "Export"
        Me.RadMenuItemExport.AccessibleName = "Export"
        Me.RadMenuItemExport.Name = "RadMenuItemExport"
        Me.RadMenuItemExport.Text = "Export"
        '
        'RadMenuItemClose
        '
        Me.RadMenuItemClose.AccessibleDescription = "Close"
        Me.RadMenuItemClose.AccessibleName = "Close"
        Me.RadMenuItemClose.Name = "RadMenuItemClose"
        Me.RadMenuItemClose.Text = "Close"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(623, 476)
        Me.SplitContainer1.SplitterDistance = 445
        Me.SplitContainer1.TabIndex = 1
        '
        'frmResponsiblePerson
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(623, 496)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Name = "frmResponsiblePerson"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Responsible Person"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblStateCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBranch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSignaturePath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSignaturePath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEMail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEMail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFaxNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFaxNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPinCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPinCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTelephoneNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTelephoneNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStateCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBranch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAddress2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAddress1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFathersName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFathersName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesignation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDesignation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtCity As common.Controls.MyTextBox
    Friend WithEvents txtAddress2 As common.Controls.MyTextBox
    Friend WithEvents txtAddress1 As common.Controls.MyTextBox
    Friend WithEvents txtFathersName As common.Controls.MyTextBox
    Friend WithEvents txtDesignation As common.Controls.MyTextBox
    Friend WithEvents txtName As common.Controls.MyTextBox
    Friend WithEvents rdbtnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtSignaturePath As common.Controls.MyTextBox
    Friend WithEvents txtEMail As common.Controls.MyTextBox
    Friend WithEvents txtFaxNo As common.Controls.MyTextBox
    Friend WithEvents txtPinCode As common.Controls.MyTextBox
    Friend WithEvents txtTelephoneNo As common.Controls.MyTextBox
    Friend WithEvents txtCountry As common.Controls.MyTextBox
    Friend WithEvents txtStateCode As common.Controls.MyTextBox
    Friend WithEvents txtBranch As common.Controls.MyTextBox
    Friend WithEvents chkActive As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rbtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents radmenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblCity As common.Controls.MyLabel
    Friend WithEvents lblAddress As common.Controls.MyLabel
    Friend WithEvents lblFathersName As common.Controls.MyLabel
    Friend WithEvents lblDesignation As common.Controls.MyLabel
    Friend WithEvents lblName As common.Controls.MyLabel
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents lblSignaturePath As common.Controls.MyLabel
    Friend WithEvents lblEMail As common.Controls.MyLabel
    Friend WithEvents lblFaxNo As common.Controls.MyLabel
    Friend WithEvents lblPinCode As common.Controls.MyLabel
    Friend WithEvents lblTelephoneNo As common.Controls.MyLabel
    Friend WithEvents lblCountry As common.Controls.MyLabel
    Friend WithEvents lblStateCode As common.Controls.MyLabel
    Friend WithEvents lblBranch As common.Controls.MyLabel
    Friend WithEvents lblActive As common.Controls.MyLabel
    Friend WithEvents fndCodeNew As common.UserControls.txtNavigator
    Friend WithEvents fndStateCodeNew As common.UserControls.txtFinder
    Friend WithEvents fndbranchCode As common.UserControls.txtFinder
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

