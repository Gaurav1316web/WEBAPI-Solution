<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRouteMaster
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
        Me.components = New System.ComponentModel.Container()
        Me.rlblRouteid = New common.Controls.MyLabel()
        Me.rlblDescription = New common.Controls.MyLabel()
        Me.rlblType = New common.Controls.MyLabel()
        Me.rlblSalesmanCode = New common.Controls.MyLabel()
        Me.rlblRouteOFFDay = New common.Controls.MyLabel()
        Me.rlblCityID = New common.Controls.MyLabel()
        Me.rlblDistrict = New common.Controls.MyLabel()
        Me.rlblCategory = New common.Controls.MyLabel()
        Me.rlblRouteLength = New common.Controls.MyLabel()
        Me.rtxtroute_length = New common.Controls.MyTextBox()
        Me.rtxtDistrict = New common.Controls.MyTextBox()
        Me.rtxtdescription = New common.Controls.MyTextBox()
        Me.rddl_category = New common.Controls.MyComboBox()
        Me.rbtnSave = New Telerik.WinControls.UI.RadButton()
        Me.rbtnDelete = New Telerik.WinControls.UI.RadButton()
        Me.rbtnClose = New Telerik.WinControls.UI.RadButton()
        Me.rbtnReset = New Telerik.WinControls.UI.RadButton()
        Me.rddl_route_offday = New common.Controls.MyComboBox()
        Me.rlblsalesman_name = New common.Controls.MyLabel()
        Me.rtxtSalesman_name = New common.Controls.MyTextBox()
        Me.ddltype = New common.Controls.MyComboBox()
        Me.ToolTipGP_Route_Master = New System.Windows.Forms.ToolTip(Me.components)
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem_import = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem_Export = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem_Close = New Telerik.WinControls.UI.RadMenuItem()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.fndRoutePrice = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtRoutePrice = New common.Controls.MyTextBox()
        Me.dtpAcIn = New common.Controls.MyDateTimePicker()
        Me.rdoIN = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdoAC = New Telerik.WinControls.UI.RadRadioButton()
        Me.fndvcode = New common.UserControls.txtFinder()
        Me.lblvechilecode = New common.Controls.MyLabel()
        Me.fndnonprice = New common.UserControls.txtFinder()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.fndPriceCode = New common.UserControls.txtFinder()
        Me.rlblPriceCode = New common.Controls.MyLabel()
        Me.fndDepot = New common.UserControls.txtFinder()
        Me.lblDepot = New common.Controls.MyLabel()
        Me.fndcity_id = New common.UserControls.txtFinder()
        Me.fndSalesman_code = New common.UserControls.txtFinder()
        Me.fndRouteid = New common.UserControls.txtNavigator()
        Me.txtnonprice = New common.Controls.MyTextBox()
        Me.txtvcodedesc = New common.Controls.MyTextBox()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvDB = New common.UserControls.MyRadGridView()
        Me.txtpricecodedescription = New common.Controls.MyTextBox()
        Me.btnprint = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.txtDistance = New common.MyNumBox()
        Me.btnDistance = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        CType(Me.rlblRouteid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblSalesmanCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblRouteOFFDay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblCityID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblDistrict, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblRouteLength, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtroute_length, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtDistrict, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtdescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rddl_category, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rddl_route_offday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblsalesman_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtSalesman_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddltype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRoutePrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpAcIn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdoIN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdoAC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvechilecode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblPriceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtnonprice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtvcodedesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtpricecodedescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDistance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDistance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rlblRouteid
        '
        Me.rlblRouteid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblRouteid.Location = New System.Drawing.Point(12, 27)
        Me.rlblRouteid.Name = "rlblRouteid"
        Me.rlblRouteid.Size = New System.Drawing.Size(67, 16)
        Me.rlblRouteid.TabIndex = 0
        Me.rlblRouteid.Text = "Route Code"
        '
        'rlblDescription
        '
        Me.rlblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblDescription.Location = New System.Drawing.Point(12, 50)
        Me.rlblDescription.Name = "rlblDescription"
        Me.rlblDescription.Size = New System.Drawing.Size(66, 16)
        Me.rlblDescription.TabIndex = 10
        Me.rlblDescription.Text = "Description "
        '
        'rlblType
        '
        Me.rlblType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblType.Location = New System.Drawing.Point(12, 74)
        Me.rlblType.Name = "rlblType"
        Me.rlblType.Size = New System.Drawing.Size(31, 16)
        Me.rlblType.TabIndex = 14
        Me.rlblType.Text = "Type"
        '
        'rlblSalesmanCode
        '
        Me.rlblSalesmanCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblSalesmanCode.Location = New System.Drawing.Point(358, 74)
        Me.rlblSalesmanCode.Name = "rlblSalesmanCode"
        Me.rlblSalesmanCode.Size = New System.Drawing.Size(90, 16)
        Me.rlblSalesmanCode.TabIndex = 16
        Me.rlblSalesmanCode.Text = "Salesman Code "
        '
        'rlblRouteOFFDay
        '
        Me.rlblRouteOFFDay.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblRouteOFFDay.Location = New System.Drawing.Point(12, 98)
        Me.rlblRouteOFFDay.Name = "rlblRouteOFFDay"
        Me.rlblRouteOFFDay.Size = New System.Drawing.Size(89, 16)
        Me.rlblRouteOFFDay.TabIndex = 18
        Me.rlblRouteOFFDay.Text = "Route OFF Day "
        '
        'rlblCityID
        '
        Me.rlblCityID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblCityID.Location = New System.Drawing.Point(12, 122)
        Me.rlblCityID.Name = "rlblCityID"
        Me.rlblCityID.Size = New System.Drawing.Size(56, 16)
        Me.rlblCityID.TabIndex = 22
        Me.rlblCityID.Text = "City Code"
        '
        'rlblDistrict
        '
        Me.rlblDistrict.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblDistrict.Location = New System.Drawing.Point(12, 242)
        Me.rlblDistrict.Name = "rlblDistrict"
        Me.rlblDistrict.Size = New System.Drawing.Size(41, 16)
        Me.rlblDistrict.TabIndex = 36
        Me.rlblDistrict.Text = "District"
        '
        'rlblCategory
        '
        Me.rlblCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblCategory.Location = New System.Drawing.Point(358, 122)
        Me.rlblCategory.Name = "rlblCategory"
        Me.rlblCategory.Size = New System.Drawing.Size(52, 16)
        Me.rlblCategory.TabIndex = 24
        Me.rlblCategory.Text = "Category"
        '
        'rlblRouteLength
        '
        Me.rlblRouteLength.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblRouteLength.Location = New System.Drawing.Point(358, 50)
        Me.rlblRouteLength.Name = "rlblRouteLength"
        Me.rlblRouteLength.Size = New System.Drawing.Size(74, 16)
        Me.rlblRouteLength.TabIndex = 12
        Me.rlblRouteLength.Text = "Route Length"
        '
        'rtxtroute_length
        '
        Me.rtxtroute_length.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtroute_length.Location = New System.Drawing.Point(459, 49)
        Me.rtxtroute_length.MaxLength = 8
        Me.rtxtroute_length.MendatroryField = False
        Me.rtxtroute_length.MyLinkLable1 = Me.rlblRouteLength
        Me.rtxtroute_length.MyLinkLable2 = Nothing
        Me.rtxtroute_length.Name = "rtxtroute_length"
        Me.rtxtroute_length.Size = New System.Drawing.Size(200, 18)
        Me.rtxtroute_length.TabIndex = 6
        '
        'rtxtDistrict
        '
        Me.rtxtDistrict.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtDistrict.Location = New System.Drawing.Point(147, 241)
        Me.rtxtDistrict.MaxLength = 20
        Me.rtxtDistrict.MendatroryField = False
        Me.rtxtDistrict.MyLinkLable1 = Me.rlblDistrict
        Me.rtxtDistrict.MyLinkLable2 = Nothing
        Me.rtxtDistrict.Name = "rtxtDistrict"
        Me.rtxtDistrict.Size = New System.Drawing.Size(200, 18)
        Me.rtxtDistrict.TabIndex = 21
        '
        'rtxtdescription
        '
        Me.rtxtdescription.AutoSize = False
        Me.rtxtdescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtdescription.Location = New System.Drawing.Point(147, 49)
        Me.rtxtdescription.MaxLength = 60
        Me.rtxtdescription.MendatroryField = False
        Me.rtxtdescription.Multiline = True
        Me.rtxtdescription.MyLinkLable1 = Me.rlblDescription
        Me.rtxtdescription.MyLinkLable2 = Nothing
        Me.rtxtdescription.Name = "rtxtdescription"
        Me.rtxtdescription.Size = New System.Drawing.Size(200, 18)
        Me.rtxtdescription.TabIndex = 5
        '
        'rddl_category
        '
        Me.rddl_category.AutoCompleteDisplayMember = Nothing
        Me.rddl_category.AutoCompleteValueMember = Nothing
        Me.rddl_category.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.rddl_category.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rddl_category.Location = New System.Drawing.Point(459, 121)
        Me.rddl_category.MendatroryField = False
        Me.rddl_category.MyLinkLable1 = Me.rlblCategory
        Me.rddl_category.MyLinkLable2 = Nothing
        Me.rddl_category.Name = "rddl_category"
        Me.rddl_category.Size = New System.Drawing.Size(200, 18)
        Me.rddl_category.TabIndex = 12
        '
        'rbtnSave
        '
        Me.rbtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnSave.Location = New System.Drawing.Point(3, 468)
        Me.rbtnSave.Name = "rbtnSave"
        Me.rbtnSave.Size = New System.Drawing.Size(68, 18)
        Me.rbtnSave.TabIndex = 0
        Me.rbtnSave.Text = "Save"
        '
        'rbtnDelete
        '
        Me.rbtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnDelete.Location = New System.Drawing.Point(77, 468)
        Me.rbtnDelete.Name = "rbtnDelete"
        Me.rbtnDelete.Size = New System.Drawing.Size(68, 18)
        Me.rbtnDelete.TabIndex = 1
        Me.rbtnDelete.Text = "Delete"
        '
        'rbtnClose
        '
        Me.rbtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbtnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnClose.Location = New System.Drawing.Point(596, 468)
        Me.rbtnClose.Name = "rbtnClose"
        Me.rbtnClose.Size = New System.Drawing.Size(68, 18)
        Me.rbtnClose.TabIndex = 3
        Me.rbtnClose.Text = "Close"
        '
        'rbtnReset
        '
        Me.rbtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.rbtnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtnReset.Location = New System.Drawing.Point(330, 26)
        Me.rbtnReset.Name = "rbtnReset"
        Me.rbtnReset.Size = New System.Drawing.Size(18, 19)
        Me.rbtnReset.TabIndex = 1
        Me.rbtnReset.Text = "&"
        '
        'rddl_route_offday
        '
        Me.rddl_route_offday.AutoCompleteDisplayMember = Nothing
        Me.rddl_route_offday.AutoCompleteValueMember = Nothing
        Me.rddl_route_offday.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.rddl_route_offday.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rddl_route_offday.Location = New System.Drawing.Point(147, 97)
        Me.rddl_route_offday.MendatroryField = False
        Me.rddl_route_offday.MyLinkLable1 = Me.rlblRouteOFFDay
        Me.rddl_route_offday.MyLinkLable2 = Nothing
        Me.rddl_route_offday.Name = "rddl_route_offday"
        Me.rddl_route_offday.Size = New System.Drawing.Size(200, 18)
        Me.rddl_route_offday.TabIndex = 9
        '
        'rlblsalesman_name
        '
        Me.rlblsalesman_name.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblsalesman_name.Location = New System.Drawing.Point(358, 98)
        Me.rlblsalesman_name.Name = "rlblsalesman_name"
        Me.rlblsalesman_name.Size = New System.Drawing.Size(90, 16)
        Me.rlblsalesman_name.TabIndex = 20
        Me.rlblsalesman_name.Text = "Salesman Name"
        '
        'rtxtSalesman_name
        '
        Me.rtxtSalesman_name.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtSalesman_name.Location = New System.Drawing.Point(459, 97)
        Me.rtxtSalesman_name.MaxLength = 50
        Me.rtxtSalesman_name.MendatroryField = False
        Me.rtxtSalesman_name.MyLinkLable1 = Me.rlblsalesman_name
        Me.rtxtSalesman_name.MyLinkLable2 = Nothing
        Me.rtxtSalesman_name.Name = "rtxtSalesman_name"
        Me.rtxtSalesman_name.Size = New System.Drawing.Size(200, 18)
        Me.rtxtSalesman_name.TabIndex = 10
        Me.rtxtSalesman_name.TabStop = False
        '
        'ddltype
        '
        Me.ddltype.AutoCompleteDisplayMember = Nothing
        Me.ddltype.AutoCompleteValueMember = Nothing
        Me.ddltype.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddltype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddltype.Location = New System.Drawing.Point(147, 73)
        Me.ddltype.MendatroryField = False
        Me.ddltype.MyLinkLable1 = Me.rlblType
        Me.ddltype.MyLinkLable2 = Nothing
        Me.ddltype.Name = "ddltype"
        Me.ddltype.Size = New System.Drawing.Size(200, 18)
        Me.ddltype.TabIndex = 7
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem_import, Me.RadMenuItem_Export, Me.RadMenuItem_Close})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem_import
        '
        Me.RadMenuItem_import.AccessibleDescription = "Import"
        Me.RadMenuItem_import.AccessibleName = "Import"
        Me.RadMenuItem_import.Name = "RadMenuItem_import"
        Me.RadMenuItem_import.Text = "Import"
        '
        'RadMenuItem_Export
        '
        Me.RadMenuItem_Export.AccessibleDescription = "Export"
        Me.RadMenuItem_Export.AccessibleName = "Export"
        Me.RadMenuItem_Export.Name = "RadMenuItem_Export"
        Me.RadMenuItem_Export.Text = "Export"
        '
        'RadMenuItem_Close
        '
        Me.RadMenuItem_Close.AccessibleDescription = "Close"
        Me.RadMenuItem_Close.AccessibleName = "Close"
        Me.RadMenuItem_Close.Name = "RadMenuItem_Close"
        Me.RadMenuItem_Close.Text = "Close"
        '
        'fndRoutePrice
        '
        Me.fndRoutePrice.Location = New System.Drawing.Point(146, 193)
        Me.fndRoutePrice.MendatroryField = False
        Me.fndRoutePrice.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRoutePrice.MyLinkLable1 = Me.MyLabel1
        Me.fndRoutePrice.MyLinkLable2 = Nothing
        Me.fndRoutePrice.MyReadOnly = False
        Me.fndRoutePrice.MyShowMasterFormButton = False
        Me.fndRoutePrice.Name = "fndRoutePrice"
        Me.fndRoutePrice.Size = New System.Drawing.Size(200, 18)
        Me.fndRoutePrice.TabIndex = 17
        Me.fndRoutePrice.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 194)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(118, 16)
        Me.MyLabel1.TabIndex = 39
        Me.MyLabel1.Text = "Route Exc Price Code"
        '
        'txtRoutePrice
        '
        Me.txtRoutePrice.AutoSize = False
        Me.txtRoutePrice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoutePrice.Location = New System.Drawing.Point(361, 193)
        Me.txtRoutePrice.MaxLength = 60
        Me.txtRoutePrice.MendatroryField = False
        Me.txtRoutePrice.Multiline = True
        Me.txtRoutePrice.MyLinkLable1 = Nothing
        Me.txtRoutePrice.MyLinkLable2 = Nothing
        Me.txtRoutePrice.Name = "txtRoutePrice"
        Me.txtRoutePrice.ReadOnly = True
        Me.txtRoutePrice.Size = New System.Drawing.Size(298, 18)
        Me.txtRoutePrice.TabIndex = 18
        Me.txtRoutePrice.TabStop = False
        '
        'dtpAcIn
        '
        Me.dtpAcIn.CustomFormat = "dd/MM/yyyy"
        Me.dtpAcIn.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAcIn.Location = New System.Drawing.Point(461, 25)
        Me.dtpAcIn.MendatroryField = False
        Me.dtpAcIn.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAcIn.MyLinkLable1 = Nothing
        Me.dtpAcIn.MyLinkLable2 = Nothing
        Me.dtpAcIn.Name = "dtpAcIn"
        Me.dtpAcIn.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAcIn.Size = New System.Drawing.Size(82, 20)
        Me.dtpAcIn.TabIndex = 4
        Me.dtpAcIn.TabStop = False
        Me.dtpAcIn.Text = "04/08/2011"
        Me.dtpAcIn.Value = New Date(2011, 8, 4, 11, 41, 7, 406)
        '
        'rdoIN
        '
        Me.rdoIN.Location = New System.Drawing.Point(598, 26)
        Me.rdoIN.Name = "rdoIN"
        Me.rdoIN.Size = New System.Drawing.Size(59, 18)
        Me.rdoIN.TabIndex = 3
        Me.rdoIN.Text = "Inactive"
        '
        'rdoAC
        '
        Me.rdoAC.Location = New System.Drawing.Point(545, 26)
        Me.rdoAC.Name = "rdoAC"
        Me.rdoAC.Size = New System.Drawing.Size(51, 18)
        Me.rdoAC.TabIndex = 2
        Me.rdoAC.Text = "Active"
        '
        'fndvcode
        '
        Me.fndvcode.Location = New System.Drawing.Point(147, 217)
        Me.fndvcode.MendatroryField = False
        Me.fndvcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndvcode.MyLinkLable1 = Me.lblvechilecode
        Me.fndvcode.MyLinkLable2 = Nothing
        Me.fndvcode.MyReadOnly = False
        Me.fndvcode.MyShowMasterFormButton = False
        Me.fndvcode.Name = "fndvcode"
        Me.fndvcode.Size = New System.Drawing.Size(200, 18)
        Me.fndvcode.TabIndex = 19
        Me.fndvcode.Value = ""
        '
        'lblvechilecode
        '
        Me.lblvechilecode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvechilecode.Location = New System.Drawing.Point(12, 218)
        Me.lblvechilecode.Name = "lblvechilecode"
        Me.lblvechilecode.Size = New System.Drawing.Size(74, 16)
        Me.lblvechilecode.TabIndex = 33
        Me.lblvechilecode.Text = "Vehicle Code"
        '
        'fndnonprice
        '
        Me.fndnonprice.Location = New System.Drawing.Point(146, 169)
        Me.fndnonprice.MendatroryField = False
        Me.fndnonprice.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndnonprice.MyLinkLable1 = Me.RadLabel1
        Me.fndnonprice.MyLinkLable2 = Nothing
        Me.fndnonprice.MyReadOnly = False
        Me.fndnonprice.MyShowMasterFormButton = False
        Me.fndnonprice.Name = "fndnonprice"
        Me.fndnonprice.Size = New System.Drawing.Size(200, 18)
        Me.fndnonprice.TabIndex = 15
        Me.fndnonprice.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(12, 170)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(108, 16)
        Me.RadLabel1.TabIndex = 30
        Me.RadLabel1.Text = "Non Exc Price Code"
        '
        'fndPriceCode
        '
        Me.fndPriceCode.Location = New System.Drawing.Point(147, 145)
        Me.fndPriceCode.MendatroryField = False
        Me.fndPriceCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPriceCode.MyLinkLable1 = Me.rlblPriceCode
        Me.fndPriceCode.MyLinkLable2 = Nothing
        Me.fndPriceCode.MyReadOnly = False
        Me.fndPriceCode.MyShowMasterFormButton = False
        Me.fndPriceCode.Name = "fndPriceCode"
        Me.fndPriceCode.Size = New System.Drawing.Size(200, 18)
        Me.fndPriceCode.TabIndex = 13
        Me.fndPriceCode.Value = ""
        '
        'rlblPriceCode
        '
        Me.rlblPriceCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblPriceCode.Location = New System.Drawing.Point(12, 146)
        Me.rlblPriceCode.Name = "rlblPriceCode"
        Me.rlblPriceCode.Size = New System.Drawing.Size(62, 16)
        Me.rlblPriceCode.TabIndex = 26
        Me.rlblPriceCode.Text = "Price Code"
        '
        'fndDepot
        '
        Me.fndDepot.Location = New System.Drawing.Point(459, 241)
        Me.fndDepot.MendatroryField = False
        Me.fndDepot.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDepot.MyLinkLable1 = Me.lblDepot
        Me.fndDepot.MyLinkLable2 = Nothing
        Me.fndDepot.MyReadOnly = False
        Me.fndDepot.MyShowMasterFormButton = False
        Me.fndDepot.Name = "fndDepot"
        Me.fndDepot.Size = New System.Drawing.Size(198, 18)
        Me.fndDepot.TabIndex = 23
        Me.fndDepot.Value = ""
        '
        'lblDepot
        '
        Me.lblDepot.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepot.Location = New System.Drawing.Point(368, 242)
        Me.lblDepot.Name = "lblDepot"
        Me.lblDepot.Size = New System.Drawing.Size(36, 16)
        Me.lblDepot.TabIndex = 22
        Me.lblDepot.Text = "Depot"
        '
        'fndcity_id
        '
        Me.fndcity_id.Location = New System.Drawing.Point(146, 121)
        Me.fndcity_id.MendatroryField = False
        Me.fndcity_id.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcity_id.MyLinkLable1 = Me.rlblCityID
        Me.fndcity_id.MyLinkLable2 = Nothing
        Me.fndcity_id.MyReadOnly = False
        Me.fndcity_id.MyShowMasterFormButton = False
        Me.fndcity_id.Name = "fndcity_id"
        Me.fndcity_id.Size = New System.Drawing.Size(200, 18)
        Me.fndcity_id.TabIndex = 11
        Me.fndcity_id.Value = ""
        '
        'fndSalesman_code
        '
        Me.fndSalesman_code.Location = New System.Drawing.Point(459, 73)
        Me.fndSalesman_code.MendatroryField = False
        Me.fndSalesman_code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSalesman_code.MyLinkLable1 = Me.rlblSalesmanCode
        Me.fndSalesman_code.MyLinkLable2 = Nothing
        Me.fndSalesman_code.MyReadOnly = False
        Me.fndSalesman_code.MyShowMasterFormButton = False
        Me.fndSalesman_code.Name = "fndSalesman_code"
        Me.fndSalesman_code.Size = New System.Drawing.Size(200, 18)
        Me.fndSalesman_code.TabIndex = 8
        Me.fndSalesman_code.Value = ""
        '
        'fndRouteid
        '
        Me.fndRouteid.Location = New System.Drawing.Point(146, 26)
        Me.fndRouteid.MendatroryField = True
        Me.fndRouteid.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndRouteid.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndRouteid.MyLinkLable1 = Me.rlblRouteid
        Me.fndRouteid.MyLinkLable2 = Nothing
        Me.fndRouteid.MyMaxLength = 32767
        Me.fndRouteid.MyReadOnly = False
        Me.fndRouteid.Name = "fndRouteid"
        Me.fndRouteid.Size = New System.Drawing.Size(184, 18)
        Me.fndRouteid.TabIndex = 0
        Me.fndRouteid.Value = ""
        '
        'txtnonprice
        '
        Me.txtnonprice.AutoSize = False
        Me.txtnonprice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnonprice.Location = New System.Drawing.Point(361, 169)
        Me.txtnonprice.MaxLength = 60
        Me.txtnonprice.MendatroryField = False
        Me.txtnonprice.Multiline = True
        Me.txtnonprice.MyLinkLable1 = Nothing
        Me.txtnonprice.MyLinkLable2 = Nothing
        Me.txtnonprice.Name = "txtnonprice"
        Me.txtnonprice.ReadOnly = True
        Me.txtnonprice.Size = New System.Drawing.Size(298, 18)
        Me.txtnonprice.TabIndex = 16
        Me.txtnonprice.TabStop = False
        '
        'txtvcodedesc
        '
        Me.txtvcodedesc.AutoSize = False
        Me.txtvcodedesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtvcodedesc.Location = New System.Drawing.Point(361, 217)
        Me.txtvcodedesc.MaxLength = 60
        Me.txtvcodedesc.MendatroryField = False
        Me.txtvcodedesc.Multiline = True
        Me.txtvcodedesc.MyLinkLable1 = Nothing
        Me.txtvcodedesc.MyLinkLable2 = Nothing
        Me.txtvcodedesc.Name = "txtvcodedesc"
        Me.txtvcodedesc.ReadOnly = True
        Me.txtvcodedesc.Size = New System.Drawing.Size(298, 18)
        Me.txtvcodedesc.TabIndex = 20
        Me.txtvcodedesc.TabStop = False
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gvDB)
        Me.RadGroupBox4.HeaderText = "Replicate In Other Companies"
        Me.RadGroupBox4.Location = New System.Drawing.Point(10, 300)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(647, 162)
        Me.RadGroupBox4.TabIndex = 24
        Me.RadGroupBox4.Text = "Replicate In Other Companies"
        '
        'gvDB
        '
        Me.gvDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDB.Location = New System.Drawing.Point(10, 20)
        '
        'gvDB
        '
        Me.gvDB.MasterTemplate.AllowAddNewRow = False
        Me.gvDB.Name = "gvDB"
        Me.gvDB.ShowGroupPanel = False
        Me.gvDB.Size = New System.Drawing.Size(627, 132)
        Me.gvDB.TabIndex = 0
        Me.gvDB.TabStop = False
        Me.gvDB.Text = "RadGridView1"
        '
        'txtpricecodedescription
        '
        Me.txtpricecodedescription.AutoSize = False
        Me.txtpricecodedescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpricecodedescription.Location = New System.Drawing.Point(361, 145)
        Me.txtpricecodedescription.MaxLength = 60
        Me.txtpricecodedescription.MendatroryField = False
        Me.txtpricecodedescription.Multiline = True
        Me.txtpricecodedescription.MyLinkLable1 = Nothing
        Me.txtpricecodedescription.MyLinkLable2 = Nothing
        Me.txtpricecodedescription.Name = "txtpricecodedescription"
        Me.txtpricecodedescription.ReadOnly = True
        Me.txtpricecodedescription.Size = New System.Drawing.Size(298, 18)
        Me.txtpricecodedescription.TabIndex = 14
        Me.txtpricecodedescription.TabStop = False
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprint.Location = New System.Drawing.Point(151, 468)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(86, 18)
        Me.btnprint.TabIndex = 2
        Me.btnprint.Text = "Print History"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(664, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'txtDistance
        '
        Me.txtDistance.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtDistance.DecimalPlaces = 3
        Me.txtDistance.Location = New System.Drawing.Point(147, 261)
        Me.txtDistance.MendatroryField = False
        Me.txtDistance.MyLinkLable1 = Nothing
        Me.txtDistance.MyLinkLable2 = Nothing
        Me.txtDistance.Name = "txtDistance"
        Me.txtDistance.Size = New System.Drawing.Size(201, 20)
        Me.txtDistance.TabIndex = 41
        Me.txtDistance.Text = "0"
        Me.txtDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDistance.Value = 0.0R
        '
        'btnDistance
        '
        Me.btnDistance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDistance.Location = New System.Drawing.Point(12, 265)
        Me.btnDistance.Name = "btnDistance"
        Me.btnDistance.Size = New System.Drawing.Size(81, 16)
        Me.btnDistance.TabIndex = 86
        Me.btnDistance.Text = "Distance(K.M.)"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(358, 27)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel2.TabIndex = 87
        Me.MyLabel2.Text = "Route Date"
        '
        'frmRouteMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(664, 487)
        Me.Controls.Add(Me.MyLabel2)
        Me.Controls.Add(Me.btnDistance)
        Me.Controls.Add(Me.txtDistance)
        Me.Controls.Add(Me.btnprint)
        Me.Controls.Add(Me.fndRoutePrice)
        Me.Controls.Add(Me.rbtnSave)
        Me.Controls.Add(Me.rbtnDelete)
        Me.Controls.Add(Me.rbtnClose)
        Me.Controls.Add(Me.txtRoutePrice)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.MyLabel1)
        Me.Controls.Add(Me.rlblRouteid)
        Me.Controls.Add(Me.dtpAcIn)
        Me.Controls.Add(Me.rtxtroute_length)
        Me.Controls.Add(Me.rdoIN)
        Me.Controls.Add(Me.rtxtDistrict)
        Me.Controls.Add(Me.rdoAC)
        Me.Controls.Add(Me.rddl_category)
        Me.Controls.Add(Me.fndvcode)
        Me.Controls.Add(Me.rlblRouteLength)
        Me.Controls.Add(Me.fndnonprice)
        Me.Controls.Add(Me.rtxtdescription)
        Me.Controls.Add(Me.fndPriceCode)
        Me.Controls.Add(Me.rlblCategory)
        Me.Controls.Add(Me.fndDepot)
        Me.Controls.Add(Me.rlblDistrict)
        Me.Controls.Add(Me.fndcity_id)
        Me.Controls.Add(Me.rlblCityID)
        Me.Controls.Add(Me.fndSalesman_code)
        Me.Controls.Add(Me.rddl_route_offday)
        Me.Controls.Add(Me.fndRouteid)
        Me.Controls.Add(Me.rbtnReset)
        Me.Controls.Add(Me.txtnonprice)
        Me.Controls.Add(Me.rlblRouteOFFDay)
        Me.Controls.Add(Me.txtvcodedesc)
        Me.Controls.Add(Me.rlblsalesman_name)
        Me.Controls.Add(Me.RadLabel1)
        Me.Controls.Add(Me.rlblSalesmanCode)
        Me.Controls.Add(Me.lblvechilecode)
        Me.Controls.Add(Me.rtxtSalesman_name)
        Me.Controls.Add(Me.RadGroupBox4)
        Me.Controls.Add(Me.rlblType)
        Me.Controls.Add(Me.lblDepot)
        Me.Controls.Add(Me.ddltype)
        Me.Controls.Add(Me.txtpricecodedescription)
        Me.Controls.Add(Me.rlblDescription)
        Me.Controls.Add(Me.rlblPriceCode)
        Me.Name = "frmRouteMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Route Master"
        CType(Me.rlblRouteid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblSalesmanCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblRouteOFFDay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblCityID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblDistrict, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblRouteLength, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtroute_length, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtDistrict, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtdescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rddl_category, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rddl_route_offday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblsalesman_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtSalesman_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddltype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRoutePrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpAcIn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdoIN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdoAC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvechilecode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblPriceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtnonprice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtvcodedesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtpricecodedescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDistance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDistance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rtxtroute_length As common.Controls.MyTextBox
    Friend WithEvents rtxtDistrict As common.Controls.MyTextBox
    Friend WithEvents rtxtdescription As common.Controls.MyTextBox
    Friend WithEvents rddl_category As common.Controls.MyComboBox
    Friend WithEvents rbtnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents rddl_route_offday As common.Controls.MyComboBox
    Friend WithEvents rtxtSalesman_name As common.Controls.MyTextBox
    Friend WithEvents ddltype As common.Controls.MyComboBox
    Friend WithEvents ToolTipGP_Route_Master As System.Windows.Forms.ToolTip
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem_import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem_Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem_Close As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents txtpricecodedescription As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvDB As common.UserControls.MyRadGridView
    Friend WithEvents txtvcodedesc As common.Controls.MyTextBox
    Friend WithEvents txtnonprice As common.Controls.MyTextBox
    Friend WithEvents rlblRouteid As common.Controls.MyLabel
    Friend WithEvents rlblDescription As common.Controls.MyLabel
    Friend WithEvents rlblType As common.Controls.MyLabel
    Friend WithEvents rlblSalesmanCode As common.Controls.MyLabel
    Friend WithEvents rlblRouteOFFDay As common.Controls.MyLabel
    Friend WithEvents rlblCityID As common.Controls.MyLabel
    Friend WithEvents rlblDistrict As common.Controls.MyLabel
    Friend WithEvents rlblCategory As common.Controls.MyLabel
    Friend WithEvents rlblRouteLength As common.Controls.MyLabel
    Friend WithEvents rlblsalesman_name As common.Controls.MyLabel
    Friend WithEvents rlblPriceCode As common.Controls.MyLabel
    Friend WithEvents lblDepot As common.Controls.MyLabel
    Friend WithEvents lblvechilecode As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents fndRouteid As common.UserControls.txtNavigator
    Friend WithEvents fndSalesman_code As common.UserControls.txtFinder
    Friend WithEvents fndcity_id As common.UserControls.txtFinder
    Friend WithEvents fndDepot As common.UserControls.txtFinder
    Friend WithEvents fndPriceCode As common.UserControls.txtFinder
    Friend WithEvents fndnonprice As common.UserControls.txtFinder
    Friend WithEvents fndvcode As common.UserControls.txtFinder
    Friend WithEvents rdoIN As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdoAC As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents dtpAcIn As common.Controls.MyDateTimePicker
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndRoutePrice As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtRoutePrice As common.Controls.MyTextBox
    Friend WithEvents txtDistance As common.MyNumBox
    Friend WithEvents btnDistance As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
End Class

