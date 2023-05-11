<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmItemPriceListLevel3
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
        Me.rmi = New Telerik.WinControls.UI.RadMenuItem
        Me.rmiExport = New Telerik.WinControls.UI.RadMenuItem
        Me.rmiImport = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.dgvitem = New common.UserControls.MyRadGridView
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnclear = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dgvitem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvitem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rmi
        '
        Me.rmi.AccessibleDescription = "File"
        Me.rmi.AccessibleName = "File"
        Me.rmi.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExport, Me.rmiImport})
        Me.rmi.Name = "rmi"
        Me.rmi.Text = "File"
        Me.rmi.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rmiExport
        '
        Me.rmiExport.AccessibleDescription = "RadMenuItem1"
        Me.rmiExport.AccessibleName = "RadMenuItem1"
        Me.rmiExport.Name = "rmiExport"
        Me.rmiExport.Text = "Export"
        Me.rmiExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rmiImport
        '
        Me.rmiImport.AccessibleDescription = "RadMenuItem2"
        Me.rmiImport.AccessibleName = "RadMenuItem2"
        Me.rmiImport.Name = "rmiImport"
        Me.rmiImport.Text = "Import"
        Me.rmiImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvitem)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclear)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(831, 401)
        Me.SplitContainer1.SplitterDistance = 372
        Me.SplitContainer1.TabIndex = 55
        '
        'dgvitem
        '
        Me.dgvitem.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvitem.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvitem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvitem.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgvitem.ForeColor = System.Drawing.Color.Black
        Me.dgvitem.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvitem.Location = New System.Drawing.Point(0, 0)
        '
        'dgvitem
        '
        Me.dgvitem.MasterTemplate.EnableGrouping = False
        Me.dgvitem.Name = "dgvitem"
        Me.dgvitem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvitem.Size = New System.Drawing.Size(831, 372)
        Me.dgvitem.TabIndex = 0
        Me.dgvitem.TabStop = False
        Me.dgvitem.Text = "RadGridView1"
        '
        'btndelete
        '
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(79, 4)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnclear
        '
        Me.btnclear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear.Location = New System.Drawing.Point(758, 3)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(66, 18)
        Me.btnclear.TabIndex = 2
        Me.btnclear.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(7, 4)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmi})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(831, 20)
        Me.RadMenu1.TabIndex = 54
        Me.RadMenu1.Text = "RadMenu1"
        '
        'FrmItemPriceListLevel3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(831, 421)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmItemPriceListLevel3"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Item Price List Level3"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dgvitem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvitem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rmi As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents dgvitem As common.UserControls.MyRadGridView
    Friend WithEvents btnclear As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmiExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
End Class

