<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSubLocationMaster
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
        Me.components = New System.ComponentModel.Container
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.desimport = New Telerik.WinControls.UI.RadMenuItem
        Me.desexport = New Telerik.WinControls.UI.RadMenuItem
        Me.desclose = New Telerik.WinControls.UI.RadMenuItem
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.lblLoc = New common.Controls.MyLabel
        Me.txtSubLoc = New common.Controls.MyTextBox
        Me.lblSubLocid = New common.Controls.MyLabel
        Me.ToolTipdesig = New System.Windows.Forms.ToolTip(Me.components)
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.btnAddnew = New Telerik.WinControls.UI.RadButton
        Me.gbdesignation = New Telerik.WinControls.UI.RadGroupBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtLocation = New common.UserControls.txtFinder
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.fndSubLocid = New common.UserControls.txtNavigator
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubLoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubLocid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbdesignation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbdesignation.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.desimport, Me.desexport, Me.desclose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'desimport
        '
        Me.desimport.AccessibleDescription = "Import"
        Me.desimport.AccessibleName = "Import"
        Me.desimport.Name = "desimport"
        Me.desimport.Text = "Import"
        Me.desimport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'desexport
        '
        Me.desexport.AccessibleDescription = "Export"
        Me.desexport.AccessibleName = "Export"
        Me.desexport.Name = "desexport"
        Me.desexport.Text = "Export"
        Me.desexport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'desclose
        '
        Me.desclose.AccessibleDescription = "Close"
        Me.desclose.AccessibleName = "Close"
        Me.desclose.Name = "desclose"
        Me.desclose.Text = "Close"
        Me.desclose.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(378, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(96, 3)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(17, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'lblLoc
        '
        Me.lblLoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoc.Location = New System.Drawing.Point(241, 72)
        Me.lblLoc.Name = "lblLoc"
        Me.lblLoc.Size = New System.Drawing.Size(82, 16)
        Me.lblLoc.TabIndex = 3
        Me.lblLoc.Text = "Location Name"
        Me.lblLoc.Visible = False
        '
        'txtSubLoc
        '
        Me.txtSubLoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubLoc.Location = New System.Drawing.Point(127, 41)
        Me.txtSubLoc.MaxLength = 49
        Me.txtSubLoc.MendatroryField = False
        Me.txtSubLoc.MyLinkLable1 = Nothing
        Me.txtSubLoc.MyLinkLable2 = Nothing
        Me.txtSubLoc.Name = "txtSubLoc"
        '
        '
        '
        Me.txtSubLoc.RootElement.StretchVertically = True
        Me.txtSubLoc.Size = New System.Drawing.Size(292, 20)
        Me.txtSubLoc.TabIndex = 2
        '
        'lblSubLocid
        '
        Me.lblSubLocid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubLocid.Location = New System.Drawing.Point(13, 13)
        Me.lblSubLocid.Name = "lblSubLocid"
        Me.lblSubLocid.Size = New System.Drawing.Size(103, 16)
        Me.lblSubLocid.TabIndex = 4
        Me.lblSubLocid.Text = "Sub Location Code"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(455, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'btnAddnew
        '
        Me.btnAddnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddnew.Location = New System.Drawing.Point(404, 12)
        Me.btnAddnew.Name = "btnAddnew"
        Me.btnAddnew.Size = New System.Drawing.Size(15, 21)
        Me.btnAddnew.TabIndex = 1
        '
        'gbdesignation
        '
        Me.gbdesignation.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbdesignation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.gbdesignation.Controls.Add(Me.MyLabel2)
        Me.gbdesignation.Controls.Add(Me.txtLocation)
        Me.gbdesignation.Controls.Add(Me.MyLabel1)
        Me.gbdesignation.Controls.Add(Me.fndSubLocid)
        Me.gbdesignation.Controls.Add(Me.lblSubLocid)
        Me.gbdesignation.Controls.Add(Me.txtSubLoc)
        Me.gbdesignation.Controls.Add(Me.lblLoc)
        Me.gbdesignation.Controls.Add(Me.btnAddnew)
        Me.gbdesignation.HeaderPosition = Telerik.WinControls.UI.HeaderPosition.Left
        Me.gbdesignation.HeaderText = ""
        Me.gbdesignation.Location = New System.Drawing.Point(7, 7)
        Me.gbdesignation.Name = "gbdesignation"
        Me.gbdesignation.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbdesignation.Size = New System.Drawing.Size(441, 99)
        Me.gbdesignation.TabIndex = 0
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(13, 43)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(106, 16)
        Me.MyLabel2.TabIndex = 5
        Me.MyLabel2.Text = "Sub Location Name"
        '
        'txtLocation
        '
        Me.txtLocation.Location = New System.Drawing.Point(127, 70)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Nothing
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(108, 20)
        Me.txtLocation.TabIndex = 5
        Me.txtLocation.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(13, 72)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel1.TabIndex = 4
        Me.MyLabel1.Text = "Location Name"
        '
        'fndSubLocid
        '
        Me.fndSubLocid.Location = New System.Drawing.Point(127, 11)
        Me.fndSubLocid.MendatroryField = False
        Me.fndSubLocid.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndSubLocid.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndSubLocid.MyLinkLable1 = Nothing
        Me.fndSubLocid.MyLinkLable2 = Nothing
        Me.fndSubLocid.MyMaxLength = 32767
        Me.fndSubLocid.MyReadOnly = False
        Me.fndSubLocid.Name = "fndSubLocid"
        Me.fndSubLocid.Size = New System.Drawing.Size(278, 21)
        Me.fndSubLocid.TabIndex = 0
        Me.fndSubLocid.Value = ""
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.gbdesignation)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(455, 157)
        Me.SplitContainer1.SplitterDistance = 128
        Me.SplitContainer1.TabIndex = 0
        '
        'frmSubLocationMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(455, 177)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmSubLocationMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Sub Location Master"
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubLoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubLocid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbdesignation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbdesignation.ResumeLayout(False)
        Me.gbdesignation.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnAddnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtSubLoc As common.Controls.MyTextBox
    Friend WithEvents ToolTipdesig As System.Windows.Forms.ToolTip
    Friend WithEvents desimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents desexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents desclose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents gbdesignation As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblLoc As common.Controls.MyLabel
    Friend WithEvents lblSubLocid As common.Controls.MyLabel
    Friend WithEvents fndSubLocid As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
End Class

