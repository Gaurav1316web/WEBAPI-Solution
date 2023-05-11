<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCompleteTransfer
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.cbgTransferNo = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        Me.btnRefreshTransfer = New Telerik.WinControls.UI.RadButton
        Me.chkTransferWithMismatchZero = New common.Controls.MyCheckBox
        Me.btnShow = New Telerik.WinControls.UI.RadButton
        Me.txtToDate = New common.Controls.MyDateTimePicker
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtFromDate = New common.Controls.MyDateTimePicker
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.RadLabel12 = New common.Controls.MyLabel
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        Me.gv2 = New common.UserControls.MyRadGridView
        Me.btnUnselectAll = New Telerik.WinControls.UI.RadButton
        Me.btnSelectAll = New Telerik.WinControls.UI.RadButton
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gvPostedInvoice = New common.UserControls.MyRadGridView
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnPostShipment = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefreshTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTransferWithMismatchZero, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUnselectAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelectAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gvPostedInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPostedInvoice.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPostShipment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPostShipment)
        Me.SplitContainer1.Size = New System.Drawing.Size(863, 484)
        Me.SplitContainer1.SplitterDistance = 449
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(863, 449)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "Filter"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemContentOrientation = Telerik.WinControls.UI.PageViewContentOrientation.[Auto]
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(842, 401)
        Me.RadPageViewPage1.Text = "Transfers to Reconciliation"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.cbgTransferNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer2.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Panel2)
        Me.SplitContainer2.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SplitContainer2.Size = New System.Drawing.Size(842, 401)
        Me.SplitContainer2.SplitterDistance = 350
        Me.SplitContainer2.TabIndex = 0
        '
        'cbgTransferNo
        '
        Me.cbgTransferNo.CheckedValue = Nothing
        Me.cbgTransferNo.DataSource = Nothing
        Me.cbgTransferNo.DisplayMember = "Name"
        Me.cbgTransferNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgTransferNo.Location = New System.Drawing.Point(0, 47)
        Me.cbgTransferNo.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.None
        Me.cbgTransferNo.MyShowHeadrText = True
        Me.cbgTransferNo.Name = "cbgTransferNo"
        Me.cbgTransferNo.Size = New System.Drawing.Size(842, 303)
        Me.cbgTransferNo.TabIndex = 0
        Me.cbgTransferNo.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadButton1)
        Me.Panel1.Controls.Add(Me.btnRefreshTransfer)
        Me.Panel1.Controls.Add(Me.chkTransferWithMismatchZero)
        Me.Panel1.Controls.Add(Me.btnShow)
        Me.Panel1.Controls.Add(Me.txtToDate)
        Me.Panel1.Controls.Add(Me.txtFromDate)
        Me.Panel1.Controls.Add(Me.MyLabel1)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(842, 47)
        Me.Panel1.TabIndex = 0
        '
        'RadButton1
        '
        Me.RadButton1.Location = New System.Drawing.Point(531, 25)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(224, 20)
        Me.RadButton1.TabIndex = 5
        Me.RadButton1.Text = "Populate Invoice with mismatch < 15"
        '
        'btnRefreshTransfer
        '
        Me.btnRefreshTransfer.Location = New System.Drawing.Point(531, 3)
        Me.btnRefreshTransfer.Name = "btnRefreshTransfer"
        Me.btnRefreshTransfer.Size = New System.Drawing.Size(101, 20)
        Me.btnRefreshTransfer.TabIndex = 3
        Me.btnRefreshTransfer.Text = "Populate Transfer"
        '
        'chkTransferWithMismatchZero
        '
        Me.chkTransferWithMismatchZero.Location = New System.Drawing.Point(319, 4)
        Me.chkTransferWithMismatchZero.MyLinkLable1 = Nothing
        Me.chkTransferWithMismatchZero.MyLinkLable2 = Nothing
        Me.chkTransferWithMismatchZero.Name = "chkTransferWithMismatchZero"
        Me.chkTransferWithMismatchZero.Size = New System.Drawing.Size(206, 18)
        Me.chkTransferWithMismatchZero.TabIndex = 2
        Me.chkTransferWithMismatchZero.Text = "Show Transfer with No Mismatch Qty"
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(633, 3)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(122, 20)
        Me.btnShow.TabIndex = 4
        Me.btnShow.Text = "Populate All Invoices"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.txtToDate.Location = New System.Drawing.Point(228, 3)
        Me.txtToDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.MyLabel2
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(85, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.Text = "MyDateTimePicker2"
        Me.txtToDate.Value = New Date(2012, 6, 19, 14, 37, 40, 640)
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(176, 4)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel2.TabIndex = 2
        Me.MyLabel2.Text = "To Date"
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.txtFromDate.Location = New System.Drawing.Point(72, 3)
        Me.txtFromDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.MyLabel1
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(85, 20)
        Me.txtFromDate.TabIndex = 0
        Me.txtFromDate.Text = "MyDateTimePicker1"
        Me.txtFromDate.Value = New Date(2012, 6, 19, 14, 37, 40, 640)
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(7, 4)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(59, 18)
        Me.MyLabel1.TabIndex = 0
        Me.MyLabel1.Text = "From Date"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(842, 26)
        Me.gv1.TabIndex = 0
        Me.gv1.Text = "RadGridView1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.RadLabel12)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 26)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(842, 21)
        Me.Panel2.TabIndex = 0
        '
        'RadLabel12
        '
        Me.RadLabel12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel12.Location = New System.Drawing.Point(683, 2)
        Me.RadLabel12.Name = "RadLabel12"
        '
        '
        '
        Me.RadLabel12.RootElement.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel12.Size = New System.Drawing.Size(158, 16)
        Me.RadLabel12.TabIndex = 0
        Me.RadLabel12.Text = "Double click to show details"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.SplitContainer3)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(629, 401)
        Me.RadPageViewPage2.Text = "Invoices To be Posted"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.gv2)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnUnselectAll)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnSelectAll)
        Me.SplitContainer3.Size = New System.Drawing.Size(629, 401)
        Me.SplitContainer3.SplitterDistance = 372
        Me.SplitContainer3.TabIndex = 0
        '
        'gv2
        '
        Me.gv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv2.Location = New System.Drawing.Point(0, 0)
        Me.gv2.Name = "gv2"
        Me.gv2.Size = New System.Drawing.Size(629, 372)
        Me.gv2.TabIndex = 0
        Me.gv2.Text = "RadGridView1"
        '
        'btnUnselectAll
        '
        Me.btnUnselectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUnselectAll.Location = New System.Drawing.Point(91, 2)
        Me.btnUnselectAll.Name = "btnUnselectAll"
        Me.btnUnselectAll.Size = New System.Drawing.Size(82, 20)
        Me.btnUnselectAll.TabIndex = 2
        Me.btnUnselectAll.Text = "Unselect All"
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelectAll.Location = New System.Drawing.Point(3, 2)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(82, 20)
        Me.btnSelectAll.TabIndex = 1
        Me.btnSelectAll.Text = "Select All"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gvPostedInvoice)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(629, 401)
        Me.RadPageViewPage3.Text = "Posted Invoice"
        '
        'gvPostedInvoice
        '
        Me.gvPostedInvoice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvPostedInvoice.Location = New System.Drawing.Point(0, 0)
        Me.gvPostedInvoice.Name = "gvPostedInvoice"
        Me.gvPostedInvoice.Size = New System.Drawing.Size(629, 401)
        Me.gvPostedInvoice.TabIndex = 1
        Me.gvPostedInvoice.Text = "RadGridView1"
        '
        'MyLabel3
        '
        Me.MyLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.ForeColor = System.Drawing.SystemColors.Highlight
        Me.MyLabel3.Location = New System.Drawing.Point(556, 8)
        Me.MyLabel3.Name = "MyLabel3"
        '
        '
        '
        Me.MyLabel3.RootElement.ForeColor = System.Drawing.SystemColors.Highlight
        Me.MyLabel3.Size = New System.Drawing.Size(206, 16)
        Me.MyLabel3.TabIndex = 1
        Me.MyLabel3.Text = "Post maximum 100 invoices at a time"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(775, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(82, 20)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnPostShipment
        '
        Me.btnPostShipment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPostShipment.Location = New System.Drawing.Point(4, 6)
        Me.btnPostShipment.Name = "btnPostShipment"
        Me.btnPostShipment.Size = New System.Drawing.Size(82, 20)
        Me.btnPostShipment.TabIndex = 0
        Me.btnPostShipment.Text = "Post Invoices"
        '
        'FrmCompleteTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(863, 484)
        Me.Controls.Add(Me.SplitContainer1)
        Me.KeyPreview = True
        Me.Name = "FrmCompleteTransfer"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Loadout -Sale Invoice Reconciliation"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefreshTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTransferWithMismatchZero, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUnselectAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelectAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gvPostedInvoice.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPostedInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPostShipment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents cbgTransferNo As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents btnPostShipment As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents chkTransferWithMismatchZero As common.Controls.MyCheckBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnUnselectAll As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSelectAll As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvPostedInvoice As common.UserControls.MyRadGridView
    Friend WithEvents btnRefreshTransfer As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnShow As Telerik.WinControls.UI.RadButton
End Class

