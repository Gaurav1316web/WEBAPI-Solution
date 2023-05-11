<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFaultCategory
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RMFile = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TxtCode = New common.UserControls.txtNavigator()
        Me.LblCode = New common.Controls.MyLabel()
        Me.LblDesp = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.TxtDesp = New common.Controls.MyTextBox()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.LblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblDesp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDesp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RMFile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(495, 20)
        Me.RadMenu1.TabIndex = 2
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RMFile
        '
        Me.RMFile.AccessibleDescription = "File"
        Me.RMFile.AccessibleName = "File"
        Me.RMFile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExport, Me.rmImport})
        Me.RMFile.Name = "RMFile"
        Me.RMFile.Text = "File"
        '
        'rmExport
        '
        Me.rmExport.AccessibleDescription = "Export"
        Me.rmExport.AccessibleName = "Export"
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export"
        '
        'rmImport
        '
        Me.rmImport.AccessibleDescription = "Import"
        Me.rmImport.AccessibleName = "Import"
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblDesp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtDesp)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(495, 131)
        Me.SplitContainer1.SplitterDistance = 82
        Me.SplitContainer1.TabIndex = 3
        '
        'TxtCode
        '
        Me.TxtCode.Location = New System.Drawing.Point(94, 12)
        Me.TxtCode.MendatroryField = True
        Me.TxtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.TxtCode.MyLinkLable1 = Me.LblCode
        Me.TxtCode.MyLinkLable2 = Nothing
        Me.TxtCode.MyMaxLength = 30
        Me.TxtCode.MyReadOnly = False
        Me.TxtCode.Name = "TxtCode"
        Me.TxtCode.Size = New System.Drawing.Size(202, 21)
        Me.TxtCode.TabIndex = 15
        Me.TxtCode.TabStop = False
        Me.TxtCode.Value = ""
        '
        'LblCode
        '
        Me.LblCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.LblCode.Location = New System.Drawing.Point(18, 14)
        Me.LblCode.Name = "LblCode"
        Me.LblCode.Size = New System.Drawing.Size(33, 16)
        Me.LblCode.TabIndex = 18
        Me.LblCode.Text = "Code"
        '
        'LblDesp
        '
        Me.LblDesp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDesp.Location = New System.Drawing.Point(18, 38)
        Me.LblDesp.Name = "LblDesp"
        Me.LblDesp.Size = New System.Drawing.Size(63, 16)
        Me.LblDesp.TabIndex = 19
        Me.LblDesp.Text = "Description"
        '
        'btnnew
        '
        Me.btnnew.Image = My.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(296, 12)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 21)
        Me.btnnew.TabIndex = 16
        '
        'TxtDesp
        '
        Me.TxtDesp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDesp.Location = New System.Drawing.Point(94, 37)
        Me.TxtDesp.MaxLength = 150
        Me.TxtDesp.MendatroryField = True
        Me.TxtDesp.MyLinkLable1 = Me.LblDesp
        Me.TxtDesp.MyLinkLable2 = Nothing
        Me.TxtDesp.Name = "TxtDesp"
        Me.TxtDesp.Size = New System.Drawing.Size(321, 18)
        Me.TxtDesp.TabIndex = 17
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(421, 13)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 18)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(6, 15)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 5
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(77, 15)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 6
        Me.btndelete.Text = "Delete"
        '
        'FrmFaultCategory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(495, 151)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmFaultCategory"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Fault Category"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.LblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblDesp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDesp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RMFile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TxtCode As common.UserControls.txtNavigator
    Friend WithEvents LblCode As common.Controls.MyLabel
    Friend WithEvents LblDesp As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents TxtDesp As common.Controls.MyTextBox
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
End Class
