<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRequisitSubTypeMaster
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
        Me.txtLevel3 = New common.MyNumBox
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.txtLevel2 = New common.MyNumBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cboItemType = New common.Controls.MyComboBox
        Me.txtDesc = New common.Controls.MyTextBox
        Me.txtCode = New common.UserControls.txtNavigator
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtLevel1 = New common.MyNumBox
        Me.lblAmount = New common.Controls.MyLabel
        CType(Me.txtLevel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLevel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLevel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtLevel3
        '
        Me.txtLevel3.BackColor = System.Drawing.Color.White
        Me.txtLevel3.DecimalPlaces = 2
        Me.txtLevel3.Location = New System.Drawing.Point(148, 51)
        Me.txtLevel3.MendatroryField = False
        Me.txtLevel3.MyLinkLable1 = Nothing
        Me.txtLevel3.MyLinkLable2 = Nothing
        Me.txtLevel3.Name = "txtLevel3"
        Me.txtLevel3.Size = New System.Drawing.Size(154, 20)
        Me.txtLevel3.TabIndex = 2
        Me.txtLevel3.Text = "0"
        Me.txtLevel3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLevel3.Value = 0
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(9, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(69, 53)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel2.TabIndex = 611
        Me.MyLabel2.Text = "Level 3 >"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.RadGroupBox2.Controls.Add(Me.btnDelete)
        Me.RadGroupBox2.Controls.Add(Me.btnSave)
        Me.RadGroupBox2.Controls.Add(Me.btnClose)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(4, 3)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(397, 26)
        Me.RadGroupBox2.TabIndex = 0
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(82, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 20)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(324, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'txtLevel2
        '
        Me.txtLevel2.BackColor = System.Drawing.Color.White
        Me.txtLevel2.DecimalPlaces = 2
        Me.txtLevel2.Location = New System.Drawing.Point(148, 29)
        Me.txtLevel2.MendatroryField = False
        Me.txtLevel2.MyLinkLable1 = Nothing
        Me.txtLevel2.MyLinkLable2 = Nothing
        Me.txtLevel2.Name = "txtLevel2"
        Me.txtLevel2.Size = New System.Drawing.Size(154, 20)
        Me.txtLevel2.TabIndex = 1
        Me.txtLevel2.Text = "0"
        Me.txtLevel2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLevel2.Value = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(1, 7)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Size = New System.Drawing.Size(407, 195)
        Me.SplitContainer1.SplitterDistance = 142
        Me.SplitContainer1.TabIndex = 0
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.RadGroupBox3.Controls.Add(Me.cboItemType)
        Me.RadGroupBox3.Controls.Add(Me.txtDesc)
        Me.RadGroupBox3.Controls.Add(Me.txtCode)
        Me.RadGroupBox3.Controls.Add(Me.btnAddNew)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(4, 26)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(396, 76)
        Me.RadGroupBox3.TabIndex = 1
        '
        'cboItemType
        '
        Me.cboItemType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboItemType.Location = New System.Drawing.Point(114, 51)
        Me.cboItemType.MendatroryField = True
        Me.cboItemType.MyLinkLable1 = Nothing
        Me.cboItemType.MyLinkLable2 = Nothing
        Me.cboItemType.Name = "cboItemType"
        Me.cboItemType.Size = New System.Drawing.Size(234, 20)
        Me.cboItemType.TabIndex = 2
        '
        'txtDesc
        '
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(114, 29)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Nothing
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(234, 18)
        Me.txtDesc.TabIndex = 1
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(114, 6)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Nothing
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(213, 20)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPPurchase.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(332, 5)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 0
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(16, 51)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel3.TabIndex = 3
        Me.MyLabel3.Text = "Type"
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(16, 31)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel4.TabIndex = 4
        Me.MyLabel4.Text = "Description"
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(16, 11)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel5.TabIndex = 5
        Me.MyLabel5.Text = "Code"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(407, 20)
        Me.RadMenu1.TabIndex = 321
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmImport, Me.rmExport})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Files"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rmImport
        '
        Me.rmImport.AccessibleDescription = "Import"
        Me.rmImport.AccessibleName = "Import"
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
        Me.rmImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rmExport
        '
        Me.rmExport.AccessibleDescription = "Export"
        Me.rmExport.AccessibleName = "Export"
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export"
        Me.rmExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.RadGroupBox1.Controls.Add(Me.txtLevel3)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtLevel2)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtLevel1)
        Me.RadGroupBox1.Controls.Add(Me.lblAmount)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(5, 26)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(396, 76)
        Me.RadGroupBox1.TabIndex = 0
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(69, 31)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(59, 16)
        Me.MyLabel1.TabIndex = 1
        Me.MyLabel1.Text = "Level 2 <="
        '
        'txtLevel1
        '
        Me.txtLevel1.BackColor = System.Drawing.Color.White
        Me.txtLevel1.DecimalPlaces = 2
        Me.txtLevel1.Location = New System.Drawing.Point(148, 7)
        Me.txtLevel1.MendatroryField = False
        Me.txtLevel1.MyLinkLable1 = Nothing
        Me.txtLevel1.MyLinkLable2 = Nothing
        Me.txtLevel1.Name = "txtLevel1"
        Me.txtLevel1.Size = New System.Drawing.Size(154, 20)
        Me.txtLevel1.TabIndex = 0
        Me.txtLevel1.Text = "0"
        Me.txtLevel1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLevel1.Value = 0
        '
        'lblAmount
        '
        Me.lblAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmount.Location = New System.Drawing.Point(69, 9)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(62, 16)
        Me.lblAmount.TabIndex = 0
        Me.lblAmount.Text = "Level 1  <="
        '
        'FrmRequisitSubTypeMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(407, 201)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmRequisitSubTypeMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Requisite SubType Master"
        CType(Me.txtLevel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLevel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLevel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtLevel3 As common.MyNumBox
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtLevel2 As common.MyNumBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtLevel1 As common.MyNumBox
    Friend WithEvents lblAmount As common.Controls.MyLabel
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents cboItemType As common.Controls.MyComboBox
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
End Class

