<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSaleHistory
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.rrbPosted = New Telerik.WinControls.UI.RadRadioButton
        Me.btnReferesh = New Telerik.WinControls.UI.RadButton
        Me.rrbAll = New Telerik.WinControls.UI.RadRadioButton
        Me.txtCustName = New common.Controls.MyTextBox
        Me.txtCustomer = New common.UserControls.txtFinder
        Me.lblCustomer = New common.Controls.MyLabel
        Me.cboSelectedBy = New common.Controls.MyComboBox
        Me.lblSelectedBy = New common.Controls.MyLabel
        Me.txtToDate = New common.Controls.MyDateTimePicker
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.txtFromDate = New common.Controls.MyDateTimePicker
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.SaveLayoutbtn = New Telerik.WinControls.UI.RadMenuItem
        Me.DeleteLaayout = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gvCustomer = New common.UserControls.MyRadGridView
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gvDetails = New common.UserControls.MyRadGridView
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rrbPosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReferesh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rrbAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboSelectedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSelectedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.gvCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gvDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetails.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rrbPosted)
        Me.RadGroupBox1.Controls.Add(Me.btnReferesh)
        Me.RadGroupBox1.Controls.Add(Me.rrbAll)
        Me.RadGroupBox1.Controls.Add(Me.txtCustName)
        Me.RadGroupBox1.Controls.Add(Me.txtCustomer)
        Me.RadGroupBox1.Controls.Add(Me.lblCustomer)
        Me.RadGroupBox1.Controls.Add(Me.cboSelectedBy)
        Me.RadGroupBox1.Controls.Add(Me.lblSelectedBy)
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.gv1)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(463, 95)
        Me.RadGroupBox1.TabIndex = 0
        '
        'rrbPosted
        '
        Me.rrbPosted.Location = New System.Drawing.Point(271, 48)
        Me.rrbPosted.Name = "rrbPosted"
        Me.rrbPosted.Size = New System.Drawing.Size(54, 18)
        Me.rrbPosted.TabIndex = 3
        Me.rrbPosted.Text = "Posted"
        '
        'btnReferesh
        '
        Me.btnReferesh.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnReferesh.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReferesh.Location = New System.Drawing.Point(390, 66)
        Me.btnReferesh.Name = "btnReferesh"
        Me.btnReferesh.Size = New System.Drawing.Size(69, 22)
        Me.btnReferesh.TabIndex = 6
        Me.btnReferesh.Text = ">>>"
        '
        'rrbAll
        '
        Me.rrbAll.Location = New System.Drawing.Point(232, 48)
        Me.rrbAll.Name = "rrbAll"
        Me.rrbAll.Size = New System.Drawing.Size(33, 18)
        Me.rrbAll.TabIndex = 3
        Me.rrbAll.Text = "All"
        '
        'txtCustName
        '
        Me.txtCustName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustName.Location = New System.Drawing.Point(232, 70)
        Me.txtCustName.MaxLength = 50
        Me.txtCustName.MendatroryField = False
        Me.txtCustName.MyLinkLable1 = Nothing
        Me.txtCustName.MyLinkLable2 = Nothing
        Me.txtCustName.Name = "txtCustName"
        Me.txtCustName.Size = New System.Drawing.Size(154, 18)
        Me.txtCustName.TabIndex = 5
        '
        'txtCustomer
        '
        Me.txtCustomer.Location = New System.Drawing.Point(83, 70)
        Me.txtCustomer.MendatroryField = True
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Nothing
        Me.txtCustomer.MyLinkLable2 = Nothing
        Me.txtCustomer.MyReadOnly = False
        Me.txtCustomer.MyShowMasterFormButton = False
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(143, 18)
        Me.txtCustomer.TabIndex = 4
        Me.txtCustomer.Value = ""
        '
        'lblCustomer
        '
        Me.lblCustomer.Location = New System.Drawing.Point(9, 70)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(55, 18)
        Me.lblCustomer.TabIndex = 44
        Me.lblCustomer.Text = "Customer"
        '
        'cboSelectedBy
        '
        Me.cboSelectedBy.AllowShowFocusCues = False
        Me.cboSelectedBy.AutoCompleteDisplayMember = Nothing
        Me.cboSelectedBy.AutoCompleteValueMember = Nothing
        Me.cboSelectedBy.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Text = "Customer"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Item Code"
        RadListDataItem2.TextWrap = True
        Me.cboSelectedBy.Items.Add(RadListDataItem1)
        Me.cboSelectedBy.Items.Add(RadListDataItem2)
        Me.cboSelectedBy.Location = New System.Drawing.Point(83, 46)
        Me.cboSelectedBy.MendatroryField = True
        Me.cboSelectedBy.MyLinkLable1 = Nothing
        Me.cboSelectedBy.MyLinkLable2 = Nothing
        Me.cboSelectedBy.Name = "cboSelectedBy"
        Me.cboSelectedBy.Size = New System.Drawing.Size(143, 20)
        Me.cboSelectedBy.TabIndex = 2
        '
        'lblSelectedBy
        '
        Me.lblSelectedBy.Location = New System.Drawing.Point(9, 46)
        Me.lblSelectedBy.Name = "lblSelectedBy"
        Me.lblSelectedBy.Size = New System.Drawing.Size(64, 18)
        Me.lblSelectedBy.TabIndex = 38
        Me.lblSelectedBy.Text = "Selected By"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(232, 11)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "17-12-2011"
        Me.txtToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'gv1
        '
        Me.gv1.Location = New System.Drawing.Point(249, 17)
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(10, 10)
        Me.gv1.TabIndex = 1
        Me.gv1.Text = "RadGridView1"
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(82, 11)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 0
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "17-12-2011"
        Me.txtFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(167, 13)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel1.TabIndex = 14
        Me.MyLabel1.Text = "To Date"
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(9, 12)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(59, 18)
        Me.MyLabel2.TabIndex = 13
        Me.MyLabel2.Text = "From Date"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(649, 10)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "close"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(734, 502)
        Me.SplitContainer1.SplitterDistance = 454
        Me.SplitContainer1.TabIndex = 43
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.Panel2)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer3.Size = New System.Drawing.Size(734, 454)
        Me.SplitContainer3.SplitterDistance = 29
        Me.SplitContainer3.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.RadMenu1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(734, 29)
        Me.Panel2.TabIndex = 6
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(734, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Setting"
        Me.RadMenuItem2.AccessibleName = "Setting"
        Me.RadMenuItem2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.SaveLayoutbtn, Me.DeleteLaayout})
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Setting"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'SaveLayoutbtn
        '
        Me.SaveLayoutbtn.AccessibleDescription = "Save layout"
        Me.SaveLayoutbtn.AccessibleName = "Save layout"
        Me.SaveLayoutbtn.Name = "SaveLayoutbtn"
        Me.SaveLayoutbtn.Text = "Save Layout"
        Me.SaveLayoutbtn.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'DeleteLaayout
        '
        Me.DeleteLaayout.AccessibleDescription = "Delete Layout"
        Me.DeleteLaayout.AccessibleName = "Delete Layout"
        Me.DeleteLaayout.Name = "DeleteLaayout"
        Me.DeleteLaayout.Text = "Delete Layout"
        Me.DeleteLaayout.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Size = New System.Drawing.Size(734, 421)
        Me.SplitContainer2.SplitterDistance = 101
        Me.SplitContainer2.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        '
        '
        '
        Me.RadPageView1.RootElement.AccessibleDescription = "Report1"
        Me.RadPageView1.RootElement.AccessibleName = "Report1"
        Me.RadPageView1.RootElement.Text = "Report1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(734, 316)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.TabStop = False
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(2), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Summary"
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(3), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Summary"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.gvCustomer)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(63.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(713, 268)
        Me.RadPageViewPage1.Text = "Summary"
        '
        'gvCustomer
        '
        Me.gvCustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCustomer.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvCustomer.ForeColor = System.Drawing.Color.Black
        Me.gvCustomer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvCustomer.Location = New System.Drawing.Point(0, 0)
        '
        'gvCustomer
        '
        Me.gvCustomer.MasterTemplate.AllowAddNewRow = False
        Me.gvCustomer.MasterTemplate.AllowDeleteRow = False
        Me.gvCustomer.MasterTemplate.EnableFiltering = True
        Me.gvCustomer.Name = "gvCustomer"
        Me.gvCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvCustomer.ShowGroupPanel = False
        Me.gvCustomer.Size = New System.Drawing.Size(713, 268)
        Me.gvCustomer.TabIndex = 1
        Me.gvCustomer.TabStop = False
        Me.gvCustomer.Text = "RadGridView1"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gvDetails)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(713, 268)
        Me.RadPageViewPage2.Text = "Details"
        '
        'gvDetails
        '
        Me.gvDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvDetails.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDetails.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gvDetails.ForeColor = System.Drawing.Color.Black
        Me.gvDetails.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvDetails.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvDetails.MasterTemplate.AllowAddNewRow = False
        Me.gvDetails.MasterTemplate.AllowDeleteRow = False
        Me.gvDetails.MasterTemplate.EnableFiltering = True
        Me.gvDetails.Name = "gvDetails"
        Me.gvDetails.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvDetails.ShowGroupPanel = False
        Me.gvDetails.Size = New System.Drawing.Size(713, 268)
        Me.gvDetails.TabIndex = 3
        Me.gvDetails.Text = "RadGridView1"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(21, 10)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(69, 22)
        Me.btnReset.TabIndex = 0
        Me.btnReset.Text = "Reset"
        '
        'FrmSaleHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 502)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MinimumSize = New System.Drawing.Size(534, 378)
        Me.Name = "FrmSaleHistory"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Sale History"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rrbPosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReferesh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rrbAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboSelectedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSelectedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.gvCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gvDetails.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblSelectedBy As common.Controls.MyLabel
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents cboSelectedBy As common.Controls.MyComboBox
    Friend WithEvents txtCustomer As common.UserControls.txtFinder
    Friend WithEvents btnReferesh As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCustName As common.Controls.MyTextBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents gvCustomer As common.UserControls.MyRadGridView
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents rrbPosted As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rrbAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvDetails As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SaveLayoutbtn As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents DeleteLaayout As Telerik.WinControls.UI.RadMenuItem
End Class

