<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTaskMaster
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.lblJobCode = New common.Controls.MyLabel
        Me.txtJobCode = New common.Controls.MyTextBox
        Me.lblUomName = New common.Controls.MyTextBox
        Me.txtCostTypeDesc = New common.Controls.MyTextBox
        Me.fndCostType = New common.UserControls.txtFinder
        Me.MyLabel13 = New common.Controls.MyLabel
        Me.txtUnitCost = New common.MyNumBox
        Me.lblUnitCost = New common.Controls.MyLabel
        Me.txtBillingRate = New common.MyNumBox
        Me.lblBillingRate = New common.Controls.MyLabel
        Me.fnduom = New common.UserControls.txtFinder
        Me.cboTaskType = New common.Controls.MyComboBox
        Me.lblTaskType = New common.Controls.MyLabel
        Me.lblTaskCode = New common.Controls.MyLabel
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.lblDescription = New common.Controls.MyLabel
        Me.lblCostType = New common.Controls.MyLabel
        Me.txtDesc = New common.Controls.MyTextBox
        Me.txtCode = New common.UserControls.txtNavigator
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage
        Me.UcCustomFields1 = New ucCustomFields
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblJobCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtJobCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUomName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCostTypeDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUnitCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnitCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBillingRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBillingRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTaskType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaskType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaskCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCostType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(592, 276)
        Me.SplitContainer1.SplitterDistance = 240
        Me.SplitContainer1.TabIndex = 42
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(592, 240)
        Me.RadPageView1.TabIndex = 216
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.lblJobCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtJobCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblUomName)
        Me.RadPageViewPage1.Controls.Add(Me.txtCostTypeDesc)
        Me.RadPageViewPage1.Controls.Add(Me.fndCostType)
        Me.RadPageViewPage1.Controls.Add(Me.txtUnitCost)
        Me.RadPageViewPage1.Controls.Add(Me.lblUnitCost)
        Me.RadPageViewPage1.Controls.Add(Me.txtBillingRate)
        Me.RadPageViewPage1.Controls.Add(Me.lblBillingRate)
        Me.RadPageViewPage1.Controls.Add(Me.fnduom)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.cboTaskType)
        Me.RadPageViewPage1.Controls.Add(Me.lblTaskType)
        Me.RadPageViewPage1.Controls.Add(Me.lblTaskCode)
        Me.RadPageViewPage1.Controls.Add(Me.btnnew)
        Me.RadPageViewPage1.Controls.Add(Me.lblDescription)
        Me.RadPageViewPage1.Controls.Add(Me.lblCostType)
        Me.RadPageViewPage1.Controls.Add(Me.txtDesc)
        Me.RadPageViewPage1.Controls.Add(Me.txtCode)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(571, 192)
        Me.RadPageViewPage1.Text = "Task"
        '
        'lblJobCode
        '
        Me.lblJobCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJobCode.Location = New System.Drawing.Point(4, 148)
        Me.lblJobCode.Name = "lblJobCode"
        Me.lblJobCode.Size = New System.Drawing.Size(55, 16)
        Me.lblJobCode.TabIndex = 231
        Me.lblJobCode.Text = "Job Code"
        '
        'txtJobCode
        '
        Me.txtJobCode.Location = New System.Drawing.Point(114, 148)
        Me.txtJobCode.MendatroryField = False
        Me.txtJobCode.MyLinkLable1 = Nothing
        Me.txtJobCode.MyLinkLable2 = Nothing
        Me.txtJobCode.Name = "txtJobCode"
        Me.txtJobCode.ReadOnly = True
        Me.txtJobCode.Size = New System.Drawing.Size(216, 20)
        Me.txtJobCode.TabIndex = 10
        '
        'lblUomName
        '
        Me.lblUomName.Location = New System.Drawing.Point(285, 57)
        Me.lblUomName.MendatroryField = False
        Me.lblUomName.MyLinkLable1 = Nothing
        Me.lblUomName.MyLinkLable2 = Nothing
        Me.lblUomName.Name = "lblUomName"
        Me.lblUomName.ReadOnly = True
        Me.lblUomName.Size = New System.Drawing.Size(216, 20)
        Me.lblUomName.TabIndex = 4
        Me.lblUomName.TabStop = False
        '
        'txtCostTypeDesc
        '
        Me.txtCostTypeDesc.Location = New System.Drawing.Point(285, 103)
        Me.txtCostTypeDesc.MendatroryField = False
        Me.txtCostTypeDesc.MyLinkLable1 = Nothing
        Me.txtCostTypeDesc.MyLinkLable2 = Nothing
        Me.txtCostTypeDesc.Name = "txtCostTypeDesc"
        Me.txtCostTypeDesc.ReadOnly = True
        Me.txtCostTypeDesc.Size = New System.Drawing.Size(216, 20)
        Me.txtCostTypeDesc.TabIndex = 8
        Me.txtCostTypeDesc.TabStop = False
        '
        'fndCostType
        '
        Me.fndCostType.Location = New System.Drawing.Point(114, 104)
        Me.fndCostType.MendatroryField = False
        Me.fndCostType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCostType.MyLinkLable1 = Me.MyLabel13
        Me.fndCostType.MyLinkLable2 = Nothing
        Me.fndCostType.MyReadOnly = False
        Me.fndCostType.Name = "fndCostType"
        Me.fndCostType.Size = New System.Drawing.Size(165, 19)
        Me.fndCostType.TabIndex = 7
        Me.fndCostType.Value = ""
        '
        'MyLabel13
        '
        Me.MyLabel13.Location = New System.Drawing.Point(4, 58)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel13.TabIndex = 222
        Me.MyLabel13.Text = "UOM"
        '
        'txtUnitCost
        '
        Me.txtUnitCost.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtUnitCost.DecimalPlaces = 6
        Me.txtUnitCost.Location = New System.Drawing.Point(285, 80)
        Me.txtUnitCost.MaxLength = 18
        Me.txtUnitCost.MendatroryField = True
        Me.txtUnitCost.MyLinkLable1 = Me.lblUnitCost
        Me.txtUnitCost.MyLinkLable2 = Nothing
        Me.txtUnitCost.Name = "txtUnitCost"
        Me.txtUnitCost.Size = New System.Drawing.Size(119, 20)
        Me.txtUnitCost.TabIndex = 6
        Me.txtUnitCost.Text = "0"
        Me.txtUnitCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtUnitCost.Value = 0
        '
        'lblUnitCost
        '
        Me.lblUnitCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnitCost.Location = New System.Drawing.Point(226, 82)
        Me.lblUnitCost.Name = "lblUnitCost"
        Me.lblUnitCost.Size = New System.Drawing.Size(53, 16)
        Me.lblUnitCost.TabIndex = 226
        Me.lblUnitCost.Text = "Unit Cost"
        '
        'txtBillingRate
        '
        Me.txtBillingRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtBillingRate.DecimalPlaces = 6
        Me.txtBillingRate.Location = New System.Drawing.Point(114, 80)
        Me.txtBillingRate.MaxLength = 18
        Me.txtBillingRate.MendatroryField = True
        Me.txtBillingRate.MyLinkLable1 = Me.lblBillingRate
        Me.txtBillingRate.MyLinkLable2 = Nothing
        Me.txtBillingRate.Name = "txtBillingRate"
        Me.txtBillingRate.Size = New System.Drawing.Size(108, 20)
        Me.txtBillingRate.TabIndex = 5
        Me.txtBillingRate.Text = "0"
        Me.txtBillingRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBillingRate.Value = 0
        '
        'lblBillingRate
        '
        Me.lblBillingRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillingRate.Location = New System.Drawing.Point(6, 82)
        Me.lblBillingRate.Name = "lblBillingRate"
        Me.lblBillingRate.Size = New System.Drawing.Size(63, 16)
        Me.lblBillingRate.TabIndex = 224
        Me.lblBillingRate.Text = "Billing Rate"
        '
        'fnduom
        '
        Me.fnduom.Location = New System.Drawing.Point(114, 58)
        Me.fnduom.MendatroryField = False
        Me.fnduom.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fnduom.MyLinkLable1 = Me.MyLabel13
        Me.fnduom.MyLinkLable2 = Nothing
        Me.fnduom.MyReadOnly = False
        Me.fnduom.Name = "fnduom"
        Me.fnduom.Size = New System.Drawing.Size(165, 19)
        Me.fnduom.TabIndex = 3
        Me.fnduom.Value = ""
        '
        'cboTaskType
        '
        Me.cboTaskType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboTaskType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "Open"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Approved"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "On Hold"
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Text = "Discountinued"
        RadListDataItem4.TextWrap = True
        Me.cboTaskType.Items.Add(RadListDataItem1)
        Me.cboTaskType.Items.Add(RadListDataItem2)
        Me.cboTaskType.Items.Add(RadListDataItem3)
        Me.cboTaskType.Items.Add(RadListDataItem4)
        Me.cboTaskType.Location = New System.Drawing.Point(114, 126)
        Me.cboTaskType.MendatroryField = False
        Me.cboTaskType.MyLinkLable1 = Me.lblTaskType
        Me.cboTaskType.MyLinkLable2 = Nothing
        Me.cboTaskType.Name = "cboTaskType"
        Me.cboTaskType.Size = New System.Drawing.Size(165, 18)
        Me.cboTaskType.TabIndex = 9
        '
        'lblTaskType
        '
        Me.lblTaskType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaskType.Location = New System.Drawing.Point(3, 125)
        Me.lblTaskType.Name = "lblTaskType"
        Me.lblTaskType.Size = New System.Drawing.Size(59, 16)
        Me.lblTaskType.TabIndex = 217
        Me.lblTaskType.Text = "Task Type"
        '
        'lblTaskCode
        '
        Me.lblTaskCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaskCode.Location = New System.Drawing.Point(3, 13)
        Me.lblTaskCode.Name = "lblTaskCode"
        Me.lblTaskCode.Size = New System.Drawing.Size(61, 16)
        Me.lblTaskCode.TabIndex = 37
        Me.lblTaskCode.Text = "Task Code"
        '
        'btnnew
        '
        Me.btnnew.Image = My.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(318, 11)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 21)
        Me.btnnew.TabIndex = 1
        '
        'lblDescription
        '
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(3, 33)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 36
        Me.lblDescription.Text = "Description"
        '
        'lblCostType
        '
        Me.lblCostType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostType.Location = New System.Drawing.Point(3, 103)
        Me.lblCostType.Name = "lblCostType"
        Me.lblCostType.Size = New System.Drawing.Size(58, 16)
        Me.lblCostType.TabIndex = 215
        Me.lblCostType.Text = "Cost Type"
        '
        'txtDesc
        '
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(114, 34)
        Me.txtDesc.MaxLength = 50
        Me.txtDesc.MendatroryField = True
        Me.txtDesc.MyLinkLable1 = Nothing
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        '
        '
        '
        Me.txtDesc.RootElement.StretchVertically = True
        Me.txtDesc.Size = New System.Drawing.Size(387, 20)
        Me.txtDesc.TabIndex = 2
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(114, 11)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Nothing
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(202, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.TabStop = False
        Me.txtCode.Value = ""
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(571, 192)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(571, 192)
        Me.UcCustomFields1.TabIndex = 1
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(17, 10)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(96, 10)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(515, 10)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(592, 20)
        Me.RadMenu1.TabIndex = 43
        Me.RadMenu1.Text = "RadMenu1"
        '
        'rdmenufile
        '
        Me.rdmenufile.AccessibleDescription = "RadMenuItem1"
        Me.rdmenufile.AccessibleName = "RadMenuItem1"
        Me.rdmenufile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Text = "File"
        Me.rdmenufile.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Import"
        Me.RadMenuItem1.AccessibleName = "Import"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Import"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Export"
        Me.RadMenuItem2.AccessibleName = "Export"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'frmTaskMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(592, 296)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmTaskMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Task Master"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblJobCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtJobCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUomName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCostTypeDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUnitCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnitCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBillingRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBillingRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTaskType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaskType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaskCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCostType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblTaskCode As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents lblCostType As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents cboTaskType As common.Controls.MyComboBox
    Friend WithEvents lblTaskType As common.Controls.MyLabel
    Friend WithEvents fnduom As common.UserControls.txtFinder
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtBillingRate As common.MyNumBox
    Friend WithEvents lblBillingRate As common.Controls.MyLabel
    Friend WithEvents txtUnitCost As common.MyNumBox
    Friend WithEvents lblUnitCost As common.Controls.MyLabel
    Friend WithEvents fndCostType As common.UserControls.txtFinder
    Friend WithEvents lblUomName As common.Controls.MyTextBox
    Friend WithEvents txtCostTypeDesc As common.Controls.MyTextBox
    Friend WithEvents txtJobCode As common.Controls.MyTextBox
    Friend WithEvents lblJobCode As common.Controls.MyLabel
    Friend WithEvents UcCustomFields1 As ucCustomFields
End Class

