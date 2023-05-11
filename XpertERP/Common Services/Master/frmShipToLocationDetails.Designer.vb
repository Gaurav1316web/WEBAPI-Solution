<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmShipToLocationDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmShipToLocationDetails))
        Dim GridViewTextBoxColumn16 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn17 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn18 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn19 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn20 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn21 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn22 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn23 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn24 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn25 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn26 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn27 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn28 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn29 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn30 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.grpShipToLocation = New Telerik.WinControls.UI.RadGroupBox
        Me.lblLocationDesc = New common.Controls.MyLabel
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.fndLocation = New common.UserControls.txtFinder
        Me.txtShipToLocation = New common.Controls.MyTextBox
        Me.fndCustomer = New common.Controls.MyTextBox
        Me.grpAddress = New Telerik.WinControls.UI.RadGroupBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.txtCSTNo = New common.Controls.MyTextBox
        Me.txtTinNo = New common.Controls.MyTextBox
        Me.lblTelephone = New common.Controls.MyLabel
        Me.lblCountry = New common.Controls.MyLabel
        Me.txtEmail = New common.Controls.MyTextBox
        Me.Email = New common.Controls.MyLabel
        Me.txtAddress22 = New common.Controls.MyTextBox
        Me.lblAddress = New common.Controls.MyLabel
        Me.txtPostalCode = New common.Controls.MyTextBox
        Me.lblZipPostalCode = New common.Controls.MyLabel
        Me.txtState = New common.Controls.MyTextBox
        Me.lblState = New common.Controls.MyLabel
        Me.txtTelephone = New common.Controls.MyTextBox
        Me.txtCountry = New common.Controls.MyTextBox
        Me.txtCity = New common.Controls.MyTextBox
        Me.lblCity = New common.Controls.MyLabel
        Me.txtAddress4 = New common.Controls.MyTextBox
        Me.txtAddress3 = New common.Controls.MyTextBox
        Me.txtAddress1 = New common.Controls.MyTextBox
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.txtShipToLocationDesc = New common.Controls.MyTextBox
        Me.lblShipToLocation = New common.Controls.MyLabel
        Me.txtCustomerName = New common.Controls.MyTextBox
        Me.lblCustomer = New common.Controls.MyLabel
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnAdd = New Telerik.WinControls.UI.RadButton
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.menuImport = New Telerik.WinControls.UI.RadMenuItem
        Me.menuImport1 = New Telerik.WinControls.UI.RadMenuItem
        Me.menuExport = New Telerik.WinControls.UI.RadMenuItem
        Me.menuClose = New Telerik.WinControls.UI.RadMenuItem
        Me.menuPrint = New Telerik.WinControls.UI.RadMenuItem
        Me.txtAddress2 = New common.Controls.MyTextBox
        Me.RadTextBox1 = New common.Controls.MyTextBox
        Me.RadTextBox2 = New common.Controls.MyTextBox
        Me.RadTextBox3 = New common.Controls.MyTextBox
        Me.RadTextBox4 = New common.Controls.MyTextBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.lblShipToType = New common.Controls.MyLabel
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtcustomer = New common.UserControls.txtNavigator
        Me.txtcustomerdesc = New common.Controls.MyTextBox
        Me.MasterTemplate = New common.UserControls.MyRadGridView
        Me.ddlShipToType = New common.Controls.MyComboBox
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.Details = New Telerik.WinControls.UI.RadPageViewPage
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage
        Me.UcCustomFields1 = New ERP.ucCustomFields
        CType(Me.grpShipToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpShipToLocation.SuspendLayout()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtShipToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fndCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpAddress.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCSTNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTinNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTelephone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Email, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAddress22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPostalCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblZipPostalCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTelephone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAddress4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAddress3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAddress1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtShipToLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShipToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAddress2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblShipToType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcustomerdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterTemplate.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlShipToType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.Details.SuspendLayout()
        Me.pvpCustomFields.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpShipToLocation
        '
        Me.grpShipToLocation.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpShipToLocation.Controls.Add(Me.lblLocationDesc)
        Me.grpShipToLocation.Controls.Add(Me.MyLabel4)
        Me.grpShipToLocation.Controls.Add(Me.fndLocation)
        Me.grpShipToLocation.Controls.Add(Me.txtShipToLocation)
        Me.grpShipToLocation.Controls.Add(Me.fndCustomer)
        Me.grpShipToLocation.Controls.Add(Me.grpAddress)
        Me.grpShipToLocation.Controls.Add(Me.btnNew)
        Me.grpShipToLocation.Controls.Add(Me.txtShipToLocationDesc)
        Me.grpShipToLocation.Controls.Add(Me.lblShipToLocation)
        Me.grpShipToLocation.Controls.Add(Me.txtCustomerName)
        Me.grpShipToLocation.Controls.Add(Me.lblCustomer)
        Me.grpShipToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpShipToLocation.HeaderText = ""
        Me.grpShipToLocation.Location = New System.Drawing.Point(1, 0)
        Me.grpShipToLocation.Name = "grpShipToLocation"
        Me.grpShipToLocation.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpShipToLocation.Size = New System.Drawing.Size(708, 270)
        Me.grpShipToLocation.TabIndex = 0
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.Location = New System.Drawing.Point(368, 54)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(329, 19)
        Me.lblLocationDesc.TabIndex = 6
        Me.lblLocationDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(14, 54)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel4.TabIndex = 4
        Me.MyLabel4.Text = "Location"
        '
        'fndLocation
        '
        Me.fndLocation.Location = New System.Drawing.Point(145, 54)
        Me.fndLocation.MendatroryField = False
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Nothing
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.Size = New System.Drawing.Size(202, 18)
        Me.fndLocation.TabIndex = 5
        Me.fndLocation.Value = ""
        '
        'txtShipToLocation
        '
        Me.txtShipToLocation.Location = New System.Drawing.Point(145, 29)
        Me.txtShipToLocation.MendatroryField = False
        Me.txtShipToLocation.MyLinkLable1 = Nothing
        Me.txtShipToLocation.MyLinkLable2 = Nothing
        Me.txtShipToLocation.Name = "txtShipToLocation"
        Me.txtShipToLocation.Size = New System.Drawing.Size(187, 20)
        Me.txtShipToLocation.TabIndex = 2
        '
        'fndCustomer
        '
        Me.fndCustomer.Location = New System.Drawing.Point(145, 6)
        Me.fndCustomer.MendatroryField = False
        Me.fndCustomer.MyLinkLable1 = Nothing
        Me.fndCustomer.MyLinkLable2 = Nothing
        Me.fndCustomer.Name = "fndCustomer"
        Me.fndCustomer.Size = New System.Drawing.Size(202, 20)
        Me.fndCustomer.TabIndex = 0
        Me.fndCustomer.TabStop = False
        Me.fndCustomer.Text = "MyTextBox1"
        '
        'grpAddress
        '
        Me.grpAddress.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpAddress.Controls.Add(Me.MyLabel2)
        Me.grpAddress.Controls.Add(Me.MyLabel3)
        Me.grpAddress.Controls.Add(Me.txtCSTNo)
        Me.grpAddress.Controls.Add(Me.txtTinNo)
        Me.grpAddress.Controls.Add(Me.lblTelephone)
        Me.grpAddress.Controls.Add(Me.lblCountry)
        Me.grpAddress.Controls.Add(Me.txtEmail)
        Me.grpAddress.Controls.Add(Me.txtAddress22)
        Me.grpAddress.Controls.Add(Me.txtPostalCode)
        Me.grpAddress.Controls.Add(Me.txtState)
        Me.grpAddress.Controls.Add(Me.txtTelephone)
        Me.grpAddress.Controls.Add(Me.txtCountry)
        Me.grpAddress.Controls.Add(Me.txtCity)
        Me.grpAddress.Controls.Add(Me.txtAddress4)
        Me.grpAddress.Controls.Add(Me.txtAddress3)
        Me.grpAddress.Controls.Add(Me.txtAddress1)
        Me.grpAddress.Controls.Add(Me.Email)
        Me.grpAddress.Controls.Add(Me.lblZipPostalCode)
        Me.grpAddress.Controls.Add(Me.lblState)
        Me.grpAddress.Controls.Add(Me.lblCity)
        Me.grpAddress.Controls.Add(Me.lblAddress)
        Me.grpAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpAddress.HeaderText = ""
        Me.grpAddress.Location = New System.Drawing.Point(11, 76)
        Me.grpAddress.Name = "grpAddress"
        Me.grpAddress.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpAddress.Size = New System.Drawing.Size(689, 183)
        Me.grpAddress.TabIndex = 7
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(373, 162)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel2.TabIndex = 13
        Me.MyLabel2.Text = "CST No"
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(373, 139)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(40, 16)
        Me.MyLabel3.TabIndex = 15
        Me.MyLabel3.Text = "Tin No"
        '
        'txtCSTNo
        '
        Me.txtCSTNo.AutoSize = False
        Me.txtCSTNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCSTNo.Location = New System.Drawing.Point(440, 161)
        Me.txtCSTNo.MaxLength = 20
        Me.txtCSTNo.MendatroryField = False
        Me.txtCSTNo.Multiline = True
        Me.txtCSTNo.MyLinkLable1 = Me.MyLabel2
        Me.txtCSTNo.MyLinkLable2 = Nothing
        Me.txtCSTNo.Name = "txtCSTNo"
        Me.txtCSTNo.Size = New System.Drawing.Size(245, 20)
        Me.txtCSTNo.TabIndex = 11
        Me.txtCSTNo.Text = " "
        '
        'txtTinNo
        '
        Me.txtTinNo.AutoSize = False
        Me.txtTinNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTinNo.Location = New System.Drawing.Point(440, 139)
        Me.txtTinNo.MaxLength = 20
        Me.txtTinNo.MendatroryField = False
        Me.txtTinNo.Multiline = True
        Me.txtTinNo.MyLinkLable1 = Me.MyLabel3
        Me.txtTinNo.MyLinkLable2 = Nothing
        Me.txtTinNo.Name = "txtTinNo"
        Me.txtTinNo.Size = New System.Drawing.Size(246, 20)
        Me.txtTinNo.TabIndex = 9
        Me.txtTinNo.Text = " "
        '
        'lblTelephone
        '
        Me.lblTelephone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTelephone.Location = New System.Drawing.Point(373, 117)
        Me.lblTelephone.Name = "lblTelephone"
        Me.lblTelephone.Size = New System.Drawing.Size(60, 16)
        Me.lblTelephone.TabIndex = 17
        Me.lblTelephone.Text = "Telephone"
        '
        'lblCountry
        '
        Me.lblCountry.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCountry.Location = New System.Drawing.Point(373, 95)
        Me.lblCountry.Name = "lblCountry"
        Me.lblCountry.Size = New System.Drawing.Size(46, 16)
        Me.lblCountry.TabIndex = 19
        Me.lblCountry.Text = "Country"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(134, 161)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.MendatroryField = False
        Me.txtEmail.MyLinkLable1 = Me.Email
        Me.txtEmail.MyLinkLable2 = Nothing
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(226, 20)
        Me.txtEmail.TabIndex = 10
        '
        'Email
        '
        Me.Email.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Email.Location = New System.Drawing.Point(13, 162)
        Me.Email.Name = "Email"
        Me.Email.Size = New System.Drawing.Size(34, 16)
        Me.Email.TabIndex = 12
        Me.Email.Text = "Email"
        '
        'txtAddress22
        '
        Me.txtAddress22.AutoSize = False
        Me.txtAddress22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress22.Location = New System.Drawing.Point(134, 29)
        Me.txtAddress22.MaxLength = 50
        Me.txtAddress22.MendatroryField = False
        Me.txtAddress22.Multiline = True
        Me.txtAddress22.MyLinkLable1 = Me.lblAddress
        Me.txtAddress22.MyLinkLable2 = Nothing
        Me.txtAddress22.Name = "txtAddress22"
        Me.txtAddress22.Size = New System.Drawing.Size(552, 20)
        Me.txtAddress22.TabIndex = 1
        Me.txtAddress22.Text = " "
        '
        'lblAddress
        '
        Me.lblAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress.Location = New System.Drawing.Point(13, 6)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(48, 16)
        Me.lblAddress.TabIndex = 20
        Me.lblAddress.Text = "Address"
        '
        'txtPostalCode
        '
        Me.txtPostalCode.AutoSize = False
        Me.txtPostalCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPostalCode.Location = New System.Drawing.Point(134, 139)
        Me.txtPostalCode.MaxLength = 9
        Me.txtPostalCode.MendatroryField = False
        Me.txtPostalCode.Multiline = True
        Me.txtPostalCode.MyLinkLable1 = Me.lblZipPostalCode
        Me.txtPostalCode.MyLinkLable2 = Nothing
        Me.txtPostalCode.Name = "txtPostalCode"
        Me.txtPostalCode.Size = New System.Drawing.Size(226, 20)
        Me.txtPostalCode.TabIndex = 8
        Me.txtPostalCode.Text = " "
        '
        'lblZipPostalCode
        '
        Me.lblZipPostalCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZipPostalCode.Location = New System.Drawing.Point(13, 139)
        Me.lblZipPostalCode.Name = "lblZipPostalCode"
        Me.lblZipPostalCode.Size = New System.Drawing.Size(87, 16)
        Me.lblZipPostalCode.TabIndex = 14
        Me.lblZipPostalCode.Text = "Zip Postal Code"
        '
        'txtState
        '
        Me.txtState.AutoSize = False
        Me.txtState.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtState.Location = New System.Drawing.Point(134, 117)
        Me.txtState.MaxLength = 50
        Me.txtState.MendatroryField = False
        Me.txtState.Multiline = True
        Me.txtState.MyLinkLable1 = Me.lblState
        Me.txtState.MyLinkLable2 = Nothing
        Me.txtState.Name = "txtState"
        Me.txtState.Size = New System.Drawing.Size(226, 20)
        Me.txtState.TabIndex = 6
        Me.txtState.Text = " "
        '
        'lblState
        '
        Me.lblState.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblState.Location = New System.Drawing.Point(13, 117)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(80, 16)
        Me.lblState.TabIndex = 16
        Me.lblState.Text = "State/Province"
        '
        'txtTelephone
        '
        Me.txtTelephone.AutoSize = False
        Me.txtTelephone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTelephone.Location = New System.Drawing.Point(440, 117)
        Me.txtTelephone.MaxLength = 50
        Me.txtTelephone.MendatroryField = False
        Me.txtTelephone.Multiline = True
        Me.txtTelephone.MyLinkLable1 = Me.lblTelephone
        Me.txtTelephone.MyLinkLable2 = Nothing
        Me.txtTelephone.Name = "txtTelephone"
        Me.txtTelephone.Size = New System.Drawing.Size(245, 20)
        Me.txtTelephone.TabIndex = 7
        Me.txtTelephone.Text = " "
        '
        'txtCountry
        '
        Me.txtCountry.AutoSize = False
        Me.txtCountry.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCountry.Location = New System.Drawing.Point(440, 95)
        Me.txtCountry.MaxLength = 50
        Me.txtCountry.MendatroryField = False
        Me.txtCountry.Multiline = True
        Me.txtCountry.MyLinkLable1 = Me.lblCountry
        Me.txtCountry.MyLinkLable2 = Nothing
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.Size = New System.Drawing.Size(246, 20)
        Me.txtCountry.TabIndex = 5
        Me.txtCountry.Text = " "
        '
        'txtCity
        '
        Me.txtCity.AutoSize = False
        Me.txtCity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCity.Location = New System.Drawing.Point(134, 95)
        Me.txtCity.MaxLength = 12
        Me.txtCity.MendatroryField = False
        Me.txtCity.Multiline = True
        Me.txtCity.MyLinkLable1 = Me.lblCity
        Me.txtCity.MyLinkLable2 = Nothing
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(226, 20)
        Me.txtCity.TabIndex = 4
        Me.txtCity.Text = " "
        '
        'lblCity
        '
        Me.lblCity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCity.Location = New System.Drawing.Point(13, 99)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(26, 16)
        Me.lblCity.TabIndex = 18
        Me.lblCity.Text = "City"
        '
        'txtAddress4
        '
        Me.txtAddress4.AutoSize = False
        Me.txtAddress4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress4.Location = New System.Drawing.Point(134, 73)
        Me.txtAddress4.MaxLength = 50
        Me.txtAddress4.MendatroryField = False
        Me.txtAddress4.Multiline = True
        Me.txtAddress4.MyLinkLable1 = Me.lblAddress
        Me.txtAddress4.MyLinkLable2 = Nothing
        Me.txtAddress4.Name = "txtAddress4"
        Me.txtAddress4.Size = New System.Drawing.Size(552, 20)
        Me.txtAddress4.TabIndex = 3
        Me.txtAddress4.Text = " "
        '
        'txtAddress3
        '
        Me.txtAddress3.AutoSize = False
        Me.txtAddress3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress3.Location = New System.Drawing.Point(134, 51)
        Me.txtAddress3.MaxLength = 50
        Me.txtAddress3.MendatroryField = False
        Me.txtAddress3.Multiline = True
        Me.txtAddress3.MyLinkLable1 = Me.lblAddress
        Me.txtAddress3.MyLinkLable2 = Nothing
        Me.txtAddress3.Name = "txtAddress3"
        Me.txtAddress3.Size = New System.Drawing.Size(552, 20)
        Me.txtAddress3.TabIndex = 2
        Me.txtAddress3.Text = " "
        '
        'txtAddress1
        '
        Me.txtAddress1.AutoSize = False
        Me.txtAddress1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress1.Location = New System.Drawing.Point(134, 5)
        Me.txtAddress1.MaxLength = 50
        Me.txtAddress1.MendatroryField = False
        Me.txtAddress1.Multiline = True
        Me.txtAddress1.MyLinkLable1 = Me.lblAddress
        Me.txtAddress1.MyLinkLable2 = Nothing
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.Size = New System.Drawing.Size(552, 20)
        Me.txtAddress1.TabIndex = 0
        Me.txtAddress1.Text = " "
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(332, 30)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 3
        Me.btnNew.Text = " "
        '
        'txtShipToLocationDesc
        '
        Me.txtShipToLocationDesc.AutoSize = False
        Me.txtShipToLocationDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipToLocationDesc.Location = New System.Drawing.Point(368, 30)
        Me.txtShipToLocationDesc.MaxLength = 50
        Me.txtShipToLocationDesc.MendatroryField = False
        Me.txtShipToLocationDesc.Multiline = True
        Me.txtShipToLocationDesc.MyLinkLable1 = Me.lblShipToLocation
        Me.txtShipToLocationDesc.MyLinkLable2 = Nothing
        Me.txtShipToLocationDesc.Name = "txtShipToLocationDesc"
        Me.txtShipToLocationDesc.Size = New System.Drawing.Size(329, 21)
        Me.txtShipToLocationDesc.TabIndex = 4
        Me.txtShipToLocationDesc.Text = " "
        '
        'lblShipToLocation
        '
        Me.lblShipToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShipToLocation.Location = New System.Drawing.Point(11, 30)
        Me.lblShipToLocation.Name = "lblShipToLocation"
        Me.lblShipToLocation.Size = New System.Drawing.Size(91, 16)
        Me.lblShipToLocation.TabIndex = 6
        Me.lblShipToLocation.Text = "Ship To Location"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.AutoSize = False
        Me.txtCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.Location = New System.Drawing.Point(368, 7)
        Me.txtCustomerName.MaxLength = 50
        Me.txtCustomerName.MendatroryField = False
        Me.txtCustomerName.Multiline = True
        Me.txtCustomerName.MyLinkLable1 = Me.lblCustomer
        Me.txtCustomerName.MyLinkLable2 = Nothing
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.ReadOnly = True
        Me.txtCustomerName.Size = New System.Drawing.Size(329, 21)
        Me.txtCustomerName.TabIndex = 1
        Me.txtCustomerName.TabStop = False
        Me.txtCustomerName.Text = " "
        '
        'lblCustomer
        '
        Me.lblCustomer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(11, 7)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(55, 16)
        Me.lblCustomer.TabIndex = 8
        Me.lblCustomer.Text = "Customer"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(922, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(95, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(25, 3)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(68, 18)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.menuImport})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1016, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'menuImport
        '
        Me.menuImport.AccessibleDescription = "File"
        Me.menuImport.AccessibleName = "File"
        Me.menuImport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.menuImport1, Me.menuExport, Me.menuClose, Me.menuPrint})
        Me.menuImport.Name = "menuImport"
        Me.menuImport.Text = "File"
        Me.menuImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'menuImport1
        '
        Me.menuImport1.AccessibleDescription = "Import.."
        Me.menuImport1.AccessibleName = "Import.."
        Me.menuImport1.Name = "menuImport1"
        Me.menuImport1.Text = "Import.."
        Me.menuImport1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'menuExport
        '
        Me.menuExport.AccessibleDescription = "Export.."
        Me.menuExport.AccessibleName = "Export.."
        Me.menuExport.Name = "menuExport"
        Me.menuExport.Text = "Export.."
        Me.menuExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'menuClose
        '
        Me.menuClose.AccessibleDescription = "Close"
        Me.menuClose.AccessibleName = "Close"
        Me.menuClose.Name = "menuClose"
        Me.menuClose.Text = "Close"
        Me.menuClose.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'menuPrint
        '
        Me.menuPrint.AccessibleDescription = "RadMenuItem1"
        Me.menuPrint.AccessibleName = "RadMenuItem1"
        Me.menuPrint.Name = "menuPrint"
        Me.menuPrint.Text = "Print.."
        Me.menuPrint.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'txtAddress2
        '
        Me.txtAddress2.AutoSize = False
        Me.txtAddress2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress2.Location = New System.Drawing.Point(120, 21)
        Me.txtAddress2.MendatroryField = False
        Me.txtAddress2.Multiline = True
        Me.txtAddress2.MyLinkLable1 = Nothing
        Me.txtAddress2.MyLinkLable2 = Nothing
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.Size = New System.Drawing.Size(557, 20)
        Me.txtAddress2.TabIndex = 15
        Me.txtAddress2.Text = " "
        '
        'RadTextBox1
        '
        Me.RadTextBox1.AutoSize = False
        Me.RadTextBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadTextBox1.Location = New System.Drawing.Point(120, 47)
        Me.RadTextBox1.MendatroryField = False
        Me.RadTextBox1.Multiline = True
        Me.RadTextBox1.MyLinkLable1 = Nothing
        Me.RadTextBox1.MyLinkLable2 = Nothing
        Me.RadTextBox1.Name = "RadTextBox1"
        Me.RadTextBox1.Size = New System.Drawing.Size(557, 20)
        Me.RadTextBox1.TabIndex = 16
        Me.RadTextBox1.Text = " "
        '
        'RadTextBox2
        '
        Me.RadTextBox2.AutoSize = False
        Me.RadTextBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadTextBox2.Location = New System.Drawing.Point(119, 73)
        Me.RadTextBox2.MendatroryField = False
        Me.RadTextBox2.Multiline = True
        Me.RadTextBox2.MyLinkLable1 = Nothing
        Me.RadTextBox2.MyLinkLable2 = Nothing
        Me.RadTextBox2.Name = "RadTextBox2"
        Me.RadTextBox2.Size = New System.Drawing.Size(557, 20)
        Me.RadTextBox2.TabIndex = 20
        Me.RadTextBox2.Text = " "
        '
        'RadTextBox3
        '
        Me.RadTextBox3.AutoSize = False
        Me.RadTextBox3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadTextBox3.Location = New System.Drawing.Point(119, 103)
        Me.RadTextBox3.MendatroryField = False
        Me.RadTextBox3.Multiline = True
        Me.RadTextBox3.MyLinkLable1 = Nothing
        Me.RadTextBox3.MyLinkLable2 = Nothing
        Me.RadTextBox3.Name = "RadTextBox3"
        Me.RadTextBox3.Size = New System.Drawing.Size(557, 20)
        Me.RadTextBox3.TabIndex = 16
        Me.RadTextBox3.Text = " "
        '
        'RadTextBox4
        '
        Me.RadTextBox4.AutoSize = False
        Me.RadTextBox4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadTextBox4.Location = New System.Drawing.Point(66, 113)
        Me.RadTextBox4.MendatroryField = False
        Me.RadTextBox4.Multiline = True
        Me.RadTextBox4.MyLinkLable1 = Nothing
        Me.RadTextBox4.MyLinkLable2 = Nothing
        Me.RadTextBox4.Name = "RadTextBox4"
        Me.RadTextBox4.Size = New System.Drawing.Size(243, 20)
        Me.RadTextBox4.TabIndex = 21
        Me.RadTextBox4.Text = " "
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAdd)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1016, 619)
        Me.SplitContainer1.SplitterDistance = 590
        Me.SplitContainer1.TabIndex = 0
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox5.HeaderText = "Location"
        Me.RadGroupBox5.Location = New System.Drawing.Point(734, 278)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(249, 299)
        Me.RadGroupBox5.TabIndex = 310
        Me.RadGroupBox5.Text = "Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 20)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(229, 269)
        Me.cbgLocation.TabIndex = 2
        Me.cbgLocation.ValueMember = "Code"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.lblShipToType)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtcustomer)
        Me.RadGroupBox1.Controls.Add(Me.txtcustomerdesc)
        Me.RadGroupBox1.Controls.Add(Me.MasterTemplate)
        Me.RadGroupBox1.Controls.Add(Me.ddlShipToType)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Ship To Location"
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 6)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(986, 256)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "Ship To Location"
        '
        'lblShipToType
        '
        Me.lblShipToType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblShipToType.Location = New System.Drawing.Point(14, 20)
        Me.lblShipToType.Name = "lblShipToType"
        Me.lblShipToType.Size = New System.Drawing.Size(60, 16)
        Me.lblShipToType.TabIndex = 3
        Me.lblShipToType.Text = "Ship Type"
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(14, 42)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel1.TabIndex = 2
        Me.MyLabel1.Text = "Customer"
        '
        'txtcustomer
        '
        Me.txtcustomer.Location = New System.Drawing.Point(99, 41)
        Me.txtcustomer.MendatroryField = True
        Me.txtcustomer.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcustomer.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcustomer.MyLinkLable1 = Me.MyLabel1
        Me.txtcustomer.MyLinkLable2 = Nothing
        Me.txtcustomer.MyMaxLength = 32767
        Me.txtcustomer.MyReadOnly = True
        Me.txtcustomer.Name = "txtcustomer"
        Me.txtcustomer.Size = New System.Drawing.Size(202, 21)
        Me.txtcustomer.TabIndex = 1
        Me.txtcustomer.TabStop = False
        Me.txtcustomer.Value = ""
        '
        'txtcustomerdesc
        '
        Me.txtcustomerdesc.AutoSize = False
        Me.txtcustomerdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustomerdesc.Location = New System.Drawing.Point(311, 42)
        Me.txtcustomerdesc.MaxLength = 50
        Me.txtcustomerdesc.MendatroryField = False
        Me.txtcustomerdesc.Multiline = True
        Me.txtcustomerdesc.MyLinkLable1 = Me.MyLabel1
        Me.txtcustomerdesc.MyLinkLable2 = Nothing
        Me.txtcustomerdesc.Name = "txtcustomerdesc"
        Me.txtcustomerdesc.ReadOnly = True
        Me.txtcustomerdesc.Size = New System.Drawing.Size(320, 21)
        Me.txtcustomerdesc.TabIndex = 2
        Me.txtcustomerdesc.TabStop = False
        Me.txtcustomerdesc.Text = " "
        '
        'MasterTemplate
        '
        Me.MasterTemplate.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MasterTemplate.Cursor = System.Windows.Forms.Cursors.Default
        Me.MasterTemplate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MasterTemplate.ForeColor = System.Drawing.Color.Black
        Me.MasterTemplate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.MasterTemplate.Location = New System.Drawing.Point(6, 68)
        '
        'MasterTemplate
        '
        Me.MasterTemplate.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GridViewTextBoxColumn16.EnableExpressionEditor = False
        GridViewTextBoxColumn16.HeaderText = "Ship-To  Location"
        GridViewTextBoxColumn16.Name = "column1"
        GridViewTextBoxColumn16.ReadOnly = True
        GridViewTextBoxColumn16.Width = 80
        GridViewTextBoxColumn17.EnableExpressionEditor = False
        GridViewTextBoxColumn17.HeaderText = "Description"
        GridViewTextBoxColumn17.Name = "column2"
        GridViewTextBoxColumn17.ReadOnly = True
        GridViewTextBoxColumn17.Width = 80
        GridViewTextBoxColumn18.EnableExpressionEditor = False
        GridViewTextBoxColumn18.HeaderText = "Address1"
        GridViewTextBoxColumn18.Name = "column4"
        GridViewTextBoxColumn18.ReadOnly = True
        GridViewTextBoxColumn18.Width = 80
        GridViewTextBoxColumn19.EnableExpressionEditor = False
        GridViewTextBoxColumn19.HeaderText = "Address2"
        GridViewTextBoxColumn19.Name = "column5"
        GridViewTextBoxColumn19.ReadOnly = True
        GridViewTextBoxColumn19.Width = 80
        GridViewTextBoxColumn20.EnableExpressionEditor = False
        GridViewTextBoxColumn20.HeaderText = "Address3"
        GridViewTextBoxColumn20.Name = "column6"
        GridViewTextBoxColumn20.ReadOnly = True
        GridViewTextBoxColumn20.Width = 80
        GridViewTextBoxColumn21.EnableExpressionEditor = False
        GridViewTextBoxColumn21.HeaderText = "Address4"
        GridViewTextBoxColumn21.Name = "column7"
        GridViewTextBoxColumn21.ReadOnly = True
        GridViewTextBoxColumn21.Width = 80
        GridViewTextBoxColumn22.EnableExpressionEditor = False
        GridViewTextBoxColumn22.HeaderText = "City"
        GridViewTextBoxColumn22.Name = "column8"
        GridViewTextBoxColumn22.ReadOnly = True
        GridViewTextBoxColumn22.Width = 80
        GridViewTextBoxColumn23.EnableExpressionEditor = False
        GridViewTextBoxColumn23.HeaderText = "State/Province"
        GridViewTextBoxColumn23.Name = "column9"
        GridViewTextBoxColumn23.ReadOnly = True
        GridViewTextBoxColumn23.Width = 80
        GridViewTextBoxColumn24.EnableExpressionEditor = False
        GridViewTextBoxColumn24.HeaderText = "ZipPostalCode"
        GridViewTextBoxColumn24.Name = "column10"
        GridViewTextBoxColumn24.ReadOnly = True
        GridViewTextBoxColumn24.Width = 80
        GridViewTextBoxColumn25.EnableExpressionEditor = False
        GridViewTextBoxColumn25.HeaderText = "Country"
        GridViewTextBoxColumn25.Name = "column11"
        GridViewTextBoxColumn25.ReadOnly = True
        GridViewTextBoxColumn25.Width = 80
        GridViewTextBoxColumn26.EnableExpressionEditor = False
        GridViewTextBoxColumn26.HeaderText = "Telephone"
        GridViewTextBoxColumn26.Name = "column12"
        GridViewTextBoxColumn26.ReadOnly = True
        GridViewTextBoxColumn26.Width = 80
        GridViewTextBoxColumn27.EnableExpressionEditor = False
        GridViewTextBoxColumn27.HeaderText = "Email"
        GridViewTextBoxColumn27.Name = "column3"
        GridViewTextBoxColumn27.ReadOnly = True
        GridViewTextBoxColumn27.Width = 80
        GridViewTextBoxColumn28.EnableExpressionEditor = False
        GridViewTextBoxColumn28.HeaderText = "Tin No"
        GridViewTextBoxColumn28.Name = "colTinNo"
        GridViewTextBoxColumn28.ReadOnly = True
        GridViewTextBoxColumn29.EnableExpressionEditor = False
        GridViewTextBoxColumn29.HeaderText = "CST No"
        GridViewTextBoxColumn29.Name = "colCST"
        GridViewTextBoxColumn29.ReadOnly = True
        GridViewTextBoxColumn30.HeaderText = "Loc_Code"
        GridViewTextBoxColumn30.Name = "colLoc"
        Me.MasterTemplate.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn16, GridViewTextBoxColumn17, GridViewTextBoxColumn18, GridViewTextBoxColumn19, GridViewTextBoxColumn20, GridViewTextBoxColumn21, GridViewTextBoxColumn22, GridViewTextBoxColumn23, GridViewTextBoxColumn24, GridViewTextBoxColumn25, GridViewTextBoxColumn26, GridViewTextBoxColumn27, GridViewTextBoxColumn28, GridViewTextBoxColumn29, GridViewTextBoxColumn30})
        Me.MasterTemplate.MasterTemplate.EnableFiltering = True
        Me.MasterTemplate.MasterTemplate.EnableGrouping = False
        Me.MasterTemplate.Name = "MasterTemplate"
        Me.MasterTemplate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MasterTemplate.Size = New System.Drawing.Size(974, 186)
        Me.MasterTemplate.TabIndex = 3
        Me.MasterTemplate.TabStop = False
        Me.MasterTemplate.Text = "RadGridView1"
        '
        'ddlShipToType
        '
        Me.ddlShipToType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlShipToType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem2.Selected = True
        RadListDataItem2.Text = "Sales"
        RadListDataItem2.TextWrap = True
        Me.ddlShipToType.Items.Add(RadListDataItem2)
        Me.ddlShipToType.Location = New System.Drawing.Point(99, 20)
        Me.ddlShipToType.MendatroryField = False
        Me.ddlShipToType.MyLinkLable1 = Me.lblShipToType
        Me.ddlShipToType.MyLinkLable2 = Nothing
        Me.ddlShipToType.Name = "ddlShipToType"
        Me.ddlShipToType.Size = New System.Drawing.Size(132, 18)
        Me.ddlShipToType.TabIndex = 0
        Me.ddlShipToType.Text = "Sales"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.Details)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Location = New System.Drawing.Point(3, 268)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.Details
        Me.RadPageView1.Size = New System.Drawing.Size(736, 318)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "Ship To Location"
        '
        'Details
        '
        Me.Details.Controls.Add(Me.grpShipToLocation)
        Me.Details.Location = New System.Drawing.Point(10, 37)
        Me.Details.Name = "Details"
        Me.Details.Size = New System.Drawing.Size(715, 270)
        Me.Details.Text = "Details"
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(928, 576)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(928, 576)
        Me.UcCustomFields1.TabIndex = 2
        '
        'FrmShipToLocationDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 639)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmShipToLocationDetails"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Ship To Location Details"
        CType(Me.grpShipToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpShipToLocation.ResumeLayout(False)
        Me.grpShipToLocation.PerformLayout()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtShipToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fndCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpAddress, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpAddress.ResumeLayout(False)
        Me.grpAddress.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCSTNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTinNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTelephone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Email, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAddress22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPostalCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblZipPostalCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTelephone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAddress4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAddress3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAddress1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtShipToLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShipToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAddress2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblShipToType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcustomerdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterTemplate.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlShipToType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.Details.ResumeLayout(False)
        Me.pvpCustomFields.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpShipToLocation As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtCustomerName As common.Controls.MyTextBox
    Friend WithEvents txtShipToLocationDesc As common.Controls.MyTextBox
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnAdd As Telerik.WinControls.UI.RadButton
    Friend WithEvents menuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuImport1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents grpAddress As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtPostalCode As common.Controls.MyTextBox
    Friend WithEvents txtState As common.Controls.MyTextBox
    Friend WithEvents txtTelephone As common.Controls.MyTextBox
    Friend WithEvents txtCountry As common.Controls.MyTextBox
    Friend WithEvents txtCity As common.Controls.MyTextBox
    Friend WithEvents txtAddress4 As common.Controls.MyTextBox
    Friend WithEvents txtAddress3 As common.Controls.MyTextBox
    Friend WithEvents txtAddress1 As common.Controls.MyTextBox
    Friend WithEvents txtAddress2 As common.Controls.MyTextBox
    Friend WithEvents RadTextBox1 As common.Controls.MyTextBox
    Friend WithEvents RadTextBox2 As common.Controls.MyTextBox
    Friend WithEvents RadTextBox3 As common.Controls.MyTextBox
    Friend WithEvents RadTextBox4 As common.Controls.MyTextBox
    Friend WithEvents txtAddress22 As common.Controls.MyTextBox
    Friend WithEvents menuPrint As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtEmail As common.Controls.MyTextBox
    Friend WithEvents lblShipToLocation As common.Controls.MyLabel
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents Email As common.Controls.MyLabel
    Friend WithEvents lblZipPostalCode As common.Controls.MyLabel
    Friend WithEvents lblState As common.Controls.MyLabel
    Friend WithEvents lblCity As common.Controls.MyLabel
    Friend WithEvents lblAddress As common.Controls.MyLabel
    Friend WithEvents lblTelephone As common.Controls.MyLabel
    Friend WithEvents lblCountry As common.Controls.MyLabel
    Friend WithEvents fndCustomer As common.Controls.MyTextBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents Details As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblShipToType As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtcustomer As common.UserControls.txtNavigator
    Friend WithEvents txtcustomerdesc As common.Controls.MyTextBox
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents ddlShipToType As common.Controls.MyComboBox
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtCSTNo As common.Controls.MyTextBox
    Friend WithEvents txtTinNo As common.Controls.MyTextBox
    Friend WithEvents txtShipToLocation As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
End Class

