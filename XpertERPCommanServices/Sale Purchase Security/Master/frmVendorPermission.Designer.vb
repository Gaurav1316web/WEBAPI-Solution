<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVendorPermission
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.rlblUserCode = New common.Controls.MyLabel()
        Me.fndUser_Name = New common.UserControls.txtNavigator()
        Me.rbtnReset = New Telerik.WinControls.UI.RadButton()
        Me.TxtUserName = New common.Controls.MyTextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ChkVendorSelect = New common.Controls.MyRadioButton()
        Me.ChkVendorAll = New common.Controls.MyRadioButton()
        Me.GvVendor = New common.UserControls.MyRadGridView()
        Me.GrpVendorGroup = New System.Windows.Forms.GroupBox()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkVendorGroupSelect = New common.Controls.MyRadioButton()
        Me.chkVendorGroupAll = New common.Controls.MyRadioButton()
        Me.rbtnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.rlblUserCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtUserName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.ChkVendorSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkVendorAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GvVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GvVendor.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpVendorGroup.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.chkVendorGroupSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendorGroupAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.rlblUserCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndUser_Name)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rbtnReset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtUserName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GrpVendorGroup)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(860, 477)
        Me.SplitContainer1.SplitterDistance = 422
        Me.SplitContainer1.TabIndex = 0
        '
        'rlblUserCode
        '
        Me.rlblUserCode.FieldName = Nothing
        Me.rlblUserCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblUserCode.Location = New System.Drawing.Point(17, 15)
        Me.rlblUserCode.Name = "rlblUserCode"
        Me.rlblUserCode.Size = New System.Drawing.Size(60, 16)
        Me.rlblUserCode.TabIndex = 127
        Me.rlblUserCode.Text = "User Code"
        '
        'fndUser_Name
        '
        Me.fndUser_Name.FieldName = Nothing
        Me.fndUser_Name.Location = New System.Drawing.Point(81, 12)
        Me.fndUser_Name.MendatroryField = True
        Me.fndUser_Name.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndUser_Name.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndUser_Name.MyLinkLable1 = Nothing
        Me.fndUser_Name.MyLinkLable2 = Nothing
        Me.fndUser_Name.MyMaxLength = 30
        Me.fndUser_Name.MyReadOnly = False
        Me.fndUser_Name.Name = "fndUser_Name"
        Me.fndUser_Name.Size = New System.Drawing.Size(210, 21)
        Me.fndUser_Name.TabIndex = 122
        Me.fndUser_Name.Value = ""
        '
        'rbtnReset
        '
        Me.rbtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnReset.Image = Global.XpertERPCommanServices.My.Resources.Resources._new
        Me.rbtnReset.Location = New System.Drawing.Point(302, 15)
        Me.rbtnReset.Name = "rbtnReset"
        Me.rbtnReset.Size = New System.Drawing.Size(14, 18)
        Me.rbtnReset.TabIndex = 123
        '
        'TxtUserName
        '
        Me.TxtUserName.CalculationExpression = Nothing
        Me.TxtUserName.FieldCode = Nothing
        Me.TxtUserName.FieldDesc = Nothing
        Me.TxtUserName.FieldMaxLength = 0
        Me.TxtUserName.FieldName = Nothing
        Me.TxtUserName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUserName.isCalculatedField = False
        Me.TxtUserName.IsSourceFromTable = False
        Me.TxtUserName.IsSourceFromValueList = False
        Me.TxtUserName.IsUnique = False
        Me.TxtUserName.Location = New System.Drawing.Point(329, 14)
        Me.TxtUserName.MaxLength = 25
        Me.TxtUserName.MendatroryField = False
        Me.TxtUserName.MyLinkLable1 = Nothing
        Me.TxtUserName.MyLinkLable2 = Nothing
        Me.TxtUserName.Name = "TxtUserName"
        Me.TxtUserName.ReadOnly = True
        Me.TxtUserName.ReferenceFieldDesc = Nothing
        Me.TxtUserName.ReferenceFieldName = Nothing
        Me.TxtUserName.ReferenceTableName = Nothing
        Me.TxtUserName.Size = New System.Drawing.Size(186, 18)
        Me.TxtUserName.TabIndex = 124
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel2)
        Me.GroupBox1.Controls.Add(Me.GvVendor)
        Me.GroupBox1.Location = New System.Drawing.Point(359, 41)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(482, 375)
        Me.GroupBox1.TabIndex = 126
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Vendor"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.ChkVendorSelect)
        Me.Panel2.Controls.Add(Me.ChkVendorAll)
        Me.Panel2.Location = New System.Drawing.Point(8, 17)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(464, 29)
        Me.Panel2.TabIndex = 0
        '
        'ChkVendorSelect
        '
        Me.ChkVendorSelect.Location = New System.Drawing.Point(232, 8)
        Me.ChkVendorSelect.MyLinkLable1 = Nothing
        Me.ChkVendorSelect.MyLinkLable2 = Nothing
        Me.ChkVendorSelect.Name = "ChkVendorSelect"
        Me.ChkVendorSelect.Size = New System.Drawing.Size(50, 18)
        Me.ChkVendorSelect.TabIndex = 1
        Me.ChkVendorSelect.Text = "Select"
        '
        'ChkVendorAll
        '
        Me.ChkVendorAll.Location = New System.Drawing.Point(183, 8)
        Me.ChkVendorAll.MyLinkLable1 = Nothing
        Me.ChkVendorAll.MyLinkLable2 = Nothing
        Me.ChkVendorAll.Name = "ChkVendorAll"
        Me.ChkVendorAll.Size = New System.Drawing.Size(33, 18)
        Me.ChkVendorAll.TabIndex = 0
        Me.ChkVendorAll.Text = "All"
        '
        'GvVendor
        '
        Me.GvVendor.Location = New System.Drawing.Point(8, 50)
        '
        '
        '
        Me.GvVendor.MasterTemplate.AllowAddNewRow = False
        Me.GvVendor.MasterTemplate.AllowEditRow = False
        Me.GvVendor.MasterTemplate.EnableFiltering = True
        Me.GvVendor.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.GvVendor.MasterTemplate.ShowHeaderCellButtons = True
        Me.GvVendor.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.GvVendor.MyStopExport = False
        Me.GvVendor.Name = "GvVendor"
        Me.GvVendor.ShowGroupPanel = False
        Me.GvVendor.ShowHeaderCellButtons = True
        Me.GvVendor.Size = New System.Drawing.Size(464, 311)
        Me.GvVendor.TabIndex = 1
        Me.GvVendor.TabStop = False
        '
        'GrpVendorGroup
        '
        Me.GrpVendorGroup.Controls.Add(Me.gv)
        Me.GrpVendorGroup.Controls.Add(Me.Panel1)
        Me.GrpVendorGroup.Location = New System.Drawing.Point(10, 41)
        Me.GrpVendorGroup.Name = "GrpVendorGroup"
        Me.GrpVendorGroup.Size = New System.Drawing.Size(339, 375)
        Me.GrpVendorGroup.TabIndex = 125
        Me.GrpVendorGroup.TabStop = False
        Me.GrpVendorGroup.Text = "Vendor Group"
        '
        'gv
        '
        Me.gv.Location = New System.Drawing.Point(6, 49)
        '
        '
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowEditRow = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv.MyStopExport = False
        Me.gv.Name = "gv"
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(327, 311)
        Me.gv.TabIndex = 2
        Me.gv.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkVendorGroupSelect)
        Me.Panel1.Controls.Add(Me.chkVendorGroupAll)
        Me.Panel1.Location = New System.Drawing.Point(8, 17)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(320, 29)
        Me.Panel1.TabIndex = 0
        '
        'chkVendorGroupSelect
        '
        Me.chkVendorGroupSelect.Location = New System.Drawing.Point(153, 8)
        Me.chkVendorGroupSelect.MyLinkLable1 = Nothing
        Me.chkVendorGroupSelect.MyLinkLable2 = Nothing
        Me.chkVendorGroupSelect.Name = "chkVendorGroupSelect"
        Me.chkVendorGroupSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkVendorGroupSelect.TabIndex = 1
        Me.chkVendorGroupSelect.Text = "Select"
        '
        'chkVendorGroupAll
        '
        Me.chkVendorGroupAll.Location = New System.Drawing.Point(104, 8)
        Me.chkVendorGroupAll.MyLinkLable1 = Nothing
        Me.chkVendorGroupAll.MyLinkLable2 = Nothing
        Me.chkVendorGroupAll.Name = "chkVendorGroupAll"
        Me.chkVendorGroupAll.Size = New System.Drawing.Size(33, 18)
        Me.chkVendorGroupAll.TabIndex = 0
        Me.chkVendorGroupAll.Text = "All"
        '
        'rbtnDelete
        '
        Me.rbtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnDelete.Location = New System.Drawing.Point(91, 16)
        Me.rbtnDelete.Name = "rbtnDelete"
        Me.rbtnDelete.Size = New System.Drawing.Size(68, 18)
        Me.rbtnDelete.TabIndex = 4
        Me.rbtnDelete.Text = "&Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(772, 15)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 5
        Me.btnclose.Text = "&Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(12, 15)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 3
        Me.btnsave.Text = "&Save"
        '
        'FrmVendorPermission
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(860, 477)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmVendorPermission"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmVendorPermission"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.rlblUserCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtUserName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.ChkVendorSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkVendorAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GvVendor.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GvVendor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpVendorGroup.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkVendorGroupSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendorGroupAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents rlblUserCode As common.Controls.MyLabel
    Friend WithEvents fndUser_Name As common.UserControls.txtNavigator
    Friend WithEvents rbtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents TxtUserName As common.Controls.MyTextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ChkVendorSelect As common.Controls.MyRadioButton
    Friend WithEvents ChkVendorAll As common.Controls.MyRadioButton
    Friend WithEvents GvVendor As common.UserControls.MyRadGridView
    Friend WithEvents GrpVendorGroup As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkVendorGroupSelect As common.Controls.MyRadioButton
    Friend WithEvents chkVendorGroupAll As common.Controls.MyRadioButton
    Friend WithEvents rbtnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv As common.UserControls.MyRadGridView
End Class

