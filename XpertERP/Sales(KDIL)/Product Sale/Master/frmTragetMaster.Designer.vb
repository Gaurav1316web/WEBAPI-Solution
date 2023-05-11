<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTragetMaster
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
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuInport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblTragetCode = New common.Controls.MyLabel()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.lblFromDate = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.RadCheckBox2 = New Telerik.WinControls.UI.RadCheckBox()
        Me.TxtFinder3 = New common.UserControls.txtFinder()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.MyDateTimePicker3 = New common.Controls.MyDateTimePicker()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.TxtFinder4 = New common.UserControls.txtFinder()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.lblTragetType = New common.Controls.MyLabel()
        Me.ddlTragetType = New common.Controls.MyComboBox()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.txtfromDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.fndTragetCode = New common.UserControls.txtNavigator()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.gvItem = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.lblCriteria = New common.Controls.MyLabel()
        Me.txtCriteria = New common.UserControls.txtFinder()
        Me.btnSelect = New Telerik.WinControls.UI.RadButton()
        Me.ddlCriteria = New common.Controls.MyComboBox()
        Me.RadLabel13 = New common.Controls.MyLabel()
        Me.gvCustomer = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTragetCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lblFromDate.SuspendLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadCheckBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyDateTimePicker3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTragetType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlTragetType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblCriteria, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlCriteria, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuInport, Me.RadMenuExport})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuInport
        '
        Me.RadMenuInport.AccessibleDescription = "Import"
        Me.RadMenuInport.AccessibleName = "Import"
        Me.RadMenuInport.Name = "RadMenuInport"
        Me.RadMenuInport.Text = "Import"
        '
        'RadMenuExport
        '
        Me.RadMenuExport.AccessibleDescription = "Export"
        Me.RadMenuExport.AccessibleName = "Export"
        Me.RadMenuExport.Name = "RadMenuExport"
        Me.RadMenuExport.Text = "Export"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(888, 487)
        Me.SplitContainer1.SplitterDistance = 451
        Me.SplitContainer1.TabIndex = 5
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(888, 451)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(65.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(867, 403)
        Me.RadPageViewPage1.Text = "Customer"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblTragetCode)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblFromDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblTragetType)
        Me.SplitContainer3.Panel1.Controls.Add(Me.ddlTragetType)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtDesc)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtfromDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblToDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndTragetCode)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtToDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnReset)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.gvItem)
        Me.SplitContainer3.Panel2.Padding = New System.Windows.Forms.Padding(1)
        Me.SplitContainer3.Size = New System.Drawing.Size(867, 403)
        Me.SplitContainer3.SplitterDistance = 74
        Me.SplitContainer3.TabIndex = 0
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
        Me.txtDate.Location = New System.Drawing.Point(394, 3)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Nothing
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(79, 18)
        Me.txtDate.TabIndex = 23
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblTragetCode
        '
        Me.lblTragetCode.FieldName = Nothing
        Me.lblTragetCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblTragetCode.Location = New System.Drawing.Point(7, 3)
        Me.lblTragetCode.Name = "lblTragetCode"
        Me.lblTragetCode.Size = New System.Drawing.Size(69, 16)
        Me.lblTragetCode.TabIndex = 20
        Me.lblTragetCode.Text = "Traget Code"
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(5, 24)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 19
        Me.lblDescription.Text = "Description"
        '
        'lblFromDate
        '
        Me.lblFromDate.Controls.Add(Me.MyLabel13)
        Me.lblFromDate.Controls.Add(Me.MyLabel10)
        Me.lblFromDate.Controls.Add(Me.RadCheckBox2)
        Me.lblFromDate.Controls.Add(Me.TxtFinder3)
        Me.lblFromDate.Controls.Add(Me.MyLabel14)
        Me.lblFromDate.Controls.Add(Me.MyDateTimePicker3)
        Me.lblFromDate.Controls.Add(Me.TxtFinder4)
        Me.lblFromDate.Controls.Add(Me.MyLabel11)
        Me.lblFromDate.FieldName = Nothing
        Me.lblFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.Location = New System.Drawing.Point(511, 3)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(60, 16)
        Me.lblFromDate.TabIndex = 21
        Me.lblFromDate.Text = "From Date"
        '
        'MyLabel13
        '
        Me.MyLabel13.AutoSize = False
        Me.MyLabel13.BorderVisible = True
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(-87, 42)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(500, 18)
        Me.MyLabel13.TabIndex = 23
        Me.MyLabel13.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(-365, 45)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel10.TabIndex = 18
        Me.MyLabel10.Text = "Main Item"
        '
        'RadCheckBox2
        '
        Me.RadCheckBox2.Location = New System.Drawing.Point(158, -1)
        Me.RadCheckBox2.Name = "RadCheckBox2"
        Me.RadCheckBox2.Size = New System.Drawing.Size(59, 18)
        Me.RadCheckBox2.TabIndex = 3
        Me.RadCheckBox2.Text = "Inactive"
        '
        'TxtFinder3
        '
        Me.TxtFinder3.CalculationExpression = Nothing
        Me.TxtFinder3.FieldCode = Nothing
        Me.TxtFinder3.FieldDesc = Nothing
        Me.TxtFinder3.FieldMaxLength = 0
        Me.TxtFinder3.FieldName = Nothing
        Me.TxtFinder3.isCalculatedField = False
        Me.TxtFinder3.IsSourceFromTable = False
        Me.TxtFinder3.IsSourceFromValueList = False
        Me.TxtFinder3.IsUnique = False
        Me.TxtFinder3.Location = New System.Drawing.Point(-250, 42)
        Me.TxtFinder3.MendatroryField = True
        Me.TxtFinder3.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinder3.MyLinkLable1 = Me.MyLabel10
        Me.TxtFinder3.MyLinkLable2 = Nothing
        Me.TxtFinder3.MyReadOnly = False
        Me.TxtFinder3.MyShowMasterFormButton = False
        Me.TxtFinder3.Name = "TxtFinder3"
        Me.TxtFinder3.ReferenceFieldDesc = Nothing
        Me.TxtFinder3.ReferenceFieldName = Nothing
        Me.TxtFinder3.ReferenceTableName = Nothing
        Me.TxtFinder3.Size = New System.Drawing.Size(157, 18)
        Me.TxtFinder3.TabIndex = 6
        Me.TxtFinder3.Value = ""
        '
        'MyLabel14
        '
        Me.MyLabel14.AutoSize = False
        Me.MyLabel14.BorderVisible = True
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(-87, 64)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(500, 18)
        Me.MyLabel14.TabIndex = 24
        Me.MyLabel14.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyDateTimePicker3
        '
        Me.MyDateTimePicker3.CalculationExpression = Nothing
        Me.MyDateTimePicker3.CustomFormat = "dd/MM/yyyy"
        Me.MyDateTimePicker3.FieldCode = Nothing
        Me.MyDateTimePicker3.FieldDesc = Nothing
        Me.MyDateTimePicker3.FieldMaxLength = 0
        Me.MyDateTimePicker3.FieldName = Nothing
        Me.MyDateTimePicker3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyDateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.MyDateTimePicker3.isCalculatedField = False
        Me.MyDateTimePicker3.IsSourceFromTable = False
        Me.MyDateTimePicker3.IsSourceFromValueList = False
        Me.MyDateTimePicker3.IsUnique = False
        Me.MyDateTimePicker3.Location = New System.Drawing.Point(328, -1)
        Me.MyDateTimePicker3.MendatroryField = False
        Me.MyDateTimePicker3.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.MyDateTimePicker3.MyLinkLable1 = Me.MyLabel11
        Me.MyDateTimePicker3.MyLinkLable2 = Nothing
        Me.MyDateTimePicker3.Name = "MyDateTimePicker3"
        Me.MyDateTimePicker3.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.MyDateTimePicker3.ReferenceFieldDesc = Nothing
        Me.MyDateTimePicker3.ReferenceFieldName = Nothing
        Me.MyDateTimePicker3.ReferenceTableName = Nothing
        Me.MyDateTimePicker3.Size = New System.Drawing.Size(85, 18)
        Me.MyDateTimePicker3.TabIndex = 4
        Me.MyDateTimePicker3.TabStop = False
        Me.MyDateTimePicker3.Text = "17/05/2011"
        Me.MyDateTimePicker3.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(244, 0)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel11.TabIndex = 22
        Me.MyLabel11.Text = "InActive Date"
        '
        'TxtFinder4
        '
        Me.TxtFinder4.CalculationExpression = Nothing
        Me.TxtFinder4.FieldCode = Nothing
        Me.TxtFinder4.FieldDesc = Nothing
        Me.TxtFinder4.FieldMaxLength = 0
        Me.TxtFinder4.FieldName = Nothing
        Me.TxtFinder4.isCalculatedField = False
        Me.TxtFinder4.IsSourceFromTable = False
        Me.TxtFinder4.IsSourceFromValueList = False
        Me.TxtFinder4.IsUnique = False
        Me.TxtFinder4.Location = New System.Drawing.Point(-250, 64)
        Me.TxtFinder4.MendatroryField = True
        Me.TxtFinder4.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinder4.MyLinkLable1 = Nothing
        Me.TxtFinder4.MyLinkLable2 = Nothing
        Me.TxtFinder4.MyReadOnly = False
        Me.TxtFinder4.MyShowMasterFormButton = False
        Me.TxtFinder4.Name = "TxtFinder4"
        Me.TxtFinder4.ReferenceFieldDesc = Nothing
        Me.TxtFinder4.ReferenceFieldName = Nothing
        Me.TxtFinder4.ReferenceTableName = Nothing
        Me.TxtFinder4.Size = New System.Drawing.Size(156, 18)
        Me.TxtFinder4.TabIndex = 7
        Me.TxtFinder4.Value = ""
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(358, 3)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel3.TabIndex = 21
        Me.RadLabel3.Text = "Date"
        '
        'lblTragetType
        '
        Me.lblTragetType.FieldName = Nothing
        Me.lblTragetType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTragetType.Location = New System.Drawing.Point(5, 49)
        Me.lblTragetType.Name = "lblTragetType"
        Me.lblTragetType.Size = New System.Drawing.Size(67, 16)
        Me.lblTragetType.TabIndex = 16
        Me.lblTragetType.Text = "Traget Type"
        '
        'ddlTragetType
        '
        Me.ddlTragetType.AutoCompleteDisplayMember = Nothing
        Me.ddlTragetType.AutoCompleteValueMember = Nothing
        Me.ddlTragetType.CalculationExpression = Nothing
        Me.ddlTragetType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlTragetType.FieldCode = Nothing
        Me.ddlTragetType.FieldDesc = Nothing
        Me.ddlTragetType.FieldMaxLength = 0
        Me.ddlTragetType.FieldName = Nothing
        Me.ddlTragetType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlTragetType.isCalculatedField = False
        Me.ddlTragetType.IsSourceFromTable = False
        Me.ddlTragetType.IsSourceFromValueList = False
        Me.ddlTragetType.IsUnique = False
        Me.ddlTragetType.Location = New System.Drawing.Point(120, 49)
        Me.ddlTragetType.MendatroryField = True
        Me.ddlTragetType.MyLinkLable1 = Me.lblTragetType
        Me.ddlTragetType.MyLinkLable2 = Nothing
        Me.ddlTragetType.Name = "ddlTragetType"
        Me.ddlTragetType.ReferenceFieldDesc = Nothing
        Me.ddlTragetType.ReferenceFieldName = Nothing
        Me.ddlTragetType.ReferenceTableName = Nothing
        Me.ddlTragetType.Size = New System.Drawing.Size(157, 18)
        Me.ddlTragetType.TabIndex = 8
        '
        'txtDesc
        '
        Me.txtDesc.CalculationExpression = Nothing
        Me.txtDesc.FieldCode = Nothing
        Me.txtDesc.FieldDesc = Nothing
        Me.txtDesc.FieldMaxLength = 0
        Me.txtDesc.FieldName = Nothing
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.isCalculatedField = False
        Me.txtDesc.IsSourceFromTable = False
        Me.txtDesc.IsSourceFromValueList = False
        Me.txtDesc.IsUnique = False
        Me.txtDesc.Location = New System.Drawing.Point(120, 24)
        Me.txtDesc.MendatroryField = True
        Me.txtDesc.MyLinkLable1 = Me.lblDescription
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(587, 18)
        Me.txtDesc.TabIndex = 5
        '
        'txtfromDate
        '
        Me.txtfromDate.CalculationExpression = Nothing
        Me.txtfromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtfromDate.FieldCode = Nothing
        Me.txtfromDate.FieldDesc = Nothing
        Me.txtfromDate.FieldMaxLength = 0
        Me.txtfromDate.FieldName = Nothing
        Me.txtfromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtfromDate.isCalculatedField = False
        Me.txtfromDate.IsSourceFromTable = False
        Me.txtfromDate.IsSourceFromValueList = False
        Me.txtfromDate.IsUnique = False
        Me.txtfromDate.Location = New System.Drawing.Point(575, 3)
        Me.txtfromDate.MendatroryField = False
        Me.txtfromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtfromDate.MyLinkLable1 = Me.RadLabel3
        Me.txtfromDate.MyLinkLable2 = Nothing
        Me.txtfromDate.Name = "txtfromDate"
        Me.txtfromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtfromDate.ReferenceFieldDesc = Nothing
        Me.txtfromDate.ReferenceFieldName = Nothing
        Me.txtfromDate.ReferenceTableName = Nothing
        Me.txtfromDate.Size = New System.Drawing.Size(106, 18)
        Me.txtfromDate.TabIndex = 2
        Me.txtfromDate.TabStop = False
        Me.txtfromDate.Text = "17/05/2011"
        Me.txtfromDate.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(686, 3)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(46, 16)
        Me.lblToDate.TabIndex = 22
        Me.lblToDate.Text = "To Date"
        '
        'fndTragetCode
        '
        Me.fndTragetCode.FieldName = Nothing
        Me.fndTragetCode.Location = New System.Drawing.Point(120, 2)
        Me.fndTragetCode.MendatroryField = True
        Me.fndTragetCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndTragetCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndTragetCode.MyLinkLable1 = Me.lblTragetCode
        Me.fndTragetCode.MyLinkLable2 = Nothing
        Me.fndTragetCode.MyMaxLength = 32767
        Me.fndTragetCode.MyReadOnly = False
        Me.fndTragetCode.Name = "fndTragetCode"
        Me.fndTragetCode.Size = New System.Drawing.Size(217, 18)
        Me.fndTragetCode.TabIndex = 0
        Me.fndTragetCode.Value = ""
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
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
        Me.txtToDate.Location = New System.Drawing.Point(740, 2)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.lblToDate
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(108, 18)
        Me.txtToDate.TabIndex = 4
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "17/05/2011"
        Me.txtToDate.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnReset.Location = New System.Drawing.Point(336, 2)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(17, 19)
        Me.btnReset.TabIndex = 1
        '
        'gvItem
        '
        Me.gvItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvItem.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gvItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gvItem.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvItem.Location = New System.Drawing.Point(1, 1)
        '
        'gvItem
        '
        Me.gvItem.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvItem.MasterTemplate.AutoGenerateColumns = False
        Me.gvItem.MasterTemplate.EnableGrouping = False
        Me.gvItem.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvItem.Name = "gvItem"
        Me.gvItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvItem.ShowHeaderCellButtons = True
        Me.gvItem.Size = New System.Drawing.Size(865, 323)
        Me.gvItem.TabIndex = 0
        Me.gvItem.TabStop = False
        Me.gvItem.Text = "RadGridView1"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(52.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(867, 403)
        Me.RadPageViewPage2.Text = "Criteria"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCriteria)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCriteria)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnSelect)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ddlCriteria)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel13)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gvCustomer)
        Me.SplitContainer2.Size = New System.Drawing.Size(867, 403)
        Me.SplitContainer2.SplitterDistance = 28
        Me.SplitContainer2.TabIndex = 0
        '
        'lblCriteria
        '
        Me.lblCriteria.AutoSize = False
        Me.lblCriteria.BorderVisible = True
        Me.lblCriteria.FieldName = Nothing
        Me.lblCriteria.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCriteria.Location = New System.Drawing.Point(383, 4)
        Me.lblCriteria.Name = "lblCriteria"
        Me.lblCriteria.Size = New System.Drawing.Size(376, 18)
        Me.lblCriteria.TabIndex = 618
        Me.lblCriteria.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCriteria
        '
        Me.txtCriteria.CalculationExpression = Nothing
        Me.txtCriteria.FieldCode = Nothing
        Me.txtCriteria.FieldDesc = Nothing
        Me.txtCriteria.FieldMaxLength = 0
        Me.txtCriteria.FieldName = Nothing
        Me.txtCriteria.isCalculatedField = False
        Me.txtCriteria.IsSourceFromTable = False
        Me.txtCriteria.IsSourceFromValueList = False
        Me.txtCriteria.IsUnique = False
        Me.txtCriteria.Location = New System.Drawing.Point(223, 4)
        Me.txtCriteria.MendatroryField = True
        Me.txtCriteria.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCriteria.MyLinkLable1 = Nothing
        Me.txtCriteria.MyLinkLable2 = Nothing
        Me.txtCriteria.MyReadOnly = False
        Me.txtCriteria.MyShowMasterFormButton = False
        Me.txtCriteria.Name = "txtCriteria"
        Me.txtCriteria.ReferenceFieldDesc = Nothing
        Me.txtCriteria.ReferenceFieldName = Nothing
        Me.txtCriteria.ReferenceTableName = Nothing
        Me.txtCriteria.Size = New System.Drawing.Size(156, 18)
        Me.txtCriteria.TabIndex = 1
        Me.txtCriteria.Value = ""
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Location = New System.Drawing.Point(780, 6)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(80, 18)
        Me.btnSelect.TabIndex = 2
        Me.btnSelect.Text = "Select All"
        '
        'ddlCriteria
        '
        Me.ddlCriteria.AutoCompleteDisplayMember = Nothing
        Me.ddlCriteria.AutoCompleteValueMember = Nothing
        Me.ddlCriteria.CalculationExpression = Nothing
        Me.ddlCriteria.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlCriteria.FieldCode = Nothing
        Me.ddlCriteria.FieldDesc = Nothing
        Me.ddlCriteria.FieldMaxLength = 0
        Me.ddlCriteria.FieldName = Nothing
        Me.ddlCriteria.isCalculatedField = False
        Me.ddlCriteria.IsSourceFromTable = False
        Me.ddlCriteria.IsSourceFromValueList = False
        Me.ddlCriteria.IsUnique = False
        Me.ddlCriteria.Location = New System.Drawing.Point(64, 3)
        Me.ddlCriteria.MendatroryField = False
        Me.ddlCriteria.MyLinkLable1 = Nothing
        Me.ddlCriteria.MyLinkLable2 = Nothing
        Me.ddlCriteria.Name = "ddlCriteria"
        Me.ddlCriteria.ReferenceFieldDesc = Nothing
        Me.ddlCriteria.ReferenceFieldName = Nothing
        Me.ddlCriteria.ReferenceTableName = Nothing
        Me.ddlCriteria.Size = New System.Drawing.Size(156, 20)
        Me.ddlCriteria.TabIndex = 0
        '
        'RadLabel13
        '
        Me.RadLabel13.FieldName = Nothing
        Me.RadLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel13.Location = New System.Drawing.Point(8, 5)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(43, 16)
        Me.RadLabel13.TabIndex = 613
        Me.RadLabel13.Text = "Criteria"
        '
        'gvCustomer
        '
        Me.gvCustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCustomer.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gvCustomer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gvCustomer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvCustomer.Location = New System.Drawing.Point(0, 0)
        '
        'gvCustomer
        '
        Me.gvCustomer.MasterTemplate.AllowAddNewRow = False
        Me.gvCustomer.MasterTemplate.EnableFiltering = True
        Me.gvCustomer.MasterTemplate.EnableGrouping = False
        Me.gvCustomer.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCustomer.Name = "gvCustomer"
        Me.gvCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvCustomer.ShowHeaderCellButtons = True
        Me.gvCustomer.Size = New System.Drawing.Size(867, 371)
        Me.gvCustomer.TabIndex = 0
        Me.gvCustomer.TabStop = False
        Me.gvCustomer.Text = "RadGridView1"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(805, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 18)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(90, 6)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(80, 18)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(7, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 18)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(888, 20)
        Me.RadMenu1.TabIndex = 4
        Me.RadMenu1.Text = "RadMenu1"
        '
        'FrmTragetMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(888, 507)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmTragetMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmTragetMaster"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTragetCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lblFromDate.ResumeLayout(False)
        Me.lblFromDate.PerformLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadCheckBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyDateTimePicker3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTragetType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlTragetType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblCriteria, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlCriteria, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblTragetCode As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents lblTragetType As common.Controls.MyLabel
    Friend WithEvents ddlTragetType As common.Controls.MyComboBox
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents txtfromDate As common.Controls.MyDateTimePicker
    Friend WithEvents fndTragetCode As common.UserControls.txtNavigator
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents gvItem As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblCriteria As common.Controls.MyLabel
    Friend WithEvents txtCriteria As common.UserControls.txtFinder
    Friend WithEvents btnSelect As Telerik.WinControls.UI.RadButton
    Friend WithEvents ddlCriteria As common.Controls.MyComboBox
    Friend WithEvents RadLabel13 As common.Controls.MyLabel
    Friend WithEvents gvCustomer As common.UserControls.MyRadGridView
    Friend WithEvents lblFromDate As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents RadCheckBox2 As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents TxtFinder3 As common.UserControls.txtFinder
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents MyDateTimePicker3 As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents TxtFinder4 As common.UserControls.txtFinder
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadMenuInport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
End Class

