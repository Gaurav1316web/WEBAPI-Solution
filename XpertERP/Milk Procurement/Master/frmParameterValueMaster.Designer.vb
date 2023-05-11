<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmParameterValueMaster
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.ddlParamCode = New common.Controls.MyComboBox()
        Me.lblParamCode = New common.Controls.MyLabel()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtParamDesc = New common.Controls.MyTextBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        CType(Me.ddlParamCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblParamCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtParamDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ddlParamCode
        '
        Me.ddlParamCode.AutoCompleteDisplayMember = Nothing
        Me.ddlParamCode.AutoCompleteValueMember = Nothing
        Me.ddlParamCode.CalculationExpression = Nothing
        Me.ddlParamCode.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlParamCode.FieldCode = Nothing
        Me.ddlParamCode.FieldDesc = Nothing
        Me.ddlParamCode.FieldMaxLength = 0
        Me.ddlParamCode.FieldName = Nothing
        Me.ddlParamCode.isCalculatedField = False
        Me.ddlParamCode.IsSourceFromTable = False
        Me.ddlParamCode.IsSourceFromValueList = False
        Me.ddlParamCode.IsUnique = False
        RadListDataItem1.Text = "Yes"
        RadListDataItem2.Text = "No"
        Me.ddlParamCode.Items.Add(RadListDataItem1)
        Me.ddlParamCode.Items.Add(RadListDataItem2)
        Me.ddlParamCode.Location = New System.Drawing.Point(73, 2)
        Me.ddlParamCode.MendatroryField = True
        Me.ddlParamCode.MyLinkLable1 = Nothing
        Me.ddlParamCode.MyLinkLable2 = Nothing
        Me.ddlParamCode.Name = "ddlParamCode"
        Me.ddlParamCode.ReferenceFieldDesc = Nothing
        Me.ddlParamCode.ReferenceFieldName = Nothing
        Me.ddlParamCode.ReferenceTableName = Nothing
        Me.ddlParamCode.Size = New System.Drawing.Size(172, 20)
        Me.ddlParamCode.TabIndex = 61
        '
        'lblParamCode
        '
        Me.lblParamCode.FieldName = Nothing
        Me.lblParamCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblParamCode.Location = New System.Drawing.Point(7, 4)
        Me.lblParamCode.Name = "lblParamCode"
        Me.lblParamCode.Size = New System.Drawing.Size(59, 16)
        Me.lblParamCode.TabIndex = 62
        Me.lblParamCode.Text = "Parameter"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(3, 18)
        '
        'gv
        '
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(840, 194)
        Me.gv.TabIndex = 63
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.gv)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(846, 215)
        Me.GroupBox1.TabIndex = 64
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Parameter Values"
        '
        'txtParamDesc
        '
        Me.txtParamDesc.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtParamDesc.CalculationExpression = Nothing
        Me.txtParamDesc.FieldCode = Nothing
        Me.txtParamDesc.FieldDesc = Nothing
        Me.txtParamDesc.FieldMaxLength = 0
        Me.txtParamDesc.FieldName = Nothing
        Me.txtParamDesc.isCalculatedField = False
        Me.txtParamDesc.IsSourceFromTable = False
        Me.txtParamDesc.IsSourceFromValueList = False
        Me.txtParamDesc.IsUnique = False
        Me.txtParamDesc.Location = New System.Drawing.Point(248, 2)
        Me.txtParamDesc.MendatroryField = False
        Me.txtParamDesc.MyLinkLable1 = Nothing
        Me.txtParamDesc.MyLinkLable2 = Nothing
        Me.txtParamDesc.Name = "txtParamDesc"
        Me.txtParamDesc.ReadOnly = True
        Me.txtParamDesc.ReferenceFieldDesc = Nothing
        Me.txtParamDesc.ReferenceFieldName = Nothing
        Me.txtParamDesc.ReferenceTableName = Nothing
        Me.txtParamDesc.Size = New System.Drawing.Size(293, 20)
        Me.txtParamDesc.TabIndex = 86
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer1.Size = New System.Drawing.Size(846, 307)
        Me.SplitContainer1.SplitterDistance = 54
        Me.SplitContainer1.TabIndex = 87
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.rdmenufile)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtParamDesc)
        Me.SplitContainer2.Panel2.Controls.Add(Me.lblParamCode)
        Me.SplitContainer2.Panel2.Controls.Add(Me.ddlParamCode)
        Me.SplitContainer2.Size = New System.Drawing.Size(846, 54)
        Me.SplitContainer2.SplitterDistance = 25
        Me.SplitContainer2.TabIndex = 0
        '
        'rdmenufile
        '
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenufile})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(846, 20)
        Me.rdmenufile.TabIndex = 2
        Me.rdmenufile.Text = "File"
        Me.rdmenufile.Visible = False
        '
        'RadMenufile
        '
        Me.RadMenufile.AccessibleDescription = "File"
        Me.RadMenufile.AccessibleName = "File"
        Me.RadMenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuImport, Me.mnuExport})
        Me.RadMenufile.Name = "RadMenufile"
        Me.RadMenufile.Text = "File"
        '
        'mnuImport
        '
        Me.mnuImport.AccessibleDescription = "Import"
        Me.mnuImport.AccessibleName = "Import"
        Me.mnuImport.Name = "mnuImport"
        Me.mnuImport.Text = "Import"
        '
        'mnuExport
        '
        Me.mnuExport.AccessibleDescription = "Export"
        Me.mnuExport.AccessibleName = "Export"
        Me.mnuExport.Name = "mnuExport"
        Me.mnuExport.Text = "Export"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer3.Size = New System.Drawing.Size(846, 249)
        Me.SplitContainer3.SplitterDistance = 215
        Me.SplitContainer3.TabIndex = 65
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(767, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 19)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(73, 7)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(70, 19)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(0, 7)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(73, 19)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        '
        'FrmParameterValueMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(846, 307)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmParameterValueMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FrmParameterValueMaster"
        CType(Me.ddlParamCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblParamCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.txtParamDesc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ddlParamCode As common.Controls.MyComboBox
    Friend WithEvents lblParamCode As common.Controls.MyLabel
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtParamDesc As common.Controls.MyTextBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuExport As Telerik.WinControls.UI.RadMenuItem
End Class

