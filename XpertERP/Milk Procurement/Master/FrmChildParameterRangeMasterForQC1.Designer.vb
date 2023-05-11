<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmChildParameterRangeMasterForQC1
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.txtDeductionRatio = New common.MyNumBox()
        Me.txtDeductionURange = New common.MyNumBox()
        Me.txtDeductionLRange = New common.MyNumBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtvalue = New Telerik.WinControls.UI.RadTextBox()
        Me.txtDeductionPer = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.cboStatus = New Telerik.WinControls.UI.RadMultiColumnComboBox()
        Me.cboQcStatus = New common.Controls.MyComboBox()
        Me.lblstatus = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.Status = New common.Controls.MyLabel()
        Me.txtUrange = New common.MyNumBox()
        Me.txtlRange = New common.MyNumBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.lblParamDesc = New common.Controls.MyLabel()
        Me.FndParameterCode = New common.UserControls.txtFinder()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.txtDeductionRatio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeductionURange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeductionLRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtvalue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeductionPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboStatus.EditorControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboStatus.EditorControl.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboQcStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblstatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Status, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUrange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtlRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblParamDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.SplitContainer1.Size = New System.Drawing.Size(662, 467)
        Me.SplitContainer1.SplitterDistance = 25
        Me.SplitContainer1.TabIndex = 0
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(662, 20)
        Me.RadMenu1.TabIndex = 0
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Settings"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "RadMenuItem2"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDeductionRatio)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDeductionURange)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDeductionLRange)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtvalue)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDeductionPer)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboStatus)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboQcStatus)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblstatus)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Status)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtUrange)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtlRange)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblParamDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.FndParameterCode)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer2.Size = New System.Drawing.Size(662, 438)
        Me.SplitContainer2.SplitterDistance = 404
        Me.SplitContainer2.TabIndex = 0
        '
        'txtDeductionRatio
        '
        Me.txtDeductionRatio.BackColor = System.Drawing.Color.White
        Me.txtDeductionRatio.CalculationExpression = Nothing
        Me.txtDeductionRatio.DecimalPlaces = 2
        Me.txtDeductionRatio.Enabled = False
        Me.txtDeductionRatio.FieldCode = Nothing
        Me.txtDeductionRatio.FieldDesc = Nothing
        Me.txtDeductionRatio.FieldMaxLength = 0
        Me.txtDeductionRatio.FieldName = Nothing
        Me.txtDeductionRatio.isCalculatedField = False
        Me.txtDeductionRatio.IsSourceFromTable = False
        Me.txtDeductionRatio.IsSourceFromValueList = False
        Me.txtDeductionRatio.IsUnique = False
        Me.txtDeductionRatio.Location = New System.Drawing.Point(119, 214)
        Me.txtDeductionRatio.MaxLength = 8
        Me.txtDeductionRatio.MendatroryField = False
        Me.txtDeductionRatio.MyLinkLable1 = Nothing
        Me.txtDeductionRatio.MyLinkLable2 = Nothing
        Me.txtDeductionRatio.Name = "txtDeductionRatio"
        Me.txtDeductionRatio.ReferenceFieldDesc = Nothing
        Me.txtDeductionRatio.ReferenceFieldName = Nothing
        Me.txtDeductionRatio.ReferenceTableName = Nothing
        Me.txtDeductionRatio.Size = New System.Drawing.Size(253, 20)
        Me.txtDeductionRatio.TabIndex = 331
        Me.txtDeductionRatio.Text = "0"
        Me.txtDeductionRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDeductionRatio.Value = 0R
        Me.txtDeductionRatio.Visible = False
        '
        'txtDeductionURange
        '
        Me.txtDeductionURange.BackColor = System.Drawing.Color.White
        Me.txtDeductionURange.CalculationExpression = Nothing
        Me.txtDeductionURange.DecimalPlaces = 2
        Me.txtDeductionURange.Enabled = False
        Me.txtDeductionURange.FieldCode = Nothing
        Me.txtDeductionURange.FieldDesc = Nothing
        Me.txtDeductionURange.FieldMaxLength = 0
        Me.txtDeductionURange.FieldName = Nothing
        Me.txtDeductionURange.isCalculatedField = False
        Me.txtDeductionURange.IsSourceFromTable = False
        Me.txtDeductionURange.IsSourceFromValueList = False
        Me.txtDeductionURange.IsUnique = False
        Me.txtDeductionURange.Location = New System.Drawing.Point(119, 192)
        Me.txtDeductionURange.MaxLength = 8
        Me.txtDeductionURange.MendatroryField = False
        Me.txtDeductionURange.MyLinkLable1 = Nothing
        Me.txtDeductionURange.MyLinkLable2 = Nothing
        Me.txtDeductionURange.Name = "txtDeductionURange"
        Me.txtDeductionURange.ReferenceFieldDesc = Nothing
        Me.txtDeductionURange.ReferenceFieldName = Nothing
        Me.txtDeductionURange.ReferenceTableName = Nothing
        Me.txtDeductionURange.Size = New System.Drawing.Size(253, 20)
        Me.txtDeductionURange.TabIndex = 330
        Me.txtDeductionURange.Text = "0"
        Me.txtDeductionURange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDeductionURange.Value = 0R
        Me.txtDeductionURange.Visible = False
        '
        'txtDeductionLRange
        '
        Me.txtDeductionLRange.BackColor = System.Drawing.Color.White
        Me.txtDeductionLRange.CalculationExpression = Nothing
        Me.txtDeductionLRange.DecimalPlaces = 2
        Me.txtDeductionLRange.Enabled = False
        Me.txtDeductionLRange.FieldCode = Nothing
        Me.txtDeductionLRange.FieldDesc = Nothing
        Me.txtDeductionLRange.FieldMaxLength = 0
        Me.txtDeductionLRange.FieldName = Nothing
        Me.txtDeductionLRange.isCalculatedField = False
        Me.txtDeductionLRange.IsSourceFromTable = False
        Me.txtDeductionLRange.IsSourceFromValueList = False
        Me.txtDeductionLRange.IsUnique = False
        Me.txtDeductionLRange.Location = New System.Drawing.Point(119, 170)
        Me.txtDeductionLRange.MaxLength = 8
        Me.txtDeductionLRange.MendatroryField = False
        Me.txtDeductionLRange.MyLinkLable1 = Nothing
        Me.txtDeductionLRange.MyLinkLable2 = Nothing
        Me.txtDeductionLRange.Name = "txtDeductionLRange"
        Me.txtDeductionLRange.ReferenceFieldDesc = Nothing
        Me.txtDeductionLRange.ReferenceFieldName = Nothing
        Me.txtDeductionLRange.ReferenceTableName = Nothing
        Me.txtDeductionLRange.Size = New System.Drawing.Size(253, 20)
        Me.txtDeductionLRange.TabIndex = 329
        Me.txtDeductionLRange.Text = "0"
        Me.txtDeductionLRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDeductionLRange.Value = 0R
        Me.txtDeductionLRange.Visible = False
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(10, 214)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel7.TabIndex = 328
        Me.MyLabel7.Text = "Deduction Ratio"
        Me.MyLabel7.Visible = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(10, 192)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(105, 16)
        Me.MyLabel3.TabIndex = 327
        Me.MyLabel3.Text = "Deduction U Range"
        Me.MyLabel3.Visible = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(10, 170)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(103, 16)
        Me.MyLabel2.TabIndex = 326
        Me.MyLabel2.Text = "Deduction L Range"
        Me.MyLabel2.Visible = False
        '
        'txtvalue
        '
        Me.txtvalue.Location = New System.Drawing.Point(119, 79)
        Me.txtvalue.Name = "txtvalue"
        Me.txtvalue.Size = New System.Drawing.Size(252, 20)
        Me.txtvalue.TabIndex = 325
        '
        'txtDeductionPer
        '
        Me.txtDeductionPer.BackColor = System.Drawing.Color.White
        Me.txtDeductionPer.CalculationExpression = Nothing
        Me.txtDeductionPer.DecimalPlaces = 2
        Me.txtDeductionPer.FieldCode = Nothing
        Me.txtDeductionPer.FieldDesc = Nothing
        Me.txtDeductionPer.FieldMaxLength = 0
        Me.txtDeductionPer.FieldName = Nothing
        Me.txtDeductionPer.isCalculatedField = False
        Me.txtDeductionPer.IsSourceFromTable = False
        Me.txtDeductionPer.IsSourceFromValueList = False
        Me.txtDeductionPer.IsUnique = False
        Me.txtDeductionPer.Location = New System.Drawing.Point(119, 148)
        Me.txtDeductionPer.MaxLength = 8
        Me.txtDeductionPer.MendatroryField = False
        Me.txtDeductionPer.MyLinkLable1 = Nothing
        Me.txtDeductionPer.MyLinkLable2 = Nothing
        Me.txtDeductionPer.Name = "txtDeductionPer"
        Me.txtDeductionPer.ReferenceFieldDesc = Nothing
        Me.txtDeductionPer.ReferenceFieldName = Nothing
        Me.txtDeductionPer.ReferenceTableName = Nothing
        Me.txtDeductionPer.Size = New System.Drawing.Size(253, 20)
        Me.txtDeductionPer.TabIndex = 324
        Me.txtDeductionPer.Text = "0"
        Me.txtDeductionPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDeductionPer.Value = 0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(10, 148)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel1.TabIndex = 323
        Me.MyLabel1.Text = "Deduction %"
        '
        'cboStatus
        '
        '
        'cboStatus.NestedRadGridView
        '
        Me.cboStatus.EditorControl.BackColor = System.Drawing.SystemColors.Window
        Me.cboStatus.EditorControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatus.EditorControl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboStatus.EditorControl.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.cboStatus.EditorControl.MasterTemplate.AllowAddNewRow = False
        Me.cboStatus.EditorControl.MasterTemplate.AllowCellContextMenu = False
        Me.cboStatus.EditorControl.MasterTemplate.AllowColumnChooser = False
        Me.cboStatus.EditorControl.MasterTemplate.EnableGrouping = False
        Me.cboStatus.EditorControl.MasterTemplate.ShowFilteringRow = False
        Me.cboStatus.EditorControl.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.cboStatus.EditorControl.Name = "NestedRadGridView"
        Me.cboStatus.EditorControl.ReadOnly = True
        Me.cboStatus.EditorControl.ShowGroupPanel = False
        Me.cboStatus.EditorControl.Size = New System.Drawing.Size(240, 150)
        Me.cboStatus.EditorControl.TabIndex = 0
        Me.cboStatus.Location = New System.Drawing.Point(119, 101)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(253, 20)
        Me.cboStatus.TabIndex = 322
        Me.cboStatus.TabStop = False
        '
        'cboQcStatus
        '
        Me.cboQcStatus.AutoCompleteDisplayMember = Nothing
        Me.cboQcStatus.AutoCompleteValueMember = Nothing
        Me.cboQcStatus.CalculationExpression = Nothing
        Me.cboQcStatus.DropDownAnimationEnabled = True
        Me.cboQcStatus.FieldCode = Nothing
        Me.cboQcStatus.FieldDesc = Nothing
        Me.cboQcStatus.FieldMaxLength = 0
        Me.cboQcStatus.FieldName = Nothing
        Me.cboQcStatus.isCalculatedField = False
        Me.cboQcStatus.IsSourceFromTable = False
        Me.cboQcStatus.IsSourceFromValueList = False
        Me.cboQcStatus.IsUnique = False
        Me.cboQcStatus.Location = New System.Drawing.Point(119, 124)
        Me.cboQcStatus.MendatroryField = True
        Me.cboQcStatus.MyLinkLable1 = Me.lblstatus
        Me.cboQcStatus.MyLinkLable2 = Nothing
        Me.cboQcStatus.Name = "cboQcStatus"
        Me.cboQcStatus.ReferenceFieldDesc = Nothing
        Me.cboQcStatus.ReferenceFieldName = Nothing
        Me.cboQcStatus.ReferenceTableName = Nothing
        Me.cboQcStatus.Size = New System.Drawing.Size(253, 20)
        Me.cboQcStatus.TabIndex = 320
        '
        'lblstatus
        '
        Me.lblstatus.FieldName = Nothing
        Me.lblstatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstatus.Location = New System.Drawing.Point(11, 126)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(58, 16)
        Me.lblstatus.TabIndex = 321
        Me.lblstatus.Text = "QC Status"
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
        Me.txtDate.Location = New System.Drawing.Point(493, 36)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(126, 18)
        Me.txtDate.TabIndex = 318
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(382, 37)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(77, 16)
        Me.RadLabel4.TabIndex = 319
        Me.RadLabel4.Text = "Effective Date"
        '
        'Status
        '
        Me.Status.FieldName = Nothing
        Me.Status.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Status.Location = New System.Drawing.Point(11, 104)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(38, 16)
        Me.Status.TabIndex = 317
        Me.Status.Text = "Status"
        '
        'txtUrange
        '
        Me.txtUrange.BackColor = System.Drawing.Color.White
        Me.txtUrange.CalculationExpression = Nothing
        Me.txtUrange.DecimalPlaces = 2
        Me.txtUrange.FieldCode = Nothing
        Me.txtUrange.FieldDesc = Nothing
        Me.txtUrange.FieldMaxLength = 0
        Me.txtUrange.FieldName = Nothing
        Me.txtUrange.isCalculatedField = False
        Me.txtUrange.IsSourceFromTable = False
        Me.txtUrange.IsSourceFromValueList = False
        Me.txtUrange.IsUnique = False
        Me.txtUrange.Location = New System.Drawing.Point(119, 57)
        Me.txtUrange.MaxLength = 8
        Me.txtUrange.MendatroryField = False
        Me.txtUrange.MyLinkLable1 = Nothing
        Me.txtUrange.MyLinkLable2 = Nothing
        Me.txtUrange.Name = "txtUrange"
        Me.txtUrange.ReferenceFieldDesc = Nothing
        Me.txtUrange.ReferenceFieldName = Nothing
        Me.txtUrange.ReferenceTableName = Nothing
        Me.txtUrange.Size = New System.Drawing.Size(253, 20)
        Me.txtUrange.TabIndex = 314
        Me.txtUrange.Text = "0"
        Me.txtUrange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtUrange.Value = 0R
        '
        'txtlRange
        '
        Me.txtlRange.BackColor = System.Drawing.Color.White
        Me.txtlRange.CalculationExpression = Nothing
        Me.txtlRange.DecimalPlaces = 2
        Me.txtlRange.FieldCode = Nothing
        Me.txtlRange.FieldDesc = Nothing
        Me.txtlRange.FieldMaxLength = 0
        Me.txtlRange.FieldName = Nothing
        Me.txtlRange.isCalculatedField = False
        Me.txtlRange.IsSourceFromTable = False
        Me.txtlRange.IsSourceFromValueList = False
        Me.txtlRange.IsUnique = False
        Me.txtlRange.Location = New System.Drawing.Point(119, 34)
        Me.txtlRange.MaxLength = 8
        Me.txtlRange.MendatroryField = False
        Me.txtlRange.MyLinkLable1 = Nothing
        Me.txtlRange.MyLinkLable2 = Nothing
        Me.txtlRange.Name = "txtlRange"
        Me.txtlRange.ReferenceFieldDesc = Nothing
        Me.txtlRange.ReferenceFieldName = Nothing
        Me.txtlRange.ReferenceTableName = Nothing
        Me.txtlRange.Size = New System.Drawing.Size(253, 20)
        Me.txtlRange.TabIndex = 313
        Me.txtlRange.Text = "0"
        Me.txtlRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtlRange.Value = 0R
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(10, 79)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(35, 16)
        Me.MyLabel6.TabIndex = 312
        Me.MyLabel6.Text = "Value"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(10, 57)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel5.TabIndex = 311
        Me.MyLabel5.Text = "Upper Range"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(10, 34)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel4.TabIndex = 310
        Me.MyLabel4.Text = "Lower Range"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(10, 12)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel8.TabIndex = 175
        Me.MyLabel8.Text = "Code"
        '
        'lblParamDesc
        '
        Me.lblParamDesc.AutoSize = False
        Me.lblParamDesc.BorderVisible = True
        Me.lblParamDesc.FieldName = Nothing
        Me.lblParamDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblParamDesc.Location = New System.Drawing.Point(384, 11)
        Me.lblParamDesc.Name = "lblParamDesc"
        Me.lblParamDesc.Size = New System.Drawing.Size(251, 18)
        Me.lblParamDesc.TabIndex = 174
        Me.lblParamDesc.TextWrap = False
        '
        'FndParameterCode
        '
        Me.FndParameterCode.CalculationExpression = Nothing
        Me.FndParameterCode.FieldCode = Nothing
        Me.FndParameterCode.FieldDesc = Nothing
        Me.FndParameterCode.FieldMaxLength = 0
        Me.FndParameterCode.FieldName = Nothing
        Me.FndParameterCode.isCalculatedField = False
        Me.FndParameterCode.IsSourceFromTable = False
        Me.FndParameterCode.IsSourceFromValueList = False
        Me.FndParameterCode.IsUnique = False
        Me.FndParameterCode.Location = New System.Drawing.Point(119, 11)
        Me.FndParameterCode.MendatroryField = True
        Me.FndParameterCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndParameterCode.MyLinkLable1 = Nothing
        Me.FndParameterCode.MyLinkLable2 = Nothing
        Me.FndParameterCode.MyReadOnly = False
        Me.FndParameterCode.MyShowMasterFormButton = False
        Me.FndParameterCode.Name = "FndParameterCode"
        Me.FndParameterCode.ReferenceFieldDesc = Nothing
        Me.FndParameterCode.ReferenceFieldName = Nothing
        Me.FndParameterCode.ReferenceTableName = Nothing
        Me.FndParameterCode.Size = New System.Drawing.Size(252, 20)
        Me.FndParameterCode.TabIndex = 173
        Me.FndParameterCode.Value = ""
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(155, 6)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 9
        Me.btnDelete.Text = "Delete"
        '
        'btnreset
        '
        Me.btnreset.Location = New System.Drawing.Point(81, 6)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 18)
        Me.btnreset.TabIndex = 8
        Me.btnreset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(591, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(7, 443)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save"
        '
        'FrmChildParameterRangeMasterForQC1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(662, 467)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmChildParameterRangeMasterForQC1"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Child Parameter Range Master For QC"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.txtDeductionRatio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeductionURange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeductionLRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtvalue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeductionPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboStatus.EditorControl.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboStatus.EditorControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboQcStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblstatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Status, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUrange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtlRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblParamDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents lblParamDesc As common.Controls.MyLabel
    Friend WithEvents FndParameterCode As common.UserControls.txtFinder
    Friend WithEvents txtUrange As common.MyNumBox
    Friend WithEvents txtlRange As common.MyNumBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents Status As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents cboQcStatus As common.Controls.MyComboBox
    Friend WithEvents lblstatus As common.Controls.MyLabel
    Friend WithEvents cboStatus As Telerik.WinControls.UI.RadMultiColumnComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtvalue As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtDeductionPer As common.MyNumBox
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtDeductionRatio As common.MyNumBox
    Friend WithEvents txtDeductionURange As common.MyNumBox
    Friend WithEvents txtDeductionLRange As common.MyNumBox
End Class

