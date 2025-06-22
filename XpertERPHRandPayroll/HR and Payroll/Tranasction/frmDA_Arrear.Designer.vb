Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDA_Arrear
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
        Me.txtEmpCode = New common.UserControls.txtMultiSelectFinder()
        Me.chkApplyLeaveIncashment = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblPayPeriod = New common.Controls.MyLabel()
        Me.txtPayPeriod = New common.UserControls.txtFinder()
        Me.lblIsuePeriod = New common.Controls.MyLabel()
        Me.txtPeriodTo = New common.Controls.MyDateTimePicker()
        Me.lblArrearDate = New common.Controls.MyLabel()
        Me.lblPeriodTo = New common.Controls.MyLabel()
        Me.txtPeriodFrom = New common.Controls.MyDateTimePicker()
        Me.lblperiodFrom = New common.Controls.MyLabel()
        Me.txtDAper = New common.Controls.MyTextBox()
        Me.lblDAper = New common.Controls.MyLabel()
        Me.lblempCode = New common.Controls.MyLabel()
        Me.txtArrearDate = New common.Controls.MyDateTimePicker()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.txtLocationCode = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblDate = New common.Controls.MyLabel()
        Me.lblCode = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.chkApplyLeaveIncashment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIsuePeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPeriodTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblArrearDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPeriodTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPeriodFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblperiodFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDAper, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDAper, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblempCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtArrearDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEmpCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkApplyLeaveIncashment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPayPeriod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblIsuePeriod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPeriodTo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPeriodTo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPeriodFrom)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblperiodFrom)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDAper)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDAper)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblempCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtArrearDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblArrearDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocationCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDocNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 411
        Me.SplitContainer1.TabIndex = 0
        '
        'txtEmpCode
        '
        Me.txtEmpCode.arrDispalyMember = Nothing
        Me.txtEmpCode.arrValueMember = Nothing
        Me.txtEmpCode.Location = New System.Drawing.Point(80, 59)
        Me.txtEmpCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.MyLinkLable1 = Nothing
        Me.txtEmpCode.MyLinkLable2 = Nothing
        Me.txtEmpCode.MyNullText = "All"
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.Size = New System.Drawing.Size(291, 20)
        Me.txtEmpCode.TabIndex = 1600
        '
        'chkApplyLeaveIncashment
        '
        Me.chkApplyLeaveIncashment.Location = New System.Drawing.Point(375, 84)
        Me.chkApplyLeaveIncashment.Name = "chkApplyLeaveIncashment"
        Me.chkApplyLeaveIncashment.Size = New System.Drawing.Size(141, 18)
        Me.chkApplyLeaveIncashment.TabIndex = 1599
        Me.chkApplyLeaveIncashment.Text = "Apply Leave Incashment"
        '
        'lblPayPeriod
        '
        Me.lblPayPeriod.AutoSize = False
        Me.lblPayPeriod.BorderVisible = True
        Me.lblPayPeriod.FieldName = Nothing
        Me.lblPayPeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayPeriod.Location = New System.Drawing.Point(206, 103)
        Me.lblPayPeriod.Name = "lblPayPeriod"
        Me.lblPayPeriod.Size = New System.Drawing.Size(166, 18)
        Me.lblPayPeriod.TabIndex = 1598
        Me.lblPayPeriod.TextWrap = False
        '
        'txtPayPeriod
        '
        Me.txtPayPeriod.CalculationExpression = Nothing
        Me.txtPayPeriod.FieldCode = Nothing
        Me.txtPayPeriod.FieldDesc = Nothing
        Me.txtPayPeriod.FieldMaxLength = 0
        Me.txtPayPeriod.FieldName = Nothing
        Me.txtPayPeriod.isCalculatedField = False
        Me.txtPayPeriod.IsSourceFromTable = False
        Me.txtPayPeriod.IsSourceFromValueList = False
        Me.txtPayPeriod.IsUnique = False
        Me.txtPayPeriod.Location = New System.Drawing.Point(82, 102)
        Me.txtPayPeriod.MendatroryField = True
        Me.txtPayPeriod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayPeriod.MyLinkLable1 = Nothing
        Me.txtPayPeriod.MyLinkLable2 = Nothing
        Me.txtPayPeriod.MyReadOnly = False
        Me.txtPayPeriod.MyShowMasterFormButton = False
        Me.txtPayPeriod.Name = "txtPayPeriod"
        Me.txtPayPeriod.ReferenceFieldDesc = Nothing
        Me.txtPayPeriod.ReferenceFieldName = Nothing
        Me.txtPayPeriod.ReferenceTableName = Nothing
        Me.txtPayPeriod.Size = New System.Drawing.Size(119, 19)
        Me.txtPayPeriod.TabIndex = 1597
        Me.txtPayPeriod.Value = ""
        '
        'lblIsuePeriod
        '
        Me.lblIsuePeriod.FieldName = Nothing
        Me.lblIsuePeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIsuePeriod.Location = New System.Drawing.Point(7, 105)
        Me.lblIsuePeriod.Name = "lblIsuePeriod"
        Me.lblIsuePeriod.Size = New System.Drawing.Size(62, 16)
        Me.lblIsuePeriod.TabIndex = 1596
        Me.lblIsuePeriod.Text = "Pay Period"
        '
        'txtPeriodTo
        '
        Me.txtPeriodTo.CalculationExpression = Nothing
        Me.txtPeriodTo.CustomFormat = "dd/MM/yyyy"
        Me.txtPeriodTo.FieldCode = Nothing
        Me.txtPeriodTo.FieldDesc = Nothing
        Me.txtPeriodTo.FieldMaxLength = 0
        Me.txtPeriodTo.FieldName = Nothing
        Me.txtPeriodTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPeriodTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtPeriodTo.isCalculatedField = False
        Me.txtPeriodTo.IsSourceFromTable = False
        Me.txtPeriodTo.IsSourceFromValueList = False
        Me.txtPeriodTo.IsUnique = False
        Me.txtPeriodTo.Location = New System.Drawing.Point(207, 82)
        Me.txtPeriodTo.MendatroryField = False
        Me.txtPeriodTo.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPeriodTo.MyLinkLable1 = Me.lblArrearDate
        Me.txtPeriodTo.MyLinkLable2 = Nothing
        Me.txtPeriodTo.Name = "txtPeriodTo"
        Me.txtPeriodTo.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPeriodTo.ReferenceFieldDesc = Nothing
        Me.txtPeriodTo.ReferenceFieldName = Nothing
        Me.txtPeriodTo.ReferenceTableName = Nothing
        Me.txtPeriodTo.Size = New System.Drawing.Size(81, 18)
        Me.txtPeriodTo.TabIndex = 1595
        Me.txtPeriodTo.TabStop = False
        Me.txtPeriodTo.Text = "13/06/2011"
        Me.txtPeriodTo.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblArrearDate
        '
        Me.lblArrearDate.FieldName = Nothing
        Me.lblArrearDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArrearDate.Location = New System.Drawing.Point(375, 38)
        Me.lblArrearDate.Name = "lblArrearDate"
        Me.lblArrearDate.Size = New System.Drawing.Size(65, 16)
        Me.lblArrearDate.TabIndex = 1585
        Me.lblArrearDate.Text = "Arrear Date"
        '
        'lblPeriodTo
        '
        Me.lblPeriodTo.FieldName = Nothing
        Me.lblPeriodTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPeriodTo.Location = New System.Drawing.Point(168, 84)
        Me.lblPeriodTo.Name = "lblPeriodTo"
        Me.lblPeriodTo.Size = New System.Drawing.Size(19, 16)
        Me.lblPeriodTo.TabIndex = 1594
        Me.lblPeriodTo.Text = "To"
        '
        'txtPeriodFrom
        '
        Me.txtPeriodFrom.CalculationExpression = Nothing
        Me.txtPeriodFrom.CustomFormat = "dd/MM/yyyy"
        Me.txtPeriodFrom.FieldCode = Nothing
        Me.txtPeriodFrom.FieldDesc = Nothing
        Me.txtPeriodFrom.FieldMaxLength = 0
        Me.txtPeriodFrom.FieldName = Nothing
        Me.txtPeriodFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPeriodFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtPeriodFrom.isCalculatedField = False
        Me.txtPeriodFrom.IsSourceFromTable = False
        Me.txtPeriodFrom.IsSourceFromValueList = False
        Me.txtPeriodFrom.IsUnique = False
        Me.txtPeriodFrom.Location = New System.Drawing.Point(81, 81)
        Me.txtPeriodFrom.MendatroryField = False
        Me.txtPeriodFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPeriodFrom.MyLinkLable1 = Me.lblArrearDate
        Me.txtPeriodFrom.MyLinkLable2 = Nothing
        Me.txtPeriodFrom.Name = "txtPeriodFrom"
        Me.txtPeriodFrom.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPeriodFrom.ReferenceFieldDesc = Nothing
        Me.txtPeriodFrom.ReferenceFieldName = Nothing
        Me.txtPeriodFrom.ReferenceTableName = Nothing
        Me.txtPeriodFrom.Size = New System.Drawing.Size(81, 18)
        Me.txtPeriodFrom.TabIndex = 1593
        Me.txtPeriodFrom.TabStop = False
        Me.txtPeriodFrom.Text = "13/06/2011"
        Me.txtPeriodFrom.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblperiodFrom
        '
        Me.lblperiodFrom.FieldName = Nothing
        Me.lblperiodFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblperiodFrom.Location = New System.Drawing.Point(6, 83)
        Me.lblperiodFrom.Name = "lblperiodFrom"
        Me.lblperiodFrom.Size = New System.Drawing.Size(68, 16)
        Me.lblperiodFrom.TabIndex = 1592
        Me.lblperiodFrom.Text = "Period From"
        '
        'txtDAper
        '
        Me.txtDAper.CalculationExpression = Nothing
        Me.txtDAper.FieldCode = Nothing
        Me.txtDAper.FieldDesc = Nothing
        Me.txtDAper.FieldMaxLength = 0
        Me.txtDAper.FieldName = Nothing
        Me.txtDAper.isCalculatedField = False
        Me.txtDAper.IsSourceFromTable = False
        Me.txtDAper.IsSourceFromValueList = False
        Me.txtDAper.IsUnique = False
        Me.txtDAper.Location = New System.Drawing.Point(446, 60)
        Me.txtDAper.MendatroryField = False
        Me.txtDAper.MyLinkLable1 = Nothing
        Me.txtDAper.MyLinkLable2 = Nothing
        Me.txtDAper.Name = "txtDAper"
        Me.txtDAper.ReferenceFieldDesc = Nothing
        Me.txtDAper.ReferenceFieldName = Nothing
        Me.txtDAper.ReferenceTableName = Nothing
        Me.txtDAper.Size = New System.Drawing.Size(123, 20)
        Me.txtDAper.TabIndex = 1591
        Me.txtDAper.Text = "0"
        Me.txtDAper.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblDAper
        '
        Me.lblDAper.FieldName = Nothing
        Me.lblDAper.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDAper.Location = New System.Drawing.Point(375, 59)
        Me.lblDAper.Name = "lblDAper"
        Me.lblDAper.Size = New System.Drawing.Size(35, 16)
        Me.lblDAper.TabIndex = 1590
        Me.lblDAper.Text = "DA %"
        '
        'lblempCode
        '
        Me.lblempCode.FieldName = Nothing
        Me.lblempCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblempCode.Location = New System.Drawing.Point(6, 61)
        Me.lblempCode.Name = "lblempCode"
        Me.lblempCode.Size = New System.Drawing.Size(57, 16)
        Me.lblempCode.TabIndex = 1587
        Me.lblempCode.Text = "Employee"
        '
        'txtArrearDate
        '
        Me.txtArrearDate.CalculationExpression = Nothing
        Me.txtArrearDate.CustomFormat = "dd/MM/yyyy"
        Me.txtArrearDate.FieldCode = Nothing
        Me.txtArrearDate.FieldDesc = Nothing
        Me.txtArrearDate.FieldMaxLength = 0
        Me.txtArrearDate.FieldName = Nothing
        Me.txtArrearDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtArrearDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtArrearDate.isCalculatedField = False
        Me.txtArrearDate.IsSourceFromTable = False
        Me.txtArrearDate.IsSourceFromValueList = False
        Me.txtArrearDate.IsUnique = False
        Me.txtArrearDate.Location = New System.Drawing.Point(446, 36)
        Me.txtArrearDate.MendatroryField = False
        Me.txtArrearDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtArrearDate.MyLinkLable1 = Me.lblArrearDate
        Me.txtArrearDate.MyLinkLable2 = Nothing
        Me.txtArrearDate.Name = "txtArrearDate"
        Me.txtArrearDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtArrearDate.ReferenceFieldDesc = Nothing
        Me.txtArrearDate.ReferenceFieldName = Nothing
        Me.txtArrearDate.ReferenceTableName = Nothing
        Me.txtArrearDate.Size = New System.Drawing.Size(123, 18)
        Me.txtArrearDate.TabIndex = 1586
        Me.txtArrearDate.TabStop = False
        Me.txtArrearDate.Text = "13/06/2011"
        Me.txtArrearDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.FieldName = Nothing
        Me.lblLocationDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationDesc.Location = New System.Drawing.Point(205, 37)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(166, 18)
        Me.lblLocationDesc.TabIndex = 1584
        Me.lblLocationDesc.TextWrap = False
        '
        'txtLocationCode
        '
        Me.txtLocationCode.CalculationExpression = Nothing
        Me.txtLocationCode.FieldCode = Nothing
        Me.txtLocationCode.FieldDesc = Nothing
        Me.txtLocationCode.FieldMaxLength = 0
        Me.txtLocationCode.FieldName = Nothing
        Me.txtLocationCode.isCalculatedField = False
        Me.txtLocationCode.IsSourceFromTable = False
        Me.txtLocationCode.IsSourceFromValueList = False
        Me.txtLocationCode.IsUnique = False
        Me.txtLocationCode.Location = New System.Drawing.Point(81, 36)
        Me.txtLocationCode.MendatroryField = True
        Me.txtLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationCode.MyLinkLable1 = Nothing
        Me.txtLocationCode.MyLinkLable2 = Nothing
        Me.txtLocationCode.MyReadOnly = False
        Me.txtLocationCode.MyShowMasterFormButton = False
        Me.txtLocationCode.Name = "txtLocationCode"
        Me.txtLocationCode.ReferenceFieldDesc = Nothing
        Me.txtLocationCode.ReferenceFieldName = Nothing
        Me.txtLocationCode.ReferenceTableName = Nothing
        Me.txtLocationCode.Size = New System.Drawing.Size(119, 19)
        Me.txtLocationCode.TabIndex = 1583
        Me.txtLocationCode.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(6, 39)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 1579
        Me.lblLocation.Text = "Location"
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
        Me.txtDate.Location = New System.Drawing.Point(446, 10)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(123, 18)
        Me.txtDate.TabIndex = 1529
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblDate
        '
        Me.lblDate.FieldName = Nothing
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(375, 12)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDate.TabIndex = 1526
        Me.lblDate.Text = "Date"
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(7, 13)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(33, 16)
        Me.lblCode.TabIndex = 1528
        Me.lblCode.Text = "Code"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(81, 10)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.lblCode
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(269, 21)
        Me.txtDocNo.TabIndex = 1525
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(351, 10)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1527
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(730, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(67, 22)
        Me.btnClose.TabIndex = 22
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(80, 7)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(67, 22)
        Me.btnDelete.TabIndex = 21
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(13, 7)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(61, 22)
        Me.btnSave.TabIndex = 19
        Me.btnSave.Text = "Save"
        '
        'frmDA_Arrear
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmDA_Arrear"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "DA Arrear"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.chkApplyLeaveIncashment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIsuePeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPeriodTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblArrearDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPeriodTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPeriodFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblperiodFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDAper, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDAper, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblempCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtArrearDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocationCode As common.UserControls.txtFinder
    Friend WithEvents lblDAper As common.Controls.MyLabel
    Friend WithEvents lblempCode As common.Controls.MyLabel
    Friend WithEvents txtArrearDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblArrearDate As common.Controls.MyLabel
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents lblPayPeriod As common.Controls.MyLabel
    Friend WithEvents txtPayPeriod As common.UserControls.txtFinder
    Friend WithEvents lblIsuePeriod As common.Controls.MyLabel
    Friend WithEvents txtPeriodTo As common.Controls.MyDateTimePicker
    Friend WithEvents lblPeriodTo As common.Controls.MyLabel
    Friend WithEvents txtPeriodFrom As common.Controls.MyDateTimePicker
    Friend WithEvents lblperiodFrom As common.Controls.MyLabel
    Friend WithEvents txtDAper As common.Controls.MyTextBox
    Friend WithEvents chkApplyLeaveIncashment As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtEmpCode As common.UserControls.txtMultiSelectFinder
End Class
