<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCSADeliveryOrder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCSADeliveryOrder))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.cmbTax = New common.Controls.MyComboBox()
        Me.cmbCSAType = New common.Controls.MyComboBox()
        Me.fndRequestNo = New common.UserControls.txtFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtRt_UOM = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txttotal_amt = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.dtpdate = New common.Controls.MyDateTimePicker()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtrate = New common.MyNumBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtstate_name = New common.Controls.MyLabel()
        Me.txtstate_code = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txttoloc_name = New common.Controls.MyLabel()
        Me.txttoloc_code = New common.UserControls.txtFinder()
        Me.txtfrmloc_name = New common.Controls.MyLabel()
        Me.txtfrmloc_code = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtcustName = New common.Controls.MyLabel()
        Me.txtcustcode = New common.UserControls.txtFinder()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.lblCode = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.UcItemBalance1 = New XpertERPEngine.ucItemBalance()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnsavelayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btndeletelayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.btnAmendment = New Telerik.WinControls.UI.RadButton()
        Me.btnsetting = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnSend = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSend_Approval = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnpost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCSAType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRt_UOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttotal_amt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtrate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtstate_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttoloc_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfrmloc_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcustName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAmendment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.MyLabel10)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAmendment)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(912, 486)
        Me.SplitContainer1.SplitterDistance = 449
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(912, 429)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(891, 381)
        Me.RadPageViewPage1.Text = "Detail"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndRequestNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRt_UOM)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txttotal_amt)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpdate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtrate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtstate_name)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtstate_code)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txttoloc_name)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txttoloc_code)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtfrmloc_name)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtfrmloc_code)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtcustName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtcustcode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(1)
        Me.SplitContainer2.Size = New System.Drawing.Size(891, 381)
        Me.SplitContainer2.SplitterDistance = 136
        Me.SplitContainer2.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Controls.Add(Me.MyLabel9)
        Me.Panel1.Controls.Add(Me.cmbTax)
        Me.Panel1.Controls.Add(Me.cmbCSAType)
        Me.Panel1.Location = New System.Drawing.Point(274, 86)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(428, 22)
        Me.Panel1.TabIndex = 1
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(2, 3)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(83, 16)
        Me.MyLabel3.TabIndex = 6
        Me.MyLabel3.Text = "CSA Item Type"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(247, 3)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel9.TabIndex = 8
        Me.MyLabel9.Text = "Including Tax"
        '
        'cmbTax
        '
        Me.cmbTax.AutoCompleteDisplayMember = Nothing
        Me.cmbTax.AutoCompleteValueMember = Nothing
        Me.cmbTax.CalculationExpression = Nothing
        Me.cmbTax.FieldCode = Nothing
        Me.cmbTax.FieldDesc = Nothing
        Me.cmbTax.FieldMaxLength = 0
        Me.cmbTax.FieldName = Nothing
        Me.cmbTax.isCalculatedField = False
        Me.cmbTax.IsSourceFromTable = False
        Me.cmbTax.IsSourceFromValueList = False
        Me.cmbTax.IsUnique = False
        Me.cmbTax.Location = New System.Drawing.Point(330, 1)
        Me.cmbTax.MendatroryField = True
        Me.cmbTax.MyLinkLable1 = Me.MyLabel9
        Me.cmbTax.MyLinkLable2 = Nothing
        Me.cmbTax.Name = "cmbTax"
        Me.cmbTax.ReferenceFieldDesc = Nothing
        Me.cmbTax.ReferenceFieldName = Nothing
        Me.cmbTax.ReferenceTableName = Nothing
        Me.cmbTax.Size = New System.Drawing.Size(79, 20)
        Me.cmbTax.TabIndex = 4
        Me.cmbTax.Text = "MyComboBox1"
        '
        'cmbCSAType
        '
        Me.cmbCSAType.AutoCompleteDisplayMember = Nothing
        Me.cmbCSAType.AutoCompleteValueMember = Nothing
        Me.cmbCSAType.CalculationExpression = Nothing
        Me.cmbCSAType.FieldCode = Nothing
        Me.cmbCSAType.FieldDesc = Nothing
        Me.cmbCSAType.FieldMaxLength = 0
        Me.cmbCSAType.FieldName = Nothing
        Me.cmbCSAType.isCalculatedField = False
        Me.cmbCSAType.IsSourceFromTable = False
        Me.cmbCSAType.IsSourceFromValueList = False
        Me.cmbCSAType.IsUnique = False
        Me.cmbCSAType.Location = New System.Drawing.Point(95, 1)
        Me.cmbCSAType.MendatroryField = True
        Me.cmbCSAType.MyLinkLable1 = Me.MyLabel3
        Me.cmbCSAType.MyLinkLable2 = Nothing
        Me.cmbCSAType.Name = "cmbCSAType"
        Me.cmbCSAType.ReferenceFieldDesc = Nothing
        Me.cmbCSAType.ReferenceFieldName = Nothing
        Me.cmbCSAType.ReferenceTableName = Nothing
        Me.cmbCSAType.Size = New System.Drawing.Size(143, 20)
        Me.cmbCSAType.TabIndex = 7
        Me.cmbCSAType.Text = "MyComboBox1"
        '
        'fndRequestNo
        '
        Me.fndRequestNo.CalculationExpression = Nothing
        Me.fndRequestNo.FieldCode = Nothing
        Me.fndRequestNo.FieldDesc = Nothing
        Me.fndRequestNo.FieldMaxLength = 0
        Me.fndRequestNo.FieldName = Nothing
        Me.fndRequestNo.isCalculatedField = False
        Me.fndRequestNo.IsSourceFromTable = False
        Me.fndRequestNo.IsSourceFromValueList = False
        Me.fndRequestNo.IsUnique = False
        Me.fndRequestNo.Location = New System.Drawing.Point(370, 110)
        Me.fndRequestNo.MendatroryField = True
        Me.fndRequestNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRequestNo.MyLinkLable1 = Me.MyLabel5
        Me.fndRequestNo.MyLinkLable2 = Nothing
        Me.fndRequestNo.MyReadOnly = False
        Me.fndRequestNo.MyShowMasterFormButton = False
        Me.fndRequestNo.Name = "fndRequestNo"
        Me.fndRequestNo.ReferenceFieldDesc = Nothing
        Me.fndRequestNo.ReferenceFieldName = Nothing
        Me.fndRequestNo.ReferenceTableName = Nothing
        Me.fndRequestNo.Size = New System.Drawing.Size(143, 19)
        Me.fndRequestNo.TabIndex = 1395
        Me.fndRequestNo.Value = ""
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(277, 111)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(66, 16)
        Me.MyLabel5.TabIndex = 1396
        Me.MyLabel5.Text = "Request No"
        '
        'txtRt_UOM
        '
        Me.txtRt_UOM.AutoSize = False
        Me.txtRt_UOM.BorderVisible = True
        Me.txtRt_UOM.FieldName = Nothing
        Me.txtRt_UOM.Location = New System.Drawing.Point(206, 88)
        Me.txtRt_UOM.Name = "txtRt_UOM"
        Me.txtRt_UOM.Size = New System.Drawing.Size(68, 19)
        Me.txtRt_UOM.TabIndex = 1394
        Me.txtRt_UOM.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(721, 67)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel4.TabIndex = 1377
        Me.MyLabel4.Text = "To Location"
        Me.MyLabel4.Visible = False
        '
        'txttotal_amt
        '
        Me.txttotal_amt.AutoSize = False
        Me.txttotal_amt.BorderVisible = True
        Me.txttotal_amt.FieldName = Nothing
        Me.txttotal_amt.Location = New System.Drawing.Point(131, 110)
        Me.txttotal_amt.Name = "txttotal_amt"
        Me.txttotal_amt.Size = New System.Drawing.Size(143, 19)
        Me.txttotal_amt.TabIndex = 1390
        Me.txttotal_amt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(13, 111)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(100, 16)
        Me.MyLabel1.TabIndex = 1389
        Me.MyLabel1.Text = "Document Amount"
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(782, 18)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(97, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1388
        '
        'dtpdate
        '
        Me.dtpdate.CalculationExpression = Nothing
        Me.dtpdate.CustomFormat = "dd/MM/yyyy"
        Me.dtpdate.FieldCode = Nothing
        Me.dtpdate.FieldDesc = Nothing
        Me.dtpdate.FieldMaxLength = 0
        Me.dtpdate.FieldName = Nothing
        Me.dtpdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpdate.isCalculatedField = False
        Me.dtpdate.IsSourceFromTable = False
        Me.dtpdate.IsSourceFromValueList = False
        Me.dtpdate.IsUnique = False
        Me.dtpdate.Location = New System.Drawing.Point(600, 18)
        Me.dtpdate.MendatroryField = True
        Me.dtpdate.MyLinkLable1 = Me.MyLabel7
        Me.dtpdate.MyLinkLable2 = Nothing
        Me.dtpdate.Name = "dtpdate"
        Me.dtpdate.NullDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.dtpdate.ReferenceFieldDesc = Nothing
        Me.dtpdate.ReferenceFieldName = Nothing
        Me.dtpdate.ReferenceTableName = Nothing
        Me.dtpdate.Size = New System.Drawing.Size(84, 20)
        Me.dtpdate.TabIndex = 2
        Me.dtpdate.TabStop = False
        Me.dtpdate.Text = "11/09/2014"
        Me.dtpdate.Value = New Date(2014, 9, 11, 16, 2, 0, 928)
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(563, 20)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel7.TabIndex = 1382
        Me.MyLabel7.Text = "Date"
        '
        'txtrate
        '
        Me.txtrate.CalculationExpression = Nothing
        Me.txtrate.DecimalPlaces = 2
        Me.txtrate.FieldCode = Nothing
        Me.txtrate.FieldDesc = Nothing
        Me.txtrate.FieldMaxLength = 0
        Me.txtrate.FieldName = Nothing
        Me.txtrate.isCalculatedField = False
        Me.txtrate.IsSourceFromTable = False
        Me.txtrate.IsSourceFromValueList = False
        Me.txtrate.IsUnique = False
        Me.txtrate.Location = New System.Drawing.Point(131, 87)
        Me.txtrate.MendatroryField = True
        Me.txtrate.MyLinkLable1 = Me.MyLabel8
        Me.txtrate.MyLinkLable2 = Me.txtRt_UOM
        Me.txtrate.Name = "txtrate"
        Me.txtrate.ReferenceFieldDesc = Nothing
        Me.txtrate.ReferenceFieldName = Nothing
        Me.txtrate.ReferenceTableName = Nothing
        Me.txtrate.Size = New System.Drawing.Size(71, 20)
        Me.txtrate.TabIndex = 3
        Me.txtrate.Text = "0"
        Me.txtrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtrate.Value = 0.0R
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(13, 89)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel8.TabIndex = 1383
        Me.MyLabel8.Text = "Rate for RT"
        '
        'txtstate_name
        '
        Me.txtstate_name.AutoSize = False
        Me.txtstate_name.BorderVisible = True
        Me.txtstate_name.FieldName = Nothing
        Me.txtstate_name.Location = New System.Drawing.Point(844, 89)
        Me.txtstate_name.Name = "txtstate_name"
        Me.txtstate_name.Size = New System.Drawing.Size(43, 19)
        Me.txtstate_name.TabIndex = 1381
        Me.txtstate_name.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtstate_name.Visible = False
        '
        'txtstate_code
        '
        Me.txtstate_code.CalculationExpression = Nothing
        Me.txtstate_code.FieldCode = Nothing
        Me.txtstate_code.FieldDesc = Nothing
        Me.txtstate_code.FieldMaxLength = 0
        Me.txtstate_code.FieldName = Nothing
        Me.txtstate_code.isCalculatedField = False
        Me.txtstate_code.IsSourceFromTable = False
        Me.txtstate_code.IsSourceFromValueList = False
        Me.txtstate_code.IsUnique = False
        Me.txtstate_code.Location = New System.Drawing.Point(792, 88)
        Me.txtstate_code.MendatroryField = False
        Me.txtstate_code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtstate_code.MyLinkLable1 = Me.MyLabel6
        Me.txtstate_code.MyLinkLable2 = Me.txtstate_name
        Me.txtstate_code.MyReadOnly = False
        Me.txtstate_code.MyShowMasterFormButton = False
        Me.txtstate_code.Name = "txtstate_code"
        Me.txtstate_code.ReferenceFieldDesc = Nothing
        Me.txtstate_code.ReferenceFieldName = Nothing
        Me.txtstate_code.ReferenceTableName = Nothing
        Me.txtstate_code.Size = New System.Drawing.Size(48, 19)
        Me.txtstate_code.TabIndex = 5
        Me.txtstate_code.Value = ""
        Me.txtstate_code.Visible = False
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(721, 90)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel6.TabIndex = 1380
        Me.MyLabel6.Text = "State"
        Me.MyLabel6.Visible = False
        '
        'txttoloc_name
        '
        Me.txttoloc_name.AutoSize = False
        Me.txttoloc_name.BorderVisible = True
        Me.txttoloc_name.FieldName = Nothing
        Me.txttoloc_name.Location = New System.Drawing.Point(844, 67)
        Me.txttoloc_name.Name = "txttoloc_name"
        Me.txttoloc_name.Size = New System.Drawing.Size(43, 19)
        Me.txttoloc_name.TabIndex = 1378
        Me.txttoloc_name.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txttoloc_name.Visible = False
        '
        'txttoloc_code
        '
        Me.txttoloc_code.CalculationExpression = Nothing
        Me.txttoloc_code.FieldCode = Nothing
        Me.txttoloc_code.FieldDesc = Nothing
        Me.txttoloc_code.FieldMaxLength = 0
        Me.txttoloc_code.FieldName = Nothing
        Me.txttoloc_code.isCalculatedField = False
        Me.txttoloc_code.IsSourceFromTable = False
        Me.txttoloc_code.IsSourceFromValueList = False
        Me.txttoloc_code.IsUnique = False
        Me.txttoloc_code.Location = New System.Drawing.Point(792, 66)
        Me.txttoloc_code.MendatroryField = True
        Me.txttoloc_code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttoloc_code.MyLinkLable1 = Me.MyLabel4
        Me.txttoloc_code.MyLinkLable2 = Me.txttoloc_name
        Me.txttoloc_code.MyReadOnly = False
        Me.txttoloc_code.MyShowMasterFormButton = False
        Me.txttoloc_code.Name = "txttoloc_code"
        Me.txttoloc_code.ReferenceFieldDesc = Nothing
        Me.txttoloc_code.ReferenceFieldName = Nothing
        Me.txttoloc_code.ReferenceTableName = Nothing
        Me.txttoloc_code.Size = New System.Drawing.Size(48, 19)
        Me.txttoloc_code.TabIndex = 4
        Me.txttoloc_code.Value = ""
        Me.txttoloc_code.Visible = False
        '
        'txtfrmloc_name
        '
        Me.txtfrmloc_name.AutoSize = False
        Me.txtfrmloc_name.BorderVisible = True
        Me.txtfrmloc_name.FieldName = Nothing
        Me.txtfrmloc_name.Location = New System.Drawing.Point(277, 64)
        Me.txtfrmloc_name.Name = "txtfrmloc_name"
        Me.txtfrmloc_name.Size = New System.Drawing.Size(407, 19)
        Me.txtfrmloc_name.TabIndex = 1375
        Me.txtfrmloc_name.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtfrmloc_code
        '
        Me.txtfrmloc_code.CalculationExpression = Nothing
        Me.txtfrmloc_code.FieldCode = Nothing
        Me.txtfrmloc_code.FieldDesc = Nothing
        Me.txtfrmloc_code.FieldMaxLength = 0
        Me.txtfrmloc_code.FieldName = Nothing
        Me.txtfrmloc_code.isCalculatedField = False
        Me.txtfrmloc_code.IsSourceFromTable = False
        Me.txtfrmloc_code.IsSourceFromValueList = False
        Me.txtfrmloc_code.IsUnique = False
        Me.txtfrmloc_code.Location = New System.Drawing.Point(131, 64)
        Me.txtfrmloc_code.MendatroryField = True
        Me.txtfrmloc_code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfrmloc_code.MyLinkLable1 = Me.MyLabel2
        Me.txtfrmloc_code.MyLinkLable2 = Me.txtfrmloc_name
        Me.txtfrmloc_code.MyReadOnly = False
        Me.txtfrmloc_code.MyShowMasterFormButton = False
        Me.txtfrmloc_code.Name = "txtfrmloc_code"
        Me.txtfrmloc_code.ReferenceFieldDesc = Nothing
        Me.txtfrmloc_code.ReferenceFieldName = Nothing
        Me.txtfrmloc_code.ReferenceTableName = Nothing
        Me.txtfrmloc_code.Size = New System.Drawing.Size(143, 19)
        Me.txtfrmloc_code.TabIndex = 2
        Me.txtfrmloc_code.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(13, 65)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel2.TabIndex = 1374
        Me.MyLabel2.Text = "From Location"
        '
        'txtcustName
        '
        Me.txtcustName.AutoSize = False
        Me.txtcustName.BorderVisible = True
        Me.txtcustName.FieldName = Nothing
        Me.txtcustName.Location = New System.Drawing.Point(277, 42)
        Me.txtcustName.Name = "txtcustName"
        Me.txtcustName.Size = New System.Drawing.Size(407, 19)
        Me.txtcustName.TabIndex = 1372
        Me.txtcustName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtcustcode
        '
        Me.txtcustcode.CalculationExpression = Nothing
        Me.txtcustcode.FieldCode = Nothing
        Me.txtcustcode.FieldDesc = Nothing
        Me.txtcustcode.FieldMaxLength = 0
        Me.txtcustcode.FieldName = Nothing
        Me.txtcustcode.isCalculatedField = False
        Me.txtcustcode.IsSourceFromTable = False
        Me.txtcustcode.IsSourceFromValueList = False
        Me.txtcustcode.IsUnique = False
        Me.txtcustcode.Location = New System.Drawing.Point(131, 42)
        Me.txtcustcode.MendatroryField = True
        Me.txtcustcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustcode.MyLinkLable1 = Me.RadLabel5
        Me.txtcustcode.MyLinkLable2 = Me.txtcustName
        Me.txtcustcode.MyReadOnly = False
        Me.txtcustcode.MyShowMasterFormButton = False
        Me.txtcustcode.Name = "txtcustcode"
        Me.txtcustcode.ReferenceFieldDesc = Nothing
        Me.txtcustcode.ReferenceFieldName = Nothing
        Me.txtcustcode.ReferenceTableName = Nothing
        Me.txtcustcode.Size = New System.Drawing.Size(143, 19)
        Me.txtcustcode.TabIndex = 1
        Me.txtcustcode.Value = ""
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(13, 42)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel5.TabIndex = 1371
        Me.RadLabel5.Text = "CSA Name"
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(13, 18)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(88, 16)
        Me.lblCode.TabIndex = 47
        Me.lblCode.Text = "Document Code"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(444, 17)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = " "
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(130, 16)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(309, 21)
        Me.txtCode.TabIndex = 46
        Me.txtCode.Value = ""
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(1, 1)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.gv)
        Me.SplitContainer3.Panel1.Padding = New System.Windows.Forms.Padding(1)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.UcItemBalance1)
        Me.SplitContainer3.Panel2.Padding = New System.Windows.Forms.Padding(1)
        Me.SplitContainer3.Size = New System.Drawing.Size(889, 239)
        Me.SplitContainer3.SplitterDistance = 157
        Me.SplitContainer3.TabIndex = 31
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(1, 1)
        '
        'gv
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.AllowDragToGroup = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.EnableGrouping = False
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(887, 155)
        Me.gv.TabIndex = 0
        Me.gv.Text = "RadGridView1"
        '
        'UcItemBalance1
        '
        Me.UcItemBalance1.CommitedQty = False
        Me.UcItemBalance1.CommitedQtyLbl = False
        Me.UcItemBalance1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcItemBalance1.ItemCode = ""
        Me.UcItemBalance1.ItemMRP = 0.0R
        Me.UcItemBalance1.ItemName = ""
        Me.UcItemBalance1.Location = New System.Drawing.Point(1, 1)
        Me.UcItemBalance1.LocationCode = ""
        Me.UcItemBalance1.LocationName = ""
        Me.UcItemBalance1.MaximumSize = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.MinimumSize = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.Name = "UcItemBalance1"
        Me.UcItemBalance1.ShowCSADOQty = False
        Me.UcItemBalance1.ShowPOQty = False
        Me.UcItemBalance1.ShowSOQty = False
        Me.UcItemBalance1.Size = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.TabIndex = 30
        Me.UcItemBalance1.TransDate = New Date(CType(0, Long))
        Me.UcItemBalance1.TransNo = ""
        Me.UcItemBalance1.UOM = ""
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(891, 381)
        Me.RadPageViewPage2.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(891, 381)
        Me.UcAttachment1.TabIndex = 7
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(912, 20)
        Me.RadMenu1.TabIndex = 1391
        Me.RadMenu1.Text = "RadMenu1"
        '
        'rdmenufile
        '
        Me.rdmenufile.AccessibleDescription = "RadMenuItem1"
        Me.rdmenufile.AccessibleName = "RadMenuItem1"
        Me.rdmenufile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnsavelayout, Me.btndeletelayout, Me.RadMenuItem1})
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Text = "File"
        '
        'btnsavelayout
        '
        Me.btnsavelayout.AccessibleDescription = "Save Layout"
        Me.btnsavelayout.AccessibleName = "Save Layout"
        Me.btnsavelayout.Name = "btnsavelayout"
        Me.btnsavelayout.Text = "Save Layout"
        '
        'btndeletelayout
        '
        Me.btndeletelayout.AccessibleDescription = "Delete Layout"
        Me.btndeletelayout.AccessibleName = "Delete Layout"
        Me.btndeletelayout.Name = "btndeletelayout"
        Me.btndeletelayout.Text = "Delete Layout"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Email And SMS Setting"
        Me.RadMenuItem1.AccessibleName = "Email And SMS Setting"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Email And SMS Setting"
        '
        'MyLabel10
        '
        Me.MyLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.ForeColor = System.Drawing.Color.Blue
        Me.MyLabel10.Location = New System.Drawing.Point(592, 7)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(229, 16)
        Me.MyLabel10.TabIndex = 11
        Me.MyLabel10.Text = "Press Alt+Ctrl+Shift+F11 for Amendment."
        '
        'btnAmendment
        '
        Me.btnAmendment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAmendment.Location = New System.Drawing.Point(414, 6)
        Me.btnAmendment.Name = "btnAmendment"
        Me.btnAmendment.Size = New System.Drawing.Size(73, 20)
        Me.btnAmendment.TabIndex = 10
        Me.btnAmendment.Text = "Amendment"
        '
        'btnsetting
        '
        Me.btnsetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnSend, Me.btnSend_Approval})
        Me.btnsetting.Location = New System.Drawing.Point(326, 6)
        Me.btnsetting.Name = "btnsetting"
        Me.btnsetting.Size = New System.Drawing.Size(83, 20)
        Me.btnsetting.TabIndex = 9
        Me.btnsetting.Text = "E-Mail/SMS"
        '
        'btnSend
        '
        Me.btnSend.AccessibleDescription = "Send Email/SMS"
        Me.btnSend.AccessibleName = "Send Email/SMS"
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Text = "Send Email/SMS"
        '
        'btnSend_Approval
        '
        Me.btnSend_Approval.AccessibleDescription = "Send For Approval"
        Me.btnSend_Approval.AccessibleName = "Send For Approval"
        Me.btnSend_Approval.Name = "btnSend_Approval"
        Me.btnSend_Approval.Text = "Send For Approval"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(247, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(73, 20)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnpost
        '
        Me.btnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnpost.Location = New System.Drawing.Point(168, 6)
        Me.btnpost.Name = "btnpost"
        Me.btnpost.Size = New System.Drawing.Size(73, 20)
        Me.btnpost.TabIndex = 2
        Me.btnpost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(827, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(89, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(10, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'FrmCSADeliveryOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(912, 486)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmCSADeliveryOrder"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmCSADeliveryOrder"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCSAType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRt_UOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttotal_amt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtrate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtstate_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttoloc_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfrmloc_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcustName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAmendment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents txtcustName As common.Controls.MyLabel
    Friend WithEvents txtcustcode As common.UserControls.txtFinder
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtstate_name As common.Controls.MyLabel
    Friend WithEvents txtstate_code As common.UserControls.txtFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txttoloc_name As common.Controls.MyLabel
    Friend WithEvents txttoloc_code As common.UserControls.txtFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtfrmloc_name As common.Controls.MyLabel
    Friend WithEvents txtfrmloc_code As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtrate As common.MyNumBox
    Friend WithEvents cmbTax As common.Controls.MyComboBox
    Friend WithEvents dtpdate As common.Controls.MyDateTimePicker
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents btnpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txttotal_amt As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnsavelayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btndeletelayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents cmbCSAType As common.Controls.MyComboBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtRt_UOM As common.Controls.MyLabel
    Friend WithEvents UcItemBalance1 As XpertERPEngine.ucItemBalance
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnsetting As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnSend As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnSend_Approval As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents fndRequestNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents btnAmendment As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class

