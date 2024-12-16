<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class rptFarmerSaleRegister
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim TableViewDefinition6 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.lblDCS = New common.Controls.MyLabel()
        Me.TxtDCS = New common.UserControls.txtMultiSelectFinder()
        Me.lblBMC = New common.Controls.MyLabel()
        Me.txtBMC = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbItemWise = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbDCSWise = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbBMCWise = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbDetail = New Telerik.WinControls.UI.RadRadioButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnGO = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv11 = New Telerik.WinControls.UI.RadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDCS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBMC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rdbItemWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbDCSWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbBMCWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv11.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Date Range"
        Me.RadGroupBox3.Location = New System.Drawing.Point(15, 12)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(246, 42)
        Me.RadGroupBox3.TabIndex = 54
        Me.RadGroupBox3.Text = "Date Range"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(130, 16)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(5, 16)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'ToDate
        '
        Me.ToDate.CustomFormat = "dd/MM/yyyy"
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(157, 15)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(78, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "11/12/2024"
        Me.ToDate.Value = New Date(2024, 12, 11, 0, 0, 0, 0)
        '
        'fromDate
        '
        Me.fromDate.CustomFormat = "dd/MM/yyyy"
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(44, 15)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(78, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "11/12/2024"
        Me.fromDate.Value = New Date(2024, 12, 11, 0, 0, 0, 0)
        '
        'lblDCS
        '
        Me.lblDCS.FieldName = Nothing
        Me.lblDCS.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDCS.Location = New System.Drawing.Point(16, 85)
        Me.lblDCS.Name = "lblDCS"
        Me.lblDCS.Size = New System.Drawing.Size(27, 18)
        Me.lblDCS.TabIndex = 364
        Me.lblDCS.Text = "DCS"
        '
        'TxtDCS
        '
        Me.TxtDCS.arrDispalyMember = Nothing
        Me.TxtDCS.arrValueMember = Nothing
        Me.TxtDCS.Location = New System.Drawing.Point(71, 84)
        Me.TxtDCS.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDCS.MyLinkLable1 = Me.lblDCS
        Me.TxtDCS.MyLinkLable2 = Nothing
        Me.TxtDCS.MyNullText = "All"
        Me.TxtDCS.Name = "TxtDCS"
        Me.TxtDCS.Size = New System.Drawing.Size(252, 19)
        Me.TxtDCS.TabIndex = 363
        '
        'lblBMC
        '
        Me.lblBMC.FieldName = Nothing
        Me.lblBMC.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBMC.Location = New System.Drawing.Point(16, 61)
        Me.lblBMC.Name = "lblBMC"
        Me.lblBMC.Size = New System.Drawing.Size(30, 18)
        Me.lblBMC.TabIndex = 362
        Me.lblBMC.Text = "BMC"
        '
        'txtBMC
        '
        Me.txtBMC.arrDispalyMember = Nothing
        Me.txtBMC.arrValueMember = Nothing
        Me.txtBMC.Location = New System.Drawing.Point(71, 60)
        Me.txtBMC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMC.MyLinkLable1 = Me.lblBMC
        Me.txtBMC.MyLinkLable2 = Nothing
        Me.txtBMC.MyNullText = "All"
        Me.txtBMC.Name = "txtBMC"
        Me.txtBMC.Size = New System.Drawing.Size(252, 19)
        Me.txtBMC.TabIndex = 361
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rdbItemWise)
        Me.RadGroupBox1.Controls.Add(Me.rdbDCSWise)
        Me.RadGroupBox1.Controls.Add(Me.rdbBMCWise)
        Me.RadGroupBox1.Controls.Add(Me.rdbDetail)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(20, 118)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(303, 37)
        Me.RadGroupBox1.TabIndex = 365
        '
        'rdbItemWise
        '
        Me.rdbItemWise.Location = New System.Drawing.Point(219, 9)
        Me.rdbItemWise.Name = "rdbItemWise"
        Me.rdbItemWise.Size = New System.Drawing.Size(70, 18)
        Me.rdbItemWise.TabIndex = 308
        Me.rdbItemWise.TabStop = False
        Me.rdbItemWise.Text = "Item Wise"
        '
        'rdbDCSWise
        '
        Me.rdbDCSWise.Location = New System.Drawing.Point(145, 9)
        Me.rdbDCSWise.Name = "rdbDCSWise"
        Me.rdbDCSWise.Size = New System.Drawing.Size(68, 18)
        Me.rdbDCSWise.TabIndex = 307
        Me.rdbDCSWise.TabStop = False
        Me.rdbDCSWise.Text = "DCS Wise"
        '
        'rdbBMCWise
        '
        Me.rdbBMCWise.Location = New System.Drawing.Point(68, 9)
        Me.rdbBMCWise.Name = "rdbBMCWise"
        Me.rdbBMCWise.Size = New System.Drawing.Size(71, 18)
        Me.rdbBMCWise.TabIndex = 306
        Me.rdbBMCWise.TabStop = False
        Me.rdbBMCWise.Text = "BMC Wise"
        '
        'rdbDetail
        '
        Me.rdbDetail.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbDetail.Location = New System.Drawing.Point(13, 9)
        Me.rdbDetail.Name = "rdbDetail"
        Me.rdbDetail.Size = New System.Drawing.Size(49, 18)
        Me.rdbDetail.TabIndex = 307
        Me.rdbDetail.Text = "Detail"
        Me.rdbDetail.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExcel, Me.rmPDF})
        Me.btnExport.Location = New System.Drawing.Point(160, 5)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(80, 20)
        Me.btnExport.TabIndex = 368
        Me.btnExport.Text = "Export"
        '
        'rmExcel
        '
        Me.rmExcel.Name = "rmExcel"
        Me.rmExcel.Text = "Excel"
        Me.rmExcel.UseCompatibleTextRendering = False
        '
        'rmPDF
        '
        Me.rmPDF.Name = "rmPDF"
        Me.rmPDF.Text = "PDF"
        Me.rmPDF.UseCompatibleTextRendering = False
        '
        'btnGO
        '
        Me.btnGO.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGO.Location = New System.Drawing.Point(12, 5)
        Me.btnGO.Name = "btnGO"
        Me.btnGO.Size = New System.Drawing.Size(68, 20)
        Me.btnGO.TabIndex = 367
        Me.btnGO.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(86, 5)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 20)
        Me.btnReset.TabIndex = 366
        Me.btnReset.Text = "Reset"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGO)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(844, 441)
        Me.SplitContainer1.SplitterDistance = 407
        Me.SplitContainer1.TabIndex = 369
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(844, 407)
        Me.RadPageView1.TabIndex = 366
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.lblDCS)
        Me.RadPageViewPage1.Controls.Add(Me.txtBMC)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.lblBMC)
        Me.RadPageViewPage1.Controls.Add(Me.TxtDCS)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(823, 359)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv11)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(823, 359)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gv11
        '
        Me.Gv11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv11.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.Gv11.MasterTemplate.ViewDefinition = TableViewDefinition6
        Me.Gv11.Name = "Gv11"
        Me.Gv11.Size = New System.Drawing.Size(823, 359)
        Me.Gv11.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(764, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 366
        Me.btnClose.Text = "Close"
        '
        'rptFarmerSaleRegister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(844, 441)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "rptFarmerSaleRegister"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "rptFarmerSaleRegister"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDCS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBMC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rdbItemWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbDCSWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbBMCWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv11.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As RadDateTimePicker
    Friend WithEvents fromDate As RadDateTimePicker
    Friend WithEvents lblDCS As common.Controls.MyLabel
    Friend WithEvents TxtDCS As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblBMC As common.Controls.MyLabel
    Friend WithEvents txtBMC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents rdbBMCWise As RadRadioButton
    Friend WithEvents rdbDetail As RadRadioButton
    Friend WithEvents rdbDCSWise As RadRadioButton
    Friend WithEvents rdbItemWise As RadRadioButton
    Friend WithEvents btnExport As RadSplitButton
    Friend WithEvents rmExcel As RadMenuItem
    Friend WithEvents rmPDF As RadMenuItem
    Friend WithEvents btnGO As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents ddlReportType As RadDropDownList
    Friend WithEvents btnClose As RadButton
    Friend WithEvents Gv11 As RadGridView
End Class
