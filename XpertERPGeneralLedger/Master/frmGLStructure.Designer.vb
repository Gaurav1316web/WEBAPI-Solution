<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGLStructure
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
        Me.radtxt_structcode = New common.Controls.MyTextBox()
        Me.listbox1 = New Telerik.WinControls.UI.RadListControl()
        Me.listbox2 = New Telerik.WinControls.UI.RadListControl()
        Me.btn_delete = New Telerik.WinControls.UI.RadButton()
        Me.btn_close = New Telerik.WinControls.UI.RadButton()
        Me.radmenu_file = New Telerik.WinControls.UI.RadMenuItem()
        Me.radmenu_export = New Telerik.WinControls.UI.RadMenuItem()
        Me.radmenu_import = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu_options = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.btn_include = New Telerik.WinControls.UI.RadButton()
        Me.btn_exclude = New Telerik.WinControls.UI.RadButton()
        Me.btn_save = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.fnd_structurecode = New common.UserControls.txtNavigator()
        Me.btn_reset = New Telerik.WinControls.UI.RadButton()
        Me.ToolTipStructure = New System.Windows.Forms.ToolTip(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.radtxt_structcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.listbox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.listbox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_delete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_close, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_include, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_exclude, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_save, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.btn_reset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'radtxt_structcode
        '
        Me.radtxt_structcode.CalculationExpression = Nothing
        Me.radtxt_structcode.FieldCode = Nothing
        Me.radtxt_structcode.FieldDesc = Nothing
        Me.radtxt_structcode.FieldMaxLength = 0
        Me.radtxt_structcode.FieldName = Nothing
        Me.radtxt_structcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radtxt_structcode.isCalculatedField = False
        Me.radtxt_structcode.IsSourceFromTable = False
        Me.radtxt_structcode.IsSourceFromValueList = False
        Me.radtxt_structcode.IsUnique = False
        Me.radtxt_structcode.Location = New System.Drawing.Point(341, 30)
        Me.radtxt_structcode.MaxLength = 60
        Me.radtxt_structcode.MendatroryField = False
        Me.radtxt_structcode.MyLinkLable1 = Nothing
        Me.radtxt_structcode.MyLinkLable2 = Nothing
        Me.radtxt_structcode.Name = "radtxt_structcode"
        Me.radtxt_structcode.ReferenceFieldDesc = Nothing
        Me.radtxt_structcode.ReferenceFieldName = Nothing
        Me.radtxt_structcode.ReferenceTableName = Nothing
        Me.radtxt_structcode.Size = New System.Drawing.Size(236, 18)
        Me.radtxt_structcode.TabIndex = 1
        '
        'listbox1
        '
        Me.listbox1.Location = New System.Drawing.Point(13, 80)
        Me.listbox1.Name = "listbox1"
        Me.listbox1.Size = New System.Drawing.Size(238, 142)
        Me.listbox1.TabIndex = 13
        '
        'listbox2
        '
        Me.listbox2.Location = New System.Drawing.Point(345, 80)
        Me.listbox2.Name = "listbox2"
        Me.listbox2.Size = New System.Drawing.Size(232, 142)
        Me.listbox2.TabIndex = 14
        '
        'btn_delete
        '
        Me.btn_delete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_delete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_delete.Location = New System.Drawing.Point(73, 3)
        Me.btn_delete.Name = "btn_delete"
        Me.btn_delete.Size = New System.Drawing.Size(68, 18)
        Me.btn_delete.TabIndex = 5
        Me.btn_delete.Text = "Delete"
        '
        'btn_close
        '
        Me.btn_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_close.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_close.Location = New System.Drawing.Point(536, 8)
        Me.btn_close.Name = "btn_close"
        Me.btn_close.Size = New System.Drawing.Size(68, 18)
        Me.btn_close.TabIndex = 6
        Me.btn_close.Text = "Close"
        '
        'radmenu_file
        '
        Me.radmenu_file.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radmenu_file.Items.AddRange(New Telerik.WinControls.RadItem() {Me.radmenu_export, Me.radmenu_import, Me.RadMenu_options})
        Me.radmenu_file.Name = "radmenu_file"
        Me.radmenu_file.Text = "File"
        '
        'radmenu_export
        '
        Me.radmenu_export.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radmenu_export.Name = "radmenu_export"
        Me.radmenu_export.Text = "Export..."
        '
        'radmenu_import
        '
        Me.radmenu_import.AccessibleDescription = "Imports..."
        Me.radmenu_import.AccessibleName = "Imports..."
        Me.radmenu_import.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radmenu_import.Name = "radmenu_import"
        Me.radmenu_import.Text = "Import..."
        '
        'RadMenu_options
        '
        Me.RadMenu_options.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenu_options.Name = "RadMenu_options"
        Me.RadMenu_options.Text = "Options"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.radmenu_file})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(622, 20)
        Me.RadMenu1.TabIndex = 20
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(13, 32)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(82, 16)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "Structure Code"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(13, 58)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(250, 16)
        Me.RadLabel2.TabIndex = 11
        Me.RadLabel2.Text = "Choose the segment to be added to this account"
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(345, 58)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(210, 16)
        Me.RadLabel3.TabIndex = 12
        Me.RadLabel3.Text = "Segments currently used by this account"
        '
        'btn_include
        '
        Me.btn_include.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_include.Location = New System.Drawing.Point(262, 111)
        Me.btn_include.Name = "btn_include"
        Me.btn_include.Size = New System.Drawing.Size(68, 18)
        Me.btn_include.TabIndex = 2
        Me.btn_include.Text = "Include->"
        '
        'btn_exclude
        '
        Me.btn_exclude.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_exclude.Location = New System.Drawing.Point(262, 166)
        Me.btn_exclude.Name = "btn_exclude"
        Me.btn_exclude.Size = New System.Drawing.Size(68, 18)
        Me.btn_exclude.TabIndex = 3
        Me.btn_exclude.Text = "<-Exclude"
        '
        'btn_save
        '
        Me.btn_save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_save.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_save.Location = New System.Drawing.Point(5, 3)
        Me.btn_save.Name = "btn_save"
        Me.btn_save.Size = New System.Drawing.Size(68, 18)
        Me.btn_save.TabIndex = 4
        Me.btn_save.Text = "Save"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.fnd_structurecode)
        Me.RadGroupBox1.Controls.Add(Me.btn_reset)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.radtxt_structcode)
        Me.RadGroupBox1.Controls.Add(Me.btn_exclude)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.listbox1)
        Me.RadGroupBox1.Controls.Add(Me.btn_include)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.listbox2)
        Me.RadGroupBox1.HeaderText = "Account Structure"
        Me.RadGroupBox1.Location = New System.Drawing.Point(14, 14)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(590, 283)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "Account Structure"
        '
        'fnd_structurecode
        '
        Me.fnd_structurecode.FieldName = Nothing
        Me.fnd_structurecode.Location = New System.Drawing.Point(102, 28)
        Me.fnd_structurecode.MendatroryField = True
        Me.fnd_structurecode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fnd_structurecode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fnd_structurecode.MyLinkLable1 = Me.RadLabel1
        Me.fnd_structurecode.MyLinkLable2 = Nothing
        Me.fnd_structurecode.MyMaxLength = 30
        Me.fnd_structurecode.MyReadOnly = False
        Me.fnd_structurecode.Name = "fnd_structurecode"
        Me.fnd_structurecode.Size = New System.Drawing.Size(214, 21)
        Me.fnd_structurecode.TabIndex = 0
        Me.fnd_structurecode.Value = ""
        '
        'btn_reset
        '
        Me.btn_reset.Image = Global.XpertERPGeneralLedger.My.Resources.Resources._new
        Me.btn_reset.Location = New System.Drawing.Point(320, 29)
        Me.btn_reset.Name = "btn_reset"
        Me.btn_reset.Size = New System.Drawing.Size(15, 20)
        Me.btn_reset.TabIndex = 1
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_close)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_delete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_save)
        Me.SplitContainer1.Size = New System.Drawing.Size(622, 393)
        Me.SplitContainer1.SplitterDistance = 360
        Me.SplitContainer1.TabIndex = 21
        '
        'frmGLStructure
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(622, 413)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmGLStructure"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Account Structure"
        CType(Me.radtxt_structcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.listbox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.listbox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_delete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_close, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_include, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_exclude, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_save, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.btn_reset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents radtxt_structcode As common.Controls.MyTextBox
    Friend WithEvents listbox1 As Telerik.WinControls.UI.RadListControl
    Friend WithEvents btn_delete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btn_close As Telerik.WinControls.UI.RadButton
    Friend WithEvents radmenu_file As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents radmenu_export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents radmenu_import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenu_options As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btn_include As Telerik.WinControls.UI.RadButton
    Friend WithEvents btn_exclude As Telerik.WinControls.UI.RadButton
    Friend WithEvents btn_save As Telerik.WinControls.UI.RadButton
    Friend WithEvents listbox2 As Telerik.WinControls.UI.RadListControl
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents ToolTipStructure As System.Windows.Forms.ToolTip
    Friend WithEvents btn_reset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents fnd_structurecode As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

