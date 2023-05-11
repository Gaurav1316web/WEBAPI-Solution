<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOperationMaster
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
        'Me.grdfndSch = New finder.gridFinder
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.cboOperationType = New common.Controls.MyComboBox
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.txtCreationDate = New common.Controls.MyTextBox
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.txtComment = New common.Controls.MyTextBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtCreatedBy = New common.Controls.MyTextBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.fndCode = New common.UserControls.txtNavigator
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.txtDesc = New common.Controls.MyTextBox
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.Filemenu = New Telerik.WinControls.UI.RadMenuItem
        Me.importmenu = New Telerik.WinControls.UI.RadMenuItem
        Me.rmHead = New Telerik.WinControls.UI.RadMenuItem
        Me.rmDetails = New Telerik.WinControls.UI.RadMenuItem
        Me.exportmenu = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.rmExdetails = New Telerik.WinControls.UI.RadMenuItem
        Me.Exitmenu = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.cboOperationType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCreationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCreatedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdfndSch
        '
        'Me.grdfndSch.Caption = Nothing
        'Me.grdfndSch.ConnectionString = Nothing
        'Me.grdfndSch.Location = New System.Drawing.Point(263, 83)
        'Me.grdfndSch.Margin = New System.Windows.Forms.Padding(0)
        'Me.grdfndSch.Name = "grdfndSch"
        'Me.grdfndSch.Query = Nothing
        'Me.grdfndSch.ResultDT = Nothing
        'Me.grdfndSch.SelectedRowDR = Nothing
        'Me.grdfndSch.SelectedValue = Nothing
        'Me.grdfndSch.SelectedValue1 = Nothing
        'Me.grdfndSch.Size = New System.Drawing.Size(15, 15)
        'Me.grdfndSch.TabIndex = 0
        'Me.grdfndSch.ValueToSelect = Nothing
        'Me.grdfndSch.ValueToSelect1 = Nothing
        'Me.grdfndSch.Visible = False
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cboOperationType)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtCreationDate)
        Me.RadGroupBox1.Controls.Add(Me.txtComment)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtCreatedBy)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.fndCode)
        Me.RadGroupBox1.Controls.Add(Me.btnReset)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox1)
        Me.RadGroupBox1.Controls.Add(Me.txtDesc)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(1, 22)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(498, 314)
        Me.RadGroupBox1.TabIndex = 0
        '
        'cboOperationType
        '
        Me.cboOperationType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboOperationType.Location = New System.Drawing.Point(388, 8)
        Me.cboOperationType.MendatroryField = True
        Me.cboOperationType.MyLinkLable1 = Me.MyLabel4
        Me.cboOperationType.MyLinkLable2 = Nothing
        Me.cboOperationType.Name = "cboOperationType"
        Me.cboOperationType.Size = New System.Drawing.Size(97, 20)
        Me.cboOperationType.TabIndex = 2
        '
        'MyLabel4
        '
        Me.MyLabel4.Location = New System.Drawing.Point(304, 9)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(84, 18)
        Me.MyLabel4.TabIndex = 13
        Me.MyLabel4.Text = "Operation Type"
        '
        'txtCreationDate
        '
        Me.txtCreationDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreationDate.Location = New System.Drawing.Point(384, 55)
        Me.txtCreationDate.MendatroryField = False
        Me.txtCreationDate.MyLinkLable1 = Me.MyLabel3
        Me.txtCreationDate.MyLinkLable2 = Nothing
        Me.txtCreationDate.Name = "txtCreationDate"
        Me.txtCreationDate.ReadOnly = True
        Me.txtCreationDate.Size = New System.Drawing.Size(101, 18)
        Me.txtCreationDate.TabIndex = 5
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(307, 56)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel3.TabIndex = 13
        Me.MyLabel3.Text = "Creation Date"
        '
        'txtComment
        '
        Me.txtComment.AutoSize = False
        Me.txtComment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComment.Location = New System.Drawing.Point(94, 78)
        Me.txtComment.MendatroryField = False
        Me.txtComment.Multiline = True
        Me.txtComment.MyLinkLable1 = Me.MyLabel2
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(391, 30)
        Me.txtComment.TabIndex = 6
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(8, 79)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel2.TabIndex = 8
        Me.MyLabel2.Text = "Comments"
        '
        'txtCreatedBy
        '
        Me.txtCreatedBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreatedBy.Location = New System.Drawing.Point(94, 55)
        Me.txtCreatedBy.MendatroryField = False
        Me.txtCreatedBy.MyLinkLable1 = Me.MyLabel1
        Me.txtCreatedBy.MyLinkLable2 = Nothing
        Me.txtCreatedBy.Name = "txtCreatedBy"
        Me.txtCreatedBy.ReadOnly = True
        Me.txtCreatedBy.Size = New System.Drawing.Size(101, 18)
        Me.txtCreatedBy.TabIndex = 4
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(8, 56)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel1.TabIndex = 10
        Me.MyLabel1.Text = "Created By"
        '
        'fndCode
        '
        Me.fndCode.Location = New System.Drawing.Point(94, 9)
        Me.fndCode.MendatroryField = True
        Me.fndCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCode.MyLinkLable1 = Me.RadLabel1
        Me.fndCode.MyLinkLable2 = Nothing
        Me.fndCode.MyMaxLength = 32767
        Me.fndCode.MyReadOnly = False
        Me.fndCode.Name = "fndCode"
        Me.fndCode.Size = New System.Drawing.Size(171, 18)
        Me.fndCode.TabIndex = 0
        Me.fndCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.RadLabel1.Location = New System.Drawing.Point(8, 10)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(74, 16)
        Me.RadLabel1.TabIndex = 12
        Me.RadLabel1.Text = "Operation No"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnReset.Location = New System.Drawing.Point(265, 10)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(17, 17)
        Me.btnReset.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.gv1)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 110)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(474, 200)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Work Center Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(3, 18)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.EnableGrouping = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.Size = New System.Drawing.Size(468, 179)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'txtDesc
        '
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(94, 32)
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel2
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(391, 18)
        Me.txtDesc.TabIndex = 3
        '
        'RadLabel2
        '
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(8, 33)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel2.TabIndex = 11
        Me.RadLabel2.Text = "Description"
        '
        'btnclose
        '
        Me.btnclose.AccessibleDescription = ""
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(433, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.AccessibleDescription = ""
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(70, 9)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(66, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 9)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.DropDownAnimationEasing = Telerik.WinControls.RadEasingType.[Default]
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Filemenu})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(502, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "menu"
        '
        'Filemenu
        '
        Me.Filemenu.AccessibleDescription = "File"
        Me.Filemenu.AccessibleName = "File"
        Me.Filemenu.Items.AddRange(New Telerik.WinControls.RadItem() {Me.importmenu, Me.exportmenu, Me.Exitmenu})
        Me.Filemenu.Name = "Filemenu"
        Me.Filemenu.Text = "File"
        Me.Filemenu.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'importmenu
        '
        Me.importmenu.AccessibleDescription = "Import"
        Me.importmenu.AccessibleName = "Import"
        Me.importmenu.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmHead, Me.rmDetails})
        Me.importmenu.Name = "importmenu"
        Me.importmenu.Text = "Import"
        Me.importmenu.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rmHead
        '
        Me.rmHead.AccessibleDescription = "Head"
        Me.rmHead.AccessibleName = "Head"
        Me.rmHead.Name = "rmHead"
        Me.rmHead.Text = "Head"
        Me.rmHead.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rmDetails
        '
        Me.rmDetails.AccessibleDescription = "Details"
        Me.rmDetails.AccessibleName = "Details"
        Me.rmDetails.Name = "rmDetails"
        Me.rmDetails.Text = "Details"
        Me.rmDetails.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'exportmenu
        '
        Me.exportmenu.AccessibleDescription = "Export"
        Me.exportmenu.AccessibleName = "Export"
        Me.exportmenu.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.rmExdetails})
        Me.exportmenu.Name = "exportmenu"
        Me.exportmenu.Text = "Export"
        Me.exportmenu.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Head"
        Me.RadMenuItem1.AccessibleName = "Head"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Head"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rmExdetails
        '
        Me.rmExdetails.AccessibleDescription = "Details"
        Me.rmExdetails.AccessibleName = "Details"
        Me.rmExdetails.Name = "rmExdetails"
        Me.rmExdetails.Text = "Details"
        Me.rmExdetails.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'Exitmenu
        '
        Me.Exitmenu.AccessibleDescription = "Exit"
        Me.Exitmenu.AccessibleName = "Exit"
        Me.Exitmenu.Name = "Exitmenu"
        Me.Exitmenu.Text = "Exit"
        Me.Exitmenu.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(502, 373)
        Me.SplitContainer1.SplitterDistance = 338
        Me.SplitContainer1.TabIndex = 0
        '
        'frmOperationMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(502, 373)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmOperationMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Operation Master"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.cboOperationType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCreationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCreatedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    'Friend WithEvents grdfndSch As finder.gridFinder
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents Filemenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents importmenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents exportmenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Exitmenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents fndCode As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents txtCreationDate As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtCreatedBy As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents cboOperationType As common.Controls.MyComboBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents rmHead As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDetails As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExdetails As Telerik.WinControls.UI.RadMenuItem
End Class

