<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSchemeReport
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
        Me.chkActive = New common.Controls.MyRadioButton
        Me.chlAll = New common.Controls.MyRadioButton
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.cgvCustCategory = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkcateselect = New common.Controls.MyRadioButton
        Me.chkcustcateall = New common.Controls.MyRadioButton
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cgvschemeitem = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkschemeselect = New common.Controls.MyRadioButton
        Me.chkschemeall = New common.Controls.MyRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cgvmainitem = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkmainselect = New common.Controls.MyRadioButton
        Me.chkMainall = New common.Controls.MyRadioButton
        Me.txtToDate = New common.Controls.MyDateTimePicker
        Me.txtFromDate = New common.Controls.MyDateTimePicker
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chlAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkcateselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkcustcateall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkschemeselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkschemeall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkmainselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMainall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkActive)
        Me.RadGroupBox1.Controls.Add(Me.chlAll)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(14, 13)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(519, 541)
        Me.RadGroupBox1.TabIndex = 0
        '
        'chkActive
        '
        Me.chkActive.Location = New System.Drawing.Point(384, 10)
        Me.chkActive.MyLinkLable1 = Nothing
        Me.chkActive.MyLinkLable2 = Nothing
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(51, 18)
        Me.chkActive.TabIndex = 325
        Me.chkActive.Text = "Active"
        '
        'chlAll
        '
        Me.chlAll.Location = New System.Drawing.Point(336, 10)
        Me.chlAll.MyLinkLable1 = Nothing
        Me.chlAll.MyLinkLable2 = Nothing
        Me.chlAll.Name = "chlAll"
        Me.chlAll.Size = New System.Drawing.Size(33, 18)
        Me.chlAll.TabIndex = 324
        Me.chlAll.Text = "All"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cgvCustCategory)
        Me.RadGroupBox4.Controls.Add(Me.Panel3)
        Me.RadGroupBox4.HeaderText = "Customer Category"
        Me.RadGroupBox4.Location = New System.Drawing.Point(13, 366)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(497, 160)
        Me.RadGroupBox4.TabIndex = 320
        Me.RadGroupBox4.Text = "Customer Category"
        '
        'cgvCustCategory
        '
        Me.cgvCustCategory.CheckedValue = Nothing
        Me.cgvCustCategory.DataSource = Nothing
        Me.cgvCustCategory.DisplayMember = "Name"
        Me.cgvCustCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cgvCustCategory.Location = New System.Drawing.Point(10, 40)
        Me.cgvCustCategory.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cgvCustCategory.MyShowHeadrText = False
        Me.cgvCustCategory.Name = "cgvCustCategory"
        Me.cgvCustCategory.Size = New System.Drawing.Size(477, 110)
        Me.cgvCustCategory.TabIndex = 2
        Me.cgvCustCategory.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkcateselect)
        Me.Panel3.Controls.Add(Me.chkcustcateall)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(477, 20)
        Me.Panel3.TabIndex = 1
        '
        'chkcateselect
        '
        Me.chkcateselect.Location = New System.Drawing.Point(194, 1)
        Me.chkcateselect.MyLinkLable1 = Nothing
        Me.chkcateselect.MyLinkLable2 = Nothing
        Me.chkcateselect.Name = "chkcateselect"
        Me.chkcateselect.Size = New System.Drawing.Size(50, 18)
        Me.chkcateselect.TabIndex = 2
        Me.chkcateselect.Text = "Select"
        '
        'chkcustcateall
        '
        Me.chkcustcateall.Location = New System.Drawing.Point(146, 1)
        Me.chkcustcateall.MyLinkLable1 = Nothing
        Me.chkcustcateall.MyLinkLable2 = Nothing
        Me.chkcustcateall.Name = "chkcustcateall"
        Me.chkcustcateall.Size = New System.Drawing.Size(33, 18)
        Me.chkcustcateall.TabIndex = 1
        Me.chkcustcateall.Text = "All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cgvschemeitem)
        Me.RadGroupBox3.Controls.Add(Me.Panel1)
        Me.RadGroupBox3.HeaderText = "Scheme Item"
        Me.RadGroupBox3.Location = New System.Drawing.Point(13, 200)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(497, 167)
        Me.RadGroupBox3.TabIndex = 319
        Me.RadGroupBox3.Text = "Scheme Item"
        '
        'cgvschemeitem
        '
        Me.cgvschemeitem.CheckedValue = Nothing
        Me.cgvschemeitem.DataSource = Nothing
        Me.cgvschemeitem.DisplayMember = "Name"
        Me.cgvschemeitem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cgvschemeitem.Location = New System.Drawing.Point(10, 40)
        Me.cgvschemeitem.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cgvschemeitem.MyShowHeadrText = False
        Me.cgvschemeitem.Name = "cgvschemeitem"
        Me.cgvschemeitem.Size = New System.Drawing.Size(477, 117)
        Me.cgvschemeitem.TabIndex = 2
        Me.cgvschemeitem.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkschemeselect)
        Me.Panel1.Controls.Add(Me.chkschemeall)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(477, 20)
        Me.Panel1.TabIndex = 1
        '
        'chkschemeselect
        '
        Me.chkschemeselect.Location = New System.Drawing.Point(192, 1)
        Me.chkschemeselect.MyLinkLable1 = Nothing
        Me.chkschemeselect.MyLinkLable2 = Nothing
        Me.chkschemeselect.Name = "chkschemeselect"
        Me.chkschemeselect.Size = New System.Drawing.Size(50, 18)
        Me.chkschemeselect.TabIndex = 2
        Me.chkschemeselect.Text = "Select"
        '
        'chkschemeall
        '
        Me.chkschemeall.Location = New System.Drawing.Point(144, 1)
        Me.chkschemeall.MyLinkLable1 = Nothing
        Me.chkschemeall.MyLinkLable2 = Nothing
        Me.chkschemeall.Name = "chkschemeall"
        Me.chkschemeall.Size = New System.Drawing.Size(33, 18)
        Me.chkschemeall.TabIndex = 1
        Me.chkschemeall.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cgvmainitem)
        Me.RadGroupBox2.Controls.Add(Me.Panel2)
        Me.RadGroupBox2.HeaderText = "Main Item"
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 31)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(497, 165)
        Me.RadGroupBox2.TabIndex = 318
        Me.RadGroupBox2.Text = "Main Item"
        '
        'cgvmainitem
        '
        Me.cgvmainitem.CheckedValue = Nothing
        Me.cgvmainitem.DataSource = Nothing
        Me.cgvmainitem.DisplayMember = "Name"
        Me.cgvmainitem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cgvmainitem.Location = New System.Drawing.Point(10, 40)
        Me.cgvmainitem.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cgvmainitem.MyShowHeadrText = False
        Me.cgvmainitem.Name = "cgvmainitem"
        Me.cgvmainitem.Size = New System.Drawing.Size(477, 115)
        Me.cgvmainitem.TabIndex = 2
        Me.cgvmainitem.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkmainselect)
        Me.Panel2.Controls.Add(Me.chkMainall)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(477, 20)
        Me.Panel2.TabIndex = 1
        '
        'chkmainselect
        '
        Me.chkmainselect.Location = New System.Drawing.Point(192, 1)
        Me.chkmainselect.MyLinkLable1 = Nothing
        Me.chkmainselect.MyLinkLable2 = Nothing
        Me.chkmainselect.Name = "chkmainselect"
        Me.chkmainselect.Size = New System.Drawing.Size(50, 18)
        Me.chkmainselect.TabIndex = 2
        Me.chkmainselect.Text = "Select"
        '
        'chkMainall
        '
        Me.chkMainall.Location = New System.Drawing.Point(144, 1)
        Me.chkMainall.MyLinkLable1 = Nothing
        Me.chkMainall.MyLinkLable2 = Nothing
        Me.chkMainall.Name = "chkMainall"
        Me.chkMainall.Size = New System.Drawing.Size(33, 18)
        Me.chkMainall.TabIndex = 1
        Me.chkMainall.Text = "All"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(230, 8)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate.TabIndex = 315
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13-06-2011"
        Me.txtToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(82, 8)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 314
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13-06-2011"
        Me.txtFromDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(174, 8)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 316
        Me.RadLabel2.Text = "To Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(13, 8)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 317
        Me.RadLabel1.Text = "From Date"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(465, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 321
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(14, 6)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 323
        Me.btnReset.Text = "Reset"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(88, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 322
        Me.btnPrint.Text = "Print"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(547, 594)
        Me.SplitContainer1.SplitterDistance = 557
        Me.SplitContainer1.TabIndex = 1
        '
        'FrmSchemeReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(547, 594)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmSchemeReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Scheme Report"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chlAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkcateselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkcustcateall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkschemeselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkschemeall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkmainselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMainall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cgvCustCategory As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkcateselect As common.Controls.MyRadioButton
    Friend WithEvents chkcustcateall As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cgvschemeitem As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkschemeselect As common.Controls.MyRadioButton
    Friend WithEvents chkschemeall As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cgvmainitem As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkmainselect As common.Controls.MyRadioButton
    Friend WithEvents chkMainall As common.Controls.MyRadioButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents chkActive As common.Controls.MyRadioButton
    Friend WithEvents chlAll As common.Controls.MyRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

