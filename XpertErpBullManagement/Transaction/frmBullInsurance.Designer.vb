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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.txtInsType = New common.UserControls.txtFinder()
        Me.lblSerChargeAmt = New common.Controls.MyLabel()
        Me.lblTotalAmt = New common.Controls.MyLabel()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.lblPremAmt = New common.Controls.MyLabel()
        Me.txtInsAmt = New common.MyNumBox()
        Me.txtSerChargePer = New common.MyNumBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtInsStartDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtPremiumPer = New common.MyNumBox()
        Me.txtInsEndDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblInsType = New common.Controls.MyLabel()
        Me.lblStatus = New common.usLock()
        Me.lblInsCompName = New common.Controls.MyLabel()
        Me.txtInsCompany = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtPolicydate = New common.Controls.MyDateTimePicker()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.lblCode = New common.Controls.MyLabel()
        Me.txtDocumentNo = New common.UserControls.txtNavigator()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReverseUnpost = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtPolicyNo = New common.Controls.MyTextBox()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblSerChargeAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPremAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInsAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSerChargePer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInsStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPremiumPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInsEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPolicyNo, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtPolicyNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel12)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtInsType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblSerChargeAmt)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblTotalAmt)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel11)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel10)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblPremAmt)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtInsAmt)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtSerChargePer)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtInsStartDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtPremiumPer)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtInsEndDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblInsType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblStatus)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblInsCompName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtInsCompany)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtPolicydate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDocumentNo)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(996, 444)
        Me.SplitContainer2.SplitterDistance = 222
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
        Me.txtInsType.Location = New System.Drawing.Point(144, 55)
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
        'lblSerChargeAmt
        '
        Me.lblSerChargeAmt.AutoSize = False
        Me.lblSerChargeAmt.BorderVisible = True
        Me.lblSerChargeAmt.FieldName = Nothing
        Me.lblSerChargeAmt.Location = New System.Drawing.Point(144, 167)
        Me.lblSerChargeAmt.Name = "lblSerChargeAmt"
        Me.lblSerChargeAmt.Size = New System.Drawing.Size(102, 20)
        Me.lblSerChargeAmt.TabIndex = 66
        '
        'lblTotalAmt
        '
        Me.lblTotalAmt.AutoSize = False
        Me.lblTotalAmt.BorderVisible = True
        Me.lblTotalAmt.FieldName = Nothing
        Me.lblTotalAmt.Location = New System.Drawing.Point(390, 165)
        Me.lblTotalAmt.Name = "lblTotalAmt"
        Me.lblTotalAmt.Size = New System.Drawing.Size(102, 20)
        Me.lblTotalAmt.TabIndex = 65
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel11.Location = New System.Drawing.Point(11, 57)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel11.TabIndex = 63
        Me.MyLabel11.Text = "Insurance Type"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel10.Location = New System.Drawing.Point(269, 167)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel10.TabIndex = 61
        Me.MyLabel10.Text = "Total Amount"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(11, 101)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(110, 16)
        Me.MyLabel1.TabIndex = 50
        Me.MyLabel1.Text = "Insurance Start Date"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(11, 167)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(126, 16)
        Me.MyLabel9.TabIndex = 59
        Me.MyLabel9.Text = "Service Charge Amount"
        '
        'lblPremAmt
        '
        Me.lblPremAmt.AutoSize = False
        Me.lblPremAmt.BorderVisible = True
        Me.lblPremAmt.FieldName = Nothing
        Me.lblPremAmt.Location = New System.Drawing.Point(144, 143)
        Me.lblPremAmt.Name = "lblPremAmt"
        Me.lblPremAmt.Size = New System.Drawing.Size(102, 20)
        Me.lblPremAmt.TabIndex = 64
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
        Me.txtInsAmt.Location = New System.Drawing.Point(144, 121)
        Me.txtInsAmt.MendatroryField = False
        Me.txtInsAmt.MyLinkLable1 = Nothing
        Me.txtInsAmt.MyLinkLable2 = Nothing
        Me.txtInsAmt.Name = "txtInsAmt"
        Me.txtInsAmt.ReadOnly = False
        Me.txtInsAmt.ReferenceFieldDesc = Nothing
        Me.txtInsAmt.ReferenceFieldName = Nothing
        Me.txtInsAmt.ReferenceTableName = Nothing
        Me.txtInsAmt.Size = New System.Drawing.Size(102, 20)
        Me.txtInsAmt.TabIndex = 1
        Me.txtInsAmt.Text = "0"
        Me.txtInsAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtInsAmt.Value = 0R
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
        Me.txtSerChargePer.Location = New System.Drawing.Point(390, 143)
        Me.txtSerChargePer.MendatroryField = False
        Me.txtSerChargePer.MyLinkLable1 = Nothing
        Me.txtSerChargePer.MyLinkLable2 = Nothing
        Me.txtSerChargePer.Name = "txtSerChargePer"
        Me.txtSerChargePer.ReferenceFieldDesc = Nothing
        Me.txtSerChargePer.ReferenceFieldName = Nothing
        Me.txtSerChargePer.ReferenceTableName = Nothing
        Me.txtSerChargePer.Size = New System.Drawing.Size(102, 20)
        Me.txtSerChargePer.TabIndex = 56
        Me.txtSerChargePer.Text = "0"
        Me.txtSerChargePer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSerChargePer.Value = 0R
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(11, 145)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel7.TabIndex = 55
        Me.MyLabel7.Text = "Premium Amount"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(269, 145)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(97, 16)
        Me.MyLabel8.TabIndex = 57
        Me.MyLabel8.Text = "Service Charge %"
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
        Me.txtInsStartDate.MyLinkLable1 = Me.MyLabel1
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
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(269, 101)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(106, 16)
        Me.MyLabel3.TabIndex = 48
        Me.MyLabel3.Text = "Insurance End Date"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(11, 123)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel4.TabIndex = 51
        Me.MyLabel4.Text = "Insured Amount"
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
        Me.txtPremiumPer.Location = New System.Drawing.Point(390, 121)
        Me.txtPremiumPer.MendatroryField = False
        Me.txtPremiumPer.MyLinkLable1 = Nothing
        Me.txtPremiumPer.MyLinkLable2 = Nothing
        Me.txtPremiumPer.Name = "txtPremiumPer"
        Me.txtPremiumPer.ReferenceFieldDesc = Nothing
        Me.txtPremiumPer.ReferenceFieldName = Nothing
        Me.txtPremiumPer.ReferenceTableName = Nothing
        Me.txtPremiumPer.Size = New System.Drawing.Size(102, 20)
        Me.txtPremiumPer.TabIndex = 52
        Me.txtPremiumPer.Text = "0"
        Me.txtPremiumPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPremiumPer.Value = 0R
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
        Me.txtInsEndDate.Location = New System.Drawing.Point(390, 99)
        Me.txtInsEndDate.MendatroryField = True
        Me.txtInsEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInsEndDate.MyLinkLable1 = Me.MyLabel3
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
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(269, 123)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel5.TabIndex = 53
        Me.MyLabel5.Text = "Premium %"
        '
        'lblInsType
        '
        Me.lblInsType.AutoSize = False
        Me.lblInsType.BorderVisible = True
        Me.lblInsType.FieldName = Nothing
        Me.lblInsType.Location = New System.Drawing.Point(334, 55)
        Me.lblInsType.Name = "lblInsType"
        Me.lblInsType.Size = New System.Drawing.Size(156, 19)
        Me.lblInsType.TabIndex = 44
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblStatus.Location = New System.Drawing.Point(428, 9)
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
        Me.lblInsCompName.Location = New System.Drawing.Point(334, 33)
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
        Me.txtInsCompany.Location = New System.Drawing.Point(144, 33)
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
        Me.MyLabel6.Location = New System.Drawing.Point(11, 35)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(108, 16)
        Me.MyLabel6.TabIndex = 20
        Me.MyLabel6.Text = "Insurance Company"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(11, 79)
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
        Me.txtPolicydate.Location = New System.Drawing.Point(144, 77)
        Me.txtPolicydate.MendatroryField = True
        Me.txtPolicydate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPolicydate.MyLinkLable1 = Me.MyLabel2
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
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertErpBullManagement.My.Resources.Resources.new1
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(401, 10)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(21, 19)
        Me.btnAddNew.TabIndex = 22
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Location = New System.Drawing.Point(11, 13)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(77, 18)
        Me.lblCode.TabIndex = 21
        Me.lblCode.Text = "Document No"
        '
        'txtDocumentNo
        '
        Me.txtDocumentNo.FieldName = Nothing
        Me.txtDocumentNo.Location = New System.Drawing.Point(144, 10)
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
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(996, 218)
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
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Location = New System.Drawing.Point(269, 79)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(54, 18)
        Me.MyLabel12.TabIndex = 67
        Me.MyLabel12.Text = "Policy No"
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
        Me.txtPolicyNo.Location = New System.Drawing.Point(390, 77)
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
        Me.Text = "Bull Insurance"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblSerChargeAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPremAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInsAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSerChargePer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInsStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPremiumPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInsEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPolicyNo, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtDocumentNo As common.UserControls.txtNavigator
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
    Friend WithEvents txtInsEndDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtInsStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtInsAmt As common.MyNumBox
    Friend WithEvents txtPremiumPer As common.MyNumBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtSerChargePer As common.MyNumBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtInsType As common.UserControls.txtFinder
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents lblPremAmt As common.Controls.MyLabel
    Friend WithEvents lblTotalAmt As common.Controls.MyLabel
    Friend WithEvents lblSerChargeAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtPolicyNo As common.Controls.MyTextBox
End Class
