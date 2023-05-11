<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProductionReceipt
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.fndIssueCode = New common.UserControls.txtFinder()
        Me.lblBatchNo = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.dtpIssueDate = New common.Controls.MyDateTimePicker()
        Me.lblBatchDate = New common.Controls.MyLabel()
        Me.chkTrading = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtBatchNo = New common.UserControls.txtFinder()
        Me.lblEmpName = New common.Controls.MyLabel()
        Me.lblReceivedBy = New common.Controls.MyLabel()
        Me.txtReceivedBy = New common.UserControls.txtFinder()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.lblReceiptCode = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.dtpBatchDate = New common.Controls.MyDateTimePicker()
        Me.txtComment = New common.Controls.MyTextBox()
        Me.dtpDate = New common.Controls.MyDateTimePicker()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.btnShowInventory = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnUnpost = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblBatchNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpIssueDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBatchDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTrading, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReceivedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReceiptCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpBatchDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnShowInventory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1036, 525)
        Me.SplitContainer1.SplitterDistance = 483
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1036, 483)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(2), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Production Receipt"
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(3), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Production Receipt"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.fndIssueCode)
        Me.RadPageViewPage1.Controls.Add(Me.dtpIssueDate)
        Me.RadPageViewPage1.Controls.Add(Me.chkTrading)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.lblBatchNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtBatchNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblEmpName)
        Me.RadPageViewPage1.Controls.Add(Me.lblReceivedBy)
        Me.RadPageViewPage1.Controls.Add(Me.txtReceivedBy)
        Me.RadPageViewPage1.Controls.Add(Me.lblBatchDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.lblReceiptCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocation)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtCode)
        Me.RadPageViewPage1.Controls.Add(Me.dtpBatchDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtComment)
        Me.RadPageViewPage1.Controls.Add(Me.dtpDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtDesc)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(112.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1015, 437)
        Me.RadPageViewPage1.Text = "Production Receipt"
        '
        'fndIssueCode
        '
        Me.fndIssueCode.CalculationExpression = Nothing
        Me.fndIssueCode.FieldCode = Nothing
        Me.fndIssueCode.FieldDesc = Nothing
        Me.fndIssueCode.FieldMaxLength = 0
        Me.fndIssueCode.FieldName = Nothing
        Me.fndIssueCode.isCalculatedField = False
        Me.fndIssueCode.IsSourceFromTable = False
        Me.fndIssueCode.IsSourceFromValueList = False
        Me.fndIssueCode.IsUnique = False
        Me.fndIssueCode.Location = New System.Drawing.Point(87, 23)
        Me.fndIssueCode.MendatroryField = True
        Me.fndIssueCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndIssueCode.MyLinkLable1 = Me.lblBatchNo
        Me.fndIssueCode.MyLinkLable2 = Me.lblLocation
        Me.fndIssueCode.MyReadOnly = False
        Me.fndIssueCode.MyShowMasterFormButton = False
        Me.fndIssueCode.Name = "fndIssueCode"
        Me.fndIssueCode.ReferenceFieldDesc = Nothing
        Me.fndIssueCode.ReferenceFieldName = Nothing
        Me.fndIssueCode.ReferenceTableName = Nothing
        Me.fndIssueCode.Size = New System.Drawing.Size(203, 20)
        Me.fndIssueCode.TabIndex = 23
        Me.fndIssueCode.Value = ""
        '
        'lblBatchNo
        '
        Me.lblBatchNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.lblBatchNo.FieldName = Nothing
        Me.lblBatchNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBatchNo.Location = New System.Drawing.Point(1, 23)
        Me.lblBatchNo.Name = "lblBatchNo"
        Me.lblBatchNo.Size = New System.Drawing.Size(53, 16)
        Me.lblBatchNo.TabIndex = 17
        Me.lblBatchNo.Text = "Batch No"
        '
        'lblLocation
        '
        Me.lblLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(718, 21)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(294, 20)
        Me.lblLocation.TabIndex = 14
        Me.lblLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpIssueDate
        '
        Me.dtpIssueDate.CalculationExpression = Nothing
        Me.dtpIssueDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpIssueDate.FieldCode = Nothing
        Me.dtpIssueDate.FieldDesc = Nothing
        Me.dtpIssueDate.FieldMaxLength = 0
        Me.dtpIssueDate.FieldName = Nothing
        Me.dtpIssueDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpIssueDate.isCalculatedField = False
        Me.dtpIssueDate.IsSourceFromTable = False
        Me.dtpIssueDate.IsSourceFromValueList = False
        Me.dtpIssueDate.IsUnique = False
        Me.dtpIssueDate.Location = New System.Drawing.Point(376, 22)
        Me.dtpIssueDate.MendatroryField = False
        Me.dtpIssueDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpIssueDate.MyLinkLable1 = Me.lblBatchDate
        Me.dtpIssueDate.MyLinkLable2 = Nothing
        Me.dtpIssueDate.Name = "dtpIssueDate"
        Me.dtpIssueDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpIssueDate.ReferenceFieldDesc = Nothing
        Me.dtpIssueDate.ReferenceFieldName = Nothing
        Me.dtpIssueDate.ReferenceTableName = Nothing
        Me.dtpIssueDate.Size = New System.Drawing.Size(81, 18)
        Me.dtpIssueDate.TabIndex = 24
        Me.dtpIssueDate.TabStop = False
        Me.dtpIssueDate.Text = "13/06/2011"
        Me.dtpIssueDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblBatchDate
        '
        Me.lblBatchDate.FieldName = Nothing
        Me.lblBatchDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBatchDate.Location = New System.Drawing.Point(313, 25)
        Me.lblBatchDate.Name = "lblBatchDate"
        Me.lblBatchDate.Size = New System.Drawing.Size(62, 16)
        Me.lblBatchDate.TabIndex = 16
        Me.lblBatchDate.Text = "Batch Date"
        '
        'chkTrading
        '
        Me.chkTrading.Location = New System.Drawing.Point(475, 2)
        Me.chkTrading.Name = "chkTrading"
        Me.chkTrading.Size = New System.Drawing.Size(58, 18)
        Me.chkTrading.TabIndex = 22
        Me.chkTrading.Text = "Trading"
        '
        'MyLabel5
        '
        Me.MyLabel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.ForeColor = System.Drawing.SystemColors.Highlight
        Me.MyLabel5.Location = New System.Drawing.Point(814, 422)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(196, 16)
        Me.MyLabel5.TabIndex = 21
        Me.MyLabel5.Text = "Press F4 to open serial item details"
        '
        'txtBatchNo
        '
        Me.txtBatchNo.CalculationExpression = Nothing
        Me.txtBatchNo.FieldCode = Nothing
        Me.txtBatchNo.FieldDesc = Nothing
        Me.txtBatchNo.FieldMaxLength = 0
        Me.txtBatchNo.FieldName = Nothing
        Me.txtBatchNo.isCalculatedField = False
        Me.txtBatchNo.IsSourceFromTable = False
        Me.txtBatchNo.IsSourceFromValueList = False
        Me.txtBatchNo.IsUnique = False
        Me.txtBatchNo.Location = New System.Drawing.Point(86, 24)
        Me.txtBatchNo.MendatroryField = True
        Me.txtBatchNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBatchNo.MyLinkLable1 = Me.lblBatchNo
        Me.txtBatchNo.MyLinkLable2 = Me.lblLocation
        Me.txtBatchNo.MyReadOnly = False
        Me.txtBatchNo.MyShowMasterFormButton = False
        Me.txtBatchNo.Name = "txtBatchNo"
        Me.txtBatchNo.ReferenceFieldDesc = Nothing
        Me.txtBatchNo.ReferenceFieldName = Nothing
        Me.txtBatchNo.ReferenceTableName = Nothing
        Me.txtBatchNo.Size = New System.Drawing.Size(203, 19)
        Me.txtBatchNo.TabIndex = 3
        Me.txtBatchNo.Value = ""
        '
        'lblEmpName
        '
        Me.lblEmpName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.FieldName = Nothing
        Me.lblEmpName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpName.Location = New System.Drawing.Point(718, 43)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(294, 20)
        Me.lblEmpName.TabIndex = 13
        Me.lblEmpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblReceivedBy
        '
        Me.lblReceivedBy.FieldName = Nothing
        Me.lblReceivedBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReceivedBy.Location = New System.Drawing.Point(475, 45)
        Me.lblReceivedBy.Name = "lblReceivedBy"
        Me.lblReceivedBy.Size = New System.Drawing.Size(70, 16)
        Me.lblReceivedBy.TabIndex = 12
        Me.lblReceivedBy.Text = "Received By"
        '
        'txtReceivedBy
        '
        Me.txtReceivedBy.CalculationExpression = Nothing
        Me.txtReceivedBy.FieldCode = Nothing
        Me.txtReceivedBy.FieldDesc = Nothing
        Me.txtReceivedBy.FieldMaxLength = 0
        Me.txtReceivedBy.FieldName = Nothing
        Me.txtReceivedBy.isCalculatedField = False
        Me.txtReceivedBy.IsSourceFromTable = False
        Me.txtReceivedBy.IsSourceFromValueList = False
        Me.txtReceivedBy.IsUnique = False
        Me.txtReceivedBy.Location = New System.Drawing.Point(558, 44)
        Me.txtReceivedBy.MendatroryField = True
        Me.txtReceivedBy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReceivedBy.MyLinkLable1 = Me.lblReceivedBy
        Me.txtReceivedBy.MyLinkLable2 = Me.lblEmpName
        Me.txtReceivedBy.MyReadOnly = False
        Me.txtReceivedBy.MyShowMasterFormButton = False
        Me.txtReceivedBy.Name = "txtReceivedBy"
        Me.txtReceivedBy.ReferenceFieldDesc = Nothing
        Me.txtReceivedBy.ReferenceFieldName = Nothing
        Me.txtReceivedBy.ReferenceTableName = Nothing
        Me.txtReceivedBy.Size = New System.Drawing.Size(148, 18)
        Me.txtReceivedBy.TabIndex = 7
        Me.txtReceivedBy.Value = ""
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(1, 46)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel5.TabIndex = 11
        Me.RadLabel5.Text = "Description"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Received Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 90)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1010, 329)
        Me.RadGroupBox2.TabIndex = 9
        Me.RadGroupBox2.Text = "Received Item Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowFilteringRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(990, 299)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(1, 65)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel2.TabIndex = 10
        Me.RadLabel2.Text = "Comment"
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(475, 23)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel6.TabIndex = 15
        Me.RadLabel6.Text = "Location"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(348, 3)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 19
        Me.RadLabel4.Text = "Date"
        '
        'lblReceiptCode
        '
        Me.lblReceiptCode.FieldName = Nothing
        Me.lblReceiptCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReceiptCode.Location = New System.Drawing.Point(1, 3)
        Me.lblReceiptCode.Name = "lblReceiptCode"
        Me.lblReceiptCode.Size = New System.Drawing.Size(75, 16)
        Me.lblReceiptCode.TabIndex = 18
        Me.lblReceiptCode.Text = "Receipt Code"
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
        Me.txtLocation.Location = New System.Drawing.Point(558, 22)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel6
        Me.txtLocation.MyLinkLable2 = Me.lblLocation
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(148, 18)
        Me.txtLocation.TabIndex = 5
        Me.txtLocation.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(592, -1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(114, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 20
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(86, 1)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblReceiptCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(230, 20)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'dtpBatchDate
        '
        Me.dtpBatchDate.CalculationExpression = Nothing
        Me.dtpBatchDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpBatchDate.FieldCode = Nothing
        Me.dtpBatchDate.FieldDesc = Nothing
        Me.dtpBatchDate.FieldMaxLength = 0
        Me.dtpBatchDate.FieldName = Nothing
        Me.dtpBatchDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpBatchDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBatchDate.isCalculatedField = False
        Me.dtpBatchDate.IsSourceFromTable = False
        Me.dtpBatchDate.IsSourceFromValueList = False
        Me.dtpBatchDate.IsUnique = False
        Me.dtpBatchDate.Location = New System.Drawing.Point(380, 22)
        Me.dtpBatchDate.MendatroryField = False
        Me.dtpBatchDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBatchDate.MyLinkLable1 = Me.lblBatchDate
        Me.dtpBatchDate.MyLinkLable2 = Nothing
        Me.dtpBatchDate.Name = "dtpBatchDate"
        Me.dtpBatchDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBatchDate.ReferenceFieldDesc = Nothing
        Me.dtpBatchDate.ReferenceFieldName = Nothing
        Me.dtpBatchDate.ReferenceTableName = Nothing
        Me.dtpBatchDate.Size = New System.Drawing.Size(79, 18)
        Me.dtpBatchDate.TabIndex = 4
        Me.dtpBatchDate.TabStop = False
        Me.dtpBatchDate.Text = "13/06/2011"
        Me.dtpBatchDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtComment
        '
        Me.txtComment.CalculationExpression = Nothing
        Me.txtComment.FieldCode = Nothing
        Me.txtComment.FieldDesc = Nothing
        Me.txtComment.FieldMaxLength = 0
        Me.txtComment.FieldName = Nothing
        Me.txtComment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComment.isCalculatedField = False
        Me.txtComment.IsSourceFromTable = False
        Me.txtComment.IsSourceFromValueList = False
        Me.txtComment.IsUnique = False
        Me.txtComment.Location = New System.Drawing.Point(86, 64)
        Me.txtComment.MaxLength = 200
        Me.txtComment.MendatroryField = False
        Me.txtComment.MyLinkLable1 = Me.RadLabel2
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        Me.txtComment.Size = New System.Drawing.Size(372, 18)
        Me.txtComment.TabIndex = 8
        '
        'dtpDate
        '
        Me.dtpDate.CalculationExpression = Nothing
        Me.dtpDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDate.FieldCode = Nothing
        Me.dtpDate.FieldDesc = Nothing
        Me.dtpDate.FieldMaxLength = 0
        Me.dtpDate.FieldName = Nothing
        Me.dtpDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.isCalculatedField = False
        Me.dtpDate.IsSourceFromTable = False
        Me.dtpDate.IsSourceFromValueList = False
        Me.dtpDate.IsUnique = False
        Me.dtpDate.Location = New System.Drawing.Point(380, 2)
        Me.dtpDate.MendatroryField = False
        Me.dtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.MyLinkLable1 = Me.RadLabel4
        Me.dtpDate.MyLinkLable2 = Nothing
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.ReferenceFieldDesc = Nothing
        Me.dtpDate.ReferenceFieldName = Nothing
        Me.dtpDate.ReferenceTableName = Nothing
        Me.dtpDate.Size = New System.Drawing.Size(79, 18)
        Me.dtpDate.TabIndex = 2
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "13/06/2011"
        Me.dtpDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtDesc
        '
        Me.txtDesc.CalculationExpression = Nothing
        Me.txtDesc.FieldCode = Nothing
        Me.txtDesc.FieldDesc = Nothing
        Me.txtDesc.FieldMaxLength = 0
        Me.txtDesc.FieldName = Nothing
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.isCalculatedField = False
        Me.txtDesc.IsSourceFromTable = False
        Me.txtDesc.IsSourceFromValueList = False
        Me.txtDesc.IsUnique = False
        Me.txtDesc.Location = New System.Drawing.Point(86, 45)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel5
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(372, 18)
        Me.txtDesc.TabIndex = 6
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(317, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'btnShowInventory
        '
        Me.btnShowInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowInventory.Location = New System.Drawing.Point(305, 5)
        Me.btnShowInventory.Name = "btnShowInventory"
        Me.btnShowInventory.Size = New System.Drawing.Size(102, 22)
        Me.btnShowInventory.TabIndex = 5
        Me.btnShowInventory.Text = "Show Inventory"
        '
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(230, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(80, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(155, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(962, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnUnpost
        '
        Me.btnUnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUnpost.Location = New System.Drawing.Point(413, 5)
        Me.btnUnpost.Name = "btnUnpost"
        Me.btnUnpost.Size = New System.Drawing.Size(69, 22)
        Me.btnUnpost.TabIndex = 6
        Me.btnUnpost.Text = "Reverse"
        '
        'frmProductionReceipt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1036, 525)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmProductionReceipt"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Production Receipt"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblBatchNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpIssueDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBatchDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTrading, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReceivedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReceiptCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpBatchDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents dtpDate As common.Controls.MyDateTimePicker
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpBatchDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblReceiptCode As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblBatchDate As common.Controls.MyLabel
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents lblReceivedBy As common.Controls.MyLabel
    Friend WithEvents txtReceivedBy As common.UserControls.txtFinder
    Friend WithEvents lblBatchNo As common.Controls.MyLabel
    Friend WithEvents txtBatchNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents fndIssueCode As common.UserControls.txtFinder
    Friend WithEvents chkTrading As RadCheckBox
    Friend WithEvents dtpIssueDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnShowInventory As RadButton
    Friend WithEvents btnUnpost As RadButton
End Class

