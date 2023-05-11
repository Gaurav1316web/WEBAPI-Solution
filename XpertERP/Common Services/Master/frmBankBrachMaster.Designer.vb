<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBankBrachMaster
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.lblIFSCCode = New common.Controls.MyLabel
        Me.txtIFSCCode = New common.Controls.MyTextBox
        Me.lblBankName = New common.Controls.MyLabel
        Me.fndBankCode = New common.UserControls.txtFinder
        Me.lblBankCode = New common.Controls.MyLabel
        Me.fndBranchCode = New common.UserControls.txtNavigator
        Me.lblCode = New common.Controls.MyLabel
        Me.lblBranchName = New common.Controls.MyLabel
        Me.rbtnReset = New Telerik.WinControls.UI.RadButton
        Me.txtBranchName = New common.Controls.MyTextBox
        Me.rdmenuimport = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenufile = New Telerik.WinControls.UI.RadMenuItem
        Me.rdmenuexport = New Telerik.WinControls.UI.RadMenuItem
        Me.rdmenuexit = New Telerik.WinControls.UI.RadMenuItem
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.rbtnSave = New Telerik.WinControls.UI.RadButton
        Me.rbtnClose = New Telerik.WinControls.UI.RadButton
        Me.rbtnDelete = New Telerik.WinControls.UI.RadButton
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblIFSCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIFSCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBranchName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBranchName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.rbtnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.lblIFSCCode)
        Me.RadGroupBox1.Controls.Add(Me.txtIFSCCode)
        Me.RadGroupBox1.Controls.Add(Me.lblBankName)
        Me.RadGroupBox1.Controls.Add(Me.fndBankCode)
        Me.RadGroupBox1.Controls.Add(Me.lblBankCode)
        Me.RadGroupBox1.Controls.Add(Me.fndBranchCode)
        Me.RadGroupBox1.Controls.Add(Me.lblCode)
        Me.RadGroupBox1.Controls.Add(Me.lblBranchName)
        Me.RadGroupBox1.Controls.Add(Me.rbtnReset)
        Me.RadGroupBox1.Controls.Add(Me.txtBranchName)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 40)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(558, 148)
        Me.RadGroupBox1.TabIndex = 0
        '
        'lblIFSCCode
        '
        Me.lblIFSCCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIFSCCode.Location = New System.Drawing.Point(12, 85)
        Me.lblIFSCCode.Name = "lblIFSCCode"
        Me.lblIFSCCode.Size = New System.Drawing.Size(62, 16)
        Me.lblIFSCCode.TabIndex = 9
        Me.lblIFSCCode.Text = "IFSC Code"
        '
        'txtIFSCCode
        '
        Me.txtIFSCCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIFSCCode.Location = New System.Drawing.Point(112, 81)
        Me.txtIFSCCode.MaxLength = 50
        Me.txtIFSCCode.MendatroryField = False
        Me.txtIFSCCode.MyLinkLable1 = Me.lblIFSCCode
        Me.txtIFSCCode.MyLinkLable2 = Nothing
        Me.txtIFSCCode.Name = "txtIFSCCode"
        Me.txtIFSCCode.Size = New System.Drawing.Size(220, 18)
        Me.txtIFSCCode.TabIndex = 2
        '
        'lblBankName
        '
        Me.lblBankName.AutoSize = False
        Me.lblBankName.BorderVisible = True
        Me.lblBankName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankName.Location = New System.Drawing.Point(265, 112)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(287, 18)
        Me.lblBankName.TabIndex = 7
        Me.lblBankName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBankName.TextWrap = False
        '
        'fndBankCode
        '
        Me.fndBankCode.Location = New System.Drawing.Point(110, 112)
        Me.fndBankCode.MendatroryField = True
        Me.fndBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBankCode.MyLinkLable1 = Me.lblBankCode
        Me.fndBankCode.MyLinkLable2 = Me.lblBankName
        Me.fndBankCode.MyReadOnly = False
        Me.fndBankCode.Name = "fndBankCode"
        Me.fndBankCode.Size = New System.Drawing.Size(154, 19)
        Me.fndBankCode.TabIndex = 3
        Me.fndBankCode.Value = ""
        '
        'lblBankCode
        '
        Me.lblBankCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankCode.Location = New System.Drawing.Point(12, 115)
        Me.lblBankCode.Name = "lblBankCode"
        Me.lblBankCode.Size = New System.Drawing.Size(62, 16)
        Me.lblBankCode.TabIndex = 4
        Me.lblBankCode.Text = "Bank Code"
        '
        'fndBranchCode
        '
        Me.fndBranchCode.Location = New System.Drawing.Point(115, 15)
        Me.fndBranchCode.MendatroryField = True
        Me.fndBranchCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndBranchCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndBranchCode.MyLinkLable1 = Me.lblCode
        Me.fndBranchCode.MyLinkLable2 = Nothing
        Me.fndBranchCode.MyMaxLength = 32767
        Me.fndBranchCode.MyReadOnly = False
        Me.fndBranchCode.Name = "fndBranchCode"
        Me.fndBranchCode.Size = New System.Drawing.Size(199, 21)
        Me.fndBranchCode.TabIndex = 0
        Me.fndBranchCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCode.Location = New System.Drawing.Point(13, 21)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(77, 16)
        Me.lblCode.TabIndex = 6
        Me.lblCode.Text = "Branch Code"
        '
        'lblBranchName
        '
        Me.lblBranchName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBranchName.Location = New System.Drawing.Point(13, 53)
        Me.lblBranchName.Name = "lblBranchName"
        Me.lblBranchName.Size = New System.Drawing.Size(75, 16)
        Me.lblBranchName.TabIndex = 5
        Me.lblBranchName.Text = "Branch Name"
        '
        'rbtnReset
        '
        Me.rbtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.rbtnReset.Location = New System.Drawing.Point(314, 17)
        Me.rbtnReset.Name = "rbtnReset"
        Me.rbtnReset.Size = New System.Drawing.Size(14, 18)
        Me.rbtnReset.TabIndex = 1
        '
        'txtBranchName
        '
        Me.txtBranchName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBranchName.Location = New System.Drawing.Point(113, 49)
        Me.txtBranchName.MaxLength = 50
        Me.txtBranchName.MendatroryField = False
        Me.txtBranchName.MyLinkLable1 = Me.lblBranchName
        Me.txtBranchName.MyLinkLable2 = Nothing
        Me.txtBranchName.Name = "txtBranchName"
        Me.txtBranchName.Size = New System.Drawing.Size(220, 18)
        Me.txtBranchName.TabIndex = 1
        '
        'rdmenuimport
        '
        Me.rdmenuimport.AccessibleDescription = "import"
        Me.rdmenuimport.AccessibleName = "import"
        Me.rdmenuimport.Name = "rdmenuimport"
        Me.rdmenuimport.Text = "Import"
        Me.rdmenuimport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenufile
        '
        Me.RadMenufile.AccessibleDescription = "File"
        Me.RadMenufile.AccessibleName = "File"
        Me.RadMenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenuimport, Me.rdmenuexport, Me.rdmenuexit})
        Me.RadMenufile.Name = "RadMenufile"
        Me.RadMenufile.Text = "File"
        Me.RadMenufile.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rdmenuexport
        '
        Me.rdmenuexport.AccessibleDescription = "Export"
        Me.rdmenuexport.AccessibleName = "Export"
        Me.rdmenuexport.Name = "rdmenuexport"
        Me.rdmenuexport.Text = "Export"
        Me.rdmenuexport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rdmenuexit
        '
        Me.rdmenuexit.AccessibleDescription = "Exit"
        Me.rdmenuexit.AccessibleName = "Exit"
        Me.rdmenuexit.Name = "rdmenuexit"
        Me.rdmenuexit.Text = "Exit"
        Me.rdmenuexit.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rdmenufile
        '
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenufile})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(757, 20)
        Me.rdmenufile.TabIndex = 1
        Me.rdmenufile.Text = "File"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdmenufile)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(757, 485)
        Me.SplitContainer1.SplitterDistance = 453
        Me.SplitContainer1.TabIndex = 4
        '
        'rbtnSave
        '
        Me.rbtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnSave.Location = New System.Drawing.Point(20, 7)
        Me.rbtnSave.Name = "rbtnSave"
        Me.rbtnSave.Size = New System.Drawing.Size(68, 18)
        Me.rbtnSave.TabIndex = 0
        Me.rbtnSave.Text = "Save"
        '
        'rbtnClose
        '
        Me.rbtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbtnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnClose.Location = New System.Drawing.Point(677, 7)
        Me.rbtnClose.Name = "rbtnClose"
        Me.rbtnClose.Size = New System.Drawing.Size(68, 18)
        Me.rbtnClose.TabIndex = 2
        Me.rbtnClose.Text = "Close"
        '
        'rbtnDelete
        '
        Me.rbtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnDelete.Location = New System.Drawing.Point(91, 7)
        Me.rbtnDelete.Name = "rbtnDelete"
        Me.rbtnDelete.Size = New System.Drawing.Size(68, 18)
        Me.rbtnDelete.TabIndex = 1
        Me.rbtnDelete.Text = "Delete"
        '
        'FrmBankBrachMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(757, 485)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmBankBrachMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmBankBrachMaster"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblIFSCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIFSCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBranchName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBranchName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.rbtnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblIFSCCode As common.Controls.MyLabel
    Friend WithEvents txtIFSCCode As common.Controls.MyTextBox
    Friend WithEvents lblBankName As common.Controls.MyLabel
    Friend WithEvents fndBankCode As common.UserControls.txtFinder
    Friend WithEvents lblBankCode As common.Controls.MyLabel
    Friend WithEvents fndBranchCode As common.UserControls.txtNavigator
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents lblBranchName As common.Controls.MyLabel
    Friend WithEvents rbtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtBranchName As common.Controls.MyTextBox
    Friend WithEvents rdmenuimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents rbtnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnDelete As Telerik.WinControls.UI.RadButton
End Class

