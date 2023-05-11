<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDesignationMaster
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
        Me.components = New System.ComponentModel.Container()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.desimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.desexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.desclose = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.lbldes = New common.Controls.MyLabel()
        Me.txtdes = New common.Controls.MyTextBox()
        Me.lbldesid = New common.Controls.MyLabel()
        Me.ToolTipdesig = New System.Windows.Forms.ToolTip(Me.components)
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.gbdesignation = New Telerik.WinControls.UI.RadGroupBox()
        Me.fnddesig = New common.UserControls.txtNavigator()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.CmbLevelCode = New common.Controls.MyComboBox()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldesid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbdesignation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbdesignation.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbLevelCode, System.ComponentModel.ISupportInitialize).BeginInit()
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
        '
        'desimport
        '
        Me.desimport.AccessibleDescription = "Import"
        Me.desimport.AccessibleName = "Import"
        Me.desimport.Name = "desimport"
        Me.desimport.Text = "Import"
        '
        'desexport
        '
        Me.desexport.AccessibleDescription = "Export"
        Me.desexport.AccessibleName = "Export"
        Me.desexport.Name = "desexport"
        Me.desexport.Text = "Export"
        '
        'desclose
        '
        Me.desclose.AccessibleDescription = "Close"
        Me.desclose.AccessibleName = "Close"
        Me.desclose.Name = "desclose"
        Me.desclose.Text = "Close"
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
        'lbldes
        '
        Me.lbldes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldes.Location = New System.Drawing.Point(7, 37)
        Me.lbldes.Name = "lbldes"
        Me.lbldes.Size = New System.Drawing.Size(63, 16)
        Me.lbldes.TabIndex = 3
        Me.lbldes.Text = "Description"
        '
        'txtdes
        '
        Me.txtdes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdes.Location = New System.Drawing.Point(116, 37)
        Me.txtdes.MaxLength = 49
        Me.txtdes.MendatroryField = True
        Me.txtdes.MyLinkLable1 = Nothing
        Me.txtdes.MyLinkLable2 = Nothing
        Me.txtdes.Name = "txtdes"
        '
        '
        '
        Me.txtdes.RootElement.StretchVertically = True
        Me.txtdes.Size = New System.Drawing.Size(290, 20)
        Me.txtdes.TabIndex = 2
        '
        'lbldesid
        '
        Me.lbldesid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldesid.Location = New System.Drawing.Point(7, 13)
        Me.lbldesid.Name = "lbldesid"
        Me.lbldesid.Size = New System.Drawing.Size(99, 16)
        Me.lbldesid.TabIndex = 4
        Me.lbldesid.Text = "Designation  Code"
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
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(317, 11)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 21)
        Me.btnnew.TabIndex = 1
        '
        'gbdesignation
        '
        Me.gbdesignation.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbdesignation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.gbdesignation.Controls.Add(Me.MyLabel3)
        Me.gbdesignation.Controls.Add(Me.CmbLevelCode)
        Me.gbdesignation.Controls.Add(Me.fnddesig)
        Me.gbdesignation.Controls.Add(Me.lbldesid)
        Me.gbdesignation.Controls.Add(Me.txtdes)
        Me.gbdesignation.Controls.Add(Me.lbldes)
        Me.gbdesignation.Controls.Add(Me.btnnew)
        Me.gbdesignation.HeaderPosition = Telerik.WinControls.UI.HeaderPosition.Left
        Me.gbdesignation.HeaderText = ""
        Me.gbdesignation.Location = New System.Drawing.Point(3, 3)
        Me.gbdesignation.Name = "gbdesignation"
        Me.gbdesignation.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbdesignation.Size = New System.Drawing.Size(449, 92)
        Me.gbdesignation.TabIndex = 0
        '
        'fnddesig
        '
        Me.fnddesig.Location = New System.Drawing.Point(116, 11)
        Me.fnddesig.MendatroryField = True
        Me.fnddesig.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fnddesig.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fnddesig.MyLinkLable1 = Nothing
        Me.fnddesig.MyLinkLable2 = Nothing
        Me.fnddesig.MyMaxLength = 32767
        Me.fnddesig.MyReadOnly = False
        Me.fnddesig.Name = "fnddesig"
        Me.fnddesig.Size = New System.Drawing.Size(202, 21)
        Me.fnddesig.TabIndex = 0
        Me.fnddesig.Value = ""
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
        Me.SplitContainer1.Size = New System.Drawing.Size(455, 129)
        Me.SplitContainer1.SplitterDistance = 100
        Me.SplitContainer1.TabIndex = 0
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(7, 65)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel3.TabIndex = 42
        Me.MyLabel3.Text = "Level Code"
        '
        'CmbLevelCode
        '
        Me.CmbLevelCode.AutoCompleteDisplayMember = Nothing
        Me.CmbLevelCode.AutoCompleteValueMember = Nothing
        Me.CmbLevelCode.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CmbLevelCode.Location = New System.Drawing.Point(116, 63)
        Me.CmbLevelCode.MendatroryField = True
        Me.CmbLevelCode.MyLinkLable1 = Me.MyLabel3
        Me.CmbLevelCode.MyLinkLable2 = Nothing
        Me.CmbLevelCode.Name = "CmbLevelCode"
        Me.CmbLevelCode.Size = New System.Drawing.Size(167, 20)
        Me.CmbLevelCode.TabIndex = 41
        '
        'frmDesignationMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(455, 149)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmDesignationMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Designation Master"
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldesid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbdesignation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbdesignation.ResumeLayout(False)
        Me.gbdesignation.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbLevelCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtdes As common.Controls.MyTextBox
    Friend WithEvents ToolTipdesig As System.Windows.Forms.ToolTip
    Friend WithEvents desimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents desexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents desclose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents gbdesignation As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lbldes As common.Controls.MyLabel
    Friend WithEvents lbldesid As common.Controls.MyLabel
    Friend WithEvents fnddesig As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents CmbLevelCode As common.Controls.MyComboBox
End Class

