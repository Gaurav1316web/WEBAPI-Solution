<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDemandApproval
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
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.lblDistributorNameDesc = New common.Controls.MyLabel()
        Me.lblDistributorName = New common.Controls.MyLabel()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.rgbShift = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnEvening = New common.Controls.MyRadioButton()
        Me.rbtnMorning = New common.Controls.MyRadioButton()
        Me.lblZoneDesc = New common.Controls.MyLabel()
        Me.lblZone = New common.Controls.MyLabel()
        Me.txtZone = New common.UserControls.txtFinder()
        Me.lblRoute = New common.Controls.MyLabel()
        Me.txtRoute = New common.UserControls.txtFinder()
        Me.lblRouteDesc = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblDate = New common.Controls.MyLabel()
        Me.GV1 = New Telerik.WinControls.UI.RadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.rgbSecurity = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblDiffAmt = New common.Controls.MyLabel()
        Me.lblBAmt = New common.Controls.MyLabel()
        Me.lblDocAmt = New common.Controls.MyLabel()
        Me.lblSAmt = New common.Controls.MyLabel()
        Me.lblSAmtDesc = New common.Controls.MyLabel()
        Me.lblBAmtDesc = New common.Controls.MyLabel()
        Me.lblDocAmtDesc = New common.Controls.MyLabel()
        Me.lblDiffAmtDesc = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblDistributorNameDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDistributorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgbShift, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbShift.SuspendLayout()
        CType(Me.rbtnEvening, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnMorning, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblZoneDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblZone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgbSecurity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbSecurity.SuspendLayout()
        CType(Me.lblDiffAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSAmtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBAmtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocAmtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDiffAmtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
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
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 407
        Me.SplitContainer1.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.rgbSecurity)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDistributorNameDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDistributorName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnGo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rgbShift)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblZoneDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblZone)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtZone)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRoute)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRoute)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRouteDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDate)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GV1)
        Me.SplitContainer2.Size = New System.Drawing.Size(800, 407)
        Me.SplitContainer2.SplitterDistance = 120
        Me.SplitContainer2.TabIndex = 0
        '
        'lblDistributorNameDesc
        '
        Me.lblDistributorNameDesc.AutoSize = False
        Me.lblDistributorNameDesc.BorderVisible = True
        Me.lblDistributorNameDesc.FieldName = Nothing
        Me.lblDistributorNameDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDistributorNameDesc.Location = New System.Drawing.Point(117, 78)
        Me.lblDistributorNameDesc.Name = "lblDistributorNameDesc"
        Me.lblDistributorNameDesc.Size = New System.Drawing.Size(296, 18)
        Me.lblDistributorNameDesc.TabIndex = 153
        Me.lblDistributorNameDesc.TextWrap = False
        '
        'lblDistributorName
        '
        Me.lblDistributorName.FieldName = Nothing
        Me.lblDistributorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDistributorName.Location = New System.Drawing.Point(24, 79)
        Me.lblDistributorName.Name = "lblDistributorName"
        Me.lblDistributorName.Size = New System.Drawing.Size(91, 16)
        Me.lblDistributorName.TabIndex = 154
        Me.lblDistributorName.Text = "Distributor Name"
        '
        'btnGo
        '
        Me.btnGo.Location = New System.Drawing.Point(422, 81)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(115, 18)
        Me.btnGo.TabIndex = 162
        Me.btnGo.Text = "Go >>"
        '
        'rgbShift
        '
        Me.rgbShift.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbShift.Controls.Add(Me.rbtnEvening)
        Me.rgbShift.Controls.Add(Me.rbtnMorning)
        Me.rgbShift.HeaderText = "Shift"
        Me.rgbShift.Location = New System.Drawing.Point(422, 6)
        Me.rgbShift.Name = "rgbShift"
        Me.rgbShift.Size = New System.Drawing.Size(115, 70)
        Me.rgbShift.TabIndex = 160
        Me.rgbShift.Text = "Shift"
        '
        'rbtnEvening
        '
        Me.rbtnEvening.Location = New System.Drawing.Point(20, 42)
        Me.rbtnEvening.MyLinkLable1 = Nothing
        Me.rbtnEvening.MyLinkLable2 = Nothing
        Me.rbtnEvening.Name = "rbtnEvening"
        Me.rbtnEvening.Size = New System.Drawing.Size(59, 18)
        Me.rbtnEvening.TabIndex = 1
        Me.rbtnEvening.Text = "Evening"
        '
        'rbtnMorning
        '
        Me.rbtnMorning.Location = New System.Drawing.Point(20, 22)
        Me.rbtnMorning.MyLinkLable1 = Nothing
        Me.rbtnMorning.MyLinkLable2 = Nothing
        Me.rbtnMorning.Name = "rbtnMorning"
        Me.rbtnMorning.Size = New System.Drawing.Size(63, 18)
        Me.rbtnMorning.TabIndex = 0
        Me.rbtnMorning.Text = "Morning"
        '
        'lblZoneDesc
        '
        Me.lblZoneDesc.AutoSize = False
        Me.lblZoneDesc.BorderVisible = True
        Me.lblZoneDesc.FieldName = Nothing
        Me.lblZoneDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZoneDesc.Location = New System.Drawing.Point(186, 34)
        Me.lblZoneDesc.Name = "lblZoneDesc"
        Me.lblZoneDesc.Size = New System.Drawing.Size(227, 18)
        Me.lblZoneDesc.TabIndex = 156
        Me.lblZoneDesc.TextWrap = False
        '
        'lblZone
        '
        Me.lblZone.FieldName = Nothing
        Me.lblZone.Location = New System.Drawing.Point(24, 34)
        Me.lblZone.Name = "lblZone"
        Me.lblZone.Size = New System.Drawing.Size(32, 18)
        Me.lblZone.TabIndex = 155
        Me.lblZone.Text = "Zone"
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
        Me.txtZone.Location = New System.Drawing.Point(64, 34)
        Me.txtZone.Margin = New System.Windows.Forms.Padding(4)
        Me.txtZone.MendatroryField = True
        Me.txtZone.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtZone.MyLinkLable1 = Me.lblZone
        Me.txtZone.MyLinkLable2 = Nothing
        Me.txtZone.MyReadOnly = False
        Me.txtZone.MyShowMasterFormButton = False
        Me.txtZone.Name = "txtZone"
        Me.txtZone.ReferenceFieldDesc = Nothing
        Me.txtZone.ReferenceFieldName = Nothing
        Me.txtZone.ReferenceTableName = Nothing
        Me.txtZone.Size = New System.Drawing.Size(115, 19)
        Me.txtZone.TabIndex = 154
        Me.txtZone.Value = ""
        '
        'lblRoute
        '
        Me.lblRoute.FieldName = Nothing
        Me.lblRoute.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoute.Location = New System.Drawing.Point(24, 57)
        Me.lblRoute.Name = "lblRoute"
        Me.lblRoute.Size = New System.Drawing.Size(36, 16)
        Me.lblRoute.TabIndex = 153
        Me.lblRoute.Text = "Route"
        '
        'txtRoute
        '
        Me.txtRoute.CalculationExpression = Nothing
        Me.txtRoute.Enabled = False
        Me.txtRoute.FieldCode = Nothing
        Me.txtRoute.FieldDesc = Nothing
        Me.txtRoute.FieldMaxLength = 0
        Me.txtRoute.FieldName = Nothing
        Me.txtRoute.isCalculatedField = False
        Me.txtRoute.IsSourceFromTable = False
        Me.txtRoute.IsSourceFromValueList = False
        Me.txtRoute.IsUnique = False
        Me.txtRoute.Location = New System.Drawing.Point(64, 57)
        Me.txtRoute.Margin = New System.Windows.Forms.Padding(4)
        Me.txtRoute.MendatroryField = True
        Me.txtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoute.MyLinkLable1 = Me.lblRoute
        Me.txtRoute.MyLinkLable2 = Me.lblRouteDesc
        Me.txtRoute.MyReadOnly = False
        Me.txtRoute.MyShowMasterFormButton = False
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.ReferenceFieldDesc = Nothing
        Me.txtRoute.ReferenceFieldName = Nothing
        Me.txtRoute.ReferenceTableName = Nothing
        Me.txtRoute.Size = New System.Drawing.Size(115, 18)
        Me.txtRoute.TabIndex = 151
        Me.txtRoute.Value = ""
        '
        'lblRouteDesc
        '
        Me.lblRouteDesc.AutoSize = False
        Me.lblRouteDesc.BorderVisible = True
        Me.lblRouteDesc.FieldName = Nothing
        Me.lblRouteDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRouteDesc.Location = New System.Drawing.Point(186, 57)
        Me.lblRouteDesc.Name = "lblRouteDesc"
        Me.lblRouteDesc.Size = New System.Drawing.Size(227, 18)
        Me.lblRouteDesc.TabIndex = 152
        Me.lblRouteDesc.TextWrap = False
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
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
        Me.txtDate.Location = New System.Drawing.Point(65, 11)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(114, 18)
        Me.txtDate.TabIndex = 50
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "04/07/2023"
        Me.txtDate.Value = New Date(2023, 7, 4, 0, 0, 0, 0)
        '
        'lblDate
        '
        Me.lblDate.FieldName = Nothing
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDate.Location = New System.Drawing.Point(24, 12)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(31, 16)
        Me.lblDate.TabIndex = 49
        Me.lblDate.Text = "Date"
        '
        'GV1
        '
        Me.GV1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GV1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.GV1.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.GV1.Name = "GV1"
        Me.GV1.Size = New System.Drawing.Size(800, 283)
        Me.GV1.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(706, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(86, 24)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(179, 7)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(74, 24)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(101, 7)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(74, 24)
        Me.btnReset.TabIndex = 1
        Me.btnReset.Text = "Reset"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(24, 7)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(74, 24)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'rgbSecurity
        '
        Me.rgbSecurity.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbSecurity.Controls.Add(Me.lblDocAmtDesc)
        Me.rgbSecurity.Controls.Add(Me.lblSAmtDesc)
        Me.rgbSecurity.Controls.Add(Me.lblDiffAmtDesc)
        Me.rgbSecurity.Controls.Add(Me.lblBAmtDesc)
        Me.rgbSecurity.Controls.Add(Me.lblDiffAmt)
        Me.rgbSecurity.Controls.Add(Me.lblBAmt)
        Me.rgbSecurity.Controls.Add(Me.lblDocAmt)
        Me.rgbSecurity.Controls.Add(Me.lblSAmt)
        Me.rgbSecurity.HeaderText = "Customer Security"
        Me.rgbSecurity.Location = New System.Drawing.Point(555, 3)
        Me.rgbSecurity.Name = "rgbSecurity"
        Me.rgbSecurity.Size = New System.Drawing.Size(237, 105)
        Me.rgbSecurity.TabIndex = 161
        Me.rgbSecurity.Text = "Customer Security"
        '
        'lblDiffAmt
        '
        Me.lblDiffAmt.FieldName = Nothing
        Me.lblDiffAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiffAmt.Location = New System.Drawing.Point(5, 81)
        Me.lblDiffAmt.Name = "lblDiffAmt"
        Me.lblDiffAmt.Size = New System.Drawing.Size(46, 16)
        Me.lblDiffAmt.TabIndex = 158
        Me.lblDiffAmt.Text = "Diff Amt"
        '
        'lblBAmt
        '
        Me.lblBAmt.FieldName = Nothing
        Me.lblBAmt.Location = New System.Drawing.Point(5, 36)
        Me.lblBAmt.Name = "lblBAmt"
        Me.lblBAmt.Size = New System.Drawing.Size(92, 18)
        Me.lblBAmt.TabIndex = 159
        Me.lblBAmt.Text = "Outstanding Amt"
        '
        'lblDocAmt
        '
        Me.lblDocAmt.FieldName = Nothing
        Me.lblDocAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocAmt.Location = New System.Drawing.Point(5, 59)
        Me.lblDocAmt.Name = "lblDocAmt"
        Me.lblDocAmt.Size = New System.Drawing.Size(55, 16)
        Me.lblDocAmt.TabIndex = 157
        Me.lblDocAmt.Text = "Doc Total"
        '
        'lblSAmt
        '
        Me.lblSAmt.FieldName = Nothing
        Me.lblSAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblSAmt.Location = New System.Drawing.Point(5, 17)
        Me.lblSAmt.Name = "lblSAmt"
        Me.lblSAmt.Size = New System.Drawing.Size(76, 16)
        Me.lblSAmt.TabIndex = 156
        Me.lblSAmt.Text = "Security Amt"
        '
        'lblSAmtDesc
        '
        Me.lblSAmtDesc.AutoSize = False
        Me.lblSAmtDesc.BorderVisible = True
        Me.lblSAmtDesc.FieldName = Nothing
        Me.lblSAmtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSAmtDesc.Location = New System.Drawing.Point(96, 17)
        Me.lblSAmtDesc.Name = "lblSAmtDesc"
        Me.lblSAmtDesc.Size = New System.Drawing.Size(136, 18)
        Me.lblSAmtDesc.TabIndex = 161
        Me.lblSAmtDesc.TextWrap = False
        '
        'lblBAmtDesc
        '
        Me.lblBAmtDesc.AutoSize = False
        Me.lblBAmtDesc.BorderVisible = True
        Me.lblBAmtDesc.FieldName = Nothing
        Me.lblBAmtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBAmtDesc.Location = New System.Drawing.Point(96, 37)
        Me.lblBAmtDesc.Name = "lblBAmtDesc"
        Me.lblBAmtDesc.Size = New System.Drawing.Size(136, 18)
        Me.lblBAmtDesc.TabIndex = 160
        Me.lblBAmtDesc.TextWrap = False
        '
        'lblDocAmtDesc
        '
        Me.lblDocAmtDesc.AutoSize = False
        Me.lblDocAmtDesc.BorderVisible = True
        Me.lblDocAmtDesc.FieldName = Nothing
        Me.lblDocAmtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocAmtDesc.Location = New System.Drawing.Point(96, 58)
        Me.lblDocAmtDesc.Name = "lblDocAmtDesc"
        Me.lblDocAmtDesc.Size = New System.Drawing.Size(136, 18)
        Me.lblDocAmtDesc.TabIndex = 163
        Me.lblDocAmtDesc.TextWrap = False
        '
        'lblDiffAmtDesc
        '
        Me.lblDiffAmtDesc.AutoSize = False
        Me.lblDiffAmtDesc.BorderVisible = True
        Me.lblDiffAmtDesc.FieldName = Nothing
        Me.lblDiffAmtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiffAmtDesc.Location = New System.Drawing.Point(96, 78)
        Me.lblDiffAmtDesc.Name = "lblDiffAmtDesc"
        Me.lblDiffAmtDesc.Size = New System.Drawing.Size(136, 18)
        Me.lblDiffAmtDesc.TabIndex = 162
        Me.lblDiffAmtDesc.TextWrap = False
        '
        'frmDemandApproval
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmDemandApproval"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Demand Approval"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblDistributorNameDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDistributorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgbShift, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbShift.ResumeLayout(False)
        Me.rgbShift.PerformLayout()
        CType(Me.rbtnEvening, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnMorning, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblZoneDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblZone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgbSecurity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbSecurity.ResumeLayout(False)
        Me.rgbSecurity.PerformLayout()
        CType(Me.lblDiffAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSAmtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBAmtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocAmtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDiffAmtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents btnGo As RadButton
    Friend WithEvents rgbShift As RadGroupBox
    Friend WithEvents rbtnEvening As common.Controls.MyRadioButton
    Friend WithEvents rbtnMorning As common.Controls.MyRadioButton
    Friend WithEvents lblZoneDesc As common.Controls.MyLabel
    Friend WithEvents lblZone As common.Controls.MyLabel
    Friend WithEvents txtZone As common.UserControls.txtFinder
    Friend WithEvents lblRoute As common.Controls.MyLabel
    Friend WithEvents txtRoute As common.UserControls.txtFinder
    Friend WithEvents lblRouteDesc As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents GV1 As RadGridView
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents btnSave As RadButton
    Friend WithEvents lblDistributorNameDesc As common.Controls.MyLabel
    Friend WithEvents lblDistributorName As common.Controls.MyLabel
    Friend WithEvents rgbSecurity As RadGroupBox
    Friend WithEvents lblDocAmtDesc As common.Controls.MyLabel
    Friend WithEvents lblSAmtDesc As common.Controls.MyLabel
    Friend WithEvents lblDiffAmtDesc As common.Controls.MyLabel
    Friend WithEvents lblBAmtDesc As common.Controls.MyLabel
    Friend WithEvents lblDiffAmt As common.Controls.MyLabel
    Friend WithEvents lblBAmt As common.Controls.MyLabel
    Friend WithEvents lblDocAmt As common.Controls.MyLabel
    Friend WithEvents lblSAmt As common.Controls.MyLabel
End Class
