Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTrainerMaster
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
        Me.components = New System.ComponentModel.Container
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.vendorgrpbox = New Telerik.WinControls.UI.RadGroupBox
        Me.UsLock1 = New common.usLock
        Me.chkIsApplicable = New Telerik.WinControls.UI.RadCheckBox
        Me.CmbType = New common.Controls.MyComboBox
        Me.lblBaseCurrency = New common.Controls.MyLabel
        Me.fndvendorNo = New common.UserControls.txtNavigator
        Me.lblvandorno = New common.Controls.MyLabel
        Me.lblvendorname = New common.Controls.MyLabel
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.txtName = New common.Controls.MyTextBox
        Me.pageCus = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.txtLastName = New common.Controls.MyTextBox
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.TxtFirstName = New common.Controls.MyTextBox
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.TxtPinCode = New common.Controls.MyTextBox
        Me.TxtRemark = New common.Controls.MyTextBox
        Me.LblEmployeeName = New common.Controls.MyLabel
        Me.LblInstituteName = New common.Controls.MyLabel
        Me.CmbPaymentType = New common.Controls.MyComboBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.lblDocDate = New common.Controls.MyLabel
        Me.dtpDoB = New common.Controls.MyDateTimePicker
        Me.CmbGender = New common.Controls.MyComboBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.FndEployeeCode = New common.UserControls.txtFinder
        Me.LblEmp = New common.Controls.MyLabel
        Me.fndInstitutecode = New common.UserControls.txtFinder
        Me.lblCusGrp = New common.Controls.MyLabel
        Me.MyLabel16 = New common.Controls.MyLabel
        Me.RadLabel13 = New common.Controls.MyLabel
        Me.txtCity = New common.Controls.MyLabel
        Me.txtcountrycode = New common.UserControls.txtFinder
        Me.RadLabel7 = New common.Controls.MyLabel
        Me.txtCountry = New common.Controls.MyLabel
        Me.txtState = New common.Controls.MyLabel
        Me.txtstatecode = New common.UserControls.txtFinder
        Me.RadLabel6 = New common.Controls.MyLabel
        Me.fndCity = New common.UserControls.txtFinder
        Me.RadLabel5 = New common.Controls.MyLabel
        Me.RadLabel11 = New common.Controls.MyLabel
        Me.RadLabel10 = New common.Controls.MyLabel
        Me.RadLabel8 = New common.Controls.MyLabel
        Me.RadLabel9 = New common.Controls.MyLabel
        Me.txtfax = New common.Controls.MyTextBox
        Me.txtEmail = New common.Controls.MyTextBox
        Me.txtPhone2 = New common.Controls.MyTextBox
        Me.txtPhone1 = New common.Controls.MyTextBox
        Me.txtAdd2 = New common.Controls.MyTextBox
        Me.txtAdd1 = New common.Controls.MyTextBox
        Me.txtAdd3 = New common.Controls.MyTextBox
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.gvTrainingCourse = New common.MyCheckBoxGrid
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.gvQualification = New common.MyCheckBoxGrid
        Me.RadGroupBox9 = New Telerik.WinControls.UI.RadGroupBox
        Me.gvTrainingCities = New common.MyCheckBoxGrid
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment
        Me.btnclear = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.MenuClose = New Telerik.WinControls.UI.RadMenuItem
        Me.MenuImport = New Telerik.WinControls.UI.RadMenuItem
        Me.MenuExport = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem
        Me.ToolTipvendor = New System.Windows.Forms.ToolTip(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.BtnPost = New Telerik.WinControls.UI.RadButton
        Me.txtempext = New common.Controls.MyTextBox
        CType(Me.vendorgrpbox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.vendorgrpbox.SuspendLayout()
        CType(Me.chkIsApplicable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBaseCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvendorname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pageCus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageCus.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.txtLastName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFirstName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPinCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblEmployeeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblInstituteName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbPaymentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDoB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbGender, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblEmp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCusGrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPhone2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPhone1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox9.SuspendLayout()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.btnclear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.BtnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtempext, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'vendorgrpbox
        '
        Me.vendorgrpbox.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.vendorgrpbox.Controls.Add(Me.UsLock1)
        Me.vendorgrpbox.Controls.Add(Me.chkIsApplicable)
        Me.vendorgrpbox.Controls.Add(Me.CmbType)
        Me.vendorgrpbox.Controls.Add(Me.lblBaseCurrency)
        Me.vendorgrpbox.Controls.Add(Me.fndvendorNo)
        Me.vendorgrpbox.Controls.Add(Me.lblvendorname)
        Me.vendorgrpbox.Controls.Add(Me.lblvandorno)
        Me.vendorgrpbox.Controls.Add(Me.btnnew)
        Me.vendorgrpbox.Controls.Add(Me.txtName)
        Me.vendorgrpbox.Controls.Add(Me.pageCus)
        Me.vendorgrpbox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vendorgrpbox.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vendorgrpbox.HeaderText = ""
        Me.vendorgrpbox.Location = New System.Drawing.Point(0, 0)
        Me.vendorgrpbox.Name = "vendorgrpbox"
        Me.vendorgrpbox.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.vendorgrpbox.Size = New System.Drawing.Size(787, 582)
        Me.vendorgrpbox.TabIndex = 1
        Me.vendorgrpbox.ThemeName = "ControlDefault"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(670, 7)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 4
        '
        'chkIsApplicable
        '
        Me.chkIsApplicable.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsApplicable.Location = New System.Drawing.Point(392, 35)
        Me.chkIsApplicable.Name = "chkIsApplicable"
        Me.chkIsApplicable.Size = New System.Drawing.Size(84, 16)
        Me.chkIsApplicable.TabIndex = 3
        Me.chkIsApplicable.Text = "Is Applicable"
        '
        'CmbType
        '
        Me.CmbType.AllowShowFocusCues = False
        Me.CmbType.AutoCompleteDisplayMember = Nothing
        Me.CmbType.AutoCompleteValueMember = Nothing
        Me.CmbType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem6.Text = "Internal"
        RadListDataItem6.TextWrap = True
        RadListDataItem7.Text = "External"
        RadListDataItem7.TextWrap = True
        Me.CmbType.Items.Add(RadListDataItem6)
        Me.CmbType.Items.Add(RadListDataItem7)
        Me.CmbType.Location = New System.Drawing.Point(107, 32)
        Me.CmbType.MendatroryField = True
        Me.CmbType.MyLinkLable1 = Nothing
        Me.CmbType.MyLinkLable2 = Nothing
        Me.CmbType.Name = "CmbType"
        Me.CmbType.Size = New System.Drawing.Size(216, 20)
        Me.CmbType.TabIndex = 2
        '
        'lblBaseCurrency
        '
        Me.lblBaseCurrency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBaseCurrency.Location = New System.Drawing.Point(30, 32)
        Me.lblBaseCurrency.Name = "lblBaseCurrency"
        Me.lblBaseCurrency.Size = New System.Drawing.Size(31, 16)
        Me.lblBaseCurrency.TabIndex = 4
        Me.lblBaseCurrency.Text = "Type"
        '
        'fndvendorNo
        '
        Me.fndvendorNo.Location = New System.Drawing.Point(107, 6)
        Me.fndvendorNo.MendatroryField = True
        Me.fndvendorNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndvendorNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndvendorNo.MyLinkLable1 = Me.lblvandorno
        Me.fndvendorNo.MyLinkLable2 = Nothing
        Me.fndvendorNo.MyMaxLength = 32767
        Me.fndvendorNo.MyReadOnly = False
        Me.fndvendorNo.Name = "fndvendorNo"
        Me.fndvendorNo.Size = New System.Drawing.Size(202, 21)
        Me.fndvendorNo.TabIndex = 0
        Me.fndvendorNo.TabStop = False
        Me.fndvendorNo.Value = ""
        '
        'lblvandorno
        '
        Me.lblvandorno.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblvandorno.Location = New System.Drawing.Point(30, 7)
        Me.lblvandorno.Name = "lblvandorno"
        Me.lblvandorno.Size = New System.Drawing.Size(33, 16)
        Me.lblvandorno.TabIndex = 0
        Me.lblvandorno.Text = "Code"
        '
        'lblvendorname
        '
        Me.lblvendorname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvendorname.Location = New System.Drawing.Point(392, 11)
        Me.lblvendorname.Name = "lblvendorname"
        Me.lblvendorname.Size = New System.Drawing.Size(36, 16)
        Me.lblvendorname.TabIndex = 4
        Me.lblvendorname.Text = "Name"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(309, 7)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 2
        '
        'txtName
        '
        Me.txtName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.Location = New System.Drawing.Point(441, 9)
        Me.txtName.MaxLength = 50
        Me.txtName.MendatroryField = True
        Me.txtName.MyLinkLable1 = Me.lblvendorname
        Me.txtName.MyLinkLable2 = Nothing
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(224, 18)
        Me.txtName.TabIndex = 1
        '
        'pageCus
        '
        Me.pageCus.Controls.Add(Me.RadPageViewPage1)
        Me.pageCus.Controls.Add(Me.RadPageViewPage2)
        Me.pageCus.Controls.Add(Me.RadPageViewPage3)
        Me.pageCus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pageCus.Location = New System.Drawing.Point(9, 57)
        Me.pageCus.Name = "pageCus"
        Me.pageCus.SelectedPage = Me.RadPageViewPage1
        Me.pageCus.Size = New System.Drawing.Size(759, 521)
        Me.pageCus.TabIndex = 5
        CType(Me.pageCus.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtempext)
        Me.RadPageViewPage1.Controls.Add(Me.txtLastName)
        Me.RadPageViewPage1.Controls.Add(Me.TxtFirstName)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.TxtPinCode)
        Me.RadPageViewPage1.Controls.Add(Me.TxtRemark)
        Me.RadPageViewPage1.Controls.Add(Me.LblEmployeeName)
        Me.RadPageViewPage1.Controls.Add(Me.LblInstituteName)
        Me.RadPageViewPage1.Controls.Add(Me.CmbPaymentType)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.lblDocDate)
        Me.RadPageViewPage1.Controls.Add(Me.dtpDoB)
        Me.RadPageViewPage1.Controls.Add(Me.CmbGender)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.FndEployeeCode)
        Me.RadPageViewPage1.Controls.Add(Me.LblEmp)
        Me.RadPageViewPage1.Controls.Add(Me.fndInstitutecode)
        Me.RadPageViewPage1.Controls.Add(Me.lblCusGrp)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel16)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.txtCity)
        Me.RadPageViewPage1.Controls.Add(Me.txtcountrycode)
        Me.RadPageViewPage1.Controls.Add(Me.txtCountry)
        Me.RadPageViewPage1.Controls.Add(Me.txtState)
        Me.RadPageViewPage1.Controls.Add(Me.txtstatecode)
        Me.RadPageViewPage1.Controls.Add(Me.fndCity)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel11)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtfax)
        Me.RadPageViewPage1.Controls.Add(Me.txtEmail)
        Me.RadPageViewPage1.Controls.Add(Me.txtPhone2)
        Me.RadPageViewPage1.Controls.Add(Me.txtPhone1)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdd2)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdd1)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdd3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(51.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(738, 475)
        Me.RadPageViewPage1.Text = "Details"
        '
        'txtLastName
        '
        Me.txtLastName.Location = New System.Drawing.Point(422, 12)
        Me.txtLastName.MaxLength = 250
        Me.txtLastName.MendatroryField = True
        Me.txtLastName.MyLinkLable1 = Me.RadLabel2
        Me.txtLastName.MyLinkLable2 = Nothing
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(285, 20)
        Me.txtLastName.TabIndex = 1
        '
        'RadLabel2
        '
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(11, 146)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(48, 16)
        Me.RadLabel2.TabIndex = 23
        Me.RadLabel2.Text = "Address"
        '
        'TxtFirstName
        '
        Me.TxtFirstName.Location = New System.Drawing.Point(112, 10)
        Me.TxtFirstName.MaxLength = 250
        Me.TxtFirstName.MendatroryField = True
        Me.TxtFirstName.MyLinkLable1 = Me.RadLabel2
        Me.TxtFirstName.MyLinkLable2 = Nothing
        Me.TxtFirstName.Name = "TxtFirstName"
        Me.TxtFirstName.Size = New System.Drawing.Size(225, 20)
        Me.TxtFirstName.TabIndex = 0
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(11, 299)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(53, 16)
        Me.MyLabel4.TabIndex = 20
        Me.MyLabel4.Text = "Pin Code"
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(11, 370)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel3.TabIndex = 103
        Me.MyLabel3.Text = "Remark"
        '
        'TxtPinCode
        '
        Me.TxtPinCode.Location = New System.Drawing.Point(112, 297)
        Me.TxtPinCode.MaxLength = 20
        Me.TxtPinCode.MendatroryField = False
        Me.TxtPinCode.MyLinkLable1 = Me.MyLabel4
        Me.TxtPinCode.MyLinkLable2 = Nothing
        Me.TxtPinCode.Name = "TxtPinCode"
        Me.TxtPinCode.Size = New System.Drawing.Size(225, 20)
        Me.TxtPinCode.TabIndex = 13
        '
        'TxtRemark
        '
        Me.TxtRemark.AutoSize = False
        Me.TxtRemark.Location = New System.Drawing.Point(112, 370)
        Me.TxtRemark.MaxLength = 50
        Me.TxtRemark.MendatroryField = False
        Me.TxtRemark.Multiline = True
        Me.TxtRemark.MyLinkLable1 = Me.MyLabel3
        Me.TxtRemark.MyLinkLable2 = Nothing
        Me.TxtRemark.Name = "TxtRemark"
        Me.TxtRemark.Size = New System.Drawing.Size(595, 59)
        Me.TxtRemark.TabIndex = 23
        '
        'LblEmployeeName
        '
        Me.LblEmployeeName.AutoSize = False
        Me.LblEmployeeName.BorderVisible = True
        Me.LblEmployeeName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEmployeeName.Location = New System.Drawing.Point(251, 83)
        Me.LblEmployeeName.Name = "LblEmployeeName"
        Me.LblEmployeeName.Size = New System.Drawing.Size(456, 18)
        Me.LblEmployeeName.TabIndex = 7
        Me.LblEmployeeName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblEmployeeName.TextWrap = False
        '
        'LblInstituteName
        '
        Me.LblInstituteName.AutoSize = False
        Me.LblInstituteName.BorderVisible = True
        Me.LblInstituteName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInstituteName.Location = New System.Drawing.Point(251, 60)
        Me.LblInstituteName.Name = "LblInstituteName"
        Me.LblInstituteName.Size = New System.Drawing.Size(456, 18)
        Me.LblInstituteName.TabIndex = 5
        Me.LblInstituteName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblInstituteName.TextWrap = False
        '
        'CmbPaymentType
        '
        Me.CmbPaymentType.AllowShowFocusCues = False
        Me.CmbPaymentType.AutoCompleteDisplayMember = Nothing
        Me.CmbPaymentType.AutoCompleteValueMember = Nothing
        Me.CmbPaymentType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CmbPaymentType.Location = New System.Drawing.Point(112, 107)
        Me.CmbPaymentType.MendatroryField = True
        Me.CmbPaymentType.MyLinkLable1 = Nothing
        Me.CmbPaymentType.MyLinkLable2 = Nothing
        Me.CmbPaymentType.Name = "CmbPaymentType"
        Me.CmbPaymentType.Size = New System.Drawing.Size(226, 20)
        Me.CmbPaymentType.TabIndex = 6
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(11, 110)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel2.TabIndex = 99
        Me.MyLabel2.Text = "Payment Type"
        '
        'lblDocDate
        '
        Me.lblDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocDate.Location = New System.Drawing.Point(348, 36)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(40, 16)
        Me.lblDocDate.TabIndex = 98
        Me.lblDocDate.Text = "D.O.B."
        '
        'dtpDoB
        '
        Me.dtpDoB.CustomFormat = "dd/MM/yyyy"
        Me.dtpDoB.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDoB.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDoB.Location = New System.Drawing.Point(422, 35)
        Me.dtpDoB.MendatroryField = True
        Me.dtpDoB.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDoB.MyLinkLable1 = Me.lblDocDate
        Me.dtpDoB.MyLinkLable2 = Nothing
        Me.dtpDoB.Name = "dtpDoB"
        Me.dtpDoB.NullDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.dtpDoB.Size = New System.Drawing.Size(205, 18)
        Me.dtpDoB.TabIndex = 3
        Me.dtpDoB.TabStop = False
        Me.dtpDoB.Text = "03/05/2011"
        Me.dtpDoB.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'CmbGender
        '
        Me.CmbGender.AllowShowFocusCues = False
        Me.CmbGender.AutoCompleteDisplayMember = Nothing
        Me.CmbGender.AutoCompleteValueMember = Nothing
        Me.CmbGender.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Text = "Male"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Female"
        RadListDataItem2.TextWrap = True
        RadListDataItem8.Text = "Unisex"
        RadListDataItem8.TextWrap = True
        Me.CmbGender.Items.Add(RadListDataItem1)
        Me.CmbGender.Items.Add(RadListDataItem2)
        Me.CmbGender.Items.Add(RadListDataItem8)
        Me.CmbGender.Location = New System.Drawing.Point(112, 34)
        Me.CmbGender.MendatroryField = True
        Me.CmbGender.MyLinkLable1 = Nothing
        Me.CmbGender.MyLinkLable2 = Nothing
        Me.CmbGender.Name = "CmbGender"
        Me.CmbGender.Size = New System.Drawing.Size(226, 20)
        Me.CmbGender.TabIndex = 2
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(11, 36)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel1.TabIndex = 95
        Me.MyLabel1.Text = "Gender"
        '
        'FndEployeeCode
        '
        Me.FndEployeeCode.Location = New System.Drawing.Point(112, 84)
        Me.FndEployeeCode.MendatroryField = True
        Me.FndEployeeCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndEployeeCode.MyLinkLable1 = Me.LblEmp
        Me.FndEployeeCode.MyLinkLable2 = Nothing
        Me.FndEployeeCode.MyReadOnly = False
        Me.FndEployeeCode.MyShowMasterFormButton = False
        Me.FndEployeeCode.Name = "FndEployeeCode"
        Me.FndEployeeCode.Size = New System.Drawing.Size(137, 19)
        Me.FndEployeeCode.TabIndex = 5
        Me.FndEployeeCode.Value = ""
        '
        'LblEmp
        '
        Me.LblEmp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEmp.Location = New System.Drawing.Point(11, 85)
        Me.LblEmp.Name = "LblEmp"
        Me.LblEmp.Size = New System.Drawing.Size(87, 16)
        Me.LblEmp.TabIndex = 94
        Me.LblEmp.Text = "Employee Code"
        '
        'fndInstitutecode
        '
        Me.fndInstitutecode.Location = New System.Drawing.Point(112, 60)
        Me.fndInstitutecode.MendatroryField = True
        Me.fndInstitutecode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndInstitutecode.MyLinkLable1 = Me.lblCusGrp
        Me.fndInstitutecode.MyLinkLable2 = Nothing
        Me.fndInstitutecode.MyReadOnly = False
        Me.fndInstitutecode.MyShowMasterFormButton = False
        Me.fndInstitutecode.Name = "fndInstitutecode"
        Me.fndInstitutecode.Size = New System.Drawing.Size(137, 19)
        Me.fndInstitutecode.TabIndex = 4
        Me.fndInstitutecode.Value = ""
        '
        'lblCusGrp
        '
        Me.lblCusGrp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCusGrp.Location = New System.Drawing.Point(11, 61)
        Me.lblCusGrp.Name = "lblCusGrp"
        Me.lblCusGrp.Size = New System.Drawing.Size(76, 16)
        Me.lblCusGrp.TabIndex = 91
        Me.lblCusGrp.Text = "Institute Code"
        '
        'MyLabel16
        '
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(11, 12)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel16.TabIndex = 88
        Me.MyLabel16.Text = "First Name"
        '
        'RadLabel13
        '
        Me.RadLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel13.Location = New System.Drawing.Point(348, 12)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(61, 16)
        Me.RadLabel13.TabIndex = 86
        Me.RadLabel13.Text = "Last Name"
        '
        'txtCity
        '
        Me.txtCity.AutoSize = False
        Me.txtCity.BorderVisible = True
        Me.txtCity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCity.Location = New System.Drawing.Point(251, 272)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(456, 18)
        Me.txtCity.TabIndex = 17
        Me.txtCity.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCity.TextWrap = False
        '
        'txtcountrycode
        '
        Me.txtcountrycode.Location = New System.Drawing.Point(112, 224)
        Me.txtcountrycode.MendatroryField = True
        Me.txtcountrycode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcountrycode.MyLinkLable1 = Me.RadLabel7
        Me.txtcountrycode.MyLinkLable2 = Me.txtCountry
        Me.txtcountrycode.MyReadOnly = False
        Me.txtcountrycode.MyShowMasterFormButton = False
        Me.txtcountrycode.Name = "txtcountrycode"
        Me.txtcountrycode.Size = New System.Drawing.Size(137, 18)
        Me.txtcountrycode.TabIndex = 10
        Me.txtcountrycode.Value = ""
        '
        'RadLabel7
        '
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(11, 224)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(46, 16)
        Me.RadLabel7.TabIndex = 20
        Me.RadLabel7.Text = "Country"
        '
        'txtCountry
        '
        Me.txtCountry.AutoSize = False
        Me.txtCountry.BorderVisible = True
        Me.txtCountry.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCountry.Location = New System.Drawing.Point(251, 224)
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.Size = New System.Drawing.Size(456, 18)
        Me.txtCountry.TabIndex = 13
        Me.txtCountry.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCountry.TextWrap = False
        '
        'txtState
        '
        Me.txtState.AutoSize = False
        Me.txtState.BorderVisible = True
        Me.txtState.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtState.Location = New System.Drawing.Point(251, 248)
        Me.txtState.Name = "txtState"
        Me.txtState.Size = New System.Drawing.Size(456, 18)
        Me.txtState.TabIndex = 15
        Me.txtState.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtState.TextWrap = False
        '
        'txtstatecode
        '
        Me.txtstatecode.Location = New System.Drawing.Point(112, 248)
        Me.txtstatecode.MendatroryField = True
        Me.txtstatecode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtstatecode.MyLinkLable1 = Me.RadLabel6
        Me.txtstatecode.MyLinkLable2 = Me.txtState
        Me.txtstatecode.MyReadOnly = False
        Me.txtstatecode.MyShowMasterFormButton = False
        Me.txtstatecode.Name = "txtstatecode"
        Me.txtstatecode.Size = New System.Drawing.Size(137, 18)
        Me.txtstatecode.TabIndex = 11
        Me.txtstatecode.Value = ""
        '
        'RadLabel6
        '
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(11, 251)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel6.TabIndex = 21
        Me.RadLabel6.Text = "State"
        '
        'fndCity
        '
        Me.fndCity.Location = New System.Drawing.Point(112, 272)
        Me.fndCity.MendatroryField = True
        Me.fndCity.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCity.MyLinkLable1 = Me.RadLabel5
        Me.fndCity.MyLinkLable2 = Me.txtCity
        Me.fndCity.MyReadOnly = False
        Me.fndCity.MyShowMasterFormButton = False
        Me.fndCity.Name = "fndCity"
        Me.fndCity.Size = New System.Drawing.Size(137, 18)
        Me.fndCity.TabIndex = 12
        Me.fndCity.Value = ""
        '
        'RadLabel5
        '
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(11, 274)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(26, 16)
        Me.RadLabel5.TabIndex = 22
        Me.RadLabel5.Text = "City"
        '
        'RadLabel11
        '
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(11, 345)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(38, 16)
        Me.RadLabel11.TabIndex = 16
        Me.RadLabel11.Text = "E-Mail"
        '
        'RadLabel10
        '
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.Location = New System.Drawing.Point(348, 299)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(25, 16)
        Me.RadLabel10.TabIndex = 19
        Me.RadLabel10.Text = "Fax"
        '
        'RadLabel8
        '
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(348, 323)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(45, 16)
        Me.RadLabel8.TabIndex = 17
        Me.RadLabel8.Text = "Phone2"
        '
        'RadLabel9
        '
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(12, 323)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(45, 16)
        Me.RadLabel9.TabIndex = 18
        Me.RadLabel9.Text = "Phone1"
        '
        'txtfax
        '
        Me.txtfax.Location = New System.Drawing.Point(422, 297)
        Me.txtfax.MaxLength = 20
        Me.txtfax.MendatroryField = False
        Me.txtfax.MyLinkLable1 = Me.RadLabel10
        Me.txtfax.MyLinkLable2 = Nothing
        Me.txtfax.Name = "txtfax"
        Me.txtfax.Size = New System.Drawing.Size(285, 20)
        Me.txtfax.TabIndex = 14
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(112, 346)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.MendatroryField = False
        Me.txtEmail.MyLinkLable1 = Nothing
        Me.txtEmail.MyLinkLable2 = Nothing
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(226, 20)
        Me.txtEmail.TabIndex = 17
        '
        'txtPhone2
        '
        Me.txtPhone2.Location = New System.Drawing.Point(422, 321)
        Me.txtPhone2.MaxLength = 20
        Me.txtPhone2.MendatroryField = False
        Me.txtPhone2.MyLinkLable1 = Me.RadLabel8
        Me.txtPhone2.MyLinkLable2 = Nothing
        Me.txtPhone2.Name = "txtPhone2"
        Me.txtPhone2.Size = New System.Drawing.Size(285, 20)
        Me.txtPhone2.TabIndex = 16
        '
        'txtPhone1
        '
        Me.txtPhone1.Location = New System.Drawing.Point(112, 321)
        Me.txtPhone1.MaxLength = 20
        Me.txtPhone1.MendatroryField = False
        Me.txtPhone1.MyLinkLable1 = Me.RadLabel9
        Me.txtPhone1.MyLinkLable2 = Nothing
        Me.txtPhone1.Name = "txtPhone1"
        Me.txtPhone1.Size = New System.Drawing.Size(226, 20)
        Me.txtPhone1.TabIndex = 15
        '
        'txtAdd2
        '
        Me.txtAdd2.Location = New System.Drawing.Point(112, 172)
        Me.txtAdd2.MaxLength = 250
        Me.txtAdd2.MendatroryField = True
        Me.txtAdd2.MyLinkLable1 = Me.RadLabel2
        Me.txtAdd2.MyLinkLable2 = Nothing
        Me.txtAdd2.Name = "txtAdd2"
        Me.txtAdd2.Size = New System.Drawing.Size(595, 20)
        Me.txtAdd2.TabIndex = 8
        '
        'txtAdd1
        '
        Me.txtAdd1.Location = New System.Drawing.Point(112, 146)
        Me.txtAdd1.MaxLength = 250
        Me.txtAdd1.MendatroryField = True
        Me.txtAdd1.MyLinkLable1 = Me.RadLabel2
        Me.txtAdd1.MyLinkLable2 = Nothing
        Me.txtAdd1.Name = "txtAdd1"
        Me.txtAdd1.Size = New System.Drawing.Size(595, 20)
        Me.txtAdd1.TabIndex = 7
        '
        'txtAdd3
        '
        Me.txtAdd3.Location = New System.Drawing.Point(112, 198)
        Me.txtAdd3.MaxLength = 250
        Me.txtAdd3.MendatroryField = True
        Me.txtAdd3.MyLinkLable1 = Me.RadLabel2
        Me.txtAdd3.MyLinkLable2 = Nothing
        Me.txtAdd3.Name = "txtAdd3"
        Me.txtAdd3.Size = New System.Drawing.Size(595, 20)
        Me.txtAdd3.TabIndex = 9
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox9)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(77.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(738, 475)
        Me.RadPageViewPage2.Text = "Other Detail"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gvTrainingCourse)
        Me.RadGroupBox2.HeaderText = "Training Course"
        Me.RadGroupBox2.Location = New System.Drawing.Point(4, 193)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(367, 169)
        Me.RadGroupBox2.TabIndex = 26
        Me.RadGroupBox2.Text = "Training Course"
        '
        'gvTrainingCourse
        '
        Me.gvTrainingCourse.AccessibleName = "cbgDoc"
        Me.gvTrainingCourse.CheckedValue = Nothing
        Me.gvTrainingCourse.DataSource = Nothing
        Me.gvTrainingCourse.DisplayMember = "Name"
        Me.gvTrainingCourse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvTrainingCourse.Location = New System.Drawing.Point(10, 20)
        Me.gvTrainingCourse.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.gvTrainingCourse.MyShowHeadrText = False
        Me.gvTrainingCourse.Name = "gvTrainingCourse"
        Me.gvTrainingCourse.Size = New System.Drawing.Size(347, 139)
        Me.gvTrainingCourse.TabIndex = 1
        Me.gvTrainingCourse.ValueMember = "Code"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gvQualification)
        Me.RadGroupBox1.HeaderText = "Qualification"
        Me.RadGroupBox1.Location = New System.Drawing.Point(377, 18)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(342, 169)
        Me.RadGroupBox1.TabIndex = 25
        Me.RadGroupBox1.Text = "Qualification"
        '
        'gvQualification
        '
        Me.gvQualification.AccessibleName = "cbgDoc"
        Me.gvQualification.CheckedValue = Nothing
        Me.gvQualification.DataSource = Nothing
        Me.gvQualification.DisplayMember = "Name"
        Me.gvQualification.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvQualification.Location = New System.Drawing.Point(10, 20)
        Me.gvQualification.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.gvQualification.MyShowHeadrText = False
        Me.gvQualification.Name = "gvQualification"
        Me.gvQualification.Size = New System.Drawing.Size(322, 139)
        Me.gvQualification.TabIndex = 1
        Me.gvQualification.ValueMember = "Code"
        '
        'RadGroupBox9
        '
        Me.RadGroupBox9.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox9.Controls.Add(Me.gvTrainingCities)
        Me.RadGroupBox9.HeaderText = "Cities Training Given"
        Me.RadGroupBox9.Location = New System.Drawing.Point(4, 18)
        Me.RadGroupBox9.Name = "RadGroupBox9"
        Me.RadGroupBox9.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox9.Size = New System.Drawing.Size(367, 169)
        Me.RadGroupBox9.TabIndex = 24
        Me.RadGroupBox9.Text = "Cities Training Given"
        '
        'gvTrainingCities
        '
        Me.gvTrainingCities.AccessibleName = "cbgDoc"
        Me.gvTrainingCities.CheckedValue = Nothing
        Me.gvTrainingCities.DataSource = Nothing
        Me.gvTrainingCities.DisplayMember = "Name"
        Me.gvTrainingCities.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvTrainingCities.Location = New System.Drawing.Point(10, 20)
        Me.gvTrainingCities.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.gvTrainingCities.MyShowHeadrText = False
        Me.gvTrainingCities.Name = "gvTrainingCities"
        Me.gvTrainingCities.Size = New System.Drawing.Size(347, 139)
        Me.gvTrainingCities.TabIndex = 1
        Me.gvTrainingCities.ValueMember = "Code"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(73.0!, 26.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(738, 475)
        Me.RadPageViewPage3.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(738, 475)
        Me.UcAttachment1.TabIndex = 2
        '
        'btnclear
        '
        Me.btnclear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear.Location = New System.Drawing.Point(714, 10)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(66, 18)
        Me.btnclear.TabIndex = 3
        Me.btnclear.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(83, 8)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(12, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuClose})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(787, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'MenuClose
        '
        Me.MenuClose.AccessibleDescription = "File"
        Me.MenuClose.AccessibleName = "File"
        Me.MenuClose.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuImport, Me.MenuExport, Me.RadMenuItem4})
        Me.MenuClose.Name = "MenuClose"
        Me.MenuClose.Text = "File"
        Me.MenuClose.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'MenuImport
        '
        Me.MenuImport.AccessibleDescription = "MenuImport"
        Me.MenuImport.AccessibleName = "RadMenuItem2"
        Me.MenuImport.Name = "MenuImport"
        Me.MenuImport.Text = "Import"
        Me.MenuImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'MenuExport
        '
        Me.MenuExport.AccessibleDescription = "MenuExport"
        Me.MenuExport.AccessibleName = "Export"
        Me.MenuExport.Name = "MenuExport"
        Me.MenuExport.Text = "Export"
        Me.MenuExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "RadMenuItem4"
        Me.RadMenuItem4.AccessibleName = "Close"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Close"
        Me.RadMenuItem4.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.vendorgrpbox)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclear)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(787, 615)
        Me.SplitContainer1.SplitterDistance = 582
        Me.SplitContainer1.TabIndex = 2
        '
        'BtnPost
        '
        Me.BtnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPost.Location = New System.Drawing.Point(157, 8)
        Me.BtnPost.Name = "BtnPost"
        Me.BtnPost.Size = New System.Drawing.Size(66, 18)
        Me.BtnPost.TabIndex = 2
        Me.BtnPost.Text = "Post"
        '
        'txtempext
        '
        Me.txtempext.Location = New System.Drawing.Point(112, 83)
        Me.txtempext.MaxLength = 50
        Me.txtempext.MendatroryField = False
        Me.txtempext.MyLinkLable1 = Me.RadLabel10
        Me.txtempext.MyLinkLable2 = Nothing
        Me.txtempext.Name = "txtempext"
        Me.txtempext.Size = New System.Drawing.Size(226, 20)
        Me.txtempext.TabIndex = 104
        '
        'frmTrainerMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(787, 635)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmTrainerMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Trainer Master"
        CType(Me.vendorgrpbox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.vendorgrpbox.ResumeLayout(False)
        Me.vendorgrpbox.PerformLayout()
        CType(Me.chkIsApplicable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBaseCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvendorname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pageCus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageCus.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.txtLastName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFirstName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPinCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblEmployeeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblInstituteName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbPaymentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDoB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbGender, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblEmp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCusGrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPhone2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPhone1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox9.ResumeLayout(False)
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.btnclear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.BtnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtempext, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents vendorgrpbox As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents pageCus As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtEmail As common.Controls.MyTextBox
    Friend WithEvents txtAdd2 As common.Controls.MyTextBox
    Friend WithEvents txtAdd1 As common.Controls.MyTextBox
    Friend WithEvents txtAdd3 As common.Controls.MyTextBox
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents btnclear As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtName As common.Controls.MyTextBox
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents ToolTipvendor As System.Windows.Forms.ToolTip
    Friend WithEvents MenuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents lblvendorname As common.Controls.MyLabel
    Friend WithEvents lblvandorno As common.Controls.MyLabel
    Friend WithEvents fndvendorNo As common.UserControls.txtNavigator
    Friend WithEvents fndCity As common.UserControls.txtFinder
    Friend WithEvents txtfax As common.Controls.MyTextBox
    Friend WithEvents txtPhone2 As common.Controls.MyTextBox
    Friend WithEvents txtPhone1 As common.Controls.MyTextBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblBaseCurrency As common.Controls.MyLabel
    Friend WithEvents txtstatecode As common.UserControls.txtFinder
    Friend WithEvents txtcountrycode As common.UserControls.txtFinder
    Friend WithEvents txtCountry As common.Controls.MyLabel
    Friend WithEvents txtState As common.Controls.MyLabel
    Friend WithEvents txtCity As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents CmbType As common.Controls.MyComboBox
    Friend WithEvents CmbPaymentType As common.Controls.MyComboBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblDocDate As common.Controls.MyLabel
    Friend WithEvents dtpDoB As common.Controls.MyDateTimePicker
    Friend WithEvents CmbGender As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents FndEployeeCode As common.UserControls.txtFinder
    Friend WithEvents LblEmp As common.Controls.MyLabel
    Friend WithEvents fndInstitutecode As common.UserControls.txtFinder
    Friend WithEvents lblCusGrp As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents RadLabel13 As common.Controls.MyLabel
    Friend WithEvents LblEmployeeName As common.Controls.MyLabel
    Friend WithEvents LblInstituteName As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents TxtRemark As common.Controls.MyTextBox
    Friend WithEvents chkIsApplicable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvQualification As common.MyCheckBoxGrid
    Friend WithEvents RadGroupBox9 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvTrainingCities As common.MyCheckBoxGrid
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvTrainingCourse As common.MyCheckBoxGrid
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents TxtPinCode As common.Controls.MyTextBox
    Friend WithEvents BtnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents TxtFirstName As common.Controls.MyTextBox
    Friend WithEvents txtLastName As common.Controls.MyTextBox
    Friend WithEvents txtempext As common.Controls.MyTextBox
End Class

