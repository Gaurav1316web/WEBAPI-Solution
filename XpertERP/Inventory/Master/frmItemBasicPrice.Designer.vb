<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmItemBasicPrice
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel
        Me.lblDescription = New common.Controls.MyLabel
        Me.txtCategory = New common.UserControls.txtFinder
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.TxtSubCategoryCode = New common.UserControls.txtNavigator
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.tbDescription = New common.Controls.MyTextBox
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem
        Me.fndMRPCode = New common.UserControls.txtFinder
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton
        Me.RadButton3 = New Telerik.WinControls.UI.RadButton
        Me.RadPanel2 = New Telerik.WinControls.UI.RadPanel
        Me.txtCost = New common.MyNumBox
        Me.fndItemCode = New common.UserControls.txtFinder
        Me.RadButton4 = New Telerik.WinControls.UI.RadButton
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel2.SuspendLayout()
        CType(Me.txtCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItemExport})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(158, 20)
        Me.RadMenu1.TabIndex = 14
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPanel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(158, 54)
        Me.SplitContainer1.SplitterDistance = 25
        Me.SplitContainer1.TabIndex = 15
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
        Me.lblDescription.Location = New System.Drawing.Point(267, 71)
        Me.lblDescription.MinimumSize = New System.Drawing.Size(300, 20)
        Me.lblDescription.Name = "lblDescription"
        '
        '
        '
        Me.lblDescription.RootElement.MinSize = New System.Drawing.Size(300, 20)
        Me.lblDescription.Size = New System.Drawing.Size(300, 20)
        Me.lblDescription.TabIndex = 4
        '
        'txtCategory
        '
        Me.txtCategory.Location = New System.Drawing.Point(0, 0)
        Me.txtCategory.MendatroryField = False
        Me.txtCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategory.MyLinkLable1 = Nothing
        Me.txtCategory.MyLinkLable2 = Nothing
        Me.txtCategory.MyReadOnly = False
        Me.txtCategory.Name = "txtCategory"
        Me.txtCategory.Size = New System.Drawing.Size(143, 19)
        Me.txtCategory.TabIndex = 5
        Me.txtCategory.Value = ""
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(0, 0)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(2, 2)
        Me.RadLabel3.TabIndex = 6
        '
        'TxtSubCategoryCode
        '
        Me.TxtSubCategoryCode.Location = New System.Drawing.Point(0, 0)
        Me.TxtSubCategoryCode.MendatroryField = False
        Me.TxtSubCategoryCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.TxtSubCategoryCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.TxtSubCategoryCode.MyLinkLable1 = Nothing
        Me.TxtSubCategoryCode.MyLinkLable2 = Nothing
        Me.TxtSubCategoryCode.MyMaxLength = 32767
        Me.TxtSubCategoryCode.MyReadOnly = False
        Me.TxtSubCategoryCode.Name = "TxtSubCategoryCode"
        Me.TxtSubCategoryCode.Size = New System.Drawing.Size(202, 21)
        Me.TxtSubCategoryCode.TabIndex = 7
        Me.TxtSubCategoryCode.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(373, 9)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(0, 0)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(2, 2)
        Me.RadLabel2.TabIndex = 8
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(0, 0)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(2, 2)
        Me.RadLabel1.TabIndex = 9
        '
        'tbDescription
        '
        Me.tbDescription.Location = New System.Drawing.Point(0, 0)
        Me.tbDescription.MendatroryField = False
        Me.tbDescription.MyLinkLable1 = Nothing
        Me.tbDescription.MyLinkLable2 = Nothing
        Me.tbDescription.Name = "tbDescription"
        Me.tbDescription.Size = New System.Drawing.Size(100, 20)
        Me.tbDescription.TabIndex = 10
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
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(6, 18)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(67, 19)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
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
        'RadMenu2
        '
        Me.RadMenu2.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(420, 20)
        Me.RadMenu2.TabIndex = 14
        Me.RadMenu2.Text = "RadMenu2"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Import"
        Me.RadMenuItem2.AccessibleName = "Import"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Import"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Export"
        Me.RadMenuItem3.AccessibleName = "Export"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Export"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "RadMenuItem1"
        Me.RadMenuItem4.AccessibleName = "RadMenuItem1"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Exit"
        Me.RadMenuItem4.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'fndMRPCode
        '
        Me.fndMRPCode.Location = New System.Drawing.Point(125, 32)
        Me.fndMRPCode.MendatroryField = True
        Me.fndMRPCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndMRPCode.MyLinkLable1 = Me.MyLabel1
        Me.fndMRPCode.MyLinkLable2 = Nothing
        Me.fndMRPCode.MyReadOnly = False
        Me.fndMRPCode.Name = "fndMRPCode"
        Me.fndMRPCode.Size = New System.Drawing.Size(158, 20)
        Me.fndMRPCode.TabIndex = 2
        Me.fndMRPCode.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(11, 53)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel1.TabIndex = 14
        Me.MyLabel1.Text = "Basic Price"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Location = New System.Drawing.Point(344, 18)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(67, 19)
        Me.RadButton1.TabIndex = 2
        Me.RadButton1.Text = "Close"
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton2.Location = New System.Drawing.Point(6, 18)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(67, 19)
        Me.RadButton2.TabIndex = 0
        Me.RadButton2.Text = "Save"
        '
        'RadButton3
        '
        Me.RadButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton3.Location = New System.Drawing.Point(84, 18)
        Me.RadButton3.Name = "RadButton3"
        Me.RadButton3.Size = New System.Drawing.Size(67, 19)
        Me.RadButton3.TabIndex = 1
        Me.RadButton3.Text = "Delete"
        '
        'RadPanel2
        '
        Me.RadPanel2.Controls.Add(Me.txtCost)
        Me.RadPanel2.Controls.Add(Me.fndItemCode)
        Me.RadPanel2.Controls.Add(Me.fndMRPCode)
        Me.RadPanel2.Controls.Add(Me.MyLabel1)
        Me.RadPanel2.Controls.Add(Me.RadButton4)
        Me.RadPanel2.Controls.Add(Me.MyLabel4)
        Me.RadPanel2.Controls.Add(Me.MyLabel3)
        Me.RadPanel2.Location = New System.Drawing.Point(6, 25)
        Me.RadPanel2.Name = "RadPanel2"
        Me.RadPanel2.Size = New System.Drawing.Size(409, 92)
        Me.RadPanel2.TabIndex = 0
        '
        'txtCost
        '
        Me.txtCost.BackColor = System.Drawing.Color.White
        Me.txtCost.DecimalPlaces = 2
        Me.txtCost.Location = New System.Drawing.Point(125, 53)
        Me.txtCost.MendatroryField = False
        Me.txtCost.MyLinkLable1 = Nothing
        Me.txtCost.MyLinkLable2 = Nothing
        Me.txtCost.Name = "txtCost"
        Me.txtCost.Size = New System.Drawing.Size(158, 20)
        Me.txtCost.TabIndex = 3
        Me.txtCost.Text = "0"
        Me.txtCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCost.Value = 0
        '
        'fndItemCode
        '
        Me.fndItemCode.Location = New System.Drawing.Point(125, 12)
        Me.fndItemCode.MendatroryField = True
        Me.fndItemCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndItemCode.MyLinkLable1 = Nothing
        Me.fndItemCode.MyLinkLable2 = Nothing
        Me.fndItemCode.MyReadOnly = False
        Me.fndItemCode.Name = "fndItemCode"
        Me.fndItemCode.Size = New System.Drawing.Size(158, 20)
        Me.fndItemCode.TabIndex = 0
        Me.fndItemCode.Value = ""
        '
        'RadButton4
        '
        Me.RadButton4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton4.Image = Global.ERP.My.Resources.Resources._new
        Me.RadButton4.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadButton4.Location = New System.Drawing.Point(289, 12)
        Me.RadButton4.Name = "RadButton4"
        Me.RadButton4.Size = New System.Drawing.Size(20, 21)
        Me.RadButton4.TabIndex = 1
        '
        'MyLabel4
        '
        Me.MyLabel4.Location = New System.Drawing.Point(11, 31)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel4.TabIndex = 11
        Me.MyLabel4.Text = "MRP "
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.MyLabel3.Location = New System.Drawing.Point(11, 12)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(61, 18)
        Me.MyLabel3.TabIndex = 10
        Me.MyLabel3.Text = "Item Code"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadPanel2)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadButton2)
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadButton3)
        Me.SplitContainer2.Size = New System.Drawing.Size(420, 189)
        Me.SplitContainer2.SplitterDistance = 127
        Me.SplitContainer2.TabIndex = 15
        '
        'FrmItemBasicPrice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(420, 189)
        Me.Controls.Add(Me.RadMenu2)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Name = "FrmItemBasicPrice"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Item Basic Price"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel2.ResumeLayout(False)
        Me.RadPanel2.PerformLayout()
        CType(Me.txtCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemExport1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemExit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtCategory As common.UserControls.txtFinder
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents TxtSubCategoryCode As common.UserControls.txtNavigator
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents tbDescription As common.Controls.MyTextBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents fndMRPCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton3 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPanel2 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadButton4 As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtCost As common.MyNumBox
    Friend WithEvents fndItemCode As common.UserControls.txtFinder
End Class

