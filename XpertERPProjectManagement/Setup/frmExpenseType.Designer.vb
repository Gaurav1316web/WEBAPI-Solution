<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmExpenseType
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
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtComments = New Telerik.WinControls.UI.RadTextBox
        Me.fndAcctCode = New common.UserControls.txtFinder
        Me.WIP = New common.Controls.MyLabel
        Me.chkIntegrate = New Telerik.WinControls.UI.RadCheckBox
        Me.txtAcctDesc = New common.Controls.MyTextBox
        Me.lblCostTypeCode = New common.Controls.MyLabel
        Me.ddlExpenseType = New common.Controls.MyComboBox
        Me.lblJobType = New common.Controls.MyLabel
        Me.lblDescription = New common.Controls.MyLabel
        Me.txtDesc = New common.Controls.MyTextBox
        Me.txtCode = New common.UserControls.txtNavigator
        Me.btnnew = New Telerik.WinControls.UI.RadButton
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
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WIP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIntegrate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAcctDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCostTypeCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlExpenseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblJobType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Size = New System.Drawing.Size(663, 260)
        Me.SplitContainer1.SplitterDistance = 224
        Me.SplitContainer1.TabIndex = 44
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(663, 224)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtComments)
        Me.RadPageViewPage1.Controls.Add(Me.fndAcctCode)
        Me.RadPageViewPage1.Controls.Add(Me.chkIntegrate)
        Me.RadPageViewPage1.Controls.Add(Me.txtAcctDesc)
        Me.RadPageViewPage1.Controls.Add(Me.WIP)
        Me.RadPageViewPage1.Controls.Add(Me.lblCostTypeCode)
        Me.RadPageViewPage1.Controls.Add(Me.ddlExpenseType)
        Me.RadPageViewPage1.Controls.Add(Me.lblDescription)
        Me.RadPageViewPage1.Controls.Add(Me.lblJobType)
        Me.RadPageViewPage1.Controls.Add(Me.txtDesc)
        Me.RadPageViewPage1.Controls.Add(Me.txtCode)
        Me.RadPageViewPage1.Controls.Add(Me.btnnew)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(642, 176)
        Me.RadPageViewPage1.Text = "Job"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(3, 104)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel2.TabIndex = 243
        Me.MyLabel2.Text = "Comments"
        '
        'txtComments
        '
        Me.txtComments.AutoSize = False
        Me.txtComments.Location = New System.Drawing.Point(113, 104)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(487, 61)
        Me.txtComments.TabIndex = 7
        '
        'fndAcctCode
        '
        Me.fndAcctCode.Location = New System.Drawing.Point(114, 79)
        Me.fndAcctCode.MendatroryField = False
        Me.fndAcctCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndAcctCode.MyLinkLable1 = Me.WIP
        Me.fndAcctCode.MyLinkLable2 = Nothing
        Me.fndAcctCode.MyReadOnly = False
        Me.fndAcctCode.Name = "fndAcctCode"
        Me.fndAcctCode.Size = New System.Drawing.Size(143, 19)
        Me.fndAcctCode.TabIndex = 5
        Me.fndAcctCode.Value = ""
        '
        'WIP
        '
        Me.WIP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WIP.Location = New System.Drawing.Point(2, 79)
        Me.WIP.Name = "WIP"
        Me.WIP.Size = New System.Drawing.Size(77, 16)
        Me.WIP.TabIndex = 14
        Me.WIP.Text = "Account Code"
        '
        'chkIntegrate
        '
        Me.chkIntegrate.CheckAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.chkIntegrate.Location = New System.Drawing.Point(339, 57)
        Me.chkIntegrate.Name = "chkIntegrate"
        Me.chkIntegrate.Size = New System.Drawing.Size(107, 18)
        Me.chkIntegrate.TabIndex = 4
        Me.chkIntegrate.Text = "Integrate with AP"
        Me.chkIntegrate.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtAcctDesc
        '
        Me.txtAcctDesc.Location = New System.Drawing.Point(263, 79)
        Me.txtAcctDesc.MendatroryField = False
        Me.txtAcctDesc.MyLinkLable1 = Me.WIP
        Me.txtAcctDesc.MyLinkLable2 = Nothing
        Me.txtAcctDesc.Name = "txtAcctDesc"
        Me.txtAcctDesc.ReadOnly = True
        Me.txtAcctDesc.Size = New System.Drawing.Size(337, 20)
        Me.txtAcctDesc.TabIndex = 6
        Me.txtAcctDesc.TabStop = False
        '
        'lblCostTypeCode
        '
        Me.lblCostTypeCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostTypeCode.Location = New System.Drawing.Point(3, 13)
        Me.lblCostTypeCode.Name = "lblCostTypeCode"
        Me.lblCostTypeCode.Size = New System.Drawing.Size(80, 16)
        Me.lblCostTypeCode.TabIndex = 37
        Me.lblCostTypeCode.Text = "Expense Code"
        '
        'ddlExpenseType
        '
        Me.ddlExpenseType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlExpenseType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "Open"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Approved"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "On Hold"
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Text = "Discountinued"
        RadListDataItem4.TextWrap = True
        Me.ddlExpenseType.Items.Add(RadListDataItem1)
        Me.ddlExpenseType.Items.Add(RadListDataItem2)
        Me.ddlExpenseType.Items.Add(RadListDataItem3)
        Me.ddlExpenseType.Items.Add(RadListDataItem4)
        Me.ddlExpenseType.Location = New System.Drawing.Point(114, 58)
        Me.ddlExpenseType.MendatroryField = True
        Me.ddlExpenseType.MyLinkLable1 = Me.lblJobType
        Me.ddlExpenseType.MyLinkLable2 = Nothing
        Me.ddlExpenseType.Name = "ddlExpenseType"
        Me.ddlExpenseType.Size = New System.Drawing.Size(219, 18)
        Me.ddlExpenseType.TabIndex = 3
        '
        'lblJobType
        '
        Me.lblJobType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJobType.Location = New System.Drawing.Point(3, 57)
        Me.lblJobType.Name = "lblJobType"
        Me.lblJobType.Size = New System.Drawing.Size(79, 16)
        Me.lblJobType.TabIndex = 215
        Me.lblJobType.Text = "Expense Type"
        '
        'lblDescription
        '
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(3, 35)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 36
        Me.lblDescription.Text = "Description"
        '
        'txtDesc
        '
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(114, 35)
        Me.txtDesc.MaxLength = 50
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Nothing
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        '
        '
        '
        Me.txtDesc.RootElement.StretchVertically = True
        Me.txtDesc.Size = New System.Drawing.Size(290, 20)
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
        'btnnew
        '
        Me.btnnew.Image = My.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(318, 11)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 21)
        Me.btnnew.TabIndex = 1
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(500, 159)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(500, 159)
        Me.UcCustomFields1.TabIndex = 2
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
        Me.btnclose.Location = New System.Drawing.Point(586, 10)
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
        Me.RadMenu1.Size = New System.Drawing.Size(663, 20)
        Me.RadMenu1.TabIndex = 0
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
        'FrmExpenseType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(663, 280)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmExpenseType"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Expense Type"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WIP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIntegrate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAcctDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCostTypeCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlExpenseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblJobType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents chkIntegrate As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblCostTypeCode As common.Controls.MyLabel
    Friend WithEvents ddlExpenseType As common.Controls.MyComboBox
    Friend WithEvents lblJobType As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ucCustomFields
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents fndAcctCode As common.UserControls.txtFinder
    Friend WithEvents WIP As common.Controls.MyLabel
    Friend WithEvents txtAcctDesc As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtComments As Telerik.WinControls.UI.RadTextBox
End Class

