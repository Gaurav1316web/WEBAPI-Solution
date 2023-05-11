Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAssetDetailReport
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgasset = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.rdallasset = New Telerik.WinControls.UI.RadRadioButton
        Me.rdselectasset = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.rdselectoutlet = New Telerik.WinControls.UI.RadRadioButton
        Me.rdalloutlet = New Telerik.WinControls.UI.RadRadioButton
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.GV = New common.UserControls.MyRadGridView
        Me.btnexport = New Telerik.WinControls.UI.RadSplitButton
        Me.btnexcel = New Telerik.WinControls.UI.RadMenuItem
        Me.btnpdf = New Telerik.WinControls.UI.RadMenuItem
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.btnrefresh = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.rdallasset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdselectasset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.rdselectoutlet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdalloutlet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.GV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GV.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnexport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnrefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnexport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnrefresh)
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer1.Size = New System.Drawing.Size(1023, 608)
        Me.SplitContainer1.SplitterDistance = 568
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
        Me.RadPageView1.Size = New System.Drawing.Size(1017, 562)
        Me.RadPageView1.TabIndex = 320
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox8)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(996, 514)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgasset)
        Me.RadGroupBox5.Controls.Add(Me.Panel3)
        Me.RadGroupBox5.HeaderText = "Asset"
        Me.RadGroupBox5.Location = New System.Drawing.Point(3, 243)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(990, 269)
        Me.RadGroupBox5.TabIndex = 319
        Me.RadGroupBox5.Text = "Asset"
        '
        'cbgasset
        '
        Me.cbgasset.CheckedValue = Nothing
        Me.cbgasset.DataSource = Nothing
        Me.cbgasset.DisplayMember = "Name"
        Me.cbgasset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgasset.Location = New System.Drawing.Point(10, 40)
        Me.cbgasset.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgasset.MyShowHeadrText = False
        Me.cbgasset.Name = "cbgasset"
        Me.cbgasset.Size = New System.Drawing.Size(970, 219)
        Me.cbgasset.TabIndex = 2
        Me.cbgasset.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.rdallasset)
        Me.Panel3.Controls.Add(Me.rdselectasset)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(970, 20)
        Me.Panel3.TabIndex = 1
        '
        'rdallasset
        '
        Me.rdallasset.Location = New System.Drawing.Point(61, 1)
        Me.rdallasset.Name = "rdallasset"
        Me.rdallasset.Size = New System.Drawing.Size(33, 18)
        Me.rdallasset.TabIndex = 110
        Me.rdallasset.Text = "All"
        '
        'rdselectasset
        '
        Me.rdselectasset.Location = New System.Drawing.Point(171, 1)
        Me.rdselectasset.Name = "rdselectasset"
        Me.rdselectasset.Size = New System.Drawing.Size(50, 18)
        Me.rdselectasset.TabIndex = 111
        Me.rdselectasset.Text = "Select"
        '
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Controls.Add(Me.cbgCustomer)
        Me.RadGroupBox8.Controls.Add(Me.Panel7)
        Me.RadGroupBox8.HeaderText = "Outlet"
        Me.RadGroupBox8.Location = New System.Drawing.Point(5, 3)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox8.Size = New System.Drawing.Size(988, 234)
        Me.RadGroupBox8.TabIndex = 318
        Me.RadGroupBox8.Text = "Outlet"
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
        Me.cbgCustomer.Size = New System.Drawing.Size(968, 184)
        Me.cbgCustomer.TabIndex = 1
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.rdselectoutlet)
        Me.Panel7.Controls.Add(Me.rdalloutlet)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(10, 20)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(968, 20)
        Me.Panel7.TabIndex = 0
        '
        'rdselectoutlet
        '
        Me.rdselectoutlet.Location = New System.Drawing.Point(179, 1)
        Me.rdselectoutlet.Name = "rdselectoutlet"
        Me.rdselectoutlet.Size = New System.Drawing.Size(50, 18)
        Me.rdselectoutlet.TabIndex = 110
        Me.rdselectoutlet.Text = "Select"
        '
        'rdalloutlet
        '
        Me.rdalloutlet.Location = New System.Drawing.Point(61, 1)
        Me.rdalloutlet.Name = "rdalloutlet"
        Me.rdalloutlet.Size = New System.Drawing.Size(33, 18)
        Me.rdalloutlet.TabIndex = 109
        Me.rdalloutlet.Text = "All"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Panel1)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(703, 275)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GV)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(703, 275)
        Me.Panel1.TabIndex = 0
        '
        'GV
        '
        Me.GV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GV.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.GV.MasterTemplate.AllowAddNewRow = False
        Me.GV.MasterTemplate.AllowColumnChooser = False
        Me.GV.MasterTemplate.AllowDeleteRow = False
        Me.GV.MasterTemplate.AllowEditRow = False
        Me.GV.MasterTemplate.AllowRowReorder = True
        Me.GV.Name = "GV"
        Me.GV.ReadOnly = True
        Me.GV.Size = New System.Drawing.Size(703, 275)
        Me.GV.TabIndex = 0
        Me.GV.Text = "RadGridView1"
        '
        'btnexport
        '
        Me.btnexport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnexport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexcel, Me.btnpdf})
        Me.btnexport.Location = New System.Drawing.Point(180, 9)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(82, 20)
        Me.btnexport.TabIndex = 2
        Me.btnexport.Text = "Export"
        '
        'btnexcel
        '
        Me.btnexcel.AccessibleDescription = "Export To Excel"
        Me.btnexcel.AccessibleName = "Export To Excel"
        Me.btnexcel.Image = My.Resources.MSE
        Me.btnexcel.Name = "btnexcel"
        Me.btnexcel.Text = "Export To Excel"
        Me.btnexcel.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnpdf
        '
        Me.btnpdf.AccessibleDescription = "Export To PDF"
        Me.btnpdf.AccessibleName = "Export To PDF"
        Me.btnpdf.Image = My.Resources.pdf
        Me.btnpdf.Name = "btnpdf"
        Me.btnpdf.Text = "Export To PDF"
        Me.btnpdf.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(929, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(82, 20)
        Me.btnclose.TabIndex = 1
        Me.btnclose.Text = "Close"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnreset.Location = New System.Drawing.Point(94, 9)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(82, 20)
        Me.btnreset.TabIndex = 1
        Me.btnreset.Text = "Reset"
        '
        'btnrefresh
        '
        Me.btnrefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnrefresh.Location = New System.Drawing.Point(7, 9)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(82, 20)
        Me.btnrefresh.TabIndex = 0
        Me.btnrefresh.Text = ">>>"
        '
        'FrmAssetDetailReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1023, 608)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmAssetDetailReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmAssetDetailReport"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.rdallasset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdselectasset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.rdselectoutlet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdalloutlet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.GV.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnexport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnrefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnrefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgasset As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents rdallasset As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdselectasset As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox8 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents rdselectoutlet As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdalloutlet As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GV As common.UserControls.MyRadGridView
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnexcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnpdf As Telerik.WinControls.UI.RadMenuItem
End Class

