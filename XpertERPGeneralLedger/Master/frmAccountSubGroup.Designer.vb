<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAccountSubGroup
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.lblAccGrp = New common.Controls.MyLabel
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem
        Me.txtAccGrp = New common.UserControls.txtFinder
        Me.RadLabel23 = New common.Controls.MyLabel
        Me.fndaccgp = New common.UserControls.txtNavigator
        Me.lblaccgp = New common.Controls.MyLabel
        Me.txtdes = New common.Controls.MyTextBox
        Me.lbldes = New common.Controls.MyLabel
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblAccGrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblaccgp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAccGrp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAccGrp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel23)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndaccgp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblaccgp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtdes)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbldes)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(511, 144)
        Me.SplitContainer1.SplitterDistance = 107
        Me.SplitContainer1.TabIndex = 0
        '
        'lblAccGrp
        '
        Me.lblAccGrp.AutoSize = False
        Me.lblAccGrp.BorderVisible = True
        Me.lblAccGrp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccGrp.Location = New System.Drawing.Point(246, 80)
        Me.lblAccGrp.Name = "lblAccGrp"
        Me.lblAccGrp.Size = New System.Drawing.Size(256, 18)
        Me.lblAccGrp.TabIndex = 45
        Me.lblAccGrp.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblAccGrp.TextWrap = False
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(511, 20)
        Me.RadMenu1.TabIndex = 47
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmImport, Me.rmExport})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rmImport
        '
        Me.rmImport.AccessibleDescription = "RadMenuItem2"
        Me.rmImport.AccessibleName = "RadMenuItem2"
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
        Me.rmImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rmExport
        '
        Me.rmExport.AccessibleDescription = "RadMenuItem3"
        Me.rmExport.AccessibleName = "RadMenuItem3"
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export"
        Me.rmExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'txtAccGrp
        '
        Me.txtAccGrp.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txtAccGrp.Location = New System.Drawing.Point(102, 79)
        Me.txtAccGrp.MendatroryField = True
        Me.txtAccGrp.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccGrp.MyLinkLable1 = Me.RadLabel23
        Me.txtAccGrp.MyLinkLable2 = Nothing
        Me.txtAccGrp.MyReadOnly = False
        Me.txtAccGrp.MyShowMasterFormButton = False
        Me.txtAccGrp.Name = "txtAccGrp"
        Me.txtAccGrp.Size = New System.Drawing.Size(143, 19)
        Me.txtAccGrp.TabIndex = 3
        Me.txtAccGrp.Value = ""
        '
        'RadLabel23
        '
        Me.RadLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel23.Location = New System.Drawing.Point(10, 80)
        Me.RadLabel23.Name = "RadLabel23"
        Me.RadLabel23.Size = New System.Drawing.Size(82, 16)
        Me.RadLabel23.TabIndex = 44
        Me.RadLabel23.Text = "Account Group"
        '
        'fndaccgp
        '
        Me.fndaccgp.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.fndaccgp.Location = New System.Drawing.Point(102, 32)
        Me.fndaccgp.MendatroryField = True
        Me.fndaccgp.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndaccgp.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndaccgp.MyLinkLable1 = Me.lblaccgp
        Me.fndaccgp.MyLinkLable2 = Nothing
        Me.fndaccgp.MyMaxLength = 30
        Me.fndaccgp.MyReadOnly = False
        Me.fndaccgp.Name = "fndaccgp"
        Me.fndaccgp.Size = New System.Drawing.Size(202, 21)
        Me.fndaccgp.TabIndex = 0
        Me.fndaccgp.TabStop = False
        Me.fndaccgp.Value = ""
        '
        'lblaccgp
        '
        Me.lblaccgp.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblaccgp.Location = New System.Drawing.Point(10, 34)
        Me.lblaccgp.Name = "lblaccgp"
        Me.lblaccgp.Size = New System.Drawing.Size(61, 16)
        Me.lblaccgp.TabIndex = 44
        Me.lblaccgp.Text = "Sub Group"
        '
        'txtdes
        '
        Me.txtdes.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txtdes.Location = New System.Drawing.Point(102, 56)
        Me.txtdes.MaxLength = 100
        Me.txtdes.MendatroryField = True
        Me.txtdes.MyLinkLable1 = Me.lbldes
        Me.txtdes.MyLinkLable2 = Nothing
        Me.txtdes.Name = "txtdes"
        '
        '
        '
        Me.txtdes.RootElement.StretchVertically = True
        Me.txtdes.Size = New System.Drawing.Size(400, 20)
        Me.txtdes.TabIndex = 2
        '
        'lbldes
        '
        Me.lbldes.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lbldes.Location = New System.Drawing.Point(10, 58)
        Me.lbldes.Name = "lbldes"
        Me.lbldes.Size = New System.Drawing.Size(63, 16)
        Me.lbldes.TabIndex = 43
        Me.lbldes.Text = "Description"
        '
        'btnnew
        '
        Me.btnnew.Image = My.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(305, 32)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(14, 21)
        Me.btnnew.TabIndex = 1
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(442, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 9
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(5, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 7
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(74, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 8
        Me.btndelete.Text = "Delete"
        '
        'FrmAccountSubGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(511, 144)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmAccountSubGroup"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Account Main Group"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblAccGrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblaccgp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents fndaccgp As common.UserControls.txtNavigator
    Friend WithEvents lblaccgp As common.Controls.MyLabel
    Friend WithEvents txtdes As common.Controls.MyTextBox
    Friend WithEvents lbldes As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblAccGrp As common.Controls.MyLabel
    Friend WithEvents txtAccGrp As common.UserControls.txtFinder
    Friend WithEvents RadLabel23 As common.Controls.MyLabel
End Class

