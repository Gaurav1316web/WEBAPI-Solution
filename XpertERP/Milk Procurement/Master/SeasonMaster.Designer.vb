<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SeasonMaster
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.rdmenu1 = New Telerik.WinControls.UI.RadMenu
        Me.File = New Telerik.WinControls.UI.RadMenuItem
        Me.Import = New Telerik.WinControls.UI.RadMenuItem
        Me.Export = New Telerik.WinControls.UI.RadMenuItem
        Me.Ex = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.chkDecember = New Telerik.WinControls.UI.RadCheckBox
        Me.chkNovember = New Telerik.WinControls.UI.RadCheckBox
        Me.chkOctober = New Telerik.WinControls.UI.RadCheckBox
        Me.chkSeptember = New Telerik.WinControls.UI.RadCheckBox
        Me.chkAugust = New Telerik.WinControls.UI.RadCheckBox
        Me.chkJuly = New Telerik.WinControls.UI.RadCheckBox
        Me.chkJune = New Telerik.WinControls.UI.RadCheckBox
        Me.chkMay = New Telerik.WinControls.UI.RadCheckBox
        Me.chkApril = New Telerik.WinControls.UI.RadCheckBox
        Me.chkMarch = New Telerik.WinControls.UI.RadCheckBox
        Me.chkFab = New Telerik.WinControls.UI.RadCheckBox
        Me.chkJan = New Telerik.WinControls.UI.RadCheckBox
        Me.lblSeasonMonth = New common.Controls.MyLabel
        Me.rdbtnreset = New Telerik.WinControls.UI.RadButton
        Me.txtDescription = New common.Controls.MyTextBox
        Me.lblDescription = New common.Controls.MyLabel
        Me.ddlSeasonCode = New common.Controls.MyComboBox
        Me.lblSeasonCode = New common.Controls.MyLabel
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton
        Me.rdbtndelete = New Telerik.WinControls.UI.RadButton
        CType(Me.rdmenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.chkDecember, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkNovember, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOctober, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSeptember, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAugust, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkJuly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkJune, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkApril, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMarch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFab, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkJan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSeasonMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlSeasonCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSeasonCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdmenu1
        '
        Me.rdmenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.File})
        Me.rdmenu1.Location = New System.Drawing.Point(0, 0)
        Me.rdmenu1.Name = "rdmenu1"
        Me.rdmenu1.Size = New System.Drawing.Size(623, 20)
        Me.rdmenu1.TabIndex = 2
        Me.rdmenu1.Text = "rdmenu"
        '
        'File
        '
        Me.File.AccessibleDescription = "File"
        Me.File.AccessibleName = "File"
        Me.File.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Import, Me.Export, Me.Ex})
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
        'Ex
        '
        Me.Ex.AccessibleDescription = "Exit"
        Me.Ex.AccessibleName = "Exit"
        Me.Ex.Name = "Ex"
        Me.Ex.Text = "Exit"
        Me.Ex.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSeasonMonth)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdbtnreset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ddlSeasonCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSeasonCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(623, 437)
        Me.SplitContainer1.SplitterDistance = 399
        Me.SplitContainer1.TabIndex = 3
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.chkDecember)
        Me.RadGroupBox2.Controls.Add(Me.chkNovember)
        Me.RadGroupBox2.Controls.Add(Me.chkOctober)
        Me.RadGroupBox2.Controls.Add(Me.chkSeptember)
        Me.RadGroupBox2.Controls.Add(Me.chkAugust)
        Me.RadGroupBox2.Controls.Add(Me.chkJuly)
        Me.RadGroupBox2.Controls.Add(Me.chkJune)
        Me.RadGroupBox2.Controls.Add(Me.chkMay)
        Me.RadGroupBox2.Controls.Add(Me.chkApril)
        Me.RadGroupBox2.Controls.Add(Me.chkMarch)
        Me.RadGroupBox2.Controls.Add(Me.chkFab)
        Me.RadGroupBox2.Controls.Add(Me.chkJan)
        Me.RadGroupBox2.HeaderText = "Month"
        Me.RadGroupBox2.Location = New System.Drawing.Point(95, 73)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(301, 88)
        Me.RadGroupBox2.TabIndex = 3
        Me.RadGroupBox2.Text = "Month"
        '
        'chkDecember
        '
        Me.chkDecember.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDecember.Location = New System.Drawing.Point(220, 67)
        Me.chkDecember.Name = "chkDecember"
        Me.chkDecember.Size = New System.Drawing.Size(72, 16)
        Me.chkDecember.TabIndex = 11
        Me.chkDecember.Text = "December"
        '
        'chkNovember
        '
        Me.chkNovember.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNovember.Location = New System.Drawing.Point(152, 67)
        Me.chkNovember.Name = "chkNovember"
        Me.chkNovember.Size = New System.Drawing.Size(72, 16)
        Me.chkNovember.TabIndex = 10
        Me.chkNovember.Text = "November"
        '
        'chkOctober
        '
        Me.chkOctober.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOctober.Location = New System.Drawing.Point(87, 67)
        Me.chkOctober.Name = "chkOctober"
        Me.chkOctober.Size = New System.Drawing.Size(60, 16)
        Me.chkOctober.TabIndex = 9
        Me.chkOctober.Text = "October"
        '
        'chkSeptember
        '
        Me.chkSeptember.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSeptember.Location = New System.Drawing.Point(13, 67)
        Me.chkSeptember.Name = "chkSeptember"
        Me.chkSeptember.Size = New System.Drawing.Size(76, 16)
        Me.chkSeptember.TabIndex = 8
        Me.chkSeptember.Text = "September"
        '
        'chkAugust
        '
        Me.chkAugust.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAugust.Location = New System.Drawing.Point(220, 45)
        Me.chkAugust.Name = "chkAugust"
        Me.chkAugust.Size = New System.Drawing.Size(55, 16)
        Me.chkAugust.TabIndex = 7
        Me.chkAugust.Text = "August"
        '
        'chkJuly
        '
        Me.chkJuly.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkJuly.Location = New System.Drawing.Point(152, 45)
        Me.chkJuly.Name = "chkJuly"
        Me.chkJuly.Size = New System.Drawing.Size(40, 16)
        Me.chkJuly.TabIndex = 6
        Me.chkJuly.Text = "July"
        '
        'chkJune
        '
        Me.chkJune.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkJune.Location = New System.Drawing.Point(87, 45)
        Me.chkJune.Name = "chkJune"
        Me.chkJune.Size = New System.Drawing.Size(45, 16)
        Me.chkJune.TabIndex = 5
        Me.chkJune.Text = "June"
        '
        'chkMay
        '
        Me.chkMay.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMay.Location = New System.Drawing.Point(13, 45)
        Me.chkMay.Name = "chkMay"
        Me.chkMay.Size = New System.Drawing.Size(42, 16)
        Me.chkMay.TabIndex = 4
        Me.chkMay.Text = "May"
        '
        'chkApril
        '
        Me.chkApril.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkApril.Location = New System.Drawing.Point(220, 23)
        Me.chkApril.Name = "chkApril"
        Me.chkApril.Size = New System.Drawing.Size(43, 16)
        Me.chkApril.TabIndex = 3
        Me.chkApril.Text = "April"
        '
        'chkMarch
        '
        Me.chkMarch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMarch.Location = New System.Drawing.Point(152, 23)
        Me.chkMarch.Name = "chkMarch"
        Me.chkMarch.Size = New System.Drawing.Size(52, 16)
        Me.chkMarch.TabIndex = 2
        Me.chkMarch.Text = "March"
        '
        'chkFab
        '
        Me.chkFab.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFab.Location = New System.Drawing.Point(87, 23)
        Me.chkFab.Name = "chkFab"
        Me.chkFab.Size = New System.Drawing.Size(65, 16)
        Me.chkFab.TabIndex = 1
        Me.chkFab.Text = "February"
        '
        'chkJan
        '
        Me.chkJan.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkJan.Location = New System.Drawing.Point(13, 23)
        Me.chkJan.Name = "chkJan"
        Me.chkJan.Size = New System.Drawing.Size(60, 16)
        Me.chkJan.TabIndex = 0
        Me.chkJan.Text = "January"
        '
        'lblSeasonMonth
        '
        Me.lblSeasonMonth.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblSeasonMonth.Location = New System.Drawing.Point(12, 73)
        Me.lblSeasonMonth.Name = "lblSeasonMonth"
        Me.lblSeasonMonth.Size = New System.Drawing.Size(78, 18)
        Me.lblSeasonMonth.TabIndex = 16
        Me.lblSeasonMonth.Text = "Season Month"
        '
        'rdbtnreset
        '
        Me.rdbtnreset.Image = Global.ERP.My.Resources.Resources._new
        Me.rdbtnreset.Location = New System.Drawing.Point(316, 23)
        Me.rdbtnreset.Name = "rdbtnreset"
        Me.rdbtnreset.Size = New System.Drawing.Size(18, 20)
        Me.rdbtnreset.TabIndex = 1
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(95, 47)
        Me.txtDescription.MaxLength = 100
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Nothing
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(215, 20)
        Me.txtDescription.TabIndex = 2
        '
        'lblDescription
        '
        Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblDescription.Location = New System.Drawing.Point(12, 49)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 18)
        Me.lblDescription.TabIndex = 13
        Me.lblDescription.Text = "Description"
        '
        'ddlSeasonCode
        '
        Me.ddlSeasonCode.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Tag = "Fat"
        RadListDataItem1.Text = "Fat"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Tag = "SNF"
        RadListDataItem2.Text = "SNF"
        RadListDataItem2.TextWrap = True
        Me.ddlSeasonCode.Items.Add(RadListDataItem1)
        Me.ddlSeasonCode.Items.Add(RadListDataItem2)
        Me.ddlSeasonCode.Location = New System.Drawing.Point(95, 21)
        Me.ddlSeasonCode.MendatroryField = True
        Me.ddlSeasonCode.MyLinkLable1 = Nothing
        Me.ddlSeasonCode.MyLinkLable2 = Nothing
        Me.ddlSeasonCode.Name = "ddlSeasonCode"
        Me.ddlSeasonCode.Size = New System.Drawing.Size(215, 20)
        Me.ddlSeasonCode.TabIndex = 0
        '
        'lblSeasonCode
        '
        Me.lblSeasonCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblSeasonCode.Location = New System.Drawing.Point(12, 23)
        Me.lblSeasonCode.Name = "lblSeasonCode"
        Me.lblSeasonCode.Size = New System.Drawing.Size(71, 18)
        Me.lblSeasonCode.TabIndex = 12
        Me.lblSeasonCode.Text = "Season Code"
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Location = New System.Drawing.Point(3, 4)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(66, 18)
        Me.rdbtnsave.TabIndex = 0
        Me.rdbtnsave.Text = "Save"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Location = New System.Drawing.Point(542, 2)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(66, 18)
        Me.rdbtnclose.TabIndex = 2
        Me.rdbtnclose.Text = "Close"
        '
        'rdbtndelete
        '
        Me.rdbtndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtndelete.Location = New System.Drawing.Point(72, 4)
        Me.rdbtndelete.Name = "rdbtndelete"
        Me.rdbtndelete.Size = New System.Drawing.Size(66, 18)
        Me.rdbtndelete.TabIndex = 1
        Me.rdbtndelete.Text = "Delete"
        '
        'SeasonMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(623, 457)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenu1)
        Me.Name = "SeasonMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "SeasonMaster"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.rdmenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.chkDecember, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkNovember, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOctober, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSeptember, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAugust, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkJuly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkJune, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkApril, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMarch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFab, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkJan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSeasonMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlSeasonCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSeasonCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdmenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents File As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Ex As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents ddlSeasonCode As common.Controls.MyComboBox
    Friend WithEvents lblSeasonCode As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblSeasonMonth As common.Controls.MyLabel
    Friend WithEvents rdbtnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkAugust As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkJuly As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkJune As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkMay As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkApril As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkMarch As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkFab As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkJan As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkDecember As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkNovember As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkOctober As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkSeptember As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtndelete As Telerik.WinControls.UI.RadButton
End Class

