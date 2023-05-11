<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCattleMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCattleMaster))
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.rmi = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtCattleCode = New common.Controls.MyTextBox()
        Me.txtCattleInAge = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel32 = New common.Controls.MyLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rdbParentType = New System.Windows.Forms.RadioButton()
        Me.rdbChildCattleType = New System.Windows.Forms.RadioButton()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.txtRegistrationCharge = New common.MyNumBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dtpInsuranceDateFrom = New common.Controls.MyDateTimePicker()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.MyLabel28 = New common.Controls.MyLabel()
        Me.dtpInsuranceTo = New common.Controls.MyDateTimePicker()
        Me.txtInsuranceNo = New common.Controls.MyTextBox()
        Me.MyLabel30 = New common.Controls.MyLabel()
        Me.lblPMCCode = New common.Controls.MyLabel()
        Me.txtPMCCode = New common.UserControls.txtFinder()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtHeadOffice = New common.Controls.MyTextBox()
        Me.lblCattleColor = New common.Controls.MyLabel()
        Me.txtCattleColor = New common.UserControls.txtFinder()
        Me.lblNDDBCode = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtNDDBCode = New common.UserControls.txtFinder()
        Me.lblItemCategoryCode = New common.Controls.MyLabel()
        Me.txtMilkFat = New common.MyNumBox()
        Me.MyLabel26 = New common.Controls.MyLabel()
        Me.txtMilkQty = New common.MyNumBox()
        Me.MyLabel25 = New common.Controls.MyLabel()
        Me.lblFather = New common.Controls.MyLabel()
        Me.txtFather = New common.UserControls.txtFinder()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.lblMother = New common.Controls.MyLabel()
        Me.txtMother = New common.UserControls.txtFinder()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.lblFarmar = New common.Controls.MyLabel()
        Me.txtFarmer = New common.UserControls.txtFinder()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.lblMCC = New common.Controls.MyLabel()
        Me.txtMCC = New common.UserControls.txtFinder()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.lblBranch = New common.Controls.MyLabel()
        Me.txtBranch = New common.UserControls.txtFinder()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.lblArea = New common.Controls.MyLabel()
        Me.txtArea = New common.UserControls.txtFinder()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.lblRegion = New common.Controls.MyLabel()
        Me.txtRegion = New common.UserControls.txtFinder()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.lblZone = New common.Controls.MyLabel()
        Me.txtZone = New common.UserControls.txtFinder()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblBreed = New common.Controls.MyLabel()
        Me.txtBreed = New common.UserControls.txtFinder()
        Me.lblCattleType = New common.Controls.MyLabel()
        Me.txtCattleType = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblType = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtRegistrationDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.txtRegistrationNo = New common.UserControls.txtNavigator()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.cboCattleStatus = New common.Controls.MyComboBox()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtTagId = New common.Controls.MyTextBox()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.dtpDOB = New common.Controls.MyDateTimePicker()
        Me.MyLabel29 = New common.Controls.MyLabel()
        Me.cboGender = New common.Controls.MyComboBox()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCattleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCattleInAge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtRegistrationCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dtpInsuranceDateFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpInsuranceTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInsuranceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPMCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHeadOffice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCattleColor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNDDBCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMilkFat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMilkQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFather, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMother, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFarmar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRegion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblZone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBreed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCattleType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRegistrationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboCattleStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTagId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDOB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboGender, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmi})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(982, 20)
        Me.RadMenu1.TabIndex = 3
        Me.RadMenu1.Text = "RadMenu1"
        '
        'rmi
        '
        Me.rmi.AccessibleDescription = "File"
        Me.rmi.AccessibleName = "File"
        Me.rmi.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2, Me.RadMenuItem3})
        Me.rmi.Name = "rmi"
        Me.rmi.Text = "File"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "RadMenuItem1"
        Me.RadMenuItem1.AccessibleName = "RadMenuItem1"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Import"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Export"
        Me.RadMenuItem2.AccessibleName = "Export"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Exit"
        Me.RadMenuItem3.AccessibleName = "Exit"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Exit"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCattleCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCattleInAge)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel32)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRegistrationDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRegistrationNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboCattleStatus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTagId)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel24)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpDOB)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel29)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboGender)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(982, 678)
        Me.SplitContainer1.SplitterDistance = 629
        Me.SplitContainer1.TabIndex = 4
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(586, 10)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(66, 16)
        Me.MyLabel4.TabIndex = 256
        Me.MyLabel4.Text = "Cattle Code"
        '
        'txtCattleCode
        '
        Me.txtCattleCode.AutoSize = False
        Me.txtCattleCode.CalculationExpression = Nothing
        Me.txtCattleCode.FieldCode = Nothing
        Me.txtCattleCode.FieldDesc = Nothing
        Me.txtCattleCode.FieldMaxLength = 0
        Me.txtCattleCode.FieldName = Nothing
        Me.txtCattleCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCattleCode.isCalculatedField = False
        Me.txtCattleCode.IsSourceFromTable = False
        Me.txtCattleCode.IsSourceFromValueList = False
        Me.txtCattleCode.IsUnique = False
        Me.txtCattleCode.Location = New System.Drawing.Point(683, 7)
        Me.txtCattleCode.MaxLength = 50
        Me.txtCattleCode.MendatroryField = False
        Me.txtCattleCode.Multiline = True
        Me.txtCattleCode.MyLinkLable1 = Nothing
        Me.txtCattleCode.MyLinkLable2 = Nothing
        Me.txtCattleCode.Name = "txtCattleCode"
        Me.txtCattleCode.ReferenceFieldDesc = Nothing
        Me.txtCattleCode.ReferenceFieldName = Nothing
        Me.txtCattleCode.ReferenceTableName = Nothing
        Me.txtCattleCode.Size = New System.Drawing.Size(220, 21)
        Me.txtCattleCode.TabIndex = 257
        Me.txtCattleCode.Text = " "
        '
        'txtCattleInAge
        '
        Me.txtCattleInAge.AutoSize = False
        Me.txtCattleInAge.CalculationExpression = Nothing
        Me.txtCattleInAge.FieldCode = Nothing
        Me.txtCattleInAge.FieldDesc = Nothing
        Me.txtCattleInAge.FieldMaxLength = 0
        Me.txtCattleInAge.FieldName = Nothing
        Me.txtCattleInAge.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCattleInAge.isCalculatedField = False
        Me.txtCattleInAge.IsSourceFromTable = False
        Me.txtCattleInAge.IsSourceFromValueList = False
        Me.txtCattleInAge.IsUnique = False
        Me.txtCattleInAge.Location = New System.Drawing.Point(259, 48)
        Me.txtCattleInAge.MaxLength = 50
        Me.txtCattleInAge.MendatroryField = False
        Me.txtCattleInAge.Multiline = True
        Me.txtCattleInAge.MyLinkLable1 = Nothing
        Me.txtCattleInAge.MyLinkLable2 = Nothing
        Me.txtCattleInAge.Name = "txtCattleInAge"
        Me.txtCattleInAge.ReferenceFieldDesc = Nothing
        Me.txtCattleInAge.ReferenceFieldName = Nothing
        Me.txtCattleInAge.ReferenceTableName = Nothing
        Me.txtCattleInAge.Size = New System.Drawing.Size(165, 21)
        Me.txtCattleInAge.TabIndex = 255
        Me.txtCattleInAge.Text = " "
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(191, 51)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel3.TabIndex = 254
        Me.MyLabel3.Text = "Cattle  Age"
        '
        'MyLabel32
        '
        Me.MyLabel32.FieldName = Nothing
        Me.MyLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel32.Location = New System.Drawing.Point(586, 74)
        Me.MyLabel32.Name = "MyLabel32"
        Me.MyLabel32.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel32.TabIndex = 253
        Me.MyLabel32.Text = "Type"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rdbParentType)
        Me.Panel1.Controls.Add(Me.rdbChildCattleType)
        Me.Panel1.Location = New System.Drawing.Point(684, 71)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(219, 21)
        Me.Panel1.TabIndex = 252
        '
        'rdbParentType
        '
        Me.rdbParentType.AutoSize = True
        Me.rdbParentType.Location = New System.Drawing.Point(6, 1)
        Me.rdbParentType.Name = "rdbParentType"
        Me.rdbParentType.Size = New System.Drawing.Size(91, 17)
        Me.rdbParentType.TabIndex = 230
        Me.rdbParentType.Text = "Parent Cattle"
        Me.rdbParentType.UseVisualStyleBackColor = True
        '
        'rdbChildCattleType
        '
        Me.rdbChildCattleType.AutoSize = True
        Me.rdbChildCattleType.Checked = True
        Me.rdbChildCattleType.Location = New System.Drawing.Point(103, 1)
        Me.rdbChildCattleType.Name = "rdbChildCattleType"
        Me.rdbChildCattleType.Size = New System.Drawing.Size(85, 17)
        Me.rdbChildCattleType.TabIndex = 231
        Me.rdbChildCattleType.TabStop = True
        Me.rdbChildCattleType.Text = "Child Cattle"
        Me.rdbChildCattleType.UseVisualStyleBackColor = True
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Location = New System.Drawing.Point(0, 73)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(982, 562)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.txtRegistrationCharge)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.lblPMCCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtPMCCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel16)
        Me.RadPageViewPage1.Controls.Add(Me.txtHeadOffice)
        Me.RadPageViewPage1.Controls.Add(Me.lblCattleColor)
        Me.RadPageViewPage1.Controls.Add(Me.txtCattleColor)
        Me.RadPageViewPage1.Controls.Add(Me.lblNDDBCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.txtNDDBCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblItemCategoryCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtMilkFat)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel26)
        Me.RadPageViewPage1.Controls.Add(Me.txtMilkQty)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel25)
        Me.RadPageViewPage1.Controls.Add(Me.lblFather)
        Me.RadPageViewPage1.Controls.Add(Me.txtFather)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel23)
        Me.RadPageViewPage1.Controls.Add(Me.lblMother)
        Me.RadPageViewPage1.Controls.Add(Me.txtMother)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel21)
        Me.RadPageViewPage1.Controls.Add(Me.lblFarmar)
        Me.RadPageViewPage1.Controls.Add(Me.txtFarmer)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel19)
        Me.RadPageViewPage1.Controls.Add(Me.lblMCC)
        Me.RadPageViewPage1.Controls.Add(Me.txtMCC)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel17)
        Me.RadPageViewPage1.Controls.Add(Me.lblBranch)
        Me.RadPageViewPage1.Controls.Add(Me.txtBranch)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.lblArea)
        Me.RadPageViewPage1.Controls.Add(Me.txtArea)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.lblRegion)
        Me.RadPageViewPage1.Controls.Add(Me.txtRegion)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel11)
        Me.RadPageViewPage1.Controls.Add(Me.lblZone)
        Me.RadPageViewPage1.Controls.Add(Me.txtZone)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.lblBreed)
        Me.RadPageViewPage1.Controls.Add(Me.txtBreed)
        Me.RadPageViewPage1.Controls.Add(Me.lblCattleType)
        Me.RadPageViewPage1.Controls.Add(Me.txtCattleType)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.lblType)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(82.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(961, 514)
        Me.RadPageViewPage1.Text = "Cattle Details"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.CheckBox3)
        Me.RadGroupBox1.Controls.Add(Me.CheckBox2)
        Me.RadGroupBox1.Controls.Add(Me.CheckBox1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(224, 412)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(474, 29)
        Me.RadGroupBox1.TabIndex = 254
        Me.RadGroupBox1.Visible = False
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(4, 5)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(79, 17)
        Me.CheckBox3.TabIndex = 2
        Me.CheckBox3.Text = "Pragnancy"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(170, 6)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(67, 17)
        Me.CheckBox2.TabIndex = 1
        Me.CheckBox2.Text = "Bill Paid"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(84, 6)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(78, 17)
        Me.CheckBox1.TabIndex = 0
        Me.CheckBox1.Text = "Bill Create"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'txtRegistrationCharge
        '
        Me.txtRegistrationCharge.BackColor = System.Drawing.Color.White
        Me.txtRegistrationCharge.CalculationExpression = Nothing
        Me.txtRegistrationCharge.DecimalPlaces = 2
        Me.txtRegistrationCharge.FieldCode = Nothing
        Me.txtRegistrationCharge.FieldDesc = Nothing
        Me.txtRegistrationCharge.FieldMaxLength = 0
        Me.txtRegistrationCharge.FieldName = Nothing
        Me.txtRegistrationCharge.isCalculatedField = False
        Me.txtRegistrationCharge.IsSourceFromTable = False
        Me.txtRegistrationCharge.IsSourceFromValueList = False
        Me.txtRegistrationCharge.IsUnique = False
        Me.txtRegistrationCharge.Location = New System.Drawing.Point(122, 412)
        Me.txtRegistrationCharge.MendatroryField = False
        Me.txtRegistrationCharge.MyLinkLable1 = Nothing
        Me.txtRegistrationCharge.MyLinkLable2 = Nothing
        Me.txtRegistrationCharge.Name = "txtRegistrationCharge"
        Me.txtRegistrationCharge.ReferenceFieldDesc = Nothing
        Me.txtRegistrationCharge.ReferenceFieldName = Nothing
        Me.txtRegistrationCharge.ReferenceTableName = Nothing
        Me.txtRegistrationCharge.Size = New System.Drawing.Size(78, 20)
        Me.txtRegistrationCharge.TabIndex = 253
        Me.txtRegistrationCharge.Text = "0"
        Me.txtRegistrationCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRegistrationCharge.Value = 0.0R
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(9, 415)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(107, 16)
        Me.MyLabel10.TabIndex = 252
        Me.MyLabel10.Text = "Registration Charge"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dtpInsuranceDateFrom)
        Me.GroupBox1.Controls.Add(Me.MyLabel28)
        Me.GroupBox1.Controls.Add(Me.dtpInsuranceTo)
        Me.GroupBox1.Controls.Add(Me.txtInsuranceNo)
        Me.GroupBox1.Controls.Add(Me.MyLabel30)
        Me.GroupBox1.Controls.Add(Me.MyLabel18)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 327)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(698, 55)
        Me.GroupBox1.TabIndex = 251
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Insurance"
        '
        'dtpInsuranceDateFrom
        '
        Me.dtpInsuranceDateFrom.CalculationExpression = Nothing
        Me.dtpInsuranceDateFrom.CustomFormat = "dd/MM/yyyy"
        Me.dtpInsuranceDateFrom.FieldCode = Nothing
        Me.dtpInsuranceDateFrom.FieldDesc = Nothing
        Me.dtpInsuranceDateFrom.FieldMaxLength = 0
        Me.dtpInsuranceDateFrom.FieldName = Nothing
        Me.dtpInsuranceDateFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpInsuranceDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpInsuranceDateFrom.isCalculatedField = False
        Me.dtpInsuranceDateFrom.IsSourceFromTable = False
        Me.dtpInsuranceDateFrom.IsSourceFromValueList = False
        Me.dtpInsuranceDateFrom.IsUnique = False
        Me.dtpInsuranceDateFrom.Location = New System.Drawing.Point(407, 19)
        Me.dtpInsuranceDateFrom.MendatroryField = False
        Me.dtpInsuranceDateFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpInsuranceDateFrom.MyLinkLable1 = Me.MyLabel18
        Me.dtpInsuranceDateFrom.MyLinkLable2 = Nothing
        Me.dtpInsuranceDateFrom.Name = "dtpInsuranceDateFrom"
        Me.dtpInsuranceDateFrom.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpInsuranceDateFrom.ReferenceFieldDesc = Nothing
        Me.dtpInsuranceDateFrom.ReferenceFieldName = Nothing
        Me.dtpInsuranceDateFrom.ReferenceTableName = Nothing
        Me.dtpInsuranceDateFrom.Size = New System.Drawing.Size(81, 18)
        Me.dtpInsuranceDateFrom.TabIndex = 251
        Me.dtpInsuranceDateFrom.TabStop = False
        Me.dtpInsuranceDateFrom.Text = "13/06/2011"
        Me.dtpInsuranceDateFrom.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(512, 20)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel18.TabIndex = 249
        Me.MyLabel18.Text = "To Date"
        '
        'MyLabel28
        '
        Me.MyLabel28.FieldName = Nothing
        Me.MyLabel28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel28.Location = New System.Drawing.Point(331, 20)
        Me.MyLabel28.Name = "MyLabel28"
        Me.MyLabel28.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel28.TabIndex = 250
        Me.MyLabel28.Text = "From Date"
        '
        'dtpInsuranceTo
        '
        Me.dtpInsuranceTo.CalculationExpression = Nothing
        Me.dtpInsuranceTo.CustomFormat = "dd/MM/yyyy"
        Me.dtpInsuranceTo.FieldCode = Nothing
        Me.dtpInsuranceTo.FieldDesc = Nothing
        Me.dtpInsuranceTo.FieldMaxLength = 0
        Me.dtpInsuranceTo.FieldName = Nothing
        Me.dtpInsuranceTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpInsuranceTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpInsuranceTo.isCalculatedField = False
        Me.dtpInsuranceTo.IsSourceFromTable = False
        Me.dtpInsuranceTo.IsSourceFromValueList = False
        Me.dtpInsuranceTo.IsUnique = False
        Me.dtpInsuranceTo.Location = New System.Drawing.Point(564, 19)
        Me.dtpInsuranceTo.MendatroryField = False
        Me.dtpInsuranceTo.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpInsuranceTo.MyLinkLable1 = Me.MyLabel18
        Me.dtpInsuranceTo.MyLinkLable2 = Nothing
        Me.dtpInsuranceTo.Name = "dtpInsuranceTo"
        Me.dtpInsuranceTo.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpInsuranceTo.ReferenceFieldDesc = Nothing
        Me.dtpInsuranceTo.ReferenceFieldName = Nothing
        Me.dtpInsuranceTo.ReferenceTableName = Nothing
        Me.dtpInsuranceTo.Size = New System.Drawing.Size(81, 18)
        Me.dtpInsuranceTo.TabIndex = 248
        Me.dtpInsuranceTo.TabStop = False
        Me.dtpInsuranceTo.Text = "13/06/2011"
        Me.dtpInsuranceTo.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtInsuranceNo
        '
        Me.txtInsuranceNo.AutoSize = False
        Me.txtInsuranceNo.CalculationExpression = Nothing
        Me.txtInsuranceNo.FieldCode = Nothing
        Me.txtInsuranceNo.FieldDesc = Nothing
        Me.txtInsuranceNo.FieldMaxLength = 0
        Me.txtInsuranceNo.FieldName = Nothing
        Me.txtInsuranceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInsuranceNo.isCalculatedField = False
        Me.txtInsuranceNo.IsSourceFromTable = False
        Me.txtInsuranceNo.IsSourceFromValueList = False
        Me.txtInsuranceNo.IsUnique = False
        Me.txtInsuranceNo.Location = New System.Drawing.Point(102, 16)
        Me.txtInsuranceNo.MaxLength = 50
        Me.txtInsuranceNo.MendatroryField = False
        Me.txtInsuranceNo.Multiline = True
        Me.txtInsuranceNo.MyLinkLable1 = Nothing
        Me.txtInsuranceNo.MyLinkLable2 = Nothing
        Me.txtInsuranceNo.Name = "txtInsuranceNo"
        Me.txtInsuranceNo.ReferenceFieldDesc = Nothing
        Me.txtInsuranceNo.ReferenceFieldName = Nothing
        Me.txtInsuranceNo.ReferenceTableName = Nothing
        Me.txtInsuranceNo.Size = New System.Drawing.Size(223, 21)
        Me.txtInsuranceNo.TabIndex = 247
        Me.txtInsuranceNo.Text = " "
        '
        'MyLabel30
        '
        Me.MyLabel30.FieldName = Nothing
        Me.MyLabel30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel30.Location = New System.Drawing.Point(9, 21)
        Me.MyLabel30.Name = "MyLabel30"
        Me.MyLabel30.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel30.TabIndex = 216
        Me.MyLabel30.Text = "Insurance No"
        '
        'lblPMCCode
        '
        Me.lblPMCCode.AutoSize = False
        Me.lblPMCCode.BorderVisible = True
        Me.lblPMCCode.FieldName = Nothing
        Me.lblPMCCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPMCCode.Location = New System.Drawing.Point(330, 165)
        Me.lblPMCCode.Name = "lblPMCCode"
        Me.lblPMCCode.Size = New System.Drawing.Size(368, 18)
        Me.lblPMCCode.TabIndex = 250
        Me.lblPMCCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblPMCCode.TextWrap = False
        '
        'txtPMCCode
        '
        Me.txtPMCCode.CalculationExpression = Nothing
        Me.txtPMCCode.FieldCode = Nothing
        Me.txtPMCCode.FieldDesc = Nothing
        Me.txtPMCCode.FieldMaxLength = 0
        Me.txtPMCCode.FieldName = Nothing
        Me.txtPMCCode.isCalculatedField = False
        Me.txtPMCCode.IsSourceFromTable = False
        Me.txtPMCCode.IsSourceFromValueList = False
        Me.txtPMCCode.IsUnique = False
        Me.txtPMCCode.Location = New System.Drawing.Point(102, 165)
        Me.txtPMCCode.MendatroryField = False
        Me.txtPMCCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMCCode.MyLinkLable1 = Nothing
        Me.txtPMCCode.MyLinkLable2 = Me.lblPMCCode
        Me.txtPMCCode.MyReadOnly = False
        Me.txtPMCCode.MyShowMasterFormButton = False
        Me.txtPMCCode.Name = "txtPMCCode"
        Me.txtPMCCode.ReferenceFieldDesc = Nothing
        Me.txtPMCCode.ReferenceFieldName = Nothing
        Me.txtPMCCode.ReferenceTableName = Nothing
        Me.txtPMCCode.Size = New System.Drawing.Size(223, 18)
        Me.txtPMCCode.TabIndex = 249
        Me.txtPMCCode.Value = ""
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(9, 165)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel16.TabIndex = 248
        Me.MyLabel16.Text = "PMC Code"
        '
        'txtHeadOffice
        '
        Me.txtHeadOffice.AutoSize = False
        Me.txtHeadOffice.CalculationExpression = Nothing
        Me.txtHeadOffice.FieldCode = Nothing
        Me.txtHeadOffice.FieldDesc = Nothing
        Me.txtHeadOffice.FieldMaxLength = 0
        Me.txtHeadOffice.FieldName = Nothing
        Me.txtHeadOffice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeadOffice.isCalculatedField = False
        Me.txtHeadOffice.IsSourceFromTable = False
        Me.txtHeadOffice.IsSourceFromValueList = False
        Me.txtHeadOffice.IsUnique = False
        Me.txtHeadOffice.Location = New System.Drawing.Point(101, 210)
        Me.txtHeadOffice.MaxLength = 50
        Me.txtHeadOffice.MendatroryField = False
        Me.txtHeadOffice.Multiline = True
        Me.txtHeadOffice.MyLinkLable1 = Nothing
        Me.txtHeadOffice.MyLinkLable2 = Nothing
        Me.txtHeadOffice.Name = "txtHeadOffice"
        Me.txtHeadOffice.ReferenceFieldDesc = Nothing
        Me.txtHeadOffice.ReferenceFieldName = Nothing
        Me.txtHeadOffice.ReferenceTableName = Nothing
        Me.txtHeadOffice.Size = New System.Drawing.Size(597, 21)
        Me.txtHeadOffice.TabIndex = 246
        Me.txtHeadOffice.Text = " "
        '
        'lblCattleColor
        '
        Me.lblCattleColor.AutoSize = False
        Me.lblCattleColor.BorderVisible = True
        Me.lblCattleColor.FieldName = Nothing
        Me.lblCattleColor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCattleColor.Location = New System.Drawing.Point(330, 98)
        Me.lblCattleColor.Name = "lblCattleColor"
        Me.lblCattleColor.Size = New System.Drawing.Size(368, 18)
        Me.lblCattleColor.TabIndex = 239
        Me.lblCattleColor.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCattleColor.TextWrap = False
        '
        'txtCattleColor
        '
        Me.txtCattleColor.CalculationExpression = Nothing
        Me.txtCattleColor.FieldCode = Nothing
        Me.txtCattleColor.FieldDesc = Nothing
        Me.txtCattleColor.FieldMaxLength = 0
        Me.txtCattleColor.FieldName = Nothing
        Me.txtCattleColor.isCalculatedField = False
        Me.txtCattleColor.IsSourceFromTable = False
        Me.txtCattleColor.IsSourceFromValueList = False
        Me.txtCattleColor.IsUnique = False
        Me.txtCattleColor.Location = New System.Drawing.Point(102, 98)
        Me.txtCattleColor.MendatroryField = True
        Me.txtCattleColor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCattleColor.MyLinkLable1 = Nothing
        Me.txtCattleColor.MyLinkLable2 = Me.lblCattleColor
        Me.txtCattleColor.MyReadOnly = False
        Me.txtCattleColor.MyShowMasterFormButton = False
        Me.txtCattleColor.Name = "txtCattleColor"
        Me.txtCattleColor.ReferenceFieldDesc = Nothing
        Me.txtCattleColor.ReferenceFieldName = Nothing
        Me.txtCattleColor.ReferenceTableName = Nothing
        Me.txtCattleColor.Size = New System.Drawing.Size(223, 18)
        Me.txtCattleColor.TabIndex = 238
        Me.txtCattleColor.Value = ""
        '
        'lblNDDBCode
        '
        Me.lblNDDBCode.AutoSize = False
        Me.lblNDDBCode.BorderVisible = True
        Me.lblNDDBCode.FieldName = Nothing
        Me.lblNDDBCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNDDBCode.Location = New System.Drawing.Point(330, 11)
        Me.lblNDDBCode.Name = "lblNDDBCode"
        Me.lblNDDBCode.Size = New System.Drawing.Size(368, 18)
        Me.lblNDDBCode.TabIndex = 245
        Me.lblNDDBCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblNDDBCode.TextWrap = False
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(9, 100)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(66, 16)
        Me.MyLabel8.TabIndex = 237
        Me.MyLabel8.Text = "Cattle Color"
        '
        'txtNDDBCode
        '
        Me.txtNDDBCode.CalculationExpression = Nothing
        Me.txtNDDBCode.FieldCode = Nothing
        Me.txtNDDBCode.FieldDesc = Nothing
        Me.txtNDDBCode.FieldMaxLength = 0
        Me.txtNDDBCode.FieldName = Nothing
        Me.txtNDDBCode.isCalculatedField = False
        Me.txtNDDBCode.IsSourceFromTable = False
        Me.txtNDDBCode.IsSourceFromValueList = False
        Me.txtNDDBCode.IsUnique = False
        Me.txtNDDBCode.Location = New System.Drawing.Point(102, 9)
        Me.txtNDDBCode.MendatroryField = True
        Me.txtNDDBCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNDDBCode.MyLinkLable1 = Nothing
        Me.txtNDDBCode.MyLinkLable2 = Me.lblCattleColor
        Me.txtNDDBCode.MyReadOnly = False
        Me.txtNDDBCode.MyShowMasterFormButton = False
        Me.txtNDDBCode.Name = "txtNDDBCode"
        Me.txtNDDBCode.ReferenceFieldDesc = Nothing
        Me.txtNDDBCode.ReferenceFieldName = Nothing
        Me.txtNDDBCode.ReferenceTableName = Nothing
        Me.txtNDDBCode.Size = New System.Drawing.Size(222, 18)
        Me.txtNDDBCode.TabIndex = 242
        Me.txtNDDBCode.Value = ""
        '
        'lblItemCategoryCode
        '
        Me.lblItemCategoryCode.FieldName = Nothing
        Me.lblItemCategoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemCategoryCode.Location = New System.Drawing.Point(9, 10)
        Me.lblItemCategoryCode.Name = "lblItemCategoryCode"
        Me.lblItemCategoryCode.Size = New System.Drawing.Size(72, 16)
        Me.lblItemCategoryCode.TabIndex = 161
        Me.lblItemCategoryCode.Text = "NDDB  Code"
        '
        'txtMilkFat
        '
        Me.txtMilkFat.BackColor = System.Drawing.Color.White
        Me.txtMilkFat.CalculationExpression = Nothing
        Me.txtMilkFat.DecimalPlaces = 2
        Me.txtMilkFat.FieldCode = Nothing
        Me.txtMilkFat.FieldDesc = Nothing
        Me.txtMilkFat.FieldMaxLength = 0
        Me.txtMilkFat.FieldName = Nothing
        Me.txtMilkFat.isCalculatedField = False
        Me.txtMilkFat.IsSourceFromTable = False
        Me.txtMilkFat.IsSourceFromValueList = False
        Me.txtMilkFat.IsUnique = False
        Me.txtMilkFat.Location = New System.Drawing.Point(101, 386)
        Me.txtMilkFat.MendatroryField = False
        Me.txtMilkFat.MyLinkLable1 = Nothing
        Me.txtMilkFat.MyLinkLable2 = Nothing
        Me.txtMilkFat.Name = "txtMilkFat"
        Me.txtMilkFat.ReferenceFieldDesc = Nothing
        Me.txtMilkFat.ReferenceFieldName = Nothing
        Me.txtMilkFat.ReferenceTableName = Nothing
        Me.txtMilkFat.Size = New System.Drawing.Size(99, 20)
        Me.txtMilkFat.TabIndex = 209
        Me.txtMilkFat.Text = "0"
        Me.txtMilkFat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMilkFat.Value = 0.0R
        '
        'MyLabel26
        '
        Me.MyLabel26.FieldName = Nothing
        Me.MyLabel26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel26.Location = New System.Drawing.Point(9, 388)
        Me.MyLabel26.Name = "MyLabel26"
        Me.MyLabel26.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel26.TabIndex = 208
        Me.MyLabel26.Text = "Milk Fat(%)"
        '
        'txtMilkQty
        '
        Me.txtMilkQty.BackColor = System.Drawing.Color.White
        Me.txtMilkQty.CalculationExpression = Nothing
        Me.txtMilkQty.DecimalPlaces = 2
        Me.txtMilkQty.FieldCode = Nothing
        Me.txtMilkQty.FieldDesc = Nothing
        Me.txtMilkQty.FieldMaxLength = 0
        Me.txtMilkQty.FieldName = Nothing
        Me.txtMilkQty.isCalculatedField = False
        Me.txtMilkQty.IsSourceFromTable = False
        Me.txtMilkQty.IsSourceFromValueList = False
        Me.txtMilkQty.IsUnique = False
        Me.txtMilkQty.Location = New System.Drawing.Point(374, 386)
        Me.txtMilkQty.MendatroryField = False
        Me.txtMilkQty.MyLinkLable1 = Nothing
        Me.txtMilkQty.MyLinkLable2 = Nothing
        Me.txtMilkQty.Name = "txtMilkQty"
        Me.txtMilkQty.ReferenceFieldDesc = Nothing
        Me.txtMilkQty.ReferenceFieldName = Nothing
        Me.txtMilkQty.ReferenceTableName = Nothing
        Me.txtMilkQty.Size = New System.Drawing.Size(143, 20)
        Me.txtMilkQty.TabIndex = 207
        Me.txtMilkQty.Text = "0"
        Me.txtMilkQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMilkQty.Value = 0.0R
        '
        'MyLabel25
        '
        Me.MyLabel25.FieldName = Nothing
        Me.MyLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel25.Location = New System.Drawing.Point(224, 388)
        Me.MyLabel25.Name = "MyLabel25"
        Me.MyLabel25.Size = New System.Drawing.Size(135, 16)
        Me.MyLabel25.TabIndex = 206
        Me.MyLabel25.Text = "Milk Quantity/per Day(Ltr)"
        '
        'lblFather
        '
        Me.lblFather.AutoSize = False
        Me.lblFather.BorderVisible = True
        Me.lblFather.FieldName = Nothing
        Me.lblFather.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFather.Location = New System.Drawing.Point(330, 56)
        Me.lblFather.Name = "lblFather"
        Me.lblFather.Size = New System.Drawing.Size(368, 18)
        Me.lblFather.TabIndex = 203
        Me.lblFather.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFather.TextWrap = False
        '
        'txtFather
        '
        Me.txtFather.CalculationExpression = Nothing
        Me.txtFather.FieldCode = Nothing
        Me.txtFather.FieldDesc = Nothing
        Me.txtFather.FieldMaxLength = 0
        Me.txtFather.FieldName = Nothing
        Me.txtFather.isCalculatedField = False
        Me.txtFather.IsSourceFromTable = False
        Me.txtFather.IsSourceFromValueList = False
        Me.txtFather.IsUnique = False
        Me.txtFather.Location = New System.Drawing.Point(102, 54)
        Me.txtFather.MendatroryField = False
        Me.txtFather.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFather.MyLinkLable1 = Nothing
        Me.txtFather.MyLinkLable2 = Me.lblFather
        Me.txtFather.MyReadOnly = False
        Me.txtFather.MyShowMasterFormButton = False
        Me.txtFather.Name = "txtFather"
        Me.txtFather.ReferenceFieldDesc = Nothing
        Me.txtFather.ReferenceFieldName = Nothing
        Me.txtFather.ReferenceTableName = Nothing
        Me.txtFather.Size = New System.Drawing.Size(223, 18)
        Me.txtFather.TabIndex = 202
        Me.txtFather.Value = ""
        '
        'MyLabel23
        '
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel23.Location = New System.Drawing.Point(9, 54)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel23.TabIndex = 201
        Me.MyLabel23.Text = "Cattle Father Id"
        '
        'lblMother
        '
        Me.lblMother.AutoSize = False
        Me.lblMother.BorderVisible = True
        Me.lblMother.FieldName = Nothing
        Me.lblMother.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMother.Location = New System.Drawing.Point(330, 34)
        Me.lblMother.Name = "lblMother"
        Me.lblMother.Size = New System.Drawing.Size(368, 18)
        Me.lblMother.TabIndex = 200
        Me.lblMother.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMother.TextWrap = False
        '
        'txtMother
        '
        Me.txtMother.CalculationExpression = Nothing
        Me.txtMother.FieldCode = Nothing
        Me.txtMother.FieldDesc = Nothing
        Me.txtMother.FieldMaxLength = 0
        Me.txtMother.FieldName = Nothing
        Me.txtMother.isCalculatedField = False
        Me.txtMother.IsSourceFromTable = False
        Me.txtMother.IsSourceFromValueList = False
        Me.txtMother.IsUnique = False
        Me.txtMother.Location = New System.Drawing.Point(102, 32)
        Me.txtMother.MendatroryField = False
        Me.txtMother.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMother.MyLinkLable1 = Nothing
        Me.txtMother.MyLinkLable2 = Me.lblMother
        Me.txtMother.MyReadOnly = False
        Me.txtMother.MyShowMasterFormButton = False
        Me.txtMother.Name = "txtMother"
        Me.txtMother.ReferenceFieldDesc = Nothing
        Me.txtMother.ReferenceFieldName = Nothing
        Me.txtMother.ReferenceTableName = Nothing
        Me.txtMother.Size = New System.Drawing.Size(223, 18)
        Me.txtMother.TabIndex = 199
        Me.txtMother.Value = ""
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(9, 32)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel21.TabIndex = 198
        Me.MyLabel21.Text = "Cattle Mother Id"
        '
        'lblFarmar
        '
        Me.lblFarmar.AutoSize = False
        Me.lblFarmar.BorderVisible = True
        Me.lblFarmar.FieldName = Nothing
        Me.lblFarmar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFarmar.Location = New System.Drawing.Point(330, 77)
        Me.lblFarmar.Name = "lblFarmar"
        Me.lblFarmar.Size = New System.Drawing.Size(368, 18)
        Me.lblFarmar.TabIndex = 197
        Me.lblFarmar.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFarmar.TextWrap = False
        '
        'txtFarmer
        '
        Me.txtFarmer.CalculationExpression = Nothing
        Me.txtFarmer.FieldCode = Nothing
        Me.txtFarmer.FieldDesc = Nothing
        Me.txtFarmer.FieldMaxLength = 0
        Me.txtFarmer.FieldName = Nothing
        Me.txtFarmer.isCalculatedField = False
        Me.txtFarmer.IsSourceFromTable = False
        Me.txtFarmer.IsSourceFromValueList = False
        Me.txtFarmer.IsUnique = False
        Me.txtFarmer.Location = New System.Drawing.Point(102, 77)
        Me.txtFarmer.MendatroryField = False
        Me.txtFarmer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFarmer.MyLinkLable1 = Nothing
        Me.txtFarmer.MyLinkLable2 = Me.lblFarmar
        Me.txtFarmer.MyReadOnly = False
        Me.txtFarmer.MyShowMasterFormButton = False
        Me.txtFarmer.Name = "txtFarmer"
        Me.txtFarmer.ReferenceFieldDesc = Nothing
        Me.txtFarmer.ReferenceFieldName = Nothing
        Me.txtFarmer.ReferenceTableName = Nothing
        Me.txtFarmer.Size = New System.Drawing.Size(223, 18)
        Me.txtFarmer.TabIndex = 196
        Me.txtFarmer.Value = ""
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(9, 78)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel19.TabIndex = 195
        Me.MyLabel19.Text = "Farmer Id"
        '
        'lblMCC
        '
        Me.lblMCC.AutoSize = False
        Me.lblMCC.BorderVisible = True
        Me.lblMCC.FieldName = Nothing
        Me.lblMCC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMCC.Location = New System.Drawing.Point(330, 186)
        Me.lblMCC.Name = "lblMCC"
        Me.lblMCC.Size = New System.Drawing.Size(368, 18)
        Me.lblMCC.TabIndex = 194
        Me.lblMCC.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMCC.TextWrap = False
        '
        'txtMCC
        '
        Me.txtMCC.CalculationExpression = Nothing
        Me.txtMCC.FieldCode = Nothing
        Me.txtMCC.FieldDesc = Nothing
        Me.txtMCC.FieldMaxLength = 0
        Me.txtMCC.FieldName = Nothing
        Me.txtMCC.isCalculatedField = False
        Me.txtMCC.IsSourceFromTable = False
        Me.txtMCC.IsSourceFromValueList = False
        Me.txtMCC.IsUnique = False
        Me.txtMCC.Location = New System.Drawing.Point(102, 188)
        Me.txtMCC.MendatroryField = False
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Nothing
        Me.txtMCC.MyLinkLable2 = Me.lblMCC
        Me.txtMCC.MyReadOnly = True
        Me.txtMCC.MyShowMasterFormButton = False
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.ReferenceFieldDesc = Nothing
        Me.txtMCC.ReferenceFieldName = Nothing
        Me.txtMCC.ReferenceTableName = Nothing
        Me.txtMCC.Size = New System.Drawing.Size(222, 18)
        Me.txtMCC.TabIndex = 193
        Me.txtMCC.Value = ""
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(9, 188)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel17.TabIndex = 192
        Me.MyLabel17.Text = "MCC Code"
        '
        'lblBranch
        '
        Me.lblBranch.AutoSize = False
        Me.lblBranch.BorderVisible = True
        Me.lblBranch.FieldName = Nothing
        Me.lblBranch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBranch.Location = New System.Drawing.Point(330, 300)
        Me.lblBranch.Name = "lblBranch"
        Me.lblBranch.Size = New System.Drawing.Size(367, 18)
        Me.lblBranch.TabIndex = 191
        Me.lblBranch.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBranch.TextWrap = False
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
        Me.txtBranch.Location = New System.Drawing.Point(101, 300)
        Me.txtBranch.MendatroryField = False
        Me.txtBranch.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBranch.MyLinkLable1 = Nothing
        Me.txtBranch.MyLinkLable2 = Me.lblBranch
        Me.txtBranch.MyReadOnly = False
        Me.txtBranch.MyShowMasterFormButton = False
        Me.txtBranch.Name = "txtBranch"
        Me.txtBranch.ReferenceFieldDesc = Nothing
        Me.txtBranch.ReferenceFieldName = Nothing
        Me.txtBranch.ReferenceTableName = Nothing
        Me.txtBranch.Size = New System.Drawing.Size(223, 18)
        Me.txtBranch.TabIndex = 190
        Me.txtBranch.Value = ""
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(9, 302)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(42, 16)
        Me.MyLabel15.TabIndex = 189
        Me.MyLabel15.Text = "Branch"
        '
        'lblArea
        '
        Me.lblArea.AutoSize = False
        Me.lblArea.BorderVisible = True
        Me.lblArea.FieldName = Nothing
        Me.lblArea.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArea.Location = New System.Drawing.Point(330, 278)
        Me.lblArea.Name = "lblArea"
        Me.lblArea.Size = New System.Drawing.Size(367, 18)
        Me.lblArea.TabIndex = 188
        Me.lblArea.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblArea.TextWrap = False
        '
        'txtArea
        '
        Me.txtArea.CalculationExpression = Nothing
        Me.txtArea.FieldCode = Nothing
        Me.txtArea.FieldDesc = Nothing
        Me.txtArea.FieldMaxLength = 0
        Me.txtArea.FieldName = Nothing
        Me.txtArea.isCalculatedField = False
        Me.txtArea.IsSourceFromTable = False
        Me.txtArea.IsSourceFromValueList = False
        Me.txtArea.IsUnique = False
        Me.txtArea.Location = New System.Drawing.Point(101, 278)
        Me.txtArea.MendatroryField = False
        Me.txtArea.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtArea.MyLinkLable1 = Nothing
        Me.txtArea.MyLinkLable2 = Me.lblArea
        Me.txtArea.MyReadOnly = False
        Me.txtArea.MyShowMasterFormButton = False
        Me.txtArea.Name = "txtArea"
        Me.txtArea.ReferenceFieldDesc = Nothing
        Me.txtArea.ReferenceFieldName = Nothing
        Me.txtArea.ReferenceTableName = Nothing
        Me.txtArea.Size = New System.Drawing.Size(223, 18)
        Me.txtArea.TabIndex = 187
        Me.txtArea.Value = ""
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(9, 280)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel13.TabIndex = 186
        Me.MyLabel13.Text = "Area"
        '
        'lblRegion
        '
        Me.lblRegion.AutoSize = False
        Me.lblRegion.BorderVisible = True
        Me.lblRegion.FieldName = Nothing
        Me.lblRegion.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegion.Location = New System.Drawing.Point(330, 256)
        Me.lblRegion.Name = "lblRegion"
        Me.lblRegion.Size = New System.Drawing.Size(367, 18)
        Me.lblRegion.TabIndex = 185
        Me.lblRegion.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblRegion.TextWrap = False
        '
        'txtRegion
        '
        Me.txtRegion.CalculationExpression = Nothing
        Me.txtRegion.FieldCode = Nothing
        Me.txtRegion.FieldDesc = Nothing
        Me.txtRegion.FieldMaxLength = 0
        Me.txtRegion.FieldName = Nothing
        Me.txtRegion.isCalculatedField = False
        Me.txtRegion.IsSourceFromTable = False
        Me.txtRegion.IsSourceFromValueList = False
        Me.txtRegion.IsUnique = False
        Me.txtRegion.Location = New System.Drawing.Point(101, 256)
        Me.txtRegion.MendatroryField = False
        Me.txtRegion.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegion.MyLinkLable1 = Nothing
        Me.txtRegion.MyLinkLable2 = Me.lblRegion
        Me.txtRegion.MyReadOnly = False
        Me.txtRegion.MyShowMasterFormButton = False
        Me.txtRegion.Name = "txtRegion"
        Me.txtRegion.ReferenceFieldDesc = Nothing
        Me.txtRegion.ReferenceFieldName = Nothing
        Me.txtRegion.ReferenceTableName = Nothing
        Me.txtRegion.Size = New System.Drawing.Size(223, 18)
        Me.txtRegion.TabIndex = 184
        Me.txtRegion.Value = ""
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(9, 258)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(42, 16)
        Me.MyLabel11.TabIndex = 183
        Me.MyLabel11.Text = "Region"
        '
        'lblZone
        '
        Me.lblZone.AutoSize = False
        Me.lblZone.BorderVisible = True
        Me.lblZone.FieldName = Nothing
        Me.lblZone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZone.Location = New System.Drawing.Point(330, 234)
        Me.lblZone.Name = "lblZone"
        Me.lblZone.Size = New System.Drawing.Size(368, 18)
        Me.lblZone.TabIndex = 182
        Me.lblZone.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblZone.TextWrap = False
        '
        'txtZone
        '
        Me.txtZone.CalculationExpression = Nothing
        Me.txtZone.FieldCode = Nothing
        Me.txtZone.FieldDesc = Nothing
        Me.txtZone.FieldMaxLength = 0
        Me.txtZone.FieldName = Nothing
        Me.txtZone.isCalculatedField = False
        Me.txtZone.IsSourceFromTable = False
        Me.txtZone.IsSourceFromValueList = False
        Me.txtZone.IsUnique = False
        Me.txtZone.Location = New System.Drawing.Point(101, 234)
        Me.txtZone.MendatroryField = False
        Me.txtZone.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtZone.MyLinkLable1 = Nothing
        Me.txtZone.MyLinkLable2 = Me.lblZone
        Me.txtZone.MyReadOnly = False
        Me.txtZone.MyShowMasterFormButton = False
        Me.txtZone.Name = "txtZone"
        Me.txtZone.ReferenceFieldDesc = Nothing
        Me.txtZone.ReferenceFieldName = Nothing
        Me.txtZone.ReferenceTableName = Nothing
        Me.txtZone.Size = New System.Drawing.Size(223, 18)
        Me.txtZone.TabIndex = 181
        Me.txtZone.Value = ""
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(9, 236)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel9.TabIndex = 180
        Me.MyLabel9.Text = "Zone"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(9, 213)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(66, 16)
        Me.MyLabel7.TabIndex = 177
        Me.MyLabel7.Text = "Head Office"
        '
        'lblBreed
        '
        Me.lblBreed.AutoSize = False
        Me.lblBreed.BorderVisible = True
        Me.lblBreed.FieldName = Nothing
        Me.lblBreed.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBreed.Location = New System.Drawing.Point(330, 144)
        Me.lblBreed.Name = "lblBreed"
        Me.lblBreed.Size = New System.Drawing.Size(368, 18)
        Me.lblBreed.TabIndex = 170
        Me.lblBreed.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBreed.TextWrap = False
        '
        'txtBreed
        '
        Me.txtBreed.CalculationExpression = Nothing
        Me.txtBreed.FieldCode = Nothing
        Me.txtBreed.FieldDesc = Nothing
        Me.txtBreed.FieldMaxLength = 0
        Me.txtBreed.FieldName = Nothing
        Me.txtBreed.isCalculatedField = False
        Me.txtBreed.IsSourceFromTable = False
        Me.txtBreed.IsSourceFromValueList = False
        Me.txtBreed.IsUnique = False
        Me.txtBreed.Location = New System.Drawing.Point(102, 144)
        Me.txtBreed.MendatroryField = True
        Me.txtBreed.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBreed.MyLinkLable1 = Nothing
        Me.txtBreed.MyLinkLable2 = Me.lblBreed
        Me.txtBreed.MyReadOnly = False
        Me.txtBreed.MyShowMasterFormButton = False
        Me.txtBreed.Name = "txtBreed"
        Me.txtBreed.ReferenceFieldDesc = Nothing
        Me.txtBreed.ReferenceFieldName = Nothing
        Me.txtBreed.ReferenceTableName = Nothing
        Me.txtBreed.Size = New System.Drawing.Size(223, 18)
        Me.txtBreed.TabIndex = 169
        Me.txtBreed.Value = ""
        '
        'lblCattleType
        '
        Me.lblCattleType.AutoSize = False
        Me.lblCattleType.BorderVisible = True
        Me.lblCattleType.FieldName = Nothing
        Me.lblCattleType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCattleType.Location = New System.Drawing.Point(330, 122)
        Me.lblCattleType.Name = "lblCattleType"
        Me.lblCattleType.Size = New System.Drawing.Size(368, 18)
        Me.lblCattleType.TabIndex = 168
        Me.lblCattleType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCattleType.TextWrap = False
        '
        'txtCattleType
        '
        Me.txtCattleType.CalculationExpression = Nothing
        Me.txtCattleType.FieldCode = Nothing
        Me.txtCattleType.FieldDesc = Nothing
        Me.txtCattleType.FieldMaxLength = 0
        Me.txtCattleType.FieldName = Nothing
        Me.txtCattleType.isCalculatedField = False
        Me.txtCattleType.IsSourceFromTable = False
        Me.txtCattleType.IsSourceFromValueList = False
        Me.txtCattleType.IsUnique = False
        Me.txtCattleType.Location = New System.Drawing.Point(102, 122)
        Me.txtCattleType.MendatroryField = True
        Me.txtCattleType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCattleType.MyLinkLable1 = Nothing
        Me.txtCattleType.MyLinkLable2 = Me.lblCattleType
        Me.txtCattleType.MyReadOnly = False
        Me.txtCattleType.MyShowMasterFormButton = False
        Me.txtCattleType.Name = "txtCattleType"
        Me.txtCattleType.ReferenceFieldDesc = Nothing
        Me.txtCattleType.ReferenceFieldName = Nothing
        Me.txtCattleType.ReferenceTableName = Nothing
        Me.txtCattleType.Size = New System.Drawing.Size(223, 18)
        Me.txtCattleType.TabIndex = 167
        Me.txtCattleType.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(9, 144)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel1.TabIndex = 166
        Me.MyLabel1.Text = "Breed Type"
        '
        'lblType
        '
        Me.lblType.FieldName = Nothing
        Me.lblType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.Location = New System.Drawing.Point(9, 122)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(64, 16)
        Me.lblType.TabIndex = 165
        Me.lblType.Text = "Cattle Type"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(10, 6)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel2.TabIndex = 241
        Me.MyLabel2.Text = "Registration No"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(428, 5)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 160
        Me.btnNew.Text = " "
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
        Me.txtRegistrationDate.Location = New System.Drawing.Point(487, 7)
        Me.txtRegistrationDate.MendatroryField = False
        Me.txtRegistrationDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRegistrationDate.MyLinkLable1 = Me.RadLabel4
        Me.txtRegistrationDate.MyLinkLable2 = Nothing
        Me.txtRegistrationDate.Name = "txtRegistrationDate"
        Me.txtRegistrationDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRegistrationDate.ReferenceFieldDesc = Nothing
        Me.txtRegistrationDate.ReferenceFieldName = Nothing
        Me.txtRegistrationDate.ReferenceTableName = Nothing
        Me.txtRegistrationDate.Size = New System.Drawing.Size(81, 18)
        Me.txtRegistrationDate.TabIndex = 162
        Me.txtRegistrationDate.TabStop = False
        Me.txtRegistrationDate.Text = "13/06/2011"
        Me.txtRegistrationDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(455, 8)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 163
        Me.RadLabel4.Text = "Date"
        '
        'txtRegistrationNo
        '
        Me.txtRegistrationNo.FieldName = Nothing
        Me.txtRegistrationNo.Location = New System.Drawing.Point(105, 4)
        Me.txtRegistrationNo.MendatroryField = True
        Me.txtRegistrationNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRegistrationNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtRegistrationNo.MyLinkLable1 = Me.lblItemCategoryCode
        Me.txtRegistrationNo.MyLinkLable2 = Nothing
        Me.txtRegistrationNo.MyMaxLength = 12
        Me.txtRegistrationNo.MyReadOnly = False
        Me.txtRegistrationNo.Name = "txtRegistrationNo"
        Me.txtRegistrationNo.Size = New System.Drawing.Size(321, 21)
        Me.txtRegistrationNo.TabIndex = 240
        Me.txtRegistrationNo.Value = ""
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(11, 28)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 235
        Me.lblDescription.Text = "Description"
        '
        'cboCattleStatus
        '
        Me.cboCattleStatus.AutoCompleteDisplayMember = Nothing
        Me.cboCattleStatus.AutoCompleteValueMember = Nothing
        Me.cboCattleStatus.CalculationExpression = Nothing
        Me.cboCattleStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboCattleStatus.FieldCode = Nothing
        Me.cboCattleStatus.FieldDesc = Nothing
        Me.cboCattleStatus.FieldMaxLength = 0
        Me.cboCattleStatus.FieldName = Nothing
        Me.cboCattleStatus.isCalculatedField = False
        Me.cboCattleStatus.IsSourceFromTable = False
        Me.cboCattleStatus.IsSourceFromValueList = False
        Me.cboCattleStatus.IsUnique = False
        RadListDataItem1.Text = "Select"
        RadListDataItem2.Text = "Parent"
        RadListDataItem3.Text = "Child"
        Me.cboCattleStatus.Items.Add(RadListDataItem1)
        Me.cboCattleStatus.Items.Add(RadListDataItem2)
        Me.cboCattleStatus.Items.Add(RadListDataItem3)
        Me.cboCattleStatus.Location = New System.Drawing.Point(683, 52)
        Me.cboCattleStatus.MendatroryField = True
        Me.cboCattleStatus.MyLinkLable1 = Me.lblType
        Me.cboCattleStatus.MyLinkLable2 = Nothing
        Me.cboCattleStatus.Name = "cboCattleStatus"
        Me.cboCattleStatus.ReferenceFieldDesc = Nothing
        Me.cboCattleStatus.ReferenceFieldName = Nothing
        Me.cboCattleStatus.ReferenceTableName = Nothing
        Me.cboCattleStatus.Size = New System.Drawing.Size(220, 20)
        Me.cboCattleStatus.TabIndex = 244
        '
        'txtDescription
        '
        Me.txtDescription.AutoSize = False
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(105, 27)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Nothing
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(464, 21)
        Me.txtDescription.TabIndex = 236
        Me.txtDescription.Text = " "
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(586, 52)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel5.TabIndex = 243
        Me.MyLabel5.Text = "Cattle Status"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(586, 32)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(38, 16)
        Me.MyLabel6.TabIndex = 174
        Me.MyLabel6.Text = "Tag Id"
        '
        'txtTagId
        '
        Me.txtTagId.AutoSize = False
        Me.txtTagId.CalculationExpression = Nothing
        Me.txtTagId.FieldCode = Nothing
        Me.txtTagId.FieldDesc = Nothing
        Me.txtTagId.FieldMaxLength = 0
        Me.txtTagId.FieldName = Nothing
        Me.txtTagId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTagId.isCalculatedField = False
        Me.txtTagId.IsSourceFromTable = False
        Me.txtTagId.IsSourceFromValueList = False
        Me.txtTagId.IsUnique = False
        Me.txtTagId.Location = New System.Drawing.Point(683, 29)
        Me.txtTagId.MaxLength = 50
        Me.txtTagId.MendatroryField = False
        Me.txtTagId.Multiline = True
        Me.txtTagId.MyLinkLable1 = Nothing
        Me.txtTagId.MyLinkLable2 = Nothing
        Me.txtTagId.Name = "txtTagId"
        Me.txtTagId.ReadOnly = True
        Me.txtTagId.ReferenceFieldDesc = Nothing
        Me.txtTagId.ReferenceFieldName = Nothing
        Me.txtTagId.ReferenceTableName = Nothing
        Me.txtTagId.Size = New System.Drawing.Size(220, 21)
        Me.txtTagId.TabIndex = 227
        Me.txtTagId.Text = " "
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel24.Location = New System.Drawing.Point(12, 50)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel24.TabIndex = 225
        Me.MyLabel24.Text = "Date Of Birth"
        '
        'dtpDOB
        '
        Me.dtpDOB.CalculationExpression = Nothing
        Me.dtpDOB.CustomFormat = "dd/MM/yyyy"
        Me.dtpDOB.FieldCode = Nothing
        Me.dtpDOB.FieldDesc = Nothing
        Me.dtpDOB.FieldMaxLength = 0
        Me.dtpDOB.FieldName = Nothing
        Me.dtpDOB.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDOB.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDOB.isCalculatedField = False
        Me.dtpDOB.IsSourceFromTable = False
        Me.dtpDOB.IsSourceFromValueList = False
        Me.dtpDOB.IsUnique = False
        Me.dtpDOB.Location = New System.Drawing.Point(105, 49)
        Me.dtpDOB.MendatroryField = False
        Me.dtpDOB.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDOB.MyLinkLable1 = Me.MyLabel24
        Me.dtpDOB.MyLinkLable2 = Nothing
        Me.dtpDOB.Name = "dtpDOB"
        Me.dtpDOB.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDOB.ReferenceFieldDesc = Nothing
        Me.dtpDOB.ReferenceFieldName = Nothing
        Me.dtpDOB.ReferenceTableName = Nothing
        Me.dtpDOB.Size = New System.Drawing.Size(82, 18)
        Me.dtpDOB.TabIndex = 224
        Me.dtpDOB.TabStop = False
        Me.dtpDOB.Text = "13/06/2011"
        Me.dtpDOB.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel29
        '
        Me.MyLabel29.FieldName = Nothing
        Me.MyLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel29.Location = New System.Drawing.Point(430, 52)
        Me.MyLabel29.Name = "MyLabel29"
        Me.MyLabel29.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel29.TabIndex = 214
        Me.MyLabel29.Text = "Gender"
        '
        'cboGender
        '
        Me.cboGender.AutoCompleteDisplayMember = Nothing
        Me.cboGender.AutoCompleteValueMember = Nothing
        Me.cboGender.CalculationExpression = Nothing
        Me.cboGender.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboGender.FieldCode = Nothing
        Me.cboGender.FieldDesc = Nothing
        Me.cboGender.FieldMaxLength = 0
        Me.cboGender.FieldName = Nothing
        Me.cboGender.isCalculatedField = False
        Me.cboGender.IsSourceFromTable = False
        Me.cboGender.IsSourceFromValueList = False
        Me.cboGender.IsUnique = False
        RadListDataItem4.Text = "Select"
        RadListDataItem5.Text = "Full"
        RadListDataItem6.Text = "Empty"
        Me.cboGender.Items.Add(RadListDataItem4)
        Me.cboGender.Items.Add(RadListDataItem5)
        Me.cboGender.Items.Add(RadListDataItem6)
        Me.cboGender.Location = New System.Drawing.Point(479, 50)
        Me.cboGender.MendatroryField = True
        Me.cboGender.MyLinkLable1 = Me.lblType
        Me.cboGender.MyLinkLable2 = Nothing
        Me.cboGender.Name = "cboGender"
        Me.cboGender.ReferenceFieldDesc = Nothing
        Me.cboGender.ReferenceFieldName = Nothing
        Me.cboGender.ReferenceTableName = Nothing
        Me.cboGender.Size = New System.Drawing.Size(89, 20)
        Me.cboGender.TabIndex = 215
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(6, 18)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 21)
        Me.btnsave.TabIndex = 3
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(910, 18)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 5
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(76, 18)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 4
        Me.btndelete.Text = "Delete"
        '
        'FrmCattleMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(982, 698)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmCattleMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Cattle Master"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCattleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCattleInAge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtRegistrationCharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dtpInsuranceDateFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpInsuranceTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInsuranceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPMCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHeadOffice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCattleColor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNDDBCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMilkFat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMilkQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFather, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMother, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFarmar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBranch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRegion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblZone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBreed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCattleType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRegistrationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboCattleStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTagId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDOB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboGender, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rmi As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblItemCategoryCode As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents txtRegistrationDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblType As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents lblBreed As common.Controls.MyLabel
    Friend WithEvents txtBreed As common.UserControls.txtFinder
    Friend WithEvents lblCattleType As common.Controls.MyLabel
    Friend WithEvents txtCattleType As common.UserControls.txtFinder
    Friend WithEvents lblArea As common.Controls.MyLabel
    Friend WithEvents txtArea As common.UserControls.txtFinder
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents lblRegion As common.Controls.MyLabel
    Friend WithEvents txtRegion As common.UserControls.txtFinder
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents lblZone As common.Controls.MyLabel
    Friend WithEvents txtZone As common.UserControls.txtFinder
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel25 As common.Controls.MyLabel
    Friend WithEvents lblFather As common.Controls.MyLabel
    Friend WithEvents txtFather As common.UserControls.txtFinder
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents lblMother As common.Controls.MyLabel
    Friend WithEvents txtMother As common.UserControls.txtFinder
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents lblFarmar As common.Controls.MyLabel
    Friend WithEvents txtFarmer As common.UserControls.txtFinder
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents lblMCC As common.Controls.MyLabel
    Friend WithEvents txtMCC As common.UserControls.txtFinder
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents lblBranch As common.Controls.MyLabel
    Friend WithEvents txtBranch As common.UserControls.txtFinder
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents txtMilkFat As common.MyNumBox
    Friend WithEvents MyLabel26 As common.Controls.MyLabel
    Friend WithEvents txtMilkQty As common.MyNumBox
    Friend WithEvents MyLabel29 As common.Controls.MyLabel
    Friend WithEvents MyLabel30 As common.Controls.MyLabel
    Friend WithEvents cboGender As common.Controls.MyComboBox
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents dtpDOB As common.Controls.MyDateTimePicker
    Friend WithEvents rdbChildCattleType As System.Windows.Forms.RadioButton
    Friend WithEvents rdbParentType As System.Windows.Forms.RadioButton
    Friend WithEvents txtTagId As common.Controls.MyTextBox
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblCattleColor As common.Controls.MyLabel
    Friend WithEvents txtCattleColor As common.UserControls.txtFinder
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtNDDBCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtRegistrationNo As common.UserControls.txtNavigator
    Friend WithEvents lblPMCCode As common.Controls.MyLabel
    Friend WithEvents txtPMCCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtHeadOffice As common.Controls.MyTextBox
    Friend WithEvents lblNDDBCode As common.Controls.MyLabel
    Friend WithEvents cboCattleStatus As common.Controls.MyComboBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel32 As common.Controls.MyLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpInsuranceDateFrom As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents dtpInsuranceTo As common.Controls.MyDateTimePicker
    Friend WithEvents txtInsuranceNo As common.Controls.MyTextBox
    Friend WithEvents txtCattleInAge As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtCattleCode As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents txtRegistrationCharge As common.MyNumBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
End Class

