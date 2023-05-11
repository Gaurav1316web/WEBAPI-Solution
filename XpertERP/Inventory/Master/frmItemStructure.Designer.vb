<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmItemStructure
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
        Dim GridViewComboBoxColumn3 As Telerik.WinControls.UI.GridViewComboBoxColumn = New Telerik.WinControls.UI.GridViewComboBoxColumn()
        Dim GridViewDecimalColumn3 As Telerik.WinControls.UI.GridViewDecimalColumn = New Telerik.WinControls.UI.GridViewDecimalColumn()
        Me.RadThemeManager1 = New Telerik.WinControls.RadThemeManager()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnclose = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.fndstructurecode = New common.UserControls.txtNavigator()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.chkdefaultstructurecode = New Telerik.WinControls.UI.RadCheckBox()
        Me.dgcaccountstrucuture = New common.UserControls.MyRadGridView()
        Me.txtlength = New common.Controls.MyTextBox()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.txtitemstructure = New common.Controls.MyTextBox()
        Me.txtdesc = New common.Controls.MyTextBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkdefaultstructurecode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgcaccountstrucuture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgcaccountstrucuture.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtlength, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtitemstructure, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.pvpCustomFields.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnimport, Me.mnexport, Me.mnclose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'mnimport
        '
        Me.mnimport.AccessibleDescription = "Import"
        Me.mnimport.AccessibleName = "Import"
        Me.mnimport.Name = "mnimport"
        Me.mnimport.Text = "Import"
        '
        'mnexport
        '
        Me.mnexport.AccessibleDescription = "Export"
        Me.mnexport.AccessibleName = "Export"
        Me.mnexport.Name = "mnexport"
        Me.mnexport.Text = "Export"
        '
        'mnclose
        '
        Me.mnclose.AccessibleDescription = "Close"
        Me.mnclose.AccessibleName = "Close"
        Me.mnclose.Name = "mnclose"
        Me.mnclose.Text = "Close"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(497, 20)
        Me.RadMenu1.TabIndex = 14
        Me.RadMenu1.Text = "File"
        '
        'fndstructurecode
        '
        Me.fndstructurecode.Location = New System.Drawing.Point(92, 10)
        Me.fndstructurecode.MendatroryField = True
        Me.fndstructurecode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndstructurecode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndstructurecode.MyLinkLable1 = Me.RadLabel1
        Me.fndstructurecode.MyLinkLable2 = Nothing
        Me.fndstructurecode.MyMaxLength = 32767
        Me.fndstructurecode.MyReadOnly = False
        Me.fndstructurecode.Name = "fndstructurecode"
        Me.fndstructurecode.Size = New System.Drawing.Size(156, 20)
        Me.fndstructurecode.TabIndex = 0
        Me.fndstructurecode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(3, 12)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(82, 16)
        Me.RadLabel1.TabIndex = 9
        Me.RadLabel1.Text = "Structure Code"
        '
        'btnreset
        '
        Me.btnreset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnreset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnreset.Location = New System.Drawing.Point(248, 10)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(22, 20)
        Me.btnreset.TabIndex = 1
        '
        'chkdefaultstructurecode
        '
        Me.chkdefaultstructurecode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkdefaultstructurecode.Location = New System.Drawing.Point(282, 12)
        Me.chkdefaultstructurecode.Name = "chkdefaultstructurecode"
        Me.chkdefaultstructurecode.Size = New System.Drawing.Size(174, 16)
        Me.chkdefaultstructurecode.TabIndex = 2
        Me.chkdefaultstructurecode.Text = "Use as Default Structure Code"
        '
        'dgcaccountstrucuture
        '
        Me.dgcaccountstrucuture.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgcaccountstrucuture.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgcaccountstrucuture.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.dgcaccountstrucuture.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dgcaccountstrucuture.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgcaccountstrucuture.Location = New System.Drawing.Point(3, 96)
        '
        'dgcaccountstrucuture
        '
        Me.dgcaccountstrucuture.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GridViewComboBoxColumn3.HeaderText = "Class"
        GridViewComboBoxColumn3.Name = "column1"
        GridViewComboBoxColumn3.Width = 150
        GridViewDecimalColumn3.HeaderText = "Class Length"
        GridViewDecimalColumn3.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        GridViewDecimalColumn3.Name = "column2"
        GridViewDecimalColumn3.ReadOnly = True
        GridViewDecimalColumn3.Width = 265
        Me.dgcaccountstrucuture.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewComboBoxColumn3, GridViewDecimalColumn3})
        Me.dgcaccountstrucuture.MasterTemplate.EnableGrouping = False
        Me.dgcaccountstrucuture.Name = "dgcaccountstrucuture"
        Me.dgcaccountstrucuture.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgcaccountstrucuture.Size = New System.Drawing.Size(438, 232)
        Me.dgcaccountstrucuture.TabIndex = 6
        Me.dgcaccountstrucuture.TabStop = False
        Me.dgcaccountstrucuture.Text = "                  "
        '
        'txtlength
        '
        Me.txtlength.Location = New System.Drawing.Point(402, 60)
        Me.txtlength.MendatroryField = False
        Me.txtlength.MyLinkLable1 = Me.RadLabel4
        Me.txtlength.MyLinkLable2 = Nothing
        Me.txtlength.Name = "txtlength"
        Me.txtlength.ReadOnly = True
        Me.txtlength.Size = New System.Drawing.Size(39, 20)
        Me.txtlength.TabIndex = 5
        Me.txtlength.TabStop = False
        '
        'RadLabel4
        '
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(327, 60)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(69, 16)
        Me.RadLabel4.TabIndex = 6
        Me.RadLabel4.Text = "Total Length"
        '
        'RadLabel3
        '
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(3, 60)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(77, 16)
        Me.RadLabel3.TabIndex = 7
        Me.RadLabel3.Text = "Item Structure"
        '
        'txtitemstructure
        '
        Me.txtitemstructure.Location = New System.Drawing.Point(92, 60)
        Me.txtitemstructure.MendatroryField = False
        Me.txtitemstructure.MyLinkLable1 = Me.RadLabel3
        Me.txtitemstructure.MyLinkLable2 = Nothing
        Me.txtitemstructure.Name = "txtitemstructure"
        Me.txtitemstructure.ReadOnly = True
        Me.txtitemstructure.Size = New System.Drawing.Size(229, 20)
        Me.txtitemstructure.TabIndex = 4
        Me.txtitemstructure.TabStop = False
        '
        'txtdesc
        '
        Me.txtdesc.Location = New System.Drawing.Point(92, 34)
        Me.txtdesc.MendatroryField = False
        Me.txtdesc.MyLinkLable1 = Me.RadLabel2
        Me.txtdesc.MyLinkLable2 = Nothing
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.Size = New System.Drawing.Size(349, 20)
        Me.txtdesc.TabIndex = 3
        '
        'RadLabel2
        '
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(3, 34)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel2.TabIndex = 8
        Me.RadLabel2.Text = "Description"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(420, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(73, 3)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(68, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(3, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(497, 416)
        Me.SplitContainer1.SplitterDistance = 380
        Me.SplitContainer1.TabIndex = 15
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(497, 380)
        Me.RadPageView1.TabIndex = 217
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.dgcaccountstrucuture)
        Me.RadPageViewPage1.Controls.Add(Me.fndstructurecode)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.btnreset)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.chkdefaultstructurecode)
        Me.RadPageViewPage1.Controls.Add(Me.txtdesc)
        Me.RadPageViewPage1.Controls.Add(Me.txtitemstructure)
        Me.RadPageViewPage1.Controls.Add(Me.txtlength)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(55.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(476, 332)
        Me.RadPageViewPage1.Text = "General"
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(86.0!, 28.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(476, 332)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(476, 332)
        Me.UcCustomFields1.TabIndex = 2
        '
        'frmItemStructure
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(497, 436)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmItemStructure"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Item Structure"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkdefaultstructurecode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgcaccountstrucuture.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgcaccountstrucuture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtlength, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtitemstructure, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        Me.pvpCustomFields.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents RadThemeManager1 As Telerik.WinControls.RadThemeManager
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnclose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkdefaultstructurecode As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents dgcaccountstrucuture As common.UserControls.MyRadGridView
    Friend WithEvents txtlength As common.Controls.MyTextBox
    Friend WithEvents txtitemstructure As common.Controls.MyTextBox
    Friend WithEvents txtdesc As common.Controls.MyTextBox
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents fndstructurecode As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
End Class

