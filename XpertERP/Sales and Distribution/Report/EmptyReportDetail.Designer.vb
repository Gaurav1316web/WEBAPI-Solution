<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EmptyReportDetail
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rbtnSaleInvoice = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnTransferLoadout = New Telerik.WinControls.UI.RadRadioButton
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.ddltype = New common.Controls.MyComboBox
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.dtpToDate = New common.Controls.MyDateTimePicker
        Me.dtpFdate = New common.Controls.MyDateTimePicker
        Me.Locationgb = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkLocationSelect = New common.Controls.MyRadioButton
        Me.chkLocationAll = New common.Controls.MyRadioButton
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv = New common.UserControls.MyRadGridView
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem
        Me.btnrefresh = New Telerik.WinControls.UI.RadButton
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnSaleInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnTransferLoadout, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddltype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Locationgb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Locationgb.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnrefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnrefresh)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(544, 338)
        Me.SplitContainer1.SplitterDistance = 308
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(544, 308)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        Me.RadPageView1.ThemeName = "ControlDefault"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.Locationgb)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(523, 260)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GroupBox1)
        Me.GroupBox2.Controls.Add(Me.MyLabel1)
        Me.GroupBox2.Controls.Add(Me.ddltype)
        Me.GroupBox2.Controls.Add(Me.RadLabel2)
        Me.GroupBox2.Controls.Add(Me.RadLabel1)
        Me.GroupBox2.Controls.Add(Me.btnReset)
        Me.GroupBox2.Controls.Add(Me.dtpToDate)
        Me.GroupBox2.Controls.Add(Me.dtpFdate)
        Me.GroupBox2.Location = New System.Drawing.Point(4, -2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(498, 62)
        Me.GroupBox2.TabIndex = 114
        Me.GroupBox2.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnSaleInvoice)
        Me.GroupBox1.Controls.Add(Me.rbtnTransferLoadout)
        Me.GroupBox1.Location = New System.Drawing.Point(363, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(129, 49)
        Me.GroupBox1.TabIndex = 111
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Against"
        '
        'rbtnSaleInvoice
        '
        Me.rbtnSaleInvoice.Location = New System.Drawing.Point(7, 29)
        Me.rbtnSaleInvoice.Name = "rbtnSaleInvoice"
        Me.rbtnSaleInvoice.Size = New System.Drawing.Size(79, 18)
        Me.rbtnSaleInvoice.TabIndex = 1
        Me.rbtnSaleInvoice.Text = "Sale Invoice"
        '
        'rbtnTransferLoadout
        '
        Me.rbtnTransferLoadout.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnTransferLoadout.Location = New System.Drawing.Point(7, 13)
        Me.rbtnTransferLoadout.Name = "rbtnTransferLoadout"
        Me.rbtnTransferLoadout.Size = New System.Drawing.Size(112, 18)
        Me.rbtnTransferLoadout.TabIndex = 0
        Me.rbtnTransferLoadout.TabStop = True
        Me.rbtnTransferLoadout.Text = "Transfer / Loadout"
        Me.rbtnTransferLoadout.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(4, 34)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel1.TabIndex = 102
        Me.MyLabel1.Text = "Type"
        '
        'ddltype
        '
        Me.ddltype.AllowShowFocusCues = False
        Me.ddltype.AutoCompleteDisplayMember = Nothing
        Me.ddltype.AutoCompleteValueMember = Nothing
        Me.ddltype.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Text = "Out"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "In"
        RadListDataItem2.TextWrap = True
        Me.ddltype.Items.Add(RadListDataItem1)
        Me.ddltype.Items.Add(RadListDataItem2)
        Me.ddltype.Location = New System.Drawing.Point(69, 33)
        Me.ddltype.MendatroryField = False
        Me.ddltype.MyLinkLable1 = Nothing
        Me.ddltype.MyLinkLable2 = Nothing
        Me.ddltype.Name = "ddltype"
        Me.ddltype.Size = New System.Drawing.Size(85, 20)
        Me.ddltype.TabIndex = 110
        Me.ddltype.Text = "Out"
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(158, 10)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 101
        Me.RadLabel2.Text = "To Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(4, 10)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 101
        Me.RadLabel1.Text = "From Date"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnReset.Location = New System.Drawing.Point(300, 10)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(17, 19)
        Me.btnReset.TabIndex = 41
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(209, 10)
        Me.dtpToDate.MendatroryField = False
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Nothing
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(86, 18)
        Me.dtpToDate.TabIndex = 99
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "23/07/2012 12:00 AM"
        Me.dtpToDate.Value = New Date(2012, 7, 23, 0, 0, 0, 0)
        '
        'dtpFdate
        '
        Me.dtpFdate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpFdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFdate.Location = New System.Drawing.Point(69, 10)
        Me.dtpFdate.MendatroryField = False
        Me.dtpFdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFdate.MyLinkLable1 = Nothing
        Me.dtpFdate.MyLinkLable2 = Nothing
        Me.dtpFdate.Name = "dtpFdate"
        Me.dtpFdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFdate.Size = New System.Drawing.Size(85, 18)
        Me.dtpFdate.TabIndex = 98
        Me.dtpFdate.TabStop = False
        Me.dtpFdate.Text = "23/07/2012 12:00 AM"
        Me.dtpFdate.Value = New Date(2012, 7, 23, 0, 0, 0, 0)
        '
        'Locationgb
        '
        Me.Locationgb.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Locationgb.Controls.Add(Me.cbgLocation)
        Me.Locationgb.Controls.Add(Me.Panel2)
        Me.Locationgb.HeaderText = "Location"
        Me.Locationgb.Location = New System.Drawing.Point(4, 63)
        Me.Locationgb.Name = "Locationgb"
        Me.Locationgb.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Locationgb.Size = New System.Drawing.Size(498, 194)
        Me.Locationgb.TabIndex = 113
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
        Me.cbgLocation.Size = New System.Drawing.Size(478, 144)
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
        Me.Panel2.Size = New System.Drawing.Size(478, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.Location = New System.Drawing.Point(247, 1)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.Location = New System.Drawing.Point(180, 1)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(523, 260)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv.MasterTemplate.EnableGrouping = False
        Me.gv.Name = "gv"
        Me.gv.ReadOnly = True
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowGroupPanel = False
        Me.gv.Size = New System.Drawing.Size(523, 260)
        Me.gv.TabIndex = 0
        Me.gv.Text = "RadGridView1"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF})
        Me.btnExport.Location = New System.Drawing.Point(173, 4)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(84, 19)
        Me.btnExport.TabIndex = 115
        Me.btnExport.Text = "Export"
        '
        'btnExcel
        '
        Me.btnExcel.AccessibleDescription = "Excel"
        Me.btnExcel.AccessibleName = "Excel"
        Me.btnExcel.Image = Global.ERP.My.Resources.Resources.MSE
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnPDF
        '
        Me.btnPDF.AccessibleDescription = "PDF"
        Me.btnPDF.AccessibleName = "PDF"
        Me.btnPDF.Image = Global.ERP.My.Resources.Resources.pdf
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "PDF"
        Me.btnPDF.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnrefresh
        '
        Me.btnrefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnrefresh.Location = New System.Drawing.Point(14, 3)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(69, 20)
        Me.btnrefresh.TabIndex = 114
        Me.btnrefresh.Text = ">>>"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(89, 3)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(78, 22)
        Me.btnprint.TabIndex = 100
        Me.btnprint.Text = "Print"
        Me.btnprint.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(445, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 20)
        Me.btnClose.TabIndex = 101
        Me.btnClose.Text = "Close"
        '
        'EmptyReportDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(544, 338)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "EmptyReportDetail"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Empty Report Detail"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnSaleInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnTransferLoadout, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddltype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Locationgb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Locationgb.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnrefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnrefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents Locationgb As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnSaleInvoice As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnTransferLoadout As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents ddltype As common.Controls.MyComboBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpFdate As common.Controls.MyDateTimePicker
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPDF As Telerik.WinControls.UI.RadMenuItem
End Class

