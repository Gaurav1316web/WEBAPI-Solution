Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFranchiseChargesReport
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
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.rbtnvselect = New common.Controls.MyRadioButton
        Me.rbtnvAll = New common.Controls.MyRadioButton
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgItem = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rbtniselect = New common.Controls.MyRadioButton
        Me.rbtniall = New common.Controls.MyRadioButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnexport = New Telerik.WinControls.UI.RadSplitButton
        Me.btnexcel = New Telerik.WinControls.UI.RadMenuItem
        Me.btnpdf = New Telerik.WinControls.UI.RadMenuItem
        Me.btnrefresh = New System.Windows.Forms.Button
        Me.btnclose = New System.Windows.Forms.Button
        Me.btnreset = New System.Windows.Forms.Button
        Me.btnprint = New System.Windows.Forms.Button
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.GV = New common.UserControls.MyRadGridView
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.rbtnvselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnvAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.rbtniselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtniall, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.btnexport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.GV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GV.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Controls.Add(Me.cbgCustomer)
        Me.RadGroupBox8.Controls.Add(Me.Panel7)
        Me.RadGroupBox8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox8.HeaderText = "Franchises"
        Me.RadGroupBox8.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox8.Size = New System.Drawing.Size(798, 170)
        Me.RadGroupBox8.TabIndex = 313
        Me.RadGroupBox8.Text = "Franchises"
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
        Me.cbgCustomer.Size = New System.Drawing.Size(778, 120)
        Me.cbgCustomer.TabIndex = 1
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.rbtnvselect)
        Me.Panel7.Controls.Add(Me.rbtnvAll)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(10, 20)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(778, 20)
        Me.Panel7.TabIndex = 0
        '
        'rbtnvselect
        '
        Me.rbtnvselect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnvselect.Location = New System.Drawing.Point(349, 1)
        Me.rbtnvselect.MyLinkLable1 = Nothing
        Me.rbtnvselect.MyLinkLable2 = Nothing
        Me.rbtnvselect.Name = "rbtnvselect"
        Me.rbtnvselect.Size = New System.Drawing.Size(62, 18)
        Me.rbtnvselect.TabIndex = 1
        Me.rbtnvselect.Text = "Selected"
        '
        'rbtnvAll
        '
        Me.rbtnvAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnvAll.Location = New System.Drawing.Point(301, 1)
        Me.rbtnvAll.MyLinkLable1 = Nothing
        Me.rbtnvAll.MyLinkLable2 = Nothing
        Me.rbtnvAll.Name = "rbtnvAll"
        Me.rbtnvAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnvAll.TabIndex = 1
        Me.rbtnvAll.Text = "All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Select Date"
        Me.RadGroupBox3.Location = New System.Drawing.Point(3, -2)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(284, 42)
        Me.RadGroupBox3.TabIndex = 314
        Me.RadGroupBox3.Text = "Select Date"
        Me.RadGroupBox3.Visible = False
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(141, 14)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(13, 14)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'ToDate
        '
        Me.ToDate.CustomFormat = "dd/MM/yyyy"
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(166, 14)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(84, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/10/2011"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'fromDate
        '
        Me.fromDate.CustomFormat = "dd/MM/yyyy"
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(51, 12)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(84, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "24/10/2011"
        Me.fromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgItem)
        Me.RadGroupBox2.Controls.Add(Me.Panel1)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.HeaderText = "Item"
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(798, 205)
        Me.RadGroupBox2.TabIndex = 319
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
        Me.cbgItem.Size = New System.Drawing.Size(778, 155)
        Me.cbgItem.TabIndex = 2
        Me.cbgItem.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbtniselect)
        Me.Panel1.Controls.Add(Me.rbtniall)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(778, 20)
        Me.Panel1.TabIndex = 1
        '
        'rbtniselect
        '
        Me.rbtniselect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtniselect.Location = New System.Drawing.Point(373, 1)
        Me.rbtniselect.MyLinkLable1 = Nothing
        Me.rbtniselect.MyLinkLable2 = Nothing
        Me.rbtniselect.Name = "rbtniselect"
        Me.rbtniselect.Size = New System.Drawing.Size(62, 18)
        Me.rbtniselect.TabIndex = 3
        Me.rbtniselect.Text = "Selected"
        '
        'rbtniall
        '
        Me.rbtniall.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtniall.Location = New System.Drawing.Point(321, 1)
        Me.rbtniall.MyLinkLable1 = Nothing
        Me.rbtniall.MyLinkLable2 = Nothing
        Me.rbtniall.Name = "rbtniall"
        Me.rbtniall.Size = New System.Drawing.Size(33, 18)
        Me.rbtniall.TabIndex = 2
        Me.rbtniall.Text = "All"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 48)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox8)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer1.Size = New System.Drawing.Size(804, 391)
        Me.SplitContainer1.SplitterDistance = 176
        Me.SplitContainer1.TabIndex = 320
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.btnexport)
        Me.Panel2.Controls.Add(Me.btnrefresh)
        Me.Panel2.Controls.Add(Me.btnclose)
        Me.Panel2.Controls.Add(Me.btnreset)
        Me.Panel2.Controls.Add(Me.btnprint)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 490)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(832, 32)
        Me.Panel2.TabIndex = 321
        '
        'btnexport
        '
        Me.btnexport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnexport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexcel, Me.btnpdf})
        Me.btnexport.Location = New System.Drawing.Point(247, 4)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(75, 23)
        Me.btnexport.TabIndex = 4
        Me.btnexport.Text = "Export"
        '
        'btnexcel
        '
        Me.btnexcel.AccessibleDescription = "Export To Excel"
        Me.btnexcel.AccessibleName = "Export To Excel"
        Me.btnexcel.Name = "btnexcel"
        Me.btnexcel.Text = "Export To Excel"
        Me.btnexcel.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnpdf
        '
        Me.btnpdf.AccessibleDescription = "Export To PDF"
        Me.btnpdf.AccessibleName = "Export To PDF"
        Me.btnpdf.Name = "btnpdf"
        Me.btnpdf.Text = "Export To PDF"
        Me.btnpdf.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnrefresh
        '
        Me.btnrefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnrefresh.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrefresh.Location = New System.Drawing.Point(3, 4)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnrefresh.TabIndex = 3
        Me.btnrefresh.Text = ">>>"
        Me.btnrefresh.UseVisualStyleBackColor = True
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(743, 4)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(75, 23)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        Me.btnclose.UseVisualStyleBackColor = True
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnreset.Location = New System.Drawing.Point(84, 4)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(75, 23)
        Me.btnreset.TabIndex = 1
        Me.btnreset.Text = "Reset"
        Me.btnreset.UseVisualStyleBackColor = True
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(166, 4)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(75, 23)
        Me.btnprint.TabIndex = 0
        Me.btnprint.Text = "Print"
        Me.btnprint.UseVisualStyleBackColor = True
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(832, 490)
        Me.RadPageView1.TabIndex = 322
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer1)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(811, 442)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.GV)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(811, 442)
        Me.RadPageViewPage2.Text = "Report"
        '
        'GV
        '
        Me.GV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GV.Location = New System.Drawing.Point(0, 0)
        Me.GV.Name = "GV"
        Me.GV.Size = New System.Drawing.Size(811, 442)
        Me.GV.TabIndex = 0
        Me.GV.Text = "RadGridView1"
        '
        'FrmFranchiseChargesReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(832, 522)
        Me.Controls.Add(Me.RadPageView1)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "FrmFranchiseChargesReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Franchise Charges Report"
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.rbtnvselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnvAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.rbtniselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtniall, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.btnexport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.GV.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox8 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnprint As System.Windows.Forms.Button
    Protected WithEvents rbtnvselect As common.Controls.MyRadioButton
    Protected WithEvents rbtnvAll As common.Controls.MyRadioButton
    Protected WithEvents rbtniselect As common.Controls.MyRadioButton
    Protected WithEvents rbtniall As common.Controls.MyRadioButton
    Friend WithEvents btnreset As System.Windows.Forms.Button
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents btnrefresh As System.Windows.Forms.Button
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GV As common.UserControls.MyRadGridView
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnexcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnpdf As Telerik.WinControls.UI.RadMenuItem
End Class

