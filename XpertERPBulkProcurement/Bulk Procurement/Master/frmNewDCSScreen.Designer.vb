<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmNewDCSScreen
    Inherits FrmMainTranScreen

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnSave As RadButton
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnUpdate As RadButton
    'Inherits System.Windows.Forms.Form

    Private Sub InitializeComponent()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNewDCSScreen))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtHeadLoad = New common.Controls.MyComboBox()
        Me.MyLabel27 = New common.Controls.MyLabel()
        Me.chkOwnBMC = New Telerik.WinControls.UI.RadCheckBox()
        Me.ddlGender = New common.Controls.MyComboBox()
        Me.chkDefaultValue = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkInActive = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtCowPriceStartDate = New common.Controls.MyDateTimePicker()
        Me.chkApplyCowPrice = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtHeadLoadRate = New common.Controls.MyTextBox()
        Me.lblbankcode = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtHeadLoadBasi = New common.Controls.MyTextBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtOwnBMCDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtDCSRouteCode = New common.Controls.MyTextBox()
        Me.txtRegistrationDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtRegistrationNo = New common.Controls.MyTextBox()
        Me.chkPDCS = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkRegistered = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtOtherDetails = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtBankDetails = New common.Controls.MyTextBox()
        Me.txtDCSUploaderCode = New common.Controls.MyTextBox()
        Me.fndDCSCode = New common.UserControls.txtNavigator()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtDCSName = New common.Controls.MyTextBox()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtVidhanSabhaName = New common.Controls.MyTextBox()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.txtPanchayatSamitiName = New common.Controls.MyTextBox()
        Me.txtGramPanchayatName = New common.Controls.MyTextBox()
        Me.txtRevenueVillageName = New common.Controls.MyTextBox()
        Me.txtZoneName = New common.Controls.MyTextBox()
        Me.txtBlockName = New common.Controls.MyTextBox()
        Me.txtDistrictName = New common.Controls.MyTextBox()
        Me.fndVidhanSabha = New common.UserControls.txtFinder()
        Me.fndPanchayatSamiti = New common.UserControls.txtFinder()
        Me.fndGramPanchayat = New common.UserControls.txtFinder()
        Me.fndRevenueVillage = New common.UserControls.txtFinder()
        Me.fndZone = New common.UserControls.txtFinder()
        Me.fndBlock = New common.UserControls.txtFinder()
        Me.fndDistrict = New common.UserControls.txtFinder()
        Me.fndSupervisorCode = New common.UserControls.txtFinder()
        Me.MyLabel38 = New common.Controls.MyLabel()
        Me.txtSupervisorName = New common.Controls.MyTextBox()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.MyLabel25 = New common.Controls.MyLabel()
        Me.MyLabel30 = New common.Controls.MyLabel()
        Me.MyLabel26 = New common.Controls.MyLabel()
        Me.MyLabel31 = New common.Controls.MyLabel()
        Me.txtBankIFSCCode1 = New common.Controls.MyTextBox()
        Me.txtBankIFSCCode = New common.Controls.MyTextBox()
        Me.MyLabel28 = New common.Controls.MyLabel()
        Me.txtBankBranchName1 = New common.Controls.MyTextBox()
        Me.MyLabel32 = New common.Controls.MyLabel()
        Me.txtBankBranchName = New common.Controls.MyTextBox()
        Me.txtBankAccountNo1 = New common.Controls.MyTextBox()
        Me.txtBankAccountNo = New common.Controls.MyTextBox()
        Me.txtBankName1 = New common.Controls.MyTextBox()
        Me.txtBankName = New common.Controls.MyTextBox()
        Me.txtDCSCurrentBankDetails1 = New common.Controls.MyTextBox()
        Me.txtDCSCurrentBankDetails = New common.Controls.MyTextBox()
        Me.fndCompanyBank1 = New common.UserControls.txtFinder()
        Me.fndCompanyBank = New common.UserControls.txtFinder()
        Me.MyLabel33 = New common.Controls.MyLabel()
        Me.MyLabel29 = New common.Controls.MyLabel()
        Me.txtCompanyBank1 = New common.Controls.MyTextBox()
        Me.txtCompanyBankName = New common.Controls.MyTextBox()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnUpdate = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtPanNo = New common.Controls.MyTextBox()
        Me.MyLabel34 = New common.Controls.MyLabel()
        Me.txtLoyaltyRate = New common.Controls.MyTextBox()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.txtHeadLoad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOwnBMC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlGender, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDefaultValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCowPriceStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkApplyCowPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHeadLoadRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHeadLoadBasi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOwnBMCDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDCSRouteCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRegistrationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRegistrationNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPDCS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRegistered, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOtherDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDCSUploaderCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDCSName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVidhanSabhaName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPanchayatSamitiName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGramPanchayatName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRevenueVillageName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtZoneName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBlockName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDistrictName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSupervisorName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankIFSCCode1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankIFSCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankBranchName1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankBranchName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankAccountNo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankAccountNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankName1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDCSCurrentBankDetails1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDCSCurrentBankDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCompanyBank1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCompanyBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLoyaltyRate, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUpdate)
        Me.SplitContainer1.Size = New System.Drawing.Size(1047, 473)
        Me.SplitContainer1.SplitterDistance = 432
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.DefaultPage = Me.RadPageViewPage1
        Me.RadPageView1.Location = New System.Drawing.Point(12, 32)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1024, 387)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ShowItemCloseButton = False
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel34)
        Me.RadPageViewPage1.Controls.Add(Me.txtLoyaltyRate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.txtHeadLoad)
        Me.RadPageViewPage1.Controls.Add(Me.txtPanNo)
        Me.RadPageViewPage1.Controls.Add(Me.chkOwnBMC)
        Me.RadPageViewPage1.Controls.Add(Me.ddlGender)
        Me.RadPageViewPage1.Controls.Add(Me.chkDefaultValue)
        Me.RadPageViewPage1.Controls.Add(Me.chkInActive)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel14)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.txtCowPriceStartDate)
        Me.RadPageViewPage1.Controls.Add(Me.chkApplyCowPrice)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel11)
        Me.RadPageViewPage1.Controls.Add(Me.txtHeadLoadRate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.txtHeadLoadBasi)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.txtOwnBMCDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.txtDCSRouteCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtRegistrationDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.txtRegistrationNo)
        Me.RadPageViewPage1.Controls.Add(Me.chkPDCS)
        Me.RadPageViewPage1.Controls.Add(Me.chkRegistered)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtOtherDetails)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtBankDetails)
        Me.RadPageViewPage1.Controls.Add(Me.txtDCSUploaderCode)
        Me.RadPageViewPage1.Controls.Add(Me.fndDCSCode)
        Me.RadPageViewPage1.Controls.Add(Me.btnNew)
        Me.RadPageViewPage1.Controls.Add(Me.txtDCSName)
        Me.RadPageViewPage1.Controls.Add(Me.lblbankcode)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(88.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1003, 339)
        Me.RadPageViewPage1.Text = "Secretary/DCS"
        '
        'txtHeadLoad
        '
        Me.txtHeadLoad.AutoCompleteDisplayMember = Nothing
        Me.txtHeadLoad.AutoCompleteValueMember = Nothing
        Me.txtHeadLoad.CalculationExpression = Nothing
        Me.txtHeadLoad.DropDownAnimationEnabled = True
        Me.txtHeadLoad.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.txtHeadLoad.FieldCode = Nothing
        Me.txtHeadLoad.FieldDesc = Nothing
        Me.txtHeadLoad.FieldMaxLength = 0
        Me.txtHeadLoad.FieldName = Nothing
        Me.txtHeadLoad.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeadLoad.isCalculatedField = False
        Me.txtHeadLoad.IsSourceFromTable = False
        Me.txtHeadLoad.IsSourceFromValueList = False
        Me.txtHeadLoad.IsUnique = False
        RadListDataItem5.Text = "Yes"
        RadListDataItem6.Text = "No"
        Me.txtHeadLoad.Items.Add(RadListDataItem5)
        Me.txtHeadLoad.Items.Add(RadListDataItem6)
        Me.txtHeadLoad.Location = New System.Drawing.Point(494, 203)
        Me.txtHeadLoad.MendatroryField = True
        Me.txtHeadLoad.MyLinkLable1 = Me.MyLabel27
        Me.txtHeadLoad.MyLinkLable2 = Nothing
        Me.txtHeadLoad.Name = "txtHeadLoad"
        Me.txtHeadLoad.ReferenceFieldDesc = Nothing
        Me.txtHeadLoad.ReferenceFieldName = Nothing
        Me.txtHeadLoad.ReferenceTableName = Nothing
        Me.txtHeadLoad.Size = New System.Drawing.Size(244, 18)
        Me.txtHeadLoad.TabIndex = 348
        '
        'MyLabel27
        '
        Me.MyLabel27.FieldName = Nothing
        Me.MyLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel27.Location = New System.Drawing.Point(14, 65)
        Me.MyLabel27.Name = "MyLabel27"
        Me.MyLabel27.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel27.TabIndex = 398
        Me.MyLabel27.Text = "Bank Name"
        '
        'chkOwnBMC
        '
        Me.chkOwnBMC.Location = New System.Drawing.Point(494, 130)
        Me.chkOwnBMC.Name = "chkOwnBMC"
        Me.chkOwnBMC.Size = New System.Drawing.Size(70, 18)
        Me.chkOwnBMC.TabIndex = 348
        Me.chkOwnBMC.Text = "Own BMC"
        '
        'ddlGender
        '
        Me.ddlGender.AutoCompleteDisplayMember = Nothing
        Me.ddlGender.AutoCompleteValueMember = Nothing
        Me.ddlGender.CalculationExpression = Nothing
        Me.ddlGender.DropDownAnimationEnabled = True
        Me.ddlGender.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlGender.FieldCode = Nothing
        Me.ddlGender.FieldDesc = Nothing
        Me.ddlGender.FieldMaxLength = 0
        Me.ddlGender.FieldName = Nothing
        Me.ddlGender.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlGender.isCalculatedField = False
        Me.ddlGender.IsSourceFromTable = False
        Me.ddlGender.IsSourceFromValueList = False
        Me.ddlGender.IsUnique = False
        RadListDataItem1.Text = "Male"
        RadListDataItem2.Text = "Female"
        Me.ddlGender.Items.Add(RadListDataItem1)
        Me.ddlGender.Items.Add(RadListDataItem2)
        Me.ddlGender.Location = New System.Drawing.Point(870, 49)
        Me.ddlGender.MendatroryField = True
        Me.ddlGender.MyLinkLable1 = Me.MyLabel27
        Me.ddlGender.MyLinkLable2 = Nothing
        Me.ddlGender.Name = "ddlGender"
        Me.ddlGender.ReferenceFieldDesc = Nothing
        Me.ddlGender.ReferenceFieldName = Nothing
        Me.ddlGender.ReferenceTableName = Nothing
        Me.ddlGender.Size = New System.Drawing.Size(121, 18)
        Me.ddlGender.TabIndex = 347
        '
        'chkDefaultValue
        '
        Me.chkDefaultValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDefaultValue.Location = New System.Drawing.Point(650, 283)
        Me.chkDefaultValue.Name = "chkDefaultValue"
        Me.chkDefaultValue.Size = New System.Drawing.Size(56, 16)
        Me.chkDefaultValue.TabIndex = 328
        Me.chkDefaultValue.Text = "Default"
        '
        'chkInActive
        '
        Me.chkInActive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInActive.Location = New System.Drawing.Point(494, 283)
        Me.chkInActive.Name = "chkInActive"
        Me.chkInActive.Size = New System.Drawing.Size(64, 16)
        Me.chkInActive.TabIndex = 327
        Me.chkInActive.Text = "In-Active"
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(13, 30)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel14.TabIndex = 61
        Me.MyLabel14.Text = "DCS Name"
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(752, 205)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(112, 16)
        Me.MyLabel13.TabIndex = 324
        Me.MyLabel13.Text = "Cow Price Start Date"
        '
        'txtCowPriceStartDate
        '
        Me.txtCowPriceStartDate.CalculationExpression = Nothing
        Me.txtCowPriceStartDate.CustomFormat = "dd/MM/yyyy"
        Me.txtCowPriceStartDate.FieldCode = Nothing
        Me.txtCowPriceStartDate.FieldDesc = Nothing
        Me.txtCowPriceStartDate.FieldMaxLength = 0
        Me.txtCowPriceStartDate.FieldName = Nothing
        Me.txtCowPriceStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCowPriceStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtCowPriceStartDate.isCalculatedField = False
        Me.txtCowPriceStartDate.IsSourceFromTable = False
        Me.txtCowPriceStartDate.IsSourceFromValueList = False
        Me.txtCowPriceStartDate.IsUnique = False
        Me.txtCowPriceStartDate.Location = New System.Drawing.Point(870, 201)
        Me.txtCowPriceStartDate.MendatroryField = False
        Me.txtCowPriceStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtCowPriceStartDate.MyLinkLable1 = Nothing
        Me.txtCowPriceStartDate.MyLinkLable2 = Nothing
        Me.txtCowPriceStartDate.Name = "txtCowPriceStartDate"
        Me.txtCowPriceStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtCowPriceStartDate.ReferenceFieldDesc = Nothing
        Me.txtCowPriceStartDate.ReferenceFieldName = Nothing
        Me.txtCowPriceStartDate.ReferenceTableName = Nothing
        Me.txtCowPriceStartDate.Size = New System.Drawing.Size(121, 18)
        Me.txtCowPriceStartDate.TabIndex = 323
        Me.txtCowPriceStartDate.TabStop = False
        Me.txtCowPriceStartDate.Text = "13/06/2011"
        Me.txtCowPriceStartDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'chkApplyCowPrice
        '
        Me.chkApplyCowPrice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkApplyCowPrice.Location = New System.Drawing.Point(870, 132)
        Me.chkApplyCowPrice.Name = "chkApplyCowPrice"
        Me.chkApplyCowPrice.Size = New System.Drawing.Size(103, 16)
        Me.chkApplyCowPrice.TabIndex = 326
        Me.chkApplyCowPrice.Text = "Apply Cow Price"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(752, 51)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel12.TabIndex = 76
        Me.MyLabel12.Text = "Gender"
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(389, 248)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(89, 16)
        Me.MyLabel11.TabIndex = 75
        Me.MyLabel11.Text = "Head Load Rate"
        '
        'txtHeadLoadRate
        '
        Me.txtHeadLoadRate.CalculationExpression = Nothing
        Me.txtHeadLoadRate.FieldCode = Nothing
        Me.txtHeadLoadRate.FieldDesc = Nothing
        Me.txtHeadLoadRate.FieldMaxLength = 0
        Me.txtHeadLoadRate.FieldName = Nothing
        Me.txtHeadLoadRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeadLoadRate.isCalculatedField = False
        Me.txtHeadLoadRate.IsSourceFromTable = False
        Me.txtHeadLoadRate.IsSourceFromValueList = False
        Me.txtHeadLoadRate.IsUnique = False
        Me.txtHeadLoadRate.Location = New System.Drawing.Point(494, 245)
        Me.txtHeadLoadRate.MaxLength = 50
        Me.txtHeadLoadRate.MendatroryField = True
        Me.txtHeadLoadRate.MyLinkLable1 = Me.lblbankcode
        Me.txtHeadLoadRate.MyLinkLable2 = Nothing
        Me.txtHeadLoadRate.Name = "txtHeadLoadRate"
        Me.txtHeadLoadRate.ReadOnly = True
        Me.txtHeadLoadRate.ReferenceFieldDesc = Nothing
        Me.txtHeadLoadRate.ReferenceFieldName = Nothing
        Me.txtHeadLoadRate.ReferenceTableName = Nothing
        Me.txtHeadLoadRate.Size = New System.Drawing.Size(244, 18)
        Me.txtHeadLoadRate.TabIndex = 74
        Me.txtHeadLoadRate.TabStop = False
        '
        'lblbankcode
        '
        Me.lblbankcode.FieldName = Nothing
        Me.lblbankcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbankcode.Location = New System.Drawing.Point(13, 10)
        Me.lblbankcode.Name = "lblbankcode"
        Me.lblbankcode.Size = New System.Drawing.Size(60, 16)
        Me.lblbankcode.TabIndex = 60
        Me.lblbankcode.Text = "DCS Code"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(389, 227)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel10.TabIndex = 73
        Me.MyLabel10.Text = "Head Load Basi"
        '
        'txtHeadLoadBasi
        '
        Me.txtHeadLoadBasi.CalculationExpression = Nothing
        Me.txtHeadLoadBasi.FieldCode = Nothing
        Me.txtHeadLoadBasi.FieldDesc = Nothing
        Me.txtHeadLoadBasi.FieldMaxLength = 0
        Me.txtHeadLoadBasi.FieldName = Nothing
        Me.txtHeadLoadBasi.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeadLoadBasi.isCalculatedField = False
        Me.txtHeadLoadBasi.IsSourceFromTable = False
        Me.txtHeadLoadBasi.IsSourceFromValueList = False
        Me.txtHeadLoadBasi.IsUnique = False
        Me.txtHeadLoadBasi.Location = New System.Drawing.Point(494, 224)
        Me.txtHeadLoadBasi.MaxLength = 50
        Me.txtHeadLoadBasi.MendatroryField = True
        Me.txtHeadLoadBasi.MyLinkLable1 = Me.lblbankcode
        Me.txtHeadLoadBasi.MyLinkLable2 = Nothing
        Me.txtHeadLoadBasi.Name = "txtHeadLoadBasi"
        Me.txtHeadLoadBasi.ReadOnly = True
        Me.txtHeadLoadBasi.ReferenceFieldDesc = Nothing
        Me.txtHeadLoadBasi.ReferenceFieldName = Nothing
        Me.txtHeadLoadBasi.ReferenceTableName = Nothing
        Me.txtHeadLoadBasi.Size = New System.Drawing.Size(244, 18)
        Me.txtHeadLoadBasi.TabIndex = 72
        Me.txtHeadLoadBasi.TabStop = False
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(389, 204)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel9.TabIndex = 71
        Me.MyLabel9.Text = "Head Load"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(389, 150)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel8.TabIndex = 324
        Me.MyLabel8.Text = "Own BMC Date"
        '
        'txtOwnBMCDate
        '
        Me.txtOwnBMCDate.CalculationExpression = Nothing
        Me.txtOwnBMCDate.CustomFormat = "dd/MM/yyyy"
        Me.txtOwnBMCDate.FieldCode = Nothing
        Me.txtOwnBMCDate.FieldDesc = Nothing
        Me.txtOwnBMCDate.FieldMaxLength = 0
        Me.txtOwnBMCDate.FieldName = Nothing
        Me.txtOwnBMCDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOwnBMCDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtOwnBMCDate.isCalculatedField = False
        Me.txtOwnBMCDate.IsSourceFromTable = False
        Me.txtOwnBMCDate.IsSourceFromValueList = False
        Me.txtOwnBMCDate.IsUnique = False
        Me.txtOwnBMCDate.Location = New System.Drawing.Point(494, 150)
        Me.txtOwnBMCDate.MendatroryField = False
        Me.txtOwnBMCDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtOwnBMCDate.MyLinkLable1 = Nothing
        Me.txtOwnBMCDate.MyLinkLable2 = Nothing
        Me.txtOwnBMCDate.Name = "txtOwnBMCDate"
        Me.txtOwnBMCDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtOwnBMCDate.ReferenceFieldDesc = Nothing
        Me.txtOwnBMCDate.ReferenceFieldName = Nothing
        Me.txtOwnBMCDate.ReferenceTableName = Nothing
        Me.txtOwnBMCDate.Size = New System.Drawing.Size(139, 18)
        Me.txtOwnBMCDate.TabIndex = 323
        Me.txtOwnBMCDate.TabStop = False
        Me.txtOwnBMCDate.Text = "13/06/2011"
        Me.txtOwnBMCDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(389, 51)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel6.TabIndex = 322
        Me.MyLabel6.Text = "Registration Date"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(389, 93)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel5.TabIndex = 67
        Me.MyLabel5.Text = "DCS Route Code"
        '
        'txtDCSRouteCode
        '
        Me.txtDCSRouteCode.CalculationExpression = Nothing
        Me.txtDCSRouteCode.FieldCode = Nothing
        Me.txtDCSRouteCode.FieldDesc = Nothing
        Me.txtDCSRouteCode.FieldMaxLength = 0
        Me.txtDCSRouteCode.FieldName = Nothing
        Me.txtDCSRouteCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDCSRouteCode.isCalculatedField = False
        Me.txtDCSRouteCode.IsSourceFromTable = False
        Me.txtDCSRouteCode.IsSourceFromValueList = False
        Me.txtDCSRouteCode.IsUnique = False
        Me.txtDCSRouteCode.Location = New System.Drawing.Point(494, 90)
        Me.txtDCSRouteCode.MaxLength = 50
        Me.txtDCSRouteCode.MendatroryField = True
        Me.txtDCSRouteCode.MyLinkLable1 = Me.lblbankcode
        Me.txtDCSRouteCode.MyLinkLable2 = Nothing
        Me.txtDCSRouteCode.Name = "txtDCSRouteCode"
        Me.txtDCSRouteCode.ReadOnly = True
        Me.txtDCSRouteCode.ReferenceFieldDesc = Nothing
        Me.txtDCSRouteCode.ReferenceFieldName = Nothing
        Me.txtDCSRouteCode.ReferenceTableName = Nothing
        Me.txtDCSRouteCode.Size = New System.Drawing.Size(244, 18)
        Me.txtDCSRouteCode.TabIndex = 66
        Me.txtDCSRouteCode.TabStop = False
        '
        'txtRegistrationDate
        '
        Me.txtRegistrationDate.CalculationExpression = Nothing
        Me.txtRegistrationDate.CustomFormat = "dd/MM/yyyy"
        Me.txtRegistrationDate.FieldCode = Nothing
        Me.txtRegistrationDate.FieldDesc = Nothing
        Me.txtRegistrationDate.FieldMaxLength = 0
        Me.txtRegistrationDate.FieldName = Nothing
        Me.txtRegistrationDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegistrationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtRegistrationDate.isCalculatedField = False
        Me.txtRegistrationDate.IsSourceFromTable = False
        Me.txtRegistrationDate.IsSourceFromValueList = False
        Me.txtRegistrationDate.IsUnique = False
        Me.txtRegistrationDate.Location = New System.Drawing.Point(494, 49)
        Me.txtRegistrationDate.MendatroryField = False
        Me.txtRegistrationDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRegistrationDate.MyLinkLable1 = Nothing
        Me.txtRegistrationDate.MyLinkLable2 = Nothing
        Me.txtRegistrationDate.Name = "txtRegistrationDate"
        Me.txtRegistrationDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRegistrationDate.ReferenceFieldDesc = Nothing
        Me.txtRegistrationDate.ReferenceFieldName = Nothing
        Me.txtRegistrationDate.ReferenceTableName = Nothing
        Me.txtRegistrationDate.Size = New System.Drawing.Size(139, 18)
        Me.txtRegistrationDate.TabIndex = 319
        Me.txtRegistrationDate.TabStop = False
        Me.txtRegistrationDate.Text = "13/06/2011"
        Me.txtRegistrationDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(389, 31)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel4.TabIndex = 65
        Me.MyLabel4.Text = "Registration No."
        '
        'txtRegistrationNo
        '
        Me.txtRegistrationNo.CalculationExpression = Nothing
        Me.txtRegistrationNo.FieldCode = Nothing
        Me.txtRegistrationNo.FieldDesc = Nothing
        Me.txtRegistrationNo.FieldMaxLength = 0
        Me.txtRegistrationNo.FieldName = Nothing
        Me.txtRegistrationNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegistrationNo.isCalculatedField = False
        Me.txtRegistrationNo.IsSourceFromTable = False
        Me.txtRegistrationNo.IsSourceFromValueList = False
        Me.txtRegistrationNo.IsUnique = False
        Me.txtRegistrationNo.Location = New System.Drawing.Point(494, 28)
        Me.txtRegistrationNo.MaxLength = 50
        Me.txtRegistrationNo.MendatroryField = True
        Me.txtRegistrationNo.MyLinkLable1 = Me.lblbankcode
        Me.txtRegistrationNo.MyLinkLable2 = Nothing
        Me.txtRegistrationNo.Name = "txtRegistrationNo"
        Me.txtRegistrationNo.ReadOnly = True
        Me.txtRegistrationNo.ReferenceFieldDesc = Nothing
        Me.txtRegistrationNo.ReferenceFieldName = Nothing
        Me.txtRegistrationNo.ReferenceTableName = Nothing
        Me.txtRegistrationNo.Size = New System.Drawing.Size(244, 18)
        Me.txtRegistrationNo.TabIndex = 64
        Me.txtRegistrationNo.TabStop = False
        '
        'chkPDCS
        '
        Me.chkPDCS.Location = New System.Drawing.Point(870, 8)
        Me.chkPDCS.Name = "chkPDCS"
        Me.chkPDCS.Size = New System.Drawing.Size(47, 18)
        Me.chkPDCS.TabIndex = 65
        Me.chkPDCS.Text = "PDCS"
        '
        'chkRegistered
        '
        Me.chkRegistered.Location = New System.Drawing.Point(494, 9)
        Me.chkRegistered.Name = "chkRegistered"
        Me.chkRegistered.Size = New System.Drawing.Size(73, 18)
        Me.chkRegistered.TabIndex = 64
        Me.chkRegistered.Text = "Registered"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(13, 204)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel3.TabIndex = 63
        Me.MyLabel3.Text = "Other Details"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(13, 132)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel2.TabIndex = 63
        Me.MyLabel2.Text = "Bank Details"
        '
        'txtOtherDetails
        '
        Me.txtOtherDetails.CalculationExpression = Nothing
        Me.txtOtherDetails.FieldCode = Nothing
        Me.txtOtherDetails.FieldDesc = Nothing
        Me.txtOtherDetails.FieldMaxLength = 0
        Me.txtOtherDetails.FieldName = Nothing
        Me.txtOtherDetails.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOtherDetails.isCalculatedField = False
        Me.txtOtherDetails.IsSourceFromTable = False
        Me.txtOtherDetails.IsSourceFromValueList = False
        Me.txtOtherDetails.IsUnique = False
        Me.txtOtherDetails.Location = New System.Drawing.Point(136, 203)
        Me.txtOtherDetails.MaxLength = 50
        Me.txtOtherDetails.MendatroryField = True
        Me.txtOtherDetails.MyLinkLable1 = Me.lblbankcode
        Me.txtOtherDetails.MyLinkLable2 = Nothing
        Me.txtOtherDetails.Name = "txtOtherDetails"
        Me.txtOtherDetails.ReadOnly = True
        Me.txtOtherDetails.ReferenceFieldDesc = Nothing
        Me.txtOtherDetails.ReferenceFieldName = Nothing
        Me.txtOtherDetails.ReferenceTableName = Nothing
        Me.txtOtherDetails.Size = New System.Drawing.Size(244, 18)
        Me.txtOtherDetails.TabIndex = 62
        Me.txtOtherDetails.TabStop = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(13, 93)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(109, 16)
        Me.MyLabel1.TabIndex = 61
        Me.MyLabel1.Text = "DCS Uploader Code"
        '
        'txtBankDetails
        '
        Me.txtBankDetails.CalculationExpression = Nothing
        Me.txtBankDetails.FieldCode = Nothing
        Me.txtBankDetails.FieldDesc = Nothing
        Me.txtBankDetails.FieldMaxLength = 0
        Me.txtBankDetails.FieldName = Nothing
        Me.txtBankDetails.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankDetails.isCalculatedField = False
        Me.txtBankDetails.IsSourceFromTable = False
        Me.txtBankDetails.IsSourceFromValueList = False
        Me.txtBankDetails.IsUnique = False
        Me.txtBankDetails.Location = New System.Drawing.Point(136, 131)
        Me.txtBankDetails.MaxLength = 50
        Me.txtBankDetails.MendatroryField = True
        Me.txtBankDetails.MyLinkLable1 = Me.lblbankcode
        Me.txtBankDetails.MyLinkLable2 = Nothing
        Me.txtBankDetails.Name = "txtBankDetails"
        Me.txtBankDetails.ReadOnly = True
        Me.txtBankDetails.ReferenceFieldDesc = Nothing
        Me.txtBankDetails.ReferenceFieldName = Nothing
        Me.txtBankDetails.ReferenceTableName = Nothing
        Me.txtBankDetails.Size = New System.Drawing.Size(244, 18)
        Me.txtBankDetails.TabIndex = 62
        Me.txtBankDetails.TabStop = False
        '
        'txtDCSUploaderCode
        '
        Me.txtDCSUploaderCode.CalculationExpression = Nothing
        Me.txtDCSUploaderCode.FieldCode = Nothing
        Me.txtDCSUploaderCode.FieldDesc = Nothing
        Me.txtDCSUploaderCode.FieldMaxLength = 0
        Me.txtDCSUploaderCode.FieldName = Nothing
        Me.txtDCSUploaderCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDCSUploaderCode.isCalculatedField = False
        Me.txtDCSUploaderCode.IsSourceFromTable = False
        Me.txtDCSUploaderCode.IsSourceFromValueList = False
        Me.txtDCSUploaderCode.IsUnique = False
        Me.txtDCSUploaderCode.Location = New System.Drawing.Point(136, 92)
        Me.txtDCSUploaderCode.MaxLength = 50
        Me.txtDCSUploaderCode.MendatroryField = True
        Me.txtDCSUploaderCode.MyLinkLable1 = Me.lblbankcode
        Me.txtDCSUploaderCode.MyLinkLable2 = Nothing
        Me.txtDCSUploaderCode.Name = "txtDCSUploaderCode"
        Me.txtDCSUploaderCode.ReadOnly = True
        Me.txtDCSUploaderCode.ReferenceFieldDesc = Nothing
        Me.txtDCSUploaderCode.ReferenceFieldName = Nothing
        Me.txtDCSUploaderCode.ReferenceTableName = Nothing
        Me.txtDCSUploaderCode.Size = New System.Drawing.Size(244, 18)
        Me.txtDCSUploaderCode.TabIndex = 60
        Me.txtDCSUploaderCode.TabStop = False
        '
        'fndDCSCode
        '
        Me.fndDCSCode.FieldName = Nothing
        Me.fndDCSCode.Location = New System.Drawing.Point(136, 9)
        Me.fndDCSCode.MendatroryField = True
        Me.fndDCSCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndDCSCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDCSCode.MyLinkLable1 = Nothing
        Me.fndDCSCode.MyLinkLable2 = Nothing
        Me.fndDCSCode.MyMaxLength = 32767
        Me.fndDCSCode.MyReadOnly = False
        Me.fndDCSCode.Name = "fndDCSCode"
        Me.fndDCSCode.Size = New System.Drawing.Size(225, 18)
        Me.fndDCSCode.TabIndex = 61
        Me.fndDCSCode.TabStop = False
        Me.fndDCSCode.Value = ""
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(362, 9)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(18, 19)
        Me.btnNew.TabIndex = 62
        Me.btnNew.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'txtDCSName
        '
        Me.txtDCSName.CalculationExpression = Nothing
        Me.txtDCSName.FieldCode = Nothing
        Me.txtDCSName.FieldDesc = Nothing
        Me.txtDCSName.FieldMaxLength = 0
        Me.txtDCSName.FieldName = Nothing
        Me.txtDCSName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDCSName.isCalculatedField = False
        Me.txtDCSName.IsSourceFromTable = False
        Me.txtDCSName.IsSourceFromValueList = False
        Me.txtDCSName.IsUnique = False
        Me.txtDCSName.Location = New System.Drawing.Point(136, 30)
        Me.txtDCSName.MaxLength = 50
        Me.txtDCSName.MendatroryField = True
        Me.txtDCSName.MyLinkLable1 = Me.lblbankcode
        Me.txtDCSName.MyLinkLable2 = Nothing
        Me.txtDCSName.Name = "txtDCSName"
        Me.txtDCSName.ReadOnly = True
        Me.txtDCSName.ReferenceFieldDesc = Nothing
        Me.txtDCSName.ReferenceFieldName = Nothing
        Me.txtDCSName.ReferenceTableName = Nothing
        Me.txtDCSName.Size = New System.Drawing.Size(244, 18)
        Me.txtDCSName.TabIndex = 59
        Me.txtDCSName.TabStop = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel21)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel20)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel19)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel18)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel17)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel16)
        Me.RadPageViewPage2.Controls.Add(Me.txtVidhanSabhaName)
        Me.RadPageViewPage2.Controls.Add(Me.txtPanchayatSamitiName)
        Me.RadPageViewPage2.Controls.Add(Me.txtGramPanchayatName)
        Me.RadPageViewPage2.Controls.Add(Me.txtRevenueVillageName)
        Me.RadPageViewPage2.Controls.Add(Me.txtZoneName)
        Me.RadPageViewPage2.Controls.Add(Me.txtBlockName)
        Me.RadPageViewPage2.Controls.Add(Me.txtDistrictName)
        Me.RadPageViewPage2.Controls.Add(Me.fndVidhanSabha)
        Me.RadPageViewPage2.Controls.Add(Me.fndPanchayatSamiti)
        Me.RadPageViewPage2.Controls.Add(Me.fndGramPanchayat)
        Me.RadPageViewPage2.Controls.Add(Me.fndRevenueVillage)
        Me.RadPageViewPage2.Controls.Add(Me.fndZone)
        Me.RadPageViewPage2.Controls.Add(Me.fndBlock)
        Me.RadPageViewPage2.Controls.Add(Me.fndDistrict)
        Me.RadPageViewPage2.Controls.Add(Me.fndSupervisorCode)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel38)
        Me.RadPageViewPage2.Controls.Add(Me.txtSupervisorName)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel15)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(122.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1003, 339)
        Me.RadPageViewPage2.Text = "Supervisor and Other"
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(6, 184)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel21.TabIndex = 364
        Me.MyLabel21.Text = "Vidhan Sabha"
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(6, 161)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel20.TabIndex = 364
        Me.MyLabel20.Text = "Panchayat Samiti"
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(6, 138)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel19.TabIndex = 364
        Me.MyLabel19.Text = "Gram Panchayat"
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(6, 114)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(89, 16)
        Me.MyLabel18.TabIndex = 364
        Me.MyLabel18.Text = "Revenue Village"
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(6, 90)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel17.TabIndex = 364
        Me.MyLabel17.Text = "Zone"
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(6, 66)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(34, 16)
        Me.MyLabel16.TabIndex = 374
        Me.MyLabel16.Text = "Block"
        '
        'txtVidhanSabhaName
        '
        Me.txtVidhanSabhaName.CalculationExpression = Nothing
        Me.txtVidhanSabhaName.FieldCode = Nothing
        Me.txtVidhanSabhaName.FieldDesc = Nothing
        Me.txtVidhanSabhaName.FieldMaxLength = 0
        Me.txtVidhanSabhaName.FieldName = Nothing
        Me.txtVidhanSabhaName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVidhanSabhaName.isCalculatedField = False
        Me.txtVidhanSabhaName.IsSourceFromTable = False
        Me.txtVidhanSabhaName.IsSourceFromValueList = False
        Me.txtVidhanSabhaName.IsUnique = False
        Me.txtVidhanSabhaName.Location = New System.Drawing.Point(382, 181)
        Me.txtVidhanSabhaName.MaxLength = 50
        Me.txtVidhanSabhaName.MendatroryField = True
        Me.txtVidhanSabhaName.MyLinkLable1 = Me.MyLabel15
        Me.txtVidhanSabhaName.MyLinkLable2 = Nothing
        Me.txtVidhanSabhaName.Name = "txtVidhanSabhaName"
        Me.txtVidhanSabhaName.ReadOnly = True
        Me.txtVidhanSabhaName.ReferenceFieldDesc = Nothing
        Me.txtVidhanSabhaName.ReferenceFieldName = Nothing
        Me.txtVidhanSabhaName.ReferenceTableName = Nothing
        Me.txtVidhanSabhaName.Size = New System.Drawing.Size(261, 18)
        Me.txtVidhanSabhaName.TabIndex = 63
        Me.txtVidhanSabhaName.TabStop = False
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(6, 18)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel15.TabIndex = 63
        Me.MyLabel15.Text = "Supervisor Code"
        '
        'txtPanchayatSamitiName
        '
        Me.txtPanchayatSamitiName.CalculationExpression = Nothing
        Me.txtPanchayatSamitiName.FieldCode = Nothing
        Me.txtPanchayatSamitiName.FieldDesc = Nothing
        Me.txtPanchayatSamitiName.FieldMaxLength = 0
        Me.txtPanchayatSamitiName.FieldName = Nothing
        Me.txtPanchayatSamitiName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPanchayatSamitiName.isCalculatedField = False
        Me.txtPanchayatSamitiName.IsSourceFromTable = False
        Me.txtPanchayatSamitiName.IsSourceFromValueList = False
        Me.txtPanchayatSamitiName.IsUnique = False
        Me.txtPanchayatSamitiName.Location = New System.Drawing.Point(382, 158)
        Me.txtPanchayatSamitiName.MaxLength = 50
        Me.txtPanchayatSamitiName.MendatroryField = True
        Me.txtPanchayatSamitiName.MyLinkLable1 = Me.MyLabel15
        Me.txtPanchayatSamitiName.MyLinkLable2 = Nothing
        Me.txtPanchayatSamitiName.Name = "txtPanchayatSamitiName"
        Me.txtPanchayatSamitiName.ReadOnly = True
        Me.txtPanchayatSamitiName.ReferenceFieldDesc = Nothing
        Me.txtPanchayatSamitiName.ReferenceFieldName = Nothing
        Me.txtPanchayatSamitiName.ReferenceTableName = Nothing
        Me.txtPanchayatSamitiName.Size = New System.Drawing.Size(261, 18)
        Me.txtPanchayatSamitiName.TabIndex = 63
        Me.txtPanchayatSamitiName.TabStop = False
        '
        'txtGramPanchayatName
        '
        Me.txtGramPanchayatName.CalculationExpression = Nothing
        Me.txtGramPanchayatName.FieldCode = Nothing
        Me.txtGramPanchayatName.FieldDesc = Nothing
        Me.txtGramPanchayatName.FieldMaxLength = 0
        Me.txtGramPanchayatName.FieldName = Nothing
        Me.txtGramPanchayatName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGramPanchayatName.isCalculatedField = False
        Me.txtGramPanchayatName.IsSourceFromTable = False
        Me.txtGramPanchayatName.IsSourceFromValueList = False
        Me.txtGramPanchayatName.IsUnique = False
        Me.txtGramPanchayatName.Location = New System.Drawing.Point(382, 135)
        Me.txtGramPanchayatName.MaxLength = 50
        Me.txtGramPanchayatName.MendatroryField = True
        Me.txtGramPanchayatName.MyLinkLable1 = Me.MyLabel15
        Me.txtGramPanchayatName.MyLinkLable2 = Nothing
        Me.txtGramPanchayatName.Name = "txtGramPanchayatName"
        Me.txtGramPanchayatName.ReadOnly = True
        Me.txtGramPanchayatName.ReferenceFieldDesc = Nothing
        Me.txtGramPanchayatName.ReferenceFieldName = Nothing
        Me.txtGramPanchayatName.ReferenceTableName = Nothing
        Me.txtGramPanchayatName.Size = New System.Drawing.Size(261, 18)
        Me.txtGramPanchayatName.TabIndex = 63
        Me.txtGramPanchayatName.TabStop = False
        '
        'txtRevenueVillageName
        '
        Me.txtRevenueVillageName.CalculationExpression = Nothing
        Me.txtRevenueVillageName.FieldCode = Nothing
        Me.txtRevenueVillageName.FieldDesc = Nothing
        Me.txtRevenueVillageName.FieldMaxLength = 0
        Me.txtRevenueVillageName.FieldName = Nothing
        Me.txtRevenueVillageName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRevenueVillageName.isCalculatedField = False
        Me.txtRevenueVillageName.IsSourceFromTable = False
        Me.txtRevenueVillageName.IsSourceFromValueList = False
        Me.txtRevenueVillageName.IsUnique = False
        Me.txtRevenueVillageName.Location = New System.Drawing.Point(382, 111)
        Me.txtRevenueVillageName.MaxLength = 50
        Me.txtRevenueVillageName.MendatroryField = True
        Me.txtRevenueVillageName.MyLinkLable1 = Me.MyLabel15
        Me.txtRevenueVillageName.MyLinkLable2 = Nothing
        Me.txtRevenueVillageName.Name = "txtRevenueVillageName"
        Me.txtRevenueVillageName.ReadOnly = True
        Me.txtRevenueVillageName.ReferenceFieldDesc = Nothing
        Me.txtRevenueVillageName.ReferenceFieldName = Nothing
        Me.txtRevenueVillageName.ReferenceTableName = Nothing
        Me.txtRevenueVillageName.Size = New System.Drawing.Size(261, 18)
        Me.txtRevenueVillageName.TabIndex = 63
        Me.txtRevenueVillageName.TabStop = False
        '
        'txtZoneName
        '
        Me.txtZoneName.CalculationExpression = Nothing
        Me.txtZoneName.FieldCode = Nothing
        Me.txtZoneName.FieldDesc = Nothing
        Me.txtZoneName.FieldMaxLength = 0
        Me.txtZoneName.FieldName = Nothing
        Me.txtZoneName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtZoneName.isCalculatedField = False
        Me.txtZoneName.IsSourceFromTable = False
        Me.txtZoneName.IsSourceFromValueList = False
        Me.txtZoneName.IsUnique = False
        Me.txtZoneName.Location = New System.Drawing.Point(382, 87)
        Me.txtZoneName.MaxLength = 50
        Me.txtZoneName.MendatroryField = True
        Me.txtZoneName.MyLinkLable1 = Me.MyLabel15
        Me.txtZoneName.MyLinkLable2 = Nothing
        Me.txtZoneName.Name = "txtZoneName"
        Me.txtZoneName.ReadOnly = True
        Me.txtZoneName.ReferenceFieldDesc = Nothing
        Me.txtZoneName.ReferenceFieldName = Nothing
        Me.txtZoneName.ReferenceTableName = Nothing
        Me.txtZoneName.Size = New System.Drawing.Size(261, 18)
        Me.txtZoneName.TabIndex = 63
        Me.txtZoneName.TabStop = False
        '
        'txtBlockName
        '
        Me.txtBlockName.CalculationExpression = Nothing
        Me.txtBlockName.FieldCode = Nothing
        Me.txtBlockName.FieldDesc = Nothing
        Me.txtBlockName.FieldMaxLength = 0
        Me.txtBlockName.FieldName = Nothing
        Me.txtBlockName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBlockName.isCalculatedField = False
        Me.txtBlockName.IsSourceFromTable = False
        Me.txtBlockName.IsSourceFromValueList = False
        Me.txtBlockName.IsUnique = False
        Me.txtBlockName.Location = New System.Drawing.Point(382, 63)
        Me.txtBlockName.MaxLength = 50
        Me.txtBlockName.MendatroryField = True
        Me.txtBlockName.MyLinkLable1 = Me.MyLabel15
        Me.txtBlockName.MyLinkLable2 = Nothing
        Me.txtBlockName.Name = "txtBlockName"
        Me.txtBlockName.ReadOnly = True
        Me.txtBlockName.ReferenceFieldDesc = Nothing
        Me.txtBlockName.ReferenceFieldName = Nothing
        Me.txtBlockName.ReferenceTableName = Nothing
        Me.txtBlockName.Size = New System.Drawing.Size(261, 18)
        Me.txtBlockName.TabIndex = 373
        Me.txtBlockName.TabStop = False
        '
        'txtDistrictName
        '
        Me.txtDistrictName.CalculationExpression = Nothing
        Me.txtDistrictName.FieldCode = Nothing
        Me.txtDistrictName.FieldDesc = Nothing
        Me.txtDistrictName.FieldMaxLength = 0
        Me.txtDistrictName.FieldName = Nothing
        Me.txtDistrictName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDistrictName.isCalculatedField = False
        Me.txtDistrictName.IsSourceFromTable = False
        Me.txtDistrictName.IsSourceFromValueList = False
        Me.txtDistrictName.IsUnique = False
        Me.txtDistrictName.Location = New System.Drawing.Point(382, 39)
        Me.txtDistrictName.MaxLength = 50
        Me.txtDistrictName.MendatroryField = True
        Me.txtDistrictName.MyLinkLable1 = Me.MyLabel15
        Me.txtDistrictName.MyLinkLable2 = Nothing
        Me.txtDistrictName.Name = "txtDistrictName"
        Me.txtDistrictName.ReadOnly = True
        Me.txtDistrictName.ReferenceFieldDesc = Nothing
        Me.txtDistrictName.ReferenceFieldName = Nothing
        Me.txtDistrictName.ReferenceTableName = Nothing
        Me.txtDistrictName.Size = New System.Drawing.Size(261, 18)
        Me.txtDistrictName.TabIndex = 63
        Me.txtDistrictName.TabStop = False
        '
        'fndVidhanSabha
        '
        Me.fndVidhanSabha.CalculationExpression = Nothing
        Me.fndVidhanSabha.FieldCode = Nothing
        Me.fndVidhanSabha.FieldDesc = Nothing
        Me.fndVidhanSabha.FieldMaxLength = 0
        Me.fndVidhanSabha.FieldName = Nothing
        Me.fndVidhanSabha.isCalculatedField = False
        Me.fndVidhanSabha.IsSourceFromTable = False
        Me.fndVidhanSabha.IsSourceFromValueList = False
        Me.fndVidhanSabha.IsUnique = False
        Me.fndVidhanSabha.Location = New System.Drawing.Point(123, 181)
        Me.fndVidhanSabha.MendatroryField = True
        Me.fndVidhanSabha.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVidhanSabha.MyLinkLable1 = Nothing
        Me.fndVidhanSabha.MyLinkLable2 = Nothing
        Me.fndVidhanSabha.MyReadOnly = False
        Me.fndVidhanSabha.MyShowMasterFormButton = False
        Me.fndVidhanSabha.Name = "fndVidhanSabha"
        Me.fndVidhanSabha.ReferenceFieldDesc = Nothing
        Me.fndVidhanSabha.ReferenceFieldName = Nothing
        Me.fndVidhanSabha.ReferenceTableName = Nothing
        Me.fndVidhanSabha.Size = New System.Drawing.Size(239, 18)
        Me.fndVidhanSabha.TabIndex = 372
        Me.fndVidhanSabha.Value = ""
        '
        'fndPanchayatSamiti
        '
        Me.fndPanchayatSamiti.CalculationExpression = Nothing
        Me.fndPanchayatSamiti.FieldCode = Nothing
        Me.fndPanchayatSamiti.FieldDesc = Nothing
        Me.fndPanchayatSamiti.FieldMaxLength = 0
        Me.fndPanchayatSamiti.FieldName = Nothing
        Me.fndPanchayatSamiti.isCalculatedField = False
        Me.fndPanchayatSamiti.IsSourceFromTable = False
        Me.fndPanchayatSamiti.IsSourceFromValueList = False
        Me.fndPanchayatSamiti.IsUnique = False
        Me.fndPanchayatSamiti.Location = New System.Drawing.Point(123, 158)
        Me.fndPanchayatSamiti.MendatroryField = True
        Me.fndPanchayatSamiti.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPanchayatSamiti.MyLinkLable1 = Nothing
        Me.fndPanchayatSamiti.MyLinkLable2 = Nothing
        Me.fndPanchayatSamiti.MyReadOnly = False
        Me.fndPanchayatSamiti.MyShowMasterFormButton = False
        Me.fndPanchayatSamiti.Name = "fndPanchayatSamiti"
        Me.fndPanchayatSamiti.ReferenceFieldDesc = Nothing
        Me.fndPanchayatSamiti.ReferenceFieldName = Nothing
        Me.fndPanchayatSamiti.ReferenceTableName = Nothing
        Me.fndPanchayatSamiti.Size = New System.Drawing.Size(239, 18)
        Me.fndPanchayatSamiti.TabIndex = 371
        Me.fndPanchayatSamiti.Value = ""
        '
        'fndGramPanchayat
        '
        Me.fndGramPanchayat.CalculationExpression = Nothing
        Me.fndGramPanchayat.FieldCode = Nothing
        Me.fndGramPanchayat.FieldDesc = Nothing
        Me.fndGramPanchayat.FieldMaxLength = 0
        Me.fndGramPanchayat.FieldName = Nothing
        Me.fndGramPanchayat.isCalculatedField = False
        Me.fndGramPanchayat.IsSourceFromTable = False
        Me.fndGramPanchayat.IsSourceFromValueList = False
        Me.fndGramPanchayat.IsUnique = False
        Me.fndGramPanchayat.Location = New System.Drawing.Point(123, 135)
        Me.fndGramPanchayat.MendatroryField = True
        Me.fndGramPanchayat.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndGramPanchayat.MyLinkLable1 = Nothing
        Me.fndGramPanchayat.MyLinkLable2 = Nothing
        Me.fndGramPanchayat.MyReadOnly = False
        Me.fndGramPanchayat.MyShowMasterFormButton = False
        Me.fndGramPanchayat.Name = "fndGramPanchayat"
        Me.fndGramPanchayat.ReferenceFieldDesc = Nothing
        Me.fndGramPanchayat.ReferenceFieldName = Nothing
        Me.fndGramPanchayat.ReferenceTableName = Nothing
        Me.fndGramPanchayat.Size = New System.Drawing.Size(239, 18)
        Me.fndGramPanchayat.TabIndex = 370
        Me.fndGramPanchayat.Value = ""
        '
        'fndRevenueVillage
        '
        Me.fndRevenueVillage.CalculationExpression = Nothing
        Me.fndRevenueVillage.FieldCode = Nothing
        Me.fndRevenueVillage.FieldDesc = Nothing
        Me.fndRevenueVillage.FieldMaxLength = 0
        Me.fndRevenueVillage.FieldName = Nothing
        Me.fndRevenueVillage.isCalculatedField = False
        Me.fndRevenueVillage.IsSourceFromTable = False
        Me.fndRevenueVillage.IsSourceFromValueList = False
        Me.fndRevenueVillage.IsUnique = False
        Me.fndRevenueVillage.Location = New System.Drawing.Point(123, 111)
        Me.fndRevenueVillage.MendatroryField = True
        Me.fndRevenueVillage.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRevenueVillage.MyLinkLable1 = Nothing
        Me.fndRevenueVillage.MyLinkLable2 = Nothing
        Me.fndRevenueVillage.MyReadOnly = False
        Me.fndRevenueVillage.MyShowMasterFormButton = False
        Me.fndRevenueVillage.Name = "fndRevenueVillage"
        Me.fndRevenueVillage.ReferenceFieldDesc = Nothing
        Me.fndRevenueVillage.ReferenceFieldName = Nothing
        Me.fndRevenueVillage.ReferenceTableName = Nothing
        Me.fndRevenueVillage.Size = New System.Drawing.Size(239, 18)
        Me.fndRevenueVillage.TabIndex = 369
        Me.fndRevenueVillage.Value = ""
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
        Me.fndZone.Location = New System.Drawing.Point(123, 87)
        Me.fndZone.MendatroryField = True
        Me.fndZone.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndZone.MyLinkLable1 = Nothing
        Me.fndZone.MyLinkLable2 = Nothing
        Me.fndZone.MyReadOnly = False
        Me.fndZone.MyShowMasterFormButton = False
        Me.fndZone.Name = "fndZone"
        Me.fndZone.ReferenceFieldDesc = Nothing
        Me.fndZone.ReferenceFieldName = Nothing
        Me.fndZone.ReferenceTableName = Nothing
        Me.fndZone.Size = New System.Drawing.Size(239, 18)
        Me.fndZone.TabIndex = 368
        Me.fndZone.Value = ""
        '
        'fndBlock
        '
        Me.fndBlock.CalculationExpression = Nothing
        Me.fndBlock.FieldCode = Nothing
        Me.fndBlock.FieldDesc = Nothing
        Me.fndBlock.FieldMaxLength = 0
        Me.fndBlock.FieldName = Nothing
        Me.fndBlock.isCalculatedField = False
        Me.fndBlock.IsSourceFromTable = False
        Me.fndBlock.IsSourceFromValueList = False
        Me.fndBlock.IsUnique = False
        Me.fndBlock.Location = New System.Drawing.Point(123, 63)
        Me.fndBlock.MendatroryField = True
        Me.fndBlock.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBlock.MyLinkLable1 = Nothing
        Me.fndBlock.MyLinkLable2 = Nothing
        Me.fndBlock.MyReadOnly = False
        Me.fndBlock.MyShowMasterFormButton = False
        Me.fndBlock.Name = "fndBlock"
        Me.fndBlock.ReferenceFieldDesc = Nothing
        Me.fndBlock.ReferenceFieldName = Nothing
        Me.fndBlock.ReferenceTableName = Nothing
        Me.fndBlock.Size = New System.Drawing.Size(239, 18)
        Me.fndBlock.TabIndex = 367
        Me.fndBlock.Value = ""
        '
        'fndDistrict
        '
        Me.fndDistrict.CalculationExpression = Nothing
        Me.fndDistrict.FieldCode = Nothing
        Me.fndDistrict.FieldDesc = Nothing
        Me.fndDistrict.FieldMaxLength = 0
        Me.fndDistrict.FieldName = Nothing
        Me.fndDistrict.isCalculatedField = False
        Me.fndDistrict.IsSourceFromTable = False
        Me.fndDistrict.IsSourceFromValueList = False
        Me.fndDistrict.IsUnique = False
        Me.fndDistrict.Location = New System.Drawing.Point(123, 39)
        Me.fndDistrict.MendatroryField = True
        Me.fndDistrict.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDistrict.MyLinkLable1 = Nothing
        Me.fndDistrict.MyLinkLable2 = Nothing
        Me.fndDistrict.MyReadOnly = False
        Me.fndDistrict.MyShowMasterFormButton = False
        Me.fndDistrict.Name = "fndDistrict"
        Me.fndDistrict.ReferenceFieldDesc = Nothing
        Me.fndDistrict.ReferenceFieldName = Nothing
        Me.fndDistrict.ReferenceTableName = Nothing
        Me.fndDistrict.Size = New System.Drawing.Size(239, 18)
        Me.fndDistrict.TabIndex = 366
        Me.fndDistrict.Value = ""
        '
        'fndSupervisorCode
        '
        Me.fndSupervisorCode.CalculationExpression = Nothing
        Me.fndSupervisorCode.FieldCode = Nothing
        Me.fndSupervisorCode.FieldDesc = Nothing
        Me.fndSupervisorCode.FieldMaxLength = 0
        Me.fndSupervisorCode.FieldName = Nothing
        Me.fndSupervisorCode.isCalculatedField = False
        Me.fndSupervisorCode.IsSourceFromTable = False
        Me.fndSupervisorCode.IsSourceFromValueList = False
        Me.fndSupervisorCode.IsUnique = False
        Me.fndSupervisorCode.Location = New System.Drawing.Point(123, 15)
        Me.fndSupervisorCode.MendatroryField = True
        Me.fndSupervisorCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSupervisorCode.MyLinkLable1 = Nothing
        Me.fndSupervisorCode.MyLinkLable2 = Nothing
        Me.fndSupervisorCode.MyReadOnly = False
        Me.fndSupervisorCode.MyShowMasterFormButton = False
        Me.fndSupervisorCode.Name = "fndSupervisorCode"
        Me.fndSupervisorCode.ReferenceFieldDesc = Nothing
        Me.fndSupervisorCode.ReferenceFieldName = Nothing
        Me.fndSupervisorCode.ReferenceTableName = Nothing
        Me.fndSupervisorCode.Size = New System.Drawing.Size(239, 18)
        Me.fndSupervisorCode.TabIndex = 365
        Me.fndSupervisorCode.Value = ""
        '
        'MyLabel38
        '
        Me.MyLabel38.FieldName = Nothing
        Me.MyLabel38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel38.Location = New System.Drawing.Point(6, 42)
        Me.MyLabel38.Name = "MyLabel38"
        Me.MyLabel38.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel38.TabIndex = 363
        Me.MyLabel38.Text = "District"
        '
        'txtSupervisorName
        '
        Me.txtSupervisorName.CalculationExpression = Nothing
        Me.txtSupervisorName.FieldCode = Nothing
        Me.txtSupervisorName.FieldDesc = Nothing
        Me.txtSupervisorName.FieldMaxLength = 0
        Me.txtSupervisorName.FieldName = Nothing
        Me.txtSupervisorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupervisorName.isCalculatedField = False
        Me.txtSupervisorName.IsSourceFromTable = False
        Me.txtSupervisorName.IsSourceFromValueList = False
        Me.txtSupervisorName.IsUnique = False
        Me.txtSupervisorName.Location = New System.Drawing.Point(382, 15)
        Me.txtSupervisorName.MaxLength = 50
        Me.txtSupervisorName.MendatroryField = True
        Me.txtSupervisorName.MyLinkLable1 = Me.MyLabel15
        Me.txtSupervisorName.MyLinkLable2 = Nothing
        Me.txtSupervisorName.Name = "txtSupervisorName"
        Me.txtSupervisorName.ReadOnly = True
        Me.txtSupervisorName.ReferenceFieldDesc = Nothing
        Me.txtSupervisorName.ReferenceFieldName = Nothing
        Me.txtSupervisorName.ReferenceTableName = Nothing
        Me.txtSupervisorName.Size = New System.Drawing.Size(261, 18)
        Me.txtSupervisorName.TabIndex = 62
        Me.txtSupervisorName.TabStop = False
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel27)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel22)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel24)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel23)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel25)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel30)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel26)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel31)
        Me.RadPageViewPage3.Controls.Add(Me.txtBankIFSCCode1)
        Me.RadPageViewPage3.Controls.Add(Me.txtBankIFSCCode)
        Me.RadPageViewPage3.Controls.Add(Me.txtBankBranchName1)
        Me.RadPageViewPage3.Controls.Add(Me.txtBankBranchName)
        Me.RadPageViewPage3.Controls.Add(Me.txtBankAccountNo1)
        Me.RadPageViewPage3.Controls.Add(Me.txtBankAccountNo)
        Me.RadPageViewPage3.Controls.Add(Me.txtBankName1)
        Me.RadPageViewPage3.Controls.Add(Me.txtBankName)
        Me.RadPageViewPage3.Controls.Add(Me.txtDCSCurrentBankDetails1)
        Me.RadPageViewPage3.Controls.Add(Me.txtDCSCurrentBankDetails)
        Me.RadPageViewPage3.Controls.Add(Me.fndCompanyBank1)
        Me.RadPageViewPage3.Controls.Add(Me.fndCompanyBank)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel32)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel33)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel29)
        Me.RadPageViewPage3.Controls.Add(Me.txtCompanyBank1)
        Me.RadPageViewPage3.Controls.Add(Me.txtCompanyBankName)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel28)
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(77.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(1003, 339)
        Me.RadPageViewPage3.Text = "Bank Details"
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel22.Location = New System.Drawing.Point(14, 303)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel22.TabIndex = 408
        Me.MyLabel22.Text = "Bank IFSC Code"
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel24.Location = New System.Drawing.Point(14, 137)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel24.TabIndex = 387
        Me.MyLabel24.Text = "Bank IFSC Code"
        '
        'MyLabel23
        '
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel23.Location = New System.Drawing.Point(14, 279)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel23.TabIndex = 407
        Me.MyLabel23.Text = "Bank Branch"
        '
        'MyLabel25
        '
        Me.MyLabel25.FieldName = Nothing
        Me.MyLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel25.Location = New System.Drawing.Point(14, 113)
        Me.MyLabel25.Name = "MyLabel25"
        Me.MyLabel25.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel25.TabIndex = 386
        Me.MyLabel25.Text = "Bank Branch"
        '
        'MyLabel30
        '
        Me.MyLabel30.FieldName = Nothing
        Me.MyLabel30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel30.Location = New System.Drawing.Point(14, 255)
        Me.MyLabel30.Name = "MyLabel30"
        Me.MyLabel30.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel30.TabIndex = 406
        Me.MyLabel30.Text = "Bank Account No"
        '
        'MyLabel26
        '
        Me.MyLabel26.FieldName = Nothing
        Me.MyLabel26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel26.Location = New System.Drawing.Point(14, 89)
        Me.MyLabel26.Name = "MyLabel26"
        Me.MyLabel26.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel26.TabIndex = 385
        Me.MyLabel26.Text = "Bank Account No"
        '
        'MyLabel31
        '
        Me.MyLabel31.FieldName = Nothing
        Me.MyLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel31.Location = New System.Drawing.Point(14, 231)
        Me.MyLabel31.Name = "MyLabel31"
        Me.MyLabel31.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel31.TabIndex = 411
        Me.MyLabel31.Text = "Bank Name"
        '
        'txtBankIFSCCode1
        '
        Me.txtBankIFSCCode1.CalculationExpression = Nothing
        Me.txtBankIFSCCode1.FieldCode = Nothing
        Me.txtBankIFSCCode1.FieldDesc = Nothing
        Me.txtBankIFSCCode1.FieldMaxLength = 0
        Me.txtBankIFSCCode1.FieldName = Nothing
        Me.txtBankIFSCCode1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankIFSCCode1.isCalculatedField = False
        Me.txtBankIFSCCode1.IsSourceFromTable = False
        Me.txtBankIFSCCode1.IsSourceFromValueList = False
        Me.txtBankIFSCCode1.IsUnique = False
        Me.txtBankIFSCCode1.Location = New System.Drawing.Point(158, 300)
        Me.txtBankIFSCCode1.MaxLength = 50
        Me.txtBankIFSCCode1.MendatroryField = True
        Me.txtBankIFSCCode1.MyLinkLable1 = Nothing
        Me.txtBankIFSCCode1.MyLinkLable2 = Nothing
        Me.txtBankIFSCCode1.Name = "txtBankIFSCCode1"
        Me.txtBankIFSCCode1.ReadOnly = True
        Me.txtBankIFSCCode1.ReferenceFieldDesc = Nothing
        Me.txtBankIFSCCode1.ReferenceFieldName = Nothing
        Me.txtBankIFSCCode1.ReferenceTableName = Nothing
        Me.txtBankIFSCCode1.Size = New System.Drawing.Size(239, 18)
        Me.txtBankIFSCCode1.TabIndex = 404
        Me.txtBankIFSCCode1.TabStop = False
        '
        'txtBankIFSCCode
        '
        Me.txtBankIFSCCode.CalculationExpression = Nothing
        Me.txtBankIFSCCode.FieldCode = Nothing
        Me.txtBankIFSCCode.FieldDesc = Nothing
        Me.txtBankIFSCCode.FieldMaxLength = 0
        Me.txtBankIFSCCode.FieldName = Nothing
        Me.txtBankIFSCCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankIFSCCode.isCalculatedField = False
        Me.txtBankIFSCCode.IsSourceFromTable = False
        Me.txtBankIFSCCode.IsSourceFromValueList = False
        Me.txtBankIFSCCode.IsUnique = False
        Me.txtBankIFSCCode.Location = New System.Drawing.Point(158, 134)
        Me.txtBankIFSCCode.MaxLength = 50
        Me.txtBankIFSCCode.MendatroryField = True
        Me.txtBankIFSCCode.MyLinkLable1 = Me.MyLabel28
        Me.txtBankIFSCCode.MyLinkLable2 = Nothing
        Me.txtBankIFSCCode.Name = "txtBankIFSCCode"
        Me.txtBankIFSCCode.ReadOnly = True
        Me.txtBankIFSCCode.ReferenceFieldDesc = Nothing
        Me.txtBankIFSCCode.ReferenceFieldName = Nothing
        Me.txtBankIFSCCode.ReferenceTableName = Nothing
        Me.txtBankIFSCCode.Size = New System.Drawing.Size(239, 18)
        Me.txtBankIFSCCode.TabIndex = 382
        Me.txtBankIFSCCode.TabStop = False
        '
        'MyLabel28
        '
        Me.MyLabel28.FieldName = Nothing
        Me.MyLabel28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel28.Location = New System.Drawing.Point(14, 17)
        Me.MyLabel28.Name = "MyLabel28"
        Me.MyLabel28.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel28.TabIndex = 376
        Me.MyLabel28.Text = "Company Bank"
        '
        'txtBankBranchName1
        '
        Me.txtBankBranchName1.CalculationExpression = Nothing
        Me.txtBankBranchName1.FieldCode = Nothing
        Me.txtBankBranchName1.FieldDesc = Nothing
        Me.txtBankBranchName1.FieldMaxLength = 0
        Me.txtBankBranchName1.FieldName = Nothing
        Me.txtBankBranchName1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankBranchName1.isCalculatedField = False
        Me.txtBankBranchName1.IsSourceFromTable = False
        Me.txtBankBranchName1.IsSourceFromValueList = False
        Me.txtBankBranchName1.IsUnique = False
        Me.txtBankBranchName1.Location = New System.Drawing.Point(158, 276)
        Me.txtBankBranchName1.MaxLength = 50
        Me.txtBankBranchName1.MendatroryField = True
        Me.txtBankBranchName1.MyLinkLable1 = Me.MyLabel32
        Me.txtBankBranchName1.MyLinkLable2 = Nothing
        Me.txtBankBranchName1.Name = "txtBankBranchName1"
        Me.txtBankBranchName1.ReadOnly = True
        Me.txtBankBranchName1.ReferenceFieldDesc = Nothing
        Me.txtBankBranchName1.ReferenceFieldName = Nothing
        Me.txtBankBranchName1.ReferenceTableName = Nothing
        Me.txtBankBranchName1.Size = New System.Drawing.Size(239, 18)
        Me.txtBankBranchName1.TabIndex = 403
        Me.txtBankBranchName1.TabStop = False
        '
        'MyLabel32
        '
        Me.MyLabel32.FieldName = Nothing
        Me.MyLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel32.Location = New System.Drawing.Point(14, 183)
        Me.MyLabel32.Name = "MyLabel32"
        Me.MyLabel32.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel32.TabIndex = 400
        Me.MyLabel32.Text = "Company Bank"
        '
        'txtBankBranchName
        '
        Me.txtBankBranchName.CalculationExpression = Nothing
        Me.txtBankBranchName.FieldCode = Nothing
        Me.txtBankBranchName.FieldDesc = Nothing
        Me.txtBankBranchName.FieldMaxLength = 0
        Me.txtBankBranchName.FieldName = Nothing
        Me.txtBankBranchName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankBranchName.isCalculatedField = False
        Me.txtBankBranchName.IsSourceFromTable = False
        Me.txtBankBranchName.IsSourceFromValueList = False
        Me.txtBankBranchName.IsUnique = False
        Me.txtBankBranchName.Location = New System.Drawing.Point(158, 110)
        Me.txtBankBranchName.MaxLength = 50
        Me.txtBankBranchName.MendatroryField = True
        Me.txtBankBranchName.MyLinkLable1 = Me.MyLabel28
        Me.txtBankBranchName.MyLinkLable2 = Nothing
        Me.txtBankBranchName.Name = "txtBankBranchName"
        Me.txtBankBranchName.ReadOnly = True
        Me.txtBankBranchName.ReferenceFieldDesc = Nothing
        Me.txtBankBranchName.ReferenceFieldName = Nothing
        Me.txtBankBranchName.ReferenceTableName = Nothing
        Me.txtBankBranchName.Size = New System.Drawing.Size(239, 18)
        Me.txtBankBranchName.TabIndex = 379
        Me.txtBankBranchName.TabStop = False
        '
        'txtBankAccountNo1
        '
        Me.txtBankAccountNo1.CalculationExpression = Nothing
        Me.txtBankAccountNo1.FieldCode = Nothing
        Me.txtBankAccountNo1.FieldDesc = Nothing
        Me.txtBankAccountNo1.FieldMaxLength = 0
        Me.txtBankAccountNo1.FieldName = Nothing
        Me.txtBankAccountNo1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankAccountNo1.isCalculatedField = False
        Me.txtBankAccountNo1.IsSourceFromTable = False
        Me.txtBankAccountNo1.IsSourceFromValueList = False
        Me.txtBankAccountNo1.IsUnique = False
        Me.txtBankAccountNo1.Location = New System.Drawing.Point(158, 252)
        Me.txtBankAccountNo1.MaxLength = 50
        Me.txtBankAccountNo1.MendatroryField = True
        Me.txtBankAccountNo1.MyLinkLable1 = Me.MyLabel32
        Me.txtBankAccountNo1.MyLinkLable2 = Nothing
        Me.txtBankAccountNo1.Name = "txtBankAccountNo1"
        Me.txtBankAccountNo1.ReadOnly = True
        Me.txtBankAccountNo1.ReferenceFieldDesc = Nothing
        Me.txtBankAccountNo1.ReferenceFieldName = Nothing
        Me.txtBankAccountNo1.ReferenceTableName = Nothing
        Me.txtBankAccountNo1.Size = New System.Drawing.Size(239, 18)
        Me.txtBankAccountNo1.TabIndex = 402
        Me.txtBankAccountNo1.TabStop = False
        '
        'txtBankAccountNo
        '
        Me.txtBankAccountNo.CalculationExpression = Nothing
        Me.txtBankAccountNo.FieldCode = Nothing
        Me.txtBankAccountNo.FieldDesc = Nothing
        Me.txtBankAccountNo.FieldMaxLength = 0
        Me.txtBankAccountNo.FieldName = Nothing
        Me.txtBankAccountNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankAccountNo.isCalculatedField = False
        Me.txtBankAccountNo.IsSourceFromTable = False
        Me.txtBankAccountNo.IsSourceFromValueList = False
        Me.txtBankAccountNo.IsUnique = False
        Me.txtBankAccountNo.Location = New System.Drawing.Point(158, 86)
        Me.txtBankAccountNo.MaxLength = 50
        Me.txtBankAccountNo.MendatroryField = True
        Me.txtBankAccountNo.MyLinkLable1 = Me.MyLabel28
        Me.txtBankAccountNo.MyLinkLable2 = Nothing
        Me.txtBankAccountNo.Name = "txtBankAccountNo"
        Me.txtBankAccountNo.ReadOnly = True
        Me.txtBankAccountNo.ReferenceFieldDesc = Nothing
        Me.txtBankAccountNo.ReferenceFieldName = Nothing
        Me.txtBankAccountNo.ReferenceTableName = Nothing
        Me.txtBankAccountNo.Size = New System.Drawing.Size(239, 18)
        Me.txtBankAccountNo.TabIndex = 378
        Me.txtBankAccountNo.TabStop = False
        '
        'txtBankName1
        '
        Me.txtBankName1.CalculationExpression = Nothing
        Me.txtBankName1.FieldCode = Nothing
        Me.txtBankName1.FieldDesc = Nothing
        Me.txtBankName1.FieldMaxLength = 0
        Me.txtBankName1.FieldName = Nothing
        Me.txtBankName1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankName1.isCalculatedField = False
        Me.txtBankName1.IsSourceFromTable = False
        Me.txtBankName1.IsSourceFromValueList = False
        Me.txtBankName1.IsUnique = False
        Me.txtBankName1.Location = New System.Drawing.Point(158, 228)
        Me.txtBankName1.MaxLength = 50
        Me.txtBankName1.MendatroryField = True
        Me.txtBankName1.MyLinkLable1 = Me.MyLabel32
        Me.txtBankName1.MyLinkLable2 = Nothing
        Me.txtBankName1.Name = "txtBankName1"
        Me.txtBankName1.ReadOnly = True
        Me.txtBankName1.ReferenceFieldDesc = Nothing
        Me.txtBankName1.ReferenceFieldName = Nothing
        Me.txtBankName1.ReferenceTableName = Nothing
        Me.txtBankName1.Size = New System.Drawing.Size(239, 18)
        Me.txtBankName1.TabIndex = 410
        Me.txtBankName1.TabStop = False
        '
        'txtBankName
        '
        Me.txtBankName.CalculationExpression = Nothing
        Me.txtBankName.FieldCode = Nothing
        Me.txtBankName.FieldDesc = Nothing
        Me.txtBankName.FieldMaxLength = 0
        Me.txtBankName.FieldName = Nothing
        Me.txtBankName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankName.isCalculatedField = False
        Me.txtBankName.IsSourceFromTable = False
        Me.txtBankName.IsSourceFromValueList = False
        Me.txtBankName.IsUnique = False
        Me.txtBankName.Location = New System.Drawing.Point(158, 62)
        Me.txtBankName.MaxLength = 50
        Me.txtBankName.MendatroryField = True
        Me.txtBankName.MyLinkLable1 = Me.MyLabel28
        Me.txtBankName.MyLinkLable2 = Nothing
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.ReadOnly = True
        Me.txtBankName.ReferenceFieldDesc = Nothing
        Me.txtBankName.ReferenceFieldName = Nothing
        Me.txtBankName.ReferenceTableName = Nothing
        Me.txtBankName.Size = New System.Drawing.Size(239, 18)
        Me.txtBankName.TabIndex = 397
        Me.txtBankName.TabStop = False
        '
        'txtDCSCurrentBankDetails1
        '
        Me.txtDCSCurrentBankDetails1.CalculationExpression = Nothing
        Me.txtDCSCurrentBankDetails1.FieldCode = Nothing
        Me.txtDCSCurrentBankDetails1.FieldDesc = Nothing
        Me.txtDCSCurrentBankDetails1.FieldMaxLength = 0
        Me.txtDCSCurrentBankDetails1.FieldName = Nothing
        Me.txtDCSCurrentBankDetails1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDCSCurrentBankDetails1.isCalculatedField = False
        Me.txtDCSCurrentBankDetails1.IsSourceFromTable = False
        Me.txtDCSCurrentBankDetails1.IsSourceFromValueList = False
        Me.txtDCSCurrentBankDetails1.IsUnique = False
        Me.txtDCSCurrentBankDetails1.Location = New System.Drawing.Point(158, 204)
        Me.txtDCSCurrentBankDetails1.MaxLength = 50
        Me.txtDCSCurrentBankDetails1.MendatroryField = True
        Me.txtDCSCurrentBankDetails1.MyLinkLable1 = Me.MyLabel32
        Me.txtDCSCurrentBankDetails1.MyLinkLable2 = Nothing
        Me.txtDCSCurrentBankDetails1.Name = "txtDCSCurrentBankDetails1"
        Me.txtDCSCurrentBankDetails1.ReadOnly = True
        Me.txtDCSCurrentBankDetails1.ReferenceFieldDesc = Nothing
        Me.txtDCSCurrentBankDetails1.ReferenceFieldName = Nothing
        Me.txtDCSCurrentBankDetails1.ReferenceTableName = Nothing
        Me.txtDCSCurrentBankDetails1.Size = New System.Drawing.Size(239, 18)
        Me.txtDCSCurrentBankDetails1.TabIndex = 401
        Me.txtDCSCurrentBankDetails1.TabStop = False
        '
        'txtDCSCurrentBankDetails
        '
        Me.txtDCSCurrentBankDetails.CalculationExpression = Nothing
        Me.txtDCSCurrentBankDetails.FieldCode = Nothing
        Me.txtDCSCurrentBankDetails.FieldDesc = Nothing
        Me.txtDCSCurrentBankDetails.FieldMaxLength = 0
        Me.txtDCSCurrentBankDetails.FieldName = Nothing
        Me.txtDCSCurrentBankDetails.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDCSCurrentBankDetails.isCalculatedField = False
        Me.txtDCSCurrentBankDetails.IsSourceFromTable = False
        Me.txtDCSCurrentBankDetails.IsSourceFromValueList = False
        Me.txtDCSCurrentBankDetails.IsUnique = False
        Me.txtDCSCurrentBankDetails.Location = New System.Drawing.Point(158, 38)
        Me.txtDCSCurrentBankDetails.MaxLength = 50
        Me.txtDCSCurrentBankDetails.MendatroryField = True
        Me.txtDCSCurrentBankDetails.MyLinkLable1 = Me.MyLabel28
        Me.txtDCSCurrentBankDetails.MyLinkLable2 = Nothing
        Me.txtDCSCurrentBankDetails.Name = "txtDCSCurrentBankDetails"
        Me.txtDCSCurrentBankDetails.ReadOnly = True
        Me.txtDCSCurrentBankDetails.ReferenceFieldDesc = Nothing
        Me.txtDCSCurrentBankDetails.ReferenceFieldName = Nothing
        Me.txtDCSCurrentBankDetails.ReferenceTableName = Nothing
        Me.txtDCSCurrentBankDetails.Size = New System.Drawing.Size(239, 18)
        Me.txtDCSCurrentBankDetails.TabIndex = 377
        Me.txtDCSCurrentBankDetails.TabStop = False
        '
        'fndCompanyBank1
        '
        Me.fndCompanyBank1.CalculationExpression = Nothing
        Me.fndCompanyBank1.FieldCode = Nothing
        Me.fndCompanyBank1.FieldDesc = Nothing
        Me.fndCompanyBank1.FieldMaxLength = 0
        Me.fndCompanyBank1.FieldName = Nothing
        Me.fndCompanyBank1.isCalculatedField = False
        Me.fndCompanyBank1.IsSourceFromTable = False
        Me.fndCompanyBank1.IsSourceFromValueList = False
        Me.fndCompanyBank1.IsUnique = False
        Me.fndCompanyBank1.Location = New System.Drawing.Point(158, 180)
        Me.fndCompanyBank1.MendatroryField = True
        Me.fndCompanyBank1.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCompanyBank1.MyLinkLable1 = Nothing
        Me.fndCompanyBank1.MyLinkLable2 = Nothing
        Me.fndCompanyBank1.MyReadOnly = False
        Me.fndCompanyBank1.MyShowMasterFormButton = False
        Me.fndCompanyBank1.Name = "fndCompanyBank1"
        Me.fndCompanyBank1.ReferenceFieldDesc = Nothing
        Me.fndCompanyBank1.ReferenceFieldName = Nothing
        Me.fndCompanyBank1.ReferenceTableName = Nothing
        Me.fndCompanyBank1.Size = New System.Drawing.Size(239, 18)
        Me.fndCompanyBank1.TabIndex = 409
        Me.fndCompanyBank1.Value = ""
        '
        'fndCompanyBank
        '
        Me.fndCompanyBank.CalculationExpression = Nothing
        Me.fndCompanyBank.FieldCode = Nothing
        Me.fndCompanyBank.FieldDesc = Nothing
        Me.fndCompanyBank.FieldMaxLength = 0
        Me.fndCompanyBank.FieldName = Nothing
        Me.fndCompanyBank.isCalculatedField = False
        Me.fndCompanyBank.IsSourceFromTable = False
        Me.fndCompanyBank.IsSourceFromValueList = False
        Me.fndCompanyBank.IsUnique = False
        Me.fndCompanyBank.Location = New System.Drawing.Point(158, 14)
        Me.fndCompanyBank.MendatroryField = True
        Me.fndCompanyBank.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCompanyBank.MyLinkLable1 = Nothing
        Me.fndCompanyBank.MyLinkLable2 = Nothing
        Me.fndCompanyBank.MyReadOnly = False
        Me.fndCompanyBank.MyShowMasterFormButton = False
        Me.fndCompanyBank.Name = "fndCompanyBank"
        Me.fndCompanyBank.ReferenceFieldDesc = Nothing
        Me.fndCompanyBank.ReferenceFieldName = Nothing
        Me.fndCompanyBank.ReferenceTableName = Nothing
        Me.fndCompanyBank.Size = New System.Drawing.Size(239, 18)
        Me.fndCompanyBank.TabIndex = 389
        Me.fndCompanyBank.Value = ""
        '
        'MyLabel33
        '
        Me.MyLabel33.FieldName = Nothing
        Me.MyLabel33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel33.Location = New System.Drawing.Point(14, 207)
        Me.MyLabel33.Name = "MyLabel33"
        Me.MyLabel33.Size = New System.Drawing.Size(138, 16)
        Me.MyLabel33.TabIndex = 405
        Me.MyLabel33.Text = "DCS Current Bank Details"
        '
        'MyLabel29
        '
        Me.MyLabel29.FieldName = Nothing
        Me.MyLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel29.Location = New System.Drawing.Point(14, 41)
        Me.MyLabel29.Name = "MyLabel29"
        Me.MyLabel29.Size = New System.Drawing.Size(138, 16)
        Me.MyLabel29.TabIndex = 383
        Me.MyLabel29.Text = "DCS Current Bank Details"
        '
        'txtCompanyBank1
        '
        Me.txtCompanyBank1.CalculationExpression = Nothing
        Me.txtCompanyBank1.FieldCode = Nothing
        Me.txtCompanyBank1.FieldDesc = Nothing
        Me.txtCompanyBank1.FieldMaxLength = 0
        Me.txtCompanyBank1.FieldName = Nothing
        Me.txtCompanyBank1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyBank1.isCalculatedField = False
        Me.txtCompanyBank1.IsSourceFromTable = False
        Me.txtCompanyBank1.IsSourceFromValueList = False
        Me.txtCompanyBank1.IsUnique = False
        Me.txtCompanyBank1.Location = New System.Drawing.Point(403, 180)
        Me.txtCompanyBank1.MaxLength = 50
        Me.txtCompanyBank1.MendatroryField = True
        Me.txtCompanyBank1.MyLinkLable1 = Me.MyLabel32
        Me.txtCompanyBank1.MyLinkLable2 = Nothing
        Me.txtCompanyBank1.Name = "txtCompanyBank1"
        Me.txtCompanyBank1.ReadOnly = True
        Me.txtCompanyBank1.ReferenceFieldDesc = Nothing
        Me.txtCompanyBank1.ReferenceFieldName = Nothing
        Me.txtCompanyBank1.ReferenceTableName = Nothing
        Me.txtCompanyBank1.Size = New System.Drawing.Size(268, 18)
        Me.txtCompanyBank1.TabIndex = 399
        Me.txtCompanyBank1.TabStop = False
        '
        'txtCompanyBankName
        '
        Me.txtCompanyBankName.CalculationExpression = Nothing
        Me.txtCompanyBankName.FieldCode = Nothing
        Me.txtCompanyBankName.FieldDesc = Nothing
        Me.txtCompanyBankName.FieldMaxLength = 0
        Me.txtCompanyBankName.FieldName = Nothing
        Me.txtCompanyBankName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyBankName.isCalculatedField = False
        Me.txtCompanyBankName.IsSourceFromTable = False
        Me.txtCompanyBankName.IsSourceFromValueList = False
        Me.txtCompanyBankName.IsUnique = False
        Me.txtCompanyBankName.Location = New System.Drawing.Point(403, 14)
        Me.txtCompanyBankName.MaxLength = 50
        Me.txtCompanyBankName.MendatroryField = True
        Me.txtCompanyBankName.MyLinkLable1 = Me.MyLabel28
        Me.txtCompanyBankName.MyLinkLable2 = Nothing
        Me.txtCompanyBankName.Name = "txtCompanyBankName"
        Me.txtCompanyBankName.ReadOnly = True
        Me.txtCompanyBankName.ReferenceFieldDesc = Nothing
        Me.txtCompanyBankName.ReferenceFieldName = Nothing
        Me.txtCompanyBankName.ReferenceTableName = Nothing
        Me.txtCompanyBankName.Size = New System.Drawing.Size(268, 18)
        Me.txtCompanyBankName.TabIndex = 375
        Me.txtCompanyBankName.TabStop = False
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.HeaderText = "Bank Details 1"
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, -2)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(997, 160)
        Me.RadGroupBox1.TabIndex = 412
        Me.RadGroupBox1.Text = "Bank Details 1"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.HeaderText = "Bank Details 2"
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 164)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(997, 168)
        Me.RadGroupBox2.TabIndex = 413
        Me.RadGroupBox2.Text = "Bank Details 2"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(1047, 432)
        Me.RadGroupBox3.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(963, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(19, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 22)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(161, 8)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(66, 22)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "Delete"
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdate.Location = New System.Drawing.Point(90, 8)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(66, 22)
        Me.btnUpdate.TabIndex = 4
        Me.btnUpdate.Text = "Update"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(13, 226)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(47, 16)
        Me.MyLabel7.TabIndex = 65
        Me.MyLabel7.Text = "PAN No"
        '
        'txtPanNo
        '
        Me.txtPanNo.CalculationExpression = Nothing
        Me.txtPanNo.FieldCode = Nothing
        Me.txtPanNo.FieldDesc = Nothing
        Me.txtPanNo.FieldMaxLength = 0
        Me.txtPanNo.FieldName = Nothing
        Me.txtPanNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPanNo.isCalculatedField = False
        Me.txtPanNo.IsSourceFromTable = False
        Me.txtPanNo.IsSourceFromValueList = False
        Me.txtPanNo.IsUnique = False
        Me.txtPanNo.Location = New System.Drawing.Point(136, 225)
        Me.txtPanNo.MaxLength = 50
        Me.txtPanNo.MendatroryField = True
        Me.txtPanNo.MyLinkLable1 = Me.lblbankcode
        Me.txtPanNo.MyLinkLable2 = Nothing
        Me.txtPanNo.Name = "txtPanNo"
        Me.txtPanNo.ReadOnly = True
        Me.txtPanNo.ReferenceFieldDesc = Nothing
        Me.txtPanNo.ReferenceFieldName = Nothing
        Me.txtPanNo.ReferenceTableName = Nothing
        Me.txtPanNo.Size = New System.Drawing.Size(244, 18)
        Me.txtPanNo.TabIndex = 64
        Me.txtPanNo.TabStop = False
        '
        'MyLabel34
        '
        Me.MyLabel34.FieldName = Nothing
        Me.MyLabel34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel34.Location = New System.Drawing.Point(752, 226)
        Me.MyLabel34.Name = "MyLabel34"
        Me.MyLabel34.Size = New System.Drawing.Size(42, 16)
        Me.MyLabel34.TabIndex = 75
        Me.MyLabel34.Text = "Loyalty"
        '
        'txtLoyaltyRate
        '
        Me.txtLoyaltyRate.CalculationExpression = Nothing
        Me.txtLoyaltyRate.FieldCode = Nothing
        Me.txtLoyaltyRate.FieldDesc = Nothing
        Me.txtLoyaltyRate.FieldMaxLength = 0
        Me.txtLoyaltyRate.FieldName = Nothing
        Me.txtLoyaltyRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoyaltyRate.isCalculatedField = False
        Me.txtLoyaltyRate.IsSourceFromTable = False
        Me.txtLoyaltyRate.IsSourceFromValueList = False
        Me.txtLoyaltyRate.IsUnique = False
        Me.txtLoyaltyRate.Location = New System.Drawing.Point(870, 223)
        Me.txtLoyaltyRate.MaxLength = 50
        Me.txtLoyaltyRate.MendatroryField = True
        Me.txtLoyaltyRate.MyLinkLable1 = Me.lblbankcode
        Me.txtLoyaltyRate.MyLinkLable2 = Nothing
        Me.txtLoyaltyRate.Name = "txtLoyaltyRate"
        Me.txtLoyaltyRate.ReadOnly = True
        Me.txtLoyaltyRate.ReferenceFieldDesc = Nothing
        Me.txtLoyaltyRate.ReferenceFieldName = Nothing
        Me.txtLoyaltyRate.ReferenceTableName = Nothing
        Me.txtLoyaltyRate.Size = New System.Drawing.Size(121, 18)
        Me.txtLoyaltyRate.TabIndex = 74
        Me.txtLoyaltyRate.TabStop = False
        '
        'frmNewDCSScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1047, 473)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmNewDCSScreen"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.txtHeadLoad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOwnBMC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlGender, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDefaultValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCowPriceStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkApplyCowPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHeadLoadRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHeadLoadBasi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOwnBMCDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDCSRouteCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRegistrationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRegistrationNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPDCS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRegistered, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOtherDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDCSUploaderCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDCSName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVidhanSabhaName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPanchayatSamitiName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGramPanchayatName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRevenueVillageName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtZoneName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBlockName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDistrictName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSupervisorName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.RadPageViewPage3.PerformLayout()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankIFSCCode1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankIFSCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankBranchName1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankBranchName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankAccountNo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankAccountNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankName1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDCSCurrentBankDetails1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDCSCurrentBankDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCompanyBank1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCompanyBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLoyaltyRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents chkOwnBMC As RadCheckBox
    Friend WithEvents ddlGender As common.Controls.MyComboBox
    Friend WithEvents MyLabel27 As common.Controls.MyLabel
    Friend WithEvents chkDefaultValue As RadCheckBox
    Friend WithEvents chkInActive As RadCheckBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtCowPriceStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents chkApplyCowPrice As RadCheckBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents txtHeadLoadRate As common.Controls.MyTextBox
    Friend WithEvents lblbankcode As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtHeadLoadBasi As common.Controls.MyTextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtOwnBMCDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtDCSRouteCode As common.Controls.MyTextBox
    Friend WithEvents txtRegistrationDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtRegistrationNo As common.Controls.MyTextBox
    Friend WithEvents chkPDCS As RadCheckBox
    Friend WithEvents chkRegistered As RadCheckBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtOtherDetails As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtBankDetails As common.Controls.MyTextBox
    Friend WithEvents txtDCSUploaderCode As common.Controls.MyTextBox
    Friend WithEvents fndDCSCode As common.UserControls.txtNavigator
    Friend WithEvents btnNew As RadButton
    Friend WithEvents txtDCSName As common.Controls.MyTextBox
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtVidhanSabhaName As common.Controls.MyTextBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents txtPanchayatSamitiName As common.Controls.MyTextBox
    Friend WithEvents txtGramPanchayatName As common.Controls.MyTextBox
    Friend WithEvents txtRevenueVillageName As common.Controls.MyTextBox
    Friend WithEvents txtZoneName As common.Controls.MyTextBox
    Friend WithEvents txtBlockName As common.Controls.MyTextBox
    Friend WithEvents txtDistrictName As common.Controls.MyTextBox
    Friend WithEvents fndVidhanSabha As common.UserControls.txtFinder
    Friend WithEvents fndPanchayatSamiti As common.UserControls.txtFinder
    Friend WithEvents fndGramPanchayat As common.UserControls.txtFinder
    Friend WithEvents fndRevenueVillage As common.UserControls.txtFinder
    Friend WithEvents fndZone As common.UserControls.txtFinder
    Friend WithEvents fndBlock As common.UserControls.txtFinder
    Friend WithEvents fndDistrict As common.UserControls.txtFinder
    Friend WithEvents fndSupervisorCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel38 As common.Controls.MyLabel
    Friend WithEvents txtSupervisorName As common.Controls.MyTextBox
    Friend WithEvents RadPageViewPage3 As RadPageViewPage
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents MyLabel25 As common.Controls.MyLabel
    Friend WithEvents MyLabel30 As common.Controls.MyLabel
    Friend WithEvents MyLabel26 As common.Controls.MyLabel
    Friend WithEvents MyLabel31 As common.Controls.MyLabel
    Friend WithEvents txtBankIFSCCode1 As common.Controls.MyTextBox
    Friend WithEvents txtBankIFSCCode As common.Controls.MyTextBox
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents txtBankBranchName1 As common.Controls.MyTextBox
    Friend WithEvents MyLabel32 As common.Controls.MyLabel
    Friend WithEvents txtBankBranchName As common.Controls.MyTextBox
    Friend WithEvents txtBankAccountNo1 As common.Controls.MyTextBox
    Friend WithEvents txtBankAccountNo As common.Controls.MyTextBox
    Friend WithEvents txtBankName1 As common.Controls.MyTextBox
    Friend WithEvents txtBankName As common.Controls.MyTextBox
    Friend WithEvents txtDCSCurrentBankDetails1 As common.Controls.MyTextBox
    Friend WithEvents txtDCSCurrentBankDetails As common.Controls.MyTextBox
    Friend WithEvents fndCompanyBank1 As common.UserControls.txtFinder
    Friend WithEvents fndCompanyBank As common.UserControls.txtFinder
    Friend WithEvents MyLabel33 As common.Controls.MyLabel
    Friend WithEvents MyLabel29 As common.Controls.MyLabel
    Friend WithEvents txtCompanyBank1 As common.Controls.MyTextBox
    Friend WithEvents txtCompanyBankName As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents txtHeadLoad As common.Controls.MyComboBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtPanNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel34 As common.Controls.MyLabel
    Friend WithEvents txtLoyaltyRate As common.Controls.MyTextBox
End Class
