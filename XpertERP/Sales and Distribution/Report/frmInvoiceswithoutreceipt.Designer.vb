<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmInvoiceswithoutreceipt
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.ChkGroup = New System.Windows.Forms.CheckBox
        Me.grpVehicle = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgVehicle = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkVehicleSelect = New common.Controls.MyRadioButton
        Me.ChkVehicleAll = New common.Controls.MyRadioButton
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkCustomerSelect = New common.Controls.MyRadioButton
        Me.chkCustomerAll = New common.Controls.MyRadioButton
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkReceiptSelect = New common.Controls.MyRadioButton
        Me.chkReceiptAll = New common.Controls.MyRadioButton
        Me.cbgReceipt = New common.MyCheckBoxGrid
        Me.dtpto = New common.Controls.MyDateTimePicker
        Me.RadLabel5 = New common.Controls.MyLabel
        Me.dtpfrom = New common.Controls.MyDateTimePicker
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadButton3 = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.RadSplitButton2 = New Telerik.WinControls.UI.RadSplitButton
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.grpVehicle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpVehicle.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkVehicleSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkVehicleAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustomerAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkReceiptSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkReceiptAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpfrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(964, 580)
        Me.RadPageView1.TabIndex = 113
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(943, 532)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.ChkGroup)
        Me.RadGroupBox1.Controls.Add(Me.grpVehicle)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.dtpto)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel5)
        Me.RadGroupBox1.Controls.Add(Me.dtpfrom)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.HeaderText = ">>>"
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(943, 518)
        Me.RadGroupBox1.TabIndex = 11
        Me.RadGroupBox1.Text = ">>>"
        '
        'ChkGroup
        '
        Me.ChkGroup.AutoSize = True
        Me.ChkGroup.Location = New System.Drawing.Point(371, 15)
        Me.ChkGroup.Name = "ChkGroup"
        Me.ChkGroup.Size = New System.Drawing.Size(133, 17)
        Me.ChkGroup.TabIndex = 338
        Me.ChkGroup.Text = "Group by Receipt No"
        Me.ChkGroup.UseVisualStyleBackColor = True
        Me.ChkGroup.Visible = False
        '
        'grpVehicle
        '
        Me.grpVehicle.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpVehicle.Controls.Add(Me.cbgVehicle)
        Me.grpVehicle.Controls.Add(Me.Panel3)
        Me.grpVehicle.HeaderText = "Vehicle"
        Me.grpVehicle.Location = New System.Drawing.Point(13, 273)
        Me.grpVehicle.Name = "grpVehicle"
        Me.grpVehicle.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpVehicle.Size = New System.Drawing.Size(450, 232)
        Me.grpVehicle.TabIndex = 31
        Me.grpVehicle.Text = "Vehicle"
        '
        'cbgVehicle
        '
        Me.cbgVehicle.CheckedValue = Nothing
        Me.cbgVehicle.DataSource = Nothing
        Me.cbgVehicle.DisplayMember = "Name"
        Me.cbgVehicle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVehicle.Location = New System.Drawing.Point(10, 40)
        Me.cbgVehicle.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVehicle.MyShowHeadrText = False
        Me.cbgVehicle.Name = "cbgVehicle"
        Me.cbgVehicle.Size = New System.Drawing.Size(430, 182)
        Me.cbgVehicle.TabIndex = 1
        Me.cbgVehicle.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkVehicleSelect)
        Me.Panel3.Controls.Add(Me.ChkVehicleAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(430, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkVehicleSelect
        '
        Me.chkVehicleSelect.Location = New System.Drawing.Point(192, 1)
        Me.chkVehicleSelect.MyLinkLable1 = Nothing
        Me.chkVehicleSelect.MyLinkLable2 = Nothing
        Me.chkVehicleSelect.Name = "chkVehicleSelect"
        Me.chkVehicleSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkVehicleSelect.TabIndex = 1
        Me.chkVehicleSelect.Text = "Select"
        '
        'ChkVehicleAll
        '
        Me.ChkVehicleAll.Location = New System.Drawing.Point(141, 1)
        Me.ChkVehicleAll.MyLinkLable1 = Nothing
        Me.ChkVehicleAll.MyLinkLable2 = Nothing
        Me.ChkVehicleAll.Name = "ChkVehicleAll"
        Me.ChkVehicleAll.Size = New System.Drawing.Size(33, 18)
        Me.ChkVehicleAll.TabIndex = 0
        Me.ChkVehicleAll.Text = "All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.Panel1)
        Me.RadGroupBox3.Controls.Add(Me.cbgCustomer)
        Me.RadGroupBox3.HeaderText = "Select Customer"
        Me.RadGroupBox3.Location = New System.Drawing.Point(478, 47)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(450, 223)
        Me.RadGroupBox3.TabIndex = 34
        Me.RadGroupBox3.Text = "Select Customer"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkCustomerSelect)
        Me.Panel1.Controls.Add(Me.chkCustomerAll)
        Me.Panel1.Location = New System.Drawing.Point(9, 14)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(436, 20)
        Me.Panel1.TabIndex = 30
        '
        'chkCustomerSelect
        '
        Me.chkCustomerSelect.Location = New System.Drawing.Point(226, 1)
        Me.chkCustomerSelect.MyLinkLable1 = Nothing
        Me.chkCustomerSelect.MyLinkLable2 = Nothing
        Me.chkCustomerSelect.Name = "chkCustomerSelect"
        Me.chkCustomerSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCustomerSelect.TabIndex = 1
        Me.chkCustomerSelect.Text = "Select"
        '
        'chkCustomerAll
        '
        Me.chkCustomerAll.Location = New System.Drawing.Point(175, 1)
        Me.chkCustomerAll.MyLinkLable1 = Nothing
        Me.chkCustomerAll.MyLinkLable2 = Nothing
        Me.chkCustomerAll.Name = "chkCustomerAll"
        Me.chkCustomerAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCustomerAll.TabIndex = 0
        Me.chkCustomerAll.Text = "All"
        '
        'cbgCustomer
        '
        Me.cbgCustomer.CheckedValue = Nothing
        Me.cbgCustomer.DataSource = Nothing
        Me.cbgCustomer.DisplayMember = "Name"
        Me.cbgCustomer.Location = New System.Drawing.Point(7, 35)
        Me.cbgCustomer.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustomer.MyShowHeadrText = False
        Me.cbgCustomer.Name = "cbgCustomer"
        Me.cbgCustomer.Size = New System.Drawing.Size(436, 175)
        Me.cbgCustomer.TabIndex = 29
        Me.cbgCustomer.ValueMember = "Code"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.Panel2)
        Me.RadGroupBox2.Controls.Add(Me.cbgReceipt)
        Me.RadGroupBox2.HeaderText = "Select Receipt"
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 37)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(450, 233)
        Me.RadGroupBox2.TabIndex = 33
        Me.RadGroupBox2.Text = "Select Receipt"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkReceiptSelect)
        Me.Panel2.Controls.Add(Me.chkReceiptAll)
        Me.Panel2.Location = New System.Drawing.Point(7, 19)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(436, 20)
        Me.Panel2.TabIndex = 30
        '
        'chkReceiptSelect
        '
        Me.chkReceiptSelect.Location = New System.Drawing.Point(226, 1)
        Me.chkReceiptSelect.MyLinkLable1 = Nothing
        Me.chkReceiptSelect.MyLinkLable2 = Nothing
        Me.chkReceiptSelect.Name = "chkReceiptSelect"
        Me.chkReceiptSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkReceiptSelect.TabIndex = 1
        Me.chkReceiptSelect.Text = "Select"
        '
        'chkReceiptAll
        '
        Me.chkReceiptAll.Location = New System.Drawing.Point(175, 1)
        Me.chkReceiptAll.MyLinkLable1 = Nothing
        Me.chkReceiptAll.MyLinkLable2 = Nothing
        Me.chkReceiptAll.Name = "chkReceiptAll"
        Me.chkReceiptAll.Size = New System.Drawing.Size(33, 18)
        Me.chkReceiptAll.TabIndex = 0
        Me.chkReceiptAll.Text = "All"
        '
        'cbgReceipt
        '
        Me.cbgReceipt.CheckedValue = Nothing
        Me.cbgReceipt.DataSource = Nothing
        Me.cbgReceipt.DisplayMember = "Name"
        Me.cbgReceipt.Location = New System.Drawing.Point(8, 41)
        Me.cbgReceipt.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgReceipt.MyShowHeadrText = False
        Me.cbgReceipt.Name = "cbgReceipt"
        Me.cbgReceipt.Size = New System.Drawing.Size(436, 179)
        Me.cbgReceipt.TabIndex = 29
        Me.cbgReceipt.ValueMember = "Code"
        '
        'dtpto
        '
        Me.dtpto.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpto.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpto.Location = New System.Drawing.Point(258, 14)
        Me.dtpto.MendatroryField = False
        Me.dtpto.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpto.MyLinkLable1 = Nothing
        Me.dtpto.MyLinkLable2 = Nothing
        Me.dtpto.Name = "dtpto"
        Me.dtpto.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpto.Size = New System.Drawing.Size(97, 18)
        Me.dtpto.TabIndex = 9
        Me.dtpto.TabStop = False
        Me.dtpto.Text = "06/07/2011 02:43 PM"
        Me.dtpto.Value = New Date(2011, 7, 6, 14, 43, 33, 828)
        '
        'RadLabel5
        '
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(189, 12)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(46, 16)
        Me.RadLabel5.TabIndex = 4
        Me.RadLabel5.Text = "To Date"
        '
        'dtpfrom
        '
        Me.dtpfrom.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpfrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpfrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfrom.Location = New System.Drawing.Point(82, 12)
        Me.dtpfrom.MendatroryField = False
        Me.dtpfrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfrom.MyLinkLable1 = Nothing
        Me.dtpfrom.MyLinkLable2 = Nothing
        Me.dtpfrom.Name = "dtpfrom"
        Me.dtpfrom.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfrom.Size = New System.Drawing.Size(96, 18)
        Me.dtpfrom.TabIndex = 9
        Me.dtpfrom.TabStop = False
        Me.dtpfrom.Text = "06/07/2011 02:43 PM"
        Me.dtpfrom.Value = New Date(2011, 7, 6, 14, 43, 19, 796)
        '
        'RadLabel3
        '
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(13, 12)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel3.TabIndex = 2
        Me.RadLabel3.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1022, 487)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.Size = New System.Drawing.Size(1022, 487)
        Me.gv1.TabIndex = 3
        Me.gv1.Text = "RadGridView1"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Size = New System.Drawing.Size(964, 621)
        Me.SplitContainer1.SplitterDistance = 580
        Me.SplitContainer1.TabIndex = 114
        '
        'RadButton3
        '
        Me.RadButton3.Location = New System.Drawing.Point(76, 8)
        Me.RadButton3.Name = "RadButton3"
        Me.RadButton3.Size = New System.Drawing.Size(68, 18)
        Me.RadButton3.TabIndex = 324
        Me.RadButton3.Text = "&Reset"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(889, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(64, 18)
        Me.btnclose.TabIndex = 322
        Me.btnclose.Text = "Close"
        '
        'RadSplitButton2
        '
        Me.RadSplitButton2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.RadSplitButton2.Location = New System.Drawing.Point(150, 8)
        Me.RadSplitButton2.Name = "RadSplitButton2"
        Me.RadSplitButton2.Size = New System.Drawing.Size(88, 18)
        Me.RadSplitButton2.TabIndex = 323
        Me.RadSplitButton2.Text = "Export"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Excel"
        Me.RadMenuItem1.AccessibleName = "Excel"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Excel"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "PDF"
        Me.RadMenuItem2.AccessibleName = "PDF"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "PDF"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprint.Location = New System.Drawing.Point(16, 8)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(54, 18)
        Me.btnprint.TabIndex = 321
        Me.btnprint.Text = ">>>"
        '
        'FrmInvoiceswithoutreceipt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(964, 621)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmInvoiceswithoutreceipt"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "INVOICES WITHOUT RECEIPT"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.grpVehicle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpVehicle.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkVehicleSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkVehicleAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustomerAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkReceiptSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkReceiptAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpfrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents ChkGroup As System.Windows.Forms.CheckBox
    Friend WithEvents grpVehicle As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVehicle As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkVehicleSelect As common.Controls.MyRadioButton
    Friend WithEvents ChkVehicleAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkCustomerSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCustomerAll As common.Controls.MyRadioButton
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkReceiptSelect As common.Controls.MyRadioButton
    Friend WithEvents chkReceiptAll As common.Controls.MyRadioButton
    Friend WithEvents cbgReceipt As common.MyCheckBoxGrid
    Friend WithEvents dtpto As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents dtpfrom As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadButton3 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadSplitButton2 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
End Class

