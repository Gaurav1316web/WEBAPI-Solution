Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExciseChapterWise
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
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox
        Me.rdbDSR = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbMTD = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbChapterWise = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbtnStockReport = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbER1 = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgItem = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkItmSelect = New common.Controls.MyRadioButton
        Me.chkItmAll = New common.Controls.MyRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.frmDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.toDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLoc = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkLocSelect = New common.Controls.MyRadioButton
        Me.chkLocAll = New common.Controls.MyRadioButton
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgChapter = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkChapterSelect = New common.Controls.MyRadioButton
        Me.chkChapterAll = New common.Controls.MyRadioButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        CType(Me.rdbDSR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbMTD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbChapterWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnStockReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbER1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkItmSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItmAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.frmDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.toDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkChapterSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkChapterAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox6)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox5)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 12)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(561, 580)
        Me.RadGroupBox1.TabIndex = 4
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.rdbDSR)
        Me.RadGroupBox6.Controls.Add(Me.rdbMTD)
        Me.RadGroupBox6.Controls.Add(Me.rdbChapterWise)
        Me.RadGroupBox6.Controls.Add(Me.rdbtnStockReport)
        Me.RadGroupBox6.Controls.Add(Me.rdbER1)
        Me.RadGroupBox6.HeaderText = "Type"
        Me.RadGroupBox6.Location = New System.Drawing.Point(8, 63)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(540, 54)
        Me.RadGroupBox6.TabIndex = 5
        Me.RadGroupBox6.Text = "Type"
        '
        'rdbDSR
        '
        Me.rdbDSR.Location = New System.Drawing.Point(454, 23)
        Me.rdbDSR.Name = "rdbDSR"
        Me.rdbDSR.Size = New System.Drawing.Size(44, 18)
        Me.rdbDSR.TabIndex = 1
        Me.rdbDSR.Text = "DSR "
        '
        'rdbMTD
        '
        Me.rdbMTD.Location = New System.Drawing.Point(364, 23)
        Me.rdbMTD.Name = "rdbMTD"
        Me.rdbMTD.Size = New System.Drawing.Size(72, 18)
        Me.rdbMTD.TabIndex = 1
        Me.rdbMTD.Text = "MTD/YTD "
        '
        'rdbChapterWise
        '
        Me.rdbChapterWise.Location = New System.Drawing.Point(13, 23)
        Me.rdbChapterWise.Name = "rdbChapterWise"
        Me.rdbChapterWise.Size = New System.Drawing.Size(176, 18)
        Me.rdbChapterWise.TabIndex = 1
        Me.rdbChapterWise.Text = "Excise Duty Chapter Head Wise"
        '
        'rdbtnStockReport
        '
        Me.rdbtnStockReport.Location = New System.Drawing.Point(267, 23)
        Me.rdbtnStockReport.Name = "rdbtnStockReport"
        Me.rdbtnStockReport.Size = New System.Drawing.Size(84, 18)
        Me.rdbtnStockReport.TabIndex = 0
        Me.rdbtnStockReport.Text = "Stock Report"
        '
        'rdbER1
        '
        Me.rdbER1.Location = New System.Drawing.Point(208, 23)
        Me.rdbER1.Name = "rdbER1"
        Me.rdbER1.Size = New System.Drawing.Size(47, 18)
        Me.rdbER1.TabIndex = 0
        Me.rdbER1.Text = "E.R. 1"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgItem)
        Me.RadGroupBox4.Controls.Add(Me.Panel2)
        Me.RadGroupBox4.HeaderText = "Items"
        Me.RadGroupBox4.Location = New System.Drawing.Point(8, 274)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(543, 145)
        Me.RadGroupBox4.TabIndex = 312
        Me.RadGroupBox4.Text = "Items"
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
        Me.cbgItem.Size = New System.Drawing.Size(523, 95)
        Me.cbgItem.TabIndex = 2
        Me.cbgItem.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkItmSelect)
        Me.Panel2.Controls.Add(Me.chkItmAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(523, 20)
        Me.Panel2.TabIndex = 1
        '
        'chkItmSelect
        '
        Me.chkItmSelect.Location = New System.Drawing.Point(189, 1)
        Me.chkItmSelect.MyLinkLable1 = Nothing
        Me.chkItmSelect.MyLinkLable2 = Nothing
        Me.chkItmSelect.Name = "chkItmSelect"
        Me.chkItmSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkItmSelect.TabIndex = 2
        Me.chkItmSelect.Text = "Select"
        '
        'chkItmAll
        '
        Me.chkItmAll.Location = New System.Drawing.Point(140, 1)
        Me.chkItmAll.MyLinkLable1 = Nothing
        Me.chkItmAll.MyLinkLable2 = Nothing
        Me.chkItmAll.Name = "chkItmAll"
        Me.chkItmAll.Size = New System.Drawing.Size(33, 18)
        Me.chkItmAll.TabIndex = 1
        Me.chkItmAll.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox2.Controls.Add(Me.frmDate)
        Me.RadGroupBox2.Controls.Add(Me.toDate)
        Me.RadGroupBox2.HeaderText = "Date & Time"
        Me.RadGroupBox2.Location = New System.Drawing.Point(8, 3)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(540, 54)
        Me.RadGroupBox2.TabIndex = 3
        Me.RadGroupBox2.Text = "Date & Time"
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(219, 24)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(21, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To:"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(13, 23)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(34, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From:"
        '
        'frmDate
        '
        Me.frmDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.frmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.frmDate.Location = New System.Drawing.Point(53, 22)
        Me.frmDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.frmDate.Name = "frmDate"
        Me.frmDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.frmDate.Size = New System.Drawing.Size(142, 20)
        Me.frmDate.TabIndex = 0
        Me.frmDate.TabStop = False
        Me.frmDate.Text = "22/02/2012 12:30 PM"
        Me.frmDate.Value = New Date(2012, 2, 22, 12, 30, 21, 470)
        '
        'toDate
        '
        Me.toDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.toDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.toDate.Location = New System.Drawing.Point(245, 23)
        Me.toDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.toDate.Name = "toDate"
        Me.toDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.toDate.Size = New System.Drawing.Size(148, 20)
        Me.toDate.TabIndex = 1
        Me.toDate.TabStop = False
        Me.toDate.Text = "22/02/2012 12:30 PM"
        Me.toDate.Value = New Date(2012, 2, 22, 12, 30, 21, 470)
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgLoc)
        Me.RadGroupBox3.Controls.Add(Me.Panel1)
        Me.RadGroupBox3.HeaderText = "Location"
        Me.RadGroupBox3.Location = New System.Drawing.Point(7, 425)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(543, 145)
        Me.RadGroupBox3.TabIndex = 311
        Me.RadGroupBox3.Text = "Location"
        '
        'cbgLoc
        '
        Me.cbgLoc.CheckedValue = Nothing
        Me.cbgLoc.DataSource = Nothing
        Me.cbgLoc.DisplayMember = "Name"
        Me.cbgLoc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLoc.Location = New System.Drawing.Point(10, 40)
        Me.cbgLoc.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLoc.MyShowHeadrText = False
        Me.cbgLoc.Name = "cbgLoc"
        Me.cbgLoc.Size = New System.Drawing.Size(523, 95)
        Me.cbgLoc.TabIndex = 2
        Me.cbgLoc.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkLocSelect)
        Me.Panel1.Controls.Add(Me.chkLocAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(523, 20)
        Me.Panel1.TabIndex = 1
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
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgChapter)
        Me.RadGroupBox5.Controls.Add(Me.Panel3)
        Me.RadGroupBox5.HeaderText = "Chapter Head"
        Me.RadGroupBox5.Location = New System.Drawing.Point(8, 123)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(543, 145)
        Me.RadGroupBox5.TabIndex = 310
        Me.RadGroupBox5.Text = "Chapter Head"
        '
        'cbgChapter
        '
        Me.cbgChapter.CheckedValue = Nothing
        Me.cbgChapter.DataSource = Nothing
        Me.cbgChapter.DisplayMember = "Name"
        Me.cbgChapter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgChapter.Location = New System.Drawing.Point(10, 40)
        Me.cbgChapter.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgChapter.MyShowHeadrText = False
        Me.cbgChapter.Name = "cbgChapter"
        Me.cbgChapter.Size = New System.Drawing.Size(523, 95)
        Me.cbgChapter.TabIndex = 2
        Me.cbgChapter.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkChapterSelect)
        Me.Panel3.Controls.Add(Me.chkChapterAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(523, 20)
        Me.Panel3.TabIndex = 1
        '
        'chkChapterSelect
        '
        Me.chkChapterSelect.Location = New System.Drawing.Point(189, 1)
        Me.chkChapterSelect.MyLinkLable1 = Nothing
        Me.chkChapterSelect.MyLinkLable2 = Nothing
        Me.chkChapterSelect.Name = "chkChapterSelect"
        Me.chkChapterSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkChapterSelect.TabIndex = 2
        Me.chkChapterSelect.Text = "Select"
        '
        'chkChapterAll
        '
        Me.chkChapterAll.Location = New System.Drawing.Point(140, 1)
        Me.chkChapterAll.MyLinkLable1 = Nothing
        Me.chkChapterAll.MyLinkLable2 = Nothing
        Me.chkChapterAll.Name = "chkChapterAll"
        Me.chkChapterAll.Size = New System.Drawing.Size(33, 18)
        Me.chkChapterAll.TabIndex = 1
        Me.chkChapterAll.Text = "All"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(491, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 24)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(3, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 24)
        Me.btnPrint.TabIndex = 1
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
        Me.SplitContainer1.Size = New System.Drawing.Size(577, 633)
        Me.SplitContainer1.SplitterDistance = 599
        Me.SplitContainer1.TabIndex = 5
        '
        'frmExciseChapterWise
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(577, 633)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmExciseChapterWise"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "ER1 Report"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.RadGroupBox6.PerformLayout()
        CType(Me.rdbDSR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbMTD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbChapterWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnStockReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbER1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkItmSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItmAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.frmDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.toDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkChapterSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkChapterAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLoc As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgChapter As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkChapterSelect As common.Controls.MyRadioButton
    Friend WithEvents chkChapterAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents frmDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents toDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkItmSelect As common.Controls.MyRadioButton
    Friend WithEvents chkItmAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbChapterWise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbER1 As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents rdbtnStockReport As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbMTD As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbDSR As Telerik.WinControls.UI.RadRadioButton
End Class

