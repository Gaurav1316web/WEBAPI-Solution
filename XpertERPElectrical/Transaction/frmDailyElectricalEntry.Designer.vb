Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDailyElectricalEntry
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
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtConsumptionDate = New common.Controls.MyDateTimePicker()
        Me.lblSRNDate = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblTotalEBUnit = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.lblChallanNo = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblSRNNo = New common.Controls.MyLabel()
        Me.lblPending = New common.usLock()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.gvSlot = New common.UserControls.MyRadGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.gvDG = New common.UserControls.MyRadGridView()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.mnuSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuExit = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtConsumptionDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSRNDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalEBUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChallanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSRNNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.gvSlot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSlot.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.gvDG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDG.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(214, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(917, 511)
        Me.SplitContainer1.SplitterDistance = 477
        Me.SplitContainer1.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Size = New System.Drawing.Size(917, 457)
        Me.SplitContainer2.SplitterDistance = 80
        Me.SplitContainer2.TabIndex = 3
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtConsumptionDate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.lblTotalEBUnit)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtRemarks)
        Me.RadGroupBox1.Controls.Add(Me.lblChallanNo)
        Me.RadGroupBox1.Controls.Add(Me.btnReset)
        Me.RadGroupBox1.Controls.Add(Me.lblSRNDate)
        Me.RadGroupBox1.Controls.Add(Me.txtDate)
        Me.RadGroupBox1.Controls.Add(Me.lblSRNNo)
        Me.RadGroupBox1.Controls.Add(Me.lblPending)
        Me.RadGroupBox1.Controls.Add(Me.txtDocNo)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(917, 80)
        Me.RadGroupBox1.TabIndex = 2
        '
        'txtConsumptionDate
        '
        Me.txtConsumptionDate.CalculationExpression = Nothing
        Me.txtConsumptionDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtConsumptionDate.FieldCode = Nothing
        Me.txtConsumptionDate.FieldDesc = Nothing
        Me.txtConsumptionDate.FieldMaxLength = 0
        Me.txtConsumptionDate.FieldName = Nothing
        Me.txtConsumptionDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConsumptionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtConsumptionDate.isCalculatedField = False
        Me.txtConsumptionDate.IsSourceFromTable = False
        Me.txtConsumptionDate.IsSourceFromValueList = False
        Me.txtConsumptionDate.IsUnique = False
        Me.txtConsumptionDate.Location = New System.Drawing.Point(703, 10)
        Me.txtConsumptionDate.MendatroryField = True
        Me.txtConsumptionDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtConsumptionDate.MyLinkLable1 = Me.lblSRNDate
        Me.txtConsumptionDate.MyLinkLable2 = Nothing
        Me.txtConsumptionDate.Name = "txtConsumptionDate"
        Me.txtConsumptionDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtConsumptionDate.ReferenceFieldDesc = Nothing
        Me.txtConsumptionDate.ReferenceFieldName = Nothing
        Me.txtConsumptionDate.ReferenceTableName = Nothing
        Me.txtConsumptionDate.Size = New System.Drawing.Size(80, 18)
        Me.txtConsumptionDate.TabIndex = 344
        Me.txtConsumptionDate.TabStop = False
        Me.txtConsumptionDate.Text = "17/10/2018 04:07 PM"
        Me.txtConsumptionDate.Value = New Date(2018, 10, 17, 16, 7, 27, 0)
        '
        'lblSRNDate
        '
        Me.lblSRNDate.FieldName = Nothing
        Me.lblSRNDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSRNDate.Location = New System.Drawing.Point(423, 11)
        Me.lblSRNDate.Name = "lblSRNDate"
        Me.lblSRNDate.Size = New System.Drawing.Size(85, 16)
        Me.lblSRNDate.TabIndex = 19
        Me.lblSRNDate.Text = "Document Date"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(597, 11)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(100, 16)
        Me.MyLabel2.TabIndex = 343
        Me.MyLabel2.Text = "Consumption Date"
        '
        'lblTotalEBUnit
        '
        Me.lblTotalEBUnit.AutoSize = False
        Me.lblTotalEBUnit.BorderVisible = True
        Me.lblTotalEBUnit.FieldName = Nothing
        Me.lblTotalEBUnit.Location = New System.Drawing.Point(99, 55)
        Me.lblTotalEBUnit.Name = "lblTotalEBUnit"
        Me.lblTotalEBUnit.Size = New System.Drawing.Size(128, 21)
        Me.lblTotalEBUnit.TabIndex = 342
        Me.lblTotalEBUnit.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTotalEBUnit.TextWrap = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(13, 57)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel1.TabIndex = 326
        Me.MyLabel1.Text = "Total EB Unit "
        '
        'txtRemarks
        '
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(100, 33)
        Me.txtRemarks.MaxLength = 500
        Me.txtRemarks.MendatroryField = True
        Me.txtRemarks.MyLinkLable1 = Me.lblChallanNo
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(683, 20)
        Me.txtRemarks.TabIndex = 325
        '
        'lblChallanNo
        '
        Me.lblChallanNo.FieldName = Nothing
        Me.lblChallanNo.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblChallanNo.Location = New System.Drawing.Point(13, 35)
        Me.lblChallanNo.Name = "lblChallanNo"
        Me.lblChallanNo.Size = New System.Drawing.Size(51, 16)
        Me.lblChallanNo.TabIndex = 324
        Me.lblChallanNo.Text = "Remarks"
        '
        'btnReset
        '
        Me.btnReset.Image = My.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(394, 9)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(20, 21)
        Me.btnReset.TabIndex = 44
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
        Me.txtDate.Location = New System.Drawing.Point(511, 10)
        Me.txtDate.MendatroryField = True
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblSRNDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReadOnly = True
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(80, 18)
        Me.txtDate.TabIndex = 1
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "03/05/2011 12:00 AM"
        Me.txtDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblSRNNo
        '
        Me.lblSRNNo.FieldName = Nothing
        Me.lblSRNNo.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblSRNNo.Location = New System.Drawing.Point(13, 11)
        Me.lblSRNNo.Name = "lblSRNNo"
        Me.lblSRNNo.Size = New System.Drawing.Size(75, 16)
        Me.lblSRNNo.TabIndex = 10
        Me.lblSRNNo.Text = "Document No"
        '
        'lblPending
        '
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(807, 11)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(98, 20)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 11
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(100, 9)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.lblSRNNo
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 30
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(294, 21)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.Value = ""
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.GroupBox2)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.GroupBox1)
        Me.SplitContainer3.Size = New System.Drawing.Size(917, 373)
        Me.SplitContainer3.SplitterDistance = 360
        Me.SplitContainer3.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.gvSlot)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(360, 373)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "EB Unit"
        '
        'gvSlot
        '
        Me.gvSlot.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvSlot.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvSlot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvSlot.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvSlot.ForeColor = System.Drawing.Color.Black
        Me.gvSlot.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvSlot.Location = New System.Drawing.Point(3, 18)
        '
        'gvSlot
        '
        Me.gvSlot.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.gvSlot.MasterTemplate.AllowDeleteRow = False
        Me.gvSlot.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvSlot.Name = "gvSlot"
        Me.gvSlot.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvSlot.ShowGroupPanel = False
        Me.gvSlot.ShowHeaderCellButtons = True
        Me.gvSlot.Size = New System.Drawing.Size(354, 352)
        Me.gvSlot.TabIndex = 1
        Me.gvSlot.TabStop = False
        Me.gvSlot.Text = "RadGridView1"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.gvDG)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(553, 373)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Diesel Genset "
        '
        'gvDG
        '
        Me.gvDG.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvDG.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvDG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDG.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvDG.ForeColor = System.Drawing.Color.Black
        Me.gvDG.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvDG.Location = New System.Drawing.Point(3, 18)
        '
        'gvDG
        '
        Me.gvDG.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.gvDG.MasterTemplate.AllowDeleteRow = False
        Me.gvDG.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDG.Name = "gvDG"
        Me.gvDG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvDG.ShowGroupPanel = False
        Me.gvDG.ShowHeaderCellButtons = True
        Me.gvDG.Size = New System.Drawing.Size(547, 352)
        Me.gvDG.TabIndex = 1
        Me.gvDG.TabStop = False
        Me.gvDG.Text = "RadGridView1"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuSetting})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(917, 20)
        Me.RadMenu1.TabIndex = 2
        Me.RadMenu1.Text = "Total"
        Me.RadMenu1.Visible = False
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
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(284, 7)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(117, 18)
        Me.btnReverse.TabIndex = 5
        Me.btnReverse.Text = "Revese and Unpost"
        Me.btnReverse.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(846, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
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
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 7)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
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
        'frmDailyElectricalEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(917, 511)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmDailyElectricalEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Daily Electrical Entry"
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtConsumptionDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSRNDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalEBUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChallanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSRNNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.gvSlot.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSlot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.gvDG.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents mnuSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuExit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblChallanNo As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblSRNDate As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblSRNNo As common.Controls.MyLabel
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents gvDG As common.UserControls.MyRadGridView
    Friend WithEvents gvSlot As common.UserControls.MyRadGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblTotalEBUnit As common.Controls.MyLabel
    Friend WithEvents txtConsumptionDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
End Class

