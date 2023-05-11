Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RouteSaleReport
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
        Me.Locationgb = New Telerik.WinControls.UI.RadGroupBox
        Me.gvlocation = New common.MyCheckBoxGrid
        Me.radiopanel = New Telerik.WinControls.UI.RadPanel
        Me.rbtnall = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnselect = New Telerik.WinControls.UI.RadRadioButton
        Me.lblstart = New common.Controls.MyLabel
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.Sales = New Telerik.WinControls.UI.RadGroupBox
        Me.gvsales = New common.MyCheckBoxGrid
        Me.RadPanel2 = New Telerik.WinControls.UI.RadPanel
        Me.rbtnallsales = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnselectsales = New Telerik.WinControls.UI.RadRadioButton
        Me.Route = New Telerik.WinControls.UI.RadGroupBox
        Me.gvroute = New common.MyCheckBoxGrid
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel
        Me.rbtnallroute = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnselectroute = New Telerik.WinControls.UI.RadRadioButton
        Me.lblenddate = New common.Controls.MyLabel
        Me.txtend = New common.Controls.MyDateTimePicker
        Me.txtStart = New common.Controls.MyDateTimePicker
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton
        Me.rdbtnprint = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.Locationgb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Locationgb.SuspendLayout()
        CType(Me.radiopanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.radiopanel.SuspendLayout()
        CType(Me.rbtnall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblstart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.Sales, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Sales.SuspendLayout()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel2.SuspendLayout()
        CType(Me.rbtnallsales, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnselectsales, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Route, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Route.SuspendLayout()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.rbtnallroute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnselectroute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblenddate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Locationgb
        '
        Me.Locationgb.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Locationgb.Controls.Add(Me.gvlocation)
        Me.Locationgb.Controls.Add(Me.radiopanel)
        Me.Locationgb.HeaderText = "Location"
        Me.Locationgb.Location = New System.Drawing.Point(13, 37)
        Me.Locationgb.Name = "Locationgb"
        Me.Locationgb.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Locationgb.Size = New System.Drawing.Size(367, 146)
        Me.Locationgb.TabIndex = 36
        Me.Locationgb.Text = "Location"
        '
        'gvlocation
        '
        Me.gvlocation.CheckedValue = Nothing
        Me.gvlocation.DataSource = Nothing
        Me.gvlocation.DisplayMember = "Name"
        Me.gvlocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvlocation.Enabled = False
        Me.gvlocation.Location = New System.Drawing.Point(10, 43)
        Me.gvlocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.gvlocation.MyShowHeadrText = False
        Me.gvlocation.Name = "gvlocation"
        Me.gvlocation.Size = New System.Drawing.Size(347, 93)
        Me.gvlocation.TabIndex = 0
        Me.gvlocation.ValueMember = "Code"
        '
        'radiopanel
        '
        Me.radiopanel.Controls.Add(Me.rbtnall)
        Me.radiopanel.Controls.Add(Me.rbtnselect)
        Me.radiopanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.radiopanel.Location = New System.Drawing.Point(10, 20)
        Me.radiopanel.Name = "radiopanel"
        Me.radiopanel.Size = New System.Drawing.Size(347, 23)
        Me.radiopanel.TabIndex = 2
        '
        'rbtnall
        '
        Me.rbtnall.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnall.Location = New System.Drawing.Point(96, 2)
        Me.rbtnall.Name = "rbtnall"
        Me.rbtnall.Size = New System.Drawing.Size(49, 18)
        Me.rbtnall.TabIndex = 0
        Me.rbtnall.TabStop = True
        Me.rbtnall.Text = "All"
        Me.rbtnall.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnselect
        '
        Me.rbtnselect.Location = New System.Drawing.Point(151, 3)
        Me.rbtnselect.Name = "rbtnselect"
        Me.rbtnselect.Size = New System.Drawing.Size(60, 18)
        Me.rbtnselect.TabIndex = 1
        Me.rbtnselect.Text = "Select "
        '
        'lblstart
        '
        Me.lblstart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblstart.Location = New System.Drawing.Point(13, 7)
        Me.lblstart.Name = "lblstart"
        Me.lblstart.Size = New System.Drawing.Size(62, 18)
        Me.lblstart.TabIndex = 32
        Me.lblstart.Text = "From Date"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.Sales)
        Me.RadGroupBox1.Controls.Add(Me.Route)
        Me.RadGroupBox1.Controls.Add(Me.lblenddate)
        Me.RadGroupBox1.Controls.Add(Me.txtend)
        Me.RadGroupBox1.Controls.Add(Me.lblstart)
        Me.RadGroupBox1.Controls.Add(Me.Locationgb)
        Me.RadGroupBox1.Controls.Add(Me.txtStart)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(15, 12)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(393, 501)
        Me.RadGroupBox1.TabIndex = 37
        '
        'Sales
        '
        Me.Sales.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Sales.Controls.Add(Me.gvsales)
        Me.Sales.Controls.Add(Me.RadPanel2)
        Me.Sales.HeaderText = "Salesman"
        Me.Sales.Location = New System.Drawing.Point(13, 341)
        Me.Sales.Name = "Sales"
        Me.Sales.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Sales.Size = New System.Drawing.Size(367, 146)
        Me.Sales.TabIndex = 38
        Me.Sales.Text = "Salesman"
        '
        'gvsales
        '
        Me.gvsales.CheckedValue = Nothing
        Me.gvsales.DataSource = Nothing
        Me.gvsales.DisplayMember = "Name"
        Me.gvsales.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvsales.Enabled = False
        Me.gvsales.Location = New System.Drawing.Point(10, 43)
        Me.gvsales.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.gvsales.MyShowHeadrText = False
        Me.gvsales.Name = "gvsales"
        Me.gvsales.Size = New System.Drawing.Size(347, 93)
        Me.gvsales.TabIndex = 0
        Me.gvsales.ValueMember = "Code"
        '
        'RadPanel2
        '
        Me.RadPanel2.Controls.Add(Me.rbtnallsales)
        Me.RadPanel2.Controls.Add(Me.rbtnselectsales)
        Me.RadPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel2.Location = New System.Drawing.Point(10, 20)
        Me.RadPanel2.Name = "RadPanel2"
        Me.RadPanel2.Size = New System.Drawing.Size(347, 23)
        Me.RadPanel2.TabIndex = 2
        '
        'rbtnallsales
        '
        Me.rbtnallsales.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnallsales.Location = New System.Drawing.Point(96, 2)
        Me.rbtnallsales.Name = "rbtnallsales"
        Me.rbtnallsales.Size = New System.Drawing.Size(49, 18)
        Me.rbtnallsales.TabIndex = 0
        Me.rbtnallsales.TabStop = True
        Me.rbtnallsales.Text = "All"
        Me.rbtnallsales.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnselectsales
        '
        Me.rbtnselectsales.Location = New System.Drawing.Point(151, 3)
        Me.rbtnselectsales.Name = "rbtnselectsales"
        Me.rbtnselectsales.Size = New System.Drawing.Size(60, 18)
        Me.rbtnselectsales.TabIndex = 1
        Me.rbtnselectsales.Text = "Select "
        '
        'Route
        '
        Me.Route.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Route.Controls.Add(Me.gvroute)
        Me.Route.Controls.Add(Me.RadPanel1)
        Me.Route.HeaderText = "Route"
        Me.Route.Location = New System.Drawing.Point(13, 189)
        Me.Route.Name = "Route"
        Me.Route.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Route.Size = New System.Drawing.Size(367, 146)
        Me.Route.TabIndex = 39
        Me.Route.Text = "Route"
        '
        'gvroute
        '
        Me.gvroute.CheckedValue = Nothing
        Me.gvroute.DataSource = Nothing
        Me.gvroute.DisplayMember = "Name"
        Me.gvroute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvroute.Enabled = False
        Me.gvroute.Location = New System.Drawing.Point(10, 43)
        Me.gvroute.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.gvroute.MyShowHeadrText = False
        Me.gvroute.Name = "gvroute"
        Me.gvroute.Size = New System.Drawing.Size(347, 93)
        Me.gvroute.TabIndex = 0
        Me.gvroute.ValueMember = "Code"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.rbtnallroute)
        Me.RadPanel1.Controls.Add(Me.rbtnselectroute)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(10, 20)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(347, 23)
        Me.RadPanel1.TabIndex = 2
        '
        'rbtnallroute
        '
        Me.rbtnallroute.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnallroute.Location = New System.Drawing.Point(96, 2)
        Me.rbtnallroute.Name = "rbtnallroute"
        Me.rbtnallroute.Size = New System.Drawing.Size(49, 18)
        Me.rbtnallroute.TabIndex = 0
        Me.rbtnallroute.TabStop = True
        Me.rbtnallroute.Text = "All"
        Me.rbtnallroute.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnselectroute
        '
        Me.rbtnselectroute.Location = New System.Drawing.Point(151, 3)
        Me.rbtnselectroute.Name = "rbtnselectroute"
        Me.rbtnselectroute.Size = New System.Drawing.Size(60, 18)
        Me.rbtnselectroute.TabIndex = 1
        Me.rbtnselectroute.Text = "Select "
        '
        'lblenddate
        '
        Me.lblenddate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblenddate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblenddate.Location = New System.Drawing.Point(222, 9)
        Me.lblenddate.Name = "lblenddate"
        Me.lblenddate.Size = New System.Drawing.Size(46, 16)
        Me.lblenddate.TabIndex = 37
        Me.lblenddate.Text = "To Date"
        '
        'txtend
        '
        Me.txtend.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtend.CustomFormat = "dd/MM/yyyy"
        Me.txtend.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtend.Location = New System.Drawing.Point(274, 8)
        Me.txtend.MendatroryField = False
        Me.txtend.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtend.MyLinkLable1 = Me.lblenddate
        Me.txtend.MyLinkLable2 = Nothing
        Me.txtend.Name = "txtend"
        Me.txtend.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtend.Size = New System.Drawing.Size(89, 20)
        Me.txtend.TabIndex = 1
        Me.txtend.TabStop = False
        Me.txtend.Text = "30/05/2012"
        Me.txtend.Value = New Date(2012, 5, 30, 0, 0, 0, 0)
        '
        'txtStart
        '
        Me.txtStart.CustomFormat = "dd/MM/yyyy"
        Me.txtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtStart.Location = New System.Drawing.Point(81, 7)
        Me.txtStart.MendatroryField = False
        Me.txtStart.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtStart.MyLinkLable1 = Me.lblstart
        Me.txtStart.MyLinkLable2 = Nothing
        Me.txtStart.Name = "txtStart"
        Me.txtStart.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtStart.Size = New System.Drawing.Size(87, 20)
        Me.txtStart.TabIndex = 0
        Me.txtStart.TabStop = False
        Me.txtStart.Text = "30/05/2012"
        Me.txtStart.Value = New Date(2012, 5, 30, 0, 0, 0, 0)
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Location = New System.Drawing.Point(338, 8)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(70, 18)
        Me.rdbtnclose.TabIndex = 3
        Me.rdbtnclose.Text = "Close"
        '
        'rdbtnprint
        '
        Me.rdbtnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnprint.Location = New System.Drawing.Point(15, 8)
        Me.rdbtnprint.Name = "rdbtnprint"
        Me.rdbtnprint.Size = New System.Drawing.Size(72, 18)
        Me.rdbtnprint.TabIndex = 2
        Me.rdbtnprint.Text = "Print"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnprint)
        Me.SplitContainer1.Size = New System.Drawing.Size(430, 577)
        Me.SplitContainer1.SplitterDistance = 535
        Me.SplitContainer1.TabIndex = 38
        '
        'RouteSaleReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(430, 577)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RouteSaleReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Route Sale Report"
        CType(Me.Locationgb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Locationgb.ResumeLayout(False)
        CType(Me.radiopanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.radiopanel.ResumeLayout(False)
        Me.radiopanel.PerformLayout()
        CType(Me.rbtnall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblstart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.Sales, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Sales.ResumeLayout(False)
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel2.ResumeLayout(False)
        Me.RadPanel2.PerformLayout()
        CType(Me.rbtnallsales, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnselectsales, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Route, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Route.ResumeLayout(False)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.rbtnallroute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnselectroute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblenddate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnprint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Locationgb As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvlocation As common.MyCheckBoxGrid
    Friend WithEvents radiopanel As Telerik.WinControls.UI.RadPanel
    Friend WithEvents rbtnall As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnselect As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents lblstart As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Sales As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvsales As common.MyCheckBoxGrid
    Friend WithEvents RadPanel2 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents rbtnallsales As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnselectsales As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents Route As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvroute As common.MyCheckBoxGrid
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents rbtnallroute As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnselectroute As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents lblenddate As common.Controls.MyLabel
    Friend WithEvents txtend As common.Controls.MyDateTimePicker
    Friend WithEvents txtStart As common.Controls.MyDateTimePicker
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

