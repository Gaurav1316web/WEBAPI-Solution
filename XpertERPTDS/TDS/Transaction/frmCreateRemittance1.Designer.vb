Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCreateRemittance
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
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.mbtnSaveLayout = New Telerik.WinControls.UI.RadMenuItem
        Me.mbtnDeleteLayout = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.lblRSTotal = New common.Controls.MyLabel
        Me.lblRSSecEduCess = New common.Controls.MyLabel
        Me.lblRSEduCess = New common.Controls.MyLabel
        Me.lblRSSurcharge = New common.Controls.MyLabel
        Me.lblRSTDSAmt = New common.Controls.MyLabel
        Me.lblRDTotal = New common.Controls.MyLabel
        Me.lblRDEduCess = New common.Controls.MyLabel
        Me.lblRDSurcharge = New common.Controls.MyLabel
        Me.lblRDTDSAmt = New common.Controls.MyLabel
        Me.lblRDSecEduCess = New common.Controls.MyLabel
        Me.RadLabel14 = New common.Controls.MyLabel
        Me.RadLabel13 = New common.Controls.MyLabel
        Me.RadLabel12 = New common.Controls.MyLabel
        Me.RadLabel11 = New common.Controls.MyLabel
        Me.RadLabel10 = New common.Controls.MyLabel
        Me.RadLabel9 = New common.Controls.MyLabel
        Me.RadLabel8 = New common.Controls.MyLabel
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtBranch = New common.UserControls.txtFinder
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.txtSection = New common.UserControls.txtFinder
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.pnlDateRange = New System.Windows.Forms.Panel
        Me.txtFromDate = New common.Controls.MyDateTimePicker
        Me.RadLabel4 = New common.Controls.MyLabel
        Me.txtToDate = New common.Controls.MyDateTimePicker
        Me.RadLabel7 = New common.Controls.MyLabel
        Me.pnlFinderRange = New System.Windows.Forms.Panel
        Me.txtTo = New common.UserControls.txtFinder
        Me.txtFrom = New common.UserControls.txtFinder
        Me.RadLabel5 = New common.Controls.MyLabel
        Me.RadLabel6 = New common.Controls.MyLabel
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.cboFilterBy = New common.Controls.MyComboBox
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rbtnNonCompany = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnCompany = New Telerik.WinControls.UI.RadRadioButton
        Me.lblBranchName = New common.Controls.MyLabel
        Me.lblSectionName = New common.Controls.MyLabel
        Me.btnClearAll = New Telerik.WinControls.UI.RadButton
        Me.btnRemittAll = New Telerik.WinControls.UI.RadButton
        Me.btnPost = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.lblRSTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRSSecEduCess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRSEduCess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRSSurcharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRSTDSAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRDTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRDEduCess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRDSurcharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRDTDSAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRDSecEduCess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDateRange.SuspendLayout()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFinderRange.SuspendLayout()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboFilterBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnNonCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBranchName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSectionName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClearAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRemittAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(688, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Setting"
        Me.RadMenuItem1.AccessibleName = "Setting"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mbtnSaveLayout, Me.mbtnDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'mbtnSaveLayout
        '
        Me.mbtnSaveLayout.AccessibleDescription = "Save Layout"
        Me.mbtnSaveLayout.AccessibleName = "Save Layout"
        Me.mbtnSaveLayout.Name = "mbtnSaveLayout"
        Me.mbtnSaveLayout.Text = "Save Layout"
        Me.mbtnSaveLayout.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'mbtnDeleteLayout
        '
        Me.mbtnDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.mbtnDeleteLayout.AccessibleName = "Delete Layout"
        Me.mbtnDeleteLayout.Name = "mbtnDeleteLayout"
        Me.mbtnDeleteLayout.Text = "Delete Layout"
        Me.mbtnDeleteLayout.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClearAll)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRemittAll)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(688, 510)
        Me.SplitContainer1.SplitterDistance = 468
        Me.SplitContainer1.TabIndex = 2
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 130)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(688, 257)
        Me.RadGroupBox2.TabIndex = 1
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.ShowFilteringRow = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowGroupPanel = False
        Me.gv1.Size = New System.Drawing.Size(668, 227)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.lblRSTotal)
        Me.RadGroupBox3.Controls.Add(Me.lblRSSecEduCess)
        Me.RadGroupBox3.Controls.Add(Me.lblRSEduCess)
        Me.RadGroupBox3.Controls.Add(Me.lblRSSurcharge)
        Me.RadGroupBox3.Controls.Add(Me.lblRSTDSAmt)
        Me.RadGroupBox3.Controls.Add(Me.lblRDTotal)
        Me.RadGroupBox3.Controls.Add(Me.lblRDEduCess)
        Me.RadGroupBox3.Controls.Add(Me.lblRDSurcharge)
        Me.RadGroupBox3.Controls.Add(Me.lblRDTDSAmt)
        Me.RadGroupBox3.Controls.Add(Me.lblRDSecEduCess)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel14)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel13)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel12)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel11)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel10)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel9)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel8)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 387)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(688, 81)
        Me.RadGroupBox3.TabIndex = 1
        '
        'lblRSTotal
        '
        Me.lblRSTotal.AutoSize = False
        Me.lblRSTotal.Location = New System.Drawing.Point(575, 53)
        Me.lblRSTotal.Name = "lblRSTotal"
        Me.lblRSTotal.Size = New System.Drawing.Size(102, 18)
        Me.lblRSTotal.TabIndex = 16
        Me.lblRSTotal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRSSecEduCess
        '
        Me.lblRSSecEduCess.AutoSize = False
        Me.lblRSSecEduCess.Location = New System.Drawing.Point(455, 53)
        Me.lblRSSecEduCess.Name = "lblRSSecEduCess"
        Me.lblRSSecEduCess.Size = New System.Drawing.Size(102, 18)
        Me.lblRSSecEduCess.TabIndex = 15
        Me.lblRSSecEduCess.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRSEduCess
        '
        Me.lblRSEduCess.AutoSize = False
        Me.lblRSEduCess.Location = New System.Drawing.Point(335, 53)
        Me.lblRSEduCess.Name = "lblRSEduCess"
        Me.lblRSEduCess.Size = New System.Drawing.Size(102, 18)
        Me.lblRSEduCess.TabIndex = 14
        Me.lblRSEduCess.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRSSurcharge
        '
        Me.lblRSSurcharge.AutoSize = False
        Me.lblRSSurcharge.Location = New System.Drawing.Point(215, 53)
        Me.lblRSSurcharge.Name = "lblRSSurcharge"
        Me.lblRSSurcharge.Size = New System.Drawing.Size(102, 18)
        Me.lblRSSurcharge.TabIndex = 13
        Me.lblRSSurcharge.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRSTDSAmt
        '
        Me.lblRSTDSAmt.AutoSize = False
        Me.lblRSTDSAmt.Location = New System.Drawing.Point(95, 53)
        Me.lblRSTDSAmt.Name = "lblRSTDSAmt"
        Me.lblRSTDSAmt.Size = New System.Drawing.Size(102, 18)
        Me.lblRSTDSAmt.TabIndex = 12
        Me.lblRSTDSAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRDTotal
        '
        Me.lblRDTotal.AutoSize = False
        Me.lblRDTotal.Location = New System.Drawing.Point(575, 30)
        Me.lblRDTotal.Name = "lblRDTotal"
        Me.lblRDTotal.Size = New System.Drawing.Size(102, 18)
        Me.lblRDTotal.TabIndex = 10
        Me.lblRDTotal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRDEduCess
        '
        Me.lblRDEduCess.AutoSize = False
        Me.lblRDEduCess.Location = New System.Drawing.Point(335, 31)
        Me.lblRDEduCess.Name = "lblRDEduCess"
        Me.lblRDEduCess.Size = New System.Drawing.Size(102, 18)
        Me.lblRDEduCess.TabIndex = 8
        Me.lblRDEduCess.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRDSurcharge
        '
        Me.lblRDSurcharge.AutoSize = False
        Me.lblRDSurcharge.Location = New System.Drawing.Point(215, 31)
        Me.lblRDSurcharge.Name = "lblRDSurcharge"
        Me.lblRDSurcharge.Size = New System.Drawing.Size(102, 18)
        Me.lblRDSurcharge.TabIndex = 7
        Me.lblRDSurcharge.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRDTDSAmt
        '
        Me.lblRDTDSAmt.AutoSize = False
        Me.lblRDTDSAmt.Location = New System.Drawing.Point(95, 31)
        Me.lblRDTDSAmt.Name = "lblRDTDSAmt"
        Me.lblRDTDSAmt.Size = New System.Drawing.Size(102, 18)
        Me.lblRDTDSAmt.TabIndex = 6
        Me.lblRDTDSAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRDSecEduCess
        '
        Me.lblRDSecEduCess.AutoSize = False
        Me.lblRDSecEduCess.Location = New System.Drawing.Point(455, 31)
        Me.lblRDSecEduCess.Name = "lblRDSecEduCess"
        Me.lblRDSecEduCess.Size = New System.Drawing.Size(102, 18)
        Me.lblRDSecEduCess.TabIndex = 9
        Me.lblRDSecEduCess.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel14
        '
        Me.RadLabel14.Location = New System.Drawing.Point(623, 7)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(54, 18)
        Me.RadLabel14.TabIndex = 4
        Me.RadLabel14.Text = "Total TDS"
        '
        'RadLabel13
        '
        Me.RadLabel13.Location = New System.Drawing.Point(455, 7)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(102, 18)
        Me.RadLabel13.TabIndex = 3
        Me.RadLabel13.Text = "Sec Education Cess"
        '
        'RadLabel12
        '
        Me.RadLabel12.Location = New System.Drawing.Point(356, 7)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(81, 18)
        Me.RadLabel12.TabIndex = 2
        Me.RadLabel12.Text = "Education Cess"
        '
        'RadLabel11
        '
        Me.RadLabel11.Location = New System.Drawing.Point(261, 7)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(56, 18)
        Me.RadLabel11.TabIndex = 1
        Me.RadLabel11.Text = "Surcharge"
        '
        'RadLabel10
        '
        Me.RadLabel10.Location = New System.Drawing.Point(127, 7)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(70, 18)
        Me.RadLabel10.TabIndex = 0
        Me.RadLabel10.Text = "TDS Amount"
        '
        'RadLabel9
        '
        Me.RadLabel9.Location = New System.Drawing.Point(5, 53)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(85, 18)
        Me.RadLabel9.TabIndex = 11
        Me.RadLabel9.Text = "Remit Summary"
        '
        'RadLabel8
        '
        Me.RadLabel8.Location = New System.Drawing.Point(5, 30)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(72, 18)
        Me.RadLabel8.TabIndex = 5
        Me.RadLabel8.Text = "Remit Details"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.pnlDateRange)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtBranch)
        Me.RadGroupBox1.Controls.Add(Me.txtSection)
        Me.RadGroupBox1.Controls.Add(Me.pnlFinderRange)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.cboFilterBy)
        Me.RadGroupBox1.Controls.Add(Me.btnRefresh)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox1)
        Me.RadGroupBox1.Controls.Add(Me.lblBranchName)
        Me.RadGroupBox1.Controls.Add(Me.lblSectionName)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(688, 130)
        Me.RadGroupBox1.TabIndex = 0
        '
        'MyLabel1
        '
        Me.MyLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel1.BorderVisible = True
        Me.MyLabel1.ForeColor = System.Drawing.Color.Blue
        Me.MyLabel1.Location = New System.Drawing.Point(521, 106)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(161, 18)
        Me.MyLabel1.TabIndex = 9
        Me.MyLabel1.Text = " Contains Debit or Credit Note "
        '
        'txtBranch
        '
        Me.txtBranch.Location = New System.Drawing.Point(52, 33)
        Me.txtBranch.MendatroryField = True
        Me.txtBranch.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBranch.MyLinkLable1 = Me.RadLabel2
        Me.txtBranch.MyLinkLable2 = Nothing
        Me.txtBranch.MyReadOnly = False
        Me.txtBranch.Name = "txtBranch"
        Me.txtBranch.Size = New System.Drawing.Size(143, 19)
        Me.txtBranch.TabIndex = 2
        Me.txtBranch.Value = ""
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(6, 33)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(40, 18)
        Me.RadLabel2.TabIndex = 1
        Me.RadLabel2.Text = "Branch"
        '
        'txtSection
        '
        Me.txtSection.Location = New System.Drawing.Point(53, 5)
        Me.txtSection.MendatroryField = True
        Me.txtSection.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSection.MyLinkLable1 = Me.RadLabel1
        Me.txtSection.MyLinkLable2 = Nothing
        Me.txtSection.MyReadOnly = False
        Me.txtSection.Name = "txtSection"
        Me.txtSection.Size = New System.Drawing.Size(143, 19)
        Me.txtSection.TabIndex = 0
        Me.txtSection.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(6, 5)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(43, 18)
        Me.RadLabel1.TabIndex = 1
        Me.RadLabel1.Text = "Section"
        '
        'pnlDateRange
        '
        Me.pnlDateRange.Controls.Add(Me.txtFromDate)
        Me.pnlDateRange.Controls.Add(Me.txtToDate)
        Me.pnlDateRange.Controls.Add(Me.RadLabel4)
        Me.pnlDateRange.Controls.Add(Me.RadLabel7)
        Me.pnlDateRange.Location = New System.Drawing.Point(200, 55)
        Me.pnlDateRange.Name = "pnlDateRange"
        Me.pnlDateRange.Size = New System.Drawing.Size(375, 28)
        Me.pnlDateRange.TabIndex = 6
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(45, 4)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.RadLabel4
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(81, 20)
        Me.txtFromDate.TabIndex = 0
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13-06-2011"
        Me.txtFromDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.Location = New System.Drawing.Point(4, 6)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(35, 18)
        Me.RadLabel4.TabIndex = 0
        Me.RadLabel4.Text = "From "
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(153, 4)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.RadLabel7
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(81, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13-06-2011"
        Me.txtToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel7
        '
        Me.RadLabel7.Location = New System.Drawing.Point(131, 5)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(22, 18)
        Me.RadLabel7.TabIndex = 2
        Me.RadLabel7.Text = "To "
        '
        'pnlFinderRange
        '
        Me.pnlFinderRange.Controls.Add(Me.txtTo)
        Me.pnlFinderRange.Controls.Add(Me.txtFrom)
        Me.pnlFinderRange.Controls.Add(Me.RadLabel5)
        Me.pnlFinderRange.Controls.Add(Me.RadLabel6)
        Me.pnlFinderRange.Location = New System.Drawing.Point(200, 55)
        Me.pnlFinderRange.Name = "pnlFinderRange"
        Me.pnlFinderRange.Size = New System.Drawing.Size(375, 28)
        Me.pnlFinderRange.TabIndex = 5
        '
        'txtTo
        '
        Me.txtTo.Location = New System.Drawing.Point(225, 3)
        Me.txtTo.MendatroryField = False
        Me.txtTo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTo.MyLinkLable1 = Nothing
        Me.txtTo.MyLinkLable2 = Nothing
        Me.txtTo.MyReadOnly = False
        Me.txtTo.Name = "txtTo"
        Me.txtTo.Size = New System.Drawing.Size(143, 21)
        Me.txtTo.TabIndex = 1
        Me.txtTo.Value = ""
        '
        'txtFrom
        '
        Me.txtFrom.Location = New System.Drawing.Point(45, 4)
        Me.txtFrom.MendatroryField = False
        Me.txtFrom.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFrom.MyLinkLable1 = Nothing
        Me.txtFrom.MyLinkLable2 = Nothing
        Me.txtFrom.MyReadOnly = False
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(143, 21)
        Me.txtFrom.TabIndex = 0
        Me.txtFrom.Value = ""
        '
        'RadLabel5
        '
        Me.RadLabel5.Location = New System.Drawing.Point(27, 57)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(35, 18)
        Me.RadLabel5.TabIndex = 0
        Me.RadLabel5.Text = "From "
        '
        'RadLabel6
        '
        Me.RadLabel6.Location = New System.Drawing.Point(195, 5)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(22, 18)
        Me.RadLabel6.TabIndex = 0
        Me.RadLabel6.Text = "To "
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(5, 59)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(46, 18)
        Me.RadLabel3.TabIndex = 4
        Me.RadLabel3.Text = "Filter By"
        '
        'cboFilterBy
        '
        Me.cboFilterBy.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Text = "Vendor"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Document No"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "Document Date"
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Text = "Nature Of Deduction"
        RadListDataItem4.TextWrap = True
        Me.cboFilterBy.Items.Add(RadListDataItem1)
        Me.cboFilterBy.Items.Add(RadListDataItem2)
        Me.cboFilterBy.Items.Add(RadListDataItem3)
        Me.cboFilterBy.Items.Add(RadListDataItem4)
        Me.cboFilterBy.Location = New System.Drawing.Point(55, 59)
        Me.cboFilterBy.MendatroryField = False
        Me.cboFilterBy.MyLinkLable1 = Me.RadLabel3
        Me.cboFilterBy.MyLinkLable2 = Nothing
        Me.cboFilterBy.Name = "cboFilterBy"
        Me.cboFilterBy.Size = New System.Drawing.Size(142, 20)
        Me.cboFilterBy.TabIndex = 4
        '
        'btnRefresh
        '
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.btnRefresh.Location = New System.Drawing.Point(227, 100)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(80, 24)
        Me.btnRefresh.TabIndex = 8
        Me.btnRefresh.Text = ">>"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnNonCompany)
        Me.GroupBox1.Controls.Add(Me.rbtnCompany)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 87)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(215, 39)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Type"
        '
        'rbtnNonCompany
        '
        Me.rbtnNonCompany.Location = New System.Drawing.Point(96, 15)
        Me.rbtnNonCompany.Name = "rbtnNonCompany"
        Me.rbtnNonCompany.Size = New System.Drawing.Size(101, 18)
        Me.rbtnNonCompany.TabIndex = 1
        Me.rbtnNonCompany.Text = "Non Companies"
        '
        'rbtnCompany
        '
        Me.rbtnCompany.Location = New System.Drawing.Point(7, 15)
        Me.rbtnCompany.Name = "rbtnCompany"
        Me.rbtnCompany.Size = New System.Drawing.Size(76, 18)
        Me.rbtnCompany.TabIndex = 0
        Me.rbtnCompany.TabStop = True
        Me.rbtnCompany.Text = "Companies"
        Me.rbtnCompany.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'lblBranchName
        '
        Me.lblBranchName.AutoSize = False
        Me.lblBranchName.BorderVisible = True
        Me.lblBranchName.Location = New System.Drawing.Point(202, 32)
        Me.lblBranchName.Name = "lblBranchName"
        Me.lblBranchName.Size = New System.Drawing.Size(198, 20)
        Me.lblBranchName.TabIndex = 3
        '
        'lblSectionName
        '
        Me.lblSectionName.AutoSize = False
        Me.lblSectionName.BorderVisible = True
        Me.lblSectionName.Location = New System.Drawing.Point(202, 4)
        Me.lblSectionName.Name = "lblSectionName"
        Me.lblSectionName.Size = New System.Drawing.Size(198, 20)
        Me.lblSectionName.TabIndex = 1
        '
        'btnClearAll
        '
        Me.btnClearAll.Location = New System.Drawing.Point(178, 8)
        Me.btnClearAll.Name = "btnClearAll"
        Me.btnClearAll.Size = New System.Drawing.Size(80, 24)
        Me.btnClearAll.TabIndex = 2
        Me.btnClearAll.Text = "Clear All"
        '
        'btnRemittAll
        '
        Me.btnRemittAll.Location = New System.Drawing.Point(92, 8)
        Me.btnRemittAll.Name = "btnRemittAll"
        Me.btnRemittAll.Size = New System.Drawing.Size(80, 24)
        Me.btnRemittAll.TabIndex = 1
        Me.btnRemittAll.Text = "Remit All"
        '
        'btnPost
        '
        Me.btnPost.Location = New System.Drawing.Point(6, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(80, 24)
        Me.btnPost.TabIndex = 0
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(596, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 24)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'FrmCreateRemittance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(688, 530)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmCreateRemittance"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Create Remittance"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.lblRSTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRSSecEduCess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRSEduCess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRSSurcharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRSTDSAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRDTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRDEduCess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRDSurcharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRDTDSAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRDSecEduCess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDateRange.ResumeLayout(False)
        Me.pnlDateRange.PerformLayout()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFinderRange.ResumeLayout(False)
        Me.pnlFinderRange.PerformLayout()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboFilterBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnNonCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBranchName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSectionName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClearAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRemittAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mbtnSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mbtnDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblRSTotal As common.Controls.MyLabel
    Friend WithEvents lblRSSecEduCess As common.Controls.MyLabel
    Friend WithEvents lblRSEduCess As common.Controls.MyLabel
    Friend WithEvents lblRSSurcharge As common.Controls.MyLabel
    Friend WithEvents lblRSTDSAmt As common.Controls.MyLabel
    Friend WithEvents lblRDTotal As common.Controls.MyLabel
    Friend WithEvents lblRDEduCess As common.Controls.MyLabel
    Friend WithEvents lblRDSurcharge As common.Controls.MyLabel
    Friend WithEvents lblRDTDSAmt As common.Controls.MyLabel
    Friend WithEvents lblRDSecEduCess As common.Controls.MyLabel
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents RadLabel13 As common.Controls.MyLabel
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtBranch As common.UserControls.txtFinder
    Friend WithEvents txtSection As common.UserControls.txtFinder
    Friend WithEvents pnlDateRange As System.Windows.Forms.Panel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents pnlFinderRange As System.Windows.Forms.Panel
    Friend WithEvents txtTo As common.UserControls.txtFinder
    Friend WithEvents txtFrom As common.UserControls.txtFinder
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents cboFilterBy As common.Controls.MyComboBox
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnNonCompany As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnCompany As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents lblBranchName As common.Controls.MyLabel
    Friend WithEvents lblSectionName As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnClearAll As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRemittAll As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton

End Class

