<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymentAdjEntry
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
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn4 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn5 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn6 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn7 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewDecimalColumn1 As Telerik.WinControls.UI.GridViewDecimalColumn = New Telerik.WinControls.UI.GridViewDecimalColumn()
        Dim GridViewTextBoxColumn8 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn9 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim SortDescriptor1 As Telerik.WinControls.Data.SortDescriptor = New Telerik.WinControls.Data.SortDescriptor()
        Me.txtEntrDesc = New common.Controls.MyTextBox()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.lblpaymentno = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.dtPost = New common.Controls.MyDateTimePicker()
        Me.lblpaymentpostdate = New common.Controls.MyLabel()
        Me.dtAdj = New common.Controls.MyDateTimePicker()
        Me.lblpaymentdate = New common.Controls.MyLabel()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.txtAdjAmt = New common.Controls.MyTextBox()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.txtDocAmt = New common.Controls.MyTextBox()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.txtVendorName = New common.Controls.MyTextBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnAdjClose = New Telerik.WinControls.UI.RadButton()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.lblInvisible = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.fndVendorCode = New common.UserControls.txtFinder()
        Me.fndDocNo = New common.UserControls.txtFinder()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.fndFnAdj = New common.UserControls.txtNavigator()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtBalanceAmt = New common.Controls.MyTextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.txtAmtBeforeRO = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtROAmount = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        CType(Me.txtEntrDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentno, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lblpaymentno.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentpostdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtAdj, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdjAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAdjClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInvisible, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBalanceAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAmtBeforeRO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtROAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtEntrDesc
        '
        Me.txtEntrDesc.CalculationExpression = Nothing
        Me.txtEntrDesc.FieldCode = Nothing
        Me.txtEntrDesc.FieldDesc = Nothing
        Me.txtEntrDesc.FieldMaxLength = 0
        Me.txtEntrDesc.FieldName = Nothing
        Me.txtEntrDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEntrDesc.isCalculatedField = False
        Me.txtEntrDesc.IsSourceFromTable = False
        Me.txtEntrDesc.IsSourceFromValueList = False
        Me.txtEntrDesc.IsUnique = False
        Me.txtEntrDesc.Location = New System.Drawing.Point(88, 27)
        Me.txtEntrDesc.MendatroryField = False
        Me.txtEntrDesc.MyLinkLable1 = Me.RadLabel1
        Me.txtEntrDesc.MyLinkLable2 = Nothing
        Me.txtEntrDesc.Name = "txtEntrDesc"
        Me.txtEntrDesc.ReferenceFieldDesc = Nothing
        Me.txtEntrDesc.ReferenceFieldName = Nothing
        Me.txtEntrDesc.ReferenceTableName = Nothing
        Me.txtEntrDesc.Size = New System.Drawing.Size(706, 18)
        Me.txtEntrDesc.TabIndex = 3
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(5, 28)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel1.TabIndex = 6
        Me.RadLabel1.Text = "Description"
        '
        'lblpaymentno
        '
        Me.lblpaymentno.Controls.Add(Me.RadLabel2)
        Me.lblpaymentno.FieldName = Nothing
        Me.lblpaymentno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentno.Location = New System.Drawing.Point(5, 7)
        Me.lblpaymentno.Name = "lblpaymentno"
        Me.lblpaymentno.Size = New System.Drawing.Size(78, 16)
        Me.lblpaymentno.TabIndex = 0
        Me.lblpaymentno.Text = "Settlement No"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(-346, 2)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(99, 16)
        Me.RadLabel2.TabIndex = 41
        Me.RadLabel2.Text = "Customer Number"
        '
        'dtPost
        '
        Me.dtPost.CalculationExpression = Nothing
        Me.dtPost.CustomFormat = "dd/MM/yyyy"
        Me.dtPost.FieldCode = Nothing
        Me.dtPost.FieldDesc = Nothing
        Me.dtPost.FieldMaxLength = 0
        Me.dtPost.FieldName = Nothing
        Me.dtPost.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtPost.isCalculatedField = False
        Me.dtPost.IsSourceFromTable = False
        Me.dtPost.IsSourceFromValueList = False
        Me.dtPost.IsUnique = False
        Me.dtPost.Location = New System.Drawing.Point(615, 5)
        Me.dtPost.MendatroryField = False
        Me.dtPost.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtPost.MyLinkLable1 = Me.lblpaymentpostdate
        Me.dtPost.MyLinkLable2 = Nothing
        Me.dtPost.Name = "dtPost"
        Me.dtPost.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtPost.ReferenceFieldDesc = Nothing
        Me.dtPost.ReferenceFieldName = Nothing
        Me.dtPost.ReferenceTableName = Nothing
        Me.dtPost.Size = New System.Drawing.Size(80, 20)
        Me.dtPost.TabIndex = 2
        Me.dtPost.TabStop = False
        Me.dtPost.Text = "10/06/2011"
        Me.dtPost.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        Me.dtPost.Visible = False
        '
        'lblpaymentpostdate
        '
        Me.lblpaymentpostdate.FieldName = Nothing
        Me.lblpaymentpostdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentpostdate.Location = New System.Drawing.Point(553, 7)
        Me.lblpaymentpostdate.Name = "lblpaymentpostdate"
        Me.lblpaymentpostdate.Size = New System.Drawing.Size(56, 16)
        Me.lblpaymentpostdate.TabIndex = 4
        Me.lblpaymentpostdate.Text = "Post Date"
        Me.lblpaymentpostdate.Visible = False
        '
        'dtAdj
        '
        Me.dtAdj.CalculationExpression = Nothing
        Me.dtAdj.CustomFormat = "dd/MM/yyyy"
        Me.dtAdj.FieldCode = Nothing
        Me.dtAdj.FieldDesc = Nothing
        Me.dtAdj.FieldMaxLength = 0
        Me.dtAdj.FieldName = Nothing
        Me.dtAdj.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtAdj.isCalculatedField = False
        Me.dtAdj.IsSourceFromTable = False
        Me.dtAdj.IsSourceFromValueList = False
        Me.dtAdj.IsUnique = False
        Me.dtAdj.Location = New System.Drawing.Point(467, 5)
        Me.dtAdj.MendatroryField = False
        Me.dtAdj.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtAdj.MyLinkLable1 = Me.lblpaymentdate
        Me.dtAdj.MyLinkLable2 = Nothing
        Me.dtAdj.Name = "dtAdj"
        Me.dtAdj.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtAdj.ReferenceFieldDesc = Nothing
        Me.dtAdj.ReferenceFieldName = Nothing
        Me.dtAdj.ReferenceTableName = Nothing
        Me.dtAdj.Size = New System.Drawing.Size(81, 20)
        Me.dtAdj.TabIndex = 1
        Me.dtAdj.TabStop = False
        Me.dtAdj.Text = "10/06/2011"
        Me.dtAdj.Value = New Date(2011, 6, 10, 11, 47, 26, 250)
        '
        'lblpaymentdate
        '
        Me.lblpaymentdate.FieldName = Nothing
        Me.lblpaymentdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentdate.Location = New System.Drawing.Point(373, 7)
        Me.lblpaymentdate.Name = "lblpaymentdate"
        Me.lblpaymentdate.Size = New System.Drawing.Size(87, 16)
        Me.lblpaymentdate.TabIndex = 2
        Me.lblpaymentdate.Text = "Settlement Date"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        GridViewTextBoxColumn1.EnableExpressionEditor = False
        GridViewTextBoxColumn1.HeaderText = "Line No"
        GridViewTextBoxColumn1.Name = "lineno"
        GridViewTextBoxColumn1.ReadOnly = True
        GridViewTextBoxColumn2.EnableExpressionEditor = False
        GridViewTextBoxColumn2.HeaderText = "Farmer Code"
        GridViewTextBoxColumn2.Name = "FarmerCode"
        GridViewTextBoxColumn2.ReadOnly = True
        GridViewTextBoxColumn2.Width = 100
        GridViewTextBoxColumn3.EnableExpressionEditor = False
        GridViewTextBoxColumn3.HeaderText = "Farmer Name"
        GridViewTextBoxColumn3.Name = "FarmerName"
        GridViewTextBoxColumn3.ReadOnly = True
        GridViewTextBoxColumn3.Width = 100
        GridViewTextBoxColumn4.EnableExpressionEditor = False
        GridViewTextBoxColumn4.HeaderText = "Discount Code"
        GridViewTextBoxColumn4.Name = "DiscountCode"
        GridViewTextBoxColumn4.Width = 150
        GridViewTextBoxColumn5.EnableExpressionEditor = False
        GridViewTextBoxColumn5.HeaderText = "Discount Description"
        GridViewTextBoxColumn5.Name = "DiscountDescription"
        GridViewTextBoxColumn5.ReadOnly = True
        GridViewTextBoxColumn5.Width = 200
        GridViewTextBoxColumn6.EnableExpressionEditor = False
        GridViewTextBoxColumn6.HeaderText = "Account Code"
        GridViewTextBoxColumn6.Name = "accountcode"
        GridViewTextBoxColumn6.Width = 150
        GridViewTextBoxColumn7.EnableExpressionEditor = False
        GridViewTextBoxColumn7.HeaderText = "Description"
        GridViewTextBoxColumn7.Name = "description"
        GridViewTextBoxColumn7.ReadOnly = True
        GridViewTextBoxColumn7.Width = 200
        GridViewDecimalColumn1.EnableExpressionEditor = False
        GridViewDecimalColumn1.HeaderText = "Amount"
        GridViewDecimalColumn1.Name = "amt"
        GridViewDecimalColumn1.Width = 100
        GridViewTextBoxColumn8.EnableExpressionEditor = False
        GridViewTextBoxColumn8.HeaderText = "Remarks"
        GridViewTextBoxColumn8.Name = "Remarks"
        GridViewTextBoxColumn8.Width = 200
        GridViewTextBoxColumn9.EnableExpressionEditor = False
        GridViewTextBoxColumn9.HeaderText = "Farmer Invoice No"
        GridViewTextBoxColumn9.Name = "Farmer Invoice No"
        GridViewTextBoxColumn9.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending
        GridViewTextBoxColumn9.Width = 200
        Me.gv1.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewTextBoxColumn3, GridViewTextBoxColumn4, GridViewTextBoxColumn5, GridViewTextBoxColumn6, GridViewTextBoxColumn7, GridViewDecimalColumn1, GridViewTextBoxColumn8, GridViewTextBoxColumn9})
        Me.gv1.MasterTemplate.EnableGrouping = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        SortDescriptor1.PropertyName = "Farmer Invoice No"
        Me.gv1.MasterTemplate.SortDescriptors.AddRange(New Telerik.WinControls.Data.SortDescriptor() {SortDescriptor1})
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(806, 253)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'RadLabel7
        '
        Me.RadLabel7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(599, 4)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(103, 16)
        Me.RadLabel7.TabIndex = 4
        Me.RadLabel7.Text = "Settlement Amount"
        '
        'txtAdjAmt
        '
        Me.txtAdjAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAdjAmt.CalculationExpression = Nothing
        Me.txtAdjAmt.FieldCode = Nothing
        Me.txtAdjAmt.FieldDesc = Nothing
        Me.txtAdjAmt.FieldMaxLength = 0
        Me.txtAdjAmt.FieldName = Nothing
        Me.txtAdjAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdjAmt.isCalculatedField = False
        Me.txtAdjAmt.IsSourceFromTable = False
        Me.txtAdjAmt.IsSourceFromValueList = False
        Me.txtAdjAmt.IsUnique = False
        Me.txtAdjAmt.Location = New System.Drawing.Point(705, 3)
        Me.txtAdjAmt.MendatroryField = False
        Me.txtAdjAmt.MyLinkLable1 = Me.RadLabel7
        Me.txtAdjAmt.MyLinkLable2 = Nothing
        Me.txtAdjAmt.Name = "txtAdjAmt"
        Me.txtAdjAmt.ReadOnly = True
        Me.txtAdjAmt.ReferenceFieldDesc = Nothing
        Me.txtAdjAmt.ReferenceFieldName = Nothing
        Me.txtAdjAmt.ReferenceTableName = Nothing
        Me.txtAdjAmt.Size = New System.Drawing.Size(97, 18)
        Me.txtAdjAmt.TabIndex = 5
        Me.txtAdjAmt.TabStop = False
        Me.txtAdjAmt.Text = "0.00"
        Me.txtAdjAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(268, 69)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(100, 16)
        Me.RadLabel6.TabIndex = 13
        Me.RadLabel6.Text = "Document Amount"
        '
        'txtDocAmt
        '
        Me.txtDocAmt.CalculationExpression = Nothing
        Me.txtDocAmt.FieldCode = Nothing
        Me.txtDocAmt.FieldDesc = Nothing
        Me.txtDocAmt.FieldMaxLength = 0
        Me.txtDocAmt.FieldName = Nothing
        Me.txtDocAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocAmt.isCalculatedField = False
        Me.txtDocAmt.IsSourceFromTable = False
        Me.txtDocAmt.IsSourceFromValueList = False
        Me.txtDocAmt.IsUnique = False
        Me.txtDocAmt.Location = New System.Drawing.Point(369, 68)
        Me.txtDocAmt.MendatroryField = False
        Me.txtDocAmt.MyLinkLable1 = Me.RadLabel6
        Me.txtDocAmt.MyLinkLable2 = Nothing
        Me.txtDocAmt.Name = "txtDocAmt"
        Me.txtDocAmt.ReadOnly = True
        Me.txtDocAmt.ReferenceFieldDesc = Nothing
        Me.txtDocAmt.ReferenceFieldName = Nothing
        Me.txtDocAmt.ReferenceTableName = Nothing
        Me.txtDocAmt.Size = New System.Drawing.Size(97, 18)
        Me.txtDocAmt.TabIndex = 7
        Me.txtDocAmt.TabStop = False
        Me.txtDocAmt.Text = "0.00"
        Me.txtDocAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(5, 90)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel5.TabIndex = 16
        Me.RadLabel5.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(88, 89)
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.RadLabel5
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(706, 18)
        Me.txtRemarks.TabIndex = 8
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(5, 69)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(75, 16)
        Me.RadLabel4.TabIndex = 11
        Me.RadLabel4.Text = "Document No"
        '
        'txtVendorName
        '
        Me.txtVendorName.CalculationExpression = Nothing
        Me.txtVendorName.FieldCode = Nothing
        Me.txtVendorName.FieldDesc = Nothing
        Me.txtVendorName.FieldMaxLength = 0
        Me.txtVendorName.FieldName = Nothing
        Me.txtVendorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorName.isCalculatedField = False
        Me.txtVendorName.IsSourceFromTable = False
        Me.txtVendorName.IsSourceFromValueList = False
        Me.txtVendorName.IsUnique = False
        Me.txtVendorName.Location = New System.Drawing.Point(268, 47)
        Me.txtVendorName.MendatroryField = False
        Me.txtVendorName.MyLinkLable1 = Nothing
        Me.txtVendorName.MyLinkLable2 = Nothing
        Me.txtVendorName.Name = "txtVendorName"
        Me.txtVendorName.ReadOnly = True
        Me.txtVendorName.ReferenceFieldDesc = Nothing
        Me.txtVendorName.ReferenceFieldName = Nothing
        Me.txtVendorName.ReferenceTableName = Nothing
        Me.txtVendorName.Size = New System.Drawing.Size(526, 18)
        Me.txtVendorName.TabIndex = 5
        Me.txtVendorName.TabStop = False
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(5, 48)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel3.TabIndex = 8
        Me.RadLabel3.Text = "Vendor No"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 24)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 19)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(151, 24)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 19)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(77, 24)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 19)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnAdjClose
        '
        Me.btnAdjClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAdjClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdjClose.Location = New System.Drawing.Point(734, 24)
        Me.btnAdjClose.Name = "btnAdjClose"
        Me.btnAdjClose.Size = New System.Drawing.Size(68, 19)
        Me.btnAdjClose.TabIndex = 5
        Me.btnAdjClose.Text = "Close"
        '
        'btnNew
        '
        Me.btnNew.BackgroundImage = Global.ERP.My.Resources.Resources._new
        Me.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(342, 5)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(18, 20)
        Me.btnNew.TabIndex = 0
        '
        'lblInvisible
        '
        Me.lblInvisible.FieldName = Nothing
        Me.lblInvisible.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvisible.Location = New System.Drawing.Point(321, 389)
        Me.lblInvisible.Name = "lblInvisible"
        Me.lblInvisible.Size = New System.Drawing.Size(2, 2)
        Me.lblInvisible.TabIndex = 0
        Me.lblInvisible.Visible = False
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(677, 67)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(115, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 15
        '
        'fndVendorCode
        '
        Me.fndVendorCode.CalculationExpression = Nothing
        Me.fndVendorCode.FieldCode = Nothing
        Me.fndVendorCode.FieldDesc = Nothing
        Me.fndVendorCode.FieldMaxLength = 0
        Me.fndVendorCode.FieldName = Nothing
        Me.fndVendorCode.isCalculatedField = False
        Me.fndVendorCode.IsSourceFromTable = False
        Me.fndVendorCode.IsSourceFromValueList = False
        Me.fndVendorCode.IsUnique = False
        Me.fndVendorCode.Location = New System.Drawing.Point(88, 47)
        Me.fndVendorCode.MendatroryField = True
        Me.fndVendorCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVendorCode.MyLinkLable1 = Me.RadLabel3
        Me.fndVendorCode.MyLinkLable2 = Nothing
        Me.fndVendorCode.MyReadOnly = False
        Me.fndVendorCode.MyShowMasterFormButton = False
        Me.fndVendorCode.Name = "fndVendorCode"
        Me.fndVendorCode.ReferenceFieldDesc = Nothing
        Me.fndVendorCode.ReferenceFieldName = Nothing
        Me.fndVendorCode.ReferenceTableName = Nothing
        Me.fndVendorCode.Size = New System.Drawing.Size(175, 19)
        Me.fndVendorCode.TabIndex = 4
        Me.fndVendorCode.Value = ""
        '
        'fndDocNo
        '
        Me.fndDocNo.CalculationExpression = Nothing
        Me.fndDocNo.FieldCode = Nothing
        Me.fndDocNo.FieldDesc = Nothing
        Me.fndDocNo.FieldMaxLength = 0
        Me.fndDocNo.FieldName = Nothing
        Me.fndDocNo.isCalculatedField = False
        Me.fndDocNo.IsSourceFromTable = False
        Me.fndDocNo.IsSourceFromValueList = False
        Me.fndDocNo.IsUnique = False
        Me.fndDocNo.Location = New System.Drawing.Point(88, 68)
        Me.fndDocNo.MendatroryField = True
        Me.fndDocNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDocNo.MyLinkLable1 = Me.RadLabel4
        Me.fndDocNo.MyLinkLable2 = Nothing
        Me.fndDocNo.MyReadOnly = False
        Me.fndDocNo.MyShowMasterFormButton = False
        Me.fndDocNo.Name = "fndDocNo"
        Me.fndDocNo.ReferenceFieldDesc = Nothing
        Me.fndDocNo.ReferenceFieldName = Nothing
        Me.fndDocNo.ReferenceTableName = Nothing
        Me.fndDocNo.Size = New System.Drawing.Size(175, 19)
        Me.fndDocNo.TabIndex = 6
        Me.fndDocNo.Value = ""
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndFnAdj)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBalanceAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblpaymentno)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndDocNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEntrDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndVendorCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblpaymentdate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtAdj)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtVendorName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblpaymentpostdate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDocAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtPost)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRemarks)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(806, 416)
        Me.SplitContainer1.SplitterDistance = 111
        Me.SplitContainer1.TabIndex = 0
        '
        'fndFnAdj
        '
        Me.fndFnAdj.FieldName = Nothing
        Me.fndFnAdj.Location = New System.Drawing.Point(89, 5)
        Me.fndFnAdj.MendatroryField = False
        Me.fndFnAdj.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndFnAdj.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndFnAdj.MyLinkLable1 = Nothing
        Me.fndFnAdj.MyLinkLable2 = Nothing
        Me.fndFnAdj.MyMaxLength = 32767
        Me.fndFnAdj.MyReadOnly = False
        Me.fndFnAdj.Name = "fndFnAdj"
        Me.fndFnAdj.Size = New System.Drawing.Size(253, 20)
        Me.fndFnAdj.TabIndex = 20
        Me.fndFnAdj.TabStop = False
        Me.fndFnAdj.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(477, 70)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(89, 16)
        Me.MyLabel1.TabIndex = 18
        Me.MyLabel1.Text = "Balance Amount"
        '
        'txtBalanceAmt
        '
        Me.txtBalanceAmt.CalculationExpression = Nothing
        Me.txtBalanceAmt.FieldCode = Nothing
        Me.txtBalanceAmt.FieldDesc = Nothing
        Me.txtBalanceAmt.FieldMaxLength = 0
        Me.txtBalanceAmt.FieldName = Nothing
        Me.txtBalanceAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalanceAmt.isCalculatedField = False
        Me.txtBalanceAmt.IsSourceFromTable = False
        Me.txtBalanceAmt.IsSourceFromValueList = False
        Me.txtBalanceAmt.IsUnique = False
        Me.txtBalanceAmt.Location = New System.Drawing.Point(573, 69)
        Me.txtBalanceAmt.MendatroryField = False
        Me.txtBalanceAmt.MyLinkLable1 = Me.MyLabel1
        Me.txtBalanceAmt.MyLinkLable2 = Nothing
        Me.txtBalanceAmt.Name = "txtBalanceAmt"
        Me.txtBalanceAmt.ReadOnly = True
        Me.txtBalanceAmt.ReferenceFieldDesc = Nothing
        Me.txtBalanceAmt.ReferenceFieldName = Nothing
        Me.txtBalanceAmt.ReferenceTableName = Nothing
        Me.txtBalanceAmt.Size = New System.Drawing.Size(97, 18)
        Me.txtBalanceAmt.TabIndex = 17
        Me.txtBalanceAmt.TabStop = False
        Me.txtBalanceAmt.Text = "0.00"
        Me.txtBalanceAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtROAmount)
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Controls.Add(Me.txtAmtBeforeRO)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Controls.Add(Me.btnReverse)
        Me.Panel1.Controls.Add(Me.btnPrint)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.btnPost)
        Me.Panel1.Controls.Add(Me.btnDelete)
        Me.Panel1.Controls.Add(Me.btnAdjClose)
        Me.Panel1.Controls.Add(Me.txtAdjAmt)
        Me.Panel1.Controls.Add(Me.RadLabel7)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 253)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(806, 48)
        Me.Panel1.TabIndex = 1
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(592, 24)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(136, 19)
        Me.btnReverse.TabIndex = 4
        Me.btnReverse.Text = "Revese and Unpost"
        Me.btnReverse.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(225, 24)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 19)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'txtAmtBeforeRO
        '
        Me.txtAmtBeforeRO.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAmtBeforeRO.CalculationExpression = Nothing
        Me.txtAmtBeforeRO.FieldCode = Nothing
        Me.txtAmtBeforeRO.FieldDesc = Nothing
        Me.txtAmtBeforeRO.FieldMaxLength = 0
        Me.txtAmtBeforeRO.FieldName = Nothing
        Me.txtAmtBeforeRO.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmtBeforeRO.isCalculatedField = False
        Me.txtAmtBeforeRO.IsSourceFromTable = False
        Me.txtAmtBeforeRO.IsSourceFromValueList = False
        Me.txtAmtBeforeRO.IsUnique = False
        Me.txtAmtBeforeRO.Location = New System.Drawing.Point(343, 3)
        Me.txtAmtBeforeRO.MendatroryField = False
        Me.txtAmtBeforeRO.MyLinkLable1 = Me.MyLabel2
        Me.txtAmtBeforeRO.MyLinkLable2 = Nothing
        Me.txtAmtBeforeRO.Name = "txtAmtBeforeRO"
        Me.txtAmtBeforeRO.ReadOnly = True
        Me.txtAmtBeforeRO.ReferenceFieldDesc = Nothing
        Me.txtAmtBeforeRO.ReferenceFieldName = Nothing
        Me.txtAmtBeforeRO.ReferenceTableName = Nothing
        Me.txtAmtBeforeRO.Size = New System.Drawing.Size(97, 18)
        Me.txtAmtBeforeRO.TabIndex = 7
        Me.txtAmtBeforeRO.TabStop = False
        Me.txtAmtBeforeRO.Text = "0.00"
        Me.txtAmtBeforeRO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel2
        '
        Me.MyLabel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(209, 4)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(131, 16)
        Me.MyLabel2.TabIndex = 6
        Me.MyLabel2.Text = "Amount Before Roundoff"
        '
        'txtROAmount
        '
        Me.txtROAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtROAmount.CalculationExpression = Nothing
        Me.txtROAmount.FieldCode = Nothing
        Me.txtROAmount.FieldDesc = Nothing
        Me.txtROAmount.FieldMaxLength = 0
        Me.txtROAmount.FieldName = Nothing
        Me.txtROAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtROAmount.isCalculatedField = False
        Me.txtROAmount.IsSourceFromTable = False
        Me.txtROAmount.IsSourceFromValueList = False
        Me.txtROAmount.IsUnique = False
        Me.txtROAmount.Location = New System.Drawing.Point(498, 3)
        Me.txtROAmount.MendatroryField = False
        Me.txtROAmount.MyLinkLable1 = Me.MyLabel3
        Me.txtROAmount.MyLinkLable2 = Nothing
        Me.txtROAmount.Name = "txtROAmount"
        Me.txtROAmount.ReadOnly = True
        Me.txtROAmount.ReferenceFieldDesc = Nothing
        Me.txtROAmount.ReferenceFieldName = Nothing
        Me.txtROAmount.ReferenceTableName = Nothing
        Me.txtROAmount.Size = New System.Drawing.Size(97, 18)
        Me.txtROAmount.TabIndex = 9
        Me.txtROAmount.TabStop = False
        Me.txtROAmount.Text = "0.00"
        Me.txtROAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel3
        '
        Me.MyLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(443, 4)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel3.TabIndex = 8
        Me.MyLabel3.Text = "Roundoff"
        '
        'frmPaymentAdjEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(806, 416)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.lblInvisible)
        Me.Name = "frmPaymentAdjEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Adjustment Entry"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.txtEntrDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentno, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lblpaymentno.ResumeLayout(False)
        Me.lblpaymentno.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentpostdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtAdj, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdjAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAdjClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInvisible, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBalanceAmt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAmtBeforeRO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtROAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtEntrDesc As common.Controls.MyTextBox
    Friend WithEvents dtPost As common.Controls.MyDateTimePicker
    Friend WithEvents dtAdj As common.Controls.MyDateTimePicker
    Friend WithEvents txtVendorName As common.Controls.MyTextBox
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents txtAdjAmt As common.Controls.MyTextBox
    Friend WithEvents txtDocAmt As common.Controls.MyTextBox
    Friend WithEvents grdAdjDetails As common.UserControls.MyRadGridView
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnAdjClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents grdAdjustment As common.UserControls.MyRadGridView
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents fndVendorCode As common.UserControls.txtFinder
    Friend WithEvents fndDocNo As common.UserControls.txtFinder
    Friend WithEvents lblpaymentno As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblpaymentpostdate As common.Controls.MyLabel
    Friend WithEvents lblpaymentdate As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents lblInvisible As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtBalanceAmt As common.Controls.MyTextBox
    Friend WithEvents fndFnAdj As common.UserControls.txtNavigator
    Friend WithEvents txtROAmount As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtAmtBeforeRO As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
End Class

