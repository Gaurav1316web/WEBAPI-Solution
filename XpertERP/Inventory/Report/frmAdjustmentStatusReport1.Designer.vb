<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAdjustmentStatusReport1
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadioBtnSummary = New Telerik.WinControls.UI.RadRadioButton
        Me.RadioBtnDetail = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.optPosted = New Telerik.WinControls.UI.RadRadioButton
        Me.optAll = New Telerik.WinControls.UI.RadRadioButton
        Me.dtpend = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgItem = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkItemSelect = New common.Controls.MyRadioButton
        Me.chkIemAll = New common.Controls.MyRadioButton
        Me.RadLabel7 = New common.Controls.MyLabel
        Me.dtpstart = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadGroupBox13 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.chkLocationSelect = New common.Controls.MyRadioButton
        Me.chkLocationAll = New common.Controls.MyRadioButton
        Me.RadLabel8 = New common.Controls.MyLabel
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgtype = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chktypeSelect = New common.Controls.MyRadioButton
        Me.chktypeAll = New common.Controls.MyRadioButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadioBtnSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioBtnDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.optPosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIemAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpstart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox13.SuspendLayout()
        Me.Panel9.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chktypeSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chktypeAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadioBtnSummary)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadioBtnDetail)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpend)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpstart)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox13)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox6)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Size = New System.Drawing.Size(738, 513)
        Me.SplitContainer1.SplitterDistance = 481
        Me.SplitContainer1.TabIndex = 0
        '
        'RadioBtnSummary
        '
        Me.RadioBtnSummary.Location = New System.Drawing.Point(15, 50)
        Me.RadioBtnSummary.Name = "RadioBtnSummary"
        Me.RadioBtnSummary.Size = New System.Drawing.Size(77, 18)
        Me.RadioBtnSummary.TabIndex = 322
        Me.RadioBtnSummary.Text = "Summary"
        Me.RadioBtnSummary.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadioBtnDetail
        '
        Me.RadioBtnDetail.Location = New System.Drawing.Point(98, 50)
        Me.RadioBtnDetail.Name = "RadioBtnDetail"
        Me.RadioBtnDetail.Size = New System.Drawing.Size(67, 18)
        Me.RadioBtnDetail.TabIndex = 321
        Me.RadioBtnDetail.Text = "Detail"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.optPosted)
        Me.RadGroupBox1.Controls.Add(Me.optAll)
        Me.RadGroupBox1.FooterImageIndex = -1
        Me.RadGroupBox1.FooterImageKey = ""
        Me.RadGroupBox1.HeaderImageIndex = -1
        Me.RadGroupBox1.HeaderImageKey = ""
        Me.RadGroupBox1.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(548, 10)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox1.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(132, 30)
        Me.RadGroupBox1.TabIndex = 320
        '
        'optPosted
        '
        Me.optPosted.Location = New System.Drawing.Point(58, 5)
        Me.optPosted.Name = "optPosted"
        Me.optPosted.Size = New System.Drawing.Size(67, 18)
        Me.optPosted.TabIndex = 30
        Me.optPosted.Text = "Pending"
        '
        'optAll
        '
        Me.optAll.Location = New System.Drawing.Point(13, 5)
        Me.optAll.Name = "optAll"
        Me.optAll.Size = New System.Drawing.Size(39, 18)
        Me.optAll.TabIndex = 30
        Me.optAll.Text = "All"
        Me.optAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'dtpend
        '
        Me.dtpend.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpend.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.dtpend.Location = New System.Drawing.Point(366, 10)
        Me.dtpend.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpend.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpend.Name = "dtpend"
        Me.dtpend.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpend.Size = New System.Drawing.Size(134, 20)
        Me.dtpend.TabIndex = 14
        Me.dtpend.Text = "RadDateTimePicker1"
        Me.dtpend.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgItem)
        Me.RadGroupBox2.Controls.Add(Me.Panel1)
        Me.RadGroupBox2.FooterImageIndex = -1
        Me.RadGroupBox2.FooterImageKey = ""
        Me.RadGroupBox2.HeaderImageIndex = -1
        Me.RadGroupBox2.HeaderImageKey = ""
        Me.RadGroupBox2.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox2.HeaderText = "Item"
        Me.RadGroupBox2.Location = New System.Drawing.Point(5, 278)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox2.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(732, 183)
        Me.RadGroupBox2.TabIndex = 315
        Me.RadGroupBox2.Text = "Item"
        '
        'cbgItem
        '
        Me.cbgItem.CheckedValue = Nothing
        Me.cbgItem.DataSource = Nothing
        Me.cbgItem.DisplayMember = "Name"
        Me.cbgItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgItem.Location = New System.Drawing.Point(10, 40)
        Me.cbgItem.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgItem.MyShowHeadrText = False
        Me.cbgItem.Name = "cbgItem"
        Me.cbgItem.Size = New System.Drawing.Size(712, 133)
        Me.cbgItem.TabIndex = 2
        Me.cbgItem.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkItemSelect)
        Me.Panel1.Controls.Add(Me.chkIemAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(712, 20)
        Me.Panel1.TabIndex = 1
        '
        'chkItemSelect
        '
        Me.chkItemSelect.Location = New System.Drawing.Point(348, 1)
        Me.chkItemSelect.MyLinkLable1 = Nothing
        Me.chkItemSelect.MyLinkLable2 = Nothing
        Me.chkItemSelect.Name = "chkItemSelect"
        Me.chkItemSelect.Size = New System.Drawing.Size(71, 18)
        Me.chkItemSelect.TabIndex = 2
        Me.chkItemSelect.Text = "Select"
        '
        'chkIemAll
        '
        Me.chkIemAll.Location = New System.Drawing.Point(299, 1)
        Me.chkIemAll.MyLinkLable1 = Nothing
        Me.chkIemAll.MyLinkLable2 = Nothing
        Me.chkIemAll.Name = "chkIemAll"
        Me.chkIemAll.Size = New System.Drawing.Size(45, 18)
        Me.chkIemAll.TabIndex = 1
        Me.chkIemAll.Text = "All"
        '
        'RadLabel7
        '
        Me.RadLabel7.Location = New System.Drawing.Point(12, 12)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(62, 18)
        Me.RadLabel7.TabIndex = 15
        Me.RadLabel7.Text = "From  Date"
        '
        'dtpstart
        '
        Me.dtpstart.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpstart.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.dtpstart.Location = New System.Drawing.Point(120, 12)
        Me.dtpstart.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpstart.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpstart.Name = "dtpstart"
        Me.dtpstart.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpstart.Size = New System.Drawing.Size(134, 20)
        Me.dtpstart.TabIndex = 13
        Me.dtpstart.Text = "RadDateTimePicker1"
        Me.dtpstart.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'RadGroupBox13
        '
        Me.RadGroupBox13.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox13.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox13.Controls.Add(Me.Panel9)
        Me.RadGroupBox13.FooterImageIndex = -1
        Me.RadGroupBox13.FooterImageKey = ""
        Me.RadGroupBox13.HeaderImageIndex = -1
        Me.RadGroupBox13.HeaderImageKey = ""
        Me.RadGroupBox13.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox13.HeaderText = " Location"
        Me.RadGroupBox13.Location = New System.Drawing.Point(377, 81)
        Me.RadGroupBox13.Name = "RadGroupBox13"
        Me.RadGroupBox13.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox13.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox13.Size = New System.Drawing.Size(360, 191)
        Me.RadGroupBox13.TabIndex = 314
        Me.RadGroupBox13.Text = " Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.AccessibleDescription = "cbgLocation"
        Me.cbgLocation.AccessibleName = ""
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 45)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(340, 136)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.chkLocationSelect)
        Me.Panel9.Controls.Add(Me.chkLocationAll)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(10, 20)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(340, 25)
        Me.Panel9.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.AccessibleDescription = "chkDoc_select"
        Me.chkLocationSelect.Location = New System.Drawing.Point(161, 3)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(71, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.AccessibleDescription = "chkdocAll"
        Me.chkLocationAll.Location = New System.Drawing.Point(110, 3)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(45, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'RadLabel8
        '
        Me.RadLabel8.Location = New System.Drawing.Point(286, 12)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(52, 18)
        Me.RadLabel8.TabIndex = 16
        Me.RadLabel8.Text = "End Date"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.cbgtype)
        Me.RadGroupBox6.Controls.Add(Me.Panel4)
        Me.RadGroupBox6.FooterImageIndex = -1
        Me.RadGroupBox6.FooterImageKey = ""
        Me.RadGroupBox6.HeaderImageIndex = -1
        Me.RadGroupBox6.HeaderImageKey = ""
        Me.RadGroupBox6.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox6.HeaderText = "Type"
        Me.RadGroupBox6.Location = New System.Drawing.Point(2, 83)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox6.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(359, 189)
        Me.RadGroupBox6.TabIndex = 313
        Me.RadGroupBox6.Text = "Type"
        '
        'cbgtype
        '
        Me.cbgtype.CheckedValue = Nothing
        Me.cbgtype.DataSource = Nothing
        Me.cbgtype.DisplayMember = "Name"
        Me.cbgtype.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgtype.Location = New System.Drawing.Point(10, 40)
        Me.cbgtype.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgtype.MyShowHeadrText = False
        Me.cbgtype.Name = "cbgtype"
        Me.cbgtype.Size = New System.Drawing.Size(339, 139)
        Me.cbgtype.TabIndex = 1
        Me.cbgtype.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chktypeSelect)
        Me.Panel4.Controls.Add(Me.chktypeAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(339, 20)
        Me.Panel4.TabIndex = 0
        '
        'chktypeSelect
        '
        Me.chktypeSelect.Location = New System.Drawing.Point(163, 1)
        Me.chktypeSelect.MyLinkLable1 = Nothing
        Me.chktypeSelect.MyLinkLable2 = Nothing
        Me.chktypeSelect.Name = "chktypeSelect"
        Me.chktypeSelect.Size = New System.Drawing.Size(71, 18)
        Me.chktypeSelect.TabIndex = 1
        Me.chktypeSelect.Text = "Select"
        '
        'chktypeAll
        '
        Me.chktypeAll.Location = New System.Drawing.Point(112, 1)
        Me.chktypeAll.MyLinkLable1 = Nothing
        Me.chktypeAll.MyLinkLable2 = Nothing
        Me.chktypeAll.Name = "chktypeAll"
        Me.chktypeAll.Size = New System.Drawing.Size(45, 18)
        Me.chktypeAll.TabIndex = 0
        Me.chktypeAll.Text = "All"
        '
        'btnclose
        '
        Me.btnclose.Location = New System.Drawing.Point(667, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 9
        Me.btnclose.Text = "Close"
        '
        'btnreset
        '
        Me.btnreset.Location = New System.Drawing.Point(90, 6)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 18)
        Me.btnreset.TabIndex = 8
        Me.btnreset.Text = "Reset"
        '
        'btnprint
        '
        Me.btnprint.Location = New System.Drawing.Point(8, 6)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(68, 18)
        Me.btnprint.TabIndex = 7
        Me.btnprint.Text = "Print"
        '
        'FrmAdjustmentStatusReport1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(738, 513)
        Me.Controls.Add(Me.SplitContainer1)
        Me.KeyPreview = True
        Me.Name = "FrmAdjustmentStatusReport1"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Adjustment Status Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadioBtnSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioBtnDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.optPosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIemAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpstart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox13, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox13.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.chktypeSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chktypeAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkItemSelect As common.Controls.MyRadioButton
    Friend WithEvents chkIemAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox13 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgtype As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chktypeSelect As common.Controls.MyRadioButton
    Friend WithEvents chktypeAll As common.Controls.MyRadioButton
    Friend WithEvents dtpend As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents dtpstart As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents optPosted As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents optAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadioBtnSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadioBtnDetail As Telerik.WinControls.UI.RadRadioButton
End Class

