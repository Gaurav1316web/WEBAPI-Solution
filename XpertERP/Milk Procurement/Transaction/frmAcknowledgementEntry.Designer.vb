<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAcknowledgementEntry
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.mnuSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuSaveLayOut = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView2 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage6 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage7 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvAE = New common.UserControls.MyRadGridView()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtTankerTransporterName = New common.Controls.MyTextBox()
        Me.lblMCCName = New common.Controls.MyLabel()
        Me.lblPending = New common.usLock()
        Me.lblFromPlantName = New common.Controls.MyTextBox()
        Me.rbtnReset = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fndDocNo = New common.UserControls.txtNavigator()
        Me.lChalanNo = New common.Controls.MyLabel()
        Me.fndMCCCode = New common.UserControls.txtFinder()
        Me.lblPlantOrMccCode = New common.Controls.MyLabel()
        Me.lblDateAndTime = New common.Controls.MyLabel()
        Me.lblTankerNo = New common.Controls.MyLabel()
        Me.dtpDateAndTime = New common.Controls.MyDateTimePicker()
        Me.fndTnakerNo = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtdispatchDate = New common.Controls.MyDateTimePicker()
        Me.txtPlantOrMccName = New common.Controls.MyTextBox()
        Me.txtMCCName = New common.Controls.MyTextBox()
        Me.txtTankerDispatch = New common.UserControls.txtFinder()
        Me.fndPlantOrMCCCode = New common.UserControls.txtFinder()
        Me.ddlTankerDispatchTo = New common.Controls.MyComboBox()
        Me.lblTankerDispatchTo = New common.Controls.MyLabel()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.xxx = New Telerik.WinControls.UI.MasterGridViewTemplate()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SplitContainer6 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.txtQCNo = New common.Controls.MyTextBox()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.RadPageViewPage8 = New Telerik.WinControls.UI.RadPageViewPage()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadPageView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView2.SuspendLayout()
        Me.RadPageViewPage6.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage7.SuspendLayout()
        CType(Me.gvAE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAE.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtTankerTransporterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromPlantName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lChalanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPlantOrMccCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDateAndTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDateAndTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdispatchDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPlantOrMccName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMCCName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlTankerDispatchTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerDispatchTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xxx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer6.SuspendLayout()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQCNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage8.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuSetting})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1261, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'mnuSetting
        '
        Me.mnuSetting.AccessibleDescription = "Setting"
        Me.mnuSetting.AccessibleName = "Setting"
        Me.mnuSetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuSaveLayOut, Me.mnuDeleteLayout})
        Me.mnuSetting.Name = "mnuSetting"
        Me.mnuSetting.Text = "Setting"
        '
        'mnuSaveLayOut
        '
        Me.mnuSaveLayOut.AccessibleDescription = "Save Layout"
        Me.mnuSaveLayOut.AccessibleName = "Save Layout"
        Me.mnuSaveLayOut.Name = "mnuSaveLayOut"
        Me.mnuSaveLayOut.Text = "Save Layout"
        '
        'mnuDeleteLayout
        '
        Me.mnuDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.mnuDeleteLayout.AccessibleName = "Delete Layout"
        Me.mnuDeleteLayout.Name = "mnuDeleteLayout"
        Me.mnuDeleteLayout.Text = "Delete Layout"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadPageView2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer2.Size = New System.Drawing.Size(1261, 442)
        Me.SplitContainer2.SplitterDistance = 410
        Me.SplitContainer2.TabIndex = 0
        '
        'RadPageView2
        '
        Me.RadPageView2.Controls.Add(Me.RadPageViewPage6)
        Me.RadPageView2.Controls.Add(Me.RadPageViewPage7)
        Me.RadPageView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView2.Location = New System.Drawing.Point(0, 190)
        Me.RadPageView2.Name = "RadPageView2"
        Me.RadPageView2.SelectedPage = Me.RadPageViewPage6
        Me.RadPageView2.Size = New System.Drawing.Size(1261, 220)
        Me.RadPageView2.TabIndex = 2
        Me.RadPageView2.Text = "RadPageView2"
        CType(Me.RadPageView2.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage6
        '
        Me.RadPageViewPage6.Controls.Add(Me.gv)
        Me.RadPageViewPage6.ItemSize = New System.Drawing.SizeF(133.0!, 28.0!)
        Me.RadPageViewPage6.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage6.Name = "RadPageViewPage6"
        Me.RadPageViewPage6.Size = New System.Drawing.Size(1240, 172)
        Me.RadPageViewPage6.Tag = ""
        Me.RadPageViewPage6.Text = "Tanker Dispatch Details"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        'gv
        '
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.ReadOnly = True
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(1240, 172)
        Me.gv.TabIndex = 203
        Me.gv.Text = "RadGridView1"
        '
        'RadPageViewPage7
        '
        Me.RadPageViewPage7.Controls.Add(Me.gvAE)
        Me.RadPageViewPage7.ItemSize = New System.Drawing.SizeF(146.0!, 28.0!)
        Me.RadPageViewPage7.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage7.Name = "RadPageViewPage7"
        Me.RadPageViewPage7.Size = New System.Drawing.Size(1240, 172)
        Me.RadPageViewPage7.Text = "Acknowledgement Details"
        '
        'gvAE
        '
        Me.gvAE.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvAE.Location = New System.Drawing.Point(0, 0)
        '
        'gvAE
        '
        Me.gvAE.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvAE.Name = "gvAE"
        Me.gvAE.ShowHeaderCellButtons = True
        Me.gvAE.Size = New System.Drawing.Size(1240, 172)
        Me.gvAE.TabIndex = 203
        Me.gvAE.Text = "RadGridView1"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1261, 190)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(55.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1240, 142)
        Me.RadPageViewPage1.Text = "General"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtTankerTransporterName)
        Me.RadGroupBox1.Controls.Add(Me.lblPending)
        Me.RadGroupBox1.Controls.Add(Me.lblFromPlantName)
        Me.RadGroupBox1.Controls.Add(Me.rbtnReset)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.fndDocNo)
        Me.RadGroupBox1.Controls.Add(Me.fndMCCCode)
        Me.RadGroupBox1.Controls.Add(Me.lblDateAndTime)
        Me.RadGroupBox1.Controls.Add(Me.lblTankerNo)
        Me.RadGroupBox1.Controls.Add(Me.lChalanNo)
        Me.RadGroupBox1.Controls.Add(Me.dtpDateAndTime)
        Me.RadGroupBox1.Controls.Add(Me.fndTnakerNo)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtdispatchDate)
        Me.RadGroupBox1.Controls.Add(Me.txtPlantOrMccName)
        Me.RadGroupBox1.Controls.Add(Me.lblPlantOrMccCode)
        Me.RadGroupBox1.Controls.Add(Me.lblMCCName)
        Me.RadGroupBox1.Controls.Add(Me.txtMCCName)
        Me.RadGroupBox1.Controls.Add(Me.txtTankerDispatch)
        Me.RadGroupBox1.Controls.Add(Me.fndPlantOrMCCCode)
        Me.RadGroupBox1.Controls.Add(Me.ddlTankerDispatchTo)
        Me.RadGroupBox1.Controls.Add(Me.lblTankerDispatchTo)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(1240, 142)
        Me.RadGroupBox1.TabIndex = 0
        '
        'txtTankerTransporterName
        '
        Me.txtTankerTransporterName.CalculationExpression = Nothing
        Me.txtTankerTransporterName.FieldCode = Nothing
        Me.txtTankerTransporterName.FieldDesc = Nothing
        Me.txtTankerTransporterName.FieldMaxLength = 0
        Me.txtTankerTransporterName.FieldName = Nothing
        Me.txtTankerTransporterName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTankerTransporterName.isCalculatedField = False
        Me.txtTankerTransporterName.IsSourceFromTable = False
        Me.txtTankerTransporterName.IsSourceFromValueList = False
        Me.txtTankerTransporterName.IsUnique = False
        Me.txtTankerTransporterName.Location = New System.Drawing.Point(295, 98)
        Me.txtTankerTransporterName.MaxLength = 50
        Me.txtTankerTransporterName.MendatroryField = False
        Me.txtTankerTransporterName.MyLinkLable1 = Me.lblMCCName
        Me.txtTankerTransporterName.MyLinkLable2 = Nothing
        Me.txtTankerTransporterName.Name = "txtTankerTransporterName"
        Me.txtTankerTransporterName.ReadOnly = True
        Me.txtTankerTransporterName.ReferenceFieldDesc = Nothing
        Me.txtTankerTransporterName.ReferenceFieldName = Nothing
        Me.txtTankerTransporterName.ReferenceTableName = Nothing
        Me.txtTankerTransporterName.Size = New System.Drawing.Size(251, 18)
        Me.txtTankerTransporterName.TabIndex = 339
        '
        'lblMCCName
        '
        Me.lblMCCName.FieldName = Nothing
        Me.lblMCCName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMCCName.Location = New System.Drawing.Point(5, 55)
        Me.lblMCCName.Name = "lblMCCName"
        Me.lblMCCName.Size = New System.Drawing.Size(61, 16)
        Me.lblMCCName.TabIndex = 11
        Me.lblMCCName.Text = "MCC/Plant"
        '
        'lblPending
        '
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(859, 6)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(94, 18)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 209
        '
        'lblFromPlantName
        '
        Me.lblFromPlantName.CalculationExpression = Nothing
        Me.lblFromPlantName.FieldCode = Nothing
        Me.lblFromPlantName.FieldDesc = Nothing
        Me.lblFromPlantName.FieldMaxLength = 0
        Me.lblFromPlantName.FieldName = Nothing
        Me.lblFromPlantName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromPlantName.isCalculatedField = False
        Me.lblFromPlantName.IsSourceFromTable = False
        Me.lblFromPlantName.IsSourceFromValueList = False
        Me.lblFromPlantName.IsUnique = False
        Me.lblFromPlantName.Location = New System.Drawing.Point(520, 53)
        Me.lblFromPlantName.MaxLength = 50
        Me.lblFromPlantName.MendatroryField = False
        Me.lblFromPlantName.MyLinkLable1 = Me.lblMCCName
        Me.lblFromPlantName.MyLinkLable2 = Nothing
        Me.lblFromPlantName.Name = "lblFromPlantName"
        Me.lblFromPlantName.ReadOnly = True
        Me.lblFromPlantName.ReferenceFieldDesc = Nothing
        Me.lblFromPlantName.ReferenceFieldName = Nothing
        Me.lblFromPlantName.ReferenceTableName = Nothing
        Me.lblFromPlantName.Size = New System.Drawing.Size(201, 18)
        Me.lblFromPlantName.TabIndex = 1423
        '
        'rbtnReset
        '
        Me.rbtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.rbtnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtnReset.Location = New System.Drawing.Point(463, 6)
        Me.rbtnReset.Name = "rbtnReset"
        Me.rbtnReset.Size = New System.Drawing.Size(20, 21)
        Me.rbtnReset.TabIndex = 130
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(294, 33)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel2.TabIndex = 1427
        Me.MyLabel2.Text = "Dispatch Date"
        '
        'fndDocNo
        '
        Me.fndDocNo.FieldName = Nothing
        Me.fndDocNo.Location = New System.Drawing.Point(137, 6)
        Me.fndDocNo.MendatroryField = True
        Me.fndDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocNo.MyLinkLable1 = Me.lChalanNo
        Me.fndDocNo.MyLinkLable2 = Nothing
        Me.fndDocNo.MyMaxLength = 32767
        Me.fndDocNo.MyReadOnly = False
        Me.fndDocNo.Name = "fndDocNo"
        Me.fndDocNo.Size = New System.Drawing.Size(326, 21)
        Me.fndDocNo.TabIndex = 1
        Me.fndDocNo.Value = ""
        '
        'lChalanNo
        '
        Me.lChalanNo.FieldName = Nothing
        Me.lChalanNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lChalanNo.Location = New System.Drawing.Point(4, 8)
        Me.lChalanNo.Name = "lChalanNo"
        Me.lChalanNo.Size = New System.Drawing.Size(75, 16)
        Me.lChalanNo.TabIndex = 131
        Me.lChalanNo.Text = "Document No"
        '
        'fndMCCCode
        '
        Me.fndMCCCode.CalculationExpression = Nothing
        Me.fndMCCCode.Enabled = False
        Me.fndMCCCode.FieldCode = Nothing
        Me.fndMCCCode.FieldDesc = Nothing
        Me.fndMCCCode.FieldMaxLength = 0
        Me.fndMCCCode.FieldName = Nothing
        Me.fndMCCCode.isCalculatedField = False
        Me.fndMCCCode.IsSourceFromTable = False
        Me.fndMCCCode.IsSourceFromValueList = False
        Me.fndMCCCode.IsUnique = False
        Me.fndMCCCode.Location = New System.Drawing.Point(137, 54)
        Me.fndMCCCode.MendatroryField = True
        Me.fndMCCCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndMCCCode.MyLinkLable1 = Me.lblPlantOrMccCode
        Me.fndMCCCode.MyLinkLable2 = Nothing
        Me.fndMCCCode.MyReadOnly = True
        Me.fndMCCCode.MyShowMasterFormButton = False
        Me.fndMCCCode.Name = "fndMCCCode"
        Me.fndMCCCode.ReferenceFieldDesc = Nothing
        Me.fndMCCCode.ReferenceFieldName = Nothing
        Me.fndMCCCode.ReferenceTableName = Nothing
        Me.fndMCCCode.Size = New System.Drawing.Size(152, 19)
        Me.fndMCCCode.TabIndex = 340
        Me.fndMCCCode.Value = ""
        '
        'lblPlantOrMccCode
        '
        Me.lblPlantOrMccCode.FieldName = Nothing
        Me.lblPlantOrMccCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlantOrMccCode.Location = New System.Drawing.Point(294, 77)
        Me.lblPlantOrMccCode.Name = "lblPlantOrMccCode"
        Me.lblPlantOrMccCode.Size = New System.Drawing.Size(96, 16)
        Me.lblPlantOrMccCode.TabIndex = 135
        Me.lblPlantOrMccCode.Text = "Select Plant/MCC"
        '
        'lblDateAndTime
        '
        Me.lblDateAndTime.FieldName = Nothing
        Me.lblDateAndTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateAndTime.Location = New System.Drawing.Point(486, 8)
        Me.lblDateAndTime.Name = "lblDateAndTime"
        Me.lblDateAndTime.Size = New System.Drawing.Size(30, 16)
        Me.lblDateAndTime.TabIndex = 128
        Me.lblDateAndTime.Text = "Date"
        '
        'lblTankerNo
        '
        Me.lblTankerNo.FieldName = Nothing
        Me.lblTankerNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTankerNo.Location = New System.Drawing.Point(5, 99)
        Me.lblTankerNo.Name = "lblTankerNo"
        Me.lblTankerNo.Size = New System.Drawing.Size(62, 16)
        Me.lblTankerNo.TabIndex = 138
        Me.lblTankerNo.Text = "Tanker No."
        '
        'dtpDateAndTime
        '
        Me.dtpDateAndTime.CalculationExpression = Nothing
        Me.dtpDateAndTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpDateAndTime.FieldCode = Nothing
        Me.dtpDateAndTime.FieldDesc = Nothing
        Me.dtpDateAndTime.FieldMaxLength = 0
        Me.dtpDateAndTime.FieldName = Nothing
        Me.dtpDateAndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateAndTime.isCalculatedField = False
        Me.dtpDateAndTime.IsSourceFromTable = False
        Me.dtpDateAndTime.IsSourceFromValueList = False
        Me.dtpDateAndTime.IsUnique = False
        Me.dtpDateAndTime.Location = New System.Drawing.Point(520, 6)
        Me.dtpDateAndTime.MendatroryField = False
        Me.dtpDateAndTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDateAndTime.MyLinkLable1 = Me.lblDateAndTime
        Me.dtpDateAndTime.MyLinkLable2 = Nothing
        Me.dtpDateAndTime.Name = "dtpDateAndTime"
        Me.dtpDateAndTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDateAndTime.ReferenceFieldDesc = Nothing
        Me.dtpDateAndTime.ReferenceFieldName = Nothing
        Me.dtpDateAndTime.ReferenceTableName = Nothing
        Me.dtpDateAndTime.Size = New System.Drawing.Size(132, 20)
        Me.dtpDateAndTime.TabIndex = 2
        Me.dtpDateAndTime.TabStop = False
        Me.dtpDateAndTime.Text = "10/06/2011 11:51 AM"
        Me.dtpDateAndTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'fndTnakerNo
        '
        Me.fndTnakerNo.CalculationExpression = Nothing
        Me.fndTnakerNo.Enabled = False
        Me.fndTnakerNo.FieldCode = Nothing
        Me.fndTnakerNo.FieldDesc = Nothing
        Me.fndTnakerNo.FieldMaxLength = 0
        Me.fndTnakerNo.FieldName = Nothing
        Me.fndTnakerNo.isCalculatedField = False
        Me.fndTnakerNo.IsSourceFromTable = False
        Me.fndTnakerNo.IsSourceFromValueList = False
        Me.fndTnakerNo.IsUnique = False
        Me.fndTnakerNo.Location = New System.Drawing.Point(137, 98)
        Me.fndTnakerNo.MendatroryField = True
        Me.fndTnakerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTnakerNo.MyLinkLable1 = Me.lblTankerNo
        Me.fndTnakerNo.MyLinkLable2 = Nothing
        Me.fndTnakerNo.MyReadOnly = True
        Me.fndTnakerNo.MyShowMasterFormButton = False
        Me.fndTnakerNo.Name = "fndTnakerNo"
        Me.fndTnakerNo.ReferenceFieldDesc = Nothing
        Me.fndTnakerNo.ReferenceFieldName = Nothing
        Me.fndTnakerNo.ReferenceTableName = Nothing
        Me.fndTnakerNo.Size = New System.Drawing.Size(152, 19)
        Me.fndTnakerNo.TabIndex = 5
        Me.fndTnakerNo.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(5, 33)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(130, 16)
        Me.MyLabel1.TabIndex = 1425
        Me.MyLabel1.Text = "Tanker Dispatch Doc No"
        '
        'txtdispatchDate
        '
        Me.txtdispatchDate.CalculationExpression = Nothing
        Me.txtdispatchDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtdispatchDate.FieldCode = Nothing
        Me.txtdispatchDate.FieldDesc = Nothing
        Me.txtdispatchDate.FieldMaxLength = 0
        Me.txtdispatchDate.FieldName = Nothing
        Me.txtdispatchDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtdispatchDate.isCalculatedField = False
        Me.txtdispatchDate.IsSourceFromTable = False
        Me.txtdispatchDate.IsSourceFromValueList = False
        Me.txtdispatchDate.IsUnique = False
        Me.txtdispatchDate.Location = New System.Drawing.Point(396, 31)
        Me.txtdispatchDate.MendatroryField = False
        Me.txtdispatchDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdispatchDate.MyLinkLable1 = Me.MyLabel2
        Me.txtdispatchDate.MyLinkLable2 = Nothing
        Me.txtdispatchDate.Name = "txtdispatchDate"
        Me.txtdispatchDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdispatchDate.ReferenceFieldDesc = Nothing
        Me.txtdispatchDate.ReferenceFieldName = Nothing
        Me.txtdispatchDate.ReferenceTableName = Nothing
        Me.txtdispatchDate.Size = New System.Drawing.Size(146, 20)
        Me.txtdispatchDate.TabIndex = 1426
        Me.txtdispatchDate.TabStop = False
        Me.txtdispatchDate.Text = "10/06/2011 11:51 AM"
        Me.txtdispatchDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'txtPlantOrMccName
        '
        Me.txtPlantOrMccName.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtPlantOrMccName.CalculationExpression = Nothing
        Me.txtPlantOrMccName.Enabled = False
        Me.txtPlantOrMccName.FieldCode = Nothing
        Me.txtPlantOrMccName.FieldDesc = Nothing
        Me.txtPlantOrMccName.FieldMaxLength = 0
        Me.txtPlantOrMccName.FieldName = Nothing
        Me.txtPlantOrMccName.isCalculatedField = False
        Me.txtPlantOrMccName.IsSourceFromTable = False
        Me.txtPlantOrMccName.IsSourceFromValueList = False
        Me.txtPlantOrMccName.IsUnique = False
        Me.txtPlantOrMccName.Location = New System.Drawing.Point(547, 75)
        Me.txtPlantOrMccName.MendatroryField = False
        Me.txtPlantOrMccName.MyLinkLable1 = Nothing
        Me.txtPlantOrMccName.MyLinkLable2 = Nothing
        Me.txtPlantOrMccName.Name = "txtPlantOrMccName"
        Me.txtPlantOrMccName.ReferenceFieldDesc = Nothing
        Me.txtPlantOrMccName.ReferenceFieldName = Nothing
        Me.txtPlantOrMccName.ReferenceTableName = Nothing
        Me.txtPlantOrMccName.Size = New System.Drawing.Size(288, 20)
        Me.txtPlantOrMccName.TabIndex = 5
        '
        'txtMCCName
        '
        Me.txtMCCName.CalculationExpression = Nothing
        Me.txtMCCName.FieldCode = Nothing
        Me.txtMCCName.FieldDesc = Nothing
        Me.txtMCCName.FieldMaxLength = 0
        Me.txtMCCName.FieldName = Nothing
        Me.txtMCCName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCCName.isCalculatedField = False
        Me.txtMCCName.IsSourceFromTable = False
        Me.txtMCCName.IsSourceFromValueList = False
        Me.txtMCCName.IsUnique = False
        Me.txtMCCName.Location = New System.Drawing.Point(295, 54)
        Me.txtMCCName.MaxLength = 50
        Me.txtMCCName.MendatroryField = False
        Me.txtMCCName.MyLinkLable1 = Me.lblMCCName
        Me.txtMCCName.MyLinkLable2 = Nothing
        Me.txtMCCName.Name = "txtMCCName"
        Me.txtMCCName.ReadOnly = True
        Me.txtMCCName.ReferenceFieldDesc = Nothing
        Me.txtMCCName.ReferenceFieldName = Nothing
        Me.txtMCCName.ReferenceTableName = Nothing
        Me.txtMCCName.Size = New System.Drawing.Size(222, 18)
        Me.txtMCCName.TabIndex = 0
        '
        'txtTankerDispatch
        '
        Me.txtTankerDispatch.CalculationExpression = Nothing
        Me.txtTankerDispatch.FieldCode = Nothing
        Me.txtTankerDispatch.FieldDesc = Nothing
        Me.txtTankerDispatch.FieldMaxLength = 0
        Me.txtTankerDispatch.FieldName = Nothing
        Me.txtTankerDispatch.isCalculatedField = False
        Me.txtTankerDispatch.IsSourceFromTable = False
        Me.txtTankerDispatch.IsSourceFromValueList = False
        Me.txtTankerDispatch.IsUnique = False
        Me.txtTankerDispatch.Location = New System.Drawing.Point(137, 32)
        Me.txtTankerDispatch.MendatroryField = True
        Me.txtTankerDispatch.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTankerDispatch.MyLinkLable1 = Me.MyLabel1
        Me.txtTankerDispatch.MyLinkLable2 = Nothing
        Me.txtTankerDispatch.MyReadOnly = False
        Me.txtTankerDispatch.MyShowMasterFormButton = False
        Me.txtTankerDispatch.Name = "txtTankerDispatch"
        Me.txtTankerDispatch.ReferenceFieldDesc = Nothing
        Me.txtTankerDispatch.ReferenceFieldName = Nothing
        Me.txtTankerDispatch.ReferenceTableName = Nothing
        Me.txtTankerDispatch.Size = New System.Drawing.Size(152, 19)
        Me.txtTankerDispatch.TabIndex = 1424
        Me.txtTankerDispatch.Value = ""
        '
        'fndPlantOrMCCCode
        '
        Me.fndPlantOrMCCCode.CalculationExpression = Nothing
        Me.fndPlantOrMCCCode.Enabled = False
        Me.fndPlantOrMCCCode.FieldCode = Nothing
        Me.fndPlantOrMCCCode.FieldDesc = Nothing
        Me.fndPlantOrMCCCode.FieldMaxLength = 0
        Me.fndPlantOrMCCCode.FieldName = Nothing
        Me.fndPlantOrMCCCode.isCalculatedField = False
        Me.fndPlantOrMCCCode.IsSourceFromTable = False
        Me.fndPlantOrMCCCode.IsSourceFromValueList = False
        Me.fndPlantOrMCCCode.IsUnique = False
        Me.fndPlantOrMCCCode.Location = New System.Drawing.Point(397, 76)
        Me.fndPlantOrMCCCode.MendatroryField = True
        Me.fndPlantOrMCCCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPlantOrMCCCode.MyLinkLable1 = Me.lblPlantOrMccCode
        Me.fndPlantOrMCCCode.MyLinkLable2 = Nothing
        Me.fndPlantOrMCCCode.MyReadOnly = False
        Me.fndPlantOrMCCCode.MyShowMasterFormButton = False
        Me.fndPlantOrMCCCode.Name = "fndPlantOrMCCCode"
        Me.fndPlantOrMCCCode.ReferenceFieldDesc = Nothing
        Me.fndPlantOrMCCCode.ReferenceFieldName = Nothing
        Me.fndPlantOrMCCCode.ReferenceTableName = Nothing
        Me.fndPlantOrMCCCode.Size = New System.Drawing.Size(146, 19)
        Me.fndPlantOrMCCCode.TabIndex = 4
        Me.fndPlantOrMCCCode.Value = ""
        '
        'ddlTankerDispatchTo
        '
        Me.ddlTankerDispatchTo.AutoCompleteDisplayMember = Nothing
        Me.ddlTankerDispatchTo.AutoCompleteValueMember = Nothing
        Me.ddlTankerDispatchTo.CalculationExpression = Nothing
        Me.ddlTankerDispatchTo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlTankerDispatchTo.Enabled = False
        Me.ddlTankerDispatchTo.FieldCode = Nothing
        Me.ddlTankerDispatchTo.FieldDesc = Nothing
        Me.ddlTankerDispatchTo.FieldMaxLength = 0
        Me.ddlTankerDispatchTo.FieldName = Nothing
        Me.ddlTankerDispatchTo.isCalculatedField = False
        Me.ddlTankerDispatchTo.IsSourceFromTable = False
        Me.ddlTankerDispatchTo.IsSourceFromValueList = False
        Me.ddlTankerDispatchTo.IsUnique = False
        RadListDataItem1.Text = "MCC"
        RadListDataItem2.Text = "PLANT"
        Me.ddlTankerDispatchTo.Items.Add(RadListDataItem1)
        Me.ddlTankerDispatchTo.Items.Add(RadListDataItem2)
        Me.ddlTankerDispatchTo.Location = New System.Drawing.Point(137, 75)
        Me.ddlTankerDispatchTo.MendatroryField = True
        Me.ddlTankerDispatchTo.MyLinkLable1 = Me.lblTankerDispatchTo
        Me.ddlTankerDispatchTo.MyLinkLable2 = Nothing
        Me.ddlTankerDispatchTo.Name = "ddlTankerDispatchTo"
        Me.ddlTankerDispatchTo.ReadOnly = True
        Me.ddlTankerDispatchTo.ReferenceFieldDesc = Nothing
        Me.ddlTankerDispatchTo.ReferenceFieldName = Nothing
        Me.ddlTankerDispatchTo.ReferenceTableName = Nothing
        Me.ddlTankerDispatchTo.Size = New System.Drawing.Size(152, 20)
        Me.ddlTankerDispatchTo.TabIndex = 3
        '
        'lblTankerDispatchTo
        '
        Me.lblTankerDispatchTo.FieldName = Nothing
        Me.lblTankerDispatchTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTankerDispatchTo.Location = New System.Drawing.Point(5, 77)
        Me.lblTankerDispatchTo.Name = "lblTankerDispatchTo"
        Me.lblTankerDispatchTo.Size = New System.Drawing.Size(105, 16)
        Me.lblTankerDispatchTo.TabIndex = 133
        Me.lblTankerDispatchTo.Text = "Tanker Dispatch To"
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(362, 2)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(68, 22)
        Me.btnReverse.TabIndex = 4
        Me.btnReverse.Text = "Reverse"
        Me.btnReverse.Visible = False
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(210, 2)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 22)
        Me.btnPost.TabIndex = 3
        Me.btnPost.Text = "Post"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(72, 2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(141, 2)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 22)
        Me.btnPrint.TabIndex = 2
        Me.btnPrint.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1193, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'SplitContainer6
        '
        Me.SplitContainer6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer6.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer6.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer6.Name = "SplitContainer6"
        Me.SplitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.SplitContainer6.Size = New System.Drawing.Size(1240, 203)
        Me.SplitContainer6.SplitterDistance = 26
        Me.SplitContainer6.TabIndex = 0
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(7, 7)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel21.TabIndex = 355
        '
        'txtQCNo
        '
        Me.txtQCNo.AcceptsTab = True
        Me.txtQCNo.CalculationExpression = Nothing
        Me.txtQCNo.FieldCode = Nothing
        Me.txtQCNo.FieldDesc = Nothing
        Me.txtQCNo.FieldMaxLength = 0
        Me.txtQCNo.FieldName = Nothing
        Me.txtQCNo.HideSelection = False
        Me.txtQCNo.isCalculatedField = False
        Me.txtQCNo.IsSourceFromTable = False
        Me.txtQCNo.IsSourceFromValueList = False
        Me.txtQCNo.IsUnique = False
        Me.txtQCNo.Location = New System.Drawing.Point(57, 5)
        Me.txtQCNo.MaxLength = 65535
        Me.txtQCNo.MendatroryField = False
        Me.txtQCNo.MyLinkLable1 = Nothing
        Me.txtQCNo.MyLinkLable2 = Nothing
        Me.txtQCNo.Name = "txtQCNo"
        Me.txtQCNo.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtQCNo.ReferenceFieldDesc = Nothing
        Me.txtQCNo.ReferenceFieldName = Nothing
        Me.txtQCNo.ReferenceTableName = Nothing
        Me.txtQCNo.Size = New System.Drawing.Size(229, 20)
        Me.txtQCNo.TabIndex = 356
        Me.txtQCNo.WordWrap = False
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(289, 7)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel20.TabIndex = 358
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(518, 7)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(100, 16)
        Me.MyLabel19.TabIndex = 360
        '
        'RadPageViewPage8
        '
        Me.RadPageViewPage8.Controls.Add(Me.SplitContainer6)
        Me.RadPageViewPage8.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage8.Name = "RadPageViewPage8"
        Me.RadPageViewPage8.Size = New System.Drawing.Size(1240, 203)
        Me.RadPageViewPage8.Text = "QC Details"
        '
        'frmAcknowledgementEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1261, 462)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmAcknowledgementEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Acknowledgement Entry"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadPageView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView2.ResumeLayout(False)
        Me.RadPageViewPage6.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage7.ResumeLayout(False)
        CType(Me.gvAE.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtTankerTransporterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromPlantName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lChalanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPlantOrMccCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDateAndTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDateAndTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdispatchDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPlantOrMccName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMCCName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlTankerDispatchTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerDispatchTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xxx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer6.ResumeLayout(False)
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQCNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage8.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblMCCName As common.Controls.MyLabel
    Friend WithEvents txtMCCName As common.Controls.MyTextBox
    Friend WithEvents lblDateAndTime As common.Controls.MyLabel
    Friend WithEvents dtpDateAndTime As common.Controls.MyDateTimePicker
    Friend WithEvents lChalanNo As common.Controls.MyLabel
    Friend WithEvents rbtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndDocNo As common.UserControls.txtNavigator
    Friend WithEvents ddlTankerDispatchTo As common.Controls.MyComboBox
    Friend WithEvents txtPlantOrMccName As common.Controls.MyTextBox
    Friend WithEvents lblPlantOrMccCode As common.Controls.MyLabel
    Friend WithEvents fndPlantOrMCCCode As common.UserControls.txtFinder
    Friend WithEvents lblTankerNo As common.Controls.MyLabel
    Friend WithEvents fndTnakerNo As common.UserControls.txtFinder
    Friend WithEvents lblTankerDispatchTo As common.Controls.MyLabel
    Friend WithEvents xxx As Telerik.WinControls.UI.MasterGridViewTemplate
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents mnuSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuSaveLayOut As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents txtTankerTransporterName As common.Controls.MyTextBox
    Friend WithEvents fndMCCCode As common.UserControls.txtFinder
    Friend WithEvents lblFromPlantName As common.Controls.MyTextBox
    Friend WithEvents RadPageView2 As RadPageView
    Friend WithEvents RadPageViewPage6 As RadPageViewPage
    Friend WithEvents RadPageViewPage7 As RadPageViewPage

    Friend WithEvents SplitContainer6 As SplitContainer

    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents txtQCNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage8 As RadPageViewPage
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtTankerDispatch As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtdispatchDate As common.Controls.MyDateTimePicker
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents gvAE As common.UserControls.MyRadGridView
End Class

