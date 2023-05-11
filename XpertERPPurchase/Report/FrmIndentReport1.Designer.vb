<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmIndentReport
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
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkLocationSelect = New common.Controls.MyRadioButton
        Me.chkLocationAll = New common.Controls.MyRadioButton
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgDocument = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkDoc_select = New common.Controls.MyRadioButton
        Me.chkdocAll = New common.Controls.MyRadioButton
        Me.dtpTodate = New common.Controls.MyDateTimePicker
        Me.dtpfromdate = New common.Controls.MyDateTimePicker
        Me.RadRadioButton2 = New Telerik.WinControls.UI.RadRadioButton
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.RadLabel4 = New common.Controls.MyLabel
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgItem = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.itemselect = New common.Controls.MyRadioButton
        Me.itemall = New common.Controls.MyRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.rdbtnBasedOnPO = New Telerik.WinControls.UI.RadRadioButton
        Me.RdbtnBasedOnSrn = New Telerik.WinControls.UI.RadRadioButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkDoc_select, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkdocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpTodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dtpfromdate.SuspendLayout()
        CType(Me.RadRadioButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.itemselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.itemall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.rdbtnBasedOnPO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RdbtnBasedOnSrn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.HeaderText = " Location"
        Me.RadGroupBox1.Location = New System.Drawing.Point(13, 369)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(411, 152)
        Me.RadGroupBox1.TabIndex = 71
        Me.RadGroupBox1.Text = " Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.AccessibleDescription = "cbgLocation"
        Me.cbgLocation.AccessibleName = ""
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 45)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(391, 97)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkLocationSelect)
        Me.Panel1.Controls.Add(Me.chkLocationAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(391, 25)
        Me.Panel1.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.AccessibleDescription = "chkDoc_select"
        Me.chkLocationSelect.Location = New System.Drawing.Point(192, 3)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.AccessibleDescription = "chkdocAll"
        Me.chkLocationAll.Location = New System.Drawing.Point(141, 3)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.cbgDocument)
        Me.RadGroupBox6.Controls.Add(Me.Panel4)
        Me.RadGroupBox6.HeaderText = "Document No"
        Me.RadGroupBox6.Location = New System.Drawing.Point(13, 54)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(411, 146)
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
        Me.cbgDocument.Size = New System.Drawing.Size(391, 96)
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
        Me.Panel4.Size = New System.Drawing.Size(391, 20)
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
        Me.dtpTodate.Location = New System.Drawing.Point(342, 6)
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
        Me.dtpfromdate.Controls.Add(Me.RadRadioButton2)
        Me.dtpfromdate.CustomFormat = "dd-MM-yyyy"
        Me.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfromdate.Location = New System.Drawing.Point(78, 6)
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
        'RadRadioButton2
        '
        Me.RadRadioButton2.Location = New System.Drawing.Point(21, 17)
        Me.RadRadioButton2.Name = "RadRadioButton2"
        Me.RadRadioButton2.Size = New System.Drawing.Size(108, 18)
        Me.RadRadioButton2.TabIndex = 73
        Me.RadRadioButton2.Text = "RadRadioButton2"
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(275, 6)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel3.TabIndex = 9
        Me.RadLabel3.Text = "To Date"
        '
        'RadLabel4
        '
        Me.RadLabel4.Location = New System.Drawing.Point(13, 6)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel4.TabIndex = 8
        Me.RadLabel4.Text = "From Date"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(373, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 7
        Me.btnclose.Text = "Close"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(10, 5)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 18)
        Me.btnreset.TabIndex = 6
        Me.btnreset.Text = "&Reset"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(84, 5)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(68, 18)
        Me.btnprint.TabIndex = 5
        Me.btnprint.Text = "Print"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgItem)
        Me.RadGroupBox4.Controls.Add(Me.Panel3)
        Me.RadGroupBox4.HeaderText = "Item Code"
        Me.RadGroupBox4.Location = New System.Drawing.Point(13, 206)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(411, 157)
        Me.RadGroupBox4.TabIndex = 20
        Me.RadGroupBox4.Text = "Item Code"
        '
        'cbgItem
        '
        Me.cbgItem.AccessibleName = "cbgVendor"
        Me.cbgItem.CheckedValue = Nothing
        Me.cbgItem.DataSource = Nothing
        Me.cbgItem.DisplayMember = "Name"
        Me.cbgItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgItem.Location = New System.Drawing.Point(10, 40)
        Me.cbgItem.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgItem.MyShowHeadrText = False
        Me.cbgItem.Name = "cbgItem"
        Me.cbgItem.Size = New System.Drawing.Size(391, 107)
        Me.cbgItem.TabIndex = 1
        Me.cbgItem.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.itemselect)
        Me.Panel3.Controls.Add(Me.itemall)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(391, 20)
        Me.Panel3.TabIndex = 0
        '
        'itemselect
        '
        Me.itemselect.AccessibleName = "chk_vendor_select"
        Me.itemselect.Location = New System.Drawing.Point(192, 1)
        Me.itemselect.MyLinkLable1 = Nothing
        Me.itemselect.MyLinkLable2 = Nothing
        Me.itemselect.Name = "itemselect"
        Me.itemselect.Size = New System.Drawing.Size(50, 18)
        Me.itemselect.TabIndex = 1
        Me.itemselect.Text = "Select"
        '
        'itemall
        '
        Me.itemall.AccessibleName = "chkvendor_All"
        Me.itemall.Location = New System.Drawing.Point(141, 1)
        Me.itemall.MyLinkLable1 = Nothing
        Me.itemall.MyLinkLable2 = Nothing
        Me.itemall.Name = "itemall"
        Me.itemall.Size = New System.Drawing.Size(33, 18)
        Me.itemall.TabIndex = 0
        Me.itemall.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rdbtnBasedOnPO)
        Me.RadGroupBox2.Controls.Add(Me.RdbtnBasedOnSrn)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox2.Controls.Add(Me.RadGroupBox1)
        Me.RadGroupBox2.Controls.Add(Me.dtpfromdate)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox2.Controls.Add(Me.dtpTodate)
        Me.RadGroupBox2.Controls.Add(Me.RadGroupBox6)
        Me.RadGroupBox2.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(6, 12)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(435, 527)
        Me.RadGroupBox2.TabIndex = 72
        '
        'rdbtnBasedOnPO
        '
        Me.rdbtnBasedOnPO.Location = New System.Drawing.Point(170, 30)
        Me.rdbtnBasedOnPO.Name = "rdbtnBasedOnPO"
        Me.rdbtnBasedOnPO.Size = New System.Drawing.Size(86, 18)
        Me.rdbtnBasedOnPO.TabIndex = 73
        Me.rdbtnBasedOnPO.Text = "Based On PO"
        '
        'RdbtnBasedOnSrn
        '
        Me.RdbtnBasedOnSrn.Location = New System.Drawing.Point(275, 30)
        Me.RdbtnBasedOnSrn.Name = "RdbtnBasedOnSrn"
        Me.RdbtnBasedOnSrn.Size = New System.Drawing.Size(92, 18)
        Me.RdbtnBasedOnSrn.TabIndex = 72
        Me.RdbtnBasedOnSrn.Text = "Based On SRN"
        Me.RdbtnBasedOnSrn.Visible = False
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(453, 575)
        Me.SplitContainer1.SplitterDistance = 542
        Me.SplitContainer1.TabIndex = 73
        '
        'FrmIndentReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(453, 575)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmIndentReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Pending Indent Report"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkDoc_select, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkdocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpTodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dtpfromdate.ResumeLayout(False)
        Me.dtpfromdate.PerformLayout()
        CType(Me.RadRadioButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.itemselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.itemall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.rdbtnBasedOnPO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RdbtnBasedOnSrn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgDocument As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkDoc_select As common.Controls.MyRadioButton
    Friend WithEvents chkdocAll As common.Controls.MyRadioButton
    Friend WithEvents dtpTodate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpfromdate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents itemselect As common.Controls.MyRadioButton
    Friend WithEvents itemall As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadRadioButton2 As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RdbtnBasedOnSrn As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbtnBasedOnPO As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

