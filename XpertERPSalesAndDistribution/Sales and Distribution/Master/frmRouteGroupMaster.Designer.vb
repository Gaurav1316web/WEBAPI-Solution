<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRouteGroupMaster
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
        Me.components = New System.ComponentModel.Container
        Me.rlblGroupID = New common.Controls.MyLabel
        Me.rlblRouteID = New common.Controls.MyLabel
        Me.rlblStart_Date = New common.Controls.MyLabel
        Me.rlblDescription = New common.Controls.MyLabel
        Me.rbtnReset = New Telerik.WinControls.UI.RadButton
        Me.dtpStart_date = New common.Controls.MyDateTimePicker
        Me.rtxtDescription = New common.Controls.MyTextBox
        Me.rgbWeekDays = New Telerik.WinControls.UI.RadGroupBox
        Me.rchkfriday = New Telerik.WinControls.UI.RadCheckBox
        Me.rchkTuesday = New Telerik.WinControls.UI.RadCheckBox
        Me.rchksaturday = New Telerik.WinControls.UI.RadCheckBox
        Me.rchkThursday = New Telerik.WinControls.UI.RadCheckBox
        Me.rchkwednesday = New Telerik.WinControls.UI.RadCheckBox
        Me.rchkSunday = New Telerik.WinControls.UI.RadCheckBox
        Me.rchkMonday = New Telerik.WinControls.UI.RadCheckBox
        Me.rlblStatus = New common.Controls.MyLabel
        Me.btn_Close = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnActive = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnSave = New Telerik.WinControls.UI.RadButton
        Me.rbtnDelete = New Telerik.WinControls.UI.RadButton
        Me.rbtnclose = New Telerik.WinControls.UI.RadButton
        Me.ToolTipgp_master = New System.Windows.Forms.ToolTip(Me.components)
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem_Import = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem_Export = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem_Close = New Telerik.WinControls.UI.RadMenuItem
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.fndRoute_id = New common.UserControls.txtFinder
        Me.fndGroup_Id = New common.UserControls.txtNavigator
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.gvDB = New common.UserControls.MyRadGridView
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.rlblGroupID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblRouteID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblStart_Date, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStart_date, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgbWeekDays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbWeekDays.SuspendLayout()
        CType(Me.rchkfriday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rchkTuesday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rchksaturday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rchkThursday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rchkwednesday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rchkSunday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rchkMonday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_Close, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rlblGroupID
        '
        Me.rlblGroupID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.rlblGroupID.Location = New System.Drawing.Point(13, 23)
        Me.rlblGroupID.Name = "rlblGroupID"
        Me.rlblGroupID.Size = New System.Drawing.Size(72, 16)
        Me.rlblGroupID.TabIndex = 0
        Me.rlblGroupID.Text = "Group Code"
        '
        'rlblRouteID
        '
        Me.rlblRouteID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblRouteID.Location = New System.Drawing.Point(13, 63)
        Me.rlblRouteID.Name = "rlblRouteID"
        Me.rlblRouteID.Size = New System.Drawing.Size(67, 16)
        Me.rlblRouteID.TabIndex = 1
        Me.rlblRouteID.Text = "Route Code"
        '
        'rlblStart_Date
        '
        Me.rlblStart_Date.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblStart_Date.Location = New System.Drawing.Point(13, 84)
        Me.rlblStart_Date.Name = "rlblStart_Date"
        Me.rlblStart_Date.Size = New System.Drawing.Size(57, 16)
        Me.rlblStart_Date.TabIndex = 2
        Me.rlblStart_Date.Text = "Start Date"
        '
        'rlblDescription
        '
        Me.rlblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblDescription.Location = New System.Drawing.Point(13, 111)
        Me.rlblDescription.Name = "rlblDescription"
        Me.rlblDescription.Size = New System.Drawing.Size(63, 16)
        Me.rlblDescription.TabIndex = 4
        Me.rlblDescription.Text = "Description"
        '
        'rbtnReset
        '
        Me.rbtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnReset.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.rbtnReset.Location = New System.Drawing.Point(306, 21)
        Me.rbtnReset.Name = "rbtnReset"
        Me.rbtnReset.Size = New System.Drawing.Size(14, 18)
        Me.rbtnReset.TabIndex = 1
        '
        'dtpStart_date
        '
        'Me.dtpStart_date.Culture = New System.Globalization.CultureInfo("en-IN")
        Me.dtpStart_date.CustomFormat = "dd/MM/yyyy"
        Me.dtpStart_date.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpStart_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStart_date.Location = New System.Drawing.Point(97, 86)
        Me.dtpStart_date.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpStart_date.MendatroryField = False
        Me.dtpStart_date.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStart_date.MyLinkLable1 = Me.rlblStart_Date
        Me.dtpStart_date.MyLinkLable2 = Nothing
        Me.dtpStart_date.Name = "dtpStart_date"
        Me.dtpStart_date.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStart_date.Size = New System.Drawing.Size(200, 18)
        Me.dtpStart_date.TabIndex = 4
        Me.dtpStart_date.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'rtxtDescription
        '
        Me.rtxtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtDescription.Location = New System.Drawing.Point(97, 109)
        Me.rtxtDescription.MaxLength = 60
        Me.rtxtDescription.MendatroryField = False
        Me.rtxtDescription.Multiline = True
        Me.rtxtDescription.MyLinkLable1 = Me.rlblDescription
        Me.rtxtDescription.MyLinkLable2 = Nothing
        Me.rtxtDescription.Name = "rtxtDescription"
        '
        '
        '
        Me.rtxtDescription.RootElement.StretchVertically = True
        Me.rtxtDescription.Size = New System.Drawing.Size(536, 22)
        Me.rtxtDescription.TabIndex = 5
        '
        'rgbWeekDays
        '
        Me.rgbWeekDays.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbWeekDays.Controls.Add(Me.rchkfriday)
        Me.rgbWeekDays.Controls.Add(Me.rchkTuesday)
        Me.rgbWeekDays.Controls.Add(Me.rchksaturday)
        Me.rgbWeekDays.Controls.Add(Me.rchkThursday)
        Me.rgbWeekDays.Controls.Add(Me.rchkwednesday)
        Me.rgbWeekDays.Controls.Add(Me.rchkSunday)
        Me.rgbWeekDays.Controls.Add(Me.rchkMonday)
        Me.rgbWeekDays.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rgbWeekDays.FooterImageIndex = -1
        Me.rgbWeekDays.FooterImageKey = ""
        Me.rgbWeekDays.HeaderImageIndex = -1
        Me.rgbWeekDays.HeaderImageKey = ""
        Me.rgbWeekDays.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.rgbWeekDays.HeaderText = "Week Days"
        Me.rgbWeekDays.Location = New System.Drawing.Point(370, 8)
        Me.rgbWeekDays.Name = "rgbWeekDays"
        Me.rgbWeekDays.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.rgbWeekDays.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rgbWeekDays.Size = New System.Drawing.Size(263, 98)
        Me.rgbWeekDays.TabIndex = 6
        Me.rgbWeekDays.Text = "Week Days"
        '
        'rchkfriday
        '
        Me.rchkfriday.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rchkfriday.Location = New System.Drawing.Point(24, 58)
        Me.rchkfriday.Name = "rchkfriday"
        Me.rchkfriday.Size = New System.Drawing.Size(52, 16)
        Me.rchkfriday.TabIndex = 4
        Me.rchkfriday.Text = "Friday"
        Me.rchkfriday.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'rchkTuesday
        '
        Me.rchkTuesday.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rchkTuesday.Location = New System.Drawing.Point(157, 19)
        Me.rchkTuesday.Name = "rchkTuesday"
        Me.rchkTuesday.Size = New System.Drawing.Size(64, 16)
        Me.rchkTuesday.TabIndex = 1
        Me.rchkTuesday.Text = "Tuesday"
        Me.rchkTuesday.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'rchksaturday
        '
        Me.rchksaturday.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rchksaturday.Location = New System.Drawing.Point(157, 58)
        Me.rchksaturday.Name = "rchksaturday"
        Me.rchksaturday.Size = New System.Drawing.Size(66, 16)
        Me.rchksaturday.TabIndex = 5
        Me.rchksaturday.Text = "Saturday"
        Me.rchksaturday.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'rchkThursday
        '
        Me.rchkThursday.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rchkThursday.Location = New System.Drawing.Point(157, 39)
        Me.rchkThursday.Name = "rchkThursday"
        Me.rchkThursday.Size = New System.Drawing.Size(67, 16)
        Me.rchkThursday.TabIndex = 3
        Me.rchkThursday.Text = "Thursday"
        Me.rchkThursday.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'rchkwednesday
        '
        Me.rchkwednesday.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rchkwednesday.Location = New System.Drawing.Point(24, 38)
        Me.rchkwednesday.Name = "rchkwednesday"
        Me.rchkwednesday.Size = New System.Drawing.Size(80, 16)
        Me.rchkwednesday.TabIndex = 2
        Me.rchkwednesday.Text = "Wednesday"
        Me.rchkwednesday.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'rchkSunday
        '
        Me.rchkSunday.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rchkSunday.Location = New System.Drawing.Point(24, 78)
        Me.rchkSunday.Name = "rchkSunday"
        Me.rchkSunday.Size = New System.Drawing.Size(59, 16)
        Me.rchkSunday.TabIndex = 6
        Me.rchkSunday.Text = "Sunday"
        Me.rchkSunday.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'rchkMonday
        '
        Me.rchkMonday.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rchkMonday.Location = New System.Drawing.Point(24, 19)
        Me.rchkMonday.Name = "rchkMonday"
        Me.rchkMonday.Size = New System.Drawing.Size(60, 16)
        Me.rchkMonday.TabIndex = 0
        Me.rchkMonday.Text = "Monday"
        Me.rchkMonday.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'rlblStatus
        '
        Me.rlblStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblStatus.Location = New System.Drawing.Point(13, 45)
        Me.rlblStatus.Name = "rlblStatus"
        Me.rlblStatus.Size = New System.Drawing.Size(38, 16)
        Me.rlblStatus.TabIndex = 33
        Me.rlblStatus.Text = "Status"
        '
        'btn_Close
        '
        Me.btn_Close.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Close.Location = New System.Drawing.Point(161, 43)
        Me.btn_Close.Name = "btn_Close"
        Me.btn_Close.Size = New System.Drawing.Size(58, 18)
        Me.btn_Close.TabIndex = 2
        Me.btn_Close.Text = "Close"
        '
        'rbtnActive
        '
        Me.rbtnActive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnActive.Location = New System.Drawing.Point(97, 42)
        Me.rbtnActive.Name = "rbtnActive"
        Me.rbtnActive.Size = New System.Drawing.Size(58, 18)
        Me.rbtnActive.TabIndex = 1
        Me.rbtnActive.Text = "Active"
        '
        'rbtnSave
        '
        Me.rbtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnSave.Location = New System.Drawing.Point(21, 3)
        Me.rbtnSave.Name = "rbtnSave"
        Me.rbtnSave.Size = New System.Drawing.Size(68, 18)
        Me.rbtnSave.TabIndex = 8
        Me.rbtnSave.Text = "Save"
        '
        'rbtnDelete
        '
        Me.rbtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnDelete.Location = New System.Drawing.Point(95, 3)
        Me.rbtnDelete.Name = "rbtnDelete"
        Me.rbtnDelete.Size = New System.Drawing.Size(68, 18)
        Me.rbtnDelete.TabIndex = 9
        Me.rbtnDelete.Text = "Delete"
        '
        'rbtnclose
        '
        Me.rbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnclose.Location = New System.Drawing.Point(578, 3)
        Me.rbtnclose.Name = "rbtnclose"
        Me.rbtnclose.Size = New System.Drawing.Size(68, 18)
        Me.rbtnclose.TabIndex = 10
        Me.rbtnclose.Text = "Close"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Class = ""
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem_Import, Me.RadMenuItem_Export, Me.RadMenuItem_Close})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem_Import
        '
        Me.RadMenuItem_Import.AccessibleDescription = "Import"
        Me.RadMenuItem_Import.AccessibleName = "Import"
        Me.RadMenuItem_Import.Name = "RadMenuItem_Import"
        Me.RadMenuItem_Import.Text = "Import"
        Me.RadMenuItem_Import.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem_Export
        '
        Me.RadMenuItem_Export.AccessibleDescription = "Export"
        Me.RadMenuItem_Export.AccessibleName = "Export"
        Me.RadMenuItem_Export.Name = "RadMenuItem_Export"
        Me.RadMenuItem_Export.Text = "Export"
        Me.RadMenuItem_Export.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem_Close
        '
        Me.RadMenuItem_Close.AccessibleDescription = "Close"
        Me.RadMenuItem_Close.AccessibleName = "Close"
        Me.RadMenuItem_Close.Name = "RadMenuItem_Close"
        Me.RadMenuItem_Close.Text = "Close"
        Me.RadMenuItem_Close.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.fndRoute_id)
        Me.RadGroupBox1.Controls.Add(Me.fndGroup_Id)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.rlblGroupID)
        Me.RadGroupBox1.Controls.Add(Me.rlblRouteID)
        Me.RadGroupBox1.Controls.Add(Me.rlblStart_Date)
        Me.RadGroupBox1.Controls.Add(Me.rlblDescription)
        Me.RadGroupBox1.Controls.Add(Me.rbtnActive)
        Me.RadGroupBox1.Controls.Add(Me.btn_Close)
        Me.RadGroupBox1.Controls.Add(Me.rbtnReset)
        Me.RadGroupBox1.Controls.Add(Me.rlblStatus)
        Me.RadGroupBox1.Controls.Add(Me.rgbWeekDays)
        Me.RadGroupBox1.Controls.Add(Me.rtxtDescription)
        Me.RadGroupBox1.Controls.Add(Me.dtpStart_date)
        Me.RadGroupBox1.FooterImageIndex = -1
        Me.RadGroupBox1.FooterImageKey = ""
        Me.RadGroupBox1.HeaderImageIndex = -1
        Me.RadGroupBox1.HeaderImageKey = ""
        Me.RadGroupBox1.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(13, 16)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox1.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(647, 362)
        Me.RadGroupBox1.TabIndex = 0
        '
        'fndRoute_id
        '
        Me.fndRoute_id.Location = New System.Drawing.Point(97, 63)
        Me.fndRoute_id.MendatroryField = True
        Me.fndRoute_id.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRoute_id.MyLinkLable1 = Me.rlblRouteID
        Me.fndRoute_id.MyLinkLable2 = Nothing
        Me.fndRoute_id.MyReadOnly = False
        Me.fndRoute_id.Name = "fndRoute_id"
        Me.fndRoute_id.Size = New System.Drawing.Size(200, 19)
        Me.fndRoute_id.TabIndex = 3
        Me.fndRoute_id.Value = ""
        '
        'fndGroup_Id
        '
        Me.fndGroup_Id.Location = New System.Drawing.Point(97, 21)
        Me.fndGroup_Id.MendatroryField = True
        Me.fndGroup_Id.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndGroup_Id.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndGroup_Id.MyLinkLable1 = Me.rlblGroupID
        Me.fndGroup_Id.MyLinkLable2 = Nothing
        Me.fndGroup_Id.MyMaxLength = 32767
        Me.fndGroup_Id.MyReadOnly = False
        Me.fndGroup_Id.Name = "fndGroup_Id"
        Me.fndGroup_Id.Size = New System.Drawing.Size(200, 21)
        Me.fndGroup_Id.TabIndex = 0
        Me.fndGroup_Id.Value = ""
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gvDB)
        Me.RadGroupBox4.FooterImageIndex = -1
        Me.RadGroupBox4.FooterImageKey = ""
        Me.RadGroupBox4.HeaderImageIndex = -1
        Me.RadGroupBox4.HeaderImageKey = ""
        Me.RadGroupBox4.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox4.HeaderText = "Replicate In Other Companies"
        Me.RadGroupBox4.Location = New System.Drawing.Point(13, 138)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox4.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(618, 211)
        Me.RadGroupBox4.TabIndex = 7
        Me.RadGroupBox4.Text = "Replicate In Other Companies"
        '
        'gvDB
        '
        Me.gvDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDB.Location = New System.Drawing.Point(10, 20)
        '
        'gvDB
        '
        Me.gvDB.MasterTemplate.AllowAddNewRow = False
        Me.gvDB.Name = "gvDB"
        Me.gvDB.ShowGroupPanel = False
        Me.gvDB.Size = New System.Drawing.Size(598, 181)
        Me.gvDB.TabIndex = 0
        Me.gvDB.Text = "RadGridView1"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(673, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(673, 413)
        Me.SplitContainer1.SplitterDistance = 380
        Me.SplitContainer1.TabIndex = 2
        '
        'frmRouteGroupMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(673, 433)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.KeyPreview = True
        Me.Name = "frmRouteGroupMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Group Master(Route)"
        CType(Me.rlblGroupID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblRouteID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblStart_Date, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStart_date, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgbWeekDays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbWeekDays.ResumeLayout(False)
        Me.rgbWeekDays.PerformLayout()
        CType(Me.rchkfriday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rchkTuesday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rchksaturday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rchkThursday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rchkwednesday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rchkSunday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rchkMonday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_Close, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rbtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpStart_date As common.Controls.MyDateTimePicker
    Friend WithEvents rtxtDescription As common.Controls.MyTextBox
    Friend WithEvents rgbWeekDays As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rchkfriday As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rchkTuesday As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rchksaturday As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rchkThursday As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rchkwednesday As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rchkSunday As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rchkMonday As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btn_Close As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnActive As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents ToolTipgp_master As System.Windows.Forms.ToolTip
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents RadMenuItem_Import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem_Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem_Close As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvDB As common.UserControls.MyRadGridView
    Friend WithEvents rlblGroupID As common.Controls.MyLabel
    Friend WithEvents rlblRouteID As common.Controls.MyLabel
    Friend WithEvents rlblStart_Date As common.Controls.MyLabel
    Friend WithEvents rlblDescription As common.Controls.MyLabel
    Friend WithEvents rlblStatus As common.Controls.MyLabel
    Friend WithEvents fndGroup_Id As common.UserControls.txtNavigator
    Friend WithEvents fndRoute_id As common.UserControls.txtFinder
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

