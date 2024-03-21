<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSectionStageMapping
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnexportExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnimportExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.txtdate = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtsec_code = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtdesc = New common.Controls.MyLabel()
        Me.txtcategorycode = New common.UserControls.txtFinder()
        Me.lblMasterItem = New common.Controls.MyLabel()
        Me.txtcategoryname = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.fndCode = New common.UserControls.txtNavigator()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btngridclose = New Telerik.WinControls.UI.RadButton()
        Me.gv_user = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnIMDetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnIMUser = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.RadGridView1 = New common.UserControls.MyRadGridView()
        Me.btnEXDetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnEXUser = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMasterItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcategoryname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gv1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.btngridclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_user, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_user.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(658, 456)
        Me.SplitContainer1.SplitterDistance = 420
        Me.SplitContainer1.TabIndex = 0
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtdate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtsec_code)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtcategorycode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtcategoryname)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblMasterItem)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtdesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnNew)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer2.Size = New System.Drawing.Size(658, 420)
        Me.SplitContainer2.SplitterDistance = 115
        Me.SplitContainer2.TabIndex = 24
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(658, 20)
        Me.RadMenu1.TabIndex = 47
        Me.RadMenu1.Visible = False
        '
        'rdmenufile
        '
        Me.rdmenufile.AccessibleDescription = "RadMenuItem1"
        Me.rdmenufile.AccessibleName = "RadMenuItem1"
        Me.rdmenufile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexportExcel, Me.btnimportExcel})
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Text = "File"
        '
        'btnexportExcel
        '
        Me.btnexportExcel.Name = "btnexportExcel"
        Me.btnexportExcel.Text = "Export Excel"
        '
        'btnimportExcel
        '
        Me.btnimportExcel.Name = "btnimportExcel"
        Me.btnimportExcel.Text = "Import Excel"
        '
        'txtdate
        '
        Me.txtdate.CalculationExpression = Nothing
        Me.txtdate.CustomFormat = "dd/MM/yyyy"
        Me.txtdate.FieldCode = Nothing
        Me.txtdate.FieldDesc = Nothing
        Me.txtdate.FieldMaxLength = 0
        Me.txtdate.FieldName = Nothing
        Me.txtdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtdate.isCalculatedField = False
        Me.txtdate.IsSourceFromTable = False
        Me.txtdate.IsSourceFromValueList = False
        Me.txtdate.IsUnique = False
        Me.txtdate.Location = New System.Drawing.Point(521, 42)
        Me.txtdate.MendatroryField = False
        Me.txtdate.MyLinkLable1 = Me.MyLabel2
        Me.txtdate.MyLinkLable2 = Nothing
        Me.txtdate.Name = "txtdate"
        Me.txtdate.ReferenceFieldDesc = Nothing
        Me.txtdate.ReferenceFieldName = Nothing
        Me.txtdate.ReferenceTableName = Nothing
        Me.txtdate.Size = New System.Drawing.Size(97, 20)
        Me.txtdate.TabIndex = 1
        Me.txtdate.TabStop = False
        Me.txtdate.Text = "17/08/2014"
        Me.txtdate.Value = New Date(2014, 8, 17, 21, 27, 46, 113)
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(486, 43)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel2.TabIndex = 46
        Me.MyLabel2.Text = "Date"
        '
        'txtsec_code
        '
        Me.txtsec_code.CalculationExpression = Nothing
        Me.txtsec_code.FieldCode = Nothing
        Me.txtsec_code.FieldDesc = Nothing
        Me.txtsec_code.FieldMaxLength = 0
        Me.txtsec_code.FieldName = Nothing
        Me.txtsec_code.isCalculatedField = False
        Me.txtsec_code.IsSourceFromTable = False
        Me.txtsec_code.IsSourceFromValueList = False
        Me.txtsec_code.IsUnique = False
        Me.txtsec_code.Location = New System.Drawing.Point(132, 66)
        Me.txtsec_code.MendatroryField = True
        Me.txtsec_code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsec_code.MyLinkLable1 = Me.MyLabel1
        Me.txtsec_code.MyLinkLable2 = Me.txtdesc
        Me.txtsec_code.MyReadOnly = False
        Me.txtsec_code.MyShowMasterFormButton = False
        Me.txtsec_code.Name = "txtsec_code"
        Me.txtsec_code.ReferenceFieldDesc = Nothing
        Me.txtsec_code.ReferenceFieldName = Nothing
        Me.txtsec_code.ReferenceTableName = Nothing
        Me.txtsec_code.Size = New System.Drawing.Size(135, 19)
        Me.txtsec_code.TabIndex = 2
        Me.txtsec_code.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(12, 67)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel1.TabIndex = 24
        Me.MyLabel1.Text = "Section Detail"
        '
        'txtdesc
        '
        Me.txtdesc.AutoSize = False
        Me.txtdesc.BorderVisible = True
        Me.txtdesc.FieldName = Nothing
        Me.txtdesc.Location = New System.Drawing.Point(269, 66)
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.Size = New System.Drawing.Size(349, 19)
        Me.txtdesc.TabIndex = 41
        '
        'txtcategorycode
        '
        Me.txtcategorycode.CalculationExpression = Nothing
        Me.txtcategorycode.FieldCode = Nothing
        Me.txtcategorycode.FieldDesc = Nothing
        Me.txtcategorycode.FieldMaxLength = 0
        Me.txtcategorycode.FieldName = Nothing
        Me.txtcategorycode.isCalculatedField = False
        Me.txtcategorycode.IsSourceFromTable = False
        Me.txtcategorycode.IsSourceFromValueList = False
        Me.txtcategorycode.IsUnique = False
        Me.txtcategorycode.Location = New System.Drawing.Point(132, 90)
        Me.txtcategorycode.MendatroryField = True
        Me.txtcategorycode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcategorycode.MyLinkLable1 = Me.lblMasterItem
        Me.txtcategorycode.MyLinkLable2 = Me.txtcategoryname
        Me.txtcategorycode.MyReadOnly = False
        Me.txtcategorycode.MyShowMasterFormButton = False
        Me.txtcategorycode.Name = "txtcategorycode"
        Me.txtcategorycode.ReferenceFieldDesc = Nothing
        Me.txtcategorycode.ReferenceFieldName = Nothing
        Me.txtcategorycode.ReferenceTableName = Nothing
        Me.txtcategorycode.Size = New System.Drawing.Size(135, 19)
        Me.txtcategorycode.TabIndex = 3
        Me.txtcategorycode.Value = ""
        '
        'lblMasterItem
        '
        Me.lblMasterItem.FieldName = Nothing
        Me.lblMasterItem.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMasterItem.Location = New System.Drawing.Point(12, 89)
        Me.lblMasterItem.Name = "lblMasterItem"
        Me.lblMasterItem.Size = New System.Drawing.Size(109, 18)
        Me.lblMasterItem.TabIndex = 44
        Me.lblMasterItem.Text = "Production Category"
        '
        'txtcategoryname
        '
        Me.txtcategoryname.AutoSize = False
        Me.txtcategoryname.BorderVisible = True
        Me.txtcategoryname.FieldName = Nothing
        Me.txtcategoryname.Location = New System.Drawing.Point(269, 90)
        Me.txtcategoryname.Name = "txtcategoryname"
        Me.txtcategoryname.Size = New System.Drawing.Size(349, 19)
        Me.txtcategoryname.TabIndex = 43
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.RadLabel1.Location = New System.Drawing.Point(12, 43)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(88, 16)
        Me.RadLabel1.TabIndex = 23
        Me.RadLabel1.Text = "Document Code"
        '
        'fndCode
        '
        Me.fndCode.FieldName = Nothing
        Me.fndCode.Location = New System.Drawing.Point(132, 40)
        Me.fndCode.MendatroryField = True
        Me.fndCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCode.MyLinkLable1 = Me.RadLabel1
        Me.fndCode.MyLinkLable2 = Nothing
        Me.fndCode.MyMaxLength = 30
        Me.fndCode.MyReadOnly = False
        Me.fndCode.Name = "fndCode"
        Me.fndCode.Size = New System.Drawing.Size(247, 22)
        Me.fndCode.TabIndex = 0
        Me.fndCode.Value = ""
        '
        'btnNew
        '
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = Global.XpertERPProcessProduction.My.Resources.Resources._new
        Me.btnNew.Location = New System.Drawing.Point(381, 41)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(15, 21)
        Me.btnNew.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(3, 3)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(652, 295)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.gv1)
        Me.RadPageViewPage1.Controls.Add(Me.Panel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(631, 247)
        Me.RadPageViewPage1.Text = "Detail"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv1.Controls.Add(Me.RadGroupBox1)
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.AllowDragToGroup = False
        Me.gv1.MasterTemplate.EnableGrouping = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(631, 226)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.btngridclose)
        Me.RadGroupBox1.Controls.Add(Me.gv_user)
        Me.RadGroupBox1.HeaderText = "User Detail"
        Me.RadGroupBox1.Location = New System.Drawing.Point(137, 3)
        Me.RadGroupBox1.MaximumSize = New System.Drawing.Size(344, 217)
        Me.RadGroupBox1.MinimumSize = New System.Drawing.Size(344, 217)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        '
        '
        '
        Me.RadGroupBox1.RootElement.MaxSize = New System.Drawing.Size(344, 217)
        Me.RadGroupBox1.RootElement.MinSize = New System.Drawing.Size(344, 217)
        Me.RadGroupBox1.Size = New System.Drawing.Size(344, 217)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "User Detail"
        '
        'btngridclose
        '
        Me.btngridclose.AccessibleDescription = ""
        Me.btngridclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btngridclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btngridclose.Location = New System.Drawing.Point(124, 192)
        Me.btngridclose.Name = "btngridclose"
        Me.btngridclose.Size = New System.Drawing.Size(71, 21)
        Me.btngridclose.TabIndex = 1
        Me.btngridclose.Text = "Close"
        '
        'gv_user
        '
        Me.gv_user.Location = New System.Drawing.Point(5, 22)
        '
        '
        '
        Me.gv_user.MasterTemplate.AllowAddNewRow = False
        Me.gv_user.MasterTemplate.AllowDeleteRow = False
        Me.gv_user.MasterTemplate.AllowDragToGroup = False
        Me.gv_user.MasterTemplate.EnableFiltering = True
        Me.gv_user.MasterTemplate.EnableGrouping = False
        Me.gv_user.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv_user.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv_user.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv_user.MyStopExport = False
        Me.gv_user.Name = "gv_user"
        Me.gv_user.ShowGroupPanel = False
        Me.gv_user.ShowHeaderCellButtons = True
        Me.gv_user.Size = New System.Drawing.Size(334, 165)
        Me.gv_user.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 226)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(631, 21)
        Me.Panel1.TabIndex = 1
        '
        'MyLabel3
        '
        Me.MyLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.MyLabel3.Location = New System.Drawing.Point(367, 0)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(261, 18)
        Me.MyLabel3.TabIndex = 3
        Me.MyLabel3.Text = "Click F4 on Log Sheet column to see QC Log Sheet."
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(631, 242)
        Me.RadPageViewPage2.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(631, 242)
        Me.UcAttachment1.TabIndex = 6
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(7, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(71, 21)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.AccessibleDescription = ""
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(82, 6)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(71, 21)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.AccessibleDescription = ""
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(579, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(71, 21)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnImport})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'btnImport
        '
        Me.btnImport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnIMDetail, Me.btnIMUser})
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Text = "Import"
        '
        'btnIMDetail
        '
        Me.btnIMDetail.Name = "btnIMDetail"
        Me.btnIMDetail.Text = "Detail Import"
        '
        'btnIMUser
        '
        Me.btnIMUser.Name = "btnIMUser"
        Me.btnIMUser.Text = "User Import"
        '
        'RadButton1
        '
        Me.RadButton1.AccessibleDescription = ""
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(124, 192)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(71, 21)
        Me.RadButton1.TabIndex = 1
        Me.RadButton1.Text = "Close"
        '
        'RadGridView1
        '
        Me.RadGridView1.Location = New System.Drawing.Point(5, 22)
        '
        '
        '
        Me.RadGridView1.MasterTemplate.AllowAddNewRow = False
        Me.RadGridView1.MasterTemplate.AllowDeleteRow = False
        Me.RadGridView1.MasterTemplate.AllowDragToGroup = False
        Me.RadGridView1.MasterTemplate.EnableFiltering = True
        Me.RadGridView1.MasterTemplate.EnableGrouping = False
        Me.RadGridView1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.RadGridView1.MasterTemplate.ShowHeaderCellButtons = True
        Me.RadGridView1.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.RadGridView1.MyStopExport = False
        Me.RadGridView1.Name = "RadGridView1"
        Me.RadGridView1.ShowGroupPanel = False
        Me.RadGridView1.ShowHeaderCellButtons = True
        Me.RadGridView1.Size = New System.Drawing.Size(334, 165)
        Me.RadGridView1.TabIndex = 0
        '
        'btnEXDetail
        '
        Me.btnEXDetail.Name = "btnEXDetail"
        Me.btnEXDetail.Text = "Detail Export"
        '
        'btnEXUser
        '
        Me.btnEXUser.Name = "btnEXUser"
        Me.btnEXUser.Text = "User Export"
        '
        'btnExport
        '
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnEXDetail, Me.btnEXUser})
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "Export"
        '
        'FrmSectionStageMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(658, 456)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmSectionStageMapping"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Section Stage Mapping"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMasterItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcategoryname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gv1.ResumeLayout(False)
        Me.gv1.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.btngridclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_user.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_user, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents fndCode As common.UserControls.txtNavigator
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtdesc As common.Controls.MyLabel
    Friend WithEvents txtcategorycode As common.UserControls.txtFinder
    Friend WithEvents lblMasterItem As common.Controls.MyLabel
    Friend WithEvents txtcategoryname As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv_user As common.UserControls.MyRadGridView
    Friend WithEvents btngridclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtsec_code As common.UserControls.txtFinder
    Friend WithEvents txtdate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnIMDetail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnIMUser As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGridView1 As common.UserControls.MyRadGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    'Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadDropDownMenu
    Friend WithEvents btnEXDetail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnEXUser As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnexportExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnimportExcel As Telerik.WinControls.UI.RadMenuItem
End Class

