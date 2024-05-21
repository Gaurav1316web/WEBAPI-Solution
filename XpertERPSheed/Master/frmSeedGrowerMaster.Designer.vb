Imports XpertERPEngine
Imports XpertERPEngineFine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSeedGrowerMaster
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtAadharNo = New common.Controls.MyTextBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtPAN = New common.Controls.MyTextBox()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtAdd2 = New common.Controls.MyTextBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.txtAdd1 = New common.Controls.MyTextBox()
        Me.txtAdd3 = New common.Controls.MyTextBox()
        Me.txtKhasraNo = New common.Controls.MyTextBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtRegNo = New common.Controls.MyTextBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.TxtAccNo = New common.Controls.MyTextBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel25 = New common.Controls.MyLabel()
        Me.TxtIFSCCode = New common.Controls.MyTextBox()
        Me.MyLabel27 = New common.Controls.MyLabel()
        Me.txtbranchname = New common.Controls.MyTextBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.TxtBankName = New common.Controls.MyTextBox()
        Me.MyLabel28 = New common.Controls.MyLabel()
        Me.lblDistrict = New common.Controls.MyLabel()
        Me.txtDistrict = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblVillage = New common.Controls.MyLabel()
        Me.fndVillegeCode = New common.UserControls.txtFinder()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtTotal = New common.MyNumBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtLeaseLand = New common.MyNumBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtOwnLand = New common.MyNumBox()
        Me.txtFamilyLand = New common.MyNumBox()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtMobile = New common.Controls.MyTextBox()
        Me.lblTelephone = New common.Controls.MyLabel()
        Me.MyLabel38 = New common.Controls.MyLabel()
        Me.txtTehsil = New common.Controls.MyTextBox()
        Me.lblTehsil = New common.Controls.MyLabel()
        Me.lblFatherName = New common.Controls.MyLabel()
        Me.txtFatherName = New common.Controls.MyTextBox()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.txtname = New common.Controls.MyTextBox()
        Me.lblName = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtAadharNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKhasraNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRegNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        CType(Me.TxtAccNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtIFSCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbranchname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDistrict, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVillage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLeaseLand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOwnLand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFamilyLand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMobile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTelephone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTehsil, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTehsil, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFatherName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFatherName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAadharNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel13)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPAN)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel12)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAdd2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAdd1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAdd3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtKhasraNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel11)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRegNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel10)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDistrict)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDistrict)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblVillage)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndVillegeCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTotal)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLeaseLand)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtOwnLand)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFamilyLand)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel23)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMobile)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTelephone)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel38)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTehsil)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTehsil)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblFatherName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFatherName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtname)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(943, 539)
        Me.SplitContainer1.SplitterDistance = 498
        Me.SplitContainer1.TabIndex = 0
        '
        'txtAadharNo
        '
        Me.txtAadharNo.CalculationExpression = Nothing
        Me.txtAadharNo.FieldCode = Nothing
        Me.txtAadharNo.FieldDesc = Nothing
        Me.txtAadharNo.FieldMaxLength = 0
        Me.txtAadharNo.FieldName = Nothing
        Me.txtAadharNo.isCalculatedField = False
        Me.txtAadharNo.IsSourceFromTable = False
        Me.txtAadharNo.IsSourceFromValueList = False
        Me.txtAadharNo.IsUnique = False
        Me.txtAadharNo.Location = New System.Drawing.Point(123, 324)
        Me.txtAadharNo.MendatroryField = False
        Me.txtAadharNo.MyLinkLable1 = Nothing
        Me.txtAadharNo.MyLinkLable2 = Nothing
        Me.txtAadharNo.Name = "txtAadharNo"
        Me.txtAadharNo.ReferenceFieldDesc = Nothing
        Me.txtAadharNo.ReferenceFieldName = Nothing
        Me.txtAadharNo.ReferenceTableName = Nothing
        Me.txtAadharNo.Size = New System.Drawing.Size(226, 20)
        Me.txtAadharNo.TabIndex = 1485
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(17, 328)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel13.TabIndex = 1486
        Me.MyLabel13.Text = "Aadhar No."
        '
        'txtPAN
        '
        Me.txtPAN.CalculationExpression = Nothing
        Me.txtPAN.FieldCode = Nothing
        Me.txtPAN.FieldDesc = Nothing
        Me.txtPAN.FieldMaxLength = 0
        Me.txtPAN.FieldName = Nothing
        Me.txtPAN.isCalculatedField = False
        Me.txtPAN.IsSourceFromTable = False
        Me.txtPAN.IsSourceFromValueList = False
        Me.txtPAN.IsUnique = False
        Me.txtPAN.Location = New System.Drawing.Point(123, 303)
        Me.txtPAN.MendatroryField = False
        Me.txtPAN.MyLinkLable1 = Nothing
        Me.txtPAN.MyLinkLable2 = Nothing
        Me.txtPAN.Name = "txtPAN"
        Me.txtPAN.ReferenceFieldDesc = Nothing
        Me.txtPAN.ReferenceFieldName = Nothing
        Me.txtPAN.ReferenceTableName = Nothing
        Me.txtPAN.Size = New System.Drawing.Size(226, 20)
        Me.txtPAN.TabIndex = 1483
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(17, 307)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(50, 16)
        Me.MyLabel12.TabIndex = 1484
        Me.MyLabel12.Text = "PAN No."
        '
        'txtAdd2
        '
        Me.txtAdd2.CalculationExpression = Nothing
        Me.txtAdd2.FieldCode = Nothing
        Me.txtAdd2.FieldDesc = Nothing
        Me.txtAdd2.FieldMaxLength = 0
        Me.txtAdd2.FieldName = Nothing
        Me.txtAdd2.isCalculatedField = False
        Me.txtAdd2.IsSourceFromTable = False
        Me.txtAdd2.IsSourceFromValueList = False
        Me.txtAdd2.IsUnique = False
        Me.txtAdd2.Location = New System.Drawing.Point(123, 127)
        Me.txtAdd2.MaxLength = 75
        Me.txtAdd2.MendatroryField = False
        Me.txtAdd2.MyLinkLable1 = Me.RadLabel2
        Me.txtAdd2.MyLinkLable2 = Nothing
        Me.txtAdd2.Name = "txtAdd2"
        Me.txtAdd2.ReferenceFieldDesc = Nothing
        Me.txtAdd2.ReferenceFieldName = Nothing
        Me.txtAdd2.ReferenceTableName = Nothing
        Me.txtAdd2.Size = New System.Drawing.Size(553, 20)
        Me.txtAdd2.TabIndex = 1480
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(17, 105)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(48, 16)
        Me.RadLabel2.TabIndex = 1482
        Me.RadLabel2.Text = "Address"
        '
        'txtAdd1
        '
        Me.txtAdd1.CalculationExpression = Nothing
        Me.txtAdd1.FieldCode = Nothing
        Me.txtAdd1.FieldDesc = Nothing
        Me.txtAdd1.FieldMaxLength = 0
        Me.txtAdd1.FieldName = Nothing
        Me.txtAdd1.isCalculatedField = False
        Me.txtAdd1.IsSourceFromTable = False
        Me.txtAdd1.IsSourceFromValueList = False
        Me.txtAdd1.IsUnique = False
        Me.txtAdd1.Location = New System.Drawing.Point(123, 105)
        Me.txtAdd1.MaxLength = 75
        Me.txtAdd1.MendatroryField = False
        Me.txtAdd1.MyLinkLable1 = Me.RadLabel2
        Me.txtAdd1.MyLinkLable2 = Nothing
        Me.txtAdd1.Name = "txtAdd1"
        Me.txtAdd1.ReferenceFieldDesc = Nothing
        Me.txtAdd1.ReferenceFieldName = Nothing
        Me.txtAdd1.ReferenceTableName = Nothing
        Me.txtAdd1.Size = New System.Drawing.Size(553, 20)
        Me.txtAdd1.TabIndex = 1479
        '
        'txtAdd3
        '
        Me.txtAdd3.CalculationExpression = Nothing
        Me.txtAdd3.FieldCode = Nothing
        Me.txtAdd3.FieldDesc = Nothing
        Me.txtAdd3.FieldMaxLength = 0
        Me.txtAdd3.FieldName = Nothing
        Me.txtAdd3.isCalculatedField = False
        Me.txtAdd3.IsSourceFromTable = False
        Me.txtAdd3.IsSourceFromValueList = False
        Me.txtAdd3.IsUnique = False
        Me.txtAdd3.Location = New System.Drawing.Point(123, 149)
        Me.txtAdd3.MaxLength = 75
        Me.txtAdd3.MendatroryField = False
        Me.txtAdd3.MyLinkLable1 = Me.RadLabel2
        Me.txtAdd3.MyLinkLable2 = Nothing
        Me.txtAdd3.Name = "txtAdd3"
        Me.txtAdd3.ReferenceFieldDesc = Nothing
        Me.txtAdd3.ReferenceFieldName = Nothing
        Me.txtAdd3.ReferenceTableName = Nothing
        Me.txtAdd3.Size = New System.Drawing.Size(553, 20)
        Me.txtAdd3.TabIndex = 1481
        '
        'txtKhasraNo
        '
        Me.txtKhasraNo.CalculationExpression = Nothing
        Me.txtKhasraNo.FieldCode = Nothing
        Me.txtKhasraNo.FieldDesc = Nothing
        Me.txtKhasraNo.FieldMaxLength = 0
        Me.txtKhasraNo.FieldName = Nothing
        Me.txtKhasraNo.isCalculatedField = False
        Me.txtKhasraNo.IsSourceFromTable = False
        Me.txtKhasraNo.IsSourceFromValueList = False
        Me.txtKhasraNo.IsUnique = False
        Me.txtKhasraNo.Location = New System.Drawing.Point(123, 282)
        Me.txtKhasraNo.MendatroryField = False
        Me.txtKhasraNo.MyLinkLable1 = Me.MyLabel11
        Me.txtKhasraNo.MyLinkLable2 = Nothing
        Me.txtKhasraNo.Name = "txtKhasraNo"
        Me.txtKhasraNo.ReferenceFieldDesc = Nothing
        Me.txtKhasraNo.ReferenceFieldName = Nothing
        Me.txtKhasraNo.ReferenceTableName = Nothing
        Me.txtKhasraNo.Size = New System.Drawing.Size(226, 20)
        Me.txtKhasraNo.TabIndex = 1477
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(17, 286)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel11.TabIndex = 1478
        Me.MyLabel11.Text = "Khasra No."
        '
        'txtRegNo
        '
        Me.txtRegNo.CalculationExpression = Nothing
        Me.txtRegNo.FieldCode = Nothing
        Me.txtRegNo.FieldDesc = Nothing
        Me.txtRegNo.FieldMaxLength = 0
        Me.txtRegNo.FieldName = Nothing
        Me.txtRegNo.isCalculatedField = False
        Me.txtRegNo.IsSourceFromTable = False
        Me.txtRegNo.IsSourceFromValueList = False
        Me.txtRegNo.IsUnique = False
        Me.txtRegNo.Location = New System.Drawing.Point(123, 261)
        Me.txtRegNo.MendatroryField = False
        Me.txtRegNo.MyLinkLable1 = Me.MyLabel10
        Me.txtRegNo.MyLinkLable2 = Nothing
        Me.txtRegNo.Name = "txtRegNo"
        Me.txtRegNo.ReferenceFieldDesc = Nothing
        Me.txtRegNo.ReferenceFieldName = Nothing
        Me.txtRegNo.ReferenceTableName = Nothing
        Me.txtRegNo.Size = New System.Drawing.Size(226, 20)
        Me.txtRegNo.TabIndex = 1475
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(17, 265)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel10.TabIndex = 1476
        Me.MyLabel10.Text = "Registration No."
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.TxtAccNo)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel25)
        Me.RadGroupBox6.Controls.Add(Me.TxtIFSCCode)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel27)
        Me.RadGroupBox6.Controls.Add(Me.txtbranchname)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox6.Controls.Add(Me.TxtBankName)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel28)
        Me.RadGroupBox6.HeaderText = "Bank Details"
        Me.RadGroupBox6.Location = New System.Drawing.Point(12, 350)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Size = New System.Drawing.Size(449, 121)
        Me.RadGroupBox6.TabIndex = 1474
        Me.RadGroupBox6.Text = "Bank Details"
        '
        'TxtAccNo
        '
        Me.TxtAccNo.CalculationExpression = Nothing
        Me.TxtAccNo.FieldCode = Nothing
        Me.TxtAccNo.FieldDesc = Nothing
        Me.TxtAccNo.FieldMaxLength = 0
        Me.TxtAccNo.FieldName = Nothing
        Me.TxtAccNo.isCalculatedField = False
        Me.TxtAccNo.IsSourceFromTable = False
        Me.TxtAccNo.IsSourceFromValueList = False
        Me.TxtAccNo.IsUnique = False
        Me.TxtAccNo.Location = New System.Drawing.Point(110, 93)
        Me.TxtAccNo.MaxLength = 50
        Me.TxtAccNo.MendatroryField = False
        Me.TxtAccNo.MyLinkLable1 = Me.MyLabel8
        Me.TxtAccNo.MyLinkLable2 = Nothing
        Me.TxtAccNo.Name = "TxtAccNo"
        Me.TxtAccNo.ReferenceFieldDesc = Nothing
        Me.TxtAccNo.ReferenceFieldName = Nothing
        Me.TxtAccNo.ReferenceTableName = Nothing
        Me.TxtAccNo.Size = New System.Drawing.Size(321, 20)
        Me.TxtAccNo.TabIndex = 121
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(355, 287)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(31, 18)
        Me.MyLabel8.TabIndex = 1467
        Me.MyLabel8.Text = "Total"
        '
        'MyLabel25
        '
        Me.MyLabel25.FieldName = Nothing
        Me.MyLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel25.Location = New System.Drawing.Point(9, 93)
        Me.MyLabel25.Name = "MyLabel25"
        Me.MyLabel25.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel25.TabIndex = 38
        Me.MyLabel25.Text = "Account No."
        '
        'TxtIFSCCode
        '
        Me.TxtIFSCCode.CalculationExpression = Nothing
        Me.TxtIFSCCode.FieldCode = Nothing
        Me.TxtIFSCCode.FieldDesc = Nothing
        Me.TxtIFSCCode.FieldMaxLength = 0
        Me.TxtIFSCCode.FieldName = Nothing
        Me.TxtIFSCCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIFSCCode.isCalculatedField = False
        Me.TxtIFSCCode.IsSourceFromTable = False
        Me.TxtIFSCCode.IsSourceFromValueList = False
        Me.TxtIFSCCode.IsUnique = False
        Me.TxtIFSCCode.Location = New System.Drawing.Point(111, 46)
        Me.TxtIFSCCode.MaxLength = 50
        Me.TxtIFSCCode.MendatroryField = False
        Me.TxtIFSCCode.MyLinkLable1 = Me.MyLabel27
        Me.TxtIFSCCode.MyLinkLable2 = Nothing
        Me.TxtIFSCCode.Name = "TxtIFSCCode"
        Me.TxtIFSCCode.ReferenceFieldDesc = Nothing
        Me.TxtIFSCCode.ReferenceFieldName = Nothing
        Me.TxtIFSCCode.ReferenceTableName = Nothing
        Me.TxtIFSCCode.Size = New System.Drawing.Size(320, 18)
        Me.TxtIFSCCode.TabIndex = 5
        '
        'MyLabel27
        '
        Me.MyLabel27.FieldName = Nothing
        Me.MyLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel27.Location = New System.Drawing.Point(9, 47)
        Me.MyLabel27.Name = "MyLabel27"
        Me.MyLabel27.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel27.TabIndex = 29
        Me.MyLabel27.Text = "IFSC Code"
        '
        'txtbranchname
        '
        Me.txtbranchname.CalculationExpression = Nothing
        Me.txtbranchname.FieldCode = Nothing
        Me.txtbranchname.FieldDesc = Nothing
        Me.txtbranchname.FieldMaxLength = 0
        Me.txtbranchname.FieldName = Nothing
        Me.txtbranchname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbranchname.isCalculatedField = False
        Me.txtbranchname.IsSourceFromTable = False
        Me.txtbranchname.IsSourceFromValueList = False
        Me.txtbranchname.IsUnique = False
        Me.txtbranchname.Location = New System.Drawing.Point(110, 70)
        Me.txtbranchname.MaxLength = 150
        Me.txtbranchname.MendatroryField = False
        Me.txtbranchname.MyLinkLable1 = Me.MyLabel9
        Me.txtbranchname.MyLinkLable2 = Nothing
        Me.txtbranchname.Name = "txtbranchname"
        Me.txtbranchname.ReferenceFieldDesc = Nothing
        Me.txtbranchname.ReferenceFieldName = Nothing
        Me.txtbranchname.ReferenceTableName = Nothing
        Me.txtbranchname.Size = New System.Drawing.Size(321, 18)
        Me.txtbranchname.TabIndex = 4
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(9, 71)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel9.TabIndex = 27
        Me.MyLabel9.Text = "Bank Branch"
        '
        'TxtBankName
        '
        Me.TxtBankName.CalculationExpression = Nothing
        Me.TxtBankName.FieldCode = Nothing
        Me.TxtBankName.FieldDesc = Nothing
        Me.TxtBankName.FieldMaxLength = 0
        Me.TxtBankName.FieldName = Nothing
        Me.TxtBankName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBankName.isCalculatedField = False
        Me.TxtBankName.IsSourceFromTable = False
        Me.TxtBankName.IsSourceFromValueList = False
        Me.TxtBankName.IsUnique = False
        Me.TxtBankName.Location = New System.Drawing.Point(111, 23)
        Me.TxtBankName.MaxLength = 50
        Me.TxtBankName.MendatroryField = False
        Me.TxtBankName.MyLinkLable1 = Me.MyLabel28
        Me.TxtBankName.MyLinkLable2 = Nothing
        Me.TxtBankName.Name = "TxtBankName"
        Me.TxtBankName.ReferenceFieldDesc = Nothing
        Me.TxtBankName.ReferenceFieldName = Nothing
        Me.TxtBankName.ReferenceTableName = Nothing
        Me.TxtBankName.Size = New System.Drawing.Size(320, 18)
        Me.TxtBankName.TabIndex = 1
        Me.TxtBankName.TabStop = False
        '
        'MyLabel28
        '
        Me.MyLabel28.FieldName = Nothing
        Me.MyLabel28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel28.Location = New System.Drawing.Point(9, 25)
        Me.MyLabel28.Name = "MyLabel28"
        Me.MyLabel28.Size = New System.Drawing.Size(78, 16)
        Me.MyLabel28.TabIndex = 26
        Me.MyLabel28.Text = "Name of Bank"
        '
        'lblDistrict
        '
        Me.lblDistrict.AutoSize = False
        Me.lblDistrict.BorderVisible = True
        Me.lblDistrict.FieldName = Nothing
        Me.lblDistrict.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDistrict.Location = New System.Drawing.Point(352, 197)
        Me.lblDistrict.Name = "lblDistrict"
        Me.lblDistrict.Size = New System.Drawing.Size(324, 19)
        Me.lblDistrict.TabIndex = 1473
        Me.lblDistrict.TextWrap = False
        '
        'txtDistrict
        '
        Me.txtDistrict.CalculationExpression = Nothing
        Me.txtDistrict.FieldCode = Nothing
        Me.txtDistrict.FieldDesc = Nothing
        Me.txtDistrict.FieldMaxLength = 0
        Me.txtDistrict.FieldName = Nothing
        Me.txtDistrict.isCalculatedField = False
        Me.txtDistrict.IsSourceFromTable = False
        Me.txtDistrict.IsSourceFromValueList = False
        Me.txtDistrict.IsUnique = False
        Me.txtDistrict.Location = New System.Drawing.Point(123, 197)
        Me.txtDistrict.MendatroryField = True
        Me.txtDistrict.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDistrict.MyLinkLable1 = Me.MyLabel1
        Me.txtDistrict.MyLinkLable2 = Me.lblDistrict
        Me.txtDistrict.MyReadOnly = False
        Me.txtDistrict.MyShowMasterFormButton = False
        Me.txtDistrict.Name = "txtDistrict"
        Me.txtDistrict.ReferenceFieldDesc = Nothing
        Me.txtDistrict.ReferenceFieldName = Nothing
        Me.txtDistrict.ReferenceTableName = Nothing
        Me.txtDistrict.Size = New System.Drawing.Size(226, 20)
        Me.txtDistrict.TabIndex = 1472
        Me.txtDistrict.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(17, 174)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(40, 16)
        Me.MyLabel1.TabIndex = 108
        Me.MyLabel1.Text = "Village"
        '
        'lblVillage
        '
        Me.lblVillage.AutoSize = False
        Me.lblVillage.BorderVisible = True
        Me.lblVillage.FieldName = Nothing
        Me.lblVillage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVillage.Location = New System.Drawing.Point(352, 174)
        Me.lblVillage.Name = "lblVillage"
        Me.lblVillage.Size = New System.Drawing.Size(324, 19)
        Me.lblVillage.TabIndex = 1471
        Me.lblVillage.TextWrap = False
        '
        'fndVillegeCode
        '
        Me.fndVillegeCode.CalculationExpression = Nothing
        Me.fndVillegeCode.FieldCode = Nothing
        Me.fndVillegeCode.FieldDesc = Nothing
        Me.fndVillegeCode.FieldMaxLength = 0
        Me.fndVillegeCode.FieldName = Nothing
        Me.fndVillegeCode.isCalculatedField = False
        Me.fndVillegeCode.IsSourceFromTable = False
        Me.fndVillegeCode.IsSourceFromValueList = False
        Me.fndVillegeCode.IsUnique = False
        Me.fndVillegeCode.Location = New System.Drawing.Point(123, 174)
        Me.fndVillegeCode.MendatroryField = True
        Me.fndVillegeCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVillegeCode.MyLinkLable1 = Me.MyLabel1
        Me.fndVillegeCode.MyLinkLable2 = Me.lblVillage
        Me.fndVillegeCode.MyReadOnly = False
        Me.fndVillegeCode.MyShowMasterFormButton = False
        Me.fndVillegeCode.Name = "fndVillegeCode"
        Me.fndVillegeCode.ReferenceFieldDesc = Nothing
        Me.fndVillegeCode.ReferenceFieldName = Nothing
        Me.fndVillegeCode.ReferenceTableName = Nothing
        Me.fndVillegeCode.Size = New System.Drawing.Size(226, 20)
        Me.fndVillegeCode.TabIndex = 1470
        Me.fndVillegeCode.Value = ""
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(570, 287)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel7.TabIndex = 1469
        Me.MyLabel7.Text = "Acre"
        '
        'txtTotal
        '
        Me.txtTotal.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTotal.CalculationExpression = Nothing
        Me.txtTotal.DecimalPlaces = 2
        Me.txtTotal.FieldCode = Nothing
        Me.txtTotal.FieldDesc = Nothing
        Me.txtTotal.FieldMaxLength = 0
        Me.txtTotal.FieldName = Nothing
        Me.txtTotal.isCalculatedField = False
        Me.txtTotal.IsSourceFromTable = False
        Me.txtTotal.IsSourceFromValueList = False
        Me.txtTotal.IsUnique = False
        Me.txtTotal.Location = New System.Drawing.Point(461, 286)
        Me.txtTotal.MendatroryField = False
        Me.txtTotal.MyLinkLable1 = Nothing
        Me.txtTotal.MyLinkLable2 = Nothing
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReferenceFieldDesc = Nothing
        Me.txtTotal.ReferenceFieldName = Nothing
        Me.txtTotal.ReferenceTableName = Nothing
        Me.txtTotal.Size = New System.Drawing.Size(98, 20)
        Me.txtTotal.TabIndex = 1468
        Me.txtTotal.Text = "0"
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotal.Value = 0R
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(570, 265)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel5.TabIndex = 1463
        Me.MyLabel5.Text = "Acre"
        '
        'txtLeaseLand
        '
        Me.txtLeaseLand.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtLeaseLand.CalculationExpression = Nothing
        Me.txtLeaseLand.DecimalPlaces = 2
        Me.txtLeaseLand.FieldCode = Nothing
        Me.txtLeaseLand.FieldDesc = Nothing
        Me.txtLeaseLand.FieldMaxLength = 0
        Me.txtLeaseLand.FieldName = Nothing
        Me.txtLeaseLand.isCalculatedField = False
        Me.txtLeaseLand.IsSourceFromTable = False
        Me.txtLeaseLand.IsSourceFromValueList = False
        Me.txtLeaseLand.IsUnique = False
        Me.txtLeaseLand.Location = New System.Drawing.Point(461, 264)
        Me.txtLeaseLand.MendatroryField = False
        Me.txtLeaseLand.MyLinkLable1 = Nothing
        Me.txtLeaseLand.MyLinkLable2 = Nothing
        Me.txtLeaseLand.Name = "txtLeaseLand"
        Me.txtLeaseLand.ReferenceFieldDesc = Nothing
        Me.txtLeaseLand.ReferenceFieldName = Nothing
        Me.txtLeaseLand.ReferenceTableName = Nothing
        Me.txtLeaseLand.Size = New System.Drawing.Size(98, 20)
        Me.txtLeaseLand.TabIndex = 1462
        Me.txtLeaseLand.Text = "0"
        Me.txtLeaseLand.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLeaseLand.Value = 0R
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(355, 265)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(61, 18)
        Me.MyLabel6.TabIndex = 1461
        Me.MyLabel6.Text = "Lease Land"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(570, 243)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel3.TabIndex = 1460
        Me.MyLabel3.Text = "Acre"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(570, 221)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel2.TabIndex = 1457
        Me.MyLabel2.Text = "Acre"
        '
        'txtOwnLand
        '
        Me.txtOwnLand.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtOwnLand.CalculationExpression = Nothing
        Me.txtOwnLand.DecimalPlaces = 2
        Me.txtOwnLand.FieldCode = Nothing
        Me.txtOwnLand.FieldDesc = Nothing
        Me.txtOwnLand.FieldMaxLength = 0
        Me.txtOwnLand.FieldName = Nothing
        Me.txtOwnLand.isCalculatedField = False
        Me.txtOwnLand.IsSourceFromTable = False
        Me.txtOwnLand.IsSourceFromValueList = False
        Me.txtOwnLand.IsUnique = False
        Me.txtOwnLand.Location = New System.Drawing.Point(461, 220)
        Me.txtOwnLand.MendatroryField = False
        Me.txtOwnLand.MyLinkLable1 = Nothing
        Me.txtOwnLand.MyLinkLable2 = Nothing
        Me.txtOwnLand.Name = "txtOwnLand"
        Me.txtOwnLand.ReferenceFieldDesc = Nothing
        Me.txtOwnLand.ReferenceFieldName = Nothing
        Me.txtOwnLand.ReferenceTableName = Nothing
        Me.txtOwnLand.Size = New System.Drawing.Size(98, 20)
        Me.txtOwnLand.TabIndex = 1456
        Me.txtOwnLand.Text = "0"
        Me.txtOwnLand.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtOwnLand.Value = 0R
        '
        'txtFamilyLand
        '
        Me.txtFamilyLand.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtFamilyLand.CalculationExpression = Nothing
        Me.txtFamilyLand.DecimalPlaces = 2
        Me.txtFamilyLand.FieldCode = Nothing
        Me.txtFamilyLand.FieldDesc = Nothing
        Me.txtFamilyLand.FieldMaxLength = 0
        Me.txtFamilyLand.FieldName = Nothing
        Me.txtFamilyLand.isCalculatedField = False
        Me.txtFamilyLand.IsSourceFromTable = False
        Me.txtFamilyLand.IsSourceFromValueList = False
        Me.txtFamilyLand.IsUnique = False
        Me.txtFamilyLand.Location = New System.Drawing.Point(461, 242)
        Me.txtFamilyLand.MendatroryField = False
        Me.txtFamilyLand.MyLinkLable1 = Nothing
        Me.txtFamilyLand.MyLinkLable2 = Nothing
        Me.txtFamilyLand.Name = "txtFamilyLand"
        Me.txtFamilyLand.ReferenceFieldDesc = Nothing
        Me.txtFamilyLand.ReferenceFieldName = Nothing
        Me.txtFamilyLand.ReferenceTableName = Nothing
        Me.txtFamilyLand.Size = New System.Drawing.Size(98, 20)
        Me.txtFamilyLand.TabIndex = 1459
        Me.txtFamilyLand.Text = "0"
        Me.txtFamilyLand.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFamilyLand.Value = 0R
        '
        'MyLabel23
        '
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel23.Location = New System.Drawing.Point(355, 221)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(57, 18)
        Me.MyLabel23.TabIndex = 1455
        Me.MyLabel23.Text = "Own Land"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(355, 243)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(65, 18)
        Me.MyLabel4.TabIndex = 1458
        Me.MyLabel4.Text = "Family Land"
        '
        'txtMobile
        '
        Me.txtMobile.CalculationExpression = Nothing
        Me.txtMobile.FieldCode = Nothing
        Me.txtMobile.FieldDesc = Nothing
        Me.txtMobile.FieldMaxLength = 0
        Me.txtMobile.FieldName = Nothing
        Me.txtMobile.isCalculatedField = False
        Me.txtMobile.IsSourceFromTable = False
        Me.txtMobile.IsSourceFromValueList = False
        Me.txtMobile.IsUnique = False
        Me.txtMobile.Location = New System.Drawing.Point(123, 240)
        Me.txtMobile.MendatroryField = False
        Me.txtMobile.MyLinkLable1 = Me.lblTelephone
        Me.txtMobile.MyLinkLable2 = Nothing
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.ReferenceFieldDesc = Nothing
        Me.txtMobile.ReferenceFieldName = Nothing
        Me.txtMobile.ReferenceTableName = Nothing
        Me.txtMobile.Size = New System.Drawing.Size(226, 20)
        Me.txtMobile.TabIndex = 407
        '
        'lblTelephone
        '
        Me.lblTelephone.FieldName = Nothing
        Me.lblTelephone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTelephone.Location = New System.Drawing.Point(17, 244)
        Me.lblTelephone.Name = "lblTelephone"
        Me.lblTelephone.Size = New System.Drawing.Size(60, 16)
        Me.lblTelephone.TabIndex = 408
        Me.lblTelephone.Text = "Mobile No."
        '
        'MyLabel38
        '
        Me.MyLabel38.FieldName = Nothing
        Me.MyLabel38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel38.Location = New System.Drawing.Point(17, 197)
        Me.MyLabel38.Name = "MyLabel38"
        Me.MyLabel38.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel38.TabIndex = 404
        Me.MyLabel38.Text = "District"
        '
        'txtTehsil
        '
        Me.txtTehsil.CalculationExpression = Nothing
        Me.txtTehsil.FieldCode = Nothing
        Me.txtTehsil.FieldDesc = Nothing
        Me.txtTehsil.FieldMaxLength = 0
        Me.txtTehsil.FieldName = Nothing
        Me.txtTehsil.isCalculatedField = False
        Me.txtTehsil.IsSourceFromTable = False
        Me.txtTehsil.IsSourceFromValueList = False
        Me.txtTehsil.IsUnique = False
        Me.txtTehsil.Location = New System.Drawing.Point(123, 219)
        Me.txtTehsil.MendatroryField = False
        Me.txtTehsil.MyLinkLable1 = Nothing
        Me.txtTehsil.MyLinkLable2 = Nothing
        Me.txtTehsil.Name = "txtTehsil"
        Me.txtTehsil.ReferenceFieldDesc = Nothing
        Me.txtTehsil.ReferenceFieldName = Nothing
        Me.txtTehsil.ReferenceTableName = Nothing
        Me.txtTehsil.Size = New System.Drawing.Size(226, 20)
        Me.txtTehsil.TabIndex = 105
        '
        'lblTehsil
        '
        Me.lblTehsil.FieldName = Nothing
        Me.lblTehsil.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTehsil.Location = New System.Drawing.Point(17, 223)
        Me.lblTehsil.Name = "lblTehsil"
        Me.lblTehsil.Size = New System.Drawing.Size(36, 16)
        Me.lblTehsil.TabIndex = 106
        Me.lblTehsil.Text = "Tehsil"
        '
        'lblFatherName
        '
        Me.lblFatherName.FieldName = Nothing
        Me.lblFatherName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFatherName.Location = New System.Drawing.Point(17, 85)
        Me.lblFatherName.Name = "lblFatherName"
        Me.lblFatherName.Size = New System.Drawing.Size(72, 16)
        Me.lblFatherName.TabIndex = 59
        Me.lblFatherName.Text = "Father Name"
        '
        'txtFatherName
        '
        Me.txtFatherName.CalculationExpression = Nothing
        Me.txtFatherName.FieldCode = Nothing
        Me.txtFatherName.FieldDesc = Nothing
        Me.txtFatherName.FieldMaxLength = 0
        Me.txtFatherName.FieldName = Nothing
        Me.txtFatherName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFatherName.isCalculatedField = False
        Me.txtFatherName.IsSourceFromTable = False
        Me.txtFatherName.IsSourceFromValueList = False
        Me.txtFatherName.IsUnique = False
        Me.txtFatherName.Location = New System.Drawing.Point(123, 85)
        Me.txtFatherName.MaxLength = 50
        Me.txtFatherName.MendatroryField = False
        Me.txtFatherName.MyLinkLable1 = Me.lblFatherName
        Me.txtFatherName.MyLinkLable2 = Nothing
        Me.txtFatherName.Name = "txtFatherName"
        Me.txtFatherName.ReferenceFieldDesc = Nothing
        Me.txtFatherName.ReferenceFieldName = Nothing
        Me.txtFatherName.ReferenceTableName = Nothing
        Me.txtFatherName.Size = New System.Drawing.Size(282, 18)
        Me.txtFatherName.TabIndex = 58
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(943, 20)
        Me.RadMenu1.TabIndex = 56
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.UseCompatibleTextRendering = False
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export"
        Me.RadMenuItem2.UseCompatibleTextRendering = False
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Import"
        Me.RadMenuItem4.UseCompatibleTextRendering = False
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(123, 39)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(266, 21)
        Me.txtCode.TabIndex = 51
        Me.txtCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(17, 39)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(33, 16)
        Me.lblCode.TabIndex = 55
        Me.lblCode.Text = "Code"
        '
        'txtname
        '
        Me.txtname.CalculationExpression = Nothing
        Me.txtname.FieldCode = Nothing
        Me.txtname.FieldDesc = Nothing
        Me.txtname.FieldMaxLength = 0
        Me.txtname.FieldName = Nothing
        Me.txtname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtname.isCalculatedField = False
        Me.txtname.IsSourceFromTable = False
        Me.txtname.IsSourceFromValueList = False
        Me.txtname.IsUnique = False
        Me.txtname.Location = New System.Drawing.Point(123, 63)
        Me.txtname.MaxLength = 200
        Me.txtname.MendatroryField = True
        Me.txtname.MyLinkLable1 = Me.lblName
        Me.txtname.MyLinkLable2 = Nothing
        Me.txtname.Name = "txtname"
        Me.txtname.ReferenceFieldDesc = Nothing
        Me.txtname.ReferenceFieldName = Nothing
        Me.txtname.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtname.RootElement.StretchVertically = True
        Me.txtname.Size = New System.Drawing.Size(282, 20)
        Me.txtname.TabIndex = 53
        '
        'lblName
        '
        Me.lblName.FieldName = Nothing
        Me.lblName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(17, 63)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(36, 16)
        Me.lblName.TabIndex = 54
        Me.lblName.Text = "Name"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPSheed.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(390, 39)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 21)
        Me.btnnew.TabIndex = 57
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(5, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 6
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(78, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 7
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(847, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 8
        Me.btnclose.Text = "Close"
        '
        'frmSheedGrowerMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(943, 539)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmSheedGrowerMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmSheedGrowerMaster"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtAadharNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKhasraNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRegNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.RadGroupBox6.PerformLayout()
        CType(Me.TxtAccNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtIFSCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbranchname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDistrict, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVillage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLeaseLand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOwnLand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFamilyLand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMobile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTelephone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTehsil, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTehsil, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFatherName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFatherName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents txtname As common.Controls.MyTextBox
    Friend WithEvents lblName As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblFatherName As common.Controls.MyLabel
    Friend WithEvents txtFatherName As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtTehsil As common.Controls.MyTextBox
    Friend WithEvents lblTehsil As common.Controls.MyLabel
    Friend WithEvents MyLabel38 As common.Controls.MyLabel
    Friend WithEvents txtMobile As common.Controls.MyTextBox
    Friend WithEvents lblTelephone As common.Controls.MyLabel
    Friend WithEvents txtOwnLand As common.MyNumBox
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtLeaseLand As common.MyNumBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtFamilyLand As common.MyNumBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtTotal As common.MyNumBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents lblVillage As common.Controls.MyLabel
    Friend WithEvents fndVillegeCode As common.UserControls.txtFinder
    Friend WithEvents lblDistrict As common.Controls.MyLabel
    Friend WithEvents txtDistrict As common.UserControls.txtFinder
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents TxtAccNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel25 As common.Controls.MyLabel
    Friend WithEvents TxtIFSCCode As common.Controls.MyTextBox
    Friend WithEvents MyLabel27 As common.Controls.MyLabel
    Friend WithEvents txtbranchname As common.Controls.MyTextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents TxtBankName As common.Controls.MyTextBox
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents txtRegNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtKhasraNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents txtAdd2 As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents txtAdd1 As common.Controls.MyTextBox
    Friend WithEvents txtAdd3 As common.Controls.MyTextBox
    Friend WithEvents txtPAN As common.Controls.MyTextBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtAadharNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
End Class
