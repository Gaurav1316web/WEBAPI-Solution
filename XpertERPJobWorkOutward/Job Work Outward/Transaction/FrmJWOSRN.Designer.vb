Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmJWOSRN
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtJWOEstimate = New common.UserControls.txtFinder()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.lblTotalAmt = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblJobAmt = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtGateEntryDate = New common.Controls.MyDateTimePicker()
        Me.txtJobLocation = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblJobLocation = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.txtVendor = New common.UserControls.txtFinder()
        Me.lblVendor = New common.Controls.MyLabel()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.grpGateEntryType = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkSKU = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnManual = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnTanker = New Telerik.WinControls.UI.RadRadioButton()
        Me.lblUnloadingNo = New common.Controls.MyLabel()
        Me.txtVehicleNo = New common.UserControls.txtFinder()
        Me.lblTankerNo = New common.Controls.MyLabel()
        Me.txtGateEntryNo = New common.Controls.MyTextBox()
        Me.lblGateEntryNo = New common.Controls.MyLabel()
        Me.lblChallanDate = New common.Controls.MyLabel()
        Me.txtChallanDate = New common.Controls.MyDateTimePicker()
        Me.txtChallanNo = New common.Controls.MyTextBox()
        Me.lblChallanNo = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.lblSRNDate = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblSRNNo = New common.Controls.MyLabel()
        Me.lblPending = New common.usLock()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.gvItem = New common.UserControls.MyRadGridView()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.mnuSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuExit = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnShowInventory = New Telerik.WinControls.UI.RadButton()
        Me.BtnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.pvpCustomFields.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblJobAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGateEntryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblJobLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpGateEntryType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpGateEntryType.SuspendLayout()
        CType(Me.chkSKU, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnTanker, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnloadingNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGateEntryNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGateEntryNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChallanDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChallanDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChallanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChallanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSRNDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSRNNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(216, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnShowInventory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(937, 511)
        Me.SplitContainer1.SplitterDistance = 477
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.pvpCustomFields
        Me.RadPageView1.Size = New System.Drawing.Size(937, 457)
        Me.RadPageView1.TabIndex = 3
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.SplitContainer2)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(75.0!, 26.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 35)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(916, 411)
        Me.pvpCustomFields.Text = "Transaction"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gvItem)
        Me.SplitContainer2.Panel2.Controls.Add(Me.MyLabel6)
        Me.SplitContainer2.Size = New System.Drawing.Size(916, 411)
        Me.SplitContainer2.SplitterDistance = 190
        Me.SplitContainer2.TabIndex = 3
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtJWOEstimate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.lblTotalAmt)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.lblJobAmt)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtGateEntryDate)
        Me.RadGroupBox1.Controls.Add(Me.txtJobLocation)
        Me.RadGroupBox1.Controls.Add(Me.lblJobLocation)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtLocation)
        Me.RadGroupBox1.Controls.Add(Me.txtVendor)
        Me.RadGroupBox1.Controls.Add(Me.grpGateEntryType)
        Me.RadGroupBox1.Controls.Add(Me.lblUnloadingNo)
        Me.RadGroupBox1.Controls.Add(Me.txtVehicleNo)
        Me.RadGroupBox1.Controls.Add(Me.txtGateEntryNo)
        Me.RadGroupBox1.Controls.Add(Me.lblGateEntryNo)
        Me.RadGroupBox1.Controls.Add(Me.lblChallanDate)
        Me.RadGroupBox1.Controls.Add(Me.txtChallanDate)
        Me.RadGroupBox1.Controls.Add(Me.lblTankerNo)
        Me.RadGroupBox1.Controls.Add(Me.lblLocationDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblLocation)
        Me.RadGroupBox1.Controls.Add(Me.txtChallanNo)
        Me.RadGroupBox1.Controls.Add(Me.lblChallanNo)
        Me.RadGroupBox1.Controls.Add(Me.lblVendorName)
        Me.RadGroupBox1.Controls.Add(Me.lblVendor)
        Me.RadGroupBox1.Controls.Add(Me.btnReset)
        Me.RadGroupBox1.Controls.Add(Me.lblSRNDate)
        Me.RadGroupBox1.Controls.Add(Me.txtDate)
        Me.RadGroupBox1.Controls.Add(Me.lblSRNNo)
        Me.RadGroupBox1.Controls.Add(Me.lblPending)
        Me.RadGroupBox1.Controls.Add(Me.txtDocNo)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "SRN Head"
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(916, 190)
        Me.RadGroupBox1.TabIndex = 2
        Me.RadGroupBox1.Text = "SRN Head"
        '
        'txtJWOEstimate
        '
        Me.txtJWOEstimate.CalculationExpression = Nothing
        Me.txtJWOEstimate.FieldCode = Nothing
        Me.txtJWOEstimate.FieldDesc = Nothing
        Me.txtJWOEstimate.FieldMaxLength = 0
        Me.txtJWOEstimate.FieldName = Nothing
        Me.txtJWOEstimate.isCalculatedField = False
        Me.txtJWOEstimate.IsSourceFromTable = False
        Me.txtJWOEstimate.IsSourceFromValueList = False
        Me.txtJWOEstimate.IsUnique = False
        Me.txtJWOEstimate.Location = New System.Drawing.Point(100, 169)
        Me.txtJWOEstimate.MendatroryField = False
        Me.txtJWOEstimate.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJWOEstimate.MyLinkLable1 = Me.MyLabel7
        Me.txtJWOEstimate.MyLinkLable2 = Nothing
        Me.txtJWOEstimate.MyReadOnly = False
        Me.txtJWOEstimate.MyShowMasterFormButton = False
        Me.txtJWOEstimate.Name = "txtJWOEstimate"
        Me.txtJWOEstimate.ReferenceFieldDesc = Nothing
        Me.txtJWOEstimate.ReferenceFieldName = Nothing
        Me.txtJWOEstimate.ReferenceTableName = Nothing
        Me.txtJWOEstimate.Size = New System.Drawing.Size(255, 19)
        Me.txtJWOEstimate.TabIndex = 349
        Me.txtJWOEstimate.Value = ""
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(3, 170)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel7.TabIndex = 348
        Me.MyLabel7.Text = "JWO Estimate"
        '
        'MyLabel4
        '
        Me.MyLabel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(709, 38)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(100, 16)
        Me.MyLabel4.TabIndex = 341
        Me.MyLabel4.Text = "Document Amount"
        '
        'lblTotalAmt
        '
        Me.lblTotalAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalAmt.AutoSize = False
        Me.lblTotalAmt.BorderVisible = True
        Me.lblTotalAmt.FieldName = Nothing
        Me.lblTotalAmt.Location = New System.Drawing.Point(813, 36)
        Me.lblTotalAmt.Name = "lblTotalAmt"
        Me.lblTotalAmt.Size = New System.Drawing.Size(101, 21)
        Me.lblTotalAmt.TabIndex = 340
        Me.lblTotalAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTotalAmt.TextWrap = False
        '
        'MyLabel5
        '
        Me.MyLabel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(709, 60)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel5.TabIndex = 343
        Me.MyLabel5.Text = "Job Amount"
        '
        'lblJobAmt
        '
        Me.lblJobAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJobAmt.AutoSize = False
        Me.lblJobAmt.BorderVisible = True
        Me.lblJobAmt.FieldName = Nothing
        Me.lblJobAmt.Location = New System.Drawing.Point(813, 58)
        Me.lblJobAmt.Name = "lblJobAmt"
        Me.lblJobAmt.Size = New System.Drawing.Size(101, 21)
        Me.lblJobAmt.TabIndex = 342
        Me.lblJobAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblJobAmt.TextWrap = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(3, 148)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel3.TabIndex = 347
        Me.MyLabel3.Text = "Unloading No"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(593, 126)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel2.TabIndex = 346
        Me.MyLabel2.Text = "Date"
        '
        'txtGateEntryDate
        '
        Me.txtGateEntryDate.CalculationExpression = Nothing
        Me.txtGateEntryDate.CustomFormat = "dd/MM/yyyy"
        Me.txtGateEntryDate.FieldCode = Nothing
        Me.txtGateEntryDate.FieldDesc = Nothing
        Me.txtGateEntryDate.FieldMaxLength = 0
        Me.txtGateEntryDate.FieldName = Nothing
        Me.txtGateEntryDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGateEntryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtGateEntryDate.isCalculatedField = False
        Me.txtGateEntryDate.IsSourceFromTable = False
        Me.txtGateEntryDate.IsSourceFromValueList = False
        Me.txtGateEntryDate.IsUnique = False
        Me.txtGateEntryDate.Location = New System.Drawing.Point(626, 125)
        Me.txtGateEntryDate.MendatroryField = True
        Me.txtGateEntryDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtGateEntryDate.MyLinkLable1 = Me.MyLabel2
        Me.txtGateEntryDate.MyLinkLable2 = Nothing
        Me.txtGateEntryDate.Name = "txtGateEntryDate"
        Me.txtGateEntryDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtGateEntryDate.ReferenceFieldDesc = Nothing
        Me.txtGateEntryDate.ReferenceFieldName = Nothing
        Me.txtGateEntryDate.ReferenceTableName = Nothing
        Me.txtGateEntryDate.Size = New System.Drawing.Size(78, 18)
        Me.txtGateEntryDate.TabIndex = 345
        Me.txtGateEntryDate.TabStop = False
        Me.txtGateEntryDate.Text = "03/05/2011"
        Me.txtGateEntryDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'txtJobLocation
        '
        Me.txtJobLocation.CalculationExpression = Nothing
        Me.txtJobLocation.FieldCode = Nothing
        Me.txtJobLocation.FieldDesc = Nothing
        Me.txtJobLocation.FieldMaxLength = 0
        Me.txtJobLocation.FieldName = Nothing
        Me.txtJobLocation.isCalculatedField = False
        Me.txtJobLocation.IsSourceFromTable = False
        Me.txtJobLocation.IsSourceFromValueList = False
        Me.txtJobLocation.IsUnique = False
        Me.txtJobLocation.Location = New System.Drawing.Point(100, 83)
        Me.txtJobLocation.MendatroryField = True
        Me.txtJobLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJobLocation.MyLinkLable1 = Me.MyLabel1
        Me.txtJobLocation.MyLinkLable2 = Me.lblJobLocation
        Me.txtJobLocation.MyReadOnly = False
        Me.txtJobLocation.MyShowMasterFormButton = False
        Me.txtJobLocation.Name = "txtJobLocation"
        Me.txtJobLocation.ReferenceFieldDesc = Nothing
        Me.txtJobLocation.ReferenceFieldName = Nothing
        Me.txtJobLocation.ReferenceTableName = Nothing
        Me.txtJobLocation.Size = New System.Drawing.Size(255, 19)
        Me.txtJobLocation.TabIndex = 344
        Me.txtJobLocation.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(3, 83)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(69, 18)
        Me.MyLabel1.TabIndex = 342
        Me.MyLabel1.Text = "Job Location"
        '
        'lblJobLocation
        '
        Me.lblJobLocation.AutoSize = False
        Me.lblJobLocation.BorderVisible = True
        Me.lblJobLocation.FieldName = Nothing
        Me.lblJobLocation.Location = New System.Drawing.Point(355, 82)
        Me.lblJobLocation.Name = "lblJobLocation"
        Me.lblJobLocation.Size = New System.Drawing.Size(349, 21)
        Me.lblJobLocation.TabIndex = 343
        Me.lblJobLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(100, 62)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblLocation
        Me.txtLocation.MyLinkLable2 = Me.lblLocationDesc
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(255, 19)
        Me.txtLocation.TabIndex = 340
        Me.txtLocation.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Location = New System.Drawing.Point(3, 62)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 327
        Me.lblLocation.Text = "Location"
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.FieldName = Nothing
        Me.lblLocationDesc.Location = New System.Drawing.Point(355, 61)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(349, 21)
        Me.lblLocationDesc.TabIndex = 328
        Me.lblLocationDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtVendor
        '
        Me.txtVendor.CalculationExpression = Nothing
        Me.txtVendor.FieldCode = Nothing
        Me.txtVendor.FieldDesc = Nothing
        Me.txtVendor.FieldMaxLength = 0
        Me.txtVendor.FieldName = Nothing
        Me.txtVendor.isCalculatedField = False
        Me.txtVendor.IsSourceFromTable = False
        Me.txtVendor.IsSourceFromValueList = False
        Me.txtVendor.IsUnique = False
        Me.txtVendor.Location = New System.Drawing.Point(100, 104)
        Me.txtVendor.MendatroryField = True
        Me.txtVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendor.MyLinkLable1 = Me.lblVendor
        Me.txtVendor.MyLinkLable2 = Me.lblVendorName
        Me.txtVendor.MyReadOnly = False
        Me.txtVendor.MyShowMasterFormButton = False
        Me.txtVendor.Name = "txtVendor"
        Me.txtVendor.ReferenceFieldDesc = Nothing
        Me.txtVendor.ReferenceFieldName = Nothing
        Me.txtVendor.ReferenceTableName = Nothing
        Me.txtVendor.Size = New System.Drawing.Size(255, 19)
        Me.txtVendor.TabIndex = 339
        Me.txtVendor.Value = ""
        '
        'lblVendor
        '
        Me.lblVendor.FieldName = Nothing
        Me.lblVendor.Location = New System.Drawing.Point(3, 104)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Size = New System.Drawing.Size(43, 18)
        Me.lblVendor.TabIndex = 322
        Me.lblVendor.Text = "Vendor"
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Location = New System.Drawing.Point(355, 103)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(349, 21)
        Me.lblVendorName.TabIndex = 323
        Me.lblVendorName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grpGateEntryType
        '
        Me.grpGateEntryType.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpGateEntryType.Controls.Add(Me.chkSKU)
        Me.grpGateEntryType.Controls.Add(Me.rbtnManual)
        Me.grpGateEntryType.Controls.Add(Me.rbtnTanker)
        Me.grpGateEntryType.HeaderText = ""
        Me.grpGateEntryType.Location = New System.Drawing.Point(100, 11)
        Me.grpGateEntryType.Name = "grpGateEntryType"
        Me.grpGateEntryType.Size = New System.Drawing.Size(312, 26)
        Me.grpGateEntryType.TabIndex = 341
        '
        'chkSKU
        '
        Me.chkSKU.Location = New System.Drawing.Point(204, 4)
        Me.chkSKU.Name = "chkSKU"
        Me.chkSKU.Size = New System.Drawing.Size(41, 18)
        Me.chkSKU.TabIndex = 3
        Me.chkSKU.Text = "SKU"
        '
        'rbtnManual
        '
        Me.rbtnManual.Location = New System.Drawing.Point(5, 4)
        Me.rbtnManual.Name = "rbtnManual"
        Me.rbtnManual.Size = New System.Drawing.Size(98, 18)
        Me.rbtnManual.TabIndex = 2
        Me.rbtnManual.Tag = "Manual"
        Me.rbtnManual.Text = "Manual Receipt"
        '
        'rbtnTanker
        '
        Me.rbtnTanker.Location = New System.Drawing.Point(104, 4)
        Me.rbtnTanker.Name = "rbtnTanker"
        Me.rbtnTanker.Size = New System.Drawing.Size(94, 18)
        Me.rbtnTanker.TabIndex = 0
        Me.rbtnTanker.Text = "Tanker Receipt"
        '
        'lblUnloadingNo
        '
        Me.lblUnloadingNo.AutoSize = False
        Me.lblUnloadingNo.BorderVisible = True
        Me.lblUnloadingNo.FieldName = Nothing
        Me.lblUnloadingNo.Location = New System.Drawing.Point(100, 146)
        Me.lblUnloadingNo.Name = "lblUnloadingNo"
        Me.lblUnloadingNo.Size = New System.Drawing.Size(255, 21)
        Me.lblUnloadingNo.TabIndex = 339
        Me.lblUnloadingNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUnloadingNo.Visible = False
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.CalculationExpression = Nothing
        Me.txtVehicleNo.FieldCode = Nothing
        Me.txtVehicleNo.FieldDesc = Nothing
        Me.txtVehicleNo.FieldMaxLength = 0
        Me.txtVehicleNo.FieldName = Nothing
        Me.txtVehicleNo.isCalculatedField = False
        Me.txtVehicleNo.IsSourceFromTable = False
        Me.txtVehicleNo.IsSourceFromValueList = False
        Me.txtVehicleNo.IsUnique = False
        Me.txtVehicleNo.Location = New System.Drawing.Point(100, 125)
        Me.txtVehicleNo.MendatroryField = True
        Me.txtVehicleNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleNo.MyLinkLable1 = Me.lblTankerNo
        Me.txtVehicleNo.MyLinkLable2 = Nothing
        Me.txtVehicleNo.MyReadOnly = False
        Me.txtVehicleNo.MyShowMasterFormButton = False
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.ReferenceFieldDesc = Nothing
        Me.txtVehicleNo.ReferenceFieldName = Nothing
        Me.txtVehicleNo.ReferenceTableName = Nothing
        Me.txtVehicleNo.Size = New System.Drawing.Size(255, 19)
        Me.txtVehicleNo.TabIndex = 338
        Me.txtVehicleNo.Value = ""
        '
        'lblTankerNo
        '
        Me.lblTankerNo.FieldName = Nothing
        Me.lblTankerNo.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblTankerNo.Location = New System.Drawing.Point(3, 126)
        Me.lblTankerNo.Name = "lblTankerNo"
        Me.lblTankerNo.Size = New System.Drawing.Size(88, 16)
        Me.lblTankerNo.TabIndex = 329
        Me.lblTankerNo.Text = "Tanker / Vehicle"
        '
        'txtGateEntryNo
        '
        Me.txtGateEntryNo.CalculationExpression = Nothing
        Me.txtGateEntryNo.FieldCode = Nothing
        Me.txtGateEntryNo.FieldDesc = Nothing
        Me.txtGateEntryNo.FieldMaxLength = 0
        Me.txtGateEntryNo.FieldName = Nothing
        Me.txtGateEntryNo.isCalculatedField = False
        Me.txtGateEntryNo.IsSourceFromTable = False
        Me.txtGateEntryNo.IsSourceFromValueList = False
        Me.txtGateEntryNo.IsUnique = False
        Me.txtGateEntryNo.Location = New System.Drawing.Point(438, 124)
        Me.txtGateEntryNo.MaxLength = 50
        Me.txtGateEntryNo.MendatroryField = True
        Me.txtGateEntryNo.MyLinkLable1 = Me.lblGateEntryNo
        Me.txtGateEntryNo.MyLinkLable2 = Nothing
        Me.txtGateEntryNo.Name = "txtGateEntryNo"
        Me.txtGateEntryNo.ReferenceFieldDesc = Nothing
        Me.txtGateEntryNo.ReferenceFieldName = Nothing
        Me.txtGateEntryNo.ReferenceTableName = Nothing
        Me.txtGateEntryNo.Size = New System.Drawing.Size(155, 20)
        Me.txtGateEntryNo.TabIndex = 335
        '
        'lblGateEntryNo
        '
        Me.lblGateEntryNo.FieldName = Nothing
        Me.lblGateEntryNo.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblGateEntryNo.Location = New System.Drawing.Point(355, 126)
        Me.lblGateEntryNo.Name = "lblGateEntryNo"
        Me.lblGateEntryNo.Size = New System.Drawing.Size(78, 16)
        Me.lblGateEntryNo.TabIndex = 336
        Me.lblGateEntryNo.Text = "Gate Entry No"
        '
        'lblChallanDate
        '
        Me.lblChallanDate.FieldName = Nothing
        Me.lblChallanDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChallanDate.Location = New System.Drawing.Point(593, 148)
        Me.lblChallanDate.Name = "lblChallanDate"
        Me.lblChallanDate.Size = New System.Drawing.Size(30, 16)
        Me.lblChallanDate.TabIndex = 332
        Me.lblChallanDate.Text = "Date"
        '
        'txtChallanDate
        '
        Me.txtChallanDate.CalculationExpression = Nothing
        Me.txtChallanDate.CustomFormat = "dd/MM/yyyy"
        Me.txtChallanDate.FieldCode = Nothing
        Me.txtChallanDate.FieldDesc = Nothing
        Me.txtChallanDate.FieldMaxLength = 0
        Me.txtChallanDate.FieldName = Nothing
        Me.txtChallanDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChallanDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtChallanDate.isCalculatedField = False
        Me.txtChallanDate.IsSourceFromTable = False
        Me.txtChallanDate.IsSourceFromValueList = False
        Me.txtChallanDate.IsUnique = False
        Me.txtChallanDate.Location = New System.Drawing.Point(626, 147)
        Me.txtChallanDate.MendatroryField = True
        Me.txtChallanDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtChallanDate.MyLinkLable1 = Me.lblChallanDate
        Me.txtChallanDate.MyLinkLable2 = Nothing
        Me.txtChallanDate.Name = "txtChallanDate"
        Me.txtChallanDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtChallanDate.ReferenceFieldDesc = Nothing
        Me.txtChallanDate.ReferenceFieldName = Nothing
        Me.txtChallanDate.ReferenceTableName = Nothing
        Me.txtChallanDate.Size = New System.Drawing.Size(78, 18)
        Me.txtChallanDate.TabIndex = 7
        Me.txtChallanDate.TabStop = False
        Me.txtChallanDate.Text = "03/05/2011"
        Me.txtChallanDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'txtChallanNo
        '
        Me.txtChallanNo.CalculationExpression = Nothing
        Me.txtChallanNo.FieldCode = Nothing
        Me.txtChallanNo.FieldDesc = Nothing
        Me.txtChallanNo.FieldMaxLength = 0
        Me.txtChallanNo.FieldName = Nothing
        Me.txtChallanNo.isCalculatedField = False
        Me.txtChallanNo.IsSourceFromTable = False
        Me.txtChallanNo.IsSourceFromValueList = False
        Me.txtChallanNo.IsUnique = False
        Me.txtChallanNo.Location = New System.Drawing.Point(438, 146)
        Me.txtChallanNo.MaxLength = 50
        Me.txtChallanNo.MendatroryField = True
        Me.txtChallanNo.MyLinkLable1 = Me.lblChallanNo
        Me.txtChallanNo.MyLinkLable2 = Nothing
        Me.txtChallanNo.Name = "txtChallanNo"
        Me.txtChallanNo.ReferenceFieldDesc = Nothing
        Me.txtChallanNo.ReferenceFieldName = Nothing
        Me.txtChallanNo.ReferenceTableName = Nothing
        Me.txtChallanNo.Size = New System.Drawing.Size(155, 20)
        Me.txtChallanNo.TabIndex = 6
        '
        'lblChallanNo
        '
        Me.lblChallanNo.FieldName = Nothing
        Me.lblChallanNo.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblChallanNo.Location = New System.Drawing.Point(355, 148)
        Me.lblChallanNo.Name = "lblChallanNo"
        Me.lblChallanNo.Size = New System.Drawing.Size(65, 16)
        Me.lblChallanNo.TabIndex = 324
        Me.lblChallanNo.Text = "Challan No."
        '
        'btnReset
        '
        Me.btnReset.Image = Global.XpertERPJobWorkOutward.My.Resources.Resources.new1
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(394, 39)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(20, 21)
        Me.btnReset.TabIndex = 44
        '
        'lblSRNDate
        '
        Me.lblSRNDate.FieldName = Nothing
        Me.lblSRNDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSRNDate.Location = New System.Drawing.Point(415, 41)
        Me.lblSRNDate.Name = "lblSRNDate"
        Me.lblSRNDate.Size = New System.Drawing.Size(57, 16)
        Me.lblSRNDate.TabIndex = 19
        Me.lblSRNDate.Text = "SRN Date"
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
        Me.txtDate.Location = New System.Drawing.Point(476, 40)
        Me.txtDate.MendatroryField = True
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblSRNDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(127, 18)
        Me.txtDate.TabIndex = 1
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "03/05/2011 12:00 AM"
        Me.txtDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblSRNNo
        '
        Me.lblSRNNo.FieldName = Nothing
        Me.lblSRNNo.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblSRNNo.Location = New System.Drawing.Point(3, 41)
        Me.lblSRNNo.Name = "lblSRNNo"
        Me.lblSRNNo.Size = New System.Drawing.Size(48, 16)
        Me.lblSRNNo.TabIndex = 10
        Me.lblSRNNo.Text = "SRN No"
        '
        'lblPending
        '
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(606, 39)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(98, 20)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 11
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(100, 39)
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
        'gvItem
        '
        Me.gvItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvItem.ForeColor = System.Drawing.Color.Black
        Me.gvItem.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvItem.Location = New System.Drawing.Point(0, 0)
        '
        'gvItem
        '
        Me.gvItem.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.gvItem.MasterTemplate.AllowDeleteRow = False
        Me.gvItem.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvItem.Name = "gvItem"
        Me.gvItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvItem.ShowGroupPanel = False
        Me.gvItem.ShowHeaderCellButtons = True
        Me.gvItem.Size = New System.Drawing.Size(916, 204)
        Me.gvItem.TabIndex = 0
        Me.gvItem.TabStop = False
        Me.gvItem.Text = "RadGridView1"
        '
        'MyLabel6
        '
        Me.MyLabel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.ForeColor = System.Drawing.SystemColors.Highlight
        Me.MyLabel6.Location = New System.Drawing.Point(0, 204)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(916, 13)
        Me.MyLabel6.TabIndex = 31
        Me.MyLabel6.Text = "Press F5 Batch item Details"
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(916, 411)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(916, 411)
        Me.UcAttachment1.TabIndex = 0
        Me.UcAttachment1.TabStop = False
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuSetting})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(937, 20)
        Me.RadMenu1.TabIndex = 2
        Me.RadMenu1.Text = "Total"
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
        'btnShowInventory
        '
        Me.btnShowInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowInventory.Location = New System.Drawing.Point(407, 4)
        Me.btnShowInventory.Name = "btnShowInventory"
        Me.btnShowInventory.Size = New System.Drawing.Size(96, 22)
        Me.btnShowInventory.TabIndex = 51
        Me.btnShowInventory.Text = "Show Inventory"
        '
        'BtnHistory
        '
        Me.BtnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnHistory.Location = New System.Drawing.Point(506, 4)
        Me.BtnHistory.Name = "BtnHistory"
        Me.BtnHistory.Size = New System.Drawing.Size(96, 22)
        Me.BtnHistory.TabIndex = 50
        Me.BtnHistory.Text = "History"
        '
        'btnReverse
        '
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(287, 4)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(117, 22)
        Me.btnReverse.TabIndex = 5
        Me.btnReverse.Text = "Revese and Unpost"
        Me.btnReverse.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(866, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 22)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(145, 4)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(74, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'FrmJWOSRN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(937, 511)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmJWOSRN"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmJobMilkSRN"
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.pvpCustomFields.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblJobAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGateEntryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblJobLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpGateEntryType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpGateEntryType.ResumeLayout(False)
        Me.grpGateEntryType.PerformLayout()
        CType(Me.chkSKU, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnTanker, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnloadingNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGateEntryNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGateEntryNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChallanDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChallanDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChallanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChallanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSRNDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSRNNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnHistory, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblSRNDate As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblSRNNo As common.Controls.MyLabel
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents gvItem As common.UserControls.MyRadGridView
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents lblVendor As common.Controls.MyLabel
    Friend WithEvents txtChallanNo As common.Controls.MyTextBox
    Friend WithEvents lblChallanNo As common.Controls.MyLabel
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblChallanDate As common.Controls.MyLabel
    Friend WithEvents txtChallanDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblTankerNo As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents mnuSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuExit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtGateEntryNo As common.Controls.MyTextBox
    Friend WithEvents lblGateEntryNo As common.Controls.MyLabel
    Friend WithEvents txtVehicleNo As common.UserControls.txtFinder
    Friend WithEvents lblUnloadingNo As common.Controls.MyLabel
    Friend WithEvents grpGateEntryType As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rbtnTanker As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnManual As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents txtVendor As common.UserControls.txtFinder
    Friend WithEvents txtJobLocation As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblJobLocation As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtGateEntryDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblTotalAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents chkSKU As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblJobAmt As common.Controls.MyLabel
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents pvpCustomFields As RadPageViewPage
    Friend WithEvents Attachments As RadPageViewPage
    Friend WithEvents UcAttachment1 As ucAttachment
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents BtnHistory As RadButton
    Friend WithEvents btnShowInventory As RadButton
    Friend WithEvents txtJWOEstimate As common.UserControls.txtFinder
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
End Class

