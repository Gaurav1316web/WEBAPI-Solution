<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmWTDRpt
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkLocSelect = New common.Controls.MyRadioButton
        Me.chkLocAll = New common.Controls.MyRadioButton
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.dtptyear = New common.Controls.MyDateTimePicker
        Me.dtpfyear = New common.Controls.MyDateTimePicker
        Me.dtptmonth = New common.Controls.MyDateTimePicker
        Me.dtpfmonth = New common.Controls.MyDateTimePicker
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cgvItem = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkIselect = New common.Controls.MyRadioButton
        Me.chkIAll = New common.Controls.MyRadioButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtptyear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpfyear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtptmonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpfmonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkIselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.dtptyear)
        Me.RadGroupBox1.Controls.Add(Me.dtpfyear)
        Me.RadGroupBox1.Controls.Add(Me.dtptmonth)
        Me.RadGroupBox1.Controls.Add(Me.dtpfmonth)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(450, 559)
        Me.RadGroupBox1.TabIndex = 0
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox3.Controls.Add(Me.Panel2)
        Me.RadGroupBox3.HeaderText = "Location"
        Me.RadGroupBox3.Location = New System.Drawing.Point(10, 319)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(431, 225)
        Me.RadGroupBox3.TabIndex = 318
        Me.RadGroupBox3.Text = "Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 40)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(411, 175)
        Me.cbgLocation.TabIndex = 2
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkLocSelect)
        Me.Panel2.Controls.Add(Me.chkLocAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(411, 20)
        Me.Panel2.TabIndex = 1
        '
        'chkLocSelect
        '
        Me.chkLocSelect.Location = New System.Drawing.Point(189, 1)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 2
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.Location = New System.Drawing.Point(140, 1)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 1
        Me.chkLocAll.Text = "All"
        '
        'MyLabel3
        '
        Me.MyLabel3.Location = New System.Drawing.Point(263, 50)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(44, 18)
        Me.MyLabel3.TabIndex = 316
        Me.MyLabel3.Text = "To Year"
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(261, 11)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel1.TabIndex = 316
        Me.MyLabel1.Text = "To Month"
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(20, 50)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(57, 18)
        Me.MyLabel2.TabIndex = 316
        Me.MyLabel2.Text = "From Year"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(20, 11)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(69, 18)
        Me.RadLabel1.TabIndex = 317
        Me.RadLabel1.Text = "From Month"
        '
        'dtptyear
        '
        Me.dtptyear.CustomFormat = "yyyy"
        Me.dtptyear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtptyear.Location = New System.Drawing.Point(344, 48)
        Me.dtptyear.MendatroryField = False
        Me.dtptyear.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptyear.MyLinkLable1 = Nothing
        Me.dtptyear.MyLinkLable2 = Nothing
        Me.dtptyear.Name = "dtptyear"
        Me.dtptyear.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptyear.Size = New System.Drawing.Size(87, 20)
        Me.dtptyear.TabIndex = 4
        Me.dtptyear.TabStop = False
        Me.dtptyear.Text = "2013"
        Me.dtptyear.Value = New Date(2013, 5, 29, 0, 0, 0, 0)
        '
        'dtpfyear
        '
        Me.dtpfyear.CustomFormat = "yyyy"
        Me.dtpfyear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfyear.Location = New System.Drawing.Point(114, 48)
        Me.dtpfyear.MendatroryField = False
        Me.dtpfyear.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfyear.MyLinkLable1 = Nothing
        Me.dtpfyear.MyLinkLable2 = Nothing
        Me.dtpfyear.Name = "dtpfyear"
        Me.dtpfyear.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfyear.Size = New System.Drawing.Size(91, 20)
        Me.dtpfyear.TabIndex = 3
        Me.dtpfyear.TabStop = False
        Me.dtpfyear.Text = "2013"
        Me.dtpfyear.Value = New Date(2013, 5, 29, 0, 0, 0, 0)
        '
        'dtptmonth
        '
        Me.dtptmonth.CustomFormat = "MMM"
        Me.dtptmonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtptmonth.Location = New System.Drawing.Point(344, 11)
        Me.dtptmonth.MendatroryField = False
        Me.dtptmonth.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptmonth.MyLinkLable1 = Nothing
        Me.dtptmonth.MyLinkLable2 = Nothing
        Me.dtptmonth.Name = "dtptmonth"
        Me.dtptmonth.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptmonth.Size = New System.Drawing.Size(87, 20)
        Me.dtptmonth.TabIndex = 2
        Me.dtptmonth.TabStop = False
        Me.dtptmonth.Text = "May"
        Me.dtptmonth.Value = New Date(2013, 5, 29, 0, 0, 0, 0)
        '
        'dtpfmonth
        '
        Me.dtpfmonth.CustomFormat = "MMM"
        Me.dtpfmonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfmonth.Location = New System.Drawing.Point(114, 11)
        Me.dtpfmonth.MendatroryField = False
        Me.dtpfmonth.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfmonth.MyLinkLable1 = Nothing
        Me.dtpfmonth.MyLinkLable2 = Nothing
        Me.dtpfmonth.Name = "dtpfmonth"
        Me.dtpfmonth.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfmonth.Size = New System.Drawing.Size(91, 20)
        Me.dtpfmonth.TabIndex = 1
        Me.dtpfmonth.TabStop = False
        Me.dtpfmonth.Text = "May"
        Me.dtpfmonth.Value = New Date(2013, 5, 29, 0, 0, 0, 0)
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cgvItem)
        Me.RadGroupBox2.Controls.Add(Me.Panel1)
        Me.RadGroupBox2.HeaderText = "Item"
        Me.RadGroupBox2.Location = New System.Drawing.Point(10, 90)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(431, 223)
        Me.RadGroupBox2.TabIndex = 311
        Me.RadGroupBox2.Text = "Item"
        '
        'cgvItem
        '
        Me.cgvItem.CheckedValue = Nothing
        Me.cgvItem.DataSource = Nothing
        Me.cgvItem.DisplayMember = "Name"
        Me.cgvItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cgvItem.Location = New System.Drawing.Point(10, 40)
        Me.cgvItem.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cgvItem.MyShowHeadrText = False
        Me.cgvItem.Name = "cgvItem"
        Me.cgvItem.Size = New System.Drawing.Size(411, 173)
        Me.cgvItem.TabIndex = 2
        Me.cgvItem.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkIselect)
        Me.Panel1.Controls.Add(Me.chkIAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(411, 20)
        Me.Panel1.TabIndex = 1
        '
        'chkIselect
        '
        Me.chkIselect.Location = New System.Drawing.Point(189, 1)
        Me.chkIselect.MyLinkLable1 = Nothing
        Me.chkIselect.MyLinkLable2 = Nothing
        Me.chkIselect.Name = "chkIselect"
        Me.chkIselect.Size = New System.Drawing.Size(50, 18)
        Me.chkIselect.TabIndex = 2
        Me.chkIselect.Text = "Select"
        '
        'chkIAll
        '
        Me.chkIAll.Location = New System.Drawing.Point(140, 1)
        Me.chkIAll.MyLinkLable1 = Nothing
        Me.chkIAll.MyLinkLable2 = Nothing
        Me.chkIAll.Name = "chkIAll"
        Me.chkIAll.Size = New System.Drawing.Size(33, 18)
        Me.chkIAll.TabIndex = 1
        Me.chkIAll.Text = "All"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(394, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(12, 5)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 6
        Me.btnReset.Text = "Reset"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(86, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 5
        Me.btnPrint.Text = "Print"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(474, 595)
        Me.SplitContainer1.SplitterDistance = 565
        Me.SplitContainer1.TabIndex = 1
        '
        'FrmWTDRpt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(474, 595)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmWTDRpt"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RM Chart"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtptyear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpfyear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtptmonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpfmonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkIselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cgvItem As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkIselect As common.Controls.MyRadioButton
    Friend WithEvents chkIAll As common.Controls.MyRadioButton
    Friend WithEvents dtptyear As common.Controls.MyDateTimePicker
    Friend WithEvents dtpfyear As common.Controls.MyDateTimePicker
    Friend WithEvents dtptmonth As common.Controls.MyDateTimePicker
    Friend WithEvents dtpfmonth As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

