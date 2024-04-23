Imports XpertERPEngine
Imports XpertERPEngineFine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBullMasters
    'Inherits System.Windows.Forms.Form
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
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.lblBullSourcePrintStrew = New common.Controls.MyLabel()
        Me.lblDamLocation = New common.Controls.MyLabel()
        Me.lblBullRating = New common.Controls.MyLabel()
        Me.lblRemark = New common.Controls.MyLabel()
        Me.lblRFID = New common.Controls.MyLabel()
        Me.lblExistDate = New common.Controls.MyLabel()
        Me.lblStatusChangeDate = New common.Controls.MyLabel()
        Me.lblPenId = New common.Controls.MyLabel()
        Me.lblShedId = New common.Controls.MyLabel()
        Me.lblSSCentre = New common.Controls.MyLabel()
        Me.lblBullBookValue = New common.Controls.MyLabel()
        Me.lblExoticBullPercentage = New common.Controls.MyLabel()
        Me.lblBullSubStatus = New common.Controls.MyLabel()
        Me.lblBullAliasName = New common.Controls.MyLabel()
        Me.lblBullStatus = New common.Controls.MyLabel()
        Me.lblSSBullId = New common.Controls.MyLabel()
        Me.lblDOB = New common.Controls.MyLabel()
        Me.lblPrevBullId = New common.Controls.MyLabel()
        Me.lblBreed = New common.Controls.MyLabel()
        Me.lbl12DigitBullId = New common.Controls.MyLabel()
        Me.lblSubCategory = New common.Controls.MyLabel()
        Me.lblCountryCode = New common.Controls.MyLabel()
        Me.lblCategory = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblSpecies = New common.Controls.MyLabel()
        Me.lblSemen = New common.Controls.MyLabel()
        Me.lblbankcode = New common.Controls.MyLabel()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.MyLabel32 = New common.Controls.MyLabel()
        Me.MyLabel28 = New common.Controls.MyLabel()
        Me.txtDob = New common.Controls.MyDateTimePicker()
        Me.txtRegDate = New common.Controls.MyDateTimePicker()
        Me.txtStatusDateChanged = New common.Controls.MyDateTimePicker()
        Me.txtEndDate = New common.Controls.MyDateTimePicker()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbBullSourceForPaintingStraws = New Telerik.WinControls.UI.RadDropDownList()
        Me.cmbBullRating = New Telerik.WinControls.UI.RadDropDownList()
        Me.cmbCountry = New Telerik.WinControls.UI.RadDropDownList()
        Me.TextBox9 = New System.Windows.Forms.TextBox()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.fndPenId = New common.UserControls.txtFinder()
        Me.fndShedId = New common.UserControls.txtFinder()
        Me.fndSSCentre = New common.UserControls.txtFinder()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.fndSubStatus = New common.UserControls.txtFinder()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.fndBullStatus = New common.UserControls.txtFinder()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.fndBreed = New common.UserControls.txtFinder()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.fndSubCategory = New common.UserControls.txtFinder()
        Me.fndCategory = New common.UserControls.txtFinder()
        Me.fndSpecies = New common.UserControls.txtFinder()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.fndCode = New common.UserControls.txtNavigator()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.fndVidhanSabha = New common.UserControls.txtFinder()
        Me.fndPanchayatSamiti = New common.UserControls.txtFinder()
        Me.fndGramPanchayat = New common.UserControls.txtFinder()
        Me.fndRevenueVillage = New common.UserControls.txtFinder()
        Me.fndZone = New common.UserControls.txtFinder()
        Me.fndBlock = New common.UserControls.txtFinder()
        Me.fndDistrict = New common.UserControls.txtFinder()
        Me.fndSupervisorCode = New common.UserControls.txtFinder()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.fndCompanyBank1 = New common.UserControls.txtFinder()
        Me.fndCompanyBank = New common.UserControls.txtFinder()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        CType(Me.lblBullSourcePrintStrew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDamLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBullRating, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRFID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExistDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStatusChangeDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPenId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShedId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSSCentre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBullBookValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExoticBullPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBullSubStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBullAliasName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBullStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSSBullId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDOB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPrevBullId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBreed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl12DigitBullId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCountryCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSpecies, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSemen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDob, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRegDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStatusDateChanged, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.cmbBullSourceForPaintingStraws, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBullRating, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblBullSourcePrintStrew
        '
        Me.lblBullSourcePrintStrew.FieldName = Nothing
        Me.lblBullSourcePrintStrew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBullSourcePrintStrew.Location = New System.Drawing.Point(606, 258)
        Me.lblBullSourcePrintStrew.Name = "lblBullSourcePrintStrew"
        Me.lblBullSourcePrintStrew.Size = New System.Drawing.Size(157, 16)
        Me.lblBullSourcePrintStrew.TabIndex = 409
        Me.lblBullSourcePrintStrew.Text = "Bull Source for printing straws"
        '
        'lblDamLocation
        '
        Me.lblDamLocation.FieldName = Nothing
        Me.lblDamLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDamLocation.Location = New System.Drawing.Point(456, 258)
        Me.lblDamLocation.Name = "lblDamLocation"
        Me.lblDamLocation.Size = New System.Drawing.Size(112, 16)
        Me.lblDamLocation.TabIndex = 407
        Me.lblDamLocation.Text = "Dam's Loaction Yield"
        '
        'lblBullRating
        '
        Me.lblBullRating.FieldName = Nothing
        Me.lblBullRating.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBullRating.Location = New System.Drawing.Point(321, 258)
        Me.lblBullRating.Name = "lblBullRating"
        Me.lblBullRating.Size = New System.Drawing.Size(61, 16)
        Me.lblBullRating.TabIndex = 406
        Me.lblBullRating.Text = "Bull Rating"
        '
        'lblRemark
        '
        Me.lblRemark.FieldName = Nothing
        Me.lblRemark.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemark.Location = New System.Drawing.Point(151, 258)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(46, 16)
        Me.lblRemark.TabIndex = 404
        Me.lblRemark.Text = "Remark"
        '
        'lblRFID
        '
        Me.lblRFID.FieldName = Nothing
        Me.lblRFID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRFID.Location = New System.Drawing.Point(12, 258)
        Me.lblRFID.Name = "lblRFID"
        Me.lblRFID.Size = New System.Drawing.Size(55, 16)
        Me.lblRFID.TabIndex = 402
        Me.lblRFID.Text = "Bull RFID"
        '
        'lblExistDate
        '
        Me.lblExistDate.FieldName = Nothing
        Me.lblExistDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExistDate.Location = New System.Drawing.Point(854, 191)
        Me.lblExistDate.Name = "lblExistDate"
        Me.lblExistDate.Size = New System.Drawing.Size(58, 16)
        Me.lblExistDate.TabIndex = 401
        Me.lblExistDate.Text = "Exist Date"
        '
        'lblStatusChangeDate
        '
        Me.lblStatusChangeDate.FieldName = Nothing
        Me.lblStatusChangeDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusChangeDate.Location = New System.Drawing.Point(731, 191)
        Me.lblStatusChangeDate.Name = "lblStatusChangeDate"
        Me.lblStatusChangeDate.Size = New System.Drawing.Size(108, 16)
        Me.lblStatusChangeDate.TabIndex = 400
        Me.lblStatusChangeDate.Text = "Status Change Date"
        '
        'lblPenId
        '
        Me.lblPenId.FieldName = Nothing
        Me.lblPenId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPenId.Location = New System.Drawing.Point(609, 189)
        Me.lblPenId.Name = "lblPenId"
        Me.lblPenId.Size = New System.Drawing.Size(39, 16)
        Me.lblPenId.TabIndex = 398
        Me.lblPenId.Text = "Pen Id"
        '
        'lblShedId
        '
        Me.lblShedId.FieldName = Nothing
        Me.lblShedId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShedId.Location = New System.Drawing.Point(458, 190)
        Me.lblShedId.Name = "lblShedId"
        Me.lblShedId.Size = New System.Drawing.Size(45, 16)
        Me.lblShedId.TabIndex = 396
        Me.lblShedId.Text = "Shed Id"
        '
        'lblSSCentre
        '
        Me.lblSSCentre.FieldName = Nothing
        Me.lblSSCentre.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSSCentre.Location = New System.Drawing.Point(322, 192)
        Me.lblSSCentre.Name = "lblSSCentre"
        Me.lblSSCentre.Size = New System.Drawing.Size(58, 16)
        Me.lblSSCentre.TabIndex = 394
        Me.lblSSCentre.Text = "SS Centre"
        '
        'lblBullBookValue
        '
        Me.lblBullBookValue.FieldName = Nothing
        Me.lblBullBookValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBullBookValue.Location = New System.Drawing.Point(154, 191)
        Me.lblBullBookValue.Name = "lblBullBookValue"
        Me.lblBullBookValue.Size = New System.Drawing.Size(86, 16)
        Me.lblBullBookValue.TabIndex = 392
        Me.lblBullBookValue.Text = "Bull Book Value"
        '
        'lblExoticBullPercentage
        '
        Me.lblExoticBullPercentage.FieldName = Nothing
        Me.lblExoticBullPercentage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExoticBullPercentage.Location = New System.Drawing.Point(9, 191)
        Me.lblExoticBullPercentage.Name = "lblExoticBullPercentage"
        Me.lblExoticBullPercentage.Size = New System.Drawing.Size(123, 16)
        Me.lblExoticBullPercentage.TabIndex = 390
        Me.lblExoticBullPercentage.Text = " Exotic Bull Percentage"
        '
        'lblBullSubStatus
        '
        Me.lblBullSubStatus.FieldName = Nothing
        Me.lblBullSubStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBullSubStatus.Location = New System.Drawing.Point(854, 124)
        Me.lblBullSubStatus.Name = "lblBullSubStatus"
        Me.lblBullSubStatus.Size = New System.Drawing.Size(84, 16)
        Me.lblBullSubStatus.TabIndex = 388
        Me.lblBullSubStatus.Text = "Bull Sub Status"
        '
        'lblBullAliasName
        '
        Me.lblBullAliasName.FieldName = Nothing
        Me.lblBullAliasName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBullAliasName.Location = New System.Drawing.Point(847, 65)
        Me.lblBullAliasName.Name = "lblBullAliasName"
        Me.lblBullAliasName.Size = New System.Drawing.Size(86, 16)
        Me.lblBullAliasName.TabIndex = 386
        Me.lblBullAliasName.Text = "Bull Alias Name"
        '
        'lblBullStatus
        '
        Me.lblBullStatus.FieldName = Nothing
        Me.lblBullStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBullStatus.Location = New System.Drawing.Point(731, 126)
        Me.lblBullStatus.Name = "lblBullStatus"
        Me.lblBullStatus.Size = New System.Drawing.Size(60, 16)
        Me.lblBullStatus.TabIndex = 384
        Me.lblBullStatus.Text = "Bull Status"
        '
        'lblSSBullId
        '
        Me.lblSSBullId.FieldName = Nothing
        Me.lblSSBullId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSSBullId.Location = New System.Drawing.Point(732, 65)
        Me.lblSSBullId.Name = "lblSSBullId"
        Me.lblSSBullId.Size = New System.Drawing.Size(56, 16)
        Me.lblSSBullId.TabIndex = 383
        Me.lblSSBullId.Text = "SS Bull Id"
        '
        'lblDOB
        '
        Me.lblDOB.FieldName = Nothing
        Me.lblDOB.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDOB.Location = New System.Drawing.Point(609, 125)
        Me.lblDOB.Name = "lblDOB"
        Me.lblDOB.Size = New System.Drawing.Size(72, 16)
        Me.lblDOB.TabIndex = 380
        Me.lblDOB.Text = "Date Of Birth"
        '
        'lblPrevBullId
        '
        Me.lblPrevBullId.FieldName = Nothing
        Me.lblPrevBullId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrevBullId.Location = New System.Drawing.Point(597, 66)
        Me.lblPrevBullId.Name = "lblPrevBullId"
        Me.lblPrevBullId.Size = New System.Drawing.Size(64, 16)
        Me.lblPrevBullId.TabIndex = 378
        Me.lblPrevBullId.Text = "Prev Bull Id"
        '
        'lblBreed
        '
        Me.lblBreed.FieldName = Nothing
        Me.lblBreed.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBreed.Location = New System.Drawing.Point(458, 128)
        Me.lblBreed.Name = "lblBreed"
        Me.lblBreed.Size = New System.Drawing.Size(36, 16)
        Me.lblBreed.TabIndex = 376
        Me.lblBreed.Text = "Breed"
        '
        'lbl12DigitBullId
        '
        Me.lbl12DigitBullId.FieldName = Nothing
        Me.lbl12DigitBullId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl12DigitBullId.Location = New System.Drawing.Point(453, 69)
        Me.lbl12DigitBullId.Name = "lbl12DigitBullId"
        Me.lbl12DigitBullId.Size = New System.Drawing.Size(79, 16)
        Me.lbl12DigitBullId.TabIndex = 374
        Me.lbl12DigitBullId.Text = "12 Digit Bull Id"
        '
        'lblSubCategory
        '
        Me.lblSubCategory.FieldName = Nothing
        Me.lblSubCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubCategory.Location = New System.Drawing.Point(319, 129)
        Me.lblSubCategory.Name = "lblSubCategory"
        Me.lblSubCategory.Size = New System.Drawing.Size(72, 16)
        Me.lblSubCategory.TabIndex = 373
        Me.lblSubCategory.Text = "SubCategory"
        '
        'lblCountryCode
        '
        Me.lblCountryCode.FieldName = Nothing
        Me.lblCountryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCountryCode.Location = New System.Drawing.Point(321, 67)
        Me.lblCountryCode.Name = "lblCountryCode"
        Me.lblCountryCode.Size = New System.Drawing.Size(76, 16)
        Me.lblCountryCode.TabIndex = 370
        Me.lblCountryCode.Text = "Country Code"
        '
        'lblCategory
        '
        Me.lblCategory.FieldName = Nothing
        Me.lblCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCategory.Location = New System.Drawing.Point(154, 134)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(52, 16)
        Me.lblCategory.TabIndex = 368
        Me.lblCategory.Text = "Category"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(154, 71)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel3.TabIndex = 367
        Me.MyLabel3.Text = "Registration Date"
        '
        'lblSpecies
        '
        Me.lblSpecies.FieldName = Nothing
        Me.lblSpecies.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSpecies.Location = New System.Drawing.Point(10, 134)
        Me.lblSpecies.Name = "lblSpecies"
        Me.lblSpecies.Size = New System.Drawing.Size(46, 16)
        Me.lblSpecies.TabIndex = 67
        Me.lblSpecies.Text = "Species"
        '
        'lblSemen
        '
        Me.lblSemen.FieldName = Nothing
        Me.lblSemen.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSemen.Location = New System.Drawing.Point(10, 71)
        Me.lblSemen.Name = "lblSemen"
        Me.lblSemen.Size = New System.Drawing.Size(124, 16)
        Me.lblSemen.TabIndex = 64
        Me.lblSemen.Text = "Is Semen/Bull Imported"
        '
        'lblbankcode
        '
        Me.lblbankcode.FieldName = Nothing
        Me.lblbankcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbankcode.Location = New System.Drawing.Point(8, 38)
        Me.lblbankcode.Name = "lblbankcode"
        Me.lblbankcode.Size = New System.Drawing.Size(33, 16)
        Me.lblbankcode.TabIndex = 63
        Me.lblbankcode.Text = "Code"
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
        'txtDob
        '
        Me.txtDob.CalculationExpression = Nothing
        Me.txtDob.CustomFormat = "dd/MM/yyyy"
        Me.txtDob.FieldCode = Nothing
        Me.txtDob.FieldDesc = Nothing
        Me.txtDob.FieldMaxLength = 0
        Me.txtDob.FieldName = Nothing
        Me.txtDob.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDob.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDob.isCalculatedField = False
        Me.txtDob.IsSourceFromTable = False
        Me.txtDob.IsSourceFromValueList = False
        Me.txtDob.IsUnique = False
        Me.txtDob.Location = New System.Drawing.Point(468, 299)
        Me.txtDob.MendatroryField = False
        Me.txtDob.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDob.MyLinkLable1 = Nothing
        Me.txtDob.MyLinkLable2 = Nothing
        Me.txtDob.Name = "txtDob"
        Me.txtDob.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDob.ReferenceFieldDesc = Nothing
        Me.txtDob.ReferenceFieldName = Nothing
        Me.txtDob.ReferenceTableName = Nothing
        Me.txtDob.Size = New System.Drawing.Size(133, 18)
        Me.txtDob.TabIndex = 451
        Me.txtDob.TabStop = False
        Me.txtDob.Text = "13/06/2011"
        Me.txtDob.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtRegDate
        '
        Me.txtRegDate.CalculationExpression = Nothing
        Me.txtRegDate.CustomFormat = "dd/MM/yyyy"
        Me.txtRegDate.FieldCode = Nothing
        Me.txtRegDate.FieldDesc = Nothing
        Me.txtRegDate.FieldMaxLength = 0
        Me.txtRegDate.FieldName = Nothing
        Me.txtRegDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtRegDate.isCalculatedField = False
        Me.txtRegDate.IsSourceFromTable = False
        Me.txtRegDate.IsSourceFromValueList = False
        Me.txtRegDate.IsUnique = False
        Me.txtRegDate.Location = New System.Drawing.Point(141, 179)
        Me.txtRegDate.MendatroryField = False
        Me.txtRegDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRegDate.MyLinkLable1 = Nothing
        Me.txtRegDate.MyLinkLable2 = Nothing
        Me.txtRegDate.Name = "txtRegDate"
        Me.txtRegDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRegDate.ReferenceFieldDesc = Nothing
        Me.txtRegDate.ReferenceFieldName = Nothing
        Me.txtRegDate.ReferenceTableName = Nothing
        Me.txtRegDate.Size = New System.Drawing.Size(87, 18)
        Me.txtRegDate.TabIndex = 452
        Me.txtRegDate.TabStop = False
        Me.txtRegDate.Text = "13/06/2011"
        Me.txtRegDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtStatusDateChanged
        '
        Me.txtStatusDateChanged.CalculationExpression = Nothing
        Me.txtStatusDateChanged.CustomFormat = "dd/MM/yyyy"
        Me.txtStatusDateChanged.FieldCode = Nothing
        Me.txtStatusDateChanged.FieldDesc = Nothing
        Me.txtStatusDateChanged.FieldMaxLength = 0
        Me.txtStatusDateChanged.FieldName = Nothing
        Me.txtStatusDateChanged.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatusDateChanged.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtStatusDateChanged.isCalculatedField = False
        Me.txtStatusDateChanged.IsSourceFromTable = False
        Me.txtStatusDateChanged.IsSourceFromValueList = False
        Me.txtStatusDateChanged.IsUnique = False
        Me.txtStatusDateChanged.Location = New System.Drawing.Point(763, 129)
        Me.txtStatusDateChanged.MendatroryField = False
        Me.txtStatusDateChanged.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtStatusDateChanged.MyLinkLable1 = Nothing
        Me.txtStatusDateChanged.MyLinkLable2 = Nothing
        Me.txtStatusDateChanged.Name = "txtStatusDateChanged"
        Me.txtStatusDateChanged.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtStatusDateChanged.ReferenceFieldDesc = Nothing
        Me.txtStatusDateChanged.ReferenceFieldName = Nothing
        Me.txtStatusDateChanged.ReferenceTableName = Nothing
        Me.txtStatusDateChanged.Size = New System.Drawing.Size(143, 18)
        Me.txtStatusDateChanged.TabIndex = 453
        Me.txtStatusDateChanged.TabStop = False
        Me.txtStatusDateChanged.Text = "13/06/2011"
        Me.txtStatusDateChanged.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtEndDate
        '
        Me.txtEndDate.CalculationExpression = Nothing
        Me.txtEndDate.CustomFormat = "dd/MM/yyyy"
        Me.txtEndDate.FieldCode = Nothing
        Me.txtEndDate.FieldDesc = Nothing
        Me.txtEndDate.FieldMaxLength = 0
        Me.txtEndDate.FieldName = Nothing
        Me.txtEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtEndDate.isCalculatedField = False
        Me.txtEndDate.IsSourceFromTable = False
        Me.txtEndDate.IsSourceFromValueList = False
        Me.txtEndDate.IsUnique = False
        Me.txtEndDate.Location = New System.Drawing.Point(764, 205)
        Me.txtEndDate.MendatroryField = False
        Me.txtEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtEndDate.MyLinkLable1 = Nothing
        Me.txtEndDate.MyLinkLable2 = Nothing
        Me.txtEndDate.Name = "txtEndDate"
        Me.txtEndDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtEndDate.ReferenceFieldDesc = Nothing
        Me.txtEndDate.ReferenceFieldName = Nothing
        Me.txtEndDate.ReferenceTableName = Nothing
        Me.txtEndDate.ShowCheckBox = True
        Me.txtEndDate.Size = New System.Drawing.Size(142, 18)
        Me.txtEndDate.TabIndex = 454
        Me.txtEndDate.TabStop = False
        Me.txtEndDate.Text = "13/06/2011"
        Me.txtEndDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
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
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(992, 473)
        Me.SplitContainer1.SplitterDistance = 421
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.DefaultPage = Me.RadPageViewPage1
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(992, 421)
        Me.RadPageView1.TabIndex = 1
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ShowItemCloseButton = False
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.Label27)
        Me.RadPageViewPage1.Controls.Add(Me.Label26)
        Me.RadPageViewPage1.Controls.Add(Me.Label25)
        Me.RadPageViewPage1.Controls.Add(Me.Label24)
        Me.RadPageViewPage1.Controls.Add(Me.Label23)
        Me.RadPageViewPage1.Controls.Add(Me.Label22)
        Me.RadPageViewPage1.Controls.Add(Me.Label21)
        Me.RadPageViewPage1.Controls.Add(Me.Label20)
        Me.RadPageViewPage1.Controls.Add(Me.Label19)
        Me.RadPageViewPage1.Controls.Add(Me.Label18)
        Me.RadPageViewPage1.Controls.Add(Me.Label17)
        Me.RadPageViewPage1.Controls.Add(Me.Label16)
        Me.RadPageViewPage1.Controls.Add(Me.Label15)
        Me.RadPageViewPage1.Controls.Add(Me.Label14)
        Me.RadPageViewPage1.Controls.Add(Me.Label13)
        Me.RadPageViewPage1.Controls.Add(Me.Label12)
        Me.RadPageViewPage1.Controls.Add(Me.Label11)
        Me.RadPageViewPage1.Controls.Add(Me.Label10)
        Me.RadPageViewPage1.Controls.Add(Me.Label9)
        Me.RadPageViewPage1.Controls.Add(Me.Label8)
        Me.RadPageViewPage1.Controls.Add(Me.Label7)
        Me.RadPageViewPage1.Controls.Add(Me.Label6)
        Me.RadPageViewPage1.Controls.Add(Me.Label5)
        Me.RadPageViewPage1.Controls.Add(Me.Label4)
        Me.RadPageViewPage1.Controls.Add(Me.Label3)
        Me.RadPageViewPage1.Controls.Add(Me.Label2)
        Me.RadPageViewPage1.Controls.Add(Me.Label1)
        Me.RadPageViewPage1.Controls.Add(Me.txtEndDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtStatusDateChanged)
        Me.RadPageViewPage1.Controls.Add(Me.txtRegDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtDob)
        Me.RadPageViewPage1.Controls.Add(Me.cmbBullSourceForPaintingStraws)
        Me.RadPageViewPage1.Controls.Add(Me.cmbBullRating)
        Me.RadPageViewPage1.Controls.Add(Me.cmbCountry)
        Me.RadPageViewPage1.Controls.Add(Me.TextBox9)
        Me.RadPageViewPage1.Controls.Add(Me.TextBox8)
        Me.RadPageViewPage1.Controls.Add(Me.TextBox7)
        Me.RadPageViewPage1.Controls.Add(Me.fndPenId)
        Me.RadPageViewPage1.Controls.Add(Me.fndShedId)
        Me.RadPageViewPage1.Controls.Add(Me.fndSSCentre)
        Me.RadPageViewPage1.Controls.Add(Me.TextBox6)
        Me.RadPageViewPage1.Controls.Add(Me.TextBox5)
        Me.RadPageViewPage1.Controls.Add(Me.fndSubStatus)
        Me.RadPageViewPage1.Controls.Add(Me.TextBox4)
        Me.RadPageViewPage1.Controls.Add(Me.fndBullStatus)
        Me.RadPageViewPage1.Controls.Add(Me.TextBox3)
        Me.RadPageViewPage1.Controls.Add(Me.TextBox2)
        Me.RadPageViewPage1.Controls.Add(Me.fndBreed)
        Me.RadPageViewPage1.Controls.Add(Me.TextBox1)
        Me.RadPageViewPage1.Controls.Add(Me.fndSubCategory)
        Me.RadPageViewPage1.Controls.Add(Me.fndCategory)
        Me.RadPageViewPage1.Controls.Add(Me.fndSpecies)
        Me.RadPageViewPage1.Controls.Add(Me.RadioButton2)
        Me.RadPageViewPage1.Controls.Add(Me.RadioButton1)
        Me.RadPageViewPage1.Controls.Add(Me.fndCode)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(121.0!, 24.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 33)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(971, 377)
        Me.RadPageViewPage1.Text = "Bull Basic Information"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(8, 35)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(34, 13)
        Me.Label27.TabIndex = 481
        Me.Label27.Text = "Code"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(645, 208)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(52, 13)
        Me.Label26.TabIndex = 480
        Me.Label26.Text = "Exit Date"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(643, 184)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(84, 13)
        Me.Label25.TabIndex = 479
        Me.Label25.Text = "Bull Sub Status"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(641, 160)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(85, 13)
        Me.Label24.TabIndex = 478
        Me.Label24.Text = "Bull Alias Name"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(641, 134)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(116, 13)
        Me.Label23.TabIndex = 477
        Me.Label23.Text = "Status Chnaged Date"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(641, 107)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(61, 13)
        Me.Label22.TabIndex = 476
        Me.Label22.Text = "Bull Status"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(640, 78)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(54, 13)
        Me.Label21.TabIndex = 475
        Me.Label21.Text = "SS Bull Id"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(304, 254)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(162, 13)
        Me.Label20.TabIndex = 474
        Me.Label20.Text = "Bull Source For Painting Straw"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(305, 178)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(39, 13)
        Me.Label19.TabIndex = 473
        Me.Label19.Text = "Pen Id"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(306, 302)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(74, 13)
        Me.Label18.TabIndex = 472
        Me.Label18.Text = "Date Of Birth"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(305, 279)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(63, 13)
        Me.Label17.TabIndex = 471
        Me.Label17.Text = "Prev Bull Id"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(305, 232)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(111, 13)
        Me.Label16.TabIndex = 470
        Me.Label16.Text = "Dam's Loaction Yeild"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(305, 156)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(46, 13)
        Me.Label15.TabIndex = 469
        Me.Label15.Text = "Shed Id"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(305, 107)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(36, 13)
        Me.Label14.TabIndex = 468
        Me.Label14.Text = "Breed"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(304, 84)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(82, 13)
        Me.Label13.TabIndex = 467
        Me.Label13.Text = "12 Digit Bull Id"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(306, 203)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(74, 13)
        Me.Label12.TabIndex = 466
        Me.Label12.Text = "Bull Rating(*)"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(305, 132)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(56, 13)
        Me.Label11.TabIndex = 465
        Me.Label11.Text = "SS Centre"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(13, 305)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 13)
        Me.Label10.TabIndex = 464
        Me.Label10.Text = "SubCategory"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(13, 256)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(45, 13)
        Me.Label9.TabIndex = 463
        Me.Label9.Text = "Remark"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(11, 156)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 13)
        Me.Label8.TabIndex = 462
        Me.Label8.Text = "Bull RFID"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(11, 231)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(86, 13)
        Me.Label7.TabIndex = 461
        Me.Label7.Text = "Bull Book Value"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 128)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(130, 13)
        Me.Label6.TabIndex = 460
        Me.Label6.Text = "Exotic Blood Percentage"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 205)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 13)
        Me.Label5.TabIndex = 459
        Me.Label5.Text = "Category"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 105)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 13)
        Me.Label4.TabIndex = 458
        Me.Label4.Text = "Species"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 178)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 13)
        Me.Label3.TabIndex = 457
        Me.Label3.Text = "Registration Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(125, 13)
        Me.Label2.TabIndex = 456
        Me.Label2.Text = "Is Semen/Bull Imported"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 281)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 455
        Me.Label1.Text = "Country Code"
        '
        'cmbBullSourceForPaintingStraws
        '
        Me.cmbBullSourceForPaintingStraws.AutoCompleteDisplayMember = Nothing
        Me.cmbBullSourceForPaintingStraws.AutoCompleteValueMember = Nothing
        Me.cmbBullSourceForPaintingStraws.DropDownAnimationEnabled = True
        RadListDataItem5.Text = "India"
        RadListDataItem6.Text = "USA"
        Me.cmbBullSourceForPaintingStraws.Items.Add(RadListDataItem5)
        Me.cmbBullSourceForPaintingStraws.Items.Add(RadListDataItem6)
        Me.cmbBullSourceForPaintingStraws.Location = New System.Drawing.Point(467, 249)
        Me.cmbBullSourceForPaintingStraws.Name = "cmbBullSourceForPaintingStraws"
        Me.cmbBullSourceForPaintingStraws.Size = New System.Drawing.Size(135, 20)
        Me.cmbBullSourceForPaintingStraws.TabIndex = 450
        '
        'cmbBullRating
        '
        Me.cmbBullRating.AutoCompleteDisplayMember = Nothing
        Me.cmbBullRating.AutoCompleteValueMember = Nothing
        Me.cmbBullRating.DropDownAnimationEnabled = True
        RadListDataItem7.Text = "India"
        RadListDataItem8.Text = "USA"
        Me.cmbBullRating.Items.Add(RadListDataItem7)
        Me.cmbBullRating.Items.Add(RadListDataItem8)
        Me.cmbBullRating.Location = New System.Drawing.Point(465, 199)
        Me.cmbBullRating.Name = "cmbBullRating"
        Me.cmbBullRating.Size = New System.Drawing.Size(135, 20)
        Me.cmbBullRating.TabIndex = 449
        '
        'cmbCountry
        '
        Me.cmbCountry.AutoCompleteDisplayMember = Nothing
        Me.cmbCountry.AutoCompleteValueMember = Nothing
        Me.cmbCountry.DropDownAnimationEnabled = True
        RadListDataItem1.Text = "India"
        RadListDataItem2.Text = "USA"
        Me.cmbCountry.Items.Add(RadListDataItem1)
        Me.cmbCountry.Items.Add(RadListDataItem2)
        Me.cmbCountry.Location = New System.Drawing.Point(138, 279)
        Me.cmbCountry.Name = "cmbCountry"
        Me.cmbCountry.Size = New System.Drawing.Size(125, 20)
        Me.cmbCountry.TabIndex = 448
        '
        'TextBox9
        '
        Me.TextBox9.Location = New System.Drawing.Point(465, 223)
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.Size = New System.Drawing.Size(135, 20)
        Me.TextBox9.TabIndex = 408
        '
        'TextBox8
        '
        Me.TextBox8.Location = New System.Drawing.Point(138, 253)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New System.Drawing.Size(126, 20)
        Me.TextBox8.TabIndex = 405
        '
        'TextBox7
        '
        Me.TextBox7.Location = New System.Drawing.Point(141, 153)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(122, 20)
        Me.TextBox7.TabIndex = 403
        '
        'fndPenId
        '
        Me.fndPenId.CalculationExpression = Nothing
        Me.fndPenId.FieldCode = Nothing
        Me.fndPenId.FieldDesc = Nothing
        Me.fndPenId.FieldMaxLength = 0
        Me.fndPenId.FieldName = Nothing
        Me.fndPenId.isCalculatedField = False
        Me.fndPenId.IsSourceFromTable = False
        Me.fndPenId.IsSourceFromValueList = False
        Me.fndPenId.IsUnique = False
        Me.fndPenId.Location = New System.Drawing.Point(465, 175)
        Me.fndPenId.MendatroryField = True
        Me.fndPenId.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPenId.MyLinkLable1 = Nothing
        Me.fndPenId.MyLinkLable2 = Nothing
        Me.fndPenId.MyReadOnly = False
        Me.fndPenId.MyShowMasterFormButton = False
        Me.fndPenId.Name = "fndPenId"
        Me.fndPenId.ReferenceFieldDesc = Nothing
        Me.fndPenId.ReferenceFieldName = Nothing
        Me.fndPenId.ReferenceTableName = Nothing
        Me.fndPenId.Size = New System.Drawing.Size(135, 18)
        Me.fndPenId.TabIndex = 399
        Me.fndPenId.Value = ""
        '
        'fndShedId
        '
        Me.fndShedId.CalculationExpression = Nothing
        Me.fndShedId.FieldCode = Nothing
        Me.fndShedId.FieldDesc = Nothing
        Me.fndShedId.FieldMaxLength = 0
        Me.fndShedId.FieldName = Nothing
        Me.fndShedId.isCalculatedField = False
        Me.fndShedId.IsSourceFromTable = False
        Me.fndShedId.IsSourceFromValueList = False
        Me.fndShedId.IsUnique = False
        Me.fndShedId.Location = New System.Drawing.Point(465, 151)
        Me.fndShedId.MendatroryField = True
        Me.fndShedId.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndShedId.MyLinkLable1 = Nothing
        Me.fndShedId.MyLinkLable2 = Nothing
        Me.fndShedId.MyReadOnly = False
        Me.fndShedId.MyShowMasterFormButton = False
        Me.fndShedId.Name = "fndShedId"
        Me.fndShedId.ReferenceFieldDesc = Nothing
        Me.fndShedId.ReferenceFieldName = Nothing
        Me.fndShedId.ReferenceTableName = Nothing
        Me.fndShedId.Size = New System.Drawing.Size(135, 18)
        Me.fndShedId.TabIndex = 397
        Me.fndShedId.Value = ""
        '
        'fndSSCentre
        '
        Me.fndSSCentre.CalculationExpression = Nothing
        Me.fndSSCentre.FieldCode = Nothing
        Me.fndSSCentre.FieldDesc = Nothing
        Me.fndSSCentre.FieldMaxLength = 0
        Me.fndSSCentre.FieldName = Nothing
        Me.fndSSCentre.isCalculatedField = False
        Me.fndSSCentre.IsSourceFromTable = False
        Me.fndSSCentre.IsSourceFromValueList = False
        Me.fndSSCentre.IsUnique = False
        Me.fndSSCentre.Location = New System.Drawing.Point(465, 129)
        Me.fndSSCentre.MendatroryField = True
        Me.fndSSCentre.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSSCentre.MyLinkLable1 = Nothing
        Me.fndSSCentre.MyLinkLable2 = Nothing
        Me.fndSSCentre.MyReadOnly = False
        Me.fndSSCentre.MyShowMasterFormButton = False
        Me.fndSSCentre.Name = "fndSSCentre"
        Me.fndSSCentre.ReferenceFieldDesc = Nothing
        Me.fndSSCentre.ReferenceFieldName = Nothing
        Me.fndSSCentre.ReferenceTableName = Nothing
        Me.fndSSCentre.Size = New System.Drawing.Size(135, 18)
        Me.fndSSCentre.TabIndex = 395
        Me.fndSSCentre.Value = ""
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(138, 227)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(126, 20)
        Me.TextBox6.TabIndex = 393
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(141, 129)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(122, 20)
        Me.TextBox5.TabIndex = 391
        '
        'fndSubStatus
        '
        Me.fndSubStatus.CalculationExpression = Nothing
        Me.fndSubStatus.FieldCode = Nothing
        Me.fndSubStatus.FieldDesc = Nothing
        Me.fndSubStatus.FieldMaxLength = 0
        Me.fndSubStatus.FieldName = Nothing
        Me.fndSubStatus.isCalculatedField = False
        Me.fndSubStatus.IsSourceFromTable = False
        Me.fndSubStatus.IsSourceFromValueList = False
        Me.fndSubStatus.IsUnique = False
        Me.fndSubStatus.Location = New System.Drawing.Point(763, 181)
        Me.fndSubStatus.MendatroryField = True
        Me.fndSubStatus.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSubStatus.MyLinkLable1 = Nothing
        Me.fndSubStatus.MyLinkLable2 = Nothing
        Me.fndSubStatus.MyReadOnly = False
        Me.fndSubStatus.MyShowMasterFormButton = False
        Me.fndSubStatus.Name = "fndSubStatus"
        Me.fndSubStatus.ReferenceFieldDesc = Nothing
        Me.fndSubStatus.ReferenceFieldName = Nothing
        Me.fndSubStatus.ReferenceTableName = Nothing
        Me.fndSubStatus.Size = New System.Drawing.Size(143, 18)
        Me.fndSubStatus.TabIndex = 389
        Me.fndSubStatus.Value = ""
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(763, 155)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(143, 20)
        Me.TextBox4.TabIndex = 387
        '
        'fndBullStatus
        '
        Me.fndBullStatus.CalculationExpression = Nothing
        Me.fndBullStatus.FieldCode = Nothing
        Me.fndBullStatus.FieldDesc = Nothing
        Me.fndBullStatus.FieldMaxLength = 0
        Me.fndBullStatus.FieldName = Nothing
        Me.fndBullStatus.isCalculatedField = False
        Me.fndBullStatus.IsSourceFromTable = False
        Me.fndBullStatus.IsSourceFromValueList = False
        Me.fndBullStatus.IsUnique = False
        Me.fndBullStatus.Location = New System.Drawing.Point(763, 104)
        Me.fndBullStatus.MendatroryField = True
        Me.fndBullStatus.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBullStatus.MyLinkLable1 = Nothing
        Me.fndBullStatus.MyLinkLable2 = Nothing
        Me.fndBullStatus.MyReadOnly = False
        Me.fndBullStatus.MyShowMasterFormButton = False
        Me.fndBullStatus.Name = "fndBullStatus"
        Me.fndBullStatus.ReferenceFieldDesc = Nothing
        Me.fndBullStatus.ReferenceFieldName = Nothing
        Me.fndBullStatus.ReferenceTableName = Nothing
        Me.fndBullStatus.Size = New System.Drawing.Size(143, 18)
        Me.fndBullStatus.TabIndex = 385
        Me.fndBullStatus.Value = ""
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(763, 76)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(143, 20)
        Me.TextBox3.TabIndex = 382
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(468, 273)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(133, 20)
        Me.TextBox2.TabIndex = 379
        '
        'fndBreed
        '
        Me.fndBreed.CalculationExpression = Nothing
        Me.fndBreed.FieldCode = Nothing
        Me.fndBreed.FieldDesc = Nothing
        Me.fndBreed.FieldMaxLength = 0
        Me.fndBreed.FieldName = Nothing
        Me.fndBreed.isCalculatedField = False
        Me.fndBreed.IsSourceFromTable = False
        Me.fndBreed.IsSourceFromValueList = False
        Me.fndBreed.IsUnique = False
        Me.fndBreed.Location = New System.Drawing.Point(465, 106)
        Me.fndBreed.MendatroryField = True
        Me.fndBreed.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBreed.MyLinkLable1 = Nothing
        Me.fndBreed.MyLinkLable2 = Nothing
        Me.fndBreed.MyReadOnly = False
        Me.fndBreed.MyShowMasterFormButton = False
        Me.fndBreed.Name = "fndBreed"
        Me.fndBreed.ReferenceFieldDesc = Nothing
        Me.fndBreed.ReferenceFieldName = Nothing
        Me.fndBreed.ReferenceTableName = Nothing
        Me.fndBreed.Size = New System.Drawing.Size(135, 18)
        Me.fndBreed.TabIndex = 377
        Me.fndBreed.Value = ""
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(465, 81)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(135, 20)
        Me.TextBox1.TabIndex = 375
        '
        'fndSubCategory
        '
        Me.fndSubCategory.CalculationExpression = Nothing
        Me.fndSubCategory.FieldCode = Nothing
        Me.fndSubCategory.FieldDesc = Nothing
        Me.fndSubCategory.FieldMaxLength = 0
        Me.fndSubCategory.FieldName = Nothing
        Me.fndSubCategory.isCalculatedField = False
        Me.fndSubCategory.IsSourceFromTable = False
        Me.fndSubCategory.IsSourceFromValueList = False
        Me.fndSubCategory.IsUnique = False
        Me.fndSubCategory.Location = New System.Drawing.Point(138, 305)
        Me.fndSubCategory.MendatroryField = True
        Me.fndSubCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSubCategory.MyLinkLable1 = Nothing
        Me.fndSubCategory.MyLinkLable2 = Nothing
        Me.fndSubCategory.MyReadOnly = False
        Me.fndSubCategory.MyShowMasterFormButton = False
        Me.fndSubCategory.Name = "fndSubCategory"
        Me.fndSubCategory.ReferenceFieldDesc = Nothing
        Me.fndSubCategory.ReferenceFieldName = Nothing
        Me.fndSubCategory.ReferenceTableName = Nothing
        Me.fndSubCategory.Size = New System.Drawing.Size(126, 18)
        Me.fndSubCategory.TabIndex = 372
        Me.fndSubCategory.Value = ""
        '
        'fndCategory
        '
        Me.fndCategory.CalculationExpression = Nothing
        Me.fndCategory.FieldCode = Nothing
        Me.fndCategory.FieldDesc = Nothing
        Me.fndCategory.FieldMaxLength = 0
        Me.fndCategory.FieldName = Nothing
        Me.fndCategory.isCalculatedField = False
        Me.fndCategory.IsSourceFromTable = False
        Me.fndCategory.IsSourceFromValueList = False
        Me.fndCategory.IsUnique = False
        Me.fndCategory.Location = New System.Drawing.Point(141, 203)
        Me.fndCategory.MendatroryField = True
        Me.fndCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCategory.MyLinkLable1 = Nothing
        Me.fndCategory.MyLinkLable2 = Nothing
        Me.fndCategory.MyReadOnly = False
        Me.fndCategory.MyShowMasterFormButton = False
        Me.fndCategory.Name = "fndCategory"
        Me.fndCategory.ReferenceFieldDesc = Nothing
        Me.fndCategory.ReferenceFieldName = Nothing
        Me.fndCategory.ReferenceTableName = Nothing
        Me.fndCategory.Size = New System.Drawing.Size(122, 18)
        Me.fndCategory.TabIndex = 369
        Me.fndCategory.Value = ""
        '
        'fndSpecies
        '
        Me.fndSpecies.CalculationExpression = Nothing
        Me.fndSpecies.FieldCode = Nothing
        Me.fndSpecies.FieldDesc = Nothing
        Me.fndSpecies.FieldMaxLength = 0
        Me.fndSpecies.FieldName = Nothing
        Me.fndSpecies.isCalculatedField = False
        Me.fndSpecies.IsSourceFromTable = False
        Me.fndSpecies.IsSourceFromValueList = False
        Me.fndSpecies.IsUnique = False
        Me.fndSpecies.Location = New System.Drawing.Point(141, 106)
        Me.fndSpecies.MendatroryField = True
        Me.fndSpecies.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSpecies.MyLinkLable1 = Nothing
        Me.fndSpecies.MyLinkLable2 = Nothing
        Me.fndSpecies.MyReadOnly = False
        Me.fndSpecies.MyShowMasterFormButton = False
        Me.fndSpecies.Name = "fndSpecies"
        Me.fndSpecies.ReferenceFieldDesc = Nothing
        Me.fndSpecies.ReferenceFieldName = Nothing
        Me.fndSpecies.ReferenceTableName = Nothing
        Me.fndSpecies.Size = New System.Drawing.Size(123, 18)
        Me.fndSpecies.TabIndex = 366
        Me.fndSpecies.Value = ""
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(142, 78)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(40, 17)
        Me.RadioButton2.TabIndex = 66
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "Yes"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(187, 78)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(40, 17)
        Me.RadioButton1.TabIndex = 65
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "No"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'fndCode
        '
        Me.fndCode.FieldName = Nothing
        Me.fndCode.Location = New System.Drawing.Point(53, 34)
        Me.fndCode.MendatroryField = True
        Me.fndCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCode.MyLinkLable1 = Nothing
        Me.fndCode.MyLinkLable2 = Nothing
        Me.fndCode.MyMaxLength = 30
        Me.fndCode.MyReadOnly = False
        Me.fndCode.Name = "fndCode"
        Me.fndCode.Size = New System.Drawing.Size(225, 18)
        Me.fndCode.TabIndex = 62
        Me.fndCode.TabStop = False
        Me.fndCode.Value = ""
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.fndVidhanSabha)
        Me.RadPageViewPage2.Controls.Add(Me.fndPanchayatSamiti)
        Me.RadPageViewPage2.Controls.Add(Me.fndGramPanchayat)
        Me.RadPageViewPage2.Controls.Add(Me.fndRevenueVillage)
        Me.RadPageViewPage2.Controls.Add(Me.fndZone)
        Me.RadPageViewPage2.Controls.Add(Me.fndBlock)
        Me.RadPageViewPage2.Controls.Add(Me.fndDistrict)
        Me.RadPageViewPage2.Controls.Add(Me.fndSupervisorCode)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(92.0!, 24.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 33)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(957, 343)
        Me.RadPageViewPage2.Text = "Bull Information"
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
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.fndCompanyBank1)
        Me.RadPageViewPage3.Controls.Add(Me.fndCompanyBank)
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(104.0!, 24.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 33)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(957, 343)
        Me.RadPageViewPage3.Text = "Bull General Detail"
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
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(104, 14)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 8
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(900, 14)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(29, 13)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 7
        Me.btnSave.Text = "Save"
        '
        'frmBullMasters
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(992, 473)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmBullMasters"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmBullMasters"
        CType(Me.lblBullSourcePrintStrew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDamLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBullRating, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRFID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExistDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStatusChangeDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPenId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShedId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSSCentre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBullBookValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExoticBullPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBullSubStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBullAliasName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBullStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSSBullId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDOB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPrevBullId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBreed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl12DigitBullId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCountryCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSpecies, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSemen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDob, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRegDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStatusDateChanged, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.cmbBullSourceForPaintingStraws, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBullRating, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCountry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents fndVidhanSabha As common.UserControls.txtFinder
    Friend WithEvents fndPanchayatSamiti As common.UserControls.txtFinder
    Friend WithEvents fndGramPanchayat As common.UserControls.txtFinder
    Friend WithEvents fndRevenueVillage As common.UserControls.txtFinder
    Friend WithEvents fndZone As common.UserControls.txtFinder
    Friend WithEvents fndBlock As common.UserControls.txtFinder
    Friend WithEvents fndDistrict As common.UserControls.txtFinder
    Friend WithEvents fndSupervisorCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents fndCompanyBank1 As common.UserControls.txtFinder
    Friend WithEvents fndCompanyBank As common.UserControls.txtFinder
    Friend WithEvents MyLabel32 As common.Controls.MyLabel
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents lblCountryCode As common.Controls.MyLabel
    Friend WithEvents fndCategory As common.UserControls.txtFinder
    Friend WithEvents lblCategory As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents fndSpecies As common.UserControls.txtFinder
    Friend WithEvents lblSpecies As common.Controls.MyLabel
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents lblSemen As common.Controls.MyLabel
    Friend WithEvents lblbankcode As common.Controls.MyLabel
    Friend WithEvents fndCode As common.UserControls.txtNavigator
    Friend WithEvents lblSubCategory As common.Controls.MyLabel
    Friend WithEvents fndSubCategory As common.UserControls.txtFinder
    Friend WithEvents lbl12DigitBullId As common.Controls.MyLabel
    Friend WithEvents lblBreed As common.Controls.MyLabel
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents lblPrevBullId As common.Controls.MyLabel
    Friend WithEvents fndBreed As common.UserControls.txtFinder
    Friend WithEvents lblDOB As common.Controls.MyLabel
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents lblSSBullId As common.Controls.MyLabel
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents lblBullStatus As common.Controls.MyLabel
    Friend WithEvents fndBullStatus As common.UserControls.txtFinder
    Friend WithEvents lblBullAliasName As common.Controls.MyLabel
    Friend WithEvents lblBullSubStatus As common.Controls.MyLabel
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents fndSubStatus As common.UserControls.txtFinder
    Friend WithEvents lblExoticBullPercentage As common.Controls.MyLabel
    Friend WithEvents lblBullBookValue As common.Controls.MyLabel
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents lblSSCentre As common.Controls.MyLabel
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents fndSSCentre As common.UserControls.txtFinder
    Friend WithEvents lblShedId As common.Controls.MyLabel
    Friend WithEvents lblPenId As common.Controls.MyLabel
    Friend WithEvents fndShedId As common.UserControls.txtFinder
    Friend WithEvents fndPenId As common.UserControls.txtFinder
    Friend WithEvents lblStatusChangeDate As common.Controls.MyLabel
    Friend WithEvents lblExistDate As common.Controls.MyLabel
    Friend WithEvents lblRFID As common.Controls.MyLabel
    Friend WithEvents lblRemark As common.Controls.MyLabel
    Friend WithEvents TextBox7 As TextBox
    Friend WithEvents lblBullRating As common.Controls.MyLabel
    Friend WithEvents TextBox8 As TextBox
    Friend WithEvents lblDamLocation As common.Controls.MyLabel
    Friend WithEvents lblBullSourcePrintStrew As common.Controls.MyLabel
    Friend WithEvents TextBox9 As TextBox
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents cmbCountry As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents cmbBullRating As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents cmbBullSourceForPaintingStraws As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents Label27 As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtDob As common.Controls.MyDateTimePicker
    Friend WithEvents txtRegDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtStatusDateChanged As common.Controls.MyDateTimePicker
    Friend WithEvents txtEndDate As common.Controls.MyDateTimePicker
End Class
