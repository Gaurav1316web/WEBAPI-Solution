Imports XpertERPEngine
Imports XpertERPEngineFine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSheedGrowerMaster
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
        Me.lblDistrict = New common.Controls.MyLabel()
        Me.txtDistrict = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblVillage = New common.Controls.MyLabel()
        Me.fndVillegeCode = New common.UserControls.txtFinder()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtTotal = New common.MyNumBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
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
        CType(Me.lblDistrict, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVillage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
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
        Me.SplitContainer1.Size = New System.Drawing.Size(906, 445)
        Me.SplitContainer1.SplitterDistance = 392
        Me.SplitContainer1.TabIndex = 0
        '
        'lblDistrict
        '
        Me.lblDistrict.AutoSize = False
        Me.lblDistrict.BorderVisible = True
        Me.lblDistrict.FieldName = Nothing
        Me.lblDistrict.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDistrict.Location = New System.Drawing.Point(352, 129)
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
        Me.txtDistrict.Location = New System.Drawing.Point(123, 129)
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
        Me.MyLabel1.Location = New System.Drawing.Point(17, 106)
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
        Me.lblVillage.Location = New System.Drawing.Point(352, 106)
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
        Me.fndVillegeCode.Location = New System.Drawing.Point(123, 106)
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
        Me.MyLabel7.Location = New System.Drawing.Point(232, 261)
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
        Me.txtTotal.Location = New System.Drawing.Point(123, 260)
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
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(17, 261)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(31, 18)
        Me.MyLabel8.TabIndex = 1467
        Me.MyLabel8.Text = "Total"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(232, 239)
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
        Me.txtLeaseLand.Location = New System.Drawing.Point(123, 238)
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
        Me.MyLabel6.Location = New System.Drawing.Point(17, 239)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(61, 18)
        Me.MyLabel6.TabIndex = 1461
        Me.MyLabel6.Text = "Lease Land"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(232, 217)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel3.TabIndex = 1460
        Me.MyLabel3.Text = "Acre"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(232, 195)
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
        Me.txtOwnLand.Location = New System.Drawing.Point(123, 194)
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
        Me.txtFamilyLand.Location = New System.Drawing.Point(123, 216)
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
        Me.MyLabel23.Location = New System.Drawing.Point(17, 195)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(57, 18)
        Me.MyLabel23.TabIndex = 1455
        Me.MyLabel23.Text = "Own Land"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(17, 217)
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
        Me.txtMobile.Location = New System.Drawing.Point(123, 172)
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
        Me.lblTelephone.Location = New System.Drawing.Point(17, 176)
        Me.lblTelephone.Name = "lblTelephone"
        Me.lblTelephone.Size = New System.Drawing.Size(60, 16)
        Me.lblTelephone.TabIndex = 408
        Me.lblTelephone.Text = "Mobile No."
        '
        'MyLabel38
        '
        Me.MyLabel38.FieldName = Nothing
        Me.MyLabel38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel38.Location = New System.Drawing.Point(17, 129)
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
        Me.txtTehsil.Location = New System.Drawing.Point(123, 151)
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
        Me.lblTehsil.Location = New System.Drawing.Point(17, 155)
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
        Me.RadMenu1.Size = New System.Drawing.Size(906, 20)
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
        Me.btnsave.Location = New System.Drawing.Point(12, 27)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 6
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(85, 27)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 7
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(817, 27)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 8
        Me.btnclose.Text = "Close"
        '
        'frmSheedGrowerMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(906, 445)
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
        CType(Me.lblDistrict, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVillage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
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
End Class
