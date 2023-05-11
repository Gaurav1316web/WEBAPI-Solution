<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVSPIncentiveTagging
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblMCCName = New common.Controls.MyLabel()
        Me.rbtnReset = New Telerik.WinControls.UI.RadButton()
        Me.txtIncentiveMult = New common.UserControls.txtMultiSelectFinder()
        Me.fndDocCode = New common.UserControls.txtNavigator()
        Me.lblDocumentCode = New common.Controls.MyLabel()
        Me.txtVSP = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblIncentiveCode = New common.Controls.MyLabel()
        Me.FndMCC = New common.UserControls.txtFinder()
        Me.txtRoute = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.lblMCCCode = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.btnSelect = New Telerik.WinControls.UI.RadButton()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.MenuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblMCCName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocumentCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIncentiveCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(700, 414)
        Me.SplitContainer1.SplitterDistance = 375
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(700, 375)
        Me.RadPageView1.TabIndex = 387
        Me.RadPageView1.Text = "RadPageView1"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.lblMCCName)
        Me.RadPageViewPage1.Controls.Add(Me.rbtnReset)
        Me.RadPageViewPage1.Controls.Add(Me.txtIncentiveMult)
        Me.RadPageViewPage1.Controls.Add(Me.fndDocCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtVSP)
        Me.RadPageViewPage1.Controls.Add(Me.lblIncentiveCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.FndMCC)
        Me.RadPageViewPage1.Controls.Add(Me.txtRoute)
        Me.RadPageViewPage1.Controls.Add(Me.lblMCCCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblDocumentCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(679, 327)
        Me.RadPageViewPage1.Text = "Setup"
        '
        'lblMCCName
        '
        Me.lblMCCName.AutoSize = False
        Me.lblMCCName.BorderVisible = True
        Me.lblMCCName.FieldName = Nothing
        Me.lblMCCName.Location = New System.Drawing.Point(288, 58)
        Me.lblMCCName.Name = "lblMCCName"
        Me.lblMCCName.Size = New System.Drawing.Size(212, 19)
        Me.lblMCCName.TabIndex = 372
        Me.lblMCCName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rbtnReset
        '
        Me.rbtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.rbtnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtnReset.Location = New System.Drawing.Point(331, 7)
        Me.rbtnReset.Name = "rbtnReset"
        Me.rbtnReset.Size = New System.Drawing.Size(20, 21)
        Me.rbtnReset.TabIndex = 375
        '
        'txtIncentiveMult
        '
        Me.txtIncentiveMult.arrDispalyMember = Nothing
        Me.txtIncentiveMult.arrValueMember = Nothing
        Me.txtIncentiveMult.Location = New System.Drawing.Point(106, 33)
        Me.txtIncentiveMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIncentiveMult.MyLinkLable1 = Nothing
        Me.txtIncentiveMult.MyLinkLable2 = Nothing
        Me.txtIncentiveMult.MyNullText = "Select Incentive"
        Me.txtIncentiveMult.Name = "txtIncentiveMult"
        Me.txtIncentiveMult.Size = New System.Drawing.Size(394, 19)
        Me.txtIncentiveMult.TabIndex = 386
        '
        'fndDocCode
        '
        Me.fndDocCode.FieldName = Nothing
        Me.fndDocCode.Location = New System.Drawing.Point(106, 7)
        Me.fndDocCode.MendatroryField = True
        Me.fndDocCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndDocCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocCode.MyLinkLable1 = Me.lblDocumentCode
        Me.fndDocCode.MyLinkLable2 = Nothing
        Me.fndDocCode.MyMaxLength = 32767
        Me.fndDocCode.MyReadOnly = False
        Me.fndDocCode.Name = "fndDocCode"
        Me.fndDocCode.Size = New System.Drawing.Size(219, 21)
        Me.fndDocCode.TabIndex = 374
        Me.fndDocCode.Value = ""
        '
        'lblDocumentCode
        '
        Me.lblDocumentCode.FieldName = Nothing
        Me.lblDocumentCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDocumentCode.Location = New System.Drawing.Point(9, 9)
        Me.lblDocumentCode.Name = "lblDocumentCode"
        Me.lblDocumentCode.Size = New System.Drawing.Size(88, 16)
        Me.lblDocumentCode.TabIndex = 376
        Me.lblDocumentCode.Text = "Document Code"
        '
        'txtVSP
        '
        Me.txtVSP.arrDispalyMember = Nothing
        Me.txtVSP.arrValueMember = Nothing
        Me.txtVSP.Location = New System.Drawing.Point(106, 102)
        Me.txtVSP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVSP.MyLinkLable1 = Me.MyLabel1
        Me.txtVSP.MyLinkLable2 = Nothing
        Me.txtVSP.MyNullText = "Select VSP..."
        Me.txtVSP.Name = "txtVSP"
        Me.txtVSP.Size = New System.Drawing.Size(394, 19)
        Me.txtVSP.TabIndex = 383
        Me.txtVSP.Visible = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(9, 102)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel1.TabIndex = 384
        Me.MyLabel1.Text = "VSP Code"
        Me.MyLabel1.Visible = False
        '
        'lblIncentiveCode
        '
        Me.lblIncentiveCode.FieldName = Nothing
        Me.lblIncentiveCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIncentiveCode.Location = New System.Drawing.Point(9, 36)
        Me.lblIncentiveCode.Name = "lblIncentiveCode"
        Me.lblIncentiveCode.Size = New System.Drawing.Size(85, 16)
        Me.lblIncentiveCode.TabIndex = 13
        Me.lblIncentiveCode.Text = "Incentive  Code"
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
        Me.FndMCC.Location = New System.Drawing.Point(106, 58)
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
        Me.FndMCC.Size = New System.Drawing.Size(175, 19)
        Me.FndMCC.TabIndex = 12
        Me.FndMCC.Value = ""
        '
        'txtRoute
        '
        Me.txtRoute.arrDispalyMember = Nothing
        Me.txtRoute.arrValueMember = Nothing
        Me.txtRoute.Location = New System.Drawing.Point(106, 80)
        Me.txtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoute.MyLinkLable1 = Me.lblLocation
        Me.txtRoute.MyLinkLable2 = Nothing
        Me.txtRoute.MyNullText = "Select Route..."
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.Size = New System.Drawing.Size(394, 19)
        Me.txtRoute.TabIndex = 381
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(9, 80)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(65, 18)
        Me.lblLocation.TabIndex = 382
        Me.lblLocation.Text = "Route Code"
        '
        'lblMCCCode
        '
        Me.lblMCCCode.FieldName = Nothing
        Me.lblMCCCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMCCCode.Location = New System.Drawing.Point(9, 59)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(62, 16)
        Me.lblMCCCode.TabIndex = 11
        Me.lblMCCCode.Text = "MCC Code"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(679, 327)
        Me.RadPageViewPage2.Text = "Detail"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnSelect)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv)
        Me.SplitContainer2.Size = New System.Drawing.Size(679, 327)
        Me.SplitContainer2.SplitterDistance = 30
        Me.SplitContainer2.TabIndex = 6
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Location = New System.Drawing.Point(6, 7)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(80, 18)
        Me.btnSelect.TabIndex = 5
        Me.btnSelect.Text = "Select All"
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        'gv
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(679, 293)
        Me.gv.TabIndex = 4
        Me.gv.Text = "gv"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExcel})
        Me.btnExport.Location = New System.Drawing.Point(87, 6)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(80, 23)
        Me.btnExport.TabIndex = 134
        Me.btnExport.Text = "Export"
        Me.btnExport.Visible = False
        '
        'rmExcel
        '
        Me.rmExcel.AccessibleDescription = "Excel"
        Me.rmExcel.AccessibleName = "Excel"
        Me.rmExcel.Name = "rmExcel"
        Me.rmExcel.Text = "Excel"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(612, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(77, 23)
        Me.btnclose.TabIndex = 15
        Me.btnclose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(8, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(73, 23)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "&Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuClose})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(700, 20)
        Me.RadMenu1.TabIndex = 13
        Me.RadMenu1.Text = "RadMenu1"
        '
        'MenuClose
        '
        Me.MenuClose.AccessibleDescription = "File"
        Me.MenuClose.AccessibleName = "File"
        Me.MenuClose.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExport, Me.rmImport})
        Me.MenuClose.Name = "MenuClose"
        Me.MenuClose.Text = "File"
        '
        'rmExport
        '
        Me.rmExport.AccessibleDescription = "RadMenuItem1"
        Me.rmExport.AccessibleName = "RadMenuItem1"
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export"
        '
        'rmImport
        '
        Me.rmImport.AccessibleDescription = "Import"
        Me.rmImport.AccessibleName = "Import"
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
        '
        'FrmVSPIncentiveTagging
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(700, 434)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmVSPIncentiveTagging"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmVSPIncentiveTagging"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblMCCName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocumentCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIncentiveCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents lblIncentiveCode As common.Controls.MyLabel
    Friend WithEvents FndMCC As common.UserControls.txtFinder
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblMCCName As common.Controls.MyLabel
    Friend WithEvents lblDocumentCode As common.Controls.MyLabel
    Friend WithEvents rbtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndDocCode As common.UserControls.txtNavigator
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents MenuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtRoute As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtVSP As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtIncentiveMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents btnSelect As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
End Class

