<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerComplain
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
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.lblSalesman = New common.Controls.MyLabel()
        Me.lblpaymentno = New common.Controls.MyLabel()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.cboType = New common.Controls.MyComboBox()
        Me.lblfullempty = New common.Controls.MyLabel()
        Me.lblCustomer = New common.Controls.MyLabel()
        Me.fndCustom = New common.UserControls.txtFinder()
        Me.fndInvoice = New common.UserControls.txtFinder()
        Me.fndComplainCode = New common.UserControls.txtFinder()
        Me.lblComplainCode = New common.Controls.MyLabel()
        Me.lblInvocieDate = New common.Controls.MyLabel()
        Me.txtDocDate = New common.Controls.MyDateTimePicker()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.UsLock1 = New common.usLock()
        Me.lblRoute = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblpaymentpostdate = New common.Controls.MyLabel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.btnReverseAndUnpost = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfullempty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblComplainCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInvocieDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentpostdate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.btnReverseAndUnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        '
        'Gv1
        '
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.Name = "Gv1"
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(848, 305)
        Me.Gv1.TabIndex = 1
        Me.Gv1.Text = "RadGridView1"
        '
        'lblSalesman
        '
        Me.lblSalesman.FieldName = Nothing
        Me.lblSalesman.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesman.Location = New System.Drawing.Point(12, 46)
        Me.lblSalesman.Name = "lblSalesman"
        Me.lblSalesman.Size = New System.Drawing.Size(60, 16)
        Me.lblSalesman.TabIndex = 26
        Me.lblSalesman.Text = "Invoice No"
        '
        'lblpaymentno
        '
        Me.lblpaymentno.FieldName = Nothing
        Me.lblpaymentno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentno.Location = New System.Drawing.Point(12, 6)
        Me.lblpaymentno.Name = "lblpaymentno"
        Me.lblpaymentno.Size = New System.Drawing.Size(72, 16)
        Me.lblpaymentno.TabIndex = 28
        Me.lblpaymentno.Text = "Complain No"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(6, 12)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 24)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(778, 11)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 24)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'cboType
        '
        Me.cboType.AutoCompleteDisplayMember = Nothing
        Me.cboType.AutoCompleteValueMember = Nothing
        Me.cboType.CalculationExpression = Nothing
        Me.cboType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboType.FieldCode = Nothing
        Me.cboType.FieldDesc = Nothing
        Me.cboType.FieldMaxLength = 0
        Me.cboType.FieldName = Nothing
        Me.cboType.isCalculatedField = False
        Me.cboType.IsSourceFromTable = False
        Me.cboType.IsSourceFromValueList = False
        Me.cboType.IsUnique = False
        RadListDataItem1.Text = "Select"
        RadListDataItem2.Text = "Full"
        RadListDataItem3.Text = "Empty"
        Me.cboType.Items.Add(RadListDataItem1)
        Me.cboType.Items.Add(RadListDataItem2)
        Me.cboType.Items.Add(RadListDataItem3)
        Me.cboType.Location = New System.Drawing.Point(101, 65)
        Me.cboType.MendatroryField = False
        Me.cboType.MyLinkLable1 = Me.lblfullempty
        Me.cboType.MyLinkLable2 = Nothing
        Me.cboType.Name = "cboType"
        Me.cboType.ReferenceFieldDesc = Nothing
        Me.cboType.ReferenceFieldName = Nothing
        Me.cboType.ReferenceTableName = Nothing
        Me.cboType.Size = New System.Drawing.Size(132, 20)
        Me.cboType.TabIndex = 95
        '
        'lblfullempty
        '
        Me.lblfullempty.FieldName = Nothing
        Me.lblfullempty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfullempty.Location = New System.Drawing.Point(12, 67)
        Me.lblfullempty.Name = "lblfullempty"
        Me.lblfullempty.Size = New System.Drawing.Size(31, 16)
        Me.lblfullempty.TabIndex = 15
        Me.lblfullempty.Text = "Type"
        '
        'lblCustomer
        '
        Me.lblCustomer.AutoSize = False
        Me.lblCustomer.BorderVisible = True
        Me.lblCustomer.FieldName = Nothing
        Me.lblCustomer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(235, 25)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(230, 18)
        Me.lblCustomer.TabIndex = 94
        Me.lblCustomer.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustomer.TextWrap = False
        '
        'fndCustom
        '
        Me.fndCustom.CalculationExpression = Nothing
        Me.fndCustom.FieldCode = Nothing
        Me.fndCustom.FieldDesc = Nothing
        Me.fndCustom.FieldMaxLength = 0
        Me.fndCustom.FieldName = Nothing
        Me.fndCustom.isCalculatedField = False
        Me.fndCustom.IsSourceFromTable = False
        Me.fndCustom.IsSourceFromValueList = False
        Me.fndCustom.IsUnique = False
        Me.fndCustom.Location = New System.Drawing.Point(101, 25)
        Me.fndCustom.MendatroryField = True
        Me.fndCustom.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCustom.MyLinkLable1 = Nothing
        Me.fndCustom.MyLinkLable2 = Nothing
        Me.fndCustom.MyReadOnly = False
        Me.fndCustom.MyShowMasterFormButton = False
        Me.fndCustom.Name = "fndCustom"
        Me.fndCustom.ReferenceFieldDesc = Nothing
        Me.fndCustom.ReferenceFieldName = Nothing
        Me.fndCustom.ReferenceTableName = Nothing
        Me.fndCustom.Size = New System.Drawing.Size(132, 18)
        Me.fndCustom.TabIndex = 93
        Me.fndCustom.Value = ""
        '
        'fndInvoice
        '
        Me.fndInvoice.CalculationExpression = Nothing
        Me.fndInvoice.FieldCode = Nothing
        Me.fndInvoice.FieldDesc = Nothing
        Me.fndInvoice.FieldMaxLength = 0
        Me.fndInvoice.FieldName = Nothing
        Me.fndInvoice.isCalculatedField = False
        Me.fndInvoice.IsSourceFromTable = False
        Me.fndInvoice.IsSourceFromValueList = False
        Me.fndInvoice.IsUnique = False
        Me.fndInvoice.Location = New System.Drawing.Point(101, 45)
        Me.fndInvoice.MendatroryField = True
        Me.fndInvoice.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndInvoice.MyLinkLable1 = Nothing
        Me.fndInvoice.MyLinkLable2 = Nothing
        Me.fndInvoice.MyReadOnly = False
        Me.fndInvoice.MyShowMasterFormButton = False
        Me.fndInvoice.Name = "fndInvoice"
        Me.fndInvoice.ReferenceFieldDesc = Nothing
        Me.fndInvoice.ReferenceFieldName = Nothing
        Me.fndInvoice.ReferenceTableName = Nothing
        Me.fndInvoice.Size = New System.Drawing.Size(132, 18)
        Me.fndInvoice.TabIndex = 92
        Me.fndInvoice.Value = ""
        '
        'fndComplainCode
        '
        Me.fndComplainCode.CalculationExpression = Nothing
        Me.fndComplainCode.FieldCode = Nothing
        Me.fndComplainCode.FieldDesc = Nothing
        Me.fndComplainCode.FieldMaxLength = 0
        Me.fndComplainCode.FieldName = Nothing
        Me.fndComplainCode.isCalculatedField = False
        Me.fndComplainCode.IsSourceFromTable = False
        Me.fndComplainCode.IsSourceFromValueList = False
        Me.fndComplainCode.IsUnique = False
        Me.fndComplainCode.Location = New System.Drawing.Point(560, 43)
        Me.fndComplainCode.MendatroryField = True
        Me.fndComplainCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndComplainCode.MyLinkLable1 = Nothing
        Me.fndComplainCode.MyLinkLable2 = Nothing
        Me.fndComplainCode.MyReadOnly = False
        Me.fndComplainCode.MyShowMasterFormButton = False
        Me.fndComplainCode.Name = "fndComplainCode"
        Me.fndComplainCode.ReferenceFieldDesc = Nothing
        Me.fndComplainCode.ReferenceFieldName = Nothing
        Me.fndComplainCode.ReferenceTableName = Nothing
        Me.fndComplainCode.Size = New System.Drawing.Size(133, 18)
        Me.fndComplainCode.TabIndex = 91
        Me.fndComplainCode.Value = ""
        Me.fndComplainCode.Visible = False
        '
        'lblComplainCode
        '
        Me.lblComplainCode.AutoSize = False
        Me.lblComplainCode.BorderVisible = True
        Me.lblComplainCode.FieldName = Nothing
        Me.lblComplainCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComplainCode.Location = New System.Drawing.Point(694, 45)
        Me.lblComplainCode.Name = "lblComplainCode"
        Me.lblComplainCode.Size = New System.Drawing.Size(142, 18)
        Me.lblComplainCode.TabIndex = 89
        Me.lblComplainCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblComplainCode.TextWrap = False
        Me.lblComplainCode.Visible = False
        '
        'lblInvocieDate
        '
        Me.lblInvocieDate.AutoSize = False
        Me.lblInvocieDate.BorderVisible = True
        Me.lblInvocieDate.FieldName = Nothing
        Me.lblInvocieDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvocieDate.Location = New System.Drawing.Point(307, 45)
        Me.lblInvocieDate.Name = "lblInvocieDate"
        Me.lblInvocieDate.Size = New System.Drawing.Size(158, 18)
        Me.lblInvocieDate.TabIndex = 88
        Me.lblInvocieDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblInvocieDate.TextWrap = False
        '
        'txtDocDate
        '
        Me.txtDocDate.CalculationExpression = Nothing
        Me.txtDocDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDocDate.FieldCode = Nothing
        Me.txtDocDate.FieldDesc = Nothing
        Me.txtDocDate.FieldMaxLength = 0
        Me.txtDocDate.FieldName = Nothing
        Me.txtDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDocDate.isCalculatedField = False
        Me.txtDocDate.IsSourceFromTable = False
        Me.txtDocDate.IsSourceFromValueList = False
        Me.txtDocDate.IsUnique = False
        Me.txtDocDate.Location = New System.Drawing.Point(382, 5)
        Me.txtDocDate.MendatroryField = False
        Me.txtDocDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDocDate.MyLinkLable1 = Nothing
        Me.txtDocDate.MyLinkLable2 = Nothing
        Me.txtDocDate.Name = "txtDocDate"
        Me.txtDocDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDocDate.ReferenceFieldDesc = Nothing
        Me.txtDocDate.ReferenceFieldName = Nothing
        Me.txtDocDate.ReferenceTableName = Nothing
        Me.txtDocDate.Size = New System.Drawing.Size(83, 18)
        Me.txtDocDate.TabIndex = 86
        Me.txtDocDate.TabStop = False
        Me.txtDocDate.Text = "13/06/2011"
        Me.txtDocDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(101, 5)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Nothing
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(226, 18)
        Me.txtDocNo.TabIndex = 84
        Me.txtDocNo.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(327, 5)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 18)
        Me.btnAddNew.TabIndex = 85
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(480, 6)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(91, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 82
        '
        'lblRoute
        '
        Me.lblRoute.FieldName = Nothing
        Me.lblRoute.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoute.Location = New System.Drawing.Point(471, 45)
        Me.lblRoute.Name = "lblRoute"
        Me.lblRoute.Size = New System.Drawing.Size(84, 16)
        Me.lblRoute.TabIndex = 25
        Me.lblRoute.Text = "Complain Code"
        Me.lblRoute.Visible = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(12, 26)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel4.TabIndex = 27
        Me.MyLabel4.Text = "Customer"
        '
        'txtRemarks
        '
        Me.txtRemarks.AutoSize = False
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
        Me.txtRemarks.Location = New System.Drawing.Point(101, 88)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.MyLinkLable1 = Me.MyLabel3
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(364, 39)
        Me.txtRemarks.TabIndex = 6
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(12, 92)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel3.TabIndex = 24
        Me.MyLabel3.Text = "Remarks"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(239, 46)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel1.TabIndex = 19
        Me.MyLabel1.Text = "Invoice Date"
        '
        'lblpaymentpostdate
        '
        Me.lblpaymentpostdate.FieldName = Nothing
        Me.lblpaymentpostdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentpostdate.Location = New System.Drawing.Point(346, 6)
        Me.lblpaymentpostdate.Name = "lblpaymentpostdate"
        Me.lblpaymentpostdate.Size = New System.Drawing.Size(30, 16)
        Me.lblpaymentpostdate.TabIndex = 14
        Me.lblpaymentpostdate.Text = "Date"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverseAndUnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(848, 486)
        Me.SplitContainer1.SplitterDistance = 443
        Me.SplitContainer1.TabIndex = 3
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblpaymentno)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCustomer)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblpaymentpostdate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndCustom)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblfullempty)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndInvoice)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblSalesman)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndComplainCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblComplainCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblInvocieDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRemarks)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDocDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDocNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRoute)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddNew)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(848, 443)
        Me.SplitContainer2.SplitterDistance = 134
        Me.SplitContainer2.TabIndex = 2
        '
        'btnReverseAndUnpost
        '
        Me.btnReverseAndUnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverseAndUnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverseAndUnpost.Location = New System.Drawing.Point(218, 12)
        Me.btnReverseAndUnpost.Name = "btnReverseAndUnpost"
        Me.btnReverseAndUnpost.Size = New System.Drawing.Size(131, 24)
        Me.btnReverseAndUnpost.TabIndex = 59
        Me.btnReverseAndUnpost.Text = "Reverse and Unpost"
        Me.btnReverseAndUnpost.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(146, 12)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 24)
        Me.btnDelete.TabIndex = 6
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.Visible = False
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(76, 12)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 24)
        Me.btnPost.TabIndex = 4
        Me.btnPost.Text = "Post"
        Me.btnPost.Visible = False
        '
        'RadMenu1
        '
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(848, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'frmCustomerComplain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(848, 506)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmCustomerComplain"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Customer Complaint"
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfullempty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblComplainCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInvocieDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentpostdate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.btnReverseAndUnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents lblSalesman As common.Controls.MyLabel
    Friend WithEvents lblpaymentno As common.Controls.MyLabel
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblpaymentpostdate As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblfullempty As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblRoute As common.Controls.MyLabel
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblComplainCode As common.Controls.MyLabel
    Friend WithEvents lblInvocieDate As common.Controls.MyLabel
    Friend WithEvents txtDocDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents fndCustom As common.UserControls.txtFinder
    Friend WithEvents fndInvoice As common.UserControls.txtFinder
    Friend WithEvents fndComplainCode As common.UserControls.txtFinder
    Friend WithEvents cboType As common.Controls.MyComboBox
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnReverseAndUnpost As Telerik.WinControls.UI.RadButton
End Class

