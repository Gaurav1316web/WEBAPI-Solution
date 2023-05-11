<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBullMaster
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBullMaster))
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtBreedInfo = New common.Controls.MyTextBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtSiteId = New common.Controls.MyTextBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.cboBullStatus = New common.Controls.MyComboBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtBreedDetails = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtDamsYield = New common.Controls.MyTextBox()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.dtpDOB = New common.Controls.MyDateTimePicker()
        Me.txtNoOfStraws = New common.MyNumBox()
        Me.MyLabel26 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtBullProfileId = New common.Controls.MyTextBox()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblCattleType = New common.Controls.MyLabel()
        Me.txtCattleType = New common.UserControls.txtFinder()
        Me.lblItemCategoryCode = New common.Controls.MyLabel()
        Me.dtpBullDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtBullMaster = New common.UserControls.txtNavigator()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtBreedInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSiteId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboBullStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBreedDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDamsYield, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDOB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoOfStraws, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBullProfileId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCattleType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpBullDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(876, 20)
        Me.RadMenu1.TabIndex = 5
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "rdmenufile"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Import"
        Me.RadMenuItem2.AccessibleName = "Import"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Import"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Export"
        Me.RadMenuItem3.AccessibleName = "Export"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Export"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "Exit"
        Me.RadMenuItem4.AccessibleName = "Exit"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Exit"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(876, 512)
        Me.SplitContainer1.SplitterDistance = 482
        Me.SplitContainer1.TabIndex = 6
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtBreedInfo)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox1.Controls.Add(Me.txtSiteId)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox1.Controls.Add(Me.cboBullStatus)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.txtBreedDetails)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtDamsYield)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel24)
        Me.RadGroupBox1.Controls.Add(Me.dtpDOB)
        Me.RadGroupBox1.Controls.Add(Me.txtNoOfStraws)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel26)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtBullProfileId)
        Me.RadGroupBox1.Controls.Add(Me.lblDescription)
        Me.RadGroupBox1.Controls.Add(Me.txtDescription)
        Me.RadGroupBox1.Controls.Add(Me.lblCattleType)
        Me.RadGroupBox1.Controls.Add(Me.txtCattleType)
        Me.RadGroupBox1.Controls.Add(Me.lblItemCategoryCode)
        Me.RadGroupBox1.Controls.Add(Me.dtpBullDate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtBullMaster)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(876, 482)
        Me.RadGroupBox1.TabIndex = 0
        '
        'txtBreedInfo
        '
        Me.txtBreedInfo.AutoSize = False
        Me.txtBreedInfo.CalculationExpression = Nothing
        Me.txtBreedInfo.FieldCode = Nothing
        Me.txtBreedInfo.FieldDesc = Nothing
        Me.txtBreedInfo.FieldMaxLength = 0
        Me.txtBreedInfo.FieldName = Nothing
        Me.txtBreedInfo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBreedInfo.isCalculatedField = False
        Me.txtBreedInfo.IsSourceFromTable = False
        Me.txtBreedInfo.IsSourceFromValueList = False
        Me.txtBreedInfo.IsUnique = False
        Me.txtBreedInfo.Location = New System.Drawing.Point(106, 198)
        Me.txtBreedInfo.MaxLength = 50
        Me.txtBreedInfo.MendatroryField = False
        Me.txtBreedInfo.Multiline = True
        Me.txtBreedInfo.MyLinkLable1 = Nothing
        Me.txtBreedInfo.MyLinkLable2 = Nothing
        Me.txtBreedInfo.Name = "txtBreedInfo"
        Me.txtBreedInfo.ReferenceFieldDesc = Nothing
        Me.txtBreedInfo.ReferenceFieldName = Nothing
        Me.txtBreedInfo.ReferenceTableName = Nothing
        Me.txtBreedInfo.Size = New System.Drawing.Size(464, 21)
        Me.txtBreedInfo.TabIndex = 304
        Me.txtBreedInfo.Text = " "
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(13, 203)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel7.TabIndex = 302
        Me.MyLabel7.Text = "Breed Info"
        '
        'txtSiteId
        '
        Me.txtSiteId.AutoSize = False
        Me.txtSiteId.CalculationExpression = Nothing
        Me.txtSiteId.FieldCode = Nothing
        Me.txtSiteId.FieldDesc = Nothing
        Me.txtSiteId.FieldMaxLength = 0
        Me.txtSiteId.FieldName = Nothing
        Me.txtSiteId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSiteId.isCalculatedField = False
        Me.txtSiteId.IsSourceFromTable = False
        Me.txtSiteId.IsSourceFromValueList = False
        Me.txtSiteId.IsUnique = False
        Me.txtSiteId.Location = New System.Drawing.Point(397, 133)
        Me.txtSiteId.MaxLength = 50
        Me.txtSiteId.MendatroryField = False
        Me.txtSiteId.Multiline = True
        Me.txtSiteId.MyLinkLable1 = Nothing
        Me.txtSiteId.MyLinkLable2 = Nothing
        Me.txtSiteId.Name = "txtSiteId"
        Me.txtSiteId.ReferenceFieldDesc = Nothing
        Me.txtSiteId.ReferenceFieldName = Nothing
        Me.txtSiteId.ReferenceTableName = Nothing
        Me.txtSiteId.Size = New System.Drawing.Size(173, 21)
        Me.txtSiteId.TabIndex = 301
        Me.txtSiteId.Text = " "
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(308, 135)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel6.TabIndex = 300
        Me.MyLabel6.Text = "Site Id "
        '
        'cboBullStatus
        '
        Me.cboBullStatus.AutoCompleteDisplayMember = Nothing
        Me.cboBullStatus.AutoCompleteValueMember = Nothing
        Me.cboBullStatus.CalculationExpression = Nothing
        Me.cboBullStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboBullStatus.FieldCode = Nothing
        Me.cboBullStatus.FieldDesc = Nothing
        Me.cboBullStatus.FieldMaxLength = 0
        Me.cboBullStatus.FieldName = Nothing
        Me.cboBullStatus.isCalculatedField = False
        Me.cboBullStatus.IsSourceFromTable = False
        Me.cboBullStatus.IsSourceFromValueList = False
        Me.cboBullStatus.IsUnique = False
        RadListDataItem1.Text = "Select"
        RadListDataItem2.Text = "Parent"
        RadListDataItem3.Text = "Child"
        Me.cboBullStatus.Items.Add(RadListDataItem1)
        Me.cboBullStatus.Items.Add(RadListDataItem2)
        Me.cboBullStatus.Items.Add(RadListDataItem3)
        Me.cboBullStatus.Location = New System.Drawing.Point(105, 133)
        Me.cboBullStatus.MendatroryField = True
        Me.cboBullStatus.MyLinkLable1 = Nothing
        Me.cboBullStatus.MyLinkLable2 = Nothing
        Me.cboBullStatus.Name = "cboBullStatus"
        Me.cboBullStatus.ReferenceFieldDesc = Nothing
        Me.cboBullStatus.ReferenceFieldName = Nothing
        Me.cboBullStatus.ReferenceTableName = Nothing
        Me.cboBullStatus.Size = New System.Drawing.Size(201, 20)
        Me.cboBullStatus.TabIndex = 299
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(13, 137)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel5.TabIndex = 298
        Me.MyLabel5.Text = "Bull Status"
        '
        'txtBreedDetails
        '
        Me.txtBreedDetails.AutoSize = False
        Me.txtBreedDetails.CalculationExpression = Nothing
        Me.txtBreedDetails.FieldCode = Nothing
        Me.txtBreedDetails.FieldDesc = Nothing
        Me.txtBreedDetails.FieldMaxLength = 0
        Me.txtBreedDetails.FieldName = Nothing
        Me.txtBreedDetails.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBreedDetails.isCalculatedField = False
        Me.txtBreedDetails.IsSourceFromTable = False
        Me.txtBreedDetails.IsSourceFromValueList = False
        Me.txtBreedDetails.IsUnique = False
        Me.txtBreedDetails.Location = New System.Drawing.Point(106, 177)
        Me.txtBreedDetails.MaxLength = 50
        Me.txtBreedDetails.MendatroryField = False
        Me.txtBreedDetails.Multiline = True
        Me.txtBreedDetails.MyLinkLable1 = Nothing
        Me.txtBreedDetails.MyLinkLable2 = Nothing
        Me.txtBreedDetails.Name = "txtBreedDetails"
        Me.txtBreedDetails.ReferenceFieldDesc = Nothing
        Me.txtBreedDetails.ReferenceFieldName = Nothing
        Me.txtBreedDetails.ReferenceTableName = Nothing
        Me.txtBreedDetails.Size = New System.Drawing.Size(464, 21)
        Me.txtBreedDetails.TabIndex = 297
        Me.txtBreedDetails.Text = " "
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(13, 179)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel4.TabIndex = 296
        Me.MyLabel4.Text = "Breed Details"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(13, 157)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(64, 16)
        Me.MyLabel3.TabIndex = 294
        Me.MyLabel3.Text = "Dams Yield"
        '
        'txtDamsYield
        '
        Me.txtDamsYield.AutoSize = False
        Me.txtDamsYield.CalculationExpression = Nothing
        Me.txtDamsYield.FieldCode = Nothing
        Me.txtDamsYield.FieldDesc = Nothing
        Me.txtDamsYield.FieldMaxLength = 0
        Me.txtDamsYield.FieldName = Nothing
        Me.txtDamsYield.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDamsYield.isCalculatedField = False
        Me.txtDamsYield.IsSourceFromTable = False
        Me.txtDamsYield.IsSourceFromValueList = False
        Me.txtDamsYield.IsUnique = False
        Me.txtDamsYield.Location = New System.Drawing.Point(106, 153)
        Me.txtDamsYield.MaxLength = 50
        Me.txtDamsYield.MendatroryField = False
        Me.txtDamsYield.Multiline = True
        Me.txtDamsYield.MyLinkLable1 = Nothing
        Me.txtDamsYield.MyLinkLable2 = Nothing
        Me.txtDamsYield.Name = "txtDamsYield"
        Me.txtDamsYield.ReferenceFieldDesc = Nothing
        Me.txtDamsYield.ReferenceFieldName = Nothing
        Me.txtDamsYield.ReferenceTableName = Nothing
        Me.txtDamsYield.Size = New System.Drawing.Size(200, 21)
        Me.txtDamsYield.TabIndex = 295
        Me.txtDamsYield.Text = " "
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel24.Location = New System.Drawing.Point(307, 115)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel24.TabIndex = 293
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
        Me.dtpDOB.Location = New System.Drawing.Point(397, 114)
        Me.dtpDOB.MendatroryField = False
        Me.dtpDOB.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDOB.MyLinkLable1 = Me.MyLabel24
        Me.dtpDOB.MyLinkLable2 = Nothing
        Me.dtpDOB.Name = "dtpDOB"
        Me.dtpDOB.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDOB.ReferenceFieldDesc = Nothing
        Me.dtpDOB.ReferenceFieldName = Nothing
        Me.dtpDOB.ReferenceTableName = Nothing
        Me.dtpDOB.Size = New System.Drawing.Size(99, 18)
        Me.dtpDOB.TabIndex = 292
        Me.dtpDOB.TabStop = False
        Me.dtpDOB.Text = "13/06/2011"
        Me.dtpDOB.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtNoOfStraws
        '
        Me.txtNoOfStraws.BackColor = System.Drawing.Color.White
        Me.txtNoOfStraws.CalculationExpression = Nothing
        Me.txtNoOfStraws.DecimalPlaces = 2
        Me.txtNoOfStraws.FieldCode = Nothing
        Me.txtNoOfStraws.FieldDesc = Nothing
        Me.txtNoOfStraws.FieldMaxLength = 0
        Me.txtNoOfStraws.FieldName = Nothing
        Me.txtNoOfStraws.isCalculatedField = False
        Me.txtNoOfStraws.IsSourceFromTable = False
        Me.txtNoOfStraws.IsSourceFromValueList = False
        Me.txtNoOfStraws.IsUnique = False
        Me.txtNoOfStraws.Location = New System.Drawing.Point(105, 111)
        Me.txtNoOfStraws.MendatroryField = False
        Me.txtNoOfStraws.MyLinkLable1 = Nothing
        Me.txtNoOfStraws.MyLinkLable2 = Nothing
        Me.txtNoOfStraws.Name = "txtNoOfStraws"
        Me.txtNoOfStraws.ReferenceFieldDesc = Nothing
        Me.txtNoOfStraws.ReferenceFieldName = Nothing
        Me.txtNoOfStraws.ReferenceTableName = Nothing
        Me.txtNoOfStraws.Size = New System.Drawing.Size(201, 20)
        Me.txtNoOfStraws.TabIndex = 291
        Me.txtNoOfStraws.Text = "0"
        Me.txtNoOfStraws.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNoOfStraws.Value = 0.0R
        '
        'MyLabel26
        '
        Me.MyLabel26.FieldName = Nothing
        Me.MyLabel26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel26.Location = New System.Drawing.Point(13, 113)
        Me.MyLabel26.Name = "MyLabel26"
        Me.MyLabel26.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel26.TabIndex = 290
        Me.MyLabel26.Text = "No of Straws "
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(13, 65)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel1.TabIndex = 288
        Me.MyLabel1.Text = "Bull Pofile Id "
        '
        'txtBullProfileId
        '
        Me.txtBullProfileId.AutoSize = False
        Me.txtBullProfileId.CalculationExpression = Nothing
        Me.txtBullProfileId.FieldCode = Nothing
        Me.txtBullProfileId.FieldDesc = Nothing
        Me.txtBullProfileId.FieldMaxLength = 0
        Me.txtBullProfileId.FieldName = Nothing
        Me.txtBullProfileId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBullProfileId.isCalculatedField = False
        Me.txtBullProfileId.IsSourceFromTable = False
        Me.txtBullProfileId.IsSourceFromValueList = False
        Me.txtBullProfileId.IsUnique = False
        Me.txtBullProfileId.Location = New System.Drawing.Point(105, 64)
        Me.txtBullProfileId.MaxLength = 50
        Me.txtBullProfileId.MendatroryField = False
        Me.txtBullProfileId.Multiline = True
        Me.txtBullProfileId.MyLinkLable1 = Nothing
        Me.txtBullProfileId.MyLinkLable2 = Nothing
        Me.txtBullProfileId.Name = "txtBullProfileId"
        Me.txtBullProfileId.ReferenceFieldDesc = Nothing
        Me.txtBullProfileId.ReferenceFieldName = Nothing
        Me.txtBullProfileId.ReferenceTableName = Nothing
        Me.txtBullProfileId.Size = New System.Drawing.Size(323, 21)
        Me.txtBullProfileId.TabIndex = 289
        Me.txtBullProfileId.Text = " "
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(13, 43)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 286
        Me.lblDescription.Text = "Description"
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
        Me.txtDescription.Location = New System.Drawing.Point(105, 42)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Nothing
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(465, 21)
        Me.txtDescription.TabIndex = 287
        Me.txtDescription.Text = " "
        '
        'lblCattleType
        '
        Me.lblCattleType.AutoSize = False
        Me.lblCattleType.BorderVisible = True
        Me.lblCattleType.FieldName = Nothing
        Me.lblCattleType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCattleType.Location = New System.Drawing.Point(304, 91)
        Me.lblCattleType.Name = "lblCattleType"
        Me.lblCattleType.Size = New System.Drawing.Size(266, 18)
        Me.lblCattleType.TabIndex = 285
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
        Me.txtCattleType.Location = New System.Drawing.Point(105, 90)
        Me.txtCattleType.MendatroryField = True
        Me.txtCattleType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCattleType.MyLinkLable1 = Nothing
        Me.txtCattleType.MyLinkLable2 = Nothing
        Me.txtCattleType.MyReadOnly = False
        Me.txtCattleType.MyShowMasterFormButton = False
        Me.txtCattleType.Name = "txtCattleType"
        Me.txtCattleType.ReferenceFieldDesc = Nothing
        Me.txtCattleType.ReferenceFieldName = Nothing
        Me.txtCattleType.ReferenceTableName = Nothing
        Me.txtCattleType.Size = New System.Drawing.Size(201, 18)
        Me.txtCattleType.TabIndex = 284
        Me.txtCattleType.Value = ""
        '
        'lblItemCategoryCode
        '
        Me.lblItemCategoryCode.FieldName = Nothing
        Me.lblItemCategoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemCategoryCode.Location = New System.Drawing.Point(13, 91)
        Me.lblItemCategoryCode.Name = "lblItemCategoryCode"
        Me.lblItemCategoryCode.Size = New System.Drawing.Size(64, 16)
        Me.lblItemCategoryCode.TabIndex = 283
        Me.lblItemCategoryCode.Text = "Cattle Type"
        '
        'dtpBullDate
        '
        Me.dtpBullDate.CalculationExpression = Nothing
        Me.dtpBullDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpBullDate.FieldCode = Nothing
        Me.dtpBullDate.FieldDesc = Nothing
        Me.dtpBullDate.FieldMaxLength = 0
        Me.dtpBullDate.FieldName = Nothing
        Me.dtpBullDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpBullDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBullDate.isCalculatedField = False
        Me.dtpBullDate.IsSourceFromTable = False
        Me.dtpBullDate.IsSourceFromValueList = False
        Me.dtpBullDate.IsUnique = False
        Me.dtpBullDate.Location = New System.Drawing.Point(489, 20)
        Me.dtpBullDate.MendatroryField = False
        Me.dtpBullDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBullDate.MyLinkLable1 = Me.RadLabel4
        Me.dtpBullDate.MyLinkLable2 = Nothing
        Me.dtpBullDate.Name = "dtpBullDate"
        Me.dtpBullDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBullDate.ReferenceFieldDesc = Nothing
        Me.dtpBullDate.ReferenceFieldName = Nothing
        Me.dtpBullDate.ReferenceTableName = Nothing
        Me.dtpBullDate.Size = New System.Drawing.Size(81, 18)
        Me.dtpBullDate.TabIndex = 282
        Me.dtpBullDate.TabStop = False
        Me.dtpBullDate.Text = "13/06/2011"
        Me.dtpBullDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(457, 23)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 279
        Me.RadLabel4.Text = "Date"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(13, 21)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel2.TabIndex = 281
        Me.MyLabel2.Text = "Bull No"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(430, 20)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 278
        Me.btnNew.Text = " "
        '
        'txtBullMaster
        '
        Me.txtBullMaster.FieldName = Nothing
        Me.txtBullMaster.Location = New System.Drawing.Point(105, 19)
        Me.txtBullMaster.MendatroryField = True
        Me.txtBullMaster.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBullMaster.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtBullMaster.MyLinkLable1 = Nothing
        Me.txtBullMaster.MyLinkLable2 = Nothing
        Me.txtBullMaster.MyMaxLength = 16
        Me.txtBullMaster.MyReadOnly = False
        Me.txtBullMaster.Name = "txtBullMaster"
        Me.txtBullMaster.Size = New System.Drawing.Size(323, 21)
        Me.txtBullMaster.TabIndex = 280
        Me.txtBullMaster.Value = ""
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(3, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 21)
        Me.btnsave.TabIndex = 9
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(807, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 11
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(73, 3)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 10
        Me.btndelete.Text = "Delete"
        '
        'FrmBullMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(876, 532)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmBullMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bull Master"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtBreedInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSiteId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboBullStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBreedDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDamsYield, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDOB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoOfStraws, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBullProfileId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCattleType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpBullDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dtpBullDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtBullMaster As common.UserControls.txtNavigator
    Friend WithEvents lblCattleType As common.Controls.MyLabel
    Friend WithEvents txtCattleType As common.UserControls.txtFinder
    Friend WithEvents lblItemCategoryCode As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtBullProfileId As common.Controls.MyTextBox
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents txtNoOfStraws As common.MyNumBox
    Friend WithEvents MyLabel26 As common.Controls.MyLabel
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents dtpDOB As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtDamsYield As common.Controls.MyTextBox
    Friend WithEvents txtBreedDetails As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtBreedInfo As common.Controls.MyTextBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtSiteId As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents cboBullStatus As common.Controls.MyComboBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
End Class

