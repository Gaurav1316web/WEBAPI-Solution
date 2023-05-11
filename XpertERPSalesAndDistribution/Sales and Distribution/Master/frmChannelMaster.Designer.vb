Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChannelMaster
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
        Me.rdmenuimport = New Telerik.WinControls.UI.RadMenuItem
        Me.rdmenuexport = New Telerik.WinControls.UI.RadMenuItem
        Me.rdmenuexit = New Telerik.WinControls.UI.RadMenuItem
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.rdgpbxchannelmaster = New Telerik.WinControls.UI.RadGroupBox
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton
        Me.rdgpbxchannel = New Telerik.WinControls.UI.RadGroupBox
        Me.fndcategory = New common.UserControls.txtFinder
        Me.rdlblcategory = New common.Controls.MyLabel
        Me.fndID = New common.UserControls.txtNavigator
        Me.rdlblId = New common.Controls.MyLabel
        Me.rdbtnreset = New Telerik.WinControls.UI.RadButton
        Me.rdtxtname = New common.Controls.MyTextBox
        Me.rdlblname = New common.Controls.MyLabel
        Me.rdbtndelete = New Telerik.WinControls.UI.RadButton
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdgpbxchannelmaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgpbxchannelmaster.SuspendLayout()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdgpbxchannel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgpbxchannel.SuspendLayout()
        CType(Me.rdlblcategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdmenufile
        '
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenufile})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(443, 20)
        Me.rdmenufile.TabIndex = 0
        Me.rdmenufile.Text = "File"
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
        'rdmenuimport
        '
        Me.rdmenuimport.AccessibleDescription = "Import"
        Me.rdmenuimport.AccessibleName = "Import"
        Me.rdmenuimport.Name = "rdmenuimport"
        Me.rdmenuimport.Text = "Import"
        Me.rdmenuimport.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        'rdgpbxchannelmaster
        '
        Me.rdgpbxchannelmaster.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgpbxchannelmaster.Controls.Add(Me.rdgpbxchannel)
        Me.rdgpbxchannelmaster.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgpbxchannelmaster.FooterImageIndex = -1
        Me.rdgpbxchannelmaster.FooterImageKey = ""
        Me.rdgpbxchannelmaster.HeaderImageIndex = -1
        Me.rdgpbxchannelmaster.HeaderImageKey = ""
        Me.rdgpbxchannelmaster.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.rdgpbxchannelmaster.HeaderText = ""
        Me.rdgpbxchannelmaster.Location = New System.Drawing.Point(13, 16)
        Me.rdgpbxchannelmaster.Name = "rdgpbxchannelmaster"
        Me.rdgpbxchannelmaster.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.rdgpbxchannelmaster.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgpbxchannelmaster.Size = New System.Drawing.Size(417, 140)
        Me.rdgpbxchannelmaster.TabIndex = 0
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnsave.Location = New System.Drawing.Point(13, 8)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(66, 18)
        Me.rdbtnsave.TabIndex = 1
        Me.rdbtnsave.Text = "Save"
        '
        'rdgpbxchannel
        '
        Me.rdgpbxchannel.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgpbxchannel.Controls.Add(Me.fndcategory)
        Me.rdgpbxchannel.Controls.Add(Me.fndID)
        Me.rdgpbxchannel.Controls.Add(Me.rdbtnreset)
        Me.rdgpbxchannel.Controls.Add(Me.rdtxtname)
        Me.rdgpbxchannel.Controls.Add(Me.rdlblname)
        Me.rdgpbxchannel.Controls.Add(Me.rdlblcategory)
        Me.rdgpbxchannel.Controls.Add(Me.rdlblId)
        Me.rdgpbxchannel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgpbxchannel.FooterImageIndex = -1
        Me.rdgpbxchannel.FooterImageKey = ""
        Me.rdgpbxchannel.HeaderImageIndex = -1
        Me.rdgpbxchannel.HeaderImageKey = ""
        Me.rdgpbxchannel.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.rdgpbxchannel.HeaderText = ""
        Me.rdgpbxchannel.Location = New System.Drawing.Point(11, 15)
        Me.rdgpbxchannel.Name = "rdgpbxchannel"
        Me.rdgpbxchannel.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.rdgpbxchannel.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgpbxchannel.Size = New System.Drawing.Size(391, 110)
        Me.rdgpbxchannel.TabIndex = 0
        '
        'fndcategory
        '
        Me.fndcategory.Location = New System.Drawing.Point(114, 46)
        Me.fndcategory.MendatroryField = False
        Me.fndcategory.MyLinkLable1 = Me.rdlblcategory
        Me.fndcategory.MyLinkLable2 = Nothing
        Me.fndcategory.MyReadOnly = False
        Me.fndcategory.Name = "fndcategory"
        Me.fndcategory.Size = New System.Drawing.Size(201, 19)
        Me.fndcategory.TabIndex = 2
        Me.fndcategory.Value = ""
        '
        'rdlblcategory
        '
        Me.rdlblcategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlblcategory.Location = New System.Drawing.Point(13, 45)
        Me.rdlblcategory.Name = "rdlblcategory"
        Me.rdlblcategory.Size = New System.Drawing.Size(97, 16)
        Me.rdlblcategory.TabIndex = 1
        Me.rdlblcategory.Text = "Channel Category"
        '
        'fndID
        '
        Me.fndID.Location = New System.Drawing.Point(114, 19)
        Me.fndID.MendatroryField = True
        Me.fndID.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndID.MyLinkLable1 = Me.rdlblId
        Me.fndID.MyLinkLable2 = Nothing
        Me.fndID.MyMaxLength = 32767
        Me.fndID.MyReadOnly = False
        Me.fndID.Name = "fndID"
        Me.fndID.Size = New System.Drawing.Size(202, 21)
        Me.fndID.TabIndex = 0
        Me.fndID.Value = ""
        '
        'rdlblId
        '
        Me.rdlblId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.rdlblId.ForeColor = System.Drawing.Color.Black
        Me.rdlblId.Location = New System.Drawing.Point(13, 20)
        Me.rdlblId.Name = "rdlblId"
        '
        '
        '
        Me.rdlblId.RootElement.ForeColor = System.Drawing.Color.Black
        Me.rdlblId.Size = New System.Drawing.Size(64, 16)
        Me.rdlblId.TabIndex = 0
        Me.rdlblId.Text = "Channel Id"
        '
        'rdbtnreset
        '
        Me.rdbtnreset.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.rdbtnreset.Location = New System.Drawing.Point(318, 20)
        Me.rdbtnreset.Name = "rdbtnreset"
        Me.rdbtnreset.Size = New System.Drawing.Size(16, 18)
        Me.rdbtnreset.TabIndex = 1
        '
        'rdtxtname
        '
        Me.rdtxtname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdtxtname.Location = New System.Drawing.Point(114, 71)
        Me.rdtxtname.MaxLength = 50
        Me.rdtxtname.MendatroryField = False
        Me.rdtxtname.MyLinkLable1 = Me.rdlblname
        Me.rdtxtname.MyLinkLable2 = Nothing
        Me.rdtxtname.Name = "rdtxtname"
        Me.rdtxtname.Size = New System.Drawing.Size(235, 18)
        Me.rdtxtname.TabIndex = 3
        '
        'rdlblname
        '
        Me.rdlblname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlblname.Location = New System.Drawing.Point(13, 71)
        Me.rdlblname.Name = "rdlblname"
        Me.rdlblname.Size = New System.Drawing.Size(82, 16)
        Me.rdlblname.TabIndex = 1
        Me.rdlblname.Text = "Channel Name"
        '
        'rdbtndelete
        '
        Me.rdbtndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtndelete.Location = New System.Drawing.Point(85, 8)
        Me.rdbtndelete.Name = "rdbtndelete"
        Me.rdbtndelete.Size = New System.Drawing.Size(66, 18)
        Me.rdbtndelete.TabIndex = 2
        Me.rdbtndelete.Text = "Delete"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnclose.Location = New System.Drawing.Point(364, 8)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(66, 18)
        Me.rdbtnclose.TabIndex = 3
        Me.rdbtnclose.Text = "Close"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdgpbxchannelmaster)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(443, 200)
        Me.SplitContainer1.SplitterDistance = 158
        Me.SplitContainer1.TabIndex = 1
        '
        'frmChannelMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(443, 220)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.KeyPreview = True
        Me.Name = "frmChannelMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.RootElement.ForeColor = System.Drawing.Color.White
        Me.Text = "Channel Master"
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdgpbxchannelmaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgpbxchannelmaster.ResumeLayout(False)
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdgpbxchannel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgpbxchannel.ResumeLayout(False)
        Me.rdgpbxchannel.PerformLayout()
        CType(Me.rdlblcategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents rdgpbxchannelmaster As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdgpbxchannel As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbtnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdtxtname As common.Controls.MyTextBox
    Friend WithEvents rdbtndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdlblname As common.Controls.MyLabel
    Friend WithEvents rdlblcategory As common.Controls.MyLabel
    Friend WithEvents rdlblId As common.Controls.MyLabel
    Friend WithEvents fndID As common.UserControls.txtNavigator
    Friend WithEvents fndcategory As common.UserControls.txtFinder
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

