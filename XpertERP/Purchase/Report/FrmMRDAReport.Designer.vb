<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMRDAReport
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
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkLocSelect = New common.Controls.MyRadioButton
        Me.chkLocAll = New common.Controls.MyRadioButton
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkvenSelect = New common.Controls.MyRadioButton
        Me.chkvendorAll = New common.Controls.MyRadioButton
        Me.cgvvendor = New common.MyCheckBoxGrid
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.mrdaselect = New common.Controls.MyRadioButton
        Me.rdomrdaall = New common.Controls.MyRadioButton
        Me.cgvMRDA = New common.MyCheckBoxGrid
        Me.dtpToDate = New common.Controls.MyDateTimePicker
        Me.dtpFromDate = New common.Controls.MyDateTimePicker
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkvenSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkvendorAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.mrdaselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdomrdaall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.dtpToDate)
        Me.RadGroupBox1.Controls.Add(Me.dtpFromDate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(24, 14)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(436, 571)
        Me.RadGroupBox1.TabIndex = 0
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.Panel3)
        Me.RadGroupBox4.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox4.HeaderText = "Location"
        Me.RadGroupBox4.Location = New System.Drawing.Point(3, 380)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(420, 178)
        Me.RadGroupBox4.TabIndex = 21
        Me.RadGroupBox4.Text = "Location"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkLocSelect)
        Me.Panel3.Controls.Add(Me.chkLocAll)
        Me.Panel3.Location = New System.Drawing.Point(5, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(405, 26)
        Me.Panel3.TabIndex = 4
        '
        'chkLocSelect
        '
        Me.chkLocSelect.Location = New System.Drawing.Point(168, 4)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 1
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.Location = New System.Drawing.Point(117, 5)
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
        Me.cbgLocation.Location = New System.Drawing.Point(5, 47)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(405, 118)
        Me.cbgLocation.TabIndex = 3
        Me.cbgLocation.ValueMember = "Code"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.Panel1)
        Me.RadGroupBox3.Controls.Add(Me.cgvvendor)
        Me.RadGroupBox3.HeaderText = "Vendor"
        Me.RadGroupBox3.Location = New System.Drawing.Point(8, 196)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(420, 178)
        Me.RadGroupBox3.TabIndex = 15
        Me.RadGroupBox3.Text = "Vendor"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkvenSelect)
        Me.Panel1.Controls.Add(Me.chkvendorAll)
        Me.Panel1.Location = New System.Drawing.Point(5, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(405, 26)
        Me.Panel1.TabIndex = 4
        '
        'chkvenSelect
        '
        Me.chkvenSelect.Location = New System.Drawing.Point(168, 4)
        Me.chkvenSelect.MyLinkLable1 = Nothing
        Me.chkvenSelect.MyLinkLable2 = Nothing
        Me.chkvenSelect.Name = "chkvenSelect"
        Me.chkvenSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkvenSelect.TabIndex = 1
        Me.chkvenSelect.Text = "Select"
        '
        'chkvendorAll
        '
        Me.chkvendorAll.Location = New System.Drawing.Point(117, 5)
        Me.chkvendorAll.MyLinkLable1 = Nothing
        Me.chkvendorAll.MyLinkLable2 = Nothing
        Me.chkvendorAll.Name = "chkvendorAll"
        Me.chkvendorAll.Size = New System.Drawing.Size(33, 18)
        Me.chkvendorAll.TabIndex = 0
        Me.chkvendorAll.Text = "All"
        '
        'cgvvendor
        '
        Me.cgvvendor.CheckedValue = Nothing
        Me.cgvvendor.DataSource = Nothing
        Me.cgvvendor.DisplayMember = "Name"
        Me.cgvvendor.Location = New System.Drawing.Point(5, 47)
        Me.cgvvendor.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cgvvendor.MyShowHeadrText = False
        Me.cgvvendor.Name = "cgvvendor"
        Me.cgvvendor.Size = New System.Drawing.Size(405, 118)
        Me.cgvvendor.TabIndex = 3
        Me.cgvvendor.ValueMember = "Code"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.Panel2)
        Me.RadGroupBox2.Controls.Add(Me.cgvMRDA)
        Me.RadGroupBox2.HeaderText = "MRDA No"
        Me.RadGroupBox2.Location = New System.Drawing.Point(8, 29)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(420, 161)
        Me.RadGroupBox2.TabIndex = 14
        Me.RadGroupBox2.Text = "MRDA No"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.mrdaselect)
        Me.Panel2.Controls.Add(Me.rdomrdaall)
        Me.Panel2.Location = New System.Drawing.Point(6, 19)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(405, 25)
        Me.Panel2.TabIndex = 5
        '
        'mrdaselect
        '
        Me.mrdaselect.Location = New System.Drawing.Point(168, 2)
        Me.mrdaselect.MyLinkLable1 = Nothing
        Me.mrdaselect.MyLinkLable2 = Nothing
        Me.mrdaselect.Name = "mrdaselect"
        Me.mrdaselect.Size = New System.Drawing.Size(50, 18)
        Me.mrdaselect.TabIndex = 1
        Me.mrdaselect.Text = "Select"
        '
        'rdomrdaall
        '
        Me.rdomrdaall.Location = New System.Drawing.Point(117, 3)
        Me.rdomrdaall.MyLinkLable1 = Nothing
        Me.rdomrdaall.MyLinkLable2 = Nothing
        Me.rdomrdaall.Name = "rdomrdaall"
        Me.rdomrdaall.Size = New System.Drawing.Size(33, 18)
        Me.rdomrdaall.TabIndex = 0
        Me.rdomrdaall.Text = "All"
        '
        'cgvMRDA
        '
        Me.cgvMRDA.CheckedValue = Nothing
        Me.cgvMRDA.DataSource = Nothing
        Me.cgvMRDA.DisplayMember = "Name"
        Me.cgvMRDA.Location = New System.Drawing.Point(6, 46)
        Me.cgvMRDA.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cgvMRDA.MyShowHeadrText = False
        Me.cgvMRDA.Name = "cgvMRDA"
        Me.cgvMRDA.Size = New System.Drawing.Size(405, 106)
        Me.cgvMRDA.TabIndex = 2
        Me.cgvMRDA.ValueMember = "Code"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(346, 6)
        Me.dtpToDate.MendatroryField = False
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Nothing
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(82, 20)
        Me.dtpToDate.TabIndex = 11
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "13-06-2011"
        Me.dtpToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(71, 6)
        Me.dtpFromDate.MendatroryField = False
        Me.dtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.MyLinkLable1 = Nothing
        Me.dtpFromDate.MyLinkLable2 = Nothing
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.Size = New System.Drawing.Size(82, 20)
        Me.dtpFromDate.TabIndex = 10
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "13-06-2011"
        Me.dtpFromDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(286, 7)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 13
        Me.RadLabel2.Text = "To Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(8, 7)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 12
        Me.RadLabel1.Text = "From Date"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(392, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 25)
        Me.btnClose.TabIndex = 20
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(12, 5)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 25)
        Me.btnReset.TabIndex = 19
        Me.btnReset.Text = "&Reset"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(86, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 25)
        Me.btnPrint.TabIndex = 18
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
        Me.SplitContainer1.Size = New System.Drawing.Size(486, 630)
        Me.SplitContainer1.SplitterDistance = 593
        Me.SplitContainer1.TabIndex = 1
        '
        'FrmMRDAReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(486, 630)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmMRDAReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "MRDA Report"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkvenSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkvendorAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.mrdaselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdomrdaall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents cgvvendor As common.MyCheckBoxGrid
    Friend WithEvents cgvMRDA As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents mrdaselect As common.Controls.MyRadioButton
    Friend WithEvents rdomrdaall As common.Controls.MyRadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkvenSelect As common.Controls.MyRadioButton
    Friend WithEvents chkvendorAll As common.Controls.MyRadioButton
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

