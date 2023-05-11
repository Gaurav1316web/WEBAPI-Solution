Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHourlyAttendance
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHourlyAttendance))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.UsLock1 = New common.usLock()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.lblPayPeriod = New common.Controls.MyLabel()
        Me.llbPPName = New common.Controls.MyLabel()
        Me.txtPayPeriodName = New common.Controls.MyTextBox()
        Me.txtPayPeriodDays = New common.Controls.MyTextBox()
        Me.findPayperiod = New common.UserControls.txtFinder()
        Me.lblEnteredBy = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblRemarks = New common.Controls.MyLabel()
        Me.findEnteredBy = New common.UserControls.txtFinder()
        Me.lblAttendanceDate = New common.Controls.MyLabel()
        Me.dtpAttendanceDate = New common.Controls.MyDateTimePicker()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblDaylyAttnCode = New common.Controls.MyLabel()
        Me.gvHourlyAttendance = New common.UserControls.MyRadGridView()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.llbPPName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPayPeriodName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPayPeriodDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEnteredBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAttendanceDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpAttendanceDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDaylyAttnCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvHourlyAttendance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvHourlyAttendance.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 26)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.llbPPName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPayPeriodName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPayPeriodDays)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findPayperiod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEnteredBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findEnteredBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAttendanceDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpAttendanceDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDaylyAttnCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gvHourlyAttendance)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(882, 485)
        Me.SplitContainer1.SplitterDistance = 446
        Me.SplitContainer1.TabIndex = 0
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(775, 9)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 18)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 211
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(344, 6)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 194
        Me.btnNew.Text = " "
        '
        'lblPayPeriod
        '
        Me.lblPayPeriod.FieldName = Nothing
        Me.lblPayPeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayPeriod.Location = New System.Drawing.Point(385, 9)
        Me.lblPayPeriod.Name = "lblPayPeriod"
        Me.lblPayPeriod.Size = New System.Drawing.Size(92, 16)
        Me.lblPayPeriod.TabIndex = 193
        Me.lblPayPeriod.Text = "Pay Period Code"
        '
        'llbPPName
        '
        Me.llbPPName.FieldName = Nothing
        Me.llbPPName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llbPPName.Location = New System.Drawing.Point(385, 36)
        Me.llbPPName.Name = "llbPPName"
        Me.llbPPName.Size = New System.Drawing.Size(95, 16)
        Me.llbPPName.TabIndex = 192
        Me.llbPPName.Text = "Pay Period Name"
        '
        'txtPayPeriodName
        '
        Me.txtPayPeriodName.CalculationExpression = Nothing
        Me.txtPayPeriodName.FieldCode = Nothing
        Me.txtPayPeriodName.FieldDesc = Nothing
        Me.txtPayPeriodName.FieldMaxLength = 0
        Me.txtPayPeriodName.FieldName = Nothing
        Me.txtPayPeriodName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayPeriodName.isCalculatedField = False
        Me.txtPayPeriodName.IsSourceFromTable = False
        Me.txtPayPeriodName.IsSourceFromValueList = False
        Me.txtPayPeriodName.IsUnique = False
        Me.txtPayPeriodName.Location = New System.Drawing.Point(494, 33)
        Me.txtPayPeriodName.MaxLength = 49
        Me.txtPayPeriodName.MendatroryField = True
        Me.txtPayPeriodName.MyLinkLable1 = Me.llbPPName
        Me.txtPayPeriodName.MyLinkLable2 = Nothing
        Me.txtPayPeriodName.Name = "txtPayPeriodName"
        Me.txtPayPeriodName.ReferenceFieldDesc = Nothing
        Me.txtPayPeriodName.ReferenceFieldName = Nothing
        Me.txtPayPeriodName.ReferenceTableName = Nothing
        Me.txtPayPeriodName.Size = New System.Drawing.Size(379, 18)
        Me.txtPayPeriodName.TabIndex = 4
        '
        'txtPayPeriodDays
        '
        Me.txtPayPeriodDays.CalculationExpression = Nothing
        Me.txtPayPeriodDays.FieldCode = Nothing
        Me.txtPayPeriodDays.FieldDesc = Nothing
        Me.txtPayPeriodDays.FieldMaxLength = 0
        Me.txtPayPeriodDays.FieldName = Nothing
        Me.txtPayPeriodDays.isCalculatedField = False
        Me.txtPayPeriodDays.IsSourceFromTable = False
        Me.txtPayPeriodDays.IsSourceFromValueList = False
        Me.txtPayPeriodDays.IsUnique = False
        Me.txtPayPeriodDays.Location = New System.Drawing.Point(663, 7)
        Me.txtPayPeriodDays.MaxLength = 50
        Me.txtPayPeriodDays.MendatroryField = False
        Me.txtPayPeriodDays.MyLinkLable1 = Nothing
        Me.txtPayPeriodDays.MyLinkLable2 = Nothing
        Me.txtPayPeriodDays.Name = "txtPayPeriodDays"
        Me.txtPayPeriodDays.ReadOnly = True
        Me.txtPayPeriodDays.ReferenceFieldDesc = Nothing
        Me.txtPayPeriodDays.ReferenceFieldName = Nothing
        Me.txtPayPeriodDays.ReferenceTableName = Nothing
        Me.txtPayPeriodDays.Size = New System.Drawing.Size(92, 20)
        Me.txtPayPeriodDays.TabIndex = 190
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
        Me.findPayperiod.Location = New System.Drawing.Point(494, 8)
        Me.findPayperiod.MendatroryField = True
        Me.findPayperiod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findPayperiod.MyLinkLable1 = Me.lblEnteredBy
        Me.findPayperiod.MyLinkLable2 = Nothing
        Me.findPayperiod.MyReadOnly = False
        Me.findPayperiod.MyShowMasterFormButton = False
        Me.findPayperiod.Name = "findPayperiod"
        Me.findPayperiod.ReferenceFieldDesc = Nothing
        Me.findPayperiod.ReferenceFieldName = Nothing
        Me.findPayperiod.ReferenceTableName = Nothing
        Me.findPayperiod.Size = New System.Drawing.Size(163, 19)
        Me.findPayperiod.TabIndex = 3
        Me.findPayperiod.Value = ""
        '
        'lblEnteredBy
        '
        Me.lblEnteredBy.FieldName = Nothing
        Me.lblEnteredBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnteredBy.Location = New System.Drawing.Point(12, 59)
        Me.lblEnteredBy.Name = "lblEnteredBy"
        Me.lblEnteredBy.Size = New System.Drawing.Size(62, 16)
        Me.lblEnteredBy.TabIndex = 165
        Me.lblEnteredBy.Text = "Entered By"
        '
        'txtDescription
        '
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
        Me.txtDescription.Location = New System.Drawing.Point(494, 57)
        Me.txtDescription.MaxLength = 49
        Me.txtDescription.MendatroryField = True
        Me.txtDescription.MyLinkLable1 = Me.lblRemarks
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(379, 18)
        Me.txtDescription.TabIndex = 5
        '
        'lblRemarks
        '
        Me.lblRemarks.FieldName = Nothing
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(385, 60)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(51, 16)
        Me.lblRemarks.TabIndex = 177
        Me.lblRemarks.Text = "Remarks"
        '
        'findEnteredBy
        '
        Me.findEnteredBy.CalculationExpression = Nothing
        Me.findEnteredBy.FieldCode = Nothing
        Me.findEnteredBy.FieldDesc = Nothing
        Me.findEnteredBy.FieldMaxLength = 0
        Me.findEnteredBy.FieldName = Nothing
        Me.findEnteredBy.isCalculatedField = False
        Me.findEnteredBy.IsSourceFromTable = False
        Me.findEnteredBy.IsSourceFromValueList = False
        Me.findEnteredBy.IsUnique = False
        Me.findEnteredBy.Location = New System.Drawing.Point(120, 59)
        Me.findEnteredBy.MendatroryField = True
        Me.findEnteredBy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findEnteredBy.MyLinkLable1 = Me.lblEnteredBy
        Me.findEnteredBy.MyLinkLable2 = Nothing
        Me.findEnteredBy.MyReadOnly = False
        Me.findEnteredBy.MyShowMasterFormButton = False
        Me.findEnteredBy.Name = "findEnteredBy"
        Me.findEnteredBy.ReferenceFieldDesc = Nothing
        Me.findEnteredBy.ReferenceFieldName = Nothing
        Me.findEnteredBy.ReferenceTableName = Nothing
        Me.findEnteredBy.Size = New System.Drawing.Size(238, 21)
        Me.findEnteredBy.TabIndex = 2
        Me.findEnteredBy.Value = ""
        '
        'lblAttendanceDate
        '
        Me.lblAttendanceDate.FieldName = Nothing
        Me.lblAttendanceDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAttendanceDate.Location = New System.Drawing.Point(12, 35)
        Me.lblAttendanceDate.Name = "lblAttendanceDate"
        Me.lblAttendanceDate.Size = New System.Drawing.Size(91, 16)
        Me.lblAttendanceDate.TabIndex = 164
        Me.lblAttendanceDate.Text = "Attendance Date"
        '
        'dtpAttendanceDate
        '
        Me.dtpAttendanceDate.CalculationExpression = Nothing
        Me.dtpAttendanceDate.Culture = New System.Globalization.CultureInfo("en-IN")
        Me.dtpAttendanceDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpAttendanceDate.FieldCode = Nothing
        Me.dtpAttendanceDate.FieldDesc = Nothing
        Me.dtpAttendanceDate.FieldMaxLength = 0
        Me.dtpAttendanceDate.FieldName = Nothing
        Me.dtpAttendanceDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpAttendanceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAttendanceDate.isCalculatedField = False
        Me.dtpAttendanceDate.IsSourceFromTable = False
        Me.dtpAttendanceDate.IsSourceFromValueList = False
        Me.dtpAttendanceDate.IsUnique = False
        Me.dtpAttendanceDate.Location = New System.Drawing.Point(120, 33)
        Me.dtpAttendanceDate.MendatroryField = True
        Me.dtpAttendanceDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAttendanceDate.MyLinkLable1 = Me.lblAttendanceDate
        Me.dtpAttendanceDate.MyLinkLable2 = Nothing
        Me.dtpAttendanceDate.Name = "dtpAttendanceDate"
        Me.dtpAttendanceDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAttendanceDate.ReferenceFieldDesc = Nothing
        Me.dtpAttendanceDate.ReferenceFieldName = Nothing
        Me.dtpAttendanceDate.ReferenceTableName = Nothing
        Me.dtpAttendanceDate.Size = New System.Drawing.Size(130, 18)
        Me.dtpAttendanceDate.TabIndex = 1
        Me.dtpAttendanceDate.TabStop = False
        Me.dtpAttendanceDate.Text = "28-06-2013"
        Me.dtpAttendanceDate.Value = New Date(2013, 6, 28, 0, 0, 0, 0)
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(120, 6)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblDaylyAttnCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(221, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblDaylyAttnCode
        '
        Me.lblDaylyAttnCode.FieldName = Nothing
        Me.lblDaylyAttnCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDaylyAttnCode.Location = New System.Drawing.Point(12, 11)
        Me.lblDaylyAttnCode.Name = "lblDaylyAttnCode"
        Me.lblDaylyAttnCode.Size = New System.Drawing.Size(94, 16)
        Me.lblDaylyAttnCode.TabIndex = 161
        Me.lblDaylyAttnCode.Text = "Attendance Code"
        '
        'gvHourlyAttendance
        '
        Me.gvHourlyAttendance.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvHourlyAttendance.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvHourlyAttendance.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvHourlyAttendance.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvHourlyAttendance.ForeColor = System.Drawing.Color.Black
        Me.gvHourlyAttendance.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvHourlyAttendance.Location = New System.Drawing.Point(9, 86)
        '
        'gvHourlyAttendance
        '
        Me.gvHourlyAttendance.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvHourlyAttendance.MasterTemplate.AllowAddNewRow = False
        Me.gvHourlyAttendance.MasterTemplate.AutoGenerateColumns = False
        Me.gvHourlyAttendance.MasterTemplate.EnableGrouping = False
        Me.gvHourlyAttendance.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvHourlyAttendance.Name = "gvHourlyAttendance"
        Me.gvHourlyAttendance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvHourlyAttendance.ShowHeaderCellButtons = True
        Me.gvHourlyAttendance.Size = New System.Drawing.Size(864, 357)
        Me.gvHourlyAttendance.TabIndex = 6
        Me.gvHourlyAttendance.Text = "RadGridView4"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(78, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 18)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 1
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(807, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 0
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(147, 8)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 3
        Me.btndelete.Text = "Delete"
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(909, 20)
        Me.RadMenu2.TabIndex = 12
        Me.RadMenu2.Text = "RadMenu2"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3, Me.RadMenuItem4, Me.RadMenuItem5})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Export Blank Sheet"
        Me.RadMenuItem2.AccessibleName = "Export Blank Sheet"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export Blank Sheet"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Import"
        Me.RadMenuItem3.AccessibleName = "Import"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Import"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "Import from Biometric "
        Me.RadMenuItem4.AccessibleName = "Import from Biometric "
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Import from Biometric "
        '
        'RadMenuItem5
        '
        Me.RadMenuItem5.AccessibleDescription = "Export from biometric "
        Me.RadMenuItem5.AccessibleName = "Export from biometric "
        Me.RadMenuItem5.Name = "RadMenuItem5"
        Me.RadMenuItem5.Text = "Export from Biometric "
        '
        'frmHourlyAttendance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(909, 515)
        Me.Controls.Add(Me.RadMenu2)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmHourlyAttendance"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Hourly Attendance"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.llbPPName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPayPeriodName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPayPeriodDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEnteredBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAttendanceDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpAttendanceDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDaylyAttnCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvHourlyAttendance.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvHourlyAttendance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblRemarks As common.Controls.MyLabel
    Friend WithEvents lblEnteredBy As common.Controls.MyLabel
    Friend WithEvents findEnteredBy As common.UserControls.txtFinder
    Friend WithEvents lblAttendanceDate As common.Controls.MyLabel
    Friend WithEvents dtpAttendanceDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblDaylyAttnCode As common.Controls.MyLabel
    Friend WithEvents gvHourlyAttendance As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents llbPPName As common.Controls.MyLabel
    Friend WithEvents txtPayPeriodName As common.Controls.MyTextBox
    Friend WithEvents txtPayPeriodDays As common.Controls.MyTextBox
    Friend WithEvents findPayperiod As common.UserControls.txtFinder
    Friend WithEvents lblPayPeriod As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem5 As Telerik.WinControls.UI.RadMenuItem
End Class
