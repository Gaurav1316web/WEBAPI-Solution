<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAbateMentMaster
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
        Me.dtpEnd = New common.Controls.MyDateTimePicker
        Me.RadLabel5 = New common.Controls.MyLabel
        Me.dtpStart = New common.Controls.MyDateTimePicker
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.RadLabel10 = New common.Controls.MyLabel
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.txtDesc = New common.Controls.MyTextBox
        Me.RadLabel6 = New common.Controls.MyLabel
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.RadLabel4 = New common.Controls.MyLabel
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.fndAbatement = New common.UserControls.txtNavigator
        Me.txtRate = New common.MyNumBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        CType(Me.dtpEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtpEnd
        '
        Me.dtpEnd.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpEnd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEnd.Location = New System.Drawing.Point(374, 66)
        Me.dtpEnd.MendatroryField = False
        Me.dtpEnd.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEnd.MyLinkLable1 = Me.RadLabel5
        Me.dtpEnd.MyLinkLable2 = Nothing
        Me.dtpEnd.Name = "dtpEnd"
        Me.dtpEnd.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEnd.Size = New System.Drawing.Size(142, 18)
        Me.dtpEnd.TabIndex = 3
        Me.dtpEnd.TabStop = False
        Me.dtpEnd.Text = "17/05/2011 09:26 PM"
        Me.dtpEnd.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'RadLabel5
        '
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(306, 67)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(46, 16)
        Me.RadLabel5.TabIndex = 9
        Me.RadLabel5.Text = "To Date"
        '
        'dtpStart
        '
        Me.dtpStart.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpStart.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStart.Location = New System.Drawing.Point(116, 66)
        Me.dtpStart.MendatroryField = False
        Me.dtpStart.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStart.MyLinkLable1 = Me.RadLabel3
        Me.dtpStart.MyLinkLable2 = Nothing
        Me.dtpStart.Name = "dtpStart"
        Me.dtpStart.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStart.Size = New System.Drawing.Size(138, 18)
        Me.dtpStart.TabIndex = 2
        Me.dtpStart.TabStop = False
        Me.dtpStart.Text = "17/05/2011 09:26 PM"
        Me.dtpStart.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'RadLabel3
        '
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(6, 67)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel3.TabIndex = 6
        Me.RadLabel3.Text = "From Date"
        '
        'RadLabel10
        '
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.Location = New System.Drawing.Point(6, 90)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel10.TabIndex = 5
        Me.RadLabel10.Text = "Rate"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(142, 12)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(70, 20)
        Me.btnReset.TabIndex = 2
        Me.btnReset.Text = "Reset"
        '
        'btnDelete
        '
        Me.btnDelete.AccessibleDescription = ""
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(71, 12)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 20)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 12)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(67, 20)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'txtDesc
        '
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(116, 44)
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel6
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(467, 18)
        Me.txtDesc.TabIndex = 1
        '
        'RadLabel6
        '
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(6, 45)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel6.TabIndex = 7
        Me.RadLabel6.Text = "Description"
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(-164, 130)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel2.TabIndex = 18
        Me.RadLabel2.Text = "Description"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(-164, 93)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(75, 18)
        Me.RadLabel1.TabIndex = 17
        Me.RadLabel1.Text = "Scheme Code"
        '
        'RadLabel4
        '
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.RadLabel4.Location = New System.Drawing.Point(6, 21)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(96, 16)
        Me.RadLabel4.TabIndex = 8
        Me.RadLabel4.Text = "Abatement Code"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(536, 11)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(70, 20)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'fndAbatement
        '
        Me.fndAbatement.Location = New System.Drawing.Point(116, 20)
        Me.fndAbatement.MendatroryField = True
        Me.fndAbatement.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndAbatement.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndAbatement.MyLinkLable1 = Me.RadLabel4
        Me.fndAbatement.MyLinkLable2 = Nothing
        Me.fndAbatement.MyMaxLength = 32767
        Me.fndAbatement.MyReadOnly = False
        Me.fndAbatement.Name = "fndAbatement"
        Me.fndAbatement.Size = New System.Drawing.Size(265, 18)
        Me.fndAbatement.TabIndex = 0
        Me.fndAbatement.Value = ""
        '
        'txtRate
        '
        Me.txtRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtRate.DecimalPlaces = 2
        Me.txtRate.Location = New System.Drawing.Point(116, 90)
        Me.txtRate.MendatroryField = True
        Me.txtRate.MyLinkLable1 = Nothing
        Me.txtRate.MyLinkLable2 = Nothing
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(138, 20)
        Me.txtRate.TabIndex = 4
        Me.txtRate.Text = "0"
        Me.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRate.Value = 0
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(609, 206)
        Me.SplitContainer1.SplitterDistance = 168
        Me.SplitContainer1.TabIndex = 0
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(609, 20)
        Me.RadMenu1.TabIndex = 320
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmImport, Me.rmExport})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadLabel4)
        Me.GroupBox1.Controls.Add(Me.txtDesc)
        Me.GroupBox1.Controls.Add(Me.txtRate)
        Me.GroupBox1.Controls.Add(Me.RadLabel10)
        Me.GroupBox1.Controls.Add(Me.RadLabel3)
        Me.GroupBox1.Controls.Add(Me.dtpEnd)
        Me.GroupBox1.Controls.Add(Me.fndAbatement)
        Me.GroupBox1.Controls.Add(Me.RadLabel6)
        Me.GroupBox1.Controls.Add(Me.dtpStart)
        Me.GroupBox1.Controls.Add(Me.RadLabel5)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 17)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(594, 139)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        '
        'FrmAbateMentMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(609, 206)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadLabel2)
        Me.Controls.Add(Me.RadLabel1)
        Me.Name = "FrmAbateMentMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "AbateMent Master"
        CType(Me.dtpEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtpEnd As common.Controls.MyDateTimePicker
    Friend WithEvents dtpStart As common.Controls.MyDateTimePicker
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndAbatement As common.UserControls.txtNavigator
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents txtRate As common.MyNumBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
End Class

