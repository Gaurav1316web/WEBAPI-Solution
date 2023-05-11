<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCommissionMaster
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
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.fndItemCode = New common.UserControls.txtNavigator
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnReplicate = New Telerik.WinControls.UI.RadButton
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.ddlhier = New Telerik.WinControls.UI.RadDropDownList
        Me.gpcustgroup = New Telerik.WinControls.UI.RadGroupBox
        Me.rgvCustGrp = New common.UserControls.MyRadGridView
        Me.fndunit = New common.UserControls.txtFinder
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.txtitemdesc = New common.Controls.MyTextBox
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.menuImport = New Telerik.WinControls.UI.RadMenuItem
        Me.menuExport = New Telerik.WinControls.UI.RadMenuItem
        Me.menuClose = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReplicate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlhier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gpcustgroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpcustgroup.SuspendLayout()
        CType(Me.rgvCustGrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgvCustGrp.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtitemdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.fndItemCode)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.ddlhier)
        Me.RadGroupBox1.Controls.Add(Me.gpcustgroup)
        Me.RadGroupBox1.Controls.Add(Me.fndunit)
        Me.RadGroupBox1.Controls.Add(Me.btnAddNew)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtitemdesc)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.FooterImageIndex = -1
        Me.RadGroupBox1.FooterImageKey = ""
        Me.RadGroupBox1.HeaderImageIndex = -1
        Me.RadGroupBox1.HeaderImageKey = ""
        Me.RadGroupBox1.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox1.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(640, 333)
        Me.RadGroupBox1.TabIndex = 0
        '
        'fndItemCode
        '
        Me.fndItemCode.Location = New System.Drawing.Point(84, 7)
        Me.fndItemCode.MendatroryField = True
        Me.fndItemCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndItemCode.MyLinkLable1 = Me.RadLabel1
        Me.fndItemCode.MyLinkLable2 = Nothing
        Me.fndItemCode.MyMaxLength = 32767
        Me.fndItemCode.MyReadOnly = False
        Me.fndItemCode.Name = "fndItemCode"
        Me.fndItemCode.Size = New System.Drawing.Size(202, 21)
        Me.fndItemCode.TabIndex = 0
        Me.fndItemCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.RadLabel1.Location = New System.Drawing.Point(6, 8)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(61, 18)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "Item Code"
        '
        'btnReplicate
        '
        Me.btnReplicate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReplicate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReplicate.Location = New System.Drawing.Point(230, 5)
        Me.btnReplicate.Name = "btnReplicate"
        Me.btnReplicate.Size = New System.Drawing.Size(327, 18)
        Me.btnReplicate.TabIndex = 3
        Me.btnReplicate.Text = "Replicate Same Commission To Multiple Items"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprint.Location = New System.Drawing.Point(151, 5)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(73, 18)
        Me.btnprint.TabIndex = 2
        Me.btnprint.Text = "Print History"
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(330, 32)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(54, 18)
        Me.MyLabel1.TabIndex = 21
        Me.MyLabel1.Text = "Hierarchy"
        '
        'ddlhier
        '
        Me.ddlhier.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Text = "HOS"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "TDM"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "ADC"
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Text = "CE"
        RadListDataItem4.TextWrap = True
        RadListDataItem5.Text = "RA"
        RadListDataItem5.TextWrap = True
        Me.ddlhier.Items.Add(RadListDataItem1)
        Me.ddlhier.Items.Add(RadListDataItem2)
        Me.ddlhier.Items.Add(RadListDataItem3)
        Me.ddlhier.Items.Add(RadListDataItem4)
        Me.ddlhier.Items.Add(RadListDataItem5)
        Me.ddlhier.Location = New System.Drawing.Point(404, 32)
        Me.ddlhier.Name = "ddlhier"
        Me.ddlhier.ShowImageInEditorArea = True
        Me.ddlhier.Size = New System.Drawing.Size(123, 20)
        Me.ddlhier.TabIndex = 4
        Me.ddlhier.Text = "HOS"
        '
        'gpcustgroup
        '
        Me.gpcustgroup.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gpcustgroup.Controls.Add(Me.rgvCustGrp)
        Me.gpcustgroup.FooterImageIndex = -1
        Me.gpcustgroup.FooterImageKey = ""
        Me.gpcustgroup.HeaderImageIndex = -1
        Me.gpcustgroup.HeaderImageKey = ""
        Me.gpcustgroup.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.gpcustgroup.HeaderText = ""
        Me.gpcustgroup.Location = New System.Drawing.Point(6, 58)
        Me.gpcustgroup.Name = "gpcustgroup"
        Me.gpcustgroup.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.gpcustgroup.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gpcustgroup.Size = New System.Drawing.Size(628, 269)
        Me.gpcustgroup.TabIndex = 5
        '
        'rgvCustGrp
        '
        Me.rgvCustGrp.Location = New System.Drawing.Point(4, 5)
        Me.rgvCustGrp.Name = "rgvCustGrp"
        Me.rgvCustGrp.Size = New System.Drawing.Size(618, 260)
        Me.rgvCustGrp.TabIndex = 0
        Me.rgvCustGrp.Text = "RadGridView1"
        '
        'fndunit
        '
        Me.fndunit.Location = New System.Drawing.Point(83, 32)
        Me.fndunit.MendatroryField = False
        Me.fndunit.MyLinkLable1 = Me.RadLabel3
        Me.fndunit.MyLinkLable2 = Nothing
        Me.fndunit.MyReadOnly = False
        Me.fndunit.Name = "fndunit"
        Me.fndunit.Size = New System.Drawing.Size(121, 19)
        Me.fndunit.TabIndex = 3
        Me.fndunit.Value = ""
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(6, 32)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(56, 18)
        Me.RadLabel3.TabIndex = 14
        Me.RadLabel3.Text = "Unit Code"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(288, 7)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(330, 8)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel2.TabIndex = 13
        Me.RadLabel2.Text = "Description"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(77, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(563, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'txtitemdesc
        '
        Me.txtitemdesc.Location = New System.Drawing.Point(404, 8)
        Me.txtitemdesc.MendatroryField = False
        Me.txtitemdesc.MyLinkLable1 = Me.RadLabel2
        Me.txtitemdesc.MyLinkLable2 = Nothing
        Me.txtitemdesc.Name = "txtitemdesc"
        Me.txtitemdesc.ReadOnly = True
        Me.txtitemdesc.Size = New System.Drawing.Size(192, 20)
        Me.txtitemdesc.TabIndex = 2
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(640, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "menu"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.menuImport, Me.menuExport, Me.menuClose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'menuImport
        '
        Me.menuImport.AccessibleDescription = "Import"
        Me.menuImport.AccessibleName = "menuImport"
        Me.menuImport.Name = "menuImport"
        Me.menuImport.Text = "Import"
        Me.menuImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'menuExport
        '
        Me.menuExport.AccessibleDescription = "Export"
        Me.menuExport.AccessibleName = "menuExport"
        Me.menuExport.Name = "menuExport"
        Me.menuExport.Text = "Export"
        Me.menuExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'menuClose
        '
        Me.menuClose.AccessibleDescription = "Close"
        Me.menuClose.AccessibleName = "menuClose"
        Me.menuClose.Name = "menuClose"
        Me.menuClose.Text = "Close"
        Me.menuClose.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReplicate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(640, 369)
        Me.SplitContainer1.SplitterDistance = 340
        Me.SplitContainer1.TabIndex = 0
        '
        'FrmCommissionMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(640, 389)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.KeyPreview = True
        Me.Name = "FrmCommissionMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Commission Master"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReplicate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlhier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gpcustgroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpcustgroup.ResumeLayout(False)
        CType(Me.rgvCustGrp.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgvCustGrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtitemdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtitemdesc As common.Controls.MyTextBox
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndunit As common.UserControls.txtFinder
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents gpcustgroup As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rgvCustGrp As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents ddlhier As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReplicate As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndItemCode As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

