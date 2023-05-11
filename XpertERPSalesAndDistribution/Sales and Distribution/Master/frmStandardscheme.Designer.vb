<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStandardscheme
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
        ' Me.grdfndSch = New finder.gridFinder
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.fndCustomer = New common.UserControls.txtFinder
        Me.fndCode = New common.UserControls.txtNavigator
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.dtpEnd = New common.Controls.MyDateTimePicker
        Me.RadLabel5 = New common.Controls.MyLabel
        Me.dtpStart = New common.Controls.MyDateTimePicker
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.txtDesc = New common.Controls.MyTextBox
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel4 = New common.Controls.MyLabel
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.Filemenu = New Telerik.WinControls.UI.RadMenuItem
        Me.importmenu = New Telerik.WinControls.UI.RadMenuItem
        Me.exportmenu = New Telerik.WinControls.UI.RadMenuItem
        Me.Exitmenu = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadGroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.fndCustomer)
        Me.RadGroupBox1.Controls.Add(Me.fndCode)
        Me.RadGroupBox1.Controls.Add(Me.btnReset)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox1)
        Me.RadGroupBox1.Controls.Add(Me.dtpEnd)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel5)
        Me.RadGroupBox1.Controls.Add(Me.dtpStart)
        Me.RadGroupBox1.Controls.Add(Me.txtDesc)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(997, 533)
        Me.RadGroupBox1.TabIndex = 0
        '
        'fndCustomer
        '
        Me.fndCustomer.Location = New System.Drawing.Point(117, 35)
        Me.fndCustomer.MendatroryField = False
        Me.fndCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCustomer.MyLinkLable1 = Nothing
        Me.fndCustomer.MyLinkLable2 = Nothing
        Me.fndCustomer.MyReadOnly = False
        Me.fndCustomer.Name = "fndCustomer"
        Me.fndCustomer.Size = New System.Drawing.Size(238, 18)
        Me.fndCustomer.TabIndex = 3
        Me.fndCustomer.Value = ""
        '
        'fndCode
        '
        Me.fndCode.Location = New System.Drawing.Point(117, 9)
        Me.fndCode.MendatroryField = True
        Me.fndCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCode.MyLinkLable1 = Me.RadLabel1
        Me.fndCode.MyLinkLable2 = Nothing
        Me.fndCode.MyMaxLength = 32767
        Me.fndCode.MyReadOnly = False
        Me.fndCode.Name = "fndCode"
        Me.fndCode.Size = New System.Drawing.Size(218, 18)
        Me.fndCode.TabIndex = 0
        Me.fndCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.RadLabel1.Location = New System.Drawing.Point(8, 10)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel1.TabIndex = 7
        Me.RadLabel1.Text = "Code"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnReset.Location = New System.Drawing.Point(338, 9)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(17, 17)
        Me.btnReset.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.gv1)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 55)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(970, 474)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Scheme Details"
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
        Me.gv1.Size = New System.Drawing.Size(964, 453)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'dtpEnd
        '
        Me.dtpEnd.CustomFormat = "dd/MM/yyyy"
        Me.dtpEnd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEnd.Location = New System.Drawing.Point(700, 32)
        Me.dtpEnd.MendatroryField = False
        Me.dtpEnd.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEnd.MyLinkLable1 = Me.RadLabel5
        Me.dtpEnd.MyLinkLable2 = Nothing
        Me.dtpEnd.Name = "dtpEnd"
        Me.dtpEnd.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEnd.ShowCheckBox = True
        Me.dtpEnd.Size = New System.Drawing.Size(103, 18)
        Me.dtpEnd.TabIndex = 5
        Me.dtpEnd.TabStop = False
        Me.dtpEnd.Text = "17/05/2011"
        Me.dtpEnd.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'RadLabel5
        '
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(625, 33)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(76, 16)
        Me.RadLabel5.TabIndex = 11
        Me.RadLabel5.Text = "Valid Till Date"
        '
        'dtpStart
        '
        Me.dtpStart.CustomFormat = "dd/MM/yyyy"
        Me.dtpStart.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStart.Location = New System.Drawing.Point(506, 32)
        Me.dtpStart.MendatroryField = False
        Me.dtpStart.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStart.MyLinkLable1 = Me.RadLabel3
        Me.dtpStart.MyLinkLable2 = Nothing
        Me.dtpStart.Name = "dtpStart"
        Me.dtpStart.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStart.Size = New System.Drawing.Size(106, 18)
        Me.dtpStart.TabIndex = 4
        Me.dtpStart.TabStop = False
        Me.dtpStart.Text = "17/05/2011"
        Me.dtpStart.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'RadLabel3
        '
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(420, 33)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel3.TabIndex = 8
        Me.RadLabel3.Text = "Date"
        '
        'txtDesc
        '
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(506, 9)
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel2
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(297, 18)
        Me.txtDesc.TabIndex = 2
        '
        'RadLabel2
        '
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(420, 10)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel2.TabIndex = 9
        Me.RadLabel2.Text = "Description"
        '
        'RadLabel4
        '
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(8, 33)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel4.TabIndex = 6
        Me.RadLabel4.Text = "Customer"
        '
        'btnclose
        '
        Me.btnclose.AccessibleDescription = ""
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(947, 8)
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
        Me.btnDelete.Location = New System.Drawing.Point(70, 8)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(66, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Filemenu})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1016, 20)
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
        Me.Filemenu.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        '
        'importmenu
        '
        Me.importmenu.AccessibleDescription = "Import"
        Me.importmenu.AccessibleName = "Import"
        Me.importmenu.Name = "importmenu"
        Me.importmenu.Text = "Import"
        Me.importmenu.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'exportmenu
        '
        Me.exportmenu.AccessibleDescription = "Export"
        Me.exportmenu.AccessibleName = "Export"
        Me.exportmenu.Name = "exportmenu"
        Me.exportmenu.Text = "Export"
        Me.exportmenu.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        Me.SplitContainer1.IsSplitterFixed = True
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1016, 572)
        Me.SplitContainer1.SplitterDistance = 538
        Me.SplitContainer1.TabIndex = 0
        '
        'frmStandardscheme
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 592)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmStandardscheme"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Standard Scheme"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    ' Friend WithEvents grdfndSch As finder.gridFinder
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpEnd As common.Controls.MyDateTimePicker
    Friend WithEvents dtpStart As common.Controls.MyDateTimePicker
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents Filemenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents importmenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents exportmenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Exitmenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents fndCode As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents fndCustomer As common.UserControls.txtFinder
End Class

