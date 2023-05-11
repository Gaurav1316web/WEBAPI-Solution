<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptListOf_Resource
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
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgResource = New common.MyCheckBoxGrid
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.chkResourceSelect = New common.Controls.MyRadioButton
        Me.chkResourceAll = New common.Controls.MyRadioButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.fnduom = New common.UserControls.txtFinder
        Me.MyLabel10 = New common.Controls.MyLabel
        Me.cboStatus = New common.Controls.MyComboBox
        Me.MyLabel11 = New common.Controls.MyLabel
        Me.MyLabel12 = New common.Controls.MyLabel
        Me.cboType = New common.Controls.MyComboBox
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv = New common.UserControls.MyRadGridView
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.chkResourceSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkResourceAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(868, 509)
        Me.SplitContainer1.SplitterDistance = 472
        Me.SplitContainer1.TabIndex = 3
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(868, 472)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(847, 424)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgResource)
        Me.RadGroupBox2.Controls.Add(Me.Panel5)
        Me.RadGroupBox2.HeaderText = "Resource"
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 67)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(359, 250)
        Me.RadGroupBox2.TabIndex = 316
        Me.RadGroupBox2.Text = "Resource"
        '
        'cbgResource
        '
        Me.cbgResource.CheckedValue = Nothing
        Me.cbgResource.DataSource = Nothing
        Me.cbgResource.DisplayMember = "Name"
        Me.cbgResource.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgResource.Location = New System.Drawing.Point(10, 40)
        Me.cbgResource.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgResource.MyShowHeadrText = False
        Me.cbgResource.Name = "cbgResource"
        Me.cbgResource.Size = New System.Drawing.Size(339, 200)
        Me.cbgResource.TabIndex = 1
        Me.cbgResource.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chkResourceSelect)
        Me.Panel5.Controls.Add(Me.chkResourceAll)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(339, 20)
        Me.Panel5.TabIndex = 0
        '
        'chkResourceSelect
        '
        Me.chkResourceSelect.Location = New System.Drawing.Point(175, 1)
        Me.chkResourceSelect.MyLinkLable1 = Nothing
        Me.chkResourceSelect.MyLinkLable2 = Nothing
        Me.chkResourceSelect.Name = "chkResourceSelect"
        Me.chkResourceSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkResourceSelect.TabIndex = 1
        Me.chkResourceSelect.Text = "Select"
        '
        'chkResourceAll
        '
        Me.chkResourceAll.Location = New System.Drawing.Point(124, 1)
        Me.chkResourceAll.MyLinkLable1 = Nothing
        Me.chkResourceAll.MyLinkLable2 = Nothing
        Me.chkResourceAll.Name = "chkResourceAll"
        Me.chkResourceAll.Size = New System.Drawing.Size(33, 18)
        Me.chkResourceAll.TabIndex = 0
        Me.chkResourceAll.TabStop = True
        Me.chkResourceAll.Text = "All"
        Me.chkResourceAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.fnduom)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel10)
        Me.RadGroupBox1.Controls.Add(Me.cboStatus)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel11)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel12)
        Me.RadGroupBox1.Controls.Add(Me.cboType)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(282, 13)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(476, 35)
        Me.RadGroupBox1.TabIndex = 318
        '
        'fnduom
        '
        Me.fnduom.Location = New System.Drawing.Point(362, 6)
        Me.fnduom.MendatroryField = False
        Me.fnduom.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fnduom.MyLinkLable1 = Nothing
        Me.fnduom.MyLinkLable2 = Nothing
        Me.fnduom.MyReadOnly = False
        Me.fnduom.Name = "fnduom"
        Me.fnduom.Size = New System.Drawing.Size(101, 19)
        Me.fnduom.TabIndex = 316
        Me.fnduom.Value = ""
        '
        'MyLabel10
        '
        Me.MyLabel10.Location = New System.Drawing.Point(13, 6)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(37, 18)
        Me.MyLabel10.TabIndex = 313
        Me.MyLabel10.Text = "Status"
        '
        'cboStatus
        '
        Me.cboStatus.BackColor = System.Drawing.Color.Transparent
        Me.cboStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatus.Location = New System.Drawing.Point(56, 6)
        Me.cboStatus.MendatroryField = False
        Me.cboStatus.MyLinkLable1 = Me.MyLabel10
        Me.cboStatus.MyLinkLable2 = Nothing
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(100, 18)
        Me.cboStatus.TabIndex = 310
        '
        'MyLabel11
        '
        Me.MyLabel11.Location = New System.Drawing.Point(320, 6)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel11.TabIndex = 315
        Me.MyLabel11.Text = "UOM"
        '
        'MyLabel12
        '
        Me.MyLabel12.Location = New System.Drawing.Point(178, 7)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel12.TabIndex = 314
        Me.MyLabel12.Text = "Type"
        '
        'cboType
        '
        Me.cboType.BackColor = System.Drawing.Color.Transparent
        Me.cboType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboType.Location = New System.Drawing.Point(214, 5)
        Me.cboType.MendatroryField = False
        Me.cboType.MyLinkLable1 = Me.MyLabel12
        Me.cboType.MyLinkLable2 = Nothing
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(100, 18)
        Me.cboType.TabIndex = 311
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Select Date"
        Me.RadGroupBox3.Location = New System.Drawing.Point(3, 6)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(273, 42)
        Me.RadGroupBox3.TabIndex = 53
        Me.RadGroupBox3.Text = "Select Date"
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
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(166, 14)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(84, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/09/2013 12:00 AM"
        Me.ToDate.Value = New Date(2013, 9, 24, 0, 0, 0, 0)
        '
        'fromDate
        '
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(51, 12)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(84, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "24/09/2013 12:00 AM"
        Me.fromDate.Value = New Date(2013, 9, 24, 0, 0, 0, 0)
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(847, 424)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(0, 0)
        Me.gv.Name = "gv"
        Me.gv.Size = New System.Drawing.Size(847, 424)
        Me.gv.TabIndex = 0
        Me.gv.Text = "RadGridView1"
        '
        'btnprint
        '
        Me.btnprint.Location = New System.Drawing.Point(88, 11)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(68, 18)
        Me.btnprint.TabIndex = 46
        Me.btnprint.Text = "Print"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(13, 11)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 46
        Me.btnReset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(789, 11)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 47
        Me.btnClose.Text = "Close"
        '
        'RptListOf_Resource
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(868, 509)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptListOf_Resource"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "List of Resource"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.chkResourceSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkResourceAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents cboStatus As common.Controls.MyComboBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents cboType As common.Controls.MyComboBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgResource As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chkResourceSelect As common.Controls.MyRadioButton
    Friend WithEvents chkResourceAll As common.Controls.MyRadioButton
    Friend WithEvents fnduom As common.UserControls.txtFinder
End Class

