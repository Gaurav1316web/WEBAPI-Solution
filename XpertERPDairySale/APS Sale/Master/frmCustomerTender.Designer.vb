<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerTender
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
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.txtTolerance = New common.MyNumBox()
        Me.txtTotalQty = New common.MyNumBox()
        Me.chkTPTInclude = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkTaxInclusive = New Telerik.WinControls.UI.RadCheckBox()
        Me.lbltolerance = New common.Controls.MyLabel()
        Me.lblQty = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.lblFromDate = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.lblCustomerGroupDesc = New common.Controls.MyLabel()
        Me.lblCustomerGroup = New common.Controls.MyLabel()
        Me.txtCustomerGroup = New common.UserControls.txtFinder()
        Me.lblItemDesc = New common.Controls.MyLabel()
        Me.lblItemCode = New common.Controls.MyLabel()
        Me.txtItemCode = New common.UserControls.txtFinder()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnAmendment = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.txtRemark = New common.Controls.MyTextBox()
        Me.lblRemark = New common.Controls.MyLabel()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTolerance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTPTInclude, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTaxInclusive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbltolerance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerGroupDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAmendment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(885, 20)
        Me.RadMenu1.TabIndex = 4
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem4, Me.RadMenuItem5})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        Me.RadMenuItem3.UseCompatibleTextRendering = False
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        Me.RadMenuItem1.UseCompatibleTextRendering = False
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Delete Layout"
        Me.RadMenuItem4.UseCompatibleTextRendering = False
        '
        'RadMenuItem5
        '
        Me.RadMenuItem5.Name = "RadMenuItem5"
        Me.RadMenuItem5.Text = "E-Mail/SMS Setting"
        Me.RadMenuItem5.UseCompatibleTextRendering = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAmendment)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(885, 430)
        Me.SplitContainer1.SplitterDistance = 392
        Me.SplitContainer1.TabIndex = 5
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRemark)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRemark)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnGo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtTolerance)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtTotalQty)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkTPTInclude)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkTaxInclusive)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lbltolerance)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblQty)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtToDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblToDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtFromDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblFromDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCustomerGroupDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCustomerGroup)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCustomerGroup)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblItemDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblItemCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtItemCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocationDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDocNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddNew)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(885, 392)
        Me.SplitContainer2.SplitterDistance = 123
        Me.SplitContainer2.TabIndex = 0
        '
        'btnGo
        '
        Me.btnGo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(567, 94)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(80, 22)
        Me.btnGo.TabIndex = 1445
        Me.btnGo.Text = ">>"
        '
        'txtTolerance
        '
        Me.txtTolerance.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTolerance.CalculationExpression = Nothing
        Me.txtTolerance.DecimalPlaces = 0
        Me.txtTolerance.FieldCode = Nothing
        Me.txtTolerance.FieldDesc = Nothing
        Me.txtTolerance.FieldMaxLength = 0
        Me.txtTolerance.FieldName = Nothing
        Me.txtTolerance.isCalculatedField = False
        Me.txtTolerance.IsSourceFromTable = False
        Me.txtTolerance.IsSourceFromValueList = False
        Me.txtTolerance.IsUnique = False
        Me.txtTolerance.Location = New System.Drawing.Point(793, 52)
        Me.txtTolerance.MendatroryField = False
        Me.txtTolerance.MyLinkLable1 = Nothing
        Me.txtTolerance.MyLinkLable2 = Nothing
        Me.txtTolerance.Name = "txtTolerance"
        Me.txtTolerance.ReferenceFieldDesc = Nothing
        Me.txtTolerance.ReferenceFieldName = Nothing
        Me.txtTolerance.ReferenceTableName = Nothing
        Me.txtTolerance.Size = New System.Drawing.Size(80, 20)
        Me.txtTolerance.TabIndex = 1444
        Me.txtTolerance.Text = "0"
        Me.txtTolerance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTolerance.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'txtTotalQty
        '
        Me.txtTotalQty.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTotalQty.CalculationExpression = Nothing
        Me.txtTotalQty.DecimalPlaces = 0
        Me.txtTotalQty.FieldCode = Nothing
        Me.txtTotalQty.FieldDesc = Nothing
        Me.txtTotalQty.FieldMaxLength = 0
        Me.txtTotalQty.FieldName = Nothing
        Me.txtTotalQty.isCalculatedField = False
        Me.txtTotalQty.IsSourceFromTable = False
        Me.txtTotalQty.IsSourceFromValueList = False
        Me.txtTotalQty.IsUnique = False
        Me.txtTotalQty.Location = New System.Drawing.Point(630, 52)
        Me.txtTotalQty.MendatroryField = False
        Me.txtTotalQty.MyLinkLable1 = Nothing
        Me.txtTotalQty.MyLinkLable2 = Nothing
        Me.txtTotalQty.Name = "txtTotalQty"
        Me.txtTotalQty.ReferenceFieldDesc = Nothing
        Me.txtTotalQty.ReferenceFieldName = Nothing
        Me.txtTotalQty.ReferenceTableName = Nothing
        Me.txtTotalQty.Size = New System.Drawing.Size(80, 20)
        Me.txtTotalQty.TabIndex = 1443
        Me.txtTotalQty.Text = "0"
        Me.txtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalQty.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'chkTPTInclude
        '
        Me.chkTPTInclude.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTPTInclude.Location = New System.Drawing.Point(659, 74)
        Me.chkTPTInclude.Name = "chkTPTInclude"
        Me.chkTPTInclude.Size = New System.Drawing.Size(89, 16)
        Me.chkTPTInclude.TabIndex = 1442
        Me.chkTPTInclude.Text = "TPT Inclusive"
        '
        'chkTaxInclusive
        '
        Me.chkTaxInclusive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTaxInclusive.Location = New System.Drawing.Point(567, 74)
        Me.chkTaxInclusive.Name = "chkTaxInclusive"
        Me.chkTaxInclusive.Size = New System.Drawing.Size(86, 16)
        Me.chkTaxInclusive.TabIndex = 1441
        Me.chkTaxInclusive.Text = "Tax Inclusive"
        '
        'lbltolerance
        '
        Me.lbltolerance.FieldName = Nothing
        Me.lbltolerance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltolerance.Location = New System.Drawing.Point(718, 54)
        Me.lbltolerance.Name = "lbltolerance"
        Me.lbltolerance.Size = New System.Drawing.Size(70, 16)
        Me.lbltolerance.TabIndex = 68
        Me.lbltolerance.Text = "Tolerance %"
        '
        'lblQty
        '
        Me.lblQty.FieldName = Nothing
        Me.lblQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQty.Location = New System.Drawing.Point(567, 54)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(52, 16)
        Me.lblQty.TabIndex = 66
        Me.lblQty.Text = "Total Qty"
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(794, 29)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.lblToDate
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(81, 18)
        Me.txtToDate.TabIndex = 64
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13/06/2011 11:29 AM"
        Me.txtToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(718, 30)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(46, 16)
        Me.lblToDate.TabIndex = 63
        Me.lblToDate.Text = "To Date"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(630, 29)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.lblFromDate
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(80, 18)
        Me.txtFromDate.TabIndex = 62
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13/06/2011 11:29 AM"
        Me.txtFromDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblFromDate
        '
        Me.lblFromDate.FieldName = Nothing
        Me.lblFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.Location = New System.Drawing.Point(567, 30)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(60, 16)
        Me.lblFromDate.TabIndex = 61
        Me.lblFromDate.Text = "From Date"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(567, 4)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(97, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 60
        '
        'lblCustomerGroupDesc
        '
        Me.lblCustomerGroupDesc.AutoSize = False
        Me.lblCustomerGroupDesc.BorderVisible = True
        Me.lblCustomerGroupDesc.FieldName = Nothing
        Me.lblCustomerGroupDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerGroupDesc.Location = New System.Drawing.Point(253, 73)
        Me.lblCustomerGroupDesc.Name = "lblCustomerGroupDesc"
        Me.lblCustomerGroupDesc.Size = New System.Drawing.Size(287, 18)
        Me.lblCustomerGroupDesc.TabIndex = 58
        Me.lblCustomerGroupDesc.TextWrap = False
        '
        'lblCustomerGroup
        '
        Me.lblCustomerGroup.FieldName = Nothing
        Me.lblCustomerGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerGroup.Location = New System.Drawing.Point(12, 74)
        Me.lblCustomerGroup.Name = "lblCustomerGroup"
        Me.lblCustomerGroup.Size = New System.Drawing.Size(90, 16)
        Me.lblCustomerGroup.TabIndex = 59
        Me.lblCustomerGroup.Text = "Customer Group"
        '
        'txtCustomerGroup
        '
        Me.txtCustomerGroup.CalculationExpression = Nothing
        Me.txtCustomerGroup.FieldCode = Nothing
        Me.txtCustomerGroup.FieldDesc = Nothing
        Me.txtCustomerGroup.FieldMaxLength = 0
        Me.txtCustomerGroup.FieldName = Nothing
        Me.txtCustomerGroup.isCalculatedField = False
        Me.txtCustomerGroup.IsSourceFromTable = False
        Me.txtCustomerGroup.IsSourceFromValueList = False
        Me.txtCustomerGroup.IsUnique = False
        Me.txtCustomerGroup.Location = New System.Drawing.Point(109, 73)
        Me.txtCustomerGroup.MendatroryField = True
        Me.txtCustomerGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerGroup.MyLinkLable1 = Me.lblCustomerGroup
        Me.txtCustomerGroup.MyLinkLable2 = Me.lblCustomerGroupDesc
        Me.txtCustomerGroup.MyReadOnly = False
        Me.txtCustomerGroup.MyShowMasterFormButton = False
        Me.txtCustomerGroup.Name = "txtCustomerGroup"
        Me.txtCustomerGroup.ReferenceFieldDesc = Nothing
        Me.txtCustomerGroup.ReferenceFieldName = Nothing
        Me.txtCustomerGroup.ReferenceTableName = Nothing
        Me.txtCustomerGroup.Size = New System.Drawing.Size(143, 19)
        Me.txtCustomerGroup.TabIndex = 57
        Me.txtCustomerGroup.Value = ""
        '
        'lblItemDesc
        '
        Me.lblItemDesc.AutoSize = False
        Me.lblItemDesc.BorderVisible = True
        Me.lblItemDesc.FieldName = Nothing
        Me.lblItemDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemDesc.Location = New System.Drawing.Point(253, 53)
        Me.lblItemDesc.Name = "lblItemDesc"
        Me.lblItemDesc.Size = New System.Drawing.Size(287, 18)
        Me.lblItemDesc.TabIndex = 55
        Me.lblItemDesc.TextWrap = False
        '
        'lblItemCode
        '
        Me.lblItemCode.FieldName = Nothing
        Me.lblItemCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemCode.Location = New System.Drawing.Point(12, 54)
        Me.lblItemCode.Name = "lblItemCode"
        Me.lblItemCode.Size = New System.Drawing.Size(58, 16)
        Me.lblItemCode.TabIndex = 56
        Me.lblItemCode.Text = "Item Code"
        '
        'txtItemCode
        '
        Me.txtItemCode.CalculationExpression = Nothing
        Me.txtItemCode.FieldCode = Nothing
        Me.txtItemCode.FieldDesc = Nothing
        Me.txtItemCode.FieldMaxLength = 0
        Me.txtItemCode.FieldName = Nothing
        Me.txtItemCode.isCalculatedField = False
        Me.txtItemCode.IsSourceFromTable = False
        Me.txtItemCode.IsSourceFromValueList = False
        Me.txtItemCode.IsUnique = False
        Me.txtItemCode.Location = New System.Drawing.Point(109, 53)
        Me.txtItemCode.MendatroryField = True
        Me.txtItemCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.MyLinkLable1 = Me.lblItemCode
        Me.txtItemCode.MyLinkLable2 = Me.lblItemDesc
        Me.txtItemCode.MyReadOnly = False
        Me.txtItemCode.MyShowMasterFormButton = False
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.ReferenceFieldDesc = Nothing
        Me.txtItemCode.ReferenceFieldName = Nothing
        Me.txtItemCode.ReferenceTableName = Nothing
        Me.txtItemCode.Size = New System.Drawing.Size(143, 19)
        Me.txtItemCode.TabIndex = 54
        Me.txtItemCode.Value = ""
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.FieldName = Nothing
        Me.lblLocationDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationDesc.Location = New System.Drawing.Point(253, 29)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(287, 18)
        Me.lblLocationDesc.TabIndex = 52
        Me.lblLocationDesc.TextWrap = False
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(12, 30)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 53
        Me.lblLocation.Text = "Location"
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
        Me.txtLocation.Location = New System.Drawing.Point(109, 29)
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
        Me.txtLocation.Size = New System.Drawing.Size(143, 19)
        Me.txtLocation.TabIndex = 51
        Me.txtLocation.Value = ""
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
        Me.txtDate.Location = New System.Drawing.Point(415, 5)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(125, 18)
        Me.txtDate.TabIndex = 49
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(383, 6)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 48
        Me.RadLabel4.Text = "Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(12, 6)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel1.TabIndex = 50
        Me.RadLabel1.Text = "Code"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(108, 4)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 30
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 20)
        Me.txtDocNo.TabIndex = 46
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(360, 4)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 47
        '
        'gv1
        '
        Me.gv1.AutoScroll = True
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.VerticalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(885, 265)
        Me.gv1.TabIndex = 1
        Me.gv1.TabStop = False
        Me.gv1.VarID = ""
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(806, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
        '
        'btnAmendment
        '
        Me.btnAmendment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAmendment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAmendment.Location = New System.Drawing.Point(234, 6)
        Me.btnAmendment.Name = "btnAmendment"
        Me.btnAmendment.Size = New System.Drawing.Size(69, 22)
        Me.btnAmendment.TabIndex = 7
        Me.btnAmendment.Text = "Amendment"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(86, 6)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 6
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(159, 6)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 5
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(12, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "Save"
        '
        'txtRemark
        '
        Me.txtRemark.CalculationExpression = Nothing
        Me.txtRemark.FieldCode = Nothing
        Me.txtRemark.FieldDesc = Nothing
        Me.txtRemark.FieldMaxLength = 0
        Me.txtRemark.FieldName = Nothing
        Me.txtRemark.isCalculatedField = False
        Me.txtRemark.IsSourceFromTable = False
        Me.txtRemark.IsSourceFromValueList = False
        Me.txtRemark.IsUnique = False
        Me.txtRemark.Location = New System.Drawing.Point(108, 96)
        Me.txtRemark.MendatroryField = False
        Me.txtRemark.MyLinkLable1 = Me.lblRemark
        Me.txtRemark.MyLinkLable2 = Nothing
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ReferenceFieldDesc = Nothing
        Me.txtRemark.ReferenceFieldName = Nothing
        Me.txtRemark.ReferenceTableName = Nothing
        Me.txtRemark.Size = New System.Drawing.Size(432, 20)
        Me.txtRemark.TabIndex = 1586
        '
        'lblRemark
        '
        Me.lblRemark.FieldName = Nothing
        Me.lblRemark.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemark.Location = New System.Drawing.Point(12, 96)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(46, 16)
        Me.lblRemark.TabIndex = 1585
        Me.lblRemark.Text = "Remark"
        '
        'frmCustomerTender
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(885, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmCustomerTender"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Customer Tender"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTolerance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTPTInclude, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTaxInclusive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbltolerance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerGroupDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAmendment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RadMenu1 As RadMenu
    Friend WithEvents RadMenuItem3 As RadMenuItem
    Friend WithEvents RadMenuItem1 As RadMenuItem
    Friend WithEvents RadMenuItem4 As RadMenuItem
    Friend WithEvents RadMenuItem5 As RadMenuItem
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents lblCustomerGroupDesc As common.Controls.MyLabel
    Friend WithEvents lblCustomerGroup As common.Controls.MyLabel
    Friend WithEvents txtCustomerGroup As common.UserControls.txtFinder
    Friend WithEvents lblItemDesc As common.Controls.MyLabel
    Friend WithEvents lblItemCode As common.Controls.MyLabel
    Friend WithEvents txtItemCode As common.UserControls.txtFinder
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblFromDate As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents lbltolerance As common.Controls.MyLabel
    Friend WithEvents lblQty As common.Controls.MyLabel
    Friend WithEvents chkTPTInclude As RadCheckBox
    Friend WithEvents chkTaxInclusive As RadCheckBox
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnAmendment As RadButton
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnPost As RadButton
    Friend WithEvents btnSave As RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents txtTolerance As common.MyNumBox
    Friend WithEvents txtTotalQty As common.MyNumBox
    Friend WithEvents btnGo As RadButton
    Friend WithEvents txtRemark As common.Controls.MyTextBox
    Friend WithEvents lblRemark As common.Controls.MyLabel
End Class
