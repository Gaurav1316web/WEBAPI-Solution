Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMilkGateEntry_JWO
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.mnuSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuExit = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtTransferNo = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblJobworkLocation = New common.Controls.MyLabel()
        Me.TxtJobworlLocation = New common.UserControls.txtFinder()
        Me.fndTankerNo = New common.UserControls.txtFinder()
        Me.lblVendorBulk = New common.Controls.MyLabel()
        Me.txtDocType = New common.Controls.MyLabel()
        Me.chkBoth = New Telerik.WinControls.UI.RadRadioButton()
        Me.lblPending = New common.usLock()
        Me.fndChallanNoMcc = New common.UserControls.txtFinder()
        Me.grpGateEntryType = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkBulkMilkProc = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkMccProc = New Telerik.WinControls.UI.RadRadioButton()
        Me.lblTankerNoBulk = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.lblDateAndTimeBulk = New common.Controls.MyLabel()
        Me.fndGateEntryNO = New common.UserControls.txtNavigator()
        Me.lblGateEntryNO = New common.Controls.MyLabel()
        Me.txtTankerNoBulk = New common.Controls.MyTextBox()
        Me.lblVendorNameBulk = New common.Controls.MyLabel()
        Me.dtpDateAndTimeBulk = New common.Controls.MyDateTimePicker()
        Me.txtChallanNoBulk = New common.Controls.MyTextBox()
        Me.lblChallanNoBulk = New common.Controls.MyLabel()
        Me.lblLocationBulk = New common.Controls.MyLabel()
        Me.fndVendorBulk = New common.UserControls.txtFinder()
        Me.dtpChallanDateBulk = New common.Controls.MyDateTimePicker()
        Me.lblChallanDateBulk = New common.Controls.MyLabel()
        Me.lblLocationDecBulk = New common.Controls.MyLabel()
        Me.fndLocationBulk = New common.UserControls.txtFinder()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvItemBulk = New common.UserControls.MyRadGridView()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvManualSeal = New common.UserControls.MyRadGridView()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvPaperSeal = New common.UserControls.MyRadGridView()
        Me.btnPrintNew = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblJobworkLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.txtDocType.SuspendLayout()
        CType(Me.chkBoth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpGateEntryType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpGateEntryType.SuspendLayout()
        CType(Me.chkBulkMilkProc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMccProc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNoBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDateAndTimeBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGateEntryNO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTankerNoBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorNameBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDateAndTimeBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChallanNoBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChallanNoBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpChallanDateBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChallanDateBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDecBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gvItemBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItemBulk.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.gvManualSeal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvManualSeal.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gvPaperSeal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPaperSeal.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrintNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Size = New System.Drawing.Size(779, 588)
        Me.SplitContainer1.SplitterDistance = 25
        Me.SplitContainer1.TabIndex = 0
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuSetting})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(779, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'mnuSetting
        '
        Me.mnuSetting.AccessibleDescription = "Setting"
        Me.mnuSetting.AccessibleName = "Setting"
        Me.mnuSetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuSaveLayout, Me.mnuDeleteLayout, Me.mnuExit})
        Me.mnuSetting.Name = "mnuSetting"
        Me.mnuSetting.Text = "Setting"
        '
        'mnuSaveLayout
        '
        Me.mnuSaveLayout.AccessibleDescription = "Save Layout"
        Me.mnuSaveLayout.AccessibleName = "Save Layout"
        Me.mnuSaveLayout.Name = "mnuSaveLayout"
        Me.mnuSaveLayout.Text = "Save Layout"
        '
        'mnuDeleteLayout
        '
        Me.mnuDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.mnuDeleteLayout.AccessibleName = "Delete Layout"
        Me.mnuDeleteLayout.Name = "mnuDeleteLayout"
        Me.mnuDeleteLayout.Text = "Delete Layout"
        '
        'mnuExit
        '
        Me.mnuExit.AccessibleDescription = "Exit"
        Me.mnuExit.AccessibleName = "Exit"
        Me.mnuExit.Name = "mnuExit"
        Me.mnuExit.Text = "Exit"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(779, 559)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(55.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(758, 511)
        Me.RadPageViewPage1.Text = "General"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer4)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnPrintNew)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer2.Size = New System.Drawing.Size(758, 511)
        Me.SplitContainer2.SplitterDistance = 475
        Me.SplitContainer2.TabIndex = 0
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtTransferNo)
        Me.SplitContainer4.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblJobworkLocation)
        Me.SplitContainer4.Panel1.Controls.Add(Me.TxtJobworlLocation)
        Me.SplitContainer4.Panel1.Controls.Add(Me.fndTankerNo)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtDocType)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblPending)
        Me.SplitContainer4.Panel1.Controls.Add(Me.fndChallanNoMcc)
        Me.SplitContainer4.Panel1.Controls.Add(Me.grpGateEntryType)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblTankerNoBulk)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblDateAndTimeBulk)
        Me.SplitContainer4.Panel1.Controls.Add(Me.fndGateEntryNO)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtTankerNoBulk)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblGateEntryNO)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblVendorBulk)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblVendorNameBulk)
        Me.SplitContainer4.Panel1.Controls.Add(Me.dtpDateAndTimeBulk)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtChallanNoBulk)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblLocationBulk)
        Me.SplitContainer4.Panel1.Controls.Add(Me.fndVendorBulk)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblChallanNoBulk)
        Me.SplitContainer4.Panel1.Controls.Add(Me.dtpChallanDateBulk)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblLocationDecBulk)
        Me.SplitContainer4.Panel1.Controls.Add(Me.fndLocationBulk)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblChallanDateBulk)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer4.Size = New System.Drawing.Size(758, 475)
        Me.SplitContainer4.SplitterDistance = 199
        Me.SplitContainer4.TabIndex = 0
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(431, 69)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(65, 18)
        Me.MyLabel5.TabIndex = 278
        Me.MyLabel5.Text = "Transfer No"
        '
        'txtTransferNo
        '
        Me.txtTransferNo.arrDispalyMember = Nothing
        Me.txtTransferNo.arrValueMember = Nothing
        Me.txtTransferNo.Location = New System.Drawing.Point(500, 69)
        Me.txtTransferNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransferNo.MyLinkLable1 = Me.MyLabel5
        Me.txtTransferNo.MyLinkLable2 = Nothing
        Me.txtTransferNo.MyNullText = "All"
        Me.txtTransferNo.Name = "txtTransferNo"
        Me.txtTransferNo.Size = New System.Drawing.Size(239, 19)
        Me.txtTransferNo.TabIndex = 277
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(9, 112)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel1.TabIndex = 275
        Me.MyLabel1.Text = "Jobwork Location"
        '
        'lblJobworkLocation
        '
        Me.lblJobworkLocation.AutoSize = False
        Me.lblJobworkLocation.BorderVisible = True
        Me.lblJobworkLocation.FieldName = Nothing
        Me.lblJobworkLocation.Location = New System.Drawing.Point(432, 112)
        Me.lblJobworkLocation.Name = "lblJobworkLocation"
        Me.lblJobworkLocation.Size = New System.Drawing.Size(307, 19)
        Me.lblJobworkLocation.TabIndex = 276
        Me.lblJobworkLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtJobworlLocation
        '
        Me.TxtJobworlLocation.CalculationExpression = Nothing
        Me.TxtJobworlLocation.FieldCode = Nothing
        Me.TxtJobworlLocation.FieldDesc = Nothing
        Me.TxtJobworlLocation.FieldMaxLength = 0
        Me.TxtJobworlLocation.FieldName = Nothing
        Me.TxtJobworlLocation.isCalculatedField = False
        Me.TxtJobworlLocation.IsSourceFromTable = False
        Me.TxtJobworlLocation.IsSourceFromValueList = False
        Me.TxtJobworlLocation.IsUnique = False
        Me.TxtJobworlLocation.Location = New System.Drawing.Point(113, 112)
        Me.TxtJobworlLocation.MendatroryField = True
        Me.TxtJobworlLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtJobworlLocation.MyLinkLable1 = Me.MyLabel1
        Me.TxtJobworlLocation.MyLinkLable2 = Nothing
        Me.TxtJobworlLocation.MyReadOnly = False
        Me.TxtJobworlLocation.MyShowMasterFormButton = False
        Me.TxtJobworlLocation.Name = "TxtJobworlLocation"
        Me.TxtJobworlLocation.ReferenceFieldDesc = Nothing
        Me.TxtJobworlLocation.ReferenceFieldName = Nothing
        Me.TxtJobworlLocation.ReferenceTableName = Nothing
        Me.TxtJobworlLocation.Size = New System.Drawing.Size(318, 19)
        Me.TxtJobworlLocation.TabIndex = 274
        Me.TxtJobworlLocation.Value = ""
        '
        'fndTankerNo
        '
        Me.fndTankerNo.CalculationExpression = Nothing
        Me.fndTankerNo.FieldCode = Nothing
        Me.fndTankerNo.FieldDesc = Nothing
        Me.fndTankerNo.FieldMaxLength = 0
        Me.fndTankerNo.FieldName = Nothing
        Me.fndTankerNo.isCalculatedField = False
        Me.fndTankerNo.IsSourceFromTable = False
        Me.fndTankerNo.IsSourceFromValueList = False
        Me.fndTankerNo.IsUnique = False
        Me.fndTankerNo.Location = New System.Drawing.Point(113, 68)
        Me.fndTankerNo.MendatroryField = True
        Me.fndTankerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTankerNo.MyLinkLable1 = Me.lblVendorBulk
        Me.fndTankerNo.MyLinkLable2 = Nothing
        Me.fndTankerNo.MyReadOnly = False
        Me.fndTankerNo.MyShowMasterFormButton = False
        Me.fndTankerNo.Name = "fndTankerNo"
        Me.fndTankerNo.ReferenceFieldDesc = Nothing
        Me.fndTankerNo.ReferenceFieldName = Nothing
        Me.fndTankerNo.ReferenceTableName = Nothing
        Me.fndTankerNo.Size = New System.Drawing.Size(318, 20)
        Me.fndTankerNo.TabIndex = 273
        Me.fndTankerNo.Value = ""
        '
        'lblVendorBulk
        '
        Me.lblVendorBulk.FieldName = Nothing
        Me.lblVendorBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorBulk.Location = New System.Drawing.Point(11, 135)
        Me.lblVendorBulk.Name = "lblVendorBulk"
        Me.lblVendorBulk.Size = New System.Drawing.Size(43, 16)
        Me.lblVendorBulk.TabIndex = 249
        Me.lblVendorBulk.Text = "Vendor"
        '
        'txtDocType
        '
        Me.txtDocType.AutoSize = False
        Me.txtDocType.BorderVisible = True
        Me.txtDocType.Controls.Add(Me.chkBoth)
        Me.txtDocType.FieldName = Nothing
        Me.txtDocType.Location = New System.Drawing.Point(324, 17)
        Me.txtDocType.Name = "txtDocType"
        Me.txtDocType.Size = New System.Drawing.Size(115, 19)
        Me.txtDocType.TabIndex = 272
        Me.txtDocType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDocType.Visible = False
        '
        'chkBoth
        '
        Me.chkBoth.Location = New System.Drawing.Point(10, -1)
        Me.chkBoth.Name = "chkBoth"
        Me.chkBoth.Size = New System.Drawing.Size(44, 18)
        Me.chkBoth.TabIndex = 2
        Me.chkBoth.Text = "Both"
        Me.chkBoth.Visible = False
        '
        'lblPending
        '
        Me.lblPending.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(655, 4)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(97, 20)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 248
        '
        'fndChallanNoMcc
        '
        Me.fndChallanNoMcc.CalculationExpression = Nothing
        Me.fndChallanNoMcc.Enabled = False
        Me.fndChallanNoMcc.FieldCode = Nothing
        Me.fndChallanNoMcc.FieldDesc = Nothing
        Me.fndChallanNoMcc.FieldMaxLength = 0
        Me.fndChallanNoMcc.FieldName = Nothing
        Me.fndChallanNoMcc.isCalculatedField = False
        Me.fndChallanNoMcc.IsSourceFromTable = False
        Me.fndChallanNoMcc.IsSourceFromValueList = False
        Me.fndChallanNoMcc.IsUnique = False
        Me.fndChallanNoMcc.Location = New System.Drawing.Point(113, 155)
        Me.fndChallanNoMcc.MendatroryField = True
        Me.fndChallanNoMcc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndChallanNoMcc.MyLinkLable1 = Nothing
        Me.fndChallanNoMcc.MyLinkLable2 = Nothing
        Me.fndChallanNoMcc.MyReadOnly = False
        Me.fndChallanNoMcc.MyShowMasterFormButton = False
        Me.fndChallanNoMcc.Name = "fndChallanNoMcc"
        Me.fndChallanNoMcc.ReferenceFieldDesc = Nothing
        Me.fndChallanNoMcc.ReferenceFieldName = Nothing
        Me.fndChallanNoMcc.ReferenceTableName = Nothing
        Me.fndChallanNoMcc.Size = New System.Drawing.Size(318, 20)
        Me.fndChallanNoMcc.TabIndex = 4
        Me.fndChallanNoMcc.Value = ""
        '
        'grpGateEntryType
        '
        Me.grpGateEntryType.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpGateEntryType.Controls.Add(Me.chkBulkMilkProc)
        Me.grpGateEntryType.Controls.Add(Me.chkMccProc)
        Me.grpGateEntryType.HeaderText = "Gate Entry Type"
        Me.grpGateEntryType.Location = New System.Drawing.Point(11, -1)
        Me.grpGateEntryType.Name = "grpGateEntryType"
        Me.grpGateEntryType.Size = New System.Drawing.Size(307, 43)
        Me.grpGateEntryType.TabIndex = 0
        Me.grpGateEntryType.Text = "Gate Entry Type"
        '
        'chkBulkMilkProc
        '
        Me.chkBulkMilkProc.Location = New System.Drawing.Point(5, 19)
        Me.chkBulkMilkProc.Name = "chkBulkMilkProc"
        Me.chkBulkMilkProc.Size = New System.Drawing.Size(94, 18)
        Me.chkBulkMilkProc.TabIndex = 0
        Me.chkBulkMilkProc.Text = "Tanker Receipt"
        '
        'chkMccProc
        '
        Me.chkMccProc.Location = New System.Drawing.Point(160, 19)
        Me.chkMccProc.Name = "chkMccProc"
        Me.chkMccProc.Size = New System.Drawing.Size(78, 18)
        Me.chkMccProc.TabIndex = 1
        Me.chkMccProc.Text = "Sku Receipt"
        '
        'lblTankerNoBulk
        '
        Me.lblTankerNoBulk.FieldName = Nothing
        Me.lblTankerNoBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTankerNoBulk.Location = New System.Drawing.Point(10, 72)
        Me.lblTankerNoBulk.Name = "lblTankerNoBulk"
        Me.lblTankerNoBulk.Size = New System.Drawing.Size(99, 16)
        Me.lblTankerNoBulk.TabIndex = 240
        Me.lblTankerNoBulk.Text = "Tanker/Vehicle No"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPJobWorkOutward.My.Resources.Resources.new1
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(412, 44)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(19, 21)
        Me.btnReset.TabIndex = 30
        '
        'lblDateAndTimeBulk
        '
        Me.lblDateAndTimeBulk.FieldName = Nothing
        Me.lblDateAndTimeBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateAndTimeBulk.Location = New System.Drawing.Point(436, 47)
        Me.lblDateAndTimeBulk.Name = "lblDateAndTimeBulk"
        Me.lblDateAndTimeBulk.Size = New System.Drawing.Size(126, 16)
        Me.lblDateAndTimeBulk.TabIndex = 251
        Me.lblDateAndTimeBulk.Text = "Gate Entry Date && Time"
        '
        'fndGateEntryNO
        '
        Me.fndGateEntryNO.FieldName = Nothing
        Me.fndGateEntryNO.Location = New System.Drawing.Point(113, 44)
        Me.fndGateEntryNO.MendatroryField = False
        Me.fndGateEntryNO.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndGateEntryNO.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndGateEntryNO.MyLinkLable1 = Me.lblGateEntryNO
        Me.fndGateEntryNO.MyLinkLable2 = Nothing
        Me.fndGateEntryNO.MyMaxLength = 32767
        Me.fndGateEntryNO.MyReadOnly = False
        Me.fndGateEntryNO.Name = "fndGateEntryNO"
        Me.fndGateEntryNO.Size = New System.Drawing.Size(299, 21)
        Me.fndGateEntryNO.TabIndex = 0
        Me.fndGateEntryNO.Value = ""
        '
        'lblGateEntryNO
        '
        Me.lblGateEntryNO.FieldName = Nothing
        Me.lblGateEntryNO.Location = New System.Drawing.Point(9, 45)
        Me.lblGateEntryNO.Name = "lblGateEntryNO"
        Me.lblGateEntryNO.Size = New System.Drawing.Size(82, 18)
        Me.lblGateEntryNO.TabIndex = 32
        Me.lblGateEntryNO.Text = "Gate Entry No. "
        '
        'txtTankerNoBulk
        '
        Me.txtTankerNoBulk.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtTankerNoBulk.CalculationExpression = Nothing
        Me.txtTankerNoBulk.FieldCode = Nothing
        Me.txtTankerNoBulk.FieldDesc = Nothing
        Me.txtTankerNoBulk.FieldMaxLength = 0
        Me.txtTankerNoBulk.FieldName = Nothing
        Me.txtTankerNoBulk.isCalculatedField = False
        Me.txtTankerNoBulk.IsSourceFromTable = False
        Me.txtTankerNoBulk.IsSourceFromValueList = False
        Me.txtTankerNoBulk.IsUnique = False
        Me.txtTankerNoBulk.Location = New System.Drawing.Point(113, 68)
        Me.txtTankerNoBulk.MendatroryField = True
        Me.txtTankerNoBulk.MyLinkLable1 = Me.lblTankerNoBulk
        Me.txtTankerNoBulk.MyLinkLable2 = Nothing
        Me.txtTankerNoBulk.Name = "txtTankerNoBulk"
        Me.txtTankerNoBulk.ReferenceFieldDesc = Nothing
        Me.txtTankerNoBulk.ReferenceFieldName = Nothing
        Me.txtTankerNoBulk.ReferenceTableName = Nothing
        Me.txtTankerNoBulk.Size = New System.Drawing.Size(290, 20)
        Me.txtTankerNoBulk.TabIndex = 7
        '
        'lblVendorNameBulk
        '
        Me.lblVendorNameBulk.AutoSize = False
        Me.lblVendorNameBulk.BorderVisible = True
        Me.lblVendorNameBulk.FieldName = Nothing
        Me.lblVendorNameBulk.Location = New System.Drawing.Point(433, 134)
        Me.lblVendorNameBulk.Name = "lblVendorNameBulk"
        Me.lblVendorNameBulk.Size = New System.Drawing.Size(306, 19)
        Me.lblVendorNameBulk.TabIndex = 271
        Me.lblVendorNameBulk.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpDateAndTimeBulk
        '
        Me.dtpDateAndTimeBulk.CalculationExpression = Nothing
        Me.dtpDateAndTimeBulk.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpDateAndTimeBulk.FieldCode = Nothing
        Me.dtpDateAndTimeBulk.FieldDesc = Nothing
        Me.dtpDateAndTimeBulk.FieldMaxLength = 0
        Me.dtpDateAndTimeBulk.FieldName = Nothing
        Me.dtpDateAndTimeBulk.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateAndTimeBulk.isCalculatedField = False
        Me.dtpDateAndTimeBulk.IsSourceFromTable = False
        Me.dtpDateAndTimeBulk.IsSourceFromValueList = False
        Me.dtpDateAndTimeBulk.IsUnique = False
        Me.dtpDateAndTimeBulk.Location = New System.Drawing.Point(565, 46)
        Me.dtpDateAndTimeBulk.MendatroryField = False
        Me.dtpDateAndTimeBulk.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDateAndTimeBulk.MyLinkLable1 = Me.lblDateAndTimeBulk
        Me.dtpDateAndTimeBulk.MyLinkLable2 = Nothing
        Me.dtpDateAndTimeBulk.Name = "dtpDateAndTimeBulk"
        Me.dtpDateAndTimeBulk.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDateAndTimeBulk.ReferenceFieldDesc = Nothing
        Me.dtpDateAndTimeBulk.ReferenceFieldName = Nothing
        Me.dtpDateAndTimeBulk.ReferenceTableName = Nothing
        Me.dtpDateAndTimeBulk.Size = New System.Drawing.Size(131, 20)
        Me.dtpDateAndTimeBulk.TabIndex = 1
        Me.dtpDateAndTimeBulk.TabStop = False
        Me.dtpDateAndTimeBulk.Text = "10/06/2011 11:51 AM"
        Me.dtpDateAndTimeBulk.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'txtChallanNoBulk
        '
        Me.txtChallanNoBulk.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtChallanNoBulk.CalculationExpression = Nothing
        Me.txtChallanNoBulk.FieldCode = Nothing
        Me.txtChallanNoBulk.FieldDesc = Nothing
        Me.txtChallanNoBulk.FieldMaxLength = 0
        Me.txtChallanNoBulk.FieldName = Nothing
        Me.txtChallanNoBulk.isCalculatedField = False
        Me.txtChallanNoBulk.IsSourceFromTable = False
        Me.txtChallanNoBulk.IsSourceFromValueList = False
        Me.txtChallanNoBulk.IsUnique = False
        Me.txtChallanNoBulk.Location = New System.Drawing.Point(112, 155)
        Me.txtChallanNoBulk.MendatroryField = True
        Me.txtChallanNoBulk.MyLinkLable1 = Me.lblChallanNoBulk
        Me.txtChallanNoBulk.MyLinkLable2 = Nothing
        Me.txtChallanNoBulk.Name = "txtChallanNoBulk"
        Me.txtChallanNoBulk.ReferenceFieldDesc = Nothing
        Me.txtChallanNoBulk.ReferenceFieldName = Nothing
        Me.txtChallanNoBulk.ReferenceTableName = Nothing
        Me.txtChallanNoBulk.Size = New System.Drawing.Size(290, 20)
        Me.txtChallanNoBulk.TabIndex = 3
        '
        'lblChallanNoBulk
        '
        Me.lblChallanNoBulk.FieldName = Nothing
        Me.lblChallanNoBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChallanNoBulk.Location = New System.Drawing.Point(10, 156)
        Me.lblChallanNoBulk.Name = "lblChallanNoBulk"
        Me.lblChallanNoBulk.Size = New System.Drawing.Size(62, 16)
        Me.lblChallanNoBulk.TabIndex = 263
        Me.lblChallanNoBulk.Text = "Challan No"
        '
        'lblLocationBulk
        '
        Me.lblLocationBulk.FieldName = Nothing
        Me.lblLocationBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationBulk.Location = New System.Drawing.Point(11, 90)
        Me.lblLocationBulk.Name = "lblLocationBulk"
        Me.lblLocationBulk.Size = New System.Drawing.Size(49, 16)
        Me.lblLocationBulk.TabIndex = 246
        Me.lblLocationBulk.Text = "Location"
        '
        'fndVendorBulk
        '
        Me.fndVendorBulk.CalculationExpression = Nothing
        Me.fndVendorBulk.FieldCode = Nothing
        Me.fndVendorBulk.FieldDesc = Nothing
        Me.fndVendorBulk.FieldMaxLength = 0
        Me.fndVendorBulk.FieldName = Nothing
        Me.fndVendorBulk.isCalculatedField = False
        Me.fndVendorBulk.IsSourceFromTable = False
        Me.fndVendorBulk.IsSourceFromValueList = False
        Me.fndVendorBulk.IsUnique = False
        Me.fndVendorBulk.Location = New System.Drawing.Point(112, 134)
        Me.fndVendorBulk.MendatroryField = True
        Me.fndVendorBulk.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVendorBulk.MyLinkLable1 = Me.lblVendorBulk
        Me.fndVendorBulk.MyLinkLable2 = Nothing
        Me.fndVendorBulk.MyReadOnly = False
        Me.fndVendorBulk.MyShowMasterFormButton = False
        Me.fndVendorBulk.Name = "fndVendorBulk"
        Me.fndVendorBulk.ReferenceFieldDesc = Nothing
        Me.fndVendorBulk.ReferenceFieldName = Nothing
        Me.fndVendorBulk.ReferenceTableName = Nothing
        Me.fndVendorBulk.Size = New System.Drawing.Size(318, 19)
        Me.fndVendorBulk.TabIndex = 6
        Me.fndVendorBulk.Value = ""
        '
        'dtpChallanDateBulk
        '
        Me.dtpChallanDateBulk.CalculationExpression = Nothing
        Me.dtpChallanDateBulk.CustomFormat = "dd/MM/yyyy"
        Me.dtpChallanDateBulk.FieldCode = Nothing
        Me.dtpChallanDateBulk.FieldDesc = Nothing
        Me.dtpChallanDateBulk.FieldMaxLength = 0
        Me.dtpChallanDateBulk.FieldName = Nothing
        Me.dtpChallanDateBulk.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChallanDateBulk.isCalculatedField = False
        Me.dtpChallanDateBulk.IsSourceFromTable = False
        Me.dtpChallanDateBulk.IsSourceFromValueList = False
        Me.dtpChallanDateBulk.IsUnique = False
        Me.dtpChallanDateBulk.Location = New System.Drawing.Point(507, 157)
        Me.dtpChallanDateBulk.MendatroryField = False
        Me.dtpChallanDateBulk.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpChallanDateBulk.MyLinkLable1 = Me.lblChallanDateBulk
        Me.dtpChallanDateBulk.MyLinkLable2 = Nothing
        Me.dtpChallanDateBulk.Name = "dtpChallanDateBulk"
        Me.dtpChallanDateBulk.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpChallanDateBulk.ReferenceFieldDesc = Nothing
        Me.dtpChallanDateBulk.ReferenceFieldName = Nothing
        Me.dtpChallanDateBulk.ReferenceTableName = Nothing
        Me.dtpChallanDateBulk.Size = New System.Drawing.Size(84, 20)
        Me.dtpChallanDateBulk.TabIndex = 5
        Me.dtpChallanDateBulk.TabStop = False
        Me.dtpChallanDateBulk.Text = "10/06/2011"
        Me.dtpChallanDateBulk.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblChallanDateBulk
        '
        Me.lblChallanDateBulk.FieldName = Nothing
        Me.lblChallanDateBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChallanDateBulk.Location = New System.Drawing.Point(431, 159)
        Me.lblChallanDateBulk.Name = "lblChallanDateBulk"
        Me.lblChallanDateBulk.Size = New System.Drawing.Size(72, 16)
        Me.lblChallanDateBulk.TabIndex = 261
        Me.lblChallanDateBulk.Text = "Challan Date"
        '
        'lblLocationDecBulk
        '
        Me.lblLocationDecBulk.AutoSize = False
        Me.lblLocationDecBulk.BorderVisible = True
        Me.lblLocationDecBulk.FieldName = Nothing
        Me.lblLocationDecBulk.Location = New System.Drawing.Point(432, 90)
        Me.lblLocationDecBulk.Name = "lblLocationDecBulk"
        Me.lblLocationDecBulk.Size = New System.Drawing.Size(307, 19)
        Me.lblLocationDecBulk.TabIndex = 271
        Me.lblLocationDecBulk.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fndLocationBulk
        '
        Me.fndLocationBulk.CalculationExpression = Nothing
        Me.fndLocationBulk.FieldCode = Nothing
        Me.fndLocationBulk.FieldDesc = Nothing
        Me.fndLocationBulk.FieldMaxLength = 0
        Me.fndLocationBulk.FieldName = Nothing
        Me.fndLocationBulk.isCalculatedField = False
        Me.fndLocationBulk.IsSourceFromTable = False
        Me.fndLocationBulk.IsSourceFromValueList = False
        Me.fndLocationBulk.IsUnique = False
        Me.fndLocationBulk.Location = New System.Drawing.Point(113, 90)
        Me.fndLocationBulk.MendatroryField = True
        Me.fndLocationBulk.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocationBulk.MyLinkLable1 = Me.lblLocationBulk
        Me.fndLocationBulk.MyLinkLable2 = Nothing
        Me.fndLocationBulk.MyReadOnly = False
        Me.fndLocationBulk.MyShowMasterFormButton = False
        Me.fndLocationBulk.Name = "fndLocationBulk"
        Me.fndLocationBulk.ReferenceFieldDesc = Nothing
        Me.fndLocationBulk.ReferenceFieldName = Nothing
        Me.fndLocationBulk.ReferenceTableName = Nothing
        Me.fndLocationBulk.Size = New System.Drawing.Size(318, 19)
        Me.fndLocationBulk.TabIndex = 2
        Me.fndLocationBulk.Value = ""
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gvItemBulk)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = "Item Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(758, 272)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "Item Details"
        '
        'gvItemBulk
        '
        Me.gvItemBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvItemBulk.Location = New System.Drawing.Point(2, 18)
        '
        'gvItemBulk
        '
        Me.gvItemBulk.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvItemBulk.Name = "gvItemBulk"
        Me.gvItemBulk.ShowHeaderCellButtons = True
        Me.gvItemBulk.Size = New System.Drawing.Size(754, 252)
        Me.gvItemBulk.TabIndex = 264
        Me.gvItemBulk.Text = "RadGridView1"
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(215, 7)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(68, 18)
        Me.btnReverse.TabIndex = 3
        Me.btnReverse.Text = "Reverse"
        Me.btnReverse.Visible = False
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(144, 7)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 18)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(73, 7)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(684, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(79.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(758, 511)
        Me.RadPageViewPage3.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(758, 511)
        Me.UcAttachment1.TabIndex = 1
        Me.UcAttachment1.TabStop = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.SplitContainer3)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(55.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(758, 511)
        Me.RadPageViewPage2.Text = "Seal No"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadGroupBox3)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer3.Size = New System.Drawing.Size(758, 511)
        Me.SplitContainer3.SplitterDistance = 373
        Me.SplitContainer3.TabIndex = 0
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.gvManualSeal)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.HeaderText = "Manual Seal"
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(373, 511)
        Me.RadGroupBox3.TabIndex = 218
        Me.RadGroupBox3.Text = "Manual Seal"
        '
        'gvManualSeal
        '
        Me.gvManualSeal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvManualSeal.Location = New System.Drawing.Point(2, 18)
        '
        'gvManualSeal
        '
        Me.gvManualSeal.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvManualSeal.Name = "gvManualSeal"
        Me.gvManualSeal.ShowHeaderCellButtons = True
        Me.gvManualSeal.Size = New System.Drawing.Size(369, 491)
        Me.gvManualSeal.TabIndex = 202
        Me.gvManualSeal.Text = "RadGridView1"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gvPaperSeal)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.HeaderText = "Paper Seal"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(381, 511)
        Me.RadGroupBox2.TabIndex = 217
        Me.RadGroupBox2.Text = "Paper Seal"
        '
        'gvPaperSeal
        '
        Me.gvPaperSeal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvPaperSeal.Location = New System.Drawing.Point(2, 18)
        '
        '
        '
        Me.gvPaperSeal.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvPaperSeal.Name = "gvPaperSeal"
        Me.gvPaperSeal.ShowHeaderCellButtons = True
        Me.gvPaperSeal.Size = New System.Drawing.Size(377, 491)
        Me.gvPaperSeal.TabIndex = 202
        Me.gvPaperSeal.Text = "RadGridView1"
        '
        'btnPrintNew
        '
        Me.btnPrintNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintNew.Location = New System.Drawing.Point(290, 7)
        Me.btnPrintNew.Name = "btnPrintNew"
        Me.btnPrintNew.Size = New System.Drawing.Size(69, 18)
        Me.btnPrintNew.TabIndex = 9
        Me.btnPrintNew.Text = "Print"
        '
        'FrmMilkGateEntry_JWO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(779, 588)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmMilkGateEntry_JWO"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Milk Gate Entry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel1.PerformLayout()
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblJobworkLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.txtDocType.ResumeLayout(False)
        Me.txtDocType.PerformLayout()
        CType(Me.chkBoth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpGateEntryType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpGateEntryType.ResumeLayout(False)
        Me.grpGateEntryType.PerformLayout()
        CType(Me.chkBulkMilkProc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMccProc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNoBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDateAndTimeBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGateEntryNO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTankerNoBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorNameBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDateAndTimeBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChallanNoBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChallanNoBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpChallanDateBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChallanDateBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDecBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gvItemBulk.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItemBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        CType(Me.gvManualSeal.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvManualSeal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gvPaperSeal.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPaperSeal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrintNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents chkMccProc As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkBulkMilkProc As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents grpGateEntryType As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents fndGateEntryNO As common.UserControls.txtNavigator
    Friend WithEvents lblGateEntryNO As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents lblLocationBulk As common.Controls.MyLabel
    Friend WithEvents fndLocationBulk As common.UserControls.txtFinder
    Friend WithEvents txtTankerNoBulk As common.Controls.MyTextBox
    Friend WithEvents lblTankerNoBulk As common.Controls.MyLabel
    Friend WithEvents lblDateAndTimeBulk As common.Controls.MyLabel
    Friend WithEvents dtpDateAndTimeBulk As common.Controls.MyDateTimePicker
    Friend WithEvents lblVendorBulk As common.Controls.MyLabel
    Friend WithEvents fndVendorBulk As common.UserControls.txtFinder
    Friend WithEvents txtChallanNoBulk As common.Controls.MyTextBox
    Friend WithEvents lblChallanNoBulk As common.Controls.MyLabel
    Friend WithEvents lblChallanDateBulk As common.Controls.MyLabel
    Friend WithEvents dtpChallanDateBulk As common.Controls.MyDateTimePicker
    Friend WithEvents gvItemBulk As common.UserControls.MyRadGridView
    Friend WithEvents lblLocationDecBulk As common.Controls.MyLabel
    Friend WithEvents lblVendorNameBulk As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents fndChallanNoMcc As common.UserControls.txtFinder
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents mnuSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuExit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvManualSeal As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvPaperSeal As common.UserControls.MyRadGridView
    Friend WithEvents chkBoth As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtDocType As common.Controls.MyLabel
    Friend WithEvents fndTankerNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblJobworkLocation As common.Controls.MyLabel
    Friend WithEvents TxtJobworlLocation As common.UserControls.txtFinder
    Friend WithEvents RadPageViewPage3 As RadPageViewPage
    Friend WithEvents UcAttachment1 As ucAttachment
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtTransferNo As common.UserControls.txtMultiSelectFinder
    Friend WithEvents btnPrintNew As RadButton
End Class

