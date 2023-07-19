<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUserMaster
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.lblUserCode = New common.Controls.MyLabel()
        Me.lblUserType = New common.Controls.MyLabel()
        Me.lbl1 = New common.Controls.MyLabel()
        Me.lbl2 = New common.Controls.MyLabel()
        Me.lblLabel3 = New common.Controls.MyLabel()
        Me.lblLabel4 = New common.Controls.MyLabel()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.ddlUserType = New common.Controls.MyComboBox()
        Me.lblPassword = New common.Controls.MyLabel()
        Me.txtPassword = New common.Controls.MyTextBox()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.munuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImportCustomerMapping = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExportBlankSheetZone = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExportBlankSheetCustomerCategory = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImportZone = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImportCustomerCategory = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkHRAdmin = New common.Controls.MyCheckBox()
        Me.lblMP = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtMP = New common.UserControls.txtFinder()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.CboAppUserType = New Telerik.WinControls.UI.RadDropDownList()
        Me.chkLicenceReserved = New common.Controls.MyCheckBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.dtInActive = New common.Controls.MyDateTimePicker()
        Me.RadLabel18 = New common.Controls.MyLabel()
        Me.chkInActive = New Telerik.WinControls.UI.RadCheckBox()
        Me.ChkDepartmentHead = New common.Controls.MyCheckBox()
        Me.mulCustomerCategory = New common.UserControls.txtMultiSelectFinder()
        Me.mulZone = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txt_Mob_No = New common.MyNumBox()
        Me.lblMobileNo = New common.Controls.MyLabel()
        Me.ChkViewMilkReceiptAndSample = New common.Controls.MyCheckBox()
        Me.lblDepartmentName = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.fndDepartment = New common.UserControls.txtFinder()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.CmbAppUserSaleType = New Telerik.WinControls.UI.RadDropDownList()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.lblDisRetailer = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.lblRetailerCode = New common.Controls.MyLabel()
        Me.fndDisRetailerCode = New common.UserControls.txtFinder()
        Me.lblCustCode = New common.Controls.MyLabel()
        Me.fndCustCode = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.CmbLoginType = New Telerik.WinControls.UI.RadDropDownList()
        Me.lblEmployeeFinder = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.EmployeeFinder = New common.UserControls.txtFinder()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.lblVendor = New common.Controls.MyLabel()
        Me.fndVendor = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtEmailId = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtDefaultLocation = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.cmbLevel = New Telerik.WinControls.UI.RadDropDownList()
        Me.lblReportingUserName = New common.Controls.MyLabel()
        Me.lblReportingPerson = New common.Controls.MyLabel()
        Me.txtUserName = New common.Controls.MyTextBox()
        Me.fndUserCode = New common.UserControls.txtNavigator()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.fndLabel4 = New common.UserControls.txtFinder()
        Me.fndEmployeeCode = New common.UserControls.txtFinder()
        Me.lblEmployeeCode = New common.Controls.MyLabel()
        Me.txtEmployeeName = New common.Controls.MyTextBox()
        Me.lblEmployeeName = New common.Controls.MyLabel()
        Me.fndLabel3 = New common.UserControls.txtFinder()
        Me.fndLabel1 = New common.UserControls.txtFinder()
        Me.fndLabel2 = New common.UserControls.txtFinder()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GBRoute = New System.Windows.Forms.GroupBox()
        Me.txtRoute = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.lblLength = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvCustomer = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvUser = New common.UserControls.MyRadGridView()
        Me.btnGetHistory = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.cboEntryUOM = New Telerik.WinControls.UI.RadDropDownList()
        Me.MyLabel12 = New common.Controls.MyLabel()
        CType(Me.lblUserCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUserType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlUserType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPassword, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPassword, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkHRAdmin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboAppUserType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLicenceReserved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.dtInActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkDepartmentHead, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Mob_No, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMobileNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkViewMilkReceiptAndSample, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartmentName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.CmbAppUserSaleType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDisRetailer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRetailerCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbLoginType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmployeeFinder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmailId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReportingUserName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReportingPerson, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUserName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmployeeCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmployeeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmployeeName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.GBRoute.SuspendLayout()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLength, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gvCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gvUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvUser.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGetHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboEntryUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblUserCode
        '
        Me.lblUserCode.FieldName = Nothing
        Me.lblUserCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserCode.Location = New System.Drawing.Point(15, 6)
        Me.lblUserCode.Name = "lblUserCode"
        Me.lblUserCode.Size = New System.Drawing.Size(60, 16)
        Me.lblUserCode.TabIndex = 10
        Me.lblUserCode.Text = "User Code"
        '
        'lblUserType
        '
        Me.lblUserType.FieldName = Nothing
        Me.lblUserType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserType.Location = New System.Drawing.Point(92, 155)
        Me.lblUserType.Name = "lblUserType"
        Me.lblUserType.Size = New System.Drawing.Size(58, 16)
        Me.lblUserType.TabIndex = 1
        Me.lblUserType.Text = "User Type"
        Me.lblUserType.Visible = False
        '
        'lbl1
        '
        Me.lbl1.FieldName = Nothing
        Me.lbl1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl1.Location = New System.Drawing.Point(92, 190)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(40, 16)
        Me.lbl1.TabIndex = 3
        Me.lbl1.Text = "Level1"
        Me.lbl1.Visible = False
        '
        'lbl2
        '
        Me.lbl2.FieldName = Nothing
        Me.lbl2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl2.Location = New System.Drawing.Point(280, 190)
        Me.lbl2.Name = "lbl2"
        Me.lbl2.Size = New System.Drawing.Size(40, 16)
        Me.lbl2.TabIndex = 8
        Me.lbl2.Text = "Level2"
        Me.lbl2.Visible = False
        '
        'lblLabel3
        '
        Me.lblLabel3.FieldName = Nothing
        Me.lblLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLabel3.Location = New System.Drawing.Point(92, 221)
        Me.lblLabel3.Name = "lblLabel3"
        Me.lblLabel3.Size = New System.Drawing.Size(40, 16)
        Me.lblLabel3.TabIndex = 5
        Me.lblLabel3.Text = "Level3"
        Me.lblLabel3.Visible = False
        '
        'lblLabel4
        '
        Me.lblLabel4.FieldName = Nothing
        Me.lblLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLabel4.Location = New System.Drawing.Point(282, 219)
        Me.lblLabel4.Name = "lblLabel4"
        Me.lblLabel4.Size = New System.Drawing.Size(40, 16)
        Me.lblLabel4.TabIndex = 9
        Me.lblLabel4.Text = "Level4"
        Me.lblLabel4.Visible = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 10)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 23)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(73, 10)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 23)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(648, 10)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 23)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'ddlUserType
        '
        Me.ddlUserType.AutoCompleteDisplayMember = Nothing
        Me.ddlUserType.AutoCompleteValueMember = Nothing
        Me.ddlUserType.CalculationExpression = Nothing
        Me.ddlUserType.DropDownAnimationEnabled = True
        Me.ddlUserType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlUserType.FieldCode = Nothing
        Me.ddlUserType.FieldDesc = Nothing
        Me.ddlUserType.FieldMaxLength = 0
        Me.ddlUserType.FieldName = Nothing
        Me.ddlUserType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlUserType.isCalculatedField = False
        Me.ddlUserType.IsSourceFromTable = False
        Me.ddlUserType.IsSourceFromValueList = False
        Me.ddlUserType.IsUnique = False
        RadListDataItem1.Text = "Level1"
        RadListDataItem2.Text = "Level2"
        RadListDataItem3.Text = "Level3"
        RadListDataItem4.Text = "Level4"
        RadListDataItem5.Text = "Level5"
        Me.ddlUserType.Items.Add(RadListDataItem1)
        Me.ddlUserType.Items.Add(RadListDataItem2)
        Me.ddlUserType.Items.Add(RadListDataItem3)
        Me.ddlUserType.Items.Add(RadListDataItem4)
        Me.ddlUserType.Items.Add(RadListDataItem5)
        Me.ddlUserType.Location = New System.Drawing.Point(192, 154)
        Me.ddlUserType.MendatroryField = False
        Me.ddlUserType.MyLinkLable1 = Nothing
        Me.ddlUserType.MyLinkLable2 = Nothing
        Me.ddlUserType.Name = "ddlUserType"
        Me.ddlUserType.ReferenceFieldDesc = Nothing
        Me.ddlUserType.ReferenceFieldName = Nothing
        Me.ddlUserType.ReferenceTableName = Nothing
        Me.ddlUserType.Size = New System.Drawing.Size(160, 18)
        Me.ddlUserType.TabIndex = 2
        Me.ddlUserType.Text = "----Select----"
        Me.ddlUserType.Visible = False
        '
        'lblPassword
        '
        Me.lblPassword.FieldName = Nothing
        Me.lblPassword.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPassword.Location = New System.Drawing.Point(15, 29)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(56, 16)
        Me.lblPassword.TabIndex = 8
        Me.lblPassword.Text = "Password"
        '
        'txtPassword
        '
        Me.txtPassword.CalculationExpression = Nothing
        Me.txtPassword.FieldCode = Nothing
        Me.txtPassword.FieldDesc = Nothing
        Me.txtPassword.FieldMaxLength = 0
        Me.txtPassword.FieldName = Nothing
        Me.txtPassword.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.isCalculatedField = False
        Me.txtPassword.IsSourceFromTable = False
        Me.txtPassword.IsSourceFromValueList = False
        Me.txtPassword.IsUnique = False
        Me.txtPassword.Location = New System.Drawing.Point(104, 28)
        Me.txtPassword.MaxLength = 20
        Me.txtPassword.MendatroryField = False
        Me.txtPassword.MyLinkLable1 = Me.lblPassword
        Me.txtPassword.MyLinkLable2 = Nothing
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.ReferenceFieldDesc = Nothing
        Me.txtPassword.ReferenceFieldName = Nothing
        Me.txtPassword.ReferenceTableName = Nothing
        Me.txtPassword.Size = New System.Drawing.Size(221, 18)
        Me.txtPassword.TabIndex = 3
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.munuExport})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(719, 20)
        Me.RadMenu1.TabIndex = 1
        '
        'munuExport
        '
        Me.munuExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.menuImport, Me.menuExport, Me.menuClose, Me.RadMenuItem1, Me.rmImportCustomerMapping, Me.rmExportBlankSheetZone, Me.rmExportBlankSheetCustomerCategory, Me.rmImportZone, Me.rmImportCustomerCategory})
        Me.munuExport.Name = "munuExport"
        Me.munuExport.Text = "File"
        '
        'menuImport
        '
        Me.menuImport.Name = "menuImport"
        Me.menuImport.Text = "Import.."
        '
        'menuExport
        '
        Me.menuExport.AccessibleDescription = "RadMenuItem3"
        Me.menuExport.AccessibleName = "RadMenuItem3"
        Me.menuExport.Name = "menuExport"
        Me.menuExport.Text = "Export.."
        '
        'menuClose
        '
        Me.menuClose.AccessibleDescription = "RadMenuItem4"
        Me.menuClose.AccessibleName = "RadMenuItem4"
        Me.menuClose.Name = "menuClose"
        Me.menuClose.Text = "Close"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "rmCustomerMappingExport"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Export Customer Mapping"
        '
        'rmImportCustomerMapping
        '
        Me.rmImportCustomerMapping.Name = "rmImportCustomerMapping"
        Me.rmImportCustomerMapping.Text = "Import Customer Mapping"
        '
        'rmExportBlankSheetZone
        '
        Me.rmExportBlankSheetZone.Name = "rmExportBlankSheetZone"
        Me.rmExportBlankSheetZone.Text = "Export Blank Sheet Zone"
        '
        'rmExportBlankSheetCustomerCategory
        '
        Me.rmExportBlankSheetCustomerCategory.Name = "rmExportBlankSheetCustomerCategory"
        Me.rmExportBlankSheetCustomerCategory.Text = "Export Blank Sheet Customer Category"
        '
        'rmImportZone
        '
        Me.rmImportZone.Name = "rmImportZone"
        Me.rmImportZone.Text = "Import Zone"
        '
        'rmImportCustomerCategory
        '
        Me.rmImportCustomerCategory.Name = "rmImportCustomerCategory"
        Me.rmImportCustomerCategory.Text = "Import Customer Category"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.RadGroupBox1.Controls.Add(Me.chkHRAdmin)
        Me.RadGroupBox1.Controls.Add(Me.lblMP)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel13)
        Me.RadGroupBox1.Controls.Add(Me.txtMP)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox1.Controls.Add(Me.CboAppUserType)
        Me.RadGroupBox1.Controls.Add(Me.chkLicenceReserved)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.ChkDepartmentHead)
        Me.RadGroupBox1.Controls.Add(Me.mulCustomerCategory)
        Me.RadGroupBox1.Controls.Add(Me.mulZone)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel22)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel10)
        Me.RadGroupBox1.Controls.Add(Me.txt_Mob_No)
        Me.RadGroupBox1.Controls.Add(Me.lblMobileNo)
        Me.RadGroupBox1.Controls.Add(Me.ChkViewMilkReceiptAndSample)
        Me.RadGroupBox1.Controls.Add(Me.lblDepartmentName)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox1.Controls.Add(Me.fndDepartment)
        Me.RadGroupBox1.Controls.Add(Me.RadPanel1)
        Me.RadGroupBox1.Controls.Add(Me.lblEmployeeFinder)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.EmployeeFinder)
        Me.RadGroupBox1.Controls.Add(Me.lblVendorName)
        Me.RadGroupBox1.Controls.Add(Me.lblLocationName)
        Me.RadGroupBox1.Controls.Add(Me.lblVendor)
        Me.RadGroupBox1.Controls.Add(Me.fndVendor)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtEmailId)
        Me.RadGroupBox1.Controls.Add(Me.txtDefaultLocation)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.cmbLevel)
        Me.RadGroupBox1.Controls.Add(Me.lblReportingUserName)
        Me.RadGroupBox1.Controls.Add(Me.lblReportingPerson)
        Me.RadGroupBox1.Controls.Add(Me.txtUserName)
        Me.RadGroupBox1.Controls.Add(Me.fndUserCode)
        Me.RadGroupBox1.Controls.Add(Me.lblUserCode)
        Me.RadGroupBox1.Controls.Add(Me.btnnew)
        Me.RadGroupBox1.Controls.Add(Me.txtPassword)
        Me.RadGroupBox1.Controls.Add(Me.lblPassword)
        Me.RadGroupBox1.Controls.Add(Me.fndLabel4)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(590, 403)
        Me.RadGroupBox1.TabIndex = 0
        '
        'chkHRAdmin
        '
        Me.chkHRAdmin.Location = New System.Drawing.Point(500, 135)
        Me.chkHRAdmin.MyLinkLable1 = Nothing
        Me.chkHRAdmin.MyLinkLable2 = Nothing
        Me.chkHRAdmin.Name = "chkHRAdmin"
        Me.chkHRAdmin.Size = New System.Drawing.Size(71, 18)
        Me.chkHRAdmin.TabIndex = 400
        Me.chkHRAdmin.Tag1 = Nothing
        Me.chkHRAdmin.Text = "HR Admin"
        '
        'lblMP
        '
        Me.lblMP.AutoSize = False
        Me.lblMP.BorderVisible = True
        Me.lblMP.FieldName = Nothing
        Me.lblMP.Location = New System.Drawing.Point(328, 116)
        Me.lblMP.Name = "lblMP"
        Me.lblMP.Size = New System.Drawing.Size(246, 19)
        Me.lblMP.TabIndex = 399
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(15, 117)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel13.TabIndex = 398
        Me.MyLabel13.Text = "Milk Producer"
        '
        'txtMP
        '
        Me.txtMP.CalculationExpression = Nothing
        Me.txtMP.FieldCode = Nothing
        Me.txtMP.FieldDesc = Nothing
        Me.txtMP.FieldMaxLength = 0
        Me.txtMP.FieldName = Nothing
        Me.txtMP.isCalculatedField = False
        Me.txtMP.IsSourceFromTable = False
        Me.txtMP.IsSourceFromValueList = False
        Me.txtMP.IsUnique = False
        Me.txtMP.Location = New System.Drawing.Point(104, 116)
        Me.txtMP.MendatroryField = False
        Me.txtMP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMP.MyLinkLable1 = Nothing
        Me.txtMP.MyLinkLable2 = Nothing
        Me.txtMP.MyReadOnly = False
        Me.txtMP.MyShowMasterFormButton = False
        Me.txtMP.Name = "txtMP"
        Me.txtMP.ReferenceFieldDesc = Nothing
        Me.txtMP.ReferenceFieldName = Nothing
        Me.txtMP.ReferenceTableName = Nothing
        Me.txtMP.Size = New System.Drawing.Size(221, 18)
        Me.txtMP.TabIndex = 397
        Me.txtMP.Value = ""
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(15, 232)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel8.TabIndex = 396
        Me.MyLabel8.Text = "App User Type"
        '
        'CboAppUserType
        '
        Me.CboAppUserType.AutoCompleteDisplayMember = Nothing
        Me.CboAppUserType.AutoCompleteValueMember = Nothing
        Me.CboAppUserType.DropDownAnimationEnabled = True
        Me.CboAppUserType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboAppUserType.Location = New System.Drawing.Point(104, 230)
        Me.CboAppUserType.Name = "CboAppUserType"
        Me.CboAppUserType.Size = New System.Drawing.Size(221, 20)
        Me.CboAppUserType.TabIndex = 395
        '
        'chkLicenceReserved
        '
        Me.chkLicenceReserved.Location = New System.Drawing.Point(467, 231)
        Me.chkLicenceReserved.MyLinkLable1 = Nothing
        Me.chkLicenceReserved.MyLinkLable2 = Nothing
        Me.chkLicenceReserved.Name = "chkLicenceReserved"
        Me.chkLicenceReserved.Size = New System.Drawing.Size(105, 18)
        Me.chkLicenceReserved.TabIndex = 222
        Me.chkLicenceReserved.Tag1 = Nothing
        Me.chkLicenceReserved.Text = "Licence Reserved"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.dtInActive)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel18)
        Me.RadGroupBox2.Controls.Add(Me.chkInActive)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(328, 157)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(259, 27)
        Me.RadGroupBox2.TabIndex = 394
        '
        'dtInActive
        '
        Me.dtInActive.CalculationExpression = Nothing
        Me.dtInActive.CustomFormat = "dd/MM/yyyy"
        Me.dtInActive.FieldCode = Nothing
        Me.dtInActive.FieldDesc = Nothing
        Me.dtInActive.FieldMaxLength = 0
        Me.dtInActive.FieldName = Nothing
        Me.dtInActive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtInActive.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtInActive.isCalculatedField = False
        Me.dtInActive.IsSourceFromTable = False
        Me.dtInActive.IsSourceFromValueList = False
        Me.dtInActive.IsUnique = False
        Me.dtInActive.Location = New System.Drawing.Point(163, 6)
        Me.dtInActive.MendatroryField = False
        Me.dtInActive.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtInActive.MyLinkLable1 = Nothing
        Me.dtInActive.MyLinkLable2 = Nothing
        Me.dtInActive.Name = "dtInActive"
        Me.dtInActive.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtInActive.ReferenceFieldDesc = Nothing
        Me.dtInActive.ReferenceFieldName = Nothing
        Me.dtInActive.ReferenceTableName = Nothing
        Me.dtInActive.Size = New System.Drawing.Size(81, 18)
        Me.dtInActive.TabIndex = 3
        Me.dtInActive.TabStop = False
        Me.dtInActive.Text = "17/05/2011"
        Me.dtInActive.Value = New Date(2011, 5, 17, 15, 2, 19, 281)
        '
        'RadLabel18
        '
        Me.RadLabel18.FieldName = Nothing
        Me.RadLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel18.Location = New System.Drawing.Point(85, 7)
        Me.RadLabel18.Name = "RadLabel18"
        Me.RadLabel18.Size = New System.Drawing.Size(72, 16)
        Me.RadLabel18.TabIndex = 1
        Me.RadLabel18.Text = "Inactive Date"
        '
        'chkInActive
        '
        Me.chkInActive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInActive.Location = New System.Drawing.Point(7, 7)
        Me.chkInActive.Name = "chkInActive"
        Me.chkInActive.Size = New System.Drawing.Size(59, 16)
        Me.chkInActive.TabIndex = 0
        Me.chkInActive.Text = "Inactive"
        '
        'ChkDepartmentHead
        '
        Me.ChkDepartmentHead.Location = New System.Drawing.Point(330, 231)
        Me.ChkDepartmentHead.MyLinkLable1 = Nothing
        Me.ChkDepartmentHead.MyLinkLable2 = Nothing
        Me.ChkDepartmentHead.Name = "ChkDepartmentHead"
        Me.ChkDepartmentHead.Size = New System.Drawing.Size(110, 18)
        Me.ChkDepartmentHead.TabIndex = 221
        Me.ChkDepartmentHead.Tag1 = Nothing
        Me.ChkDepartmentHead.Text = "Department Head"
        '
        'mulCustomerCategory
        '
        Me.mulCustomerCategory.arrDispalyMember = Nothing
        Me.mulCustomerCategory.arrValueMember = Nothing
        Me.mulCustomerCategory.Location = New System.Drawing.Point(104, 277)
        Me.mulCustomerCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mulCustomerCategory.MyLinkLable1 = Nothing
        Me.mulCustomerCategory.MyLinkLable2 = Nothing
        Me.mulCustomerCategory.MyNullText = "Please Select"
        Me.mulCustomerCategory.Name = "mulCustomerCategory"
        Me.mulCustomerCategory.Size = New System.Drawing.Size(470, 19)
        Me.mulCustomerCategory.TabIndex = 393
        '
        'mulZone
        '
        Me.mulZone.arrDispalyMember = Nothing
        Me.mulZone.arrValueMember = Nothing
        Me.mulZone.Location = New System.Drawing.Point(104, 254)
        Me.mulZone.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mulZone.MyLinkLable1 = Nothing
        Me.mulZone.MyLinkLable2 = Nothing
        Me.mulZone.MyNullText = "Please Select"
        Me.mulZone.Name = "mulZone"
        Me.mulZone.Size = New System.Drawing.Size(470, 19)
        Me.mulZone.TabIndex = 392
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel22.Location = New System.Drawing.Point(13, 278)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel22.TabIndex = 391
        Me.MyLabel22.Text = "Cust Category"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(15, 255)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel10.TabIndex = 227
        Me.MyLabel10.Text = "Zone"
        '
        'txt_Mob_No
        '
        Me.txt_Mob_No.BackColor = System.Drawing.Color.White
        Me.txt_Mob_No.CalculationExpression = Nothing
        Me.txt_Mob_No.DecimalPlaces = 2
        Me.txt_Mob_No.FieldCode = Nothing
        Me.txt_Mob_No.FieldDesc = Nothing
        Me.txt_Mob_No.FieldMaxLength = 0
        Me.txt_Mob_No.FieldName = Nothing
        Me.txt_Mob_No.isCalculatedField = False
        Me.txt_Mob_No.IsSourceFromTable = False
        Me.txt_Mob_No.IsSourceFromValueList = False
        Me.txt_Mob_No.IsUnique = False
        Me.txt_Mob_No.Location = New System.Drawing.Point(104, 162)
        Me.txt_Mob_No.MendatroryField = False
        Me.txt_Mob_No.MyLinkLable1 = Nothing
        Me.txt_Mob_No.MyLinkLable2 = Nothing
        Me.txt_Mob_No.Name = "txt_Mob_No"
        Me.txt_Mob_No.ReferenceFieldDesc = Nothing
        Me.txt_Mob_No.ReferenceFieldName = Nothing
        Me.txt_Mob_No.ReferenceTableName = Nothing
        Me.txt_Mob_No.Size = New System.Drawing.Size(221, 20)
        Me.txt_Mob_No.TabIndex = 225
        Me.txt_Mob_No.Text = "0"
        Me.txt_Mob_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txt_Mob_No.Value = 0R
        '
        'lblMobileNo
        '
        Me.lblMobileNo.FieldName = Nothing
        Me.lblMobileNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMobileNo.Location = New System.Drawing.Point(15, 164)
        Me.lblMobileNo.Name = "lblMobileNo"
        Me.lblMobileNo.Size = New System.Drawing.Size(57, 16)
        Me.lblMobileNo.TabIndex = 223
        Me.lblMobileNo.Text = "Mobile No"
        '
        'ChkViewMilkReceiptAndSample
        '
        Me.ChkViewMilkReceiptAndSample.Location = New System.Drawing.Point(328, 135)
        Me.ChkViewMilkReceiptAndSample.MyLinkLable1 = Nothing
        Me.ChkViewMilkReceiptAndSample.MyLinkLable2 = Nothing
        Me.ChkViewMilkReceiptAndSample.Name = "ChkViewMilkReceiptAndSample"
        Me.ChkViewMilkReceiptAndSample.Size = New System.Drawing.Size(161, 18)
        Me.ChkViewMilkReceiptAndSample.TabIndex = 3
        Me.ChkViewMilkReceiptAndSample.Tag1 = Nothing
        Me.ChkViewMilkReceiptAndSample.Text = "View Milk Receipt && Sample"
        '
        'lblDepartmentName
        '
        Me.lblDepartmentName.AutoSize = False
        Me.lblDepartmentName.BorderVisible = True
        Me.lblDepartmentName.FieldName = Nothing
        Me.lblDepartmentName.Location = New System.Drawing.Point(328, 208)
        Me.lblDepartmentName.Name = "lblDepartmentName"
        Me.lblDepartmentName.Size = New System.Drawing.Size(246, 19)
        Me.lblDepartmentName.TabIndex = 220
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(15, 209)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel9.TabIndex = 219
        Me.MyLabel9.Text = "Department"
        '
        'fndDepartment
        '
        Me.fndDepartment.CalculationExpression = Nothing
        Me.fndDepartment.FieldCode = Nothing
        Me.fndDepartment.FieldDesc = Nothing
        Me.fndDepartment.FieldMaxLength = 0
        Me.fndDepartment.FieldName = Nothing
        Me.fndDepartment.isCalculatedField = False
        Me.fndDepartment.IsSourceFromTable = False
        Me.fndDepartment.IsSourceFromValueList = False
        Me.fndDepartment.IsUnique = False
        Me.fndDepartment.Location = New System.Drawing.Point(104, 208)
        Me.fndDepartment.MendatroryField = False
        Me.fndDepartment.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDepartment.MyLinkLable1 = Nothing
        Me.fndDepartment.MyLinkLable2 = Nothing
        Me.fndDepartment.MyReadOnly = False
        Me.fndDepartment.MyShowMasterFormButton = False
        Me.fndDepartment.Name = "fndDepartment"
        Me.fndDepartment.ReferenceFieldDesc = Nothing
        Me.fndDepartment.ReferenceFieldName = Nothing
        Me.fndDepartment.ReferenceTableName = Nothing
        Me.fndDepartment.Size = New System.Drawing.Size(221, 18)
        Me.fndDepartment.TabIndex = 218
        Me.fndDepartment.Value = ""
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.CmbAppUserSaleType)
        Me.RadPanel1.Controls.Add(Me.MyLabel11)
        Me.RadPanel1.Controls.Add(Me.cboEntryUOM)
        Me.RadPanel1.Controls.Add(Me.MyLabel12)
        Me.RadPanel1.Controls.Add(Me.lblDisRetailer)
        Me.RadPanel1.Controls.Add(Me.MyLabel6)
        Me.RadPanel1.Controls.Add(Me.lblRetailerCode)
        Me.RadPanel1.Controls.Add(Me.fndDisRetailerCode)
        Me.RadPanel1.Controls.Add(Me.lblCustCode)
        Me.RadPanel1.Controls.Add(Me.fndCustCode)
        Me.RadPanel1.Controls.Add(Me.MyLabel3)
        Me.RadPanel1.Controls.Add(Me.CmbLoginType)
        Me.RadPanel1.Location = New System.Drawing.Point(9, 304)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(578, 95)
        Me.RadPanel1.TabIndex = 1
        '
        'CmbAppUserSaleType
        '
        Me.CmbAppUserSaleType.AutoCompleteDisplayMember = Nothing
        Me.CmbAppUserSaleType.AutoCompleteValueMember = Nothing
        Me.CmbAppUserSaleType.DropDownAnimationEnabled = True
        Me.CmbAppUserSaleType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CmbAppUserSaleType.Location = New System.Drawing.Point(432, 71)
        Me.CmbAppUserSaleType.Name = "CmbAppUserSaleType"
        Me.CmbAppUserSaleType.Size = New System.Drawing.Size(135, 20)
        Me.CmbAppUserSaleType.TabIndex = 222
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(321, 73)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(108, 16)
        Me.MyLabel11.TabIndex = 221
        Me.MyLabel11.Text = "App User Sale Type"
        '
        'lblDisRetailer
        '
        Me.lblDisRetailer.FieldName = Nothing
        Me.lblDisRetailer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDisRetailer.Location = New System.Drawing.Point(5, 50)
        Me.lblDisRetailer.Name = "lblDisRetailer"
        Me.lblDisRetailer.Size = New System.Drawing.Size(58, 16)
        Me.lblDisRetailer.TabIndex = 220
        Me.lblDisRetailer.Text = "Distributer"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(5, 29)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel6.TabIndex = 219
        Me.MyLabel6.Text = "Customer"
        '
        'lblRetailerCode
        '
        Me.lblRetailerCode.AutoSize = False
        Me.lblRetailerCode.BorderVisible = True
        Me.lblRetailerCode.FieldName = Nothing
        Me.lblRetailerCode.Location = New System.Drawing.Point(321, 50)
        Me.lblRetailerCode.Name = "lblRetailerCode"
        Me.lblRetailerCode.Size = New System.Drawing.Size(246, 19)
        Me.lblRetailerCode.TabIndex = 218
        '
        'fndDisRetailerCode
        '
        Me.fndDisRetailerCode.CalculationExpression = Nothing
        Me.fndDisRetailerCode.FieldCode = Nothing
        Me.fndDisRetailerCode.FieldDesc = Nothing
        Me.fndDisRetailerCode.FieldMaxLength = 0
        Me.fndDisRetailerCode.FieldName = Nothing
        Me.fndDisRetailerCode.isCalculatedField = False
        Me.fndDisRetailerCode.IsSourceFromTable = False
        Me.fndDisRetailerCode.IsSourceFromValueList = False
        Me.fndDisRetailerCode.IsUnique = False
        Me.fndDisRetailerCode.Location = New System.Drawing.Point(95, 50)
        Me.fndDisRetailerCode.MendatroryField = False
        Me.fndDisRetailerCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDisRetailerCode.MyLinkLable1 = Nothing
        Me.fndDisRetailerCode.MyLinkLable2 = Nothing
        Me.fndDisRetailerCode.MyReadOnly = False
        Me.fndDisRetailerCode.MyShowMasterFormButton = False
        Me.fndDisRetailerCode.Name = "fndDisRetailerCode"
        Me.fndDisRetailerCode.ReferenceFieldDesc = Nothing
        Me.fndDisRetailerCode.ReferenceFieldName = Nothing
        Me.fndDisRetailerCode.ReferenceTableName = Nothing
        Me.fndDisRetailerCode.Size = New System.Drawing.Size(221, 20)
        Me.fndDisRetailerCode.TabIndex = 217
        Me.fndDisRetailerCode.Value = ""
        '
        'lblCustCode
        '
        Me.lblCustCode.AutoSize = False
        Me.lblCustCode.BorderVisible = True
        Me.lblCustCode.FieldName = Nothing
        Me.lblCustCode.Location = New System.Drawing.Point(321, 28)
        Me.lblCustCode.Name = "lblCustCode"
        Me.lblCustCode.Size = New System.Drawing.Size(246, 19)
        Me.lblCustCode.TabIndex = 216
        '
        'fndCustCode
        '
        Me.fndCustCode.CalculationExpression = Nothing
        Me.fndCustCode.FieldCode = Nothing
        Me.fndCustCode.FieldDesc = Nothing
        Me.fndCustCode.FieldMaxLength = 0
        Me.fndCustCode.FieldName = Nothing
        Me.fndCustCode.isCalculatedField = False
        Me.fndCustCode.IsSourceFromTable = False
        Me.fndCustCode.IsSourceFromValueList = False
        Me.fndCustCode.IsUnique = False
        Me.fndCustCode.Location = New System.Drawing.Point(95, 28)
        Me.fndCustCode.MendatroryField = False
        Me.fndCustCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCustCode.MyLinkLable1 = Nothing
        Me.fndCustCode.MyLinkLable2 = Nothing
        Me.fndCustCode.MyReadOnly = False
        Me.fndCustCode.MyShowMasterFormButton = False
        Me.fndCustCode.Name = "fndCustCode"
        Me.fndCustCode.ReferenceFieldDesc = Nothing
        Me.fndCustCode.ReferenceFieldName = Nothing
        Me.fndCustCode.ReferenceTableName = Nothing
        Me.fndCustCode.Size = New System.Drawing.Size(221, 20)
        Me.fndCustCode.TabIndex = 215
        Me.fndCustCode.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(5, 7)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel3.TabIndex = 11
        Me.MyLabel3.Text = "Type"
        '
        'CmbLoginType
        '
        Me.CmbLoginType.AutoCompleteDisplayMember = Nothing
        Me.CmbLoginType.AutoCompleteValueMember = Nothing
        Me.CmbLoginType.DropDownAnimationEnabled = True
        Me.CmbLoginType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CmbLoginType.Location = New System.Drawing.Point(95, 5)
        Me.CmbLoginType.Name = "CmbLoginType"
        Me.CmbLoginType.Size = New System.Drawing.Size(221, 20)
        Me.CmbLoginType.TabIndex = 10
        '
        'lblEmployeeFinder
        '
        Me.lblEmployeeFinder.AutoSize = False
        Me.lblEmployeeFinder.BorderVisible = True
        Me.lblEmployeeFinder.FieldName = Nothing
        Me.lblEmployeeFinder.Location = New System.Drawing.Point(328, 186)
        Me.lblEmployeeFinder.Name = "lblEmployeeFinder"
        Me.lblEmployeeFinder.Size = New System.Drawing.Size(246, 19)
        Me.lblEmployeeFinder.TabIndex = 217
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(15, 187)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel5.TabIndex = 216
        Me.MyLabel5.Text = "Employee"
        '
        'EmployeeFinder
        '
        Me.EmployeeFinder.CalculationExpression = Nothing
        Me.EmployeeFinder.FieldCode = Nothing
        Me.EmployeeFinder.FieldDesc = Nothing
        Me.EmployeeFinder.FieldMaxLength = 0
        Me.EmployeeFinder.FieldName = Nothing
        Me.EmployeeFinder.isCalculatedField = False
        Me.EmployeeFinder.IsSourceFromTable = False
        Me.EmployeeFinder.IsSourceFromValueList = False
        Me.EmployeeFinder.IsUnique = False
        Me.EmployeeFinder.Location = New System.Drawing.Point(104, 186)
        Me.EmployeeFinder.MendatroryField = False
        Me.EmployeeFinder.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EmployeeFinder.MyLinkLable1 = Nothing
        Me.EmployeeFinder.MyLinkLable2 = Nothing
        Me.EmployeeFinder.MyReadOnly = False
        Me.EmployeeFinder.MyShowMasterFormButton = False
        Me.EmployeeFinder.Name = "EmployeeFinder"
        Me.EmployeeFinder.ReferenceFieldDesc = Nothing
        Me.EmployeeFinder.ReferenceFieldName = Nothing
        Me.EmployeeFinder.ReferenceTableName = Nothing
        Me.EmployeeFinder.Size = New System.Drawing.Size(221, 18)
        Me.EmployeeFinder.TabIndex = 215
        Me.EmployeeFinder.Value = ""
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Location = New System.Drawing.Point(328, 94)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(246, 19)
        Me.lblVendorName.TabIndex = 214
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Location = New System.Drawing.Point(328, 72)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(246, 19)
        Me.lblLocationName.TabIndex = 211
        '
        'lblVendor
        '
        Me.lblVendor.FieldName = Nothing
        Me.lblVendor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendor.Location = New System.Drawing.Point(15, 95)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Size = New System.Drawing.Size(43, 16)
        Me.lblVendor.TabIndex = 213
        Me.lblVendor.Text = "Vendor"
        '
        'fndVendor
        '
        Me.fndVendor.CalculationExpression = Nothing
        Me.fndVendor.FieldCode = Nothing
        Me.fndVendor.FieldDesc = Nothing
        Me.fndVendor.FieldMaxLength = 0
        Me.fndVendor.FieldName = Nothing
        Me.fndVendor.isCalculatedField = False
        Me.fndVendor.IsSourceFromTable = False
        Me.fndVendor.IsSourceFromValueList = False
        Me.fndVendor.IsUnique = False
        Me.fndVendor.Location = New System.Drawing.Point(104, 94)
        Me.fndVendor.MendatroryField = False
        Me.fndVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVendor.MyLinkLable1 = Nothing
        Me.fndVendor.MyLinkLable2 = Nothing
        Me.fndVendor.MyReadOnly = False
        Me.fndVendor.MyShowMasterFormButton = False
        Me.fndVendor.Name = "fndVendor"
        Me.fndVendor.ReferenceFieldDesc = Nothing
        Me.fndVendor.ReferenceFieldName = Nothing
        Me.fndVendor.ReferenceTableName = Nothing
        Me.fndVendor.Size = New System.Drawing.Size(221, 18)
        Me.fndVendor.TabIndex = 212
        Me.fndVendor.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(15, 73)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(88, 16)
        Me.MyLabel4.TabIndex = 210
        Me.MyLabel4.Text = "Default Location"
        '
        'txtEmailId
        '
        Me.txtEmailId.CalculationExpression = Nothing
        Me.txtEmailId.FieldCode = Nothing
        Me.txtEmailId.FieldDesc = Nothing
        Me.txtEmailId.FieldMaxLength = 0
        Me.txtEmailId.FieldName = Nothing
        Me.txtEmailId.isCalculatedField = False
        Me.txtEmailId.IsSourceFromTable = False
        Me.txtEmailId.IsSourceFromValueList = False
        Me.txtEmailId.IsUnique = False
        Me.txtEmailId.Location = New System.Drawing.Point(104, 138)
        Me.txtEmailId.MendatroryField = False
        Me.txtEmailId.MyLinkLable1 = Me.MyLabel2
        Me.txtEmailId.MyLinkLable2 = Nothing
        Me.txtEmailId.Name = "txtEmailId"
        Me.txtEmailId.ReferenceFieldDesc = Nothing
        Me.txtEmailId.ReferenceFieldName = Nothing
        Me.txtEmailId.ReferenceTableName = Nothing
        Me.txtEmailId.Size = New System.Drawing.Size(221, 20)
        Me.txtEmailId.TabIndex = 7
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(15, 140)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(47, 16)
        Me.MyLabel2.TabIndex = 6
        Me.MyLabel2.Text = "Email Id"
        '
        'txtDefaultLocation
        '
        Me.txtDefaultLocation.CalculationExpression = Nothing
        Me.txtDefaultLocation.FieldCode = Nothing
        Me.txtDefaultLocation.FieldDesc = Nothing
        Me.txtDefaultLocation.FieldMaxLength = 0
        Me.txtDefaultLocation.FieldName = Nothing
        Me.txtDefaultLocation.isCalculatedField = False
        Me.txtDefaultLocation.IsSourceFromTable = False
        Me.txtDefaultLocation.IsSourceFromValueList = False
        Me.txtDefaultLocation.IsUnique = False
        Me.txtDefaultLocation.Location = New System.Drawing.Point(104, 72)
        Me.txtDefaultLocation.MendatroryField = False
        Me.txtDefaultLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDefaultLocation.MyLinkLable1 = Nothing
        Me.txtDefaultLocation.MyLinkLable2 = Nothing
        Me.txtDefaultLocation.MyReadOnly = False
        Me.txtDefaultLocation.MyShowMasterFormButton = False
        Me.txtDefaultLocation.Name = "txtDefaultLocation"
        Me.txtDefaultLocation.ReferenceFieldDesc = Nothing
        Me.txtDefaultLocation.ReferenceFieldName = Nothing
        Me.txtDefaultLocation.ReferenceTableName = Nothing
        Me.txtDefaultLocation.Size = New System.Drawing.Size(221, 18)
        Me.txtDefaultLocation.TabIndex = 6
        Me.txtDefaultLocation.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(328, 29)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel1.TabIndex = 9
        Me.MyLabel1.Text = "User Level"
        '
        'cmbLevel
        '
        Me.cmbLevel.AutoCompleteDisplayMember = Nothing
        Me.cmbLevel.AutoCompleteValueMember = Nothing
        Me.cmbLevel.DropDownAnimationEnabled = True
        Me.cmbLevel.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbLevel.Location = New System.Drawing.Point(394, 27)
        Me.cmbLevel.Name = "cmbLevel"
        Me.cmbLevel.Size = New System.Drawing.Size(180, 20)
        Me.cmbLevel.TabIndex = 4
        '
        'lblReportingUserName
        '
        Me.lblReportingUserName.AutoSize = False
        Me.lblReportingUserName.BorderVisible = True
        Me.lblReportingUserName.FieldName = Nothing
        Me.lblReportingUserName.Location = New System.Drawing.Point(328, 50)
        Me.lblReportingUserName.Name = "lblReportingUserName"
        Me.lblReportingUserName.Size = New System.Drawing.Size(246, 19)
        Me.lblReportingUserName.TabIndex = 208
        '
        'lblReportingPerson
        '
        Me.lblReportingPerson.FieldName = Nothing
        Me.lblReportingPerson.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReportingPerson.Location = New System.Drawing.Point(15, 51)
        Me.lblReportingPerson.Name = "lblReportingPerson"
        Me.lblReportingPerson.Size = New System.Drawing.Size(82, 16)
        Me.lblReportingPerson.TabIndex = 7
        Me.lblReportingPerson.Text = "Reporting User"
        '
        'txtUserName
        '
        Me.txtUserName.CalculationExpression = Nothing
        Me.txtUserName.FieldCode = Nothing
        Me.txtUserName.FieldDesc = Nothing
        Me.txtUserName.FieldMaxLength = 0
        Me.txtUserName.FieldName = Nothing
        Me.txtUserName.isCalculatedField = False
        Me.txtUserName.IsSourceFromTable = False
        Me.txtUserName.IsSourceFromValueList = False
        Me.txtUserName.IsUnique = False
        Me.txtUserName.Location = New System.Drawing.Point(328, 4)
        Me.txtUserName.MendatroryField = False
        Me.txtUserName.MyLinkLable1 = Nothing
        Me.txtUserName.MyLinkLable2 = Nothing
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.ReferenceFieldDesc = Nothing
        Me.txtUserName.ReferenceFieldName = Nothing
        Me.txtUserName.ReferenceTableName = Nothing
        Me.txtUserName.Size = New System.Drawing.Size(246, 20)
        Me.txtUserName.TabIndex = 2
        '
        'fndUserCode
        '
        Me.fndUserCode.FieldName = Nothing
        Me.fndUserCode.Location = New System.Drawing.Point(104, 4)
        Me.fndUserCode.MendatroryField = True
        Me.fndUserCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndUserCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndUserCode.MyLinkLable1 = Me.lblUserCode
        Me.fndUserCode.MyLinkLable2 = Nothing
        Me.fndUserCode.MyMaxLength = 32767
        Me.fndUserCode.MyReadOnly = False
        Me.fndUserCode.Name = "fndUserCode"
        Me.fndUserCode.Size = New System.Drawing.Size(202, 20)
        Me.fndUserCode.TabIndex = 0
        Me.fndUserCode.Value = ""
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = Global.XpertERPEngine.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(306, 4)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(19, 20)
        Me.btnnew.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnnew, "New")
        '
        'fndLabel4
        '
        Me.fndLabel4.CalculationExpression = Nothing
        Me.fndLabel4.FieldCode = Nothing
        Me.fndLabel4.FieldDesc = Nothing
        Me.fndLabel4.FieldMaxLength = 0
        Me.fndLabel4.FieldName = Nothing
        Me.fndLabel4.isCalculatedField = False
        Me.fndLabel4.IsSourceFromTable = False
        Me.fndLabel4.IsSourceFromValueList = False
        Me.fndLabel4.IsUnique = False
        Me.fndLabel4.Location = New System.Drawing.Point(104, 50)
        Me.fndLabel4.MendatroryField = False
        Me.fndLabel4.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLabel4.MyLinkLable1 = Nothing
        Me.fndLabel4.MyLinkLable2 = Nothing
        Me.fndLabel4.MyReadOnly = False
        Me.fndLabel4.MyShowMasterFormButton = False
        Me.fndLabel4.Name = "fndLabel4"
        Me.fndLabel4.ReferenceFieldDesc = Nothing
        Me.fndLabel4.ReferenceFieldName = Nothing
        Me.fndLabel4.ReferenceTableName = Nothing
        Me.fndLabel4.Size = New System.Drawing.Size(221, 18)
        Me.fndLabel4.TabIndex = 5
        Me.fndLabel4.Value = ""
        '
        'fndEmployeeCode
        '
        Me.fndEmployeeCode.CalculationExpression = Nothing
        Me.fndEmployeeCode.FieldCode = Nothing
        Me.fndEmployeeCode.FieldDesc = Nothing
        Me.fndEmployeeCode.FieldMaxLength = 0
        Me.fndEmployeeCode.FieldName = Nothing
        Me.fndEmployeeCode.isCalculatedField = False
        Me.fndEmployeeCode.IsSourceFromTable = False
        Me.fndEmployeeCode.IsSourceFromValueList = False
        Me.fndEmployeeCode.IsUnique = False
        Me.fndEmployeeCode.Location = New System.Drawing.Point(123, 244)
        Me.fndEmployeeCode.MendatroryField = False
        Me.fndEmployeeCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndEmployeeCode.MyLinkLable1 = Me.lblEmployeeCode
        Me.fndEmployeeCode.MyLinkLable2 = Nothing
        Me.fndEmployeeCode.MyReadOnly = False
        Me.fndEmployeeCode.MyShowMasterFormButton = False
        Me.fndEmployeeCode.Name = "fndEmployeeCode"
        Me.fndEmployeeCode.ReferenceFieldDesc = Nothing
        Me.fndEmployeeCode.ReferenceFieldName = Nothing
        Me.fndEmployeeCode.ReferenceTableName = Nothing
        Me.fndEmployeeCode.Size = New System.Drawing.Size(143, 19)
        Me.fndEmployeeCode.TabIndex = 7
        Me.fndEmployeeCode.Value = ""
        Me.fndEmployeeCode.Visible = False
        '
        'lblEmployeeCode
        '
        Me.lblEmployeeCode.FieldName = Nothing
        Me.lblEmployeeCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmployeeCode.Location = New System.Drawing.Point(23, 244)
        Me.lblEmployeeCode.Name = "lblEmployeeCode"
        Me.lblEmployeeCode.Size = New System.Drawing.Size(87, 16)
        Me.lblEmployeeCode.TabIndex = 14
        Me.lblEmployeeCode.Text = "Employee Code"
        Me.lblEmployeeCode.Visible = False
        '
        'txtEmployeeName
        '
        Me.txtEmployeeName.AutoSize = False
        Me.txtEmployeeName.CalculationExpression = Nothing
        Me.txtEmployeeName.FieldCode = Nothing
        Me.txtEmployeeName.FieldDesc = Nothing
        Me.txtEmployeeName.FieldMaxLength = 0
        Me.txtEmployeeName.FieldName = Nothing
        Me.txtEmployeeName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmployeeName.isCalculatedField = False
        Me.txtEmployeeName.IsSourceFromTable = False
        Me.txtEmployeeName.IsSourceFromValueList = False
        Me.txtEmployeeName.IsUnique = False
        Me.txtEmployeeName.Location = New System.Drawing.Point(430, 245)
        Me.txtEmployeeName.MaxLength = 50
        Me.txtEmployeeName.MendatroryField = False
        Me.txtEmployeeName.Multiline = True
        Me.txtEmployeeName.MyLinkLable1 = Me.lblEmployeeName
        Me.txtEmployeeName.MyLinkLable2 = Nothing
        Me.txtEmployeeName.Name = "txtEmployeeName"
        Me.txtEmployeeName.ReadOnly = True
        Me.txtEmployeeName.ReferenceFieldDesc = Nothing
        Me.txtEmployeeName.ReferenceFieldName = Nothing
        Me.txtEmployeeName.ReferenceTableName = Nothing
        Me.txtEmployeeName.Size = New System.Drawing.Size(160, 21)
        Me.txtEmployeeName.TabIndex = 13
        Me.txtEmployeeName.Visible = False
        '
        'lblEmployeeName
        '
        Me.lblEmployeeName.FieldName = Nothing
        Me.lblEmployeeName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmployeeName.Location = New System.Drawing.Point(331, 246)
        Me.lblEmployeeName.Name = "lblEmployeeName"
        Me.lblEmployeeName.Size = New System.Drawing.Size(90, 16)
        Me.lblEmployeeName.TabIndex = 10
        Me.lblEmployeeName.Text = "Employee Name"
        Me.lblEmployeeName.Visible = False
        '
        'fndLabel3
        '
        Me.fndLabel3.CalculationExpression = Nothing
        Me.fndLabel3.FieldCode = Nothing
        Me.fndLabel3.FieldDesc = Nothing
        Me.fndLabel3.FieldMaxLength = 0
        Me.fndLabel3.FieldName = Nothing
        Me.fndLabel3.isCalculatedField = False
        Me.fndLabel3.IsSourceFromTable = False
        Me.fndLabel3.IsSourceFromValueList = False
        Me.fndLabel3.IsUnique = False
        Me.fndLabel3.Location = New System.Drawing.Point(136, 219)
        Me.fndLabel3.MendatroryField = False
        Me.fndLabel3.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLabel3.MyLinkLable1 = Nothing
        Me.fndLabel3.MyLinkLable2 = Nothing
        Me.fndLabel3.MyReadOnly = False
        Me.fndLabel3.MyShowMasterFormButton = False
        Me.fndLabel3.Name = "fndLabel3"
        Me.fndLabel3.ReferenceFieldDesc = Nothing
        Me.fndLabel3.ReferenceFieldName = Nothing
        Me.fndLabel3.ReferenceTableName = Nothing
        Me.fndLabel3.Size = New System.Drawing.Size(143, 18)
        Me.fndLabel3.TabIndex = 6
        Me.fndLabel3.Value = ""
        Me.fndLabel3.Visible = False
        '
        'fndLabel1
        '
        Me.fndLabel1.CalculationExpression = Nothing
        Me.fndLabel1.FieldCode = Nothing
        Me.fndLabel1.FieldDesc = Nothing
        Me.fndLabel1.FieldMaxLength = 0
        Me.fndLabel1.FieldName = Nothing
        Me.fndLabel1.isCalculatedField = False
        Me.fndLabel1.IsSourceFromTable = False
        Me.fndLabel1.IsSourceFromValueList = False
        Me.fndLabel1.IsUnique = False
        Me.fndLabel1.Location = New System.Drawing.Point(136, 187)
        Me.fndLabel1.MendatroryField = False
        Me.fndLabel1.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLabel1.MyLinkLable1 = Nothing
        Me.fndLabel1.MyLinkLable2 = Nothing
        Me.fndLabel1.MyReadOnly = False
        Me.fndLabel1.MyShowMasterFormButton = False
        Me.fndLabel1.Name = "fndLabel1"
        Me.fndLabel1.ReferenceFieldDesc = Nothing
        Me.fndLabel1.ReferenceFieldName = Nothing
        Me.fndLabel1.ReferenceTableName = Nothing
        Me.fndLabel1.Size = New System.Drawing.Size(143, 18)
        Me.fndLabel1.TabIndex = 4
        Me.fndLabel1.Value = ""
        Me.fndLabel1.Visible = False
        '
        'fndLabel2
        '
        Me.fndLabel2.CalculationExpression = Nothing
        Me.fndLabel2.FieldCode = Nothing
        Me.fndLabel2.FieldDesc = Nothing
        Me.fndLabel2.FieldMaxLength = 0
        Me.fndLabel2.FieldName = Nothing
        Me.fndLabel2.isCalculatedField = False
        Me.fndLabel2.IsSourceFromTable = False
        Me.fndLabel2.IsSourceFromValueList = False
        Me.fndLabel2.IsUnique = False
        Me.fndLabel2.Location = New System.Drawing.Point(335, 187)
        Me.fndLabel2.MendatroryField = False
        Me.fndLabel2.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLabel2.MyLinkLable1 = Nothing
        Me.fndLabel2.MyLinkLable2 = Nothing
        Me.fndLabel2.MyReadOnly = False
        Me.fndLabel2.MyShowMasterFormButton = False
        Me.fndLabel2.Name = "fndLabel2"
        Me.fndLabel2.ReferenceFieldDesc = Nothing
        Me.fndLabel2.ReferenceFieldName = Nothing
        Me.fndLabel2.ReferenceTableName = Nothing
        Me.fndLabel2.Size = New System.Drawing.Size(143, 18)
        Me.fndLabel2.TabIndex = 11
        Me.fndLabel2.Value = ""
        Me.fndLabel2.Visible = False
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGetHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(719, 568)
        Me.SplitContainer1.SplitterDistance = 523
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(719, 523)
        Me.RadPageView1.TabIndex = 1
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.GBRoute)
        Me.RadPageViewPage1.Controls.Add(Me.lblLength)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(55.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(698, 475)
        Me.RadPageViewPage1.Text = "General"
        '
        'GBRoute
        '
        Me.GBRoute.Controls.Add(Me.txtRoute)
        Me.GBRoute.Controls.Add(Me.MyLabel16)
        Me.GBRoute.Location = New System.Drawing.Point(3, 403)
        Me.GBRoute.Name = "GBRoute"
        Me.GBRoute.Size = New System.Drawing.Size(590, 33)
        Me.GBRoute.TabIndex = 16
        Me.GBRoute.TabStop = False
        '
        'txtRoute
        '
        Me.txtRoute.arrDispalyMember = Nothing
        Me.txtRoute.arrValueMember = Nothing
        Me.txtRoute.Location = New System.Drawing.Point(48, 11)
        Me.txtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoute.MyLinkLable1 = Me.MyLabel16
        Me.txtRoute.MyLinkLable2 = Nothing
        Me.txtRoute.MyNullText = "All"
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.Size = New System.Drawing.Size(524, 19)
        Me.txtRoute.TabIndex = 429
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(6, 12)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(36, 18)
        Me.MyLabel16.TabIndex = 428
        Me.MyLabel16.Text = "Route"
        '
        'lblLength
        '
        Me.lblLength.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.lblLength.FieldName = Nothing
        Me.lblLength.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblLength.Location = New System.Drawing.Point(12, 431)
        Me.lblLength.Name = "lblLength"
        Me.lblLength.Size = New System.Drawing.Size(471, 46)
        Me.lblLength.TabIndex = 15
        Me.lblLength.Text = "<html><p>Important Instruction</p><p>Password Length maximun 8 characters.</p><p>" &
    "( Including Special Characters, Numeric, Upper case Alphabet and Lower case Alph" &
    "abet )</p></html>"
        Me.lblLength.Visible = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gvCustomer)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(113.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(698, 475)
        Me.RadPageViewPage2.Text = "Customer Mapping"
        '
        'gvCustomer
        '
        Me.gvCustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCustomer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvCustomer.ForeColor = System.Drawing.Color.Black
        Me.gvCustomer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvCustomer.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvCustomer.MasterTemplate.AllowDeleteRow = False
        Me.gvCustomer.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvCustomer.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCustomer.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvCustomer.Name = "gvCustomer"
        Me.gvCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvCustomer.ShowGroupPanel = False
        Me.gvCustomer.ShowHeaderCellButtons = True
        Me.gvCustomer.Size = New System.Drawing.Size(698, 475)
        Me.gvCustomer.TabIndex = 1
        Me.gvCustomer.TabStop = False
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gvUser)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(87.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(698, 465)
        Me.RadPageViewPage3.Text = "User Mapping"
        '
        'gvUser
        '
        Me.gvUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvUser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvUser.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvUser.ForeColor = System.Drawing.Color.Black
        Me.gvUser.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvUser.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvUser.MasterTemplate.AllowDeleteRow = False
        Me.gvUser.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvUser.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvUser.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gvUser.Name = "gvUser"
        Me.gvUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvUser.ShowGroupPanel = False
        Me.gvUser.ShowHeaderCellButtons = True
        Me.gvUser.Size = New System.Drawing.Size(698, 465)
        Me.gvUser.TabIndex = 2
        Me.gvUser.TabStop = False
        '
        'btnGetHistory
        '
        Me.btnGetHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGetHistory.Location = New System.Drawing.Point(275, 10)
        Me.btnGetHistory.Name = "btnGetHistory"
        Me.btnGetHistory.Size = New System.Drawing.Size(80, 23)
        Me.btnGetHistory.TabIndex = 17
        Me.btnGetHistory.Text = "Show History"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(141, 10)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(134, 23)
        Me.RadButton1.TabIndex = 3
        Me.RadButton1.Text = "Add Biometric Login"
        '
        'cboEntryUOM
        '
        Me.cboEntryUOM.AutoCompleteDisplayMember = Nothing
        Me.cboEntryUOM.AutoCompleteValueMember = Nothing
        Me.cboEntryUOM.DropDownAnimationEnabled = True
        Me.cboEntryUOM.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboEntryUOM.Location = New System.Drawing.Point(95, 72)
        Me.cboEntryUOM.Name = "cboEntryUOM"
        Me.cboEntryUOM.Size = New System.Drawing.Size(221, 20)
        Me.cboEntryUOM.TabIndex = 224
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(5, 74)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel12.TabIndex = 223
        Me.MyLabel12.Text = "Entry UOM"
        '
        'FrmUserMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(719, 588)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.fndEmployeeCode)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.txtEmployeeName)
        Me.Controls.Add(Me.fndLabel3)
        Me.Controls.Add(Me.lblEmployeeName)
        Me.Controls.Add(Me.lblUserType)
        Me.Controls.Add(Me.lblEmployeeCode)
        Me.Controls.Add(Me.lbl1)
        Me.Controls.Add(Me.lbl2)
        Me.Controls.Add(Me.fndLabel1)
        Me.Controls.Add(Me.lblLabel3)
        Me.Controls.Add(Me.lblLabel4)
        Me.Controls.Add(Me.ddlUserType)
        Me.Controls.Add(Me.fndLabel2)
        Me.Name = "FrmUserMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "User Master"
        CType(Me.lblUserCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUserType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlUserType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPassword, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPassword, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkHRAdmin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboAppUserType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLicenceReserved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.dtInActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkDepartmentHead, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Mob_No, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMobileNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkViewMilkReceiptAndSample, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartmentName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.CmbAppUserSaleType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDisRetailer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRetailerCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbLoginType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmployeeFinder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmailId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReportingUserName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReportingPerson, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUserName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmployeeCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmployeeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmployeeName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        Me.GBRoute.ResumeLayout(False)
        Me.GBRoute.PerformLayout()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLength, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gvCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gvUser.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGetHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboEntryUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents ddlUserType As common.Controls.MyComboBox
    Friend WithEvents txtPassword As common.Controls.MyTextBox
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents munuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtEmployeeName As common.Controls.MyTextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblUserCode As common.Controls.MyLabel
    Friend WithEvents lblUserType As common.Controls.MyLabel
    Friend WithEvents lbl1 As common.Controls.MyLabel
    Friend WithEvents lbl2 As common.Controls.MyLabel
    Friend WithEvents lblLabel3 As common.Controls.MyLabel
    Friend WithEvents lblLabel4 As common.Controls.MyLabel
    Friend WithEvents lblPassword As common.Controls.MyLabel
    Friend WithEvents lblEmployeeName As common.Controls.MyLabel
    Friend WithEvents lblEmployeeCode As common.Controls.MyLabel
    Friend WithEvents fndLabel4 As common.UserControls.txtFinder
    Friend WithEvents fndLabel2 As common.UserControls.txtFinder
    Friend WithEvents fndLabel1 As common.UserControls.txtFinder
    Friend WithEvents fndLabel3 As common.UserControls.txtFinder
    Friend WithEvents fndEmployeeCode As common.UserControls.txtFinder
    Friend WithEvents fndUserCode As common.UserControls.txtNavigator
    Friend WithEvents txtUserName As common.Controls.MyTextBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblReportingUserName As common.Controls.MyLabel
    Friend WithEvents lblReportingPerson As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents cmbLevel As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents txtEmailId As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtDefaultLocation As common.UserControls.txtFinder
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents lblVendor As common.Controls.MyLabel
    Friend WithEvents fndVendor As common.UserControls.txtFinder
    Friend WithEvents lblEmployeeFinder As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents EmployeeFinder As common.UserControls.txtFinder
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents lblRetailerCode As common.Controls.MyLabel
    Friend WithEvents fndDisRetailerCode As common.UserControls.txtFinder
    Friend WithEvents lblCustCode As common.Controls.MyLabel
    Friend WithEvents fndCustCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents CmbLoginType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents lblDisRetailer As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents gvCustomer As common.UserControls.MyRadGridView
    Friend WithEvents lblDepartmentName As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents fndDepartment As common.UserControls.txtFinder
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents ChkViewMilkReceiptAndSample As common.Controls.MyCheckBox
    Friend WithEvents ChkDepartmentHead As common.Controls.MyCheckBox
    Friend WithEvents lblLength As common.Controls.MyLabel
    Friend WithEvents btnGetHistory As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkLicenceReserved As common.Controls.MyCheckBox
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImportCustomerMapping As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblMobileNo As common.Controls.MyLabel
    Friend WithEvents txt_Mob_No As common.MyNumBox
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvUser As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents mulCustomerCategory As common.UserControls.txtMultiSelectFinder
    Friend WithEvents mulZone As common.UserControls.txtMultiSelectFinder
    Friend WithEvents rmExportBlankSheetZone As RadMenuItem
    Friend WithEvents rmExportBlankSheetCustomerCategory As RadMenuItem
    Friend WithEvents rmImportZone As RadMenuItem
    Friend WithEvents rmImportCustomerCategory As RadMenuItem
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents RadLabel18 As Controls.MyLabel
    Friend WithEvents chkInActive As RadCheckBox
    Friend WithEvents dtInActive As Controls.MyDateTimePicker
    Friend WithEvents MyLabel8 As Controls.MyLabel
    Friend WithEvents CboAppUserType As RadDropDownList
    Friend WithEvents CmbAppUserSaleType As RadDropDownList
    Friend WithEvents MyLabel11 As Controls.MyLabel
    Friend WithEvents GBRoute As GroupBox
    Friend WithEvents txtRoute As UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel16 As Controls.MyLabel
    Friend WithEvents lblMP As Controls.MyLabel
    Friend WithEvents MyLabel13 As Controls.MyLabel
    Friend WithEvents txtMP As UserControls.txtFinder
    Friend WithEvents chkHRAdmin As Controls.MyCheckBox
    Friend WithEvents cboEntryUOM As RadDropDownList
    Friend WithEvents MyLabel12 As Controls.MyLabel
End Class

