<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOperatorEfficiencyReport
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.dtpToDate = New common.Controls.MyDateTimePicker
        Me.gbCustomer = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkCustSelect = New common.Controls.MyRadioButton
        Me.chkCustAll = New common.Controls.MyRadioButton
        Me.dtpFromDate = New common.Controls.MyDateTimePicker
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv = New common.UserControls.MyRadGridView
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbCustomer.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkCustSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(865, 435)
        Me.RadPageView1.TabIndex = 3
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(844, 387)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox1.Controls.Add(Me.dtpToDate)
        Me.RadGroupBox1.Controls.Add(Me.gbCustomer)
        Me.RadGroupBox1.Controls.Add(Me.dtpFromDate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(844, 387)
        Me.RadGroupBox1.TabIndex = 3
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(221, 9)
        Me.dtpToDate.MendatroryField = False
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Nothing
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(82, 20)
        Me.dtpToDate.TabIndex = 6
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "13-06-2011"
        Me.dtpToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'gbCustomer
        '
        Me.gbCustomer.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbCustomer.Controls.Add(Me.cbgCustomer)
        Me.gbCustomer.Controls.Add(Me.Panel3)
        Me.gbCustomer.HeaderText = "Operator"
        Me.gbCustomer.Location = New System.Drawing.Point(13, 47)
        Me.gbCustomer.Name = "gbCustomer"
        Me.gbCustomer.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbCustomer.Size = New System.Drawing.Size(430, 275)
        Me.gbCustomer.TabIndex = 15
        Me.gbCustomer.Text = "Operator"
        '
        'cbgCustomer
        '
        Me.cbgCustomer.CheckedValue = Nothing
        Me.cbgCustomer.DataSource = Nothing
        Me.cbgCustomer.DisplayMember = "Name"
        Me.cbgCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCustomer.Location = New System.Drawing.Point(10, 40)
        Me.cbgCustomer.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustomer.MyShowHeadrText = False
        Me.cbgCustomer.Name = "cbgCustomer"
        Me.cbgCustomer.Size = New System.Drawing.Size(410, 225)
        Me.cbgCustomer.TabIndex = 2
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkCustSelect)
        Me.Panel3.Controls.Add(Me.chkCustAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(410, 20)
        Me.Panel3.TabIndex = 1
        '
        'chkCustSelect
        '
        Me.chkCustSelect.Location = New System.Drawing.Point(192, 1)
        Me.chkCustSelect.MyLinkLable1 = Nothing
        Me.chkCustSelect.MyLinkLable2 = Nothing
        Me.chkCustSelect.Name = "chkCustSelect"
        Me.chkCustSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCustSelect.TabIndex = 2
        Me.chkCustSelect.Text = "Select"
        '
        'chkCustAll
        '
        Me.chkCustAll.Location = New System.Drawing.Point(141, 1)
        Me.chkCustAll.MyLinkLable1 = Nothing
        Me.chkCustAll.MyLinkLable2 = Nothing
        Me.chkCustAll.Name = "chkCustAll"
        Me.chkCustAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCustAll.TabIndex = 1
        Me.chkCustAll.Text = "All"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(78, 9)
        Me.dtpFromDate.MendatroryField = False
        Me.dtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.MyLinkLable1 = Nothing
        Me.dtpFromDate.MyLinkLable2 = Nothing
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.Size = New System.Drawing.Size(85, 20)
        Me.dtpFromDate.TabIndex = 5
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "13-06-2011"
        Me.dtpFromDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(172, 10)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 8
        Me.RadLabel2.Text = "To Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(13, 10)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 9
        Me.RadLabel1.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(883, 431)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowEditRow = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.Name = "gv"
        Me.gv.ShowGroupPanel = False
        Me.gv.Size = New System.Drawing.Size(883, 431)
        Me.gv.TabIndex = 1
        Me.gv.Text = "RadGridView1"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "PDF"
        Me.RadMenuItem2.AccessibleName = "PDF"
        Me.RadMenuItem2.Image = Global.XpertERPProcessProduction.My.Resources.Resources.pdf
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "PDF"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(85, 3)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 7
        Me.btnReset.Text = "Reset"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(10, 3)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnRefresh.TabIndex = 112
        Me.btnRefresh.Text = ">>>"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(865, 464)
        Me.SplitContainer1.SplitterDistance = 435
        Me.SplitContainer1.TabIndex = 7
        '
        'btnExport
        '
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.btnExport.Location = New System.Drawing.Point(159, 3)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(83, 19)
        Me.btnExport.TabIndex = 113
        Me.btnExport.Text = "Export"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Excel"
        Me.RadMenuItem1.AccessibleName = "Excel"
        Me.RadMenuItem1.Image = Global.XpertERPProcessProduction.My.Resources.Resources.MSE
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Excel"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(777, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
        '
        'FrmOperatorEfficiencyReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(865, 464)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmOperatorEfficiencyReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Operator Efficiency Report"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbCustomer.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkCustSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents gbCustomer As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkCustSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCustAll As common.Controls.MyRadioButton
    Friend WithEvents dtpFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
End Class

