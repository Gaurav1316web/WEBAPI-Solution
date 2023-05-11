<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCompanyMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCompanyMaster))
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.dtPanIssueDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.dtTinIssueDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.dtpInsurance_Valid_Date = New common.Controls.MyDateTimePicker()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtInsurance_Comp_Name = New common.Controls.MyTextBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtinsuranceno = New common.Controls.MyTextBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtPFNO = New common.Controls.MyTextBox()
        Me.lblPFNO = New common.Controls.MyLabel()
        Me.txtESICNO = New common.Controls.MyTextBox()
        Me.lblCompESIC = New common.Controls.MyLabel()
        Me.TxtIECode = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtCIN = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtTelephone2 = New Telerik.WinControls.UI.RadMaskedEditBox()
        Me.txtTelephone1 = New Telerik.WinControls.UI.RadMaskedEditBox()
        Me.txtcust_name = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtcust_code = New common.UserControls.txtFinder()
        Me.chk_main_company = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkApplyMultiCurrency = New Telerik.WinControls.UI.RadCheckBox()
        Me.fndBaseCurrency = New common.UserControls.txtFinder()
        Me.lblBaseCurrency = New common.Controls.MyLabel()
        Me.txtCompanyNameTally = New common.Controls.MyTextBox()
        Me.lblCEDivision = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.chkIntegrateWithTally = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtState = New common.UserControls.txtFinder()
        Me.fndCompanyCode = New common.UserControls.txtNavigator()
        Me.lblCompanyCode = New common.Controls.MyLabel()
        Me.lblAccessOfficer = New common.Controls.MyLabel()
        Me.txtRegdNo = New common.Controls.MyTextBox()
        Me.txtRegNo = New common.Controls.MyLabel()
        Me.txtAccessOfficer = New common.Controls.MyTextBox()
        Me.txtWardNo = New common.Controls.MyTextBox()
        Me.lblWardNo = New common.Controls.MyLabel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.txtCircleNo = New common.Controls.MyTextBox()
        Me.lblCircleNo = New common.Controls.MyLabel()
        Me.txtTcanNo = New common.Controls.MyTextBox()
        Me.lblTcanNo = New common.Controls.MyLabel()
        Me.txtTanNo = New common.Controls.MyTextBox()
        Me.lblTanNo = New common.Controls.MyLabel()
        Me.txtPanNo = New common.Controls.MyTextBox()
        Me.lblPanNo = New common.Controls.MyLabel()
        Me.txtCEDivision = New common.Controls.MyTextBox()
        Me.txtCECommissionerate = New common.Controls.MyTextBox()
        Me.lblCECommissionerate = New common.Controls.MyLabel()
        Me.txtCERange = New common.Controls.MyTextBox()
        Me.lblCERange = New common.Controls.MyLabel()
        Me.txtEccNo = New common.Controls.MyTextBox()
        Me.lblEccNo = New common.Controls.MyLabel()
        Me.txtServiceTaxRegNo = New common.Controls.MyTextBox()
        Me.lblServiceTaxRegNo = New common.Controls.MyLabel()
        Me.txtVatRegNo = New common.Controls.MyTextBox()
        Me.lblVatRegNo = New common.Controls.MyLabel()
        Me.cboDataBase = New common.Controls.MyComboBox()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.ddlModeofTransp = New common.Controls.MyComboBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.chkCformRequiredTN = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtCstLst = New common.Controls.MyTextBox()
        Me.lblCstLst = New common.Controls.MyLabel()
        Me.txtTinNo = New common.Controls.MyTextBox()
        Me.txtPinCode = New common.Controls.MyTextBox()
        Me.lblPinCode = New common.Controls.MyLabel()
        Me.lblTinNo = New common.Controls.MyLabel()
        Me.lblState = New common.Controls.MyLabel()
        Me.txtFax = New common.Controls.MyTextBox()
        Me.lblTax = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtEmail = New common.Controls.MyTextBox()
        Me.lblEmail = New common.Controls.MyLabel()
        Me.lblTelephone2 = New common.Controls.MyLabel()
        Me.lblTelephone1 = New common.Controls.MyLabel()
        Me.txtCity = New common.Controls.MyTextBox()
        Me.lblCity = New common.Controls.MyLabel()
        Me.txtAdd3 = New common.Controls.MyTextBox()
        Me.lblAdress = New common.Controls.MyLabel()
        Me.txtAdd2 = New common.Controls.MyTextBox()
        Me.txtAdd1 = New common.Controls.MyTextBox()
        Me.txtCompanyName = New common.Controls.MyTextBox()
        Me.lblCompanyName = New common.Controls.MyLabel()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.btnClearLogo1 = New Telerik.WinControls.UI.RadButton()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnSelectPath1 = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.btnClearLogo2 = New Telerik.WinControls.UI.RadButton()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.btnSelectPath2 = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.TxtEmployerDesg = New common.Controls.MyTextBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.TxtEmployerAdd3 = New common.Controls.MyTextBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.TxtEmployerAdd2 = New common.Controls.MyTextBox()
        Me.TxtEmployerAdd1 = New common.Controls.MyTextBox()
        Me.TxtEmployerName = New common.Controls.MyTextBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.TxtGstInNo = New common.Controls.MyTextBox()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.TxtGstReg = New common.Controls.MyTextBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.dtPanIssueDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTinIssueDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpInsurance_Valid_Date, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInsurance_Comp_Name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtinsuranceno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPFNO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPFNO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtESICNO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCompESIC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtIECode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCIN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTelephone2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTelephone1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcust_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_main_company, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkApplyMultiCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBaseCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCompanyNameTally, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCEDivision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIntegrateWithTally, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCompanyCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAccessOfficer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRegdNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRegNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAccessOfficer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWardNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWardNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lblWardNo.SuspendLayout()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCircleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCircleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTcanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTcanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCEDivision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCECommissionerate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCECommissionerate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCERange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCERange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEccNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEccNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtServiceTaxRegNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblServiceTaxRegNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVatRegNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVatRegNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDataBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlModeofTransp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCformRequiredTN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCstLst, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCstLst, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTinNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPinCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPinCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTinNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTelephone2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTelephone1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCompanyName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCompanyName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.btnClearLogo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelectPath1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.btnClearLogo2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelectPath2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.TxtEmployerDesg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtEmployerAdd3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtEmployerAdd2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtEmployerAdd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtEmployerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.TxtGstInNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtGstReg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.dtPanIssueDate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel12)
        Me.RadGroupBox1.Controls.Add(Me.dtTinIssueDate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel11)
        Me.RadGroupBox1.Controls.Add(Me.dtpInsurance_Valid_Date)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel10)
        Me.RadGroupBox1.Controls.Add(Me.txtInsurance_Comp_Name)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox1.Controls.Add(Me.txtinsuranceno)
        Me.RadGroupBox1.Controls.Add(Me.txtPFNO)
        Me.RadGroupBox1.Controls.Add(Me.lblPFNO)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox1.Controls.Add(Me.txtESICNO)
        Me.RadGroupBox1.Controls.Add(Me.lblCompESIC)
        Me.RadGroupBox1.Controls.Add(Me.TxtIECode)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtCIN)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtTelephone2)
        Me.RadGroupBox1.Controls.Add(Me.txtTelephone1)
        Me.RadGroupBox1.Controls.Add(Me.txtcust_name)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtcust_code)
        Me.RadGroupBox1.Controls.Add(Me.chk_main_company)
        Me.RadGroupBox1.Controls.Add(Me.chkApplyMultiCurrency)
        Me.RadGroupBox1.Controls.Add(Me.fndBaseCurrency)
        Me.RadGroupBox1.Controls.Add(Me.lblBaseCurrency)
        Me.RadGroupBox1.Controls.Add(Me.txtCompanyNameTally)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.chkIntegrateWithTally)
        Me.RadGroupBox1.Controls.Add(Me.txtState)
        Me.RadGroupBox1.Controls.Add(Me.fndCompanyCode)
        Me.RadGroupBox1.Controls.Add(Me.lblAccessOfficer)
        Me.RadGroupBox1.Controls.Add(Me.txtRegdNo)
        Me.RadGroupBox1.Controls.Add(Me.txtAccessOfficer)
        Me.RadGroupBox1.Controls.Add(Me.txtWardNo)
        Me.RadGroupBox1.Controls.Add(Me.txtCircleNo)
        Me.RadGroupBox1.Controls.Add(Me.lblWardNo)
        Me.RadGroupBox1.Controls.Add(Me.lblCircleNo)
        Me.RadGroupBox1.Controls.Add(Me.txtTcanNo)
        Me.RadGroupBox1.Controls.Add(Me.lblTcanNo)
        Me.RadGroupBox1.Controls.Add(Me.txtTanNo)
        Me.RadGroupBox1.Controls.Add(Me.lblTanNo)
        Me.RadGroupBox1.Controls.Add(Me.txtPanNo)
        Me.RadGroupBox1.Controls.Add(Me.lblPanNo)
        Me.RadGroupBox1.Controls.Add(Me.txtCEDivision)
        Me.RadGroupBox1.Controls.Add(Me.txtCECommissionerate)
        Me.RadGroupBox1.Controls.Add(Me.lblCEDivision)
        Me.RadGroupBox1.Controls.Add(Me.lblCECommissionerate)
        Me.RadGroupBox1.Controls.Add(Me.txtCERange)
        Me.RadGroupBox1.Controls.Add(Me.txtEccNo)
        Me.RadGroupBox1.Controls.Add(Me.lblCERange)
        Me.RadGroupBox1.Controls.Add(Me.lblEccNo)
        Me.RadGroupBox1.Controls.Add(Me.txtServiceTaxRegNo)
        Me.RadGroupBox1.Controls.Add(Me.lblServiceTaxRegNo)
        Me.RadGroupBox1.Controls.Add(Me.txtVatRegNo)
        Me.RadGroupBox1.Controls.Add(Me.lblVatRegNo)
        Me.RadGroupBox1.Controls.Add(Me.cboDataBase)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.ddlModeofTransp)
        Me.RadGroupBox1.Controls.Add(Me.chkCformRequiredTN)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtRegNo)
        Me.RadGroupBox1.Controls.Add(Me.txtCstLst)
        Me.RadGroupBox1.Controls.Add(Me.txtTinNo)
        Me.RadGroupBox1.Controls.Add(Me.txtPinCode)
        Me.RadGroupBox1.Controls.Add(Me.lblTinNo)
        Me.RadGroupBox1.Controls.Add(Me.lblCstLst)
        Me.RadGroupBox1.Controls.Add(Me.lblState)
        Me.RadGroupBox1.Controls.Add(Me.lblPinCode)
        Me.RadGroupBox1.Controls.Add(Me.txtFax)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.txtEmail)
        Me.RadGroupBox1.Controls.Add(Me.lblEmail)
        Me.RadGroupBox1.Controls.Add(Me.lblTax)
        Me.RadGroupBox1.Controls.Add(Me.lblTelephone2)
        Me.RadGroupBox1.Controls.Add(Me.lblTelephone1)
        Me.RadGroupBox1.Controls.Add(Me.txtCity)
        Me.RadGroupBox1.Controls.Add(Me.lblCity)
        Me.RadGroupBox1.Controls.Add(Me.txtAdd3)
        Me.RadGroupBox1.Controls.Add(Me.txtAdd2)
        Me.RadGroupBox1.Controls.Add(Me.txtAdd1)
        Me.RadGroupBox1.Controls.Add(Me.lblAdress)
        Me.RadGroupBox1.Controls.Add(Me.txtCompanyName)
        Me.RadGroupBox1.Controls.Add(Me.lblCompanyName)
        Me.RadGroupBox1.Controls.Add(Me.lblCompanyCode)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(807, 488)
        Me.RadGroupBox1.TabIndex = 0
        '
        'dtPanIssueDate
        '
        Me.dtPanIssueDate.CalculationExpression = Nothing
        Me.dtPanIssueDate.CustomFormat = "dd/MM/yyyy"
        Me.dtPanIssueDate.FieldCode = Nothing
        Me.dtPanIssueDate.FieldDesc = Nothing
        Me.dtPanIssueDate.FieldMaxLength = 0
        Me.dtPanIssueDate.FieldName = Nothing
        Me.dtPanIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtPanIssueDate.isCalculatedField = False
        Me.dtPanIssueDate.IsSourceFromTable = False
        Me.dtPanIssueDate.IsSourceFromValueList = False
        Me.dtPanIssueDate.IsUnique = False
        Me.dtPanIssueDate.Location = New System.Drawing.Point(699, 207)
        Me.dtPanIssueDate.MendatroryField = False
        Me.dtPanIssueDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtPanIssueDate.MyLinkLable1 = Nothing
        Me.dtPanIssueDate.MyLinkLable2 = Nothing
        Me.dtPanIssueDate.Name = "dtPanIssueDate"
        Me.dtPanIssueDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtPanIssueDate.ReferenceFieldDesc = Nothing
        Me.dtPanIssueDate.ReferenceFieldName = Nothing
        Me.dtPanIssueDate.ReferenceTableName = Nothing
        Me.dtPanIssueDate.Size = New System.Drawing.Size(96, 20)
        Me.dtPanIssueDate.TabIndex = 303
        Me.dtPanIssueDate.TabStop = False
        Me.dtPanIssueDate.Text = "10/06/2011"
        Me.dtPanIssueDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(664, 209)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel12.TabIndex = 302
        Me.MyLabel12.Text = "Date"
        '
        'dtTinIssueDate
        '
        Me.dtTinIssueDate.CalculationExpression = Nothing
        Me.dtTinIssueDate.CustomFormat = "dd/MM/yyyy"
        Me.dtTinIssueDate.FieldCode = Nothing
        Me.dtTinIssueDate.FieldDesc = Nothing
        Me.dtTinIssueDate.FieldMaxLength = 0
        Me.dtTinIssueDate.FieldName = Nothing
        Me.dtTinIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTinIssueDate.isCalculatedField = False
        Me.dtTinIssueDate.IsSourceFromTable = False
        Me.dtTinIssueDate.IsSourceFromValueList = False
        Me.dtTinIssueDate.IsUnique = False
        Me.dtTinIssueDate.Location = New System.Drawing.Point(699, 145)
        Me.dtTinIssueDate.MendatroryField = False
        Me.dtTinIssueDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtTinIssueDate.MyLinkLable1 = Nothing
        Me.dtTinIssueDate.MyLinkLable2 = Nothing
        Me.dtTinIssueDate.Name = "dtTinIssueDate"
        Me.dtTinIssueDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtTinIssueDate.ReferenceFieldDesc = Nothing
        Me.dtTinIssueDate.ReferenceFieldName = Nothing
        Me.dtTinIssueDate.ReferenceTableName = Nothing
        Me.dtTinIssueDate.Size = New System.Drawing.Size(96, 20)
        Me.dtTinIssueDate.TabIndex = 301
        Me.dtTinIssueDate.TabStop = False
        Me.dtTinIssueDate.Text = "10/06/2011"
        Me.dtTinIssueDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(664, 147)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel11.TabIndex = 83
        Me.MyLabel11.Text = "Date"
        '
        'dtpInsurance_Valid_Date
        '
        Me.dtpInsurance_Valid_Date.CalculationExpression = Nothing
        Me.dtpInsurance_Valid_Date.CustomFormat = "dd/MM/yyyy"
        Me.dtpInsurance_Valid_Date.FieldCode = Nothing
        Me.dtpInsurance_Valid_Date.FieldDesc = Nothing
        Me.dtpInsurance_Valid_Date.FieldMaxLength = 0
        Me.dtpInsurance_Valid_Date.FieldName = Nothing
        Me.dtpInsurance_Valid_Date.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpInsurance_Valid_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpInsurance_Valid_Date.isCalculatedField = False
        Me.dtpInsurance_Valid_Date.IsSourceFromTable = False
        Me.dtpInsurance_Valid_Date.IsSourceFromValueList = False
        Me.dtpInsurance_Valid_Date.IsUnique = False
        Me.dtpInsurance_Valid_Date.Location = New System.Drawing.Point(166, 467)
        Me.dtpInsurance_Valid_Date.MendatroryField = False
        Me.dtpInsurance_Valid_Date.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpInsurance_Valid_Date.MyLinkLable1 = Me.MyLabel10
        Me.dtpInsurance_Valid_Date.MyLinkLable2 = Nothing
        Me.dtpInsurance_Valid_Date.Name = "dtpInsurance_Valid_Date"
        Me.dtpInsurance_Valid_Date.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpInsurance_Valid_Date.ReferenceFieldDesc = Nothing
        Me.dtpInsurance_Valid_Date.ReferenceFieldName = Nothing
        Me.dtpInsurance_Valid_Date.ReferenceTableName = Nothing
        Me.dtpInsurance_Valid_Date.ShowCheckBox = True
        Me.dtpInsurance_Valid_Date.Size = New System.Drawing.Size(92, 18)
        Me.dtpInsurance_Valid_Date.TabIndex = 82
        Me.dtpInsurance_Valid_Date.TabStop = False
        Me.dtpInsurance_Valid_Date.Text = "03/05/2011"
        Me.dtpInsurance_Valid_Date.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(12, 467)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(111, 16)
        Me.MyLabel10.TabIndex = 81
        Me.MyLabel10.Text = "Insurance Valid Upto"
        '
        'txtInsurance_Comp_Name
        '
        Me.txtInsurance_Comp_Name.CalculationExpression = Nothing
        Me.txtInsurance_Comp_Name.FieldCode = Nothing
        Me.txtInsurance_Comp_Name.FieldDesc = Nothing
        Me.txtInsurance_Comp_Name.FieldMaxLength = 0
        Me.txtInsurance_Comp_Name.FieldName = Nothing
        Me.txtInsurance_Comp_Name.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInsurance_Comp_Name.isCalculatedField = False
        Me.txtInsurance_Comp_Name.IsSourceFromTable = False
        Me.txtInsurance_Comp_Name.IsSourceFromValueList = False
        Me.txtInsurance_Comp_Name.IsUnique = False
        Me.txtInsurance_Comp_Name.Location = New System.Drawing.Point(166, 447)
        Me.txtInsurance_Comp_Name.MaxLength = 200
        Me.txtInsurance_Comp_Name.MendatroryField = False
        Me.txtInsurance_Comp_Name.MyLinkLable1 = Me.MyLabel9
        Me.txtInsurance_Comp_Name.MyLinkLable2 = Nothing
        Me.txtInsurance_Comp_Name.Name = "txtInsurance_Comp_Name"
        Me.txtInsurance_Comp_Name.ReferenceFieldDesc = Nothing
        Me.txtInsurance_Comp_Name.ReferenceFieldName = Nothing
        Me.txtInsurance_Comp_Name.ReferenceTableName = Nothing
        Me.txtInsurance_Comp_Name.Size = New System.Drawing.Size(262, 18)
        Me.txtInsurance_Comp_Name.TabIndex = 79
        Me.txtInsurance_Comp_Name.Text = " "
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(12, 448)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(141, 16)
        Me.MyLabel9.TabIndex = 80
        Me.MyLabel9.Text = "Insurance Company Name"
        '
        'txtinsuranceno
        '
        Me.txtinsuranceno.CalculationExpression = Nothing
        Me.txtinsuranceno.FieldCode = Nothing
        Me.txtinsuranceno.FieldDesc = Nothing
        Me.txtinsuranceno.FieldMaxLength = 0
        Me.txtinsuranceno.FieldName = Nothing
        Me.txtinsuranceno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtinsuranceno.isCalculatedField = False
        Me.txtinsuranceno.IsSourceFromTable = False
        Me.txtinsuranceno.IsSourceFromValueList = False
        Me.txtinsuranceno.IsUnique = False
        Me.txtinsuranceno.Location = New System.Drawing.Point(518, 447)
        Me.txtinsuranceno.MaxLength = 50
        Me.txtinsuranceno.MendatroryField = False
        Me.txtinsuranceno.MyLinkLable1 = Me.MyLabel8
        Me.txtinsuranceno.MyLinkLable2 = Nothing
        Me.txtinsuranceno.Name = "txtinsuranceno"
        Me.txtinsuranceno.ReferenceFieldDesc = Nothing
        Me.txtinsuranceno.ReferenceFieldName = Nothing
        Me.txtinsuranceno.ReferenceTableName = Nothing
        Me.txtinsuranceno.Size = New System.Drawing.Size(262, 18)
        Me.txtinsuranceno.TabIndex = 77
        Me.txtinsuranceno.Text = " "
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(434, 447)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel8.TabIndex = 78
        Me.MyLabel8.Text = "Insurance No."
        '
        'txtPFNO
        '
        Me.txtPFNO.CalculationExpression = Nothing
        Me.txtPFNO.FieldCode = Nothing
        Me.txtPFNO.FieldDesc = Nothing
        Me.txtPFNO.FieldMaxLength = 0
        Me.txtPFNO.FieldName = Nothing
        Me.txtPFNO.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPFNO.isCalculatedField = False
        Me.txtPFNO.IsSourceFromTable = False
        Me.txtPFNO.IsSourceFromValueList = False
        Me.txtPFNO.IsUnique = False
        Me.txtPFNO.Location = New System.Drawing.Point(624, 382)
        Me.txtPFNO.MaxLength = 30
        Me.txtPFNO.MendatroryField = False
        Me.txtPFNO.MyLinkLable1 = Me.lblPFNO
        Me.txtPFNO.MyLinkLable2 = Nothing
        Me.txtPFNO.Name = "txtPFNO"
        Me.txtPFNO.ReferenceFieldDesc = Nothing
        Me.txtPFNO.ReferenceFieldName = Nothing
        Me.txtPFNO.ReferenceTableName = Nothing
        Me.txtPFNO.Size = New System.Drawing.Size(171, 18)
        Me.txtPFNO.TabIndex = 75
        Me.txtPFNO.Text = " "
        '
        'lblPFNO
        '
        Me.lblPFNO.FieldName = Nothing
        Me.lblPFNO.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPFNO.Location = New System.Drawing.Point(567, 386)
        Me.lblPFNO.Name = "lblPFNO"
        Me.lblPFNO.Size = New System.Drawing.Size(44, 16)
        Me.lblPFNO.TabIndex = 76
        Me.lblPFNO.Text = "PF NO."
        '
        'txtESICNO
        '
        Me.txtESICNO.CalculationExpression = Nothing
        Me.txtESICNO.FieldCode = Nothing
        Me.txtESICNO.FieldDesc = Nothing
        Me.txtESICNO.FieldMaxLength = 0
        Me.txtESICNO.FieldName = Nothing
        Me.txtESICNO.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtESICNO.isCalculatedField = False
        Me.txtESICNO.IsSourceFromTable = False
        Me.txtESICNO.IsSourceFromValueList = False
        Me.txtESICNO.IsUnique = False
        Me.txtESICNO.Location = New System.Drawing.Point(624, 359)
        Me.txtESICNO.MaxLength = 30
        Me.txtESICNO.MendatroryField = False
        Me.txtESICNO.MyLinkLable1 = Me.lblCompESIC
        Me.txtESICNO.MyLinkLable2 = Nothing
        Me.txtESICNO.Name = "txtESICNO"
        Me.txtESICNO.ReferenceFieldDesc = Nothing
        Me.txtESICNO.ReferenceFieldName = Nothing
        Me.txtESICNO.ReferenceTableName = Nothing
        Me.txtESICNO.Size = New System.Drawing.Size(171, 18)
        Me.txtESICNO.TabIndex = 73
        Me.txtESICNO.Text = " "
        '
        'lblCompESIC
        '
        Me.lblCompESIC.FieldName = Nothing
        Me.lblCompESIC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompESIC.Location = New System.Drawing.Point(565, 363)
        Me.lblCompESIC.Name = "lblCompESIC"
        Me.lblCompESIC.Size = New System.Drawing.Size(56, 16)
        Me.lblCompESIC.TabIndex = 74
        Me.lblCompESIC.Text = "ESIC NO."
        '
        'TxtIECode
        '
        Me.TxtIECode.CalculationExpression = Nothing
        Me.TxtIECode.FieldCode = Nothing
        Me.TxtIECode.FieldDesc = Nothing
        Me.TxtIECode.FieldMaxLength = 0
        Me.TxtIECode.FieldName = Nothing
        Me.TxtIECode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIECode.isCalculatedField = False
        Me.TxtIECode.IsSourceFromTable = False
        Me.TxtIECode.IsSourceFromValueList = False
        Me.TxtIECode.IsUnique = False
        Me.TxtIECode.Location = New System.Drawing.Point(518, 427)
        Me.TxtIECode.MaxLength = 30
        Me.TxtIECode.MendatroryField = False
        Me.TxtIECode.MyLinkLable1 = Me.MyLabel4
        Me.TxtIECode.MyLinkLable2 = Nothing
        Me.TxtIECode.Name = "TxtIECode"
        Me.TxtIECode.ReferenceFieldDesc = Nothing
        Me.TxtIECode.ReferenceFieldName = Nothing
        Me.TxtIECode.ReferenceTableName = Nothing
        Me.TxtIECode.Size = New System.Drawing.Size(262, 18)
        Me.TxtIECode.TabIndex = 71
        Me.TxtIECode.Text = " "
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(434, 427)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(53, 16)
        Me.MyLabel4.TabIndex = 72
        Me.MyLabel4.Text = "I.E. Code"
        '
        'txtCIN
        '
        Me.txtCIN.CalculationExpression = Nothing
        Me.txtCIN.FieldCode = Nothing
        Me.txtCIN.FieldDesc = Nothing
        Me.txtCIN.FieldMaxLength = 0
        Me.txtCIN.FieldName = Nothing
        Me.txtCIN.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCIN.isCalculatedField = False
        Me.txtCIN.IsSourceFromTable = False
        Me.txtCIN.IsSourceFromValueList = False
        Me.txtCIN.IsUnique = False
        Me.txtCIN.Location = New System.Drawing.Point(166, 427)
        Me.txtCIN.MaxLength = 21
        Me.txtCIN.MendatroryField = True
        Me.txtCIN.MyLinkLable1 = Me.MyLabel3
        Me.txtCIN.MyLinkLable2 = Nothing
        Me.txtCIN.Name = "txtCIN"
        Me.txtCIN.ReferenceFieldDesc = Nothing
        Me.txtCIN.ReferenceFieldName = Nothing
        Me.txtCIN.ReferenceTableName = Nothing
        Me.txtCIN.Size = New System.Drawing.Size(262, 18)
        Me.txtCIN.TabIndex = 36
        Me.txtCIN.Text = " "
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(12, 427)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel3.TabIndex = 70
        Me.MyLabel3.Text = "CIN No."
        '
        'txtTelephone2
        '
        Me.txtTelephone2.Location = New System.Drawing.Point(167, 148)
        Me.txtTelephone2.Mask = "(+99)0000000000"
        Me.txtTelephone2.MaskType = Telerik.WinControls.UI.MaskType.Standard
        Me.txtTelephone2.Name = "txtTelephone2"
        Me.txtTelephone2.Size = New System.Drawing.Size(292, 20)
        Me.txtTelephone2.TabIndex = 68
        Me.txtTelephone2.TabStop = False
        Me.txtTelephone2.Text = "(+__)__________"
        '
        'txtTelephone1
        '
        Me.txtTelephone1.Location = New System.Drawing.Point(167, 126)
        Me.txtTelephone1.Mask = "(+99)0000000000"
        Me.txtTelephone1.MaskType = Telerik.WinControls.UI.MaskType.Standard
        Me.txtTelephone1.Name = "txtTelephone1"
        Me.txtTelephone1.Size = New System.Drawing.Size(292, 20)
        Me.txtTelephone1.TabIndex = 67
        Me.txtTelephone1.TabStop = False
        Me.txtTelephone1.Text = "(+__)__________"
        '
        'txtcust_name
        '
        Me.txtcust_name.AutoSize = False
        Me.txtcust_name.BorderVisible = True
        Me.txtcust_name.FieldName = Nothing
        Me.txtcust_name.Location = New System.Drawing.Point(322, 405)
        Me.txtcust_name.Name = "txtcust_name"
        Me.txtcust_name.Size = New System.Drawing.Size(473, 19)
        Me.txtcust_name.TabIndex = 65
        Me.txtcust_name.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 405)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel1.TabIndex = 66
        Me.MyLabel1.Text = "Customer Detail"
        '
        'txtcust_code
        '
        Me.txtcust_code.CalculationExpression = Nothing
        Me.txtcust_code.FieldCode = Nothing
        Me.txtcust_code.FieldDesc = Nothing
        Me.txtcust_code.FieldMaxLength = 0
        Me.txtcust_code.FieldName = Nothing
        Me.txtcust_code.isCalculatedField = False
        Me.txtcust_code.IsSourceFromTable = False
        Me.txtcust_code.IsSourceFromValueList = False
        Me.txtcust_code.IsUnique = False
        Me.txtcust_code.Location = New System.Drawing.Point(167, 405)
        Me.txtcust_code.MendatroryField = False
        Me.txtcust_code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcust_code.MyLinkLable1 = Me.MyLabel1
        Me.txtcust_code.MyLinkLable2 = Me.txtcust_name
        Me.txtcust_code.MyReadOnly = False
        Me.txtcust_code.MyShowMasterFormButton = False
        Me.txtcust_code.Name = "txtcust_code"
        Me.txtcust_code.ReferenceFieldDesc = Nothing
        Me.txtcust_code.ReferenceFieldName = Nothing
        Me.txtcust_code.ReferenceTableName = Nothing
        Me.txtcust_code.Size = New System.Drawing.Size(153, 19)
        Me.txtcust_code.TabIndex = 35
        Me.txtcust_code.Value = ""
        '
        'chk_main_company
        '
        Me.chk_main_company.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_main_company.Location = New System.Drawing.Point(470, 7)
        Me.chk_main_company.Name = "chk_main_company"
        Me.chk_main_company.Size = New System.Drawing.Size(108, 16)
        Me.chk_main_company.TabIndex = 63
        Me.chk_main_company.Text = "Is Main Company"
        '
        'chkApplyMultiCurrency
        '
        Me.chkApplyMultiCurrency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkApplyMultiCurrency.Location = New System.Drawing.Point(434, 362)
        Me.chkApplyMultiCurrency.Name = "chkApplyMultiCurrency"
        Me.chkApplyMultiCurrency.Size = New System.Drawing.Size(125, 16)
        Me.chkApplyMultiCurrency.TabIndex = 32
        Me.chkApplyMultiCurrency.Text = "Apply Multi Currency"
        '
        'fndBaseCurrency
        '
        Me.fndBaseCurrency.CalculationExpression = Nothing
        Me.fndBaseCurrency.FieldCode = Nothing
        Me.fndBaseCurrency.FieldDesc = Nothing
        Me.fndBaseCurrency.FieldMaxLength = 0
        Me.fndBaseCurrency.FieldName = Nothing
        Me.fndBaseCurrency.isCalculatedField = False
        Me.fndBaseCurrency.IsSourceFromTable = False
        Me.fndBaseCurrency.IsSourceFromValueList = False
        Me.fndBaseCurrency.IsUnique = False
        Me.fndBaseCurrency.Location = New System.Drawing.Point(167, 360)
        Me.fndBaseCurrency.MendatroryField = False
        Me.fndBaseCurrency.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBaseCurrency.MyLinkLable1 = Nothing
        Me.fndBaseCurrency.MyLinkLable2 = Nothing
        Me.fndBaseCurrency.MyReadOnly = False
        Me.fndBaseCurrency.MyShowMasterFormButton = False
        Me.fndBaseCurrency.Name = "fndBaseCurrency"
        Me.fndBaseCurrency.ReferenceFieldDesc = Nothing
        Me.fndBaseCurrency.ReferenceFieldName = Nothing
        Me.fndBaseCurrency.ReferenceTableName = Nothing
        Me.fndBaseCurrency.Size = New System.Drawing.Size(261, 19)
        Me.fndBaseCurrency.TabIndex = 31
        Me.fndBaseCurrency.Value = ""
        '
        'lblBaseCurrency
        '
        Me.lblBaseCurrency.FieldName = Nothing
        Me.lblBaseCurrency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBaseCurrency.Location = New System.Drawing.Point(12, 361)
        Me.lblBaseCurrency.Name = "lblBaseCurrency"
        Me.lblBaseCurrency.Size = New System.Drawing.Size(81, 16)
        Me.lblBaseCurrency.TabIndex = 36
        Me.lblBaseCurrency.Text = "Base Currency"
        '
        'txtCompanyNameTally
        '
        Me.txtCompanyNameTally.CalculationExpression = Nothing
        Me.txtCompanyNameTally.FieldCode = Nothing
        Me.txtCompanyNameTally.FieldDesc = Nothing
        Me.txtCompanyNameTally.FieldMaxLength = 0
        Me.txtCompanyNameTally.FieldName = Nothing
        Me.txtCompanyNameTally.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyNameTally.isCalculatedField = False
        Me.txtCompanyNameTally.IsSourceFromTable = False
        Me.txtCompanyNameTally.IsSourceFromValueList = False
        Me.txtCompanyNameTally.IsUnique = False
        Me.txtCompanyNameTally.Location = New System.Drawing.Point(167, 383)
        Me.txtCompanyNameTally.MaxLength = 30
        Me.txtCompanyNameTally.MendatroryField = False
        Me.txtCompanyNameTally.MyLinkLable1 = Me.lblCEDivision
        Me.txtCompanyNameTally.MyLinkLable2 = Nothing
        Me.txtCompanyNameTally.Name = "txtCompanyNameTally"
        Me.txtCompanyNameTally.ReferenceFieldDesc = Nothing
        Me.txtCompanyNameTally.ReferenceFieldName = Nothing
        Me.txtCompanyNameTally.ReferenceTableName = Nothing
        Me.txtCompanyNameTally.Size = New System.Drawing.Size(261, 18)
        Me.txtCompanyNameTally.TabIndex = 33
        Me.txtCompanyNameTally.Text = " "
        Me.txtCompanyNameTally.Visible = False
        '
        'lblCEDivision
        '
        Me.lblCEDivision.FieldName = Nothing
        Me.lblCEDivision.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCEDivision.Location = New System.Drawing.Point(12, 318)
        Me.lblCEDivision.Name = "lblCEDivision"
        Me.lblCEDivision.Size = New System.Drawing.Size(73, 16)
        Me.lblCEDivision.TabIndex = 39
        Me.lblCEDivision.Text = "ECC Division"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(12, 384)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(127, 16)
        Me.MyLabel2.TabIndex = 35
        Me.MyLabel2.Text = "Company Name in Tally"
        Me.MyLabel2.Visible = False
        '
        'chkIntegrateWithTally
        '
        Me.chkIntegrateWithTally.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIntegrateWithTally.Location = New System.Drawing.Point(434, 383)
        Me.chkIntegrateWithTally.Name = "chkIntegrateWithTally"
        Me.chkIntegrateWithTally.Size = New System.Drawing.Size(127, 16)
        Me.chkIntegrateWithTally.TabIndex = 34
        Me.chkIntegrateWithTally.Text = "Is Integrate with Tally"
        Me.chkIntegrateWithTally.Visible = False
        '
        'txtState
        '
        Me.txtState.CalculationExpression = Nothing
        Me.txtState.FieldCode = Nothing
        Me.txtState.FieldDesc = Nothing
        Me.txtState.FieldMaxLength = 0
        Me.txtState.FieldName = Nothing
        Me.txtState.isCalculatedField = False
        Me.txtState.IsSourceFromTable = False
        Me.txtState.IsSourceFromValueList = False
        Me.txtState.IsUnique = False
        Me.txtState.Location = New System.Drawing.Point(533, 125)
        Me.txtState.MendatroryField = False
        Me.txtState.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtState.MyLinkLable1 = Nothing
        Me.txtState.MyLinkLable2 = Nothing
        Me.txtState.MyReadOnly = False
        Me.txtState.MyShowMasterFormButton = False
        Me.txtState.Name = "txtState"
        Me.txtState.ReferenceFieldDesc = Nothing
        Me.txtState.ReferenceFieldName = Nothing
        Me.txtState.ReferenceTableName = Nothing
        Me.txtState.Size = New System.Drawing.Size(262, 19)
        Me.txtState.TabIndex = 9
        Me.txtState.Value = ""
        '
        'fndCompanyCode
        '
        Me.fndCompanyCode.FieldName = Nothing
        Me.fndCompanyCode.Location = New System.Drawing.Point(167, 6)
        Me.fndCompanyCode.MendatroryField = True
        Me.fndCompanyCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndCompanyCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCompanyCode.MyLinkLable1 = Me.lblCompanyCode
        Me.fndCompanyCode.MyLinkLable2 = Nothing
        Me.fndCompanyCode.MyMaxLength = 32767
        Me.fndCompanyCode.MyReadOnly = False
        Me.fndCompanyCode.Name = "fndCompanyCode"
        Me.fndCompanyCode.Size = New System.Drawing.Size(272, 18)
        Me.fndCompanyCode.TabIndex = 0
        Me.fndCompanyCode.Value = ""
        '
        'lblCompanyCode
        '
        Me.lblCompanyCode.FieldName = Nothing
        Me.lblCompanyCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompanyCode.Location = New System.Drawing.Point(12, 6)
        Me.lblCompanyCode.Name = "lblCompanyCode"
        Me.lblCompanyCode.Size = New System.Drawing.Size(85, 16)
        Me.lblCompanyCode.TabIndex = 62
        Me.lblCompanyCode.Text = "Company Code"
        '
        'lblAccessOfficer
        '
        Me.lblAccessOfficer.FieldName = Nothing
        Me.lblAccessOfficer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccessOfficer.Location = New System.Drawing.Point(12, 250)
        Me.lblAccessOfficer.Name = "lblAccessOfficer"
        Me.lblAccessOfficer.Size = New System.Drawing.Size(83, 16)
        Me.lblAccessOfficer.TabIndex = 44
        Me.lblAccessOfficer.Text = "FSSAI LIC NO."
        '
        'txtRegdNo
        '
        Me.txtRegdNo.CalculationExpression = Nothing
        Me.txtRegdNo.FieldCode = Nothing
        Me.txtRegdNo.FieldDesc = Nothing
        Me.txtRegdNo.FieldMaxLength = 0
        Me.txtRegdNo.FieldName = Nothing
        Me.txtRegdNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegdNo.isCalculatedField = False
        Me.txtRegdNo.IsSourceFromTable = False
        Me.txtRegdNo.IsSourceFromValueList = False
        Me.txtRegdNo.IsUnique = False
        Me.txtRegdNo.Location = New System.Drawing.Point(533, 189)
        Me.txtRegdNo.MaxLength = 30
        Me.txtRegdNo.MendatroryField = False
        Me.txtRegdNo.MyLinkLable1 = Me.txtRegNo
        Me.txtRegdNo.MyLinkLable2 = Nothing
        Me.txtRegdNo.Name = "txtRegdNo"
        Me.txtRegdNo.ReferenceFieldDesc = Nothing
        Me.txtRegdNo.ReferenceFieldName = Nothing
        Me.txtRegdNo.ReferenceTableName = Nothing
        Me.txtRegdNo.Size = New System.Drawing.Size(262, 18)
        Me.txtRegdNo.TabIndex = 15
        '
        'txtRegNo
        '
        Me.txtRegNo.FieldName = Nothing
        Me.txtRegNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegNo.Location = New System.Drawing.Point(468, 191)
        Me.txtRegNo.Name = "txtRegNo"
        Me.txtRegNo.Size = New System.Drawing.Size(57, 16)
        Me.txtRegNo.TabIndex = 50
        Me.txtRegNo.Text = "Regd  No."
        '
        'txtAccessOfficer
        '
        Me.txtAccessOfficer.CalculationExpression = Nothing
        Me.txtAccessOfficer.FieldCode = Nothing
        Me.txtAccessOfficer.FieldDesc = Nothing
        Me.txtAccessOfficer.FieldMaxLength = 0
        Me.txtAccessOfficer.FieldName = Nothing
        Me.txtAccessOfficer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccessOfficer.isCalculatedField = False
        Me.txtAccessOfficer.IsSourceFromTable = False
        Me.txtAccessOfficer.IsSourceFromValueList = False
        Me.txtAccessOfficer.IsUnique = False
        Me.txtAccessOfficer.Location = New System.Drawing.Point(167, 251)
        Me.txtAccessOfficer.MaxLength = 30
        Me.txtAccessOfficer.MendatroryField = False
        Me.txtAccessOfficer.MyLinkLable1 = Me.lblAccessOfficer
        Me.txtAccessOfficer.MyLinkLable2 = Nothing
        Me.txtAccessOfficer.Name = "txtAccessOfficer"
        Me.txtAccessOfficer.ReferenceFieldDesc = Nothing
        Me.txtAccessOfficer.ReferenceFieldName = Nothing
        Me.txtAccessOfficer.ReferenceTableName = Nothing
        Me.txtAccessOfficer.Size = New System.Drawing.Size(292, 18)
        Me.txtAccessOfficer.TabIndex = 20
        Me.txtAccessOfficer.Text = " "
        '
        'txtWardNo
        '
        Me.txtWardNo.CalculationExpression = Nothing
        Me.txtWardNo.FieldCode = Nothing
        Me.txtWardNo.FieldDesc = Nothing
        Me.txtWardNo.FieldMaxLength = 0
        Me.txtWardNo.FieldName = Nothing
        Me.txtWardNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWardNo.isCalculatedField = False
        Me.txtWardNo.IsSourceFromTable = False
        Me.txtWardNo.IsSourceFromValueList = False
        Me.txtWardNo.IsUnique = False
        Me.txtWardNo.Location = New System.Drawing.Point(533, 293)
        Me.txtWardNo.MaxLength = 30
        Me.txtWardNo.MendatroryField = False
        Me.txtWardNo.MyLinkLable1 = Me.lblWardNo
        Me.txtWardNo.MyLinkLable2 = Nothing
        Me.txtWardNo.Name = "txtWardNo"
        Me.txtWardNo.ReferenceFieldDesc = Nothing
        Me.txtWardNo.ReferenceFieldName = Nothing
        Me.txtWardNo.ReferenceTableName = Nothing
        Me.txtWardNo.Size = New System.Drawing.Size(262, 18)
        Me.txtWardNo.TabIndex = 25
        Me.txtWardNo.Text = " "
        '
        'lblWardNo
        '
        Me.lblWardNo.Controls.Add(Me.RadLabel15)
        Me.lblWardNo.FieldName = Nothing
        Me.lblWardNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWardNo.Location = New System.Drawing.Point(468, 297)
        Me.lblWardNo.Name = "lblWardNo"
        Me.lblWardNo.Size = New System.Drawing.Size(54, 16)
        Me.lblWardNo.TabIndex = 41
        Me.lblWardNo.Text = "Ward No."
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(0, 22)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(48, 16)
        Me.RadLabel15.TabIndex = 0
        Me.RadLabel15.Text = "ECC No"
        '
        'txtCircleNo
        '
        Me.txtCircleNo.CalculationExpression = Nothing
        Me.txtCircleNo.FieldCode = Nothing
        Me.txtCircleNo.FieldDesc = Nothing
        Me.txtCircleNo.FieldMaxLength = 0
        Me.txtCircleNo.FieldName = Nothing
        Me.txtCircleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCircleNo.isCalculatedField = False
        Me.txtCircleNo.IsSourceFromTable = False
        Me.txtCircleNo.IsSourceFromValueList = False
        Me.txtCircleNo.IsUnique = False
        Me.txtCircleNo.Location = New System.Drawing.Point(533, 271)
        Me.txtCircleNo.MaxLength = 30
        Me.txtCircleNo.MendatroryField = False
        Me.txtCircleNo.MyLinkLable1 = Me.lblCircleNo
        Me.txtCircleNo.MyLinkLable2 = Nothing
        Me.txtCircleNo.Name = "txtCircleNo"
        Me.txtCircleNo.ReferenceFieldDesc = Nothing
        Me.txtCircleNo.ReferenceFieldName = Nothing
        Me.txtCircleNo.ReferenceTableName = Nothing
        Me.txtCircleNo.Size = New System.Drawing.Size(262, 18)
        Me.txtCircleNo.TabIndex = 23
        Me.txtCircleNo.Text = " "
        '
        'lblCircleNo
        '
        Me.lblCircleNo.FieldName = Nothing
        Me.lblCircleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCircleNo.Location = New System.Drawing.Point(468, 275)
        Me.lblCircleNo.Name = "lblCircleNo"
        Me.lblCircleNo.Size = New System.Drawing.Size(56, 16)
        Me.lblCircleNo.TabIndex = 42
        Me.lblCircleNo.Text = "Circle No."
        '
        'txtTcanNo
        '
        Me.txtTcanNo.CalculationExpression = Nothing
        Me.txtTcanNo.FieldCode = Nothing
        Me.txtTcanNo.FieldDesc = Nothing
        Me.txtTcanNo.FieldMaxLength = 0
        Me.txtTcanNo.FieldName = Nothing
        Me.txtTcanNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTcanNo.isCalculatedField = False
        Me.txtTcanNo.IsSourceFromTable = False
        Me.txtTcanNo.IsSourceFromValueList = False
        Me.txtTcanNo.IsUnique = False
        Me.txtTcanNo.Location = New System.Drawing.Point(533, 249)
        Me.txtTcanNo.MaxLength = 30
        Me.txtTcanNo.MendatroryField = False
        Me.txtTcanNo.MyLinkLable1 = Me.lblTcanNo
        Me.txtTcanNo.MyLinkLable2 = Nothing
        Me.txtTcanNo.Name = "txtTcanNo"
        Me.txtTcanNo.ReferenceFieldDesc = Nothing
        Me.txtTcanNo.ReferenceFieldName = Nothing
        Me.txtTcanNo.ReferenceTableName = Nothing
        Me.txtTcanNo.Size = New System.Drawing.Size(262, 18)
        Me.txtTcanNo.TabIndex = 21
        Me.txtTcanNo.Text = " "
        '
        'lblTcanNo
        '
        Me.lblTcanNo.FieldName = Nothing
        Me.lblTcanNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTcanNo.Location = New System.Drawing.Point(468, 249)
        Me.lblTcanNo.Name = "lblTcanNo"
        Me.lblTcanNo.Size = New System.Drawing.Size(47, 16)
        Me.lblTcanNo.TabIndex = 45
        Me.lblTcanNo.Text = "Website"
        '
        'txtTanNo
        '
        Me.txtTanNo.CalculationExpression = Nothing
        Me.txtTanNo.FieldCode = Nothing
        Me.txtTanNo.FieldDesc = Nothing
        Me.txtTanNo.FieldMaxLength = 0
        Me.txtTanNo.FieldName = Nothing
        Me.txtTanNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTanNo.isCalculatedField = False
        Me.txtTanNo.IsSourceFromTable = False
        Me.txtTanNo.IsSourceFromValueList = False
        Me.txtTanNo.IsUnique = False
        Me.txtTanNo.Location = New System.Drawing.Point(533, 229)
        Me.txtTanNo.MaxLength = 30
        Me.txtTanNo.MendatroryField = False
        Me.txtTanNo.MyLinkLable1 = Me.lblTanNo
        Me.txtTanNo.MyLinkLable2 = Nothing
        Me.txtTanNo.Name = "txtTanNo"
        Me.txtTanNo.ReferenceFieldDesc = Nothing
        Me.txtTanNo.ReferenceFieldName = Nothing
        Me.txtTanNo.ReferenceTableName = Nothing
        Me.txtTanNo.Size = New System.Drawing.Size(262, 18)
        Me.txtTanNo.TabIndex = 19
        Me.txtTanNo.Text = " "
        '
        'lblTanNo
        '
        Me.lblTanNo.FieldName = Nothing
        Me.lblTanNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTanNo.Location = New System.Drawing.Point(468, 229)
        Me.lblTanNo.Name = "lblTanNo"
        Me.lblTanNo.Size = New System.Drawing.Size(50, 16)
        Me.lblTanNo.TabIndex = 46
        Me.lblTanNo.Text = "Tan  No."
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
        Me.txtPanNo.Location = New System.Drawing.Point(533, 209)
        Me.txtPanNo.MaxLength = 30
        Me.txtPanNo.MendatroryField = False
        Me.txtPanNo.MyLinkLable1 = Me.lblPanNo
        Me.txtPanNo.MyLinkLable2 = Nothing
        Me.txtPanNo.Name = "txtPanNo"
        Me.txtPanNo.ReferenceFieldDesc = Nothing
        Me.txtPanNo.ReferenceFieldName = Nothing
        Me.txtPanNo.ReferenceTableName = Nothing
        Me.txtPanNo.Size = New System.Drawing.Size(127, 18)
        Me.txtPanNo.TabIndex = 17
        Me.txtPanNo.Text = " "
        '
        'lblPanNo
        '
        Me.lblPanNo.FieldName = Nothing
        Me.lblPanNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPanNo.Location = New System.Drawing.Point(468, 209)
        Me.lblPanNo.Name = "lblPanNo"
        Me.lblPanNo.Size = New System.Drawing.Size(47, 16)
        Me.lblPanNo.TabIndex = 49
        Me.lblPanNo.Text = "Pan No."
        '
        'txtCEDivision
        '
        Me.txtCEDivision.CalculationExpression = Nothing
        Me.txtCEDivision.FieldCode = Nothing
        Me.txtCEDivision.FieldDesc = Nothing
        Me.txtCEDivision.FieldMaxLength = 0
        Me.txtCEDivision.FieldName = Nothing
        Me.txtCEDivision.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCEDivision.isCalculatedField = False
        Me.txtCEDivision.IsSourceFromTable = False
        Me.txtCEDivision.IsSourceFromValueList = False
        Me.txtCEDivision.IsUnique = False
        Me.txtCEDivision.Location = New System.Drawing.Point(167, 317)
        Me.txtCEDivision.MaxLength = 30
        Me.txtCEDivision.MendatroryField = False
        Me.txtCEDivision.MyLinkLable1 = Me.lblCEDivision
        Me.txtCEDivision.MyLinkLable2 = Nothing
        Me.txtCEDivision.Name = "txtCEDivision"
        Me.txtCEDivision.ReferenceFieldDesc = Nothing
        Me.txtCEDivision.ReferenceFieldName = Nothing
        Me.txtCEDivision.ReferenceTableName = Nothing
        Me.txtCEDivision.Size = New System.Drawing.Size(292, 18)
        Me.txtCEDivision.TabIndex = 26
        Me.txtCEDivision.Text = " "
        '
        'txtCECommissionerate
        '
        Me.txtCECommissionerate.CalculationExpression = Nothing
        Me.txtCECommissionerate.FieldCode = Nothing
        Me.txtCECommissionerate.FieldDesc = Nothing
        Me.txtCECommissionerate.FieldMaxLength = 0
        Me.txtCECommissionerate.FieldName = Nothing
        Me.txtCECommissionerate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCECommissionerate.isCalculatedField = False
        Me.txtCECommissionerate.IsSourceFromTable = False
        Me.txtCECommissionerate.IsSourceFromValueList = False
        Me.txtCECommissionerate.IsUnique = False
        Me.txtCECommissionerate.Location = New System.Drawing.Point(167, 295)
        Me.txtCECommissionerate.MaxLength = 30
        Me.txtCECommissionerate.MendatroryField = False
        Me.txtCECommissionerate.MyLinkLable1 = Me.lblCECommissionerate
        Me.txtCECommissionerate.MyLinkLable2 = Nothing
        Me.txtCECommissionerate.Name = "txtCECommissionerate"
        Me.txtCECommissionerate.ReferenceFieldDesc = Nothing
        Me.txtCECommissionerate.ReferenceFieldName = Nothing
        Me.txtCECommissionerate.ReferenceTableName = Nothing
        Me.txtCECommissionerate.Size = New System.Drawing.Size(292, 18)
        Me.txtCECommissionerate.TabIndex = 24
        Me.txtCECommissionerate.Text = " "
        '
        'lblCECommissionerate
        '
        Me.lblCECommissionerate.FieldName = Nothing
        Me.lblCECommissionerate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCECommissionerate.Location = New System.Drawing.Point(12, 298)
        Me.lblCECommissionerate.Name = "lblCECommissionerate"
        Me.lblCECommissionerate.Size = New System.Drawing.Size(113, 16)
        Me.lblCECommissionerate.TabIndex = 40
        Me.lblCECommissionerate.Text = "CE Commissionerate"
        '
        'txtCERange
        '
        Me.txtCERange.CalculationExpression = Nothing
        Me.txtCERange.FieldCode = Nothing
        Me.txtCERange.FieldDesc = Nothing
        Me.txtCERange.FieldMaxLength = 0
        Me.txtCERange.FieldName = Nothing
        Me.txtCERange.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCERange.isCalculatedField = False
        Me.txtCERange.IsSourceFromTable = False
        Me.txtCERange.IsSourceFromValueList = False
        Me.txtCERange.IsUnique = False
        Me.txtCERange.Location = New System.Drawing.Point(167, 273)
        Me.txtCERange.MaxLength = 30
        Me.txtCERange.MendatroryField = False
        Me.txtCERange.MyLinkLable1 = Me.lblCERange
        Me.txtCERange.MyLinkLable2 = Nothing
        Me.txtCERange.Name = "txtCERange"
        Me.txtCERange.ReferenceFieldDesc = Nothing
        Me.txtCERange.ReferenceFieldName = Nothing
        Me.txtCERange.ReferenceTableName = Nothing
        Me.txtCERange.Size = New System.Drawing.Size(292, 18)
        Me.txtCERange.TabIndex = 22
        Me.txtCERange.Text = " "
        '
        'lblCERange
        '
        Me.lblCERange.FieldName = Nothing
        Me.lblCERange.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCERange.Location = New System.Drawing.Point(12, 274)
        Me.lblCERange.Name = "lblCERange"
        Me.lblCERange.Size = New System.Drawing.Size(58, 16)
        Me.lblCERange.TabIndex = 43
        Me.lblCERange.Text = "CE Range"
        '
        'txtEccNo
        '
        Me.txtEccNo.CalculationExpression = Nothing
        Me.txtEccNo.FieldCode = Nothing
        Me.txtEccNo.FieldDesc = Nothing
        Me.txtEccNo.FieldMaxLength = 0
        Me.txtEccNo.FieldName = Nothing
        Me.txtEccNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEccNo.isCalculatedField = False
        Me.txtEccNo.IsSourceFromTable = False
        Me.txtEccNo.IsSourceFromValueList = False
        Me.txtEccNo.IsUnique = False
        Me.txtEccNo.Location = New System.Drawing.Point(533, 315)
        Me.txtEccNo.MaxLength = 30
        Me.txtEccNo.MendatroryField = False
        Me.txtEccNo.MyLinkLable1 = Me.lblEccNo
        Me.txtEccNo.MyLinkLable2 = Nothing
        Me.txtEccNo.Name = "txtEccNo"
        Me.txtEccNo.ReferenceFieldDesc = Nothing
        Me.txtEccNo.ReferenceFieldName = Nothing
        Me.txtEccNo.ReferenceTableName = Nothing
        Me.txtEccNo.Size = New System.Drawing.Size(262, 18)
        Me.txtEccNo.TabIndex = 27
        Me.txtEccNo.Text = " "
        '
        'lblEccNo
        '
        Me.lblEccNo.FieldName = Nothing
        Me.lblEccNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEccNo.Location = New System.Drawing.Point(468, 319)
        Me.lblEccNo.Name = "lblEccNo"
        Me.lblEccNo.Size = New System.Drawing.Size(48, 16)
        Me.lblEccNo.TabIndex = 50
        Me.lblEccNo.Text = "ECC No"
        '
        'txtServiceTaxRegNo
        '
        Me.txtServiceTaxRegNo.CalculationExpression = Nothing
        Me.txtServiceTaxRegNo.FieldCode = Nothing
        Me.txtServiceTaxRegNo.FieldDesc = Nothing
        Me.txtServiceTaxRegNo.FieldMaxLength = 0
        Me.txtServiceTaxRegNo.FieldName = Nothing
        Me.txtServiceTaxRegNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServiceTaxRegNo.isCalculatedField = False
        Me.txtServiceTaxRegNo.IsSourceFromTable = False
        Me.txtServiceTaxRegNo.IsSourceFromValueList = False
        Me.txtServiceTaxRegNo.IsUnique = False
        Me.txtServiceTaxRegNo.Location = New System.Drawing.Point(167, 231)
        Me.txtServiceTaxRegNo.MaxLength = 30
        Me.txtServiceTaxRegNo.MendatroryField = False
        Me.txtServiceTaxRegNo.MyLinkLable1 = Me.lblServiceTaxRegNo
        Me.txtServiceTaxRegNo.MyLinkLable2 = Nothing
        Me.txtServiceTaxRegNo.Name = "txtServiceTaxRegNo"
        Me.txtServiceTaxRegNo.ReferenceFieldDesc = Nothing
        Me.txtServiceTaxRegNo.ReferenceFieldName = Nothing
        Me.txtServiceTaxRegNo.ReferenceTableName = Nothing
        Me.txtServiceTaxRegNo.Size = New System.Drawing.Size(292, 18)
        Me.txtServiceTaxRegNo.TabIndex = 18
        Me.txtServiceTaxRegNo.Text = " "
        '
        'lblServiceTaxRegNo
        '
        Me.lblServiceTaxRegNo.FieldName = Nothing
        Me.lblServiceTaxRegNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServiceTaxRegNo.Location = New System.Drawing.Point(12, 232)
        Me.lblServiceTaxRegNo.Name = "lblServiceTaxRegNo"
        Me.lblServiceTaxRegNo.Size = New System.Drawing.Size(147, 16)
        Me.lblServiceTaxRegNo.TabIndex = 47
        Me.lblServiceTaxRegNo.Text = "Service Tax Registration No"
        '
        'txtVatRegNo
        '
        Me.txtVatRegNo.CalculationExpression = Nothing
        Me.txtVatRegNo.FieldCode = Nothing
        Me.txtVatRegNo.FieldDesc = Nothing
        Me.txtVatRegNo.FieldMaxLength = 0
        Me.txtVatRegNo.FieldName = Nothing
        Me.txtVatRegNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVatRegNo.isCalculatedField = False
        Me.txtVatRegNo.IsSourceFromTable = False
        Me.txtVatRegNo.IsSourceFromValueList = False
        Me.txtVatRegNo.IsUnique = False
        Me.txtVatRegNo.Location = New System.Drawing.Point(167, 211)
        Me.txtVatRegNo.MaxLength = 30
        Me.txtVatRegNo.MendatroryField = False
        Me.txtVatRegNo.MyLinkLable1 = Me.lblVatRegNo
        Me.txtVatRegNo.MyLinkLable2 = Nothing
        Me.txtVatRegNo.Name = "txtVatRegNo"
        Me.txtVatRegNo.ReferenceFieldDesc = Nothing
        Me.txtVatRegNo.ReferenceFieldName = Nothing
        Me.txtVatRegNo.ReferenceTableName = Nothing
        Me.txtVatRegNo.Size = New System.Drawing.Size(292, 18)
        Me.txtVatRegNo.TabIndex = 16
        Me.txtVatRegNo.Text = " "
        '
        'lblVatRegNo
        '
        Me.lblVatRegNo.FieldName = Nothing
        Me.lblVatRegNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVatRegNo.Location = New System.Drawing.Point(12, 212)
        Me.lblVatRegNo.Name = "lblVatRegNo"
        Me.lblVatRegNo.Size = New System.Drawing.Size(108, 16)
        Me.lblVatRegNo.TabIndex = 48
        Me.lblVatRegNo.Text = "Vat Registration No."
        '
        'cboDataBase
        '
        Me.cboDataBase.AutoCompleteDisplayMember = Nothing
        Me.cboDataBase.AutoCompleteValueMember = Nothing
        Me.cboDataBase.CalculationExpression = Nothing
        Me.cboDataBase.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboDataBase.FieldCode = Nothing
        Me.cboDataBase.FieldDesc = Nothing
        Me.cboDataBase.FieldMaxLength = 0
        Me.cboDataBase.FieldName = Nothing
        Me.cboDataBase.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDataBase.isCalculatedField = False
        Me.cboDataBase.IsSourceFromTable = False
        Me.cboDataBase.IsSourceFromValueList = False
        Me.cboDataBase.IsUnique = False
        Me.cboDataBase.Location = New System.Drawing.Point(533, 337)
        Me.cboDataBase.MendatroryField = False
        Me.cboDataBase.MyLinkLable1 = Me.RadLabel1
        Me.cboDataBase.MyLinkLable2 = Nothing
        Me.cboDataBase.Name = "cboDataBase"
        Me.cboDataBase.ReferenceFieldDesc = Nothing
        Me.cboDataBase.ReferenceFieldName = Nothing
        Me.cboDataBase.ReferenceTableName = Nothing
        Me.cboDataBase.Size = New System.Drawing.Size(168, 18)
        Me.cboDataBase.TabIndex = 30
        Me.cboDataBase.Text = "RadDropDownList1"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(468, 339)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 16)
        Me.RadLabel1.TabIndex = 38
        Me.RadLabel1.Text = "Data Base"
        '
        'ddlModeofTransp
        '
        Me.ddlModeofTransp.AutoCompleteDisplayMember = Nothing
        Me.ddlModeofTransp.AutoCompleteValueMember = Nothing
        Me.ddlModeofTransp.CalculationExpression = Nothing
        Me.ddlModeofTransp.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlModeofTransp.FieldCode = Nothing
        Me.ddlModeofTransp.FieldDesc = Nothing
        Me.ddlModeofTransp.FieldMaxLength = 0
        Me.ddlModeofTransp.FieldName = Nothing
        Me.ddlModeofTransp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlModeofTransp.isCalculatedField = False
        Me.ddlModeofTransp.IsSourceFromTable = False
        Me.ddlModeofTransp.IsSourceFromValueList = False
        Me.ddlModeofTransp.IsUnique = False
        RadListDataItem1.Text = "By Air"
        RadListDataItem2.Text = "By Road"
        RadListDataItem3.Text = "By Sea"
        Me.ddlModeofTransp.Items.Add(RadListDataItem1)
        Me.ddlModeofTransp.Items.Add(RadListDataItem2)
        Me.ddlModeofTransp.Items.Add(RadListDataItem3)
        Me.ddlModeofTransp.Location = New System.Drawing.Point(167, 339)
        Me.ddlModeofTransp.MendatroryField = False
        Me.ddlModeofTransp.MyLinkLable1 = Me.RadLabel2
        Me.ddlModeofTransp.MyLinkLable2 = Nothing
        Me.ddlModeofTransp.Name = "ddlModeofTransp"
        Me.ddlModeofTransp.ReferenceFieldDesc = Nothing
        Me.ddlModeofTransp.ReferenceFieldName = Nothing
        Me.ddlModeofTransp.ReferenceTableName = Nothing
        Me.ddlModeofTransp.Size = New System.Drawing.Size(137, 18)
        Me.ddlModeofTransp.TabIndex = 28
        Me.ddlModeofTransp.Text = "      -----Select-----"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(12, 338)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(89, 16)
        Me.RadLabel2.TabIndex = 37
        Me.RadLabel2.Text = "Mode of Transp."
        '
        'chkCformRequiredTN
        '
        Me.chkCformRequiredTN.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCformRequiredTN.Location = New System.Drawing.Point(327, 339)
        Me.chkCformRequiredTN.Name = "chkCformRequiredTN"
        Me.chkCformRequiredTN.Size = New System.Drawing.Size(132, 16)
        Me.chkCformRequiredTN.TabIndex = 29
        Me.chkCformRequiredTN.Text = "C. Form Required T/N"
        '
        'txtCstLst
        '
        Me.txtCstLst.CalculationExpression = Nothing
        Me.txtCstLst.FieldCode = Nothing
        Me.txtCstLst.FieldDesc = Nothing
        Me.txtCstLst.FieldMaxLength = 0
        Me.txtCstLst.FieldName = Nothing
        Me.txtCstLst.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCstLst.isCalculatedField = False
        Me.txtCstLst.IsSourceFromTable = False
        Me.txtCstLst.IsSourceFromValueList = False
        Me.txtCstLst.IsUnique = False
        Me.txtCstLst.Location = New System.Drawing.Point(533, 167)
        Me.txtCstLst.MaxLength = 30
        Me.txtCstLst.MendatroryField = False
        Me.txtCstLst.MyLinkLable1 = Me.lblCstLst
        Me.txtCstLst.MyLinkLable2 = Nothing
        Me.txtCstLst.Name = "txtCstLst"
        Me.txtCstLst.ReferenceFieldDesc = Nothing
        Me.txtCstLst.ReferenceFieldName = Nothing
        Me.txtCstLst.ReferenceTableName = Nothing
        Me.txtCstLst.Size = New System.Drawing.Size(262, 18)
        Me.txtCstLst.TabIndex = 13
        '
        'lblCstLst
        '
        Me.lblCstLst.FieldName = Nothing
        Me.lblCstLst.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCstLst.Location = New System.Drawing.Point(468, 169)
        Me.lblCstLst.Name = "lblCstLst"
        Me.lblCstLst.Size = New System.Drawing.Size(41, 16)
        Me.lblCstLst.TabIndex = 53
        Me.lblCstLst.Text = "Cst/Lst"
        '
        'txtTinNo
        '
        Me.txtTinNo.CalculationExpression = Nothing
        Me.txtTinNo.FieldCode = Nothing
        Me.txtTinNo.FieldDesc = Nothing
        Me.txtTinNo.FieldMaxLength = 0
        Me.txtTinNo.FieldName = Nothing
        Me.txtTinNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTinNo.isCalculatedField = False
        Me.txtTinNo.IsSourceFromTable = False
        Me.txtTinNo.IsSourceFromValueList = False
        Me.txtTinNo.IsUnique = False
        Me.txtTinNo.Location = New System.Drawing.Point(533, 145)
        Me.txtTinNo.MaxLength = 20
        Me.txtTinNo.MendatroryField = False
        Me.txtTinNo.MyLinkLable1 = Me.lblTanNo
        Me.txtTinNo.MyLinkLable2 = Nothing
        Me.txtTinNo.Name = "txtTinNo"
        Me.txtTinNo.ReferenceFieldDesc = Nothing
        Me.txtTinNo.ReferenceFieldName = Nothing
        Me.txtTinNo.ReferenceTableName = Nothing
        Me.txtTinNo.Size = New System.Drawing.Size(127, 18)
        Me.txtTinNo.TabIndex = 11
        '
        'txtPinCode
        '
        Me.txtPinCode.CalculationExpression = Nothing
        Me.txtPinCode.FieldCode = Nothing
        Me.txtPinCode.FieldDesc = Nothing
        Me.txtPinCode.FieldMaxLength = 0
        Me.txtPinCode.FieldName = Nothing
        Me.txtPinCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPinCode.isCalculatedField = False
        Me.txtPinCode.IsSourceFromTable = False
        Me.txtPinCode.IsSourceFromValueList = False
        Me.txtPinCode.IsUnique = False
        Me.txtPinCode.Location = New System.Drawing.Point(533, 106)
        Me.txtPinCode.MaxLength = 20
        Me.txtPinCode.MendatroryField = False
        Me.txtPinCode.MyLinkLable1 = Me.lblPinCode
        Me.txtPinCode.MyLinkLable2 = Nothing
        Me.txtPinCode.Name = "txtPinCode"
        Me.txtPinCode.ReferenceFieldDesc = Nothing
        Me.txtPinCode.ReferenceFieldName = Nothing
        Me.txtPinCode.ReferenceTableName = Nothing
        Me.txtPinCode.Size = New System.Drawing.Size(262, 18)
        Me.txtPinCode.TabIndex = 7
        '
        'lblPinCode
        '
        Me.lblPinCode.FieldName = Nothing
        Me.lblPinCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPinCode.Location = New System.Drawing.Point(468, 108)
        Me.lblPinCode.Name = "lblPinCode"
        Me.lblPinCode.Size = New System.Drawing.Size(53, 16)
        Me.lblPinCode.TabIndex = 58
        Me.lblPinCode.Text = "Pin Code"
        '
        'lblTinNo
        '
        Me.lblTinNo.FieldName = Nothing
        Me.lblTinNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTinNo.Location = New System.Drawing.Point(468, 147)
        Me.lblTinNo.Name = "lblTinNo"
        Me.lblTinNo.Size = New System.Drawing.Size(40, 16)
        Me.lblTinNo.TabIndex = 54
        Me.lblTinNo.Text = "Tin No"
        '
        'lblState
        '
        Me.lblState.FieldName = Nothing
        Me.lblState.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblState.Location = New System.Drawing.Point(468, 129)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(33, 16)
        Me.lblState.TabIndex = 57
        Me.lblState.Text = "State"
        '
        'txtFax
        '
        Me.txtFax.CalculationExpression = Nothing
        Me.txtFax.FieldCode = Nothing
        Me.txtFax.FieldDesc = Nothing
        Me.txtFax.FieldMaxLength = 0
        Me.txtFax.FieldName = Nothing
        Me.txtFax.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFax.isCalculatedField = False
        Me.txtFax.IsSourceFromTable = False
        Me.txtFax.IsSourceFromValueList = False
        Me.txtFax.IsUnique = False
        Me.txtFax.Location = New System.Drawing.Point(167, 170)
        Me.txtFax.MaxLength = 12
        Me.txtFax.MendatroryField = False
        Me.txtFax.MyLinkLable1 = Me.lblTax
        Me.txtFax.MyLinkLable2 = Nothing
        Me.txtFax.Name = "txtFax"
        Me.txtFax.ReferenceFieldDesc = Nothing
        Me.txtFax.ReferenceFieldName = Nothing
        Me.txtFax.ReferenceTableName = Nothing
        Me.txtFax.Size = New System.Drawing.Size(292, 18)
        Me.txtFax.TabIndex = 12
        '
        'lblTax
        '
        Me.lblTax.FieldName = Nothing
        Me.lblTax.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTax.Location = New System.Drawing.Point(12, 171)
        Me.lblTax.Name = "lblTax"
        Me.lblTax.Size = New System.Drawing.Size(25, 16)
        Me.lblTax.TabIndex = 52
        Me.lblTax.Text = "Fax"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(445, 5)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 21)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnNew, "New")
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
        Me.txtEmail.Location = New System.Drawing.Point(167, 191)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.MendatroryField = False
        Me.txtEmail.MyLinkLable1 = Me.lblEmail
        Me.txtEmail.MyLinkLable2 = Nothing
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.ReferenceFieldDesc = Nothing
        Me.txtEmail.ReferenceFieldName = Nothing
        Me.txtEmail.ReferenceTableName = Nothing
        Me.txtEmail.Size = New System.Drawing.Size(292, 18)
        Me.txtEmail.TabIndex = 14
        '
        'lblEmail
        '
        Me.lblEmail.FieldName = Nothing
        Me.lblEmail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmail.Location = New System.Drawing.Point(12, 192)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(34, 16)
        Me.lblEmail.TabIndex = 51
        Me.lblEmail.Text = "Email"
        '
        'lblTelephone2
        '
        Me.lblTelephone2.FieldName = Nothing
        Me.lblTelephone2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTelephone2.Location = New System.Drawing.Point(12, 148)
        Me.lblTelephone2.Name = "lblTelephone2"
        Me.lblTelephone2.Size = New System.Drawing.Size(66, 16)
        Me.lblTelephone2.TabIndex = 55
        Me.lblTelephone2.Text = "Telephone2"
        '
        'lblTelephone1
        '
        Me.lblTelephone1.FieldName = Nothing
        Me.lblTelephone1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTelephone1.Location = New System.Drawing.Point(12, 128)
        Me.lblTelephone1.Name = "lblTelephone1"
        Me.lblTelephone1.Size = New System.Drawing.Size(66, 16)
        Me.lblTelephone1.TabIndex = 56
        Me.lblTelephone1.Text = "Telephone1"
        '
        'txtCity
        '
        Me.txtCity.CalculationExpression = Nothing
        Me.txtCity.FieldCode = Nothing
        Me.txtCity.FieldDesc = Nothing
        Me.txtCity.FieldMaxLength = 0
        Me.txtCity.FieldName = Nothing
        Me.txtCity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCity.isCalculatedField = False
        Me.txtCity.IsSourceFromTable = False
        Me.txtCity.IsSourceFromValueList = False
        Me.txtCity.IsUnique = False
        Me.txtCity.Location = New System.Drawing.Point(167, 107)
        Me.txtCity.MaxLength = 50
        Me.txtCity.MendatroryField = False
        Me.txtCity.MyLinkLable1 = Me.lblCity
        Me.txtCity.MyLinkLable2 = Nothing
        Me.txtCity.Name = "txtCity"
        Me.txtCity.ReferenceFieldDesc = Nothing
        Me.txtCity.ReferenceFieldName = Nothing
        Me.txtCity.ReferenceTableName = Nothing
        Me.txtCity.Size = New System.Drawing.Size(292, 18)
        Me.txtCity.TabIndex = 6
        '
        'lblCity
        '
        Me.lblCity.FieldName = Nothing
        Me.lblCity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCity.Location = New System.Drawing.Point(12, 109)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(56, 16)
        Me.lblCity.TabIndex = 59
        Me.lblCity.Text = "City Code"
        '
        'txtAdd3
        '
        Me.txtAdd3.CalculationExpression = Nothing
        Me.txtAdd3.FieldCode = Nothing
        Me.txtAdd3.FieldDesc = Nothing
        Me.txtAdd3.FieldMaxLength = 0
        Me.txtAdd3.FieldName = Nothing
        Me.txtAdd3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd3.isCalculatedField = False
        Me.txtAdd3.IsSourceFromTable = False
        Me.txtAdd3.IsSourceFromValueList = False
        Me.txtAdd3.IsUnique = False
        Me.txtAdd3.Location = New System.Drawing.Point(167, 87)
        Me.txtAdd3.MaxLength = 50
        Me.txtAdd3.MendatroryField = False
        Me.txtAdd3.MyLinkLable1 = Me.lblAdress
        Me.txtAdd3.MyLinkLable2 = Nothing
        Me.txtAdd3.Name = "txtAdd3"
        Me.txtAdd3.ReferenceFieldDesc = Nothing
        Me.txtAdd3.ReferenceFieldName = Nothing
        Me.txtAdd3.ReferenceTableName = Nothing
        Me.txtAdd3.Size = New System.Drawing.Size(628, 18)
        Me.txtAdd3.TabIndex = 5
        '
        'lblAdress
        '
        Me.lblAdress.FieldName = Nothing
        Me.lblAdress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdress.Location = New System.Drawing.Point(12, 47)
        Me.lblAdress.Name = "lblAdress"
        Me.lblAdress.Size = New System.Drawing.Size(48, 16)
        Me.lblAdress.TabIndex = 60
        Me.lblAdress.Text = "Address"
        '
        'txtAdd2
        '
        Me.txtAdd2.CalculationExpression = Nothing
        Me.txtAdd2.FieldCode = Nothing
        Me.txtAdd2.FieldDesc = Nothing
        Me.txtAdd2.FieldMaxLength = 0
        Me.txtAdd2.FieldName = Nothing
        Me.txtAdd2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd2.isCalculatedField = False
        Me.txtAdd2.IsSourceFromTable = False
        Me.txtAdd2.IsSourceFromValueList = False
        Me.txtAdd2.IsUnique = False
        Me.txtAdd2.Location = New System.Drawing.Point(167, 67)
        Me.txtAdd2.MaxLength = 50
        Me.txtAdd2.MendatroryField = False
        Me.txtAdd2.MyLinkLable1 = Me.lblAdress
        Me.txtAdd2.MyLinkLable2 = Nothing
        Me.txtAdd2.Name = "txtAdd2"
        Me.txtAdd2.ReferenceFieldDesc = Nothing
        Me.txtAdd2.ReferenceFieldName = Nothing
        Me.txtAdd2.ReferenceTableName = Nothing
        Me.txtAdd2.Size = New System.Drawing.Size(628, 18)
        Me.txtAdd2.TabIndex = 4
        '
        'txtAdd1
        '
        Me.txtAdd1.CalculationExpression = Nothing
        Me.txtAdd1.FieldCode = Nothing
        Me.txtAdd1.FieldDesc = Nothing
        Me.txtAdd1.FieldMaxLength = 0
        Me.txtAdd1.FieldName = Nothing
        Me.txtAdd1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd1.isCalculatedField = False
        Me.txtAdd1.IsSourceFromTable = False
        Me.txtAdd1.IsSourceFromValueList = False
        Me.txtAdd1.IsUnique = False
        Me.txtAdd1.Location = New System.Drawing.Point(167, 47)
        Me.txtAdd1.MaxLength = 50
        Me.txtAdd1.MendatroryField = False
        Me.txtAdd1.MyLinkLable1 = Me.lblAdress
        Me.txtAdd1.MyLinkLable2 = Nothing
        Me.txtAdd1.Name = "txtAdd1"
        Me.txtAdd1.ReferenceFieldDesc = Nothing
        Me.txtAdd1.ReferenceFieldName = Nothing
        Me.txtAdd1.ReferenceTableName = Nothing
        Me.txtAdd1.Size = New System.Drawing.Size(628, 18)
        Me.txtAdd1.TabIndex = 3
        '
        'txtCompanyName
        '
        Me.txtCompanyName.CalculationExpression = Nothing
        Me.txtCompanyName.FieldCode = Nothing
        Me.txtCompanyName.FieldDesc = Nothing
        Me.txtCompanyName.FieldMaxLength = 0
        Me.txtCompanyName.FieldName = Nothing
        Me.txtCompanyName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyName.isCalculatedField = False
        Me.txtCompanyName.IsSourceFromTable = False
        Me.txtCompanyName.IsSourceFromValueList = False
        Me.txtCompanyName.IsUnique = False
        Me.txtCompanyName.Location = New System.Drawing.Point(167, 26)
        Me.txtCompanyName.MaxLength = 100
        Me.txtCompanyName.MendatroryField = False
        Me.txtCompanyName.MyLinkLable1 = Me.lblCompanyName
        Me.txtCompanyName.MyLinkLable2 = Nothing
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.ReferenceFieldDesc = Nothing
        Me.txtCompanyName.ReferenceFieldName = Nothing
        Me.txtCompanyName.ReferenceTableName = Nothing
        Me.txtCompanyName.Size = New System.Drawing.Size(628, 18)
        Me.txtCompanyName.TabIndex = 2
        '
        'lblCompanyName
        '
        Me.lblCompanyName.FieldName = Nothing
        Me.lblCompanyName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompanyName.Location = New System.Drawing.Point(12, 25)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(88, 16)
        Me.lblCompanyName.TabIndex = 61
        Me.lblCompanyName.Text = "Company Name"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(6, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(759, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.menuImport, Me.menuExport, Me.menuClose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'menuImport
        '
        Me.menuImport.AccessibleDescription = "Import.."
        Me.menuImport.AccessibleName = "Import.."
        Me.menuImport.Name = "menuImport"
        Me.menuImport.Text = "Print.."
        Me.menuImport.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        '
        'menuExport
        '
        Me.menuExport.AccessibleDescription = "Export"
        Me.menuExport.AccessibleName = "Export"
        Me.menuExport.Name = "menuExport"
        Me.menuExport.Text = "Export"
        '
        'menuClose
        '
        Me.menuClose.AccessibleDescription = "Close"
        Me.menuClose.AccessibleName = "Close"
        Me.menuClose.Name = "menuClose"
        Me.menuClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(77, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(830, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(1, 1)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage5
        Me.RadPageView1.Size = New System.Drawing.Size(828, 536)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemFitMode = Telerik.WinControls.UI.StripViewItemFitMode.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(101.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(807, 488)
        Me.RadPageViewPage1.Text = "Company Details"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.btnClearLogo1)
        Me.RadPageViewPage2.Controls.Add(Me.PictureBox1)
        Me.RadPageViewPage2.Controls.Add(Me.btnSelectPath1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(51.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(807, 488)
        Me.RadPageViewPage2.Text = "Logo 1"
        '
        'btnClearLogo1
        '
        Me.btnClearLogo1.Location = New System.Drawing.Point(163, 15)
        Me.btnClearLogo1.Name = "btnClearLogo1"
        Me.btnClearLogo1.Size = New System.Drawing.Size(130, 24)
        Me.btnClearLogo1.TabIndex = 1
        Me.btnClearLogo1.Text = "Clear Logo"
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(13, 45)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(763, 252)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'btnSelectPath1
        '
        Me.btnSelectPath1.Location = New System.Drawing.Point(27, 15)
        Me.btnSelectPath1.Name = "btnSelectPath1"
        Me.btnSelectPath1.Size = New System.Drawing.Size(130, 24)
        Me.btnSelectPath1.TabIndex = 0
        Me.btnSelectPath1.Text = "Select Logo"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.btnClearLogo2)
        Me.RadPageViewPage3.Controls.Add(Me.PictureBox2)
        Me.RadPageViewPage3.Controls.Add(Me.btnSelectPath2)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(51.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(807, 488)
        Me.RadPageViewPage3.Text = "Logo 2"
        '
        'btnClearLogo2
        '
        Me.btnClearLogo2.Location = New System.Drawing.Point(160, 10)
        Me.btnClearLogo2.Name = "btnClearLogo2"
        Me.btnClearLogo2.Size = New System.Drawing.Size(130, 24)
        Me.btnClearLogo2.TabIndex = 1
        Me.btnClearLogo2.Text = "Clear Logo"
        '
        'PictureBox2
        '
        Me.PictureBox2.Location = New System.Drawing.Point(10, 40)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(763, 252)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 3
        Me.PictureBox2.TabStop = False
        '
        'btnSelectPath2
        '
        Me.btnSelectPath2.Location = New System.Drawing.Point(24, 10)
        Me.btnSelectPath2.Name = "btnSelectPath2"
        Me.btnSelectPath2.Size = New System.Drawing.Size(130, 24)
        Me.btnSelectPath2.TabIndex = 0
        Me.btnSelectPath2.Text = "Select Logo"
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.TxtEmployerDesg)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage4.Controls.Add(Me.TxtEmployerAdd3)
        Me.RadPageViewPage4.Controls.Add(Me.TxtEmployerAdd2)
        Me.RadPageViewPage4.Controls.Add(Me.TxtEmployerAdd1)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage4.Controls.Add(Me.TxtEmployerName)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(100.0!, 28.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(807, 488)
        Me.RadPageViewPage4.Text = "Employer Details"
        '
        'TxtEmployerDesg
        '
        Me.TxtEmployerDesg.CalculationExpression = Nothing
        Me.TxtEmployerDesg.FieldCode = Nothing
        Me.TxtEmployerDesg.FieldDesc = Nothing
        Me.TxtEmployerDesg.FieldMaxLength = 0
        Me.TxtEmployerDesg.FieldName = Nothing
        Me.TxtEmployerDesg.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEmployerDesg.isCalculatedField = False
        Me.TxtEmployerDesg.IsSourceFromTable = False
        Me.TxtEmployerDesg.IsSourceFromValueList = False
        Me.TxtEmployerDesg.IsUnique = False
        Me.TxtEmployerDesg.Location = New System.Drawing.Point(155, 35)
        Me.TxtEmployerDesg.MaxLength = 50
        Me.TxtEmployerDesg.MendatroryField = False
        Me.TxtEmployerDesg.MyLinkLable1 = Me.MyLabel5
        Me.TxtEmployerDesg.MyLinkLable2 = Nothing
        Me.TxtEmployerDesg.Name = "TxtEmployerDesg"
        Me.TxtEmployerDesg.ReferenceFieldDesc = Nothing
        Me.TxtEmployerDesg.ReferenceFieldName = Nothing
        Me.TxtEmployerDesg.ReferenceTableName = Nothing
        Me.TxtEmployerDesg.Size = New System.Drawing.Size(292, 18)
        Me.TxtEmployerDesg.TabIndex = 66
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(2, 36)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(66, 16)
        Me.MyLabel5.TabIndex = 67
        Me.MyLabel5.Text = "Designation"
        '
        'TxtEmployerAdd3
        '
        Me.TxtEmployerAdd3.CalculationExpression = Nothing
        Me.TxtEmployerAdd3.FieldCode = Nothing
        Me.TxtEmployerAdd3.FieldDesc = Nothing
        Me.TxtEmployerAdd3.FieldMaxLength = 0
        Me.TxtEmployerAdd3.FieldName = Nothing
        Me.TxtEmployerAdd3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEmployerAdd3.isCalculatedField = False
        Me.TxtEmployerAdd3.IsSourceFromTable = False
        Me.TxtEmployerAdd3.IsSourceFromValueList = False
        Me.TxtEmployerAdd3.IsUnique = False
        Me.TxtEmployerAdd3.Location = New System.Drawing.Point(155, 98)
        Me.TxtEmployerAdd3.MaxLength = 50
        Me.TxtEmployerAdd3.MendatroryField = False
        Me.TxtEmployerAdd3.MyLinkLable1 = Me.MyLabel6
        Me.TxtEmployerAdd3.MyLinkLable2 = Nothing
        Me.TxtEmployerAdd3.Name = "TxtEmployerAdd3"
        Me.TxtEmployerAdd3.ReferenceFieldDesc = Nothing
        Me.TxtEmployerAdd3.ReferenceFieldName = Nothing
        Me.TxtEmployerAdd3.ReferenceTableName = Nothing
        Me.TxtEmployerAdd3.Size = New System.Drawing.Size(292, 18)
        Me.TxtEmployerAdd3.TabIndex = 65
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(2, 57)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(99, 16)
        Me.MyLabel6.TabIndex = 68
        Me.MyLabel6.Text = "Employer Address"
        '
        'TxtEmployerAdd2
        '
        Me.TxtEmployerAdd2.CalculationExpression = Nothing
        Me.TxtEmployerAdd2.FieldCode = Nothing
        Me.TxtEmployerAdd2.FieldDesc = Nothing
        Me.TxtEmployerAdd2.FieldMaxLength = 0
        Me.TxtEmployerAdd2.FieldName = Nothing
        Me.TxtEmployerAdd2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEmployerAdd2.isCalculatedField = False
        Me.TxtEmployerAdd2.IsSourceFromTable = False
        Me.TxtEmployerAdd2.IsSourceFromValueList = False
        Me.TxtEmployerAdd2.IsUnique = False
        Me.TxtEmployerAdd2.Location = New System.Drawing.Point(155, 77)
        Me.TxtEmployerAdd2.MaxLength = 50
        Me.TxtEmployerAdd2.MendatroryField = False
        Me.TxtEmployerAdd2.MyLinkLable1 = Me.MyLabel6
        Me.TxtEmployerAdd2.MyLinkLable2 = Nothing
        Me.TxtEmployerAdd2.Name = "TxtEmployerAdd2"
        Me.TxtEmployerAdd2.ReferenceFieldDesc = Nothing
        Me.TxtEmployerAdd2.ReferenceFieldName = Nothing
        Me.TxtEmployerAdd2.ReferenceTableName = Nothing
        Me.TxtEmployerAdd2.Size = New System.Drawing.Size(292, 18)
        Me.TxtEmployerAdd2.TabIndex = 64
        '
        'TxtEmployerAdd1
        '
        Me.TxtEmployerAdd1.CalculationExpression = Nothing
        Me.TxtEmployerAdd1.FieldCode = Nothing
        Me.TxtEmployerAdd1.FieldDesc = Nothing
        Me.TxtEmployerAdd1.FieldMaxLength = 0
        Me.TxtEmployerAdd1.FieldName = Nothing
        Me.TxtEmployerAdd1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEmployerAdd1.isCalculatedField = False
        Me.TxtEmployerAdd1.IsSourceFromTable = False
        Me.TxtEmployerAdd1.IsSourceFromValueList = False
        Me.TxtEmployerAdd1.IsUnique = False
        Me.TxtEmployerAdd1.Location = New System.Drawing.Point(155, 56)
        Me.TxtEmployerAdd1.MaxLength = 50
        Me.TxtEmployerAdd1.MendatroryField = False
        Me.TxtEmployerAdd1.MyLinkLable1 = Me.MyLabel6
        Me.TxtEmployerAdd1.MyLinkLable2 = Nothing
        Me.TxtEmployerAdd1.Name = "TxtEmployerAdd1"
        Me.TxtEmployerAdd1.ReferenceFieldDesc = Nothing
        Me.TxtEmployerAdd1.ReferenceFieldName = Nothing
        Me.TxtEmployerAdd1.ReferenceTableName = Nothing
        Me.TxtEmployerAdd1.Size = New System.Drawing.Size(292, 18)
        Me.TxtEmployerAdd1.TabIndex = 63
        '
        'TxtEmployerName
        '
        Me.TxtEmployerName.CalculationExpression = Nothing
        Me.TxtEmployerName.FieldCode = Nothing
        Me.TxtEmployerName.FieldDesc = Nothing
        Me.TxtEmployerName.FieldMaxLength = 0
        Me.TxtEmployerName.FieldName = Nothing
        Me.TxtEmployerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEmployerName.isCalculatedField = False
        Me.TxtEmployerName.IsSourceFromTable = False
        Me.TxtEmployerName.IsSourceFromValueList = False
        Me.TxtEmployerName.IsUnique = False
        Me.TxtEmployerName.Location = New System.Drawing.Point(155, 14)
        Me.TxtEmployerName.MaxLength = 50
        Me.TxtEmployerName.MendatroryField = False
        Me.TxtEmployerName.MyLinkLable1 = Me.MyLabel7
        Me.TxtEmployerName.MyLinkLable2 = Nothing
        Me.TxtEmployerName.Name = "TxtEmployerName"
        Me.TxtEmployerName.ReferenceFieldDesc = Nothing
        Me.TxtEmployerName.ReferenceFieldName = Nothing
        Me.TxtEmployerName.ReferenceTableName = Nothing
        Me.TxtEmployerName.Size = New System.Drawing.Size(292, 18)
        Me.TxtEmployerName.TabIndex = 62
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(2, 15)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel7.TabIndex = 69
        Me.MyLabel7.Text = "Employer Name"
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage5.Enabled = False
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(36.0!, 28.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(807, 488)
        Me.RadPageViewPage5.Text = "GST"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.TxtGstInNo)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel14)
        Me.RadGroupBox2.Controls.Add(Me.TxtGstReg)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel13)
        Me.RadGroupBox2.HeaderText = "GST"
        Me.RadGroupBox2.Location = New System.Drawing.Point(21, 14)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(457, 100)
        Me.RadGroupBox2.TabIndex = 0
        Me.RadGroupBox2.Text = "GST"
        '
        'TxtGstInNo
        '
        Me.TxtGstInNo.CalculationExpression = Nothing
        Me.TxtGstInNo.FieldCode = Nothing
        Me.TxtGstInNo.FieldDesc = Nothing
        Me.TxtGstInNo.FieldMaxLength = 0
        Me.TxtGstInNo.FieldName = Nothing
        Me.TxtGstInNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGstInNo.isCalculatedField = False
        Me.TxtGstInNo.IsSourceFromTable = False
        Me.TxtGstInNo.IsSourceFromValueList = False
        Me.TxtGstInNo.IsUnique = False
        Me.TxtGstInNo.Location = New System.Drawing.Point(126, 48)
        Me.TxtGstInNo.MaxLength = 50
        Me.TxtGstInNo.MendatroryField = False
        Me.TxtGstInNo.MyLinkLable1 = Me.MyLabel14
        Me.TxtGstInNo.MyLinkLable2 = Nothing
        Me.TxtGstInNo.Name = "TxtGstInNo"
        Me.TxtGstInNo.ReferenceFieldDesc = Nothing
        Me.TxtGstInNo.ReferenceFieldName = Nothing
        Me.TxtGstInNo.ReferenceTableName = Nothing
        Me.TxtGstInNo.Size = New System.Drawing.Size(292, 18)
        Me.TxtGstInNo.TabIndex = 62
        Me.TxtGstInNo.Visible = False
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(6, 50)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel14.TabIndex = 63
        Me.MyLabel14.Text = "GSTIN No."
        Me.MyLabel14.Visible = False
        '
        'TxtGstReg
        '
        Me.TxtGstReg.CalculationExpression = Nothing
        Me.TxtGstReg.FieldCode = Nothing
        Me.TxtGstReg.FieldDesc = Nothing
        Me.TxtGstReg.FieldMaxLength = 0
        Me.TxtGstReg.FieldName = Nothing
        Me.TxtGstReg.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGstReg.isCalculatedField = False
        Me.TxtGstReg.IsSourceFromTable = False
        Me.TxtGstReg.IsSourceFromValueList = False
        Me.TxtGstReg.IsUnique = False
        Me.TxtGstReg.Location = New System.Drawing.Point(125, 24)
        Me.TxtGstReg.MaxLength = 50
        Me.TxtGstReg.MendatroryField = False
        Me.TxtGstReg.MyLinkLable1 = Me.MyLabel13
        Me.TxtGstReg.MyLinkLable2 = Nothing
        Me.TxtGstReg.Name = "TxtGstReg"
        Me.TxtGstReg.ReferenceFieldDesc = Nothing
        Me.TxtGstReg.ReferenceFieldName = Nothing
        Me.TxtGstReg.ReferenceTableName = Nothing
        Me.TxtGstReg.Size = New System.Drawing.Size(292, 18)
        Me.TxtGstReg.TabIndex = 60
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(5, 26)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(111, 16)
        Me.MyLabel13.TabIndex = 61
        Me.MyLabel13.Text = "GST Registration No"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
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
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(830, 567)
        Me.SplitContainer1.SplitterDistance = 538
        Me.SplitContainer1.TabIndex = 0
        '
        'FrmCompanyMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(830, 587)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmCompanyMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Company Master"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.dtPanIssueDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTinIssueDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpInsurance_Valid_Date, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInsurance_Comp_Name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtinsuranceno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPFNO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPFNO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtESICNO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCompESIC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtIECode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCIN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTelephone2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTelephone1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcust_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_main_company, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkApplyMultiCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBaseCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCompanyNameTally, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCEDivision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIntegrateWithTally, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCompanyCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAccessOfficer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRegdNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRegNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAccessOfficer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWardNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWardNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lblWardNo.ResumeLayout(False)
        Me.lblWardNo.PerformLayout()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCircleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCircleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTcanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTcanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCEDivision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCECommissionerate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCECommissionerate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCERange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCERange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEccNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEccNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtServiceTaxRegNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblServiceTaxRegNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVatRegNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVatRegNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDataBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlModeofTransp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCformRequiredTN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCstLst, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCstLst, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTinNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPinCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPinCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTinNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTelephone2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTelephone1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCompanyName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCompanyName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.btnClearLogo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelectPath1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.btnClearLogo2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelectPath2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.RadPageViewPage4.PerformLayout()
        CType(Me.TxtEmployerDesg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtEmployerAdd3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtEmployerAdd2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtEmployerAdd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtEmployerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.TxtGstInNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtGstReg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtCompanyName As common.Controls.MyTextBox
    Friend WithEvents txtCity As common.Controls.MyTextBox
    Friend WithEvents txtAdd3 As common.Controls.MyTextBox
    Friend WithEvents txtAdd2 As common.Controls.MyTextBox
    Friend WithEvents txtAdd1 As common.Controls.MyTextBox
    Friend WithEvents txtEmail As common.Controls.MyTextBox
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents txtFax As common.Controls.MyTextBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents txtCstLst As common.Controls.MyTextBox
    Friend WithEvents txtTinNo As common.Controls.MyTextBox
    Friend WithEvents txtPinCode As common.Controls.MyTextBox
    Friend WithEvents txtRegdNo As common.Controls.MyTextBox
    Friend WithEvents chkCformRequiredTN As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents ddlModeofTransp As common.Controls.MyComboBox
    Friend WithEvents cboDataBase As common.Controls.MyComboBox
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnSelectPath1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents btnSelectPath2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClearLogo1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClearLogo2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtVatRegNo As common.Controls.MyTextBox
    Friend WithEvents txtServiceTaxRegNo As common.Controls.MyTextBox
    Friend WithEvents txtEccNo As common.Controls.MyTextBox
    Friend WithEvents txtCEDivision As common.Controls.MyTextBox
    Friend WithEvents txtCECommissionerate As common.Controls.MyTextBox
    Friend WithEvents txtCERange As common.Controls.MyTextBox
    Friend WithEvents txtPanNo As common.Controls.MyTextBox
    Friend WithEvents txtTcanNo As common.Controls.MyTextBox
    Friend WithEvents txtTanNo As common.Controls.MyTextBox
    Friend WithEvents txtAccessOfficer As common.Controls.MyTextBox
    Friend WithEvents txtWardNo As common.Controls.MyTextBox
    Friend WithEvents txtCircleNo As common.Controls.MyTextBox
    Friend WithEvents lblCompanyName As common.Controls.MyLabel
    Friend WithEvents lblCompanyCode As common.Controls.MyLabel
    Friend WithEvents lblTelephone2 As common.Controls.MyLabel
    Friend WithEvents lblTelephone1 As common.Controls.MyLabel
    Friend WithEvents lblCity As common.Controls.MyLabel
    Friend WithEvents lblAdress As common.Controls.MyLabel
    Friend WithEvents lblEmail As common.Controls.MyLabel
    Friend WithEvents lblTax As common.Controls.MyLabel
    Friend WithEvents lblTinNo As common.Controls.MyLabel
    Friend WithEvents lblCstLst As common.Controls.MyLabel
    Friend WithEvents lblState As common.Controls.MyLabel
    Friend WithEvents lblPinCode As common.Controls.MyLabel
    Friend WithEvents txtRegNo As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblVatRegNo As common.Controls.MyLabel
    Friend WithEvents lblServiceTaxRegNo As common.Controls.MyLabel
    Friend WithEvents lblCERange As common.Controls.MyLabel
    Friend WithEvents lblEccNo As common.Controls.MyLabel
    Friend WithEvents lblCEDivision As common.Controls.MyLabel
    Friend WithEvents lblCECommissionerate As common.Controls.MyLabel
    Friend WithEvents lblPanNo As common.Controls.MyLabel
    Friend WithEvents lblTcanNo As common.Controls.MyLabel
    Friend WithEvents lblTanNo As common.Controls.MyLabel
    Friend WithEvents lblWardNo As common.Controls.MyLabel
    Friend WithEvents lblCircleNo As common.Controls.MyLabel
    Friend WithEvents lblAccessOfficer As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents fndCompanyCode As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtState As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents chkIntegrateWithTally As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtCompanyNameTally As common.Controls.MyTextBox
    Friend WithEvents fndBaseCurrency As common.UserControls.txtFinder
    Friend WithEvents lblBaseCurrency As common.Controls.MyLabel
    Friend WithEvents chkApplyMultiCurrency As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chk_main_company As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtcust_name As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtcust_code As common.UserControls.txtFinder
    Friend WithEvents txtTelephone2 As Telerik.WinControls.UI.RadMaskedEditBox
    Friend WithEvents txtTelephone1 As Telerik.WinControls.UI.RadMaskedEditBox
    Friend WithEvents txtCIN As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents TxtIECode As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtPFNO As common.Controls.MyTextBox
    Friend WithEvents lblPFNO As common.Controls.MyLabel
    Friend WithEvents txtESICNO As common.Controls.MyTextBox
    Friend WithEvents lblCompESIC As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents TxtEmployerDesg As common.Controls.MyTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents TxtEmployerAdd3 As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents TxtEmployerAdd2 As common.Controls.MyTextBox
    Friend WithEvents TxtEmployerAdd1 As common.Controls.MyTextBox
    Friend WithEvents TxtEmployerName As common.Controls.MyTextBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtinsuranceno As common.Controls.MyTextBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtInsurance_Comp_Name As common.Controls.MyTextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents dtpInsurance_Valid_Date As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents dtTinIssueDate As common.Controls.MyDateTimePicker
    Friend WithEvents dtPanIssueDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage5 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents TxtGstInNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents TxtGstReg As common.Controls.MyTextBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
End Class

