<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSettlementMaster
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.ChkFinancial = New Telerik.WinControls.UI.RadCheckBox()
        Me.cmbType = New common.Controls.MyComboBox()
        Me.lblTYpe = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.ddlSettlementType = New common.Controls.MyComboBox()
        Me.fndSettleMentCode = New common.UserControls.txtNavigator()
        Me.lblSettleMentCode = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtGLAccountDesc = New common.Controls.MyTextBox()
        Me.txtGLAccount = New common.UserControls.txtFinder()
        Me.grpCalculate = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbDoNothing = New common.Controls.MyRadioButton()
        Me.rdbSubtract = New common.Controls.MyRadioButton()
        Me.rdbAdd = New common.Controls.MyRadioButton()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.MenuRD = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.ChkFinancial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTYpe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlSettlementType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSettleMentCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGLAccountDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpCalculate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCalculate.SuspendLayout()
        CType(Me.rdbDoNothing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSubtract, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MenuRD, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.ChkFinancial)
        Me.RadGroupBox1.Controls.Add(Me.cmbType)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.lblTYpe)
        Me.RadGroupBox1.Controls.Add(Me.ddlSettlementType)
        Me.RadGroupBox1.Controls.Add(Me.fndSettleMentCode)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtGLAccountDesc)
        Me.RadGroupBox1.Controls.Add(Me.txtGLAccount)
        Me.RadGroupBox1.Controls.Add(Me.grpCalculate)
        Me.RadGroupBox1.Controls.Add(Me.txtDescription)
        Me.RadGroupBox1.Controls.Add(Me.lblDescription)
        Me.RadGroupBox1.Controls.Add(Me.lblSettleMentCode)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(13, 17)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(581, 158)
        Me.RadGroupBox1.TabIndex = 0
        '
        'ChkFinancial
        '
        Me.ChkFinancial.Location = New System.Drawing.Point(271, 109)
        Me.ChkFinancial.Name = "ChkFinancial"
        Me.ChkFinancial.Size = New System.Drawing.Size(92, 18)
        Me.ChkFinancial.TabIndex = 7
        Me.ChkFinancial.Text = "Financial Entry"
        '
        'cmbType
        '
        Me.cmbType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Text = "Select"
        RadListDataItem2.Text = "Quick Settlement"
        RadListDataItem3.Text = "Settlement"
        RadListDataItem4.Text = "Both"
        Me.cmbType.Items.Add(RadListDataItem1)
        Me.cmbType.Items.Add(RadListDataItem2)
        Me.cmbType.Items.Add(RadListDataItem3)
        Me.cmbType.Items.Add(RadListDataItem4)
        Me.cmbType.Location = New System.Drawing.Point(450, 109)
        Me.cmbType.MendatroryField = True
        Me.cmbType.MyLinkLable1 = Me.lblTYpe
        Me.cmbType.MyLinkLable2 = Nothing
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(118, 20)
        Me.cmbType.TabIndex = 8
        Me.cmbType.Text = "Select"
        '
        'lblTYpe
        '
        Me.lblTYpe.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTYpe.Location = New System.Drawing.Point(406, 111)
        Me.lblTYpe.Name = "lblTYpe"
        Me.lblTYpe.Size = New System.Drawing.Size(35, 16)
        Me.lblTYpe.TabIndex = 15
        Me.lblTYpe.Text = " Type"
        '
        'btnNew
        '
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(322, 17)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(20, 21)
        Me.btnNew.TabIndex = 1
        '
        'ddlSettlementType
        '
        Me.ddlSettlementType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlSettlementType.Location = New System.Drawing.Point(450, 17)
        Me.ddlSettlementType.MendatroryField = True
        Me.ddlSettlementType.MyLinkLable1 = Nothing
        Me.ddlSettlementType.MyLinkLable2 = Nothing
        Me.ddlSettlementType.Name = "ddlSettlementType"
        Me.ddlSettlementType.Size = New System.Drawing.Size(118, 20)
        Me.ddlSettlementType.TabIndex = 2
        Me.ddlSettlementType.Text = "Select"
        '
        'fndSettleMentCode
        '
        Me.fndSettleMentCode.Location = New System.Drawing.Point(114, 17)
        Me.fndSettleMentCode.MendatroryField = True
        Me.fndSettleMentCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndSettleMentCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndSettleMentCode.MyLinkLable1 = Me.lblSettleMentCode
        Me.fndSettleMentCode.MyLinkLable2 = Nothing
        Me.fndSettleMentCode.MyMaxLength = 32767
        Me.fndSettleMentCode.MyReadOnly = False
        Me.fndSettleMentCode.Name = "fndSettleMentCode"
        Me.fndSettleMentCode.Size = New System.Drawing.Size(202, 21)
        Me.fndSettleMentCode.TabIndex = 0
        Me.fndSettleMentCode.Value = ""
        '
        'lblSettleMentCode
        '
        Me.lblSettleMentCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblSettleMentCode.Location = New System.Drawing.Point(13, 19)
        Me.lblSettleMentCode.Name = "lblSettleMentCode"
        Me.lblSettleMentCode.Size = New System.Drawing.Size(95, 16)
        Me.lblSettleMentCode.TabIndex = 11
        Me.lblSettleMentCode.Text = "SettleMent Code"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(13, 72)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel2.TabIndex = 9
        Me.MyLabel2.Text = "GL/Account"
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(355, 19)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(89, 16)
        Me.MyLabel3.TabIndex = 13
        Me.MyLabel3.Text = "Settlement Type"
        '
        'txtGLAccountDesc
        '
        Me.txtGLAccountDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGLAccountDesc.Location = New System.Drawing.Point(262, 71)
        Me.txtGLAccountDesc.MaxLength = 50
        Me.txtGLAccountDesc.MendatroryField = False
        Me.txtGLAccountDesc.MyLinkLable1 = Nothing
        Me.txtGLAccountDesc.MyLinkLable2 = Nothing
        Me.txtGLAccountDesc.Name = "txtGLAccountDesc"
        Me.txtGLAccountDesc.Size = New System.Drawing.Size(306, 18)
        Me.txtGLAccountDesc.TabIndex = 5
        Me.txtGLAccountDesc.Text = " "
        '
        'txtGLAccount
        '
        Me.txtGLAccount.Location = New System.Drawing.Point(114, 71)
        Me.txtGLAccount.MendatroryField = False
        Me.txtGLAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGLAccount.MyLinkLable1 = Me.MyLabel2
        Me.txtGLAccount.MyLinkLable2 = Nothing
        Me.txtGLAccount.MyReadOnly = False
        Me.txtGLAccount.MyShowMasterFormButton = False
        Me.txtGLAccount.Name = "txtGLAccount"
        Me.txtGLAccount.Size = New System.Drawing.Size(139, 19)
        Me.txtGLAccount.TabIndex = 4
        Me.txtGLAccount.Value = ""
        '
        'grpCalculate
        '
        Me.grpCalculate.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpCalculate.Controls.Add(Me.rdbDoNothing)
        Me.grpCalculate.Controls.Add(Me.rdbSubtract)
        Me.grpCalculate.Controls.Add(Me.rdbAdd)
        Me.grpCalculate.HeaderText = " Calculate"
        Me.grpCalculate.Location = New System.Drawing.Point(13, 96)
        Me.grpCalculate.Name = "grpCalculate"
        Me.grpCalculate.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpCalculate.Size = New System.Drawing.Size(240, 44)
        Me.grpCalculate.TabIndex = 6
        Me.grpCalculate.Text = " Calculate"
        '
        'rdbDoNothing
        '
        Me.rdbDoNothing.Location = New System.Drawing.Point(148, 17)
        Me.rdbDoNothing.MyLinkLable1 = Nothing
        Me.rdbDoNothing.MyLinkLable2 = Nothing
        Me.rdbDoNothing.Name = "rdbDoNothing"
        Me.rdbDoNothing.Size = New System.Drawing.Size(79, 18)
        Me.rdbDoNothing.TabIndex = 2
        Me.rdbDoNothing.Text = "Do Nothing"
        '
        'rdbSubtract
        '
        Me.rdbSubtract.Location = New System.Drawing.Point(62, 17)
        Me.rdbSubtract.MyLinkLable1 = Nothing
        Me.rdbSubtract.MyLinkLable2 = Nothing
        Me.rdbSubtract.Name = "rdbSubtract"
        Me.rdbSubtract.Size = New System.Drawing.Size(62, 18)
        Me.rdbSubtract.TabIndex = 1
        Me.rdbSubtract.Text = "Subtract"
        '
        'rdbAdd
        '
        Me.rdbAdd.Location = New System.Drawing.Point(7, 17)
        Me.rdbAdd.MyLinkLable1 = Nothing
        Me.rdbAdd.MyLinkLable2 = Nothing
        Me.rdbAdd.Name = "rdbAdd"
        Me.rdbAdd.Size = New System.Drawing.Size(41, 18)
        Me.rdbAdd.TabIndex = 0
        Me.rdbAdd.Text = "Add"
        '
        'txtDescription
        '
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(114, 45)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.lblDescription
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(454, 18)
        Me.txtDescription.TabIndex = 3
        Me.txtDescription.Text = " "
        '
        'lblDescription
        '
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(13, 46)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 10
        Me.lblDescription.Text = "Description"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Enabled = False
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(87, 17)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 10
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(13, 17)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 9
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(521, 17)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        '
        'MenuRD
        '
        Me.MenuRD.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.MenuRD.Location = New System.Drawing.Point(0, 0)
        Me.MenuRD.Name = "MenuRD"
        Me.MenuRD.Size = New System.Drawing.Size(602, 20)
        Me.MenuRD.TabIndex = 1
        Me.MenuRD.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Menu"
        Me.RadMenuItem1.AccessibleName = "Menu"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Menu"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "MenuImport"
        Me.RadMenuItem2.AccessibleName = "MenuImport"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Import"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Export"
        Me.RadMenuItem3.AccessibleName = "Export"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Export"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "Exit"
        Me.RadMenuItem4.AccessibleName = "Exit"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Exit"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(602, 227)
        Me.SplitContainer1.SplitterDistance = 183
        Me.SplitContainer1.TabIndex = 2
        '
        'FrmSettlementMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(602, 247)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuRD)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FrmSettlementMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Settlement tMaster"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.ChkFinancial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTYpe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlSettlementType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSettleMentCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGLAccountDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpCalculate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCalculate.ResumeLayout(False)
        Me.grpCalculate.PerformLayout()
        CType(Me.rdbDoNothing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSubtract, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MenuRD, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents grpCalculate As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbSubtract As common.Controls.MyRadioButton
    Friend WithEvents rdbAdd As common.Controls.MyRadioButton
    Friend WithEvents rdbDoNothing As common.Controls.MyRadioButton
    Friend WithEvents lblSettleMentCode As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtGLAccountDesc As common.Controls.MyTextBox
    Friend WithEvents txtGLAccount As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MenuRD As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents fndSettleMentCode As common.UserControls.txtNavigator
    Friend WithEvents ddlSettlementType As common.Controls.MyComboBox
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents cmbType As common.Controls.MyComboBox
    Friend WithEvents lblTYpe As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents ChkFinancial As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

