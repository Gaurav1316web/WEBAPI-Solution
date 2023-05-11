<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCashVoucher
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgBankCode = New common.MyCheckBoxGrid
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel
        Me.chkBankSelect = New common.Controls.MyRadioButton
        Me.chkBankAll = New common.Controls.MyRadioButton
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.chkLocationSelect = New common.Controls.MyRadioButton
        Me.chkLocationAll = New common.Controls.MyRadioButton
        Me.dtpToDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.lblFromDate = New common.Controls.MyLabel
        Me.lblToDate = New common.Controls.MyLabel
        Me.dtpFromdate1 = New Telerik.WinControls.UI.RadDateTimePicker
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.chkBankSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBankAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.dtpToDate)
        Me.RadGroupBox1.Controls.Add(Me.lblFromDate)
        Me.RadGroupBox1.Controls.Add(Me.lblToDate)
        Me.RadGroupBox1.Controls.Add(Me.dtpFromdate1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(20, 14)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(330, 394)
        Me.RadGroupBox1.TabIndex = 0
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgBankCode)
        Me.RadGroupBox2.Controls.Add(Me.RadPanel1)
        Me.RadGroupBox2.HeaderText = "Bank Code"
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 47)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(299, 171)
        Me.RadGroupBox2.TabIndex = 76
        Me.RadGroupBox2.Text = "Bank Code"
        '
        'cbgBankCode
        '
        Me.cbgBankCode.AccessibleName = "cbgBankCode"
        Me.cbgBankCode.CheckedValue = Nothing
        Me.cbgBankCode.DataSource = Nothing
        Me.cbgBankCode.DisplayMember = "Name"
        Me.cbgBankCode.Location = New System.Drawing.Point(10, 41)
        Me.cbgBankCode.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgBankCode.MyShowHeadrText = False
        Me.cbgBankCode.Name = "cbgBankCode"
        Me.cbgBankCode.Size = New System.Drawing.Size(279, 121)
        Me.cbgBankCode.TabIndex = 1
        Me.cbgBankCode.ValueMember = "Code"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.chkBankSelect)
        Me.RadPanel1.Controls.Add(Me.chkBankAll)
        Me.RadPanel1.Location = New System.Drawing.Point(10, 14)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(279, 30)
        Me.RadPanel1.TabIndex = 0
        '
        'chkBankSelect
        '
        Me.chkBankSelect.AccessibleName = "chkBankSelect"
        Me.chkBankSelect.Location = New System.Drawing.Point(106, 3)
        Me.chkBankSelect.MyLinkLable1 = Nothing
        Me.chkBankSelect.MyLinkLable2 = Nothing
        Me.chkBankSelect.Name = "chkBankSelect"
        Me.chkBankSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkBankSelect.TabIndex = 1
        Me.chkBankSelect.Text = "Select"
        '
        'chkBankAll
        '
        Me.chkBankAll.AccessibleName = "chkBankAll"
        Me.chkBankAll.Location = New System.Drawing.Point(46, 3)
        Me.chkBankAll.MyLinkLable1 = Nothing
        Me.chkBankAll.MyLinkLable2 = Nothing
        Me.chkBankAll.Name = "chkBankAll"
        Me.chkBankAll.Size = New System.Drawing.Size(33, 18)
        Me.chkBankAll.TabIndex = 0
        Me.chkBankAll.Text = "All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox3.Controls.Add(Me.Panel5)
        Me.RadGroupBox3.HeaderText = " Location"
        Me.RadGroupBox3.Location = New System.Drawing.Point(13, 215)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(299, 162)
        Me.RadGroupBox3.TabIndex = 71
        Me.RadGroupBox3.Text = " Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.AccessibleDescription = "cbgLocation"
        Me.cbgLocation.AccessibleName = ""
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 51)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(279, 101)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chkLocationSelect)
        Me.Panel5.Controls.Add(Me.chkLocationAll)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(279, 31)
        Me.Panel5.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.AccessibleDescription = "chkLoc_select"
        Me.chkLocationSelect.AccessibleName = "chkLocationSelect"
        Me.chkLocationSelect.Location = New System.Drawing.Point(106, 7)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.AccessibleDescription = "chkLocAll"
        Me.chkLocationAll.AccessibleName = "chkLocationAll"
        Me.chkLocationAll.Location = New System.Drawing.Point(46, 7)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'dtpToDate
        '
        Me.dtpToDate.AccessibleName = "dtpToDate"
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(224, 21)
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(88, 20)
        Me.dtpToDate.TabIndex = 33
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "01/02/2012"
        Me.dtpToDate.Value = New Date(2012, 2, 1, 0, 0, 0, 0)
        '
        'lblFromDate
        '
        Me.lblFromDate.Location = New System.Drawing.Point(13, 23)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblFromDate.TabIndex = 30
        Me.lblFromDate.Text = "From Date"
        '
        'lblToDate
        '
        Me.lblToDate.Location = New System.Drawing.Point(173, 23)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 31
        Me.lblToDate.Text = "To Date"
        '
        'dtpFromdate1
        '
        Me.dtpFromdate1.AccessibleName = "dtpFromDate"
        Me.dtpFromdate1.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromdate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromdate1.Location = New System.Drawing.Point(78, 21)
        Me.dtpFromdate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.Name = "dtpFromdate1"
        Me.dtpFromdate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.Size = New System.Drawing.Size(89, 20)
        Me.dtpFromdate1.TabIndex = 32
        Me.dtpFromdate1.TabStop = False
        Me.dtpFromdate1.Text = "01/02/2012"
        Me.dtpFromdate1.Value = New Date(2012, 2, 1, 0, 0, 0, 0)
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(73, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(59, 24)
        Me.btnPrint.TabIndex = 73
        Me.btnPrint.Text = "Print"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(12, 3)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(55, 24)
        Me.btnreset.TabIndex = 75
        Me.btnreset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(293, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(57, 24)
        Me.btnClose.TabIndex = 74
        Me.btnClose.Text = "Close"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Size = New System.Drawing.Size(362, 450)
        Me.SplitContainer1.SplitterDistance = 414
        Me.SplitContainer1.TabIndex = 1
        '
        'FrmCashVoucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(362, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmCashVoucher"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Contra Voucher"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.chkBankSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBankAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dtpToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtpFromdate1 As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents cbgBankCode As common.MyCheckBoxGrid
    Friend WithEvents chkBankSelect As common.Controls.MyRadioButton
    Friend WithEvents chkBankAll As common.Controls.MyRadioButton
    Friend WithEvents lblFromDate As common.Controls.MyLabel
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

