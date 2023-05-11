<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPurchaseOrderReport
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
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkLocSelect = New common.Controls.MyRadioButton
        Me.chkLocAll = New common.Controls.MyRadioButton
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgVendor = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkVendor_select = New common.Controls.MyRadioButton
        Me.chkVendor_all = New common.Controls.MyRadioButton
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgDocument = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkDoc_select = New common.Controls.MyRadioButton
        Me.chkdocAll = New common.Controls.MyRadioButton
        Me.dtpTodate = New common.Controls.MyDateTimePicker
        Me.dtpfromdate = New common.Controls.MyDateTimePicker
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.RadLabel4 = New common.Controls.MyLabel
        Me.btnprintSplit = New Telerik.WinControls.UI.RadSplitButton
        Me.btnPrint1 = New Telerik.WinControls.UI.RadMenuItem
        Me.btnPrePrint = New Telerik.WinControls.UI.RadMenuItem
        Me.btnPrintAmendment = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkVendor_select, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendor_all, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkDoc_select, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkdocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpTodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprintSplit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrintAmendment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox5.Controls.Add(Me.RadGroupBox1)
        Me.RadGroupBox5.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox5.Controls.Add(Me.RadGroupBox6)
        Me.RadGroupBox5.Controls.Add(Me.dtpTodate)
        Me.RadGroupBox5.Controls.Add(Me.dtpfromdate)
        Me.RadGroupBox5.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox5.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox5.HeaderText = ""
        Me.RadGroupBox5.Location = New System.Drawing.Point(12, 12)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(441, 581)
        Me.RadGroupBox5.TabIndex = 7
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.HeaderText = "Location"
        Me.RadGroupBox1.Location = New System.Drawing.Point(8, 393)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(425, 173)
        Me.RadGroupBox1.TabIndex = 10
        Me.RadGroupBox1.Text = "Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.AccessibleName = ""
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 40)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(405, 123)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkLocSelect)
        Me.Panel1.Controls.Add(Me.chkLocAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(405, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkLocSelect
        '
        Me.chkLocSelect.Location = New System.Drawing.Point(192, 1)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 1
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.Location = New System.Drawing.Point(141, 1)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 0
        Me.chkLocAll.Text = "All"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgVendor)
        Me.RadGroupBox4.Controls.Add(Me.Panel3)
        Me.RadGroupBox4.HeaderText = "Vendor"
        Me.RadGroupBox4.Location = New System.Drawing.Point(8, 214)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(425, 173)
        Me.RadGroupBox4.TabIndex = 4
        Me.RadGroupBox4.Text = "Vendor"
        '
        'cbgVendor
        '
        Me.cbgVendor.AccessibleName = ""
        Me.cbgVendor.CheckedValue = Nothing
        Me.cbgVendor.DataSource = Nothing
        Me.cbgVendor.DisplayMember = "Name"
        Me.cbgVendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVendor.Location = New System.Drawing.Point(10, 40)
        Me.cbgVendor.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVendor.MyShowHeadrText = False
        Me.cbgVendor.Name = "cbgVendor"
        Me.cbgVendor.Size = New System.Drawing.Size(405, 123)
        Me.cbgVendor.TabIndex = 1
        Me.cbgVendor.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkVendor_select)
        Me.Panel3.Controls.Add(Me.chkVendor_all)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(405, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkVendor_select
        '
        Me.chkVendor_select.Location = New System.Drawing.Point(192, 1)
        Me.chkVendor_select.MyLinkLable1 = Nothing
        Me.chkVendor_select.MyLinkLable2 = Nothing
        Me.chkVendor_select.Name = "chkVendor_select"
        Me.chkVendor_select.Size = New System.Drawing.Size(50, 18)
        Me.chkVendor_select.TabIndex = 1
        Me.chkVendor_select.Text = "Select"
        '
        'chkVendor_all
        '
        Me.chkVendor_all.Location = New System.Drawing.Point(141, 1)
        Me.chkVendor_all.MyLinkLable1 = Nothing
        Me.chkVendor_all.MyLinkLable2 = Nothing
        Me.chkVendor_all.Name = "chkVendor_all"
        Me.chkVendor_all.Size = New System.Drawing.Size(33, 18)
        Me.chkVendor_all.TabIndex = 0
        Me.chkVendor_all.Text = "All"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.cbgDocument)
        Me.RadGroupBox6.Controls.Add(Me.Panel4)
        Me.RadGroupBox6.HeaderText = "Document No"
        Me.RadGroupBox6.Location = New System.Drawing.Point(8, 35)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(425, 173)
        Me.RadGroupBox6.TabIndex = 3
        Me.RadGroupBox6.Text = "Document No"
        '
        'cbgDocument
        '
        Me.cbgDocument.AccessibleName = ""
        Me.cbgDocument.CheckedValue = Nothing
        Me.cbgDocument.DataSource = Nothing
        Me.cbgDocument.DisplayMember = "Name"
        Me.cbgDocument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgDocument.Location = New System.Drawing.Point(10, 40)
        Me.cbgDocument.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgDocument.MyShowHeadrText = False
        Me.cbgDocument.Name = "cbgDocument"
        Me.cbgDocument.Size = New System.Drawing.Size(405, 123)
        Me.cbgDocument.TabIndex = 1
        Me.cbgDocument.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkDoc_select)
        Me.Panel4.Controls.Add(Me.chkdocAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(405, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkDoc_select
        '
        Me.chkDoc_select.Location = New System.Drawing.Point(192, 1)
        Me.chkDoc_select.MyLinkLable1 = Nothing
        Me.chkDoc_select.MyLinkLable2 = Nothing
        Me.chkDoc_select.Name = "chkDoc_select"
        Me.chkDoc_select.Size = New System.Drawing.Size(50, 18)
        Me.chkDoc_select.TabIndex = 1
        Me.chkDoc_select.Text = "Select"
        '
        'chkdocAll
        '
        Me.chkdocAll.Location = New System.Drawing.Point(141, 1)
        Me.chkdocAll.MyLinkLable1 = Nothing
        Me.chkdocAll.MyLinkLable2 = Nothing
        Me.chkdocAll.Name = "chkdocAll"
        Me.chkdocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkdocAll.TabIndex = 0
        Me.chkdocAll.Text = "All"
        '
        'dtpTodate
        '
        Me.dtpTodate.CustomFormat = "dd-MM-yyyy"
        Me.dtpTodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTodate.Location = New System.Drawing.Point(351, 8)
        Me.dtpTodate.MendatroryField = False
        Me.dtpTodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTodate.MyLinkLable1 = Nothing
        Me.dtpTodate.MyLinkLable2 = Nothing
        Me.dtpTodate.Name = "dtpTodate"
        Me.dtpTodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTodate.Size = New System.Drawing.Size(82, 20)
        Me.dtpTodate.TabIndex = 1
        Me.dtpTodate.TabStop = False
        Me.dtpTodate.Text = "14-09-2011"
        Me.dtpTodate.Value = New Date(2011, 9, 14, 0, 0, 0, 0)
        '
        'dtpfromdate
        '
        Me.dtpfromdate.CustomFormat = "dd-MM-yyyy"
        Me.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfromdate.Location = New System.Drawing.Point(70, 8)
        Me.dtpfromdate.MendatroryField = False
        Me.dtpfromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate.MyLinkLable1 = Nothing
        Me.dtpfromdate.MyLinkLable2 = Nothing
        Me.dtpfromdate.Name = "dtpfromdate"
        Me.dtpfromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate.Size = New System.Drawing.Size(82, 20)
        Me.dtpfromdate.TabIndex = 0
        Me.dtpfromdate.TabStop = False
        Me.dtpfromdate.Text = "14-09-2011"
        Me.dtpfromdate.Value = New Date(2011, 9, 14, 0, 0, 0, 0)
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(303, 9)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel3.TabIndex = 9
        Me.RadLabel3.Text = "To Date"
        '
        'RadLabel4
        '
        Me.RadLabel4.Location = New System.Drawing.Point(7, 9)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel4.TabIndex = 8
        Me.RadLabel4.Text = "From Date"
        '
        'btnprintSplit
        '
        Me.btnprintSplit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprintSplit.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnPrint1, Me.btnPrePrint})
        Me.btnprintSplit.Location = New System.Drawing.Point(158, 6)
        Me.btnprintSplit.Name = "btnprintSplit"
        Me.btnprintSplit.Size = New System.Drawing.Size(80, 18)
        Me.btnprintSplit.TabIndex = 25
        Me.btnprintSplit.Text = "Print"
        '
        'btnPrint1
        '
        Me.btnPrint1.AccessibleDescription = "Print"
        Me.btnPrint1.AccessibleName = "Print"
        Me.btnPrint1.Name = "btnPrint1"
        Me.btnPrint1.Text = "Print"
        Me.btnPrint1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnPrePrint
        '
        Me.btnPrePrint.AccessibleDescription = "Pre Print"
        Me.btnPrePrint.AccessibleName = "Pre Print"
        Me.btnPrePrint.Name = "btnPrePrint"
        Me.btnPrePrint.Text = "Pre Print"
        Me.btnPrePrint.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnPrintAmendment
        '
        Me.btnPrintAmendment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrintAmendment.Location = New System.Drawing.Point(244, 6)
        Me.btnPrintAmendment.Name = "btnPrintAmendment"
        Me.btnPrintAmendment.Size = New System.Drawing.Size(105, 18)
        Me.btnPrintAmendment.TabIndex = 6
        Me.btnPrintAmendment.Text = "Print Amendment"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(385, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 7
        Me.btnclose.Text = "Close"
        '
        'btnreset
        '
        Me.btnreset.Location = New System.Drawing.Point(11, 6)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 18)
        Me.btnreset.TabIndex = 6
        Me.btnreset.Text = "&Reset"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(85, 6)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(68, 18)
        Me.btnprint.TabIndex = 5
        Me.btnprint.Text = "Print"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox5)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprintSplit)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrintAmendment)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(464, 634)
        Me.SplitContainer1.SplitterDistance = 598
        Me.SplitContainer1.TabIndex = 8
        '
        'FrmPurchaseOrderReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(464, 634)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmPurchaseOrderReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Purchase Order Report"
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkVendor_select, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendor_all, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkDoc_select, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkdocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpTodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprintSplit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrintAmendment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVendor As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkVendor_select As common.Controls.MyRadioButton
    Friend WithEvents chkVendor_all As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgDocument As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkDoc_select As common.Controls.MyRadioButton
    Friend WithEvents chkdocAll As common.Controls.MyRadioButton
    Friend WithEvents dtpTodate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpfromdate As common.Controls.MyDateTimePicker
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrintAmendment As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents btnprintSplit As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnPrint1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPrePrint As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

