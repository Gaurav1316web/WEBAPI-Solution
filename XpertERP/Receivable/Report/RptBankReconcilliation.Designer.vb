<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptBankReconcilliation
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
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.dtpfromdate = New common.Controls.MyDateTimePicker()
        Me.dtptodate = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.btnprint = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.cbgfrombank = New common.MyCheckBoxGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkfrombankSelect = New common.Controls.MyRadioButton()
        Me.chkfrombankAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.chkfrombankSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkfrombankAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(12, 21)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(59, 18)
        Me.MyLabel1.TabIndex = 0
        Me.MyLabel1.Text = "From Date"
        '
        'dtpfromdate
        '
        Me.dtpfromdate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpfromdate.Location = New System.Drawing.Point(82, 21)
        Me.dtpfromdate.MendatroryField = False
        Me.dtpfromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate.MyLinkLable1 = Nothing
        Me.dtpfromdate.MyLinkLable2 = Nothing
        Me.dtpfromdate.Name = "dtpfromdate"
        Me.dtpfromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate.Size = New System.Drawing.Size(98, 20)
        Me.dtpfromdate.TabIndex = 1
        Me.dtpfromdate.TabStop = False
        Me.dtpfromdate.Text = "27/07/2012"
        Me.dtpfromdate.Value = New Date(2012, 7, 27, 16, 40, 17, 687)
        '
        'dtptodate
        '
        Me.dtptodate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtptodate.Location = New System.Drawing.Point(283, 23)
        Me.dtptodate.MendatroryField = False
        Me.dtptodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.MyLinkLable1 = Nothing
        Me.dtptodate.MyLinkLable2 = Nothing
        Me.dtptodate.Name = "dtptodate"
        Me.dtptodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.Size = New System.Drawing.Size(98, 20)
        Me.dtptodate.TabIndex = 3
        Me.dtptodate.TabStop = False
        Me.dtptodate.Text = "27/07/2012"
        Me.dtptodate.Value = New Date(2012, 7, 27, 16, 40, 17, 687)
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(231, 23)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel2.TabIndex = 2
        Me.MyLabel2.Text = "To Date"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(12, 292)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(75, 24)
        Me.btnprint.TabIndex = 8
        Me.btnprint.Text = "Print"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(306, 292)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(75, 24)
        Me.btnclose.TabIndex = 9
        Me.btnclose.Text = "Close"
        '
        'cbgfrombank
        '
        Me.cbgfrombank.CheckedValue = Nothing
        Me.cbgfrombank.DataSource = Nothing
        Me.cbgfrombank.DisplayMember = "Name"
        Me.cbgfrombank.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgfrombank.Location = New System.Drawing.Point(10, 40)
        Me.cbgfrombank.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgfrombank.MyShowHeadrText = False
        Me.cbgfrombank.Name = "cbgfrombank"
        Me.cbgfrombank.Size = New System.Drawing.Size(347, 184)
        Me.cbgfrombank.TabIndex = 1
        Me.cbgfrombank.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkfrombankSelect)
        Me.Panel2.Controls.Add(Me.chkfrombankAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(347, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkfrombankSelect
        '
        Me.chkfrombankSelect.Location = New System.Drawing.Point(168, 1)
        Me.chkfrombankSelect.MyLinkLable1 = Nothing
        Me.chkfrombankSelect.MyLinkLable2 = Nothing
        Me.chkfrombankSelect.Name = "chkfrombankSelect"
        Me.chkfrombankSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkfrombankSelect.TabIndex = 1
        Me.chkfrombankSelect.Text = "Select"
        '
        'chkfrombankAll
        '
        Me.chkfrombankAll.Location = New System.Drawing.Point(101, 1)
        Me.chkfrombankAll.MyLinkLable1 = Nothing
        Me.chkfrombankAll.MyLinkLable2 = Nothing
        Me.chkfrombankAll.Name = "chkfrombankAll"
        Me.chkfrombankAll.Size = New System.Drawing.Size(33, 18)
        Me.chkfrombankAll.TabIndex = 0
        Me.chkfrombankAll.Text = "All"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgfrombank)
        Me.RadGroupBox1.Controls.Add(Me.Panel2)
        Me.RadGroupBox1.HeaderText = " Bank"
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 52)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(367, 234)
        Me.RadGroupBox1.TabIndex = 51
        Me.RadGroupBox1.Text = " Bank"
        '
        'RptBankReconcilliation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(409, 330)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Controls.Add(Me.btnclose)
        Me.Controls.Add(Me.btnprint)
        Me.Controls.Add(Me.dtptodate)
        Me.Controls.Add(Me.MyLabel2)
        Me.Controls.Add(Me.dtpfromdate)
        Me.Controls.Add(Me.MyLabel1)
        Me.Name = "RptBankReconcilliation"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bank Reconcilliation"
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkfrombankSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkfrombankAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents dtpfromdate As common.Controls.MyDateTimePicker
    Friend WithEvents dtptodate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents cbgfrombank As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkfrombankSelect As common.Controls.MyRadioButton
    Friend WithEvents chkfrombankAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
End Class

