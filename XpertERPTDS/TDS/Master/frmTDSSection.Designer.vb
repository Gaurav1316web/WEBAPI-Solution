Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTDSSection
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
        Me.ToolTipTDSSection = New System.Windows.Forms.ToolTip(Me.components)
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuclose = New Telerik.WinControls.UI.RadMenuItem()
        Me.gbTDSSection = New Telerik.WinControls.UI.RadGroupBox()
        Me.fndTdsSectionNew = New common.UserControls.txtNavigator()
        Me.lblcode = New common.Controls.MyLabel()
        Me.chkintax = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtreport = New common.Controls.MyTextBox()
        Me.lblreport = New common.Controls.MyLabel()
        Me.chkcommulative = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtdes = New common.Controls.MyTextBox()
        Me.lbldes = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbTDSSection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTDSSection.SuspendLayout()
        CType(Me.lblcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkintax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtreport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblreport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkcommulative, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadMenu1.Size = New System.Drawing.Size(577, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.menuImport, Me.Export, Me.menuclose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'menuImport
        '
        Me.menuImport.AccessibleDescription = "RadMenuItem2"
        Me.menuImport.AccessibleName = "RadMenuItem2"
        Me.menuImport.Name = "menuImport"
        Me.menuImport.Text = "Import"
        '
        'Export
        '
        Me.Export.AccessibleDescription = "Export"
        Me.Export.AccessibleName = "Export"
        Me.Export.Name = "Export"
        Me.Export.Text = "Export"
        '
        'menuclose
        '
        Me.menuclose.AccessibleDescription = "Close"
        Me.menuclose.AccessibleName = "Close"
        Me.menuclose.Name = "menuclose"
        Me.menuclose.Text = "Close"
        '
        'gbTDSSection
        '
        Me.gbTDSSection.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbTDSSection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.gbTDSSection.Controls.Add(Me.fndTdsSectionNew)
        Me.gbTDSSection.Controls.Add(Me.chkintax)
        Me.gbTDSSection.Controls.Add(Me.txtreport)
        Me.gbTDSSection.Controls.Add(Me.chkcommulative)
        Me.gbTDSSection.Controls.Add(Me.lblreport)
        Me.gbTDSSection.Controls.Add(Me.lblcode)
        Me.gbTDSSection.Controls.Add(Me.txtdes)
        Me.gbTDSSection.Controls.Add(Me.lbldes)
        Me.gbTDSSection.Controls.Add(Me.btnnew)
        Me.gbTDSSection.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbTDSSection.HeaderPosition = Telerik.WinControls.UI.HeaderPosition.Left
        Me.gbTDSSection.HeaderText = ""
        Me.gbTDSSection.Location = New System.Drawing.Point(22, 12)
        Me.gbTDSSection.Name = "gbTDSSection"
        Me.gbTDSSection.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbTDSSection.Size = New System.Drawing.Size(529, 173)
        Me.gbTDSSection.TabIndex = 1
        '
        'fndTdsSectionNew
        '
        Me.fndTdsSectionNew.AccessibleName = "fndTdsSectionNew"
        Me.fndTdsSectionNew.FieldName = Nothing
        Me.fndTdsSectionNew.Location = New System.Drawing.Point(120, 11)
        Me.fndTdsSectionNew.MendatroryField = False
        Me.fndTdsSectionNew.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndTdsSectionNew.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndTdsSectionNew.MyLinkLable1 = Me.lblcode
        Me.fndTdsSectionNew.MyLinkLable2 = Nothing
        Me.fndTdsSectionNew.MyMaxLength = 32767
        Me.fndTdsSectionNew.MyReadOnly = False
        Me.fndTdsSectionNew.Name = "fndTdsSectionNew"
        Me.fndTdsSectionNew.Size = New System.Drawing.Size(264, 21)
        Me.fndTdsSectionNew.TabIndex = 0
        Me.fndTdsSectionNew.TabStop = False
        Me.fndTdsSectionNew.Value = ""
        '
        'lblcode
        '
        Me.lblcode.FieldName = Nothing
        Me.lblcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcode.Location = New System.Drawing.Point(13, 13)
        Me.lblcode.Name = "lblcode"
        Me.lblcode.Size = New System.Drawing.Size(63, 16)
        Me.lblcode.TabIndex = 37
        Me.lblcode.Text = "TDS Group"
        '
        'chkintax
        '
        Me.chkintax.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkintax.Location = New System.Drawing.Point(120, 112)
        Me.chkintax.Name = "chkintax"
        Me.chkintax.Size = New System.Drawing.Size(79, 16)
        Me.chkintax.TabIndex = 5
        Me.chkintax.Text = "Include Tax"
        '
        'txtreport
        '
        Me.txtreport.CalculationExpression = Nothing
        Me.txtreport.FieldCode = Nothing
        Me.txtreport.FieldDesc = Nothing
        Me.txtreport.FieldMaxLength = 0
        Me.txtreport.FieldName = Nothing
        Me.txtreport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtreport.isCalculatedField = False
        Me.txtreport.IsSourceFromTable = False
        Me.txtreport.IsSourceFromValueList = False
        Me.txtreport.IsUnique = False
        Me.txtreport.Location = New System.Drawing.Point(120, 64)
        Me.txtreport.MaxLength = 100
        Me.txtreport.MendatroryField = False
        Me.txtreport.MyLinkLable1 = Me.lblreport
        Me.txtreport.MyLinkLable2 = Nothing
        Me.txtreport.Name = "txtreport"
        Me.txtreport.ReferenceFieldDesc = Nothing
        Me.txtreport.ReferenceFieldName = Nothing
        Me.txtreport.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtreport.RootElement.StretchVertically = True
        Me.txtreport.Size = New System.Drawing.Size(393, 20)
        Me.txtreport.TabIndex = 3
        '
        'lblreport
        '
        Me.lblreport.FieldName = Nothing
        Me.lblreport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblreport.Location = New System.Drawing.Point(13, 64)
        Me.lblreport.Name = "lblreport"
        Me.lblreport.Size = New System.Drawing.Size(81, 16)
        Me.lblreport.TabIndex = 38
        Me.lblreport.Text = "Report Section"
        '
        'chkcommulative
        '
        Me.chkcommulative.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkcommulative.Location = New System.Drawing.Point(120, 90)
        Me.chkcommulative.Name = "chkcommulative"
        Me.chkcommulative.Size = New System.Drawing.Size(120, 16)
        Me.chkcommulative.TabIndex = 4
        Me.chkcommulative.Text = "Cummulative Cutoff"
        '
        'txtdes
        '
        Me.txtdes.CalculationExpression = Nothing
        Me.txtdes.FieldCode = Nothing
        Me.txtdes.FieldDesc = Nothing
        Me.txtdes.FieldMaxLength = 0
        Me.txtdes.FieldName = Nothing
        Me.txtdes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdes.isCalculatedField = False
        Me.txtdes.IsSourceFromTable = False
        Me.txtdes.IsSourceFromValueList = False
        Me.txtdes.IsUnique = False
        Me.txtdes.Location = New System.Drawing.Point(120, 38)
        Me.txtdes.MaxLength = 100
        Me.txtdes.MendatroryField = False
        Me.txtdes.MyLinkLable1 = Me.lbldes
        Me.txtdes.MyLinkLable2 = Nothing
        Me.txtdes.Name = "txtdes"
        Me.txtdes.ReferenceFieldDesc = Nothing
        Me.txtdes.ReferenceFieldName = Nothing
        Me.txtdes.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtdes.RootElement.StretchVertically = True
        Me.txtdes.Size = New System.Drawing.Size(393, 20)
        Me.txtdes.TabIndex = 2
        '
        'lbldes
        '
        Me.lbldes.FieldName = Nothing
        Me.lbldes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldes.Location = New System.Drawing.Point(13, 38)
        Me.lbldes.Name = "lbldes"
        Me.lbldes.Size = New System.Drawing.Size(63, 16)
        Me.lbldes.TabIndex = 36
        Me.lbldes.Text = "Description"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPTDS.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(390, 11)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 21)
        Me.btnnew.TabIndex = 1
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(472, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 8
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(101, 9)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 7
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(32, 9)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 6
        Me.btnsave.Text = "Save"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.gbTDSSection)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(577, 235)
        Me.SplitContainer1.SplitterDistance = 192
        Me.SplitContainer1.TabIndex = 2
        '
        'frmTDSSection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(577, 255)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmTDSSection"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "TDS Section"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbTDSSection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTDSSection.ResumeLayout(False)
        Me.gbTDSSection.PerformLayout()
        CType(Me.lblcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkintax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtreport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblreport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkcommulative, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTipTDSSection As System.Windows.Forms.ToolTip
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents gbTDSSection As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkintax As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtreport As common.Controls.MyTextBox
    Friend WithEvents chkcommulative As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtdes As common.Controls.MyTextBox
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents menuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuclose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblreport As common.Controls.MyLabel
    Friend WithEvents lblcode As common.Controls.MyLabel
    Friend WithEvents lbldes As common.Controls.MyLabel
    Friend WithEvents fndTdsSectionNew As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

