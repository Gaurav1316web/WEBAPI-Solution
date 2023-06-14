<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmOwnBMCGainLossRate
    'Inherits System.Windows.Forms.Form
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
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtLossSNFPer = New common.MyNumBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtLossFATPer = New common.MyNumBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtLSnf = New common.MyNumBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtLFat = New common.MyNumBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.rdbtnreset = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtGainSNFPer = New common.MyNumBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtGainFATPer = New common.MyNumBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtSNF = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtFat = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.dtpEndDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.dtstrDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.dtStartDate = New common.Controls.MyDateTimePicker()
        Me.chkInactive = New Telerik.WinControls.UI.RadCheckBox()
        Me.UsLock1 = New common.usLock()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.txtLossSNFPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLossFATPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLSnf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLFat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtGainSNFPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGainFATPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtstrDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.txtDescription.SuspendLayout()
        CType(Me.dtStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdbtnreset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpEndDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel13)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtstrDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkInactive)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(638, 286)
        Me.SplitContainer1.SplitterDistance = 247
        Me.SplitContainer1.TabIndex = 0
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.txtLossSNFPer)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox2.Controls.Add(Me.txtLossFATPer)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel10)
        Me.RadGroupBox2.Controls.Add(Me.txtLSnf)
        Me.RadGroupBox2.Controls.Add(Me.txtLFat)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox2.HeaderText = "Loss"
        Me.RadGroupBox2.Location = New System.Drawing.Point(12, 141)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(584, 49)
        Me.RadGroupBox2.TabIndex = 1092
        Me.RadGroupBox2.Text = "Loss"
        '
        'txtLossSNFPer
        '
        Me.txtLossSNFPer.BackColor = System.Drawing.Color.Transparent
        Me.txtLossSNFPer.CalculationExpression = Nothing
        Me.txtLossSNFPer.DecimalPlaces = 2
        Me.txtLossSNFPer.FieldCode = Nothing
        Me.txtLossSNFPer.FieldDesc = Nothing
        Me.txtLossSNFPer.FieldMaxLength = 0
        Me.txtLossSNFPer.FieldName = Nothing
        Me.txtLossSNFPer.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtLossSNFPer.isCalculatedField = False
        Me.txtLossSNFPer.IsSourceFromTable = False
        Me.txtLossSNFPer.IsSourceFromValueList = False
        Me.txtLossSNFPer.IsUnique = False
        Me.txtLossSNFPer.Location = New System.Drawing.Point(508, 19)
        Me.txtLossSNFPer.MaxLength = 6
        Me.txtLossSNFPer.MendatroryField = False
        Me.txtLossSNFPer.MyLinkLable1 = Me.MyLabel9
        Me.txtLossSNFPer.MyLinkLable2 = Nothing
        Me.txtLossSNFPer.Name = "txtLossSNFPer"
        Me.txtLossSNFPer.ReferenceFieldDesc = Nothing
        Me.txtLossSNFPer.ReferenceFieldName = Nothing
        Me.txtLossSNFPer.ReferenceTableName = Nothing
        Me.txtLossSNFPer.Size = New System.Drawing.Size(74, 21)
        Me.txtLossSNFPer.TabIndex = 1100
        Me.txtLossSNFPer.Text = "0"
        Me.txtLossSNFPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLossSNFPer.Value = 0R
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(434, 21)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel9.TabIndex = 1099
        Me.MyLabel9.Text = "SNF% Allow"
        '
        'txtLossFATPer
        '
        Me.txtLossFATPer.BackColor = System.Drawing.Color.Transparent
        Me.txtLossFATPer.CalculationExpression = Nothing
        Me.txtLossFATPer.DecimalPlaces = 2
        Me.txtLossFATPer.FieldCode = Nothing
        Me.txtLossFATPer.FieldDesc = Nothing
        Me.txtLossFATPer.FieldMaxLength = 0
        Me.txtLossFATPer.FieldName = Nothing
        Me.txtLossFATPer.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtLossFATPer.isCalculatedField = False
        Me.txtLossFATPer.IsSourceFromTable = False
        Me.txtLossFATPer.IsSourceFromValueList = False
        Me.txtLossFATPer.IsUnique = False
        Me.txtLossFATPer.Location = New System.Drawing.Point(355, 18)
        Me.txtLossFATPer.MaxLength = 6
        Me.txtLossFATPer.MendatroryField = False
        Me.txtLossFATPer.MyLinkLable1 = Me.MyLabel10
        Me.txtLossFATPer.MyLinkLable2 = Nothing
        Me.txtLossFATPer.Name = "txtLossFATPer"
        Me.txtLossFATPer.ReferenceFieldDesc = Nothing
        Me.txtLossFATPer.ReferenceFieldName = Nothing
        Me.txtLossFATPer.ReferenceTableName = Nothing
        Me.txtLossFATPer.Size = New System.Drawing.Size(74, 21)
        Me.txtLossFATPer.TabIndex = 1098
        Me.txtLossFATPer.Text = "0"
        Me.txtLossFATPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLossFATPer.Value = 0R
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel10.Location = New System.Drawing.Point(285, 20)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel10.TabIndex = 1097
        Me.MyLabel10.Text = "FAT% Allow"
        '
        'txtLSnf
        '
        Me.txtLSnf.BackColor = System.Drawing.Color.Transparent
        Me.txtLSnf.CalculationExpression = Nothing
        Me.txtLSnf.DecimalPlaces = 2
        Me.txtLSnf.FieldCode = Nothing
        Me.txtLSnf.FieldDesc = Nothing
        Me.txtLSnf.FieldMaxLength = 0
        Me.txtLSnf.FieldName = Nothing
        Me.txtLSnf.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtLSnf.isCalculatedField = False
        Me.txtLSnf.IsSourceFromTable = False
        Me.txtLSnf.IsSourceFromValueList = False
        Me.txtLSnf.IsUnique = False
        Me.txtLSnf.Location = New System.Drawing.Point(207, 18)
        Me.txtLSnf.MaxLength = 6
        Me.txtLSnf.MendatroryField = False
        Me.txtLSnf.MyLinkLable1 = Me.MyLabel6
        Me.txtLSnf.MyLinkLable2 = Nothing
        Me.txtLSnf.Name = "txtLSnf"
        Me.txtLSnf.ReferenceFieldDesc = Nothing
        Me.txtLSnf.ReferenceFieldName = Nothing
        Me.txtLSnf.ReferenceTableName = Nothing
        Me.txtLSnf.Size = New System.Drawing.Size(74, 21)
        Me.txtLSnf.TabIndex = 1094
        Me.txtLSnf.Text = "0"
        Me.txtLSnf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLSnf.Value = 0R
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(147, 20)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel6.TabIndex = 1092
        Me.MyLabel6.Text = "SNF Rate"
        '
        'txtLFat
        '
        Me.txtLFat.BackColor = System.Drawing.Color.Transparent
        Me.txtLFat.CalculationExpression = Nothing
        Me.txtLFat.DecimalPlaces = 2
        Me.txtLFat.FieldCode = Nothing
        Me.txtLFat.FieldDesc = Nothing
        Me.txtLFat.FieldMaxLength = 0
        Me.txtLFat.FieldName = Nothing
        Me.txtLFat.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtLFat.isCalculatedField = False
        Me.txtLFat.IsSourceFromTable = False
        Me.txtLFat.IsSourceFromValueList = False
        Me.txtLFat.IsUnique = False
        Me.txtLFat.Location = New System.Drawing.Point(69, 18)
        Me.txtLFat.MaxLength = 6
        Me.txtLFat.MendatroryField = False
        Me.txtLFat.MyLinkLable1 = Me.MyLabel5
        Me.txtLFat.MyLinkLable2 = Nothing
        Me.txtLFat.Name = "txtLFat"
        Me.txtLFat.ReferenceFieldDesc = Nothing
        Me.txtLFat.ReferenceFieldName = Nothing
        Me.txtLFat.ReferenceTableName = Nothing
        Me.txtLFat.Size = New System.Drawing.Size(74, 21)
        Me.txtLFat.TabIndex = 1093
        Me.txtLFat.Text = "0"
        Me.txtLFat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLFat.Value = 0R
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(8, 20)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel5.TabIndex = 1090
        Me.MyLabel5.Text = "FAT Rate"
        '
        'rdbtnreset
        '
        Me.rdbtnreset.Image = Global.XpertERPBulkProcurement.My.Resources.Resources._new
        Me.rdbtnreset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdbtnreset.Location = New System.Drawing.Point(420, 12)
        Me.rdbtnreset.Name = "rdbtnreset"
        Me.rdbtnreset.Size = New System.Drawing.Size(19, 21)
        Me.rdbtnreset.TabIndex = 1091
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtGainSNFPer)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox1.Controls.Add(Me.txtGainFATPer)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox1.Controls.Add(Me.txtSNF)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtFat)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.HeaderText = "Gain"
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 88)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(584, 49)
        Me.RadGroupBox1.TabIndex = 1090
        Me.RadGroupBox1.Text = "Gain"
        '
        'txtGainSNFPer
        '
        Me.txtGainSNFPer.BackColor = System.Drawing.Color.Transparent
        Me.txtGainSNFPer.CalculationExpression = Nothing
        Me.txtGainSNFPer.DecimalPlaces = 2
        Me.txtGainSNFPer.FieldCode = Nothing
        Me.txtGainSNFPer.FieldDesc = Nothing
        Me.txtGainSNFPer.FieldMaxLength = 0
        Me.txtGainSNFPer.FieldName = Nothing
        Me.txtGainSNFPer.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtGainSNFPer.isCalculatedField = False
        Me.txtGainSNFPer.IsSourceFromTable = False
        Me.txtGainSNFPer.IsSourceFromValueList = False
        Me.txtGainSNFPer.IsUnique = False
        Me.txtGainSNFPer.Location = New System.Drawing.Point(508, 21)
        Me.txtGainSNFPer.MaxLength = 6
        Me.txtGainSNFPer.MendatroryField = False
        Me.txtGainSNFPer.MyLinkLable1 = Me.MyLabel8
        Me.txtGainSNFPer.MyLinkLable2 = Nothing
        Me.txtGainSNFPer.Name = "txtGainSNFPer"
        Me.txtGainSNFPer.ReferenceFieldDesc = Nothing
        Me.txtGainSNFPer.ReferenceFieldName = Nothing
        Me.txtGainSNFPer.ReferenceTableName = Nothing
        Me.txtGainSNFPer.Size = New System.Drawing.Size(74, 21)
        Me.txtGainSNFPer.TabIndex = 1096
        Me.txtGainSNFPer.Text = "0"
        Me.txtGainSNFPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtGainSNFPer.Value = 0R
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(434, 23)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel8.TabIndex = 1095
        Me.MyLabel8.Text = "SNF% Allow"
        '
        'txtGainFATPer
        '
        Me.txtGainFATPer.BackColor = System.Drawing.Color.Transparent
        Me.txtGainFATPer.CalculationExpression = Nothing
        Me.txtGainFATPer.DecimalPlaces = 2
        Me.txtGainFATPer.FieldCode = Nothing
        Me.txtGainFATPer.FieldDesc = Nothing
        Me.txtGainFATPer.FieldMaxLength = 0
        Me.txtGainFATPer.FieldName = Nothing
        Me.txtGainFATPer.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtGainFATPer.isCalculatedField = False
        Me.txtGainFATPer.IsSourceFromTable = False
        Me.txtGainFATPer.IsSourceFromValueList = False
        Me.txtGainFATPer.IsUnique = False
        Me.txtGainFATPer.Location = New System.Drawing.Point(355, 20)
        Me.txtGainFATPer.MaxLength = 6
        Me.txtGainFATPer.MendatroryField = False
        Me.txtGainFATPer.MyLinkLable1 = Me.MyLabel7
        Me.txtGainFATPer.MyLinkLable2 = Nothing
        Me.txtGainFATPer.Name = "txtGainFATPer"
        Me.txtGainFATPer.ReferenceFieldDesc = Nothing
        Me.txtGainFATPer.ReferenceFieldName = Nothing
        Me.txtGainFATPer.ReferenceTableName = Nothing
        Me.txtGainFATPer.Size = New System.Drawing.Size(74, 21)
        Me.txtGainFATPer.TabIndex = 1094
        Me.txtGainFATPer.Text = "0"
        Me.txtGainFATPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtGainFATPer.Value = 0R
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(285, 22)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel7.TabIndex = 1093
        Me.MyLabel7.Text = "FAT% Allow"
        '
        'txtSNF
        '
        Me.txtSNF.BackColor = System.Drawing.Color.Transparent
        Me.txtSNF.CalculationExpression = Nothing
        Me.txtSNF.DecimalPlaces = 2
        Me.txtSNF.FieldCode = Nothing
        Me.txtSNF.FieldDesc = Nothing
        Me.txtSNF.FieldMaxLength = 0
        Me.txtSNF.FieldName = Nothing
        Me.txtSNF.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtSNF.isCalculatedField = False
        Me.txtSNF.IsSourceFromTable = False
        Me.txtSNF.IsSourceFromValueList = False
        Me.txtSNF.IsUnique = False
        Me.txtSNF.Location = New System.Drawing.Point(207, 18)
        Me.txtSNF.MaxLength = 6
        Me.txtSNF.MendatroryField = False
        Me.txtSNF.MyLinkLable1 = Me.MyLabel4
        Me.txtSNF.MyLinkLable2 = Nothing
        Me.txtSNF.Name = "txtSNF"
        Me.txtSNF.ReferenceFieldDesc = Nothing
        Me.txtSNF.ReferenceFieldName = Nothing
        Me.txtSNF.ReferenceTableName = Nothing
        Me.txtSNF.Size = New System.Drawing.Size(74, 21)
        Me.txtSNF.TabIndex = 1092
        Me.txtSNF.Text = "0"
        Me.txtSNF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSNF.Value = 0R
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(147, 20)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel4.TabIndex = 1091
        Me.MyLabel4.Text = "SNF Rate"
        '
        'txtFat
        '
        Me.txtFat.BackColor = System.Drawing.Color.Transparent
        Me.txtFat.CalculationExpression = Nothing
        Me.txtFat.DecimalPlaces = 2
        Me.txtFat.FieldCode = Nothing
        Me.txtFat.FieldDesc = Nothing
        Me.txtFat.FieldMaxLength = 0
        Me.txtFat.FieldName = Nothing
        Me.txtFat.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtFat.isCalculatedField = False
        Me.txtFat.IsSourceFromTable = False
        Me.txtFat.IsSourceFromValueList = False
        Me.txtFat.IsUnique = False
        Me.txtFat.Location = New System.Drawing.Point(69, 18)
        Me.txtFat.MaxLength = 6
        Me.txtFat.MendatroryField = False
        Me.txtFat.MyLinkLable1 = Me.MyLabel2
        Me.txtFat.MyLinkLable2 = Nothing
        Me.txtFat.Name = "txtFat"
        Me.txtFat.ReferenceFieldDesc = Nothing
        Me.txtFat.ReferenceFieldName = Nothing
        Me.txtFat.ReferenceTableName = Nothing
        Me.txtFat.Size = New System.Drawing.Size(74, 21)
        Me.txtFat.TabIndex = 1090
        Me.txtFat.Text = "0"
        Me.txtFat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFat.Value = 0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(8, 20)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel2.TabIndex = 1089
        Me.MyLabel2.Text = "FAT Rate"
        '
        'dtpEndDate
        '
        Me.dtpEndDate.CalculationExpression = Nothing
        Me.dtpEndDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpEndDate.FieldCode = Nothing
        Me.dtpEndDate.FieldDesc = Nothing
        Me.dtpEndDate.FieldMaxLength = 0
        Me.dtpEndDate.FieldName = Nothing
        Me.dtpEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDate.isCalculatedField = False
        Me.dtpEndDate.IsSourceFromTable = False
        Me.dtpEndDate.IsSourceFromValueList = False
        Me.dtpEndDate.IsUnique = False
        Me.dtpEndDate.Location = New System.Drawing.Point(236, 63)
        Me.dtpEndDate.MendatroryField = False
        Me.dtpEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndDate.MyLinkLable1 = Me.MyLabel13
        Me.dtpEndDate.MyLinkLable2 = Nothing
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndDate.ReferenceFieldDesc = Nothing
        Me.dtpEndDate.ReferenceFieldName = Nothing
        Me.dtpEndDate.ReferenceTableName = Nothing
        Me.dtpEndDate.ShowCheckBox = True
        Me.dtpEndDate.Size = New System.Drawing.Size(91, 18)
        Me.dtpEndDate.TabIndex = 1089
        Me.dtpEndDate.TabStop = False
        Me.dtpEndDate.Text = "13/06/2011"
        Me.dtpEndDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(177, 64)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(53, 16)
        Me.MyLabel13.TabIndex = 1088
        Me.MyLabel13.Text = "End Date"
        '
        'dtstrDate
        '
        Me.dtstrDate.CalculationExpression = Nothing
        Me.dtstrDate.CustomFormat = "dd/MM/yyyy"
        Me.dtstrDate.FieldCode = Nothing
        Me.dtstrDate.FieldDesc = Nothing
        Me.dtstrDate.FieldMaxLength = 0
        Me.dtstrDate.FieldName = Nothing
        Me.dtstrDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtstrDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtstrDate.isCalculatedField = False
        Me.dtstrDate.IsSourceFromTable = False
        Me.dtstrDate.IsSourceFromValueList = False
        Me.dtstrDate.IsUnique = False
        Me.dtstrDate.Location = New System.Drawing.Point(81, 63)
        Me.dtstrDate.MendatroryField = False
        Me.dtstrDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtstrDate.MyLinkLable1 = Me.MyLabel3
        Me.dtstrDate.MyLinkLable2 = Nothing
        Me.dtstrDate.Name = "dtstrDate"
        Me.dtstrDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtstrDate.ReferenceFieldDesc = Nothing
        Me.dtstrDate.ReferenceFieldName = Nothing
        Me.dtstrDate.ReferenceTableName = Nothing
        Me.dtstrDate.Size = New System.Drawing.Size(91, 18)
        Me.dtstrDate.TabIndex = 1087
        Me.dtstrDate.TabStop = False
        Me.dtstrDate.Text = "13/06/2011"
        Me.dtstrDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(12, 64)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel3.TabIndex = 1086
        Me.MyLabel3.Text = "Start Date"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(12, 41)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel1.TabIndex = 1085
        Me.MyLabel1.Text = "Description"
        '
        'txtDescription
        '
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.Controls.Add(Me.dtStartDate)
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(81, 38)
        Me.txtDescription.MaxLength = 100
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.MyLabel1
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(515, 20)
        Me.txtDescription.TabIndex = 1084
        '
        'dtStartDate
        '
        Me.dtStartDate.CalculationExpression = Nothing
        Me.dtStartDate.CustomFormat = "dd/MM/yyyy"
        Me.dtStartDate.FieldCode = Nothing
        Me.dtStartDate.FieldDesc = Nothing
        Me.dtStartDate.FieldMaxLength = 0
        Me.dtStartDate.FieldName = Nothing
        Me.dtStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtStartDate.isCalculatedField = False
        Me.dtStartDate.IsSourceFromTable = False
        Me.dtStartDate.IsSourceFromValueList = False
        Me.dtStartDate.IsUnique = False
        Me.dtStartDate.Location = New System.Drawing.Point(0, 23)
        Me.dtStartDate.MendatroryField = False
        Me.dtStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtStartDate.MyLinkLable1 = Me.MyLabel3
        Me.dtStartDate.MyLinkLable2 = Nothing
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtStartDate.ReferenceFieldDesc = Nothing
        Me.dtStartDate.ReferenceFieldName = Nothing
        Me.dtStartDate.ReferenceTableName = Nothing
        Me.dtStartDate.Size = New System.Drawing.Size(80, 18)
        Me.dtStartDate.TabIndex = 1087
        Me.dtStartDate.TabStop = False
        Me.dtStartDate.Text = "13/06/2011"
        Me.dtStartDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'chkInactive
        '
        Me.chkInactive.Enabled = False
        Me.chkInactive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInactive.Location = New System.Drawing.Point(537, 14)
        Me.chkInactive.Name = "chkInactive"
        Me.chkInactive.Size = New System.Drawing.Size(59, 16)
        Me.chkInactive.TabIndex = 1083
        Me.chkInactive.Text = "Inactive"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(443, 12)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(88, 21)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1082
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDescription.Location = New System.Drawing.Point(12, 13)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(33, 18)
        Me.lblDescription.TabIndex = 7
        Me.lblDescription.Text = "Code"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(81, 12)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblDescription
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(339, 21)
        Me.txtCode.TabIndex = 1
        Me.txtCode.Value = ""
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(569, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 24)
        Me.btnClose.TabIndex = 112
        Me.btnClose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Location = New System.Drawing.Point(142, 6)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 24)
        Me.btnPost.TabIndex = 111
        Me.btnPost.Text = "Post"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(74, 6)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(66, 24)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(5, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 24)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        '
        'frmOwnBMCGainLossRate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(638, 286)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmOwnBMCGainLossRate"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmOwnBMCGainLossRate"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.txtLossSNFPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLossFATPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLSnf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLFat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtGainSNFPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGainFATPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtstrDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        Me.txtDescription.ResumeLayout(False)
        Me.txtDescription.PerformLayout()
        CType(Me.dtStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents chkInactive As RadCheckBox
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents dtStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents dtstrDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents dtpEndDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents rdbtnreset As RadButton
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtFat As common.MyNumBox
    Friend WithEvents txtSNF As common.MyNumBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents txtLSnf As common.MyNumBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtLFat As common.MyNumBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents btnSave As RadButton
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnPost As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents txtLossSNFPer As common.MyNumBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtLossFATPer As common.MyNumBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtGainSNFPer As common.MyNumBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtGainFATPer As common.MyNumBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
End Class
