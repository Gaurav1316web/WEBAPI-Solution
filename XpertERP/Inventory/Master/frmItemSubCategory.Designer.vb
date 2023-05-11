<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmItemSubCategory
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItemExport = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItemImport = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItemExport1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItemExit = New Telerik.WinControls.UI.RadMenuItem
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel
        Me.lblDescription = New common.Controls.MyLabel
        Me.txtCategory = New common.UserControls.txtFinder
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.TxtSubCategoryCode = New common.UserControls.txtNavigator
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.tbDescription = New common.Controls.MyTextBox
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItemExport})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(538, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItemExport
        '
        Me.RadMenuItemExport.AccessibleDescription = "File"
        Me.RadMenuItemExport.AccessibleName = "File"
        Me.RadMenuItemExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItemImport, Me.RadMenuItemExport1, Me.RadMenuItemExit})
        Me.RadMenuItemExport.Name = "RadMenuItemExport"
        Me.RadMenuItemExport.Text = "File"
        Me.RadMenuItemExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItemImport
        '
        Me.RadMenuItemImport.AccessibleDescription = "Import"
        Me.RadMenuItemImport.AccessibleName = "Import"
        Me.RadMenuItemImport.Name = "RadMenuItemImport"
        Me.RadMenuItemImport.Text = "Import"
        Me.RadMenuItemImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItemExport1
        '
        Me.RadMenuItemExport1.AccessibleDescription = "Export"
        Me.RadMenuItemExport1.AccessibleName = "Export"
        Me.RadMenuItemExport1.Name = "RadMenuItemExport1"
        Me.RadMenuItemExport1.Text = "Export"
        Me.RadMenuItemExport1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItemExit
        '
        Me.RadMenuItemExit.AccessibleDescription = "RadMenuItem1"
        Me.RadMenuItemExit.AccessibleName = "RadMenuItem1"
        Me.RadMenuItemExit.Name = "RadMenuItemExit"
        Me.RadMenuItemExit.Text = "Exit"
        Me.RadMenuItemExit.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.lblDescription)
        Me.RadPanel1.Controls.Add(Me.txtCategory)
        Me.RadPanel1.Controls.Add(Me.RadLabel3)
        Me.RadPanel1.Controls.Add(Me.TxtSubCategoryCode)
        Me.RadPanel1.Controls.Add(Me.btnAddNew)
        Me.RadPanel1.Controls.Add(Me.RadLabel2)
        Me.RadPanel1.Controls.Add(Me.RadLabel1)
        Me.RadPanel1.Controls.Add(Me.tbDescription)
        Me.RadPanel1.Location = New System.Drawing.Point(6, 3)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(523, 113)
        Me.RadPanel1.TabIndex = 0
        '
        'lblDescription
        '
        Me.lblDescription.Location = New System.Drawing.Point(261, 71)
        Me.lblDescription.MinimumSize = New System.Drawing.Size(250, 20)
        Me.lblDescription.Name = "lblDescription"
        '
        '
        '
        Me.lblDescription.RootElement.MinSize = New System.Drawing.Size(250, 20)
        Me.lblDescription.Size = New System.Drawing.Size(250, 20)
        Me.lblDescription.TabIndex = 0
        '
        'txtCategory
        '
        Me.txtCategory.Location = New System.Drawing.Point(125, 71)
        Me.txtCategory.MendatroryField = True
        Me.txtCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategory.MyLinkLable1 = Me.RadLabel3
        Me.txtCategory.MyLinkLable2 = Nothing
        Me.txtCategory.MyReadOnly = False
        Me.txtCategory.Name = "txtCategory"
        Me.txtCategory.Size = New System.Drawing.Size(130, 20)
        Me.txtCategory.TabIndex = 4
        Me.txtCategory.Value = ""
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(10, 71)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(51, 18)
        Me.RadLabel3.TabIndex = 5
        Me.RadLabel3.Text = "Category"
        '
        'TxtSubCategoryCode
        '
        Me.TxtSubCategoryCode.Location = New System.Drawing.Point(125, 9)
        Me.TxtSubCategoryCode.MendatroryField = False
        Me.TxtSubCategoryCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.TxtSubCategoryCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.TxtSubCategoryCode.MyLinkLable1 = Me.RadLabel1
        Me.TxtSubCategoryCode.MyLinkLable2 = Nothing
        Me.TxtSubCategoryCode.MyMaxLength = 32767
        Me.TxtSubCategoryCode.MyReadOnly = False
        Me.TxtSubCategoryCode.Name = "TxtSubCategoryCode"
        Me.TxtSubCategoryCode.Size = New System.Drawing.Size(244, 21)
        Me.TxtSubCategoryCode.TabIndex = 1
        Me.TxtSubCategoryCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.RadLabel1.Location = New System.Drawing.Point(11, 12)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(108, 18)
        Me.RadLabel1.TabIndex = 7
        Me.RadLabel1.Text = "Sub Category Code"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(373, 9)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 2
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(10, 41)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel2.TabIndex = 6
        Me.RadLabel2.Text = "Description"
        '
        'tbDescription
        '
        Me.tbDescription.Location = New System.Drawing.Point(125, 41)
        Me.tbDescription.MendatroryField = False
        Me.tbDescription.MyLinkLable1 = Me.RadLabel2
        Me.tbDescription.MyLinkLable2 = Nothing
        Me.tbDescription.Name = "tbDescription"
        Me.tbDescription.Size = New System.Drawing.Size(268, 20)
        Me.tbDescription.TabIndex = 3
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(462, 18)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(67, 19)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(84, 18)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(67, 19)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(6, 18)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(67, 19)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPanel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(538, 185)
        Me.SplitContainer1.SplitterDistance = 123
        Me.SplitContainer1.TabIndex = 0
        '
        'FrmItemSubCategory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(538, 205)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmItemSubCategory"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Item Sub Category"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents TxtSubCategoryCode As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents tbDescription As common.Controls.MyTextBox
    Friend WithEvents txtCategory As common.UserControls.txtFinder
    Friend WithEvents RadMenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemExport1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemExit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

