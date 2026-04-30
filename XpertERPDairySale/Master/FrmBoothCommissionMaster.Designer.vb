<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBoothCommissionMaster
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.txtperDayQty = New common.MyNumBox()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblDate = New common.Controls.MyLabel()
        Me.chkInActive = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtComment = New common.Controls.MyTextBox()
        Me.lblComment = New System.Windows.Forms.Label()
        Me.txtRemark = New common.Controls.MyTextBox()
        Me.lblRemark = New System.Windows.Forms.Label()
        Me.lblMinPerDayQty = New common.Controls.MyLabel()
        Me.txtUOM = New common.UserControls.txtFinder()
        Me.lblCommissionUOM = New common.Controls.MyLabel()
        Me.chkMobileUser = New Telerik.WinControls.UI.RadCheckBox()
        Me.UsLock1 = New common.usLock()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.lblDocCode = New common.Controls.MyLabel()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnpost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.txtperDayQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMinPerDayQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCommissionUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMobileUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(974, 530)
        Me.SplitContainer1.SplitterDistance = 492
        Me.SplitContainer1.TabIndex = 0
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtperDayQty)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkInActive)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtComment)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblComment)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRemark)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRemark)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblMinPerDayQty)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtUOM)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCommissionUOM)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkMobileUser)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadButton1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtToDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtFromDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblToDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblFromDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDocNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDocCode)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(974, 492)
        Me.SplitContainer2.SplitterDistance = 143
        Me.SplitContainer2.TabIndex = 177
        '
        'txtperDayQty
        '
        Me.txtperDayQty.BackColor = System.Drawing.Color.White
        Me.txtperDayQty.CalculationExpression = Nothing
        Me.txtperDayQty.DecimalPlaces = 0
        Me.txtperDayQty.FieldCode = Nothing
        Me.txtperDayQty.FieldDesc = Nothing
        Me.txtperDayQty.FieldMaxLength = 5
        Me.txtperDayQty.FieldName = Nothing
        Me.txtperDayQty.isCalculatedField = False
        Me.txtperDayQty.IsSourceFromTable = False
        Me.txtperDayQty.IsSourceFromValueList = False
        Me.txtperDayQty.IsUnique = False
        Me.txtperDayQty.Location = New System.Drawing.Point(116, 59)
        Me.txtperDayQty.MendatroryField = False
        Me.txtperDayQty.MyLinkLable1 = Nothing
        Me.txtperDayQty.MyLinkLable2 = Nothing
        Me.txtperDayQty.Name = "txtperDayQty"
        Me.txtperDayQty.ReferenceFieldDesc = Nothing
        Me.txtperDayQty.ReferenceFieldName = Nothing
        Me.txtperDayQty.ReferenceTableName = Nothing
        Me.txtperDayQty.Size = New System.Drawing.Size(84, 20)
        Me.txtperDayQty.TabIndex = 1539
        Me.txtperDayQty.Text = "0"
        Me.txtperDayQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtperDayQty.Value = New Decimal(New Integer() {0, 0, 0, 0})
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
        Me.txtDate.Location = New System.Drawing.Point(429, 16)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(90, 18)
        Me.txtDate.TabIndex = 1538
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "04/07/2023"
        Me.txtDate.Value = New Date(2023, 7, 4, 0, 0, 0, 0)
        '
        'lblDate
        '
        Me.lblDate.FieldName = Nothing
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDate.Location = New System.Drawing.Point(388, 18)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(31, 16)
        Me.lblDate.TabIndex = 1537
        Me.lblDate.Text = "Date"
        '
        'chkInActive
        '
        Me.chkInActive.Location = New System.Drawing.Point(388, 81)
        Me.chkInActive.Name = "chkInActive"
        Me.chkInActive.Size = New System.Drawing.Size(60, 18)
        Me.chkInActive.TabIndex = 1536
        Me.chkInActive.Text = "InActive"
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
        Me.txtComment.Location = New System.Drawing.Point(117, 105)
        Me.txtComment.MaxLength = 200
        Me.txtComment.MendatroryField = False
        Me.txtComment.Multiline = True
        Me.txtComment.MyLinkLable1 = Nothing
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtComment.RootElement.StretchVertically = True
        Me.txtComment.Size = New System.Drawing.Size(254, 19)
        Me.txtComment.TabIndex = 1535
        '
        'lblComment
        '
        Me.lblComment.AutoSize = True
        Me.lblComment.Location = New System.Drawing.Point(10, 108)
        Me.lblComment.Name = "lblComment"
        Me.lblComment.Size = New System.Drawing.Size(56, 13)
        Me.lblComment.TabIndex = 1534
        Me.lblComment.Text = "Comment"
        '
        'txtRemark
        '
        Me.txtRemark.CalculationExpression = Nothing
        Me.txtRemark.FieldCode = Nothing
        Me.txtRemark.FieldDesc = Nothing
        Me.txtRemark.FieldMaxLength = 0
        Me.txtRemark.FieldName = Nothing
        Me.txtRemark.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemark.isCalculatedField = False
        Me.txtRemark.IsSourceFromTable = False
        Me.txtRemark.IsSourceFromValueList = False
        Me.txtRemark.IsUnique = False
        Me.txtRemark.Location = New System.Drawing.Point(117, 82)
        Me.txtRemark.MaxLength = 200
        Me.txtRemark.MendatroryField = False
        Me.txtRemark.Multiline = True
        Me.txtRemark.MyLinkLable1 = Nothing
        Me.txtRemark.MyLinkLable2 = Nothing
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ReferenceFieldDesc = Nothing
        Me.txtRemark.ReferenceFieldName = Nothing
        Me.txtRemark.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtRemark.RootElement.StretchVertically = True
        Me.txtRemark.Size = New System.Drawing.Size(254, 19)
        Me.txtRemark.TabIndex = 1533
        '
        'lblRemark
        '
        Me.lblRemark.AutoSize = True
        Me.lblRemark.Location = New System.Drawing.Point(10, 82)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(45, 13)
        Me.lblRemark.TabIndex = 1532
        Me.lblRemark.Text = "Remark"
        '
        'lblMinPerDayQty
        '
        Me.lblMinPerDayQty.FieldName = Nothing
        Me.lblMinPerDayQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMinPerDayQty.Location = New System.Drawing.Point(10, 59)
        Me.lblMinPerDayQty.Name = "lblMinPerDayQty"
        Me.lblMinPerDayQty.Size = New System.Drawing.Size(89, 16)
        Me.lblMinPerDayQty.TabIndex = 1530
        Me.lblMinPerDayQty.Text = "Min Per Day Qty"
        '
        'txtUOM
        '
        Me.txtUOM.CalculationExpression = Nothing
        Me.txtUOM.FieldCode = Nothing
        Me.txtUOM.FieldDesc = Nothing
        Me.txtUOM.FieldMaxLength = 0
        Me.txtUOM.FieldName = Nothing
        Me.txtUOM.isCalculatedField = False
        Me.txtUOM.IsSourceFromTable = False
        Me.txtUOM.IsSourceFromValueList = False
        Me.txtUOM.IsUnique = False
        Me.txtUOM.Location = New System.Drawing.Point(117, 36)
        Me.txtUOM.MendatroryField = False
        Me.txtUOM.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUOM.MyLinkLable1 = Nothing
        Me.txtUOM.MyLinkLable2 = Nothing
        Me.txtUOM.MyReadOnly = False
        Me.txtUOM.MyShowMasterFormButton = False
        Me.txtUOM.Name = "txtUOM"
        Me.txtUOM.ReferenceFieldDesc = Nothing
        Me.txtUOM.ReferenceFieldName = Nothing
        Me.txtUOM.ReferenceTableName = Nothing
        Me.txtUOM.Size = New System.Drawing.Size(248, 19)
        Me.txtUOM.TabIndex = 1529
        Me.txtUOM.Value = ""
        '
        'lblCommissionUOM
        '
        Me.lblCommissionUOM.FieldName = Nothing
        Me.lblCommissionUOM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCommissionUOM.Location = New System.Drawing.Point(10, 36)
        Me.lblCommissionUOM.Name = "lblCommissionUOM"
        Me.lblCommissionUOM.Size = New System.Drawing.Size(106, 16)
        Me.lblCommissionUOM.TabIndex = 1528
        Me.lblCommissionUOM.Text = "Commission (UOM)"
        '
        'chkMobileUser
        '
        Me.chkMobileUser.Location = New System.Drawing.Point(388, 40)
        Me.chkMobileUser.Name = "chkMobileUser"
        Me.chkMobileUser.Size = New System.Drawing.Size(126, 18)
        Me.chkMobileUser.TabIndex = 1527
        Me.chkMobileUser.Text = "Only For Mobile User"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(534, 14)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(92, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 186
        '
        'RadButton1
        '
        Me.RadButton1.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadButton1.Location = New System.Drawing.Point(361, 15)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(20, 21)
        Me.RadButton1.TabIndex = 184
        Me.RadButton1.Text = "CC"
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "MMM/yyyy"
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
        Me.txtToDate.Location = New System.Drawing.Point(441, 59)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.ShowCheckBox = True
        Me.txtToDate.Size = New System.Drawing.Size(92, 18)
        Me.txtToDate.TabIndex = 183
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "Jun/2011"
        Me.txtToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "MMM/yyyy"
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
        Me.txtFromDate.Location = New System.Drawing.Point(279, 59)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(90, 18)
        Me.txtFromDate.TabIndex = 180
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "Jun/2011"
        Me.txtFromDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Location = New System.Drawing.Point(385, 61)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(46, 13)
        Me.lblToDate.TabIndex = 182
        Me.lblToDate.Text = "To Date"
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Location = New System.Drawing.Point(218, 62)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(60, 13)
        Me.lblFromDate.TabIndex = 181
        Me.lblFromDate.Text = "From Date"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(343, 15)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 179
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(117, 15)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Nothing
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 30
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(228, 19)
        Me.txtDocNo.TabIndex = 178
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
        '
        'lblDocCode
        '
        Me.lblDocCode.FieldName = Nothing
        Me.lblDocCode.Location = New System.Drawing.Point(10, 15)
        Me.lblDocCode.Name = "lblDocCode"
        Me.lblDocCode.Size = New System.Drawing.Size(88, 18)
        Me.lblDocCode.TabIndex = 177
        Me.lblDocCode.Text = "Document Code"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv1.MyExportAPI = False
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(974, 345)
        Me.gv1.TabIndex = 5
        Me.gv1.TabStop = False
        Me.gv1.VarID = ""
        '
        'btnpost
        '
        Me.btnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnpost.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnpost.Location = New System.Drawing.Point(85, 7)
        Me.btnpost.Name = "btnpost"
        Me.btnpost.Size = New System.Drawing.Size(73, 20)
        Me.btnpost.TabIndex = 9
        Me.btnpost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(889, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 8
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(10, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 7
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(161, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 6
        Me.btndelete.Text = "Delete"
        '
        'FrmBoothCommissionMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(974, 530)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmBoothCommissionMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Booth Commission Master"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.txtperDayQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMinPerDayQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCommissionUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMobileUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As Label
    Friend WithEvents lblFromDate As Label
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents lblDocCode As common.Controls.MyLabel
    Friend WithEvents chkMobileUser As RadCheckBox
    Friend WithEvents txtUOM As common.UserControls.txtFinder
    Friend WithEvents lblCommissionUOM As common.Controls.MyLabel
    Friend WithEvents lblMinPerDayQty As common.Controls.MyLabel
    Friend WithEvents txtRemark As common.Controls.MyTextBox
    Friend WithEvents lblRemark As Label
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents lblComment As Label
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnpost As RadButton
    Friend WithEvents btnclose As RadButton
    Friend WithEvents btnsave As RadButton
    Friend WithEvents btndelete As RadButton
    Friend WithEvents chkInActive As RadCheckBox
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents txtperDayQty As common.MyNumBox
End Class
