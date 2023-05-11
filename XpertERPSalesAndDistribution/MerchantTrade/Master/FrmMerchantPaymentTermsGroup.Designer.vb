<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMerchantPaymentTermsGroup
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
        Dim GridViewTextBoxColumn5 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn6 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewCheckBoxColumn3 As Telerik.WinControls.UI.GridViewCheckBoxColumn = New Telerik.WinControls.UI.GridViewCheckBoxColumn
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenu_file = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.fndGroupCode = New common.UserControls.txtNavigator
        Me.lblcode = New common.Controls.MyLabel
        Me.TxtDescription = New common.Controls.MyTextBox
        Me.lblname = New common.Controls.MyLabel
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.dgv_Groupmapping = New common.UserControls.MyRadGridView
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.RMImport = New Telerik.WinControls.UI.RadMenuItem
        Me.RMExport = New Telerik.WinControls.UI.RadMenuItem
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv_Groupmapping, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv_Groupmapping.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenu_file})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(576, 20)
        Me.RadMenu1.TabIndex = 7
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenu_file
        '
        Me.RadMenu_file.AccessibleDescription = "File"
        Me.RadMenu_file.AccessibleName = "File"
        Me.RadMenu_file.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenu_file.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RMImport, Me.RMExport})
        Me.RadMenu_file.Name = "RadMenu_file"
        Me.RadMenu_file.Text = "File"
        Me.RadMenu_file.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(576, 374)
        Me.SplitContainer1.SplitterDistance = 340
        Me.SplitContainer1.TabIndex = 8
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndGroupCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblcode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.TxtDescription)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblname)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnnew)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.dgv_Groupmapping)
        Me.SplitContainer2.Size = New System.Drawing.Size(576, 340)
        Me.SplitContainer2.SplitterDistance = 53
        Me.SplitContainer2.TabIndex = 0
        '
        'fndGroupCode
        '
        Me.fndGroupCode.Location = New System.Drawing.Point(89, 4)
        Me.fndGroupCode.MendatroryField = True
        Me.fndGroupCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndGroupCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndGroupCode.MyLinkLable1 = Nothing
        Me.fndGroupCode.MyLinkLable2 = Nothing
        Me.fndGroupCode.MyMaxLength = 32767
        Me.fndGroupCode.MyReadOnly = False
        Me.fndGroupCode.Name = "fndGroupCode"
        Me.fndGroupCode.Size = New System.Drawing.Size(215, 21)
        Me.fndGroupCode.TabIndex = 6
        Me.fndGroupCode.Value = ""
        '
        'lblcode
        '
        Me.lblcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcode.Location = New System.Drawing.Point(12, 6)
        Me.lblcode.Name = "lblcode"
        Me.lblcode.Size = New System.Drawing.Size(33, 16)
        Me.lblcode.TabIndex = 5
        Me.lblcode.Text = "Code"
        '
        'TxtDescription
        '
        Me.TxtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDescription.Location = New System.Drawing.Point(89, 29)
        Me.TxtDescription.MaxLength = 200
        Me.TxtDescription.MendatroryField = True
        Me.TxtDescription.MyLinkLable1 = Nothing
        Me.TxtDescription.MyLinkLable2 = Nothing
        Me.TxtDescription.Name = "TxtDescription"
        '
        '
        '
        Me.TxtDescription.RootElement.StretchVertically = True
        Me.TxtDescription.Size = New System.Drawing.Size(339, 20)
        Me.TxtDescription.TabIndex = 9
        '
        'lblname
        '
        Me.lblname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblname.Location = New System.Drawing.Point(12, 31)
        Me.lblname.Name = "lblname"
        Me.lblname.Size = New System.Drawing.Size(63, 16)
        Me.lblname.TabIndex = 8
        Me.lblname.Text = "Description"
        '
        'btnnew
        '
        Me.btnnew.Image = XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(307, 4)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 7
        '
        'dgv_Groupmapping
        '
        Me.dgv_Groupmapping.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv_Groupmapping.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgv_Groupmapping.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.dgv_Groupmapping.ForeColor = System.Drawing.Color.Black
        Me.dgv_Groupmapping.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgv_Groupmapping.Location = New System.Drawing.Point(5, 2)
        '
        'dgv_Groupmapping
        '
        Me.dgv_Groupmapping.MasterTemplate.AllowAddNewRow = False
        GridViewTextBoxColumn5.HeaderText = "Terms Code"
        GridViewTextBoxColumn5.Name = "TermsCode"
        GridViewTextBoxColumn5.ReadOnly = True
        GridViewTextBoxColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        GridViewTextBoxColumn5.Width = 180
        GridViewTextBoxColumn6.HeaderText = "Description"
        GridViewTextBoxColumn6.Name = "Description"
        GridViewTextBoxColumn6.ReadOnly = True
        GridViewTextBoxColumn6.Width = 197
        GridViewCheckBoxColumn3.HeaderText = "Status"
        GridViewCheckBoxColumn3.MinWidth = 20
        GridViewCheckBoxColumn3.Name = "Status"
        GridViewCheckBoxColumn3.Width = 169
        Me.dgv_Groupmapping.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn5, GridViewTextBoxColumn6, GridViewCheckBoxColumn3})
        Me.dgv_Groupmapping.MasterTemplate.EnableFiltering = True
        Me.dgv_Groupmapping.MasterTemplate.EnableGrouping = False
        Me.dgv_Groupmapping.Name = "dgv_Groupmapping"
        Me.dgv_Groupmapping.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgv_Groupmapping.Size = New System.Drawing.Size(567, 274)
        Me.dgv_Groupmapping.TabIndex = 1
        Me.dgv_Groupmapping.TabStop = False
        Me.dgv_Groupmapping.Text = "dgv Groupmapping"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(5, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 3
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(78, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 4
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(506, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 5
        Me.btnclose.Text = "Close"
        '
        'RMImport
        '
        Me.RMImport.AccessibleDescription = "Import"
        Me.RMImport.AccessibleName = "Import"
        Me.RMImport.Name = "RMImport"
        Me.RMImport.Text = "Import"
        Me.RMImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RMExport
        '
        Me.RMExport.AccessibleDescription = "Export"
        Me.RMExport.AccessibleName = "Export"
        Me.RMExport.Name = "RMExport"
        Me.RMExport.Text = "Export"
        Me.RMExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'FrmMerchantPaymentTermsGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(576, 394)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmMerchantPaymentTermsGroup"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Merchant Payment Terms Group"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv_Groupmapping.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv_Groupmapping, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenu_file As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents fndGroupCode As common.UserControls.txtNavigator
    Friend WithEvents lblcode As common.Controls.MyLabel
    Friend WithEvents TxtDescription As common.Controls.MyTextBox
    Friend WithEvents lblname As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents dgv_Groupmapping As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RMImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMExport As Telerik.WinControls.UI.RadMenuItem
End Class

