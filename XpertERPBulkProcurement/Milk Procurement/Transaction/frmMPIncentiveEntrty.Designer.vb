<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMPIncentiveEntrty
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.txtRahatKampekatFeedRate = New common.MyNumBox()
        Me.lblRahatKampekatFeedRate = New common.Controls.MyLabel()
        Me.txtSailejRate = New common.MyNumBox()
        Me.lblSailejRate = New common.Controls.MyLabel()
        Me.txtMineralMixture = New common.MyNumBox()
        Me.lblMineralMixture = New common.Controls.MyLabel()
        Me.txtPashuAahar = New common.MyNumBox()
        Me.lblPashuAahar = New common.Controls.MyLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnFATSNFNA = New common.Controls.MyRadioButton()
        Me.rbtnFATSNFKG = New common.Controls.MyRadioButton()
        Me.rbtnFATSNFPer = New common.Controls.MyRadioButton()
        Me.txtIncentiveRate = New common.MyNumBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.lblPending = New common.usLock()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel34 = New common.Controls.MyLabel()
        Me.lblMCCDesc = New common.Controls.MyLabel()
        Me.txtMCC = New common.UserControls.txtFinder()
        Me.mtxtVLC = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtdate = New common.Controls.MyDateTimePicker()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.lblCode = New common.Controls.MyLabel()
        Me.txtDocumentNo = New common.UserControls.txtNavigator()
        Me.gvItem = New common.UserControls.MyRadGridView()
        Me.btnAddMissing = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.txtRahatKampekatFeedRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRahatKampekatFeedRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSailejRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSailejRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMineralMixture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMineralMixture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPashuAahar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPashuAahar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnFATSNFNA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnFATSNFKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnFATSNFPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIncentiveRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddMissing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAddMissing)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(996, 489)
        Me.SplitContainer1.SplitterDistance = 452
        Me.SplitContainer1.TabIndex = 1
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRahatKampekatFeedRate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRahatKampekatFeedRate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtSailejRate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblSailejRate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtMineralMixture)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblMineralMixture)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtPashuAahar)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblPashuAahar)
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtIncentiveRate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel11)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtToDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblPending)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtFromDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel34)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblMCCDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtMCC)
        Me.SplitContainer2.Panel1.Controls.Add(Me.mtxtVLC)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtdate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDocumentNo)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gvItem)
        Me.SplitContainer2.Size = New System.Drawing.Size(996, 452)
        Me.SplitContainer2.SplitterDistance = 98
        Me.SplitContainer2.TabIndex = 0
        '
        'txtRahatKampekatFeedRate
        '
        Me.txtRahatKampekatFeedRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtRahatKampekatFeedRate.CalculationExpression = Nothing
        Me.txtRahatKampekatFeedRate.DecimalPlaces = 5
        Me.txtRahatKampekatFeedRate.FieldCode = Nothing
        Me.txtRahatKampekatFeedRate.FieldDesc = Nothing
        Me.txtRahatKampekatFeedRate.FieldMaxLength = 0
        Me.txtRahatKampekatFeedRate.FieldName = Nothing
        Me.txtRahatKampekatFeedRate.isCalculatedField = False
        Me.txtRahatKampekatFeedRate.IsSourceFromTable = False
        Me.txtRahatKampekatFeedRate.IsSourceFromValueList = False
        Me.txtRahatKampekatFeedRate.IsUnique = False
        Me.txtRahatKampekatFeedRate.Location = New System.Drawing.Point(851, 74)
        Me.txtRahatKampekatFeedRate.MendatroryField = True
        Me.txtRahatKampekatFeedRate.MyLinkLable1 = Nothing
        Me.txtRahatKampekatFeedRate.MyLinkLable2 = Nothing
        Me.txtRahatKampekatFeedRate.Name = "txtRahatKampekatFeedRate"
        Me.txtRahatKampekatFeedRate.ReferenceFieldDesc = Nothing
        Me.txtRahatKampekatFeedRate.ReferenceFieldName = Nothing
        Me.txtRahatKampekatFeedRate.ReferenceTableName = Nothing
        Me.txtRahatKampekatFeedRate.Size = New System.Drawing.Size(75, 20)
        Me.txtRahatKampekatFeedRate.TabIndex = 24
        Me.txtRahatKampekatFeedRate.Text = "0"
        Me.txtRahatKampekatFeedRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRahatKampekatFeedRate.Value = 0R
        '
        'lblRahatKampekatFeedRate
        '
        Me.lblRahatKampekatFeedRate.FieldName = Nothing
        Me.lblRahatKampekatFeedRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRahatKampekatFeedRate.Location = New System.Drawing.Point(698, 76)
        Me.lblRahatKampekatFeedRate.Name = "lblRahatKampekatFeedRate"
        Me.lblRahatKampekatFeedRate.Size = New System.Drawing.Size(147, 16)
        Me.lblRahatKampekatFeedRate.TabIndex = 25
        Me.lblRahatKampekatFeedRate.Text = "Rahat Kampekat Feed Rate"
        '
        'txtSailejRate
        '
        Me.txtSailejRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSailejRate.CalculationExpression = Nothing
        Me.txtSailejRate.DecimalPlaces = 5
        Me.txtSailejRate.FieldCode = Nothing
        Me.txtSailejRate.FieldDesc = Nothing
        Me.txtSailejRate.FieldMaxLength = 0
        Me.txtSailejRate.FieldName = Nothing
        Me.txtSailejRate.isCalculatedField = False
        Me.txtSailejRate.IsSourceFromTable = False
        Me.txtSailejRate.IsSourceFromValueList = False
        Me.txtSailejRate.IsUnique = False
        Me.txtSailejRate.Location = New System.Drawing.Point(851, 52)
        Me.txtSailejRate.MendatroryField = True
        Me.txtSailejRate.MyLinkLable1 = Nothing
        Me.txtSailejRate.MyLinkLable2 = Nothing
        Me.txtSailejRate.Name = "txtSailejRate"
        Me.txtSailejRate.ReferenceFieldDesc = Nothing
        Me.txtSailejRate.ReferenceFieldName = Nothing
        Me.txtSailejRate.ReferenceTableName = Nothing
        Me.txtSailejRate.Size = New System.Drawing.Size(75, 20)
        Me.txtSailejRate.TabIndex = 22
        Me.txtSailejRate.Text = "0"
        Me.txtSailejRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSailejRate.Value = 0R
        '
        'lblSailejRate
        '
        Me.lblSailejRate.FieldName = Nothing
        Me.lblSailejRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSailejRate.Location = New System.Drawing.Point(698, 54)
        Me.lblSailejRate.Name = "lblSailejRate"
        Me.lblSailejRate.Size = New System.Drawing.Size(61, 16)
        Me.lblSailejRate.TabIndex = 23
        Me.lblSailejRate.Text = "Sailej Rate"
        '
        'txtMineralMixture
        '
        Me.txtMineralMixture.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMineralMixture.CalculationExpression = Nothing
        Me.txtMineralMixture.DecimalPlaces = 5
        Me.txtMineralMixture.FieldCode = Nothing
        Me.txtMineralMixture.FieldDesc = Nothing
        Me.txtMineralMixture.FieldMaxLength = 0
        Me.txtMineralMixture.FieldName = Nothing
        Me.txtMineralMixture.isCalculatedField = False
        Me.txtMineralMixture.IsSourceFromTable = False
        Me.txtMineralMixture.IsSourceFromValueList = False
        Me.txtMineralMixture.IsUnique = False
        Me.txtMineralMixture.Location = New System.Drawing.Point(617, 74)
        Me.txtMineralMixture.MendatroryField = True
        Me.txtMineralMixture.MyLinkLable1 = Nothing
        Me.txtMineralMixture.MyLinkLable2 = Nothing
        Me.txtMineralMixture.Name = "txtMineralMixture"
        Me.txtMineralMixture.ReferenceFieldDesc = Nothing
        Me.txtMineralMixture.ReferenceFieldName = Nothing
        Me.txtMineralMixture.ReferenceTableName = Nothing
        Me.txtMineralMixture.Size = New System.Drawing.Size(75, 20)
        Me.txtMineralMixture.TabIndex = 20
        Me.txtMineralMixture.Text = "0"
        Me.txtMineralMixture.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMineralMixture.Value = 0R
        '
        'lblMineralMixture
        '
        Me.lblMineralMixture.FieldName = Nothing
        Me.lblMineralMixture.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMineralMixture.Location = New System.Drawing.Point(503, 76)
        Me.lblMineralMixture.Name = "lblMineralMixture"
        Me.lblMineralMixture.Size = New System.Drawing.Size(111, 16)
        Me.lblMineralMixture.TabIndex = 21
        Me.lblMineralMixture.Text = "Mineral Mixture Rate"
        '
        'txtPashuAahar
        '
        Me.txtPashuAahar.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtPashuAahar.CalculationExpression = Nothing
        Me.txtPashuAahar.DecimalPlaces = 5
        Me.txtPashuAahar.FieldCode = Nothing
        Me.txtPashuAahar.FieldDesc = Nothing
        Me.txtPashuAahar.FieldMaxLength = 0
        Me.txtPashuAahar.FieldName = Nothing
        Me.txtPashuAahar.isCalculatedField = False
        Me.txtPashuAahar.IsSourceFromTable = False
        Me.txtPashuAahar.IsSourceFromValueList = False
        Me.txtPashuAahar.IsUnique = False
        Me.txtPashuAahar.Location = New System.Drawing.Point(617, 52)
        Me.txtPashuAahar.MendatroryField = True
        Me.txtPashuAahar.MyLinkLable1 = Nothing
        Me.txtPashuAahar.MyLinkLable2 = Nothing
        Me.txtPashuAahar.Name = "txtPashuAahar"
        Me.txtPashuAahar.ReferenceFieldDesc = Nothing
        Me.txtPashuAahar.ReferenceFieldName = Nothing
        Me.txtPashuAahar.ReferenceTableName = Nothing
        Me.txtPashuAahar.Size = New System.Drawing.Size(75, 20)
        Me.txtPashuAahar.TabIndex = 18
        Me.txtPashuAahar.Text = "0"
        Me.txtPashuAahar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPashuAahar.Value = 0R
        '
        'lblPashuAahar
        '
        Me.lblPashuAahar.FieldName = Nothing
        Me.lblPashuAahar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPashuAahar.Location = New System.Drawing.Point(503, 54)
        Me.lblPashuAahar.Name = "lblPashuAahar"
        Me.lblPashuAahar.Size = New System.Drawing.Size(99, 16)
        Me.lblPashuAahar.TabIndex = 19
        Me.lblPashuAahar.Text = "Pashu Aahar Rate"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnFATSNFNA)
        Me.GroupBox1.Controls.Add(Me.rbtnFATSNFKG)
        Me.GroupBox1.Controls.Add(Me.rbtnFATSNFPer)
        Me.GroupBox1.Location = New System.Drawing.Point(503, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(120, 42)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "FAT/SNF In"
        '
        'rbtnFATSNFNA
        '
        Me.rbtnFATSNFNA.Location = New System.Drawing.Point(76, 16)
        Me.rbtnFATSNFNA.MyLinkLable1 = Nothing
        Me.rbtnFATSNFNA.MyLinkLable2 = Nothing
        Me.rbtnFATSNFNA.Name = "rbtnFATSNFNA"
        Me.rbtnFATSNFNA.Size = New System.Drawing.Size(36, 18)
        Me.rbtnFATSNFNA.TabIndex = 2
        Me.rbtnFATSNFNA.TabStop = False
        Me.rbtnFATSNFNA.Text = "NA"
        '
        'rbtnFATSNFKG
        '
        Me.rbtnFATSNFKG.Location = New System.Drawing.Point(37, 16)
        Me.rbtnFATSNFKG.MyLinkLable1 = Nothing
        Me.rbtnFATSNFKG.MyLinkLable2 = Nothing
        Me.rbtnFATSNFKG.Name = "rbtnFATSNFKG"
        Me.rbtnFATSNFKG.Size = New System.Drawing.Size(33, 18)
        Me.rbtnFATSNFKG.TabIndex = 1
        Me.rbtnFATSNFKG.TabStop = False
        Me.rbtnFATSNFKG.Text = "Kg"
        '
        'rbtnFATSNFPer
        '
        Me.rbtnFATSNFPer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnFATSNFPer.Location = New System.Drawing.Point(5, 16)
        Me.rbtnFATSNFPer.MyLinkLable1 = Nothing
        Me.rbtnFATSNFPer.MyLinkLable2 = Nothing
        Me.rbtnFATSNFPer.Name = "rbtnFATSNFPer"
        Me.rbtnFATSNFPer.Size = New System.Drawing.Size(29, 18)
        Me.rbtnFATSNFPer.TabIndex = 0
        Me.rbtnFATSNFPer.Text = "%"
        Me.rbtnFATSNFPer.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'txtIncentiveRate
        '
        Me.txtIncentiveRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtIncentiveRate.CalculationExpression = Nothing
        Me.txtIncentiveRate.DecimalPlaces = 5
        Me.txtIncentiveRate.FieldCode = Nothing
        Me.txtIncentiveRate.FieldDesc = Nothing
        Me.txtIncentiveRate.FieldMaxLength = 0
        Me.txtIncentiveRate.FieldName = Nothing
        Me.txtIncentiveRate.isCalculatedField = False
        Me.txtIncentiveRate.IsSourceFromTable = False
        Me.txtIncentiveRate.IsSourceFromValueList = False
        Me.txtIncentiveRate.IsUnique = False
        Me.txtIncentiveRate.Location = New System.Drawing.Point(409, 52)
        Me.txtIncentiveRate.MendatroryField = True
        Me.txtIncentiveRate.MyLinkLable1 = Nothing
        Me.txtIncentiveRate.MyLinkLable2 = Nothing
        Me.txtIncentiveRate.Name = "txtIncentiveRate"
        Me.txtIncentiveRate.ReferenceFieldDesc = Nothing
        Me.txtIncentiveRate.ReferenceFieldName = Nothing
        Me.txtIncentiveRate.ReferenceTableName = Nothing
        Me.txtIncentiveRate.Size = New System.Drawing.Size(90, 20)
        Me.txtIncentiveRate.TabIndex = 4
        Me.txtIncentiveRate.Text = "0"
        Me.txtIncentiveRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtIncentiveRate.Value = 0R
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(312, 54)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel11.TabIndex = 14
        Me.MyLabel11.Text = "Incentive Rate"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(190, 54)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(19, 16)
        Me.MyLabel1.TabIndex = 15
        Me.MyLabel1.Text = "To"
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(217, 52)
        Me.txtToDate.MendatroryField = True
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.MyLabel1
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReadOnly = True
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(87, 20)
        Me.txtToDate.TabIndex = 3
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "10/06/2011"
        Me.txtToDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblPending
        '
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(631, 7)
        Me.lblPending.Margin = New System.Windows.Forms.Padding(4)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(122, 42)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 12
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(7, 54)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel3.TabIndex = 7
        Me.MyLabel3.Text = "Payment Cycle"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(94, 52)
        Me.txtFromDate.MendatroryField = True
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.MyLabel3
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(87, 20)
        Me.txtFromDate.TabIndex = 2
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "10/06/2011"
        Me.txtFromDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel34
        '
        Me.MyLabel34.FieldName = Nothing
        Me.MyLabel34.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel34.Location = New System.Drawing.Point(7, 75)
        Me.MyLabel34.Name = "MyLabel34"
        Me.MyLabel34.Size = New System.Drawing.Size(42, 18)
        Me.MyLabel34.TabIndex = 6
        Me.MyLabel34.Text = "Society"
        '
        'lblMCCDesc
        '
        Me.lblMCCDesc.AutoSize = False
        Me.lblMCCDesc.BorderVisible = True
        Me.lblMCCDesc.FieldName = Nothing
        Me.lblMCCDesc.Location = New System.Drawing.Point(311, 30)
        Me.lblMCCDesc.Name = "lblMCCDesc"
        Me.lblMCCDesc.Size = New System.Drawing.Size(188, 19)
        Me.lblMCCDesc.TabIndex = 13
        '
        'txtMCC
        '
        Me.txtMCC.CalculationExpression = Nothing
        Me.txtMCC.FieldCode = Nothing
        Me.txtMCC.FieldDesc = Nothing
        Me.txtMCC.FieldMaxLength = 0
        Me.txtMCC.FieldName = Nothing
        Me.txtMCC.isCalculatedField = False
        Me.txtMCC.IsSourceFromTable = False
        Me.txtMCC.IsSourceFromValueList = False
        Me.txtMCC.IsUnique = False
        Me.txtMCC.Location = New System.Drawing.Point(94, 30)
        Me.txtMCC.MendatroryField = True
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Nothing
        Me.txtMCC.MyLinkLable2 = Nothing
        Me.txtMCC.MyReadOnly = False
        Me.txtMCC.MyShowMasterFormButton = False
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.ReferenceFieldDesc = Nothing
        Me.txtMCC.ReferenceFieldName = Nothing
        Me.txtMCC.ReferenceTableName = Nothing
        Me.txtMCC.Size = New System.Drawing.Size(212, 19)
        Me.txtMCC.TabIndex = 1
        Me.txtMCC.Value = ""
        '
        'mtxtVLC
        '
        Me.mtxtVLC.arrDispalyMember = Nothing
        Me.mtxtVLC.arrValueMember = Nothing
        Me.mtxtVLC.Location = New System.Drawing.Point(94, 75)
        Me.mtxtVLC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mtxtVLC.MyLinkLable1 = Nothing
        Me.mtxtVLC.MyLinkLable2 = Nothing
        Me.mtxtVLC.MyNullText = "Please Select VLC"
        Me.mtxtVLC.Name = "mtxtVLC"
        Me.mtxtVLC.Size = New System.Drawing.Size(405, 19)
        Me.mtxtVLC.TabIndex = 5
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(7, 31)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel6.TabIndex = 8
        Me.MyLabel6.Text = "MCC Code"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(373, 9)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel2.TabIndex = 11
        Me.MyLabel2.Text = "Date"
        '
        'txtdate
        '
        Me.txtdate.CalculationExpression = Nothing
        Me.txtdate.CustomFormat = "dd/MM/yyyy"
        Me.txtdate.FieldCode = Nothing
        Me.txtdate.FieldDesc = Nothing
        Me.txtdate.FieldMaxLength = 0
        Me.txtdate.FieldName = Nothing
        Me.txtdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtdate.isCalculatedField = False
        Me.txtdate.IsSourceFromTable = False
        Me.txtdate.IsSourceFromValueList = False
        Me.txtdate.IsUnique = False
        Me.txtdate.Location = New System.Drawing.Point(409, 8)
        Me.txtdate.MendatroryField = True
        Me.txtdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdate.MyLinkLable1 = Me.MyLabel2
        Me.txtdate.MyLinkLable2 = Nothing
        Me.txtdate.Name = "txtdate"
        Me.txtdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdate.ReferenceFieldDesc = Nothing
        Me.txtdate.ReferenceFieldName = Nothing
        Me.txtdate.ReferenceTableName = Nothing
        Me.txtdate.Size = New System.Drawing.Size(90, 18)
        Me.txtdate.TabIndex = 0
        Me.txtdate.TabStop = False
        Me.txtdate.Text = "13/06/2011"
        Me.txtdate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPBulkProcurement.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(350, 8)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(21, 19)
        Me.btnReset.TabIndex = 10
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Location = New System.Drawing.Point(7, 8)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(32, 18)
        Me.lblCode.TabIndex = 9
        Me.lblCode.Text = "Code"
        '
        'txtDocumentNo
        '
        Me.txtDocumentNo.FieldName = Nothing
        Me.txtDocumentNo.Location = New System.Drawing.Point(94, 7)
        Me.txtDocumentNo.MendatroryField = False
        Me.txtDocumentNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocumentNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocumentNo.MyLinkLable1 = Me.lblCode
        Me.txtDocumentNo.MyLinkLable2 = Nothing
        Me.txtDocumentNo.MyMaxLength = 30
        Me.txtDocumentNo.MyReadOnly = False
        Me.txtDocumentNo.Name = "txtDocumentNo"
        Me.txtDocumentNo.Size = New System.Drawing.Size(256, 20)
        Me.txtDocumentNo.TabIndex = 16
        Me.txtDocumentNo.Value = ""
        '
        'gvItem
        '
        Me.gvItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvItem.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvItem.MasterTemplate.AllowAddNewRow = False
        Me.gvItem.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvItem.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvItem.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvItem.MyStopExport = False
        Me.gvItem.Name = "gvItem"
        Me.gvItem.ShowHeaderCellButtons = True
        Me.gvItem.Size = New System.Drawing.Size(996, 350)
        Me.gvItem.TabIndex = 0
        Me.gvItem.TabStop = False
        '
        'btnAddMissing
        '
        Me.btnAddMissing.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAddMissing.Location = New System.Drawing.Point(220, 6)
        Me.btnAddMissing.Name = "btnAddMissing"
        Me.btnAddMissing.Size = New System.Drawing.Size(79, 20)
        Me.btnAddMissing.TabIndex = 42
        Me.btnAddMissing.Text = "Add Missing"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(149, 6)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 20)
        Me.btnPost.TabIndex = 4
        Me.btnPost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(924, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(69, 20)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(78, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(69, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(7, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(69, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExport, Me.rmimport, Me.RadMenuItem4, Me.RadMenuItem5, Me.RadMenuItem1, Me.RadMenuItem2})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'rmExport
        '
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export Grid"
        '
        'rmimport
        '
        Me.rmimport.Name = "rmimport"
        Me.rmimport.Text = "Import Grid"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Export"
        '
        'RadMenuItem5
        '
        Me.RadMenuItem5.Name = "RadMenuItem5"
        Me.RadMenuItem5.Text = "Import"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Delete Layout"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(996, 20)
        Me.RadMenu1.TabIndex = 12
        '
        'frmMPIncentiveEntrty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(996, 509)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmMPIncentiveEntrty"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "MP Incetive Entry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.txtRahatKampekatFeedRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRahatKampekatFeedRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSailejRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSailejRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMineralMixture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMineralMixture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPashuAahar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPashuAahar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnFATSNFNA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnFATSNFKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnFATSNFPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIncentiveRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddMissing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtdate As common.Controls.MyDateTimePicker
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents txtDocumentNo As common.UserControls.txtNavigator
    Friend WithEvents gvItem As common.UserControls.MyRadGridView
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblMCCDesc As common.Controls.MyLabel
    Friend WithEvents txtMCC As common.UserControls.txtFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents rmimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel34 As common.Controls.MyLabel
    Friend WithEvents mtxtVLC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtIncentiveRate As common.MyNumBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents btnPost As RadButton
    Friend WithEvents RadMenu1 As RadMenu
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rbtnFATSNFKG As common.Controls.MyRadioButton
    Friend WithEvents rbtnFATSNFPer As common.Controls.MyRadioButton
    Friend WithEvents rbtnFATSNFNA As common.Controls.MyRadioButton
    Friend WithEvents txtMineralMixture As common.MyNumBox
    Friend WithEvents lblMineralMixture As common.Controls.MyLabel
    Friend WithEvents txtPashuAahar As common.MyNumBox
    Friend WithEvents lblPashuAahar As common.Controls.MyLabel
    Friend WithEvents txtRahatKampekatFeedRate As common.MyNumBox
    Friend WithEvents lblRahatKampekatFeedRate As common.Controls.MyLabel
    Friend WithEvents txtSailejRate As common.MyNumBox
    Friend WithEvents lblSailejRate As common.Controls.MyLabel
    Friend WithEvents RadMenuItem1 As RadMenuItem
    Friend WithEvents RadMenuItem2 As RadMenuItem
    Friend WithEvents RadMenuItem4 As RadMenuItem
    Friend WithEvents RadMenuItem5 As RadMenuItem
    Friend WithEvents btnAddMissing As RadButton
End Class

