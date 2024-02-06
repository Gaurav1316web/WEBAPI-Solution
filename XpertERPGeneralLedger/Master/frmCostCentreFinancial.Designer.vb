<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCostCentreFinancial
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCostCentreFinancial))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.LblLevel = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.TxtCostCentreLevel = New common.UserControls.txtFinder()
        Me.LblCostCentre = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.TxtHirerachyLevel = New common.UserControls.txtFinder()
        Me.LblHirerachy = New common.Controls.MyLabel()
        Me.txtdescription = New common.Controls.MyTextBox()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.txtcostcenter = New common.UserControls.txtFinder()
        Me.lblCostDesp = New common.Controls.MyLabel()
        Me.fndCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.rdbtnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblCostCentre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblHirerachy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCostDesp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblLevel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtCostCentreLevel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblCostCentre)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtHirerachyLevel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblHirerachy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtdescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtcostcenter)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCostDesp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdbtnreset)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(563, 490)
        Me.SplitContainer1.SplitterDistance = 450
        Me.SplitContainer1.TabIndex = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.gv1)
        Me.RadGroupBox1.HeaderText = "Cost Centre Level"
        Me.RadGroupBox1.Location = New System.Drawing.Point(8, 130)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(552, 317)
        Me.RadGroupBox1.TabIndex = 332
        Me.RadGroupBox1.Text = "Cost Centre Level"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(2, 18)
        '
        '
        '
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(548, 297)
        Me.gv1.TabIndex = 0
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(13, 88)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel3.TabIndex = 330
        Me.MyLabel3.Text = "Level"
        '
        'LblLevel
        '
        Me.LblLevel.AutoSize = False
        Me.LblLevel.BorderVisible = True
        Me.LblLevel.FieldName = Nothing
        Me.LblLevel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLevel.Location = New System.Drawing.Point(126, 87)
        Me.LblLevel.Name = "LblLevel"
        Me.LblLevel.Size = New System.Drawing.Size(392, 18)
        Me.LblLevel.TabIndex = 331
        Me.LblLevel.TextWrap = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(13, 107)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(97, 16)
        Me.MyLabel2.TabIndex = 328
        Me.MyLabel2.Text = "Cost Centre Level"
        Me.MyLabel2.Visible = False
        '
        'TxtCostCentreLevel
        '
        Me.TxtCostCentreLevel.CalculationExpression = Nothing
        Me.TxtCostCentreLevel.FieldCode = Nothing
        Me.TxtCostCentreLevel.FieldDesc = Nothing
        Me.TxtCostCentreLevel.FieldMaxLength = 0
        Me.TxtCostCentreLevel.FieldName = Nothing
        Me.TxtCostCentreLevel.isCalculatedField = False
        Me.TxtCostCentreLevel.IsSourceFromTable = False
        Me.TxtCostCentreLevel.IsSourceFromValueList = False
        Me.TxtCostCentreLevel.IsUnique = False
        Me.TxtCostCentreLevel.Location = New System.Drawing.Point(126, 106)
        Me.TxtCostCentreLevel.MendatroryField = True
        Me.TxtCostCentreLevel.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCostCentreLevel.MyLinkLable1 = Me.MyLabel2
        Me.TxtCostCentreLevel.MyLinkLable2 = Me.LblCostCentre
        Me.TxtCostCentreLevel.MyReadOnly = False
        Me.TxtCostCentreLevel.MyShowMasterFormButton = False
        Me.TxtCostCentreLevel.Name = "TxtCostCentreLevel"
        Me.TxtCostCentreLevel.ReferenceFieldDesc = Nothing
        Me.TxtCostCentreLevel.ReferenceFieldName = Nothing
        Me.TxtCostCentreLevel.ReferenceTableName = Nothing
        Me.TxtCostCentreLevel.Size = New System.Drawing.Size(154, 18)
        Me.TxtCostCentreLevel.TabIndex = 5
        Me.TxtCostCentreLevel.Value = ""
        Me.TxtCostCentreLevel.Visible = False
        '
        'LblCostCentre
        '
        Me.LblCostCentre.AutoSize = False
        Me.LblCostCentre.BorderVisible = True
        Me.LblCostCentre.FieldName = Nothing
        Me.LblCostCentre.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCostCentre.Location = New System.Drawing.Point(283, 106)
        Me.LblCostCentre.Name = "LblCostCentre"
        Me.LblCostCentre.Size = New System.Drawing.Size(235, 18)
        Me.LblCostCentre.TabIndex = 329
        Me.LblCostCentre.TextWrap = False
        Me.LblCostCentre.Visible = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(13, 68)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel1.TabIndex = 325
        Me.MyLabel1.Text = "Hirerachy Level"
        '
        'TxtHirerachyLevel
        '
        Me.TxtHirerachyLevel.CalculationExpression = Nothing
        Me.TxtHirerachyLevel.FieldCode = Nothing
        Me.TxtHirerachyLevel.FieldDesc = Nothing
        Me.TxtHirerachyLevel.FieldMaxLength = 0
        Me.TxtHirerachyLevel.FieldName = Nothing
        Me.TxtHirerachyLevel.isCalculatedField = False
        Me.TxtHirerachyLevel.IsSourceFromTable = False
        Me.TxtHirerachyLevel.IsSourceFromValueList = False
        Me.TxtHirerachyLevel.IsUnique = False
        Me.TxtHirerachyLevel.Location = New System.Drawing.Point(126, 67)
        Me.TxtHirerachyLevel.MendatroryField = True
        Me.TxtHirerachyLevel.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtHirerachyLevel.MyLinkLable1 = Me.MyLabel1
        Me.TxtHirerachyLevel.MyLinkLable2 = Me.LblHirerachy
        Me.TxtHirerachyLevel.MyReadOnly = False
        Me.TxtHirerachyLevel.MyShowMasterFormButton = False
        Me.TxtHirerachyLevel.Name = "TxtHirerachyLevel"
        Me.TxtHirerachyLevel.ReferenceFieldDesc = Nothing
        Me.TxtHirerachyLevel.ReferenceFieldName = Nothing
        Me.TxtHirerachyLevel.ReferenceTableName = Nothing
        Me.TxtHirerachyLevel.Size = New System.Drawing.Size(154, 18)
        Me.TxtHirerachyLevel.TabIndex = 4
        Me.TxtHirerachyLevel.Value = ""
        '
        'LblHirerachy
        '
        Me.LblHirerachy.AutoSize = False
        Me.LblHirerachy.BorderVisible = True
        Me.LblHirerachy.FieldName = Nothing
        Me.LblHirerachy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblHirerachy.Location = New System.Drawing.Point(283, 67)
        Me.LblHirerachy.Name = "LblHirerachy"
        Me.LblHirerachy.Size = New System.Drawing.Size(235, 18)
        Me.LblHirerachy.TabIndex = 326
        Me.LblHirerachy.TextWrap = False
        '
        'txtdescription
        '
        Me.txtdescription.CalculationExpression = Nothing
        Me.txtdescription.FieldCode = Nothing
        Me.txtdescription.FieldDesc = Nothing
        Me.txtdescription.FieldMaxLength = 0
        Me.txtdescription.FieldName = Nothing
        Me.txtdescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdescription.isCalculatedField = False
        Me.txtdescription.IsSourceFromTable = False
        Me.txtdescription.IsSourceFromValueList = False
        Me.txtdescription.IsUnique = False
        Me.txtdescription.Location = New System.Drawing.Point(126, 44)
        Me.txtdescription.MaxLength = 100
        Me.txtdescription.MendatroryField = True
        Me.txtdescription.MyLinkLable1 = Me.lblDescription
        Me.txtdescription.MyLinkLable2 = Nothing
        Me.txtdescription.Name = "txtdescription"
        Me.txtdescription.ReferenceFieldDesc = Nothing
        Me.txtdescription.ReferenceFieldName = Nothing
        Me.txtdescription.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtdescription.RootElement.StretchVertically = True
        Me.txtdescription.Size = New System.Drawing.Size(290, 19)
        Me.txtdescription.TabIndex = 3
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(13, 45)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 10
        Me.lblDescription.Text = "Description"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(563, 20)
        Me.RadMenu1.TabIndex = 323
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmImport, Me.rmExport})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'rmImport
        '
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
        '
        'rmExport
        '
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export"
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(450, 44)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(101, 16)
        Me.RadLabel6.TabIndex = 96
        Me.RadLabel6.Text = "Cost Centre Group"
        Me.RadLabel6.Visible = False
        '
        'txtcostcenter
        '
        Me.txtcostcenter.CalculationExpression = Nothing
        Me.txtcostcenter.FieldCode = Nothing
        Me.txtcostcenter.FieldDesc = Nothing
        Me.txtcostcenter.FieldMaxLength = 0
        Me.txtcostcenter.FieldName = Nothing
        Me.txtcostcenter.isCalculatedField = False
        Me.txtcostcenter.IsSourceFromTable = False
        Me.txtcostcenter.IsSourceFromValueList = False
        Me.txtcostcenter.IsUnique = False
        Me.txtcostcenter.Location = New System.Drawing.Point(452, 43)
        Me.txtcostcenter.MendatroryField = True
        Me.txtcostcenter.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcostcenter.MyLinkLable1 = Me.RadLabel6
        Me.txtcostcenter.MyLinkLable2 = Me.lblCostDesp
        Me.txtcostcenter.MyReadOnly = False
        Me.txtcostcenter.MyShowMasterFormButton = False
        Me.txtcostcenter.Name = "txtcostcenter"
        Me.txtcostcenter.ReferenceFieldDesc = Nothing
        Me.txtcostcenter.ReferenceFieldName = Nothing
        Me.txtcostcenter.ReferenceTableName = Nothing
        Me.txtcostcenter.Size = New System.Drawing.Size(10, 18)
        Me.txtcostcenter.TabIndex = 3
        Me.txtcostcenter.Value = ""
        Me.txtcostcenter.Visible = False
        '
        'lblCostDesp
        '
        Me.lblCostDesp.AutoSize = False
        Me.lblCostDesp.BorderVisible = True
        Me.lblCostDesp.FieldName = Nothing
        Me.lblCostDesp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostDesp.Location = New System.Drawing.Point(468, 45)
        Me.lblCostDesp.Name = "lblCostDesp"
        Me.lblCostDesp.Size = New System.Drawing.Size(31, 18)
        Me.lblCostDesp.TabIndex = 97
        Me.lblCostDesp.TextWrap = False
        Me.lblCostDesp.Visible = False
        '
        'fndCode
        '
        Me.fndCode.FieldName = Nothing
        Me.fndCode.Location = New System.Drawing.Point(126, 21)
        Me.fndCode.MendatroryField = True
        Me.fndCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCode.MyLinkLable1 = Me.lblCode
        Me.fndCode.MyLinkLable2 = Nothing
        Me.fndCode.MyMaxLength = 30
        Me.fndCode.MyReadOnly = False
        Me.fndCode.Name = "fndCode"
        Me.fndCode.Size = New System.Drawing.Size(202, 21)
        Me.fndCode.TabIndex = 0
        Me.fndCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCode.Location = New System.Drawing.Point(13, 23)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(34, 16)
        Me.lblCode.TabIndex = 6
        Me.lblCode.Text = "Code"
        '
        'rdbtnreset
        '
        Me.rdbtnreset.Image = CType(resources.GetObject("rdbtnreset.Image"), System.Drawing.Image)
        Me.rdbtnreset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdbtnreset.Location = New System.Drawing.Point(328, 21)
        Me.rdbtnreset.Name = "rdbtnreset"
        Me.rdbtnreset.Size = New System.Drawing.Size(16, 21)
        Me.rdbtnreset.TabIndex = 1
        Me.rdbtnreset.Text = " "
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(8, 10)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(487, 10)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(80, 10)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = " Delete"
        '
        'FrmCostCentreFinancial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(563, 490)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmCostCentreFinancial"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Cost Centre Financial"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblCostCentre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblHirerachy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCostDesp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents fndCode As common.UserControls.txtNavigator
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents rdbtnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents txtcostcenter As common.UserControls.txtFinder
    Friend WithEvents lblCostDesp As common.Controls.MyLabel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtdescription As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents LblLevel As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents TxtCostCentreLevel As common.UserControls.txtFinder
    Friend WithEvents LblCostCentre As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents TxtHirerachyLevel As common.UserControls.txtFinder
    Friend WithEvents LblHirerachy As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
End Class

