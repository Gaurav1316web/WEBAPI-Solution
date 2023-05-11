<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCreateReceiptAgainstSales
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        Me.txtToDate = New common.Controls.MyDateTimePicker
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtFromDate = New common.Controls.MyDateTimePicker
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.dgvTransfer = New common.MyCheckBoxGrid
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.chkTransferSelect = New common.Controls.MyRadioButton
        Me.chkTransferAll = New common.Controls.MyRadioButton
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkLocationSelect = New common.Controls.MyRadioButton
        Me.chkLocatioAll = New common.Controls.MyRadioButton
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgRoute = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkRouteSelect = New common.Controls.MyRadioButton
        Me.chkRouteAll = New common.Controls.MyRadioButton
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnSelect = New Telerik.WinControls.UI.RadButton
        Me.btnSaveAndPost = New Telerik.WinControls.UI.RadButton
        Me.Panel1.SuspendLayout()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.chkTransferSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTransferAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocatioAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkRouteSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRouteAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSaveAndPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnRefresh)
        Me.Panel1.Controls.Add(Me.txtToDate)
        Me.Panel1.Controls.Add(Me.txtFromDate)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Controls.Add(Me.MyLabel1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1028, 31)
        Me.Panel1.TabIndex = 0
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(319, 2)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(84, 24)
        Me.btnRefresh.TabIndex = 2
        Me.btnRefresh.Text = ">>"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.txtToDate.Location = New System.Drawing.Point(228, 4)
        Me.txtToDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.MyLabel2
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(85, 20)
        Me.txtToDate.TabIndex = 3
        Me.txtToDate.Text = "MyDateTimePicker2"
        Me.txtToDate.Value = New Date(2012, 6, 6, 17, 33, 25, 468)
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(177, 5)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel2.TabIndex = 1
        Me.MyLabel2.Text = "To Date"
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.txtFromDate.Location = New System.Drawing.Point(79, 4)
        Me.txtFromDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.MyLabel1
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(85, 20)
        Me.txtFromDate.TabIndex = 2
        Me.txtFromDate.Text = "MyDateTimePicker1"
        Me.txtFromDate.Value = New Date(2012, 6, 6, 17, 33, 25, 468)
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.MyLabel1.Location = New System.Drawing.Point(13, 5)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(62, 18)
        Me.MyLabel1.TabIndex = 0
        Me.MyLabel1.Text = "From Date"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 31)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSelect)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSaveAndPost)
        Me.SplitContainer1.Size = New System.Drawing.Size(1028, 451)
        Me.SplitContainer1.SplitterDistance = 418
        Me.SplitContainer1.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox4)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(1028, 418)
        Me.SplitContainer2.SplitterDistance = 154
        Me.SplitContainer2.TabIndex = 1
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.dgvTransfer)
        Me.RadGroupBox1.Controls.Add(Me.Panel5)
        Me.RadGroupBox1.FooterImageIndex = -1
        Me.RadGroupBox1.FooterImageKey = ""
        Me.RadGroupBox1.HeaderImageIndex = -1
        Me.RadGroupBox1.HeaderImageKey = ""
        Me.RadGroupBox1.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox1.HeaderText = "Transfer"
        Me.RadGroupBox1.Location = New System.Drawing.Point(725, 1)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox1.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(354, 151)
        Me.RadGroupBox1.TabIndex = 114
        Me.RadGroupBox1.Text = "Transfer"
        '
        'dgvTransfer
        '
        Me.dgvTransfer.CheckedValue = Nothing
        Me.dgvTransfer.DataSource = Nothing
        Me.dgvTransfer.DisplayMember = "Name"
        Me.dgvTransfer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTransfer.Location = New System.Drawing.Point(10, 40)
        Me.dgvTransfer.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.dgvTransfer.MyShowHeadrText = False
        Me.dgvTransfer.Name = "dgvTransfer"
        Me.dgvTransfer.Size = New System.Drawing.Size(334, 101)
        Me.dgvTransfer.TabIndex = 1
        Me.dgvTransfer.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chkTransferSelect)
        Me.Panel5.Controls.Add(Me.chkTransferAll)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(334, 20)
        Me.Panel5.TabIndex = 0
        '
        'chkTransferSelect
        '
        Me.chkTransferSelect.Location = New System.Drawing.Point(178, 1)
        Me.chkTransferSelect.MyLinkLable1 = Nothing
        Me.chkTransferSelect.MyLinkLable2 = Nothing
        Me.chkTransferSelect.Name = "chkTransferSelect"
        Me.chkTransferSelect.Size = New System.Drawing.Size(71, 18)
        Me.chkTransferSelect.TabIndex = 1
        Me.chkTransferSelect.Text = "Select"
        '
        'chkTransferAll
        '
        Me.chkTransferAll.Location = New System.Drawing.Point(127, 1)
        Me.chkTransferAll.MyLinkLable1 = Nothing
        Me.chkTransferAll.MyLinkLable2 = Nothing
        Me.chkTransferAll.Name = "chkTransferAll"
        Me.chkTransferAll.Size = New System.Drawing.Size(45, 18)
        Me.chkTransferAll.TabIndex = 0
        Me.chkTransferAll.Text = "All"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox5.Controls.Add(Me.Panel2)
        Me.RadGroupBox5.FooterImageIndex = -1
        Me.RadGroupBox5.FooterImageKey = ""
        Me.RadGroupBox5.HeaderImageIndex = -1
        Me.RadGroupBox5.HeaderImageKey = ""
        Me.RadGroupBox5.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox5.HeaderText = "Location"
        Me.RadGroupBox5.Location = New System.Drawing.Point(5, 1)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox5.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(353, 151)
        Me.RadGroupBox5.TabIndex = 113
        Me.RadGroupBox5.Text = "Location"
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
        Me.cbgLocation.Size = New System.Drawing.Size(333, 101)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkLocationSelect)
        Me.Panel2.Controls.Add(Me.chkLocatioAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(333, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.Location = New System.Drawing.Point(178, 1)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(71, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocatioAll
        '
        Me.chkLocatioAll.Location = New System.Drawing.Point(127, 1)
        Me.chkLocatioAll.MyLinkLable1 = Nothing
        Me.chkLocatioAll.MyLinkLable2 = Nothing
        Me.chkLocatioAll.Name = "chkLocatioAll"
        Me.chkLocatioAll.Size = New System.Drawing.Size(45, 18)
        Me.chkLocatioAll.TabIndex = 0
        Me.chkLocatioAll.Text = "All"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgRoute)
        Me.RadGroupBox4.Controls.Add(Me.Panel4)
        Me.RadGroupBox4.FooterImageIndex = -1
        Me.RadGroupBox4.FooterImageKey = ""
        Me.RadGroupBox4.HeaderImageIndex = -1
        Me.RadGroupBox4.HeaderImageKey = ""
        Me.RadGroupBox4.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox4.HeaderText = "Route"
        Me.RadGroupBox4.Location = New System.Drawing.Point(364, 1)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox4.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(356, 151)
        Me.RadGroupBox4.TabIndex = 112
        Me.RadGroupBox4.Text = "Route"
        '
        'cbgRoute
        '
        Me.cbgRoute.CheckedValue = Nothing
        Me.cbgRoute.DataSource = Nothing
        Me.cbgRoute.DisplayMember = "Name"
        Me.cbgRoute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgRoute.Location = New System.Drawing.Point(10, 40)
        Me.cbgRoute.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgRoute.MyShowHeadrText = False
        Me.cbgRoute.Name = "cbgRoute"
        Me.cbgRoute.Size = New System.Drawing.Size(336, 101)
        Me.cbgRoute.TabIndex = 1
        Me.cbgRoute.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkRouteSelect)
        Me.Panel4.Controls.Add(Me.chkRouteAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(336, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkRouteSelect
        '
        Me.chkRouteSelect.Location = New System.Drawing.Point(178, 1)
        Me.chkRouteSelect.MyLinkLable1 = Nothing
        Me.chkRouteSelect.MyLinkLable2 = Nothing
        Me.chkRouteSelect.Name = "chkRouteSelect"
        Me.chkRouteSelect.Size = New System.Drawing.Size(71, 18)
        Me.chkRouteSelect.TabIndex = 1
        Me.chkRouteSelect.Text = "Select"
        '
        'chkRouteAll
        '
        Me.chkRouteAll.Location = New System.Drawing.Point(127, 1)
        Me.chkRouteAll.MyLinkLable1 = Nothing
        Me.chkRouteAll.MyLinkLable2 = Nothing
        Me.chkRouteAll.Name = "chkRouteAll"
        Me.chkRouteAll.Size = New System.Drawing.Size(45, 18)
        Me.chkRouteAll.TabIndex = 0
        Me.chkRouteAll.Text = "All"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(1028, 260)
        Me.gv1.TabIndex = 0
        Me.gv1.Text = "RadGridView1"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(941, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 24)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnSelect
        '
        Me.btnSelect.Location = New System.Drawing.Point(94, 3)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(84, 24)
        Me.btnSelect.TabIndex = 1
        Me.btnSelect.Text = "Select All"
        '
        'btnSaveAndPost
        '
        Me.btnSaveAndPost.Location = New System.Drawing.Point(6, 3)
        Me.btnSaveAndPost.Name = "btnSaveAndPost"
        Me.btnSaveAndPost.Size = New System.Drawing.Size(84, 24)
        Me.btnSaveAndPost.TabIndex = 0
        Me.btnSaveAndPost.Text = "Create Empties"
        '
        'FrmCreateReceiptAgainstSales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 482)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.KeyPreview = True
        Me.Name = "FrmCreateReceiptAgainstSales"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Create Empities Against Sales Invoice"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        CType(Me.chkTransferSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTransferAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocatioAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.chkRouteSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRouteAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSaveAndPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents btnSelect As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSaveAndPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dgvTransfer As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chkTransferSelect As common.Controls.MyRadioButton
    Friend WithEvents chkTransferAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocatioAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgRoute As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkRouteSelect As common.Controls.MyRadioButton
    Friend WithEvents chkRouteAll As common.Controls.MyRadioButton
End Class

