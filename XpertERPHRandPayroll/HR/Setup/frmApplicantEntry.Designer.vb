Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmApplicantEntry
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        Me.UsLock1 = New common.usLock
        Me.lblMCCCode = New common.Controls.MyLabel
        Me.txtdesp = New common.Controls.MyTextBox
        Me.MyLabel19 = New common.Controls.MyLabel
        Me.txtcode = New common.UserControls.txtNavigator
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.dtpDate = New common.Controls.MyDateTimePicker
        Me.MyLabel12 = New common.Controls.MyLabel
        Me.txtsourcetype = New common.UserControls.txtFinder
        Me.lblChillingVendor = New common.Controls.MyLabel
        Me.lblsourcedetail = New common.Controls.MyLabel
        Me.txtsourcedetail = New common.UserControls.txtFinder
        Me.MyLabel43 = New common.Controls.MyLabel
        Me.lblsourcetype = New common.Controls.MyLabel
        Me.lblrequisition = New common.Controls.MyLabel
        Me.txtrequisitioncode = New common.UserControls.txtFinder
        Me.MyLabel44 = New common.Controls.MyLabel
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.txtTelephone = New Telerik.WinControls.UI.RadMaskedEditBox
        Me.txtPinCode = New common.MyNumBox
        Me.MyLabel7 = New common.Controls.MyLabel
        Me.txtadd3 = New common.Controls.MyTextBox
        Me.MyLabel16 = New common.Controls.MyLabel
        Me.txtadd4 = New common.Controls.MyTextBox
        Me.MyLabel15 = New common.Controls.MyLabel
        Me.lblCity = New common.Controls.MyLabel
        Me.lblState = New common.Controls.MyLabel
        Me.lblCountry = New common.Controls.MyLabel
        Me.txtAdd1 = New common.Controls.MyTextBox
        Me.lblAdd1 = New common.Controls.MyLabel
        Me.MyLabel9 = New common.Controls.MyLabel
        Me.txtEmail = New common.Controls.MyTextBox
        Me.MyLabel6 = New common.Controls.MyLabel
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.txtCity = New common.UserControls.txtFinder
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.txtState = New common.UserControls.txtFinder
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtCountry = New common.UserControls.txtFinder
        Me.txtAdd2 = New common.Controls.MyTextBox
        Me.lblAdd2 = New common.Controls.MyLabel
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.PicImage = New System.Windows.Forms.PictureBox
        Me.btnclearphoto = New Telerik.WinControls.UI.RadButton
        Me.btnPhotoBrowse = New Telerik.WinControls.UI.RadButton
        Me.MyLabel21 = New common.Controls.MyLabel
        Me.txtPanNo = New common.Controls.MyTextBox
        Me.MyLabel20 = New common.Controls.MyLabel
        Me.CmbMarStatus = New common.Controls.MyComboBox
        Me.MyLabel14 = New common.Controls.MyLabel
        Me.dtpDateofBirth = New common.Controls.MyDateTimePicker
        Me.MyLabel13 = New common.Controls.MyLabel
        Me.txtLastName = New common.Controls.MyTextBox
        Me.MyLabel11 = New common.Controls.MyLabel
        Me.txtMiddleName = New common.Controls.MyTextBox
        Me.MyLabel8 = New common.Controls.MyLabel
        Me.txtFirstName = New common.Controls.MyTextBox
        Me.MyLabel10 = New common.Controls.MyLabel
        Me.cmbGender = New common.Controls.MyComboBox
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.cmbSalutation = New common.Controls.MyComboBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.dtpDateofIntr = New common.Controls.MyDateTimePicker
        Me.lblPPDate = New common.Controls.MyLabel
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.gvQualification = New common.UserControls.MyRadGridView
        Me.MyLabel22 = New common.Controls.MyLabel
        Me.btnShowDoc = New Telerik.WinControls.UI.RadButton
        Me.btnBrowse = New Telerik.WinControls.UI.RadButton
        Me.BtnDeleteDoc = New Telerik.WinControls.UI.RadButton
        Me.txtResume = New common.Controls.MyTextBox
        Me.BtnDocReset = New Telerik.WinControls.UI.RadButton
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gvDoc = New common.UserControls.MyRadGridView
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.ChkFresher = New Telerik.WinControls.UI.RadCheckBox
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.gvEmpHis = New common.UserControls.MyRadGridView
        Me.RadPageViewPage6 = New Telerik.WinControls.UI.RadPageViewPage
        Me.LblAgency = New common.Controls.MyLabel
        Me.MyLabel23 = New common.Controls.MyLabel
        Me.txtAgency = New common.UserControls.txtFinder
        Me.lblRelation = New common.Controls.MyLabel
        Me.lblEmpName = New common.Controls.MyLabel
        Me.LblRefBy = New common.Controls.MyLabel
        Me.txtEmpCode = New common.UserControls.txtFinder
        Me.MyLabel18 = New common.Controls.MyLabel
        Me.txtRelation = New common.UserControls.txtFinder
        Me.rbnrefbyAge = New common.Controls.MyRadioButton
        Me.rbnRefbyEmp = New common.Controls.MyRadioButton
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer
        Me.gvFamily = New common.UserControls.MyRadGridView
        Me.ChkHandicaped = New Telerik.WinControls.UI.RadCheckBox
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.txtHandiDetail = New common.Controls.MyTextBox
        Me.MyLabel41 = New common.Controls.MyLabel
        Me.txtTotalCTC = New common.MyNumBox
        Me.MyLabel29 = New common.Controls.MyLabel
        Me.txtCurrGrossSal = New common.MyNumBox
        Me.MyLabel27 = New common.Controls.MyLabel
        Me.LblPreLoc = New common.Controls.MyLabel
        Me.MyLabel40 = New common.Controls.MyLabel
        Me.txtPreferedLoc = New common.UserControls.txtFinder
        Me.LblLocation = New common.Controls.MyLabel
        Me.MyLabel48 = New common.Controls.MyLabel
        Me.txtLocation = New common.UserControls.txtFinder
        Me.txtPerBy = New common.Controls.MyTextBox
        Me.MyLabel24 = New common.Controls.MyLabel
        Me.ChkRelocation = New Telerik.WinControls.UI.RadCheckBox
        Me.grpReLoc = New Telerik.WinControls.UI.RadGroupBox
        Me.LblToLoc = New common.Controls.MyLabel
        Me.MyLabel46 = New common.Controls.MyLabel
        Me.txtToLoc = New common.UserControls.txtFinder
        Me.LblFromLoc = New common.Controls.MyLabel
        Me.MyLabel53 = New common.Controls.MyLabel
        Me.txtFromLoc = New common.UserControls.txtFinder
        Me.lblBranchName = New common.Controls.MyLabel
        Me.LblBankName = New common.Controls.MyLabel
        Me.txtAccNo = New common.Controls.MyTextBox
        Me.MyLabel28 = New common.Controls.MyLabel
        Me.MyLabel31 = New common.Controls.MyLabel
        Me.txtBranchCode = New common.UserControls.txtFinder
        Me.MyLabel32 = New common.Controls.MyLabel
        Me.txtBankCode = New common.UserControls.txtFinder
        Me.txtBloodGrp = New common.Controls.MyTextBox
        Me.MyLabel33 = New common.Controls.MyLabel
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChillingVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblsourcedetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblsourcetype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblrequisition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtTelephone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPinCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtadd3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtadd4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdd2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.PicImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclearphoto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPhotoBrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbMarStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDateofBirth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLastName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMiddleName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFirstName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbGender, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSalutation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDateofIntr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPPDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.gvQualification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvQualification.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShowDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnDeleteDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtResume, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnDocReset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.gvDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDoc.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.ChkFresher, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.gvEmpHis, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvEmpHis.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage6.SuspendLayout()
        CType(Me.LblAgency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRelation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblRefBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbnrefbyAge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbnRefbyEmp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.gvFamily, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvFamily.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkHandicaped, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.txtHandiDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalCTC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCurrGrossSal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblPreLoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel48, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPerBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkRelocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpReLoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpReLoc.SuspendLayout()
        CType(Me.LblToLoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel46, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblFromLoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel53, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBranchName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAccNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBloodGrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(970, 451)
        Me.SplitContainer1.SplitterDistance = 419
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage6)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(970, 419)
        Me.RadPageView1.TabIndex = 2
        Me.RadPageView1.Text = "Compressor"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(96.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(949, 371)
        Me.RadPageViewPage1.Text = "Personal Details"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblMCCCode)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtdesp)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtcode)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel19)
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtsourcetype)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel12)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblChillingVendor)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblsourcedetail)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtsourcedetail)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblsourcetype)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel43)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblrequisition)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtrequisitioncode)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel44)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer3.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer3.Size = New System.Drawing.Size(949, 371)
        Me.SplitContainer3.SplitterDistance = 124
        Me.SplitContainer3.TabIndex = 115
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(842, 10)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 110
        '
        'lblMCCCode
        '
        Me.lblMCCCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblMCCCode.Location = New System.Drawing.Point(9, 13)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(34, 16)
        Me.lblMCCCode.TabIndex = 9
        Me.lblMCCCode.Text = "Code"
        '
        'txtdesp
        '
        Me.txtdesp.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtdesp.Location = New System.Drawing.Point(115, 98)
        Me.txtdesp.MaxLength = 100
        Me.txtdesp.MendatroryField = False
        Me.txtdesp.MyLinkLable1 = Me.MyLabel19
        Me.txtdesp.MyLinkLable2 = Nothing
        Me.txtdesp.Name = "txtdesp"
        Me.txtdesp.Size = New System.Drawing.Size(475, 20)
        Me.txtdesp.TabIndex = 6
        '
        'MyLabel19
        '
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(9, 99)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel19.TabIndex = 114
        Me.MyLabel19.Text = "Description"
        '
        'txtcode
        '
        Me.txtcode.Location = New System.Drawing.Point(117, 9)
        Me.txtcode.MendatroryField = True
        Me.txtcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcode.MyLinkLable1 = Me.lblMCCCode
        Me.txtcode.MyLinkLable2 = Nothing
        Me.txtcode.MyMaxLength = 30
        Me.txtcode.MyReadOnly = False
        Me.txtcode.Name = "txtcode"
        Me.txtcode.Size = New System.Drawing.Size(199, 21)
        Me.txtcode.TabIndex = 0
        Me.txtcode.Value = ""
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(317, 9)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(14, 21)
        Me.btnnew.TabIndex = 1
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MM/yyyy "
        Me.dtpDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(505, 10)
        Me.dtpDate.MendatroryField = True
        Me.dtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.MyLinkLable1 = Me.MyLabel12
        Me.dtpDate.MyLinkLable2 = Nothing
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.Size = New System.Drawing.Size(85, 18)
        Me.dtpDate.TabIndex = 2
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "03/05/2011 "
        Me.dtpDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel12
        '
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel12.Location = New System.Drawing.Point(462, 11)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel12.TabIndex = 113
        Me.MyLabel12.Text = "Date"
        '
        'txtsourcetype
        '
        Me.txtsourcetype.Location = New System.Drawing.Point(116, 54)
        Me.txtsourcetype.MendatroryField = True
        Me.txtsourcetype.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsourcetype.MyLinkLable1 = Me.lblChillingVendor
        Me.txtsourcetype.MyLinkLable2 = Nothing
        Me.txtsourcetype.MyReadOnly = False
        Me.txtsourcetype.MyShowMasterFormButton = False
        Me.txtsourcetype.Name = "txtsourcetype"
        Me.txtsourcetype.Size = New System.Drawing.Size(182, 19)
        Me.txtsourcetype.TabIndex = 4
        Me.txtsourcetype.Value = ""
        '
        'lblChillingVendor
        '
        Me.lblChillingVendor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChillingVendor.Location = New System.Drawing.Point(9, 56)
        Me.lblChillingVendor.Name = "lblChillingVendor"
        Me.lblChillingVendor.Size = New System.Drawing.Size(74, 16)
        Me.lblChillingVendor.TabIndex = 9
        Me.lblChillingVendor.Text = "Source Type "
        '
        'lblsourcedetail
        '
        Me.lblsourcedetail.AutoSize = False
        Me.lblsourcedetail.BorderVisible = True
        Me.lblsourcedetail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsourcedetail.Location = New System.Drawing.Point(301, 77)
        Me.lblsourcedetail.Name = "lblsourcedetail"
        Me.lblsourcedetail.Size = New System.Drawing.Size(289, 18)
        Me.lblsourcedetail.TabIndex = 111
        Me.lblsourcedetail.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblsourcedetail.TextWrap = False
        '
        'txtsourcedetail
        '
        Me.txtsourcedetail.Location = New System.Drawing.Point(116, 76)
        Me.txtsourcedetail.MendatroryField = True
        Me.txtsourcedetail.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsourcedetail.MyLinkLable1 = Me.MyLabel43
        Me.txtsourcedetail.MyLinkLable2 = Nothing
        Me.txtsourcedetail.MyReadOnly = False
        Me.txtsourcedetail.MyShowMasterFormButton = False
        Me.txtsourcedetail.Name = "txtsourcedetail"
        Me.txtsourcedetail.Size = New System.Drawing.Size(182, 19)
        Me.txtsourcedetail.TabIndex = 5
        Me.txtsourcedetail.Value = ""
        '
        'MyLabel43
        '
        Me.MyLabel43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel43.Location = New System.Drawing.Point(9, 78)
        Me.MyLabel43.Name = "MyLabel43"
        Me.MyLabel43.Size = New System.Drawing.Size(103, 16)
        Me.MyLabel43.TabIndex = 102
        Me.MyLabel43.Text = "Source Type Detail"
        '
        'lblsourcetype
        '
        Me.lblsourcetype.AutoSize = False
        Me.lblsourcetype.BorderVisible = True
        Me.lblsourcetype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsourcetype.Location = New System.Drawing.Point(301, 54)
        Me.lblsourcetype.Name = "lblsourcetype"
        Me.lblsourcetype.Size = New System.Drawing.Size(289, 18)
        Me.lblsourcetype.TabIndex = 110
        Me.lblsourcetype.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblsourcetype.TextWrap = False
        '
        'lblrequisition
        '
        Me.lblrequisition.AutoSize = False
        Me.lblrequisition.BorderVisible = True
        Me.lblrequisition.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblrequisition.Location = New System.Drawing.Point(301, 33)
        Me.lblrequisition.Name = "lblrequisition"
        Me.lblrequisition.Size = New System.Drawing.Size(289, 18)
        Me.lblrequisition.TabIndex = 109
        Me.lblrequisition.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblrequisition.TextWrap = False
        '
        'txtrequisitioncode
        '
        Me.txtrequisitioncode.Location = New System.Drawing.Point(117, 33)
        Me.txtrequisitioncode.MendatroryField = True
        Me.txtrequisitioncode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtrequisitioncode.MyLinkLable1 = Me.MyLabel44
        Me.txtrequisitioncode.MyLinkLable2 = Nothing
        Me.txtrequisitioncode.MyReadOnly = False
        Me.txtrequisitioncode.MyShowMasterFormButton = False
        Me.txtrequisitioncode.Name = "txtrequisitioncode"
        Me.txtrequisitioncode.Size = New System.Drawing.Size(182, 19)
        Me.txtrequisitioncode.TabIndex = 3
        Me.txtrequisitioncode.Value = ""
        '
        'MyLabel44
        '
        Me.MyLabel44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel44.Location = New System.Drawing.Point(9, 33)
        Me.MyLabel44.Name = "MyLabel44"
        Me.MyLabel44.Size = New System.Drawing.Size(92, 16)
        Me.MyLabel44.TabIndex = 105
        Me.MyLabel44.Text = "Requisition Code"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtTelephone)
        Me.RadGroupBox1.Controls.Add(Me.txtPinCode)
        Me.RadGroupBox1.Controls.Add(Me.txtadd3)
        Me.RadGroupBox1.Controls.Add(Me.txtadd4)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel15)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel16)
        Me.RadGroupBox1.Controls.Add(Me.lblCity)
        Me.RadGroupBox1.Controls.Add(Me.lblState)
        Me.RadGroupBox1.Controls.Add(Me.lblCountry)
        Me.RadGroupBox1.Controls.Add(Me.txtAdd1)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox1.Controls.Add(Me.txtEmail)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtCity)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtState)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtCountry)
        Me.RadGroupBox1.Controls.Add(Me.txtAdd2)
        Me.RadGroupBox1.Controls.Add(Me.lblAdd2)
        Me.RadGroupBox1.Controls.Add(Me.lblAdd1)
        Me.RadGroupBox1.HeaderText = "Address Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(449, 1)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(497, 239)
        Me.RadGroupBox1.TabIndex = 18
        Me.RadGroupBox1.Text = "Address Details"
        '
        'txtTelephone
        '
        Me.txtTelephone.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtTelephone.Location = New System.Drawing.Point(355, 183)
        Me.txtTelephone.Mask = "(+99)0000000000"
        Me.txtTelephone.MaskType = Telerik.WinControls.UI.MaskType.Standard
        Me.txtTelephone.Name = "txtTelephone"
        Me.txtTelephone.Size = New System.Drawing.Size(134, 20)
        Me.txtTelephone.TabIndex = 61
        Me.txtTelephone.TabStop = False
        Me.txtTelephone.Text = "(+__)__________"
        '
        'txtPinCode
        '
        Me.txtPinCode.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtPinCode.DecimalPlaces = 0
        Me.txtPinCode.Location = New System.Drawing.Point(101, 183)
        Me.txtPinCode.MaxLength = 6
        Me.txtPinCode.MendatroryField = False
        Me.txtPinCode.MyLinkLable1 = Me.MyLabel7
        Me.txtPinCode.MyLinkLable2 = Nothing
        Me.txtPinCode.Name = "txtPinCode"
        Me.txtPinCode.Size = New System.Drawing.Size(118, 20)
        Me.txtPinCode.TabIndex = 26
        Me.txtPinCode.Text = "0"
        Me.txtPinCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPinCode.Value = 0
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(10, 185)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(53, 16)
        Me.MyLabel7.TabIndex = 52
        Me.MyLabel7.Text = "Pin Code"
        '
        'txtadd3
        '
        Me.txtadd3.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtadd3.Location = New System.Drawing.Point(101, 67)
        Me.txtadd3.MaxLength = 150
        Me.txtadd3.MendatroryField = False
        Me.txtadd3.MyLinkLable1 = Me.MyLabel16
        Me.txtadd3.MyLinkLable2 = Nothing
        Me.txtadd3.Name = "txtadd3"
        Me.txtadd3.Size = New System.Drawing.Size(388, 20)
        Me.txtadd3.TabIndex = 21
        '
        'MyLabel16
        '
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(10, 69)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel16.TabIndex = 59
        Me.MyLabel16.Text = "Address Line 3"
        '
        'txtadd4
        '
        Me.txtadd4.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtadd4.Location = New System.Drawing.Point(101, 90)
        Me.txtadd4.MaxLength = 150
        Me.txtadd4.MendatroryField = False
        Me.txtadd4.MyLinkLable1 = Me.MyLabel15
        Me.txtadd4.MyLinkLable2 = Nothing
        Me.txtadd4.Name = "txtadd4"
        Me.txtadd4.Size = New System.Drawing.Size(388, 20)
        Me.txtadd4.TabIndex = 22
        '
        'MyLabel15
        '
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(10, 92)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel15.TabIndex = 60
        Me.MyLabel15.Text = "Address Line 4"
        '
        'lblCity
        '
        Me.lblCity.AutoSize = False
        Me.lblCity.BorderVisible = True
        Me.lblCity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCity.Location = New System.Drawing.Point(225, 158)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(264, 18)
        Me.lblCity.TabIndex = 57
        Me.lblCity.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCity.TextWrap = False
        '
        'lblState
        '
        Me.lblState.AutoSize = False
        Me.lblState.BorderVisible = True
        Me.lblState.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblState.Location = New System.Drawing.Point(225, 137)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(264, 18)
        Me.lblState.TabIndex = 56
        Me.lblState.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblState.TextWrap = False
        '
        'lblCountry
        '
        Me.lblCountry.AutoSize = False
        Me.lblCountry.BorderVisible = True
        Me.lblCountry.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCountry.Location = New System.Drawing.Point(225, 114)
        Me.lblCountry.Name = "lblCountry"
        Me.lblCountry.Size = New System.Drawing.Size(264, 18)
        Me.lblCountry.TabIndex = 55
        Me.lblCountry.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCountry.TextWrap = False
        '
        'txtAdd1
        '
        Me.txtAdd1.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtAdd1.Location = New System.Drawing.Point(101, 21)
        Me.txtAdd1.MaxLength = 150
        Me.txtAdd1.MendatroryField = True
        Me.txtAdd1.MyLinkLable1 = Me.lblAdd1
        Me.txtAdd1.MyLinkLable2 = Nothing
        Me.txtAdd1.Name = "txtAdd1"
        Me.txtAdd1.Size = New System.Drawing.Size(388, 20)
        Me.txtAdd1.TabIndex = 19
        '
        'lblAdd1
        '
        Me.lblAdd1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdd1.Location = New System.Drawing.Point(10, 23)
        Me.lblAdd1.Name = "lblAdd1"
        Me.lblAdd1.Size = New System.Drawing.Size(82, 16)
        Me.lblAdd1.TabIndex = 43
        Me.lblAdd1.Text = "Address Line 1"
        '
        'MyLabel9
        '
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(269, 185)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(80, 16)
        Me.MyLabel9.TabIndex = 53
        Me.MyLabel9.Text = "Telephone No."
        '
        'txtEmail
        '
        Me.txtEmail.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtEmail.Location = New System.Drawing.Point(101, 207)
        Me.txtEmail.MaxLength = 100
        Me.txtEmail.MendatroryField = True
        Me.txtEmail.MyLinkLable1 = Me.MyLabel6
        Me.txtEmail.MyLinkLable2 = Nothing
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(248, 20)
        Me.txtEmail.TabIndex = 28
        '
        'MyLabel6
        '
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(10, 209)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(34, 16)
        Me.MyLabel6.TabIndex = 51
        Me.MyLabel6.Text = "Email"
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(10, 162)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(26, 16)
        Me.MyLabel4.TabIndex = 49
        Me.MyLabel4.Text = "City"
        '
        'txtCity
        '
        Me.txtCity.Location = New System.Drawing.Point(101, 160)
        Me.txtCity.MendatroryField = True
        Me.txtCity.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCity.MyLinkLable1 = Me.MyLabel4
        Me.txtCity.MyLinkLable2 = Nothing
        Me.txtCity.MyReadOnly = False
        Me.txtCity.MyShowMasterFormButton = False
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(118, 19)
        Me.txtCity.TabIndex = 25
        Me.txtCity.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(10, 138)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel3.TabIndex = 47
        Me.MyLabel3.Text = "State"
        '
        'txtState
        '
        Me.txtState.Location = New System.Drawing.Point(101, 137)
        Me.txtState.MendatroryField = True
        Me.txtState.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtState.MyLinkLable1 = Me.MyLabel3
        Me.txtState.MyLinkLable2 = Nothing
        Me.txtState.MyReadOnly = False
        Me.txtState.MyShowMasterFormButton = False
        Me.txtState.Name = "txtState"
        Me.txtState.Size = New System.Drawing.Size(118, 19)
        Me.txtState.TabIndex = 24
        Me.txtState.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(10, 115)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel2.TabIndex = 45
        Me.MyLabel2.Text = "Country"
        '
        'txtCountry
        '
        Me.txtCountry.Location = New System.Drawing.Point(101, 114)
        Me.txtCountry.MendatroryField = True
        Me.txtCountry.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCountry.MyLinkLable1 = Me.MyLabel2
        Me.txtCountry.MyLinkLable2 = Nothing
        Me.txtCountry.MyReadOnly = False
        Me.txtCountry.MyShowMasterFormButton = False
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.Size = New System.Drawing.Size(118, 19)
        Me.txtCountry.TabIndex = 23
        Me.txtCountry.Value = ""
        '
        'txtAdd2
        '
        Me.txtAdd2.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtAdd2.Location = New System.Drawing.Point(101, 44)
        Me.txtAdd2.MaxLength = 150
        Me.txtAdd2.MendatroryField = False
        Me.txtAdd2.MyLinkLable1 = Me.lblAdd2
        Me.txtAdd2.MyLinkLable2 = Nothing
        Me.txtAdd2.Name = "txtAdd2"
        Me.txtAdd2.Size = New System.Drawing.Size(388, 20)
        Me.txtAdd2.TabIndex = 20
        '
        'lblAdd2
        '
        Me.lblAdd2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdd2.Location = New System.Drawing.Point(10, 46)
        Me.lblAdd2.Name = "lblAdd2"
        Me.lblAdd2.Size = New System.Drawing.Size(82, 16)
        Me.lblAdd2.TabIndex = 44
        Me.lblAdd2.Text = "Address Line 2"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.PicImage)
        Me.RadGroupBox2.Controls.Add(Me.btnclearphoto)
        Me.RadGroupBox2.Controls.Add(Me.btnPhotoBrowse)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel21)
        Me.RadGroupBox2.Controls.Add(Me.txtPanNo)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel20)
        Me.RadGroupBox2.Controls.Add(Me.CmbMarStatus)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel14)
        Me.RadGroupBox2.Controls.Add(Me.dtpDateofBirth)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel13)
        Me.RadGroupBox2.Controls.Add(Me.txtLastName)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel11)
        Me.RadGroupBox2.Controls.Add(Me.txtMiddleName)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox2.Controls.Add(Me.txtFirstName)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel10)
        Me.RadGroupBox2.Controls.Add(Me.cmbGender)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox2.Controls.Add(Me.cmbSalutation)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox2.Controls.Add(Me.dtpDateofIntr)
        Me.RadGroupBox2.Controls.Add(Me.lblPPDate)
        Me.RadGroupBox2.HeaderText = "Personal Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(7, 1)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(435, 240)
        Me.RadGroupBox2.TabIndex = 7
        Me.RadGroupBox2.Text = "Personal Details"
        '
        'PicImage
        '
        Me.PicImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PicImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PicImage.Location = New System.Drawing.Point(255, 18)
        Me.PicImage.Name = "PicImage"
        Me.PicImage.Size = New System.Drawing.Size(175, 199)
        Me.PicImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicImage.TabIndex = 158
        Me.PicImage.TabStop = False
        '
        'btnclearphoto
        '
        Me.btnclearphoto.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclearphoto.Location = New System.Drawing.Point(308, 220)
        Me.btnclearphoto.Name = "btnclearphoto"
        Me.btnclearphoto.Size = New System.Drawing.Size(66, 18)
        Me.btnclearphoto.TabIndex = 123
        Me.btnclearphoto.Text = "Clear Photo"
        '
        'btnPhotoBrowse
        '
        Me.btnPhotoBrowse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPhotoBrowse.Location = New System.Drawing.Point(102, 220)
        Me.btnPhotoBrowse.Name = "btnPhotoBrowse"
        Me.btnPhotoBrowse.Size = New System.Drawing.Size(66, 18)
        Me.btnPhotoBrowse.TabIndex = 17
        Me.btnPhotoBrowse.Text = "Browse"
        '
        'MyLabel21
        '
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(10, 222)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel21.TabIndex = 120
        Me.MyLabel21.Text = "Applicant Photo"
        '
        'txtPanNo
        '
        Me.txtPanNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtPanNo.Location = New System.Drawing.Point(102, 197)
        Me.txtPanNo.MaxLength = 10
        Me.txtPanNo.MendatroryField = True
        Me.txtPanNo.MyLinkLable1 = Me.MyLabel20
        Me.txtPanNo.MyLinkLable2 = Nothing
        Me.txtPanNo.Name = "txtPanNo"
        Me.txtPanNo.Size = New System.Drawing.Size(147, 20)
        Me.txtPanNo.TabIndex = 16
        '
        'MyLabel20
        '
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(10, 199)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(50, 16)
        Me.MyLabel20.TabIndex = 119
        Me.MyLabel20.Text = "PAN No."
        '
        'CmbMarStatus
        '
        Me.CmbMarStatus.AllowShowFocusCues = False
        Me.CmbMarStatus.AutoCompleteDisplayMember = Nothing
        Me.CmbMarStatus.AutoCompleteValueMember = Nothing
        Me.CmbMarStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CmbMarStatus.Location = New System.Drawing.Point(102, 175)
        Me.CmbMarStatus.MendatroryField = True
        Me.CmbMarStatus.MyLinkLable1 = Me.MyLabel14
        Me.CmbMarStatus.MyLinkLable2 = Nothing
        Me.CmbMarStatus.Name = "CmbMarStatus"
        Me.CmbMarStatus.Size = New System.Drawing.Size(107, 20)
        Me.CmbMarStatus.TabIndex = 15
        '
        'MyLabel14
        '
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(10, 177)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(78, 16)
        Me.MyLabel14.TabIndex = 117
        Me.MyLabel14.Text = "Maritial Status"
        '
        'dtpDateofBirth
        '
        Me.dtpDateofBirth.CustomFormat = "dd/MM/yyyy "
        Me.dtpDateofBirth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDateofBirth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateofBirth.Location = New System.Drawing.Point(102, 154)
        Me.dtpDateofBirth.MendatroryField = True
        Me.dtpDateofBirth.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDateofBirth.MyLinkLable1 = Me.MyLabel13
        Me.dtpDateofBirth.MyLinkLable2 = Nothing
        Me.dtpDateofBirth.Name = "dtpDateofBirth"
        Me.dtpDateofBirth.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDateofBirth.Size = New System.Drawing.Size(108, 18)
        Me.dtpDateofBirth.TabIndex = 14
        Me.dtpDateofBirth.TabStop = False
        Me.dtpDateofBirth.Text = "03/05/2011 "
        Me.dtpDateofBirth.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel13
        '
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel13.Location = New System.Drawing.Point(10, 155)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel13.TabIndex = 115
        Me.MyLabel13.Text = "Date of Birth"
        '
        'txtLastName
        '
        Me.txtLastName.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtLastName.Location = New System.Drawing.Point(102, 131)
        Me.txtLastName.MaxLength = 100
        Me.txtLastName.MendatroryField = True
        Me.txtLastName.MyLinkLable1 = Me.MyLabel11
        Me.txtLastName.MyLinkLable2 = Nothing
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(147, 20)
        Me.txtLastName.TabIndex = 13
        '
        'MyLabel11
        '
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(10, 133)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel11.TabIndex = 66
        Me.MyLabel11.Text = "Last Name"
        '
        'txtMiddleName
        '
        Me.txtMiddleName.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtMiddleName.Location = New System.Drawing.Point(102, 109)
        Me.txtMiddleName.MaxLength = 100
        Me.txtMiddleName.MendatroryField = False
        Me.txtMiddleName.MyLinkLable1 = Me.MyLabel8
        Me.txtMiddleName.MyLinkLable2 = Nothing
        Me.txtMiddleName.Name = "txtMiddleName"
        Me.txtMiddleName.Size = New System.Drawing.Size(147, 20)
        Me.txtMiddleName.TabIndex = 12
        '
        'MyLabel8
        '
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(10, 112)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(73, 16)
        Me.MyLabel8.TabIndex = 64
        Me.MyLabel8.Text = "Middle Name"
        '
        'txtFirstName
        '
        Me.txtFirstName.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtFirstName.Location = New System.Drawing.Point(102, 86)
        Me.txtFirstName.MaxLength = 100
        Me.txtFirstName.MendatroryField = True
        Me.txtFirstName.MyLinkLable1 = Me.MyLabel10
        Me.txtFirstName.MyLinkLable2 = Nothing
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(147, 20)
        Me.txtFirstName.TabIndex = 11
        '
        'MyLabel10
        '
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(10, 88)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel10.TabIndex = 63
        Me.MyLabel10.Text = "First Name"
        '
        'cmbGender
        '
        Me.cmbGender.AllowShowFocusCues = False
        Me.cmbGender.AutoCompleteDisplayMember = Nothing
        Me.cmbGender.AutoCompleteValueMember = Nothing
        Me.cmbGender.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbGender.Location = New System.Drawing.Point(102, 41)
        Me.cmbGender.MendatroryField = True
        Me.cmbGender.MyLinkLable1 = Me.MyLabel5
        Me.cmbGender.MyLinkLable2 = Nothing
        Me.cmbGender.Name = "cmbGender"
        Me.cmbGender.Size = New System.Drawing.Size(107, 20)
        Me.cmbGender.TabIndex = 9
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(10, 43)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel5.TabIndex = 60
        Me.MyLabel5.Text = "Gender"
        '
        'cmbSalutation
        '
        Me.cmbSalutation.AllowShowFocusCues = False
        Me.cmbSalutation.AutoCompleteDisplayMember = Nothing
        Me.cmbSalutation.AutoCompleteValueMember = Nothing
        Me.cmbSalutation.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbSalutation.Location = New System.Drawing.Point(102, 63)
        Me.cmbSalutation.MendatroryField = True
        Me.cmbSalutation.MyLinkLable1 = Me.MyLabel1
        Me.cmbSalutation.MyLinkLable2 = Nothing
        Me.cmbSalutation.Name = "cmbSalutation"
        Me.cmbSalutation.Size = New System.Drawing.Size(108, 20)
        Me.cmbSalutation.TabIndex = 10
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(10, 65)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel1.TabIndex = 58
        Me.MyLabel1.Text = "Salutation"
        '
        'dtpDateofIntr
        '
        Me.dtpDateofIntr.CustomFormat = "dd/MM/yyyy "
        Me.dtpDateofIntr.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDateofIntr.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateofIntr.Location = New System.Drawing.Point(102, 20)
        Me.dtpDateofIntr.MendatroryField = True
        Me.dtpDateofIntr.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDateofIntr.MyLinkLable1 = Me.lblPPDate
        Me.dtpDateofIntr.MyLinkLable2 = Nothing
        Me.dtpDateofIntr.Name = "dtpDateofIntr"
        Me.dtpDateofIntr.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDateofIntr.Size = New System.Drawing.Size(107, 18)
        Me.dtpDateofIntr.TabIndex = 8
        Me.dtpDateofIntr.TabStop = False
        Me.dtpDateofIntr.Text = "03/05/2011 "
        Me.dtpDateofIntr.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblPPDate
        '
        Me.lblPPDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblPPDate.Location = New System.Drawing.Point(10, 21)
        Me.lblPPDate.Name = "lblPPDate"
        Me.lblPPDate.Size = New System.Drawing.Size(91, 16)
        Me.lblPPDate.TabIndex = 56
        Me.lblPPDate.Text = "Date of Interview"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(116.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(949, 371)
        Me.RadPageViewPage3.Text = "Qualification Details"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.gvQualification)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.MyLabel22)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnShowDoc)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnBrowse)
        Me.SplitContainer2.Panel2.Controls.Add(Me.BtnDeleteDoc)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtResume)
        Me.SplitContainer2.Panel2.Controls.Add(Me.BtnDocReset)
        Me.SplitContainer2.Size = New System.Drawing.Size(949, 371)
        Me.SplitContainer2.SplitterDistance = 324
        Me.SplitContainer2.TabIndex = 131
        '
        'gvQualification
        '
        Me.gvQualification.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvQualification.Location = New System.Drawing.Point(0, 0)
        Me.gvQualification.Name = "gvQualification"
        Me.gvQualification.Size = New System.Drawing.Size(949, 324)
        Me.gvQualification.TabIndex = 0
        '
        'MyLabel22
        '
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel22.Location = New System.Drawing.Point(5, 15)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel22.TabIndex = 125
        Me.MyLabel22.Text = "Resume"
        '
        'btnShowDoc
        '
        Me.btnShowDoc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnShowDoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowDoc.Location = New System.Drawing.Point(535, 17)
        Me.btnShowDoc.Name = "btnShowDoc"
        Me.btnShowDoc.Size = New System.Drawing.Size(66, 18)
        Me.btnShowDoc.TabIndex = 3
        Me.btnShowDoc.Text = "Show"
        '
        'btnBrowse
        '
        Me.btnBrowse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(55, 14)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(66, 18)
        Me.btnBrowse.TabIndex = 1
        Me.btnBrowse.Text = "Browse"
        '
        'BtnDeleteDoc
        '
        Me.BtnDeleteDoc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnDeleteDoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDeleteDoc.Location = New System.Drawing.Point(604, 17)
        Me.BtnDeleteDoc.Name = "BtnDeleteDoc"
        Me.BtnDeleteDoc.Size = New System.Drawing.Size(66, 18)
        Me.BtnDeleteDoc.TabIndex = 130
        Me.BtnDeleteDoc.Text = "Delete"
        '
        'txtResume
        '
        Me.txtResume.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtResume.Location = New System.Drawing.Point(127, 13)
        Me.txtResume.MendatroryField = False
        Me.txtResume.MyLinkLable1 = Me.MyLabel22
        Me.txtResume.MyLinkLable2 = Nothing
        Me.txtResume.Name = "txtResume"
        Me.txtResume.Size = New System.Drawing.Size(388, 20)
        Me.txtResume.TabIndex = 2
        '
        'BtnDocReset
        '
        Me.BtnDocReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnDocReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDocReset.Location = New System.Drawing.Point(880, 17)
        Me.BtnDocReset.Name = "BtnDocReset"
        Me.BtnDocReset.Size = New System.Drawing.Size(66, 18)
        Me.BtnDocReset.TabIndex = 4
        Me.BtnDocReset.Text = "Reset"
        Me.BtnDocReset.Visible = False
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.gvDoc)
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(121.0!, 28.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(949, 371)
        Me.RadPageViewPage4.Text = "Document Check List"
        '
        'gvDoc
        '
        Me.gvDoc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDoc.Location = New System.Drawing.Point(0, 0)
        Me.gvDoc.Name = "gvDoc"
        Me.gvDoc.Size = New System.Drawing.Size(949, 371)
        Me.gvDoc.TabIndex = 1
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.ChkFresher)
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(118.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(949, 371)
        Me.RadPageViewPage2.Text = "Employment History"
        '
        'ChkFresher
        '
        Me.ChkFresher.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkFresher.Location = New System.Drawing.Point(23, 2)
        Me.ChkFresher.Name = "ChkFresher"
        Me.ChkFresher.Size = New System.Drawing.Size(62, 16)
        Me.ChkFresher.TabIndex = 0
        Me.ChkFresher.Text = "Fresher"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox5.Controls.Add(Me.gvEmpHis)
        Me.RadGroupBox5.HeaderText = ""
        Me.RadGroupBox5.Location = New System.Drawing.Point(9, 9)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Size = New System.Drawing.Size(931, 362)
        Me.RadGroupBox5.TabIndex = 112
        '
        'gvEmpHis
        '
        Me.gvEmpHis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvEmpHis.Location = New System.Drawing.Point(2, 18)
        Me.gvEmpHis.Name = "gvEmpHis"
        Me.gvEmpHis.Size = New System.Drawing.Size(927, 342)
        Me.gvEmpHis.TabIndex = 1
        '
        'RadPageViewPage6
        '
        Me.RadPageViewPage6.Controls.Add(Me.LblAgency)
        Me.RadPageViewPage6.Controls.Add(Me.MyLabel23)
        Me.RadPageViewPage6.Controls.Add(Me.txtAgency)
        Me.RadPageViewPage6.Controls.Add(Me.lblRelation)
        Me.RadPageViewPage6.Controls.Add(Me.lblEmpName)
        Me.RadPageViewPage6.Controls.Add(Me.LblRefBy)
        Me.RadPageViewPage6.Controls.Add(Me.txtEmpCode)
        Me.RadPageViewPage6.Controls.Add(Me.MyLabel18)
        Me.RadPageViewPage6.Controls.Add(Me.txtRelation)
        Me.RadPageViewPage6.Controls.Add(Me.rbnrefbyAge)
        Me.RadPageViewPage6.Controls.Add(Me.rbnRefbyEmp)
        Me.RadPageViewPage6.ItemSize = New System.Drawing.SizeF(71.0!, 28.0!)
        Me.RadPageViewPage6.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage6.Name = "RadPageViewPage6"
        Me.RadPageViewPage6.Size = New System.Drawing.Size(949, 371)
        Me.RadPageViewPage6.Text = "References"
        '
        'LblAgency
        '
        Me.LblAgency.AutoSize = False
        Me.LblAgency.BorderVisible = True
        Me.LblAgency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAgency.Location = New System.Drawing.Point(265, 121)
        Me.LblAgency.Name = "LblAgency"
        Me.LblAgency.Size = New System.Drawing.Size(289, 18)
        Me.LblAgency.TabIndex = 119
        Me.LblAgency.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblAgency.TextWrap = False
        '
        'MyLabel23
        '
        Me.MyLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel23.Location = New System.Drawing.Point(3, 122)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel23.TabIndex = 118
        Me.MyLabel23.Text = "Agency Code"
        '
        'txtAgency
        '
        Me.txtAgency.Location = New System.Drawing.Point(81, 121)
        Me.txtAgency.MendatroryField = False
        Me.txtAgency.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAgency.MyLinkLable1 = Me.MyLabel23
        Me.txtAgency.MyLinkLable2 = Nothing
        Me.txtAgency.MyReadOnly = False
        Me.txtAgency.MyShowMasterFormButton = False
        Me.txtAgency.Name = "txtAgency"
        Me.txtAgency.Size = New System.Drawing.Size(182, 19)
        Me.txtAgency.TabIndex = 4
        Me.txtAgency.Value = ""
        '
        'lblRelation
        '
        Me.lblRelation.AutoSize = False
        Me.lblRelation.BorderVisible = True
        Me.lblRelation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRelation.Location = New System.Drawing.Point(254, 57)
        Me.lblRelation.Name = "lblRelation"
        Me.lblRelation.Size = New System.Drawing.Size(289, 18)
        Me.lblRelation.TabIndex = 116
        Me.lblRelation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblRelation.TextWrap = False
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpName.Location = New System.Drawing.Point(254, 35)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(289, 18)
        Me.lblEmpName.TabIndex = 115
        Me.lblEmpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblEmpName.TextWrap = False
        '
        'LblRefBy
        '
        Me.LblRefBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRefBy.Location = New System.Drawing.Point(3, 36)
        Me.LblRefBy.Name = "LblRefBy"
        Me.LblRefBy.Size = New System.Drawing.Size(60, 16)
        Me.LblRefBy.TabIndex = 114
        Me.LblRefBy.Text = "Emp Code"
        '
        'txtEmpCode
        '
        Me.txtEmpCode.Location = New System.Drawing.Point(70, 35)
        Me.txtEmpCode.MendatroryField = False
        Me.txtEmpCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.MyLinkLable1 = Me.LblRefBy
        Me.txtEmpCode.MyLinkLable2 = Nothing
        Me.txtEmpCode.MyReadOnly = False
        Me.txtEmpCode.MyShowMasterFormButton = False
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.Size = New System.Drawing.Size(182, 19)
        Me.txtEmpCode.TabIndex = 1
        Me.txtEmpCode.Value = ""
        '
        'MyLabel18
        '
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(3, 60)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel18.TabIndex = 112
        Me.MyLabel18.Text = "Relation"
        '
        'txtRelation
        '
        Me.txtRelation.Location = New System.Drawing.Point(70, 57)
        Me.txtRelation.MendatroryField = False
        Me.txtRelation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRelation.MyLinkLable1 = Me.MyLabel18
        Me.txtRelation.MyLinkLable2 = Nothing
        Me.txtRelation.MyReadOnly = False
        Me.txtRelation.MyShowMasterFormButton = False
        Me.txtRelation.Name = "txtRelation"
        Me.txtRelation.Size = New System.Drawing.Size(182, 19)
        Me.txtRelation.TabIndex = 2
        Me.txtRelation.Value = ""
        '
        'rbnrefbyAge
        '
        Me.rbnrefbyAge.Location = New System.Drawing.Point(35, 95)
        Me.rbnrefbyAge.MyLinkLable1 = Nothing
        Me.rbnrefbyAge.MyLinkLable2 = Nothing
        Me.rbnrefbyAge.Name = "rbnrefbyAge"
        Me.rbnrefbyAge.Size = New System.Drawing.Size(119, 18)
        Me.rbnrefbyAge.TabIndex = 3
        Me.rbnrefbyAge.Text = "Refrence By Agency"
        '
        'rbnRefbyEmp
        '
        Me.rbnRefbyEmp.Location = New System.Drawing.Point(35, 9)
        Me.rbnRefbyEmp.MyLinkLable1 = Nothing
        Me.rbnRefbyEmp.MyLinkLable2 = Nothing
        Me.rbnRefbyEmp.Name = "rbnRefbyEmp"
        Me.rbnRefbyEmp.Size = New System.Drawing.Size(131, 18)
        Me.rbnRefbyEmp.TabIndex = 0
        Me.rbnRefbyEmp.Text = "Refrence by Employee"
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.SplitContainer4)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(107.0!, 28.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(949, 371)
        Me.RadPageViewPage5.Text = "Other Information"
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.gvFamily)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.ChkHandicaped)
        Me.SplitContainer4.Panel2.Controls.Add(Me.RadGroupBox3)
        Me.SplitContainer4.Size = New System.Drawing.Size(949, 371)
        Me.SplitContainer4.SplitterDistance = 183
        Me.SplitContainer4.TabIndex = 109
        '
        'gvFamily
        '
        Me.gvFamily.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvFamily.Location = New System.Drawing.Point(0, 0)
        Me.gvFamily.Name = "gvFamily"
        Me.gvFamily.Size = New System.Drawing.Size(949, 183)
        Me.gvFamily.TabIndex = 0
        '
        'ChkHandicaped
        '
        Me.ChkHandicaped.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkHandicaped.Location = New System.Drawing.Point(14, 7)
        Me.ChkHandicaped.Name = "ChkHandicaped"
        Me.ChkHandicaped.Size = New System.Drawing.Size(84, 16)
        Me.ChkHandicaped.TabIndex = 1
        Me.ChkHandicaped.Text = "Handicaped"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox3.Controls.Add(Me.txtTotalCTC)
        Me.RadGroupBox3.Controls.Add(Me.txtCurrGrossSal)
        Me.RadGroupBox3.Controls.Add(Me.LblPreLoc)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel40)
        Me.RadGroupBox3.Controls.Add(Me.txtPreferedLoc)
        Me.RadGroupBox3.Controls.Add(Me.LblLocation)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel48)
        Me.RadGroupBox3.Controls.Add(Me.txtLocation)
        Me.RadGroupBox3.Controls.Add(Me.txtPerBy)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel24)
        Me.RadGroupBox3.Controls.Add(Me.ChkRelocation)
        Me.RadGroupBox3.Controls.Add(Me.grpReLoc)
        Me.RadGroupBox3.Controls.Add(Me.lblBranchName)
        Me.RadGroupBox3.Controls.Add(Me.LblBankName)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel27)
        Me.RadGroupBox3.Controls.Add(Me.txtAccNo)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel28)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel29)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel31)
        Me.RadGroupBox3.Controls.Add(Me.txtBranchCode)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel32)
        Me.RadGroupBox3.Controls.Add(Me.txtBankCode)
        Me.RadGroupBox3.Controls.Add(Me.txtBloodGrp)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel33)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(949, 184)
        Me.RadGroupBox3.TabIndex = 108
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.txtHandiDetail)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel41)
        Me.RadGroupBox4.HeaderText = ""
        Me.RadGroupBox4.Location = New System.Drawing.Point(2, 15)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Size = New System.Drawing.Size(426, 34)
        Me.RadGroupBox4.TabIndex = 120
        '
        'txtHandiDetail
        '
        Me.txtHandiDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtHandiDetail.Location = New System.Drawing.Point(122, 10)
        Me.txtHandiDetail.MaxLength = 150
        Me.txtHandiDetail.MendatroryField = True
        Me.txtHandiDetail.MyLinkLable1 = Me.MyLabel41
        Me.txtHandiDetail.MyLinkLable2 = Nothing
        Me.txtHandiDetail.Name = "txtHandiDetail"
        Me.txtHandiDetail.Size = New System.Drawing.Size(298, 20)
        Me.txtHandiDetail.TabIndex = 2
        '
        'MyLabel41
        '
        Me.MyLabel41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel41.Location = New System.Drawing.Point(5, 12)
        Me.MyLabel41.Name = "MyLabel41"
        Me.MyLabel41.Size = New System.Drawing.Size(99, 16)
        Me.MyLabel41.TabIndex = 43
        Me.MyLabel41.Text = "Handicaped Detail"
        '
        'txtTotalCTC
        '
        Me.txtTotalCTC.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTotalCTC.DecimalPlaces = 3
        Me.txtTotalCTC.Location = New System.Drawing.Point(314, 146)
        Me.txtTotalCTC.MaxLength = 10
        Me.txtTotalCTC.MendatroryField = True
        Me.txtTotalCTC.MyLinkLable1 = Me.MyLabel29
        Me.txtTotalCTC.MyLinkLable2 = Nothing
        Me.txtTotalCTC.Name = "txtTotalCTC"
        Me.txtTotalCTC.Size = New System.Drawing.Size(111, 20)
        Me.txtTotalCTC.TabIndex = 8
        Me.txtTotalCTC.Text = "0"
        Me.txtTotalCTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalCTC.Value = 0
        '
        'MyLabel29
        '
        Me.MyLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel29.Location = New System.Drawing.Point(251, 148)
        Me.MyLabel29.Name = "MyLabel29"
        Me.MyLabel29.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel29.TabIndex = 51
        Me.MyLabel29.Text = "Total CTC"
        '
        'txtCurrGrossSal
        '
        Me.txtCurrGrossSal.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCurrGrossSal.DecimalPlaces = 3
        Me.txtCurrGrossSal.Location = New System.Drawing.Point(127, 146)
        Me.txtCurrGrossSal.MaxLength = 10
        Me.txtCurrGrossSal.MendatroryField = True
        Me.txtCurrGrossSal.MyLinkLable1 = Me.MyLabel27
        Me.txtCurrGrossSal.MyLinkLable2 = Nothing
        Me.txtCurrGrossSal.Name = "txtCurrGrossSal"
        Me.txtCurrGrossSal.Size = New System.Drawing.Size(118, 20)
        Me.txtCurrGrossSal.TabIndex = 7
        Me.txtCurrGrossSal.Text = "0"
        Me.txtCurrGrossSal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCurrGrossSal.Value = 0
        '
        'MyLabel27
        '
        Me.MyLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel27.Location = New System.Drawing.Point(10, 148)
        Me.MyLabel27.Name = "MyLabel27"
        Me.MyLabel27.Size = New System.Drawing.Size(113, 16)
        Me.MyLabel27.TabIndex = 53
        Me.MyLabel27.Text = "Current Gross Salary"
        '
        'LblPreLoc
        '
        Me.LblPreLoc.AutoSize = False
        Me.LblPreLoc.BorderVisible = True
        Me.LblPreLoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPreLoc.Location = New System.Drawing.Point(666, 123)
        Me.LblPreLoc.Name = "LblPreLoc"
        Me.LblPreLoc.Size = New System.Drawing.Size(264, 17)
        Me.LblPreLoc.TabIndex = 119
        Me.LblPreLoc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblPreLoc.TextWrap = False
        '
        'MyLabel40
        '
        Me.MyLabel40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel40.Location = New System.Drawing.Point(437, 124)
        Me.MyLabel40.Name = "MyLabel40"
        Me.MyLabel40.Size = New System.Drawing.Size(96, 16)
        Me.MyLabel40.TabIndex = 118
        Me.MyLabel40.Text = "Prefered Location"
        '
        'txtPreferedLoc
        '
        Me.txtPreferedLoc.Location = New System.Drawing.Point(542, 123)
        Me.txtPreferedLoc.MendatroryField = True
        Me.txtPreferedLoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPreferedLoc.MyLinkLable1 = Me.MyLabel40
        Me.txtPreferedLoc.MyLinkLable2 = Nothing
        Me.txtPreferedLoc.MyReadOnly = False
        Me.txtPreferedLoc.MyShowMasterFormButton = False
        Me.txtPreferedLoc.Name = "txtPreferedLoc"
        Me.txtPreferedLoc.Size = New System.Drawing.Size(118, 18)
        Me.txtPreferedLoc.TabIndex = 14
        Me.txtPreferedLoc.Value = ""
        '
        'LblLocation
        '
        Me.LblLocation.AutoSize = False
        Me.LblLocation.BorderVisible = True
        Me.LblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLocation.Location = New System.Drawing.Point(666, 101)
        Me.LblLocation.Name = "LblLocation"
        Me.LblLocation.Size = New System.Drawing.Size(264, 18)
        Me.LblLocation.TabIndex = 116
        Me.LblLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblLocation.TextWrap = False
        '
        'MyLabel48
        '
        Me.MyLabel48.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel48.Location = New System.Drawing.Point(438, 102)
        Me.MyLabel48.Name = "MyLabel48"
        Me.MyLabel48.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel48.TabIndex = 115
        Me.MyLabel48.Text = "Location"
        '
        'txtLocation
        '
        Me.txtLocation.Location = New System.Drawing.Point(542, 101)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.MyLabel48
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(118, 19)
        Me.txtLocation.TabIndex = 13
        Me.txtLocation.Value = ""
        '
        'txtPerBy
        '
        Me.txtPerBy.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtPerBy.Location = New System.Drawing.Point(542, 77)
        Me.txtPerBy.MaxLength = 150
        Me.txtPerBy.MendatroryField = False
        Me.txtPerBy.MyLinkLable1 = Me.MyLabel24
        Me.txtPerBy.MyLinkLable2 = Nothing
        Me.txtPerBy.Name = "txtPerBy"
        Me.txtPerBy.Size = New System.Drawing.Size(387, 20)
        Me.txtPerBy.TabIndex = 12
        '
        'MyLabel24
        '
        Me.MyLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel24.Location = New System.Drawing.Point(438, 79)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel24.TabIndex = 113
        Me.MyLabel24.Text = "Performance By"
        '
        'ChkRelocation
        '
        Me.ChkRelocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkRelocation.Location = New System.Drawing.Point(443, 6)
        Me.ChkRelocation.Name = "ChkRelocation"
        Me.ChkRelocation.Size = New System.Drawing.Size(78, 16)
        Me.ChkRelocation.TabIndex = 9
        Me.ChkRelocation.Text = "Relocation"
        '
        'grpReLoc
        '
        Me.grpReLoc.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpReLoc.Controls.Add(Me.LblToLoc)
        Me.grpReLoc.Controls.Add(Me.MyLabel46)
        Me.grpReLoc.Controls.Add(Me.txtToLoc)
        Me.grpReLoc.Controls.Add(Me.LblFromLoc)
        Me.grpReLoc.Controls.Add(Me.MyLabel53)
        Me.grpReLoc.Controls.Add(Me.txtFromLoc)
        Me.grpReLoc.HeaderText = ""
        Me.grpReLoc.Location = New System.Drawing.Point(434, 15)
        Me.grpReLoc.Name = "grpReLoc"
        Me.grpReLoc.Size = New System.Drawing.Size(501, 51)
        Me.grpReLoc.TabIndex = 110
        '
        'LblToLoc
        '
        Me.LblToLoc.AutoSize = False
        Me.LblToLoc.BorderVisible = True
        Me.LblToLoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblToLoc.Location = New System.Drawing.Point(230, 29)
        Me.LblToLoc.Name = "LblToLoc"
        Me.LblToLoc.Size = New System.Drawing.Size(264, 17)
        Me.LblToLoc.TabIndex = 60
        Me.LblToLoc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblToLoc.TextWrap = False
        '
        'MyLabel46
        '
        Me.MyLabel46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel46.Location = New System.Drawing.Point(10, 30)
        Me.MyLabel46.Name = "MyLabel46"
        Me.MyLabel46.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel46.TabIndex = 59
        Me.MyLabel46.Text = "Location To"
        '
        'txtToLoc
        '
        Me.txtToLoc.Location = New System.Drawing.Point(107, 29)
        Me.txtToLoc.MendatroryField = True
        Me.txtToLoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToLoc.MyLinkLable1 = Me.MyLabel46
        Me.txtToLoc.MyLinkLable2 = Nothing
        Me.txtToLoc.MyReadOnly = False
        Me.txtToLoc.MyShowMasterFormButton = False
        Me.txtToLoc.Name = "txtToLoc"
        Me.txtToLoc.Size = New System.Drawing.Size(118, 18)
        Me.txtToLoc.TabIndex = 11
        Me.txtToLoc.Value = ""
        '
        'LblFromLoc
        '
        Me.LblFromLoc.AutoSize = False
        Me.LblFromLoc.BorderVisible = True
        Me.LblFromLoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFromLoc.Location = New System.Drawing.Point(230, 7)
        Me.LblFromLoc.Name = "LblFromLoc"
        Me.LblFromLoc.Size = New System.Drawing.Size(264, 18)
        Me.LblFromLoc.TabIndex = 57
        Me.LblFromLoc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblFromLoc.TextWrap = False
        '
        'MyLabel53
        '
        Me.MyLabel53.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel53.Location = New System.Drawing.Point(10, 8)
        Me.MyLabel53.Name = "MyLabel53"
        Me.MyLabel53.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel53.TabIndex = 49
        Me.MyLabel53.Text = "Location From"
        '
        'txtFromLoc
        '
        Me.txtFromLoc.Location = New System.Drawing.Point(107, 7)
        Me.txtFromLoc.MendatroryField = True
        Me.txtFromLoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromLoc.MyLinkLable1 = Me.MyLabel53
        Me.txtFromLoc.MyLinkLable2 = Nothing
        Me.txtFromLoc.MyReadOnly = False
        Me.txtFromLoc.MyShowMasterFormButton = False
        Me.txtFromLoc.Name = "txtFromLoc"
        Me.txtFromLoc.Size = New System.Drawing.Size(118, 19)
        Me.txtFromLoc.TabIndex = 10
        Me.txtFromLoc.Value = ""
        '
        'lblBranchName
        '
        Me.lblBranchName.AutoSize = False
        Me.lblBranchName.BorderVisible = True
        Me.lblBranchName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBranchName.Location = New System.Drawing.Point(249, 99)
        Me.lblBranchName.Name = "lblBranchName"
        Me.lblBranchName.Size = New System.Drawing.Size(176, 18)
        Me.lblBranchName.TabIndex = 56
        Me.lblBranchName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBranchName.TextWrap = False
        '
        'LblBankName
        '
        Me.LblBankName.AutoSize = False
        Me.LblBankName.BorderVisible = True
        Me.LblBankName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBankName.Location = New System.Drawing.Point(249, 76)
        Me.LblBankName.Name = "LblBankName"
        Me.LblBankName.Size = New System.Drawing.Size(176, 18)
        Me.LblBankName.TabIndex = 55
        Me.LblBankName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblBankName.TextWrap = False
        '
        'txtAccNo
        '
        Me.txtAccNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtAccNo.Location = New System.Drawing.Point(127, 122)
        Me.txtAccNo.MaxLength = 50
        Me.txtAccNo.MendatroryField = False
        Me.txtAccNo.MyLinkLable1 = Me.MyLabel28
        Me.txtAccNo.MyLinkLable2 = Nothing
        Me.txtAccNo.Name = "txtAccNo"
        Me.txtAccNo.Size = New System.Drawing.Size(118, 20)
        Me.txtAccNo.TabIndex = 6
        '
        'MyLabel28
        '
        Me.MyLabel28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel28.Location = New System.Drawing.Point(10, 124)
        Me.MyLabel28.Name = "MyLabel28"
        Me.MyLabel28.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel28.TabIndex = 52
        Me.MyLabel28.Text = "A/C No"
        '
        'MyLabel31
        '
        Me.MyLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel31.Location = New System.Drawing.Point(10, 100)
        Me.MyLabel31.Name = "MyLabel31"
        Me.MyLabel31.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel31.TabIndex = 47
        Me.MyLabel31.Text = "Branch Code"
        '
        'txtBranchCode
        '
        Me.txtBranchCode.Location = New System.Drawing.Point(127, 99)
        Me.txtBranchCode.MendatroryField = True
        Me.txtBranchCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBranchCode.MyLinkLable1 = Me.MyLabel31
        Me.txtBranchCode.MyLinkLable2 = Nothing
        Me.txtBranchCode.MyReadOnly = False
        Me.txtBranchCode.MyShowMasterFormButton = False
        Me.txtBranchCode.Name = "txtBranchCode"
        Me.txtBranchCode.Size = New System.Drawing.Size(118, 19)
        Me.txtBranchCode.TabIndex = 5
        Me.txtBranchCode.Value = ""
        '
        'MyLabel32
        '
        Me.MyLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel32.Location = New System.Drawing.Point(10, 77)
        Me.MyLabel32.Name = "MyLabel32"
        Me.MyLabel32.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel32.TabIndex = 45
        Me.MyLabel32.Text = "Bank Code"
        '
        'txtBankCode
        '
        Me.txtBankCode.Location = New System.Drawing.Point(127, 76)
        Me.txtBankCode.MendatroryField = True
        Me.txtBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankCode.MyLinkLable1 = Me.MyLabel32
        Me.txtBankCode.MyLinkLable2 = Nothing
        Me.txtBankCode.MyReadOnly = False
        Me.txtBankCode.MyShowMasterFormButton = False
        Me.txtBankCode.Name = "txtBankCode"
        Me.txtBankCode.Size = New System.Drawing.Size(118, 19)
        Me.txtBankCode.TabIndex = 4
        Me.txtBankCode.Value = ""
        '
        'txtBloodGrp
        '
        Me.txtBloodGrp.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtBloodGrp.Location = New System.Drawing.Point(127, 52)
        Me.txtBloodGrp.MaxLength = 3
        Me.txtBloodGrp.MendatroryField = True
        Me.txtBloodGrp.MyLinkLable1 = Me.MyLabel33
        Me.txtBloodGrp.MyLinkLable2 = Nothing
        Me.txtBloodGrp.Name = "txtBloodGrp"
        Me.txtBloodGrp.Size = New System.Drawing.Size(118, 20)
        Me.txtBloodGrp.TabIndex = 3
        '
        'MyLabel33
        '
        Me.MyLabel33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel33.Location = New System.Drawing.Point(10, 54)
        Me.MyLabel33.Name = "MyLabel33"
        Me.MyLabel33.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel33.TabIndex = 44
        Me.MyLabel33.Text = "Blood Group"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(88, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(893, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'FrmApplicantEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(970, 451)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmApplicantEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Applicant Entry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChillingVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblsourcedetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblsourcetype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblrequisition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtTelephone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPinCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtadd3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtadd4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdd2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.PicImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclearphoto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPhotoBrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbMarStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDateofBirth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLastName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMiddleName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFirstName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbGender, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSalutation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDateofIntr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPPDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.gvQualification.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvQualification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShowDoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnDeleteDoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtResume, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnDocReset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        CType(Me.gvDoc.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.ChkFresher, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        CType(Me.gvEmpHis.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvEmpHis, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage6.ResumeLayout(False)
        Me.RadPageViewPage6.PerformLayout()
        CType(Me.LblAgency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRelation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblRefBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbnrefbyAge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbnRefbyEmp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.Panel2.PerformLayout()
        Me.SplitContainer4.ResumeLayout(False)
        CType(Me.gvFamily.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvFamily, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkHandicaped, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.txtHandiDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalCTC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCurrGrossSal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblPreLoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel48, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPerBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkRelocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpReLoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpReLoc.ResumeLayout(False)
        Me.grpReLoc.PerformLayout()
        CType(Me.LblToLoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel46, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblFromLoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel53, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBranchName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAccNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBloodGrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents lblsourcedetail As common.Controls.MyLabel
    Friend WithEvents lblsourcetype As common.Controls.MyLabel
    Friend WithEvents lblrequisition As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtLastName As common.Controls.MyTextBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents txtMiddleName As common.Controls.MyTextBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtFirstName As common.Controls.MyTextBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents cmbGender As common.Controls.MyComboBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents cmbSalutation As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents dtpDateofIntr As common.Controls.MyDateTimePicker
    Friend WithEvents lblPPDate As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblCity As common.Controls.MyLabel
    Friend WithEvents lblState As common.Controls.MyLabel
    Friend WithEvents lblCountry As common.Controls.MyLabel
    Friend WithEvents txtAdd1 As common.Controls.MyTextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtEmail As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtCity As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtState As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtCountry As common.UserControls.txtFinder
    Friend WithEvents txtAdd2 As common.Controls.MyTextBox
    Friend WithEvents lblAdd2 As common.Controls.MyLabel
    Friend WithEvents lblAdd1 As common.Controls.MyLabel
    Friend WithEvents MyLabel44 As common.Controls.MyLabel
    Friend WithEvents txtrequisitioncode As common.UserControls.txtFinder
    Friend WithEvents MyLabel43 As common.Controls.MyLabel
    Friend WithEvents txtsourcedetail As common.UserControls.txtFinder
    Friend WithEvents lblChillingVendor As common.Controls.MyLabel
    Friend WithEvents txtsourcetype As common.UserControls.txtFinder
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtcode As common.UserControls.txtNavigator
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvDoc As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvEmpHis As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage6 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage5 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents dtpDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents CmbMarStatus As common.Controls.MyComboBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents dtpDateofBirth As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtPanNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPhotoBrowse As Telerik.WinControls.UI.RadButton
    Friend WithEvents gvQualification As common.UserControls.MyRadGridView
    Friend WithEvents txtResume As common.Controls.MyTextBox
    Friend WithEvents btnBrowse As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblBranchName As common.Controls.MyLabel
    Friend WithEvents LblBankName As common.Controls.MyLabel
    Friend WithEvents txtHandiDetail As common.Controls.MyTextBox
    Friend WithEvents MyLabel27 As common.Controls.MyLabel
    Friend WithEvents txtAccNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents MyLabel29 As common.Controls.MyLabel
    Friend WithEvents MyLabel31 As common.Controls.MyLabel
    Friend WithEvents txtBranchCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel32 As common.Controls.MyLabel
    Friend WithEvents txtBankCode As common.UserControls.txtFinder
    Friend WithEvents txtBloodGrp As common.Controls.MyTextBox
    Friend WithEvents MyLabel33 As common.Controls.MyLabel
    Friend WithEvents MyLabel41 As common.Controls.MyLabel
    Friend WithEvents gvFamily As common.UserControls.MyRadGridView
    Friend WithEvents grpReLoc As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents LblToLoc As common.Controls.MyLabel
    Friend WithEvents MyLabel46 As common.Controls.MyLabel
    Friend WithEvents txtToLoc As common.UserControls.txtFinder
    Friend WithEvents LblFromLoc As common.Controls.MyLabel
    Friend WithEvents MyLabel53 As common.Controls.MyLabel
    Friend WithEvents txtFromLoc As common.UserControls.txtFinder
    Friend WithEvents ChkRelocation As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents ChkHandicaped As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents LblPreLoc As common.Controls.MyLabel
    Friend WithEvents MyLabel40 As common.Controls.MyLabel
    Friend WithEvents txtPreferedLoc As common.UserControls.txtFinder
    Friend WithEvents LblLocation As common.Controls.MyLabel
    Friend WithEvents MyLabel48 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents txtPerBy As common.Controls.MyTextBox
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents rbnrefbyAge As common.Controls.MyRadioButton
    Friend WithEvents rbnRefbyEmp As common.Controls.MyRadioButton
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents LblRefBy As common.Controls.MyLabel
    Friend WithEvents txtEmpCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtRelation As common.UserControls.txtFinder
    Friend WithEvents ChkFresher As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtadd3 As common.Controls.MyTextBox
    Friend WithEvents txtadd4 As common.Controls.MyTextBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents lblRelation As common.Controls.MyLabel
    Friend WithEvents txtTotalCTC As common.MyNumBox
    Friend WithEvents txtCurrGrossSal As common.MyNumBox
    Friend WithEvents txtdesp As common.Controls.MyTextBox
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents BtnDocReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnShowDoc As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnDeleteDoc As Telerik.WinControls.UI.RadButton
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents btnclearphoto As Telerik.WinControls.UI.RadButton
    Friend WithEvents LblAgency As common.Controls.MyLabel
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents txtAgency As common.UserControls.txtFinder
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtPinCode As common.MyNumBox
    Friend WithEvents PicImage As System.Windows.Forms.PictureBox
    Friend WithEvents txtTelephone As Telerik.WinControls.UI.RadMaskedEditBox
End Class

