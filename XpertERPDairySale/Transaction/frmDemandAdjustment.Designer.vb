<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDemandAdjustment
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.rgbAdjustment = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnPre = New common.Controls.MyRadioButton()
        Me.rbtnQty = New common.Controls.MyRadioButton()
        Me.rgbShift = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnEvening = New common.Controls.MyRadioButton()
        Me.rbtnMorning = New common.Controls.MyRadioButton()
        Me.lblAreaDesc = New common.Controls.MyLabel()
        Me.lblArea = New common.Controls.MyLabel()
        Me.TxtArea = New common.UserControls.txtFinder()
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
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgbAdjustment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbAdjustment.SuspendLayout()
        CType(Me.rbtnPre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgbShift, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbShift.SuspendLayout()
        CType(Me.rbtnEvening, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnMorning, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAreaDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblArea, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Size = New System.Drawing.Size(671, 450)
        Me.SplitContainer1.SplitterDistance = 407
        Me.SplitContainer1.TabIndex = 0
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnGo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rgbAdjustment)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rgbShift)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblAreaDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblArea)
        Me.SplitContainer2.Panel1.Controls.Add(Me.TxtArea)
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
        Me.SplitContainer2.Size = New System.Drawing.Size(671, 407)
        Me.SplitContainer2.SplitterDistance = 120
        Me.SplitContainer2.TabIndex = 0
        '
        'btnGo
        '
        Me.btnGo.Location = New System.Drawing.Point(422, 81)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(233, 18)
        Me.btnGo.TabIndex = 162
        Me.btnGo.Text = "Go >>"
        '
        'rgbAdjustment
        '
        Me.rgbAdjustment.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbAdjustment.Controls.Add(Me.rbtnPre)
        Me.rgbAdjustment.Controls.Add(Me.rbtnQty)
        Me.rgbAdjustment.HeaderText = "Adjustment"
        Me.rgbAdjustment.Location = New System.Drawing.Point(540, 7)
        Me.rgbAdjustment.Name = "rgbAdjustment"
        Me.rgbAdjustment.Size = New System.Drawing.Size(115, 70)
        Me.rgbAdjustment.TabIndex = 161
        Me.rgbAdjustment.Text = "Adjustment"
        '
        'rbtnPre
        '
        Me.rbtnPre.Location = New System.Drawing.Point(16, 38)
        Me.rbtnPre.MyLinkLable1 = Nothing
        Me.rbtnPre.MyLinkLable2 = Nothing
        Me.rbtnPre.Name = "rbtnPre"
        Me.rbtnPre.Size = New System.Drawing.Size(76, 18)
        Me.rbtnPre.TabIndex = 3
        Me.rbtnPre.Text = "Percentage"
        '
        'rbtnQty
        '
        Me.rbtnQty.Location = New System.Drawing.Point(16, 18)
        Me.rbtnQty.MyLinkLable1 = Nothing
        Me.rbtnQty.MyLinkLable2 = Nothing
        Me.rbtnQty.Name = "rbtnQty"
        Me.rbtnQty.Size = New System.Drawing.Size(63, 18)
        Me.rbtnQty.TabIndex = 2
        Me.rbtnQty.Text = "Quantity"
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
        'lblAreaDesc
        '
        Me.lblAreaDesc.AutoSize = False
        Me.lblAreaDesc.BorderVisible = True
        Me.lblAreaDesc.FieldName = Nothing
        Me.lblAreaDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAreaDesc.Location = New System.Drawing.Point(186, 59)
        Me.lblAreaDesc.Name = "lblAreaDesc"
        Me.lblAreaDesc.Size = New System.Drawing.Size(227, 18)
        Me.lblAreaDesc.TabIndex = 159
        Me.lblAreaDesc.TextWrap = False
        '
        'lblArea
        '
        Me.lblArea.FieldName = Nothing
        Me.lblArea.Location = New System.Drawing.Point(24, 59)
        Me.lblArea.Name = "lblArea"
        Me.lblArea.Size = New System.Drawing.Size(29, 18)
        Me.lblArea.TabIndex = 158
        Me.lblArea.Text = "Area"
        '
        'TxtArea
        '
        Me.TxtArea.CalculationExpression = Nothing
        Me.TxtArea.Enabled = False
        Me.TxtArea.FieldCode = Nothing
        Me.TxtArea.FieldDesc = Nothing
        Me.TxtArea.FieldMaxLength = 0
        Me.TxtArea.FieldName = Nothing
        Me.TxtArea.isCalculatedField = False
        Me.TxtArea.IsSourceFromTable = False
        Me.TxtArea.IsSourceFromValueList = False
        Me.TxtArea.IsUnique = False
        Me.TxtArea.Location = New System.Drawing.Point(64, 59)
        Me.TxtArea.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtArea.MendatroryField = True
        Me.TxtArea.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtArea.MyLinkLable1 = Me.lblArea
        Me.TxtArea.MyLinkLable2 = Nothing
        Me.TxtArea.MyReadOnly = False
        Me.TxtArea.MyShowMasterFormButton = False
        Me.TxtArea.Name = "TxtArea"
        Me.TxtArea.ReferenceFieldDesc = Nothing
        Me.TxtArea.ReferenceFieldName = Nothing
        Me.TxtArea.ReferenceTableName = Nothing
        Me.TxtArea.Size = New System.Drawing.Size(115, 19)
        Me.TxtArea.TabIndex = 157
        Me.TxtArea.Value = ""
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
        Me.lblRoute.Location = New System.Drawing.Point(24, 82)
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
        Me.txtRoute.Location = New System.Drawing.Point(64, 82)
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
        Me.lblRouteDesc.Location = New System.Drawing.Point(186, 82)
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
        Me.GV1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.GV1.Name = "GV1"
        Me.GV1.Size = New System.Drawing.Size(671, 283)
        Me.GV1.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(577, 7)
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
        'frmDemandAdjustment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(671, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmDemandAdjustment"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Demand Adjustment"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgbAdjustment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbAdjustment.ResumeLayout(False)
        Me.rgbAdjustment.PerformLayout()
        CType(Me.rbtnPre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgbShift, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbShift.ResumeLayout(False)
        Me.rgbShift.PerformLayout()
        CType(Me.rbtnEvening, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnMorning, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAreaDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblArea, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents GV1 As RadGridView
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents lblAreaDesc As common.Controls.MyLabel
    Friend WithEvents lblArea As common.Controls.MyLabel
    Friend WithEvents TxtArea As common.UserControls.txtFinder
    Friend WithEvents lblZoneDesc As common.Controls.MyLabel
    Friend WithEvents lblZone As common.Controls.MyLabel
    Friend WithEvents txtZone As common.UserControls.txtFinder
    Friend WithEvents lblRoute As common.Controls.MyLabel
    Friend WithEvents txtRoute As common.UserControls.txtFinder
    Friend WithEvents lblRouteDesc As common.Controls.MyLabel
    Friend WithEvents rgbAdjustment As RadGroupBox
    Friend WithEvents rgbShift As RadGroupBox
    Friend WithEvents btnGo As RadButton
    Friend WithEvents rbtnPre As common.Controls.MyRadioButton
    Friend WithEvents rbtnQty As common.Controls.MyRadioButton
    Friend WithEvents rbtnEvening As common.Controls.MyRadioButton
    Friend WithEvents rbtnMorning As common.Controls.MyRadioButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents btnSave As RadButton
End Class
