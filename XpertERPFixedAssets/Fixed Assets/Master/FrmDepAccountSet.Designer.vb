Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDepAccountSet
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export = New Telerik.WinControls.UI.RadMenuItem()
        Me.Import = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblWIP = New common.Controls.MyLabel()
        Me.lblWIPDesc = New common.Controls.MyLabel()
        Me.fndWIP = New common.UserControls.txtFinder()
        Me.lblLoss = New common.Controls.MyLabel()
        Me.lblLossDesc = New common.Controls.MyLabel()
        Me.lblProfit = New common.Controls.MyLabel()
        Me.fndLoss = New common.UserControls.txtFinder()
        Me.lblProfitDesc = New common.Controls.MyLabel()
        Me.fndProfit = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.lblDepAc = New common.Controls.MyLabel()
        Me.txtDepAccount = New common.UserControls.txtFinder()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.lblDisposalProceedAccount = New common.Controls.MyLabel()
        Me.txtDisposalProceedAccount = New common.UserControls.txtFinder()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.lblDisposalAccount = New common.Controls.MyLabel()
        Me.txtDisposalAccount = New common.UserControls.txtFinder()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.lblAccumDep = New common.Controls.MyLabel()
        Me.fndAccumDep = New common.UserControls.txtFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblDisposalCostAccount = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtDisposalCostAccount = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblTransferClearingAccount = New common.Controls.MyLabel()
        Me.fndTransferClearingAccount = New common.UserControls.txtFinder()
        Me.lblAssetControl = New common.Controls.MyLabel()
        Me.fndAssetControl = New common.UserControls.txtFinder()
        Me.chkinactive = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblWIP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWIPDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLoss, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLossDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProfit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProfitDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepAc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDisposalProceedAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDisposalAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAccumDep, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDisposalCostAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransferClearingAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAssetControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkinactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(777, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Export, Me.Import})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'Export
        '
        Me.Export.AccessibleDescription = "Export"
        Me.Export.AccessibleName = "Export"
        Me.Export.Name = "Export"
        Me.Export.Text = "Export"
        '
        'Import
        '
        Me.Import.AccessibleDescription = "Import"
        Me.Import.AccessibleName = "Import"
        Me.Import.Name = "Import"
        Me.Import.Text = "Import"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblWIP)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblWIPDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndWIP)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLoss)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLossDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblProfit)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndLoss)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblProfitDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndProfit)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDepAc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDepAccount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel13)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDisposalProceedAccount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDisposalProceedAccount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel11)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDisposalAccount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDisposalAccount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAccumDep)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndAccumDep)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDisposalCostAccount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDisposalCostAccount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTransferClearingAccount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndTransferClearingAccount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAssetControl)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndAssetControl)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkinactive)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDocNo)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(777, 425)
        Me.SplitContainer1.SplitterDistance = 390
        Me.SplitContainer1.TabIndex = 1
        '
        'lblWIP
        '
        Me.lblWIP.FieldName = Nothing
        Me.lblWIP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWIP.Location = New System.Drawing.Point(12, 194)
        Me.lblWIP.Name = "lblWIP"
        Me.lblWIP.Size = New System.Drawing.Size(138, 16)
        Me.lblWIP.TabIndex = 54
        Me.lblWIP.Text = "Work In Progress Account"
        '
        'lblWIPDesc
        '
        Me.lblWIPDesc.AutoSize = False
        Me.lblWIPDesc.BorderVisible = True
        Me.lblWIPDesc.FieldName = Nothing
        Me.lblWIPDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWIPDesc.Location = New System.Drawing.Point(381, 193)
        Me.lblWIPDesc.Name = "lblWIPDesc"
        Me.lblWIPDesc.Size = New System.Drawing.Size(384, 18)
        Me.lblWIPDesc.TabIndex = 55
        Me.lblWIPDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblWIPDesc.TextWrap = False
        '
        'fndWIP
        '
        Me.fndWIP.CalculationExpression = Nothing
        Me.fndWIP.FieldCode = Nothing
        Me.fndWIP.FieldDesc = Nothing
        Me.fndWIP.FieldMaxLength = 0
        Me.fndWIP.FieldName = Nothing
        Me.fndWIP.isCalculatedField = False
        Me.fndWIP.IsSourceFromTable = False
        Me.fndWIP.IsSourceFromValueList = False
        Me.fndWIP.IsUnique = False
        Me.fndWIP.Location = New System.Drawing.Point(160, 193)
        Me.fndWIP.MendatroryField = True
        Me.fndWIP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndWIP.MyLinkLable1 = Me.lblWIP
        Me.fndWIP.MyLinkLable2 = Me.lblWIPDesc
        Me.fndWIP.MyReadOnly = False
        Me.fndWIP.MyShowMasterFormButton = False
        Me.fndWIP.Name = "fndWIP"
        Me.fndWIP.ReferenceFieldDesc = Nothing
        Me.fndWIP.ReferenceFieldName = Nothing
        Me.fndWIP.ReferenceTableName = Nothing
        Me.fndWIP.Size = New System.Drawing.Size(214, 18)
        Me.fndWIP.TabIndex = 47
        Me.fndWIP.Value = ""
        '
        'lblLoss
        '
        Me.lblLoss.FieldName = Nothing
        Me.lblLoss.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoss.Location = New System.Drawing.Point(12, 238)
        Me.lblLoss.Name = "lblLoss"
        Me.lblLoss.Size = New System.Drawing.Size(74, 16)
        Me.lblLoss.TabIndex = 52
        Me.lblLoss.Text = "Loss Account"
        '
        'lblLossDesc
        '
        Me.lblLossDesc.AutoSize = False
        Me.lblLossDesc.BorderVisible = True
        Me.lblLossDesc.FieldName = Nothing
        Me.lblLossDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLossDesc.Location = New System.Drawing.Point(381, 237)
        Me.lblLossDesc.Name = "lblLossDesc"
        Me.lblLossDesc.Size = New System.Drawing.Size(384, 18)
        Me.lblLossDesc.TabIndex = 53
        Me.lblLossDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLossDesc.TextWrap = False
        '
        'lblProfit
        '
        Me.lblProfit.FieldName = Nothing
        Me.lblProfit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProfit.Location = New System.Drawing.Point(12, 216)
        Me.lblProfit.Name = "lblProfit"
        Me.lblProfit.Size = New System.Drawing.Size(77, 16)
        Me.lblProfit.TabIndex = 50
        Me.lblProfit.Text = "Profit Account"
        '
        'fndLoss
        '
        Me.fndLoss.CalculationExpression = Nothing
        Me.fndLoss.FieldCode = Nothing
        Me.fndLoss.FieldDesc = Nothing
        Me.fndLoss.FieldMaxLength = 0
        Me.fndLoss.FieldName = Nothing
        Me.fndLoss.isCalculatedField = False
        Me.fndLoss.IsSourceFromTable = False
        Me.fndLoss.IsSourceFromValueList = False
        Me.fndLoss.IsUnique = False
        Me.fndLoss.Location = New System.Drawing.Point(160, 237)
        Me.fndLoss.MendatroryField = True
        Me.fndLoss.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLoss.MyLinkLable1 = Me.lblLoss
        Me.fndLoss.MyLinkLable2 = Me.lblLossDesc
        Me.fndLoss.MyReadOnly = False
        Me.fndLoss.MyShowMasterFormButton = False
        Me.fndLoss.Name = "fndLoss"
        Me.fndLoss.ReferenceFieldDesc = Nothing
        Me.fndLoss.ReferenceFieldName = Nothing
        Me.fndLoss.ReferenceTableName = Nothing
        Me.fndLoss.Size = New System.Drawing.Size(214, 18)
        Me.fndLoss.TabIndex = 49
        Me.fndLoss.Value = ""
        '
        'lblProfitDesc
        '
        Me.lblProfitDesc.AutoSize = False
        Me.lblProfitDesc.BorderVisible = True
        Me.lblProfitDesc.FieldName = Nothing
        Me.lblProfitDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProfitDesc.Location = New System.Drawing.Point(381, 215)
        Me.lblProfitDesc.Name = "lblProfitDesc"
        Me.lblProfitDesc.Size = New System.Drawing.Size(384, 18)
        Me.lblProfitDesc.TabIndex = 51
        Me.lblProfitDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblProfitDesc.TextWrap = False
        '
        'fndProfit
        '
        Me.fndProfit.CalculationExpression = Nothing
        Me.fndProfit.FieldCode = Nothing
        Me.fndProfit.FieldDesc = Nothing
        Me.fndProfit.FieldMaxLength = 0
        Me.fndProfit.FieldName = Nothing
        Me.fndProfit.isCalculatedField = False
        Me.fndProfit.IsSourceFromTable = False
        Me.fndProfit.IsSourceFromValueList = False
        Me.fndProfit.IsUnique = False
        Me.fndProfit.Location = New System.Drawing.Point(160, 215)
        Me.fndProfit.MendatroryField = True
        Me.fndProfit.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndProfit.MyLinkLable1 = Me.lblProfitDesc
        Me.fndProfit.MyLinkLable2 = Me.lblProfit
        Me.fndProfit.MyReadOnly = False
        Me.fndProfit.MyShowMasterFormButton = False
        Me.fndProfit.Name = "fndProfit"
        Me.fndProfit.ReferenceFieldDesc = Nothing
        Me.fndProfit.ReferenceFieldName = Nothing
        Me.fndProfit.ReferenceTableName = Nothing
        Me.fndProfit.Size = New System.Drawing.Size(214, 18)
        Me.fndProfit.TabIndex = 48
        Me.fndProfit.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(12, 107)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(92, 16)
        Me.MyLabel4.TabIndex = 45
        Me.MyLabel4.Text = "Depreciation A/C"
        '
        'lblDepAc
        '
        Me.lblDepAc.AutoSize = False
        Me.lblDepAc.BorderVisible = True
        Me.lblDepAc.FieldName = Nothing
        Me.lblDepAc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepAc.Location = New System.Drawing.Point(381, 106)
        Me.lblDepAc.Name = "lblDepAc"
        Me.lblDepAc.Size = New System.Drawing.Size(384, 18)
        Me.lblDepAc.TabIndex = 46
        Me.lblDepAc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDepAc.TextWrap = False
        '
        'txtDepAccount
        '
        Me.txtDepAccount.CalculationExpression = Nothing
        Me.txtDepAccount.FieldCode = Nothing
        Me.txtDepAccount.FieldDesc = Nothing
        Me.txtDepAccount.FieldMaxLength = 0
        Me.txtDepAccount.FieldName = Nothing
        Me.txtDepAccount.isCalculatedField = False
        Me.txtDepAccount.IsSourceFromTable = False
        Me.txtDepAccount.IsSourceFromValueList = False
        Me.txtDepAccount.IsUnique = False
        Me.txtDepAccount.Location = New System.Drawing.Point(160, 106)
        Me.txtDepAccount.MendatroryField = True
        Me.txtDepAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepAccount.MyLinkLable1 = Me.MyLabel4
        Me.txtDepAccount.MyLinkLable2 = Me.lblDepAc
        Me.txtDepAccount.MyReadOnly = False
        Me.txtDepAccount.MyShowMasterFormButton = False
        Me.txtDepAccount.Name = "txtDepAccount"
        Me.txtDepAccount.ReferenceFieldDesc = Nothing
        Me.txtDepAccount.ReferenceFieldName = Nothing
        Me.txtDepAccount.ReferenceTableName = Nothing
        Me.txtDepAccount.Size = New System.Drawing.Size(214, 18)
        Me.txtDepAccount.TabIndex = 4
        Me.txtDepAccount.Value = ""
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(12, 260)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(139, 16)
        Me.MyLabel13.TabIndex = 42
        Me.MyLabel13.Text = "Disposal Proceed Account"
        Me.MyLabel13.Visible = False
        '
        'lblDisposalProceedAccount
        '
        Me.lblDisposalProceedAccount.AutoSize = False
        Me.lblDisposalProceedAccount.BorderVisible = True
        Me.lblDisposalProceedAccount.FieldName = Nothing
        Me.lblDisposalProceedAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDisposalProceedAccount.Location = New System.Drawing.Point(381, 259)
        Me.lblDisposalProceedAccount.Name = "lblDisposalProceedAccount"
        Me.lblDisposalProceedAccount.Size = New System.Drawing.Size(384, 18)
        Me.lblDisposalProceedAccount.TabIndex = 43
        Me.lblDisposalProceedAccount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDisposalProceedAccount.TextWrap = False
        Me.lblDisposalProceedAccount.Visible = False
        '
        'txtDisposalProceedAccount
        '
        Me.txtDisposalProceedAccount.CalculationExpression = Nothing
        Me.txtDisposalProceedAccount.FieldCode = Nothing
        Me.txtDisposalProceedAccount.FieldDesc = Nothing
        Me.txtDisposalProceedAccount.FieldMaxLength = 0
        Me.txtDisposalProceedAccount.FieldName = Nothing
        Me.txtDisposalProceedAccount.isCalculatedField = False
        Me.txtDisposalProceedAccount.IsSourceFromTable = False
        Me.txtDisposalProceedAccount.IsSourceFromValueList = False
        Me.txtDisposalProceedAccount.IsUnique = False
        Me.txtDisposalProceedAccount.Location = New System.Drawing.Point(160, 259)
        Me.txtDisposalProceedAccount.MendatroryField = True
        Me.txtDisposalProceedAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDisposalProceedAccount.MyLinkLable1 = Me.MyLabel13
        Me.txtDisposalProceedAccount.MyLinkLable2 = Me.lblDisposalProceedAccount
        Me.txtDisposalProceedAccount.MyReadOnly = False
        Me.txtDisposalProceedAccount.MyShowMasterFormButton = False
        Me.txtDisposalProceedAccount.Name = "txtDisposalProceedAccount"
        Me.txtDisposalProceedAccount.ReferenceFieldDesc = Nothing
        Me.txtDisposalProceedAccount.ReferenceFieldName = Nothing
        Me.txtDisposalProceedAccount.ReferenceTableName = Nothing
        Me.txtDisposalProceedAccount.Size = New System.Drawing.Size(214, 18)
        Me.txtDisposalProceedAccount.TabIndex = 6
        Me.txtDisposalProceedAccount.Value = ""
        Me.txtDisposalProceedAccount.Visible = False
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(12, 129)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel11.TabIndex = 39
        Me.MyLabel11.Text = "Disposal Account"
        '
        'lblDisposalAccount
        '
        Me.lblDisposalAccount.AutoSize = False
        Me.lblDisposalAccount.BorderVisible = True
        Me.lblDisposalAccount.FieldName = Nothing
        Me.lblDisposalAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDisposalAccount.Location = New System.Drawing.Point(381, 128)
        Me.lblDisposalAccount.Name = "lblDisposalAccount"
        Me.lblDisposalAccount.Size = New System.Drawing.Size(384, 18)
        Me.lblDisposalAccount.TabIndex = 40
        Me.lblDisposalAccount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDisposalAccount.TextWrap = False
        '
        'txtDisposalAccount
        '
        Me.txtDisposalAccount.CalculationExpression = Nothing
        Me.txtDisposalAccount.FieldCode = Nothing
        Me.txtDisposalAccount.FieldDesc = Nothing
        Me.txtDisposalAccount.FieldMaxLength = 0
        Me.txtDisposalAccount.FieldName = Nothing
        Me.txtDisposalAccount.isCalculatedField = False
        Me.txtDisposalAccount.IsSourceFromTable = False
        Me.txtDisposalAccount.IsSourceFromValueList = False
        Me.txtDisposalAccount.IsUnique = False
        Me.txtDisposalAccount.Location = New System.Drawing.Point(160, 128)
        Me.txtDisposalAccount.MendatroryField = True
        Me.txtDisposalAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDisposalAccount.MyLinkLable1 = Me.MyLabel11
        Me.txtDisposalAccount.MyLinkLable2 = Me.lblDisposalAccount
        Me.txtDisposalAccount.MyReadOnly = False
        Me.txtDisposalAccount.MyShowMasterFormButton = False
        Me.txtDisposalAccount.Name = "txtDisposalAccount"
        Me.txtDisposalAccount.ReferenceFieldDesc = Nothing
        Me.txtDisposalAccount.ReferenceFieldName = Nothing
        Me.txtDisposalAccount.ReferenceTableName = Nothing
        Me.txtDisposalAccount.Size = New System.Drawing.Size(214, 18)
        Me.txtDisposalAccount.TabIndex = 5
        Me.txtDisposalAccount.Value = ""
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(12, 85)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(138, 16)
        Me.MyLabel9.TabIndex = 33
        Me.MyLabel9.Text = "Accumulated Depreciation"
        '
        'lblAccumDep
        '
        Me.lblAccumDep.AutoSize = False
        Me.lblAccumDep.BorderVisible = True
        Me.lblAccumDep.FieldName = Nothing
        Me.lblAccumDep.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccumDep.Location = New System.Drawing.Point(381, 84)
        Me.lblAccumDep.Name = "lblAccumDep"
        Me.lblAccumDep.Size = New System.Drawing.Size(384, 18)
        Me.lblAccumDep.TabIndex = 34
        Me.lblAccumDep.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblAccumDep.TextWrap = False
        '
        'fndAccumDep
        '
        Me.fndAccumDep.CalculationExpression = Nothing
        Me.fndAccumDep.FieldCode = Nothing
        Me.fndAccumDep.FieldDesc = Nothing
        Me.fndAccumDep.FieldMaxLength = 0
        Me.fndAccumDep.FieldName = Nothing
        Me.fndAccumDep.isCalculatedField = False
        Me.fndAccumDep.IsSourceFromTable = False
        Me.fndAccumDep.IsSourceFromValueList = False
        Me.fndAccumDep.IsUnique = False
        Me.fndAccumDep.Location = New System.Drawing.Point(160, 84)
        Me.fndAccumDep.MendatroryField = True
        Me.fndAccumDep.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndAccumDep.MyLinkLable1 = Me.MyLabel9
        Me.fndAccumDep.MyLinkLable2 = Me.lblAccumDep
        Me.fndAccumDep.MyReadOnly = False
        Me.fndAccumDep.MyShowMasterFormButton = False
        Me.fndAccumDep.Name = "fndAccumDep"
        Me.fndAccumDep.ReferenceFieldDesc = Nothing
        Me.fndAccumDep.ReferenceFieldName = Nothing
        Me.fndAccumDep.ReferenceTableName = Nothing
        Me.fndAccumDep.Size = New System.Drawing.Size(214, 18)
        Me.fndAccumDep.TabIndex = 3
        Me.fndAccumDep.Value = ""
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(12, 173)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(120, 16)
        Me.MyLabel5.TabIndex = 33
        Me.MyLabel5.Text = "Disposal Cost Account"
        '
        'lblDisposalCostAccount
        '
        Me.lblDisposalCostAccount.AutoSize = False
        Me.lblDisposalCostAccount.BorderVisible = True
        Me.lblDisposalCostAccount.FieldName = Nothing
        Me.lblDisposalCostAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDisposalCostAccount.Location = New System.Drawing.Point(381, 172)
        Me.lblDisposalCostAccount.Name = "lblDisposalCostAccount"
        Me.lblDisposalCostAccount.Size = New System.Drawing.Size(384, 18)
        Me.lblDisposalCostAccount.TabIndex = 34
        Me.lblDisposalCostAccount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDisposalCostAccount.TextWrap = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(12, 151)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(138, 16)
        Me.MyLabel3.TabIndex = 30
        Me.MyLabel3.Text = "Transfer Clearing Account"
        '
        'txtDisposalCostAccount
        '
        Me.txtDisposalCostAccount.CalculationExpression = Nothing
        Me.txtDisposalCostAccount.FieldCode = Nothing
        Me.txtDisposalCostAccount.FieldDesc = Nothing
        Me.txtDisposalCostAccount.FieldMaxLength = 0
        Me.txtDisposalCostAccount.FieldName = Nothing
        Me.txtDisposalCostAccount.isCalculatedField = False
        Me.txtDisposalCostAccount.IsSourceFromTable = False
        Me.txtDisposalCostAccount.IsSourceFromValueList = False
        Me.txtDisposalCostAccount.IsUnique = False
        Me.txtDisposalCostAccount.Location = New System.Drawing.Point(160, 172)
        Me.txtDisposalCostAccount.MendatroryField = True
        Me.txtDisposalCostAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDisposalCostAccount.MyLinkLable1 = Me.MyLabel5
        Me.txtDisposalCostAccount.MyLinkLable2 = Me.lblDisposalCostAccount
        Me.txtDisposalCostAccount.MyReadOnly = False
        Me.txtDisposalCostAccount.MyShowMasterFormButton = False
        Me.txtDisposalCostAccount.Name = "txtDisposalCostAccount"
        Me.txtDisposalCostAccount.ReferenceFieldDesc = Nothing
        Me.txtDisposalCostAccount.ReferenceFieldName = Nothing
        Me.txtDisposalCostAccount.ReferenceTableName = Nothing
        Me.txtDisposalCostAccount.Size = New System.Drawing.Size(214, 18)
        Me.txtDisposalCostAccount.TabIndex = 8
        Me.txtDisposalCostAccount.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(12, 63)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel2.TabIndex = 28
        Me.MyLabel2.Text = "Asset Control"
        '
        'lblTransferClearingAccount
        '
        Me.lblTransferClearingAccount.AutoSize = False
        Me.lblTransferClearingAccount.BorderVisible = True
        Me.lblTransferClearingAccount.FieldName = Nothing
        Me.lblTransferClearingAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransferClearingAccount.Location = New System.Drawing.Point(381, 150)
        Me.lblTransferClearingAccount.Name = "lblTransferClearingAccount"
        Me.lblTransferClearingAccount.Size = New System.Drawing.Size(384, 18)
        Me.lblTransferClearingAccount.TabIndex = 31
        Me.lblTransferClearingAccount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTransferClearingAccount.TextWrap = False
        '
        'fndTransferClearingAccount
        '
        Me.fndTransferClearingAccount.CalculationExpression = Nothing
        Me.fndTransferClearingAccount.FieldCode = Nothing
        Me.fndTransferClearingAccount.FieldDesc = Nothing
        Me.fndTransferClearingAccount.FieldMaxLength = 0
        Me.fndTransferClearingAccount.FieldName = Nothing
        Me.fndTransferClearingAccount.isCalculatedField = False
        Me.fndTransferClearingAccount.IsSourceFromTable = False
        Me.fndTransferClearingAccount.IsSourceFromValueList = False
        Me.fndTransferClearingAccount.IsUnique = False
        Me.fndTransferClearingAccount.Location = New System.Drawing.Point(160, 150)
        Me.fndTransferClearingAccount.MendatroryField = True
        Me.fndTransferClearingAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTransferClearingAccount.MyLinkLable1 = Me.lblTransferClearingAccount
        Me.fndTransferClearingAccount.MyLinkLable2 = Me.MyLabel3
        Me.fndTransferClearingAccount.MyReadOnly = False
        Me.fndTransferClearingAccount.MyShowMasterFormButton = False
        Me.fndTransferClearingAccount.Name = "fndTransferClearingAccount"
        Me.fndTransferClearingAccount.ReferenceFieldDesc = Nothing
        Me.fndTransferClearingAccount.ReferenceFieldName = Nothing
        Me.fndTransferClearingAccount.ReferenceTableName = Nothing
        Me.fndTransferClearingAccount.Size = New System.Drawing.Size(214, 18)
        Me.fndTransferClearingAccount.TabIndex = 7
        Me.fndTransferClearingAccount.Value = ""
        '
        'lblAssetControl
        '
        Me.lblAssetControl.AutoSize = False
        Me.lblAssetControl.BorderVisible = True
        Me.lblAssetControl.FieldName = Nothing
        Me.lblAssetControl.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssetControl.Location = New System.Drawing.Point(381, 62)
        Me.lblAssetControl.Name = "lblAssetControl"
        Me.lblAssetControl.Size = New System.Drawing.Size(384, 18)
        Me.lblAssetControl.TabIndex = 28
        Me.lblAssetControl.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblAssetControl.TextWrap = False
        '
        'fndAssetControl
        '
        Me.fndAssetControl.CalculationExpression = Nothing
        Me.fndAssetControl.FieldCode = Nothing
        Me.fndAssetControl.FieldDesc = Nothing
        Me.fndAssetControl.FieldMaxLength = 0
        Me.fndAssetControl.FieldName = Nothing
        Me.fndAssetControl.isCalculatedField = False
        Me.fndAssetControl.IsSourceFromTable = False
        Me.fndAssetControl.IsSourceFromValueList = False
        Me.fndAssetControl.IsUnique = False
        Me.fndAssetControl.Location = New System.Drawing.Point(160, 62)
        Me.fndAssetControl.MendatroryField = True
        Me.fndAssetControl.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndAssetControl.MyLinkLable1 = Me.lblAssetControl
        Me.fndAssetControl.MyLinkLable2 = Me.MyLabel2
        Me.fndAssetControl.MyReadOnly = False
        Me.fndAssetControl.MyShowMasterFormButton = False
        Me.fndAssetControl.Name = "fndAssetControl"
        Me.fndAssetControl.ReferenceFieldDesc = Nothing
        Me.fndAssetControl.ReferenceFieldName = Nothing
        Me.fndAssetControl.ReferenceTableName = Nothing
        Me.fndAssetControl.Size = New System.Drawing.Size(214, 18)
        Me.fndAssetControl.TabIndex = 2
        Me.fndAssetControl.Value = ""
        '
        'chkinactive
        '
        Me.chkinactive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkinactive.Location = New System.Drawing.Point(415, 16)
        Me.chkinactive.Name = "chkinactive"
        Me.chkinactive.Size = New System.Drawing.Size(74, 16)
        Me.chkinactive.TabIndex = 1
        Me.chkinactive.Text = "Not In Use"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 40)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel1.TabIndex = 24
        Me.MyLabel1.Text = "Description"
        '
        'txtDesc
        '
        Me.txtDesc.CalculationExpression = Nothing
        Me.txtDesc.FieldCode = Nothing
        Me.txtDesc.FieldDesc = Nothing
        Me.txtDesc.FieldMaxLength = 0
        Me.txtDesc.FieldName = Nothing
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.isCalculatedField = False
        Me.txtDesc.IsSourceFromTable = False
        Me.txtDesc.IsSourceFromValueList = False
        Me.txtDesc.IsUnique = False
        Me.txtDesc.Location = New System.Drawing.Point(115, 39)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Nothing
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(650, 18)
        Me.txtDesc.TabIndex = 3
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(12, 16)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(97, 16)
        Me.RadLabel1.TabIndex = 23
        Me.RadLabel1.Text = "Account Set Code"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPFixedAssets.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(378, 14)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(115, 14)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Nothing
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(259, 20)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.Value = ""
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(696, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(78, 23)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(87, 5)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(78, 23)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(3, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(78, 23)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(171, 5)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(78, 23)
        Me.btnHistory.TabIndex = 3
        Me.btnHistory.Text = "&History"
        '
        'FrmDepAccountSet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(777, 445)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmDepAccountSet"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Account Sets"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblWIP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWIPDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLoss, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLossDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProfit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProfitDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepAc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDisposalProceedAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDisposalAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAccumDep, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDisposalCostAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransferClearingAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAssetControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkinactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents chkinactive As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents fndAssetControl As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblAssetControl As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents lblDisposalProceedAccount As common.Controls.MyLabel
    Friend WithEvents txtDisposalProceedAccount As common.UserControls.txtFinder
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents lblDisposalAccount As common.Controls.MyLabel
    Friend WithEvents txtDisposalAccount As common.UserControls.txtFinder
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents lblAccumDep As common.Controls.MyLabel
    Friend WithEvents fndAccumDep As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblDisposalCostAccount As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtDisposalCostAccount As common.UserControls.txtFinder
    Friend WithEvents lblTransferClearingAccount As common.Controls.MyLabel
    Friend WithEvents fndTransferClearingAccount As common.UserControls.txtFinder
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblDepAc As common.Controls.MyLabel
    Friend WithEvents txtDepAccount As common.UserControls.txtFinder
    Friend WithEvents lblWIP As common.Controls.MyLabel
    Friend WithEvents lblWIPDesc As common.Controls.MyLabel
    Friend WithEvents fndWIP As common.UserControls.txtFinder
    Friend WithEvents lblLoss As common.Controls.MyLabel
    Friend WithEvents lblLossDesc As common.Controls.MyLabel
    Friend WithEvents lblProfit As common.Controls.MyLabel
    Friend WithEvents fndLoss As common.UserControls.txtFinder
    Friend WithEvents lblProfitDesc As common.Controls.MyLabel
    Friend WithEvents fndProfit As common.UserControls.txtFinder
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
End Class

