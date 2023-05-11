<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PendingSaleOrderReport
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
        Me.dtptodate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.dtpfrmdatte = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.txtSaleOrderNo = New common.UserControls.txtFinder
        Me.RadGroupBox9 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgdoc = New common.MyCheckBoxGrid
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.chkDoc_select1 = New common.Controls.MyRadioButton
        Me.chkdocAll1 = New common.Controls.MyRadioButton
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton
        Me.rdbtnprint = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpfrmdatte, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox9.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.chkDoc_select1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkdocAll1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnprint, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadGroupBox1.HeaderText = " "
        Me.RadGroupBox1.Location = New System.Drawing.Point(16, 20)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(473, 333)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = " "
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.dtptodate)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox2.Controls.Add(Me.dtpfrmdatte)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox2.Controls.Add(Me.txtSaleOrderNo)
        Me.RadGroupBox2.Controls.Add(Me.RadGroupBox9)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 23)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(447, 293)
        Me.RadGroupBox2.TabIndex = 0
        '
        'dtptodate
        '
        Me.dtptodate.CustomFormat = "dd/MM/yyyy"
        Me.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtptodate.Location = New System.Drawing.Point(343, 44)
        Me.dtptodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.Name = "dtptodate"
        Me.dtptodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.Size = New System.Drawing.Size(91, 20)
        Me.dtptodate.TabIndex = 15
        Me.dtptodate.TabStop = False
        Me.dtptodate.Text = "28/11/2011"
        Me.dtptodate.Value = New Date(2011, 11, 28, 17, 43, 21, 78)
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(292, 46)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel3.TabIndex = 14
        Me.RadLabel3.Text = "To Date"
        '
        'dtpfrmdatte
        '
        Me.dtpfrmdatte.CustomFormat = "dd/MM/yyyy"
        Me.dtpfrmdatte.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfrmdatte.Location = New System.Drawing.Point(106, 42)
        Me.dtpfrmdatte.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfrmdatte.Name = "dtpfrmdatte"
        Me.dtpfrmdatte.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfrmdatte.Size = New System.Drawing.Size(91, 20)
        Me.dtpfrmdatte.TabIndex = 13
        Me.dtpfrmdatte.TabStop = False
        Me.dtpfrmdatte.Text = "28/11/2011"
        Me.dtpfrmdatte.Value = New Date(2011, 11, 28, 17, 43, 21, 78)
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(14, 44)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel2.TabIndex = 12
        Me.RadLabel2.Text = "From Date"
        '
        'txtSaleOrderNo
        '
        Me.txtSaleOrderNo.Location = New System.Drawing.Point(106, 10)
        Me.txtSaleOrderNo.MendatroryField = False
        Me.txtSaleOrderNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleOrderNo.MyLinkLable1 = Nothing
        Me.txtSaleOrderNo.MyLinkLable2 = Nothing
        Me.txtSaleOrderNo.MyReadOnly = False
        Me.txtSaleOrderNo.Name = "txtSaleOrderNo"
        Me.txtSaleOrderNo.Size = New System.Drawing.Size(172, 19)
        Me.txtSaleOrderNo.TabIndex = 11
        Me.txtSaleOrderNo.Value = ""
        '
        'RadGroupBox9
        '
        Me.RadGroupBox9.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox9.Controls.Add(Me.cbgdoc)
        Me.RadGroupBox9.Controls.Add(Me.Panel6)
        Me.RadGroupBox9.HeaderText = "Select Customer"
        Me.RadGroupBox9.Location = New System.Drawing.Point(13, 68)
        Me.RadGroupBox9.Name = "RadGroupBox9"
        Me.RadGroupBox9.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox9.Size = New System.Drawing.Size(421, 215)
        Me.RadGroupBox9.TabIndex = 4
        Me.RadGroupBox9.Text = "Select Customer"
        '
        'cbgdoc
        '
        Me.cbgdoc.AccessibleDescription = "cbgdoc"
        Me.cbgdoc.AccessibleName = ""
        Me.cbgdoc.CheckedValue = Nothing
        Me.cbgdoc.DataSource = Nothing
        Me.cbgdoc.DisplayMember = "Name"
        Me.cbgdoc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgdoc.Location = New System.Drawing.Point(10, 40)
        Me.cbgdoc.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgdoc.MyShowHeadrText = False
        Me.cbgdoc.Name = "cbgdoc"
        Me.cbgdoc.Size = New System.Drawing.Size(401, 165)
        Me.cbgdoc.TabIndex = 1
        Me.cbgdoc.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.chkDoc_select1)
        Me.Panel6.Controls.Add(Me.chkdocAll1)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(401, 20)
        Me.Panel6.TabIndex = 0
        '
        'chkDoc_select1
        '
        Me.chkDoc_select1.AccessibleDescription = "chkDoc_select"
        Me.chkDoc_select1.Location = New System.Drawing.Point(192, 1)
        Me.chkDoc_select1.MyLinkLable1 = Nothing
        Me.chkDoc_select1.MyLinkLable2 = Nothing
        Me.chkDoc_select1.Name = "chkDoc_select1"
        Me.chkDoc_select1.Size = New System.Drawing.Size(50, 18)
        Me.chkDoc_select1.TabIndex = 1
        Me.chkDoc_select1.Text = "Select"
        '
        'chkdocAll1
        '
        Me.chkdocAll1.AccessibleDescription = "chkdocAll"
        Me.chkdocAll1.Location = New System.Drawing.Point(141, 1)
        Me.chkdocAll1.MyLinkLable1 = Nothing
        Me.chkdocAll1.MyLinkLable2 = Nothing
        Me.chkdocAll1.Name = "chkdocAll1"
        Me.chkdocAll1.Size = New System.Drawing.Size(33, 18)
        Me.chkdocAll1.TabIndex = 0
        Me.chkdocAll1.Text = "All"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(14, 11)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(77, 18)
        Me.RadLabel1.TabIndex = 8
        Me.RadLabel1.Text = "Sale Order No"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(16, 6)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(72, 18)
        Me.btnReset.TabIndex = 13
        Me.btnReset.Text = "Reset"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Location = New System.Drawing.Point(419, 6)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(70, 18)
        Me.rdbtnclose.TabIndex = 12
        Me.rdbtnclose.Text = "Close"
        '
        'rdbtnprint
        '
        Me.rdbtnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnprint.Location = New System.Drawing.Point(94, 6)
        Me.rdbtnprint.Name = "rdbtnprint"
        Me.rdbtnprint.Size = New System.Drawing.Size(72, 18)
        Me.rdbtnprint.TabIndex = 11
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(503, 396)
        Me.SplitContainer1.SplitterDistance = 358
        Me.SplitContainer1.TabIndex = 1
        '
        'PendingSaleOrderReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(503, 396)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "PendingSaleOrderReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Pending SaleOrder Report"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpfrmdatte, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox9.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.chkDoc_select1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkdocAll1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnprint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtSaleOrderNo As common.UserControls.txtFinder
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox9 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgdoc As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chkDoc_select1 As common.Controls.MyRadioButton
    Friend WithEvents chkdocAll1 As common.Controls.MyRadioButton
    Friend WithEvents dtptodate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtpfrmdatte As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

