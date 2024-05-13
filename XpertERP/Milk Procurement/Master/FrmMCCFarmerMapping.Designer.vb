<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMCCFarmerMapping
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.lblVLCName = New common.Controls.MyLabel()
        Me.fndVLCCode = New common.UserControls.txtFinder()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.lblRouteName = New common.Controls.MyLabel()
        Me.fndRouteCode = New common.UserControls.txtFinder()
        Me.lblRoute = New common.Controls.MyLabel()
        Me.lblMCCName = New common.Controls.MyLabel()
        Me.FndMCC = New common.UserControls.txtFinder()
        Me.lblMCC = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgVSP = New common.MyCheckBoxGrid()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.TxtFinder1 = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnUnSelect = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblVLCName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUnSelect)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1118, 523)
        Me.SplitContainer1.SplitterDistance = 480
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblVLCName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndVLCCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel10)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRouteName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndRouteCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRoute)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblMCCName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.FndMCC)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblMCC)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.TxtFinder1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(1118, 480)
        Me.SplitContainer2.SplitterDistance = 256
        Me.SplitContainer2.TabIndex = 113
        '
        'lblVLCName
        '
        Me.lblVLCName.AutoSize = False
        Me.lblVLCName.BorderVisible = True
        Me.lblVLCName.FieldName = Nothing
        Me.lblVLCName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVLCName.Location = New System.Drawing.Point(249, 65)
        Me.lblVLCName.Name = "lblVLCName"
        Me.lblVLCName.Size = New System.Drawing.Size(205, 18)
        Me.lblVLCName.TabIndex = 165
        Me.lblVLCName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVLCName.TextWrap = False
        '
        'fndVLCCode
        '
        Me.fndVLCCode.CalculationExpression = Nothing
        Me.fndVLCCode.FieldCode = Nothing
        Me.fndVLCCode.FieldDesc = Nothing
        Me.fndVLCCode.FieldMaxLength = 0
        Me.fndVLCCode.FieldName = Nothing
        Me.fndVLCCode.isCalculatedField = False
        Me.fndVLCCode.IsSourceFromTable = False
        Me.fndVLCCode.IsSourceFromValueList = False
        Me.fndVLCCode.IsUnique = False
        Me.fndVLCCode.Location = New System.Drawing.Point(83, 65)
        Me.fndVLCCode.MendatroryField = True
        Me.fndVLCCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVLCCode.MyLinkLable1 = Nothing
        Me.fndVLCCode.MyLinkLable2 = Nothing
        Me.fndVLCCode.MyReadOnly = False
        Me.fndVLCCode.MyShowMasterFormButton = False
        Me.fndVLCCode.Name = "fndVLCCode"
        Me.fndVLCCode.ReferenceFieldDesc = Nothing
        Me.fndVLCCode.ReferenceFieldName = Nothing
        Me.fndVLCCode.ReferenceTableName = Nothing
        Me.fndVLCCode.Size = New System.Drawing.Size(160, 19)
        Me.fndVLCCode.TabIndex = 164
        Me.fndVLCCode.Value = ""
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(14, 64)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel10.TabIndex = 163
        Me.MyLabel10.Text = "DCS Code"
        '
        'lblRouteName
        '
        Me.lblRouteName.AutoSize = False
        Me.lblRouteName.BorderVisible = True
        Me.lblRouteName.FieldName = Nothing
        Me.lblRouteName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRouteName.Location = New System.Drawing.Point(249, 43)
        Me.lblRouteName.Name = "lblRouteName"
        Me.lblRouteName.Size = New System.Drawing.Size(205, 18)
        Me.lblRouteName.TabIndex = 162
        Me.lblRouteName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblRouteName.TextWrap = False
        '
        'fndRouteCode
        '
        Me.fndRouteCode.CalculationExpression = Nothing
        Me.fndRouteCode.FieldCode = Nothing
        Me.fndRouteCode.FieldDesc = Nothing
        Me.fndRouteCode.FieldMaxLength = 0
        Me.fndRouteCode.FieldName = Nothing
        Me.fndRouteCode.isCalculatedField = False
        Me.fndRouteCode.IsSourceFromTable = False
        Me.fndRouteCode.IsSourceFromValueList = False
        Me.fndRouteCode.IsUnique = False
        Me.fndRouteCode.Location = New System.Drawing.Point(83, 43)
        Me.fndRouteCode.MendatroryField = True
        Me.fndRouteCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRouteCode.MyLinkLable1 = Nothing
        Me.fndRouteCode.MyLinkLable2 = Nothing
        Me.fndRouteCode.MyReadOnly = False
        Me.fndRouteCode.MyShowMasterFormButton = False
        Me.fndRouteCode.Name = "fndRouteCode"
        Me.fndRouteCode.ReferenceFieldDesc = Nothing
        Me.fndRouteCode.ReferenceFieldName = Nothing
        Me.fndRouteCode.ReferenceTableName = Nothing
        Me.fndRouteCode.Size = New System.Drawing.Size(160, 19)
        Me.fndRouteCode.TabIndex = 161
        Me.fndRouteCode.Value = ""
        '
        'lblRoute
        '
        Me.lblRoute.FieldName = Nothing
        Me.lblRoute.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoute.Location = New System.Drawing.Point(14, 42)
        Me.lblRoute.Name = "lblRoute"
        Me.lblRoute.Size = New System.Drawing.Size(67, 16)
        Me.lblRoute.TabIndex = 160
        Me.lblRoute.Text = "Route Code"
        '
        'lblMCCName
        '
        Me.lblMCCName.AutoSize = False
        Me.lblMCCName.BorderVisible = True
        Me.lblMCCName.FieldName = Nothing
        Me.lblMCCName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMCCName.Location = New System.Drawing.Point(249, 20)
        Me.lblMCCName.Name = "lblMCCName"
        Me.lblMCCName.Size = New System.Drawing.Size(205, 18)
        Me.lblMCCName.TabIndex = 149
        Me.lblMCCName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMCCName.TextWrap = False
        '
        'FndMCC
        '
        Me.FndMCC.CalculationExpression = Nothing
        Me.FndMCC.FieldCode = Nothing
        Me.FndMCC.FieldDesc = Nothing
        Me.FndMCC.FieldMaxLength = 0
        Me.FndMCC.FieldName = Nothing
        Me.FndMCC.isCalculatedField = False
        Me.FndMCC.IsSourceFromTable = False
        Me.FndMCC.IsSourceFromValueList = False
        Me.FndMCC.IsUnique = False
        Me.FndMCC.Location = New System.Drawing.Point(83, 20)
        Me.FndMCC.MendatroryField = True
        Me.FndMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndMCC.MyLinkLable1 = Nothing
        Me.FndMCC.MyLinkLable2 = Nothing
        Me.FndMCC.MyReadOnly = False
        Me.FndMCC.MyShowMasterFormButton = False
        Me.FndMCC.Name = "FndMCC"
        Me.FndMCC.ReferenceFieldDesc = Nothing
        Me.FndMCC.ReferenceFieldName = Nothing
        Me.FndMCC.ReferenceTableName = Nothing
        Me.FndMCC.Size = New System.Drawing.Size(160, 19)
        Me.FndMCC.TabIndex = 148
        Me.FndMCC.Value = ""
        '
        'lblMCC
        '
        Me.lblMCC.FieldName = Nothing
        Me.lblMCC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMCC.Location = New System.Drawing.Point(14, 19)
        Me.lblMCC.Name = "lblMCC"
        Me.lblMCC.Size = New System.Drawing.Size(62, 16)
        Me.lblMCC.TabIndex = 147
        Me.lblMCC.Text = "MCC Code"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgVSP)
        Me.RadGroupBox1.HeaderText = "VSP"
        Me.RadGroupBox1.Location = New System.Drawing.Point(14, 89)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(355, 153)
        Me.RadGroupBox1.TabIndex = 112
        Me.RadGroupBox1.Text = "VSP"
        '
        'cbgVSP
        '
        Me.cbgVSP.CheckedValue = Nothing
        Me.cbgVSP.DataSource = Nothing
        Me.cbgVSP.DisplayMember = "Name"
        Me.cbgVSP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVSP.Location = New System.Drawing.Point(10, 20)
        Me.cbgVSP.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVSP.MyShowHeadrText = False
        Me.cbgVSP.Name = "cbgVSP"
        Me.cbgVSP.Size = New System.Drawing.Size(335, 123)
        Me.cbgVSP.TabIndex = 1
        Me.cbgVSP.ValueMember = "Code"
        '
        'MyLabel2
        '
        Me.MyLabel2.AutoSize = False
        Me.MyLabel2.BorderVisible = True
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(249, 20)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(205, 18)
        Me.MyLabel2.TabIndex = 149
        Me.MyLabel2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.MyLabel2.TextWrap = False
        '
        'TxtFinder1
        '
        Me.TxtFinder1.CalculationExpression = Nothing
        Me.TxtFinder1.FieldCode = Nothing
        Me.TxtFinder1.FieldDesc = Nothing
        Me.TxtFinder1.FieldMaxLength = 0
        Me.TxtFinder1.FieldName = Nothing
        Me.TxtFinder1.isCalculatedField = False
        Me.TxtFinder1.IsSourceFromTable = False
        Me.TxtFinder1.IsSourceFromValueList = False
        Me.TxtFinder1.IsUnique = False
        Me.TxtFinder1.Location = New System.Drawing.Point(83, 20)
        Me.TxtFinder1.MendatroryField = True
        Me.TxtFinder1.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinder1.MyLinkLable1 = Nothing
        Me.TxtFinder1.MyLinkLable2 = Nothing
        Me.TxtFinder1.MyReadOnly = False
        Me.TxtFinder1.MyShowMasterFormButton = False
        Me.TxtFinder1.Name = "TxtFinder1"
        Me.TxtFinder1.ReferenceFieldDesc = Nothing
        Me.TxtFinder1.ReferenceFieldName = Nothing
        Me.TxtFinder1.ReferenceTableName = Nothing
        Me.TxtFinder1.Size = New System.Drawing.Size(160, 19)
        Me.TxtFinder1.TabIndex = 148
        Me.TxtFinder1.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(14, 19)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel1.TabIndex = 147
        Me.MyLabel1.Text = "MCC Code"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1118, 220)
        Me.gv1.TabIndex = 6
        Me.gv1.Text = "gv1"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(97, 9)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(80, 23)
        Me.btnReset.TabIndex = 12
        Me.btnReset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnClose.Location = New System.Drawing.Point(1032, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(74, 18)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(12, 9)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 23)
        Me.btnSave.TabIndex = 10
        Me.btnSave.Text = "Save"
        '
        'btnUnSelect
        '
        Me.btnUnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUnSelect.Location = New System.Drawing.Point(177, 9)
        Me.btnUnSelect.Name = "btnUnSelect"
        Me.btnUnSelect.Size = New System.Drawing.Size(80, 23)
        Me.btnUnSelect.TabIndex = 25
        Me.btnUnSelect.Text = "Select All"
        '
        'FrmMCCFarmerMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1118, 523)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmMCCFarmerMapping"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmMCCFarmerMapping"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblVLCName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVSP As common.MyCheckBoxGrid
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents FndMCC As common.UserControls.txtFinder
    Friend WithEvents lblMCC As common.Controls.MyLabel
    Friend WithEvents lblMCCName As common.Controls.MyLabel
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents TxtFinder1 As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblVLCName As common.Controls.MyLabel
    Friend WithEvents fndVLCCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents lblRouteName As common.Controls.MyLabel
    Friend WithEvents fndRouteCode As common.UserControls.txtFinder
    Friend WithEvents lblRoute As common.Controls.MyLabel
    Friend WithEvents btnUnSelect As Telerik.WinControls.UI.RadButton
End Class

