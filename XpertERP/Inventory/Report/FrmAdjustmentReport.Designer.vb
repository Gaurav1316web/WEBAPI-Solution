<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAdjustmentReport
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
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.chk_Location_Select = New common.Controls.MyRadioButton
        Me.chk_Location_All = New common.Controls.MyRadioButton
        Me.RadGroupBox9 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgDoc = New common.MyCheckBoxGrid
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.chk_Doc_Select = New common.Controls.MyRadioButton
        Me.chkall = New common.Controls.MyRadioButton
        Me.rdbtnReceipt = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbtnIssue = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbtnAdjustment = New Telerik.WinControls.UI.RadRadioButton
        Me.RadLabel6 = New Telerik.WinControls.UI.RadLabel
        Me.dtpFromdate1 = New common.Controls.MyDateTimePicker
        Me.RadLabel5 = New Telerik.WinControls.UI.RadLabel
        Me.dtpToDate1 = New common.Controls.MyDateTimePicker
        Me.btnreset1 = New Telerik.WinControls.UI.RadButton
        Me.btnClose1 = New Telerik.WinControls.UI.RadButton
        Me.btnprint1 = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.chk_Location_Select, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_Location_All, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox9.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.chk_Doc_Select, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnAdjustment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox8)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox9)
        Me.RadGroupBox1.Controls.Add(Me.rdbtnReceipt)
        Me.RadGroupBox1.Controls.Add(Me.rdbtnIssue)
        Me.RadGroupBox1.Controls.Add(Me.rdbtnAdjustment)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel6)
        Me.RadGroupBox1.Controls.Add(Me.dtpFromdate1)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel5)
        Me.RadGroupBox1.Controls.Add(Me.dtpToDate1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(13, 14)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(393, 435)
        Me.RadGroupBox1.TabIndex = 0
        '
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox8.Controls.Add(Me.Panel5)
        Me.RadGroupBox8.HeaderText = "Location"
        Me.RadGroupBox8.Location = New System.Drawing.Point(13, 246)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox8.Size = New System.Drawing.Size(367, 176)
        Me.RadGroupBox8.TabIndex = 24
        Me.RadGroupBox8.Text = "Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.AccessibleName = "cbgLocation"
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 40)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(347, 126)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chk_Location_Select)
        Me.Panel5.Controls.Add(Me.chk_Location_All)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(347, 20)
        Me.Panel5.TabIndex = 0
        '
        'chk_Location_Select
        '
        Me.chk_Location_Select.AccessibleName = "chk_Location_Select"
        Me.chk_Location_Select.Location = New System.Drawing.Point(165, 2)
        Me.chk_Location_Select.MyLinkLable1 = Nothing
        Me.chk_Location_Select.MyLinkLable2 = Nothing
        Me.chk_Location_Select.Name = "chk_Location_Select"
        Me.chk_Location_Select.Size = New System.Drawing.Size(50, 18)
        Me.chk_Location_Select.TabIndex = 1
        Me.chk_Location_Select.Text = "Select"
        '
        'chk_Location_All
        '
        Me.chk_Location_All.AccessibleName = "chk_Location_All"
        Me.chk_Location_All.Location = New System.Drawing.Point(114, 2)
        Me.chk_Location_All.MyLinkLable1 = Nothing
        Me.chk_Location_All.MyLinkLable2 = Nothing
        Me.chk_Location_All.Name = "chk_Location_All"
        Me.chk_Location_All.Size = New System.Drawing.Size(33, 18)
        Me.chk_Location_All.TabIndex = 0
        Me.chk_Location_All.Text = "All"
        '
        'RadGroupBox9
        '
        Me.RadGroupBox9.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox9.Controls.Add(Me.cbgDoc)
        Me.RadGroupBox9.Controls.Add(Me.Panel6)
        Me.RadGroupBox9.HeaderText = "Document No"
        Me.RadGroupBox9.Location = New System.Drawing.Point(13, 71)
        Me.RadGroupBox9.Name = "RadGroupBox9"
        Me.RadGroupBox9.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox9.Size = New System.Drawing.Size(367, 169)
        Me.RadGroupBox9.TabIndex = 23
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
        Me.cbgDoc.Size = New System.Drawing.Size(347, 119)
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
        Me.Panel6.Size = New System.Drawing.Size(347, 20)
        Me.Panel6.TabIndex = 0
        '
        'chk_Doc_Select
        '
        Me.chk_Doc_Select.AccessibleName = "chk_Doc_Select"
        Me.chk_Doc_Select.Location = New System.Drawing.Point(165, 1)
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
        Me.chkall.Location = New System.Drawing.Point(114, 1)
        Me.chkall.MyLinkLable1 = Nothing
        Me.chkall.MyLinkLable2 = Nothing
        Me.chkall.Name = "chkall"
        Me.chkall.Size = New System.Drawing.Size(33, 18)
        Me.chkall.TabIndex = 0
        Me.chkall.Text = "All"
        '
        'rdbtnReceipt
        '
        Me.rdbtnReceipt.Location = New System.Drawing.Point(268, 47)
        Me.rdbtnReceipt.Name = "rdbtnReceipt"
        Me.rdbtnReceipt.Size = New System.Drawing.Size(57, 18)
        Me.rdbtnReceipt.TabIndex = 22
        Me.rdbtnReceipt.Text = "Receipt"
        '
        'rdbtnIssue
        '
        Me.rdbtnIssue.Location = New System.Drawing.Point(153, 47)
        Me.rdbtnIssue.Name = "rdbtnIssue"
        Me.rdbtnIssue.Size = New System.Drawing.Size(45, 18)
        Me.rdbtnIssue.TabIndex = 21
        Me.rdbtnIssue.Text = "Issue"
        '
        'rdbtnAdjustment
        '
        Me.rdbtnAdjustment.Location = New System.Drawing.Point(23, 47)
        Me.rdbtnAdjustment.Name = "rdbtnAdjustment"
        Me.rdbtnAdjustment.Size = New System.Drawing.Size(78, 18)
        Me.rdbtnAdjustment.TabIndex = 20
        Me.rdbtnAdjustment.Text = "Adjustment"
        '
        'RadLabel6
        '
        Me.RadLabel6.Location = New System.Drawing.Point(13, 11)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel6.TabIndex = 13
        Me.RadLabel6.Text = "From Date"
        '
        'dtpFromdate1
        '
        Me.dtpFromdate1.AccessibleName = "dtpFromdate"
        Me.dtpFromdate1.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromdate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromdate1.Location = New System.Drawing.Point(78, 11)
        Me.dtpFromdate1.MendatroryField = False
        Me.dtpFromdate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.MyLinkLable1 = Nothing
        Me.dtpFromdate1.MyLinkLable2 = Nothing
        Me.dtpFromdate1.Name = "dtpFromdate1"
        Me.dtpFromdate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.Size = New System.Drawing.Size(82, 20)
        Me.dtpFromdate1.TabIndex = 15
        Me.dtpFromdate1.TabStop = False
        Me.dtpFromdate1.Text = "28-03-2012"
        Me.dtpFromdate1.Value = New Date(2012, 3, 28, 0, 0, 0, 0)
        '
        'RadLabel5
        '
        Me.RadLabel5.Location = New System.Drawing.Point(235, 11)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel5.TabIndex = 14
        Me.RadLabel5.Text = "To Date"
        '
        'dtpToDate1
        '
        Me.dtpToDate1.AccessibleName = "dtpToDate"
        Me.dtpToDate1.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate1.Location = New System.Drawing.Point(298, 9)
        Me.dtpToDate1.MendatroryField = False
        Me.dtpToDate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate1.MyLinkLable1 = Nothing
        Me.dtpToDate1.MyLinkLable2 = Nothing
        Me.dtpToDate1.Name = "dtpToDate1"
        Me.dtpToDate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate1.Size = New System.Drawing.Size(82, 20)
        Me.dtpToDate1.TabIndex = 12
        Me.dtpToDate1.TabStop = False
        Me.dtpToDate1.Text = "28-03-2012"
        Me.dtpToDate1.Value = New Date(2012, 3, 28, 0, 0, 0, 0)
        '
        'btnreset1
        '
        Me.btnreset1.AccessibleName = "btnreset"
        Me.btnreset1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset1.Location = New System.Drawing.Point(12, 4)
        Me.btnreset1.Name = "btnreset1"
        Me.btnreset1.Size = New System.Drawing.Size(68, 18)
        Me.btnreset1.TabIndex = 26
        Me.btnreset1.Text = "Reset"
        '
        'btnClose1
        '
        Me.btnClose1.AccessibleName = "btnClose"
        Me.btnClose1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose1.Location = New System.Drawing.Point(338, 4)
        Me.btnClose1.Name = "btnClose1"
        Me.btnClose1.Size = New System.Drawing.Size(68, 18)
        Me.btnClose1.TabIndex = 27
        Me.btnClose1.Text = "Close"
        '
        'btnprint1
        '
        Me.btnprint1.AccessibleName = "btnPrint"
        Me.btnprint1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint1.Location = New System.Drawing.Point(86, 4)
        Me.btnprint1.Name = "btnprint1"
        Me.btnprint1.Size = New System.Drawing.Size(68, 18)
        Me.btnprint1.TabIndex = 25
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose1)
        Me.SplitContainer1.Size = New System.Drawing.Size(420, 487)
        Me.SplitContainer1.SplitterDistance = 451
        Me.SplitContainer1.TabIndex = 1
        '
        'FrmAdjustmentReport
        '
        Me.AccessibleName = "FrmAdjustmentReport"
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(420, 487)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmAdjustmentReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Adjustment Report"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.chk_Location_Select, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_Location_All, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox9.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.chk_Doc_Select, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnIssue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnAdjustment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents RadLabel5 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents dtpFromdate1 As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel6 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents rdbtnReceipt As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbtnIssue As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbtnAdjustment As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox9 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgDoc As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chk_Doc_Select As common.Controls.MyRadioButton
    Friend WithEvents chkall As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox8 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chk_Location_Select As common.Controls.MyRadioButton
    Friend WithEvents chk_Location_All As common.Controls.MyRadioButton
    Friend WithEvents btnreset1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

