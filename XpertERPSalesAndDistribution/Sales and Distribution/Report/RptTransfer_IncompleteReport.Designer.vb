<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptTransfer_IncompleteReport
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
        Me.txtstartdate = New common.Controls.MyLabel
        Me.dtpFromDate = New common.Controls.MyDateTimePicker
        Me.txtendDate = New common.Controls.MyLabel
        Me.dtpToDate = New common.Controls.MyDateTimePicker
        Me.cbgtans = New common.MyCheckBoxGrid
        Me.cbgloc = New common.MyCheckBoxGrid
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chklocselect = New Telerik.WinControls.UI.RadRadioButton
        Me.chklocall = New Telerik.WinControls.UI.RadRadioButton
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.rbtnselect = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtall = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        CType(Me.txtstartdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtendDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.chklocselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chklocall, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.rbtnselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtstartdate
        '
        Me.txtstartdate.Location = New System.Drawing.Point(12, 27)
        Me.txtstartdate.Name = "txtstartdate"
        Me.txtstartdate.Size = New System.Drawing.Size(59, 18)
        Me.txtstartdate.TabIndex = 0
        Me.txtstartdate.Text = "From Date"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(77, 26)
        Me.dtpFromDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpFromDate.MendatroryField = False
        Me.dtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.MyLinkLable1 = Nothing
        Me.dtpFromDate.MyLinkLable2 = Nothing
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.Size = New System.Drawing.Size(85, 20)
        Me.dtpFromDate.TabIndex = 1
        Me.dtpFromDate.Text = "MyDateTimePicker1"
        Me.dtpFromDate.Value = New Date(2012, 6, 22, 11, 22, 57, 656)
        '
        'txtendDate
        '
        Me.txtendDate.Location = New System.Drawing.Point(182, 26)
        Me.txtendDate.Name = "txtendDate"
        Me.txtendDate.Size = New System.Drawing.Size(45, 18)
        Me.txtendDate.TabIndex = 2
        Me.txtendDate.Text = "To Date"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(233, 25)
        Me.dtpToDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpToDate.MendatroryField = False
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Nothing
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(98, 20)
        Me.dtpToDate.TabIndex = 3
        Me.dtpToDate.Text = "MyDateTimePicker2"
        Me.dtpToDate.Value = New Date(2012, 6, 22, 11, 23, 30, 937)
        '
        'cbgtans
        '
        Me.cbgtans.CheckedValue = Nothing
        Me.cbgtans.DataSource = Nothing
        Me.cbgtans.DisplayMember = "Name"
        Me.cbgtans.Location = New System.Drawing.Point(13, 56)
        Me.cbgtans.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgtans.MyShowHeadrText = False
        Me.cbgtans.Name = "cbgtans"
        Me.cbgtans.Size = New System.Drawing.Size(304, 111)
        Me.cbgtans.TabIndex = 4
        Me.cbgtans.ValueMember = "Code"
        '
        'cbgloc
        '
        Me.cbgloc.CheckedValue = Nothing
        Me.cbgloc.DataSource = Nothing
        Me.cbgloc.DisplayMember = "Name"
        Me.cbgloc.Location = New System.Drawing.Point(13, 52)
        Me.cbgloc.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgloc.MyShowHeadrText = False
        Me.cbgloc.Name = "cbgloc"
        Me.cbgloc.Size = New System.Drawing.Size(304, 121)
        Me.cbgloc.TabIndex = 5
        Me.cbgloc.ValueMember = "Code"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(12, 409)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(59, 24)
        Me.btnprint.TabIndex = 6
        Me.btnprint.Text = "Print"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(290, 409)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(59, 24)
        Me.btnclose.TabIndex = 7
        Me.btnclose.Text = "Close"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chklocselect)
        Me.Panel1.Controls.Add(Me.chklocall)
        Me.Panel1.Location = New System.Drawing.Point(13, 15)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(304, 31)
        Me.Panel1.TabIndex = 8
        '
        'chklocselect
        '
        Me.chklocselect.Location = New System.Drawing.Point(146, 5)
        Me.chklocselect.Name = "chklocselect"
        Me.chklocselect.Size = New System.Drawing.Size(56, 18)
        Me.chklocselect.TabIndex = 3
        Me.chklocselect.Text = "Select"
        '
        'chklocall
        '
        Me.chklocall.Location = New System.Drawing.Point(82, 7)
        Me.chklocall.Name = "chklocall"
        Me.chklocall.Size = New System.Drawing.Size(47, 18)
        Me.chklocall.TabIndex = 2
        Me.chklocall.Text = "All"
        Me.chklocall.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.rbtnselect)
        Me.Panel2.Controls.Add(Me.rbtall)
        Me.Panel2.Location = New System.Drawing.Point(13, 23)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(304, 27)
        Me.Panel2.TabIndex = 9
        '
        'rbtnselect
        '
        Me.rbtnselect.Location = New System.Drawing.Point(146, 4)
        Me.rbtnselect.Name = "rbtnselect"
        Me.rbtnselect.Size = New System.Drawing.Size(56, 18)
        Me.rbtnselect.TabIndex = 1
        Me.rbtnselect.Text = "Select"
        '
        'rbtall
        '
        Me.rbtall.Location = New System.Drawing.Point(75, 6)
        Me.rbtall.Name = "rbtall"
        Me.rbtall.Size = New System.Drawing.Size(47, 18)
        Me.rbtall.TabIndex = 0
        Me.rbtall.Text = "All"
        Me.rbtall.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.Controls.Add(Me.cbgloc)
        Me.RadGroupBox1.FooterImageIndex = -1
        Me.RadGroupBox1.FooterImageKey = ""
        Me.RadGroupBox1.HeaderImageIndex = -1
        Me.RadGroupBox1.HeaderImageKey = ""
        Me.RadGroupBox1.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox1.HeaderText = "Location"
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 51)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox1.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(339, 174)
        Me.RadGroupBox1.TabIndex = 10
        Me.RadGroupBox1.Text = "Location"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.Panel2)
        Me.RadGroupBox2.Controls.Add(Me.cbgtans)
        Me.RadGroupBox2.FooterImageIndex = -1
        Me.RadGroupBox2.FooterImageKey = ""
        Me.RadGroupBox2.HeaderImageIndex = -1
        Me.RadGroupBox2.HeaderImageKey = ""
        Me.RadGroupBox2.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox2.HeaderText = "Transfer"
        Me.RadGroupBox2.Location = New System.Drawing.Point(12, 231)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox2.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(339, 174)
        Me.RadGroupBox2.TabIndex = 11
        Me.RadGroupBox2.Text = "Transfer"
        '
        'RptTransfer_IncompleteReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(354, 445)
        Me.Controls.Add(Me.RadGroupBox2)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Controls.Add(Me.btnclose)
        Me.Controls.Add(Me.btnprint)
        Me.Controls.Add(Me.dtpToDate)
        Me.Controls.Add(Me.txtendDate)
        Me.Controls.Add(Me.dtpFromDate)
        Me.Controls.Add(Me.txtstartdate)
        Me.KeyPreview = True
        Me.Name = "RptTransfer_IncompleteReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Transfer Incomplete Report"
        CType(Me.txtstartdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtendDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.chklocselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chklocall, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.rbtnselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtstartdate As common.Controls.MyLabel
    Friend WithEvents dtpFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtendDate As common.Controls.MyLabel
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents cbgtans As common.MyCheckBoxGrid
    Friend WithEvents cbgloc As common.MyCheckBoxGrid
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents rbtnselect As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtall As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chklocselect As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chklocall As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
End Class

