<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmVSPDayWiseIncentive
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
        Me.rdmenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.File = New Telerik.WinControls.UI.RadMenuItem()
        Me.Import = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtDWIFrom5 = New common.MyNumBox()
        Me.MyLabel88 = New common.Controls.MyLabel()
        Me.txtDWIRate5 = New common.MyNumBox()
        Me.MyLabel90 = New common.Controls.MyLabel()
        Me.txtDWITo5 = New common.MyNumBox()
        Me.MyLabel91 = New common.Controls.MyLabel()
        Me.txtDWIFrom4 = New common.MyNumBox()
        Me.txtDWIRate4 = New common.MyNumBox()
        Me.txtDWITo4 = New common.MyNumBox()
        Me.txtDWIFrom3 = New common.MyNumBox()
        Me.txtDWIRate3 = New common.MyNumBox()
        Me.txtDWITo3 = New common.MyNumBox()
        Me.txtDWIFrom2 = New common.MyNumBox()
        Me.txtDWIRate2 = New common.MyNumBox()
        Me.txtDWITo2 = New common.MyNumBox()
        Me.txtDWIFrom1 = New common.MyNumBox()
        Me.txtDWIRate1 = New common.MyNumBox()
        Me.txtDWITo1 = New common.MyNumBox()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.rdbtnreset = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblAdvanceCode = New common.Controls.MyLabel()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.rdbtndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.rdmenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        CType(Me.txtDWIFrom5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel88, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDWIRate5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel90, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDWITo5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel91, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDWIFrom4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDWIRate4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDWITo4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDWIFrom3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDWIRate3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDWITo3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDWIFrom2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDWIRate2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDWITo2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDWIFrom1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDWIRate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDWITo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdvanceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdmenu1
        '
        Me.rdmenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.File})
        Me.rdmenu1.Location = New System.Drawing.Point(0, 0)
        Me.rdmenu1.Name = "rdmenu1"
        Me.rdmenu1.Size = New System.Drawing.Size(605, 20)
        Me.rdmenu1.TabIndex = 1
        '
        'File
        '
        Me.File.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Import, Me.Export, Me.RadMenuItem1})
        Me.File.Name = "File"
        Me.File.Text = "File"
        '
        'Import
        '
        Me.Import.Name = "Import"
        Me.Import.Text = "Import"
        '
        'Export
        '
        Me.Export.Name = "Export"
        Me.Export.Text = "Export"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "RadMenuItem1"
        Me.RadMenuItem1.AccessibleName = "RadMenuItem1"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Exit"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "RadMenuItem2"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdbtnreset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAdvanceCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(605, 365)
        Me.SplitContainer1.SplitterDistance = 321
        Me.SplitContainer1.TabIndex = 1
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.txtDWIFrom5)
        Me.RadGroupBox6.Controls.Add(Me.txtDWIRate5)
        Me.RadGroupBox6.Controls.Add(Me.txtDWITo5)
        Me.RadGroupBox6.Controls.Add(Me.txtDWIFrom4)
        Me.RadGroupBox6.Controls.Add(Me.txtDWIRate4)
        Me.RadGroupBox6.Controls.Add(Me.txtDWITo4)
        Me.RadGroupBox6.Controls.Add(Me.txtDWIFrom3)
        Me.RadGroupBox6.Controls.Add(Me.txtDWIRate3)
        Me.RadGroupBox6.Controls.Add(Me.txtDWITo3)
        Me.RadGroupBox6.Controls.Add(Me.txtDWIFrom2)
        Me.RadGroupBox6.Controls.Add(Me.txtDWIRate2)
        Me.RadGroupBox6.Controls.Add(Me.txtDWITo2)
        Me.RadGroupBox6.Controls.Add(Me.txtDWIFrom1)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel88)
        Me.RadGroupBox6.Controls.Add(Me.txtDWIRate1)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel90)
        Me.RadGroupBox6.Controls.Add(Me.txtDWITo1)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel91)
        Me.RadGroupBox6.HeaderText = ""
        Me.RadGroupBox6.Location = New System.Drawing.Point(96, 63)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Size = New System.Drawing.Size(233, 140)
        Me.RadGroupBox6.TabIndex = 7
        '
        'txtDWIFrom5
        '
        Me.txtDWIFrom5.BackColor = System.Drawing.Color.White
        Me.txtDWIFrom5.CalculationExpression = Nothing
        Me.txtDWIFrom5.DecimalPlaces = 2
        Me.txtDWIFrom5.FieldCode = Nothing
        Me.txtDWIFrom5.FieldDesc = Nothing
        Me.txtDWIFrom5.FieldMaxLength = 0
        Me.txtDWIFrom5.FieldName = Nothing
        Me.txtDWIFrom5.isCalculatedField = False
        Me.txtDWIFrom5.IsSourceFromTable = False
        Me.txtDWIFrom5.IsSourceFromValueList = False
        Me.txtDWIFrom5.IsUnique = False
        Me.txtDWIFrom5.Location = New System.Drawing.Point(5, 116)
        Me.txtDWIFrom5.MendatroryField = False
        Me.txtDWIFrom5.MyLinkLable1 = Me.MyLabel88
        Me.txtDWIFrom5.MyLinkLable2 = Nothing
        Me.txtDWIFrom5.Name = "txtDWIFrom5"
        Me.txtDWIFrom5.ReferenceFieldDesc = Nothing
        Me.txtDWIFrom5.ReferenceFieldName = Nothing
        Me.txtDWIFrom5.ReferenceTableName = Nothing
        Me.txtDWIFrom5.Size = New System.Drawing.Size(60, 20)
        Me.txtDWIFrom5.TabIndex = 12
        Me.txtDWIFrom5.Text = "0"
        Me.txtDWIFrom5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDWIFrom5.Value = 0R
        '
        'MyLabel88
        '
        Me.MyLabel88.FieldName = Nothing
        Me.MyLabel88.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel88.Location = New System.Drawing.Point(19, 2)
        Me.MyLabel88.Name = "MyLabel88"
        Me.MyLabel88.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel88.TabIndex = 15
        Me.MyLabel88.Text = "From"
        '
        'txtDWIRate5
        '
        Me.txtDWIRate5.BackColor = System.Drawing.Color.White
        Me.txtDWIRate5.CalculationExpression = Nothing
        Me.txtDWIRate5.DecimalPlaces = 2
        Me.txtDWIRate5.FieldCode = Nothing
        Me.txtDWIRate5.FieldDesc = Nothing
        Me.txtDWIRate5.FieldMaxLength = 0
        Me.txtDWIRate5.FieldName = Nothing
        Me.txtDWIRate5.isCalculatedField = False
        Me.txtDWIRate5.IsSourceFromTable = False
        Me.txtDWIRate5.IsSourceFromValueList = False
        Me.txtDWIRate5.IsUnique = False
        Me.txtDWIRate5.Location = New System.Drawing.Point(162, 116)
        Me.txtDWIRate5.MendatroryField = False
        Me.txtDWIRate5.MyLinkLable1 = Me.MyLabel90
        Me.txtDWIRate5.MyLinkLable2 = Nothing
        Me.txtDWIRate5.Name = "txtDWIRate5"
        Me.txtDWIRate5.ReferenceFieldDesc = Nothing
        Me.txtDWIRate5.ReferenceFieldName = Nothing
        Me.txtDWIRate5.ReferenceTableName = Nothing
        Me.txtDWIRate5.Size = New System.Drawing.Size(60, 20)
        Me.txtDWIRate5.TabIndex = 14
        Me.txtDWIRate5.Text = "0"
        Me.txtDWIRate5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDWIRate5.Value = 0R
        '
        'MyLabel90
        '
        Me.MyLabel90.FieldName = Nothing
        Me.MyLabel90.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel90.Location = New System.Drawing.Point(177, 1)
        Me.MyLabel90.Name = "MyLabel90"
        Me.MyLabel90.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel90.TabIndex = 17
        Me.MyLabel90.Text = "Rate"
        '
        'txtDWITo5
        '
        Me.txtDWITo5.BackColor = System.Drawing.Color.White
        Me.txtDWITo5.CalculationExpression = Nothing
        Me.txtDWITo5.DecimalPlaces = 2
        Me.txtDWITo5.FieldCode = Nothing
        Me.txtDWITo5.FieldDesc = Nothing
        Me.txtDWITo5.FieldMaxLength = 0
        Me.txtDWITo5.FieldName = Nothing
        Me.txtDWITo5.isCalculatedField = False
        Me.txtDWITo5.IsSourceFromTable = False
        Me.txtDWITo5.IsSourceFromValueList = False
        Me.txtDWITo5.IsUnique = False
        Me.txtDWITo5.Location = New System.Drawing.Point(84, 116)
        Me.txtDWITo5.MendatroryField = False
        Me.txtDWITo5.MyLinkLable1 = Me.MyLabel91
        Me.txtDWITo5.MyLinkLable2 = Nothing
        Me.txtDWITo5.Name = "txtDWITo5"
        Me.txtDWITo5.ReferenceFieldDesc = Nothing
        Me.txtDWITo5.ReferenceFieldName = Nothing
        Me.txtDWITo5.ReferenceTableName = Nothing
        Me.txtDWITo5.Size = New System.Drawing.Size(60, 20)
        Me.txtDWITo5.TabIndex = 13
        Me.txtDWITo5.Text = "0"
        Me.txtDWITo5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDWITo5.Value = 0R
        '
        'MyLabel91
        '
        Me.MyLabel91.FieldName = Nothing
        Me.MyLabel91.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel91.Location = New System.Drawing.Point(105, 3)
        Me.MyLabel91.Name = "MyLabel91"
        Me.MyLabel91.Size = New System.Drawing.Size(19, 16)
        Me.MyLabel91.TabIndex = 16
        Me.MyLabel91.Text = "To"
        '
        'txtDWIFrom4
        '
        Me.txtDWIFrom4.BackColor = System.Drawing.Color.White
        Me.txtDWIFrom4.CalculationExpression = Nothing
        Me.txtDWIFrom4.DecimalPlaces = 2
        Me.txtDWIFrom4.FieldCode = Nothing
        Me.txtDWIFrom4.FieldDesc = Nothing
        Me.txtDWIFrom4.FieldMaxLength = 0
        Me.txtDWIFrom4.FieldName = Nothing
        Me.txtDWIFrom4.isCalculatedField = False
        Me.txtDWIFrom4.IsSourceFromTable = False
        Me.txtDWIFrom4.IsSourceFromValueList = False
        Me.txtDWIFrom4.IsUnique = False
        Me.txtDWIFrom4.Location = New System.Drawing.Point(5, 92)
        Me.txtDWIFrom4.MendatroryField = False
        Me.txtDWIFrom4.MyLinkLable1 = Me.MyLabel88
        Me.txtDWIFrom4.MyLinkLable2 = Nothing
        Me.txtDWIFrom4.Name = "txtDWIFrom4"
        Me.txtDWIFrom4.ReferenceFieldDesc = Nothing
        Me.txtDWIFrom4.ReferenceFieldName = Nothing
        Me.txtDWIFrom4.ReferenceTableName = Nothing
        Me.txtDWIFrom4.Size = New System.Drawing.Size(60, 20)
        Me.txtDWIFrom4.TabIndex = 9
        Me.txtDWIFrom4.Text = "0"
        Me.txtDWIFrom4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDWIFrom4.Value = 0R
        '
        'txtDWIRate4
        '
        Me.txtDWIRate4.BackColor = System.Drawing.Color.White
        Me.txtDWIRate4.CalculationExpression = Nothing
        Me.txtDWIRate4.DecimalPlaces = 2
        Me.txtDWIRate4.FieldCode = Nothing
        Me.txtDWIRate4.FieldDesc = Nothing
        Me.txtDWIRate4.FieldMaxLength = 0
        Me.txtDWIRate4.FieldName = Nothing
        Me.txtDWIRate4.isCalculatedField = False
        Me.txtDWIRate4.IsSourceFromTable = False
        Me.txtDWIRate4.IsSourceFromValueList = False
        Me.txtDWIRate4.IsUnique = False
        Me.txtDWIRate4.Location = New System.Drawing.Point(162, 92)
        Me.txtDWIRate4.MendatroryField = False
        Me.txtDWIRate4.MyLinkLable1 = Me.MyLabel90
        Me.txtDWIRate4.MyLinkLable2 = Nothing
        Me.txtDWIRate4.Name = "txtDWIRate4"
        Me.txtDWIRate4.ReferenceFieldDesc = Nothing
        Me.txtDWIRate4.ReferenceFieldName = Nothing
        Me.txtDWIRate4.ReferenceTableName = Nothing
        Me.txtDWIRate4.Size = New System.Drawing.Size(60, 20)
        Me.txtDWIRate4.TabIndex = 11
        Me.txtDWIRate4.Text = "0"
        Me.txtDWIRate4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDWIRate4.Value = 0R
        '
        'txtDWITo4
        '
        Me.txtDWITo4.BackColor = System.Drawing.Color.White
        Me.txtDWITo4.CalculationExpression = Nothing
        Me.txtDWITo4.DecimalPlaces = 2
        Me.txtDWITo4.FieldCode = Nothing
        Me.txtDWITo4.FieldDesc = Nothing
        Me.txtDWITo4.FieldMaxLength = 0
        Me.txtDWITo4.FieldName = Nothing
        Me.txtDWITo4.isCalculatedField = False
        Me.txtDWITo4.IsSourceFromTable = False
        Me.txtDWITo4.IsSourceFromValueList = False
        Me.txtDWITo4.IsUnique = False
        Me.txtDWITo4.Location = New System.Drawing.Point(84, 92)
        Me.txtDWITo4.MendatroryField = False
        Me.txtDWITo4.MyLinkLable1 = Me.MyLabel91
        Me.txtDWITo4.MyLinkLable2 = Nothing
        Me.txtDWITo4.Name = "txtDWITo4"
        Me.txtDWITo4.ReferenceFieldDesc = Nothing
        Me.txtDWITo4.ReferenceFieldName = Nothing
        Me.txtDWITo4.ReferenceTableName = Nothing
        Me.txtDWITo4.Size = New System.Drawing.Size(60, 20)
        Me.txtDWITo4.TabIndex = 10
        Me.txtDWITo4.Text = "0"
        Me.txtDWITo4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDWITo4.Value = 0R
        '
        'txtDWIFrom3
        '
        Me.txtDWIFrom3.BackColor = System.Drawing.Color.White
        Me.txtDWIFrom3.CalculationExpression = Nothing
        Me.txtDWIFrom3.DecimalPlaces = 2
        Me.txtDWIFrom3.FieldCode = Nothing
        Me.txtDWIFrom3.FieldDesc = Nothing
        Me.txtDWIFrom3.FieldMaxLength = 0
        Me.txtDWIFrom3.FieldName = Nothing
        Me.txtDWIFrom3.isCalculatedField = False
        Me.txtDWIFrom3.IsSourceFromTable = False
        Me.txtDWIFrom3.IsSourceFromValueList = False
        Me.txtDWIFrom3.IsUnique = False
        Me.txtDWIFrom3.Location = New System.Drawing.Point(5, 68)
        Me.txtDWIFrom3.MendatroryField = False
        Me.txtDWIFrom3.MyLinkLable1 = Me.MyLabel88
        Me.txtDWIFrom3.MyLinkLable2 = Nothing
        Me.txtDWIFrom3.Name = "txtDWIFrom3"
        Me.txtDWIFrom3.ReferenceFieldDesc = Nothing
        Me.txtDWIFrom3.ReferenceFieldName = Nothing
        Me.txtDWIFrom3.ReferenceTableName = Nothing
        Me.txtDWIFrom3.Size = New System.Drawing.Size(60, 20)
        Me.txtDWIFrom3.TabIndex = 6
        Me.txtDWIFrom3.Text = "0"
        Me.txtDWIFrom3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDWIFrom3.Value = 0R
        '
        'txtDWIRate3
        '
        Me.txtDWIRate3.BackColor = System.Drawing.Color.White
        Me.txtDWIRate3.CalculationExpression = Nothing
        Me.txtDWIRate3.DecimalPlaces = 2
        Me.txtDWIRate3.FieldCode = Nothing
        Me.txtDWIRate3.FieldDesc = Nothing
        Me.txtDWIRate3.FieldMaxLength = 0
        Me.txtDWIRate3.FieldName = Nothing
        Me.txtDWIRate3.isCalculatedField = False
        Me.txtDWIRate3.IsSourceFromTable = False
        Me.txtDWIRate3.IsSourceFromValueList = False
        Me.txtDWIRate3.IsUnique = False
        Me.txtDWIRate3.Location = New System.Drawing.Point(162, 68)
        Me.txtDWIRate3.MendatroryField = False
        Me.txtDWIRate3.MyLinkLable1 = Me.MyLabel90
        Me.txtDWIRate3.MyLinkLable2 = Nothing
        Me.txtDWIRate3.Name = "txtDWIRate3"
        Me.txtDWIRate3.ReferenceFieldDesc = Nothing
        Me.txtDWIRate3.ReferenceFieldName = Nothing
        Me.txtDWIRate3.ReferenceTableName = Nothing
        Me.txtDWIRate3.Size = New System.Drawing.Size(60, 20)
        Me.txtDWIRate3.TabIndex = 8
        Me.txtDWIRate3.Text = "0"
        Me.txtDWIRate3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDWIRate3.Value = 0R
        '
        'txtDWITo3
        '
        Me.txtDWITo3.BackColor = System.Drawing.Color.White
        Me.txtDWITo3.CalculationExpression = Nothing
        Me.txtDWITo3.DecimalPlaces = 2
        Me.txtDWITo3.FieldCode = Nothing
        Me.txtDWITo3.FieldDesc = Nothing
        Me.txtDWITo3.FieldMaxLength = 0
        Me.txtDWITo3.FieldName = Nothing
        Me.txtDWITo3.isCalculatedField = False
        Me.txtDWITo3.IsSourceFromTable = False
        Me.txtDWITo3.IsSourceFromValueList = False
        Me.txtDWITo3.IsUnique = False
        Me.txtDWITo3.Location = New System.Drawing.Point(84, 68)
        Me.txtDWITo3.MendatroryField = False
        Me.txtDWITo3.MyLinkLable1 = Me.MyLabel91
        Me.txtDWITo3.MyLinkLable2 = Nothing
        Me.txtDWITo3.Name = "txtDWITo3"
        Me.txtDWITo3.ReferenceFieldDesc = Nothing
        Me.txtDWITo3.ReferenceFieldName = Nothing
        Me.txtDWITo3.ReferenceTableName = Nothing
        Me.txtDWITo3.Size = New System.Drawing.Size(60, 20)
        Me.txtDWITo3.TabIndex = 7
        Me.txtDWITo3.Text = "0"
        Me.txtDWITo3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDWITo3.Value = 0R
        '
        'txtDWIFrom2
        '
        Me.txtDWIFrom2.BackColor = System.Drawing.Color.White
        Me.txtDWIFrom2.CalculationExpression = Nothing
        Me.txtDWIFrom2.DecimalPlaces = 2
        Me.txtDWIFrom2.FieldCode = Nothing
        Me.txtDWIFrom2.FieldDesc = Nothing
        Me.txtDWIFrom2.FieldMaxLength = 0
        Me.txtDWIFrom2.FieldName = Nothing
        Me.txtDWIFrom2.isCalculatedField = False
        Me.txtDWIFrom2.IsSourceFromTable = False
        Me.txtDWIFrom2.IsSourceFromValueList = False
        Me.txtDWIFrom2.IsUnique = False
        Me.txtDWIFrom2.Location = New System.Drawing.Point(5, 44)
        Me.txtDWIFrom2.MendatroryField = False
        Me.txtDWIFrom2.MyLinkLable1 = Me.MyLabel88
        Me.txtDWIFrom2.MyLinkLable2 = Nothing
        Me.txtDWIFrom2.Name = "txtDWIFrom2"
        Me.txtDWIFrom2.ReferenceFieldDesc = Nothing
        Me.txtDWIFrom2.ReferenceFieldName = Nothing
        Me.txtDWIFrom2.ReferenceTableName = Nothing
        Me.txtDWIFrom2.Size = New System.Drawing.Size(60, 20)
        Me.txtDWIFrom2.TabIndex = 3
        Me.txtDWIFrom2.Text = "0"
        Me.txtDWIFrom2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDWIFrom2.Value = 0R
        '
        'txtDWIRate2
        '
        Me.txtDWIRate2.BackColor = System.Drawing.Color.White
        Me.txtDWIRate2.CalculationExpression = Nothing
        Me.txtDWIRate2.DecimalPlaces = 2
        Me.txtDWIRate2.FieldCode = Nothing
        Me.txtDWIRate2.FieldDesc = Nothing
        Me.txtDWIRate2.FieldMaxLength = 0
        Me.txtDWIRate2.FieldName = Nothing
        Me.txtDWIRate2.isCalculatedField = False
        Me.txtDWIRate2.IsSourceFromTable = False
        Me.txtDWIRate2.IsSourceFromValueList = False
        Me.txtDWIRate2.IsUnique = False
        Me.txtDWIRate2.Location = New System.Drawing.Point(162, 44)
        Me.txtDWIRate2.MendatroryField = False
        Me.txtDWIRate2.MyLinkLable1 = Me.MyLabel90
        Me.txtDWIRate2.MyLinkLable2 = Nothing
        Me.txtDWIRate2.Name = "txtDWIRate2"
        Me.txtDWIRate2.ReferenceFieldDesc = Nothing
        Me.txtDWIRate2.ReferenceFieldName = Nothing
        Me.txtDWIRate2.ReferenceTableName = Nothing
        Me.txtDWIRate2.Size = New System.Drawing.Size(60, 20)
        Me.txtDWIRate2.TabIndex = 5
        Me.txtDWIRate2.Text = "0"
        Me.txtDWIRate2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDWIRate2.Value = 0R
        '
        'txtDWITo2
        '
        Me.txtDWITo2.BackColor = System.Drawing.Color.White
        Me.txtDWITo2.CalculationExpression = Nothing
        Me.txtDWITo2.DecimalPlaces = 2
        Me.txtDWITo2.FieldCode = Nothing
        Me.txtDWITo2.FieldDesc = Nothing
        Me.txtDWITo2.FieldMaxLength = 0
        Me.txtDWITo2.FieldName = Nothing
        Me.txtDWITo2.isCalculatedField = False
        Me.txtDWITo2.IsSourceFromTable = False
        Me.txtDWITo2.IsSourceFromValueList = False
        Me.txtDWITo2.IsUnique = False
        Me.txtDWITo2.Location = New System.Drawing.Point(84, 44)
        Me.txtDWITo2.MendatroryField = False
        Me.txtDWITo2.MyLinkLable1 = Me.MyLabel91
        Me.txtDWITo2.MyLinkLable2 = Nothing
        Me.txtDWITo2.Name = "txtDWITo2"
        Me.txtDWITo2.ReferenceFieldDesc = Nothing
        Me.txtDWITo2.ReferenceFieldName = Nothing
        Me.txtDWITo2.ReferenceTableName = Nothing
        Me.txtDWITo2.Size = New System.Drawing.Size(60, 20)
        Me.txtDWITo2.TabIndex = 4
        Me.txtDWITo2.Text = "0"
        Me.txtDWITo2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDWITo2.Value = 0R
        '
        'txtDWIFrom1
        '
        Me.txtDWIFrom1.BackColor = System.Drawing.Color.White
        Me.txtDWIFrom1.CalculationExpression = Nothing
        Me.txtDWIFrom1.DecimalPlaces = 2
        Me.txtDWIFrom1.FieldCode = Nothing
        Me.txtDWIFrom1.FieldDesc = Nothing
        Me.txtDWIFrom1.FieldMaxLength = 0
        Me.txtDWIFrom1.FieldName = Nothing
        Me.txtDWIFrom1.isCalculatedField = False
        Me.txtDWIFrom1.IsSourceFromTable = False
        Me.txtDWIFrom1.IsSourceFromValueList = False
        Me.txtDWIFrom1.IsUnique = False
        Me.txtDWIFrom1.Location = New System.Drawing.Point(5, 20)
        Me.txtDWIFrom1.MendatroryField = False
        Me.txtDWIFrom1.MyLinkLable1 = Me.MyLabel88
        Me.txtDWIFrom1.MyLinkLable2 = Nothing
        Me.txtDWIFrom1.Name = "txtDWIFrom1"
        Me.txtDWIFrom1.ReferenceFieldDesc = Nothing
        Me.txtDWIFrom1.ReferenceFieldName = Nothing
        Me.txtDWIFrom1.ReferenceTableName = Nothing
        Me.txtDWIFrom1.Size = New System.Drawing.Size(60, 20)
        Me.txtDWIFrom1.TabIndex = 0
        Me.txtDWIFrom1.Text = "0"
        Me.txtDWIFrom1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDWIFrom1.Value = 0R
        '
        'txtDWIRate1
        '
        Me.txtDWIRate1.BackColor = System.Drawing.Color.White
        Me.txtDWIRate1.CalculationExpression = Nothing
        Me.txtDWIRate1.DecimalPlaces = 2
        Me.txtDWIRate1.FieldCode = Nothing
        Me.txtDWIRate1.FieldDesc = Nothing
        Me.txtDWIRate1.FieldMaxLength = 0
        Me.txtDWIRate1.FieldName = Nothing
        Me.txtDWIRate1.isCalculatedField = False
        Me.txtDWIRate1.IsSourceFromTable = False
        Me.txtDWIRate1.IsSourceFromValueList = False
        Me.txtDWIRate1.IsUnique = False
        Me.txtDWIRate1.Location = New System.Drawing.Point(162, 20)
        Me.txtDWIRate1.MendatroryField = False
        Me.txtDWIRate1.MyLinkLable1 = Me.MyLabel90
        Me.txtDWIRate1.MyLinkLable2 = Nothing
        Me.txtDWIRate1.Name = "txtDWIRate1"
        Me.txtDWIRate1.ReferenceFieldDesc = Nothing
        Me.txtDWIRate1.ReferenceFieldName = Nothing
        Me.txtDWIRate1.ReferenceTableName = Nothing
        Me.txtDWIRate1.Size = New System.Drawing.Size(60, 20)
        Me.txtDWIRate1.TabIndex = 2
        Me.txtDWIRate1.Text = "0"
        Me.txtDWIRate1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDWIRate1.Value = 0R
        '
        'txtDWITo1
        '
        Me.txtDWITo1.BackColor = System.Drawing.Color.White
        Me.txtDWITo1.CalculationExpression = Nothing
        Me.txtDWITo1.DecimalPlaces = 2
        Me.txtDWITo1.FieldCode = Nothing
        Me.txtDWITo1.FieldDesc = Nothing
        Me.txtDWITo1.FieldMaxLength = 0
        Me.txtDWITo1.FieldName = Nothing
        Me.txtDWITo1.isCalculatedField = False
        Me.txtDWITo1.IsSourceFromTable = False
        Me.txtDWITo1.IsSourceFromValueList = False
        Me.txtDWITo1.IsUnique = False
        Me.txtDWITo1.Location = New System.Drawing.Point(84, 20)
        Me.txtDWITo1.MendatroryField = False
        Me.txtDWITo1.MyLinkLable1 = Me.MyLabel91
        Me.txtDWITo1.MyLinkLable2 = Nothing
        Me.txtDWITo1.Name = "txtDWITo1"
        Me.txtDWITo1.ReferenceFieldDesc = Nothing
        Me.txtDWITo1.ReferenceFieldName = Nothing
        Me.txtDWITo1.ReferenceTableName = Nothing
        Me.txtDWITo1.Size = New System.Drawing.Size(60, 20)
        Me.txtDWITo1.TabIndex = 1
        Me.txtDWITo1.Text = "0"
        Me.txtDWITo1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDWITo1.Value = 0R
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblDescription.Location = New System.Drawing.Point(14, 41)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 18)
        Me.lblDescription.TabIndex = 6
        Me.lblDescription.Text = "Description"
        '
        'txtDescription
        '
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(96, 40)
        Me.txtDescription.MaxLength = 100
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Nothing
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(233, 20)
        Me.txtDescription.TabIndex = 1
        '
        'rdbtnreset
        '
        Me.rdbtnreset.Image = Global.ERP.My.Resources.Resources._new
        Me.rdbtnreset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdbtnreset.Location = New System.Drawing.Point(311, 16)
        Me.rdbtnreset.Name = "rdbtnreset"
        Me.rdbtnreset.Size = New System.Drawing.Size(18, 21)
        Me.rdbtnreset.TabIndex = 1
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(96, 16)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblAdvanceCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(216, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblAdvanceCode
        '
        Me.lblAdvanceCode.FieldName = Nothing
        Me.lblAdvanceCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblAdvanceCode.Location = New System.Drawing.Point(14, 17)
        Me.lblAdvanceCode.Name = "lblAdvanceCode"
        Me.lblAdvanceCode.Size = New System.Drawing.Size(32, 18)
        Me.lblAdvanceCode.TabIndex = 2
        Me.lblAdvanceCode.Text = "Code"
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(143, 10)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(66, 23)
        Me.btnHistory.TabIndex = 108
        Me.btnHistory.Text = "&History"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(5, 10)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 23)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Location = New System.Drawing.Point(536, 10)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(66, 23)
        Me.rdbtnclose.TabIndex = 2
        Me.rdbtnclose.Text = "Close"
        '
        'rdbtndelete
        '
        Me.rdbtndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtndelete.Location = New System.Drawing.Point(74, 10)
        Me.rdbtndelete.Name = "rdbtndelete"
        Me.rdbtndelete.Size = New System.Drawing.Size(66, 23)
        Me.rdbtndelete.TabIndex = 1
        Me.rdbtndelete.Text = "Delete"
        '
        'frmVSPDayWiseIncentive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(605, 385)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenu1)
        Me.Name = "frmVSPDayWiseIncentive"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "VSP Day Wise Incentive"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.rdmenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.RadGroupBox6.PerformLayout()
        CType(Me.txtDWIFrom5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel88, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDWIRate5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel90, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDWITo5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel91, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDWIFrom4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDWIRate4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDWITo4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDWIFrom3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDWIRate3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDWITo3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDWIFrom2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDWIRate2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDWITo2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDWIFrom1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDWIRate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDWITo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdvanceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdmenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents File As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblAdvanceCode As common.Controls.MyLabel
    Friend WithEvents rdbtnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents Import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox6 As RadGroupBox
    Friend WithEvents txtDWIFrom5 As common.MyNumBox
    Friend WithEvents MyLabel88 As common.Controls.MyLabel
    Friend WithEvents txtDWIRate5 As common.MyNumBox
    Friend WithEvents MyLabel90 As common.Controls.MyLabel
    Friend WithEvents txtDWITo5 As common.MyNumBox
    Friend WithEvents MyLabel91 As common.Controls.MyLabel
    Friend WithEvents txtDWIFrom4 As common.MyNumBox
    Friend WithEvents txtDWIRate4 As common.MyNumBox
    Friend WithEvents txtDWITo4 As common.MyNumBox
    Friend WithEvents txtDWIFrom3 As common.MyNumBox
    Friend WithEvents txtDWIRate3 As common.MyNumBox
    Friend WithEvents txtDWITo3 As common.MyNumBox
    Friend WithEvents txtDWIFrom2 As common.MyNumBox
    Friend WithEvents txtDWIRate2 As common.MyNumBox
    Friend WithEvents txtDWITo2 As common.MyNumBox
    Friend WithEvents txtDWIFrom1 As common.MyNumBox
    Friend WithEvents txtDWIRate1 As common.MyNumBox
    Friend WithEvents txtDWITo1 As common.MyNumBox
End Class
