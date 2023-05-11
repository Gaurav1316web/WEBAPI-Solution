Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmItemChargeFranchiseMappingMaster
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
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu
        Me.RadMenufile = New Telerik.WinControls.UI.RadMenuItem
        Me.rdexport = New Telerik.WinControls.UI.RadMenuItem
        Me.rdimport = New Telerik.WinControls.UI.RadMenuItem
        Me.rdexit = New Telerik.WinControls.UI.RadMenuItem
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.lblchrcat = New common.Controls.MyLabel
        Me.lblchrdesc = New common.Controls.MyLabel
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.txtchrcode = New common.UserControls.txtFinder
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblchrcat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblchrdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdmenufile
        '
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenufile})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(619, 20)
        Me.rdmenufile.TabIndex = 0
        Me.rdmenufile.Text = "File"
        '
        'RadMenufile
        '
        Me.RadMenufile.AccessibleDescription = "File"
        Me.RadMenufile.AccessibleName = "File"
        Me.RadMenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdexport, Me.rdimport, Me.rdexit})
        Me.RadMenufile.Name = "RadMenufile"
        Me.RadMenufile.Text = "File"
        Me.RadMenufile.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rdexport
        '
        Me.rdexport.AccessibleDescription = "Export"
        Me.rdexport.AccessibleName = "Export"
        Me.rdexport.Name = "rdexport"
        Me.rdexport.Text = "Export"
        Me.rdexport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rdimport
        '
        Me.rdimport.AccessibleDescription = "Import"
        Me.rdimport.AccessibleName = "Import"
        Me.rdimport.Name = "rdimport"
        Me.rdimport.Text = "Import"
        Me.rdimport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rdexit
        '
        Me.rdexit.AccessibleDescription = "Exit"
        Me.rdexit.AccessibleName = "Exit"
        Me.rdexit.Name = "rdexit"
        Me.rdexit.Text = "Exit"
        Me.rdexit.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(79, 2)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(70, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(1, 2)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(70, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.SplitContainer2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(619, 369)
        Me.Panel1.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer2.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer2.Size = New System.Drawing.Size(613, 363)
        Me.SplitContainer2.SplitterDistance = 334
        Me.SplitContainer2.TabIndex = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.lblchrcat)
        Me.RadGroupBox1.Controls.Add(Me.lblchrdesc)
        Me.RadGroupBox1.Controls.Add(Me.gv1)
        Me.RadGroupBox1.Controls.Add(Me.txtchrcode)
        Me.RadGroupBox1.Controls.Add(Me.btnnew)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(3)
        Me.RadGroupBox1.Size = New System.Drawing.Size(594, 297)
        Me.RadGroupBox1.TabIndex = 0
        '
        'lblchrcat
        '
        Me.lblchrcat.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblchrcat.Location = New System.Drawing.Point(12, 15)
        Me.lblchrcat.Name = "lblchrcat"
        Me.lblchrcat.Size = New System.Drawing.Size(92, 16)
        Me.lblchrcat.TabIndex = 2
        Me.lblchrcat.Text = "Charge Category"
        '
        'lblchrdesc
        '
        Me.lblchrdesc.AutoSize = False
        Me.lblchrdesc.BorderVisible = True
        Me.lblchrdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblchrdesc.Location = New System.Drawing.Point(276, 14)
        Me.lblchrdesc.Name = "lblchrdesc"
        Me.lblchrdesc.Size = New System.Drawing.Size(287, 18)
        Me.lblchrdesc.TabIndex = 3
        Me.lblchrdesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblchrdesc.TextWrap = False
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(12, 43)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.Size = New System.Drawing.Size(572, 244)
        Me.gv1.TabIndex = 4
        Me.gv1.Text = "RadGridView1"
        '
        'txtchrcode
        '
        Me.txtchrcode.Location = New System.Drawing.Point(116, 13)
        Me.txtchrcode.MendatroryField = True
        Me.txtchrcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtchrcode.MyLinkLable1 = Me.lblchrcat
        Me.txtchrcode.MyLinkLable2 = Me.lblchrdesc
        Me.txtchrcode.MyReadOnly = True
        Me.txtchrcode.Name = "txtchrcode"
        Me.txtchrcode.Size = New System.Drawing.Size(154, 19)
        Me.txtchrcode.TabIndex = 0
        Me.txtchrcode.Value = ""
        '
        'btnnew
        '
        Me.btnnew.Image = My.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(569, 12)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 1
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(530, 2)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(70, 20)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'FrmItemChargeFranchiseMappingMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(619, 389)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.rdmenufile)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "FrmItemChargeFranchiseMappingMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Item Charge Franchise Mapping Master"
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblchrcat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblchrdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblchrdesc As common.Controls.MyLabel
    Friend WithEvents txtchrcode As common.UserControls.txtFinder
    Friend WithEvents lblchrcat As common.Controls.MyLabel
    Friend WithEvents rdexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdexit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
End Class

