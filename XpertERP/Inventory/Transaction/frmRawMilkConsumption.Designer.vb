<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRawMilkConsumption
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
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.LblMainLocation = New common.Controls.MyLabel()
        Me.FndMainLocation = New common.UserControls.txtFinder()
        Me.ChkMilkType = New common.Controls.MyCheckBox()
        Me.chklocation = New common.Controls.MyCheckBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtBarCode = New common.Controls.MyTextBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.cboTransType = New common.Controls.MyComboBox()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.txtAdjustmentNo = New common.UserControls.txtNavigator()
        Me.UsLock1 = New common.usLock()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.txtReference = New common.Controls.MyTextBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.cmdEditAndPost = New Telerik.WinControls.UI.RadButton()
        Me.rbtnImportPosted = New Telerik.WinControls.UI.RadButton()
        Me.rbtnExportPosted = New Telerik.WinControls.UI.RadButton()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RmiExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.Exporttoexcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.ExporttoExcelWithSerial = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExcelforMilkType = New Telerik.WinControls.UI.RadMenuItem()
        Me.Opening = New Telerik.WinControls.UI.RadMenuItem()
        Me.OpeningExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.OpeningwithSerial = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmOpeningForMilkType = New Telerik.WinControls.UI.RadMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblMainLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkMilkType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chklocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBarCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTransType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReference, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdEditAndPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnImportPosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnExportPosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblMainLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.FndMainLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ChkMilkType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chklocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel16)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBarCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel15)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboTransType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAdjustmentNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtReference)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmdEditAndPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnImportPosted)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnExportPosted)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(913, 475)
        Me.SplitContainer1.SplitterDistance = 445
        Me.SplitContainer1.TabIndex = 0
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(15, 29)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel1.TabIndex = 19
        Me.MyLabel1.Text = "Main Location"
        '
        'LblMainLocation
        '
        Me.LblMainLocation.AutoSize = False
        Me.LblMainLocation.BorderVisible = True
        Me.LblMainLocation.FieldName = Nothing
        Me.LblMainLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMainLocation.Location = New System.Drawing.Point(240, 28)
        Me.LblMainLocation.Name = "LblMainLocation"
        Me.LblMainLocation.Size = New System.Drawing.Size(313, 18)
        Me.LblMainLocation.TabIndex = 21
        Me.LblMainLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblMainLocation.TextWrap = False
        '
        'FndMainLocation
        '
        Me.FndMainLocation.CalculationExpression = Nothing
        Me.FndMainLocation.FieldCode = Nothing
        Me.FndMainLocation.FieldDesc = Nothing
        Me.FndMainLocation.FieldMaxLength = 0
        Me.FndMainLocation.FieldName = Nothing
        Me.FndMainLocation.isCalculatedField = False
        Me.FndMainLocation.IsSourceFromTable = False
        Me.FndMainLocation.IsSourceFromValueList = False
        Me.FndMainLocation.IsUnique = False
        Me.FndMainLocation.Location = New System.Drawing.Point(132, 28)
        Me.FndMainLocation.MendatroryField = True
        Me.FndMainLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndMainLocation.MyLinkLable1 = Me.MyLabel1
        Me.FndMainLocation.MyLinkLable2 = Me.LblMainLocation
        Me.FndMainLocation.MyReadOnly = False
        Me.FndMainLocation.MyShowMasterFormButton = False
        Me.FndMainLocation.Name = "FndMainLocation"
        Me.FndMainLocation.ReferenceFieldDesc = Nothing
        Me.FndMainLocation.ReferenceFieldName = Nothing
        Me.FndMainLocation.ReferenceTableName = Nothing
        Me.FndMainLocation.Size = New System.Drawing.Size(104, 20)
        Me.FndMainLocation.TabIndex = 20
        Me.FndMainLocation.Value = ""
        '
        'ChkMilkType
        '
        Me.ChkMilkType.Location = New System.Drawing.Point(679, 7)
        Me.ChkMilkType.MyLinkLable1 = Nothing
        Me.ChkMilkType.MyLinkLable2 = Nothing
        Me.ChkMilkType.Name = "ChkMilkType"
        Me.ChkMilkType.Size = New System.Drawing.Size(69, 18)
        Me.ChkMilkType.TabIndex = 6
        Me.ChkMilkType.Tag1 = Nothing
        Me.ChkMilkType.Text = "Milk Type"
        '
        'chklocation
        '
        Me.chklocation.Location = New System.Drawing.Point(554, 51)
        Me.chklocation.MyLinkLable1 = Nothing
        Me.chklocation.MyLinkLable2 = Nothing
        Me.chklocation.Name = "chklocation"
        Me.chklocation.Size = New System.Drawing.Size(120, 18)
        Me.chklocation.TabIndex = 11
        Me.chklocation.Tag1 = Nothing
        Me.chklocation.Text = "Third Party Location"
        Me.chklocation.Visible = False
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(13, 116)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(54, 16)
        Me.MyLabel16.TabIndex = 16
        Me.MyLabel16.Text = "Bar Code"
        '
        'txtBarCode
        '
        Me.txtBarCode.CalculationExpression = Nothing
        Me.txtBarCode.FieldCode = Nothing
        Me.txtBarCode.FieldDesc = Nothing
        Me.txtBarCode.FieldMaxLength = 0
        Me.txtBarCode.FieldName = Nothing
        Me.txtBarCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarCode.isCalculatedField = False
        Me.txtBarCode.IsSourceFromTable = False
        Me.txtBarCode.IsSourceFromValueList = False
        Me.txtBarCode.IsUnique = False
        Me.txtBarCode.Location = New System.Drawing.Point(132, 115)
        Me.txtBarCode.MaxLength = 200
        Me.txtBarCode.MendatroryField = False
        Me.txtBarCode.MyLinkLable1 = Me.MyLabel16
        Me.txtBarCode.MyLinkLable2 = Nothing
        Me.txtBarCode.Name = "txtBarCode"
        Me.txtBarCode.ReferenceFieldDesc = Nothing
        Me.txtBarCode.ReferenceFieldName = Nothing
        Me.txtBarCode.ReferenceTableName = Nothing
        Me.txtBarCode.Size = New System.Drawing.Size(541, 18)
        Me.txtBarCode.TabIndex = 17
        Me.txtBarCode.TabStop = False
        '
        'MyLabel5
        '
        Me.MyLabel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.ForeColor = System.Drawing.SystemColors.Highlight
        Me.MyLabel5.Location = New System.Drawing.Point(709, 426)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(197, 16)
        Me.MyLabel5.TabIndex = 14
        Me.MyLabel5.Text = "Press F4 to open serial item Details"
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(14, 52)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 8
        Me.RadLabel15.Text = "Location"
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(239, 51)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(313, 18)
        Me.lblLocation.TabIndex = 10
        Me.lblLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocation.TextWrap = False
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
        Me.txtLocation.Location = New System.Drawing.Point(132, 51)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtLocation.MyLinkLable2 = Me.lblLocation
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(104, 20)
        Me.txtLocation.TabIndex = 9
        Me.txtLocation.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(14, 8)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(80, 16)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "Adjustment No"
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
        Me.txtDesc.Location = New System.Drawing.Point(132, 72)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel3
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(541, 18)
        Me.txtDesc.TabIndex = 13
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(14, 73)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel3.TabIndex = 12
        Me.RadLabel3.Text = "Description"
        '
        'cboTransType
        '
        Me.cboTransType.AutoCompleteDisplayMember = Nothing
        Me.cboTransType.AutoCompleteValueMember = Nothing
        Me.cboTransType.CalculationExpression = Nothing
        Me.cboTransType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboTransType.FieldCode = Nothing
        Me.cboTransType.FieldDesc = Nothing
        Me.cboTransType.FieldMaxLength = 0
        Me.cboTransType.FieldName = Nothing
        Me.cboTransType.isCalculatedField = False
        Me.cboTransType.IsSourceFromTable = False
        Me.cboTransType.IsSourceFromValueList = False
        Me.cboTransType.IsUnique = False
        Me.cboTransType.Location = New System.Drawing.Point(611, 6)
        Me.cboTransType.MendatroryField = True
        Me.cboTransType.MyLinkLable1 = Me.RadLabel8
        Me.cboTransType.MyLinkLable2 = Nothing
        Me.cboTransType.Name = "cboTransType"
        Me.cboTransType.ReferenceFieldDesc = Nothing
        Me.cboTransType.ReferenceFieldName = Nothing
        Me.cboTransType.ReferenceTableName = Nothing
        Me.cboTransType.Size = New System.Drawing.Size(62, 20)
        Me.cboTransType.TabIndex = 5
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(574, 8)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(31, 16)
        Me.RadLabel8.TabIndex = 4
        Me.RadLabel8.Text = "Type"
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
        Me.txtDate.Location = New System.Drawing.Point(440, 7)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(127, 18)
        Me.txtDate.TabIndex = 3
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(408, 8)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 2
        Me.RadLabel4.Text = "Date"
        '
        'txtAdjustmentNo
        '
        Me.txtAdjustmentNo.FieldName = Nothing
        Me.txtAdjustmentNo.Location = New System.Drawing.Point(132, 6)
        Me.txtAdjustmentNo.MendatroryField = False
        Me.txtAdjustmentNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtAdjustmentNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtAdjustmentNo.MyLinkLable1 = Me.RadLabel1
        Me.txtAdjustmentNo.MyLinkLable2 = Nothing
        Me.txtAdjustmentNo.MyMaxLength = 32767
        Me.txtAdjustmentNo.MyReadOnly = False
        Me.txtAdjustmentNo.Name = "txtAdjustmentNo"
        Me.txtAdjustmentNo.Size = New System.Drawing.Size(252, 20)
        Me.txtAdjustmentNo.TabIndex = 1
        Me.txtAdjustmentNo.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(756, 6)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 7
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(14, 94)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(58, 16)
        Me.RadLabel6.TabIndex = 14
        Me.RadLabel6.Text = "Reference"
        '
        'txtReference
        '
        Me.txtReference.CalculationExpression = Nothing
        Me.txtReference.FieldCode = Nothing
        Me.txtReference.FieldDesc = Nothing
        Me.txtReference.FieldMaxLength = 0
        Me.txtReference.FieldName = Nothing
        Me.txtReference.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReference.isCalculatedField = False
        Me.txtReference.IsSourceFromTable = False
        Me.txtReference.IsSourceFromValueList = False
        Me.txtReference.IsUnique = False
        Me.txtReference.Location = New System.Drawing.Point(132, 93)
        Me.txtReference.MaxLength = 200
        Me.txtReference.MendatroryField = False
        Me.txtReference.MyLinkLable1 = Me.RadLabel6
        Me.txtReference.MyLinkLable2 = Nothing
        Me.txtReference.Name = "txtReference"
        Me.txtReference.ReferenceFieldDesc = Nothing
        Me.txtReference.ReferenceFieldName = Nothing
        Me.txtReference.ReferenceTableName = Nothing
        Me.txtReference.Size = New System.Drawing.Size(541, 18)
        Me.txtReference.TabIndex = 15
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(5, 137)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(903, 287)
        Me.RadGroupBox2.TabIndex = 18
        Me.RadGroupBox2.Text = "Item Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(883, 257)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(384, 6)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 2
        '
        'cmdEditAndPost
        '
        Me.cmdEditAndPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdEditAndPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEditAndPost.Location = New System.Drawing.Point(328, 2)
        Me.cmdEditAndPost.Name = "cmdEditAndPost"
        Me.cmdEditAndPost.Size = New System.Drawing.Size(121, 22)
        Me.cmdEditAndPost.TabIndex = 8
        Me.cmdEditAndPost.Text = "Edit and Post"
        Me.cmdEditAndPost.Visible = False
        '
        'rbtnImportPosted
        '
        Me.rbtnImportPosted.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbtnImportPosted.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnImportPosted.Location = New System.Drawing.Point(582, 1)
        Me.rbtnImportPosted.Name = "rbtnImportPosted"
        Me.rbtnImportPosted.Size = New System.Drawing.Size(121, 22)
        Me.rbtnImportPosted.TabIndex = 7
        Me.rbtnImportPosted.Text = "Import Posted Data"
        Me.rbtnImportPosted.Visible = False
        '
        'rbtnExportPosted
        '
        Me.rbtnExportPosted.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbtnExportPosted.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnExportPosted.Location = New System.Drawing.Point(455, 2)
        Me.rbtnExportPosted.Name = "rbtnExportPosted"
        Me.rbtnExportPosted.Size = New System.Drawing.Size(121, 22)
        Me.rbtnExportPosted.TabIndex = 6
        Me.rbtnExportPosted.Text = "Export Posted Data"
        Me.rbtnExportPosted.Visible = False
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(709, 2)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(121, 22)
        Me.btnReverse.TabIndex = 4
        Me.btnReverse.Text = "Reverse and Unpost"
        Me.btnReverse.Visible = False
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(221, 2)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(69, 22)
        Me.RadButton1.TabIndex = 3
        Me.RadButton1.Text = "Print"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(149, 2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(77, 2)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(836, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(913, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Setting"
        Me.RadMenuItem1.AccessibleName = "Setting"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RmiExport, Me.Opening})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'RmiExport
        '
        Me.RmiExport.AccessibleDescription = "RadMenuItem2"
        Me.RmiExport.AccessibleName = "RadMenuItem2"
        Me.RmiExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Exporttoexcel, Me.ExporttoExcelWithSerial, Me.rmExcelforMilkType})
        Me.RmiExport.Name = "RmiExport"
        Me.RmiExport.Text = "Export excel"
        '
        'Exporttoexcel
        '
        Me.Exporttoexcel.AccessibleDescription = "Export to Excel"
        Me.Exporttoexcel.AccessibleName = "Export to Excel"
        Me.Exporttoexcel.Name = "Exporttoexcel"
        Me.Exporttoexcel.Text = "Export to Excel"
        '
        'ExporttoExcelWithSerial
        '
        Me.ExporttoExcelWithSerial.AccessibleDescription = "Export to Excel With Serial"
        Me.ExporttoExcelWithSerial.AccessibleName = "Export to Excel With Serial"
        Me.ExporttoExcelWithSerial.Name = "ExporttoExcelWithSerial"
        Me.ExporttoExcelWithSerial.Text = "Export to Excel With Serial"
        '
        'rmExcelforMilkType
        '
        Me.rmExcelforMilkType.AccessibleDescription = "RadMenuItem2"
        Me.rmExcelforMilkType.AccessibleName = "RadMenuItem2"
        Me.rmExcelforMilkType.Name = "rmExcelforMilkType"
        Me.rmExcelforMilkType.Text = "Export For Milk Type"
        '
        'Opening
        '
        Me.Opening.AccessibleDescription = "Opening"
        Me.Opening.AccessibleName = "Opening"
        Me.Opening.Items.AddRange(New Telerik.WinControls.RadItem() {Me.OpeningExcel, Me.OpeningwithSerial, Me.rmOpeningForMilkType})
        Me.Opening.Name = "Opening"
        Me.Opening.Text = "Opening"
        '
        'OpeningExcel
        '
        Me.OpeningExcel.AccessibleDescription = "Opening"
        Me.OpeningExcel.AccessibleName = "Opening"
        Me.OpeningExcel.Name = "OpeningExcel"
        Me.OpeningExcel.Text = "Opening"
        '
        'OpeningwithSerial
        '
        Me.OpeningwithSerial.AccessibleDescription = "Opening With Serial"
        Me.OpeningwithSerial.AccessibleName = "Opening With Serial"
        Me.OpeningwithSerial.Name = "OpeningwithSerial"
        Me.OpeningwithSerial.Text = "Opening With Serial"
        '
        'rmOpeningForMilkType
        '
        Me.rmOpeningForMilkType.AccessibleDescription = "Opening For Milk Type"
        Me.rmOpeningForMilkType.AccessibleName = "Opening For Milk Type"
        Me.rmOpeningForMilkType.Name = "rmOpeningForMilkType"
        Me.rmOpeningForMilkType.Text = "Opening For Milk Type"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(913, 475)
        Me.Panel1.TabIndex = 2
        '
        'frmAdjustmentStore
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(913, 495)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadMenu1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmAdjustmentStore"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Store Adjustment"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblMainLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkMilkType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chklocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBarCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTransType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReference, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdEditAndPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnImportPosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnExportPosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtAdjustmentNo As common.UserControls.txtNavigator
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents txtReference As common.Controls.MyTextBox
    Friend WithEvents cboTransType As common.Controls.MyComboBox
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RmiExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Opening As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtBarCode As common.Controls.MyTextBox
    Friend WithEvents chklocation As common.Controls.MyCheckBox
    Friend WithEvents Exporttoexcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ExporttoExcelWithSerial As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents OpeningExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents OpeningwithSerial As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ChkMilkType As common.Controls.MyCheckBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents LblMainLocation As common.Controls.MyLabel
    Friend WithEvents FndMainLocation As common.UserControls.txtFinder
    Friend WithEvents rbtnImportPosted As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnExportPosted As Telerik.WinControls.UI.RadButton
    Friend WithEvents cmdEditAndPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmExcelforMilkType As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmOpeningForMilkType As Telerik.WinControls.UI.RadMenuItem
End Class

