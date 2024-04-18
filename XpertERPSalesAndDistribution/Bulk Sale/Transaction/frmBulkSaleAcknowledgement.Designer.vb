Imports common
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBulkSaleAcknowledgement
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.fndBulkSaleNo = New common.UserControls.txtFinder()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.txtDiffAmount = New common.MyNumBox()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.txtAmount = New common.MyNumBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtSNFRate = New common.MyNumBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtSNFKg = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtFATRate = New common.MyNumBox()
        Me.txtSNF = New common.MyNumBox()
        Me.txtFAT = New common.MyNumBox()
        Me.txtQty = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtFATKg = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.txtSaleAmount = New common.Controls.MyLabel()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.txtSaleSNFRate = New common.Controls.MyLabel()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtSaleFATRate = New common.Controls.MyLabel()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.txtSaleSNF = New common.Controls.MyLabel()
        Me.txtSaleFAT = New common.Controls.MyLabel()
        Me.txtQtySale = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtSaleSNFKg = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.txtSaleFATKg = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.fndDocNo = New common.UserControls.txtNavigator()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.txtCustomer = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiffAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSNFRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSNFKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFATRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFATKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSaleAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSaleSNFRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSaleFATRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSaleSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSaleFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQtySale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSaleSNFKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSaleFATKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndBulkSaleNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel19)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDiffAmount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel18)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAmount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel11)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSNFRate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSNFKg)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFATRate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSNF)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFAT)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtQty)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFATKg)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndDocNo)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(889, 486)
        Me.SplitContainer1.SplitterDistance = 444
        Me.SplitContainer1.TabIndex = 0
        '
        'fndBulkSaleNo
        '
        Me.fndBulkSaleNo.CalculationExpression = Nothing
        Me.fndBulkSaleNo.FieldCode = Nothing
        Me.fndBulkSaleNo.FieldDesc = Nothing
        Me.fndBulkSaleNo.FieldMaxLength = 0
        Me.fndBulkSaleNo.FieldName = Nothing
        Me.fndBulkSaleNo.isCalculatedField = False
        Me.fndBulkSaleNo.IsSourceFromTable = False
        Me.fndBulkSaleNo.IsSourceFromValueList = False
        Me.fndBulkSaleNo.IsUnique = False
        Me.fndBulkSaleNo.Location = New System.Drawing.Point(92, 47)
        Me.fndBulkSaleNo.MendatroryField = True
        Me.fndBulkSaleNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBulkSaleNo.MyLinkLable1 = Nothing
        Me.fndBulkSaleNo.MyLinkLable2 = Nothing
        Me.fndBulkSaleNo.MyReadOnly = False
        Me.fndBulkSaleNo.MyShowMasterFormButton = False
        Me.fndBulkSaleNo.Name = "fndBulkSaleNo"
        Me.fndBulkSaleNo.ReferenceFieldDesc = Nothing
        Me.fndBulkSaleNo.ReferenceFieldName = Nothing
        Me.fndBulkSaleNo.ReferenceTableName = Nothing
        Me.fndBulkSaleNo.Size = New System.Drawing.Size(289, 19)
        Me.fndBulkSaleNo.TabIndex = 1450
        Me.fndBulkSaleNo.Value = ""
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(12, 202)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel19.TabIndex = 1437
        Me.MyLabel19.Text = "Diff. Amount"
        '
        'txtDiffAmount
        '
        Me.txtDiffAmount.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtDiffAmount.CalculationExpression = Nothing
        Me.txtDiffAmount.DecimalPlaces = 2
        Me.txtDiffAmount.FieldCode = Nothing
        Me.txtDiffAmount.FieldDesc = Nothing
        Me.txtDiffAmount.FieldMaxLength = 0
        Me.txtDiffAmount.FieldName = Nothing
        Me.txtDiffAmount.isCalculatedField = False
        Me.txtDiffAmount.IsSourceFromTable = False
        Me.txtDiffAmount.IsSourceFromValueList = False
        Me.txtDiffAmount.IsUnique = False
        Me.txtDiffAmount.Location = New System.Drawing.Point(92, 200)
        Me.txtDiffAmount.MendatroryField = False
        Me.txtDiffAmount.MyLinkLable1 = Nothing
        Me.txtDiffAmount.MyLinkLable2 = Nothing
        Me.txtDiffAmount.Name = "txtDiffAmount"
        Me.txtDiffAmount.ReadOnly = True
        Me.txtDiffAmount.ReferenceFieldDesc = Nothing
        Me.txtDiffAmount.ReferenceFieldName = Nothing
        Me.txtDiffAmount.ReferenceTableName = Nothing
        Me.txtDiffAmount.Size = New System.Drawing.Size(110, 20)
        Me.txtDiffAmount.TabIndex = 1438
        Me.txtDiffAmount.Text = "0"
        Me.txtDiffAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDiffAmount.Value = 0R
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(12, 180)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel18.TabIndex = 1435
        Me.MyLabel18.Text = "Amount"
        '
        'txtAmount
        '
        Me.txtAmount.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtAmount.CalculationExpression = Nothing
        Me.txtAmount.DecimalPlaces = 2
        Me.txtAmount.FieldCode = Nothing
        Me.txtAmount.FieldDesc = Nothing
        Me.txtAmount.FieldMaxLength = 0
        Me.txtAmount.FieldName = Nothing
        Me.txtAmount.isCalculatedField = False
        Me.txtAmount.IsSourceFromTable = False
        Me.txtAmount.IsSourceFromValueList = False
        Me.txtAmount.IsUnique = False
        Me.txtAmount.Location = New System.Drawing.Point(92, 178)
        Me.txtAmount.MendatroryField = False
        Me.txtAmount.MyLinkLable1 = Nothing
        Me.txtAmount.MyLinkLable2 = Nothing
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.ReadOnly = True
        Me.txtAmount.ReferenceFieldDesc = Nothing
        Me.txtAmount.ReferenceFieldName = Nothing
        Me.txtAmount.ReferenceTableName = Nothing
        Me.txtAmount.Size = New System.Drawing.Size(110, 20)
        Me.txtAmount.TabIndex = 1436
        Me.txtAmount.Text = "0"
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAmount.Value = 0R
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(11, 159)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel11.TabIndex = 1433
        Me.MyLabel11.Text = "SNF Rate"
        '
        'txtSNFRate
        '
        Me.txtSNFRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSNFRate.CalculationExpression = Nothing
        Me.txtSNFRate.DecimalPlaces = 2
        Me.txtSNFRate.FieldCode = Nothing
        Me.txtSNFRate.FieldDesc = Nothing
        Me.txtSNFRate.FieldMaxLength = 0
        Me.txtSNFRate.FieldName = Nothing
        Me.txtSNFRate.isCalculatedField = False
        Me.txtSNFRate.IsSourceFromTable = False
        Me.txtSNFRate.IsSourceFromValueList = False
        Me.txtSNFRate.IsUnique = False
        Me.txtSNFRate.Location = New System.Drawing.Point(92, 156)
        Me.txtSNFRate.MendatroryField = False
        Me.txtSNFRate.MyLinkLable1 = Nothing
        Me.txtSNFRate.MyLinkLable2 = Nothing
        Me.txtSNFRate.Name = "txtSNFRate"
        Me.txtSNFRate.ReadOnly = True
        Me.txtSNFRate.ReferenceFieldDesc = Nothing
        Me.txtSNFRate.ReferenceFieldName = Nothing
        Me.txtSNFRate.ReferenceTableName = Nothing
        Me.txtSNFRate.Size = New System.Drawing.Size(110, 20)
        Me.txtSNFRate.TabIndex = 1434
        Me.txtSNFRate.Text = "0"
        Me.txtSNFRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSNFRate.Value = 0R
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(92, 222)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(289, 49)
        Me.txtRemarks.TabIndex = 1432
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(12, 221)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel6.TabIndex = 1431
        Me.MyLabel6.Text = "Remarks"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(361, 24)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1430
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(203, 114)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel8.TabIndex = 1429
        Me.MyLabel8.Text = "SNF Kg"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(203, 92)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel7.TabIndex = 1428
        Me.MyLabel7.Text = "FAT Kg"
        '
        'txtSNFKg
        '
        Me.txtSNFKg.AutoSize = False
        Me.txtSNFKg.BorderVisible = True
        Me.txtSNFKg.FieldName = Nothing
        Me.txtSNFKg.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSNFKg.Location = New System.Drawing.Point(251, 114)
        Me.txtSNFKg.Name = "txtSNFKg"
        Me.txtSNFKg.Size = New System.Drawing.Size(110, 18)
        Me.txtSNFKg.TabIndex = 1427
        Me.txtSNFKg.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(12, 138)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel5.TabIndex = 20
        Me.MyLabel5.Text = "FAT Rate"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(12, 116)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(29, 16)
        Me.MyLabel4.TabIndex = 20
        Me.MyLabel4.Text = "SNF"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(12, 94)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(28, 16)
        Me.MyLabel3.TabIndex = 1426
        Me.MyLabel3.Text = "FAT"
        '
        'txtFATRate
        '
        Me.txtFATRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtFATRate.CalculationExpression = Nothing
        Me.txtFATRate.DecimalPlaces = 2
        Me.txtFATRate.FieldCode = Nothing
        Me.txtFATRate.FieldDesc = Nothing
        Me.txtFATRate.FieldMaxLength = 0
        Me.txtFATRate.FieldName = Nothing
        Me.txtFATRate.isCalculatedField = False
        Me.txtFATRate.IsSourceFromTable = False
        Me.txtFATRate.IsSourceFromValueList = False
        Me.txtFATRate.IsUnique = False
        Me.txtFATRate.Location = New System.Drawing.Point(92, 134)
        Me.txtFATRate.MendatroryField = False
        Me.txtFATRate.MyLinkLable1 = Nothing
        Me.txtFATRate.MyLinkLable2 = Nothing
        Me.txtFATRate.Name = "txtFATRate"
        Me.txtFATRate.ReadOnly = True
        Me.txtFATRate.ReferenceFieldDesc = Nothing
        Me.txtFATRate.ReferenceFieldName = Nothing
        Me.txtFATRate.ReferenceTableName = Nothing
        Me.txtFATRate.Size = New System.Drawing.Size(110, 20)
        Me.txtFATRate.TabIndex = 1425
        Me.txtFATRate.Text = "0"
        Me.txtFATRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFATRate.Value = 0R
        '
        'txtSNF
        '
        Me.txtSNF.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSNF.CalculationExpression = Nothing
        Me.txtSNF.DecimalPlaces = 2
        Me.txtSNF.FieldCode = Nothing
        Me.txtSNF.FieldDesc = Nothing
        Me.txtSNF.FieldMaxLength = 0
        Me.txtSNF.FieldName = Nothing
        Me.txtSNF.isCalculatedField = False
        Me.txtSNF.IsSourceFromTable = False
        Me.txtSNF.IsSourceFromValueList = False
        Me.txtSNF.IsUnique = False
        Me.txtSNF.Location = New System.Drawing.Point(92, 112)
        Me.txtSNF.MendatroryField = False
        Me.txtSNF.MyLinkLable1 = Nothing
        Me.txtSNF.MyLinkLable2 = Nothing
        Me.txtSNF.Name = "txtSNF"
        Me.txtSNF.ReferenceFieldDesc = Nothing
        Me.txtSNF.ReferenceFieldName = Nothing
        Me.txtSNF.ReferenceTableName = Nothing
        Me.txtSNF.Size = New System.Drawing.Size(110, 20)
        Me.txtSNF.TabIndex = 1424
        Me.txtSNF.Text = "0"
        Me.txtSNF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSNF.Value = 0R
        '
        'txtFAT
        '
        Me.txtFAT.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtFAT.CalculationExpression = Nothing
        Me.txtFAT.DecimalPlaces = 2
        Me.txtFAT.FieldCode = Nothing
        Me.txtFAT.FieldDesc = Nothing
        Me.txtFAT.FieldMaxLength = 0
        Me.txtFAT.FieldName = Nothing
        Me.txtFAT.isCalculatedField = False
        Me.txtFAT.IsSourceFromTable = False
        Me.txtFAT.IsSourceFromValueList = False
        Me.txtFAT.IsUnique = False
        Me.txtFAT.Location = New System.Drawing.Point(92, 90)
        Me.txtFAT.MendatroryField = False
        Me.txtFAT.MyLinkLable1 = Nothing
        Me.txtFAT.MyLinkLable2 = Nothing
        Me.txtFAT.Name = "txtFAT"
        Me.txtFAT.ReferenceFieldDesc = Nothing
        Me.txtFAT.ReferenceFieldName = Nothing
        Me.txtFAT.ReferenceTableName = Nothing
        Me.txtFAT.Size = New System.Drawing.Size(110, 20)
        Me.txtFAT.TabIndex = 1423
        Me.txtFAT.Text = "0"
        Me.txtFAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFAT.Value = 0R
        '
        'txtQty
        '
        Me.txtQty.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtQty.CalculationExpression = Nothing
        Me.txtQty.DecimalPlaces = 0
        Me.txtQty.FieldCode = Nothing
        Me.txtQty.FieldDesc = Nothing
        Me.txtQty.FieldMaxLength = 0
        Me.txtQty.FieldName = Nothing
        Me.txtQty.isCalculatedField = False
        Me.txtQty.IsSourceFromTable = False
        Me.txtQty.IsSourceFromValueList = False
        Me.txtQty.IsUnique = False
        Me.txtQty.Location = New System.Drawing.Point(92, 68)
        Me.txtQty.MendatroryField = False
        Me.txtQty.MyLinkLable1 = Nothing
        Me.txtQty.MyLinkLable2 = Nothing
        Me.txtQty.Name = "txtQty"
        Me.txtQty.ReferenceFieldDesc = Nothing
        Me.txtQty.ReferenceFieldName = Nothing
        Me.txtQty.ReferenceTableName = Nothing
        Me.txtQty.Size = New System.Drawing.Size(110, 20)
        Me.txtQty.TabIndex = 1422
        Me.txtQty.Text = "0"
        Me.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtQty.Value = 0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(12, 70)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(24, 16)
        Me.MyLabel2.TabIndex = 19
        Me.MyLabel2.Text = "Qty"
        '
        'txtFATKg
        '
        Me.txtFATKg.AutoSize = False
        Me.txtFATKg.BorderVisible = True
        Me.txtFATKg.FieldName = Nothing
        Me.txtFATKg.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFATKg.Location = New System.Drawing.Point(251, 92)
        Me.txtFATKg.Name = "txtFATKg"
        Me.txtFATKg.Size = New System.Drawing.Size(110, 18)
        Me.txtFATKg.TabIndex = 18
        Me.txtFATKg.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtCustomer)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel20)
        Me.RadGroupBox1.Controls.Add(Me.txtSaleAmount)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel17)
        Me.RadGroupBox1.Controls.Add(Me.txtSaleSNFRate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel16)
        Me.RadGroupBox1.Controls.Add(Me.txtSaleFATRate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel15)
        Me.RadGroupBox1.Controls.Add(Me.txtSaleSNF)
        Me.RadGroupBox1.Controls.Add(Me.txtSaleFAT)
        Me.RadGroupBox1.Controls.Add(Me.txtQtySale)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel10)
        Me.RadGroupBox1.Controls.Add(Me.txtSaleSNFKg)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel12)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel13)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel14)
        Me.RadGroupBox1.Controls.Add(Me.txtSaleFATKg)
        Me.RadGroupBox1.HeaderText = "Bulk Sale Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(409, 47)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(380, 190)
        Me.RadGroupBox1.TabIndex = 14
        Me.RadGroupBox1.Text = "Bulk Sale Details"
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel20.Location = New System.Drawing.Point(15, 21)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel20.TabIndex = 1450
        Me.MyLabel20.Text = "Customer"
        '
        'txtSaleAmount
        '
        Me.txtSaleAmount.AutoSize = False
        Me.txtSaleAmount.BorderVisible = True
        Me.txtSaleAmount.FieldName = Nothing
        Me.txtSaleAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleAmount.Location = New System.Drawing.Point(79, 153)
        Me.txtSaleAmount.Name = "txtSaleAmount"
        Me.txtSaleAmount.Size = New System.Drawing.Size(110, 18)
        Me.txtSaleAmount.TabIndex = 1448
        Me.txtSaleAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(15, 154)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel17.TabIndex = 1447
        Me.MyLabel17.Text = "Amount"
        '
        'txtSaleSNFRate
        '
        Me.txtSaleSNFRate.AutoSize = False
        Me.txtSaleSNFRate.BorderVisible = True
        Me.txtSaleSNFRate.FieldName = Nothing
        Me.txtSaleSNFRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleSNFRate.Location = New System.Drawing.Point(79, 131)
        Me.txtSaleSNFRate.Name = "txtSaleSNFRate"
        Me.txtSaleSNFRate.Size = New System.Drawing.Size(110, 18)
        Me.txtSaleSNFRate.TabIndex = 1446
        Me.txtSaleSNFRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(15, 132)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel16.TabIndex = 1445
        Me.MyLabel16.Text = "SNF Rate"
        '
        'txtSaleFATRate
        '
        Me.txtSaleFATRate.AutoSize = False
        Me.txtSaleFATRate.BorderVisible = True
        Me.txtSaleFATRate.FieldName = Nothing
        Me.txtSaleFATRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleFATRate.Location = New System.Drawing.Point(79, 109)
        Me.txtSaleFATRate.Name = "txtSaleFATRate"
        Me.txtSaleFATRate.Size = New System.Drawing.Size(110, 18)
        Me.txtSaleFATRate.TabIndex = 1444
        Me.txtSaleFATRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(15, 110)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel15.TabIndex = 1443
        Me.MyLabel15.Text = "FAT Rate"
        '
        'txtSaleSNF
        '
        Me.txtSaleSNF.AutoSize = False
        Me.txtSaleSNF.BorderVisible = True
        Me.txtSaleSNF.FieldName = Nothing
        Me.txtSaleSNF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleSNF.Location = New System.Drawing.Point(79, 87)
        Me.txtSaleSNF.Name = "txtSaleSNF"
        Me.txtSaleSNF.Size = New System.Drawing.Size(110, 18)
        Me.txtSaleSNF.TabIndex = 1442
        Me.txtSaleSNF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSaleFAT
        '
        Me.txtSaleFAT.AutoSize = False
        Me.txtSaleFAT.BorderVisible = True
        Me.txtSaleFAT.FieldName = Nothing
        Me.txtSaleFAT.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleFAT.Location = New System.Drawing.Point(79, 65)
        Me.txtSaleFAT.Name = "txtSaleFAT"
        Me.txtSaleFAT.Size = New System.Drawing.Size(110, 18)
        Me.txtSaleFAT.TabIndex = 1441
        Me.txtSaleFAT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtQtySale
        '
        Me.txtQtySale.AutoSize = False
        Me.txtQtySale.BorderVisible = True
        Me.txtQtySale.FieldName = Nothing
        Me.txtQtySale.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQtySale.Location = New System.Drawing.Point(79, 44)
        Me.txtQtySale.Name = "txtQtySale"
        Me.txtQtySale.Size = New System.Drawing.Size(110, 18)
        Me.txtQtySale.TabIndex = 1440
        Me.txtQtySale.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(201, 89)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel9.TabIndex = 1439
        Me.MyLabel9.Text = "SNF Kg"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(201, 67)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel10.TabIndex = 1438
        Me.MyLabel10.Text = "FAT Kg"
        '
        'txtSaleSNFKg
        '
        Me.txtSaleSNFKg.AutoSize = False
        Me.txtSaleSNFKg.BorderVisible = True
        Me.txtSaleSNFKg.FieldName = Nothing
        Me.txtSaleSNFKg.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleSNFKg.Location = New System.Drawing.Point(249, 87)
        Me.txtSaleSNFKg.Name = "txtSaleSNFKg"
        Me.txtSaleSNFKg.Size = New System.Drawing.Size(110, 18)
        Me.txtSaleSNFKg.TabIndex = 1437
        Me.txtSaleSNFKg.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(15, 89)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(29, 16)
        Me.MyLabel12.TabIndex = 1432
        Me.MyLabel12.Text = "SNF"
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(16, 67)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(28, 16)
        Me.MyLabel13.TabIndex = 1436
        Me.MyLabel13.Text = "FAT"
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(15, 43)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(24, 16)
        Me.MyLabel14.TabIndex = 1431
        Me.MyLabel14.Text = "Qty"
        '
        'txtSaleFATKg
        '
        Me.txtSaleFATKg.AutoSize = False
        Me.txtSaleFATKg.BorderVisible = True
        Me.txtSaleFATKg.FieldName = Nothing
        Me.txtSaleFATKg.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleFATKg.Location = New System.Drawing.Point(249, 65)
        Me.txtSaleFATKg.Name = "txtSaleFATKg"
        Me.txtSaleFATKg.Size = New System.Drawing.Size(110, 18)
        Me.txtSaleFATKg.TabIndex = 1430
        Me.txtSaleFATKg.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 48)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel1.TabIndex = 13
        Me.MyLabel1.Text = "Bulk Sale No"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(584, 24)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(92, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 11
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
        Me.txtDate.Location = New System.Drawing.Point(441, 25)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(2023, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(127, 18)
        Me.txtDate.TabIndex = 9
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "01/01/2023"
        Me.txtDate.Value = New Date(2023, 1, 1, 0, 0, 0, 0)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(409, 26)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 10
        Me.RadLabel4.Text = "Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.RadLabel1.Location = New System.Drawing.Point(12, 26)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(80, 16)
        Me.RadLabel1.TabIndex = 8
        Me.RadLabel1.Text = "Document No"
        '
        'fndDocNo
        '
        Me.fndDocNo.FieldName = Nothing
        Me.fndDocNo.Location = New System.Drawing.Point(92, 25)
        Me.fndDocNo.MendatroryField = False
        Me.fndDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocNo.MyLinkLable1 = Me.RadLabel1
        Me.fndDocNo.MyLinkLable2 = Nothing
        Me.fndDocNo.MyMaxLength = 32767
        Me.fndDocNo.MyReadOnly = False
        Me.fndDocNo.Name = "fndDocNo"
        Me.fndDocNo.Size = New System.Drawing.Size(269, 19)
        Me.fndDocNo.TabIndex = 7
        Me.fndDocNo.Value = ""
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(810, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 8
        Me.btnclose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(157, 9)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 6
        Me.btnPost.Text = "Post"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(82, 9)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 7
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(7, 9)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 5
        Me.btnsave.Text = "Save"
        '
        'txtCustomer
        '
        Me.txtCustomer.AutoSize = False
        Me.txtCustomer.BorderVisible = True
        Me.txtCustomer.FieldName = Nothing
        Me.txtCustomer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.Location = New System.Drawing.Point(79, 21)
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(110, 18)
        Me.txtCustomer.TabIndex = 1451
        Me.txtCustomer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmBulkSaleAcknowledgement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(889, 486)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmBulkSaleAcknowledgement"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmBulkSaleAcknowledgement"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiffAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSNFRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSNFKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFATRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFATKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSaleAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSaleSNFRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSaleFATRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSaleSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSaleFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQtySale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSaleSNFKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSaleFATKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnclose As RadButton
    Friend WithEvents btnPost As RadButton
    Friend WithEvents btndelete As RadButton
    Friend WithEvents btnsave As RadButton
    Friend WithEvents UsLock1 As usLock
    Friend WithEvents txtDate As Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As Controls.MyLabel
    Friend WithEvents RadLabel1 As Controls.MyLabel
    Friend WithEvents fndDocNo As UserControls.txtNavigator
    Friend WithEvents MyLabel1 As Controls.MyLabel
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents MyLabel2 As Controls.MyLabel
    Friend WithEvents txtFATKg As Controls.MyLabel
    Friend WithEvents txtQty As MyNumBox
    Friend WithEvents MyLabel5 As Controls.MyLabel
    Friend WithEvents MyLabel4 As Controls.MyLabel
    Friend WithEvents MyLabel3 As Controls.MyLabel
    Friend WithEvents txtFATRate As MyNumBox
    Friend WithEvents txtSNF As MyNumBox
    Friend WithEvents txtFAT As MyNumBox
    Friend WithEvents MyLabel8 As Controls.MyLabel
    Friend WithEvents MyLabel7 As Controls.MyLabel
    Friend WithEvents txtSNFKg As Controls.MyLabel
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents txtSaleSNF As Controls.MyLabel
    Friend WithEvents txtSaleFAT As Controls.MyLabel
    Friend WithEvents txtQtySale As Controls.MyLabel
    Friend WithEvents MyLabel9 As Controls.MyLabel
    Friend WithEvents MyLabel10 As Controls.MyLabel
    Friend WithEvents txtSaleSNFKg As Controls.MyLabel
    Friend WithEvents MyLabel12 As Controls.MyLabel
    Friend WithEvents MyLabel13 As Controls.MyLabel
    Friend WithEvents MyLabel14 As Controls.MyLabel
    Friend WithEvents txtSaleFATKg As Controls.MyLabel
    Friend WithEvents MyLabel6 As Controls.MyLabel
    Friend WithEvents txtRemarks As TextBox
    Friend WithEvents txtSaleAmount As Controls.MyLabel
    Friend WithEvents MyLabel17 As Controls.MyLabel
    Friend WithEvents txtSaleSNFRate As Controls.MyLabel
    Friend WithEvents MyLabel16 As Controls.MyLabel
    Friend WithEvents txtSaleFATRate As Controls.MyLabel
    Friend WithEvents MyLabel15 As Controls.MyLabel
    Friend WithEvents MyLabel11 As Controls.MyLabel
    Friend WithEvents txtSNFRate As MyNumBox
    Friend WithEvents MyLabel18 As Controls.MyLabel
    Friend WithEvents txtAmount As MyNumBox
    Friend WithEvents MyLabel19 As Controls.MyLabel
    Friend WithEvents txtDiffAmount As MyNumBox
    Friend WithEvents MyLabel20 As Controls.MyLabel
    Friend WithEvents fndBulkSaleNo As UserControls.txtFinder
    Friend WithEvents txtCustomer As Controls.MyLabel
End Class
