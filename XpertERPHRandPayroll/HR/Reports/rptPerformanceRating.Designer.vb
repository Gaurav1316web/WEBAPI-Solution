Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptPerformanceRating
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgDepartment = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkDepartmentSelect = New common.Controls.MyRadioButton()
        Me.chkDepartmentAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgTelecaller = New common.MyCheckBoxGrid()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.chkTelecallerSelect = New common.Controls.MyRadioButton()
        Me.chkTeleCallerAll = New common.Controls.MyRadioButton()
        Me.txtTodate = New common.Controls.MyDateTimePicker()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.PanelmenueItems1 = New System.Windows.Forms.Panel()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.mbtnDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkDepartmentSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDepartmentAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.chkTelecallerSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTeleCallerAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelmenueItems1.SuspendLayout()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Size = New System.Drawing.Size(760, 508)
        Me.SplitContainer1.SplitterDistance = 478
        Me.SplitContainer1.TabIndex = 94
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(760, 478)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(739, 430)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.RadGroupBox1)
        Me.RadGroupBox5.Controls.Add(Me.RadGroupBox8)
        Me.RadGroupBox5.Controls.Add(Me.txtTodate)
        Me.RadGroupBox5.Controls.Add(Me.RadLabel7)
        Me.RadGroupBox5.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox5.Controls.Add(Me.RadLabel8)
        Me.RadGroupBox5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox5.HeaderText = ""
        Me.RadGroupBox5.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(739, 430)
        Me.RadGroupBox5.TabIndex = 2
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgDepartment)
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.HeaderText = "Department"
        Me.RadGroupBox1.Location = New System.Drawing.Point(372, 44)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(357, 170)
        Me.RadGroupBox1.TabIndex = 31
        Me.RadGroupBox1.Text = "Department"
        '
        'cbgDepartment
        '
        Me.cbgDepartment.CheckedValue = Nothing
        Me.cbgDepartment.DataSource = Nothing
        Me.cbgDepartment.DisplayMember = "Name"
        Me.cbgDepartment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgDepartment.Location = New System.Drawing.Point(10, 40)
        Me.cbgDepartment.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgDepartment.MyShowHeadrText = False
        Me.cbgDepartment.Name = "cbgDepartment"
        Me.cbgDepartment.Size = New System.Drawing.Size(337, 120)
        Me.cbgDepartment.TabIndex = 1
        Me.cbgDepartment.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkDepartmentSelect)
        Me.Panel1.Controls.Add(Me.chkDepartmentAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(337, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkDepartmentSelect
        '
        Me.chkDepartmentSelect.Location = New System.Drawing.Point(161, 1)
        Me.chkDepartmentSelect.MyLinkLable1 = Nothing
        Me.chkDepartmentSelect.MyLinkLable2 = Nothing
        Me.chkDepartmentSelect.Name = "chkDepartmentSelect"
        Me.chkDepartmentSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkDepartmentSelect.TabIndex = 1
        Me.chkDepartmentSelect.Text = "Select"
        '
        'chkDepartmentAll
        '
        Me.chkDepartmentAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDepartmentAll.Location = New System.Drawing.Point(120, 1)
        Me.chkDepartmentAll.MyLinkLable1 = Nothing
        Me.chkDepartmentAll.MyLinkLable2 = Nothing
        Me.chkDepartmentAll.Name = "chkDepartmentAll"
        Me.chkDepartmentAll.Size = New System.Drawing.Size(33, 18)
        Me.chkDepartmentAll.TabIndex = 0
        Me.chkDepartmentAll.Text = "All"
        Me.chkDepartmentAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Controls.Add(Me.cbgTelecaller)
        Me.RadGroupBox8.Controls.Add(Me.Panel6)
        Me.RadGroupBox8.HeaderText = "User"
        Me.RadGroupBox8.Location = New System.Drawing.Point(7, 44)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox8.Size = New System.Drawing.Size(357, 170)
        Me.RadGroupBox8.TabIndex = 30
        Me.RadGroupBox8.Text = "User"
        '
        'cbgTelecaller
        '
        Me.cbgTelecaller.CheckedValue = Nothing
        Me.cbgTelecaller.DataSource = Nothing
        Me.cbgTelecaller.DisplayMember = "Name"
        Me.cbgTelecaller.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgTelecaller.Location = New System.Drawing.Point(10, 40)
        Me.cbgTelecaller.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgTelecaller.MyShowHeadrText = False
        Me.cbgTelecaller.Name = "cbgTelecaller"
        Me.cbgTelecaller.Size = New System.Drawing.Size(337, 120)
        Me.cbgTelecaller.TabIndex = 1
        Me.cbgTelecaller.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.chkTelecallerSelect)
        Me.Panel6.Controls.Add(Me.chkTeleCallerAll)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(337, 20)
        Me.Panel6.TabIndex = 0
        '
        'chkTelecallerSelect
        '
        Me.chkTelecallerSelect.Location = New System.Drawing.Point(161, 1)
        Me.chkTelecallerSelect.MyLinkLable1 = Nothing
        Me.chkTelecallerSelect.MyLinkLable2 = Nothing
        Me.chkTelecallerSelect.Name = "chkTelecallerSelect"
        Me.chkTelecallerSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkTelecallerSelect.TabIndex = 1
        Me.chkTelecallerSelect.Text = "Select"
        '
        'chkTeleCallerAll
        '
        Me.chkTeleCallerAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTeleCallerAll.Location = New System.Drawing.Point(120, 1)
        Me.chkTeleCallerAll.MyLinkLable1 = Nothing
        Me.chkTeleCallerAll.MyLinkLable2 = Nothing
        Me.chkTeleCallerAll.Name = "chkTeleCallerAll"
        Me.chkTeleCallerAll.Size = New System.Drawing.Size(33, 18)
        Me.chkTeleCallerAll.TabIndex = 0
        Me.chkTeleCallerAll.Text = "All"
        Me.chkTeleCallerAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'txtTodate
        '
        Me.txtTodate.CustomFormat = "MMM/yyyy"
        Me.txtTodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtTodate.Location = New System.Drawing.Point(237, 13)
        Me.txtTodate.MendatroryField = False
        Me.txtTodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtTodate.MyLinkLable1 = Me.RadLabel8
        Me.txtTodate.MyLinkLable2 = Nothing
        Me.txtTodate.Name = "txtTodate"
        Me.txtTodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtTodate.Size = New System.Drawing.Size(71, 20)
        Me.txtTodate.TabIndex = 28
        Me.txtTodate.TabStop = False
        Me.txtTodate.Text = "Jun/2013"
        Me.txtTodate.Value = New Date(2013, 6, 20, 0, 0, 0, 0)
        '
        'RadLabel8
        '
        Me.RadLabel8.Location = New System.Drawing.Point(181, 15)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(55, 18)
        Me.RadLabel8.TabIndex = 27
        Me.RadLabel8.Text = "To Month"
        '
        'RadLabel7
        '
        Me.RadLabel7.Location = New System.Drawing.Point(13, 14)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(69, 18)
        Me.RadLabel7.TabIndex = 25
        Me.RadLabel7.Text = "From Month"
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "MMM/yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(84, 14)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.RadLabel7
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(72, 20)
        Me.txtFromDate.TabIndex = 26
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "Jun/2013"
        Me.txtFromDate.Value = New Date(2013, 6, 20, 0, 0, 0, 0)
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(739, 430)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(739, 430)
        Me.gv1.TabIndex = 0
        Me.gv1.Text = "RadGridView1"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExcel, Me.rmPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(147, 3)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(95, 20)
        Me.RadSplitButton1.TabIndex = 7
        Me.RadSplitButton1.Text = "Export"
        '
        'rmExcel
        '
        Me.rmExcel.AccessibleDescription = "Excel"
        Me.rmExcel.AccessibleName = "Excel"
        Me.rmExcel.Name = "rmExcel"
        Me.rmExcel.Text = "Excel"
        '
        'rmPDF
        '
        Me.rmPDF.AccessibleDescription = "PDF"
        Me.rmPDF.AccessibleName = "PDF"
        Me.rmPDF.Name = "rmPDF"
        Me.rmPDF.Text = "PDF"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(5, 3)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnRefresh.TabIndex = 0
        Me.btnRefresh.Text = "Refresh"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(684, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(77, 3)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 18)
        Me.btnreset.TabIndex = 1
        Me.btnreset.Text = "Reset"
        '
        'PanelmenueItems1
        '
        Me.PanelmenueItems1.Controls.Add(Me.RadMenu2)
        Me.PanelmenueItems1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelmenueItems1.Location = New System.Drawing.Point(0, 0)
        Me.PanelmenueItems1.Name = "PanelmenueItems1"
        Me.PanelmenueItems1.Size = New System.Drawing.Size(760, 20)
        Me.PanelmenueItems1.TabIndex = 98
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mbtnDeleteLayout})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(760, 20)
        Me.RadMenu2.TabIndex = 5
        Me.RadMenu2.Text = "RadMenu2"
        '
        'mbtnDeleteLayout
        '
        Me.mbtnDeleteLayout.AccessibleDescription = "Setting"
        Me.mbtnDeleteLayout.AccessibleName = "Setting"
        Me.mbtnDeleteLayout.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnSaveLayout, Me.btnDeleteLayout})
        Me.mbtnDeleteLayout.Name = "mbtnDeleteLayout"
        Me.mbtnDeleteLayout.Text = "Setting"
        '
        'btnSaveLayout
        '
        Me.btnSaveLayout.AccessibleDescription = "Save Layout"
        Me.btnSaveLayout.AccessibleName = "Save Layout"
        Me.btnSaveLayout.Name = "btnSaveLayout"
        Me.btnSaveLayout.Text = "Save Layout"
        '
        'btnDeleteLayout
        '
        Me.btnDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.btnDeleteLayout.AccessibleName = "Delete Layout"
        Me.btnDeleteLayout.Name = "btnDeleteLayout"
        Me.btnDeleteLayout.Text = "Delete Layout"
        '
        'rptPerformanceRating
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(760, 528)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.PanelmenueItems1)
        Me.Name = "rptPerformanceRating"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Performance Rating"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkDepartmentSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDepartmentAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.chkTelecallerSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTeleCallerAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelmenueItems1.ResumeLayout(False)
        Me.PanelmenueItems1.PerformLayout()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox8 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgTelecaller As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chkTelecallerSelect As common.Controls.MyRadioButton
    Friend WithEvents chkTeleCallerAll As common.Controls.MyRadioButton
    Friend WithEvents txtTodate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents PanelmenueItems1 As System.Windows.Forms.Panel
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents mbtnDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgDepartment As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkDepartmentSelect As common.Controls.MyRadioButton
    Friend WithEvents chkDepartmentAll As common.Controls.MyRadioButton
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmPDF As Telerik.WinControls.UI.RadMenuItem
End Class

