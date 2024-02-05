<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVendorReg
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmVendorReg))
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageView2 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtManufacturing_facilities = New Telerik.WinControls.UI.RadTextBox()
        Me.RadLabel11 = New Telerik.WinControls.UI.RadLabel()
        Me.RadGroupBox10 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtdetails_orgstructure = New Telerik.WinControls.UI.RadTextBox()
        Me.txtTurnOver = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.RadGroupBox9 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtContactPPhone = New Telerik.WinControls.UI.RadMaskedEditBox()
        Me.txtContactPName = New common.Controls.MyTextBox()
        Me.MyLabel39 = New common.Controls.MyLabel()
        Me.MyLabel37 = New common.Controls.MyLabel()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.txtFaxNo = New common.Controls.MyTextBox()
        Me.txtPhoneNo = New Telerik.WinControls.UI.RadMaskedEditBox()
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtn_subcontractor = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadGroupBox7 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtn_ownership = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtn_govt = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtn_public = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtn_pvt = New Telerik.WinControls.UI.RadRadioButton()
        Me.txtName_Owners = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtAddress1 = New common.Controls.MyTextBox()
        Me.lblChillingVendor = New common.Controls.MyLabel()
        Me.txtName = New common.Controls.MyTextBox()
        Me.MyLabel44 = New common.Controls.MyLabel()
        Me.lblMCCCode = New common.Controls.MyLabel()
        Me.txtAddress2 = New common.Controls.MyTextBox()
        Me.txtcode = New common.UserControls.txtNavigator()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvtesting = New common.UserControls.MyRadGridView()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel15 = New Telerik.WinControls.UI.RadLabel()
        Me.chk_captive = New Telerik.WinControls.UI.RadCheckBox()
        Me.txt_captivedetails = New Telerik.WinControls.UI.RadTextBox()
        Me.gvmanufacturing = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadLabel14 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel13 = New Telerik.WinControls.UI.RadLabel()
        Me.chk_PackingDefined = New Telerik.WinControls.UI.RadCheckBox()
        Me.chk_definedQP = New Telerik.WinControls.UI.RadCheckBox()
        Me.txt_PackingDefinedDetails = New Telerik.WinControls.UI.RadTextBox()
        Me.chk_FacilitiesAvailable = New Telerik.WinControls.UI.RadCheckBox()
        Me.txt_definedQPdetails = New Telerik.WinControls.UI.RadTextBox()
        Me.chk_ProStorageSys = New Telerik.WinControls.UI.RadCheckBox()
        Me.chk_equipcalperiodically = New Telerik.WinControls.UI.RadCheckBox()
        Me.chk_RMIdentified = New Telerik.WinControls.UI.RadCheckBox()
        Me.chk_tmppermdevRecord = New Telerik.WinControls.UI.RadCheckBox()
        Me.chk_KeepRecord = New Telerik.WinControls.UI.RadCheckBox()
        Me.chk_ProInsAvailable = New Telerik.WinControls.UI.RadCheckBox()
        Me.chk_InsDespatch = New Telerik.WinControls.UI.RadCheckBox()
        Me.txt_whoisauthorised = New Telerik.WinControls.UI.RadTextBox()
        Me.RadLabel12 = New Telerik.WinControls.UI.RadLabel()
        Me.chk_RMInsAvailable = New Telerik.WinControls.UI.RadCheckBox()
        Me.chk_fulldocqsavailable = New Telerik.WinControls.UI.RadCheckBox()
        Me.chk_stddrawingsavailable = New Telerik.WinControls.UI.RadCheckBox()
        Me.chk_separatesection = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadPageViewPage6 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.btnclear_vendor = New Telerik.WinControls.UI.RadButton()
        Me.btnselectsign_vendor = New Telerik.WinControls.UI.RadButton()
        Me.pb_vendor = New System.Windows.Forms.PictureBox()
        Me.dtp_vendorsigndate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadLabel28 = New Telerik.WinControls.UI.RadLabel()
        Me.txt_vendordesig = New common.Controls.MyTextBox()
        Me.RadLabel34 = New Telerik.WinControls.UI.RadLabel()
        Me.txt_vendorname = New common.Controls.MyTextBox()
        Me.RadLabel35 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel36 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel37 = New Telerik.WinControls.UI.RadLabel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.txt_paymentterms = New common.Controls.MyTextBox()
        Me.RadLabel46 = New Telerik.WinControls.UI.RadLabel()
        Me.txt_centralexciseregno = New common.Controls.MyTextBox()
        Me.txt_salestaxregno = New common.Controls.MyTextBox()
        Me.txt_nameofbanker = New common.Controls.MyTextBox()
        Me.RadLabel43 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel44 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel45 = New Telerik.WinControls.UI.RadLabel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.btnclear_assessor = New Telerik.WinControls.UI.RadButton()
        Me.btnselectsign_assessor = New Telerik.WinControls.UI.RadButton()
        Me.pb_assessor = New System.Windows.Forms.PictureBox()
        Me.dtp_assessorsigndate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadLabel38 = New Telerik.WinControls.UI.RadLabel()
        Me.txt_assessordesig = New common.Controls.MyTextBox()
        Me.RadLabel39 = New Telerik.WinControls.UI.RadLabel()
        Me.txt_assessorname = New common.Controls.MyTextBox()
        Me.RadLabel40 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel41 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel42 = New Telerik.WinControls.UI.RadLabel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.txt_cust10 = New common.Controls.MyTextBox()
        Me.txt_cust5 = New common.Controls.MyTextBox()
        Me.txt_cust9 = New common.Controls.MyTextBox()
        Me.txt_cust4 = New common.Controls.MyTextBox()
        Me.RadLabel23 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel22 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel24 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel16 = New Telerik.WinControls.UI.RadLabel()
        Me.txt_cust8 = New common.Controls.MyTextBox()
        Me.RadLabel18 = New Telerik.WinControls.UI.RadLabel()
        Me.txt_cust7 = New common.Controls.MyTextBox()
        Me.RadLabel17 = New Telerik.WinControls.UI.RadLabel()
        Me.txt_cust6 = New common.Controls.MyTextBox()
        Me.txt_cust3 = New common.Controls.MyTextBox()
        Me.RadLabel25 = New Telerik.WinControls.UI.RadLabel()
        Me.txt_cust2 = New common.Controls.MyTextBox()
        Me.RadLabel26 = New Telerik.WinControls.UI.RadLabel()
        Me.txt_cust1 = New common.Controls.MyTextBox()
        Me.RadLabel27 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel19 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel20 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel21 = New Telerik.WinControls.UI.RadLabel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rb_notapproved = New Telerik.WinControls.UI.RadRadioButton()
        Me.rb_approved = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadLabel10 = New Telerik.WinControls.UI.RadLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnclear_approved = New Telerik.WinControls.UI.RadButton()
        Me.btnselectsign_approved = New Telerik.WinControls.UI.RadButton()
        Me.pb_approved = New System.Windows.Forms.PictureBox()
        Me.dtp_approveddate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadLabel9 = New Telerik.WinControls.UI.RadLabel()
        Me.txt_approveddesig = New common.Controls.MyTextBox()
        Me.RadLabel8 = New Telerik.WinControls.UI.RadLabel()
        Me.txt_approvedname = New common.Controls.MyTextBox()
        Me.RadLabel7 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel6 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel5 = New Telerik.WinControls.UI.RadLabel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel()
        Me.txt_visitorname = New common.Controls.MyTextBox()
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txt_suitableforvendor = New Telerik.WinControls.UI.RadTextBox()
        Me.txt_suitablefor = New Telerik.WinControls.UI.RadTextBox()
        Me.RadLabel4 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel3 = New Telerik.WinControls.UI.RadLabel()
        Me.btn_VenRegApprove = New Telerik.WinControls.UI.RadButton()
        Me.btn_print = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.RadGridView1 = New Telerik.WinControls.UI.MasterGridViewTemplate()
        Me.UsLock1 = New common.usLock()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadPageView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView2.SuspendLayout()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtManufacturing_facilities, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox10.SuspendLayout()
        CType(Me.txtdetails_orgstructure, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTurnOver, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox9.SuspendLayout()
        CType(Me.txtContactPPhone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtContactPName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFaxNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPhoneNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        CType(Me.rbtn_subcontractor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox7.SuspendLayout()
        CType(Me.rbtn_ownership, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtn_govt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtn_public, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtn_pvt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName_Owners, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAddress1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChillingVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAddress2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gvtesting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvtesting.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_captive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_captivedetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvmanufacturing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvmanufacturing.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_PackingDefined, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_definedQP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_PackingDefinedDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_FacilitiesAvailable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_definedQPdetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_ProStorageSys, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_equipcalperiodically, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_RMIdentified, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_tmppermdevRecord, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_KeepRecord, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_ProInsAvailable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_InsDespatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_whoisauthorised, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_RMInsAvailable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_fulldocqsavailable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_stddrawingsavailable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_separatesection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage6.SuspendLayout()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.btnclear_vendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnselectsign_vendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_vendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtp_vendorsigndate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_vendordesig, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_vendorname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel36, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_paymentterms, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel46, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_centralexciseregno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_salestaxregno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_nameofbanker, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel44, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel45, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel10.SuspendLayout()
        CType(Me.btnclear_assessor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnselectsign_assessor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_assessor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtp_assessorsigndate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_assessordesig, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_assessorname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel42, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.txt_cust10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_cust5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_cust9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_cust4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_cust8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_cust7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_cust6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_cust3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_cust2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_cust1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.rb_notapproved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rb_approved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnclear_approved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnselectsign_approved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_approved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtp_approveddate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_approveddesig, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_approvedname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_visitorname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txt_suitableforvendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_suitablefor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_VenRegApprove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_print, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1060, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        Me.RadMenu1.Visible = False
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Setting"
        Me.RadMenuItem1.AccessibleName = "Setting"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Save Layout"
        Me.RadMenuItem2.AccessibleName = "Save Layout"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Save Layout"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem3.AccessibleName = "Delete Layout"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Delete Layout"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_VenRegApprove)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_print)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1060, 596)
        Me.SplitContainer1.SplitterDistance = 560
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1060, 560)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadPageView2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(96.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1039, 512)
        Me.RadPageViewPage1.Text = "Filled by vendor"
        '
        'RadPageView2
        '
        Me.RadPageView2.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView2.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView2.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView2.Controls.Add(Me.RadPageViewPage6)
        Me.RadPageView2.DefaultPage = Me.RadPageViewPage3
        Me.RadPageView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView2.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView2.Name = "RadPageView2"
        Me.RadPageView2.SelectedPage = Me.RadPageViewPage3
        Me.RadPageView2.Size = New System.Drawing.Size(1039, 512)
        Me.RadPageView2.TabIndex = 0
        Me.RadPageView2.Text = "RadPageView2"
        CType(Me.RadPageView2.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage3.Controls.Add(Me.btnNew)
        Me.RadPageViewPage3.Controls.Add(Me.txtManufacturing_facilities)
        Me.RadPageViewPage3.Controls.Add(Me.RadLabel11)
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox10)
        Me.RadPageViewPage3.Controls.Add(Me.txtTurnOver)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox9)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel19)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel17)
        Me.RadPageViewPage3.Controls.Add(Me.txtFaxNo)
        Me.RadPageViewPage3.Controls.Add(Me.txtPhoneNo)
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox8)
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox7)
        Me.RadPageViewPage3.Controls.Add(Me.txtName_Owners)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage3.Controls.Add(Me.txtAddress1)
        Me.RadPageViewPage3.Controls.Add(Me.txtName)
        Me.RadPageViewPage3.Controls.Add(Me.lblMCCCode)
        Me.RadPageViewPage3.Controls.Add(Me.txtAddress2)
        Me.RadPageViewPage3.Controls.Add(Me.txtcode)
        Me.RadPageViewPage3.Controls.Add(Me.lblChillingVendor)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel44)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(90.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(1018, 464)
        Me.RadPageViewPage3.Text = "Vendor Details"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(635, 0)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(16, 21)
        Me.btnNew.TabIndex = 138
        Me.btnNew.Text = " "
        '
        'txtManufacturing_facilities
        '
        Me.txtManufacturing_facilities.AutoSize = False
        Me.txtManufacturing_facilities.Location = New System.Drawing.Point(164, 391)
        Me.txtManufacturing_facilities.Multiline = True
        Me.txtManufacturing_facilities.Name = "txtManufacturing_facilities"
        Me.txtManufacturing_facilities.Size = New System.Drawing.Size(491, 70)
        Me.txtManufacturing_facilities.TabIndex = 137
        '
        'RadLabel11
        '
        Me.RadLabel11.Location = New System.Drawing.Point(9, 390)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(155, 18)
        Me.RadLabel11.TabIndex = 136
        Me.RadLabel11.Text = "MANUFACTURING FACILITIES"
        '
        'RadGroupBox10
        '
        Me.RadGroupBox10.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox10.Controls.Add(Me.txtdetails_orgstructure)
        Me.RadGroupBox10.HeaderText = "GIVE DETAILS OF ORGANISATIONAL STRUCTURE: (please attach company profile)"
        Me.RadGroupBox10.Location = New System.Drawing.Point(9, 267)
        Me.RadGroupBox10.Name = "RadGroupBox10"
        Me.RadGroupBox10.Size = New System.Drawing.Size(646, 117)
        Me.RadGroupBox10.TabIndex = 135
        Me.RadGroupBox10.Text = "GIVE DETAILS OF ORGANISATIONAL STRUCTURE: (please attach company profile)"
        '
        'txtdetails_orgstructure
        '
        Me.txtdetails_orgstructure.AutoSize = False
        Me.txtdetails_orgstructure.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtdetails_orgstructure.Location = New System.Drawing.Point(2, 18)
        Me.txtdetails_orgstructure.Multiline = True
        Me.txtdetails_orgstructure.Name = "txtdetails_orgstructure"
        Me.txtdetails_orgstructure.Size = New System.Drawing.Size(642, 97)
        Me.txtdetails_orgstructure.TabIndex = 5
        '
        'txtTurnOver
        '
        Me.txtTurnOver.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtTurnOver.CalculationExpression = Nothing
        Me.txtTurnOver.FieldCode = Nothing
        Me.txtTurnOver.FieldDesc = Nothing
        Me.txtTurnOver.FieldMaxLength = 0
        Me.txtTurnOver.FieldName = Nothing
        Me.txtTurnOver.isCalculatedField = False
        Me.txtTurnOver.IsSourceFromTable = False
        Me.txtTurnOver.IsSourceFromValueList = False
        Me.txtTurnOver.IsUnique = False
        Me.txtTurnOver.Location = New System.Drawing.Point(171, 243)
        Me.txtTurnOver.MaxLength = 20
        Me.txtTurnOver.MendatroryField = False
        Me.txtTurnOver.MyLinkLable1 = Me.MyLabel4
        Me.txtTurnOver.MyLinkLable2 = Nothing
        Me.txtTurnOver.Name = "txtTurnOver"
        Me.txtTurnOver.ReferenceFieldDesc = Nothing
        Me.txtTurnOver.ReferenceFieldName = Nothing
        Me.txtTurnOver.ReferenceTableName = Nothing
        Me.txtTurnOver.Size = New System.Drawing.Size(197, 20)
        Me.txtTurnOver.TabIndex = 134
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(9, 245)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(73, 16)
        Me.MyLabel4.TabIndex = 133
        Me.MyLabel4.Text = "TURN OVER"
        '
        'RadGroupBox9
        '
        Me.RadGroupBox9.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox9.Controls.Add(Me.txtContactPPhone)
        Me.RadGroupBox9.Controls.Add(Me.txtContactPName)
        Me.RadGroupBox9.Controls.Add(Me.MyLabel39)
        Me.RadGroupBox9.Controls.Add(Me.MyLabel37)
        Me.RadGroupBox9.HeaderText = "CONTACT PERSON"
        Me.RadGroupBox9.Location = New System.Drawing.Point(374, 196)
        Me.RadGroupBox9.Name = "RadGroupBox9"
        Me.RadGroupBox9.Size = New System.Drawing.Size(281, 67)
        Me.RadGroupBox9.TabIndex = 132
        Me.RadGroupBox9.Text = "CONTACT PERSON"
        '
        'txtContactPPhone
        '
        Me.txtContactPPhone.Location = New System.Drawing.Point(90, 41)
        Me.txtContactPPhone.Mask = "(+99)0000000000"
        Me.txtContactPPhone.MaskType = Telerik.WinControls.UI.MaskType.Standard
        Me.txtContactPPhone.Name = "txtContactPPhone"
        Me.txtContactPPhone.Size = New System.Drawing.Size(186, 20)
        Me.txtContactPPhone.TabIndex = 132
        Me.txtContactPPhone.TabStop = False
        Me.txtContactPPhone.Text = "(+__)__________"
        '
        'txtContactPName
        '
        Me.txtContactPName.CalculationExpression = Nothing
        Me.txtContactPName.FieldCode = Nothing
        Me.txtContactPName.FieldDesc = Nothing
        Me.txtContactPName.FieldMaxLength = 0
        Me.txtContactPName.FieldName = Nothing
        Me.txtContactPName.isCalculatedField = False
        Me.txtContactPName.IsSourceFromTable = False
        Me.txtContactPName.IsSourceFromValueList = False
        Me.txtContactPName.IsUnique = False
        Me.txtContactPName.Location = New System.Drawing.Point(90, 19)
        Me.txtContactPName.MaxLength = 100
        Me.txtContactPName.MendatroryField = False
        Me.txtContactPName.MyLinkLable1 = Me.MyLabel39
        Me.txtContactPName.MyLinkLable2 = Nothing
        Me.txtContactPName.Name = "txtContactPName"
        Me.txtContactPName.ReferenceFieldDesc = Nothing
        Me.txtContactPName.ReferenceFieldName = Nothing
        Me.txtContactPName.ReferenceTableName = Nothing
        Me.txtContactPName.Size = New System.Drawing.Size(186, 20)
        Me.txtContactPName.TabIndex = 129
        '
        'MyLabel39
        '
        Me.MyLabel39.FieldName = Nothing
        Me.MyLabel39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel39.Location = New System.Drawing.Point(5, 21)
        Me.MyLabel39.Name = "MyLabel39"
        Me.MyLabel39.Size = New System.Drawing.Size(39, 16)
        Me.MyLabel39.TabIndex = 130
        Me.MyLabel39.Text = "NAME"
        '
        'MyLabel37
        '
        Me.MyLabel37.FieldName = Nothing
        Me.MyLabel37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel37.Location = New System.Drawing.Point(5, 45)
        Me.MyLabel37.Name = "MyLabel37"
        Me.MyLabel37.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel37.TabIndex = 131
        Me.MyLabel37.Text = "PHONE NO."
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(9, 223)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel19.TabIndex = 130
        Me.MyLabel19.Text = "FAX NO."
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(9, 202)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel17.TabIndex = 129
        Me.MyLabel17.Text = "PHONE NO."
        '
        'txtFaxNo
        '
        Me.txtFaxNo.CalculationExpression = Nothing
        Me.txtFaxNo.FieldCode = Nothing
        Me.txtFaxNo.FieldDesc = Nothing
        Me.txtFaxNo.FieldMaxLength = 0
        Me.txtFaxNo.FieldName = Nothing
        Me.txtFaxNo.isCalculatedField = False
        Me.txtFaxNo.IsSourceFromTable = False
        Me.txtFaxNo.IsSourceFromValueList = False
        Me.txtFaxNo.IsUnique = False
        Me.txtFaxNo.Location = New System.Drawing.Point(171, 221)
        Me.txtFaxNo.MaxLength = 20
        Me.txtFaxNo.MendatroryField = False
        Me.txtFaxNo.MyLinkLable1 = Me.MyLabel19
        Me.txtFaxNo.MyLinkLable2 = Nothing
        Me.txtFaxNo.Name = "txtFaxNo"
        Me.txtFaxNo.ReferenceFieldDesc = Nothing
        Me.txtFaxNo.ReferenceFieldName = Nothing
        Me.txtFaxNo.ReferenceTableName = Nothing
        Me.txtFaxNo.Size = New System.Drawing.Size(197, 20)
        Me.txtFaxNo.TabIndex = 131
        '
        'txtPhoneNo
        '
        Me.txtPhoneNo.Location = New System.Drawing.Point(171, 199)
        Me.txtPhoneNo.Mask = "(+99)0000000000"
        Me.txtPhoneNo.MaskType = Telerik.WinControls.UI.MaskType.Standard
        Me.txtPhoneNo.Name = "txtPhoneNo"
        Me.txtPhoneNo.Size = New System.Drawing.Size(197, 20)
        Me.txtPhoneNo.TabIndex = 128
        Me.txtPhoneNo.TabStop = False
        Me.txtPhoneNo.Text = "(+__)__________"
        '
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Controls.Add(Me.rbtn_subcontractor)
        Me.RadGroupBox8.HeaderText = "ORGANISATIONAL SETUP"
        Me.RadGroupBox8.Location = New System.Drawing.Point(374, 115)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Size = New System.Drawing.Size(281, 78)
        Me.RadGroupBox8.TabIndex = 127
        Me.RadGroupBox8.Text = "ORGANISATIONAL SETUP"
        '
        'rbtn_subcontractor
        '
        Me.rbtn_subcontractor.Location = New System.Drawing.Point(5, 34)
        Me.rbtn_subcontractor.Name = "rbtn_subcontractor"
        Me.rbtn_subcontractor.Size = New System.Drawing.Size(207, 18)
        Me.rbtn_subcontractor.TabIndex = 4
        Me.rbtn_subcontractor.TabStop = False
        Me.rbtn_subcontractor.Text = "MANUFACTURED SUB CONTRACTOR"
        '
        'RadGroupBox7
        '
        Me.RadGroupBox7.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox7.Controls.Add(Me.rbtn_ownership)
        Me.RadGroupBox7.Controls.Add(Me.rbtn_govt)
        Me.RadGroupBox7.Controls.Add(Me.rbtn_public)
        Me.RadGroupBox7.Controls.Add(Me.rbtn_pvt)
        Me.RadGroupBox7.HeaderText = "KIND OF ORGANIZATION"
        Me.RadGroupBox7.Location = New System.Drawing.Point(9, 115)
        Me.RadGroupBox7.Name = "RadGroupBox7"
        Me.RadGroupBox7.Size = New System.Drawing.Size(359, 78)
        Me.RadGroupBox7.TabIndex = 126
        Me.RadGroupBox7.Text = "KIND OF ORGANIZATION"
        '
        'rbtn_ownership
        '
        Me.rbtn_ownership.Location = New System.Drawing.Point(162, 45)
        Me.rbtn_ownership.Name = "rbtn_ownership"
        Me.rbtn_ownership.Size = New System.Drawing.Size(187, 18)
        Me.rbtn_ownership.TabIndex = 6
        Me.rbtn_ownership.TabStop = False
        Me.rbtn_ownership.Text = "PVT. OWNERSHIP/ PARTNERSHIP"
        '
        'rbtn_govt
        '
        Me.rbtn_govt.Location = New System.Drawing.Point(162, 21)
        Me.rbtn_govt.Name = "rbtn_govt"
        Me.rbtn_govt.Size = New System.Drawing.Size(131, 18)
        Me.rbtn_govt.TabIndex = 5
        Me.rbtn_govt.TabStop = False
        Me.rbtn_govt.Text = "GOVT. UNDERTAKING"
        '
        'rbtn_public
        '
        Me.rbtn_public.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtn_public.Location = New System.Drawing.Point(5, 45)
        Me.rbtn_public.Name = "rbtn_public"
        Me.rbtn_public.Size = New System.Drawing.Size(81, 18)
        Me.rbtn_public.TabIndex = 5
        Me.rbtn_public.TabStop = False
        Me.rbtn_public.Text = "PUBLIC LTD."
        Me.rbtn_public.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtn_pvt
        '
        Me.rbtn_pvt.Location = New System.Drawing.Point(5, 21)
        Me.rbtn_pvt.Name = "rbtn_pvt"
        Me.rbtn_pvt.Size = New System.Drawing.Size(87, 18)
        Me.rbtn_pvt.TabIndex = 4
        Me.rbtn_pvt.TabStop = False
        Me.rbtn_pvt.Text = "PRIVATE LTD."
        '
        'txtName_Owners
        '
        Me.txtName_Owners.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtName_Owners.CalculationExpression = Nothing
        Me.txtName_Owners.FieldCode = Nothing
        Me.txtName_Owners.FieldDesc = Nothing
        Me.txtName_Owners.FieldMaxLength = 0
        Me.txtName_Owners.FieldName = Nothing
        Me.txtName_Owners.isCalculatedField = False
        Me.txtName_Owners.IsSourceFromTable = False
        Me.txtName_Owners.IsSourceFromValueList = False
        Me.txtName_Owners.IsUnique = False
        Me.txtName_Owners.Location = New System.Drawing.Point(235, 89)
        Me.txtName_Owners.MaxLength = 200
        Me.txtName_Owners.MendatroryField = False
        Me.txtName_Owners.MyLinkLable1 = Me.MyLabel3
        Me.txtName_Owners.MyLinkLable2 = Nothing
        Me.txtName_Owners.Name = "txtName_Owners"
        Me.txtName_Owners.ReferenceFieldDesc = Nothing
        Me.txtName_Owners.ReferenceFieldName = Nothing
        Me.txtName_Owners.ReferenceTableName = Nothing
        Me.txtName_Owners.Size = New System.Drawing.Size(420, 20)
        Me.txtName_Owners.TabIndex = 125
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(9, 90)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(193, 16)
        Me.MyLabel3.TabIndex = 124
        Me.MyLabel3.Text = "NAME OF PROPRIETOR/ OWNERS"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(9, 67)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel2.TabIndex = 123
        Me.MyLabel2.Text = "ADRESS LINE 2"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(9, 45)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel1.TabIndex = 122
        Me.MyLabel1.Text = "ADRESS LINE 1"
        '
        'txtAddress1
        '
        Me.txtAddress1.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtAddress1.CalculationExpression = Nothing
        Me.txtAddress1.FieldCode = Nothing
        Me.txtAddress1.FieldDesc = Nothing
        Me.txtAddress1.FieldMaxLength = 0
        Me.txtAddress1.FieldName = Nothing
        Me.txtAddress1.isCalculatedField = False
        Me.txtAddress1.IsSourceFromTable = False
        Me.txtAddress1.IsSourceFromValueList = False
        Me.txtAddress1.IsUnique = False
        Me.txtAddress1.Location = New System.Drawing.Point(235, 45)
        Me.txtAddress1.MaxLength = 200
        Me.txtAddress1.MendatroryField = False
        Me.txtAddress1.MyLinkLable1 = Me.lblChillingVendor
        Me.txtAddress1.MyLinkLable2 = Nothing
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.ReferenceFieldDesc = Nothing
        Me.txtAddress1.ReferenceFieldName = Nothing
        Me.txtAddress1.ReferenceTableName = Nothing
        Me.txtAddress1.Size = New System.Drawing.Size(420, 20)
        Me.txtAddress1.TabIndex = 123
        '
        'lblChillingVendor
        '
        Me.lblChillingVendor.FieldName = Nothing
        Me.lblChillingVendor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChillingVendor.Location = New System.Drawing.Point(9, 47)
        Me.lblChillingVendor.Name = "lblChillingVendor"
        Me.lblChillingVendor.Size = New System.Drawing.Size(2, 2)
        Me.lblChillingVendor.TabIndex = 120
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtName.CalculationExpression = Nothing
        Me.txtName.FieldCode = Nothing
        Me.txtName.FieldDesc = Nothing
        Me.txtName.FieldMaxLength = 0
        Me.txtName.FieldName = Nothing
        Me.txtName.isCalculatedField = False
        Me.txtName.IsSourceFromTable = False
        Me.txtName.IsSourceFromValueList = False
        Me.txtName.IsUnique = False
        Me.txtName.Location = New System.Drawing.Point(235, 23)
        Me.txtName.MaxLength = 200
        Me.txtName.MendatroryField = False
        Me.txtName.MyLinkLable1 = Me.MyLabel44
        Me.txtName.MyLinkLable2 = Nothing
        Me.txtName.Name = "txtName"
        Me.txtName.ReferenceFieldDesc = Nothing
        Me.txtName.ReferenceFieldName = Nothing
        Me.txtName.ReferenceTableName = Nothing
        Me.txtName.Size = New System.Drawing.Size(420, 20)
        Me.txtName.TabIndex = 122
        '
        'MyLabel44
        '
        Me.MyLabel44.FieldName = Nothing
        Me.MyLabel44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel44.Location = New System.Drawing.Point(9, 24)
        Me.MyLabel44.Name = "MyLabel44"
        Me.MyLabel44.Size = New System.Drawing.Size(39, 16)
        Me.MyLabel44.TabIndex = 121
        Me.MyLabel44.Text = "NAME"
        '
        'lblMCCCode
        '
        Me.lblMCCCode.FieldName = Nothing
        Me.lblMCCCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblMCCCode.Location = New System.Drawing.Point(9, 1)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(115, 16)
        Me.lblMCCCode.TabIndex = 119
        Me.lblMCCCode.Text = "REGISTRATION NO."
        '
        'txtAddress2
        '
        Me.txtAddress2.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtAddress2.CalculationExpression = Nothing
        Me.txtAddress2.FieldCode = Nothing
        Me.txtAddress2.FieldDesc = Nothing
        Me.txtAddress2.FieldMaxLength = 0
        Me.txtAddress2.FieldName = Nothing
        Me.txtAddress2.isCalculatedField = False
        Me.txtAddress2.IsSourceFromTable = False
        Me.txtAddress2.IsSourceFromValueList = False
        Me.txtAddress2.IsUnique = False
        Me.txtAddress2.Location = New System.Drawing.Point(235, 67)
        Me.txtAddress2.MaxLength = 200
        Me.txtAddress2.MendatroryField = False
        Me.txtAddress2.MyLinkLable1 = Me.lblChillingVendor
        Me.txtAddress2.MyLinkLable2 = Nothing
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.ReferenceFieldDesc = Nothing
        Me.txtAddress2.ReferenceFieldName = Nothing
        Me.txtAddress2.ReferenceTableName = Nothing
        Me.txtAddress2.Size = New System.Drawing.Size(420, 20)
        Me.txtAddress2.TabIndex = 118
        '
        'txtcode
        '
        Me.txtcode.FieldName = Nothing
        Me.txtcode.Location = New System.Drawing.Point(235, 0)
        Me.txtcode.MendatroryField = True
        Me.txtcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcode.MyLinkLable1 = Me.lblMCCCode
        Me.txtcode.MyLinkLable2 = Nothing
        Me.txtcode.MyMaxLength = 30
        Me.txtcode.MyReadOnly = False
        Me.txtcode.Name = "txtcode"
        Me.txtcode.Size = New System.Drawing.Size(399, 21)
        Me.txtcode.TabIndex = 117
        Me.txtcode.Value = ""
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage4.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(105.0!, 28.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(1018, 464)
        Me.RadPageViewPage4.Text = "Machinery Details"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gvtesting)
        Me.RadGroupBox4.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadGroupBox4.HeaderText = "LIST OF INSPECTION & TEST INSTRUMENTS/ EQUIPMENT :"
        Me.RadGroupBox4.Location = New System.Drawing.Point(0, 256)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Size = New System.Drawing.Size(1018, 208)
        Me.RadGroupBox4.TabIndex = 1
        Me.RadGroupBox4.Text = "LIST OF INSPECTION & TEST INSTRUMENTS/ EQUIPMENT :"
        '
        'gvtesting
        '
        Me.gvtesting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvtesting.Location = New System.Drawing.Point(2, 18)
        Me.gvtesting.Name = "gvtesting"
        Me.gvtesting.Size = New System.Drawing.Size(1014, 188)
        Me.gvtesting.TabIndex = 0
        Me.gvtesting.Text = "RadGridView3"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel15)
        Me.RadGroupBox3.Controls.Add(Me.chk_captive)
        Me.RadGroupBox3.Controls.Add(Me.txt_captivedetails)
        Me.RadGroupBox3.Controls.Add(Me.gvmanufacturing)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadGroupBox3.HeaderText = "LIST OF MACHINERY & EQUIPMENT : (FOR MANUFACTURING)"
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(1018, 256)
        Me.RadGroupBox3.TabIndex = 0
        Me.RadGroupBox3.Text = "LIST OF MACHINERY & EQUIPMENT : (FOR MANUFACTURING)"
        '
        'RadLabel15
        '
        Me.RadLabel15.Location = New System.Drawing.Point(5, 231)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(109, 18)
        Me.RadLabel15.TabIndex = 41
        Me.RadLabel15.Text = "If yes, please specify."
        '
        'chk_captive
        '
        Me.chk_captive.Location = New System.Drawing.Point(5, 213)
        Me.chk_captive.Name = "chk_captive"
        Me.chk_captive.Size = New System.Drawing.Size(216, 18)
        Me.chk_captive.TabIndex = 40
        Me.chk_captive.Text = "Do you have captive power (Generator)"
        '
        'txt_captivedetails
        '
        Me.txt_captivedetails.AutoSize = False
        Me.txt_captivedetails.Location = New System.Drawing.Point(123, 231)
        Me.txt_captivedetails.Multiline = True
        Me.txt_captivedetails.Name = "txt_captivedetails"
        Me.txt_captivedetails.Size = New System.Drawing.Size(893, 23)
        Me.txt_captivedetails.TabIndex = 39
        '
        'gvmanufacturing
        '
        Me.gvmanufacturing.Dock = System.Windows.Forms.DockStyle.Top
        Me.gvmanufacturing.Location = New System.Drawing.Point(2, 18)
        Me.gvmanufacturing.Name = "gvmanufacturing"
        Me.gvmanufacturing.Size = New System.Drawing.Size(1014, 189)
        Me.gvmanufacturing.TabIndex = 0
        Me.gvmanufacturing.Text = "RadGridView2"
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.RadLabel14)
        Me.RadPageViewPage5.Controls.Add(Me.RadLabel13)
        Me.RadPageViewPage5.Controls.Add(Me.chk_PackingDefined)
        Me.RadPageViewPage5.Controls.Add(Me.chk_definedQP)
        Me.RadPageViewPage5.Controls.Add(Me.txt_PackingDefinedDetails)
        Me.RadPageViewPage5.Controls.Add(Me.chk_FacilitiesAvailable)
        Me.RadPageViewPage5.Controls.Add(Me.txt_definedQPdetails)
        Me.RadPageViewPage5.Controls.Add(Me.chk_ProStorageSys)
        Me.RadPageViewPage5.Controls.Add(Me.chk_equipcalperiodically)
        Me.RadPageViewPage5.Controls.Add(Me.chk_RMIdentified)
        Me.RadPageViewPage5.Controls.Add(Me.chk_tmppermdevRecord)
        Me.RadPageViewPage5.Controls.Add(Me.chk_KeepRecord)
        Me.RadPageViewPage5.Controls.Add(Me.chk_ProInsAvailable)
        Me.RadPageViewPage5.Controls.Add(Me.chk_InsDespatch)
        Me.RadPageViewPage5.Controls.Add(Me.txt_whoisauthorised)
        Me.RadPageViewPage5.Controls.Add(Me.RadLabel12)
        Me.RadPageViewPage5.Controls.Add(Me.chk_RMInsAvailable)
        Me.RadPageViewPage5.Controls.Add(Me.chk_fulldocqsavailable)
        Me.RadPageViewPage5.Controls.Add(Me.chk_stddrawingsavailable)
        Me.RadPageViewPage5.Controls.Add(Me.chk_separatesection)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(107.0!, 28.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(1018, 464)
        Me.RadPageViewPage5.Text = "Other Information"
        '
        'RadLabel14
        '
        Me.RadLabel14.Location = New System.Drawing.Point(9, 340)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(114, 18)
        Me.RadLabel14.TabIndex = 38
        Me.RadLabel14.Text = "If yes, indicate briefly."
        '
        'RadLabel13
        '
        Me.RadLabel13.Location = New System.Drawing.Point(9, 232)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(114, 18)
        Me.RadLabel13.TabIndex = 32
        Me.RadLabel13.Text = "If yes, indicate briefly."
        '
        'chk_PackingDefined
        '
        Me.chk_PackingDefined.Location = New System.Drawing.Point(9, 322)
        Me.chk_PackingDefined.Name = "chk_PackingDefined"
        Me.chk_PackingDefined.Size = New System.Drawing.Size(279, 18)
        Me.chk_PackingDefined.TabIndex = 37
        Me.chk_PackingDefined.Text = "Are packing, storage and handling system defined ?"
        '
        'chk_definedQP
        '
        Me.chk_definedQP.Location = New System.Drawing.Point(9, 214)
        Me.chk_definedQP.Name = "chk_definedQP"
        Me.chk_definedQP.Size = New System.Drawing.Size(186, 18)
        Me.chk_definedQP.TabIndex = 31
        Me.chk_definedQP.Text = "Is there a defined Quality Policy ?"
        '
        'txt_PackingDefinedDetails
        '
        Me.txt_PackingDefinedDetails.AutoSize = False
        Me.txt_PackingDefinedDetails.Location = New System.Drawing.Point(127, 340)
        Me.txt_PackingDefinedDetails.Multiline = True
        Me.txt_PackingDefinedDetails.Name = "txt_PackingDefinedDetails"
        Me.txt_PackingDefinedDetails.Size = New System.Drawing.Size(417, 36)
        Me.txt_PackingDefinedDetails.TabIndex = 36
        '
        'chk_FacilitiesAvailable
        '
        Me.chk_FacilitiesAvailable.Location = New System.Drawing.Point(9, 304)
        Me.chk_FacilitiesAvailable.Name = "chk_FacilitiesAvailable"
        Me.chk_FacilitiesAvailable.Size = New System.Drawing.Size(329, 18)
        Me.chk_FacilitiesAvailable.TabIndex = 35
        Me.chk_FacilitiesAvailable.Text = "Are facilities available for collection and delivery of materials ?"
        '
        'txt_definedQPdetails
        '
        Me.txt_definedQPdetails.AutoSize = False
        Me.txt_definedQPdetails.Location = New System.Drawing.Point(127, 232)
        Me.txt_definedQPdetails.Multiline = True
        Me.txt_definedQPdetails.Name = "txt_definedQPdetails"
        Me.txt_definedQPdetails.Size = New System.Drawing.Size(417, 36)
        Me.txt_definedQPdetails.TabIndex = 30
        '
        'chk_ProStorageSys
        '
        Me.chk_ProStorageSys.Location = New System.Drawing.Point(9, 268)
        Me.chk_ProStorageSys.Name = "chk_ProStorageSys"
        Me.chk_ProStorageSys.Size = New System.Drawing.Size(215, 18)
        Me.chk_ProStorageSys.TabIndex = 33
        Me.chk_ProStorageSys.Text = "Do you have a proper storage system ?"
        '
        'chk_equipcalperiodically
        '
        Me.chk_equipcalperiodically.Location = New System.Drawing.Point(9, 196)
        Me.chk_equipcalperiodically.Name = "chk_equipcalperiodically"
        Me.chk_equipcalperiodically.Size = New System.Drawing.Size(379, 18)
        Me.chk_equipcalperiodically.TabIndex = 29
        Me.chk_equipcalperiodically.Text = "Are Inspection, Measuring and Test equipments calibrated periodically ?"
        '
        'chk_RMIdentified
        '
        Me.chk_RMIdentified.Location = New System.Drawing.Point(9, 286)
        Me.chk_RMIdentified.Name = "chk_RMIdentified"
        Me.chk_RMIdentified.Size = New System.Drawing.Size(391, 18)
        Me.chk_RMIdentified.TabIndex = 34
        Me.chk_RMIdentified.Text = "Is Raw Material identified in stores to prevent the use of wrong materials ?"
        '
        'chk_tmppermdevRecord
        '
        Me.chk_tmppermdevRecord.Location = New System.Drawing.Point(9, 178)
        Me.chk_tmppermdevRecord.Name = "chk_tmppermdevRecord"
        Me.chk_tmppermdevRecord.Size = New System.Drawing.Size(401, 18)
        Me.chk_tmppermdevRecord.TabIndex = 28
        Me.chk_tmppermdevRecord.Text = "Do you maintain record of temporary and permanent deviation, if required ?"
        '
        'chk_KeepRecord
        '
        Me.chk_KeepRecord.Location = New System.Drawing.Point(9, 160)
        Me.chk_KeepRecord.Name = "chk_KeepRecord"
        Me.chk_KeepRecord.Size = New System.Drawing.Size(237, 18)
        Me.chk_KeepRecord.TabIndex = 26
        Me.chk_KeepRecord.Text = "If yes, do you keep record for each supply ?"
        '
        'chk_ProInsAvailable
        '
        Me.chk_ProInsAvailable.Location = New System.Drawing.Point(9, 124)
        Me.chk_ProInsAvailable.Name = "chk_ProInsAvailable"
        Me.chk_ProInsAvailable.Size = New System.Drawing.Size(309, 18)
        Me.chk_ProInsAvailable.TabIndex = 24
        Me.chk_ProInsAvailable.Text = "Is there Process Inspection && recording system available ?"
        '
        'chk_InsDespatch
        '
        Me.chk_InsDespatch.Location = New System.Drawing.Point(9, 142)
        Me.chk_InsDespatch.Name = "chk_InsDespatch"
        Me.chk_InsDespatch.Size = New System.Drawing.Size(309, 18)
        Me.chk_InsDespatch.TabIndex = 25
        Me.chk_InsDespatch.Text = "Do you have a system of Lot Inspection before despatch ?"
        '
        'txt_whoisauthorised
        '
        Me.txt_whoisauthorised.AutoSize = False
        Me.txt_whoisauthorised.Location = New System.Drawing.Point(9, 34)
        Me.txt_whoisauthorised.Multiline = True
        Me.txt_whoisauthorised.Name = "txt_whoisauthorised"
        Me.txt_whoisauthorised.Size = New System.Drawing.Size(537, 36)
        Me.txt_whoisauthorised.TabIndex = 23
        '
        'RadLabel12
        '
        Me.RadLabel12.Location = New System.Drawing.Point(9, 16)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(540, 18)
        Me.RadLabel12.TabIndex = 22
        Me.RadLabel12.Text = "Who is authorised to take final decision in case product/component does not meet " &
    "Specification/Drawings."
        '
        'chk_RMInsAvailable
        '
        Me.chk_RMInsAvailable.Location = New System.Drawing.Point(9, 106)
        Me.chk_RMInsAvailable.Name = "chk_RMInsAvailable"
        Me.chk_RMInsAvailable.Size = New System.Drawing.Size(323, 18)
        Me.chk_RMInsAvailable.TabIndex = 21
        Me.chk_RMInsAvailable.Text = "Is there Incoming Raw Material Inspection System available ?"
        '
        'chk_fulldocqsavailable
        '
        Me.chk_fulldocqsavailable.Location = New System.Drawing.Point(9, 70)
        Me.chk_fulldocqsavailable.Name = "chk_fulldocqsavailable"
        Me.chk_fulldocqsavailable.Size = New System.Drawing.Size(276, 18)
        Me.chk_fulldocqsavailable.TabIndex = 19
        Me.chk_fulldocqsavailable.Text = "Is there full documented Quality System Available ?"
        '
        'chk_stddrawingsavailable
        '
        Me.chk_stddrawingsavailable.Location = New System.Drawing.Point(9, 88)
        Me.chk_stddrawingsavailable.Name = "chk_stddrawingsavailable"
        Me.chk_stddrawingsavailable.Size = New System.Drawing.Size(323, 18)
        Me.chk_stddrawingsavailable.TabIndex = 20
        Me.chk_stddrawingsavailable.Text = "Are reference standards, drawings available and kept safely ?"
        '
        'chk_separatesection
        '
        Me.chk_separatesection.Location = New System.Drawing.Point(9, -2)
        Me.chk_separatesection.Name = "chk_separatesection"
        Me.chk_separatesection.Size = New System.Drawing.Size(326, 18)
        Me.chk_separatesection.TabIndex = 18
        Me.chk_separatesection.Text = "Is there Separate section, Responsible for Quality Assurance ?"
        '
        'RadPageViewPage6
        '
        Me.RadPageViewPage6.Controls.Add(Me.RadGroupBox6)
        Me.RadPageViewPage6.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage6.ItemSize = New System.Drawing.SizeF(82.0!, 28.0!)
        Me.RadPageViewPage6.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage6.Name = "RadPageViewPage6"
        Me.RadPageViewPage6.Size = New System.Drawing.Size(1018, 464)
        Me.RadPageViewPage6.Text = "Other Details"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.Panel8)
        Me.RadGroupBox6.Controls.Add(Me.txt_paymentterms)
        Me.RadGroupBox6.Controls.Add(Me.RadLabel46)
        Me.RadGroupBox6.Controls.Add(Me.txt_centralexciseregno)
        Me.RadGroupBox6.Controls.Add(Me.txt_salestaxregno)
        Me.RadGroupBox6.Controls.Add(Me.txt_nameofbanker)
        Me.RadGroupBox6.Controls.Add(Me.RadLabel43)
        Me.RadGroupBox6.Controls.Add(Me.RadLabel44)
        Me.RadGroupBox6.Controls.Add(Me.RadLabel45)
        Me.RadGroupBox6.Controls.Add(Me.Panel10)
        Me.RadGroupBox6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox6.HeaderText = "COMMERCIAL CONDITION :"
        Me.RadGroupBox6.Location = New System.Drawing.Point(0, 173)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Size = New System.Drawing.Size(1018, 291)
        Me.RadGroupBox6.TabIndex = 126
        Me.RadGroupBox6.Text = "COMMERCIAL CONDITION :"
        '
        'Panel8
        '
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Controls.Add(Me.btnclear_vendor)
        Me.Panel8.Controls.Add(Me.btnselectsign_vendor)
        Me.Panel8.Controls.Add(Me.pb_vendor)
        Me.Panel8.Controls.Add(Me.dtp_vendorsigndate)
        Me.Panel8.Controls.Add(Me.RadLabel28)
        Me.Panel8.Controls.Add(Me.txt_vendordesig)
        Me.Panel8.Controls.Add(Me.RadLabel34)
        Me.Panel8.Controls.Add(Me.txt_vendorname)
        Me.Panel8.Controls.Add(Me.RadLabel35)
        Me.Panel8.Controls.Add(Me.RadLabel36)
        Me.Panel8.Controls.Add(Me.RadLabel37)
        Me.Panel8.Controls.Add(Me.Panel9)
        Me.Panel8.Location = New System.Drawing.Point(223, 108)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(262, 140)
        Me.Panel8.TabIndex = 129
        '
        'btnclear_vendor
        '
        Me.btnclear_vendor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnclear_vendor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear_vendor.Location = New System.Drawing.Point(198, 93)
        Me.btnclear_vendor.Name = "btnclear_vendor"
        Me.btnclear_vendor.Size = New System.Drawing.Size(55, 18)
        Me.btnclear_vendor.TabIndex = 131
        Me.btnclear_vendor.Text = "Clear"
        '
        'btnselectsign_vendor
        '
        Me.btnselectsign_vendor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnselectsign_vendor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnselectsign_vendor.Location = New System.Drawing.Point(198, 74)
        Me.btnselectsign_vendor.Name = "btnselectsign_vendor"
        Me.btnselectsign_vendor.Size = New System.Drawing.Size(55, 18)
        Me.btnselectsign_vendor.TabIndex = 130
        Me.btnselectsign_vendor.Text = "Select"
        '
        'pb_vendor
        '
        Me.pb_vendor.Location = New System.Drawing.Point(92, 74)
        Me.pb_vendor.Name = "pb_vendor"
        Me.pb_vendor.Size = New System.Drawing.Size(103, 37)
        Me.pb_vendor.TabIndex = 132
        Me.pb_vendor.TabStop = False
        '
        'dtp_vendorsigndate
        '
        Me.dtp_vendorsigndate.Location = New System.Drawing.Point(92, 113)
        Me.dtp_vendorsigndate.Name = "dtp_vendorsigndate"
        Me.dtp_vendorsigndate.Size = New System.Drawing.Size(161, 20)
        Me.dtp_vendorsigndate.TabIndex = 128
        Me.dtp_vendorsigndate.TabStop = False
        Me.dtp_vendorsigndate.Text = "Tuesday, October 25, 2016"
        Me.dtp_vendorsigndate.Value = New Date(2016, 10, 25, 13, 12, 53, 146)
        '
        'RadLabel28
        '
        Me.RadLabel28.Location = New System.Drawing.Point(3, 3)
        Me.RadLabel28.Name = "RadLabel28"
        Me.RadLabel28.Size = New System.Drawing.Size(121, 18)
        Me.RadLabel28.TabIndex = 127
        Me.RadLabel28.Text = "VENDOR'S SIGNATURE"
        '
        'txt_vendordesig
        '
        Me.txt_vendordesig.CalculationExpression = Nothing
        Me.txt_vendordesig.FieldCode = Nothing
        Me.txt_vendordesig.FieldDesc = Nothing
        Me.txt_vendordesig.FieldMaxLength = 0
        Me.txt_vendordesig.FieldName = Nothing
        Me.txt_vendordesig.isCalculatedField = False
        Me.txt_vendordesig.IsSourceFromTable = False
        Me.txt_vendordesig.IsSourceFromValueList = False
        Me.txt_vendordesig.IsUnique = False
        Me.txt_vendordesig.Location = New System.Drawing.Point(92, 52)
        Me.txt_vendordesig.MaxLength = 100
        Me.txt_vendordesig.MendatroryField = False
        Me.txt_vendordesig.MyLinkLable1 = Nothing
        Me.txt_vendordesig.MyLinkLable2 = Nothing
        Me.txt_vendordesig.Name = "txt_vendordesig"
        Me.txt_vendordesig.ReferenceFieldDesc = Nothing
        Me.txt_vendordesig.ReferenceFieldName = Nothing
        Me.txt_vendordesig.ReferenceTableName = Nothing
        Me.txt_vendordesig.Size = New System.Drawing.Size(161, 20)
        Me.txt_vendordesig.TabIndex = 125
        '
        'RadLabel34
        '
        Me.RadLabel34.Location = New System.Drawing.Point(3, 116)
        Me.RadLabel34.Name = "RadLabel34"
        Me.RadLabel34.Size = New System.Drawing.Size(33, 18)
        Me.RadLabel34.TabIndex = 5
        Me.RadLabel34.Text = "DATE"
        '
        'txt_vendorname
        '
        Me.txt_vendorname.CalculationExpression = Nothing
        Me.txt_vendorname.FieldCode = Nothing
        Me.txt_vendorname.FieldDesc = Nothing
        Me.txt_vendorname.FieldMaxLength = 0
        Me.txt_vendorname.FieldName = Nothing
        Me.txt_vendorname.isCalculatedField = False
        Me.txt_vendorname.IsSourceFromTable = False
        Me.txt_vendorname.IsSourceFromValueList = False
        Me.txt_vendorname.IsUnique = False
        Me.txt_vendorname.Location = New System.Drawing.Point(92, 30)
        Me.txt_vendorname.MaxLength = 100
        Me.txt_vendorname.MendatroryField = False
        Me.txt_vendorname.MyLinkLable1 = Nothing
        Me.txt_vendorname.MyLinkLable2 = Nothing
        Me.txt_vendorname.Name = "txt_vendorname"
        Me.txt_vendorname.ReferenceFieldDesc = Nothing
        Me.txt_vendorname.ReferenceFieldName = Nothing
        Me.txt_vendorname.ReferenceTableName = Nothing
        Me.txt_vendorname.Size = New System.Drawing.Size(161, 20)
        Me.txt_vendorname.TabIndex = 124
        '
        'RadLabel35
        '
        Me.RadLabel35.Location = New System.Drawing.Point(3, 76)
        Me.RadLabel35.Name = "RadLabel35"
        Me.RadLabel35.Size = New System.Drawing.Size(65, 18)
        Me.RadLabel35.TabIndex = 4
        Me.RadLabel35.Text = "SIGNATURE"
        '
        'RadLabel36
        '
        Me.RadLabel36.Location = New System.Drawing.Point(3, 53)
        Me.RadLabel36.Name = "RadLabel36"
        Me.RadLabel36.Size = New System.Drawing.Size(78, 18)
        Me.RadLabel36.TabIndex = 4
        Me.RadLabel36.Text = "DESIGNATION"
        '
        'RadLabel37
        '
        Me.RadLabel37.Location = New System.Drawing.Point(3, 30)
        Me.RadLabel37.Name = "RadLabel37"
        Me.RadLabel37.Size = New System.Drawing.Size(38, 18)
        Me.RadLabel37.TabIndex = 4
        Me.RadLabel37.Text = "NAME"
        '
        'Panel9
        '
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Location = New System.Drawing.Point(0, 24)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(260, 1)
        Me.Panel9.TabIndex = 2
        '
        'txt_paymentterms
        '
        Me.txt_paymentterms.CalculationExpression = Nothing
        Me.txt_paymentterms.FieldCode = Nothing
        Me.txt_paymentterms.FieldDesc = Nothing
        Me.txt_paymentterms.FieldMaxLength = 0
        Me.txt_paymentterms.FieldName = Nothing
        Me.txt_paymentterms.isCalculatedField = False
        Me.txt_paymentterms.IsSourceFromTable = False
        Me.txt_paymentterms.IsSourceFromValueList = False
        Me.txt_paymentterms.IsUnique = False
        Me.txt_paymentterms.Location = New System.Drawing.Point(223, 86)
        Me.txt_paymentterms.MaxLength = 100
        Me.txt_paymentterms.MendatroryField = False
        Me.txt_paymentterms.MyLinkLable1 = Nothing
        Me.txt_paymentterms.MyLinkLable2 = Nothing
        Me.txt_paymentterms.Name = "txt_paymentterms"
        Me.txt_paymentterms.ReferenceFieldDesc = Nothing
        Me.txt_paymentterms.ReferenceFieldName = Nothing
        Me.txt_paymentterms.ReferenceTableName = Nothing
        Me.txt_paymentterms.Size = New System.Drawing.Size(618, 20)
        Me.txt_paymentterms.TabIndex = 138
        '
        'RadLabel46
        '
        Me.RadLabel46.Location = New System.Drawing.Point(9, 87)
        Me.RadLabel46.Name = "RadLabel46"
        Me.RadLabel46.Size = New System.Drawing.Size(94, 18)
        Me.RadLabel46.TabIndex = 137
        Me.RadLabel46.Text = "PAYMENT TERMS"
        '
        'txt_centralexciseregno
        '
        Me.txt_centralexciseregno.CalculationExpression = Nothing
        Me.txt_centralexciseregno.FieldCode = Nothing
        Me.txt_centralexciseregno.FieldDesc = Nothing
        Me.txt_centralexciseregno.FieldMaxLength = 0
        Me.txt_centralexciseregno.FieldName = Nothing
        Me.txt_centralexciseregno.isCalculatedField = False
        Me.txt_centralexciseregno.IsSourceFromTable = False
        Me.txt_centralexciseregno.IsSourceFromValueList = False
        Me.txt_centralexciseregno.IsUnique = False
        Me.txt_centralexciseregno.Location = New System.Drawing.Point(223, 64)
        Me.txt_centralexciseregno.MaxLength = 100
        Me.txt_centralexciseregno.MendatroryField = False
        Me.txt_centralexciseregno.MyLinkLable1 = Nothing
        Me.txt_centralexciseregno.MyLinkLable2 = Nothing
        Me.txt_centralexciseregno.Name = "txt_centralexciseregno"
        Me.txt_centralexciseregno.ReferenceFieldDesc = Nothing
        Me.txt_centralexciseregno.ReferenceFieldName = Nothing
        Me.txt_centralexciseregno.ReferenceTableName = Nothing
        Me.txt_centralexciseregno.Size = New System.Drawing.Size(289, 20)
        Me.txt_centralexciseregno.TabIndex = 136
        '
        'txt_salestaxregno
        '
        Me.txt_salestaxregno.CalculationExpression = Nothing
        Me.txt_salestaxregno.FieldCode = Nothing
        Me.txt_salestaxregno.FieldDesc = Nothing
        Me.txt_salestaxregno.FieldMaxLength = 0
        Me.txt_salestaxregno.FieldName = Nothing
        Me.txt_salestaxregno.isCalculatedField = False
        Me.txt_salestaxregno.IsSourceFromTable = False
        Me.txt_salestaxregno.IsSourceFromValueList = False
        Me.txt_salestaxregno.IsUnique = False
        Me.txt_salestaxregno.Location = New System.Drawing.Point(223, 42)
        Me.txt_salestaxregno.MaxLength = 100
        Me.txt_salestaxregno.MendatroryField = False
        Me.txt_salestaxregno.MyLinkLable1 = Nothing
        Me.txt_salestaxregno.MyLinkLable2 = Nothing
        Me.txt_salestaxregno.Name = "txt_salestaxregno"
        Me.txt_salestaxregno.ReferenceFieldDesc = Nothing
        Me.txt_salestaxregno.ReferenceFieldName = Nothing
        Me.txt_salestaxregno.ReferenceTableName = Nothing
        Me.txt_salestaxregno.Size = New System.Drawing.Size(289, 20)
        Me.txt_salestaxregno.TabIndex = 135
        '
        'txt_nameofbanker
        '
        Me.txt_nameofbanker.CalculationExpression = Nothing
        Me.txt_nameofbanker.FieldCode = Nothing
        Me.txt_nameofbanker.FieldDesc = Nothing
        Me.txt_nameofbanker.FieldMaxLength = 0
        Me.txt_nameofbanker.FieldName = Nothing
        Me.txt_nameofbanker.isCalculatedField = False
        Me.txt_nameofbanker.IsSourceFromTable = False
        Me.txt_nameofbanker.IsSourceFromValueList = False
        Me.txt_nameofbanker.IsUnique = False
        Me.txt_nameofbanker.Location = New System.Drawing.Point(223, 20)
        Me.txt_nameofbanker.MaxLength = 100
        Me.txt_nameofbanker.MendatroryField = False
        Me.txt_nameofbanker.MyLinkLable1 = Nothing
        Me.txt_nameofbanker.MyLinkLable2 = Nothing
        Me.txt_nameofbanker.Name = "txt_nameofbanker"
        Me.txt_nameofbanker.ReferenceFieldDesc = Nothing
        Me.txt_nameofbanker.ReferenceFieldName = Nothing
        Me.txt_nameofbanker.ReferenceTableName = Nothing
        Me.txt_nameofbanker.Size = New System.Drawing.Size(618, 20)
        Me.txt_nameofbanker.TabIndex = 134
        '
        'RadLabel43
        '
        Me.RadLabel43.Location = New System.Drawing.Point(9, 67)
        Me.RadLabel43.Name = "RadLabel43"
        Me.RadLabel43.Size = New System.Drawing.Size(191, 18)
        Me.RadLabel43.TabIndex = 131
        Me.RadLabel43.Text = "CENTRAL EXCISE REGISTRATION NO."
        '
        'RadLabel44
        '
        Me.RadLabel44.Location = New System.Drawing.Point(9, 44)
        Me.RadLabel44.Name = "RadLabel44"
        Me.RadLabel44.Size = New System.Drawing.Size(161, 18)
        Me.RadLabel44.TabIndex = 132
        Me.RadLabel44.Text = "SALES TAX REGISTRATION NO."
        '
        'RadLabel45
        '
        Me.RadLabel45.Location = New System.Drawing.Point(9, 21)
        Me.RadLabel45.Name = "RadLabel45"
        Me.RadLabel45.Size = New System.Drawing.Size(163, 18)
        Me.RadLabel45.TabIndex = 133
        Me.RadLabel45.Text = "NAME && ADDRESS OF BANKER"
        '
        'Panel10
        '
        Me.Panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel10.Controls.Add(Me.btnclear_assessor)
        Me.Panel10.Controls.Add(Me.btnselectsign_assessor)
        Me.Panel10.Controls.Add(Me.pb_assessor)
        Me.Panel10.Controls.Add(Me.dtp_assessorsigndate)
        Me.Panel10.Controls.Add(Me.RadLabel38)
        Me.Panel10.Controls.Add(Me.txt_assessordesig)
        Me.Panel10.Controls.Add(Me.RadLabel39)
        Me.Panel10.Controls.Add(Me.txt_assessorname)
        Me.Panel10.Controls.Add(Me.RadLabel40)
        Me.Panel10.Controls.Add(Me.RadLabel41)
        Me.Panel10.Controls.Add(Me.RadLabel42)
        Me.Panel10.Controls.Add(Me.Panel11)
        Me.Panel10.Location = New System.Drawing.Point(580, 108)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(262, 140)
        Me.Panel10.TabIndex = 130
        '
        'btnclear_assessor
        '
        Me.btnclear_assessor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnclear_assessor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear_assessor.Location = New System.Drawing.Point(198, 93)
        Me.btnclear_assessor.Name = "btnclear_assessor"
        Me.btnclear_assessor.Size = New System.Drawing.Size(55, 18)
        Me.btnclear_assessor.TabIndex = 131
        Me.btnclear_assessor.Text = "Clear"
        '
        'btnselectsign_assessor
        '
        Me.btnselectsign_assessor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnselectsign_assessor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnselectsign_assessor.Location = New System.Drawing.Point(198, 74)
        Me.btnselectsign_assessor.Name = "btnselectsign_assessor"
        Me.btnselectsign_assessor.Size = New System.Drawing.Size(55, 18)
        Me.btnselectsign_assessor.TabIndex = 130
        Me.btnselectsign_assessor.Text = "Select"
        '
        'pb_assessor
        '
        Me.pb_assessor.Location = New System.Drawing.Point(92, 74)
        Me.pb_assessor.Name = "pb_assessor"
        Me.pb_assessor.Size = New System.Drawing.Size(103, 37)
        Me.pb_assessor.TabIndex = 132
        Me.pb_assessor.TabStop = False
        '
        'dtp_assessorsigndate
        '
        Me.dtp_assessorsigndate.Location = New System.Drawing.Point(92, 113)
        Me.dtp_assessorsigndate.Name = "dtp_assessorsigndate"
        Me.dtp_assessorsigndate.Size = New System.Drawing.Size(161, 20)
        Me.dtp_assessorsigndate.TabIndex = 128
        Me.dtp_assessorsigndate.TabStop = False
        Me.dtp_assessorsigndate.Text = "Tuesday, October 25, 2016"
        Me.dtp_assessorsigndate.Value = New Date(2016, 10, 25, 13, 12, 53, 146)
        '
        'RadLabel38
        '
        Me.RadLabel38.Location = New System.Drawing.Point(3, 3)
        Me.RadLabel38.Name = "RadLabel38"
        Me.RadLabel38.Size = New System.Drawing.Size(129, 18)
        Me.RadLabel38.TabIndex = 127
        Me.RadLabel38.Text = "ASSESSOR'S SIGNATURE"
        '
        'txt_assessordesig
        '
        Me.txt_assessordesig.CalculationExpression = Nothing
        Me.txt_assessordesig.FieldCode = Nothing
        Me.txt_assessordesig.FieldDesc = Nothing
        Me.txt_assessordesig.FieldMaxLength = 0
        Me.txt_assessordesig.FieldName = Nothing
        Me.txt_assessordesig.isCalculatedField = False
        Me.txt_assessordesig.IsSourceFromTable = False
        Me.txt_assessordesig.IsSourceFromValueList = False
        Me.txt_assessordesig.IsUnique = False
        Me.txt_assessordesig.Location = New System.Drawing.Point(92, 52)
        Me.txt_assessordesig.MaxLength = 100
        Me.txt_assessordesig.MendatroryField = False
        Me.txt_assessordesig.MyLinkLable1 = Nothing
        Me.txt_assessordesig.MyLinkLable2 = Nothing
        Me.txt_assessordesig.Name = "txt_assessordesig"
        Me.txt_assessordesig.ReferenceFieldDesc = Nothing
        Me.txt_assessordesig.ReferenceFieldName = Nothing
        Me.txt_assessordesig.ReferenceTableName = Nothing
        Me.txt_assessordesig.Size = New System.Drawing.Size(161, 20)
        Me.txt_assessordesig.TabIndex = 125
        '
        'RadLabel39
        '
        Me.RadLabel39.Location = New System.Drawing.Point(3, 116)
        Me.RadLabel39.Name = "RadLabel39"
        Me.RadLabel39.Size = New System.Drawing.Size(33, 18)
        Me.RadLabel39.TabIndex = 5
        Me.RadLabel39.Text = "DATE"
        '
        'txt_assessorname
        '
        Me.txt_assessorname.CalculationExpression = Nothing
        Me.txt_assessorname.FieldCode = Nothing
        Me.txt_assessorname.FieldDesc = Nothing
        Me.txt_assessorname.FieldMaxLength = 0
        Me.txt_assessorname.FieldName = Nothing
        Me.txt_assessorname.isCalculatedField = False
        Me.txt_assessorname.IsSourceFromTable = False
        Me.txt_assessorname.IsSourceFromValueList = False
        Me.txt_assessorname.IsUnique = False
        Me.txt_assessorname.Location = New System.Drawing.Point(92, 30)
        Me.txt_assessorname.MaxLength = 100
        Me.txt_assessorname.MendatroryField = False
        Me.txt_assessorname.MyLinkLable1 = Nothing
        Me.txt_assessorname.MyLinkLable2 = Nothing
        Me.txt_assessorname.Name = "txt_assessorname"
        Me.txt_assessorname.ReferenceFieldDesc = Nothing
        Me.txt_assessorname.ReferenceFieldName = Nothing
        Me.txt_assessorname.ReferenceTableName = Nothing
        Me.txt_assessorname.Size = New System.Drawing.Size(161, 20)
        Me.txt_assessorname.TabIndex = 124
        '
        'RadLabel40
        '
        Me.RadLabel40.Location = New System.Drawing.Point(3, 76)
        Me.RadLabel40.Name = "RadLabel40"
        Me.RadLabel40.Size = New System.Drawing.Size(65, 18)
        Me.RadLabel40.TabIndex = 4
        Me.RadLabel40.Text = "SIGNATURE"
        '
        'RadLabel41
        '
        Me.RadLabel41.Location = New System.Drawing.Point(3, 53)
        Me.RadLabel41.Name = "RadLabel41"
        Me.RadLabel41.Size = New System.Drawing.Size(78, 18)
        Me.RadLabel41.TabIndex = 4
        Me.RadLabel41.Text = "DESIGNATION"
        '
        'RadLabel42
        '
        Me.RadLabel42.Location = New System.Drawing.Point(3, 30)
        Me.RadLabel42.Name = "RadLabel42"
        Me.RadLabel42.Size = New System.Drawing.Size(38, 18)
        Me.RadLabel42.TabIndex = 4
        Me.RadLabel42.Text = "NAME"
        '
        'Panel11
        '
        Me.Panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel11.Location = New System.Drawing.Point(0, 24)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(260, 1)
        Me.Panel11.TabIndex = 2
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.Panel3)
        Me.RadGroupBox5.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadGroupBox5.HeaderText = "NAME OF CUSTOMERS TO WHOM VENDOR HAS SUPPLIED HIS PRODUCT :"
        Me.RadGroupBox5.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Size = New System.Drawing.Size(1018, 173)
        Me.RadGroupBox5.TabIndex = 125
        Me.RadGroupBox5.Text = "NAME OF CUSTOMERS TO WHOM VENDOR HAS SUPPLIED HIS PRODUCT :"
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Panel5)
        Me.Panel3.Controls.Add(Me.txt_cust10)
        Me.Panel3.Controls.Add(Me.txt_cust5)
        Me.Panel3.Controls.Add(Me.txt_cust9)
        Me.Panel3.Controls.Add(Me.txt_cust4)
        Me.Panel3.Controls.Add(Me.RadLabel23)
        Me.Panel3.Controls.Add(Me.RadLabel22)
        Me.Panel3.Controls.Add(Me.RadLabel24)
        Me.Panel3.Controls.Add(Me.RadLabel16)
        Me.Panel3.Controls.Add(Me.txt_cust8)
        Me.Panel3.Controls.Add(Me.RadLabel18)
        Me.Panel3.Controls.Add(Me.txt_cust7)
        Me.Panel3.Controls.Add(Me.RadLabel17)
        Me.Panel3.Controls.Add(Me.txt_cust6)
        Me.Panel3.Controls.Add(Me.txt_cust3)
        Me.Panel3.Controls.Add(Me.RadLabel25)
        Me.Panel3.Controls.Add(Me.txt_cust2)
        Me.Panel3.Controls.Add(Me.RadLabel26)
        Me.Panel3.Controls.Add(Me.txt_cust1)
        Me.Panel3.Controls.Add(Me.RadLabel27)
        Me.Panel3.Controls.Add(Me.RadLabel19)
        Me.Panel3.Controls.Add(Me.RadLabel20)
        Me.Panel3.Controls.Add(Me.RadLabel21)
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Location = New System.Drawing.Point(223, 21)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(619, 145)
        Me.Panel3.TabIndex = 1
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Location = New System.Drawing.Point(307, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1, 143)
        Me.Panel5.TabIndex = 1
        '
        'txt_cust10
        '
        Me.txt_cust10.CalculationExpression = Nothing
        Me.txt_cust10.FieldCode = Nothing
        Me.txt_cust10.FieldDesc = Nothing
        Me.txt_cust10.FieldMaxLength = 0
        Me.txt_cust10.FieldName = Nothing
        Me.txt_cust10.isCalculatedField = False
        Me.txt_cust10.IsSourceFromTable = False
        Me.txt_cust10.IsSourceFromValueList = False
        Me.txt_cust10.IsUnique = False
        Me.txt_cust10.Location = New System.Drawing.Point(338, 118)
        Me.txt_cust10.MaxLength = 100
        Me.txt_cust10.MendatroryField = False
        Me.txt_cust10.MyLinkLable1 = Nothing
        Me.txt_cust10.MyLinkLable2 = Nothing
        Me.txt_cust10.Name = "txt_cust10"
        Me.txt_cust10.ReferenceFieldDesc = Nothing
        Me.txt_cust10.ReferenceFieldName = Nothing
        Me.txt_cust10.ReferenceTableName = Nothing
        Me.txt_cust10.Size = New System.Drawing.Size(259, 20)
        Me.txt_cust10.TabIndex = 140
        '
        'txt_cust5
        '
        Me.txt_cust5.CalculationExpression = Nothing
        Me.txt_cust5.FieldCode = Nothing
        Me.txt_cust5.FieldDesc = Nothing
        Me.txt_cust5.FieldMaxLength = 0
        Me.txt_cust5.FieldName = Nothing
        Me.txt_cust5.isCalculatedField = False
        Me.txt_cust5.IsSourceFromTable = False
        Me.txt_cust5.IsSourceFromValueList = False
        Me.txt_cust5.IsUnique = False
        Me.txt_cust5.Location = New System.Drawing.Point(29, 118)
        Me.txt_cust5.MaxLength = 100
        Me.txt_cust5.MendatroryField = False
        Me.txt_cust5.MyLinkLable1 = Nothing
        Me.txt_cust5.MyLinkLable2 = Nothing
        Me.txt_cust5.Name = "txt_cust5"
        Me.txt_cust5.ReferenceFieldDesc = Nothing
        Me.txt_cust5.ReferenceFieldName = Nothing
        Me.txt_cust5.ReferenceTableName = Nothing
        Me.txt_cust5.Size = New System.Drawing.Size(259, 20)
        Me.txt_cust5.TabIndex = 130
        '
        'txt_cust9
        '
        Me.txt_cust9.CalculationExpression = Nothing
        Me.txt_cust9.FieldCode = Nothing
        Me.txt_cust9.FieldDesc = Nothing
        Me.txt_cust9.FieldMaxLength = 0
        Me.txt_cust9.FieldName = Nothing
        Me.txt_cust9.isCalculatedField = False
        Me.txt_cust9.IsSourceFromTable = False
        Me.txt_cust9.IsSourceFromValueList = False
        Me.txt_cust9.IsUnique = False
        Me.txt_cust9.Location = New System.Drawing.Point(338, 96)
        Me.txt_cust9.MaxLength = 100
        Me.txt_cust9.MendatroryField = False
        Me.txt_cust9.MyLinkLable1 = Nothing
        Me.txt_cust9.MyLinkLable2 = Nothing
        Me.txt_cust9.Name = "txt_cust9"
        Me.txt_cust9.ReferenceFieldDesc = Nothing
        Me.txt_cust9.ReferenceFieldName = Nothing
        Me.txt_cust9.ReferenceTableName = Nothing
        Me.txt_cust9.Size = New System.Drawing.Size(259, 20)
        Me.txt_cust9.TabIndex = 138
        '
        'txt_cust4
        '
        Me.txt_cust4.CalculationExpression = Nothing
        Me.txt_cust4.FieldCode = Nothing
        Me.txt_cust4.FieldDesc = Nothing
        Me.txt_cust4.FieldMaxLength = 0
        Me.txt_cust4.FieldName = Nothing
        Me.txt_cust4.isCalculatedField = False
        Me.txt_cust4.IsSourceFromTable = False
        Me.txt_cust4.IsSourceFromValueList = False
        Me.txt_cust4.IsUnique = False
        Me.txt_cust4.Location = New System.Drawing.Point(29, 96)
        Me.txt_cust4.MaxLength = 100
        Me.txt_cust4.MendatroryField = False
        Me.txt_cust4.MyLinkLable1 = Nothing
        Me.txt_cust4.MyLinkLable2 = Nothing
        Me.txt_cust4.Name = "txt_cust4"
        Me.txt_cust4.ReferenceFieldDesc = Nothing
        Me.txt_cust4.ReferenceFieldName = Nothing
        Me.txt_cust4.ReferenceTableName = Nothing
        Me.txt_cust4.Size = New System.Drawing.Size(259, 20)
        Me.txt_cust4.TabIndex = 128
        '
        'RadLabel23
        '
        Me.RadLabel23.Location = New System.Drawing.Point(312, 120)
        Me.RadLabel23.Name = "RadLabel23"
        Me.RadLabel23.Size = New System.Drawing.Size(21, 18)
        Me.RadLabel23.TabIndex = 139
        Me.RadLabel23.Text = "10."
        '
        'RadLabel22
        '
        Me.RadLabel22.Location = New System.Drawing.Point(3, 120)
        Me.RadLabel22.Name = "RadLabel22"
        Me.RadLabel22.Size = New System.Drawing.Size(15, 18)
        Me.RadLabel22.TabIndex = 129
        Me.RadLabel22.Text = "5."
        '
        'RadLabel24
        '
        Me.RadLabel24.Location = New System.Drawing.Point(312, 98)
        Me.RadLabel24.Name = "RadLabel24"
        Me.RadLabel24.Size = New System.Drawing.Size(15, 18)
        Me.RadLabel24.TabIndex = 137
        Me.RadLabel24.Text = "9."
        '
        'RadLabel16
        '
        Me.RadLabel16.Location = New System.Drawing.Point(312, 3)
        Me.RadLabel16.Name = "RadLabel16"
        Me.RadLabel16.Size = New System.Drawing.Size(155, 18)
        Me.RadLabel16.TabIndex = 129
        Me.RadLabel16.Text = "CUSTOMER'S NAME && PLACE"
        '
        'txt_cust8
        '
        Me.txt_cust8.CalculationExpression = Nothing
        Me.txt_cust8.FieldCode = Nothing
        Me.txt_cust8.FieldDesc = Nothing
        Me.txt_cust8.FieldMaxLength = 0
        Me.txt_cust8.FieldName = Nothing
        Me.txt_cust8.isCalculatedField = False
        Me.txt_cust8.IsSourceFromTable = False
        Me.txt_cust8.IsSourceFromValueList = False
        Me.txt_cust8.IsUnique = False
        Me.txt_cust8.Location = New System.Drawing.Point(338, 74)
        Me.txt_cust8.MaxLength = 100
        Me.txt_cust8.MendatroryField = False
        Me.txt_cust8.MyLinkLable1 = Nothing
        Me.txt_cust8.MyLinkLable2 = Nothing
        Me.txt_cust8.Name = "txt_cust8"
        Me.txt_cust8.ReferenceFieldDesc = Nothing
        Me.txt_cust8.ReferenceFieldName = Nothing
        Me.txt_cust8.ReferenceTableName = Nothing
        Me.txt_cust8.Size = New System.Drawing.Size(259, 20)
        Me.txt_cust8.TabIndex = 136
        '
        'RadLabel18
        '
        Me.RadLabel18.Location = New System.Drawing.Point(3, 98)
        Me.RadLabel18.Name = "RadLabel18"
        Me.RadLabel18.Size = New System.Drawing.Size(15, 18)
        Me.RadLabel18.TabIndex = 127
        Me.RadLabel18.Text = "4."
        '
        'txt_cust7
        '
        Me.txt_cust7.CalculationExpression = Nothing
        Me.txt_cust7.FieldCode = Nothing
        Me.txt_cust7.FieldDesc = Nothing
        Me.txt_cust7.FieldMaxLength = 0
        Me.txt_cust7.FieldName = Nothing
        Me.txt_cust7.isCalculatedField = False
        Me.txt_cust7.IsSourceFromTable = False
        Me.txt_cust7.IsSourceFromValueList = False
        Me.txt_cust7.IsUnique = False
        Me.txt_cust7.Location = New System.Drawing.Point(338, 52)
        Me.txt_cust7.MaxLength = 100
        Me.txt_cust7.MendatroryField = False
        Me.txt_cust7.MyLinkLable1 = Nothing
        Me.txt_cust7.MyLinkLable2 = Nothing
        Me.txt_cust7.Name = "txt_cust7"
        Me.txt_cust7.ReferenceFieldDesc = Nothing
        Me.txt_cust7.ReferenceFieldName = Nothing
        Me.txt_cust7.ReferenceTableName = Nothing
        Me.txt_cust7.Size = New System.Drawing.Size(259, 20)
        Me.txt_cust7.TabIndex = 135
        '
        'RadLabel17
        '
        Me.RadLabel17.Location = New System.Drawing.Point(3, 3)
        Me.RadLabel17.Name = "RadLabel17"
        Me.RadLabel17.Size = New System.Drawing.Size(155, 18)
        Me.RadLabel17.TabIndex = 127
        Me.RadLabel17.Text = "CUSTOMER'S NAME && PLACE"
        '
        'txt_cust6
        '
        Me.txt_cust6.CalculationExpression = Nothing
        Me.txt_cust6.FieldCode = Nothing
        Me.txt_cust6.FieldDesc = Nothing
        Me.txt_cust6.FieldMaxLength = 0
        Me.txt_cust6.FieldName = Nothing
        Me.txt_cust6.isCalculatedField = False
        Me.txt_cust6.IsSourceFromTable = False
        Me.txt_cust6.IsSourceFromValueList = False
        Me.txt_cust6.IsUnique = False
        Me.txt_cust6.Location = New System.Drawing.Point(338, 30)
        Me.txt_cust6.MaxLength = 100
        Me.txt_cust6.MendatroryField = False
        Me.txt_cust6.MyLinkLable1 = Nothing
        Me.txt_cust6.MyLinkLable2 = Nothing
        Me.txt_cust6.Name = "txt_cust6"
        Me.txt_cust6.ReferenceFieldDesc = Nothing
        Me.txt_cust6.ReferenceFieldName = Nothing
        Me.txt_cust6.ReferenceTableName = Nothing
        Me.txt_cust6.Size = New System.Drawing.Size(259, 20)
        Me.txt_cust6.TabIndex = 134
        '
        'txt_cust3
        '
        Me.txt_cust3.CalculationExpression = Nothing
        Me.txt_cust3.FieldCode = Nothing
        Me.txt_cust3.FieldDesc = Nothing
        Me.txt_cust3.FieldMaxLength = 0
        Me.txt_cust3.FieldName = Nothing
        Me.txt_cust3.isCalculatedField = False
        Me.txt_cust3.IsSourceFromTable = False
        Me.txt_cust3.IsSourceFromValueList = False
        Me.txt_cust3.IsUnique = False
        Me.txt_cust3.Location = New System.Drawing.Point(29, 74)
        Me.txt_cust3.MaxLength = 100
        Me.txt_cust3.MendatroryField = False
        Me.txt_cust3.MyLinkLable1 = Nothing
        Me.txt_cust3.MyLinkLable2 = Nothing
        Me.txt_cust3.Name = "txt_cust3"
        Me.txt_cust3.ReferenceFieldDesc = Nothing
        Me.txt_cust3.ReferenceFieldName = Nothing
        Me.txt_cust3.ReferenceTableName = Nothing
        Me.txt_cust3.Size = New System.Drawing.Size(259, 20)
        Me.txt_cust3.TabIndex = 126
        '
        'RadLabel25
        '
        Me.RadLabel25.Location = New System.Drawing.Point(312, 76)
        Me.RadLabel25.Name = "RadLabel25"
        Me.RadLabel25.Size = New System.Drawing.Size(15, 18)
        Me.RadLabel25.TabIndex = 131
        Me.RadLabel25.Text = "8."
        '
        'txt_cust2
        '
        Me.txt_cust2.CalculationExpression = Nothing
        Me.txt_cust2.FieldCode = Nothing
        Me.txt_cust2.FieldDesc = Nothing
        Me.txt_cust2.FieldMaxLength = 0
        Me.txt_cust2.FieldName = Nothing
        Me.txt_cust2.isCalculatedField = False
        Me.txt_cust2.IsSourceFromTable = False
        Me.txt_cust2.IsSourceFromValueList = False
        Me.txt_cust2.IsUnique = False
        Me.txt_cust2.Location = New System.Drawing.Point(29, 52)
        Me.txt_cust2.MaxLength = 100
        Me.txt_cust2.MendatroryField = False
        Me.txt_cust2.MyLinkLable1 = Nothing
        Me.txt_cust2.MyLinkLable2 = Nothing
        Me.txt_cust2.Name = "txt_cust2"
        Me.txt_cust2.ReferenceFieldDesc = Nothing
        Me.txt_cust2.ReferenceFieldName = Nothing
        Me.txt_cust2.ReferenceTableName = Nothing
        Me.txt_cust2.Size = New System.Drawing.Size(259, 20)
        Me.txt_cust2.TabIndex = 125
        '
        'RadLabel26
        '
        Me.RadLabel26.Location = New System.Drawing.Point(312, 53)
        Me.RadLabel26.Name = "RadLabel26"
        Me.RadLabel26.Size = New System.Drawing.Size(15, 18)
        Me.RadLabel26.TabIndex = 132
        Me.RadLabel26.Text = "7."
        '
        'txt_cust1
        '
        Me.txt_cust1.CalculationExpression = Nothing
        Me.txt_cust1.FieldCode = Nothing
        Me.txt_cust1.FieldDesc = Nothing
        Me.txt_cust1.FieldMaxLength = 0
        Me.txt_cust1.FieldName = Nothing
        Me.txt_cust1.isCalculatedField = False
        Me.txt_cust1.IsSourceFromTable = False
        Me.txt_cust1.IsSourceFromValueList = False
        Me.txt_cust1.IsUnique = False
        Me.txt_cust1.Location = New System.Drawing.Point(29, 30)
        Me.txt_cust1.MaxLength = 100
        Me.txt_cust1.MendatroryField = False
        Me.txt_cust1.MyLinkLable1 = Nothing
        Me.txt_cust1.MyLinkLable2 = Nothing
        Me.txt_cust1.Name = "txt_cust1"
        Me.txt_cust1.ReferenceFieldDesc = Nothing
        Me.txt_cust1.ReferenceFieldName = Nothing
        Me.txt_cust1.ReferenceTableName = Nothing
        Me.txt_cust1.Size = New System.Drawing.Size(259, 20)
        Me.txt_cust1.TabIndex = 124
        '
        'RadLabel27
        '
        Me.RadLabel27.Location = New System.Drawing.Point(312, 30)
        Me.RadLabel27.Name = "RadLabel27"
        Me.RadLabel27.Size = New System.Drawing.Size(15, 18)
        Me.RadLabel27.TabIndex = 133
        Me.RadLabel27.Text = "6."
        '
        'RadLabel19
        '
        Me.RadLabel19.Location = New System.Drawing.Point(3, 76)
        Me.RadLabel19.Name = "RadLabel19"
        Me.RadLabel19.Size = New System.Drawing.Size(15, 18)
        Me.RadLabel19.TabIndex = 4
        Me.RadLabel19.Text = "3."
        '
        'RadLabel20
        '
        Me.RadLabel20.Location = New System.Drawing.Point(3, 53)
        Me.RadLabel20.Name = "RadLabel20"
        Me.RadLabel20.Size = New System.Drawing.Size(15, 18)
        Me.RadLabel20.TabIndex = 4
        Me.RadLabel20.Text = "2."
        '
        'RadLabel21
        '
        Me.RadLabel21.Location = New System.Drawing.Point(3, 30)
        Me.RadLabel21.Name = "RadLabel21"
        Me.RadLabel21.Size = New System.Drawing.Size(15, 18)
        Me.RadLabel21.TabIndex = 4
        Me.RadLabel21.Text = "1."
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Location = New System.Drawing.Point(-18, 24)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(639, 1)
        Me.Panel4.TabIndex = 2
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage2.Controls.Add(Me.txt_visitorname)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(89.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1039, 512)
        Me.RadPageViewPage2.Text = "For official use"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rb_notapproved)
        Me.RadGroupBox2.Controls.Add(Me.rb_approved)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel10)
        Me.RadGroupBox2.Controls.Add(Me.Panel1)
        Me.RadGroupBox2.HeaderText = "NOT SUITABLE FOR THE TYPE OF PRODUCT AND LEVEL QUALITY REQUIRED BY US"
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 205)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(1034, 167)
        Me.RadGroupBox2.TabIndex = 124
        Me.RadGroupBox2.Text = "NOT SUITABLE FOR THE TYPE OF PRODUCT AND LEVEL QUALITY REQUIRED BY US"
        '
        'rb_notapproved
        '
        Me.rb_notapproved.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rb_notapproved.Location = New System.Drawing.Point(71, 72)
        Me.rb_notapproved.Name = "rb_notapproved"
        Me.rb_notapproved.Size = New System.Drawing.Size(102, 18)
        Me.rb_notapproved.TabIndex = 5
        Me.rb_notapproved.Text = "NOT APPROVED"
        Me.rb_notapproved.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rb_approved
        '
        Me.rb_approved.Location = New System.Drawing.Point(71, 48)
        Me.rb_approved.Name = "rb_approved"
        Me.rb_approved.Size = New System.Drawing.Size(76, 18)
        Me.rb_approved.TabIndex = 4
        Me.rb_approved.TabStop = False
        Me.rb_approved.Text = "APPROVED"
        '
        'RadLabel10
        '
        Me.RadLabel10.BorderVisible = True
        Me.RadLabel10.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.Location = New System.Drawing.Point(70, 19)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(58, 21)
        Me.RadLabel10.TabIndex = 3
        Me.RadLabel10.Text = "RESULTS"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnclear_approved)
        Me.Panel1.Controls.Add(Me.btnselectsign_approved)
        Me.Panel1.Controls.Add(Me.pb_approved)
        Me.Panel1.Controls.Add(Me.dtp_approveddate)
        Me.Panel1.Controls.Add(Me.RadLabel9)
        Me.Panel1.Controls.Add(Me.txt_approveddesig)
        Me.Panel1.Controls.Add(Me.RadLabel8)
        Me.Panel1.Controls.Add(Me.txt_approvedname)
        Me.Panel1.Controls.Add(Me.RadLabel7)
        Me.Panel1.Controls.Add(Me.RadLabel6)
        Me.Panel1.Controls.Add(Me.RadLabel5)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(764, 22)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(262, 140)
        Me.Panel1.TabIndex = 1
        '
        'btnclear_approved
        '
        Me.btnclear_approved.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnclear_approved.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear_approved.Location = New System.Drawing.Point(198, 93)
        Me.btnclear_approved.Name = "btnclear_approved"
        Me.btnclear_approved.Size = New System.Drawing.Size(55, 18)
        Me.btnclear_approved.TabIndex = 7
        Me.btnclear_approved.Text = "Clear"
        '
        'btnselectsign_approved
        '
        Me.btnselectsign_approved.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnselectsign_approved.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnselectsign_approved.Location = New System.Drawing.Point(198, 74)
        Me.btnselectsign_approved.Name = "btnselectsign_approved"
        Me.btnselectsign_approved.Size = New System.Drawing.Size(55, 18)
        Me.btnselectsign_approved.TabIndex = 6
        Me.btnselectsign_approved.Text = "Select"
        '
        'pb_approved
        '
        Me.pb_approved.Location = New System.Drawing.Point(92, 74)
        Me.pb_approved.Name = "pb_approved"
        Me.pb_approved.Size = New System.Drawing.Size(103, 37)
        Me.pb_approved.TabIndex = 129
        Me.pb_approved.TabStop = False
        '
        'dtp_approveddate
        '
        Me.dtp_approveddate.Location = New System.Drawing.Point(92, 113)
        Me.dtp_approveddate.Name = "dtp_approveddate"
        Me.dtp_approveddate.Size = New System.Drawing.Size(161, 20)
        Me.dtp_approveddate.TabIndex = 128
        Me.dtp_approveddate.TabStop = False
        Me.dtp_approveddate.Text = "Tuesday, October 25, 2016"
        Me.dtp_approveddate.Value = New Date(2016, 10, 25, 13, 12, 53, 146)
        '
        'RadLabel9
        '
        Me.RadLabel9.Location = New System.Drawing.Point(3, 3)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(78, 18)
        Me.RadLabel9.TabIndex = 127
        Me.RadLabel9.Text = "APPROVED BY"
        '
        'txt_approveddesig
        '
        Me.txt_approveddesig.CalculationExpression = Nothing
        Me.txt_approveddesig.FieldCode = Nothing
        Me.txt_approveddesig.FieldDesc = Nothing
        Me.txt_approveddesig.FieldMaxLength = 0
        Me.txt_approveddesig.FieldName = Nothing
        Me.txt_approveddesig.isCalculatedField = False
        Me.txt_approveddesig.IsSourceFromTable = False
        Me.txt_approveddesig.IsSourceFromValueList = False
        Me.txt_approveddesig.IsUnique = False
        Me.txt_approveddesig.Location = New System.Drawing.Point(92, 52)
        Me.txt_approveddesig.MaxLength = 100
        Me.txt_approveddesig.MendatroryField = False
        Me.txt_approveddesig.MyLinkLable1 = Nothing
        Me.txt_approveddesig.MyLinkLable2 = Nothing
        Me.txt_approveddesig.Name = "txt_approveddesig"
        Me.txt_approveddesig.ReferenceFieldDesc = Nothing
        Me.txt_approveddesig.ReferenceFieldName = Nothing
        Me.txt_approveddesig.ReferenceTableName = Nothing
        Me.txt_approveddesig.Size = New System.Drawing.Size(161, 20)
        Me.txt_approveddesig.TabIndex = 125
        '
        'RadLabel8
        '
        Me.RadLabel8.Location = New System.Drawing.Point(3, 116)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(33, 18)
        Me.RadLabel8.TabIndex = 5
        Me.RadLabel8.Text = "DATE"
        '
        'txt_approvedname
        '
        Me.txt_approvedname.CalculationExpression = Nothing
        Me.txt_approvedname.FieldCode = Nothing
        Me.txt_approvedname.FieldDesc = Nothing
        Me.txt_approvedname.FieldMaxLength = 0
        Me.txt_approvedname.FieldName = Nothing
        Me.txt_approvedname.isCalculatedField = False
        Me.txt_approvedname.IsSourceFromTable = False
        Me.txt_approvedname.IsSourceFromValueList = False
        Me.txt_approvedname.IsUnique = False
        Me.txt_approvedname.Location = New System.Drawing.Point(92, 30)
        Me.txt_approvedname.MaxLength = 100
        Me.txt_approvedname.MendatroryField = False
        Me.txt_approvedname.MyLinkLable1 = Nothing
        Me.txt_approvedname.MyLinkLable2 = Nothing
        Me.txt_approvedname.Name = "txt_approvedname"
        Me.txt_approvedname.ReferenceFieldDesc = Nothing
        Me.txt_approvedname.ReferenceFieldName = Nothing
        Me.txt_approvedname.ReferenceTableName = Nothing
        Me.txt_approvedname.Size = New System.Drawing.Size(161, 20)
        Me.txt_approvedname.TabIndex = 124
        '
        'RadLabel7
        '
        Me.RadLabel7.Location = New System.Drawing.Point(3, 76)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(65, 18)
        Me.RadLabel7.TabIndex = 4
        Me.RadLabel7.Text = "SIGNATURE"
        '
        'RadLabel6
        '
        Me.RadLabel6.Location = New System.Drawing.Point(3, 53)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(78, 18)
        Me.RadLabel6.TabIndex = 4
        Me.RadLabel6.Text = "DESIGNATION"
        '
        'RadLabel5
        '
        Me.RadLabel5.Location = New System.Drawing.Point(3, 30)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(38, 18)
        Me.RadLabel5.TabIndex = 4
        Me.RadLabel5.Text = "NAME"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Location = New System.Drawing.Point(0, 24)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(260, 1)
        Me.Panel2.TabIndex = 2
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(9, 1)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(208, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "VENDOR WAS VISITED/ NOT VISITED BY"
        '
        'txt_visitorname
        '
        Me.txt_visitorname.CalculationExpression = Nothing
        Me.txt_visitorname.FieldCode = Nothing
        Me.txt_visitorname.FieldDesc = Nothing
        Me.txt_visitorname.FieldMaxLength = 0
        Me.txt_visitorname.FieldName = Nothing
        Me.txt_visitorname.isCalculatedField = False
        Me.txt_visitorname.IsSourceFromTable = False
        Me.txt_visitorname.IsSourceFromValueList = False
        Me.txt_visitorname.IsUnique = False
        Me.txt_visitorname.Location = New System.Drawing.Point(217, 1)
        Me.txt_visitorname.MaxLength = 100
        Me.txt_visitorname.MendatroryField = False
        Me.txt_visitorname.MyLinkLable1 = Nothing
        Me.txt_visitorname.MyLinkLable2 = Nothing
        Me.txt_visitorname.Name = "txt_visitorname"
        Me.txt_visitorname.ReferenceFieldDesc = Nothing
        Me.txt_visitorname.ReferenceFieldName = Nothing
        Me.txt_visitorname.ReferenceTableName = Nothing
        Me.txt_visitorname.Size = New System.Drawing.Size(152, 20)
        Me.txt_visitorname.TabIndex = 123
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(371, 1)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(240, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "OF THIS COMPANY HIS REPORT IS ATTACHED."
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txt_suitableforvendor)
        Me.RadGroupBox1.Controls.Add(Me.txt_suitablefor)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.HeaderText = "SOURCE APPROVED FOR SUPPLY FOR FOLLOWING" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.RadGroupBox1.Location = New System.Drawing.Point(2, 51)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(1034, 148)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "SOURCE APPROVED FOR SUPPLY FOR FOLLOWING" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'txt_suitableforvendor
        '
        Me.txt_suitableforvendor.AutoSize = False
        Me.txt_suitableforvendor.Location = New System.Drawing.Point(162, 93)
        Me.txt_suitableforvendor.Multiline = True
        Me.txt_suitableforvendor.Name = "txt_suitableforvendor"
        Me.txt_suitableforvendor.Size = New System.Drawing.Size(864, 47)
        Me.txt_suitableforvendor.TabIndex = 6
        '
        'txt_suitablefor
        '
        Me.txt_suitablefor.AutoSize = False
        Me.txt_suitablefor.Location = New System.Drawing.Point(162, 21)
        Me.txt_suitablefor.Multiline = True
        Me.txt_suitablefor.Name = "txt_suitablefor"
        Me.txt_suitablefor.Size = New System.Drawing.Size(864, 66)
        Me.txt_suitablefor.TabIndex = 5
        '
        'RadLabel4
        '
        Me.RadLabel4.Location = New System.Drawing.Point(7, 92)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(148, 18)
        Me.RadLabel4.TabIndex = 4
        Me.RadLabel4.Text = "SUITABLE FOR (IF VENDOR)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(7, 21)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(82, 18)
        Me.RadLabel3.TabIndex = 3
        Me.RadLabel3.Text = "SUITABLE FOR" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'btn_VenRegApprove
        '
        Me.btn_VenRegApprove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_VenRegApprove.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_VenRegApprove.Location = New System.Drawing.Point(225, 7)
        Me.btn_VenRegApprove.Name = "btn_VenRegApprove"
        Me.btn_VenRegApprove.Size = New System.Drawing.Size(60, 18)
        Me.btn_VenRegApprove.TabIndex = 125
        Me.btn_VenRegApprove.Text = "Approve"
        '
        'btn_print
        '
        Me.btn_print.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_print.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_print.Location = New System.Drawing.Point(153, 7)
        Me.btn_print.Name = "btn_print"
        Me.btn_print.Size = New System.Drawing.Size(66, 18)
        Me.btn_print.TabIndex = 7
        Me.btn_print.Text = "Print"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(13, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 4
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(83, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 5
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(981, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 6
        Me.btnclose.Text = "Close"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(912, 1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(88, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 139
        '
        'FrmVendorReg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1060, 616)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmVendorReg"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Vendor Registration"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadPageView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView2.ResumeLayout(False)
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.RadPageViewPage3.PerformLayout()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtManufacturing_facilities, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox10.ResumeLayout(False)
        CType(Me.txtdetails_orgstructure, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTurnOver, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox9.ResumeLayout(False)
        Me.RadGroupBox9.PerformLayout()
        CType(Me.txtContactPPhone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtContactPName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFaxNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPhoneNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        Me.RadGroupBox8.PerformLayout()
        CType(Me.rbtn_subcontractor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox7.ResumeLayout(False)
        Me.RadGroupBox7.PerformLayout()
        CType(Me.rbtn_ownership, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtn_govt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtn_public, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtn_pvt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName_Owners, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAddress1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChillingVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAddress2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gvtesting.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvtesting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_captive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_captivedetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvmanufacturing.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvmanufacturing, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        Me.RadPageViewPage5.PerformLayout()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_PackingDefined, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_definedQP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_PackingDefinedDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_FacilitiesAvailable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_definedQPdetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_ProStorageSys, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_equipcalperiodically, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_RMIdentified, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_tmppermdevRecord, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_KeepRecord, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_ProInsAvailable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_InsDespatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_whoisauthorised, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_RMInsAvailable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_fulldocqsavailable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_stddrawingsavailable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_separatesection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage6.ResumeLayout(False)
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.RadGroupBox6.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        CType(Me.btnclear_vendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnselectsign_vendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_vendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtp_vendorsigndate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_vendordesig, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_vendorname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel36, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_paymentterms, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel46, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_centralexciseregno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_salestaxregno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_nameofbanker, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel44, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel45, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        CType(Me.btnclear_assessor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnselectsign_assessor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_assessor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtp_assessorsigndate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_assessordesig, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_assessorname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel42, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.txt_cust10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_cust5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_cust9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_cust4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_cust8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_cust7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_cust6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_cust3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_cust2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_cust1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.rb_notapproved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rb_approved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.btnclear_approved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnselectsign_approved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_approved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtp_approveddate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_approveddesig, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_approvedname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_visitorname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txt_suitableforvendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_suitablefor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_VenRegApprove, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_print, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView

    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txt_visitorname As common.Controls.MyTextBox
    Friend WithEvents txt_suitableforvendor As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txt_suitablefor As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents RadLabel4 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel3 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dtp_approveddate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadLabel9 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txt_approveddesig As common.Controls.MyTextBox

    Friend WithEvents RadLabel8 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txt_approvedname As common.Controls.MyTextBox
    Friend WithEvents RadLabel7 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel6 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel5 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents rb_notapproved As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rb_approved As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadLabel10 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadPageView2 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage5 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadLabel14 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel13 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents chk_definedQP As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chk_FacilitiesAvailable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txt_definedQPdetails As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents chk_ProStorageSys As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chk_equipcalperiodically As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chk_RMIdentified As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chk_tmppermdevRecord As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chk_KeepRecord As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chk_ProInsAvailable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chk_InsDespatch As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txt_whoisauthorised As Telerik.WinControls.UI.RadTextBox

    Friend WithEvents RadLabel12 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents chk_RMInsAvailable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chk_fulldocqsavailable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chk_stddrawingsavailable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chk_separatesection As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chk_PackingDefined As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txt_PackingDefinedDetails As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel15 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents chk_captive As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txt_captivedetails As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents gvmanufacturing As common.UserControls.MyRadGridView
    Friend WithEvents RadGridView1 As Telerik.WinControls.UI.MasterGridViewTemplate
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvtesting As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage6 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents RadLabel17 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txt_cust3 As common.Controls.MyTextBox
    Friend WithEvents txt_cust2 As common.Controls.MyTextBox
    Friend WithEvents txt_cust1 As common.Controls.MyTextBox
    Friend WithEvents RadLabel19 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel20 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel21 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents txt_cust10 As common.Controls.MyTextBox
    Friend WithEvents txt_cust5 As common.Controls.MyTextBox
    Friend WithEvents txt_cust9 As common.Controls.MyTextBox
    Friend WithEvents txt_cust4 As common.Controls.MyTextBox
    Friend WithEvents RadLabel23 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel22 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel24 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel16 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txt_cust8 As common.Controls.MyTextBox
    Friend WithEvents RadLabel18 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txt_cust7 As common.Controls.MyTextBox
    Friend WithEvents txt_cust6 As common.Controls.MyTextBox
    Friend WithEvents RadLabel25 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel26 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel27 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents dtp_assessorsigndate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadLabel38 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txt_assessordesig As common.Controls.MyTextBox

    Friend WithEvents RadLabel39 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txt_assessorname As common.Controls.MyTextBox
    Friend WithEvents RadLabel40 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel41 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel42 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents dtp_vendorsigndate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadLabel28 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txt_vendordesig As common.Controls.MyTextBox

    Friend WithEvents RadLabel34 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txt_vendorname As common.Controls.MyTextBox
    Friend WithEvents RadLabel35 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel36 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel37 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents txt_paymentterms As common.Controls.MyTextBox
    Friend WithEvents RadLabel46 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txt_centralexciseregno As common.Controls.MyTextBox
    Friend WithEvents txt_salestaxregno As common.Controls.MyTextBox
    Friend WithEvents txt_nameofbanker As common.Controls.MyTextBox
    Friend WithEvents RadLabel43 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel44 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel45 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadGroupBox8 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rbtn_subcontractor As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox7 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rbtn_ownership As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtn_govt As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtn_public As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtn_pvt As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtName_Owners As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtAddress1 As common.Controls.MyTextBox
    Friend WithEvents lblChillingVendor As common.Controls.MyLabel
    Friend WithEvents txtName As common.Controls.MyTextBox
    Friend WithEvents MyLabel44 As common.Controls.MyLabel
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents txtAddress2 As common.Controls.MyTextBox
    Friend WithEvents txtcode As common.UserControls.txtNavigator
    Friend WithEvents RadGroupBox9 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents txtFaxNo As common.Controls.MyTextBox
    Friend WithEvents txtPhoneNo As Telerik.WinControls.UI.RadMaskedEditBox
    Friend WithEvents txtContactPPhone As Telerik.WinControls.UI.RadMaskedEditBox
    Friend WithEvents txtContactPName As common.Controls.MyTextBox
    Friend WithEvents MyLabel39 As common.Controls.MyLabel
    Friend WithEvents MyLabel37 As common.Controls.MyLabel
    Friend WithEvents txtTurnOver As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox10 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtdetails_orgstructure As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtManufacturing_facilities As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents RadLabel11 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents btn_VenRegApprove As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents pb_approved As System.Windows.Forms.PictureBox
    Friend WithEvents btnclear_approved As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnselectsign_approved As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclear_vendor As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnselectsign_vendor As Telerik.WinControls.UI.RadButton
    Friend WithEvents pb_vendor As System.Windows.Forms.PictureBox
    Friend WithEvents btnclear_assessor As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnselectsign_assessor As Telerik.WinControls.UI.RadButton
    Friend WithEvents pb_assessor As System.Windows.Forms.PictureBox
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btn_print As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
End Class