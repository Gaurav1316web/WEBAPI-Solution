Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDailyAttendance
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDailyAttendance))
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.UsLock1 = New common.usLock()
        Me.txtPayPeriodName = New common.Controls.MyTextBox()
        Me.txtPayPeriodDays = New common.Controls.MyTextBox()
        Me.lblPayPeriod = New common.Controls.MyLabel()
        Me.findPayperiod = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblRemarks = New common.Controls.MyLabel()
        Me.findLocation = New common.UserControls.txtFinder()
        Me.lblAttendanceDate = New common.Controls.MyLabel()
        Me.dtpAttendanceDate = New common.Controls.MyDateTimePicker()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblDaylyAttnCode = New common.Controls.MyLabel()
        Me.gvDailyAttendance = New common.UserControls.MyRadGridView()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.radPageAttachment = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ucCustomFields()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MIImportSingle = New Telerik.WinControls.UI.RadMenuItem()
        Me.MIImportDual = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuImportDual = New Telerik.WinControls.UI.RadMenuItem()
        Me.MIExportSingle = New Telerik.WinControls.UI.RadMenuItem()
        Me.MiExportDual = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MIExportBlankSingle = New Telerik.WinControls.UI.RadMenuItem()
        Me.MIExportBlankDual = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtPayPeriodName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPayPeriodDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAttendanceDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpAttendanceDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDaylyAttnCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDailyAttendance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDailyAttendance.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.radPageAttachment.SuspendLayout()
        Me.pvpCustomFields.SuspendLayout()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadPageView1)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(904, 506)
        Me.RadGroupBox3.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.radPageAttachment)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(10, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(884, 476)
        Me.RadPageView1.TabIndex = 212
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(73.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(863, 428)
        Me.RadPageViewPage1.Text = "Attendance"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPayPeriodName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPayPeriodDays)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findPayperiod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAttendanceDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpAttendanceDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDaylyAttnCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(863, 428)
        Me.SplitContainer1.SplitterDistance = 393
        Me.SplitContainer1.TabIndex = 0
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(714, 8)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 18)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 210
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
        Me.txtPayPeriodName.Location = New System.Drawing.Point(346, 27)
        Me.txtPayPeriodName.MaxLength = 49
        Me.txtPayPeriodName.MendatroryField = True
        Me.txtPayPeriodName.MyLinkLable1 = Nothing
        Me.txtPayPeriodName.MyLinkLable2 = Nothing
        Me.txtPayPeriodName.Name = "txtPayPeriodName"
        Me.txtPayPeriodName.ReferenceFieldDesc = Nothing
        Me.txtPayPeriodName.ReferenceFieldName = Nothing
        Me.txtPayPeriodName.ReferenceTableName = Nothing
        Me.txtPayPeriodName.Size = New System.Drawing.Size(171, 18)
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
        Me.txtPayPeriodDays.Location = New System.Drawing.Point(293, 26)
        Me.txtPayPeriodDays.MaxLength = 50
        Me.txtPayPeriodDays.MendatroryField = False
        Me.txtPayPeriodDays.MyLinkLable1 = Me.lblPayPeriod
        Me.txtPayPeriodDays.MyLinkLable2 = Nothing
        Me.txtPayPeriodDays.Name = "txtPayPeriodDays"
        Me.txtPayPeriodDays.ReadOnly = True
        Me.txtPayPeriodDays.ReferenceFieldDesc = Nothing
        Me.txtPayPeriodDays.ReferenceFieldName = Nothing
        Me.txtPayPeriodDays.ReferenceTableName = Nothing
        Me.txtPayPeriodDays.Size = New System.Drawing.Size(50, 20)
        Me.txtPayPeriodDays.TabIndex = 186
        '
        'lblPayPeriod
        '
        Me.lblPayPeriod.FieldName = Nothing
        Me.lblPayPeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayPeriod.Location = New System.Drawing.Point(13, 29)
        Me.lblPayPeriod.Name = "lblPayPeriod"
        Me.lblPayPeriod.Size = New System.Drawing.Size(92, 16)
        Me.lblPayPeriod.TabIndex = 184
        Me.lblPayPeriod.Text = "Pay Period Code"
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
        Me.findPayperiod.Location = New System.Drawing.Point(109, 27)
        Me.findPayperiod.MendatroryField = True
        Me.findPayperiod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findPayperiod.MyLinkLable1 = Me.lblLocation
        Me.findPayperiod.MyLinkLable2 = Nothing
        Me.findPayperiod.MyReadOnly = False
        Me.findPayperiod.MyShowMasterFormButton = False
        Me.findPayperiod.Name = "findPayperiod"
        Me.findPayperiod.ReferenceFieldDesc = Nothing
        Me.findPayperiod.ReferenceFieldName = Nothing
        Me.findPayperiod.ReferenceTableName = Nothing
        Me.findPayperiod.Size = New System.Drawing.Size(182, 18)
        Me.findPayperiod.TabIndex = 3
        Me.findPayperiod.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(14, 48)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 165
        Me.lblLocation.Text = "Location"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(330, 8)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 18)
        Me.btnNew.TabIndex = 180
        Me.btnNew.Text = " "
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
        Me.txtDescription.Location = New System.Drawing.Point(109, 68)
        Me.txtDescription.MaxLength = 49
        Me.txtDescription.MendatroryField = True
        Me.txtDescription.MyLinkLable1 = Me.lblRemarks
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(409, 18)
        Me.txtDescription.TabIndex = 5
        '
        'lblRemarks
        '
        Me.lblRemarks.FieldName = Nothing
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(15, 69)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(51, 16)
        Me.lblRemarks.TabIndex = 177
        Me.lblRemarks.Text = "Remarks"
        '
        'findLocation
        '
        Me.findLocation.CalculationExpression = Nothing
        Me.findLocation.FieldCode = Nothing
        Me.findLocation.FieldDesc = Nothing
        Me.findLocation.FieldMaxLength = 0
        Me.findLocation.FieldName = Nothing
        Me.findLocation.isCalculatedField = False
        Me.findLocation.IsSourceFromTable = False
        Me.findLocation.IsSourceFromValueList = False
        Me.findLocation.IsUnique = False
        Me.findLocation.Location = New System.Drawing.Point(109, 47)
        Me.findLocation.MendatroryField = True
        Me.findLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findLocation.MyLinkLable1 = Me.lblLocation
        Me.findLocation.MyLinkLable2 = Nothing
        Me.findLocation.MyReadOnly = False
        Me.findLocation.MyShowMasterFormButton = False
        Me.findLocation.Name = "findLocation"
        Me.findLocation.ReferenceFieldDesc = Nothing
        Me.findLocation.ReferenceFieldName = Nothing
        Me.findLocation.ReferenceTableName = Nothing
        Me.findLocation.Size = New System.Drawing.Size(221, 19)
        Me.findLocation.TabIndex = 2
        Me.findLocation.Value = ""
        '
        'lblAttendanceDate
        '
        Me.lblAttendanceDate.FieldName = Nothing
        Me.lblAttendanceDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAttendanceDate.Location = New System.Drawing.Point(351, 9)
        Me.lblAttendanceDate.Name = "lblAttendanceDate"
        Me.lblAttendanceDate.Size = New System.Drawing.Size(33, 16)
        Me.lblAttendanceDate.TabIndex = 164
        Me.lblAttendanceDate.Text = " Date"
        '
        'dtpAttendanceDate
        '
        Me.dtpAttendanceDate.CalculationExpression = Nothing
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
        Me.dtpAttendanceDate.Location = New System.Drawing.Point(386, 8)
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
        Me.dtpAttendanceDate.Text = "28/06/2013"
        Me.dtpAttendanceDate.Value = New Date(2013, 6, 28, 0, 0, 0, 0)
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(109, 8)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblDaylyAttnCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(221, 18)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblDaylyAttnCode
        '
        Me.lblDaylyAttnCode.FieldName = Nothing
        Me.lblDaylyAttnCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDaylyAttnCode.Location = New System.Drawing.Point(13, 9)
        Me.lblDaylyAttnCode.Name = "lblDaylyAttnCode"
        Me.lblDaylyAttnCode.Size = New System.Drawing.Size(94, 16)
        Me.lblDaylyAttnCode.TabIndex = 161
        Me.lblDaylyAttnCode.Text = "Attendance Code"
        '
        'gvDailyAttendance
        '
        Me.gvDailyAttendance.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvDailyAttendance.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvDailyAttendance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDailyAttendance.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvDailyAttendance.ForeColor = System.Drawing.Color.Black
        Me.gvDailyAttendance.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvDailyAttendance.Location = New System.Drawing.Point(3, 18)
        '
        'gvDailyAttendance
        '
        Me.gvDailyAttendance.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvDailyAttendance.MasterTemplate.AllowAddNewRow = False
        Me.gvDailyAttendance.MasterTemplate.AutoGenerateColumns = False
        Me.gvDailyAttendance.MasterTemplate.EnableGrouping = False
        Me.gvDailyAttendance.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDailyAttendance.Name = "gvDailyAttendance"
        Me.gvDailyAttendance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvDailyAttendance.ShowHeaderCellButtons = True
        Me.gvDailyAttendance.Size = New System.Drawing.Size(835, 277)
        Me.gvDailyAttendance.TabIndex = 145
        Me.gvDailyAttendance.Text = "RadGridView4"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(78, 10)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 18)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 10)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(788, 10)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(147, 10)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'radPageAttachment
        '
        Me.radPageAttachment.Controls.Add(Me.UcAttachment1)
        Me.radPageAttachment.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.radPageAttachment.Location = New System.Drawing.Point(10, 37)
        Me.radPageAttachment.Name = "radPageAttachment"
        Me.radPageAttachment.Size = New System.Drawing.Size(847, 416)
        Me.radPageAttachment.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(847, 416)
        Me.UcAttachment1.TabIndex = 2
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(86.0!, 28.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(847, 416)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(847, 416)
        Me.UcCustomFields1.TabIndex = 1
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "File"
        Me.RadMenuItem3.AccessibleName = "File"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuImport, Me.MenuImportDual, Me.RadMenuItem2})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'MenuImport
        '
        Me.MenuImport.AccessibleDescription = "MenuImport"
        Me.MenuImport.AccessibleName = "MenuImport"
        Me.MenuImport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MIImportSingle, Me.MIImportDual})
        Me.MenuImport.Name = "MenuImport"
        Me.MenuImport.Text = "Import"
        '
        'MIImportSingle
        '
        Me.MIImportSingle.AccessibleDescription = "RadMenuItem4"
        Me.MIImportSingle.AccessibleName = "MIImportSingle"
        Me.MIImportSingle.Name = "MIImportSingle"
        Me.MIImportSingle.Text = "Single"
        '
        'MIImportDual
        '
        Me.MIImportDual.AccessibleDescription = "RadMenuItem5"
        Me.MIImportDual.AccessibleName = "RadMenuItem5"
        Me.MIImportDual.Name = "MIImportDual"
        Me.MIImportDual.Text = "Dual"
        '
        'MenuImportDual
        '
        Me.MenuImportDual.AccessibleDescription = "MenuImportDual"
        Me.MenuImportDual.AccessibleName = "MenuImportDual"
        Me.MenuImportDual.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MIExportSingle, Me.MiExportDual})
        Me.MenuImportDual.Name = "MenuImportDual"
        Me.MenuImportDual.Text = "Export"
        '
        'MIExportSingle
        '
        Me.MIExportSingle.AccessibleDescription = "Single"
        Me.MIExportSingle.AccessibleName = "Single"
        Me.MIExportSingle.Name = "MIExportSingle"
        Me.MIExportSingle.Text = "Single"
        '
        'MiExportDual
        '
        Me.MiExportDual.AccessibleDescription = "Dual"
        Me.MiExportDual.AccessibleName = "Dual"
        Me.MiExportDual.Name = "MiExportDual"
        Me.MiExportDual.Text = "Dual"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Export Blank Sheet"
        Me.RadMenuItem2.AccessibleName = "Export Blank Sheet"
        Me.RadMenuItem2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MIExportBlankSingle, Me.MIExportBlankDual})
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export Blank Sheet"
        '
        'MIExportBlankSingle
        '
        Me.MIExportBlankSingle.AccessibleDescription = "Single"
        Me.MIExportBlankSingle.AccessibleName = "Single"
        Me.MIExportBlankSingle.Name = "MIExportBlankSingle"
        Me.MIExportBlankSingle.Text = "Single"
        '
        'MIExportBlankDual
        '
        Me.MIExportBlankDual.AccessibleDescription = "Dual"
        Me.MIExportBlankDual.AccessibleName = "Dual"
        Me.MIExportBlankDual.Name = "MIExportBlankDual"
        Me.MIExportBlankDual.Text = "Dual"
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(904, 20)
        Me.RadMenu2.TabIndex = 11
        Me.RadMenu2.Text = "RadMenu2"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.gvDailyAttendance)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 92)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(841, 298)
        Me.GroupBox1.TabIndex = 211
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Employee"
        '
        'frmDailyAttendance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(904, 506)
        Me.Controls.Add(Me.RadMenu2)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Name = "frmDailyAttendance"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Daily Attendance"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtPayPeriodName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPayPeriodDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAttendanceDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpAttendanceDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDaylyAttnCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDailyAttendance.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDailyAttendance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.radPageAttachment.ResumeLayout(False)
        Me.pvpCustomFields.ResumeLayout(False)
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblRemarks As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents findLocation As common.UserControls.txtFinder
    Friend WithEvents lblAttendanceDate As common.Controls.MyLabel
    Friend WithEvents dtpAttendanceDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblDaylyAttnCode As common.Controls.MyLabel
    Friend WithEvents gvDailyAttendance As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtPayPeriodName As common.Controls.MyTextBox
    Friend WithEvents txtPayPeriodDays As common.Controls.MyTextBox
    Friend WithEvents lblPayPeriod As common.Controls.MyLabel
    Friend WithEvents findPayperiod As common.UserControls.txtFinder
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuImportDual As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents MIImportSingle As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MIImportDual As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MIExportSingle As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MiExportDual As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MIExportBlankSingle As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MIExportBlankDual As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents radPageAttachment As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ucCustomFields
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
