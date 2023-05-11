Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPriceComponantMaster
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
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.txtDesc = New common.Controls.MyTextBox
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.txtComponentCode = New common.UserControls.txtNavigator
        Me.ddlSerialNumber = New common.Controls.MyComboBox
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.chktpt = New Telerik.WinControls.UI.RadCheckBox
        Me.txtGLAccountcc = New common.UserControls.txtFinder
        Me.lblglaccount = New common.Controls.MyLabel
        Me.lblglaccdescription = New common.Controls.MyLabel
        Me.chkGLAccountApplicable = New Telerik.WinControls.UI.RadCheckBox
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        Me.MenuFile = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.Importmenu = New Telerik.WinControls.UI.RadMenuItem
        Me.Exportmenu = New Telerik.WinControls.UI.RadMenuItem
        Me.exitmenu = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gbGLAccount = New System.Windows.Forms.GroupBox
        Me.lblAccountDesc = New common.Controls.MyLabel
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage
        Me.UcCustomFields1 = New ucCustomFields
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlSerialNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chktpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblglaccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblglaccdescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGLAccountApplicable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MenuFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.gbGLAccount.SuspendLayout()
        CType(Me.lblAccountDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.RadLabel1.Location = New System.Drawing.Point(3, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(95, 16)
        Me.RadLabel1.TabIndex = 8
        Me.RadLabel1.Text = "Component Code"
        '
        'RadLabel2
        '
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(3, 25)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel2.TabIndex = 9
        Me.RadLabel2.Text = "Description"
        '
        'txtDesc
        '
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(121, 25)
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel2
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(555, 18)
        Me.txtDesc.TabIndex = 2
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnReset.Location = New System.Drawing.Point(329, 3)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(18, 18)
        Me.btnReset.TabIndex = 1
        '
        'btnDelete
        '
        Me.btnDelete.AccessibleDescription = ""
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(71, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(66, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'txtComponentCode
        '
        Me.txtComponentCode.Location = New System.Drawing.Point(121, 3)
        Me.txtComponentCode.MendatroryField = True
        Me.txtComponentCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtComponentCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtComponentCode.MyLinkLable1 = Me.RadLabel1
        Me.txtComponentCode.MyLinkLable2 = Nothing
        Me.txtComponentCode.MyMaxLength = 32767
        Me.txtComponentCode.MyReadOnly = False
        Me.txtComponentCode.Name = "txtComponentCode"
        Me.txtComponentCode.Size = New System.Drawing.Size(202, 18)
        Me.txtComponentCode.TabIndex = 0
        Me.txtComponentCode.Value = ""
        '
        'ddlSerialNumber
        '
        Me.ddlSerialNumber.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlSerialNumber.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlSerialNumber.Location = New System.Drawing.Point(121, 47)
        Me.ddlSerialNumber.MendatroryField = False
        Me.ddlSerialNumber.MyLinkLable1 = Me.RadLabel3
        Me.ddlSerialNumber.MyLinkLable2 = Nothing
        Me.ddlSerialNumber.Name = "ddlSerialNumber"
        Me.ddlSerialNumber.Size = New System.Drawing.Size(61, 18)
        Me.ddlSerialNumber.TabIndex = 3
        '
        'RadLabel3
        '
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(3, 47)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(79, 16)
        Me.RadLabel3.TabIndex = 10
        Me.RadLabel3.Text = "Serial Number"
        '
        'chktpt
        '
        Me.chktpt.Location = New System.Drawing.Point(340, 47)
        Me.chktpt.Name = "chktpt"
        Me.chktpt.Size = New System.Drawing.Size(65, 18)
        Me.chktpt.TabIndex = 5
        Me.chktpt.Text = "Type TPT"
        '
        'txtGLAccountcc
        '
        Me.txtGLAccountcc.Location = New System.Drawing.Point(124, 10)
        Me.txtGLAccountcc.MendatroryField = False
        Me.txtGLAccountcc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGLAccountcc.MyLinkLable1 = Me.lblglaccount
        Me.txtGLAccountcc.MyLinkLable2 = Nothing
        Me.txtGLAccountcc.MyReadOnly = False
        Me.txtGLAccountcc.Name = "txtGLAccountcc"
        Me.txtGLAccountcc.Size = New System.Drawing.Size(143, 18)
        Me.txtGLAccountcc.TabIndex = 0
        Me.txtGLAccountcc.Value = ""
        '
        'lblglaccount
        '
        Me.lblglaccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblglaccount.Location = New System.Drawing.Point(6, 10)
        Me.lblglaccount.Name = "lblglaccount"
        Me.lblglaccount.Size = New System.Drawing.Size(65, 16)
        Me.lblglaccount.TabIndex = 2
        Me.lblglaccount.Text = "GL Account"
        '
        'lblglaccdescription
        '
        Me.lblglaccdescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblglaccdescription.Location = New System.Drawing.Point(6, 34)
        Me.lblglaccdescription.Name = "lblglaccdescription"
        Me.lblglaccdescription.Size = New System.Drawing.Size(85, 16)
        Me.lblglaccdescription.TabIndex = 3
        Me.lblglaccdescription.Text = "A/C Descriptoin"
        '
        'chkGLAccountApplicable
        '
        Me.chkGLAccountApplicable.Location = New System.Drawing.Point(201, 47)
        Me.chkGLAccountApplicable.Name = "chkGLAccountApplicable"
        Me.chkGLAccountApplicable.Size = New System.Drawing.Size(133, 18)
        Me.chkGLAccountApplicable.TabIndex = 4
        Me.chkGLAccountApplicable.Text = "GL Account Applicable"
        '
        'RadButton1
        '
        Me.RadButton1.AccessibleDescription = ""
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(631, 3)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(66, 18)
        Me.RadButton1.TabIndex = 2
        Me.RadButton1.Text = "Close"
        '
        'MenuFile
        '
        Me.MenuFile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.MenuFile.Location = New System.Drawing.Point(0, 0)
        Me.MenuFile.Name = "MenuFile"
        Me.MenuFile.Size = New System.Drawing.Size(700, 20)
        Me.MenuFile.TabIndex = 0
        Me.MenuFile.Text = "File"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "FIleMenu"
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Importmenu, Me.Exportmenu, Me.exitmenu})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'Importmenu
        '
        Me.Importmenu.AccessibleDescription = "Import"
        Me.Importmenu.AccessibleName = "Import"
        Me.Importmenu.Name = "Importmenu"
        Me.Importmenu.Text = "Import"
        Me.Importmenu.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'Exportmenu
        '
        Me.Exportmenu.AccessibleDescription = "Export"
        Me.Exportmenu.AccessibleName = "Export"
        Me.Exportmenu.Name = "Exportmenu"
        Me.Exportmenu.Text = "Export"
        Me.Exportmenu.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'exitmenu
        '
        Me.exitmenu.AccessibleDescription = "Exit"
        Me.exitmenu.AccessibleName = "Exit"
        Me.exitmenu.Name = "exitmenu"
        Me.exitmenu.Text = "Exit"
        Me.exitmenu.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Size = New System.Drawing.Size(700, 293)
        Me.SplitContainer1.SplitterDistance = 264
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(700, 264)
        Me.RadPageView1.TabIndex = 122
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.gbGLAccount)
        Me.RadPageViewPage1.Controls.Add(Me.chkGLAccountApplicable)
        Me.RadPageViewPage1.Controls.Add(Me.txtComponentCode)
        Me.RadPageViewPage1.Controls.Add(Me.ddlSerialNumber)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.txtDesc)
        Me.RadPageViewPage1.Controls.Add(Me.chktpt)
        Me.RadPageViewPage1.Controls.Add(Me.btnReset)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(679, 216)
        Me.RadPageViewPage1.Text = "Customer"
        '
        'gbGLAccount
        '
        Me.gbGLAccount.Controls.Add(Me.lblglaccount)
        Me.gbGLAccount.Controls.Add(Me.lblAccountDesc)
        Me.gbGLAccount.Controls.Add(Me.lblglaccdescription)
        Me.gbGLAccount.Controls.Add(Me.txtGLAccountcc)
        Me.gbGLAccount.Location = New System.Drawing.Point(3, 67)
        Me.gbGLAccount.Name = "gbGLAccount"
        Me.gbGLAccount.Size = New System.Drawing.Size(673, 59)
        Me.gbGLAccount.TabIndex = 609
        Me.gbGLAccount.TabStop = False
        '
        'lblAccountDesc
        '
        Me.lblAccountDesc.AutoSize = False
        Me.lblAccountDesc.BorderVisible = True
        Me.lblAccountDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccountDesc.Location = New System.Drawing.Point(124, 33)
        Me.lblAccountDesc.Name = "lblAccountDesc"
        Me.lblAccountDesc.Size = New System.Drawing.Size(543, 18)
        Me.lblAccountDesc.TabIndex = 608
        Me.lblAccountDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(1027, 353)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(1027, 353)
        Me.UcCustomFields1.TabIndex = 1
        '
        'FrmPriceComponantMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(700, 313)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuFile)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FrmPriceComponantMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Price Component Master"
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlSerialNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chktpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblglaccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblglaccdescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGLAccountApplicable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MenuFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        Me.gbGLAccount.ResumeLayout(False)
        Me.gbGLAccount.PerformLayout()
        CType(Me.lblAccountDesc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkGLAccountApplicable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MenuFile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Importmenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Exportmenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents exitmenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chktpt As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents ddlSerialNumber As common.Controls.MyComboBox
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents lblglaccdescription As common.Controls.MyLabel
    Friend WithEvents lblglaccount As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents txtComponentCode As common.UserControls.txtNavigator
    Friend WithEvents txtGLAccountcc As common.UserControls.txtFinder
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ucCustomFields
    Friend WithEvents lblAccountDesc As common.Controls.MyLabel
    Friend WithEvents gbGLAccount As System.Windows.Forms.GroupBox
End Class

