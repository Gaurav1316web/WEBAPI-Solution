<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOpenMCCShift
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
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.txtCLR = New common.MyNumBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.TxtManualSNF_Per = New common.MyNumBox()
        Me.LblManualSNF_Per = New common.Controls.MyLabel()
        Me.split_systemstock = New System.Windows.Forms.SplitContainer()
        Me.txtremarks = New Telerik.WinControls.UI.RadTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtsystemsnf = New common.MyNumBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtsystemstock = New common.MyNumBox()
        Me.txtsystemfat = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.chkAllowManualGeteEntryWeighment = New common.Controls.MyCheckBox()
        Me.TxtManualStock = New common.MyNumBox()
        Me.LblManualStock = New common.Controls.MyLabel()
        Me.lbldesid = New common.Controls.MyLabel()
        Me.LblManualFAT = New common.Controls.MyLabel()
        Me.ChkManualEntry = New common.Controls.MyCheckBox()
        Me.GrpRegular = New Telerik.WinControls.UI.RadGroupBox()
        Me.FndIrregularMcc = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.LblIrregularMccName = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.lblPPDate = New common.Controls.MyLabel()
        Me.TxtManualFAT = New common.MyNumBox()
        Me.dtpShiftDate = New common.Controls.MyDateTimePicker()
        Me.LblManualSNF = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.Chkregular = New common.Controls.MyCheckBox()
        Me.ChkManualWeighment = New common.Controls.MyCheckBox()
        Me.LblManualFAT_Per = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.TxtManualFat_Per = New common.MyNumBox()
        Me.TxtManualSNF = New common.MyNumBox()
        Me.TxtBookSNF_per = New common.MyNumBox()
        Me.LblBookSNF_Per = New common.Controls.MyLabel()
        Me.lblmccname = New common.Controls.MyLabel()
        Me.txtmccode = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.cmbShift = New common.Controls.MyComboBox()
        Me.chkHoliday = New common.Controls.MyCheckBox()
        Me.LblBookStock = New common.Controls.MyLabel()
        Me.TxtActualStock = New common.MyNumBox()
        Me.TxtBookFat_Per = New common.MyNumBox()
        Me.LblBookFat_Per = New common.Controls.MyLabel()
        Me.LblBookFAT = New common.Controls.MyLabel()
        Me.TxtActualFat = New common.MyNumBox()
        Me.LblBookSNF = New common.Controls.MyLabel()
        Me.TxtActualSNF = New common.MyNumBox()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenu = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.txtCLR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtManualSNF_Per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblManualSNF_Per, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.split_systemstock.Panel2.SuspendLayout()
        Me.split_systemstock.SuspendLayout()
        CType(Me.txtremarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtsystemsnf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtsystemstock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtsystemfat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllowManualGeteEntryWeighment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtManualStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblManualStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldesid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblManualFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkManualEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpRegular, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpRegular.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblIrregularMccName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPPDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtManualFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpShiftDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblManualSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chkregular, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkManualWeighment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblManualFAT_Per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtManualFat_Per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtManualSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtBookSNF_per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblBookSNF_Per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblmccname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkHoliday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblBookStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtActualStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtBookFat_Per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblBookFat_Per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblBookFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtActualFat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblBookSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtActualSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.pvpCustomFields.SuspendLayout()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(890, 492)
        Me.SplitContainer1.SplitterDistance = 458
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Location = New System.Drawing.Point(1, 21)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(893, 443)
        Me.RadPageView1.TabIndex = 1045
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer3)
        Me.RadPageViewPage1.Controls.Add(Me.split_systemstock)
        Me.RadPageViewPage1.Controls.Add(Me.chkAllowManualGeteEntryWeighment)
        Me.RadPageViewPage1.Controls.Add(Me.TxtManualStock)
        Me.RadPageViewPage1.Controls.Add(Me.LblManualStock)
        Me.RadPageViewPage1.Controls.Add(Me.lbldesid)
        Me.RadPageViewPage1.Controls.Add(Me.LblManualFAT)
        Me.RadPageViewPage1.Controls.Add(Me.ChkManualEntry)
        Me.RadPageViewPage1.Controls.Add(Me.GrpRegular)
        Me.RadPageViewPage1.Controls.Add(Me.lblPPDate)
        Me.RadPageViewPage1.Controls.Add(Me.TxtManualFAT)
        Me.RadPageViewPage1.Controls.Add(Me.dtpShiftDate)
        Me.RadPageViewPage1.Controls.Add(Me.LblManualSNF)
        Me.RadPageViewPage1.Controls.Add(Me.btnnew)
        Me.RadPageViewPage1.Controls.Add(Me.Chkregular)
        Me.RadPageViewPage1.Controls.Add(Me.ChkManualWeighment)
        Me.RadPageViewPage1.Controls.Add(Me.LblManualFAT_Per)
        Me.RadPageViewPage1.Controls.Add(Me.txtCode)
        Me.RadPageViewPage1.Controls.Add(Me.TxtManualFat_Per)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.TxtManualSNF)
        Me.RadPageViewPage1.Controls.Add(Me.TxtBookSNF_per)
        Me.RadPageViewPage1.Controls.Add(Me.LblManualSNF_Per)
        Me.RadPageViewPage1.Controls.Add(Me.lblmccname)
        Me.RadPageViewPage1.Controls.Add(Me.txtmccode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.cmbShift)
        Me.RadPageViewPage1.Controls.Add(Me.chkHoliday)
        Me.RadPageViewPage1.Controls.Add(Me.LblBookSNF_Per)
        Me.RadPageViewPage1.Controls.Add(Me.LblBookStock)
        Me.RadPageViewPage1.Controls.Add(Me.TxtActualStock)
        Me.RadPageViewPage1.Controls.Add(Me.TxtBookFat_Per)
        Me.RadPageViewPage1.Controls.Add(Me.LblBookFAT)
        Me.RadPageViewPage1.Controls.Add(Me.LblBookFat_Per)
        Me.RadPageViewPage1.Controls.Add(Me.TxtActualFat)
        Me.RadPageViewPage1.Controls.Add(Me.LblBookSNF)
        Me.RadPageViewPage1.Controls.Add(Me.TxtActualSNF)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(872, 395)
        Me.RadPageViewPage1.Text = "Detail"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Location = New System.Drawing.Point(131, 120)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtCLR)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel13)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.TxtManualSNF_Per)
        Me.SplitContainer3.Size = New System.Drawing.Size(154, 20)
        Me.SplitContainer3.SplitterDistance = 72
        Me.SplitContainer3.TabIndex = 1054
        '
        'txtCLR
        '
        Me.txtCLR.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCLR.CalculationExpression = Nothing
        Me.txtCLR.DecimalPlaces = 1
        Me.txtCLR.FieldCode = Nothing
        Me.txtCLR.FieldDesc = Nothing
        Me.txtCLR.FieldMaxLength = 0
        Me.txtCLR.FieldName = Nothing
        Me.txtCLR.isCalculatedField = False
        Me.txtCLR.IsSourceFromTable = False
        Me.txtCLR.IsSourceFromValueList = False
        Me.txtCLR.IsUnique = False
        Me.txtCLR.Location = New System.Drawing.Point(33, 0)
        Me.txtCLR.MendatroryField = True
        Me.txtCLR.MyLinkLable1 = Me.MyLabel13
        Me.txtCLR.MyLinkLable2 = Nothing
        Me.txtCLR.Name = "txtCLR"
        Me.txtCLR.ReferenceFieldDesc = Nothing
        Me.txtCLR.ReferenceFieldName = Nothing
        Me.txtCLR.ReferenceTableName = Nothing
        Me.txtCLR.Size = New System.Drawing.Size(36, 20)
        Me.txtCLR.TabIndex = 19
        Me.txtCLR.Text = "0"
        Me.txtCLR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCLR.Value = 0.0R
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel13.Location = New System.Drawing.Point(3, 2)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(29, 16)
        Me.MyLabel13.TabIndex = 18
        Me.MyLabel13.Text = "CLR"
        '
        'TxtManualSNF_Per
        '
        Me.TxtManualSNF_Per.BackColor = System.Drawing.Color.White
        Me.TxtManualSNF_Per.CalculationExpression = Nothing
        Me.TxtManualSNF_Per.DecimalPlaces = 3
        Me.TxtManualSNF_Per.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtManualSNF_Per.FieldCode = Nothing
        Me.TxtManualSNF_Per.FieldDesc = Nothing
        Me.TxtManualSNF_Per.FieldMaxLength = 0
        Me.TxtManualSNF_Per.FieldName = Nothing
        Me.TxtManualSNF_Per.isCalculatedField = False
        Me.TxtManualSNF_Per.IsSourceFromTable = False
        Me.TxtManualSNF_Per.IsSourceFromValueList = False
        Me.TxtManualSNF_Per.IsUnique = False
        Me.TxtManualSNF_Per.Location = New System.Drawing.Point(0, 0)
        Me.TxtManualSNF_Per.MendatroryField = False
        Me.TxtManualSNF_Per.MyLinkLable1 = Me.LblManualSNF_Per
        Me.TxtManualSNF_Per.MyLinkLable2 = Nothing
        Me.TxtManualSNF_Per.Name = "TxtManualSNF_Per"
        Me.TxtManualSNF_Per.ReferenceFieldDesc = Nothing
        Me.TxtManualSNF_Per.ReferenceFieldName = Nothing
        Me.TxtManualSNF_Per.ReferenceTableName = Nothing
        Me.TxtManualSNF_Per.Size = New System.Drawing.Size(78, 20)
        Me.TxtManualSNF_Per.TabIndex = 1039
        Me.TxtManualSNF_Per.Text = "0"
        Me.TxtManualSNF_Per.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtManualSNF_Per.Value = 0.0R
        '
        'LblManualSNF_Per
        '
        Me.LblManualSNF_Per.FieldName = Nothing
        Me.LblManualSNF_Per.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualSNF_Per.Location = New System.Drawing.Point(19, 123)
        Me.LblManualSNF_Per.Name = "LblManualSNF_Per"
        Me.LblManualSNF_Per.Size = New System.Drawing.Size(92, 16)
        Me.LblManualSNF_Per.TabIndex = 1040
        Me.LblManualSNF_Per.Text = "Opening SNF(%)"
        '
        'split_systemstock
        '
        Me.split_systemstock.IsSplitterFixed = True
        Me.split_systemstock.Location = New System.Drawing.Point(1, 143)
        Me.split_systemstock.Name = "split_systemstock"
        Me.split_systemstock.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'split_systemstock.Panel1
        '
        Me.split_systemstock.Panel1.Padding = New System.Windows.Forms.Padding(2)
        Me.split_systemstock.Panel1Collapsed = True
        '
        'split_systemstock.Panel2
        '
        Me.split_systemstock.Panel2.Controls.Add(Me.txtremarks)
        Me.split_systemstock.Panel2.Controls.Add(Me.MyLabel1)
        Me.split_systemstock.Panel2.Controls.Add(Me.MyLabel6)
        Me.split_systemstock.Panel2.Controls.Add(Me.txtsystemsnf)
        Me.split_systemstock.Panel2.Controls.Add(Me.txtsystemstock)
        Me.split_systemstock.Panel2.Controls.Add(Me.MyLabel5)
        Me.split_systemstock.Panel2.Controls.Add(Me.txtsystemfat)
        Me.split_systemstock.Panel2.Controls.Add(Me.MyLabel2)
        Me.split_systemstock.Panel2.Padding = New System.Windows.Forms.Padding(2)
        Me.split_systemstock.Size = New System.Drawing.Size(575, 100)
        Me.split_systemstock.SplitterDistance = 25
        Me.split_systemstock.TabIndex = 1053
        '
        'txtremarks
        '
        Me.txtremarks.Enabled = False
        Me.txtremarks.Location = New System.Drawing.Point(131, 76)
        Me.txtremarks.MaxLength = 1000
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.Size = New System.Drawing.Size(427, 20)
        Me.txtremarks.TabIndex = 1060
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(19, 5)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(99, 16)
        Me.MyLabel1.TabIndex = 1053
        Me.MyLabel1.Text = "System Stock(KG)"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(19, 76)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel6.TabIndex = 1059
        Me.MyLabel6.Text = "Remarks"
        '
        'txtsystemsnf
        '
        Me.txtsystemsnf.BackColor = System.Drawing.Color.White
        Me.txtsystemsnf.CalculationExpression = Nothing
        Me.txtsystemsnf.DecimalPlaces = 3
        Me.txtsystemsnf.Enabled = False
        Me.txtsystemsnf.FieldCode = Nothing
        Me.txtsystemsnf.FieldDesc = Nothing
        Me.txtsystemsnf.FieldMaxLength = 0
        Me.txtsystemsnf.FieldName = Nothing
        Me.txtsystemsnf.isCalculatedField = False
        Me.txtsystemsnf.IsSourceFromTable = False
        Me.txtsystemsnf.IsSourceFromValueList = False
        Me.txtsystemsnf.IsUnique = False
        Me.txtsystemsnf.Location = New System.Drawing.Point(131, 52)
        Me.txtsystemsnf.MendatroryField = False
        Me.txtsystemsnf.MyLinkLable1 = Me.MyLabel5
        Me.txtsystemsnf.MyLinkLable2 = Nothing
        Me.txtsystemsnf.Name = "txtsystemsnf"
        Me.txtsystemsnf.ReferenceFieldDesc = Nothing
        Me.txtsystemsnf.ReferenceFieldName = Nothing
        Me.txtsystemsnf.ReferenceTableName = Nothing
        Me.txtsystemsnf.Size = New System.Drawing.Size(154, 20)
        Me.txtsystemsnf.TabIndex = 1056
        Me.txtsystemsnf.Text = "0"
        Me.txtsystemsnf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtsystemsnf.Value = 0.0R
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(19, 54)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel5.TabIndex = 1057
        Me.MyLabel5.Text = "System SNF(%)"
        '
        'txtsystemstock
        '
        Me.txtsystemstock.BackColor = System.Drawing.Color.White
        Me.txtsystemstock.CalculationExpression = Nothing
        Me.txtsystemstock.DecimalPlaces = 3
        Me.txtsystemstock.Enabled = False
        Me.txtsystemstock.FieldCode = Nothing
        Me.txtsystemstock.FieldDesc = Nothing
        Me.txtsystemstock.FieldMaxLength = 0
        Me.txtsystemstock.FieldName = Nothing
        Me.txtsystemstock.isCalculatedField = False
        Me.txtsystemstock.IsSourceFromTable = False
        Me.txtsystemstock.IsSourceFromValueList = False
        Me.txtsystemstock.IsUnique = False
        Me.txtsystemstock.Location = New System.Drawing.Point(131, 3)
        Me.txtsystemstock.MendatroryField = False
        Me.txtsystemstock.MyLinkLable1 = Me.MyLabel1
        Me.txtsystemstock.MyLinkLable2 = Nothing
        Me.txtsystemstock.Name = "txtsystemstock"
        Me.txtsystemstock.ReferenceFieldDesc = Nothing
        Me.txtsystemstock.ReferenceFieldName = Nothing
        Me.txtsystemstock.ReferenceTableName = Nothing
        Me.txtsystemstock.Size = New System.Drawing.Size(154, 20)
        Me.txtsystemstock.TabIndex = 1052
        Me.txtsystemstock.Text = "0"
        Me.txtsystemstock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtsystemstock.Value = 0.0R
        '
        'txtsystemfat
        '
        Me.txtsystemfat.BackColor = System.Drawing.Color.White
        Me.txtsystemfat.CalculationExpression = Nothing
        Me.txtsystemfat.DecimalPlaces = 3
        Me.txtsystemfat.Enabled = False
        Me.txtsystemfat.FieldCode = Nothing
        Me.txtsystemfat.FieldDesc = Nothing
        Me.txtsystemfat.FieldMaxLength = 0
        Me.txtsystemfat.FieldName = Nothing
        Me.txtsystemfat.isCalculatedField = False
        Me.txtsystemfat.IsSourceFromTable = False
        Me.txtsystemfat.IsSourceFromValueList = False
        Me.txtsystemfat.IsUnique = False
        Me.txtsystemfat.Location = New System.Drawing.Point(131, 27)
        Me.txtsystemfat.MendatroryField = False
        Me.txtsystemfat.MyLinkLable1 = Me.MyLabel2
        Me.txtsystemfat.MyLinkLable2 = Nothing
        Me.txtsystemfat.Name = "txtsystemfat"
        Me.txtsystemfat.ReferenceFieldDesc = Nothing
        Me.txtsystemfat.ReferenceFieldName = Nothing
        Me.txtsystemfat.ReferenceTableName = Nothing
        Me.txtsystemfat.Size = New System.Drawing.Size(154, 20)
        Me.txtsystemfat.TabIndex = 1054
        Me.txtsystemfat.Text = "0"
        Me.txtsystemfat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtsystemfat.Value = 0.0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(19, 29)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel2.TabIndex = 1055
        Me.MyLabel2.Text = "System FAT(%)"
        '
        'chkAllowManualGeteEntryWeighment
        '
        Me.chkAllowManualGeteEntryWeighment.Location = New System.Drawing.Point(483, 73)
        Me.chkAllowManualGeteEntryWeighment.MyLinkLable1 = Nothing
        Me.chkAllowManualGeteEntryWeighment.MyLinkLable2 = Nothing
        Me.chkAllowManualGeteEntryWeighment.Name = "chkAllowManualGeteEntryWeighment"
        Me.chkAllowManualGeteEntryWeighment.Size = New System.Drawing.Size(204, 18)
        Me.chkAllowManualGeteEntryWeighment.TabIndex = 1045
        Me.chkAllowManualGeteEntryWeighment.Tag1 = Nothing
        Me.chkAllowManualGeteEntryWeighment.Text = "Allow Manual Gate Entry Weighment"
        Me.chkAllowManualGeteEntryWeighment.Visible = False
        '
        'TxtManualStock
        '
        Me.TxtManualStock.BackColor = System.Drawing.Color.White
        Me.TxtManualStock.CalculationExpression = Nothing
        Me.TxtManualStock.DecimalPlaces = 3
        Me.TxtManualStock.FieldCode = Nothing
        Me.TxtManualStock.FieldDesc = Nothing
        Me.TxtManualStock.FieldMaxLength = 0
        Me.TxtManualStock.FieldName = Nothing
        Me.TxtManualStock.isCalculatedField = False
        Me.TxtManualStock.IsSourceFromTable = False
        Me.TxtManualStock.IsSourceFromValueList = False
        Me.TxtManualStock.IsUnique = False
        Me.TxtManualStock.Location = New System.Drawing.Point(131, 72)
        Me.TxtManualStock.MendatroryField = False
        Me.TxtManualStock.MyLinkLable1 = Me.LblManualStock
        Me.TxtManualStock.MyLinkLable2 = Nothing
        Me.TxtManualStock.Name = "TxtManualStock"
        Me.TxtManualStock.ReferenceFieldDesc = Nothing
        Me.TxtManualStock.ReferenceFieldName = Nothing
        Me.TxtManualStock.ReferenceTableName = Nothing
        Me.TxtManualStock.Size = New System.Drawing.Size(154, 20)
        Me.TxtManualStock.TabIndex = 1031
        Me.TxtManualStock.Text = "0"
        Me.TxtManualStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtManualStock.Value = 0.0R
        '
        'LblManualStock
        '
        Me.LblManualStock.FieldName = Nothing
        Me.LblManualStock.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualStock.Location = New System.Drawing.Point(19, 74)
        Me.LblManualStock.Name = "LblManualStock"
        Me.LblManualStock.Size = New System.Drawing.Size(104, 16)
        Me.LblManualStock.TabIndex = 1032
        Me.LblManualStock.Text = "Opening Stock(KG)"
        '
        'lbldesid
        '
        Me.lbldesid.FieldName = Nothing
        Me.lbldesid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldesid.Location = New System.Drawing.Point(19, 3)
        Me.lbldesid.Name = "lbldesid"
        Me.lbldesid.Size = New System.Drawing.Size(79, 16)
        Me.lbldesid.TabIndex = 18
        Me.lbldesid.Text = "MCC Shift ID :"
        '
        'LblManualFAT
        '
        Me.LblManualFAT.FieldName = Nothing
        Me.LblManualFAT.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualFAT.Location = New System.Drawing.Point(295, 98)
        Me.LblManualFAT.Name = "LblManualFAT"
        Me.LblManualFAT.Size = New System.Drawing.Size(97, 16)
        Me.LblManualFAT.TabIndex = 1034
        Me.LblManualFAT.Text = "Opening FAT(KG)"
        '
        'ChkManualEntry
        '
        Me.ChkManualEntry.Location = New System.Drawing.Point(131, 73)
        Me.ChkManualEntry.MyLinkLable1 = Nothing
        Me.ChkManualEntry.MyLinkLable2 = Nothing
        Me.ChkManualEntry.Name = "ChkManualEntry"
        Me.ChkManualEntry.Size = New System.Drawing.Size(163, 18)
        Me.ChkManualEntry.TabIndex = 1041
        Me.ChkManualEntry.Tag1 = Nothing
        Me.ChkManualEntry.Text = "Allow Manual Entry (Sample)"
        Me.ChkManualEntry.Visible = False
        '
        'GrpRegular
        '
        Me.GrpRegular.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.GrpRegular.Controls.Add(Me.FndIrregularMcc)
        Me.GrpRegular.Controls.Add(Me.LblIrregularMccName)
        Me.GrpRegular.Controls.Add(Me.MyLabel9)
        Me.GrpRegular.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpRegular.HeaderText = "Receiving Mcc Details"
        Me.GrpRegular.Location = New System.Drawing.Point(20, 272)
        Me.GrpRegular.Name = "GrpRegular"
        Me.GrpRegular.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.GrpRegular.Size = New System.Drawing.Size(538, 41)
        Me.GrpRegular.TabIndex = 1044
        Me.GrpRegular.Text = "Receiving Mcc Details"
        Me.GrpRegular.Visible = False
        '
        'FndIrregularMcc
        '
        Me.FndIrregularMcc.CalculationExpression = Nothing
        Me.FndIrregularMcc.FieldCode = Nothing
        Me.FndIrregularMcc.FieldDesc = Nothing
        Me.FndIrregularMcc.FieldMaxLength = 0
        Me.FndIrregularMcc.FieldName = Nothing
        Me.FndIrregularMcc.isCalculatedField = False
        Me.FndIrregularMcc.IsSourceFromTable = False
        Me.FndIrregularMcc.IsSourceFromValueList = False
        Me.FndIrregularMcc.IsUnique = False
        Me.FndIrregularMcc.Location = New System.Drawing.Point(106, 16)
        Me.FndIrregularMcc.MendatroryField = True
        Me.FndIrregularMcc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndIrregularMcc.MyLinkLable1 = Me.MyLabel4
        Me.FndIrregularMcc.MyLinkLable2 = Me.LblIrregularMccName
        Me.FndIrregularMcc.MyReadOnly = False
        Me.FndIrregularMcc.MyShowMasterFormButton = False
        Me.FndIrregularMcc.Name = "FndIrregularMcc"
        Me.FndIrregularMcc.ReferenceFieldDesc = Nothing
        Me.FndIrregularMcc.ReferenceFieldName = Nothing
        Me.FndIrregularMcc.ReferenceTableName = Nothing
        Me.FndIrregularMcc.Size = New System.Drawing.Size(154, 18)
        Me.FndIrregularMcc.TabIndex = 1025
        Me.FndIrregularMcc.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(19, 27)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel4.TabIndex = 51
        Me.MyLabel4.Text = "MCC Code :"
        '
        'LblIrregularMccName
        '
        Me.LblIrregularMccName.AutoSize = False
        Me.LblIrregularMccName.BorderVisible = True
        Me.LblIrregularMccName.FieldName = Nothing
        Me.LblIrregularMccName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIrregularMccName.Location = New System.Drawing.Point(266, 16)
        Me.LblIrregularMccName.Name = "LblIrregularMccName"
        Me.LblIrregularMccName.Size = New System.Drawing.Size(259, 18)
        Me.LblIrregularMccName.TabIndex = 1026
        Me.LblIrregularMccName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblIrregularMccName.TextWrap = False
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(28, 17)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel9.TabIndex = 1024
        Me.MyLabel9.Text = "Mcc Name"
        '
        'lblPPDate
        '
        Me.lblPPDate.FieldName = Nothing
        Me.lblPPDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblPPDate.Location = New System.Drawing.Point(292, 50)
        Me.lblPPDate.Name = "lblPPDate"
        Me.lblPPDate.Size = New System.Drawing.Size(56, 16)
        Me.lblPPDate.TabIndex = 10
        Me.lblPPDate.Text = "Shift Date"
        '
        'TxtManualFAT
        '
        Me.TxtManualFAT.BackColor = System.Drawing.Color.White
        Me.TxtManualFAT.CalculationExpression = Nothing
        Me.TxtManualFAT.DecimalPlaces = 3
        Me.TxtManualFAT.Enabled = False
        Me.TxtManualFAT.FieldCode = Nothing
        Me.TxtManualFAT.FieldDesc = Nothing
        Me.TxtManualFAT.FieldMaxLength = 0
        Me.TxtManualFAT.FieldName = Nothing
        Me.TxtManualFAT.isCalculatedField = False
        Me.TxtManualFAT.IsSourceFromTable = False
        Me.TxtManualFAT.IsSourceFromValueList = False
        Me.TxtManualFAT.IsUnique = False
        Me.TxtManualFAT.Location = New System.Drawing.Point(401, 96)
        Me.TxtManualFAT.MendatroryField = False
        Me.TxtManualFAT.MyLinkLable1 = Me.LblManualFAT
        Me.TxtManualFAT.MyLinkLable2 = Nothing
        Me.TxtManualFAT.Name = "TxtManualFAT"
        Me.TxtManualFAT.ReferenceFieldDesc = Nothing
        Me.TxtManualFAT.ReferenceFieldName = Nothing
        Me.TxtManualFAT.ReferenceTableName = Nothing
        Me.TxtManualFAT.Size = New System.Drawing.Size(157, 20)
        Me.TxtManualFAT.TabIndex = 1033
        Me.TxtManualFAT.Text = "0"
        Me.TxtManualFAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtManualFAT.Value = 0.0R
        '
        'dtpShiftDate
        '
        Me.dtpShiftDate.CalculationExpression = Nothing
        Me.dtpShiftDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpShiftDate.FieldCode = Nothing
        Me.dtpShiftDate.FieldDesc = Nothing
        Me.dtpShiftDate.FieldMaxLength = 0
        Me.dtpShiftDate.FieldName = Nothing
        Me.dtpShiftDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpShiftDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpShiftDate.isCalculatedField = False
        Me.dtpShiftDate.IsSourceFromTable = False
        Me.dtpShiftDate.IsSourceFromValueList = False
        Me.dtpShiftDate.IsUnique = False
        Me.dtpShiftDate.Location = New System.Drawing.Point(354, 49)
        Me.dtpShiftDate.MendatroryField = True
        Me.dtpShiftDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpShiftDate.MyLinkLable1 = Me.lblPPDate
        Me.dtpShiftDate.MyLinkLable2 = Nothing
        Me.dtpShiftDate.Name = "dtpShiftDate"
        Me.dtpShiftDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpShiftDate.ReadOnly = True
        Me.dtpShiftDate.ReferenceFieldDesc = Nothing
        Me.dtpShiftDate.ReferenceFieldName = Nothing
        Me.dtpShiftDate.ReferenceTableName = Nothing
        Me.dtpShiftDate.Size = New System.Drawing.Size(192, 18)
        Me.dtpShiftDate.TabIndex = 4
        Me.dtpShiftDate.TabStop = False
        Me.dtpShiftDate.Text = "03/05/2011 12:00 AM"
        Me.dtpShiftDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'LblManualSNF
        '
        Me.LblManualSNF.FieldName = Nothing
        Me.LblManualSNF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualSNF.Location = New System.Drawing.Point(295, 123)
        Me.LblManualSNF.Name = "LblManualSNF"
        Me.LblManualSNF.Size = New System.Drawing.Size(99, 16)
        Me.LblManualSNF.TabIndex = 1036
        Me.LblManualSNF.Text = "Opening SNF(KG)"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(402, 1)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(20, 21)
        Me.btnnew.TabIndex = 1
        '
        'Chkregular
        '
        Me.Chkregular.Location = New System.Drawing.Point(19, 249)
        Me.Chkregular.MyLinkLable1 = Nothing
        Me.Chkregular.MyLinkLable2 = Nothing
        Me.Chkregular.Name = "Chkregular"
        Me.Chkregular.Size = New System.Drawing.Size(58, 18)
        Me.Chkregular.TabIndex = 1043
        Me.Chkregular.Tag1 = Nothing
        Me.Chkregular.Text = "Regular"
        '
        'ChkManualWeighment
        '
        Me.ChkManualWeighment.Location = New System.Drawing.Point(295, 73)
        Me.ChkManualWeighment.MyLinkLable1 = Nothing
        Me.ChkManualWeighment.MyLinkLable2 = Nothing
        Me.ChkManualWeighment.Name = "ChkManualWeighment"
        Me.ChkManualWeighment.Size = New System.Drawing.Size(182, 18)
        Me.ChkManualWeighment.TabIndex = 1042
        Me.ChkManualWeighment.Tag1 = Nothing
        Me.ChkManualWeighment.Text = "Allow Manual Entry(Weighment)"
        Me.ChkManualWeighment.Visible = False
        '
        'LblManualFAT_Per
        '
        Me.LblManualFAT_Per.FieldName = Nothing
        Me.LblManualFAT_Per.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualFAT_Per.Location = New System.Drawing.Point(19, 98)
        Me.LblManualFAT_Per.Name = "LblManualFAT_Per"
        Me.LblManualFAT_Per.Size = New System.Drawing.Size(91, 16)
        Me.LblManualFAT_Per.TabIndex = 1038
        Me.LblManualFAT_Per.Text = "Opening FAT(%)"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(131, 1)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Nothing
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(271, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'TxtManualFat_Per
        '
        Me.TxtManualFat_Per.BackColor = System.Drawing.Color.White
        Me.TxtManualFat_Per.CalculationExpression = Nothing
        Me.TxtManualFat_Per.DecimalPlaces = 3
        Me.TxtManualFat_Per.FieldCode = Nothing
        Me.TxtManualFat_Per.FieldDesc = Nothing
        Me.TxtManualFat_Per.FieldMaxLength = 0
        Me.TxtManualFat_Per.FieldName = Nothing
        Me.TxtManualFat_Per.isCalculatedField = False
        Me.TxtManualFat_Per.IsSourceFromTable = False
        Me.TxtManualFat_Per.IsSourceFromValueList = False
        Me.TxtManualFat_Per.IsUnique = False
        Me.TxtManualFat_Per.Location = New System.Drawing.Point(131, 96)
        Me.TxtManualFat_Per.MendatroryField = False
        Me.TxtManualFat_Per.MyLinkLable1 = Me.LblManualFAT_Per
        Me.TxtManualFat_Per.MyLinkLable2 = Nothing
        Me.TxtManualFat_Per.Name = "TxtManualFat_Per"
        Me.TxtManualFat_Per.ReferenceFieldDesc = Nothing
        Me.TxtManualFat_Per.ReferenceFieldName = Nothing
        Me.TxtManualFat_Per.ReferenceTableName = Nothing
        Me.TxtManualFat_Per.Size = New System.Drawing.Size(154, 20)
        Me.TxtManualFat_Per.TabIndex = 1037
        Me.TxtManualFat_Per.Text = "0"
        Me.TxtManualFat_Per.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtManualFat_Per.Value = 0.0R
        '
        'TxtManualSNF
        '
        Me.TxtManualSNF.BackColor = System.Drawing.Color.White
        Me.TxtManualSNF.CalculationExpression = Nothing
        Me.TxtManualSNF.DecimalPlaces = 3
        Me.TxtManualSNF.Enabled = False
        Me.TxtManualSNF.FieldCode = Nothing
        Me.TxtManualSNF.FieldDesc = Nothing
        Me.TxtManualSNF.FieldMaxLength = 0
        Me.TxtManualSNF.FieldName = Nothing
        Me.TxtManualSNF.isCalculatedField = False
        Me.TxtManualSNF.IsSourceFromTable = False
        Me.TxtManualSNF.IsSourceFromValueList = False
        Me.TxtManualSNF.IsUnique = False
        Me.TxtManualSNF.Location = New System.Drawing.Point(401, 121)
        Me.TxtManualSNF.MendatroryField = False
        Me.TxtManualSNF.MyLinkLable1 = Me.LblManualSNF
        Me.TxtManualSNF.MyLinkLable2 = Nothing
        Me.TxtManualSNF.Name = "TxtManualSNF"
        Me.TxtManualSNF.ReferenceFieldDesc = Nothing
        Me.TxtManualSNF.ReferenceFieldName = Nothing
        Me.TxtManualSNF.ReferenceTableName = Nothing
        Me.TxtManualSNF.Size = New System.Drawing.Size(157, 20)
        Me.TxtManualSNF.TabIndex = 1035
        Me.TxtManualSNF.Text = "0"
        Me.TxtManualSNF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtManualSNF.Value = 0.0R
        '
        'TxtBookSNF_per
        '
        Me.TxtBookSNF_per.BackColor = System.Drawing.Color.White
        Me.TxtBookSNF_per.CalculationExpression = Nothing
        Me.TxtBookSNF_per.DecimalPlaces = 3
        Me.TxtBookSNF_per.FieldCode = Nothing
        Me.TxtBookSNF_per.FieldDesc = Nothing
        Me.TxtBookSNF_per.FieldMaxLength = 0
        Me.TxtBookSNF_per.FieldName = Nothing
        Me.TxtBookSNF_per.isCalculatedField = False
        Me.TxtBookSNF_per.IsSourceFromTable = False
        Me.TxtBookSNF_per.IsSourceFromValueList = False
        Me.TxtBookSNF_per.IsUnique = False
        Me.TxtBookSNF_per.Location = New System.Drawing.Point(136, 374)
        Me.TxtBookSNF_per.MendatroryField = False
        Me.TxtBookSNF_per.MyLinkLable1 = Me.LblBookSNF_Per
        Me.TxtBookSNF_per.MyLinkLable2 = Nothing
        Me.TxtBookSNF_per.Name = "TxtBookSNF_per"
        Me.TxtBookSNF_per.ReadOnly = True
        Me.TxtBookSNF_per.ReferenceFieldDesc = Nothing
        Me.TxtBookSNF_per.ReferenceFieldName = Nothing
        Me.TxtBookSNF_per.ReferenceTableName = Nothing
        Me.TxtBookSNF_per.Size = New System.Drawing.Size(154, 20)
        Me.TxtBookSNF_per.TabIndex = 1033
        Me.TxtBookSNF_per.Text = "0"
        Me.TxtBookSNF_per.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtBookSNF_per.Value = 0.0R
        Me.TxtBookSNF_per.Visible = False
        '
        'LblBookSNF_Per
        '
        Me.LblBookSNF_Per.FieldName = Nothing
        Me.LblBookSNF_Per.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBookSNF_Per.Location = New System.Drawing.Point(24, 376)
        Me.LblBookSNF_Per.Name = "LblBookSNF_Per"
        Me.LblBookSNF_Per.Size = New System.Drawing.Size(75, 16)
        Me.LblBookSNF_Per.TabIndex = 1034
        Me.LblBookSNF_Per.Text = "Book SNF(%)"
        Me.LblBookSNF_Per.Visible = False
        '
        'lblmccname
        '
        Me.lblmccname.AutoSize = False
        Me.lblmccname.BorderVisible = True
        Me.lblmccname.FieldName = Nothing
        Me.lblmccname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmccname.Location = New System.Drawing.Point(291, 26)
        Me.lblmccname.Name = "lblmccname"
        Me.lblmccname.Size = New System.Drawing.Size(166, 18)
        Me.lblmccname.TabIndex = 52
        Me.lblmccname.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblmccname.TextWrap = False
        '
        'txtmccode
        '
        Me.txtmccode.CalculationExpression = Nothing
        Me.txtmccode.FieldCode = Nothing
        Me.txtmccode.FieldDesc = Nothing
        Me.txtmccode.FieldMaxLength = 0
        Me.txtmccode.FieldName = Nothing
        Me.txtmccode.isCalculatedField = False
        Me.txtmccode.IsSourceFromTable = False
        Me.txtmccode.IsSourceFromValueList = False
        Me.txtmccode.IsUnique = False
        Me.txtmccode.Location = New System.Drawing.Point(131, 26)
        Me.txtmccode.MendatroryField = True
        Me.txtmccode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmccode.MyLinkLable1 = Me.MyLabel4
        Me.txtmccode.MyLinkLable2 = Me.lblmccname
        Me.txtmccode.MyReadOnly = False
        Me.txtmccode.MyShowMasterFormButton = False
        Me.txtmccode.Name = "txtmccode"
        Me.txtmccode.ReferenceFieldDesc = Nothing
        Me.txtmccode.ReferenceFieldName = Nothing
        Me.txtmccode.ReferenceTableName = Nothing
        Me.txtmccode.Size = New System.Drawing.Size(154, 18)
        Me.txtmccode.TabIndex = 2
        Me.txtmccode.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(19, 50)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(35, 16)
        Me.MyLabel3.TabIndex = 54
        Me.MyLabel3.Text = "Shift :"
        '
        'cmbShift
        '
        Me.cmbShift.AutoCompleteDisplayMember = Nothing
        Me.cmbShift.AutoCompleteValueMember = Nothing
        Me.cmbShift.CalculationExpression = Nothing
        Me.cmbShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbShift.FieldCode = Nothing
        Me.cmbShift.FieldDesc = Nothing
        Me.cmbShift.FieldMaxLength = 0
        Me.cmbShift.FieldName = Nothing
        Me.cmbShift.isCalculatedField = False
        Me.cmbShift.IsSourceFromTable = False
        Me.cmbShift.IsSourceFromValueList = False
        Me.cmbShift.IsUnique = False
        Me.cmbShift.Location = New System.Drawing.Point(131, 48)
        Me.cmbShift.MendatroryField = True
        Me.cmbShift.MyLinkLable1 = Me.MyLabel3
        Me.cmbShift.MyLinkLable2 = Nothing
        Me.cmbShift.Name = "cmbShift"
        Me.cmbShift.ReferenceFieldDesc = Nothing
        Me.cmbShift.ReferenceFieldName = Nothing
        Me.cmbShift.ReferenceTableName = Nothing
        Me.cmbShift.Size = New System.Drawing.Size(154, 20)
        Me.cmbShift.TabIndex = 3
        '
        'chkHoliday
        '
        Me.chkHoliday.Location = New System.Drawing.Point(463, 26)
        Me.chkHoliday.MyLinkLable1 = Nothing
        Me.chkHoliday.MyLinkLable2 = Nothing
        Me.chkHoliday.Name = "chkHoliday"
        Me.chkHoliday.Size = New System.Drawing.Size(83, 18)
        Me.chkHoliday.TabIndex = 1024
        Me.chkHoliday.Tag1 = Nothing
        Me.chkHoliday.Text = "Milk Holiday"
        '
        'LblBookStock
        '
        Me.LblBookStock.FieldName = Nothing
        Me.LblBookStock.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBookStock.Location = New System.Drawing.Point(24, 331)
        Me.LblBookStock.Name = "LblBookStock"
        Me.LblBookStock.Size = New System.Drawing.Size(87, 16)
        Me.LblBookStock.TabIndex = 1026
        Me.LblBookStock.Text = "Book Stock(KG)"
        Me.LblBookStock.Visible = False
        '
        'TxtActualStock
        '
        Me.TxtActualStock.BackColor = System.Drawing.Color.White
        Me.TxtActualStock.CalculationExpression = Nothing
        Me.TxtActualStock.DecimalPlaces = 3
        Me.TxtActualStock.FieldCode = Nothing
        Me.TxtActualStock.FieldDesc = Nothing
        Me.TxtActualStock.FieldMaxLength = 0
        Me.TxtActualStock.FieldName = Nothing
        Me.TxtActualStock.isCalculatedField = False
        Me.TxtActualStock.IsSourceFromTable = False
        Me.TxtActualStock.IsSourceFromValueList = False
        Me.TxtActualStock.IsUnique = False
        Me.TxtActualStock.Location = New System.Drawing.Point(136, 324)
        Me.TxtActualStock.MendatroryField = False
        Me.TxtActualStock.MyLinkLable1 = Me.LblBookStock
        Me.TxtActualStock.MyLinkLable2 = Nothing
        Me.TxtActualStock.Name = "TxtActualStock"
        Me.TxtActualStock.ReadOnly = True
        Me.TxtActualStock.ReferenceFieldDesc = Nothing
        Me.TxtActualStock.ReferenceFieldName = Nothing
        Me.TxtActualStock.ReferenceTableName = Nothing
        Me.TxtActualStock.Size = New System.Drawing.Size(154, 20)
        Me.TxtActualStock.TabIndex = 1025
        Me.TxtActualStock.Text = "0"
        Me.TxtActualStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtActualStock.Value = 0.0R
        Me.TxtActualStock.Visible = False
        '
        'TxtBookFat_Per
        '
        Me.TxtBookFat_Per.BackColor = System.Drawing.Color.White
        Me.TxtBookFat_Per.CalculationExpression = Nothing
        Me.TxtBookFat_Per.DecimalPlaces = 3
        Me.TxtBookFat_Per.FieldCode = Nothing
        Me.TxtBookFat_Per.FieldDesc = Nothing
        Me.TxtBookFat_Per.FieldMaxLength = 0
        Me.TxtBookFat_Per.FieldName = Nothing
        Me.TxtBookFat_Per.isCalculatedField = False
        Me.TxtBookFat_Per.IsSourceFromTable = False
        Me.TxtBookFat_Per.IsSourceFromValueList = False
        Me.TxtBookFat_Per.IsUnique = False
        Me.TxtBookFat_Per.Location = New System.Drawing.Point(136, 351)
        Me.TxtBookFat_Per.MendatroryField = False
        Me.TxtBookFat_Per.MyLinkLable1 = Me.LblBookFat_Per
        Me.TxtBookFat_Per.MyLinkLable2 = Nothing
        Me.TxtBookFat_Per.Name = "TxtBookFat_Per"
        Me.TxtBookFat_Per.ReadOnly = True
        Me.TxtBookFat_Per.ReferenceFieldDesc = Nothing
        Me.TxtBookFat_Per.ReferenceFieldName = Nothing
        Me.TxtBookFat_Per.ReferenceTableName = Nothing
        Me.TxtBookFat_Per.Size = New System.Drawing.Size(154, 20)
        Me.TxtBookFat_Per.TabIndex = 1031
        Me.TxtBookFat_Per.Text = "0"
        Me.TxtBookFat_Per.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtBookFat_Per.Value = 0.0R
        Me.TxtBookFat_Per.Visible = False
        '
        'LblBookFat_Per
        '
        Me.LblBookFat_Per.FieldName = Nothing
        Me.LblBookFat_Per.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBookFat_Per.Location = New System.Drawing.Point(24, 353)
        Me.LblBookFat_Per.Name = "LblBookFat_Per"
        Me.LblBookFat_Per.Size = New System.Drawing.Size(74, 16)
        Me.LblBookFat_Per.TabIndex = 1032
        Me.LblBookFat_Per.Text = "Book FAT(%)"
        Me.LblBookFat_Per.Visible = False
        '
        'LblBookFAT
        '
        Me.LblBookFAT.FieldName = Nothing
        Me.LblBookFAT.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBookFAT.Location = New System.Drawing.Point(300, 353)
        Me.LblBookFAT.Name = "LblBookFAT"
        Me.LblBookFAT.Size = New System.Drawing.Size(80, 16)
        Me.LblBookFAT.TabIndex = 1028
        Me.LblBookFAT.Text = "Book FAT(KG)"
        Me.LblBookFAT.Visible = False
        '
        'TxtActualFat
        '
        Me.TxtActualFat.BackColor = System.Drawing.Color.White
        Me.TxtActualFat.CalculationExpression = Nothing
        Me.TxtActualFat.DecimalPlaces = 3
        Me.TxtActualFat.FieldCode = Nothing
        Me.TxtActualFat.FieldDesc = Nothing
        Me.TxtActualFat.FieldMaxLength = 0
        Me.TxtActualFat.FieldName = Nothing
        Me.TxtActualFat.isCalculatedField = False
        Me.TxtActualFat.IsSourceFromTable = False
        Me.TxtActualFat.IsSourceFromValueList = False
        Me.TxtActualFat.IsUnique = False
        Me.TxtActualFat.Location = New System.Drawing.Point(406, 351)
        Me.TxtActualFat.MendatroryField = False
        Me.TxtActualFat.MyLinkLable1 = Me.LblBookFAT
        Me.TxtActualFat.MyLinkLable2 = Nothing
        Me.TxtActualFat.Name = "TxtActualFat"
        Me.TxtActualFat.ReadOnly = True
        Me.TxtActualFat.ReferenceFieldDesc = Nothing
        Me.TxtActualFat.ReferenceFieldName = Nothing
        Me.TxtActualFat.ReferenceTableName = Nothing
        Me.TxtActualFat.Size = New System.Drawing.Size(157, 20)
        Me.TxtActualFat.TabIndex = 1027
        Me.TxtActualFat.Text = "0"
        Me.TxtActualFat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtActualFat.Value = 0.0R
        Me.TxtActualFat.Visible = False
        '
        'LblBookSNF
        '
        Me.LblBookSNF.FieldName = Nothing
        Me.LblBookSNF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBookSNF.Location = New System.Drawing.Point(300, 376)
        Me.LblBookSNF.Name = "LblBookSNF"
        Me.LblBookSNF.Size = New System.Drawing.Size(82, 16)
        Me.LblBookSNF.TabIndex = 1030
        Me.LblBookSNF.Text = "Book SNF(KG)"
        Me.LblBookSNF.Visible = False
        '
        'TxtActualSNF
        '
        Me.TxtActualSNF.BackColor = System.Drawing.Color.White
        Me.TxtActualSNF.CalculationExpression = Nothing
        Me.TxtActualSNF.DecimalPlaces = 3
        Me.TxtActualSNF.FieldCode = Nothing
        Me.TxtActualSNF.FieldDesc = Nothing
        Me.TxtActualSNF.FieldMaxLength = 0
        Me.TxtActualSNF.FieldName = Nothing
        Me.TxtActualSNF.isCalculatedField = False
        Me.TxtActualSNF.IsSourceFromTable = False
        Me.TxtActualSNF.IsSourceFromValueList = False
        Me.TxtActualSNF.IsUnique = False
        Me.TxtActualSNF.Location = New System.Drawing.Point(406, 374)
        Me.TxtActualSNF.MendatroryField = False
        Me.TxtActualSNF.MyLinkLable1 = Me.LblBookSNF
        Me.TxtActualSNF.MyLinkLable2 = Nothing
        Me.TxtActualSNF.Name = "TxtActualSNF"
        Me.TxtActualSNF.ReadOnly = True
        Me.TxtActualSNF.ReferenceFieldDesc = Nothing
        Me.TxtActualSNF.ReferenceFieldName = Nothing
        Me.TxtActualSNF.ReferenceTableName = Nothing
        Me.TxtActualSNF.Size = New System.Drawing.Size(157, 20)
        Me.TxtActualSNF.TabIndex = 1029
        Me.TxtActualSNF.Text = "0"
        Me.TxtActualSNF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtActualSNF.Value = 0.0R
        Me.TxtActualSNF.Visible = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(603, 338)
        Me.RadPageViewPage2.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(603, 338)
        Me.UcAttachment1.TabIndex = 2
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(86.0!, 28.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(603, 364)
        Me.pvpCustomFields.Text = "Custum Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(603, 364)
        Me.UcCustomFields1.TabIndex = 2
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(8, 9)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 5
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(87, 9)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 6
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(815, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 7
        Me.btnclose.Text = "Close"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenu})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(890, 20)
        Me.RadMenu1.TabIndex = 321
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenu
        '
        Me.RadMenu.AccessibleDescription = "Setting"
        Me.RadMenu.AccessibleName = "Setting"
        Me.RadMenu.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExport, Me.rmImport})
        Me.RadMenu.Name = "RadMenu"
        Me.RadMenu.Text = "File"
        '
        'rmExport
        '
        Me.rmExport.AccessibleDescription = "RadMenuItem1"
        Me.rmExport.AccessibleName = "RadMenuItem1"
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export"
        '
        'rmImport
        '
        Me.rmImport.AccessibleDescription = "Import"
        Me.rmImport.AccessibleName = "Import"
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
        '
        'FrmOpenMCCShift
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(890, 492)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmOpenMCCShift"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Open MCC Shift"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.Panel2.PerformLayout()
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.txtCLR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtManualSNF_Per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblManualSNF_Per, System.ComponentModel.ISupportInitialize).EndInit()
        Me.split_systemstock.Panel2.ResumeLayout(False)
        Me.split_systemstock.Panel2.PerformLayout()
        Me.split_systemstock.ResumeLayout(False)
        CType(Me.txtremarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtsystemsnf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtsystemstock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtsystemfat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllowManualGeteEntryWeighment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtManualStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblManualStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldesid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblManualFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkManualEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpRegular, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpRegular.ResumeLayout(False)
        Me.GrpRegular.PerformLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblIrregularMccName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPPDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtManualFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpShiftDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblManualSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chkregular, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkManualWeighment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblManualFAT_Per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtManualFat_Per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtManualSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtBookSNF_per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblBookSNF_Per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblmccname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkHoliday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblBookStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtActualStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtBookFat_Per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblBookFat_Per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblBookFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtActualFat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblBookSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtActualSNF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.pvpCustomFields.ResumeLayout(False)
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents dtpShiftDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblPPDate As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lbldesid As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtmccode As common.UserControls.txtFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblmccname As common.Controls.MyLabel
    Friend WithEvents cmbShift As common.Controls.MyComboBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkHoliday As common.Controls.MyCheckBox
    Friend WithEvents TxtActualSNF As common.MyNumBox
    Friend WithEvents LblBookSNF As common.Controls.MyLabel
    Friend WithEvents TxtActualFat As common.MyNumBox
    Friend WithEvents LblBookFAT As common.Controls.MyLabel
    Friend WithEvents TxtActualStock As common.MyNumBox
    Friend WithEvents LblBookStock As common.Controls.MyLabel
    Friend WithEvents TxtManualSNF As common.MyNumBox
    Friend WithEvents LblManualSNF As common.Controls.MyLabel
    Friend WithEvents TxtManualFAT As common.MyNumBox
    Friend WithEvents LblManualFAT As common.Controls.MyLabel
    Friend WithEvents TxtManualStock As common.MyNumBox
    Friend WithEvents LblManualStock As common.Controls.MyLabel
    Friend WithEvents TxtBookSNF_per As common.MyNumBox
    Friend WithEvents LblBookSNF_Per As common.Controls.MyLabel
    Friend WithEvents TxtBookFat_Per As common.MyNumBox
    Friend WithEvents LblBookFat_Per As common.Controls.MyLabel
    Friend WithEvents TxtManualSNF_Per As common.MyNumBox
    Friend WithEvents LblManualSNF_Per As common.Controls.MyLabel
    Friend WithEvents TxtManualFat_Per As common.MyNumBox
    Friend WithEvents LblManualFAT_Per As common.Controls.MyLabel
    Friend WithEvents ChkManualEntry As common.Controls.MyCheckBox
    Friend WithEvents ChkManualWeighment As common.Controls.MyCheckBox
    Friend WithEvents Chkregular As common.Controls.MyCheckBox
    Friend WithEvents GrpRegular As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents FndIrregularMcc As common.UserControls.txtFinder
    Friend WithEvents LblIrregularMccName As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents chkAllowManualGeteEntryWeighment As common.Controls.MyCheckBox
    Friend WithEvents txtremarks As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtsystemstock As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtsystemfat As common.MyNumBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtsystemsnf As common.MyNumBox
    Friend WithEvents split_systemstock As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtCLR As common.MyNumBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
End Class

