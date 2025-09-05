<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBulkRoutMaster
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.rdmenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.File = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GVLocatiom = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.lblScheduleTime = New common.Controls.MyLabel()
        Me.txtRouteNo = New common.UserControls.txtNavigator()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtRouteName = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtScheduleTime = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtDistance = New common.MyNumBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtScheduleTimeE = New common.Controls.MyDateTimePicker()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtRate = New common.MyNumBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtScheduleTimeM = New common.Controls.MyDateTimePicker()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.lblToLocation = New common.Controls.MyLabel()
        Me.txtVehicleNo = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.CuttOffTime = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtTankerNo = New common.UserControls.txtFinder()
        Me.txtWeight = New common.MyNumBox()
        Me.txtTollAmount = New common.MyNumBox()
        Me.txtcuttofftime = New common.Controls.MyDateTimePicker()
        Me.lblToLocationName = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtToLocationCode = New common.UserControls.txtFinder()
        Me.chkForContractor = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtMCC = New common.UserControls.txtMultiSelectFinder()
        Me.txtRouteNameHindi = New common.Controls.MyTextBox()
        Me.lblAmout = New common.Controls.MyLabel()
        Me.chkDefault = New Telerik.WinControls.UI.RadCheckBox()
        Me.GBRoute = New System.Windows.Forms.GroupBox()
        Me.txtRoute = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel28 = New common.Controls.MyLabel()
        Me.lblLength = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton()
        Me.rdbtndelete = New Telerik.WinControls.UI.RadButton()
        Me.lblZone = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtZone = New common.UserControls.txtFinder()
        CType(Me.rdmenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.GVLocatiom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GVLocatiom.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblScheduleTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRouteName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtScheduleTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDistance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtScheduleTimeE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtScheduleTimeM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CuttOffTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTollAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcuttofftime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkForContractor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRouteNameHindi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmout, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDefault, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBRoute.SuspendLayout()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLength, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblZone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdmenu1
        '
        Me.rdmenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.File})
        Me.rdmenu1.Location = New System.Drawing.Point(0, 0)
        Me.rdmenu1.Name = "rdmenu1"
        Me.rdmenu1.Size = New System.Drawing.Size(772, 20)
        Me.rdmenu1.TabIndex = 2
        '
        'File
        '
        Me.File.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2, Me.RadMenuItem3})
        Me.File.Name = "File"
        Me.File.Text = "File"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Import"
        Me.RadMenuItem1.AccessibleName = "Import"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Export"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Export"
        Me.RadMenuItem2.AccessibleName = "Export"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Import"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Exit"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GVLocatiom)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(772, 454)
        Me.SplitContainer1.SplitterDistance = 419
        Me.SplitContainer1.TabIndex = 3
        '
        'GVLocatiom
        '
        Me.GVLocatiom.Controls.Add(Me.RadPageViewPage1)
        Me.GVLocatiom.Controls.Add(Me.RadPageViewPage2)
        Me.GVLocatiom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GVLocatiom.Location = New System.Drawing.Point(0, 0)
        Me.GVLocatiom.Name = "GVLocatiom"
        Me.GVLocatiom.SelectedPage = Me.RadPageViewPage1
        Me.GVLocatiom.Size = New System.Drawing.Size(772, 419)
        Me.GVLocatiom.TabIndex = 12153
        CType(Me.GVLocatiom.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.GBRoute)
        Me.RadPageViewPage1.Controls.Add(Me.lblLength)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(55.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(751, 371)
        Me.RadPageViewPage1.Text = "General"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.RadGroupBox2.Controls.Add(Me.lblZone)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel10)
        Me.RadGroupBox2.Controls.Add(Me.txtZone)
        Me.RadGroupBox2.Controls.Add(Me.lblScheduleTime)
        Me.RadGroupBox2.Controls.Add(Me.txtRouteNo)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel19)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox2.Controls.Add(Me.txtRouteName)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox2.Controls.Add(Me.txtScheduleTime)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox2.Controls.Add(Me.txtDistance)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox2.Controls.Add(Me.txtScheduleTimeE)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox2.Controls.Add(Me.txtRate)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel11)
        Me.RadGroupBox2.Controls.Add(Me.txtScheduleTimeM)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel16)
        Me.RadGroupBox2.Controls.Add(Me.btnNew)
        Me.RadGroupBox2.Controls.Add(Me.lblToLocation)
        Me.RadGroupBox2.Controls.Add(Me.txtVehicleNo)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox2.Controls.Add(Me.CuttOffTime)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox2.Controls.Add(Me.txtTankerNo)
        Me.RadGroupBox2.Controls.Add(Me.txtWeight)
        Me.RadGroupBox2.Controls.Add(Me.txtTollAmount)
        Me.RadGroupBox2.Controls.Add(Me.txtcuttofftime)
        Me.RadGroupBox2.Controls.Add(Me.lblToLocationName)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox2.Controls.Add(Me.txtToLocationCode)
        Me.RadGroupBox2.Controls.Add(Me.chkForContractor)
        Me.RadGroupBox2.Controls.Add(Me.txtMCC)
        Me.RadGroupBox2.Controls.Add(Me.txtRouteNameHindi)
        Me.RadGroupBox2.Controls.Add(Me.lblAmout)
        Me.RadGroupBox2.Controls.Add(Me.chkDefault)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(751, 371)
        Me.RadGroupBox2.TabIndex = 0
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(19, 8)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(54, 16)
        Me.RadLabel1.TabIndex = 39
        Me.RadLabel1.Text = "Route No"
        '
        'lblScheduleTime
        '
        Me.lblScheduleTime.FieldName = Nothing
        Me.lblScheduleTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScheduleTime.Location = New System.Drawing.Point(19, 303)
        Me.lblScheduleTime.Name = "lblScheduleTime"
        Me.lblScheduleTime.Size = New System.Drawing.Size(82, 16)
        Me.lblScheduleTime.TabIndex = 12153
        Me.lblScheduleTime.Text = "Schedule Time"
        '
        'txtRouteNo
        '
        Me.txtRouteNo.FieldName = Nothing
        Me.txtRouteNo.Location = New System.Drawing.Point(153, 7)
        Me.txtRouteNo.MendatroryField = False
        Me.txtRouteNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtRouteNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtRouteNo.MyLinkLable1 = Me.RadLabel1
        Me.txtRouteNo.MyLinkLable2 = Nothing
        Me.txtRouteNo.MyMaxLength = 30
        Me.txtRouteNo.MyReadOnly = False
        Me.txtRouteNo.Name = "txtRouteNo"
        Me.txtRouteNo.Size = New System.Drawing.Size(252, 19)
        Me.txtRouteNo.TabIndex = 1437
        Me.txtRouteNo.Value = ""
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(19, 30)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel19.TabIndex = 1421
        Me.MyLabel19.Text = "Route Name"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(19, 52)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(50, 16)
        Me.MyLabel1.TabIndex = 1422
        Me.MyLabel1.Text = "Distance"
        '
        'txtRouteName
        '
        Me.txtRouteName.CalculationExpression = Nothing
        Me.txtRouteName.FieldCode = Nothing
        Me.txtRouteName.FieldDesc = Nothing
        Me.txtRouteName.FieldMaxLength = 0
        Me.txtRouteName.FieldName = Nothing
        Me.txtRouteName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteName.isCalculatedField = False
        Me.txtRouteName.IsSourceFromTable = False
        Me.txtRouteName.IsSourceFromValueList = False
        Me.txtRouteName.IsUnique = False
        Me.txtRouteName.Location = New System.Drawing.Point(153, 29)
        Me.txtRouteName.MaxLength = 30
        Me.txtRouteName.MendatroryField = False
        Me.txtRouteName.MyLinkLable1 = Me.MyLabel19
        Me.txtRouteName.MyLinkLable2 = Nothing
        Me.txtRouteName.Name = "txtRouteName"
        Me.txtRouteName.ReferenceFieldDesc = Nothing
        Me.txtRouteName.ReferenceFieldName = Nothing
        Me.txtRouteName.ReferenceTableName = Nothing
        Me.txtRouteName.Size = New System.Drawing.Size(270, 18)
        Me.txtRouteName.TabIndex = 1420
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(19, 75)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel2.TabIndex = 1423
        Me.MyLabel2.Text = "Rate"
        '
        'txtScheduleTime
        '
        Me.txtScheduleTime.CalculationExpression = Nothing
        Me.txtScheduleTime.CustomFormat = "hh:mm tt"
        Me.txtScheduleTime.FieldCode = Nothing
        Me.txtScheduleTime.FieldDesc = Nothing
        Me.txtScheduleTime.FieldMaxLength = 0
        Me.txtScheduleTime.FieldName = Nothing
        Me.txtScheduleTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtScheduleTime.isCalculatedField = False
        Me.txtScheduleTime.IsSourceFromTable = False
        Me.txtScheduleTime.IsSourceFromValueList = False
        Me.txtScheduleTime.IsUnique = False
        Me.txtScheduleTime.Location = New System.Drawing.Point(153, 301)
        Me.txtScheduleTime.MendatroryField = False
        Me.txtScheduleTime.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.txtScheduleTime.MyLinkLable1 = Nothing
        Me.txtScheduleTime.MyLinkLable2 = Nothing
        Me.txtScheduleTime.Name = "txtScheduleTime"
        Me.txtScheduleTime.ReferenceFieldDesc = Nothing
        Me.txtScheduleTime.ReferenceFieldName = Nothing
        Me.txtScheduleTime.ReferenceTableName = Nothing
        Me.txtScheduleTime.Size = New System.Drawing.Size(153, 20)
        Me.txtScheduleTime.TabIndex = 12152
        Me.txtScheduleTime.TabStop = False
        Me.txtScheduleTime.Text = "02:13 PM"
        Me.txtScheduleTime.Value = New Date(2023, 11, 27, 14, 13, 0, 0)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(19, 120)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel3.TabIndex = 1424
        Me.MyLabel3.Text = "Amount"
        '
        'txtDistance
        '
        Me.txtDistance.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtDistance.CalculationExpression = Nothing
        Me.txtDistance.DecimalPlaces = 0
        Me.txtDistance.FieldCode = Nothing
        Me.txtDistance.FieldDesc = Nothing
        Me.txtDistance.FieldMaxLength = 0
        Me.txtDistance.FieldName = Nothing
        Me.txtDistance.isCalculatedField = False
        Me.txtDistance.IsSourceFromTable = False
        Me.txtDistance.IsSourceFromValueList = False
        Me.txtDistance.IsUnique = False
        Me.txtDistance.Location = New System.Drawing.Point(153, 50)
        Me.txtDistance.MendatroryField = False
        Me.txtDistance.MyLinkLable1 = Me.MyLabel1
        Me.txtDistance.MyLinkLable2 = Nothing
        Me.txtDistance.Name = "txtDistance"
        Me.txtDistance.ReferenceFieldDesc = Nothing
        Me.txtDistance.ReferenceFieldName = Nothing
        Me.txtDistance.ReferenceTableName = Nothing
        Me.txtDistance.Size = New System.Drawing.Size(155, 20)
        Me.txtDistance.TabIndex = 1435
        Me.txtDistance.Text = "0"
        Me.txtDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDistance.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(19, 280)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(126, 16)
        Me.MyLabel8.TabIndex = 12149
        Me.MyLabel8.Text = "Schedule Time Morning"
        '
        'txtScheduleTimeE
        '
        Me.txtScheduleTimeE.CalculationExpression = Nothing
        Me.txtScheduleTimeE.CustomFormat = "hh:mm tt"
        Me.txtScheduleTimeE.FieldCode = Nothing
        Me.txtScheduleTimeE.FieldDesc = Nothing
        Me.txtScheduleTimeE.FieldMaxLength = 0
        Me.txtScheduleTimeE.FieldName = Nothing
        Me.txtScheduleTimeE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtScheduleTimeE.isCalculatedField = False
        Me.txtScheduleTimeE.IsSourceFromTable = False
        Me.txtScheduleTimeE.IsSourceFromValueList = False
        Me.txtScheduleTimeE.IsUnique = False
        Me.txtScheduleTimeE.Location = New System.Drawing.Point(153, 278)
        Me.txtScheduleTimeE.MendatroryField = False
        Me.txtScheduleTimeE.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.txtScheduleTimeE.MyLinkLable1 = Nothing
        Me.txtScheduleTimeE.MyLinkLable2 = Nothing
        Me.txtScheduleTimeE.Name = "txtScheduleTimeE"
        Me.txtScheduleTimeE.ReferenceFieldDesc = Nothing
        Me.txtScheduleTimeE.ReferenceFieldName = Nothing
        Me.txtScheduleTimeE.ReferenceTableName = Nothing
        Me.txtScheduleTimeE.Size = New System.Drawing.Size(153, 20)
        Me.txtScheduleTimeE.TabIndex = 12151
        Me.txtScheduleTimeE.TabStop = False
        Me.txtScheduleTimeE.Text = "02:13 PM"
        Me.txtScheduleTimeE.Value = New Date(2023, 11, 27, 14, 13, 0, 0)
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(19, 98)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel4.TabIndex = 1441
        Me.MyLabel4.Text = "Weight"
        '
        'txtRate
        '
        Me.txtRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtRate.CalculationExpression = Nothing
        Me.txtRate.DecimalPlaces = 2
        Me.txtRate.FieldCode = Nothing
        Me.txtRate.FieldDesc = Nothing
        Me.txtRate.FieldMaxLength = 0
        Me.txtRate.FieldName = Nothing
        Me.txtRate.isCalculatedField = False
        Me.txtRate.IsSourceFromTable = False
        Me.txtRate.IsSourceFromValueList = False
        Me.txtRate.IsUnique = False
        Me.txtRate.Location = New System.Drawing.Point(153, 73)
        Me.txtRate.MendatroryField = False
        Me.txtRate.MyLinkLable1 = Me.MyLabel2
        Me.txtRate.MyLinkLable2 = Nothing
        Me.txtRate.Name = "txtRate"
        Me.txtRate.ReferenceFieldDesc = Nothing
        Me.txtRate.ReferenceFieldName = Nothing
        Me.txtRate.ReferenceTableName = Nothing
        Me.txtRate.Size = New System.Drawing.Size(155, 20)
        Me.txtRate.TabIndex = 1436
        Me.txtRate.Text = "0"
        Me.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRate.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(19, 257)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(126, 16)
        Me.MyLabel11.TabIndex = 12147
        Me.MyLabel11.Text = "Schedule Time Morning"
        '
        'txtScheduleTimeM
        '
        Me.txtScheduleTimeM.CalculationExpression = Nothing
        Me.txtScheduleTimeM.CustomFormat = "hh:mm tt"
        Me.txtScheduleTimeM.FieldCode = Nothing
        Me.txtScheduleTimeM.FieldDesc = Nothing
        Me.txtScheduleTimeM.FieldMaxLength = 0
        Me.txtScheduleTimeM.FieldName = Nothing
        Me.txtScheduleTimeM.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtScheduleTimeM.isCalculatedField = False
        Me.txtScheduleTimeM.IsSourceFromTable = False
        Me.txtScheduleTimeM.IsSourceFromValueList = False
        Me.txtScheduleTimeM.IsUnique = False
        Me.txtScheduleTimeM.Location = New System.Drawing.Point(153, 255)
        Me.txtScheduleTimeM.MendatroryField = False
        Me.txtScheduleTimeM.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.txtScheduleTimeM.MyLinkLable1 = Nothing
        Me.txtScheduleTimeM.MyLinkLable2 = Nothing
        Me.txtScheduleTimeM.Name = "txtScheduleTimeM"
        Me.txtScheduleTimeM.ReferenceFieldDesc = Nothing
        Me.txtScheduleTimeM.ReferenceFieldName = Nothing
        Me.txtScheduleTimeM.ReferenceTableName = Nothing
        Me.txtScheduleTimeM.Size = New System.Drawing.Size(154, 20)
        Me.txtScheduleTimeM.TabIndex = 12150
        Me.txtScheduleTimeM.TabStop = False
        Me.txtScheduleTimeM.Text = "02:13 PM"
        Me.txtScheduleTimeM.Value = New Date(2023, 11, 27, 14, 13, 0, 0)
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(19, 140)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel16.TabIndex = 1447
        Me.MyLabel16.Text = "MCC"
        '
        'btnNew
        '
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(404, 7)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(21, 19)
        Me.btnNew.TabIndex = 1438
        '
        'lblToLocation
        '
        Me.lblToLocation.FieldName = Nothing
        Me.lblToLocation.Location = New System.Drawing.Point(19, 163)
        Me.lblToLocation.Name = "lblToLocation"
        Me.lblToLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblToLocation.TabIndex = 12134
        Me.lblToLocation.Text = "Location"
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.AutoSize = False
        Me.txtVehicleNo.BorderVisible = True
        Me.txtVehicleNo.FieldName = Nothing
        Me.txtVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleNo.Location = New System.Drawing.Point(311, 186)
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.Size = New System.Drawing.Size(300, 21)
        Me.txtVehicleNo.TabIndex = 12143
        Me.txtVehicleNo.TextWrap = False
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(19, 234)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel6.TabIndex = 12137
        Me.MyLabel6.Text = "Toll Amount"
        '
        'CuttOffTime
        '
        Me.CuttOffTime.FieldName = Nothing
        Me.CuttOffTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CuttOffTime.Location = New System.Drawing.Point(427, 52)
        Me.CuttOffTime.Name = "CuttOffTime"
        Me.CuttOffTime.Size = New System.Drawing.Size(74, 16)
        Me.CuttOffTime.TabIndex = 12145
        Me.CuttOffTime.Text = "Cutt Off Time"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(19, 187)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel7.TabIndex = 12142
        Me.MyLabel7.Text = "Tanker No"
        '
        'txtTankerNo
        '
        Me.txtTankerNo.CalculationExpression = Nothing
        Me.txtTankerNo.FieldCode = Nothing
        Me.txtTankerNo.FieldDesc = Nothing
        Me.txtTankerNo.FieldMaxLength = 0
        Me.txtTankerNo.FieldName = Nothing
        Me.txtTankerNo.isCalculatedField = False
        Me.txtTankerNo.IsSourceFromTable = False
        Me.txtTankerNo.IsSourceFromValueList = False
        Me.txtTankerNo.IsUnique = False
        Me.txtTankerNo.Location = New System.Drawing.Point(153, 186)
        Me.txtTankerNo.MendatroryField = True
        Me.txtTankerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTankerNo.MyLinkLable1 = Me.MyLabel7
        Me.txtTankerNo.MyLinkLable2 = Nothing
        Me.txtTankerNo.MyReadOnly = False
        Me.txtTankerNo.MyShowMasterFormButton = False
        Me.txtTankerNo.Name = "txtTankerNo"
        Me.txtTankerNo.ReferenceFieldDesc = Nothing
        Me.txtTankerNo.ReferenceFieldName = Nothing
        Me.txtTankerNo.ReferenceTableName = Nothing
        Me.txtTankerNo.Size = New System.Drawing.Size(155, 20)
        Me.txtTankerNo.TabIndex = 12141
        Me.txtTankerNo.Value = ""
        '
        'txtWeight
        '
        Me.txtWeight.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtWeight.CalculationExpression = Nothing
        Me.txtWeight.DecimalPlaces = 2
        Me.txtWeight.FieldCode = Nothing
        Me.txtWeight.FieldDesc = Nothing
        Me.txtWeight.FieldMaxLength = 0
        Me.txtWeight.FieldName = Nothing
        Me.txtWeight.isCalculatedField = False
        Me.txtWeight.IsSourceFromTable = False
        Me.txtWeight.IsSourceFromValueList = False
        Me.txtWeight.IsUnique = False
        Me.txtWeight.Location = New System.Drawing.Point(153, 96)
        Me.txtWeight.MendatroryField = False
        Me.txtWeight.MyLinkLable1 = Me.MyLabel4
        Me.txtWeight.MyLinkLable2 = Nothing
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.ReferenceFieldDesc = Nothing
        Me.txtWeight.ReferenceFieldName = Nothing
        Me.txtWeight.ReferenceTableName = Nothing
        Me.txtWeight.Size = New System.Drawing.Size(155, 20)
        Me.txtWeight.TabIndex = 1443
        Me.txtWeight.Text = "0"
        Me.txtWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtWeight.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'txtTollAmount
        '
        Me.txtTollAmount.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTollAmount.CalculationExpression = Nothing
        Me.txtTollAmount.DecimalPlaces = 0
        Me.txtTollAmount.FieldCode = Nothing
        Me.txtTollAmount.FieldDesc = Nothing
        Me.txtTollAmount.FieldMaxLength = 0
        Me.txtTollAmount.FieldName = Nothing
        Me.txtTollAmount.isCalculatedField = False
        Me.txtTollAmount.IsSourceFromTable = False
        Me.txtTollAmount.IsSourceFromValueList = False
        Me.txtTollAmount.IsUnique = False
        Me.txtTollAmount.Location = New System.Drawing.Point(153, 232)
        Me.txtTollAmount.MendatroryField = False
        Me.txtTollAmount.MyLinkLable1 = Me.MyLabel6
        Me.txtTollAmount.MyLinkLable2 = Nothing
        Me.txtTollAmount.Name = "txtTollAmount"
        Me.txtTollAmount.ReferenceFieldDesc = Nothing
        Me.txtTollAmount.ReferenceFieldName = Nothing
        Me.txtTollAmount.ReferenceTableName = Nothing
        Me.txtTollAmount.Size = New System.Drawing.Size(155, 20)
        Me.txtTollAmount.TabIndex = 12138
        Me.txtTollAmount.Text = "0"
        Me.txtTollAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTollAmount.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'txtcuttofftime
        '
        Me.txtcuttofftime.CalculationExpression = Nothing
        Me.txtcuttofftime.CustomFormat = "dd/MM/yyyy"
        Me.txtcuttofftime.FieldCode = Nothing
        Me.txtcuttofftime.FieldDesc = Nothing
        Me.txtcuttofftime.FieldMaxLength = 0
        Me.txtcuttofftime.FieldName = Nothing
        Me.txtcuttofftime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcuttofftime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtcuttofftime.isCalculatedField = False
        Me.txtcuttofftime.IsSourceFromTable = False
        Me.txtcuttofftime.IsSourceFromValueList = False
        Me.txtcuttofftime.IsUnique = False
        Me.txtcuttofftime.Location = New System.Drawing.Point(505, 51)
        Me.txtcuttofftime.MendatroryField = False
        Me.txtcuttofftime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtcuttofftime.MyLinkLable1 = Me.CuttOffTime
        Me.txtcuttofftime.MyLinkLable2 = Nothing
        Me.txtcuttofftime.Name = "txtcuttofftime"
        Me.txtcuttofftime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtcuttofftime.ReferenceFieldDesc = Nothing
        Me.txtcuttofftime.ReferenceFieldName = Nothing
        Me.txtcuttofftime.ReferenceTableName = Nothing
        Me.txtcuttofftime.Size = New System.Drawing.Size(81, 18)
        Me.txtcuttofftime.TabIndex = 12144
        Me.txtcuttofftime.TabStop = False
        Me.txtcuttofftime.Text = "17/05/2011"
        Me.txtcuttofftime.Value = New Date(2011, 5, 17, 15, 2, 19, 281)
        '
        'lblToLocationName
        '
        Me.lblToLocationName.AutoSize = False
        Me.lblToLocationName.BorderVisible = True
        Me.lblToLocationName.FieldName = Nothing
        Me.lblToLocationName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToLocationName.Location = New System.Drawing.Point(311, 162)
        Me.lblToLocationName.Name = "lblToLocationName"
        Me.lblToLocationName.Size = New System.Drawing.Size(300, 21)
        Me.lblToLocationName.TabIndex = 12136
        Me.lblToLocationName.TextWrap = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(308, 98)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(20, 16)
        Me.MyLabel5.TabIndex = 1444
        Me.MyLabel5.Text = "Kg"
        '
        'txtToLocationCode
        '
        Me.txtToLocationCode.CalculationExpression = Nothing
        Me.txtToLocationCode.FieldCode = Nothing
        Me.txtToLocationCode.FieldDesc = Nothing
        Me.txtToLocationCode.FieldMaxLength = 0
        Me.txtToLocationCode.FieldName = Nothing
        Me.txtToLocationCode.isCalculatedField = False
        Me.txtToLocationCode.IsSourceFromTable = False
        Me.txtToLocationCode.IsSourceFromValueList = False
        Me.txtToLocationCode.IsUnique = False
        Me.txtToLocationCode.Location = New System.Drawing.Point(153, 162)
        Me.txtToLocationCode.MendatroryField = True
        Me.txtToLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToLocationCode.MyLinkLable1 = Me.MyLabel3
        Me.txtToLocationCode.MyLinkLable2 = Nothing
        Me.txtToLocationCode.MyReadOnly = False
        Me.txtToLocationCode.MyShowMasterFormButton = False
        Me.txtToLocationCode.Name = "txtToLocationCode"
        Me.txtToLocationCode.ReferenceFieldDesc = Nothing
        Me.txtToLocationCode.ReferenceFieldName = Nothing
        Me.txtToLocationCode.ReferenceTableName = Nothing
        Me.txtToLocationCode.Size = New System.Drawing.Size(155, 21)
        Me.txtToLocationCode.TabIndex = 12135
        Me.txtToLocationCode.Value = ""
        '
        'chkForContractor
        '
        Me.chkForContractor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkForContractor.Location = New System.Drawing.Point(311, 52)
        Me.chkForContractor.Name = "chkForContractor"
        Me.chkForContractor.Size = New System.Drawing.Size(93, 16)
        Me.chkForContractor.TabIndex = 12133
        Me.chkForContractor.Text = "For Contractor"
        '
        'txtMCC
        '
        Me.txtMCC.arrDispalyMember = Nothing
        Me.txtMCC.arrValueMember = Nothing
        Me.txtMCC.Location = New System.Drawing.Point(153, 140)
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Me.MyLabel16
        Me.txtMCC.MyLinkLable2 = Nothing
        Me.txtMCC.MyNullText = "All"
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.Size = New System.Drawing.Size(458, 19)
        Me.txtMCC.TabIndex = 1446
        '
        'txtRouteNameHindi
        '
        Me.txtRouteNameHindi.CalculationExpression = Nothing
        Me.txtRouteNameHindi.FieldCode = Nothing
        Me.txtRouteNameHindi.FieldDesc = Nothing
        Me.txtRouteNameHindi.FieldMaxLength = 0
        Me.txtRouteNameHindi.FieldName = Nothing
        Me.txtRouteNameHindi.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteNameHindi.isCalculatedField = False
        Me.txtRouteNameHindi.IsSourceFromTable = False
        Me.txtRouteNameHindi.IsSourceFromValueList = False
        Me.txtRouteNameHindi.IsUnique = False
        Me.txtRouteNameHindi.Location = New System.Drawing.Point(427, 29)
        Me.txtRouteNameHindi.MaxLength = 30
        Me.txtRouteNameHindi.MendatroryField = False
        Me.txtRouteNameHindi.MyLinkLable1 = Me.MyLabel19
        Me.txtRouteNameHindi.MyLinkLable2 = Nothing
        Me.txtRouteNameHindi.Name = "txtRouteNameHindi"
        Me.txtRouteNameHindi.ReferenceFieldDesc = Nothing
        Me.txtRouteNameHindi.ReferenceFieldName = Nothing
        Me.txtRouteNameHindi.ReferenceTableName = Nothing
        Me.txtRouteNameHindi.Size = New System.Drawing.Size(263, 18)
        Me.txtRouteNameHindi.TabIndex = 12139
        '
        'lblAmout
        '
        Me.lblAmout.AutoSize = False
        Me.lblAmout.BorderVisible = True
        Me.lblAmout.FieldName = Nothing
        Me.lblAmout.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmout.Location = New System.Drawing.Point(153, 119)
        Me.lblAmout.Name = "lblAmout"
        Me.lblAmout.Size = New System.Drawing.Size(155, 18)
        Me.lblAmout.TabIndex = 1440
        Me.lblAmout.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkDefault
        '
        Me.chkDefault.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDefault.Location = New System.Drawing.Point(427, 8)
        Me.chkDefault.Name = "chkDefault"
        Me.chkDefault.Size = New System.Drawing.Size(56, 16)
        Me.chkDefault.TabIndex = 12140
        Me.chkDefault.Text = "Default"
        '
        'GBRoute
        '
        Me.GBRoute.Controls.Add(Me.txtRoute)
        Me.GBRoute.Controls.Add(Me.MyLabel28)
        Me.GBRoute.Location = New System.Drawing.Point(5, 481)
        Me.GBRoute.Name = "GBRoute"
        Me.GBRoute.Size = New System.Drawing.Size(590, 33)
        Me.GBRoute.TabIndex = 16
        Me.GBRoute.TabStop = False
        '
        'txtRoute
        '
        Me.txtRoute.arrDispalyMember = Nothing
        Me.txtRoute.arrValueMember = Nothing
        Me.txtRoute.Location = New System.Drawing.Point(104, 11)
        Me.txtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoute.MyLinkLable1 = Me.MyLabel28
        Me.txtRoute.MyLinkLable2 = Nothing
        Me.txtRoute.MyNullText = "All"
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.Size = New System.Drawing.Size(472, 19)
        Me.txtRoute.TabIndex = 429
        '
        'MyLabel28
        '
        Me.MyLabel28.FieldName = Nothing
        Me.MyLabel28.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel28.Location = New System.Drawing.Point(6, 12)
        Me.MyLabel28.Name = "MyLabel28"
        Me.MyLabel28.Size = New System.Drawing.Size(59, 18)
        Me.MyLabel28.TabIndex = 428
        Me.MyLabel28.Text = "Sale Route"
        '
        'lblLength
        '
        Me.lblLength.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.lblLength.FieldName = Nothing
        Me.lblLength.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblLength.Location = New System.Drawing.Point(18, 520)
        Me.lblLength.Name = "lblLength"
        Me.lblLength.Size = New System.Drawing.Size(471, 46)
        Me.lblLength.TabIndex = 15
        Me.lblLength.Text = "<html><p>Important Instruction</p><p>Password Length maximun 8 characters.</p><p>" &
    "( Including Special Characters, Numeric, Upper case Alphabet and Lower case Alph" &
    "abet )</p></html>"
        Me.lblLength.Visible = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(83.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(727, 315)
        Me.RadPageViewPage2.Text = "Bulk Location"
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
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(727, 315)
        Me.gv1.TabIndex = 5
        Me.gv1.TabStop = False
        Me.gv1.VarID = ""
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(694, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 23)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(141, 4)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(66, 23)
        Me.btnHistory.TabIndex = 5
        Me.btnHistory.Text = "History"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Location = New System.Drawing.Point(998, 3)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(66, 18)
        Me.rdbtnclose.TabIndex = 4
        Me.rdbtnclose.Text = "Close"
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Location = New System.Drawing.Point(7, 4)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(66, 23)
        Me.rdbtnsave.TabIndex = 2
        Me.rdbtnsave.Text = "Save"
        '
        'rdbtndelete
        '
        Me.rdbtndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtndelete.Location = New System.Drawing.Point(74, 4)
        Me.rdbtndelete.Name = "rdbtndelete"
        Me.rdbtndelete.Size = New System.Drawing.Size(66, 23)
        Me.rdbtndelete.TabIndex = 3
        Me.rdbtndelete.Text = "Delete"
        '
        'lblZone
        '
        Me.lblZone.AutoSize = False
        Me.lblZone.BorderVisible = True
        Me.lblZone.FieldName = Nothing
        Me.lblZone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZone.Location = New System.Drawing.Point(311, 209)
        Me.lblZone.Name = "lblZone"
        Me.lblZone.Size = New System.Drawing.Size(300, 21)
        Me.lblZone.TabIndex = 12146
        Me.lblZone.TextWrap = False
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel10.Location = New System.Drawing.Point(19, 210)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel10.TabIndex = 12145
        Me.MyLabel10.Text = "Zone"
        '
        'txtZone
        '
        Me.txtZone.CalculationExpression = Nothing
        Me.txtZone.FieldCode = Nothing
        Me.txtZone.FieldDesc = Nothing
        Me.txtZone.FieldMaxLength = 0
        Me.txtZone.FieldName = Nothing
        Me.txtZone.isCalculatedField = False
        Me.txtZone.IsSourceFromTable = False
        Me.txtZone.IsSourceFromValueList = False
        Me.txtZone.IsUnique = False
        Me.txtZone.Location = New System.Drawing.Point(153, 209)
        Me.txtZone.MendatroryField = True
        Me.txtZone.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtZone.MyLinkLable1 = Me.MyLabel10
        Me.txtZone.MyLinkLable2 = Nothing
        Me.txtZone.MyReadOnly = False
        Me.txtZone.MyShowMasterFormButton = False
        Me.txtZone.Name = "txtZone"
        Me.txtZone.ReferenceFieldDesc = Nothing
        Me.txtZone.ReferenceFieldName = Nothing
        Me.txtZone.ReferenceTableName = Nothing
        Me.txtZone.Size = New System.Drawing.Size(155, 20)
        Me.txtZone.TabIndex = 12144
        Me.txtZone.Value = ""
        '
        'FrmBulkRoutMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(772, 474)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenu1)
        Me.Name = "FrmBulkRoutMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bulk Route Master"
        CType(Me.rdmenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.GVLocatiom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GVLocatiom.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblScheduleTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRouteName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtScheduleTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDistance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtScheduleTimeE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtScheduleTimeM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CuttOffTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTollAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcuttofftime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkForContractor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRouteNameHindi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmout, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDefault, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBRoute.ResumeLayout(False)
        Me.GBRoute.PerformLayout()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLength, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblZone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdmenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents File As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtRouteName As common.Controls.MyTextBox
    Friend WithEvents txtRate As common.MyNumBox
    Friend WithEvents txtDistance As common.MyNumBox
    Friend WithEvents txtRouteNo As common.UserControls.txtNavigator
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblAmout As common.Controls.MyLabel
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtWeight As common.MyNumBox
    Friend WithEvents txtMCC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents chkForContractor As RadCheckBox
    Friend WithEvents lblToLocationName As common.Controls.MyLabel
    Friend WithEvents txtToLocationCode As common.UserControls.txtFinder
    Friend WithEvents txtTollAmount As common.MyNumBox
    Friend WithEvents txtRouteNameHindi As common.Controls.MyTextBox
    Friend WithEvents chkDefault As RadCheckBox
    Friend WithEvents txtTankerNo As common.UserControls.txtFinder
    Friend WithEvents txtVehicleNo As common.Controls.MyLabel
    Friend WithEvents txtcuttofftime As common.Controls.MyDateTimePicker
    Friend WithEvents CuttOffTime As common.Controls.MyLabel
    Friend WithEvents txtScheduleTimeE As common.Controls.MyDateTimePicker
    Friend WithEvents txtScheduleTimeM As common.Controls.MyDateTimePicker
    Friend WithEvents txtScheduleTime As common.Controls.MyDateTimePicker
    Friend WithEvents btnHistory As RadButton
    Friend WithEvents GVLocatiom As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblScheduleTime As common.Controls.MyLabel
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents lblToLocation As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents GBRoute As GroupBox
    Friend WithEvents txtRoute As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents lblLength As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents btnClose As RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents lblZone As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtZone As common.UserControls.txtFinder
End Class

