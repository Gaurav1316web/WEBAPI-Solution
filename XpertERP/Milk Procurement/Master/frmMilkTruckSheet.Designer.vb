<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMilkTruckSheet
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.UsLock1 = New common.usLock()
        Me.lblDocCode = New common.Controls.MyLabel()
        Me.lblMccName = New common.Controls.MyLabel()
        Me.txtUnloadingTime = New common.Controls.MyDateTimePicker()
        Me.lblMCCArrivalTime = New common.Controls.MyLabel()
        Me.lblRouteCode = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.fndMccCode = New common.UserControls.txtFinder()
        Me.lblMCCCode = New common.Controls.MyLabel()
        Me.lblUnloadingTime = New common.Controls.MyLabel()
        Me.fndRouteCode = New common.UserControls.txtFinder()
        Me.fndDocCode = New common.UserControls.txtNavigator()
        Me.lblRouteName = New common.Controls.MyLabel()
        Me.lblDocDate = New common.Controls.MyLabel()
        Me.cboShift = New common.Controls.MyComboBox()
        Me.lblShift = New common.Controls.MyLabel()
        Me.txtVehicleNo = New common.Controls.MyTextBox()
        Me.lblVehicleNo = New common.Controls.MyLabel()
        Me.lblSuperviserName = New common.Controls.MyLabel()
        Me.DtpDocDate = New common.Controls.MyDateTimePicker()
        Me.txtArrivalTime = New common.Controls.MyDateTimePicker()
        Me.txtSuperViserName = New common.Controls.MyTextBox()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnsaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
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
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMccName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUnloadingTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCArrivalTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnloadingTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSuperviserName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtpDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtArrivalTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSuperViserName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        Me.Attachments.SuspendLayout()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.Panel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(99.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(908, 483)
        Me.RadPageViewPage1.Text = "Milk Truck Sheet"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Truck Sheet Detail"
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 158)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(899, 322)
        Me.RadGroupBox2.TabIndex = 417
        Me.RadGroupBox2.Text = "Truck Sheet Detail"
        '
        'gv1
        '
        Me.gv1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(879, 292)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.Panel1.Location = New System.Drawing.Point(3, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(899, 151)
        Me.Panel1.TabIndex = 415
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.UsLock1)
        Me.RadGroupBox1.Controls.Add(Me.lblDocCode)
        Me.RadGroupBox1.Controls.Add(Me.lblMccName)
        Me.RadGroupBox1.Controls.Add(Me.txtUnloadingTime)
        Me.RadGroupBox1.Controls.Add(Me.lblRouteCode)
        Me.RadGroupBox1.Controls.Add(Me.btnnew)
        Me.RadGroupBox1.Controls.Add(Me.fndMccCode)
        Me.RadGroupBox1.Controls.Add(Me.lblUnloadingTime)
        Me.RadGroupBox1.Controls.Add(Me.fndRouteCode)
        Me.RadGroupBox1.Controls.Add(Me.fndDocCode)
        Me.RadGroupBox1.Controls.Add(Me.lblMCCCode)
        Me.RadGroupBox1.Controls.Add(Me.lblMCCArrivalTime)
        Me.RadGroupBox1.Controls.Add(Me.lblRouteName)
        Me.RadGroupBox1.Controls.Add(Me.lblDocDate)
        Me.RadGroupBox1.Controls.Add(Me.cboShift)
        Me.RadGroupBox1.Controls.Add(Me.txtVehicleNo)
        Me.RadGroupBox1.Controls.Add(Me.lblSuperviserName)
        Me.RadGroupBox1.Controls.Add(Me.DtpDocDate)
        Me.RadGroupBox1.Controls.Add(Me.txtArrivalTime)
        Me.RadGroupBox1.Controls.Add(Me.lblVehicleNo)
        Me.RadGroupBox1.Controls.Add(Me.txtSuperViserName)
        Me.RadGroupBox1.Controls.Add(Me.lblShift)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Truck Sheet Head"
        Me.RadGroupBox1.Location = New System.Drawing.Point(8, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(885, 145)
        Me.RadGroupBox1.TabIndex = 416
        Me.RadGroupBox1.Text = "Truck Sheet Head"
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
        Me.lblMccName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUnloadingTime
        '
        Me.txtUnloadingTime.CalculationExpression = Nothing
        Me.txtUnloadingTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtUnloadingTime.FieldCode = Nothing
        Me.txtUnloadingTime.FieldDesc = Nothing
        Me.txtUnloadingTime.FieldMaxLength = 0
        Me.txtUnloadingTime.FieldName = Nothing
        Me.txtUnloadingTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnloadingTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtUnloadingTime.isCalculatedField = False
        Me.txtUnloadingTime.IsSourceFromTable = False
        Me.txtUnloadingTime.IsSourceFromValueList = False
        Me.txtUnloadingTime.IsUnique = False
        Me.txtUnloadingTime.Location = New System.Drawing.Point(440, 114)
        Me.txtUnloadingTime.MendatroryField = False
        Me.txtUnloadingTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtUnloadingTime.MyLinkLable1 = Me.lblMCCArrivalTime
        Me.txtUnloadingTime.MyLinkLable2 = Nothing
        Me.txtUnloadingTime.Name = "txtUnloadingTime"
        Me.txtUnloadingTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtUnloadingTime.ReferenceFieldDesc = Nothing
        Me.txtUnloadingTime.ReferenceFieldName = Nothing
        Me.txtUnloadingTime.ReferenceTableName = Nothing
        Me.txtUnloadingTime.Size = New System.Drawing.Size(218, 18)
        Me.txtUnloadingTime.TabIndex = 5
        Me.txtUnloadingTime.TabStop = False
        Me.txtUnloadingTime.Text = "13/06/2011 11:29 AM"
        Me.txtUnloadingTime.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblMCCArrivalTime
        '
        Me.lblMCCArrivalTime.FieldName = Nothing
        Me.lblMCCArrivalTime.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblMCCArrivalTime.Location = New System.Drawing.Point(13, 115)
        Me.lblMCCArrivalTime.Name = "lblMCCArrivalTime"
        Me.lblMCCArrivalTime.Size = New System.Drawing.Size(96, 16)
        Me.lblMCCArrivalTime.TabIndex = 412
        Me.lblMCCArrivalTime.Text = "MCC Arrival Time"
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
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(407, 21)
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
        'lblUnloadingTime
        '
        Me.lblUnloadingTime.FieldName = Nothing
        Me.lblUnloadingTime.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblUnloadingTime.Location = New System.Drawing.Point(343, 115)
        Me.lblUnloadingTime.Name = "lblUnloadingTime"
        Me.lblUnloadingTime.Size = New System.Drawing.Size(86, 16)
        Me.lblUnloadingTime.TabIndex = 413
        Me.lblUnloadingTime.Text = "Unloading Time"
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
        Me.fndDocCode.MyMaxLength = 32767
        Me.fndDocCode.MyReadOnly = False
        Me.fndDocCode.Name = "fndDocCode"
        Me.fndDocCode.Size = New System.Drawing.Size(288, 21)
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
        Me.lblRouteName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDocDate
        '
        Me.lblDocDate.FieldName = Nothing
        Me.lblDocDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDocDate.Location = New System.Drawing.Point(432, 23)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(53, 16)
        Me.lblDocDate.TabIndex = 5
        Me.lblDocDate.Text = "Doc Date"
        '
        'cboShift
        '
        Me.cboShift.AutoCompleteDisplayMember = Nothing
        Me.cboShift.AutoCompleteValueMember = Nothing
        Me.cboShift.CalculationExpression = Nothing
        Me.cboShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboShift.FieldCode = Nothing
        Me.cboShift.FieldDesc = Nothing
        Me.cboShift.FieldMaxLength = 0
        Me.cboShift.FieldName = Nothing
        Me.cboShift.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShift.isCalculatedField = False
        Me.cboShift.IsSourceFromTable = False
        Me.cboShift.IsSourceFromValueList = False
        Me.cboShift.IsUnique = False
        RadListDataItem1.Text = "AM"
        RadListDataItem2.Text = "PM"
        Me.cboShift.Items.Add(RadListDataItem1)
        Me.cboShift.Items.Add(RadListDataItem2)
        Me.cboShift.Location = New System.Drawing.Point(659, 22)
        Me.cboShift.MendatroryField = False
        Me.cboShift.MyLinkLable1 = Me.lblShift
        Me.cboShift.MyLinkLable2 = Nothing
        Me.cboShift.Name = "cboShift"
        Me.cboShift.ReferenceFieldDesc = Nothing
        Me.cboShift.ReferenceFieldName = Nothing
        Me.cboShift.ReferenceTableName = Nothing
        Me.cboShift.Size = New System.Drawing.Size(83, 18)
        Me.cboShift.TabIndex = 9
        '
        'lblShift
        '
        Me.lblShift.FieldName = Nothing
        Me.lblShift.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblShift.Location = New System.Drawing.Point(624, 23)
        Me.lblShift.Name = "lblShift"
        Me.lblShift.Size = New System.Drawing.Size(29, 16)
        Me.lblShift.TabIndex = 7
        Me.lblShift.Text = "Shift"
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.CalculationExpression = Nothing
        Me.txtVehicleNo.FieldCode = Nothing
        Me.txtVehicleNo.FieldDesc = Nothing
        Me.txtVehicleNo.FieldMaxLength = 0
        Me.txtVehicleNo.FieldName = Nothing
        Me.txtVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleNo.isCalculatedField = False
        Me.txtVehicleNo.IsSourceFromTable = False
        Me.txtVehicleNo.IsSourceFromValueList = False
        Me.txtVehicleNo.IsUnique = False
        Me.txtVehicleNo.Location = New System.Drawing.Point(440, 92)
        Me.txtVehicleNo.MaxLength = 200
        Me.txtVehicleNo.MendatroryField = False
        Me.txtVehicleNo.MyLinkLable1 = Me.lblVehicleNo
        Me.txtVehicleNo.MyLinkLable2 = Nothing
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.ReferenceFieldDesc = Nothing
        Me.txtVehicleNo.ReferenceFieldName = Nothing
        Me.txtVehicleNo.ReferenceTableName = Nothing
        Me.txtVehicleNo.Size = New System.Drawing.Size(220, 18)
        Me.txtVehicleNo.TabIndex = 3
        Me.txtVehicleNo.TabStop = False
        '
        'lblVehicleNo
        '
        Me.lblVehicleNo.FieldName = Nothing
        Me.lblVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblVehicleNo.Location = New System.Drawing.Point(343, 93)
        Me.lblVehicleNo.Name = "lblVehicleNo"
        Me.lblVehicleNo.Size = New System.Drawing.Size(61, 16)
        Me.lblVehicleNo.TabIndex = 410
        Me.lblVehicleNo.Text = "Vehicle No"
        '
        'lblSuperviserName
        '
        Me.lblSuperviserName.FieldName = Nothing
        Me.lblSuperviserName.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblSuperviserName.Location = New System.Drawing.Point(13, 93)
        Me.lblSuperviserName.Name = "lblSuperviserName"
        Me.lblSuperviserName.Size = New System.Drawing.Size(94, 16)
        Me.lblSuperviserName.TabIndex = 16
        Me.lblSuperviserName.Text = "Supervisor Name"
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
        Me.DtpDocDate.Location = New System.Drawing.Point(491, 22)
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
        'txtArrivalTime
        '
        Me.txtArrivalTime.CalculationExpression = Nothing
        Me.txtArrivalTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtArrivalTime.FieldCode = Nothing
        Me.txtArrivalTime.FieldDesc = Nothing
        Me.txtArrivalTime.FieldMaxLength = 0
        Me.txtArrivalTime.FieldName = Nothing
        Me.txtArrivalTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtArrivalTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtArrivalTime.isCalculatedField = False
        Me.txtArrivalTime.IsSourceFromTable = False
        Me.txtArrivalTime.IsSourceFromValueList = False
        Me.txtArrivalTime.IsUnique = False
        Me.txtArrivalTime.Location = New System.Drawing.Point(117, 114)
        Me.txtArrivalTime.MendatroryField = False
        Me.txtArrivalTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtArrivalTime.MyLinkLable1 = Me.lblMCCArrivalTime
        Me.txtArrivalTime.MyLinkLable2 = Nothing
        Me.txtArrivalTime.Name = "txtArrivalTime"
        Me.txtArrivalTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtArrivalTime.ReferenceFieldDesc = Nothing
        Me.txtArrivalTime.ReferenceFieldName = Nothing
        Me.txtArrivalTime.ReferenceTableName = Nothing
        Me.txtArrivalTime.Size = New System.Drawing.Size(220, 18)
        Me.txtArrivalTime.TabIndex = 4
        Me.txtArrivalTime.TabStop = False
        Me.txtArrivalTime.Text = "13/06/2011 11:29 AM"
        Me.txtArrivalTime.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtSuperViserName
        '
        Me.txtSuperViserName.CalculationExpression = Nothing
        Me.txtSuperViserName.FieldCode = Nothing
        Me.txtSuperViserName.FieldDesc = Nothing
        Me.txtSuperViserName.FieldMaxLength = 0
        Me.txtSuperViserName.FieldName = Nothing
        Me.txtSuperViserName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuperViserName.isCalculatedField = False
        Me.txtSuperViserName.IsSourceFromTable = False
        Me.txtSuperViserName.IsSourceFromValueList = False
        Me.txtSuperViserName.IsUnique = False
        Me.txtSuperViserName.Location = New System.Drawing.Point(117, 92)
        Me.txtSuperViserName.MaxLength = 200
        Me.txtSuperViserName.MendatroryField = True
        Me.txtSuperViserName.MyLinkLable1 = Me.lblSuperviserName
        Me.txtSuperViserName.MyLinkLable2 = Nothing
        Me.txtSuperViserName.Name = "txtSuperViserName"
        Me.txtSuperViserName.ReferenceFieldDesc = Nothing
        Me.txtSuperViserName.ReferenceFieldName = Nothing
        Me.txtSuperViserName.ReferenceTableName = Nothing
        Me.txtSuperViserName.Size = New System.Drawing.Size(220, 18)
        Me.txtSuperViserName.TabIndex = 2
        Me.txtSuperViserName.TabStop = False
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(86.0!, 28.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(908, 483)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(908, 483)
        Me.UcCustomFields1.TabIndex = 0
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 28.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 37)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(908, 483)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(908, 483)
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
        Me.rdmenufile.Text = "File"
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
        Me.BtnsaveLayout.AccessibleDescription = "Save Layout"
        Me.BtnsaveLayout.AccessibleName = "Save Layout"
        Me.BtnsaveLayout.Name = "BtnsaveLayout"
        Me.BtnsaveLayout.Text = "Save Layout"
        '
        'BtnDeleteLayout
        '
        Me.BtnDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.BtnDeleteLayout.AccessibleName = "Delete Layout"
        Me.BtnDeleteLayout.Name = "BtnDeleteLayout"
        Me.BtnDeleteLayout.Text = "Delete Layout"
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
        'FrmMilkTruckSheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(929, 587)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmMilkTruckSheet"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmMilkTruckSheet"
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
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMccName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUnloadingTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCArrivalTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnloadingTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSuperviserName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtpDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtArrivalTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSuperViserName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        Me.Attachments.ResumeLayout(False)
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents lblShift As common.Controls.MyLabel
    Friend WithEvents DtpDocDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtArrivalTime As common.Controls.MyDateTimePicker
    Friend WithEvents cboShift As common.Controls.MyComboBox
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents lblRouteName As common.Controls.MyLabel
    Friend WithEvents fndRouteCode As common.UserControls.txtFinder
    Friend WithEvents lblRouteCode As common.Controls.MyLabel
    Friend WithEvents lblMccName As common.Controls.MyLabel
    Friend WithEvents fndMccCode As common.UserControls.txtFinder
    Friend WithEvents lblSuperviserName As common.Controls.MyLabel
    Friend WithEvents lblMCCArrivalTime As common.Controls.MyLabel
    Friend WithEvents txtVehicleNo As common.Controls.MyTextBox
    Friend WithEvents lblVehicleNo As common.Controls.MyLabel
    Friend WithEvents txtSuperViserName As common.Controls.MyTextBox
    Friend WithEvents txtUnloadingTime As common.Controls.MyDateTimePicker
    Friend WithEvents lblUnloadingTime As common.Controls.MyLabel
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
    ' Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnsaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
End Class

