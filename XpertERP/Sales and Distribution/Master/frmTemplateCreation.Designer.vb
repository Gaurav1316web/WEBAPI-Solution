<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTemplateCreation
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
        Me.TxtTmplateId = New common.UserControls.txtNavigator
        Me.lblvendor = New common.Controls.MyLabel
        Me.txtDesc = New common.Controls.MyTextBox
        Me.RadLabel12 = New common.Controls.MyLabel
        Me.dtpStartDate = New common.Controls.MyDateTimePicker
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.dgvCustomer = New common.UserControls.MyRadGridView
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItemExport = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItemExportt = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItemImport = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItemExit = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnAddCustomer = New Telerik.WinControls.UI.RadButton
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        CType(Me.lblvendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.btnAddCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtTmplateId
        '
        Me.TxtTmplateId.Location = New System.Drawing.Point(106, 5)
        Me.TxtTmplateId.MendatroryField = False
        Me.TxtTmplateId.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.TxtTmplateId.MyLinkLable1 = Me.lblvendor
        Me.TxtTmplateId.MyLinkLable2 = Nothing
        Me.TxtTmplateId.MyMaxLength = 32767
        Me.TxtTmplateId.MyReadOnly = False
        Me.TxtTmplateId.Name = "TxtTmplateId"
        Me.TxtTmplateId.Size = New System.Drawing.Size(243, 21)
        Me.TxtTmplateId.TabIndex = 0
        Me.TxtTmplateId.Value = ""
        '
        'lblvendor
        '
        Me.lblvendor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvendor.Location = New System.Drawing.Point(13, 8)
        Me.lblvendor.Name = "lblvendor"
        Me.lblvendor.Size = New System.Drawing.Size(66, 16)
        Me.lblvendor.TabIndex = 44
        Me.lblvendor.Text = "Template Id"
        '
        'txtDesc
        '
        Me.txtDesc.Location = New System.Drawing.Point(106, 28)
        Me.txtDesc.MaxLength = 50
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel12
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(661, 20)
        Me.txtDesc.TabIndex = 2
        '
        'RadLabel12
        '
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.Location = New System.Drawing.Point(13, 29)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel12.TabIndex = 43
        Me.RadLabel12.Text = "Description"
        '
        'dtpStartDate
        '
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.dtpStartDate.Location = New System.Drawing.Point(106, 50)
        Me.dtpStartDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpStartDate.MendatroryField = False
        Me.dtpStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartDate.MyLinkLable1 = Me.RadLabel1
        Me.dtpStartDate.MyLinkLable2 = Nothing
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartDate.Size = New System.Drawing.Size(116, 20)
        Me.dtpStartDate.TabIndex = 3
        Me.dtpStartDate.Text = "RadDateTimePicker1"
        Me.dtpStartDate.Value = New Date(2012, 3, 13, 12, 7, 19, 78)
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(13, 51)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(57, 16)
        Me.RadLabel1.TabIndex = 50
        Me.RadLabel1.Text = "Start Date"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(704, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(73, 4)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(4, 4)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'dgvCustomer
        '
        Me.dgvCustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCustomer.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgvCustomer.ForeColor = System.Drawing.Color.Black
        Me.dgvCustomer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvCustomer.Location = New System.Drawing.Point(0, 76)
        '
        'dgvCustomer
        '
        Me.dgvCustomer.MasterTemplate.EnableGrouping = False
        Me.dgvCustomer.Name = "dgvCustomer"
        Me.dgvCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        '
        '
        '
        Me.dgvCustomer.RootElement.ForeColor = System.Drawing.Color.Black
        Me.dgvCustomer.Size = New System.Drawing.Size(776, 229)
        Me.dgvCustomer.TabIndex = 1
        Me.dgvCustomer.Text = "RadGridView1"
        '
        'RadMenu1
        '
        Me.RadMenu1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItemExport})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(776, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItemExport
        '
        Me.RadMenuItemExport.AccessibleDescription = "File"
        Me.RadMenuItemExport.AccessibleName = "File"
        Me.RadMenuItemExport.Class = ""
        Me.RadMenuItemExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItemExportt, Me.RadMenuItemImport, Me.RadMenuItemExit})
        Me.RadMenuItemExport.Name = "RadMenuItemExport"
        Me.RadMenuItemExport.Text = "File"
        Me.RadMenuItemExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItemExportt
        '
        Me.RadMenuItemExportt.AccessibleDescription = "Export"
        Me.RadMenuItemExportt.AccessibleName = "Export"
        Me.RadMenuItemExportt.Name = "RadMenuItemExportt"
        Me.RadMenuItemExportt.Text = "Export"
        Me.RadMenuItemExportt.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItemImport
        '
        Me.RadMenuItemImport.AccessibleDescription = "Import"
        Me.RadMenuItemImport.AccessibleName = "Import"
        Me.RadMenuItemImport.Name = "RadMenuItemImport"
        Me.RadMenuItemImport.Text = "Import"
        Me.RadMenuItemImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItemExit
        '
        Me.RadMenuItemExit.AccessibleDescription = "Exit"
        Me.RadMenuItemExit.AccessibleName = "Exit"
        Me.RadMenuItemExit.Name = "RadMenuItemExit"
        Me.RadMenuItemExit.Text = "Exit"
        Me.RadMenuItemExit.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvCustomer)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(776, 334)
        Me.SplitContainer1.SplitterDistance = 305
        Me.SplitContainer1.TabIndex = 12
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnAddCustomer)
        Me.Panel1.Controls.Add(Me.lblvendor)
        Me.Panel1.Controls.Add(Me.TxtTmplateId)
        Me.Panel1.Controls.Add(Me.RadLabel12)
        Me.Panel1.Controls.Add(Me.txtDesc)
        Me.Panel1.Controls.Add(Me.btnnew)
        Me.Panel1.Controls.Add(Me.dtpStartDate)
        Me.Panel1.Controls.Add(Me.RadLabel1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(776, 76)
        Me.Panel1.TabIndex = 0
        '
        'btnAddCustomer
        '
        Me.btnAddCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddCustomer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddCustomer.Location = New System.Drawing.Point(658, 54)
        Me.btnAddCustomer.Name = "btnAddCustomer"
        Me.btnAddCustomer.Size = New System.Drawing.Size(106, 18)
        Me.btnAddCustomer.TabIndex = 4
        Me.btnAddCustomer.Text = "Add Customers"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(354, 6)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(17, 20)
        Me.btnnew.TabIndex = 1
        '
        'FrmTemplateCreation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(776, 354)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.KeyPreview = True
        Me.Name = "FrmTemplateCreation"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Template Creation"
        CType(Me.lblvendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.btnAddCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents dgvCustomer As common.UserControls.MyRadGridView
    Friend WithEvents dtpStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents TxtTmplateId As common.UserControls.txtNavigator
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemExportt As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemExit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnAddCustomer As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblvendor As common.Controls.MyLabel
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
End Class

