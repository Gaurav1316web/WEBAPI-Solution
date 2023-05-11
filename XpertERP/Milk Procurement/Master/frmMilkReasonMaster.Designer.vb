<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMilkReasonMaster
    Inherits FrmMainTranScreen 'Telerik.WinControls.UI.RadForm

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
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.lbl_Name = New Telerik.WinControls.UI.RadLabel
        Me.lbl_Code = New Telerik.WinControls.UI.RadLabel
        Me.txt_Code = New common.UserControls.txtNavigator
        Me.BtnClose = New Telerik.WinControls.UI.RadButton
        Me.BtnDelete = New Telerik.WinControls.UI.RadButton
        Me.Btnsave = New Telerik.WinControls.UI.RadButton
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RMFile = New Telerik.WinControls.UI.RadMenuItem
        Me.RMImport = New Telerik.WinControls.UI.RadMenuItem
        Me.RMExport = New Telerik.WinControls.UI.RadMenuItem
        Me.txt_Name = New common.Controls.MyTextBox
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_Name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_Code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.txt_Name)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbl_Name)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbl_Code)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txt_Code)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(563, 282)
        Me.SplitContainer1.SplitterDistance = 247
        Me.SplitContainer1.TabIndex = 0
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(322, 10)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 1
        '
        'lbl_Name
        '
        Me.lbl_Name.Location = New System.Drawing.Point(9, 35)
        Me.lbl_Name.Name = "lbl_Name"
        Me.lbl_Name.Size = New System.Drawing.Size(36, 18)
        Me.lbl_Name.TabIndex = 20
        Me.lbl_Name.Text = "Name"
        '
        'lbl_Code
        '
        Me.lbl_Code.Location = New System.Drawing.Point(9, 10)
        Me.lbl_Code.Name = "lbl_Code"
        Me.lbl_Code.Size = New System.Drawing.Size(32, 18)
        Me.lbl_Code.TabIndex = 19
        Me.lbl_Code.Text = "Code"
        '
        'txt_Code
        '
        Me.txt_Code.Location = New System.Drawing.Point(47, 10)
        Me.txt_Code.MendatroryField = True
        Me.txt_Code.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txt_Code.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txt_Code.MyLinkLable1 = Nothing
        Me.txt_Code.MyLinkLable2 = Nothing
        Me.txt_Code.MyMaxLength = 32767
        Me.txt_Code.MyReadOnly = False
        Me.txt_Code.Name = "txt_Code"
        Me.txt_Code.Size = New System.Drawing.Size(275, 20)
        Me.txt_Code.TabIndex = 1
        Me.txt_Code.Value = ""
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.Location = New System.Drawing.Point(479, 5)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(73, 21)
        Me.BtnClose.TabIndex = 2
        Me.BtnClose.Text = "Close"
        '
        'BtnDelete
        '
        Me.BtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnDelete.Location = New System.Drawing.Point(87, 5)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(67, 20)
        Me.BtnDelete.TabIndex = 1
        Me.BtnDelete.Text = "Delete"
        '
        'Btnsave
        '
        Me.Btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Btnsave.Location = New System.Drawing.Point(13, 5)
        Me.Btnsave.Name = "Btnsave"
        Me.Btnsave.Size = New System.Drawing.Size(68, 21)
        Me.Btnsave.TabIndex = 0
        Me.Btnsave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RMFile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(563, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RMFile
        '
        Me.RMFile.AccessibleDescription = "File"
        Me.RMFile.AccessibleName = "File"
        Me.RMFile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RMImport, Me.RMExport})
        Me.RMFile.Name = "RMFile"
        Me.RMFile.Text = "File"
        Me.RMFile.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        'txt_Name
        '
        Me.txt_Name.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Name.Location = New System.Drawing.Point(47, 35)
        Me.txt_Name.MaxLength = 150
        Me.txt_Name.MendatroryField = True
        Me.txt_Name.MyLinkLable1 = Nothing
        Me.txt_Name.MyLinkLable2 = Nothing
        Me.txt_Name.Name = "txt_Name"
        Me.txt_Name.Size = New System.Drawing.Size(275, 18)
        Me.txt_Name.TabIndex = 4
        '
        'frmTrainingMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(563, 302)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmTrainingMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Training Master"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_Name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_Code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer

    Friend WithEvents Btnsave As Telerik.WinControls.UI.RadButton

    Friend WithEvents BtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtdescription As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents lbl_Code As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txt_Code As common.UserControls.txtNavigator
    Friend WithEvents lbl_Name As Telerik.WinControls.UI.RadLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RMFile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txt_Name As common.Controls.MyTextBox
End Class

