<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDailyStockAccountRpt
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
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.RadioBtnDetail = New Telerik.WinControls.UI.RadRadioButton
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.RadioBtnSummary = New Telerik.WinControls.UI.RadRadioButton
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.dtpendtime = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.dtpStarttime = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.dtpend = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadLabel7 = New common.Controls.MyLabel
        Me.dtpstart = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadLabel8 = New common.Controls.MyLabel
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioBtnDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioBtnSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpendtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStarttime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpstart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.btnclose)
        Me.RadGroupBox1.Controls.Add(Me.RadioBtnDetail)
        Me.RadGroupBox1.Controls.Add(Me.btnreset)
        Me.RadGroupBox1.Controls.Add(Me.RadioBtnSummary)
        Me.RadGroupBox1.Controls.Add(Me.btnprint)
        Me.RadGroupBox1.Controls.Add(Me.dtpendtime)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.dtpStarttime)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.dtpend)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel7)
        Me.RadGroupBox1.Controls.Add(Me.dtpstart)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel8)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(4, 12)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(521, 138)
        Me.RadGroupBox1.TabIndex = 0
        '
        'btnclose
        '
        Me.btnclose.Location = New System.Drawing.Point(433, 104)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 34
        Me.btnclose.Text = "Close"
        '
        'RadioBtnDetail
        '
        Me.RadioBtnDetail.Location = New System.Drawing.Point(13, 76)
        Me.RadioBtnDetail.Name = "RadioBtnDetail"
        Me.RadioBtnDetail.Size = New System.Drawing.Size(49, 18)
        Me.RadioBtnDetail.TabIndex = 35
        Me.RadioBtnDetail.Text = "Detail"
        '
        'btnreset
        '
        Me.btnreset.Location = New System.Drawing.Point(13, 107)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 18)
        Me.btnreset.TabIndex = 33
        Me.btnreset.Text = "Reset"
        '
        'RadioBtnSummary
        '
        Me.RadioBtnSummary.Location = New System.Drawing.Point(94, 76)
        Me.RadioBtnSummary.Name = "RadioBtnSummary"
        Me.RadioBtnSummary.Size = New System.Drawing.Size(67, 18)
        Me.RadioBtnSummary.TabIndex = 36
        Me.RadioBtnSummary.Text = "Summary"
        '
        'btnprint
        '
        Me.btnprint.Location = New System.Drawing.Point(87, 107)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(68, 18)
        Me.btnprint.TabIndex = 32
        Me.btnprint.Text = "Print"
        '
        'dtpendtime
        '
        Me.dtpendtime.CustomFormat = "HH:mm tt"
        Me.dtpendtime.Location = New System.Drawing.Point(367, 34)
        Me.dtpendtime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpendtime.Name = "dtpendtime"
        Me.dtpendtime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpendtime.Size = New System.Drawing.Size(134, 20)
        Me.dtpendtime.TabIndex = 30
        Me.dtpendtime.TabStop = False
        Me.dtpendtime.Text = "Wednesday, November 16, 2011"
        Me.dtpendtime.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(13, 36)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(57, 18)
        Me.RadLabel1.TabIndex = 33
        Me.RadLabel1.Text = "Start Time"
        '
        'dtpStarttime
        '
        Me.dtpStarttime.CustomFormat = "HH:mm tt"
        Me.dtpStarttime.Location = New System.Drawing.Point(121, 36)
        Me.dtpStarttime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStarttime.Name = "dtpStarttime"
        Me.dtpStarttime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStarttime.Size = New System.Drawing.Size(134, 20)
        Me.dtpStarttime.TabIndex = 29
        Me.dtpStarttime.TabStop = False
        Me.dtpStarttime.Text = "Wednesday, November 16, 2011"
        Me.dtpStarttime.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(287, 36)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(52, 18)
        Me.RadLabel2.TabIndex = 34
        Me.RadLabel2.Text = "End Time"
        '
        'dtpend
        '
        Me.dtpend.CustomFormat = "dd/MM/yyyy"
        Me.dtpend.Location = New System.Drawing.Point(367, 10)
        Me.dtpend.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpend.Name = "dtpend"
        Me.dtpend.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpend.Size = New System.Drawing.Size(134, 20)
        Me.dtpend.TabIndex = 28
        Me.dtpend.TabStop = False
        Me.dtpend.Text = "Wednesday, November 16, 2011"
        Me.dtpend.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'RadLabel7
        '
        Me.RadLabel7.Location = New System.Drawing.Point(13, 12)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(56, 18)
        Me.RadLabel7.TabIndex = 31
        Me.RadLabel7.Text = "Start Date"
        '
        'dtpstart
        '
        Me.dtpstart.CustomFormat = "dd/MM/yyyy"
        Me.dtpstart.Location = New System.Drawing.Point(121, 12)
        Me.dtpstart.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpstart.Name = "dtpstart"
        Me.dtpstart.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpstart.Size = New System.Drawing.Size(134, 20)
        Me.dtpstart.TabIndex = 27
        Me.dtpstart.TabStop = False
        Me.dtpstart.Text = "Wednesday, November 16, 2011"
        Me.dtpstart.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'RadLabel8
        '
        Me.RadLabel8.Location = New System.Drawing.Point(287, 12)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(52, 18)
        Me.RadLabel8.TabIndex = 32
        Me.RadLabel8.Text = "End Date"
        '
        'FrmDailyStockAccountRpt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(531, 157)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "FrmDailyStockAccountRpt"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Daily Stock Account"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioBtnDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioBtnSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpendtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStarttime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpstart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dtpendtime As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtpStarttime As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtpend As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtpstart As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadioBtnDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadioBtnSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
End Class

