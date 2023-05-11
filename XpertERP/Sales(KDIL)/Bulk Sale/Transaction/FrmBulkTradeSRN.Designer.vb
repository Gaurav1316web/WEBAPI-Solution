<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBulkTradeSRN
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TxtTolerance = New common.MyNumBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
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
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblVendor = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.FndVendor = New common.UserControls.txtFinder()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.lblTotRAmt1 = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.fndLocationCode = New common.UserControls.txtFinder()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RDSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RDDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtTanker = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.TxtTolerance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTanker, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(906, 549)
        Me.SplitContainer1.SplitterDistance = 506
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
        Me.RadPageView1.Size = New System.Drawing.Size(906, 486)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtTanker)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.lblVendor)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.FndVendor)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocationName)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel11)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotRAmt1)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.fndLocationCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(67.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(885, 440)
        Me.RadPageViewPage1.Text = "SRN Note"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TxtTolerance)
        Me.GroupBox1.Controls.Add(Me.MyLabel17)
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
        Me.GroupBox1.Controls.Add(Me.MyLabel2)
        Me.GroupBox1.Location = New System.Drawing.Point(536, 23)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(346, 107)
        Me.GroupBox1.TabIndex = 339
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Price Detail"
        '
        'TxtTolerance
        '
        Me.TxtTolerance.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtTolerance.CalculationExpression = Nothing
        Me.TxtTolerance.DecimalPlaces = 2
        Me.TxtTolerance.Enabled = False
        Me.TxtTolerance.FieldCode = Nothing
        Me.TxtTolerance.FieldDesc = Nothing
        Me.TxtTolerance.FieldMaxLength = 0
        Me.TxtTolerance.FieldName = Nothing
        Me.TxtTolerance.isCalculatedField = False
        Me.TxtTolerance.IsSourceFromTable = False
        Me.TxtTolerance.IsSourceFromValueList = False
        Me.TxtTolerance.IsUnique = False
        Me.TxtTolerance.Location = New System.Drawing.Point(97, 81)
        Me.TxtTolerance.MendatroryField = True
        Me.TxtTolerance.MyLinkLable1 = Nothing
        Me.TxtTolerance.MyLinkLable2 = Nothing
        Me.TxtTolerance.Name = "TxtTolerance"
        Me.TxtTolerance.ReferenceFieldDesc = Nothing
        Me.TxtTolerance.ReferenceFieldName = Nothing
        Me.TxtTolerance.ReferenceTableName = Nothing
        Me.TxtTolerance.Size = New System.Drawing.Size(69, 20)
        Me.TxtTolerance.TabIndex = 347
        Me.TxtTolerance.Text = "0"
        Me.TxtTolerance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtTolerance.Value = 0.0R
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel17.Location = New System.Drawing.Point(7, 82)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel17.TabIndex = 348
        Me.MyLabel17.Text = "Tolerance %"
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
        Me.txtSNFRatio.Location = New System.Drawing.Point(263, 58)
        Me.txtSNFRatio.MendatroryField = True
        Me.txtSNFRatio.MyLinkLable1 = Nothing
        Me.txtSNFRatio.MyLinkLable2 = Nothing
        Me.txtSNFRatio.Name = "txtSNFRatio"
        Me.txtSNFRatio.ReferenceFieldDesc = Nothing
        Me.txtSNFRatio.ReferenceFieldName = Nothing
        Me.txtSNFRatio.ReferenceTableName = Nothing
        Me.txtSNFRatio.Size = New System.Drawing.Size(66, 20)
        Me.txtSNFRatio.TabIndex = 343
        Me.txtSNFRatio.Text = "0"
        Me.txtSNFRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSNFRatio.Value = 0.0R
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
        Me.TxtSNFWeightage.Location = New System.Drawing.Point(263, 35)
        Me.TxtSNFWeightage.MendatroryField = True
        Me.TxtSNFWeightage.MyLinkLable1 = Nothing
        Me.TxtSNFWeightage.MyLinkLable2 = Nothing
        Me.TxtSNFWeightage.Name = "TxtSNFWeightage"
        Me.TxtSNFWeightage.ReferenceFieldDesc = Nothing
        Me.TxtSNFWeightage.ReferenceFieldName = Nothing
        Me.TxtSNFWeightage.ReferenceTableName = Nothing
        Me.TxtSNFWeightage.Size = New System.Drawing.Size(66, 20)
        Me.TxtSNFWeightage.TabIndex = 336
        Me.TxtSNFWeightage.Text = "0"
        Me.TxtSNFWeightage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtSNFWeightage.Value = 0.0R
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
        Me.TxtFatWeightage.Value = 0.0R
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
        Me.FndPriceCode.Location = New System.Drawing.Point(97, 11)
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
        Me.FndPriceCode.Size = New System.Drawing.Size(239, 20)
        Me.FndPriceCode.TabIndex = 11
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
        Me.txtStanadardrate.Location = New System.Drawing.Point(263, 81)
        Me.txtStanadardrate.MendatroryField = True
        Me.txtStanadardrate.MyLinkLable1 = Nothing
        Me.txtStanadardrate.MyLinkLable2 = Nothing
        Me.txtStanadardrate.Name = "txtStanadardrate"
        Me.txtStanadardrate.ReferenceFieldDesc = Nothing
        Me.txtStanadardrate.ReferenceFieldName = Nothing
        Me.txtStanadardrate.ReferenceTableName = Nothing
        Me.txtStanadardrate.Size = New System.Drawing.Size(68, 20)
        Me.txtStanadardrate.TabIndex = 338
        Me.txtStanadardrate.Text = "0"
        Me.txtStanadardrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtStanadardrate.Value = 0.0R
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel15.Location = New System.Drawing.Point(172, 82)
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
        Me.txtfatRatio.Value = 0.0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(7, 14)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel2.TabIndex = 321
        Me.MyLabel2.Text = "Price Chart"
        '
        'lblVendor
        '
        Me.lblVendor.AutoSize = False
        Me.lblVendor.BorderVisible = True
        Me.lblVendor.FieldName = Nothing
        Me.lblVendor.Location = New System.Drawing.Point(240, 46)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Size = New System.Drawing.Size(291, 20)
        Me.lblVendor.TabIndex = 9
        Me.lblVendor.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(22, 48)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel3.TabIndex = 7
        Me.MyLabel3.Text = "Vendor"
        '
        'FndVendor
        '
        Me.FndVendor.CalculationExpression = Nothing
        Me.FndVendor.FieldCode = Nothing
        Me.FndVendor.FieldDesc = Nothing
        Me.FndVendor.FieldMaxLength = 0
        Me.FndVendor.FieldName = Nothing
        Me.FndVendor.isCalculatedField = False
        Me.FndVendor.IsSourceFromTable = False
        Me.FndVendor.IsSourceFromValueList = False
        Me.FndVendor.IsUnique = False
        Me.FndVendor.Location = New System.Drawing.Point(89, 46)
        Me.FndVendor.MendatroryField = True
        Me.FndVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndVendor.MyLinkLable1 = Nothing
        Me.FndVendor.MyLinkLable2 = Nothing
        Me.FndVendor.MyReadOnly = False
        Me.FndVendor.MyShowMasterFormButton = False
        Me.FndVendor.Name = "FndVendor"
        Me.FndVendor.ReferenceFieldDesc = Nothing
        Me.FndVendor.ReferenceFieldName = Nothing
        Me.FndVendor.ReferenceTableName = Nothing
        Me.FndVendor.Size = New System.Drawing.Size(146, 20)
        Me.FndVendor.TabIndex = 8
        Me.FndVendor.Value = ""
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Location = New System.Drawing.Point(241, 23)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(291, 20)
        Me.lblLocationName.TabIndex = 6
        Me.lblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(526, -1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(92, 22)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 334
        '
        'MyLabel11
        '
        Me.MyLabel11.AutoSize = False
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(22, 72)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(63, 29)
        Me.MyLabel11.TabIndex = 12
        Me.MyLabel11.Text = "Document Amount"
        '
        'lblTotRAmt1
        '
        Me.lblTotRAmt1.AutoSize = False
        Me.lblTotRAmt1.BorderVisible = True
        Me.lblTotRAmt1.FieldName = Nothing
        Me.lblTotRAmt1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt1.Location = New System.Drawing.Point(89, 70)
        Me.lblTotRAmt1.Name = "lblTotRAmt1"
        Me.lblTotRAmt1.Size = New System.Drawing.Size(147, 20)
        Me.lblTotRAmt1.TabIndex = 13
        Me.lblTotRAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel10.Location = New System.Drawing.Point(22, 2)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel10.TabIndex = 0
        Me.MyLabel10.Text = "SRN No"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(22, 25)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel5.TabIndex = 4
        Me.MyLabel5.Text = "Location"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
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
        Me.txtDate.Location = New System.Drawing.Point(381, 1)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(126, 18)
        Me.txtDate.TabIndex = 3
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(350, 2)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 2
        Me.RadLabel4.Text = "Date"
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
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 136)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(880, 301)
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
        Me.gv1.Size = New System.Drawing.Size(860, 271)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'fndLocationCode
        '
        Me.fndLocationCode.CalculationExpression = Nothing
        Me.fndLocationCode.Enabled = False
        Me.fndLocationCode.FieldCode = Nothing
        Me.fndLocationCode.FieldDesc = Nothing
        Me.fndLocationCode.FieldMaxLength = 0
        Me.fndLocationCode.FieldName = Nothing
        Me.fndLocationCode.isCalculatedField = False
        Me.fndLocationCode.IsSourceFromTable = False
        Me.fndLocationCode.IsSourceFromValueList = False
        Me.fndLocationCode.IsUnique = False
        Me.fndLocationCode.Location = New System.Drawing.Point(90, 23)
        Me.fndLocationCode.MendatroryField = True
        Me.fndLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocationCode.MyLinkLable1 = Nothing
        Me.fndLocationCode.MyLinkLable2 = Nothing
        Me.fndLocationCode.MyReadOnly = False
        Me.fndLocationCode.MyShowMasterFormButton = False
        Me.fndLocationCode.Name = "fndLocationCode"
        Me.fndLocationCode.ReferenceFieldDesc = Nothing
        Me.fndLocationCode.ReferenceFieldName = Nothing
        Me.fndLocationCode.ReferenceTableName = Nothing
        Me.fndLocationCode.Size = New System.Drawing.Size(146, 20)
        Me.fndLocationCode.TabIndex = 5
        Me.fndLocationCode.Value = ""
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(90, 0)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Nothing
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 20)
        Me.txtDocNo.TabIndex = 1
        Me.txtDocNo.Value = ""
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(979, 544)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(979, 544)
        Me.UcAttachment1.TabIndex = 0
        Me.UcAttachment1.TabStop = False
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(906, 20)
        Me.RadMenu1.TabIndex = 12
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RDSaveLayout, Me.RDDeleteLayout})
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
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(91, 12)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(821, 12)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(170, 12)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(12, 12)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(242, 72)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(59, 16)
        Me.MyLabel7.TabIndex = 340
        Me.MyLabel7.Text = "Tanker No"
        '
        'txtTanker
        '
        Me.txtTanker.AutoSize = False
        Me.txtTanker.BorderVisible = True
        Me.txtTanker.FieldName = Nothing
        Me.txtTanker.Location = New System.Drawing.Point(306, 70)
        Me.txtTanker.Name = "txtTanker"
        Me.txtTanker.Size = New System.Drawing.Size(224, 20)
        Me.txtTanker.TabIndex = 341
        Me.txtTanker.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FrmBulkTradeSRN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(906, 549)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmBulkTradeSRN"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bulk Trade SRN"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.TxtTolerance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTanker, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt1 As common.Controls.MyLabel
    Friend WithEvents FndPriceCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents fndLocationCode As common.UserControls.txtFinder
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents lblVendor As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents FndVendor As common.UserControls.txtFinder
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RDSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RDDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtTolerance As common.MyNumBox
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtSNFRatio As common.MyNumBox
    Friend WithEvents TxtSNFWeightage As common.MyNumBox
    Friend WithEvents TxtFatWeightage As common.MyNumBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents txtStanadardrate As common.MyNumBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtfatRatio As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtTanker As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
End Class

