<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExportTemplate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExportTemplate))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblUserName = New common.Controls.MyLabel()
        Me.fndUser = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtProgram = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.gvColumnsMain = New common.UserControls.MyRadGridView()
        Me.rdbNext = New Telerik.WinControls.UI.RadButton()
        Me.btnBack = New Telerik.WinControls.UI.RadButton()
        Me.gvColumnTemp = New common.UserControls.MyRadGridView()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.txtName = New common.Controls.MyTextBox()
        Me.lblItemCategoryCode = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblUserName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.gvColumnsMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvColumnsMain.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbNext, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvColumnTemp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvColumnTemp.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblUserName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndUser)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtProgram)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblItemCategoryCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(782, 505)
        Me.SplitContainer1.SplitterDistance = 457
        Me.SplitContainer1.TabIndex = 158
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = False
        Me.lblUserName.BorderVisible = True
        Me.lblUserName.FieldName = Nothing
        Me.lblUserName.Location = New System.Drawing.Point(328, 73)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(217, 18)
        Me.lblUserName.TabIndex = 327
        Me.lblUserName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fndUser
        '
        Me.fndUser.CalculationExpression = Nothing
        Me.fndUser.Enabled = False
        Me.fndUser.FieldCode = Nothing
        Me.fndUser.FieldDesc = Nothing
        Me.fndUser.FieldMaxLength = 0
        Me.fndUser.FieldName = Nothing
        Me.fndUser.isCalculatedField = False
        Me.fndUser.IsSourceFromTable = False
        Me.fndUser.IsSourceFromValueList = False
        Me.fndUser.IsUnique = False
        Me.fndUser.Location = New System.Drawing.Point(102, 73)
        Me.fndUser.MendatroryField = True
        Me.fndUser.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndUser.MyLinkLable1 = Me.lblUserName
        Me.fndUser.MyLinkLable2 = Me.MyLabel3
        Me.fndUser.MyReadOnly = False
        Me.fndUser.MyShowMasterFormButton = False
        Me.fndUser.Name = "fndUser"
        Me.fndUser.ReferenceFieldDesc = Nothing
        Me.fndUser.ReferenceFieldName = Nothing
        Me.fndUser.ReferenceTableName = Nothing
        Me.fndUser.Size = New System.Drawing.Size(221, 18)
        Me.fndUser.TabIndex = 326
        Me.fndUser.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(12, 73)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(69, 18)
        Me.MyLabel3.TabIndex = 328
        Me.MyLabel3.Text = "Current User"
        '
        'txtProgram
        '
        Me.txtProgram.CalculationExpression = Nothing
        Me.txtProgram.Enabled = False
        Me.txtProgram.FieldCode = Nothing
        Me.txtProgram.FieldDesc = Nothing
        Me.txtProgram.FieldMaxLength = 0
        Me.txtProgram.FieldName = Nothing
        Me.txtProgram.isCalculatedField = False
        Me.txtProgram.IsSourceFromTable = False
        Me.txtProgram.IsSourceFromValueList = False
        Me.txtProgram.IsUnique = False
        Me.txtProgram.Location = New System.Drawing.Point(101, 51)
        Me.txtProgram.MendatroryField = True
        Me.txtProgram.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProgram.MyLinkLable1 = Nothing
        Me.txtProgram.MyLinkLable2 = Me.MyLabel1
        Me.txtProgram.MyReadOnly = False
        Me.txtProgram.MyShowMasterFormButton = False
        Me.txtProgram.Name = "txtProgram"
        Me.txtProgram.ReferenceFieldDesc = Nothing
        Me.txtProgram.ReferenceFieldName = Nothing
        Me.txtProgram.ReferenceTableName = Nothing
        Me.txtProgram.Size = New System.Drawing.Size(221, 18)
        Me.txtProgram.TabIndex = 178
        Me.txtProgram.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(12, 51)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(54, 18)
        Me.MyLabel1.TabIndex = 180
        Me.MyLabel1.Text = "Report ID"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 129)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.gvColumnsMain)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rdbNext)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnBack)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gvColumnTemp)
        Me.SplitContainer2.Size = New System.Drawing.Size(776, 325)
        Me.SplitContainer2.SplitterDistance = 410
        Me.SplitContainer2.TabIndex = 177
        '
        'gvColumnsMain
        '
        Me.gvColumnsMain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gvColumnsMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvColumnsMain.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvColumnsMain.EnableCustomFiltering = True
        Me.gvColumnsMain.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvColumnsMain.ForeColor = System.Drawing.Color.Black
        Me.gvColumnsMain.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvColumnsMain.Location = New System.Drawing.Point(0, 0)
        '
        'gvColumnsMain
        '
        Me.gvColumnsMain.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvColumnsMain.MasterTemplate.AllowAddNewRow = False
        Me.gvColumnsMain.MasterTemplate.AllowDeleteRow = False
        Me.gvColumnsMain.MasterTemplate.AutoGenerateColumns = False
        Me.gvColumnsMain.MasterTemplate.EnableCustomFiltering = True
        Me.gvColumnsMain.MasterTemplate.EnableGrouping = False
        Me.gvColumnsMain.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvColumnsMain.Name = "gvColumnsMain"
        Me.gvColumnsMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvColumnsMain.ShowHeaderCellButtons = True
        Me.gvColumnsMain.Size = New System.Drawing.Size(369, 325)
        Me.gvColumnsMain.TabIndex = 3
        Me.gvColumnsMain.TabStop = False
        '
        'rdbNext
        '
        Me.rdbNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbNext.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbNext.Location = New System.Drawing.Point(373, 87)
        Me.rdbNext.Name = "rdbNext"
        Me.rdbNext.Size = New System.Drawing.Size(32, 21)
        Me.rdbNext.TabIndex = 4
        Me.rdbNext.Text = ">>"
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.Location = New System.Drawing.Point(373, 114)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(32, 21)
        Me.btnBack.TabIndex = 5
        Me.btnBack.Text = "<<"
        Me.btnBack.Visible = False
        '
        'gvColumnTemp
        '
        Me.gvColumnTemp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gvColumnTemp.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvColumnTemp.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvColumnTemp.EnableCustomFiltering = True
        Me.gvColumnTemp.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvColumnTemp.ForeColor = System.Drawing.Color.Black
        Me.gvColumnTemp.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvColumnTemp.Location = New System.Drawing.Point(0, 0)
        '
        'gvColumnTemp
        '
        Me.gvColumnTemp.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvColumnTemp.MasterTemplate.AllowAddNewRow = False
        Me.gvColumnTemp.MasterTemplate.AllowEditRow = False
        Me.gvColumnTemp.MasterTemplate.AutoGenerateColumns = False
        Me.gvColumnTemp.MasterTemplate.EnableCustomFiltering = True
        Me.gvColumnTemp.MasterTemplate.EnableGrouping = False
        Me.gvColumnTemp.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvColumnTemp.Name = "gvColumnTemp"
        Me.gvColumnTemp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvColumnTemp.ShowHeaderCellButtons = True
        Me.gvColumnTemp.Size = New System.Drawing.Size(362, 325)
        Me.gvColumnTemp.TabIndex = 6
        Me.gvColumnTemp.TabStop = False
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(12, 28)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(36, 16)
        Me.lblDescription.TabIndex = 176
        Me.lblDescription.Text = "Name"
        '
        'txtName
        '
        Me.txtName.AutoSize = False
        Me.txtName.CalculationExpression = Nothing
        Me.txtName.FieldCode = Nothing
        Me.txtName.FieldDesc = Nothing
        Me.txtName.FieldMaxLength = 0
        Me.txtName.FieldName = Nothing
        Me.txtName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.isCalculatedField = False
        Me.txtName.IsSourceFromTable = False
        Me.txtName.IsSourceFromValueList = False
        Me.txtName.IsUnique = False
        Me.txtName.Location = New System.Drawing.Point(101, 26)
        Me.txtName.MaxLength = 50
        Me.txtName.MendatroryField = False
        Me.txtName.Multiline = True
        Me.txtName.MyLinkLable1 = Me.lblDescription
        Me.txtName.MyLinkLable2 = Nothing
        Me.txtName.Name = "txtName"
        Me.txtName.ReferenceFieldDesc = Nothing
        Me.txtName.ReferenceFieldName = Nothing
        Me.txtName.ReferenceTableName = Nothing
        Me.txtName.Size = New System.Drawing.Size(444, 21)
        Me.txtName.TabIndex = 2
        Me.txtName.Text = " "
        '
        'lblItemCategoryCode
        '
        Me.lblItemCategoryCode.FieldName = Nothing
        Me.lblItemCategoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemCategoryCode.Location = New System.Drawing.Point(11, 5)
        Me.lblItemCategoryCode.Name = "lblItemCategoryCode"
        Me.lblItemCategoryCode.Size = New System.Drawing.Size(33, 16)
        Me.lblItemCategoryCode.TabIndex = 158
        Me.lblItemCategoryCode.Text = "Code"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(434, 3)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(18, 21)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(101, 3)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblItemCategoryCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(331, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 13)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 21)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(707, 13)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(81, 13)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'frmExportTemplate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(782, 505)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmExportTemplate"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Export Template"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblUserName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.gvColumnsMain.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvColumnsMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbNext, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvColumnTemp.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvColumnTemp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblItemCategoryCode As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents gvColumnsMain As common.UserControls.MyRadGridView
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtName As common.Controls.MyTextBox
    Friend WithEvents rdbNext As Telerik.WinControls.UI.RadButton
    Friend WithEvents gvColumnTemp As common.UserControls.MyRadGridView
    Friend WithEvents btnBack As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtProgram As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblUserName As common.Controls.MyLabel
    Friend WithEvents fndUser As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
End Class
