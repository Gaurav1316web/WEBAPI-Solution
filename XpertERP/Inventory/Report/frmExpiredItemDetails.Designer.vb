<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmExpiredItemDetails
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
        Me.grpSelect = New System.Windows.Forms.GroupBox
        Me.rdbDetail = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbSummary = New Telerik.WinControls.UI.RadRadioButton
        Me.chkLocationSelect = New common.Controls.MyRadioButton
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.chkLocationAll = New common.Controls.MyRadioButton
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Locationgb = New Telerik.WinControls.UI.RadGroupBox
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton
        Me.Export = New Telerik.WinControls.UI.RadMenuItem
        Me.PDF = New Telerik.WinControls.UI.RadMenuItem
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel
        Me.txtToDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel
        Me.txtFromDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.cg = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCust = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkCustSelect = New common.Controls.MyRadioButton
        Me.chkCustAll = New common.Controls.MyRadioButton
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.grpSelect.SuspendLayout()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.Locationgb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Locationgb.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cg.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkCustSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpSelect
        '
        Me.grpSelect.Controls.Add(Me.rdbDetail)
        Me.grpSelect.Controls.Add(Me.rdbSummary)
        Me.grpSelect.Location = New System.Drawing.Point(345, 3)
        Me.grpSelect.Name = "grpSelect"
        Me.grpSelect.Size = New System.Drawing.Size(189, 42)
        Me.grpSelect.TabIndex = 108
        Me.grpSelect.TabStop = False
        Me.grpSelect.Text = "Select"
        Me.grpSelect.Visible = False
        '
        'rdbDetail
        '
        Me.rdbDetail.Location = New System.Drawing.Point(118, 18)
        Me.rdbDetail.Name = "rdbDetail"
        Me.rdbDetail.Size = New System.Drawing.Size(49, 18)
        Me.rdbDetail.TabIndex = 0
        Me.rdbDetail.Text = "Detail"
        '
        'rdbSummary
        '
        Me.rdbSummary.Location = New System.Drawing.Point(6, 18)
        Me.rdbSummary.Name = "rdbSummary"
        Me.rdbSummary.Size = New System.Drawing.Size(67, 18)
        Me.rdbSummary.TabIndex = 103
        Me.rdbSummary.Text = "Summary"
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.Location = New System.Drawing.Point(202, 1)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
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
        Me.cbgLocation.Size = New System.Drawing.Size(424, 136)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.Location = New System.Drawing.Point(151, 1)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkLocationSelect)
        Me.Panel2.Controls.Add(Me.chkLocationAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(424, 20)
        Me.Panel2.TabIndex = 0
        '
        'Locationgb
        '
        Me.Locationgb.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Locationgb.Controls.Add(Me.cbgLocation)
        Me.Locationgb.Controls.Add(Me.Panel2)
        Me.Locationgb.HeaderText = "Location"
        Me.Locationgb.Location = New System.Drawing.Point(8, 51)
        Me.Locationgb.Name = "Locationgb"
        Me.Locationgb.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Locationgb.Size = New System.Drawing.Size(444, 186)
        Me.Locationgb.TabIndex = 47
        Me.Locationgb.Text = "Location"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.btnExport)
        Me.Panel6.Controls.Add(Me.btnClose)
        Me.Panel6.Controls.Add(Me.btnReset)
        Me.Panel6.Controls.Add(Me.btnRefresh)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel6.Location = New System.Drawing.Point(0, 490)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(567, 31)
        Me.Panel6.TabIndex = 27
        '
        'btnExport
        '
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Export, Me.PDF})
        Me.btnExport.Location = New System.Drawing.Point(152, 3)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(80, 18)
        Me.btnExport.TabIndex = 130
        Me.btnExport.Text = "Export"
        '
        'Export
        '
        Me.Export.AccessibleDescription = "Export"
        Me.Export.AccessibleName = "Export"
        Me.Export.Image = Global.ERP.My.Resources.Resources.MSE
        Me.Export.Name = "Export"
        Me.Export.Text = "Export"
        Me.Export.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'PDF
        '
        Me.PDF.AccessibleDescription = "PDF"
        Me.PDF.AccessibleName = "PDF"
        Me.PDF.Image = Global.ERP.My.Resources.Resources.pdf
        Me.PDF.Name = "PDF"
        Me.PDF.Text = "PDF"
        Me.PDF.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnClose.Location = New System.Drawing.Point(490, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(74, 18)
        Me.btnClose.TabIndex = 128
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(76, 3)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 127
        Me.btnReset.Text = "Reset"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(2, 3)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnRefresh.TabIndex = 129
        Me.btnRefresh.Text = ">>>"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(8, 8)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd/MM/yyyy "
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(209, 8)
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(130, 20)
        Me.txtToDate.TabIndex = 73
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "24/10/2011 "
        Me.txtToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Locationgb)
        Me.SplitContainer1.Panel1.Controls.Add(Me.grpSelect)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtToDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFromDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.cg)
        Me.SplitContainer1.Size = New System.Drawing.Size(546, 442)
        Me.SplitContainer1.SplitterDistance = 245
        Me.SplitContainer1.TabIndex = 22
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(183, 8)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy "
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(43, 8)
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(130, 20)
        Me.txtFromDate.TabIndex = 72
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "24/10/2011 "
        Me.txtFromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'cg
        '
        Me.cg.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.cg.Controls.Add(Me.cbgCust)
        Me.cg.Controls.Add(Me.Panel4)
        Me.cg.HeaderText = "Customer"
        Me.cg.Location = New System.Drawing.Point(8, 9)
        Me.cg.Name = "cg"
        Me.cg.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.cg.Size = New System.Drawing.Size(444, 181)
        Me.cg.TabIndex = 40
        Me.cg.Text = "Customer"
        '
        'cbgCust
        '
        Me.cbgCust.CheckedValue = Nothing
        Me.cbgCust.DataSource = Nothing
        Me.cbgCust.DisplayMember = "Name"
        Me.cbgCust.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCust.Location = New System.Drawing.Point(10, 40)
        Me.cbgCust.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCust.MyShowHeadrText = False
        Me.cbgCust.Name = "cbgCust"
        Me.cbgCust.Size = New System.Drawing.Size(424, 131)
        Me.cbgCust.TabIndex = 2
        Me.cbgCust.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkCustSelect)
        Me.Panel4.Controls.Add(Me.chkCustAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(424, 20)
        Me.Panel4.TabIndex = 1
        '
        'chkCustSelect
        '
        Me.chkCustSelect.Location = New System.Drawing.Point(202, 1)
        Me.chkCustSelect.MyLinkLable1 = Nothing
        Me.chkCustSelect.MyLinkLable2 = Nothing
        Me.chkCustSelect.Name = "chkCustSelect"
        Me.chkCustSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCustSelect.TabIndex = 2
        Me.chkCustSelect.Text = "Select"
        '
        'chkCustAll
        '
        Me.chkCustAll.Location = New System.Drawing.Point(151, 1)
        Me.chkCustAll.MyLinkLable1 = Nothing
        Me.chkCustAll.MyLinkLable2 = Nothing
        Me.chkCustAll.Name = "chkCustAll"
        Me.chkCustAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCustAll.TabIndex = 1
        Me.chkCustAll.Text = "All"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(546, 442)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(880, 484)
        Me.gv1.TabIndex = 3
        Me.gv1.Text = "RadGridView1"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(567, 490)
        Me.RadPageView1.TabIndex = 26
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(880, 484)
        Me.RadPageViewPage2.Text = "Report"
        '
        'FrmExpiredItemDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(567, 521)
        Me.Controls.Add(Me.RadPageView1)
        Me.Controls.Add(Me.Panel6)
        Me.Name = "FrmExpiredItemDetails"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Expired Item Details"
        Me.grpSelect.ResumeLayout(False)
        Me.grpSelect.PerformLayout()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.Locationgb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Locationgb.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cg.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkCustSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpSelect As System.Windows.Forms.GroupBox
    Friend WithEvents rdbDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Locationgb As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtFromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents cg As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCust As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkCustSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCustAll As common.Controls.MyRadioButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PDF As Telerik.WinControls.UI.RadMenuItem
End Class

