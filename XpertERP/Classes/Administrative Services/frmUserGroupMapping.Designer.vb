<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUserGroupMapping
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
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn4 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewCheckBoxColumn2 As Telerik.WinControls.UI.GridViewCheckBoxColumn = New Telerik.WinControls.UI.GridViewCheckBoxColumn
        Me.dgv_Groupmapping = New common.UserControls.MyRadGridView
        Me.rlblUserCode = New common.Controls.MyLabel
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenu_file = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem_Import = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem_Export = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem_Close = New Telerik.WinControls.UI.RadMenuItem
        Me.rbtnClose = New Telerik.WinControls.UI.RadButton
        Me.rbtnDelete = New Telerik.WinControls.UI.RadButton
        Me.rbtnSave = New Telerik.WinControls.UI.RadButton
        Me.rbtnReset = New Telerik.WinControls.UI.RadButton
        Me.TxtUserName = New common.Controls.MyTextBox
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.fndUser_Name = New common.UserControls.txtNavigator
        CType(Me.dgv_Groupmapping, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv_Groupmapping.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblUserCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtUserName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgv_Groupmapping
        '
        Me.dgv_Groupmapping.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_Groupmapping.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgv_Groupmapping.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_Groupmapping.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.dgv_Groupmapping.ForeColor = System.Drawing.Color.Black
        Me.dgv_Groupmapping.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgv_Groupmapping.Location = New System.Drawing.Point(0, 0)
        '
        'dgv_Groupmapping
        '
        Me.dgv_Groupmapping.MasterTemplate.AllowAddNewRow = False
        GridViewTextBoxColumn3.HeaderText = "Group Code"
        GridViewTextBoxColumn3.Name = "GroupCode"
        GridViewTextBoxColumn3.ReadOnly = True
        GridViewTextBoxColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        GridViewTextBoxColumn3.Width = 180
        GridViewTextBoxColumn4.HeaderText = "Description"
        GridViewTextBoxColumn4.Name = "Description"
        GridViewTextBoxColumn4.ReadOnly = True
        GridViewTextBoxColumn4.Width = 197
        GridViewCheckBoxColumn2.HeaderText = "Status"
        GridViewCheckBoxColumn2.MinWidth = 20
        GridViewCheckBoxColumn2.Name = "Status"
        GridViewCheckBoxColumn2.Width = 169
        Me.dgv_Groupmapping.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn3, GridViewTextBoxColumn4, GridViewCheckBoxColumn2})
        Me.dgv_Groupmapping.MasterTemplate.EnableFiltering = True
        Me.dgv_Groupmapping.MasterTemplate.EnableGrouping = False
        Me.dgv_Groupmapping.Name = "dgv_Groupmapping"
        Me.dgv_Groupmapping.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgv_Groupmapping.Size = New System.Drawing.Size(626, 365)
        Me.dgv_Groupmapping.TabIndex = 0
        Me.dgv_Groupmapping.TabStop = False
        Me.dgv_Groupmapping.Text = "dgv Groupmapping"
        '
        'rlblUserCode
        '
        Me.rlblUserCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblUserCode.Location = New System.Drawing.Point(3, 3)
        Me.rlblUserCode.Name = "rlblUserCode"
        Me.rlblUserCode.Size = New System.Drawing.Size(60, 16)
        Me.rlblUserCode.TabIndex = 1
        Me.rlblUserCode.Text = "User Code"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenu_file})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(629, 20)
        Me.RadMenu1.TabIndex = 6
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenu_file
        '
        Me.RadMenu_file.AccessibleDescription = "File"
        Me.RadMenu_file.AccessibleName = "File"
        Me.RadMenu_file.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenu_file.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem_Import, Me.RadMenuItem_Export, Me.RadMenuItem_Close})
        Me.RadMenu_file.Name = "RadMenu_file"
        Me.RadMenu_file.Text = "File"
        Me.RadMenu_file.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem_Import
        '
        Me.RadMenuItem_Import.AccessibleDescription = "Import"
        Me.RadMenuItem_Import.AccessibleName = "Import"
        Me.RadMenuItem_Import.Name = "RadMenuItem_Import"
        Me.RadMenuItem_Import.Text = "Import"
        Me.RadMenuItem_Import.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem_Export
        '
        Me.RadMenuItem_Export.AccessibleDescription = "Export"
        Me.RadMenuItem_Export.AccessibleName = "Export"
        Me.RadMenuItem_Export.Name = "RadMenuItem_Export"
        Me.RadMenuItem_Export.Text = "Export"
        Me.RadMenuItem_Export.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem_Close
        '
        Me.RadMenuItem_Close.AccessibleDescription = "Close"
        Me.RadMenuItem_Close.AccessibleName = "Close"
        Me.RadMenuItem_Close.Name = "RadMenuItem_Close"
        Me.RadMenuItem_Close.Text = "Close"
        Me.RadMenuItem_Close.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rbtnClose
        '
        Me.rbtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbtnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnClose.Location = New System.Drawing.Point(527, 6)
        Me.rbtnClose.Name = "rbtnClose"
        Me.rbtnClose.Size = New System.Drawing.Size(68, 18)
        Me.rbtnClose.TabIndex = 2
        Me.rbtnClose.Text = "Close"
        '
        'rbtnDelete
        '
        Me.rbtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnDelete.Location = New System.Drawing.Point(84, 6)
        Me.rbtnDelete.Name = "rbtnDelete"
        Me.rbtnDelete.Size = New System.Drawing.Size(68, 18)
        Me.rbtnDelete.TabIndex = 1
        Me.rbtnDelete.Text = "Delete"
        '
        'rbtnSave
        '
        Me.rbtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnSave.Location = New System.Drawing.Point(13, 6)
        Me.rbtnSave.Name = "rbtnSave"
        Me.rbtnSave.Size = New System.Drawing.Size(68, 18)
        Me.rbtnSave.TabIndex = 0
        Me.rbtnSave.Text = "Save"
        '
        'rbtnReset
        '
        Me.rbtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.rbtnReset.Location = New System.Drawing.Point(288, 3)
        Me.rbtnReset.Name = "rbtnReset"
        Me.rbtnReset.Size = New System.Drawing.Size(14, 18)
        Me.rbtnReset.TabIndex = 1
        '
        'TxtUserName
        '
        Me.TxtUserName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUserName.Location = New System.Drawing.Point(315, 2)
        Me.TxtUserName.MaxLength = 25
        Me.TxtUserName.MendatroryField = False
        Me.TxtUserName.MyLinkLable1 = Nothing
        Me.TxtUserName.MyLinkLable2 = Nothing
        Me.TxtUserName.Name = "TxtUserName"
        Me.TxtUserName.ReadOnly = True
        Me.TxtUserName.Size = New System.Drawing.Size(186, 18)
        Me.TxtUserName.TabIndex = 2
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.Controls.Add(Me.SplitContainer1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.FooterTextAlignment = System.Drawing.ContentAlignment.BottomCenter
        Me.RadGroupBox1.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 20)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(629, 454)
        Me.RadGroupBox1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbtnSave)
        Me.Panel1.Controls.Add(Me.rbtnDelete)
        Me.Panel1.Controls.Add(Me.rbtnClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(10, 417)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(609, 27)
        Me.Panel1.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.rlblUserCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndUser_Name)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rbtnReset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtUserName)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgv_Groupmapping)
        Me.SplitContainer1.Size = New System.Drawing.Size(626, 397)
        Me.SplitContainer1.SplitterDistance = 28
        Me.SplitContainer1.TabIndex = 29
        '
        'fndUser_Name
        '
        Me.fndUser_Name.Location = New System.Drawing.Point(67, 0)
        Me.fndUser_Name.MendatroryField = True
        Me.fndUser_Name.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndUser_Name.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndUser_Name.MyLinkLable1 = Nothing
        Me.fndUser_Name.MyLinkLable2 = Nothing
        Me.fndUser_Name.MyMaxLength = 32767
        Me.fndUser_Name.MyReadOnly = False
        Me.fndUser_Name.Name = "fndUser_Name"
        Me.fndUser_Name.Size = New System.Drawing.Size(210, 21)
        Me.fndUser_Name.TabIndex = 0
        Me.fndUser_Name.Value = ""
        '
        'FrmUserGroupMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(629, 474)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FrmUserGroupMapping"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "User Group Mapping"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgv_Groupmapping.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv_Groupmapping, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblUserCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtUserName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgv_Groupmapping As common.UserControls.MyRadGridView
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenu_file As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem_Import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem_Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem_Close As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rbtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents TxtUserName As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rlblUserCode As common.Controls.MyLabel
    Friend WithEvents fndUser_Name As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class

