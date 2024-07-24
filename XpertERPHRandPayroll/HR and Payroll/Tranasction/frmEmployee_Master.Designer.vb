Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmployee_Master
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition7 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition8 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition9 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition10 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition11 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition12 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.General = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtActiveInactiveDate = New common.Controls.MyDateTimePicker()
        Me.lblActiveInactiveDate = New common.Controls.MyLabel()
        Me.cboemployeebasistype = New common.Controls.MyComboBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel42 = New common.Controls.MyLabel()
        Me.txtBiometricEmpID = New common.Controls.MyTextBox()
        Me.MyLabel40 = New common.Controls.MyLabel()
        Me.fndcity = New common.UserControls.txtFinder()
        Me.MyLabel36 = New common.Controls.MyLabel()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.txtWorkingLocation = New common.UserControls.txtFinder()
        Me.lblLocation2 = New common.Controls.MyLabel()
        Me.txtEmployeeBand = New common.UserControls.txtFinder()
        Me.chkHoldsalary = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtBloodGroup = New common.Controls.MyTextBox()
        Me.MyLabel93 = New common.Controls.MyLabel()
        Me.txtSubDepartment = New common.UserControls.txtFinder()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblSubDepartment = New common.Controls.MyLabel()
        Me.txtUINNo = New common.Controls.MyTextBox()
        Me.MyLabel92 = New common.Controls.MyLabel()
        Me.txtAdvToStaff = New common.UserControls.txtFinder()
        Me.MyLabel85 = New common.Controls.MyLabel()
        Me.txtname = New common.Controls.MyTextBox()
        Me.lblname = New common.Controls.MyLabel()
        Me.txtSalaryAccount = New common.UserControls.txtFinder()
        Me.MyLabel84 = New common.Controls.MyLabel()
        Me.lbldatebirth = New common.Controls.MyLabel()
        Me.lbldes = New common.Controls.MyLabel()
        Me.MyLabel80 = New common.Controls.MyLabel()
        Me.txtWardCircle = New common.Controls.MyTextBox()
        Me.txtDOB = New common.Controls.MyDateTimePicker()
        Me.lbljoin = New common.Controls.MyLabel()
        Me.lblcardno = New common.Controls.MyLabel()
        Me.txtcardno = New common.Controls.MyTextBox()
        Me.txtJoiningDate = New common.Controls.MyDateTimePicker()
        Me.TxtDesignation = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtDepartment = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.CboGender = New common.Controls.MyComboBox()
        Me.txtOccupation = New common.UserControls.txtFinder()
        Me.CboMaritalStatus = New common.Controls.MyComboBox()
        Me.txtShift = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtEmptyEx = New common.MyNumBox()
        Me.MyLabel70 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel75 = New common.Controls.MyLabel()
        Me.TxtGLAccount = New common.UserControls.txtFinder()
        Me.MyLabel34 = New common.Controls.MyLabel()
        Me.txtAnniversaryDate = New common.Controls.MyDateTimePicker()
        Me.txtSpouseName = New common.Controls.MyTextBox()
        Me.txtDivision = New common.UserControls.txtFinder()
        Me.MyLabel74 = New common.Controls.MyLabel()
        Me.MyLabel37 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtPayRollCode = New common.Controls.MyTextBox()
        Me.txtMothersName = New common.Controls.MyTextBox()
        Me.txtGrade = New common.UserControls.txtFinder()
        Me.MyLabel73 = New common.Controls.MyLabel()
        Me.MyLabel51 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtReligion = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.TxtFathersName = New common.Controls.MyTextBox()
        Me.txtBranch = New common.UserControls.txtFinder()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtAttendance = New common.UserControls.txtFinder()
        Me.txtProbationEndDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.MyLabel69 = New common.Controls.MyLabel()
        Me.txtConfirmationDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.txtCastCategory = New common.UserControls.txtFinder()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.txtCompanyCode = New common.UserControls.txtFinder()
        Me.CboEmployeeStatus = New common.Controls.MyComboBox()
        Me.lblstatus = New common.Controls.MyLabel()
        Me.Contact = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtAdd1_Verfi_Remarks = New common.Controls.MyTextBox()
        Me.MyLabel104 = New common.Controls.MyLabel()
        Me.chkAdd1_Verified = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel103 = New common.Controls.MyLabel()
        Me.cboAdd1_Type = New common.Controls.MyComboBox()
        Me.txtAdd1_PoliceStation = New common.Controls.MyTextBox()
        Me.MyLabel102 = New common.Controls.MyLabel()
        Me.txtAdd1_PostOffice = New common.Controls.MyTextBox()
        Me.MyLabel101 = New common.Controls.MyLabel()
        Me.txtAdd1_Village = New common.Controls.MyTextBox()
        Me.MyLabel100 = New common.Controls.MyLabel()
        Me.txtAdd1_Tehsil = New common.Controls.MyTextBox()
        Me.MyLabel99 = New common.Controls.MyLabel()
        Me.txtPermCountry = New common.UserControls.txtFinder()
        Me.MyLabel30 = New common.Controls.MyLabel()
        Me.txtPermMobileNo = New common.Controls.MyTextBox()
        Me.MyLabel26 = New common.Controls.MyLabel()
        Me.txtPermPhoneNo = New common.Controls.MyTextBox()
        Me.MyLabel27 = New common.Controls.MyLabel()
        Me.txtPermPostalCode = New common.Controls.MyTextBox()
        Me.MyLabel25 = New common.Controls.MyLabel()
        Me.chkSame = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtPermCity = New common.UserControls.txtFinder()
        Me.MyLabel28 = New common.Controls.MyLabel()
        Me.txtPermState = New common.UserControls.txtFinder()
        Me.MyLabel29 = New common.Controls.MyLabel()
        Me.txtPermAddress = New common.Controls.MyTextBox()
        Me.MyLabel31 = New common.Controls.MyLabel()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtDLNo = New common.Controls.MyTextBox()
        Me.MyLabel97 = New common.Controls.MyLabel()
        Me.txtVoterCard = New common.Controls.MyTextBox()
        Me.MyLabel98 = New common.Controls.MyLabel()
        Me.txtRationCard = New common.Controls.MyTextBox()
        Me.MyLabel95 = New common.Controls.MyLabel()
        Me.txtAadharCard = New common.Controls.MyTextBox()
        Me.MyLabel96 = New common.Controls.MyLabel()
        Me.txtAlternateEmail = New common.Controls.MyTextBox()
        Me.MyLabel94 = New common.Controls.MyLabel()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.MyLabel35 = New common.Controls.MyLabel()
        Me.txtEmail = New common.Controls.MyTextBox()
        Me.MyLabel33 = New common.Controls.MyLabel()
        Me.txtPassportNo = New common.Controls.MyTextBox()
        Me.MyLabel32 = New common.Controls.MyLabel()
        Me.txtPanNo = New common.Controls.MyTextBox()
        Me.MyLabel41 = New common.Controls.MyLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtAdd2_Verifi_Remarks = New common.Controls.MyTextBox()
        Me.MyLabel105 = New common.Controls.MyLabel()
        Me.chkAdd2_Verified = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel106 = New common.Controls.MyLabel()
        Me.cboAdd2_Type = New common.Controls.MyComboBox()
        Me.txtAdd2_PoliceStation = New common.Controls.MyTextBox()
        Me.MyLabel107 = New common.Controls.MyLabel()
        Me.txtAdd2_PostOffice = New common.Controls.MyTextBox()
        Me.MyLabel108 = New common.Controls.MyLabel()
        Me.txtAdd2_Village = New common.Controls.MyTextBox()
        Me.MyLabel109 = New common.Controls.MyLabel()
        Me.txtAdd2_Tehsil = New common.Controls.MyTextBox()
        Me.MyLabel110 = New common.Controls.MyLabel()
        Me.txtPresentCountry = New common.UserControls.txtFinder()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.txtPresentMobileNo = New common.Controls.MyTextBox()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.txtPresentPhoneNo = New common.Controls.MyTextBox()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.txtPresentPostalCode = New common.Controls.MyTextBox()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.txtPresentCity = New common.UserControls.txtFinder()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.txtPresentState = New common.UserControls.txtFinder()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.txtPresentAddress = New common.Controls.MyTextBox()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.Documents = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvEmpDoc = New common.UserControls.MyRadGridView()
        Me.Experience = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvEmpEx = New common.UserControls.MyRadGridView()
        Me.Qualification = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvEmpQuli = New common.UserControls.MyRadGridView()
        Me.Languages = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvEmpLanguage = New common.UserControls.MyRadGridView()
        Me.txtFamilyAge = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvEmpFamily = New common.UserControls.MyRadGridView()
        Me.PageResignation = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtRelevingDate = New common.Controls.MyDateTimePicker()
        Me.lblreleaving = New common.Controls.MyLabel()
        Me.chkNoDues = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtResigSubDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel82 = New common.Controls.MyLabel()
        Me.txtNoticeInDays = New common.MyNumBox()
        Me.MyLabel83 = New common.Controls.MyLabel()
        Me.txtReasonOfLeaving = New common.Controls.MyTextBox()
        Me.MyLabel81 = New common.Controls.MyLabel()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvAssets = New common.UserControls.MyRadGridView()
        Me.pageOthers = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.MyLabel43 = New common.Controls.MyLabel()
        Me.txtGPFNo = New common.Controls.MyTextBox()
        Me.txtTransferPF = New common.Controls.MyTextBox()
        Me.chlTransPF = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtUANNo = New common.Controls.MyTextBox()
        Me.MyLabel79 = New common.Controls.MyLabel()
        Me.lblUANNo = New common.Controls.MyLabel()
        Me.txtEPFMaxLimit = New common.MyNumBox()
        Me.lblMaxLimit = New common.Controls.MyLabel()
        Me.lblEPFRate = New common.Controls.MyLabel()
        Me.txtEPFRate = New common.MyNumBox()
        Me.MyLabel76 = New common.Controls.MyLabel()
        Me.txtEsiNo = New common.Controls.MyTextBox()
        Me.cboPFCalculatnType = New common.Controls.MyComboBox()
        Me.chkApplyESI = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtPFNo = New common.Controls.MyTextBox()
        Me.MyLabel77 = New common.Controls.MyLabel()
        Me.txtPFNoforDeptFile = New common.Controls.MyTextBox()
        Me.MyLabel78 = New common.Controls.MyLabel()
        Me.chkPFApplicable = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtESIDispensary = New common.Controls.MyTextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.MyLabel47 = New common.Controls.MyLabel()
        Me.MyLabel46 = New common.Controls.MyLabel()
        Me.txtmembershipid = New common.Controls.MyTextBox()
        Me.txtspecialdesc = New common.Controls.MyTextBox()
        Me.MyLabel45 = New common.Controls.MyLabel()
        Me.txtpolicy = New common.Controls.MyTextBox()
        Me.MyLabel44 = New common.Controls.MyLabel()
        Me.txtLICID = New common.Controls.MyTextBox()
        Me.txtSecChequeRs100 = New common.Controls.MyTextBox()
        Me.MyLabel52 = New common.Controls.MyLabel()
        Me.txtSecChequeLac1 = New common.Controls.MyTextBox()
        Me.MyLabel50 = New common.Controls.MyLabel()
        Me.fndPaymentMode = New common.UserControls.txtFinder()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtaccno = New common.Controls.MyTextBox()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtBank = New common.UserControls.txtFinder()
        Me.MyLabel86 = New common.Controls.MyLabel()
        Me.cboEmpNature = New common.Controls.MyComboBox()
        Me.cboConveyanceType = New common.Controls.MyComboBox()
        Me.MyLabel87 = New common.Controls.MyLabel()
        Me.chkOTApplicable = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkODApplicable = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkShowInStatory = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel88 = New common.Controls.MyLabel()
        Me.txtMinBasicSalary = New common.MyNumBox()
        Me.MyLabel89 = New common.Controls.MyLabel()
        Me.fndVendor = New common.UserControls.txtFinder()
        Me.MyLabel90 = New common.Controls.MyLabel()
        Me.TxtAgeFPen = New common.MyNumBox()
        Me.MyLabel71 = New common.Controls.MyLabel()
        Me.fndAgent = New common.UserControls.txtFinder()
        Me.MyLabel91 = New common.Controls.MyLabel()
        Me.MyLabel39 = New common.Controls.MyLabel()
        Me.fndUser = New common.UserControls.txtFinder()
        Me.txtbankname = New common.Controls.MyTextBox()
        Me.txtBankBranch = New common.Controls.MyTextBox()
        Me.MyLabel38 = New common.Controls.MyLabel()
        Me.lblBankBranch = New common.Controls.MyLabel()
        Me.TxtBankBranchName = New common.Controls.MyTextBox()
        Me.lbltype = New common.Controls.MyLabel()
        Me.CboEmployeeType = New common.Controls.MyComboBox()
        Me.grpFranchise = New System.Windows.Forms.GroupBox()
        Me.lblFranchiseCode = New common.Controls.MyLabel()
        Me.lblFranchiseName = New common.Controls.MyLabel()
        Me.txtFranchiseCode = New common.UserControls.txtFinder()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblempcode = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtLeavingDate = New common.Controls.MyDateTimePicker()
        Me.dtpJoining = New common.Controls.MyDateTimePicker()
        Me.MyLabel48 = New common.Controls.MyLabel()
        Me.txtCompBank = New common.UserControls.txtFinder()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.General.SuspendLayout()
        CType(Me.txtActiveInactiveDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblActiveInactiveDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboemployeebasistype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBiometricEmpID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel36, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkHoldsalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBloodGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel93, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUINNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel92, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel85, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel84, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldatebirth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel80, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWardCircle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDOB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbljoin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcardno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcardno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtJoiningDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboGender, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboMaritalStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmptyEx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel70, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel75, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAnniversaryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSpouseName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel74, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPayRollCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMothersName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel73, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel51, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFathersName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProbationEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel69, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtConfirmationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboEmployeeStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblstatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Contact.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.txtAdd1_Verfi_Remarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel104, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAdd1_Verified, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel103, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboAdd1_Type, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd1_PoliceStation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel102, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd1_PostOffice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel101, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd1_Village, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel100, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd1_Tehsil, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel99, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPermMobileNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPermPhoneNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPermPostalCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSame, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPermAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.txtDLNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel97, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVoterCard, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel98, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRationCard, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel95, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAadharCard, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel96, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAlternateEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel94, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPassportNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.txtAdd2_Verifi_Remarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel105, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAdd2_Verified, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel106, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboAdd2_Type, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd2_PoliceStation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel107, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd2_PostOffice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel108, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd2_Village, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel109, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd2_Tehsil, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel110, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPresentMobileNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPresentPhoneNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPresentPostalCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPresentAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Documents.SuspendLayout()
        CType(Me.gvEmpDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvEmpDoc.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Experience.SuspendLayout()
        CType(Me.gvEmpEx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvEmpEx.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Qualification.SuspendLayout()
        CType(Me.gvEmpQuli, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvEmpQuli.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Languages.SuspendLayout()
        CType(Me.gvEmpLanguage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvEmpLanguage.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.txtFamilyAge.SuspendLayout()
        CType(Me.gvEmpFamily, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvEmpFamily.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PageResignation.SuspendLayout()
        CType(Me.txtRelevingDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblreleaving, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkNoDues, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtResigSubDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel82, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoticeInDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel83, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReasonOfLeaving, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel81, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.gvAssets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAssets.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageOthers.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGPFNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTransferPF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chlTransPF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUANNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel79, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUANNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEPFMaxLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMaxLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEPFRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEPFRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel76, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEsiNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPFCalculatnType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkApplyESI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPFNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel77, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPFNoforDeptFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel78, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPFApplicable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtESIDispensary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MyLabel47, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel46, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtmembershipid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtspecialdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel45, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtpolicy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLICID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSecChequeRs100, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel52, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSecChequeLac1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel50, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtaccno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel86, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboEmpNature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboConveyanceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel87, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOTApplicable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkODApplicable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkShowInStatory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel88, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMinBasicSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel89, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel90, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAgeFPen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel71, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel91, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbankname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtBankBranchName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbltype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboEmployeeType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFranchise.SuspendLayout()
        CType(Me.lblFranchiseCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFranchiseName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblempcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtLeavingDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpJoining, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel48, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPageView1.Controls.Add(Me.General)
        Me.RadPageView1.Controls.Add(Me.Contact)
        Me.RadPageView1.Controls.Add(Me.Documents)
        Me.RadPageView1.Controls.Add(Me.Experience)
        Me.RadPageView1.Controls.Add(Me.Qualification)
        Me.RadPageView1.Controls.Add(Me.Languages)
        Me.RadPageView1.Controls.Add(Me.txtFamilyAge)
        Me.RadPageView1.Controls.Add(Me.PageResignation)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.pageOthers)
        Me.RadPageView1.Location = New System.Drawing.Point(5, 35)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.General
        Me.RadPageView1.Size = New System.Drawing.Size(866, 460)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemFitMode = Telerik.WinControls.UI.StripViewItemFitMode.None
        '
        'General
        '
        Me.General.Controls.Add(Me.MyLabel48)
        Me.General.Controls.Add(Me.txtCompBank)
        Me.General.Controls.Add(Me.txtActiveInactiveDate)
        Me.General.Controls.Add(Me.lblActiveInactiveDate)
        Me.General.Controls.Add(Me.cboemployeebasistype)
        Me.General.Controls.Add(Me.MyLabel42)
        Me.General.Controls.Add(Me.txtBiometricEmpID)
        Me.General.Controls.Add(Me.MyLabel40)
        Me.General.Controls.Add(Me.fndcity)
        Me.General.Controls.Add(Me.MyLabel36)
        Me.General.Controls.Add(Me.MyLabel17)
        Me.General.Controls.Add(Me.txtWorkingLocation)
        Me.General.Controls.Add(Me.txtEmployeeBand)
        Me.General.Controls.Add(Me.chkHoldsalary)
        Me.General.Controls.Add(Me.txtBloodGroup)
        Me.General.Controls.Add(Me.MyLabel93)
        Me.General.Controls.Add(Me.lblLocation2)
        Me.General.Controls.Add(Me.txtSubDepartment)
        Me.General.Controls.Add(Me.lblSubDepartment)
        Me.General.Controls.Add(Me.txtUINNo)
        Me.General.Controls.Add(Me.MyLabel92)
        Me.General.Controls.Add(Me.txtAdvToStaff)
        Me.General.Controls.Add(Me.MyLabel85)
        Me.General.Controls.Add(Me.txtname)
        Me.General.Controls.Add(Me.txtSalaryAccount)
        Me.General.Controls.Add(Me.lbldatebirth)
        Me.General.Controls.Add(Me.MyLabel84)
        Me.General.Controls.Add(Me.lbldes)
        Me.General.Controls.Add(Me.MyLabel80)
        Me.General.Controls.Add(Me.txtWardCircle)
        Me.General.Controls.Add(Me.lblname)
        Me.General.Controls.Add(Me.txtDOB)
        Me.General.Controls.Add(Me.lbljoin)
        Me.General.Controls.Add(Me.lblcardno)
        Me.General.Controls.Add(Me.txtcardno)
        Me.General.Controls.Add(Me.txtJoiningDate)
        Me.General.Controls.Add(Me.TxtDesignation)
        Me.General.Controls.Add(Me.MyLabel2)
        Me.General.Controls.Add(Me.txtDepartment)
        Me.General.Controls.Add(Me.MyLabel3)
        Me.General.Controls.Add(Me.CboGender)
        Me.General.Controls.Add(Me.MyLabel4)
        Me.General.Controls.Add(Me.txtOccupation)
        Me.General.Controls.Add(Me.MyLabel7)
        Me.General.Controls.Add(Me.CboMaritalStatus)
        Me.General.Controls.Add(Me.txtShift)
        Me.General.Controls.Add(Me.MyLabel6)
        Me.General.Controls.Add(Me.txtEmptyEx)
        Me.General.Controls.Add(Me.MyLabel5)
        Me.General.Controls.Add(Me.MyLabel70)
        Me.General.Controls.Add(Me.MyLabel75)
        Me.General.Controls.Add(Me.TxtGLAccount)
        Me.General.Controls.Add(Me.txtAnniversaryDate)
        Me.General.Controls.Add(Me.MyLabel34)
        Me.General.Controls.Add(Me.txtSpouseName)
        Me.General.Controls.Add(Me.txtDivision)
        Me.General.Controls.Add(Me.MyLabel74)
        Me.General.Controls.Add(Me.MyLabel37)
        Me.General.Controls.Add(Me.MyLabel8)
        Me.General.Controls.Add(Me.txtPayRollCode)
        Me.General.Controls.Add(Me.txtMothersName)
        Me.General.Controls.Add(Me.txtGrade)
        Me.General.Controls.Add(Me.MyLabel73)
        Me.General.Controls.Add(Me.MyLabel51)
        Me.General.Controls.Add(Me.MyLabel9)
        Me.General.Controls.Add(Me.txtReligion)
        Me.General.Controls.Add(Me.TxtFathersName)
        Me.General.Controls.Add(Me.MyLabel1)
        Me.General.Controls.Add(Me.txtBranch)
        Me.General.Controls.Add(Me.MyLabel10)
        Me.General.Controls.Add(Me.txtAttendance)
        Me.General.Controls.Add(Me.txtProbationEndDate)
        Me.General.Controls.Add(Me.MyLabel16)
        Me.General.Controls.Add(Me.MyLabel69)
        Me.General.Controls.Add(Me.txtConfirmationDate)
        Me.General.Controls.Add(Me.MyLabel15)
        Me.General.Controls.Add(Me.txtCastCategory)
        Me.General.Controls.Add(Me.txtCompanyCode)
        Me.General.Controls.Add(Me.MyLabel14)
        Me.General.Controls.Add(Me.CboEmployeeStatus)
        Me.General.Controls.Add(Me.lblstatus)
        Me.General.ItemSize = New System.Drawing.SizeF(116.0!, 28.0!)
        Me.General.Location = New System.Drawing.Point(10, 37)
        Me.General.Name = "General"
        Me.General.Size = New System.Drawing.Size(845, 412)
        Me.General.Text = "General Information"
        '
        'txtActiveInactiveDate
        '
        Me.txtActiveInactiveDate.CalculationExpression = Nothing
        Me.txtActiveInactiveDate.CustomFormat = "dd/MM/yyyy"
        Me.txtActiveInactiveDate.FieldCode = Nothing
        Me.txtActiveInactiveDate.FieldDesc = Nothing
        Me.txtActiveInactiveDate.FieldMaxLength = 0
        Me.txtActiveInactiveDate.FieldName = Nothing
        Me.txtActiveInactiveDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActiveInactiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtActiveInactiveDate.isCalculatedField = False
        Me.txtActiveInactiveDate.IsSourceFromTable = False
        Me.txtActiveInactiveDate.IsSourceFromValueList = False
        Me.txtActiveInactiveDate.IsUnique = False
        Me.txtActiveInactiveDate.Location = New System.Drawing.Point(749, 82)
        Me.txtActiveInactiveDate.MendatroryField = False
        Me.txtActiveInactiveDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtActiveInactiveDate.MyLinkLable1 = Me.lblActiveInactiveDate
        Me.txtActiveInactiveDate.MyLinkLable2 = Nothing
        Me.txtActiveInactiveDate.Name = "txtActiveInactiveDate"
        Me.txtActiveInactiveDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtActiveInactiveDate.ReferenceFieldDesc = Nothing
        Me.txtActiveInactiveDate.ReferenceFieldName = Nothing
        Me.txtActiveInactiveDate.ReferenceTableName = Nothing
        Me.txtActiveInactiveDate.ShowCheckBox = True
        Me.txtActiveInactiveDate.Size = New System.Drawing.Size(96, 18)
        Me.txtActiveInactiveDate.TabIndex = 166
        Me.txtActiveInactiveDate.TabStop = False
        Me.txtActiveInactiveDate.Text = "03/05/2011"
        Me.txtActiveInactiveDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblActiveInactiveDate
        '
        Me.lblActiveInactiveDate.FieldName = Nothing
        Me.lblActiveInactiveDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActiveInactiveDate.Location = New System.Drawing.Point(669, 83)
        Me.lblActiveInactiveDate.Name = "lblActiveInactiveDate"
        Me.lblActiveInactiveDate.Size = New System.Drawing.Size(34, 16)
        Me.lblActiveInactiveDate.TabIndex = 167
        Me.lblActiveInactiveDate.Text = "Label"
        '
        'cboemployeebasistype
        '
        Me.cboemployeebasistype.AutoCompleteDisplayMember = Nothing
        Me.cboemployeebasistype.AutoCompleteValueMember = Nothing
        Me.cboemployeebasistype.CalculationExpression = Nothing
        Me.cboemployeebasistype.DropDownAnimationEnabled = True
        Me.cboemployeebasistype.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboemployeebasistype.FieldCode = Nothing
        Me.cboemployeebasistype.FieldDesc = Nothing
        Me.cboemployeebasistype.FieldMaxLength = 0
        Me.cboemployeebasistype.FieldName = Nothing
        Me.cboemployeebasistype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboemployeebasistype.isCalculatedField = False
        Me.cboemployeebasistype.IsSourceFromTable = False
        Me.cboemployeebasistype.IsSourceFromValueList = False
        Me.cboemployeebasistype.IsUnique = False
        RadListDataItem5.Text = "Single"
        RadListDataItem6.Text = "married"
        Me.cboemployeebasistype.Items.Add(RadListDataItem5)
        Me.cboemployeebasistype.Items.Add(RadListDataItem6)
        Me.cboemployeebasistype.Location = New System.Drawing.Point(435, 353)
        Me.cboemployeebasistype.MendatroryField = True
        Me.cboemployeebasistype.MyLinkLable1 = Me.MyLabel4
        Me.cboemployeebasistype.MyLinkLable2 = Nothing
        Me.cboemployeebasistype.Name = "cboemployeebasistype"
        Me.cboemployeebasistype.ReferenceFieldDesc = Nothing
        Me.cboemployeebasistype.ReferenceFieldName = Nothing
        Me.cboemployeebasistype.ReferenceTableName = Nothing
        Me.cboemployeebasistype.Size = New System.Drawing.Size(227, 18)
        Me.cboemployeebasistype.TabIndex = 165
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(3, 83)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel4.TabIndex = 75
        Me.MyLabel4.Text = "Marital Status"
        '
        'MyLabel42
        '
        Me.MyLabel42.FieldName = Nothing
        Me.MyLabel42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel42.Location = New System.Drawing.Point(326, 354)
        Me.MyLabel42.Name = "MyLabel42"
        Me.MyLabel42.Size = New System.Drawing.Size(89, 16)
        Me.MyLabel42.TabIndex = 164
        Me.MyLabel42.Text = "Emp Basis Type"
        '
        'txtBiometricEmpID
        '
        Me.txtBiometricEmpID.CalculationExpression = Nothing
        Me.txtBiometricEmpID.FieldCode = Nothing
        Me.txtBiometricEmpID.FieldDesc = Nothing
        Me.txtBiometricEmpID.FieldMaxLength = 0
        Me.txtBiometricEmpID.FieldName = Nothing
        Me.txtBiometricEmpID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBiometricEmpID.isCalculatedField = False
        Me.txtBiometricEmpID.IsSourceFromTable = False
        Me.txtBiometricEmpID.IsSourceFromValueList = False
        Me.txtBiometricEmpID.IsUnique = False
        Me.txtBiometricEmpID.Location = New System.Drawing.Point(103, 353)
        Me.txtBiometricEmpID.MaxLength = 49
        Me.txtBiometricEmpID.MendatroryField = False
        Me.txtBiometricEmpID.MyLinkLable1 = Nothing
        Me.txtBiometricEmpID.MyLinkLable2 = Nothing
        Me.txtBiometricEmpID.Name = "txtBiometricEmpID"
        Me.txtBiometricEmpID.ReferenceFieldDesc = Nothing
        Me.txtBiometricEmpID.ReferenceFieldName = Nothing
        Me.txtBiometricEmpID.ReferenceTableName = Nothing
        Me.txtBiometricEmpID.Size = New System.Drawing.Size(217, 18)
        Me.txtBiometricEmpID.TabIndex = 162
        '
        'MyLabel40
        '
        Me.MyLabel40.FieldName = Nothing
        Me.MyLabel40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel40.Location = New System.Drawing.Point(4, 354)
        Me.MyLabel40.Name = "MyLabel40"
        Me.MyLabel40.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel40.TabIndex = 163
        Me.MyLabel40.Text = "Biometric Emp ID"
        '
        'fndcity
        '
        Me.fndcity.CalculationExpression = Nothing
        Me.fndcity.FieldCode = Nothing
        Me.fndcity.FieldDesc = Nothing
        Me.fndcity.FieldMaxLength = 0
        Me.fndcity.FieldName = Nothing
        Me.fndcity.isCalculatedField = False
        Me.fndcity.IsSourceFromTable = False
        Me.fndcity.IsSourceFromValueList = False
        Me.fndcity.IsUnique = False
        Me.fndcity.Location = New System.Drawing.Point(435, 206)
        Me.fndcity.MendatroryField = False
        Me.fndcity.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcity.MyLinkLable1 = Me.MyLabel36
        Me.fndcity.MyLinkLable2 = Nothing
        Me.fndcity.MyReadOnly = False
        Me.fndcity.MyShowMasterFormButton = False
        Me.fndcity.Name = "fndcity"
        Me.fndcity.ReferenceFieldDesc = Nothing
        Me.fndcity.ReferenceFieldName = Nothing
        Me.fndcity.ReferenceTableName = Nothing
        Me.fndcity.Size = New System.Drawing.Size(228, 19)
        Me.fndcity.TabIndex = 160
        Me.fndcity.Value = ""
        '
        'MyLabel36
        '
        Me.MyLabel36.FieldName = Nothing
        Me.MyLabel36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel36.Location = New System.Drawing.Point(326, 207)
        Me.MyLabel36.Name = "MyLabel36"
        Me.MyLabel36.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel36.TabIndex = 161
        Me.MyLabel36.Text = "Working Location"
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(3, 270)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel17.TabIndex = 159
        Me.MyLabel17.Text = "Employee Band"
        '
        'txtWorkingLocation
        '
        Me.txtWorkingLocation.CalculationExpression = Nothing
        Me.txtWorkingLocation.FieldCode = Nothing
        Me.txtWorkingLocation.FieldDesc = Nothing
        Me.txtWorkingLocation.FieldMaxLength = 0
        Me.txtWorkingLocation.FieldName = Nothing
        Me.txtWorkingLocation.isCalculatedField = False
        Me.txtWorkingLocation.IsSourceFromTable = False
        Me.txtWorkingLocation.IsSourceFromValueList = False
        Me.txtWorkingLocation.IsUnique = False
        Me.txtWorkingLocation.Location = New System.Drawing.Point(435, 331)
        Me.txtWorkingLocation.MendatroryField = False
        Me.txtWorkingLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWorkingLocation.MyLinkLable1 = Me.lblLocation2
        Me.txtWorkingLocation.MyLinkLable2 = Nothing
        Me.txtWorkingLocation.MyReadOnly = False
        Me.txtWorkingLocation.MyShowMasterFormButton = False
        Me.txtWorkingLocation.Name = "txtWorkingLocation"
        Me.txtWorkingLocation.ReferenceFieldDesc = Nothing
        Me.txtWorkingLocation.ReferenceFieldName = Nothing
        Me.txtWorkingLocation.ReferenceTableName = Nothing
        Me.txtWorkingLocation.Size = New System.Drawing.Size(228, 19)
        Me.txtWorkingLocation.TabIndex = 24
        Me.txtWorkingLocation.Value = ""
        Me.txtWorkingLocation.Visible = False
        '
        'lblLocation2
        '
        Me.lblLocation2.FieldName = Nothing
        Me.lblLocation2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation2.Location = New System.Drawing.Point(326, 332)
        Me.lblLocation2.Name = "lblLocation2"
        Me.lblLocation2.Size = New System.Drawing.Size(94, 16)
        Me.lblLocation2.TabIndex = 155
        Me.lblLocation2.Text = "Working Location"
        Me.lblLocation2.Visible = False
        '
        'txtEmployeeBand
        '
        Me.txtEmployeeBand.CalculationExpression = Nothing
        Me.txtEmployeeBand.FieldCode = Nothing
        Me.txtEmployeeBand.FieldDesc = Nothing
        Me.txtEmployeeBand.FieldMaxLength = 0
        Me.txtEmployeeBand.FieldName = Nothing
        Me.txtEmployeeBand.isCalculatedField = False
        Me.txtEmployeeBand.IsSourceFromTable = False
        Me.txtEmployeeBand.IsSourceFromValueList = False
        Me.txtEmployeeBand.IsUnique = False
        Me.txtEmployeeBand.Location = New System.Drawing.Point(104, 269)
        Me.txtEmployeeBand.MendatroryField = False
        Me.txtEmployeeBand.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmployeeBand.MyLinkLable1 = Me.MyLabel17
        Me.txtEmployeeBand.MyLinkLable2 = Nothing
        Me.txtEmployeeBand.MyReadOnly = False
        Me.txtEmployeeBand.MyShowMasterFormButton = False
        Me.txtEmployeeBand.Name = "txtEmployeeBand"
        Me.txtEmployeeBand.ReferenceFieldDesc = Nothing
        Me.txtEmployeeBand.ReferenceFieldName = Nothing
        Me.txtEmployeeBand.ReferenceTableName = Nothing
        Me.txtEmployeeBand.Size = New System.Drawing.Size(216, 19)
        Me.txtEmployeeBand.TabIndex = 158
        Me.txtEmployeeBand.Value = ""
        '
        'chkHoldsalary
        '
        Me.chkHoldsalary.Location = New System.Drawing.Point(527, 42)
        Me.chkHoldsalary.Name = "chkHoldsalary"
        Me.chkHoldsalary.Size = New System.Drawing.Size(77, 18)
        Me.chkHoldsalary.TabIndex = 8
        Me.chkHoldsalary.Text = "Hold Salary"
        '
        'txtBloodGroup
        '
        Me.txtBloodGroup.CalculationExpression = Nothing
        Me.txtBloodGroup.FieldCode = Nothing
        Me.txtBloodGroup.FieldDesc = Nothing
        Me.txtBloodGroup.FieldMaxLength = 0
        Me.txtBloodGroup.FieldName = Nothing
        Me.txtBloodGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBloodGroup.isCalculatedField = False
        Me.txtBloodGroup.IsSourceFromTable = False
        Me.txtBloodGroup.IsSourceFromValueList = False
        Me.txtBloodGroup.IsUnique = False
        Me.txtBloodGroup.Location = New System.Drawing.Point(435, 62)
        Me.txtBloodGroup.MaxLength = 49
        Me.txtBloodGroup.MendatroryField = False
        Me.txtBloodGroup.MyLinkLable1 = Nothing
        Me.txtBloodGroup.MyLinkLable2 = Nothing
        Me.txtBloodGroup.Name = "txtBloodGroup"
        Me.txtBloodGroup.ReferenceFieldDesc = Nothing
        Me.txtBloodGroup.ReferenceFieldName = Nothing
        Me.txtBloodGroup.ReferenceTableName = Nothing
        Me.txtBloodGroup.Size = New System.Drawing.Size(228, 18)
        Me.txtBloodGroup.TabIndex = 10
        '
        'MyLabel93
        '
        Me.MyLabel93.FieldName = Nothing
        Me.MyLabel93.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel93.Location = New System.Drawing.Point(326, 63)
        Me.MyLabel93.Name = "MyLabel93"
        Me.MyLabel93.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel93.TabIndex = 157
        Me.MyLabel93.Text = "Blood Group"
        '
        'txtSubDepartment
        '
        Me.txtSubDepartment.CalculationExpression = Nothing
        Me.txtSubDepartment.FieldCode = Nothing
        Me.txtSubDepartment.FieldDesc = Nothing
        Me.txtSubDepartment.FieldMaxLength = 0
        Me.txtSubDepartment.FieldName = Nothing
        Me.txtSubDepartment.isCalculatedField = False
        Me.txtSubDepartment.IsSourceFromTable = False
        Me.txtSubDepartment.IsSourceFromValueList = False
        Me.txtSubDepartment.IsUnique = False
        Me.txtSubDepartment.Location = New System.Drawing.Point(104, 185)
        Me.txtSubDepartment.MendatroryField = False
        Me.txtSubDepartment.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubDepartment.MyLinkLable1 = Me.MyLabel7
        Me.txtSubDepartment.MyLinkLable2 = Nothing
        Me.txtSubDepartment.MyReadOnly = False
        Me.txtSubDepartment.MyShowMasterFormButton = False
        Me.txtSubDepartment.Name = "txtSubDepartment"
        Me.txtSubDepartment.ReferenceFieldDesc = Nothing
        Me.txtSubDepartment.ReferenceFieldName = Nothing
        Me.txtSubDepartment.ReferenceTableName = Nothing
        Me.txtSubDepartment.Size = New System.Drawing.Size(216, 19)
        Me.txtSubDepartment.TabIndex = 21
        Me.txtSubDepartment.Value = ""
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(3, 207)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel7.TabIndex = 78
        Me.MyLabel7.Text = "Occupation"
        '
        'lblSubDepartment
        '
        Me.lblSubDepartment.FieldName = Nothing
        Me.lblSubDepartment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubDepartment.Location = New System.Drawing.Point(3, 186)
        Me.lblSubDepartment.Name = "lblSubDepartment"
        Me.lblSubDepartment.Size = New System.Drawing.Size(89, 16)
        Me.lblSubDepartment.TabIndex = 152
        Me.lblSubDepartment.Text = "Sub Department"
        '
        'txtUINNo
        '
        Me.txtUINNo.CalculationExpression = Nothing
        Me.txtUINNo.FieldCode = Nothing
        Me.txtUINNo.FieldDesc = Nothing
        Me.txtUINNo.FieldMaxLength = 0
        Me.txtUINNo.FieldName = Nothing
        Me.txtUINNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUINNo.isCalculatedField = False
        Me.txtUINNo.IsSourceFromTable = False
        Me.txtUINNo.IsSourceFromValueList = False
        Me.txtUINNo.IsUnique = False
        Me.txtUINNo.Location = New System.Drawing.Point(435, 227)
        Me.txtUINNo.MaxLength = 49
        Me.txtUINNo.MendatroryField = False
        Me.txtUINNo.MyLinkLable1 = Nothing
        Me.txtUINNo.MyLinkLable2 = Nothing
        Me.txtUINNo.Name = "txtUINNo"
        Me.txtUINNo.ReferenceFieldDesc = Nothing
        Me.txtUINNo.ReferenceFieldName = Nothing
        Me.txtUINNo.ReferenceTableName = Nothing
        Me.txtUINNo.Size = New System.Drawing.Size(228, 18)
        Me.txtUINNo.TabIndex = 26
        '
        'MyLabel92
        '
        Me.MyLabel92.FieldName = Nothing
        Me.MyLabel92.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel92.Location = New System.Drawing.Point(326, 228)
        Me.MyLabel92.Name = "MyLabel92"
        Me.MyLabel92.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel92.TabIndex = 148
        Me.MyLabel92.Text = "UIN No"
        '
        'txtAdvToStaff
        '
        Me.txtAdvToStaff.CalculationExpression = Nothing
        Me.txtAdvToStaff.FieldCode = Nothing
        Me.txtAdvToStaff.FieldDesc = Nothing
        Me.txtAdvToStaff.FieldMaxLength = 0
        Me.txtAdvToStaff.FieldName = Nothing
        Me.txtAdvToStaff.isCalculatedField = False
        Me.txtAdvToStaff.IsSourceFromTable = False
        Me.txtAdvToStaff.IsSourceFromValueList = False
        Me.txtAdvToStaff.IsUnique = False
        Me.txtAdvToStaff.Location = New System.Drawing.Point(435, 290)
        Me.txtAdvToStaff.MendatroryField = False
        Me.txtAdvToStaff.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvToStaff.MyLinkLable1 = Me.MyLabel85
        Me.txtAdvToStaff.MyLinkLable2 = Nothing
        Me.txtAdvToStaff.MyReadOnly = False
        Me.txtAdvToStaff.MyShowMasterFormButton = False
        Me.txtAdvToStaff.Name = "txtAdvToStaff"
        Me.txtAdvToStaff.ReferenceFieldDesc = Nothing
        Me.txtAdvToStaff.ReferenceFieldName = Nothing
        Me.txtAdvToStaff.ReferenceTableName = Nothing
        Me.txtAdvToStaff.Size = New System.Drawing.Size(228, 19)
        Me.txtAdvToStaff.TabIndex = 32
        Me.txtAdvToStaff.Value = ""
        '
        'MyLabel85
        '
        Me.MyLabel85.FieldName = Nothing
        Me.MyLabel85.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel85.Location = New System.Drawing.Point(326, 291)
        Me.MyLabel85.Name = "MyLabel85"
        Me.MyLabel85.Size = New System.Drawing.Size(93, 16)
        Me.MyLabel85.TabIndex = 146
        Me.MyLabel85.Text = "Advance To Staff"
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
        Me.txtname.Location = New System.Drawing.Point(104, 2)
        Me.txtname.MaxLength = 49
        Me.txtname.MendatroryField = True
        Me.txtname.MyLinkLable1 = Me.lblname
        Me.txtname.MyLinkLable2 = Nothing
        Me.txtname.Name = "txtname"
        Me.txtname.ReferenceFieldDesc = Nothing
        Me.txtname.ReferenceFieldName = Nothing
        Me.txtname.ReferenceTableName = Nothing
        Me.txtname.Size = New System.Drawing.Size(216, 18)
        Me.txtname.TabIndex = 0
        '
        'lblname
        '
        Me.lblname.FieldName = Nothing
        Me.lblname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblname.Location = New System.Drawing.Point(3, 3)
        Me.lblname.Name = "lblname"
        Me.lblname.Size = New System.Drawing.Size(90, 16)
        Me.lblname.TabIndex = 24
        Me.lblname.Text = "Employee Name"
        '
        'txtSalaryAccount
        '
        Me.txtSalaryAccount.CalculationExpression = Nothing
        Me.txtSalaryAccount.FieldCode = Nothing
        Me.txtSalaryAccount.FieldDesc = Nothing
        Me.txtSalaryAccount.FieldMaxLength = 0
        Me.txtSalaryAccount.FieldName = Nothing
        Me.txtSalaryAccount.isCalculatedField = False
        Me.txtSalaryAccount.IsSourceFromTable = False
        Me.txtSalaryAccount.IsSourceFromValueList = False
        Me.txtSalaryAccount.IsUnique = False
        Me.txtSalaryAccount.Location = New System.Drawing.Point(435, 269)
        Me.txtSalaryAccount.MendatroryField = False
        Me.txtSalaryAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalaryAccount.MyLinkLable1 = Me.MyLabel84
        Me.txtSalaryAccount.MyLinkLable2 = Nothing
        Me.txtSalaryAccount.MyReadOnly = False
        Me.txtSalaryAccount.MyShowMasterFormButton = False
        Me.txtSalaryAccount.Name = "txtSalaryAccount"
        Me.txtSalaryAccount.ReferenceFieldDesc = Nothing
        Me.txtSalaryAccount.ReferenceFieldName = Nothing
        Me.txtSalaryAccount.ReferenceTableName = Nothing
        Me.txtSalaryAccount.Size = New System.Drawing.Size(228, 19)
        Me.txtSalaryAccount.TabIndex = 30
        Me.txtSalaryAccount.Value = ""
        '
        'MyLabel84
        '
        Me.MyLabel84.FieldName = Nothing
        Me.MyLabel84.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel84.Location = New System.Drawing.Point(326, 270)
        Me.MyLabel84.Name = "MyLabel84"
        Me.MyLabel84.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel84.TabIndex = 145
        Me.MyLabel84.Text = "Salary Account"
        '
        'lbldatebirth
        '
        Me.lbldatebirth.FieldName = Nothing
        Me.lbldatebirth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldatebirth.Location = New System.Drawing.Point(527, 23)
        Me.lbldatebirth.Name = "lbldatebirth"
        Me.lbldatebirth.Size = New System.Drawing.Size(69, 16)
        Me.lbldatebirth.TabIndex = 28
        Me.lbldatebirth.Text = "Date of Birth"
        '
        'lbldes
        '
        Me.lbldes.FieldName = Nothing
        Me.lbldes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldes.Location = New System.Drawing.Point(3, 144)
        Me.lbldes.Name = "lbldes"
        Me.lbldes.Size = New System.Drawing.Size(66, 16)
        Me.lbldes.TabIndex = 0
        Me.lbldes.Text = "Designation"
        '
        'MyLabel80
        '
        Me.MyLabel80.FieldName = Nothing
        Me.MyLabel80.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel80.Location = New System.Drawing.Point(326, 249)
        Me.MyLabel80.Name = "MyLabel80"
        Me.MyLabel80.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel80.TabIndex = 138
        Me.MyLabel80.Text = "Ward / Circle"
        '
        'txtWardCircle
        '
        Me.txtWardCircle.CalculationExpression = Nothing
        Me.txtWardCircle.FieldCode = Nothing
        Me.txtWardCircle.FieldDesc = Nothing
        Me.txtWardCircle.FieldMaxLength = 0
        Me.txtWardCircle.FieldName = Nothing
        Me.txtWardCircle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWardCircle.isCalculatedField = False
        Me.txtWardCircle.IsSourceFromTable = False
        Me.txtWardCircle.IsSourceFromValueList = False
        Me.txtWardCircle.IsUnique = False
        Me.txtWardCircle.Location = New System.Drawing.Point(435, 248)
        Me.txtWardCircle.MaxLength = 49
        Me.txtWardCircle.MendatroryField = False
        Me.txtWardCircle.MyLinkLable1 = Me.MyLabel80
        Me.txtWardCircle.MyLinkLable2 = Nothing
        Me.txtWardCircle.Name = "txtWardCircle"
        Me.txtWardCircle.ReferenceFieldDesc = Nothing
        Me.txtWardCircle.ReferenceFieldName = Nothing
        Me.txtWardCircle.ReferenceTableName = Nothing
        Me.txtWardCircle.Size = New System.Drawing.Size(228, 18)
        Me.txtWardCircle.TabIndex = 28
        '
        'txtDOB
        '
        Me.txtDOB.CalculationExpression = Nothing
        Me.txtDOB.CustomFormat = "dd/MM/yyyy"
        Me.txtDOB.FieldCode = Nothing
        Me.txtDOB.FieldDesc = Nothing
        Me.txtDOB.FieldMaxLength = 0
        Me.txtDOB.FieldName = Nothing
        Me.txtDOB.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDOB.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDOB.isCalculatedField = False
        Me.txtDOB.IsSourceFromTable = False
        Me.txtDOB.IsSourceFromValueList = False
        Me.txtDOB.IsUnique = False
        Me.txtDOB.Location = New System.Drawing.Point(625, 22)
        Me.txtDOB.MendatroryField = False
        Me.txtDOB.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.txtDOB.MyLinkLable1 = Me.lbldatebirth
        Me.txtDOB.MyLinkLable2 = Nothing
        Me.txtDOB.Name = "txtDOB"
        Me.txtDOB.NullDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.txtDOB.ReferenceFieldDesc = Nothing
        Me.txtDOB.ReferenceFieldName = Nothing
        Me.txtDOB.ReferenceTableName = Nothing
        Me.txtDOB.Size = New System.Drawing.Size(90, 18)
        Me.txtDOB.TabIndex = 5
        Me.txtDOB.TabStop = False
        Me.txtDOB.Text = "03/05/2011"
        Me.txtDOB.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lbljoin
        '
        Me.lbljoin.FieldName = Nothing
        Me.lbljoin.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbljoin.Location = New System.Drawing.Point(326, 43)
        Me.lbljoin.Name = "lbljoin"
        Me.lbljoin.Size = New System.Drawing.Size(69, 16)
        Me.lbljoin.TabIndex = 54
        Me.lbljoin.Text = "Joining Date"
        '
        'lblcardno
        '
        Me.lblcardno.FieldName = Nothing
        Me.lblcardno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcardno.Location = New System.Drawing.Point(326, 312)
        Me.lblcardno.Name = "lblcardno"
        Me.lblcardno.Size = New System.Drawing.Size(49, 16)
        Me.lblcardno.TabIndex = 53
        Me.lblcardno.Text = "Card-No"
        '
        'txtcardno
        '
        Me.txtcardno.CalculationExpression = Nothing
        Me.txtcardno.FieldCode = Nothing
        Me.txtcardno.FieldDesc = Nothing
        Me.txtcardno.FieldMaxLength = 0
        Me.txtcardno.FieldName = Nothing
        Me.txtcardno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcardno.isCalculatedField = False
        Me.txtcardno.IsSourceFromTable = False
        Me.txtcardno.IsSourceFromValueList = False
        Me.txtcardno.IsUnique = False
        Me.txtcardno.Location = New System.Drawing.Point(435, 311)
        Me.txtcardno.MaxLength = 49
        Me.txtcardno.MendatroryField = False
        Me.txtcardno.MyLinkLable1 = Me.lblcardno
        Me.txtcardno.MyLinkLable2 = Nothing
        Me.txtcardno.Name = "txtcardno"
        Me.txtcardno.ReferenceFieldDesc = Nothing
        Me.txtcardno.ReferenceFieldName = Nothing
        Me.txtcardno.ReferenceTableName = Nothing
        Me.txtcardno.Size = New System.Drawing.Size(228, 18)
        Me.txtcardno.TabIndex = 34
        '
        'txtJoiningDate
        '
        Me.txtJoiningDate.CalculationExpression = Nothing
        Me.txtJoiningDate.CustomFormat = "dd/MM/yyyy"
        Me.txtJoiningDate.FieldCode = Nothing
        Me.txtJoiningDate.FieldDesc = Nothing
        Me.txtJoiningDate.FieldMaxLength = 0
        Me.txtJoiningDate.FieldName = Nothing
        Me.txtJoiningDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJoiningDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtJoiningDate.isCalculatedField = False
        Me.txtJoiningDate.IsSourceFromTable = False
        Me.txtJoiningDate.IsSourceFromValueList = False
        Me.txtJoiningDate.IsUnique = False
        Me.txtJoiningDate.Location = New System.Drawing.Point(435, 42)
        Me.txtJoiningDate.MendatroryField = False
        Me.txtJoiningDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtJoiningDate.MyLinkLable1 = Me.lbljoin
        Me.txtJoiningDate.MyLinkLable2 = Nothing
        Me.txtJoiningDate.Name = "txtJoiningDate"
        Me.txtJoiningDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtJoiningDate.ReferenceFieldDesc = Nothing
        Me.txtJoiningDate.ReferenceFieldName = Nothing
        Me.txtJoiningDate.ReferenceTableName = Nothing
        Me.txtJoiningDate.Size = New System.Drawing.Size(91, 18)
        Me.txtJoiningDate.TabIndex = 7
        Me.txtJoiningDate.TabStop = False
        Me.txtJoiningDate.Text = "03/05/2011"
        Me.txtJoiningDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'TxtDesignation
        '
        Me.TxtDesignation.CalculationExpression = Nothing
        Me.TxtDesignation.FieldCode = Nothing
        Me.TxtDesignation.FieldDesc = Nothing
        Me.TxtDesignation.FieldMaxLength = 0
        Me.TxtDesignation.FieldName = Nothing
        Me.TxtDesignation.isCalculatedField = False
        Me.TxtDesignation.IsSourceFromTable = False
        Me.TxtDesignation.IsSourceFromValueList = False
        Me.TxtDesignation.IsUnique = False
        Me.TxtDesignation.Location = New System.Drawing.Point(104, 143)
        Me.TxtDesignation.MendatroryField = False
        Me.TxtDesignation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDesignation.MyLinkLable1 = Me.lbldes
        Me.TxtDesignation.MyLinkLable2 = Nothing
        Me.TxtDesignation.MyReadOnly = False
        Me.TxtDesignation.MyShowMasterFormButton = False
        Me.TxtDesignation.Name = "TxtDesignation"
        Me.TxtDesignation.ReferenceFieldDesc = Nothing
        Me.TxtDesignation.ReferenceFieldName = Nothing
        Me.TxtDesignation.ReferenceTableName = Nothing
        Me.TxtDesignation.Size = New System.Drawing.Size(216, 19)
        Me.TxtDesignation.TabIndex = 17
        Me.TxtDesignation.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(3, 165)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel2.TabIndex = 70
        Me.MyLabel2.Text = "Department"
        '
        'txtDepartment
        '
        Me.txtDepartment.CalculationExpression = Nothing
        Me.txtDepartment.FieldCode = Nothing
        Me.txtDepartment.FieldDesc = Nothing
        Me.txtDepartment.FieldMaxLength = 0
        Me.txtDepartment.FieldName = Nothing
        Me.txtDepartment.isCalculatedField = False
        Me.txtDepartment.IsSourceFromTable = False
        Me.txtDepartment.IsSourceFromValueList = False
        Me.txtDepartment.IsUnique = False
        Me.txtDepartment.Location = New System.Drawing.Point(104, 164)
        Me.txtDepartment.MendatroryField = True
        Me.txtDepartment.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepartment.MyLinkLable1 = Me.MyLabel2
        Me.txtDepartment.MyLinkLable2 = Nothing
        Me.txtDepartment.MyReadOnly = False
        Me.txtDepartment.MyShowMasterFormButton = False
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.ReferenceFieldDesc = Nothing
        Me.txtDepartment.ReferenceFieldName = Nothing
        Me.txtDepartment.ReferenceTableName = Nothing
        Me.txtDepartment.Size = New System.Drawing.Size(216, 19)
        Me.txtDepartment.TabIndex = 19
        Me.txtDepartment.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(3, 23)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel3.TabIndex = 73
        Me.MyLabel3.Text = "Gender"
        '
        'CboGender
        '
        Me.CboGender.AutoCompleteDisplayMember = Nothing
        Me.CboGender.AutoCompleteValueMember = Nothing
        Me.CboGender.CalculationExpression = Nothing
        Me.CboGender.DropDownAnimationEnabled = True
        Me.CboGender.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboGender.FieldCode = Nothing
        Me.CboGender.FieldDesc = Nothing
        Me.CboGender.FieldMaxLength = 0
        Me.CboGender.FieldName = Nothing
        Me.CboGender.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboGender.isCalculatedField = False
        Me.CboGender.IsSourceFromTable = False
        Me.CboGender.IsSourceFromValueList = False
        Me.CboGender.IsUnique = False
        Me.CboGender.Location = New System.Drawing.Point(104, 22)
        Me.CboGender.MendatroryField = True
        Me.CboGender.MyLinkLable1 = Me.MyLabel3
        Me.CboGender.MyLinkLable2 = Nothing
        Me.CboGender.Name = "CboGender"
        Me.CboGender.ReferenceFieldDesc = Nothing
        Me.CboGender.ReferenceFieldName = Nothing
        Me.CboGender.ReferenceTableName = Nothing
        Me.CboGender.Size = New System.Drawing.Size(216, 18)
        Me.CboGender.TabIndex = 3
        '
        'txtOccupation
        '
        Me.txtOccupation.CalculationExpression = Nothing
        Me.txtOccupation.FieldCode = Nothing
        Me.txtOccupation.FieldDesc = Nothing
        Me.txtOccupation.FieldMaxLength = 0
        Me.txtOccupation.FieldName = Nothing
        Me.txtOccupation.isCalculatedField = False
        Me.txtOccupation.IsSourceFromTable = False
        Me.txtOccupation.IsSourceFromValueList = False
        Me.txtOccupation.IsUnique = False
        Me.txtOccupation.Location = New System.Drawing.Point(104, 206)
        Me.txtOccupation.MendatroryField = False
        Me.txtOccupation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOccupation.MyLinkLable1 = Me.MyLabel7
        Me.txtOccupation.MyLinkLable2 = Nothing
        Me.txtOccupation.MyReadOnly = False
        Me.txtOccupation.MyShowMasterFormButton = False
        Me.txtOccupation.Name = "txtOccupation"
        Me.txtOccupation.ReferenceFieldDesc = Nothing
        Me.txtOccupation.ReferenceFieldName = Nothing
        Me.txtOccupation.ReferenceTableName = Nothing
        Me.txtOccupation.Size = New System.Drawing.Size(216, 19)
        Me.txtOccupation.TabIndex = 23
        Me.txtOccupation.Value = ""
        '
        'CboMaritalStatus
        '
        Me.CboMaritalStatus.AutoCompleteDisplayMember = Nothing
        Me.CboMaritalStatus.AutoCompleteValueMember = Nothing
        Me.CboMaritalStatus.CalculationExpression = Nothing
        Me.CboMaritalStatus.DropDownAnimationEnabled = True
        Me.CboMaritalStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboMaritalStatus.FieldCode = Nothing
        Me.CboMaritalStatus.FieldDesc = Nothing
        Me.CboMaritalStatus.FieldMaxLength = 0
        Me.CboMaritalStatus.FieldName = Nothing
        Me.CboMaritalStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboMaritalStatus.isCalculatedField = False
        Me.CboMaritalStatus.IsSourceFromTable = False
        Me.CboMaritalStatus.IsSourceFromValueList = False
        Me.CboMaritalStatus.IsUnique = False
        RadListDataItem1.Text = "Single"
        RadListDataItem2.Text = "married"
        Me.CboMaritalStatus.Items.Add(RadListDataItem1)
        Me.CboMaritalStatus.Items.Add(RadListDataItem2)
        Me.CboMaritalStatus.Location = New System.Drawing.Point(104, 82)
        Me.CboMaritalStatus.MendatroryField = True
        Me.CboMaritalStatus.MyLinkLable1 = Me.MyLabel4
        Me.CboMaritalStatus.MyLinkLable2 = Nothing
        Me.CboMaritalStatus.Name = "CboMaritalStatus"
        Me.CboMaritalStatus.ReferenceFieldDesc = Nothing
        Me.CboMaritalStatus.ReferenceFieldName = Nothing
        Me.CboMaritalStatus.ReferenceTableName = Nothing
        Me.CboMaritalStatus.Size = New System.Drawing.Size(216, 18)
        Me.CboMaritalStatus.TabIndex = 11
        '
        'txtShift
        '
        Me.txtShift.CalculationExpression = Nothing
        Me.txtShift.FieldCode = Nothing
        Me.txtShift.FieldDesc = Nothing
        Me.txtShift.FieldMaxLength = 0
        Me.txtShift.FieldName = Nothing
        Me.txtShift.isCalculatedField = False
        Me.txtShift.IsSourceFromTable = False
        Me.txtShift.IsSourceFromValueList = False
        Me.txtShift.IsUnique = False
        Me.txtShift.Location = New System.Drawing.Point(104, 122)
        Me.txtShift.MendatroryField = False
        Me.txtShift.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShift.MyLinkLable1 = Me.lbldes
        Me.txtShift.MyLinkLable2 = Nothing
        Me.txtShift.MyReadOnly = False
        Me.txtShift.MyShowMasterFormButton = False
        Me.txtShift.Name = "txtShift"
        Me.txtShift.ReferenceFieldDesc = Nothing
        Me.txtShift.ReferenceFieldName = Nothing
        Me.txtShift.ReferenceTableName = Nothing
        Me.txtShift.Size = New System.Drawing.Size(216, 19)
        Me.txtShift.TabIndex = 15
        Me.txtShift.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(3, 228)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel6.TabIndex = 80
        Me.MyLabel6.Text = "Division"
        '
        'txtEmptyEx
        '
        Me.txtEmptyEx.BackColor = System.Drawing.Color.White
        Me.txtEmptyEx.CalculationExpression = Nothing
        Me.txtEmptyEx.DecimalPlaces = 2
        Me.txtEmptyEx.FieldCode = Nothing
        Me.txtEmptyEx.FieldDesc = Nothing
        Me.txtEmptyEx.FieldMaxLength = 0
        Me.txtEmptyEx.FieldName = Nothing
        Me.txtEmptyEx.isCalculatedField = False
        Me.txtEmptyEx.IsSourceFromTable = False
        Me.txtEmptyEx.IsSourceFromValueList = False
        Me.txtEmptyEx.IsUnique = False
        Me.txtEmptyEx.Location = New System.Drawing.Point(435, 184)
        Me.txtEmptyEx.MaxLength = 19
        Me.txtEmptyEx.MendatroryField = False
        Me.txtEmptyEx.MyLinkLable1 = Me.MyLabel70
        Me.txtEmptyEx.MyLinkLable2 = Nothing
        Me.txtEmptyEx.Name = "txtEmptyEx"
        Me.txtEmptyEx.ReferenceFieldDesc = Nothing
        Me.txtEmptyEx.ReferenceFieldName = Nothing
        Me.txtEmptyEx.ReferenceTableName = Nothing
        Me.txtEmptyEx.Size = New System.Drawing.Size(227, 20)
        Me.txtEmptyEx.TabIndex = 22
        Me.txtEmptyEx.Text = "0"
        Me.txtEmptyEx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtEmptyEx.Value = 0R
        '
        'MyLabel70
        '
        Me.MyLabel70.FieldName = Nothing
        Me.MyLabel70.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel70.Location = New System.Drawing.Point(326, 186)
        Me.MyLabel70.Name = "MyLabel70"
        Me.MyLabel70.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel70.TabIndex = 116
        Me.MyLabel70.Text = "Empty Ex"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(527, 3)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(93, 16)
        Me.MyLabel5.TabIndex = 77
        Me.MyLabel5.Text = "Anniversary Date"
        '
        'MyLabel75
        '
        Me.MyLabel75.FieldName = Nothing
        Me.MyLabel75.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel75.Location = New System.Drawing.Point(3, 103)
        Me.MyLabel75.Name = "MyLabel75"
        Me.MyLabel75.Size = New System.Drawing.Size(78, 16)
        Me.MyLabel75.TabIndex = 122
        Me.MyLabel75.Text = "Spouse Name"
        '
        'TxtGLAccount
        '
        Me.TxtGLAccount.CalculationExpression = Nothing
        Me.TxtGLAccount.FieldCode = Nothing
        Me.TxtGLAccount.FieldDesc = Nothing
        Me.TxtGLAccount.FieldMaxLength = 0
        Me.TxtGLAccount.FieldName = Nothing
        Me.TxtGLAccount.isCalculatedField = False
        Me.TxtGLAccount.IsSourceFromTable = False
        Me.TxtGLAccount.IsSourceFromValueList = False
        Me.TxtGLAccount.IsUnique = False
        Me.TxtGLAccount.Location = New System.Drawing.Point(435, 121)
        Me.TxtGLAccount.MendatroryField = False
        Me.TxtGLAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGLAccount.MyLinkLable1 = Me.MyLabel34
        Me.TxtGLAccount.MyLinkLable2 = Nothing
        Me.TxtGLAccount.MyReadOnly = False
        Me.TxtGLAccount.MyShowMasterFormButton = False
        Me.TxtGLAccount.Name = "TxtGLAccount"
        Me.TxtGLAccount.ReferenceFieldDesc = Nothing
        Me.TxtGLAccount.ReferenceFieldName = Nothing
        Me.TxtGLAccount.ReferenceTableName = Nothing
        Me.TxtGLAccount.Size = New System.Drawing.Size(228, 20)
        Me.TxtGLAccount.TabIndex = 16
        Me.TxtGLAccount.Value = ""
        '
        'MyLabel34
        '
        Me.MyLabel34.FieldName = Nothing
        Me.MyLabel34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel34.Location = New System.Drawing.Point(326, 123)
        Me.MyLabel34.Name = "MyLabel34"
        Me.MyLabel34.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel34.TabIndex = 112
        Me.MyLabel34.Text = "GL Account"
        '
        'txtAnniversaryDate
        '
        Me.txtAnniversaryDate.CalculationExpression = Nothing
        Me.txtAnniversaryDate.CustomFormat = "dd/MM/yyyy"
        Me.txtAnniversaryDate.FieldCode = Nothing
        Me.txtAnniversaryDate.FieldDesc = Nothing
        Me.txtAnniversaryDate.FieldMaxLength = 0
        Me.txtAnniversaryDate.FieldName = Nothing
        Me.txtAnniversaryDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAnniversaryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtAnniversaryDate.isCalculatedField = False
        Me.txtAnniversaryDate.IsSourceFromTable = False
        Me.txtAnniversaryDate.IsSourceFromValueList = False
        Me.txtAnniversaryDate.IsUnique = False
        Me.txtAnniversaryDate.Location = New System.Drawing.Point(625, 2)
        Me.txtAnniversaryDate.MendatroryField = False
        Me.txtAnniversaryDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtAnniversaryDate.MyLinkLable1 = Me.MyLabel5
        Me.txtAnniversaryDate.MyLinkLable2 = Nothing
        Me.txtAnniversaryDate.Name = "txtAnniversaryDate"
        Me.txtAnniversaryDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtAnniversaryDate.ReferenceFieldDesc = Nothing
        Me.txtAnniversaryDate.ReferenceFieldName = Nothing
        Me.txtAnniversaryDate.ReferenceTableName = Nothing
        Me.txtAnniversaryDate.ShowCheckBox = True
        Me.txtAnniversaryDate.Size = New System.Drawing.Size(90, 18)
        Me.txtAnniversaryDate.TabIndex = 2
        Me.txtAnniversaryDate.TabStop = False
        Me.txtAnniversaryDate.Text = "03/05/2011"
        Me.txtAnniversaryDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'txtSpouseName
        '
        Me.txtSpouseName.CalculationExpression = Nothing
        Me.txtSpouseName.FieldCode = Nothing
        Me.txtSpouseName.FieldDesc = Nothing
        Me.txtSpouseName.FieldMaxLength = 0
        Me.txtSpouseName.FieldName = Nothing
        Me.txtSpouseName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSpouseName.isCalculatedField = False
        Me.txtSpouseName.IsSourceFromTable = False
        Me.txtSpouseName.IsSourceFromValueList = False
        Me.txtSpouseName.IsUnique = False
        Me.txtSpouseName.Location = New System.Drawing.Point(104, 102)
        Me.txtSpouseName.MaxLength = 49
        Me.txtSpouseName.MendatroryField = True
        Me.txtSpouseName.MyLinkLable1 = Me.MyLabel75
        Me.txtSpouseName.MyLinkLable2 = Nothing
        Me.txtSpouseName.Name = "txtSpouseName"
        Me.txtSpouseName.ReferenceFieldDesc = Nothing
        Me.txtSpouseName.ReferenceFieldName = Nothing
        Me.txtSpouseName.ReferenceTableName = Nothing
        Me.txtSpouseName.Size = New System.Drawing.Size(216, 18)
        Me.txtSpouseName.TabIndex = 13
        '
        'txtDivision
        '
        Me.txtDivision.CalculationExpression = Nothing
        Me.txtDivision.FieldCode = Nothing
        Me.txtDivision.FieldDesc = Nothing
        Me.txtDivision.FieldMaxLength = 0
        Me.txtDivision.FieldName = Nothing
        Me.txtDivision.isCalculatedField = False
        Me.txtDivision.IsSourceFromTable = False
        Me.txtDivision.IsSourceFromValueList = False
        Me.txtDivision.IsUnique = False
        Me.txtDivision.Location = New System.Drawing.Point(104, 227)
        Me.txtDivision.MendatroryField = False
        Me.txtDivision.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDivision.MyLinkLable1 = Me.MyLabel6
        Me.txtDivision.MyLinkLable2 = Nothing
        Me.txtDivision.MyReadOnly = False
        Me.txtDivision.MyShowMasterFormButton = False
        Me.txtDivision.Name = "txtDivision"
        Me.txtDivision.ReferenceFieldDesc = Nothing
        Me.txtDivision.ReferenceFieldName = Nothing
        Me.txtDivision.ReferenceTableName = Nothing
        Me.txtDivision.Size = New System.Drawing.Size(216, 19)
        Me.txtDivision.TabIndex = 25
        Me.txtDivision.Value = ""
        '
        'MyLabel74
        '
        Me.MyLabel74.FieldName = Nothing
        Me.MyLabel74.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel74.Location = New System.Drawing.Point(3, 63)
        Me.MyLabel74.Name = "MyLabel74"
        Me.MyLabel74.Size = New System.Drawing.Size(83, 16)
        Me.MyLabel74.TabIndex = 120
        Me.MyLabel74.Text = "Mother's Name"
        '
        'MyLabel37
        '
        Me.MyLabel37.FieldName = Nothing
        Me.MyLabel37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel37.Location = New System.Drawing.Point(326, 103)
        Me.MyLabel37.Name = "MyLabel37"
        Me.MyLabel37.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel37.TabIndex = 109
        Me.MyLabel37.Text = "Payroll Code"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(3, 249)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(38, 16)
        Me.MyLabel8.TabIndex = 82
        Me.MyLabel8.Text = "Grade"
        '
        'txtPayRollCode
        '
        Me.txtPayRollCode.CalculationExpression = Nothing
        Me.txtPayRollCode.FieldCode = Nothing
        Me.txtPayRollCode.FieldDesc = Nothing
        Me.txtPayRollCode.FieldMaxLength = 0
        Me.txtPayRollCode.FieldName = Nothing
        Me.txtPayRollCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayRollCode.isCalculatedField = False
        Me.txtPayRollCode.IsSourceFromTable = False
        Me.txtPayRollCode.IsSourceFromValueList = False
        Me.txtPayRollCode.IsUnique = False
        Me.txtPayRollCode.Location = New System.Drawing.Point(435, 102)
        Me.txtPayRollCode.MaxLength = 49
        Me.txtPayRollCode.MendatroryField = False
        Me.txtPayRollCode.MyLinkLable1 = Me.MyLabel37
        Me.txtPayRollCode.MyLinkLable2 = Nothing
        Me.txtPayRollCode.Name = "txtPayRollCode"
        Me.txtPayRollCode.ReferenceFieldDesc = Nothing
        Me.txtPayRollCode.ReferenceFieldName = Nothing
        Me.txtPayRollCode.ReferenceTableName = Nothing
        Me.txtPayRollCode.Size = New System.Drawing.Size(228, 18)
        Me.txtPayRollCode.TabIndex = 14
        '
        'txtMothersName
        '
        Me.txtMothersName.CalculationExpression = Nothing
        Me.txtMothersName.FieldCode = Nothing
        Me.txtMothersName.FieldDesc = Nothing
        Me.txtMothersName.FieldMaxLength = 0
        Me.txtMothersName.FieldName = Nothing
        Me.txtMothersName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMothersName.isCalculatedField = False
        Me.txtMothersName.IsSourceFromTable = False
        Me.txtMothersName.IsSourceFromValueList = False
        Me.txtMothersName.IsUnique = False
        Me.txtMothersName.Location = New System.Drawing.Point(104, 62)
        Me.txtMothersName.MaxLength = 49
        Me.txtMothersName.MendatroryField = False
        Me.txtMothersName.MyLinkLable1 = Me.MyLabel74
        Me.txtMothersName.MyLinkLable2 = Nothing
        Me.txtMothersName.Name = "txtMothersName"
        Me.txtMothersName.ReferenceFieldDesc = Nothing
        Me.txtMothersName.ReferenceFieldName = Nothing
        Me.txtMothersName.ReferenceTableName = Nothing
        Me.txtMothersName.Size = New System.Drawing.Size(216, 18)
        Me.txtMothersName.TabIndex = 9
        '
        'txtGrade
        '
        Me.txtGrade.CalculationExpression = Nothing
        Me.txtGrade.FieldCode = Nothing
        Me.txtGrade.FieldDesc = Nothing
        Me.txtGrade.FieldMaxLength = 0
        Me.txtGrade.FieldName = Nothing
        Me.txtGrade.isCalculatedField = False
        Me.txtGrade.IsSourceFromTable = False
        Me.txtGrade.IsSourceFromValueList = False
        Me.txtGrade.IsUnique = False
        Me.txtGrade.Location = New System.Drawing.Point(104, 248)
        Me.txtGrade.MendatroryField = False
        Me.txtGrade.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGrade.MyLinkLable1 = Me.MyLabel8
        Me.txtGrade.MyLinkLable2 = Nothing
        Me.txtGrade.MyReadOnly = False
        Me.txtGrade.MyShowMasterFormButton = False
        Me.txtGrade.Name = "txtGrade"
        Me.txtGrade.ReferenceFieldDesc = Nothing
        Me.txtGrade.ReferenceFieldName = Nothing
        Me.txtGrade.ReferenceTableName = Nothing
        Me.txtGrade.Size = New System.Drawing.Size(216, 19)
        Me.txtGrade.TabIndex = 27
        Me.txtGrade.Value = ""
        '
        'MyLabel73
        '
        Me.MyLabel73.FieldName = Nothing
        Me.MyLabel73.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel73.Location = New System.Drawing.Point(3, 43)
        Me.MyLabel73.Name = "MyLabel73"
        Me.MyLabel73.Size = New System.Drawing.Size(80, 16)
        Me.MyLabel73.TabIndex = 118
        Me.MyLabel73.Text = "Father's Name"
        '
        'MyLabel51
        '
        Me.MyLabel51.FieldName = Nothing
        Me.MyLabel51.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel51.Location = New System.Drawing.Point(3, 123)
        Me.MyLabel51.Name = "MyLabel51"
        Me.MyLabel51.Size = New System.Drawing.Size(29, 16)
        Me.MyLabel51.TabIndex = 105
        Me.MyLabel51.Text = "Shift"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(3, 312)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel9.TabIndex = 84
        Me.MyLabel9.Text = "Location"
        '
        'txtReligion
        '
        Me.txtReligion.CalculationExpression = Nothing
        Me.txtReligion.FieldCode = Nothing
        Me.txtReligion.FieldDesc = Nothing
        Me.txtReligion.FieldMaxLength = 0
        Me.txtReligion.FieldName = Nothing
        Me.txtReligion.isCalculatedField = False
        Me.txtReligion.IsSourceFromTable = False
        Me.txtReligion.IsSourceFromValueList = False
        Me.txtReligion.IsUnique = False
        Me.txtReligion.Location = New System.Drawing.Point(435, 163)
        Me.txtReligion.MendatroryField = False
        Me.txtReligion.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReligion.MyLinkLable1 = Me.MyLabel1
        Me.txtReligion.MyLinkLable2 = Nothing
        Me.txtReligion.MyReadOnly = False
        Me.txtReligion.MyShowMasterFormButton = False
        Me.txtReligion.Name = "txtReligion"
        Me.txtReligion.ReferenceFieldDesc = Nothing
        Me.txtReligion.ReferenceFieldName = Nothing
        Me.txtReligion.ReferenceTableName = Nothing
        Me.txtReligion.Size = New System.Drawing.Size(228, 20)
        Me.txtReligion.TabIndex = 20
        Me.txtReligion.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(326, 165)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(80, 16)
        Me.MyLabel1.TabIndex = 102
        Me.MyLabel1.Text = "Religion Name"
        '
        'TxtFathersName
        '
        Me.TxtFathersName.CalculationExpression = Nothing
        Me.TxtFathersName.FieldCode = Nothing
        Me.TxtFathersName.FieldDesc = Nothing
        Me.TxtFathersName.FieldMaxLength = 0
        Me.TxtFathersName.FieldName = Nothing
        Me.TxtFathersName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFathersName.isCalculatedField = False
        Me.TxtFathersName.IsSourceFromTable = False
        Me.TxtFathersName.IsSourceFromValueList = False
        Me.TxtFathersName.IsUnique = False
        Me.TxtFathersName.Location = New System.Drawing.Point(104, 42)
        Me.TxtFathersName.MaxLength = 49
        Me.TxtFathersName.MendatroryField = False
        Me.TxtFathersName.MyLinkLable1 = Me.MyLabel73
        Me.TxtFathersName.MyLinkLable2 = Nothing
        Me.TxtFathersName.Name = "TxtFathersName"
        Me.TxtFathersName.ReferenceFieldDesc = Nothing
        Me.TxtFathersName.ReferenceFieldName = Nothing
        Me.TxtFathersName.ReferenceTableName = Nothing
        Me.TxtFathersName.Size = New System.Drawing.Size(216, 18)
        Me.TxtFathersName.TabIndex = 6
        '
        'txtBranch
        '
        Me.txtBranch.CalculationExpression = Nothing
        Me.txtBranch.FieldCode = Nothing
        Me.txtBranch.FieldDesc = Nothing
        Me.txtBranch.FieldMaxLength = 0
        Me.txtBranch.FieldName = Nothing
        Me.txtBranch.isCalculatedField = False
        Me.txtBranch.IsSourceFromTable = False
        Me.txtBranch.IsSourceFromValueList = False
        Me.txtBranch.IsUnique = False
        Me.txtBranch.Location = New System.Drawing.Point(104, 311)
        Me.txtBranch.MendatroryField = True
        Me.txtBranch.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBranch.MyLinkLable1 = Me.MyLabel9
        Me.txtBranch.MyLinkLable2 = Nothing
        Me.txtBranch.MyReadOnly = False
        Me.txtBranch.MyShowMasterFormButton = False
        Me.txtBranch.Name = "txtBranch"
        Me.txtBranch.ReferenceFieldDesc = Nothing
        Me.txtBranch.ReferenceFieldName = Nothing
        Me.txtBranch.ReferenceTableName = Nothing
        Me.txtBranch.Size = New System.Drawing.Size(216, 19)
        Me.txtBranch.TabIndex = 31
        Me.txtBranch.Value = ""
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(3, 333)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel10.TabIndex = 86
        Me.MyLabel10.Text = "Attendance"
        '
        'txtAttendance
        '
        Me.txtAttendance.CalculationExpression = Nothing
        Me.txtAttendance.FieldCode = Nothing
        Me.txtAttendance.FieldDesc = Nothing
        Me.txtAttendance.FieldMaxLength = 0
        Me.txtAttendance.FieldName = Nothing
        Me.txtAttendance.isCalculatedField = False
        Me.txtAttendance.IsSourceFromTable = False
        Me.txtAttendance.IsSourceFromValueList = False
        Me.txtAttendance.IsUnique = False
        Me.txtAttendance.Location = New System.Drawing.Point(104, 332)
        Me.txtAttendance.MendatroryField = True
        Me.txtAttendance.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAttendance.MyLinkLable1 = Me.MyLabel10
        Me.txtAttendance.MyLinkLable2 = Nothing
        Me.txtAttendance.MyReadOnly = False
        Me.txtAttendance.MyShowMasterFormButton = False
        Me.txtAttendance.Name = "txtAttendance"
        Me.txtAttendance.ReferenceFieldDesc = Nothing
        Me.txtAttendance.ReferenceFieldName = Nothing
        Me.txtAttendance.ReferenceTableName = Nothing
        Me.txtAttendance.Size = New System.Drawing.Size(216, 19)
        Me.txtAttendance.TabIndex = 33
        Me.txtAttendance.Value = ""
        '
        'txtProbationEndDate
        '
        Me.txtProbationEndDate.CalculationExpression = Nothing
        Me.txtProbationEndDate.CustomFormat = "dd/MM/yyyy"
        Me.txtProbationEndDate.FieldCode = Nothing
        Me.txtProbationEndDate.FieldDesc = Nothing
        Me.txtProbationEndDate.FieldMaxLength = 0
        Me.txtProbationEndDate.FieldName = Nothing
        Me.txtProbationEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProbationEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtProbationEndDate.isCalculatedField = False
        Me.txtProbationEndDate.IsSourceFromTable = False
        Me.txtProbationEndDate.IsSourceFromValueList = False
        Me.txtProbationEndDate.IsUnique = False
        Me.txtProbationEndDate.Location = New System.Drawing.Point(435, 22)
        Me.txtProbationEndDate.MendatroryField = False
        Me.txtProbationEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtProbationEndDate.MyLinkLable1 = Me.MyLabel16
        Me.txtProbationEndDate.MyLinkLable2 = Nothing
        Me.txtProbationEndDate.Name = "txtProbationEndDate"
        Me.txtProbationEndDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtProbationEndDate.ReferenceFieldDesc = Nothing
        Me.txtProbationEndDate.ReferenceFieldName = Nothing
        Me.txtProbationEndDate.ReferenceTableName = Nothing
        Me.txtProbationEndDate.ShowCheckBox = True
        Me.txtProbationEndDate.Size = New System.Drawing.Size(90, 18)
        Me.txtProbationEndDate.TabIndex = 4
        Me.txtProbationEndDate.TabStop = False
        Me.txtProbationEndDate.Text = "03/05/2011"
        Me.txtProbationEndDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(326, 23)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(105, 16)
        Me.MyLabel16.TabIndex = 99
        Me.MyLabel16.Text = "Probation End Date"
        '
        'MyLabel69
        '
        Me.MyLabel69.FieldName = Nothing
        Me.MyLabel69.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel69.Location = New System.Drawing.Point(3, 291)
        Me.MyLabel69.Name = "MyLabel69"
        Me.MyLabel69.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel69.TabIndex = 115
        Me.MyLabel69.Text = "Company Code"
        '
        'txtConfirmationDate
        '
        Me.txtConfirmationDate.CalculationExpression = Nothing
        Me.txtConfirmationDate.CustomFormat = "dd/MM/yyyy"
        Me.txtConfirmationDate.FieldCode = Nothing
        Me.txtConfirmationDate.FieldDesc = Nothing
        Me.txtConfirmationDate.FieldMaxLength = 0
        Me.txtConfirmationDate.FieldName = Nothing
        Me.txtConfirmationDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConfirmationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtConfirmationDate.isCalculatedField = False
        Me.txtConfirmationDate.IsSourceFromTable = False
        Me.txtConfirmationDate.IsSourceFromValueList = False
        Me.txtConfirmationDate.IsUnique = False
        Me.txtConfirmationDate.Location = New System.Drawing.Point(435, 2)
        Me.txtConfirmationDate.MendatroryField = False
        Me.txtConfirmationDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtConfirmationDate.MyLinkLable1 = Me.MyLabel15
        Me.txtConfirmationDate.MyLinkLable2 = Nothing
        Me.txtConfirmationDate.Name = "txtConfirmationDate"
        Me.txtConfirmationDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtConfirmationDate.ReferenceFieldDesc = Nothing
        Me.txtConfirmationDate.ReferenceFieldName = Nothing
        Me.txtConfirmationDate.ReferenceTableName = Nothing
        Me.txtConfirmationDate.ShowCheckBox = True
        Me.txtConfirmationDate.Size = New System.Drawing.Size(90, 18)
        Me.txtConfirmationDate.TabIndex = 1
        Me.txtConfirmationDate.TabStop = False
        Me.txtConfirmationDate.Text = "03/05/2011"
        Me.txtConfirmationDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(326, 3)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(97, 16)
        Me.MyLabel15.TabIndex = 97
        Me.MyLabel15.Text = "Confirmation Date"
        '
        'txtCastCategory
        '
        Me.txtCastCategory.CalculationExpression = Nothing
        Me.txtCastCategory.FieldCode = Nothing
        Me.txtCastCategory.FieldDesc = Nothing
        Me.txtCastCategory.FieldMaxLength = 0
        Me.txtCastCategory.FieldName = Nothing
        Me.txtCastCategory.isCalculatedField = False
        Me.txtCastCategory.IsSourceFromTable = False
        Me.txtCastCategory.IsSourceFromValueList = False
        Me.txtCastCategory.IsUnique = False
        Me.txtCastCategory.Location = New System.Drawing.Point(435, 142)
        Me.txtCastCategory.MendatroryField = False
        Me.txtCastCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCastCategory.MyLinkLable1 = Me.MyLabel14
        Me.txtCastCategory.MyLinkLable2 = Nothing
        Me.txtCastCategory.MyReadOnly = False
        Me.txtCastCategory.MyShowMasterFormButton = False
        Me.txtCastCategory.Name = "txtCastCategory"
        Me.txtCastCategory.ReferenceFieldDesc = Nothing
        Me.txtCastCategory.ReferenceFieldName = Nothing
        Me.txtCastCategory.ReferenceTableName = Nothing
        Me.txtCastCategory.Size = New System.Drawing.Size(228, 20)
        Me.txtCastCategory.TabIndex = 18
        Me.txtCastCategory.Value = ""
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(326, 144)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel14.TabIndex = 94
        Me.MyLabel14.Text = "Cast Category"
        '
        'txtCompanyCode
        '
        Me.txtCompanyCode.CalculationExpression = Nothing
        Me.txtCompanyCode.FieldCode = Nothing
        Me.txtCompanyCode.FieldDesc = Nothing
        Me.txtCompanyCode.FieldMaxLength = 0
        Me.txtCompanyCode.FieldName = Nothing
        Me.txtCompanyCode.isCalculatedField = False
        Me.txtCompanyCode.IsSourceFromTable = False
        Me.txtCompanyCode.IsSourceFromValueList = False
        Me.txtCompanyCode.IsUnique = False
        Me.txtCompanyCode.Location = New System.Drawing.Point(104, 290)
        Me.txtCompanyCode.MendatroryField = False
        Me.txtCompanyCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyCode.MyLinkLable1 = Me.MyLabel69
        Me.txtCompanyCode.MyLinkLable2 = Nothing
        Me.txtCompanyCode.MyReadOnly = False
        Me.txtCompanyCode.MyShowMasterFormButton = False
        Me.txtCompanyCode.Name = "txtCompanyCode"
        Me.txtCompanyCode.ReferenceFieldDesc = Nothing
        Me.txtCompanyCode.ReferenceFieldName = Nothing
        Me.txtCompanyCode.ReferenceTableName = Nothing
        Me.txtCompanyCode.Size = New System.Drawing.Size(216, 19)
        Me.txtCompanyCode.TabIndex = 29
        Me.txtCompanyCode.Value = ""
        '
        'CboEmployeeStatus
        '
        Me.CboEmployeeStatus.AutoCompleteDisplayMember = Nothing
        Me.CboEmployeeStatus.AutoCompleteValueMember = Nothing
        Me.CboEmployeeStatus.CalculationExpression = Nothing
        Me.CboEmployeeStatus.DropDownAnimationEnabled = True
        Me.CboEmployeeStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboEmployeeStatus.FieldCode = Nothing
        Me.CboEmployeeStatus.FieldDesc = Nothing
        Me.CboEmployeeStatus.FieldMaxLength = 0
        Me.CboEmployeeStatus.FieldName = Nothing
        Me.CboEmployeeStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboEmployeeStatus.isCalculatedField = False
        Me.CboEmployeeStatus.IsSourceFromTable = False
        Me.CboEmployeeStatus.IsSourceFromValueList = False
        Me.CboEmployeeStatus.IsUnique = False
        Me.CboEmployeeStatus.Location = New System.Drawing.Point(435, 82)
        Me.CboEmployeeStatus.MendatroryField = False
        Me.CboEmployeeStatus.MyLinkLable1 = Me.lblstatus
        Me.CboEmployeeStatus.MyLinkLable2 = Nothing
        Me.CboEmployeeStatus.Name = "CboEmployeeStatus"
        Me.CboEmployeeStatus.ReferenceFieldDesc = Nothing
        Me.CboEmployeeStatus.ReferenceFieldName = Nothing
        Me.CboEmployeeStatus.ReferenceTableName = Nothing
        Me.CboEmployeeStatus.Size = New System.Drawing.Size(228, 18)
        Me.CboEmployeeStatus.TabIndex = 12
        '
        'lblstatus
        '
        Me.lblstatus.FieldName = Nothing
        Me.lblstatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstatus.Location = New System.Drawing.Point(326, 83)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(92, 16)
        Me.lblstatus.TabIndex = 57
        Me.lblstatus.Text = "Employee Status"
        '
        'Contact
        '
        Me.Contact.Controls.Add(Me.RadGroupBox4)
        Me.Contact.Controls.Add(Me.RadGroupBox5)
        Me.Contact.Controls.Add(Me.RadGroupBox3)
        Me.Contact.ItemSize = New System.Drawing.SizeF(92.0!, 28.0!)
        Me.Contact.Location = New System.Drawing.Point(10, 37)
        Me.Contact.Name = "Contact"
        Me.Contact.Size = New System.Drawing.Size(845, 412)
        Me.Contact.Text = "Contact Details"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.txtAdd1_Verfi_Remarks)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel104)
        Me.RadGroupBox4.Controls.Add(Me.chkAdd1_Verified)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel103)
        Me.RadGroupBox4.Controls.Add(Me.cboAdd1_Type)
        Me.RadGroupBox4.Controls.Add(Me.txtAdd1_PoliceStation)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel102)
        Me.RadGroupBox4.Controls.Add(Me.txtAdd1_PostOffice)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel101)
        Me.RadGroupBox4.Controls.Add(Me.txtAdd1_Village)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel100)
        Me.RadGroupBox4.Controls.Add(Me.txtAdd1_Tehsil)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel99)
        Me.RadGroupBox4.Controls.Add(Me.txtPermCountry)
        Me.RadGroupBox4.Controls.Add(Me.txtPermMobileNo)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel26)
        Me.RadGroupBox4.Controls.Add(Me.txtPermPhoneNo)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel27)
        Me.RadGroupBox4.Controls.Add(Me.txtPermPostalCode)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel25)
        Me.RadGroupBox4.Controls.Add(Me.chkSame)
        Me.RadGroupBox4.Controls.Add(Me.txtPermCity)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel28)
        Me.RadGroupBox4.Controls.Add(Me.txtPermState)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel29)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel30)
        Me.RadGroupBox4.Controls.Add(Me.txtPermAddress)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel31)
        Me.RadGroupBox4.HeaderText = "Permanent Address"
        Me.RadGroupBox4.Location = New System.Drawing.Point(6, 12)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(336, 289)
        Me.RadGroupBox4.TabIndex = 0
        Me.RadGroupBox4.Text = "Permanent Address"
        '
        'txtAdd1_Verfi_Remarks
        '
        Me.txtAdd1_Verfi_Remarks.CalculationExpression = Nothing
        Me.txtAdd1_Verfi_Remarks.FieldCode = Nothing
        Me.txtAdd1_Verfi_Remarks.FieldDesc = Nothing
        Me.txtAdd1_Verfi_Remarks.FieldMaxLength = 0
        Me.txtAdd1_Verfi_Remarks.FieldName = Nothing
        Me.txtAdd1_Verfi_Remarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd1_Verfi_Remarks.isCalculatedField = False
        Me.txtAdd1_Verfi_Remarks.IsSourceFromTable = False
        Me.txtAdd1_Verfi_Remarks.IsSourceFromValueList = False
        Me.txtAdd1_Verfi_Remarks.IsUnique = False
        Me.txtAdd1_Verfi_Remarks.Location = New System.Drawing.Point(115, 266)
        Me.txtAdd1_Verfi_Remarks.MaxLength = 100
        Me.txtAdd1_Verfi_Remarks.MendatroryField = False
        Me.txtAdd1_Verfi_Remarks.MyLinkLable1 = Me.MyLabel104
        Me.txtAdd1_Verfi_Remarks.MyLinkLable2 = Nothing
        Me.txtAdd1_Verfi_Remarks.Name = "txtAdd1_Verfi_Remarks"
        Me.txtAdd1_Verfi_Remarks.ReferenceFieldDesc = Nothing
        Me.txtAdd1_Verfi_Remarks.ReferenceFieldName = Nothing
        Me.txtAdd1_Verfi_Remarks.ReferenceTableName = Nothing
        Me.txtAdd1_Verfi_Remarks.Size = New System.Drawing.Size(208, 18)
        Me.txtAdd1_Verfi_Remarks.TabIndex = 13
        '
        'MyLabel104
        '
        Me.MyLabel104.FieldName = Nothing
        Me.MyLabel104.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel104.Location = New System.Drawing.Point(9, 267)
        Me.MyLabel104.Name = "MyLabel104"
        Me.MyLabel104.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel104.TabIndex = 144
        Me.MyLabel104.Text = "Verifi. Remarks"
        '
        'chkAdd1_Verified
        '
        Me.chkAdd1_Verified.Location = New System.Drawing.Point(115, 248)
        Me.chkAdd1_Verified.Name = "chkAdd1_Verified"
        Me.chkAdd1_Verified.Size = New System.Drawing.Size(102, 18)
        Me.chkAdd1_Verified.TabIndex = 12
        Me.chkAdd1_Verified.Text = "Address Verified"
        '
        'MyLabel103
        '
        Me.MyLabel103.FieldName = Nothing
        Me.MyLabel103.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel103.Location = New System.Drawing.Point(10, 231)
        Me.MyLabel103.Name = "MyLabel103"
        Me.MyLabel103.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel103.TabIndex = 141
        Me.MyLabel103.Text = "Type"
        '
        'cboAdd1_Type
        '
        Me.cboAdd1_Type.AutoCompleteDisplayMember = Nothing
        Me.cboAdd1_Type.AutoCompleteValueMember = Nothing
        Me.cboAdd1_Type.CalculationExpression = Nothing
        Me.cboAdd1_Type.DropDownAnimationEnabled = True
        Me.cboAdd1_Type.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboAdd1_Type.FieldCode = Nothing
        Me.cboAdd1_Type.FieldDesc = Nothing
        Me.cboAdd1_Type.FieldMaxLength = 0
        Me.cboAdd1_Type.FieldName = Nothing
        Me.cboAdd1_Type.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAdd1_Type.isCalculatedField = False
        Me.cboAdd1_Type.IsSourceFromTable = False
        Me.cboAdd1_Type.IsSourceFromValueList = False
        Me.cboAdd1_Type.IsUnique = False
        Me.cboAdd1_Type.Location = New System.Drawing.Point(115, 230)
        Me.cboAdd1_Type.MendatroryField = False
        Me.cboAdd1_Type.MyLinkLable1 = Me.MyLabel103
        Me.cboAdd1_Type.MyLinkLable2 = Nothing
        Me.cboAdd1_Type.Name = "cboAdd1_Type"
        Me.cboAdd1_Type.ReferenceFieldDesc = Nothing
        Me.cboAdd1_Type.ReferenceFieldName = Nothing
        Me.cboAdd1_Type.ReferenceTableName = Nothing
        Me.cboAdd1_Type.Size = New System.Drawing.Size(208, 18)
        Me.cboAdd1_Type.TabIndex = 11
        '
        'txtAdd1_PoliceStation
        '
        Me.txtAdd1_PoliceStation.CalculationExpression = Nothing
        Me.txtAdd1_PoliceStation.FieldCode = Nothing
        Me.txtAdd1_PoliceStation.FieldDesc = Nothing
        Me.txtAdd1_PoliceStation.FieldMaxLength = 0
        Me.txtAdd1_PoliceStation.FieldName = Nothing
        Me.txtAdd1_PoliceStation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd1_PoliceStation.isCalculatedField = False
        Me.txtAdd1_PoliceStation.IsSourceFromTable = False
        Me.txtAdd1_PoliceStation.IsSourceFromValueList = False
        Me.txtAdd1_PoliceStation.IsUnique = False
        Me.txtAdd1_PoliceStation.Location = New System.Drawing.Point(115, 212)
        Me.txtAdd1_PoliceStation.MaxLength = 100
        Me.txtAdd1_PoliceStation.MendatroryField = False
        Me.txtAdd1_PoliceStation.MyLinkLable1 = Me.MyLabel102
        Me.txtAdd1_PoliceStation.MyLinkLable2 = Nothing
        Me.txtAdd1_PoliceStation.Name = "txtAdd1_PoliceStation"
        Me.txtAdd1_PoliceStation.ReferenceFieldDesc = Nothing
        Me.txtAdd1_PoliceStation.ReferenceFieldName = Nothing
        Me.txtAdd1_PoliceStation.ReferenceTableName = Nothing
        Me.txtAdd1_PoliceStation.Size = New System.Drawing.Size(208, 18)
        Me.txtAdd1_PoliceStation.TabIndex = 10
        '
        'MyLabel102
        '
        Me.MyLabel102.FieldName = Nothing
        Me.MyLabel102.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel102.Location = New System.Drawing.Point(10, 213)
        Me.MyLabel102.Name = "MyLabel102"
        Me.MyLabel102.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel102.TabIndex = 139
        Me.MyLabel102.Text = "Police Station"
        '
        'txtAdd1_PostOffice
        '
        Me.txtAdd1_PostOffice.CalculationExpression = Nothing
        Me.txtAdd1_PostOffice.FieldCode = Nothing
        Me.txtAdd1_PostOffice.FieldDesc = Nothing
        Me.txtAdd1_PostOffice.FieldMaxLength = 0
        Me.txtAdd1_PostOffice.FieldName = Nothing
        Me.txtAdd1_PostOffice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd1_PostOffice.isCalculatedField = False
        Me.txtAdd1_PostOffice.IsSourceFromTable = False
        Me.txtAdd1_PostOffice.IsSourceFromValueList = False
        Me.txtAdd1_PostOffice.IsUnique = False
        Me.txtAdd1_PostOffice.Location = New System.Drawing.Point(116, 194)
        Me.txtAdd1_PostOffice.MaxLength = 100
        Me.txtAdd1_PostOffice.MendatroryField = False
        Me.txtAdd1_PostOffice.MyLinkLable1 = Me.MyLabel101
        Me.txtAdd1_PostOffice.MyLinkLable2 = Nothing
        Me.txtAdd1_PostOffice.Name = "txtAdd1_PostOffice"
        Me.txtAdd1_PostOffice.ReferenceFieldDesc = Nothing
        Me.txtAdd1_PostOffice.ReferenceFieldName = Nothing
        Me.txtAdd1_PostOffice.ReferenceTableName = Nothing
        Me.txtAdd1_PostOffice.Size = New System.Drawing.Size(208, 18)
        Me.txtAdd1_PostOffice.TabIndex = 9
        '
        'MyLabel101
        '
        Me.MyLabel101.FieldName = Nothing
        Me.MyLabel101.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel101.Location = New System.Drawing.Point(10, 195)
        Me.MyLabel101.Name = "MyLabel101"
        Me.MyLabel101.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel101.TabIndex = 137
        Me.MyLabel101.Text = "Post Office"
        '
        'txtAdd1_Village
        '
        Me.txtAdd1_Village.CalculationExpression = Nothing
        Me.txtAdd1_Village.FieldCode = Nothing
        Me.txtAdd1_Village.FieldDesc = Nothing
        Me.txtAdd1_Village.FieldMaxLength = 0
        Me.txtAdd1_Village.FieldName = Nothing
        Me.txtAdd1_Village.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd1_Village.isCalculatedField = False
        Me.txtAdd1_Village.IsSourceFromTable = False
        Me.txtAdd1_Village.IsSourceFromValueList = False
        Me.txtAdd1_Village.IsUnique = False
        Me.txtAdd1_Village.Location = New System.Drawing.Point(116, 176)
        Me.txtAdd1_Village.MaxLength = 100
        Me.txtAdd1_Village.MendatroryField = False
        Me.txtAdd1_Village.MyLinkLable1 = Me.MyLabel100
        Me.txtAdd1_Village.MyLinkLable2 = Nothing
        Me.txtAdd1_Village.Name = "txtAdd1_Village"
        Me.txtAdd1_Village.ReferenceFieldDesc = Nothing
        Me.txtAdd1_Village.ReferenceFieldName = Nothing
        Me.txtAdd1_Village.ReferenceTableName = Nothing
        Me.txtAdd1_Village.Size = New System.Drawing.Size(208, 18)
        Me.txtAdd1_Village.TabIndex = 8
        '
        'MyLabel100
        '
        Me.MyLabel100.FieldName = Nothing
        Me.MyLabel100.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel100.Location = New System.Drawing.Point(10, 177)
        Me.MyLabel100.Name = "MyLabel100"
        Me.MyLabel100.Size = New System.Drawing.Size(40, 16)
        Me.MyLabel100.TabIndex = 135
        Me.MyLabel100.Text = "Village"
        '
        'txtAdd1_Tehsil
        '
        Me.txtAdd1_Tehsil.CalculationExpression = Nothing
        Me.txtAdd1_Tehsil.FieldCode = Nothing
        Me.txtAdd1_Tehsil.FieldDesc = Nothing
        Me.txtAdd1_Tehsil.FieldMaxLength = 0
        Me.txtAdd1_Tehsil.FieldName = Nothing
        Me.txtAdd1_Tehsil.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd1_Tehsil.isCalculatedField = False
        Me.txtAdd1_Tehsil.IsSourceFromTable = False
        Me.txtAdd1_Tehsil.IsSourceFromValueList = False
        Me.txtAdd1_Tehsil.IsUnique = False
        Me.txtAdd1_Tehsil.Location = New System.Drawing.Point(116, 158)
        Me.txtAdd1_Tehsil.MaxLength = 100
        Me.txtAdd1_Tehsil.MendatroryField = False
        Me.txtAdd1_Tehsil.MyLinkLable1 = Me.MyLabel99
        Me.txtAdd1_Tehsil.MyLinkLable2 = Nothing
        Me.txtAdd1_Tehsil.Name = "txtAdd1_Tehsil"
        Me.txtAdd1_Tehsil.ReferenceFieldDesc = Nothing
        Me.txtAdd1_Tehsil.ReferenceFieldName = Nothing
        Me.txtAdd1_Tehsil.ReferenceTableName = Nothing
        Me.txtAdd1_Tehsil.Size = New System.Drawing.Size(208, 18)
        Me.txtAdd1_Tehsil.TabIndex = 7
        '
        'MyLabel99
        '
        Me.MyLabel99.FieldName = Nothing
        Me.MyLabel99.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel99.Location = New System.Drawing.Point(10, 159)
        Me.MyLabel99.Name = "MyLabel99"
        Me.MyLabel99.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel99.TabIndex = 133
        Me.MyLabel99.Text = "Tehsil"
        '
        'txtPermCountry
        '
        Me.txtPermCountry.CalculationExpression = Nothing
        Me.txtPermCountry.FieldCode = Nothing
        Me.txtPermCountry.FieldDesc = Nothing
        Me.txtPermCountry.FieldMaxLength = 0
        Me.txtPermCountry.FieldName = Nothing
        Me.txtPermCountry.isCalculatedField = False
        Me.txtPermCountry.IsSourceFromTable = False
        Me.txtPermCountry.IsSourceFromValueList = False
        Me.txtPermCountry.IsUnique = False
        Me.txtPermCountry.Location = New System.Drawing.Point(116, 47)
        Me.txtPermCountry.MendatroryField = False
        Me.txtPermCountry.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPermCountry.MyLinkLable1 = Me.MyLabel30
        Me.txtPermCountry.MyLinkLable2 = Nothing
        Me.txtPermCountry.MyReadOnly = False
        Me.txtPermCountry.MyShowMasterFormButton = False
        Me.txtPermCountry.Name = "txtPermCountry"
        Me.txtPermCountry.ReferenceFieldDesc = Nothing
        Me.txtPermCountry.ReferenceFieldName = Nothing
        Me.txtPermCountry.ReferenceTableName = Nothing
        Me.txtPermCountry.Size = New System.Drawing.Size(208, 19)
        Me.txtPermCountry.TabIndex = 1
        Me.txtPermCountry.Value = ""
        '
        'MyLabel30
        '
        Me.MyLabel30.FieldName = Nothing
        Me.MyLabel30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel30.Location = New System.Drawing.Point(9, 48)
        Me.MyLabel30.Name = "MyLabel30"
        Me.MyLabel30.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel30.TabIndex = 121
        Me.MyLabel30.Text = "Country"
        '
        'txtPermMobileNo
        '
        Me.txtPermMobileNo.CalculationExpression = Nothing
        Me.txtPermMobileNo.FieldCode = Nothing
        Me.txtPermMobileNo.FieldDesc = Nothing
        Me.txtPermMobileNo.FieldMaxLength = 0
        Me.txtPermMobileNo.FieldName = Nothing
        Me.txtPermMobileNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPermMobileNo.isCalculatedField = False
        Me.txtPermMobileNo.IsSourceFromTable = False
        Me.txtPermMobileNo.IsSourceFromValueList = False
        Me.txtPermMobileNo.IsUnique = False
        Me.txtPermMobileNo.Location = New System.Drawing.Point(116, 122)
        Me.txtPermMobileNo.MaxLength = 50
        Me.txtPermMobileNo.MendatroryField = False
        Me.txtPermMobileNo.MyLinkLable1 = Me.MyLabel26
        Me.txtPermMobileNo.MyLinkLable2 = Nothing
        Me.txtPermMobileNo.Name = "txtPermMobileNo"
        Me.txtPermMobileNo.ReferenceFieldDesc = Nothing
        Me.txtPermMobileNo.ReferenceFieldName = Nothing
        Me.txtPermMobileNo.ReferenceTableName = Nothing
        Me.txtPermMobileNo.Size = New System.Drawing.Size(208, 18)
        Me.txtPermMobileNo.TabIndex = 5
        '
        'MyLabel26
        '
        Me.MyLabel26.FieldName = Nothing
        Me.MyLabel26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel26.Location = New System.Drawing.Point(10, 123)
        Me.MyLabel26.Name = "MyLabel26"
        Me.MyLabel26.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel26.TabIndex = 129
        Me.MyLabel26.Text = "Mobile No"
        '
        'txtPermPhoneNo
        '
        Me.txtPermPhoneNo.CalculationExpression = Nothing
        Me.txtPermPhoneNo.FieldCode = Nothing
        Me.txtPermPhoneNo.FieldDesc = Nothing
        Me.txtPermPhoneNo.FieldMaxLength = 0
        Me.txtPermPhoneNo.FieldName = Nothing
        Me.txtPermPhoneNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPermPhoneNo.isCalculatedField = False
        Me.txtPermPhoneNo.IsSourceFromTable = False
        Me.txtPermPhoneNo.IsSourceFromValueList = False
        Me.txtPermPhoneNo.IsUnique = False
        Me.txtPermPhoneNo.Location = New System.Drawing.Point(116, 104)
        Me.txtPermPhoneNo.MaxLength = 50
        Me.txtPermPhoneNo.MendatroryField = False
        Me.txtPermPhoneNo.MyLinkLable1 = Me.MyLabel27
        Me.txtPermPhoneNo.MyLinkLable2 = Nothing
        Me.txtPermPhoneNo.Name = "txtPermPhoneNo"
        Me.txtPermPhoneNo.ReferenceFieldDesc = Nothing
        Me.txtPermPhoneNo.ReferenceFieldName = Nothing
        Me.txtPermPhoneNo.ReferenceTableName = Nothing
        Me.txtPermPhoneNo.Size = New System.Drawing.Size(208, 18)
        Me.txtPermPhoneNo.TabIndex = 4
        '
        'MyLabel27
        '
        Me.MyLabel27.FieldName = Nothing
        Me.MyLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel27.Location = New System.Drawing.Point(10, 105)
        Me.MyLabel27.Name = "MyLabel27"
        Me.MyLabel27.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel27.TabIndex = 127
        Me.MyLabel27.Text = "Phone No"
        '
        'txtPermPostalCode
        '
        Me.txtPermPostalCode.CalculationExpression = Nothing
        Me.txtPermPostalCode.FieldCode = Nothing
        Me.txtPermPostalCode.FieldDesc = Nothing
        Me.txtPermPostalCode.FieldMaxLength = 0
        Me.txtPermPostalCode.FieldName = Nothing
        Me.txtPermPostalCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPermPostalCode.isCalculatedField = False
        Me.txtPermPostalCode.IsSourceFromTable = False
        Me.txtPermPostalCode.IsSourceFromValueList = False
        Me.txtPermPostalCode.IsUnique = False
        Me.txtPermPostalCode.Location = New System.Drawing.Point(116, 140)
        Me.txtPermPostalCode.MaxLength = 6
        Me.txtPermPostalCode.MendatroryField = False
        Me.txtPermPostalCode.MyLinkLable1 = Me.MyLabel25
        Me.txtPermPostalCode.MyLinkLable2 = Nothing
        Me.txtPermPostalCode.Name = "txtPermPostalCode"
        Me.txtPermPostalCode.ReferenceFieldDesc = Nothing
        Me.txtPermPostalCode.ReferenceFieldName = Nothing
        Me.txtPermPostalCode.ReferenceTableName = Nothing
        Me.txtPermPostalCode.Size = New System.Drawing.Size(208, 18)
        Me.txtPermPostalCode.TabIndex = 6
        '
        'MyLabel25
        '
        Me.MyLabel25.FieldName = Nothing
        Me.MyLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel25.Location = New System.Drawing.Point(10, 141)
        Me.MyLabel25.Name = "MyLabel25"
        Me.MyLabel25.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel25.TabIndex = 131
        Me.MyLabel25.Text = "Postal Code"
        '
        'chkSame
        '
        Me.chkSame.Location = New System.Drawing.Point(116, 11)
        Me.chkSame.Name = "chkSame"
        Me.chkSame.Size = New System.Drawing.Size(145, 18)
        Me.chkSame.TabIndex = 0
        Me.chkSame.Text = "Same as Present Address"
        '
        'txtPermCity
        '
        Me.txtPermCity.CalculationExpression = Nothing
        Me.txtPermCity.FieldCode = Nothing
        Me.txtPermCity.FieldDesc = Nothing
        Me.txtPermCity.FieldMaxLength = 0
        Me.txtPermCity.FieldName = Nothing
        Me.txtPermCity.isCalculatedField = False
        Me.txtPermCity.IsSourceFromTable = False
        Me.txtPermCity.IsSourceFromValueList = False
        Me.txtPermCity.IsUnique = False
        Me.txtPermCity.Location = New System.Drawing.Point(116, 85)
        Me.txtPermCity.MendatroryField = False
        Me.txtPermCity.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPermCity.MyLinkLable1 = Me.MyLabel28
        Me.txtPermCity.MyLinkLable2 = Nothing
        Me.txtPermCity.MyReadOnly = False
        Me.txtPermCity.MyShowMasterFormButton = False
        Me.txtPermCity.Name = "txtPermCity"
        Me.txtPermCity.ReferenceFieldDesc = Nothing
        Me.txtPermCity.ReferenceFieldName = Nothing
        Me.txtPermCity.ReferenceTableName = Nothing
        Me.txtPermCity.Size = New System.Drawing.Size(208, 19)
        Me.txtPermCity.TabIndex = 3
        Me.txtPermCity.Value = ""
        '
        'MyLabel28
        '
        Me.MyLabel28.FieldName = Nothing
        Me.MyLabel28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel28.Location = New System.Drawing.Point(10, 86)
        Me.MyLabel28.Name = "MyLabel28"
        Me.MyLabel28.Size = New System.Drawing.Size(26, 16)
        Me.MyLabel28.TabIndex = 124
        Me.MyLabel28.Text = "City"
        '
        'txtPermState
        '
        Me.txtPermState.CalculationExpression = Nothing
        Me.txtPermState.FieldCode = Nothing
        Me.txtPermState.FieldDesc = Nothing
        Me.txtPermState.FieldMaxLength = 0
        Me.txtPermState.FieldName = Nothing
        Me.txtPermState.isCalculatedField = False
        Me.txtPermState.IsSourceFromTable = False
        Me.txtPermState.IsSourceFromValueList = False
        Me.txtPermState.IsUnique = False
        Me.txtPermState.Location = New System.Drawing.Point(116, 66)
        Me.txtPermState.MendatroryField = False
        Me.txtPermState.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPermState.MyLinkLable1 = Me.MyLabel29
        Me.txtPermState.MyLinkLable2 = Nothing
        Me.txtPermState.MyReadOnly = False
        Me.txtPermState.MyShowMasterFormButton = False
        Me.txtPermState.Name = "txtPermState"
        Me.txtPermState.ReferenceFieldDesc = Nothing
        Me.txtPermState.ReferenceFieldName = Nothing
        Me.txtPermState.ReferenceTableName = Nothing
        Me.txtPermState.Size = New System.Drawing.Size(208, 19)
        Me.txtPermState.TabIndex = 2
        Me.txtPermState.Value = ""
        '
        'MyLabel29
        '
        Me.MyLabel29.FieldName = Nothing
        Me.MyLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel29.Location = New System.Drawing.Point(9, 67)
        Me.MyLabel29.Name = "MyLabel29"
        Me.MyLabel29.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel29.TabIndex = 122
        Me.MyLabel29.Text = "State"
        '
        'txtPermAddress
        '
        Me.txtPermAddress.CalculationExpression = Nothing
        Me.txtPermAddress.FieldCode = Nothing
        Me.txtPermAddress.FieldDesc = Nothing
        Me.txtPermAddress.FieldMaxLength = 0
        Me.txtPermAddress.FieldName = Nothing
        Me.txtPermAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPermAddress.isCalculatedField = False
        Me.txtPermAddress.IsSourceFromTable = False
        Me.txtPermAddress.IsSourceFromValueList = False
        Me.txtPermAddress.IsUnique = False
        Me.txtPermAddress.Location = New System.Drawing.Point(116, 29)
        Me.txtPermAddress.MaxLength = 49
        Me.txtPermAddress.MendatroryField = False
        Me.txtPermAddress.MyLinkLable1 = Me.MyLabel31
        Me.txtPermAddress.MyLinkLable2 = Nothing
        Me.txtPermAddress.Name = "txtPermAddress"
        Me.txtPermAddress.ReferenceFieldDesc = Nothing
        Me.txtPermAddress.ReferenceFieldName = Nothing
        Me.txtPermAddress.ReferenceTableName = Nothing
        Me.txtPermAddress.Size = New System.Drawing.Size(208, 18)
        Me.txtPermAddress.TabIndex = 0
        '
        'MyLabel31
        '
        Me.MyLabel31.FieldName = Nothing
        Me.MyLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel31.Location = New System.Drawing.Point(10, 30)
        Me.MyLabel31.Name = "MyLabel31"
        Me.MyLabel31.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel31.TabIndex = 119
        Me.MyLabel31.Text = "Address"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.txtDLNo)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel97)
        Me.RadGroupBox5.Controls.Add(Me.txtVoterCard)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel98)
        Me.RadGroupBox5.Controls.Add(Me.txtRationCard)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel95)
        Me.RadGroupBox5.Controls.Add(Me.txtAadharCard)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel96)
        Me.RadGroupBox5.Controls.Add(Me.txtAlternateEmail)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel94)
        Me.RadGroupBox5.Controls.Add(Me.txtRemarks)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel35)
        Me.RadGroupBox5.Controls.Add(Me.txtEmail)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel33)
        Me.RadGroupBox5.Controls.Add(Me.txtPassportNo)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel32)
        Me.RadGroupBox5.Controls.Add(Me.txtPanNo)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel41)
        Me.RadGroupBox5.HeaderText = "Other Details"
        Me.RadGroupBox5.Location = New System.Drawing.Point(5, 300)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(682, 112)
        Me.RadGroupBox5.TabIndex = 2
        Me.RadGroupBox5.Text = "Other Details"
        '
        'txtDLNo
        '
        Me.txtDLNo.CalculationExpression = Nothing
        Me.txtDLNo.FieldCode = Nothing
        Me.txtDLNo.FieldDesc = Nothing
        Me.txtDLNo.FieldMaxLength = 0
        Me.txtDLNo.FieldName = Nothing
        Me.txtDLNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDLNo.isCalculatedField = False
        Me.txtDLNo.IsSourceFromTable = False
        Me.txtDLNo.IsSourceFromValueList = False
        Me.txtDLNo.IsUnique = False
        Me.txtDLNo.Location = New System.Drawing.Point(459, 72)
        Me.txtDLNo.MaxLength = 100
        Me.txtDLNo.MendatroryField = False
        Me.txtDLNo.MyLinkLable1 = Me.MyLabel97
        Me.txtDLNo.MyLinkLable2 = Nothing
        Me.txtDLNo.Name = "txtDLNo"
        Me.txtDLNo.ReferenceFieldDesc = Nothing
        Me.txtDLNo.ReferenceFieldName = Nothing
        Me.txtDLNo.ReferenceTableName = Nothing
        Me.txtDLNo.Size = New System.Drawing.Size(208, 18)
        Me.txtDLNo.TabIndex = 7
        '
        'MyLabel97
        '
        Me.MyLabel97.FieldName = Nothing
        Me.MyLabel97.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel97.Location = New System.Drawing.Point(353, 73)
        Me.MyLabel97.Name = "MyLabel97"
        Me.MyLabel97.Size = New System.Drawing.Size(38, 16)
        Me.MyLabel97.TabIndex = 71
        Me.MyLabel97.Text = "DL No"
        '
        'txtVoterCard
        '
        Me.txtVoterCard.CalculationExpression = Nothing
        Me.txtVoterCard.FieldCode = Nothing
        Me.txtVoterCard.FieldDesc = Nothing
        Me.txtVoterCard.FieldMaxLength = 0
        Me.txtVoterCard.FieldName = Nothing
        Me.txtVoterCard.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVoterCard.isCalculatedField = False
        Me.txtVoterCard.IsSourceFromTable = False
        Me.txtVoterCard.IsSourceFromValueList = False
        Me.txtVoterCard.IsUnique = False
        Me.txtVoterCard.Location = New System.Drawing.Point(116, 72)
        Me.txtVoterCard.MaxLength = 100
        Me.txtVoterCard.MendatroryField = False
        Me.txtVoterCard.MyLinkLable1 = Me.MyLabel98
        Me.txtVoterCard.MyLinkLable2 = Nothing
        Me.txtVoterCard.Name = "txtVoterCard"
        Me.txtVoterCard.ReferenceFieldDesc = Nothing
        Me.txtVoterCard.ReferenceFieldName = Nothing
        Me.txtVoterCard.ReferenceTableName = Nothing
        Me.txtVoterCard.Size = New System.Drawing.Size(208, 18)
        Me.txtVoterCard.TabIndex = 6
        '
        'MyLabel98
        '
        Me.MyLabel98.FieldName = Nothing
        Me.MyLabel98.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel98.Location = New System.Drawing.Point(10, 73)
        Me.MyLabel98.Name = "MyLabel98"
        Me.MyLabel98.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel98.TabIndex = 69
        Me.MyLabel98.Text = "Voter Card"
        '
        'txtRationCard
        '
        Me.txtRationCard.CalculationExpression = Nothing
        Me.txtRationCard.FieldCode = Nothing
        Me.txtRationCard.FieldDesc = Nothing
        Me.txtRationCard.FieldMaxLength = 0
        Me.txtRationCard.FieldName = Nothing
        Me.txtRationCard.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRationCard.isCalculatedField = False
        Me.txtRationCard.IsSourceFromTable = False
        Me.txtRationCard.IsSourceFromValueList = False
        Me.txtRationCard.IsUnique = False
        Me.txtRationCard.Location = New System.Drawing.Point(459, 53)
        Me.txtRationCard.MaxLength = 100
        Me.txtRationCard.MendatroryField = False
        Me.txtRationCard.MyLinkLable1 = Me.MyLabel95
        Me.txtRationCard.MyLinkLable2 = Nothing
        Me.txtRationCard.Name = "txtRationCard"
        Me.txtRationCard.ReferenceFieldDesc = Nothing
        Me.txtRationCard.ReferenceFieldName = Nothing
        Me.txtRationCard.ReferenceTableName = Nothing
        Me.txtRationCard.Size = New System.Drawing.Size(208, 18)
        Me.txtRationCard.TabIndex = 5
        '
        'MyLabel95
        '
        Me.MyLabel95.FieldName = Nothing
        Me.MyLabel95.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel95.Location = New System.Drawing.Point(353, 54)
        Me.MyLabel95.Name = "MyLabel95"
        Me.MyLabel95.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel95.TabIndex = 67
        Me.MyLabel95.Text = "Ration Card"
        '
        'txtAadharCard
        '
        Me.txtAadharCard.CalculationExpression = Nothing
        Me.txtAadharCard.FieldCode = Nothing
        Me.txtAadharCard.FieldDesc = Nothing
        Me.txtAadharCard.FieldMaxLength = 0
        Me.txtAadharCard.FieldName = Nothing
        Me.txtAadharCard.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAadharCard.isCalculatedField = False
        Me.txtAadharCard.IsSourceFromTable = False
        Me.txtAadharCard.IsSourceFromValueList = False
        Me.txtAadharCard.IsUnique = False
        Me.txtAadharCard.Location = New System.Drawing.Point(116, 53)
        Me.txtAadharCard.MaxLength = 100
        Me.txtAadharCard.MendatroryField = False
        Me.txtAadharCard.MyLinkLable1 = Me.MyLabel96
        Me.txtAadharCard.MyLinkLable2 = Nothing
        Me.txtAadharCard.Name = "txtAadharCard"
        Me.txtAadharCard.ReferenceFieldDesc = Nothing
        Me.txtAadharCard.ReferenceFieldName = Nothing
        Me.txtAadharCard.ReferenceTableName = Nothing
        Me.txtAadharCard.Size = New System.Drawing.Size(208, 18)
        Me.txtAadharCard.TabIndex = 4
        '
        'MyLabel96
        '
        Me.MyLabel96.FieldName = Nothing
        Me.MyLabel96.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel96.Location = New System.Drawing.Point(10, 54)
        Me.MyLabel96.Name = "MyLabel96"
        Me.MyLabel96.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel96.TabIndex = 65
        Me.MyLabel96.Text = "Aadhar Card"
        '
        'txtAlternateEmail
        '
        Me.txtAlternateEmail.CalculationExpression = Nothing
        Me.txtAlternateEmail.FieldCode = Nothing
        Me.txtAlternateEmail.FieldDesc = Nothing
        Me.txtAlternateEmail.FieldMaxLength = 0
        Me.txtAlternateEmail.FieldName = Nothing
        Me.txtAlternateEmail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAlternateEmail.isCalculatedField = False
        Me.txtAlternateEmail.IsSourceFromTable = False
        Me.txtAlternateEmail.IsSourceFromValueList = False
        Me.txtAlternateEmail.IsUnique = False
        Me.txtAlternateEmail.Location = New System.Drawing.Point(459, 34)
        Me.txtAlternateEmail.MaxLength = 100
        Me.txtAlternateEmail.MendatroryField = False
        Me.txtAlternateEmail.MyLinkLable1 = Me.MyLabel94
        Me.txtAlternateEmail.MyLinkLable2 = Nothing
        Me.txtAlternateEmail.Name = "txtAlternateEmail"
        Me.txtAlternateEmail.ReferenceFieldDesc = Nothing
        Me.txtAlternateEmail.ReferenceFieldName = Nothing
        Me.txtAlternateEmail.ReferenceTableName = Nothing
        Me.txtAlternateEmail.Size = New System.Drawing.Size(208, 18)
        Me.txtAlternateEmail.TabIndex = 3
        '
        'MyLabel94
        '
        Me.MyLabel94.FieldName = Nothing
        Me.MyLabel94.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel94.Location = New System.Drawing.Point(353, 35)
        Me.MyLabel94.Name = "MyLabel94"
        Me.MyLabel94.Size = New System.Drawing.Size(83, 16)
        Me.MyLabel94.TabIndex = 63
        Me.MyLabel94.Text = "Alternate Email"
        '
        'txtRemarks
        '
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(116, 91)
        Me.txtRemarks.MaxLength = 100
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.MyLabel35
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(551, 18)
        Me.txtRemarks.TabIndex = 8
        '
        'MyLabel35
        '
        Me.MyLabel35.FieldName = Nothing
        Me.MyLabel35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel35.Location = New System.Drawing.Point(10, 92)
        Me.MyLabel35.Name = "MyLabel35"
        Me.MyLabel35.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel35.TabIndex = 61
        Me.MyLabel35.Text = "Remarks"
        '
        'txtEmail
        '
        Me.txtEmail.CalculationExpression = Nothing
        Me.txtEmail.FieldCode = Nothing
        Me.txtEmail.FieldDesc = Nothing
        Me.txtEmail.FieldMaxLength = 0
        Me.txtEmail.FieldName = Nothing
        Me.txtEmail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.isCalculatedField = False
        Me.txtEmail.IsSourceFromTable = False
        Me.txtEmail.IsSourceFromValueList = False
        Me.txtEmail.IsUnique = False
        Me.txtEmail.Location = New System.Drawing.Point(116, 34)
        Me.txtEmail.MaxLength = 100
        Me.txtEmail.MendatroryField = False
        Me.txtEmail.MyLinkLable1 = Me.MyLabel33
        Me.txtEmail.MyLinkLable2 = Nothing
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.ReferenceFieldDesc = Nothing
        Me.txtEmail.ReferenceFieldName = Nothing
        Me.txtEmail.ReferenceTableName = Nothing
        Me.txtEmail.Size = New System.Drawing.Size(208, 18)
        Me.txtEmail.TabIndex = 2
        '
        'MyLabel33
        '
        Me.MyLabel33.FieldName = Nothing
        Me.MyLabel33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel33.Location = New System.Drawing.Point(10, 35)
        Me.MyLabel33.Name = "MyLabel33"
        Me.MyLabel33.Size = New System.Drawing.Size(34, 16)
        Me.MyLabel33.TabIndex = 59
        Me.MyLabel33.Text = "Email"
        '
        'txtPassportNo
        '
        Me.txtPassportNo.CalculationExpression = Nothing
        Me.txtPassportNo.FieldCode = Nothing
        Me.txtPassportNo.FieldDesc = Nothing
        Me.txtPassportNo.FieldMaxLength = 0
        Me.txtPassportNo.FieldName = Nothing
        Me.txtPassportNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassportNo.isCalculatedField = False
        Me.txtPassportNo.IsSourceFromTable = False
        Me.txtPassportNo.IsSourceFromValueList = False
        Me.txtPassportNo.IsUnique = False
        Me.txtPassportNo.Location = New System.Drawing.Point(459, 15)
        Me.txtPassportNo.MaxLength = 20
        Me.txtPassportNo.MendatroryField = False
        Me.txtPassportNo.MyLinkLable1 = Me.MyLabel32
        Me.txtPassportNo.MyLinkLable2 = Nothing
        Me.txtPassportNo.Name = "txtPassportNo"
        Me.txtPassportNo.ReferenceFieldDesc = Nothing
        Me.txtPassportNo.ReferenceFieldName = Nothing
        Me.txtPassportNo.ReferenceTableName = Nothing
        Me.txtPassportNo.Size = New System.Drawing.Size(208, 18)
        Me.txtPassportNo.TabIndex = 1
        '
        'MyLabel32
        '
        Me.MyLabel32.FieldName = Nothing
        Me.MyLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel32.Location = New System.Drawing.Point(353, 16)
        Me.MyLabel32.Name = "MyLabel32"
        Me.MyLabel32.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel32.TabIndex = 57
        Me.MyLabel32.Text = "Passport No"
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
        Me.txtPanNo.Location = New System.Drawing.Point(116, 15)
        Me.txtPanNo.MaxLength = 20
        Me.txtPanNo.MendatroryField = False
        Me.txtPanNo.MyLinkLable1 = Me.MyLabel41
        Me.txtPanNo.MyLinkLable2 = Nothing
        Me.txtPanNo.Name = "txtPanNo"
        Me.txtPanNo.ReferenceFieldDesc = Nothing
        Me.txtPanNo.ReferenceFieldName = Nothing
        Me.txtPanNo.ReferenceTableName = Nothing
        Me.txtPanNo.Size = New System.Drawing.Size(208, 18)
        Me.txtPanNo.TabIndex = 0
        '
        'MyLabel41
        '
        Me.MyLabel41.FieldName = Nothing
        Me.MyLabel41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel41.Location = New System.Drawing.Point(10, 16)
        Me.MyLabel41.Name = "MyLabel41"
        Me.MyLabel41.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel41.TabIndex = 55
        Me.MyLabel41.Text = "Pan No"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.txtAdd2_Verifi_Remarks)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel105)
        Me.RadGroupBox3.Controls.Add(Me.chkAdd2_Verified)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel106)
        Me.RadGroupBox3.Controls.Add(Me.cboAdd2_Type)
        Me.RadGroupBox3.Controls.Add(Me.txtAdd2_PoliceStation)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel107)
        Me.RadGroupBox3.Controls.Add(Me.txtAdd2_PostOffice)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel108)
        Me.RadGroupBox3.Controls.Add(Me.txtAdd2_Village)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel109)
        Me.RadGroupBox3.Controls.Add(Me.txtAdd2_Tehsil)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel110)
        Me.RadGroupBox3.Controls.Add(Me.txtPresentCountry)
        Me.RadGroupBox3.Controls.Add(Me.txtPresentMobileNo)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel23)
        Me.RadGroupBox3.Controls.Add(Me.txtPresentPhoneNo)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel22)
        Me.RadGroupBox3.Controls.Add(Me.txtPresentPostalCode)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel24)
        Me.RadGroupBox3.Controls.Add(Me.txtPresentCity)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel20)
        Me.RadGroupBox3.Controls.Add(Me.txtPresentState)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel21)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel19)
        Me.RadGroupBox3.Controls.Add(Me.txtPresentAddress)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel18)
        Me.RadGroupBox3.HeaderText = "Current Address"
        Me.RadGroupBox3.Location = New System.Drawing.Point(348, 12)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(340, 289)
        Me.RadGroupBox3.TabIndex = 1
        Me.RadGroupBox3.Text = "Current Address"
        '
        'txtAdd2_Verifi_Remarks
        '
        Me.txtAdd2_Verifi_Remarks.CalculationExpression = Nothing
        Me.txtAdd2_Verifi_Remarks.FieldCode = Nothing
        Me.txtAdd2_Verifi_Remarks.FieldDesc = Nothing
        Me.txtAdd2_Verifi_Remarks.FieldMaxLength = 0
        Me.txtAdd2_Verifi_Remarks.FieldName = Nothing
        Me.txtAdd2_Verifi_Remarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd2_Verifi_Remarks.isCalculatedField = False
        Me.txtAdd2_Verifi_Remarks.IsSourceFromTable = False
        Me.txtAdd2_Verifi_Remarks.IsSourceFromValueList = False
        Me.txtAdd2_Verifi_Remarks.IsUnique = False
        Me.txtAdd2_Verifi_Remarks.Location = New System.Drawing.Point(116, 266)
        Me.txtAdd2_Verifi_Remarks.MaxLength = 100
        Me.txtAdd2_Verifi_Remarks.MendatroryField = False
        Me.txtAdd2_Verifi_Remarks.MyLinkLable1 = Me.MyLabel105
        Me.txtAdd2_Verifi_Remarks.MyLinkLable2 = Nothing
        Me.txtAdd2_Verifi_Remarks.Name = "txtAdd2_Verifi_Remarks"
        Me.txtAdd2_Verifi_Remarks.ReferenceFieldDesc = Nothing
        Me.txtAdd2_Verifi_Remarks.ReferenceFieldName = Nothing
        Me.txtAdd2_Verifi_Remarks.ReferenceTableName = Nothing
        Me.txtAdd2_Verifi_Remarks.Size = New System.Drawing.Size(208, 18)
        Me.txtAdd2_Verifi_Remarks.TabIndex = 13
        '
        'MyLabel105
        '
        Me.MyLabel105.FieldName = Nothing
        Me.MyLabel105.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel105.Location = New System.Drawing.Point(10, 267)
        Me.MyLabel105.Name = "MyLabel105"
        Me.MyLabel105.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel105.TabIndex = 157
        Me.MyLabel105.Text = "Verifi. Remarks"
        '
        'chkAdd2_Verified
        '
        Me.chkAdd2_Verified.Location = New System.Drawing.Point(116, 248)
        Me.chkAdd2_Verified.Name = "chkAdd2_Verified"
        Me.chkAdd2_Verified.Size = New System.Drawing.Size(102, 18)
        Me.chkAdd2_Verified.TabIndex = 12
        Me.chkAdd2_Verified.Text = "Address Verified"
        '
        'MyLabel106
        '
        Me.MyLabel106.FieldName = Nothing
        Me.MyLabel106.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel106.Location = New System.Drawing.Point(11, 231)
        Me.MyLabel106.Name = "MyLabel106"
        Me.MyLabel106.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel106.TabIndex = 154
        Me.MyLabel106.Text = "Type"
        '
        'cboAdd2_Type
        '
        Me.cboAdd2_Type.AutoCompleteDisplayMember = Nothing
        Me.cboAdd2_Type.AutoCompleteValueMember = Nothing
        Me.cboAdd2_Type.CalculationExpression = Nothing
        Me.cboAdd2_Type.DropDownAnimationEnabled = True
        Me.cboAdd2_Type.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboAdd2_Type.FieldCode = Nothing
        Me.cboAdd2_Type.FieldDesc = Nothing
        Me.cboAdd2_Type.FieldMaxLength = 0
        Me.cboAdd2_Type.FieldName = Nothing
        Me.cboAdd2_Type.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAdd2_Type.isCalculatedField = False
        Me.cboAdd2_Type.IsSourceFromTable = False
        Me.cboAdd2_Type.IsSourceFromValueList = False
        Me.cboAdd2_Type.IsUnique = False
        Me.cboAdd2_Type.Location = New System.Drawing.Point(116, 230)
        Me.cboAdd2_Type.MendatroryField = False
        Me.cboAdd2_Type.MyLinkLable1 = Me.MyLabel106
        Me.cboAdd2_Type.MyLinkLable2 = Nothing
        Me.cboAdd2_Type.Name = "cboAdd2_Type"
        Me.cboAdd2_Type.ReferenceFieldDesc = Nothing
        Me.cboAdd2_Type.ReferenceFieldName = Nothing
        Me.cboAdd2_Type.ReferenceTableName = Nothing
        Me.cboAdd2_Type.Size = New System.Drawing.Size(208, 18)
        Me.cboAdd2_Type.TabIndex = 11
        '
        'txtAdd2_PoliceStation
        '
        Me.txtAdd2_PoliceStation.CalculationExpression = Nothing
        Me.txtAdd2_PoliceStation.FieldCode = Nothing
        Me.txtAdd2_PoliceStation.FieldDesc = Nothing
        Me.txtAdd2_PoliceStation.FieldMaxLength = 0
        Me.txtAdd2_PoliceStation.FieldName = Nothing
        Me.txtAdd2_PoliceStation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd2_PoliceStation.isCalculatedField = False
        Me.txtAdd2_PoliceStation.IsSourceFromTable = False
        Me.txtAdd2_PoliceStation.IsSourceFromValueList = False
        Me.txtAdd2_PoliceStation.IsUnique = False
        Me.txtAdd2_PoliceStation.Location = New System.Drawing.Point(116, 212)
        Me.txtAdd2_PoliceStation.MaxLength = 100
        Me.txtAdd2_PoliceStation.MendatroryField = False
        Me.txtAdd2_PoliceStation.MyLinkLable1 = Me.MyLabel107
        Me.txtAdd2_PoliceStation.MyLinkLable2 = Nothing
        Me.txtAdd2_PoliceStation.Name = "txtAdd2_PoliceStation"
        Me.txtAdd2_PoliceStation.ReferenceFieldDesc = Nothing
        Me.txtAdd2_PoliceStation.ReferenceFieldName = Nothing
        Me.txtAdd2_PoliceStation.ReferenceTableName = Nothing
        Me.txtAdd2_PoliceStation.Size = New System.Drawing.Size(208, 18)
        Me.txtAdd2_PoliceStation.TabIndex = 10
        '
        'MyLabel107
        '
        Me.MyLabel107.FieldName = Nothing
        Me.MyLabel107.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel107.Location = New System.Drawing.Point(11, 213)
        Me.MyLabel107.Name = "MyLabel107"
        Me.MyLabel107.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel107.TabIndex = 152
        Me.MyLabel107.Text = "Police Station"
        '
        'txtAdd2_PostOffice
        '
        Me.txtAdd2_PostOffice.CalculationExpression = Nothing
        Me.txtAdd2_PostOffice.FieldCode = Nothing
        Me.txtAdd2_PostOffice.FieldDesc = Nothing
        Me.txtAdd2_PostOffice.FieldMaxLength = 0
        Me.txtAdd2_PostOffice.FieldName = Nothing
        Me.txtAdd2_PostOffice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd2_PostOffice.isCalculatedField = False
        Me.txtAdd2_PostOffice.IsSourceFromTable = False
        Me.txtAdd2_PostOffice.IsSourceFromValueList = False
        Me.txtAdd2_PostOffice.IsUnique = False
        Me.txtAdd2_PostOffice.Location = New System.Drawing.Point(117, 194)
        Me.txtAdd2_PostOffice.MaxLength = 100
        Me.txtAdd2_PostOffice.MendatroryField = False
        Me.txtAdd2_PostOffice.MyLinkLable1 = Me.MyLabel108
        Me.txtAdd2_PostOffice.MyLinkLable2 = Nothing
        Me.txtAdd2_PostOffice.Name = "txtAdd2_PostOffice"
        Me.txtAdd2_PostOffice.ReferenceFieldDesc = Nothing
        Me.txtAdd2_PostOffice.ReferenceFieldName = Nothing
        Me.txtAdd2_PostOffice.ReferenceTableName = Nothing
        Me.txtAdd2_PostOffice.Size = New System.Drawing.Size(208, 18)
        Me.txtAdd2_PostOffice.TabIndex = 9
        '
        'MyLabel108
        '
        Me.MyLabel108.FieldName = Nothing
        Me.MyLabel108.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel108.Location = New System.Drawing.Point(11, 195)
        Me.MyLabel108.Name = "MyLabel108"
        Me.MyLabel108.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel108.TabIndex = 150
        Me.MyLabel108.Text = "Post Office"
        '
        'txtAdd2_Village
        '
        Me.txtAdd2_Village.CalculationExpression = Nothing
        Me.txtAdd2_Village.FieldCode = Nothing
        Me.txtAdd2_Village.FieldDesc = Nothing
        Me.txtAdd2_Village.FieldMaxLength = 0
        Me.txtAdd2_Village.FieldName = Nothing
        Me.txtAdd2_Village.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd2_Village.isCalculatedField = False
        Me.txtAdd2_Village.IsSourceFromTable = False
        Me.txtAdd2_Village.IsSourceFromValueList = False
        Me.txtAdd2_Village.IsUnique = False
        Me.txtAdd2_Village.Location = New System.Drawing.Point(117, 176)
        Me.txtAdd2_Village.MaxLength = 100
        Me.txtAdd2_Village.MendatroryField = False
        Me.txtAdd2_Village.MyLinkLable1 = Me.MyLabel109
        Me.txtAdd2_Village.MyLinkLable2 = Nothing
        Me.txtAdd2_Village.Name = "txtAdd2_Village"
        Me.txtAdd2_Village.ReferenceFieldDesc = Nothing
        Me.txtAdd2_Village.ReferenceFieldName = Nothing
        Me.txtAdd2_Village.ReferenceTableName = Nothing
        Me.txtAdd2_Village.Size = New System.Drawing.Size(208, 18)
        Me.txtAdd2_Village.TabIndex = 8
        '
        'MyLabel109
        '
        Me.MyLabel109.FieldName = Nothing
        Me.MyLabel109.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel109.Location = New System.Drawing.Point(11, 177)
        Me.MyLabel109.Name = "MyLabel109"
        Me.MyLabel109.Size = New System.Drawing.Size(40, 16)
        Me.MyLabel109.TabIndex = 148
        Me.MyLabel109.Text = "Village"
        '
        'txtAdd2_Tehsil
        '
        Me.txtAdd2_Tehsil.CalculationExpression = Nothing
        Me.txtAdd2_Tehsil.FieldCode = Nothing
        Me.txtAdd2_Tehsil.FieldDesc = Nothing
        Me.txtAdd2_Tehsil.FieldMaxLength = 0
        Me.txtAdd2_Tehsil.FieldName = Nothing
        Me.txtAdd2_Tehsil.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd2_Tehsil.isCalculatedField = False
        Me.txtAdd2_Tehsil.IsSourceFromTable = False
        Me.txtAdd2_Tehsil.IsSourceFromValueList = False
        Me.txtAdd2_Tehsil.IsUnique = False
        Me.txtAdd2_Tehsil.Location = New System.Drawing.Point(117, 158)
        Me.txtAdd2_Tehsil.MaxLength = 100
        Me.txtAdd2_Tehsil.MendatroryField = False
        Me.txtAdd2_Tehsil.MyLinkLable1 = Me.MyLabel110
        Me.txtAdd2_Tehsil.MyLinkLable2 = Nothing
        Me.txtAdd2_Tehsil.Name = "txtAdd2_Tehsil"
        Me.txtAdd2_Tehsil.ReferenceFieldDesc = Nothing
        Me.txtAdd2_Tehsil.ReferenceFieldName = Nothing
        Me.txtAdd2_Tehsil.ReferenceTableName = Nothing
        Me.txtAdd2_Tehsil.Size = New System.Drawing.Size(208, 18)
        Me.txtAdd2_Tehsil.TabIndex = 7
        '
        'MyLabel110
        '
        Me.MyLabel110.FieldName = Nothing
        Me.MyLabel110.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel110.Location = New System.Drawing.Point(11, 159)
        Me.MyLabel110.Name = "MyLabel110"
        Me.MyLabel110.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel110.TabIndex = 146
        Me.MyLabel110.Text = "Tehsil"
        '
        'txtPresentCountry
        '
        Me.txtPresentCountry.CalculationExpression = Nothing
        Me.txtPresentCountry.FieldCode = Nothing
        Me.txtPresentCountry.FieldDesc = Nothing
        Me.txtPresentCountry.FieldMaxLength = 0
        Me.txtPresentCountry.FieldName = Nothing
        Me.txtPresentCountry.isCalculatedField = False
        Me.txtPresentCountry.IsSourceFromTable = False
        Me.txtPresentCountry.IsSourceFromValueList = False
        Me.txtPresentCountry.IsUnique = False
        Me.txtPresentCountry.Location = New System.Drawing.Point(117, 47)
        Me.txtPresentCountry.MendatroryField = False
        Me.txtPresentCountry.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPresentCountry.MyLinkLable1 = Me.MyLabel19
        Me.txtPresentCountry.MyLinkLable2 = Nothing
        Me.txtPresentCountry.MyReadOnly = False
        Me.txtPresentCountry.MyShowMasterFormButton = False
        Me.txtPresentCountry.Name = "txtPresentCountry"
        Me.txtPresentCountry.ReferenceFieldDesc = Nothing
        Me.txtPresentCountry.ReferenceFieldName = Nothing
        Me.txtPresentCountry.ReferenceTableName = Nothing
        Me.txtPresentCountry.Size = New System.Drawing.Size(208, 19)
        Me.txtPresentCountry.TabIndex = 1
        Me.txtPresentCountry.Value = ""
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(11, 48)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel19.TabIndex = 107
        Me.MyLabel19.Text = "Country"
        '
        'txtPresentMobileNo
        '
        Me.txtPresentMobileNo.CalculationExpression = Nothing
        Me.txtPresentMobileNo.FieldCode = Nothing
        Me.txtPresentMobileNo.FieldDesc = Nothing
        Me.txtPresentMobileNo.FieldMaxLength = 0
        Me.txtPresentMobileNo.FieldName = Nothing
        Me.txtPresentMobileNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPresentMobileNo.isCalculatedField = False
        Me.txtPresentMobileNo.IsSourceFromTable = False
        Me.txtPresentMobileNo.IsSourceFromValueList = False
        Me.txtPresentMobileNo.IsUnique = False
        Me.txtPresentMobileNo.Location = New System.Drawing.Point(117, 122)
        Me.txtPresentMobileNo.MaxLength = 50
        Me.txtPresentMobileNo.MendatroryField = False
        Me.txtPresentMobileNo.MyLinkLable1 = Me.MyLabel23
        Me.txtPresentMobileNo.MyLinkLable2 = Nothing
        Me.txtPresentMobileNo.Name = "txtPresentMobileNo"
        Me.txtPresentMobileNo.ReferenceFieldDesc = Nothing
        Me.txtPresentMobileNo.ReferenceFieldName = Nothing
        Me.txtPresentMobileNo.ReferenceTableName = Nothing
        Me.txtPresentMobileNo.Size = New System.Drawing.Size(208, 18)
        Me.txtPresentMobileNo.TabIndex = 5
        '
        'MyLabel23
        '
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel23.Location = New System.Drawing.Point(11, 123)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel23.TabIndex = 115
        Me.MyLabel23.Text = "Mobile No"
        '
        'txtPresentPhoneNo
        '
        Me.txtPresentPhoneNo.CalculationExpression = Nothing
        Me.txtPresentPhoneNo.FieldCode = Nothing
        Me.txtPresentPhoneNo.FieldDesc = Nothing
        Me.txtPresentPhoneNo.FieldMaxLength = 0
        Me.txtPresentPhoneNo.FieldName = Nothing
        Me.txtPresentPhoneNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPresentPhoneNo.isCalculatedField = False
        Me.txtPresentPhoneNo.IsSourceFromTable = False
        Me.txtPresentPhoneNo.IsSourceFromValueList = False
        Me.txtPresentPhoneNo.IsUnique = False
        Me.txtPresentPhoneNo.Location = New System.Drawing.Point(117, 104)
        Me.txtPresentPhoneNo.MaxLength = 50
        Me.txtPresentPhoneNo.MendatroryField = False
        Me.txtPresentPhoneNo.MyLinkLable1 = Me.MyLabel22
        Me.txtPresentPhoneNo.MyLinkLable2 = Nothing
        Me.txtPresentPhoneNo.Name = "txtPresentPhoneNo"
        Me.txtPresentPhoneNo.ReferenceFieldDesc = Nothing
        Me.txtPresentPhoneNo.ReferenceFieldName = Nothing
        Me.txtPresentPhoneNo.ReferenceTableName = Nothing
        Me.txtPresentPhoneNo.Size = New System.Drawing.Size(208, 18)
        Me.txtPresentPhoneNo.TabIndex = 4
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel22.Location = New System.Drawing.Point(11, 105)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel22.TabIndex = 113
        Me.MyLabel22.Text = "Phone No"
        '
        'txtPresentPostalCode
        '
        Me.txtPresentPostalCode.CalculationExpression = Nothing
        Me.txtPresentPostalCode.FieldCode = Nothing
        Me.txtPresentPostalCode.FieldDesc = Nothing
        Me.txtPresentPostalCode.FieldMaxLength = 0
        Me.txtPresentPostalCode.FieldName = Nothing
        Me.txtPresentPostalCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPresentPostalCode.isCalculatedField = False
        Me.txtPresentPostalCode.IsSourceFromTable = False
        Me.txtPresentPostalCode.IsSourceFromValueList = False
        Me.txtPresentPostalCode.IsUnique = False
        Me.txtPresentPostalCode.Location = New System.Drawing.Point(117, 140)
        Me.txtPresentPostalCode.MaxLength = 6
        Me.txtPresentPostalCode.MendatroryField = False
        Me.txtPresentPostalCode.MyLinkLable1 = Me.MyLabel24
        Me.txtPresentPostalCode.MyLinkLable2 = Nothing
        Me.txtPresentPostalCode.Name = "txtPresentPostalCode"
        Me.txtPresentPostalCode.ReferenceFieldDesc = Nothing
        Me.txtPresentPostalCode.ReferenceFieldName = Nothing
        Me.txtPresentPostalCode.ReferenceTableName = Nothing
        Me.txtPresentPostalCode.Size = New System.Drawing.Size(208, 18)
        Me.txtPresentPostalCode.TabIndex = 6
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel24.Location = New System.Drawing.Point(11, 141)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel24.TabIndex = 117
        Me.MyLabel24.Text = "Postal Code"
        '
        'txtPresentCity
        '
        Me.txtPresentCity.CalculationExpression = Nothing
        Me.txtPresentCity.FieldCode = Nothing
        Me.txtPresentCity.FieldDesc = Nothing
        Me.txtPresentCity.FieldMaxLength = 0
        Me.txtPresentCity.FieldName = Nothing
        Me.txtPresentCity.isCalculatedField = False
        Me.txtPresentCity.IsSourceFromTable = False
        Me.txtPresentCity.IsSourceFromValueList = False
        Me.txtPresentCity.IsUnique = False
        Me.txtPresentCity.Location = New System.Drawing.Point(117, 85)
        Me.txtPresentCity.MendatroryField = False
        Me.txtPresentCity.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPresentCity.MyLinkLable1 = Me.MyLabel20
        Me.txtPresentCity.MyLinkLable2 = Nothing
        Me.txtPresentCity.MyReadOnly = False
        Me.txtPresentCity.MyShowMasterFormButton = False
        Me.txtPresentCity.Name = "txtPresentCity"
        Me.txtPresentCity.ReferenceFieldDesc = Nothing
        Me.txtPresentCity.ReferenceFieldName = Nothing
        Me.txtPresentCity.ReferenceTableName = Nothing
        Me.txtPresentCity.Size = New System.Drawing.Size(208, 19)
        Me.txtPresentCity.TabIndex = 3
        Me.txtPresentCity.Value = ""
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(11, 86)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(26, 16)
        Me.MyLabel20.TabIndex = 110
        Me.MyLabel20.Text = "City"
        '
        'txtPresentState
        '
        Me.txtPresentState.CalculationExpression = Nothing
        Me.txtPresentState.FieldCode = Nothing
        Me.txtPresentState.FieldDesc = Nothing
        Me.txtPresentState.FieldMaxLength = 0
        Me.txtPresentState.FieldName = Nothing
        Me.txtPresentState.isCalculatedField = False
        Me.txtPresentState.IsSourceFromTable = False
        Me.txtPresentState.IsSourceFromValueList = False
        Me.txtPresentState.IsUnique = False
        Me.txtPresentState.Location = New System.Drawing.Point(117, 66)
        Me.txtPresentState.MendatroryField = False
        Me.txtPresentState.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPresentState.MyLinkLable1 = Me.MyLabel21
        Me.txtPresentState.MyLinkLable2 = Nothing
        Me.txtPresentState.MyReadOnly = False
        Me.txtPresentState.MyShowMasterFormButton = False
        Me.txtPresentState.Name = "txtPresentState"
        Me.txtPresentState.ReferenceFieldDesc = Nothing
        Me.txtPresentState.ReferenceFieldName = Nothing
        Me.txtPresentState.ReferenceTableName = Nothing
        Me.txtPresentState.Size = New System.Drawing.Size(208, 19)
        Me.txtPresentState.TabIndex = 2
        Me.txtPresentState.Value = ""
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(11, 67)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel21.TabIndex = 108
        Me.MyLabel21.Text = "State"
        '
        'txtPresentAddress
        '
        Me.txtPresentAddress.CalculationExpression = Nothing
        Me.txtPresentAddress.FieldCode = Nothing
        Me.txtPresentAddress.FieldDesc = Nothing
        Me.txtPresentAddress.FieldMaxLength = 0
        Me.txtPresentAddress.FieldName = Nothing
        Me.txtPresentAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPresentAddress.isCalculatedField = False
        Me.txtPresentAddress.IsSourceFromTable = False
        Me.txtPresentAddress.IsSourceFromValueList = False
        Me.txtPresentAddress.IsUnique = False
        Me.txtPresentAddress.Location = New System.Drawing.Point(117, 29)
        Me.txtPresentAddress.MaxLength = 49
        Me.txtPresentAddress.MendatroryField = False
        Me.txtPresentAddress.MyLinkLable1 = Me.MyLabel18
        Me.txtPresentAddress.MyLinkLable2 = Nothing
        Me.txtPresentAddress.Name = "txtPresentAddress"
        Me.txtPresentAddress.ReferenceFieldDesc = Nothing
        Me.txtPresentAddress.ReferenceFieldName = Nothing
        Me.txtPresentAddress.ReferenceTableName = Nothing
        Me.txtPresentAddress.Size = New System.Drawing.Size(208, 18)
        Me.txtPresentAddress.TabIndex = 0
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(11, 30)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel18.TabIndex = 55
        Me.MyLabel18.Text = "Address"
        '
        'Documents
        '
        Me.Documents.Controls.Add(Me.gvEmpDoc)
        Me.Documents.ItemSize = New System.Drawing.SizeF(73.0!, 28.0!)
        Me.Documents.Location = New System.Drawing.Point(10, 37)
        Me.Documents.Name = "Documents"
        Me.Documents.Size = New System.Drawing.Size(845, 412)
        Me.Documents.Text = "Documents"
        '
        'gvEmpDoc
        '
        Me.gvEmpDoc.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvEmpDoc.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvEmpDoc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvEmpDoc.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvEmpDoc.ForeColor = System.Drawing.Color.Black
        Me.gvEmpDoc.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvEmpDoc.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvEmpDoc.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvEmpDoc.MasterTemplate.AllowAddNewRow = False
        Me.gvEmpDoc.MasterTemplate.AutoGenerateColumns = False
        Me.gvEmpDoc.MasterTemplate.EnableGrouping = False
        Me.gvEmpDoc.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvEmpDoc.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvEmpDoc.MasterTemplate.ViewDefinition = TableViewDefinition7
        Me.gvEmpDoc.MyStopExport = False
        Me.gvEmpDoc.Name = "gvEmpDoc"
        Me.gvEmpDoc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvEmpDoc.ShowHeaderCellButtons = True
        Me.gvEmpDoc.Size = New System.Drawing.Size(845, 412)
        Me.gvEmpDoc.TabIndex = 9
        Me.gvEmpDoc.TabStop = False
        '
        'Experience
        '
        Me.Experience.Controls.Add(Me.gvEmpEx)
        Me.Experience.ItemSize = New System.Drawing.SizeF(70.0!, 28.0!)
        Me.Experience.Location = New System.Drawing.Point(10, 37)
        Me.Experience.Name = "Experience"
        Me.Experience.Size = New System.Drawing.Size(845, 412)
        Me.Experience.Text = "Experience"
        '
        'gvEmpEx
        '
        Me.gvEmpEx.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvEmpEx.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvEmpEx.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvEmpEx.EnableCustomFiltering = True
        Me.gvEmpEx.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvEmpEx.ForeColor = System.Drawing.Color.Black
        Me.gvEmpEx.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvEmpEx.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvEmpEx.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvEmpEx.MasterTemplate.AllowAddNewRow = False
        Me.gvEmpEx.MasterTemplate.AutoGenerateColumns = False
        Me.gvEmpEx.MasterTemplate.EnableCustomFiltering = True
        Me.gvEmpEx.MasterTemplate.EnableGrouping = False
        Me.gvEmpEx.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvEmpEx.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvEmpEx.MasterTemplate.ViewDefinition = TableViewDefinition8
        Me.gvEmpEx.MyStopExport = False
        Me.gvEmpEx.Name = "gvEmpEx"
        Me.gvEmpEx.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvEmpEx.ShowHeaderCellButtons = True
        Me.gvEmpEx.Size = New System.Drawing.Size(845, 412)
        Me.gvEmpEx.TabIndex = 175
        Me.gvEmpEx.TabStop = False
        '
        'Qualification
        '
        Me.Qualification.Controls.Add(Me.gvEmpQuli)
        Me.Qualification.ItemSize = New System.Drawing.SizeF(79.0!, 28.0!)
        Me.Qualification.Location = New System.Drawing.Point(10, 37)
        Me.Qualification.Name = "Qualification"
        Me.Qualification.Size = New System.Drawing.Size(845, 412)
        Me.Qualification.Text = "Qualification"
        '
        'gvEmpQuli
        '
        Me.gvEmpQuli.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvEmpQuli.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvEmpQuli.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvEmpQuli.EnableCustomFiltering = True
        Me.gvEmpQuli.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvEmpQuli.ForeColor = System.Drawing.Color.Black
        Me.gvEmpQuli.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvEmpQuli.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvEmpQuli.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvEmpQuli.MasterTemplate.AllowAddNewRow = False
        Me.gvEmpQuli.MasterTemplate.AutoGenerateColumns = False
        Me.gvEmpQuli.MasterTemplate.EnableCustomFiltering = True
        Me.gvEmpQuli.MasterTemplate.EnableGrouping = False
        Me.gvEmpQuli.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvEmpQuli.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvEmpQuli.MasterTemplate.ViewDefinition = TableViewDefinition9
        Me.gvEmpQuli.MyStopExport = False
        Me.gvEmpQuli.Name = "gvEmpQuli"
        Me.gvEmpQuli.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvEmpQuli.ShowHeaderCellButtons = True
        Me.gvEmpQuli.Size = New System.Drawing.Size(845, 412)
        Me.gvEmpQuli.TabIndex = 176
        Me.gvEmpQuli.TabStop = False
        '
        'Languages
        '
        Me.Languages.Controls.Add(Me.gvEmpLanguage)
        Me.Languages.ItemSize = New System.Drawing.SizeF(65.0!, 28.0!)
        Me.Languages.Location = New System.Drawing.Point(10, 37)
        Me.Languages.Name = "Languages"
        Me.Languages.Size = New System.Drawing.Size(845, 412)
        Me.Languages.Text = "Language"
        '
        'gvEmpLanguage
        '
        Me.gvEmpLanguage.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvEmpLanguage.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvEmpLanguage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvEmpLanguage.EnableCustomFiltering = True
        Me.gvEmpLanguage.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvEmpLanguage.ForeColor = System.Drawing.Color.Black
        Me.gvEmpLanguage.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvEmpLanguage.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvEmpLanguage.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvEmpLanguage.MasterTemplate.AllowAddNewRow = False
        Me.gvEmpLanguage.MasterTemplate.AutoGenerateColumns = False
        Me.gvEmpLanguage.MasterTemplate.EnableCustomFiltering = True
        Me.gvEmpLanguage.MasterTemplate.EnableGrouping = False
        Me.gvEmpLanguage.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvEmpLanguage.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvEmpLanguage.MasterTemplate.ViewDefinition = TableViewDefinition10
        Me.gvEmpLanguage.MyStopExport = False
        Me.gvEmpLanguage.Name = "gvEmpLanguage"
        Me.gvEmpLanguage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvEmpLanguage.ShowHeaderCellButtons = True
        Me.gvEmpLanguage.Size = New System.Drawing.Size(845, 412)
        Me.gvEmpLanguage.TabIndex = 177
        Me.gvEmpLanguage.TabStop = False
        '
        'txtFamilyAge
        '
        Me.txtFamilyAge.Controls.Add(Me.gvEmpFamily)
        Me.txtFamilyAge.ItemSize = New System.Drawing.SizeF(85.0!, 28.0!)
        Me.txtFamilyAge.Location = New System.Drawing.Point(10, 37)
        Me.txtFamilyAge.Name = "txtFamilyAge"
        Me.txtFamilyAge.Size = New System.Drawing.Size(845, 412)
        Me.txtFamilyAge.Text = "Family Details"
        '
        'gvEmpFamily
        '
        Me.gvEmpFamily.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvEmpFamily.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvEmpFamily.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvEmpFamily.EnableCustomFiltering = True
        Me.gvEmpFamily.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvEmpFamily.ForeColor = System.Drawing.Color.Black
        Me.gvEmpFamily.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvEmpFamily.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvEmpFamily.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvEmpFamily.MasterTemplate.AllowAddNewRow = False
        Me.gvEmpFamily.MasterTemplate.AutoGenerateColumns = False
        Me.gvEmpFamily.MasterTemplate.EnableCustomFiltering = True
        Me.gvEmpFamily.MasterTemplate.EnableGrouping = False
        Me.gvEmpFamily.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvEmpFamily.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvEmpFamily.MasterTemplate.ViewDefinition = TableViewDefinition11
        Me.gvEmpFamily.MyStopExport = False
        Me.gvEmpFamily.Name = "gvEmpFamily"
        Me.gvEmpFamily.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvEmpFamily.ShowHeaderCellButtons = True
        Me.gvEmpFamily.Size = New System.Drawing.Size(845, 412)
        Me.gvEmpFamily.TabIndex = 178
        Me.gvEmpFamily.TabStop = False
        '
        'PageResignation
        '
        Me.PageResignation.Controls.Add(Me.txtRelevingDate)
        Me.PageResignation.Controls.Add(Me.lblreleaving)
        Me.PageResignation.Controls.Add(Me.chkNoDues)
        Me.PageResignation.Controls.Add(Me.txtResigSubDate)
        Me.PageResignation.Controls.Add(Me.txtNoticeInDays)
        Me.PageResignation.Controls.Add(Me.MyLabel83)
        Me.PageResignation.Controls.Add(Me.txtReasonOfLeaving)
        Me.PageResignation.Controls.Add(Me.MyLabel81)
        Me.PageResignation.Controls.Add(Me.MyLabel82)
        Me.PageResignation.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.PageResignation.Location = New System.Drawing.Point(10, 37)
        Me.PageResignation.Name = "PageResignation"
        Me.PageResignation.Size = New System.Drawing.Size(845, 412)
        Me.PageResignation.Text = "Resignation"
        '
        'txtRelevingDate
        '
        Me.txtRelevingDate.CalculationExpression = Nothing
        Me.txtRelevingDate.CustomFormat = "dd/MM/yyyy"
        Me.txtRelevingDate.FieldCode = Nothing
        Me.txtRelevingDate.FieldDesc = Nothing
        Me.txtRelevingDate.FieldMaxLength = 0
        Me.txtRelevingDate.FieldName = Nothing
        Me.txtRelevingDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRelevingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtRelevingDate.isCalculatedField = False
        Me.txtRelevingDate.IsSourceFromTable = False
        Me.txtRelevingDate.IsSourceFromValueList = False
        Me.txtRelevingDate.IsUnique = False
        Me.txtRelevingDate.Location = New System.Drawing.Point(176, 57)
        Me.txtRelevingDate.MendatroryField = False
        Me.txtRelevingDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRelevingDate.MyLinkLable1 = Nothing
        Me.txtRelevingDate.MyLinkLable2 = Nothing
        Me.txtRelevingDate.Name = "txtRelevingDate"
        Me.txtRelevingDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRelevingDate.ReferenceFieldDesc = Nothing
        Me.txtRelevingDate.ReferenceFieldName = Nothing
        Me.txtRelevingDate.ReferenceTableName = Nothing
        Me.txtRelevingDate.ShowCheckBox = True
        Me.txtRelevingDate.Size = New System.Drawing.Size(98, 18)
        Me.txtRelevingDate.TabIndex = 2
        Me.txtRelevingDate.TabStop = False
        Me.txtRelevingDate.Text = "03/05/2011"
        Me.txtRelevingDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblreleaving
        '
        Me.lblreleaving.FieldName = Nothing
        Me.lblreleaving.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblreleaving.Location = New System.Drawing.Point(6, 58)
        Me.lblreleaving.Name = "lblreleaving"
        Me.lblreleaving.Size = New System.Drawing.Size(84, 16)
        Me.lblreleaving.TabIndex = 180
        Me.lblreleaving.Text = "Releaving Date"
        '
        'chkNoDues
        '
        Me.chkNoDues.Location = New System.Drawing.Point(280, 57)
        Me.chkNoDues.Name = "chkNoDues"
        Me.chkNoDues.Size = New System.Drawing.Size(63, 18)
        Me.chkNoDues.TabIndex = 3
        Me.chkNoDues.Text = "No Dues"
        '
        'txtResigSubDate
        '
        Me.txtResigSubDate.CalculationExpression = Nothing
        Me.txtResigSubDate.CustomFormat = "dd/MM/yyyy"
        Me.txtResigSubDate.FieldCode = Nothing
        Me.txtResigSubDate.FieldDesc = Nothing
        Me.txtResigSubDate.FieldMaxLength = 0
        Me.txtResigSubDate.FieldName = Nothing
        Me.txtResigSubDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResigSubDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtResigSubDate.isCalculatedField = False
        Me.txtResigSubDate.IsSourceFromTable = False
        Me.txtResigSubDate.IsSourceFromValueList = False
        Me.txtResigSubDate.IsUnique = False
        Me.txtResigSubDate.Location = New System.Drawing.Point(176, 11)
        Me.txtResigSubDate.MendatroryField = False
        Me.txtResigSubDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtResigSubDate.MyLinkLable1 = Me.MyLabel82
        Me.txtResigSubDate.MyLinkLable2 = Nothing
        Me.txtResigSubDate.Name = "txtResigSubDate"
        Me.txtResigSubDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtResigSubDate.ReferenceFieldDesc = Nothing
        Me.txtResigSubDate.ReferenceFieldName = Nothing
        Me.txtResigSubDate.ReferenceTableName = Nothing
        Me.txtResigSubDate.ShowCheckBox = True
        Me.txtResigSubDate.Size = New System.Drawing.Size(98, 18)
        Me.txtResigSubDate.TabIndex = 0
        Me.txtResigSubDate.TabStop = False
        Me.txtResigSubDate.Text = "03/05/2011"
        Me.txtResigSubDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel82
        '
        Me.MyLabel82.FieldName = Nothing
        Me.MyLabel82.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel82.Location = New System.Drawing.Point(6, 12)
        Me.MyLabel82.Name = "MyLabel82"
        Me.MyLabel82.Size = New System.Drawing.Size(155, 16)
        Me.MyLabel82.TabIndex = 104
        Me.MyLabel82.Text = "Resignation Submission Date"
        '
        'txtNoticeInDays
        '
        Me.txtNoticeInDays.BackColor = System.Drawing.Color.White
        Me.txtNoticeInDays.CalculationExpression = Nothing
        Me.txtNoticeInDays.DecimalPlaces = 2
        Me.txtNoticeInDays.FieldCode = Nothing
        Me.txtNoticeInDays.FieldDesc = Nothing
        Me.txtNoticeInDays.FieldMaxLength = 0
        Me.txtNoticeInDays.FieldName = Nothing
        Me.txtNoticeInDays.isCalculatedField = False
        Me.txtNoticeInDays.IsSourceFromTable = False
        Me.txtNoticeInDays.IsSourceFromValueList = False
        Me.txtNoticeInDays.IsUnique = False
        Me.txtNoticeInDays.Location = New System.Drawing.Point(176, 33)
        Me.txtNoticeInDays.MendatroryField = False
        Me.txtNoticeInDays.MyLinkLable1 = Nothing
        Me.txtNoticeInDays.MyLinkLable2 = Nothing
        Me.txtNoticeInDays.Name = "txtNoticeInDays"
        Me.txtNoticeInDays.ReferenceFieldDesc = Nothing
        Me.txtNoticeInDays.ReferenceFieldName = Nothing
        Me.txtNoticeInDays.ReferenceTableName = Nothing
        Me.txtNoticeInDays.Size = New System.Drawing.Size(99, 20)
        Me.txtNoticeInDays.TabIndex = 1
        Me.txtNoticeInDays.Text = "0"
        Me.txtNoticeInDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNoticeInDays.Value = 0R
        '
        'MyLabel83
        '
        Me.MyLabel83.FieldName = Nothing
        Me.MyLabel83.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel83.Location = New System.Drawing.Point(6, 35)
        Me.MyLabel83.Name = "MyLabel83"
        Me.MyLabel83.Size = New System.Drawing.Size(130, 16)
        Me.MyLabel83.TabIndex = 107
        Me.MyLabel83.Text = "Notice Period ( In Days )"
        '
        'txtReasonOfLeaving
        '
        Me.txtReasonOfLeaving.CalculationExpression = Nothing
        Me.txtReasonOfLeaving.FieldCode = Nothing
        Me.txtReasonOfLeaving.FieldDesc = Nothing
        Me.txtReasonOfLeaving.FieldMaxLength = 0
        Me.txtReasonOfLeaving.FieldName = Nothing
        Me.txtReasonOfLeaving.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReasonOfLeaving.isCalculatedField = False
        Me.txtReasonOfLeaving.IsSourceFromTable = False
        Me.txtReasonOfLeaving.IsSourceFromValueList = False
        Me.txtReasonOfLeaving.IsUnique = False
        Me.txtReasonOfLeaving.Location = New System.Drawing.Point(176, 79)
        Me.txtReasonOfLeaving.MaxLength = 49
        Me.txtReasonOfLeaving.MendatroryField = False
        Me.txtReasonOfLeaving.MyLinkLable1 = Me.MyLabel81
        Me.txtReasonOfLeaving.MyLinkLable2 = Nothing
        Me.txtReasonOfLeaving.Name = "txtReasonOfLeaving"
        Me.txtReasonOfLeaving.ReferenceFieldDesc = Nothing
        Me.txtReasonOfLeaving.ReferenceFieldName = Nothing
        Me.txtReasonOfLeaving.ReferenceTableName = Nothing
        Me.txtReasonOfLeaving.Size = New System.Drawing.Size(243, 18)
        Me.txtReasonOfLeaving.TabIndex = 4
        '
        'MyLabel81
        '
        Me.MyLabel81.FieldName = Nothing
        Me.MyLabel81.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel81.Location = New System.Drawing.Point(6, 80)
        Me.MyLabel81.Name = "MyLabel81"
        Me.MyLabel81.Size = New System.Drawing.Size(101, 16)
        Me.MyLabel81.TabIndex = 105
        Me.MyLabel81.Text = "Reason of Leaving"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.gvAssets)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(98.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(845, 412)
        Me.RadPageViewPage1.Text = "Allocated Assets"
        '
        'gvAssets
        '
        Me.gvAssets.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvAssets.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvAssets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvAssets.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvAssets.ForeColor = System.Drawing.Color.Black
        Me.gvAssets.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvAssets.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvAssets.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvAssets.MasterTemplate.AllowAddNewRow = False
        Me.gvAssets.MasterTemplate.AutoGenerateColumns = False
        Me.gvAssets.MasterTemplate.EnableGrouping = False
        Me.gvAssets.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvAssets.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvAssets.MasterTemplate.ViewDefinition = TableViewDefinition12
        Me.gvAssets.MyStopExport = False
        Me.gvAssets.Name = "gvAssets"
        Me.gvAssets.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvAssets.ShowHeaderCellButtons = True
        Me.gvAssets.Size = New System.Drawing.Size(845, 412)
        Me.gvAssets.TabIndex = 10
        Me.gvAssets.TabStop = False
        '
        'pageOthers
        '
        Me.pageOthers.Controls.Add(Me.GroupBox2)
        Me.pageOthers.Controls.Add(Me.GroupBox1)
        Me.pageOthers.Controls.Add(Me.grpFranchise)
        Me.pageOthers.ItemSize = New System.Drawing.SizeF(134.0!, 28.0!)
        Me.pageOthers.Location = New System.Drawing.Point(10, 37)
        Me.pageOthers.Name = "pageOthers"
        Me.pageOthers.Size = New System.Drawing.Size(845, 412)
        Me.pageOthers.Text = "PF/ESI/Bank and Others"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.MyLabel43)
        Me.GroupBox2.Controls.Add(Me.txtGPFNo)
        Me.GroupBox2.Controls.Add(Me.txtTransferPF)
        Me.GroupBox2.Controls.Add(Me.chlTransPF)
        Me.GroupBox2.Controls.Add(Me.txtUANNo)
        Me.GroupBox2.Controls.Add(Me.lblUANNo)
        Me.GroupBox2.Controls.Add(Me.txtEPFMaxLimit)
        Me.GroupBox2.Controls.Add(Me.lblMaxLimit)
        Me.GroupBox2.Controls.Add(Me.lblEPFRate)
        Me.GroupBox2.Controls.Add(Me.txtEPFRate)
        Me.GroupBox2.Controls.Add(Me.MyLabel76)
        Me.GroupBox2.Controls.Add(Me.txtEsiNo)
        Me.GroupBox2.Controls.Add(Me.cboPFCalculatnType)
        Me.GroupBox2.Controls.Add(Me.chkApplyESI)
        Me.GroupBox2.Controls.Add(Me.MyLabel79)
        Me.GroupBox2.Controls.Add(Me.txtPFNo)
        Me.GroupBox2.Controls.Add(Me.txtPFNoforDeptFile)
        Me.GroupBox2.Controls.Add(Me.MyLabel77)
        Me.GroupBox2.Controls.Add(Me.MyLabel78)
        Me.GroupBox2.Controls.Add(Me.chkPFApplicable)
        Me.GroupBox2.Controls.Add(Me.txtESIDispensary)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(708, 92)
        Me.GroupBox2.TabIndex = 172
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "PF/ESI"
        '
        'MyLabel43
        '
        Me.MyLabel43.FieldName = Nothing
        Me.MyLabel43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel43.Location = New System.Drawing.Point(374, 32)
        Me.MyLabel43.Name = "MyLabel43"
        Me.MyLabel43.Size = New System.Drawing.Size(47, 16)
        Me.MyLabel43.TabIndex = 175
        Me.MyLabel43.Text = "GPF No"
        '
        'txtGPFNo
        '
        Me.txtGPFNo.CalculationExpression = Nothing
        Me.txtGPFNo.FieldCode = Nothing
        Me.txtGPFNo.FieldDesc = Nothing
        Me.txtGPFNo.FieldMaxLength = 0
        Me.txtGPFNo.FieldName = Nothing
        Me.txtGPFNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGPFNo.isCalculatedField = False
        Me.txtGPFNo.IsSourceFromTable = False
        Me.txtGPFNo.IsSourceFromValueList = False
        Me.txtGPFNo.IsUnique = False
        Me.txtGPFNo.Location = New System.Drawing.Point(432, 31)
        Me.txtGPFNo.MaxLength = 49
        Me.txtGPFNo.MendatroryField = False
        Me.txtGPFNo.MyLinkLable1 = Nothing
        Me.txtGPFNo.MyLinkLable2 = Nothing
        Me.txtGPFNo.Name = "txtGPFNo"
        Me.txtGPFNo.ReferenceFieldDesc = Nothing
        Me.txtGPFNo.ReferenceFieldName = Nothing
        Me.txtGPFNo.ReferenceTableName = Nothing
        Me.txtGPFNo.Size = New System.Drawing.Size(228, 18)
        Me.txtGPFNo.TabIndex = 174
        '
        'txtTransferPF
        '
        Me.txtTransferPF.CalculationExpression = Nothing
        Me.txtTransferPF.FieldCode = Nothing
        Me.txtTransferPF.FieldDesc = Nothing
        Me.txtTransferPF.FieldMaxLength = 0
        Me.txtTransferPF.FieldName = Nothing
        Me.txtTransferPF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransferPF.isCalculatedField = False
        Me.txtTransferPF.IsSourceFromTable = False
        Me.txtTransferPF.IsSourceFromValueList = False
        Me.txtTransferPF.IsUnique = False
        Me.txtTransferPF.Location = New System.Drawing.Point(432, 12)
        Me.txtTransferPF.MaxLength = 49
        Me.txtTransferPF.MendatroryField = False
        Me.txtTransferPF.MyLinkLable1 = Nothing
        Me.txtTransferPF.MyLinkLable2 = Nothing
        Me.txtTransferPF.Name = "txtTransferPF"
        Me.txtTransferPF.ReferenceFieldDesc = Nothing
        Me.txtTransferPF.ReferenceFieldName = Nothing
        Me.txtTransferPF.ReferenceTableName = Nothing
        Me.txtTransferPF.Size = New System.Drawing.Size(228, 18)
        Me.txtTransferPF.TabIndex = 173
        '
        'chlTransPF
        '
        Me.chlTransPF.Location = New System.Drawing.Point(350, 12)
        Me.chlTransPF.Name = "chlTransPF"
        Me.chlTransPF.Size = New System.Drawing.Size(75, 18)
        Me.chlTransPF.TabIndex = 172
        Me.chlTransPF.Text = "Transfer PF"
        '
        'txtUANNo
        '
        Me.txtUANNo.CalculationExpression = Nothing
        Me.txtUANNo.FieldCode = Nothing
        Me.txtUANNo.FieldDesc = Nothing
        Me.txtUANNo.FieldMaxLength = 0
        Me.txtUANNo.FieldName = Nothing
        Me.txtUANNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUANNo.isCalculatedField = False
        Me.txtUANNo.IsSourceFromTable = False
        Me.txtUANNo.IsSourceFromValueList = False
        Me.txtUANNo.IsUnique = False
        Me.txtUANNo.Location = New System.Drawing.Point(432, 71)
        Me.txtUANNo.MaxLength = 49
        Me.txtUANNo.MendatroryField = False
        Me.txtUANNo.MyLinkLable1 = Me.MyLabel79
        Me.txtUANNo.MyLinkLable2 = Nothing
        Me.txtUANNo.Name = "txtUANNo"
        Me.txtUANNo.ReferenceFieldDesc = Nothing
        Me.txtUANNo.ReferenceFieldName = Nothing
        Me.txtUANNo.ReferenceTableName = Nothing
        Me.txtUANNo.Size = New System.Drawing.Size(228, 18)
        Me.txtUANNo.TabIndex = 171
        '
        'MyLabel79
        '
        Me.MyLabel79.FieldName = Nothing
        Me.MyLabel79.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel79.Location = New System.Drawing.Point(6, 70)
        Me.MyLabel79.Name = "MyLabel79"
        Me.MyLabel79.Size = New System.Drawing.Size(103, 16)
        Me.MyLabel79.TabIndex = 169
        Me.MyLabel79.Text = "PF No for Dept File"
        '
        'lblUANNo
        '
        Me.lblUANNo.FieldName = Nothing
        Me.lblUANNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUANNo.Location = New System.Drawing.Point(377, 74)
        Me.lblUANNo.Name = "lblUANNo"
        Me.lblUANNo.Size = New System.Drawing.Size(48, 16)
        Me.lblUANNo.TabIndex = 170
        Me.lblUANNo.Text = "UAN No"
        '
        'txtEPFMaxLimit
        '
        Me.txtEPFMaxLimit.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtEPFMaxLimit.CalculationExpression = Nothing
        Me.txtEPFMaxLimit.DecimalPlaces = 0
        Me.txtEPFMaxLimit.FieldCode = Nothing
        Me.txtEPFMaxLimit.FieldDesc = Nothing
        Me.txtEPFMaxLimit.FieldMaxLength = 0
        Me.txtEPFMaxLimit.FieldName = Nothing
        Me.txtEPFMaxLimit.isCalculatedField = False
        Me.txtEPFMaxLimit.IsSourceFromTable = False
        Me.txtEPFMaxLimit.IsSourceFromValueList = False
        Me.txtEPFMaxLimit.IsUnique = False
        Me.txtEPFMaxLimit.Location = New System.Drawing.Point(432, 50)
        Me.txtEPFMaxLimit.MendatroryField = True
        Me.txtEPFMaxLimit.MyLinkLable1 = Me.lblMaxLimit
        Me.txtEPFMaxLimit.MyLinkLable2 = Nothing
        Me.txtEPFMaxLimit.Name = "txtEPFMaxLimit"
        Me.txtEPFMaxLimit.ReadOnly = True
        Me.txtEPFMaxLimit.ReferenceFieldDesc = Nothing
        Me.txtEPFMaxLimit.ReferenceFieldName = Nothing
        Me.txtEPFMaxLimit.ReferenceTableName = Nothing
        Me.txtEPFMaxLimit.Size = New System.Drawing.Size(79, 20)
        Me.txtEPFMaxLimit.TabIndex = 6
        Me.txtEPFMaxLimit.Text = "0"
        Me.txtEPFMaxLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtEPFMaxLimit.Value = 0R
        '
        'lblMaxLimit
        '
        Me.lblMaxLimit.FieldName = Nothing
        Me.lblMaxLimit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMaxLimit.Location = New System.Drawing.Point(374, 51)
        Me.lblMaxLimit.Name = "lblMaxLimit"
        Me.lblMaxLimit.Size = New System.Drawing.Size(55, 16)
        Me.lblMaxLimit.TabIndex = 9
        Me.lblMaxLimit.Text = "Max Limit"
        '
        'lblEPFRate
        '
        Me.lblEPFRate.FieldName = Nothing
        Me.lblEPFRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEPFRate.Location = New System.Drawing.Point(233, 51)
        Me.lblEPFRate.Name = "lblEPFRate"
        Me.lblEPFRate.Size = New System.Drawing.Size(55, 16)
        Me.lblEPFRate.TabIndex = 8
        Me.lblEPFRate.Text = "EPF Rate"
        '
        'txtEPFRate
        '
        Me.txtEPFRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtEPFRate.CalculationExpression = Nothing
        Me.txtEPFRate.DecimalPlaces = 0
        Me.txtEPFRate.FieldCode = Nothing
        Me.txtEPFRate.FieldDesc = Nothing
        Me.txtEPFRate.FieldMaxLength = 0
        Me.txtEPFRate.FieldName = Nothing
        Me.txtEPFRate.isCalculatedField = False
        Me.txtEPFRate.IsSourceFromTable = False
        Me.txtEPFRate.IsSourceFromValueList = False
        Me.txtEPFRate.IsUnique = False
        Me.txtEPFRate.Location = New System.Drawing.Point(293, 49)
        Me.txtEPFRate.MendatroryField = True
        Me.txtEPFRate.MyLinkLable1 = Me.lblMaxLimit
        Me.txtEPFRate.MyLinkLable2 = Nothing
        Me.txtEPFRate.Name = "txtEPFRate"
        Me.txtEPFRate.ReadOnly = True
        Me.txtEPFRate.ReferenceFieldDesc = Nothing
        Me.txtEPFRate.ReferenceFieldName = Nothing
        Me.txtEPFRate.ReferenceTableName = Nothing
        Me.txtEPFRate.Size = New System.Drawing.Size(74, 20)
        Me.txtEPFRate.TabIndex = 5
        Me.txtEPFRate.Text = "0"
        Me.txtEPFRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtEPFRate.Value = 0R
        '
        'MyLabel76
        '
        Me.MyLabel76.FieldName = Nothing
        Me.MyLabel76.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel76.Location = New System.Drawing.Point(6, 13)
        Me.MyLabel76.Name = "MyLabel76"
        Me.MyLabel76.Size = New System.Drawing.Size(42, 16)
        Me.MyLabel76.TabIndex = 166
        Me.MyLabel76.Text = "ESI No"
        '
        'txtEsiNo
        '
        Me.txtEsiNo.CalculationExpression = Nothing
        Me.txtEsiNo.FieldCode = Nothing
        Me.txtEsiNo.FieldDesc = Nothing
        Me.txtEsiNo.FieldMaxLength = 0
        Me.txtEsiNo.FieldName = Nothing
        Me.txtEsiNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEsiNo.isCalculatedField = False
        Me.txtEsiNo.IsSourceFromTable = False
        Me.txtEsiNo.IsSourceFromValueList = False
        Me.txtEsiNo.IsUnique = False
        Me.txtEsiNo.Location = New System.Drawing.Point(115, 12)
        Me.txtEsiNo.MaxLength = 49
        Me.txtEsiNo.MendatroryField = False
        Me.txtEsiNo.MyLinkLable1 = Nothing
        Me.txtEsiNo.MyLinkLable2 = Nothing
        Me.txtEsiNo.Name = "txtEsiNo"
        Me.txtEsiNo.ReferenceFieldDesc = Nothing
        Me.txtEsiNo.ReferenceFieldName = Nothing
        Me.txtEsiNo.ReferenceTableName = Nothing
        Me.txtEsiNo.Size = New System.Drawing.Size(228, 18)
        Me.txtEsiNo.TabIndex = 1
        '
        'cboPFCalculatnType
        '
        Me.cboPFCalculatnType.AutoCompleteDisplayMember = Nothing
        Me.cboPFCalculatnType.AutoCompleteValueMember = Nothing
        Me.cboPFCalculatnType.CalculationExpression = Nothing
        Me.cboPFCalculatnType.DropDownAnimationEnabled = True
        Me.cboPFCalculatnType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboPFCalculatnType.FieldCode = Nothing
        Me.cboPFCalculatnType.FieldDesc = Nothing
        Me.cboPFCalculatnType.FieldMaxLength = 0
        Me.cboPFCalculatnType.FieldName = Nothing
        Me.cboPFCalculatnType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPFCalculatnType.isCalculatedField = False
        Me.cboPFCalculatnType.IsSourceFromTable = False
        Me.cboPFCalculatnType.IsSourceFromValueList = False
        Me.cboPFCalculatnType.IsUnique = False
        Me.cboPFCalculatnType.Location = New System.Drawing.Point(518, 50)
        Me.cboPFCalculatnType.MendatroryField = False
        Me.cboPFCalculatnType.MyLinkLable1 = Nothing
        Me.cboPFCalculatnType.MyLinkLable2 = Nothing
        Me.cboPFCalculatnType.Name = "cboPFCalculatnType"
        Me.cboPFCalculatnType.ReferenceFieldDesc = Nothing
        Me.cboPFCalculatnType.ReferenceFieldName = Nothing
        Me.cboPFCalculatnType.ReferenceTableName = Nothing
        Me.cboPFCalculatnType.Size = New System.Drawing.Size(142, 18)
        Me.cboPFCalculatnType.TabIndex = 7
        '
        'chkApplyESI
        '
        Me.chkApplyESI.Location = New System.Drawing.Point(96, 14)
        Me.chkApplyESI.Name = "chkApplyESI"
        Me.chkApplyESI.Size = New System.Drawing.Size(15, 15)
        Me.chkApplyESI.TabIndex = 0
        '
        'txtPFNo
        '
        Me.txtPFNo.CalculationExpression = Nothing
        Me.txtPFNo.FieldCode = Nothing
        Me.txtPFNo.FieldDesc = Nothing
        Me.txtPFNo.FieldMaxLength = 0
        Me.txtPFNo.FieldName = Nothing
        Me.txtPFNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPFNo.isCalculatedField = False
        Me.txtPFNo.IsSourceFromTable = False
        Me.txtPFNo.IsSourceFromValueList = False
        Me.txtPFNo.IsUnique = False
        Me.txtPFNo.Location = New System.Drawing.Point(115, 50)
        Me.txtPFNo.MaxLength = 49
        Me.txtPFNo.MendatroryField = False
        Me.txtPFNo.MyLinkLable1 = Me.MyLabel77
        Me.txtPFNo.MyLinkLable2 = Nothing
        Me.txtPFNo.Name = "txtPFNo"
        Me.txtPFNo.ReferenceFieldDesc = Nothing
        Me.txtPFNo.ReferenceFieldName = Nothing
        Me.txtPFNo.ReferenceTableName = Nothing
        Me.txtPFNo.Size = New System.Drawing.Size(112, 18)
        Me.txtPFNo.TabIndex = 4
        '
        'MyLabel77
        '
        Me.MyLabel77.FieldName = Nothing
        Me.MyLabel77.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel77.Location = New System.Drawing.Point(6, 51)
        Me.MyLabel77.Name = "MyLabel77"
        Me.MyLabel77.Size = New System.Drawing.Size(38, 16)
        Me.MyLabel77.TabIndex = 10
        Me.MyLabel77.Text = "PF No"
        '
        'txtPFNoforDeptFile
        '
        Me.txtPFNoforDeptFile.CalculationExpression = Nothing
        Me.txtPFNoforDeptFile.FieldCode = Nothing
        Me.txtPFNoforDeptFile.FieldDesc = Nothing
        Me.txtPFNoforDeptFile.FieldMaxLength = 0
        Me.txtPFNoforDeptFile.FieldName = Nothing
        Me.txtPFNoforDeptFile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPFNoforDeptFile.isCalculatedField = False
        Me.txtPFNoforDeptFile.IsSourceFromTable = False
        Me.txtPFNoforDeptFile.IsSourceFromValueList = False
        Me.txtPFNoforDeptFile.IsUnique = False
        Me.txtPFNoforDeptFile.Location = New System.Drawing.Point(115, 69)
        Me.txtPFNoforDeptFile.MaxLength = 49
        Me.txtPFNoforDeptFile.MendatroryField = False
        Me.txtPFNoforDeptFile.MyLinkLable1 = Me.MyLabel79
        Me.txtPFNoforDeptFile.MyLinkLable2 = Nothing
        Me.txtPFNoforDeptFile.Name = "txtPFNoforDeptFile"
        Me.txtPFNoforDeptFile.ReferenceFieldDesc = Nothing
        Me.txtPFNoforDeptFile.ReferenceFieldName = Nothing
        Me.txtPFNoforDeptFile.ReferenceTableName = Nothing
        Me.txtPFNoforDeptFile.Size = New System.Drawing.Size(228, 18)
        Me.txtPFNoforDeptFile.TabIndex = 11
        '
        'MyLabel78
        '
        Me.MyLabel78.FieldName = Nothing
        Me.MyLabel78.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel78.Location = New System.Drawing.Point(6, 32)
        Me.MyLabel78.Name = "MyLabel78"
        Me.MyLabel78.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel78.TabIndex = 168
        Me.MyLabel78.Text = "ESI Dispensary"
        '
        'chkPFApplicable
        '
        Me.chkPFApplicable.Location = New System.Drawing.Point(96, 52)
        Me.chkPFApplicable.Name = "chkPFApplicable"
        Me.chkPFApplicable.Size = New System.Drawing.Size(15, 15)
        Me.chkPFApplicable.TabIndex = 3
        '
        'txtESIDispensary
        '
        Me.txtESIDispensary.CalculationExpression = Nothing
        Me.txtESIDispensary.FieldCode = Nothing
        Me.txtESIDispensary.FieldDesc = Nothing
        Me.txtESIDispensary.FieldMaxLength = 0
        Me.txtESIDispensary.FieldName = Nothing
        Me.txtESIDispensary.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtESIDispensary.isCalculatedField = False
        Me.txtESIDispensary.IsSourceFromTable = False
        Me.txtESIDispensary.IsSourceFromValueList = False
        Me.txtESIDispensary.IsUnique = False
        Me.txtESIDispensary.Location = New System.Drawing.Point(115, 31)
        Me.txtESIDispensary.MaxLength = 49
        Me.txtESIDispensary.MendatroryField = False
        Me.txtESIDispensary.MyLinkLable1 = Nothing
        Me.txtESIDispensary.MyLinkLable2 = Nothing
        Me.txtESIDispensary.Name = "txtESIDispensary"
        Me.txtESIDispensary.ReferenceFieldDesc = Nothing
        Me.txtESIDispensary.ReferenceFieldName = Nothing
        Me.txtESIDispensary.ReferenceTableName = Nothing
        Me.txtESIDispensary.Size = New System.Drawing.Size(228, 18)
        Me.txtESIDispensary.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MyLabel47)
        Me.GroupBox1.Controls.Add(Me.MyLabel46)
        Me.GroupBox1.Controls.Add(Me.txtmembershipid)
        Me.GroupBox1.Controls.Add(Me.txtspecialdesc)
        Me.GroupBox1.Controls.Add(Me.MyLabel45)
        Me.GroupBox1.Controls.Add(Me.txtpolicy)
        Me.GroupBox1.Controls.Add(Me.MyLabel44)
        Me.GroupBox1.Controls.Add(Me.txtLICID)
        Me.GroupBox1.Controls.Add(Me.txtSecChequeRs100)
        Me.GroupBox1.Controls.Add(Me.MyLabel52)
        Me.GroupBox1.Controls.Add(Me.txtSecChequeLac1)
        Me.GroupBox1.Controls.Add(Me.MyLabel50)
        Me.GroupBox1.Controls.Add(Me.fndPaymentMode)
        Me.GroupBox1.Controls.Add(Me.MyLabel11)
        Me.GroupBox1.Controls.Add(Me.txtaccno)
        Me.GroupBox1.Controls.Add(Me.MyLabel12)
        Me.GroupBox1.Controls.Add(Me.MyLabel13)
        Me.GroupBox1.Controls.Add(Me.txtBank)
        Me.GroupBox1.Controls.Add(Me.MyLabel86)
        Me.GroupBox1.Controls.Add(Me.cboEmpNature)
        Me.GroupBox1.Controls.Add(Me.cboConveyanceType)
        Me.GroupBox1.Controls.Add(Me.MyLabel87)
        Me.GroupBox1.Controls.Add(Me.chkOTApplicable)
        Me.GroupBox1.Controls.Add(Me.chkODApplicable)
        Me.GroupBox1.Controls.Add(Me.chkShowInStatory)
        Me.GroupBox1.Controls.Add(Me.MyLabel88)
        Me.GroupBox1.Controls.Add(Me.txtMinBasicSalary)
        Me.GroupBox1.Controls.Add(Me.MyLabel89)
        Me.GroupBox1.Controls.Add(Me.fndVendor)
        Me.GroupBox1.Controls.Add(Me.MyLabel90)
        Me.GroupBox1.Controls.Add(Me.TxtAgeFPen)
        Me.GroupBox1.Controls.Add(Me.fndAgent)
        Me.GroupBox1.Controls.Add(Me.MyLabel71)
        Me.GroupBox1.Controls.Add(Me.MyLabel91)
        Me.GroupBox1.Controls.Add(Me.MyLabel39)
        Me.GroupBox1.Controls.Add(Me.fndUser)
        Me.GroupBox1.Controls.Add(Me.txtbankname)
        Me.GroupBox1.Controls.Add(Me.txtBankBranch)
        Me.GroupBox1.Controls.Add(Me.MyLabel38)
        Me.GroupBox1.Controls.Add(Me.lblBankBranch)
        Me.GroupBox1.Controls.Add(Me.TxtBankBranchName)
        Me.GroupBox1.Controls.Add(Me.lbltype)
        Me.GroupBox1.Controls.Add(Me.CboEmployeeType)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 96)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(708, 285)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Other"
        '
        'MyLabel47
        '
        Me.MyLabel47.FieldName = Nothing
        Me.MyLabel47.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel47.Location = New System.Drawing.Point(370, 171)
        Me.MyLabel47.Name = "MyLabel47"
        Me.MyLabel47.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel47.TabIndex = 186
        Me.MyLabel47.Text = "Special Desc "
        '
        'MyLabel46
        '
        Me.MyLabel46.FieldName = Nothing
        Me.MyLabel46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel46.Location = New System.Drawing.Point(368, 150)
        Me.MyLabel46.Name = "MyLabel46"
        Me.MyLabel46.Size = New System.Drawing.Size(81, 16)
        Me.MyLabel46.TabIndex = 185
        Me.MyLabel46.Text = "Membership Id"
        '
        'txtmembershipid
        '
        Me.txtmembershipid.CalculationExpression = Nothing
        Me.txtmembershipid.FieldCode = Nothing
        Me.txtmembershipid.FieldDesc = Nothing
        Me.txtmembershipid.FieldMaxLength = 0
        Me.txtmembershipid.FieldName = Nothing
        Me.txtmembershipid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmembershipid.isCalculatedField = False
        Me.txtmembershipid.IsSourceFromTable = False
        Me.txtmembershipid.IsSourceFromValueList = False
        Me.txtmembershipid.IsUnique = False
        Me.txtmembershipid.Location = New System.Drawing.Point(470, 149)
        Me.txtmembershipid.MaxLength = 49
        Me.txtmembershipid.MendatroryField = False
        Me.txtmembershipid.MyLinkLable1 = Nothing
        Me.txtmembershipid.MyLinkLable2 = Nothing
        Me.txtmembershipid.Name = "txtmembershipid"
        Me.txtmembershipid.ReferenceFieldDesc = Nothing
        Me.txtmembershipid.ReferenceFieldName = Nothing
        Me.txtmembershipid.ReferenceTableName = Nothing
        Me.txtmembershipid.Size = New System.Drawing.Size(219, 18)
        Me.txtmembershipid.TabIndex = 184
        '
        'txtspecialdesc
        '
        Me.txtspecialdesc.CalculationExpression = Nothing
        Me.txtspecialdesc.FieldCode = Nothing
        Me.txtspecialdesc.FieldDesc = Nothing
        Me.txtspecialdesc.FieldMaxLength = 0
        Me.txtspecialdesc.FieldName = Nothing
        Me.txtspecialdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtspecialdesc.isCalculatedField = False
        Me.txtspecialdesc.IsSourceFromTable = False
        Me.txtspecialdesc.IsSourceFromValueList = False
        Me.txtspecialdesc.IsUnique = False
        Me.txtspecialdesc.Location = New System.Drawing.Point(470, 169)
        Me.txtspecialdesc.MaxLength = 49
        Me.txtspecialdesc.MendatroryField = False
        Me.txtspecialdesc.MyLinkLable1 = Nothing
        Me.txtspecialdesc.MyLinkLable2 = Nothing
        Me.txtspecialdesc.Name = "txtspecialdesc"
        Me.txtspecialdesc.ReferenceFieldDesc = Nothing
        Me.txtspecialdesc.ReferenceFieldName = Nothing
        Me.txtspecialdesc.ReferenceTableName = Nothing
        Me.txtspecialdesc.Size = New System.Drawing.Size(219, 18)
        Me.txtspecialdesc.TabIndex = 183
        '
        'MyLabel45
        '
        Me.MyLabel45.FieldName = Nothing
        Me.MyLabel45.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel45.Location = New System.Drawing.Point(373, 34)
        Me.MyLabel45.Name = "MyLabel45"
        Me.MyLabel45.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel45.TabIndex = 178
        Me.MyLabel45.Text = "Policy"
        '
        'txtpolicy
        '
        Me.txtpolicy.CalculationExpression = Nothing
        Me.txtpolicy.FieldCode = Nothing
        Me.txtpolicy.FieldDesc = Nothing
        Me.txtpolicy.FieldMaxLength = 0
        Me.txtpolicy.FieldName = Nothing
        Me.txtpolicy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpolicy.isCalculatedField = False
        Me.txtpolicy.IsSourceFromTable = False
        Me.txtpolicy.IsSourceFromValueList = False
        Me.txtpolicy.IsUnique = False
        Me.txtpolicy.Location = New System.Drawing.Point(470, 34)
        Me.txtpolicy.MaxLength = 49
        Me.txtpolicy.MendatroryField = False
        Me.txtpolicy.MyLinkLable1 = Nothing
        Me.txtpolicy.MyLinkLable2 = Nothing
        Me.txtpolicy.Name = "txtpolicy"
        Me.txtpolicy.ReferenceFieldDesc = Nothing
        Me.txtpolicy.ReferenceFieldName = Nothing
        Me.txtpolicy.ReferenceTableName = Nothing
        Me.txtpolicy.Size = New System.Drawing.Size(219, 18)
        Me.txtpolicy.TabIndex = 177
        '
        'MyLabel44
        '
        Me.MyLabel44.FieldName = Nothing
        Me.MyLabel44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel44.Location = New System.Drawing.Point(371, 53)
        Me.MyLabel44.Name = "MyLabel44"
        Me.MyLabel44.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel44.TabIndex = 176
        Me.MyLabel44.Text = "LIC Id"
        '
        'txtLICID
        '
        Me.txtLICID.CalculationExpression = Nothing
        Me.txtLICID.FieldCode = Nothing
        Me.txtLICID.FieldDesc = Nothing
        Me.txtLICID.FieldMaxLength = 0
        Me.txtLICID.FieldName = Nothing
        Me.txtLICID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLICID.isCalculatedField = False
        Me.txtLICID.IsSourceFromTable = False
        Me.txtLICID.IsSourceFromValueList = False
        Me.txtLICID.IsUnique = False
        Me.txtLICID.Location = New System.Drawing.Point(470, 52)
        Me.txtLICID.MaxLength = 49
        Me.txtLICID.MendatroryField = False
        Me.txtLICID.MyLinkLable1 = Nothing
        Me.txtLICID.MyLinkLable2 = Nothing
        Me.txtLICID.Name = "txtLICID"
        Me.txtLICID.ReferenceFieldDesc = Nothing
        Me.txtLICID.ReferenceFieldName = Nothing
        Me.txtLICID.ReferenceTableName = Nothing
        Me.txtLICID.Size = New System.Drawing.Size(219, 18)
        Me.txtLICID.TabIndex = 175
        '
        'txtSecChequeRs100
        '
        Me.txtSecChequeRs100.CalculationExpression = Nothing
        Me.txtSecChequeRs100.FieldCode = Nothing
        Me.txtSecChequeRs100.FieldDesc = Nothing
        Me.txtSecChequeRs100.FieldMaxLength = 0
        Me.txtSecChequeRs100.FieldName = Nothing
        Me.txtSecChequeRs100.isCalculatedField = False
        Me.txtSecChequeRs100.IsSourceFromTable = False
        Me.txtSecChequeRs100.IsSourceFromValueList = False
        Me.txtSecChequeRs100.IsUnique = False
        Me.txtSecChequeRs100.Location = New System.Drawing.Point(165, 260)
        Me.txtSecChequeRs100.MaxLength = 200
        Me.txtSecChequeRs100.MendatroryField = False
        Me.txtSecChequeRs100.MyLinkLable1 = Me.MyLabel28
        Me.txtSecChequeRs100.MyLinkLable2 = Nothing
        Me.txtSecChequeRs100.Name = "txtSecChequeRs100"
        Me.txtSecChequeRs100.ReferenceFieldDesc = Nothing
        Me.txtSecChequeRs100.ReferenceFieldName = Nothing
        Me.txtSecChequeRs100.ReferenceTableName = Nothing
        Me.txtSecChequeRs100.Size = New System.Drawing.Size(197, 20)
        Me.txtSecChequeRs100.TabIndex = 167
        '
        'MyLabel52
        '
        Me.MyLabel52.FieldName = Nothing
        Me.MyLabel52.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel52.Location = New System.Drawing.Point(9, 263)
        Me.MyLabel52.Name = "MyLabel52"
        Me.MyLabel52.Size = New System.Drawing.Size(150, 16)
        Me.MyLabel52.TabIndex = 166
        Me.MyLabel52.Text = "Security Cheque No. Rs 100"
        '
        'txtSecChequeLac1
        '
        Me.txtSecChequeLac1.CalculationExpression = Nothing
        Me.txtSecChequeLac1.FieldCode = Nothing
        Me.txtSecChequeLac1.FieldDesc = Nothing
        Me.txtSecChequeLac1.FieldMaxLength = 0
        Me.txtSecChequeLac1.FieldName = Nothing
        Me.txtSecChequeLac1.isCalculatedField = False
        Me.txtSecChequeLac1.IsSourceFromTable = False
        Me.txtSecChequeLac1.IsSourceFromValueList = False
        Me.txtSecChequeLac1.IsUnique = False
        Me.txtSecChequeLac1.Location = New System.Drawing.Point(165, 238)
        Me.txtSecChequeLac1.MaxLength = 200
        Me.txtSecChequeLac1.MendatroryField = False
        Me.txtSecChequeLac1.MyLinkLable1 = Me.MyLabel28
        Me.txtSecChequeLac1.MyLinkLable2 = Nothing
        Me.txtSecChequeLac1.Name = "txtSecChequeLac1"
        Me.txtSecChequeLac1.ReferenceFieldDesc = Nothing
        Me.txtSecChequeLac1.ReferenceFieldName = Nothing
        Me.txtSecChequeLac1.ReferenceTableName = Nothing
        Me.txtSecChequeLac1.Size = New System.Drawing.Size(197, 20)
        Me.txtSecChequeLac1.TabIndex = 165
        '
        'MyLabel50
        '
        Me.MyLabel50.FieldName = Nothing
        Me.MyLabel50.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel50.Location = New System.Drawing.Point(9, 241)
        Me.MyLabel50.Name = "MyLabel50"
        Me.MyLabel50.Size = New System.Drawing.Size(142, 16)
        Me.MyLabel50.TabIndex = 164
        Me.MyLabel50.Text = "Security Cheque No. Lac 1"
        '
        'fndPaymentMode
        '
        Me.fndPaymentMode.CalculationExpression = Nothing
        Me.fndPaymentMode.FieldCode = Nothing
        Me.fndPaymentMode.FieldDesc = Nothing
        Me.fndPaymentMode.FieldMaxLength = 0
        Me.fndPaymentMode.FieldName = Nothing
        Me.fndPaymentMode.isCalculatedField = False
        Me.fndPaymentMode.IsSourceFromTable = False
        Me.fndPaymentMode.IsSourceFromValueList = False
        Me.fndPaymentMode.IsUnique = False
        Me.fndPaymentMode.Location = New System.Drawing.Point(470, 90)
        Me.fndPaymentMode.MendatroryField = False
        Me.fndPaymentMode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPaymentMode.MyLinkLable1 = Me.MyLabel10
        Me.fndPaymentMode.MyLinkLable2 = Nothing
        Me.fndPaymentMode.MyReadOnly = False
        Me.fndPaymentMode.MyShowMasterFormButton = False
        Me.fndPaymentMode.Name = "fndPaymentMode"
        Me.fndPaymentMode.ReferenceFieldDesc = Nothing
        Me.fndPaymentMode.ReferenceFieldName = Nothing
        Me.fndPaymentMode.ReferenceTableName = Nothing
        Me.fndPaymentMode.Size = New System.Drawing.Size(219, 19)
        Me.fndPaymentMode.TabIndex = 12
        Me.fndPaymentMode.Value = ""
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(369, 110)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel11.TabIndex = 161
        Me.MyLabel11.Text = "Bank Acc No"
        '
        'txtaccno
        '
        Me.txtaccno.CalculationExpression = Nothing
        Me.txtaccno.FieldCode = Nothing
        Me.txtaccno.FieldDesc = Nothing
        Me.txtaccno.FieldMaxLength = 0
        Me.txtaccno.FieldName = Nothing
        Me.txtaccno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtaccno.isCalculatedField = False
        Me.txtaccno.IsSourceFromTable = False
        Me.txtaccno.IsSourceFromValueList = False
        Me.txtaccno.IsUnique = False
        Me.txtaccno.Location = New System.Drawing.Point(470, 109)
        Me.txtaccno.MaxLength = 50
        Me.txtaccno.MendatroryField = False
        Me.txtaccno.MyLinkLable1 = Me.MyLabel11
        Me.txtaccno.MyLinkLable2 = Nothing
        Me.txtaccno.Name = "txtaccno"
        Me.txtaccno.ReferenceFieldDesc = Nothing
        Me.txtaccno.ReferenceFieldName = Nothing
        Me.txtaccno.ReferenceTableName = Nothing
        Me.txtaccno.Size = New System.Drawing.Size(219, 18)
        Me.txtaccno.TabIndex = 14
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(369, 91)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel12.TabIndex = 162
        Me.MyLabel12.Text = "Payment Mode"
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(369, 129)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel13.TabIndex = 163
        Me.MyLabel13.Text = "Bank Name"
        '
        'txtBank
        '
        Me.txtBank.CalculationExpression = Nothing
        Me.txtBank.FieldCode = Nothing
        Me.txtBank.FieldDesc = Nothing
        Me.txtBank.FieldMaxLength = 0
        Me.txtBank.FieldName = Nothing
        Me.txtBank.isCalculatedField = False
        Me.txtBank.IsSourceFromTable = False
        Me.txtBank.IsSourceFromValueList = False
        Me.txtBank.IsUnique = False
        Me.txtBank.Location = New System.Drawing.Point(470, 127)
        Me.txtBank.MendatroryField = False
        Me.txtBank.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBank.MyLinkLable1 = Me.MyLabel13
        Me.txtBank.MyLinkLable2 = Nothing
        Me.txtBank.MyReadOnly = False
        Me.txtBank.MyShowMasterFormButton = False
        Me.txtBank.Name = "txtBank"
        Me.txtBank.ReferenceFieldDesc = Nothing
        Me.txtBank.ReferenceFieldName = Nothing
        Me.txtBank.ReferenceTableName = Nothing
        Me.txtBank.Size = New System.Drawing.Size(219, 20)
        Me.txtBank.TabIndex = 16
        Me.txtBank.Value = ""
        '
        'MyLabel86
        '
        Me.MyLabel86.FieldName = Nothing
        Me.MyLabel86.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel86.Location = New System.Drawing.Point(6, 14)
        Me.MyLabel86.Name = "MyLabel86"
        Me.MyLabel86.Size = New System.Drawing.Size(106, 16)
        Me.MyLabel86.TabIndex = 75
        Me.MyLabel86.Text = "Employment Nature"
        '
        'cboEmpNature
        '
        Me.cboEmpNature.AutoCompleteDisplayMember = Nothing
        Me.cboEmpNature.AutoCompleteValueMember = Nothing
        Me.cboEmpNature.CalculationExpression = Nothing
        Me.cboEmpNature.DropDownAnimationEnabled = True
        Me.cboEmpNature.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboEmpNature.FieldCode = Nothing
        Me.cboEmpNature.FieldDesc = Nothing
        Me.cboEmpNature.FieldMaxLength = 0
        Me.cboEmpNature.FieldName = Nothing
        Me.cboEmpNature.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEmpNature.isCalculatedField = False
        Me.cboEmpNature.IsSourceFromTable = False
        Me.cboEmpNature.IsSourceFromValueList = False
        Me.cboEmpNature.IsUnique = False
        Me.cboEmpNature.Location = New System.Drawing.Point(144, 13)
        Me.cboEmpNature.MendatroryField = True
        Me.cboEmpNature.MyLinkLable1 = Me.MyLabel86
        Me.cboEmpNature.MyLinkLable2 = Nothing
        Me.cboEmpNature.Name = "cboEmpNature"
        Me.cboEmpNature.ReferenceFieldDesc = Nothing
        Me.cboEmpNature.ReferenceFieldName = Nothing
        Me.cboEmpNature.ReferenceTableName = Nothing
        Me.cboEmpNature.Size = New System.Drawing.Size(219, 18)
        Me.cboEmpNature.TabIndex = 0
        '
        'cboConveyanceType
        '
        Me.cboConveyanceType.AutoCompleteDisplayMember = Nothing
        Me.cboConveyanceType.AutoCompleteValueMember = Nothing
        Me.cboConveyanceType.CalculationExpression = Nothing
        Me.cboConveyanceType.DropDownAnimationEnabled = True
        Me.cboConveyanceType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboConveyanceType.FieldCode = Nothing
        Me.cboConveyanceType.FieldDesc = Nothing
        Me.cboConveyanceType.FieldMaxLength = 0
        Me.cboConveyanceType.FieldName = Nothing
        Me.cboConveyanceType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboConveyanceType.isCalculatedField = False
        Me.cboConveyanceType.IsSourceFromTable = False
        Me.cboConveyanceType.IsSourceFromValueList = False
        Me.cboConveyanceType.IsUnique = False
        Me.cboConveyanceType.Location = New System.Drawing.Point(144, 32)
        Me.cboConveyanceType.MendatroryField = True
        Me.cboConveyanceType.MyLinkLable1 = Me.MyLabel87
        Me.cboConveyanceType.MyLinkLable2 = Nothing
        Me.cboConveyanceType.Name = "cboConveyanceType"
        Me.cboConveyanceType.ReferenceFieldDesc = Nothing
        Me.cboConveyanceType.ReferenceFieldName = Nothing
        Me.cboConveyanceType.ReferenceTableName = Nothing
        Me.cboConveyanceType.Size = New System.Drawing.Size(219, 18)
        Me.cboConveyanceType.TabIndex = 2
        '
        'MyLabel87
        '
        Me.MyLabel87.FieldName = Nothing
        Me.MyLabel87.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel87.Location = New System.Drawing.Point(6, 33)
        Me.MyLabel87.Name = "MyLabel87"
        Me.MyLabel87.Size = New System.Drawing.Size(97, 16)
        Me.MyLabel87.TabIndex = 77
        Me.MyLabel87.Text = "Conveyance Type"
        '
        'chkOTApplicable
        '
        Me.chkOTApplicable.Location = New System.Drawing.Point(144, 112)
        Me.chkOTApplicable.Name = "chkOTApplicable"
        Me.chkOTApplicable.Size = New System.Drawing.Size(101, 18)
        Me.chkOTApplicable.TabIndex = 6
        Me.chkOTApplicable.Text = "Is OT Applicable"
        '
        'chkODApplicable
        '
        Me.chkODApplicable.Location = New System.Drawing.Point(261, 113)
        Me.chkODApplicable.Name = "chkODApplicable"
        Me.chkODApplicable.Size = New System.Drawing.Size(103, 18)
        Me.chkODApplicable.TabIndex = 7
        Me.chkODApplicable.Text = "Is OD Applicable"
        '
        'chkShowInStatory
        '
        Me.chkShowInStatory.Location = New System.Drawing.Point(143, 131)
        Me.chkShowInStatory.Name = "chkShowInStatory"
        Me.chkShowInStatory.Size = New System.Drawing.Size(109, 18)
        Me.chkShowInStatory.TabIndex = 8
        Me.chkShowInStatory.Text = "Show in Statutory"
        '
        'MyLabel88
        '
        Me.MyLabel88.FieldName = Nothing
        Me.MyLabel88.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel88.Location = New System.Drawing.Point(6, 53)
        Me.MyLabel88.Name = "MyLabel88"
        Me.MyLabel88.Size = New System.Drawing.Size(118, 16)
        Me.MyLabel88.TabIndex = 109
        Me.MyLabel88.Text = "Minimum Basic Salary"
        '
        'txtMinBasicSalary
        '
        Me.txtMinBasicSalary.BackColor = System.Drawing.Color.White
        Me.txtMinBasicSalary.CalculationExpression = Nothing
        Me.txtMinBasicSalary.DecimalPlaces = 2
        Me.txtMinBasicSalary.FieldCode = Nothing
        Me.txtMinBasicSalary.FieldDesc = Nothing
        Me.txtMinBasicSalary.FieldMaxLength = 0
        Me.txtMinBasicSalary.FieldName = Nothing
        Me.txtMinBasicSalary.isCalculatedField = False
        Me.txtMinBasicSalary.IsSourceFromTable = False
        Me.txtMinBasicSalary.IsSourceFromValueList = False
        Me.txtMinBasicSalary.IsUnique = False
        Me.txtMinBasicSalary.Location = New System.Drawing.Point(144, 51)
        Me.txtMinBasicSalary.MendatroryField = False
        Me.txtMinBasicSalary.MyLinkLable1 = Nothing
        Me.txtMinBasicSalary.MyLinkLable2 = Nothing
        Me.txtMinBasicSalary.Name = "txtMinBasicSalary"
        Me.txtMinBasicSalary.ReferenceFieldDesc = Nothing
        Me.txtMinBasicSalary.ReferenceFieldName = Nothing
        Me.txtMinBasicSalary.ReferenceTableName = Nothing
        Me.txtMinBasicSalary.Size = New System.Drawing.Size(218, 20)
        Me.txtMinBasicSalary.TabIndex = 3
        Me.txtMinBasicSalary.Text = "0"
        Me.txtMinBasicSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMinBasicSalary.Value = 0R
        '
        'MyLabel89
        '
        Me.MyLabel89.FieldName = Nothing
        Me.MyLabel89.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel89.Location = New System.Drawing.Point(6, 73)
        Me.MyLabel89.Name = "MyLabel89"
        Me.MyLabel89.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel89.TabIndex = 111
        Me.MyLabel89.Text = "Vendor"
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
        Me.fndVendor.Location = New System.Drawing.Point(144, 72)
        Me.fndVendor.MendatroryField = False
        Me.fndVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVendor.MyLinkLable1 = Me.MyLabel89
        Me.fndVendor.MyLinkLable2 = Nothing
        Me.fndVendor.MyReadOnly = False
        Me.fndVendor.MyShowMasterFormButton = False
        Me.fndVendor.Name = "fndVendor"
        Me.fndVendor.ReferenceFieldDesc = Nothing
        Me.fndVendor.ReferenceFieldName = Nothing
        Me.fndVendor.ReferenceTableName = Nothing
        Me.fndVendor.Size = New System.Drawing.Size(219, 19)
        Me.fndVendor.TabIndex = 4
        Me.fndVendor.Value = ""
        '
        'MyLabel90
        '
        Me.MyLabel90.FieldName = Nothing
        Me.MyLabel90.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel90.Location = New System.Drawing.Point(6, 93)
        Me.MyLabel90.Name = "MyLabel90"
        Me.MyLabel90.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel90.TabIndex = 113
        Me.MyLabel90.Text = "Agent"
        '
        'TxtAgeFPen
        '
        Me.TxtAgeFPen.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtAgeFPen.CalculationExpression = Nothing
        Me.TxtAgeFPen.DecimalPlaces = 0
        Me.TxtAgeFPen.FieldCode = Nothing
        Me.TxtAgeFPen.FieldDesc = Nothing
        Me.TxtAgeFPen.FieldMaxLength = 0
        Me.TxtAgeFPen.FieldName = Nothing
        Me.TxtAgeFPen.isCalculatedField = False
        Me.TxtAgeFPen.IsSourceFromTable = False
        Me.TxtAgeFPen.IsSourceFromValueList = False
        Me.TxtAgeFPen.IsUnique = False
        Me.TxtAgeFPen.Location = New System.Drawing.Point(518, 12)
        Me.TxtAgeFPen.MaxLength = 2
        Me.TxtAgeFPen.MendatroryField = False
        Me.TxtAgeFPen.MyLinkLable1 = Me.MyLabel71
        Me.TxtAgeFPen.MyLinkLable2 = Nothing
        Me.TxtAgeFPen.Name = "TxtAgeFPen"
        Me.TxtAgeFPen.ReferenceFieldDesc = Nothing
        Me.TxtAgeFPen.ReferenceFieldName = Nothing
        Me.TxtAgeFPen.ReferenceTableName = Nothing
        Me.TxtAgeFPen.Size = New System.Drawing.Size(111, 20)
        Me.TxtAgeFPen.TabIndex = 1
        Me.TxtAgeFPen.Text = "0"
        Me.TxtAgeFPen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtAgeFPen.Value = 0R
        '
        'MyLabel71
        '
        Me.MyLabel71.FieldName = Nothing
        Me.MyLabel71.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel71.Location = New System.Drawing.Point(417, 14)
        Me.MyLabel71.Name = "MyLabel71"
        Me.MyLabel71.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel71.TabIndex = 154
        Me.MyLabel71.Text = "Age For Pension"
        '
        'fndAgent
        '
        Me.fndAgent.CalculationExpression = Nothing
        Me.fndAgent.FieldCode = Nothing
        Me.fndAgent.FieldDesc = Nothing
        Me.fndAgent.FieldMaxLength = 0
        Me.fndAgent.FieldName = Nothing
        Me.fndAgent.isCalculatedField = False
        Me.fndAgent.IsSourceFromTable = False
        Me.fndAgent.IsSourceFromValueList = False
        Me.fndAgent.IsUnique = False
        Me.fndAgent.Location = New System.Drawing.Point(144, 92)
        Me.fndAgent.MendatroryField = False
        Me.fndAgent.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndAgent.MyLinkLable1 = Me.MyLabel90
        Me.fndAgent.MyLinkLable2 = Nothing
        Me.fndAgent.MyReadOnly = False
        Me.fndAgent.MyShowMasterFormButton = False
        Me.fndAgent.Name = "fndAgent"
        Me.fndAgent.ReferenceFieldDesc = Nothing
        Me.fndAgent.ReferenceFieldName = Nothing
        Me.fndAgent.ReferenceTableName = Nothing
        Me.fndAgent.Size = New System.Drawing.Size(219, 19)
        Me.fndAgent.TabIndex = 5
        Me.fndAgent.Value = ""
        '
        'MyLabel91
        '
        Me.MyLabel91.FieldName = Nothing
        Me.MyLabel91.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel91.Location = New System.Drawing.Point(9, 151)
        Me.MyLabel91.Name = "MyLabel91"
        Me.MyLabel91.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel91.TabIndex = 115
        Me.MyLabel91.Text = "User"
        '
        'MyLabel39
        '
        Me.MyLabel39.FieldName = Nothing
        Me.MyLabel39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel39.Location = New System.Drawing.Point(9, 209)
        Me.MyLabel39.Name = "MyLabel39"
        Me.MyLabel39.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel39.TabIndex = 152
        Me.MyLabel39.Text = "Bank Name"
        '
        'fndUser
        '
        Me.fndUser.CalculationExpression = Nothing
        Me.fndUser.FieldCode = Nothing
        Me.fndUser.FieldDesc = Nothing
        Me.fndUser.FieldMaxLength = 0
        Me.fndUser.FieldName = Nothing
        Me.fndUser.isCalculatedField = False
        Me.fndUser.IsSourceFromTable = False
        Me.fndUser.IsSourceFromValueList = False
        Me.fndUser.IsUnique = False
        Me.fndUser.Location = New System.Drawing.Point(144, 150)
        Me.fndUser.MendatroryField = False
        Me.fndUser.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndUser.MyLinkLable1 = Me.MyLabel91
        Me.fndUser.MyLinkLable2 = Nothing
        Me.fndUser.MyReadOnly = False
        Me.fndUser.MyShowMasterFormButton = False
        Me.fndUser.Name = "fndUser"
        Me.fndUser.ReferenceFieldDesc = Nothing
        Me.fndUser.ReferenceFieldName = Nothing
        Me.fndUser.ReferenceTableName = Nothing
        Me.fndUser.Size = New System.Drawing.Size(219, 19)
        Me.fndUser.TabIndex = 9
        Me.fndUser.Value = ""
        '
        'txtbankname
        '
        Me.txtbankname.CalculationExpression = Nothing
        Me.txtbankname.FieldCode = Nothing
        Me.txtbankname.FieldDesc = Nothing
        Me.txtbankname.FieldMaxLength = 0
        Me.txtbankname.FieldName = Nothing
        Me.txtbankname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbankname.isCalculatedField = False
        Me.txtbankname.IsSourceFromTable = False
        Me.txtbankname.IsSourceFromValueList = False
        Me.txtbankname.IsUnique = False
        Me.txtbankname.Location = New System.Drawing.Point(143, 208)
        Me.txtbankname.MaxLength = 100
        Me.txtbankname.MendatroryField = False
        Me.txtbankname.MyLinkLable1 = Nothing
        Me.txtbankname.MyLinkLable2 = Nothing
        Me.txtbankname.Name = "txtbankname"
        Me.txtbankname.ReferenceFieldDesc = Nothing
        Me.txtbankname.ReferenceFieldName = Nothing
        Me.txtbankname.ReferenceTableName = Nothing
        Me.txtbankname.Size = New System.Drawing.Size(222, 18)
        Me.txtbankname.TabIndex = 15
        '
        'txtBankBranch
        '
        Me.txtBankBranch.CalculationExpression = Nothing
        Me.txtBankBranch.FieldCode = Nothing
        Me.txtBankBranch.FieldDesc = Nothing
        Me.txtBankBranch.FieldMaxLength = 0
        Me.txtBankBranch.FieldName = Nothing
        Me.txtBankBranch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankBranch.isCalculatedField = False
        Me.txtBankBranch.IsSourceFromTable = False
        Me.txtBankBranch.IsSourceFromValueList = False
        Me.txtBankBranch.IsUnique = False
        Me.txtBankBranch.Location = New System.Drawing.Point(144, 170)
        Me.txtBankBranch.MaxLength = 49
        Me.txtBankBranch.MendatroryField = False
        Me.txtBankBranch.MyLinkLable1 = Nothing
        Me.txtBankBranch.MyLinkLable2 = Nothing
        Me.txtBankBranch.Name = "txtBankBranch"
        Me.txtBankBranch.ReferenceFieldDesc = Nothing
        Me.txtBankBranch.ReferenceFieldName = Nothing
        Me.txtBankBranch.ReferenceTableName = Nothing
        Me.txtBankBranch.Size = New System.Drawing.Size(219, 18)
        Me.txtBankBranch.TabIndex = 11
        '
        'MyLabel38
        '
        Me.MyLabel38.FieldName = Nothing
        Me.MyLabel38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel38.Location = New System.Drawing.Point(9, 190)
        Me.MyLabel38.Name = "MyLabel38"
        Me.MyLabel38.Size = New System.Drawing.Size(104, 16)
        Me.MyLabel38.TabIndex = 150
        Me.MyLabel38.Text = "Bank Branch Name"
        '
        'lblBankBranch
        '
        Me.lblBankBranch.FieldName = Nothing
        Me.lblBankBranch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankBranch.Location = New System.Drawing.Point(11, 171)
        Me.lblBankBranch.Name = "lblBankBranch"
        Me.lblBankBranch.Size = New System.Drawing.Size(100, 16)
        Me.lblBankBranch.TabIndex = 117
        Me.lblBankBranch.Text = "Bank Branch IFSC"
        '
        'TxtBankBranchName
        '
        Me.TxtBankBranchName.CalculationExpression = Nothing
        Me.TxtBankBranchName.FieldCode = Nothing
        Me.TxtBankBranchName.FieldDesc = Nothing
        Me.TxtBankBranchName.FieldMaxLength = 0
        Me.TxtBankBranchName.FieldName = Nothing
        Me.TxtBankBranchName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBankBranchName.isCalculatedField = False
        Me.TxtBankBranchName.IsSourceFromTable = False
        Me.TxtBankBranchName.IsSourceFromValueList = False
        Me.TxtBankBranchName.IsUnique = False
        Me.TxtBankBranchName.Location = New System.Drawing.Point(143, 189)
        Me.TxtBankBranchName.MaxLength = 100
        Me.TxtBankBranchName.MendatroryField = False
        Me.TxtBankBranchName.MyLinkLable1 = Nothing
        Me.TxtBankBranchName.MyLinkLable2 = Nothing
        Me.TxtBankBranchName.Name = "TxtBankBranchName"
        Me.TxtBankBranchName.ReferenceFieldDesc = Nothing
        Me.TxtBankBranchName.ReferenceFieldName = Nothing
        Me.TxtBankBranchName.ReferenceTableName = Nothing
        Me.TxtBankBranchName.Size = New System.Drawing.Size(223, 18)
        Me.TxtBankBranchName.TabIndex = 13
        '
        'lbltype
        '
        Me.lbltype.FieldName = Nothing
        Me.lbltype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltype.Location = New System.Drawing.Point(370, 71)
        Me.lbltype.Name = "lbltype"
        Me.lbltype.Size = New System.Drawing.Size(85, 16)
        Me.lbltype.TabIndex = 119
        Me.lbltype.Text = "Employee Type"
        '
        'CboEmployeeType
        '
        Me.CboEmployeeType.AutoCompleteDisplayMember = Nothing
        Me.CboEmployeeType.AutoCompleteValueMember = Nothing
        Me.CboEmployeeType.CalculationExpression = Nothing
        Me.CboEmployeeType.DropDownAnimationEnabled = True
        Me.CboEmployeeType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboEmployeeType.FieldCode = Nothing
        Me.CboEmployeeType.FieldDesc = Nothing
        Me.CboEmployeeType.FieldMaxLength = 0
        Me.CboEmployeeType.FieldName = Nothing
        Me.CboEmployeeType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboEmployeeType.isCalculatedField = False
        Me.CboEmployeeType.IsSourceFromTable = False
        Me.CboEmployeeType.IsSourceFromValueList = False
        Me.CboEmployeeType.IsUnique = False
        Me.CboEmployeeType.Location = New System.Drawing.Point(470, 71)
        Me.CboEmployeeType.MendatroryField = False
        Me.CboEmployeeType.MyLinkLable1 = Me.lbltype
        Me.CboEmployeeType.MyLinkLable2 = Nothing
        Me.CboEmployeeType.Name = "CboEmployeeType"
        Me.CboEmployeeType.ReferenceFieldDesc = Nothing
        Me.CboEmployeeType.ReferenceFieldName = Nothing
        Me.CboEmployeeType.ReferenceTableName = Nothing
        Me.CboEmployeeType.Size = New System.Drawing.Size(219, 18)
        Me.CboEmployeeType.TabIndex = 10
        '
        'grpFranchise
        '
        Me.grpFranchise.Controls.Add(Me.lblFranchiseCode)
        Me.grpFranchise.Controls.Add(Me.lblFranchiseName)
        Me.grpFranchise.Controls.Add(Me.txtFranchiseCode)
        Me.grpFranchise.Location = New System.Drawing.Point(3, 439)
        Me.grpFranchise.Name = "grpFranchise"
        Me.grpFranchise.Size = New System.Drawing.Size(686, 56)
        Me.grpFranchise.TabIndex = 148
        Me.grpFranchise.TabStop = False
        '
        'lblFranchiseCode
        '
        Me.lblFranchiseCode.FieldName = Nothing
        Me.lblFranchiseCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFranchiseCode.Location = New System.Drawing.Point(13, 18)
        Me.lblFranchiseCode.Name = "lblFranchiseCode"
        Me.lblFranchiseCode.Size = New System.Drawing.Size(86, 16)
        Me.lblFranchiseCode.TabIndex = 140
        Me.lblFranchiseCode.Text = "Franchise Code"
        '
        'lblFranchiseName
        '
        Me.lblFranchiseName.FieldName = Nothing
        Me.lblFranchiseName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFranchiseName.Location = New System.Drawing.Point(13, 41)
        Me.lblFranchiseName.Name = "lblFranchiseName"
        Me.lblFranchiseName.Size = New System.Drawing.Size(89, 16)
        Me.lblFranchiseName.TabIndex = 1
        Me.lblFranchiseName.Text = "Franchise Name"
        '
        'txtFranchiseCode
        '
        Me.txtFranchiseCode.CalculationExpression = Nothing
        Me.txtFranchiseCode.FieldCode = Nothing
        Me.txtFranchiseCode.FieldDesc = Nothing
        Me.txtFranchiseCode.FieldMaxLength = 0
        Me.txtFranchiseCode.FieldName = Nothing
        Me.txtFranchiseCode.isCalculatedField = False
        Me.txtFranchiseCode.IsSourceFromTable = False
        Me.txtFranchiseCode.IsSourceFromValueList = False
        Me.txtFranchiseCode.IsUnique = False
        Me.txtFranchiseCode.Location = New System.Drawing.Point(148, 15)
        Me.txtFranchiseCode.MendatroryField = False
        Me.txtFranchiseCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFranchiseCode.MyLinkLable1 = Me.lblFranchiseCode
        Me.txtFranchiseCode.MyLinkLable2 = Me.lblFranchiseName
        Me.txtFranchiseCode.MyReadOnly = False
        Me.txtFranchiseCode.MyShowMasterFormButton = False
        Me.txtFranchiseCode.Name = "txtFranchiseCode"
        Me.txtFranchiseCode.ReferenceFieldDesc = Nothing
        Me.txtFranchiseCode.ReferenceFieldName = Nothing
        Me.txtFranchiseCode.ReferenceTableName = Nothing
        Me.txtFranchiseCode.Size = New System.Drawing.Size(218, 19)
        Me.txtFranchiseCode.TabIndex = 0
        Me.txtFranchiseCode.Value = ""
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(101, 9)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblempcode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(346, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblempcode
        '
        Me.lblempcode.FieldName = Nothing
        Me.lblempcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblempcode.Location = New System.Drawing.Point(8, 11)
        Me.lblempcode.Name = "lblempcode"
        Me.lblempcode.Size = New System.Drawing.Size(91, 16)
        Me.lblempcode.TabIndex = 53
        Me.lblempcode.Text = "Employee Code"
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(447, 9)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(18, 21)
        Me.btnnew.TabIndex = 1
        Me.btnnew.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(7, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 1
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(81, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(805, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(876, 20)
        Me.RadMenu2.TabIndex = 61
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuItemExport, Me.MenuItemImport, Me.MenuItemClose})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'MenuItemExport
        '
        Me.MenuItemExport.Name = "MenuItemExport"
        Me.MenuItemExport.Text = "Export"
        '
        'MenuItemImport
        '
        Me.MenuItemImport.Name = "MenuItemImport"
        Me.MenuItemImport.Text = "Import"
        '
        'MenuItemClose
        '
        Me.MenuItemClose.AccessibleDescription = "Close"
        Me.MenuItemClose.AccessibleName = "Close"
        Me.MenuItemClose.Name = "MenuItemClose"
        Me.MenuItemClose.Text = "Email/SMS Setting"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblempcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(876, 535)
        Me.SplitContainer1.SplitterDistance = 502
        Me.SplitContainer1.TabIndex = 0
        '
        'txtLeavingDate
        '
        Me.txtLeavingDate.CalculationExpression = Nothing
        Me.txtLeavingDate.CustomFormat = "dd/MM/yyyy"
        Me.txtLeavingDate.FieldCode = Nothing
        Me.txtLeavingDate.FieldDesc = Nothing
        Me.txtLeavingDate.FieldMaxLength = 0
        Me.txtLeavingDate.FieldName = Nothing
        Me.txtLeavingDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLeavingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtLeavingDate.isCalculatedField = False
        Me.txtLeavingDate.IsSourceFromTable = False
        Me.txtLeavingDate.IsSourceFromValueList = False
        Me.txtLeavingDate.IsUnique = False
        Me.txtLeavingDate.Location = New System.Drawing.Point(474, 37)
        Me.txtLeavingDate.MendatroryField = False
        Me.txtLeavingDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtLeavingDate.MyLinkLable1 = Nothing
        Me.txtLeavingDate.MyLinkLable2 = Nothing
        Me.txtLeavingDate.Name = "txtLeavingDate"
        Me.txtLeavingDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtLeavingDate.ReferenceFieldDesc = Nothing
        Me.txtLeavingDate.ReferenceFieldName = Nothing
        Me.txtLeavingDate.ReferenceTableName = Nothing
        Me.txtLeavingDate.Size = New System.Drawing.Size(110, 18)
        Me.txtLeavingDate.TabIndex = 3
        Me.txtLeavingDate.TabStop = False
        Me.txtLeavingDate.Text = "03/05/2011"
        Me.txtLeavingDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'dtpJoining
        '
        Me.dtpJoining.CalculationExpression = Nothing
        Me.dtpJoining.CustomFormat = "dd/MM/yyyy"
        Me.dtpJoining.FieldCode = Nothing
        Me.dtpJoining.FieldDesc = Nothing
        Me.dtpJoining.FieldMaxLength = 0
        Me.dtpJoining.FieldName = Nothing
        Me.dtpJoining.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpJoining.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpJoining.isCalculatedField = False
        Me.dtpJoining.IsSourceFromTable = False
        Me.dtpJoining.IsSourceFromValueList = False
        Me.dtpJoining.IsUnique = False
        Me.dtpJoining.Location = New System.Drawing.Point(133, 37)
        Me.dtpJoining.MendatroryField = False
        Me.dtpJoining.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpJoining.MyLinkLable1 = Nothing
        Me.dtpJoining.MyLinkLable2 = Nothing
        Me.dtpJoining.Name = "dtpJoining"
        Me.dtpJoining.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpJoining.ReferenceFieldDesc = Nothing
        Me.dtpJoining.ReferenceFieldName = Nothing
        Me.dtpJoining.ReferenceTableName = Nothing
        Me.dtpJoining.Size = New System.Drawing.Size(85, 18)
        Me.dtpJoining.TabIndex = 2
        Me.dtpJoining.TabStop = False
        Me.dtpJoining.Text = "03/05/2011"
        Me.dtpJoining.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel48
        '
        Me.MyLabel48.FieldName = Nothing
        Me.MyLabel48.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel48.Location = New System.Drawing.Point(3, 374)
        Me.MyLabel48.Name = "MyLabel48"
        Me.MyLabel48.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel48.TabIndex = 169
        Me.MyLabel48.Text = "Company Bank"
        '
        'txtCompBank
        '
        Me.txtCompBank.CalculationExpression = Nothing
        Me.txtCompBank.FieldCode = Nothing
        Me.txtCompBank.FieldDesc = Nothing
        Me.txtCompBank.FieldMaxLength = 0
        Me.txtCompBank.FieldName = Nothing
        Me.txtCompBank.isCalculatedField = False
        Me.txtCompBank.IsSourceFromTable = False
        Me.txtCompBank.IsSourceFromValueList = False
        Me.txtCompBank.IsUnique = False
        Me.txtCompBank.Location = New System.Drawing.Point(104, 373)
        Me.txtCompBank.MendatroryField = False
        Me.txtCompBank.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompBank.MyLinkLable1 = Me.MyLabel48
        Me.txtCompBank.MyLinkLable2 = Nothing
        Me.txtCompBank.MyReadOnly = False
        Me.txtCompBank.MyShowMasterFormButton = False
        Me.txtCompBank.Name = "txtCompBank"
        Me.txtCompBank.ReferenceFieldDesc = Nothing
        Me.txtCompBank.ReferenceFieldName = Nothing
        Me.txtCompBank.ReferenceTableName = Nothing
        Me.txtCompBank.Size = New System.Drawing.Size(216, 19)
        Me.txtCompBank.TabIndex = 168
        Me.txtCompBank.Value = ""
        '
        'frmEmployee_Master
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(876, 555)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu2)
        Me.Name = "frmEmployee_Master"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Employee Master"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.General.ResumeLayout(False)
        Me.General.PerformLayout()
        CType(Me.txtActiveInactiveDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblActiveInactiveDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboemployeebasistype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBiometricEmpID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel36, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkHoldsalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBloodGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel93, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUINNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel92, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel85, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel84, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldatebirth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel80, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWardCircle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDOB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbljoin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcardno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcardno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtJoiningDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboGender, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboMaritalStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmptyEx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel70, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel75, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAnniversaryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSpouseName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel74, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPayRollCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMothersName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel73, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel51, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFathersName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProbationEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel69, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtConfirmationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboEmployeeStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblstatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Contact.ResumeLayout(False)
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.txtAdd1_Verfi_Remarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel104, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAdd1_Verified, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel103, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboAdd1_Type, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd1_PoliceStation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel102, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd1_PostOffice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel101, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd1_Village, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel100, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd1_Tehsil, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel99, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPermMobileNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPermPhoneNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPermPostalCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSame, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPermAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.txtDLNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel97, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVoterCard, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel98, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRationCard, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel95, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAadharCard, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel96, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAlternateEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel94, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPassportNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.txtAdd2_Verifi_Remarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel105, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAdd2_Verified, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel106, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboAdd2_Type, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd2_PoliceStation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel107, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd2_PostOffice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel108, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd2_Village, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel109, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd2_Tehsil, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel110, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPresentMobileNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPresentPhoneNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPresentPostalCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPresentAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Documents.ResumeLayout(False)
        CType(Me.gvEmpDoc.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvEmpDoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Experience.ResumeLayout(False)
        CType(Me.gvEmpEx.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvEmpEx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Qualification.ResumeLayout(False)
        CType(Me.gvEmpQuli.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvEmpQuli, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Languages.ResumeLayout(False)
        CType(Me.gvEmpLanguage.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvEmpLanguage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.txtFamilyAge.ResumeLayout(False)
        CType(Me.gvEmpFamily.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvEmpFamily, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PageResignation.ResumeLayout(False)
        Me.PageResignation.PerformLayout()
        CType(Me.txtRelevingDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblreleaving, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkNoDues, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtResigSubDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel82, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoticeInDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel83, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReasonOfLeaving, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel81, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.gvAssets.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAssets, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageOthers.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGPFNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTransferPF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chlTransPF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUANNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel79, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUANNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEPFMaxLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMaxLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEPFRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEPFRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel76, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEsiNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPFCalculatnType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkApplyESI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPFNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel77, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPFNoforDeptFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel78, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPFApplicable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtESIDispensary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MyLabel47, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel46, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtmembershipid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtspecialdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel45, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtpolicy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLICID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSecChequeRs100, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel52, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSecChequeLac1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel50, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtaccno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel86, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboEmpNature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboConveyanceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel87, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOTApplicable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkODApplicable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkShowInStatory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel88, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMinBasicSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel89, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel90, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAgeFPen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel71, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel91, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbankname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankBranch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankBranch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtBankBranchName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbltype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboEmployeeType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFranchise.ResumeLayout(False)
        Me.grpFranchise.PerformLayout()
        CType(Me.lblFranchiseCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFranchiseName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblempcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtLeavingDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpJoining, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel48, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblempcode As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents General As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Documents As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Experience As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Qualification As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Languages As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtFamilyAge As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents TxtDesignation As common.UserControls.txtFinder
    Friend WithEvents CboEmployeeStatus As common.Controls.MyComboBox
    Friend WithEvents txtJoiningDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtcardno As common.Controls.MyTextBox
    Friend WithEvents lblcardno As common.Controls.MyLabel
    Friend WithEvents lbljoin As common.Controls.MyLabel
    Friend WithEvents lblstatus As common.Controls.MyLabel
    Friend WithEvents txtDOB As common.Controls.MyDateTimePicker
    Friend WithEvents lblname As common.Controls.MyLabel
    Friend WithEvents lbldes As common.Controls.MyLabel
    Friend WithEvents lbldatebirth As common.Controls.MyLabel
    Friend WithEvents txtname As common.Controls.MyTextBox
    Friend WithEvents txtDepartment As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents CboGender As common.Controls.MyComboBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents CboMaritalStatus As common.Controls.MyComboBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtAnniversaryDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtDivision As common.UserControls.txtFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtOccupation As common.UserControls.txtFinder
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtBranch As common.UserControls.txtFinder
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtGrade As common.UserControls.txtFinder
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtAttendance As common.UserControls.txtFinder
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtCastCategory As common.UserControls.txtFinder
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents txtConfirmationDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents txtProbationEndDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents Contact As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtReligion As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel51 As common.Controls.MyLabel
    Friend WithEvents MyLabel37 As common.Controls.MyLabel
    Friend WithEvents txtPayRollCode As common.Controls.MyTextBox
    Friend WithEvents TxtGLAccount As common.UserControls.txtFinder
    Friend WithEvents MyLabel34 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtPresentAddress As common.Controls.MyTextBox
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtPresentCity As common.UserControls.txtFinder
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents txtPresentState As common.UserControls.txtFinder
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents txtPresentPhoneNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents txtPresentMobileNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents txtPresentPostalCode As common.Controls.MyTextBox
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents txtPermPostalCode As common.Controls.MyTextBox
    Friend WithEvents MyLabel25 As common.Controls.MyLabel
    Friend WithEvents txtPermMobileNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel26 As common.Controls.MyLabel
    Friend WithEvents txtPermPhoneNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel27 As common.Controls.MyLabel
    Friend WithEvents txtPermCity As common.UserControls.txtFinder
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents txtPermState As common.UserControls.txtFinder
    Friend WithEvents MyLabel29 As common.Controls.MyLabel
    Friend WithEvents MyLabel30 As common.Controls.MyLabel
    Friend WithEvents txtPermAddress As common.Controls.MyTextBox
    Friend WithEvents MyLabel31 As common.Controls.MyLabel
    Friend WithEvents chkSame As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtPanNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel41 As common.Controls.MyLabel
    Friend WithEvents txtPassportNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel32 As common.Controls.MyLabel
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents MyLabel35 As common.Controls.MyLabel
    Friend WithEvents txtEmail As common.Controls.MyTextBox
    Friend WithEvents MyLabel33 As common.Controls.MyLabel
    Friend WithEvents gvEmpDoc As common.UserControls.MyRadGridView
    Friend WithEvents txtPermCountry As common.UserControls.txtFinder
    Friend WithEvents txtPresentCountry As common.UserControls.txtFinder
    Friend WithEvents txtEmptyEx As common.MyNumBox
    Friend WithEvents MyLabel70 As common.Controls.MyLabel
    Friend WithEvents MyLabel69 As common.Controls.MyLabel
    Friend WithEvents txtCompanyCode As common.UserControls.txtFinder
    Friend WithEvents txtShift As common.UserControls.txtFinder
    Friend WithEvents FileSystemWatcher1 As System.IO.FileSystemWatcher
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents gvEmpEx As common.UserControls.MyRadGridView
    Friend WithEvents gvEmpQuli As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel74 As common.Controls.MyLabel
    Friend WithEvents txtMothersName As common.Controls.MyTextBox
    Friend WithEvents MyLabel73 As common.Controls.MyLabel
    Friend WithEvents TxtFathersName As common.Controls.MyTextBox
    Friend WithEvents MyLabel75 As common.Controls.MyLabel
    Friend WithEvents txtSpouseName As common.Controls.MyTextBox
    Friend WithEvents MyLabel80 As common.Controls.MyLabel
    Friend WithEvents txtWardCircle As common.Controls.MyTextBox
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PageResignation As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel83 As common.Controls.MyLabel
    Friend WithEvents txtReasonOfLeaving As common.Controls.MyTextBox
    Friend WithEvents MyLabel81 As common.Controls.MyLabel
    Friend WithEvents MyLabel82 As common.Controls.MyLabel
    Friend WithEvents txtNoticeInDays As common.MyNumBox
    Friend WithEvents txtResigSubDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtAdvToStaff As common.UserControls.txtFinder
    Friend WithEvents MyLabel85 As common.Controls.MyLabel
    Friend WithEvents txtSalaryAccount As common.UserControls.txtFinder
    Friend WithEvents MyLabel84 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents pageOthers As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents chkShowInStatory As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkODApplicable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkOTApplicable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel87 As common.Controls.MyLabel
    Friend WithEvents cboConveyanceType As common.Controls.MyComboBox
    Friend WithEvents MyLabel86 As common.Controls.MyLabel
    Friend WithEvents cboEmpNature As common.Controls.MyComboBox
    Friend WithEvents txtMinBasicSalary As common.MyNumBox
    Friend WithEvents MyLabel88 As common.Controls.MyLabel
    Friend WithEvents fndAgent As common.UserControls.txtFinder
    Friend WithEvents MyLabel90 As common.Controls.MyLabel
    Friend WithEvents fndVendor As common.UserControls.txtFinder
    Friend WithEvents MyLabel89 As common.Controls.MyLabel
    Friend WithEvents fndUser As common.UserControls.txtFinder
    Friend WithEvents MyLabel91 As common.Controls.MyLabel
    Friend WithEvents txtUINNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel92 As common.Controls.MyLabel
    Friend WithEvents txtAlternateEmail As common.Controls.MyTextBox
    Friend WithEvents MyLabel94 As common.Controls.MyLabel
    Friend WithEvents txtRationCard As common.Controls.MyTextBox
    Friend WithEvents MyLabel95 As common.Controls.MyLabel
    Friend WithEvents txtAadharCard As common.Controls.MyTextBox
    Friend WithEvents MyLabel96 As common.Controls.MyLabel
    Friend WithEvents txtDLNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel97 As common.Controls.MyLabel
    Friend WithEvents txtVoterCard As common.Controls.MyTextBox
    Friend WithEvents MyLabel98 As common.Controls.MyLabel
    Friend WithEvents txtAdd1_Tehsil As common.Controls.MyTextBox
    Friend WithEvents MyLabel99 As common.Controls.MyLabel
    Friend WithEvents txtAdd1_Village As common.Controls.MyTextBox
    Friend WithEvents MyLabel100 As common.Controls.MyLabel
    Friend WithEvents txtAdd1_PostOffice As common.Controls.MyTextBox
    Friend WithEvents MyLabel101 As common.Controls.MyLabel
    Friend WithEvents MyLabel103 As common.Controls.MyLabel
    Friend WithEvents cboAdd1_Type As common.Controls.MyComboBox
    Friend WithEvents txtAdd1_PoliceStation As common.Controls.MyTextBox
    Friend WithEvents MyLabel102 As common.Controls.MyLabel
    Friend WithEvents chkAdd1_Verified As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtAdd1_Verfi_Remarks As common.Controls.MyTextBox
    Friend WithEvents MyLabel104 As common.Controls.MyLabel
    Friend WithEvents txtAdd2_Verifi_Remarks As common.Controls.MyTextBox
    Friend WithEvents MyLabel105 As common.Controls.MyLabel
    Friend WithEvents chkAdd2_Verified As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel106 As common.Controls.MyLabel
    Friend WithEvents cboAdd2_Type As common.Controls.MyComboBox
    Friend WithEvents txtAdd2_PoliceStation As common.Controls.MyTextBox
    Friend WithEvents MyLabel107 As common.Controls.MyLabel
    Friend WithEvents txtAdd2_PostOffice As common.Controls.MyTextBox
    Friend WithEvents MyLabel108 As common.Controls.MyLabel
    Friend WithEvents txtAdd2_Village As common.Controls.MyTextBox
    Friend WithEvents MyLabel109 As common.Controls.MyLabel
    Friend WithEvents txtAdd2_Tehsil As common.Controls.MyTextBox
    Friend WithEvents MyLabel110 As common.Controls.MyLabel
    Friend WithEvents chkNoDues As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvAssets As common.UserControls.MyRadGridView
    Friend WithEvents txtSubDepartment As common.UserControls.txtFinder
    Friend WithEvents lblSubDepartment As common.Controls.MyLabel
    Friend WithEvents lblWorkingLocation As common.Controls.MyLabel
    Friend WithEvents TxtFinder5 As common.UserControls.txtFinder
    Friend WithEvents lblLocation2 As common.Controls.MyLabel
    Friend WithEvents txtWorkingLocation As common.UserControls.txtFinder
    Friend WithEvents lblBankBranch As common.Controls.MyLabel
    Friend WithEvents txtBankBranch As common.Controls.MyTextBox
    Friend WithEvents CboEmployeeType As common.Controls.MyComboBox
    Friend WithEvents lbltype As common.Controls.MyLabel
    Friend WithEvents txtBloodGroup As common.Controls.MyTextBox
    Friend WithEvents MyLabel93 As common.Controls.MyLabel
    Friend WithEvents grpFranchise As System.Windows.Forms.GroupBox
    Friend WithEvents lblFranchiseCode As common.Controls.MyLabel
    Friend WithEvents lblFranchiseName As common.Controls.MyLabel
    Friend WithEvents txtFranchiseCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel38 As common.Controls.MyLabel
    Friend WithEvents TxtBankBranchName As common.Controls.MyTextBox
    Friend WithEvents MyLabel39 As common.Controls.MyLabel
    Friend WithEvents txtbankname As common.Controls.MyTextBox
    Friend WithEvents chkHoldsalary As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents TxtAgeFPen As common.MyNumBox
    Friend WithEvents MyLabel71 As common.Controls.MyLabel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents MyLabel76 As common.Controls.MyLabel
    Friend WithEvents txtEsiNo As common.Controls.MyTextBox
    Friend WithEvents cboPFCalculatnType As common.Controls.MyComboBox
    Friend WithEvents chkApplyESI As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel79 As common.Controls.MyLabel
    Friend WithEvents txtPFNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel77 As common.Controls.MyLabel
    Friend WithEvents txtPFNoforDeptFile As common.Controls.MyTextBox
    Friend WithEvents MyLabel78 As common.Controls.MyLabel
    Friend WithEvents chkPFApplicable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtESIDispensary As common.Controls.MyTextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtEPFMaxLimit As common.MyNumBox
    Friend WithEvents lblMaxLimit As common.Controls.MyLabel
    Friend WithEvents lblEPFRate As common.Controls.MyLabel
    Friend WithEvents txtEPFRate As common.MyNumBox
    Friend WithEvents txtRelevingDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblreleaving As common.Controls.MyLabel
    Friend WithEvents fndPaymentMode As common.UserControls.txtFinder
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents txtaccno As common.Controls.MyTextBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtBank As common.UserControls.txtFinder
    Friend WithEvents txtLeavingDate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpJoining As common.Controls.MyDateTimePicker
    Friend WithEvents gvEmpLanguage As common.UserControls.MyRadGridView
    Friend WithEvents gvEmpFamily As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents txtEmployeeBand As common.UserControls.txtFinder
    Friend WithEvents fndcity As common.UserControls.txtFinder
    Friend WithEvents MyLabel36 As common.Controls.MyLabel
    Friend WithEvents txtBiometricEmpID As common.Controls.MyTextBox
    Friend WithEvents MyLabel40 As common.Controls.MyLabel
    Friend WithEvents cboemployeebasistype As common.Controls.MyComboBox
    Friend WithEvents MyLabel42 As common.Controls.MyLabel
    Friend WithEvents MyLabel50 As common.Controls.MyLabel
    Friend WithEvents txtSecChequeLac1 As common.Controls.MyTextBox
    Friend WithEvents MyLabel52 As common.Controls.MyLabel
    Friend WithEvents txtSecChequeRs100 As common.Controls.MyTextBox
    Friend WithEvents lblUANNo As common.Controls.MyLabel
    Friend WithEvents txtUANNo As common.Controls.MyTextBox
    Friend WithEvents txtTransferPF As common.Controls.MyTextBox
    Friend WithEvents chlTransPF As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel43 As common.Controls.MyLabel
    Friend WithEvents txtGPFNo As common.Controls.MyTextBox
    Friend WithEvents txtActiveInactiveDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblActiveInactiveDate As common.Controls.MyLabel
    Friend WithEvents MyLabel44 As common.Controls.MyLabel
    Friend WithEvents txtLICID As common.Controls.MyTextBox
    Friend WithEvents MyLabel45 As common.Controls.MyLabel
    Friend WithEvents txtpolicy As common.Controls.MyTextBox
    Friend WithEvents MyLabel47 As common.Controls.MyLabel
    Friend WithEvents MyLabel46 As common.Controls.MyLabel
    Friend WithEvents txtmembershipid As common.Controls.MyTextBox
    Friend WithEvents txtspecialdesc As common.Controls.MyTextBox
    Friend WithEvents MyLabel48 As common.Controls.MyLabel
    Friend WithEvents txtCompBank As common.UserControls.txtFinder
End Class
