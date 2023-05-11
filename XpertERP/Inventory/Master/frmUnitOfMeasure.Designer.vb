<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUnitOfCode
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
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewCheckBoxColumn1 As Telerik.WinControls.UI.GridViewCheckBoxColumn = New Telerik.WinControls.UI.GridViewCheckBoxColumn()
        Dim GridViewCheckBoxColumn2 As Telerik.WinControls.UI.GridViewCheckBoxColumn = New Telerik.WinControls.UI.GridViewCheckBoxColumn()
        Dim GridViewCheckBoxColumn3 As Telerik.WinControls.UI.GridViewCheckBoxColumn = New Telerik.WinControls.UI.GridViewCheckBoxColumn()
        Dim GridViewCheckBoxColumn4 As Telerik.WinControls.UI.GridViewCheckBoxColumn = New Telerik.WinControls.UI.GridViewCheckBoxColumn()
        Dim GridViewCheckBoxColumn5 As Telerik.WinControls.UI.GridViewCheckBoxColumn = New Telerik.WinControls.UI.GridViewCheckBoxColumn()
        Dim GridViewCheckBoxColumn6 As Telerik.WinControls.UI.GridViewCheckBoxColumn = New Telerik.WinControls.UI.GridViewCheckBoxColumn()
        Dim GridViewCheckBoxColumn7 As Telerik.WinControls.UI.GridViewCheckBoxColumn = New Telerik.WinControls.UI.GridViewCheckBoxColumn()
        Dim GridViewCheckBoxColumn8 As Telerik.WinControls.UI.GridViewCheckBoxColumn = New Telerik.WinControls.UI.GridViewCheckBoxColumn()
        Dim GridViewCheckBoxColumn9 As Telerik.WinControls.UI.GridViewCheckBoxColumn = New Telerik.WinControls.UI.GridViewCheckBoxColumn()
        Dim GridViewCheckBoxColumn10 As Telerik.WinControls.UI.GridViewCheckBoxColumn = New Telerik.WinControls.UI.GridViewCheckBoxColumn()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.Rdmenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuexit = New Telerik.WinControls.UI.RadMenuItem()
        Me.MasterTemplate = New common.UserControls.MyRadGridView()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterTemplate.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(6, 11)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnclose.Location = New System.Drawing.Point(890, 11)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(65, 18)
        Me.rdbtnclose.TabIndex = 1
        Me.rdbtnclose.Text = "Close"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(966, 20)
        Me.RadMenu1.TabIndex = 1
        '
        'Rdmenufile
        '
        Me.Rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenuimport, Me.rdmenuexport, Me.rdmenuexit})
        Me.Rdmenufile.Name = "Rdmenufile"
        Me.Rdmenufile.Text = "File"
        '
        'rdmenuimport
        '
        Me.rdmenuimport.Name = "rdmenuimport"
        Me.rdmenuimport.Text = "Import"
        '
        'rdmenuexport
        '
        Me.rdmenuexport.Name = "rdmenuexport"
        Me.rdmenuexport.Text = "Export"
        '
        'rdmenuexit
        '
        Me.rdmenuexit.Name = "rdmenuexit"
        Me.rdmenuexit.Text = "Exit"
        '
        'MasterTemplate
        '
        Me.MasterTemplate.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MasterTemplate.Cursor = System.Windows.Forms.Cursors.Default
        Me.MasterTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MasterTemplate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MasterTemplate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.MasterTemplate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.MasterTemplate.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.MasterTemplate.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GridViewTextBoxColumn1.EnableExpressionEditor = False
        GridViewTextBoxColumn1.HeaderText = "Unit of Measure"
        GridViewTextBoxColumn1.MaxLength = 12
        GridViewTextBoxColumn1.Name = "uom"
        GridViewTextBoxColumn1.Width = 100
        GridViewTextBoxColumn2.EnableExpressionEditor = False
        GridViewTextBoxColumn2.HeaderText = "Description"
        GridViewTextBoxColumn2.MaxLength = 50
        GridViewTextBoxColumn2.Name = "desc"
        GridViewTextBoxColumn2.Width = 140
        GridViewTextBoxColumn3.EnableExpressionEditor = False
        GridViewTextBoxColumn3.HeaderText = "Default Conversion Factor"
        GridViewTextBoxColumn3.MaxLength = 50
        GridViewTextBoxColumn3.Name = "conversion"
        GridViewTextBoxColumn3.Width = 150
        GridViewCheckBoxColumn1.EnableExpressionEditor = False
        GridViewCheckBoxColumn1.HeaderText = "Empty"
        GridViewCheckBoxColumn1.MinWidth = 20
        GridViewCheckBoxColumn1.Name = "empty"
        GridViewCheckBoxColumn1.Width = 100
        GridViewCheckBoxColumn2.EnableExpressionEditor = False
        GridViewCheckBoxColumn2.HeaderText = "Create Price"
        GridViewCheckBoxColumn2.MinWidth = 20
        GridViewCheckBoxColumn2.Name = "createprice"
        GridViewCheckBoxColumn2.Width = 100
        GridViewCheckBoxColumn3.EnableExpressionEditor = False
        GridViewCheckBoxColumn3.HeaderText = "Weight Type"
        GridViewCheckBoxColumn3.MinWidth = 20
        GridViewCheckBoxColumn3.Name = "colIsWeightType"
        GridViewCheckBoxColumn3.Width = 100
        GridViewCheckBoxColumn4.HeaderText = "Crate Type"
        GridViewCheckBoxColumn4.Name = "ColIsCrateType"
        GridViewCheckBoxColumn4.Width = 100
        GridViewCheckBoxColumn5.HeaderText = "RT Rate"
        GridViewCheckBoxColumn5.Name = "colRTrate"
        GridViewCheckBoxColumn6.HeaderText = "Ltr Type"
        GridViewCheckBoxColumn6.Name = "colIsLtrType"
        GridViewCheckBoxColumn7.HeaderText = "Is Box Type"
        GridViewCheckBoxColumn7.Name = "ColBoxType"
        GridViewCheckBoxColumn8.HeaderText = "Is CAN Type"
        GridViewCheckBoxColumn8.Name = "colIsCanType"
        GridViewCheckBoxColumn9.HeaderText = "is Packet Type"
        GridViewCheckBoxColumn9.Name = "colPacketType"
        GridViewCheckBoxColumn9.Width = 100
        GridViewCheckBoxColumn10.HeaderText = "Is Default"
        GridViewCheckBoxColumn10.Name = "colIsDefault"
        GridViewCheckBoxColumn10.Width = 100
        Me.MasterTemplate.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewTextBoxColumn3, GridViewCheckBoxColumn1, GridViewCheckBoxColumn2, GridViewCheckBoxColumn3, GridViewCheckBoxColumn4, GridViewCheckBoxColumn5, GridViewCheckBoxColumn6, GridViewCheckBoxColumn7, GridViewCheckBoxColumn8, GridViewCheckBoxColumn9, GridViewCheckBoxColumn10})
        Me.MasterTemplate.MasterTemplate.EnableFiltering = True
        Me.MasterTemplate.MasterTemplate.EnableGrouping = False
        Me.MasterTemplate.MasterTemplate.EnableSorting = False
        Me.MasterTemplate.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.MasterTemplate.MasterTemplate.ShowHeaderCellButtons = True
        Me.MasterTemplate.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.MasterTemplate.Name = "MasterTemplate"
        Me.MasterTemplate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MasterTemplate.ShowHeaderCellButtons = True
        Me.MasterTemplate.Size = New System.Drawing.Size(966, 354)
        Me.MasterTemplate.TabIndex = 0
        Me.MasterTemplate.TabStop = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.MasterTemplate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(966, 399)
        Me.SplitContainer1.SplitterDistance = 354
        Me.SplitContainer1.TabIndex = 9
        '
        'frmUnitOfCode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(966, 419)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmUnitOfCode"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Unit Of Measure"
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterTemplate.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents Rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents dgUnitofMasterDetails As common.UserControls.MyRadGridView
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

