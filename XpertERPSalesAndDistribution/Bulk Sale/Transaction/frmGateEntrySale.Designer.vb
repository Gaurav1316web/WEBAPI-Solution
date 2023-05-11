<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGateEntrySale
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.fndtankersale = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.chkSaleReturn = New common.Controls.MyCheckBox()
        Me.FndSaleReturnTanker = New common.UserControls.txtFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.FndDispatchNo = New common.UserControls.txtFinder()
        Me.TxtTankerName = New common.Controls.MyTextBox()
        Me.lblTanker = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.FndCustomer = New common.UserControls.txtFinder()
        Me.LblCustomer = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.fndTransporter = New common.UserControls.txtFinder()
        Me.lblItemName = New common.Controls.MyLabel()
        Me.lblItemCode = New common.Controls.MyLabel()
        Me.lblTransporter = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fndOrderNo = New common.UserControls.txtFinder()
        Me.fndTanker = New common.UserControls.txtFinder()
        Me.lblLocationBulk = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblDateAndTimeBulk = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.fndGateEntryNo = New common.UserControls.txtNavigator()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSaleReturn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTankerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTanker, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDateAndTimeBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndtankersale)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkSaleReturn)
        Me.SplitContainer1.Panel1.Controls.Add(Me.FndSaleReturnTanker)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.FndDispatchNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtTankerName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTanker)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.FndCustomer)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblCustomer)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndTransporter)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblItemName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblItemCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTransporter)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndOrderNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndTanker)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationBulk)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDateAndTimeBulk)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndGateEntryNo)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(672, 293)
        Me.SplitContainer1.SplitterDistance = 261
        Me.SplitContainer1.TabIndex = 0
        '
        'fndtankersale
        '
        Me.fndtankersale.CalculationExpression = Nothing
        Me.fndtankersale.FieldCode = Nothing
        Me.fndtankersale.FieldDesc = Nothing
        Me.fndtankersale.FieldMaxLength = 0
        Me.fndtankersale.FieldName = Nothing
        Me.fndtankersale.isCalculatedField = False
        Me.fndtankersale.IsSourceFromTable = False
        Me.fndtankersale.IsSourceFromValueList = False
        Me.fndtankersale.IsUnique = False
        Me.fndtankersale.Location = New System.Drawing.Point(129, 59)
        Me.fndtankersale.MendatroryField = True
        Me.fndtankersale.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndtankersale.MyLinkLable1 = Me.MyLabel4
        Me.fndtankersale.MyLinkLable2 = Nothing
        Me.fndtankersale.MyReadOnly = False
        Me.fndtankersale.MyShowMasterFormButton = False
        Me.fndtankersale.Name = "fndtankersale"
        Me.fndtankersale.ReferenceFieldDesc = Nothing
        Me.fndtankersale.ReferenceFieldName = Nothing
        Me.fndtankersale.ReferenceTableName = Nothing
        Me.fndtankersale.Size = New System.Drawing.Size(148, 19)
        Me.fndtankersale.TabIndex = 300
        Me.fndtankersale.Value = ""
        Me.fndtankersale.Visible = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(16, 157)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel4.TabIndex = 11
        Me.MyLabel4.Text = "Transporter No"
        Me.MyLabel4.Visible = False
        '
        'chkSaleReturn
        '
        Me.chkSaleReturn.Location = New System.Drawing.Point(284, 36)
        Me.chkSaleReturn.MyLinkLable1 = Nothing
        Me.chkSaleReturn.MyLinkLable2 = Nothing
        Me.chkSaleReturn.Name = "chkSaleReturn"
        Me.chkSaleReturn.Size = New System.Drawing.Size(77, 18)
        Me.chkSaleReturn.TabIndex = 299
        Me.chkSaleReturn.Tag1 = Nothing
        Me.chkSaleReturn.Text = "Sale Return"
        '
        'FndSaleReturnTanker
        '
        Me.FndSaleReturnTanker.CalculationExpression = Nothing
        Me.FndSaleReturnTanker.FieldCode = Nothing
        Me.FndSaleReturnTanker.FieldDesc = Nothing
        Me.FndSaleReturnTanker.FieldMaxLength = 0
        Me.FndSaleReturnTanker.FieldName = Nothing
        Me.FndSaleReturnTanker.isCalculatedField = False
        Me.FndSaleReturnTanker.IsSourceFromTable = False
        Me.FndSaleReturnTanker.IsSourceFromValueList = False
        Me.FndSaleReturnTanker.IsUnique = False
        Me.FndSaleReturnTanker.Location = New System.Drawing.Point(129, 59)
        Me.FndSaleReturnTanker.MendatroryField = True
        Me.FndSaleReturnTanker.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndSaleReturnTanker.MyLinkLable1 = Me.MyLabel4
        Me.FndSaleReturnTanker.MyLinkLable2 = Nothing
        Me.FndSaleReturnTanker.MyReadOnly = False
        Me.FndSaleReturnTanker.MyShowMasterFormButton = False
        Me.FndSaleReturnTanker.Name = "FndSaleReturnTanker"
        Me.FndSaleReturnTanker.ReferenceFieldDesc = Nothing
        Me.FndSaleReturnTanker.ReferenceFieldName = Nothing
        Me.FndSaleReturnTanker.ReferenceTableName = Nothing
        Me.FndSaleReturnTanker.Size = New System.Drawing.Size(148, 19)
        Me.FndSaleReturnTanker.TabIndex = 297
        Me.FndSaleReturnTanker.Value = ""
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(16, 133)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel5.TabIndex = 297
        Me.MyLabel5.Text = "Dispatch No"
        '
        'FndDispatchNo
        '
        Me.FndDispatchNo.CalculationExpression = Nothing
        Me.FndDispatchNo.FieldCode = Nothing
        Me.FndDispatchNo.FieldDesc = Nothing
        Me.FndDispatchNo.FieldMaxLength = 0
        Me.FndDispatchNo.FieldName = Nothing
        Me.FndDispatchNo.isCalculatedField = False
        Me.FndDispatchNo.IsSourceFromTable = False
        Me.FndDispatchNo.IsSourceFromValueList = False
        Me.FndDispatchNo.IsUnique = False
        Me.FndDispatchNo.Location = New System.Drawing.Point(129, 132)
        Me.FndDispatchNo.MendatroryField = True
        Me.FndDispatchNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndDispatchNo.MyLinkLable1 = Me.MyLabel5
        Me.FndDispatchNo.MyLinkLable2 = Nothing
        Me.FndDispatchNo.MyReadOnly = False
        Me.FndDispatchNo.MyShowMasterFormButton = False
        Me.FndDispatchNo.Name = "FndDispatchNo"
        Me.FndDispatchNo.ReferenceFieldDesc = Nothing
        Me.FndDispatchNo.ReferenceFieldName = Nothing
        Me.FndDispatchNo.ReferenceTableName = Nothing
        Me.FndDispatchNo.Size = New System.Drawing.Size(147, 19)
        Me.FndDispatchNo.TabIndex = 298
        Me.FndDispatchNo.Value = ""
        '
        'TxtTankerName
        '
        Me.TxtTankerName.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.TxtTankerName.CalculationExpression = Nothing
        Me.TxtTankerName.FieldCode = Nothing
        Me.TxtTankerName.FieldDesc = Nothing
        Me.TxtTankerName.FieldMaxLength = 0
        Me.TxtTankerName.FieldName = Nothing
        Me.TxtTankerName.isCalculatedField = False
        Me.TxtTankerName.IsSourceFromTable = False
        Me.TxtTankerName.IsSourceFromValueList = False
        Me.TxtTankerName.IsUnique = False
        Me.TxtTankerName.Location = New System.Drawing.Point(129, 59)
        Me.TxtTankerName.MaxLength = 20
        Me.TxtTankerName.MendatroryField = False
        Me.TxtTankerName.MyLinkLable1 = Nothing
        Me.TxtTankerName.MyLinkLable2 = Nothing
        Me.TxtTankerName.Name = "TxtTankerName"
        Me.TxtTankerName.ReferenceFieldDesc = Nothing
        Me.TxtTankerName.ReferenceFieldName = Nothing
        Me.TxtTankerName.ReferenceTableName = Nothing
        Me.TxtTankerName.Size = New System.Drawing.Size(239, 20)
        Me.TxtTankerName.TabIndex = 296
        '
        'lblTanker
        '
        Me.lblTanker.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.lblTanker.CalculationExpression = Nothing
        Me.lblTanker.FieldCode = Nothing
        Me.lblTanker.FieldDesc = Nothing
        Me.lblTanker.FieldMaxLength = 0
        Me.lblTanker.FieldName = Nothing
        Me.lblTanker.isCalculatedField = False
        Me.lblTanker.IsSourceFromTable = False
        Me.lblTanker.IsSourceFromValueList = False
        Me.lblTanker.IsUnique = False
        Me.lblTanker.Location = New System.Drawing.Point(595, 58)
        Me.lblTanker.MaxLength = 150
        Me.lblTanker.MendatroryField = False
        Me.lblTanker.MyLinkLable1 = Nothing
        Me.lblTanker.MyLinkLable2 = Nothing
        Me.lblTanker.Name = "lblTanker"
        Me.lblTanker.ReferenceFieldDesc = Nothing
        Me.lblTanker.ReferenceFieldName = Nothing
        Me.lblTanker.ReferenceTableName = Nothing
        Me.lblTanker.Size = New System.Drawing.Size(68, 20)
        Me.lblTanker.TabIndex = 15
        Me.lblTanker.Visible = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(16, 85)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel3.TabIndex = 7
        Me.MyLabel3.Text = "Customer"
        '
        'FndCustomer
        '
        Me.FndCustomer.CalculationExpression = Nothing
        Me.FndCustomer.FieldCode = Nothing
        Me.FndCustomer.FieldDesc = Nothing
        Me.FndCustomer.FieldMaxLength = 0
        Me.FndCustomer.FieldName = Nothing
        Me.FndCustomer.isCalculatedField = False
        Me.FndCustomer.IsSourceFromTable = False
        Me.FndCustomer.IsSourceFromValueList = False
        Me.FndCustomer.IsUnique = False
        Me.FndCustomer.Location = New System.Drawing.Point(129, 84)
        Me.FndCustomer.MendatroryField = True
        Me.FndCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndCustomer.MyLinkLable1 = Me.MyLabel3
        Me.FndCustomer.MyLinkLable2 = Nothing
        Me.FndCustomer.MyReadOnly = False
        Me.FndCustomer.MyShowMasterFormButton = False
        Me.FndCustomer.Name = "FndCustomer"
        Me.FndCustomer.ReferenceFieldDesc = Nothing
        Me.FndCustomer.ReferenceFieldName = Nothing
        Me.FndCustomer.ReferenceTableName = Nothing
        Me.FndCustomer.Size = New System.Drawing.Size(147, 19)
        Me.FndCustomer.TabIndex = 8
        Me.FndCustomer.Value = ""
        '
        'LblCustomer
        '
        Me.LblCustomer.AutoSize = False
        Me.LblCustomer.BorderVisible = True
        Me.LblCustomer.FieldName = Nothing
        Me.LblCustomer.Location = New System.Drawing.Point(282, 84)
        Me.LblCustomer.Name = "LblCustomer"
        Me.LblCustomer.Size = New System.Drawing.Size(381, 19)
        Me.LblCustomer.TabIndex = 295
        Me.LblCustomer.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(412, 8)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(92, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 4
        '
        'fndTransporter
        '
        Me.fndTransporter.CalculationExpression = Nothing
        Me.fndTransporter.FieldCode = Nothing
        Me.fndTransporter.FieldDesc = Nothing
        Me.fndTransporter.FieldMaxLength = 0
        Me.fndTransporter.FieldName = Nothing
        Me.fndTransporter.isCalculatedField = False
        Me.fndTransporter.IsSourceFromTable = False
        Me.fndTransporter.IsSourceFromValueList = False
        Me.fndTransporter.IsUnique = False
        Me.fndTransporter.Location = New System.Drawing.Point(129, 156)
        Me.fndTransporter.MendatroryField = True
        Me.fndTransporter.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTransporter.MyLinkLable1 = Me.MyLabel4
        Me.fndTransporter.MyLinkLable2 = Nothing
        Me.fndTransporter.MyReadOnly = False
        Me.fndTransporter.MyShowMasterFormButton = False
        Me.fndTransporter.Name = "fndTransporter"
        Me.fndTransporter.ReferenceFieldDesc = Nothing
        Me.fndTransporter.ReferenceFieldName = Nothing
        Me.fndTransporter.ReferenceTableName = Nothing
        Me.fndTransporter.Size = New System.Drawing.Size(147, 19)
        Me.fndTransporter.TabIndex = 12
        Me.fndTransporter.Value = ""
        Me.fndTransporter.Visible = False
        '
        'lblItemName
        '
        Me.lblItemName.AutoSize = False
        Me.lblItemName.BorderVisible = True
        Me.lblItemName.FieldName = Nothing
        Me.lblItemName.Location = New System.Drawing.Point(398, 181)
        Me.lblItemName.Name = "lblItemName"
        Me.lblItemName.Size = New System.Drawing.Size(265, 19)
        Me.lblItemName.TabIndex = 291
        Me.lblItemName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblItemName.Visible = False
        '
        'lblItemCode
        '
        Me.lblItemCode.AutoSize = False
        Me.lblItemCode.BorderVisible = True
        Me.lblItemCode.FieldName = Nothing
        Me.lblItemCode.Location = New System.Drawing.Point(282, 181)
        Me.lblItemCode.Name = "lblItemCode"
        Me.lblItemCode.Size = New System.Drawing.Size(114, 19)
        Me.lblItemCode.TabIndex = 291
        Me.lblItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblItemCode.Visible = False
        '
        'lblTransporter
        '
        Me.lblTransporter.AutoSize = False
        Me.lblTransporter.BorderVisible = True
        Me.lblTransporter.FieldName = Nothing
        Me.lblTransporter.Location = New System.Drawing.Point(282, 156)
        Me.lblTransporter.Name = "lblTransporter"
        Me.lblTransporter.Size = New System.Drawing.Size(381, 19)
        Me.lblTransporter.TabIndex = 291
        Me.lblTransporter.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTransporter.Visible = False
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(16, 180)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(53, 16)
        Me.MyLabel6.TabIndex = 13
        Me.MyLabel6.Text = "Order No"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(16, 59)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel2.TabIndex = 13
        Me.MyLabel2.Text = "Tanker"
        '
        'fndOrderNo
        '
        Me.fndOrderNo.CalculationExpression = Nothing
        Me.fndOrderNo.FieldCode = Nothing
        Me.fndOrderNo.FieldDesc = Nothing
        Me.fndOrderNo.FieldMaxLength = 0
        Me.fndOrderNo.FieldName = Nothing
        Me.fndOrderNo.isCalculatedField = False
        Me.fndOrderNo.IsSourceFromTable = False
        Me.fndOrderNo.IsSourceFromValueList = False
        Me.fndOrderNo.IsUnique = False
        Me.fndOrderNo.Location = New System.Drawing.Point(129, 180)
        Me.fndOrderNo.MendatroryField = False
        Me.fndOrderNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndOrderNo.MyLinkLable1 = Me.MyLabel6
        Me.fndOrderNo.MyLinkLable2 = Nothing
        Me.fndOrderNo.MyReadOnly = False
        Me.fndOrderNo.MyShowMasterFormButton = False
        Me.fndOrderNo.Name = "fndOrderNo"
        Me.fndOrderNo.ReferenceFieldDesc = Nothing
        Me.fndOrderNo.ReferenceFieldName = Nothing
        Me.fndOrderNo.ReferenceTableName = Nothing
        Me.fndOrderNo.Size = New System.Drawing.Size(147, 19)
        Me.fndOrderNo.TabIndex = 14
        Me.fndOrderNo.Value = ""
        Me.fndOrderNo.Visible = False
        '
        'fndTanker
        '
        Me.fndTanker.CalculationExpression = Nothing
        Me.fndTanker.FieldCode = Nothing
        Me.fndTanker.FieldDesc = Nothing
        Me.fndTanker.FieldMaxLength = 0
        Me.fndTanker.FieldName = Nothing
        Me.fndTanker.isCalculatedField = False
        Me.fndTanker.IsSourceFromTable = False
        Me.fndTanker.IsSourceFromValueList = False
        Me.fndTanker.IsUnique = False
        Me.fndTanker.Location = New System.Drawing.Point(491, 58)
        Me.fndTanker.MendatroryField = True
        Me.fndTanker.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTanker.MyLinkLable1 = Me.MyLabel2
        Me.fndTanker.MyLinkLable2 = Nothing
        Me.fndTanker.MyReadOnly = False
        Me.fndTanker.MyShowMasterFormButton = False
        Me.fndTanker.Name = "fndTanker"
        Me.fndTanker.ReferenceFieldDesc = Nothing
        Me.fndTanker.ReferenceFieldName = Nothing
        Me.fndTanker.ReferenceTableName = Nothing
        Me.fndTanker.Size = New System.Drawing.Size(94, 19)
        Me.fndTanker.TabIndex = 14
        Me.fndTanker.Value = ""
        Me.fndTanker.Visible = False
        '
        'lblLocationBulk
        '
        Me.lblLocationBulk.FieldName = Nothing
        Me.lblLocationBulk.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblLocationBulk.Location = New System.Drawing.Point(16, 109)
        Me.lblLocationBulk.Name = "lblLocationBulk"
        Me.lblLocationBulk.Size = New System.Drawing.Size(49, 16)
        Me.lblLocationBulk.TabIndex = 9
        Me.lblLocationBulk.Text = "Location"
        '
        'fndLocation
        '
        Me.fndLocation.CalculationExpression = Nothing
        Me.fndLocation.FieldCode = Nothing
        Me.fndLocation.FieldDesc = Nothing
        Me.fndLocation.FieldMaxLength = 0
        Me.fndLocation.FieldName = Nothing
        Me.fndLocation.isCalculatedField = False
        Me.fndLocation.IsSourceFromTable = False
        Me.fndLocation.IsSourceFromValueList = False
        Me.fndLocation.IsUnique = False
        Me.fndLocation.Location = New System.Drawing.Point(129, 108)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.lblLocationBulk
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(147, 19)
        Me.fndLocation.TabIndex = 10
        Me.fndLocation.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Location = New System.Drawing.Point(282, 108)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(381, 19)
        Me.lblLocation.TabIndex = 288
        Me.lblLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(15, 8)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(78, 16)
        Me.MyLabel1.TabIndex = 1
        Me.MyLabel1.Text = "Gate Entry No"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(129, 34)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDateAndTimeBulk
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(151, 20)
        Me.txtDate.TabIndex = 6
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "10/06/2011 11:51 AM"
        Me.txtDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblDateAndTimeBulk
        '
        Me.lblDateAndTimeBulk.FieldName = Nothing
        Me.lblDateAndTimeBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateAndTimeBulk.Location = New System.Drawing.Point(15, 36)
        Me.lblDateAndTimeBulk.Name = "lblDateAndTimeBulk"
        Me.lblDateAndTimeBulk.Size = New System.Drawing.Size(87, 16)
        Me.lblDateAndTimeBulk.TabIndex = 5
        Me.lblDateAndTimeBulk.Text = "Gate Entry Date"
        '
        'btnNew
        '
        Me.btnNew.Image = XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(378, 9)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(18, 20)
        Me.btnNew.TabIndex = 3
        '
        'fndGateEntryNo
        '
        Me.fndGateEntryNo.FieldName = Nothing
        Me.fndGateEntryNo.Location = New System.Drawing.Point(129, 8)
        Me.fndGateEntryNo.MendatroryField = False
        Me.fndGateEntryNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndGateEntryNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndGateEntryNo.MyLinkLable1 = Nothing
        Me.fndGateEntryNo.MyLinkLable2 = Nothing
        Me.fndGateEntryNo.MyMaxLength = 12
        Me.fndGateEntryNo.MyReadOnly = False
        Me.fndGateEntryNo.Name = "fndGateEntryNo"
        Me.fndGateEntryNo.Size = New System.Drawing.Size(247, 21)
        Me.fndGateEntryNo.TabIndex = 2
        Me.fndGateEntryNo.Value = ""
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(163, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(595, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(84, 5)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(3, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'FrmGateEntrySale
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(672, 293)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmGateEntrySale"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "MCC Gate Entry "
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSaleReturn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTankerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTanker, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDateAndTimeBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents fndTransporter As common.UserControls.txtFinder
    Friend WithEvents lblTransporter As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents fndTanker As common.UserControls.txtFinder
    Friend WithEvents lblLocationBulk As common.Controls.MyLabel
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblDateAndTimeBulk As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndGateEntryNo As common.UserControls.txtNavigator
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents FndCustomer As common.UserControls.txtFinder
    Friend WithEvents LblCustomer As common.Controls.MyLabel
    Friend WithEvents lblTanker As common.Controls.MyTextBox
    Friend WithEvents TxtTankerName As common.Controls.MyTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents FndDispatchNo As common.UserControls.txtFinder
    Friend WithEvents FndSaleReturnTanker As common.UserControls.txtFinder
    Friend WithEvents chkSaleReturn As common.Controls.MyCheckBox
    Friend WithEvents fndtankersale As common.UserControls.txtFinder
    Friend WithEvents fndOrderNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents lblItemName As common.Controls.MyLabel
    Friend WithEvents lblItemCode As common.Controls.MyLabel
End Class

