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
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtLeaseLand = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtOwnLand = New common.MyNumBox()
        Me.txtFamilyLand = New common.MyNumBox()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyTextBox4 = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyTextBox3 = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.TxtFinder5 = New common.UserControls.txtFinder()
        Me.MyLabel40 = New common.Controls.MyLabel()
        Me.MyLabel41 = New common.Controls.MyLabel()
        Me.MyTextBox2 = New common.Controls.MyTextBox()
        Me.MyLabel25 = New common.Controls.MyLabel()
        Me.MyLabel26 = New common.Controls.MyLabel()
        Me.TxtFinder3 = New common.UserControls.txtFinder()
        Me.MyLabel29 = New common.Controls.MyLabel()
        Me.UsLock2 = New common.usLock()
        Me.MyLabel33 = New common.Controls.MyLabel()
        Me.MyLabel35 = New common.Controls.MyLabel()
        Me.TxtFinder4 = New common.UserControls.txtFinder()
        Me.MyLabel37 = New common.Controls.MyLabel()
        Me.TxtNavigator2 = New common.UserControls.txtNavigator()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.txtPolicyNo = New common.Controls.MyTextBox()
        Me.lblCode = New common.Controls.MyLabel()
        Me.txtInsType = New common.UserControls.txtFinder()
        Me.lblInsType = New common.Controls.MyLabel()
        Me.lblStatus = New common.usLock()
        Me.lblInsCompName = New common.Controls.MyLabel()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtInsCompany = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtDocumentNo = New common.UserControls.txtNavigator()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.MyTextBox1 = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.TxtFinder1 = New common.UserControls.txtFinder()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.TxtFinder2 = New common.UserControls.txtFinder()
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
        Me.cmbUsedAs = New common.Controls.MyComboBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLeaseLand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOwnLand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFamilyLand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyTextBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyTextBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyTextBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPolicyNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInsType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInsCompName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyTextBox1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.cmbUsedAs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmbUsedAs)
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyTextBox4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyTextBox3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtFinder5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel40)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel41)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyTextBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel25)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel26)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtFinder3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel29)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel33)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel35)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtFinder4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel37)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtNavigator2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadButton2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPolicyNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtInsType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblInsType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblStatus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblInsCompName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel11)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtInsCompany)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDocumentNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyTextBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtFinder1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel14)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel18)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel20)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtFinder2)
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
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(254, 222)
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
        Me.txtLeaseLand.Location = New System.Drawing.Point(145, 221)
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
        Me.MyLabel4.Location = New System.Drawing.Point(12, 223)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(61, 18)
        Me.MyLabel4.TabIndex = 1470
        Me.MyLabel4.Text = "Lease Land"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(254, 200)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel7.TabIndex = 1469
        Me.MyLabel7.Text = "Acre"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(254, 178)
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
        Me.txtOwnLand.Location = New System.Drawing.Point(145, 177)
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
        Me.txtFamilyLand.Location = New System.Drawing.Point(145, 199)
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
        Me.MyLabel23.Location = New System.Drawing.Point(12, 179)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(57, 18)
        Me.MyLabel23.TabIndex = 1464
        Me.MyLabel23.Text = "Own Land"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(12, 201)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(65, 18)
        Me.MyLabel9.TabIndex = 1467
        Me.MyLabel9.Text = "Family Land"
        '
        'MyTextBox4
        '
        Me.MyTextBox4.CalculationExpression = Nothing
        Me.MyTextBox4.FieldCode = Nothing
        Me.MyTextBox4.FieldDesc = Nothing
        Me.MyTextBox4.FieldMaxLength = 0
        Me.MyTextBox4.FieldName = Nothing
        Me.MyTextBox4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyTextBox4.isCalculatedField = False
        Me.MyTextBox4.IsSourceFromTable = False
        Me.MyTextBox4.IsSourceFromValueList = False
        Me.MyTextBox4.IsUnique = False
        Me.MyTextBox4.Location = New System.Drawing.Point(144, 137)
        Me.MyTextBox4.MaxLength = 200
        Me.MyTextBox4.MendatroryField = False
        Me.MyTextBox4.MyLinkLable1 = Nothing
        Me.MyTextBox4.MyLinkLable2 = Nothing
        Me.MyTextBox4.Name = "MyTextBox4"
        Me.MyTextBox4.ReferenceFieldDesc = Nothing
        Me.MyTextBox4.ReferenceFieldName = Nothing
        Me.MyTextBox4.ReferenceTableName = Nothing
        Me.MyTextBox4.Size = New System.Drawing.Size(187, 18)
        Me.MyTextBox4.TabIndex = 75
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(12, 137)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel3.TabIndex = 74
        Me.MyLabel3.Text = "Khasra No"
        '
        'MyTextBox3
        '
        Me.MyTextBox3.CalculationExpression = Nothing
        Me.MyTextBox3.FieldCode = Nothing
        Me.MyTextBox3.FieldDesc = Nothing
        Me.MyTextBox3.FieldMaxLength = 0
        Me.MyTextBox3.FieldName = Nothing
        Me.MyTextBox3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyTextBox3.isCalculatedField = False
        Me.MyTextBox3.IsSourceFromTable = False
        Me.MyTextBox3.IsSourceFromValueList = False
        Me.MyTextBox3.IsUnique = False
        Me.MyTextBox3.Location = New System.Drawing.Point(144, 118)
        Me.MyTextBox3.MaxLength = 200
        Me.MyTextBox3.MendatroryField = False
        Me.MyTextBox3.MyLinkLable1 = Nothing
        Me.MyTextBox3.MyLinkLable2 = Nothing
        Me.MyTextBox3.Name = "MyTextBox3"
        Me.MyTextBox3.ReferenceFieldDesc = Nothing
        Me.MyTextBox3.ReferenceFieldName = Nothing
        Me.MyTextBox3.ReferenceTableName = Nothing
        Me.MyTextBox3.Size = New System.Drawing.Size(187, 18)
        Me.MyTextBox3.TabIndex = 73
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(12, 119)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(76, 18)
        Me.MyLabel2.TabIndex = 72
        Me.MyLabel2.Text = "Crop Location"
        '
        'TxtFinder5
        '
        Me.TxtFinder5.CalculationExpression = Nothing
        Me.TxtFinder5.FieldCode = Nothing
        Me.TxtFinder5.FieldDesc = Nothing
        Me.TxtFinder5.FieldMaxLength = 0
        Me.TxtFinder5.FieldName = Nothing
        Me.TxtFinder5.isCalculatedField = False
        Me.TxtFinder5.IsSourceFromTable = False
        Me.TxtFinder5.IsSourceFromValueList = False
        Me.TxtFinder5.IsUnique = False
        Me.TxtFinder5.Location = New System.Drawing.Point(145, 79)
        Me.TxtFinder5.MendatroryField = True
        Me.TxtFinder5.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinder5.MyLinkLable1 = Nothing
        Me.TxtFinder5.MyLinkLable2 = Nothing
        Me.TxtFinder5.MyReadOnly = False
        Me.TxtFinder5.MyShowMasterFormButton = False
        Me.TxtFinder5.Name = "TxtFinder5"
        Me.TxtFinder5.ReferenceFieldDesc = Nothing
        Me.TxtFinder5.ReferenceFieldName = Nothing
        Me.TxtFinder5.ReferenceTableName = Nothing
        Me.TxtFinder5.Size = New System.Drawing.Size(187, 19)
        Me.TxtFinder5.TabIndex = 70
        Me.TxtFinder5.Value = ""
        '
        'MyLabel40
        '
        Me.MyLabel40.AutoSize = False
        Me.MyLabel40.BorderVisible = True
        Me.MyLabel40.FieldName = Nothing
        Me.MyLabel40.Location = New System.Drawing.Point(335, 79)
        Me.MyLabel40.Name = "MyLabel40"
        Me.MyLabel40.Size = New System.Drawing.Size(169, 19)
        Me.MyLabel40.TabIndex = 69
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
        'MyTextBox2
        '
        Me.MyTextBox2.CalculationExpression = Nothing
        Me.MyTextBox2.FieldCode = Nothing
        Me.MyTextBox2.FieldDesc = Nothing
        Me.MyTextBox2.FieldMaxLength = 0
        Me.MyTextBox2.FieldName = Nothing
        Me.MyTextBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyTextBox2.isCalculatedField = False
        Me.MyTextBox2.IsSourceFromTable = False
        Me.MyTextBox2.IsSourceFromValueList = False
        Me.MyTextBox2.IsUnique = False
        Me.MyTextBox2.Location = New System.Drawing.Point(144, 99)
        Me.MyTextBox2.MaxLength = 200
        Me.MyTextBox2.MendatroryField = False
        Me.MyTextBox2.MyLinkLable1 = Nothing
        Me.MyTextBox2.MyLinkLable2 = Nothing
        Me.MyTextBox2.Name = "MyTextBox2"
        Me.MyTextBox2.ReferenceFieldDesc = Nothing
        Me.MyTextBox2.ReferenceFieldName = Nothing
        Me.MyTextBox2.ReferenceTableName = Nothing
        Me.MyTextBox2.Size = New System.Drawing.Size(187, 18)
        Me.MyTextBox2.TabIndex = 68
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
        Me.MyLabel26.Location = New System.Drawing.Point(12, 99)
        Me.MyLabel26.Name = "MyLabel26"
        Me.MyLabel26.Size = New System.Drawing.Size(82, 18)
        Me.MyLabel26.TabIndex = 67
        Me.MyLabel26.Text = "Crop Reg Code"
        '
        'TxtFinder3
        '
        Me.TxtFinder3.CalculationExpression = Nothing
        Me.TxtFinder3.FieldCode = Nothing
        Me.TxtFinder3.FieldDesc = Nothing
        Me.TxtFinder3.FieldMaxLength = 0
        Me.TxtFinder3.FieldName = Nothing
        Me.TxtFinder3.isCalculatedField = False
        Me.TxtFinder3.IsSourceFromTable = False
        Me.TxtFinder3.IsSourceFromValueList = False
        Me.TxtFinder3.IsUnique = False
        Me.TxtFinder3.Location = New System.Drawing.Point(145, 57)
        Me.TxtFinder3.MendatroryField = True
        Me.TxtFinder3.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinder3.MyLinkLable1 = Nothing
        Me.TxtFinder3.MyLinkLable2 = Nothing
        Me.TxtFinder3.MyReadOnly = False
        Me.TxtFinder3.MyShowMasterFormButton = False
        Me.TxtFinder3.Name = "TxtFinder3"
        Me.TxtFinder3.ReferenceFieldDesc = Nothing
        Me.TxtFinder3.ReferenceFieldName = Nothing
        Me.TxtFinder3.ReferenceTableName = Nothing
        Me.TxtFinder3.Size = New System.Drawing.Size(187, 19)
        Me.TxtFinder3.TabIndex = 62
        Me.TxtFinder3.Value = ""
        '
        'MyLabel29
        '
        Me.MyLabel29.AutoSize = False
        Me.MyLabel29.BorderVisible = True
        Me.MyLabel29.FieldName = Nothing
        Me.MyLabel29.Location = New System.Drawing.Point(335, 57)
        Me.MyLabel29.Name = "MyLabel29"
        Me.MyLabel29.Size = New System.Drawing.Size(169, 19)
        Me.MyLabel29.TabIndex = 44
        '
        'UsLock2
        '
        Me.UsLock2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock2.Location = New System.Drawing.Point(429, 11)
        Me.UsLock2.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock2.Name = "UsLock2"
        Me.UsLock2.Size = New System.Drawing.Size(97, 20)
        Me.UsLock2.Status = common.ERPTransactionStatus.Pending
        Me.UsLock2.TabIndex = 26
        '
        'MyLabel33
        '
        Me.MyLabel33.AutoSize = False
        Me.MyLabel33.BorderVisible = True
        Me.MyLabel33.FieldName = Nothing
        Me.MyLabel33.Location = New System.Drawing.Point(335, 35)
        Me.MyLabel33.Name = "MyLabel33"
        Me.MyLabel33.Size = New System.Drawing.Size(169, 19)
        Me.MyLabel33.TabIndex = 24
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
        'TxtFinder4
        '
        Me.TxtFinder4.CalculationExpression = Nothing
        Me.TxtFinder4.FieldCode = Nothing
        Me.TxtFinder4.FieldDesc = Nothing
        Me.TxtFinder4.FieldMaxLength = 0
        Me.TxtFinder4.FieldName = Nothing
        Me.TxtFinder4.isCalculatedField = False
        Me.TxtFinder4.IsSourceFromTable = False
        Me.TxtFinder4.IsSourceFromValueList = False
        Me.TxtFinder4.IsUnique = False
        Me.TxtFinder4.Location = New System.Drawing.Point(145, 35)
        Me.TxtFinder4.MendatroryField = True
        Me.TxtFinder4.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinder4.MyLinkLable1 = Nothing
        Me.TxtFinder4.MyLinkLable2 = Nothing
        Me.TxtFinder4.MyReadOnly = False
        Me.TxtFinder4.MyShowMasterFormButton = False
        Me.TxtFinder4.Name = "TxtFinder4"
        Me.TxtFinder4.ReferenceFieldDesc = Nothing
        Me.TxtFinder4.ReferenceFieldName = Nothing
        Me.TxtFinder4.ReferenceTableName = Nothing
        Me.TxtFinder4.Size = New System.Drawing.Size(187, 19)
        Me.TxtFinder4.TabIndex = 19
        Me.TxtFinder4.Value = ""
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
        'TxtNavigator2
        '
        Me.TxtNavigator2.FieldName = Nothing
        Me.TxtNavigator2.Location = New System.Drawing.Point(145, 12)
        Me.TxtNavigator2.MendatroryField = False
        Me.TxtNavigator2.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.TxtNavigator2.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.TxtNavigator2.MyLinkLable1 = Me.MyLabel25
        Me.TxtNavigator2.MyLinkLable2 = Nothing
        Me.TxtNavigator2.MyMaxLength = 32767
        Me.TxtNavigator2.MyReadOnly = False
        Me.TxtNavigator2.Name = "TxtNavigator2"
        Me.TxtNavigator2.Size = New System.Drawing.Size(256, 20)
        Me.TxtNavigator2.TabIndex = 25
        Me.TxtNavigator2.Value = ""
        '
        'RadButton2
        '
        Me.RadButton2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton2.Image = Global.XpertERPSheed.My.Resources.Resources._new
        Me.RadButton2.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadButton2.Location = New System.Drawing.Point(401, 12)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(21, 20)
        Me.RadButton2.TabIndex = 22
        '
        'txtPolicyNo
        '
        Me.txtPolicyNo.CalculationExpression = Nothing
        Me.txtPolicyNo.FieldCode = Nothing
        Me.txtPolicyNo.FieldDesc = Nothing
        Me.txtPolicyNo.FieldMaxLength = 0
        Me.txtPolicyNo.FieldName = Nothing
        Me.txtPolicyNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPolicyNo.isCalculatedField = False
        Me.txtPolicyNo.IsSourceFromTable = False
        Me.txtPolicyNo.IsSourceFromValueList = False
        Me.txtPolicyNo.IsUnique = False
        Me.txtPolicyNo.Location = New System.Drawing.Point(145, 99)
        Me.txtPolicyNo.MaxLength = 200
        Me.txtPolicyNo.MendatroryField = False
        Me.txtPolicyNo.MyLinkLable1 = Nothing
        Me.txtPolicyNo.MyLinkLable2 = Nothing
        Me.txtPolicyNo.Name = "txtPolicyNo"
        Me.txtPolicyNo.ReferenceFieldDesc = Nothing
        Me.txtPolicyNo.ReferenceFieldName = Nothing
        Me.txtPolicyNo.ReferenceTableName = Nothing
        Me.txtPolicyNo.Size = New System.Drawing.Size(102, 18)
        Me.txtPolicyNo.TabIndex = 68
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
        'txtInsType
        '
        Me.txtInsType.CalculationExpression = Nothing
        Me.txtInsType.FieldCode = Nothing
        Me.txtInsType.FieldDesc = Nothing
        Me.txtInsType.FieldMaxLength = 0
        Me.txtInsType.FieldName = Nothing
        Me.txtInsType.isCalculatedField = False
        Me.txtInsType.IsSourceFromTable = False
        Me.txtInsType.IsSourceFromValueList = False
        Me.txtInsType.IsUnique = False
        Me.txtInsType.Location = New System.Drawing.Point(145, 57)
        Me.txtInsType.MendatroryField = True
        Me.txtInsType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInsType.MyLinkLable1 = Nothing
        Me.txtInsType.MyLinkLable2 = Nothing
        Me.txtInsType.MyReadOnly = False
        Me.txtInsType.MyShowMasterFormButton = False
        Me.txtInsType.Name = "txtInsType"
        Me.txtInsType.ReferenceFieldDesc = Nothing
        Me.txtInsType.ReferenceFieldName = Nothing
        Me.txtInsType.ReferenceTableName = Nothing
        Me.txtInsType.Size = New System.Drawing.Size(187, 19)
        Me.txtInsType.TabIndex = 62
        Me.txtInsType.Value = ""
        '
        'lblInsType
        '
        Me.lblInsType.AutoSize = False
        Me.lblInsType.BorderVisible = True
        Me.lblInsType.FieldName = Nothing
        Me.lblInsType.Location = New System.Drawing.Point(335, 57)
        Me.lblInsType.Name = "lblInsType"
        Me.lblInsType.Size = New System.Drawing.Size(169, 19)
        Me.lblInsType.TabIndex = 44
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblStatus.Location = New System.Drawing.Point(429, 11)
        Me.lblStatus.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(97, 20)
        Me.lblStatus.Status = common.ERPTransactionStatus.Pending
        Me.lblStatus.TabIndex = 26
        '
        'lblInsCompName
        '
        Me.lblInsCompName.AutoSize = False
        Me.lblInsCompName.BorderVisible = True
        Me.lblInsCompName.FieldName = Nothing
        Me.lblInsCompName.Location = New System.Drawing.Point(335, 35)
        Me.lblInsCompName.Name = "lblInsCompName"
        Me.lblInsCompName.Size = New System.Drawing.Size(169, 19)
        Me.lblInsCompName.TabIndex = 24
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
        'txtInsCompany
        '
        Me.txtInsCompany.CalculationExpression = Nothing
        Me.txtInsCompany.FieldCode = Nothing
        Me.txtInsCompany.FieldDesc = Nothing
        Me.txtInsCompany.FieldMaxLength = 0
        Me.txtInsCompany.FieldName = Nothing
        Me.txtInsCompany.isCalculatedField = False
        Me.txtInsCompany.IsSourceFromTable = False
        Me.txtInsCompany.IsSourceFromValueList = False
        Me.txtInsCompany.IsUnique = False
        Me.txtInsCompany.Location = New System.Drawing.Point(145, 35)
        Me.txtInsCompany.MendatroryField = True
        Me.txtInsCompany.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInsCompany.MyLinkLable1 = Nothing
        Me.txtInsCompany.MyLinkLable2 = Nothing
        Me.txtInsCompany.MyReadOnly = False
        Me.txtInsCompany.MyShowMasterFormButton = False
        Me.txtInsCompany.Name = "txtInsCompany"
        Me.txtInsCompany.ReferenceFieldDesc = Nothing
        Me.txtInsCompany.ReferenceFieldName = Nothing
        Me.txtInsCompany.ReferenceTableName = Nothing
        Me.txtInsCompany.Size = New System.Drawing.Size(187, 19)
        Me.txtInsCompany.TabIndex = 19
        Me.txtInsCompany.Value = ""
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
        'txtDocumentNo
        '
        Me.txtDocumentNo.FieldName = Nothing
        Me.txtDocumentNo.Location = New System.Drawing.Point(145, 12)
        Me.txtDocumentNo.MendatroryField = False
        Me.txtDocumentNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocumentNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocumentNo.MyLinkLable1 = Me.lblCode
        Me.txtDocumentNo.MyLinkLable2 = Nothing
        Me.txtDocumentNo.MyMaxLength = 32767
        Me.txtDocumentNo.MyReadOnly = False
        Me.txtDocumentNo.Name = "txtDocumentNo"
        Me.txtDocumentNo.Size = New System.Drawing.Size(256, 20)
        Me.txtDocumentNo.TabIndex = 25
        Me.txtDocumentNo.Value = ""
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
        'MyTextBox1
        '
        Me.MyTextBox1.CalculationExpression = Nothing
        Me.MyTextBox1.FieldCode = Nothing
        Me.MyTextBox1.FieldDesc = Nothing
        Me.MyTextBox1.FieldMaxLength = 0
        Me.MyTextBox1.FieldName = Nothing
        Me.MyTextBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyTextBox1.isCalculatedField = False
        Me.MyTextBox1.IsSourceFromTable = False
        Me.MyTextBox1.IsSourceFromValueList = False
        Me.MyTextBox1.IsUnique = False
        Me.MyTextBox1.Location = New System.Drawing.Point(145, 99)
        Me.MyTextBox1.MaxLength = 200
        Me.MyTextBox1.MendatroryField = False
        Me.MyTextBox1.MyLinkLable1 = Nothing
        Me.MyTextBox1.MyLinkLable2 = Nothing
        Me.MyTextBox1.Name = "MyTextBox1"
        Me.MyTextBox1.ReferenceFieldDesc = Nothing
        Me.MyTextBox1.ReferenceFieldName = Nothing
        Me.MyTextBox1.ReferenceTableName = Nothing
        Me.MyTextBox1.Size = New System.Drawing.Size(102, 18)
        Me.MyTextBox1.TabIndex = 68
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
        'TxtFinder1
        '
        Me.TxtFinder1.CalculationExpression = Nothing
        Me.TxtFinder1.FieldCode = Nothing
        Me.TxtFinder1.FieldDesc = Nothing
        Me.TxtFinder1.FieldMaxLength = 0
        Me.TxtFinder1.FieldName = Nothing
        Me.TxtFinder1.isCalculatedField = False
        Me.TxtFinder1.IsSourceFromTable = False
        Me.TxtFinder1.IsSourceFromValueList = False
        Me.TxtFinder1.IsUnique = False
        Me.TxtFinder1.Location = New System.Drawing.Point(145, 57)
        Me.TxtFinder1.MendatroryField = True
        Me.TxtFinder1.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinder1.MyLinkLable1 = Nothing
        Me.TxtFinder1.MyLinkLable2 = Nothing
        Me.TxtFinder1.MyReadOnly = False
        Me.TxtFinder1.MyShowMasterFormButton = False
        Me.TxtFinder1.Name = "TxtFinder1"
        Me.TxtFinder1.ReferenceFieldDesc = Nothing
        Me.TxtFinder1.ReferenceFieldName = Nothing
        Me.TxtFinder1.ReferenceTableName = Nothing
        Me.TxtFinder1.Size = New System.Drawing.Size(187, 19)
        Me.TxtFinder1.TabIndex = 62
        Me.TxtFinder1.Value = ""
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
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(429, 11)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(97, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 26
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
        'TxtFinder2
        '
        Me.TxtFinder2.CalculationExpression = Nothing
        Me.TxtFinder2.FieldCode = Nothing
        Me.TxtFinder2.FieldDesc = Nothing
        Me.TxtFinder2.FieldMaxLength = 0
        Me.TxtFinder2.FieldName = Nothing
        Me.TxtFinder2.isCalculatedField = False
        Me.TxtFinder2.IsSourceFromTable = False
        Me.TxtFinder2.IsSourceFromValueList = False
        Me.TxtFinder2.IsUnique = False
        Me.TxtFinder2.Location = New System.Drawing.Point(145, 35)
        Me.TxtFinder2.MendatroryField = True
        Me.TxtFinder2.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinder2.MyLinkLable1 = Nothing
        Me.TxtFinder2.MyLinkLable2 = Nothing
        Me.TxtFinder2.MyReadOnly = False
        Me.TxtFinder2.MyShowMasterFormButton = False
        Me.TxtFinder2.Name = "TxtFinder2"
        Me.TxtFinder2.ReferenceFieldDesc = Nothing
        Me.TxtFinder2.ReferenceFieldName = Nothing
        Me.TxtFinder2.ReferenceTableName = Nothing
        Me.TxtFinder2.Size = New System.Drawing.Size(187, 19)
        Me.TxtFinder2.TabIndex = 19
        Me.TxtFinder2.Value = ""
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
        'cmbUsedAs
        '
        Me.cmbUsedAs.AutoCompleteDisplayMember = Nothing
        Me.cmbUsedAs.AutoCompleteValueMember = Nothing
        Me.cmbUsedAs.CalculationExpression = Nothing
        Me.cmbUsedAs.DropDownAnimationEnabled = True
        Me.cmbUsedAs.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbUsedAs.FieldCode = Nothing
        Me.cmbUsedAs.FieldDesc = Nothing
        Me.cmbUsedAs.FieldMaxLength = 0
        Me.cmbUsedAs.FieldName = Nothing
        Me.cmbUsedAs.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbUsedAs.isCalculatedField = False
        Me.cmbUsedAs.IsSourceFromTable = False
        Me.cmbUsedAs.IsSourceFromValueList = False
        Me.cmbUsedAs.IsUnique = False
        RadListDataItem1.Text = "Finished Goods"
        RadListDataItem2.Text = "Promotional Item"
        RadListDataItem3.Text = "Trading Item"
        Me.cmbUsedAs.Items.Add(RadListDataItem1)
        Me.cmbUsedAs.Items.Add(RadListDataItem2)
        Me.cmbUsedAs.Items.Add(RadListDataItem3)
        Me.cmbUsedAs.Location = New System.Drawing.Point(145, 157)
        Me.cmbUsedAs.MendatroryField = True
        Me.cmbUsedAs.MyLinkLable1 = Me.MyLabel10
        Me.cmbUsedAs.MyLinkLable2 = Nothing
        Me.cmbUsedAs.Name = "cmbUsedAs"
        Me.cmbUsedAs.ReferenceFieldDesc = Nothing
        Me.cmbUsedAs.ReferenceFieldName = Nothing
        Me.cmbUsedAs.ReferenceTableName = Nothing
        Me.cmbUsedAs.Size = New System.Drawing.Size(138, 18)
        Me.cmbUsedAs.TabIndex = 1473
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Location = New System.Drawing.Point(12, 157)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel10.TabIndex = 1474
        Me.MyLabel10.Text = "Season"
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
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLeaseLand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOwnLand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFamilyLand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyTextBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyTextBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyTextBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPolicyNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInsType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInsCompName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyTextBox1, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.cmbUsedAs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtPolicyNo As common.Controls.MyTextBox
    Friend WithEvents txtInsType As common.UserControls.txtFinder
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents lblInsType As common.Controls.MyLabel
    Friend WithEvents lblStatus As common.usLock
    Friend WithEvents lblInsCompName As common.Controls.MyLabel
    Friend WithEvents txtInsCompany As common.UserControls.txtFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents txtDocumentNo As common.UserControls.txtNavigator
    Friend WithEvents txtPolicydate As common.Controls.MyDateTimePicker
    Friend WithEvents txtInsEndDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtInsStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents TxtFinder5 As common.UserControls.txtFinder
    Friend WithEvents MyLabel40 As common.Controls.MyLabel
    Friend WithEvents MyLabel41 As common.Controls.MyLabel
    Friend WithEvents MyTextBox2 As common.Controls.MyTextBox
    Friend WithEvents MyLabel25 As common.Controls.MyLabel
    Friend WithEvents TxtFinder3 As common.UserControls.txtFinder
    Friend WithEvents MyLabel29 As common.Controls.MyLabel
    Friend WithEvents UsLock2 As common.usLock
    Friend WithEvents MyLabel33 As common.Controls.MyLabel
    Friend WithEvents MyLabel35 As common.Controls.MyLabel
    Friend WithEvents TxtFinder4 As common.UserControls.txtFinder
    Friend WithEvents MyLabel37 As common.Controls.MyLabel
    Friend WithEvents TxtNavigator2 As common.UserControls.txtNavigator
    Friend WithEvents RadButton2 As RadButton
    Friend WithEvents MyTextBox1 As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents TxtFinder1 As common.UserControls.txtFinder
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents TxtFinder2 As common.UserControls.txtFinder
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents TxtNavigator1 As common.UserControls.txtNavigator
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents MyTextBox3 As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel26 As common.Controls.MyLabel
    Friend WithEvents MyTextBox4 As common.Controls.MyTextBox
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
    Friend WithEvents cmbUsedAs As common.Controls.MyComboBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
End Class
