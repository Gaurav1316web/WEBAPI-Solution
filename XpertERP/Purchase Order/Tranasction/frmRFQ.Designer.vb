<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRFQ
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
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtRFQNo = New common.UserControls.txtNavigator()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel23 = New common.Controls.MyLabel()
        Me.txtReqNo = New common.UserControls.txtFinder()
        Me.txtLastAppBy = New common.Controls.MyTextBox()
        Me.txtAmount = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtLastAppDate = New common.Controls.MyDateTimePicker()
        Me.txtreqDate = New common.Controls.MyDateTimePicker()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.UsLock1 = New common.usLock()
        Me.RadReqDetails = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.txtComment = New common.Controls.MyTextBox()
        Me.txtRmks = New common.Controls.MyTextBox()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLastAppBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLastAppDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtreqDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadReqDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadReqDetails.SuspendLayout()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRmks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(12, 9)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(48, 16)
        Me.RadLabel1.TabIndex = 8
        Me.RadLabel1.Text = "RFQ No"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(323, 7)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(21, 20)
        Me.btnAddNew.TabIndex = 1
        '
        'txtRFQNo
        '
        Me.txtRFQNo.FieldName = Nothing
        Me.txtRFQNo.Location = New System.Drawing.Point(72, 7)
        Me.txtRFQNo.MendatroryField = False
        Me.txtRFQNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtRFQNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtRFQNo.MyLinkLable1 = Me.RadLabel1
        Me.txtRFQNo.MyLinkLable2 = Nothing
        Me.txtRFQNo.MyMaxLength = 32767
        Me.txtRFQNo.MyReadOnly = True
        Me.txtRFQNo.Name = "txtRFQNo"
        Me.txtRFQNo.Size = New System.Drawing.Size(251, 20)
        Me.txtRFQNo.TabIndex = 0
        Me.txtRFQNo.Value = ""
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(364, 9)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 6
        Me.RadLabel4.Text = "Date"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
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
        Me.txtDate.Location = New System.Drawing.Point(400, 8)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(115, 18)
        Me.txtDate.TabIndex = 2
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "01/06/2013"
        Me.txtDate.Value = New Date(2013, 6, 1, 0, 0, 0, 0)
        '
        'RadLabel23
        '
        Me.RadLabel23.FieldName = Nothing
        Me.RadLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel23.Location = New System.Drawing.Point(12, 32)
        Me.RadLabel23.Name = "RadLabel23"
        Me.RadLabel23.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel23.TabIndex = 7
        Me.RadLabel23.Text = "Indent No"
        '
        'txtReqNo
        '
        Me.txtReqNo.CalculationExpression = Nothing
        Me.txtReqNo.FieldCode = Nothing
        Me.txtReqNo.FieldDesc = Nothing
        Me.txtReqNo.FieldMaxLength = 0
        Me.txtReqNo.FieldName = Nothing
        Me.txtReqNo.isCalculatedField = False
        Me.txtReqNo.IsSourceFromTable = False
        Me.txtReqNo.IsSourceFromValueList = False
        Me.txtReqNo.IsUnique = False
        Me.txtReqNo.Location = New System.Drawing.Point(73, 31)
        Me.txtReqNo.MendatroryField = True
        Me.txtReqNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReqNo.MyLinkLable1 = Me.RadLabel23
        Me.txtReqNo.MyLinkLable2 = Nothing
        Me.txtReqNo.MyReadOnly = True
        Me.txtReqNo.MyShowMasterFormButton = False
        Me.txtReqNo.Name = "txtReqNo"
        Me.txtReqNo.ReferenceFieldDesc = Nothing
        Me.txtReqNo.ReferenceFieldName = Nothing
        Me.txtReqNo.ReferenceTableName = Nothing
        Me.txtReqNo.Size = New System.Drawing.Size(271, 18)
        Me.txtReqNo.TabIndex = 3
        Me.txtReqNo.Value = ""
        '
        'txtLastAppBy
        '
        Me.txtLastAppBy.CalculationExpression = Nothing
        Me.txtLastAppBy.FieldCode = Nothing
        Me.txtLastAppBy.FieldDesc = Nothing
        Me.txtLastAppBy.FieldMaxLength = 0
        Me.txtLastAppBy.FieldName = Nothing
        Me.txtLastAppBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastAppBy.isCalculatedField = False
        Me.txtLastAppBy.IsSourceFromTable = False
        Me.txtLastAppBy.IsSourceFromValueList = False
        Me.txtLastAppBy.IsUnique = False
        Me.txtLastAppBy.Location = New System.Drawing.Point(107, 99)
        Me.txtLastAppBy.MaxLength = 200
        Me.txtLastAppBy.MendatroryField = False
        Me.txtLastAppBy.MyLinkLable1 = Nothing
        Me.txtLastAppBy.MyLinkLable2 = Nothing
        Me.txtLastAppBy.Name = "txtLastAppBy"
        Me.txtLastAppBy.ReferenceFieldDesc = Nothing
        Me.txtLastAppBy.ReferenceFieldName = Nothing
        Me.txtLastAppBy.ReferenceTableName = Nothing
        Me.txtLastAppBy.Size = New System.Drawing.Size(124, 18)
        Me.txtLastAppBy.TabIndex = 2
        '
        'txtAmount
        '
        Me.txtAmount.CalculationExpression = Nothing
        Me.txtAmount.FieldCode = Nothing
        Me.txtAmount.FieldDesc = Nothing
        Me.txtAmount.FieldMaxLength = 0
        Me.txtAmount.FieldName = Nothing
        Me.txtAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.isCalculatedField = False
        Me.txtAmount.IsSourceFromTable = False
        Me.txtAmount.IsSourceFromValueList = False
        Me.txtAmount.IsUnique = False
        Me.txtAmount.Location = New System.Drawing.Point(379, 19)
        Me.txtAmount.MaxLength = 200
        Me.txtAmount.MendatroryField = False
        Me.txtAmount.MyLinkLable1 = Nothing
        Me.txtAmount.MyLinkLable2 = Nothing
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.ReferenceFieldDesc = Nothing
        Me.txtAmount.ReferenceFieldName = Nothing
        Me.txtAmount.ReferenceTableName = Nothing
        Me.txtAmount.Size = New System.Drawing.Size(119, 18)
        Me.txtAmount.TabIndex = 1
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(272, 100)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(103, 16)
        Me.MyLabel4.TabIndex = 6
        Me.MyLabel4.Text = "Last Approval Date"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(6, 100)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(92, 16)
        Me.MyLabel3.TabIndex = 4
        Me.MyLabel3.Text = "Last Approval By"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(330, 20)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel2.TabIndex = 7
        Me.MyLabel2.Text = "Amount"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 20)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel1.TabIndex = 5
        Me.MyLabel1.Text = "Indent Date"
        '
        'txtLastAppDate
        '
        Me.txtLastAppDate.CalculationExpression = Nothing
        Me.txtLastAppDate.CustomFormat = "dd/MM/yyyy"
        Me.txtLastAppDate.FieldCode = Nothing
        Me.txtLastAppDate.FieldDesc = Nothing
        Me.txtLastAppDate.FieldMaxLength = 0
        Me.txtLastAppDate.FieldName = Nothing
        Me.txtLastAppDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastAppDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtLastAppDate.isCalculatedField = False
        Me.txtLastAppDate.IsSourceFromTable = False
        Me.txtLastAppDate.IsSourceFromValueList = False
        Me.txtLastAppDate.IsUnique = False
        Me.txtLastAppDate.Location = New System.Drawing.Point(379, 99)
        Me.txtLastAppDate.MendatroryField = False
        Me.txtLastAppDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtLastAppDate.MyLinkLable1 = Nothing
        Me.txtLastAppDate.MyLinkLable2 = Nothing
        Me.txtLastAppDate.Name = "txtLastAppDate"
        Me.txtLastAppDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtLastAppDate.ReferenceFieldDesc = Nothing
        Me.txtLastAppDate.ReferenceFieldName = Nothing
        Me.txtLastAppDate.ReferenceTableName = Nothing
        Me.txtLastAppDate.Size = New System.Drawing.Size(119, 18)
        Me.txtLastAppDate.TabIndex = 3
        Me.txtLastAppDate.TabStop = False
        Me.txtLastAppDate.Text = "01/06/2013"
        Me.txtLastAppDate.Value = New Date(2013, 6, 1, 0, 0, 0, 0)
        '
        'txtreqDate
        '
        Me.txtreqDate.CalculationExpression = Nothing
        Me.txtreqDate.CustomFormat = "dd/MM/yyyy"
        Me.txtreqDate.FieldCode = Nothing
        Me.txtreqDate.FieldDesc = Nothing
        Me.txtreqDate.FieldMaxLength = 0
        Me.txtreqDate.FieldName = Nothing
        Me.txtreqDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtreqDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtreqDate.isCalculatedField = False
        Me.txtreqDate.IsSourceFromTable = False
        Me.txtreqDate.IsSourceFromValueList = False
        Me.txtreqDate.IsUnique = False
        Me.txtreqDate.Location = New System.Drawing.Point(107, 19)
        Me.txtreqDate.MendatroryField = False
        Me.txtreqDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtreqDate.MyLinkLable1 = Me.MyLabel1
        Me.txtreqDate.MyLinkLable2 = Nothing
        Me.txtreqDate.Name = "txtreqDate"
        Me.txtreqDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtreqDate.ReferenceFieldDesc = Nothing
        Me.txtreqDate.ReferenceFieldName = Nothing
        Me.txtreqDate.ReferenceTableName = Nothing
        Me.txtreqDate.Size = New System.Drawing.Size(124, 18)
        Me.txtreqDate.TabIndex = 0
        Me.txtreqDate.TabStop = False
        Me.txtreqDate.Text = "01/06/2013"
        Me.txtreqDate.Value = New Date(2013, 6, 1, 0, 0, 0, 0)
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(154, 8)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(79, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(4, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.HeaderText = "Select Vendor [Double Click on vendor row to select Items]"
        Me.RadGroupBox2.Location = New System.Drawing.Point(8, 183)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(510, 205)
        Me.RadGroupBox2.TabIndex = 5
        Me.RadGroupBox2.Text = "Select Vendor [Double Click on vendor row to select Items]"
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
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(490, 175)
        Me.gv1.TabIndex = 1
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(449, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 22)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(364, 30)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(151, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 9
        '
        'RadReqDetails
        '
        Me.RadReqDetails.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadReqDetails.Controls.Add(Me.RadLabel5)
        Me.RadReqDetails.Controls.Add(Me.RadLabel2)
        Me.RadReqDetails.Controls.Add(Me.RadLabel8)
        Me.RadReqDetails.Controls.Add(Me.txtComment)
        Me.RadReqDetails.Controls.Add(Me.txtRmks)
        Me.RadReqDetails.Controls.Add(Me.txtDesc)
        Me.RadReqDetails.Controls.Add(Me.txtLastAppBy)
        Me.RadReqDetails.Controls.Add(Me.MyLabel1)
        Me.RadReqDetails.Controls.Add(Me.txtAmount)
        Me.RadReqDetails.Controls.Add(Me.txtreqDate)
        Me.RadReqDetails.Controls.Add(Me.MyLabel4)
        Me.RadReqDetails.Controls.Add(Me.txtLastAppDate)
        Me.RadReqDetails.Controls.Add(Me.MyLabel3)
        Me.RadReqDetails.Controls.Add(Me.MyLabel2)
        Me.RadReqDetails.HeaderText = "Requision Detail"
        Me.RadReqDetails.HeaderTextAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.RadReqDetails.Location = New System.Drawing.Point(8, 54)
        Me.RadReqDetails.Name = "RadReqDetails"
        Me.RadReqDetails.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadReqDetails.Size = New System.Drawing.Size(508, 128)
        Me.RadReqDetails.TabIndex = 4
        Me.RadReqDetails.Text = "Requision Detail"
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(6, 40)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel5.TabIndex = 41
        Me.RadLabel5.Text = "Description"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(6, 80)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel2.TabIndex = 39
        Me.RadLabel2.Text = "Comment"
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(6, 60)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel8.TabIndex = 40
        Me.RadLabel8.Text = "Remarks"
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
        Me.txtComment.Location = New System.Drawing.Point(107, 79)
        Me.txtComment.MaxLength = 200
        Me.txtComment.MendatroryField = False
        Me.txtComment.MyLinkLable1 = Me.RadLabel2
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        Me.txtComment.Size = New System.Drawing.Size(391, 18)
        Me.txtComment.TabIndex = 38
        '
        'txtRmks
        '
        Me.txtRmks.CalculationExpression = Nothing
        Me.txtRmks.FieldCode = Nothing
        Me.txtRmks.FieldDesc = Nothing
        Me.txtRmks.FieldMaxLength = 0
        Me.txtRmks.FieldName = Nothing
        Me.txtRmks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRmks.isCalculatedField = False
        Me.txtRmks.IsSourceFromTable = False
        Me.txtRmks.IsSourceFromValueList = False
        Me.txtRmks.IsUnique = False
        Me.txtRmks.Location = New System.Drawing.Point(107, 59)
        Me.txtRmks.MaxLength = 200
        Me.txtRmks.MendatroryField = False
        Me.txtRmks.MyLinkLable1 = Me.RadLabel8
        Me.txtRmks.MyLinkLable2 = Nothing
        Me.txtRmks.Name = "txtRmks"
        Me.txtRmks.ReferenceFieldDesc = Nothing
        Me.txtRmks.ReferenceFieldName = Nothing
        Me.txtRmks.ReferenceTableName = Nothing
        Me.txtRmks.Size = New System.Drawing.Size(391, 18)
        Me.txtRmks.TabIndex = 37
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
        Me.txtDesc.Location = New System.Drawing.Point(107, 39)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel5
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(391, 18)
        Me.txtDesc.TabIndex = 36
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadReqDetails)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRFQNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtReqNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel23)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(531, 428)
        Me.SplitContainer1.SplitterDistance = 391
        Me.SplitContainer1.TabIndex = 0
        '
        'FrmRFQ
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(531, 428)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmRFQ"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Request For Quotation"
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLastAppBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLastAppDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtreqDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadReqDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadReqDetails.ResumeLayout(False)
        Me.RadReqDetails.PerformLayout()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRmks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtRFQNo As common.UserControls.txtNavigator
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel23 As common.Controls.MyLabel
    Friend WithEvents txtReqNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtreqDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtAmount As common.Controls.MyTextBox
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtLastAppBy As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtLastAppDate As common.Controls.MyDateTimePicker
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents RadReqDetails As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents txtRmks As common.Controls.MyTextBox
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
End Class

