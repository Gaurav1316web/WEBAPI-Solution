Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports XpertERPEngineFine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSeedSelectionEntry
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.cmbSelectedFlag = New common.Controls.MyComboBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtSowingWeekMonth = New common.Controls.MyTextBox()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.txtArea = New common.Controls.MyTextBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtItemCode = New common.UserControls.txtFinder()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtPONo = New common.UserControls.txtFinder()
        Me.RadLabel24 = New common.Controls.MyLabel()
        Me.cmbSeason = New common.Controls.MyComboBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtLeaseLand = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtOwnLand = New common.MyNumBox()
        Me.txtFamilyLand = New common.MyNumBox()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtKhasraNo = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtCropLocation = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtDistrict = New common.UserControls.txtFinder()
        Me.lblDistrict = New common.Controls.MyLabel()
        Me.MyLabel41 = New common.Controls.MyLabel()
        Me.txtCropRegCode = New common.Controls.MyTextBox()
        Me.MyLabel25 = New common.Controls.MyLabel()
        Me.MyLabel26 = New common.Controls.MyLabel()
        Me.txtVillage = New common.UserControls.txtFinder()
        Me.UsLock = New common.usLock()
        Me.MyLabel35 = New common.Controls.MyLabel()
        Me.txtGrower = New common.UserControls.txtFinder()
        Me.MyLabel37 = New common.Controls.MyLabel()
        Me.txtDocumentNo = New common.UserControls.txtNavigator()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.lblCode = New common.Controls.MyLabel()
        Me.lblVillage = New common.Controls.MyLabel()
        Me.lblGrower = New common.Controls.MyLabel()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.TxtNavigator1 = New common.UserControls.txtNavigator()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReverseUnpost = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.txtPolicydate = New common.Controls.MyDateTimePicker()
        Me.txtInsEndDate = New common.Controls.MyDateTimePicker()
        Me.txtInsStartDate = New common.Controls.MyDateTimePicker()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSelectedFlag, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSowingWeekMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSeason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLeaseLand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOwnLand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFamilyLand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKhasraNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCropLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDistrict, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCropRegCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVillage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGrower, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverseUnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPolicydate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInsEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInsStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExport, Me.rmimport, Me.btnSaveLayout, Me.btnDeleteLayout})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'rmExport
        '
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export Blank Grid"
        '
        'rmimport
        '
        Me.rmimport.Name = "rmimport"
        Me.rmimport.Text = "Import Grid"
        '
        'btnSaveLayout
        '
        Me.btnSaveLayout.Name = "btnSaveLayout"
        Me.btnSaveLayout.Text = "Save Layout"
        '
        'btnDeleteLayout
        '
        Me.btnDeleteLayout.Name = "btnDeleteLayout"
        Me.btnDeleteLayout.Text = "Delete Layout"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(996, 20)
        Me.RadMenu1.TabIndex = 12
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmbSelectedFlag)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel16)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSowingWeekMonth)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel15)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtArea)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel13)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtItemCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel12)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPONo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel24)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmbSeason)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel10)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLeaseLand)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtOwnLand)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFamilyLand)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel23)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtKhasraNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCropLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDistrict)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDistrict)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel41)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCropRegCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel25)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel26)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtVillage)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel35)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtGrower)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel37)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDocumentNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblVillage)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblGrower)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel11)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel14)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel18)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel20)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel22)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtNavigator1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadButton1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverseUnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(996, 489)
        Me.SplitContainer1.SplitterDistance = 439
        Me.SplitContainer1.TabIndex = 13
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(428, 13)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 1486
        Me.RadLabel4.Text = "Date"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(462, 12)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(83, 18)
        Me.txtDate.TabIndex = 1485
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'cmbSelectedFlag
        '
        Me.cmbSelectedFlag.AutoCompleteDisplayMember = Nothing
        Me.cmbSelectedFlag.AutoCompleteValueMember = Nothing
        Me.cmbSelectedFlag.CalculationExpression = Nothing
        Me.cmbSelectedFlag.DropDownAnimationEnabled = True
        Me.cmbSelectedFlag.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbSelectedFlag.FieldCode = Nothing
        Me.cmbSelectedFlag.FieldDesc = Nothing
        Me.cmbSelectedFlag.FieldMaxLength = 0
        Me.cmbSelectedFlag.FieldName = Nothing
        Me.cmbSelectedFlag.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSelectedFlag.isCalculatedField = False
        Me.cmbSelectedFlag.IsSourceFromTable = False
        Me.cmbSelectedFlag.IsSourceFromValueList = False
        Me.cmbSelectedFlag.IsUnique = False
        RadListDataItem1.Text = "YES"
        RadListDataItem2.Text = "NO"
        Me.cmbSelectedFlag.Items.Add(RadListDataItem1)
        Me.cmbSelectedFlag.Items.Add(RadListDataItem2)
        Me.cmbSelectedFlag.Location = New System.Drawing.Point(143, 234)
        Me.cmbSelectedFlag.MendatroryField = True
        Me.cmbSelectedFlag.MyLinkLable1 = Me.MyLabel16
        Me.cmbSelectedFlag.MyLinkLable2 = Nothing
        Me.cmbSelectedFlag.Name = "cmbSelectedFlag"
        Me.cmbSelectedFlag.ReferenceFieldDesc = Nothing
        Me.cmbSelectedFlag.ReferenceFieldName = Nothing
        Me.cmbSelectedFlag.ReferenceTableName = Nothing
        Me.cmbSelectedFlag.Size = New System.Drawing.Size(138, 18)
        Me.cmbSelectedFlag.TabIndex = 1483
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Location = New System.Drawing.Point(11, 234)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(72, 18)
        Me.MyLabel16.TabIndex = 1484
        Me.MyLabel16.Text = "Selected Flag"
        '
        'txtSowingWeekMonth
        '
        Me.txtSowingWeekMonth.CalculationExpression = Nothing
        Me.txtSowingWeekMonth.FieldCode = Nothing
        Me.txtSowingWeekMonth.FieldDesc = Nothing
        Me.txtSowingWeekMonth.FieldMaxLength = 0
        Me.txtSowingWeekMonth.FieldName = Nothing
        Me.txtSowingWeekMonth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSowingWeekMonth.isCalculatedField = False
        Me.txtSowingWeekMonth.IsSourceFromTable = False
        Me.txtSowingWeekMonth.IsSourceFromValueList = False
        Me.txtSowingWeekMonth.IsUnique = False
        Me.txtSowingWeekMonth.Location = New System.Drawing.Point(144, 196)
        Me.txtSowingWeekMonth.MaxLength = 200
        Me.txtSowingWeekMonth.MendatroryField = False
        Me.txtSowingWeekMonth.MyLinkLable1 = Nothing
        Me.txtSowingWeekMonth.MyLinkLable2 = Nothing
        Me.txtSowingWeekMonth.Name = "txtSowingWeekMonth"
        Me.txtSowingWeekMonth.ReferenceFieldDesc = Nothing
        Me.txtSowingWeekMonth.ReferenceFieldName = Nothing
        Me.txtSowingWeekMonth.ReferenceTableName = Nothing
        Me.txtSowingWeekMonth.Size = New System.Drawing.Size(187, 18)
        Me.txtSowingWeekMonth.TabIndex = 1482
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Location = New System.Drawing.Point(11, 196)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(108, 18)
        Me.MyLabel15.TabIndex = 1481
        Me.MyLabel15.Text = "Sowing week Month"
        '
        'txtArea
        '
        Me.txtArea.CalculationExpression = Nothing
        Me.txtArea.FieldCode = Nothing
        Me.txtArea.FieldDesc = Nothing
        Me.txtArea.FieldMaxLength = 0
        Me.txtArea.FieldName = Nothing
        Me.txtArea.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtArea.isCalculatedField = False
        Me.txtArea.IsSourceFromTable = False
        Me.txtArea.IsSourceFromValueList = False
        Me.txtArea.IsUnique = False
        Me.txtArea.Location = New System.Drawing.Point(144, 120)
        Me.txtArea.MaxLength = 200
        Me.txtArea.MendatroryField = False
        Me.txtArea.MyLinkLable1 = Nothing
        Me.txtArea.MyLinkLable2 = Nothing
        Me.txtArea.Name = "txtArea"
        Me.txtArea.ReferenceFieldDesc = Nothing
        Me.txtArea.ReferenceFieldName = Nothing
        Me.txtArea.ReferenceTableName = Nothing
        Me.txtArea.Size = New System.Drawing.Size(187, 18)
        Me.txtArea.TabIndex = 1480
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Location = New System.Drawing.Point(12, 120)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel13.TabIndex = 1479
        Me.MyLabel13.Text = "Area"
        '
        'txtItemCode
        '
        Me.txtItemCode.CalculationExpression = Nothing
        Me.txtItemCode.FieldCode = Nothing
        Me.txtItemCode.FieldDesc = Nothing
        Me.txtItemCode.FieldMaxLength = 0
        Me.txtItemCode.FieldName = Nothing
        Me.txtItemCode.isCalculatedField = False
        Me.txtItemCode.IsSourceFromTable = False
        Me.txtItemCode.IsSourceFromValueList = False
        Me.txtItemCode.IsUnique = False
        Me.txtItemCode.Location = New System.Drawing.Point(144, 100)
        Me.txtItemCode.MendatroryField = False
        Me.txtItemCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.MyLinkLable1 = Me.MyLabel12
        Me.txtItemCode.MyLinkLable2 = Nothing
        Me.txtItemCode.MyReadOnly = True
        Me.txtItemCode.MyShowMasterFormButton = False
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.ReferenceFieldDesc = Nothing
        Me.txtItemCode.ReferenceFieldName = Nothing
        Me.txtItemCode.ReferenceTableName = Nothing
        Me.txtItemCode.Size = New System.Drawing.Size(188, 19)
        Me.txtItemCode.TabIndex = 1477
        Me.txtItemCode.Value = ""
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(12, 101)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel12.TabIndex = 1478
        Me.MyLabel12.Text = "Item Code"
        '
        'txtPONo
        '
        Me.txtPONo.CalculationExpression = Nothing
        Me.txtPONo.FieldCode = Nothing
        Me.txtPONo.FieldDesc = Nothing
        Me.txtPONo.FieldMaxLength = 0
        Me.txtPONo.FieldName = Nothing
        Me.txtPONo.isCalculatedField = False
        Me.txtPONo.IsSourceFromTable = False
        Me.txtPONo.IsSourceFromValueList = False
        Me.txtPONo.IsUnique = False
        Me.txtPONo.Location = New System.Drawing.Point(614, 12)
        Me.txtPONo.MendatroryField = False
        Me.txtPONo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONo.MyLinkLable1 = Me.RadLabel24
        Me.txtPONo.MyLinkLable2 = Nothing
        Me.txtPONo.MyReadOnly = True
        Me.txtPONo.MyShowMasterFormButton = False
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.ReferenceFieldDesc = Nothing
        Me.txtPONo.ReferenceFieldName = Nothing
        Me.txtPONo.ReferenceTableName = Nothing
        Me.txtPONo.Size = New System.Drawing.Size(154, 19)
        Me.txtPONo.TabIndex = 1475
        Me.txtPONo.Value = ""
        '
        'RadLabel24
        '
        Me.RadLabel24.FieldName = Nothing
        Me.RadLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel24.Location = New System.Drawing.Point(560, 13)
        Me.RadLabel24.Name = "RadLabel24"
        Me.RadLabel24.Size = New System.Drawing.Size(40, 16)
        Me.RadLabel24.TabIndex = 1476
        Me.RadLabel24.Text = "PO No"
        '
        'cmbSeason
        '
        Me.cmbSeason.AutoCompleteDisplayMember = Nothing
        Me.cmbSeason.AutoCompleteValueMember = Nothing
        Me.cmbSeason.CalculationExpression = Nothing
        Me.cmbSeason.DropDownAnimationEnabled = True
        Me.cmbSeason.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbSeason.FieldCode = Nothing
        Me.cmbSeason.FieldDesc = Nothing
        Me.cmbSeason.FieldMaxLength = 0
        Me.cmbSeason.FieldName = Nothing
        Me.cmbSeason.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSeason.isCalculatedField = False
        Me.cmbSeason.IsSourceFromTable = False
        Me.cmbSeason.IsSourceFromValueList = False
        Me.cmbSeason.IsUnique = False
        RadListDataItem3.Text = "RABI"
        RadListDataItem4.Text = "KHARIF"
        Me.cmbSeason.Items.Add(RadListDataItem3)
        Me.cmbSeason.Items.Add(RadListDataItem4)
        Me.cmbSeason.Location = New System.Drawing.Point(143, 216)
        Me.cmbSeason.MendatroryField = True
        Me.cmbSeason.MyLinkLable1 = Me.MyLabel10
        Me.cmbSeason.MyLinkLable2 = Nothing
        Me.cmbSeason.Name = "cmbSeason"
        Me.cmbSeason.ReferenceFieldDesc = Nothing
        Me.cmbSeason.ReferenceFieldName = Nothing
        Me.cmbSeason.ReferenceTableName = Nothing
        Me.cmbSeason.Size = New System.Drawing.Size(138, 18)
        Me.cmbSeason.TabIndex = 1473
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Location = New System.Drawing.Point(12, 216)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(42, 18)
        Me.MyLabel10.TabIndex = 1474
        Me.MyLabel10.Text = "Season"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(253, 298)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel5.TabIndex = 1472
        Me.MyLabel5.Text = "Acre"
        '
        'txtLeaseLand
        '
        Me.txtLeaseLand.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtLeaseLand.CalculationExpression = Nothing
        Me.txtLeaseLand.DecimalPlaces = 2
        Me.txtLeaseLand.FieldCode = Nothing
        Me.txtLeaseLand.FieldDesc = Nothing
        Me.txtLeaseLand.FieldMaxLength = 0
        Me.txtLeaseLand.FieldName = Nothing
        Me.txtLeaseLand.isCalculatedField = False
        Me.txtLeaseLand.IsSourceFromTable = False
        Me.txtLeaseLand.IsSourceFromValueList = False
        Me.txtLeaseLand.IsUnique = False
        Me.txtLeaseLand.Location = New System.Drawing.Point(143, 297)
        Me.txtLeaseLand.MendatroryField = False
        Me.txtLeaseLand.MyLinkLable1 = Nothing
        Me.txtLeaseLand.MyLinkLable2 = Nothing
        Me.txtLeaseLand.Name = "txtLeaseLand"
        Me.txtLeaseLand.ReferenceFieldDesc = Nothing
        Me.txtLeaseLand.ReferenceFieldName = Nothing
        Me.txtLeaseLand.ReferenceTableName = Nothing
        Me.txtLeaseLand.Size = New System.Drawing.Size(98, 20)
        Me.txtLeaseLand.TabIndex = 1471
        Me.txtLeaseLand.Text = "0"
        Me.txtLeaseLand.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLeaseLand.Value = 0R
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(11, 299)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(61, 18)
        Me.MyLabel4.TabIndex = 1470
        Me.MyLabel4.Text = "Lease Land"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(253, 276)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel7.TabIndex = 1469
        Me.MyLabel7.Text = "Acre"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(253, 254)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel8.TabIndex = 1466
        Me.MyLabel8.Text = "Acre"
        '
        'txtOwnLand
        '
        Me.txtOwnLand.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtOwnLand.CalculationExpression = Nothing
        Me.txtOwnLand.DecimalPlaces = 2
        Me.txtOwnLand.FieldCode = Nothing
        Me.txtOwnLand.FieldDesc = Nothing
        Me.txtOwnLand.FieldMaxLength = 0
        Me.txtOwnLand.FieldName = Nothing
        Me.txtOwnLand.isCalculatedField = False
        Me.txtOwnLand.IsSourceFromTable = False
        Me.txtOwnLand.IsSourceFromValueList = False
        Me.txtOwnLand.IsUnique = False
        Me.txtOwnLand.Location = New System.Drawing.Point(143, 253)
        Me.txtOwnLand.MendatroryField = False
        Me.txtOwnLand.MyLinkLable1 = Nothing
        Me.txtOwnLand.MyLinkLable2 = Nothing
        Me.txtOwnLand.Name = "txtOwnLand"
        Me.txtOwnLand.ReferenceFieldDesc = Nothing
        Me.txtOwnLand.ReferenceFieldName = Nothing
        Me.txtOwnLand.ReferenceTableName = Nothing
        Me.txtOwnLand.Size = New System.Drawing.Size(98, 20)
        Me.txtOwnLand.TabIndex = 1465
        Me.txtOwnLand.Text = "0"
        Me.txtOwnLand.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtOwnLand.Value = 0R
        '
        'txtFamilyLand
        '
        Me.txtFamilyLand.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtFamilyLand.CalculationExpression = Nothing
        Me.txtFamilyLand.DecimalPlaces = 2
        Me.txtFamilyLand.FieldCode = Nothing
        Me.txtFamilyLand.FieldDesc = Nothing
        Me.txtFamilyLand.FieldMaxLength = 0
        Me.txtFamilyLand.FieldName = Nothing
        Me.txtFamilyLand.isCalculatedField = False
        Me.txtFamilyLand.IsSourceFromTable = False
        Me.txtFamilyLand.IsSourceFromValueList = False
        Me.txtFamilyLand.IsUnique = False
        Me.txtFamilyLand.Location = New System.Drawing.Point(143, 275)
        Me.txtFamilyLand.MendatroryField = False
        Me.txtFamilyLand.MyLinkLable1 = Nothing
        Me.txtFamilyLand.MyLinkLable2 = Nothing
        Me.txtFamilyLand.Name = "txtFamilyLand"
        Me.txtFamilyLand.ReferenceFieldDesc = Nothing
        Me.txtFamilyLand.ReferenceFieldName = Nothing
        Me.txtFamilyLand.ReferenceTableName = Nothing
        Me.txtFamilyLand.Size = New System.Drawing.Size(98, 20)
        Me.txtFamilyLand.TabIndex = 1468
        Me.txtFamilyLand.Text = "0"
        Me.txtFamilyLand.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFamilyLand.Value = 0R
        '
        'MyLabel23
        '
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel23.Location = New System.Drawing.Point(11, 255)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(57, 18)
        Me.MyLabel23.TabIndex = 1464
        Me.MyLabel23.Text = "Own Land"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(11, 277)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(65, 18)
        Me.MyLabel9.TabIndex = 1467
        Me.MyLabel9.Text = "Family Land"
        '
        'txtKhasraNo
        '
        Me.txtKhasraNo.CalculationExpression = Nothing
        Me.txtKhasraNo.FieldCode = Nothing
        Me.txtKhasraNo.FieldDesc = Nothing
        Me.txtKhasraNo.FieldMaxLength = 0
        Me.txtKhasraNo.FieldName = Nothing
        Me.txtKhasraNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKhasraNo.isCalculatedField = False
        Me.txtKhasraNo.IsSourceFromTable = False
        Me.txtKhasraNo.IsSourceFromValueList = False
        Me.txtKhasraNo.IsUnique = False
        Me.txtKhasraNo.Location = New System.Drawing.Point(144, 177)
        Me.txtKhasraNo.MaxLength = 200
        Me.txtKhasraNo.MendatroryField = False
        Me.txtKhasraNo.MyLinkLable1 = Nothing
        Me.txtKhasraNo.MyLinkLable2 = Nothing
        Me.txtKhasraNo.Name = "txtKhasraNo"
        Me.txtKhasraNo.ReferenceFieldDesc = Nothing
        Me.txtKhasraNo.ReferenceFieldName = Nothing
        Me.txtKhasraNo.ReferenceTableName = Nothing
        Me.txtKhasraNo.Size = New System.Drawing.Size(187, 18)
        Me.txtKhasraNo.TabIndex = 75
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(11, 177)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel3.TabIndex = 74
        Me.MyLabel3.Text = "Khasra No"
        '
        'txtCropLocation
        '
        Me.txtCropLocation.CalculationExpression = Nothing
        Me.txtCropLocation.FieldCode = Nothing
        Me.txtCropLocation.FieldDesc = Nothing
        Me.txtCropLocation.FieldMaxLength = 0
        Me.txtCropLocation.FieldName = Nothing
        Me.txtCropLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCropLocation.isCalculatedField = False
        Me.txtCropLocation.IsSourceFromTable = False
        Me.txtCropLocation.IsSourceFromValueList = False
        Me.txtCropLocation.IsUnique = False
        Me.txtCropLocation.Location = New System.Drawing.Point(144, 158)
        Me.txtCropLocation.MaxLength = 200
        Me.txtCropLocation.MendatroryField = False
        Me.txtCropLocation.MyLinkLable1 = Nothing
        Me.txtCropLocation.MyLinkLable2 = Nothing
        Me.txtCropLocation.Name = "txtCropLocation"
        Me.txtCropLocation.ReferenceFieldDesc = Nothing
        Me.txtCropLocation.ReferenceFieldName = Nothing
        Me.txtCropLocation.ReferenceTableName = Nothing
        Me.txtCropLocation.Size = New System.Drawing.Size(187, 18)
        Me.txtCropLocation.TabIndex = 73
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(12, 158)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(76, 18)
        Me.MyLabel2.TabIndex = 72
        Me.MyLabel2.Text = "Crop Location"
        '
        'txtDistrict
        '
        Me.txtDistrict.CalculationExpression = Nothing
        Me.txtDistrict.FieldCode = Nothing
        Me.txtDistrict.FieldDesc = Nothing
        Me.txtDistrict.FieldMaxLength = 0
        Me.txtDistrict.FieldName = Nothing
        Me.txtDistrict.isCalculatedField = False
        Me.txtDistrict.IsSourceFromTable = False
        Me.txtDistrict.IsSourceFromValueList = False
        Me.txtDistrict.IsUnique = False
        Me.txtDistrict.Location = New System.Drawing.Point(145, 78)
        Me.txtDistrict.MendatroryField = True
        Me.txtDistrict.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDistrict.MyLinkLable1 = Nothing
        Me.txtDistrict.MyLinkLable2 = Nothing
        Me.txtDistrict.MyReadOnly = False
        Me.txtDistrict.MyShowMasterFormButton = False
        Me.txtDistrict.Name = "txtDistrict"
        Me.txtDistrict.ReferenceFieldDesc = Nothing
        Me.txtDistrict.ReferenceFieldName = Nothing
        Me.txtDistrict.ReferenceTableName = Nothing
        Me.txtDistrict.Size = New System.Drawing.Size(187, 19)
        Me.txtDistrict.TabIndex = 70
        Me.txtDistrict.Value = ""
        '
        'lblDistrict
        '
        Me.lblDistrict.AutoSize = False
        Me.lblDistrict.BorderVisible = True
        Me.lblDistrict.FieldName = Nothing
        Me.lblDistrict.Location = New System.Drawing.Point(335, 79)
        Me.lblDistrict.Name = "lblDistrict"
        Me.lblDistrict.Size = New System.Drawing.Size(301, 19)
        Me.lblDistrict.TabIndex = 69
        '
        'MyLabel41
        '
        Me.MyLabel41.FieldName = Nothing
        Me.MyLabel41.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel41.Location = New System.Drawing.Point(12, 81)
        Me.MyLabel41.Name = "MyLabel41"
        Me.MyLabel41.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel41.TabIndex = 71
        Me.MyLabel41.Text = "District"
        '
        'txtCropRegCode
        '
        Me.txtCropRegCode.CalculationExpression = Nothing
        Me.txtCropRegCode.FieldCode = Nothing
        Me.txtCropRegCode.FieldDesc = Nothing
        Me.txtCropRegCode.FieldMaxLength = 0
        Me.txtCropRegCode.FieldName = Nothing
        Me.txtCropRegCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCropRegCode.isCalculatedField = False
        Me.txtCropRegCode.IsSourceFromTable = False
        Me.txtCropRegCode.IsSourceFromValueList = False
        Me.txtCropRegCode.IsUnique = False
        Me.txtCropRegCode.Location = New System.Drawing.Point(144, 139)
        Me.txtCropRegCode.MaxLength = 200
        Me.txtCropRegCode.MendatroryField = False
        Me.txtCropRegCode.MyLinkLable1 = Nothing
        Me.txtCropRegCode.MyLinkLable2 = Nothing
        Me.txtCropRegCode.Name = "txtCropRegCode"
        Me.txtCropRegCode.ReferenceFieldDesc = Nothing
        Me.txtCropRegCode.ReferenceFieldName = Nothing
        Me.txtCropRegCode.ReferenceTableName = Nothing
        Me.txtCropRegCode.Size = New System.Drawing.Size(187, 18)
        Me.txtCropRegCode.TabIndex = 68
        '
        'MyLabel25
        '
        Me.MyLabel25.FieldName = Nothing
        Me.MyLabel25.Location = New System.Drawing.Point(12, 15)
        Me.MyLabel25.Name = "MyLabel25"
        Me.MyLabel25.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel25.TabIndex = 21
        Me.MyLabel25.Text = "Document No"
        '
        'MyLabel26
        '
        Me.MyLabel26.FieldName = Nothing
        Me.MyLabel26.Location = New System.Drawing.Point(12, 139)
        Me.MyLabel26.Name = "MyLabel26"
        Me.MyLabel26.Size = New System.Drawing.Size(82, 18)
        Me.MyLabel26.TabIndex = 67
        Me.MyLabel26.Text = "Crop Reg Code"
        '
        'txtVillage
        '
        Me.txtVillage.CalculationExpression = Nothing
        Me.txtVillage.FieldCode = Nothing
        Me.txtVillage.FieldDesc = Nothing
        Me.txtVillage.FieldMaxLength = 0
        Me.txtVillage.FieldName = Nothing
        Me.txtVillage.isCalculatedField = False
        Me.txtVillage.IsSourceFromTable = False
        Me.txtVillage.IsSourceFromValueList = False
        Me.txtVillage.IsUnique = False
        Me.txtVillage.Location = New System.Drawing.Point(145, 56)
        Me.txtVillage.MendatroryField = True
        Me.txtVillage.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVillage.MyLinkLable1 = Nothing
        Me.txtVillage.MyLinkLable2 = Nothing
        Me.txtVillage.MyReadOnly = False
        Me.txtVillage.MyShowMasterFormButton = False
        Me.txtVillage.Name = "txtVillage"
        Me.txtVillage.ReferenceFieldDesc = Nothing
        Me.txtVillage.ReferenceFieldName = Nothing
        Me.txtVillage.ReferenceTableName = Nothing
        Me.txtVillage.Size = New System.Drawing.Size(187, 19)
        Me.txtVillage.TabIndex = 62
        Me.txtVillage.Value = ""
        '
        'UsLock
        '
        Me.UsLock.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock.Location = New System.Drawing.Point(794, 13)
        Me.UsLock.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock.Name = "UsLock"
        Me.UsLock.Size = New System.Drawing.Size(97, 20)
        Me.UsLock.Status = common.ERPTransactionStatus.Pending
        Me.UsLock.TabIndex = 26
        '
        'MyLabel35
        '
        Me.MyLabel35.FieldName = Nothing
        Me.MyLabel35.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel35.Location = New System.Drawing.Point(12, 59)
        Me.MyLabel35.Name = "MyLabel35"
        Me.MyLabel35.Size = New System.Drawing.Size(40, 16)
        Me.MyLabel35.TabIndex = 63
        Me.MyLabel35.Text = "Village"
        '
        'txtGrower
        '
        Me.txtGrower.CalculationExpression = Nothing
        Me.txtGrower.FieldCode = Nothing
        Me.txtGrower.FieldDesc = Nothing
        Me.txtGrower.FieldMaxLength = 0
        Me.txtGrower.FieldName = Nothing
        Me.txtGrower.isCalculatedField = False
        Me.txtGrower.IsSourceFromTable = False
        Me.txtGrower.IsSourceFromValueList = False
        Me.txtGrower.IsUnique = False
        Me.txtGrower.Location = New System.Drawing.Point(145, 35)
        Me.txtGrower.MendatroryField = True
        Me.txtGrower.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGrower.MyLinkLable1 = Nothing
        Me.txtGrower.MyLinkLable2 = Nothing
        Me.txtGrower.MyReadOnly = False
        Me.txtGrower.MyShowMasterFormButton = False
        Me.txtGrower.Name = "txtGrower"
        Me.txtGrower.ReferenceFieldDesc = Nothing
        Me.txtGrower.ReferenceFieldName = Nothing
        Me.txtGrower.ReferenceTableName = Nothing
        Me.txtGrower.Size = New System.Drawing.Size(187, 19)
        Me.txtGrower.TabIndex = 19
        Me.txtGrower.Value = ""
        '
        'MyLabel37
        '
        Me.MyLabel37.FieldName = Nothing
        Me.MyLabel37.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel37.Location = New System.Drawing.Point(12, 37)
        Me.MyLabel37.Name = "MyLabel37"
        Me.MyLabel37.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel37.TabIndex = 20
        Me.MyLabel37.Text = "Grower"
        '
        'txtDocumentNo
        '
        Me.txtDocumentNo.FieldName = Nothing
        Me.txtDocumentNo.Location = New System.Drawing.Point(145, 12)
        Me.txtDocumentNo.MendatroryField = False
        Me.txtDocumentNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocumentNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocumentNo.MyLinkLable1 = Me.MyLabel25
        Me.txtDocumentNo.MyLinkLable2 = Nothing
        Me.txtDocumentNo.MyMaxLength = 32767
        Me.txtDocumentNo.MyReadOnly = False
        Me.txtDocumentNo.Name = "txtDocumentNo"
        Me.txtDocumentNo.Size = New System.Drawing.Size(256, 20)
        Me.txtDocumentNo.TabIndex = 25
        Me.txtDocumentNo.Value = ""
        '
        'btnNew
        '
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = Global.XpertERPSheed.My.Resources.Resources._new
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(401, 12)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(21, 20)
        Me.btnNew.TabIndex = 22
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Location = New System.Drawing.Point(12, 15)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(77, 18)
        Me.lblCode.TabIndex = 21
        Me.lblCode.Text = "Document No"
        '
        'lblVillage
        '
        Me.lblVillage.AutoSize = False
        Me.lblVillage.BorderVisible = True
        Me.lblVillage.FieldName = Nothing
        Me.lblVillage.Location = New System.Drawing.Point(335, 57)
        Me.lblVillage.Name = "lblVillage"
        Me.lblVillage.Size = New System.Drawing.Size(301, 19)
        Me.lblVillage.TabIndex = 44
        '
        'lblGrower
        '
        Me.lblGrower.AutoSize = False
        Me.lblGrower.BorderVisible = True
        Me.lblGrower.FieldName = Nothing
        Me.lblGrower.Location = New System.Drawing.Point(335, 35)
        Me.lblGrower.Name = "lblGrower"
        Me.lblGrower.Size = New System.Drawing.Size(301, 19)
        Me.lblGrower.TabIndex = 24
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel11.Location = New System.Drawing.Point(12, 59)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(40, 16)
        Me.MyLabel11.TabIndex = 63
        Me.MyLabel11.Text = "Village"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(12, 37)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel6.TabIndex = 20
        Me.MyLabel6.Text = "Grower"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPSheed.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(401, 12)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(21, 20)
        Me.btnAddNew.TabIndex = 22
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(12, 15)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel1.TabIndex = 21
        Me.MyLabel1.Text = "Document No"
        '
        'MyLabel14
        '
        Me.MyLabel14.AutoSize = False
        Me.MyLabel14.BorderVisible = True
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Location = New System.Drawing.Point(335, 57)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(169, 19)
        Me.MyLabel14.TabIndex = 44
        '
        'MyLabel18
        '
        Me.MyLabel18.AutoSize = False
        Me.MyLabel18.BorderVisible = True
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Location = New System.Drawing.Point(335, 35)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(169, 19)
        Me.MyLabel18.TabIndex = 24
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel20.Location = New System.Drawing.Point(12, 59)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(40, 16)
        Me.MyLabel20.TabIndex = 63
        Me.MyLabel20.Text = "Village"
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel22.Location = New System.Drawing.Point(12, 37)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel22.TabIndex = 20
        Me.MyLabel22.Text = "Grower"
        '
        'TxtNavigator1
        '
        Me.TxtNavigator1.FieldName = Nothing
        Me.TxtNavigator1.Location = New System.Drawing.Point(145, 12)
        Me.TxtNavigator1.MendatroryField = False
        Me.TxtNavigator1.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.TxtNavigator1.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.TxtNavigator1.MyLinkLable1 = Me.MyLabel1
        Me.TxtNavigator1.MyLinkLable2 = Nothing
        Me.TxtNavigator1.MyMaxLength = 32767
        Me.TxtNavigator1.MyReadOnly = False
        Me.TxtNavigator1.Name = "TxtNavigator1"
        Me.TxtNavigator1.Size = New System.Drawing.Size(256, 20)
        Me.TxtNavigator1.TabIndex = 25
        Me.TxtNavigator1.Value = ""
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Image = Global.XpertERPSheed.My.Resources.Resources._new
        Me.RadButton1.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadButton1.Location = New System.Drawing.Point(401, 12)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(21, 20)
        Me.RadButton1.TabIndex = 22
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(218, 14)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(95, 22)
        Me.RadSplitButton1.TabIndex = 161
        Me.RadSplitButton1.Text = "Export"
        '
        'btnExcel
        '
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.UseCompatibleTextRendering = False
        '
        'btnPDF
        '
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "PDF"
        Me.btnPDF.UseCompatibleTextRendering = False
        '
        'btnReverseUnpost
        '
        Me.btnReverseUnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverseUnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverseUnpost.Location = New System.Drawing.Point(320, 15)
        Me.btnReverseUnpost.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnReverseUnpost.Name = "btnReverseUnpost"
        Me.btnReverseUnpost.Size = New System.Drawing.Size(122, 20)
        Me.btnReverseUnpost.TabIndex = 160
        Me.btnReverseUnpost.Text = "Reverse and Unpost"
        Me.btnReverseUnpost.Visible = False
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(145, 14)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 20)
        Me.btnPost.TabIndex = 159
        Me.btnPost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(920, 14)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(69, 20)
        Me.btnclose.TabIndex = 158
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(74, 14)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(69, 20)
        Me.btndelete.TabIndex = 157
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(3, 14)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(69, 20)
        Me.btnsave.TabIndex = 156
        Me.btnsave.Text = "Save"
        '
        'txtPolicydate
        '
        Me.txtPolicydate.CalculationExpression = Nothing
        Me.txtPolicydate.CustomFormat = "dd/MM/yyyy"
        Me.txtPolicydate.FieldCode = Nothing
        Me.txtPolicydate.FieldDesc = Nothing
        Me.txtPolicydate.FieldMaxLength = 0
        Me.txtPolicydate.FieldName = Nothing
        Me.txtPolicydate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPolicydate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtPolicydate.isCalculatedField = False
        Me.txtPolicydate.IsSourceFromTable = False
        Me.txtPolicydate.IsSourceFromValueList = False
        Me.txtPolicydate.IsUnique = False
        Me.txtPolicydate.Location = New System.Drawing.Point(402, 77)
        Me.txtPolicydate.MendatroryField = True
        Me.txtPolicydate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPolicydate.MyLinkLable1 = Nothing
        Me.txtPolicydate.MyLinkLable2 = Nothing
        Me.txtPolicydate.Name = "txtPolicydate"
        Me.txtPolicydate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPolicydate.ReferenceFieldDesc = Nothing
        Me.txtPolicydate.ReferenceFieldName = Nothing
        Me.txtPolicydate.ReferenceTableName = Nothing
        Me.txtPolicydate.Size = New System.Drawing.Size(102, 18)
        Me.txtPolicydate.TabIndex = 18
        Me.txtPolicydate.TabStop = False
        Me.txtPolicydate.Text = "13/06/2011"
        Me.txtPolicydate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtInsEndDate
        '
        Me.txtInsEndDate.CalculationExpression = Nothing
        Me.txtInsEndDate.CustomFormat = "dd/MM/yyyy"
        Me.txtInsEndDate.FieldCode = Nothing
        Me.txtInsEndDate.FieldDesc = Nothing
        Me.txtInsEndDate.FieldMaxLength = 0
        Me.txtInsEndDate.FieldName = Nothing
        Me.txtInsEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInsEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtInsEndDate.isCalculatedField = False
        Me.txtInsEndDate.IsSourceFromTable = False
        Me.txtInsEndDate.IsSourceFromValueList = False
        Me.txtInsEndDate.IsUnique = False
        Me.txtInsEndDate.Location = New System.Drawing.Point(402, 99)
        Me.txtInsEndDate.MendatroryField = True
        Me.txtInsEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInsEndDate.MyLinkLable1 = Nothing
        Me.txtInsEndDate.MyLinkLable2 = Nothing
        Me.txtInsEndDate.Name = "txtInsEndDate"
        Me.txtInsEndDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInsEndDate.ReferenceFieldDesc = Nothing
        Me.txtInsEndDate.ReferenceFieldName = Nothing
        Me.txtInsEndDate.ReferenceTableName = Nothing
        Me.txtInsEndDate.Size = New System.Drawing.Size(102, 18)
        Me.txtInsEndDate.TabIndex = 47
        Me.txtInsEndDate.TabStop = False
        Me.txtInsEndDate.Text = "13/06/2011"
        Me.txtInsEndDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtInsStartDate
        '
        Me.txtInsStartDate.CalculationExpression = Nothing
        Me.txtInsStartDate.CustomFormat = "dd/MM/yyyy"
        Me.txtInsStartDate.FieldCode = Nothing
        Me.txtInsStartDate.FieldDesc = Nothing
        Me.txtInsStartDate.FieldMaxLength = 0
        Me.txtInsStartDate.FieldName = Nothing
        Me.txtInsStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInsStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtInsStartDate.isCalculatedField = False
        Me.txtInsStartDate.IsSourceFromTable = False
        Me.txtInsStartDate.IsSourceFromValueList = False
        Me.txtInsStartDate.IsUnique = False
        Me.txtInsStartDate.Location = New System.Drawing.Point(144, 99)
        Me.txtInsStartDate.MendatroryField = True
        Me.txtInsStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInsStartDate.MyLinkLable1 = Nothing
        Me.txtInsStartDate.MyLinkLable2 = Nothing
        Me.txtInsStartDate.Name = "txtInsStartDate"
        Me.txtInsStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInsStartDate.ReferenceFieldDesc = Nothing
        Me.txtInsStartDate.ReferenceFieldName = Nothing
        Me.txtInsStartDate.ReferenceTableName = Nothing
        Me.txtInsStartDate.Size = New System.Drawing.Size(102, 18)
        Me.txtInsStartDate.TabIndex = 49
        Me.txtInsStartDate.TabStop = False
        Me.txtInsStartDate.Text = "13/06/2011"
        Me.txtInsStartDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'frmSeedSelectionEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(996, 509)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmSeedSelectionEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Seed Selection Entry"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSelectedFlag, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSowingWeekMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSeason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLeaseLand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOwnLand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFamilyLand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKhasraNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCropLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDistrict, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCropRegCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVillage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGrower, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverseUnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPolicydate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInsEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInsStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnSaveLayout As RadMenuItem
    Friend WithEvents btnDeleteLayout As RadMenuItem
    Friend WithEvents RadMenu1 As RadMenu
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadSplitButton1 As RadSplitButton
    Friend WithEvents btnExcel As RadMenuItem
    Friend WithEvents btnPDF As RadMenuItem
    Friend WithEvents btnReverseUnpost As RadButton
    Friend WithEvents btnPost As RadButton
    Friend WithEvents btnclose As RadButton
    Friend WithEvents btndelete As RadButton
    Friend WithEvents btnsave As RadButton
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents lblVillage As common.Controls.MyLabel
    Friend WithEvents lblGrower As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents txtPolicydate As common.Controls.MyDateTimePicker
    Friend WithEvents txtInsEndDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtInsStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtDistrict As common.UserControls.txtFinder
    Friend WithEvents lblDistrict As common.Controls.MyLabel
    Friend WithEvents MyLabel41 As common.Controls.MyLabel
    Friend WithEvents txtCropRegCode As common.Controls.MyTextBox
    Friend WithEvents MyLabel25 As common.Controls.MyLabel
    Friend WithEvents txtVillage As common.UserControls.txtFinder
    Friend WithEvents UsLock As common.usLock
    Friend WithEvents MyLabel35 As common.Controls.MyLabel
    Friend WithEvents txtGrower As common.UserControls.txtFinder
    Friend WithEvents MyLabel37 As common.Controls.MyLabel
    Friend WithEvents txtDocumentNo As common.UserControls.txtNavigator
    Friend WithEvents btnNew As RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents TxtNavigator1 As common.UserControls.txtNavigator
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents txtCropLocation As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel26 As common.Controls.MyLabel
    Friend WithEvents txtKhasraNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtLeaseLand As common.MyNumBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtOwnLand As common.MyNumBox
    Friend WithEvents txtFamilyLand As common.MyNumBox
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents cmbSeason As common.Controls.MyComboBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtPONo As common.UserControls.txtFinder
    Friend WithEvents RadLabel24 As common.Controls.MyLabel
    Friend WithEvents txtItemCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtArea As common.Controls.MyTextBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtSowingWeekMonth As common.Controls.MyTextBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents cmbSelectedFlag As common.Controls.MyComboBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
End Class
