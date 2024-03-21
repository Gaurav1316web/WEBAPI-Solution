<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSectionParameterMapping
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
        Me.components = New System.ComponentModel.Container()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.lbldes = New common.Controls.MyLabel()
        Me.txtSecdes = New common.Controls.MyTextBox()
        Me.lblid = New common.Controls.MyLabel()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.import = New Telerik.WinControls.UI.RadMenuItem()
        Me.export = New Telerik.WinControls.UI.RadMenuItem()
        Me.cityclose = New Telerik.WinControls.UI.RadMenuItem()
        Me.ToolTipcity = New System.Windows.Forms.ToolTip(Me.components)
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.gbcity = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtConsumType = New common.UserControls.txtFinder()
        Me.txtConsumTypedes = New common.Controls.MyTextBox()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.dgv = New common.UserControls.MyRadGridView()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSecdes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbcity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbcity.SuspendLayout()
        CType(Me.txtConsumTypedes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(541, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(75, 8)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(6, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'lbldes
        '
        Me.lbldes.FieldName = Nothing
        Me.lbldes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldes.Location = New System.Drawing.Point(9, 31)
        Me.lbldes.Name = "lbldes"
        Me.lbldes.Size = New System.Drawing.Size(103, 16)
        Me.lbldes.TabIndex = 6
        Me.lbldes.Text = "Consumption Code"
        '
        'txtSecdes
        '
        Me.txtSecdes.BackColor = System.Drawing.Color.Transparent
        Me.txtSecdes.CalculationExpression = Nothing
        Me.txtSecdes.Enabled = False
        Me.txtSecdes.FieldCode = Nothing
        Me.txtSecdes.FieldDesc = Nothing
        Me.txtSecdes.FieldMaxLength = 0
        Me.txtSecdes.FieldName = Nothing
        Me.txtSecdes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSecdes.isCalculatedField = False
        Me.txtSecdes.IsSourceFromTable = False
        Me.txtSecdes.IsSourceFromValueList = False
        Me.txtSecdes.IsUnique = False
        Me.txtSecdes.Location = New System.Drawing.Point(344, 7)
        Me.txtSecdes.MaxLength = 49
        Me.txtSecdes.MendatroryField = True
        Me.txtSecdes.MyLinkLable1 = Me.lbldes
        Me.txtSecdes.MyLinkLable2 = Nothing
        Me.txtSecdes.Name = "txtSecdes"
        Me.txtSecdes.ReferenceFieldDesc = Nothing
        Me.txtSecdes.ReferenceFieldName = Nothing
        Me.txtSecdes.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtSecdes.RootElement.StretchVertically = True
        Me.txtSecdes.Size = New System.Drawing.Size(257, 18)
        Me.txtSecdes.TabIndex = 2
        '
        'lblid
        '
        Me.lblid.FieldName = Nothing
        Me.lblid.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblid.Location = New System.Drawing.Point(9, 11)
        Me.lblid.Name = "lblid"
        Me.lblid.Size = New System.Drawing.Size(74, 16)
        Me.lblid.TabIndex = 7
        Me.lblid.Text = "Section Code"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.import, Me.export, Me.cityclose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'import
        '
        Me.import.Name = "import"
        Me.import.Text = "Import"
        '
        'export
        '
        Me.export.Name = "export"
        Me.export.Text = "Export"
        '
        'cityclose
        '
        Me.cityclose.Name = "cityclose"
        Me.cityclose.Text = "Close"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(614, 20)
        Me.RadMenu1.TabIndex = 1
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPEngineeringAndPlantManagement.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(323, 6)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 1
        '
        'gbcity
        '
        Me.gbcity.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbcity.BackColor = System.Drawing.Color.Transparent
        Me.gbcity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.gbcity.Controls.Add(Me.txtConsumType)
        Me.gbcity.Controls.Add(Me.txtConsumTypedes)
        Me.gbcity.Controls.Add(Me.txtCode)
        Me.gbcity.Controls.Add(Me.RadGroupBox4)
        Me.gbcity.Controls.Add(Me.lblid)
        Me.gbcity.Controls.Add(Me.txtSecdes)
        Me.gbcity.Controls.Add(Me.lbldes)
        Me.gbcity.Controls.Add(Me.btnnew)
        Me.gbcity.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbcity.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gbcity.HeaderText = ""
        Me.gbcity.Location = New System.Drawing.Point(0, 0)
        Me.gbcity.Name = "gbcity"
        Me.gbcity.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbcity.Size = New System.Drawing.Size(614, 402)
        Me.gbcity.TabIndex = 0
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
        Me.txtConsumType.Location = New System.Drawing.Point(120, 27)
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
        Me.txtConsumType.Size = New System.Drawing.Size(218, 20)
        Me.txtConsumType.TabIndex = 9
        Me.txtConsumType.Value = ""
        '
        'txtConsumTypedes
        '
        Me.txtConsumTypedes.CalculationExpression = Nothing
        Me.txtConsumTypedes.Enabled = False
        Me.txtConsumTypedes.FieldCode = Nothing
        Me.txtConsumTypedes.FieldDesc = Nothing
        Me.txtConsumTypedes.FieldMaxLength = 0
        Me.txtConsumTypedes.FieldName = Nothing
        Me.txtConsumTypedes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConsumTypedes.isCalculatedField = False
        Me.txtConsumTypedes.IsSourceFromTable = False
        Me.txtConsumTypedes.IsSourceFromValueList = False
        Me.txtConsumTypedes.IsUnique = False
        Me.txtConsumTypedes.Location = New System.Drawing.Point(344, 28)
        Me.txtConsumTypedes.MaxLength = 150
        Me.txtConsumTypedes.MendatroryField = True
        Me.txtConsumTypedes.MyLinkLable1 = Nothing
        Me.txtConsumTypedes.MyLinkLable2 = Nothing
        Me.txtConsumTypedes.Name = "txtConsumTypedes"
        Me.txtConsumTypedes.ReferenceFieldDesc = Nothing
        Me.txtConsumTypedes.ReferenceFieldName = Nothing
        Me.txtConsumTypedes.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtConsumTypedes.RootElement.StretchVertically = True
        Me.txtConsumTypedes.Size = New System.Drawing.Size(257, 19)
        Me.txtConsumTypedes.TabIndex = 8
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(120, 7)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblid
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(202, 18)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.dgv)
        Me.RadGroupBox4.HeaderText = ""
        Me.RadGroupBox4.Location = New System.Drawing.Point(6, 68)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(595, 322)
        Me.RadGroupBox4.TabIndex = 4
        '
        'dgv
        '
        Me.dgv.BackColor = System.Drawing.Color.Transparent
        Me.dgv.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgv.ForeColor = System.Drawing.Color.Black
        Me.dgv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgv.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.dgv.MasterTemplate.AllowAddNewRow = False
        Me.dgv.MasterTemplate.EnableFiltering = True
        Me.dgv.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.dgv.MasterTemplate.ShowHeaderCellButtons = True
        Me.dgv.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.dgv.MyStopExport = False
        Me.dgv.Name = "dgv"
        Me.dgv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgv.ShowGroupPanel = False
        Me.dgv.ShowHeaderCellButtons = True
        Me.dgv.Size = New System.Drawing.Size(575, 292)
        Me.dgv.TabIndex = 1
        Me.dgv.TabStop = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.gbcity)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(614, 435)
        Me.SplitContainer1.SplitterDistance = 402
        Me.SplitContainer1.TabIndex = 2
        '
        'frmSectionParameterMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(614, 455)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmSectionParameterMapping"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Section Parameter Mapping"
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSecdes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbcity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbcity.ResumeLayout(False)
        Me.gbcity.PerformLayout()
        CType(Me.txtConsumTypedes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.dgv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtSecdes As common.Controls.MyTextBox
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ToolTipcity As System.Windows.Forms.ToolTip
    Friend WithEvents import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents cityclose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents gbcity As Telerik.WinControls.UI.RadGroupBox
    'Friend WithEvents Office2007SilverTheme1 As Telerik.WinControls.Themes.Office2007SilverTheme
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lbldes As common.Controls.MyLabel
    Friend WithEvents lblid As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents dgv As common.UserControls.MyRadGridView
    Friend WithEvents txtConsumTypedes As common.Controls.MyTextBox
    Friend WithEvents txtConsumType As common.UserControls.txtFinder
End Class

