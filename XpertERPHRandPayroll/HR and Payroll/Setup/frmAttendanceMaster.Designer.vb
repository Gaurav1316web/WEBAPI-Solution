Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAttendanceMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAttendanceMaster))
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.lblOTCode = New common.Controls.MyLabel
        Me.txtOTCode = New common.UserControls.txtFinder
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.cboAttRegType = New common.Controls.MyComboBox
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.cboSalaryCalOnDay = New common.Controls.MyComboBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.cboSalaryDep = New common.Controls.MyComboBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtDescription = New common.Controls.MyTextBox
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.RadLabel4 = New common.Controls.MyLabel
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.txtName = New common.Controls.MyTextBox
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.txtCode = New common.UserControls.txtNavigator
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.RadMenuItemExport = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.MenuItemImport = New Telerik.WinControls.UI.RadMenuItem
        Me.MenuItemExport = New Telerik.WinControls.UI.RadMenuItem
        Me.MenuItemClose = New Telerik.WinControls.UI.RadMenuItem
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblOTCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboAttRegType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboSalaryCalOnDay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboSalaryDep, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.lblOTCode)
        Me.RadGroupBox1.Controls.Add(Me.txtOTCode)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.cboAttRegType)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.cboSalaryCalOnDay)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.cboSalaryDep)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtDescription)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox1.Controls.Add(Me.RadButton1)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.txtName)
        Me.RadGroupBox1.Controls.Add(Me.txtCode)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.HeaderText = " "
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 15)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(519, 200)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = " "
        '
        'lblOTCode
        '
        Me.lblOTCode.AutoSize = False
        Me.lblOTCode.BorderVisible = True
        Me.lblOTCode.Location = New System.Drawing.Point(372, 97)
        Me.lblOTCode.Name = "lblOTCode"
        Me.lblOTCode.Size = New System.Drawing.Size(134, 19)
        Me.lblOTCode.TabIndex = 5
        Me.lblOTCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblOTCode.TextWrap = False
        '
        'txtOTCode
        '
        Me.txtOTCode.Location = New System.Drawing.Point(150, 97)
        Me.txtOTCode.MendatroryField = True
        Me.txtOTCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOTCode.MyLinkLable1 = Me.lblOTCode
        Me.txtOTCode.MyLinkLable2 = Me.MyLabel4
        Me.txtOTCode.MyReadOnly = False
        Me.txtOTCode.Name = "txtOTCode"
        Me.txtOTCode.Size = New System.Drawing.Size(222, 19)
        Me.txtOTCode.TabIndex = 4
        Me.txtOTCode.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.Location = New System.Drawing.Point(13, 97)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(50, 18)
        Me.MyLabel4.TabIndex = 33
        Me.MyLabel4.Text = "OT Code"
        '
        'cboAttRegType
        '
        Me.cboAttRegType.BackColor = System.Drawing.Color.Transparent
        Me.cboAttRegType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAttRegType.Location = New System.Drawing.Point(150, 173)
        Me.cboAttRegType.MendatroryField = False
        Me.cboAttRegType.MyLinkLable1 = Me.MyLabel3
        Me.cboAttRegType.MyLinkLable2 = Nothing
        Me.cboAttRegType.Name = "cboAttRegType"
        Me.cboAttRegType.Size = New System.Drawing.Size(222, 18)
        Me.cboAttRegType.TabIndex = 8
        '
        'MyLabel3
        '
        Me.MyLabel3.Location = New System.Drawing.Point(12, 173)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(134, 18)
        Me.MyLabel3.TabIndex = 32
        Me.MyLabel3.Text = "Attendance Register Type"
        '
        'cboSalaryCalOnDay
        '
        Me.cboSalaryCalOnDay.BackColor = System.Drawing.Color.Transparent
        Me.cboSalaryCalOnDay.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSalaryCalOnDay.Location = New System.Drawing.Point(150, 147)
        Me.cboSalaryCalOnDay.MendatroryField = False
        Me.cboSalaryCalOnDay.MyLinkLable1 = Me.MyLabel2
        Me.cboSalaryCalOnDay.MyLinkLable2 = Nothing
        Me.cboSalaryCalOnDay.Name = "cboSalaryCalOnDay"
        Me.cboSalaryCalOnDay.Size = New System.Drawing.Size(222, 18)
        Me.cboSalaryCalOnDay.TabIndex = 7
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(12, 147)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(138, 18)
        Me.MyLabel2.TabIndex = 30
        Me.MyLabel2.Text = "Salary Calculation on Days"
        '
        'cboSalaryDep
        '
        Me.cboSalaryDep.BackColor = System.Drawing.Color.Transparent
        Me.cboSalaryDep.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSalaryDep.Location = New System.Drawing.Point(150, 123)
        Me.cboSalaryDep.MendatroryField = False
        Me.cboSalaryDep.MyLinkLable1 = Me.MyLabel1
        Me.cboSalaryDep.MyLinkLable2 = Nothing
        Me.cboSalaryDep.Name = "cboSalaryDep"
        Me.cboSalaryDep.Size = New System.Drawing.Size(222, 18)
        Me.cboSalaryDep.TabIndex = 6
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(12, 123)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(102, 18)
        Me.MyLabel1.TabIndex = 28
        Me.MyLabel1.Text = "Salary Dependency"
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(149, 72)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.RadLabel3
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(360, 20)
        Me.txtDescription.TabIndex = 3
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(12, 73)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel3.TabIndex = 20
        Me.RadLabel3.Text = "Description"
        '
        'RadLabel4
        '
        Me.RadLabel4.Location = New System.Drawing.Point(12, 89)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(2, 2)
        Me.RadLabel4.TabIndex = 19
        '
        'RadButton1
        '
        Me.RadButton1.Image = CType(resources.GetObject("RadButton1.Image"), System.Drawing.Image)
        Me.RadButton1.Location = New System.Drawing.Point(352, -422)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(14, 20)
        Me.RadButton1.TabIndex = 17
        Me.RadButton1.Text = " "
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(372, 22)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(149, 45)
        Me.txtName.MaxLength = 50
        Me.txtName.MendatroryField = False
        Me.txtName.MyLinkLable1 = Me.RadLabel2
        Me.txtName.MyLinkLable2 = Nothing
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(360, 20)
        Me.txtName.TabIndex = 2
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(12, 46)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(97, 18)
        Me.RadLabel2.TabIndex = 1
        Me.RadLabel2.Text = "Attendance Name"
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(149, 22)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.RadLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(223, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(12, 23)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "Code"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(460, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnDelete.Location = New System.Drawing.Point(74, 6)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnSave.Location = New System.Drawing.Point(3, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenuItemExport
        '
        Me.RadMenuItemExport.AccessibleDescription = "File"
        Me.RadMenuItemExport.AccessibleName = "File"
        Me.RadMenuItemExport.Name = "RadMenuItemExport"
        Me.RadMenuItemExport.Text = "File"
        Me.RadMenuItemExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "File"
        Me.RadMenuItem2.AccessibleName = "File"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "File"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(529, 478)
        Me.SplitContainer1.SplitterDistance = 447
        Me.SplitContainer1.TabIndex = 0
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(529, 20)
        Me.RadMenu2.TabIndex = 10
        Me.RadMenu2.Text = "RadMenu2"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "File"
        Me.RadMenuItem3.AccessibleName = "File"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuItemImport, Me.MenuItemExport, Me.MenuItemClose})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'MenuItemImport
        '
        Me.MenuItemImport.AccessibleDescription = "Import"
        Me.MenuItemImport.AccessibleName = "Import"
        Me.MenuItemImport.Name = "MenuItemImport"
        Me.MenuItemImport.Text = "Import"
        Me.MenuItemImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'MenuItemExport
        '
        Me.MenuItemExport.AccessibleDescription = "Export"
        Me.MenuItemExport.AccessibleName = "Export"
        Me.MenuItemExport.Name = "MenuItemExport"
        Me.MenuItemExport.Text = "Export"
        Me.MenuItemExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'MenuItemClose
        '
        Me.MenuItemClose.AccessibleDescription = "Close"
        Me.MenuItemClose.AccessibleName = "Close"
        Me.MenuItemClose.Name = "MenuItemClose"
        Me.MenuItemClose.Text = "Close"
        Me.MenuItemClose.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'frmAttendanceMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(529, 478)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmAttendanceMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Attendance Master"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblOTCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboAttRegType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboSalaryCalOnDay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboSalaryDep, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtName As common.Controls.MyTextBox
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents cboSalaryDep As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents cboAttRegType As common.Controls.MyComboBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents cboSalaryCalOnDay As common.Controls.MyComboBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblOTCode As common.Controls.MyLabel
    Friend WithEvents txtOTCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
End Class

