Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmployee_Status
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmployee_Status))
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem9 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem10 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem11 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtTransferPF = New common.Controls.MyTextBox()
        Me.chkTransPF = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkProfessionalTaxApplicable = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkRevisionNo = New Telerik.WinControls.UI.RadCheckBox()
        Me.cboPFCalculatnType = New common.Controls.MyComboBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtEPFMaxLimit = New common.MyNumBox()
        Me.lblMaxLimit = New common.Controls.MyLabel()
        Me.lblEPFRate = New common.Controls.MyLabel()
        Me.txtESIRate = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtEPFRate = New common.MyNumBox()
        Me.txtESIMaxLim = New common.MyNumBox()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvLoanGeneration = New common.UserControls.MyRadGridView()
        Me.chkODApplicable = New Telerik.WinControls.UI.RadCheckBox()
        Me.cboConveyanceType = New common.Controls.MyComboBox()
        Me.MyLabel87 = New common.Controls.MyLabel()
        Me.fndConveyanceRate = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.cboShiftChangeType = New common.Controls.MyComboBox()
        Me.fndShift = New common.UserControls.txtFinder()
        Me.MyLabel51 = New common.Controls.MyLabel()
        Me.lblBonusName = New common.Controls.MyLabel()
        Me.chkEPStoEPF = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblOTName = New common.Controls.MyLabel()
        Me.cboWorkingStatus = New common.Controls.MyComboBox()
        Me.lblWorkingStatus = New common.Controls.MyLabel()
        Me.lblBankName = New common.Controls.MyLabel()
        Me.lblAttendanceName = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.lblGradeName = New common.Controls.MyLabel()
        Me.lblBranch = New common.Controls.MyLabel()
        Me.lblBranchName = New common.Controls.MyLabel()
        Me.lblDevisionName = New common.Controls.MyLabel()
        Me.lblReportingPersonName = New common.Controls.MyLabel()
        Me.lblNameinAcc = New common.Controls.MyLabel()
        Me.lblDepartmentName = New common.Controls.MyLabel()
        Me.lblESINo = New common.Controls.MyLabel()
        Me.lblDesignationName = New common.Controls.MyLabel()
        Me.lblPFNo = New common.Controls.MyLabel()
        Me.lblEmpName = New common.Controls.MyLabel()
        Me.lblBank = New common.Controls.MyLabel()
        Me.txtEmpCode = New common.UserControls.txtFinder()
        Me.lblEmpCode = New common.Controls.MyLabel()
        Me.lblGrade = New common.Controls.MyLabel()
        Me.lblCode = New common.Controls.MyLabel()
        Me.lblRevisionNo = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.txtRevisionNo = New common.MyNumBox()
        Me.dtpApplicableFrom = New common.Controls.MyDateTimePicker()
        Me.lblApplicableFrom = New common.Controls.MyLabel()
        Me.findBonus = New common.UserControls.txtFinder()
        Me.lblBonusCode = New common.Controls.MyLabel()
        Me.lbldesignation = New common.Controls.MyLabel()
        Me.chkBonusApplicable = New Telerik.WinControls.UI.RadCheckBox()
        Me.finddesignation = New common.UserControls.txtFinder()
        Me.findOT = New common.UserControls.txtFinder()
        Me.lblOTCode = New common.Controls.MyLabel()
        Me.lblDepartment = New common.Controls.MyLabel()
        Me.findDepartment = New common.UserControls.txtFinder()
        Me.chkOtApplicable = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblDevision = New common.Controls.MyLabel()
        Me.txtEsiNo = New common.Controls.MyTextBox()
        Me.findDevision = New common.UserControls.txtFinder()
        Me.chkEsiApplicable = New Telerik.WinControls.UI.RadCheckBox()
        Me.findGrade = New common.UserControls.txtFinder()
        Me.txtPfNo = New common.Controls.MyTextBox()
        Me.chkPFApplicable = New Telerik.WinControls.UI.RadCheckBox()
        Me.findBranch = New common.UserControls.txtFinder()
        Me.findBank = New common.UserControls.txtFinder()
        Me.lblReportingPerson = New common.Controls.MyLabel()
        Me.cboPaymentMode = New common.Controls.MyComboBox()
        Me.lblPaymentMode = New common.Controls.MyLabel()
        Me.findEmployee = New common.UserControls.txtFinder()
        Me.lblAttendance = New common.Controls.MyLabel()
        Me.txtNameinAccount = New common.Controls.MyTextBox()
        Me.findAttendance = New common.UserControls.txtFinder()
        Me.txtaccno = New common.Controls.MyTextBox()
        Me.lblBankAccNo = New common.Controls.MyLabel()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel43 = New common.Controls.MyLabel()
        Me.txtGPFNo = New common.Controls.MyTextBox()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtTransferPF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTransPF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkProfessionalTaxApplicable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRevisionNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPFCalculatnType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEPFMaxLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMaxLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEPFRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtESIRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEPFRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtESIMaxLim, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        CType(Me.gvLoanGeneration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvLoanGeneration.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkODApplicable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboConveyanceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel87, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboShiftChangeType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel51, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBonusName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEPStoEPF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOTName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboWorkingStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWorkingStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAttendanceName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGradeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBranchName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDevisionName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReportingPersonName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNameinAcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartmentName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblESINo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDesignationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPFNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGrade, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRevisionNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRevisionNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpApplicableFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApplicableFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBonusCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldesignation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBonusApplicable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOTCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOtApplicable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDevision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEsiNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEsiApplicable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPfNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPFApplicable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReportingPerson, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPaymentMode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPaymentMode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAttendance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNameinAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtaccno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankAccNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGPFNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(913, 20)
        Me.RadMenu2.TabIndex = 214
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Import"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel43)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtGPFNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTransferPF)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkTransPF)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkProfessionalTaxApplicable)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkRevisionNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboPFCalculatnType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEPFMaxLimit)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblMaxLimit)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEPFRate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtESIRate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEPFRate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtESIMaxLim)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkODApplicable)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboConveyanceType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel87)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndConveyanceRate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboShiftChangeType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndShift)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel51)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBonusName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkEPStoEPF)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblOTName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboWorkingStatus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBankName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblWorkingStatus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAttendanceName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblGradeName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBranch)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBranchName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDevisionName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblReportingPersonName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblNameinAcc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDepartmentName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblESINo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDesignationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPFNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEmpName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBank)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEmpCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblGrade)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEmpCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRevisionNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRevisionNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpApplicableFrom)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findBonus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblApplicableFrom)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBonusCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbldesignation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkBonusApplicable)
        Me.SplitContainer1.Panel1.Controls.Add(Me.finddesignation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findOT)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDepartment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblOTCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findDepartment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkOtApplicable)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDevision)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEsiNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findDevision)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkEsiApplicable)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findGrade)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPfNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkPFApplicable)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findBranch)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findBank)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblReportingPerson)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboPaymentMode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findEmployee)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPaymentMode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAttendance)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtNameinAccount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findAttendance)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtaccno)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBankAccNo)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(913, 486)
        Me.SplitContainer1.SplitterDistance = 448
        Me.SplitContainer1.TabIndex = 215
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
        Me.txtTransferPF.Location = New System.Drawing.Point(669, 170)
        Me.txtTransferPF.MaxLength = 49
        Me.txtTransferPF.MendatroryField = False
        Me.txtTransferPF.MyLinkLable1 = Nothing
        Me.txtTransferPF.MyLinkLable2 = Nothing
        Me.txtTransferPF.Name = "txtTransferPF"
        Me.txtTransferPF.ReferenceFieldDesc = Nothing
        Me.txtTransferPF.ReferenceFieldName = Nothing
        Me.txtTransferPF.ReferenceTableName = Nothing
        Me.txtTransferPF.Size = New System.Drawing.Size(228, 18)
        Me.txtTransferPF.TabIndex = 175
        '
        'chkTransPF
        '
        Me.chkTransPF.Location = New System.Drawing.Point(586, 170)
        Me.chkTransPF.Name = "chkTransPF"
        Me.chkTransPF.Size = New System.Drawing.Size(75, 18)
        Me.chkTransPF.TabIndex = 174
        Me.chkTransPF.Text = "Transfer PF"
        '
        'chkProfessionalTaxApplicable
        '
        Me.chkProfessionalTaxApplicable.Location = New System.Drawing.Point(586, 147)
        Me.chkProfessionalTaxApplicable.Name = "chkProfessionalTaxApplicable"
        Me.chkProfessionalTaxApplicable.Size = New System.Drawing.Size(156, 18)
        Me.chkProfessionalTaxApplicable.TabIndex = 235
        Me.chkProfessionalTaxApplicable.Text = "Professional Tax Applicable"
        '
        'chkRevisionNo
        '
        Me.chkRevisionNo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRevisionNo.Location = New System.Drawing.Point(586, 10)
        Me.chkRevisionNo.Name = "chkRevisionNo"
        '
        '
        '
        Me.chkRevisionNo.RootElement.StretchHorizontally = True
        Me.chkRevisionNo.RootElement.StretchVertically = True
        Me.chkRevisionNo.Size = New System.Drawing.Size(122, 18)
        Me.chkRevisionNo.TabIndex = 234
        Me.chkRevisionNo.Text = "Latest Revision No"
        Me.chkRevisionNo.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
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
        Me.cboPFCalculatnType.Location = New System.Drawing.Point(727, 57)
        Me.cboPFCalculatnType.MendatroryField = False
        Me.cboPFCalculatnType.MyLinkLable1 = Nothing
        Me.cboPFCalculatnType.MyLinkLable2 = Nothing
        Me.cboPFCalculatnType.Name = "cboPFCalculatnType"
        Me.cboPFCalculatnType.ReferenceFieldDesc = Nothing
        Me.cboPFCalculatnType.ReferenceFieldName = Nothing
        Me.cboPFCalculatnType.ReferenceTableName = Nothing
        Me.cboPFCalculatnType.Size = New System.Drawing.Size(149, 18)
        Me.cboPFCalculatnType.TabIndex = 233
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(452, 80)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel5.TabIndex = 232
        Me.MyLabel5.Text = "ESI Rate"
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
        Me.txtEPFMaxLimit.Location = New System.Drawing.Point(652, 56)
        Me.txtEPFMaxLimit.MendatroryField = True
        Me.txtEPFMaxLimit.MyLinkLable1 = Me.lblMaxLimit
        Me.txtEPFMaxLimit.MyLinkLable2 = Nothing
        Me.txtEPFMaxLimit.Name = "txtEPFMaxLimit"
        Me.txtEPFMaxLimit.ReadOnly = True
        Me.txtEPFMaxLimit.ReferenceFieldDesc = Nothing
        Me.txtEPFMaxLimit.ReferenceFieldName = Nothing
        Me.txtEPFMaxLimit.ReferenceTableName = Nothing
        Me.txtEPFMaxLimit.Size = New System.Drawing.Size(74, 20)
        Me.txtEPFMaxLimit.TabIndex = 225
        Me.txtEPFMaxLimit.Text = "0"
        Me.txtEPFMaxLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtEPFMaxLimit.Value = 0R
        '
        'lblMaxLimit
        '
        Me.lblMaxLimit.FieldName = Nothing
        Me.lblMaxLimit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMaxLimit.Location = New System.Drawing.Point(586, 58)
        Me.lblMaxLimit.Name = "lblMaxLimit"
        Me.lblMaxLimit.Size = New System.Drawing.Size(55, 16)
        Me.lblMaxLimit.TabIndex = 226
        Me.lblMaxLimit.Text = "Max Limit"
        '
        'lblEPFRate
        '
        Me.lblEPFRate.FieldName = Nothing
        Me.lblEPFRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEPFRate.Location = New System.Drawing.Point(452, 58)
        Me.lblEPFRate.Name = "lblEPFRate"
        Me.lblEPFRate.Size = New System.Drawing.Size(55, 16)
        Me.lblEPFRate.TabIndex = 231
        Me.lblEPFRate.Text = "EPF Rate"
        '
        'txtESIRate
        '
        Me.txtESIRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtESIRate.CalculationExpression = Nothing
        Me.txtESIRate.DecimalPlaces = 0
        Me.txtESIRate.FieldCode = Nothing
        Me.txtESIRate.FieldDesc = Nothing
        Me.txtESIRate.FieldMaxLength = 0
        Me.txtESIRate.FieldName = Nothing
        Me.txtESIRate.isCalculatedField = False
        Me.txtESIRate.IsSourceFromTable = False
        Me.txtESIRate.IsSourceFromValueList = False
        Me.txtESIRate.IsUnique = False
        Me.txtESIRate.Location = New System.Drawing.Point(508, 78)
        Me.txtESIRate.MendatroryField = True
        Me.txtESIRate.MyLinkLable1 = Me.MyLabel4
        Me.txtESIRate.MyLinkLable2 = Nothing
        Me.txtESIRate.Name = "txtESIRate"
        Me.txtESIRate.ReadOnly = True
        Me.txtESIRate.ReferenceFieldDesc = Nothing
        Me.txtESIRate.ReferenceFieldName = Nothing
        Me.txtESIRate.ReferenceTableName = Nothing
        Me.txtESIRate.Size = New System.Drawing.Size(74, 20)
        Me.txtESIRate.TabIndex = 230
        Me.txtESIRate.Text = "0"
        Me.txtESIRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtESIRate.Value = 0R
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(586, 80)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel4.TabIndex = 228
        Me.MyLabel4.Text = "Max Limit"
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
        Me.txtEPFRate.Location = New System.Drawing.Point(508, 56)
        Me.txtEPFRate.MendatroryField = True
        Me.txtEPFRate.MyLinkLable1 = Me.lblMaxLimit
        Me.txtEPFRate.MyLinkLable2 = Nothing
        Me.txtEPFRate.Name = "txtEPFRate"
        Me.txtEPFRate.ReadOnly = True
        Me.txtEPFRate.ReferenceFieldDesc = Nothing
        Me.txtEPFRate.ReferenceFieldName = Nothing
        Me.txtEPFRate.ReferenceTableName = Nothing
        Me.txtEPFRate.Size = New System.Drawing.Size(74, 20)
        Me.txtEPFRate.TabIndex = 229
        Me.txtEPFRate.Text = "0"
        Me.txtEPFRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtEPFRate.Value = 0R
        '
        'txtESIMaxLim
        '
        Me.txtESIMaxLim.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtESIMaxLim.CalculationExpression = Nothing
        Me.txtESIMaxLim.DecimalPlaces = 0
        Me.txtESIMaxLim.FieldCode = Nothing
        Me.txtESIMaxLim.FieldDesc = Nothing
        Me.txtESIMaxLim.FieldMaxLength = 0
        Me.txtESIMaxLim.FieldName = Nothing
        Me.txtESIMaxLim.isCalculatedField = False
        Me.txtESIMaxLim.IsSourceFromTable = False
        Me.txtESIMaxLim.IsSourceFromValueList = False
        Me.txtESIMaxLim.IsUnique = False
        Me.txtESIMaxLim.Location = New System.Drawing.Point(652, 78)
        Me.txtESIMaxLim.MendatroryField = True
        Me.txtESIMaxLim.MyLinkLable1 = Me.MyLabel4
        Me.txtESIMaxLim.MyLinkLable2 = Nothing
        Me.txtESIMaxLim.Name = "txtESIMaxLim"
        Me.txtESIMaxLim.ReadOnly = True
        Me.txtESIMaxLim.ReferenceFieldDesc = Nothing
        Me.txtESIMaxLim.ReferenceFieldName = Nothing
        Me.txtESIMaxLim.ReferenceTableName = Nothing
        Me.txtESIMaxLim.Size = New System.Drawing.Size(74, 20)
        Me.txtESIMaxLim.TabIndex = 227
        Me.txtESIMaxLim.Text = "0"
        Me.txtESIMaxLim.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtESIMaxLim.Value = 0R
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.gvLoanGeneration)
        Me.RadGroupBox6.HeaderText = "Weekly Holidays"
        Me.RadGroupBox6.Location = New System.Drawing.Point(586, 304)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(290, 137)
        Me.RadGroupBox6.TabIndex = 224
        Me.RadGroupBox6.Text = "Weekly Holidays"
        '
        'gvLoanGeneration
        '
        Me.gvLoanGeneration.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvLoanGeneration.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvLoanGeneration.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvLoanGeneration.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvLoanGeneration.ForeColor = System.Drawing.Color.Black
        Me.gvLoanGeneration.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvLoanGeneration.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gvLoanGeneration.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvLoanGeneration.MasterTemplate.AllowAddNewRow = False
        Me.gvLoanGeneration.MasterTemplate.AutoGenerateColumns = False
        Me.gvLoanGeneration.MasterTemplate.EnableGrouping = False
        Me.gvLoanGeneration.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvLoanGeneration.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvLoanGeneration.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvLoanGeneration.MyStopExport = False
        Me.gvLoanGeneration.Name = "gvLoanGeneration"
        Me.gvLoanGeneration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvLoanGeneration.ShowHeaderCellButtons = True
        Me.gvLoanGeneration.Size = New System.Drawing.Size(270, 107)
        Me.gvLoanGeneration.TabIndex = 223
        Me.gvLoanGeneration.TabStop = False
        '
        'chkODApplicable
        '
        Me.chkODApplicable.Location = New System.Drawing.Point(586, 124)
        Me.chkODApplicable.Name = "chkODApplicable"
        Me.chkODApplicable.Size = New System.Drawing.Size(103, 18)
        Me.chkODApplicable.TabIndex = 222
        Me.chkODApplicable.Text = "Is OD Applicable"
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
        RadListDataItem1.Text = "Male"
        RadListDataItem2.Text = "Female"
        Me.cboConveyanceType.Items.Add(RadListDataItem1)
        Me.cboConveyanceType.Items.Add(RadListDataItem2)
        Me.cboConveyanceType.Location = New System.Drawing.Point(689, 260)
        Me.cboConveyanceType.MendatroryField = True
        Me.cboConveyanceType.MyLinkLable1 = Me.MyLabel87
        Me.cboConveyanceType.MyLinkLable2 = Nothing
        Me.cboConveyanceType.Name = "cboConveyanceType"
        Me.cboConveyanceType.ReferenceFieldDesc = Nothing
        Me.cboConveyanceType.ReferenceFieldName = Nothing
        Me.cboConveyanceType.ReferenceTableName = Nothing
        Me.cboConveyanceType.Size = New System.Drawing.Size(187, 18)
        Me.cboConveyanceType.TabIndex = 221
        '
        'MyLabel87
        '
        Me.MyLabel87.FieldName = Nothing
        Me.MyLabel87.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel87.Location = New System.Drawing.Point(586, 261)
        Me.MyLabel87.Name = "MyLabel87"
        Me.MyLabel87.Size = New System.Drawing.Size(97, 16)
        Me.MyLabel87.TabIndex = 219
        Me.MyLabel87.Text = "Conveyance Type"
        '
        'fndConveyanceRate
        '
        Me.fndConveyanceRate.CalculationExpression = Nothing
        Me.fndConveyanceRate.FieldCode = Nothing
        Me.fndConveyanceRate.FieldDesc = Nothing
        Me.fndConveyanceRate.FieldMaxLength = 0
        Me.fndConveyanceRate.FieldName = Nothing
        Me.fndConveyanceRate.isCalculatedField = False
        Me.fndConveyanceRate.IsSourceFromTable = False
        Me.fndConveyanceRate.IsSourceFromValueList = False
        Me.fndConveyanceRate.IsUnique = False
        Me.fndConveyanceRate.Location = New System.Drawing.Point(689, 283)
        Me.fndConveyanceRate.MendatroryField = False
        Me.fndConveyanceRate.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndConveyanceRate.MyLinkLable1 = Nothing
        Me.fndConveyanceRate.MyLinkLable2 = Nothing
        Me.fndConveyanceRate.MyReadOnly = False
        Me.fndConveyanceRate.MyShowMasterFormButton = False
        Me.fndConveyanceRate.Name = "fndConveyanceRate"
        Me.fndConveyanceRate.ReferenceFieldDesc = Nothing
        Me.fndConveyanceRate.ReferenceFieldName = Nothing
        Me.fndConveyanceRate.ReferenceTableName = Nothing
        Me.fndConveyanceRate.Size = New System.Drawing.Size(187, 19)
        Me.fndConveyanceRate.TabIndex = 217
        Me.fndConveyanceRate.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(586, 284)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(96, 16)
        Me.MyLabel1.TabIndex = 218
        Me.MyLabel1.Text = "Conveyance Rate"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(586, 238)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(100, 16)
        Me.MyLabel3.TabIndex = 216
        Me.MyLabel3.Text = "Shift Change Type"
        '
        'cboShiftChangeType
        '
        Me.cboShiftChangeType.AutoCompleteDisplayMember = Nothing
        Me.cboShiftChangeType.AutoCompleteValueMember = Nothing
        Me.cboShiftChangeType.CalculationExpression = Nothing
        Me.cboShiftChangeType.DropDownAnimationEnabled = True
        Me.cboShiftChangeType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboShiftChangeType.FieldCode = Nothing
        Me.cboShiftChangeType.FieldDesc = Nothing
        Me.cboShiftChangeType.FieldMaxLength = 0
        Me.cboShiftChangeType.FieldName = Nothing
        Me.cboShiftChangeType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShiftChangeType.isCalculatedField = False
        Me.cboShiftChangeType.IsSourceFromTable = False
        Me.cboShiftChangeType.IsSourceFromValueList = False
        Me.cboShiftChangeType.IsUnique = False
        RadListDataItem3.Text = "Male"
        RadListDataItem4.Text = "Female"
        Me.cboShiftChangeType.Items.Add(RadListDataItem3)
        Me.cboShiftChangeType.Items.Add(RadListDataItem4)
        Me.cboShiftChangeType.Location = New System.Drawing.Point(689, 237)
        Me.cboShiftChangeType.MendatroryField = True
        Me.cboShiftChangeType.MyLinkLable1 = Me.MyLabel3
        Me.cboShiftChangeType.MyLinkLable2 = Nothing
        Me.cboShiftChangeType.Name = "cboShiftChangeType"
        Me.cboShiftChangeType.ReferenceFieldDesc = Nothing
        Me.cboShiftChangeType.ReferenceFieldName = Nothing
        Me.cboShiftChangeType.ReferenceTableName = Nothing
        Me.cboShiftChangeType.Size = New System.Drawing.Size(187, 18)
        Me.cboShiftChangeType.TabIndex = 215
        '
        'fndShift
        '
        Me.fndShift.CalculationExpression = Nothing
        Me.fndShift.FieldCode = Nothing
        Me.fndShift.FieldDesc = Nothing
        Me.fndShift.FieldMaxLength = 0
        Me.fndShift.FieldName = Nothing
        Me.fndShift.isCalculatedField = False
        Me.fndShift.IsSourceFromTable = False
        Me.fndShift.IsSourceFromValueList = False
        Me.fndShift.IsUnique = False
        Me.fndShift.Location = New System.Drawing.Point(689, 214)
        Me.fndShift.MendatroryField = False
        Me.fndShift.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndShift.MyLinkLable1 = Nothing
        Me.fndShift.MyLinkLable2 = Nothing
        Me.fndShift.MyReadOnly = False
        Me.fndShift.MyShowMasterFormButton = False
        Me.fndShift.Name = "fndShift"
        Me.fndShift.ReferenceFieldDesc = Nothing
        Me.fndShift.ReferenceFieldName = Nothing
        Me.fndShift.ReferenceTableName = Nothing
        Me.fndShift.Size = New System.Drawing.Size(187, 19)
        Me.fndShift.TabIndex = 213
        Me.fndShift.Value = ""
        '
        'MyLabel51
        '
        Me.MyLabel51.FieldName = Nothing
        Me.MyLabel51.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel51.Location = New System.Drawing.Point(586, 215)
        Me.MyLabel51.Name = "MyLabel51"
        Me.MyLabel51.Size = New System.Drawing.Size(29, 16)
        Me.MyLabel51.TabIndex = 214
        Me.MyLabel51.Text = "Shift"
        '
        'lblBonusName
        '
        Me.lblBonusName.AutoSize = False
        Me.lblBonusName.BorderVisible = True
        Me.lblBonusName.FieldName = Nothing
        Me.lblBonusName.Location = New System.Drawing.Point(352, 418)
        Me.lblBonusName.Name = "lblBonusName"
        Me.lblBonusName.Size = New System.Drawing.Size(230, 19)
        Me.lblBonusName.TabIndex = 212
        Me.lblBonusName.TextWrap = False
        '
        'chkEPStoEPF
        '
        Me.chkEPStoEPF.Location = New System.Drawing.Point(586, 101)
        Me.chkEPStoEPF.Name = "chkEPStoEPF"
        '
        '
        '
        Me.chkEPStoEPF.RootElement.StretchHorizontally = True
        Me.chkEPStoEPF.RootElement.StretchVertically = True
        Me.chkEPStoEPF.Size = New System.Drawing.Size(73, 18)
        Me.chkEPStoEPF.TabIndex = 212
        Me.chkEPStoEPF.Text = "EPS to EPF"
        '
        'lblOTName
        '
        Me.lblOTName.AutoSize = False
        Me.lblOTName.BorderVisible = True
        Me.lblOTName.FieldName = Nothing
        Me.lblOTName.Location = New System.Drawing.Point(352, 395)
        Me.lblOTName.Name = "lblOTName"
        Me.lblOTName.Size = New System.Drawing.Size(230, 19)
        Me.lblOTName.TabIndex = 211
        Me.lblOTName.TextWrap = False
        '
        'cboWorkingStatus
        '
        Me.cboWorkingStatus.AutoCompleteDisplayMember = Nothing
        Me.cboWorkingStatus.AutoCompleteValueMember = Nothing
        Me.cboWorkingStatus.CalculationExpression = Nothing
        Me.cboWorkingStatus.DropDownAnimationEnabled = True
        Me.cboWorkingStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboWorkingStatus.FieldCode = Nothing
        Me.cboWorkingStatus.FieldDesc = Nothing
        Me.cboWorkingStatus.FieldMaxLength = 0
        Me.cboWorkingStatus.FieldName = Nothing
        Me.cboWorkingStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboWorkingStatus.isCalculatedField = False
        Me.cboWorkingStatus.IsSourceFromTable = False
        Me.cboWorkingStatus.IsSourceFromValueList = False
        Me.cboWorkingStatus.IsUnique = False
        RadListDataItem5.Text = "Working"
        RadListDataItem6.Text = "Resigned"
        Me.cboWorkingStatus.Items.Add(RadListDataItem5)
        Me.cboWorkingStatus.Items.Add(RadListDataItem6)
        Me.cboWorkingStatus.Location = New System.Drawing.Point(108, 79)
        Me.cboWorkingStatus.MendatroryField = True
        Me.cboWorkingStatus.MyLinkLable1 = Me.lblWorkingStatus
        Me.cboWorkingStatus.MyLinkLable2 = Nothing
        Me.cboWorkingStatus.Name = "cboWorkingStatus"
        Me.cboWorkingStatus.ReferenceFieldDesc = Nothing
        Me.cboWorkingStatus.ReferenceFieldName = Nothing
        Me.cboWorkingStatus.ReferenceTableName = Nothing
        '
        '
        '
        Me.cboWorkingStatus.RootElement.StretchVertically = True
        Me.cboWorkingStatus.Size = New System.Drawing.Size(243, 18)
        Me.cboWorkingStatus.TabIndex = 210
        '
        'lblWorkingStatus
        '
        Me.lblWorkingStatus.FieldName = Nothing
        Me.lblWorkingStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWorkingStatus.Location = New System.Drawing.Point(6, 80)
        Me.lblWorkingStatus.Name = "lblWorkingStatus"
        Me.lblWorkingStatus.Size = New System.Drawing.Size(83, 16)
        Me.lblWorkingStatus.TabIndex = 211
        Me.lblWorkingStatus.Text = "Working Status"
        '
        'lblBankName
        '
        Me.lblBankName.AutoSize = False
        Me.lblBankName.BorderVisible = True
        Me.lblBankName.FieldName = Nothing
        Me.lblBankName.Location = New System.Drawing.Point(352, 328)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(230, 19)
        Me.lblBankName.TabIndex = 210
        Me.lblBankName.TextWrap = False
        '
        'lblAttendanceName
        '
        Me.lblAttendanceName.AutoSize = False
        Me.lblAttendanceName.BorderVisible = True
        Me.lblAttendanceName.FieldName = Nothing
        Me.lblAttendanceName.Location = New System.Drawing.Point(352, 283)
        Me.lblAttendanceName.Name = "lblAttendanceName"
        Me.lblAttendanceName.Size = New System.Drawing.Size(230, 19)
        Me.lblAttendanceName.TabIndex = 209
        Me.lblAttendanceName.TextWrap = False
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(331, 9)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(20, 21)
        Me.btnNew.TabIndex = 209
        Me.btnNew.Text = " "
        '
        'lblGradeName
        '
        Me.lblGradeName.AutoSize = False
        Me.lblGradeName.BorderVisible = True
        Me.lblGradeName.FieldName = Nothing
        Me.lblGradeName.Location = New System.Drawing.Point(352, 260)
        Me.lblGradeName.Name = "lblGradeName"
        Me.lblGradeName.Size = New System.Drawing.Size(230, 19)
        Me.lblGradeName.TabIndex = 208
        Me.lblGradeName.TextWrap = False
        '
        'lblBranch
        '
        Me.lblBranch.FieldName = Nothing
        Me.lblBranch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBranch.Location = New System.Drawing.Point(6, 215)
        Me.lblBranch.Name = "lblBranch"
        Me.lblBranch.Size = New System.Drawing.Size(49, 16)
        Me.lblBranch.TabIndex = 208
        Me.lblBranch.Text = "Location"
        '
        'lblBranchName
        '
        Me.lblBranchName.AutoSize = False
        Me.lblBranchName.BorderVisible = True
        Me.lblBranchName.FieldName = Nothing
        Me.lblBranchName.Location = New System.Drawing.Point(352, 214)
        Me.lblBranchName.Name = "lblBranchName"
        Me.lblBranchName.Size = New System.Drawing.Size(230, 19)
        Me.lblBranchName.TabIndex = 206
        Me.lblBranchName.TextWrap = False
        '
        'lblDevisionName
        '
        Me.lblDevisionName.AutoSize = False
        Me.lblDevisionName.BorderVisible = True
        Me.lblDevisionName.FieldName = Nothing
        Me.lblDevisionName.Location = New System.Drawing.Point(352, 237)
        Me.lblDevisionName.Name = "lblDevisionName"
        Me.lblDevisionName.Size = New System.Drawing.Size(230, 19)
        Me.lblDevisionName.TabIndex = 207
        Me.lblDevisionName.TextWrap = False
        '
        'lblReportingPersonName
        '
        Me.lblReportingPersonName.AutoSize = False
        Me.lblReportingPersonName.BorderVisible = True
        Me.lblReportingPersonName.FieldName = Nothing
        Me.lblReportingPersonName.Location = New System.Drawing.Point(352, 147)
        Me.lblReportingPersonName.Name = "lblReportingPersonName"
        Me.lblReportingPersonName.Size = New System.Drawing.Size(230, 19)
        Me.lblReportingPersonName.TabIndex = 205
        Me.lblReportingPersonName.TextWrap = False
        '
        'lblNameinAcc
        '
        Me.lblNameinAcc.FieldName = Nothing
        Me.lblNameinAcc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNameinAcc.Location = New System.Drawing.Point(6, 307)
        Me.lblNameinAcc.Name = "lblNameinAcc"
        Me.lblNameinAcc.Size = New System.Drawing.Size(92, 16)
        Me.lblNameinAcc.TabIndex = 93
        Me.lblNameinAcc.Text = "Name in Account"
        '
        'lblDepartmentName
        '
        Me.lblDepartmentName.AutoSize = False
        Me.lblDepartmentName.BorderVisible = True
        Me.lblDepartmentName.FieldName = Nothing
        Me.lblDepartmentName.Location = New System.Drawing.Point(352, 124)
        Me.lblDepartmentName.Name = "lblDepartmentName"
        Me.lblDepartmentName.Size = New System.Drawing.Size(230, 19)
        Me.lblDepartmentName.TabIndex = 204
        Me.lblDepartmentName.TextWrap = False
        '
        'lblESINo
        '
        Me.lblESINo.FieldName = Nothing
        Me.lblESINo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblESINo.Location = New System.Drawing.Point(6, 374)
        Me.lblESINo.Name = "lblESINo"
        Me.lblESINo.Size = New System.Drawing.Size(42, 16)
        Me.lblESINo.TabIndex = 103
        Me.lblESINo.Text = "ESI No"
        '
        'lblDesignationName
        '
        Me.lblDesignationName.AutoSize = False
        Me.lblDesignationName.BorderVisible = True
        Me.lblDesignationName.FieldName = Nothing
        Me.lblDesignationName.Location = New System.Drawing.Point(352, 101)
        Me.lblDesignationName.Name = "lblDesignationName"
        Me.lblDesignationName.Size = New System.Drawing.Size(230, 19)
        Me.lblDesignationName.TabIndex = 203
        Me.lblDesignationName.TextWrap = False
        '
        'lblPFNo
        '
        Me.lblPFNo.FieldName = Nothing
        Me.lblPFNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPFNo.Location = New System.Drawing.Point(6, 352)
        Me.lblPFNo.Name = "lblPFNo"
        Me.lblPFNo.Size = New System.Drawing.Size(38, 16)
        Me.lblPFNo.TabIndex = 100
        Me.lblPFNo.Text = "PF No"
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.FieldName = Nothing
        Me.lblEmpName.Location = New System.Drawing.Point(352, 34)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(230, 19)
        Me.lblEmpName.TabIndex = 202
        Me.lblEmpName.TextWrap = False
        '
        'lblBank
        '
        Me.lblBank.FieldName = Nothing
        Me.lblBank.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBank.Location = New System.Drawing.Point(6, 329)
        Me.lblBank.Name = "lblBank"
        Me.lblBank.Size = New System.Drawing.Size(65, 16)
        Me.lblBank.TabIndex = 96
        Me.lblBank.Text = "Bank Name"
        '
        'txtEmpCode
        '
        Me.txtEmpCode.CalculationExpression = Nothing
        Me.txtEmpCode.FieldCode = Nothing
        Me.txtEmpCode.FieldDesc = Nothing
        Me.txtEmpCode.FieldMaxLength = 0
        Me.txtEmpCode.FieldName = Nothing
        Me.txtEmpCode.isCalculatedField = False
        Me.txtEmpCode.IsSourceFromTable = False
        Me.txtEmpCode.IsSourceFromValueList = False
        Me.txtEmpCode.IsUnique = False
        Me.txtEmpCode.Location = New System.Drawing.Point(108, 34)
        Me.txtEmpCode.MendatroryField = True
        Me.txtEmpCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.MyLinkLable1 = Me.lblEmpCode
        Me.txtEmpCode.MyLinkLable2 = Nothing
        Me.txtEmpCode.MyReadOnly = False
        Me.txtEmpCode.MyShowMasterFormButton = False
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.ReferenceFieldDesc = Nothing
        Me.txtEmpCode.ReferenceFieldName = Nothing
        Me.txtEmpCode.ReferenceTableName = Nothing
        Me.txtEmpCode.Size = New System.Drawing.Size(243, 19)
        Me.txtEmpCode.TabIndex = 116
        Me.txtEmpCode.Value = ""
        '
        'lblEmpCode
        '
        Me.lblEmpCode.FieldName = Nothing
        Me.lblEmpCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpCode.Location = New System.Drawing.Point(6, 35)
        Me.lblEmpCode.Name = "lblEmpCode"
        Me.lblEmpCode.Size = New System.Drawing.Size(87, 16)
        Me.lblEmpCode.TabIndex = 117
        Me.lblEmpCode.Text = "Employee Code"
        '
        'lblGrade
        '
        Me.lblGrade.FieldName = Nothing
        Me.lblGrade.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGrade.Location = New System.Drawing.Point(6, 261)
        Me.lblGrade.Name = "lblGrade"
        Me.lblGrade.Size = New System.Drawing.Size(38, 16)
        Me.lblGrade.TabIndex = 78
        Me.lblGrade.Text = "Grade"
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(6, 11)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(69, 16)
        Me.lblCode.TabIndex = 55
        Me.lblCode.Text = "Status Code"
        '
        'lblRevisionNo
        '
        Me.lblRevisionNo.FieldName = Nothing
        Me.lblRevisionNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRevisionNo.Location = New System.Drawing.Point(354, 11)
        Me.lblRevisionNo.Name = "lblRevisionNo"
        Me.lblRevisionNo.Size = New System.Drawing.Size(67, 16)
        Me.lblRevisionNo.TabIndex = 115
        Me.lblRevisionNo.Text = "Revision No"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(108, 9)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(223, 21)
        Me.txtCode.TabIndex = 54
        Me.txtCode.Value = ""
        '
        'txtRevisionNo
        '
        Me.txtRevisionNo.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtRevisionNo.CalculationExpression = Nothing
        Me.txtRevisionNo.DecimalPlaces = 0
        Me.txtRevisionNo.FieldCode = Nothing
        Me.txtRevisionNo.FieldDesc = Nothing
        Me.txtRevisionNo.FieldMaxLength = 0
        Me.txtRevisionNo.FieldName = Nothing
        Me.txtRevisionNo.isCalculatedField = False
        Me.txtRevisionNo.IsSourceFromTable = False
        Me.txtRevisionNo.IsSourceFromValueList = False
        Me.txtRevisionNo.IsUnique = False
        Me.txtRevisionNo.Location = New System.Drawing.Point(428, 9)
        Me.txtRevisionNo.MendatroryField = True
        Me.txtRevisionNo.MyLinkLable1 = Me.lblRevisionNo
        Me.txtRevisionNo.MyLinkLable2 = Nothing
        Me.txtRevisionNo.Name = "txtRevisionNo"
        Me.txtRevisionNo.ReadOnly = True
        Me.txtRevisionNo.ReferenceFieldDesc = Nothing
        Me.txtRevisionNo.ReferenceFieldName = Nothing
        Me.txtRevisionNo.ReferenceTableName = Nothing
        Me.txtRevisionNo.Size = New System.Drawing.Size(154, 20)
        Me.txtRevisionNo.TabIndex = 114
        Me.txtRevisionNo.Text = "0"
        Me.txtRevisionNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRevisionNo.Value = 0R
        '
        'dtpApplicableFrom
        '
        Me.dtpApplicableFrom.CalculationExpression = Nothing
        Me.dtpApplicableFrom.CustomFormat = "dd/MM/yyyy"
        Me.dtpApplicableFrom.FieldCode = Nothing
        Me.dtpApplicableFrom.FieldDesc = Nothing
        Me.dtpApplicableFrom.FieldMaxLength = 0
        Me.dtpApplicableFrom.FieldName = Nothing
        Me.dtpApplicableFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpApplicableFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpApplicableFrom.isCalculatedField = False
        Me.dtpApplicableFrom.IsSourceFromTable = False
        Me.dtpApplicableFrom.IsSourceFromValueList = False
        Me.dtpApplicableFrom.IsUnique = False
        Me.dtpApplicableFrom.Location = New System.Drawing.Point(108, 57)
        Me.dtpApplicableFrom.MendatroryField = True
        Me.dtpApplicableFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpApplicableFrom.MyLinkLable1 = Me.lblApplicableFrom
        Me.dtpApplicableFrom.MyLinkLable2 = Nothing
        Me.dtpApplicableFrom.Name = "dtpApplicableFrom"
        Me.dtpApplicableFrom.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpApplicableFrom.ReferenceFieldDesc = Nothing
        Me.dtpApplicableFrom.ReferenceFieldName = Nothing
        Me.dtpApplicableFrom.ReferenceTableName = Nothing
        Me.dtpApplicableFrom.Size = New System.Drawing.Size(147, 18)
        Me.dtpApplicableFrom.TabIndex = 1
        Me.dtpApplicableFrom.TabStop = False
        Me.dtpApplicableFrom.Text = "06/07/2013"
        Me.dtpApplicableFrom.Value = New Date(2013, 7, 6, 0, 0, 0, 0)
        '
        'lblApplicableFrom
        '
        Me.lblApplicableFrom.FieldName = Nothing
        Me.lblApplicableFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApplicableFrom.Location = New System.Drawing.Point(6, 58)
        Me.lblApplicableFrom.Name = "lblApplicableFrom"
        Me.lblApplicableFrom.Size = New System.Drawing.Size(88, 16)
        Me.lblApplicableFrom.TabIndex = 57
        Me.lblApplicableFrom.Text = "Applicable From"
        '
        'findBonus
        '
        Me.findBonus.CalculationExpression = Nothing
        Me.findBonus.FieldCode = Nothing
        Me.findBonus.FieldDesc = Nothing
        Me.findBonus.FieldMaxLength = 0
        Me.findBonus.FieldName = Nothing
        Me.findBonus.isCalculatedField = False
        Me.findBonus.IsSourceFromTable = False
        Me.findBonus.IsSourceFromValueList = False
        Me.findBonus.IsUnique = False
        Me.findBonus.Location = New System.Drawing.Point(108, 418)
        Me.findBonus.MendatroryField = True
        Me.findBonus.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findBonus.MyLinkLable1 = Me.lblBonusCode
        Me.findBonus.MyLinkLable2 = Nothing
        Me.findBonus.MyReadOnly = False
        Me.findBonus.MyShowMasterFormButton = False
        Me.findBonus.Name = "findBonus"
        Me.findBonus.ReferenceFieldDesc = Nothing
        Me.findBonus.ReferenceFieldName = Nothing
        Me.findBonus.ReferenceTableName = Nothing
        Me.findBonus.Size = New System.Drawing.Size(243, 19)
        Me.findBonus.TabIndex = 20
        Me.findBonus.Value = ""
        '
        'lblBonusCode
        '
        Me.lblBonusCode.FieldName = Nothing
        Me.lblBonusCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBonusCode.Location = New System.Drawing.Point(6, 419)
        Me.lblBonusCode.Name = "lblBonusCode"
        Me.lblBonusCode.Size = New System.Drawing.Size(69, 16)
        Me.lblBonusCode.TabIndex = 109
        Me.lblBonusCode.Text = "Bonus Code"
        '
        'lbldesignation
        '
        Me.lbldesignation.FieldName = Nothing
        Me.lbldesignation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldesignation.Location = New System.Drawing.Point(6, 102)
        Me.lbldesignation.Name = "lbldesignation"
        Me.lbldesignation.Size = New System.Drawing.Size(66, 16)
        Me.lbldesignation.TabIndex = 72
        Me.lbldesignation.Text = "Designation"
        '
        'chkBonusApplicable
        '
        Me.chkBonusApplicable.Location = New System.Drawing.Point(727, 124)
        Me.chkBonusApplicable.Name = "chkBonusApplicable"
        '
        '
        '
        Me.chkBonusApplicable.RootElement.StretchHorizontally = True
        Me.chkBonusApplicable.RootElement.StretchVertically = True
        Me.chkBonusApplicable.Size = New System.Drawing.Size(106, 18)
        Me.chkBonusApplicable.TabIndex = 10
        Me.chkBonusApplicable.Text = "Bonus Applicable"
        '
        'finddesignation
        '
        Me.finddesignation.CalculationExpression = Nothing
        Me.finddesignation.FieldCode = Nothing
        Me.finddesignation.FieldDesc = Nothing
        Me.finddesignation.FieldMaxLength = 0
        Me.finddesignation.FieldName = Nothing
        Me.finddesignation.isCalculatedField = False
        Me.finddesignation.IsSourceFromTable = False
        Me.finddesignation.IsSourceFromValueList = False
        Me.finddesignation.IsUnique = False
        Me.finddesignation.Location = New System.Drawing.Point(108, 101)
        Me.finddesignation.MendatroryField = True
        Me.finddesignation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.finddesignation.MyLinkLable1 = Me.lbldesignation
        Me.finddesignation.MyLinkLable2 = Nothing
        Me.finddesignation.MyReadOnly = False
        Me.finddesignation.MyShowMasterFormButton = False
        Me.finddesignation.Name = "finddesignation"
        Me.finddesignation.ReferenceFieldDesc = Nothing
        Me.finddesignation.ReferenceFieldName = Nothing
        Me.finddesignation.ReferenceTableName = Nothing
        Me.finddesignation.Size = New System.Drawing.Size(243, 19)
        Me.finddesignation.TabIndex = 2
        Me.finddesignation.Value = ""
        '
        'findOT
        '
        Me.findOT.CalculationExpression = Nothing
        Me.findOT.FieldCode = Nothing
        Me.findOT.FieldDesc = Nothing
        Me.findOT.FieldMaxLength = 0
        Me.findOT.FieldName = Nothing
        Me.findOT.isCalculatedField = False
        Me.findOT.IsSourceFromTable = False
        Me.findOT.IsSourceFromValueList = False
        Me.findOT.IsUnique = False
        Me.findOT.Location = New System.Drawing.Point(108, 395)
        Me.findOT.MendatroryField = True
        Me.findOT.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findOT.MyLinkLable1 = Me.lblOTCode
        Me.findOT.MyLinkLable2 = Nothing
        Me.findOT.MyReadOnly = False
        Me.findOT.MyShowMasterFormButton = False
        Me.findOT.Name = "findOT"
        Me.findOT.ReferenceFieldDesc = Nothing
        Me.findOT.ReferenceFieldName = Nothing
        Me.findOT.ReferenceTableName = Nothing
        Me.findOT.Size = New System.Drawing.Size(243, 19)
        Me.findOT.TabIndex = 19
        Me.findOT.Value = ""
        '
        'lblOTCode
        '
        Me.lblOTCode.FieldName = Nothing
        Me.lblOTCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOTCode.Location = New System.Drawing.Point(6, 396)
        Me.lblOTCode.Name = "lblOTCode"
        Me.lblOTCode.Size = New System.Drawing.Size(52, 16)
        Me.lblOTCode.TabIndex = 106
        Me.lblOTCode.Text = "OT Code"
        '
        'lblDepartment
        '
        Me.lblDepartment.FieldName = Nothing
        Me.lblDepartment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepartment.Location = New System.Drawing.Point(6, 125)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(65, 16)
        Me.lblDepartment.TabIndex = 74
        Me.lblDepartment.Text = "Department"
        '
        'findDepartment
        '
        Me.findDepartment.CalculationExpression = Nothing
        Me.findDepartment.FieldCode = Nothing
        Me.findDepartment.FieldDesc = Nothing
        Me.findDepartment.FieldMaxLength = 0
        Me.findDepartment.FieldName = Nothing
        Me.findDepartment.isCalculatedField = False
        Me.findDepartment.IsSourceFromTable = False
        Me.findDepartment.IsSourceFromValueList = False
        Me.findDepartment.IsUnique = False
        Me.findDepartment.Location = New System.Drawing.Point(108, 124)
        Me.findDepartment.MendatroryField = True
        Me.findDepartment.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findDepartment.MyLinkLable1 = Me.lblDepartment
        Me.findDepartment.MyLinkLable2 = Nothing
        Me.findDepartment.MyReadOnly = False
        Me.findDepartment.MyShowMasterFormButton = False
        Me.findDepartment.Name = "findDepartment"
        Me.findDepartment.ReferenceFieldDesc = Nothing
        Me.findDepartment.ReferenceFieldName = Nothing
        Me.findDepartment.ReferenceTableName = Nothing
        Me.findDepartment.Size = New System.Drawing.Size(243, 19)
        Me.findDepartment.TabIndex = 3
        Me.findDepartment.Value = ""
        '
        'chkOtApplicable
        '
        Me.chkOtApplicable.Location = New System.Drawing.Point(727, 101)
        Me.chkOtApplicable.Name = "chkOtApplicable"
        '
        '
        '
        Me.chkOtApplicable.RootElement.StretchHorizontally = True
        Me.chkOtApplicable.RootElement.StretchVertically = True
        Me.chkOtApplicable.Size = New System.Drawing.Size(90, 18)
        Me.chkOtApplicable.TabIndex = 9
        Me.chkOtApplicable.Text = "OT Applicable"
        '
        'lblDevision
        '
        Me.lblDevision.FieldName = Nothing
        Me.lblDevision.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDevision.Location = New System.Drawing.Point(6, 238)
        Me.lblDevision.Name = "lblDevision"
        Me.lblDevision.Size = New System.Drawing.Size(50, 16)
        Me.lblDevision.TabIndex = 76
        Me.lblDevision.Text = "Devision"
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
        Me.txtEsiNo.Location = New System.Drawing.Point(108, 373)
        Me.txtEsiNo.MaxLength = 49
        Me.txtEsiNo.MendatroryField = True
        Me.txtEsiNo.MyLinkLable1 = Me.lblESINo
        Me.txtEsiNo.MyLinkLable2 = Nothing
        Me.txtEsiNo.Name = "txtEsiNo"
        Me.txtEsiNo.ReferenceFieldDesc = Nothing
        Me.txtEsiNo.ReferenceFieldName = Nothing
        Me.txtEsiNo.ReferenceTableName = Nothing
        Me.txtEsiNo.Size = New System.Drawing.Size(243, 18)
        Me.txtEsiNo.TabIndex = 18
        '
        'findDevision
        '
        Me.findDevision.CalculationExpression = Nothing
        Me.findDevision.FieldCode = Nothing
        Me.findDevision.FieldDesc = Nothing
        Me.findDevision.FieldMaxLength = 0
        Me.findDevision.FieldName = Nothing
        Me.findDevision.isCalculatedField = False
        Me.findDevision.IsSourceFromTable = False
        Me.findDevision.IsSourceFromValueList = False
        Me.findDevision.IsUnique = False
        Me.findDevision.Location = New System.Drawing.Point(108, 237)
        Me.findDevision.MendatroryField = False
        Me.findDevision.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findDevision.MyLinkLable1 = Me.lblDevision
        Me.findDevision.MyLinkLable2 = Nothing
        Me.findDevision.MyReadOnly = False
        Me.findDevision.MyShowMasterFormButton = False
        Me.findDevision.Name = "findDevision"
        Me.findDevision.ReferenceFieldDesc = Nothing
        Me.findDevision.ReferenceFieldName = Nothing
        Me.findDevision.ReferenceTableName = Nothing
        Me.findDevision.Size = New System.Drawing.Size(243, 19)
        Me.findDevision.TabIndex = 12
        Me.findDevision.Value = ""
        '
        'chkEsiApplicable
        '
        Me.chkEsiApplicable.Location = New System.Drawing.Point(355, 79)
        Me.chkEsiApplicable.Name = "chkEsiApplicable"
        '
        '
        '
        Me.chkEsiApplicable.RootElement.StretchHorizontally = True
        Me.chkEsiApplicable.RootElement.StretchVertically = True
        Me.chkEsiApplicable.Size = New System.Drawing.Size(90, 18)
        Me.chkEsiApplicable.TabIndex = 8
        Me.chkEsiApplicable.Text = "ESI Applicable"
        '
        'findGrade
        '
        Me.findGrade.CalculationExpression = Nothing
        Me.findGrade.FieldCode = Nothing
        Me.findGrade.FieldDesc = Nothing
        Me.findGrade.FieldMaxLength = 0
        Me.findGrade.FieldName = Nothing
        Me.findGrade.isCalculatedField = False
        Me.findGrade.IsSourceFromTable = False
        Me.findGrade.IsSourceFromValueList = False
        Me.findGrade.IsUnique = False
        Me.findGrade.Location = New System.Drawing.Point(108, 260)
        Me.findGrade.MendatroryField = False
        Me.findGrade.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findGrade.MyLinkLable1 = Me.lblGrade
        Me.findGrade.MyLinkLable2 = Nothing
        Me.findGrade.MyReadOnly = False
        Me.findGrade.MyShowMasterFormButton = False
        Me.findGrade.Name = "findGrade"
        Me.findGrade.ReferenceFieldDesc = Nothing
        Me.findGrade.ReferenceFieldName = Nothing
        Me.findGrade.ReferenceTableName = Nothing
        Me.findGrade.Size = New System.Drawing.Size(243, 19)
        Me.findGrade.TabIndex = 13
        Me.findGrade.Value = ""
        '
        'txtPfNo
        '
        Me.txtPfNo.CalculationExpression = Nothing
        Me.txtPfNo.FieldCode = Nothing
        Me.txtPfNo.FieldDesc = Nothing
        Me.txtPfNo.FieldMaxLength = 0
        Me.txtPfNo.FieldName = Nothing
        Me.txtPfNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPfNo.isCalculatedField = False
        Me.txtPfNo.IsSourceFromTable = False
        Me.txtPfNo.IsSourceFromValueList = False
        Me.txtPfNo.IsUnique = False
        Me.txtPfNo.Location = New System.Drawing.Point(108, 351)
        Me.txtPfNo.MaxLength = 49
        Me.txtPfNo.MendatroryField = True
        Me.txtPfNo.MyLinkLable1 = Me.lblPFNo
        Me.txtPfNo.MyLinkLable2 = Nothing
        Me.txtPfNo.Name = "txtPfNo"
        Me.txtPfNo.ReferenceFieldDesc = Nothing
        Me.txtPfNo.ReferenceFieldName = Nothing
        Me.txtPfNo.ReferenceTableName = Nothing
        Me.txtPfNo.Size = New System.Drawing.Size(243, 18)
        Me.txtPfNo.TabIndex = 17
        '
        'chkPFApplicable
        '
        Me.chkPFApplicable.Location = New System.Drawing.Point(355, 57)
        Me.chkPFApplicable.Name = "chkPFApplicable"
        '
        '
        '
        Me.chkPFApplicable.RootElement.StretchHorizontally = True
        Me.chkPFApplicable.RootElement.StretchVertically = True
        Me.chkPFApplicable.Size = New System.Drawing.Size(88, 18)
        Me.chkPFApplicable.TabIndex = 7
        Me.chkPFApplicable.Text = "PF Applicable"
        '
        'findBranch
        '
        Me.findBranch.CalculationExpression = Nothing
        Me.findBranch.FieldCode = Nothing
        Me.findBranch.FieldDesc = Nothing
        Me.findBranch.FieldMaxLength = 0
        Me.findBranch.FieldName = Nothing
        Me.findBranch.isCalculatedField = False
        Me.findBranch.IsSourceFromTable = False
        Me.findBranch.IsSourceFromValueList = False
        Me.findBranch.IsUnique = False
        Me.findBranch.Location = New System.Drawing.Point(108, 214)
        Me.findBranch.MendatroryField = False
        Me.findBranch.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findBranch.MyLinkLable1 = Me.lblBranch
        Me.findBranch.MyLinkLable2 = Nothing
        Me.findBranch.MyReadOnly = False
        Me.findBranch.MyShowMasterFormButton = False
        Me.findBranch.Name = "findBranch"
        Me.findBranch.ReferenceFieldDesc = Nothing
        Me.findBranch.ReferenceFieldName = Nothing
        Me.findBranch.ReferenceTableName = Nothing
        Me.findBranch.Size = New System.Drawing.Size(243, 19)
        Me.findBranch.TabIndex = 11
        Me.findBranch.Value = ""
        '
        'findBank
        '
        Me.findBank.CalculationExpression = Nothing
        Me.findBank.FieldCode = Nothing
        Me.findBank.FieldDesc = Nothing
        Me.findBank.FieldMaxLength = 0
        Me.findBank.FieldName = Nothing
        Me.findBank.isCalculatedField = False
        Me.findBank.IsSourceFromTable = False
        Me.findBank.IsSourceFromValueList = False
        Me.findBank.IsUnique = False
        Me.findBank.Location = New System.Drawing.Point(108, 328)
        Me.findBank.MendatroryField = False
        Me.findBank.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findBank.MyLinkLable1 = Me.lblBank
        Me.findBank.MyLinkLable2 = Nothing
        Me.findBank.MyReadOnly = False
        Me.findBank.MyShowMasterFormButton = False
        Me.findBank.Name = "findBank"
        Me.findBank.ReferenceFieldDesc = Nothing
        Me.findBank.ReferenceFieldName = Nothing
        Me.findBank.ReferenceTableName = Nothing
        Me.findBank.Size = New System.Drawing.Size(243, 19)
        Me.findBank.TabIndex = 16
        Me.findBank.Value = ""
        '
        'lblReportingPerson
        '
        Me.lblReportingPerson.FieldName = Nothing
        Me.lblReportingPerson.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReportingPerson.Location = New System.Drawing.Point(6, 148)
        Me.lblReportingPerson.Name = "lblReportingPerson"
        Me.lblReportingPerson.Size = New System.Drawing.Size(94, 16)
        Me.lblReportingPerson.TabIndex = 82
        Me.lblReportingPerson.Text = "Reporting Person"
        '
        'cboPaymentMode
        '
        Me.cboPaymentMode.AutoCompleteDisplayMember = Nothing
        Me.cboPaymentMode.AutoCompleteValueMember = Nothing
        Me.cboPaymentMode.CalculationExpression = Nothing
        Me.cboPaymentMode.DropDownAnimationEnabled = True
        Me.cboPaymentMode.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboPaymentMode.FieldCode = Nothing
        Me.cboPaymentMode.FieldDesc = Nothing
        Me.cboPaymentMode.FieldMaxLength = 0
        Me.cboPaymentMode.FieldName = Nothing
        Me.cboPaymentMode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPaymentMode.isCalculatedField = False
        Me.cboPaymentMode.IsSourceFromTable = False
        Me.cboPaymentMode.IsSourceFromValueList = False
        Me.cboPaymentMode.IsUnique = False
        RadListDataItem7.Text = "Cheque"
        RadListDataItem8.Text = "Cash"
        RadListDataItem9.Text = "DD"
        RadListDataItem10.Text = "Internet Transfer"
        RadListDataItem11.Text = "Others"
        Me.cboPaymentMode.Items.Add(RadListDataItem7)
        Me.cboPaymentMode.Items.Add(RadListDataItem8)
        Me.cboPaymentMode.Items.Add(RadListDataItem9)
        Me.cboPaymentMode.Items.Add(RadListDataItem10)
        Me.cboPaymentMode.Items.Add(RadListDataItem11)
        Me.cboPaymentMode.Location = New System.Drawing.Point(108, 192)
        Me.cboPaymentMode.MendatroryField = False
        Me.cboPaymentMode.MyLinkLable1 = Me.lblPaymentMode
        Me.cboPaymentMode.MyLinkLable2 = Nothing
        Me.cboPaymentMode.Name = "cboPaymentMode"
        Me.cboPaymentMode.ReferenceFieldDesc = Nothing
        Me.cboPaymentMode.ReferenceFieldName = Nothing
        Me.cboPaymentMode.ReferenceTableName = Nothing
        '
        '
        '
        Me.cboPaymentMode.RootElement.StretchVertically = True
        Me.cboPaymentMode.Size = New System.Drawing.Size(243, 18)
        Me.cboPaymentMode.TabIndex = 6
        '
        'lblPaymentMode
        '
        Me.lblPaymentMode.FieldName = Nothing
        Me.lblPaymentMode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymentMode.Location = New System.Drawing.Point(6, 193)
        Me.lblPaymentMode.Name = "lblPaymentMode"
        Me.lblPaymentMode.Size = New System.Drawing.Size(82, 16)
        Me.lblPaymentMode.TabIndex = 95
        Me.lblPaymentMode.Text = "Payment Mode"
        '
        'findEmployee
        '
        Me.findEmployee.CalculationExpression = Nothing
        Me.findEmployee.FieldCode = Nothing
        Me.findEmployee.FieldDesc = Nothing
        Me.findEmployee.FieldMaxLength = 0
        Me.findEmployee.FieldName = Nothing
        Me.findEmployee.isCalculatedField = False
        Me.findEmployee.IsSourceFromTable = False
        Me.findEmployee.IsSourceFromValueList = False
        Me.findEmployee.IsUnique = False
        Me.findEmployee.Location = New System.Drawing.Point(108, 147)
        Me.findEmployee.MendatroryField = False
        Me.findEmployee.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findEmployee.MyLinkLable1 = Me.lblReportingPerson
        Me.findEmployee.MyLinkLable2 = Nothing
        Me.findEmployee.MyReadOnly = False
        Me.findEmployee.MyShowMasterFormButton = False
        Me.findEmployee.Name = "findEmployee"
        Me.findEmployee.ReferenceFieldDesc = Nothing
        Me.findEmployee.ReferenceFieldName = Nothing
        Me.findEmployee.ReferenceTableName = Nothing
        Me.findEmployee.Size = New System.Drawing.Size(243, 19)
        Me.findEmployee.TabIndex = 4
        Me.findEmployee.Value = ""
        '
        'lblAttendance
        '
        Me.lblAttendance.FieldName = Nothing
        Me.lblAttendance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAttendance.Location = New System.Drawing.Point(6, 284)
        Me.lblAttendance.Name = "lblAttendance"
        Me.lblAttendance.Size = New System.Drawing.Size(63, 16)
        Me.lblAttendance.TabIndex = 84
        Me.lblAttendance.Text = "Attendance"
        '
        'txtNameinAccount
        '
        Me.txtNameinAccount.CalculationExpression = Nothing
        Me.txtNameinAccount.FieldCode = Nothing
        Me.txtNameinAccount.FieldDesc = Nothing
        Me.txtNameinAccount.FieldMaxLength = 0
        Me.txtNameinAccount.FieldName = Nothing
        Me.txtNameinAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNameinAccount.isCalculatedField = False
        Me.txtNameinAccount.IsSourceFromTable = False
        Me.txtNameinAccount.IsSourceFromValueList = False
        Me.txtNameinAccount.IsUnique = False
        Me.txtNameinAccount.Location = New System.Drawing.Point(108, 306)
        Me.txtNameinAccount.MaxLength = 49
        Me.txtNameinAccount.MendatroryField = False
        Me.txtNameinAccount.MyLinkLable1 = Me.lblNameinAcc
        Me.txtNameinAccount.MyLinkLable2 = Nothing
        Me.txtNameinAccount.Name = "txtNameinAccount"
        Me.txtNameinAccount.ReferenceFieldDesc = Nothing
        Me.txtNameinAccount.ReferenceFieldName = Nothing
        Me.txtNameinAccount.ReferenceTableName = Nothing
        Me.txtNameinAccount.Size = New System.Drawing.Size(243, 18)
        Me.txtNameinAccount.TabIndex = 15
        '
        'findAttendance
        '
        Me.findAttendance.CalculationExpression = Nothing
        Me.findAttendance.FieldCode = Nothing
        Me.findAttendance.FieldDesc = Nothing
        Me.findAttendance.FieldMaxLength = 0
        Me.findAttendance.FieldName = Nothing
        Me.findAttendance.isCalculatedField = False
        Me.findAttendance.IsSourceFromTable = False
        Me.findAttendance.IsSourceFromValueList = False
        Me.findAttendance.IsUnique = False
        Me.findAttendance.Location = New System.Drawing.Point(108, 283)
        Me.findAttendance.MendatroryField = True
        Me.findAttendance.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findAttendance.MyLinkLable1 = Me.lblAttendance
        Me.findAttendance.MyLinkLable2 = Nothing
        Me.findAttendance.MyReadOnly = False
        Me.findAttendance.MyShowMasterFormButton = False
        Me.findAttendance.Name = "findAttendance"
        Me.findAttendance.ReferenceFieldDesc = Nothing
        Me.findAttendance.ReferenceFieldName = Nothing
        Me.findAttendance.ReferenceTableName = Nothing
        Me.findAttendance.Size = New System.Drawing.Size(243, 19)
        Me.findAttendance.TabIndex = 14
        Me.findAttendance.Value = ""
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
        Me.txtaccno.Location = New System.Drawing.Point(108, 170)
        Me.txtaccno.MaxLength = 49
        Me.txtaccno.MendatroryField = False
        Me.txtaccno.MyLinkLable1 = Me.lblBankAccNo
        Me.txtaccno.MyLinkLable2 = Nothing
        Me.txtaccno.Name = "txtaccno"
        Me.txtaccno.ReferenceFieldDesc = Nothing
        Me.txtaccno.ReferenceFieldName = Nothing
        Me.txtaccno.ReferenceTableName = Nothing
        Me.txtaccno.Size = New System.Drawing.Size(243, 18)
        Me.txtaccno.TabIndex = 5
        '
        'lblBankAccNo
        '
        Me.lblBankAccNo.FieldName = Nothing
        Me.lblBankAccNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankAccNo.Location = New System.Drawing.Point(6, 171)
        Me.lblBankAccNo.Name = "lblBankAccNo"
        Me.lblBankAccNo.Size = New System.Drawing.Size(72, 16)
        Me.lblBankAccNo.TabIndex = 91
        Me.lblBankAccNo.Text = "Bank Acc No"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(13, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 22)
        Me.btnsave.TabIndex = 131
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(85, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 22)
        Me.btndelete.TabIndex = 132
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(839, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 22)
        Me.btnclose.TabIndex = 133
        Me.btnclose.Text = "Close"
        '
        'MyLabel43
        '
        Me.MyLabel43.FieldName = Nothing
        Me.MyLabel43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel43.Location = New System.Drawing.Point(586, 192)
        Me.MyLabel43.Name = "MyLabel43"
        Me.MyLabel43.Size = New System.Drawing.Size(47, 16)
        Me.MyLabel43.TabIndex = 237
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
        Me.txtGPFNo.Location = New System.Drawing.Point(669, 191)
        Me.txtGPFNo.MaxLength = 49
        Me.txtGPFNo.MendatroryField = False
        Me.txtGPFNo.MyLinkLable1 = Nothing
        Me.txtGPFNo.MyLinkLable2 = Nothing
        Me.txtGPFNo.Name = "txtGPFNo"
        Me.txtGPFNo.ReferenceFieldDesc = Nothing
        Me.txtGPFNo.ReferenceFieldName = Nothing
        Me.txtGPFNo.ReferenceTableName = Nothing
        Me.txtGPFNo.Size = New System.Drawing.Size(228, 18)
        Me.txtGPFNo.TabIndex = 236
        '
        'frmEmployee_Status
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(913, 506)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu2)
        Me.Name = "frmEmployee_Status"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Employee Status"
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtTransferPF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTransPF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkProfessionalTaxApplicable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRevisionNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPFCalculatnType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEPFMaxLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMaxLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEPFRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtESIRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEPFRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtESIMaxLim, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        CType(Me.gvLoanGeneration.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvLoanGeneration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkODApplicable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboConveyanceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel87, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboShiftChangeType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel51, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBonusName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEPStoEPF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOTName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboWorkingStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWorkingStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAttendanceName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGradeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBranch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBranchName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDevisionName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReportingPersonName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNameinAcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartmentName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblESINo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDesignationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPFNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGrade, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRevisionNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRevisionNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpApplicableFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApplicableFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBonusCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldesignation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBonusApplicable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOTCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOtApplicable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDevision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEsiNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEsiApplicable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPfNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPFApplicable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReportingPerson, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPaymentMode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPaymentMode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAttendance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNameinAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtaccno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankAccNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGPFNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblBonusName As common.Controls.MyLabel
    Friend WithEvents chkEPStoEPF As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblOTName As common.Controls.MyLabel
    Friend WithEvents cboWorkingStatus As common.Controls.MyComboBox
    Friend WithEvents lblWorkingStatus As common.Controls.MyLabel
    Friend WithEvents lblBankName As common.Controls.MyLabel
    Friend WithEvents lblAttendanceName As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblGradeName As common.Controls.MyLabel
    Friend WithEvents lblBranch As common.Controls.MyLabel
    Friend WithEvents lblBranchName As common.Controls.MyLabel
    Friend WithEvents lblDevisionName As common.Controls.MyLabel
    Friend WithEvents lblReportingPersonName As common.Controls.MyLabel
    Friend WithEvents lblNameinAcc As common.Controls.MyLabel
    Friend WithEvents lblDepartmentName As common.Controls.MyLabel
    Friend WithEvents lblESINo As common.Controls.MyLabel
    Friend WithEvents lblDesignationName As common.Controls.MyLabel
    Friend WithEvents lblPFNo As common.Controls.MyLabel
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents lblBank As common.Controls.MyLabel
    Friend WithEvents txtEmpCode As common.UserControls.txtFinder
    Friend WithEvents lblEmpCode As common.Controls.MyLabel
    Friend WithEvents lblGrade As common.Controls.MyLabel
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents lblRevisionNo As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents txtRevisionNo As common.MyNumBox
    Friend WithEvents dtpApplicableFrom As common.Controls.MyDateTimePicker
    Friend WithEvents lblApplicableFrom As common.Controls.MyLabel
    Friend WithEvents findBonus As common.UserControls.txtFinder
    Friend WithEvents lblBonusCode As common.Controls.MyLabel
    Friend WithEvents lbldesignation As common.Controls.MyLabel
    Friend WithEvents chkBonusApplicable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents finddesignation As common.UserControls.txtFinder
    Friend WithEvents findOT As common.UserControls.txtFinder
    Friend WithEvents lblOTCode As common.Controls.MyLabel
    Friend WithEvents lblDepartment As common.Controls.MyLabel
    Friend WithEvents findDepartment As common.UserControls.txtFinder
    Friend WithEvents chkOtApplicable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblDevision As common.Controls.MyLabel
    Friend WithEvents txtEsiNo As common.Controls.MyTextBox
    Friend WithEvents findDevision As common.UserControls.txtFinder
    Friend WithEvents chkEsiApplicable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents findGrade As common.UserControls.txtFinder
    Friend WithEvents txtPfNo As common.Controls.MyTextBox
    Friend WithEvents chkPFApplicable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents findBranch As common.UserControls.txtFinder
    Friend WithEvents findBank As common.UserControls.txtFinder
    Friend WithEvents lblReportingPerson As common.Controls.MyLabel
    Friend WithEvents cboPaymentMode As common.Controls.MyComboBox
    Friend WithEvents lblPaymentMode As common.Controls.MyLabel
    Friend WithEvents findEmployee As common.UserControls.txtFinder
    Friend WithEvents lblAttendance As common.Controls.MyLabel
    Friend WithEvents txtNameinAccount As common.Controls.MyTextBox
    Friend WithEvents findAttendance As common.UserControls.txtFinder
    Friend WithEvents txtaccno As common.Controls.MyTextBox
    Friend WithEvents lblBankAccNo As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents fndShift As common.UserControls.txtFinder
    Friend WithEvents MyLabel51 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents cboShiftChangeType As common.Controls.MyComboBox
    Friend WithEvents fndConveyanceRate As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel87 As common.Controls.MyLabel
    Friend WithEvents cboConveyanceType As common.Controls.MyComboBox
    Friend WithEvents chkODApplicable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents gvLoanGeneration As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtESIMaxLim As common.MyNumBox
    Friend WithEvents lblMaxLimit As common.Controls.MyLabel
    Friend WithEvents txtEPFMaxLimit As common.MyNumBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblEPFRate As common.Controls.MyLabel
    Friend WithEvents txtESIRate As common.MyNumBox
    Friend WithEvents txtEPFRate As common.MyNumBox
    Friend WithEvents cboPFCalculatnType As common.Controls.MyComboBox
    Friend WithEvents chkRevisionNo As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkProfessionalTaxApplicable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtTransferPF As common.Controls.MyTextBox
    Friend WithEvents chkTransPF As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel43 As common.Controls.MyLabel
    Friend WithEvents txtGPFNo As common.Controls.MyTextBox
End Class
