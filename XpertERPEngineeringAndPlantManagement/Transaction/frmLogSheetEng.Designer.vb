<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogSheetEng
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogSheetEng))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.lblSection = New common.Controls.MyLabel()
        Me.txtConsumType = New common.UserControls.txtFinder()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblConsumTypedes = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtSection = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.lblCode = New common.Controls.MyLabel()
        Me.lblBomDate = New common.Controls.MyLabel()
        Me.dtpDate = New common.Controls.MyDateTimePicker()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvParam = New common.UserControls.MyRadGridView()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.txtRepair = New common.Controls.MyTextBox()
        Me.txtOilChange = New common.Controls.MyTextBox()
        Me.txtopTotal = New common.Controls.MyTextBox()
        Me.txtOp = New common.Controls.MyTextBox()
        Me.txtOpEarlier = New common.Controls.MyTextBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConsumTypedes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gvParam, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvParam.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.txtRepair, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOilChange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtopTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOpEarlier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer1.Size = New System.Drawing.Size(830, 598)
        Me.SplitContainer1.SplitterDistance = 399
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblSection)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtConsumType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblConsumTypedes)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtSection)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBomDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer2.Size = New System.Drawing.Size(824, 393)
        Me.SplitContainer2.SplitterDistance = 120
        Me.SplitContainer2.TabIndex = 0
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(120, 36)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Nothing
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(194, 20)
        Me.txtLocation.TabIndex = 90
        Me.txtLocation.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(320, 36)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(221, 20)
        Me.lblLocation.TabIndex = 89
        Me.lblLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocation.TextWrap = False
        '
        'lblSection
        '
        Me.lblSection.AutoSize = False
        Me.lblSection.BorderVisible = True
        Me.lblSection.FieldName = Nothing
        Me.lblSection.Location = New System.Drawing.Point(320, 59)
        Me.lblSection.Name = "lblSection"
        Me.lblSection.Size = New System.Drawing.Size(221, 19)
        Me.lblSection.TabIndex = 88
        Me.lblSection.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtConsumType
        '
        Me.txtConsumType.CalculationExpression = Nothing
        Me.txtConsumType.FieldCode = Nothing
        Me.txtConsumType.FieldDesc = Nothing
        Me.txtConsumType.FieldMaxLength = 0
        Me.txtConsumType.FieldName = Nothing
        Me.txtConsumType.isCalculatedField = False
        Me.txtConsumType.IsSourceFromTable = False
        Me.txtConsumType.IsSourceFromValueList = False
        Me.txtConsumType.IsUnique = False
        Me.txtConsumType.Location = New System.Drawing.Point(120, 80)
        Me.txtConsumType.MendatroryField = True
        Me.txtConsumType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConsumType.MyLinkLable1 = Nothing
        Me.txtConsumType.MyLinkLable2 = Nothing
        Me.txtConsumType.MyReadOnly = False
        Me.txtConsumType.MyShowMasterFormButton = False
        Me.txtConsumType.Name = "txtConsumType"
        Me.txtConsumType.ReferenceFieldDesc = Nothing
        Me.txtConsumType.ReferenceFieldName = Nothing
        Me.txtConsumType.ReferenceTableName = Nothing
        Me.txtConsumType.Size = New System.Drawing.Size(194, 20)
        Me.txtConsumType.TabIndex = 87
        Me.txtConsumType.Value = ""
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(9, 61)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel7.TabIndex = 86
        Me.MyLabel7.Text = "Section"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(9, 40)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel5.TabIndex = 85
        Me.MyLabel5.Text = "Location"
        '
        'lblConsumTypedes
        '
        Me.lblConsumTypedes.AutoSize = False
        Me.lblConsumTypedes.BorderVisible = True
        Me.lblConsumTypedes.FieldName = Nothing
        Me.lblConsumTypedes.Location = New System.Drawing.Point(320, 81)
        Me.lblConsumTypedes.Name = "lblConsumTypedes"
        Me.lblConsumTypedes.Size = New System.Drawing.Size(221, 19)
        Me.lblConsumTypedes.TabIndex = 43
        Me.lblConsumTypedes.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(9, 82)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(101, 16)
        Me.MyLabel1.TabIndex = 36
        Me.MyLabel1.Text = "Consumption Type"
        '
        'txtSection
        '
        Me.txtSection.CalculationExpression = Nothing
        Me.txtSection.FieldCode = Nothing
        Me.txtSection.FieldDesc = Nothing
        Me.txtSection.FieldMaxLength = 0
        Me.txtSection.FieldName = Nothing
        Me.txtSection.isCalculatedField = False
        Me.txtSection.IsSourceFromTable = False
        Me.txtSection.IsSourceFromValueList = False
        Me.txtSection.IsUnique = False
        Me.txtSection.Location = New System.Drawing.Point(120, 59)
        Me.txtSection.MendatroryField = True
        Me.txtSection.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSection.MyLinkLable1 = Me.MyLabel6
        Me.txtSection.MyLinkLable2 = Me.lblConsumTypedes
        Me.txtSection.MyReadOnly = False
        Me.txtSection.MyShowMasterFormButton = False
        Me.txtSection.Name = "txtSection"
        Me.txtSection.ReferenceFieldDesc = Nothing
        Me.txtSection.ReferenceFieldName = Nothing
        Me.txtSection.ReferenceTableName = Nothing
        Me.txtSection.Size = New System.Drawing.Size(194, 19)
        Me.txtSection.TabIndex = 4
        Me.txtSection.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(16, 37)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(120, 16)
        Me.MyLabel6.TabIndex = 42
        Me.MyLabel6.Text = "No. of Hours Operated"
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(9, 15)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(33, 16)
        Me.lblCode.TabIndex = 4
        Me.lblCode.Text = "Code"
        '
        'lblBomDate
        '
        Me.lblBomDate.FieldName = Nothing
        Me.lblBomDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBomDate.Location = New System.Drawing.Point(422, 15)
        Me.lblBomDate.Name = "lblBomDate"
        Me.lblBomDate.Size = New System.Drawing.Size(30, 16)
        Me.lblBomDate.TabIndex = 5
        Me.lblBomDate.Text = "Date"
        '
        'dtpDate
        '
        Me.dtpDate.CalculationExpression = Nothing
        Me.dtpDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDate.FieldCode = Nothing
        Me.dtpDate.FieldDesc = Nothing
        Me.dtpDate.FieldMaxLength = 0
        Me.dtpDate.FieldName = Nothing
        Me.dtpDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.isCalculatedField = False
        Me.dtpDate.IsSourceFromTable = False
        Me.dtpDate.IsSourceFromValueList = False
        Me.dtpDate.IsUnique = False
        Me.dtpDate.Location = New System.Drawing.Point(454, 14)
        Me.dtpDate.MendatroryField = True
        Me.dtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.MyLinkLable1 = Me.lblBomDate
        Me.dtpDate.MyLinkLable2 = Nothing
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.ReferenceFieldDesc = Nothing
        Me.dtpDate.ReferenceFieldName = Nothing
        Me.dtpDate.ReferenceTableName = Nothing
        Me.dtpDate.Size = New System.Drawing.Size(87, 18)
        Me.dtpDate.TabIndex = 1
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "03/05/2011"
        Me.dtpDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(388, 13)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(20, 21)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = " "
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(120, 13)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(267, 21)
        Me.txtCode.TabIndex = 3
        Me.txtCode.Value = ""
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(3, 3)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(818, 263)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gvParam)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(67.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(797, 215)
        Me.RadPageViewPage2.Text = "Parameter"
        '
        'gvParam
        '
        Me.gvParam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvParam.Location = New System.Drawing.Point(0, 0)
        '
        'gvParam
        '
        Me.gvParam.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvParam.Name = "gvParam"
        Me.gvParam.ShowHeaderCellButtons = True
        Me.gvParam.Size = New System.Drawing.Size(797, 215)
        Me.gvParam.TabIndex = 265
        Me.gvParam.Text = "RadGridView1"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtRepair)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtOilChange)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtopTotal)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtOp)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtOpEarlier)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel6)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer3.Size = New System.Drawing.Size(830, 195)
        Me.SplitContainer3.SplitterDistance = 157
        Me.SplitContainer3.TabIndex = 34
        '
        'txtRepair
        '
        Me.txtRepair.CalculationExpression = Nothing
        Me.txtRepair.FieldCode = Nothing
        Me.txtRepair.FieldDesc = Nothing
        Me.txtRepair.FieldMaxLength = 0
        Me.txtRepair.FieldName = Nothing
        Me.txtRepair.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRepair.isCalculatedField = False
        Me.txtRepair.IsSourceFromTable = False
        Me.txtRepair.IsSourceFromValueList = False
        Me.txtRepair.IsUnique = False
        Me.txtRepair.Location = New System.Drawing.Point(177, 100)
        Me.txtRepair.MaxLength = 50
        Me.txtRepair.MendatroryField = False
        Me.txtRepair.MyLinkLable1 = Nothing
        Me.txtRepair.MyLinkLable2 = Nothing
        Me.txtRepair.Name = "txtRepair"
        Me.txtRepair.ReferenceFieldDesc = Nothing
        Me.txtRepair.ReferenceFieldName = Nothing
        Me.txtRepair.ReferenceTableName = Nothing
        Me.txtRepair.Size = New System.Drawing.Size(178, 18)
        Me.txtRepair.TabIndex = 50
        '
        'txtOilChange
        '
        Me.txtOilChange.CalculationExpression = Nothing
        Me.txtOilChange.FieldCode = Nothing
        Me.txtOilChange.FieldDesc = Nothing
        Me.txtOilChange.FieldMaxLength = 0
        Me.txtOilChange.FieldName = Nothing
        Me.txtOilChange.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOilChange.isCalculatedField = False
        Me.txtOilChange.IsSourceFromTable = False
        Me.txtOilChange.IsSourceFromValueList = False
        Me.txtOilChange.IsUnique = False
        Me.txtOilChange.Location = New System.Drawing.Point(177, 79)
        Me.txtOilChange.MaxLength = 50
        Me.txtOilChange.MendatroryField = False
        Me.txtOilChange.MyLinkLable1 = Nothing
        Me.txtOilChange.MyLinkLable2 = Nothing
        Me.txtOilChange.Name = "txtOilChange"
        Me.txtOilChange.ReferenceFieldDesc = Nothing
        Me.txtOilChange.ReferenceFieldName = Nothing
        Me.txtOilChange.ReferenceTableName = Nothing
        Me.txtOilChange.Size = New System.Drawing.Size(178, 18)
        Me.txtOilChange.TabIndex = 49
        '
        'txtopTotal
        '
        Me.txtopTotal.CalculationExpression = Nothing
        Me.txtopTotal.FieldCode = Nothing
        Me.txtopTotal.FieldDesc = Nothing
        Me.txtopTotal.FieldMaxLength = 0
        Me.txtopTotal.FieldName = Nothing
        Me.txtopTotal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtopTotal.isCalculatedField = False
        Me.txtopTotal.IsSourceFromTable = False
        Me.txtopTotal.IsSourceFromValueList = False
        Me.txtopTotal.IsUnique = False
        Me.txtopTotal.Location = New System.Drawing.Point(177, 57)
        Me.txtopTotal.MaxLength = 12
        Me.txtopTotal.MendatroryField = False
        Me.txtopTotal.MyLinkLable1 = Nothing
        Me.txtopTotal.MyLinkLable2 = Nothing
        Me.txtopTotal.Name = "txtopTotal"
        Me.txtopTotal.ReferenceFieldDesc = Nothing
        Me.txtopTotal.ReferenceFieldName = Nothing
        Me.txtopTotal.ReferenceTableName = Nothing
        Me.txtopTotal.Size = New System.Drawing.Size(178, 18)
        Me.txtopTotal.TabIndex = 48
        '
        'txtOp
        '
        Me.txtOp.CalculationExpression = Nothing
        Me.txtOp.FieldCode = Nothing
        Me.txtOp.FieldDesc = Nothing
        Me.txtOp.FieldMaxLength = 0
        Me.txtOp.FieldName = Nothing
        Me.txtOp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOp.isCalculatedField = False
        Me.txtOp.IsSourceFromTable = False
        Me.txtOp.IsSourceFromValueList = False
        Me.txtOp.IsUnique = False
        Me.txtOp.Location = New System.Drawing.Point(177, 36)
        Me.txtOp.MaxLength = 12
        Me.txtOp.MendatroryField = False
        Me.txtOp.MyLinkLable1 = Nothing
        Me.txtOp.MyLinkLable2 = Nothing
        Me.txtOp.Name = "txtOp"
        Me.txtOp.ReferenceFieldDesc = Nothing
        Me.txtOp.ReferenceFieldName = Nothing
        Me.txtOp.ReferenceTableName = Nothing
        Me.txtOp.Size = New System.Drawing.Size(178, 18)
        Me.txtOp.TabIndex = 47
        '
        'txtOpEarlier
        '
        Me.txtOpEarlier.CalculationExpression = Nothing
        Me.txtOpEarlier.FieldCode = Nothing
        Me.txtOpEarlier.FieldDesc = Nothing
        Me.txtOpEarlier.FieldMaxLength = 0
        Me.txtOpEarlier.FieldName = Nothing
        Me.txtOpEarlier.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOpEarlier.isCalculatedField = False
        Me.txtOpEarlier.IsSourceFromTable = False
        Me.txtOpEarlier.IsSourceFromValueList = False
        Me.txtOpEarlier.IsUnique = False
        Me.txtOpEarlier.Location = New System.Drawing.Point(177, 15)
        Me.txtOpEarlier.MaxLength = 12
        Me.txtOpEarlier.MendatroryField = False
        Me.txtOpEarlier.MyLinkLable1 = Nothing
        Me.txtOpEarlier.MyLinkLable2 = Nothing
        Me.txtOpEarlier.Name = "txtOpEarlier"
        Me.txtOpEarlier.ReferenceFieldDesc = Nothing
        Me.txtOpEarlier.ReferenceFieldName = Nothing
        Me.txtOpEarlier.ReferenceTableName = Nothing
        Me.txtOpEarlier.Size = New System.Drawing.Size(178, 18)
        Me.txtOpEarlier.TabIndex = 46
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(16, 103)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel8.TabIndex = 45
        Me.MyLabel8.Text = "Repairs"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(16, 81)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(132, 16)
        Me.MyLabel3.TabIndex = 44
        Me.MyLabel3.Text = "Particulars of Oil Change"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(16, 59)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(139, 16)
        Me.MyLabel2.TabIndex = 43
        Me.MyLabel2.Text = "Total No. of Hrs. Operated"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(16, 15)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(155, 16)
        Me.MyLabel4.TabIndex = 39
        Me.MyLabel4.Text = "No. of Hours Operated earlier"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(167, 9)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 4
        Me.btnPrint.Text = "Print"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(3, 9)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(76, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(740, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(78, 22)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(85, 9)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(76, 22)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'frmLogSheetEng
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(830, 598)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmLogSheetEng"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Log Sheet Eng"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConsumTypedes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gvParam.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvParam, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.txtRepair, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOilChange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtopTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOpEarlier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents lblBomDate As common.Controls.MyLabel
    Friend WithEvents dtpDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblConsumTypedes As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtSection As common.UserControls.txtFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvParam As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtConsumType As common.UserControls.txtFinder
    Friend WithEvents txtRepair As common.Controls.MyTextBox
    Friend WithEvents txtOilChange As common.Controls.MyTextBox
    Friend WithEvents txtopTotal As common.Controls.MyTextBox
    Friend WithEvents txtOp As common.Controls.MyTextBox
    Friend WithEvents txtOpEarlier As common.Controls.MyTextBox
    Friend WithEvents lblSection As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
End Class

