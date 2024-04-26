Imports Telerik.WinControls.UI
Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBullInsurance
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
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.txtInsType = New common.UserControls.txtFinder()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtTotalAmt = New common.MyNumBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtSerChargeAmt = New common.MyNumBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtSerChargePer = New common.MyNumBox()
        Me.txtPremAmt = New common.MyNumBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtPremiumPer = New common.MyNumBox()
        Me.txtInsAmt = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txInsStartDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txInsEndDate = New common.Controls.MyDateTimePicker()
        Me.lblInsType = New common.Controls.MyLabel()
        Me.lblStatus = New common.usLock()
        Me.lblInsCompName = New common.Controls.MyLabel()
        Me.txtInsCompany = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtPolicydate = New common.Controls.MyDateTimePicker()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.lblCode = New common.Controls.MyLabel()
        Me.txtPolicyNo = New common.UserControls.txtNavigator()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReverseUnpost = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSerChargeAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSerChargePer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPremAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPremiumPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInsAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txInsStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txInsEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInsType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInsCompName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPolicydate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverseUnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
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
        Me.SplitContainer1.SplitterDistance = 444
        Me.SplitContainer1.TabIndex = 13
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtInsType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel11)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel10)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtTotalAmt)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtSerChargeAmt)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtSerChargePer)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtPremAmt)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtPremiumPer)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtInsAmt)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txInsStartDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txInsEndDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblInsType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblStatus)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblInsCompName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtInsCompany)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtPolicydate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtPolicyNo)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(996, 444)
        Me.SplitContainer2.SplitterDistance = 141
        Me.SplitContainer2.TabIndex = 0
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
        Me.txtInsType.Location = New System.Drawing.Point(130, 99)
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
        Me.txtInsType.Size = New System.Drawing.Size(196, 19)
        Me.txtInsType.TabIndex = 62
        Me.txtInsType.Value = ""
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel11.Location = New System.Drawing.Point(11, 101)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel11.TabIndex = 63
        Me.MyLabel11.Text = "Insurance Type"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel10.Location = New System.Drawing.Point(484, 79)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel10.TabIndex = 61
        Me.MyLabel10.Text = "Total Amount"
        '
        'txtTotalAmt
        '
        Me.txtTotalAmt.BackColor = System.Drawing.Color.White
        Me.txtTotalAmt.CalculationExpression = Nothing
        Me.txtTotalAmt.DecimalPlaces = 2
        Me.txtTotalAmt.FieldCode = Nothing
        Me.txtTotalAmt.FieldDesc = Nothing
        Me.txtTotalAmt.FieldMaxLength = 0
        Me.txtTotalAmt.FieldName = Nothing
        Me.txtTotalAmt.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtTotalAmt.isCalculatedField = False
        Me.txtTotalAmt.IsSourceFromTable = False
        Me.txtTotalAmt.IsSourceFromValueList = False
        Me.txtTotalAmt.IsUnique = False
        Me.txtTotalAmt.Location = New System.Drawing.Point(603, 77)
        Me.txtTotalAmt.MendatroryField = False
        Me.txtTotalAmt.MyLinkLable1 = Nothing
        Me.txtTotalAmt.MyLinkLable2 = Nothing
        Me.txtTotalAmt.Name = "txtTotalAmt"
        Me.txtTotalAmt.ReferenceFieldDesc = Nothing
        Me.txtTotalAmt.ReferenceFieldName = Nothing
        Me.txtTotalAmt.ReferenceTableName = Nothing
        Me.txtTotalAmt.Size = New System.Drawing.Size(102, 20)
        Me.txtTotalAmt.TabIndex = 60
        Me.txtTotalAmt.Text = "0"
        Me.txtTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalAmt.Value = 0R
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(229, 79)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(126, 16)
        Me.MyLabel9.TabIndex = 59
        Me.MyLabel9.Text = "Service Charge Amount"
        '
        'txtSerChargeAmt
        '
        Me.txtSerChargeAmt.BackColor = System.Drawing.Color.White
        Me.txtSerChargeAmt.CalculationExpression = Nothing
        Me.txtSerChargeAmt.DecimalPlaces = 2
        Me.txtSerChargeAmt.FieldCode = Nothing
        Me.txtSerChargeAmt.FieldDesc = Nothing
        Me.txtSerChargeAmt.FieldMaxLength = 0
        Me.txtSerChargeAmt.FieldName = Nothing
        Me.txtSerChargeAmt.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtSerChargeAmt.isCalculatedField = False
        Me.txtSerChargeAmt.IsSourceFromTable = False
        Me.txtSerChargeAmt.IsSourceFromValueList = False
        Me.txtSerChargeAmt.IsUnique = False
        Me.txtSerChargeAmt.Location = New System.Drawing.Point(360, 77)
        Me.txtSerChargeAmt.MendatroryField = False
        Me.txtSerChargeAmt.MyLinkLable1 = Nothing
        Me.txtSerChargeAmt.MyLinkLable2 = Nothing
        Me.txtSerChargeAmt.Name = "txtSerChargeAmt"
        Me.txtSerChargeAmt.ReferenceFieldDesc = Nothing
        Me.txtSerChargeAmt.ReferenceFieldName = Nothing
        Me.txtSerChargeAmt.ReferenceTableName = Nothing
        Me.txtSerChargeAmt.Size = New System.Drawing.Size(95, 20)
        Me.txtSerChargeAmt.TabIndex = 58
        Me.txtSerChargeAmt.Text = "0"
        Me.txtSerChargeAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSerChargeAmt.Value = 0R
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(11, 79)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(97, 16)
        Me.MyLabel8.TabIndex = 57
        Me.MyLabel8.Text = "Service Charge %"
        '
        'txtSerChargePer
        '
        Me.txtSerChargePer.BackColor = System.Drawing.Color.White
        Me.txtSerChargePer.CalculationExpression = Nothing
        Me.txtSerChargePer.DecimalPlaces = 2
        Me.txtSerChargePer.FieldCode = Nothing
        Me.txtSerChargePer.FieldDesc = Nothing
        Me.txtSerChargePer.FieldMaxLength = 0
        Me.txtSerChargePer.FieldName = Nothing
        Me.txtSerChargePer.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtSerChargePer.isCalculatedField = False
        Me.txtSerChargePer.IsSourceFromTable = False
        Me.txtSerChargePer.IsSourceFromValueList = False
        Me.txtSerChargePer.IsUnique = False
        Me.txtSerChargePer.Location = New System.Drawing.Point(130, 77)
        Me.txtSerChargePer.MendatroryField = False
        Me.txtSerChargePer.MyLinkLable1 = Nothing
        Me.txtSerChargePer.MyLinkLable2 = Nothing
        Me.txtSerChargePer.Name = "txtSerChargePer"
        Me.txtSerChargePer.ReferenceFieldDesc = Nothing
        Me.txtSerChargePer.ReferenceFieldName = Nothing
        Me.txtSerChargePer.ReferenceTableName = Nothing
        Me.txtSerChargePer.Size = New System.Drawing.Size(93, 20)
        Me.txtSerChargePer.TabIndex = 56
        Me.txtSerChargePer.Text = "0"
        Me.txtSerChargePer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSerChargePer.Value = 0R
        '
        'txtPremAmt
        '
        Me.txtPremAmt.BackColor = System.Drawing.Color.White
        Me.txtPremAmt.CalculationExpression = Nothing
        Me.txtPremAmt.DecimalPlaces = 3
        Me.txtPremAmt.FieldCode = Nothing
        Me.txtPremAmt.FieldDesc = Nothing
        Me.txtPremAmt.FieldMaxLength = 0
        Me.txtPremAmt.FieldName = Nothing
        Me.txtPremAmt.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtPremAmt.isCalculatedField = False
        Me.txtPremAmt.IsSourceFromTable = False
        Me.txtPremAmt.IsSourceFromValueList = False
        Me.txtPremAmt.IsUnique = False
        Me.txtPremAmt.Location = New System.Drawing.Point(603, 55)
        Me.txtPremAmt.MendatroryField = False
        Me.txtPremAmt.MyLinkLable1 = Nothing
        Me.txtPremAmt.MyLinkLable2 = Nothing
        Me.txtPremAmt.Name = "txtPremAmt"
        Me.txtPremAmt.ReadOnly = True
        Me.txtPremAmt.ReferenceFieldDesc = Nothing
        Me.txtPremAmt.ReferenceFieldName = Nothing
        Me.txtPremAmt.ReferenceTableName = Nothing
        Me.txtPremAmt.Size = New System.Drawing.Size(102, 20)
        Me.txtPremAmt.TabIndex = 54
        Me.txtPremAmt.Text = "0"
        Me.txtPremAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPremAmt.Value = 0R
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(484, 57)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel7.TabIndex = 55
        Me.MyLabel7.Text = "Premium Amount"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(229, 57)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel5.TabIndex = 53
        Me.MyLabel5.Text = "Premium %"
        '
        'txtPremiumPer
        '
        Me.txtPremiumPer.BackColor = System.Drawing.Color.White
        Me.txtPremiumPer.CalculationExpression = Nothing
        Me.txtPremiumPer.DecimalPlaces = 2
        Me.txtPremiumPer.FieldCode = Nothing
        Me.txtPremiumPer.FieldDesc = Nothing
        Me.txtPremiumPer.FieldMaxLength = 0
        Me.txtPremiumPer.FieldName = Nothing
        Me.txtPremiumPer.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtPremiumPer.isCalculatedField = False
        Me.txtPremiumPer.IsSourceFromTable = False
        Me.txtPremiumPer.IsSourceFromValueList = False
        Me.txtPremiumPer.IsUnique = False
        Me.txtPremiumPer.Location = New System.Drawing.Point(360, 55)
        Me.txtPremiumPer.MendatroryField = False
        Me.txtPremiumPer.MyLinkLable1 = Nothing
        Me.txtPremiumPer.MyLinkLable2 = Nothing
        Me.txtPremiumPer.Name = "txtPremiumPer"
        Me.txtPremiumPer.ReferenceFieldDesc = Nothing
        Me.txtPremiumPer.ReferenceFieldName = Nothing
        Me.txtPremiumPer.ReferenceTableName = Nothing
        Me.txtPremiumPer.Size = New System.Drawing.Size(95, 20)
        Me.txtPremiumPer.TabIndex = 52
        Me.txtPremiumPer.Text = "0"
        Me.txtPremiumPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPremiumPer.Value = 0R
        '
        'txtInsAmt
        '
        Me.txtInsAmt.BackColor = System.Drawing.Color.White
        Me.txtInsAmt.CalculationExpression = Nothing
        Me.txtInsAmt.DecimalPlaces = 3
        Me.txtInsAmt.FieldCode = Nothing
        Me.txtInsAmt.FieldDesc = Nothing
        Me.txtInsAmt.FieldMaxLength = 0
        Me.txtInsAmt.FieldName = Nothing
        Me.txtInsAmt.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtInsAmt.isCalculatedField = False
        Me.txtInsAmt.IsSourceFromTable = False
        Me.txtInsAmt.IsSourceFromValueList = False
        Me.txtInsAmt.IsUnique = False
        Me.txtInsAmt.Location = New System.Drawing.Point(130, 55)
        Me.txtInsAmt.MendatroryField = False
        Me.txtInsAmt.MyLinkLable1 = Nothing
        Me.txtInsAmt.MyLinkLable2 = Nothing
        Me.txtInsAmt.Name = "txtInsAmt"
        Me.txtInsAmt.ReadOnly = True
        Me.txtInsAmt.ReferenceFieldDesc = Nothing
        Me.txtInsAmt.ReferenceFieldName = Nothing
        Me.txtInsAmt.ReferenceTableName = Nothing
        Me.txtInsAmt.Size = New System.Drawing.Size(93, 20)
        Me.txtInsAmt.TabIndex = 1
        Me.txtInsAmt.Text = "0"
        Me.txtInsAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtInsAmt.Value = 0R
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(11, 57)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel4.TabIndex = 51
        Me.MyLabel4.Text = "Insured Amount"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(577, 11)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(110, 16)
        Me.MyLabel1.TabIndex = 50
        Me.MyLabel1.Text = "Insurance Start Date"
        '
        'txInsStartDate
        '
        Me.txInsStartDate.CalculationExpression = Nothing
        Me.txInsStartDate.CustomFormat = "dd/MM/yyyy"
        Me.txInsStartDate.FieldCode = Nothing
        Me.txInsStartDate.FieldDesc = Nothing
        Me.txInsStartDate.FieldMaxLength = 0
        Me.txInsStartDate.FieldName = Nothing
        Me.txInsStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txInsStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txInsStartDate.isCalculatedField = False
        Me.txInsStartDate.IsSourceFromTable = False
        Me.txInsStartDate.IsSourceFromValueList = False
        Me.txInsStartDate.IsUnique = False
        Me.txInsStartDate.Location = New System.Drawing.Point(694, 10)
        Me.txInsStartDate.MendatroryField = True
        Me.txInsStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txInsStartDate.MyLinkLable1 = Me.MyLabel1
        Me.txInsStartDate.MyLinkLable2 = Nothing
        Me.txInsStartDate.Name = "txInsStartDate"
        Me.txInsStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txInsStartDate.ReferenceFieldDesc = Nothing
        Me.txInsStartDate.ReferenceFieldName = Nothing
        Me.txInsStartDate.ReferenceTableName = Nothing
        Me.txInsStartDate.Size = New System.Drawing.Size(90, 18)
        Me.txInsStartDate.TabIndex = 49
        Me.txInsStartDate.TabStop = False
        Me.txInsStartDate.Text = "13/06/2011"
        Me.txInsStartDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(11, 35)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(106, 16)
        Me.MyLabel3.TabIndex = 48
        Me.MyLabel3.Text = "Insurance End Date"
        '
        'txInsEndDate
        '
        Me.txInsEndDate.CalculationExpression = Nothing
        Me.txInsEndDate.CustomFormat = "dd/MM/yyyy"
        Me.txInsEndDate.FieldCode = Nothing
        Me.txInsEndDate.FieldDesc = Nothing
        Me.txInsEndDate.FieldMaxLength = 0
        Me.txInsEndDate.FieldName = Nothing
        Me.txInsEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txInsEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txInsEndDate.isCalculatedField = False
        Me.txInsEndDate.IsSourceFromTable = False
        Me.txInsEndDate.IsSourceFromValueList = False
        Me.txInsEndDate.IsUnique = False
        Me.txInsEndDate.Location = New System.Drawing.Point(130, 33)
        Me.txInsEndDate.MendatroryField = True
        Me.txInsEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txInsEndDate.MyLinkLable1 = Me.MyLabel3
        Me.txInsEndDate.MyLinkLable2 = Nothing
        Me.txInsEndDate.Name = "txInsEndDate"
        Me.txInsEndDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txInsEndDate.ReferenceFieldDesc = Nothing
        Me.txInsEndDate.ReferenceFieldName = Nothing
        Me.txInsEndDate.ReferenceTableName = Nothing
        Me.txInsEndDate.Size = New System.Drawing.Size(92, 18)
        Me.txInsEndDate.TabIndex = 47
        Me.txInsEndDate.TabStop = False
        Me.txInsEndDate.Text = "13/06/2011"
        Me.txInsEndDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblInsType
        '
        Me.lblInsType.AutoSize = False
        Me.lblInsType.BorderVisible = True
        Me.lblInsType.FieldName = Nothing
        Me.lblInsType.Location = New System.Drawing.Point(328, 99)
        Me.lblInsType.Name = "lblInsType"
        Me.lblInsType.Size = New System.Drawing.Size(188, 19)
        Me.lblInsType.TabIndex = 44
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblStatus.Location = New System.Drawing.Point(790, 10)
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
        Me.lblInsCompName.Location = New System.Drawing.Point(549, 34)
        Me.lblInsCompName.Name = "lblInsCompName"
        Me.lblInsCompName.Size = New System.Drawing.Size(156, 19)
        Me.lblInsCompName.TabIndex = 24
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
        Me.txtInsCompany.Location = New System.Drawing.Point(361, 34)
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
        Me.MyLabel6.Location = New System.Drawing.Point(229, 35)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(108, 16)
        Me.MyLabel6.TabIndex = 20
        Me.MyLabel6.Text = "Insurance Company"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(409, 11)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel2.TabIndex = 23
        Me.MyLabel2.Text = "Policy Date"
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
        Me.txtPolicydate.Location = New System.Drawing.Point(481, 10)
        Me.txtPolicydate.MendatroryField = True
        Me.txtPolicydate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPolicydate.MyLinkLable1 = Me.MyLabel2
        Me.txtPolicydate.MyLinkLable2 = Nothing
        Me.txtPolicydate.Name = "txtPolicydate"
        Me.txtPolicydate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPolicydate.ReferenceFieldDesc = Nothing
        Me.txtPolicydate.ReferenceFieldName = Nothing
        Me.txtPolicydate.ReferenceTableName = Nothing
        Me.txtPolicydate.Size = New System.Drawing.Size(90, 18)
        Me.txtPolicydate.TabIndex = 18
        Me.txtPolicydate.TabStop = False
        Me.txtPolicydate.Text = "13/06/2011"
        Me.txtPolicydate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertErpBullManagement.My.Resources.Resources.new1
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(387, 10)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(21, 19)
        Me.btnAddNew.TabIndex = 22
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Location = New System.Drawing.Point(11, 13)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(54, 18)
        Me.lblCode.TabIndex = 21
        Me.lblCode.Text = "Policy No"
        '
        'txtPolicyNo
        '
        Me.txtPolicyNo.FieldName = Nothing
        Me.txtPolicyNo.Location = New System.Drawing.Point(130, 10)
        Me.txtPolicyNo.MendatroryField = False
        Me.txtPolicyNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtPolicyNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtPolicyNo.MyLinkLable1 = Me.lblCode
        Me.txtPolicyNo.MyLinkLable2 = Nothing
        Me.txtPolicyNo.MyMaxLength = 32767
        Me.txtPolicyNo.MyReadOnly = False
        Me.txtPolicyNo.Name = "txtPolicyNo"
        Me.txtPolicyNo.Size = New System.Drawing.Size(256, 20)
        Me.txtPolicyNo.TabIndex = 25
        Me.txtPolicyNo.Value = ""
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(996, 299)
        Me.gv1.TabIndex = 2
        Me.gv1.TabStop = False
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(220, 12)
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
        Me.btnReverseUnpost.Location = New System.Drawing.Point(322, 12)
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
        Me.btnPost.Location = New System.Drawing.Point(147, 12)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 20)
        Me.btnPost.TabIndex = 159
        Me.btnPost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(922, 12)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(69, 20)
        Me.btnclose.TabIndex = 158
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(76, 12)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(69, 20)
        Me.btndelete.TabIndex = 157
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(5, 12)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(69, 20)
        Me.btnsave.TabIndex = 156
        Me.btnsave.Text = "Save"
        '
        'frmBullInsurance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(996, 509)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmBullInsurance"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bulk Sale Freight Master"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSerChargeAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSerChargePer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPremAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPremiumPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInsAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txInsStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txInsEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInsType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInsCompName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPolicydate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverseUnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents lblStatus As common.usLock
    Friend WithEvents lblInsCompName As common.Controls.MyLabel
    Friend WithEvents txtInsCompany As common.UserControls.txtFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtPolicydate As common.Controls.MyDateTimePicker
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents txtPolicyNo As common.UserControls.txtNavigator
    Friend WithEvents RadSplitButton1 As RadSplitButton
    Friend WithEvents btnExcel As RadMenuItem
    Friend WithEvents btnPDF As RadMenuItem
    Friend WithEvents btnReverseUnpost As RadButton
    Friend WithEvents btnPost As RadButton
    Friend WithEvents btnclose As RadButton
    Friend WithEvents btndelete As RadButton
    Friend WithEvents btnsave As RadButton
    Friend WithEvents lblInsType As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txInsEndDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txInsStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtInsAmt As common.MyNumBox
    Friend WithEvents txtPremiumPer As common.MyNumBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtPremAmt As common.MyNumBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtSerChargePer As common.MyNumBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtSerChargeAmt As common.MyNumBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtTotalAmt As common.MyNumBox
    Friend WithEvents txtInsType As common.UserControls.txtFinder
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
End Class
