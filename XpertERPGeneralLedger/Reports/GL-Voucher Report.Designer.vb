<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JrnlVoucherReport
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
        Me.lblTransType = New common.Controls.MyLabel()
        Me.fndtransType = New common.UserControls.txtFinder()
        Me.chkSummary = New Telerik.WinControls.UI.RadCheckBox()
        Me.dgvLocation = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgLocSeg = New common.MyCheckBoxGrid()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.chkLocSelect = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkLocAll = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cgvtrans = New common.MyCheckBoxGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkTypeSelect = New common.Controls.MyRadioButton()
        Me.chktransAll = New common.Controls.MyRadioButton()
        Me.cg = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgSource = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkcodeselect = New common.Controls.MyRadioButton()
        Me.chkallcode = New common.Controls.MyRadioButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgVoucher = New common.MyCheckBoxGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkVouchertSelect = New common.Controls.MyRadioButton()
        Me.chkVoucherAll = New common.Controls.MyRadioButton()
        Me.dtTo = New common.Controls.MyDateTimePicker()
        Me.dtFrm = New common.Controls.MyDateTimePicker()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gridvoucher = New common.UserControls.MyRadGridView()
        Me.Refreshbtn = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnQuickExport = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.lblTransType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dgvLocation.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkTypeSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chktransAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cg.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkcodeselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkallcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkVouchertSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVoucherAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFrm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gridvoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridvoucher.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Refreshbtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnQuickExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTransType
        '
        Me.lblTransType.FieldName = Nothing
        Me.lblTransType.Location = New System.Drawing.Point(536, 6)
        Me.lblTransType.Name = "lblTransType"
        Me.lblTransType.Size = New System.Drawing.Size(89, 18)
        Me.lblTransType.TabIndex = 311
        Me.lblTransType.Text = "transaction Type"
        '
        'fndtransType
        '
        Me.fndtransType.CalculationExpression = Nothing
        Me.fndtransType.FieldCode = Nothing
        Me.fndtransType.FieldDesc = Nothing
        Me.fndtransType.FieldMaxLength = 0
        Me.fndtransType.FieldName = Nothing
        Me.fndtransType.isCalculatedField = False
        Me.fndtransType.IsSourceFromTable = False
        Me.fndtransType.IsSourceFromValueList = False
        Me.fndtransType.IsUnique = False
        Me.fndtransType.Location = New System.Drawing.Point(631, 6)
        Me.fndtransType.MendatroryField = True
        Me.fndtransType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndtransType.MyLinkLable1 = Nothing
        Me.fndtransType.MyLinkLable2 = Nothing
        Me.fndtransType.MyReadOnly = False
        Me.fndtransType.MyShowMasterFormButton = False
        Me.fndtransType.Name = "fndtransType"
        Me.fndtransType.ReferenceFieldDesc = Nothing
        Me.fndtransType.ReferenceFieldName = Nothing
        Me.fndtransType.ReferenceTableName = Nothing
        Me.fndtransType.Size = New System.Drawing.Size(159, 19)
        Me.fndtransType.TabIndex = 310
        Me.fndtransType.Value = ""
        '
        'chkSummary
        '
        Me.chkSummary.Location = New System.Drawing.Point(445, 6)
        Me.chkSummary.Name = "chkSummary"
        Me.chkSummary.Size = New System.Drawing.Size(67, 18)
        Me.chkSummary.TabIndex = 309
        Me.chkSummary.Text = "Summary"
        '
        'dgvLocation
        '
        Me.dgvLocation.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.dgvLocation.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.dgvLocation.Controls.Add(Me.cbgLocSeg)
        Me.dgvLocation.Controls.Add(Me.Panel4)
        Me.dgvLocation.HeaderText = "Location"
        Me.dgvLocation.Location = New System.Drawing.Point(669, 34)
        Me.dgvLocation.Name = "dgvLocation"
        Me.dgvLocation.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.dgvLocation.Size = New System.Drawing.Size(347, 203)
        Me.dgvLocation.TabIndex = 308
        Me.dgvLocation.Text = "Location"
        '
        'cbgLocSeg
        '
        Me.cbgLocSeg.CheckedValue = Nothing
        Me.cbgLocSeg.DataSource = Nothing
        Me.cbgLocSeg.DisplayMember = "Name"
        Me.cbgLocSeg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocSeg.Location = New System.Drawing.Point(10, 46)
        Me.cbgLocSeg.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocSeg.MyShowHeadrText = False
        Me.cbgLocSeg.Name = "cbgLocSeg"
        Me.cbgLocSeg.Size = New System.Drawing.Size(327, 147)
        Me.cbgLocSeg.TabIndex = 1
        Me.cbgLocSeg.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkLocSelect)
        Me.Panel4.Controls.Add(Me.chkLocAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(327, 26)
        Me.Panel4.TabIndex = 0
        '
        'chkLocSelect
        '
        Me.chkLocSelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkLocSelect.Location = New System.Drawing.Point(168, 4)
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 1
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkLocAll.Location = New System.Drawing.Point(125, 4)
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 0
        Me.chkLocAll.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.RadGroupBox2.Controls.Add(Me.cgvtrans)
        Me.RadGroupBox2.Controls.Add(Me.Panel3)
        Me.RadGroupBox2.HeaderText = "Transection Type"
        Me.RadGroupBox2.Location = New System.Drawing.Point(307, 34)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(347, 203)
        Me.RadGroupBox2.TabIndex = 307
        Me.RadGroupBox2.Text = "Transection Type"
        '
        'cgvtrans
        '
        Me.cgvtrans.CheckedValue = Nothing
        Me.cgvtrans.DataSource = Nothing
        Me.cgvtrans.DisplayMember = "Name"
        Me.cgvtrans.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cgvtrans.Location = New System.Drawing.Point(10, 40)
        Me.cgvtrans.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cgvtrans.MyShowHeadrText = False
        Me.cgvtrans.Name = "cgvtrans"
        Me.cgvtrans.Size = New System.Drawing.Size(327, 153)
        Me.cgvtrans.TabIndex = 2
        Me.cgvtrans.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkTypeSelect)
        Me.Panel3.Controls.Add(Me.chktransAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(327, 20)
        Me.Panel3.TabIndex = 1
        '
        'chkTypeSelect
        '
        Me.chkTypeSelect.Location = New System.Drawing.Point(156, 1)
        Me.chkTypeSelect.MyLinkLable1 = Nothing
        Me.chkTypeSelect.MyLinkLable2 = Nothing
        Me.chkTypeSelect.Name = "chkTypeSelect"
        Me.chkTypeSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkTypeSelect.TabIndex = 2
        Me.chkTypeSelect.Text = "Select"
        '
        'chktransAll
        '
        Me.chktransAll.Location = New System.Drawing.Point(94, 1)
        Me.chktransAll.MyLinkLable1 = Nothing
        Me.chktransAll.MyLinkLable2 = Nothing
        Me.chktransAll.Name = "chktransAll"
        Me.chktransAll.Size = New System.Drawing.Size(33, 18)
        Me.chktransAll.TabIndex = 1
        Me.chktransAll.Text = "All"
        '
        'cg
        '
        Me.cg.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.cg.Controls.Add(Me.cbgSource)
        Me.cg.Controls.Add(Me.Panel1)
        Me.cg.HeaderText = "Source Code"
        Me.cg.Location = New System.Drawing.Point(13, 32)
        Me.cg.Name = "cg"
        Me.cg.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.cg.Size = New System.Drawing.Size(347, 203)
        Me.cg.TabIndex = 306
        Me.cg.Text = "Source Code"
        '
        'cbgSource
        '
        Me.cbgSource.CheckedValue = Nothing
        Me.cbgSource.DataSource = Nothing
        Me.cbgSource.DisplayMember = "Name"
        Me.cbgSource.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgSource.Location = New System.Drawing.Point(10, 40)
        Me.cbgSource.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgSource.MyShowHeadrText = False
        Me.cbgSource.Name = "cbgSource"
        Me.cbgSource.Size = New System.Drawing.Size(327, 153)
        Me.cbgSource.TabIndex = 2
        Me.cbgSource.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkcodeselect)
        Me.Panel1.Controls.Add(Me.chkallcode)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(327, 20)
        Me.Panel1.TabIndex = 1
        '
        'chkcodeselect
        '
        Me.chkcodeselect.Location = New System.Drawing.Point(139, 1)
        Me.chkcodeselect.MyLinkLable1 = Nothing
        Me.chkcodeselect.MyLinkLable2 = Nothing
        Me.chkcodeselect.Name = "chkcodeselect"
        Me.chkcodeselect.Size = New System.Drawing.Size(50, 18)
        Me.chkcodeselect.TabIndex = 2
        Me.chkcodeselect.Text = "Select"
        '
        'chkallcode
        '
        Me.chkallcode.Location = New System.Drawing.Point(88, 1)
        Me.chkallcode.MyLinkLable1 = Nothing
        Me.chkallcode.MyLinkLable2 = Nothing
        Me.chkallcode.Name = "chkallcode"
        Me.chkallcode.Size = New System.Drawing.Size(33, 18)
        Me.chkallcode.TabIndex = 1
        Me.chkallcode.Text = "All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgVoucher)
        Me.RadGroupBox3.Controls.Add(Me.Panel2)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.HeaderText = "Select Voucher"
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(995, 305)
        Me.RadGroupBox3.TabIndex = 305
        Me.RadGroupBox3.Text = "Select Voucher"
        '
        'cbgVoucher
        '
        Me.cbgVoucher.CheckedValue = Nothing
        Me.cbgVoucher.DataSource = Nothing
        Me.cbgVoucher.DisplayMember = "Name"
        Me.cbgVoucher.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVoucher.Location = New System.Drawing.Point(10, 40)
        Me.cbgVoucher.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.None
        Me.cbgVoucher.MyShowHeadrText = True
        Me.cbgVoucher.Name = "cbgVoucher"
        Me.cbgVoucher.Size = New System.Drawing.Size(975, 255)
        Me.cbgVoucher.TabIndex = 1
        Me.cbgVoucher.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkVouchertSelect)
        Me.Panel2.Controls.Add(Me.chkVoucherAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(975, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkVouchertSelect
        '
        Me.chkVouchertSelect.Location = New System.Drawing.Point(456, 1)
        Me.chkVouchertSelect.MyLinkLable1 = Nothing
        Me.chkVouchertSelect.MyLinkLable2 = Nothing
        Me.chkVouchertSelect.Name = "chkVouchertSelect"
        Me.chkVouchertSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkVouchertSelect.TabIndex = 1
        Me.chkVouchertSelect.Text = "Select"
        '
        'chkVoucherAll
        '
        Me.chkVoucherAll.Location = New System.Drawing.Point(405, 1)
        Me.chkVoucherAll.MyLinkLable1 = Nothing
        Me.chkVoucherAll.MyLinkLable2 = Nothing
        Me.chkVoucherAll.Name = "chkVoucherAll"
        Me.chkVoucherAll.Size = New System.Drawing.Size(33, 18)
        Me.chkVoucherAll.TabIndex = 0
        Me.chkVoucherAll.Text = "All"
        '
        'dtTo
        '
        Me.dtTo.CalculationExpression = Nothing
        Me.dtTo.CustomFormat = "dd/MM/yyyy"
        Me.dtTo.FieldCode = Nothing
        Me.dtTo.FieldDesc = Nothing
        Me.dtTo.FieldMaxLength = 0
        Me.dtTo.FieldName = Nothing
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTo.isCalculatedField = False
        Me.dtTo.IsSourceFromTable = False
        Me.dtTo.IsSourceFromValueList = False
        Me.dtTo.IsUnique = False
        Me.dtTo.Location = New System.Drawing.Point(316, 8)
        Me.dtTo.MendatroryField = False
        Me.dtTo.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtTo.MyLinkLable1 = Nothing
        Me.dtTo.MyLinkLable2 = Nothing
        Me.dtTo.Name = "dtTo"
        Me.dtTo.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtTo.ReferenceFieldDesc = Nothing
        Me.dtTo.ReferenceFieldName = Nothing
        Me.dtTo.ReferenceTableName = Nothing
        Me.dtTo.Size = New System.Drawing.Size(85, 20)
        Me.dtTo.TabIndex = 1
        Me.dtTo.TabStop = False
        Me.dtTo.Text = "29/05/2011"
        Me.dtTo.Value = New Date(2011, 5, 29, 19, 15, 53, 125)
        '
        'dtFrm
        '
        Me.dtFrm.CalculationExpression = Nothing
        Me.dtFrm.CustomFormat = "dd/MM/yyyy"
        Me.dtFrm.FieldCode = Nothing
        Me.dtFrm.FieldDesc = Nothing
        Me.dtFrm.FieldMaxLength = 0
        Me.dtFrm.FieldName = Nothing
        Me.dtFrm.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFrm.isCalculatedField = False
        Me.dtFrm.IsSourceFromTable = False
        Me.dtFrm.IsSourceFromValueList = False
        Me.dtFrm.IsUnique = False
        Me.dtFrm.Location = New System.Drawing.Point(149, 8)
        Me.dtFrm.MendatroryField = False
        Me.dtFrm.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtFrm.MyLinkLable1 = Nothing
        Me.dtFrm.MyLinkLable2 = Nothing
        Me.dtFrm.Name = "dtFrm"
        Me.dtFrm.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtFrm.ReferenceFieldDesc = Nothing
        Me.dtFrm.ReferenceFieldName = Nothing
        Me.dtFrm.ReferenceTableName = Nothing
        Me.dtFrm.Size = New System.Drawing.Size(85, 20)
        Me.dtFrm.TabIndex = 0
        Me.dtFrm.TabStop = False
        Me.dtFrm.Text = "29/05/2011"
        Me.dtFrm.Value = New Date(2011, 5, 29, 19, 15, 53, 125)
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(276, 10)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel3.TabIndex = 3
        Me.RadLabel3.Text = "To"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(107, 9)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel2.TabIndex = 2
        Me.RadLabel2.Text = "From"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(10, 7)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(77, 18)
        Me.RadLabel1.TabIndex = 1
        Me.RadLabel1.Text = "Voucher Date:"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(146, 2)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(61, 24)
        Me.btnPrint.TabIndex = 1
        Me.btnPrint.Text = "Print"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(79, 2)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(61, 24)
        Me.btnReset.TabIndex = 2
        Me.btnReset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(942, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(61, 24)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1016, 597)
        Me.RadPageView1.TabIndex = 3
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(995, 549)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblTransType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndtransType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkSummary)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dgvLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtFrm)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtTo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cg)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox3)
        Me.SplitContainer2.Size = New System.Drawing.Size(995, 549)
        Me.SplitContainer2.SplitterDistance = 240
        Me.SplitContainer2.TabIndex = 306
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gridvoucher)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(37.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(995, 580)
        Me.RadPageViewPage2.Text = "Grid"
        '
        'gridvoucher
        '
        Me.gridvoucher.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridvoucher.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gridvoucher.MasterTemplate.ShowHeaderCellButtons = True
        Me.gridvoucher.Name = "gridvoucher"
        Me.gridvoucher.ShowHeaderCellButtons = True
        Me.gridvoucher.Size = New System.Drawing.Size(995, 580)
        Me.gridvoucher.TabIndex = 0
        Me.gridvoucher.Text = "RadGridView1"
        '
        'Refreshbtn
        '
        Me.Refreshbtn.Location = New System.Drawing.Point(12, 2)
        Me.Refreshbtn.Name = "Refreshbtn"
        Me.Refreshbtn.Size = New System.Drawing.Size(61, 24)
        Me.Refreshbtn.TabIndex = 3
        Me.Refreshbtn.Text = ">>>"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnQuickExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Refreshbtn)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1016, 630)
        Me.SplitContainer1.SplitterDistance = 597
        Me.SplitContainer1.TabIndex = 4
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(213, 2)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(86, 24)
        Me.RadSplitButton1.TabIndex = 6
        Me.RadSplitButton1.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.AccessibleDescription = "Excel"
        Me.rmiExcel.AccessibleName = "Excel"
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        '
        'rmiPDF
        '
        Me.rmiPDF.AccessibleDescription = "PDF"
        Me.rmiPDF.AccessibleName = "PDF"
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        '
        'btnQuickExport
        '
        Me.btnQuickExport.Location = New System.Drawing.Point(305, 3)
        Me.btnQuickExport.Name = "btnQuickExport"
        Me.btnQuickExport.Size = New System.Drawing.Size(86, 24)
        Me.btnQuickExport.TabIndex = 5
        Me.btnQuickExport.Text = "Quick Export"
        Me.btnQuickExport.Visible = False
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1016, 20)
        Me.RadMenu1.TabIndex = 5
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Setting"
        Me.RadMenuItem1.AccessibleName = "Setting"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.AccessibleDescription = "Save Layout"
        Me.rmSaveLayout.AccessibleName = "Save Layout"
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.rmDeleteLayout.AccessibleName = "Delete Layout"
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'JrnlVoucherReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 650)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "JrnlVoucherReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Jounral Voucher Report"
        CType(Me.lblTransType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dgvLocation.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkTypeSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chktransAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cg.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkcodeselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkallcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkVouchertSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVoucherAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFrm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gridvoucher.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridvoucher, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Refreshbtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnQuickExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtTo As common.Controls.MyDateTimePicker
    Friend WithEvents dtFrm As common.Controls.MyDateTimePicker
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVoucher As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkVouchertSelect As common.Controls.MyRadioButton
    Friend WithEvents chkVoucherAll As common.Controls.MyRadioButton
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents cg As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgSource As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkcodeselect As common.Controls.MyRadioButton
    Friend WithEvents chkallcode As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cgvtrans As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkTypeSelect As common.Controls.MyRadioButton
    Friend WithEvents chktransAll As common.Controls.MyRadioButton
    Friend WithEvents dgvLocation As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocSeg As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkLocAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkSummary As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents fndtransType As common.UserControls.txtFinder
    Friend WithEvents lblTransType As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Refreshbtn As Telerik.WinControls.UI.RadButton
    Friend WithEvents gridvoucher As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnQuickExport As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
End Class

