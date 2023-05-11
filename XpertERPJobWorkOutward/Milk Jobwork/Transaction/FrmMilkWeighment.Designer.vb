<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMilkWeighment
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
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.dtpTareWeight = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.grpGateEntryType = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkBulkMilkProc = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkMccProc = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkBothDoc = New Telerik.WinControls.UI.RadCheckBox()
        Me.fndTankerNo = New common.UserControls.txtFinder()
        Me.chkPendingTare = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblVendorCodeBulk = New common.Controls.MyTextBox()
        Me.lblLocationCodeBulk = New common.Controls.MyTextBox()
        Me.lblChallanDateBulk = New common.Controls.MyTextBox()
        Me.lblChallanNoBulk = New common.Controls.MyTextBox()
        Me.lblTankerNoBulk = New common.Controls.MyTextBox()
        Me.lblGateEntryDateAndTimeValueBulk = New common.Controls.MyTextBox()
        Me.txtWeighmentSlipNo = New common.Controls.MyTextBox()
        Me.lblWeighmentSlipNo = New common.Controls.MyLabel()
        Me.txtDipValue = New common.Controls.MyTextBox()
        Me.lblDipValue = New common.Controls.MyLabel()
        Me.lblPending = New common.usLock()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.lblStatus = New common.Controls.MyLabel()
        Me.fndDocNO = New common.UserControls.txtNavigator()
        Me.lblDocNo = New common.Controls.MyLabel()
        Me.dtpWeighmentDateBulk = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblTankerNo = New common.Controls.MyLabel()
        Me.fndGateEntryNoBulk = New common.UserControls.txtFinder()
        Me.lblLocationNameBulk = New common.Controls.MyLabel()
        Me.lblLocationBulk = New common.Controls.MyLabel()
        Me.lblStatusBulk = New common.Controls.MyLabel()
        Me.lblDateAndTimeBulk = New common.Controls.MyLabel()
        Me.lblChallanDate = New common.Controls.MyLabel()
        Me.lblChallanNo = New common.Controls.MyLabel()
        Me.lblVendorNameBulk = New common.Controls.MyLabel()
        Me.lblVendorBulk = New common.Controls.MyLabel()
        Me.lblGateEntryNO = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvItemBulk = New common.UserControls.MyRadGridView()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.gvItemMcc = New Telerik.WinControls.UI.MasterGridViewTemplate()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.dtpTareWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpGateEntryType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpGateEntryType.SuspendLayout()
        CType(Me.chkBulkMilkProc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMccProc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBothDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPendingTare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorCodeBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationCodeBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChallanDateBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChallanNoBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNoBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGateEntryDateAndTimeValueBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWeighmentSlipNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWeighmentSlipNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDipValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDipValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpWeighmentDateBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationNameBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStatusBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDateAndTimeBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChallanDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChallanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorNameBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGateEntryNO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gvItemBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItemBulk.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItemMcc, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(863, 529)
        Me.SplitContainer1.SplitterDistance = 25
        Me.SplitContainer1.TabIndex = 1
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuSetting})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(863, 20)
        Me.RadMenu1.TabIndex = 1
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer2.Size = New System.Drawing.Size(863, 500)
        Me.SplitContainer2.SplitterDistance = 464
        Me.SplitContainer2.TabIndex = 1
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpTareWeight)
        Me.SplitContainer3.Panel1.Controls.Add(Me.grpGateEntryType)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkBothDoc)
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndTankerNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkPendingTare)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblVendorCodeBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblLocationCodeBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblChallanDateBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblChallanNoBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblTankerNoBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblGateEntryDateAndTimeValueBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtWeighmentSlipNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblWeighmentSlipNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtDipValue)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblDipValue)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblPending)
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblStatus)
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndDocNO)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpWeighmentDateBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblDocNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblTankerNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndGateEntryNoBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblLocationNameBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblLocationBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblStatusBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblDateAndTimeBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblChallanDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblChallanNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblVendorNameBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblVendorBulk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblGateEntryNO)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer3.Size = New System.Drawing.Size(863, 464)
        Me.SplitContainer3.SplitterDistance = 211
        Me.SplitContainer3.TabIndex = 0
        '
        'dtpTareWeight
        '
        Me.dtpTareWeight.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpTareWeight.Enabled = False
        Me.dtpTareWeight.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTareWeight.Location = New System.Drawing.Point(633, 61)
        Me.dtpTareWeight.MendatroryField = False
        Me.dtpTareWeight.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTareWeight.MyLinkLable1 = Me.MyLabel1
        Me.dtpTareWeight.MyLinkLable2 = Nothing
        Me.dtpTareWeight.Name = "dtpTareWeight"
        Me.dtpTareWeight.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTareWeight.Size = New System.Drawing.Size(132, 20)
        Me.dtpTareWeight.TabIndex = 346
        Me.dtpTareWeight.TabStop = False
        Me.dtpTareWeight.Text = "10/06/2011 11:51 AM"
        Me.dtpTareWeight.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(523, 62)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(95, 16)
        Me.MyLabel1.TabIndex = 347
        Me.MyLabel1.Text = "Tare Weight Date"
        '
        'grpGateEntryType
        '
        Me.grpGateEntryType.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpGateEntryType.Controls.Add(Me.chkBulkMilkProc)
        Me.grpGateEntryType.Controls.Add(Me.chkMccProc)
        Me.grpGateEntryType.HeaderText = "Type"
        Me.grpGateEntryType.Location = New System.Drawing.Point(815, 83)
        Me.grpGateEntryType.Name = "grpGateEntryType"
        Me.grpGateEntryType.Size = New System.Drawing.Size(23, 34)
        Me.grpGateEntryType.TabIndex = 10
        Me.grpGateEntryType.Text = "Type"
        Me.grpGateEntryType.Visible = False
        '
        'chkBulkMilkProc
        '
        Me.chkBulkMilkProc.Location = New System.Drawing.Point(5, 13)
        Me.chkBulkMilkProc.Name = "chkBulkMilkProc"
        Me.chkBulkMilkProc.Size = New System.Drawing.Size(94, 18)
        Me.chkBulkMilkProc.TabIndex = 0
        Me.chkBulkMilkProc.Text = "Tanker Receipt"
        '
        'chkMccProc
        '
        Me.chkMccProc.Location = New System.Drawing.Point(230, 13)
        Me.chkMccProc.Name = "chkMccProc"
        Me.chkMccProc.Size = New System.Drawing.Size(78, 18)
        Me.chkMccProc.TabIndex = 1
        Me.chkMccProc.Text = "Sku Receipt"
        Me.chkMccProc.Visible = False
        '
        'chkBothDoc
        '
        Me.chkBothDoc.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBothDoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBothDoc.Location = New System.Drawing.Point(478, 193)
        Me.chkBothDoc.Name = "chkBothDoc"
        Me.chkBothDoc.Size = New System.Drawing.Size(225, 16)
        Me.chkBothDoc.TabIndex = 345
        Me.chkBothDoc.Text = "Show Tanker and Sku In Both Document"
        Me.chkBothDoc.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        Me.chkBothDoc.Visible = False
        '
        'fndTankerNo
        '
        Me.fndTankerNo.Location = New System.Drawing.Point(134, 40)
        Me.fndTankerNo.MendatroryField = True
        Me.fndTankerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTankerNo.MyLinkLable1 = Nothing
        Me.fndTankerNo.MyLinkLable2 = Nothing
        Me.fndTankerNo.MyReadOnly = False
        Me.fndTankerNo.MyShowMasterFormButton = False
        Me.fndTankerNo.Name = "fndTankerNo"
        Me.fndTankerNo.Size = New System.Drawing.Size(338, 19)
        Me.fndTankerNo.TabIndex = 344
        Me.fndTankerNo.Value = ""
        '
        'chkPendingTare
        '
        Me.chkPendingTare.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPendingTare.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPendingTare.Location = New System.Drawing.Point(478, 22)
        Me.chkPendingTare.Name = "chkPendingTare"
        Me.chkPendingTare.Size = New System.Drawing.Size(127, 16)
        Me.chkPendingTare.TabIndex = 343
        Me.chkPendingTare.Text = "Pending Tare Weight"
        Me.chkPendingTare.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'lblVendorCodeBulk
        '
        Me.lblVendorCodeBulk.Enabled = False
        Me.lblVendorCodeBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorCodeBulk.Location = New System.Drawing.Point(135, 166)
        Me.lblVendorCodeBulk.MaxLength = 50
        Me.lblVendorCodeBulk.MendatroryField = True
        Me.lblVendorCodeBulk.MyLinkLable1 = Nothing
        Me.lblVendorCodeBulk.MyLinkLable2 = Nothing
        Me.lblVendorCodeBulk.Name = "lblVendorCodeBulk"
        Me.lblVendorCodeBulk.Size = New System.Drawing.Size(333, 18)
        Me.lblVendorCodeBulk.TabIndex = 298
        '
        'lblLocationCodeBulk
        '
        Me.lblLocationCodeBulk.Enabled = False
        Me.lblLocationCodeBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationCodeBulk.Location = New System.Drawing.Point(135, 145)
        Me.lblLocationCodeBulk.MaxLength = 50
        Me.lblLocationCodeBulk.MendatroryField = True
        Me.lblLocationCodeBulk.MyLinkLable1 = Nothing
        Me.lblLocationCodeBulk.MyLinkLable2 = Nothing
        Me.lblLocationCodeBulk.Name = "lblLocationCodeBulk"
        Me.lblLocationCodeBulk.Size = New System.Drawing.Size(333, 18)
        Me.lblLocationCodeBulk.TabIndex = 297
        '
        'lblChallanDateBulk
        '
        Me.lblChallanDateBulk.Enabled = False
        Me.lblChallanDateBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChallanDateBulk.Location = New System.Drawing.Point(135, 124)
        Me.lblChallanDateBulk.MaxLength = 50
        Me.lblChallanDateBulk.MendatroryField = True
        Me.lblChallanDateBulk.MyLinkLable1 = Nothing
        Me.lblChallanDateBulk.MyLinkLable2 = Nothing
        Me.lblChallanDateBulk.Name = "lblChallanDateBulk"
        Me.lblChallanDateBulk.Size = New System.Drawing.Size(334, 18)
        Me.lblChallanDateBulk.TabIndex = 296
        '
        'lblChallanNoBulk
        '
        Me.lblChallanNoBulk.Enabled = False
        Me.lblChallanNoBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChallanNoBulk.Location = New System.Drawing.Point(135, 103)
        Me.lblChallanNoBulk.MaxLength = 50
        Me.lblChallanNoBulk.MendatroryField = True
        Me.lblChallanNoBulk.MyLinkLable1 = Nothing
        Me.lblChallanNoBulk.MyLinkLable2 = Nothing
        Me.lblChallanNoBulk.Name = "lblChallanNoBulk"
        Me.lblChallanNoBulk.Size = New System.Drawing.Size(334, 18)
        Me.lblChallanNoBulk.TabIndex = 295
        '
        'lblTankerNoBulk
        '
        Me.lblTankerNoBulk.Enabled = False
        Me.lblTankerNoBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTankerNoBulk.Location = New System.Drawing.Point(134, 40)
        Me.lblTankerNoBulk.MaxLength = 50
        Me.lblTankerNoBulk.MendatroryField = True
        Me.lblTankerNoBulk.MyLinkLable1 = Nothing
        Me.lblTankerNoBulk.MyLinkLable2 = Nothing
        Me.lblTankerNoBulk.Name = "lblTankerNoBulk"
        Me.lblTankerNoBulk.Size = New System.Drawing.Size(290, 18)
        Me.lblTankerNoBulk.TabIndex = 294
        Me.lblTankerNoBulk.Visible = False
        '
        'lblGateEntryDateAndTimeValueBulk
        '
        Me.lblGateEntryDateAndTimeValueBulk.Enabled = False
        Me.lblGateEntryDateAndTimeValueBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGateEntryDateAndTimeValueBulk.Location = New System.Drawing.Point(135, 83)
        Me.lblGateEntryDateAndTimeValueBulk.MaxLength = 50
        Me.lblGateEntryDateAndTimeValueBulk.MendatroryField = True
        Me.lblGateEntryDateAndTimeValueBulk.MyLinkLable1 = Nothing
        Me.lblGateEntryDateAndTimeValueBulk.MyLinkLable2 = Nothing
        Me.lblGateEntryDateAndTimeValueBulk.Name = "lblGateEntryDateAndTimeValueBulk"
        Me.lblGateEntryDateAndTimeValueBulk.Size = New System.Drawing.Size(334, 18)
        Me.lblGateEntryDateAndTimeValueBulk.TabIndex = 293
        '
        'txtWeighmentSlipNo
        '
        Me.txtWeighmentSlipNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeighmentSlipNo.Location = New System.Drawing.Point(633, 124)
        Me.txtWeighmentSlipNo.MaxLength = 50
        Me.txtWeighmentSlipNo.MendatroryField = False
        Me.txtWeighmentSlipNo.MyLinkLable1 = Nothing
        Me.txtWeighmentSlipNo.MyLinkLable2 = Nothing
        Me.txtWeighmentSlipNo.Name = "txtWeighmentSlipNo"
        Me.txtWeighmentSlipNo.Size = New System.Drawing.Size(148, 18)
        Me.txtWeighmentSlipNo.TabIndex = 291
        '
        'lblWeighmentSlipNo
        '
        Me.lblWeighmentSlipNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeighmentSlipNo.Location = New System.Drawing.Point(523, 126)
        Me.lblWeighmentSlipNo.Name = "lblWeighmentSlipNo"
        Me.lblWeighmentSlipNo.Size = New System.Drawing.Size(103, 16)
        Me.lblWeighmentSlipNo.TabIndex = 292
        Me.lblWeighmentSlipNo.Text = "Weighment Slip No"
        '
        'txtDipValue
        '
        Me.txtDipValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDipValue.Location = New System.Drawing.Point(633, 104)
        Me.txtDipValue.MaxLength = 50
        Me.txtDipValue.MendatroryField = False
        Me.txtDipValue.MyLinkLable1 = Nothing
        Me.txtDipValue.MyLinkLable2 = Nothing
        Me.txtDipValue.Name = "txtDipValue"
        Me.txtDipValue.Size = New System.Drawing.Size(148, 18)
        Me.txtDipValue.TabIndex = 3
        '
        'lblDipValue
        '
        Me.lblDipValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDipValue.Location = New System.Drawing.Point(522, 105)
        Me.lblDipValue.Name = "lblDipValue"
        Me.lblDipValue.Size = New System.Drawing.Size(57, 16)
        Me.lblDipValue.TabIndex = 290
        Me.lblDipValue.Text = "DIP Value"
        '
        'lblPending
        '
        Me.lblPending.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(741, 14)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(97, 20)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 248
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = My.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(454, 19)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(19, 18)
        Me.btnReset.TabIndex = 250
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(523, 83)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(58, 16)
        Me.lblStatus.TabIndex = 285
        Me.lblStatus.Text = "QC Status"
        '
        'fndDocNO
        '
        Me.fndDocNO.Location = New System.Drawing.Point(136, 19)
        Me.fndDocNO.MendatroryField = False
        Me.fndDocNO.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndDocNO.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocNO.MyLinkLable1 = Me.lblDocNo
        Me.fndDocNO.MyLinkLable2 = Nothing
        Me.fndDocNO.MyMaxLength = 32767
        Me.fndDocNO.MyReadOnly = False
        Me.fndDocNO.Name = "fndDocNO"
        Me.fndDocNO.Size = New System.Drawing.Size(317, 18)
        Me.fndDocNO.TabIndex = 0
        Me.fndDocNO.Value = ""
        '
        'lblDocNo
        '
        Me.lblDocNo.Location = New System.Drawing.Point(12, 20)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(79, 18)
        Me.lblDocNo.TabIndex = 251
        Me.lblDocNo.Text = "Document No."
        '
        'dtpWeighmentDateBulk
        '
        Me.dtpWeighmentDateBulk.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpWeighmentDateBulk.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpWeighmentDateBulk.Location = New System.Drawing.Point(633, 40)
        Me.dtpWeighmentDateBulk.MendatroryField = False
        Me.dtpWeighmentDateBulk.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpWeighmentDateBulk.MyLinkLable1 = Me.MyLabel2
        Me.dtpWeighmentDateBulk.MyLinkLable2 = Nothing
        Me.dtpWeighmentDateBulk.Name = "dtpWeighmentDateBulk"
        Me.dtpWeighmentDateBulk.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpWeighmentDateBulk.Size = New System.Drawing.Size(132, 20)
        Me.dtpWeighmentDateBulk.TabIndex = 2
        Me.dtpWeighmentDateBulk.TabStop = False
        Me.dtpWeighmentDateBulk.Text = "10/06/2011 11:51 AM"
        Me.dtpWeighmentDateBulk.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(523, 42)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(102, 16)
        Me.MyLabel2.TabIndex = 287
        Me.MyLabel2.Text = "Gross Weight Date"
        '
        'lblTankerNo
        '
        Me.lblTankerNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTankerNo.Location = New System.Drawing.Point(10, 40)
        Me.lblTankerNo.Name = "lblTankerNo"
        Me.lblTankerNo.Size = New System.Drawing.Size(59, 16)
        Me.lblTankerNo.TabIndex = 279
        Me.lblTankerNo.Text = "Tanker No"
        '
        'fndGateEntryNoBulk
        '
        Me.fndGateEntryNoBulk.Enabled = False
        Me.fndGateEntryNoBulk.Location = New System.Drawing.Point(135, 62)
        Me.fndGateEntryNoBulk.MendatroryField = True
        Me.fndGateEntryNoBulk.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndGateEntryNoBulk.MyLinkLable1 = Nothing
        Me.fndGateEntryNoBulk.MyLinkLable2 = Nothing
        Me.fndGateEntryNoBulk.MyReadOnly = False
        Me.fndGateEntryNoBulk.MyShowMasterFormButton = False
        Me.fndGateEntryNoBulk.Name = "fndGateEntryNoBulk"
        Me.fndGateEntryNoBulk.Size = New System.Drawing.Size(335, 19)
        Me.fndGateEntryNoBulk.TabIndex = 1
        Me.fndGateEntryNoBulk.Value = ""
        '
        'lblLocationNameBulk
        '
        Me.lblLocationNameBulk.AutoSize = False
        Me.lblLocationNameBulk.BorderVisible = True
        Me.lblLocationNameBulk.Location = New System.Drawing.Point(473, 145)
        Me.lblLocationNameBulk.Name = "lblLocationNameBulk"
        Me.lblLocationNameBulk.Size = New System.Drawing.Size(308, 18)
        Me.lblLocationNameBulk.TabIndex = 275
        Me.lblLocationNameBulk.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLocationBulk
        '
        Me.lblLocationBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationBulk.Location = New System.Drawing.Point(10, 145)
        Me.lblLocationBulk.Name = "lblLocationBulk"
        Me.lblLocationBulk.Size = New System.Drawing.Size(49, 16)
        Me.lblLocationBulk.TabIndex = 273
        Me.lblLocationBulk.Text = "Location"
        '
        'lblStatusBulk
        '
        Me.lblStatusBulk.AutoSize = False
        Me.lblStatusBulk.BorderVisible = True
        Me.lblStatusBulk.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblStatusBulk.Location = New System.Drawing.Point(633, 83)
        Me.lblStatusBulk.Name = "lblStatusBulk"
        Me.lblStatusBulk.Size = New System.Drawing.Size(148, 18)
        Me.lblStatusBulk.TabIndex = 286
        Me.lblStatusBulk.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDateAndTimeBulk
        '
        Me.lblDateAndTimeBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateAndTimeBulk.Location = New System.Drawing.Point(9, 83)
        Me.lblDateAndTimeBulk.Name = "lblDateAndTimeBulk"
        Me.lblDateAndTimeBulk.Size = New System.Drawing.Size(126, 16)
        Me.lblDateAndTimeBulk.TabIndex = 252
        Me.lblDateAndTimeBulk.Text = "Gate Entry Date && Time"
        '
        'lblChallanDate
        '
        Me.lblChallanDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChallanDate.Location = New System.Drawing.Point(10, 124)
        Me.lblChallanDate.Name = "lblChallanDate"
        Me.lblChallanDate.Size = New System.Drawing.Size(72, 16)
        Me.lblChallanDate.TabIndex = 283
        Me.lblChallanDate.Text = "Challan Date"
        '
        'lblChallanNo
        '
        Me.lblChallanNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChallanNo.Location = New System.Drawing.Point(10, 103)
        Me.lblChallanNo.Name = "lblChallanNo"
        Me.lblChallanNo.Size = New System.Drawing.Size(62, 16)
        Me.lblChallanNo.TabIndex = 281
        Me.lblChallanNo.Text = "Challan No"
        '
        'lblVendorNameBulk
        '
        Me.lblVendorNameBulk.AutoSize = False
        Me.lblVendorNameBulk.BorderVisible = True
        Me.lblVendorNameBulk.Location = New System.Drawing.Point(473, 166)
        Me.lblVendorNameBulk.Name = "lblVendorNameBulk"
        Me.lblVendorNameBulk.Size = New System.Drawing.Size(308, 18)
        Me.lblVendorNameBulk.TabIndex = 278
        Me.lblVendorNameBulk.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblVendorBulk
        '
        Me.lblVendorBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorBulk.Location = New System.Drawing.Point(10, 166)
        Me.lblVendorBulk.Name = "lblVendorBulk"
        Me.lblVendorBulk.Size = New System.Drawing.Size(43, 16)
        Me.lblVendorBulk.TabIndex = 276
        Me.lblVendorBulk.Text = "Vendor"
        '
        'lblGateEntryNO
        '
        Me.lblGateEntryNO.Location = New System.Drawing.Point(11, 61)
        Me.lblGateEntryNO.Name = "lblGateEntryNO"
        Me.lblGateEntryNO.Size = New System.Drawing.Size(82, 18)
        Me.lblGateEntryNO.TabIndex = 32
        Me.lblGateEntryNO.Text = "Gate Entry No. "
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gvItemBulk)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = "Item Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(863, 249)
        Me.RadGroupBox1.TabIndex = 285
        Me.RadGroupBox1.Text = "Item Details"
        '
        'gvItemBulk
        '
        Me.gvItemBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvItemBulk.Location = New System.Drawing.Point(2, 18)
        Me.gvItemBulk.Name = "gvItemBulk"
        Me.gvItemBulk.Size = New System.Drawing.Size(859, 229)
        Me.gvItemBulk.TabIndex = 264
        Me.gvItemBulk.Text = "RadGridView1"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(215, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(286, 7)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(69, 18)
        Me.btnReverse.TabIndex = 4
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
        Me.btnClose.Location = New System.Drawing.Point(795, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 5
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
        'FrmMilkWeighment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(863, 529)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmMilkWeighment"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmMilkWeighment"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.dtpTareWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpGateEntryType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpGateEntryType.ResumeLayout(False)
        Me.grpGateEntryType.PerformLayout()
        CType(Me.chkBulkMilkProc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMccProc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBothDoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPendingTare, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorCodeBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationCodeBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChallanDateBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChallanNoBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNoBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGateEntryDateAndTimeValueBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWeighmentSlipNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWeighmentSlipNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDipValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDipValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpWeighmentDateBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationNameBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStatusBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDateAndTimeBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChallanDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChallanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorNameBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGateEntryNO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gvItemBulk.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItemBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItemMcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents grpGateEntryType As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkBulkMilkProc As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkMccProc As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents lblGateEntryNO As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndGateEntryNoBulk As common.UserControls.txtFinder
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDateAndTimeBulk As common.Controls.MyLabel
    Friend WithEvents lblVendorNameBulk As common.Controls.MyLabel
    Friend WithEvents lblVendorBulk As common.Controls.MyLabel
    Friend WithEvents lblLocationNameBulk As common.Controls.MyLabel
    Friend WithEvents lblLocationBulk As common.Controls.MyLabel
    Friend WithEvents lblTankerNo As common.Controls.MyLabel
    Friend WithEvents lblChallanDate As common.Controls.MyLabel
    Friend WithEvents lblChallanNo As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvItemBulk As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblStatusBulk As common.Controls.MyLabel
    Friend WithEvents lblStatus As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents dtpWeighmentDateBulk As common.Controls.MyDateTimePicker
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndDocNO As common.UserControls.txtNavigator
    Friend WithEvents lblDocNo As common.Controls.MyLabel
    Friend WithEvents gvItemMcc As Telerik.WinControls.UI.MasterGridViewTemplate
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents mnuSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuExit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtDipValue As common.Controls.MyTextBox
    Friend WithEvents lblDipValue As common.Controls.MyLabel
    Friend WithEvents txtWeighmentSlipNo As common.Controls.MyTextBox
    Friend WithEvents lblWeighmentSlipNo As common.Controls.MyLabel
    Friend WithEvents lblGateEntryDateAndTimeValueBulk As common.Controls.MyTextBox
    Friend WithEvents lblChallanNoBulk As common.Controls.MyTextBox
    Friend WithEvents lblTankerNoBulk As common.Controls.MyTextBox
    Friend WithEvents lblChallanDateBulk As common.Controls.MyTextBox
    Friend WithEvents lblVendorCodeBulk As common.Controls.MyTextBox
    Friend WithEvents lblLocationCodeBulk As common.Controls.MyTextBox
    Friend WithEvents chkPendingTare As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents fndTankerNo As common.UserControls.txtFinder
    Friend WithEvents chkBothDoc As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents dtpTareWeight As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
End Class

