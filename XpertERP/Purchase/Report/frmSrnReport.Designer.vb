<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSrnReport
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
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkLocSelect = New common.Controls.MyRadioButton
        Me.chkLocAll = New common.Controls.MyRadioButton
        Me.rdbtnOther = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbtnFinishedGood = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgVendor1 = New common.MyCheckBoxGrid
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.chk_Vendor_Select = New common.Controls.MyRadioButton
        Me.chk_Vendor_All = New common.Controls.MyRadioButton
        Me.RadGroupBox9 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgDoc = New common.MyCheckBoxGrid
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.chk_Doc_Select = New common.Controls.MyRadioButton
        Me.chkall = New common.Controls.MyRadioButton
        Me.dtpToDate1 = New common.Controls.MyDateTimePicker
        Me.RadLabel6 = New Telerik.WinControls.UI.RadLabel
        Me.dtpFromdate1 = New common.Controls.MyDateTimePicker
        Me.RadLabel5 = New Telerik.WinControls.UI.RadLabel
        Me.btnreset1 = New Telerik.WinControls.UI.RadButton
        Me.btnClose1 = New Telerik.WinControls.UI.RadButton
        Me.btnprint1 = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnOther, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnFinishedGood, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.chk_Vendor_Select, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_Vendor_All, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox9.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.chk_Doc_Select, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadGroupBox1.Controls.Add(Me.rdbtnOther)
        Me.RadGroupBox1.Controls.Add(Me.rdbtnFinishedGood)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox8)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox9)
        Me.RadGroupBox1.Controls.Add(Me.dtpToDate1)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel6)
        Me.RadGroupBox1.Controls.Add(Me.dtpFromdate1)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel5)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(14, 13)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(427, 592)
        Me.RadGroupBox1.TabIndex = 0
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox2.Controls.Add(Me.Panel1)
        Me.RadGroupBox2.HeaderText = "Location"
        Me.RadGroupBox2.Location = New System.Drawing.Point(10, 405)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(404, 176)
        Me.RadGroupBox2.TabIndex = 21
        Me.RadGroupBox2.Text = "Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.AccessibleName = "cbgVendor"
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 40)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(384, 126)
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
        Me.Panel1.Size = New System.Drawing.Size(384, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkLocSelect
        '
        Me.chkLocSelect.AccessibleName = "chk_Vendor_Select"
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
        Me.chkLocAll.AccessibleName = "chk_Vendor_All"
        Me.chkLocAll.Location = New System.Drawing.Point(141, 1)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 0
        Me.chkLocAll.Text = "All"
        '
        'rdbtnOther
        '
        Me.rdbtnOther.Location = New System.Drawing.Point(258, 33)
        Me.rdbtnOther.Name = "rdbtnOther"
        Me.rdbtnOther.Size = New System.Drawing.Size(49, 18)
        Me.rdbtnOther.TabIndex = 20
        Me.rdbtnOther.Text = "Other"
        '
        'rdbtnFinishedGood
        '
        Me.rdbtnFinishedGood.Location = New System.Drawing.Point(74, 33)
        Me.rdbtnFinishedGood.Name = "rdbtnFinishedGood"
        Me.rdbtnFinishedGood.Size = New System.Drawing.Size(90, 18)
        Me.rdbtnFinishedGood.TabIndex = 19
        Me.rdbtnFinishedGood.Text = "finished Good"
        '
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Controls.Add(Me.cbgVendor1)
        Me.RadGroupBox8.Controls.Add(Me.Panel5)
        Me.RadGroupBox8.HeaderText = "Vendor"
        Me.RadGroupBox8.Location = New System.Drawing.Point(9, 223)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox8.Size = New System.Drawing.Size(404, 176)
        Me.RadGroupBox8.TabIndex = 15
        Me.RadGroupBox8.Text = "Vendor"
        '
        'cbgVendor1
        '
        Me.cbgVendor1.AccessibleName = "cbgVendor"
        Me.cbgVendor1.CheckedValue = Nothing
        Me.cbgVendor1.DataSource = Nothing
        Me.cbgVendor1.DisplayMember = "Name"
        Me.cbgVendor1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVendor1.Location = New System.Drawing.Point(10, 40)
        Me.cbgVendor1.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVendor1.MyShowHeadrText = False
        Me.cbgVendor1.Name = "cbgVendor1"
        Me.cbgVendor1.Size = New System.Drawing.Size(384, 126)
        Me.cbgVendor1.TabIndex = 1
        Me.cbgVendor1.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chk_Vendor_Select)
        Me.Panel5.Controls.Add(Me.chk_Vendor_All)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(384, 20)
        Me.Panel5.TabIndex = 0
        '
        'chk_Vendor_Select
        '
        Me.chk_Vendor_Select.AccessibleName = "chk_Vendor_Select"
        Me.chk_Vendor_Select.Location = New System.Drawing.Point(192, 1)
        Me.chk_Vendor_Select.MyLinkLable1 = Nothing
        Me.chk_Vendor_Select.MyLinkLable2 = Nothing
        Me.chk_Vendor_Select.Name = "chk_Vendor_Select"
        Me.chk_Vendor_Select.Size = New System.Drawing.Size(50, 18)
        Me.chk_Vendor_Select.TabIndex = 1
        Me.chk_Vendor_Select.Text = "Select"
        '
        'chk_Vendor_All
        '
        Me.chk_Vendor_All.AccessibleName = "chk_Vendor_All"
        Me.chk_Vendor_All.Location = New System.Drawing.Point(141, 1)
        Me.chk_Vendor_All.MyLinkLable1 = Nothing
        Me.chk_Vendor_All.MyLinkLable2 = Nothing
        Me.chk_Vendor_All.Name = "chk_Vendor_All"
        Me.chk_Vendor_All.Size = New System.Drawing.Size(33, 18)
        Me.chk_Vendor_All.TabIndex = 0
        Me.chk_Vendor_All.Text = "All"
        '
        'RadGroupBox9
        '
        Me.RadGroupBox9.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox9.Controls.Add(Me.cbgDoc)
        Me.RadGroupBox9.Controls.Add(Me.Panel6)
        Me.RadGroupBox9.HeaderText = "Document No"
        Me.RadGroupBox9.Location = New System.Drawing.Point(10, 57)
        Me.RadGroupBox9.Name = "RadGroupBox9"
        Me.RadGroupBox9.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox9.Size = New System.Drawing.Size(404, 160)
        Me.RadGroupBox9.TabIndex = 14
        Me.RadGroupBox9.Text = "Document No"
        '
        'cbgDoc
        '
        Me.cbgDoc.AccessibleName = "cbgDoc"
        Me.cbgDoc.CheckedValue = Nothing
        Me.cbgDoc.DataSource = Nothing
        Me.cbgDoc.DisplayMember = "Name"
        Me.cbgDoc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgDoc.Location = New System.Drawing.Point(10, 40)
        Me.cbgDoc.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgDoc.MyShowHeadrText = False
        Me.cbgDoc.Name = "cbgDoc"
        Me.cbgDoc.Size = New System.Drawing.Size(384, 110)
        Me.cbgDoc.TabIndex = 1
        Me.cbgDoc.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.chk_Doc_Select)
        Me.Panel6.Controls.Add(Me.chkall)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(384, 20)
        Me.Panel6.TabIndex = 0
        '
        'chk_Doc_Select
        '
        Me.chk_Doc_Select.AccessibleName = "chk_Doc_Select"
        Me.chk_Doc_Select.Location = New System.Drawing.Point(192, 1)
        Me.chk_Doc_Select.MyLinkLable1 = Nothing
        Me.chk_Doc_Select.MyLinkLable2 = Nothing
        Me.chk_Doc_Select.Name = "chk_Doc_Select"
        Me.chk_Doc_Select.Size = New System.Drawing.Size(50, 18)
        Me.chk_Doc_Select.TabIndex = 1
        Me.chk_Doc_Select.Text = "Select"
        '
        'chkall
        '
        Me.chkall.AccessibleName = "chkAll"
        Me.chkall.Location = New System.Drawing.Point(141, 1)
        Me.chkall.MyLinkLable1 = Nothing
        Me.chkall.MyLinkLable2 = Nothing
        Me.chkall.Name = "chkall"
        Me.chkall.Size = New System.Drawing.Size(33, 18)
        Me.chkall.TabIndex = 0
        Me.chkall.Text = "All"
        '
        'dtpToDate1
        '
        Me.dtpToDate1.AccessibleName = "dtpToDate"
        Me.dtpToDate1.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate1.Location = New System.Drawing.Point(332, 7)
        Me.dtpToDate1.MendatroryField = False
        Me.dtpToDate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate1.MyLinkLable1 = Nothing
        Me.dtpToDate1.MyLinkLable2 = Nothing
        Me.dtpToDate1.Name = "dtpToDate1"
        Me.dtpToDate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate1.Size = New System.Drawing.Size(82, 20)
        Me.dtpToDate1.TabIndex = 11
        Me.dtpToDate1.TabStop = False
        Me.dtpToDate1.Text = "28-03-2012"
        Me.dtpToDate1.Value = New Date(2012, 3, 28, 0, 0, 0, 0)
        '
        'RadLabel6
        '
        Me.RadLabel6.Location = New System.Drawing.Point(9, 7)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel6.TabIndex = 12
        Me.RadLabel6.Text = "From Date"
        '
        'dtpFromdate1
        '
        Me.dtpFromdate1.AccessibleName = "dtpFromdate"
        Me.dtpFromdate1.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromdate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromdate1.Location = New System.Drawing.Point(74, 7)
        Me.dtpFromdate1.MendatroryField = False
        Me.dtpFromdate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.MyLinkLable1 = Nothing
        Me.dtpFromdate1.MyLinkLable2 = Nothing
        Me.dtpFromdate1.Name = "dtpFromdate1"
        Me.dtpFromdate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.Size = New System.Drawing.Size(82, 20)
        Me.dtpFromdate1.TabIndex = 10
        Me.dtpFromdate1.TabStop = False
        Me.dtpFromdate1.Text = "28-03-2012"
        Me.dtpFromdate1.Value = New Date(2012, 3, 28, 0, 0, 0, 0)
        '
        'RadLabel5
        '
        Me.RadLabel5.Location = New System.Drawing.Point(258, 7)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel5.TabIndex = 13
        Me.RadLabel5.Text = "To Date"
        '
        'btnreset1
        '
        Me.btnreset1.AccessibleName = "btnreset"
        Me.btnreset1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset1.Location = New System.Drawing.Point(12, 3)
        Me.btnreset1.Name = "btnreset1"
        Me.btnreset1.Size = New System.Drawing.Size(68, 18)
        Me.btnreset1.TabIndex = 17
        Me.btnreset1.Text = "Reset"
        '
        'btnClose1
        '
        Me.btnClose1.AccessibleName = "btnClose"
        Me.btnClose1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose1.Location = New System.Drawing.Point(373, 3)
        Me.btnClose1.Name = "btnClose1"
        Me.btnClose1.Size = New System.Drawing.Size(68, 18)
        Me.btnClose1.TabIndex = 18
        Me.btnClose1.Text = "Close"
        '
        'btnprint1
        '
        Me.btnprint1.AccessibleName = "btnPrint"
        Me.btnprint1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint1.Location = New System.Drawing.Point(86, 3)
        Me.btnprint1.Name = "btnprint1"
        Me.btnprint1.Size = New System.Drawing.Size(68, 18)
        Me.btnprint1.TabIndex = 16
        Me.btnprint1.Text = "Print"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset1)
        Me.SplitContainer1.Size = New System.Drawing.Size(452, 645)
        Me.SplitContainer1.SplitterDistance = 611
        Me.SplitContainer1.TabIndex = 1
        '
        'FrmSrnReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(452, 645)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmSrnReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "SRN Report"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnOther, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnFinishedGood, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.chk_Vendor_Select, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_Vendor_All, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox9.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.chk_Doc_Select, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dtpToDate1 As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel6 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents dtpFromdate1 As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel5 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadGroupBox9 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgDoc As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chk_Doc_Select As common.Controls.MyRadioButton
    Friend WithEvents chkall As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox8 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVendor1 As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chk_Vendor_Select As common.Controls.MyRadioButton
    Friend WithEvents chk_Vendor_All As common.Controls.MyRadioButton
    Friend WithEvents btnreset1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnFinishedGood As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbtnOther As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

