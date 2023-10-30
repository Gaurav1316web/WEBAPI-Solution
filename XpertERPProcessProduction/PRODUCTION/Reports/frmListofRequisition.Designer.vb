<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmListofRequisition
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
        Me.dtpFromdate = New Telerik.WinControls.UI.RadGroupBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkDetail = New Telerik.WinControls.UI.RadRadioButton
        Me.chkSum = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox13 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgBatchOrder = New common.MyCheckBoxGrid
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.chkBOSelect = New common.Controls.MyRadioButton
        Me.chkBOAll = New common.Controls.MyRadioButton
        Me.dtpToDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.dtpFromdate1 = New Telerik.WinControls.UI.RadDateTimePicker
        Me.grpbxItemWise = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkLocationSelect = New common.Controls.MyRadioButton
        Me.chkLocationAll = New common.Controls.MyRadioButton
        Me.lblToDate = New common.Controls.MyLabel
        Me.lblFromDate = New common.Controls.MyLabel
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv = New common.UserControls.MyRadGridView
        CType(Me.dtpFromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dtpFromdate.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox13.SuspendLayout()
        Me.Panel9.SuspendLayout()
        CType(Me.chkBOSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBOAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpbxItemWise, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpbxItemWise.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtpFromdate
        '
        Me.dtpFromdate.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.dtpFromdate.Controls.Add(Me.Panel1)
        Me.dtpFromdate.Controls.Add(Me.RadGroupBox13)
        Me.dtpFromdate.Controls.Add(Me.dtpToDate)
        Me.dtpFromdate.Controls.Add(Me.dtpFromdate1)
        Me.dtpFromdate.Controls.Add(Me.grpbxItemWise)
        Me.dtpFromdate.Controls.Add(Me.lblToDate)
        Me.dtpFromdate.Controls.Add(Me.lblFromDate)
        Me.dtpFromdate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpFromdate.HeaderText = ""
        Me.dtpFromdate.Location = New System.Drawing.Point(0, 0)
        Me.dtpFromdate.Name = "dtpFromdate"
        Me.dtpFromdate.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.dtpFromdate.Size = New System.Drawing.Size(750, 461)
        Me.dtpFromdate.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.chkDetail)
        Me.Panel1.Controls.Add(Me.chkSum)
        Me.Panel1.Location = New System.Drawing.Point(373, 6)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(355, 21)
        Me.Panel1.TabIndex = 223
        '
        'chkDetail
        '
        Me.chkDetail.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkDetail.Location = New System.Drawing.Point(173, 1)
        Me.chkDetail.Name = "chkDetail"
        Me.chkDetail.Size = New System.Drawing.Size(62, 18)
        Me.chkDetail.TabIndex = 1
        Me.chkDetail.Text = "Detailed"
        '
        'chkSum
        '
        Me.chkSum.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkSum.Location = New System.Drawing.Point(87, 1)
        Me.chkSum.Name = "chkSum"
        Me.chkSum.Size = New System.Drawing.Size(76, 18)
        Me.chkSum.TabIndex = 0
        Me.chkSum.TabStop = True
        Me.chkSum.Text = "Summarize"
        Me.chkSum.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox13
        '
        Me.RadGroupBox13.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox13.Controls.Add(Me.cbgBatchOrder)
        Me.RadGroupBox13.Controls.Add(Me.Panel9)
        Me.RadGroupBox13.HeaderText = "Batch Order"
        Me.RadGroupBox13.Location = New System.Drawing.Point(12, 33)
        Me.RadGroupBox13.Name = "RadGroupBox13"
        Me.RadGroupBox13.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox13.Size = New System.Drawing.Size(355, 223)
        Me.RadGroupBox13.TabIndex = 77
        Me.RadGroupBox13.Text = "Batch Order"
        '
        'cbgBatchOrder
        '
        Me.cbgBatchOrder.AccessibleDescription = "cbgLocation"
        Me.cbgBatchOrder.AccessibleName = ""
        Me.cbgBatchOrder.CheckedValue = Nothing
        Me.cbgBatchOrder.DataSource = Nothing
        Me.cbgBatchOrder.DisplayMember = "Name"
        Me.cbgBatchOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgBatchOrder.Location = New System.Drawing.Point(10, 45)
        Me.cbgBatchOrder.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgBatchOrder.MyShowHeadrText = False
        Me.cbgBatchOrder.Name = "cbgBatchOrder"
        Me.cbgBatchOrder.Size = New System.Drawing.Size(335, 168)
        Me.cbgBatchOrder.TabIndex = 1
        Me.cbgBatchOrder.ValueMember = "Code"
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.chkBOSelect)
        Me.Panel9.Controls.Add(Me.chkBOAll)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(10, 20)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(335, 25)
        Me.Panel9.TabIndex = 0
        '
        'chkBOSelect
        '
        Me.chkBOSelect.AccessibleDescription = "chkDoc_select"
        Me.chkBOSelect.Location = New System.Drawing.Point(173, 3)
        Me.chkBOSelect.MyLinkLable1 = Nothing
        Me.chkBOSelect.MyLinkLable2 = Nothing
        Me.chkBOSelect.Name = "chkBOSelect"
        Me.chkBOSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkBOSelect.TabIndex = 1
        Me.chkBOSelect.Text = "Select"
        '
        'chkBOAll
        '
        Me.chkBOAll.AccessibleDescription = "chkdocAll"
        Me.chkBOAll.Location = New System.Drawing.Point(113, 4)
        Me.chkBOAll.MyLinkLable1 = Nothing
        Me.chkBOAll.MyLinkLable2 = Nothing
        Me.chkBOAll.Name = "chkBOAll"
        Me.chkBOAll.Size = New System.Drawing.Size(33, 18)
        Me.chkBOAll.TabIndex = 0
        Me.chkBOAll.Text = "All"
        '
        'dtpToDate
        '
        Me.dtpToDate.AccessibleName = "dtpToDate"
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(219, 6)
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(84, 20)
        Me.dtpToDate.TabIndex = 21
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "01/02/2012"
        Me.dtpToDate.Value = New Date(2012, 2, 1, 0, 0, 0, 0)
        '
        'dtpFromdate1
        '
        Me.dtpFromdate1.AccessibleName = "dtpFromDate"
        Me.dtpFromdate1.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromdate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromdate1.Location = New System.Drawing.Point(75, 6)
        Me.dtpFromdate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.Name = "dtpFromdate1"
        Me.dtpFromdate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.Size = New System.Drawing.Size(88, 20)
        Me.dtpFromdate1.TabIndex = 20
        Me.dtpFromdate1.TabStop = False
        Me.dtpFromdate1.Text = "01/02/2012"
        Me.dtpFromdate1.Value = New Date(2012, 2, 1, 0, 0, 0, 0)
        '
        'grpbxItemWise
        '
        Me.grpbxItemWise.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpbxItemWise.Controls.Add(Me.cbgLocation)
        Me.grpbxItemWise.Controls.Add(Me.Panel2)
        Me.grpbxItemWise.HeaderText = "Location"
        Me.grpbxItemWise.Location = New System.Drawing.Point(373, 33)
        Me.grpbxItemWise.Name = "grpbxItemWise"
        Me.grpbxItemWise.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpbxItemWise.Size = New System.Drawing.Size(355, 223)
        Me.grpbxItemWise.TabIndex = 15
        Me.grpbxItemWise.Text = "Location"
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
        Me.cbgLocation.Size = New System.Drawing.Size(335, 173)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkLocationSelect)
        Me.Panel2.Controls.Add(Me.chkLocationAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(335, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.Location = New System.Drawing.Point(173, 1)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.Location = New System.Drawing.Point(113, 1)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'lblToDate
        '
        Me.lblToDate.Location = New System.Drawing.Point(169, 8)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 2
        Me.lblToDate.Text = "To Date"
        '
        'lblFromDate
        '
        Me.lblFromDate.Location = New System.Drawing.Point(13, 6)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblFromDate.TabIndex = 1
        Me.lblFromDate.Text = "From Date"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF})
        Me.btnExport.Location = New System.Drawing.Point(170, 516)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(86, 24)
        Me.btnExport.TabIndex = 80
        Me.btnExport.Text = "Export"
        '
        'btnExcel
        '
        Me.btnExcel.AccessibleDescription = "Excel"
        Me.btnExcel.AccessibleName = "Excel"
        Me.btnExcel.Image = Global.XpertERPProcessProduction.My.Resources.Resources.MSE
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnPDF
        '
        Me.btnPDF.AccessibleDescription = "PDF"
        Me.btnPDF.AccessibleName = "PDF"
        Me.btnPDF.Image = Global.XpertERPProcessProduction.My.Resources.Resources.pdf
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "PDF"
        Me.btnPDF.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(88, 515)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(76, 24)
        Me.btnreset.TabIndex = 19
        Me.btnreset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(683, 514)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 24)
        Me.btnClose.TabIndex = 18
        Me.btnClose.Text = "Close"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(6, 514)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(76, 24)
        Me.btnRefresh.TabIndex = 17
        Me.btnRefresh.Text = ">>>"
        '
        'RadPageView1
        '
        Me.RadPageView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(771, 509)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.dtpFromdate)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(750, 461)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(750, 535)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(0, 0)
        Me.gv.Name = "gv"
        Me.gv.Size = New System.Drawing.Size(750, 535)
        Me.gv.TabIndex = 0
        Me.gv.Text = "RadGridView1"
        '
        'frmListofRequisition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(771, 550)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.RadPageView1)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnreset)
        Me.Name = "frmListofRequisition"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "List of Requisition"
        CType(Me.dtpFromdate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dtpFromdate.ResumeLayout(False)
        Me.dtpFromdate.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox13, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox13.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        CType(Me.chkBOSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBOAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpbxItemWise, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpbxItemWise.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dtpFromdate As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents grpbxItemWise As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtpFromdate1 As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadGroupBox13 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgBatchOrder As common.MyCheckBoxGrid
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents chkBOSelect As common.Controls.MyRadioButton
    Friend WithEvents chkBOAll As common.Controls.MyRadioButton
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblFromDate As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkSum As Telerik.WinControls.UI.RadRadioButton
End Class

