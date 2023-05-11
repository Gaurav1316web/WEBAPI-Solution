<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MilkRateType
    Inherits Telerik.WinControls.UI.RadForm

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
Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
Me.rdmenu1 = New Telerik.WinControls.UI.RadMenu
Me.File = New Telerik.WinControls.UI.RadMenuItem
Me.Import = New Telerik.WinControls.UI.RadMenuItem
Me.Export = New Telerik.WinControls.UI.RadMenuItem
Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
Me.ddlCategory = New common.Controls.MyComboBox
Me.lblCategory = New common.Controls.MyLabel
Me.rdbtnreset = New Telerik.WinControls.UI.RadButton
Me.lblDescription = New common.Controls.MyLabel
Me.txtDescription = New common.Controls.MyTextBox
Me.fndRatetypeCode = New common.UserControls.txtNavigator
Me.lblRateTypeCode = New common.Controls.MyLabel
Me.rdbtnsave = New Telerik.WinControls.UI.RadButton
Me.rdbtndelete = New Telerik.WinControls.UI.RadButton
Me.rdbtnclose = New Telerik.WinControls.UI.RadButton
CType(Me.rdmenu1,System.ComponentModel.ISupportInitialize).BeginInit
Me.SplitContainer1.Panel1.SuspendLayout
Me.SplitContainer1.Panel2.SuspendLayout
Me.SplitContainer1.SuspendLayout
CType(Me.ddlCategory,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.lblCategory,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.rdbtnreset,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.lblDescription,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.txtDescription,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.lblRateTypeCode,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.rdbtnsave,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.rdbtndelete,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me.rdbtnclose,System.ComponentModel.ISupportInitialize).BeginInit
CType(Me,System.ComponentModel.ISupportInitialize).BeginInit
Me.SuspendLayout
'
'rdmenu1
'
Me.rdmenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.File})
Me.rdmenu1.Location = New System.Drawing.Point(0, 0)
Me.rdmenu1.Name = "rdmenu1"
Me.rdmenu1.Size = New System.Drawing.Size(586, 20)
Me.rdmenu1.TabIndex = 3
Me.rdmenu1.Text = "rdmenu"
'
'File
'
Me.File.AccessibleDescription = "File"
Me.File.AccessibleName = "File"
Me.File.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Import, Me.Export, Me.RadMenuItem1})
Me.File.Name = "File"
Me.File.Text = "File"
Me.File.Visibility = Telerik.WinControls.ElementVisibility.Visible
'
'Import
'
Me.Import.AccessibleDescription = "Import"
Me.Import.AccessibleName = "Import"
Me.Import.Name = "Import"
Me.Import.Text = "Import"
Me.Import.Visibility = Telerik.WinControls.ElementVisibility.Visible
'
'Export
'
Me.Export.AccessibleDescription = "Export"
Me.Export.AccessibleName = "Export"
Me.Export.Name = "Export"
Me.Export.Text = "Export"
Me.Export.Visibility = Telerik.WinControls.ElementVisibility.Visible
'
'RadMenuItem1
'
Me.RadMenuItem1.AccessibleDescription = "Exit"
Me.RadMenuItem1.AccessibleName = "Exit"
Me.RadMenuItem1.Name = "RadMenuItem1"
Me.RadMenuItem1.Text = "Exit"
Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
'
'SplitContainer1
'
Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
Me.SplitContainer1.IsSplitterFixed = true
Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
Me.SplitContainer1.Name = "SplitContainer1"
Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
'
'SplitContainer1.Panel1
'
Me.SplitContainer1.Panel1.Controls.Add(Me.ddlCategory)
Me.SplitContainer1.Panel1.Controls.Add(Me.lblCategory)
Me.SplitContainer1.Panel1.Controls.Add(Me.rdbtnreset)
Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
Me.SplitContainer1.Panel1.Controls.Add(Me.fndRatetypeCode)
Me.SplitContainer1.Panel1.Controls.Add(Me.lblRateTypeCode)
'
'SplitContainer1.Panel2
'
Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtndelete)
Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
Me.SplitContainer1.Size = New System.Drawing.Size(586, 344)
Me.SplitContainer1.SplitterDistance = 307
Me.SplitContainer1.TabIndex = 4
'
'ddlCategory
'
Me.ddlCategory.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
RadListDataItem3.Tag = "Fat"
RadListDataItem3.Text = "Fat"
RadListDataItem3.TextWrap = true
RadListDataItem4.Tag = "SNF"
RadListDataItem4.Text = "SNF"
RadListDataItem4.TextWrap = true
Me.ddlCategory.Items.Add(RadListDataItem3)
Me.ddlCategory.Items.Add(RadListDataItem4)
Me.ddlCategory.Location = New System.Drawing.Point(119, 67)
Me.ddlCategory.MendatroryField = true
Me.ddlCategory.MyLinkLable1 = Nothing
Me.ddlCategory.MyLinkLable2 = Nothing
Me.ddlCategory.Name = "ddlCategory"
Me.ddlCategory.Size = New System.Drawing.Size(215, 20)
Me.ddlCategory.TabIndex = 3
'
'lblCategory
'
Me.lblCategory.Font = New System.Drawing.Font("Segoe UI", 8.25!)
Me.lblCategory.Location = New System.Drawing.Point(12, 69)
Me.lblCategory.Name = "lblCategory"
Me.lblCategory.Size = New System.Drawing.Size(51, 18)
Me.lblCategory.TabIndex = 17
Me.lblCategory.Text = "Category"
'
'rdbtnreset
'
Me.rdbtnreset.Image = Global.ERP.My.Resources.Resources._new
Me.rdbtnreset.Location = New System.Drawing.Point(340, 15)
Me.rdbtnreset.Name = "rdbtnreset"
Me.rdbtnreset.Size = New System.Drawing.Size(18, 20)
Me.rdbtnreset.TabIndex = 1
'
'lblDescription
'
Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 8.25!)
Me.lblDescription.Location = New System.Drawing.Point(12, 43)
Me.lblDescription.Name = "lblDescription"
Me.lblDescription.Size = New System.Drawing.Size(63, 18)
Me.lblDescription.TabIndex = 15
Me.lblDescription.Text = "Description"
'
'txtDescription
'
Me.txtDescription.Location = New System.Drawing.Point(119, 41)
Me.txtDescription.MaxLength = 100
Me.txtDescription.MendatroryField = false
Me.txtDescription.MyLinkLable1 = Nothing
Me.txtDescription.MyLinkLable2 = Nothing
Me.txtDescription.Name = "txtDescription"
Me.txtDescription.Size = New System.Drawing.Size(215, 20)
Me.txtDescription.TabIndex = 2
'
'fndRatetypeCode
'
Me.fndRatetypeCode.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar
Me.fndRatetypeCode.Location = New System.Drawing.Point(119, 14)
Me.fndRatetypeCode.MendatroryField = false
Me.fndRatetypeCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
Me.fndRatetypeCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
Me.fndRatetypeCode.MyLinkLable1 = Me.lblRateTypeCode
Me.fndRatetypeCode.MyLinkLable2 = Nothing
Me.fndRatetypeCode.MyMaxLength = 12
Me.fndRatetypeCode.MyReadOnly = false
Me.fndRatetypeCode.Name = "fndRatetypeCode"
Me.fndRatetypeCode.Size = New System.Drawing.Size(215, 21)
Me.fndRatetypeCode.TabIndex = 0
Me.fndRatetypeCode.Value = ""
'
'lblRateTypeCode
'
Me.lblRateTypeCode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
Me.lblRateTypeCode.Location = New System.Drawing.Point(12, 17)
Me.lblRateTypeCode.Name = "lblRateTypeCode"
Me.lblRateTypeCode.Size = New System.Drawing.Size(89, 18)
Me.lblRateTypeCode.TabIndex = 11
Me.lblRateTypeCode.Text = "Rate Type Code"
'
'rdbtnsave
'
Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
Me.rdbtnsave.Location = New System.Drawing.Point(9, 12)
Me.rdbtnsave.Name = "rdbtnsave"
Me.rdbtnsave.Size = New System.Drawing.Size(66, 18)
Me.rdbtnsave.TabIndex = 0
Me.rdbtnsave.Text = "Save"
'
'rdbtndelete
'
Me.rdbtndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
Me.rdbtndelete.Location = New System.Drawing.Point(82, 12)
Me.rdbtndelete.Name = "rdbtndelete"
Me.rdbtndelete.Size = New System.Drawing.Size(66, 18)
Me.rdbtndelete.TabIndex = 1
Me.rdbtndelete.Text = "Delete"
'
'rdbtnclose
'
Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
Me.rdbtnclose.Location = New System.Drawing.Point(506, 12)
Me.rdbtnclose.Name = "rdbtnclose"
Me.rdbtnclose.Size = New System.Drawing.Size(66, 18)
Me.rdbtnclose.TabIndex = 2
Me.rdbtnclose.Text = "Close"
'
'MilkRateType
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.ClientSize = New System.Drawing.Size(586, 364)
Me.Controls.Add(Me.SplitContainer1)
Me.Controls.Add(Me.rdmenu1)
Me.Name = "MilkRateType"
'
'
'
Me.RootElement.ApplyShapeToControl = true
Me.Text = "MilkRateType"
Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
CType(Me.rdmenu1,System.ComponentModel.ISupportInitialize).EndInit
Me.SplitContainer1.Panel1.ResumeLayout(false)
Me.SplitContainer1.Panel1.PerformLayout
Me.SplitContainer1.Panel2.ResumeLayout(false)
Me.SplitContainer1.ResumeLayout(false)
CType(Me.ddlCategory,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.lblCategory,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.rdbtnreset,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.lblDescription,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.txtDescription,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.lblRateTypeCode,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.rdbtnsave,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.rdbtndelete,System.ComponentModel.ISupportInitialize).EndInit
CType(Me.rdbtnclose,System.ComponentModel.ISupportInitialize).EndInit
CType(Me,System.ComponentModel.ISupportInitialize).EndInit
Me.ResumeLayout(false)
Me.PerformLayout

End Sub
    Friend WithEvents rdmenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents File As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ddlCategory As common.Controls.MyComboBox
    Friend WithEvents lblCategory As common.Controls.MyLabel
    Friend WithEvents rdbtnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents fndRatetypeCode As common.UserControls.txtNavigator
    Friend WithEvents lblRateTypeCode As common.Controls.MyLabel
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtndelete As Telerik.WinControls.UI.RadButton
End Class

