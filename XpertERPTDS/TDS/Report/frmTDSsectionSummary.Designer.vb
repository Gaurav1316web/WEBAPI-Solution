Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTDSsectionSummary
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
        Me.dtpfromdate = New common.Controls.MyDateTimePicker
        Me.lblfrom = New common.Controls.MyLabel
        Me.lblTo = New common.Controls.MyLabel
        Me.dtpTodate = New common.Controls.MyDateTimePicker
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.ChkLocSelect = New common.Controls.MyRadioButton
        Me.chkLocAll = New common.Controls.MyRadioButton
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkSelect = New common.Controls.MyRadioButton
        Me.chkAll = New common.Controls.MyRadioButton
        Me.cbgSection = New common.MyCheckBoxGrid
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpTodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.ChkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtpfromdate
        '
        Me.dtpfromdate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfromdate.Location = New System.Drawing.Point(92, 23)
        Me.dtpfromdate.MendatroryField = False
        Me.dtpfromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate.MyLinkLable1 = Nothing
        Me.dtpfromdate.MyLinkLable2 = Nothing
        Me.dtpfromdate.Name = "dtpfromdate"
        Me.dtpfromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate.Size = New System.Drawing.Size(101, 20)
        Me.dtpfromdate.TabIndex = 0
        Me.dtpfromdate.TabStop = False
        Me.dtpfromdate.Text = "23/05/2012 12:00 AM"
        Me.dtpfromdate.Value = New Date(2012, 5, 23, 0, 0, 0, 0)
        '
        'lblfrom
        '
        Me.lblfrom.Location = New System.Drawing.Point(23, 25)
        Me.lblfrom.Name = "lblfrom"
        Me.lblfrom.Size = New System.Drawing.Size(32, 18)
        Me.lblfrom.TabIndex = 2
        Me.lblfrom.Text = "From"
        '
        'lblTo
        '
        Me.lblTo.Location = New System.Drawing.Point(239, 23)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(19, 18)
        Me.lblTo.TabIndex = 2
        Me.lblTo.Text = "To"
        '
        'dtpTodate
        '
        Me.dtpTodate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpTodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTodate.Location = New System.Drawing.Point(290, 23)
        Me.dtpTodate.MendatroryField = False
        Me.dtpTodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTodate.MyLinkLable1 = Nothing
        Me.dtpTodate.MyLinkLable2 = Nothing
        Me.dtpTodate.Name = "dtpTodate"
        Me.dtpTodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTodate.Size = New System.Drawing.Size(99, 20)
        Me.dtpTodate.TabIndex = 1
        Me.dtpTodate.TabStop = False
        Me.dtpTodate.Text = "23/05/2012 12:00 AM"
        Me.dtpTodate.Value = New Date(2012, 5, 23, 0, 0, 0, 0)
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(93, 3)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(75, 21)
        Me.btnprint.TabIndex = 3
        Me.btnprint.Text = "Print"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(339, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(79, 21)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "Close"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox6)
        Me.RadGroupBox1.Controls.Add(Me.lblfrom)
        Me.RadGroupBox1.Controls.Add(Me.dtpfromdate)
        Me.RadGroupBox1.Controls.Add(Me.dtpTodate)
        Me.RadGroupBox1.Controls.Add(Me.lblTo)
        Me.RadGroupBox1.HeaderText = "Document Date"
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(412, 376)
        Me.RadGroupBox1.TabIndex = 5
        Me.RadGroupBox1.Text = "Document Date"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.Panel3)
        Me.RadGroupBox2.HeaderText = "Location"
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 215)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(386, 158)
        Me.RadGroupBox2.TabIndex = 6
        Me.RadGroupBox2.Text = "Location"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Controls.Add(Me.cbgLocation)
        Me.Panel3.Location = New System.Drawing.Point(13, 18)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(363, 127)
        Me.Panel3.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.ChkLocSelect)
        Me.Panel4.Controls.Add(Me.chkLocAll)
        Me.Panel4.Location = New System.Drawing.Point(3, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(357, 25)
        Me.Panel4.TabIndex = 1
        '
        'ChkLocSelect
        '
        Me.ChkLocSelect.Location = New System.Drawing.Point(187, 5)
        Me.ChkLocSelect.MyLinkLable1 = Nothing
        Me.ChkLocSelect.MyLinkLable2 = Nothing
        Me.ChkLocSelect.Name = "ChkLocSelect"
        Me.ChkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.ChkLocSelect.TabIndex = 1
        Me.ChkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.Location = New System.Drawing.Point(121, 4)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 0
        Me.chkLocAll.Text = "All"
        '
        'cbgLocation
        '
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Location = New System.Drawing.Point(3, 28)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(357, 96)
        Me.cbgLocation.TabIndex = 0
        Me.cbgLocation.ValueMember = "Code"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.Panel1)
        Me.RadGroupBox6.HeaderText = "Section No"
        Me.RadGroupBox6.Location = New System.Drawing.Point(13, 49)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(386, 158)
        Me.RadGroupBox6.TabIndex = 5
        Me.RadGroupBox6.Text = "Section No"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.cbgSection)
        Me.Panel1.Location = New System.Drawing.Point(13, 18)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(363, 127)
        Me.Panel1.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkSelect)
        Me.Panel2.Controls.Add(Me.chkAll)
        Me.Panel2.Location = New System.Drawing.Point(3, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(357, 25)
        Me.Panel2.TabIndex = 1
        '
        'chkSelect
        '
        Me.chkSelect.Location = New System.Drawing.Point(187, 5)
        Me.chkSelect.MyLinkLable1 = Nothing
        Me.chkSelect.MyLinkLable2 = Nothing
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkSelect.TabIndex = 1
        Me.chkSelect.Text = "Select"
        '
        'chkAll
        '
        Me.chkAll.Location = New System.Drawing.Point(95, 5)
        Me.chkAll.MyLinkLable1 = Nothing
        Me.chkAll.MyLinkLable2 = Nothing
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(33, 18)
        Me.chkAll.TabIndex = 0
        Me.chkAll.Text = "All"
        '
        'cbgSection
        '
        Me.cbgSection.CheckedValue = Nothing
        Me.cbgSection.DataSource = Nothing
        Me.cbgSection.DisplayMember = "Name"
        Me.cbgSection.Location = New System.Drawing.Point(3, 28)
        Me.cbgSection.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgSection.MyShowHeadrText = False
        Me.cbgSection.Name = "cbgSection"
        Me.cbgSection.Size = New System.Drawing.Size(357, 96)
        Me.cbgSection.TabIndex = 0
        Me.cbgSection.ValueMember = "Code"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(12, 3)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(75, 21)
        Me.btnreset.TabIndex = 4
        Me.btnreset.Text = "Reset"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(431, 414)
        Me.SplitContainer1.SplitterDistance = 382
        Me.SplitContainer1.TabIndex = 6
        '
        'FrmTDSsectionSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(431, 414)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmTDSsectionSummary"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "TDS Section Summary Report"
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpTodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.ChkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub
    Friend WithEvents dtpfromdate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpTodate As common.Controls.MyDateTimePicker
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblfrom As common.Controls.MyLabel
    Friend WithEvents lblTo As common.Controls.MyLabel
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cbgSection As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkSelect As common.Controls.MyRadioButton
    Friend WithEvents chkAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents ChkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

