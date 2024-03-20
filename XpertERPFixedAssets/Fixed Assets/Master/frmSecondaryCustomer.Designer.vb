Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSecondaryCustomer
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblCurrencyName = New common.Controls.MyLabel()
        Me.lblStateName = New common.Controls.MyLabel()
        Me.lblCityName = New common.Controls.MyLabel()
        Me.chkInActive = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblDistributorName = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtCustomerName = New common.Controls.MyTextBox()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.fndstate = New common.UserControls.txtFinder()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.fndCity = New common.UserControls.txtFinder()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel32 = New common.Controls.MyLabel()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.txtDistributor = New common.UserControls.txtFinder()
        Me.RadLabel11 = New common.Controls.MyLabel()
        Me.txtWeb = New common.Controls.MyTextBox()
        Me.fndCustomer = New common.UserControls.txtNavigator()
        Me.txtfax = New common.Controls.MyTextBox()
        Me.txtEmail = New common.Controls.MyTextBox()
        Me.txtPhone2 = New common.Controls.MyTextBox()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.RadLabel10 = New common.Controls.MyLabel()
        Me.txtCountry = New common.Controls.MyTextBox()
        Me.lblBaseCurrency = New common.Controls.MyLabel()
        Me.txtPhone1 = New common.Controls.MyTextBox()
        Me.RadLabel9 = New common.Controls.MyLabel()
        Me.fndCustCurrency = New common.UserControls.txtFinder()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.txtAdd3 = New common.Controls.MyTextBox()
        Me.txtAdd1 = New common.Controls.MyTextBox()
        Me.txtAdd2 = New common.Controls.MyTextBox()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New XpertERPEngine.ucCustomFields()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblCurrencyName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStateName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCityName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDistributorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWeb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPhone2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBaseCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPhone1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1048, 20)
        Me.RadMenu1.TabIndex = 1
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExport, Me.rmiImport, Me.rmiClose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'rmiExport
        '
        Me.rmiExport.Name = "rmiExport"
        Me.rmiExport.Text = "Export"
        '
        'rmiImport
        '
        Me.rmiImport.Name = "rmiImport"
        Me.rmiImport.Text = "Import"
        '
        'rmiClose
        '
        Me.rmiClose.Name = "rmiClose"
        Me.rmiClose.Text = "Close"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
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
        Me.SplitContainer1.Size = New System.Drawing.Size(1048, 430)
        Me.SplitContainer1.SplitterDistance = 401
        Me.SplitContainer1.TabIndex = 2
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1048, 401)
        Me.RadPageView1.TabIndex = 121
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.lblCurrencyName)
        Me.RadPageViewPage1.Controls.Add(Me.lblStateName)
        Me.RadPageViewPage1.Controls.Add(Me.lblCityName)
        Me.RadPageViewPage1.Controls.Add(Me.chkInActive)
        Me.RadPageViewPage1.Controls.Add(Me.lblDistributorName)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustomerName)
        Me.RadPageViewPage1.Controls.Add(Me.fndstate)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.fndCity)
        Me.RadPageViewPage1.Controls.Add(Me.btnNew)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel32)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.txtDistributor)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel11)
        Me.RadPageViewPage1.Controls.Add(Me.txtWeb)
        Me.RadPageViewPage1.Controls.Add(Me.fndCustomer)
        Me.RadPageViewPage1.Controls.Add(Me.txtfax)
        Me.RadPageViewPage1.Controls.Add(Me.txtEmail)
        Me.RadPageViewPage1.Controls.Add(Me.txtPhone2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.txtCountry)
        Me.RadPageViewPage1.Controls.Add(Me.lblBaseCurrency)
        Me.RadPageViewPage1.Controls.Add(Me.txtPhone1)
        Me.RadPageViewPage1.Controls.Add(Me.fndCustCurrency)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdd3)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdd1)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdd2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(65.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1027, 353)
        Me.RadPageViewPage1.Text = "Customer"
        '
        'lblCurrencyName
        '
        Me.lblCurrencyName.AutoSize = False
        Me.lblCurrencyName.BorderVisible = True
        Me.lblCurrencyName.FieldName = Nothing
        Me.lblCurrencyName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrencyName.Location = New System.Drawing.Point(224, 76)
        Me.lblCurrencyName.Name = "lblCurrencyName"
        Me.lblCurrencyName.Size = New System.Drawing.Size(227, 18)
        Me.lblCurrencyName.TabIndex = 610
        '
        'lblStateName
        '
        Me.lblStateName.AutoSize = False
        Me.lblStateName.BorderVisible = True
        Me.lblStateName.FieldName = Nothing
        Me.lblStateName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStateName.Location = New System.Drawing.Point(215, 203)
        Me.lblStateName.Name = "lblStateName"
        Me.lblStateName.Size = New System.Drawing.Size(227, 18)
        Me.lblStateName.TabIndex = 609
        '
        'lblCityName
        '
        Me.lblCityName.AutoSize = False
        Me.lblCityName.BorderVisible = True
        Me.lblCityName.FieldName = Nothing
        Me.lblCityName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCityName.Location = New System.Drawing.Point(215, 177)
        Me.lblCityName.Name = "lblCityName"
        Me.lblCityName.Size = New System.Drawing.Size(227, 18)
        Me.lblCityName.TabIndex = 608
        '
        'chkInActive
        '
        Me.chkInActive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInActive.Location = New System.Drawing.Point(626, 29)
        Me.chkInActive.Name = "chkInActive"
        Me.chkInActive.Size = New System.Drawing.Size(64, 16)
        Me.chkInActive.TabIndex = 3
        Me.chkInActive.Text = "In-Active"
        '
        'lblDistributorName
        '
        Me.lblDistributorName.AutoSize = False
        Me.lblDistributorName.BorderVisible = True
        Me.lblDistributorName.FieldName = Nothing
        Me.lblDistributorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDistributorName.Location = New System.Drawing.Point(224, 29)
        Me.lblDistributorName.Name = "lblDistributorName"
        Me.lblDistributorName.Size = New System.Drawing.Size(364, 18)
        Me.lblDistributorName.TabIndex = 607
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(3, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel1.TabIndex = 84
        Me.RadLabel1.Text = "Code"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.CalculationExpression = Nothing
        Me.txtCustomerName.FieldCode = Nothing
        Me.txtCustomerName.FieldDesc = Nothing
        Me.txtCustomerName.FieldMaxLength = 0
        Me.txtCustomerName.FieldName = Nothing
        Me.txtCustomerName.isCalculatedField = False
        Me.txtCustomerName.IsSourceFromTable = False
        Me.txtCustomerName.IsSourceFromValueList = False
        Me.txtCustomerName.IsUnique = False
        Me.txtCustomerName.Location = New System.Drawing.Point(92, 52)
        Me.txtCustomerName.MaxLength = 50
        Me.txtCustomerName.MendatroryField = False
        Me.txtCustomerName.MyLinkLable1 = Me.RadLabel4
        Me.txtCustomerName.MyLinkLable2 = Nothing
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.ReferenceFieldDesc = Nothing
        Me.txtCustomerName.ReferenceFieldName = Nothing
        Me.txtCustomerName.ReferenceTableName = Nothing
        Me.txtCustomerName.Size = New System.Drawing.Size(598, 20)
        Me.txtCustomerName.TabIndex = 4
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(3, 52)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(36, 16)
        Me.RadLabel4.TabIndex = 86
        Me.RadLabel4.Text = "Name"
        '
        'fndstate
        '
        Me.fndstate.CalculationExpression = Nothing
        Me.fndstate.FieldCode = Nothing
        Me.fndstate.FieldDesc = Nothing
        Me.fndstate.FieldMaxLength = 0
        Me.fndstate.FieldName = Nothing
        Me.fndstate.isCalculatedField = False
        Me.fndstate.IsSourceFromTable = False
        Me.fndstate.IsSourceFromValueList = False
        Me.fndstate.IsUnique = False
        Me.fndstate.Location = New System.Drawing.Point(92, 202)
        Me.fndstate.MendatroryField = False
        Me.fndstate.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndstate.MyLinkLable1 = Me.RadLabel6
        Me.fndstate.MyLinkLable2 = Nothing
        Me.fndstate.MyReadOnly = False
        Me.fndstate.MyShowMasterFormButton = False
        Me.fndstate.Name = "fndstate"
        Me.fndstate.ReferenceFieldDesc = Nothing
        Me.fndstate.ReferenceFieldName = Nothing
        Me.fndstate.ReferenceTableName = Nothing
        Me.fndstate.Size = New System.Drawing.Size(117, 19)
        Me.fndstate.TabIndex = 10
        Me.fndstate.Value = ""
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(3, 203)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel6.TabIndex = 114
        Me.RadLabel6.Text = "State"
        '
        'fndCity
        '
        Me.fndCity.CalculationExpression = Nothing
        Me.fndCity.FieldCode = Nothing
        Me.fndCity.FieldDesc = Nothing
        Me.fndCity.FieldMaxLength = 0
        Me.fndCity.FieldName = Nothing
        Me.fndCity.isCalculatedField = False
        Me.fndCity.IsSourceFromTable = False
        Me.fndCity.IsSourceFromValueList = False
        Me.fndCity.IsUnique = False
        Me.fndCity.Location = New System.Drawing.Point(92, 176)
        Me.fndCity.MendatroryField = False
        Me.fndCity.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCity.MyLinkLable1 = Me.RadLabel5
        Me.fndCity.MyLinkLable2 = Nothing
        Me.fndCity.MyReadOnly = False
        Me.fndCity.MyShowMasterFormButton = False
        Me.fndCity.Name = "fndCity"
        Me.fndCity.ReferenceFieldDesc = Nothing
        Me.fndCity.ReferenceFieldName = Nothing
        Me.fndCity.ReferenceTableName = Nothing
        Me.fndCity.Size = New System.Drawing.Size(117, 19)
        Me.fndCity.TabIndex = 9
        Me.fndCity.Value = ""
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(3, 181)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(26, 16)
        Me.RadLabel5.TabIndex = 112
        Me.RadLabel5.Text = "City"
        '
        'btnNew
        '
        Me.btnNew.Image = Global.XpertERPFixedAssets.My.Resources.Resources._new
        Me.btnNew.Location = New System.Drawing.Point(312, 4)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(15, 20)
        Me.btnNew.TabIndex = 0
        '
        'RadLabel32
        '
        Me.RadLabel32.FieldName = Nothing
        Me.RadLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel32.Location = New System.Drawing.Point(3, 29)
        Me.RadLabel32.Name = "RadLabel32"
        Me.RadLabel32.Size = New System.Drawing.Size(58, 16)
        Me.RadLabel32.TabIndex = 87
        Me.RadLabel32.Text = "Distributor"
        '
        'RadLabel12
        '
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.Location = New System.Drawing.Point(3, 310)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel12.TabIndex = 118
        Me.RadLabel12.Text = "WebSite"
        '
        'txtDistributor
        '
        Me.txtDistributor.AccessibleName = "txtParentCstNo"
        Me.txtDistributor.CalculationExpression = Nothing
        Me.txtDistributor.FieldCode = Nothing
        Me.txtDistributor.FieldDesc = Nothing
        Me.txtDistributor.FieldMaxLength = 0
        Me.txtDistributor.FieldName = Nothing
        Me.txtDistributor.isCalculatedField = False
        Me.txtDistributor.IsSourceFromTable = False
        Me.txtDistributor.IsSourceFromValueList = False
        Me.txtDistributor.IsUnique = False
        Me.txtDistributor.Location = New System.Drawing.Point(91, 28)
        Me.txtDistributor.MendatroryField = False
        Me.txtDistributor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDistributor.MyLinkLable1 = Me.RadLabel32
        Me.txtDistributor.MyLinkLable2 = Nothing
        Me.txtDistributor.MyReadOnly = False
        Me.txtDistributor.MyShowMasterFormButton = False
        Me.txtDistributor.Name = "txtDistributor"
        Me.txtDistributor.ReferenceFieldDesc = Nothing
        Me.txtDistributor.ReferenceFieldName = Nothing
        Me.txtDistributor.ReferenceTableName = Nothing
        Me.txtDistributor.Size = New System.Drawing.Size(127, 20)
        Me.txtDistributor.TabIndex = 2
        Me.txtDistributor.Value = ""
        '
        'RadLabel11
        '
        Me.RadLabel11.FieldName = Nothing
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(3, 284)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(38, 16)
        Me.RadLabel11.TabIndex = 117
        Me.RadLabel11.Text = "E-Mail"
        '
        'txtWeb
        '
        Me.txtWeb.CalculationExpression = Nothing
        Me.txtWeb.FieldCode = Nothing
        Me.txtWeb.FieldDesc = Nothing
        Me.txtWeb.FieldMaxLength = 0
        Me.txtWeb.FieldName = Nothing
        Me.txtWeb.isCalculatedField = False
        Me.txtWeb.IsSourceFromTable = False
        Me.txtWeb.IsSourceFromValueList = False
        Me.txtWeb.IsUnique = False
        Me.txtWeb.Location = New System.Drawing.Point(91, 306)
        Me.txtWeb.MaxLength = 50
        Me.txtWeb.MendatroryField = False
        Me.txtWeb.MyLinkLable1 = Me.RadLabel12
        Me.txtWeb.MyLinkLable2 = Nothing
        Me.txtWeb.Name = "txtWeb"
        Me.txtWeb.ReferenceFieldDesc = Nothing
        Me.txtWeb.ReferenceFieldName = Nothing
        Me.txtWeb.ReferenceTableName = Nothing
        Me.txtWeb.Size = New System.Drawing.Size(599, 20)
        Me.txtWeb.TabIndex = 16
        '
        'fndCustomer
        '
        Me.fndCustomer.FieldName = Nothing
        Me.fndCustomer.Location = New System.Drawing.Point(92, 3)
        Me.fndCustomer.MendatroryField = True
        Me.fndCustomer.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndCustomer.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCustomer.MyLinkLable1 = Nothing
        Me.fndCustomer.MyLinkLable2 = Nothing
        Me.fndCustomer.MyMaxLength = 30
        Me.fndCustomer.MyReadOnly = False
        Me.fndCustomer.Name = "fndCustomer"
        Me.fndCustomer.Size = New System.Drawing.Size(218, 21)
        Me.fndCustomer.TabIndex = 1
        Me.fndCustomer.Value = ""
        '
        'txtfax
        '
        Me.txtfax.CalculationExpression = Nothing
        Me.txtfax.FieldCode = Nothing
        Me.txtfax.FieldDesc = Nothing
        Me.txtfax.FieldMaxLength = 0
        Me.txtfax.FieldName = Nothing
        Me.txtfax.isCalculatedField = False
        Me.txtfax.IsSourceFromTable = False
        Me.txtfax.IsSourceFromValueList = False
        Me.txtfax.IsUnique = False
        Me.txtfax.Location = New System.Drawing.Point(415, 228)
        Me.txtfax.MaxLength = 15
        Me.txtfax.MendatroryField = False
        Me.txtfax.MyLinkLable1 = Nothing
        Me.txtfax.MyLinkLable2 = Nothing
        Me.txtfax.Name = "txtfax"
        Me.txtfax.ReferenceFieldDesc = Nothing
        Me.txtfax.ReferenceFieldName = Nothing
        Me.txtfax.ReferenceTableName = Nothing
        Me.txtfax.Size = New System.Drawing.Size(276, 20)
        Me.txtfax.TabIndex = 13
        '
        'txtEmail
        '
        Me.txtEmail.CalculationExpression = Nothing
        Me.txtEmail.FieldCode = Nothing
        Me.txtEmail.FieldDesc = Nothing
        Me.txtEmail.FieldMaxLength = 0
        Me.txtEmail.FieldName = Nothing
        Me.txtEmail.isCalculatedField = False
        Me.txtEmail.IsSourceFromTable = False
        Me.txtEmail.IsSourceFromValueList = False
        Me.txtEmail.IsUnique = False
        Me.txtEmail.Location = New System.Drawing.Point(91, 280)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.MendatroryField = False
        Me.txtEmail.MyLinkLable1 = Me.RadLabel11
        Me.txtEmail.MyLinkLable2 = Nothing
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.ReferenceFieldDesc = Nothing
        Me.txtEmail.ReferenceFieldName = Nothing
        Me.txtEmail.ReferenceTableName = Nothing
        Me.txtEmail.Size = New System.Drawing.Size(599, 20)
        Me.txtEmail.TabIndex = 15
        '
        'txtPhone2
        '
        Me.txtPhone2.CalculationExpression = Nothing
        Me.txtPhone2.FieldCode = Nothing
        Me.txtPhone2.FieldDesc = Nothing
        Me.txtPhone2.FieldMaxLength = 0
        Me.txtPhone2.FieldName = Nothing
        Me.txtPhone2.isCalculatedField = False
        Me.txtPhone2.IsSourceFromTable = False
        Me.txtPhone2.IsSourceFromValueList = False
        Me.txtPhone2.IsUnique = False
        Me.txtPhone2.Location = New System.Drawing.Point(91, 254)
        Me.txtPhone2.MaxLength = 15
        Me.txtPhone2.MendatroryField = False
        Me.txtPhone2.MyLinkLable1 = Me.RadLabel8
        Me.txtPhone2.MyLinkLable2 = Nothing
        Me.txtPhone2.Name = "txtPhone2"
        Me.txtPhone2.ReferenceFieldDesc = Nothing
        Me.txtPhone2.ReferenceFieldName = Nothing
        Me.txtPhone2.ReferenceTableName = Nothing
        Me.txtPhone2.Size = New System.Drawing.Size(226, 20)
        Me.txtPhone2.TabIndex = 14
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(3, 258)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(45, 16)
        Me.RadLabel8.TabIndex = 120
        Me.RadLabel8.Text = "Phone2"
        '
        'RadLabel10
        '
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.Location = New System.Drawing.Point(357, 232)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(25, 16)
        Me.RadLabel10.TabIndex = 119
        Me.RadLabel10.Text = "Fax"
        '
        'txtCountry
        '
        Me.txtCountry.CalculationExpression = Nothing
        Me.txtCountry.FieldCode = Nothing
        Me.txtCountry.FieldDesc = Nothing
        Me.txtCountry.FieldMaxLength = 0
        Me.txtCountry.FieldName = Nothing
        Me.txtCountry.isCalculatedField = False
        Me.txtCountry.IsSourceFromTable = False
        Me.txtCountry.IsSourceFromValueList = False
        Me.txtCountry.IsUnique = False
        Me.txtCountry.Location = New System.Drawing.Point(516, 200)
        Me.txtCountry.MaxLength = 25
        Me.txtCountry.MendatroryField = False
        Me.txtCountry.MyLinkLable1 = Nothing
        Me.txtCountry.MyLinkLable2 = Nothing
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.ReferenceFieldDesc = Nothing
        Me.txtCountry.ReferenceFieldName = Nothing
        Me.txtCountry.ReferenceTableName = Nothing
        Me.txtCountry.Size = New System.Drawing.Size(175, 20)
        Me.txtCountry.TabIndex = 11
        '
        'lblBaseCurrency
        '
        Me.lblBaseCurrency.FieldName = Nothing
        Me.lblBaseCurrency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBaseCurrency.Location = New System.Drawing.Point(3, 76)
        Me.lblBaseCurrency.Name = "lblBaseCurrency"
        Me.lblBaseCurrency.Size = New System.Drawing.Size(52, 16)
        Me.lblBaseCurrency.TabIndex = 93
        Me.lblBaseCurrency.Text = "Currency"
        '
        'txtPhone1
        '
        Me.txtPhone1.CalculationExpression = Nothing
        Me.txtPhone1.FieldCode = Nothing
        Me.txtPhone1.FieldDesc = Nothing
        Me.txtPhone1.FieldMaxLength = 0
        Me.txtPhone1.FieldName = Nothing
        Me.txtPhone1.isCalculatedField = False
        Me.txtPhone1.IsSourceFromTable = False
        Me.txtPhone1.IsSourceFromValueList = False
        Me.txtPhone1.IsUnique = False
        Me.txtPhone1.Location = New System.Drawing.Point(92, 228)
        Me.txtPhone1.MaxLength = 15
        Me.txtPhone1.MendatroryField = False
        Me.txtPhone1.MyLinkLable1 = Me.RadLabel9
        Me.txtPhone1.MyLinkLable2 = Nothing
        Me.txtPhone1.Name = "txtPhone1"
        Me.txtPhone1.ReferenceFieldDesc = Nothing
        Me.txtPhone1.ReferenceFieldName = Nothing
        Me.txtPhone1.ReferenceTableName = Nothing
        Me.txtPhone1.Size = New System.Drawing.Size(225, 20)
        Me.txtPhone1.TabIndex = 12
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(3, 230)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(45, 16)
        Me.RadLabel9.TabIndex = 116
        Me.RadLabel9.Text = "Phone1"
        '
        'fndCustCurrency
        '
        Me.fndCustCurrency.CalculationExpression = Nothing
        Me.fndCustCurrency.FieldCode = Nothing
        Me.fndCustCurrency.FieldDesc = Nothing
        Me.fndCustCurrency.FieldMaxLength = 0
        Me.fndCustCurrency.FieldName = Nothing
        Me.fndCustCurrency.isCalculatedField = False
        Me.fndCustCurrency.IsSourceFromTable = False
        Me.fndCustCurrency.IsSourceFromValueList = False
        Me.fndCustCurrency.IsUnique = False
        Me.fndCustCurrency.Location = New System.Drawing.Point(92, 75)
        Me.fndCustCurrency.MendatroryField = False
        Me.fndCustCurrency.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCustCurrency.MyLinkLable1 = Nothing
        Me.fndCustCurrency.MyLinkLable2 = Nothing
        Me.fndCustCurrency.MyReadOnly = False
        Me.fndCustCurrency.MyShowMasterFormButton = False
        Me.fndCustCurrency.Name = "fndCustCurrency"
        Me.fndCustCurrency.ReferenceFieldDesc = Nothing
        Me.fndCustCurrency.ReferenceFieldName = Nothing
        Me.fndCustCurrency.ReferenceTableName = Nothing
        Me.fndCustCurrency.Size = New System.Drawing.Size(126, 19)
        Me.fndCustCurrency.TabIndex = 5
        Me.fndCustCurrency.Value = ""
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(3, 99)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(48, 16)
        Me.RadLabel2.TabIndex = 111
        Me.RadLabel2.Text = "Address"
        '
        'RadLabel7
        '
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(463, 202)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(46, 16)
        Me.RadLabel7.TabIndex = 115
        Me.RadLabel7.Text = "Country"
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
        Me.txtAdd3.Location = New System.Drawing.Point(92, 151)
        Me.txtAdd3.MaxLength = 75
        Me.txtAdd3.MendatroryField = False
        Me.txtAdd3.MyLinkLable1 = Me.RadLabel2
        Me.txtAdd3.MyLinkLable2 = Nothing
        Me.txtAdd3.Name = "txtAdd3"
        Me.txtAdd3.ReferenceFieldDesc = Nothing
        Me.txtAdd3.ReferenceFieldName = Nothing
        Me.txtAdd3.ReferenceTableName = Nothing
        Me.txtAdd3.Size = New System.Drawing.Size(599, 20)
        Me.txtAdd3.TabIndex = 8
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
        Me.txtAdd1.Location = New System.Drawing.Point(92, 99)
        Me.txtAdd1.MaxLength = 75
        Me.txtAdd1.MendatroryField = False
        Me.txtAdd1.MyLinkLable1 = Me.RadLabel2
        Me.txtAdd1.MyLinkLable2 = Nothing
        Me.txtAdd1.Name = "txtAdd1"
        Me.txtAdd1.ReferenceFieldDesc = Nothing
        Me.txtAdd1.ReferenceFieldName = Nothing
        Me.txtAdd1.ReferenceTableName = Nothing
        Me.txtAdd1.Size = New System.Drawing.Size(599, 20)
        Me.txtAdd1.TabIndex = 6
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
        Me.txtAdd2.Location = New System.Drawing.Point(92, 125)
        Me.txtAdd2.MaxLength = 75
        Me.txtAdd2.MendatroryField = False
        Me.txtAdd2.MyLinkLable1 = Me.RadLabel2
        Me.txtAdd2.MyLinkLable2 = Nothing
        Me.txtAdd2.Name = "txtAdd2"
        Me.txtAdd2.ReferenceFieldDesc = Nothing
        Me.txtAdd2.ReferenceFieldName = Nothing
        Me.txtAdd2.ReferenceTableName = Nothing
        Me.txtAdd2.Size = New System.Drawing.Size(599, 20)
        Me.txtAdd2.TabIndex = 7
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(86.0!, 28.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(1027, 353)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(1027, 353)
        Me.UcCustomFields1.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(965, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(89, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(80, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(6, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'FrmSecondaryCustomer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1048, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmSecondaryCustomer"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Secondary Customer"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblCurrencyName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStateName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCityName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDistributorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWeb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPhone2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBaseCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPhone1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents fndCustCurrency As common.UserControls.txtFinder
    Friend WithEvents lblBaseCurrency As common.Controls.MyLabel
    Friend WithEvents fndCustomer As common.UserControls.txtNavigator
    Friend WithEvents txtDistributor As common.UserControls.txtFinder
    Friend WithEvents RadLabel32 As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtCustomerName As common.Controls.MyTextBox
    Friend WithEvents fndstate As common.UserControls.txtFinder
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents fndCity As common.UserControls.txtFinder
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents txtWeb As common.Controls.MyTextBox
    Friend WithEvents txtfax As common.Controls.MyTextBox
    Friend WithEvents txtEmail As common.Controls.MyTextBox
    Friend WithEvents txtPhone2 As common.Controls.MyTextBox
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents txtCountry As common.Controls.MyTextBox
    Friend WithEvents txtPhone1 As common.Controls.MyTextBox
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents chkInActive As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtAdd2 As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents txtAdd1 As common.Controls.MyTextBox
    Friend WithEvents txtAdd3 As common.Controls.MyTextBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ucCustomFields
    Friend WithEvents lblStateName As common.Controls.MyLabel
    Friend WithEvents lblCityName As common.Controls.MyLabel
    Friend WithEvents lblDistributorName As common.Controls.MyLabel
    Friend WithEvents lblCurrencyName As common.Controls.MyLabel
    Friend WithEvents rmiExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiClose As Telerik.WinControls.UI.RadMenuItem
End Class

