<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerWiseMonthlySalesAnalysis
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
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.gvDB = New common.UserControls.MyRadGridView
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.rdbAmtAfterDisc = New common.Controls.MyRadioButton
        Me.rdbNetAmount = New common.Controls.MyRadioButton
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkCustSelect = New common.Controls.MyRadioButton
        Me.chkCustAll = New common.Controls.MyRadioButton
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgVendor = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkVendorSelect = New common.Controls.MyRadioButton
        Me.chkVendorAll = New common.Controls.MyRadioButton
        Me.txtYear = New common.Controls.MyDateTimePicker
        Me.txtMonth = New common.Controls.MyDateTimePicker
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton
        Me.Export = New Telerik.WinControls.UI.RadMenuItem
        Me.PDF = New Telerik.WinControls.UI.RadMenuItem
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.rdbAmtAfterDisc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbNetAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkCustSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkVendorSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendorAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(655, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(13, 6)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 5
        Me.btnReset.Text = "Reset"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(87, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 4
        Me.btnPrint.Text = "Print"
        '
        'gvDB
        '
        Me.gvDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDB.Location = New System.Drawing.Point(10, 46)
        '
        '
        '
        Me.gvDB.MasterTemplate.AllowAddNewRow = False
        Me.gvDB.Name = "gvDB"
        Me.gvDB.ShowGroupPanel = False
        Me.gvDB.Size = New System.Drawing.Size(471, 100)
        Me.gvDB.TabIndex = 0
        Me.gvDB.Text = "RadGridView1"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(740, 540)
        Me.SplitContainer1.SplitterDistance = 501
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(740, 501)
        Me.RadPageView1.TabIndex = 25
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(719, 453)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox5)
        Me.RadGroupBox1.Controls.Add(Me.txtYear)
        Me.RadGroupBox1.Controls.Add(Me.txtMonth)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(671, 447)
        Me.RadGroupBox1.TabIndex = 2
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.rdbAmtAfterDisc)
        Me.RadGroupBox4.Controls.Add(Me.rdbNetAmount)
        Me.RadGroupBox4.HeaderText = ""
        Me.RadGroupBox4.Location = New System.Drawing.Point(346, 7)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Size = New System.Drawing.Size(282, 26)
        Me.RadGroupBox4.TabIndex = 2
        '
        'rdbAmtAfterDisc
        '
        Me.rdbAmtAfterDisc.Location = New System.Drawing.Point(8, 2)
        Me.rdbAmtAfterDisc.MyLinkLable1 = Nothing
        Me.rdbAmtAfterDisc.MyLinkLable2 = Nothing
        Me.rdbAmtAfterDisc.Name = "rdbAmtAfterDisc"
        '
        '
        '
        Me.rdbAmtAfterDisc.RootElement.StretchHorizontally = True
        Me.rdbAmtAfterDisc.RootElement.StretchVertically = True
        Me.rdbAmtAfterDisc.Size = New System.Drawing.Size(135, 18)
        Me.rdbAmtAfterDisc.TabIndex = 313
        Me.rdbAmtAfterDisc.TabStop = True
        Me.rdbAmtAfterDisc.Text = "Amount After Discount"
        Me.rdbAmtAfterDisc.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rdbNetAmount
        '
        Me.rdbNetAmount.Location = New System.Drawing.Point(152, 2)
        Me.rdbNetAmount.MyLinkLable1 = Nothing
        Me.rdbNetAmount.MyLinkLable2 = Nothing
        Me.rdbNetAmount.Name = "rdbNetAmount"
        '
        '
        '
        Me.rdbNetAmount.RootElement.StretchHorizontally = True
        Me.rdbNetAmount.RootElement.StretchVertically = True
        Me.rdbNetAmount.Size = New System.Drawing.Size(125, 18)
        Me.rdbNetAmount.TabIndex = 314
        Me.rdbNetAmount.Text = "Net Amount"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgCustomer)
        Me.RadGroupBox3.Controls.Add(Me.Panel4)
        Me.RadGroupBox3.HeaderText = "Customer"
        Me.RadGroupBox3.Location = New System.Drawing.Point(336, 39)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(327, 169)
        Me.RadGroupBox3.TabIndex = 312
        Me.RadGroupBox3.Text = "Customer"
        '
        'cbgCustomer
        '
        Me.cbgCustomer.CheckedValue = Nothing
        Me.cbgCustomer.DataSource = Nothing
        Me.cbgCustomer.DisplayMember = "Name"
        Me.cbgCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCustomer.Location = New System.Drawing.Point(10, 40)
        Me.cbgCustomer.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustomer.MyShowHeadrText = False
        Me.cbgCustomer.Name = "cbgCustomer"
        Me.cbgCustomer.Size = New System.Drawing.Size(307, 119)
        Me.cbgCustomer.TabIndex = 1
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkCustSelect)
        Me.Panel4.Controls.Add(Me.chkCustAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(307, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkCustSelect
        '
        Me.chkCustSelect.Location = New System.Drawing.Point(161, 1)
        Me.chkCustSelect.MyLinkLable1 = Nothing
        Me.chkCustSelect.MyLinkLable2 = Nothing
        Me.chkCustSelect.Name = "chkCustSelect"
        Me.chkCustSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCustSelect.TabIndex = 1
        Me.chkCustSelect.Text = "Select"
        '
        'chkCustAll
        '
        Me.chkCustAll.Location = New System.Drawing.Point(110, 1)
        Me.chkCustAll.MyLinkLable1 = Nothing
        Me.chkCustAll.MyLinkLable2 = Nothing
        Me.chkCustAll.Name = "chkCustAll"
        Me.chkCustAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCustAll.TabIndex = 0
        Me.chkCustAll.TabStop = True
        Me.chkCustAll.Text = "All"
        Me.chkCustAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgVendor)
        Me.RadGroupBox5.Controls.Add(Me.Panel3)
        Me.RadGroupBox5.HeaderText = "Vendor"
        Me.RadGroupBox5.Location = New System.Drawing.Point(5, 39)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(325, 169)
        Me.RadGroupBox5.TabIndex = 309
        Me.RadGroupBox5.Text = "Vendor"
        '
        'cbgVendor
        '
        Me.cbgVendor.CheckedValue = Nothing
        Me.cbgVendor.DataSource = Nothing
        Me.cbgVendor.DisplayMember = "Name"
        Me.cbgVendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVendor.Location = New System.Drawing.Point(10, 40)
        Me.cbgVendor.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVendor.MyShowHeadrText = False
        Me.cbgVendor.Name = "cbgVendor"
        Me.cbgVendor.Size = New System.Drawing.Size(305, 119)
        Me.cbgVendor.TabIndex = 2
        Me.cbgVendor.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkVendorSelect)
        Me.Panel3.Controls.Add(Me.chkVendorAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(305, 20)
        Me.Panel3.TabIndex = 1
        '
        'chkVendorSelect
        '
        Me.chkVendorSelect.Location = New System.Drawing.Point(150, 1)
        Me.chkVendorSelect.MyLinkLable1 = Nothing
        Me.chkVendorSelect.MyLinkLable2 = Nothing
        Me.chkVendorSelect.Name = "chkVendorSelect"
        Me.chkVendorSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkVendorSelect.TabIndex = 2
        Me.chkVendorSelect.Text = "Select"
        '
        'chkVendorAll
        '
        Me.chkVendorAll.Location = New System.Drawing.Point(101, 1)
        Me.chkVendorAll.MyLinkLable1 = Nothing
        Me.chkVendorAll.MyLinkLable2 = Nothing
        Me.chkVendorAll.Name = "chkVendorAll"
        Me.chkVendorAll.Size = New System.Drawing.Size(33, 18)
        Me.chkVendorAll.TabIndex = 1
        Me.chkVendorAll.Text = "All"
        '
        'txtYear
        '
        Me.txtYear.CustomFormat = "yyyy"
        Me.txtYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtYear.Location = New System.Drawing.Point(216, 7)
        Me.txtYear.MendatroryField = False
        Me.txtYear.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtYear.MyLinkLable1 = Nothing
        Me.txtYear.MyLinkLable2 = Nothing
        Me.txtYear.Name = "txtYear"
        Me.txtYear.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtYear.Size = New System.Drawing.Size(82, 20)
        Me.txtYear.TabIndex = 1
        Me.txtYear.TabStop = False
        Me.txtYear.Text = "2011"
        Me.txtYear.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtMonth
        '
        Me.txtMonth.CustomFormat = "MMM"
        Me.txtMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtMonth.Location = New System.Drawing.Point(70, 7)
        Me.txtMonth.MendatroryField = False
        Me.txtMonth.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMonth.MyLinkLable1 = Nothing
        Me.txtMonth.MyLinkLable2 = Nothing
        Me.txtMonth.Name = "txtMonth"
        Me.txtMonth.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMonth.Size = New System.Drawing.Size(82, 20)
        Me.txtMonth.TabIndex = 0
        Me.txtMonth.TabStop = False
        Me.txtMonth.Text = "Jun"
        Me.txtMonth.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(165, 9)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(28, 18)
        Me.RadLabel2.TabIndex = 9
        Me.RadLabel2.Text = "Year"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(5, 9)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(40, 18)
        Me.RadLabel1.TabIndex = 10
        Me.RadLabel1.Text = "Month"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(719, 453)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(719, 453)
        Me.gv1.TabIndex = 3
        Me.gv1.Text = "RadGridView1"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Export, Me.PDF})
        Me.btnExport.Location = New System.Drawing.Point(161, 6)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(80, 18)
        Me.btnExport.TabIndex = 127
        Me.btnExport.Text = "Export"
        '
        'Export
        '
        Me.Export.AccessibleDescription = "Export"
        Me.Export.AccessibleName = "Export"
        Me.Export.Image = Global.ERP.My.Resources.Resources.MSE
        Me.Export.Name = "Export"
        Me.Export.Text = "Export"
        Me.Export.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'PDF
        '
        Me.PDF.AccessibleDescription = "PDF"
        Me.PDF.AccessibleName = "PDF"
        Me.PDF.Image = Global.ERP.My.Resources.Resources.pdf
        Me.PDF.Name = "PDF"
        Me.PDF.Text = "PDF"
        Me.PDF.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'frmCustomerWiseMonthlySalesAnalysis
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(740, 540)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmCustomerWiseMonthlySalesAnalysis"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Form of Guarntee"
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.rdbAmtAfterDisc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbNetAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkCustSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkVendorSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendorAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents gvDB As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadRadioButton1 As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkCustSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCustAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVendor As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkVendorSelect As common.Controls.MyRadioButton
    Friend WithEvents chkVendorAll As common.Controls.MyRadioButton
    Friend WithEvents txtYear As common.Controls.MyDateTimePicker
    Friend WithEvents txtMonth As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbAmtAfterDisc As common.Controls.MyRadioButton
    Friend WithEvents rdbNetAmount As common.Controls.MyRadioButton
End Class

