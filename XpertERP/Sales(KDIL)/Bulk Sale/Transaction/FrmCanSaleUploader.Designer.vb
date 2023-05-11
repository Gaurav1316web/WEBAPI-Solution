<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCanSaleUploader
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.ChkCanInventoryType = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbEnterDataManual = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbImportDataFromSheet = New Telerik.WinControls.UI.RadRadioButton()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TxtToleranceinplus = New common.MyNumBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.txtToleranceinminus = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtSNFRatio = New common.MyNumBox()
        Me.TxtSNFWeightage = New common.MyNumBox()
        Me.TxtFatWeightage = New common.MyNumBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.FndPriceCode = New common.UserControls.txtFinder()
        Me.txtStanadardrate = New common.MyNumBox()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtfatRatio = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.LblLocationName = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RDSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RDDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExportFormat = New Telerik.WinControls.UI.RadButton()
        Me.btnValidate = New Telerik.WinControls.UI.RadButton()
        Me.btnSelectSheet = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.txtewaybilldate = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.TxtUOM = New common.UserControls.txtFinder()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.ChkCanInventoryType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.rdbEnterDataManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbImportDataFromSheet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.TxtToleranceinplus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToleranceinminus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSNFRatio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSNFWeightage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFatWeightage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStanadardrate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfatRatio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExportFormat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnValidate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelectSheet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtewaybilldate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExportFormat)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnValidate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSelectSheet)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1009, 546)
        Me.SplitContainer1.SplitterDistance = 511
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1009, 491)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.ChkCanInventoryType)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.fndLocation)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.LblLocationName)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(112.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(988, 445)
        Me.RadPageViewPage1.Text = "Can Sale Uploader"
        '
        'ChkCanInventoryType
        '
        Me.ChkCanInventoryType.Location = New System.Drawing.Point(379, 67)
        Me.ChkCanInventoryType.Name = "ChkCanInventoryType"
        Me.ChkCanInventoryType.Size = New System.Drawing.Size(117, 18)
        Me.ChkCanInventoryType.TabIndex = 345
        Me.ChkCanInventoryType.Text = "Can Inventory Type"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.rdbEnterDataManual)
        Me.RadGroupBox3.Controls.Add(Me.rdbImportDataFromSheet)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(373, 37)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(296, 23)
        Me.RadGroupBox3.TabIndex = 339
        '
        'rdbEnterDataManual
        '
        Me.rdbEnterDataManual.Location = New System.Drawing.Point(167, 2)
        Me.rdbEnterDataManual.Name = "rdbEnterDataManual"
        Me.rdbEnterDataManual.Size = New System.Drawing.Size(113, 18)
        Me.rdbEnterDataManual.TabIndex = 1
        Me.rdbEnterDataManual.Text = "Data Enter Manual"
        '
        'rdbImportDataFromSheet
        '
        Me.rdbImportDataFromSheet.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbImportDataFromSheet.Location = New System.Drawing.Point(6, 2)
        Me.rdbImportDataFromSheet.Name = "rdbImportDataFromSheet"
        Me.rdbImportDataFromSheet.Size = New System.Drawing.Size(141, 18)
        Me.rdbImportDataFromSheet.TabIndex = 0
        Me.rdbImportDataFromSheet.Text = "Import Data From Sheet"
        Me.rdbImportDataFromSheet.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
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
        Me.fndLocation.Location = New System.Drawing.Point(84, 4)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.RadLabel2
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(146, 20)
        Me.fndLocation.TabIndex = 2
        Me.fndLocation.Value = ""
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(10, 4)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel2.TabIndex = 38
        Me.RadLabel2.Text = "Location"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TxtUOM)
        Me.GroupBox1.Controls.Add(Me.MyLabel2)
        Me.GroupBox1.Controls.Add(Me.TxtToleranceinplus)
        Me.GroupBox1.Controls.Add(Me.MyLabel17)
        Me.GroupBox1.Controls.Add(Me.txtToleranceinminus)
        Me.GroupBox1.Controls.Add(Me.MyLabel4)
        Me.GroupBox1.Controls.Add(Me.MyLabel12)
        Me.GroupBox1.Controls.Add(Me.txtSNFRatio)
        Me.GroupBox1.Controls.Add(Me.TxtSNFWeightage)
        Me.GroupBox1.Controls.Add(Me.TxtFatWeightage)
        Me.GroupBox1.Controls.Add(Me.MyLabel13)
        Me.GroupBox1.Controls.Add(Me.MyLabel14)
        Me.GroupBox1.Controls.Add(Me.FndPriceCode)
        Me.GroupBox1.Controls.Add(Me.txtStanadardrate)
        Me.GroupBox1.Controls.Add(Me.MyLabel15)
        Me.GroupBox1.Controls.Add(Me.MyLabel16)
        Me.GroupBox1.Controls.Add(Me.txtfatRatio)
        Me.GroupBox1.Controls.Add(Me.MyLabel1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 30)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(346, 130)
        Me.GroupBox1.TabIndex = 338
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Price Detail"
        '
        'TxtToleranceinplus
        '
        Me.TxtToleranceinplus.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtToleranceinplus.CalculationExpression = Nothing
        Me.TxtToleranceinplus.DecimalPlaces = 2
        Me.TxtToleranceinplus.Enabled = False
        Me.TxtToleranceinplus.FieldCode = Nothing
        Me.TxtToleranceinplus.FieldDesc = Nothing
        Me.TxtToleranceinplus.FieldMaxLength = 0
        Me.TxtToleranceinplus.FieldName = Nothing
        Me.TxtToleranceinplus.isCalculatedField = False
        Me.TxtToleranceinplus.IsSourceFromTable = False
        Me.TxtToleranceinplus.IsSourceFromValueList = False
        Me.TxtToleranceinplus.IsUnique = False
        Me.TxtToleranceinplus.Location = New System.Drawing.Point(97, 81)
        Me.TxtToleranceinplus.MendatroryField = True
        Me.TxtToleranceinplus.MyLinkLable1 = Nothing
        Me.TxtToleranceinplus.MyLinkLable2 = Nothing
        Me.TxtToleranceinplus.Name = "TxtToleranceinplus"
        Me.TxtToleranceinplus.ReferenceFieldDesc = Nothing
        Me.TxtToleranceinplus.ReferenceFieldName = Nothing
        Me.TxtToleranceinplus.ReferenceTableName = Nothing
        Me.TxtToleranceinplus.Size = New System.Drawing.Size(69, 20)
        Me.TxtToleranceinplus.TabIndex = 347
        Me.TxtToleranceinplus.Text = "0"
        Me.TxtToleranceinplus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtToleranceinplus.Value = 0R
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel17.Location = New System.Drawing.Point(7, 82)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel17.TabIndex = 348
        Me.MyLabel17.Text = "Tolerance %(+)"
        '
        'txtToleranceinminus
        '
        Me.txtToleranceinminus.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtToleranceinminus.CalculationExpression = Nothing
        Me.txtToleranceinminus.DecimalPlaces = 2
        Me.txtToleranceinminus.Enabled = False
        Me.txtToleranceinminus.FieldCode = Nothing
        Me.txtToleranceinminus.FieldDesc = Nothing
        Me.txtToleranceinminus.FieldMaxLength = 0
        Me.txtToleranceinminus.FieldName = Nothing
        Me.txtToleranceinminus.isCalculatedField = False
        Me.txtToleranceinminus.IsSourceFromTable = False
        Me.txtToleranceinminus.IsSourceFromValueList = False
        Me.txtToleranceinminus.IsUnique = False
        Me.txtToleranceinminus.Location = New System.Drawing.Point(253, 81)
        Me.txtToleranceinminus.MendatroryField = True
        Me.txtToleranceinminus.MyLinkLable1 = Nothing
        Me.txtToleranceinminus.MyLinkLable2 = Nothing
        Me.txtToleranceinminus.Name = "txtToleranceinminus"
        Me.txtToleranceinminus.ReferenceFieldDesc = Nothing
        Me.txtToleranceinminus.ReferenceFieldName = Nothing
        Me.txtToleranceinminus.ReferenceTableName = Nothing
        Me.txtToleranceinminus.Size = New System.Drawing.Size(76, 20)
        Me.txtToleranceinminus.TabIndex = 345
        Me.txtToleranceinminus.Text = "0"
        Me.txtToleranceinminus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtToleranceinminus.Value = 0R
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(169, 82)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(81, 16)
        Me.MyLabel4.TabIndex = 346
        Me.MyLabel4.Text = "Tolerance %(-)"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel12.Location = New System.Drawing.Point(170, 60)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel12.TabIndex = 344
        Me.MyLabel12.Text = "SNF Ratio"
        '
        'txtSNFRatio
        '
        Me.txtSNFRatio.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSNFRatio.CalculationExpression = Nothing
        Me.txtSNFRatio.DecimalPlaces = 2
        Me.txtSNFRatio.Enabled = False
        Me.txtSNFRatio.FieldCode = Nothing
        Me.txtSNFRatio.FieldDesc = Nothing
        Me.txtSNFRatio.FieldMaxLength = 0
        Me.txtSNFRatio.FieldName = Nothing
        Me.txtSNFRatio.isCalculatedField = False
        Me.txtSNFRatio.IsSourceFromTable = False
        Me.txtSNFRatio.IsSourceFromValueList = False
        Me.txtSNFRatio.IsUnique = False
        Me.txtSNFRatio.Location = New System.Drawing.Point(253, 58)
        Me.txtSNFRatio.MendatroryField = True
        Me.txtSNFRatio.MyLinkLable1 = Nothing
        Me.txtSNFRatio.MyLinkLable2 = Nothing
        Me.txtSNFRatio.Name = "txtSNFRatio"
        Me.txtSNFRatio.ReferenceFieldDesc = Nothing
        Me.txtSNFRatio.ReferenceFieldName = Nothing
        Me.txtSNFRatio.ReferenceTableName = Nothing
        Me.txtSNFRatio.Size = New System.Drawing.Size(76, 20)
        Me.txtSNFRatio.TabIndex = 343
        Me.txtSNFRatio.Text = "0"
        Me.txtSNFRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSNFRatio.Value = 0R
        '
        'TxtSNFWeightage
        '
        Me.TxtSNFWeightage.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtSNFWeightage.CalculationExpression = Nothing
        Me.TxtSNFWeightage.DecimalPlaces = 2
        Me.TxtSNFWeightage.Enabled = False
        Me.TxtSNFWeightage.FieldCode = Nothing
        Me.TxtSNFWeightage.FieldDesc = Nothing
        Me.TxtSNFWeightage.FieldMaxLength = 0
        Me.TxtSNFWeightage.FieldName = Nothing
        Me.TxtSNFWeightage.isCalculatedField = False
        Me.TxtSNFWeightage.IsSourceFromTable = False
        Me.TxtSNFWeightage.IsSourceFromValueList = False
        Me.TxtSNFWeightage.IsUnique = False
        Me.TxtSNFWeightage.Location = New System.Drawing.Point(253, 35)
        Me.TxtSNFWeightage.MendatroryField = True
        Me.TxtSNFWeightage.MyLinkLable1 = Nothing
        Me.TxtSNFWeightage.MyLinkLable2 = Nothing
        Me.TxtSNFWeightage.Name = "TxtSNFWeightage"
        Me.TxtSNFWeightage.ReferenceFieldDesc = Nothing
        Me.TxtSNFWeightage.ReferenceFieldName = Nothing
        Me.TxtSNFWeightage.ReferenceTableName = Nothing
        Me.TxtSNFWeightage.Size = New System.Drawing.Size(76, 20)
        Me.TxtSNFWeightage.TabIndex = 336
        Me.TxtSNFWeightage.Text = "0"
        Me.TxtSNFWeightage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtSNFWeightage.Value = 0R
        '
        'TxtFatWeightage
        '
        Me.TxtFatWeightage.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtFatWeightage.CalculationExpression = Nothing
        Me.TxtFatWeightage.DecimalPlaces = 2
        Me.TxtFatWeightage.Enabled = False
        Me.TxtFatWeightage.FieldCode = Nothing
        Me.TxtFatWeightage.FieldDesc = Nothing
        Me.TxtFatWeightage.FieldMaxLength = 0
        Me.TxtFatWeightage.FieldName = Nothing
        Me.TxtFatWeightage.isCalculatedField = False
        Me.TxtFatWeightage.IsSourceFromTable = False
        Me.TxtFatWeightage.IsSourceFromValueList = False
        Me.TxtFatWeightage.IsUnique = False
        Me.TxtFatWeightage.Location = New System.Drawing.Point(97, 35)
        Me.TxtFatWeightage.MendatroryField = True
        Me.TxtFatWeightage.MyLinkLable1 = Nothing
        Me.TxtFatWeightage.MyLinkLable2 = Nothing
        Me.TxtFatWeightage.Name = "TxtFatWeightage"
        Me.TxtFatWeightage.ReferenceFieldDesc = Nothing
        Me.TxtFatWeightage.ReferenceFieldName = Nothing
        Me.TxtFatWeightage.ReferenceTableName = Nothing
        Me.TxtFatWeightage.Size = New System.Drawing.Size(68, 20)
        Me.TxtFatWeightage.TabIndex = 335
        Me.TxtFatWeightage.Text = "0"
        Me.TxtFatWeightage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtFatWeightage.Value = 0R
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel13.Location = New System.Drawing.Point(168, 37)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel13.TabIndex = 342
        Me.MyLabel13.Text = "SNF Weightage"
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel14.Location = New System.Drawing.Point(7, 60)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel14.TabIndex = 340
        Me.MyLabel14.Text = "FAT Ratio"
        '
        'FndPriceCode
        '
        Me.FndPriceCode.CalculationExpression = Nothing
        Me.FndPriceCode.FieldCode = Nothing
        Me.FndPriceCode.FieldDesc = Nothing
        Me.FndPriceCode.FieldMaxLength = 0
        Me.FndPriceCode.FieldName = Nothing
        Me.FndPriceCode.isCalculatedField = False
        Me.FndPriceCode.IsSourceFromTable = False
        Me.FndPriceCode.IsSourceFromValueList = False
        Me.FndPriceCode.IsUnique = False
        Me.FndPriceCode.Location = New System.Drawing.Point(96, 12)
        Me.FndPriceCode.MendatroryField = True
        Me.FndPriceCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndPriceCode.MyLinkLable1 = Nothing
        Me.FndPriceCode.MyLinkLable2 = Nothing
        Me.FndPriceCode.MyReadOnly = False
        Me.FndPriceCode.MyShowMasterFormButton = False
        Me.FndPriceCode.Name = "FndPriceCode"
        Me.FndPriceCode.ReferenceFieldDesc = Nothing
        Me.FndPriceCode.ReferenceFieldName = Nothing
        Me.FndPriceCode.ReferenceTableName = Nothing
        Me.FndPriceCode.Size = New System.Drawing.Size(233, 20)
        Me.FndPriceCode.TabIndex = 12
        Me.FndPriceCode.Value = ""
        '
        'txtStanadardrate
        '
        Me.txtStanadardrate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtStanadardrate.CalculationExpression = Nothing
        Me.txtStanadardrate.DecimalPlaces = 2
        Me.txtStanadardrate.FieldCode = Nothing
        Me.txtStanadardrate.FieldDesc = Nothing
        Me.txtStanadardrate.FieldMaxLength = 0
        Me.txtStanadardrate.FieldName = Nothing
        Me.txtStanadardrate.isCalculatedField = False
        Me.txtStanadardrate.IsSourceFromTable = False
        Me.txtStanadardrate.IsSourceFromValueList = False
        Me.txtStanadardrate.IsUnique = False
        Me.txtStanadardrate.Location = New System.Drawing.Point(97, 104)
        Me.txtStanadardrate.MendatroryField = True
        Me.txtStanadardrate.MyLinkLable1 = Nothing
        Me.txtStanadardrate.MyLinkLable2 = Nothing
        Me.txtStanadardrate.Name = "txtStanadardrate"
        Me.txtStanadardrate.ReadOnly = True
        Me.txtStanadardrate.ReferenceFieldDesc = Nothing
        Me.txtStanadardrate.ReferenceFieldName = Nothing
        Me.txtStanadardrate.ReferenceTableName = Nothing
        Me.txtStanadardrate.Size = New System.Drawing.Size(69, 20)
        Me.txtStanadardrate.TabIndex = 338
        Me.txtStanadardrate.Text = "0"
        Me.txtStanadardrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtStanadardrate.Value = 0R
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel15.Location = New System.Drawing.Point(7, 105)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel15.TabIndex = 341
        Me.MyLabel15.Text = "Standard Rate"
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel16.Location = New System.Drawing.Point(7, 37)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel16.TabIndex = 339
        Me.MyLabel16.Text = "FAT Weightage"
        '
        'txtfatRatio
        '
        Me.txtfatRatio.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtfatRatio.CalculationExpression = Nothing
        Me.txtfatRatio.DecimalPlaces = 2
        Me.txtfatRatio.Enabled = False
        Me.txtfatRatio.FieldCode = Nothing
        Me.txtfatRatio.FieldDesc = Nothing
        Me.txtfatRatio.FieldMaxLength = 0
        Me.txtfatRatio.FieldName = Nothing
        Me.txtfatRatio.isCalculatedField = False
        Me.txtfatRatio.IsSourceFromTable = False
        Me.txtfatRatio.IsSourceFromValueList = False
        Me.txtfatRatio.IsUnique = False
        Me.txtfatRatio.Location = New System.Drawing.Point(97, 58)
        Me.txtfatRatio.MendatroryField = True
        Me.txtfatRatio.MyLinkLable1 = Nothing
        Me.txtfatRatio.MyLinkLable2 = Nothing
        Me.txtfatRatio.Name = "txtfatRatio"
        Me.txtfatRatio.ReferenceFieldDesc = Nothing
        Me.txtfatRatio.ReferenceFieldName = Nothing
        Me.txtfatRatio.ReferenceTableName = Nothing
        Me.txtfatRatio.Size = New System.Drawing.Size(69, 20)
        Me.txtfatRatio.TabIndex = 337
        Me.txtfatRatio.Text = "0"
        Me.txtfatRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtfatRatio.Value = 0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(7, 14)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel1.TabIndex = 321
        Me.MyLabel1.Text = "Price Chart"
        '
        'LblLocationName
        '
        Me.LblLocationName.AutoSize = False
        Me.LblLocationName.BorderVisible = True
        Me.LblLocationName.FieldName = Nothing
        Me.LblLocationName.Location = New System.Drawing.Point(241, 4)
        Me.LblLocationName.Name = "LblLocationName"
        Me.LblLocationName.Size = New System.Drawing.Size(291, 20)
        Me.LblLocationName.TabIndex = 312
        Me.LblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 166)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(983, 277)
        Me.RadGroupBox2.TabIndex = 14
        Me.RadGroupBox2.Text = "Item Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(963, 247)
        Me.gv1.TabIndex = 13
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(988, 445)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(988, 445)
        Me.UcAttachment1.TabIndex = 1
        Me.UcAttachment1.TabStop = False
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1009, 20)
        Me.RadMenu1.TabIndex = 10
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RDSaveLayout, Me.RDDeleteLayout, Me.RmImport})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RDSaveLayout
        '
        Me.RDSaveLayout.AccessibleDescription = "Save Layout"
        Me.RDSaveLayout.AccessibleName = "Save Layout"
        Me.RDSaveLayout.Name = "RDSaveLayout"
        Me.RDSaveLayout.Text = "Save Layout"
        '
        'RDDeleteLayout
        '
        Me.RDDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.RDDeleteLayout.AccessibleName = "Delete Layout"
        Me.RDDeleteLayout.Name = "RDDeleteLayout"
        Me.RDDeleteLayout.Text = "Delete Layout"
        '
        'RmImport
        '
        Me.RmImport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RmImport.Name = "RmImport"
        Me.RmImport.Text = ""
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "RadMenuItem1"
        Me.RadMenuItem1.AccessibleName = "RadMenuItem1"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "RadMenuItem1"
        '
        'btnExportFormat
        '
        Me.btnExportFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExportFormat.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportFormat.Location = New System.Drawing.Point(322, 5)
        Me.btnExportFormat.Name = "btnExportFormat"
        Me.btnExportFormat.Size = New System.Drawing.Size(83, 20)
        Me.btnExportFormat.TabIndex = 12
        Me.btnExportFormat.Text = "Export Format"
        '
        'btnValidate
        '
        Me.btnValidate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnValidate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnValidate.Location = New System.Drawing.Point(250, 5)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(66, 20)
        Me.btnValidate.TabIndex = 11
        Me.btnValidate.Text = "Validate"
        '
        'btnSelectSheet
        '
        Me.btnSelectSheet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelectSheet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectSheet.Location = New System.Drawing.Point(172, 5)
        Me.btnSelectSheet.Name = "btnSelectSheet"
        Me.btnSelectSheet.Size = New System.Drawing.Size(72, 20)
        Me.btnSelectSheet.TabIndex = 9
        Me.btnSelectSheet.Text = "Select Sheet"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(93, 5)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(73, 20)
        Me.btnReset.TabIndex = 6
        Me.btnReset.Text = "Reset"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(446, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        Me.btnPost.Visible = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(922, 4)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 5
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(14, 4)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'txtewaybilldate
        '
        Me.txtewaybilldate.CalculationExpression = Nothing
        Me.txtewaybilldate.CustomFormat = "dd/MM/yyyy"
        Me.txtewaybilldate.FieldCode = Nothing
        Me.txtewaybilldate.FieldDesc = Nothing
        Me.txtewaybilldate.FieldMaxLength = 0
        Me.txtewaybilldate.FieldName = Nothing
        Me.txtewaybilldate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtewaybilldate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtewaybilldate.isCalculatedField = False
        Me.txtewaybilldate.IsSourceFromTable = False
        Me.txtewaybilldate.IsSourceFromValueList = False
        Me.txtewaybilldate.IsUnique = False
        Me.txtewaybilldate.Location = New System.Drawing.Point(97, 32)
        Me.txtewaybilldate.MendatroryField = False
        Me.txtewaybilldate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtewaybilldate.MyLinkLable1 = Nothing
        Me.txtewaybilldate.MyLinkLable2 = Nothing
        Me.txtewaybilldate.Name = "txtewaybilldate"
        Me.txtewaybilldate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtewaybilldate.ReferenceFieldDesc = Nothing
        Me.txtewaybilldate.ReferenceFieldName = Nothing
        Me.txtewaybilldate.ReferenceTableName = Nothing
        Me.txtewaybilldate.ShowCheckBox = True
        Me.txtewaybilldate.Size = New System.Drawing.Size(146, 18)
        Me.txtewaybilldate.TabIndex = 337
        Me.txtewaybilldate.TabStop = False
        Me.txtewaybilldate.Text = "13/06/2011"
        Me.txtewaybilldate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(170, 105)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel2.TabIndex = 346
        Me.MyLabel2.Text = "UOM"
        '
        'TxtUOM
        '
        Me.TxtUOM.CalculationExpression = Nothing
        Me.TxtUOM.FieldCode = Nothing
        Me.TxtUOM.FieldDesc = Nothing
        Me.TxtUOM.FieldMaxLength = 0
        Me.TxtUOM.FieldName = Nothing
        Me.TxtUOM.isCalculatedField = False
        Me.TxtUOM.IsSourceFromTable = False
        Me.TxtUOM.IsSourceFromValueList = False
        Me.TxtUOM.IsUnique = False
        Me.TxtUOM.Location = New System.Drawing.Point(253, 104)
        Me.TxtUOM.MendatroryField = False
        Me.TxtUOM.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUOM.MyLinkLable1 = Me.MyLabel2
        Me.TxtUOM.MyLinkLable2 = Nothing
        Me.TxtUOM.MyReadOnly = False
        Me.TxtUOM.MyShowMasterFormButton = False
        Me.TxtUOM.Name = "TxtUOM"
        Me.TxtUOM.ReferenceFieldDesc = Nothing
        Me.TxtUOM.ReferenceFieldName = Nothing
        Me.TxtUOM.ReferenceTableName = Nothing
        Me.TxtUOM.Size = New System.Drawing.Size(76, 20)
        Me.TxtUOM.TabIndex = 347
        Me.TxtUOM.Value = ""
        '
        'FrmCanSaleUploader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1009, 546)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmCanSaleUploader"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Can Sale Uploader"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.ChkCanInventoryType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.rdbEnterDataManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbImportDataFromSheet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.TxtToleranceinplus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToleranceinminus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSNFRatio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSNFWeightage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFatWeightage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStanadardrate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfatRatio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExportFormat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnValidate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelectSheet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtewaybilldate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents LblLocationName As common.Controls.MyLabel
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RDSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RDDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents txtewaybilldate As common.Controls.MyDateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtToleranceinplus As common.MyNumBox
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents txtToleranceinminus As common.MyNumBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtSNFRatio As common.MyNumBox
    Friend WithEvents TxtSNFWeightage As common.MyNumBox
    Friend WithEvents TxtFatWeightage As common.MyNumBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents FndPriceCode As common.UserControls.txtFinder
    Friend WithEvents txtStanadardrate As common.MyNumBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtfatRatio As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbEnterDataManual As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbImportDataFromSheet As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btnSelectSheet As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnValidate As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExportFormat As Telerik.WinControls.UI.RadButton
    Friend WithEvents ChkCanInventoryType As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents TxtUOM As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
End Class

