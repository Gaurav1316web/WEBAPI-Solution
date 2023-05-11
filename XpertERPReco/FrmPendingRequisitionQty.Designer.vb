Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPendingRequisitionQty
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
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.dtpTodate = New common.Controls.MyDateTimePicker()
        Me.dtpfromdate = New common.Controls.MyDateTimePicker()
        Me.rdoPonever = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.lblstatus = New System.Windows.Forms.Label()
        Me.rdoCompleted = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdobtnall = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdoPOPartial = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgLocation = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkLocationSelect = New common.Controls.MyRadioButton()
        Me.chkLocationAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgVendor = New common.MyCheckBoxGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkVendor_select = New common.Controls.MyRadioButton()
        Me.chkVendor_all = New common.Controls.MyRadioButton()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgDocument = New common.MyCheckBoxGrid()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.chkDoc_select = New common.Controls.MyRadioButton()
        Me.chkdocAll = New common.Controls.MyRadioButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnprint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtItem = New common.UserControls.txtMultiSelectFinder()
        Me.lblVendor = New common.Controls.MyLabel()
        Me.lblDocNo = New common.Controls.MyLabel()
        Me.txtVendor = New common.UserControls.txtMultiSelectFinder()
        Me.txtDocNo = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.BtnQuickExport = New Telerik.WinControls.UI.RadButton()
        Me.btnExcel = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.IndentTypePurchase = New Telerik.WinControls.UI.RadRadioButton()
        Me.IndentTypeBoth = New Telerik.WinControls.UI.RadRadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.IndentTypeStore = New Telerik.WinControls.UI.RadRadioButton()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpTodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdoPonever, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdoCompleted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdobtnall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdoPOPartial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkVendor_select, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendor_all, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkDoc_select, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkdocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnQuickExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.IndentTypePurchase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndentTypeBoth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndentTypeStore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox5.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox5.Controls.Add(Me.dtpTodate)
        Me.RadGroupBox5.Controls.Add(Me.dtpfromdate)
        Me.RadGroupBox5.Controls.Add(Me.rdoPonever)
        Me.RadGroupBox5.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox5.Controls.Add(Me.lblstatus)
        Me.RadGroupBox5.Controls.Add(Me.rdoCompleted)
        Me.RadGroupBox5.Controls.Add(Me.rdobtnall)
        Me.RadGroupBox5.Controls.Add(Me.rdoPOPartial)
        Me.RadGroupBox5.HeaderText = ""
        Me.RadGroupBox5.Location = New System.Drawing.Point(13, 9)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(440, 69)
        Me.RadGroupBox5.TabIndex = 8
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Location = New System.Drawing.Point(10, 10)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel4.TabIndex = 8
        Me.RadLabel4.Text = "From Date"
        '
        'dtpTodate
        '
        Me.dtpTodate.CalculationExpression = Nothing
        Me.dtpTodate.CustomFormat = "dd-MM-yyyy"
        Me.dtpTodate.FieldCode = Nothing
        Me.dtpTodate.FieldDesc = Nothing
        Me.dtpTodate.FieldMaxLength = 0
        Me.dtpTodate.FieldName = Nothing
        Me.dtpTodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTodate.isCalculatedField = False
        Me.dtpTodate.IsSourceFromTable = False
        Me.dtpTodate.IsSourceFromValueList = False
        Me.dtpTodate.IsUnique = False
        Me.dtpTodate.Location = New System.Drawing.Point(347, 10)
        Me.dtpTodate.MendatroryField = False
        Me.dtpTodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTodate.MyLinkLable1 = Nothing
        Me.dtpTodate.MyLinkLable2 = Nothing
        Me.dtpTodate.Name = "dtpTodate"
        Me.dtpTodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTodate.ReferenceFieldDesc = Nothing
        Me.dtpTodate.ReferenceFieldName = Nothing
        Me.dtpTodate.ReferenceTableName = Nothing
        Me.dtpTodate.Size = New System.Drawing.Size(82, 20)
        Me.dtpTodate.TabIndex = 1
        Me.dtpTodate.TabStop = False
        Me.dtpTodate.Text = "14-09-2011"
        Me.dtpTodate.Value = New Date(2011, 9, 14, 0, 0, 0, 0)
        '
        'dtpfromdate
        '
        Me.dtpfromdate.CalculationExpression = Nothing
        Me.dtpfromdate.CustomFormat = "dd-MM-yyyy"
        Me.dtpfromdate.FieldCode = Nothing
        Me.dtpfromdate.FieldDesc = Nothing
        Me.dtpfromdate.FieldMaxLength = 0
        Me.dtpfromdate.FieldName = Nothing
        Me.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfromdate.isCalculatedField = False
        Me.dtpfromdate.IsSourceFromTable = False
        Me.dtpfromdate.IsSourceFromValueList = False
        Me.dtpfromdate.IsUnique = False
        Me.dtpfromdate.Location = New System.Drawing.Point(73, 9)
        Me.dtpfromdate.MendatroryField = False
        Me.dtpfromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate.MyLinkLable1 = Nothing
        Me.dtpfromdate.MyLinkLable2 = Nothing
        Me.dtpfromdate.Name = "dtpfromdate"
        Me.dtpfromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate.ReferenceFieldDesc = Nothing
        Me.dtpfromdate.ReferenceFieldName = Nothing
        Me.dtpfromdate.ReferenceTableName = Nothing
        Me.dtpfromdate.Size = New System.Drawing.Size(82, 20)
        Me.dtpfromdate.TabIndex = 0
        Me.dtpfromdate.TabStop = False
        Me.dtpfromdate.Text = "14-09-2011"
        Me.dtpfromdate.Value = New Date(2011, 9, 14, 0, 0, 0, 0)
        '
        'rdoPonever
        '
        Me.rdoPonever.Location = New System.Drawing.Point(113, 45)
        Me.rdoPonever.Name = "rdoPonever"
        Me.rdoPonever.Size = New System.Drawing.Size(110, 18)
        Me.rdoPonever.TabIndex = 12
        Me.rdoPonever.TabStop = False
        Me.rdoPonever.Text = "PO Never Created"
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(296, 10)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel3.TabIndex = 9
        Me.RadLabel3.Text = "To Date"
        '
        'lblstatus
        '
        Me.lblstatus.AutoSize = True
        Me.lblstatus.Location = New System.Drawing.Point(9, 45)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(39, 13)
        Me.lblstatus.TabIndex = 10
        Me.lblstatus.Text = "Status"
        '
        'rdoCompleted
        '
        Me.rdoCompleted.Location = New System.Drawing.Point(349, 45)
        Me.rdoCompleted.Name = "rdoCompleted"
        Me.rdoCompleted.Size = New System.Drawing.Size(75, 18)
        Me.rdoCompleted.TabIndex = 12
        Me.rdoCompleted.TabStop = False
        Me.rdoCompleted.Text = "Completed"
        '
        'rdobtnall
        '
        Me.rdobtnall.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdobtnall.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdobtnall.Location = New System.Drawing.Point(54, 45)
        Me.rdobtnall.Name = "rdobtnall"
        Me.rdobtnall.Size = New System.Drawing.Size(33, 18)
        Me.rdobtnall.TabIndex = 11
        Me.rdobtnall.Text = "All"
        Me.rdobtnall.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rdoPOPartial
        '
        Me.rdoPOPartial.Location = New System.Drawing.Point(231, 45)
        Me.rdoPOPartial.Name = "rdoPOPartial"
        Me.rdoPOPartial.Size = New System.Drawing.Size(111, 18)
        Me.rdoPOPartial.TabIndex = 12
        Me.rdoPOPartial.TabStop = False
        Me.rdoPOPartial.Text = "PO Partial Created"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.HeaderText = " Location"
        Me.RadGroupBox1.Location = New System.Drawing.Point(770, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(136, 57)
        Me.RadGroupBox1.TabIndex = 71
        Me.RadGroupBox1.Text = " Location"
        Me.RadGroupBox1.Visible = False
        '
        'cbgLocation
        '
        Me.cbgLocation.AccessibleDescription = "cbgLocation"
        Me.cbgLocation.AccessibleName = ""
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 45)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(116, 2)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkLocationSelect)
        Me.Panel1.Controls.Add(Me.chkLocationAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(116, 25)
        Me.Panel1.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.AccessibleDescription = "chkDoc_select"
        Me.chkLocationSelect.Location = New System.Drawing.Point(192, 3)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.AccessibleDescription = "chkdocAll"
        Me.chkLocationAll.Location = New System.Drawing.Point(141, 3)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgVendor)
        Me.RadGroupBox4.Controls.Add(Me.Panel3)
        Me.RadGroupBox4.HeaderText = "Vendor"
        Me.RadGroupBox4.Location = New System.Drawing.Point(780, 75)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(71, 56)
        Me.RadGroupBox4.TabIndex = 4
        Me.RadGroupBox4.Text = "Vendor"
        Me.RadGroupBox4.Visible = False
        '
        'cbgVendor
        '
        Me.cbgVendor.AccessibleName = ""
        Me.cbgVendor.CheckedValue = Nothing
        Me.cbgVendor.DataSource = Nothing
        Me.cbgVendor.DisplayMember = "Name"
        Me.cbgVendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVendor.Location = New System.Drawing.Point(10, 40)
        Me.cbgVendor.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVendor.MyShowHeadrText = False
        Me.cbgVendor.Name = "cbgVendor"
        Me.cbgVendor.Size = New System.Drawing.Size(51, 6)
        Me.cbgVendor.TabIndex = 1
        Me.cbgVendor.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkVendor_select)
        Me.Panel3.Controls.Add(Me.chkVendor_all)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(51, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkVendor_select
        '
        Me.chkVendor_select.Location = New System.Drawing.Point(192, 1)
        Me.chkVendor_select.MyLinkLable1 = Nothing
        Me.chkVendor_select.MyLinkLable2 = Nothing
        Me.chkVendor_select.Name = "chkVendor_select"
        Me.chkVendor_select.Size = New System.Drawing.Size(50, 18)
        Me.chkVendor_select.TabIndex = 1
        Me.chkVendor_select.Text = "Select"
        '
        'chkVendor_all
        '
        Me.chkVendor_all.Location = New System.Drawing.Point(141, 1)
        Me.chkVendor_all.MyLinkLable1 = Nothing
        Me.chkVendor_all.MyLinkLable2 = Nothing
        Me.chkVendor_all.Name = "chkVendor_all"
        Me.chkVendor_all.Size = New System.Drawing.Size(33, 18)
        Me.chkVendor_all.TabIndex = 0
        Me.chkVendor_all.Text = "All"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.cbgDocument)
        Me.RadGroupBox6.Controls.Add(Me.Panel4)
        Me.RadGroupBox6.HeaderText = "Document No"
        Me.RadGroupBox6.Location = New System.Drawing.Point(649, 3)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(115, 78)
        Me.RadGroupBox6.TabIndex = 3
        Me.RadGroupBox6.Text = "Document No"
        Me.RadGroupBox6.Visible = False
        '
        'cbgDocument
        '
        Me.cbgDocument.AccessibleName = ""
        Me.cbgDocument.CheckedValue = Nothing
        Me.cbgDocument.DataSource = Nothing
        Me.cbgDocument.DisplayMember = "Name"
        Me.cbgDocument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgDocument.Location = New System.Drawing.Point(10, 40)
        Me.cbgDocument.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgDocument.MyShowHeadrText = False
        Me.cbgDocument.Name = "cbgDocument"
        Me.cbgDocument.Size = New System.Drawing.Size(95, 28)
        Me.cbgDocument.TabIndex = 1
        Me.cbgDocument.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkDoc_select)
        Me.Panel4.Controls.Add(Me.chkdocAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(95, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkDoc_select
        '
        Me.chkDoc_select.Location = New System.Drawing.Point(192, 1)
        Me.chkDoc_select.MyLinkLable1 = Nothing
        Me.chkDoc_select.MyLinkLable2 = Nothing
        Me.chkDoc_select.Name = "chkDoc_select"
        Me.chkDoc_select.Size = New System.Drawing.Size(50, 18)
        Me.chkDoc_select.TabIndex = 1
        Me.chkDoc_select.Text = "Select"
        '
        'chkdocAll
        '
        Me.chkdocAll.Location = New System.Drawing.Point(141, 1)
        Me.chkdocAll.MyLinkLable1 = Nothing
        Me.chkdocAll.MyLinkLable2 = Nothing
        Me.chkdocAll.Name = "chkdocAll"
        Me.chkdocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkdocAll.TabIndex = 0
        Me.chkdocAll.Text = "All"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(854, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 7
        Me.btnclose.Text = "Close"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(85, 6)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 18)
        Me.btnreset.TabIndex = 6
        Me.btnreset.Text = "&Reset"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(156, 6)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(68, 18)
        Me.btnprint.TabIndex = 5
        Me.btnprint.Text = "Print"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnQuickExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExcel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(930, 596)
        Me.SplitContainer1.SplitterDistance = 565
        Me.SplitContainer1.TabIndex = 9
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        '
        '
        '
        Me.RadPageView1.RootElement.AccessibleDescription = "Report1"
        Me.RadPageView1.RootElement.AccessibleName = "Report1"
        Me.RadPageView1.RootElement.Text = "Report1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(930, 565)
        Me.RadPageView1.TabIndex = 3
        Me.RadPageView1.TabStop = False
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(2), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(3), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox6)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(909, 517)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox2.Controls.Add(Me.GroupBox1)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox2.Controls.Add(Me.txtItem)
        Me.RadGroupBox2.Controls.Add(Me.RadGroupBox5)
        Me.RadGroupBox2.Controls.Add(Me.lblVendor)
        Me.RadGroupBox2.Controls.Add(Me.lblDocNo)
        Me.RadGroupBox2.Controls.Add(Me.txtVendor)
        Me.RadGroupBox2.Controls.Add(Me.txtDocNo)
        Me.RadGroupBox2.Controls.Add(Me.lblLocation)
        Me.RadGroupBox2.Controls.Add(Me.txtLocation)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 3)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(906, 511)
        Me.RadGroupBox2.TabIndex = 331
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(13, 205)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel4.TabIndex = 332
        Me.MyLabel4.Text = "Item"
        '
        'txtItem
        '
        Me.txtItem.arrDispalyMember = Nothing
        Me.txtItem.arrValueMember = Nothing
        Me.txtItem.Location = New System.Drawing.Point(99, 205)
        Me.txtItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.MyLinkLable1 = Me.MyLabel4
        Me.txtItem.MyLinkLable2 = Nothing
        Me.txtItem.MyNullText = "All"
        Me.txtItem.Name = "txtItem"
        Me.txtItem.Size = New System.Drawing.Size(343, 19)
        Me.txtItem.TabIndex = 331
        '
        'lblVendor
        '
        Me.lblVendor.FieldName = Nothing
        Me.lblVendor.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendor.Location = New System.Drawing.Point(13, 181)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Size = New System.Drawing.Size(43, 18)
        Me.lblVendor.TabIndex = 330
        Me.lblVendor.Text = "Vendor"
        '
        'lblDocNo
        '
        Me.lblDocNo.FieldName = Nothing
        Me.lblDocNo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocNo.Location = New System.Drawing.Point(13, 133)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(79, 18)
        Me.lblDocNo.TabIndex = 326
        Me.lblDocNo.Text = "Document No."
        '
        'txtVendor
        '
        Me.txtVendor.arrDispalyMember = Nothing
        Me.txtVendor.arrValueMember = Nothing
        Me.txtVendor.Location = New System.Drawing.Point(98, 180)
        Me.txtVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendor.MyLinkLable1 = Me.lblVendor
        Me.txtVendor.MyLinkLable2 = Nothing
        Me.txtVendor.MyNullText = "All"
        Me.txtVendor.Name = "txtVendor"
        Me.txtVendor.Size = New System.Drawing.Size(344, 19)
        Me.txtVendor.TabIndex = 329
        '
        'txtDocNo
        '
        Me.txtDocNo.arrDispalyMember = Nothing
        Me.txtDocNo.arrValueMember = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(98, 132)
        Me.txtDocNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocNo.MyLinkLable1 = Me.lblDocNo
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyNullText = "All"
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(344, 19)
        Me.txtDocNo.TabIndex = 325
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(13, 157)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 328
        Me.lblLocation.Text = "Location"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(98, 155)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblLocation
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(344, 19)
        Me.txtLocation.TabIndex = 327
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(909, 517)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(909, 517)
        Me.gv.TabIndex = 3
        Me.gv.Text = "gv"
        '
        'BtnQuickExport
        '
        Me.BtnQuickExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnQuickExport.Location = New System.Drawing.Point(329, 6)
        Me.BtnQuickExport.Name = "BtnQuickExport"
        Me.BtnQuickExport.Size = New System.Drawing.Size(93, 18)
        Me.BtnQuickExport.TabIndex = 10
        Me.BtnQuickExport.Text = "Quick Export"
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExcel.Location = New System.Drawing.Point(230, 6)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(93, 18)
        Me.btnExcel.TabIndex = 9
        Me.btnExcel.Text = "Export To Excel"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Location = New System.Drawing.Point(13, 6)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(68, 18)
        Me.btnGo.TabIndex = 8
        Me.btnGo.Text = ">>"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(930, 20)
        Me.RadMenu1.TabIndex = 10
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.IndentTypeStore)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.IndentTypePurchase)
        Me.GroupBox1.Controls.Add(Me.IndentTypeBoth)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 80)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(440, 32)
        Me.GroupBox1.TabIndex = 333
        Me.GroupBox1.TabStop = False
        '
        'IndentTypePurchase
        '
        Me.IndentTypePurchase.Location = New System.Drawing.Point(145, 9)
        Me.IndentTypePurchase.Name = "IndentTypePurchase"
        Me.IndentTypePurchase.Size = New System.Drawing.Size(100, 18)
        Me.IndentTypePurchase.TabIndex = 1
        Me.IndentTypePurchase.Text = "Purchase Indent"
        '
        'IndentTypeBoth
        '
        Me.IndentTypeBoth.CheckState = System.Windows.Forms.CheckState.Checked
        Me.IndentTypeBoth.Location = New System.Drawing.Point(86, 9)
        Me.IndentTypeBoth.Name = "IndentTypeBoth"
        Me.IndentTypeBoth.Size = New System.Drawing.Size(44, 18)
        Me.IndentTypeBoth.TabIndex = 0
        Me.IndentTypeBoth.Text = "Both"
        Me.IndentTypeBoth.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 334
        Me.Label1.Text = "Indent Type"
        '
        'IndentTypeStore
        '
        Me.IndentTypeStore.Location = New System.Drawing.Point(250, 10)
        Me.IndentTypeStore.Name = "IndentTypeStore"
        Me.IndentTypeStore.Size = New System.Drawing.Size(105, 18)
        Me.IndentTypeStore.TabIndex = 334
        Me.IndentTypeStore.Text = "Store Requisition"
        '
        'FrmPendingRequisitionQty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(930, 616)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmPendingRequisitionQty"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Pending Requistion"
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpTodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdoPonever, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdoCompleted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdobtnall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdoPOPartial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkVendor_select, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendor_all, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkDoc_select, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkdocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnQuickExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.IndentTypePurchase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndentTypeBoth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndentTypeStore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblstatus As System.Windows.Forms.Label
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVendor As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkVendor_select As common.Controls.MyRadioButton
    Friend WithEvents chkVendor_all As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgDocument As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkDoc_select As common.Controls.MyRadioButton
    Friend WithEvents chkdocAll As common.Controls.MyRadioButton
    Friend WithEvents dtpTodate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpfromdate As common.Controls.MyDateTimePicker
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdoPonever As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdobtnall As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdoCompleted As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdoPOPartial As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExcel As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblVendor As common.Controls.MyLabel
    Friend WithEvents txtVendor As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblDocNo As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents BtnQuickExport As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtItem As common.UserControls.txtMultiSelectFinder
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents IndentTypeStore As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents IndentTypePurchase As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents IndentTypeBoth As Telerik.WinControls.UI.RadRadioButton
End Class

