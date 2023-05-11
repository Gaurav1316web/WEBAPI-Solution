<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Store_Receipt_Note
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
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgDoc = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chk_doc_select = New common.Controls.MyRadioButton
        Me.chkdocall = New common.Controls.MyRadioButton
        Me.dtptodate = New common.Controls.MyDateTimePicker
        Me.dtpfromdate = New common.Controls.MyDateTimePicker
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.btnclose1 = New Telerik.WinControls.UI.RadButton
        Me.btnreset1 = New Telerik.WinControls.UI.RadButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.chkMDoc = New common.Controls.MyRadioButton
        Me.chkMVendor = New common.Controls.MyRadioButton
        Me.chkMPI = New common.Controls.MyRadioButton
        Me.chkMItem = New common.Controls.MyRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkLocalSelect = New common.Controls.MyRadioButton
        Me.chkLocalAll = New common.Controls.MyRadioButton
        Me.chkOthers = New Telerik.WinControls.UI.RadRadioButton
        Me.chkFnshdGoods = New Telerik.WinControls.UI.RadRadioButton
        Me.btnprint1 = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.grpItemType = New Telerik.WinControls.UI.RadGroupBox
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chk_doc_select, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkdocall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.chkMDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMPI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkLocalSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocalAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOthers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFnshdGoods, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.grpItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpItemType.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgDoc)
        Me.RadGroupBox3.Controls.Add(Me.Panel2)
        Me.RadGroupBox3.HeaderText = "Document No"
        Me.RadGroupBox3.Location = New System.Drawing.Point(7, 73)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(440, 180)
        Me.RadGroupBox3.TabIndex = 12
        Me.RadGroupBox3.Text = "Document No"
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
        Me.cbgDoc.Size = New System.Drawing.Size(420, 130)
        Me.cbgDoc.TabIndex = 1
        Me.cbgDoc.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chk_doc_select)
        Me.Panel2.Controls.Add(Me.chkdocall)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(420, 20)
        Me.Panel2.TabIndex = 0
        '
        'chk_doc_select
        '
        Me.chk_doc_select.AccessibleName = "chk_doc_select"
        Me.chk_doc_select.Location = New System.Drawing.Point(192, 1)
        Me.chk_doc_select.MyLinkLable1 = Nothing
        Me.chk_doc_select.MyLinkLable2 = Nothing
        Me.chk_doc_select.Name = "chk_doc_select"
        Me.chk_doc_select.Size = New System.Drawing.Size(50, 18)
        Me.chk_doc_select.TabIndex = 1
        Me.chk_doc_select.Text = "Select"
        '
        'chkdocall
        '
        Me.chkdocall.AccessibleName = "chk_All"
        Me.chkdocall.Location = New System.Drawing.Point(141, 1)
        Me.chkdocall.MyLinkLable1 = Nothing
        Me.chkdocall.MyLinkLable2 = Nothing
        Me.chkdocall.Name = "chkdocall"
        Me.chkdocall.Size = New System.Drawing.Size(33, 18)
        Me.chkdocall.TabIndex = 0
        Me.chkdocall.Text = "All"
        '
        'dtptodate
        '
        Me.dtptodate.AccessibleName = "dtptodate"
        Me.dtptodate.CustomFormat = "dd-MM-yyyy"
        Me.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtptodate.Location = New System.Drawing.Point(206, 10)
        Me.dtptodate.MendatroryField = False
        Me.dtptodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.MyLinkLable1 = Nothing
        Me.dtptodate.MyLinkLable2 = Nothing
        Me.dtptodate.Name = "dtptodate"
        Me.dtptodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.Size = New System.Drawing.Size(82, 20)
        Me.dtptodate.TabIndex = 11
        Me.dtptodate.TabStop = False
        Me.dtptodate.Text = "23-01-2012"
        Me.dtptodate.Value = New Date(2012, 1, 23, 0, 0, 0, 0)
        '
        'dtpfromdate
        '
        Me.dtpfromdate.AccessibleName = "dtpfromdate"
        Me.dtpfromdate.CustomFormat = "dd-MM-yyyy"
        Me.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfromdate.Location = New System.Drawing.Point(67, 9)
        Me.dtpfromdate.MendatroryField = False
        Me.dtpfromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate.MyLinkLable1 = Nothing
        Me.dtpfromdate.MyLinkLable2 = Nothing
        Me.dtpfromdate.Name = "dtpfromdate"
        Me.dtpfromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate.Size = New System.Drawing.Size(82, 20)
        Me.dtpfromdate.TabIndex = 10
        Me.dtpfromdate.TabStop = False
        Me.dtpfromdate.Text = "23-01-2012"
        Me.dtpfromdate.Value = New Date(2012, 1, 23, 0, 0, 0, 0)
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(155, 10)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel1.TabIndex = 18
        Me.RadLabel1.Text = "To Date"
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(4, 10)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel2.TabIndex = 17
        Me.RadLabel2.Text = "From Date"
        '
        'btnclose1
        '
        Me.btnclose1.AccessibleName = "btnclose"
        Me.btnclose1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose1.Location = New System.Drawing.Point(389, 8)
        Me.btnclose1.Name = "btnclose1"
        Me.btnclose1.Size = New System.Drawing.Size(68, 18)
        Me.btnclose1.TabIndex = 16
        Me.btnclose1.Text = "Close"
        '
        'btnreset1
        '
        Me.btnreset1.AccessibleName = "btnreset"
        Me.btnreset1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset1.Location = New System.Drawing.Point(12, 8)
        Me.btnreset1.Name = "btnreset1"
        Me.btnreset1.Size = New System.Drawing.Size(68, 18)
        Me.btnreset1.TabIndex = 15
        Me.btnreset1.Text = "Reset"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.dtptodate)
        Me.RadGroupBox1.Controls.Add(Me.dtpfromdate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(13, 14)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(459, 451)
        Me.RadGroupBox1.TabIndex = 1
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.chkMDoc)
        Me.RadGroupBox4.Controls.Add(Me.chkMVendor)
        Me.RadGroupBox4.Controls.Add(Me.chkMPI)
        Me.RadGroupBox4.Controls.Add(Me.chkMItem)
        Me.RadGroupBox4.HeaderText = ""
        Me.RadGroupBox4.Location = New System.Drawing.Point(7, 36)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(437, 39)
        Me.RadGroupBox4.TabIndex = 24
        '
        'chkMDoc
        '
        Me.chkMDoc.AccessibleName = "chkDoc"
        Me.chkMDoc.Location = New System.Drawing.Point(13, 7)
        Me.chkMDoc.MyLinkLable1 = Nothing
        Me.chkMDoc.MyLinkLable2 = Nothing
        Me.chkMDoc.Name = "chkMDoc"
        Me.chkMDoc.Size = New System.Drawing.Size(87, 18)
        Me.chkMDoc.TabIndex = 21
        Me.chkMDoc.Text = "DocumentNo"
        '
        'chkMVendor
        '
        Me.chkMVendor.AccessibleName = "chkVendor"
        Me.chkMVendor.Location = New System.Drawing.Point(202, 8)
        Me.chkMVendor.MyLinkLable1 = Nothing
        Me.chkMVendor.MyLinkLable2 = Nothing
        Me.chkMVendor.Name = "chkMVendor"
        Me.chkMVendor.Size = New System.Drawing.Size(57, 18)
        Me.chkMVendor.TabIndex = 20
        Me.chkMVendor.Text = "Vendor"
        '
        'chkMPI
        '
        Me.chkMPI.AccessibleName = "chk_doc_select"
        Me.chkMPI.Location = New System.Drawing.Point(312, 8)
        Me.chkMPI.MyLinkLable1 = Nothing
        Me.chkMPI.MyLinkLable2 = Nothing
        Me.chkMPI.Name = "chkMPI"
        Me.chkMPI.Size = New System.Drawing.Size(103, 18)
        Me.chkMPI.TabIndex = 22
        Me.chkMPI.TabStop = True
        Me.chkMPI.Text = "Purchase Invoice"
        Me.chkMPI.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'chkMItem
        '
        Me.chkMItem.AccessibleName = "chkItem"
        Me.chkMItem.Location = New System.Drawing.Point(125, 8)
        Me.chkMItem.MyLinkLable1 = Nothing
        Me.chkMItem.MyLinkLable2 = Nothing
        Me.chkMItem.Name = "chkMItem"
        Me.chkMItem.Size = New System.Drawing.Size(43, 18)
        Me.chkMItem.TabIndex = 19
        Me.chkMItem.Text = "Item"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox2.Controls.Add(Me.Panel1)
        Me.RadGroupBox2.HeaderText = "Location"
        Me.RadGroupBox2.Location = New System.Drawing.Point(7, 259)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(440, 180)
        Me.RadGroupBox2.TabIndex = 23
        Me.RadGroupBox2.Text = "Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.AccessibleName = "cbgDoc"
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 40)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(420, 130)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkLocalSelect)
        Me.Panel1.Controls.Add(Me.chkLocalAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(420, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkLocalSelect
        '
        Me.chkLocalSelect.AccessibleName = "chk_doc_select"
        Me.chkLocalSelect.Location = New System.Drawing.Point(192, 1)
        Me.chkLocalSelect.MyLinkLable1 = Nothing
        Me.chkLocalSelect.MyLinkLable2 = Nothing
        Me.chkLocalSelect.Name = "chkLocalSelect"
        Me.chkLocalSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocalSelect.TabIndex = 1
        Me.chkLocalSelect.Text = "Select"
        '
        'chkLocalAll
        '
        Me.chkLocalAll.AccessibleName = "chk_All"
        Me.chkLocalAll.Location = New System.Drawing.Point(141, 1)
        Me.chkLocalAll.MyLinkLable1 = Nothing
        Me.chkLocalAll.MyLinkLable2 = Nothing
        Me.chkLocalAll.Name = "chkLocalAll"
        Me.chkLocalAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocalAll.TabIndex = 0
        Me.chkLocalAll.Text = "All"
        '
        'chkOthers
        '
        Me.chkOthers.Location = New System.Drawing.Point(109, 10)
        Me.chkOthers.Name = "chkOthers"
        Me.chkOthers.Size = New System.Drawing.Size(54, 18)
        Me.chkOthers.TabIndex = 26
        Me.chkOthers.Text = "Others"
        '
        'chkFnshdGoods
        '
        Me.chkFnshdGoods.Location = New System.Drawing.Point(5, 10)
        Me.chkFnshdGoods.Name = "chkFnshdGoods"
        Me.chkFnshdGoods.Size = New System.Drawing.Size(97, 18)
        Me.chkFnshdGoods.TabIndex = 25
        Me.chkFnshdGoods.Text = "Finished Goods"
        '
        'btnprint1
        '
        Me.btnprint1.AccessibleName = "btnprint"
        Me.btnprint1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint1.Location = New System.Drawing.Point(89, 8)
        Me.btnprint1.Name = "btnprint1"
        Me.btnprint1.Size = New System.Drawing.Size(68, 18)
        Me.btnprint1.TabIndex = 14
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.grpItemType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose1)
        Me.SplitContainer1.Size = New System.Drawing.Size(484, 507)
        Me.SplitContainer1.SplitterDistance = 468
        Me.SplitContainer1.TabIndex = 2
        '
        'grpItemType
        '
        Me.grpItemType.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpItemType.Controls.Add(Me.chkOthers)
        Me.grpItemType.Controls.Add(Me.chkFnshdGoods)
        Me.grpItemType.HeaderText = ""
        Me.grpItemType.Location = New System.Drawing.Point(304, 17)
        Me.grpItemType.Name = "grpItemType"
        Me.grpItemType.Size = New System.Drawing.Size(166, 33)
        Me.grpItemType.TabIndex = 0
        '
        'Store_Receipt_Note
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 507)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Store_Receipt_Note"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Daily Receipt Note"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chk_doc_select, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkdocall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.chkMDoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMPI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkLocalSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocalAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOthers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFnshdGoods, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.grpItemType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpItemType.ResumeLayout(False)
        Me.grpItemType.PerformLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgDoc As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chk_doc_select As common.Controls.MyRadioButton
    Friend WithEvents chkdocall As common.Controls.MyRadioButton
    Friend WithEvents dtptodate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpfromdate As common.Controls.MyDateTimePicker
    Friend WithEvents btnclose1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnprint1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkMPI As common.Controls.MyRadioButton
    Friend WithEvents chkMDoc As common.Controls.MyRadioButton
    Friend WithEvents chkMVendor As common.Controls.MyRadioButton
    Friend WithEvents chkMItem As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkLocalSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocalAll As common.Controls.MyRadioButton
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkOthers As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkFnshdGoods As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents grpItemType As Telerik.WinControls.UI.RadGroupBox
End Class

