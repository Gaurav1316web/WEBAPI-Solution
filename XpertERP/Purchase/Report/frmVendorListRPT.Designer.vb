<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVendorListRPT
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgvendor = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkvendorselect = New common.Controls.MyRadioButton()
        Me.chlvendorall = New common.Controls.MyRadioButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbggroup = New common.MyCheckBoxGrid()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.chkSelect = New common.Controls.MyRadioButton()
        Me.chkAll = New common.Controls.MyRadioButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.btnexport = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnexcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnpdf = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnprint = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkvendorselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chlvendorall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.chkSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnexport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnexport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Size = New System.Drawing.Size(826, 422)
        Me.SplitContainer1.SplitterDistance = 384
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(3, 3)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(820, 378)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(62.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(799, 330)
        Me.RadPageViewPage1.Text = "Selection"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgvendor)
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.HeaderText = "Vendor"
        Me.RadGroupBox1.Location = New System.Drawing.Point(393, 24)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(384, 212)
        Me.RadGroupBox1.TabIndex = 73
        Me.RadGroupBox1.Text = "Vendor"
        '
        'cbgvendor
        '
        Me.cbgvendor.AccessibleName = ""
        Me.cbgvendor.CheckedValue = Nothing
        Me.cbgvendor.DataSource = Nothing
        Me.cbgvendor.DisplayMember = "Name"
        Me.cbgvendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgvendor.Location = New System.Drawing.Point(10, 40)
        Me.cbgvendor.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgvendor.MyShowHeadrText = False
        Me.cbgvendor.Name = "cbgvendor"
        Me.cbgvendor.Size = New System.Drawing.Size(364, 162)
        Me.cbgvendor.TabIndex = 1
        Me.cbgvendor.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkvendorselect)
        Me.Panel1.Controls.Add(Me.chlvendorall)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(364, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkvendorselect
        '
        Me.chkvendorselect.Location = New System.Drawing.Point(208, 1)
        Me.chkvendorselect.MyLinkLable1 = Nothing
        Me.chkvendorselect.MyLinkLable2 = Nothing
        Me.chkvendorselect.Name = "chkvendorselect"
        Me.chkvendorselect.Size = New System.Drawing.Size(50, 18)
        Me.chkvendorselect.TabIndex = 1
        Me.chkvendorselect.Text = "Select"
        '
        'chlvendorall
        '
        Me.chlvendorall.Location = New System.Drawing.Point(52, 1)
        Me.chlvendorall.MyLinkLable1 = Nothing
        Me.chlvendorall.MyLinkLable2 = Nothing
        Me.chlvendorall.Name = "chlvendorall"
        Me.chlvendorall.Size = New System.Drawing.Size(33, 18)
        Me.chlvendorall.TabIndex = 0
        Me.chlvendorall.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbggroup)
        Me.RadGroupBox2.Controls.Add(Me.Panel6)
        Me.RadGroupBox2.HeaderText = "Vendor Group"
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 24)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(384, 212)
        Me.RadGroupBox2.TabIndex = 72
        Me.RadGroupBox2.Text = "Vendor Group"
        '
        'cbggroup
        '
        Me.cbggroup.AccessibleName = ""
        Me.cbggroup.CheckedValue = Nothing
        Me.cbggroup.DataSource = Nothing
        Me.cbggroup.DisplayMember = "Name"
        Me.cbggroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbggroup.Location = New System.Drawing.Point(10, 40)
        Me.cbggroup.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbggroup.MyShowHeadrText = False
        Me.cbggroup.Name = "cbggroup"
        Me.cbggroup.Size = New System.Drawing.Size(364, 162)
        Me.cbggroup.TabIndex = 1
        Me.cbggroup.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.chkSelect)
        Me.Panel6.Controls.Add(Me.chkAll)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(364, 20)
        Me.Panel6.TabIndex = 0
        '
        'chkSelect
        '
        Me.chkSelect.Location = New System.Drawing.Point(208, 1)
        Me.chkSelect.MyLinkLable1 = Nothing
        Me.chkSelect.MyLinkLable2 = Nothing
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkSelect.TabIndex = 1
        Me.chkSelect.Text = "Select"
        '
        'chkAll
        '
        Me.chkAll.Location = New System.Drawing.Point(52, 1)
        Me.chkAll.MyLinkLable1 = Nothing
        Me.chkAll.MyLinkLable2 = Nothing
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(33, 18)
        Me.chkAll.TabIndex = 0
        Me.chkAll.Text = "All"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(799, 330)
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
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.AllowDragToGroup = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.EnableGrouping = False
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(799, 330)
        Me.gv.TabIndex = 0
        Me.gv.Text = "RadGridView1"
        '
        'btnexport
        '
        Me.btnexport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnexport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexcel, Me.btnpdf})
        Me.btnexport.Location = New System.Drawing.Point(152, 8)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(75, 20)
        Me.btnexport.TabIndex = 3
        Me.btnexport.Text = "Export"
        '
        'btnexcel
        '
        Me.btnexcel.AccessibleDescription = "Export To Excel"
        Me.btnexcel.AccessibleName = "Export To Excel"
        Me.btnexcel.Image = Global.ERP.My.Resources.Resources.MSE
        Me.btnexcel.Name = "btnexcel"
        Me.btnexcel.Text = "Export To Excel"
        '
        'btnpdf
        '
        Me.btnpdf.AccessibleDescription = "Export To PDF"
        Me.btnpdf.AccessibleName = "Export To PDF"
        Me.btnpdf.Image = Global.ERP.My.Resources.Resources.pdf
        Me.btnpdf.Name = "btnpdf"
        Me.btnpdf.Text = "Export To PDF"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(748, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 20)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(80, 8)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(66, 20)
        Me.btnreset.TabIndex = 1
        Me.btnreset.Text = "Reset"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(8, 8)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(66, 20)
        Me.btnprint.TabIndex = 0
        Me.btnprint.Text = ">>>"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(826, 20)
        Me.RadMenu1.TabIndex = 4
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Setting"
        Me.RadMenuItem1.AccessibleName = "Setting"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.AccessibleDescription = "Save Layout"
        Me.rmSaveLayout.AccessibleName = "Save Layout"
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.rmDeleteLayout.AccessibleName = "Delete Layout"
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'FrmVendorListRPT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(826, 442)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmVendorListRPT"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmVendorListRPT"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkvendorselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chlvendorall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.chkSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnexport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnexcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnpdf As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbggroup As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chkSelect As common.Controls.MyRadioButton
    Friend WithEvents chkAll As common.Controls.MyRadioButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgvendor As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkvendorselect As common.Controls.MyRadioButton
    Friend WithEvents chlvendorall As common.Controls.MyRadioButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
End Class

