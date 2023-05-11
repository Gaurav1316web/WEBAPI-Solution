<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmItemGroup
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
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewCheckBoxColumn1 As Telerik.WinControls.UI.GridViewCheckBoxColumn = New Telerik.WinControls.UI.GridViewCheckBoxColumn
        Dim GridViewCheckBoxColumn2 As Telerik.WinControls.UI.GridViewCheckBoxColumn = New Telerik.WinControls.UI.GridViewCheckBoxColumn
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.mnimport = New Telerik.WinControls.UI.RadMenuItem
        Me.mnexport = New Telerik.WinControls.UI.RadMenuItem
        Me.mnclose = New Telerik.WinControls.UI.RadMenuItem
        Me.rdgrpbxitemgroup = New Telerik.WinControls.UI.RadGroupBox
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.dgvclassgroup = New common.UserControls.MyRadGridView
        Me.ddlclassname = New common.Controls.MyComboBox
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdgrpbxitemgroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgrpbxitemgroup.SuspendLayout()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvclassgroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvclassgroup.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlclassname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(467, 20)
        Me.RadMenu1.TabIndex = 6
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnimport, Me.mnexport, Me.mnclose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'mnimport
        '
        Me.mnimport.AccessibleDescription = "Import"
        Me.mnimport.AccessibleName = "Import"
        Me.mnimport.Name = "mnimport"
        Me.mnimport.Text = "Import"
        Me.mnimport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'mnexport
        '
        Me.mnexport.AccessibleDescription = "Export"
        Me.mnexport.AccessibleName = "Export"
        Me.mnexport.Name = "mnexport"
        Me.mnexport.Text = "Export"
        Me.mnexport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'mnclose
        '
        Me.mnclose.AccessibleDescription = "Close"
        Me.mnclose.AccessibleName = "Close"
        Me.mnclose.Name = "mnclose"
        Me.mnclose.Text = "Close"
        Me.mnclose.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rdgrpbxitemgroup
        '
        Me.rdgrpbxitemgroup.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgrpbxitemgroup.Controls.Add(Me.btnreset)
        Me.rdgrpbxitemgroup.Controls.Add(Me.dgvclassgroup)
        Me.rdgrpbxitemgroup.Controls.Add(Me.ddlclassname)
        Me.rdgrpbxitemgroup.Controls.Add(Me.RadLabel1)
        Me.rdgrpbxitemgroup.HeaderText = ""
        Me.rdgrpbxitemgroup.Location = New System.Drawing.Point(3, 3)
        Me.rdgrpbxitemgroup.Name = "rdgrpbxitemgroup"
        Me.rdgrpbxitemgroup.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgrpbxitemgroup.Size = New System.Drawing.Size(456, 276)
        Me.rdgrpbxitemgroup.TabIndex = 0
        '
        'btnreset
        '
        Me.btnreset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnreset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnreset.Location = New System.Drawing.Point(355, 23)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(22, 20)
        Me.btnreset.TabIndex = 1
        '
        'dgvclassgroup
        '
        Me.dgvclassgroup.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvclassgroup.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvclassgroup.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.dgvclassgroup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dgvclassgroup.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvclassgroup.Location = New System.Drawing.Point(13, 51)
        '
        'dgvclassgroup
        '
        Me.dgvclassgroup.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GridViewTextBoxColumn1.ColumnCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        GridViewTextBoxColumn1.HeaderText = "Class Code"
        GridViewTextBoxColumn1.Name = "column1"
        GridViewTextBoxColumn1.Width = 130
        GridViewTextBoxColumn2.HeaderText = "Description"
        GridViewTextBoxColumn2.MaxLength = 45
        GridViewTextBoxColumn2.Name = "column2"
        GridViewTextBoxColumn2.Width = 270
        GridViewCheckBoxColumn1.HeaderText = "Parent"
        GridViewCheckBoxColumn1.MinWidth = 20
        GridViewCheckBoxColumn1.Name = "column3"
        GridViewCheckBoxColumn1.Width = 100
        GridViewCheckBoxColumn2.HeaderText = "Child"
        GridViewCheckBoxColumn2.MinWidth = 20
        GridViewCheckBoxColumn2.Name = "column4"
        GridViewCheckBoxColumn2.Width = 100
        Me.dgvclassgroup.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewCheckBoxColumn1, GridViewCheckBoxColumn2})
        Me.dgvclassgroup.MasterTemplate.EnableFiltering = True
        Me.dgvclassgroup.MasterTemplate.EnableGrouping = False
        Me.dgvclassgroup.Name = "dgvclassgroup"
        Me.dgvclassgroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvclassgroup.Size = New System.Drawing.Size(424, 217)
        Me.dgvclassgroup.TabIndex = 2
        Me.dgvclassgroup.TabStop = False
        Me.dgvclassgroup.Text = "RadGridView1"
        '
        'ddlclassname
        '
        Me.ddlclassname.AllowShowFocusCues = False
        Me.ddlclassname.AutoCompleteDisplayMember = Nothing
        Me.ddlclassname.AutoCompleteValueMember = Nothing
        Me.ddlclassname.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlclassname.Location = New System.Drawing.Point(83, 23)
        Me.ddlclassname.MendatroryField = False
        Me.ddlclassname.MyLinkLable1 = Me.RadLabel1
        Me.ddlclassname.MyLinkLable2 = Nothing
        Me.ddlclassname.Name = "ddlclassname"
        Me.ddlclassname.Size = New System.Drawing.Size(267, 20)
        Me.ddlclassname.TabIndex = 0
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.RadLabel1.Location = New System.Drawing.Point(13, 23)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(70, 16)
        Me.RadLabel1.TabIndex = 6
        Me.RadLabel1.Text = "Class Name"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(395, 2)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(69, 20)
        Me.btnclose.TabIndex = 1
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(3, 2)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(69, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdgrpbxitemgroup)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(467, 315)
        Me.SplitContainer1.SplitterDistance = 286
        Me.SplitContainer1.TabIndex = 7
        '
        'frmItemGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 335)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmItemGroup"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Item Group"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdgrpbxitemgroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgrpbxitemgroup.ResumeLayout(False)
        Me.rdgrpbxitemgroup.PerformLayout()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvclassgroup.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvclassgroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlclassname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnclose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdgrpbxitemgroup As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents dgvclassgroup As common.UserControls.MyRadGridView
    Friend WithEvents ddlclassname As common.Controls.MyComboBox
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

