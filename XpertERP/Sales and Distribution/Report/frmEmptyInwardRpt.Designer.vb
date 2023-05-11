<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEmptyInwardRpt
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
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.dtpend = New Telerik.WinControls.UI.RadDateTimePicker
        Me.dtpstart = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbglocation = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkSelect = New common.Controls.MyRadioButton
        Me.chkAll = New common.Controls.MyRadioButton
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpstart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.btnClose)
        Me.RadGroupBox1.Controls.Add(Me.btnReset)
        Me.RadGroupBox1.Controls.Add(Me.btnPrint)
        Me.RadGroupBox1.Controls.Add(Me.dtpend)
        Me.RadGroupBox1.Controls.Add(Me.dtpstart)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 9)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(524, 284)
        Me.RadGroupBox1.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(439, 253)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(13, 253)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 9
        Me.btnReset.Text = "Reset"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(89, 253)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 8
        Me.btnPrint.Text = "Print"
        '
        'dtpend
        '
        Me.dtpend.CustomFormat = "dd/MM/yyyy"
        Me.dtpend.Location = New System.Drawing.Point(367, 10)
        Me.dtpend.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpend.Name = "dtpend"
        Me.dtpend.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpend.Size = New System.Drawing.Size(134, 20)
        Me.dtpend.TabIndex = 7
        Me.dtpend.TabStop = False
        Me.dtpend.Text = "Wednesday, November 16, 2011"
        Me.dtpend.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'dtpstart
        '
        Me.dtpstart.CustomFormat = "dd/MM/yyyy"
        Me.dtpstart.Location = New System.Drawing.Point(106, 12)
        Me.dtpstart.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpstart.Name = "dtpstart"
        Me.dtpstart.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpstart.Size = New System.Drawing.Size(134, 20)
        Me.dtpstart.TabIndex = 6
        Me.dtpstart.TabStop = False
        Me.dtpstart.Text = "Wednesday, November 16, 2011"
        Me.dtpstart.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(287, 12)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(52, 18)
        Me.RadLabel2.TabIndex = 5
        Me.RadLabel2.Text = "End Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(23, 12)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(56, 18)
        Me.RadLabel1.TabIndex = 1
        Me.RadLabel1.Text = "Start Date"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbglocation)
        Me.RadGroupBox2.Controls.Add(Me.Panel1)
        Me.RadGroupBox2.HeaderText = "Location"
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 44)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(498, 192)
        Me.RadGroupBox2.TabIndex = 4
        Me.RadGroupBox2.Text = "Location"
        '
        'cbglocation
        '
        Me.cbglocation.CheckedValue = Nothing
        Me.cbglocation.DataSource = Nothing
        Me.cbglocation.DisplayMember = "Name"
        Me.cbglocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbglocation.Location = New System.Drawing.Point(10, 40)
        Me.cbglocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbglocation.MyShowHeadrText = False
        Me.cbglocation.Name = "cbglocation"
        Me.cbglocation.Size = New System.Drawing.Size(478, 142)
        Me.cbglocation.TabIndex = 1
        Me.cbglocation.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkSelect)
        Me.Panel1.Controls.Add(Me.chkAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(478, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkSelect
        '
        Me.chkSelect.Location = New System.Drawing.Point(192, 1)
        Me.chkSelect.MyLinkLable1 = Nothing
        Me.chkSelect.MyLinkLable2 = Nothing
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkSelect.TabIndex = 1
        Me.chkSelect.Text = "Select"
        '
        'chkAll
        '
        Me.chkAll.Location = New System.Drawing.Point(141, 1)
        Me.chkAll.MyLinkLable1 = Nothing
        Me.chkAll.MyLinkLable2 = Nothing
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(33, 18)
        Me.chkAll.TabIndex = 0
        Me.chkAll.Text = "All"
        '
        'FrmEmptyInwardRpt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(549, 305)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "FrmEmptyInwardRpt"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Empty Inward Register Summary"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpstart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dtpend As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtpstart As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbglocation As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkSelect As common.Controls.MyRadioButton
    Friend WithEvents chkAll As common.Controls.MyRadioButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
End Class

