Imports XpertERPEngine
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
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
        Me.fndRouteid = New common.UserControls.txtNavigator()
        Me.txtnonprice = New common.Controls.MyTextBox()
        Me.txtvcodedesc = New common.Controls.MyTextBox()
        Me.txtpricecodedescription = New common.Controls.MyTextBox()
        Me.btnprint = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.txtDistance = New common.MyNumBox()
        Me.btnDistance = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fnd_saleman_code = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtRouteTime = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.dgv = New common.UserControls.MyRadGridView()
        Me.txtTollAmount = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.chkIsEarlyRoute = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtMorningCOT = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtEveningCOT = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtSeqNo = New common.MyNumBox()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.cboEntryUOM = New common.Controls.MyComboBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtLocationDesc = New common.Controls.MyTextBox()
        Me.btnStatus = New Telerik.WinControls.UI.RadButton()
        Me.fndZone = New common.UserControls.txtFinder()
        Me.MyLabel8 = New common.Controls.MyLabel()
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
        CType(Me.txtpricecodedescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDistance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDistance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRouteTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTollAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsEarlyRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMorningCOT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEveningCOT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSeqNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboEntryUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rlblRouteid
        '
        Me.rlblRouteid.FieldName = Nothing
        Me.rlblRouteid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblRouteid.Location = New System.Drawing.Point(12, 27)
        Me.rlblRouteid.Name = "rlblRouteid"
        Me.rlblRouteid.Size = New System.Drawing.Size(67, 16)
        Me.rlblRouteid.TabIndex = 0
        Me.rlblRouteid.Text = "Route Code"
        '
        'rlblDescription
        '
        Me.rlblDescription.FieldName = Nothing
        Me.rlblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblDescription.Location = New System.Drawing.Point(12, 50)
        Me.rlblDescription.Name = "rlblDescription"
        Me.rlblDescription.Size = New System.Drawing.Size(66, 16)
        Me.rlblDescription.TabIndex = 10
        Me.rlblDescription.Text = "Description "
        '
        'rlblType
        '
        Me.rlblType.FieldName = Nothing
        Me.rlblType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblType.Location = New System.Drawing.Point(12, 73)
        Me.rlblType.Name = "rlblType"
        Me.rlblType.Size = New System.Drawing.Size(31, 16)
        Me.rlblType.TabIndex = 14
        Me.rlblType.Text = "Type"
        '
        'rlblSalesmanCode
        '
        Me.rlblSalesmanCode.FieldName = Nothing
        Me.rlblSalesmanCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblSalesmanCode.Location = New System.Drawing.Point(361, 73)
        Me.rlblSalesmanCode.Name = "rlblSalesmanCode"
        Me.rlblSalesmanCode.Size = New System.Drawing.Size(57, 16)
        Me.rlblSalesmanCode.TabIndex = 16
        Me.rlblSalesmanCode.Text = "Salesman"
        '
        'rlblRouteOFFDay
        '
        Me.rlblRouteOFFDay.FieldName = Nothing
        Me.rlblRouteOFFDay.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblRouteOFFDay.Location = New System.Drawing.Point(12, 96)
        Me.rlblRouteOFFDay.Name = "rlblRouteOFFDay"
        Me.rlblRouteOFFDay.Size = New System.Drawing.Size(89, 16)
        Me.rlblRouteOFFDay.TabIndex = 18
        Me.rlblRouteOFFDay.Text = "Route OFF Day "
        '
        'rlblCityID
        '
        Me.rlblCityID.FieldName = Nothing
        Me.rlblCityID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblCityID.Location = New System.Drawing.Point(12, 119)
        Me.rlblCityID.Name = "rlblCityID"
        Me.rlblCityID.Size = New System.Drawing.Size(56, 16)
        Me.rlblCityID.TabIndex = 22
        Me.rlblCityID.Text = "City Code"
        '
        'rlblDistrict
        '
        Me.rlblDistrict.FieldName = Nothing
        Me.rlblDistrict.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblDistrict.Location = New System.Drawing.Point(12, 255)
        Me.rlblDistrict.Name = "rlblDistrict"
        Me.rlblDistrict.Size = New System.Drawing.Size(41, 16)
        Me.rlblDistrict.TabIndex = 36
        Me.rlblDistrict.Text = "District"
        '
        'rlblCategory
        '
        Me.rlblCategory.FieldName = Nothing
        Me.rlblCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblCategory.Location = New System.Drawing.Point(361, 96)
        Me.rlblCategory.Name = "rlblCategory"
        Me.rlblCategory.Size = New System.Drawing.Size(52, 16)
        Me.rlblCategory.TabIndex = 24
        Me.rlblCategory.Text = "Category"
        '
        'rlblRouteLength
        '
        Me.rlblRouteLength.FieldName = Nothing
        Me.rlblRouteLength.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblRouteLength.Location = New System.Drawing.Point(361, 50)
        Me.rlblRouteLength.Name = "rlblRouteLength"
        Me.rlblRouteLength.Size = New System.Drawing.Size(74, 16)
        Me.rlblRouteLength.TabIndex = 12
        Me.rlblRouteLength.Text = "Route Length"
        '
        'rtxtroute_length
        '
        Me.rtxtroute_length.CalculationExpression = Nothing
        Me.rtxtroute_length.FieldCode = Nothing
        Me.rtxtroute_length.FieldDesc = Nothing
        Me.rtxtroute_length.FieldMaxLength = 0
        Me.rtxtroute_length.FieldName = Nothing
        Me.rtxtroute_length.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtroute_length.isCalculatedField = False
        Me.rtxtroute_length.IsSourceFromTable = False
        Me.rtxtroute_length.IsSourceFromValueList = False
        Me.rtxtroute_length.IsUnique = False
        Me.rtxtroute_length.Location = New System.Drawing.Point(459, 49)
        Me.rtxtroute_length.MaxLength = 8
        Me.rtxtroute_length.MendatroryField = False
        Me.rtxtroute_length.MyLinkLable1 = Me.rlblRouteLength
        Me.rtxtroute_length.MyLinkLable2 = Nothing
        Me.rtxtroute_length.Name = "rtxtroute_length"
        Me.rtxtroute_length.ReferenceFieldDesc = Nothing
        Me.rtxtroute_length.ReferenceFieldName = Nothing
        Me.rtxtroute_length.ReferenceTableName = Nothing
        Me.rtxtroute_length.Size = New System.Drawing.Size(200, 18)
        Me.rtxtroute_length.TabIndex = 6
        '
        'rtxtDistrict
        '
        Me.rtxtDistrict.CalculationExpression = Nothing
        Me.rtxtDistrict.FieldCode = Nothing
        Me.rtxtDistrict.FieldDesc = Nothing
        Me.rtxtDistrict.FieldMaxLength = 0
        Me.rtxtDistrict.FieldName = Nothing
        Me.rtxtDistrict.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtDistrict.isCalculatedField = False
        Me.rtxtDistrict.IsSourceFromTable = False
        Me.rtxtDistrict.IsSourceFromValueList = False
        Me.rtxtDistrict.IsUnique = False
        Me.rtxtDistrict.Location = New System.Drawing.Point(146, 254)
        Me.rtxtDistrict.MaxLength = 20
        Me.rtxtDistrict.MendatroryField = False
        Me.rtxtDistrict.MyLinkLable1 = Me.rlblDistrict
        Me.rtxtDistrict.MyLinkLable2 = Nothing
        Me.rtxtDistrict.Name = "rtxtDistrict"
        Me.rtxtDistrict.ReferenceFieldDesc = Nothing
        Me.rtxtDistrict.ReferenceFieldName = Nothing
        Me.rtxtDistrict.ReferenceTableName = Nothing
        Me.rtxtDistrict.Size = New System.Drawing.Size(200, 18)
        Me.rtxtDistrict.TabIndex = 21
        '
        'rtxtdescription
        '
        Me.rtxtdescription.AutoSize = False
        Me.rtxtdescription.CalculationExpression = Nothing
        Me.rtxtdescription.FieldCode = Nothing
        Me.rtxtdescription.FieldDesc = Nothing
        Me.rtxtdescription.FieldMaxLength = 0
        Me.rtxtdescription.FieldName = Nothing
        Me.rtxtdescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtdescription.isCalculatedField = False
        Me.rtxtdescription.IsSourceFromTable = False
        Me.rtxtdescription.IsSourceFromValueList = False
        Me.rtxtdescription.IsUnique = False
        Me.rtxtdescription.Location = New System.Drawing.Point(146, 49)
        Me.rtxtdescription.MaxLength = 60
        Me.rtxtdescription.MendatroryField = False
        Me.rtxtdescription.Multiline = True
        Me.rtxtdescription.MyLinkLable1 = Me.rlblDescription
        Me.rtxtdescription.MyLinkLable2 = Nothing
        Me.rtxtdescription.Name = "rtxtdescription"
        Me.rtxtdescription.ReferenceFieldDesc = Nothing
        Me.rtxtdescription.ReferenceFieldName = Nothing
        Me.rtxtdescription.ReferenceTableName = Nothing
        Me.rtxtdescription.Size = New System.Drawing.Size(200, 18)
        Me.rtxtdescription.TabIndex = 5
        '
        'rddl_category
        '
        Me.rddl_category.AutoCompleteDisplayMember = Nothing
        Me.rddl_category.AutoCompleteValueMember = Nothing
        Me.rddl_category.CalculationExpression = Nothing
        Me.rddl_category.DropDownAnimationEnabled = True
        Me.rddl_category.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.rddl_category.FieldCode = Nothing
        Me.rddl_category.FieldDesc = Nothing
        Me.rddl_category.FieldMaxLength = 0
        Me.rddl_category.FieldName = Nothing
        Me.rddl_category.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rddl_category.isCalculatedField = False
        Me.rddl_category.IsSourceFromTable = False
        Me.rddl_category.IsSourceFromValueList = False
        Me.rddl_category.IsUnique = False
        Me.rddl_category.Location = New System.Drawing.Point(459, 95)
        Me.rddl_category.MendatroryField = False
        Me.rddl_category.MyLinkLable1 = Me.rlblCategory
        Me.rddl_category.MyLinkLable2 = Nothing
        Me.rddl_category.Name = "rddl_category"
        Me.rddl_category.ReferenceFieldDesc = Nothing
        Me.rddl_category.ReferenceFieldName = Nothing
        Me.rddl_category.ReferenceTableName = Nothing
        Me.rddl_category.Size = New System.Drawing.Size(200, 18)
        Me.rddl_category.TabIndex = 12
        '
        'rbtnSave
        '
        Me.rbtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnSave.Location = New System.Drawing.Point(3, 492)
        Me.rbtnSave.Name = "rbtnSave"
        Me.rbtnSave.Size = New System.Drawing.Size(68, 18)
        Me.rbtnSave.TabIndex = 0
        Me.rbtnSave.Text = "Save"
        '
        'rbtnDelete
        '
        Me.rbtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnDelete.Location = New System.Drawing.Point(77, 492)
        Me.rbtnDelete.Name = "rbtnDelete"
        Me.rbtnDelete.Size = New System.Drawing.Size(68, 18)
        Me.rbtnDelete.TabIndex = 1
        Me.rbtnDelete.Text = "Delete"
        '
        'rbtnClose
        '
        Me.rbtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbtnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnClose.Location = New System.Drawing.Point(819, 492)
        Me.rbtnClose.Name = "rbtnClose"
        Me.rbtnClose.Size = New System.Drawing.Size(68, 18)
        Me.rbtnClose.TabIndex = 3
        Me.rbtnClose.Text = "Close"
        '
        'rbtnReset
        '
        Me.rbtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnReset.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources._new
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
        Me.rddl_route_offday.CalculationExpression = Nothing
        Me.rddl_route_offday.DropDownAnimationEnabled = True
        Me.rddl_route_offday.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.rddl_route_offday.FieldCode = Nothing
        Me.rddl_route_offday.FieldDesc = Nothing
        Me.rddl_route_offday.FieldMaxLength = 0
        Me.rddl_route_offday.FieldName = Nothing
        Me.rddl_route_offday.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rddl_route_offday.isCalculatedField = False
        Me.rddl_route_offday.IsSourceFromTable = False
        Me.rddl_route_offday.IsSourceFromValueList = False
        Me.rddl_route_offday.IsUnique = False
        Me.rddl_route_offday.Location = New System.Drawing.Point(146, 95)
        Me.rddl_route_offday.MendatroryField = False
        Me.rddl_route_offday.MyLinkLable1 = Me.rlblRouteOFFDay
        Me.rddl_route_offday.MyLinkLable2 = Nothing
        Me.rddl_route_offday.Name = "rddl_route_offday"
        Me.rddl_route_offday.ReferenceFieldDesc = Nothing
        Me.rddl_route_offday.ReferenceFieldName = Nothing
        Me.rddl_route_offday.ReferenceTableName = Nothing
        Me.rddl_route_offday.Size = New System.Drawing.Size(200, 18)
        Me.rddl_route_offday.TabIndex = 9
        '
        'ddltype
        '
        Me.ddltype.AutoCompleteDisplayMember = Nothing
        Me.ddltype.AutoCompleteValueMember = Nothing
        Me.ddltype.CalculationExpression = Nothing
        Me.ddltype.DropDownAnimationEnabled = True
        Me.ddltype.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddltype.FieldCode = Nothing
        Me.ddltype.FieldDesc = Nothing
        Me.ddltype.FieldMaxLength = 0
        Me.ddltype.FieldName = Nothing
        Me.ddltype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddltype.isCalculatedField = False
        Me.ddltype.IsSourceFromTable = False
        Me.ddltype.IsSourceFromValueList = False
        Me.ddltype.IsUnique = False
        Me.ddltype.Location = New System.Drawing.Point(146, 72)
        Me.ddltype.MendatroryField = False
        Me.ddltype.MyLinkLable1 = Me.rlblType
        Me.ddltype.MyLinkLable2 = Nothing
        Me.ddltype.Name = "ddltype"
        Me.ddltype.ReferenceFieldDesc = Nothing
        Me.ddltype.ReferenceFieldName = Nothing
        Me.ddltype.ReferenceTableName = Nothing
        Me.ddltype.Size = New System.Drawing.Size(200, 18)
        Me.ddltype.TabIndex = 7
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem_import, Me.RadMenuItem_Export, Me.RadMenuItem_Close})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem_import
        '
        Me.RadMenuItem_import.Name = "RadMenuItem_import"
        Me.RadMenuItem_import.Text = "Import"
        '
        'RadMenuItem_Export
        '
        Me.RadMenuItem_Export.Name = "RadMenuItem_Export"
        Me.RadMenuItem_Export.Text = "Export"
        '
        'RadMenuItem_Close
        '
        Me.RadMenuItem_Close.Name = "RadMenuItem_Close"
        Me.RadMenuItem_Close.Text = "Close"
        '
        'fndRoutePrice
        '
        Me.fndRoutePrice.CalculationExpression = Nothing
        Me.fndRoutePrice.FieldCode = Nothing
        Me.fndRoutePrice.FieldDesc = Nothing
        Me.fndRoutePrice.FieldMaxLength = 0
        Me.fndRoutePrice.FieldName = Nothing
        Me.fndRoutePrice.isCalculatedField = False
        Me.fndRoutePrice.IsSourceFromTable = False
        Me.fndRoutePrice.IsSourceFromValueList = False
        Me.fndRoutePrice.IsUnique = False
        Me.fndRoutePrice.Location = New System.Drawing.Point(146, 187)
        Me.fndRoutePrice.MendatroryField = False
        Me.fndRoutePrice.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRoutePrice.MyLinkLable1 = Me.MyLabel1
        Me.fndRoutePrice.MyLinkLable2 = Nothing
        Me.fndRoutePrice.MyReadOnly = False
        Me.fndRoutePrice.MyShowMasterFormButton = False
        Me.fndRoutePrice.Name = "fndRoutePrice"
        Me.fndRoutePrice.ReferenceFieldDesc = Nothing
        Me.fndRoutePrice.ReferenceFieldName = Nothing
        Me.fndRoutePrice.ReferenceTableName = Nothing
        Me.fndRoutePrice.Size = New System.Drawing.Size(200, 18)
        Me.fndRoutePrice.TabIndex = 17
        Me.fndRoutePrice.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 188)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(118, 16)
        Me.MyLabel1.TabIndex = 39
        Me.MyLabel1.Text = "Route Exc Price Code"
        '
        'txtRoutePrice
        '
        Me.txtRoutePrice.AutoSize = False
        Me.txtRoutePrice.CalculationExpression = Nothing
        Me.txtRoutePrice.FieldCode = Nothing
        Me.txtRoutePrice.FieldDesc = Nothing
        Me.txtRoutePrice.FieldMaxLength = 0
        Me.txtRoutePrice.FieldName = Nothing
        Me.txtRoutePrice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoutePrice.isCalculatedField = False
        Me.txtRoutePrice.IsSourceFromTable = False
        Me.txtRoutePrice.IsSourceFromValueList = False
        Me.txtRoutePrice.IsUnique = False
        Me.txtRoutePrice.Location = New System.Drawing.Point(361, 187)
        Me.txtRoutePrice.MaxLength = 60
        Me.txtRoutePrice.MendatroryField = False
        Me.txtRoutePrice.Multiline = True
        Me.txtRoutePrice.MyLinkLable1 = Nothing
        Me.txtRoutePrice.MyLinkLable2 = Nothing
        Me.txtRoutePrice.Name = "txtRoutePrice"
        Me.txtRoutePrice.ReadOnly = True
        Me.txtRoutePrice.ReferenceFieldDesc = Nothing
        Me.txtRoutePrice.ReferenceFieldName = Nothing
        Me.txtRoutePrice.ReferenceTableName = Nothing
        Me.txtRoutePrice.Size = New System.Drawing.Size(298, 18)
        Me.txtRoutePrice.TabIndex = 18
        Me.txtRoutePrice.TabStop = False
        '
        'dtpAcIn
        '
        Me.dtpAcIn.CalculationExpression = Nothing
        Me.dtpAcIn.CustomFormat = "dd/MM/yyyy"
        Me.dtpAcIn.FieldCode = Nothing
        Me.dtpAcIn.FieldDesc = Nothing
        Me.dtpAcIn.FieldMaxLength = 0
        Me.dtpAcIn.FieldName = Nothing
        Me.dtpAcIn.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAcIn.isCalculatedField = False
        Me.dtpAcIn.IsSourceFromTable = False
        Me.dtpAcIn.IsSourceFromValueList = False
        Me.dtpAcIn.IsUnique = False
        Me.dtpAcIn.Location = New System.Drawing.Point(461, 25)
        Me.dtpAcIn.MendatroryField = False
        Me.dtpAcIn.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAcIn.MyLinkLable1 = Nothing
        Me.dtpAcIn.MyLinkLable2 = Nothing
        Me.dtpAcIn.Name = "dtpAcIn"
        Me.dtpAcIn.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAcIn.ReferenceFieldDesc = Nothing
        Me.dtpAcIn.ReferenceFieldName = Nothing
        Me.dtpAcIn.ReferenceTableName = Nothing
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
        Me.fndvcode.CalculationExpression = Nothing
        Me.fndvcode.FieldCode = Nothing
        Me.fndvcode.FieldDesc = Nothing
        Me.fndvcode.FieldMaxLength = 0
        Me.fndvcode.FieldName = Nothing
        Me.fndvcode.isCalculatedField = False
        Me.fndvcode.IsSourceFromTable = False
        Me.fndvcode.IsSourceFromValueList = False
        Me.fndvcode.IsUnique = False
        Me.fndvcode.Location = New System.Drawing.Point(146, 210)
        Me.fndvcode.MendatroryField = False
        Me.fndvcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndvcode.MyLinkLable1 = Me.lblvechilecode
        Me.fndvcode.MyLinkLable2 = Nothing
        Me.fndvcode.MyReadOnly = False
        Me.fndvcode.MyShowMasterFormButton = False
        Me.fndvcode.Name = "fndvcode"
        Me.fndvcode.ReferenceFieldDesc = Nothing
        Me.fndvcode.ReferenceFieldName = Nothing
        Me.fndvcode.ReferenceTableName = Nothing
        Me.fndvcode.Size = New System.Drawing.Size(200, 18)
        Me.fndvcode.TabIndex = 19
        Me.fndvcode.Value = ""
        '
        'lblvechilecode
        '
        Me.lblvechilecode.FieldName = Nothing
        Me.lblvechilecode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvechilecode.Location = New System.Drawing.Point(12, 211)
        Me.lblvechilecode.Name = "lblvechilecode"
        Me.lblvechilecode.Size = New System.Drawing.Size(74, 16)
        Me.lblvechilecode.TabIndex = 33
        Me.lblvechilecode.Text = "Vehicle Code"
        '
        'fndnonprice
        '
        Me.fndnonprice.CalculationExpression = Nothing
        Me.fndnonprice.FieldCode = Nothing
        Me.fndnonprice.FieldDesc = Nothing
        Me.fndnonprice.FieldMaxLength = 0
        Me.fndnonprice.FieldName = Nothing
        Me.fndnonprice.isCalculatedField = False
        Me.fndnonprice.IsSourceFromTable = False
        Me.fndnonprice.IsSourceFromValueList = False
        Me.fndnonprice.IsUnique = False
        Me.fndnonprice.Location = New System.Drawing.Point(146, 164)
        Me.fndnonprice.MendatroryField = False
        Me.fndnonprice.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndnonprice.MyLinkLable1 = Me.RadLabel1
        Me.fndnonprice.MyLinkLable2 = Nothing
        Me.fndnonprice.MyReadOnly = False
        Me.fndnonprice.MyShowMasterFormButton = False
        Me.fndnonprice.Name = "fndnonprice"
        Me.fndnonprice.ReferenceFieldDesc = Nothing
        Me.fndnonprice.ReferenceFieldName = Nothing
        Me.fndnonprice.ReferenceTableName = Nothing
        Me.fndnonprice.Size = New System.Drawing.Size(200, 18)
        Me.fndnonprice.TabIndex = 15
        Me.fndnonprice.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(12, 165)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(108, 16)
        Me.RadLabel1.TabIndex = 30
        Me.RadLabel1.Text = "Non Exc Price Code"
        '
        'fndPriceCode
        '
        Me.fndPriceCode.CalculationExpression = Nothing
        Me.fndPriceCode.FieldCode = Nothing
        Me.fndPriceCode.FieldDesc = Nothing
        Me.fndPriceCode.FieldMaxLength = 0
        Me.fndPriceCode.FieldName = Nothing
        Me.fndPriceCode.isCalculatedField = False
        Me.fndPriceCode.IsSourceFromTable = False
        Me.fndPriceCode.IsSourceFromValueList = False
        Me.fndPriceCode.IsUnique = False
        Me.fndPriceCode.Location = New System.Drawing.Point(146, 141)
        Me.fndPriceCode.MendatroryField = False
        Me.fndPriceCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPriceCode.MyLinkLable1 = Me.rlblPriceCode
        Me.fndPriceCode.MyLinkLable2 = Nothing
        Me.fndPriceCode.MyReadOnly = False
        Me.fndPriceCode.MyShowMasterFormButton = False
        Me.fndPriceCode.Name = "fndPriceCode"
        Me.fndPriceCode.ReferenceFieldDesc = Nothing
        Me.fndPriceCode.ReferenceFieldName = Nothing
        Me.fndPriceCode.ReferenceTableName = Nothing
        Me.fndPriceCode.Size = New System.Drawing.Size(200, 18)
        Me.fndPriceCode.TabIndex = 13
        Me.fndPriceCode.Value = ""
        '
        'rlblPriceCode
        '
        Me.rlblPriceCode.FieldName = Nothing
        Me.rlblPriceCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblPriceCode.Location = New System.Drawing.Point(12, 142)
        Me.rlblPriceCode.Name = "rlblPriceCode"
        Me.rlblPriceCode.Size = New System.Drawing.Size(62, 16)
        Me.rlblPriceCode.TabIndex = 26
        Me.rlblPriceCode.Text = "Price Code"
        '
        'fndDepot
        '
        Me.fndDepot.CalculationExpression = Nothing
        Me.fndDepot.FieldCode = Nothing
        Me.fndDepot.FieldDesc = Nothing
        Me.fndDepot.FieldMaxLength = 0
        Me.fndDepot.FieldName = Nothing
        Me.fndDepot.isCalculatedField = False
        Me.fndDepot.IsSourceFromTable = False
        Me.fndDepot.IsSourceFromValueList = False
        Me.fndDepot.IsUnique = False
        Me.fndDepot.Location = New System.Drawing.Point(461, 254)
        Me.fndDepot.MendatroryField = False
        Me.fndDepot.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDepot.MyLinkLable1 = Me.lblDepot
        Me.fndDepot.MyLinkLable2 = Nothing
        Me.fndDepot.MyReadOnly = False
        Me.fndDepot.MyShowMasterFormButton = False
        Me.fndDepot.Name = "fndDepot"
        Me.fndDepot.ReferenceFieldDesc = Nothing
        Me.fndDepot.ReferenceFieldName = Nothing
        Me.fndDepot.ReferenceTableName = Nothing
        Me.fndDepot.Size = New System.Drawing.Size(196, 19)
        Me.fndDepot.TabIndex = 23
        Me.fndDepot.Value = ""
        '
        'lblDepot
        '
        Me.lblDepot.FieldName = Nothing
        Me.lblDepot.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepot.Location = New System.Drawing.Point(363, 253)
        Me.lblDepot.Name = "lblDepot"
        Me.lblDepot.Size = New System.Drawing.Size(36, 16)
        Me.lblDepot.TabIndex = 22
        Me.lblDepot.Text = "Depot"
        '
        'fndcity_id
        '
        Me.fndcity_id.CalculationExpression = Nothing
        Me.fndcity_id.FieldCode = Nothing
        Me.fndcity_id.FieldDesc = Nothing
        Me.fndcity_id.FieldMaxLength = 0
        Me.fndcity_id.FieldName = Nothing
        Me.fndcity_id.isCalculatedField = False
        Me.fndcity_id.IsSourceFromTable = False
        Me.fndcity_id.IsSourceFromValueList = False
        Me.fndcity_id.IsUnique = False
        Me.fndcity_id.Location = New System.Drawing.Point(146, 118)
        Me.fndcity_id.MendatroryField = False
        Me.fndcity_id.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcity_id.MyLinkLable1 = Me.rlblCityID
        Me.fndcity_id.MyLinkLable2 = Nothing
        Me.fndcity_id.MyReadOnly = False
        Me.fndcity_id.MyShowMasterFormButton = False
        Me.fndcity_id.Name = "fndcity_id"
        Me.fndcity_id.ReferenceFieldDesc = Nothing
        Me.fndcity_id.ReferenceFieldName = Nothing
        Me.fndcity_id.ReferenceTableName = Nothing
        Me.fndcity_id.Size = New System.Drawing.Size(200, 18)
        Me.fndcity_id.TabIndex = 11
        Me.fndcity_id.Value = ""
        '
        'fndRouteid
        '
        Me.fndRouteid.FieldName = Nothing
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
        Me.txtnonprice.CalculationExpression = Nothing
        Me.txtnonprice.FieldCode = Nothing
        Me.txtnonprice.FieldDesc = Nothing
        Me.txtnonprice.FieldMaxLength = 0
        Me.txtnonprice.FieldName = Nothing
        Me.txtnonprice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnonprice.isCalculatedField = False
        Me.txtnonprice.IsSourceFromTable = False
        Me.txtnonprice.IsSourceFromValueList = False
        Me.txtnonprice.IsUnique = False
        Me.txtnonprice.Location = New System.Drawing.Point(361, 164)
        Me.txtnonprice.MaxLength = 60
        Me.txtnonprice.MendatroryField = False
        Me.txtnonprice.Multiline = True
        Me.txtnonprice.MyLinkLable1 = Nothing
        Me.txtnonprice.MyLinkLable2 = Nothing
        Me.txtnonprice.Name = "txtnonprice"
        Me.txtnonprice.ReadOnly = True
        Me.txtnonprice.ReferenceFieldDesc = Nothing
        Me.txtnonprice.ReferenceFieldName = Nothing
        Me.txtnonprice.ReferenceTableName = Nothing
        Me.txtnonprice.Size = New System.Drawing.Size(298, 18)
        Me.txtnonprice.TabIndex = 16
        Me.txtnonprice.TabStop = False
        '
        'txtvcodedesc
        '
        Me.txtvcodedesc.AutoSize = False
        Me.txtvcodedesc.CalculationExpression = Nothing
        Me.txtvcodedesc.FieldCode = Nothing
        Me.txtvcodedesc.FieldDesc = Nothing
        Me.txtvcodedesc.FieldMaxLength = 0
        Me.txtvcodedesc.FieldName = Nothing
        Me.txtvcodedesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtvcodedesc.isCalculatedField = False
        Me.txtvcodedesc.IsSourceFromTable = False
        Me.txtvcodedesc.IsSourceFromValueList = False
        Me.txtvcodedesc.IsUnique = False
        Me.txtvcodedesc.Location = New System.Drawing.Point(361, 210)
        Me.txtvcodedesc.MaxLength = 60
        Me.txtvcodedesc.MendatroryField = False
        Me.txtvcodedesc.Multiline = True
        Me.txtvcodedesc.MyLinkLable1 = Nothing
        Me.txtvcodedesc.MyLinkLable2 = Nothing
        Me.txtvcodedesc.Name = "txtvcodedesc"
        Me.txtvcodedesc.ReadOnly = True
        Me.txtvcodedesc.ReferenceFieldDesc = Nothing
        Me.txtvcodedesc.ReferenceFieldName = Nothing
        Me.txtvcodedesc.ReferenceTableName = Nothing
        Me.txtvcodedesc.Size = New System.Drawing.Size(298, 18)
        Me.txtvcodedesc.TabIndex = 20
        Me.txtvcodedesc.TabStop = False
        '
        'txtpricecodedescription
        '
        Me.txtpricecodedescription.AutoSize = False
        Me.txtpricecodedescription.CalculationExpression = Nothing
        Me.txtpricecodedescription.FieldCode = Nothing
        Me.txtpricecodedescription.FieldDesc = Nothing
        Me.txtpricecodedescription.FieldMaxLength = 0
        Me.txtpricecodedescription.FieldName = Nothing
        Me.txtpricecodedescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpricecodedescription.isCalculatedField = False
        Me.txtpricecodedescription.IsSourceFromTable = False
        Me.txtpricecodedescription.IsSourceFromValueList = False
        Me.txtpricecodedescription.IsUnique = False
        Me.txtpricecodedescription.Location = New System.Drawing.Point(361, 141)
        Me.txtpricecodedescription.MaxLength = 60
        Me.txtpricecodedescription.MendatroryField = False
        Me.txtpricecodedescription.Multiline = True
        Me.txtpricecodedescription.MyLinkLable1 = Nothing
        Me.txtpricecodedescription.MyLinkLable2 = Nothing
        Me.txtpricecodedescription.Name = "txtpricecodedescription"
        Me.txtpricecodedescription.ReadOnly = True
        Me.txtpricecodedescription.ReferenceFieldDesc = Nothing
        Me.txtpricecodedescription.ReferenceFieldName = Nothing
        Me.txtpricecodedescription.ReferenceTableName = Nothing
        Me.txtpricecodedescription.Size = New System.Drawing.Size(298, 18)
        Me.txtpricecodedescription.TabIndex = 14
        Me.txtpricecodedescription.TabStop = False
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprint.Location = New System.Drawing.Point(151, 492)
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
        Me.RadMenu1.Size = New System.Drawing.Size(887, 20)
        Me.RadMenu1.TabIndex = 0
        '
        'txtDistance
        '
        Me.txtDistance.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtDistance.CalculationExpression = Nothing
        Me.txtDistance.DecimalPlaces = 3
        Me.txtDistance.FieldCode = Nothing
        Me.txtDistance.FieldDesc = Nothing
        Me.txtDistance.FieldMaxLength = 0
        Me.txtDistance.FieldName = Nothing
        Me.txtDistance.isCalculatedField = False
        Me.txtDistance.IsSourceFromTable = False
        Me.txtDistance.IsSourceFromValueList = False
        Me.txtDistance.IsUnique = False
        Me.txtDistance.Location = New System.Drawing.Point(146, 277)
        Me.txtDistance.MendatroryField = False
        Me.txtDistance.MyLinkLable1 = Nothing
        Me.txtDistance.MyLinkLable2 = Nothing
        Me.txtDistance.Name = "txtDistance"
        Me.txtDistance.ReferenceFieldDesc = Nothing
        Me.txtDistance.ReferenceFieldName = Nothing
        Me.txtDistance.ReferenceTableName = Nothing
        Me.txtDistance.Size = New System.Drawing.Size(201, 20)
        Me.txtDistance.TabIndex = 41
        Me.txtDistance.Text = "0"
        Me.txtDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDistance.Value = 0R
        '
        'btnDistance
        '
        Me.btnDistance.FieldName = Nothing
        Me.btnDistance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDistance.Location = New System.Drawing.Point(12, 279)
        Me.btnDistance.Name = "btnDistance"
        Me.btnDistance.Size = New System.Drawing.Size(81, 16)
        Me.btnDistance.TabIndex = 86
        Me.btnDistance.Text = "Distance(K.M.)"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(361, 27)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel2.TabIndex = 87
        Me.MyLabel2.Text = "Route Date"
        '
        'fnd_saleman_code
        '
        Me.fnd_saleman_code.arrDispalyMember = Nothing
        Me.fnd_saleman_code.arrValueMember = Nothing
        Me.fnd_saleman_code.Location = New System.Drawing.Point(459, 72)
        Me.fnd_saleman_code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fnd_saleman_code.MyLinkLable1 = Me.rlblSalesmanCode
        Me.fnd_saleman_code.MyLinkLable2 = Nothing
        Me.fnd_saleman_code.MyNullText = "All"
        Me.fnd_saleman_code.Name = "fnd_saleman_code"
        Me.fnd_saleman_code.Size = New System.Drawing.Size(200, 19)
        Me.fnd_saleman_code.TabIndex = 354
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(12, 304)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel3.TabIndex = 355
        Me.MyLabel3.Text = "Time"
        '
        'txtRouteTime
        '
        Me.txtRouteTime.CustomFormat = "hh:mm tt"
        Me.txtRouteTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtRouteTime.Location = New System.Drawing.Point(146, 302)
        Me.txtRouteTime.Name = "txtRouteTime"
        Me.txtRouteTime.ShowCheckBox = True
        Me.txtRouteTime.ShowUpDown = True
        Me.txtRouteTime.Size = New System.Drawing.Size(109, 20)
        Me.txtRouteTime.TabIndex = 357
        Me.txtRouteTime.TabStop = False
        Me.txtRouteTime.Text = "02:08 PM"
        Me.txtRouteTime.Value = New Date(2018, 12, 11, 14, 8, 55, 115)
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.dgv)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(10, 351)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(870, 136)
        Me.RadGroupBox1.TabIndex = 358
        '
        'dgv
        '
        Me.dgv.BackColor = System.Drawing.Color.Transparent
        Me.dgv.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgv.ForeColor = System.Drawing.Color.Black
        Me.dgv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgv.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.dgv.MasterTemplate.AllowAddNewRow = False
        Me.dgv.MasterTemplate.EnableFiltering = True
        Me.dgv.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.dgv.MasterTemplate.ShowHeaderCellButtons = True
        Me.dgv.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.dgv.MyStopExport = False
        Me.dgv.Name = "dgv"
        Me.dgv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgv.ShowGroupPanel = False
        Me.dgv.ShowHeaderCellButtons = True
        Me.dgv.Size = New System.Drawing.Size(850, 106)
        Me.dgv.TabIndex = 1
        Me.dgv.TabStop = False
        '
        'txtTollAmount
        '
        Me.txtTollAmount.BackColor = System.Drawing.Color.White
        Me.txtTollAmount.CalculationExpression = Nothing
        Me.txtTollAmount.DecimalPlaces = 2
        Me.txtTollAmount.FieldCode = Nothing
        Me.txtTollAmount.FieldDesc = Nothing
        Me.txtTollAmount.FieldMaxLength = 5
        Me.txtTollAmount.FieldName = Nothing
        Me.txtTollAmount.isCalculatedField = False
        Me.txtTollAmount.IsSourceFromTable = False
        Me.txtTollAmount.IsSourceFromValueList = False
        Me.txtTollAmount.IsUnique = False
        Me.txtTollAmount.Location = New System.Drawing.Point(461, 277)
        Me.txtTollAmount.MendatroryField = False
        Me.txtTollAmount.MyLinkLable1 = Nothing
        Me.txtTollAmount.MyLinkLable2 = Nothing
        Me.txtTollAmount.Name = "txtTollAmount"
        Me.txtTollAmount.ReferenceFieldDesc = Nothing
        Me.txtTollAmount.ReferenceFieldName = Nothing
        Me.txtTollAmount.ReferenceTableName = Nothing
        Me.txtTollAmount.Size = New System.Drawing.Size(196, 20)
        Me.txtTollAmount.TabIndex = 360
        Me.txtTollAmount.Text = "0"
        Me.txtTollAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTollAmount.Value = 0R
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(363, 277)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel4.TabIndex = 361
        Me.MyLabel4.Text = "TOLL Cost"
        '
        'chkIsEarlyRoute
        '
        Me.chkIsEarlyRoute.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsEarlyRoute.Location = New System.Drawing.Point(261, 304)
        Me.chkIsEarlyRoute.Name = "chkIsEarlyRoute"
        Me.chkIsEarlyRoute.Size = New System.Drawing.Size(79, 16)
        Me.chkIsEarlyRoute.TabIndex = 1394
        Me.chkIsEarlyRoute.Text = "Early Route"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(361, 304)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(93, 16)
        Me.MyLabel5.TabIndex = 1395
        Me.MyLabel5.Text = "Mor Cut Off Time"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(361, 329)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(93, 16)
        Me.MyLabel6.TabIndex = 1396
        Me.MyLabel6.Text = "Eve Cut Off Time"
        '
        'txtMorningCOT
        '
        Me.txtMorningCOT.CustomFormat = "hh:mm tt"
        Me.txtMorningCOT.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtMorningCOT.Location = New System.Drawing.Point(461, 302)
        Me.txtMorningCOT.Name = "txtMorningCOT"
        Me.txtMorningCOT.ShowCheckBox = True
        Me.txtMorningCOT.ShowUpDown = True
        Me.txtMorningCOT.Size = New System.Drawing.Size(109, 20)
        Me.txtMorningCOT.TabIndex = 1397
        Me.txtMorningCOT.TabStop = False
        Me.txtMorningCOT.Text = "02:08 PM"
        Me.txtMorningCOT.Value = New Date(2018, 12, 11, 14, 8, 55, 115)
        '
        'txtEveningCOT
        '
        Me.txtEveningCOT.CustomFormat = "hh:mm tt"
        Me.txtEveningCOT.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtEveningCOT.Location = New System.Drawing.Point(461, 327)
        Me.txtEveningCOT.Name = "txtEveningCOT"
        Me.txtEveningCOT.ShowCheckBox = True
        Me.txtEveningCOT.ShowUpDown = True
        Me.txtEveningCOT.Size = New System.Drawing.Size(109, 20)
        Me.txtEveningCOT.TabIndex = 1398
        Me.txtEveningCOT.TabStop = False
        Me.txtEveningCOT.Text = "02:08 PM"
        Me.txtEveningCOT.Value = New Date(2018, 12, 11, 14, 8, 55, 115)
        '
        'txtSeqNo
        '
        Me.txtSeqNo.BackColor = System.Drawing.Color.White
        Me.txtSeqNo.CalculationExpression = Nothing
        Me.txtSeqNo.DecimalPlaces = 2
        Me.txtSeqNo.FieldCode = Nothing
        Me.txtSeqNo.FieldDesc = Nothing
        Me.txtSeqNo.FieldMaxLength = 0
        Me.txtSeqNo.FieldName = Nothing
        Me.txtSeqNo.isCalculatedField = False
        Me.txtSeqNo.IsSourceFromTable = False
        Me.txtSeqNo.IsSourceFromValueList = False
        Me.txtSeqNo.IsUnique = False
        Me.txtSeqNo.Location = New System.Drawing.Point(146, 327)
        Me.txtSeqNo.MendatroryField = False
        Me.txtSeqNo.MyLinkLable1 = Me.MyLabel24
        Me.txtSeqNo.MyLinkLable2 = Nothing
        Me.txtSeqNo.Name = "txtSeqNo"
        Me.txtSeqNo.ReferenceFieldDesc = Nothing
        Me.txtSeqNo.ReferenceFieldName = Nothing
        Me.txtSeqNo.ReferenceTableName = Nothing
        Me.txtSeqNo.Size = New System.Drawing.Size(56, 20)
        Me.txtSeqNo.TabIndex = 1400
        Me.txtSeqNo.Text = "0"
        Me.txtSeqNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSeqNo.Value = 0R
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Location = New System.Drawing.Point(12, 328)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(43, 18)
        Me.MyLabel24.TabIndex = 1399
        Me.MyLabel24.Text = "Seq No"
        '
        'cboEntryUOM
        '
        Me.cboEntryUOM.AutoCompleteDisplayMember = Nothing
        Me.cboEntryUOM.AutoCompleteValueMember = Nothing
        Me.cboEntryUOM.CalculationExpression = Nothing
        Me.cboEntryUOM.DropDownAnimationEnabled = True
        Me.cboEntryUOM.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboEntryUOM.FieldCode = Nothing
        Me.cboEntryUOM.FieldDesc = Nothing
        Me.cboEntryUOM.FieldMaxLength = 0
        Me.cboEntryUOM.FieldName = Nothing
        Me.cboEntryUOM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEntryUOM.isCalculatedField = False
        Me.cboEntryUOM.IsSourceFromTable = False
        Me.cboEntryUOM.IsSourceFromValueList = False
        Me.cboEntryUOM.IsUnique = False
        Me.cboEntryUOM.Location = New System.Drawing.Point(459, 118)
        Me.cboEntryUOM.MendatroryField = False
        Me.cboEntryUOM.MyLinkLable1 = Me.MyLabel7
        Me.cboEntryUOM.MyLinkLable2 = Nothing
        Me.cboEntryUOM.Name = "cboEntryUOM"
        Me.cboEntryUOM.ReferenceFieldDesc = Nothing
        Me.cboEntryUOM.ReferenceFieldName = Nothing
        Me.cboEntryUOM.ReferenceTableName = Nothing
        Me.cboEntryUOM.Size = New System.Drawing.Size(200, 18)
        Me.cboEntryUOM.TabIndex = 1401
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(361, 119)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel7.TabIndex = 1402
        Me.MyLabel7.Text = "Entry UOM"
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
        Me.txtLocation.Location = New System.Drawing.Point(146, 232)
        Me.txtLocation.MendatroryField = False
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblLocation
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(200, 18)
        Me.txtLocation.TabIndex = 1403
        Me.txtLocation.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(12, 233)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(79, 16)
        Me.lblLocation.TabIndex = 1405
        Me.lblLocation.Text = "Location Code"
        '
        'txtLocationDesc
        '
        Me.txtLocationDesc.AutoSize = False
        Me.txtLocationDesc.CalculationExpression = Nothing
        Me.txtLocationDesc.FieldCode = Nothing
        Me.txtLocationDesc.FieldDesc = Nothing
        Me.txtLocationDesc.FieldMaxLength = 0
        Me.txtLocationDesc.FieldName = Nothing
        Me.txtLocationDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationDesc.isCalculatedField = False
        Me.txtLocationDesc.IsSourceFromTable = False
        Me.txtLocationDesc.IsSourceFromValueList = False
        Me.txtLocationDesc.IsUnique = False
        Me.txtLocationDesc.Location = New System.Drawing.Point(361, 232)
        Me.txtLocationDesc.MaxLength = 60
        Me.txtLocationDesc.MendatroryField = False
        Me.txtLocationDesc.Multiline = True
        Me.txtLocationDesc.MyLinkLable1 = Nothing
        Me.txtLocationDesc.MyLinkLable2 = Nothing
        Me.txtLocationDesc.Name = "txtLocationDesc"
        Me.txtLocationDesc.ReadOnly = True
        Me.txtLocationDesc.ReferenceFieldDesc = Nothing
        Me.txtLocationDesc.ReferenceFieldName = Nothing
        Me.txtLocationDesc.ReferenceTableName = Nothing
        Me.txtLocationDesc.Size = New System.Drawing.Size(298, 18)
        Me.txtLocationDesc.TabIndex = 1404
        Me.txtLocationDesc.TabStop = False
        '
        'btnStatus
        '
        Me.btnStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStatus.Location = New System.Drawing.Point(243, 492)
        Me.btnStatus.Name = "btnStatus"
        Me.btnStatus.Size = New System.Drawing.Size(68, 18)
        Me.btnStatus.TabIndex = 1406
        Me.btnStatus.Text = "Status"
        '
        'fndZone
        '
        Me.fndZone.CalculationExpression = Nothing
        Me.fndZone.FieldCode = Nothing
        Me.fndZone.FieldDesc = Nothing
        Me.fndZone.FieldMaxLength = 0
        Me.fndZone.FieldName = Nothing
        Me.fndZone.isCalculatedField = False
        Me.fndZone.IsSourceFromTable = False
        Me.fndZone.IsSourceFromValueList = False
        Me.fndZone.IsUnique = False
        Me.fndZone.Location = New System.Drawing.Point(715, 47)
        Me.fndZone.MendatroryField = False
        Me.fndZone.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndZone.MyLinkLable1 = Me.MyLabel8
        Me.fndZone.MyLinkLable2 = Nothing
        Me.fndZone.MyReadOnly = False
        Me.fndZone.MyShowMasterFormButton = False
        Me.fndZone.Name = "fndZone"
        Me.fndZone.ReferenceFieldDesc = Nothing
        Me.fndZone.ReferenceFieldName = Nothing
        Me.fndZone.ReferenceTableName = Nothing
        Me.fndZone.Size = New System.Drawing.Size(167, 19)
        Me.fndZone.TabIndex = 1408
        Me.fndZone.Value = ""
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(665, 50)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel8.TabIndex = 1407
        Me.MyLabel8.Text = "Zone"
        '
        'frmRouteMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(887, 511)
        Me.Controls.Add(Me.fndZone)
        Me.Controls.Add(Me.MyLabel8)
        Me.Controls.Add(Me.btnStatus)
        Me.Controls.Add(Me.txtLocation)
        Me.Controls.Add(Me.txtLocationDesc)
        Me.Controls.Add(Me.lblLocation)
        Me.Controls.Add(Me.cboEntryUOM)
        Me.Controls.Add(Me.MyLabel7)
        Me.Controls.Add(Me.txtSeqNo)
        Me.Controls.Add(Me.MyLabel24)
        Me.Controls.Add(Me.txtEveningCOT)
        Me.Controls.Add(Me.txtMorningCOT)
        Me.Controls.Add(Me.MyLabel6)
        Me.Controls.Add(Me.MyLabel5)
        Me.Controls.Add(Me.chkIsEarlyRoute)
        Me.Controls.Add(Me.MyLabel4)
        Me.Controls.Add(Me.txtTollAmount)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Controls.Add(Me.txtRouteTime)
        Me.Controls.Add(Me.MyLabel3)
        Me.Controls.Add(Me.fnd_saleman_code)
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
        Me.Controls.Add(Me.rddl_route_offday)
        Me.Controls.Add(Me.fndRouteid)
        Me.Controls.Add(Me.rbtnReset)
        Me.Controls.Add(Me.txtnonprice)
        Me.Controls.Add(Me.rlblRouteOFFDay)
        Me.Controls.Add(Me.txtvcodedesc)
        Me.Controls.Add(Me.RadLabel1)
        Me.Controls.Add(Me.rlblSalesmanCode)
        Me.Controls.Add(Me.lblvechilecode)
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
        CType(Me.txtpricecodedescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDistance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDistance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRouteTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.dgv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTollAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsEarlyRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMorningCOT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEveningCOT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSeqNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboEntryUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents ddltype As common.Controls.MyComboBox
    Friend WithEvents ToolTipGP_Route_Master As System.Windows.Forms.ToolTip
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem_import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem_Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem_Close As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents txtpricecodedescription As common.Controls.MyTextBox
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
    Friend WithEvents rlblPriceCode As common.Controls.MyLabel
    Friend WithEvents lblDepot As common.Controls.MyLabel
    Friend WithEvents lblvechilecode As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents fndRouteid As common.UserControls.txtNavigator
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
    Friend WithEvents fnd_saleman_code As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtRouteTime As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dgv As common.UserControls.MyRadGridView
    Friend WithEvents txtTollAmount As common.MyNumBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents chkIsEarlyRoute As RadCheckBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtMorningCOT As RadDateTimePicker
    Friend WithEvents txtEveningCOT As RadDateTimePicker
    Friend WithEvents txtSeqNo As common.MyNumBox
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents cboEntryUOM As common.Controls.MyComboBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocationDesc As common.Controls.MyTextBox
    Friend WithEvents btnStatus As RadButton
    Friend WithEvents fndZone As common.UserControls.txtFinder
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
End Class

