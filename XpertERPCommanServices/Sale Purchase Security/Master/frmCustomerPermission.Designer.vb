<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCustomerPermission
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.rlblUserCode = New common.Controls.MyLabel()
        Me.fndUser_Name = New common.UserControls.txtNavigator()
        Me.rbtnReset = New Telerik.WinControls.UI.RadButton()
        Me.TxtUserName = New common.Controls.MyTextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ChkCustomerSelect = New common.Controls.MyRadioButton()
        Me.ChkCustomerAll = New common.Controls.MyRadioButton()
        Me.GvCustomer = New common.UserControls.MyRadGridView()
        Me.GrpCustomerGroup = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkCustomerGroupSelect = New common.Controls.MyRadioButton()
        Me.chkCustomerGroupAll = New common.Controls.MyRadioButton()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.rbtnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.Import = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.rlblUserCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtUserName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.ChkCustomerSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkCustomerAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GvCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GvCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpCustomerGroup.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkCustomerGroupSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustomerGroupAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.rlblUserCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndUser_Name)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rbtnReset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtUserName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GrpCustomerGroup)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1054, 463)
        Me.SplitContainer1.SplitterDistance = 418
        Me.SplitContainer1.TabIndex = 0
        '
        'rlblUserCode
        '
        Me.rlblUserCode.FieldName = Nothing
        Me.rlblUserCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblUserCode.Location = New System.Drawing.Point(16, 21)
        Me.rlblUserCode.Name = "rlblUserCode"
        Me.rlblUserCode.Size = New System.Drawing.Size(60, 16)
        Me.rlblUserCode.TabIndex = 121
        Me.rlblUserCode.Text = "User Code"
        '
        'fndUser_Name
        '
        Me.fndUser_Name.FieldName = Nothing
        Me.fndUser_Name.Location = New System.Drawing.Point(80, 18)
        Me.fndUser_Name.MendatroryField = True
        Me.fndUser_Name.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndUser_Name.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndUser_Name.MyLinkLable1 = Nothing
        Me.fndUser_Name.MyLinkLable2 = Nothing
        Me.fndUser_Name.MyMaxLength = 32767
        Me.fndUser_Name.MyReadOnly = False
        Me.fndUser_Name.Name = "fndUser_Name"
        Me.fndUser_Name.Size = New System.Drawing.Size(210, 21)
        Me.fndUser_Name.TabIndex = 0
        Me.fndUser_Name.Value = ""
        '
        'rbtnReset
        '
        Me.rbtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnReset.Image = Global.XpertERPCommanServices.My.Resources.Resources._new
        Me.rbtnReset.Location = New System.Drawing.Point(301, 21)
        Me.rbtnReset.Name = "rbtnReset"
        Me.rbtnReset.Size = New System.Drawing.Size(14, 18)
        Me.rbtnReset.TabIndex = 1
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
        Me.TxtUserName.Location = New System.Drawing.Point(328, 20)
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
        Me.TxtUserName.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel2)
        Me.GroupBox1.Controls.Add(Me.GvCustomer)
        Me.GroupBox1.Location = New System.Drawing.Point(358, 47)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(684, 375)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Customer"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.ChkCustomerSelect)
        Me.Panel2.Controls.Add(Me.ChkCustomerAll)
        Me.Panel2.Location = New System.Drawing.Point(8, 17)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(672, 29)
        Me.Panel2.TabIndex = 0
        '
        'ChkCustomerSelect
        '
        Me.ChkCustomerSelect.Location = New System.Drawing.Point(216, 8)
        Me.ChkCustomerSelect.MyLinkLable1 = Nothing
        Me.ChkCustomerSelect.MyLinkLable2 = Nothing
        Me.ChkCustomerSelect.Name = "ChkCustomerSelect"
        Me.ChkCustomerSelect.Size = New System.Drawing.Size(50, 18)
        Me.ChkCustomerSelect.TabIndex = 1
        Me.ChkCustomerSelect.Text = "Select"
        '
        'ChkCustomerAll
        '
        Me.ChkCustomerAll.Location = New System.Drawing.Point(167, 8)
        Me.ChkCustomerAll.MyLinkLable1 = Nothing
        Me.ChkCustomerAll.MyLinkLable2 = Nothing
        Me.ChkCustomerAll.Name = "ChkCustomerAll"
        Me.ChkCustomerAll.Size = New System.Drawing.Size(33, 18)
        Me.ChkCustomerAll.TabIndex = 0
        Me.ChkCustomerAll.Text = "All"
        '
        'GvCustomer
        '
        Me.GvCustomer.Location = New System.Drawing.Point(8, 50)
        '
        'GvCustomer
        '
        Me.GvCustomer.MasterTemplate.AllowAddNewRow = False
        Me.GvCustomer.MasterTemplate.AllowEditRow = False
        Me.GvCustomer.MasterTemplate.EnableFiltering = True
        Me.GvCustomer.MasterTemplate.ShowHeaderCellButtons = True
        Me.GvCustomer.Name = "GvCustomer"
        Me.GvCustomer.ShowGroupPanel = False
        Me.GvCustomer.ShowHeaderCellButtons = True
        Me.GvCustomer.Size = New System.Drawing.Size(672, 311)
        Me.GvCustomer.TabIndex = 1
        Me.GvCustomer.TabStop = False
        Me.GvCustomer.Text = "RadGridView1"
        '
        'GrpCustomerGroup
        '
        Me.GrpCustomerGroup.Controls.Add(Me.Panel1)
        Me.GrpCustomerGroup.Controls.Add(Me.gv)
        Me.GrpCustomerGroup.Location = New System.Drawing.Point(9, 47)
        Me.GrpCustomerGroup.Name = "GrpCustomerGroup"
        Me.GrpCustomerGroup.Size = New System.Drawing.Size(339, 375)
        Me.GrpCustomerGroup.TabIndex = 3
        Me.GrpCustomerGroup.TabStop = False
        Me.GrpCustomerGroup.Text = "Customer Group"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkCustomerGroupSelect)
        Me.Panel1.Controls.Add(Me.chkCustomerGroupAll)
        Me.Panel1.Location = New System.Drawing.Point(8, 17)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(320, 29)
        Me.Panel1.TabIndex = 0
        '
        'chkCustomerGroupSelect
        '
        Me.chkCustomerGroupSelect.Location = New System.Drawing.Point(153, 8)
        Me.chkCustomerGroupSelect.MyLinkLable1 = Nothing
        Me.chkCustomerGroupSelect.MyLinkLable2 = Nothing
        Me.chkCustomerGroupSelect.Name = "chkCustomerGroupSelect"
        Me.chkCustomerGroupSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCustomerGroupSelect.TabIndex = 1
        Me.chkCustomerGroupSelect.Text = "Select"
        '
        'chkCustomerGroupAll
        '
        Me.chkCustomerGroupAll.Location = New System.Drawing.Point(104, 8)
        Me.chkCustomerGroupAll.MyLinkLable1 = Nothing
        Me.chkCustomerGroupAll.MyLinkLable2 = Nothing
        Me.chkCustomerGroupAll.Name = "chkCustomerGroupAll"
        Me.chkCustomerGroupAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCustomerGroupAll.TabIndex = 0
        Me.chkCustomerGroupAll.Text = "All"
        '
        'gv
        '
        Me.gv.Location = New System.Drawing.Point(8, 50)
        '
        'gv
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowEditRow = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(320, 311)
        Me.gv.TabIndex = 1
        Me.gv.TabStop = False
        Me.gv.Text = "RadGridView1"
        '
        'rbtnDelete
        '
        Me.rbtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnDelete.Location = New System.Drawing.Point(88, 7)
        Me.rbtnDelete.Name = "rbtnDelete"
        Me.rbtnDelete.Size = New System.Drawing.Size(68, 18)
        Me.rbtnDelete.TabIndex = 1
        Me.rbtnDelete.Text = "&Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(965, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "&Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(9, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "&Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1054, 20)
        Me.RadMenu1.TabIndex = 2
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Import, Me.Export})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'Import
        '
        Me.Import.AccessibleDescription = "Import"
        Me.Import.AccessibleName = "Import"
        Me.Import.Name = "Import"
        Me.Import.Text = "Import"
        '
        'Export
        '
        Me.Export.AccessibleDescription = "Export"
        Me.Export.AccessibleName = "Export"
        Me.Export.Name = "Export"
        Me.Export.Text = "Export"
        '
        'FrmCustomerPermission
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1054, 483)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmCustomerPermission"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmCustomerPermission"
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
        CType(Me.ChkCustomerSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkCustomerAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GvCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GvCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpCustomerGroup.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkCustomerGroupSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustomerGroupAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents GrpCustomerGroup As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkCustomerGroupSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCustomerGroupAll As common.Controls.MyRadioButton
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ChkCustomerSelect As common.Controls.MyRadioButton
    Friend WithEvents ChkCustomerAll As common.Controls.MyRadioButton
    Friend WithEvents GvCustomer As common.UserControls.MyRadGridView
    Friend WithEvents rlblUserCode As common.Controls.MyLabel
    Friend WithEvents fndUser_Name As common.UserControls.txtNavigator
    Friend WithEvents rbtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents TxtUserName As common.Controls.MyTextBox
    Friend WithEvents rbtnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
End Class

