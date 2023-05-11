<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSourceCode
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSourceCode))
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.lblSourceCode = New common.Controls.MyLabel
        Me.lblDescription = New common.Controls.MyLabel
        Me.txtSourceCodeDesc = New common.Controls.MyTextBox
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.MenuImport = New Telerik.WinControls.UI.RadMenuItem
        Me.MenuExport = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem6 = New Telerik.WinControls.UI.RadMenuItem
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.mskSourceCode = New System.Windows.Forms.MaskedTextBox
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.fndSourceCode = New common.UserControls.txtNavigator
        Me.CboTallyName = New common.Controls.MyComboBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        CType(Me.lblSourceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSourceCodeDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.CboTallyName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblSourceCode
        '
        Me.lblSourceCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSourceCode.Location = New System.Drawing.Point(13, 23)
        Me.lblSourceCode.Name = "lblSourceCode"
        Me.lblSourceCode.Size = New System.Drawing.Size(72, 16)
        Me.lblSourceCode.TabIndex = 10
        Me.lblSourceCode.Text = "Source Code"
        '
        'lblDescription
        '
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(13, 48)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 11
        Me.lblDescription.Text = "Description"
        '
        'txtSourceCodeDesc
        '
        Me.txtSourceCodeDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSourceCodeDesc.Location = New System.Drawing.Point(116, 47)
        Me.txtSourceCodeDesc.MaxLength = 50
        Me.txtSourceCodeDesc.MendatroryField = False
        Me.txtSourceCodeDesc.MyLinkLable1 = Me.lblDescription
        Me.txtSourceCodeDesc.MyLinkLable2 = Nothing
        Me.txtSourceCodeDesc.Name = "txtSourceCodeDesc"
        Me.txtSourceCodeDesc.Size = New System.Drawing.Size(272, 18)
        Me.txtSourceCodeDesc.TabIndex = 1
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.AutoSize = True
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuImport, Me.MenuExport, Me.RadMenuItem6})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'MenuImport
        '
        Me.MenuImport.AccessibleDescription = "Import"
        Me.MenuImport.AccessibleName = "Import"
        Me.MenuImport.Name = "MenuImport"
        Me.MenuImport.Text = "Import"
        Me.MenuImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'MenuExport
        '
        Me.MenuExport.AccessibleDescription = "Export"
        Me.MenuExport.AccessibleName = "Export"
        Me.MenuExport.Name = "MenuExport"
        Me.MenuExport.Text = "Export"
        Me.MenuExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem6
        '
        Me.RadMenuItem6.AccessibleDescription = "Close"
        Me.RadMenuItem6.AccessibleName = "Close"
        Me.RadMenuItem6.Name = "RadMenuItem6"
        Me.RadMenuItem6.Text = "Close"
        Me.RadMenuItem6.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(16, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(87, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(347, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(335, 17)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnNew, "New")
        '
        'mskSourceCode
        '
        Me.mskSourceCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskSourceCode.Location = New System.Drawing.Point(157, 18)
        Me.mskSourceCode.Mask = "LL-LL"
        Me.mskSourceCode.Name = "mskSourceCode"
        Me.mskSourceCode.Size = New System.Drawing.Size(114, 20)
        Me.mskSourceCode.TabIndex = 40
        Me.ToolTip1.SetToolTip(Me.mskSourceCode, "Finder")
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.mskSourceCode)
        Me.RadGroupBox1.Controls.Add(Me.fndSourceCode)
        Me.RadGroupBox1.Controls.Add(Me.CboTallyName)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.lblSourceCode)
        Me.RadGroupBox1.Controls.Add(Me.lblDescription)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.txtSourceCodeDesc)
        Me.RadGroupBox1.HeaderText = " "
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 6)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(409, 122)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = " "
        '
        'fndSourceCode
        '
        Me.fndSourceCode.Location = New System.Drawing.Point(116, 18)
        Me.fndSourceCode.MendatroryField = False
        Me.fndSourceCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndSourceCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndSourceCode.MyLinkLable1 = Nothing
        Me.fndSourceCode.MyLinkLable2 = Nothing
        Me.fndSourceCode.MyMaxLength = 5
        Me.fndSourceCode.MyReadOnly = False
        Me.fndSourceCode.Name = "fndSourceCode"
        Me.fndSourceCode.Size = New System.Drawing.Size(213, 21)
        Me.fndSourceCode.TabIndex = 39
        Me.fndSourceCode.Value = ""
        '
        'CboTallyName
        '
        Me.CboTallyName.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboTallyName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "Normal"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Adjustment"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Enabled = False
        RadListDataItem3.Text = "Closing"
        RadListDataItem3.TextWrap = True
        Me.CboTallyName.Items.Add(RadListDataItem1)
        Me.CboTallyName.Items.Add(RadListDataItem2)
        Me.CboTallyName.Items.Add(RadListDataItem3)
        Me.CboTallyName.Location = New System.Drawing.Point(116, 72)
        Me.CboTallyName.MendatroryField = False
        Me.CboTallyName.MyLinkLable1 = Nothing
        Me.CboTallyName.MyLinkLable2 = Nothing
        Me.CboTallyName.Name = "CboTallyName"
        Me.CboTallyName.Size = New System.Drawing.Size(272, 18)
        Me.CboTallyName.TabIndex = 2
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(13, 73)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel1.TabIndex = 13
        Me.MyLabel1.Text = "Name In Tally"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(437, 177)
        Me.SplitContainer1.SplitterDistance = 146
        Me.SplitContainer1.TabIndex = 0
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(437, 20)
        Me.RadMenu1.TabIndex = 13
        Me.RadMenu1.Text = "RadMenu1"
        '
        'FrmSourceCode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(437, 197)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmSourceCode"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Source  Codes"
        Me.ToolTip1.SetToolTip(Me, "New")
        CType(Me.lblSourceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSourceCodeDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.CboTallyName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtSourceCodeDesc As common.Controls.MyTextBox
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem6 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblSourceCode As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents CboTallyName As common.Controls.MyComboBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents fndSourceCode As common.UserControls.txtNavigator
    Friend WithEvents mskSourceCode As System.Windows.Forms.MaskedTextBox
End Class

