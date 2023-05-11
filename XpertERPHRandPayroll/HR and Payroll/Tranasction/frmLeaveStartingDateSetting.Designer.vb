Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLeaveStartingDateSetting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLeaveStartingDateSetting))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.MenuItemImport = New Telerik.WinControls.UI.RadMenuItem
        Me.MenuItemExport = New Telerik.WinControls.UI.RadMenuItem
        Me.MenuItemClose = New Telerik.WinControls.UI.RadMenuItem
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.txtCode = New common.UserControls.txtFinder
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.lblEmpName = New common.Controls.MyLabel
        Me.lblOpeningDate = New common.Controls.MyLabel
        Me.dtpAvailStarting = New common.Controls.MyDateTimePicker
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.txtLeaveCode = New common.UserControls.txtFinder
        Me.dtpAllotStarting = New common.Controls.MyDateTimePicker
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOpeningDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpAvailStarting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpAllotStarting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(638, 252)
        Me.SplitContainer1.SplitterDistance = 212
        Me.SplitContainer1.TabIndex = 0
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(638, 20)
        Me.RadMenu2.TabIndex = 171
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
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtCode)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.lblEmpName)
        Me.RadGroupBox1.Controls.Add(Me.lblOpeningDate)
        Me.RadGroupBox1.Controls.Add(Me.dtpAvailStarting)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtLeaveCode)
        Me.RadGroupBox1.Controls.Add(Me.dtpAllotStarting)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(4, 23)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(622, 173)
        Me.RadGroupBox1.TabIndex = 0
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(141, 22)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.MyLinkLable1 = Me.MyLabel2
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(222, 19)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(13, 71)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel2.TabIndex = 147
        Me.MyLabel2.Text = "Leave Code"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(368, 21)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(13, 23)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel4.TabIndex = 153
        Me.MyLabel4.Text = "Employee Code"
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(13, 47)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(90, 16)
        Me.MyLabel1.TabIndex = 169
        Me.MyLabel1.Text = "Employee Name"
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.Location = New System.Drawing.Point(141, 46)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(222, 19)
        Me.lblEmpName.TabIndex = 2
        Me.lblEmpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblOpeningDate
        '
        Me.lblOpeningDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOpeningDate.Location = New System.Drawing.Point(13, 95)
        Me.lblOpeningDate.Name = "lblOpeningDate"
        Me.lblOpeningDate.Size = New System.Drawing.Size(97, 16)
        Me.lblOpeningDate.TabIndex = 155
        Me.lblOpeningDate.Text = "Allot Starting Date"
        '
        'dtpAvailStarting
        '
        Me.dtpAvailStarting.CustomFormat = "dd/MM/yyyy"
        Me.dtpAvailStarting.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpAvailStarting.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAvailStarting.Location = New System.Drawing.Point(141, 118)
        Me.dtpAvailStarting.MendatroryField = False
        Me.dtpAvailStarting.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.dtpAvailStarting.MyLinkLable1 = Me.MyLabel3
        Me.dtpAvailStarting.MyLinkLable2 = Nothing
        Me.dtpAvailStarting.Name = "dtpAvailStarting"
        Me.dtpAvailStarting.Size = New System.Drawing.Size(108, 18)
        Me.dtpAvailStarting.TabIndex = 5
        Me.dtpAvailStarting.TabStop = False
        Me.dtpAvailStarting.Text = "03/05/2011"
        Me.dtpAvailStarting.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(13, 119)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(100, 16)
        Me.MyLabel3.TabIndex = 162
        Me.MyLabel3.Text = "Avail Starting Date"
        '
        'txtLeaveCode
        '
        Me.txtLeaveCode.Location = New System.Drawing.Point(141, 70)
        Me.txtLeaveCode.MendatroryField = False
        Me.txtLeaveCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLeaveCode.MyLinkLable1 = Me.MyLabel2
        Me.txtLeaveCode.MyLinkLable2 = Nothing
        Me.txtLeaveCode.MyReadOnly = False
        Me.txtLeaveCode.Name = "txtLeaveCode"
        Me.txtLeaveCode.Size = New System.Drawing.Size(222, 19)
        Me.txtLeaveCode.TabIndex = 3
        Me.txtLeaveCode.Value = ""
        '
        'dtpAllotStarting
        '
        Me.dtpAllotStarting.CustomFormat = "dd/MM/yyyy"
        Me.dtpAllotStarting.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpAllotStarting.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAllotStarting.Location = New System.Drawing.Point(141, 94)
        Me.dtpAllotStarting.MendatroryField = False
        Me.dtpAllotStarting.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.dtpAllotStarting.MyLinkLable1 = Me.lblOpeningDate
        Me.dtpAllotStarting.MyLinkLable2 = Nothing
        Me.dtpAllotStarting.Name = "dtpAllotStarting"
        Me.dtpAllotStarting.NullDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.dtpAllotStarting.Size = New System.Drawing.Size(108, 18)
        Me.dtpAllotStarting.TabIndex = 4
        Me.dtpAllotStarting.TabStop = False
        Me.dtpAllotStarting.Text = "03/05/2011"
        Me.dtpAllotStarting.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(81, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 21)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(564, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'frmLeaveStartingDateSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(638, 252)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmLeaveStartingDateSetting"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Leave Starting Date Setting"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOpeningDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpAvailStarting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpAllotStarting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents dtpAvailStarting As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents dtpAllotStarting As common.Controls.MyDateTimePicker
    Friend WithEvents txtLeaveCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblOpeningDate As common.Controls.MyLabel
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtFinder
End Class
