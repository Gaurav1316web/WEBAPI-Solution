<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCapexBudget
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCapexBudget))
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtCapexAmount = New common.MyNumBox()
        Me.lblCapexAmount = New common.Controls.MyLabel()
        Me.txtCapexBalance = New common.MyNumBox()
        Me.lblCapexBalance = New common.Controls.MyLabel()
        Me.MyLabel28 = New common.Controls.MyLabel()
        Me.lblcurrentBudget = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.NumIncBudget = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblIncBudget = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.txt_revisionno = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.txtCapexCode = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtdate = New common.Controls.MyDateTimePicker()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtCapexName = New common.Controls.MyLabel()
        Me.txt_tolerence = New common.MyNumBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txt_budget = New common.MyNumBox()
        Me.txt_revisedbudget = New common.MyNumBox()
        Me.BtnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.chkProvisional = New Telerik.WinControls.UI.RadCheckBox()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.txtCapexAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCapexAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCapexBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCapexBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcurrentBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumIncBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIncBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_revisionno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCapexName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_tolerence, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_budget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_revisedbudget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkProvisional, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(802, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
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
        Me.RadMenuItem4.AccessibleDescription = "Close"
        Me.RadMenuItem4.AccessibleName = "Close"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Close"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(2)
        Me.SplitContainer1.Size = New System.Drawing.Size(802, 248)
        Me.SplitContainer1.SplitterDistance = 215
        Me.SplitContainer1.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkProvisional)
        Me.Panel1.Controls.Add(Me.txtCapexAmount)
        Me.Panel1.Controls.Add(Me.lblCapexAmount)
        Me.Panel1.Controls.Add(Me.txtCapexBalance)
        Me.Panel1.Controls.Add(Me.lblCapexBalance)
        Me.Panel1.Controls.Add(Me.MyLabel28)
        Me.Panel1.Controls.Add(Me.lblcurrentBudget)
        Me.Panel1.Controls.Add(Me.MyLabel4)
        Me.Panel1.Controls.Add(Me.NumIncBudget)
        Me.Panel1.Controls.Add(Me.lblIncBudget)
        Me.Panel1.Controls.Add(Me.RadLabel1)
        Me.Panel1.Controls.Add(Me.MyLabel5)
        Me.Panel1.Controls.Add(Me.txtCode)
        Me.Panel1.Controls.Add(Me.txt_revisionno)
        Me.Panel1.Controls.Add(Me.btnNew)
        Me.Panel1.Controls.Add(Me.txtDesc)
        Me.Panel1.Controls.Add(Me.txtCapexCode)
        Me.Panel1.Controls.Add(Me.RadLabel2)
        Me.Panel1.Controls.Add(Me.MyLabel1)
        Me.Panel1.Controls.Add(Me.txtdate)
        Me.Panel1.Controls.Add(Me.txtCapexName)
        Me.Panel1.Controls.Add(Me.MyLabel12)
        Me.Panel1.Controls.Add(Me.txt_tolerence)
        Me.Panel1.Controls.Add(Me.txt_budget)
        Me.Panel1.Controls.Add(Me.txt_revisedbudget)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(2, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(798, 211)
        Me.Panel1.TabIndex = 193
        '
        'txtCapexAmount
        '
        Me.txtCapexAmount.BackColor = System.Drawing.Color.White
        Me.txtCapexAmount.CalculationExpression = Nothing
        Me.txtCapexAmount.DecimalPlaces = 2
        Me.txtCapexAmount.FieldCode = Nothing
        Me.txtCapexAmount.FieldDesc = Nothing
        Me.txtCapexAmount.FieldMaxLength = 100
        Me.txtCapexAmount.FieldName = Nothing
        Me.txtCapexAmount.isCalculatedField = False
        Me.txtCapexAmount.IsSourceFromTable = False
        Me.txtCapexAmount.IsSourceFromValueList = False
        Me.txtCapexAmount.IsUnique = False
        Me.txtCapexAmount.Location = New System.Drawing.Point(608, 88)
        Me.txtCapexAmount.MendatroryField = False
        Me.txtCapexAmount.MyLinkLable1 = Me.lblCapexAmount
        Me.txtCapexAmount.MyLinkLable2 = Nothing
        Me.txtCapexAmount.Name = "txtCapexAmount"
        Me.txtCapexAmount.ReadOnly = True
        Me.txtCapexAmount.ReferenceFieldDesc = Nothing
        Me.txtCapexAmount.ReferenceFieldName = Nothing
        Me.txtCapexAmount.ReferenceTableName = Nothing
        Me.txtCapexAmount.Size = New System.Drawing.Size(128, 20)
        Me.txtCapexAmount.TabIndex = 209
        Me.txtCapexAmount.Text = "0"
        Me.txtCapexAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCapexAmount.Value = 0.0R
        '
        'lblCapexAmount
        '
        Me.lblCapexAmount.FieldName = Nothing
        Me.lblCapexAmount.Location = New System.Drawing.Point(512, 90)
        Me.lblCapexAmount.Name = "lblCapexAmount"
        Me.lblCapexAmount.Size = New System.Drawing.Size(80, 18)
        Me.lblCapexAmount.TabIndex = 210
        Me.lblCapexAmount.Text = "Capex Amount"
        '
        'txtCapexBalance
        '
        Me.txtCapexBalance.BackColor = System.Drawing.Color.White
        Me.txtCapexBalance.CalculationExpression = Nothing
        Me.txtCapexBalance.DecimalPlaces = 2
        Me.txtCapexBalance.FieldCode = Nothing
        Me.txtCapexBalance.FieldDesc = Nothing
        Me.txtCapexBalance.FieldMaxLength = 100
        Me.txtCapexBalance.FieldName = Nothing
        Me.txtCapexBalance.isCalculatedField = False
        Me.txtCapexBalance.IsSourceFromTable = False
        Me.txtCapexBalance.IsSourceFromValueList = False
        Me.txtCapexBalance.IsUnique = False
        Me.txtCapexBalance.Location = New System.Drawing.Point(608, 111)
        Me.txtCapexBalance.MendatroryField = False
        Me.txtCapexBalance.MyLinkLable1 = Me.lblCapexBalance
        Me.txtCapexBalance.MyLinkLable2 = Nothing
        Me.txtCapexBalance.Name = "txtCapexBalance"
        Me.txtCapexBalance.ReadOnly = True
        Me.txtCapexBalance.ReferenceFieldDesc = Nothing
        Me.txtCapexBalance.ReferenceFieldName = Nothing
        Me.txtCapexBalance.ReferenceTableName = Nothing
        Me.txtCapexBalance.Size = New System.Drawing.Size(128, 20)
        Me.txtCapexBalance.TabIndex = 207
        Me.txtCapexBalance.Text = "0"
        Me.txtCapexBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCapexBalance.Value = 0.0R
        '
        'lblCapexBalance
        '
        Me.lblCapexBalance.FieldName = Nothing
        Me.lblCapexBalance.Location = New System.Drawing.Point(512, 113)
        Me.lblCapexBalance.Name = "lblCapexBalance"
        Me.lblCapexBalance.Size = New System.Drawing.Size(78, 18)
        Me.lblCapexBalance.TabIndex = 208
        Me.lblCapexBalance.Text = "Capex Balance"
        '
        'MyLabel28
        '
        Me.MyLabel28.FieldName = Nothing
        Me.MyLabel28.Location = New System.Drawing.Point(17, 89)
        Me.MyLabel28.Name = "MyLabel28"
        Me.MyLabel28.Size = New System.Drawing.Size(67, 18)
        Me.MyLabel28.TabIndex = 195
        Me.MyLabel28.Text = "Org. Budget"
        '
        'lblcurrentBudget
        '
        Me.lblcurrentBudget.AutoSize = False
        Me.lblcurrentBudget.BorderVisible = True
        Me.lblcurrentBudget.FieldName = Nothing
        Me.lblcurrentBudget.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcurrentBudget.Location = New System.Drawing.Point(148, 112)
        Me.lblcurrentBudget.Name = "lblcurrentBudget"
        Me.lblcurrentBudget.Size = New System.Drawing.Size(130, 20)
        Me.lblcurrentBudget.TabIndex = 206
        Me.lblcurrentBudget.Text = "0"
        Me.lblcurrentBudget.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(16, 111)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(83, 18)
        Me.MyLabel4.TabIndex = 205
        Me.MyLabel4.Text = "Current Budget"
        '
        'NumIncBudget
        '
        Me.NumIncBudget.BackColor = System.Drawing.Color.White
        Me.NumIncBudget.CalculationExpression = Nothing
        Me.NumIncBudget.DecimalPlaces = 2
        Me.NumIncBudget.FieldCode = Nothing
        Me.NumIncBudget.FieldDesc = Nothing
        Me.NumIncBudget.FieldMaxLength = 100
        Me.NumIncBudget.FieldName = Nothing
        Me.NumIncBudget.isCalculatedField = False
        Me.NumIncBudget.IsSourceFromTable = False
        Me.NumIncBudget.IsSourceFromValueList = False
        Me.NumIncBudget.IsUnique = False
        Me.NumIncBudget.Location = New System.Drawing.Point(391, 88)
        Me.NumIncBudget.MendatroryField = False
        Me.NumIncBudget.MyLinkLable1 = Me.MyLabel2
        Me.NumIncBudget.MyLinkLable2 = Nothing
        Me.NumIncBudget.Name = "NumIncBudget"
        Me.NumIncBudget.ReferenceFieldDesc = Nothing
        Me.NumIncBudget.ReferenceFieldName = Nothing
        Me.NumIncBudget.ReferenceTableName = Nothing
        Me.NumIncBudget.Size = New System.Drawing.Size(115, 20)
        Me.NumIncBudget.TabIndex = 204
        Me.NumIncBudget.Text = "0"
        Me.NumIncBudget.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumIncBudget.Value = 0.0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(282, 114)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(84, 18)
        Me.MyLabel2.TabIndex = 187
        Me.MyLabel2.Text = "Revised Budget"
        '
        'lblIncBudget
        '
        Me.lblIncBudget.FieldName = Nothing
        Me.lblIncBudget.Location = New System.Drawing.Point(281, 89)
        Me.lblIncBudget.Name = "lblIncBudget"
        Me.lblIncBudget.Size = New System.Drawing.Size(104, 18)
        Me.lblIncBudget.TabIndex = 203
        Me.lblIncBudget.Text = "Incremental Budget"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(17, 16)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 7
        Me.RadLabel1.Text = "Code"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(17, 159)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(66, 18)
        Me.MyLabel5.TabIndex = 190
        Me.MyLabel5.Text = "Revision No"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(149, 15)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.RadLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(219, 21)
        Me.txtCode.TabIndex = 6
        Me.txtCode.Value = ""
        '
        'txt_revisionno
        '
        Me.txt_revisionno.AutoSize = False
        Me.txt_revisionno.BorderVisible = True
        Me.txt_revisionno.FieldName = Nothing
        Me.txt_revisionno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_revisionno.Location = New System.Drawing.Point(148, 159)
        Me.txt_revisionno.Name = "txt_revisionno"
        Me.txt_revisionno.Size = New System.Drawing.Size(126, 20)
        Me.txt_revisionno.TabIndex = 192
        Me.txt_revisionno.Text = "0"
        Me.txt_revisionno.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(368, 16)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 8
        Me.btnNew.Text = " "
        '
        'txtDesc
        '
        Me.txtDesc.CalculationExpression = Nothing
        Me.txtDesc.FieldCode = Nothing
        Me.txtDesc.FieldDesc = Nothing
        Me.txtDesc.FieldMaxLength = 0
        Me.txtDesc.FieldName = Nothing
        Me.txtDesc.isCalculatedField = False
        Me.txtDesc.IsSourceFromTable = False
        Me.txtDesc.IsSourceFromValueList = False
        Me.txtDesc.IsUnique = False
        Me.txtDesc.Location = New System.Drawing.Point(149, 40)
        Me.txtDesc.MaxLength = 50
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel2
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(359, 20)
        Me.txtDesc.TabIndex = 191
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(17, 42)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel2.TabIndex = 190
        Me.RadLabel2.Text = "Description"
        '
        'txtCapexCode
        '
        Me.txtCapexCode.CalculationExpression = Nothing
        Me.txtCapexCode.FieldCode = Nothing
        Me.txtCapexCode.FieldDesc = Nothing
        Me.txtCapexCode.FieldMaxLength = 0
        Me.txtCapexCode.FieldName = Nothing
        Me.txtCapexCode.isCalculatedField = False
        Me.txtCapexCode.IsSourceFromTable = False
        Me.txtCapexCode.IsSourceFromValueList = False
        Me.txtCapexCode.IsUnique = False
        Me.txtCapexCode.Location = New System.Drawing.Point(149, 64)
        Me.txtCapexCode.MendatroryField = True
        Me.txtCapexCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCapexCode.MyLinkLable1 = Me.MyLabel1
        Me.txtCapexCode.MyLinkLable2 = Nothing
        Me.txtCapexCode.MyReadOnly = False
        Me.txtCapexCode.MyShowMasterFormButton = False
        Me.txtCapexCode.Name = "txtCapexCode"
        Me.txtCapexCode.ReferenceFieldDesc = Nothing
        Me.txtCapexCode.ReferenceFieldName = Nothing
        Me.txtCapexCode.ReferenceTableName = Nothing
        Me.txtCapexCode.Size = New System.Drawing.Size(129, 19)
        Me.txtCapexCode.TabIndex = 18
        Me.txtCapexCode.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(17, 66)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(66, 18)
        Me.MyLabel1.TabIndex = 8
        Me.MyLabel1.Text = "Capex Code"
        '
        'txtdate
        '
        Me.txtdate.CalculationExpression = Nothing
        Me.txtdate.CustomFormat = "dd/MM/yyyy"
        Me.txtdate.FieldCode = Nothing
        Me.txtdate.FieldDesc = Nothing
        Me.txtdate.FieldMaxLength = 0
        Me.txtdate.FieldName = Nothing
        Me.txtdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtdate.isCalculatedField = False
        Me.txtdate.IsSourceFromTable = False
        Me.txtdate.IsSourceFromValueList = False
        Me.txtdate.IsUnique = False
        Me.txtdate.Location = New System.Drawing.Point(424, 16)
        Me.txtdate.MendatroryField = True
        Me.txtdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdate.MyLinkLable1 = Me.MyLabel12
        Me.txtdate.MyLinkLable2 = Nothing
        Me.txtdate.Name = "txtdate"
        Me.txtdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdate.ReferenceFieldDesc = Nothing
        Me.txtdate.ReferenceFieldName = Nothing
        Me.txtdate.ReferenceTableName = Nothing
        Me.txtdate.Size = New System.Drawing.Size(84, 20)
        Me.txtdate.TabIndex = 188
        Me.txtdate.TabStop = False
        Me.txtdate.Text = "13/06/2011"
        Me.txtdate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Location = New System.Drawing.Point(388, 17)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel12.TabIndex = 189
        Me.MyLabel12.Text = "Date"
        '
        'txtCapexName
        '
        Me.txtCapexName.AutoSize = False
        Me.txtCapexName.BorderVisible = True
        Me.txtCapexName.FieldName = Nothing
        Me.txtCapexName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCapexName.Location = New System.Drawing.Point(278, 64)
        Me.txtCapexName.Name = "txtCapexName"
        Me.txtCapexName.Size = New System.Drawing.Size(228, 20)
        Me.txtCapexName.TabIndex = 22
        Me.txtCapexName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_tolerence
        '
        Me.txt_tolerence.BackColor = System.Drawing.Color.White
        Me.txt_tolerence.CalculationExpression = Nothing
        Me.txt_tolerence.DecimalPlaces = 2
        Me.txt_tolerence.FieldCode = Nothing
        Me.txt_tolerence.FieldDesc = Nothing
        Me.txt_tolerence.FieldMaxLength = 100
        Me.txt_tolerence.FieldName = Nothing
        Me.txt_tolerence.isCalculatedField = False
        Me.txt_tolerence.IsSourceFromTable = False
        Me.txt_tolerence.IsSourceFromValueList = False
        Me.txt_tolerence.IsUnique = False
        Me.txt_tolerence.Location = New System.Drawing.Point(149, 134)
        Me.txt_tolerence.MendatroryField = False
        Me.txt_tolerence.MyLinkLable1 = Me.MyLabel3
        Me.txt_tolerence.MyLinkLable2 = Nothing
        Me.txt_tolerence.Name = "txt_tolerence"
        Me.txt_tolerence.ReferenceFieldDesc = Nothing
        Me.txt_tolerence.ReferenceFieldName = Nothing
        Me.txt_tolerence.ReferenceTableName = Nothing
        Me.txt_tolerence.Size = New System.Drawing.Size(129, 20)
        Me.txt_tolerence.TabIndex = 186
        Me.txt_tolerence.Text = "0"
        Me.txt_tolerence.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txt_tolerence.Value = 0.0R
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(17, 135)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(74, 18)
        Me.MyLabel3.TabIndex = 187
        Me.MyLabel3.Text = "Tolerence (%)"
        '
        'txt_budget
        '
        Me.txt_budget.BackColor = System.Drawing.Color.White
        Me.txt_budget.CalculationExpression = Nothing
        Me.txt_budget.DecimalPlaces = 2
        Me.txt_budget.FieldCode = Nothing
        Me.txt_budget.FieldDesc = Nothing
        Me.txt_budget.FieldMaxLength = 100
        Me.txt_budget.FieldName = Nothing
        Me.txt_budget.isCalculatedField = False
        Me.txt_budget.IsSourceFromTable = False
        Me.txt_budget.IsSourceFromValueList = False
        Me.txt_budget.IsUnique = False
        Me.txt_budget.Location = New System.Drawing.Point(149, 87)
        Me.txt_budget.MendatroryField = True
        Me.txt_budget.MyLinkLable1 = Nothing
        Me.txt_budget.MyLinkLable2 = Nothing
        Me.txt_budget.Name = "txt_budget"
        Me.txt_budget.ReferenceFieldDesc = Nothing
        Me.txt_budget.ReferenceFieldName = Nothing
        Me.txt_budget.ReferenceTableName = Nothing
        Me.txt_budget.Size = New System.Drawing.Size(129, 20)
        Me.txt_budget.TabIndex = 184
        Me.txt_budget.Text = "0"
        Me.txt_budget.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txt_budget.Value = 0.0R
        '
        'txt_revisedbudget
        '
        Me.txt_revisedbudget.BackColor = System.Drawing.Color.White
        Me.txt_revisedbudget.CalculationExpression = Nothing
        Me.txt_revisedbudget.DecimalPlaces = 2
        Me.txt_revisedbudget.FieldCode = Nothing
        Me.txt_revisedbudget.FieldDesc = Nothing
        Me.txt_revisedbudget.FieldMaxLength = 100
        Me.txt_revisedbudget.FieldName = Nothing
        Me.txt_revisedbudget.isCalculatedField = False
        Me.txt_revisedbudget.IsSourceFromTable = False
        Me.txt_revisedbudget.IsSourceFromValueList = False
        Me.txt_revisedbudget.IsUnique = False
        Me.txt_revisedbudget.Location = New System.Drawing.Point(378, 112)
        Me.txt_revisedbudget.MendatroryField = False
        Me.txt_revisedbudget.MyLinkLable1 = Me.MyLabel2
        Me.txt_revisedbudget.MyLinkLable2 = Nothing
        Me.txt_revisedbudget.Name = "txt_revisedbudget"
        Me.txt_revisedbudget.ReadOnly = True
        Me.txt_revisedbudget.ReferenceFieldDesc = Nothing
        Me.txt_revisedbudget.ReferenceFieldName = Nothing
        Me.txt_revisedbudget.ReferenceTableName = Nothing
        Me.txt_revisedbudget.Size = New System.Drawing.Size(128, 20)
        Me.txt_revisedbudget.TabIndex = 186
        Me.txt_revisedbudget.Text = "0"
        Me.txt_revisedbudget.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txt_revisedbudget.Value = 0.0R
        '
        'BtnHistory
        '
        Me.BtnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnHistory.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.BtnHistory.Location = New System.Drawing.Point(150, 4)
        Me.BtnHistory.Name = "BtnHistory"
        Me.BtnHistory.Size = New System.Drawing.Size(68, 21)
        Me.BtnHistory.TabIndex = 9
        Me.BtnHistory.Text = "&History"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnSave.Location = New System.Drawing.Point(6, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 21)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnDelete.Location = New System.Drawing.Point(77, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 21)
        Me.btnDelete.TabIndex = 7
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(728, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 21)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
        '
        'chkProvisional
        '
        Me.chkProvisional.AccessibleDescription = ""
        Me.chkProvisional.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkProvisional.Location = New System.Drawing.Point(516, 18)
        Me.chkProvisional.Name = "chkProvisional"
        Me.chkProvisional.Size = New System.Drawing.Size(76, 16)
        Me.chkProvisional.TabIndex = 211
        Me.chkProvisional.Text = "Provisional"
        '
        'FrmCapexBudget
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(802, 268)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmCapexBudget"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmCapexBudget"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.txtCapexAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCapexAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCapexBalance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCapexBalance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcurrentBudget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumIncBudget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIncBudget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_revisionno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCapexName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_tolerence, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_budget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_revisedbudget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkProvisional, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtCapexCode As common.UserControls.txtFinder
    Friend WithEvents txtCapexName As common.Controls.MyLabel
    Friend WithEvents txt_tolerence As common.MyNumBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txt_revisedbudget As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txt_budget As common.MyNumBox
    Friend WithEvents txtdate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txt_revisionno As common.Controls.MyLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents BtnHistory As Telerik.WinControls.UI.RadButton
    Friend WithEvents NumIncBudget As common.MyNumBox
    Friend WithEvents lblIncBudget As common.Controls.MyLabel
    Friend WithEvents lblcurrentBudget As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents txtCapexAmount As common.MyNumBox
    Friend WithEvents lblCapexAmount As common.Controls.MyLabel
    Friend WithEvents txtCapexBalance As common.MyNumBox
    Friend WithEvents lblCapexBalance As common.Controls.MyLabel
    Friend WithEvents chkProvisional As Telerik.WinControls.UI.RadCheckBox
End Class

