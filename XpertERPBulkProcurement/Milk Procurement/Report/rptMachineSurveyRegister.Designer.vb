<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class rptMachineSurveyRegister
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtMultWeighingMachine = New common.UserControls.txtMultiSelectFinder()
        Me.lblUnion = New common.Controls.MyLabel()
        Me.lblMilkAnalyzer = New common.Controls.MyLabel()
        Me.txtMultMilkAnalyzer = New common.UserControls.txtMultiSelectFinder()
        Me.txtMultZone = New common.UserControls.txtMultiSelectFinder()
        Me.txtMultUnion = New common.UserControls.txtMultiSelectFinder()
        Me.lblWeighmentMachine = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtMultDCS = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtMultBMC = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnUnionWise = New System.Windows.Forms.RadioButton()
        Me.rbtnMachineWise = New System.Windows.Forms.RadioButton()
        Me.rbtnDetail = New System.Windows.Forms.RadioButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btngo = New Telerik.WinControls.UI.RadButton()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMilkAnalyzer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWeighmentMachine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btngo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 405
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.DefaultPage = Me.RadPageViewPage1
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(800, 405)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 357)
        Me.RadPageViewPage1.Text = "Details"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtMultWeighingMachine)
        Me.RadGroupBox1.Controls.Add(Me.lblMilkAnalyzer)
        Me.RadGroupBox1.Controls.Add(Me.txtMultMilkAnalyzer)
        Me.RadGroupBox1.Controls.Add(Me.txtMultZone)
        Me.RadGroupBox1.Controls.Add(Me.txtMultUnion)
        Me.RadGroupBox1.Controls.Add(Me.lblWeighmentMachine)
        Me.RadGroupBox1.Controls.Add(Me.lblUnion)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtMultDCS)
        Me.RadGroupBox1.Controls.Add(Me.txtMultBMC)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(10, 6)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(405, 145)
        Me.RadGroupBox1.TabIndex = 464
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(9, 11)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel3.TabIndex = 451
        Me.MyLabel3.Text = "Zone"
        '
        'txtMultWeighingMachine
        '
        Me.txtMultWeighingMachine.arrDispalyMember = Nothing
        Me.txtMultWeighingMachine.arrValueMember = Nothing
        Me.txtMultWeighingMachine.Location = New System.Drawing.Point(121, 119)
        Me.txtMultWeighingMachine.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultWeighingMachine.MyLinkLable1 = Me.lblUnion
        Me.txtMultWeighingMachine.MyLinkLable2 = Nothing
        Me.txtMultWeighingMachine.MyNullText = "All"
        Me.txtMultWeighingMachine.Name = "txtMultWeighingMachine"
        Me.txtMultWeighingMachine.Size = New System.Drawing.Size(276, 19)
        Me.txtMultWeighingMachine.TabIndex = 463
        '
        'lblUnion
        '
        Me.lblUnion.FieldName = Nothing
        Me.lblUnion.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblUnion.Location = New System.Drawing.Point(9, 78)
        Me.lblUnion.Name = "lblUnion"
        Me.lblUnion.Size = New System.Drawing.Size(36, 16)
        Me.lblUnion.TabIndex = 460
        Me.lblUnion.Text = "Union"
        '
        'lblMilkAnalyzer
        '
        Me.lblMilkAnalyzer.FieldName = Nothing
        Me.lblMilkAnalyzer.Location = New System.Drawing.Point(9, 97)
        Me.lblMilkAnalyzer.Name = "lblMilkAnalyzer"
        Me.lblMilkAnalyzer.Size = New System.Drawing.Size(73, 18)
        Me.lblMilkAnalyzer.TabIndex = 449
        Me.lblMilkAnalyzer.Text = "Milk Analyzer"
        '
        'txtMultMilkAnalyzer
        '
        Me.txtMultMilkAnalyzer.arrDispalyMember = Nothing
        Me.txtMultMilkAnalyzer.arrValueMember = Nothing
        Me.txtMultMilkAnalyzer.Location = New System.Drawing.Point(121, 97)
        Me.txtMultMilkAnalyzer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultMilkAnalyzer.MyLinkLable1 = Me.lblUnion
        Me.txtMultMilkAnalyzer.MyLinkLable2 = Nothing
        Me.txtMultMilkAnalyzer.MyNullText = "All"
        Me.txtMultMilkAnalyzer.Name = "txtMultMilkAnalyzer"
        Me.txtMultMilkAnalyzer.Size = New System.Drawing.Size(276, 19)
        Me.txtMultMilkAnalyzer.TabIndex = 462
        '
        'txtMultZone
        '
        Me.txtMultZone.arrDispalyMember = Nothing
        Me.txtMultZone.arrValueMember = Nothing
        Me.txtMultZone.Location = New System.Drawing.Point(121, 8)
        Me.txtMultZone.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultZone.MyLinkLable1 = Me.MyLabel3
        Me.txtMultZone.MyLinkLable2 = Nothing
        Me.txtMultZone.MyNullText = "All"
        Me.txtMultZone.Name = "txtMultZone"
        Me.txtMultZone.Size = New System.Drawing.Size(276, 19)
        Me.txtMultZone.TabIndex = 452
        '
        'txtMultUnion
        '
        Me.txtMultUnion.arrDispalyMember = Nothing
        Me.txtMultUnion.arrValueMember = Nothing
        Me.txtMultUnion.Location = New System.Drawing.Point(121, 75)
        Me.txtMultUnion.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultUnion.MyLinkLable1 = Me.lblUnion
        Me.txtMultUnion.MyLinkLable2 = Nothing
        Me.txtMultUnion.MyNullText = "All"
        Me.txtMultUnion.Name = "txtMultUnion"
        Me.txtMultUnion.Size = New System.Drawing.Size(276, 19)
        Me.txtMultUnion.TabIndex = 461
        '
        'lblWeighmentMachine
        '
        Me.lblWeighmentMachine.FieldName = Nothing
        Me.lblWeighmentMachine.Location = New System.Drawing.Point(9, 118)
        Me.lblWeighmentMachine.Name = "lblWeighmentMachine"
        Me.lblWeighmentMachine.Size = New System.Drawing.Size(83, 18)
        Me.lblWeighmentMachine.TabIndex = 454
        Me.lblWeighmentMachine.Text = "Weighing Scale"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(9, 34)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel1.TabIndex = 456
        Me.MyLabel1.Text = "BMC"
        '
        'txtMultDCS
        '
        Me.txtMultDCS.arrDispalyMember = Nothing
        Me.txtMultDCS.arrValueMember = Nothing
        Me.txtMultDCS.Location = New System.Drawing.Point(121, 53)
        Me.txtMultDCS.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultDCS.MyLinkLable1 = Me.MyLabel2
        Me.txtMultDCS.MyLinkLable2 = Nothing
        Me.txtMultDCS.MyNullText = "All"
        Me.txtMultDCS.Name = "txtMultDCS"
        Me.txtMultDCS.Size = New System.Drawing.Size(276, 19)
        Me.txtMultDCS.TabIndex = 459
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(9, 56)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel2.TabIndex = 458
        Me.MyLabel2.Text = "DCS"
        '
        'txtMultBMC
        '
        Me.txtMultBMC.arrDispalyMember = Nothing
        Me.txtMultBMC.arrValueMember = Nothing
        Me.txtMultBMC.Location = New System.Drawing.Point(121, 31)
        Me.txtMultBMC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultBMC.MyLinkLable1 = Me.MyLabel1
        Me.txtMultBMC.MyLinkLable2 = Nothing
        Me.txtMultBMC.MyNullText = "All"
        Me.txtMultBMC.Name = "txtMultBMC"
        Me.txtMultBMC.Size = New System.Drawing.Size(276, 19)
        Me.txtMultBMC.TabIndex = 457
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rbtnUnionWise)
        Me.RadGroupBox2.Controls.Add(Me.rbtnMachineWise)
        Me.RadGroupBox2.Controls.Add(Me.rbtnDetail)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(421, 6)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(291, 29)
        Me.RadGroupBox2.TabIndex = 453
        '
        'rbtnUnionWise
        '
        Me.rbtnUnionWise.AutoSize = True
        Me.rbtnUnionWise.Location = New System.Drawing.Point(194, 6)
        Me.rbtnUnionWise.Name = "rbtnUnionWise"
        Me.rbtnUnionWise.Size = New System.Drawing.Size(85, 17)
        Me.rbtnUnionWise.TabIndex = 454
        Me.rbtnUnionWise.Text = "Union Wise"
        Me.rbtnUnionWise.UseVisualStyleBackColor = True
        '
        'rbtnMachineWise
        '
        Me.rbtnMachineWise.AutoSize = True
        Me.rbtnMachineWise.Location = New System.Drawing.Point(91, 6)
        Me.rbtnMachineWise.Name = "rbtnMachineWise"
        Me.rbtnMachineWise.Size = New System.Drawing.Size(97, 17)
        Me.rbtnMachineWise.TabIndex = 11
        Me.rbtnMachineWise.Text = "Machine Wise"
        Me.rbtnMachineWise.UseVisualStyleBackColor = True
        '
        'rbtnDetail
        '
        Me.rbtnDetail.AutoSize = True
        Me.rbtnDetail.Checked = True
        Me.rbtnDetail.Location = New System.Drawing.Point(13, 6)
        Me.rbtnDetail.Name = "rbtnDetail"
        Me.rbtnDetail.Size = New System.Drawing.Size(60, 17)
        Me.rbtnDetail.TabIndex = 8
        Me.rbtnDetail.TabStop = True
        Me.rbtnDetail.Text = "Details"
        Me.rbtnDetail.UseVisualStyleBackColor = True
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(779, 357)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.AllowDragToGroup = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyExportAPI = False
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(779, 357)
        Me.gv1.TabIndex = 5
        Me.gv1.VarID = ""
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF})
        Me.btnExport.Location = New System.Drawing.Point(169, 12)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(83, 18)
        Me.btnExport.TabIndex = 349
        Me.btnExport.Text = "Export"
        '
        'btnExcel
        '
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.UseCompatibleTextRendering = False
        '
        'btnPDF
        '
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "PDF"
        Me.btnPDF.UseCompatibleTextRendering = False
        '
        'btngo
        '
        Me.btngo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btngo.Location = New System.Drawing.Point(11, 12)
        Me.btngo.Name = "btngo"
        Me.btngo.Size = New System.Drawing.Size(77, 18)
        Me.btngo.TabIndex = 347
        Me.btngo.Text = ">>>"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(90, 12)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(77, 18)
        Me.btnreset.TabIndex = 348
        Me.btnreset.Text = "Reset"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(254, 12)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(77, 18)
        Me.btnPrint.TabIndex = 345
        Me.btnPrint.Text = "Print"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(712, 12)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(77, 18)
        Me.btnclose.TabIndex = 346
        Me.btnclose.Text = "Close"
        '
        'rptMachineSurveyRegister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "rptMachineSurveyRegister"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "rptMachineSurveyRegister"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMilkAnalyzer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWeighmentMachine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents btnExport As RadSplitButton
    Friend WithEvents btnExcel As RadMenuItem
    Friend WithEvents btnPDF As RadMenuItem
    Friend WithEvents btngo As RadButton
    Friend WithEvents btnreset As RadButton
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents btnclose As RadButton
    Friend WithEvents lblMilkAnalyzer As common.Controls.MyLabel
    Friend WithEvents txtMultZone As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents rbtnDetail As RadioButton
    Friend WithEvents rbtnMachineWise As RadioButton
    Friend WithEvents rbtnUnionWise As RadioButton
    Friend WithEvents lblWeighmentMachine As common.Controls.MyLabel
    Friend WithEvents txtMultBMC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtMultDCS As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtMultUnion As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblUnion As common.Controls.MyLabel
    Friend WithEvents txtMultMilkAnalyzer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtMultWeighingMachine As common.UserControls.txtMultiSelectFinder
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox1 As RadGroupBox
End Class
