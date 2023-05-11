<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMilkUnloading
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
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.dtpGateEntryDateTime = New common.Controls.MyDateTimePicker()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.dtpQCDateTime = New common.Controls.MyDateTimePicker()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.dtpWeighmentDateTime = New common.Controls.MyDateTimePicker()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.FndTankerNo = New common.UserControls.txtFinder()
        Me.fndGateEntryNo = New common.UserControls.txtFinder()
        Me.lblSubLocationName = New common.Controls.MyLabel()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblGateEntryNO = New common.Controls.MyLabel()
        Me.fndSubLocation = New common.UserControls.txtFinder()
        Me.txtQCNo = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtLocation = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtWeighmentNo = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.dtpUnloadingDateTime = New common.Controls.MyDateTimePicker()
        Me.lblQCOutDateAndTime = New common.Controls.MyLabel()
        Me.fndUnloadingNo = New common.UserControls.txtNavigator()
        Me.lblQcNo = New common.Controls.MyLabel()
        Me.lblPending = New common.usLock()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvItem = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.dtpGateEntryDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpQCDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpWeighmentDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGateEntryNO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQCNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWeighmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpUnloadingDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQCOutDateAndTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQcNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(214, 19)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(917, 487)
        Me.SplitContainer1.SplitterDistance = 444
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpGateEntryDateTime)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpQCDateTime)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpWeighmentDateTime)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.FndTankerNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndGateEntryNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblSubLocationName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocationName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblGateEntryNO)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndSubLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtQCNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtWeighmentNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpUnloadingDateTime)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblQCOutDateAndTime)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndUnloadingNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblPending)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblQcNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnReset)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer2.Size = New System.Drawing.Size(917, 444)
        Me.SplitContainer2.SplitterDistance = 178
        Me.SplitContainer2.TabIndex = 0
        '
        'dtpGateEntryDateTime
        '
        Me.dtpGateEntryDateTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpGateEntryDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpGateEntryDateTime.Location = New System.Drawing.Point(534, 107)
        Me.dtpGateEntryDateTime.MendatroryField = False
        Me.dtpGateEntryDateTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpGateEntryDateTime.MyLinkLable1 = Nothing
        Me.dtpGateEntryDateTime.MyLinkLable2 = Nothing
        Me.dtpGateEntryDateTime.Name = "dtpGateEntryDateTime"
        Me.dtpGateEntryDateTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpGateEntryDateTime.Size = New System.Drawing.Size(188, 20)
        Me.dtpGateEntryDateTime.TabIndex = 319
        Me.dtpGateEntryDateTime.TabStop = False
        Me.dtpGateEntryDateTime.Text = "10/06/2011 11:51:56 AM"
        Me.dtpGateEntryDateTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel8
        '
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(405, 110)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(116, 16)
        Me.MyLabel8.TabIndex = 320
        Me.MyLabel8.Text = "Gate Entry Date Time"
        '
        'dtpQCDateTime
        '
        Me.dtpQCDateTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpQCDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpQCDateTime.Location = New System.Drawing.Point(534, 81)
        Me.dtpQCDateTime.MendatroryField = False
        Me.dtpQCDateTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpQCDateTime.MyLinkLable1 = Nothing
        Me.dtpQCDateTime.MyLinkLable2 = Nothing
        Me.dtpQCDateTime.Name = "dtpQCDateTime"
        Me.dtpQCDateTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpQCDateTime.Size = New System.Drawing.Size(188, 20)
        Me.dtpQCDateTime.TabIndex = 317
        Me.dtpQCDateTime.TabStop = False
        Me.dtpQCDateTime.Text = "10/06/2011 11:51:56 AM"
        Me.dtpQCDateTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(405, 84)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel7.TabIndex = 318
        Me.MyLabel7.Text = "QC Date Time"
        '
        'dtpWeighmentDateTime
        '
        Me.dtpWeighmentDateTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpWeighmentDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpWeighmentDateTime.Location = New System.Drawing.Point(534, 58)
        Me.dtpWeighmentDateTime.MendatroryField = False
        Me.dtpWeighmentDateTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpWeighmentDateTime.MyLinkLable1 = Nothing
        Me.dtpWeighmentDateTime.MyLinkLable2 = Nothing
        Me.dtpWeighmentDateTime.Name = "dtpWeighmentDateTime"
        Me.dtpWeighmentDateTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpWeighmentDateTime.Size = New System.Drawing.Size(188, 20)
        Me.dtpWeighmentDateTime.TabIndex = 315
        Me.dtpWeighmentDateTime.TabStop = False
        Me.dtpWeighmentDateTime.Text = "10/06/2011 11:51:56 AM"
        Me.dtpWeighmentDateTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel6
        '
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(405, 61)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(119, 16)
        Me.MyLabel6.TabIndex = 316
        Me.MyLabel6.Text = "Weighment Date Time"
        '
        'FndTankerNo
        '
        Me.FndTankerNo.Location = New System.Drawing.Point(113, 36)
        Me.FndTankerNo.MendatroryField = True
        Me.FndTankerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndTankerNo.MyLinkLable1 = Nothing
        Me.FndTankerNo.MyLinkLable2 = Nothing
        Me.FndTankerNo.MyReadOnly = False
        Me.FndTankerNo.MyShowMasterFormButton = False
        Me.FndTankerNo.Name = "FndTankerNo"
        Me.FndTankerNo.Size = New System.Drawing.Size(608, 19)
        Me.FndTankerNo.TabIndex = 314
        Me.FndTankerNo.Value = ""
        '
        'fndGateEntryNo
        '
        Me.fndGateEntryNo.Location = New System.Drawing.Point(113, 107)
        Me.fndGateEntryNo.MendatroryField = True
        Me.fndGateEntryNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndGateEntryNo.MyLinkLable1 = Nothing
        Me.fndGateEntryNo.MyLinkLable2 = Nothing
        Me.fndGateEntryNo.MyReadOnly = True
        Me.fndGateEntryNo.MyShowMasterFormButton = False
        Me.fndGateEntryNo.Name = "fndGateEntryNo"
        Me.fndGateEntryNo.Size = New System.Drawing.Size(287, 19)
        Me.fndGateEntryNo.TabIndex = 2
        Me.fndGateEntryNo.Value = ""
        '
        'lblSubLocationName
        '
        Me.lblSubLocationName.AutoSize = False
        Me.lblSubLocationName.BorderVisible = True
        Me.lblSubLocationName.Location = New System.Drawing.Point(405, 153)
        Me.lblSubLocationName.Name = "lblSubLocationName"
        Me.lblSubLocationName.Size = New System.Drawing.Size(316, 19)
        Me.lblSubLocationName.TabIndex = 312
        Me.lblSubLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.Location = New System.Drawing.Point(405, 132)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(316, 19)
        Me.lblLocationName.TabIndex = 313
        Me.lblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(13, 36)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel5.TabIndex = 310
        Me.MyLabel5.Text = "Tanker No."
        '
        'lblGateEntryNO
        '
        Me.lblGateEntryNO.Location = New System.Drawing.Point(13, 154)
        Me.lblGateEntryNO.Name = "lblGateEntryNO"
        Me.lblGateEntryNO.Size = New System.Drawing.Size(43, 18)
        Me.lblGateEntryNO.TabIndex = 309
        Me.lblGateEntryNO.Text = "Silo No"
        '
        'fndSubLocation
        '
        Me.fndSubLocation.Location = New System.Drawing.Point(113, 153)
        Me.fndSubLocation.MendatroryField = True
        Me.fndSubLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSubLocation.MyLinkLable1 = Nothing
        Me.fndSubLocation.MyLinkLable2 = Nothing
        Me.fndSubLocation.MyReadOnly = False
        Me.fndSubLocation.MyShowMasterFormButton = False
        Me.fndSubLocation.Name = "fndSubLocation"
        Me.fndSubLocation.Size = New System.Drawing.Size(288, 19)
        Me.fndSubLocation.TabIndex = 7
        Me.fndSubLocation.Value = ""
        '
        'txtQCNo
        '
        Me.txtQCNo.Location = New System.Drawing.Point(113, 82)
        Me.txtQCNo.MaxLength = 50
        Me.txtQCNo.MendatroryField = False
        Me.txtQCNo.MyLinkLable1 = Nothing
        Me.txtQCNo.MyLinkLable2 = Nothing
        Me.txtQCNo.Name = "txtQCNo"
        Me.txtQCNo.ReadOnly = True
        Me.txtQCNo.Size = New System.Drawing.Size(288, 20)
        Me.txtQCNo.TabIndex = 4
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(13, 85)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel3.TabIndex = 306
        Me.MyLabel3.Text = "QC No."
        '
        'txtLocation
        '
        Me.txtLocation.Location = New System.Drawing.Point(113, 130)
        Me.txtLocation.MaxLength = 50
        Me.txtLocation.MendatroryField = False
        Me.txtLocation.MyLinkLable1 = Nothing
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReadOnly = True
        Me.txtLocation.Size = New System.Drawing.Size(288, 20)
        Me.txtLocation.TabIndex = 6
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(13, 132)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel4.TabIndex = 304
        Me.MyLabel4.Text = "Location"
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(13, 107)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(81, 16)
        Me.MyLabel1.TabIndex = 302
        Me.MyLabel1.Text = "Gate Entry No."
        '
        'txtWeighmentNo
        '
        Me.txtWeighmentNo.Location = New System.Drawing.Point(113, 58)
        Me.txtWeighmentNo.MaxLength = 50
        Me.txtWeighmentNo.MendatroryField = False
        Me.txtWeighmentNo.MyLinkLable1 = Nothing
        Me.txtWeighmentNo.MyLinkLable2 = Nothing
        Me.txtWeighmentNo.Name = "txtWeighmentNo"
        Me.txtWeighmentNo.ReadOnly = True
        Me.txtWeighmentNo.Size = New System.Drawing.Size(287, 20)
        Me.txtWeighmentNo.TabIndex = 3
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(13, 62)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(81, 16)
        Me.MyLabel2.TabIndex = 300
        Me.MyLabel2.Text = "Weighment No"
        '
        'dtpUnloadingDateTime
        '
        Me.dtpUnloadingDateTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpUnloadingDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUnloadingDateTime.Location = New System.Drawing.Point(534, 14)
        Me.dtpUnloadingDateTime.MendatroryField = False
        Me.dtpUnloadingDateTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpUnloadingDateTime.MyLinkLable1 = Nothing
        Me.dtpUnloadingDateTime.MyLinkLable2 = Nothing
        Me.dtpUnloadingDateTime.Name = "dtpUnloadingDateTime"
        Me.dtpUnloadingDateTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpUnloadingDateTime.Size = New System.Drawing.Size(188, 20)
        Me.dtpUnloadingDateTime.TabIndex = 1
        Me.dtpUnloadingDateTime.TabStop = False
        Me.dtpUnloadingDateTime.Text = "10/06/2011 11:51:56 AM"
        Me.dtpUnloadingDateTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblQCOutDateAndTime
        '
        Me.lblQCOutDateAndTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQCOutDateAndTime.Location = New System.Drawing.Point(405, 17)
        Me.lblQCOutDateAndTime.Name = "lblQCOutDateAndTime"
        Me.lblQCOutDateAndTime.Size = New System.Drawing.Size(113, 16)
        Me.lblQCOutDateAndTime.TabIndex = 291
        Me.lblQCOutDateAndTime.Text = "Unloading Date Time"
        '
        'fndUnloadingNo
        '
        Me.fndUnloadingNo.Location = New System.Drawing.Point(113, 15)
        Me.fndUnloadingNo.MendatroryField = False
        Me.fndUnloadingNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndUnloadingNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndUnloadingNo.MyLinkLable1 = Me.lblQcNo
        Me.fndUnloadingNo.MyLinkLable2 = Nothing
        Me.fndUnloadingNo.MyMaxLength = 32767
        Me.fndUnloadingNo.MyReadOnly = False
        Me.fndUnloadingNo.Name = "fndUnloadingNo"
        Me.fndUnloadingNo.Size = New System.Drawing.Size(266, 18)
        Me.fndUnloadingNo.TabIndex = 0
        Me.fndUnloadingNo.Value = ""
        '
        'lblQcNo
        '
        Me.lblQcNo.Location = New System.Drawing.Point(13, 15)
        Me.lblQcNo.Name = "lblQcNo"
        Me.lblQcNo.Size = New System.Drawing.Size(82, 18)
        Me.lblQcNo.TabIndex = 255
        Me.lblQcNo.Text = "Unloading No. "
        '
        'lblPending
        '
        Me.lblPending.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(732, 13)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(97, 20)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 253
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = My.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(382, 16)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(19, 18)
        Me.btnReset.TabIndex = 254
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gvItem)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = "Item Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(917, 262)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "Item Details"
        '
        'gvItem
        '
        Me.gvItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvItem.Location = New System.Drawing.Point(2, 18)
        Me.gvItem.Name = "gvItem"
        Me.gvItem.Size = New System.Drawing.Size(913, 242)
        Me.gvItem.TabIndex = 0
        Me.gvItem.Text = "RadGridView1"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(846, 19)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(144, 19)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 18)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 18)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(73, 19)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'FrmMilkUnloading
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(917, 487)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmMilkUnloading"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmMilkUnloading"
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.dtpGateEntryDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpQCDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpWeighmentDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGateEntryNO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQCNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWeighmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpUnloadingDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQCOutDateAndTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQcNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndUnloadingNo As common.UserControls.txtNavigator
    Friend WithEvents lblQcNo As common.Controls.MyLabel
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents dtpUnloadingDateTime As common.Controls.MyDateTimePicker
    Friend WithEvents lblQCOutDateAndTime As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtWeighmentNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtQCNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblGateEntryNO As common.Controls.MyLabel
    Friend WithEvents fndSubLocation As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvItem As common.UserControls.MyRadGridView
    Friend WithEvents lblSubLocationName As common.Controls.MyLabel
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents fndGateEntryNo As common.UserControls.txtFinder
    Friend WithEvents FndTankerNo As common.UserControls.txtFinder
    Friend WithEvents dtpGateEntryDateTime As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents dtpQCDateTime As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents dtpWeighmentDateTime As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
End Class

