<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSettlementReport
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
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgTransfer = New common.MyCheckBoxGrid
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.ChkTransferSelect = New common.Controls.MyRadioButton
        Me.chktransferAll = New common.Controls.MyRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.chkCustomerClass = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkClassSelect = New common.Controls.MyRadioButton
        Me.chkClassAll = New common.Controls.MyRadioButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLoadOut = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkLoadoutSelect = New common.Controls.MyRadioButton
        Me.chkLoadoutAll = New common.Controls.MyRadioButton
        Me.Locationgb = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkLocationSelect = New common.Controls.MyRadioButton
        Me.chkLocationAll = New common.Controls.MyRadioButton
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.rdbLoadoutwise = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbTransferwise = New Telerik.WinControls.UI.RadRadioButton
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.DtpTodate = New common.Controls.MyDateTimePicker
        Me.dtpFdate = New common.Controls.MyDateTimePicker
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.ChkTransferSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chktransferAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkClassSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkClassAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkLoadoutSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLoadoutAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Locationgb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Locationgb.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.rdbLoadoutwise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbTransferwise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtpTodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnprint
        '
        Me.btnprint.Location = New System.Drawing.Point(10, 318)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(78, 22)
        Me.btnprint.TabIndex = 100
        Me.btnprint.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(684, 318)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 101
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnReset.Location = New System.Drawing.Point(548, 22)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(17, 19)
        Me.btnReset.TabIndex = 41
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RadGroupBox3)
        Me.GroupBox2.Controls.Add(Me.btnprint)
        Me.GroupBox2.Controls.Add(Me.btnClose)
        Me.GroupBox2.Controls.Add(Me.RadGroupBox2)
        Me.GroupBox2.Controls.Add(Me.RadGroupBox1)
        Me.GroupBox2.Controls.Add(Me.Locationgb)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 57)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(759, 349)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgTransfer)
        Me.RadGroupBox3.Controls.Add(Me.Panel7)
        Me.RadGroupBox3.HeaderText = "Transfer No"
        Me.RadGroupBox3.Location = New System.Drawing.Point(387, 19)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(361, 141)
        Me.RadGroupBox3.TabIndex = 105
        Me.RadGroupBox3.Text = "Transfer No"
        '
        'cbgTransfer
        '
        Me.cbgTransfer.CheckedValue = Nothing
        Me.cbgTransfer.DataSource = Nothing
        Me.cbgTransfer.DisplayMember = "Name"
        Me.cbgTransfer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgTransfer.Location = New System.Drawing.Point(10, 40)
        Me.cbgTransfer.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgTransfer.MyShowHeadrText = False
        Me.cbgTransfer.Name = "cbgTransfer"
        Me.cbgTransfer.Size = New System.Drawing.Size(341, 91)
        Me.cbgTransfer.TabIndex = 1
        Me.cbgTransfer.ValueMember = "Code"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.ChkTransferSelect)
        Me.Panel7.Controls.Add(Me.chktransferAll)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(10, 20)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(341, 20)
        Me.Panel7.TabIndex = 0
        '
        'ChkTransferSelect
        '
        Me.ChkTransferSelect.Location = New System.Drawing.Point(174, 1)
        Me.ChkTransferSelect.MyLinkLable1 = Nothing
        Me.ChkTransferSelect.MyLinkLable2 = Nothing
        Me.ChkTransferSelect.Name = "ChkTransferSelect"
        Me.ChkTransferSelect.Size = New System.Drawing.Size(71, 18)
        Me.ChkTransferSelect.TabIndex = 1
        Me.ChkTransferSelect.Text = "Select"
        '
        'chktransferAll
        '
        Me.chktransferAll.Location = New System.Drawing.Point(120, 1)
        Me.chktransferAll.MyLinkLable1 = Nothing
        Me.chktransferAll.MyLinkLable2 = Nothing
        Me.chktransferAll.Name = "chktransferAll"
        Me.chktransferAll.Size = New System.Drawing.Size(45, 18)
        Me.chktransferAll.TabIndex = 0
        Me.chktransferAll.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.chkCustomerClass)
        Me.RadGroupBox2.Controls.Add(Me.Panel3)
        Me.RadGroupBox2.HeaderText = "Customer Class"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 460)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(367, 131)
        Me.RadGroupBox2.TabIndex = 104
        Me.RadGroupBox2.Text = "Customer Class"
        Me.RadGroupBox2.Visible = False
        '
        'chkCustomerClass
        '
        Me.chkCustomerClass.CheckedValue = Nothing
        Me.chkCustomerClass.DataSource = Nothing
        Me.chkCustomerClass.DisplayMember = "Name"
        Me.chkCustomerClass.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkCustomerClass.Location = New System.Drawing.Point(10, 40)
        Me.chkCustomerClass.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.chkCustomerClass.MyShowHeadrText = False
        Me.chkCustomerClass.Name = "chkCustomerClass"
        Me.chkCustomerClass.Size = New System.Drawing.Size(347, 81)
        Me.chkCustomerClass.TabIndex = 1
        Me.chkCustomerClass.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkClassSelect)
        Me.Panel3.Controls.Add(Me.chkClassAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(347, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkClassSelect
        '
        Me.chkClassSelect.Location = New System.Drawing.Point(182, 1)
        Me.chkClassSelect.MyLinkLable1 = Nothing
        Me.chkClassSelect.MyLinkLable2 = Nothing
        Me.chkClassSelect.Name = "chkClassSelect"
        Me.chkClassSelect.Size = New System.Drawing.Size(71, 18)
        Me.chkClassSelect.TabIndex = 1
        Me.chkClassSelect.Text = "Select"
        '
        'chkClassAll
        '
        Me.chkClassAll.Location = New System.Drawing.Point(131, 1)
        Me.chkClassAll.MyLinkLable1 = Nothing
        Me.chkClassAll.MyLinkLable2 = Nothing
        Me.chkClassAll.Name = "chkClassAll"
        Me.chkClassAll.Size = New System.Drawing.Size(45, 18)
        Me.chkClassAll.TabIndex = 0
        Me.chkClassAll.Text = "All"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgLoadOut)
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.HeaderText = "LoadOut No"
        Me.RadGroupBox1.Location = New System.Drawing.Point(14, 163)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(367, 145)
        Me.RadGroupBox1.TabIndex = 49
        Me.RadGroupBox1.Text = "LoadOut No"
        '
        'cbgLoadOut
        '
        Me.cbgLoadOut.CheckedValue = Nothing
        Me.cbgLoadOut.DataSource = Nothing
        Me.cbgLoadOut.DisplayMember = "Name"
        Me.cbgLoadOut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLoadOut.Location = New System.Drawing.Point(10, 40)
        Me.cbgLoadOut.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLoadOut.MyShowHeadrText = False
        Me.cbgLoadOut.Name = "cbgLoadOut"
        Me.cbgLoadOut.Size = New System.Drawing.Size(347, 95)
        Me.cbgLoadOut.TabIndex = 1
        Me.cbgLoadOut.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkLoadoutSelect)
        Me.Panel1.Controls.Add(Me.chkLoadoutAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(347, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkLoadoutSelect
        '
        Me.chkLoadoutSelect.Location = New System.Drawing.Point(168, 1)
        Me.chkLoadoutSelect.MyLinkLable1 = Nothing
        Me.chkLoadoutSelect.MyLinkLable2 = Nothing
        Me.chkLoadoutSelect.Name = "chkLoadoutSelect"
        Me.chkLoadoutSelect.Size = New System.Drawing.Size(71, 18)
        Me.chkLoadoutSelect.TabIndex = 1
        Me.chkLoadoutSelect.Text = "Select"
        '
        'chkLoadoutAll
        '
        Me.chkLoadoutAll.Location = New System.Drawing.Point(101, 1)
        Me.chkLoadoutAll.MyLinkLable1 = Nothing
        Me.chkLoadoutAll.MyLinkLable2 = Nothing
        Me.chkLoadoutAll.Name = "chkLoadoutAll"
        Me.chkLoadoutAll.Size = New System.Drawing.Size(45, 18)
        Me.chkLoadoutAll.TabIndex = 0
        Me.chkLoadoutAll.Text = "All"
        '
        'Locationgb
        '
        Me.Locationgb.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Locationgb.Controls.Add(Me.cbgLocation)
        Me.Locationgb.Controls.Add(Me.Panel2)
        Me.Locationgb.HeaderText = "Location"
        Me.Locationgb.Location = New System.Drawing.Point(14, 15)
        Me.Locationgb.Name = "Locationgb"
        Me.Locationgb.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Locationgb.Size = New System.Drawing.Size(367, 145)
        Me.Locationgb.TabIndex = 48
        Me.Locationgb.Text = "Location"
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
        Me.cbgLocation.Size = New System.Drawing.Size(347, 95)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkLocationSelect)
        Me.Panel2.Controls.Add(Me.chkLocationAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(347, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.Location = New System.Drawing.Point(168, 1)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(71, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.Location = New System.Drawing.Point(101, 1)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(45, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rdbLoadoutwise)
        Me.GroupBox3.Controls.Add(Me.rdbTransferwise)
        Me.GroupBox3.Controls.Add(Me.btnReset)
        Me.GroupBox3.Controls.Add(Me.MyLabel1)
        Me.GroupBox3.Controls.Add(Me.MyLabel2)
        Me.GroupBox3.Controls.Add(Me.DtpTodate)
        Me.GroupBox3.Controls.Add(Me.dtpFdate)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(759, 45)
        Me.GroupBox3.TabIndex = 10
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Select"
        '
        'rdbLoadoutwise
        '
        Me.rdbLoadoutwise.Location = New System.Drawing.Point(120, 21)
        Me.rdbLoadoutwise.Name = "rdbLoadoutwise"
        Me.rdbLoadoutwise.Size = New System.Drawing.Size(116, 18)
        Me.rdbLoadoutwise.TabIndex = 105
        Me.rdbLoadoutwise.Text = "LoadOut Wise"
        '
        'rdbTransferwise
        '
        Me.rdbTransferwise.Location = New System.Drawing.Point(10, 20)
        Me.rdbTransferwise.Name = "rdbTransferwise"
        Me.rdbTransferwise.Size = New System.Drawing.Size(104, 18)
        Me.rdbTransferwise.TabIndex = 104
        Me.rdbTransferwise.Text = "Transfer Wise"
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(403, 22)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel1.TabIndex = 101
        Me.MyLabel1.Text = "To Date"
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(242, 20)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(59, 18)
        Me.MyLabel2.TabIndex = 101
        Me.MyLabel2.Text = "From Date"
        '
        'DtpTodate
        '
        Me.DtpTodate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.DtpTodate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpTodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpTodate.Location = New System.Drawing.Point(454, 23)
        Me.DtpTodate.MendatroryField = False
        Me.DtpTodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpTodate.MyLinkLable1 = Nothing
        Me.DtpTodate.MyLinkLable2 = Nothing
        Me.DtpTodate.Name = "DtpTodate"
        Me.DtpTodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpTodate.Size = New System.Drawing.Size(86, 18)
        Me.DtpTodate.TabIndex = 99
        Me.DtpTodate.TabStop = False
        Me.DtpTodate.Text = "18/05/2011 02:11 PM"
        Me.DtpTodate.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'dtpFdate
        '
        Me.dtpFdate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpFdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFdate.Location = New System.Drawing.Point(307, 21)
        Me.dtpFdate.MendatroryField = False
        Me.dtpFdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFdate.MyLinkLable1 = Nothing
        Me.dtpFdate.MyLinkLable2 = Nothing
        Me.dtpFdate.Name = "dtpFdate"
        Me.dtpFdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFdate.Size = New System.Drawing.Size(85, 18)
        Me.dtpFdate.TabIndex = 98
        Me.dtpFdate.TabStop = False
        Me.dtpFdate.Text = "18/05/2011 02:11 PM"
        Me.dtpFdate.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'FrmSettlementReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(778, 417)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Name = "FrmSettlementReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Settlement Report"
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.ChkTransferSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chktransferAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkClassSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkClassAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkLoadoutSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLoadoutAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Locationgb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Locationgb.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.rdbLoadoutwise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbTransferwise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtpTodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgTransfer As common.MyCheckBoxGrid
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents ChkTransferSelect As common.Controls.MyRadioButton
    Friend WithEvents chktransferAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkCustomerClass As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkClassSelect As common.Controls.MyRadioButton
    Friend WithEvents chkClassAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLoadOut As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkLoadoutSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLoadoutAll As common.Controls.MyRadioButton
    Friend WithEvents Locationgb As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbLoadoutwise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbTransferwise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents DtpTodate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpFdate As common.Controls.MyDateTimePicker
End Class

