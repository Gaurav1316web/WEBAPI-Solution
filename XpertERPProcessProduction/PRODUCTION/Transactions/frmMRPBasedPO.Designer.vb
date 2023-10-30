<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMRPBasedPO
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.dtptodate = New common.Controls.MyDateTimePicker()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.dtpfromdate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtLocationcode = New common.UserControls.txtFinder()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.txtLocationdesc = New common.Controls.MyLabel()
        Me.txtMRPNo = New common.UserControls.txtFinder()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.txtMrpDesc = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv_PO = New common.UserControls.MyRadGridView()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvMRP = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.chkGenAutoSchedule = New common.Controls.MyCheckBox()
        Me.chkConsiderOpenPO = New common.Controls.MyCheckBox()
        Me.chkAutoPO = New common.Controls.MyRadioButton()
        Me.chkAutoIndent = New common.Controls.MyRadioButton()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocationdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMrpDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv_PO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_PO.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.gvMRP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvMRP.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGenAutoSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkConsiderOpenPO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAutoPO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAutoIndent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(809, 527)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer1)
        Me.RadPageViewPage1.Controls.Add(Me.Panel1)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(160.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(788, 481)
        Me.RadPageViewPage1.Text = "MRP Based Purchase Order"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkGenAutoSchedule)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkConsiderOpenPO)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkAutoPO)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkAutoIndent)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtptodate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDocNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpfromdate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocationcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMRPNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMrpDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocationdesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel15)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadGroupBox5)
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(1)
        Me.SplitContainer1.Size = New System.Drawing.Size(788, 453)
        Me.SplitContainer1.SplitterDistance = 103
        Me.SplitContainer1.TabIndex = 63
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(11, 15)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(75, 16)
        Me.RadLabel1.TabIndex = 39
        Me.RadLabel1.Text = "Document No"
        Me.RadLabel1.Visible = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(179, 82)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel3.TabIndex = 62
        Me.MyLabel3.Text = "To Date"
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(685, 12)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(97, 19)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 51
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPProcessProduction.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(348, 13)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(16, 20)
        Me.btnAddNew.TabIndex = 0
        Me.btnAddNew.Visible = False
        '
        'dtptodate
        '
        Me.dtptodate.CalculationExpression = Nothing
        Me.dtptodate.CustomFormat = "dd/MM/yyyy"
        Me.dtptodate.FieldCode = Nothing
        Me.dtptodate.FieldDesc = Nothing
        Me.dtptodate.FieldMaxLength = 0
        Me.dtptodate.FieldName = Nothing
        Me.dtptodate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtptodate.isCalculatedField = False
        Me.dtptodate.IsSourceFromTable = False
        Me.dtptodate.IsSourceFromValueList = False
        Me.dtptodate.IsUnique = False
        Me.dtptodate.Location = New System.Drawing.Point(240, 80)
        Me.dtptodate.MendatroryField = False
        Me.dtptodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.MyLinkLable1 = Me.MyLabel3
        Me.dtptodate.MyLinkLable2 = Nothing
        Me.dtptodate.Name = "dtptodate"
        Me.dtptodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.ReferenceFieldDesc = Nothing
        Me.dtptodate.ReferenceFieldName = Nothing
        Me.dtptodate.ReferenceTableName = Nothing
        Me.dtptodate.Size = New System.Drawing.Size(78, 18)
        Me.dtptodate.TabIndex = 5
        Me.dtptodate.TabStop = False
        Me.dtptodate.Text = "13/06/2011"
        Me.dtptodate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(96, 13)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 19)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.Value = ""
        Me.txtDocNo.Visible = False
        '
        'dtpfromdate
        '
        Me.dtpfromdate.CalculationExpression = Nothing
        Me.dtpfromdate.CustomFormat = "dd/MM/yyyy"
        Me.dtpfromdate.FieldCode = Nothing
        Me.dtpfromdate.FieldDesc = Nothing
        Me.dtpfromdate.FieldMaxLength = 0
        Me.dtpfromdate.FieldName = Nothing
        Me.dtpfromdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfromdate.isCalculatedField = False
        Me.dtpfromdate.IsSourceFromTable = False
        Me.dtpfromdate.IsSourceFromValueList = False
        Me.dtpfromdate.IsUnique = False
        Me.dtpfromdate.Location = New System.Drawing.Point(96, 80)
        Me.dtpfromdate.MendatroryField = False
        Me.dtpfromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate.MyLinkLable1 = Me.MyLabel1
        Me.dtpfromdate.MyLinkLable2 = Nothing
        Me.dtpfromdate.Name = "dtpfromdate"
        Me.dtpfromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate.ReferenceFieldDesc = Nothing
        Me.dtpfromdate.ReferenceFieldName = Nothing
        Me.dtpfromdate.ReferenceTableName = Nothing
        Me.dtpfromdate.Size = New System.Drawing.Size(81, 18)
        Me.dtpfromdate.TabIndex = 4
        Me.dtpfromdate.TabStop = False
        Me.dtpfromdate.Text = "13/06/2011"
        Me.dtpfromdate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(11, 81)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel1.TabIndex = 60
        Me.MyLabel1.Text = "From Date"
        '
        'txtLocationcode
        '
        Me.txtLocationcode.CalculationExpression = Nothing
        Me.txtLocationcode.FieldCode = Nothing
        Me.txtLocationcode.FieldDesc = Nothing
        Me.txtLocationcode.FieldMaxLength = 0
        Me.txtLocationcode.FieldName = Nothing
        Me.txtLocationcode.isCalculatedField = False
        Me.txtLocationcode.IsSourceFromTable = False
        Me.txtLocationcode.IsSourceFromValueList = False
        Me.txtLocationcode.IsUnique = False
        Me.txtLocationcode.Location = New System.Drawing.Point(96, 59)
        Me.txtLocationcode.MendatroryField = True
        Me.txtLocationcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationcode.MyLinkLable1 = Me.RadLabel15
        Me.txtLocationcode.MyLinkLable2 = Me.txtLocationdesc
        Me.txtLocationcode.MyReadOnly = False
        Me.txtLocationcode.MyShowMasterFormButton = False
        Me.txtLocationcode.Name = "txtLocationcode"
        Me.txtLocationcode.ReferenceFieldDesc = Nothing
        Me.txtLocationcode.ReferenceFieldName = Nothing
        Me.txtLocationcode.ReferenceTableName = Nothing
        Me.txtLocationcode.Size = New System.Drawing.Size(143, 17)
        Me.txtLocationcode.TabIndex = 3
        Me.txtLocationcode.Value = ""
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(11, 60)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 37
        Me.RadLabel15.Text = "Location"
        '
        'txtLocationdesc
        '
        Me.txtLocationdesc.AutoSize = False
        Me.txtLocationdesc.BorderVisible = True
        Me.txtLocationdesc.FieldName = Nothing
        Me.txtLocationdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationdesc.Location = New System.Drawing.Point(240, 59)
        Me.txtLocationdesc.Name = "txtLocationdesc"
        Me.txtLocationdesc.Size = New System.Drawing.Size(287, 18)
        Me.txtLocationdesc.TabIndex = 55
        Me.txtLocationdesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtLocationdesc.TextWrap = False
        '
        'txtMRPNo
        '
        Me.txtMRPNo.CalculationExpression = Nothing
        Me.txtMRPNo.FieldCode = Nothing
        Me.txtMRPNo.FieldDesc = Nothing
        Me.txtMRPNo.FieldMaxLength = 0
        Me.txtMRPNo.FieldName = Nothing
        Me.txtMRPNo.isCalculatedField = False
        Me.txtMRPNo.IsSourceFromTable = False
        Me.txtMRPNo.IsSourceFromValueList = False
        Me.txtMRPNo.IsUnique = False
        Me.txtMRPNo.Location = New System.Drawing.Point(96, 37)
        Me.txtMRPNo.MendatroryField = True
        Me.txtMRPNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRPNo.MyLinkLable1 = Me.RadLabel2
        Me.txtMRPNo.MyLinkLable2 = Me.txtMrpDesc
        Me.txtMRPNo.MyReadOnly = False
        Me.txtMRPNo.MyShowMasterFormButton = False
        Me.txtMRPNo.Name = "txtMRPNo"
        Me.txtMRPNo.ReferenceFieldDesc = Nothing
        Me.txtMRPNo.ReferenceFieldName = Nothing
        Me.txtMRPNo.ReferenceTableName = Nothing
        Me.txtMRPNo.Size = New System.Drawing.Size(143, 17)
        Me.txtMRPNo.TabIndex = 2
        Me.txtMRPNo.Value = ""
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(11, 38)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel2.TabIndex = 38
        Me.RadLabel2.Text = "MRP No"
        '
        'txtMrpDesc
        '
        Me.txtMrpDesc.AutoSize = False
        Me.txtMrpDesc.BorderVisible = True
        Me.txtMrpDesc.FieldName = Nothing
        Me.txtMrpDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMrpDesc.Location = New System.Drawing.Point(240, 37)
        Me.txtMrpDesc.Name = "txtMrpDesc"
        Me.txtMrpDesc.Size = New System.Drawing.Size(287, 18)
        Me.txtMrpDesc.TabIndex = 56
        Me.txtMrpDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtMrpDesc.TextWrap = False
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
        Me.txtDate.Location = New System.Drawing.Point(404, 14)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(123, 18)
        Me.txtDate.TabIndex = 1
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        Me.txtDate.Visible = False
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(369, 15)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 40
        Me.RadLabel4.Text = "Date"
        Me.RadLabel4.Visible = False
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gv_PO)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "PO Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(1, 134)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(786, 211)
        Me.RadGroupBox2.TabIndex = 1
        Me.RadGroupBox2.Text = "PO Details"
        '
        'gv_PO
        '
        Me.gv_PO.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv_PO.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv_PO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv_PO.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv_PO.ForeColor = System.Drawing.Color.Black
        Me.gv_PO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv_PO.Location = New System.Drawing.Point(10, 20)
        '
        'gv_PO
        '
        Me.gv_PO.MasterTemplate.AllowDeleteRow = False
        Me.gv_PO.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv_PO.Name = "gv_PO"
        Me.gv_PO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv_PO.ShowGroupPanel = False
        Me.gv_PO.ShowHeaderCellButtons = True
        Me.gv_PO.Size = New System.Drawing.Size(766, 181)
        Me.gv_PO.TabIndex = 0
        Me.gv_PO.TabStop = False
        Me.gv_PO.Text = "RadGridView1"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.gvMRP)
        Me.RadGroupBox5.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadGroupBox5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox5.HeaderText = "MRP Item Details"
        Me.RadGroupBox5.Location = New System.Drawing.Point(1, 1)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(786, 133)
        Me.RadGroupBox5.TabIndex = 0
        Me.RadGroupBox5.Text = "MRP Item Details"
        '
        'gvMRP
        '
        Me.gvMRP.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvMRP.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvMRP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvMRP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvMRP.ForeColor = System.Drawing.Color.Black
        Me.gvMRP.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvMRP.Location = New System.Drawing.Point(10, 20)
        '
        'gvMRP
        '
        Me.gvMRP.MasterTemplate.AllowDeleteRow = False
        Me.gvMRP.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvMRP.Name = "gvMRP"
        Me.gvMRP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvMRP.ShowGroupPanel = False
        Me.gvMRP.ShowHeaderCellButtons = True
        Me.gvMRP.Size = New System.Drawing.Size(766, 103)
        Me.gvMRP.TabIndex = 0
        Me.gvMRP.TabStop = False
        Me.gvMRP.Text = "RadGridView1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnclose)
        Me.Panel1.Controls.Add(Me.btnsave)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 453)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(788, 28)
        Me.Panel1.TabIndex = 64
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(704, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(78, 21)
        Me.btnclose.TabIndex = 1
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(6, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(78, 21)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'chkGenAutoSchedule
        '
        Me.chkGenAutoSchedule.Enabled = False
        Me.chkGenAutoSchedule.Location = New System.Drawing.Point(448, 79)
        Me.chkGenAutoSchedule.MyLinkLable1 = Nothing
        Me.chkGenAutoSchedule.MyLinkLable2 = Nothing
        Me.chkGenAutoSchedule.Name = "chkGenAutoSchedule"
        Me.chkGenAutoSchedule.Size = New System.Drawing.Size(141, 18)
        Me.chkGenAutoSchedule.TabIndex = 285
        Me.chkGenAutoSchedule.Tag1 = Nothing
        Me.chkGenAutoSchedule.Text = "Generate Auto Schedule"
        '
        'chkConsiderOpenPO
        '
        Me.chkConsiderOpenPO.Enabled = False
        Me.chkConsiderOpenPO.Location = New System.Drawing.Point(329, 79)
        Me.chkConsiderOpenPO.MyLinkLable1 = Nothing
        Me.chkConsiderOpenPO.MyLinkLable2 = Nothing
        Me.chkConsiderOpenPO.Name = "chkConsiderOpenPO"
        Me.chkConsiderOpenPO.Size = New System.Drawing.Size(113, 18)
        Me.chkConsiderOpenPO.TabIndex = 284
        Me.chkConsiderOpenPO.Tag1 = Nothing
        Me.chkConsiderOpenPO.Text = "Consider Open PO"
        '
        'chkAutoPO
        '
        Me.chkAutoPO.Enabled = False
        Me.chkAutoPO.Location = New System.Drawing.Point(615, 79)
        Me.chkAutoPO.MyLinkLable1 = Nothing
        Me.chkAutoPO.MyLinkLable2 = Nothing
        Me.chkAutoPO.Name = "chkAutoPO"
        Me.chkAutoPO.Size = New System.Drawing.Size(111, 18)
        Me.chkAutoPO.TabIndex = 283
        Me.chkAutoPO.Text = "Generate Auto PO"
        '
        'chkAutoIndent
        '
        Me.chkAutoIndent.Enabled = False
        Me.chkAutoIndent.Location = New System.Drawing.Point(615, 58)
        Me.chkAutoIndent.MyLinkLable1 = Nothing
        Me.chkAutoIndent.MyLinkLable2 = Nothing
        Me.chkAutoIndent.Name = "chkAutoIndent"
        Me.chkAutoIndent.Size = New System.Drawing.Size(128, 18)
        Me.chkAutoIndent.TabIndex = 282
        Me.chkAutoIndent.Text = "Generate Auto Indent"
        '
        'FrmMRPBasedPO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(809, 527)
        Me.Controls.Add(Me.RadPageView1)
        Me.Name = "FrmMRPBasedPO"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "MRP Based PO"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocationdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMrpDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv_PO.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_PO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        CType(Me.gvMRP.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvMRP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGenAutoSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkConsiderOpenPO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAutoPO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAutoIndent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents dtpfromdate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvMRP As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv_PO As common.UserControls.MyRadGridView
    Friend WithEvents UcItemBalance1 As XpertERPEngine.ucItemBalance
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents txtLocationdesc As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents txtMrpDesc As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtMRPNo As common.UserControls.txtFinder
    Friend WithEvents txtLocationcode As common.UserControls.txtFinder
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    ' Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents UcCustomFields1 As ucCustomFields
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents dtptodate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkGenAutoSchedule As common.Controls.MyCheckBox
    Friend WithEvents chkConsiderOpenPO As common.Controls.MyCheckBox
    Friend WithEvents chkAutoPO As common.Controls.MyRadioButton
    Friend WithEvents chkAutoIndent As common.Controls.MyRadioButton
End Class

