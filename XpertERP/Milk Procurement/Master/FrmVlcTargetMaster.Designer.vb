<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVlcTargetMaster
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblVSP = New common.Controls.MyLabel()
        Me.fndVSPCode = New common.Controls.MyLabel()
        Me.lblVLC = New common.Controls.MyLabel()
        Me.fndVSPName = New common.Controls.MyLabel()
        Me.fndVLCCode = New common.UserControls.txtFinder()
        Me.lblRouteCode = New common.Controls.MyLabel()
        Me.fndVLCName = New common.Controls.MyLabel()
        Me.chkMP = New common.Controls.MyCheckBox()
        Me.LblTodate = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.DtpTodate = New common.Controls.MyDateTimePicker()
        Me.DtpFromDate = New common.Controls.MyDateTimePicker()
        Me.UsLock1 = New common.usLock()
        Me.lblDocCode = New common.Controls.MyLabel()
        Me.lblMccName = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.fndMccCode = New common.UserControls.txtFinder()
        Me.lblMCCCode = New common.Controls.MyLabel()
        Me.fndRouteCode = New common.UserControls.txtFinder()
        Me.fndDocCode = New common.UserControls.txtNavigator()
        Me.lblRouteName = New common.Controls.MyLabel()
        Me.lblDocDate = New common.Controls.MyLabel()
        Me.DtpDocDate = New common.Controls.MyDateTimePicker()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnsaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnSelect = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.BtnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblVSP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fndVSPCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVLC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fndVSPName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fndVLCName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblTodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtpTodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMccName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtpDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        Me.Attachments.SuspendLayout()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdmenufile)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnSelect)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(929, 587)
        Me.SplitContainer1.SplitterDistance = 551
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(929, 531)
        Me.RadPageView1.TabIndex = 417
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.Panel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(108.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(908, 483)
        Me.RadPageViewPage1.Text = "VLC Target Master"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "VLC Target Detail"
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 172)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(899, 308)
        Me.RadGroupBox2.TabIndex = 417
        Me.RadGroupBox2.Text = "VLC Target Detail"
        '
        'gv1
        '
        Me.gv1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(879, 278)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.Panel1.Location = New System.Drawing.Point(3, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(899, 165)
        Me.Panel1.TabIndex = 415
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.lblVSP)
        Me.RadGroupBox1.Controls.Add(Me.fndVSPCode)
        Me.RadGroupBox1.Controls.Add(Me.lblVLC)
        Me.RadGroupBox1.Controls.Add(Me.fndVSPName)
        Me.RadGroupBox1.Controls.Add(Me.fndVLCCode)
        Me.RadGroupBox1.Controls.Add(Me.fndVLCName)
        Me.RadGroupBox1.Controls.Add(Me.chkMP)
        Me.RadGroupBox1.Controls.Add(Me.LblTodate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.DtpTodate)
        Me.RadGroupBox1.Controls.Add(Me.DtpFromDate)
        Me.RadGroupBox1.Controls.Add(Me.UsLock1)
        Me.RadGroupBox1.Controls.Add(Me.lblDocCode)
        Me.RadGroupBox1.Controls.Add(Me.lblMccName)
        Me.RadGroupBox1.Controls.Add(Me.lblRouteCode)
        Me.RadGroupBox1.Controls.Add(Me.btnnew)
        Me.RadGroupBox1.Controls.Add(Me.fndMccCode)
        Me.RadGroupBox1.Controls.Add(Me.fndRouteCode)
        Me.RadGroupBox1.Controls.Add(Me.fndDocCode)
        Me.RadGroupBox1.Controls.Add(Me.lblMCCCode)
        Me.RadGroupBox1.Controls.Add(Me.lblRouteName)
        Me.RadGroupBox1.Controls.Add(Me.lblDocDate)
        Me.RadGroupBox1.Controls.Add(Me.DtpDocDate)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "VLC Target Head"
        Me.RadGroupBox1.Location = New System.Drawing.Point(8, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(885, 145)
        Me.RadGroupBox1.TabIndex = 416
        Me.RadGroupBox1.Text = "VLC Target Head"
        '
        'lblVSP
        '
        Me.lblVSP.FieldName = Nothing
        Me.lblVSP.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblVSP.Location = New System.Drawing.Point(430, 120)
        Me.lblVSP.Name = "lblVSP"
        Me.lblVSP.Size = New System.Drawing.Size(59, 16)
        Me.lblVSP.TabIndex = 428
        Me.lblVSP.Text = "VSP Code"
        '
        'fndVSPCode
        '
        Me.fndVSPCode.AutoSize = False
        Me.fndVSPCode.BorderVisible = True
        Me.fndVSPCode.FieldName = Nothing
        Me.fndVSPCode.Location = New System.Drawing.Point(496, 117)
        Me.fndVSPCode.Name = "fndVSPCode"
        Me.fndVSPCode.Size = New System.Drawing.Size(184, 19)
        Me.fndVSPCode.TabIndex = 427
        '
        'lblVLC
        '
        Me.lblVLC.FieldName = Nothing
        Me.lblVLC.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblVLC.Location = New System.Drawing.Point(13, 120)
        Me.lblVLC.Name = "lblVLC"
        Me.lblVLC.Size = New System.Drawing.Size(58, 16)
        Me.lblVLC.TabIndex = 426
        Me.lblVLC.Text = "VLC Code"
        '
        'fndVSPName
        '
        Me.fndVSPName.AutoSize = False
        Me.fndVSPName.BorderVisible = True
        Me.fndVSPName.FieldName = Nothing
        Me.fndVSPName.Location = New System.Drawing.Point(686, 117)
        Me.fndVSPName.Name = "fndVSPName"
        Me.fndVSPName.Size = New System.Drawing.Size(184, 19)
        Me.fndVSPName.TabIndex = 425
        '
        'fndVLCCode
        '
        Me.fndVLCCode.CalculationExpression = Nothing
        Me.fndVLCCode.FieldCode = Nothing
        Me.fndVLCCode.FieldDesc = Nothing
        Me.fndVLCCode.FieldMaxLength = 0
        Me.fndVLCCode.FieldName = Nothing
        Me.fndVLCCode.isCalculatedField = False
        Me.fndVLCCode.IsSourceFromTable = False
        Me.fndVLCCode.IsSourceFromValueList = False
        Me.fndVLCCode.IsUnique = False
        Me.fndVLCCode.Location = New System.Drawing.Point(114, 117)
        Me.fndVLCCode.MendatroryField = True
        Me.fndVLCCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVLCCode.MyLinkLable1 = Me.lblRouteCode
        Me.fndVLCCode.MyLinkLable2 = Nothing
        Me.fndVLCCode.MyReadOnly = False
        Me.fndVLCCode.MyShowMasterFormButton = False
        Me.fndVLCCode.Name = "fndVLCCode"
        Me.fndVLCCode.ReferenceFieldDesc = Nothing
        Me.fndVLCCode.ReferenceFieldName = Nothing
        Me.fndVLCCode.ReferenceTableName = Nothing
        Me.fndVLCCode.Size = New System.Drawing.Size(143, 19)
        Me.fndVLCCode.TabIndex = 422
        Me.fndVLCCode.Value = ""
        '
        'lblRouteCode
        '
        Me.lblRouteCode.FieldName = Nothing
        Me.lblRouteCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblRouteCode.Location = New System.Drawing.Point(13, 70)
        Me.lblRouteCode.Name = "lblRouteCode"
        Me.lblRouteCode.Size = New System.Drawing.Size(67, 16)
        Me.lblRouteCode.TabIndex = 13
        Me.lblRouteCode.Text = "Route Code"
        '
        'fndVLCName
        '
        Me.fndVLCName.AutoSize = False
        Me.fndVLCName.BorderVisible = True
        Me.fndVLCName.FieldName = Nothing
        Me.fndVLCName.Location = New System.Drawing.Point(261, 117)
        Me.fndVLCName.Name = "fndVLCName"
        Me.fndVLCName.Size = New System.Drawing.Size(163, 19)
        Me.fndVLCName.TabIndex = 423
        '
        'chkMP
        '
        Me.chkMP.Location = New System.Drawing.Point(764, 96)
        Me.chkMP.MyLinkLable1 = Nothing
        Me.chkMP.MyLinkLable2 = Nothing
        Me.chkMP.Name = "chkMP"
        Me.chkMP.Size = New System.Drawing.Size(37, 18)
        Me.chkMP.TabIndex = 421
        Me.chkMP.Tag1 = Nothing
        Me.chkMP.Text = "MP"
        '
        'LblTodate
        '
        Me.LblTodate.FieldName = Nothing
        Me.LblTodate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.LblTodate.Location = New System.Drawing.Point(347, 94)
        Me.LblTodate.Name = "LblTodate"
        Me.LblTodate.Size = New System.Drawing.Size(46, 16)
        Me.LblTodate.TabIndex = 418
        Me.LblTodate.Text = "To Date"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(13, 94)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel1.TabIndex = 416
        Me.MyLabel1.Text = "From Date"
        '
        'DtpTodate
        '
        Me.DtpTodate.CalculationExpression = Nothing
        Me.DtpTodate.CustomFormat = "dd/MM/yyyy"
        Me.DtpTodate.FieldCode = Nothing
        Me.DtpTodate.FieldDesc = Nothing
        Me.DtpTodate.FieldMaxLength = 0
        Me.DtpTodate.FieldName = Nothing
        Me.DtpTodate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpTodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpTodate.isCalculatedField = False
        Me.DtpTodate.IsSourceFromTable = False
        Me.DtpTodate.IsSourceFromValueList = False
        Me.DtpTodate.IsUnique = False
        Me.DtpTodate.Location = New System.Drawing.Point(402, 93)
        Me.DtpTodate.MendatroryField = False
        Me.DtpTodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpTodate.MyLinkLable1 = Me.LblTodate
        Me.DtpTodate.MyLinkLable2 = Nothing
        Me.DtpTodate.Name = "DtpTodate"
        Me.DtpTodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpTodate.ReferenceFieldDesc = Nothing
        Me.DtpTodate.ReferenceFieldName = Nothing
        Me.DtpTodate.ReferenceTableName = Nothing
        Me.DtpTodate.Size = New System.Drawing.Size(130, 18)
        Me.DtpTodate.TabIndex = 419
        Me.DtpTodate.TabStop = False
        Me.DtpTodate.Text = "13/06/2011"
        Me.DtpTodate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'DtpFromDate
        '
        Me.DtpFromDate.CalculationExpression = Nothing
        Me.DtpFromDate.CustomFormat = "dd/MM/yyyy"
        Me.DtpFromDate.FieldCode = Nothing
        Me.DtpFromDate.FieldDesc = Nothing
        Me.DtpFromDate.FieldMaxLength = 0
        Me.DtpFromDate.FieldName = Nothing
        Me.DtpFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpFromDate.isCalculatedField = False
        Me.DtpFromDate.IsSourceFromTable = False
        Me.DtpFromDate.IsSourceFromValueList = False
        Me.DtpFromDate.IsUnique = False
        Me.DtpFromDate.Location = New System.Drawing.Point(117, 93)
        Me.DtpFromDate.MendatroryField = False
        Me.DtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpFromDate.MyLinkLable1 = Me.MyLabel1
        Me.DtpFromDate.MyLinkLable2 = Nothing
        Me.DtpFromDate.Name = "DtpFromDate"
        Me.DtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpFromDate.ReferenceFieldDesc = Nothing
        Me.DtpFromDate.ReferenceFieldName = Nothing
        Me.DtpFromDate.ReferenceTableName = Nothing
        Me.DtpFromDate.Size = New System.Drawing.Size(140, 18)
        Me.DtpFromDate.TabIndex = 417
        Me.DtpFromDate.TabStop = False
        Me.DtpFromDate.Text = "13/06/2011"
        Me.DtpFromDate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(762, 21)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(108, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 415
        '
        'lblDocCode
        '
        Me.lblDocCode.FieldName = Nothing
        Me.lblDocCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDocCode.Location = New System.Drawing.Point(13, 23)
        Me.lblDocCode.Name = "lblDocCode"
        Me.lblDocCode.Size = New System.Drawing.Size(57, 16)
        Me.lblDocCode.TabIndex = 2
        Me.lblDocCode.Text = "Doc Code"
        '
        'lblMccName
        '
        Me.lblMccName.AutoSize = False
        Me.lblMccName.BorderVisible = True
        Me.lblMccName.FieldName = Nothing
        Me.lblMccName.Location = New System.Drawing.Point(343, 46)
        Me.lblMccName.Name = "lblMccName"
        Me.lblMccName.Size = New System.Drawing.Size(527, 19)
        Me.lblMccName.TabIndex = 12
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(319, 21)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 4
        '
        'fndMccCode
        '
        Me.fndMccCode.CalculationExpression = Nothing
        Me.fndMccCode.FieldCode = Nothing
        Me.fndMccCode.FieldDesc = Nothing
        Me.fndMccCode.FieldMaxLength = 0
        Me.fndMccCode.FieldName = Nothing
        Me.fndMccCode.isCalculatedField = False
        Me.fndMccCode.IsSourceFromTable = False
        Me.fndMccCode.IsSourceFromValueList = False
        Me.fndMccCode.IsUnique = False
        Me.fndMccCode.Location = New System.Drawing.Point(117, 46)
        Me.fndMccCode.MendatroryField = True
        Me.fndMccCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndMccCode.MyLinkLable1 = Me.lblMCCCode
        Me.fndMccCode.MyLinkLable2 = Nothing
        Me.fndMccCode.MyReadOnly = False
        Me.fndMccCode.MyShowMasterFormButton = False
        Me.fndMccCode.Name = "fndMccCode"
        Me.fndMccCode.ReferenceFieldDesc = Nothing
        Me.fndMccCode.ReferenceFieldName = Nothing
        Me.fndMccCode.ReferenceTableName = Nothing
        Me.fndMccCode.Size = New System.Drawing.Size(220, 19)
        Me.fndMccCode.TabIndex = 11
        Me.fndMccCode.Value = ""
        '
        'lblMCCCode
        '
        Me.lblMCCCode.FieldName = Nothing
        Me.lblMCCCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblMCCCode.Location = New System.Drawing.Point(13, 47)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(62, 16)
        Me.lblMCCCode.TabIndex = 10
        Me.lblMCCCode.Text = "MCC Code"
        '
        'fndRouteCode
        '
        Me.fndRouteCode.CalculationExpression = Nothing
        Me.fndRouteCode.FieldCode = Nothing
        Me.fndRouteCode.FieldDesc = Nothing
        Me.fndRouteCode.FieldMaxLength = 0
        Me.fndRouteCode.FieldName = Nothing
        Me.fndRouteCode.isCalculatedField = False
        Me.fndRouteCode.IsSourceFromTable = False
        Me.fndRouteCode.IsSourceFromValueList = False
        Me.fndRouteCode.IsUnique = False
        Me.fndRouteCode.Location = New System.Drawing.Point(117, 69)
        Me.fndRouteCode.MendatroryField = True
        Me.fndRouteCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRouteCode.MyLinkLable1 = Me.lblRouteCode
        Me.fndRouteCode.MyLinkLable2 = Nothing
        Me.fndRouteCode.MyReadOnly = False
        Me.fndRouteCode.MyShowMasterFormButton = False
        Me.fndRouteCode.Name = "fndRouteCode"
        Me.fndRouteCode.ReferenceFieldDesc = Nothing
        Me.fndRouteCode.ReferenceFieldName = Nothing
        Me.fndRouteCode.ReferenceTableName = Nothing
        Me.fndRouteCode.Size = New System.Drawing.Size(220, 19)
        Me.fndRouteCode.TabIndex = 1
        Me.fndRouteCode.Value = ""
        '
        'fndDocCode
        '
        Me.fndDocCode.FieldName = Nothing
        Me.fndDocCode.Location = New System.Drawing.Point(117, 21)
        Me.fndDocCode.MendatroryField = True
        Me.fndDocCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndDocCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocCode.MyLinkLable1 = Me.lblDocCode
        Me.fndDocCode.MyLinkLable2 = Nothing
        Me.fndDocCode.MyMaxLength = 30
        Me.fndDocCode.MyReadOnly = False
        Me.fndDocCode.Name = "fndDocCode"
        Me.fndDocCode.Size = New System.Drawing.Size(202, 21)
        Me.fndDocCode.TabIndex = 0
        Me.fndDocCode.Value = ""
        '
        'lblRouteName
        '
        Me.lblRouteName.AutoSize = False
        Me.lblRouteName.BorderVisible = True
        Me.lblRouteName.FieldName = Nothing
        Me.lblRouteName.Location = New System.Drawing.Point(343, 69)
        Me.lblRouteName.Name = "lblRouteName"
        Me.lblRouteName.Size = New System.Drawing.Size(527, 19)
        Me.lblRouteName.TabIndex = 15
        '
        'lblDocDate
        '
        Me.lblDocDate.FieldName = Nothing
        Me.lblDocDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDocDate.Location = New System.Drawing.Point(343, 23)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(53, 16)
        Me.lblDocDate.TabIndex = 5
        Me.lblDocDate.Text = "Doc Date"
        '
        'DtpDocDate
        '
        Me.DtpDocDate.CalculationExpression = Nothing
        Me.DtpDocDate.CustomFormat = "dd/MM/yyyy"
        Me.DtpDocDate.FieldCode = Nothing
        Me.DtpDocDate.FieldDesc = Nothing
        Me.DtpDocDate.FieldMaxLength = 0
        Me.DtpDocDate.FieldName = Nothing
        Me.DtpDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpDocDate.isCalculatedField = False
        Me.DtpDocDate.IsSourceFromTable = False
        Me.DtpDocDate.IsSourceFromValueList = False
        Me.DtpDocDate.IsUnique = False
        Me.DtpDocDate.Location = New System.Drawing.Point(402, 22)
        Me.DtpDocDate.MendatroryField = False
        Me.DtpDocDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpDocDate.MyLinkLable1 = Me.lblDocDate
        Me.DtpDocDate.MyLinkLable2 = Nothing
        Me.DtpDocDate.Name = "DtpDocDate"
        Me.DtpDocDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpDocDate.ReferenceFieldDesc = Nothing
        Me.DtpDocDate.ReferenceFieldName = Nothing
        Me.DtpDocDate.ReferenceTableName = Nothing
        Me.DtpDocDate.Size = New System.Drawing.Size(130, 18)
        Me.DtpDocDate.TabIndex = 6
        Me.DtpDocDate.TabStop = False
        Me.DtpDocDate.Text = "13/06/2011"
        Me.DtpDocDate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(86.0!, 28.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(967, 484)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(967, 484)
        Me.UcCustomFields1.TabIndex = 1
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 28.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 37)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(967, 464)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(967, 464)
        Me.UcAttachment1.TabIndex = 0
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.Transparent
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(929, 20)
        Me.rdmenufile.TabIndex = 418
        '
        'rdmenufile1
        '
        Me.rdmenufile1.AccessibleDescription = "File"
        Me.rdmenufile1.AccessibleName = "File"
        Me.rdmenufile1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.BtnsaveLayout, Me.BtnDeleteLayout})
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "Settings"
        '
        'BtnsaveLayout
        '
        Me.BtnsaveLayout.Name = "BtnsaveLayout"
        Me.BtnsaveLayout.Text = "Save Layout"
        '
        'BtnDeleteLayout
        '
        Me.BtnDeleteLayout.Name = "BtnDeleteLayout"
        Me.BtnDeleteLayout.Text = "Delete Layout"
        '
        'BtnSelect
        '
        Me.BtnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnSelect.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelect.Location = New System.Drawing.Point(227, 8)
        Me.BtnSelect.Name = "BtnSelect"
        Me.BtnSelect.Size = New System.Drawing.Size(66, 18)
        Me.BtnSelect.TabIndex = 2
        Me.BtnSelect.Text = "Select All"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(851, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 18)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(83, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 18)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'BtnSave
        '
        Me.BtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSave.Location = New System.Drawing.Point(11, 8)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(66, 18)
        Me.BtnSave.TabIndex = 1
        Me.BtnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(155, 8)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(66, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'FrmVlcTargetMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(929, 587)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmVlcTargetMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmMilkVLCTargetMaster"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblVSP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fndVSPCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVLC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fndVSPName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fndVLCName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblTodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtpTodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMccName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtpDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        Me.Attachments.ResumeLayout(False)
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents fndDocCode As common.UserControls.txtNavigator
    Friend WithEvents lblDocCode As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDocDate As common.Controls.MyLabel
    Friend WithEvents DtpDocDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents lblRouteName As common.Controls.MyLabel
    Friend WithEvents fndRouteCode As common.UserControls.txtFinder
    Friend WithEvents lblRouteCode As common.Controls.MyLabel
    Friend WithEvents lblMccName As common.Controls.MyLabel
    Friend WithEvents fndMccCode As common.UserControls.txtFinder
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnsaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents DtpFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents LblTodate As common.Controls.MyLabel
    Friend WithEvents DtpTodate As common.Controls.MyDateTimePicker
    Friend WithEvents BtnSelect As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblVSP As common.Controls.MyLabel
    Friend WithEvents fndVSPCode As common.Controls.MyLabel
    Friend WithEvents lblVLC As common.Controls.MyLabel
    Friend WithEvents fndVSPName As common.Controls.MyLabel
    Friend WithEvents fndVLCCode As common.UserControls.txtFinder
    Friend WithEvents fndVLCName As common.Controls.MyLabel
    Friend WithEvents chkMP As common.Controls.MyCheckBox
End Class

