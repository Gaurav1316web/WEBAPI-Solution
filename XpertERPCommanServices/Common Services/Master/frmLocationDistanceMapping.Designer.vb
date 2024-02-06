<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLocationDistanceMapping
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.fndCustomer = New common.UserControls.txtFinder()
        Me.lblvendor = New common.Controls.MyLabel()
        Me.dgvitem = New common.UserControls.MyRadGridView()
        Me.Import = New Telerik.WinControls.UI.RadMenuItem()
        Me.txtdesc = New common.Controls.MyLabel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.grpFull = New System.Windows.Forms.GroupBox()
        Me.rdbSale = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbTransfer = New Telerik.WinControls.UI.RadRadioButton()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.btnclear = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.lblvendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvitem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvitem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.grpFull.SuspendLayout()
        CType(Me.rdbSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'fndCustomer
        '
        Me.fndCustomer.CalculationExpression = Nothing
        Me.fndCustomer.FieldCode = Nothing
        Me.fndCustomer.FieldDesc = Nothing
        Me.fndCustomer.FieldMaxLength = 0
        Me.fndCustomer.FieldName = Nothing
        Me.fndCustomer.isCalculatedField = False
        Me.fndCustomer.IsSourceFromTable = False
        Me.fndCustomer.IsSourceFromValueList = False
        Me.fndCustomer.IsUnique = False
        Me.fndCustomer.Location = New System.Drawing.Point(83, 4)
        Me.fndCustomer.MendatroryField = False
        Me.fndCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCustomer.MyLinkLable1 = Me.lblvendor
        Me.fndCustomer.MyLinkLable2 = Nothing
        Me.fndCustomer.MyReadOnly = False
        Me.fndCustomer.MyShowMasterFormButton = False
        Me.fndCustomer.Name = "fndCustomer"
        Me.fndCustomer.ReferenceFieldDesc = Nothing
        Me.fndCustomer.ReferenceFieldName = Nothing
        Me.fndCustomer.ReferenceTableName = Nothing
        Me.fndCustomer.Size = New System.Drawing.Size(156, 18)
        Me.fndCustomer.TabIndex = 40
        Me.fndCustomer.Value = ""
        '
        'lblvendor
        '
        Me.lblvendor.FieldName = Nothing
        Me.lblvendor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvendor.Location = New System.Drawing.Point(4, 5)
        Me.lblvendor.Name = "lblvendor"
        Me.lblvendor.Size = New System.Drawing.Size(49, 16)
        Me.lblvendor.TabIndex = 44
        Me.lblvendor.Text = "Location"
        '
        'dgvitem
        '
        Me.dgvitem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvitem.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvitem.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvitem.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgvitem.ForeColor = System.Drawing.Color.Black
        Me.dgvitem.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvitem.Location = New System.Drawing.Point(7, 33)
        '
        'dgvitem
        '
        Me.dgvitem.MasterTemplate.EnableGrouping = False
        Me.dgvitem.Name = "dgvitem"
        Me.dgvitem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvitem.Size = New System.Drawing.Size(817, 331)
        Me.dgvitem.TabIndex = 1
        Me.dgvitem.Text = "RadGridView1"
        '
        'Import
        '
        Me.Import.AccessibleDescription = "Import"
        Me.Import.AccessibleName = "Import"
        Me.Import.Name = "Import"
        Me.Import.Text = "Import"
        '
        'txtdesc
        '
        Me.txtdesc.AutoSize = False
        Me.txtdesc.BorderVisible = True
        Me.txtdesc.FieldName = Nothing
        Me.txtdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdesc.Location = New System.Drawing.Point(259, 4)
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.Size = New System.Drawing.Size(283, 18)
        Me.txtdesc.TabIndex = 43
        Me.txtdesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvitem)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclear)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(831, 401)
        Me.SplitContainer1.SplitterDistance = 371
        Me.SplitContainer1.TabIndex = 54
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.grpFull)
        Me.Panel1.Controls.Add(Me.lblvendor)
        Me.Panel1.Controls.Add(Me.fndCustomer)
        Me.Panel1.Controls.Add(Me.btnnew)
        Me.Panel1.Controls.Add(Me.txtdesc)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(831, 27)
        Me.Panel1.TabIndex = 0
        '
        'grpFull
        '
        Me.grpFull.Controls.Add(Me.rdbSale)
        Me.grpFull.Controls.Add(Me.rdbTransfer)
        Me.grpFull.Location = New System.Drawing.Point(557, -3)
        Me.grpFull.Name = "grpFull"
        Me.grpFull.Size = New System.Drawing.Size(161, 26)
        Me.grpFull.TabIndex = 55
        Me.grpFull.TabStop = False
        '
        'rdbSale
        '
        Me.rdbSale.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbSale.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.rdbSale.Location = New System.Drawing.Point(4, 9)
        Me.rdbSale.Name = "rdbSale"
        Me.rdbSale.Size = New System.Drawing.Size(43, 16)
        Me.rdbSale.TabIndex = 1
        Me.rdbSale.Text = "Sale"
        Me.rdbSale.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rdbTransfer
        '
        Me.rdbTransfer.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.rdbTransfer.Location = New System.Drawing.Point(89, 9)
        Me.rdbTransfer.Name = "rdbTransfer"
        Me.rdbTransfer.Size = New System.Drawing.Size(62, 16)
        Me.rdbTransfer.TabIndex = 0
        Me.rdbTransfer.Text = "Transfer"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPCommanServices.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(239, 4)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(19, 19)
        Me.btnnew.TabIndex = 49
        '
        'btnclear
        '
        Me.btnclear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear.Location = New System.Drawing.Point(758, 4)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(66, 18)
        Me.btnclear.TabIndex = 48
        Me.btnclear.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(7, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 46
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(78, 5)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 47
        Me.btndelete.Text = "Delete"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(831, 20)
        Me.RadMenu1.TabIndex = 53
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Export, Me.Import})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'Export
        '
        Me.Export.AccessibleDescription = "Export"
        Me.Export.AccessibleName = "Export"
        Me.Export.Name = "Export"
        Me.Export.Text = "Export"
        '
        'frmLocationDistanceMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(831, 421)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmLocationDistanceMapping"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Location Distance Mapping"
        CType(Me.lblvendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvitem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvitem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.grpFull.ResumeLayout(False)
        Me.grpFull.PerformLayout()
        CType(Me.rdbSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents fndCustomer As common.UserControls.txtFinder
    Friend WithEvents dgvitem As common.UserControls.MyRadGridView
    Friend WithEvents Import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtdesc As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclear As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblvendor As common.Controls.MyLabel
    Friend WithEvents grpFull As System.Windows.Forms.GroupBox
    Friend WithEvents rdbSale As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbTransfer As Telerik.WinControls.UI.RadRadioButton
End Class

