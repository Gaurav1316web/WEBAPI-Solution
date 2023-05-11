Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmHouseRentDeclaration
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtFinYear = New common.UserControls.txtFinder()
        Me.TxtEmpCode = New common.UserControls.txtFinder()
        Me.TxtPayPeriod = New common.UserControls.txtFinder()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.txtcode = New common.UserControls.txtNavigator()
        Me.lblEmpName = New common.Controls.MyLabel()
        Me.lblPayName = New common.Controls.MyLabel()
        Me.txtHouseRentAmt = New common.MyNumBox()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel84 = New common.Controls.MyLabel()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.TxtDesp = New common.Controls.MyTextBox()
        Me.lblItemCategoryCode = New common.Controls.MyLabel()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHouseRentAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel84, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDesp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(534, 399)
        Me.SplitContainer1.SplitterDistance = 355
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(534, 355)
        Me.RadPageView1.TabIndex = 319
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtFinYear)
        Me.RadPageViewPage1.Controls.Add(Me.TxtEmpCode)
        Me.RadPageViewPage1.Controls.Add(Me.TxtPayPeriod)
        Me.RadPageViewPage1.Controls.Add(Me.btnreset)
        Me.RadPageViewPage1.Controls.Add(Me.txtcode)
        Me.RadPageViewPage1.Controls.Add(Me.lblEmpName)
        Me.RadPageViewPage1.Controls.Add(Me.lblPayName)
        Me.RadPageViewPage1.Controls.Add(Me.txtHouseRentAmt)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel84)
        Me.RadPageViewPage1.Controls.Add(Me.lblDescription)
        Me.RadPageViewPage1.Controls.Add(Me.TxtDesp)
        Me.RadPageViewPage1.Controls.Add(Me.lblItemCategoryCode)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(51.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(513, 307)
        Me.RadPageViewPage1.Text = "Master"
        '
        'txtFinYear
        '
        Me.txtFinYear.Location = New System.Drawing.Point(123, 51)
        Me.txtFinYear.MendatroryField = True
        Me.txtFinYear.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFinYear.MyLinkLable1 = Nothing
        Me.txtFinYear.MyLinkLable2 = Nothing
        Me.txtFinYear.MyReadOnly = False
        Me.txtFinYear.MyShowMasterFormButton = False
        Me.txtFinYear.Name = "txtFinYear"
        Me.txtFinYear.Size = New System.Drawing.Size(186, 20)
        Me.txtFinYear.TabIndex = 303
        Me.txtFinYear.Value = ""
        '
        'TxtEmpCode
        '
        Me.TxtEmpCode.Location = New System.Drawing.Point(123, 98)
        Me.TxtEmpCode.MendatroryField = True
        Me.TxtEmpCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEmpCode.MyLinkLable1 = Nothing
        Me.TxtEmpCode.MyLinkLable2 = Nothing
        Me.TxtEmpCode.MyReadOnly = False
        Me.TxtEmpCode.MyShowMasterFormButton = False
        Me.TxtEmpCode.Name = "TxtEmpCode"
        Me.TxtEmpCode.Size = New System.Drawing.Size(186, 20)
        Me.TxtEmpCode.TabIndex = 302
        Me.TxtEmpCode.Value = ""
        '
        'TxtPayPeriod
        '
        Me.TxtPayPeriod.Location = New System.Drawing.Point(123, 74)
        Me.TxtPayPeriod.MendatroryField = True
        Me.TxtPayPeriod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPayPeriod.MyLinkLable1 = Nothing
        Me.TxtPayPeriod.MyLinkLable2 = Nothing
        Me.TxtPayPeriod.MyReadOnly = False
        Me.TxtPayPeriod.MyShowMasterFormButton = False
        Me.TxtPayPeriod.Name = "TxtPayPeriod"
        Me.TxtPayPeriod.Size = New System.Drawing.Size(186, 20)
        Me.TxtPayPeriod.TabIndex = 301
        Me.TxtPayPeriod.Value = ""
        '
        'btnreset
        '
        Me.btnreset.Image = My.Resources.Resources._new
        Me.btnreset.Location = New System.Drawing.Point(397, 6)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(15, 21)
        Me.btnreset.TabIndex = 234
        '
        'txtcode
        '
        Me.txtcode.Location = New System.Drawing.Point(123, 6)
        Me.txtcode.MendatroryField = True
        Me.txtcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcode.MyLinkLable1 = Nothing
        Me.txtcode.MyLinkLable2 = Nothing
        Me.txtcode.MyMaxLength = 32767
        Me.txtcode.MyReadOnly = False
        Me.txtcode.Name = "txtcode"
        Me.txtcode.Size = New System.Drawing.Size(274, 21)
        Me.txtcode.TabIndex = 233
        Me.txtcode.Value = ""
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblEmpName.Location = New System.Drawing.Point(314, 98)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(188, 20)
        Me.lblEmpName.TabIndex = 232
        Me.lblEmpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPayName
        '
        Me.lblPayName.AutoSize = False
        Me.lblPayName.BorderVisible = True
        Me.lblPayName.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblPayName.Location = New System.Drawing.Point(314, 76)
        Me.lblPayName.Name = "lblPayName"
        Me.lblPayName.Size = New System.Drawing.Size(188, 20)
        Me.lblPayName.TabIndex = 231
        Me.lblPayName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtHouseRentAmt
        '
        Me.txtHouseRentAmt.BackColor = System.Drawing.Color.White
        Me.txtHouseRentAmt.DecimalPlaces = 2
        Me.txtHouseRentAmt.Location = New System.Drawing.Point(123, 120)
        Me.txtHouseRentAmt.MaxLength = 10
        Me.txtHouseRentAmt.MendatroryField = False
        Me.txtHouseRentAmt.MyLinkLable1 = Me.RadLabel8
        Me.txtHouseRentAmt.MyLinkLable2 = Nothing
        Me.txtHouseRentAmt.Name = "txtHouseRentAmt"
        Me.txtHouseRentAmt.Size = New System.Drawing.Size(186, 20)
        Me.txtHouseRentAmt.TabIndex = 6
        Me.txtHouseRentAmt.Text = "0"
        Me.txtHouseRentAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtHouseRentAmt.Value = 0.0R
        '
        'RadLabel8
        '
        Me.RadLabel8.Location = New System.Drawing.Point(9, 122)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(111, 18)
        Me.RadLabel8.TabIndex = 229
        Me.RadLabel8.Text = "House Rent Amount "
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(9, 100)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(90, 16)
        Me.MyLabel2.TabIndex = 227
        Me.MyLabel2.Text = "Employee Code "
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(9, 78)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(95, 16)
        Me.MyLabel1.TabIndex = 225
        Me.MyLabel1.Text = "Pay Period Code "
        '
        'MyLabel84
        '
        Me.MyLabel84.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel84.Location = New System.Drawing.Point(9, 55)
        Me.MyLabel84.Name = "MyLabel84"
        Me.MyLabel84.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel84.TabIndex = 223
        Me.MyLabel84.Text = "Financial Year "
        '
        'lblDescription
        '
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(9, 34)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(66, 16)
        Me.lblDescription.TabIndex = 222
        Me.lblDescription.Text = "Description "
        '
        'TxtDesp
        '
        Me.TxtDesp.AutoSize = False
        Me.TxtDesp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDesp.Location = New System.Drawing.Point(123, 28)
        Me.TxtDesp.MaxLength = 100
        Me.TxtDesp.MendatroryField = False
        Me.TxtDesp.Multiline = True
        Me.TxtDesp.MyLinkLable1 = Me.lblDescription
        Me.TxtDesp.MyLinkLable2 = Nothing
        Me.TxtDesp.Name = "TxtDesp"
        Me.TxtDesp.Size = New System.Drawing.Size(274, 21)
        Me.TxtDesp.TabIndex = 2
        Me.TxtDesp.Text = " "
        '
        'lblItemCategoryCode
        '
        Me.lblItemCategoryCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblItemCategoryCode.Location = New System.Drawing.Point(9, 11)
        Me.lblItemCategoryCode.Name = "lblItemCategoryCode"
        Me.lblItemCategoryCode.Size = New System.Drawing.Size(36, 16)
        Me.lblItemCategoryCode.TabIndex = 221
        Me.lblItemCategoryCode.Text = "Code "
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 28.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 37)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(545, 196)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(545, 196)
        Me.UcAttachment1.TabIndex = 1
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(19, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 21)
        Me.btnsave.TabIndex = 10
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(454, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 12
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(90, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 11
        Me.btndelete.Text = "Delete"
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(534, 20)
        Me.RadMenu2.TabIndex = 66
        Me.RadMenu2.Text = "RadMenu2"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "File"
        Me.RadMenuItem3.AccessibleName = "File"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmImport, Me.Export})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'rmImport
        '
        Me.rmImport.AccessibleDescription = "Import"
        Me.rmImport.AccessibleName = "Import"
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
        '
        'Export
        '
        Me.Export.AccessibleDescription = "Export"
        Me.Export.AccessibleName = "Export"
        Me.Export.Name = "Export"
        Me.Export.Text = "Export"
        '
        'FrmHouseRentDeclaration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(534, 419)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu2)
        Me.Name = "FrmHouseRentDeclaration"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmHouseRentDeclaration"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHouseRentAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel84, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDesp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents lblPayName As common.Controls.MyLabel
    Friend WithEvents txtHouseRentAmt As common.MyNumBox
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel84 As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents TxtDesp As common.Controls.MyTextBox
    Friend WithEvents lblItemCategoryCode As common.Controls.MyLabel
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtcode As common.UserControls.txtNavigator
    Friend WithEvents TxtEmpCode As common.UserControls.txtFinder
    Friend WithEvents TxtPayPeriod As common.UserControls.txtFinder
    Friend WithEvents txtFinYear As common.UserControls.txtFinder
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
End Class

