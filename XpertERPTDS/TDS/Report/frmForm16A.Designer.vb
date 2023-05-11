Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmForm16A
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgLocation = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkLocSelect = New common.Controls.MyRadioButton()
        Me.chkLocAll = New common.Controls.MyRadioButton()
        Me.fndVendorNameNew = New common.UserControls.txtFinder()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgDocument = New common.MyCheckBoxGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkDocumentSelect = New common.Controls.MyRadioButton()
        Me.chkDocumentAll = New common.Controls.MyRadioButton()
        Me.dtpcontodate = New common.Controls.MyDateTimePicker()
        Me.dtpdatefrom = New common.Controls.MyDateTimePicker()
        Me.dtpdateto = New common.Controls.MyDateTimePicker()
        Me.dtpconFromdate = New common.Controls.MyDateTimePicker()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.lblConsolidate = New common.Controls.MyLabel()
        Me.lblToConsolidate = New common.Controls.MyLabel()
        Me.lblFromConsolidate = New common.Controls.MyLabel()
        Me.lblDateRange = New common.Controls.MyLabel()
        Me.lblTodateRange = New common.Controls.MyLabel()
        Me.lblFromDateRange = New common.Controls.MyLabel()
        Me.lblVendorNumber = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnConsolidation = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnDuplicate = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnOriginal = New Telerik.WinControls.UI.RadRadioButton()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkDocumentSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDocumentAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpcontodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpdatefrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpdateto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpconFromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConsolidate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToConsolidate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromConsolidate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDateRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTodateRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromDateRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.rbtnConsolidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDuplicate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnOriginal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.fndVendorNameNew)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.dtpcontodate)
        Me.RadGroupBox1.Controls.Add(Me.dtpdatefrom)
        Me.RadGroupBox1.Controls.Add(Me.dtpdateto)
        Me.RadGroupBox1.Controls.Add(Me.dtpconFromdate)
        Me.RadGroupBox1.Controls.Add(Me.btnReset)
        Me.RadGroupBox1.Controls.Add(Me.btnClose)
        Me.RadGroupBox1.Controls.Add(Me.btnPrint)
        Me.RadGroupBox1.Controls.Add(Me.lblConsolidate)
        Me.RadGroupBox1.Controls.Add(Me.lblToConsolidate)
        Me.RadGroupBox1.Controls.Add(Me.lblFromConsolidate)
        Me.RadGroupBox1.Controls.Add(Me.lblDateRange)
        Me.RadGroupBox1.Controls.Add(Me.lblTodateRange)
        Me.RadGroupBox1.Controls.Add(Me.lblFromDateRange)
        Me.RadGroupBox1.Controls.Add(Me.lblVendorNumber)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(8, 8)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(571, 621)
        Me.RadGroupBox1.TabIndex = 0
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox4.Controls.Add(Me.Panel1)
        Me.RadGroupBox4.HeaderText = "Location"
        Me.RadGroupBox4.Location = New System.Drawing.Point(4, 315)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(563, 202)
        Me.RadGroupBox4.TabIndex = 306
        Me.RadGroupBox4.Text = "Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 40)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(543, 152)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkLocSelect)
        Me.Panel1.Controls.Add(Me.chkLocAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(543, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkLocSelect
        '
        Me.chkLocSelect.Location = New System.Drawing.Point(256, 1)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 1
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.Location = New System.Drawing.Point(205, 1)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 0
        Me.chkLocAll.Text = "All"
        '
        'fndVendorNameNew
        '
        Me.fndVendorNameNew.CalculationExpression = Nothing
        Me.fndVendorNameNew.FieldCode = Nothing
        Me.fndVendorNameNew.FieldDesc = Nothing
        Me.fndVendorNameNew.FieldMaxLength = 0
        Me.fndVendorNameNew.FieldName = Nothing
        Me.fndVendorNameNew.isCalculatedField = False
        Me.fndVendorNameNew.IsSourceFromTable = False
        Me.fndVendorNameNew.IsSourceFromValueList = False
        Me.fndVendorNameNew.IsUnique = False
        Me.fndVendorNameNew.Location = New System.Drawing.Point(136, 66)
        Me.fndVendorNameNew.MendatroryField = False
        Me.fndVendorNameNew.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVendorNameNew.MyLinkLable1 = Nothing
        Me.fndVendorNameNew.MyLinkLable2 = Nothing
        Me.fndVendorNameNew.MyReadOnly = False
        Me.fndVendorNameNew.MyShowMasterFormButton = False
        Me.fndVendorNameNew.Name = "fndVendorNameNew"
        Me.fndVendorNameNew.ReferenceFieldDesc = Nothing
        Me.fndVendorNameNew.ReferenceFieldName = Nothing
        Me.fndVendorNameNew.ReferenceTableName = Nothing
        Me.fndVendorNameNew.Size = New System.Drawing.Size(215, 19)
        Me.fndVendorNameNew.TabIndex = 305
        Me.fndVendorNameNew.Value = ""
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgDocument)
        Me.RadGroupBox3.Controls.Add(Me.Panel2)
        Me.RadGroupBox3.HeaderText = "Document"
        Me.RadGroupBox3.Location = New System.Drawing.Point(4, 105)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(563, 204)
        Me.RadGroupBox3.TabIndex = 304
        Me.RadGroupBox3.Text = "Document"
        '
        'cbgDocument
        '
        Me.cbgDocument.CheckedValue = Nothing
        Me.cbgDocument.DataSource = Nothing
        Me.cbgDocument.DisplayMember = "Name"
        Me.cbgDocument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgDocument.Location = New System.Drawing.Point(10, 40)
        Me.cbgDocument.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgDocument.MyShowHeadrText = False
        Me.cbgDocument.Name = "cbgDocument"
        Me.cbgDocument.Size = New System.Drawing.Size(543, 154)
        Me.cbgDocument.TabIndex = 1
        Me.cbgDocument.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkDocumentSelect)
        Me.Panel2.Controls.Add(Me.chkDocumentAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(543, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkDocumentSelect
        '
        Me.chkDocumentSelect.Location = New System.Drawing.Point(256, 1)
        Me.chkDocumentSelect.MyLinkLable1 = Nothing
        Me.chkDocumentSelect.MyLinkLable2 = Nothing
        Me.chkDocumentSelect.Name = "chkDocumentSelect"
        Me.chkDocumentSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkDocumentSelect.TabIndex = 1
        Me.chkDocumentSelect.Text = "Select"
        '
        'chkDocumentAll
        '
        Me.chkDocumentAll.Location = New System.Drawing.Point(205, 1)
        Me.chkDocumentAll.MyLinkLable1 = Nothing
        Me.chkDocumentAll.MyLinkLable2 = Nothing
        Me.chkDocumentAll.Name = "chkDocumentAll"
        Me.chkDocumentAll.Size = New System.Drawing.Size(33, 18)
        Me.chkDocumentAll.TabIndex = 0
        Me.chkDocumentAll.Text = "All"
        '
        'dtpcontodate
        '
        Me.dtpcontodate.CalculationExpression = Nothing
        Me.dtpcontodate.CustomFormat = "dd/MM/yyyy"
        Me.dtpcontodate.FieldCode = Nothing
        Me.dtpcontodate.FieldDesc = Nothing
        Me.dtpcontodate.FieldMaxLength = 0
        Me.dtpcontodate.FieldName = Nothing
        Me.dtpcontodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpcontodate.isCalculatedField = False
        Me.dtpcontodate.IsSourceFromTable = False
        Me.dtpcontodate.IsSourceFromValueList = False
        Me.dtpcontodate.IsUnique = False
        Me.dtpcontodate.Location = New System.Drawing.Point(268, 574)
        Me.dtpcontodate.MendatroryField = False
        Me.dtpcontodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpcontodate.MyLinkLable1 = Nothing
        Me.dtpcontodate.MyLinkLable2 = Nothing
        Me.dtpcontodate.Name = "dtpcontodate"
        Me.dtpcontodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpcontodate.ReferenceFieldDesc = Nothing
        Me.dtpcontodate.ReferenceFieldName = Nothing
        Me.dtpcontodate.ReferenceTableName = Nothing
        Me.dtpcontodate.Size = New System.Drawing.Size(87, 20)
        Me.dtpcontodate.TabIndex = 10
        Me.dtpcontodate.TabStop = False
        Me.dtpcontodate.Text = "05/08/2011"
        Me.dtpcontodate.Value = New Date(2011, 8, 5, 17, 38, 42, 656)
        '
        'dtpdatefrom
        '
        Me.dtpdatefrom.CalculationExpression = Nothing
        Me.dtpdatefrom.CustomFormat = "dd/MM/yyyy"
        Me.dtpdatefrom.FieldCode = Nothing
        Me.dtpdatefrom.FieldDesc = Nothing
        Me.dtpdatefrom.FieldMaxLength = 0
        Me.dtpdatefrom.FieldName = Nothing
        Me.dtpdatefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpdatefrom.isCalculatedField = False
        Me.dtpdatefrom.IsSourceFromTable = False
        Me.dtpdatefrom.IsSourceFromValueList = False
        Me.dtpdatefrom.IsUnique = False
        Me.dtpdatefrom.Location = New System.Drawing.Point(140, 534)
        Me.dtpdatefrom.MendatroryField = False
        Me.dtpdatefrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpdatefrom.MyLinkLable1 = Nothing
        Me.dtpdatefrom.MyLinkLable2 = Nothing
        Me.dtpdatefrom.Name = "dtpdatefrom"
        Me.dtpdatefrom.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpdatefrom.ReferenceFieldDesc = Nothing
        Me.dtpdatefrom.ReferenceFieldName = Nothing
        Me.dtpdatefrom.ReferenceTableName = Nothing
        Me.dtpdatefrom.Size = New System.Drawing.Size(87, 20)
        Me.dtpdatefrom.TabIndex = 7
        Me.dtpdatefrom.TabStop = False
        Me.dtpdatefrom.Text = "05/08/2011"
        Me.dtpdatefrom.Value = New Date(2011, 8, 5, 17, 38, 42, 656)
        '
        'dtpdateto
        '
        Me.dtpdateto.CalculationExpression = Nothing
        Me.dtpdateto.CustomFormat = "dd/MM/yyyy"
        Me.dtpdateto.FieldCode = Nothing
        Me.dtpdateto.FieldDesc = Nothing
        Me.dtpdateto.FieldMaxLength = 0
        Me.dtpdateto.FieldName = Nothing
        Me.dtpdateto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpdateto.isCalculatedField = False
        Me.dtpdateto.IsSourceFromTable = False
        Me.dtpdateto.IsSourceFromValueList = False
        Me.dtpdateto.IsUnique = False
        Me.dtpdateto.Location = New System.Drawing.Point(269, 534)
        Me.dtpdateto.MendatroryField = False
        Me.dtpdateto.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpdateto.MyLinkLable1 = Nothing
        Me.dtpdateto.MyLinkLable2 = Nothing
        Me.dtpdateto.Name = "dtpdateto"
        Me.dtpdateto.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpdateto.ReferenceFieldDesc = Nothing
        Me.dtpdateto.ReferenceFieldName = Nothing
        Me.dtpdateto.ReferenceTableName = Nothing
        Me.dtpdateto.Size = New System.Drawing.Size(87, 20)
        Me.dtpdateto.TabIndex = 8
        Me.dtpdateto.TabStop = False
        Me.dtpdateto.Text = "05/08/2011"
        Me.dtpdateto.Value = New Date(2011, 8, 5, 17, 38, 42, 656)
        '
        'dtpconFromdate
        '
        Me.dtpconFromdate.CalculationExpression = Nothing
        Me.dtpconFromdate.CustomFormat = "dd/MM/yyyy"
        Me.dtpconFromdate.FieldCode = Nothing
        Me.dtpconFromdate.FieldDesc = Nothing
        Me.dtpconFromdate.FieldMaxLength = 0
        Me.dtpconFromdate.FieldName = Nothing
        Me.dtpconFromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpconFromdate.isCalculatedField = False
        Me.dtpconFromdate.IsSourceFromTable = False
        Me.dtpconFromdate.IsSourceFromValueList = False
        Me.dtpconFromdate.IsUnique = False
        Me.dtpconFromdate.Location = New System.Drawing.Point(140, 573)
        Me.dtpconFromdate.MendatroryField = False
        Me.dtpconFromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpconFromdate.MyLinkLable1 = Nothing
        Me.dtpconFromdate.MyLinkLable2 = Nothing
        Me.dtpconFromdate.Name = "dtpconFromdate"
        Me.dtpconFromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpconFromdate.ReferenceFieldDesc = Nothing
        Me.dtpconFromdate.ReferenceFieldName = Nothing
        Me.dtpconFromdate.ReferenceTableName = Nothing
        Me.dtpconFromdate.Size = New System.Drawing.Size(87, 20)
        Me.dtpconFromdate.TabIndex = 9
        Me.dtpconFromdate.TabStop = False
        Me.dtpconFromdate.Text = "05/08/2011"
        Me.dtpconFromdate.Value = New Date(2011, 8, 5, 17, 38, 42, 656)
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(13, 596)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 12
        Me.btnReset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(479, 596)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(87, 596)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 11
        Me.btnPrint.Text = "Print"
        '
        'lblConsolidate
        '
        Me.lblConsolidate.FieldName = Nothing
        Me.lblConsolidate.Location = New System.Drawing.Point(19, 573)
        Me.lblConsolidate.Name = "lblConsolidate"
        Me.lblConsolidate.Size = New System.Drawing.Size(65, 18)
        Me.lblConsolidate.TabIndex = 37
        Me.lblConsolidate.Text = "Consolidate"
        '
        'lblToConsolidate
        '
        Me.lblToConsolidate.FieldName = Nothing
        Me.lblToConsolidate.Location = New System.Drawing.Point(293, 556)
        Me.lblToConsolidate.Name = "lblToConsolidate"
        Me.lblToConsolidate.Size = New System.Drawing.Size(19, 18)
        Me.lblToConsolidate.TabIndex = 36
        Me.lblToConsolidate.Text = "To"
        '
        'lblFromConsolidate
        '
        Me.lblFromConsolidate.FieldName = Nothing
        Me.lblFromConsolidate.Location = New System.Drawing.Point(159, 556)
        Me.lblFromConsolidate.Name = "lblFromConsolidate"
        Me.lblFromConsolidate.Size = New System.Drawing.Size(32, 18)
        Me.lblFromConsolidate.TabIndex = 35
        Me.lblFromConsolidate.Text = "From"
        '
        'lblDateRange
        '
        Me.lblDateRange.FieldName = Nothing
        Me.lblDateRange.Location = New System.Drawing.Point(19, 536)
        Me.lblDateRange.Name = "lblDateRange"
        Me.lblDateRange.Size = New System.Drawing.Size(64, 18)
        Me.lblDateRange.TabIndex = 32
        Me.lblDateRange.Text = "Date Range"
        '
        'lblTodateRange
        '
        Me.lblTodateRange.FieldName = Nothing
        Me.lblTodateRange.Location = New System.Drawing.Point(293, 516)
        Me.lblTodateRange.Name = "lblTodateRange"
        Me.lblTodateRange.Size = New System.Drawing.Size(19, 18)
        Me.lblTodateRange.TabIndex = 31
        Me.lblTodateRange.Text = "To"
        '
        'lblFromDateRange
        '
        Me.lblFromDateRange.FieldName = Nothing
        Me.lblFromDateRange.Location = New System.Drawing.Point(158, 516)
        Me.lblFromDateRange.Name = "lblFromDateRange"
        Me.lblFromDateRange.Size = New System.Drawing.Size(32, 18)
        Me.lblFromDateRange.TabIndex = 30
        Me.lblFromDateRange.Text = "From"
        '
        'lblVendorNumber
        '
        Me.lblVendorNumber.FieldName = Nothing
        Me.lblVendorNumber.Location = New System.Drawing.Point(17, 66)
        Me.lblVendorNumber.Name = "lblVendorNumber"
        Me.lblVendorNumber.Size = New System.Drawing.Size(87, 18)
        Me.lblVendorNumber.TabIndex = 22
        Me.lblVendorNumber.Text = "Vendor Number"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rbtnConsolidation)
        Me.RadGroupBox2.Controls.Add(Me.rbtnDuplicate)
        Me.RadGroupBox2.Controls.Add(Me.rbtnOriginal)
        Me.RadGroupBox2.HeaderText = "Select Type"
        Me.RadGroupBox2.Location = New System.Drawing.Point(17, 16)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(337, 40)
        Me.RadGroupBox2.TabIndex = 21
        Me.RadGroupBox2.Text = "Select Type"
        '
        'rbtnConsolidation
        '
        Me.rbtnConsolidation.Location = New System.Drawing.Point(224, 14)
        Me.rbtnConsolidation.Name = "rbtnConsolidation"
        Me.rbtnConsolidation.Size = New System.Drawing.Size(89, 18)
        Me.rbtnConsolidation.TabIndex = 3
        Me.rbtnConsolidation.Text = "Consolidation"
        Me.rbtnConsolidation.Visible = False
        '
        'rbtnDuplicate
        '
        Me.rbtnDuplicate.Location = New System.Drawing.Point(119, 14)
        Me.rbtnDuplicate.Name = "rbtnDuplicate"
        Me.rbtnDuplicate.Size = New System.Drawing.Size(67, 18)
        Me.rbtnDuplicate.TabIndex = 2
        Me.rbtnDuplicate.Text = "Duplicate"
        '
        'rbtnOriginal
        '
        Me.rbtnOriginal.Location = New System.Drawing.Point(13, 15)
        Me.rbtnOriginal.Name = "rbtnOriginal"
        Me.rbtnOriginal.Size = New System.Drawing.Size(60, 18)
        Me.rbtnOriginal.TabIndex = 1
        Me.rbtnOriginal.Text = "Original"
        '
        'FrmForm16A
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 641)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "FrmForm16A"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Form 16A Report"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkDocumentSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDocumentAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpcontodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpdatefrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpdateto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpconFromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConsolidate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToConsolidate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromConsolidate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDateRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTodateRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromDateRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.rbtnConsolidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDuplicate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnOriginal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rbtnConsolidation As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnDuplicate As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnOriginal As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpcontodate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpdatefrom As common.Controls.MyDateTimePicker
    Friend WithEvents dtpdateto As common.Controls.MyDateTimePicker
    Friend WithEvents dtpconFromdate As common.Controls.MyDateTimePicker
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgDocument As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkDocumentSelect As common.Controls.MyRadioButton
    Friend WithEvents chkDocumentAll As common.Controls.MyRadioButton
    Friend WithEvents lblConsolidate As common.Controls.MyLabel
    Friend WithEvents lblFromConsolidate As common.Controls.MyLabel
    Friend WithEvents lblDateRange As common.Controls.MyLabel
    Friend WithEvents lblTodateRange As common.Controls.MyLabel
    Friend WithEvents lblFromDateRange As common.Controls.MyLabel
    Friend WithEvents lblVendorNumber As common.Controls.MyLabel
    Friend WithEvents lblToConsolidate As common.Controls.MyLabel
    Friend WithEvents fndVendorNameNew As common.UserControls.txtFinder
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
End Class

