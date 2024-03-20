Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoanAdjustment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLoanAdjustment))
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.UsLock1 = New common.usLock()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.lblAdjustedByName = New common.Controls.MyLabel()
        Me.lblEmpName = New common.Controls.MyLabel()
        Me.lblEmpCode = New common.Controls.MyLabel()
        Me.txtEmpCode = New common.UserControls.txtFinder()
        Me.lblLoanName = New common.Controls.MyLabel()
        Me.lblPayPeriodName = New common.Controls.MyLabel()
        Me.findPayperiod = New common.UserControls.txtFinder()
        Me.lblPayPeriodCode = New common.Controls.MyLabel()
        Me.lblAdjustmentMinus = New common.Controls.MyLabel()
        Me.lblAdjustmentPlus = New common.Controls.MyLabel()
        Me.txtAdjustMinus = New common.Controls.MyTextBox()
        Me.txtAdjustPlus = New common.Controls.MyTextBox()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblReason = New common.Controls.MyLabel()
        Me.lblAdjustmentBy = New common.Controls.MyLabel()
        Me.findLoanAdjustby = New common.UserControls.txtFinder()
        Me.lblAdjustmentDate = New common.Controls.MyLabel()
        Me.dtpLoanAdjustDate = New common.Controls.MyDateTimePicker()
        Me.lblLoanCode = New common.Controls.MyLabel()
        Me.txtLoanCode = New common.UserControls.txtFinder()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdjustedByName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLoanName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriodName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriodCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdjustmentMinus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdjustmentPlus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdjustMinus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdjustPlus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdjustmentBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdjustmentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpLoanAdjustDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLoanCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.SplitContainer1)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(836, 491)
        Me.RadGroupBox3.TabIndex = 64
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(10, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAdjustedByName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEmpName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEmpCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEmpCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLoanName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriodName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findPayperiod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriodCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAdjustmentMinus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAdjustmentPlus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAdjustMinus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAdjustPlus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblReason)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAdjustmentBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findLoanAdjustby)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAdjustmentDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpLoanAdjustDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLoanCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLoanCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(816, 461)
        Me.SplitContainer1.SplitterDistance = 404
        Me.SplitContainer1.TabIndex = 0
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(500, 19)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 207
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(376, 20)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'lblAdjustedByName
        '
        Me.lblAdjustedByName.AutoSize = False
        Me.lblAdjustedByName.BorderVisible = True
        Me.lblAdjustedByName.FieldName = Nothing
        Me.lblAdjustedByName.Location = New System.Drawing.Point(376, 189)
        Me.lblAdjustedByName.Name = "lblAdjustedByName"
        Me.lblAdjustedByName.Size = New System.Drawing.Size(222, 19)
        Me.lblAdjustedByName.TabIndex = 12
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.FieldName = Nothing
        Me.lblEmpName.Location = New System.Drawing.Point(376, 93)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(222, 19)
        Me.lblEmpName.TabIndex = 7
        '
        'lblEmpCode
        '
        Me.lblEmpCode.FieldName = Nothing
        Me.lblEmpCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpCode.Location = New System.Drawing.Point(14, 96)
        Me.lblEmpCode.Name = "lblEmpCode"
        Me.lblEmpCode.Size = New System.Drawing.Size(87, 16)
        Me.lblEmpCode.TabIndex = 202
        Me.lblEmpCode.Text = "Employee Code"
        '
        'txtEmpCode
        '
        Me.txtEmpCode.CalculationExpression = Nothing
        Me.txtEmpCode.FieldCode = Nothing
        Me.txtEmpCode.FieldDesc = Nothing
        Me.txtEmpCode.FieldMaxLength = 0
        Me.txtEmpCode.FieldName = Nothing
        Me.txtEmpCode.isCalculatedField = False
        Me.txtEmpCode.IsSourceFromTable = False
        Me.txtEmpCode.IsSourceFromValueList = False
        Me.txtEmpCode.IsUnique = False
        Me.txtEmpCode.Location = New System.Drawing.Point(148, 93)
        Me.txtEmpCode.MendatroryField = True
        Me.txtEmpCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.MyLinkLable1 = Me.lblEmpCode
        Me.txtEmpCode.MyLinkLable2 = Nothing
        Me.txtEmpCode.MyReadOnly = True
        Me.txtEmpCode.MyShowMasterFormButton = False
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.ReferenceFieldDesc = Nothing
        Me.txtEmpCode.ReferenceFieldName = Nothing
        Me.txtEmpCode.ReferenceTableName = Nothing
        Me.txtEmpCode.Size = New System.Drawing.Size(221, 19)
        Me.txtEmpCode.TabIndex = 6
        Me.txtEmpCode.Value = ""
        '
        'lblLoanName
        '
        Me.lblLoanName.AutoSize = False
        Me.lblLoanName.BorderVisible = True
        Me.lblLoanName.FieldName = Nothing
        Me.lblLoanName.Location = New System.Drawing.Point(376, 70)
        Me.lblLoanName.Name = "lblLoanName"
        Me.lblLoanName.Size = New System.Drawing.Size(222, 19)
        Me.lblLoanName.TabIndex = 5
        '
        'lblPayPeriodName
        '
        Me.lblPayPeriodName.AutoSize = False
        Me.lblPayPeriodName.BorderVisible = True
        Me.lblPayPeriodName.FieldName = Nothing
        Me.lblPayPeriodName.Location = New System.Drawing.Point(376, 47)
        Me.lblPayPeriodName.Name = "lblPayPeriodName"
        Me.lblPayPeriodName.Size = New System.Drawing.Size(222, 19)
        Me.lblPayPeriodName.TabIndex = 3
        '
        'findPayperiod
        '
        Me.findPayperiod.CalculationExpression = Nothing
        Me.findPayperiod.FieldCode = Nothing
        Me.findPayperiod.FieldDesc = Nothing
        Me.findPayperiod.FieldMaxLength = 0
        Me.findPayperiod.FieldName = Nothing
        Me.findPayperiod.isCalculatedField = False
        Me.findPayperiod.IsSourceFromTable = False
        Me.findPayperiod.IsSourceFromValueList = False
        Me.findPayperiod.IsUnique = False
        Me.findPayperiod.Location = New System.Drawing.Point(148, 46)
        Me.findPayperiod.MendatroryField = True
        Me.findPayperiod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findPayperiod.MyLinkLable1 = Me.lblPayPeriodCode
        Me.findPayperiod.MyLinkLable2 = Nothing
        Me.findPayperiod.MyReadOnly = False
        Me.findPayperiod.MyShowMasterFormButton = False
        Me.findPayperiod.Name = "findPayperiod"
        Me.findPayperiod.ReferenceFieldDesc = Nothing
        Me.findPayperiod.ReferenceFieldName = Nothing
        Me.findPayperiod.ReferenceTableName = Nothing
        Me.findPayperiod.Size = New System.Drawing.Size(221, 19)
        Me.findPayperiod.TabIndex = 2
        Me.findPayperiod.Value = ""
        '
        'lblPayPeriodCode
        '
        Me.lblPayPeriodCode.FieldName = Nothing
        Me.lblPayPeriodCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayPeriodCode.Location = New System.Drawing.Point(14, 50)
        Me.lblPayPeriodCode.Name = "lblPayPeriodCode"
        Me.lblPayPeriodCode.Size = New System.Drawing.Size(92, 16)
        Me.lblPayPeriodCode.TabIndex = 198
        Me.lblPayPeriodCode.Text = "Pay Period Code"
        '
        'lblAdjustmentMinus
        '
        Me.lblAdjustmentMinus.FieldName = Nothing
        Me.lblAdjustmentMinus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdjustmentMinus.Location = New System.Drawing.Point(14, 165)
        Me.lblAdjustmentMinus.Name = "lblAdjustmentMinus"
        Me.lblAdjustmentMinus.Size = New System.Drawing.Size(96, 16)
        Me.lblAdjustmentMinus.TabIndex = 182
        Me.lblAdjustmentMinus.Text = "Adjustment Minus"
        '
        'lblAdjustmentPlus
        '
        Me.lblAdjustmentPlus.FieldName = Nothing
        Me.lblAdjustmentPlus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdjustmentPlus.Location = New System.Drawing.Point(14, 143)
        Me.lblAdjustmentPlus.Name = "lblAdjustmentPlus"
        Me.lblAdjustmentPlus.Size = New System.Drawing.Size(88, 16)
        Me.lblAdjustmentPlus.TabIndex = 181
        Me.lblAdjustmentPlus.Text = "Adjustment Plus"
        '
        'txtAdjustMinus
        '
        Me.txtAdjustMinus.CalculationExpression = Nothing
        Me.txtAdjustMinus.FieldCode = Nothing
        Me.txtAdjustMinus.FieldDesc = Nothing
        Me.txtAdjustMinus.FieldMaxLength = 0
        Me.txtAdjustMinus.FieldName = Nothing
        Me.txtAdjustMinus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdjustMinus.isCalculatedField = False
        Me.txtAdjustMinus.IsSourceFromTable = False
        Me.txtAdjustMinus.IsSourceFromValueList = False
        Me.txtAdjustMinus.IsUnique = False
        Me.txtAdjustMinus.Location = New System.Drawing.Point(148, 165)
        Me.txtAdjustMinus.MaxLength = 49
        Me.txtAdjustMinus.MendatroryField = True
        Me.txtAdjustMinus.MyLinkLable1 = Me.lblAdjustmentMinus
        Me.txtAdjustMinus.MyLinkLable2 = Nothing
        Me.txtAdjustMinus.Name = "txtAdjustMinus"
        Me.txtAdjustMinus.ReferenceFieldDesc = Nothing
        Me.txtAdjustMinus.ReferenceFieldName = Nothing
        Me.txtAdjustMinus.ReferenceTableName = Nothing
        Me.txtAdjustMinus.Size = New System.Drawing.Size(221, 18)
        Me.txtAdjustMinus.TabIndex = 10
        '
        'txtAdjustPlus
        '
        Me.txtAdjustPlus.CalculationExpression = Nothing
        Me.txtAdjustPlus.FieldCode = Nothing
        Me.txtAdjustPlus.FieldDesc = Nothing
        Me.txtAdjustPlus.FieldMaxLength = 0
        Me.txtAdjustPlus.FieldName = Nothing
        Me.txtAdjustPlus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdjustPlus.isCalculatedField = False
        Me.txtAdjustPlus.IsSourceFromTable = False
        Me.txtAdjustPlus.IsSourceFromValueList = False
        Me.txtAdjustPlus.IsUnique = False
        Me.txtAdjustPlus.Location = New System.Drawing.Point(148, 143)
        Me.txtAdjustPlus.MaxLength = 49
        Me.txtAdjustPlus.MendatroryField = True
        Me.txtAdjustPlus.MyLinkLable1 = Me.lblAdjustmentPlus
        Me.txtAdjustPlus.MyLinkLable2 = Nothing
        Me.txtAdjustPlus.Name = "txtAdjustPlus"
        Me.txtAdjustPlus.ReferenceFieldDesc = Nothing
        Me.txtAdjustPlus.ReferenceFieldName = Nothing
        Me.txtAdjustPlus.ReferenceTableName = Nothing
        Me.txtAdjustPlus.Size = New System.Drawing.Size(221, 18)
        Me.txtAdjustPlus.TabIndex = 9
        '
        'txtDescription
        '
        Me.txtDescription.AutoSize = False
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(148, 214)
        Me.txtDescription.MaxLength = 49
        Me.txtDescription.MendatroryField = True
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.lblReason
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(222, 43)
        Me.txtDescription.TabIndex = 13
        '
        'lblReason
        '
        Me.lblReason.FieldName = Nothing
        Me.lblReason.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReason.Location = New System.Drawing.Point(14, 214)
        Me.lblReason.Name = "lblReason"
        Me.lblReason.Size = New System.Drawing.Size(45, 16)
        Me.lblReason.TabIndex = 177
        Me.lblReason.Text = "Reason"
        '
        'lblAdjustmentBy
        '
        Me.lblAdjustmentBy.FieldName = Nothing
        Me.lblAdjustmentBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdjustmentBy.Location = New System.Drawing.Point(14, 192)
        Me.lblAdjustmentBy.Name = "lblAdjustmentBy"
        Me.lblAdjustmentBy.Size = New System.Drawing.Size(79, 16)
        Me.lblAdjustmentBy.TabIndex = 165
        Me.lblAdjustmentBy.Text = "Adjustment By"
        '
        'findLoanAdjustby
        '
        Me.findLoanAdjustby.CalculationExpression = Nothing
        Me.findLoanAdjustby.FieldCode = Nothing
        Me.findLoanAdjustby.FieldDesc = Nothing
        Me.findLoanAdjustby.FieldMaxLength = 0
        Me.findLoanAdjustby.FieldName = Nothing
        Me.findLoanAdjustby.isCalculatedField = False
        Me.findLoanAdjustby.IsSourceFromTable = False
        Me.findLoanAdjustby.IsSourceFromValueList = False
        Me.findLoanAdjustby.IsUnique = False
        Me.findLoanAdjustby.Location = New System.Drawing.Point(148, 189)
        Me.findLoanAdjustby.MendatroryField = False
        Me.findLoanAdjustby.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findLoanAdjustby.MyLinkLable1 = Me.lblAdjustmentBy
        Me.findLoanAdjustby.MyLinkLable2 = Nothing
        Me.findLoanAdjustby.MyReadOnly = False
        Me.findLoanAdjustby.MyShowMasterFormButton = False
        Me.findLoanAdjustby.Name = "findLoanAdjustby"
        Me.findLoanAdjustby.ReferenceFieldDesc = Nothing
        Me.findLoanAdjustby.ReferenceFieldName = Nothing
        Me.findLoanAdjustby.ReferenceTableName = Nothing
        Me.findLoanAdjustby.Size = New System.Drawing.Size(221, 19)
        Me.findLoanAdjustby.TabIndex = 11
        Me.findLoanAdjustby.Value = ""
        '
        'lblAdjustmentDate
        '
        Me.lblAdjustmentDate.FieldName = Nothing
        Me.lblAdjustmentDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdjustmentDate.Location = New System.Drawing.Point(14, 118)
        Me.lblAdjustmentDate.Name = "lblAdjustmentDate"
        Me.lblAdjustmentDate.Size = New System.Drawing.Size(90, 16)
        Me.lblAdjustmentDate.TabIndex = 164
        Me.lblAdjustmentDate.Text = "Adjustment Date"
        '
        'dtpLoanAdjustDate
        '
        Me.dtpLoanAdjustDate.CalculationExpression = Nothing
        Me.dtpLoanAdjustDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpLoanAdjustDate.FieldCode = Nothing
        Me.dtpLoanAdjustDate.FieldDesc = Nothing
        Me.dtpLoanAdjustDate.FieldMaxLength = 0
        Me.dtpLoanAdjustDate.FieldName = Nothing
        Me.dtpLoanAdjustDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpLoanAdjustDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpLoanAdjustDate.isCalculatedField = False
        Me.dtpLoanAdjustDate.IsSourceFromTable = False
        Me.dtpLoanAdjustDate.IsSourceFromValueList = False
        Me.dtpLoanAdjustDate.IsUnique = False
        Me.dtpLoanAdjustDate.Location = New System.Drawing.Point(148, 119)
        Me.dtpLoanAdjustDate.MendatroryField = True
        Me.dtpLoanAdjustDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpLoanAdjustDate.MyLinkLable1 = Me.lblAdjustmentDate
        Me.dtpLoanAdjustDate.MyLinkLable2 = Nothing
        Me.dtpLoanAdjustDate.Name = "dtpLoanAdjustDate"
        Me.dtpLoanAdjustDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpLoanAdjustDate.ReferenceFieldDesc = Nothing
        Me.dtpLoanAdjustDate.ReferenceFieldName = Nothing
        Me.dtpLoanAdjustDate.ReferenceTableName = Nothing
        Me.dtpLoanAdjustDate.Size = New System.Drawing.Size(130, 18)
        Me.dtpLoanAdjustDate.TabIndex = 8
        Me.dtpLoanAdjustDate.TabStop = False
        Me.dtpLoanAdjustDate.Text = "03/05/2011"
        Me.dtpLoanAdjustDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblLoanCode
        '
        Me.lblLoanCode.FieldName = Nothing
        Me.lblLoanCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoanCode.Location = New System.Drawing.Point(14, 72)
        Me.lblLoanCode.Name = "lblLoanCode"
        Me.lblLoanCode.Size = New System.Drawing.Size(62, 16)
        Me.lblLoanCode.TabIndex = 154
        Me.lblLoanCode.Text = "Loan Code"
        '
        'txtLoanCode
        '
        Me.txtLoanCode.CalculationExpression = Nothing
        Me.txtLoanCode.FieldCode = Nothing
        Me.txtLoanCode.FieldDesc = Nothing
        Me.txtLoanCode.FieldMaxLength = 0
        Me.txtLoanCode.FieldName = Nothing
        Me.txtLoanCode.isCalculatedField = False
        Me.txtLoanCode.IsSourceFromTable = False
        Me.txtLoanCode.IsSourceFromValueList = False
        Me.txtLoanCode.IsUnique = False
        Me.txtLoanCode.Location = New System.Drawing.Point(148, 69)
        Me.txtLoanCode.MendatroryField = True
        Me.txtLoanCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoanCode.MyLinkLable1 = Me.lblLoanCode
        Me.txtLoanCode.MyLinkLable2 = Nothing
        Me.txtLoanCode.MyReadOnly = False
        Me.txtLoanCode.MyShowMasterFormButton = False
        Me.txtLoanCode.Name = "txtLoanCode"
        Me.txtLoanCode.ReferenceFieldDesc = Nothing
        Me.txtLoanCode.ReferenceFieldName = Nothing
        Me.txtLoanCode.ReferenceTableName = Nothing
        Me.txtLoanCode.Size = New System.Drawing.Size(221, 19)
        Me.txtLoanCode.TabIndex = 4
        Me.txtLoanCode.Value = ""
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(149, 19)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(221, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(14, 24)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(121, 16)
        Me.lblCode.TabIndex = 161
        Me.lblCode.Text = "Loan Adjustment Code"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(78, 26)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 18)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 26)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(741, 26)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(147, 26)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'frmLoanAdjustment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(836, 491)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Name = "frmLoanAdjustment"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Loan Adjustment"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdjustedByName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLoanName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriodName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriodCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdjustmentMinus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdjustmentPlus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdjustMinus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdjustPlus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdjustmentBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdjustmentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpLoanAdjustDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLoanCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblReason As common.Controls.MyLabel
    Friend WithEvents lblAdjustmentBy As common.Controls.MyLabel
    Friend WithEvents findLoanAdjustby As common.UserControls.txtFinder
    Friend WithEvents lblAdjustmentDate As common.Controls.MyLabel
    Friend WithEvents dtpLoanAdjustDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblLoanCode As common.Controls.MyLabel
    Friend WithEvents txtLoanCode As common.UserControls.txtFinder
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblAdjustmentMinus As common.Controls.MyLabel
    Friend WithEvents lblAdjustmentPlus As common.Controls.MyLabel
    Friend WithEvents txtAdjustMinus As common.Controls.MyTextBox
    Friend WithEvents txtAdjustPlus As common.Controls.MyTextBox
    Friend WithEvents lblPayPeriodName As common.Controls.MyLabel
    Friend WithEvents findPayperiod As common.UserControls.txtFinder
    Friend WithEvents lblPayPeriodCode As common.Controls.MyLabel
    Friend WithEvents lblLoanName As common.Controls.MyLabel
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents lblEmpCode As common.Controls.MyLabel
    Friend WithEvents txtEmpCode As common.UserControls.txtFinder
    Friend WithEvents lblAdjustedByName As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
End Class
