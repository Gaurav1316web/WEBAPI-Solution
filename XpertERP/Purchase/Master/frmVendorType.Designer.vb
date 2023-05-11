<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVendorType
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVendorType))
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.rdmenuimport = New Telerik.WinControls.UI.RadMenuItem
        Me.rdmenuexport = New Telerik.WinControls.UI.RadMenuItem
        Me.rdmenuexit = New Telerik.WinControls.UI.RadMenuItem
        Me.lblDescription = New common.Controls.MyLabel
        Me.rdbtnDelete = New Telerik.WinControls.UI.RadButton
        Me.grpCustomer = New Telerik.WinControls.UI.RadGroupBox
        Me.fndvendorid = New common.UserControls.txtNavigator
        Me.lblCustomerId = New common.Controls.MyLabel
        Me.rdtxtvendordesc = New common.Controls.MyTextBox
        Me.rdbtnreset = New Telerik.WinControls.UI.RadButton
        Me.rdbtnSave = New Telerik.WinControls.UI.RadButton
        Me.rdbtnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCustomer.SuspendLayout()
        CType(Me.lblCustomerId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtvendordesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadMenu1.Size = New System.Drawing.Size(429, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "rdmenufile"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenuimport, Me.rdmenuexport, Me.rdmenuexit})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rdmenuimport
        '
        Me.rdmenuimport.AccessibleDescription = "Import"
        Me.rdmenuimport.AccessibleName = "rdmenuimport"
        Me.rdmenuimport.Name = "rdmenuimport"
        Me.rdmenuimport.Text = "Import"
        Me.rdmenuimport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rdmenuexport
        '
        Me.rdmenuexport.AccessibleDescription = "Export"
        Me.rdmenuexport.AccessibleName = "rdmenuexport"
        Me.rdmenuexport.Name = "rdmenuexport"
        Me.rdmenuexport.Text = "Export"
        Me.rdmenuexport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rdmenuexit
        '
        Me.rdmenuexit.AccessibleDescription = "Exit"
        Me.rdmenuexit.AccessibleName = "rdmenuexit"
        Me.rdmenuexit.Name = "rdmenuexit"
        Me.rdmenuexit.Text = "Exit"
        Me.rdmenuexit.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'lblDescription
        '
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(8, 52)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 2
        Me.lblDescription.Text = "Description"
        '
        'rdbtnDelete
        '
        Me.rdbtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnDelete.Location = New System.Drawing.Point(75, 5)
        Me.rdbtnDelete.Name = "rdbtnDelete"
        Me.rdbtnDelete.Size = New System.Drawing.Size(68, 18)
        Me.rdbtnDelete.TabIndex = 1
        Me.rdbtnDelete.Text = " Delete"
        '
        'grpCustomer
        '
        Me.grpCustomer.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpCustomer.Controls.Add(Me.fndvendorid)
        Me.grpCustomer.Controls.Add(Me.lblDescription)
        Me.grpCustomer.Controls.Add(Me.lblCustomerId)
        Me.grpCustomer.Controls.Add(Me.rdtxtvendordesc)
        Me.grpCustomer.Controls.Add(Me.rdbtnreset)
        Me.grpCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCustomer.HeaderText = ""
        Me.grpCustomer.Location = New System.Drawing.Point(0, 0)
        Me.grpCustomer.Name = "grpCustomer"
        Me.grpCustomer.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpCustomer.Size = New System.Drawing.Size(429, 278)
        Me.grpCustomer.TabIndex = 0
        '
        'fndvendorid
        '
        Me.fndvendorid.Location = New System.Drawing.Point(85, 21)
        Me.fndvendorid.MendatroryField = False
        Me.fndvendorid.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndvendorid.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndvendorid.MyLinkLable1 = Me.lblCustomerId
        Me.fndvendorid.MyLinkLable2 = Nothing
        Me.fndvendorid.MyMaxLength = 12
        Me.fndvendorid.MyReadOnly = False
        Me.fndvendorid.Name = "fndvendorid"
        Me.fndvendorid.Size = New System.Drawing.Size(224, 21)
        Me.fndvendorid.TabIndex = 0
        Me.fndvendorid.Value = ""
        '
        'lblCustomerId
        '
        Me.lblCustomerId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerId.Location = New System.Drawing.Point(8, 23)
        Me.lblCustomerId.Name = "lblCustomerId"
        Me.lblCustomerId.Size = New System.Drawing.Size(71, 16)
        Me.lblCustomerId.TabIndex = 3
        Me.lblCustomerId.Text = "Vendor Type"
        '
        'rdtxtvendordesc
        '
        Me.rdtxtvendordesc.AutoSize = False
        Me.rdtxtvendordesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdtxtvendordesc.Location = New System.Drawing.Point(85, 50)
        Me.rdtxtvendordesc.MaxLength = 50
        Me.rdtxtvendordesc.MendatroryField = False
        Me.rdtxtvendordesc.Multiline = True
        Me.rdtxtvendordesc.MyLinkLable1 = Me.lblDescription
        Me.rdtxtvendordesc.MyLinkLable2 = Nothing
        Me.rdtxtvendordesc.Name = "rdtxtvendordesc"
        Me.rdtxtvendordesc.Size = New System.Drawing.Size(306, 21)
        Me.rdtxtvendordesc.TabIndex = 2
        Me.rdtxtvendordesc.Text = " "
        '
        'rdbtnreset
        '
        Me.rdbtnreset.Image = CType(resources.GetObject("rdbtnreset.Image"), System.Drawing.Image)
        Me.rdbtnreset.Location = New System.Drawing.Point(312, 20)
        Me.rdbtnreset.Name = "rdbtnreset"
        Me.rdbtnreset.Size = New System.Drawing.Size(16, 23)
        Me.rdbtnreset.TabIndex = 1
        Me.rdbtnreset.Text = " "
        '
        'rdbtnSave
        '
        Me.rdbtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnSave.Location = New System.Drawing.Point(6, 5)
        Me.rdbtnSave.Name = "rdbtnSave"
        Me.rdbtnSave.Size = New System.Drawing.Size(68, 18)
        Me.rdbtnSave.TabIndex = 0
        Me.rdbtnSave.Text = "Save"
        '
        'rdbtnClose
        '
        Me.rdbtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnClose.Location = New System.Drawing.Point(354, 5)
        Me.rdbtnClose.Name = "rdbtnClose"
        Me.rdbtnClose.Size = New System.Drawing.Size(68, 18)
        Me.rdbtnClose.TabIndex = 2
        Me.rdbtnClose.Text = "Close"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.grpCustomer)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(429, 308)
        Me.SplitContainer1.SplitterDistance = 278
        Me.SplitContainer1.TabIndex = 1
        '
        'frmVendorType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(429, 328)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmVendorType"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmVendorType"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCustomer.ResumeLayout(False)
        Me.grpCustomer.PerformLayout()
        CType(Me.lblCustomerId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtvendordesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdbtnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents grpCustomer As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdtxtvendordesc As common.Controls.MyTextBox
    Friend WithEvents rdbtnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents lblCustomerId As common.Controls.MyLabel
    Friend WithEvents fndvendorid As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

